//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : ONLINE ADMISSION PORTAL REPORT
// CREATION DATE :                                                        
// CREATED BY    :                           
// MODIFIED DATE : 29/01/2016
// modified by   : MANISH ANIL CHAWADE
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;


public partial class ACADEMIC_OnlineAdmissionReportPortal : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FetchDataController ObjFetch = new FetchDataController();
    StudentController objSC = new StudentController();

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
                    //Page Authorization
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                }
                //Fill DropDown List
                PopulateDropDownList();
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_StudentStrength.Page_Load-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=StudentLocalAddressLabel.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentLocalAddressLabel.aspx");
        }
    }

    //Fill DropdownList
    protected void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN Admission Batch
            objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
            // FILL DROPDOWN Degree
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_OnlineAdmissionReportPortal.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FILL DROPDOWN BRANCH
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
        ddlBranch.Focus();
    }
    protected void btnApplystudList_Click(object sender, EventArgs e)
    {
        ShowReport("StudentsApplicationReceivedList", "ApplicationReceivedStudentList.rpt", 1);
    }
    protected void btnRecStudList_Click(object sender, EventArgs e)
    {
        ShowReport("StudentsApplicationReceivedList", "ApplicationReceivedStudentList.rpt", 2);
    }
    protected void btnPendingStudList_Click(object sender, EventArgs e)
    {
        ShowReport("StudentsApplicationReceivedList", "ApplicationReceivedStudentList.rpt", 3);
    }

    private void ShowReport(string reportTitle, string rptFileName, int app_status)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_APPLICATION_STATUS=" + app_status + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmbatch.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ReceiveApplicationStatus.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        this.Export();
    }

    private void Export()
    {
        string attachment = "attachment; filename=" + "RegistredUserSheet.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        DataSet ds = ObjFetch.GetUserData(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
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

    protected void btnRegCountinExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmbatch.SelectedIndex > 0)
            {
                ShowReportinxcel("xls", "RegistrationStatisticsCount.rpt");
            }
            else
            {
                objCommon.DisplayMessage("Please Select Admission Batch.", this.Page);
                ddlAdmbatch.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentsStaticalReport.btnRegCountinExcel_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReportinxcel(string exporttype, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlAdmbatch.SelectedItem.Text;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_ADMBATCH=" + ddlAdmbatch.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlUser, this.updpnlUser.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_OnlineAdmissionReportPortal.ShowReportinxcel() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnApplirecvCount_Click(object sender, EventArgs e)
    {
        ShowStatasticReport("ApplicationsReceivedCoursewiseList", "rptReceivedApplicationStatastics.rpt");
    }

    protected void btnDeptStudList_Click(object sender, EventArgs e)
    {
        ShowStatasticReport("DepartmentwiseApplicationList", "rptDepartmentwiseApplicationlist.rpt");
    }

    private void ShowStatasticReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmbatch.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ReceiveApplicationStatus.ShowStatasticReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }







    //Generating Registration Statistics Report 
    protected void btnRegCount_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmbatch.SelectedIndex > 0)
            {
                ShowReportRegStatistcs("Registration_Statistics_Report", "RegistrationStatisticsCount.rpt");
            }
            else
            {
                objCommon.DisplayMessage("Please Select Admission Batch.", this.Page);
                ddlAdmbatch.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentsStaticalReport.btnRegCount_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReportRegStatistcs(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_ADMBATCH=" + ddlAdmbatch.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.ShowReportRegStatistcs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportFeeStatistcs(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_ADMBATCH=" + ddlAdmbatch.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.ShowReportRegStatistcs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnRegCountDatewise_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmbatch.SelectedIndex > 0)
            {
                this.ExportforRegCountDatewise();
            }
            else
            {
                objCommon.DisplayMessage("Please Select Admission Batch.", this.Page);
                ddlAdmbatch.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentsStaticalReport.btnRegCountDatewise_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //This Method is for Registration Statistics Datewise Report in excel.
    private void ExportforRegCountDatewise()
    {
        string attachment = "attachment; filename=" + "RegistrationStatistics.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        int sessionNo = 0;
        sessionNo = Convert.ToInt32(ddlAdmbatch.SelectedValue);

        DataSet dsfee = objSC.GetStudenRegList_DateWise(Convert.ToInt32(ddlAdmbatch.SelectedValue));
        DataGrid dg = new DataGrid();
        if (dsfee.Tables.Count > 0)
        {
            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }



    protected void btnFeeCount_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmbatch.SelectedIndex > 0)
            {
                ShowReportRegStatistcs("Registration_Fees_Statistics_Report", "RegistrationFeeStatisticsCount.rpt");
            }
            else
            {
                objCommon.DisplayMessage("Please Select Admission Batch.", this.Page);
                ddlAdmbatch.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentsStaticalReport.btnFeeCount_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlAdmbatch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
    }
}