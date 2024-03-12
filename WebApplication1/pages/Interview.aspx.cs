using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using mastersofterp;
using Newtonsoft.Json;
using EASendMail;

public partial class Interview : System.Web.UI.Page
{
    Common objCommon = new Common();
    ConfigController ObjConfig = new ConfigController();

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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                PopulateDropDown();
            }
        }
        objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // dynamic page header addded by sarang 09/07/2021
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Interview.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Interview.aspx");
        }
    }
    protected void PopulateDropDown()
    {
        try
        {
            DataSet ds = null;
            ds = ObjConfig.GetInterViewDetails(Convert.ToInt32(0), Convert.ToString("0"), Convert.ToString("0"), Convert.ToString("0"), Convert.ToString("0"), Convert.ToString("0"));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlIntake.DataSource = ds.Tables[0];
                ddlIntake.DataValueField = "BATCHNO";
                ddlIntake.DataTextField = "BATCHNAME";
                ddlIntake.DataBind();

                ddlResultIntake.DataSource = ds.Tables[0];
                ddlResultIntake.DataValueField = "BATCHNO";
                ddlResultIntake.DataTextField = "BATCHNAME";
                ddlResultIntake.DataBind();
            }
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void ddlIntake_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlIntake.SelectedIndex > 0)
            {
                ddlStudyLevel.Items.Clear();

                DataSet ds = null;
                ds = ObjConfig.GetInterViewDetails(Convert.ToInt32(ddlIntake.SelectedValue), Convert.ToString("0"), Convert.ToString("0"), Convert.ToString("0"), Convert.ToString("0"), Convert.ToString("0"));
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    ddlStudyLevel.DataSource = ds.Tables[1];
                    ddlStudyLevel.DataValueField = "UA_SECTION";
                    ddlStudyLevel.DataTextField = "UA_SECTIONNAME";
                    ddlStudyLevel.DataBind();
                }
                if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                {
                    Panel2.Visible = true;
                    lvInterviewSchedule.DataSource = ds.Tables[3];
                    lvInterviewSchedule.DataBind();
                }
                else
                {
                    Panel2.Visible = false;
                    //objCommon.DisplayMessage(updInterViewSchedule, "No Record Found !!!", this.Page);
                    lvInterviewSchedule.DataSource = null;
                    lvInterviewSchedule.DataBind();
                }
            }
            else
            {
                ddlStudyLevel.Items.Clear();
                ddlProgram.Items.Clear();
                lvInterviewSchedule.DataSource = null;
                lvInterviewSchedule.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlStudyLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string StudyLevel = "";
            foreach (ListItem items in ddlStudyLevel.Items)
            {
                if (items.Selected == true)
                {
                    StudyLevel += items.Value + ',';
                }
            }
            if (StudyLevel == string.Empty)
            {
                ddlProgram.Items.Clear();
                return;
            }
            else
            {
                StudyLevel = StudyLevel.Substring(0, StudyLevel.Length - 1);
            }

            if (ddlIntake.SelectedIndex > 0)
            {
                ddlProgram.Items.Clear();

                DataSet ds = null;
                ds = ObjConfig.GetInterViewDetails(Convert.ToInt32(ddlIntake.SelectedValue), Convert.ToString(StudyLevel), Convert.ToString("0"), Convert.ToString("0"), Convert.ToString("0"), Convert.ToString("0"));
                if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    ddlProgram.DataSource = ds.Tables[2];
                    ddlProgram.DataValueField = "DEGREENO";
                    ddlProgram.DataTextField = "DEGREENAME";
                    ddlProgram.DataBind();
                }
            }
            else
            {
                ddlProgram.Items.Clear();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlIntake.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updInterViewSchedule, "Please Select Intake !!!", this.Page);
                lvInterviewSchedule.DataSource = null;
                lvInterviewSchedule.DataBind();
                return;
            }
            string StudyLevel = "",ProgramNos="";
            foreach (ListItem items in ddlStudyLevel.Items)
            {
                if (items.Selected == true)
                {
                    StudyLevel += items.Value + ',';
                }
            }
            if (StudyLevel == string.Empty)
            {
                StudyLevel = "0";
            }
            else
            {
                StudyLevel = StudyLevel.Substring(0, StudyLevel.Length - 1);
            }
            //foreach (ListItem items in ddlProgram.Items)
            //{
            //    if (items.Selected == true)
            //    {
            //        ProgramNos += items.Value + ',';
            //    }
            //}
            //if (ProgramNos == string.Empty)
            //{
            //    ProgramNos = "0";
            //}
            //else
            //{
            //    ProgramNos = ProgramNos.Substring(0, ProgramNos.Length - 1);
            //}
            string Degreenos = ""; string BranchNos = "";
            foreach (ListItem item in ddlProgram.Items)
            {
                if (item.Selected)
                {
                    string[] branchpref = item.Value.Split(',');
                    string selectedValue = item.Value;
                    Degreenos += branchpref[0] + ',';
                    BranchNos += branchpref[1] + ',';
                }
            }
            if (Degreenos.ToString() != "")
            {

                Degreenos = Degreenos.Substring(0, Degreenos.Length - 1);
            }
            // ViewState["DegreeNos"] = Degreenos.Substring(0, Degreenos.Length - 1);
            if (BranchNos.ToString() != "")
            {
                BranchNos = BranchNos.Substring(0, BranchNos.Length - 1);
            }

             DataSet ds = null;
             ds = ObjConfig.GetInterViewDetails(Convert.ToInt32(ddlIntake.SelectedValue), Convert.ToString(StudyLevel), Convert.ToString(Degreenos), Convert.ToString(BranchNos), Convert.ToString("0"), Convert.ToString("0"));
             if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
             {
                 Panel2.Visible = true;
                 lvInterviewSchedule.DataSource = ds.Tables[3];
                 lvInterviewSchedule.DataBind();
             }
             else
             {
                 Panel2.Visible = false;
                 objCommon.DisplayMessage(updInterViewSchedule,"No Record Found !!!",this.Page);
                 lvInterviewSchedule.DataSource = null;
                 lvInterviewSchedule.DataBind();
             }
        }
        catch (Exception ex)
        { 
            
        }
    }
    protected void btnSheduleInterview_Click(object sender, EventArgs e)
    {
        try
        {
            string ProgramNos = "",usernos=""; int count = 0;
            
            foreach (ListItem items in ddlProgram.Items)
            {
                if (items.Selected == true)
                {
                    ProgramNos += items.Value + ',';
                }
            }
            if (ProgramNos == string.Empty)
            {
                ProgramNos = "0";
            }
            else
            {
                ProgramNos = ProgramNos.Substring(0, ProgramNos.Length - 1);
            }
            DataSet dsUserContact = null;
            UserController objUC = new UserController();
            dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));

            foreach (ListViewDataItem items in lvInterviewSchedule.Items)
            {
                CheckBox chk = items.FindControl("chkAllot") as CheckBox;
                Label lblUserNo = items.FindControl("lblUserNo") as Label;
                Label lblEmail = items.FindControl("lblEmail") as Label;
                Label lblStudentName = items.FindControl("lblFullName") as Label;
                if (chk.Checked == true)
                {
                    count++;
                    usernos += lblUserNo.Text + ',';
                    string Subject = dsUserContact.Tables[1].Rows[1]["SUBJECT"].ToString();
                    string message = "";
                    message = dsUserContact.Tables[1].Rows[1]["TEMPLATE"].ToString();

                    Execute(message, lblEmail.Text, Subject, lblStudentName.Text, txtInterViewDate.Text, txttime.Text, txtVenue.Text, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"]), Convert.ToInt32(1), Convert.ToString("0"), Convert.ToString("0")).Wait();
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(updInterViewSchedule, "Please Select Atleast One CheckBox !!!", this.Page);
                return;
            }
            else
            {
                usernos = usernos.Substring(0, usernos.Length - 1);
            }
            CustomStatus cs = CustomStatus.Others;
            cs = (CustomStatus)ObjConfig.InsertInterviewDetails(Convert.ToInt32(ddlIntake.SelectedValue),Convert.ToString(ProgramNos),Convert.ToString(usernos),Convert.ToString(txtInterViewDate.Text),Convert.ToString(txttime.Text),Convert.ToString(txtVenue.Text),Convert.ToString("0"),Convert.ToString("0"),Convert.ToInt32(Session["userno"]),Convert.ToInt32(1));
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updInterViewSchedule, "Interview Schedule Successfully !!!", this.Page);
                Clear();
            }
            else
            {
                objCommon.DisplayMessage(updInterViewSchedule, "Error !!!", this.Page);
                Clear();
            }
        }
        catch (Exception ex)
        { 
            
        }
    }
    protected void Clear()
    {
        ddlIntake.SelectedValue = null;
        ddlStudyLevel.Items.Clear();
        ddlProgram.Items.Clear();
        txtVenue.Text = "";
        txttime.Text = "";
        txtInterViewDate.Text = "";
        Panel2.Visible = false;
        lvInterviewSchedule.DataSource = null;
        lvInterviewSchedule.DataBind();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.ToString());
        Clear();
    }
    static async Task Execute(string message, string toSendAddress, string Subject, string firstname,string Date,string Time,string Venue, string ReffEmail, string ReffPassword,int emailtype,string Status,string Remark)
    {
        try
        {
            //Common objCommon = new Common();

            SmtpMail oMail = new SmtpMail("TryIt");

            oMail.From = ReffEmail;

            oMail.To = toSendAddress;

            oMail.Subject = Subject;

            oMail.HtmlBody = message;

            if (emailtype == 1)
            {
                oMail.HtmlBody = oMail.HtmlBody.Replace("[USERFIRSTNAME]", firstname.ToString());
                oMail.HtmlBody = oMail.HtmlBody.Replace("[DATE]", Date.ToString());
                oMail.HtmlBody = oMail.HtmlBody.Replace("[TIME]", Time.ToString());
                oMail.HtmlBody = oMail.HtmlBody.Replace("[VENUE]", Venue.ToString());
            }
            if (emailtype == 2)
            {
                oMail.HtmlBody = oMail.HtmlBody.Replace("[USERFIRSTNAME]", firstname.ToString());
                oMail.HtmlBody = oMail.HtmlBody.Replace("[STATUS]", Status.ToString());
                oMail.HtmlBody = oMail.HtmlBody.Replace("[REMARK]", Remark.ToString());
            }
            SmtpServer oServer = new SmtpServer("smtp.office365.com");
            oServer.User = ReffEmail;
            oServer.Password = ReffPassword;
            oServer.Port = 587;
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
            oSmtp.SendMail(oServer, oMail);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlResultIntake_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlResultIntake.SelectedIndex > 0)
            {
                ddlStudyLevelResult.Items.Clear();

                DataSet ds = null;
                ds = ObjConfig.GetInterViewDetails(Convert.ToInt32(ddlResultIntake.SelectedValue), Convert.ToString("0"), Convert.ToString("0"), Convert.ToString("0"), Convert.ToString("0"), Convert.ToString("0"));
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    ddlStudyLevelResult.DataSource = ds.Tables[1];
                    ddlStudyLevelResult.DataValueField = "UA_SECTION";
                    ddlStudyLevelResult.DataTextField = "UA_SECTIONNAME";
                    ddlStudyLevelResult.DataBind();
                }
            }
            else
            {
                ddlStudyLevelResult.Items.Clear();
                ddlProgramResult.Items.Clear();
                lvInterviewResult.DataSource = null;
                lvInterviewResult.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlStudyLevelResult_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string StudyLevel = "";
            foreach (ListItem items in ddlStudyLevelResult.Items)
            {
                if (items.Selected == true)
                {
                    StudyLevel += items.Value + ',';
                }
            }
            if (StudyLevel == string.Empty)
            {
                ddlProgramResult.Items.Clear();
                return;
            }
            else
            {
                StudyLevel = StudyLevel.Substring(0, StudyLevel.Length - 1);
            }

            if (ddlResultIntake.SelectedIndex > 0)
            {
                ddlProgramResult.Items.Clear();

                DataSet ds = null;
                ds = ObjConfig.GetInterViewDetails(Convert.ToInt32(ddlResultIntake.SelectedValue), Convert.ToString(StudyLevel), Convert.ToString("0"), Convert.ToString("0"), Convert.ToString("0"), Convert.ToString("0"));
                if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    ddlProgramResult.DataSource = ds.Tables[2];
                    ddlProgramResult.DataValueField = "DEGREENO";
                    ddlProgramResult.DataTextField = "DEGREENAME";
                    ddlProgramResult.DataBind();
                }
            }
            else
            {
                ddlProgramResult.Items.Clear();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlProgramResult_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlInterViewDate.Items.Clear();
            ddlInterViewDate.Items.Insert(0, "Please Select");
            string StudyLevel = "", ProgramNos = "";
            foreach (ListItem items in ddlStudyLevelResult.Items)
            {
                if (items.Selected == true)
                {
                    StudyLevel += items.Value + ',';
                }
            }
            if (StudyLevel == string.Empty)
            {
                ddlProgramResult.Items.Clear();
                return;
            }
            else
            {
                StudyLevel = StudyLevel.Substring(0, StudyLevel.Length - 1);
            }
            //foreach (ListItem items in ddlProgramResult.Items)
            //{
            //    if (items.Selected == true)
            //    {
            //        ProgramNos += items.Value + ',';
            //    }
            //}
            //if (ProgramNos != string.Empty)
            //{
            //    ProgramNos = ProgramNos.Substring(0, ProgramNos.Length - 1);
            //}
            string Degreenos = ""; string BranchNos = "";
            foreach (ListItem item in ddlProgramResult.Items)
            {
                if (item.Selected)
                {
                    string[] branchpref = item.Value.Split(',');
                    string selectedValue = item.Value;
                    Degreenos += branchpref[0] + ',';
                    BranchNos += branchpref[1] + ',';
                }
            }
            if (Degreenos.ToString() != "")
            {

                Degreenos = Degreenos.Substring(0, Degreenos.Length - 1);
            }
            // ViewState["DegreeNos"] = Degreenos.Substring(0, Degreenos.Length - 1);
            if (BranchNos.ToString() != "")
            {
                BranchNos = BranchNos.Substring(0, BranchNos.Length - 1);
            }

            if (ddlResultIntake.SelectedIndex > 0)
            {
                DataSet ds = null;
                ds = ObjConfig.GetInterViewDetails(Convert.ToInt32(ddlResultIntake.SelectedValue), Convert.ToString(StudyLevel), Convert.ToString(Degreenos), Convert.ToString(BranchNos), Convert.ToString("0"), Convert.ToString("0"));
                if (ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
                {
                    ddlInterViewDate.DataSource = ds.Tables[4];
                    ddlInterViewDate.DataValueField = "DATEANDTIME";
                    ddlInterViewDate.DataTextField = "DATEANDTIME1";
                    ddlInterViewDate.DataBind();
                }
            }
            else
            {
                ddlInterViewDate.Items.Clear();
                ddlProgramResult.Items.Clear();
                ddlStudyLevelResult.Items.Clear();
                ddlResultIntake.Items.Clear();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnShowResult_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlResultIntake.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updInterviewResult, "Please Select Intake !!!", this.Page);
                return;
            }
            string StudyLevel = "", ProgramNos = ""; string[] datetime;
            foreach (ListItem items in ddlStudyLevelResult.Items)
            {
                if (items.Selected == true)
                {
                    StudyLevel += items.Value + ',';
                }
            }
            if (StudyLevel == string.Empty)
            {
                StudyLevel = "0";
                objCommon.DisplayMessage(updInterviewResult, "Please Select Study Level !!!", this.Page);
                return;
            }
            else
            {
                StudyLevel = StudyLevel.Substring(0, StudyLevel.Length - 1);
            }
            //foreach (ListItem items in ddlProgramResult.Items)
            //{
            //    if (items.Selected == true)
            //    {
            //        ProgramNos += items.Value + ',';
            //    }
            //}
            //if (ProgramNos == string.Empty)
            //{
            //    ProgramNos = "0";
            //    objCommon.DisplayMessage(updInterviewResult, "Please Select Program !!!", this.Page);
            //    return;
            //}
            //else
            //{
            //    ProgramNos = ProgramNos.Substring(0, ProgramNos.Length - 1);
            //}
            string Degreenos = ""; string BranchNos = "";
            foreach (ListItem item in ddlProgramResult.Items)
            {
                if (item.Selected)
                {
                    string[] branchpref = item.Value.Split(',');
                    string selectedValue = item.Value;
                    Degreenos += branchpref[0] + ',';
                    BranchNos += branchpref[1] + ',';
                }
            }
            if (Degreenos.ToString() != "")
            {

                Degreenos = Degreenos.Substring(0, Degreenos.Length - 1);
            }
            // ViewState["DegreeNos"] = Degreenos.Substring(0, Degreenos.Length - 1);
            if (BranchNos.ToString() != "")
            {
                BranchNos = BranchNos.Substring(0, BranchNos.Length - 1);
            }

            DataSet ds = null;
            if (ddlInterViewDate.SelectedIndex == 0)
            {
                datetime = "0,0".Split(',');
                objCommon.DisplayMessage(updInterviewResult, "Please Select Interview Date !!!", this.Page);
                return;
            }
            else
            {
                datetime = ddlInterViewDate.SelectedValue.Split(',');
            }
            ds = ObjConfig.GetInterViewDetails(Convert.ToInt32(ddlResultIntake.SelectedValue), Convert.ToString(StudyLevel), Convert.ToString(Degreenos), Convert.ToString(BranchNos), Convert.ToString(datetime[0]), Convert.ToString(datetime[1]));
            if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
            {
                Panel3.Visible = true;
                lvInterviewResult.DataSource = ds.Tables[3];
                lvInterviewResult.DataBind();
                btnSubmitResult.Visible = true;

                foreach(ListViewDataItem row in lvInterviewResult.Items)
                {
                    Label lblStatus = row.FindControl("lblStats") as Label;
                    DropDownList ddlStatus = row.FindControl("ddlStatus") as DropDownList;
                    for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                    {
                        if (lblStatus.Text.ToString() == ds.Tables[3].Rows[i]["INTERVIEW_RESULT_STATUS"].ToString())
                        {
                            ddlStatus.SelectedValue = ds.Tables[3].Rows[i]["INTERVIEW_RESULT_STATUS"].ToString();
                        }
                    }
                }
            }
            else
            {

                objCommon.DisplayMessage(updInterviewResult, "No Record Found !!!", this.Page);
                lvInterviewResult.DataSource = null;
                Panel3.Visible = false;
                lvInterviewResult.DataBind();
                btnSubmitResult.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnClearResult_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.ToString());
        ddlResultIntake.SelectedIndex=0;
        ddlStudyLevelResult.ClearSelection();
        ddlProgramResult.ClearSelection();
        ddlInterViewDate.SelectedIndex = 0;
        btnSubmitResult.Visible = false;
        Panel3.Visible = false;
        lvInterviewResult.DataSource = null;
        lvInterviewResult.DataBind();
    }
    protected void btnSubmitResult_Click(object sender, EventArgs e)
    {
        try
        {
            string ProgramNos = "", usernos = "", status = "", remark = ""; int count = 0; string[] datetime;

            foreach (ListItem items in ddlProgramResult.Items)
            {
                if (items.Selected == true)
                {
                    ProgramNos += items.Value + ',';
                }
            }
            if (ProgramNos == string.Empty)
            {
                ProgramNos = "0";
            }
            else
            {
                ProgramNos = ProgramNos.Substring(0, ProgramNos.Length - 1);
            }
            DataSet dsUserContact = null;
            UserController objUC = new UserController();
            dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));
            if (ddlInterViewDate.SelectedIndex == 0)
            {
                datetime = "0,0".Split(',');
            }
            else
            {
                datetime = ddlInterViewDate.SelectedValue.Split(',');
            }
            foreach (ListViewDataItem items in lvInterviewResult.Items)
            {
                CheckBox chk = items.FindControl("chkResult") as CheckBox;
                Label lblUserNo = items.FindControl("lblUserNoResult") as Label;
                Label lblUserName = items.FindControl("lblUserNameResult") as Label;
                Label lblEmail = items.FindControl("lblEmailResult") as Label;
                TextBox txtRemark = items.FindControl("txtRemark") as TextBox;
                Label lblStudentName = items.FindControl("lblFullNameResult") as Label;
                DropDownList ddlStatus = items.FindControl("ddlStatus") as DropDownList;
                
                if (chk.Checked == true)
                {
                    count++;
                    usernos += lblUserNo.Text + ',';
                    if (ddlStatus.SelectedIndex == 0)
                    {
                        objCommon.DisplayMessage(updInterViewSchedule, "Please Select Status for Application ID :- " + lblUserName .Text + " !!!", this.Page);
                        return;
                    }
                    else
                    {
                        status += ddlStatus.SelectedValue + ',';
                    }
                    if (Convert.ToString(txtRemark.Text) == string.Empty)
                    {
                        objCommon.DisplayMessage(updInterViewSchedule, "Please Enter Remark for Application ID :- " + lblUserName.Text + " !!!", this.Page);
                        return;
                    }
                    else
                    {
                        remark += txtRemark.Text + ',';
                    }
                    string Subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                    string message = "";
                    message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();

                    Execute(message, lblEmail.Text, Subject, lblStudentName.Text, txtInterViewDate.Text, txttime.Text, txtVenue.Text, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"]), Convert.ToInt32(2), Convert.ToString(ddlStatus.SelectedItem.Text),Convert.ToString(txtRemark.Text)).Wait();
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(updInterViewSchedule, "Please Select Atleast One CheckBox !!!", this.Page);
                return;
            }
            else
            {
                usernos = usernos.Substring(0, usernos.Length - 1);
                status = status.Substring(0, status.Length - 1);
                remark = remark.Substring(0, remark.Length - 1);
            }
            CustomStatus cs = CustomStatus.Others;
            cs = (CustomStatus)ObjConfig.InsertInterviewDetails(Convert.ToInt32(ddlResultIntake.SelectedValue), Convert.ToString(ProgramNos), Convert.ToString(usernos), Convert.ToString(datetime[0]), Convert.ToString(datetime[1]), Convert.ToString(0),Convert.ToString(status),Convert.ToString(remark),Convert.ToInt32(Session["userno"]), Convert.ToInt32(2));
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updInterViewSchedule, "Interview Result Saved Successfully !!!", this.Page);
                ClearResult();
            }
            else
            {
                objCommon.DisplayMessage(updInterViewSchedule, "Error !!!", this.Page);
                ClearResult();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ClearResult()
    {
        btnSubmitResult.Visible = false;
        ddlResultIntake.SelectedValue = null;
        ddlProgramResult.Items.Clear();
        ddlStudyLevelResult.Items.Clear();
        ddlInterViewDate.SelectedValue = null;
        Panel3.Visible = false;
        lvInterviewResult.DataSource = null;
        lvInterviewResult.DataBind();

    }
    protected void lkschedule_Click(object sender, EventArgs e)
    {
        divlkinterview.Attributes.Remove("class");
        divlkschedule.Attributes.Add("class", "active");
        divSchedule.Visible = true;
        divInterview.Visible = false;
        Panel3.Visible = false;
        lvInterviewResult.DataSource = null;
        lvInterviewResult.DataBind();
        ddlResultIntake.SelectedIndex=0;
        ddlStudyLevelResult.ClearSelection();
        ddlProgramResult.ClearSelection();
        ddlInterViewDate.SelectedIndex = 0;

    }
    protected void lnkinterview_Click(object sender, EventArgs e)
    {
        divlkschedule.Attributes.Remove("class");
        divlkinterview.Attributes.Add("class", "active");
        divInterview.Visible = true;
        divSchedule.Visible = false;
        Panel2.Visible = false;
        lvInterviewSchedule.DataSource = null;
        lvInterviewSchedule.DataBind();

        ddlIntake.SelectedIndex = 0;
        ddlStudyLevel.ClearSelection();
        ddlProgram.ClearSelection();
        txtInterViewDate.Text="";
        txttime.Text="";
        txtVenue.Text = "";
    }
}