//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : COURSE ALLOTMENT                                      
// CREATION DATE : 15-OCT-2011
// CREATED BY    :                                                 
// MODIFIED DATE : 
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_teacherallotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string deptno = string.Empty;

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
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }
                //    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0", "SESSIONNO desc");
                // ddlSession.SelectedIndex = 0;
                int mul_col_flag = Convert.ToInt32(objCommon.LookUp("REFF", "MUL_COL_FLAG", "CollegeName='" + Session["coll_name"].ToString() + "'"));
                if (mul_col_flag == 0)
                {
                    // objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0", "COLLEGE_ID");
                    ddlCollege.SelectedIndex = 1;
                }
                else
                {
                    // objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0", "COLLEGE_ID");
                    ddlCollege.SelectedIndex = 0;
                }
                ViewState["degreeno"] = 0;
                ViewState["branchno"] = 0;
                ViewState["college_id"] = 0;
                ViewState["schemeno"] = 0;
                ViewState["YearWise"] = 0;

                this.PopulateDropDownList();
            }
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            if (Session["usertype"].ToString() != "1")
            {
                string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());

                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID IN (" + Session["college_nos"].ToString() + ") AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
            }
            else
            {
                ViewState["DEPTNO"] = "0";
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID IN (" + Session["college_nos"].ToString() + ")", "D.DEGREENO");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
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
                Response.Redirect("~/notauthorized.aspx?page=teacherallotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=teacherallotment.aspx");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            //if (Session["usertype"].ToString() != "1")
            //    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
            //else
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");

            DataSet ds = objCommon.FillDropDown("ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
            string BranchNos = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                BranchNos += ds.Tables[0].Rows[i]["BranchNo"].ToString() + ",";
            }
            DataSet dsReff = objCommon.FillDropDown("REFF", "*", "", string.Empty, string.Empty);
            //on faculty login to get only those dept which is related to logged in faculty
            objCommon.FilterDataByBranch(ddlBranch, dsReff.Tables[0].Rows[0]["FILTER_USER_TYPE"].ToString(), BranchNos, Convert.ToInt32(dsReff.Tables[0].Rows[0]["BRANCH_FILTER"].ToString()), Convert.ToInt32(Session["usertype"]));
            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlDegree.SelectedIndex = 0;
        }

        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue + "AND DEGREENO= " + ddlDegree.SelectedValue, "SCHEMENO DESC");
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

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {

                //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND ISNULL(SR.PREV_STATUS,0) = 0", "SR.SEMESTERNO");
                //bind semester
                ddlSemester.Items.Clear();
                string odd_even = objCommon.LookUp("acd_session_master", "odd_Even", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue));
                string exam_type = objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                string semCount = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "CAST(DURATION AS INT)*2 AS DURATION", "DEGREENO=" + ddlDegree.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + "");
                if (exam_type == "1" && odd_even != "3")
                {
                    objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.ODD_EVEN=" + odd_even + "AND SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNO");
                }
                else
                {
                    objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNO");

                }
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

        //try
        //{
        //    if (ddlScheme.SelectedIndex > 0)
        //    {
        //        objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
        //        ddlSemester.Focus();
        //    }
        //    else
        //    {
        //        ddlSemester.Items.Clear();
        //        ddlScheme.SelectedIndex = 0;
        //    }
        //    lvStudents.DataSource = null;
        //    lvStudents.DataBind();
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "Academic_batchallotment.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ViewState["schemeno"].ToString(), "C.SUBID");
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
        ddlTeacher.SelectedIndex = 0;
    }

    protected void ddlSubjectType_SelectedIndexChanged1(object sender, EventArgs e)
    {

        try
        {
            if (ddlSubjectType.SelectedIndex > 0)
            {
                // The value '100' is used for the Tutorials
                if (ddlSubjectType.SelectedValue == "100")
                {
                    objCommon.FillDropDownList(ddlCourse, "ACD_OFFERED_COURSE F INNER JOIN ACD_COURSE C ON(C.COURSENO=F.COURSENO) ", "F.COURSENO", "(F.CCODE + ' - ' +C. COURSE_NAME) COURSE_NAME ", "SESSIONNO=" + ddlSession.SelectedValue + " AND  f.SCHEMENO = " + ViewState["schemeno"].ToString() + " AND c.SUBID = 1" + " AND F.SEMESTERNO = " + ddlSemester.SelectedValue + " AND THEORY = 1", "f.CCODE");
                }
                else
                {
                    //if (Convert.ToInt32(ddlSubjectType.SelectedValue) == 2)
                    //{
                    //    objCommon.FillDropDownList(ddlCourse, "ACD_OFFERED_COURSE F INNER JOIN ACD_COURSE C ON(C.COURSENO=F.COURSENO) ", "F.COURSENO", "(F.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME ", "SESSIONNO=" + ddlSession.SelectedValue + " AND  f.SCHEMENO = " + ddlScheme.SelectedValue + " AND c.SUBID = " + ddlSubjectType.SelectedValue + " AND F.SEMESTERNO = " + ddlSemester.SelectedValue, "f.CCODE");
                    //}
                    //else
                    //{
                    //    objCommon.FillDropDownList(ddlCourse, "ACD_OFFERED_COURSE F INNER JOIN ACD_COURSE C ON(C.COURSENO=F.COURSENO)", "F.COURSENO", "(F.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME ", "SESSIONNO=" + ddlSession.SelectedValue + " AND  f.SCHEMENO = " + ddlScheme.SelectedValue + " AND c.SUBID = " + ddlSubjectType.SelectedValue + " AND F.SEMESTERNO = " + ddlSemester.SelectedValue, "f.CCODE");
                    //}
                    objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON (C.COURSENO = SR.COURSENO) INNER JOIN ACD_OFFERED_COURSE O ON O.COURSENO = SR.COURSENO AND O.SESSIONNO = SR.SESSIONNO AND O.SEMESTERNO = SR.SEMESTERNO AND O.SCHEMENO = SR.SCHEMENO", "DISTINCT C.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME", "SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + " AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SR.SCHEMENO=" + ViewState["schemeno"].ToString(), "C.COURSENO");
                }

                ddlCourse.Focus();
                if (ddlSubjectType.SelectedValue == "1")
                {
                    ddltheorypractical.Items.Clear();
                    ListItem newlist = new ListItem("Please Select", "0");
                    ddltheorypractical.Items.Add(newlist);
                    ListItem newlist1 = new ListItem("Theory", "1");
                    ddltheorypractical.Items.Add(newlist1);
                }
                else if (ddlSubjectType.SelectedValue == "2")
                {
                    ddltheorypractical.Items.Clear();
                    ListItem newlist = new ListItem("Please Select", "0");
                    ddltheorypractical.Items.Add(newlist);
                    ListItem newlist1 = new ListItem("Practical", "2");
                    ddltheorypractical.Items.Add(newlist1);
                }
                else if (ddlSubjectType.SelectedValue == "3")
                {
                    ddltheorypractical.Items.Clear();
                    ListItem newlist = new ListItem("Please Select", "0");
                    ddltheorypractical.Items.Add(newlist);
                    ListItem newlist2 = new ListItem("Theory", "1");
                    ddltheorypractical.Items.Add(newlist2);
                    ListItem newlist1 = new ListItem("Practical", "2");
                    ddltheorypractical.Items.Add(newlist1);
                }
                else if (ddlSubjectType.SelectedValue == "4")
                {
                    ddltheorypractical.Items.Clear();
                    ListItem newlist = new ListItem("Please Select", "0");
                    ddltheorypractical.Items.Add(newlist);
                    ListItem newlist1 = new ListItem("Unaudit", "4");
                    ddltheorypractical.Items.Add(newlist1);
                }
                else if (ddlSubjectType.SelectedValue == "13")
                {
                    ddltheorypractical.Items.Clear();
                    ListItem newlist = new ListItem("Please Select", "0");
                    ddltheorypractical.Items.Add(newlist);
                    ListItem newlist2 = new ListItem("Theory", "1");
                    ddltheorypractical.Items.Add(newlist2);
                    ListItem newlist1 = new ListItem("Tutorial", "3");
                    ddltheorypractical.Items.Add(newlist1);
                }
                else
                {
                    ddltheorypractical.Items.Clear();
                    ListItem newlist = new ListItem("Please Select", "0");
                    ddltheorypractical.Items.Add(newlist);
                    ListItem newlist2 = new ListItem("Theory", "1");
                    ddltheorypractical.Items.Add(newlist2);
                    ListItem newlist3 = new ListItem("Practical", "2");
                    ddltheorypractical.Items.Add(newlist3);
                    ListItem newlist1 = new ListItem("Tutorial", "3");
                    ddltheorypractical.Items.Add(newlist1);
                }
            }
            else
            {
                ddlCourse.Items.Clear();
                ddlSubjectType.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_TIMETABLE_TimeTable.ShowRegisteredCourses-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        #region commented
        //if (ddlSemester.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(LTRIM(RTRIM(SR.CCODE)) + ' - ' + LTRIM(RTRIM(C.COURSE_NAME)))COURSE_NAME", "SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
        //    ddlCourse.Focus();
        //    if (ddlSubjectType.SelectedValue == "1")
        //    {
        //        ddltheorypractical.Items.Clear();
        //        ListItem newlist = new ListItem("Please Select", "0");
        //        ddltheorypractical.Items.Add(newlist);
        //        ListItem newlist1 = new ListItem("Theory", "1");
        //        ddltheorypractical.Items.Add(newlist1);

        //    }
        //    else if (ddlSubjectType.SelectedValue == "2")
        //    {
        //        ddltheorypractical.Items.Clear();
        //        ListItem newlist = new ListItem("Please Select", "0");
        //        ddltheorypractical.Items.Add(newlist);
        //        ListItem newlist1 = new ListItem("Practical", "2");
        //        ddltheorypractical.Items.Add(newlist1);
        //    }
        //    else if (ddlSubjectType.SelectedValue == "3")
        //    {
        //        ddltheorypractical.Items.Clear();
        //        ListItem newlist = new ListItem("Please Select", "0");
        //        ddltheorypractical.Items.Add(newlist);
        //        ListItem newlist2 = new ListItem("Theory", "1");
        //        ddltheorypractical.Items.Add(newlist2);
        //        ListItem newlist1 = new ListItem("Practical", "2");
        //        ddltheorypractical.Items.Add(newlist1);
        //    }
        //    else if (ddlSubjectType.SelectedValue == "13")
        //    {
        //        ddltheorypractical.Items.Clear();
        //        ListItem newlist = new ListItem("Please Select", "0");
        //        ddltheorypractical.Items.Add(newlist);
        //        ListItem newlist2 = new ListItem("Theory", "1");
        //        ddltheorypractical.Items.Add(newlist2);
        //        ListItem newlist1 = new ListItem("Tutorial", "3");
        //        ddltheorypractical.Items.Add(newlist1);
        //    }
        //    else
        //    {
        //        ddltheorypractical.Items.Clear();
        //        ListItem newlist = new ListItem("Please Select", "0");
        //        ddltheorypractical.Items.Add(newlist);
        //        ListItem newlist2 = new ListItem("Theory", "1");
        //        ddltheorypractical.Items.Add(newlist2);
        //        ListItem newlist3 = new ListItem("Practical", "2");
        //        ddltheorypractical.Items.Add(newlist3);
        //        ListItem newlist1 = new ListItem("Tutorial", "3");
        //        ddltheorypractical.Items.Add(newlist1);
        //    }
        //}
        //else
        //{
        //    ddlCourse.Items.Clear();
        //    ddlScheme.SelectedIndex = 0;
        //}

        //lvStudents.DataSource = null;
        //lvStudents.DataBind();
        //ddlSection.SelectedIndex = 0;
        //ddlCourse.SelectedIndex = 0;
        //ddlTeacher.SelectedIndex = 0;
        #endregion
    }

    protected void ddlCourse_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlCourse.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN  ACD_SECTION S ON (SR.SECTIONNO = S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.COURSENO = " + ddlCourse.SelectedValue, "S.SECTIONNO");
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

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        dvBatch.Visible = false;
        //rfvBatch.Visible = false;
        ddltheorypractical.SelectedIndex = 0;
    }

    protected void ddlbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }

    protected void ddltheorypractical_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltheorypractical.SelectedValue == "2" || ddltheorypractical.SelectedValue == "3")
        {
            dvBatch.Visible = true;
            rfvBatch.Visible = true;

            if (ddlSection.SelectedIndex > 0 && ddltheorypractical.SelectedValue != "1")
            {
                if (ddltheorypractical.SelectedValue == "2")
                {
                    objCommon.FillDropDownList(ddlBatch, "ACD_STUDENT_RESULT SR INNER JOIN ACD_BATCH B ON (B.BATCHNO = SR.BATCHNO)", "DISTINCT ISNULL(B.BATCHNO,0)BATCHNO", "B.BATCHNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.COURSENO = " + ddlCourse.SelectedValue + " AND B.BATCHNO>0 AND SR.SECTIONNO =" + ddlSection.SelectedValue, "BATCHNO");

                }
                else
                {
                    objCommon.FillDropDownList(ddlBatch, "ACD_STUDENT_RESULT SR INNER JOIN ACD_BATCH B ON (B.BATCHNO = SR.TH_BATCHNO)", "DISTINCT ISNULL(B.BATCHNO,0)BATCHNO", "B.BATCHNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.COURSENO = " + ddlCourse.SelectedValue + " AND B.BATCHNO>0 AND SR.SECTIONNO =" + ddlSection.SelectedValue, "BATCHNO");
                }
                ddlBatch.Focus();
            }
            else
            {
                ddlSection.Items.Clear();
                ddlSection.SelectedIndex = 0;
                ddlBatch.Items.Clear();
                ddlBatch.SelectedIndex = 0;
            }
        }
        else
        {
            dvBatch.Visible = false;
            rfvBatch.Visible = false;
        }

        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }
 //protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{

    //}
    //protected void lvStudents_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{

    //}

    private void FillTeacher()
    {

        objCommon.FillDropDownList(ddlTeacher, "ACD_COURSE_TEACHER CT INNER JOIN USER_ACC UA ON (CT.UA_NO=UA.UA_NO)", "DISTINCT CT.UA_NO", "UA.UA_FULLNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + " AND CT.SEMESTERNO =" + ddlSemester.SelectedValue + " AND CT.SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(CT.CANCEL,0)=0 AND CT.SCHEMENO =" + ViewState["schemeno"].ToString() + " AND CT.th_pr =" + ddltheorypractical.SelectedValue, "UA.UA_FULLNAME");


    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.BindListView();
    }

    private void BindListView()
    {
        try
        {
            //Fill Teacher DropDown
            this.FillTeacher();

            StudentController objSC = new StudentController();
            DataSet ds = new DataSet();

            ds = objSC.GetStudentsForTeacherAllotment(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddltheorypractical.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue), Convert.ToInt32(rbSortBy.SelectedValue));

            if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
            }
            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                objCommon.DisplayMessage(this.UpdatePanel1, "No Record Found for Current Selection!!", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_teacherallotment.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void rbRollNo_CheckedChanged(object sender, EventArgs e)
    {
        this.BindListView();
    }

    protected void rbUSNNo_CheckedChanged(object sender, EventArgs e)
    {
        this.BindListView();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        ViewState["degreeno"] = 0;
        ViewState["branchno"] = 0;
        ViewState["college_id"] = 0;
        ViewState["schemeno"] = 0;
        ViewState["YearWise"] = 0;
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //Validations
            if (ddlTeacher.SelectedIndex <= 0)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Teacher", this.Page);
                return;
            }

            StudentController objSC = new StudentController();
            Student_Acd objStudent = new Student_Acd();
            objStudent.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objStudent.SchemeNo = Convert.ToInt32(ViewState["schemeno"].ToString());
            objStudent.Sem = ddlSemester.SelectedValue;
            objStudent.Sectionno = Convert.ToInt32(ddlSection.SelectedValue);
            objStudent.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
            objStudent.UA_No = Convert.ToInt32(ddlTeacher.SelectedValue);
            objStudent.Th_Pr = Convert.ToInt32(ddltheorypractical.SelectedValue);

            foreach (ListViewDataItem lvItem in lvStudents.Items)
            {
                CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
                if (chkBox.Checked == true)
                    objStudent.StudId += chkBox.ToolTip + ",";
            }

            if (objStudent.StudId.Length <= 0)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Student", this.Page);
                return;
            }

            if (objSC.UpdateStudent_TeachAllot(objStudent) == Convert.ToInt32(CustomStatus.RecordUpdated))
                objCommon.DisplayMessage(this.UpdatePanel1, "Teacher Alloted Sucessfully..", this.Page);
            else
                objCommon.DisplayMessage(this.UpdatePanel1, "Server Error", this.Page);

            this.BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_teacherallotment.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
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
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO DESC");
            }
        }
        else
        {
            ddlSession.SelectedIndex = 0;
            objCommon.DisplayMessage("Please Select College & Curriculum", this.Page);
            ddlCollege.Focus();
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex > 0)
            {
                ddlSemester.Items.Clear();
                ViewState["YearWise"] = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "ISNULL(YEARWISE,0)YEARWISE", "DEGREENO=" + ViewState["degreeno"].ToString()));
                if (ViewState["YearWise"].ToString() == "1")
                {
                    objCommon.FillDropDownList(ddlSemester, "ACD_YEAR", "SEM", "YEARNAME", "YEAR<>0", "YEAR");
                }
                else
                {
                    string odd_even = objCommon.LookUp("acd_session_master", "odd_Even", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue));
                    string exam_type = objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                    string semCount = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "CAST(CAST(DURATION AS NUMERIC(12,1))*2 AS INT) AS DURATION", "DEGREENO=" + ViewState["degreeno"].ToString() + " AND BRANCHNO=" + ViewState["branchno"].ToString() + "");
                    if (exam_type == "1" && odd_even != "3")
                    {
                        objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.ODD_EVEN=" + odd_even + "AND SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNO");
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNO");

                    }
                }
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
                objUCommon.ShowError(Page, "Academic_teacherallotment.ddlSession_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
}
