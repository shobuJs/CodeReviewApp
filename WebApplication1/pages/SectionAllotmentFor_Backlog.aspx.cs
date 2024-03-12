using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class SectionAllotmentFor_Backlog : System.Web.UI.Page
{
    #region Page Events
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
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }
                PopulateDropDownList();
                btnSubmit.Enabled = false;
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
                Response.Redirect("~/notauthorized.aspx?page=SectionAllotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SectionAllotment.aspx");
        }
    }
    #endregion

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.BindListView();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.Clear();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        {
            try
            {
                StudentController objSC = new StudentController();
                string studids = string.Empty;
                string sections = string.Empty;
                string rollnos = string.Empty;

                int sessiono = Convert.ToInt32(ddlsession.SelectedValue);
                int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
                int branchno = Convert.ToInt32(ddlBranch.SelectedValue);
                int schemeno = Convert.ToInt32(ddlscheme.SelectedValue);
                int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                int courseno = Convert.ToInt32(ddlcourse.SelectedValue);
                int userno = Convert.ToInt32(Session["userno"]);

                if (sessiono > 0 && degreeno > 0 & branchno > 0 && schemeno > 0 && semesterno > 0 && courseno > 0)
                {
                    foreach (ListViewDataItem lvItem in lvStudents.Items)
                    {
                        if (Convert.ToInt32((lvItem.FindControl("ddlsec") as DropDownList).SelectedValue) > 0)
                        {
                            studids += (lvItem.FindControl("cbRow") as CheckBox).ToolTip + "$";
                            sections += (lvItem.FindControl("ddlsec") as DropDownList).SelectedValue + "$";
                            rollnos += (lvItem.FindControl("txtRollNo") as TextBox).Text + "$";
                        }
                    }
                    if (studids.Length <= 0 && sections.Length <= 0)
                    {
                        objCommon.DisplayMessage(this.updSection, "Please Select Student/Section", this.Page);
                        return;
                    }
                    if (objSC.UpdateStudentCourseWiseSection(sessiono, degreeno, branchno, schemeno, semesterno, courseno, studids, sections, rollnos, userno) == Convert.ToInt32(CustomStatus.RecordUpdated))
                    {
                        this.BindListView();
                        objCommon.DisplayMessage(this.updSection, "Student Section Alloted Successfully!!!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updSection, "Server Error...", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updSection, "Please Select Session/Degree/Branch/Scheme/Semester/Course!!", this.Page);
                }
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "ACADEMIC_CourseWiseSectionAllotment.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    #region Private Methods
    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlsession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO desc");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_SectionAllotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    private void BindListView()
    {
        try
        {
            //Fill Student List
            hdfTot.Value = "0";
            txtTotStud.Text = "0";
            DataSet ds = null;

            ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR INNER JOIN ACD_COURSE AC ON(SR.COURSENO=AC.COURSENO AND SR.SCHEMENO=AC.SCHEMENO AND SR.SEMESTERNO=AC.SEMESTERNO) INNER JOIN ACD_STUDENT ST ON(ST.IDNO=SR.IDNO) LEFT OUTER JOIN ACD_SECTION SC ON (SR.SECTIONNO = SC.SECTIONNO)", "DISTINCT ST.IDNO", "ST.REGNO,ST.STUDNAME,ISNULL(SR.SECTIONNO,0)SECTIONNO,SC.SECTIONNAME,SR.SESSIONNO,SR.SCHEMENO,SR.SEMESTERNO,SR.COURSENO,SR.ROLL_NO", "SR.PREV_STATUS= 1 AND SR.SESSIONNO=" + Convert.ToInt32(ddlsession.SelectedValue) + "AND SR.SCHEMENO=" + Convert.ToInt32(ddlscheme.SelectedValue) + " AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "AND SR.COURSENO=" + Convert.ToInt32(ddlcourse.SelectedValue) + "AND SR.EXAM_REGISTERED=1 AND ISNULL(SR.CANCEL,0)=0", "ST.REGNO");

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvStudents.DataSource = ds;
                    lvStudents.DataBind();
                    hdfTot.Value = ds.Tables[0].Rows.Count.ToString();
                    btnSubmit.Enabled = true;
                }
                else
                {
                    lvStudents.DataSource = null;
                    lvStudents.DataBind();
                    hdfTot.Value = "0";
                    objCommon.DisplayMessage(this.updSection, "No Students found for selected criteria!", this.Page);
                    btnSubmit.Enabled = false;
                }
            }
            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                hdfTot.Value = "0";
                objCommon.DisplayMessage(this.updSection, "No Students found for selected criteria!", this.Page);
                btnSubmit.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_SectionAllotment.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        ddlsession.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        txtTotStud.Text = "0";
        hdfTot.Value = "0";
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        btnSubmit.Enabled = false;
    }
    #endregion

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");

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

    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlsec = e.Item.FindControl("ddlsec") as DropDownList;
            DataSet ds = objCommon.FillDropDown("ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNAME");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTableReader dtr = ds.Tables[0].CreateDataReader();
                while (dtr.Read())
                {
                    ddlsec.Items.Add(new ListItem(dtr["SECTIONNAME"].ToString(), dtr["SECTIONNO"].ToString()));
                }
            }
            ddlsec.SelectedValue = ddlsec.ToolTip;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_SectionAllotment.lvStudents_ItemDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlscheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", " DEGREENO =" + ddlDegree.SelectedValue + " and BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");

        }
        else
        {
            ddlscheme.Items.Clear();

        }


    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlcourse, "ACD_STUDENT_RESULT SR INNER JOIN ACD_COURSE AC ON(SR.COURSENO=AC.COURSENO AND SR.SCHEMENO=AC.SCHEMENO AND SR.SEMESTERNO=AC.SEMESTERNO)", "DISTINCT AC.COURSENO", "AC.CCODE+ ' - ' +AC.COURSE_NAME", "SR.PREV_STATUS= 1 AND SR.SESSIONNO=" + Convert.ToInt32(ddlsession.SelectedValue) + "AND SR.SCHEMENO = " + ddlscheme.SelectedValue + "AND SR.SEMESTERNO = " + Convert.ToInt32(ddlSemester.SelectedValue) + "AND SR.EXAM_REGISTERED=1 AND ISNULL(SR.CANCEL,0)=0", "AC.COURSENO");


        }
        else
        {
            ddlcourse.Items.Clear();

        }
    }

    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER AE ON (SR.SEMESTERNO=AE.SEMESTERNO )", "DISTINCT SR.SEMESTERNO", "AE.SEMESTERNAME", "PREV_STATUS= 1 AND SR.SESSIONNO=" + Convert.ToInt32(ddlsession.SelectedValue) + "AND SR.SCHEMENO = " + Convert.ToInt32(ddlscheme.SelectedValue) + "", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
        }
        else
        {
            ddlSemester.Items.Clear();
        }

    }
}

