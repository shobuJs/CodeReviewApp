//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACADEMIC
// PAGE NAME     : COURSE CREATION                                                 
// CREATION DATE : 21-May-2009
// CREATED BY    : NIRAJ D. PHALKE & ASHWINI BARBATE                               
// MODIFIED DATE : 10-NOV-2010
// MODIFIED BY   : MANGESH MOHATKAR
// MODIFIED DESC : 
//=================================================================================

using System.Linq;
using System.Web;
using System.Xml.Linq;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Web.Configuration;



public partial class Administration_courseMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //ConnectionStrings
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    #region Page Events
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
                //Populate the DropDownList 
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
                lvCourseMaterial.DataSource = null;
                lvCourseMaterial.DataBind();
                ViewState["action"] = "add";
                trbtn.Visible = false;
            }
        }
        divMsg.InnerHtml = string.Empty;
    }
    #endregion

    #region Other Events
    // bind department on degree selection
    protected void ddlDegree_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                if (Session["usertype"].ToString() != "1")
                    objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", " DISTINCT D.DEPTNO", "D.DEPTNAME", "D.DEPTNO>0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO=" + Session["userdeptno"].ToString(), "D.DEPTNAME");
                else
                    objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", " DISTINCT D.DEPTNO", "D.DEPTNAME", "D.DEPTNO>0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "D.DEPTNAME");
            }
            else
            {
                ddlDept.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_courseMaster.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // bind Branch on department selection
    protected void ddlDept_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            rtpScheme.DataBind();
            if (ddlDept.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", "CB.BRANCHNO> 0 AND  CB.DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + "AND CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
            }
            else
                ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_courseMaster.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // bind scheme on branch selection
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Course objc = new Course();
        if (ddlBranch.SelectedValue != "0")
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue), string.Empty);
        }
        else
        {
            rtpScheme.DataSource = null;
            rtpScheme.DataBind();
        }
    }

    //Modified by Neha 20/06/19
    protected void ddlTP_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
            string[] ex1 = ddlScheme.SelectedValue.Split('-');
            int patternno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "ISNULL(PATTERNNO,0)", "schemeno='" + ddlScheme.SelectedValue + "'"));
            //int schemetype = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "ISNULL(SCHEMETYPE,0)", "patternno='" + patternno + "'"));
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_PATTERNNO", patternno);
            DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_ALL_EXAM", objParams);

            if (ds.Tables[0].Rows.Count > 0)
            {
                rtpScheme.DataSource = ds;
                rtpScheme.DataBind();
                if (ddlTP.SelectedItem.Text == "THEORY")
                {
                    for (int i = 0; i < rtpScheme.Items.Count; i++)
                    {
                        var GetTheoryorPractical = ds.Tables[0].Rows[i]["EXAMNAME"].ToString().Substring(ds.Tables[0].Rows[0]["EXAMNAME"].ToString().Length - 2);
                        if (GetTheoryorPractical == "TH")
                        {
                            TextBox txtMinMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMinMarks");
                            TextBox txtMaxMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMaxMarks");
                            txtMinMarks.ReadOnly = false;
                            txtMaxMarks.ReadOnly = false;
                        }
                        if (GetTheoryorPractical == "PR")
                        {
                            TextBox txtMinMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMinMarks");
                            TextBox txtMaxMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMaxMarks");
                            txtMinMarks.ReadOnly = true;
                            txtMaxMarks.ReadOnly = true;
                        }
                        else
                        {
                            TextBox txtMinMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMinMarks");
                            TextBox txtMaxMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMaxMarks");
                            txtMinMarks.ReadOnly = false;
                            txtMaxMarks.ReadOnly = false;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < rtpScheme.Items.Count; i++)
                    {
                        var GetTheoryorPractical = ds.Tables[0].Rows[i]["EXAMNAME"].ToString().Substring(ds.Tables[0].Rows[0]["EXAMNAME"].ToString().Length - 2);
                        if (GetTheoryorPractical == "PR")
                        {
                            TextBox txtMinMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMinMarks");
                            TextBox txtMaxMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMaxMarks");
                            txtMinMarks.ReadOnly = false;
                            txtMaxMarks.ReadOnly = false;
                        }
                        if (GetTheoryorPractical == "TH")
                        {
                            TextBox txtMinMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMinMarks");
                            TextBox txtMaxMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMaxMarks");
                            txtMinMarks.ReadOnly = true;
                            txtMaxMarks.ReadOnly = true;
                        }
                        else
                        {
                            TextBox txtMinMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMinMarks");
                            TextBox txtMaxMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMaxMarks");
                            txtMinMarks.ReadOnly = false;
                            txtMaxMarks.ReadOnly = false;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_courseMaster.ddlTP_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // bind Scheme
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetExamName();
        ddlExtCourse.Enabled = true;
        // bind  Existing Courses
        FillDropDownCourse();

        //this.BindCourseList(Convert.ToInt32(ddlScheme.SelectedValue), "0");//, Convert.ToInt32(ddlSem.SelectedValue)

        // visible Existing Courses dropdown
        ddlExtCourse.Visible = true;
        //ddlSemester.SelectedValue = ddlSem.SelectedValue;
        if (ViewState["action"] == "edit")
        {
            ClearControls();
        }
    }

    // bind Existing courses
    private void FillDropDownCourse()
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_SCHEMENO", Convert.ToInt32(ddlScheme.SelectedValue));
                ////objParams[1] = new SqlParameter("@P_SEMESTERNO", Convert.ToInt32(ddlSem.SelectedValue));

                DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_ALL_COURSES", objParams);

                ddlExtCourse.Items.Clear();
                ddlExtCourse.Items.Add(new ListItem("Please Select", "0"));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlExtCourse.DataSource = ds;
                    ddlExtCourse.DataTextField = "COURSENAME";
                    ddlExtCourse.DataValueField = "COURSENO";
                    ddlExtCourse.DataBind();

                    ddlExtCourse.Enabled = true;

                    //lblStatus.Text = "Ready to Add New Course";
                    ViewState["action"] = "add";
                    ddlExtCourse.Focus();
                }
                else
                {
                    ddlExtCourse.Enabled = true;
                    ViewState["action"] = "add";
                }
            }
            else
            {
                ddlExtCourse.Items.Clear();
                ddlExtCourse.Items.Add(new ListItem("Please Select", "0"));
                ddlScheme.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_courseMaster.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // bind Exam Name
    private void GetExamName()
    {
        try
        {
            if (ddlDept.SelectedIndex > 0)
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                string[] ex1 = ddlScheme.SelectedValue.Split('-');
                int patternno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "ISNULL(PATTERNNO,0)", "schemeno='" + ddlScheme.SelectedValue + "'"));
                //int schemetype = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "ISNULL(SCHEMETYPE,0)", "patternno='" + patternno + "'"));


                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_PATTERNNO", patternno);
                DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_ALL_EXAM", objParams);
                //DataSet ds = objSQLHelper.ExecuteDataSetSP("[PKG_EXAM_GET_ALL_EXAM_HEADS]", objParams);
                ddlExtCourse.Items.Clear();
                ddlExtCourse.Items.Add(new ListItem("Please Select", "0"));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    rtpScheme.DataSource = ds;
                    rtpScheme.DataBind();
                }
                else
                {
                    ddlExtCourse.Enabled = true;
                    ViewState["action"] = "add";
                }
            }
            else
            {
                ddlExtCourse.Items.Clear();
                ddlExtCourse.Items.Add(new ListItem("Please Select", "0"));
                ddlDept.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_courseMaster.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lvScheme_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem item = e.Item as ListViewDataItem;
        if (ViewState["action"] != null)
        {
            if (ViewState["action"].ToString().Equals("edit"))
            {
                //Check and Select
                CheckBox cb = item.FindControl("cbRow") as CheckBox;
                cb.Checked = true;
            }
        }
    }

    #endregion

    #region Click Events

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddlDegree.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlExtCourse.SelectedIndex = 0;
        ddlExtCourse.SelectedIndex = 0;
        ddlSpecialisation.Enabled = false;
        ddlElectiveGroup.SelectedIndex = 0;
        ddlCElectiveGroup.SelectedIndex = 0;
        btnCancel1.Visible = true;
        btnUpdate.Visible = true;
        lblMsg.Text = string.Empty;
        txtCCode.Text = string.Empty;
        txtCourseName.Text = string.Empty;
        txtLectures.Text = string.Empty;
        txtTheory.Text = string.Empty;
        txtTutorial.Text = string.Empty;
        txtTotal.Text = string.Empty;
        txtPract.Text = string.Empty;
        chkElective.Checked = false;
        txtCourseName.Enabled = true;
        txtCCode.Enabled = true;
        ddlTP.Enabled = true;
        ClearControls();
        DataSet ds = null;
        lvCourseMaterial.DataSource = null;
        lvCourseMaterial.DataBind();
        GetExamName();
        rtpScheme.DataSource = null;
        rtpScheme.DataBind();
        ViewState["action"] = null;
        txtLectHours.Text = string.Empty;
        txtMinLHours.Text = string.Empty;
        txtPracHours.Text = string.Empty;
        txtMinPHours.Text = string.Empty;
        txtClinHours.Text = string.Empty;
        txtMinCHours.Text = string.Empty;
        txtIntegrateLHrs.Text = string.Empty;
        txtMinIntegrateHrs.Text = string.Empty;
        txtTotalHours.Text = string.Empty;
        txtJournalClub.Text = string.Empty;
        txtGuestLecture.Text = string.Empty;
        txtPosterPresentation.Text = string.Empty;
        txtOralPresentation.Text = string.Empty;
        txtPublication.Text = string.Empty;
        txtCaseDiscuss.Text = string.Empty;
        ddlCollege.SelectedIndex = 0;
    }

    public void LoadUploadedDocs()
    {
    }

    // Modification of previous courses 
    protected void btnModifyCourse_Click(object sender, EventArgs e)
    {
        try
        {
            lvCourseMaterial.DataSource = null;
            lvCourseMaterial.DataBind();


            btnUpdate.Visible = false;
            btnCancel1.Visible = false;
            trbtn.Visible = true;
            // div5.Visible = true;

            if (ddlExtCourse.SelectedIndex == 0)
            {
                //lblStatus.Text = "Please Select Existing Course";
            }
            else
            {
                ViewState["action"] = "edit";
                string[] schno = ddlScheme.SelectedValue.Split('-');
                this.ShowDetails(Convert.ToInt32(ddlExtCourse.SelectedValue), Convert.ToInt32(schno[0]));

                string Courseno = objCommon.LookUp("ACD_STUDENT_RESULT", "COURSENO", "COURSENO='" + ddlExtCourse.SelectedValue + "' AND ISNULL(CANCEL,0)=0");
                if (ddlExtCourse.SelectedValue == Courseno)
                {
                    txtCCode.Enabled = false;
                }
                else
                    txtCCode.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_courseMaster.btnModifyCourse_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    CourseController objCourse = new CourseController();
    Course objc = new Course();

    // Stored All details
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlDept.SelectedIndex == 0 && ddlDegree.SelectedIndex == 0 && ddlScheme.SelectedIndex == 0)
        {
            lblMsg.Text = "Please Select Proper Data for Subject Creation/Modification";
            return;
        }

        if (chkElective.Checked == true && ddlElectiveGroup.SelectedIndex <= 0)
        {
            objCommon.DisplayMessage(this.UPDCOURSE, "Please Select Elective Group!!", this.Page);
            return;
        }
        try
        {
            objc.CourseName = txtCourseName.Text.Replace("'", "").Trim();
            objc.CCode = txtCCode.Text.Replace("'", "").Trim();
            if (!txtLectures.Text.Trim().Equals(string.Empty)) objc.Lecture = Convert.ToDecimal(txtLectures.Text.Trim());
            if (!txtPract.Text.Trim().Equals(string.Empty)) objc.Practical = Convert.ToDecimal(txtPract.Text.Trim());
            if (!txtTutorial.Text.Trim().Equals(string.Empty)) objc.Theory = Convert.ToDecimal(txtTutorial.Text.Trim());
            if (!txtTheory.Text.Trim().Equals(string.Empty)) objc.Credits = Convert.ToDecimal(txtTheory.Text.Trim());
            objc.Elect = (chkElective.Checked == true ? 1 : 0);
            objc.CGroupno = Convert.ToInt32(ddlCElectiveGroup.SelectedValue);
            objc.Groupno = Convert.ToInt32(ddlElectiveGroup.SelectedValue);
            objc.SubID = Convert.ToInt32(ddlTP.SelectedValue);
            objc.CollegeCode = Session["colcode"].ToString();
            objc.Deptno = Convert.ToInt32(ddlParentDept.SelectedValue);
            objc.Paper_hrs = Convert.ToInt16(txtPaper.Text.ToString() == "" ? "0" : txtPaper.Text.ToString());
            objc.Semester_Hrs = Convert.ToDecimal(txtHrsInSem.Text.ToString() == "" ? "0.00" : txtHrsInSem.Text.ToString());
            objc.Categoryno = 0;
            // Added by Pritish on 15/01/2021        
            objc.LectureHours = txtLectHours.Text.ToString() == string.Empty ? 0 : Convert.ToInt32(txtLectHours.Text.ToString());
            objc.MinLectureHours = txtMinLHours.Text.ToString() == string.Empty ? 0 : Convert.ToInt32(txtMinLHours.Text.ToString());
            objc.PracticalHours = txtPracHours.Text.ToString() == string.Empty ? 0 : Convert.ToInt32(txtPracHours.Text.ToString());
            objc.MinPracticalHours = txtMinPHours.Text.ToString() == string.Empty ? 0 : Convert.ToInt32(txtMinPHours.Text.ToString());
            objc.ClinicalHours = txtClinHours.Text.ToString() == string.Empty ? 0 : Convert.ToInt32(txtClinHours.Text.ToString());
            objc.MinClinicalHours = txtMinCHours.Text.ToString() == string.Empty ? 0 : Convert.ToInt32(txtMinCHours.Text.ToString());
            objc.IntegratedHours = txtIntegrateLHrs.Text.ToString() == string.Empty ? 0 : Convert.ToInt32(txtIntegrateLHrs.Text.ToString());
            objc.MinIntegratedHours = txtMinIntegrateHrs.Text.ToString() == string.Empty ? 0 : Convert.ToInt32(txtMinIntegrateHrs.Text.ToString());
            objc.TotalHours = txtTotalHours.Text.ToString() == string.Empty ? 0 : hfTotal.Value == string.Empty ? Convert.ToInt32(txtTotalHours.Text.ToString()) : Convert.ToInt32(hfTotal.Value);

            objc.Journalclub = txtJournalClub.Text.ToString() == string.Empty ? 0 : Convert.ToInt32(txtJournalClub.Text.ToString());
            objc.Casediscussion = txtCaseDiscuss.Text.ToString() == string.Empty ? 0 : Convert.ToInt32(txtCaseDiscuss.Text.ToString());
            objc.Guestlecture = txtGuestLecture.Text.ToString() == string.Empty ? 0 : Convert.ToInt32(txtGuestLecture.Text.ToString());
            objc.Posterpresentation = txtPosterPresentation.Text.ToString() == string.Empty ? 0 : Convert.ToInt32(txtPosterPresentation.Text.ToString());
            objc.Oralpresentation = txtOralPresentation.Text.ToString() == string.Empty ? 0 : Convert.ToInt32(txtOralPresentation.Text.ToString());
            objc.Publication = txtPublication.Text.ToString() == string.Empty ? 0 : Convert.ToInt32(txtPublication.Text.ToString());

            foreach (RepeaterItem item in rtpScheme.Items)
            {
                TextBox txtMinMarks = item.FindControl("txtMinMarks") as TextBox;
                TextBox txtMaxMarks = item.FindControl("txtMaxMarks") as TextBox;
                Label lblFldName = item.FindControl("lblFldName") as Label;

                switch (lblFldName.Text.ToString())
                {
                    case ("S1"):
                        objc.S1Max = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S1Min = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S2"):
                        objc.S2Max = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S2Min = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S3"):
                        objc.S3Max = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S3Min = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S4"):
                        objc.S4Max = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S4Min = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S5"):
                        objc.S5Max = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S5Min = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S6"):
                        objc.S6Max = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S6Min = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S7"):
                        objc.S7Max = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S7Min = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S8"):
                        objc.S8Max = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S8Min = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S9"):
                        objc.S9Max = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S9Min = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S10"):
                        objc.S10Max = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S10Min = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("EXTERMARK"):
                        objc.ExtermarkMax = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.ExtermarkMin = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                }
            }

            //Delete Scheme No
            string[] sno = ddlScheme.SelectedValue.Split('-');
            string delschno = string.Empty;
            string insschno = string.Empty;

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    //Edit Course
                    objc.SchemeNo = Convert.ToInt32(sno[0]);
                    objc.CourseNo = Convert.ToInt32(ddlExtCourse.SelectedValue);


                    objc.SchNo = insschno;
                    objc.DelSchNo = delschno;

                    //modified by neha 20/06/19
                    //to upload the course material
                    uploadDocument();
                    if (furefMaterial.HasFile)
                    {
                        if (status == 1)
                        {
                            CustomStatus cs = (CustomStatus)objCourse.UpdateCourse(objc);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                objCommon.DisplayMessage(this.UPDCOURSE, "Subject Modified Successfully!!", this.Page);
                                ViewState["action"] = "add";
                                ClearControls();
                                FillDropDownCourse();
                                //this.BindCourseList(Convert.ToInt32(ddlScheme.SelectedValue), "0");//, Convert.ToInt32(ddlSem.SelectedValue)
                            }
                            else
                                objCommon.DisplayMessage(this.UPDCOURSE, "Error!!", this.Page);
                        }
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objCourse.UpdateCourse(objc);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage(this.UPDCOURSE, "Subject Modified Successfully!!", this.Page);
                            ViewState["action"] = "add";
                            ClearControls();
                            FillDropDownCourse();
                        }
                        else
                            objCommon.DisplayMessage(this.UPDCOURSE, "Error!!", this.Page);
                    }
                }
                else
                {
                    //Add New Course
                    objc.SchNo = sno[0] + "," + insschno;

                    //To ckeck Course Code in current selected Scheme
                    int cnt = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "COUNT(*)", "CCODE='" + txtCCode.Text + "' AND SCHEMENO=" + sno[0]));

                    if (cnt >= 1)
                    {
                        objCommon.DisplayMessage(this.UPDCOURSE, "Subject with same Subject Code. Already Exist!!", this.Page);
                        return;
                    }

                    //modified by neha 20/06/19
                    //to upload the course material
                    uploadDocument();

                    if (furefMaterial.HasFile)
                    {
                        if (status == 1)
                        {
                            CustomStatus cs = (CustomStatus)objCourse.AddCourse(objc);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                objCommon.DisplayMessage(this.UPDCOURSE, "Subject Added Successfully!!", this.Page);
                                ViewState["action"] = "add";
                                FillDropDownCourse();
                                ClearControls();
                            }
                            else
                                objCommon.DisplayMessage(this.UPDCOURSE, "Error!!", this.Page);
                        }
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objCourse.AddCourse(objc);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(this.UPDCOURSE, "Subject Added Successfully!!", this.Page);
                            ViewState["action"] = "add";
                            FillDropDownCourse();
                            ClearControls();
                        }
                        else
                            objCommon.DisplayMessage(this.UPDCOURSE, "Error!!", this.Page);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_courseMaster.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // delete Courses
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        lvCourseMaterial.DataSource = null;
        lvCourseMaterial.DataBind();
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            string filename = btnDelete.ToolTip;
            string path = MapPath("~/CourseMaterial/");
            string courseno = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.Add("CourseNo");
            dt.Columns.Add("Filename");
            if (File.Exists(path + filename))
            {
                File.Delete(path + filename);
                objCommon.DisplayMessage(pnl_course, "File Deleted Successfully!", this);
                string[] array1 = Directory.GetFiles(path);
                courseno = filename.Substring(0, filename.IndexOf(" - "));
                foreach (string str in array1)
                    if (str.Contains(path + courseno))
                        dt.Rows.Add(new Object[] { courseno, str.ToString().Remove(str.IndexOf(path), path.Length) });
                if (dt != null && dt.Rows.Count > 0)
                {
                    lvCourseMaterial.DataSource = dt;
                    lvCourseMaterial.DataBind();
                }
            }
            else
            {
                objCommon.DisplayMessage(this.UPDCOURSE, "File  Not Found!", this);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "domain.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // Modify Marks
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ViewState["action"] == null)
        {
            CourseController objCourse = new CourseController();
            Course objc = new Course();
            foreach (RepeaterItem item in rtpScheme.Items)
            {
                TextBox txtMinMarks = item.FindControl("txtMinMarks") as TextBox;
                TextBox txtMaxMarks = item.FindControl("txtMaxMarks") as TextBox;
                Label lblFldName = item.FindControl("lblFldName") as Label;

                switch (lblFldName.Text.ToString())
                {
                    case ("S1"):
                        objc.S1Max = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S1Min = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S2"):
                        objc.S2Max = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S2Min = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S3"):
                        objc.S3Max = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S3Min = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S4"):
                        objc.S4Max = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S4Min = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S5"):
                        objc.S5Max = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S5Min = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("EXTERMARK"):
                        objc.ExtermarkMax = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.ExtermarkMin = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S6"):
                        objc.S6Max = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S6Min = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S7"):
                        objc.S7Max = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S7Min = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S8"):
                        objc.S8Max = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S8Min = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S9"):
                        objc.S9Max = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S9Min = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S10"):
                        objc.S10Max = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S10Min = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                }
            }
            int ret = objCourse.UpdateExamMarks(objc);
            if (ret == Convert.ToInt16(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.UPDCOURSE, "Marks Updated SuccessFully...!", this);
                GetExamName();
            }
            else
                objCommon.DisplayMessage(this.UPDCOURSE, "Error!", this);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        rtpScheme.DataSource = null;
        rtpScheme.DataBind();
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).CommandArgument.ToString();
        string ContentType = string.Empty;

        string filepath = WebConfigurationManager.AppSettings["SVCE_SUBJECT_DOC"].ToString() + txtCCode.Text + "\\";

        // Create New instance of FileInfo class to get the properties of the file being downloaded
        FileInfo myfile = new FileInfo(filepath + filename);
        string file = filepath + filename;

        string ext = Path.GetExtension(filename);
        // Checking if file exists
        if (myfile.Exists)
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.TransmitFile(file);
            Response.Flush();
            Response.End();
        }
    }

    int status = 0;

    private void uploadDocument()
    {
        try
        {
            string folderPath = WebConfigurationManager.AppSettings["SVCE_SUBJECT_DOC"].ToString() + txtCCode.Text + "\\";

            if (furefMaterial.HasFile)
            {
                string ext = System.IO.Path.GetExtension(furefMaterial.PostedFile.FileName);
                HttpPostedFile file = furefMaterial.PostedFile;
                string filename = Path.GetFileName(furefMaterial.PostedFile.FileName);
                if (ext == ".pdf" || ext == ".xls" || ext == ".xlsx" || ext == ".doc" || ext == ".docx" || ext == ".PDF")
                {
                    if (file.ContentLength <= 102400)// 31457280 before size  //For Allowing 100 Kb Size Files only 
                    {
                        string contentType = furefMaterial.PostedFile.ContentType;
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        objc.FileName = filename;
                        objc.FilePath = folderPath + filename;
                        furefMaterial.PostedFile.SaveAs(folderPath + filename);
                        status = 1;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Document size must not exceed 100 Kb !", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this, "Only .xls,.xlsx,.pdf,.doc,.docx file type allowed  !", this.Page);
                }
            }
            else
            {
                objc.FileName = "";
                objc.FilePath = "";
            }
        }
        catch (Exception ex)
        {
            // objCommon.DisplayMessage(this, "Something went wrong ! Please contact Admin", this.Page);
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        #region Previouscode
        DataTable dt = new DataTable();
        int courseno = 0;

        string path = WebConfigurationManager.AppSettings["SVCE_SUBJECT_DOC"].ToString() + "\\";
        //string path = MapPath("~/CourseMaterial/");

        dt.Columns.Add("Courseno");
        dt.Columns.Add("Filename");

        if (ViewState["action"].ToString() == "edit")
            courseno = Convert.ToInt16(ddlExtCourse.SelectedValue);
        else
        {
            courseno = Convert.ToInt16(objCommon.LookUp("ACD_COURSE", "ISNULL(MAX(COURSENO),0)", "COURSENO <> 0"));
            courseno = courseno + 1;
        }
        try
        {
            if (!(Directory.Exists(path)))
                Directory.CreateDirectory(path);

            if (furefMaterial.HasFile)
            {
                string[] array1 = Directory.GetFiles(path);

                foreach (string str in array1)
                {
                    if ((path + courseno.ToString() + " - " + furefMaterial.FileName.ToString()).Equals(str))
                    {
                        objCommon.DisplayMessage(pnl_course, "File Already Exists!", this);
                        return;
                    }
                    if (str.Contains(path + courseno.ToString()))
                    {
                        dt.Rows.Add(new Object[] { courseno, str.ToString().Remove(str.IndexOf(path), path.Length) });
                    }
                }
                furefMaterial.SaveAs(MapPath(path + courseno.ToString() + "-" + furefMaterial.FileName));
                dt.Rows.Add(new Object[] { courseno, courseno.ToString() + "-" + furefMaterial.FileName.ToString() });
                objCommon.DisplayMessage(pnl_course, "File Uploaded SuccessFully...!", this);
                lvCourseMaterial.DataSource = dt;
                lvCourseMaterial.DataBind();
            }

            else
            {
                objCommon.DisplayMessage(this.UPDCOURSE, "Select File to Upload!", this);
                return;
            }

        }
        catch (DirectoryNotFoundException ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_courseMaster.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Directory Not Found Exception!!");
        }
        #endregion



    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
        ddlDegree.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlExtCourse.SelectedIndex = 0;
        lvCourseMaterial.DataSource = null;
        lvCourseMaterial.DataBind();
        txtPaper.Text = string.Empty;
        txtCCode.Text = string.Empty;
        txtCourseName.Text = string.Empty;
        txtLectures.Text = string.Empty;
        txtTutorial.Text = string.Empty;
        txtTotal.Text = string.Empty;
        txtPract.Text = string.Empty;
        ddlExtCourse.SelectedIndex = 0;
        ddlTP.SelectedIndex = 0;
        ddlParentDept.SelectedIndex = 0;
        DataSet ds = null;
        btnCancel1.Visible = true;
        btnUpdate.Visible = true;
        txtTheory.Text = string.Empty;
        txtCCode.Enabled = true;
        chkElective.Checked = false;
        ddlCElectiveGroup.SelectedIndex = 0;
        ddlElectiveGroup.SelectedIndex = 0;
        trbtn.Visible = false;
        txtHrsInSem.Text = string.Empty;
        chkElective.Checked = false;
        txtDrawing.Text = string.Empty;
        rtpScheme.DataSource = null;
        rtpScheme.DataBind();
        foreach (RepeaterItem item in rtpScheme.Items)
        {
            TextBox txtMinMarks = item.FindControl("txtMinMarks") as TextBox;
            TextBox txtMaxMarks = item.FindControl("txtMaxMarks") as TextBox;
            txtMinMarks.Text = string.Empty;
            txtMaxMarks.Text = string.Empty;
        }
        txtLectHours.Text = string.Empty;
        txtMinLHours.Text = string.Empty;
        txtPracHours.Text = string.Empty;
        txtMinPHours.Text = string.Empty;
        txtClinHours.Text = string.Empty;
        txtMinCHours.Text = string.Empty;
        txtIntegrateLHrs.Text = string.Empty;
        txtMinIntegrateHrs.Text = string.Empty;
        txtTotalHours.Text = string.Empty;
        txtJournalClub.Text = string.Empty;
        txtGuestLecture.Text = string.Empty;
        txtPosterPresentation.Text = string.Empty;
        txtOralPresentation.Text = string.Empty;
        txtPublication.Text = string.Empty;
        txtCaseDiscuss.Text = string.Empty;
        ddlCollege.SelectedIndex = 0;
        lblMsg.Text = string.Empty;
    }

    #endregion

    #region User Methods

    private void BindSchemeListView()
    {
        try
        {
            char[] sep = { ',' };
            string[] semno = ViewState["sem_no"].ToString().Split(sep);
            string[] schno = ddlScheme.SelectedValue.Split('-');

            DataSet ds = null;
            CourseController objCC = new CourseController();

            if (ViewState["action"] == null)
            {
                ds = objCC.GetGElecScheme(txtCCode.Text.Trim(), ddlDegree.SelectedValue, Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(semno[Convert.ToInt32(ddlScheme.SelectedIndex) - 1]), Convert.ToInt32(schno[1]));
            }
            else
            {
                if (ViewState["action"].ToString().Equals("edit"))
                    ds = objCC.GetSchemeNoByCCode((ddlExtCourse.SelectedItem.Text.Substring(0, Convert.ToInt32(ddlExtCourse.SelectedItem.Text.IndexOf("-")) - 1)), ddlDegree.SelectedValue, Convert.ToInt32(semno[Convert.ToInt32(ddlScheme.SelectedIndex) - 1]), Convert.ToInt32(schno[1]));
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_courseMaster.BindSchemeListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ClearControls()
    {
        ViewState["action"] = "add";
        // ddlDegree.SelectedIndex = 0;
        //  ddlDept.SelectedIndex = 0;
        // ddlScheme.SelectedIndex = 0;
        // ddlBranch.SelectedIndex = 0;
        //  ddlExtCourse.SelectedIndex = 0;
        lvCourseMaterial.DataSource = null;
        lvCourseMaterial.DataBind();
        txtPaper.Text = string.Empty;
        txtCCode.Text = string.Empty;
        txtCourseName.Text = string.Empty;
        txtLectures.Text = string.Empty;
        txtTutorial.Text = string.Empty;
        txtTotal.Text = string.Empty;
        txtPract.Text = string.Empty;
        ddlExtCourse.SelectedIndex = 0;
        ddlTP.SelectedIndex = 0;
        ddlParentDept.SelectedIndex = 0;
        DataSet ds = null;
        btnCancel1.Visible = true;
        btnUpdate.Visible = true;
        txtTheory.Text = string.Empty;
        txtCCode.Enabled = true;
        chkElective.Checked = false;
        ddlCElectiveGroup.SelectedIndex = 0;
        ddlElectiveGroup.SelectedIndex = 0;
        trbtn.Visible = false;
        txtHrsInSem.Text = string.Empty;
        chkElective.Checked = false;
        txtDrawing.Text = string.Empty;
        rtpScheme.DataSource = null;
        rtpScheme.DataBind();
        foreach (RepeaterItem item in rtpScheme.Items)
        {
            TextBox txtMinMarks = item.FindControl("txtMinMarks") as TextBox;
            TextBox txtMaxMarks = item.FindControl("txtMaxMarks") as TextBox;
            txtMinMarks.Text = string.Empty;
            txtMaxMarks.Text = string.Empty;
        }
        txtLectHours.Text = string.Empty;
        txtMinLHours.Text = string.Empty;
        txtPracHours.Text = string.Empty;
        txtMinPHours.Text = string.Empty;
        txtClinHours.Text = string.Empty;
        txtMinCHours.Text = string.Empty;
        txtIntegrateLHrs.Text = string.Empty;
        txtMinIntegrateHrs.Text = string.Empty;
        txtTotalHours.Text = string.Empty;

        txtJournalClub.Text = string.Empty;
        txtGuestLecture.Text = string.Empty;
        txtPosterPresentation.Text = string.Empty;
        txtOralPresentation.Text = string.Empty;
        txtPublication.Text = string.Empty;
        txtCaseDiscuss.Text = string.Empty;
        // ddlCollege.SelectedIndex = 0;
    }

    private void ShowDetails(int courseno, int schemeno)
    {
        try
        {
            CourseController objCC = new CourseController();
            SqlDataReader dr = objCC.GetCourses(courseno, schemeno);
            if (dr != null)
            {
                if (dr.Read())
                {
                    txtCourseName.Text = dr["COURSE_NAME"] == DBNull.Value ? string.Empty : dr["COURSE_NAME"].ToString();
                    txtLectures.Text = dr["LECTURE"] == DBNull.Value ? "0" : dr["LECTURE"].ToString();
                    txtTutorial.Text = dr["THEORY"] == DBNull.Value ? "0" : dr["THEORY"].ToString();
                    txtPract.Text = dr["PRACTICAL"] == DBNull.Value ? "0" : dr["PRACTICAL"].ToString();
                    txtTheory.Text = dr["CREDITS"] == DBNull.Value ? "0" : dr["CREDITS"].ToString();
                    txtCCode.Text = dr["CCODE"] == DBNull.Value ? string.Empty : dr["CCODE"].ToString();
                    txtPaper.Text = dr["PAPER_HRS"] == DBNull.Value ? string.Empty : dr["PAPER_HRS"].ToString();
                    txtHrsInSem.Text = dr["SEMESTER_HRS"] == DBNull.Value ? string.Empty : dr["SEMESTER_HRS"].ToString();
                   
                    ddlTP.SelectedValue = dr["SUBID"] == DBNull.Value ? "-1" : dr["SUBID"].ToString();
                    ddlParentDept.SelectedValue = dr["BOS_DEPTNO"] == DBNull.Value ? "-1" : dr["BOS_DEPTNO"].ToString();
                    ddlCElectiveGroup.SelectedValue = dr["CGROUPNO"].ToString() == "" ? "0" : dr["CGROUPNO"].ToString();
                    // Added by Pritish on 15/01/2021
                    txtLectHours.Text = dr["TH_HOURS"] == DBNull.Value ? string.Empty : dr["TH_HOURS"].ToString();
                    txtMinLHours.Text = dr["MIN_TH_HOURS"] == DBNull.Value ? string.Empty : dr["MIN_TH_HOURS"].ToString();
                    txtPracHours.Text = dr["PR_HOURS"] == DBNull.Value ? string.Empty : dr["PR_HOURS"].ToString();
                    txtMinPHours.Text = dr["MIN_PR_HOURS"] == DBNull.Value ? string.Empty : dr["MIN_PR_HOURS"].ToString();
                    txtClinHours.Text = dr["CL_HOURS"] == DBNull.Value ? string.Empty : dr["CL_HOURS"].ToString();
                    txtMinCHours.Text = dr["MIN_CL_HOURS"] == DBNull.Value ? string.Empty : dr["MIN_CL_HOURS"].ToString();
                    txtIntegrateLHrs.Text = dr["INTEGRATED_HOURS"] == DBNull.Value ? string.Empty : dr["INTEGRATED_HOURS"].ToString();
                    txtMinIntegrateHrs.Text = dr["MIN_INTEGRATED_HOURS"] == DBNull.Value ? string.Empty : dr["MIN_INTEGRATED_HOURS"].ToString();
                    txtTotalHours.Text = dr["TOTAL_HOURS"] == DBNull.Value ? string.Empty : dr["TOTAL_HOURS"].ToString();

                    txtJournalClub.Text = dr["JOURNAL_CLUB"] == DBNull.Value ? "0" : dr["JOURNAL_CLUB"].ToString();
                    txtCaseDiscuss.Text = dr["CASE_DISCUSSION"] == DBNull.Value ? "0" : dr["CASE_DISCUSSION"].ToString();
                    txtGuestLecture.Text = dr["GUEST_LECTURE"] == DBNull.Value ? "0" : dr["GUEST_LECTURE"].ToString();
                    txtPosterPresentation.Text = dr["POSTER_PRESENTATION"] == DBNull.Value ? "0" : dr["POSTER_PRESENTATION"].ToString();
                    txtOralPresentation.Text = dr["ORAL_PRESENTATION"] == DBNull.Value ? "0" : dr["ORAL_PRESENTATION"].ToString();
                    txtPublication.Text = dr["PUBLICATION"] == DBNull.Value ? "0" : dr["PUBLICATION"].ToString();
                    txtDrawing.Text = "0";
                    string total = (Convert.ToInt32(txtJournalClub.Text) + Convert.ToInt32(txtCaseDiscuss.Text) + Convert.ToInt32(txtGuestLecture.Text) + Convert.ToInt32(txtPosterPresentation.Text) + Convert.ToInt32(txtOralPresentation.Text) + Convert.ToInt32(txtPublication.Text)).ToString();

                    txtTotal.Text = (Convert.ToDecimal(txtLectures.Text) + Convert.ToDecimal(txtTutorial.Text) + Convert.ToDecimal(txtPract.Text) + Convert.ToDecimal(total)).ToString();

                    //to get uploaded documents
                    DataSet dsDOCS = new DataSet();
                    dsDOCS = objCC.GetDocsByCourseNo(courseno);
                    if (dsDOCS.Tables[0].Rows.Count > 0)
                    {
                        lvCourseMaterial.DataSource = dsDOCS;
                        lvCourseMaterial.DataBind();
                    }
                    else
                    {
                        lvCourseMaterial.DataSource = null;
                        lvCourseMaterial.DataBind();
                    }

                    if (dr["ELECT"] != DBNull.Value)
                    {
                        chkElective.Checked = Convert.ToBoolean(dr["ELECT"]);

                        if (chkElective.Checked == true)
                        {
                            ddlElectiveGroup.Enabled = true;
                        }
                        else
                        {
                            ddlElectiveGroup.Enabled = false;
                        }

                        ddlElectiveGroup.SelectedValue = dr["GROUPNO"].ToString() == "" ? "0" : dr["GROUPNO"].ToString();
                    }
                }
                dr.Close();

                int patternno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "ISNULL(PATTERNNO,0)", "SCHEMENO='" + schemeno + "'"));
                DataSet ds = objCC.GetCoursesMarks(courseno, patternno);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        rtpScheme.DataSource = ds;
                        rtpScheme.DataBind();

                        if (ddlTP.SelectedItem.Text == "THEORY")
                        {
                            for (int i = 0; i < rtpScheme.Items.Count; i++)
                            {
                                var GetTheoryorPractical = ds.Tables[0].Rows[i]["EXAMNAME"].ToString().Substring(ds.Tables[0].Rows[0]["EXAMNAME"].ToString().Length - 2);
                                if (GetTheoryorPractical == "TH")
                                {
                                    TextBox txtMinMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMinMarks");
                                    TextBox txtMaxMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMaxMarks");
                                    txtMinMarks.ReadOnly = false;
                                    txtMaxMarks.ReadOnly = false;
                                }
                                if (GetTheoryorPractical == "PR")
                                {
                                    TextBox txtMinMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMinMarks");
                                    TextBox txtMaxMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMaxMarks");
                                    txtMinMarks.ReadOnly = true;
                                    txtMaxMarks.ReadOnly = true;
                                }
                                else
                                {
                                    TextBox txtMinMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMinMarks");
                                    TextBox txtMaxMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMaxMarks");
                                    txtMinMarks.ReadOnly = false;
                                    txtMaxMarks.ReadOnly = false;
                                }
                            }

                        }
                        else
                        {
                            for (int i = 0; i < rtpScheme.Items.Count; i++)
                            {
                                var GetTheoryorPractical = ds.Tables[0].Rows[i]["EXAMNAME"].ToString().Substring(ds.Tables[0].Rows[0]["EXAMNAME"].ToString().Length - 2);
                                if (GetTheoryorPractical == "PR")
                                {
                                    TextBox txtMinMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMinMarks");
                                    TextBox txtMaxMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMaxMarks");
                                    txtMinMarks.ReadOnly = false;
                                    txtMaxMarks.ReadOnly = false;
                                }
                                if (GetTheoryorPractical == "TH")
                                {
                                    TextBox txtMinMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMinMarks");
                                    TextBox txtMaxMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMaxMarks");
                                    txtMinMarks.ReadOnly = true;
                                    txtMaxMarks.ReadOnly = true;
                                }
                                else
                                {
                                    TextBox txtMinMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMinMarks");
                                    TextBox txtMaxMarks = (TextBox)rtpScheme.Items[i].FindControl("txtMaxMarks");
                                    txtMinMarks.ReadOnly = false;
                                    txtMaxMarks.ReadOnly = false;
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_courseMaster.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=coursemaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=coursemaster.aspx");
        }
    }

    //Load data on page Load
    private void PopulateDropDown()
    {
        try
        {
            ////fill degree name
            //if (Session["usertype"].ToString() != "1")
            //{
            //    string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
            //    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
            //}
            //else
            //{
            //    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0", "D.DEGREENO");
            //}

            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"].ToString() + ")", "COLLEGE_NAME");
            objCommon.FillDropDownList(ddlTP, "ACD_SUBJECTTYPE", "SUBID", "SUBNAME", "SUBID > 0", "SUBID");
            objCommon.FillDropDownList(ddlCElectiveGroup, "ACD_COURSE_CATEGORY", "CATEGORYNO", "CATEGORYNAME", "CATEGORYNO > 0", "CATEGORYNO");
            objCommon.FillDropDownList(ddlElectiveGroup, "ACD_ELECTGROUP", "GROUPNO", "GROUPNAME", "GROUPNO > 0", "GROUPNO");
            if (Session["usertype"].ToString() != "1")
                objCommon.FillDropDownList(ddlParentDept, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", " DISTINCT D.DEPTNO", "D.DEPTNAME", "D.DEPTNO>0 AND B.DEPTNO=" + Session["userdeptno"].ToString(), "D.DEPTNAME");
            else
                objCommon.FillDropDownList(ddlParentDept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO <>0 AND DEPTNO IS NOT NULL ", "DEPTNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_courseMaster.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".png":
            case "png":
                return "image/png";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
            case ".cs":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }

    public string GetFileNamePath(string filename)
    {
        string path = MapPath("~/CourseMaterial/");
        if (filename != null && filename.ToString() != "")
            return path.ToString() + filename.ToString().Replace("%2520", " ");
        else
            return "";
    }

    protected void btnCheckListReport_Click(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue != "0")
        {
            string[] sno = ddlScheme.SelectedValue.Split('-');
            ShowReport("Check_List", "rptSubjectCourseListSchemewise.rpt", Convert.ToInt32(sno[0]));
        }
        else
        {
            objCommon.DisplayMessage(this.UPDCOURSE, "Please Select Path/Regulation", this.Page);
            return;
        }
    }

    private void ShowReport(string reportTitle, string rptFileName, int schemeno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SCHEMENO=" + Convert.ToInt32(schemeno) + "";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UPDCOURSE, this.UPDCOURSE.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_REPORTS_CheckListReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    #endregion

    //private void BindListView()
    //{
    //    try
    //    {
    //        SchemeController objSC = new SchemeController();
    //        DataSet ds = objSC.GetScheme(Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));
    //        rtpScheme.DataSource = ds;
    //        rtpScheme.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_schememaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    protected void chkElective_CheckedChanged(object sender, EventArgs e)
    {
        if (chkElective.Checked == true)
        {
            ddlElectiveGroup.Enabled = true;
        }
        else
        {
            ddlElectiveGroup.Enabled = false;
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CDB.DEGREENO)", "DISTINCT (D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CDB.COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENO");
    }

}