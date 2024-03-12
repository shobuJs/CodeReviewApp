//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : COURSE REGISTRATION BY ADMIN                                    
// CREATION DATE : 24-JUNE-2015
// ADDED BY      : MR. MANISH WALDE
// ADDED DATE    : 
// MODIFIED BY   : PRITY KHANDAIT
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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net;

public partial class ACADEMIC_CourseRegistrationByAdmin : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();

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

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                this.PopulateDropDownList();
                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                string IPADDRESS = string.Empty;

                IPADDRESS = ip.AddressList[0].ToString();
                ViewState["ipAddress"] = IPADDRESS;

                ViewState["action"] = "add";
                ViewState["idno"] = "0";
                if (Session["usertype"].ToString().Equals("8") || Session["usertype"].ToString().Equals("1") || Session["usertype"].ToString().Equals("12"))   //Only Admin & HoD Added on 25/07/2017
                {
                    divOptions.Visible = false;
                    divCourses.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage("You are Not Authorized to View this Page. Contact Admin.", this.Page);
                }
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
                Response.Redirect("~/notauthorized.aspx?page=CourseRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CourseRegistration.aspx");
        }
    }

    private bool CheckActivity()
    {
        bool ret = true;
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage(updReg, "This Activity has been Stopped. Contact Admin.!!", this.Page);
                ret = false;
            }

            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage(updReg, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                ret = false;
            }
        }
        else
        {
            objCommon.DisplayMessage(updReg, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            ret = false;
        }
        dtr.Close();
        return ret;
    }
    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int flag = 0;
            StudentRegist objSR = new StudentRegist();

            objSR.REGISTERED = 1;

            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {
                if ((dataitem.FindControl("chkRegister") as CheckBox).Checked == true)
                    objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + ",";
            }
            foreach (ListViewDataItem dataitem in lvBacklogSubjects.Items)
            {
                if ((dataitem.FindControl("chkRegister") as CheckBox).Checked == true)
                    objSR.Backlog_course = objSR.Backlog_course + (dataitem.FindControl("lblCCode") as Label).ToolTip + ",";
            }
            if (objSR.COURSENOS == null)
            {
                objSR.COURSENOS = "0";
                flag = 1;
            }
            else
            {
                objSR.COURSENOS = objSR.COURSENOS.TrimEnd(',');
            }
            objSR.Backlog_course = objSR.Backlog_course.TrimEnd(',');
            objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objSR.IDNO = Convert.ToInt32(lblName.ToolTip);
            objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip)==0?Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT","SEMESTERNO","SESSIONNO="+Convert.ToInt32(ddlSession.SelectedValue)+" AND IDNO="+Convert.ToInt32(lblName.ToolTip))):Convert.ToInt32(lblSemester.ToolTip);
            objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);

            objSR.IPADDRESS = Session["ipAddress"].ToString();
            objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objSR.COLLEGE_CODE = Session["colcode"].ToString();

            objSR.REGNO = lblEnrollNo.Text.Trim();
            objSR.ROLLNO = lblRegNo.Text.Trim();

            int ret = objSReg.InsertStudentRegistrationByAdmin(objSR);
            if (ret == 1)
            {
                if (flag == 1)
                {
                    objCommon.DisplayMessage(updReg, "Course Removal Done Successfully!!", this.Page);
                    btnPrintRegSlip.Enabled = false;
                }
                else
                {
                    objCommon.DisplayMessage(updReg, "Course Registration Successful. Print the Registration Slip.", this.Page);
                    btnPrintRegSlip.Enabled = true;
                }

                txtRollNo.Enabled = false;
            }
            else
                objCommon.DisplayMessage(updReg, "Registration Failed! Error in saving record.", this.Page);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void rblOptions_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "' OR ENROLLNO = '" + txtRollNo.Text.Trim() + "'");

        if (idno == "")
        {
            objCommon.DisplayMessage(updReg, "Student Not Found for Entered Univ. Reg. No. Or TAN/PAN [" + txtRollNo.Text.Trim() + "]", this.Page);
        }
        else
        {
            ViewState["idno"] = idno;

            if (string.IsNullOrEmpty(ViewState["idno"].ToString()) || ViewState["idno"].ToString() == "0")
            {
                objCommon.DisplayMessage(updReg, "Student with Univ. Reg. No. Or TAN/PAN " + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                return;
            }
            if (Session["usertype"].ToString().Equals("8") || Session["usertype"].ToString().Equals("1") || Session["usertype"].ToString().Equals("12"))     //Admin & HoD Added on 25/07/2017
            {
                this.ShowDetails();
                ///Check current semester applied or not
                string applyCount = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ACCEPTED = 1 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                if (applyCount == "0")
                {
                    objCommon.DisplayMessage(updReg, "Student with Univ. Reg. No. Or TAN/PAN [" + txtRollNo.Text.Trim() + "] has not registered for selected session. \\nBut you can directly register him.", this.Page);
                }

                BindStudentDetails();

                txtRollNo.Enabled = false;
                ddlSession.Enabled = false;
                rblOptions.Enabled = false;
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["idno"] = "0";
        divCourses.Visible = true;
        ddlSession.Enabled = true;
        txtRollNo.Text = string.Empty;
        txtRollNo.Enabled = true;
        rblOptions.Enabled = true;

        lvCurrentSubjects.DataSource = null;
        lvCurrentSubjects.DataBind();
        lvBacklogSubjects.DataSource = null;
        lvBacklogSubjects.DataBind();
        lvAuditSubjects.DataSource = null;
        lvAuditSubjects.DataBind();

        tblInfo.Visible = false;
        ddlCollege.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
    }

    #region Private Methods

    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        ddlCollege.Focus();
    }

    private void ShowDetails()
    {
        try
        {
            DataSet dsStudent = objSReg.GetStudInfoForCourseRegi(Convert.ToInt32(ViewState["idno"].ToString()), Convert.ToInt32(ddlSession.SelectedValue));

            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                    lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                    lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";
                    lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    if (dsStudent.Tables[0].Rows[0]["YEARWISE"].ToString() == "1")
                    {
                        lblSemester.Text = dsStudent.Tables[0].Rows[0]["YEARNAME"].ToString();
                        lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEM"].ToString();
                    }
                    else
                    {
                        lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                        lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    }
                    lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                    lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                    lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();
                    lblCollege.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                    lblRegNo.Text = dsStudent.Tables[0].Rows[0]["ENROLLNO"].ToString();
                    tblInfo.Visible = true;
                    divCourses.Visible = true;
                    if (lblScheme.ToolTip == "0" || lblScheme.ToolTip == "")
                    {
                        objCommon.DisplayMessage(updReg, "Scheme is not alloted for searched student!!", this.Page);
                    }
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

    private void BindStudentDetails()
    {
        BindAvailableCourseList();
        BindStudentFailedCourseList();
        if (lvCurrentSubjects.Visible == true || lvBacklogSubjects.Visible == true || lvAuditSubjects.Visible == true)
        {
            btnSubmit.Enabled = true;
        }
        BindStudAppliedCourseList();

        string count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND REGISTERED = 1 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        if (count != "0")
        {
            objCommon.DisplayMessage(updReg, "Student Registration already done.", this.Page);
            btnSubmit.Enabled = true;
            btnPrintRegSlip.Enabled = true;
        }
    }

    private void BindAvailableCourseList()
    {      
        DataSet dsCurrCourses = null;
        //Show Current Semester Courses ..
      //dsCurrCourses = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.COURSENO", "C.CCODE,C.COURSE_NAME,C.SUBID,C.ELECT,CAST(C.CREDITS AS INT) CREDITS,S.SUBNAME, 0 as ACCEPTED, 0 as EXAM_REGISTERED, DBO.FN_DESC('SEMESTER',C.SEMESTERNO)SEMESTER ", "C.SCHEMENO = " + lblScheme.ToolTip + " AND C.SEMESTERNO = " + lblSemester.ToolTip + " AND C.OFFERED = 1", "C.CCODE");
        //dsCurrCourses = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO) INNER JOIN ACD_STUDENT_RESULT SR ON (SR.SEMESTERNO=OC.SEMESTERNO)", "DISTINCT C.COURSENO", "C.CCODE,C.COURSE_NAME,C.SUBID,C.ELECT,CAST(C.CREDITS AS INT) CREDITS,S.SUBNAME, 0 as ACCEPTED, REGISTERED, DBO.FN_DESC('SEMESTER',OC.SEMESTERNO)SEMESTER ", "C.SCHEMENO = " + lblScheme.ToolTip + "AND SR.IDNO = " + lblName.ToolTip+" AND OC.SEMESTERNO = " + lblSemester.ToolTip, "C.CCODE");
        dsCurrCourses = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO) LEFT JOIN ACD_STUDENT_RESULT SR ON (SR.SEMESTERNO=OC.SEMESTERNO)", "DISTINCT C.COURSENO", "C.CCODE,C.COURSE_NAME,C.SUBID,C.ELECT,CAST(C.CREDITS AS INT) CREDITS,S.SUBNAME, 0 as ACCEPTED, REGISTERED, DBO.FN_DESC('SEMESTER',OC.SEMESTERNO)SEMESTER ", "OC.SCHEMENO = " + lblScheme.ToolTip + " AND OC.SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND SR.IDNO = " + lblName.ToolTip + "AND ISNULL(SR.CANCEL,0)=0 AND OC.SEMESTERNO = " + lblSemester.ToolTip, "C.CCODE");
        if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
        {
            btnSubmit.Enabled = true;
            lvCurrentSubjects.DataSource = dsCurrCourses.Tables[0];
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = true;
        }
        else
        {
            btnSubmit.Enabled = false;
            lvCurrentSubjects.DataSource = null;
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = false;
            objCommon.DisplayMessage(updReg, "No Course found in Allotted Scheme and Semester.", this.Page);
        }
    }

    private void BindStudentFailedCourseList()
    {
        DataSet dsCurrCourses = null;
        //Show Arrear Semester Courses ..
        dsCurrCourses = objSReg.GetStudentCoursesForBacklogRegistration(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt32(ViewState["idno"]), 0);
        if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
        {
            lvBacklogSubjects.DataSource = dsCurrCourses.Tables[0];
            lvBacklogSubjects.DataBind();
            lvBacklogSubjects.Visible = true;
        }
        else
        {
            lvBacklogSubjects.DataSource = null;
            lvBacklogSubjects.DataBind();
            lvBacklogSubjects.Visible = false;
        }
    }

    #endregion

    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        ShowReport("RegistrationSlip", "rptPreRegslip_student.rpt");
    }

    //private void ShowReport(string reportTitle, string rptFileName)
    //{
    //    int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
    //    int idno = Convert.ToInt32(lblName.ToolTip);
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@UserName=" + Session["username"].ToString();

    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";
    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");
    //        ScriptManager.RegisterClientScriptBlock(this.updReg, this.updReg.GetType(), "controlJSScript", sb.ToString(), true);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    private void ShowReport(string reportTitle, string rptFileName)
    {
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int idno = Convert.ToInt32(lblName.ToolTip);
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@P_UA_NO=" + Session["userno"].ToString(); ;
            //+ ",@UserName=" + Session["username"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region Faculty Advisor Accepting Student Registration

    private void BindStudAppliedCourseList()
    {
        try
        {
            StudentRegistration objSRegist = new StudentRegistration();
            DataSet dsOfferedCourses = null;
            string sessionNo = string.Empty;
            dsOfferedCourses = objCommon.FillDropDown("ACD_STUDENT_RESULT SR", "DISTINCT SR.COURSENO", "SR.CCODE, SR.SEMESTERNO, SR.REGISTERED, SR.ACCEPTED", "ISNULL(SR.CANCEL,0) = 0 AND SR.SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND SR.IDNO = " + ViewState["idno"], "SR.CCODE");

            if (dsOfferedCourses != null)
            {
                if (dsOfferedCourses.Tables.Count > 0 && dsOfferedCourses.Tables[0].Rows.Count > 0)
                {
                    ListOperations(lvCurrentSubjects, dsOfferedCourses.Tables[0]);
                    ListOperations(lvBacklogSubjects, dsOfferedCourses.Tables[0]);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_courseRegistration.BindStudAppliedCourseList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ListOperations(ListView list, DataTable dt)
    {
        foreach (ListViewDataItem item in list.Items)
        {
            CheckBox cbHead = list.FindControl("cbHead") as CheckBox;
            CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
            string lblCCode = (item.FindControl("lblCCode") as Label).ToolTip;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (lblCCode == dt.Rows[i]["courseno"].ToString())
                {
                    if (dt.Rows[i]["REGISTERED"].ToString() == "1")
                    {
                        CheckBox cbHeadReg = list.FindControl("cbHeadReg") as CheckBox;
                        CheckBox chkRegister = item.FindControl("chkRegister") as CheckBox;
                        chkRegister.Checked = true;
                        cbHeadReg.Checked = true;
                    }
                }
            }
        }
    }

    protected void btnBackHOD_Click(object sender, EventArgs e)
    {
        ViewState["idno"] = "0";
        btnShow.Visible = false;
        divCourses.Visible = false;
        lvCurrentSubjects.DataSource = null;
        lvCurrentSubjects.DataBind();
        lvBacklogSubjects.DataSource = null;
        lvBacklogSubjects.DataBind();
        txtRollNo.Text = string.Empty;
        lblAdmBatch.Text = string.Empty;
        lblBranch.Text = string.Empty;
        lblEnrollNo.Text = string.Empty;
        lblFatherName.Text = string.Empty;
        lblMotherName.Text = string.Empty;
        lblName.Text = string.Empty;
        lblPH.Text = string.Empty;
        lblScheme.Text = string.Empty;
        lblSemester.Text = string.Empty;
        rblOptions.Enabled = true;
    }

    #endregion

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSession.SelectedIndex = 0;
        ddlSession.Items.Clear();
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND COLLEGE_ID=" + ddlCollege.SelectedValue, "SESSIONNO DESC");
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Insert(0, "Please Select");
        }
    }

}


