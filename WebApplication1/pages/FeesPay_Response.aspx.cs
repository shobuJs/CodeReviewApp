using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Linq;
using System.Net;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using System.Net.NetworkInformation;
using CCA.Util;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Data.Sql;
using System.Data.SqlClient;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Mail;
using Newtonsoft.Json;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SendGrid.Helpers.Mail;


public partial class FeesPay_Response : System.Web.UI.Page
{
    #region Variable Declaration

    //string
    //      strPG_TxnStatus = string.Empty,
    //      strPG_TPSLTxnBankCode = string.Empty,
    //      strPG_TxnDateTime = string.Empty,
    //      strPG_TxnDate = string.Empty,
    //      strPG_TxnTime = string.Empty,
    //      strPG_TxnType = string.Empty;

    string strPG_TxnStatus = string.Empty;
    string[] strArrPG_TxnDateTime;
    string strHEX, strPGActualReponseWithChecksum, strPGActualReponseEncrypted, strPGActualReponseDecrypted, strPGresponseChecksum, strPGTxnStatusCode;
    string[] strPGChecksum, strPGTxnString;
    bool isDecryptable = false;
    string strPG_MerchantCode = string.Empty;
    string strPGResponse;
    string strMerTrxId = string.Empty;
    string[] strSplitDecryptedResponse;
    string RECIEPT_CODE = string.Empty;
    string txt_date = string.Empty;

    #endregion

    String MerchantCode = ConfigurationManager.AppSettings["MERCHANT_KEY"];
    public string strAccessCode = ConfigurationManager.AppSettings["workingKey"]; // put the access code in the quotes provided here.
    string workingKey = ConfigurationManager.AppSettings["strAccessCode"];//put in the 32bit alpha numeric key in the quotes provided here 	


    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objfeeController = new FeeCollectionController();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //for bill desk
                //string[] responseArrary = Convert.ToString(HttpContext.Current.Request["msg"]).Split('|');
                //CheckResponseFromBillDesk(responseArrary);

                //for cc avenue PG
                display();

            }
        }
        catch { }
    }



    #region "CC AVENUE Payment Response"
    public void display()
    {
        try
        {
            //int orderid =Convert.ToInt32(Session["ORDERID"].ToString());
            //decimal amt = Convert.ToDecimal(Session["FINAL_TOTALAMOUNT"].ToString());

            string strDecryptedVal;
            string workingKey = ConfigurationManager.AppSettings["workingKey"];//put in the 32bit alpha numeric key in the quotes provided here
            CCACrypto ccaCrypto = new CCACrypto();
            string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
            NameValueCollection Params = new NameValueCollection();
            string[] segments = encResponse.Split('&');
            foreach (string seg in segments)
            {
                string[] parts = seg.Split('=');
                if (parts.Length > 0)
                {
                    string Key = parts[0].Trim();
                    string Value = parts[1].Trim();
                    Params.Add(Key, Value);
                }
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            string res = "Online Fees Payment - Normal Response";
            SqlCommand cmd = new SqlCommand("Insert into Test (Name,Response) values ('" + Convert.ToString(encResponse) + "','" + res + "')", con);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();


            //Session["ORDERID"] = Session["ORDERID"].ToString();

            GetPGRespnseData(segments);

            ViewState["TRANSACTIONID"] = lblCLNT_TrackingTXN_REF.Text;
            ViewState["ORDERID"] = lblTPSL_ORDER_TXN_ID.Text;
            //RECIEPT_CODE = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID='" + lblTPSL_ORDER_TXN_ID.Text + "'");
            //ViewState["RECIEPT_CODE"] = RECIEPT_CODE;
            ViewState["RECIEPT_CODE"] = lblRec_Code.Text;

            string order_id = objCommon.LookUp("ACD_DCR", "ISNULL(ORDER_ID,'') ORDER_ID", "IDNO=" + ViewState["IDNO"].ToString() + " AND RECIEPT_CODE='" + lblRec_Code.Text + "' AND SESSIONNO=" + ViewState["SESSIONNO"].ToString() + " AND ORDER_ID= '" + lblTPSL_ORDER_TXN_ID.Text + "'");

            string order_Amount = objCommon.LookUp("ACD_DCR", "ISNULL(SUM(TOTAL_AMT),0) TOTAL_AMT", "IDNO=" + ViewState["IDNO"].ToString() + " AND RECIEPT_CODE='" + lblRec_Code.Text + "' AND SESSIONNO=" + ViewState["SESSIONNO"].ToString() + " AND ORDER_ID= '" + lblTPSL_ORDER_TXN_ID.Text + "'");
            if (lblTPSL_ORDER_TXN_ID.Text.Trim() == order_id.Trim())
            {
              if (lblTXN_AMT.Text.Trim() == order_Amount.Trim())
               {
                    if (lblTXN_STATUS.Text == "Success")
                    {
                        LiteralMessage.Text = "<H3>Your Transaction Done Successfully.<br>Please Note Down Transaction Details For Future Reference.</h3><br> <table width='100%'><tr width='100%'><td align='left' width='50%' style='padding-left:122px;'>ORDER ID</td><td align='left' width='50%' style='color:black; padding-left: 69px;'>" + lblTPSL_ORDER_TXN_ID.Text + "</td></tr><tr width='100%'><td align='left' width='50%' style='padding-left:122px;'>Transaction Id</td><td align='left' width='50%' style='color:black; padding-left: 69px;'>" + lblCLNT_TrackingTXN_REF.Text + "</td></tr><tr width='100%'><td align='left' width='50%'  style='padding-left:122px;'>Total Amount</td><td align='left' width='50%' style='color:black; padding-left: 69px;'>" + lblTXN_AMT.Text + "</td></tr><tr width='100%'><td align='left' width='50%' style='padding-left:122px;'>Transaction Status Code</td><td align='left' width='50%' style='color:black; padding-left: 69px;'>" + lblTXN_STATUS.Text + "</td></tr><tr width='100%'><td align='left' width='50%'style='padding-left:122px;'>Status</td><td align='left' width='50%' style='color:green; padding-left: 69px;'>Success</td></tr></table>";
                        lblStatus.Text = "Your Transaction Completed Successfully."; // +strPG_TxnStatus;

                        btnReports.Visible = true;
                        btnRegistrationSlip.Visible = false;
                        lbtnGoBack.Visible = true;
                        lblStatus.ForeColor = System.Drawing.Color.LightGreen;
                        Session["Status"] = lblTXN_STATUS.Text;
                    }
                    else
                    {
                        LiteralMessage.Text = "<H3>Your Transaction Failed.</h3><br> <table width='100%'><tr width='100%'><td align='left' width='50%' style='padding-left:122px;'>ORDER ID</td><td align='left' width='50%' style='color:black; padding-left: 69px;'>" + lblTPSL_ORDER_TXN_ID.Text + "</td></tr><tr width='100%'><td align='left' width='50%' style='padding-left:122px;'>Transaction Id</td><td align='left' width='50%' style='color:black; padding-left: 69px;'>" + lblCLNT_TrackingTXN_REF.Text + "</td></tr><tr width='100%'><td align='left' width='50%'  style='padding-left:122px;'>Total Amount</td><td align='left' width='50%' style='color:black; padding-left: 69px;'>" + lblTXN_AMT.Text + "</td></tr><tr width='100%'><td align='left' width='50%' style='padding-left:122px;'>Transaction Status Code</td><td align='left' width='50%' style='color:black; padding-left: 69px;'>" + lblTXN_STATUS.Text + "</td></tr><tr width='100%'><td align='left' width='50%'style='padding-left:122px;'>Status</td><td align='left' width='50%' style='color:red; padding-left: 69px;'>Fail</td></tr></table>";
                        lblStatus.Text = "Transaction Fail ";// +"Response :: <br/>" + strDecryptedVal;
                        btnReports.Visible = false;
                        btnRegistrationSlip.Visible = false;
                        lbtnGoBack.Visible = true;
                        lblStatus.ForeColor = System.Drawing.Color.Red;

                    }

                    string Trans_Status = "0";
                    if (lblTXN_STATUS.Text == "Success")
                    {
                        Trans_Status = "1";
                    }
                    else
                    {
                        Trans_Status = "0";
                    }


                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
                    if (con1.State == ConnectionState.Open)
                    {
                        con1.Close();
                    }
                    con1.Open();
                    string res1 = "Online Fees Payment - Normal Response";
                    string Pay_type = "Online Fees Payment";
                    SqlCommand cmd1 = new SqlCommand("Insert into Test (Name,Response,IDNO,ORDER_ID,Status,REC_CODE,PAY_TYPE) values ('" + Convert.ToString(encResponse) + "','" + res + "','" + ViewState["IDNO"].ToString() + "','" + lblTPSL_ORDER_TXN_ID.Text + "','" + lblTXN_STATUS.Text + "','" + ViewState["RECIEPT_CODE"].ToString() + "','" + Pay_type + "')", con1);
                    cmd1.Connection = con1;
                    cmd1.ExecuteNonQuery();
                    con1.Close();


                    // result = objfeeController.UpdateAppliedPaymentStatus(lblTPSL_BANKREFNO_CD.Text, lblCLNT_TrackingTXN_REF.Text, lblTPSL_ORDER_TXN_ID.Text, lblTXN_AMT.Text, Trans_Status, lblTXN_STATUS.Text, encResponse, lblRec_Code.Text, lblPaymentMode.Text, lblCardName.Text);//Convert.ToString(TransStatus)
                    int result = 0;
                    result = objfeeController.OnlineInstallmentFeesPayment(lblTPSL_BANKREFNO_CD.Text, lblTPSL_ORDER_TXN_ID.Text, lblTXN_AMT.Text, Trans_Status, lblTXN_STATUS.Text, encResponse, lblPaymentMode.Text, lblCLNT_TrackingTXN_REF.Text, lblCardName.Text, lblRec_Code.Text);

                    //TransferToEmail();
                    // Execute(message, emailid).Wait();


                    InitializeSession();

                    if (lblTXN_STATUS.Text == "Success")
                    {
                        string Email = "";
                        Email = objCommon.LookUp("ACD_STUDENT", "ISNULL(EMAILID,'') EMAILID", "IDNO=" + ViewState["IDNO"].ToString() + "");

                        int BRANCHNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + ViewState["IDNO"].ToString() + ""));
                        string BranchName = objCommon.LookUp("ACD_BRANCH", "SHORTNAME", "BRANCHNO=" + BRANCHNO + "");

                        int Degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + ViewState["IDNO"].ToString() + ""));
                        string DegreeName = objCommon.LookUp("ACD_DEGREE", "CODE", "DEGREENO=" + Degreeno + "");

                        string StudentEnrollNo = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO=" + ViewState["IDNO"].ToString() + "");
                        string RegNo = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + ViewState["IDNO"].ToString() + "");
                        string Enrollno = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO=" + ViewState["IDNO"].ToString() + "");
                        var toAddress = "";

                        if (Email == "")
                        {
                            toAddress = objCommon.LookUp("USER_ACC", "ISNULL(UA_EMAIL,'') UA_EMAIL", "UA_TYPE=2 AND UA_IDNO=" + ViewState["IDNO"].ToString() + "");
                        }
                        else
                        {
                            toAddress = Email.ToString();
                        }

                        string trandate = Convert.ToString(objCommon.LookUp("ACD_FEES_LOG", "TRANSDATE", "ORDER_ID = '" + ViewState["ORDERID"].ToString() + "'"));
                        string fullreceiptname = Convert.ToString(objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_TITLE", "RECIEPT_CODE = '" + ViewState["RECIEPT_CODE"].ToString() + "'"));

                        string withoutlatefee = "0";
                        withoutlatefee = Convert.ToString(Convert.ToDecimal(lblTXN_AMT.Text) - Convert.ToDecimal(ViewState["LATEFEE"]));

                        Execute(toAddress, ViewState["STUDNAME"].ToString(), RegNo, DegreeName, BranchName, Enrollno,
                             trandate, fullreceiptname,
                             withoutlatefee, ViewState["LATEFEE"].ToString(), lblTXN_AMT.Text,
                             ViewState["ORDERID"].ToString(), lblCLNT_TrackingTXN_REF.Text, lblTPSL_BANKREFNO_CD.Text,
                           lblTXN_STATUS.Text, lblPaymentMode.Text, lblCardName.Text).Wait();

                    }

                    // InitializeSession();
               }//if order amt match
              else
              {
                  lblTXN_STATUS.Text = "Fail";

                  LiteralMessage.Text = "<H3>Your Transaction Failed (Amount Details Not Matched).</h3><br> <table width='100%'><tr width='100%'><td align='left' width='50%' style='padding-left:122px;'>ORDER ID</td><td align='left' width='50%' style='color:black; padding-left: 69px;'>" + lblTPSL_ORDER_TXN_ID.Text + "</td></tr><tr width='100%'><td align='left' width='50%' style='padding-left:122px;'>Transaction Id</td><td align='left' width='50%' style='color:black; padding-left: 69px;'>" + lblCLNT_TrackingTXN_REF.Text + "</td></tr><tr width='100%'><td align='left' width='50%'  style='padding-left:122px;'>Total Amount</td><td align='left' width='50%' style='color:black; padding-left: 69px;'>" + lblTXN_AMT.Text + "</td></tr><tr width='100%'><td align='left' width='50%' style='padding-left:122px;'>Transaction Status Code</td><td align='left' width='50%' style='color:black; padding-left: 69px;'>" + lblTXN_STATUS.Text + "</td></tr><tr width='100%'><td align='left' width='50%'style='padding-left:122px;'>Status</td><td align='left' width='50%' style='color:red; padding-left: 69px;'>Fail</td></tr></table>";
                  lblStatus.Text = "Transaction Fail ";// +"Response :: <br/>" + strDecryptedVal;
                  btnReports.Visible = false;
                  btnRegistrationSlip.Visible = false;
                  lbtnGoBack.Visible = true;
                  lblStatus.ForeColor = System.Drawing.Color.Red;



                  int result = 0;
                  result = objfeeController.OnlineInstallmentFeesPayment(lblTPSL_BANKREFNO_CD.Text, lblTPSL_ORDER_TXN_ID.Text, lblTXN_AMT.Text, "0", lblTXN_STATUS.Text, encResponse, lblPaymentMode.Text, lblCLNT_TrackingTXN_REF.Text, lblCardName.Text, lblRec_Code.Text);

                  //TransferToEmail();
                  // Execute(message, emailid).Wait();
                  InitializeSession();

              }


            }//if order id match
            else
            {
                lblTXN_STATUS.Text = "Fail";

                LiteralMessage.Text = "<H3>Your Transaction Failed (Details Not Matched).</h3><br> <table width='100%'><tr width='100%'><td align='left' width='50%' style='padding-left:122px;'>ORDER ID</td><td align='left' width='50%' style='color:black; padding-left: 69px;'>" + lblTPSL_ORDER_TXN_ID.Text + "</td></tr><tr width='100%'><td align='left' width='50%' style='padding-left:122px;'>Transaction Id</td><td align='left' width='50%' style='color:black; padding-left: 69px;'>" + lblCLNT_TrackingTXN_REF.Text + "</td></tr><tr width='100%'><td align='left' width='50%'  style='padding-left:122px;'>Total Amount</td><td align='left' width='50%' style='color:black; padding-left: 69px;'>" + lblTXN_AMT.Text + "</td></tr><tr width='100%'><td align='left' width='50%' style='padding-left:122px;'>Transaction Status Code</td><td align='left' width='50%' style='color:black; padding-left: 69px;'>" + lblTXN_STATUS.Text + "</td></tr><tr width='100%'><td align='left' width='50%'style='padding-left:122px;'>Status</td><td align='left' width='50%' style='color:red; padding-left: 69px;'>Fail</td></tr></table>";
                lblStatus.Text = "Transaction Fail ";// +"Response :: <br/>" + strDecryptedVal;
                btnReports.Visible = false;
                btnRegistrationSlip.Visible = false;
                lbtnGoBack.Visible = true;
                lblStatus.ForeColor = System.Drawing.Color.Red;



                int result = 0;
                result = objfeeController.OnlineInstallmentFeesPayment(lblTPSL_BANKREFNO_CD.Text, lblTPSL_ORDER_TXN_ID.Text, lblTXN_AMT.Text, "0", lblTXN_STATUS.Text, encResponse, lblPaymentMode.Text, lblCLNT_TrackingTXN_REF.Text, lblCardName.Text, lblRec_Code.Text);

                //TransferToEmail();
                // Execute(message, emailid).Wait();
                InitializeSession();
            }
        }
        catch (Exception ex)
        {

        }

    }






    static async Task Execute(string mailId, string StuName, string RegNo, string degree, string branch, string enrollno,
        string trandate, string fullreceiptname,
        string amount, string late_fee, string tot_amt, string order_id, string tracking_id, string bankrefid,
        string status,
        string paymode, string cardname)
    {
        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID ,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            string clg = "MNR";
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString(), "MNR");
            var toAddress = new MailAddress(mailId, "");

            var apiKey = "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";

            //var apiKey = dsconfig.Tables[0].Rows[0]["API_KEY_SENDGRID"].ToString();
            var client = new SendGridClient(apiKey.ToString());
            // var apiKey = "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";           
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString(), "MNR");
            //var subject = "Your Admission for HSNC University " + clg + "is confirmed!";  //subjects;// "Your OTP for Certificate Registration.";
            var subject = "Fees Payment Status";  //subjects;// "Your OTP for Certificate Registration.";
            var to = new EmailAddress(mailId, "");
            var plainTextContent = "";
            //var htmlContent = "";
            //var msgs = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);           

            //MemoryStream oAttachment = ShowGeneralExportReportForMail("Reports,Academic,rptCertificateRegslip_student.rpt", "@P_COLLEGE_CODE=" + collegecode + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@P_UA_NO=" + uano + ",@P_COLLEGE_ID=" + 8 + "");
            //var bytesRpt = oAttachment.ToArray();  //File.ReadAllBytes(oAttachment);
            // var fileRpt = Convert.ToBase64String(bytesRpt);

            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            msg.Subject = "Fees Payment Status";
            //string LogoPath = System.Web.HttpContext.Current.Server.MapPath("~/IMAGES/HSNCU_Logo2.png");
            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/paysuccessEmail.html")))
            {
                msg.Body = reader.ReadToEnd();
            }

            //msg.Body = msg.Body.Replace("{Name}", StuName.ToString());
            //msg.Body = msg.Body.Replace("{collegeName}", clg.ToString());
            //msg.Body = msg.Body.Replace("{ApplicationNo}", enroll.ToString());
            //msg.Body = msg.Body.Replace("{Degree}", degree.ToString());
            //msg.Body = msg.Body.Replace("{FeesPaid}", amount.ToString());



            msg.Body = msg.Body.Replace("{name}", StuName.ToString());
            msg.Body = msg.Body.Replace("{rollno}", RegNo.ToString());
            msg.Body = msg.Body.Replace("{branch}", degree + " - " + branch.ToString());
            msg.Body = msg.Body.Replace("{enrollno}", enrollno.ToString());
            msg.Body = msg.Body.Replace("{receipttype}", fullreceiptname);//Rectype
            msg.Body = msg.Body.Replace("{amount}", amount);

            msg.Body = msg.Body.Replace("{latefee}", late_fee);
            msg.Body = msg.Body.Replace("{tot_amount}", tot_amt);

            msg.Body = msg.Body.Replace("{orderid}", order_id.ToString());
            msg.Body = msg.Body.Replace("{trackingid}", tracking_id);
            msg.Body = msg.Body.Replace("{bankrefid}", bankrefid);
            msg.Body = msg.Body.Replace("{status}", status);
            msg.Body = msg.Body.Replace("{Tdatetime}", trandate); //transactiondate
            msg.Body = msg.Body.Replace("{recamount}", tot_amt);
            msg.Body = msg.Body.Replace("{paymode}", paymode);
            msg.Body = msg.Body.Replace("{cardname}", cardname);

            msg.IsBodyHtml = true;
            var htmlContent = "";
            //var msgs = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var msgs = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, msg.Body);
            //  var attachments = new List<SendGrid.Helpers.Mail.Attachment>();
            //msgs.AddAttachment("rptCertificateRegslip_student.pdf", fileRpt);
            var response = await client.SendEmailAsync(msgs);
        }
        catch (Exception ex)
        {
        }
    }



    //Other
    //public void TransferToEmail()
    //{
    //    try
    //    {
    //        string Email = "";
    //        Email = objCommon.LookUp("ACD_STUDENT", "ISNULL(EMAILID,'') EMAILID", "IDNO=" + ViewState["IDNO"].ToString() + "");

    //        int BRANCHNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + ViewState["IDNO"].ToString() + ""));
    //        string BranchName = objCommon.LookUp("ACD_BRANCH", "LONGNAME", "BRANCHNO=" + BRANCHNO + "");

    //        string StudentEnrollNo = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO=" + ViewState["IDNO"].ToString() + "");
    //        string RegNo = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + ViewState["IDNO"].ToString() + "");

    //        var toAddress = "";

    //        if (Email == "")
    //        {
    //            toAddress = objCommon.LookUp("USER_ACC", "ISNULL(UA_EMAIL,'') UA_EMAIL", "UA_TYPE=2 AND UA_IDNO=" + ViewState["IDNO"].ToString() + "");
    //        }
    //        else
    //        {
    //            toAddress = Email.ToString();
    //        }

    //        DataSet dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

    //        //var fromAddress = new MailAddress("noreply.mis@nitrr.ac.in", "NITR");

    //        MailMessage msg = new MailMessage();
    //        SmtpClient smtp = new SmtpClient();

    //       // msg.From = new MailAddress("noreply.mis@nitrr.ac.in", "NITR");
    //        msg.From = new MailAddress(dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString(), "MNR");
    //        msg.To.Add(new MailAddress(toAddress));

    //        msg.Subject = "Thank you for your Payment";

    //        using (StreamReader reader = new StreamReader(Server.MapPath("~/paysuccessEmail.html")))
    //        {
    //            msg.Body = reader.ReadToEnd();
    //        }


    //        string trandate = Convert.ToString(objCommon.LookUp("ACD_FEES_LOG", "TRANSDATE", "ORDER_ID = '" + ViewState["ORDERID"].ToString() + "'"));
    //        string fullreceiptname = Convert.ToString(objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_TITLE", "RECIEPT_CODE = '" + ViewState["RECIEPT_CODE"].ToString() + "'"));


    //        msg.Body = msg.Body.Replace("{name}", ViewState["STUDNAME"].ToString());
    //        msg.Body = msg.Body.Replace("{rollno}", RegNo.ToString());
    //        msg.Body = msg.Body.Replace("{branch}", BranchName.ToString());
    //        msg.Body = msg.Body.Replace("{enrollno}", StudentEnrollNo.ToString());
    //        msg.Body = msg.Body.Replace("{receipttype}", fullreceiptname);//Rectype
    //        msg.Body = msg.Body.Replace("{amount}", lblTXN_AMT.Text);

    //        msg.Body = msg.Body.Replace("{latefee}", lblTXN_AMT.Text);
    //        msg.Body = msg.Body.Replace("{tot_amount}", lblTXN_AMT.Text);

    //        msg.Body = msg.Body.Replace("{orderid}", ViewState["ORDERID"].ToString());
    //        msg.Body = msg.Body.Replace("{trackingid}", lblCLNT_TrackingTXN_REF.Text);
    //        msg.Body = msg.Body.Replace("{bankrefid}", lblTPSL_BANKREFNO_CD.Text);
    //        msg.Body = msg.Body.Replace("{status}", lblTXN_STATUS.Text);
    //        msg.Body = msg.Body.Replace("{Tdatetime}", trandate); //transactiondate
    //        msg.Body = msg.Body.Replace("{recamount}", lblTXN_AMT.Text);
    //        msg.Body = msg.Body.Replace("{paymode}", lblPaymentMode.Text);
    //        msg.Body = msg.Body.Replace("{cardname}", lblCardName.Text);

    //        msg.IsBodyHtml = true;
    //        smtp.Host = "smtp.gmail.com";
    //        smtp.Port = 587;
    //        smtp.UseDefaultCredentials = false;
    //        smtp.EnableSsl = true;
    //        //smtp.Credentials = new System.Net.NetworkCredential("noreply.mis@nitrr.ac.in", "mis@nitraipur");
    //        smtp.Credentials = new System.Net.NetworkCredential(dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString(), "MNR");
    //        ServicePointManager.ServerCertificateValidationCallback =
    //            delegate(object s, X509Certificate certificate,
    //            X509Chain chain, SslPolicyErrors sslPolicyErrors)
    //            { return true; };
    //        try
    //        {
    //            smtp.Send(msg);
    //        }
    //        catch (Exception ex)
    //        { }
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}


    //public static async Task Execute(string Message, string toEmailId)
    // {
    //     string LogoPath = System.Web.HttpContext.Current.Server.MapPath("~/IMAGES/MNRLogo.jpg");
    //     Byte[] Imgbytes = File.ReadAllBytes(LogoPath);
    //     string Imgfile = Convert.ToBase64String(Imgbytes);
    //     MemoryStream oAttachment = ShowGeneralExportReportForMail("Reports,Academic,rptStudentConfirmReport.rpt", "@P_IDNO=5050");



    //     var bytesRpt = oAttachment.ToArray(); 
    //     var fileRpt = Convert.ToBase64String(bytesRpt);



    //     try
    //     {
    //         Common objCommon = new Common();
    //         DataSet dsconfig = null;
    //         dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

    //         var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString(), "MNR");
    //         var toAddress = new MailAddress(toEmailId, "");
    //         var apiKey = "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";
    //         var client = new SendGridClient(apiKey);
    //         var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString(), "MNR");
    //         var subject = "Your OTP for Certificate Registration.";
    //         var to = new EmailAddress(toEmailId, "");
    //         var plainTextContent = "";
    //         var htmlContent = Message;



    //         var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
    //         var attachments = new List<SendGrid.Helpers.Mail.Attachment>();
    //         var attachment = new SendGrid.Helpers.Mail.Attachment()
    //         {
    //             Content = Imgfile,
    //             Type = "image/png",
    //             Filename = "Logo.png",
    //             Disposition = "inline",
    //             ContentId = "Logo"
    //         };
    //         attachments.Add(attachment);
    //         msg.AddAttachments(attachments);



    //         msg.AddAttachment("rptStudentConfirmReport.pdf", fileRpt);



    //         var response = await client.SendEmailAsync(msg);
    //     }
    //     catch (Exception ex)
    //     {



    //     }
    // }

    /// <summary>
    /// Generate HASH for encrypt all parameter passing while transaction
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public string Generatehash512(string text)
    {

        byte[] message = Encoding.UTF8.GetBytes(text);

        UnicodeEncoding UE = new UnicodeEncoding();
        byte[] hashValue;
        SHA512Managed hashString = new SHA512Managed();
        string hex = "";
        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }

    public void GetPGRespnseData(string[] parameters)
    {
        try
        {
            string[] strGetMerchantParamForCompare;

            string strPG_ClintTxnRefNo = string.Empty;
            string strPG_TPSLTxnBankCode = string.Empty;
            string strPG_TPSLTxnID = string.Empty;
            string strPG_TxnAmount = string.Empty;
            string strPG_TxnDateTime = string.Empty;
            string strPG_TxnDate = string.Empty;
            string strPG_TxnTime = string.Empty;
            string payment_mode = string.Empty;
            string card_name = string.Empty;
            string rec_code = string.Empty;

            for (int i = 0; i < parameters.Length; i++)
            {
                strGetMerchantParamForCompare = parameters[i].ToString().Split('=');
                if (Convert.ToString(strGetMerchantParamForCompare[0]).Trim() == "order_id")
                {
                    strPG_TxnStatus = Convert.ToString(strGetMerchantParamForCompare[1]);
                    //lblTXN_STATUS.Text = strPG_TxnStatus;
                    lblTPSL_ORDER_TXN_ID.Text = strPG_TxnStatus;
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).Trim() == "tracking_id")
                {
                    strPG_ClintTxnRefNo = Convert.ToString(strGetMerchantParamForCompare[1]);
                    lblCLNT_TrackingTXN_REF.Text = strPG_ClintTxnRefNo;
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).Trim() == "bank_ref_no")
                {
                    strPG_TPSLTxnBankCode = Convert.ToString(strGetMerchantParamForCompare[1]);
                    lblTPSL_BANKREFNO_CD.Text = strPG_TPSLTxnBankCode;
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).Trim() == "order_status")
                {
                    strPG_TPSLTxnID = Convert.ToString(strGetMerchantParamForCompare[1]);
                    //lblTPSL_TXN_ID.Text = strPG_TPSLTxnID;
                    lblTXN_STATUS.Text = strPG_TPSLTxnID;
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).Trim() == "mer_amount")
                {
                    strPG_TxnAmount = Convert.ToString(strGetMerchantParamForCompare[1]);
                    lblTXN_AMT.Text = strPG_TxnAmount;
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).Trim() == "payment_mode")
                {
                    payment_mode = Convert.ToString(strGetMerchantParamForCompare[1]);
                    lblPaymentMode.Text = payment_mode;
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).Trim() == "card_name")
                {
                    card_name = Convert.ToString(strGetMerchantParamForCompare[1]);
                    lblCardName.Text = card_name;
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).Trim() == "merchant_param1")
                {
                    ViewState["IDNO"] = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).Trim() == "merchant_param2")
                {
                    ViewState["SESSIONNO"] = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).Trim() == "merchant_param3")
                {
                    rec_code = Convert.ToString(strGetMerchantParamForCompare[1]);
                    lblRec_Code.Text = rec_code;
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).Trim() == "billing_name")
                {
                    ViewState["STUDNAME"] = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).Trim() == "merchant_param5")
                {
                    ViewState["LATEFEE"] = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).Trim() == "trans_date")
                {
                    strPG_TxnDateTime = Convert.ToString(strGetMerchantParamForCompare[1]);
                    strArrPG_TxnDateTime = strPG_TxnDateTime.Split(' ');
                    strPG_TxnDate = Convert.ToString(strArrPG_TxnDateTime[0]);
                    strPG_TxnTime = Convert.ToString(strArrPG_TxnDateTime[1]);
                    lblDatetime.Text = strPG_TxnDate + " " + strPG_TxnTime;
                    txt_date = strPG_TxnDate;
                }
            }
        }
        catch (Exception ex)
        { }
    }

    #endregion "CC AVENUE Payment Response"



    protected void lbtnGoBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["ORDERID"] != null)
            {

                InitializeSession();
                string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["ORDERID"] + "'");
                if (rec_code == "TF" || rec_code == "HF" || rec_code == "TPF")
                {
                    int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "AL_NO", "AL_URL = 'ACADEMIC/Feespayment.aspx' AND ACTIVE_STATUS=1"));
                    Response.Redirect("Feespayment.aspx?pageno=" + pageno + "");//2541 in test

                    //int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "AL_NO", "AL_URL = 'Academic/Feespayment.aspx'"));
                    //Response.Redirect("Feespayment.aspx?pageno=" + pageno + "");//2480 in test
                }

            }
        }
        catch { }
    }

    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }

    public string GetMACAddress()
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

    public void InitializeSession()
    {
        // int IDNO = Convert.ToInt32(Request.QueryString["IDNO"].ToString());
        //int IDNO = Convert.ToInt32(Session["IDNO"]);
        int IDNO = Convert.ToInt32(ViewState["IDNO"]);
        //Session["colcode"] = 50;
        int userno = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_NO", "UA_IDNO = " + IDNO + ""));

        //Get Common Details
        SqlDataReader dr = objCommon.GetCommonDetails();
        if (dr != null)
        {
            if (dr.Read())
            {
                Session["coll_name"] = dr["CollegeName"].ToString();
            }
        }

        User_AccController objUC = new User_AccController();
        UserAcc objUA = objUC.GetSingleRecordByUANo(userno);

        DataSet ds = objCommon.FillDropDown("ACD_ACCESS_MASTER A INNER JOIN ACD_MACHINE_TYPE_MASTER B ON (B.MACTYPENO=A.MACTYPENO AND B.COLLEGE_CODE=A.COLLEGE_CODE)", "A.MACADD", "B.MACTYPE_STATUS", "A.UA_NO=" + objUA.UA_No + "", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToInt32(ds.Tables[0].Rows[0][1]) == 0)
            {
                Session["USER_MAC"] = Convert.ToString(ds.Tables[0].Rows[0][0]);
            }
            else
            {
                Session["USER_MAC"] = "0";
            }
        }

        Session["userno"] = objUA.UA_No.ToString();
        Session["idno"] = objUA.UA_IDNo.ToString();
        Session["username"] = objUA.UA_Name;
        Session["usertype"] = objUA.UA_Type;
        Session["userfullname"] = objUA.UA_FullName;
        Session["dec"] = objUA.UA_Dec.ToString();
        Session["userdeptno"] = objUA.UA_DeptNo.ToString();
        DataSet dsReff = objCommon.FillDropDown("REFF", "*", "", string.Empty, string.Empty);
        Session["colcode"] = dsReff.Tables[0].Rows[0]["COLLEGE_CODE"].ToString(); //Added by Irfan Shaikh on 20190424
        Session["currentsession"] = objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO)", "SESSIONNO>0 AND FLOCK=1");
        Session["firstlog"] = objUA.UA_FirstLogin;
        Session["ua_status"] = objUA.UA_Status;
        Session["ua_section"] = objUA.UA_section.ToString();
        Session["UA_DESIG"] = objUA.UA_Desig.ToString();
        string ipAddress = Request.ServerVariables["REMOTE_HOST"];
        Session["ipAddress"] = ipAddress;
        string macAddress = GetMACAddress();
        Session["macAddress"] = macAddress;

        int retLogID = LogTableController.AddtoLog(Session["username"].ToString(), Session["ipAddress"].ToString(), Session["macAddress"].ToString(), DateTime.Now);
        Session["logid"] = retLogID + 1;
        Session["loginid"] = retLogID.ToString();

        string lastlogout = string.Empty;
        string lastloginid = objCommon.LookUp("LOGFILE", "MAX(ID)", "UA_NAME='" + Session["username"].ToString() + "' AND UA_NAME IS NOT NULL");
        Session["lastloginid"] = lastloginid.ToString();
        if (Session["lastloginid"].ToString() != string.Empty)
        {
            lastlogout = objCommon.LookUp("LOGFILE", "LOGOUTTIME", "ID=" + Convert.ToInt32(Session["lastloginid"].ToString()));
        }

        Session["sessionname"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "FLOCK=1");
        Session["hostel_session"] = objCommon.LookUp("ACD_HOSTEL_SESSION", "MAX(HOSTEL_SESSION_NO)", "FLOCK=1");
        Session["WorkingDate"] = DateTime.Now.ToString();
        Session["college_nos"] = objUA.COLLEGE_CODE;
        Session["Session"] = Session["sessionname"].ToString();


    }

    //GENERATE REPORT AFTER ONLINE PAYMENT DONE SUCCESSFULLY. 
    private void ShowReport_NEW(string reportTitle, string rptFileName, string rec_code)
    {
        try
        {
            InitializeSession();
            int IDNO = Convert.ToInt32(ViewState["IDNO"]);
            ViewState["COLLEGE_ID"] = objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + IDNO + "");
            //string col = Session["colcode"].ToString();
            //string userno = Session["userno"].ToString();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //  string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //@P_REVAL_TYPE = 1 for Photo copy AND 2 for reval
            url += "&param=@P_ORDER_ID=" + ViewState["ORDERID"] + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_COLLEGE_ID=" + ViewState["COLLEGE_ID"].ToString() + "";
            //url += "&param=@P_ORDER_ID=" + ViewState["Orderid"] + ",@P_REVAL_TYPE=1,@P_COLLEGE_CODE=50";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PhotoReval_Response.ShowReport_NEW() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //THIS IS FOR ONLINE PAYMENT FEES REPORT 27 MARCH 2020 BY PANKAJ NAKHALE
    protected void btnReports_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["ORDERID"] != null)
            {
                string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["ORDERID"] + "'");
                if (rec_code == "TF" || rec_code == "HF" || rec_code == "TPF")
                {
                    this.ShowReport_NEW("Online Fees Payment", "PaymentReceiptInstallment.rpt", rec_code);//1 for photo copy
                }
            }
        }
        catch { }
    }

    public string getRemoteAddr()
    {
        string UserIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (UserIPAddress == null)
        {
            UserIPAddress = Request.ServerVariables["REMOTE_ADDR"];
        }
        return UserIPAddress;
    }


    private void ShowReport(string reportTitle, string rptFileName, string rec_code, int reval_type)
    {
        try
        {
            InitializeSession();

            int SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "SESSIONNO", "ORDER_ID = " + ViewState["ORDERID"] + " AND ISNULL(CAN,0)=0 AND RECIEPT_CODE='" + rec_code + "'"));
            int idno = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "IDNO", "ORDER_ID = " + ViewState["ORDERID"] + " AND ISNULL(CAN,0)=0 AND RECIEPT_CODE='" + rec_code + "'"));

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            //@P_REVAL_TYPE = 1 for Photo copy AND 2 for reval
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(idno) + ",@P_SESSIONNO=" + Convert.ToInt32(SessionNo) + ",@P_REVAL_TYPE=" + reval_type + "";

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PhotoReval_Response.ShowReport_NEW() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnRegistrationSlip_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["ORDERID"] != null)
            {
                string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = " + ViewState["ORDERID"]);
                if (rec_code == "PRF")
                {
                    ShowReport("Photo Copy Registration Slip", "rptPhotoRevaluation.rpt", rec_code, 1); //1 for photo copy
                }
                else
                {
                    ShowReport("Photo Copy Registration Slip", "rptPhotoRevaluation.rpt", rec_code, 2); //2 for reval
                }
            }
        }
        catch { }
    }



    //// ----  BILl Desk Payment Response 
    //public void CheckResponseFromBillDesk(string[] BDResponse)
    //{
    //    #region Declaration for Response processing
    //    //string urlPath = ConfigurationManager.AppSettings["PaymentPath"];
    //    string ErrorMessage = string.Empty;
    //    #endregion

    //    #region BillDesk Response Data
    //    string MerchantID = BDResponse[0].Replace('+', ' ');
    //    string UniTranNo = BDResponse[1].Replace('+', ' ');
    //    string TxnReferenceNo = BDResponse[2].Replace('+', ' ');
    //    string BankReferenceNo = BDResponse[3].Replace('+', ' ');
    //    string txn_amount = BDResponse[4].Replace('+', ' ');
    //    string BankID = BDResponse[5].Replace('+', ' ');
    //    string BankMerchantID = BDResponse[6].Replace('+', ' ');
    //    string TxnType = BDResponse[7].Replace('+', ' ');
    //    string CurrencyType = BDResponse[8].Replace('+', ' ');
    //    string ItemCode = BDResponse[9].Replace('+', ' ');
    //    string SecurityType = BDResponse[10].Replace('+', ' ');
    //    string SecurityID = BDResponse[11].Replace('+', ' ');
    //    string SecurityPasswod = BDResponse[12].Replace('+', ' ');
    //    string TxnDate = BDResponse[13].Replace('+', ' ');
    //    string AuthStatus = BDResponse[14].Replace('+', ' ');
    //    string SettlementType = BDResponse[15].Replace('+', ' ');
    //    string additional_info1 = BDResponse[16].Replace('+', ' ');
    //    string additional_info2 = BDResponse[17].Replace('+', ' ');
    //    string additional_info3 = BDResponse[18].Replace('+', ' ');
    //    string additional_info4 = BDResponse[19].Replace('+', ' ');
    //    string additional_info5 = BDResponse[20].Replace('+', ' ');
    //    string additional_info6 = BDResponse[21].Replace('+', ' ');
    //    string additional_info7 = BDResponse[22].Replace('+', ' ');
    //    string ErrorStatus = BDResponse[23].Replace('+', ' ');
    //    string errorDescription = BDResponse[24].Replace('+', ' ');
    //    String Checksum = BDResponse[25].Replace('+', ' ');
    //    #endregion

    //    #region Generate Bill Desk Check Sum
    //    StringBuilder billRequest = new StringBuilder();
    //    billRequest.Append(MerchantID).Append("|");
    //    billRequest.Append(UniTranNo).Append("|");
    //    billRequest.Append(TxnReferenceNo).Append("|");
    //    billRequest.Append(BankReferenceNo).Append("|");
    //    billRequest.Append(txn_amount).Append("|");
    //    billRequest.Append(BankID).Append("|");
    //    billRequest.Append(BankMerchantID).Append("|");
    //    billRequest.Append(TxnType).Append("|");
    //    billRequest.Append(CurrencyType).Append("|");
    //    billRequest.Append(ItemCode).Append("|");
    //    billRequest.Append(SecurityType).Append("|");
    //    billRequest.Append(SecurityID).Append("|");
    //    billRequest.Append(SecurityPasswod).Append("|");
    //    billRequest.Append(TxnDate).Append("|");
    //    billRequest.Append(AuthStatus).Append("|");
    //    billRequest.Append(SettlementType).Append("|");
    //    billRequest.Append(additional_info1).Append("|");
    //    billRequest.Append(additional_info2).Append("|");
    //    billRequest.Append(additional_info3).Append("|");
    //    billRequest.Append(additional_info4).Append("|");
    //    billRequest.Append(additional_info5).Append("|");
    //    billRequest.Append(additional_info6).Append("|");
    //    billRequest.Append(additional_info7).Append("|");
    //    billRequest.Append(ErrorStatus).Append("|");
    //    billRequest.Append(errorDescription);

    //    string data = billRequest.ToString();
    //    lblmessage.Text = data;
    //    ViewState["Orderid"] = UniTranNo;
    //    ViewState["IDNO"] = additional_info2;
    //    string ChecksumKey = "gpOT4FkIAejC";
    //    String hash = String.Empty;
    //    hash = GetHMACSHA256(data, ChecksumKey);
    //    hash = hash.ToUpper();

    //    #endregion

    //    #region Payment Transaction Update

    //    string txnMessage = string.Empty;
    //    string txnStatus = string.Empty;
    //    string txnMode = string.Empty;

    //    if (hash == Checksum)
    //    {
    //        #region Get Transaction Details
    //        if (AuthStatus == "0300")
    //        {
    //            txnMessage = "Successful Transaction";
    //            txnStatus = "Success";
    //        }
    //        else if (AuthStatus == "0399")
    //        {
    //            txnMessage = "Cancel Transaction";
    //            txnStatus = "Invalid Authentication at Bank";
    //        }
    //        else if (AuthStatus == "NA")
    //        {
    //            txnMessage = "Cancel Transaction";
    //            txnStatus = "Invalid Input in the Request Message";
    //        }
    //        else if (AuthStatus == "0002")
    //        {
    //            txnMessage = "Cancel Transaction";
    //            txnStatus = "BillDesk is waiting for Response from Bank";
    //        }
    //        else if (AuthStatus == "0001")
    //        {
    //            txnMessage = "Cancel Transaction";
    //            txnStatus = "Error at BillDesk";
    //        }
    //        else
    //        {
    //            txnMessage = "Something went wrong. Try Again!.";
    //            txnStatus = "Payment Faild";
    //        }
    //        #endregion

    //        #region Transaction Type
    //        if (TxnType == "01")
    //            txnMode = "Netbanking";
    //        else if (TxnType == "02")
    //            txnMode = "Credit Card";
    //        else if (TxnType == "03")
    //            txnMode = "Debit Card";
    //        else if (TxnType == "04")
    //            txnMode = "Cash Card";
    //        else if (TxnType == "05")
    //            txnMode = "Mobile Wallet";
    //        else if (TxnType == "06")
    //            txnMode = "IMPS";
    //        else if (TxnType == "07")
    //            txnMode = "Reward Points";
    //        else if (TxnType == "08")
    //            txnMode = "Rupay";
    //        #endregion

    //        #region Assign Values to objEntity
    //        string TxRefNo = TxnReferenceNo;
    //        string PgTxnNo = BankReferenceNo;
    //        decimal TxnAmount = Convert.ToDecimal(txn_amount);
    //        string TxStatus = txnStatus;
    //        string TxMssg = txnMessage;
    //        string TransactionType = "Online";
    //        string PaymentFor = TxnType;
    //        #endregion


    //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
    //        if (con.State == ConnectionState.Open)
    //        {
    //            con.Close();
    //        }
    //        con.Open();
    //        SqlCommand cmd = new SqlCommand("Insert into Test (Name) values ('" + lblmessage.Text.ToString() + "')", con);
    //        cmd.Connection = con;

    //        cmd.ExecuteNonQuery();
    //        con.Close();

    //        if (txnStatus == "Success")
    //        {
    //            Session["ResponseMsg"] = "Payment success.";

    //            if (ViewState["Orderid"] != null)
    //            {
    //                int result = 0;
    //                string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID ='" + (ViewState["Orderid"]) + "'");//ViewState["Orderid"]
    //                if (rec_code == "TF" || rec_code == "HF" || rec_code == "TPF")
    //                {
    //                    result = feeController.OnlineInstallmentFeesPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, rec_code);
    //                }
    //                else//"RF"
    //                {
    //                    result = feeController.OnlineInstallmentFeesPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, rec_code);
    //                }

    //                btnReports.Visible = true;
    //                btnRegistrationSlip.Visible = false ;

    //                lblmessage.Visible = true;
    //                lblmessage.ForeColor = System.Drawing.Color.Green; lblmessage.Font.Bold = true;
    //                lblmessage.Text = "Hello " + additional_info1 + " ! <br /> We have processed Payment of  Rs." + txn_amount + " successfully. <br/> <br/> Transaction ID : " + BankReferenceNo + ".<br/> Thank You";
    //                // SuccessMessage = "Hello " + udf1 + " ! , We have processed Payment of  Rs." + Amount + " successfully. Transaction ID : " + bank_txn + ". Thank You";
    //                lbtnGoBack.Visible = true;
    //            }

    //        }
    //        else
    //        {
    //            Session["ResponseMsg"] = "Please try again.";
    //            if (ViewState["Orderid"] != null)
    //            {
    //                int result = 0;
    //                string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" +(ViewState["Orderid"])+"'");

    //                if (rec_code == "TF" || rec_code == "HF" || rec_code == "TPF")
    //                {
    //                    result = feeController.OnlineInstallmentFeesPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, rec_code);  //1 for photo copy
    //                }
    //                else//"RF"
    //                {
    //                    result = feeController.OnlineInstallmentFeesPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, rec_code);
    //                }

    //                //btnReports.Visible = true;
    //                //btnRegistrationSlip.Visible = true;
    //                btnReports.Visible = false;
    //                btnRegistrationSlip.Visible = false;

    //                lblmessage.Visible = true;
    //                lblmessage.ForeColor = System.Drawing.Color.Red; lblmessage.Font.Bold = true;
    //                lblmessage.Text = "Hello " + additional_info1 + " ! <br /> We are Unable To Process Payment of  Rs." + txn_amount + ".<br/> <br/>  Due to Reason : ' NA'.  <br/> <br/> Transaction ID : " + BankReferenceNo + ".<br/> Thank You";
    //                lbtnGoBack.Visible = true;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        //txnMessage = "Something went wrong. Try Again!.";
    //        //txnStatus = "Payment Faild";
    //        //Session["ResponseMsg"] = "Something went wrong. Please try again.";
    //        lblmessage.Visible = true;
    //        lblmessage.ForeColor = System.Drawing.Color.Red; lblmessage.Font.Bold = true;
    //        lblmessage.Text = "Something went wrong. Try Again!.";
    //    }

    //    #endregion
    //}

    //public string GetHMACSHA256(string text, string key)
    //{
    //    UTF8Encoding encoder = new UTF8Encoding();

    //    byte[] hashValue;
    //    byte[] keybyt = encoder.GetBytes(key);
    //    byte[] message = encoder.GetBytes(text);

    //    HMACSHA256 hashString = new HMACSHA256(keybyt);
    //    string hex = "";

    //    hashValue = hashString.ComputeHash(message);
    //    foreach (byte x in hashValue)
    //    {
    //        hex += String.Format("{0:x2}", x);
    //    }
    //    return hex;
    //}
    //// ----  BILl Desk Payment Response 

}
