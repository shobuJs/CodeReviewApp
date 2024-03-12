using System;
using System.Collections.Generic;
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
using System.IO;
using System.Web;
using System.Text.RegularExpressions;

public partial class ResponseStatus : System.Web.UI.Page
{
    #region Variable Declaration
    string
          strPG_TxnStatus = string.Empty,
          strPG_TPSLTxnBankCode = string.Empty,
          strPG_TxnDateTime = string.Empty,
          strPG_TxnDate = string.Empty,
          strPG_TxnTime = string.Empty,
          strPG_TxnType = string.Empty;
    //string strPGResponse;
    //string[] strSplitDecryptedResponse;
    //string[] strArrPG_TxnDateTime;
    //string strPG_MerchantCode;
    #endregion

    Common objCommon = new Common();
    FeeCollectionController feeController = new FeeCollectionController();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string chkstring = "";
            chkstring = Convert.ToString(HttpContext.Current.Request["msg"].Trim());
            //chkstring = Convert.ToString("SVCE2|21615868063|SHMP8677955599|NA|1.00|HMP|NA|NA|INR|DIRECT|NA|NA|10.00|03-04-2020 18:13:05|0399|NA|SRIHARI  A|2161|AEF|AEF180701121,58,3,9843045936|1|NA|58|NA|Canceled By User|5EF0EE2C167918C6F4D8DDB5A38E15890D08A1D35CD3F52CE0DF496850A7C5B8");
            //chkstring = Convert.ToString("SVCE2|44295852183|SHMP8673652607|508637|1.00|HMP|510372|03|INR|MDDIRECT|02-NA|NA|00000000.00|02-04-2020 10:33:52|0300|NA|ANAND VIJAY RAJ R|4429|AEF|AEF161001009,58,8,9443270125|1|NA|58|NA|PGS10001-Success|37C13CAF0638C437395081D78F6D5D399C63A9D8D9C4506B9D26CCECFE0E922D");
            if (chkstring != "")
            {
                SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
                if (con3.State == ConnectionState.Open)
                {
                    con3.Close();
                }
                con3.Open();
                string res3 = "S";
                SqlCommand cmd3 = new SqlCommand("Insert into Test (Name,Response) values ('" + Convert.ToString(HttpContext.Current.Request["msg"].Trim()) + "','" + res3 + "')", con3);
                cmd3.Connection = con3;
                cmd3.ExecuteNonQuery();
                con3.Close();

               // string[] responseArrary = Convert.ToString(HttpContext.Current.Request["msg"]).Split('|');
                //CheckResponseFromBillDesk(responseArrary);

                string[] BDResponse = Convert.ToString(HttpContext.Current.Request["msg"].Trim()).Split('|');

                #region Declaration for Response processing
                //string urlPath = ConfigurationManager.AppSettings["PaymentPath"];
                string ErrorMessage = string.Empty;
                #endregion

                #region BillDesk Response Data
                string MerchantID = BDResponse[0].Replace('+', ' ');
                string UniTranNo = BDResponse[1].Replace('+', ' ');
                string TxnReferenceNo = BDResponse[2].Replace('+', ' ');
                string BankReferenceNo = BDResponse[3].Replace('+', ' ');
                string txn_amount = BDResponse[4].Replace('+', ' ');
                string BankID = BDResponse[5].Replace('+', ' ');
                string BankMerchantID = BDResponse[6].Replace('+', ' ');
                string TxnType = BDResponse[7].Replace('+', ' ');
                string CurrencyType = BDResponse[8].Replace('+', ' ');
                string ItemCode = BDResponse[9].Replace('+', ' ');
                string SecurityType = BDResponse[10].Replace('+', ' ');
                string SecurityID = BDResponse[11].Replace('+', ' ');
                string SecurityPasswod = BDResponse[12].Replace('+', ' ');
                string TxnDate = BDResponse[13].Replace('+', ' ');
                string AuthStatus = BDResponse[14].Replace('+', ' ');
                string SettlementType = BDResponse[15].Replace('+', ' ');
                string additional_info1 = BDResponse[16].Replace('+', ' ');
                string additional_info2 = BDResponse[17].Replace('+', ' ');
                string additional_info3 = BDResponse[18].Replace('+', ' ');
                string additional_info4 = BDResponse[19].Replace('+', ' ');
                string additional_info5 = BDResponse[20].Replace('+', ' ');
                string additional_info6 = BDResponse[21].Replace('+', ' ');
                string additional_info7 = BDResponse[22].Replace('+', ' ');
                string ErrorStatus = BDResponse[23].Replace('+', ' ');
                string errorDescription = BDResponse[24].Replace('+', ' ');
                String Checksum = BDResponse[25].Replace('+', ' ');
                #endregion

                #region Generate Bill Desk Check Sum
                StringBuilder billRequest = new StringBuilder();
                billRequest.Append(MerchantID).Append("|");
                billRequest.Append(UniTranNo).Append("|");
                billRequest.Append(TxnReferenceNo).Append("|");
                billRequest.Append(BankReferenceNo).Append("|");
                billRequest.Append(txn_amount).Append("|");
                billRequest.Append(BankID).Append("|");
                billRequest.Append(BankMerchantID).Append("|");
                billRequest.Append(TxnType).Append("|");
                billRequest.Append(CurrencyType).Append("|");
                billRequest.Append(ItemCode).Append("|");
                billRequest.Append(SecurityType).Append("|");
                billRequest.Append(SecurityID).Append("|");
                billRequest.Append(SecurityPasswod).Append("|");
                billRequest.Append(TxnDate).Append("|");
                billRequest.Append(AuthStatus).Append("|");
                billRequest.Append(SettlementType).Append("|");
                billRequest.Append(additional_info1).Append("|");
                billRequest.Append(additional_info2).Append("|");
                billRequest.Append(additional_info3).Append("|");
                billRequest.Append(additional_info4).Append("|");
                billRequest.Append(additional_info5).Append("|");
                billRequest.Append(additional_info6).Append("|");
                billRequest.Append(additional_info7).Append("|");
                billRequest.Append(ErrorStatus).Append("|");
                billRequest.Append(errorDescription);

                string data = billRequest.ToString();
                lblmessage.Text = data;
                ViewState["Orderid"] = UniTranNo;
                ViewState["IDNO"] = additional_info2;
                ViewState["RECIEPT"] = additional_info3;

                if (additional_info7 == "NA")
                {
                    ViewState["SESSIONNO"] = 0;
                }
                else
                {
                    ViewState["SESSIONNO"] = additional_info7;
                }

                string ChecksumKey = "gpOT4FkIAejC";
                String hash = String.Empty;
                hash = GetHMACSHA256(data, ChecksumKey);
                hash = hash.ToUpper();

                #endregion

                #region Payment Transaction Update

                string txnMessage = string.Empty;
                string txnStatus = string.Empty;
                string txnMode = string.Empty;

                //SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
                //if (con1.State == ConnectionState.Open)
                //{
                //    con1.Close();
                //}
                //con1.Open();
                //string res1 = "S1";
                //SqlCommand cmd1 = new SqlCommand("Insert into Test (Name,Response) values ('" + Convert.ToString(chkstring) + "','" + res1 + "')", con1);
                //cmd1.Connection = con1;
                //cmd1.ExecuteNonQuery();
                //con1.Close();



                //SqlConnection con4 = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
                //if (con4.State == ConnectionState.Open)
                //{
                //    con4.Close();
                //}
                //con4.Open();
                //string res4 = "Hash";
                //SqlCommand cmd4 = new SqlCommand("Insert into Test (Name,Response) values ('" + Convert.ToString(hash.Trim()) + "','" + res4 + "')", con4);
                //cmd4.Connection = con4;
                //cmd4.ExecuteNonQuery();
                //con4.Close();


                //SqlConnection con5 = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
                //if (con5.State == ConnectionState.Open)
                //{
                //    con5.Close();
                //}
                //con5.Open();
                //string res5 = "Checksum";
                //SqlCommand cmd5 = new SqlCommand("Insert into Test (Name,Response) values ('" + Convert.ToString(Checksum.Trim()) + "','" + res5 + "')", con5);
                //cmd5.Connection = con5;
                //cmd5.ExecuteNonQuery();
                //con5.Close();


                //SqlConnection con6 = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
                //if (con6.State == ConnectionState.Open)
                //{
                //    con6.Close();
                //}
                //con6.Open();
                //string res6 = "LabelValue";
                //SqlCommand cmd6 = new SqlCommand("Insert into Test (Name,Response) values ('" + Convert.ToString(lblmessage.Text.Trim()) + "','" + res6 + "')", con6);
                //cmd6.Connection = con6;
                //cmd6.ExecuteNonQuery();
                //con6.Close();

                String Final_hash = String.Empty;
                String Final_checksum = String.Empty;

                //Final_hash = hash.Replace(" ", String.Empty); 
                //Final_checksum = Checksum.Replace(" ", String.Empty);

                //Final_hash = Regex.Replace(Final_hash, @"\t\n\r", "");
                //Final_checksum = Regex.Replace(Final_checksum, @"\t\n\r", "");

                Final_hash = hash.Trim();
                Final_checksum = Checksum.Trim();

                Final_hash = Final_hash.Trim();
                Final_checksum = Final_checksum.Trim();

                //SqlConnection con7 = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
                //if (con7.State == ConnectionState.Open)
                //{
                //    con7.Close();
                //}
                //con7.Open();
                //string res7 = "Final_hash";
                //SqlCommand cmd7 = new SqlCommand("Insert into Test (Name,Response) values ('" + Convert.ToString(Final_hash) + "','" + res7 + "')", con7);
                //cmd7.Connection = con7;
                //cmd7.ExecuteNonQuery();
                //con7.Close();


                //SqlConnection con8 = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
                //if (con8.State == ConnectionState.Open)
                //{
                //    con8.Close();
                //}
                //con8.Open();
                //string res8 = "Final_checksum";
                //SqlCommand cmd8 = new SqlCommand("Insert into Test (Name,Response) values ('" + Convert.ToString(Final_checksum) + "','" + res8 + "')", con8);
                //cmd8.Connection = con8;
                //cmd8.ExecuteNonQuery();
                //con8.Close();

                //if (hash == Checksum)
                //if (Final_hash.ToString() == Final_checksum.ToString())
                if (String.Equals(Final_hash.ToString(),Final_checksum.ToString()))
                {
                    #region Get Transaction Details
                    if (AuthStatus == "0300")
                    {
                        txnMessage = "Successful Transaction";
                        txnStatus = "Success";
                    }
                    else if (AuthStatus == "0399")
                    {
                        txnMessage = "Cancel Transaction";
                        txnStatus = "Invalid Authentication at Bank";
                    }
                    else if (AuthStatus == "NA")
                    {
                        txnMessage = "Cancel Transaction";
                        txnStatus = "Invalid Input in the Request Message";
                    }
                    else if (AuthStatus == "0002")
                    {
                        txnMessage = "Cancel Transaction";
                        txnStatus = "BillDesk is waiting for Response from Bank";
                    }
                    else if (AuthStatus == "0001")
                    {
                        txnMessage = "Cancel Transaction";
                        txnStatus = "Error at BillDesk";
                    }
                    else
                    {
                        txnMessage = "Something went wrong. Try Again!.";
                        txnStatus = "Payment Faild";
                    }
                    #endregion

                    #region Transaction Type
                    if (TxnType == "01")
                        txnMode = "Netbanking";
                    else if (TxnType == "02")
                        txnMode = "Credit Card";
                    else if (TxnType == "03")
                        txnMode = "Debit Card";
                    else if (TxnType == "04")
                        txnMode = "Cash Card";
                    else if (TxnType == "05")
                        txnMode = "Mobile Wallet";
                    else if (TxnType == "06")
                        txnMode = "IMPS";
                    else if (TxnType == "07")
                        txnMode = "Reward Points";
                    else if (TxnType == "08")
                        txnMode = "Rupay";
                    #endregion

                    #region Assign Values to objEntity
                    string TxRefNo = TxnReferenceNo;
                    string PgTxnNo = BankReferenceNo;
                    decimal TxnAmount = Convert.ToDecimal(txn_amount);
                    string TxStatus = txnStatus;
                    string TxMssg = txnMessage;
                    string TransactionType = "Online";
                    string PaymentFor = TxnType;
                    #endregion

                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                        con.Open();
                        string res = "S1";
                        SqlCommand cmd = new SqlCommand("Insert into Test (Name,Response) values ('" + Convert.ToString(lblmessage.Text) + "','" + res + "')", con);
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        con.Close();

                    if (txnStatus == "Success")
                    {
                        Session["ResponseMsg"] = "Payment success.";

                        if (ViewState["Orderid"] != null && ViewState["IDNO"] != null && ViewState["SESSIONNO"] != null)
                        {
                            int result = 0;
                            string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["Orderid"].ToString() + "' ");

                            if (rec_code == "PRF")//prf
                            {
                                result = feeController.OnlinePaymentS2S(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 1, Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(ViewState["SESSIONNO"])); //1 for photo copy
                            }
                            else if (rec_code == "RF")//"RF"
                            {
                                result = feeController.OnlinePaymentS2S(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 2, Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(ViewState["SESSIONNO"])); //2 for reval
                            }
                            else if (rec_code == "AEF")//"AEF"
                            {
                                result = feeController.OnlinePaymentS2S(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 3, Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(ViewState["SESSIONNO"])); //3 for arrear exam
                            }
                            else if (rec_code == "CF")//"CF"
                            {
                                result = feeController.OnlinePaymentS2S(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 4, Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(ViewState["SESSIONNO"])); //4 for Condonation
                            }
                            else if (rec_code == "TF" || rec_code == "HF" || rec_code == "TPF")
                            {
                                result = feeController.OnlinePaymentS2S_Installment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 5, Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(ViewState["SESSIONNO"]), rec_code); //5 for online fees payment

                            }
                        }
                    }
                    else
                    {
                        Session["ResponseMsg"] = "Please try again.";
                        if (ViewState["Orderid"] != null)
                        {
                            int result = 0;
                            string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["Orderid"].ToString() + "'");

                            if (rec_code == "PRF")
                            {
                                result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 1);  //1 for photo copy
                            }
                            else if (rec_code == "RF")//"RF"
                            {
                                result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 2);  //2 for reval
                            }
                            else if (rec_code == "AEF")//"AEF"
                            {
                                result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 3);  //3 for arrear
                            }
                            else if (rec_code == "CF")//"CF"
                            {
                                result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 4);  //4 for Condonation
                            }
                            else if (rec_code == "TF" || rec_code == "HF" || rec_code == "TPF")
                            {
                                result = feeController.OnlineInstallmentFeesPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, rec_code);  //5 for online fees payment
                                

                            }
                        }
                    }
                }
                else
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    string res = "KEYNOTMATCH";
                    SqlCommand cmd = new SqlCommand("Insert into Test (Name,Response) values ('" + Convert.ToString("KEYNOTMATCH") + "','" + res + "')", con);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                #endregion
            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                string res = "ERROR";
                SqlCommand cmd = new SqlCommand("Insert into Test (Name,Response) values ('" + Convert.ToString("NOTFOUNDSTRING") + "','" + res + "')", con);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
                lblmessage.Text = chkstring + "ERROR : RESPOSNE NOT FOUND";
            }   
        }
        catch { }
    }


    // ----  BILl Desk Payment Response 
    public void CheckResponseFromBillDesk(string[] BDResponse)
    {
        #region Declaration for Response processing
        //string urlPath = ConfigurationManager.AppSettings["PaymentPath"];
        string ErrorMessage = string.Empty;
        #endregion

        #region BillDesk Response Data
        string MerchantID = BDResponse[0].Replace('+', ' ');
        string UniTranNo = BDResponse[1].Replace('+', ' ');
        string TxnReferenceNo = BDResponse[2].Replace('+', ' ');
        string BankReferenceNo = BDResponse[3].Replace('+', ' ');
        string txn_amount = BDResponse[4].Replace('+', ' ');
        string BankID = BDResponse[5].Replace('+', ' ');
        string BankMerchantID = BDResponse[6].Replace('+', ' ');
        string TxnType = BDResponse[7].Replace('+', ' ');
        string CurrencyType = BDResponse[8].Replace('+', ' ');
        string ItemCode = BDResponse[9].Replace('+', ' ');
        string SecurityType = BDResponse[10].Replace('+', ' ');
        string SecurityID = BDResponse[11].Replace('+', ' ');
        string SecurityPasswod = BDResponse[12].Replace('+', ' ');
        string TxnDate = BDResponse[13].Replace('+', ' ');
        string AuthStatus = BDResponse[14].Replace('+', ' ');
        string SettlementType = BDResponse[15].Replace('+', ' ');
        string additional_info1 = BDResponse[16].Replace('+', ' ');
        string additional_info2 = BDResponse[17].Replace('+', ' ');
        string additional_info3 = BDResponse[18].Replace('+', ' ');
        string additional_info4 = BDResponse[19].Replace('+', ' ');
        string additional_info5 = BDResponse[20].Replace('+', ' ');
        string additional_info6 = BDResponse[21].Replace('+', ' ');
        string additional_info7 = BDResponse[22].Replace('+', ' ');
        string ErrorStatus = BDResponse[23].Replace('+', ' ');
        string errorDescription = BDResponse[24].Replace('+', ' ');
        String Checksum = BDResponse[25].Replace('+', ' ');
        #endregion

        #region Generate Bill Desk Check Sum
        StringBuilder billRequest = new StringBuilder();
        billRequest.Append(MerchantID).Append("|");
        billRequest.Append(UniTranNo).Append("|");
        billRequest.Append(TxnReferenceNo).Append("|");
        billRequest.Append(BankReferenceNo).Append("|");
        billRequest.Append(txn_amount).Append("|");
        billRequest.Append(BankID).Append("|");
        billRequest.Append(BankMerchantID).Append("|");
        billRequest.Append(TxnType).Append("|");
        billRequest.Append(CurrencyType).Append("|");
        billRequest.Append(ItemCode).Append("|");
        billRequest.Append(SecurityType).Append("|");
        billRequest.Append(SecurityID).Append("|");
        billRequest.Append(SecurityPasswod).Append("|");
        billRequest.Append(TxnDate).Append("|");
        billRequest.Append(AuthStatus).Append("|");
        billRequest.Append(SettlementType).Append("|");
        billRequest.Append(additional_info1).Append("|");
        billRequest.Append(additional_info2).Append("|");
        billRequest.Append(additional_info3).Append("|");
        billRequest.Append(additional_info4).Append("|");
        billRequest.Append(additional_info5).Append("|");
        billRequest.Append(additional_info6).Append("|");
        billRequest.Append(additional_info7).Append("|");
        billRequest.Append(ErrorStatus).Append("|");
        billRequest.Append(errorDescription);

        string data = billRequest.ToString();
        lblmessage.Text = data;
        ViewState["Orderid"] = UniTranNo;
        ViewState["IDNO"] = additional_info2;
        ViewState["RECIEPT"] = additional_info3;

        if (additional_info7 == "NA")
        {
            ViewState["SESSIONNO"] = 0;
        }
        else
        {
            ViewState["SESSIONNO"] = additional_info7;
        }

        string ChecksumKey = "gpOT4FkIAejC";
        String hash = String.Empty;
        hash = GetHMACSHA256(data, ChecksumKey);
        hash = hash.ToUpper();

        #endregion

        #region Payment Transaction Update

        string txnMessage = string.Empty;
        string txnStatus = string.Empty;
        string txnMode = string.Empty;

        if (hash == Checksum)
        {
            #region Get Transaction Details
            if (AuthStatus == "0300")
            {
                txnMessage = "Successful Transaction";
                txnStatus = "Success";
            }
            else if (AuthStatus == "0399")
            {
                txnMessage = "Cancel Transaction";
                txnStatus = "Invalid Authentication at Bank";
            }
            else if (AuthStatus == "NA")
            {
                txnMessage = "Cancel Transaction";
                txnStatus = "Invalid Input in the Request Message";
            }
            else if (AuthStatus == "0002")
            {
                txnMessage = "Cancel Transaction";
                txnStatus = "BillDesk is waiting for Response from Bank";
            }
            else if (AuthStatus == "0001")
            {
                txnMessage = "Cancel Transaction";
                txnStatus = "Error at BillDesk";
            }
            else
            {
                txnMessage = "Something went wrong. Try Again!.";
                txnStatus = "Payment Faild";
            }
            #endregion

            #region Transaction Type
            if (TxnType == "01")
                txnMode = "Netbanking";
            else if (TxnType == "02")
                txnMode = "Credit Card";
            else if (TxnType == "03")
                txnMode = "Debit Card";
            else if (TxnType == "04")
                txnMode = "Cash Card";
            else if (TxnType == "05")
                txnMode = "Mobile Wallet";
            else if (TxnType == "06")
                txnMode = "IMPS";
            else if (TxnType == "07")
                txnMode = "Reward Points";
            else if (TxnType == "08")
                txnMode = "Rupay";
            #endregion

            #region Assign Values to objEntity
            string TxRefNo = TxnReferenceNo;
            string PgTxnNo = BankReferenceNo;
            decimal TxnAmount = Convert.ToDecimal(txn_amount);
            string TxStatus = txnStatus;
            string TxMssg = txnMessage;
            string TransactionType = "Online";
            string PaymentFor = TxnType;
            #endregion


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            string res = "SS";
            SqlCommand cmd = new SqlCommand("Insert into Test (Name,Response) values ('" + Convert.ToString(lblmessage.Text) + "','" + res + "')", con);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();

            if (txnStatus == "Success")
            {
                Session["ResponseMsg"] = "Payment success.";

                if (ViewState["Orderid"] != null && ViewState["IDNO"] != null && ViewState["SESSIONNO"] != null)
                {
                    int result = 0;
                    string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["Orderid"].ToString()  + "' ");

                    if (rec_code == "PRF")//prf
                    {
                        result = feeController.OnlinePaymentS2S(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 1, Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(ViewState["SESSIONNO"])); //1 for photo copy
                    }
                    else if (rec_code == "RF")//"RF"
                    {
                        result = feeController.OnlinePaymentS2S(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 2, Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(ViewState["SESSIONNO"])); //2 for reval
                    }
                    else if (rec_code == "AEF")//"AEF"
                    {
                        result = feeController.OnlinePaymentS2S(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 3, Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(ViewState["SESSIONNO"])); //3 for arrear exam
                    }
                    else if (rec_code == "CF")//"CF"
                    {
                        result = feeController.OnlinePaymentS2S(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 4, Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(ViewState["SESSIONNO"])); //4 for Condonation
                    }
                    //else if (rec_code == "TF" || rec_code == "HF" || rec_code == "TPF")
                    //{
                    //    result = feeController.OnlinePaymentS2S(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 5, Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(ViewState["SESSIONNO"])); //5 for fees online payment
                    //}
                    //lblmessage.Visible = true;
                    //lblmessage.ForeColor = System.Drawing.Color.Green; lblmessage.Font.Bold = true;
                    //lblmessage.Text = "Hello " + additional_info1 + " ! <br /> We have processed Payment of  Rs." + txn_amount + " successfully. <br/> <br/> Transaction ID : " + BankReferenceNo + ".<br/> Thank You";
                }

            }
            else
            {
                Session["ResponseMsg"] = "Please try again.";
                if (ViewState["Orderid"] != null)
                {
                    int result = 0;
                    string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["Orderid"].ToString() + "'");

                    if (rec_code == "PRF")
                    {
                        result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 1);  //1 for photo copy
                    }
                    else if (rec_code == "RF")//"RF"
                    {
                        result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 2);  //2 for reval
                    }
                    else if (rec_code == "AEF")//"AEF"
                    {
                        result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 3);  //3 for arrear
                    }
                    else if (rec_code == "CF")//"CF"
                    {
                        result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 4);  //4 for Condonation
                    }
                    //else if (rec_code == "TF" || rec_code == "HF" || rec_code == "TPF")
                    //{
                    //    result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 5);  //5 for online fees payment
                    //}
                    //lblmessage.Visible = true;
                    //lblmessage.ForeColor = System.Drawing.Color.Red; lblmessage.Font.Bold = true;
                    //lblmessage.Text = "Hello " + additional_info1 + " ! <br /> We are Unable To Process Payment of  Rs." + txn_amount + ".<br/> <br/>  Due to Reason : ' NA'.  <br/> <br/> Transaction ID : " + BankReferenceNo + ".<br/> Thank You";
                }
            }
        }
        //else
        //{
        //    //lblmessage.Visible = true;
        //    //lblmessage.ForeColor = System.Drawing.Color.Red; lblmessage.Font.Bold = true;
        //    //lblmessage.Text = "Something went wrong. Try Again!.";
        //}

        #endregion
    }


    public void doubleVerification()
    {
        try
        {
            String data = "0122|SVCE2" + "|" + ViewState["Orderid"] + "|" + DateTime.Now.ToString("yyyyMMddhhmmss");

            string ChecksumKey = "gpOT4FkIAejC";
            String hash = String.Empty;
            hash = GetHMACSHA256(data, ChecksumKey);
            hash = hash.ToUpper();
            string msg = data + "|" + hash;

            //TO ENABLE SECURE CONNECTION SSL/TLS
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            var request = (HttpWebRequest)WebRequest.Create("https://www.billdesk.com/pgidsk/PGIQueryController?msg=" + msg);
            var response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string strResponse = reader.ReadToEnd();
            //lblNote1.Text = strResponse;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            string res = "SD";
            SqlCommand cmd = new SqlCommand("Insert into Test (Name,Response) values ('" + strResponse.ToString() + "','" + res + "')", con);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            //lblNote1.Text = string.Empty;
            string[] repoarray;
            repoarray = strResponse.Split('|');

            string authstatus = repoarray[15].ToString();
            string txnid1 = repoarray[2].ToString();
            string amount1 = repoarray[5].ToString();
            string apitransid = repoarray[3].ToString();
            string BankReferenceNo = repoarray[4].ToString();
            string TxnType = repoarray[8].ToString();
            string receipt = repoarray[19].ToString();

            string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["Orderid"].ToString()  + "'");
            //string status = repoarray[15].ToString();
            //string status_msg = repoarray[25].ToString();
            //string msgR = repoarray[32].ToString();
            string txnMessage = string.Empty;
            string txnStatus = string.Empty;
            #region Get Transaction Details
            if (authstatus == "0300")
            {
                txnMessage = "Successful Transaction";
                txnStatus = "Success";
            }
            else if (authstatus == "0399")
            {
                txnMessage = "Cancel Transaction";
                txnStatus = "Invalid Authentication at Bank";
            }
            else if (authstatus == "NA")
            {
                txnMessage = "Cancel Transaction";
                txnStatus = "Invalid Input in the Request Message";
            }
            else if (authstatus == "0002")
            {
                txnMessage = "Cancel Transaction";
                txnStatus = "BillDesk is waiting for Response from Bank";
            }
            else if (authstatus == "0001")
            {
                txnMessage = "Cancel Transaction";
                txnStatus = "Error at BillDesk";
            }
            else
            {
                txnMessage = "Something went wrong. Try Again!.";
                txnStatus = "Payment Faild";
            }
            #endregion

            FeeCollectionController objFeesCnt = new FeeCollectionController();
            if (authstatus == "0300")
            {
                int retval = 0;
                if (rec_code == "PRF")
                {
                    retval = objFeesCnt.OnlinePaymentUpdation(apitransid, txnid1, amount1, authstatus, txnStatus, TxnType, txnMessage, BankReferenceNo, 1);

                }
                else if (rec_code == "RF")
                {
                    retval = objFeesCnt.OnlinePaymentUpdation(apitransid, txnid1, amount1, authstatus, txnStatus, TxnType, txnMessage, BankReferenceNo, 2);

                }
                else if (rec_code == "AEF")
                {
                    retval = objFeesCnt.OnlinePaymentUpdation(apitransid, txnid1, amount1, authstatus, txnStatus, TxnType, txnMessage, BankReferenceNo, 3);

                }
                else if (rec_code == "CF")
                {
                    retval = objFeesCnt.OnlinePaymentUpdation(apitransid, txnid1, amount1, authstatus, txnStatus, TxnType, txnMessage, BankReferenceNo, 4);

                }
                //else if (rec_code == "TF" || rec_code == "HF" || rec_code == "TPF")
                //{
                //    retval = objFeesCnt.OnlinePaymentUpdation(apitransid, txnid1, amount1, authstatus, txnStatus, TxnType, txnMessage, BankReferenceNo, 5);

                //}
                //if (retval == -99)
                //{
                //    objCommon.DisplayMessage(this, "Error occured", this.Page);
                //}
            }
        }
        catch { }
    }


    public string GetHMACSHA256(string text, string key)
    {
        UTF8Encoding encoder = new UTF8Encoding();

        byte[] hashValue;
        byte[] keybyt = encoder.GetBytes(key);
        byte[] message = encoder.GetBytes(text);

        HMACSHA256 hashString = new HMACSHA256(keybyt);
        string hex = "";

        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }

}