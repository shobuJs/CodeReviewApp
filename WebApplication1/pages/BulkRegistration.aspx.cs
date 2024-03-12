
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_BulkRegistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    #region PageLoad
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
                ddlAdmBatch.Focus();
                int mul_col_flag = Convert.ToInt32(objCommon.LookUp("REFF", "MUL_COL_FLAG", "CollegeName='" + Page.Title + "'"));
                if (mul_col_flag == 0)
                {
                   
                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0", "COLLEGE_ID");
                    ddlCollege.SelectedIndex = 1;
                }
                else
                {
             
                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0", "COLLEGE_ID");
                    ddlCollege.SelectedIndex = 0;
                }
                ViewState["degreeno"] = 0;
                ViewState["branchno"] = 0;
                ViewState["college_id"] = 0;
                ViewState["schemeno"] = 0;
                this.PopulateDropDown();

                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                objCommon.SetLabelData("1");
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
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
            //Check for Authorization of Page
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
      
            if (Session["usertype"].ToString() != "1")
            {
                string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
            }
            else
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0", "D.DEGREENO");
            }

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

    #endregion PageLoad

    #region dropdown

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlSession.SelectedIndex > 0)
        {

                ddlSemester.Items.Clear();
               // objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO=" + ViewState["degreeno"].ToString() + " AND S.SCHEMENO=" + ViewState["schemeno"].ToString(), "SM.SEMESTERNO");

                int SCHEMNO = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "SCHEMENO", "COSCHNO=" + Convert.ToInt32(ddlCollege.SelectedValue)));
                int Scheme_type = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "SCHEMETYPE", "SCHEMENO=" + Convert.ToInt32(SCHEMNO)));
                ddlSemester.Items.Clear();
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "SCHEMETYPENO=" + Scheme_type + "AND S.DEGREENO=" + ViewState["degreeno"].ToString() + " AND S.SCHEMENO=" + ViewState["schemeno"].ToString(), "SM.SEMESTERNO");
            
            
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
        pnlStudents.Visible = false;
        btnSubmit.Enabled = false;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));

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

            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ViewState["degreeno"].ToString(), "A.LONGNAME");

            DataSet ds = objCommon.FillDropDown("ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ViewState["degreeno"].ToString() + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
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
        pnlStudents.Visible = false;
        pnlStudentsReamin.Visible = false;
        btnSubmit.Enabled = false;
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlScheme.Items.Clear();
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ViewState["degreeno"].ToString() + " AND BRANCHNO = " + ViewState["branchno"].ToString(), "SCHEMENO");
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
                ddlSemester.Items.Clear();
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO=" + ddlDegree.SelectedValue + " AND S.SCHEMENO=" + ddlScheme.SelectedValue, "SM.SEMESTERNO");
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
            pnlStudents.Visible = false;
            pnlStudentsReamin.Visible = false;
            btnSubmit.Enabled = false;
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
            objCommon.FillDropDownList(ddlSection, "ACD_STUDENT ST INNER JOIN  ACD_SECTION  S ON (ST.SECTIONNO = S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "ST.Schemeno = " + ViewState["schemeno"].ToString() + " AND ST.Semesterno= " + ddlSemester.SelectedValue, "S.SECTIONNO"); //added by reena on  4_10_16
            ddlSection.Focus();
        }
        else
        {
            ddlSection.SelectedIndex = 0;
            objCommon.DisplayMessage("Please Select Semester", this.Page);
            ddlSemester.Focus();
        }

    }

    #endregion dowpdown

    #region User Defined Methods

    protected void btnShow_Click(object sender, EventArgs e)
    {
        txtTotStud.Text = "0";
        ViewState["orderBy"] = 0;
        this.BindListView();
    }

    public int cInt(string strInteger)
    {
        int i = 0; int.TryParse(strInteger, out i); return i;
    }

    private void BindListView()
    {
        StudentController objSC = new StudentController();
        DataSet ds = null;

        int scheme = Convert.ToInt32(ViewState["schemeno"].ToString());
        int orderby = 0;
        if (rbSortBy.SelectedValue == "1")
        {
            orderby = 1;
        }
        else if (rbSortBy.SelectedValue == "2")
        {
            orderby = 2;

        }
        else if (rbSortBy.SelectedValue == "3")
        {
            orderby = 3;
        }
        else
        {
            orderby = 0;
        }
      
        
     
        ds = objSC.GetStudentsByScheme(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), Convert.ToInt32(ViewState["degreeno"].ToString()), Convert.ToInt32(ddlSchemeType.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), Convert.ToInt32(ViewState["college_id"].ToString()), Convert.ToInt32(ddlSection.SelectedValue),Convert.ToInt32(ddlRegisttype.SelectedValue),orderby);
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds.Tables[0];
                lvStudent.DataBind();
                pnlStudents.Visible = true;
                btnSubmit.Enabled = true;
                hftot.Value = ds.Tables[0].Rows.Count.ToString();

                //for getting student list semester wise
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chkBox = item.FindControl("cbRow") as CheckBox;
                    String lblReg = (item.FindControl("lblStudName") as Label).ToolTip;

                    if (lblReg == "1")  //IF STUDENT IS ALREADY REGISTERED THEN CHECKBOX WILL BE DISABLED
                    {
                        chkBox.Enabled = false;
                        chkBox.BackColor = System.Drawing.Color.Green;
                    }
                    lblReg = string.Empty;
                }

               
                DataSet dsCourse = objCommon.FillDropDown("ACD_OFFERED_COURSE OC LEFT JOIN ACD_COURSE C ON C.COURSENO=OC.COURSENO", "DISTINCT OC.COURSENO", "OC.CCODE,REVISION_COURSE_NAME as COURSE_NAME,OC.ELECT", "OC.SCHEMENO = " + ViewState["schemeno"].ToString() + " AND OC.SESSIONNO = " + ddlSession.SelectedValue + " AND OC.SEMESTERNO = " + ddlSemester.SelectedValue, "OC.COURSENO");
              
                if (dsCourse != null && dsCourse.Tables.Count > 0)
                {
                    if (dsCourse.Tables[0].Rows.Count > 0)
                    {
                        lvCourse.DataSource = dsCourse;
                        lvCourse.DataBind();
                        pnlCourses.Visible = true;
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
                    lvCourse.DataSource = null;
                    lvCourse.DataBind();
                    pnlCourses.Visible = false;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.updBulkReg, "No Record Found!", this.Page);
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
            ((e.Item.FindControl("cbRow")) as CheckBox).Checked = true;
        }
    }
    #endregion

    #region transaction
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
                if (cbRow.Checked == true)
                {
                    objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                    objSR.IDNO = Convert.ToInt32(((dataitem.FindControl("lblIDNo")) as Label).ToolTip);
                    objSR.REGNO = ((dataitem.FindControl("lblIDNo")) as Label).Text;
                    objSR.SEMESTERNO = Convert.ToInt32(ddlSemester.SelectedValue);
                    objSR.SCHEMENO = Convert.ToInt32(ViewState["schemeno"].ToString());
                    objSR.IPADDRESS = ViewState["ipAddress"].ToString();
                    objSR.COLLEGE_CODE = Session["colcode"].ToString();
                    objSR.UA_NO = Convert.ToInt32(Session["userno"]);

                    //Get Course Details
                    foreach (ListViewDataItem dataitemCourse in lvCourse.Items)
                    {
                        if (((dataitemCourse.FindControl("cbRow")) as CheckBox).Checked == true)
                        {
                            objSR.COURSENOS += ((dataitemCourse.FindControl("lblCCode")) as Label).ToolTip + "$";
                            Label elective = (dataitemCourse.FindControl("lblCourseName")) as Label;
                            if (elective.ToolTip == "False")
                            {
                                objSR.ELECTIVE += "0" + "$";
                            }
                            else
                            {
                                objSR.ELECTIVE += "1" + "$";
                            }
                        }
                    }

                    objSR.ACEEPTSUB = "1";

                    if (!(objSR.COURSENOS == null) && objSR.COURSENOS.Length > 0)
                    {

                        //Register Single Student
                        int prev_status = 0;    //Regular Courses
                        CustomStatus cs = (CustomStatus)objSRegist.AddRegisteredSubjectsBulk(objSR, prev_status);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objSR.COURSENOS = string.Empty;
                            objSR.ACEEPTSUB = string.Empty;
                        }

                    }
                    else
                    {
                        objCommon.DisplayMessage(updBulkReg, "Please Select atleast One Subject in Subject list for Subject Registration..!", this.Page);
                        return;
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        pnlCourses.Visible = false;
        pnlStudents.Visible = false;
    }

    protected void clear()
    {
        ddlCollege.SelectedIndex = 0;
        txtTotStud.Text = "0";
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        ddlSchemeType.SelectedIndex = 0;
        ddlRegisttype.SelectedIndex = 0;

        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        pnlCourses.Visible = false;
        pnlStudents.Visible = false;
        lvStudentsRemain.DataSource = null;
        lvStudentsRemain.DataBind();
        pnlStudentsReamin.Visible = false;
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
        ShowReport("RegistrationSlip", "rptBulkCourseRegslip.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";

            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];

            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ViewState["schemeno"].ToString() + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"].ToString() + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue + ",@UserName=" + Session["username"] + ",@P_COLLEGE_ID=" + ViewState["college_id"].ToString(); //+ ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue)

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



    #endregion transaction

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

               
             //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO DESC");

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER A INNER JOIN ACD_SESSION_MAPPING B ON (A.SESSIONNO=B.SESSIONNO)", "A.SESSIONNO", "SESSION_PNAME", "A.SESSIONNO>0 AND B.COLLEGE_ID=" + ViewState["college_id"].ToString(), "A.SESSIONNO DESC");


            }           
        }
        else
        {
            ddlSession.SelectedIndex = 0;
            objCommon.DisplayMessage("Please Select College & Regulation", this.Page);
            ddlCollege.Focus();
        }
    }
    protected void rbSortBy_SelectedIndexChanged(object sender, EventArgs e)
    {   
        BindListView();
    }

}
