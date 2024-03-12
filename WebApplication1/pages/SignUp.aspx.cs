//==============================================
//Module             : Direct Entry Admission
//Create Date        : 11/07/2019
//Controller file    : New_Student_RegController.cs
//Entity file        : New_Student.cs
//Validatin file     : validation.js &
//                     buttonValidation.js
//==============================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Web.Configuration;
using mastersofterp;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Cryptography;

using System.IO;
using System.Text;
using System.Windows.Forms;
using mastersofterp;
using System.Web.Configuration;
using System.Data;

public partial class ACADEMIC_SignUp : System.Web.UI.Page
{

    UAIMS_Common objUCommon = new UAIMS_Common();
    New_Student_RegController objSC = new New_Student_RegController();
    New_Student objNS = new New_Student();
    Common objCommon = new Common();
    User_AccController objUC = new User_AccController();
    UserAcc objUA = new UserAcc();
    string uname = string.Empty;
    string pass = string.Empty;
    string emailId = string.Empty;
    int userno = -1;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, string.Empty);

        //Session["userno"] = "2";
        //Session["username"] = "New User";
        //Session["usertype"] = "Student";
        //Session["userfullname"] = "New User";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["coll_name"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                rblCourse.Focus();
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0 AND DEGREENO IN (1,2)", "DEGREENAME");
                objCommon.FillDropDownList(ddlAdmissionType, "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO>0", "IDTYPENO"); // Admission Type
            }
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentInfo.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentInfo.aspx");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH M ON (B.BRANCHNO = M.BRANCHNO)", "B.BRANCHNO", "LONGNAME", "B.BRANCHNO>0 AND DEGREENO=" + ddlDegree.SelectedValue + "", "LONGNAME");

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PresentationLayer_StudentRegistration.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        ddlDegree.Focus();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string ipAddress = string.Empty, email = string.Empty, _mobileNo = string.Empty, admYear = string.Empty;
        int _catNo = 0;

        try
        {
            if (ddlAdmissionType.SelectedValue == "2")
            {
                admYear = objCommon.LookUp("ACD_ADMBATCH", "BATCHNAME", "BATCHNO=(SELECT (MAX(BATCHNO)-1) FROM ACD_ADMBATCH)");
            }
            else
            {
                admYear = objCommon.LookUp("ACD_ADMBATCH", "BATCHNAME", "BATCHNO=(SELECT MAX(BATCHNO) FROM ACD_ADMBATCH)");
            }

            _catNo = Convert.ToInt32(objCommon.LookUp("NEW_STUDENT_REGISTRAION", "COUNT(CATNO)", "CATNO !='' AND CATNO='" + txtCatNo.Text.Trim() + "'"));
            if (_catNo > 0)
            {
                txtCatNo.Text = string.Empty;
                txtCatNo.Focus();
                objCommon.DisplayMessage(this, "CAT/TNEA Number already exists !", this);
                return;
            }

            _mobileNo = objCommon.LookUp("USER_ACC", "ISNULL(UA_MOBILE,0)", "UA_MOBILE='" + txtMobileNo.Text.Trim() + "'");
            if (_mobileNo.Equals(txtMobileNo.Text.Trim()))
            {
                txtMobileNo.Text = string.Empty;
                txtMobileNo.Focus();
                objCommon.DisplayMessage(this, "Mobile No. already exists !", this);
                return;
            }


            objNS.UA_NO = Convert.ToInt32(Session["userno"]);
            if (!txtCatNo.Text.Trim().Equals(string.Empty)) objNS.CatNo = txtCatNo.Text.Trim();
            if (!txtStudName.Text.Trim().Equals(string.Empty)) objNS.StudName = txtStudName.Text.Trim();
            if (!txtMobileNo.Text.Trim().Equals(string.Empty)) objNS.MobileNo = txtMobileNo.Text.Trim();
            if (!txtEmailId.Text.Trim().Equals(string.Empty)) objNS.EmailId = txtEmailId.Text.Trim();
            if (!txtDOB.Text.Trim().Equals(string.Empty)) objNS.Dob = Convert.ToDateTime(txtDOB.Text.Trim());
            objNS.Degree = Convert.ToInt32(ddlDegree.SelectedValue);
            objNS.Branch = Convert.ToInt32(ddlBranch.SelectedValue);
            objNS.AdmissionType = Convert.ToInt32(ddlAdmissionType.SelectedValue);

            if (rblCourse.SelectedValue == "1")
                objNS.Course = "1";  // Consortium
            else if (rblCourse.SelectedValue == "2")
                objNS.Course = "2";// TNEA
            else if (rblCourse.SelectedValue == "3")
                objNS.Course = "3";// Other


            Random rnd = new Random();
            string studname = txtStudName.Text.ToUpper();
            if (studname.Contains(" "))
            {
                uname = studname.Substring(0, studname.IndexOf(" "));
            }
            else
            {
                uname = studname;
            }
            uname = admYear + uname;
            uname += rnd.Next(10, 2000);
            Session["uname"] = uname;
            objNS.UserName = uname;

            //if (studname.Contains(" "))
            //{
            //    uname = studname.Substring(0, studname.IndexOf(" "));
            //}
            //else
            //{
            //    uname = studname;
            //}

            pass = CreateRandomPassword(8);
            Session["password"] = pass;
            //uname = Common.EncryptPassword(uname);
            pass = clsTripleLvlEncyrpt.ThreeLevelEncrypt(pass);
            objNS.Password = pass;

            Session["SSName"] = txtStudName.Text.Trim();
            emailId = txtEmailId.Text.Trim();
            Session["SMobNo"] = txtMobileNo.Text.Trim();

            ipAddress = GetIPAddress();

            int count = Convert.ToInt32(objCommon.LookUp("USER_ACC", "COUNT(UA_NAME)", "UA_NAME='" + objNS.UserName + "'"));

            if (count == 0)
            {
                //upload document

                int status = uploadDocument();
                if (status == 1)
                {

                    int cs = (Int32)objSC.Insert_Update_New_Student(objNS, ipAddress);
                    if (cs > 0)
                    {

                        objCommon.DisplayMessage(this, "Registration Successfully Completed with Temp No. : "+cs, this.Page);

                        //SMS send
                        //SendSMSToUser();
                        //if (!string.IsNullOrEmpty(txtMobileNo.Text.Trim()) && txtMobileNo.Text.Length == 10)
                        //{
                        //    SendSMSUser();
                        //}

                        //if (!string.IsNullOrEmpty(txtEmailId.Text.Trim()))
                        //{
                        //    //EMAIL send
                        //    TransferToEmail(emailId);
                        //    lblMsgToUser.Text = "User ID and Password Sent to Registered email ID and Mobile";

                        //    lblMsgToUser.Visible = true;
                        //}

                        ClearAllField();
                        Session["uname"] = null;


                        //divMsg.Visible = true;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Registration Not Completed, Please try again !", this.Page);
                        //divMsg.Visible = true;
                    }
                }
                else
                {
                    //document upload error
                    objCommon.DisplayMessage(this, "Document upload failed, Please try again !", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "Registration Not Completed, User Name Already Exists!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this, "Something went wrong ! Please contact Admin", this.Page);
            return;
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "New_Stud_Registration.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected string GetIPAddress()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }

        return context.Request.ServerVariables["REMOTE_ADDR"];
    }

    private void SendSMSUser()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");

            string Url = string.Format("http://" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?");//"http://smsnmms.co.in/sms.aspx";
            Session["url"] = Url;
            string UserId = ds.Tables[0].Rows[0]["SMSSVCID"].ToString(); //"hr@iitms.co.in";
            Session["userid"] = UserId;

            string Password = ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();//"iitmsTEST@5448";
            Session["pwd"] = Password;
            string MobileNo = "91" + txtMobileNo.Text.Trim();

            string Message = "Registration Successfully Completed at SVCE Admission Portal, UserID :" + Session["uname"] + " and Password : " + Session["password"];

            if (txtMobileNo.Text.Trim() != string.Empty)
            {
                SendSMS(Url, UserId, Password, MobileNo, Message);
            }

        }
        catch (Exception)
        {
            // objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
            return;
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
        catch (Exception)
        {

        }
    }

    //private void SendSMSToUser()
    //{
    //    objNS.RegNo = Convert.ToString(10);
    //    objNS.UName = Session["SSName"].ToString();
    //    objNS.PhoneNo = Session["SMobNo"].ToString();
    //    objNS.SmsBody = ", Registration Successfully Completed, UserID :" + Session["uname"] + " and Password : " + Session["password"] + ". SVCE";
    //    int pkId = objSC.VerifyOTP(objNS);
    //}

    public void TransferToEmail(string mailId)
    {
        try
        {
            string EMAILID = mailId.Trim();
            var fromAddress = objCommon.LookUp("REFF", "LTRIM(RTRIM(EMAILSVCID))", ""); //"incharge_mis_ghrcemp@raisoni.net";   

            // any address where the email will be sending
            var toAddress = EMAILID.Trim();
            //Password of your gmail address


            var fromPassword = objCommon.LookUp("REFF", "LTRIM(RTRIM(EMAILSVCPWD))", "");   // const string fromPassword = "thebestofall";  
            // Passing the values and make a email formate to display

            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            msg.From = new MailAddress(fromAddress, "SVCE");
            msg.To.Add(new MailAddress(toAddress));
            msg.Subject = "Registration Successfully Completed";

            using (StreamReader reader = new StreamReader(Server.MapPath("~/email_template_registration.html")))
            {
                msg.Body = reader.ReadToEnd();
            }

            msg.Body = msg.Body.Replace("{Name}", Session["SSName"].ToString());
            msg.Body = msg.Body.Replace("{UserName}", Session["uname"].ToString());
            msg.Body = msg.Body.Replace("{Password}", Session["password"].ToString());

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
            { }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Admission_NewStudent.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAllField();
        rblCourse.Focus();
        lblMsgToUser.Text = string.Empty;
    }

    private void ClearAllField()
    {
        txtStudName.Text = txtMobileNo.Text = txtEmailId.Text = txtDOB.Text = txtCatNo.Text = string.Empty;
        rblCourse.SelectedItem.Value = "1";
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlAdmissionType.SelectedIndex = 0;

    }



    public static string EncryptPassword(string password)
    {
        string mchar = string.Empty;
        string pvalue = string.Empty;

        for (int i = 1; i <= password.Length; i++)
        {
            mchar = password.Substring(i - 1, 1);
            byte[] bt = System.Text.Encoding.Unicode.GetBytes(mchar);
            int no = int.Parse(bt[0].ToString());
            char ch = Convert.ToChar(no + i);
            pvalue += ch.ToString();
        }
        return pvalue;
    }

    public static string DecryptPassword(string password)
    {
        string mchar = string.Empty;
        string pvalue = string.Empty;

        for (int i = 1; i <= password.Length; i++)
        {
            mchar = password.Substring(i - 1, 1);
            byte[] bt = System.Text.Encoding.Unicode.GetBytes(mchar);
            int no = int.Parse(bt[0].ToString());
            char ch = Convert.ToChar(no - i);
            pvalue += ch.ToString();
        }
        return pvalue;
    }

    public static string CreateRandomPassword(int PasswordLength)
    {
        string _allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ"; //abcdefghijkmnopqrstuvwxyz
        Random randNum = new Random();
        char[] chars = new char[PasswordLength];
        int allowedCharCount = _allowedChars.Length;
        for (int i = 0; i < PasswordLength; i++)
        {
            chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
        }
        return new string(chars);
    }

    private int uploadDocument()
    {
        int status = 0;

        try
        {



            string folderPath = WebConfigurationManager.AppSettings["SVCE_SIGNUP_DOC"].ToString() + Session["uname"] + "\\";

            if (fuDocUpload.HasFile)
            {


                string ext = System.IO.Path.GetExtension(fuDocUpload.PostedFile.FileName);
                HttpPostedFile file = fuDocUpload.PostedFile;
                string filename = Path.GetFileName(fuDocUpload.PostedFile.FileName);
                if (ext == ".jpeg" || ext == ".jpg" || ext == ".png" || ext == ".doc" || ext == ".docx" || ext == ".pdf")
                {
                    if (file.ContentLength <= 102400)// 31457280 before size  //For Allowing 100 Kb Size Files only 
                    {
                        string contentType = fuDocUpload.PostedFile.ContentType;
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }


                        objNS.FileName = filename;
                        objNS.FilePath = folderPath + filename;
                        fuDocUpload.PostedFile.SaveAs(folderPath + filename);
                        status = 1;

                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Document size must not exceed 100 Kb !", this.Page);

                    }
                }
                else
                {
                    objCommon.DisplayMessage(this, "Only .jpg,.jpeg,.png,.pdf,.doc,.docx file type allowed  !", this.Page);

                }

            }
            else if (rblCourse.SelectedValue == "2")
            {
                objCommon.DisplayMessage(this, "Please upload document file !", this.Page);
                return status;
            }
            else
            {
                return 1;
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this, "Something went wrong ! Please contact Admin", this.Page);
            return status;
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "PresentationLayer_SignUp.uploadDocument-> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server UnAvailable");
        }

        return status;
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
    }
    protected void ddlAdmissionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlAdmissionType.Focus();
    }
}