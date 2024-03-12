//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : BATCH ALLOTMENT                                  
// CREATION DATE : 
// ADDED BY      : ASHISH DHAKATE                                                  
// ADDED DATE    : 03-Feb-2012
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_BatchAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
                int mul_col_flag = Convert.ToInt32(objCommon.LookUp("REFF", "MUL_COL_FLAG", "CollegeName='" + Page.Title + "'"));
                if (mul_col_flag == 0)
                {
                   // objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COSCHNO>0 AND COLLEGE_ID IN (" + Session["college_nos"].ToString() + ")", "COLLEGE_ID");
                    ddlCollege.SelectedIndex = 1;

                }
                else
                {
                   // objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COSCHNO>0 AND COLLEGE_ID IN (" + Session["college_nos"].ToString() + ")", "COLLEGE_ID");
                    ddlCollege.SelectedIndex = 0;
                }
                PopulateDropDownList();
                ViewState["degreeno"] = 0;
                ViewState["branchno"] = 0;
                ViewState["deptno"] = 0;
                ViewState["schemeno"] = 0;
                ViewState["YearWise"] = 0;
                trFilter.Visible = false;
                trRollNo.Visible = false;
                trRdo.Visible = false;
                ddlBatch.Enabled = false;
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=batchallotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=batchallotment.aspx");
        }
    }

    #region Form Events
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_COURSE", "COURSENO", "CCODE + ' - ' + COURSE_NAME", "OFFERED = 1 AND SCHEMENO = " + ddlBranch.SelectedValue, "CCODE");
            lblStatus2.Text = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_batchallotment.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindListView();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objSC = new StudentController();
            Student_Acd objStudent = new Student_Acd();
            int i = 0;
            foreach (ListViewDataItem lvItem in lvStudents.Items)
            {
                CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
                if (chkBox.Checked == true)
                    objStudent.StudId += chkBox.ToolTip + ",";
                else
                    i++;
            }

            if (i == lvStudents.Items.Count)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please select at least one student from the student list.", this.Page);
                return;
            }

            objStudent.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objStudent.DegreeNo = Convert.ToInt32(ViewState["degreeno"].ToString());
            objStudent.BranchNo = Convert.ToInt32(ViewState["branchno"].ToString());
            objStudent.SchemeNo = Convert.ToInt32(ViewState["schemeno"].ToString());
            objStudent.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            objStudent.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
            objStudent.Sectionno = Convert.ToInt32(ddlSection.SelectedValue);
            objStudent.ThBatchNo = Convert.ToInt32(ddlSubjectType.SelectedValue);
            objStudent.BatchNo = Convert.ToInt32(ddlBatch.SelectedValue);
            objStudent.Pract_Theory = Convert.ToInt32(ddlAttfor.SelectedValue);

            if (objSC.UpdateStudent_BatchAllot(objStudent) == Convert.ToInt32(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Batch Allotted Successfully.", this.Page);
                BindListView();
            }
            else
                objCommon.DisplayMessage(this.UpdatePanel1, "Server Error...", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_batchallotment.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO AND SR.SCHEMENO = C.SCHEMENO)", "DISTINCT SR.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME", "SR.SCHEMENO = " + ViewState["schemeno"].ToString() + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SESSIONNO = " + ddlSession.SelectedValue, "COURSE_NAME");
            ddlCourse.Focus();
            if (ddlSubjectType.SelectedValue == "1")
            {
                ddlAttfor.Items.Clear();
                ListItem newlist = new ListItem("Please Select", "0");
                ddlAttfor.Items.Add(newlist);
            }
            else if (ddlSubjectType.SelectedValue == "2")
            {
                ddlAttfor.Items.Clear();
                ListItem newlist = new ListItem("Please Select", "0");
                ddlAttfor.Items.Add(newlist);
                ListItem newlist1 = new ListItem("Practical", "2");
                ddlAttfor.Items.Add(newlist1);
            }
            else if (ddlSubjectType.SelectedValue == "3")
            {
                ddlAttfor.Items.Clear();
                ListItem newlist = new ListItem("Please Select", "0");
                ListItem newlist1 = new ListItem("Practical", "2");
                ddlAttfor.Items.Add(newlist1);
            }
            else if (ddlSubjectType.SelectedValue == "13")
            {
                ddlAttfor.Items.Clear();
                ListItem newlist = new ListItem("Please Select", "0");
                ddlAttfor.Items.Add(newlist);
                ListItem newlist1 = new ListItem("Tutorial", "3");
                ddlAttfor.Items.Add(newlist1);
            }
            else
            {
                ddlAttfor.Items.Clear();
                ListItem newlist = new ListItem("Please Select", "0");
                ddlAttfor.Items.Add(newlist);
                ListItem newlist3 = new ListItem("Practical", "2");
                ddlAttfor.Items.Add(newlist3);
                ListItem newlist1 = new ListItem("Tutorial", "3");
                ddlAttfor.Items.Add(newlist1);
            }
        }
        else
        {
            ddlCourse.Items.Clear();
            ddlScheme.SelectedIndex = 0;
        }

        lvStudents.DataSource = null;
        lvStudents.DataBind();
        ddlSection.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            if (Session["usertype"].ToString() != "1")
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
            else
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");

            ddlBranch.Focus();
        }
        else
        {
            ddlDegree.SelectedIndex = 0;
        }

        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }

    #endregion

    #region Private Methods

    private void BindListView()
    {
        try
        {
            //Fill Student List
            hdfTot.Value = "0";
            txtTotStud.Text = "0";
            int branchNo = 0;
            if (ViewState["branchno"].ToString() == "99")
                branchNo = 0;
            else
                branchNo = Convert.ToInt32(ViewState["branchno"].ToString());
            StudentController objSC = new StudentController();
            //DataSet ds = objCommon.FillDropDown("ACD_STUDENT ST INNER JOIN ACD_STUDENT_RESULT SR ON ST.IDNO = SR.IDNO", "DISTINCT ST.IDNO", "ST.REGNO,ST.ENROLLNO,  CAST ( CASE WHEN ST.ROLLNO = '' THEN NULL ELSE ST.ROLLNO END  AS INT ) ROLLNO , ST.STUDNAME,(CASE WHEN SR.BATCHNO IS NULL THEN '-' ELSE DBO.FN_DESC('BATCH',SR.BATCHNO) END) AS PR_BATCHNAME,(CASE WHEN SR.TH_BATCHNO  IS NULL  THEN '-' ELSE  DBO.FN_DESC('BATCH',SR.TH_BATCHNO) END) AS TH_BATCHNAME,ST.REGNO, ST.ROLLNO , ST.STUDNAME, (CASE WHEN SR.BATCHNO IS NULL THEN '-' ELSE DBO.FN_DESC('BATCH',SR.BATCHNO) END) AS PR_BATCHNAME, (CASE WHEN SR.TH_BATCHNO  IS NULL  THEN '-' ELSE  DBO.FN_DESC('BATCH',SR.TH_BATCHNO) END) AS TH_BATCHNAME", " SR.SESSIONNO =" + ddlSession.SelectedValue + " AND ST.COLLEGE_ID =" + ddlCollege.SelectedValue + " AND ST.DEGREENO =" + ddlDegree.SelectedValue + " AND ST.BRANCHNO =" + ddlBranch.SelectedValue + " AND SR.SCHEMENO =" + ddlScheme.SelectedValue + " AND SR.SEMESTERNO =" + ddlSemester.SelectedValue + " AND SR.COURSENO =" + ddlCourse.SelectedValue + " AND SR.SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(CANCEL,0) <> 1", (rbRollNo.Checked == true ? "CAST ( CASE WHEN ST.ROLLNO = '' THEN NULL ELSE ST.ROLLNO END  AS INT )" : "ST.REGNO"));
            string sortby = (rbRegNo.Checked == true ? "ST.REGNO" : rbTAN.Checked == true ? "ST.TANNO" : rbAdmno.Checked == true ? "ST.ENROLLNO" : rbRollNo.Checked == true ? "ST.ROLLNO" : "ST.STUDNAME");
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT ST INNER JOIN ACD_STUDENT_RESULT SR ON ST.IDNO = SR.IDNO", "DISTINCT ST.IDNO", "ST.TANNO,ST.REGNO,ST.ENROLLNO,  CAST ( CASE WHEN ST.ROLLNO = '' THEN NULL ELSE ST.ROLLNO END  AS INT ) ROLLNO , ST.STUDNAME,(CASE WHEN SR.BATCHNO IS NULL THEN '-' ELSE DBO.FN_DESC('BATCH',SR.BATCHNO) END) AS PR_BATCHNAME,(CASE WHEN SR.TH_BATCHNO  IS NULL  THEN '-' ELSE  DBO.FN_DESC('BATCH',SR.TH_BATCHNO) END) AS TH_BATCHNAME,ST.REGNO, ST.ROLLNO , ST.STUDNAME, (CASE WHEN SR.BATCHNO IS NULL THEN '-' ELSE DBO.FN_DESC('BATCH',SR.BATCHNO) END) AS PR_BATCHNAME, (CASE WHEN SR.TH_BATCHNO  IS NULL  THEN '-' ELSE  DBO.FN_DESC('BATCH',SR.TH_BATCHNO) END) AS TH_BATCHNAME", " SR.SESSIONNO =" + ddlSession.SelectedValue + " AND ST.COLLEGE_ID =" + ViewState["college_id"].ToString() + " AND ST.DEGREENO =" + ViewState["degreeno"].ToString() + " AND ST.BRANCHNO =" + ViewState["branchno"].ToString() + " AND SR.SCHEMENO =" + ViewState["schemeno"].ToString() + " AND SR.SEMESTERNO =" + ddlSemester.SelectedValue + " AND SR.COURSENO =" + ddlCourse.SelectedValue + " AND SR.SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(CANCEL,0) <> 1", sortby);
         
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                objCommon.FillDropDownList(ddlBatch, "ACD_BATCH", "BATCHNO", "BATCHNAME", "SECTIONNO = " + ddlSection.SelectedValue, "BATCHNO");
                ddlBatch.Enabled = true;
                dvAttFor.Visible = true;
            }
            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                ddlBatch.Enabled = false;
                dvAttFor.Visible = false;
                objCommon.DisplayMessage(this.UpdatePanel1, "Student(s) Not Found!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_batchallotment.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSubjectType.SelectedIndex = 0;
        ddlBatch.SelectedIndex = 0;
        txtFromRollNo.Text = string.Empty;
        txtToRollNo.Text = string.Empty;
        ddlBatch.SelectedIndex = 0;
        txtTotStud.Text = "0";
        hdfTot.Value = "0";
        lblStatus2.Text = string.Empty;
    }

    private void PopulateDropDownList()
    {
        try
        {
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO >0", "SESSIONNO DESC");
            //if (Session["usertype"].ToString() != "1")
            //    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "COLLEGE_NAME", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "C.COLLEGE_ID");
            //else
            //    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_batchallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");

            }
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue + " AND DEGREENO = " + ddlDegree.SelectedValue, "SCHEMENO");
            ddlScheme.Focus();
        }
        else
        {
            ddlScheme.Items.Clear();
            ddlBranch.SelectedIndex = 0;
        }

        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }

    protected void ddlScheme_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue, "SR.SEMESTERNO");
                ddlSemester.Focus();
            }
            else
            {
                ddlSemester.Items.Clear();
                ddlScheme.SelectedIndex = 0;
            }

            lvStudents.DataSource = null;
            lvStudents.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_batchallotment.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ViewState["schemeno"].ToString(), "C.SUBID");
            ddlSubjectType.SelectedIndex = 1;
            ddlSubjectType.Focus();
        }
        else
        {
            ddlSubjectType.Items.Clear();
            ddlSemester.SelectedIndex = 0;
        }

        lvStudents.DataSource = null;
        lvStudents.DataBind();
        ddlSection.SelectedIndex = 0;
        ddlSubjectType.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCourse.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN  ACD_STUDENT ST ON SR.IDNO = ST.IDNO INNER JOIN  ACD_SECTION S ON (SR.SECTIONNO = S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.COURSENO = " + ddlCourse.SelectedValue, "S.SECTIONNO");
            ddlSection.Focus();
        }
        else
        {
            ddlSection.Items.Clear();
            ddlCourse.SelectedIndex = 0;
        }

        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }

    #endregion

    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        hdfTot.Value = (Convert.ToInt16(hdfTot.Value) + 1).ToString();
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollege.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO >0 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO DESC");
            ddlSession.Focus();           
        }
        else
        {
            objCommon.DisplayMessage("Please Select College", this.Page);
            ddlCollege.SelectedIndex = 0;
            ddlSession.SelectedIndex = 0;
            ddlCollege.Focus();
        }

        //if (Session["usertype"].ToString() != "1")
        //{
        //    string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
        //    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
        //}
        //else
        //{
        //    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENO");
        //}
        //ddlDegree.Focus();
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("BatchAllotmentList", "rptBatchAllotmentList.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGEID=" + ViewState["college_id"].ToString() + ",@P_DEGREENO=" + ViewState["degreeno"].ToString() + ",@P_BRANCHNO=" + ViewState["branchno"].ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"].ToString()) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_SECTIONNO=" + ddlSection.SelectedValue;

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            ViewState["YearWise"] = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "ISNULL(YEARWISE,0)YEARWISE", "DEGREENO=" + ViewState["degreeno"].ToString()));
            if (ViewState["YearWise"].ToString() == "1")
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR<>0", "YEAR");
            }
            else
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO<>0", "SEMESTERNO");
            }
            ddlSemester.Focus();
        }
        else
        {
            ddlSemester.SelectedIndex = 0;
            objCommon.DisplayMessage("Please Select Session", this.Page);
            ddlSession.Focus();
        }
    }
}
