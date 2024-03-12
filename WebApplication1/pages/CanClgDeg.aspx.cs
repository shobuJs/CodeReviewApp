using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;



public partial class ACADEMIC_CanClgDeg : System.Web.UI.Page
{
    private Common objCommon = new Common();
    private UAIMS_Common objUCommon = new UAIMS_Common();
    AdmissionCancellationController objAcc = new AdmissionCancellationController();
    Student_Acd objsc = new Student_Acd();
    StudentFees studfees = new StudentFees();
    StudentInformation studinfo = new StudentInformation();
    StudentAddress studadd = new StudentAddress();  
   
    //ConnectionString
    private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();

                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                string IPADDRESS = string.Empty;

                IPADDRESS = ip.AddressList[0].ToString();
                ViewState["ipAddress"] = IPADDRESS;
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header

                if (Convert.ToInt32(Session["usertype"]) == 2)
                {
                    //DataSet ds1 = objCommon.FillDropDown("ACD_USER_CANCEL_COURSE", "Remark", "Admin_Remark,StudNameBank , BankName , AccountNo , InsertStatus, IfscCode , BranchAddress , PinCode , StateNo , CityNo  , StudMobNo", "ENROLLNO = " + Convert.ToInt64(Session["username"]), "");
                    DataSet ds1 = objCommon.FillDropDown("ACD_USER_CANCEL_COURSE", "Remark", "Admin_Remark,StudNameBank , BankName , AccountNo , InsertStatus, IfscCode , BranchAddress , PinCode , StateNo , CityNo  , StudMobNo", "ENROLLNO = '" + Session["username"] + "'", "");
                    if (ds1 != null && ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            txtAdminRemark.Text = ds1.Tables[0].Rows[0]["Admin_Remark"].ToString();
                            txtAdminRemark.Enabled = false;

                            txtRemark.Text = ds1.Tables[0].Rows[0]["Remark"].ToString();
                            txtStudNameBank.Text = ds1.Tables[0].Rows[0]["StudNameBank"].ToString();
                            txtBankName.Text = ds1.Tables[0].Rows[0]["BankName"].ToString();
                            txtAccountNumber.Text = ds1.Tables[0].Rows[0]["AccountNo"].ToString();
                            txtBranchCode.Text = ds1.Tables[0].Rows[0]["InsertStatus"].ToString();
                            txtIFSCCode.Text = ds1.Tables[0].Rows[0]["IfscCode"].ToString();
                            txtAddress.Text = ds1.Tables[0].Rows[0]["BranchAddress"].ToString();
                            txtPinCode.Text = ds1.Tables[0].Rows[0]["PinCode"].ToString();
                            //ddlState.SelectedValue = ds1.Tables[0].Rows[0]["StateNo"].ToString();
                            ////ddlCity.SelectedValue = ds1.Tables[0].Rows[0]["CityNo"].ToString();
                            txtStudMob.Text = ds1.Tables[0].Rows[0]["StudMobNo"].ToString();

                            studadd.PSTATE = Convert.ToInt32(ds1.Tables[0].Rows[0]["StateNo"].ToString());
                            studadd.PCITY = Convert.ToInt32(ds1.Tables[0].Rows[0]["CityNo"].ToString());
                            if (studadd.PSTATE > 0)
                            {
                                objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO > 0", "STATENO");
                                ddlState.SelectedValue = studadd.PSTATE.ToString();
                            }
                            if (studadd.PCITY > 0)
                            {
                                objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO > 0", "CITYNO");
                                ddlCity.SelectedValue = studadd.PCITY.ToString();
                            }

                            btnSubmit.Enabled = false;
                            btnCancel.Enabled = false;
                            chkDeclaration.Checked = true;
                            chkDeclaration.Enabled = false;
                            //txtRemark.Visible = true;
                            txtRemark.Enabled = false;

                            txtStudNameBank.Enabled = false;
                            txtBankName.Enabled = false;
                            txtAccountNumber.Enabled = false;
                            txtBranchCode.Enabled = false;
                            txtIFSCCode.Enabled = false;
                            txtAddress.Enabled = false;
                            txtPinCode.Enabled = false;
                            ddlState.Enabled = false;
                            ddlCity.Enabled = false;
                            txtStudMob.Enabled = false;
                        }
                        else
                        {
                            BindState();
                        }
                    }
                    else
                    {
                        BindState();
                    }

                    ShowDetails();
                    tblInfo.Visible = true;
                    divAllCoursesFromHist.Visible = true;
                    Div2.Visible = true;
                    DivAdm.Visible = false;
                    txtAdminRemark.Enabled = false;
                }
                else
                {
                    tblInfo.Visible = false;
                    DivAdm.Visible = true;
                    divAllCoursesFromHist.Visible = false;
                    Div2.Visible = false;
                    divSuccess.Visible = false;
                }

                Page.Title = Session["coll_name"].ToString();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
        }
    }

    private void BindState()
    {
        objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO > 0", "STATENAME");
        ddlState.SelectedIndex = 0;

        objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO > 0", "CITY");
        ddlCity.SelectedIndex = 0;
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SendSmstoParents.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SendSmstoParents.aspx");
        }
    }

    private void ShowDetails()
    {
        try
        {
            //DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_USER_ADDRESS A ON S.USERNO=A.USERNO", "S.ENROLLNO", "(S.STUDNAME)AS NAME,S.STUDENTMOBILE,S.EMAILID,(A.PADDRESS+','+A.PCITY+','+A.PSTATE)AS PADDRESS", "ISNULL(S.ADMCAN,0)=0 AND ISNULL(S.CAN,0)=0 AND S.ENROLLNO = '" + Convert.ToString(Session["username"]) + "'", string.Empty);//COMMENTED AS PER ROSHAN SIR 11082021 BY PANKAJ
            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S LEFT JOIN ACD_USER_ADDRESS A ON S.USERNO=A.USERNO", "S.ENROLLNO AS ENROLLNO", "(S.STUDNAME)AS NAME,S.STUDENTMOBILE,S.EMAILID,(A.PADDRESS+','+A.LDISTRICT+','+A.LCOUNTRY)AS PADDRESS", "S.ENROLLNO = '" + Convert.ToString(Session["username"]) + "'", string.Empty);
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    lblName.Text = dsStudent.Tables[0].Rows[0]["NAME"].ToString();
                    lblMobileNo.Text = dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                    lblEmail.Text = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString();
                    lblApplication.Text = dsStudent.Tables[0].Rows[0]["ENROLLNO"].ToString();
                    lblStudAddress.Text = dsStudent.Tables[0].Rows[0]["PADDRESS"].ToString();
                }
                else
                {
                    objCommon.DisplayMessage(updDetails, "Details not Found.Please contact to Admin", this.Page);
                    return;
                }
            }


            // int chkCount = Convert.ToInt32(objCommon.LookUp("ACD_USER_CANCEL_COURSE", "count(*)", "ENROLLNO=" + Session["username"])); //"incharge_mis_ghrcemp@raisoni.net";            
            int chkCount = Convert.ToInt32(objCommon.LookUp("ACD_USER_CANCEL_COURSE", "count(*)", "ENROLLNO='" + Session["username"] + "'")); //"incharge_mis_ghrcemp@raisoni.net";  
            if (chkCount > 0)
            {
                int CanAppliedCount = Convert.ToInt32(objCommon.LookUp("ACD_USER_CANCEL_COURSE", "count(*)", "isnull(CanStatus,0)=0 and INSERTSTATUS IN (1,2) and ENROLLNO='" + Session["username"] + "'"));// get cancel and applied entry count
                int DCRCount = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(*)", "ENROLLNMENTNO='" + Session["username"] + "'"));// get dcr entry count

                if (CanAppliedCount == DCRCount)
                {
                    //DataSet ds1 = objCommon.FillDropDown("ACD_USER_BRANCH_PREF BP INNER JOIN ACD_student S ON (BP.USERNO=S.USERNO AND S.DEGREENO=BP.DEGREENO AND S.COLLEGE_ID=BP.COLLEGE_ID) INNER JOIN ACD_DCR DC ON DC.IDNO=S.IDNO  INNER JOIN ACD_COLLEGE_MASTER CM ON CM.COLLEGE_ID=S.COLLEGE_ID  INNER JOIN ACD_DEGREE D ON D.DEGREENO=S.DEGREENO left join ACD_SUB_GROUP_MATER SUBG on (SUBG.SUGNO= BP.BRANCHNO) LEFT  JOIN ACD_USER_CANCEL_COURSE CC ON (CC.ENROLLNO=S.ENROLLNO AND CC.COLLEGE_ID=BP.COLLEGE_ID AND CC.DEGREENO=BP.DEGREENO)", "S.ENROLLNO", "CM.COLLEGE_NAME,S.COLLEGE_ID,(D.DEGREENAME+' '+'('++SUB_GROUP_NAME+')')AS DEGREENAME,S.DEGREENO	,SUBG.SUGNO AS PREFERENCE,TOTAL_AMT AS AMOUNT,  ORDER_ID AS ORDERID,REC_DT AS TRANSDATE,InsertStatus,BRPREF", "BP.USERNO = " + Convert.ToInt32(Session["username"]), "S.COLLEGE_ID");

                    //commed by pankaj nakhale 01092020 for new modification
                    //DataSet ds1 = objCommon.FillDropDown("ACD_USER_CANCEL_COURSE CC INNER JOIN ACD_COLLEGE_MASTER CM ON CM.COLLEGE_ID=CC.COLLEGE_ID  INNER JOIN ACD_DEGREE D ON D.DEGREENO=CC.DEGREENO  INNER JOIN ACD_SUB_GROUP_MATER SUBG ON (SUBG.SUGNO= CC.PREFERENCE)", "CC.ENROLLNO", "CM.COLLEGE_NAME,CC.COLLEGE_ID,(D.DEGREENAME+' '+'('++SUB_GROUP_NAME+')')AS DEGREENAME,CC.DEGREENO,SUBG.SUGNO AS PREFERENCE,AMOUNT AS AMOUNT,ORDERID AS ORDERID,TRANDATE AS TRANSDATE,INSERTSTATUS", "INSERTSTATUS IN (1,2) AND CC.ENROLLNO = " + Convert.ToInt32(Session["username"]), "CC.COLLEGE_ID");

                    // fro today updattion  02092020
                    //DataSet ds1 = objCommon.FillDropDown("ACD_USER_CANCEL_COURSE CC INNER JOIN ACD_COLLEGE_MASTER CM ON CM.COLLEGE_ID=CC.COLLEGE_ID  INNER JOIN ACD_DEGREE D ON D.DEGREENO=CC.DEGREENO  INNER JOIN ACD_SUB_GROUP_MATER SUBG ON (SUBG.SUGNO= CC.PREFERENCE)", "CC.ENROLLNO", "CM.COLLEGE_NAME,CC.COLLEGE_ID,(D.DEGREENAME+' '+'('++SUB_GROUP_NAME+')')AS DEGREENAME,CC.DEGREENO,SUBG.SUGNO AS PREFERENCE,AMOUNT AS AMOUNT,ORDERID AS ORDERID,TRANDATE AS TRANSDATE,INSERTSTATUS", "INSERTSTATUS ='2' AND ISNULL(CanStatus,0)=0 AND CC.ENROLLNO = " + Convert.ToInt32(Session["username"]), "CC.COLLEGE_ID");

                    DataSet ds1 = objAcc.GetCancelCourses(Session["username"].ToString());

                    if (ds1 != null && ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            lvCurrentSubjects.DataSource = ds1.Tables[0];
                            lvCurrentSubjects.DataBind();
                            btnSubmit.Enabled = true;
                            btnCancel.Enabled = true;

                            //ADDED BY PNAKAJ NAKHALE 11082021
                            foreach (ListViewDataItem item in lvCurrentSubjects.Items)
                            {
                                HiddenField hdfValid = item.FindControl("hdfValid") as HiddenField;
                                CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
                                if (hdfValid.Value == "1")
                                {
                                    chkAccept.Checked = true;
                                    chkAccept.Enabled = false;
                                }
                            }

                            //foreach (ListViewDataItem item in lvCurrentSubjects.Items)
                            //{
                            //    HiddenField hdfValid = item.FindControl("hdfValid") as HiddenField;
                            //    CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
                            //    if (hdfValid.Value == "1")
                            //    {
                            //        chkAccept.Checked = true;
                            //        chkAccept.Enabled = false;
                            //    }
                            //    //else if (hdfValid.Value == "2")
                            //    //{
                            //    //    // chkAccept.Checked = true;
                            //    //    chkAccept.Enabled = false;
                            //    //}
                            //}

                        }
                        else
                        {

                            divAllCoursesFromHist.Visible = false;
                        }
                    }
                    else
                    {

                        divAllCoursesFromHist.Visible = false;
                    }
                }
                else if (DCRCount > CanAppliedCount)
                {
                    DataSet ds1 = objAcc.GetCancelCourses(Session["username"].ToString());
                    if (ds1 != null && ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            lvCurrentSubjects.DataSource = ds1.Tables[0];
                            lvCurrentSubjects.DataBind();
                            btnSubmit.Enabled = true;
                            btnCancel.Enabled = true;
                            //ADDED BY PNAKAJ NAKHALE 11082021
                            foreach (ListViewDataItem item in lvCurrentSubjects.Items)
                            {
                                HiddenField hdfValid = item.FindControl("hdfValid") as HiddenField;
                                CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
                                if (hdfValid.Value == "1")
                                {
                                    chkAccept.Checked = true;
                                    chkAccept.Enabled = false;
                                }
                            }
                            //foreach (ListViewDataItem item in lvCurrentSubjects.Items)
                            //{
                            //    HiddenField hdfValid = item.FindControl("hdfValid") as HiddenField;
                            //    CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
                            //    if (hdfValid.Value == "1")
                            //    {
                            //        chkAccept.Checked = true;
                            //        chkAccept.Enabled = false;
                            //    }
                            //}
                        }
                        else
                        {

                            divAllCoursesFromHist.Visible = false;
                        }
                    }
                    else
                    {

                        divAllCoursesFromHist.Visible = false;
                    }
                }

                /////////for cancel course list i.e. Insertstatus=1 ///////////

                //DataSet dsCan = objCommon.FillDropDown("ACD_USER_CANCEL_COURSE CC INNER JOIN ACD_COLLEGE_MASTER CM ON CM.COLLEGE_ID=CC.COLLEGE_ID  INNER JOIN ACD_DEGREE D ON D.DEGREENO=CC.DEGREENO  INNER JOIN ACD_SUB_GROUP_MATER SUBG ON (SUBG.SUGNO= CC.PREFERENCE)", "CC.ENROLLNO", "CM.COLLEGE_NAME,CC.COLLEGE_ID,(D.DEGREENAME+' '+'('++SUB_GROUP_NAME+')')AS DEGREENAME,CC.DEGREENO,SUBG.SUGNO AS PREFERENCE,AMOUNT AS AMOUNT,ORDERID AS ORDERID,TRANDATE AS TRANSDATE,INSERTSTATUS,CANCEL_DATE as CANCELDATE,APPROVAL_DATE", "INSERTSTATUS ='1' AND CC.ENROLLNO = " + Convert.ToInt32(Session["username"]), "CC.COLLEGE_ID");
                //DataSet dsCan = objCommon.FillDropDown("ACD_USER_CANCEL_COURSE CC INNER JOIN ACD_COLLEGE_MASTER CM ON CM.COLLEGE_ID=CC.COLLEGE_ID  INNER JOIN ACD_DEGREE D ON D.DEGREENO=CC.DEGREENO  LEFT JOIN ACD_SUB_GROUP_MATER SUBG ON (SUBG.SUGNO= CC.PREFERENCE)", "CC.ENROLLNO", "CM.COLLEGE_NAME,CC.COLLEGE_ID,(D.DEGREENAME+' '+'('++SUB_GROUP_NAME+')')AS DEGREENAME,CC.DEGREENO,SUBG.SUGNO AS PREFERENCE,AMOUNT AS AMOUNT,ORDERID AS ORDERID,TRANDATE AS TRANSDATE,INSERTSTATUS,CANCEL_DATE as CANCELDATE,APPROVAL_DATE", "INSERTSTATUS ='1' AND CC.ENROLLNO = " + Convert.ToInt32(Session["username"]), "CC.COLLEGE_ID");
                DataSet dsCan = objCommon.FillDropDown("ACD_USER_CANCEL_COURSE CC INNER JOIN ACD_COLLEGE_MASTER CM ON CM.COLLEGE_ID=CC.COLLEGE_ID INNER JOIN ACD_STUDENT ST ON (ST.USERNO=CC.ENROLLNO AND ST.DEGREENO=CC.DEGREENO AND ST.COLLEGE_ID=CC.COLLEGE_ID ) INNER JOIN ACD_DEGREE D ON D.DEGREENO=CC.DEGREENO  LEFT JOIN acd_branch SUBG ON (SUBG.branchno= ST.BRANCHNO)", "CC.ENROLLNO", "CM.COLLEGE_NAME,CC.COLLEGE_ID,(D.DEGREENAME+' '+'('++longname+')')AS DEGREENAME,CC.DEGREENO,SUBG.branchno AS PREFERENCE,AMOUNT AS AMOUNT,ORDERID AS ORDERID,TRANDATE AS TRANSDATE,INSERTSTATUS,CANCEL_DATE as CANCELDATE,APPROVAL_DATE", "INSERTSTATUS ='1' AND CC.ENROLLNO = '" + Convert.ToString(Session["username"]) + "'", "CC.COLLEGE_ID");

                if (dsCan != null && dsCan.Tables.Count > 0)
                {
                    if (dsCan.Tables[0].Rows.Count > 0)
                    {
                        lvCANCELDATE.DataSource = dsCan.Tables[0];
                        lvCANCELDATE.DataBind();

                    }
                }
            }
            else
            {
                DataSet ds1 = objAcc.GetCancelCourses(Session["username"].ToString());
                if (ds1 != null && ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        lvCurrentSubjects.DataSource = ds1.Tables[0];
                        lvCurrentSubjects.DataBind();

                        foreach (ListViewDataItem item in lvCurrentSubjects.Items)
                        {
                            HiddenField hdfValid = item.FindControl("hdfValid") as HiddenField;
                            CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
                            if (hdfValid.Value == "1")
                            {
                                chkAccept.Checked = true;
                                chkAccept.Enabled = false;
                            }
                        }
                    }
                }
            }

            /////////for auto course only ///////////

            // DataSet dsAut = objCommon.FillDropDown("ACD_USER_BRANCH_PREF BP INNER JOIN ACD_COLLEGE_MASTER CM ON CM.COLLEGE_ID=BP.COLLEGE_ID  INNER JOIN ACD_DEGREE D ON D.DEGREENO=BP.DEGREENO left join ACD_SUB_GROUP_MATER SUBG on (SUBG.SUGNO= BP.BRANCHNO)", "BP.USERNO", "CM.COLLEGE_NAME,BP.COLLEGE_ID,(D.DEGREENAME+' '+'('++SUB_GROUP_NAME+')')AS DEGREENAME,D.DEGREENO	,SUBG.SUGNO AS PREFERENCE", "ISNULL(BP.APPLIED,0)=0 and BP.USERNO = " + Convert.ToInt32(Session["username"]), "BP.COLLEGE_ID");

            //DataSet dsAut = objCommon.FillDropDown("ACD_USER_BRANCH_PREF BP INNER JOIN ACD_COLLEGE_MASTER CM ON CM.COLLEGE_ID=BP.COLLEGE_ID  INNER JOIN ACD_DEGREE D ON D.DEGREENO=BP.DEGREENO left join ACD_SUB_GROUP_MATER SUBG on (SUBG.SUGNO= BP.BRANCHNO) LEFT JOIN ACD_STUDENT S ON  S.ENROLLNO=BP.USERNO", "distinct BP.USERNO", "CM.COLLEGE_NAME,BP.COLLEGE_ID,(D.DEGREENAME+' '+'('++SUB_GROUP_NAME+')')AS DEGREENAME,D.DEGREENO,SUBG.SUGNO AS PREFERENCE", "ISNULL(BP.APPLIED,0)=0 and bp.degreeno not in (select sa.degreeno from ACD_STUDENT sa where userno=" + Convert.ToInt32(Session["username"]) + " and sa.college_id = bp.college_id) and BP.USERNO = " + Convert.ToInt32(Session["username"]), "BP.COLLEGE_ID");
            DataSet dsAut = objCommon.FillDropDown("ACD_USER_BRANCH_PREF BP INNER JOIN ACD_COLLEGE_MASTER CM ON CM.COLLEGE_ID=BP.COLLEGE_ID  INNER JOIN ACD_DEGREE D ON D.DEGREENO=BP.DEGREENO left join ACD_SUB_GROUP_MATER SUBG on (SUBG.SUGNO= BP.BRANCHNO) LEFT JOIN ACD_STUDENT S ON  S.ENROLLNO=BP.USERNO", "distinct BP.USERNO", "CM.COLLEGE_NAME,BP.COLLEGE_ID,(D.DEGREENAME+' '+'('++SUB_GROUP_NAME+')')AS DEGREENAME,D.DEGREENO,SUBG.SUGNO AS PREFERENCE", "ISNULL(BP.APPLIED,0)=0 and bp.degreeno not in (select sa.degreeno from ACD_STUDENT sa where userno='" + Convert.ToString(Session["username"]) + "' and sa.college_id = bp.college_id) and BP.USERNO = '" + Convert.ToString(Session["username"]) + "'", "BP.COLLEGE_ID");

            if (dsAut != null && dsAut.Tables.Count > 0)
            {
                if (dsAut.Tables[0].Rows.Count > 0)
                {
                    lvAut.DataSource = dsAut.Tables[0];
                    lvAut.DataBind();

                }
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void chkAccept_CheckedChanged(object sender, EventArgs e)
    {

    }

    private string getcourseno()
    {
        try
        {
            string retCNO = string.Empty;
            foreach (ListViewDataItem item in lvCurrentSubjects.Items)
            {
                CheckBox cbRow = item.FindControl("chkAccept") as CheckBox;
                if (cbRow.Checked && cbRow.Enabled)
                {
                    if (retCNO.Length == 0) retCNO = ((item.FindControl("lblDegree")) as Label).ToolTip.ToString();
                    else
                        retCNO += "," + ((item.FindControl("lblDegree")) as Label).ToolTip.ToString();
                }
            }
            if (retCNO.Equals(""))
            {
                return "0";
            }
            else
            {
                return retCNO;
            }
        }
        catch { return null; }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string courseno = "";
        courseno = "1"; // getcourseno();
        if (courseno == "0")
        {
            objCommon.DisplayMessage(updDetails, "Please Select At least One Degree For Apply Cancelation Of Admission Courses!!", this.Page);
            return;
        }
        else
        {
            //for select course
            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {

                //Get Student Details from lvStudent
                CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                HiddenField hdnDegreePreference = dataitem.FindControl("hdnDegreePreference") as HiddenField;
                // HiddenField hdfPrefe = dataitem.FindControl("hdfPrefe") as HiddenField;

                //if (cbRow.Checked == true && cbRow.Enabled == false)
                if (cbRow.Checked == true)
                {
                    objsc.college += ((dataitem.FindControl("lblCollege")) as Label).ToolTip + "$";
                    objsc.degree += ((dataitem.FindControl("lblDegree")) as Label).ToolTip + "$";
                    objsc.DegreePref += hdnDegreePreference.Value + "$";
                    objsc.Amount += ((dataitem.FindControl("lblAmount")) as Label).ToolTip + "$";
                    objsc.OrdereID += ((dataitem.FindControl("lblOrderId")) as Label).ToolTip + "$";
                    objsc.TranDate += ((dataitem.FindControl("lblTransdate")) as Label).ToolTip + "$";
                    //  prefe += hdfPrefe.Value + "$";
                }
                objsc.collegeid = objsc.college.TrimEnd('$');
                objsc.degreenoo = objsc.degree.TrimEnd('$');
                objsc.DegreePrefs = objsc.DegreePref.TrimEnd('$');
                objsc.Amounts = objsc.Amount.TrimEnd('$');
                objsc.OrdereIDs = objsc.OrdereID.TrimEnd('$');
                objsc.TranDates = objsc.TranDate.TrimEnd('$');
                //   prefef = prefe.TrimEnd('$');
                objsc.chk = 1;
            }
            //////////////////////start applied 10082021 pankaj nakhale
            ////for unselect course
            //foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            //{

            //    //Get Student Details from lvStudent
            //    CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
            //    HiddenField hdnDegreePreferenceUnChk = dataitem.FindControl("hdnDegreePreference") as HiddenField;
            //    if (cbRow.Checked == false && cbRow.Enabled == true)
            //    {
            //        if (cbRow.Checked == false && cbRow.Enabled == true)
            //        {
            //            cs.collegeUnChk += ((dataitem.FindControl("lblCollege")) as Label).ToolTip + "$";
            //            cs.degreeUnChk += ((dataitem.FindControl("lblDegree")) as Label).ToolTip + "$";
            //            cs.DegreePrefUnChk += hdnDegreePreferenceUnChk.Value + "$";
            //            cs.AmountUnChk += ((dataitem.FindControl("lblAmount")) as Label).ToolTip + "$";
            //            cs.OrdereIDUnChk += ((dataitem.FindControl("lblOrderId")) as Label).ToolTip + "$";
            //            cs.TranDateUnChk += ((dataitem.FindControl("lblTransdate")) as Label).ToolTip + "$";
            //        }
            //        cs.collegeidUnChk = cs.collegeUnChk.TrimEnd('$');
            //        cs.degreenoUnChk = cs.degreeUnChk.TrimEnd('$');
            //        cs.DegreePrefsUnChk = cs.DegreePrefUnChk.TrimEnd('$');
            //        cs.AmountsUnChk = cs.AmountUnChk.TrimEnd('$');
            //        cs.OrdereIDsUnChk = cs.OrdereIDUnChk.TrimEnd('$');
            //        cs.TranDatesUnChk = cs.TranDateUnChk.TrimEnd('$');
            //        cs.unchkStatus = 1;
            //    }
            //}
            //////////////////////end applied 10082021 pankaj nakhale
            //            ////////////////////start autocancel 10082021 pankaj nakhale
            //            int chkCount = Convert.ToInt32(objCommon.LookUp("ACD_USER_CANCEL_COURSE", "count(1)", "ENROLLNO=" + Session["username"])); //"incharge_mis_ghrcemp@raisoni.net";  
            //            if (chkCount > 0)
            //            {

            //            }
            //            else
            //            {
            //                // for auto select

            //                foreach (ListViewDataItem dataitem in lvAut.Items)
            //                {

            //                    //Get Student Details from lvStudent
            //                    CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
            //                    HiddenField hdnDegreePreferenceAut = dataitem.FindControl("hdnDegreePreference") as HiddenField;

            //                    if (cbRow.Checked == true && cbRow.Enabled == false)
            //                    {
            //                        cs.collegeAut += ((dataitem.FindControl("lblCollege")) as Label).ToolTip + "$";
            //                        cs.degreeAut += ((dataitem.FindControl("lblDegree")) as Label).ToolTip + "$";
            //                        cs.DegreePrefAut += hdnDegreePreferenceAut.Value + "$";
            //                    }
            //                    cs.collegeidAut = cs.collegeAut.TrimEnd('$');
            //                    cs.degreenoAut = cs.degreeAut.TrimEnd('$');
            //                    cs.DegreePrefsAut = cs.DegreePrefAut.TrimEnd('$');
            //                    cs.aut = 1;
            //                }
            //            }
            //////////////////////end autocancel 10082021 pankaj nakhale
            objsc.EnrollmentNo = lblApplication.Text;


            if (txtRemark.Text == "")
            {
                objsc.Remarks = null;
            }
            else
            {
                objsc.Remarks = txtRemark.Text;
            }

            objsc.StudName = txtStudNameBank.Text;
            studfees.BankName = txtBankName.Text;
            studinfo.Acc_no = txtAccountNumber.Text;
            studinfo.Bank_branch = txtBranchCode.Text;
            studinfo.Ifsc_code = txtIFSCCode.Text;
            studadd.PADDRESS = txtAddress.Text;
            studadd.PPINCODE = txtPinCode.Text;
            studadd.PSTATE = Convert.ToInt32(ddlState.SelectedValue);
            studadd.PCITY = Convert.ToInt32(ddlCity.SelectedValue);
            studadd.PMOBILE = txtStudMob.Text;
            objsc.StudIPAddress = Session["ipAddress"].ToString();

            //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script", "alert('Do you Really want to Cancel the Admission Courses?')", true);

            int ret = objAcc.InsertCancelStudent(objsc, studfees, studinfo, studadd);

            if (ret == 1)
            {
                objCommon.DisplayMessage(updDetails, "Admission Courses  Cancel Successfully..", this.Page);

                btnSubmit.Enabled = false;
                btnCancel.Enabled = false;
                chkDeclaration.Checked = true;
                chkDeclaration.Enabled = false;
                //txtRemark.Visible = true;
                txtRemark.Enabled = false;

                txtStudNameBank.Enabled = false;
                txtBankName.Enabled = false;
                txtAccountNumber.Enabled = false;
                txtBranchCode.Enabled = false;
                txtIFSCCode.Enabled = false;
                txtAddress.Enabled = false;
                txtPinCode.Enabled = false;
                ddlState.Enabled = false;
                ddlCity.Enabled = false;
                txtStudMob.Enabled = false;
                foreach (ListViewDataItem dataitem1 in lvCurrentSubjects.Items)
                {
                    btnSubmit.Enabled = false;
                    btnCancel.Enabled = false;
                    chkDeclaration.Enabled = false;
                    txtRemark.Enabled = false;
                }
                /////
                //for MAIL MASSAGE
                foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
                {

                    //Get Student Details from lvStudent
                    CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                    // HiddenField hdfPrefe = dataitem.FindControl("hdfPrefe") as HiddenField;
                    if (cbRow.Checked == true)
                    {
                        string degree = string.Empty;
                        string degreeno = string.Empty;
                        string college = string.Empty;
                        string collegeno = string.Empty;
                        if (cbRow.Checked == true)
                        {
                            degree += ((dataitem.FindControl("lblDegree")) as Label).ToolTip + "$";
                            college += ((dataitem.FindControl("lblCollege")) as Label).ToolTip + "$";
                            //  prefe += hdfPrefe.Value + "$";
                        }

                        degreeno = degree.TrimEnd('$');
                        collegeno = college.TrimEnd('$');
                        // prefef = prefe.TrimEnd('$');
                        string Degreename = string.Empty;
                        Degreename = objCommon.LookUp("ACD_DEGREE", "DEGREENAME", "DEGREENO IN ('" + degreeno + "')");
                        string collegeName = string.Empty;
                        collegeName = objCommon.LookUp("ACD_COLLEGE_MASTER", "COLLEGE_NAME", "COLLEGE_ID IN ('" + collegeno + "')");

                        ////////////for email //////////           
                        string Emassage = "Your request for Cancellation of Admission from " + Degreename + " For " + collegeName + " has been received for the application" +
                          " id No. " + lblApplication.Text + " on Date " + DateTime.Now.ToString("dd/MM/yyyy") + ". It will be forwarded for due processing. It will be processed and refund " + " generated within 30 days from the date of the request.Please quote the Application ID  No. for all further correspondence.";


                        string Subject = "Cancellation of Admission.";

                        if (lblEmail.Text != string.Empty)
                        {
                            //this.TransferToEmail(lblEmail.Text, Emassage, Subject);
                            //14102021 Execute(Emassage, lblEmail.Text, Subject).Wait();
                            objCommon.DisplayUserMessage(this.updDetails, "Email Successfully Send To Student(s)", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updDetails, "Sorry..! Didn't found Mobile no. for some Parent(s)", this.Page);
                        }

                        /////////////////for massage //////////////////////                      
                        if (txtStudMob.Text != string.Empty)
                        {
                            //14102021  this.SendSMS(txtStudMob.Text, Emassage);
                            objCommon.DisplayUserMessage(updDetails, "SMS send succesfully", this.Page);
                        }
                        else
                            objCommon.DisplayMessage("Sorry..! Dont find Mobile no. for some employee", this.Page);
                    }
                }
                ShowDetails();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "showPopup", "$('#myModal33').modal('show')", true);
                undertaking.Visible = true;
            }
        }
    }
    public void TransferToEmail(string mailId, string Emassage, string Subject)
    {
        try
        {
            string EMAILID = mailId.Trim();
            var fromAddress = objCommon.LookUp("REFF", "LTRIM(RTRIM(EMAILSVCID))", ""); //"incharge_mis_ghrcemp@raisoni.net";   

            // any address where the email will be sending
            var toAddress = EMAILID.Trim();
            //Password of your gmail address

            var fromPassword = objCommon.LookUp("REFF", "LTRIM(RTRIM(EMAILSVCPWD))", "");   // const string fromPassword = "thebestofall";  

            // Passing the values and make a email formate to display

            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            //msg.From = new MailAddress(fromAddress, "HSNC University");
            msg.From = new MailAddress("noreply@mastersofterp.in", "HSNC University");
            msg.To.Add(new MailAddress(toAddress));
            //  msg.To.Add(new MailAddress("satish.tichkule@mastersofterp.co.in"));
            msg.Subject = Subject;
            // msg.Subject = Emassage;
            //  msg.Body = Emassage;

            using (StreamReader reader = new StreamReader(Server.MapPath("~/email_template_Cancel course.html")))
            {
                msg.Body = reader.ReadToEnd();
            }

            msg.Body = msg.Body.Replace("{Name}", lblName.Text);
            msg.Body = msg.Body.Replace("{Massage}", Emassage.ToString());

            msg.IsBodyHtml = true;
            //smtp.enableSsl = "true";            
            smtp.Host = "smtp.sendgrid.net";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("mastersoftsupport", "Iitms@123");
            ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate,
                X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
            try
            {
                smtp.Send(msg);
            }
            catch (Exception ex)
            { }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Admission_NewStudent.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void SendSMS(string Mobile, string text)
    {
        string status = "";
        try
        {
            string Message = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?"));
                request.ContentType = "text/xml; charset=utf-8";
                request.Method = "POST";
                string TemplateID = "1007966951769871255";// "1007811562228174047";// "1007082884952499778";
                string postDate = "ID=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                postDate += "&";
                postDate += "Pwd=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                postDate += "&";
                //postDate += "PhNo=91" + Mobile;
                postDate += "PhNo=" + Mobile;
                postDate += "&";
                postDate += "Text=" + text;
                postDate += "&TemplateID=" + TemplateID;
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
                request.ContentType = "application/x-www-form-urlencoded";

                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse _webresponse = request.GetResponse();
                dataStream = _webresponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                status = reader.ReadToEnd();
            }
            else
            {
                status = "0";
            }

        }
        catch
        {

        }

    }
    //public void SendSMS(string mobno, string message, string TemplateID = "")
    //{
    //    try
    //    {
    //        DataSet dss = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");

    //        string Url = string.Format("http://" + dss.Tables[0].Rows[0]["SMSProvider"].ToString() + "?");//"http://smsnmms.co.in/sms.aspx";
    //        Session["url"] = Url;
    //        string UserId = dss.Tables[0].Rows[0]["SMSSVCID"].ToString(); //"hr@iitms.co.in";
    //        Session["userid"] = UserId;

    //        string Password = dss.Tables[0].Rows[0]["SMSSVCPWD"].ToString();//"iitmsTEST@5448";
    //        Session["pwd"] = Password;

    //        WebRequest request = HttpWebRequest.Create("" + Url + "ID=" + UserId + "&PWD=" + Password + "&PHNO=" + mobno + "&TEXT=" + message + "&TemplateID=" + TemplateID + "");
    //        WebResponse response = request.GetResponse();
    //        System.IO.StreamReader reader = new StreamReader(response.GetResponseStream());
    //        string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need
    //        //return urlText;
    //    }
    //    catch (Exception)
    //    {
    //    }
    //}
    //public void SendSMS(string Mobile, string text, string TemplateID = "")
    //{
    //    string status = "";
    //    try
    //    {
    //        string Message = string.Empty;
    //        DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?"));
    //            request.ContentType = "text/xml; charset=utf-8";
    //            request.Method = "POST";

    //            string postDate = "ID=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
    //            postDate += "&";
    //            postDate += "Pwd=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
    //            postDate += "&";
    //            postDate += "PhNo=91" + Mobile;
    //            // postDate += "PhNo=" + Mobile;
    //            postDate += "&";
    //            postDate += "Text=" + text;
    //            postDate += "&";
    //            postDate += "TemplateID=" + TemplateID;

    //            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
    //            request.ContentType = "application/x-www-form-urlencoded";

    //            request.ContentLength = byteArray.Length;
    //            Stream dataStream = request.GetRequestStream();
    //            dataStream.Write(byteArray, 0, byteArray.Length);
    //            dataStream.Close();
    //            WebResponse _webresponse = request.GetResponse();
    //            dataStream = _webresponse.GetResponseStream();
    //            StreamReader reader = new StreamReader(dataStream);
    //            status = reader.ReadToEnd();
    //        }
    //        else
    //        {
    //            status = "0";
    //        }
    //    }
    //    catch
    //    {
    //    }
    //}
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        div1.Visible = false;
        divSuccess.Visible = false;
        chkDeclaration.Checked = false;
        txtRemark.Text = string.Empty;
        txtApplication.Text = string.Empty;
        Div2.Visible = false;
        tblInfo.Visible = false;
        divAllCoursesFromHist.Visible = false;

        txtStudNameBank.Text = string.Empty;
        txtBankName.Text = string.Empty;
        txtAccountNumber.Text = string.Empty;
        txtBranchCode.Text = string.Empty;
        txtIFSCCode.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtPinCode.Text = string.Empty;
        ddlState.SelectedIndex = 0;
        ddlCity.SelectedIndex = 0;
        txtStudMob.Text = string.Empty;

    }
    private void ShowDetailsForAdm()
    {
        try
        {
            int chkStudData = Convert.ToInt32(objCommon.LookUp("ACD_USER_CANCEL_COURSE", "count(*)", "ENROLLNO='" + (txtApplication.Text) + "'")); //"incharge_mis_ghrcemp@raisoni.net";  
            if (chkStudData > 0)
            {
                //DataSet ds1 = objCommon.FillDropDown("ACD_USER_BRANCH_PREF BP INNER JOIN ACD_student S ON (BP.USERNO=S.USERNO AND S.DEGREENO=BP.DEGREENO AND S.COLLEGE_ID=BP.COLLEGE_ID) INNER JOIN ACD_DCR DC ON DC.IDNO=S.IDNO  INNER JOIN ACD_COLLEGE_MASTER CM ON CM.COLLEGE_ID=S.COLLEGE_ID  INNER JOIN ACD_DEGREE D ON D.DEGREENO=S.DEGREENO left join ACD_SUB_GROUP_MATER SUBG on (SUBG.SUGNO= BP.BRANCHNO) LEFT  JOIN ACD_USER_CANCEL_COURSE CC ON (CC.ENROLLNO=S.ENROLLNO AND CC.COLLEGE_ID=BP.COLLEGE_ID AND CC.DEGREENO=BP.DEGREENO)", "S.ENROLLNO", "CM.COLLEGE_NAME,S.COLLEGE_ID,(D.DEGREENAME+' '+'('++SUB_GROUP_NAME+')')AS DEGREENAME,S.DEGREENO	,SUBG.SUGNO AS PREFERENCE,TOTAL_AMT AS AMOUNT,  ORDER_ID AS ORDERID,REC_DT AS TRANSDATE,InsertStatus,BRPREF", "BP.USERNO = " + Convert.ToInt32(txtApplication.Text), "S.COLLEGE_ID");
                // comment by pankaj nakhale 03092020 for transdate not bind
                //DataSet ds1 = objCommon.FillDropDown("ACD_USER_CANCEL_COURSE CC INNER JOIN ACD_COLLEGE_MASTER CM ON CM.COLLEGE_ID=CC.COLLEGE_ID  INNER JOIN ACD_DEGREE D ON D.DEGREENO=CC.DEGREENO  INNER JOIN ACD_SUB_GROUP_MATER SUBG ON (SUBG.SUGNO= CC.PREFERENCE)", "CC.ENROLLNO", "CM.COLLEGE_NAME,CC.COLLEGE_ID,(D.DEGREENAME+' '+'('++SUB_GROUP_NAME+')')AS DEGREENAME,CC.DEGREENO,SUBG.SUGNO AS PREFERENCE,AMOUNT AS AMOUNT,ORDERID AS ORDERID,TRANDATE AS TRANSDATE,INSERTSTATUS", "ISNULL(CANSTATUS,0)=0 and ISNULL(ADMIN_APPROVAL,0)=0 AND INSERTSTATUS ='1' AND CC.ENROLLNO = " + Convert.ToInt32(txtApplication.Text), "CC.COLLEGE_ID");
                //PANKAJ18NOV2020 DataSet ds1 = objCommon.FillDropDown("ACD_USER_CANCEL_COURSE CC INNER JOIN ACD_COLLEGE_MASTER CM ON CM.COLLEGE_ID=CC.COLLEGE_ID  INNER JOIN ACD_DEGREE D ON D.DEGREENO=CC.DEGREENO  INNER JOIN ACD_SUB_GROUP_MATER SUBG ON (SUBG.SUGNO= CC.PREFERENCE) INNER JOIN ACD_DCR DC ON (DC.ENROLLNMENTNO=CC.ENROLLNO AND DC.ORDER_ID=CC.ORDERID)", "CC.ENROLLNO", "CM.COLLEGE_NAME,CC.COLLEGE_ID,(D.DEGREENAME+' '+'('++SUB_GROUP_NAME+')')AS DEGREENAME,CC.DEGREENO,SUBG.SUGNO AS PREFERENCE,CC.AMOUNT AS AMOUNT,ORDERID AS ORDERID,REC_DT AS TRANSDATE,INSERTSTATUS", "ISNULL(CANSTATUS,0)=0 and ISNULL(ADMIN_APPROVAL,0)=0 AND INSERTSTATUS ='1' AND CC.ENROLLNO = " + Convert.ToInt32(txtApplication.Text), "CC.COLLEGE_ID");
                DataSet ds1 = objCommon.FillDropDown("ACD_USER_CANCEL_COURSE CC INNER JOIN ACD_COLLEGE_MASTER CM ON CM.COLLEGE_ID=CC.COLLEGE_ID  INNER JOIN ACD_DEGREE D ON D.DEGREENO=CC.DEGREENO  LEFT JOIN acd_branch SUBG ON (SUBG.branchno= CC.PREFERENCE) LEFT JOIN ACD_DCR DC ON (DC.ENROLLNMENTNO=CC.ENROLLNO AND DC.ORDER_ID=CC.ORDERID)", "CC.ENROLLNO", "CM.COLLEGE_NAME,CC.COLLEGE_ID,(D.DEGREENAME+' '+'('++longname+')')AS DEGREENAME,CC.DEGREENO,SUBG.branchno AS PREFERENCE,CC.AMOUNT AS AMOUNT,ORDERID AS ORDERID,REC_DT AS TRANSDATE,INSERTSTATUS", "ISNULL(CANSTATUS,0)=0 and ISNULL(ADMIN_APPROVAL,0)=0 AND INSERTSTATUS ='1' AND CC.ENROLLNO = '" + Convert.ToString(txtApplication.Text) + "'", "CC.COLLEGE_ID");

                if (ds1 != null && ds1.Tables.Count > 0)
                {   //bind degree college details
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        lvCurrentSubjects.DataSource = ds1.Tables[0];
                        lvCurrentSubjects.DataBind();
                        btnApprove.Visible = true;
                        btnApprove.Enabled = true;
                        btnCancel.Enabled = true;

                        foreach (ListViewDataItem item in lvCurrentSubjects.Items)
                        {
                            HiddenField hdfValid = item.FindControl("hdfValid") as HiddenField;
                            CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
                            if (hdfValid.Value == "1")
                            {
                                chkAccept.Checked = true;
                                chkAccept.Enabled = false;
                            }
                            else if (hdfValid.Value == "2")
                            {
                                //  chkAccept.Checked = true;
                                chkAccept.Enabled = false;
                            }
                        }
                        divAllCoursesFromHist.Visible = true;
                    }
                    else
                    {
                        divAllCoursesFromHist.Visible = false;
                    }
                }
                else
                {
                    divAllCoursesFromHist.Visible = false;
                }
                Div2.Visible = true;
                tblInfo.Visible = true;
                //    divAllCoursesFromHist.Visible = true;
                //bind student details
                //DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_USER_ADDRESS A ON S.USERNO=A.USERNO", "S.ENROLLNO", "(S.STUDNAME)AS NAME,S.STUDENTMOBILE,S.EMAILID,(A.PADDRESS+','+A.PCITY+','+A.PSTATE)AS PADDRESS", "ISNULL(S.ADMCAN,0)=0 AND ISNULL(S.CAN,0)=0 AND S.ENROLLNO = '" + Convert.ToString(txtApplication.Text) + "'", string.Empty);//COMENETD AS PER ROSHAN SIR 11082021 BY PANKAJ NAKHALE
                DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S LEFT JOIN ACD_USER_ADDRESS A ON S.USERNO=A.USERNO", "S.ENROLLNO AS ENROLLNO", "(S.STUDNAME)AS NAME,S.STUDENTMOBILE,S.EMAILID,(A.PADDRESS+','+A.LADDRESS+','+A.LDISTRICT)AS PADDRESS", "S.ENROLLNO = '" + Convert.ToString(txtApplication.Text) + "'", string.Empty);

                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        lblName.Text = dsStudent.Tables[0].Rows[0]["NAME"].ToString();
                        lblMobileNo.Text = dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                        lblEmail.Text = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString();
                        lblApplication.Text = dsStudent.Tables[0].Rows[0]["ENROLLNO"].ToString();
                        lblStudAddress.Text = dsStudent.Tables[0].Rows[0]["PADDRESS"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updDetails, "Details not Found.Please contact to Admin", this.Page);
                        return;
                    }
                }

                DataSet dsdata = objCommon.FillDropDown("ACD_USER_CANCEL_COURSE", "Remark",
"Admin_Remark,StudNameBank , BankName , AccountNo , InsertStatus, IfscCode , BranchAddress , PinCode , StateNo , CityNo  , StudMobNo", "ENROLLNO = '" + Convert.ToString(txtApplication.Text) + "'", "");
                if (dsdata != null && dsdata.Tables.Count > 0)
                {
                    if (dsdata.Tables[0].Rows.Count > 0)
                    {
                        txtAdminRemark.Text = dsdata.Tables[0].Rows[0]["Admin_Remark"].ToString();
                        txtAdminRemark.Enabled = true;

                        txtRemark.Text = dsdata.Tables[0].Rows[0]["Remark"].ToString();
                        txtStudNameBank.Text = dsdata.Tables[0].Rows[0]["StudNameBank"].ToString();
                        txtBankName.Text = dsdata.Tables[0].Rows[0]["BankName"].ToString();
                        txtAccountNumber.Text = dsdata.Tables[0].Rows[0]["AccountNo"].ToString();
                        txtBranchCode.Text = dsdata.Tables[0].Rows[0]["InsertStatus"].ToString();
                        txtIFSCCode.Text = dsdata.Tables[0].Rows[0]["IfscCode"].ToString();
                        txtAddress.Text = dsdata.Tables[0].Rows[0]["BranchAddress"].ToString();
                        txtPinCode.Text = dsdata.Tables[0].Rows[0]["PinCode"].ToString();
                        //ddlState.SelectedValue = ds1.Tables[0].Rows[0]["StateNo"].ToString();
                        ////ddlCity.SelectedValue = ds1.Tables[0].Rows[0]["CityNo"].ToString();
                        txtStudMob.Text = dsdata.Tables[0].Rows[0]["StudMobNo"].ToString();

                        studadd.PSTATE = Convert.ToInt32(dsdata.Tables[0].Rows[0]["StateNo"].ToString());
                        studadd.PCITY = Convert.ToInt32(dsdata.Tables[0].Rows[0]["CityNo"].ToString());
                        if (studadd.PSTATE > 0)
                        {
                            objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO > 0", "STATENO");
                            ddlState.SelectedValue = studadd.PSTATE.ToString();
                        }
                        if (studadd.PCITY > 0)
                        {
                            objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO > 0", "CITYNO");
                            ddlCity.SelectedValue = studadd.PCITY.ToString();
                        }



                        btnSubmit.Enabled = false;
                        //   btnCancel.Enabled = false;
                        chkDeclaration.Checked = true;
                        chkDeclaration.Enabled = false;
                        //txtRemark.Visible = true;
                        txtRemark.Enabled = false;

                        txtStudNameBank.Enabled = false;
                        txtBankName.Enabled = false;
                        txtAccountNumber.Enabled = false;
                        txtBranchCode.Enabled = false;
                        txtIFSCCode.Enabled = false;
                        txtAddress.Enabled = false;
                        txtPinCode.Enabled = false;
                        ddlState.Enabled = false;
                        ddlCity.Enabled = false;
                        txtStudMob.Enabled = false;
                        divSuccess.Visible = true;
                    }
                }

                /////////for auto course only ///////////

                //  DataSet dsAut = objCommon.FillDropDown("ACD_USER_BRANCH_PREF BP INNER JOIN ACD_COLLEGE_MASTER CM ON CM.COLLEGE_ID=BP.COLLEGE_ID  INNER JOIN ACD_DEGREE D ON D.DEGREENO=BP.DEGREENO left join ACD_SUB_GROUP_MATER SUBG on (SUBG.SUGNO= BP.BRANCHNO)", "BP.USERNO", "CM.COLLEGE_NAME,BP.COLLEGE_ID,(D.DEGREENAME+' '+'('++SUB_GROUP_NAME+')')AS DEGREENAME,D.DEGREENO	,SUBG.SUGNO AS PREFERENCE", "ISNULL(BP.APPLIED,0)=0 and BP.USERNO = " + Convert.ToInt32(txtApplication.Text), "BP.COLLEGE_ID");

                DataSet dsAut = objCommon.FillDropDown("ACD_USER_BRANCH_PREF BP INNER JOIN ACD_COLLEGE_MASTER CM ON CM.COLLEGE_ID=BP.COLLEGE_ID  INNER JOIN ACD_DEGREE D ON D.DEGREENO=BP.DEGREENO left join ACD_SUB_GROUP_MATER SUBG on (SUBG.SUGNO= BP.BRANCHNO) LEFT JOIN ACD_STUDENT S ON  S.ENROLLNO=BP.USERNO", "distinct BP.USERNO", "CM.COLLEGE_NAME,BP.COLLEGE_ID,(D.DEGREENAME+' '+'('++SUB_GROUP_NAME+')')AS DEGREENAME,D.DEGREENO,SUBG.SUGNO AS PREFERENCE", "ISNULL(BP.APPLIED,0)=0 and bp.degreeno not in (select sa.degreeno from ACD_STUDENT sa where userno=" + Convert.ToInt32(txtApplication.Text) + " and sa.college_id = bp.college_id) and BP.USERNO = " + Convert.ToInt32(txtApplication.Text), "BP.COLLEGE_ID");

                if (dsAut != null && dsAut.Tables.Count > 0)
                {
                    if (dsAut.Tables[0].Rows.Count > 0)
                    {
                        lvAut.DataSource = dsAut.Tables[0];
                        lvAut.DataBind();

                    }
                }
                /////////for cancel course list i.e. Insertstatus=1 02092020 pankaj///get approved list////////

                DataSet dsCan = objCommon.FillDropDown("ACD_USER_CANCEL_COURSE CC INNER JOIN ACD_COLLEGE_MASTER CM ON CM.COLLEGE_ID=CC.COLLEGE_ID  INNER JOIN ACD_DEGREE D ON D.DEGREENO=CC.DEGREENO  INNER JOIN ACD_SUB_GROUP_MATER SUBG ON (SUBG.SUGNO= CC.PREFERENCE)", "CC.ENROLLNO", "CM.COLLEGE_NAME,CC.COLLEGE_ID,(D.DEGREENAME+' '+'('++SUB_GROUP_NAME+')')AS DEGREENAME,CC.DEGREENO,SUBG.SUGNO AS PREFERENCE,AMOUNT AS AMOUNT,ORDERID AS ORDERID,TRANDATE AS TRANSDATE,INSERTSTATUS,CANCEL_DATE as CANCELDATE,APPROVAL_DATE", "ISNULL(CANSTATUS,0)=0 and ADMIN_APPROVAL=1 AND INSERTSTATUS ='1' AND CC.ENROLLNO = '" + Convert.ToString(txtApplication.Text) + "'", "CC.COLLEGE_ID");

                if (dsCan != null && dsCan.Tables.Count > 0)
                {
                    if (dsCan.Tables[0].Rows.Count > 0)
                    {
                        lvCANCELDATE.DataSource = dsCan.Tables[0];
                        lvCANCELDATE.DataBind();

                    }
                }

                //checked allready approved or not
                int count = Convert.ToInt32(objCommon.LookUp("ACD_USER_CANCEL_COURSE", "count(*)", "ADMIN_APPROVAL=1 and ENROLLNO='" + (txtApplication.Text) + "'")); //"incharge_mis_ghrcemp@raisoni.net";  
                if (count > 0)
                {
                    // objCommon.DisplayMessage(updDetails, "For Application Number : " + txtApplication.Text + " Admission Cancel Courses Already Approved.", this.Page);
                    //btnApprove.Visible = true;
                    //btnApprove.Enabled = false;
                    //btnCancel.Enabled = false;
                    // get admin remark
                    DataSet dsremark = objCommon.FillDropDown("ACD_USER_CANCEL_COURSE", "Admin_Remark", "", "ADMIN_APPROVAL=1 and ENROLLNO = '" + Convert.ToInt32(txtApplication.Text) + "'", "");
                    if (dsremark != null && dsremark.Tables.Count > 0)
                    {
                        if (dsremark.Tables[0].Rows.Count > 0)
                        {
                            txtAdminRemark.Text = dsremark.Tables[0].Rows[0]["Admin_Remark"].ToString();
                            txtAdminRemark.Enabled = false;
                        }
                    }

                }
                else
                {
                    btnApprove.Visible = true;
                    btnApprove.Enabled = true;
                }


                //    }

                //    }
                divSuccess.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(updDetails, "For  Application Number :" + txtApplication.Text + " Not Cancel Any Admission Courses", this.Page);
                Div2.Visible = false;
                divAllCoursesFromHist.Visible = false;
                tblInfo.Visible = false;
                btnApprove.Visible = false;
                divSuccess.Visible = false;
                return;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            //int chkStudAdmt = 0;
            //chkStudAdmt = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "count(1)", "ENROLLNO='" + txtApplication.Text + "' AND ISNULL(ADMCAN,0)=0 AND ISNULL(CAN,0)=0")); //"incharge_mis_ghrcemp@raisoni.net";  
            //if (chkStudAdmt > 0)
            //{
            int chkStudData = Convert.ToInt32(objCommon.LookUp("ACD_USER_CANCEL_COURSE", "count(*)", "ENROLLNO='" + Convert.ToString(txtApplication.Text) + "'")); //"incharge_mis_ghrcemp@raisoni.net";  
            if (chkStudData > 0)
            {
                ShowDetailsForAdm();
            }
            else
            {
                objCommon.DisplayMessage(updDetails, "For  Application Number :" + txtApplication.Text + " Not Cancel Any Admission Courses", this.Page);
                Div2.Visible = false;
                divAllCoursesFromHist.Visible = false;
                tblInfo.Visible = false;
                btnApprove.Visible = false;
                divSuccess.Visible = false;
                return;
            }
            btnSubmit.Visible = false;
            chkDeclaration.Visible = false;
            //}
            //else
            //{
            //    objCommon.DisplayMessage(updDetails, "Student Not Found", this.Page);
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        objsc.EnrollmentNo = txtApplication.Text;
        objsc.UA_No = Convert.ToInt32(Session["userno"]);
        if (txtAdminRemark.Text == "")
        {
            objCommon.DisplayMessage(updDetails, "Please Enter The Remark.", this.Page);
            // objsc.Remarks = null;
            return;
        }
        else
        {
            objsc.Remarks = txtAdminRemark.Text;
        }
        objsc.AdminIPAddress = Session["ipAddress"].ToString();
        //int count = Convert.ToInt32(objCommon.LookUp("ACD_USER_CANCEL_COURSE", "count(*)", "ADMIN_APPROVAL=1 and ENROLLNO=" + objsc.EnrollmentNo)); //"incharge_mis_ghrcemp@raisoni.net";  
        //if (count > 0)
        //{
        //    objCommon.DisplayMessage(updDetails, "For " + objsc.EnrollmentNo + " Application Number Courses  Cancel Allready Approved.", this.Page);
        //}
        //else
        //{
        int ret = objAcc.UpdateCancelStudent(objsc);
        if (ret == 1)
        {
            objCommon.DisplayMessage(updDetails, "Canceled Admitted Courses Approved Successfully..", this.Page);
            chkDeclaration.Enabled = false;
            txtAdminRemark.Enabled = false;
            btnApprove.Enabled = false;
            btnCancel.Enabled = false;

            /////
            //for MAIL MASSAGE
            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {
                //Get Student Details from lvStudent
                CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                // HiddenField hdfPrefe = dataitem.FindControl("hdfPrefe") as HiddenField;
                if (cbRow.Checked == true)
                {
                    string degree = string.Empty;
                    string degreeno = string.Empty;
                    string college = string.Empty;
                    string collegeno = string.Empty;
                    string OrdereID = string.Empty;
                    string OrdereIDs = string.Empty;
                    int UPDDATA = 0;
                    //string prefe = string.Empty;
                    //string prefef = string.Empty;
                    //if (cbRow.Checked == true && cbRow.Enabled == false)
                    if (cbRow.Checked == true)
                    {
                        degree += ((dataitem.FindControl("lblDegree")) as Label).ToolTip + "$";
                        college += ((dataitem.FindControl("lblCollege")) as Label).ToolTip + "$";
                        //  prefe += hdfPrefe.Value + "$";
                        OrdereID += ((dataitem.FindControl("lblOrderId")) as Label).ToolTip + "$";
                    }

                    degreeno = degree.TrimEnd('$');
                    collegeno = college.TrimEnd('$');
                    // prefef = prefe.TrimEnd('$');
                    OrdereIDs = OrdereID.TrimEnd('$');

                    UPDDATA = objAcc.UpdateCancelStudentDCR(degreeno, collegeno, OrdereIDs, lblApplication.Text);

                    string Degreename = string.Empty;
                    Degreename = objCommon.LookUp("ACD_DEGREE", "DEGREENAME", "DEGREENO IN ('" + degreeno + "')");
                    string collegeName = string.Empty;
                    collegeName = objCommon.LookUp("ACD_COLLEGE_MASTER", "COLLEGE_NAME", "COLLEGE_ID IN ('" + collegeno + "')");

                    ////////////for email //////////      
                    // string Emassage = "Application ID No." + lblApplication.Text + " / Date " + DateTime.Now.ToString("dd/MM/yyyy") + ""
                    //+ " Your request for Cancellation of Admission in " + Degreename + " For " + collegeName + " has been accepted and under process for refund. The Refund of Fees on Cancellation of Admission will be subject to the Rules of refund as per HSNC University, Mumbai. The refund amount will be credited to your Bank Account as per details submitted by you. For any queries / grievance in the matter, please contact college office For KC: 9321771619 And For HR: 9665403944";
                    string kcmob = "9321771619";
                    string hrmob = "9665403944";
                    //   string Emassage = "Application ID No." + lblApplication.Text + " / Date " + DateTime.Now.ToString("dd/MM/yyyy") + ""
                    //+ "Your request for Cancellation of Admission in " + Degreename + " For " + collegeName + " College has been accepted and under process for refund. The Refund of Fees on Cancellation of Admission will be subject to the Rules of refund as per HSNC University, Mumbai. The refund amount will be credited to your Bank Account as per details submitted by you. For any queries / grievance in the matter, please contact college office For KC: " + kcmob + " And For HR: " + hrmob + "";

                    string Emassage = "Application ID No." + lblApplication.Text + " / Date " + DateTime.Now.ToString("dd/MM/yyyy") + " Your request for Cancellation of Admission in " + Degreename + " For " + collegeName + " College has been accepted and under process for refund. The Refund of Fees on Cancellation of Admission will be subject to the Rules of refund as per HSNC University, Mumbai. The refund amount will be credited to your Bank Account as per details submitted by you. For any queries / grievance in the matter, please contact college office For KC: " + kcmob + " And For HR: " + hrmob + "";

                    string Subject = "Approved Cancellation of Admission.";

                    if (lblEmail.Text != string.Empty)
                    {
                        // this.TransferToEmail(lblEmail.Text, Emassage, Subject);
                        Execute(Emassage, lblEmail.Text, Subject).Wait();
                        objCommon.DisplayUserMessage(this.updDetails, "Email Successfully Send To Student(s)", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updDetails, "Sorry..! Didn't found Mobile no. for some Parent(s)", this.Page);
                    }

                    /////////////////for massage //////////////////////                      
                    if (txtStudMob.Text != string.Empty)
                    {
                        //this.SendSMS(txtStudMob.Text, Emassage, "1007811562228174047");

                        // string mob = "91" + txtStudMob.Text;
                        SendSMS(txtStudMob.Text, Emassage);
                        objCommon.DisplayUserMessage(updDetails, "SMS send succesfully", this.Page);
                    }
                    else
                        objCommon.DisplayMessage("Sorry..! Dont find Mobile no. for some employee", this.Page);
                }
            }

            ShowDetailsForAdm();
        }
        //  }
    }


    static async Task Execute(string Message, string toEmailId, string subjects)
    {
        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,MASTERSOFT_GRID_MAILID,MASTERSOFT_GRID_PASSWORD,MASTERSOFT_GRID_USERNAME,API_KEY_SENDGRID,CLIENT_API_KEY,FCGRIDEMAILID", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString(), "HSNCU");
            var toAddress = new MailAddress(toEmailId, "");
            var apiKey = dsconfig.Tables[0].Rows[0]["API_KEY_SENDGRID"].ToString();  //"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";
            var client = new SendGridClient(apiKey.ToString());
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString(), "HSNCU");
            var subject = subjects;// "Your OTP for Certificate Registration.";
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Admission Course Cancel", "rptCancelCourse.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            if (Convert.ToInt32(Session["usertype"]) == 2)
            {
                objsc.EnrollmentNo = Session["username"].ToString();
            }
            else
            {
                objsc.EnrollmentNo = txtApplication.Text.ToString();
                // objCommon.DisplayMessage(updDetails, "Report Not Available", this.Page);
            }

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ENROLLMENTNO=" + objsc.EnrollmentNo;

            // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updDetails, this.updDetails.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_REPORTS_StudentStrength.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

}