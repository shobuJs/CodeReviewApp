//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STUDENT APPROVAL STATUS                                            
// CREATION DATE : 15-JUNE-2019                                                          
// CREATED BY    : IRFAN SHAIKH                                                
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using mastersofterp;

public partial class ACADEMIC_StudentApprovalStatus : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    StudentController objStudCon = new StudentController();
    //private string DirPath1 = System.Configuration.ConfigurationManager.AppSettings["docPath"].ToString();
    public string Docpath = string.Empty;

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
            if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();

                //Bind dropdown list 
                objCommon.FillDropDownList(ddlAdmBach, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                int Admbatch = Convert.ToInt32(objCommon.LookUp("ACD_ADMBATCH", "MAX(BATCHNO)", "BATCHNO<>0"));

                BindList(0,0);//For all records
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
                Response.Redirect("~/notauthorized.aspx?page=StudentApprovalStatus.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentApprovalStatus.aspx");
        }
    }

    private void BindList(int AdmBatch, int AdmType)
    {
        try
        {
            //// For displaying data in GridView
            DataSet ds = objStudCon.GetApprovalStatus(AdmBatch, AdmType);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudApproveList.DataSource = ds;
                lvStudApproveList.DataBind();
                lvStudApproveList.Visible = true;
                dvCourse.Visible = true;
                Panel1.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(this,"No record found.",this);
                lvStudApproveList.DataSource = null;
                lvStudApproveList.DataBind();
                lvStudApproveList.Visible = false;
                Panel1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentApprovalStatus.BindList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void chkAllow_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chkAllow = sender as CheckBox;

            int uaDec = Convert.ToInt32(chkAllow.Checked == true ? 0 : 1);
            //HiddenField hdn = sender as  HiddenField;
            GridViewRow row = (GridViewRow)chkAllow.NamingContainer;

            HiddenField HdfValue = (HiddenField)row.FindControl("hdnUaNo");

            int uaNo = Convert.ToInt32(chkAllow.ToolTip);
            int Aprv_by = Convert.ToInt32(Session["userno"]);

            


            int ret = objStudCon.ToggleStudentApprovalStatus(uaNo, uaDec, Aprv_by);
            {
                //// For messages if any;
                string emailId = string.Empty;
                Label ContactNo = (Label)row.FindControl("lblContact");

                HiddenField hdfEmail = (HiddenField)row.FindControl("hdfEmailId");
                emailId = hdfEmail.Value.ToString().Trim();
                BindList(Convert.ToInt32(ddlAdmBach.SelectedValue),Convert.ToInt32(rblAdmType.SelectedValue));

                //get student userId and Password to send email/sms
                DataSet dsStudentDetail = objCommon.FillDropDown("USER_ACC", "UA_NAME", "UA_PWD", "UA_NO = " + uaNo + " AND USER_TYPE=1", "");
                if (dsStudentDetail.Tables[0].Rows.Count > 0)
                {
                    Session["studID"] = dsStudentDetail.Tables[0].Rows[0]["UA_NAME"].ToString();
                    Session["studPass"] = clsTripleLvlEncyrpt.ThreeLevelDecrypt(dsStudentDetail.Tables[0].Rows[0]["UA_PWD"].ToString());
                }


                if (!string.IsNullOrEmpty(emailId) && uaNo != 0 && uaDec==0)
                {                      
                    //EMAIL send
                    SendEmail(emailId, uaNo);   //Please Comment this line before Testing ****

                    DataSet ds = objCommon.FillDropDown("REFF", "SMSPROVIDER", "SMSSVCID,SMSSVCPWD", "", "");
                    
                    string Url = string.Format("http://" + ds.Tables[0].Rows[0]["SMSPROVIDER"].ToString() + "?");////"http://smsnmms.co.in/sms.aspx";
                    Session["url"] = Url;
                    string UserId = ds.Tables[0].Rows[0]["SMSSVCID"].ToString(); //"hr@iitms.co.in";
                    Session["userid"] = UserId;
                    // string Password = clsTripleLvlEncyrpt.ThreeLevelDecrypt(Session["SMSSVCPWD"].ToString());//"iitmsTEST@5448";

                    string Password = ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();//"iitmsTEST@5448";
                    Session["pwd"] = Password;

                    //string Message = "Your verification done and Approved ! Now you can login with your credentials.";
                    string Message = "Your registration is verified and approved. Your login UserID : " + Session["studID"] + " Password: " + Session["studPass"] + " Please proceed to enter your particulars.";

                    string MobileNo = "91" + ContactNo.Text.Trim();
                    if (ContactNo.Text.Trim() != string.Empty)
                    {
                        SendSMS(Url, UserId, Password, MobileNo, Message); //Please Comment this line before Testing ****
                    }
                    objCommon.DisplayMessage(this, "Approved successfully and email sent to " + emailId, this);
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentApprovalStatus.chkAllow_CheckedChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void chkReject_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chkReject = sender as CheckBox;

            int cancel = Convert.ToInt32(chkReject.Checked == true ? 1 : 0);
            //HiddenField hdn = sender as  HiddenField;
            GridViewRow row = (GridViewRow)chkReject.NamingContainer;

            HiddenField HdfValue = (HiddenField)row.FindControl("hdnUaNo");

            int uaNo = Convert.ToInt32(chkReject.ToolTip);
            int can_by = Convert.ToInt32(Session["userno"]);

            int ua_dec = Convert.ToInt32(objCommon.LookUp("USER_ACC", "ISNULL(UA_DEC,0)", "ISNULL(UA_STATUS,0)=0 AND  UA_NO =" + uaNo));
            if (ua_dec == 0)
            {
                objCommon.DisplayMessage(this, "Sorry, can't Reject ! Application already Approved.",this);
                return;
            }

            int ret = objStudCon.ToggleStudentCancelStatus(uaNo, cancel, can_by);
            {
                //// For messages if any;
                string emailId = string.Empty;
                Label ContactNo = (Label)row.FindControl("lblContact");

                HiddenField hdfEmail = (HiddenField)row.FindControl("hdfEmailId");
                emailId = hdfEmail.Value.ToString().Trim();
                BindList(Convert.ToInt32(ddlAdmBach.SelectedValue), Convert.ToInt32(rblAdmType.SelectedValue));

                if (!string.IsNullOrEmpty(emailId) && uaNo != 0 && cancel == 1)
                {
                    //EMAIL send for Rejection
                    SendEmailReject(emailId, uaNo);   //Please Comment this line before Testing ****

                    DataSet ds = objCommon.FillDropDown("REFF", "SMSPROVIDER", "SMSSVCID,SMSSVCPWD", "", "");

                    string Url = string.Format("http://" + ds.Tables[0].Rows[0]["SMSPROVIDER"].ToString() + "?");////"http://smsnmms.co.in/sms.aspx";
                    Session["url"] = Url;
                    string UserId = ds.Tables[0].Rows[0]["SMSSVCID"].ToString(); //"hr@iitms.co.in";
                    Session["userid"] = UserId;
                    // string Password = clsTripleLvlEncyrpt.ThreeLevelDecrypt(Session["SMSSVCPWD"].ToString());//"iitmsTEST@5448";

                    string Password = ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();//"iitmsTEST@5448";
                    Session["pwd"] = Password;

                    string Message = "Your Application has been Rejected, Please sign up again with valid details.";

                    string MobileNo = "91" + ContactNo.Text.Trim();
                    if (ContactNo.Text.Trim() != string.Empty)
                    {
                        SendSMS(Url, UserId, Password, MobileNo, Message); //Please Comment this line before Testing ****
                    }
                    objCommon.DisplayMessage(this, "Rejected successfully and email sent to " + emailId, this);
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentApprovalStatus.chkAllow_CheckedChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    public void SendEmail(string mailId,int uaNo)
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
            msg.Subject = "Verification Completed Successfully";

            using (StreamReader reader = new StreamReader(Server.MapPath("~/verification_template.html")))
            {
                msg.Body = reader.ReadToEnd();
            }

            Session["SSName"] = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "ISNULL(UA_NO,0)<>0 AND UA_NO=" + uaNo);

            msg.Body = msg.Body.Replace("{Name}", Session["SSName"].ToString());
            msg.Body = msg.Body.Replace("{UserName}", Session["studID"].ToString());
            msg.Body = msg.Body.Replace("{Password}", Session["studPass"].ToString());

            msg.IsBodyHtml = true;
            //smtp.enableSsl = "true";
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
                    objUCommon.ShowError(Page, "ACADEMIC_StudentApprovalStatus.smtp.Send-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentApprovalStatus.chkAllow_CheckedChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void SendEmailReject(string mailId, int uaNo)
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
            msg.Subject = "Application Rejected";

            using (StreamReader reader = new StreamReader(Server.MapPath("~/rejection_template.html")))
            {
                msg.Body = reader.ReadToEnd();
            }

            Session["SSName"] = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "ISNULL(UA_NO,0)<>0 AND UA_NO=" + uaNo);

            msg.Body = msg.Body.Replace("{Name}", Session["SSName"].ToString());

            msg.IsBodyHtml = true;
            //smtp.enableSsl = "true";
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
                    objUCommon.ShowError(Page, "ACADEMIC_StudentApprovalStatus.smtp.Send-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentApprovalStatus.chkAllow_CheckedChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    public void SendSMS(string url, string uid, string pass, string mobno, string message)
    {
        try
        {
            WebRequest request = HttpWebRequest.Create("" + url + "ID=" + uid + "&PWD=" + pass + "&PHNO=" + mobno + "&TEXT=" + message + "");
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need 
            //return urlText;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentApprovalStatus.SendSMS-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lnkdocDownload_Click(object sender, EventArgs e)
    {
        GridViewRow item = (GridViewRow)(sender as Control).NamingContainer;
        HiddenField hdfFilename = (HiddenField)item.FindControl("hdfFilename");
        HiddenField hdfFilePath = (HiddenField)item.FindControl("hdfFilePath");
        LinkButton lnkbtndoc = (LinkButton)item.FindControl("lnkdocDownload");

        string FILENAME = string.Empty;

        FILENAME = hdfFilename.Value.ToString().Trim();

        string filePath;
        filePath = hdfFilePath.Value.ToString().Trim();

        FileInfo file = new FileInfo(filePath);

        if (file.Exists)
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            //Response.AddHeader("content-disposition", "attachment; filename=" + filename);
            //Response.AddHeader("Content-Type", "application/pdf");
        
            Response.AppendHeader("Content-disposition", "attachment; filename=" + filePath);// GHRCEPROSPECT.pdf");
            //Response.ContentType = "application/pdf";
            Response.ContentType = GetResponseType(filePath.Substring(filePath.IndexOf('.')));
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.Clear();
            Response.WriteFile(file.FullName);
            Response.End();
        }
        //=============================================//      
    }

    public void DownloadFile(string filePath1,  string filename)
    {
        try
        {
            Docpath =  filePath1;
            FileStream sourceFile = new FileStream((Docpath), FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();

            Response.Clear();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(filePath1.Substring(filePath1.IndexOf('.')));
            Response.AddHeader("content-disposition", "attachment; filename=" + filename);
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.ContentType = GetResponseType(filePath1.Substring(filePath1.IndexOf('.')));
            Response.Write("Unable to download the attachment.");
        }
    }
        
    private string GetResponseType(string fileExtension)
    {
        string ret = string.Empty;
        switch (fileExtension.ToLower())
        {
            case ".doc":
                ret = "application/vnd.ms-word";
                break;

            case ".docx":
                ret = "application/vnd.ms-word";
                break;

            case ".wps":
                ret = "application/ms-excel";
                break;

            case ".jpeg":
                ret = "image/jpeg";
                break;

            case ".jpg":
                ret = "image/jpg";
                break;

            case ".gif":
                ret = "image/gif";
                break;

            case ".png":
                ret = "image/png";
                break;

            case ".bmp":
                ret = "image/bmp";
                break;

            case ".tiff":
                ret = "image/tiff";
                break;

            case ".ico":
                ret = "image/x-icon";
                break;

            case ".txt":
                ret = "text/plain";
                break;

            case ".pdf":
                ret = "application/pdf";
                break;
                            
            case "":
                ret = "";
                break;

            default:
                ret = "";
                break;
        }
        return ret;
    }

    protected void ddlAdmBach_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(ddlAdmBach.SelectedValue!=null)
            {
                BindList(Convert.ToInt32(ddlAdmBach.SelectedValue), Convert.ToInt32(rblAdmType.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentApprovalStatus.ddlAdmBach_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void rblAdmType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(ddlAdmBach.SelectedValue!=null && rblAdmType.SelectedValue!=null)
            {
                BindList(Convert.ToInt32(ddlAdmBach.SelectedValue), Convert.ToInt32(rblAdmType.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentApprovalStatus.rblAdmType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}