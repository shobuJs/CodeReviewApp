using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Web.Services;

public partial class ACADEMIC_RuleEngineDefiniton : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objCourse = new CourseController();
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
            }


            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
            ViewState["action"] = "add";
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DegreeMapping.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DegreeMapping.aspx");
        }
    }
    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        CustomStatus cs = new CustomStatus();
        int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
        int branchno = Convert.ToInt32(ddlbranch.SelectedValue);
        int schemeno = Convert.ToInt32(ddlScheme.SelectedValue);
        int count = 0;

        foreach (ListViewDataItem dataitem in lvSubjectType.Items)
        {


            TextBox txtFinalCie = dataitem.FindControl("txtFinalCie") as TextBox;
            TextBox txtFInalEse = dataitem.FindControl("txtFInalEse") as TextBox;
            TextBox txtFinalCiePer = dataitem.FindControl("txtFinalCiePer") as TextBox;
            TextBox txtFinalEsePer = dataitem.FindControl("txtFinalEsePer") as TextBox;
            TextBox txtFinalPassingCriteria = dataitem.FindControl("txtFinalPassingCriteria") as TextBox;

            int subtype = Convert.ToInt32((dataitem.FindControl("lblsubtype") as Label).ToolTip);
            cs = (CustomStatus)objCourse.InsertResultRulesDefinition(schemeno, subtype, txtFinalCie.Text, txtFInalEse.Text, txtFinalCiePer.Text, txtFinalEsePer.Text, txtFinalPassingCriteria.Text);

        }

        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this.updRuleEngine, "Examination Rules addred sucessfully", this.Page);

            Clear();

        }



    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        ddlScheme.SelectedIndex = 0;
        ddlbranch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        BindListView();
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlbranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
                lvSubjectType.DataSource = null;
                lvSubjectType.DataBind();
            }
            else
            {
                ddlbranch.Items.Clear();
                ddlDegree.SelectedIndex = 0;

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_SVETstudentAdmissionstatus.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlbranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlbranch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue, "SCHEMENO DESC");
                ddlScheme.Focus();
                lvSubjectType.DataSource = null;
                lvSubjectType.DataBind();

            }
            else
            {
                ddlScheme.Items.Clear();
                ddlbranch.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_SVETstudentAdmissionstatus.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }
    protected void Clear()
    {
        ddlDegree.SelectedIndex = 0;
        ddlbranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        lvSubjectType.DataSource = null;
        lvSubjectType.DataBind();

    }
    private void BindListView()
    {
        try
        {

            DataSet ds = null;
            ds = objCourse.GetRuleEngineForScheme(Convert.ToInt32(ddlScheme.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSubjectType.DataSource = ds;
                lvSubjectType.DataBind();
            }
            else
            {
                ds = objCommon.FillDropDown("ACD_SUBJECTTYPE", "SUBID", "SUBNAME,'' [CIE_SCALE],''[ESE_SCALE],''[CIE_PASSINGCRITERIA],''[ESE_PASSINGCRITERIA],''[TOT_PASSINGCRITERIA]", "SUBID >0", "SUBID");
                lvSubjectType.DataSource = ds;
                lvSubjectType.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_degree.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void btnreport_Click(object sender, EventArgs e)
    {
        ShowReport("MarkEntryAllocationReport", "rptRuleEngineDefinition.rpt");
    }




    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + ddlScheme.SelectedValue.ToString();// +",@P_SUBID=" + ddlSubjectType.SelectedValue;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updRuleEngine, this.updRuleEngine.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RandomStatusReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedIndex > 0)
        {
            try
            {
                BindListView();

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Academic_Masters_degree.BindListView-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    [WebMethod]
    public static void Savedata()
    {
        if (HttpContext.Current != null)
        {
            Page page = (Page)HttpContext.Current.Handler;
            TextBox txtFinalCie = (TextBox)page.FindControl("txtFinalCie") as TextBox;
            TextBox txtFInalEse = (TextBox)page.FindControl("txtFInalEse") as TextBox;
            TextBox txtFinalCiePer = (TextBox)page.FindControl("txtFinalCiePer") as TextBox;
            TextBox txtFinalEsePer = (TextBox)page.FindControl("txtFinalEsePer") as TextBox;
            TextBox txtFinalPassingCriteria = (TextBox)page.FindControl("txtFinalPassingCriteria") as TextBox;


        }
    }
}