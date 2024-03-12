//======================================================================================
// PROJECT NAME  : RFC SVCE                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : FACULTY ADVISOR ALLOTMENT TO STUDENT                                    
// CREATION DATE : 22/05/19
// CREATED BY    : Bhushan P                                                
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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using CrystalDecisions.CrystalReports.Engine; //crystal report
using CrystalDecisions.Shared; //crystal report


public partial class Academic_assignFacultyAdvisor : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //   string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    #region Page Events
    //USED FOR INITIALSING THE MASTER PAGE
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
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
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                PopulateDropDownList();                
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
            }

            hdfTot.Visible = false;
            Session["reportdata"] = null;
            objCommon.SetLabelData("0");//for label
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
        }

    }

    //private void CheckPageAuthorization()
    //{
    //    if (Request.QueryString["pageno"] != null)
    //    {
    //        //Check for Authorization of Page
    //        if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
    //        {
    //            Response.Redirect("~/notauthorized.aspx?page=assignFacultyAdvisor.aspx");
    //        }
    //    }
    //    else
    //    {
    //        //Even if PageNo is Null then, don't show the page
    //        Response.Redirect("~/notauthorized.aspx?page=assignFacultyAdvisor.aspx");
    //    }
    //}
    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page. 
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=assignFacultyAdvisor.aspx");
            }
            Common objCommon = new Common();
            // objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=assignFacultyAdvisor.aspx");
        }
    }

    private void GetCurrentSession()
    {
        //try
        //{
        //    DataSet ds = objCommon.GetDropDownData("PKG_DROPDOWN_SP_CURRENT_SESSION");
        //    //Get the First Rows first column
        //    DataRow dr = ds.Tables[0].Rows[0];

        //    if (dr != null)
        //    {
        //        lblCurrentSession.ToolTip = dr[0].ToString();
        //        lblCurrentSession.Text = dr[1].ToString();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "Academic_assignFacultyAdvisor.GetCurrentSession-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
    }

    #endregion

    #region Other Events
    //Button show used for to Showing the Student details in list view using bind list view methods
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            //txtTotChk.Text = "0";
            hdfTot.Value = "0";

            //Fill the ListView
            this.BindListView();

            //if (rblAllFaculty.Checked == true)
            //{
            //    objCommon.FillDropDownList(ddlAdvisor, "USER_ACC", "UA_NO", "(UA_NAME COLLATE DATABASE_DEFAULT + ' - ' + UA_FULLNAME COLLATE DATABASE_DEFAULT ) UANAME", "UA_TYPE IN (3)", "UA_NAME");
            //}
            //    objCommon.FillDropDownList(ddlAdvisor, "USER_ACC", "UA_NO", "(UA_NAME COLLATE DATABASE_DEFAULT + ' - ' + UA_FULLNAME COLLATE DATABASE_DEFAULT ) UANAME", "UA_TYPE IN (3,5)", "UA_NAME");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_assignFacultyAdvisor.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //this button used for Alloting the faculty advisor to one or more student
    protected void btnAssignFA0_Click(object sender, EventArgs e)
    {
        try
        {
            //Validations...
            if (ddlAdvisor.SelectedIndex <= 0)
            {
                objCommon.DisplayMessage(this.updpnl_details, "Please Select Faculty Advisor!", this.Page);
                return;
            }


            StudentController objSC = new StudentController();
            Student objStudent = new Student();
            objStudent.FacAdvisor = Convert.ToInt32(ddlAdvisor.SelectedValue);

            foreach (ListViewDataItem lvItem in lvFaculty.Items)
            {
                CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
                if (chkBox.Checked == true)
                    objStudent.StudId += chkBox.ToolTip + ",";
            }

            if (objStudent.StudId == "")
            {
                objCommon.DisplayMessage(this.updpnl_details, "Please Select At Least One Student!", this.Page);
                return;
            }



            if (objSC.UpdateStudent_FacultAdvisor(objStudent) == Convert.ToInt32(CustomStatus.RecordUpdated))
            {
                ddlAdvisor.SelectedIndex = 0;
                BindListView();
                objCommon.DisplayMessage(this.updpnl_details, "Faculty Advisor Alloted Successfully!", this.Page);
            }
            else
                objCommon.DisplayMessage(this.updpnl_details, "Error in Alloting Faculty Advisor", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_assignFacultyAdvisor.btnAssignFA0_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //refresh page or reload the page
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        ViewState["degreeno"] = 0;
        ViewState["branchno"] = 0;
        ViewState["college_id"] = 0;
        ViewState["schemeno"] = 0;
    }
    //On select of Degree bind Branch name ,SEMESTER NAME in drop down list
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Branch
        //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO<>99 and BRANCHNO > 0 and DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
        //ddlBranch.Focus();
        //Semester
        //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO<>99 and BRANCHNO > 0 and DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
        //ddlBranch.Focus();

        if (ddlDegree.SelectedIndex > 0)
        {

            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");

            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON B.BRANCHNO = CDB.BRANCHNO", "B.BRANCHNO", "B.LONGNAME", "CDB.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");

        }

        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");

        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlAdvisor.SelectedIndex = 0;
        lvFaculty.DataSource = null;
        lvFaculty.DataBind();
        lvFaculty.Visible = false;
    }
    ////showing the Faculty Adviser Report in rptFaculty_Advisor.rpt file.
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.updpnl_details, "Please Select College & Regulation !", this.Page);
                return;
            }
            //if (ddlDegree.SelectedValue == "0")
            //{
            //    objCommon.DisplayMessage(this.updpnl_details, "Please Select Degree !", this.Page);
            //    return;
            //}
            //if (ddlBranch.SelectedValue == "0")
            //{
            //    objCommon.DisplayMessage(this.updpnl_details, "Please Select Branch !", this.Page);
            //    return;
            //}
            if (ddlSemester.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.updpnl_details, "Please Select Semester !", this.Page);
                return;
            }
            ShowReport("Faculty Advisor Report", "rptFaculty_Advisor.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_assignFacultyAdvisor.btnPrint_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    #endregion

    #region User Defined Methods
    //bind Degree name in drop down list
    private void PopulateDropDownList()
    {
        try
        {
            //Department 
          //  objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");



            btnAssignFA0.Enabled = false;
            ddlAdvisor.Enabled = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_assignFacultyAdvisor.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //bind faculty name in ddlAdvisor drop down list and bind Student details in list view for allting the faculty adviser
    private void BindListView()
    {
        try
        {
            int facno = int.Parse(ddlAdvisor.SelectedValue);
            int branchno = int.Parse((ViewState["branchno"].ToString() != string.Empty) ? ViewState["branchno"].ToString() : "0");
            int sem = int.Parse(ddlSemester.SelectedValue);
            int sectionno = Convert.ToInt32(ddlSection.SelectedValue);
            int degreeno = Convert.ToInt32(ViewState["degreeno"].ToString());


            //  int deptno = Convert.ToInt32(objCommon.LookUp(" ACD_BRANCH", "DISTINCT DEPTNO", "BRANCHNO=" + branchno));
            DataSet dsfaculty = null;
            StudentController objStud = new StudentController();
            if (rblAllFaculty.Checked == true)
            {
                objCommon.FillDropDownList(ddlAdvisor, "USER_ACC", "UA_NO", "(UA_NAME COLLATE DATABASE_DEFAULT + ' - ' + UA_FULLNAME COLLATE DATABASE_DEFAULT ) UANAME", "UA_TYPE IN (3)", "UA_NAME");
            }

            if (rblStudent.SelectedValue == "0")
            {
                dsfaculty = objStud.GetStudentForFaculty(facno, branchno, sem, degreeno, sectionno);
            }

            else
            {
                dsfaculty = objStud.GetRemaingStudentForFaculty(facno, branchno, sem, degreeno, sectionno);
            }



            if (dsfaculty != null && dsfaculty.Tables.Count > 0 && dsfaculty.Tables[0].Rows.Count > 0)
            {
                lvFaculty.DataSource = dsfaculty;
                lvFaculty.DataBind();
                btnAssignFA0.Enabled = true;
                btnPrint.Enabled = true;
                ddlAdvisor.Enabled = true;
                btnAssignFA0.Enabled = true;
                lvFaculty.Visible = true;
            }
            else
            {
                btnAssignFA0.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_assignFacultyAdvisor.BindListView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lvfaculty_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        hdfTot.Value = (Convert.ToInt16(hdfTot.Value) + 1).ToString();
    }
    //showing the report in pdf formate as per as  selection of report name  or file name.
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string CollegeName = objCommon.LookUp(" Reff", "CollegeName", "College_Code=40");
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_DEGREENO=" + ViewState["degreeno"].ToString() + ",@P_BRANCHNO=" + ViewState["branchno"].ToString() + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SECTIONNO=" + ddlSection.SelectedValue + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_USERNAME=" + Session["username"].ToString() + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"].ToString());

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnl_details, this.updpnl_details.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentReport1.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion
    //On select of Semester bind Section name  and Faculty Advisor name  in drop down list and 
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataSet ds2 = null;
        StudentController objStud = new StudentController();
        #region for 1st year Case not Needed
        //if (ddlBranch.SelectedValue == "99" && ddlDegree.SelectedValue == "1")
        //{
        //    objCommon.FillDropDownList(ddlSection, "ACD_STUDENT S INNER JOIN ACD_SECTION SC ON (SC.SECTIONNO = S.SECTIONNO)", " DISTINCT S.SECTIONNO", "SC.SECTIONNAME", " S.DEGREENO =" + ViewState["degreeno"].ToString() + " AND S.BRANCHNO IN(SELECT BRANCHNO FROM ACD_BRANCH WHERE DEGREENO = 1) AND S.SEMESTERNO =" + ddlSemester.SelectedValue + " AND S.SECTIONNO > 0", "S.SECTIONNO");

        //    //    objCommon.FillDropDownList(ddlAdvisor, "USER_ACC acc inner join ACD_COLLEGE_DEGREE_BRANCH b on acc.UA_DEPTNO = b.DEPTNO", "ua_no", "UA_FULLNAME", " b.branchno =" + ddlBranch.SelectedValue + " and b.degreeno= " + ddlDegree.SelectedValue, "ua_no");                  

        //    ds2 = objStud.GetStudentForFaculty_UA_NAME(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
        //    if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
        //    {
        //        DataTable dt = new DataTable();
        //        dt = ds2.Tables[0];

        //        ddlAdvisor.DataSource = dt;
        //        ddlAdvisor.DataTextField = "UA_FULLNAME";
        //        ddlAdvisor.DataValueField = "ua_no";
        //        ddlAdvisor.DataBind();
        //    }

        //}
        //else
        //{
        #endregion
        //Section
        objCommon.FillDropDownList(ddlSection, "ACD_STUDENT S INNER JOIN ACD_SECTION SC ON (SC.SECTIONNO = S.SECTIONNO)", " DISTINCT S.SECTIONNO", "SC.SECTIONNAME", " S.DEGREENO =" + ViewState["degreeno"].ToString() + " AND S.BRANCHNO =" + ViewState["branchno"].ToString() + " AND S.SEMESTERNO =" + ddlSemester.SelectedValue + " AND S.SECTIONNO > 0", "S.SECTIONNO");

            //  objCommon.FillDropDownList(ddlAdvisor, "USER_ACC acc inner join ACD_COLLEGE_DEGREE_BRANCH b on acc.UA_DEPTNO = b.DEPTNO", "ua_no", " UA_FULLNAME", " b.branchno =" + ddlBranch.SelectedValue + " and b.degreeno= " + ddlDegree.SelectedValue, "ua_no");                  

        ds2 = objStud.GetStudentForFaculty_UA_NAME(Convert.ToInt32(ViewState["degreeno"].ToString()), Convert.ToInt32(ViewState["branchno"].ToString()));
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds2.Tables[0];

                ddlAdvisor.DataSource = dt;
                ddlAdvisor.DataTextField = "UA_FULLNAME";
                ddlAdvisor.DataValueField = "ua_no";
                ddlAdvisor.DataBind();
            }

        //}

        ddlSection.SelectedIndex = 0;
        ddlAdvisor.SelectedIndex = 0;
        lvFaculty.DataSource = null;
        lvFaculty.DataBind();
        lvFaculty.Visible = false;

    }
    //On select of Branch bind SEMESTER NAME  in drop down list
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedValue == "99")
        {
            //Semester
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO in (1,2)", "SEMESTERNO");
        }

        ddlSemester.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlAdvisor.SelectedIndex = 0;
        lvFaculty.DataSource = null;
        lvFaculty.DataBind();
        lvFaculty.Visible = false;

    }
    //On select of Section ddlAdvisor drop down list will be 0th index and lvFaculty list view null.
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlAdvisor.SelectedIndex = 0;
        lvFaculty.DataSource = null;
        lvFaculty.DataBind();
        lvFaculty.Visible = false;
    }
    protected void ddlAdvisor_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lvFaculty.DataSource = null;
        //lvFaculty.DataBind();
        //lvFaculty.Visible = false;
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAssignFA0.Enabled = false;
        if (ddlCollege.SelectedIndex > 0)
        {
            btnAssignFA0.Enabled = false;
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollege.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();

                ViewState["YearWise"] = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "ISNULL(YEARWISE,0)YEARWISE", "DEGREENO=" + ViewState["degreeno"].ToString()));

                if (ViewState["YearWise"].ToString() == "1")
                {
                    objCommon.FillDropDownList(ddlSemester, "ACD_YEAR", "SEM", "YEARNAME", "YEAR<>0", "YEAR");
                }
                else
                {
                    //lblDYSemester.Text = "Semester / Year";
                    objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
                }
                ddlAdvisor.Items.Clear();
                ddlAdvisor.Items.Add("Please Select");
                ddlAdvisor.SelectedItem.Value = "0";
                ddlAdvisor.Enabled = false;
                btnAssignFA0.Enabled = false;
            }
            ddlSemester.SelectedIndex = 0;
            ddlSemester.Focus();
            ddlSection.SelectedIndex = 0;
            ddlAdvisor.SelectedIndex = 0;
            lvFaculty.DataSource = null;
            lvFaculty.DataBind();
            lvFaculty.Visible = false;
        }
        else
        {
            ddlSemester.SelectedIndex = 0;
            objCommon.DisplayMessage("Please Select College & Regulation", this.Page);
            ddlCollege.Focus();
            ddlSection.SelectedIndex = 0;
            ddlAdvisor.SelectedIndex = 0;
            btnAssignFA0.Enabled = false;
            lvFaculty.DataSource = null;
            lvFaculty.DataBind();
            lvFaculty.Visible = false;
        }
    }
}
