//======================================================================================
// PROJECT NAME  : SVCE
// MODULE NAME   : ACADEMIC
// PAGE NAME     : New Student Approval
// CREATION DATE : 19/06/19
// CREATED BY    : Dipali N
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
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using mastersofterp;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
using ClosedXML.Excel;
using System.Net.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Newtonsoft.Json;
using System.Web;



public partial class Academic_NewStudentApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController ObjNuc = new FeeCollectionController();
    StudentFees objStudentFees = new StudentFees();
    StudentController studcon = new StudentController();

    #region Page Events

    //USED FOR INITIALSING THE MASTER PAGE
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    //USED FOR BYDEFAULT LOADING THE default PAGE
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
                    Page.Title = Session["coll_name"].ToString();

                    ViewState["ipAddress"] = GetUserIPAddress();
                    this.objCommon.FillDropDownList(ddlpaytype, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "", "");
                    this.objCommon.FillDropDownList(ddlAdmQuota, "ACD_ADMISSION_QUOTA", "DISTINCT ADMQUOTANO", "QUOTANAME", "ADMQUOTANO>0", "ADMQUOTANO");
                    this.objCommon.FillDropDownList(ddlhostel, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "BELONGS_TO = 'H'", "RECIEPT_CODE");
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_CreateDemand.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    // IPADRESS  DETAILS ..
    private string GetUserIPAddress()
    {
        string User_IPAddress = string.Empty;
        string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
        {
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];

        }
        else////with Proxy detection
        {
            string[] splitter = { "," };
            string[] IP_Array = User_IPAddressRange.Split(splitter, System.StringSplitOptions.None);

            int LatestItem = IP_Array.Length - 1;

        }
        return User_IPAddress;
    }

    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page. 
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

    //Search button is used for to Showin the register Student details on lable controllers.
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ddlpaytype.Enabled = true;
        ddlAdmQuota.Enabled = true;
        GetStudentDetails();
    }

    //button approve is used for to Approve the register student details.
    protected void btnapproval_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtAdmdate.Text != string.Empty)
            {
                string fathermobile = objCommon.LookUp("ACD_STUDENT", "DISTINCT FATHERMOBILE", "TANNO='" + txtEnrollmentSearch.Text + "'") == string.Empty ? "" : objCommon.LookUp("ACD_STUDENT", "DISTINCT FATHERMOBILE", "TANNO='" + txtEnrollmentSearch.Text + "'");
                string password = clsTripleLvlEncyrpt.ThreeLevelEncrypt(fathermobile.ToString());

                DataSet ds = studcon.GetApprovedStudentDetails(Convert.ToInt32(ViewState["IDNO"].ToString()), 2, password);

                if (fathermobile.ToString() != "")
                {
                    objCommon.SendSMS(fathermobile.ToString(), "Account for the Parents of candidate " + ViewState["STUDNAME"].ToString() + " has been created successfully! Parents can now login with Username : " + fathermobile.ToString() + "  Password : " + "" + fathermobile.ToString() + "\nFollow this link for further process https://mnrtest.mastersofterp.in/" + "");
                }

                string fatheremail = objCommon.LookUp("ACD_STUDENT", "DISTINCT FATHER_EMAIL", "TANNO='" + txtEnrollmentSearch.Text + "'") == string.Empty ? "" : objCommon.LookUp("ACD_STUDENT", "DISTINCT FATHER_EMAIL", "TANNO='" + txtEnrollmentSearch.Text + "'");

                if (fatheremail.ToString() != "")
                {
                    ParentExecute("Account for the Parents of candidate <b>" + ViewState["STUDNAME"].ToString() + "</b> has been created successfully! Parents can now login with Username : <b>" + fathermobile.ToString() + "</b>  Password : <b>" + fathermobile.ToString() + "</b>\nFollow this link for further process https://mnrtest.mastersofterp.in/" + "", ViewState["EMAILID"].ToString().Trim()).Wait();
                }

                objCommon.DisplayMessage(this.pnlFeeTable, "Student Approved Successfully! \\n Admission No. is : " + lblenrollno.Text + "\\n.", this.Page);

                string message = "Student with Admission No.: " + lblenrollno.Text + " has been approved successfully!";
               
                btnapproval.Enabled = false;
                ddlpaytype.Enabled = false;
                ddlAdmQuota.Enabled = false;
                rdbhostelNo.Enabled = false;
                rdbhostelyes.Enabled = false;
                rdbtransportNo.Enabled = false;
                rdbtransportyes.Enabled = false;

                Execute(message, ViewState["EMAILID"].ToString().Trim(), ViewState["IDNO"].ToString()).Wait();
            }
            else
            {
                objCommon.DisplayMessage(this.pnlFeeTable, "Please Enter Student Admission Date !!", this);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_CreateDemand.btnCreateDemand_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //refresh current page or reload current page
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    //on select pf payment type getting Fees count and count is 0 or not if 0 then button Fees Process enability true else false.
    protected void ddlpaytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        int count = 0;
    }

    //bind Registerd Student deatils on lables and and image  and showing demand details.
    public void GetStudentDetails()
    {
        int idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "TANNO='" + txtEnrollmentSearch.Text + "'") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "TANNO='" + txtEnrollmentSearch.Text + "'"));

        DataSet ds = studcon.GetApprovedStudentDetails(idno, 1, "");

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["IDNO"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
                txtStudName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                ViewState["STUDNAME"] = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                lblapp.Text = ds.Tables[0].Rows[0]["TANNO"].ToString();
                lblenrollno.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
                lbldegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                lbldegree.ToolTip = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                lblbranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                lblbranch.ToolTip = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                txtMobile.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                ViewState["EMAILID"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                lblSession.Text = ds.Tables[0].Rows[0]["SESSIONNAME"].ToString();
                lblSession.ToolTip = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
                lblcategory.Text = ds.Tables[0].Rows[0]["CATEGORY"].ToString();
                hdfadmbatch.Value = ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
                lbladmbatch.Text = ds.Tables[0].Rows[0]["ADMBATCHNAME"].ToString();
                lblsem.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                lblsem.ToolTip = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                txtAdmdate.Text = ds.Tables[0].Rows[0]["ADMDATE"].ToString();
                txtStudInitial.Text = ds.Tables[0].Rows[0]["STUDLASTNAME"].ToString();
                string ptype = ds.Tables[0].Rows[0]["PTYPE"].ToString();
                lblAmount.Text = ds.Tables[0].Rows[0]["TOTAL_AMT"].ToString();
                lblFeeStatus.Text = ds.Tables[0].Rows[0]["FEESTATUS"].ToString();

                if (Convert.ToInt32(ptype) > 0)
                {
                    ddlpaytype.SelectedValue = ptype;
                }

                if (Convert.ToInt32(ds.Tables[0].Rows[0]["PTYPE"].ToString()) > 0)
                {
                    ddlAdmQuota.SelectedValue = ds.Tables[0].Rows[0]["PTYPE"].ToString();
                }

                string hostel = (ds.Tables[0].Rows[0]["HOSTELER"].ToString());
                int Transport = Convert.ToInt32(ds.Tables[0].Rows[0]["TRANSPORT"].ToString());

                if (Convert.ToBoolean(hostel) == false)
                {
                    rdbhostelyes.Checked = false;
                    rdbhostelNo.Checked = true;
                }
                else
                {
                    rdbhostelyes.Checked = true;
                    rdbhostelNo.Checked = false;
                }

                if (Transport == 0)
                {
                    rdbtransportyes.Checked = false;
                    rdbtransportNo.Checked = true;
                }
                else
                {
                    rdbtransportyes.Checked = true;
                    rdbtransportNo.Checked = false;
                }

                int TransportWithAC = Convert.ToInt32(ds.Tables[0].Rows[0]["TRANSPORT_WITH_AC"].ToString());
                if (TransportWithAC == 0)
                {
                    rdbTransportACyes.Checked = false;
                    rdbTransportACno.Checked = true;
                }
                else
                {
                    rdbTransportACyes.Checked = true;
                    rdbTransportACno.Checked = false;
                }

                string Status = ds.Tables[0].Rows[0]["APP_STATUS"].ToString();

                imgpreview.ImageUrl = "~/showimage.aspx?id=" + lblapp.Text + "&type=student";

                if (Convert.ToInt32(Status) == 1)
                {
                    objCommon.DisplayMessage(this.pnlFeeTable, "Student has already been approved successfully! \\n Admission No. is : " + lblenrollno.Text + "\\n.", this);

                    btnapproval.Enabled = false;
                    ddlpaytype.Enabled = false;
                    ddlAdmQuota.Enabled = false;
                    rdbhostelNo.Enabled = false;
                    rdbhostelyes.Enabled = false;
                    rdbtransportNo.Enabled = false;
                    rdbtransportyes.Enabled = false;
                }
                else
                {
                    btnapproval.Enabled = true;
                }

            }
            else
            {
                objCommon.DisplayMessage(this.pnlFeeTable, "Student Registration is Not Done or Student Completed all Registration Process!", this);
                clear();
                txtEnrollmentSearch.Text = string.Empty;
                return;
            }
        }
        else
        {
            objCommon.DisplayMessage(this.pnlFeeTable, "Please Enter Valid Temp No.!", this);
            clear();
            return;
        }
    }

    //used for sending email for addmissiuon approve successfuly.
    public void SendEmail(string mailId, string name)
    {
        try
        {
            string EMAILID = mailId.Trim();
            var fromAddress = objCommon.LookUp("REFF", "LTRIM(RTRIM(EMAILSVCID))", "");

            // any address where the email will be sending
            var toAddress = EMAILID.Trim();

            //Password of your gmail address
            var fromPassword = objCommon.LookUp("REFF", "LTRIM(RTRIM(EMAILSVCPWD))", "");

            // Passing the values and make a email formate to display
            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            msg.From = new MailAddress(fromAddress, "SVCE");
            msg.To.Add(new MailAddress(toAddress));
            msg.Subject = "Admission Approved Successfully";

            using (StreamReader reader = new StreamReader(Server.MapPath("~/approval_template.html")))
            {
                msg.Body = reader.ReadToEnd();
            }

            msg.Body = msg.Body.Replace("{Name}", name);
            msg.Body = msg.Body.Replace("{AdmissionNumber}", lblenrollno.Text);

            msg.IsBodyHtml = true;
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential(fromAddress.Trim(), fromPassword.Trim());
            ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate,
                X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
            try
            {
                smtp.Send(msg);
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUaimsCommon.ShowError(Page, "ACADEMIC_NewStudentApproval_SemdEmail-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUaimsCommon.ShowError(Page, "Server UnAvailable");
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_NewStudentApproval_SemdEmail-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //used for reset all the labels controllers.
    public void clear()
    {
        lblname.Text = string.Empty;
        lblapp.Text = string.Empty;
        lbldegree.Text = string.Empty;
        lblbranch.Text = string.Empty;
        lblmobile.Text = string.Empty;
        lblEmail.Text = string.Empty;
        lblSession.Text = string.Empty;
        lblcategory.Text = string.Empty;
        lbladmbatch.Text = string.Empty;
        lblsem.Text = string.Empty;
        txtAdmdate.Text = string.Empty;
        rdbhostelNo.Checked = false;
        rdbhostelyes.Checked = false;
        rdbtransportyes.Checked = false;
        rdbtransportNo.Checked = false;
        ddlpaytype.SelectedIndex = 0;
        ddlAdmQuota.SelectedIndex = 0;
        lbladmquota.Text = string.Empty;
        lbllastname.Text = string.Empty;
        lblenrollno.Text = string.Empty;
        imgpreview.Dispose();
        imgpreview.ImageUrl = null;
        txtStudName.Text = string.Empty;
        txtStudInitial.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtMobile.Text = string.Empty;
    }

    protected void rdbhostelyes_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbhostelyes.Checked == true) { ddlhostel.Enabled = true; ddlhostel.SelectedIndex = 0; } else { ddlhostel.Enabled = false; ddlhostel.SelectedIndex = 0; }
    }

    static async Task Execute(string Message, string toEmailId, string idno)
    {
        MemoryStream oAttachment = ShowGeneralExportReportForMail("Reports,Academic,rptStudentConfirmReport.rpt", "@P_COLLEGE_CODE=51,@P_IDNO=" + idno.ToString());

        var bytesRpt = oAttachment.ToArray();
        var fileRpt = Convert.ToBase64String(bytesRpt);

        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString(), "MNR");
            var toAddress = new MailAddress(toEmailId, "");
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString(), "MNR");
            var subject = "Student Registration Approval";
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            msg.AddAttachment("rptStudentConfirmReport.pdf", fileRpt);

            var response = await client.SendEmailAsync(msg);
        }
        catch (Exception ex)
        {

        }
    }

    static private MemoryStream ShowGeneralExportReportForMail(string path, string paramString)
    {
        MemoryStream oStream;
        ReportDocument customReport;
        customReport = new ReportDocument();
        string reportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Academic/rptStudentConfirmReport.rpt");

        customReport.Load(reportPath);
       
        char ch = ',';
        string[] val = paramString.Split(ch);
        if (customReport.ParameterFields.Count > 0)
        {
            for (int i = 0; i < val.Length; i++)
            {
                int indexOfEql = val[i].IndexOf('=');
                int indexOfStar = val[i].IndexOf('*');

                string paramName = string.Empty;
                string value = string.Empty;
                string reportName = "MainRpt";

                paramName = val[i].Substring(0, indexOfEql);

                if (indexOfStar > 0)
                {
                    value = val[i].Substring(indexOfEql + 1, ((indexOfStar - 1) - indexOfEql));
                    reportName = val[i].Substring(indexOfStar + 1);
                }
                else
                {
                    value = val[i].Substring(indexOfEql + 1);
                }

                if (reportName == "MainRpt")
                {
                    if (value == "null")
                    {
                        customReport.SetParameterValue(paramName, null);
                    }
                    else
                        customReport.SetParameterValue(paramName, value);
                }
                else
                    customReport.SetParameterValue(paramName, value, reportName);
            }
        }

        /// set login details & db details for report document
        ConfigureCrystalReports(customReport);

        for (int i = 0; i < customReport.Subreports.Count; i++)
        {
            ConfigureCrystalReports(customReport.Subreports[i]);
        }

        oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        return oStream;
    }

    static private void ConfigureCrystalReports(ReportDocument customReport)
    {
        ConnectionInfo connectionInfo = Common.GetCrystalConnection();
        Common.SetDBLogonForReport(connectionInfo, customReport);
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        string reportTitle = "StudentResultRemark";
        string rptFileName = "rptStudentConfirmReport.rpt";
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + ViewState["IDNO"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.pnlFeeTable, this.pnlFeeTable.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {

        }
    }

    static async Task ParentExecute(string Message, string toEmailId)
    {
        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString(), "MNR");
            var toAddress = new MailAddress(toEmailId, "");
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString(), "MNR");
            var subject = "Parent Login Details";
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);
        }
        catch (Exception ex)
        {

        }
    }

}