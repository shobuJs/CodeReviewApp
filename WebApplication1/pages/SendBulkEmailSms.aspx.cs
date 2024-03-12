using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using IITMS.SQLServer.SQLDAL;
using System.Net;
using System.Web.Configuration;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Security;
using mastersofterp;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using CrystalDecisions.CrystalReports.Engine;



public partial class ACADEMIC_SendBulkEmailSms : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ImportDataController IDC = new ImportDataController();

    //CONNECTIONSTRING
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            //Page Authorization
            //CheckPageAuthorization();

            //Set the Page Title
            Page.Title = Session["coll_name"].ToString();

            //Load Page Help
            if (Request.QueryString["pageno"] != null)
            {
                //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
            }

            PopulateDropDown();
        }

        divMsg.InnerHtml = string.Empty;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkStudentLogin.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkStudentLogin.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNAME DESC");
            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_BulkStudentLogin.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {

        string branch = GetBranch();
        branch = branch.Replace('$', ',');

        ViewState["DegreeNo"] = branch;
        string[] Branchno = branch.Split(',');
        try
        {
            // DataSet ds = objCommon.FillDropDown("ACD_ONLINE_USER_UPLOAD U LEFT JOIN USER_ACC US ON (US.UA_NAME=U.USERNAME)", "CASE WHEN U.USERNAME IS NULL THEN '-' ELSE U.USERNAME END REGNO, 'HSNCU@Stud_2020' as DOBNEW, US.UA_MOBILE, US.UA_EMAIL, U.STUDNAME", "ISNULL(US.UA_NO,0) AS CREATED", "U.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND U.ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + "AND U.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "REGNO");
            // DataSet ds = objCommon.FillDropDown("USER_ACC US LEFT JOIN ACD_USER_REGISTRATION UR ON (US.UA_NAME=UR.USERNAME) CROSS APPLY(SELECT TOP 1 DEGREENO, COLLEGE_ID FROM ACD_USER_BRANCH_PREF BP WHERE UR.USERNO=BP.USERNO) BP", "DISTINCT US.UA_NAME AS REGNO, US.UA_MOBILE", "US.UA_EMAIL, US.UA_FULLNAME AS STUDNAME", "UR.ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + " AND US.UA_TYPE=2 AND BP.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BP.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "REGNO");

            //DataSet ds = objCommon.FillDropDown("USER_ACC US LEFT JOIN ACD_USER_REGISTRATION UR ON (US.UA_NAME=UR.USERNAME) CROSS APPLY(SELECT TOP 1 DEGREENO, COLLEGE_ID FROM ACD_USER_BRANCH_PREF BP WHERE UR.USERNO=BP.USERNO) BP  INNER JOIN ACD_STUDENT ST ON (ST.IDNO=US.UA_IDNO AND ST.USERNO=UR.USERNAME)", "DISTINCT US.UA_NAME AS REGNO, US.UA_MOBILE", "US.UA_EMAIL, US.UA_FULLNAME AS STUDNAME", "UR.ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + " AND US.UA_TYPE=2 AND ST.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND  ISNULL(ST.ADMCAN,0)=0   AND ISNULL(ST.CAN,0)=0  AND ST.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "REGNO");

            //Added for According to branch finding Data on dated 09/10/2020 by Swapnil Thakare

            //  DataSet ds = objCommon.FillDropDown("USER_ACC US LEFT JOIN ACD_USER_REGISTRATION UR ON (US.UA_NAME=UR.USERNAME) CROSS APPLY(SELECT TOP 1 DEGREENO, COLLEGE_ID FROM ACD_USER_BRANCH_PREF BP WHERE UR.USERNO=BP.USERNO) BP  INNER JOIN ACD_STUDENT ST ON (ST.IDNO=US.UA_IDNO AND ST.USERNO=UR.USERNAME)", "DISTINCT US.UA_NAME AS REGNO, US.UA_MOBILE", "US.UA_EMAIL, US.UA_FULLNAME AS STUDNAME", "UR.ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + " AND US.UA_TYPE=2 AND ST.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND  ISNULL(ST.ADMCAN,0)=0   AND ISNULL(ST.CAN,0)=0 AND ST.BRANCHNO IN (" + branch + ") AND ST.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "REGNO");
            DataSet ds = objCommon.FillDropDown("USER_ACC US LEFT JOIN ACD_USER_REGISTRATION UR ON (US.UA_NAME=UR.USERNAME) CROSS APPLY(SELECT DEGREENO, COLLEGE_ID FROM ACD_USER_BRANCH_PREF BP WHERE UR.USERNO=BP.USERNO) BP  INNER JOIN ACD_STUDENT ST ON (ST.REGNO=US.UA_NAME AND ST.USERNO=UR.USERNAME)", "DISTINCT US.UA_NAME AS REGNO, US.UA_MOBILE", "US.UA_EMAIL, US.UA_FULLNAME AS STUDNAME", "UR.ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + " AND US.UA_TYPE=2 AND ST.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND  ISNULL(ST.ADMCAN,0)=0   AND ISNULL(ST.CAN,0)=0 AND ST.BRANCHNO IN (" + branch + ") AND ST.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "REGNO");


            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds.Tables[0];
                lvStudents.DataBind();
                lvStudents.Visible = true;
                btnSendSMS.Enabled = true;
            }
            else
            {
                objCommon.DisplayUserMessage(upduser, "No Record Found!", Page);
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                lvStudents.Visible = false;
                btnSendSMS.Enabled = false;
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Items.Clear();
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlColg.SelectedValue + "", "DEGREENO");
    }

    protected void btnSendSMS_Click(object sender, EventArgs e)
    {

        string folderPath = Server.MapPath("~/TempDocument/");
        //string folderPath = @"E:\Images\"; // Your path Where you want to save other than Server.MapPath
        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        //Save the File to the Directory (Folder).
        Session["FileName"] = fuAttachment.FileName;
        if (Session["FileName"] != string.Empty || Session["FileName"] != "")  // ADDED FOR CHECKING IF FILE EXISTS OR NOT ON 09-10-2020 BY SWAPNIL T
        {
            fuAttachment.SaveAs(folderPath + Path.GetFileName(fuAttachment.FileName));
        }

        Session["result"] = "0";
        int i = 0;
        int msgtype = 0;

        if (rbEmail.Checked == true || rbBoth.Checked == true)
        {
            if (txtSubject.Text == string.Empty)
            {
                objCommon.DisplayMessage(upduser, "Please Enter the Subject!", this.Page);
                return;
            }
            else if (txtMatter.Text == string.Empty)
            {
                objCommon.DisplayMessage(upduser, "Please Enter the Message!", this.Page);
                return;
            }
        }
        else if (rbSMS.Checked == true)
        {
            if (txtMatter.Text == string.Empty)
            {
                objCommon.DisplayMessage(upduser, "Please Enter the Message!", this.Page);
                return;
            }
        }

        foreach (ListViewDataItem item in lvStudents.Items)
        {
            CheckBox chk = item.FindControl("chkRow") as CheckBox;

            if (chk.Checked == false)
            {
                i++;
            }
        }

        if (i == lvStudents.Items.Count)
        {
            objCommon.DisplayMessage(upduser, "Please Select Students from the Student List!", this.Page);
            return;
        }

        foreach (ListViewDataItem item in lvStudents.Items)
        {
            CheckBox chk = item.FindControl("chkRow") as CheckBox;
            Label lblreg = item.FindControl("lblreg") as Label;
            Label lblStudName = item.FindControl("lblstud") as Label;
            Label lblEmailId = item.FindControl("lblEmailId") as Label;
            Label lblMobileNo = item.FindControl("lblMobileNo") as Label;

            if (chk.Checked == true)
            {
                string useremail = lblEmailId.Text == string.Empty ? objCommon.LookUp("acd_student a inner join user_acc b on (a.idno=b.UA_IDNO)", "b.UA_EMAIL", "UA_NAME='" + lblreg.Text.Replace("'", "`").Trim() + "' and UA_NAME IS NOT NULL") : lblEmailId.Text;
                string message = txtMatter.Text;
                string subject = txtSubject.Text;

                if (rbBoth.Checked == true)
                {
                    msgtype = 3;

                    // sendmail(useremail, subject, message);
                    //sendEmail(message, useremail, subject);
                    string filename = string.Empty;
                    if (Session["FileName"] != string.Empty || Session["FileName"] != "")
                    {
                        filename = Convert.ToString(Session["FileName"]);
                    }
                    Execute(message, useremail, subject, filename).Wait();

                    string Mobileno = lblMobileNo.Text == string.Empty ? objCommon.LookUp("acd_student a inner join user_acc b on (a.idno=b.UA_IDNO)", "b.UA_MOBILE", "UA_NAME='" + lblreg.Text.Replace("'", "`").Trim() + "' and UA_NAME IS NOT NULL") : lblMobileNo.Text;
                    if (Mobileno != "")
                    {
                        SendSMS(Mobileno, txtMatter.Text);
                    }
                }
                else if (rbEmail.Checked == true)
                {
                    msgtype = 1;
                    string filename = string.Empty;
                    if (Session["FileName"] != string.Empty || Session["FileName"] != "")
                    {
                        filename = Convert.ToString(Session["FileName"]);
                    }
                    // sendmail(useremail, subject, message);
                    //temp  sendEmail(message, useremail, subject);  
                    Execute(message, useremail, subject, filename).Wait();
                    Session["result"] = 1;
                }
                else if (rbSMS.Checked == true)
                {
                    msgtype = 2;

                    string Mobileno = lblMobileNo.Text == string.Empty ? objCommon.LookUp("acd_student a inner join user_acc b on (a.idno=b.UA_IDNO)", "b.UA_MOBILE", "UA_NAME='" + lblreg.Text.Replace("'", "`").Trim() + "' and UA_NAME IS NOT NULL") : lblMobileNo.Text;
                    if (Mobileno != "")
                    {
                        SendSMS(Mobileno, txtMatter.Text);
                    }
                }

                int status = IDC.InsertBulkMessageLog(lblreg.Text, msgtype, txtSubject.Text, txtMatter.Text, Convert.ToInt32(Session["userno"].ToString()), Session["ipAddress"].ToString(), lblEmailId.Text, lblMobileNo.Text);

                if (status != 1)
                {
                    objCommon.DisplayMessage(upduser, "Something went wrong!", this.Page);
                }
            }
        }

        //  File.Delete(Server.MapPath(Server.MapPath("~/TempDocument/") + "\\" + Session["FileName"].ToString())); //Delete Sending file after send done.

        if (Session["result"].ToString() == "1")
        {
            objCommon.DisplayMessage(upduser, "Message / Mail Sent Successfully!!", this.Page);
        }
        else
        {
            objCommon.DisplayMessage(upduser, "Sorry, Your Application not configured with mail server, Please contact Admin Department !!", this.Page);
        }
    }

    private void SendSMS(string Mobile, string text)
    {
        string status = "";
        try
        {
            string Message = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?"));
                request.ContentType = "text/xml; charset=utf-8";
                request.Method = "POST";

                string postDate = "ID=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                postDate += "&";
                postDate += "Pwd=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                postDate += "&";
                postDate += "PhNo=91" + Mobile;
                postDate += "&";
                postDate += "Text=" + text;

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
                request.ContentType = "application/x-www-form-urlencoded";

                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse _webresponse = request.GetResponse();
                dataStream = _webresponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                status = reader.ReadToEnd();
                Session["result"] = 1;
            }
            else
            {
                status = "0";
            }
        }
        catch
        {

        }
    }

    #region Backup 22092020 SMTP.GMAIL
    //public void sendmail(string toEmailId, string Sub, string body)
    //{
    //    try
    //    {
    //        DataSet dsconfig = null;
    //        MailMessage mail = new MailMessage();
    //       // string message = string.Empty;            
    //       // var message = (dynamic)null;
    //        dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
    //        var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString(), "");
    //        var toAddress = new MailAddress(toEmailId, "");
    //        // string fromPassword = clsTripleLvlEncyrpt.ThreeLevelDecrypt(Session["EMAILSVCPWD"].ToString());
    //        string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
    //        var smtp = new SmtpClient
    //        {
    //            Host = "smtp.gmail.com",
    //            Port = 587,
    //            EnableSsl = true,
    //            DeliveryMethod = SmtpDeliveryMethod.Network,
    //            UseDefaultCredentials = false,
    //            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
    //        };
    //        using (var message = new MailMessage(fromAddress, toAddress)
    //        {
    //            Subject = Sub,
    //            Body = body,
    //            BodyEncoding = System.Text.Encoding.UTF8,
    //            SubjectEncoding = System.Text.Encoding.Default,
    //            IsBodyHtml = true

    //        })
    //            if (fuAttachment.HasFile)
    //            {
    //                mail.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, fuAttachment.FileName));
    //            }
    //        mail.IsBodyHtml = true;
    //      //  mail.Body = message;
    //        {
    //            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
    //            smtp.Send(mail);

    //            if (DeliveryNotificationOptions.OnSuccess == DeliveryNotificationOptions.OnSuccess)
    //            {
    //                Session["result"] = "1";
    //                //Storing the details of sent email
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message);
    //    }
    //}
    #endregion


    public void sendEmail(string Message, string toEmailId, string sub)
    {
        try
        {
            //FileStream fStream;
            //DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/TempDocument/"));
            MailMessage mail = new MailMessage();
            //if (Session["FileName"] != string.Empty || Session["FileName"] != "")
            //{
            //   Attachment attachFile = new Attachment(Server.MapPath("~/TempDocument/") + "\\" + Session["FileName"].ToString());
            //   mail.Attachments.Add(attachFile);
            //}           

            DataSet dsconfig = null;
            // string message = string.Empty;            
            var message = (dynamic)null;
            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,MASTERSOFT_GRID_MAILID,MASTERSOFT_GRID_PASSWORD,MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString(), "HSNCU");
            var toAddress = new MailAddress(toEmailId, "");
            // string fromPassword = clsTripleLvlEncyrpt.ThreeLevelDecrypt(Session["EMAILSVCPWD"].ToString());
            string fromPassword = dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_PASSWORD"].ToString();
            string SendgridUserId = dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_USERNAME"].ToString();


            //added by pankaj nakhale 23102020 bcz of credential changed
            //var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "HSNCU");
            //var toAddress = new MailAddress(toEmailId, "");
            //string fromPassword = dsconfig.Tables[0].Rows[0]["SENDGRID_PWD"].ToString();
            //string userId = dsconfig.Tables[0].Rows[0]["SENDGRID_USERNAME"].ToString();      


            var smtp = new SmtpClient
            {
                //Host = "smtp.gmail.com",
                Host = "smtp.sendgrid.net",
                Port = 587,

                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 10000,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(SendgridUserId, fromPassword)
                //Credentials = new NetworkCredential(userId, fromPassword)
            };
            using (message = new MailMessage(fromAddress, toAddress)
            {
                Subject = sub,
                Body = Message,
                BodyEncoding = System.Text.Encoding.UTF8,
                SubjectEncoding = System.Text.Encoding.Default,
                IsBodyHtml = true
            })
                //if (fuAttachment.HasFile)
                //{                   
                //    for (int i = 0; i < Request.Files.Count; i++)
                //    {
                //     HttpPostedFile fu = Request.Files[i];
                //     mail.Attachments.Add(new Attachment(Path.GetFileName("~/TempDocument/" + Session["FileName"].ToString()))); //fuAttachment.PostedFile.InputStream, fu.FileName
                //    }
                //}

                // Attachment attachFile = new Attachment(Server.MapPath("~/TempDocument/") + "\\" + Session["FileName"].ToString());



                //mail.Attachments.Add(new Attachment(Path.GetFileName( + )));
                message.IsBodyHtml = true;
            mail.Body = Message;
            mail.From = fromAddress;
            mail.Subject = sub;
            mail.To.Clear();
            mail.To.Add(toAddress);
            {
                //ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(mail);
                Session["result"] = 1;
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }
    }

    protected void rbEmail_CheckedChanged(object sender, EventArgs e)
    {
        if (rbEmail.Checked == true)
        {
            btnSendSMS.Text = "Send Email";
            divSubject.Visible = true;
        }
        else
        {
            btnSendSMS.Text = "Send Email/SMS";
            divSubject.Visible = false;
        }
    }

    protected void rbSMS_CheckedChanged(object sender, EventArgs e)
    {
        if (rbSMS.Checked == true)
        {
            btnSendSMS.Text = "Send SMS";
            divSubject.Visible = false;
        }
        else
        {
            btnSendSMS.Text = "Send Email/SMS";
            divSubject.Visible = false;
        }
    }

    protected void rbBoth_CheckedChanged(object sender, EventArgs e)
    {
        if (rbBoth.Checked == true)
        {
            btnSendSMS.Text = "Send Email & SMS";
            divSubject.Visible = true;
        }
        else
        {
            btnSendSMS.Text = "Send Email/SMS";
            divSubject.Visible = false;
        }
    }


    private void test()
    {
    }

    private string GetBranch()
    {
        string branchNo = "";
        string branchno = string.Empty;
        int X = 0;
        // pnlStudent.Update();
        foreach (ListItem item in ddlBranch.Items)
        {
            if (item.Selected == true)
            {
                branchNo += item.Value + '$';
                X = 1;
            }
        }

        if (X == 0)
        {
            branchNo = "0";
        }

        if (branchNo != "0")
        {
            branchno = branchNo.Substring(0, branchNo.Length - 1);
        }
        else
        {
            branchno = branchNo;
        }
        if (branchno != "")
        {
            string[] bValue = branchno.Split('$');
        }
        // degreeno = degreeno.Substring(0, degreeno.Length - 1);
        //}
        return branchno;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlBranch.Items.Clear();
            if (ddlDegree.SelectedIndex > 0)
            {
                // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B , ACD_COLLEGE_DEGREE_BRANCH AD", "B.BRANCHNO", "B.SHORTNAME", "B.BRANCHNO = AD.BRANCHNO AND AD.COLLEGE_ID=" + ddlColg.SelectedValue + " AND AD.DEGREENO = " + ddlDegree.SelectedValue + " ", "BRANCHNO");
                DataSet ds = objCommon.FillDropDown("ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH AD ON ( B.BRANCHNO = AD.BRANCHNO )", "DISTINCT(B.BRANCHNO)", "B.LONGNAME", "B.BRANCHNO > 0 AND AD.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ddlBranch.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
                    // DropDownCheckBoxesBranch.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
                }
            }
            else
            {
                ShowMessage("Please select college/school");
                ddlDegree.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ddlClg_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudents.Visible = false;
    }

    static async Task Execute(string Message, string toEmailId, string subjects, string filename)
    {
        try
        {
            Common objCommon = new Common();


            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,MASTERSOFT_GRID_MAILID,MASTERSOFT_GRID_PASSWORD,MASTERSOFT_GRID_USERNAME,API_KEY_SENDGRID,CLIENT_API_KEY,FCGRIDEMAILID", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["FCGRIDEMAILID"].ToString(), "HSNCU");
            var toAddress = new MailAddress(toEmailId, "");
            var apiKey = dsconfig.Tables[0].Rows[0]["CLIENT_API_KEY"].ToString();  //"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";
            var client = new SendGridClient(apiKey.ToString());
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["FCGRIDEMAILID"].ToString(), "HSNCU");
            var subject = subjects;// "Your OTP for Certificate Registration.";
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;

            string AttcPath = System.Web.HttpContext.Current.Server.MapPath("~/TempDocument/" + filename + "");
            var bytes = File.ReadAllBytes(AttcPath);
            var file = Convert.ToBase64String(bytes);
            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            var msgs = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            msgs.AddAttachment("" + filename + "", file);
            var response = await client.SendEmailAsync(msgs);
        }
        catch (Exception ex)
        {

        }
    }
    //static private MemoryStream ShowGeneralExportReportForMail(string path)
    //{
    //    MemoryStream oStream;
    //    ReportDocument customReport;
    //    customReport = new ReportDocument();
    //    string reportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Academic/rptCertificateRegslip_student.rpt");
    //    customReport.Load(reportPath);
    //    oStream = (MemoryStream)customReport.ToString(reportPath);

    //    return oStream;
    //}

    //private string ShowGeneralExportReportForMail1(string path)
    //{
    //    string reportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Academic/rptCertificateRegslip_student.rpt");
    //    return reportPath;
    //}
}