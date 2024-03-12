
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

public partial class ACADEMIC_Electiveregistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    #region Pageload
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
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }

                objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNAME DESC");
                ddlSession.SelectedIndex = 1;
                int mul_col_flag = Convert.ToInt32(objCommon.LookUp("REFF", "MUL_COL_FLAG", "CollegeName='" + Page.Title + "'"));
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
                this.PopulateDropDown();

                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
        }
        divMsg.InnerHtml = string.Empty;
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkRegistration.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            //if (Session["usertype"].ToString() != "1")
            //    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "COLLEGE_NAME", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "C.COLLEGE_ID");
            //else
            //    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");



            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO >0", "BRANCHNO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_BulkRegistration.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion Pageload

    #region submit
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string roll = string.Empty;
        StudentRegistration objSRegist = new StudentRegistration();
        StudentRegist objSR = new StudentRegist();
        CheckBox cbRow = new CheckBox();

        try
        {
            foreach (ListViewDataItem dataitem in lvStudent.Items)
            {
                //Get Student Details from lvStudent
                cbRow = dataitem.FindControl("cbRow") as CheckBox;
                if (cbRow.Checked == true && cbRow.Enabled == true)
                {
                    objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                    objSR.IDNO = Convert.ToInt32(((dataitem.FindControl("lblIDNo")) as Label).ToolTip);
                    objSR.REGNO = ((dataitem.FindControl("lblIDNo")) as Label).Text;
                    objSR.SEMESTERNO = Convert.ToInt32(ddlSemester.SelectedValue);
                    objSR.SCHEMENO = Convert.ToInt32(ddlScheme.SelectedValue);
                    objSR.IPADDRESS = ViewState["ipAddress"].ToString();
                    objSR.COLLEGE_CODE = Session["colcode"].ToString();
                    objSR.UA_NO = Convert.ToInt32(Session["userno"]);

                    objSR.COURSENOS = ddlcourselist.SelectedValue;
                    objSR.ELECTIVE = "1";
                    objSR.ACEEPTSUB = "1";

                    //Register Single Student
                    int prev_status = 0;    //Regular Courses
                    CustomStatus cs = (CustomStatus)objSRegist.AddRegisteredSubjectsBulkElective(objSR, prev_status);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objSR.COURSENOS = string.Empty;
                        objSR.ACEEPTSUB = string.Empty;
                    }
                }
            }
            objCommon.DisplayMessage(updBulkReg, "Student(s) Registered Successfully!!", this.Page);
            clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_BulkRegistration.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void clear()
    {
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        ddlSchemeType.SelectedIndex = 0;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        pnlCourses.Visible = false;
        pnlStudents.Visible = false;
        divcourse.Visible = false;
        ddlcourselist.SelectedIndex = 0;
        ddlcourselist.SelectedIndex = 0;
        lvStudentsRemain.DataSource = null;
        lvStudentsRemain.DataBind();
        pnlStudentsReamin.Visible = false;
        btnSubmit.Enabled = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        pnlCourses.Visible = false;
        pnlStudents.Visible = false;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        LinkButton printButton = sender as LinkButton;
        int idno = Int32.Parse(printButton.CommandArgument);
        ViewState["IDNO"] = idno;
        int count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(*)", "IDNO=" + idno + " AND SESSIONNO = " + ddlSession.SelectedValue));
        if (count > 0)
        {
            objCommon.DisplayMessage("This Student is already registered for session " + ddlSession.SelectedItem.Text, this.Page);
            ShowReport("Registered_Courses", "rptPreRegSlip.rpt");
        }
        else
        {
            objCommon.DisplayMessage("This Student is not registered for session " + ddlSession.SelectedItem.Text, this.Page);
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("RegistrationSlip", "rptBulkCourseRegslipelective.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue + ",@UserName=" + Session["username"] + ",@P_SECTIONNO=" + ddlSection.SelectedValue;

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updBulkReg, this.updBulkReg.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion submit

    #region dropdown
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvStudentsRemain.DataSource = null;
        lvStudentsRemain.DataBind();
        divcourse.Visible = false;
        ddlcourselist.SelectedIndex = 0;
        pnlCourses.Visible = false;
        pnlStudents.Visible = false;
        btnSubmit.Enabled = false;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlStatus.SelectedIndex = 0;
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        btnSubmit.Enabled = false;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        string uano = Session["userno"].ToString();
        string uatype = Session["usertype"].ToString();
        string dept = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(uano));
        if (ddlDegree.SelectedIndex > 0)
        {
            ddlBranch.Items.Clear();

            //if (Session["usertype"].ToString() != "1")
            //    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
            //else
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");


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


            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlStatus.SelectedIndex = 0;
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlStatus.SelectedIndex = 0;
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvStudentsRemain.DataSource = null;
        lvStudentsRemain.DataBind();
        pnlCourses.Visible = false;
        divcourse.Visible = false;
        ddlcourselist.SelectedIndex = 0;
        pnlStudents.Visible = false;
        pnlStudentsReamin.Visible = false;
        btnSubmit.Enabled = false;
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlScheme.Items.Clear();
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO");
            ddlScheme.Focus();
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlStatus.SelectedIndex = 0;
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }
        else
        {
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlStatus.SelectedIndex = 0;
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvStudentsRemain.DataSource = null;
        lvStudentsRemain.DataBind();
        pnlCourses.Visible = false;
        divcourse.Visible = false;
        ddlcourselist.SelectedIndex = 0;
        pnlStudents.Visible = false;
        pnlStudentsReamin.Visible = false;
        btnSubmit.Enabled = false;
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                int yearwise = objCommon.LookUp("ACD_DEGREE", "YEARWISE", "DEGREENO=" + ddlDegree.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "YEARWISE", "DEGREENO=" + ddlDegree.SelectedValue));
                ddlSemester.Items.Clear();

                if (yearwise == 1)
                {
                    objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_YEAR Y ON(S.YEAR=Y.YEAR) LEFT OUTER JOIN ACD_SEM_PROMOTION SP ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT S.YEAR", "Y.YEARNAME", "S.DEGREENO=" + ddlDegree.SelectedValue + " AND S.SCHEMENO=" + ddlScheme.SelectedValue, "S.YEAR");
                }
                else
                {
                    objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO=" + ddlDegree.SelectedValue + " AND S.SCHEMENO=" + ddlScheme.SelectedValue, "SM.SEMESTERNO");
                }

                ddlSemester.Focus();
                ddlStatus.SelectedIndex = 0;
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
            }
            else
            {
                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Please Select", "0"));
                ddlStatus.SelectedIndex = 0;
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
            }
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lvStudentsRemain.DataSource = null;
            lvStudentsRemain.DataBind();
            pnlCourses.Visible = false;
            btnSubmit.Enabled = false;
            divcourse.Visible = false;
            ddlcourselist.SelectedIndex = 0;
            pnlStudents.Visible = false;
            pnlStudentsReamin.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_BulkRegistration.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            if (ddlBranch.SelectedIndex <= 0 || ddlScheme.SelectedIndex <= 0)
            {
                objCommon.DisplayMessage("Please Select Branch/Scheme", this.Page);
                return;
            }
            else
            {
                objCommon.FillDropDownList(ddlSection, "ACD_STUDENT ST INNER JOIN  ACD_SECTION  S ON (ST.SECTIONNO = S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "ST.Schemeno = " + ddlScheme.SelectedValue + " AND ST.Semesterno= " + ddlSemester.SelectedValue, "S.SECTIONNO"); //added by reena on  4_10_16
            }
        }
        pnlCourses.Visible = false;
        pnlStudents.Visible = false;
        btnSubmit.Enabled = false;
        divcourse.Visible = false;
        ddlcourselist.SelectedIndex = 0;


    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex <= 0 || ddlScheme.SelectedIndex <= 0)
        {
            objCommon.DisplayMessage("Please Select Semester/Scheme", this.Page);
            return;
        }
        else
        {
            this.BindListView();
        }
    }

    #endregion dropdown

    #region User Defined Methods

    public int cInt(string strInteger)
    {
        int i = 0; int.TryParse(strInteger, out i); return i;
    }

    StudentController objSC = new StudentController();
    DataSet ds = null;

    private void BindListView()
    {
        //Get student list as per scheme & semester & secion(AND SECTIONNO = " + ddlSection.SelectedValue)



        objCommon.FillDropDownList(ddlcourselist, "ACD_OFFERED_COURSE OC INNER JOIN ACD_COURSE C ON C.COURSENO=OC.COURSENO AND OC.SCHEMENO=C.SCHEMENO", "DISTINCT OC.COURSENO", "OC.CCODE +'-'+C.COURSE_NAME", "OC.SCHEMENO = " + ddlScheme.SelectedValue + " AND OC.SEMESTERNO = " + ddlSemester.SelectedValue + " AND ISNULL(OC.elect,0)=1", "OC.COURSENO");
        DataSet dsCourse = objCommon.FillDropDown("ACD_OFFERED_COURSE OC INNER JOIN ACD_COURSE C ON C.COURSENO=OC.COURSENO AND OC.SCHEMENO=C.SCHEMENO", "DISTINCT OC.COURSENO", "OC.CCODE,C.COURSE_NAME,OC.ELECT", "OC.SCHEMENO = " + ddlScheme.SelectedValue + " AND OC.SEMESTERNO = " + ddlSemester.SelectedValue + " AND ISNULL(OC.elect,0) = 1", "OC.COURSENO");
        if (dsCourse != null && dsCourse.Tables.Count > 0)
        {
            if (dsCourse.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = dsCourse;
                lvCourse.DataBind();
                divcourse.Visible = true;
                pnlCourses.Visible = true;


                ds = objSC.GetStudentsBySchemeElective(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlSchemeType.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue));

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lvStudent.DataSource = ds.Tables[0];
                        lvStudent.DataBind();
                        pnlStudents.Visible = true;
                        btnSubmit.Enabled = true;
                        hftot.Value = ds.Tables[0].Rows.Count.ToString();

                        foreach (ListViewDataItem item in lvStudent.Items)
                        {
                            CheckBox chkBox = item.FindControl("cbRow") as CheckBox;
                            String lblReg = (item.FindControl("lblStudName") as Label).ToolTip;

                            lblReg = string.Empty;
                        }
                    }
                    else
                    {
                        lvCourse.DataSource = null;
                        divcourse.Visible = false;
                        lvCourse.DataBind();
                        pnlCourses.Visible = false;
                        objCommon.DisplayUserMessage(this.updBulkReg, "No Students Found!", this.Page);
                    }
                }
                else
                {
                    lvCourse.DataSource = null;
                    lvCourse.DataBind();
                    pnlCourses.Visible = false;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.updBulkReg, "No Elective courses Found!", this.Page);
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                btnSubmit.Enabled = false;
                pnlStudents.Visible = false;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.updBulkReg, "No Record Found!", this.Page);
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            pnlStudents.Visible = false;
            btnSubmit.Enabled = false;
        }
    }


    protected void lvCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label elective = (e.Item.FindControl("lblCourseName")) as Label;
        if (elective.ToolTip == "False")
        {
            ((e.Item.FindControl("cbRow")) as CheckBox).Checked = true;
        }
        else
        {
            ((e.Item.FindControl("cbRow")) as CheckBox).Checked = false;
        }
    }

    #endregion
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    protected void ddlcourselist_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            ds = objSC.GetStudentsBySchemeElective(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlSchemeType.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue));

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvStudent.DataSource = ds.Tables[0];
                    lvStudent.DataBind();
                    pnlStudents.Visible = true;
                    btnSubmit.Enabled = true;
                    hftot.Value = ds.Tables[0].Rows.Count.ToString();

                    foreach (ListViewDataItem item in lvStudent.Items)
                    {
                        CheckBox chkBox = item.FindControl("cbRow") as CheckBox;
                        String lblReg = (item.FindControl("lblStudName") as Label).ToolTip;

                        lblReg = string.Empty;
                    }
                }
                else
                {
                    lvCourse.DataSource = null;
                    divcourse.Visible = false;
                    lvCourse.DataBind();
                    pnlCourses.Visible = false;
                    objCommon.DisplayUserMessage(this.updBulkReg, "No Students Found!", this.Page);
                }
            }
            else
            {
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                pnlCourses.Visible = false;
            }

            SQLHelper objsql = new IITMS.SQLServer.SQLDAL.SQLHelper(_nitprm_constr);
            //to get selected sections
            DataSet dsGetStudents = objsql.ExecuteDataSet("select IDNO from ACD_STUDENT_RESULT T  where T.COURSENO=" + ddlcourselist.SelectedValue + " and SESSIONNO=" + ddlSession.SelectedValue + " and SEMESTERNO=" + ddlSemester.SelectedValue + " and ISNULL(CANCEL,0)=0 AND SECTIONNO=" + ddlSection.SelectedValue + "and SCHEMENO=" + ddlScheme.SelectedValue);
            // DataSet dsGetADTeacher = objsql.ExecuteDataSet("select S.SECTIONNO,S.SECTIONNAME,T.IS_ADTEACHER from ACD_Course_TEACHER T inner join ACD_SECTION S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip);
            for (int i = 0; i < dsGetStudents.Tables[0].Rows.Count; i++)
            {
                foreach (ListViewDataItem ditem in lvStudent.Items)
                {
                    CheckBox chk = (CheckBox)ditem.FindControl("cbRow");

                    if (dsGetStudents.Tables[0].Rows[i]["IDNO"].ToString() == chk.ToolTip)
                    {
                        chk.Checked = true;
                        chk.Enabled = false;
                    }
                }
            }
        }
        catch
        {
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSession.SelectedIndex = 0;
        ddlSession.Items.Clear();

        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND COLLEGE_ID=" + ddlCollege.SelectedValue, "SESSIONNO DESC");


            if (Session["usertype"].ToString() != "1")
            {
                string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
            }
            else
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENO");
            }

        }
        else
        {
            ddlSession.Items.Insert(0, "Please Select");
        }
    }

}
