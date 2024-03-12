//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADAMIC/REPORT
// PAGE NAME     : COURSEWISESTUDENTREPORT.ASPX                                                   
// CREATION DATE : 27-APRIL-2012                                                        
// CREATED BY    : ABHIJIT L. DESHPANDE                                                       
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

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

public partial class ACADEMIC_REPORTS_CoursewiseStudentCountReport : System.Web.UI.Page
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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                PopulateDropDownList();
                ddlSession.Focus();
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CoursewiseStudentCountReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CoursewiseStudentCountReport.aspx");
        }
    }


    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO >0", "SEMESTERNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_COURSEWISESTUDENTCOUNTREPORT.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }




    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {

            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
            ddlBranch.Focus();
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.Focus();
        }
        else
        {
            ClearControls();
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {

            ddlScheme.Items.Clear();
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMETYPE=1 AND BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO");
            ddlScheme.Focus();
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlScheme.Focus();
        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
        }


    }


    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedIndex > 0)
        {

            ddlSem.Items.Clear();
            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT SR ON (S.SEMESTERNO=SR.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + ddlScheme.SelectedValue, "SEMESTERNO");
            ddlSem.Focus();
        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    private void ClearControls()
    {
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        ddlSession.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport(rdoReportType.SelectedValue, "rptCoursewiseStudentCount.rpt");
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        Response.Redirect(Request.Url.ToString());
    }

    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            if (ddlBranch.SelectedIndex == 0 && ddlScheme.SelectedIndex == 0 && ddlSem.SelectedIndex == 0)
                url += "&filename=" + ddlDegree.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            else if (ddlBranch.SelectedIndex > 0 && ddlScheme.SelectedIndex == 0 && ddlSem.SelectedIndex == 0)
                url += "&filename=" + ddlDegree.SelectedItem.Text + "_" + ddlBranch.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            else if (ddlBranch.SelectedIndex > 0 && ddlScheme.SelectedIndex > 0 && ddlSem.SelectedIndex == 0)
                url += "&filename=" + ddlDegree.SelectedItem.Text + "_" + ddlBranch.SelectedItem.Text + "_" + ddlScheme.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            else if (ddlBranch.SelectedIndex > 0 && ddlScheme.SelectedIndex == 0 && ddlSem.SelectedIndex > 0)
                url += "&filename=" + ddlDegree.SelectedItem.Text + "_" + ddlBranch.SelectedItem.Text + "_" + ddlSem.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            else if (ddlBranch.SelectedIndex == 0 && ddlScheme.SelectedIndex == 0 && ddlSem.SelectedIndex > 0)
                url += "&filename=" + ddlDegree.SelectedItem.Text + "_" + ddlSem.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            else if (ddlBranch.SelectedIndex == 0 && ddlScheme.SelectedIndex > 0 && ddlSem.SelectedIndex == 0)
                url += "&filename=" + ddlDegree.SelectedItem.Text + "_" + ddlScheme.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            else
                url += "&filename=" + ddlDegree.SelectedItem.Text + "_" + ddlBranch.SelectedItem.Text + "_" + ddlScheme.SelectedItem.Text + "_" + ddlSem.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " window.close();";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO IN ( SELECT DISTINCT SC.DEGREENO FROM ACD_SCHEME SC INNER JOIN ACD_STUDENT_RESULT SR ON (SC.SCHEMENO=SR.SCHEMENO) WHERE SESSIONNO=" + ddlSession.SelectedValue + " AND  SCHEMETYPE=1)", "DEGREENO");
        }
        else
        {
            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new ListItem("Please Select", "0"));
        }
    }
}
