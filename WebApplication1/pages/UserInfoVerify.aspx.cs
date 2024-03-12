//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Applicant data verify
// CREATION DATE : 19-MAY-2014
// CREATED BY    : RENUKA ADULKAR
// MODIFIED BY   : 
// MODIFIED DATE : 
// MODIFIED DESC : 
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;

public partial class Academic_UserInfoVerify : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    DailyFeeCollectionController objdfcc = new DailyFeeCollectionController();

    #region page

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
        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
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


                objCommon.FillDropDownList(ddlSession, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                ddlSession.Items.RemoveAt(0);

                objCommon.FillDropDownList(ddlAdmcat, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO>0 AND QEXAMSTATUS='E'", string.Empty);


            }

        }
        divMsg.InnerHtml = string.Empty;


    }

    #endregion

    #region User Defined Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=UserInfoVerify.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=UserInfoVerify.aspx");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {

        btnSubmit_Click(sender, e);
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;



            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_QUALIFYNO=" + ddlAdmcat.SelectedValue + ",@P_APPID=" + txtAppID.Text.Trim();


            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updMarks, this.updMarks.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentReconsilReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    #endregion

    #region button

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            DataSet dsfee = objdfcc.GetUserFeesPaidData(Convert.ToInt32(ddlSession.SelectedValue.ToString()), Convert.ToInt32(ddlAdmcat.SelectedValue.ToString()), txtAppID.Text.Trim());
            if (dsfee.Tables[0] != null && dsfee.Tables[0].Rows.Count > 0)
            {
                btnVerify.Enabled = true;
                divDetails.Visible = true;
                lvApplicantdata.Visible = true;

                lvApplicantdata.DataSource = dsfee.Tables[0];
                lvApplicantdata.DataBind();

                foreach (ListViewDataItem lvItem in lvApplicantdata.Items)
                {
                    CheckBox chkBox = lvItem.FindControl("chkVerify") as CheckBox;
                    TextBox txtJEETotal = (TextBox)lvItem.FindControl("txtJEETotal");

                    if (chkBox.ToolTip == "1")
                    {
                        chkBox.Enabled = false;
                        chkBox.Checked = true;
                        txtJEETotal.Enabled = false;
                    }
                }
            }
            else
            {
                btnVerify.Enabled = false;
                divDetails.Visible = false;
                lvApplicantdata.Visible = false;
                lvApplicantdata.DataSource = null;
                lvApplicantdata.DataBind();
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentBranchPrefCounter3.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnStatus_Click(object sender, EventArgs e)
    {

    }

    protected void btnexport_Click(object sender, EventArgs e)
    {
        this.Export();
    }

    private void Export()
    {
        string attachment = "attachment; filename=" + "Verification.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        int sessionNo = 0;
        sessionNo = Convert.ToInt32(ddlSession.SelectedValue);

        DataSet dsfee = objdfcc.GetUserJEEVerifed(Convert.ToInt32(ddlSession.SelectedValue.ToString()), Convert.ToInt32(ddlAdmcat.SelectedValue.ToString()), txtAppID.Text.Trim());
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

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedValue == string.Empty)
        {
            objCommon.DisplayMessage("Please Select Admission Batch", this.Page);
            return;
        }
        else if (ddlAdmcat.SelectedValue == string.Empty)
        {
            objCommon.DisplayMessage("Please Select Admission Category ", this.Page);
            return;
        }
        else
        {
            ShowReport("Verification List", "rptUserMarksVerificationStudList.rpt");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        try
        {

            string studentIds = string.Empty;
            string jeetotal = string.Empty;
            int appid = 0;

            int countone = 0; int counttwo = 0;

            foreach (ListViewDataItem lvItem in lvApplicantdata.Items)
            {
                CheckBox chkBox = lvItem.FindControl("chkVerify") as CheckBox;
                HiddenField chkhdn = lvItem.FindControl("hiduserno") as HiddenField;
                TextBox txttotal = lvItem.FindControl("txtJEETotal") as TextBox;

                if (chkBox.Checked == true && chkBox.Enabled == true)
                {
                    studentIds = chkhdn.Value;
                    jeetotal = txttotal.Text;
                    appid = 1;
                }
                countone++;
                if (chkBox.Enabled == false && chkBox.Enabled == false)
                {
                    counttwo++;
                }
            }
            if (string.IsNullOrEmpty(studentIds) && (counttwo != countone))
            {
                objCommon.DisplayMessage("Please Select Students!", this.Page);
            }
            else if (counttwo == countone)
            {
                if (txtAppID.Text == string.Empty)
                    objCommon.DisplayMessage("Marks are Already Verified For Selected Admission Batch " + ddlSession.SelectedItem.Text + " of Admission Category " + ddlAdmcat.SelectedItem.Text + "!", this.Page);
                else
                    objCommon.DisplayMessage("Marks are Already Verified For Selected Admission Batch " + ddlSession.SelectedItem.Text + " of Admission Category " + ddlAdmcat.SelectedItem.Text + " of Applicantion ID " + txtAppID.Text.Trim() + "!", this.Page);
            }
            else
            {
                CustomStatus cs = new CustomStatus();

                foreach (ListViewDataItem lvItem in lvApplicantdata.Items)
                {
                    CheckBox chkBox = lvItem.FindControl("chkVerify") as CheckBox;
                    HiddenField chkhdn = lvItem.FindControl("hiduserno") as HiddenField;
                    TextBox txttotal = lvItem.FindControl("txtJEETotal") as TextBox;

                    if (chkBox.Checked == true && chkBox.Enabled == true)
                    {
                        studentIds = chkhdn.Value;
                        jeetotal = txttotal.Text;
                        appid = 1;
                    }

                    cs = (CustomStatus)objdfcc.GetUserjeeinsertData(Convert.ToInt32(ddlSession.SelectedValue), studentIds, Convert.ToInt32(ddlAdmcat.SelectedValue), Convert.ToInt32(Session["userno"]), jeetotal);
                }
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage("Marks Verified Successfully.", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage("Error To Verified Marks.", this.Page);
                    return;
                }
                btnSubmit_Click(sender, e);
            }
        }
        catch (Exception ex)
        {
        }
    }

    #endregion

}