//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : OUTSTANDING FEE REPORT
// CREATION DATE : 23-JUN-2009
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Academic_OutstandingFeesReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

    #region Page Events

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
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
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "CODE", "CODE <> ''", "CODE");
                    this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "SHORTNAME", "SHORTNAME <> ''", "SHORTNAME");
                    this.objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "", "");
                    this.objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "SEMESTERNO");
                    this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "", "");
                }
            }
            else
                divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_OutstandingFeesReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
        }
    }

    #endregion

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string reportTitle = string.Empty;
            string rptFileName = string.Empty;
            bool showBalanceRpt = false;

            if (rdoShowStudentsWithBalance.Checked)
                showBalanceRpt = true;

            if (rdoDetailedReport.Checked)
            {
                if (ddlReceiptType.SelectedIndex < 1)
                {
                    ShowMessage("Please select a receipt type for detailed report.");
                    return;
                }
                reportTitle = "Outstanding_Fee_Report";
                rptFileName = "OutstandingFeesReport_Detailed.rpt";
            }
            else
            {
                reportTitle = "Outstanding_Fee_Report";
                rptFileName = "OutstandingFeesReport_Summary.rpt";
            }

            DailyFeeCollectionRpt outstanFeeReport = GetReportCriteria();
            DailyFeeCollectionController dfcController = new DailyFeeCollectionController();
            DataSet ds = dfcController.GetOutstandingFeeReportData(outstanFeeReport, showBalanceRpt);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                this.ShowReport(outstanFeeReport, showBalanceRpt, reportTitle, rptFileName);
            }
            else
            {
                this.ShowMessage("No information found based on given criteria.");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_OutstandingFeesReport.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DailyFeeCollectionRpt GetReportCriteria()
    {
        DailyFeeCollectionRpt dcrReport = new DailyFeeCollectionRpt();
        try
        {
            dcrReport.ReceiptTypes = (ddlReceiptType.SelectedIndex > 0) ? ddlReceiptType.SelectedValue : "";
            dcrReport.DegreeNo = (ddlDegree.SelectedIndex > 0) ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
            dcrReport.BranchNo = (ddlBranch.SelectedIndex > 0) ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
            dcrReport.YearNo = (ddlYear.SelectedIndex > 0) ? Convert.ToInt32(ddlYear.SelectedValue) : 0;
            dcrReport.SemesterNo = (ddlSemester.SelectedIndex > 0) ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_OutstandingFeesReport.GetReportCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return dcrReport;
    }

    private void ShowReport(DailyFeeCollectionRpt dcrRpt, bool showBalanceRpt, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=" + this.GetReportParameters(dcrRpt) + ",@P_SHOWBALANCE=" + showBalanceRpt.ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'> try{";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description); } </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_OutstandingFeesReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetReportParameters(DailyFeeCollectionRpt dcrRpt)
    {
        StringBuilder param = new StringBuilder();
        try
        {
            param.Append("@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",UserName=" + Session["userfullname"].ToString());
            param.Append(",@P_RECIEPT_CODE=" + dcrRpt.ReceiptTypes + ",@P_DEGREENO=" + dcrRpt.DegreeNo.ToString());
            param.Append(",@P_BRANCHNO=" + dcrRpt.BranchNo.ToString() + ",@P_YEARNO=" + dcrRpt.YearNo.ToString());
            param.Append(",@P_SEMESTERNO=" + dcrRpt.SemesterNo.ToString());
            param.Append(",Degree=" + ((ddlDegree.SelectedIndex > 0) ? ddlDegree.SelectedValue : "0"));
            param.Append(",Branch=" + ((ddlBranch.SelectedIndex > 0) ? ddlBranch.SelectedValue : "0"));
            param.Append(",Year=" + ((ddlYear.SelectedIndex > 0) ? ddlDegree.SelectedValue : "0"));
            param.Append(",Semester=" + ((ddlSemester.SelectedIndex > 0) ? ddlSemester.SelectedValue : "0"));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_OutstandingFeesReport.GetReportParameters() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return param.ToString();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }
}