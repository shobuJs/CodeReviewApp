using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessLogic;
using System.Transactions;
using CrystalDecisions.Shared;
using System.IO;
public partial class ACADEMIC_Course_Mapping : System.Web.UI.Page
{
    Common objCommon = new Common();

    CourseController objCourse = new CourseController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                }
                ViewState["action"] = "add";
                DropDownBind();
                //BindListView();
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACDEMIC_COLLEGE_MASTER.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Course_Mapping.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Course_Mapping.aspx");
        }
    }

    protected void OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        (lvCourse.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["DYNAMIC_DATASET"];
        lvCourse.DataSource = dt;
        lvCourse.DataBind();
        //this.BindListView();
    }

    protected void DropDownBind()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and isnull(ACTIVE,0)=1", "COLLEGE_ID");
        objCommon.FillListBox(ddlSubjectAL, "ACD_COURSE ", "DISTINCT COURSENO", "(CCODE+'-'+COURSE_NAME + ' ( '+ convert(varchar(11), LECTURE,0) + ' | '+ convert(varchar(11), PRACTICAL,0) + ' | '+ convert(varchar(11), CREDITS,0) + ' )')COURSENAME", "COURSENO>0 and isnull(ACTIVE,0)=1", "");
        objCommon.FillDropDownList(ddlSemister, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0", "SEMESTERNO");
        objCommon.FillDropDownList(ddlSubjectClassification, "ACD_SUBJECT_CLASSIFICATION", "DISTINCT SUBCLASSIFIC_NO", "SUBCLASSIFIC_NAME", "ISNULL(STATUS,0)=1", "SUBCLASSIFIC_NAME");
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindListView();
        objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "DISTINCT SCHEMENO", "SCHEMENAME", "SCHEMENO>0 AND COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "SCHEMENO");
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        ddlScheme.SelectedIndex = 0;
        ddlSubjectAL.ClearSelection();
        ddlCoreElective.SelectedIndex = 0;
        //lvCourse.Visible = false;
        ddlSemister.SelectedIndex = 0;
        ddlSubjectClassification.SelectedValue = "0";
        //ddlCollege.Enabled = true;
        ddlScheme.Enabled = true;
        txtSemHours.Text = string.Empty;
        ddlSubjectAL.Attributes.Remove("disabled");

    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex != 0)
        {

            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_MASTER CM inner join ACD_COLLEGE_DEPT CD ON(CM.COLLEGE_ID=CD.COLLEGE_ID) INNER JOIN  ACD_DEPARTMENT D ON(D.DEPTNO=CD.DEPTNO) INNER JOIN ACD_COLLEGE_DEGREE ACD ON(CD.COLLEGE_ID = ACD.COLLEGE_ID) INNER JOIN ACD_DEGREE AD ON(ACD.DEGREENO = AD.DEGREENO)",
                "DISTINCT AD.DEGREENO", "AD.DEGREENAME", "D.DEPTNO>0 AND D.DEPTNO=" + ddlDepartment.SelectedValue, "AD.DEGREENO");
            //BindListView();
        }
    }

    protected void BindListView()
    {
        ViewState["DYNAMIC_DATASET"] = null;
        try
        {

            DataSet ds = objCourse.GetCourseM(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = ds;
                lvCourse.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvCourse);//Set label 
                objCommon.SetListViewHeaderLabel(Convert.ToString(Request.QueryString["pageno"]), lvCourse);
                ViewState["DYNAMIC_DATASET"] = ds.Tables[0];
            }
            else
            {
                lvCourse.DataSource = null;
                lvCourse.DataBind();
            }
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_dePT.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int Active = 0;
            if (hfdStat.Value == "true")
            {
                Active = 1;
            }

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {

                    string module = string.Empty;
                    foreach (ListItem items in ddlSubjectAL.Items)
                    {
                        if (items.Selected == true)
                        {
                            module += items.Value + ',';

                        }
                    }
                    if (module == string.Empty)
                    {
                        objCommon.DisplayMessage(updGradeEntry, "Please Select Module", this.Page);
                        return;
                    }
                    module = module.ToString();
                    int _module = 0, ck = 0;
                    ck = objCourse.InsertCourseMapping(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), module, Convert.ToInt32(ddlCoreElective.SelectedValue), Convert.ToInt32(ddlSemister.SelectedValue), Convert.ToInt32(ddlSubjectClassification.SelectedValue), Active, txtSemHours.Text);
                    if (ck == 1)
                    {
                        objCommon.DisplayMessage(this.updGradeEntry, "Record Saved Successfully", this.Page);
                        BindListView();
                        ddlCollege.SelectedIndex = 0;
                        ddlCollege_SelectedIndexChanged(new object(), new EventArgs());
                        ddlScheme.SelectedIndex = 0;
                        ddlSubjectAL.ClearSelection();
                        ddlCoreElective.SelectedIndex = 0;
                        lvCourse.Visible = false;
                        ddlSemister.SelectedIndex = 0;
                        ddlSubjectClassification.SelectedValue = "0";
                        ddlCollege.Enabled = true;
                        ddlScheme.Enabled = true;
                        txtSemHours.Text = string.Empty;
                        ddlSubjectAL.Attributes.Remove("disabled");
                        return;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updGradeEntry, "Record Are Already Exists", this.Page);
                        ddlCollege.SelectedIndex = 0;
                        ddlCollege_SelectedIndexChanged(new object(), new EventArgs());
                        ddlScheme.SelectedIndex = 0;
                        ddlSubjectAL.ClearSelection();
                        ddlCoreElective.SelectedIndex = 0;
                        lvCourse.Visible = false;
                        ddlSemister.SelectedIndex = 0;
                        ddlSubjectClassification.SelectedValue = "0";
                        ddlCollege.Enabled = true;
                        ddlScheme.Enabled = true;
                        txtSemHours.Text = string.Empty;
                        ddlSubjectAL.Attributes.Remove("disabled");
                        ViewState["action"] = "add";
                        return;
                    }
                }
                else if (ViewState["action"].ToString().Equals("edit"))
                {
                    string SP_Name1 = "ACD_UPDATE_COURSE_MAPPING";
                    string SP_Parameters1 = "@P_COLLEGE_ID,@P_DEPTNO,@P_DEGREENO,@P_SCHEMENO,@P_COURSENO,@P_CORE_ELECTIVE,@P_SEMESTERNO,@P_COURSEN_MAPPING_NO,@P_SUB_CLASSIFICATION,@P_TOTSEMESTERHOURS,@P_STATUS,@P_OUT";
                    string Call_Values1 = "" + Convert.ToInt32(ddlCollege.SelectedValue) + "," + Convert.ToInt32(ddlDepartment.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlDegree.SelectedValue) + "," +
                        Convert.ToInt32(ddlScheme.SelectedValue) + "," + (ViewState["COURSENO"]) + "," + Convert.ToInt32(ddlCoreElective.SelectedValue) + "," + Convert.ToInt32(Convert.ToInt32(ddlSemister.SelectedValue)) + "," +
                        Convert.ToInt32(ViewState["COURSEN_MAPPING_NO"]) + "," + Convert.ToInt32(ddlSubjectClassification.SelectedValue) + "," + txtSemHours.Text + "," + Active + ",0";
                    string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//insert
                    if (que_out1 == "2")
                    {
                        objCommon.DisplayMessage(this.updGradeEntry, "Record Updated Successfully", this.Page);
                        BindListView();
                        ddlCollege.SelectedIndex = 0;
                        ddlCollege_SelectedIndexChanged(new object(), new EventArgs());
                        ddlScheme.SelectedIndex = 0;
                        ddlSubjectAL.ClearSelection();
                        ddlCoreElective.SelectedIndex = 0;
                        ddlSemister.SelectedIndex = 0;
                        ddlSubjectClassification.SelectedValue = "0";
                        ddlCollege.Enabled = true;
                        ddlScheme.Enabled = true;
                        ddlSubjectAL.Attributes.Remove("disabled");
                        txtSemHours.Text = string.Empty;
                        ViewState["action"] = "add";
                        return;
                    }
                    else if (que_out1 == "3")
                    {
                        objCommon.DisplayMessage(this.updGradeEntry, "Already Module Registered.!!!", this.Page);
                        return;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updGradeEntry, "Error", this.Page);
                        return;
                    }
                }
            }
        }
        catch { }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        ddlSubjectAL.Attributes.Remove("disabled");
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView();
        lvCourse.Visible = true;
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["action"] = "edit";
            int COURSEN_MAPPING_NO = int.Parse(btnEdit.CommandArgument);
            ViewState["COURSEN_MAPPING_NO"] = COURSEN_MAPPING_NO;
            ViewState["COURSENO"] = int.Parse(btnEdit.CommandName);
            ShowDetails(COURSEN_MAPPING_NO);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_CollegeMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int COURSEN_MAPPING_NO)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_COURSE_MAPPING", "ISNULL(SUBCLASSIFIC_NO,0) AS SUBCLASSI_NO,*", "", "COURSEN_MAPPING_NO=" + COURSEN_MAPPING_NO, "COURSEN_MAPPING_NO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                ddlCollege.Enabled = false;
                ddlCollege_SelectedIndexChanged(new object(), new EventArgs());
                ddlScheme.SelectedValue = ds.Tables[0].Rows[0]["SCHEMENO"].ToString();
                ddlScheme.Enabled = false;
                ddlSubjectAL.Attributes.Add("disabled", "");
                ddlSubjectAL.SelectedValue = ds.Tables[0].Rows[0]["COURSENO"].ToString();
                ddlCoreElective.SelectedValue = ds.Tables[0].Rows[0]["CORE_ELECTIVE"].ToString();
                ddlSemister.SelectedValue = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                txtSemHours.Text = ds.Tables[0].Rows[0]["TOTSEMESTERHOURS"].ToString();
                string status = ds.Tables[0].Rows[0]["IS_ACTIVE"].ToString().Trim();
                if (status == "True" || status == "1")
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Src", "SetActive(true);", true);
                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Src", "SetActive(false);", true);
                }
                ddlSubjectClassification.SelectedValue = ds.Tables[0].Rows[0]["SUBCLASSI_NO"].ToString();
                BindListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseCreation.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string SP_Name2 = "PR_GET_SUBJECT_CURRICULUM_MAPPING_LIST_EXCEL";
            string SP_Parameters2 = "@P_COLLEGE_ID,@P_SCHEMENO";
            string Call_Values2 = "" + Convert.ToInt32(ddlCollege.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlScheme.SelectedValue) + "";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string attachment = "attachment; filename=" + "SubjectCurriculumList.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/" + "ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                DataGrid dg = new DataGrid();

                if (ds.Tables.Count > 0)
                {
                    dg.DataSource = ds.Tables[0];
                    dg.DataBind();
                }
                dg.HeaderStyle.Font.Bold = true;
                dg.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this, "Record Not Exists..", this.Page);
                return;
            }
        }
        catch { }
    }

    protected void btnPrReport_Click(object sender, EventArgs e)
    {
        try
        {
            this.ShowReportPDF("Prospectus_Report.pdf", "rptAcademicRegistryProspectusReport.rpt");
        }
        catch (Exception ex)
        {

        }
    }

    private void ShowReportPDF(string reportTitle, string rptFileName)
    {
        try
        {
            string college_id = ddlCollege.SelectedValue;
            string scheme_no = ddlScheme.SelectedValue;

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGEID=" + college_id + ",@P_SCHEMENO=" + scheme_no;
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'> try{";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description); } </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updRule, this.updRule.GetType(), "controlJSScript", sb.ToString(), true);
        }

        catch (Exception ex)
        {
            throw;
        }
    }
}