//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : COURSE ALLOTMENT                                      
// CREATION DATE : 08-NOV-2011
// CREATED BY    : ASHISH DHAKATE                                                  
// MODIFIED DATE : 
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_Student_Prospectus : System.Web.UI.Page
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

                //fill sessions
                string sessionno = objCommon.LookUp("REFF", "CUR_ADM_SESSIONNO", string.Empty);
                if (string.IsNullOrEmpty(sessionno))
                {
                    objCommon.DisplayMessage("Please Set the Admission Session from Reference Page!!", this.Page);
                    return;
                }
                else
                {
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO=" + sessionno, "SESSIONNO DESC");
                    objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                    ddlSession.Items.RemoveAt(0);
                    ddlAdmBatch.SelectedIndex = 1;
                    txtSaleDate.Text = DateTime.Now.ToShortDateString();

                }

                this.PopulateDropDown();

                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
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
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Student_Prospectus.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");

            //COADING FOR AUTOGENERATE THE RECEIPT NO
            string STR1 = objCommon.LookUp("ACD_PROSPECTUS", "MAX(PROSNO)+1", "PROSNO IS NOT NULL");
            if (STR1 == "")
            {
                STR1 = "1000";
            }
            txtReciptNo.Text = STR1;
            ddlBranch.Focus();

        }
        else
        {
            ddlBranch.Items.Clear();
            ddlDegree.SelectedIndex = 0;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        StudentController objSC = new StudentController();
        Student objS = new Student();
        try
        {
            objS.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objS.AdmBatch = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            if (!txtStudentName.Text.Trim().Equals(string.Empty)) objS.StudName = txtStudentName.Text.ToUpper().Trim();
            if (!txtSerialNo.Text.Trim().Equals(string.Empty)) objS.SerialNo = txtSerialNo.Text.Trim();
            if (!txtSaleDate.Text.Trim().Equals(string.Empty)) objS.SaleDate = Convert.ToDateTime(txtSaleDate.Text.Trim());
            if (!txtAmount.Text.Trim().Equals(string.Empty)) objS.Amount = txtAmount.Text.Trim();
            objS.Uano = Convert.ToInt32(Session["userno"].ToString());
            objS.IPADDRESS = ViewState["ipAddress"].ToString();
            objS.CollegeCode = Session["colcode"].ToString();
            if (!txtReciptNo.Text.Trim().Equals(string.Empty)) objS.ReciptNo = txtReciptNo.Text.Trim();
            int output = objSC.AddProspectusSaleStudent(objS);

            if (output != -99)
            {
                objS.ProsNo = output;
                ShowReport("ProspectusSlip", "rptProspectusSlip.rpt", output);
            }
            else
            {
                objCommon.DisplayMessage("Existing Receipt number..", this.Page);
            }

            this.clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentInfoEntry.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void clear()
    {
        ddlDegree.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 1;
        ddlBranch.SelectedIndex = 0;
        txtAmount.Text = string.Empty;
        txtStudentName.Text = string.Empty;
        txtSaleDate.Text = DateTime.Now.ToString();
        txtReciptNo.Text = string.Empty;
        txtSerialNo.Text = string.Empty;
    }
    protected void txtStudentName_TextChanged(object sender, EventArgs e)
    {

    }
    private void ShowReport(string reportTitle, string rptFileName, int prosno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_PROSNO=" + prosno;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updProspectus, this.updProspectus.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

}
