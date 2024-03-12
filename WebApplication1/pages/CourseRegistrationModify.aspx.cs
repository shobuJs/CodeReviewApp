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

public partial class ACADEMIC_CourseRegistrationModify : System.Web.UI.Page
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
                //ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                ViewState["ipAddress"] = IPADDRESS;

                ////Check for Activity On/Off for course registration.
                //if (CheckActivity())
                //{
                ViewState["action"] = "add";
                ViewState["idno"] = "0";
                ///if (Session["usertype"].ToString().Equals("1"))     //Only Admin 
                //if (Session["dec"].ToString().Equals("1") || Session["usertype"].ToString().Equals("1"))
                if (Session["usertype"].ToString().Equals("8") || Session["usertype"].ToString().Equals("1") || Session["usertype"].ToString().Equals("12") || Session["usertype"].ToString().Equals("3") )   //Only Admin & HoD Added on 25/07/2017
                {
                    divOptions.Visible = false;
                    divCourses.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage("You are Not Authorized to View this Page. Contact Admin.", this.Page);
                }
                objCommon.SetLabelData("1");
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // dynamic page header

                //}
                //else
                //{
                //    divCourses.Visible = true;
                //    divOptions.Visible = false;
                //}
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

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
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

            objSR.EXAM_REGISTERED = 1;

            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {
                if ((dataitem.FindControl("chkRegister") as CheckBox).Checked == true)
                    objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
            }
            foreach (ListViewDataItem dataitem in lvBacklogSubjects.Items)
            {
                if ((dataitem.FindControl("chkRegister") as CheckBox).Checked == true)
                    objSR.Backlog_course = objSR.Backlog_course + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
            }
            if (objSR.COURSENOS == null)
            {
                objSR.COURSENOS = "0";
                flag = 1;
            }
            else
            {
                objSR.COURSENOS = objSR.COURSENOS.TrimEnd('$');
            }
            objSR.Backlog_course = objSR.Backlog_course.TrimEnd('$');
            //if (objSR.COURSENOS.Length > 0 || objSR.Backlog_course.Length > 0 || objSR.Audit_course.Length > 0)
            //{
            //Add registered 
            objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objSR.IDNO = Convert.ToInt32(lblName.ToolTip);
            objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip);
            objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);

            objSR.IPADDRESS = Session["ipAddress"].ToString();
            objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.REGNO = lblEnrollNo.Text.Trim();
            objSR.ROLLNO = txtRollNo.Text.Trim();

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
                    objCommon.DisplayMessage(updReg, "Course Registration Successfull. Print the Registration Slip.", this.Page);
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
       // string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");
        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO='" + txtRollNo.Text.Trim() + "' OR REGNO='" + txtRollNo.Text.Trim() + "'");


        if (idno == "")
        {
            objCommon.DisplayMessage(updReg, "Student Not Found for Entered Registration No.[" + txtRollNo.Text.Trim() + "]", this.Page);
        }
        else
        {
            ViewState["idno"] = idno;

            if (string.IsNullOrEmpty(ViewState["idno"].ToString()) || ViewState["idno"].ToString() == "0")
            {
                objCommon.DisplayMessage(updReg, "Student with Registration No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                return;
            }
            ////if (Session["usertype"].ToString().Equals("1"))
            if (Session["usertype"].ToString().Equals("8") || Session["usertype"].ToString().Equals("1") || Session["usertype"].ToString().Equals("12") || Session["usertype"].ToString().Equals("3") )     //Admin & HoD Added on 25/07/2017
            {
                this.ShowDetails();
                ///Check current semester applied or not
                string applyCount = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ACCEPTED = 1 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                if (applyCount == "0")
                {
                    objCommon.DisplayMessage(updReg, "Student with Registration No.[" + txtRollNo.Text.Trim() + "] has not registered for selected session. \\nBut you can directly register him.", this.Page);
                    //return;
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

        //Response.Redirect(Request.Url.ToString());
    }

    #region Private Methods

    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONID", "SESSION_NAME", "SESSIONID > 0 ", "SESSIONID DESC");
        ddlSession.SelectedIndex = 1;
        ddlSession.Focus();
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
                    lblFatherName.Text = dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                    lblMotherName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() ;
                    lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                    lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                    lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();
                    lblCollege.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                    lblRegNo.Text = dsStudent.Tables[0].Rows[0]["ENROLLNO"].ToString();

                    //ViewState["PREV_STATUS"] = dsStudent.Tables[0].Rows[0]["ISREGULAR"].ToString();
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
        //string CountEligibility = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ACCEPTED = 1 AND EXAM_REGISTERED = 1 AND IDNO=" + ViewState["idno"] + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SESSIONNO <" + Convert.ToInt32(ddlSession.SelectedValue));
        //if (Convert.ToInt32(CountEligibility) == 0)
        //{
        BindAvailableCourseList();
        //}
        BindStudentFailedCourseList();
        if (lvCurrentSubjects.Visible == true || lvBacklogSubjects.Visible == true || lvAuditSubjects.Visible == true)
        {
            btnSubmit.Enabled = true;
        }
        BindStudAppliedCourseList();

        //Check current semester registered or not  //PREV_STATUS = 0 and 
        string count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND EXAM_REGISTERED = 1 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
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
      // dsCurrCourses = objCommon.FillDropDown("ACD_STUDENT_RESULT SR INNER JOIN ACD_OFFERED_COURSE C ON (SR.SESSIONNO=C.SESSIONNO AND SR.SCHEMENO=C.SCHEMENO AND C.COURSENO=SR.COURSENO AND SR.SEMESTERNO=C.SEMESTERNO)INNER JOIN ACD_SUBJECTTYPE S ON (sr.SUBID = S.SUBID)LEFT JOIN ACD_SEMESTER SM ON (sr.SEMESTERNO = SM.SEMESTERNO)INNER JOIN ACD_CORE_ELECTIVE E ON (E.GROUPNO=C.ELECT)", "DISTINCT SR.COURSENO", "ELECTIVENAME AS ELECT,C.ELECT,SR.CCODE, SR.SEMESTERNO, SR.EXAM_REGISTERED,Sr.COURSENAME as COURSE_NAME,S.SUBNAME,S.SUBID,SM.SEMESTERNAME AS SEMESTER,sr.CREDITS", "ISNULL(SR.CANCEL,0) = 0 AND SR.SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND SR.IDNO = " + ViewState["idno"], "SR.CCODE");
       
        string SP_Name1 = "PKG_GET_STUDENT_OFFER_AND_REGISTER_COURSE";    //ADDED BY ROSHAN PATIL 23/09/2022
        string SP_Parameters1 = "@P_IDNO,@P_SESSIONNO";
        string Call_Values1 = ""+Convert.ToInt32(ViewState["idno"].ToString())+","+ Convert.ToInt32(ddlSession.SelectedValue) +"";
        dsCurrCourses = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);
  
        //dsCurrCourses = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) INNER JOIN ACD_OFFERED_COURSE O ON (O.COURSENO=C.COURSENO AND C.SEMESTERNO=O.SEMESTERNO)", "DISTINCT C.COURSENO", "C.CCODE,C.COURSE_NAME,C.SUBID,C.ELECT,C.CREDITS,S.SUBNAME, 0 as ACCEPTED, 0 as EXAM_REGISTERED, DBO.FN_DESC('SEMESTER',C.SEMESTERNO)SEMESTER ", "O.SCHEMENO = " + lblScheme.ToolTip + " AND O.SEMESTERNO = " + lblSemester.ToolTip + "" , "C.CCODE");
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
        //Show Backlog Semester Courses ..
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
        ShowReport("RegistrationSlip", "rptCourseRegSlip.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int idno = Convert.ToInt32(lblName.ToolTip);
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
            string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@UserName=" + Session["username"].ToString();
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@UserName=" + Session["username"].ToString() + ",@P_UANO=" + Convert.ToInt32(Session["userno"].ToString());
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updReg, this.updReg.GetType(), "controlJSScript", sb.ToString(), true);
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
            dsOfferedCourses = objCommon.FillDropDown("ACD_STUDENT_RESULT SR", "DISTINCT SR.COURSENO", "SR.CCODE, SR.SEMESTERNO, SR.EXAM_REGISTERED", "ISNULL(SR.CANCEL,0) = 0 AND SR.SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND SR.IDNO = " + ViewState["idno"], "SR.CCODE");

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
                    if (dt.Rows[i]["EXAM_REGISTERED"].ToString() == "1")
                    {
                        CheckBox cbHeadReg = list.FindControl("cbHeadReg") as CheckBox;
                        CheckBox chkRegister = item.FindControl("chkRegister") as CheckBox;
                        chkRegister.Checked = true;
                        cbHeadReg.Checked = false;
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
}


