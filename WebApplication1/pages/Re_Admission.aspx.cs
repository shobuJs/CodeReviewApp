//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : RE ADMISSION                                    
// CREATION DATE : 04 April 2020
// ADDED BY      : MR. Pankaj Nakhale
// ADDED DATE    : 
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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net;

public partial class ACADEMIC_Re_Admission : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    ActivityController objActController = new ActivityController();
    StudentRegistration objSRegist = new StudentRegistration();
    StudentRegist objSR = new StudentRegist();

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
                // this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //this.PopulateDropDownList();
                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                string IPADDRESS = string.Empty;

                IPADDRESS = ip.AddressList[0].ToString();
                //ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                ViewState["ipAddress"] = IPADDRESS;

                ////Check for Activity On/Off for course registration.
                if (CheckActivity() == false)
                {
                    //fees.Visible = false;
                    return;
                }
                else
                {
                    this.PopulateDropDownList();
                    ViewState["action"] = "add";
                    ViewState["idno"] = "0";
                    if (Session["usertype"].ToString().Equals("1"))     //Only Admin 
                    {
                        divOptions.Visible = false;
                        divCourses.Visible = true;
                    }
                    else
                    {
                        objCommon.DisplayMessage(updCourse, "You are Not Authorized to View this Page. Contact Admin.", this.Page);
                    }
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
                Response.Redirect("~/notauthorized.aspx?page=Re_Admission.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Re_Admission.aspx");
        }
    }

    private bool CheckActivity()
    {
        try
        {
            bool ret = true;
            string sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
            if (sessionno != "")
            {
                //DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), Convert.ToString(Degreeno), Convert.ToString(branchno), Convert.ToString(Semesterno));
                DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));
                if (dtr.Read())
                {
                    if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                    {
                        objCommon.DisplayMessage(updCourse, "This Activity has been Stopped. Contact Admin.!!", this.Page);
                        ret = false;
                    }

                    if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                    {
                        objCommon.DisplayMessage(updCourse, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                        ret = false;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updCourse, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                    ret = false;
                }
                dtr.Close();
                return ret;
            }
            else
            {
                objCommon.DisplayMessage(updCourse, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                ret = false;
                return ret;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.ShowError(Page, "ACADEMIC_PhotoCopyRegistration.CheckActivity() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            else
            {
                objCommon.ShowError(Page, "Server Unavailable.");
                return false;
            }
        }

    }
    #endregion
    //used for register offered courses
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            StudentRegist objSR = new StudentRegist();

            objSR.EXAM_REGISTERED = 1;
            decimal totalcheckedCredit = 0;

            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {
                if ((dataitem.FindControl("chkRegister") as CheckBox).Checked == true)
                {
                    objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                    Label lb = (Label)dataitem.FindControl("lblCredits");
                    totalcheckedCredit = totalcheckedCredit + Convert.ToDecimal(lb.Text);
                }
            }

            if (objSR.COURSENOS == null)
            {
                objCommon.DisplayMessage(updCourse, "Please select atleast one course for Re-Admission.", this.Page);
                return;
            }
            else
            {

                objSR.COURSENOS = objSR.COURSENOS.TrimEnd('$');
                objSR.Backlog_course = objSR.Backlog_course.TrimEnd('$');
                objSR.Audit_course = objSR.Audit_course.TrimEnd('$');

                //if (objSR.COURSENOS.Length > 0 || objSR.Backlog_course.Length > 0 || objSR.Audit_course.Length > 0)
                //{
                //Add registered 
                objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                objSR.IDNO = Convert.ToInt32(lblName.ToolTip);
                //objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip);
                objSR.SEMESTERNO = Convert.ToInt32(ddlSem.SelectedValue);
                objSR.SCHEMENO = Convert.ToInt32(ddlScheme.SelectedItem.Value);  // pankakj 4 april   Convert.ToInt32(lblScheme.ToolTip);

                objSR.IPADDRESS = Session["ipAddress"].ToString();
                objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                objSR.COLLEGE_CODE = Session["colcode"].ToString();
                objSR.REGNO = lblEnrollNo.Text.Trim();
                //objSR.ROLLNO = txtRollNo.Text.Trim();
                //check register credit is not less than and grater than of rule book credit.
                if (totalcheckedCredit <= Convert.ToDecimal(lblfTocredit.Text) && totalcheckedCredit >= Convert.ToDecimal(lblfromcredit.Text))
                {
                    int ret = objSReg.InsertStudent_Re_Admission(objSR);
                    if (ret == 1)
                    {
                        objCommon.DisplayMessage(updCourse, "Course Registration Done Successfully . Print the Registration Slip.", this.Page);
                        btnPrintRegSlip.Enabled = true;
                        txtRollNo.Enabled = true;
                        btnSubmit.Visible = false;

                    }
                    else
                        objCommon.DisplayMessage(updCourse, "Course Registration Failed! Error in saving record.", this.Page);
                }
                else
                {

                    //  objCommon.ShowError(Page, "Please Register  Credit's between " + lblfromcredit.Text + "  to  " + lblfTocredit.Text + " .");
                    objCommon.DisplayMessage(updCourse, "Please Register Credits between " + lblfromcredit.Text + "  to  " + lblfTocredit.Text + "!", this.Page);
                    return;
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistrationByAdmin.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void rblOptions_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    // used for showing student details on based on entered regno and enrollno
    protected void btnShow_Click(object sender, EventArgs e)
    {
        ////string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");
      //  string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'OR ENROLLNO = '" + txtRollNo.Text.Trim() + "' ");
        string idno = string.Empty;
        int count = Convert.ToInt32(objCommon.LookUp("RE_ADMISSION_SEQUENCE_LOG", "count(*)", "REGNO_NEW = '" + txtRollNo.Text.Trim() + "'OR ENROLLNO_NEW = '" + txtRollNo.Text.Trim() + "' and SESSIONNO="+ddlSession.SelectedValue+" "));
        if (count > 0)
        {
               idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'OR ENROLLNO = '" + txtRollNo.Text.Trim() + "' ");
        }
        else
        {
            idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "(REGNO = '" + txtRollNo.Text.Trim() + "'OR ENROLLNO = '" + txtRollNo.Text.Trim() + "') and can=1 and admcan=1 and BREAK_FLAG=1");
        }
       // string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'OR ENROLLNO = '" + txtRollNo.Text.Trim() + "' and can=1 and admcan=1 ");
        if (idno == "")
        {
            objCommon.DisplayMessage(updCourse, "Student Not Found for Entered Registration No.[" + txtRollNo.Text.Trim() + "]", this.Page);
        }
        else
        {
            ViewState["idno"] = idno;

            if (string.IsNullOrEmpty(ViewState["idno"].ToString()) || ViewState["idno"].ToString() == "0")
            {
                objCommon.DisplayMessage(updCourse, "Student with Registration No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                return;
            }
            if (Session["usertype"].ToString().Equals("1"))
            {
                this.ShowDetails();
                ///Check current semester applied or not
                //string applyCount = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ACCEPTED = 1 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                //if (applyCount == "0")
                //{
                //    objCommon.DisplayMessage(updCourse, "Student with Registration No.[" + txtRollNo.Text.Trim() + "] has not registered for selected session. \\n  You can register this Student.", this.Page);
                //    //return;
                //}

                //  BindStudentDetails();

                txtRollNo.Enabled = false;
                ddlSession.Enabled = false;
                rblOptions.Enabled = false;
            }
        }
    }
    //used for refresh pages.
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

    }

    #region Private Methods
    //bind session on loading page
    private void PopulateDropDownList()
    {
        int page = Convert.ToInt32(Request.QueryString["pageno"].ToString());
        // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND EXAMTYPE=1 and flock=1", "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1 and PAGE_LINK = '" + Request.QueryString["pageno"].ToString() + "')", "SESSIONNO DESC");
        ddlSession.SelectedIndex = 1;
        ddlSession.Enabled = false;
        ddlSession.Focus();
    }
    //used for showing student details
    private void ShowDetails()
    {
        try
        {
            DataSet dsStudent = objSReg.GetStudInfoForCourseRegi_ForAdmission(Convert.ToInt32(ViewState["idno"].ToString()), Convert.ToInt32(ddlSession.SelectedValue));

            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                    lblFatherName.Text = dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                    lblMotherName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                    //lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                    //lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";
                    lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                    lblAdmno.Text = dsStudent.Tables[0].Rows[0]["ENROLLNO"].ToString();
                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    //lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    //lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                    lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                    //   ddlScheme.SelectedItem.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                    //ddlScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    //   ddlSemester.SelectedItem.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    // ddlSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    //  ddlAdmBatch.SelectedItem.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                    // ddlAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                    lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();
                    //ViewState["PREV_STATUS"] = dsStudent.Tables[0].Rows[0]["ISREGULAR"].ToString();
                    tblInfo.Visible = true;
                    divCourses.Visible = true;

                    int degreeno = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString());

                    int evenodd = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "ODD_EVEN", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));
                    int semesterNo = Convert.ToInt32(objCommon.LookUp("acd_student", "semesterNo", "idno=" + Convert.ToInt32(ViewState["idno"].ToString())));
                    //int semesterNo = Convert.ToInt32(objCommon.LookUp("acd_student", "semesterNo", "can=1 and admcan=1 and idno=" + Convert.ToInt32(ViewState["idno"].ToString())));
                    // used for bind odd sem only by pankaj 08042020 objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO >0 AND SEMESTERNO <=" + semesterNo + "AND ODD_EVEN=" + evenodd, "SEMESTERNO");
                    //objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO >0 AND SEMESTERNO <=" + lblSemester.Text, "SEMESTERNO");

                    // objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO >0 AND SEMESTERNO <=" + semesterNo + "AND ODD_EVEN=" + evenodd, "SEMESTERNO");


                    ///////////       find current schemeno and admbatchno from acd_student
                    objSR.SCHEMENO = Convert.ToInt32(objCommon.LookUp("acd_student", "schemeno", "idno=" + lblName.ToolTip));
                    int admbatch = Convert.ToInt32(objCommon.LookUp("acd_student", "admbatch", "idno=" + lblName.ToolTip));
                    //  ddlScheme.SelectedValue = schemeno.ToString();
                    //  ddlAdmBatch.SelectedValue = admbatch.ToString();

                    //////////////
                    // bind schemename                  

                    objCommon.FillDropDownList(ddlScheme, "acd_scheme", "schemeno", "schemename", "branchno=" + lblBranch.ToolTip + " AND schemeno >=" + lblScheme.ToolTip + "AND degreeno=" + degreeno, "schemeno");
                    //ddlScheme.SelectedIndex = 1;
                    ddlScheme.SelectedValue = objSR.SCHEMENO.ToString();
                    // bind batchname
                    objCommon.FillDropDownList(ddlAdmBatch, "acd_admbatch", "batchno", "batchname", "BATCHNO >=" + lblAdmBatch.ToolTip, "BATCHNO DESC");
                    // ddlAdmBatch.SelectedIndex = 1;
                    ddlAdmBatch.SelectedValue = admbatch.ToString();
                    lvCurrentSubjects.DataSource = null;
                    lvCurrentSubjects.DataBind();

                    int Re_Admitted_or_not = Convert.ToInt32(objCommon.LookUp("acd_student", "count(*)", "idno=" + lblName.ToolTip + " and RE_ADMITTED=" + 1));
                    if (Re_Admitted_or_not > 0)
                    {

                        objSR.SEMESTERNO = Convert.ToInt32(objCommon.LookUp("acd_student", "SEMESTERNO", "idno=" + lblName.ToolTip));
                        objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO >0 AND SEMESTERNO =" + objSR.SEMESTERNO, "SEMESTERNO");
                        ddlSem.SelectedIndex = 1;


                        btnSubmitDetails.Enabled = false;
                        btnPrintRegSlip.Enabled = true;
                        ddlSem.Enabled = false;
                        ddlScheme.Enabled = false;
                        ddlAdmBatch.Enabled = false;

                        btnSubmitDetails.Visible = true;
                        btnSubmit.Visible = false;
                        objCommon.DisplayMessage(updCourse, "This Student Allready Re-Admitted !", this.Page);                        
                    }
                    else
                    {
                        //check this student details is allready updated or not for this session
                        int updatedetailsornot = Convert.ToInt32(objCommon.LookUp("RE_ADMISSION_SEQUENCE_LOG", "count(*)", "idno=" + lblName.ToolTip + " and SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));

                        if (updatedetailsornot > 0)
                        {
                            objSR.SEMESTERNO = Convert.ToInt32(objCommon.LookUp("acd_student", "SEMESTERNO", "idno=" + lblName.ToolTip));
                            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO >0 AND SEMESTERNO =" + objSR.SEMESTERNO, "SEMESTERNO");//used for get registerd semester
                            ddlSem.SelectedIndex = 1;
                        }
                        else
                        {
                            int ODDEVEN = Convert.ToInt32(objCommon.LookUp("acd_session_master", "ODD_EVEN", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue)));
                            //int ODDEVEN = Convert.ToInt32(objCommon.LookUp("ACD_SEMESTER", "ODD_EVEN", "SEMESTERNAME=" + lblSemester.Text)); AND ODD_EVEN=" + ODDEVEN + "
                            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO >0 AND ODD_EVEN=" + ODDEVEN + "  AND SEMESTERNO <=" + lblSemester.Text, "SEMESTERNO");
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Re_Admission.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindAvailableCourseList()
    {
        try
        {
            DataSet dsCurrCourses = null;
            dsCurrCourses = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) LEFT JOIN ACD_ELECTGROUP E ON (C.GROUPNO=E.GROUPNO) LEFT JOIN ACD_STUDENT_RESULT R ON (C.COURSENO=R.COURSENO) inner join  acd_offered_course  oc on (oc.courseno= c.courseno)", "DISTINCT C.COURSENO", "C.CCODE,C.GROUPNO,C.COURSE_NAME,C.SUBID,C.ELECT,CASE WHEN C.ELECT=0 THEN 'Core' else E.GROUPNAME END AS ELECTIVE,C.CREDITS AS CREDITS,S.SUBNAME, 0 as ACCEPTED, 0 as EXAM_REGISTERED, DBO.FN_DESC('SEMESTER',OC.SEMESTERNO)SEMESTER,(CASE WHEN RE_ADMITTED=1 THEN '1' ELSE '0' END) RE_ADMITTED ", "C.SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedItem.Value) + " AND oC.SEMESTERNO = " + Convert.ToInt32(ddlSem.SelectedValue) + " AND oc.sessionno =" + Convert.ToInt32(ddlSession.SelectedValue) + " and isnull(RE_ADMITTED,0)=" + 0, "C.COURSENO");

            if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Enabled = true;
                lvCurrentSubjects.DataSource = dsCurrCourses.Tables[0];
                lvCurrentSubjects.DataBind();
                lvCurrentSubjects.Visible = true;
                btnSubmit.Visible = false;
            }
            else
            {
                btnSubmit.Enabled = false;
                lvCurrentSubjects.DataSource = null;
                lvCurrentSubjects.DataBind();
                lvCurrentSubjects.Visible = false;
                objCommon.DisplayMessage(updCourse, "No Course found in Allotted Scheme and Semester.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Re_Admission.BindAvailableCourseList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindRegisteredCourseList()
    {
        try
        {
            DataSet dsCurrCourses = null;
            //Show Current Semester Courses ..
            //dsCurrCourses = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) LEFT JOIN ACD_ELECTGROUP E ON (C.GROUPNO=E.GROUPNO)", "DISTINCT C.COURSENO", "C.CCODE,C.GROUPNO,C.COURSE_NAME,C.SUBID,C.ELECT,CASE WHEN ELECT=0 THEN 'Core' else E.GROUPNAME END AS ELECTIVE,CAST(C.CREDITS AS INT) CREDITS,S.SUBNAME, 0 as ACCEPTED, 0 as EXAM_REGISTERED, DBO.FN_DESC('SEMESTER',C.SEMESTERNO)SEMESTER ", "C.SCHEMENO = " + lblScheme.ToolTip + " AND C.SEMESTERNO = " + Convert.ToInt32(ddlSem.SelectedValue) + " AND C.OFFERED = 1", "C.GROUPNO,C.CCODE");
            dsCurrCourses = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) LEFT JOIN ACD_ELECTGROUP E ON (C.GROUPNO=E.GROUPNO) LEFT JOIN ACD_STUDENT_RESULT R ON (C.COURSENO=R.COURSENO)", "DISTINCT C.COURSENO", "C.CCODE,C.GROUPNO,C.COURSE_NAME,C.SUBID,C.ELECT,CASE WHEN C.ELECT=0 THEN 'Core' else E.GROUPNAME END AS ELECTIVE,CAST(C.CREDITS AS INT) CREDITS,S.SUBNAME, 0 as ACCEPTED, 0 as EXAM_REGISTERED, DBO.FN_DESC('SEMESTER',C.SEMESTERNO)SEMESTER,(CASE WHEN RE_ADMITTED=1 THEN '1' ELSE '0' END) RE_ADMITTED ", "C.SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedItem.Value) + " AND C.SEMESTERNO = " + Convert.ToInt32(ddlSem.SelectedValue) + " AND C.OFFERED = 1 AND RE_ADMITTED = 1", "C.GROUPNO,C.CCODE");

            if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
            {
                //btnSubmit.Enabled = true;
                lvCurrentSubjects.DataSource = dsCurrCourses.Tables[0];
                lvCurrentSubjects.DataBind();
                lvCurrentSubjects.Visible = true;

                btnSubmit.Visible = false;
            }
            else
            {
                btnSubmit.Enabled = false;
                lvCurrentSubjects.DataSource = null;
                lvCurrentSubjects.DataBind();
                lvCurrentSubjects.Visible = false;
                objCommon.DisplayMessage(updCourse, "No Course found in Allotted Scheme and Semester.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Re_Admission.BindAvailableCourseList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion
    // used for get register course slip.
    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        ShowReport("RegistrationSlip", "rptRe_ReAdmissionSlip.rpt");
    }

    //used for generating course registration slip. by pankaj nakhale 09 04 2020
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
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@P_UA_NO=" + Session["userno"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updCourse, this.updCourse.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Re_Admission.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region Faculty Advisor Accepting Student Registration

    #endregion
    //this is used for to updating semester name and scheme name , adm batch name , regno , enrollment no in acd_student table addded by pankaj nakhale 08 04 2020
    protected void btnSubmitDetails_Click(object sender, EventArgs e)
    {

        int admbatch = Convert.ToInt32(ddlAdmBatch.SelectedItem.Value);
        objSR.SCHEMENO = Convert.ToInt32(ddlScheme.SelectedItem.Value);
        objSR.SEMESTERNO = Convert.ToInt32(ddlSem.SelectedItem.Value);
        string regno = lblEnrollNo.Text;
        objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
        int IDNO = Convert.ToInt32(lblName.ToolTip);
        int UA_NO = Convert.ToInt32(Session["userno"].ToString());
        int deegreeno = Convert.ToInt32(objCommon.LookUp("acd_student", "degreeno", "IDNO=" + IDNO));
        int IDTYPE = Convert.ToInt32(objCommon.LookUp("acd_student", "IDTYPE", "IDNO=" + IDNO));
       // decimal fromCredit = 0;
       // decimal ToCredit = 0;
        //dsAuditCourse = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) INNER JOIN ACD_NONCREDIT_COURSE_STUD NC ON (C.COURSENO = NC.COURSENO)", "DISTINCT C.COURSENO", "C.CCODE,C.COURSE_NAME,C.SUBID,C.ELECT,0 as CREDITS,S.SUBNAME, 0 as ACCEPTED, 0 as EXAM_REGISTERED, DBO.FN_DESC('SEMESTER',C.SEMESTERNO)SEMESTER ", "NC.IDNO = " + ViewState["idno"] + " AND NC.SESSIONNO = " + ddlSession.SelectedValue + " AND C.OFFERED = 1", "C.CCODE");

        //DataSet ds = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT", "FROM_CREDIT", "To_CREDIT", "SESSIONNO = " + Convert.ToInt32(objSR.SESSIONNO) + "AND DEGREENO =" + deegreeno + " AND To_CREDIT > 0 and adm_type=" + IDTYPE + " AND " + Convert.ToInt32(objSR.SEMESTERNO) + " IN(SELECT A.VALUE FROM DBO.SPLIT((SEMESTER),',')A)", "IDNO");
        //if (ds != null && ds.Tables.Count > 0)
        //{
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        fromCredit = (ds.Tables[0].Rows[0]["FROM_CREDIT"].ToString() == string.Empty ? 1 : Convert.ToInt32(ds.Tables[0].Rows[0]["FROM_CREDIT"].ToString()));
        //        ToCredit = (ds.Tables[0].Rows[0]["To_CREDIT"].ToString() == string.Empty ? 1 : Convert.ToInt32(ds.Tables[0].Rows[0]["To_CREDIT"].ToString()));
        //    }
        //    else
        //    {
        //        objCommon.DisplayMessage(updCourse, "Credit Not Defined For Current Session In Rule Book. So,Please Define Credit In Rule Book And Proceed For Course Registration !", this.Page);
        //        return;
        //    }
        //}
        //else
        //{
        //    objCommon.DisplayMessage(updCourse, "Credit Not Defined For Current Session In Rule Book. So,Please Define Credit In Rule Book And Proceed For Course Registration !", this.Page);
        //    return;
        //}
        // this patch is used for update student details like schemeno , semesterno , admbatch , regno , enrollment no. added by pankaj nakhale 06 april 2020
        if (ddlSem.SelectedIndex > 0)
        {
            //check this student details is allready updated or not for this session
            int Re_Admitted_or_not = Convert.ToInt32(objCommon.LookUp("RE_ADMISSION_SEQUENCE_LOG", "count(*)", "idno=" + lblName.ToolTip + " and SESSIONNO=" + Convert.ToInt32(objSR.SESSIONNO)));

            if (Re_Admitted_or_not > 0)
            {
                objCommon.DisplayMessage(updCourse, "Re-Admission Student Details Updated Allready. So,Please proceed For Course Registration  for selected semester.", this.Page);
            }
            else
            {

                int ret = objSReg.Update_StudentDetails_For_Re_Admission(admbatch, Convert.ToInt32(objSR.SCHEMENO), Convert.ToInt32(objSR.SEMESTERNO), regno, Convert.ToInt32(objSR.SESSIONNO), IDNO, UA_NO, deegreeno);
                //int ret = objSReg.Update_StudentDetails_For_Re_Admission(admbatch, scheme, semesetr, regno, SESSIONNO, IDNO, UA_NO, deegreeno);
                if (ret == 1)
                {
                    objCommon.DisplayMessage(updCourse, "Re-Admission Student Details Updated Successfully. So,Please Proceed For Course Registration on Course Registration Page!", this.Page);
                    btnPrintRegSlip.Enabled = false;
                    btnPrintRegSlip.Visible= false;
                    txtRollNo.Enabled = true;
                    btnSubmitDetails.Enabled = false;
                }
                else
                    objCommon.DisplayMessage(updCourse, "Re-Admission Student Details Updated Failed! Error in saving record.", this.Page);
            }
            // this patch is used for bind availabel and registerd course list added by pankaj nakhale 06 april 2020

            // BindStudentDetails();

            //string count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND EXAM_REGISTERED = 1 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND RE_ADMITTED=1 AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue));
            //if (count != "0")
            //{
            //    BindRegisteredCourseList();
            //}
            //else
            //{
            //    BindAvailableCourseList();
            //}

            //lblfromcredit.Text = fromCredit.ToString();
            //lblfTocredit.Text = ToCredit.ToString();
            //l1.Visible = true;
            //l2.Visible = true;
            //lblfromcredit.Visible = true;
            //lblfTocredit.Visible = true;

        }
        else
        {
            objCommon.DisplayMessage(updCourse, "Please select semester for proceed the Re-Admission process as per as selected semester.", this.Page);
        }
    }


}

