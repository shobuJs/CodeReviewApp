//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : STUDENT LEDGER REPORT
// CREATION DATE : 24-JUN-2009
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

public partial class Academic_FeeReceiptLedgerReport : System.Web.UI.Page
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
                    this.CheckPageAuthorization();

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
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeReceiptLedgerReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page and maintain user activity
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FeeReceiptLedgerReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeeReceiptLedgerReport.aspx");
        }
    }


    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        StudentController studCont = new StudentController();
        string searchText = txtSearchText.Text.Trim();
        if (searchText != string.Empty)
        {
            string searchBy = (rdoEnrollmentNo.Checked ? "enrollmentno" : (rdoStudentName.Checked ? "name" : "idno"));
            DataSet ds = studCont.RetrieveStudentDetails(searchText, searchBy); ;
            if (ds != null && ds.Tables.Count > 0)
            {
                lvStudentRecords.DataSource = ds.Tables[0];
                lvStudentRecords.DataBind();
            }
        }
        else
            ShowMessage("Please enter text to search.");

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string reportTitle = "Fees_Ledger_Report";
            string rptFileName = "FeeReceiptLedgerReport.rpt";

            FeeReceiptLedgerRpt ledgerReport = GetReportCriteria();
            this.ShowReport(ledgerReport, reportTitle, rptFileName);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeReceiptLedgerReport.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnShowReport(object sender, EventArgs e)
    {
        ImageButton btnShowRpt = sender as ImageButton;
    }

    private FeeReceiptLedgerRpt GetReportCriteria()
    {
        FeeReceiptLedgerRpt ledgerReport = new FeeReceiptLedgerRpt();
        try
        {
            ledgerReport.FilterBychallan = rdoFilterByChallan.Checked;
            ledgerReport.FilterByReceipt = rdoFilterByReceipt.Checked;
            ledgerReport.ReceiptTypes = ddlReceiptType.SelectedValue;
            ledgerReport.DegreeNo = (ddlDegree.SelectedIndex > 0) ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
            ledgerReport.BranchNo = (ddlBranch.SelectedIndex > 0) ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
            ledgerReport.YearNo = (ddlYear.SelectedIndex > 0) ? Convert.ToInt32(ddlYear.SelectedValue) : 0;
            ledgerReport.SemesterNo = (ddlSemester.SelectedIndex > 0) ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeReceiptLedgerReport.GetReportCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return ledgerReport;
    }

    private void ShowReport(FeeReceiptLedgerRpt ledgerRpt, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle; //Daily Fee Collection Report";
            url += "&path=~,Reports,Academic," + rptFileName; //DailyFeeCollection.rpt";
            url += "&param=" + this.GetReportParameters(ledgerRpt);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeReceiptLedgerReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetReportParameters(FeeReceiptLedgerRpt ledgerRpt)
    {
        StringBuilder param = new StringBuilder();
        try
        {
            param.Append("CollegeName=" + Session["coll_name"].ToString() + ",UserName=" + Session["userfullname"].ToString());
            param.Append(",@P_RECIEPTCODE=" + ledgerRpt.ReceiptTypes + ",@P_DEGREENO=" + ledgerRpt.DegreeNo.ToString());
            param.Append(",@P_BRANCHNO=" + ledgerRpt.BranchNo.ToString() + ",@P_YEARNO=" + ledgerRpt.YearNo.ToString());
            param.Append(",@P_SEMESTERNO=" + ledgerRpt.SemesterNo.ToString() + ",@P_FROM_DT=" + ledgerRpt.FromDate.ToShortDateString());
            param.Append(",@P_TO_DATE=" + ledgerRpt.ToDate.ToShortDateString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeReceiptLedgerReport.GetReportParameters() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return param.ToString();
    }

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}