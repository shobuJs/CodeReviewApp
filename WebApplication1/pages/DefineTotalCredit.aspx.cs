//======================================================================================
// PROJECT NAME  : RFCAMPUS-SVCE
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Rule Book[Credits Defination]                                   
// MODIFIED BY   : RAJU BITODE
// MODIFIED DESC : Define Rule Book for CBCS Course Registration  
// MODIFIED DATE : 21-May-2019                                                
//======================================================================================
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;



public partial class ACADEMIC_MASTERS_DefineTotalCredit : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    DefineTotalCreditController objDefineTotalCredit = new DefineTotalCreditController();
    DefineCreditLimit dcl = new DefineCreditLimit();

    #region Page Load
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

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
                ////Page Authorization
                this.CheckPageAuthorization();

                //Check for Activity On/Off
                //CheckActivity();
                ViewState["edit"] = "submit";
                ViewState["recordNo"] = null;
                btnSubmit.Text = "Submit";
                LabelCredit.Text="Min Regular Credit limit";

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                this.PopulateDropDownList();
                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                string IPADDRESS = string.Empty;

                IPADDRESS = ip.AddressList[1].ToString();
                //ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                ViewState["ipAddress"] = IPADDRESS;
                //check activity for course registration.

                objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0 AND COLLEGE_ID IN (" + Session["college_nos"].ToString() + ")", "COLLEGE_ID");
                objCommon.FillDropDownList(ddlAdmissionType, "ACD_MODULE_REGISTRATION_TYPE", "MODULE_REGISTRATION_NO", "MODULE_REGISTRATION_TYPE", "", "MODULE_REGISTRATION_NO");
                
                showDetails();
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
            }
        }

        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DefineTotalCredit.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DefineTotalCredit.aspx");
        }
    }

    private void PopulateDropDownList()
    {
      //  objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
     //   objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");
        fillChecboxlistOfterm();
    }

    public void fillChecboxlistOfterm()
    {
        DataSet dsSemester = objCommon.FillDropDown("ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
        if (dsSemester != null && dsSemester.Tables.Count > 0)
        {
            if (dsSemester.Tables[0].Rows.Count > 0)
            {
                chkListTerm.DataTextField = "SEMESTERNAME";
                chkListTerm.DataValueField = "SEMESTERNO";
                chkListTerm.DataSource = dsSemester.Tables[0];
                chkListTerm.DataBind();
                chkListTerm.Visible = true;
            }
            else
            {
                chkListTerm.DataSource = null;
                chkListTerm.DataBind();
                chkListTerm.Visible = false;
            }
        }
    }

    void showDetails()
    {
        DataSet dsCredit = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT C INNER JOIN ACD_DEGREE D ON(C.DEGREENO=D.DEGREENO) INNER JOIN ACD_MODULE_REGISTRATION_TYPE R ON (C.ADM_TYPE=R.MODULE_REGISTRATION_NO)", "MODULE_REG_TYPE,R.MODULE_REGISTRATION_TYPE,D.DEGREENAME,idno,SESSIONNO,SESSIONNAME,FROM_CREDIT,TO_CREDIT,SESSIONTYPE,STUDENT_TYPE,FROM_CGPA,TO_CGPA,ADDITIONAL_COURSE", "(CASE  WHEN STUDENT_TYPE=1 THEN 'All Pass' ELSE 'Backlog' END)AS STUDENT_TYPE_NAME,(CASE  WHEN ADDITIONAL_COURSE=1 THEN 'YES' ELSE 'NO' END)AS ADDITIONAL_COURSE_NAME,(CASE  WHEN MIN_SCHEME_LIMIT=1 THEN 'YES' ELSE 'NO' END)AS MIN_SCHEME_LIMIT,(CASE  WHEN MAX_SCHEME_LIMIT=1 THEN 'YES' ELSE 'NO' END)AS MAX_SCHEME_LIMIT,(CASE  WHEN DEGREE_TYPE=1 THEN 'UG' ELSE '' END)AS DEGREE_TYPE,SEMESTER_TEXT,(CASE  WHEN isnull(LOCK,0)=1 THEN 'Yes' else 'No' END)AS LOCK,MIN_REG_CREDIT_LIMIT,(CASE  WHEN ADM_TYPE=1 THEN 'Regular' ELSE 'Direct Second Year' END)AS ADM_TYPE", "", "IDNO DESC");
        lvCredit.DataSource = dsCredit.Tables[0];
        lvCredit.DataBind();
    }
    #endregion Page Load

    #region Filter
    //protected void chkMinimumSchemeLimit_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkMinimumSchemeLimit.Checked)
    //    {
    //        txtFromCredit.Text = "0";
    //        txtFromCredit.Enabled = false;
    //    }
    //    else
    //    {
    //        txtFromCredit.Text = "0";
    //        txtFromCredit.Enabled = true;
    //    }
    //}
    //protected void chkMaximumSchemeLimit_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkMaximumSchemeLimit.Checked)
    //    {
    //        txtToCredits.Text = "0";
    //        txtToCredits.Enabled = false;
    //    }
    //    else
    //    {
    //        txtToCredits.Text = "0";
    //        txtToCredits.Enabled = true;
    //    }
    //}
    protected void chkCreditStatus_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCreditStatus.Checked)
        {
            txtFromCredit.Text = "";
            txtToCredits.Text = "";
            txtFromCredit.Enabled = true;
            txtToCredits.Enabled = true;
        }
        else
        {
            txtFromCredit.Text = "0";
            txtToCredits.Text = "0";
            txtFromCredit.Enabled = false;
            txtToCredits.Enabled = false;
        }
    }
    protected void chkCGPAStatus_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCGPAStatus.Checked)
        {
            txtFromRange.Text = "";
            txtToRange.Text = "";
            txtFromRange.Enabled = true;
            txtToRange.Enabled = true;
        }
        else
        {
            txtFromRange.Text = "0";
            txtToRange.Text = "0";
            txtFromRange.Enabled = false;
            txtToRange.Enabled = false;
        }

    }
    protected void chkAddionalCourse_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAddionalCourse.Checked)
        {
            trAdditionalCourses.Visible = true;
        }
        else
        {
            trAdditionalCourses.Visible = false;
            ddlAdditionalCourseDegree.SelectedIndex = 0;
        }
    }
    #endregion Filter

    #region Click Event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //if (chkMinimumSchemeLimit.Checked == false)
            //{
            //    if (txtFromCredit.Text == string.Empty)
            //    {
            //        objCommon.DisplayMessage("Enter Minimum Credit Limit", this.Page);
            //        return;
            //    }
            //}

            //if (chkMaximumSchemeLimit.Checked == false)
            //{
            //    if (txtToCredits.Text == string.Empty)
            //    {
            //        objCommon.DisplayMessage("Enter Maximum Credit Limit", this.Page);
            //        return;
            //    }
            //}

            //if (Convert.ToDouble(txtFromRange.Text) > Convert.ToDouble(txtToRange.Text))
            //{
            //    objCommon.DisplayMessage("To CGPA Should be greater than From CGPA", this.Page);
            //    return;
            //}

            //if (ddlAdmissionType.SelectedValue == "1")
            //{
            //    if (Convert.ToDouble(txtFromRange.Text) >= Convert.ToDouble(txtToRange.Text))
            //    {
            //        objCommon.DisplayMessage("To CGPA Should be greater than From CGPA", this.Page);
            //        return;
            //    }
            //}

            int count = 0;

            foreach (ListItem item in chkListTerm.Items)
            {
                if (item.Selected)
                {
                    count = count + 1;
                    if (count == 1)
                    {
                        dcl.Semester = item.Value;
                        dcl.Semester_Text = item.Text;
                    }
                    else
                    {
                        dcl.Semester = dcl.Semester + "," + item.Value;
                        dcl.Semester_Text = dcl.Semester_Text + "," + item.Text;
                    }
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage("Please Select Atleast One Semester", this.Page);
                return;
            }

            int addtionalCourse = 0;
            int minimumSchemeLimit = 0;
            int maximumSchemeLimit = 0;

            if (chkMinimumSchemeLimit.Checked)
            {
                minimumSchemeLimit = 1;
                //txtFromCredit.Text = "0";
            }

            if (chkMaximumSchemeLimit.Checked)
            {
                maximumSchemeLimit = 1;
                //txtToCredits.Text = "0";
            }

            if (chkAddionalCourse.Checked)
                addtionalCourse = 1;

            dcl.CreditStatus = (chkCreditStatus.Checked == true ? 1 : 0);
            dcl.FROM_CREDIT = (chkCreditStatus.Checked == true ? Convert.ToInt32(txtFromCredit.Text.Trim()) : 0);
            dcl.TO_CREDIT = (chkCreditStatus.Checked == true ? Convert.ToInt32(txtToCredits.Text.Trim()) : 0);

            dcl.CgpaStatus = (chkCGPAStatus.Checked == true ? 1 : 0);
            dcl.FROM_CGPA = (chkCGPAStatus.Checked == true ? Convert.ToDouble(txtFromCredit.Text.Trim()) : 0);
            dcl.TO_CGPA = (chkCGPAStatus.Checked == true ? Convert.ToDouble(txtToCredits.Text.Trim()) : 0);

            dcl.SESSION = Convert.ToInt32(ddlSession.SelectedValue);
            dcl.DEGREENO = Convert.ToInt32(ddlDegree.SelectedValue);
            dcl.SESSIONNAME = ddlSession.SelectedItem.Text;
            dcl.STUDENT_TYPE = Convert.ToInt32(ddlStudentType.SelectedValue);
            //dcl.FROM_CGPA = Convert.ToDouble(txtFromRange.Text);
            //dcl.TO_CGPA = Convert.ToDouble(txtToRange.Text);
            //dcl.TO_CREDIT = Convert.ToInt32(txtToCredits.Text);
            //dcl.FROM_CREDIT = Convert.ToInt32(txtFromCredit.Text);
            dcl.ADM_TYPE = Convert.ToInt32(ddladm.SelectedValue);
            dcl.ADDITIONAL_COURSE = addtionalCourse;
            dcl.DEGREE_TYPE = Convert.ToInt32(ddlAdditionalCourseDegree.SelectedValue);
            dcl.MIN_SCHEMELIMIT = minimumSchemeLimit;
            dcl.MAX_SCHEMELIMIT = maximumSchemeLimit;
            dcl.MIN_REG_CREDIT_LIMIT = Convert.ToInt32(txtMinRegCredit.Text);

            dcl.ElectGrp1 = (txtElect1.Text == string.Empty ? 0 : Convert.ToInt32(txtElect1.Text.Trim()));
            dcl.ElectGrp2 = (txtElect2.Text == string.Empty ? 0 : Convert.ToInt32(txtElect2.Text.Trim()));
            dcl.ElectGrp3 = (txtElect3.Text == string.Empty ? 0 : Convert.ToInt32(txtElect3.Text.Trim()));
            dcl.ElectGrp4 = (txtElect4.Text == string.Empty ? 0 : Convert.ToInt32(txtElect4.Text.Trim()));
            dcl.ElectGrp5 = (txtElect5.Text == string.Empty ? 0 : Convert.ToInt32(txtElect5.Text.Trim()));
            dcl.ElectGrp6 = (txtElect6.Text == string.Empty ? 0 : Convert.ToInt32(txtElect6.Text.Trim()));
            dcl.ElectGrp7 = (txtElect7.Text == string.Empty ? 0 : Convert.ToInt32(txtElect7.Text.Trim()));
            dcl.ElectGrp8 = (txtElect8.Text == string.Empty ? 0 : Convert.ToInt32(txtElect8.Text.Trim()));
            dcl.ElectGrp9 = (txtElect9.Text == string.Empty ? 0 : Convert.ToInt32(txtElect9.Text.Trim()));
            dcl.ElectGrp10 = (txtElect10.Text == string.Empty ? 0 : Convert.ToInt32(txtElect10.Text.Trim()));
            dcl.ElectGrp11 = (txtElect11.Text == string.Empty ? 0 : Convert.ToInt32(txtElect11.Text.Trim()));
            dcl.ElectGrp12 = (txtElect12.Text == string.Empty ? 0 : Convert.ToInt32(txtElect12.Text.Trim()));
            dcl.ElectGrp13 = (txtElect13.Text == string.Empty ? 0 : Convert.ToInt32(txtElect13.Text.Trim()));
            dcl.ElectGrp14 = (txtElect14.Text == string.Empty ? 0 : Convert.ToInt32(txtElect14.Text.Trim()));
            dcl.ElectGrp15 = (txtElect15.Text == string.Empty ? 0 : Convert.ToInt32(txtElect15.Text.Trim()));
            dcl.MovableSub = (txtMovableSubject.Text == string.Empty ? 0 : Convert.ToInt32(txtMovableSubject.Text.Trim()));

            if (ViewState["edit"].ToString() == "submit")
            {
                int ret = objDefineTotalCredit.AddCredit(dcl, Convert.ToInt32(ddlCollege.SelectedValue),Convert.ToInt32(ddlAdmissionType.SelectedValue));
                if (ret > 0)
                {
                    objCommon.DisplayMessage("Saved Successfully !!", this.Page);
                    showDetails();
                    btnCancel_Click(btnCancel, e);
                }
            }
            else
            {
                dcl.RECORDNO = Convert.ToInt32(ViewState["recordNo"]);
                int ret = objDefineTotalCredit.UpdateCredit(dcl, Convert.ToInt32(ddlCollege.SelectedValue),Convert.ToInt32(ddlAdmissionType.SelectedValue));
                objCommon.DisplayMessage("Updated Successfully !!", this.Page);

                showDetails();
                btnCancel_Click(btnCancel, e);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_DefineTotalCredit.btnSubmit_CLick --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
        ddlStudentType.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        txtToRange.Text = string.Empty;
        txtFromRange.Text = string.Empty;
        chkAddionalCourse.Checked = false;
        chkMinimumSchemeLimit.Checked = false;
        chkMaximumSchemeLimit.Checked = false;

        chkCreditStatus.Checked = false;
        chkCGPAStatus.Checked = false;

        txtToCredits.Enabled = false;
        txtFromCredit.Enabled = false;

        txtFromRange.Enabled = false;
        txtToRange.Enabled = false;

        txtMinRegCredit.Text = string.Empty;
        txtToCredits.Text = string.Empty;
        txtFromCredit.Text = string.Empty;
        ViewState["edit"] = "submit";
        btnSubmit.Text = "Submit";
        ViewState["recordNo"] = null;

        ddlAdmissionType.SelectedIndex = 0;
        ddlAdditionalCourseDegree.SelectedIndex = 0;
        trAdditionalCourses.Visible = false;

        txtElect1.Text = string.Empty;
        txtElect2.Text = string.Empty;
        txtElect3.Text = string.Empty;
        txtElect4.Text = string.Empty;
        txtElect5.Text = string.Empty;
        txtElect6.Text = string.Empty;
        txtElect7.Text = string.Empty;
        txtElect8.Text = string.Empty;
        txtElect9.Text = string.Empty;
        txtElect10.Text = string.Empty;
        txtElect11.Text = string.Empty;
        txtElect12.Text = string.Empty;
        txtElect13.Text = string.Empty;
        txtElect14.Text = string.Empty;
        txtElect15.Text = string.Empty;
        txtMovableSubject.Text = string.Empty;

        foreach (ListItem i in chkListTerm.Items)
        {
            i.Selected = false;
        }

        LabelCredit.Text = "Min Regular Credit limit";
        ddladm.SelectedIndex = 0;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton editButton = sender as ImageButton;
            int idno = Int32.Parse(editButton.CommandArgument);

            ViewState["edit"] = "edit";
            btnSubmit.Text = "Update";
            ViewState["recordNo"] = idno;
            DataSet ds = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT", "*", "", "IDNO='" + idno + "'", "IDNO DESC");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["LOCK"].ToString() == "1")
                {
                    objCommon.DisplayMessage("Credit Defination is Locked ,Please Contact Administrator.", this.Page);
                    showDetails();
                    btnCancel_Click(btnCancel, e);
                    return;
                }
                txtFromCredit.Enabled = true;
                txtToCredits.Enabled = true;
                txtFromRange.Enabled = true;
                txtToRange.Enabled = true;
                if ( ds.Tables[0].Rows[0]["ADM_TYPE"].ToString()== "1")
                {
                    LabelCredit.Text = "Rupees Per Subject";
                }
                if (ds.Tables[0].Rows[0]["ADM_TYPE"].ToString() == "2")
                {
                    LabelCredit.Text = "Rupees Per Credit";
                }
                if (ds.Tables[0].Rows[0]["ADM_TYPE"].ToString() == "3")
                {
                    LabelCredit.Text = "Rupees Per Module";
                }
                lbcredit.Visible = true;
                txtFromCredit.Text = ds.Tables[0].Rows[0]["FROM_CREDIT"].ToString();
                txtToCredits.Text = ds.Tables[0].Rows[0]["TO_CREDIT"].ToString();
                ddlStudentType.SelectedValue = ds.Tables[0].Rows[0]["STUDENT_TYPE"].ToString();
                txtFromRange.Text = ds.Tables[0].Rows[0]["FROM_CGPA"].ToString();
                txtToRange.Text = ds.Tables[0].Rows[0]["TO_CGPA"].ToString();
                ddladm.SelectedValue = ds.Tables[0].Rows[0]["ADM_TYPE"].ToString();
                ddlAdditionalCourseDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREE_TYPE"].ToString();
                ddlSession.SelectedValue = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
                ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();

                txtMinRegCredit.Text = ds.Tables[0].Rows[0]["MIN_REG_CREDIT_LIMIT"].ToString();
                txtMovableSubject.Text = ds.Tables[0].Rows[0]["MovableSub"].ToString();
                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                ddlAdmissionType.SelectedValue = ds.Tables[0].Rows[0]["MODULE_REG_TYPE"].ToString();

                if (ds.Tables[0].Rows[0]["ADDITIONAL_COURSE"].ToString() == "1")
                {
                    chkAddionalCourse.Checked = true;
                    trAdditionalCourses.Visible = true;
                }
                else
                {
                    chkAddionalCourse.Checked = false;
                    trAdditionalCourses.Visible = false;
                    ddlAdditionalCourseDegree.SelectedIndex = 0;
                }

                if (ds.Tables[0].Rows[0]["MIN_SCHEME_LIMIT"].ToString() == "1")
                {
                    chkMinimumSchemeLimit.Checked = true;
                    //txtFromCredit.Enabled = false;
                }
                else
                {
                    chkMinimumSchemeLimit.Checked = false;
                    //txtFromCredit.Enabled = true;
                }
         

                if (ds.Tables[0].Rows[0]["MAX_SCHEME_LIMIT"].ToString() == "1")
                {
                    chkMaximumSchemeLimit.Checked = true;
                    //txtToCredits.Enabled = false;
                }
                else
                {
                    chkMaximumSchemeLimit.Checked = false;
                    //txtToCredits.Enabled = true;
                }
                chkCreditStatus.Checked = (ds.Tables[0].Rows[0]["CREDIT_LIMIT_STATUS"].ToString() == "1" ? true : false);
                chkCGPAStatus.Checked = (ds.Tables[0].Rows[0]["CGPA_STATUS"].ToString() == "1" ? true : false);

                txtElect1.Text = ds.Tables[0].Rows[0]["ElectGrp1"].ToString();
                txtElect2.Text = ds.Tables[0].Rows[0]["ElectGrp2"].ToString();
                txtElect3.Text = ds.Tables[0].Rows[0]["ElectGrp3"].ToString();
                txtElect4.Text = ds.Tables[0].Rows[0]["ElectGrp4"].ToString();
                txtElect5.Text = ds.Tables[0].Rows[0]["ElectGrp5"].ToString();
                txtElect6.Text = ds.Tables[0].Rows[0]["ElectGrp6"].ToString();
                txtElect7.Text = ds.Tables[0].Rows[0]["ElectGrp7"].ToString();
                txtElect8.Text = ds.Tables[0].Rows[0]["ElectGrp8"].ToString();
                txtElect9.Text = ds.Tables[0].Rows[0]["ElectGrp9"].ToString();
                txtElect10.Text = ds.Tables[0].Rows[0]["ElectGrp10"].ToString();

                txtElect11.Text = ds.Tables[0].Rows[0]["ElectGrp11"].ToString();
                txtElect12.Text = ds.Tables[0].Rows[0]["ElectGrp12"].ToString();
                txtElect13.Text = ds.Tables[0].Rows[0]["ElectGrp13"].ToString();
                txtElect14.Text = ds.Tables[0].Rows[0]["ElectGrp14"].ToString();
                txtElect15.Text = ds.Tables[0].Rows[0]["ElectGrp15"].ToString();

                string[] strings = ds.Tables[0].Rows[0]["SEMESTER"].ToString().Split(',');

                for (int i = 0; i < strings.Count(); i++)
                {
                    foreach (ListItem item in chkListTerm.Items)
                    {
                        item.Selected = false;
                    }
                }

                for (int i = 0; i < strings.Count(); i++)
                {
                    foreach (ListItem item in chkListTerm.Items)
                    {
                        if (item.Value == strings[i])
                        {
                            item.Selected = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "DefineCounter.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        try
        {
            int ret = objDefineTotalCredit.LockCreditDefination();
            if (ret > 0)
            {
                objCommon.DisplayMessage("Locked Successfully !!", this.Page);
                showDetails();
                btnCancel_Click(btnCancel, e);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_DefineTotalCredit.btnSubmit_CLick --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion Click Event
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND COLLEGE_ID="+ddlCollege.SelectedValue, "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE CD ON (D.DEGREENO=CD.DEGREENO)", "DISTINCT CD.DEGREENO", "D.DEGREENAME", "D.DEGREENO>0 AND CD.COLLEGE_ID="+ddlCollege.SelectedValue, "CD.DEGREENO"); 
            ddlSession.Focus();
        }
        else
        {
            objCommon.DisplayMessage("Please Select Faculty/School", this.Page);
            ddlCollege.Focus();
            ddlSession.SelectedIndex = 0;
        }
    }
    protected void ddlAdmissionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbcredit.Visible = true;
      
        if (ddlAdmissionType.SelectedValue == "1")
        {
           
           LabelCredit.Visible = true;
           LabelCredit.Text = "Rupees Per Subject";           
        }
        if (ddlAdmissionType.SelectedValue == "2")
        {

            LabelCredit.Visible = true;
            LabelCredit.Text = "Rupees Per Credit";
        }
        if (ddlAdmissionType.SelectedValue == "3")
        {
            LabelCredit.Visible = true;
            LabelCredit.Text = "Rupees Per Module";
        }
       
    }
}
