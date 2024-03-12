//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : CHANGE ELECTIVE COURSE                                   
// CREATION DATE : 12-AUG-2013
// ADDED BY      : ASHISH DHAKATE                                                
// ADDED DATE    : 
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_CourseRegistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    int retCnt;

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
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }

        }

        divCourses.Visible = true;
        ddlSession.SelectedIndex = 0;
        PopulateDropDownList();

        divMsg.InnerHtml = string.Empty;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.ShowDetails();
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string chkMark = objCommon.LookUp("ACD_STUDENT_RESULT", "S2MARK", "IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlOfferedCourse.SelectedValue));

        if (chkMark == null || chkMark == string.Empty)
        {
            if (Convert.ToInt32(ddlOfferedCourse.SelectedValue) > 0 && Convert.ToInt32(ddlOfferedCourse.SelectedValue) > 1)
            {

                StudentRegist objSR = new StudentRegist();
                objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                objSR.IDNO = Convert.ToInt32(lblName.ToolTip);
                objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip);
                objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
                objSR.IPADDRESS = ViewState["ipAddress"].ToString();
                objSR.COLLEGE_CODE = Session["colcode"].ToString();
                objSR.REGNO = txtRollNo.Text.Trim();
                objSR.ROLLNO = txtRollNo.Text.Trim();
                objSR.COURSENO = Convert.ToInt32(ddlOfferedCourse.SelectedValue);
                objSR.SELECT_COURSE = Convert.ToInt32(ddlSelectCourse.SelectedValue);
                double credits = Convert.ToDouble(objCommon.LookUp("ACD_COURSE", "CREDITS", "COURSENO=" + ddlSelectCourse.SelectedValue));
                objSR.CREDITS = Convert.ToInt32(credits);
                objSR.UA_NO = Convert.ToInt32(Session["userno"]);



                int ret = objSReg.AddUpdElectiveSubject(objSR);

                if (ret > 0)
                {

                    objCommon.DisplayMessage("Elective course Registration is successfull.", this.Page);

                }
                ShowDetails();
            }
        }

        else
        {
            objCommon.DisplayMessage("Mark entry enter for this student ! No change the elective course", this.Page);
        }

    }

    #region
    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");
        ddlSession.SelectedIndex = 1;
        ddlSession.Focus();

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
    private void ShowDetails()
    {
        try
        {
            if (txtRollNo.Text.Trim() == string.Empty)
            {
                objCommon.DisplayMessage("Please Enter Student Roll No.", this.Page);
                return;
            }

            string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");
            string sessionno = ddlSession.SelectedValue;

            if (string.IsNullOrEmpty(idno))
            {
                objCommon.DisplayMessage("Student with Roll No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                return;
            }

            string branchno = objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + idno);
            string deptno = objCommon.LookUp("ACD_BRANCH", "DEPTNO", "BRANCHNO=" + branchno);
            int hoddeptno = Convert.ToInt32(Session["userdeptno"]);

            //if (Convert.ToInt32(deptno) == hoddeptno || hoddeptno == 0)
            //{

            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO)", "S.IDNO,DG.DEGREENAME", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE S.PHYSICALLY_HANDICAPPED WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH", "S.IDNO = " + idno, string.Empty);
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    //Show Student Details..
                    lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                    lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                    lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";


                    lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["ENROLLNO"].ToString();
                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                    lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                    //physically handicapped
                    lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();
                    tblInfo.Visible = true;

                    //Show Already saved in  Course table courses..
                    //objCommon.FillDropDownList(ddlOfferedCourse, "ACD_STUDENT_RESULT R INNER JOIN ACD_COURSE C ON (R.COURSENO=C.COURSENO) AND (R.SCHEMENO=C.SCHEMENO) AND (R.SEMESTERNO=C.SEMESTERNO) AND (R.SUBID=C.SUBID)", "DISTINCT R.COURSENO", "(R.CCODE +'-'+R.COURSENAME) AS COURSENAME", "R.IDNO=" + idno + " AND ISNULL(R.CANCEL,0)=0 AND R.SESSIONNO=" + ddlSession.SelectedValue + " AND GROUPNO IN (5,6)", "R.COURSENO");
                    objCommon.FillDropDownList(ddlSelectCourse, "ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) LEFT OUTER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.IDNO = " + idno + ")", "DISTINCT C.COURSENO", "(C.CCODE +'-'+C.COURSE_NAME) AS COURSE_NAME", "C.SCHEMENO = " + lblScheme.ToolTip + " AND C.SEMESTERNO=" + lblSemester.ToolTip + " AND C.ELECT > 0 ", "C.COURSENO");
                    //ddlOfferedCourse.Items.Add(new ListItem("Others", "1"));
                    //ddlOfferedCourse.Items.Add(new ListItem("Add New Course", "2"));

                    //Show Current Semester Alloted Courses..
                    DataSet dsHistCourses = objCommon.FillDropDown("ACD_STUDENT_RESULT R INNER JOIN ACD_SEMESTER S ON (R.SEMESTERNO = S.SEMESTERNO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = R.SESSIONNO)", "SM.SESSIONNO,R.SEMESTERNO,R.IDNO,R.SCHEMENO", "SM.SESSION_NAME,S.SEMESTERNAME,R.CCODE,R.COURSENAME,R.GRADE,R.CREDITS", "R.IDNO = " + idno + " AND ISNULL(R.CANCEL,0)=0 AND  R.SEMESTERNO=" + lblSemester.ToolTip, "R.SESSIONNO,R.SEMESTERNO,R.CCODE");
                    lvHistory.DataSource = dsHistCourses.Tables[0];
                    lvHistory.DataBind();

                    divCourses.Visible = true;
                    objCommon.FillDropDownList(ddlElectiveCourse, "ACD_ELECTGROUP", "GROUPNO", "GROUPNAME", "GROUPNO in (5,6)", "GROUPNO DESC");
                    ddlOfferedCourse.SelectedIndex = 0;
                }
                else
                {
                    objCommon.DisplayMessage("Student with Roll No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                    divCourses.Visible = false;
                    return;

                }
            }
            //}

            // else
            // {
            //     objCommon.DisplayMessage("Student with Roll No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
            //     divCourses.Visible = false;
            //     return;
            // }



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowTRReport(string reportTitle, string rptFileName, string sessionNo, string schemeNo, string semesterNo, string idNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + sessionNo + ",@P_SCHEMENO=" + schemeNo + ",@P_SEMESTERNO=" + semesterNo + ",@P_IDNO=" + idNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion
    private void clear()
    {
        tblInfo.Visible = false;
        txtRollNo.Text = string.Empty;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void ddlElectiveCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlOfferedCourse, "ACD_STUDENT_RESULT R INNER JOIN ACD_COURSE C ON (R.COURSENO=C.COURSENO) AND (R.SCHEMENO=C.SCHEMENO) AND (R.SEMESTERNO=C.SEMESTERNO) AND (R.SUBID=C.SUBID)", "DISTINCT R.COURSENO", "(R.CCODE +'-'+R.COURSENAME) AS COURSENAME", "R.IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND ISNULL(R.CANCEL,0)=0 AND R.SESSIONNO=" + ddlSession.SelectedValue + " AND GROUPNO IN (" + ddlElectiveCourse.SelectedValue + ")", "R.COURSENO");

    }
    protected void ddlOfferedCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSelectCourse, "ACD_COURSE C", "DISTINCT C.COURSENO", "(C.CCODE +'-'+C.COURSE_NAME) AS COURSENAME", "GROUPNO IN (" + ddlElectiveCourse.SelectedValue + ")  AND OFFERED = 1 AND SCHEMENO=" + lblScheme.ToolTip + " AND COURSENO NOT IN(SELECT COURSENO FROM ACD_STUDENT_RESULT WHERE IDNO=" + lblName.ToolTip + " AND SCHEMENO = " + lblScheme.ToolTip + ") and semesterno = " + lblSemester.ToolTip, "C.COURSENO");
    }
}


