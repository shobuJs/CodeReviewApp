//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : COURSE ALLOTMENT                                      
// CREATION DATE : 15-OCT-2011
// ADDED BY      : ASHISH DHAKATE                                                  
// MODIFIED DATE : 12-DEC-2010 
// MODIFIED BY   : Renuka P. Adulkar
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

public partial class ACADEMIC_BackBulkRegistration : System.Web.UI.Page
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
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "TOP 1 SESSIONNO", "SESSION_PNAME", "SESSIONNO>0", "SESSIONNO DESC");
                objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
                ddlSession.SelectedIndex = 1;
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
                    objSR.SCHEMENO = Convert.ToInt32(ddlScheme.SelectedValue);
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

                    //Register Single Student
                    int prev_status = 0;    //Regular Courses
                    CustomStatus cs = (CustomStatus)objSRegist.AddRegisteredSubjectsBulkBACK(objSR, prev_status);
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
        lvStudentsRemain.DataSource = null;
        lvStudentsRemain.DataBind();
        pnlStudentsReamin.Visible = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        pnlCourses.Visible = false;
        pnlStudents.Visible = false;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        string uano = Session["userno"].ToString();
        string uatype = Session["usertype"].ToString();
        string dept = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(uano));
        if (ddlDegree.SelectedIndex > 0)
        {
            ddlBranch.Items.Clear();
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
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
        pnlStudents.Visible = false;
        pnlStudentsReamin.Visible = false;
        btnSubmit.Enabled = false;
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            if (ddlBranch.SelectedIndex <= 0 || ddlScheme.SelectedIndex <= 0)
            {
                objCommon.DisplayMessage("Please Select Branch/Regulation", this.Page);
                return;
            }

            this.BindListView();
        }
        else
        {
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            pnlStudents.Visible = false;

            lvCourse.DataSource = null;
            lvCourse.DataBind();
            pnlCourses.Visible = false;

            lvStudentsRemain.DataSource = null;
            lvStudentsRemain.DataBind();
            pnlStudentsReamin.Visible = false;

        }
    }

    #region User Defined Methods
    public int cInt(string strInteger)
    {
        int i = 0; int.TryParse(strInteger, out i); return i;
    }
    private void BindListView()
    {
        StudentController objSC = new StudentController();
        DataSet ds = null;

        ds = objSC.GetStudentsBySchemeBACK(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlSchemeType.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds.Tables[0];
                lvStudent.DataBind();
                pnlStudents.Visible = true;
                btnSubmit.Enabled = true;
                hftot.Value = ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                btnSubmit.Enabled = false;
                pnlStudents.Visible = false;
            }
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chkBox = item.FindControl("cbRow") as CheckBox;
                String lblReg = (item.FindControl("lblStudName") as Label).ToolTip;

                if (lblReg == "1")  //IF STUDENT IS ALREADY REGISTERED THEN CHECKBOX WILL BE DISABLED
                {
                    chkBox.Enabled = false;
                }
                lblReg = string.Empty;
            }

        }
        else
        {
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            pnlStudents.Visible = false;
            btnSubmit.Enabled = false;
        }

        DataSet dsCourse = objCommon.FillDropDown("ACD_COURSE", "COURSENO", "CCODE,COURSE_NAME,ELECT", "SCHEMENO = " + ddlScheme.SelectedValue + " AND SEMESTERNO = " + ddlSemester.SelectedValue + " AND OFFERED = 0 ", "COURSENO");
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

    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0 AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "C.COLLEGE_ID");
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
    #endregion
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
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
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
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlStatus.SelectedIndex = 0;
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        btnSubmit.Enabled = false;
    }



    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C, ACD_COLLEGE_DEGREE_BRANCH CD", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND CD.DEGREENO=D.DEGREENO AND C.DEGREENO>0 AND (C.COLLEGE_ID=" + ddlCollege.SelectedValue + " OR " + ddlCollege.SelectedValue + "= 0) AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "DEGREENO");
    }
}
