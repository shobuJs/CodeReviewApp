using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;

using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class ACADEMIC_ScholarshipReports : System.Web.UI.Page
{
    Common objCommon = new Common();
    StudentController objSC = new StudentController();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

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
            if (Session["userno"] == null || Session["username"] == null ||
                  Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null || Session["idno"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    CheckPageAuthorization();
                    if (Convert.ToInt32(Session["usertype"]) == 2)
                    {
                        Response.Redirect("~/notauthorized.aspx");
                    }
                    else
                    {
                        BindListView();
                    }
                }
                objCommon.SetLabelData("0");
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // dynamic page header addded by sarang 09/07/2021
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {

            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx");
            }
            Common objCommon = new Common();

        }
        else
        {

            Response.Redirect("~/notauthorized.aspx");
        }
    }
    protected void BindListView()
    {
        try
        {

            Common objCommon = new Common();

            string SP_Name = "PKG_ACD_BIND_SCHOLARSHIP_DROP_DOWN";
            string SP_Parameters = "@P_DYNAMIC_FILTER,@P_SESSIONNO,@P_SEMESTERNO,@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO";
            string Call_Values = "" + Convert.ToInt32(1) + ",0" + ",0" + ",0" + ",0" + ",0";

            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            ddlSession.Items.Clear();
            ddlSession.Items.Add("Please Select");
            ddlSession.SelectedItem.Value = "0";

            ddlCollege.Items.Clear();
            ddlCollege.Items.Add("Please Select");
            ddlCollege.SelectedItem.Value = "0";

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlSession.DataSource = ds.Tables[0];
                ddlSession.DataValueField = "SESSIONNO";
                ddlSession.DataTextField = "SESSION_NAME";
                ddlSession.DataBind();
            }
            if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
            {
                ddlCollege.DataSource = ds.Tables[2];
                ddlCollege.DataValueField = "COLLEGE_ID";
                ddlCollege.DataTextField = "COLLEGE_NAME";
                ddlCollege.DataBind();
            }

            ViewState["SEMESTER"] = null;
            ViewState["DEGREE"] = null;

            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                ViewState["SEMESTER"] = ds.Tables[1];
            }
            if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
            {
                ViewState["DEGREE"] = ds.Tables[3];
            }
            if (ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
            {
                ddlScholarshipType.DataSource = ds.Tables[4];
                ddlScholarshipType.DataValueField = "SCHOLARSHIP_NO";
                ddlScholarshipType.DataTextField = "SCHOLARSHIP";
                ddlScholarshipType.DataBind();
            }
            
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            ddlSemester.Items.Clear();
            ddlSemester.Items.Add("Please Select");
            ddlSemester.SelectedItem.Value = "0";

            DataTable dt = ViewState["SEMESTER"] as DataTable;
            DataView dv = new DataView();

            if (dt != null)
            {
                dv = dt.DefaultView;
                dv.RowFilter = "SESSIONNO IN(" + ddlSession.SelectedValue + ")";

                ddlSemester.DataSource = dv;
                ddlSemester.DataValueField = "SEMESTERNO";
                ddlSemester.DataTextField = "SEMESTERNAME";
                ddlSemester.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlProgram.Items.Clear();
            ddlProgram.Items.Add("Please Select");
            ddlProgram.SelectedItem.Value = "0";

            DataTable dt = ViewState["DEGREE"] as DataTable;
            DataView dv = new DataView();

            if (dt != null)
            {
                dv = dt.DefaultView;
                dv.RowFilter = "COLLEGE_ID IN(" + ddlCollege.SelectedValue + ")";

                ddlProgram.DataSource = dv;
                ddlProgram.DataValueField = "DEGREENO";
                ddlProgram.DataTextField = "DEGREENAME";
                ddlProgram.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlScholarshipType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.objCommon.FillDropDownList(ddlConcessionType, "ACD_CONCESSION_TYPE", "CONCESSION_TYPENO", "CONCESSION_TYPE", "SCHOLARSHIP_TYPE ="+Convert.ToInt32(ddlScholarshipType.SelectedValue)+"", "CONCESSION_TYPENO");

            if (ddlScholarshipType.SelectedValue == "1")
            {
                ddlReportType.Items.FindByValue("3").Enabled = false;
                ddlReportType.Items.FindByValue("2").Enabled = true;
            }
            else if (ddlScholarshipType.SelectedValue == "2")
            {
                ddlReportType.Items.FindByValue("2").Enabled = false;
                ddlReportType.Items.FindByValue("3").Enabled = true;
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlReportType.SelectedValue == "2")  //For USA Based scholarship
            {
                if (Convert.ToInt32(ddlScholarshipType.SelectedValue) != 1)
                {
                    objCommon.DisplayMessage(this, "Please select USA Based Scholarship Type for this Report !!!", this.Page);
                    return;
                }
                else
                {
                    ShowReport(ddlReportType.SelectedItem.Text, "rptScholarshipListUSABased.rpt", 1);
                }
              
            }

            else if (ddlReportType.SelectedValue == "3") // For externally funded
            {
                if (Convert.ToInt32(ddlScholarshipType.SelectedValue) != 2)
                {
                    objCommon.DisplayMessage(this, "Please select Externally Funded Scholarship Type for this Report !!!", this.Page);
                    return;
                }
                else
                {
                    ShowReport(ddlReportType.SelectedItem.Text, "rptScholarshipListExternallyFunded.rpt", 2);
                }
            }
            else if (ddlReportType.SelectedValue == "1") // For externally funded
            {
                ShowReport(ddlReportType.SelectedItem.Text, "rptConsolidatedListOfScholars.rpt", Convert.ToInt32(ddlScholarshipType.SelectedValue));
            }
            else
            {
                
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void ShowReport(string reportTitle, string rptFileName,int ScholarshipNo)
    {
       
        try
        {
    
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle="+reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_COLLEGE_ID=" + (Convert.ToInt32(ddlCollege.SelectedValue)) + "," + "@P_SESSIONNO=" + (Convert.ToInt32(ddlSession.SelectedValue)) + "," + "@P_DEGREENO=" + (Convert.ToInt32(ddlProgram.SelectedValue.Split('$')[0])) + "," + "@P_SEMESTERNO=" + (Convert.ToInt32(ddlSemester.SelectedValue)) + "," + "@P_SCHOLARSHIP_NO=" + (Convert.ToInt32(ScholarshipNo)) + "," + "@P_CONCESSION_NO=" + (Convert.ToInt32(ddlConcessionType.SelectedValue));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updScholarshipApprove, this.updScholarshipApprove.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

}