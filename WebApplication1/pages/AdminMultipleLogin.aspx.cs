//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC/EXAMINATION
// PAGE NAME     : AdminMultipleLogin.ASPX
// CREATION DATE : 06-June-2023
// CREATED BY    : Aashna M
// MODIFIED BY   :
// MODIFIED DATE  :
// MODIFIED DESC :
//======================================================================================

using EASendMail;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using SendGrid;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.BusinessEntities.Common;
using Newtonsoft.Json;
using System.Configuration;
using DynamicAL;
using Newtonsoft.Json.Linq;

public partial class ACADEMIC_AdminMultipleLogin : System.Web.UI.Page
{
    private Common objCommon = new Common();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, string.Empty);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            divMsg.InnerHtml = string.Empty;
            // pnlVerifyOTP.Visible = false;
            if (!Page.IsPostBack)
            {
                //Check Session
                if (Session["userno"] == null && Session["username"] == null &&
                    Session["usertype"] == null && Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    CheckPageAuthorization();

                    //Populate the user dropdownlist with username and userid
                    PopulateDropDownList();
                    //PopulateAccessLink();

                    //ShowPanel();
                    ViewState["action"] = null;
                }
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Sarang on 28/12/2021
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void CheckPageAuthorization()
    {
        try
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=AdminMultipleLogin.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=AdminMultipleLogin.aspx");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            DataSet dsUser = objCommon.FillDropDown("USER_RIGHTS", "USERTYPEID", "USERDESC", "USERTYPEID IN (2,3)", "USERTYPEID");
            ddlUserType.DataSource = dsUser;
            ddlUserType.DataTextField = dsUser.Tables[0].Columns[1].ToString();
            ddlUserType.DataValueField = dsUser.Tables[0].Columns[0].ToString();
            ddlUserType.DataBind();

            ddlDept.Items.Add(new ListItem("Please Select", "0"));
            DataSet dsAcd = objCommon.GetDropDownData("PKG_DROPDOWN_SP_ALLACDDEPT");
            ddlDept.DataSource = dsAcd;
            ddlDept.DataTextField = dsAcd.Tables[0].Columns[1].ToString();
            ddlDept.DataValueField = dsAcd.Tables[0].Columns[0].ToString();
            ddlDept.DataBind();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //  dvReason.Visible = false;
            lvlinks.Visible = false;
            if (ddlUserType.SelectedIndex > 0)
            {
                // dvReason.Visible = true;
                // ShowPanel();
                // trDept.Visible = (Convert.ToInt32(ddlUserType.SelectedValue) == 2) ? false : true;

                txtSearch.Text = "";

                pnlStudent.Visible = true;

                lblSearch.Text = (Convert.ToInt32(ddlUserType.SelectedValue) == 2) ? "Enter User Name" : "Enter User Name";

                rfvSearch.Enabled = true;
                rfvSearch.ErrorMessage = (Convert.ToInt32(ddlUserType.SelectedValue) == 2) ? "Please Enter User Name." : "Please Enter User Name";
            }
            else
            {
                pnlStudent.Visible = false;
                trDept.Visible = false;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ShowPanel()
    {
        try
        {
            DataSet ds = null;
            if (Convert.ToInt32(ddlUserType.SelectedValue) != 2)
                ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID", "UA_NO,UA_FULLNAME,UA_NAME", "UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "UA_NO IS NOT NULL AND  UA_STATUS = 0 AND (UA_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " OR " + Convert.ToInt32(ddlDept.SelectedValue) + "=0)" + " AND  UA_TYPE=" + ddlUserType.SelectedValue, "UA_TYPE,UA_NO");

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvlinks.DataSource = ds;
                    lvlinks.DataBind();
                    //lblEmpty.Visible = false;
                    lvlinks.Visible = true;
                }
                else
                {
                    //lblEmpty.Visible = true;
                    lvlinks.Visible = false;
                }
            }
            else
                lvlinks.Visible = false;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ShowPanelStudent(string usrName)
    {
        try
        {
            DataSet ds = null;
            if (ddlUserType.SelectedValue == "2")
            {
                ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID INNER JOIN ACD_STUDENT S ON A.UA_IDNO= S.IDNO",
                    "A.UA_NO,UA_FULLNAME,UA_NAME",
                    "UA_PWD,UA_TYPE,UA_STATUS,USERDESC",
                   "UA_TYPE=2 AND UA_STATUS = 0 AND CAN=0 AND ADMCAN=0 AND UA_NAME='" + usrName.ToString() + "'", "UA_FULLNAME");
            }
            else if (ddlUserType.SelectedValue == "01")
            {
                ds = objCommon.FillDropDown("ACD_USER_REGISTRATION", "USERNO AS UA_NO,CONCAT(FIRSTNAME, ' ', LASTNAME)AS UA_FULLNAME,USERNAME AS UA_NAME", "USER_PASSWORD AS UA_PWD", "USERNAME='" + usrName.ToString() + "'", "USERNAME");
            }
            else
                ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID",
                    "UA_NO,UA_FULLNAME,UA_NAME", "UA_PWD,UA_TYPE,UA_STATUS,USERDESC",
                    "UA_NO IS NOT NULL AND  UA_STATUS = 0  AND  UA_TYPE=" + ddlUserType.SelectedValue +
                    " AND (UA_NAME LIKE '%" + usrName + "%' OR UA_FULLNAME LIKE '%" + usrName + "%') ", "UA_TYPE,UA_NO");

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvlinks.DataSource = ds;
                    lvlinks.DataBind();
                    //lblEmpty.Visible = false;
                    lvlinks.Visible = true;
                }
                else
                {
                    //lblEmpty.Visible = true;
                    lvlinks.Visible = false;
                    objCommon.DisplayMessage(updpnlUser, "No Records Found.", this.Page);
                }
            }
            else
            {
                //lblEmpty.Visible = true;
                lvlinks.Visible = false;
                objCommon.DisplayMessage(updpnlUser, "No Records Found.", this.Page);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDept.SelectedIndex > 0)
                ShowPanel();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            string ipAddress = string.Empty;
            User_AccController objUC = new User_AccController();
            string macAddress = string.Empty;
            LinkButton btnLogin = sender as LinkButton;
            string username = btnLogin.CommandArgument.Trim();
            int userno = Convert.ToInt32(btnLogin.ToolTip);
            string lastlogout = string.Empty;
            ViewState["userno"] = userno;
            ViewState["UA_NAME"] = username;

            ViewState["adminEmailID"] = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_EMAIL", "UA_NAME='" + Session["username"].ToString() + "' and UA_NAME IS NOT NULL");
            ViewState["AdminMobileNo"] = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_MOBILE", "UA_NAME='" + Session["username"].ToString() + "' and UA_NAME IS NOT NULL");
            string user_Fullname = string.Empty;
            if (ddlUserType.SelectedValue == "01")
            {
                user_Fullname = objCommon.LookUp("ACD_USER_REGISTRATION WITH (NOLOCK)", "CONCAT(FIRSTNAME, ' ', LASTNAME) AS UA_FULLNAME", "USERNAME='" + username + "' and USERNAME IS NOT NULL");
            }
            else
            {
                user_Fullname = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_FULLNAME", "UA_NAME='" + username + "' and UA_NAME IS NOT NULL");
            }

            string Purpose = string.Empty;
            int purposeID = 0;
            foreach (ListViewItem item in lvlinks.Items)
            {
                TextBox txtPurpose = item.FindControl("txtReson") as TextBox;
                DropDownList ddlPurpose = item.FindControl("ddlPurpose") as DropDownList;
                Button btnAd = item.FindControl("btnAddReason") as Button;
                if (btnAd.CommandArgument == btnLogin.CommandArgument)
                {
                    Purpose = (ddlPurpose.SelectedIndex > 0) ? ddlPurpose.SelectedItem.Text : txtPurpose.Text;
                    purposeID = (ddlPurpose.SelectedIndex > 0) ? Convert.ToInt16(ddlPurpose.SelectedValue) : 1;
                    break;
                }
            }

            if (string.IsNullOrEmpty(Purpose))
            {
                objCommon.DisplayMessage(updpnlUser, "Kindly submit the Purpose for Login.", this.Page);
                return;
            }

            int ua_Type = Convert.ToInt32(ddlUserType.SelectedValue);
            int loginBy = Convert.ToInt32(Session["userno"].ToString());
            string ipAddr = Request.ServerVariables["REMOTE_HOST"];
            StudentController studinfo = new StudentController();
            string OTP = GenerateOTP(5);
            Session["OTP"] = OTP;
            int ret = studinfo.InsMultipleLoginReasonLog(userno, ua_Type, Purpose, loginBy, ipAddr, purposeID, OTP);
            if (ret == 1)
            {
                string usr = "(" + username + " / " + user_Fullname + ")";
                int ret1 = SendEmailSMS(userno, usr, ViewState["adminEmailID"].ToString(), ViewState["AdminMobileNo"].ToString(), OTP);
                if (ret1 == 1)
                {
                    objCommon.DisplayMessage(updpnlUser, "OTP has been send on Your Email Id, Enter To Continue Login Process.", this.Page);
                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "myfunction();", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "Reg", "onTimer();", true);
                }
                else
                {
                    objCommon.DisplayMessage(updpnlUser, "Failed to send Email", this.Page);
                    return;
                }
            }
        }
        catch (Exception)
        {
            objCommon.DisplayMessage("Login Failed !, Please Check Your Username Or Password !", this.Page);
        }
    }

    public string GetMACAddress()
    {
        try
        {
            String st = String.Empty;
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                OperationalStatus ot = nic.OperationalStatus;
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    st = nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return st;
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                string search = txtSearch.Text.Trim();
                ShowPanelStudent(search);
                //txtSearch.Text = string.Empty;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private int SendEmailSMS(int userNo, string userName, string userEmail, string mobileNo, string OTP)
    {
        try
        {
            int ret = 0;
            int email = 1;// email=1 and sms=2;
            string TEMPLATE = string.Empty;
            string TemplateID = string.Empty;
            try
            {
                // successFail=0 --security fail
                //successFail=1 --security passed;
                int successFail = checkOTPSecurity(Convert.ToInt32(ViewState["userno"]), OTP);
                if (successFail == 0)
                    return ret;

                string subject = "ERP || OTP for Login";
                string message = "Your One Time Password is : ";
                message += OTP;
                message += " for User login : " + userName;
                message += "<br /><br />Note :This is system generated email. Please do not reply to this email.<br />";

                if (email == 1)
                {
                    ret = SendEmail(userEmail, message, subject);
                }
                else
                {
                    if (mobileNo != string.Empty && mobileNo.Length == 10)
                    {
                        string templatename = "Admin Multiple Login";
                        User_AccController objUC = new User_AccController();
                        DataSet ds = objUC.GetSMSTemplate(0, templatename);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            TEMPLATE = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
                            TemplateID = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
                        }
                        message = TEMPLATE;
                        message = message.Replace("{#var#}", OTP);

                        // Create a StringBuilder and append the template
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.Append(message);
                        // Get the final message string
                        string template = stringBuilder.ToString();
                        // SendSMS_today(lblParMobile.Text.Trim(), template, TemplateID);
                        string status = this.SendSMS(mobileNo, template, TemplateID);
                        //string status = SendSMS_today(mobileNo, template, TemplateID);
                        ret = status.Contains("Submitted") ? 1 : 0;
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_AdminMultipleLogin.SendEmail-> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server UnAvailable");
            }
            return ret;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GenerateOTP(int length)
    {
        try
        {
            //It will generate string with combination of small,capital letters and numbers
            char[] charArr = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            string randomString = string.Empty;
            Random objRandom = new Random();
            for (int i = 0; i < length; i++)
            {
                //Don't Allow Repetation of Characters
                int x = objRandom.Next(1, charArr.Length);
                if (!randomString.Contains(charArr.GetValue(x).ToString()))
                    randomString += charArr.GetValue(x);
                else
                    i--;
            }

            return randomString;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private DataSet getModuleConfig(int OrganizationId)
    {
        try
        {
            DataSet ds = objCommon.GetModuleConfig(OrganizationId);
            return ds;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public int SendEmail(string ToEmail, string Message, string Subject)
    {
        int status = 0;
        try
        {
            string Res = "";
            EmailSmsWhatsaap Email = new EmailSmsWhatsaap();
            string email_type = string.Empty;
            string Link = string.Empty;
            UserController objUC = new UserController();
            DataSet ds = getModuleConfig(0);
            DataSet dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", "0", Convert.ToInt32(33));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
                Link = ds.Tables[0].Rows[0]["LINK"].ToString();
            }
            //if (email_type == "2" && email_type != "")
            // {
            MemoryStream Attachment = null; string AttachmentName = "";
            Task<string> task = Email.Execute(Message, ToEmail, Subject, Attachment, AttachmentName);
            //Task<int> ret = Execute(Message, ToEmail, Subject, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"]));
            //status = ret.Result;
            Res = task.Result.ToString();
            //Task<string> task = Email.Execute(Message, ToEmail, Subject, null, null);
            //Res = task.Result;
            //}
            if (Res == "Email Send Succesfully")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
        }
        catch (Exception)
        {
        }
        return status;
    }

    public int sendEmail(string mailId, string Message, string Subject)
    {
        int status = 0;
        try
        {
            DataSet ds;
            ds = objCommon.FillDropDown("REFF", "SUBJECT_OTP", "", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
            string Org = (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) ? ds.Tables[0].Rows[0]["SUBJECT_OTP"].ToString() : string.Empty;
            string EMAILID = mailId.Trim();
            var fromAddress = objCommon.LookUp("REFF", "EMAILSVCID", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]));
            // any address where the email will be sending
            var toAddress = EMAILID.Trim();
            //Password of your gmail address

            var fromPassword = objCommon.LookUp("REFF", "(EMAILSVCPWD)", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]));
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            MailMessage msg = new MailMessage();
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

            msg.From = new System.Net.Mail.MailAddress(fromAddress, Org);
            msg.To.Add(new System.Net.Mail.MailAddress(toAddress));
            msg.Subject = Subject;

            msg.IsBodyHtml = true;
            msg.Body = Message;
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new System.Net.NetworkCredential(fromAddress.Trim(), fromPassword.Trim());
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
              { return true; };
            smtp.Send(msg);
            status = 1;
        }
        catch (Exception ex)
        {
            throw;
        }
        return status;
    }

    public int SendBirthdayWishesYCCE(string MsgBody, string Toemail, string Subject)
    {
        int status = 0;
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        string fromMail = "no-reply@crescent.education";
        string password = "crescentmis";
        MailMessage msg = new MailMessage();
        msg.To.Add(new System.Net.Mail.MailAddress(Toemail));
        msg.From = new System.Net.Mail.MailAddress(fromMail);
        msg.Subject = Subject;
        StringBuilder sb = new StringBuilder();
        msg.Body = MsgBody;
        msg.BodyEncoding = Encoding.UTF8;
        msg.IsBodyHtml = true;
        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential(fromMail, password);

        client.Port = 587; // You can use Port 25 if 587 is blocked (mine is)
        client.Host = "smtp-mail.outlook.com"; // "smtp.live.com";
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //client.TargetName = "STARTTLS/smtp.office365.com";
        client.EnableSsl = true;
        try
        {
            client.Send(msg);
            //lblText.Text = "Message Sent Succesfully";
        }
        catch (Exception ex)
        {
            //lblText.Text = ex.ToString();
        }
        return status;
    }

    private int OutLook_Email(string Message, string toEmailId, string sub)
    {
        int ret = 0;
        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            SmtpMail oMail = new SmtpMail("TryIt");
            oMail.From = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();// "no-reply@crescent.education"; // dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();

            oMail.To = toEmailId;
            oMail.Subject = sub;
            oMail.HtmlBody = Message;
            // SmtpServer oServer = new SmtpServer("smtp.live.com");
            SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022
            oServer.User = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();// "no-reply@crescent.education"; //
            oServer.Password = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();  //"crescentmis"; //
            oServer.Port = 587;
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
            Console.WriteLine("start to send email over TLS...");
            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
            oSmtp.SendMail(oServer, oMail);
            // Console.WriteLine("email sent successfully!");
            ret = 1;
        }
        catch (Exception ep)
        {
            Console.WriteLine("failed to send email with the following error:");
            Console.WriteLine(ep.Message);
            ret = 0;
        }
        return ret;
    }

    public string SendSMS(string Mobile, string text, string TemplateID)
    {
        string status = "";
        try
        {
            string Message = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?"));
                request.ContentType = "text/xml; charset=utf-8";
                request.Method = "POST";

                string postDate = "ID=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                postDate += "&";
                postDate += "Pwd=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                postDate += "&";
                postDate += "PhNo=91" + Mobile;
                postDate += "&";
                postDate += "Text=" + text;
                postDate += "&";
                postDate += "TemplateID=" + TemplateID;

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
                request.ContentType = "application/x-www-form-urlencoded";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse _webresponse = request.GetResponse();
                dataStream = _webresponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                status = reader.ReadToEnd();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_AdminMultipleLogin.SendSMS-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        return status;
    }

    public string SendSMS_today(string mobno, string message, string TemplateID)
    {
        string status = "";
        try
        {
            string url = string.Empty;
            string uid = string.Empty;
            string pass = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                url = string.Format(ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?");
                //url = string.Format(ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?");
                uid = ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                pass = ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
                WebRequest request = HttpWebRequest.Create("" + url + "ID=" + uid + "&PWD=" + pass + "&PHNO=" + mobno + "&TEXT=" + message + "&TemplateID=" + TemplateID + "");
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
                WebResponse response = request.GetResponse();
                System.IO.StreamReader reader = new StreamReader(response.GetResponseStream());
                string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need
                //return urlText;
                Session["result"] = 1;
            }
            status = "1";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_AdminMultipleLogin.SendSMS_today-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        return status;
    }

    protected void btnVerifyOTP_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["OTP"].ToString().Trim() != txtVerifyOTP.Text.Trim())
            {
                objCommon.DisplayMessage(updpnlUser, "Please Enter Valid OTP", this.Page);
                txtVerifyOTP.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "Src", " myfunction();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Reg1", "onTimer();", true);
            }
            else
            {
                objCommon.DisplayMessage(updpnlUser, "OTP is verified successfully", this.Page);
                Session["OTP"] = string.Empty;
                txtVerifyOTP.Text = string.Empty;
                LoginAs(ViewState["UA_NAME"].ToString(), Convert.ToInt32(ViewState["userno"].ToString()));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_AdminMultipleLogin_btnVerifyOTP_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
            return;
        }
    }

    protected void btnAddReason_Click(object sender, EventArgs e)
    {
        Button btnAdd = sender as Button;
        int UA_NO = Convert.ToInt32(btnAdd.ToolTip);
        string Purpose = string.Empty;
        int purposeID = 0;

        foreach (ListViewItem item in lvlinks.Items)
        {
            Button btnAd = item.FindControl("btnAddReason") as Button;

            if (btnAd.CommandArgument == btnAdd.CommandArgument)
            {
                TextBox txtReason = item.FindControl("txtReson") as TextBox;
                LinkButton btnLogin = item.FindControl("btnLogin") as LinkButton;
                DropDownList ddlPurpose = item.FindControl("ddlPurpose") as DropDownList;
                if (!btnAd.Text.Contains("Back"))
                {
                    txtReason.Visible = true;
                    ddlPurpose.Visible = false;
                    btnAd.Text = "Back To Purpose Selection";
                }
                else
                {
                    btnAd.Text = "Add New Purpose";
                    txtReason.Visible = false;
                    ddlPurpose.Visible = true;
                }
                break;
            }
        }
    }

    protected void lvlinks_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlPurpose = e.Item.FindControl("ddlPurpose") as DropDownList;
            objCommon.FillDropDownList(ddlPurpose, "ACD_LOGIN_PURPOSE_MASTER", "DISTINCT(LOGIN_PUROSE_ID)", "PURPOSE", "LOGIN_PUROSE_ID >0", "LOGIN_PUROSE_ID DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_AdminMultipleLogin_lvlinks_ItemDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlPurpose_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListViewItem item in lvlinks.Items)
        {
            TextBox txtReason = item.FindControl("txtReson") as TextBox;
            DropDownList ddlPurpose = item.FindControl("ddlPurpose") as DropDownList;
            txtReason.Visible = (ddlPurpose.SelectedValue == "1") ? true : false;
        }
    }

    protected void btnResndOTP_Click(object sender, EventArgs e)
    {
        try
        {
            int ret1 = SendEmailSMS(Convert.ToInt32(ViewState["userno"]), ViewState["UA_NAME"].ToString(), ViewState["adminEmailID"].ToString(), ViewState["AdminMobileNo"].ToString(), Session["OTP"].ToString());

            if (ret1 == 1)
            {
                objCommon.DisplayMessage(updpnlUser, "OTP has been send on Your Email Id, Enter To Continue Login Process.", this.Page);
                lblTimer.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "myfunction();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Reg", "onTimer();", true);
            }
            else
            {
                objCommon.DisplayMessage(updpnlUser, "Failed to send email", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_AdminMultipleLogin_btnResndOTP_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        //upd_ModalPopupExtender1.Hide();
    }

    private void LoginAs(string userName, int userNo)
    {
        try
        {
            string ipAddress = string.Empty;
            User_AccController objUC = new User_AccController();
            string macAddress = string.Empty;
            int ATTEMPT = 0;
            int UANO = 0;
            string lastlogout = string.Empty;
            string emailid = string.Empty;
            string mobileNo = string.Empty;
            string ua_status = string.Empty;
            string password = string.Empty;

            if (ddlUserType.SelectedValue == "01")
            {
                emailid = objCommon.LookUp("ACD_USER_REGISTRATION WITH (NOLOCK)", "EMAILID", "USERNAME='" + userName + "' and USERNAME IS NOT NULL"); 
                mobileNo = objCommon.LookUp("ACD_USER_REGISTRATION WITH (NOLOCK)", "MOBILENO", "USERNAME='" + userName + "' and USERNAME IS NOT NULL"); 
                UANO = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION WITH (NOLOCK)", "ISNULL(USERNO,0)", "USERNAME='" + userName + "' and USERNAME IS NOT NULL")); 
                password = objCommon.LookUp("ACD_USER_REGISTRATION WITH (NOLOCK)", "USER_PASSWORD", "USERNAME='" + userName + "' and USERNAME IS NOT NULL");
            }
            else
            {
                emailid = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_EMAIL", "UA_NAME='" + userName + "' and UA_NAME IS NOT NULL");
                mobileNo = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_MOBILE", "UA_NAME='" + userName + "' and UA_NAME IS NOT NULL");
                ua_status = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_STATUS", "UA_NAME='" + userName + "' and UA_NAME IS NOT NULL");
                UANO = Convert.ToInt32(objCommon.LookUp("USER_ACC WITH (NOLOCK)", "ISNULL(UA_NO,0)", "UA_NAME='" + userName + "' and UA_NAME IS NOT NULL")); 
            }
            
           
            ATTEMPT = Convert.ToInt32(objCommon.LookUp("reff with (nolock)", "ATTEMPT", ""));

            if (UANO != 0 && ddlUserType.SelectedValue == "01")
            {
                //Code for LogTable
                //=================
                int retLogIDD = LogTableController.AddtoLog(Session["username"].ToString(), Session["ipAddress"].ToString(), Session["macAddress"].ToString(), DateTime.Now);
                Session["logid"] = retLogIDD + 1;

                string IMAGEE = string.Empty;

                LogFile objLFF = new LogFile();
                objLFF.Ua_Name = Session["username"].ToString();
                objLFF.LoginTime = DateTime.Now;
                macAddress = GetMACAddress();
                Session["macAddress"] = macAddress;
                Session["MACADDR"] = macAddress;
                int aa = objUC.AddtoLogTranUserReg(Session["username"].ToString(), ipAddress, Session["macAddress"].ToString(), Convert.ToDateTime(DateTime.Now));
                Session["loginid"] = aa.ToString();

                //code for cookies
               DefaultEntity.Login objregist = new DefaultEntity.Login();
               DataSet ds = null;
               string status = string.Empty;
               string PassWord = string.Empty; int UA_Type = 0;

               objregist.UserNameEmail = userName;
               objregist.PassWord = password;
               objregist.UA_Type = UA_Type;
               objregist.UserNo = UANO;

               int retuserno = objUC.GetUserPasswordSignIn(objregist);
               if (retuserno > 0)
               {
                   objregist.UserNo = retuserno;
                   ds = objUC.GetSingleRecordByUANoForRegistration(objregist);
                  
                   if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                   {
                       HttpContext.Current.Session["USERNO"] = ds.Tables[0].Rows[0]["USERNO"].ToString();
                       HttpContext.Current.Session["FIRSTNAME"] = ds.Tables[0].Rows[0]["FIRSTNAME"].ToString();
                       HttpContext.Current.Session["LASTNAME"] = ds.Tables[0].Rows[0]["LASTNAME"].ToString();
                       HttpContext.Current.Session["USERNAME"] = ds.Tables[0].Rows[0]["USERNAME"].ToString();
                       HttpContext.Current.Session["UATYPE"] = 2;
                       HttpContext.Current.Session["ADMSITEURL"] = ds.Tables[0].Rows[0]["ADM_SITE_URL"].ToString();
                       HttpContext.Current.Session["FRESHMAN_TRANSFEREE"] = ds.Tables[0].Rows[0]["FRESHMAN_TRANSFEREE"].ToString();

                       if (Convert.ToInt32(objregist.UA_NO) == 0)
                       {
                           HttpContext.Current.Session["UANO"] = ds.Tables[0].Rows[0]["USERNO"].ToString();
                       }
                       else
                       {
                           HttpContext.Current.Session["UANO"] = objregist.UA_NO;
                       }

                       status = HttpContext.Current.Session["FRESHMAN_TRANSFEREE"].ToString();
                   }
               }

               //string Url = "http://localhost:56315/PresentationLayer/Admission/dashboard.aspx";
               string Url = Session["ADMSITEURL"].ToString() + "Admission/dashboard.aspx";

               GenerateOauthToken(objregist.UserNameEmail, Url);

               Response.Redirect(Session["ADMSITEURL"].ToString() + "Admission/dashboard.aspx", false);

               //Response.Redirect("http://localhost:56315/PresentationLayer/Admission/dashboard.aspx", false);
                 
                //Response.Redirect("https://usadevadmissions.mastersofterp.in/Admission/dashboard.aspx", false);

            }

            else if (UANO != 0)
            {
                #region 90-Days

                DateTime ChangePassDate = Convert.ToDateTime(objCommon.LookUp("USER_ACC WITH (NOLOCK)", "isnull(CHANGEPASSDATE,0)", "ua_name=" + "'" + userName + "'"));
                int ua_type = Convert.ToInt32(ddlUserType.SelectedValue);
                DateTime TodayDate = DateTime.Now;
                int Difference = (TodayDate - ChangePassDate).Days;
                int MAILINDAYS = Convert.ToInt32(objCommon.LookUp("User_Rights WITH (NOLOCK)", "isnull(MAILINDAYS,0)", "USERTYPEID=" + ua_type));
                int FIRSTLOGDAYS = Convert.ToInt32(objCommon.LookUp("User_Rights with (nolock)", "isnull(FIRSTLOGDAYS,0)", "USERTYPEID=" + ua_type));
                string UA_FIRSTLOG = (objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_FIRSTLOG", "ua_name=" + "'" + userName + "'"));

                #endregion 90-Days

                if (userNo > 0)
                {
                    UserAcc objUA = objUC.GetSingleRecordByUANo(userNo);

                    #region erp
                    if (objUA.UA_No != 0)
                    {
                        string UA_NO = Session["userno"].ToString();
                        Session["mainUserNo"] = UA_NO;
                        Session["OrgId"] = 0;
                        Session["userno"] = objUA.UA_No.ToString();
                        Session["idno"] = objUA.UA_IDNo.ToString();
                        Session["username"] = objUA.UA_Name;
                        Session["usertype"] = objUA.UA_Type;
                        Session["userfullname"] = objUA.UA_FullName;
                        Session["dec"] = objUA.UA_Dec.ToString();
                        Session["userdeptno"] = objUA.UA_DeptNo.ToString();
                        Session["colcode"] = objCommon.LookUp("reff", "college_code", string.Empty);
                        Session["firstlog"] = objUA.UA_FirstLogin;
                        Session["ua_status"] = objUA.UA_Status;
                        Session["ua_section"] = objUA.UA_section.ToString();
                        Session["UA_DESIG"] = objUA.UA_Desig.ToString();
                        Session["userEmpDeptno"] = objUA.UA_EmpDeptNo.ToString();
                        ipAddress = Request.ServerVariables["REMOTE_HOST"];
                        Session["ipAddress"] = ipAddress;
                        macAddress = GetMACAddress();
                        Session["macAddress"] = macAddress;
                        Session["payment"] = "default";
                        if (Convert.ToString(Session["firstlog"]) == "False")
                            Response.Redirect("~/changePassword.aspx?IsReset=1");

                        ATTEMPT = Convert.ToInt32(objCommon.LookUp("reff with (nolock)", "ATTEMPT", ""));
                        // GenerateOauthToken(objUA.UA_Name, OBETokenUrl);
                        string Lms_Url = objCommon.LookUp("reff with (nolock)", "Lms_Url", "").ToString();

                        GenerateOauthToken(objUA.UA_Name, Lms_Url);

                        if (ua_status == "1")
                        {
                            string subject = "ERP Login Credentials";
                            string message = "Due to the unsucessfully  " + ATTEMPT + " login attempt ,your ERP account is blocked. Please contact system administrator!";
                            if (emailid != "")
                            {
                                objCommon.sendEmail(message, emailid, subject);
                            }
                            objCommon.DisplayMessage(updpnlUser, "This Account is Blocked.", this.Page);
                            return;
                        }

                        string lastloginid = objCommon.LookUp("LOGFILE WITH (NOLOCK)", "MAX(ID)", "UA_NAME='" + Session["username"].ToString() + "' and UA_NAME IS NOT NULL");
                        //FOR STORE MODULE
                        Session["lastloginid"] = lastloginid.ToString();
                        if (Session["lastloginid"].ToString() != string.Empty)
                        {
                            lastlogout = objCommon.LookUp("LOGFILE WITH (NOLOCK)", "LOGOUTTIME", "ID=" + Convert.ToInt32(Session["lastloginid"].ToString()));
                        }
                        string Allowpopup = objCommon.LookUp("reff WITH (NOLOCK)", "ALLOWLOGOUTPOPUP", "");
                        Session["currentsession"] = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER WITH (NOLOCK)", "count(*)", "FLOCK=1")) == 0 ? "0" : objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "FLOCK=1");
                        Session["sessionname"] = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER WITH (NOLOCK)", "count(*)", "FLOCK=1")) == 0 ? "" : objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "FLOCK=1");
                        //Session["hostel_session"] = objCommon.LookUp("ACD_HOSTEL_SESSION WITH (NOLOCK)", "MAX(HOSTEL_SESSION_NO)", "FLOCK=1");
                        Session["FeesSessionStartDate"] = "2014";
                        Session["FeesSessionEndDate"] = "2015";

                        ipAddress = Request.ServerVariables["REMOTE_HOST"];
                        Session["ipAddress"] = ipAddress;
                        Session["IPADDR"] = ipAddress;
                        Session["WorkingDate"] = DateTime.Now.ToString();
                        Session["college_nos"] = objUA.COLLEGE_CODE;
                        macAddress = GetMACAddress();
                        Session["macAddress"] = macAddress;
                        Session["MACADDR"] = macAddress;
                        Session["Session"] = Session["sessionname"].ToString();

                        if (Session["usertype"].ToString() == "2")
                        {
                            int degreeNo = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"])));
                            int activeStatus = objUC.ValidateActiveStatusOfStudent(Convert.ToInt32(Session["currentsession"]), degreeNo, Convert.ToInt32(Session["idno"]));
                            if (activeStatus == 1)
                            {
                                objCommon.DisplayMessage("This Account is Deactivated.", this.Page);
                                return;
                            }
                        }

                        //Code for LogTable
                        //=================
                        int retLogID = LogTableController.AddtoLog(Session["username"].ToString(), Session["ipAddress"].ToString(), Session["macAddress"].ToString(), DateTime.Now);
                        Session["logid"] = retLogID + 1;

                        string IMAGE = string.Empty;

                        LogFile objLF = new LogFile();
                        objLF.Ua_Name = Session["username"].ToString();
                        objLF.LoginTime = DateTime.Now;
                        macAddress = GetMACAddress();
                        Session["macAddress"] = macAddress;
                        Session["MACADDR"] = macAddress;
                        int a = objUC.AddtoLogTran(Session["username"].ToString(), ipAddress, Session["macAddress"].ToString(), Convert.ToDateTime(DateTime.Now));
                        Session["loginid"] = a.ToString();

                        if (Convert.ToString(Session["firstlog"]) == "False")
                            Response.Redirect("~/changePassword.aspx?IsReset=1");
                        else
                        {
                            //if (Session["lastloginid"].ToString() != "")
                            //{
                            if (lastlogout == "" && Allowpopup == "1")
                                Response.Redirect("~/SignoutHold.aspx", false);
                            else
                            {
                                if (Session["username"].ToString() == "superadmin")
                                {
                                    Response.Redirect("~/RFC_CONFIG/home.aspx", false);
                                }
                                else if (Session["usertype"].ToString() == "1")
                                {
                                    Response.Redirect("~/principalHome.aspx", false);
                                }
                                else if (Session["usertype"].ToString() == "2" || Session["usertype"].ToString() == "14")
                                {
                                    Response.Redirect("~/studeHome.aspx", false);
                                }
                                else if (Session["usertype"].ToString() == "3")
                                {
                                    Response.Redirect("~/principalHome.aspx", false);
                                }
                                else if (Session["usertype"].ToString() == "5")
                                {
                                    Response.Redirect("~/principalHome.aspx", false);
                                }
                                else
                                {
                                    Response.Redirect("~/principalHome.aspx", false);
                                }
                            }
                            //}
                            //else
                            //{
                            //}
                        }
                    }

                    #endregion erp

                }
            }

        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage("Login Failed !, Please Check Your Username Or Password !", this.Page);
        }
    }

    private int AuthorisedUser()
    {
        int ret = 0;
        string AuthorisedUser = objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(AUTHORISED_USERS_FOR_GO_TO_USERLOGIN,'')", "");
        if (!string.IsNullOrEmpty(AuthorisedUser))
        {
            string[] arrstr = AuthorisedUser.Split(',');
            foreach (string i in arrstr)
            {
                if (i == Session["userno"].ToString())
                {
                    ret = 1;
                    break;
                }
            }
        }
        return ret;
    }

    private int checkOTPSecurity(int AdminUserNo, string OTP)
    {
        int success = 0;
        DataSet CheckAttempt = objCommon.FillDropDown("ACD_ADMP_USER_OTP", "TOP 1 ISNULL(OTPATTEMPT,0)AS TOTALATTEMPT", "", "USERNO='" + AdminUserNo + "'" + "AND   CAST(OTPDATE AS DATE)=CAST(CURRENT_TIMESTAMP AS DATE) ", "OTPID desc");
        int cnt = 0;
        if (CheckAttempt.Tables[0].Rows.Count > 0)
            cnt = Convert.ToInt32((CheckAttempt.Tables[0].Rows[0]["TotalAttempt"].ToString()));

        string TimeDiff = "";
        TimeDiff = objCommon.LookUp("ACD_ADMP_USER_OTP", "TOP 1 DATEDIFF(MINUTE, OTPTIME , CAST(CURRENT_TIMESTAMP AS TIME)) AS MINUTEDIFF", "USERNO=1 AND IsAttempt=1 and IsSuccess=0 AND  CAST(OTPDATE AS DATE)=CAST(CURRENT_TIMESTAMP AS DATE) order by OTPTime desc");
        if (TimeDiff == "")
            TimeDiff = "0";

        DataSet TDeley = objCommon.FillDropDown("[dbo].[Reff]", "OTPDeleyMin", "OTPAttempt", "", "");
        int Attempcnt = Convert.ToInt32((TDeley.Tables[0].Rows[0]["OTPAttempt"].ToString()));
        int DelayTim = Convert.ToInt32((TDeley.Tables[0].Rows[0]["OTPDeleyMin"].ToString()));
        int Tdd = DelayTim - Convert.ToInt32(TimeDiff);
        if (cnt >= Attempcnt && Convert.ToInt32(TimeDiff) < DelayTim)
        {
            objCommon.DisplayMessage(this.Page, "OTP Session is Expire.\\n You can generate OTP only after " + Tdd + " " + "Minutes", this.Page);
            return success;
        }
        else
        {
            User_AccController objUC = new User_AccController();
            CustomStatus cs = (CustomStatus)objUC.InsertOTP(OTP, AdminUserNo, 0, false, "InsertOTP", 1);
            success = (cs.Equals(CustomStatus.RecordSaved)) ? 1 : 0;
        }
        return success;
    }

    public int TransferToEmailAmazon(string useremail, string message, string subject)
    {
        int ret = 0;
        try
        {
            var smtpClient = new System.Net.Mail.SmtpClient("email-smtp.ap-south-1.amazonaws.com", 587)
            {
                Credentials = new NetworkCredential("AKIAUVZ5FSTMFA3CG74W", "BLYE5zzrcQkbKZEqICN3S+lhS3EdwBLl9Sl8n3EUbHEU"),
                EnableSsl = true
            };

            var messageNew = new MailMessage
            {
                From = new System.Net.Mail.MailAddress("no-reply@iitms.co.in"),
                Subject = subject,//"Test Email",
                Body = message,//"This is the body of the email."
                IsBodyHtml = true
            };

            //messageNew.To.Add("yograj.chaple@mastersofterp.co.in");
            messageNew.To.Add(useremail);
            smtpClient.Send(messageNew);
            return ret = (System.Net.Mail.DeliveryNotificationOptions.OnSuccess == System.Net.Mail.DeliveryNotificationOptions.OnSuccess) ? 1 : 0;
        }
        catch (Exception ex)
        {
            ret = 0;
        }

        return ret;
    }

    private void GenerateOauthToken(string ua_name, string DomainURL)
    {

        var data = webPostMethod(ua_name);

        if (data.Contains("access_token"))
        {
            string x = data.Split('{')[1];
            string y = x.Split('}')[0];
            string z = "{" + y + "}";
            dynamic parsed_data = JObject.Parse(z);
            setCookies(Convert.ToString(parsed_data.access_token), ua_name);
        }
    }
    public string webPostMethod(string ua_name)
    {
        string responseFromServer = "";
        responseFromServer = GenerateToken(ua_name, 60);
        return responseFromServer;
    }


    void setCookies(string acc_tok, string ua_name)
    {

        string Url = System.Configuration.ConfigurationManager.AppSettings["DomainName"].ToString();
        int TimeOut = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DomainTimeOut"].ToString());

        HttpCookie ToA = new HttpCookie("ToA");
        ToA.Value = acc_tok;
        ToA.Domain = Url;
        ToA.Expires = DateTime.Now.AddMinutes(TimeOut);
        Response.Cookies.Add(ToA);

        HttpCookie UA = new HttpCookie("UA");
        UA.Value = ua_name;
        UA.Domain = Url;
        UA.Expires = DateTime.Now.AddMinutes(TimeOut);
        Response.Cookies.Add(UA);

        HttpCookie CollegeId = new HttpCookie("CollegeId");
        CollegeId.Value = "1";
        CollegeId.Domain = Url;
        CollegeId.Expires = DateTime.Now.AddMinutes(TimeOut);
        Response.Cookies.Add(CollegeId);

    }
    DynamicControllerAL AL = new DynamicControllerAL();
    public string GenerateToken(string UserName, int TimeLimit)
    {
        Packet1 pkg;
        string NewUserNAme = UserName.Substring(0, Math.Min(UserName.Length, 20));
        string encryPass = mastersofterp_MAKAUAT.clsTripleLvlEncyrpt.ThreeLevelEncrypt(NewUserNAme);
        DateTime created_dnt = DateTime.Now;
        DateTime expiry_dnt = created_dnt.AddMinutes(TimeLimit);
        if (encryPass != "-99")
        {
            pkg = new Packet1
            {
                res = "1",
                msg = "Success",
                access_token = encryPass,
                created_dnt = created_dnt.ToString("yyyy-MM-dd HH:mm:ss"),
                expiry_dnt = expiry_dnt.ToString("yyyy-MM-dd HH:mm:ss"),
                time_limit = TimeLimit.ToString()
            };
        }
        else
        {
            pkg = new Packet1
            {
                res = "0",
                msg = "Error",
                access_token = encryPass
            };
        }

        string ConStr = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        string retVal = "";
        string SP = "PKG_CRUD_TOKEN_MANAGEMENT_NEW_REG";
        string PM = "@P_UA_NAME, @P_ACC_TOKEN, @P_CREATED_DNT, @P_EXPIRY_DNT, @P_TIME_LIMIT, @P_DEVICE_TYPE, @P_OPERATION";
        string VL = "" + UserName + "," + encryPass + "," + created_dnt.ToString("yyyy-MM-dd HH:mm:ss") + "," + expiry_dnt.ToString("yyyy-MM-dd HH:mm:ss") + "," + TimeLimit.ToString() + ",PC,1";
        retVal = AL.DynamicSPCall_IUD(SP, PM, VL, true, 2);

        return JsonConvert.SerializeObject(pkg, Newtonsoft.Json.Formatting.Indented);

    }
    public class Packet1
    {
        public string res { get; set; }
        public string msg { get; set; }
        public string access_token { get; set; }
        public string created_dnt { get; set; }
        public string expiry_dnt { get; set; }
        public string time_limit { get; set; }
    }
}