using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
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
using System.Security.Cryptography.X509Certificates;
using System.Data.Sql;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Net.Mail;
using Newtonsoft.Json;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


public partial class PhotoReval_Response : System.Web.UI.Page
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
                ////btnReports.Visible = true;
                ////btnRegistrationSlip.Visible = true;
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
                //if (con.State == ConnectionState.Open)
                //{
                //    con.Close();
                //}
                //con.Open();
                //string res = "PHOTORES";
                //SqlCommand cmd = new SqlCommand("Insert into Test (Name,Response) values ('" + Convert.ToString(HttpContext.Current.Request["msg"]) + "','" + res + "')", con);
                //cmd.Connection = con;
                //cmd.ExecuteNonQuery();
                //con.Close();

                //string[] responseArrary = Convert.ToString(HttpContext.Current.Request["msg"]).Split('|');

                // //string[] responseArrary = Convert.ToString("SVCE2|273857134|SHMP8655201768|218097|1.00|HMP|510372|03|INR|MDDIRECT|02-NA|NA|00000000.00|26-03-2020 12:25:03|0300|NA|ADITHYA  R|2738|CF|CF170501003,57,5,9486246104|1|NA|57|NA|PGS10001-Success|CE80567379DBFBED8442ECF5FD7B86D07381D49023A0D0927883DCA879638D69").Split('|'); 
                ////string[] responseArrary = Convert.ToString("SVCE2|27385790953|SPMP8654983858|NA|1.00|PMP|607093|03|INR|RDDIRECT|NA|NA|0.00|26-03-2020 11:23:01|0399|NA|ADITHYA  R|2738|CF|CF170501003,57,5,9486246104|1|NA|57|NA|PME10014-Connection timedout|ACBD05C74D7E8E6EB4F8D905C8AC549E4B85165F80F8DA7966FAEF2EFD1B62D3
                ////string[] responseArrary = Convert.ToString("SVCE2|172057403|SHMP8634441559|993142|1.00|HMP|510372|03|INR|MDDIRECT|02-NA|NA|00000000.00|19-03-2020 10:46:48|0300|NA|BALACHANDAR M|1662|AEF|AEF180301010,58,3,9444208734|1|NA|58|NA|PGS10001-Success|gpOT4FkIAejC").Split('|');
            
                //CheckResponseFromBillDesk(responseArrary);

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

            //string encrypteddata = ccaCrypto.Encrypt("order_id=50681727&tracking_id=110145085467&bank_ref_no=704213&order_status=Success&failure_message=&payment_mode=Debit Card&card_name=MasterCard Debit Card&status_code=null&status_message=SUCCESS&currency=INR&amount=1.00&billing_name=PANDE KISHORE SAGAR&billing_address=&billing_city=&billing_state=&billing_zip=&billing_country=&billing_tel=&billing_email=&delivery_name=&delivery_address=&delivery_city=&delivery_state=&delivery_zip=&delivery_country=&delivery_tel=&merchant_param1=5068&merchant_param2=56&merchant_param3=RF&merchant_param4=T/BDS/21/018,T/BDS/21/018,BDS,1,0987456321&merchant_param5=0&vault=N&offer_type=null&offer_code=null&discount_value=0.0&mer_amount=1.00&eci_value=null&retry=N&response_code=0&billing_notes=&trans_date=26/04/2021 16:34:43&bin_country=INDIA&trans_fee=21.0&service_tax=0.0", workingKey);
            //string encResponse = ccaCrypto.Decrypt(encrypteddata, workingKey);

            //string encrypteddata = ccaCrypto.Encrypt("order_id=50688629&tracking_id=110143401582&bank_ref_no=986977&order_status=Success&failure_message=&payment_mode=Debit Card&card_name=MasterCard Debit Card&status_code=null&status_message=SUCCESS&currency=INR&amount=1.00&billing_name=PANDE KISHORE SAGAR&billing_address=&billing_city=&billing_state=&billing_zip=&billing_country=&billing_tel=&billing_email=&delivery_name=&delivery_address=&delivery_city=&delivery_state=&delivery_zip=&delivery_country=&delivery_tel=&merchant_param1=5068&merchant_param2=56&merchant_param3=RF&merchant_param4=T/BDS/21/018,T/BDS/21/018,BDS,1,0987456321&merchant_param5=0&vault=N&offer_type=null&offer_code=null&discount_value=0.0&mer_amount=1.00&eci_value=null&retry=N&response_code=0&billing_notes=&trans_date=24/04/2021 13:05:36&bin_country=INDIA&trans_fee=21.0&service_tax=0.0", workingKey);
            //string encResponse = ccaCrypto.Decrypt(encrypteddata, workingKey);

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
            string res = "RF,EF - Normal Response";
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

            //to get order id from dcr
            string order_id = objCommon.LookUp("ACD_DCR", "ISNULL(ORDER_ID,'') ORDER_ID", "IDNO=" + ViewState["IDNO"].ToString() + " AND RECIEPT_CODE='" + lblRec_Code.Text + "' AND SESSIONNO=" + ViewState["SESSIONNO"].ToString() + " AND ORDER_ID= '" + lblTPSL_ORDER_TXN_ID.Text + "'");
            //to get order amt from dcr
            string order_Amount = objCommon.LookUp("ACD_DCR", "ISNULL(SUM(TOTAL_AMT),0) TOTAL_AMT", "IDNO=" + ViewState["IDNO"].ToString() + " AND RECIEPT_CODE='" + lblRec_Code.Text + "' AND SESSIONNO=" + ViewState["SESSIONNO"].ToString() + " AND ORDER_ID= '" + lblTPSL_ORDER_TXN_ID.Text + "'");

            if (lblTPSL_ORDER_TXN_ID.Text.Trim() == order_id.Trim())
            {
                if (lblTXN_AMT.Text.Trim() == order_Amount.Trim())
                ////if (lblTXN_AMT.Text.Trim() == "1.00")
                {
                    if (lblTXN_STATUS.Text == "Success")
                    {
                        LiteralMessage.Text = "<H3>Your Transaction Done Successfully.<br>Please Note Down Transaction Details For Future Reference.</h3><br> <table width='100%'><tr width='100%'><td align='left' width='50%' style='padding-left:122px;'>ORDER ID</td><td align='left' width='50%' style='color:black; padding-left: 69px;'>" + lblTPSL_ORDER_TXN_ID.Text + "</td></tr><tr width='100%'><td align='left' width='50%' style='padding-left:122px;'>Transaction Id</td><td align='left' width='50%' style='color:black; padding-left: 69px;'>" + lblCLNT_TrackingTXN_REF.Text + "</td></tr><tr width='100%'><td align='left' width='50%'  style='padding-left:122px;'>Total Amount</td><td align='left' width='50%' style='color:black; padding-left: 69px;'>" + lblTXN_AMT.Text + "</td></tr><tr width='100%'><td align='left' width='50%' style='padding-left:122px;'>Transaction Status Code</td><td align='left' width='50%' style='color:black; padding-left: 69px;'>" + lblTXN_STATUS.Text + "</td></tr><tr width='100%'><td align='left' width='50%'style='padding-left:122px;'>Status</td><td align='left' width='50%' style='color:green; padding-left: 69px;'>Success</td></tr></table>";
                        lblStatus.Text = "Your Transaction Completed Successfully."; // +strPG_TxnStatus;

                        btnReports.Visible = true;
                        btnRegistrationSlip.Visible = true;
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
                    string res1 = ViewState["RECIEPT_CODE"].ToString() + " - Normal Response";
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
                }//if amount match
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


            }//if  order id match
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
            msg.Subject = "Fee Payment Status";
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




    #region "Bill desk Code"
    // ----  BILl Desk Payment Response 
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
    //    //string cc="94667B7E9D0A4F0329B5BAA119D9F4E0126635AC4725C58B5671C0FE4E03925D";
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


    //    //SqlConnection con4 = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
    //    //if (con4.State == ConnectionState.Open)
    //    //{
    //    //    con4.Close();
    //    //}
    //    //con4.Open();
    //    //string res4 = "OHash";
    //    //SqlCommand cmd4 = new SqlCommand("Insert into Test (Name,Response) values ('" + Convert.ToString(hash) + "','" + res4 + "')", con4);
    //    //cmd4.Connection = con4;
    //    //cmd4.ExecuteNonQuery();
    //    //con4.Close();


    //    //SqlConnection con5 = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
    //    //if (con5.State == ConnectionState.Open)
    //    //{
    //    //    con5.Close();
    //    //}
    //    //con5.Open();
    //    //string res5 = "OChecksum";
    //    //SqlCommand cmd5 = new SqlCommand("Insert into Test (Name,Response) values ('" + Convert.ToString(Checksum) + "','" + res5 + "')", con5);
    //    //cmd5.Connection = con5;
    //    //cmd5.ExecuteNonQuery();
    //    //con5.Close();


    //    //SqlConnection con6 = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
    //    //if (con6.State == ConnectionState.Open)
    //    //{
    //    //    con6.Close();
    //    //}
    //    //con6.Open();
    //    //string res6 = "OLabelValue";
    //    //SqlCommand cmd6 = new SqlCommand("Insert into Test (Name,Response) values ('" + Convert.ToString(lblmessage.Text) + "','" + res6 + "')", con6);
    //    //cmd6.Connection = con6;
    //    //cmd6.ExecuteNonQuery();
    //    //con6.Close();


    //    if (hash == Checksum)//if (hash == cc)
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
    //        string res = "R";
    //        SqlCommand cmd = new SqlCommand("Insert into Test (Name,Response) values ('" + lblmessage.Text.ToString() + "','" + res + "')", con);
    //        cmd.Connection = con;
    //        cmd.ExecuteNonQuery();
    //        con.Close();

    //        if (txnStatus == "Success")
    //        {
    //            Session["ResponseMsg"] = "Payment success.";

    //            if (ViewState["Orderid"] != null)
    //            {
    //                int result = 0;
    //                string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["Orderid"] +"'");

    //                if (rec_code == "PRF")
    //                {
    //                    result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 1); //1 for photo copy
    //                }
    //                else if (rec_code == "RF")//"RF"
    //                {
    //                    result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 2); //2 for reval
    //                }
    //                else if (rec_code == "AEF")//"AEF"
    //                {
    //                    result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 3); //3 for arrear exam
    //                }
    //                else if (rec_code == "CF")//"CF"
    //                {
    //                    result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 4); //4 for Condonation
    //                }

    //                btnReports.Visible = true;
    //                btnRegistrationSlip.Visible = true;

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
    //                string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["Orderid"] + "'");

    //                if (rec_code == "PRF")
    //                {
    //                    result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 1);  //1 for photo copy
    //                }
    //                else if (rec_code == "RF")//"RF"
    //                {
    //                    result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 2);  //2 for reval
    //                }
    //                else if (rec_code == "AEF")//"AEF"
    //                {
    //                    result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 3);  //3 for arrear
    //                }
    //                else if (rec_code == "CF")//"CF"
    //                {
    //                    result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 4);  //4 for Condonation
    //                }
    //                //btnReports.Visible = true;
    //                //btnRegistrationSlip.Visible = true;
    //                btnReports.Visible = false;
    //                btnRegistrationSlip.Visible = false;

    //                lblmessage.Visible = true;
    //                lblmessage.ForeColor = System.Drawing.Color.Red; lblmessage.Font.Bold = true;
    //                lblmessage.Text = "Hello " + additional_info1 + " ! <br /> We are Unable To Process Payment of  Rs." + txn_amount + ".<br/> <br/>  Due to Reason : ' NA'.  <br/> <br/> Transaction ID : " + BankReferenceNo + ".<br/> Thank You";
    //                lbtnGoBack.Visible = true;
    //                //try
    //                //{
    //                //    if (AuthStatus == "0002")
    //                //    {
    //                //        doubleVerification();
    //                //    }
    //                //    else if (txnMessage == "Cancel Transaction")
    //                //    {
    //                //        doubleVerification();
    //                //    }
    //                //}
    //                //catch { }
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
    
    //public void doubleVerification()
    //{
    //    try
    //    {
    //        String data = "0122|SVCE2" + "|" + ViewState["Orderid"] + "|" + DateTime.Now.ToString("yyyyMMddhhmmss");
    //        //string data = "0122|SVCE2|" + ViewState["Orderid"] + "|SHMP8634441559|993142|1.00|HMP|510372|03|INR|MDDIRECT|02-NA|NA|00000000.00|19-03-2020 10:46:48|0300|NA|BALACHANDAR M|1662|AEF|AEF180301010,58,3,9444208734|1|NA|58|NA|PGS10001-Success|gpOT4FkIAejC";
    //        string ChecksumKey = "gpOT4FkIAejC";
    //        String hash = String.Empty;
    //        hash = GetHMACSHA256(data, ChecksumKey);
    //        hash = hash.ToUpper();
    //        string msg = data + "|" + hash;

    //        //TO ENABLE SECURE CONNECTION SSL/TLS
    //        ServicePointManager.Expect100Continue = true;
    //        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; 

    //        var request = (HttpWebRequest)WebRequest.Create("https://www.billdesk.com/pgidsk/PGIQueryController?msg=" + msg);
    //        var response = (HttpWebResponse)request.GetResponse();
    //        Stream dataStream = response.GetResponseStream();
    //        StreamReader reader = new StreamReader(dataStream);
    //        string strResponse = reader.ReadToEnd();
    //        //lblNote1.Text = strResponse;
    //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
    //        if (con.State == ConnectionState.Open)
    //        {
    //            con.Close();
    //        }
    //        con.Open();
    //        string res = "RD";
    //        SqlCommand cmd = new SqlCommand("Insert into Test (Name,Response) values ('" + strResponse.ToString() + "','" + res + "')", con);
    //        cmd.Connection = con;
    //        cmd.ExecuteNonQuery();
    //        con.Close();
    //        //lblNote1.Text = string.Empty;
    //        string[] repoarray;
    //        repoarray = strResponse.Split('|');

    //        string authstatus = repoarray[15].ToString();
    //        string txnid1 = repoarray[2].ToString();
    //        string amount1 = repoarray[5].ToString();
    //        string apitransid = repoarray[3].ToString();
    //        string BankReferenceNo = repoarray[4].ToString();
    //        string TxnType = repoarray[8].ToString();
    //        string receipt = repoarray[19].ToString();
    //        //string status_msg = repoarray[25].ToString();

    //        string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["Orderid"]+"'");
    //        //string status = repoarray[15].ToString();
    //        //string status_msg = repoarray[25].ToString();
    //        //string msgR = repoarray[32].ToString();
    //        string txnMessage = string.Empty;
    //        string txnStatus = string.Empty;
    //        #region Get Transaction Details
    //        if (authstatus == "0300")
    //        {
    //            txnMessage = "Successful Transaction";
    //            txnStatus = "Success";
    //        }
    //        else if (authstatus == "0399")
    //        {
    //            txnMessage = "Cancel Transaction";
    //            txnStatus = "Invalid Authentication at Bank";
    //        }
    //        else if (authstatus == "NA")
    //        {
    //            txnMessage = "Cancel Transaction";
    //            txnStatus = "Invalid Input in the Request Message";
    //        }
    //        else if (authstatus == "0002")
    //        {
    //            txnMessage = "Cancel Transaction";
    //            txnStatus = "BillDesk is waiting for Response from Bank";
    //        }
    //        else if (authstatus == "0001")
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

    //        FeeCollectionController objFeesCnt = new FeeCollectionController();
    //        if (authstatus == "0300")
    //        {
    //            int retval = 0;
    //            if (rec_code == "PRF")
    //            {
    //                retval = objFeesCnt.OnlinePaymentUpdation(apitransid, txnid1, amount1, authstatus, txnStatus, TxnType, txnMessage, BankReferenceNo, 1);

    //            }
    //            else if (rec_code == "RF")
    //            {
    //                retval = objFeesCnt.OnlinePaymentUpdation(apitransid, txnid1, amount1, authstatus, txnStatus, TxnType, txnMessage, BankReferenceNo, 2);

    //            }
    //            else if (rec_code == "AEF")
    //            {
    //                retval = objFeesCnt.OnlinePaymentUpdation(apitransid, txnid1, amount1, authstatus, txnStatus, TxnType, txnMessage, BankReferenceNo, 3);

    //            }
    //            else if (rec_code == "CF")
    //            {
    //                retval = objFeesCnt.OnlinePaymentUpdation(apitransid, txnid1, amount1, authstatus, txnStatus, TxnType, txnMessage, BankReferenceNo, 4);

    //            }
    //            //if (retval == -99)
    //            //{
    //            //    objCommon.DisplayMessage(this, "Error occured", this.Page);
    //            //}
    //        }
    //    }
    //    catch { }
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
    #endregion "Bill desk"



    protected void lbtnGoBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["RECIEPT_CODE"] != null)
            {
                InitializeSession();
                //string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["ORDERID"] + "'");
                //if (rec_code == "PRF")
                //{
                //    int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "AL_NO", "AL_URL = 'ACADEMIC/PhotoCopyRegistration.aspx'"));
                //    Response.Redirect("PhotoCopyRegistration.aspx?pageno=" + pageno + "");//2480 in test
                //}
                if (ViewState["RECIEPT_CODE"].ToString() == "RF")
                {
                    int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "AL_NO", "AL_URL = 'ACADEMIC/EXAMINATION/BacklogExamregEndSem.aspx' AND ACTIVE_STATUS=1"));
                    Response.Redirect("Examination/BacklogExamregEndSem.aspx?pageno=" + pageno + "");//2174 in test
                }
                if (ViewState["RECIEPT_CODE"].ToString() == "EF")
                {
                    int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "AL_NO", "AL_URL = 'ACADEMIC/EXAMINATION/ExamRegistration.aspx' AND ACTIVE_STATUS=1"));
                    Response.Redirect("Examination/ExamRegistration.aspx?pageno=" + pageno + "");//2729 in test
                }
                //else if (rec_code == "AEF")
                //{
                //    int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "AL_NO", "AL_URL = 'ACADEMIC/EXAMINATION/BacklogExamregEndSem.aspx' AND ACTIVE_STATUS=1"));
                //    Response.Redirect("Examination/BacklogExamregEndSem.aspx?pageno=" + pageno + "");//2174 in test
                //}
                //else if (rec_code == "CF")
                //{
                //    int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "AL_NO", "AL_URL = 'ACADEMIC/EXAMINATION/StudentCondonationFees.aspx' AND ACTIVE_STATUS=1"));
                //    Response.Redirect("Examination/StudentCondonationFees.aspx?pageno=" + pageno + "");//2541 in test
                //}
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
        int userno = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_NO", "UA_IDNO = " + IDNO + " AND UA_TYPE=2")); //for student

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
    private void ShowReport_NEW(string reportTitle, string rptFileName,string receipt_code)
    {
        try
        {
            InitializeSession();
            string college_id = objCommon.LookUp("ACD_STUDENT", "ISNULL(COLLEGE_ID,'') COLLEGE_ID", "IDNO = " + ViewState["IDNO"] + "");

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_ORDER_ID=" + ViewState["ORDERID"].ToString() + ",@P_RECEIPT_CODE=" + receipt_code + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_COLLEGE_ID=" + college_id.ToString() + "";
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

    protected void btnReports_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["RECIEPT_CODE"] != null)
            {
                //string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["ORDERID"] + "'");
                //if (ViewState["RECIEPT_CODE"].ToString() == "PRF")
                //{
                //    this.ShowReport_NEW("Photo Reval Payment_Details", "PhotoRevalPaymentReceipt.rpt", 1);//1 for photo copy
                //}
                
                if (ViewState["RECIEPT_CODE"].ToString() == "RF")
                {
                    this.ShowReport_NEW("Referred Payment Details", "rptPaymentReceipt.rpt", ViewState["RECIEPT_CODE"].ToString());//2 for referred fee
                }

                if (ViewState["RECIEPT_CODE"].ToString() == "EF")
                {
                    this.ShowReport_NEW("Exam Registration Payment Details", "rptPaymentReceipt.rpt", ViewState["RECIEPT_CODE"].ToString());//for exam reg fee
                }


                //else if (ViewState["RECIEPT_CODE"].ToString() == "AEF")
                //{
                //    this.ShowReport_NEW("Arrear Payment_Details", "PhotoRevalPaymentReceipt.rpt", 3);//3 for Arrear
                //}
                //else if (ViewState["RECIEPT_CODE"].ToString() == "CF")
                //{
                //    this.ShowReport_NEW("Condonation Payment_Details", "PhotoRevalPaymentReceipt.rpt", 4);//4 for Condonation
                //}
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
    
    private void ShowReport(string reportTitle, string rptFileName,string rec_code,int reval_type)
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

    private void ShowExamReport(string reportTitle, string rptFileName, string rec_code)
    {
        try
        {
            InitializeSession();
            int SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "SESSIONNO", "ORDER_ID = " + ViewState["ORDERID"] + " AND ISNULL(CAN,0)=0 AND RECIEPT_CODE='" + rec_code + "'"));
            int idno = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "IDNO", "ORDER_ID = " + ViewState["ORDERID"] + " AND ISNULL(CAN,0)=0 AND RECIEPT_CODE='" + rec_code + "'"));
            //int Semesterno = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "SEMESTERNO", "ORDER_ID = " + ViewState["Orderid"] + " AND ISNULL(CAN,0)=0 AND RECIEPT_CODE='" + rec_code + "'"));
            string college_id = objCommon.LookUp("ACD_STUDENT", "ISNULL(COLLEGE_ID,'') COLLEGE_ID", "IDNO = " + ViewState["IDNO"] + "");

       
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_COLLEGE_ID=" + college_id.ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + Convert.ToInt32(SessionNo) + ",@P_RECEIPT_CODE=" + rec_code + "";
           
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowAEFReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnRegistrationSlip_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["RECIEPT_CODE"] != null)
            {
                //string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["Orderid"]+"'");
                //if (rec_code == "PRF")
                //{
                //    ShowReport("Photo Copy Registration Slip", "rptPhotoRevaluation.rpt", rec_code, 1); //1 for photo copy
                //}
                if (ViewState["RECIEPT_CODE"].ToString() == "RF")
                {
                   // ShowReport("Referred Registration Slip", "rptExamRegistrationSlip.rpt", ViewState["RECIEPT_CODE"].ToString(), 2); //2 for reval
                    ShowExamReport("Referred Exam Registration Slip", "rptExamRegistrationSlip.rpt", ViewState["RECIEPT_CODE"].ToString());
                }

                if (ViewState["RECIEPT_CODE"].ToString() == "EF")
                {
                    ShowExamReport("Exam Registration Slip", "rptExamRegistrationSlip.rpt", ViewState["RECIEPT_CODE"].ToString());
                }



                //else if (rec_code == "AEF")
                //{
                //    ShowAEFReport("ExamRegistrationSlip", "PaymentReceipt_Exam_Registered_Courses.rpt", rec_code);
                //}
                //else if (rec_code == "CF")
                //{
                //    ShowReport("CondonationSlip", "StudCondonation.rpt", rec_code, 4);
                //}
            }
        }
        catch { }
    }       




}
