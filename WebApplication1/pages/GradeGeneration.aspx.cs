//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : STUDENT COURSE REGISTRATION REPORT                                   
// CREATION DATE : 22-AUG-2011                                                       
// CREATED BY    :                                                    
// MODIFIED DATE : 20-AUG-2012 
// MODIFIED BY   : Pawan Mourya                                                                     
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Data;
using System.Web.UI;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;

public partial class CourseWise_Registration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    //ConnectionString
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                PopulateDropDown();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

            }
            divMsg.InnerHtml = string.Empty;
            btn_Process.Enabled = lbnt_Lock.Enabled = lbtn_Print.Enabled = false;
            ddlSession.Focus();
        }
        //Blank Div
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DecodingGeneration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DecodingGeneration.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
            // Fill Semester Dropdown
            objCommon.FillDropDownList(ddlCollegeName, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            //Fill Dropdown semester
            try
            {
                if (ddlScheme.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
                    ddlSemester.Focus();
                }
                else
                {
                    ddlSemester.Items.Clear();
                    ddlScheme.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Academic_batchallotment.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        ddlScheme.Focus();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Refresh Page url
        Response.Redirect(Request.Url.ToString());
    }

    //private void ShowReport(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_SECTIONNO=" + ddlSection.SelectedValue + ",@P_SUBID="+ ddlSubjectType.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue;
    //        //To open new window from Updatepanel
    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");
    //        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    //private void ShowStudentListReport(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        //Check record found or not
    //        string count = string.Empty;
    //        if (rdbReport.SelectedValue == "3")
    //            count = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT SR ON(S.IDNO = SR.IDNO)", "COUNT(DISTINCT S.IDNO)", "SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SR.SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND S.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue));
    //        else
    //            count = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT SR ON(S.IDNO = SR.IDNO)", "COUNT(DISTINCT S.IDNO)", "SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SR.SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND S.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND S.PRO=1");

    //        if (count == "0")
    //        {
    //            objCommon.DisplayMessage(this.UpdatePanel1, "Record Not Found!!", this.Page);
    //        }
    //        else
    //        {
    //            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //            url += "Reports/CommonReport.aspx?";
    //            url += "pagetitle=" + reportTitle;
    //            url += "&path=~,Reports,Academic," + rptFileName;
    //            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_SECTIONNO=" + ddlSection.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue;
    //            //To open new window from Updatepanel
    //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //            sb.Append(@"window.open('" + url + "','','" + features + "');");
    //            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    private void BindListView()
    {
        try
        {
            StudentController objSC = new StudentController();
            DataSet ds = objSC.GetCoursewiseStudentsCount(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue));//,Convert.ToInt32(ddlSemester.SelectedValue),Convert.ToInt32(ddlSchemeType.SelectedValue),Convert.ToInt32(ddlSection.SelectedValue));

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //lvStudents.DataSource = ds;
                //lvStudents.DataBind();
            }
            else
            {
                //lblStatus.Text = "No Students for selected criteria";
                //lvStudents.DataSource = null;
                //lvStudents.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
    }

    protected void ddlCollegeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindListView();
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON A.DEGREENO=B.DEGREENO INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO = A.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "B.COLLEGE_ID=" + ddlCollegeName.SelectedValue + "AND B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND B.COLLEGE_ID > 0", "A.DEGREENO");
        ddlCollegeName.Focus();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Fill Dropdown Scheme
        if (ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO");
            ddlScheme.Focus();
        }
        else
        {
            ddlScheme.Items.Clear();
            ddlBranch.SelectedIndex = 0;
        }

        ddlBranch.Focus();
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        // fill branch according degree selection
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.COLLEGE_ID=" + ddlCollegeName.SelectedValue, "A.LONGNAME");

            DataSet ds = objCommon.FillDropDown("ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
            string BranchNos = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                BranchNos += ds.Tables[0].Rows[i]["BranchNo"].ToString() + ",";
            }
            DataSet dsReff = objCommon.FillDropDown("REFF", "*", "", string.Empty, string.Empty);
            //on faculty login to get only those dept which is related to logged in faculty
            objCommon.FilterDataByBranch(ddlBranch, dsReff.Tables[0].Rows[0]["FILTER_USER_TYPE"].ToString(), BranchNos, Convert.ToInt32(dsReff.Tables[0].Rows[0]["BRANCH_FILTER"].ToString()), Convert.ToInt32(Session["usertype"]));
            ddlDegree.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlDegree.SelectedIndex = 0;
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
            ddlCourse.Focus();
        }
        else
        {
            ddlCourse.Items.Clear();
            ddlScheme.SelectedIndex = 0;
        }

        ddlSection.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;

        ddlSemester.Focus();

        //if (ddlSemester.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO >0", "C.SUBID");
        //    ddlSubjectType.Focus();
        //}
        //else
        //{
        //    ddlSubjectType.Items.Clear();
        //    ddlSemester.SelectedIndex = 0;
        //}

        //ddlSection.SelectedIndex = 0;
        //ddlSubjectType.SelectedIndex = 0;
        //ddlCourse.SelectedIndex = 0;

    }

    //protected void ddlSubjectType_SelectedIndexChanged1(object sender, EventArgs e)
    //{
    //    if (ddlSemester.SelectedIndex > 0)
    //    {
    //        objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
    //        ddlCourse.Focus();
    //    }
    //    else
    //    {
    //        ddlCourse.Items.Clear();
    //        ddlScheme.SelectedIndex = 0;
    //    }

    //    ddlSection.SelectedIndex = 0;
    //    ddlCourse.SelectedIndex = 0;

    //}

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR,ACD_SECTION SEC", "DISTINCT SR.SECTIONNO", "SEC.SECTIONNAME", "SEC.SECTIONNO=SR.SECTIONNO AND  SEC.SECTIONNO > 0 AND  SR.SCHEMENO=" + ddlScheme.SelectedValue + "AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "AND SR.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SR.SESSIONNO=" + ddlSession.SelectedValue + "", "SEC.SECTIONNAME");
        ddlCourse.Focus();
        div_Result.Visible = div1_Alert.Visible = false;
    }

    private void ShowCourseStudentCountReport(string reportTitle, string rptFileName, int type)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SECTIONNO=" + ddlSection.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",@P_TYPE=" + type;
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowCourseRegistration(string reportTitle, string rptFileName)
    {
        try
        {
            string count = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT SR ON(S.IDNO = SR.IDNO)", "COUNT(DISTINCT S.IDNO)", "SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND  SR.SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND S.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue));
            if (count == "0")
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Record Not Found!!", this.Page);
            }
            else
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
             // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTER_NO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SECTIONNO=" + ddlSection.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",username=" + Session["username"].ToString();
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTER_NO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SECTIONNO=" + ddlSection.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

            }
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
        ddlSession.Focus();
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSection.Focus();
        div_Result.Visible = div1_Alert.Visible = false;
    }
    protected void lbtn_Done_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex == 0) { ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Session !!');", true); return; }
            else if (ddlCollegeName.SelectedIndex == 0) { ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select College / School Name !!');", true); return; }
            else if (ddlDegree.SelectedIndex == 0) { ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Degree !!');", true); return; }
            else if (ddlBranch.SelectedIndex == 0) { ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Branch !!');", true); return; }
            else if (ddlScheme.SelectedIndex == 0) { ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Regulation !!');", true); return; }
            else if (ddlSemester.SelectedIndex == 0) { ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select Semester!!');", true); return; }
            string SP_Name = "PKG_GRADE_GENERATION";
            string SP_Parameters = "@P_SESSIONNO,@P_SCHEMENO,@P_COURSENO,@P_SEMESTERNO,@P_SECTIONNO,@P_UA_NO,@P_OPERATION,@P_OUT";
            string Call_Values = "" + ddlSession.SelectedValue + "," + ddlScheme.SelectedValue + "," + ddlCourse.SelectedValue + "," + ddlSemester.SelectedValue + "," + ddlSection.SelectedValue + "," + Convert.ToInt32(Session["userno"].ToString()) + ",2,0";

            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count != 0)
            {
                if (ds.Tables[0].Columns.Count == 3)
                {
                    string DynamicTable = @"
                                            <div class='row' style='margin-bottom:15px'>
                                                <div class='col-md-12'>
                                                    <h3>
                                                        <span class='label label-default pull-left'>Marks not Locked !!</span> 
                                                    </h3>
                                                </div>
                                            </div>
				                            <table class='table table-hover table-bordered myCss'>
					                            <thead style='background-color:#3c8dbc;color:white;' class='sticky'>
						                            <tr>
						                            <th>Subject Name</th>
                                                    <th><center>Sec.</center></th>
						                            <th>Teacher Name</th>
						                            <th>Teacher Mobile No.</th>
						                            <th>Exam Name</th>
						                            <th>Exam Lock Date</th>
						                            </tr>
					                            </thead>
					                            <tbody>
					                        ";
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        DynamicTable += ds.Tables[0].Rows[i][2].ToString();
                    }

                    DynamicTable += @"</tbody></table>";

                    div1_Alert.InnerHtml = DynamicTable;
                    div_Result.Visible = false;
                    div1_Alert.Visible = true;
                    btn_Process.Enabled = lbnt_Lock.Enabled = false;
                }
                else
                {
                    rpt_Success.DataSource = ds.Tables[0];
                    rpt_Success.DataBind();
                    div_Result.Visible = true;
                    div1_Alert.Visible = false;
                    lbl_cnt.Text = ds.Tables[0].Rows.Count.ToString();
                    
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["GRADE_LOCK_STATUS"]) == 1)
                    {
                        btn_Process.Enabled = lbnt_Lock.Enabled = false;
                        lbtn_Print.Enabled = true;
                    }
                    else if (Convert.ToInt32(ds.Tables[0].Rows[0]["GRADE_LOCK_STATUS"]) == 0)
                    {
                        btn_Process.Enabled = lbnt_Lock.Enabled = true;
                        lbtn_Print.Enabled = false;
                    }

                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["STYPE"]) == 2)
                    {
                        string src = @"$('.h_cat3').hide();$('.v_cat3').hide();";
                        ScriptManager.RegisterStartupScript(this, GetType(), "key", src, true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", "alert('No Record Found !!!');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", "alert('Something wents Wrong !!!');", true);
            lbnt_Lock.Enabled = false;
        }
    }
    protected void lbtn_Cancel_Click(object sender, EventArgs e)
    {
        div_Result.Visible = div1_Alert.Visible = false;
        ddlSession.SelectedIndex = ddlCollegeName.SelectedIndex = ddlDegree.SelectedIndex = ddlBranch.SelectedIndex = ddlScheme.SelectedIndex = ddlSemester.SelectedIndex = ddlCourse.SelectedIndex = ddlSection.SelectedIndex = 0;
        btn_Process.Enabled = lbnt_Lock.Enabled = lbtn_Print.Enabled = false;
        ddlSession.Focus();
    }
    protected void lbtn_Print_Click(object sender, EventArgs e)
    {
        string reportTitle = "GradeReport";
        string rptFileName = "GradeReport.rpt";

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + ",@P_OPERATION=2,@P_OUT=0,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";

            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "key", Print_Val, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btn_Process_Click(object sender, EventArgs e)
    {
        try
        {
            string SP_Name = "PKG_GRADE_GENERATION";
            string SP_Parameters = "@P_SESSIONNO,@P_SCHEMENO,@P_COURSENO,@P_SEMESTERNO,@P_SECTIONNO,@P_UA_NO,@P_OPERATION,@P_OUT";
            string Call_Values = "" + ddlSession.SelectedValue + "," + ddlScheme.SelectedValue + "," + ddlCourse.SelectedValue + "," + ddlSemester.SelectedValue + "," + ddlSection.SelectedValue + "," + Convert.ToInt32(Session["userno"].ToString()) + ",1,0";

            string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true,2);

            if (que_out == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", "alert('Grade Saved Successfully.');", true);
                lbnt_Lock.Enabled = true;
            }
            else if (que_out == "9")
            {
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", "alert('Grade Updated Successfully.');", true);
                lbnt_Lock.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", "alert('Something wents Wrong !!');", true);
            lbnt_Lock.Enabled = false;
        }
    }
    protected void lbnt_Lock_Click(object sender, EventArgs e)
    {
        string SP_Name = "PKG_GRADE_GENERATION";
        string SP_Parameters = "@P_SESSIONNO,@P_SCHEMENO,@P_COURSENO,@P_SEMESTERNO,@P_SECTIONNO,@P_UA_NO,@P_OPERATION,@P_OUT";
        string Call_Values = "" + ddlSession.SelectedValue + "," + ddlScheme.SelectedValue + "," + ddlCourse.SelectedValue + "," + ddlSemester.SelectedValue + "," + ddlSection.SelectedValue + "," + Convert.ToInt32(Session["userno"].ToString()) + ",3,0";

        string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true,2);

        if (que_out == "2")
        {
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", "alert('Grade Locked Successfully ...');", true);
        }
        //lbnt_Lock.Enabled = btn_Process.Enabled = false;

        lbtn_Done_Click(sender, e);
    }

}

