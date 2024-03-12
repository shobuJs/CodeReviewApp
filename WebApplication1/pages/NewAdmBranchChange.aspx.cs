//======================================================================================
// PROJECT NAME  : RFC SVCE                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : New Adm Branch Change                               
// CREATION DATE : 2/8/2019
// CREATED BY    : Pritish S                                               
// MODIFIED DATE : 
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//====================================================================================== 

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Data.SqlClient;
using System.Net;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_NewAdmBranchChange : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    //ConnectionString
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    //USED FOR INITIALSING THE MASTER PAGE
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }

    //USED FOR BYDEFAULT LOADING THE default PAGE
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

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();


                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    PopulateDropDown();
                int mul_col_flag = Convert.ToInt32(objCommon.LookUp("REFF", "MUL_COL_FLAG", "CollegeName='" + Session["coll_name"].ToString() + "'"));
                if (mul_col_flag == 0)
                {
                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");                    
                    ddlCollege.SelectedIndex = 1;
                }
                else
                {
                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    ddlCollege.SelectedIndex = 0;
                }
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                btnSubmit.Enabled = false;
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
              
            }
        }
        else
        {
            if (Request.Params["__EVENTTARGET"] != null &&
                   Request.Params["__EVENTTARGET"].ToString() != string.Empty)
            {
                if (Request.Params["__EVENTTARGET"].ToString() == "CreateDemand")
                    this.CreateDemand();
            }
        }

        divMsg.InnerHtml = string.Empty;
    }

    //used for showing Register student details
    protected void btnShow_Click(object sender, EventArgs e)
    {
        divMsg.InnerHtml = string.Empty;

        if (txtStudent.Text.Trim() == "" || txtStudent.Text.Trim() == string.Empty || txtStudent.Text == null)
        {
            lblMsg.Text = "Please Enter Proper Univ. Reg. No.";
            txtStudent.Focus();
            return;
        }

        lblMsg.Text = string.Empty;

        try
        {
            string searchBy = string.Empty;
            string searchText = txtStudent.Text.Trim();
            string errorMsg = string.Empty;
            txtRemark.Text = string.Empty;

            if (rbRegno.Checked)
            {
                searchBy = "REGNO";
                errorMsg = "Having Univ. Reg. No.: " + txtStudent.Text.Trim();
            }
            else if (rbEnrollno.Checked)
            {
                searchBy = "ENROLLNO";
                errorMsg = "Having ENROLLNO: " + searchText;
            }
            //student details shown
            ShowStudents(searchBy, searchText, errorMsg);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AdmissionCancellation.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page.z 
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=NewAdmBranchChange.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=NewAdmBranchChange.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            //objCommon.FillDropDownList(ddlDegreeReport, "ACD_DEGREE", "DISTINCT DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "DISTINCT BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BranchChange.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //used for showing student details on Controlers
    private void ShowStudentDetails(int idno)
    {
        try
        {
            StudentRegistration objSRegist = new StudentRegistration();
            DataTableReader dtr = objSRegist.GetStudentDetails(idno);

            if (dtr.Read())
            {
                lblName.Text = dtr["STUDNAME"].ToString();
                lblRegNo.Text = dtr["REGNO"].ToString();
                ViewState["lblregno"] = lblRegNo.Text;
                lblEnrollno.Text = dtr["ENROLLNO"].ToString();
                lblBranch.Text = dtr["LONGNAME"].ToString();
                lblBranch.ToolTip = dtr["BRANCHNO"].ToString();
                //imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + idno.ToString() + "&type=student";
            }
            dtr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BranchChange.ShowStudentDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //used for showing the alertt message
    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

    //used for submitting the id from student details and  branch change details
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Student objStudent = new Student();
        Common objcommon = new Common();

        int IDNO = 0;
        //int schemeno = 0;
        int i = Session["idno"].ToString()==string.Empty?0:Convert.ToInt32(Session["idno"].ToString());

        if (i>0)
        {
           //get idno from student details
            IDNO = Convert.ToInt32(objCommon.LookUp("ACD_Student", "IDNO", "IDNO=" + Session["idno"].ToString()));
            //schemeno = Convert.ToInt32(objCommon.LookUp("acd_student_result", "schemeno", "IDNO=" + IDNO));
        }

        string searchBy = string.Empty;

        if (rbRegno.Checked)
        {
            searchBy = "REGNO";
        }
        else if (rbEnrollno.Checked)
        {
            searchBy = "ENROLLNO";
        }
        //geting student id by using REGNO or ENROLLNO
        int idnobr = objcommon.LookUp("ACD_STUDENT", "IDNO", searchBy + "='" + txtStudent.Text + "'") == string.Empty ? 0 : Convert.ToInt32(objcommon.LookUp("ACD_STUDENT", "IDNO", searchBy + "='" + txtStudent.Text + "'"));
       // int chkregno = objcommon.LookUp("ACD_STUDENT", "1", "IDNO=" + idnobr) == string.Empty ? 0 : Convert.ToInt32(objcommon.LookUp("ACD_STUDENT", "1", "IDNO=" + idnobr));
        int flag = objcommon.LookUp("ACD_BRCHANGE", "IDNO", "IDNO=" + idnobr) == string.Empty ? 0 : Convert.ToInt32(objcommon.LookUp("ACD_BRCHANGE", "IDNO", "IDNO=" + idnobr));
        if (flag > 0)
        {
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append(@"if(confirm('Course registration for previous scheme of semester 3rd is already exists..Do you want to cancel?'))");
            //sb.Append(@"{__doPostBack('CreateDemand', '');}");
            //ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

            objCommon.DisplayMessage(UpdatePanel1, "Program Change Already Done for this Student !!", this.Page);
            return;
        }
        //else if (chkregno > 0)
        //{
        //    objCommon.DisplayMessage(UpdatePanel1, "Entered New Univ. Reg. No. Already Exists !!", this.Page);
        //}
        else
        {
            this.CreateDemand();
        }

    }

    //used for create new student registraion or banch change
    public void CreateDemand()
    {
        StudentController objSController = new StudentController();
        BranchController objBranch = new BranchController();
        Student objStudent = new Student();
        Common objcommon = new Common();
        int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_Student", "IDNO", "IDNO=" + Session["idno"].ToString()));
        //string NewRegNo = ViewState["newregno"].ToString();
        string NewRegNo = Session["regno"].ToString();
        string NewEnrollNo = Session["newenrollno"].ToString();

        StudentRegistration objSRegist = new StudentRegistration();
        //get student details and bind on lable
        DataTableReader dtr = objSRegist.GetStudentDetails(IDNO);

        if (dtr.Read())
        {
            lblName.Text = dtr["STUDNAME"].ToString();
            lblRegNo.Text = dtr["REGNO"].ToString();
            lblEnrollno.Text = dtr["ENROLLNO"].ToString();
            lblBranch.Text = dtr["LONGNAME"].ToString();
            lblBranch.ToolTip = dtr["BRANCHNO"].ToString();
        }
        dtr.Close();

        string searchBy = string.Empty;

        if (rbRegno.Checked)
        {
            searchBy = "REGNO";
        }
        else if (rbEnrollno.Checked)
        {
            searchBy = "ENROLLNO";
        }
      
        objStudent.IdNo = Convert.ToInt32(IDNO);
        objStudent.BranchNo = Convert.ToInt32(lblBranch.ToolTip); // old branch no
        objStudent.StudName = lblName.Text;
        objStudent.NewBranchNo = Convert.ToInt32(ddlBranch.SelectedValue); // new branch no
        //int schemeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "schemeno", "IDNO=" + IDNO));
        objStudent.Remark = txtRemark.Text.Trim();
        objStudent.CollegeCode = Session["colcode"].ToString();
        objStudent.RegNo = objCommon.LookUp("ACD_STUDENT","REGNO",searchBy+"='"+txtStudent.Text.Trim()+"'");
        int userno = Convert.ToInt32(Session["userno"]);
        string hostName = Dns.GetHostName();
        string ipaddress = Dns.GetHostByName(hostName).AddressList[0].ToString();
        objStudent.EnrollNo = objCommon.LookUp("ACD_STUDENT","ENROLLNO",searchBy+"='"+txtStudent.Text.Trim()+"'");
        objStudent.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
        //methos for change branching
        CustomStatus cs = (CustomStatus)objBranch.ChangeBranch_NewStudent(objStudent, userno, ipaddress, NewEnrollNo);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(UpdatePanel1, "Program Change Process Completed Successfully !!", this.Page);
           // lblMsg.Text = "Branch Change Process Completed Successfully !!";
            divdata.Visible = false;
            ClearControls();

        }
        else if (cs.Equals(CustomStatus.RecordExist))
        {
            objCommon.DisplayMessage(UpdatePanel1, "Student with new generated Univ. Reg. No. already exist.", this.Page);
            lblMsg.Text = "Student with new generated Univ. Reg. No. already exist.";
            divdata.Visible = false;
            ClearControls();
        }
        else
        {
            lblMsg.Text = "Error...";
            divdata.Visible = false;
        }
    }

    //reset all controllers
    private void ClearControls()
    {
        lblName.Text = string.Empty;
        lblRegNo.Text = string.Empty;
        lblEnrollno.Text = string.Empty;
        lblBranch.Text = string.Empty;
        txtStudent.Text = string.Empty;
        txtNewRegNo.Text = string.Empty;
        ddlBranch.SelectedIndex = 0;
        txtRemark.Text = string.Empty;
    }

    //refresh or reload current page 
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    //on selection of Branch name bind  NewEnrollNo ,NewRegNo
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        int branchNo = Convert.ToInt32(ddlBranch.SelectedValue);

        StudentController objsc = new StudentController();

        string searchBy = string.Empty;

        if (rbRegno.Checked)
        {
            searchBy = "REGNO";
        }
        else if (rbEnrollno.Checked)
        {
            searchBy = "ENROLLNO";
        }
        //get REGNO or ENROLLNO from ACD_STUDENT table
        string regno = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", searchBy + "='" + txtStudent.Text.Trim() + "'");
        string NewEnrollNo = string.Empty;
        string NewRegNo = string.Empty;
        //get REGNO on selection of criteria
        DataTableReader dtr = objsc.GetRegNoForBranchChange(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ViewState["batchNo"]), regno);
        if (dtr.Read())
        {
            NewEnrollNo = dtr["NEWENROLLNO"].ToString();
            NewRegNo = dtr["NEWREGNO"].ToString();
        }

        //txtNewRegNo.Text=NewRegNo.ToString();

        Session["newenrollno"] = NewEnrollNo.ToString();
        ViewState["newregno"] = txtNewRegNo.Text;

        if (ddlBranch.SelectedValue == "0")
        {
            txtNewRegNo.Text = string.Empty;
        }
    }

    protected void rblSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //
    }

    //show student details
    private void ShowStudents(string searchBy, string searchText, string errorMsg)
    {
        divMsg.InnerHtml = string.Empty;
        if (txtStudent.Text.Trim() == "" || txtStudent.Text.Trim() == string.Empty || txtStudent.Text == null)
        {
            lblMsg.Text = "Please Enter Univ. Reg. No./ Admission No.";
            txtStudent.Focus();
            return;
        }
        lblMsg.Text = string.Empty;

        try
        {
            StudentRegistration objSRegist = new StudentRegistration();
            StudentController objSC = new StudentController();
            string idno = "0";
            string category = "";
            string admcan = "0";
            //bind IDNO for entering ENROLLEMENT NO
            idno = Convert.ToString(objCommon.LookUp("ACD_STUDENT", "IDNO", "" + searchBy + "='" + txtStudent.Text + "' AND ISNULL(CAN,0)=" + 0 + " AND ISNULL(ADMCAN,0)=" + 0));

            if (idno == string.Empty)
            {
                btnSubmit.Enabled = false;
                divdata.Visible = false;
                objCommon.DisplayMessage(UpdatePanel1, "No Student Found " + errorMsg, this.Page);
            }
            else
            {

                if (txtStudent.Text.Trim() != string.Empty)
                {
                    admcan = objCommon.LookUp("ACD_STUDENT", "ISNULL(ADMCAN,0)", searchBy + "='" + txtStudent.Text.Trim() + "'");

                    if (admcan == "True")
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Entered student admission has been cancelled!", this.Page);
                        txtStudent.Text = "";
                        txtStudent.Focus();
                        return;
                    }

                    if (txtStudent.Text.Contains("("))
                    {
                        if (txtStudent.Text.Contains("["))
                        {
                            char[] ct = { '(' };
                            string[] cat = txtStudent.Text.Trim().Split(ct);
                            category = cat[1].Replace(")", "");
                            cat = category.Split('[');
                            category = cat[0].Replace("]", "");
                            char[] sp = { '[' };
                            string[] data = txtStudent.Text.Trim().Split(sp);
                            idno = data[1].Replace("]", "");
                            ViewState["idno"] = Convert.ToInt32(idno);
                        }
                    }
                }

                if (idno != string.Empty)
                {
                    //get dregree no for entering REGNO or ENROLLMENT NO
                    string degreeNo = objCommon.LookUp("acd_student", "degreeno", searchBy + "='" + txtStudent.Text.Trim() + "'");
                    ViewState["idno"] = idno.ToString();
                 //get student details for perticular student id and bind all the details on controllers
                    DataTableReader dtr = objSRegist.GetStudentDetails(Convert.ToInt32(idno));

                    if (dtr.Read())
                    {
                        Session["idno"] = dtr["idno"].ToString();
                        lblName.Text = dtr["STUDNAME"].ToString();
                        lblRegNo.Text = dtr["REGNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["REGNO"].ToString();
                        Session["regno"] = lblRegNo.Text;
                        lblRegNo.ToolTip = dtr["ROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ROLLNO"].ToString();
                        lblEnrollno.Text = dtr["ENROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ENROLLNO"].ToString();
                        lblEnrollno.ToolTip = dtr["ENROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ENROLLNO"].ToString(); ;
                        lblBranch.Text = dtr["LONGNAME"].ToString();
                        lblBranch.ToolTip = dtr["BRANCHNO"].ToString();
                        ViewState["semesterNo"] = dtr["SEMESTERNO"].ToString();
                        ViewState["degreeNo"] = dtr["DEGREENO"].ToString();
                        ViewState["batchNo"] = dtr["ADMBATCH"].ToString();
                        //if (Convert.ToInt32(dtr["SEMESTERNO"]) != 3)
                        //{
                        //    objCommon.DisplayMessage(UpdatePanel1, "The change of branch can be applied in 2nd year i.e 3rd semester!", this.Page);
                        //}
                        //get reg no from ACD_STUDENT
                        string reg=objCommon.LookUp("ACD_STUDENT","REGNO","ENROLLNO='"+lblEnrollno.Text+"'")==string.Empty?"0":objCommon.LookUp("ACD_STUDENT","REGNO","ENROLLNO='"+lblEnrollno.Text+"'");
                        if (reg != "0")
                        {
                            objCommon.DisplayMessage(UpdatePanel1, "Program change cannot be performed for this student !!", this.Page);
                            divdata.Visible = false;
                            return;
                        }
                        else
                        {
                            btnSubmit.Enabled = true;
                            divdata.Visible = true;
                        }
                        //get PROGRAM NO from ACD_DEGREE
                        int programno = objCommon.LookUp("ACD_DEGREE", "DISTINCT PROGRAMNO", "DEGREENO=" + Convert.ToInt32(dtr["DEGREENO"])) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "DISTINCT PROGRAMNO", "DEGREENO=" + Convert.ToInt32(dtr["DEGREENO"])));
                        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "PROGRAMNO=" + Convert.ToInt32(programno), "DEGREENO");                        

                        //if (Convert.ToInt32(dtr["DEGREENO"]) == 1 || Convert.ToInt32(dtr["DEGREENO"]) == 2)
                        //{
                        //    string degreeno = "1,2";
                        //    objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=DB.BRANCHNO) INNER JOIN ACD_DEGREE D ON(D.DEGREENO=DB.DEGREENO)", "B.BRANCHNO", "(D.DEGREENAME+' - '+B.LONGNAME) AS LONGNAME", "DB.COLLEGE_ID=" + Convert.ToInt32(dtr["COLLEGE_ID"]) + " AND DB.DEGREENO IN(" + degreeno + ") AND DB.BRANCHNO<>" + Convert.ToInt32(dtr["BRANCHNO"]), "LONGNAME");
                        //}
                        //else
                        //{
                        //    objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=DB.BRANCHNO) INNER JOIN ACD_DEGREE D ON(D.DEGREENO=DB.DEGREENO)", "B.BRANCHNO", "(D.DEGREENAME+' - '+B.LONGNAME) AS LONGNAME", "DB.COLLEGE_ID=" + Convert.ToInt32(dtr["COLLEGE_ID"]) + " AND DB.DEGREENO=" + Convert.ToInt32(dtr["DEGREENO"]) + " AND DB.BRANCHNO<>" + Convert.ToInt32(dtr["BRANCHNO"]), "LONGNAME");
                        //}
                    }
                    else
                    {
                        btnSubmit.Enabled = false;
                    }
                    dtr.Close();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BranchChange.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    ////On select of Degree bind Branch name in drop down list
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=DB.BRANCHNO) INNER JOIN ACD_DEGREE D ON(D.DEGREENO=DB.DEGREENO)", "B.BRANCHNO", "(D.DEGREENAME+' - '+B.LONGNAME) AS LONGNAME", "DB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            ShowReport("ProgramChangeReport", "rptBranchChangeDetails.rpt", 1);
        }
        else
        {
            objCommon.DisplayMessage("Please Select College/School Name",this.Page);
        }
    }

    protected void btnCancelReport_Click(object sender, EventArgs e)
    {
        ddlDegreeReport.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        txtToDate.Text = string.Empty;
        txtFromDate.Text = string.Empty;
    }

    private void ShowReport(string reportTitle, string rptFileName, int reportno)
    {
        try
        {
            string StartEndDate = hdnDate.Value;
            string[] dates = new string[] { };
            if ((StartEndDate) == "")//GetDocs()
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please select Start Date End Date !", this.Page);
                return;
            }
            else
            {
                StartEndDate = StartEndDate.Substring(0, StartEndDate.Length - 0);
                //string[]
                dates = StartEndDate.Split('-');
            }
            string StartDate = dates[0];//Jul 15, 2021
            string EndDate = dates[1];
            //DateTime dateTime10 = Convert.ToDateTime(a);
            DateTime dtStartDate = DateTime.Parse(StartDate);
            string SDate = dtStartDate.ToString("yyyy/MM/dd");
            DateTime dtEndDate = DateTime.Parse(EndDate);
            string EDate = dtEndDate.ToString("yyyy/MM/dd");

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            if (reportno == 1)
            {
                //if (txtFromDate.Text != "")
                //{
                //    DateTime fromdatetime = Convert.ToDateTime(txtFromDate.Text);
                //    fromdate = fromdatetime.ToString("yyyy-MM-dd");
                //}

                //if (txtToDate.Text != "")
                //{
                //    DateTime todatetime = Convert.ToDateTime(txtToDate.Text);
                //    todate = todatetime.ToString("yyyy-MM-dd");
                //}

                url += "&param=@P_DEGREENO=" + Convert.ToInt32(ddlDegreeReport.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_FROMDATE=" + SDate + ",@P_TODATE=" + EDate;
            }        

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            // objCommon.FillDropDownList(ddlDegreeReport, "ACD_DEGREE ", "DISTINCT DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
            objCommon.FillDropDownList(ddlDegreeReport, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID =" + ddlCollege.SelectedValue, "D.DEGREENO");
            ddlDegreeReport.Focus();
        }
        else
            ddlDegreeReport.SelectedIndex = 0;
    }
}
