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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Academic_StudentLedgerReport : System.Web.UI.Page
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
                }
            }
            divMsg.InnerHtml = "";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_StudentLedgerReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentLedgerReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentLedgerReport.aspx");
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

    protected void btnShowReport_Click(object sender, EventArgs e)
    {        
        try
        {
            ImageButton btnShowRpt = sender as ImageButton;
            int studentId = int.Parse(btnShowRpt.CommandArgument);

            string reportTitle = "Student_Ledger_Report";
            string rptFileName = "StudentLedgerReport.rpt";

            this.ShowReport(studentId.ToString(), reportTitle, rptFileName);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_StudentLedgerReport.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReport(string studentId, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle; 
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_IDNO=" + studentId + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",UserName=" + Session["userfullname"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_StudentLedgerReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
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