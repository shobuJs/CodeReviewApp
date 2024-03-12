//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : INVIGILATOR DUTY ENTRY 
// CREATION DATE : 21-MAR-2012
// CREATED BY    : PRIYANKA KABADE
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

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

public partial class ACADEMIC_InvigilationDutyEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objEC = new ExamController();
    SeatingController objSc = new SeatingController();

    #region Page Events
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
            }
            PopulateDropDownList();
            btnReport.Visible = true;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=assignFacultyAdvisor.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=assignFacultyAdvisor.aspx");
        }
    }

    #endregion

    #region Other Events

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ddlDay_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDay.SelectedValue != "0")
        {
            string sessionno = ddlSession.SelectedValue;
            string day_no = ddlDay.SelectedValue;
            string slot = ddlSlot.SelectedValue;
            lblExamDate.Text = objCommon.LookUp("ACD_EXAM_DATE", "DISTINCT CONVERT(NVARCHAR,EXAMDATE,103)", "DAYNO = " + ddlDay.SelectedValue + " AND SESSIONNO = " + ddlSession.SelectedValue);
            objCommon.FillDropDownList(ddlSlot, "ACD_EXAM_DATE EX INNER JOIN ACD_EXAM_TT_SLOT S ON (S.SLOTNO = EX.SLOTNO)", "DISTINCT EX.SLOTNO", "S.SLOTNAME", " EX.SESSIONNO = " + ddlSession.SelectedValue + " AND EX.DAYNO = " + ddlDay.SelectedValue, "EX.SLOTNO");
        }
        else
        {
            lblExamDate.Text = null;
            ddlSlot.Items.Clear();
            ddlSlot.Items.Add(new ListItem("Please Select", "0"));
            ddlSlot.Focus();
        }
    }

    protected void ddlSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    #endregion

    #region Click Events

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            CustomStatus cs = (CustomStatus)objSc.InvigilatorDuty(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlExTTType.SelectedValue), Convert.ToInt32(ddlDay.SelectedValue), Convert.ToDateTime(lblExamDate.Text), Convert.ToInt32(ddlSlot.SelectedValue), Convert.ToInt32(txtExtraInv.Text), Session["colcode"].ToString());
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updInvigDuty, "Invigilation Duty Done ...!!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updInvigDuty, "Error while Invigilation Duty..", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_InvigilationDutyEntry.btnGenerate_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedValue != "0" || ddlExTTType.SelectedValue != "0")
            ShowReportinFormate(rdoReportType.SelectedValue, "rptInvigilation.rpt");
        else
            objCommon.DisplayMessage(this.updInvigDuty, "Please select Session and Exam Name..!!", this.Page);
    }

    private void ShowReportinFormate(string exporttype, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlExTTType.SelectedItem.Text + "_" + ddlSession.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_EXAM_NO=" + ddlExTTType.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updInvigDuty, this.updInvigDuty.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnReport2_Click(object sender, EventArgs e)
    {
    }
    #endregion

    #region User Methods

    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0  ", "SESSIONNO DESC");
    }

    private void ShowReportDayWise(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_EXAM_NO=" + ddlExTTType.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();


            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updInvigDuty, this.updInvigDuty.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_InvigilationDuty.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ClearControls()
    {
        ddlSession.SelectedIndex = 0;
        ddlExTTType.SelectedIndex = 0;
        ddlDay.SelectedIndex = 0;
        ddlSlot.SelectedIndex = 0;
        lblExamDate.Text = null;
        txtExtraInv.Text = string.Empty;
    }
    #endregion

    protected void ddlExTTType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlExTTType.SelectedValue != "0")
            objCommon.FillDropDownList(ddlDay, "ACD_EXAM_DATE", "DISTINCT DAYNO AS DAY", "DAYNO", "SESSIONNO =" + ddlSession.SelectedValue + " AND EXAM_TT_TYPE=" + ddlExTTType.SelectedValue, "DAYNO");
        else
            ClearControls();
    }
}

