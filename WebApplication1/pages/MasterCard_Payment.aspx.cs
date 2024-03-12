using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
//using SFA;
//using paytm;
using System.IO;
using System.Xml;
using System.Text;
using System.Net;
using System.Configuration;
using System.Web;
using System.Drawing;

using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Diagnostics;

public partial class ACADEMIC_MasterCard_Payment : System.Web.UI.Page
{
    Common objCommon = new Common();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void PostPayment(double Amount, string orderid)
    {
        #region Get Payment Details
        string feeAmount = string.Empty;
        feeAmount = Amount.ToString();// (ViewState["Final_Amt"]).ToString();"1"; //
        #endregion

        #region Payment Log for Different Transaction Id
        string TransactionCode = string.Empty;
        TransactionCode = orderid.ToString(); //lblOrderID.Text; // This may be configured from Database for Different Running Number
        #endregion

        #region MasterCard Payment Data Declaration
        string url = string.Empty;
        string pwd = string.Empty;

        string result = string.Empty;
        string orderId = string.Empty;
        string resultStatus = string.Empty;
        int orderIdInt = 0;
        string merchantId = string.Empty;
        string currency = string.Empty;
            string interaction = string.Empty;
        decimal amount = 0;
        decimal amountWithSc = 0;
        string email = string.Empty;
        bool canProceed = false;
        string payModel = string.Empty;
        int studentId = 0;

        string ReturnURL = string.Empty;
        string ChecksumKey = string.Empty;
        #endregion

        #region Param Data
        //url = "https://test-bankofceylon.mtf.gateway.mastercard.com/api/nvp/version/57";
        url = "https://test-bankofceylon.mtf.gateway.mastercard.com/api/nvp/version/60";
        pwd = "004dc01e2adfd7ec414ec6255a16e505";

        result = null;
        orderId = TransactionCode;
        resultStatus = null;
        orderIdInt = 1;
        merchantId = "700182200072";
        currency = "LKR"; ;
      //  interaction.operation = "Purchase";
        amount = Convert.ToDecimal(feeAmount.ToString());
        amountWithSc = amount;//Math.Round(((Convert.ToDecimal(Amount) / 100) * Convert.ToDecimal("101.42")), 2);
        email = "roshan.pannase@gmail.com";
        canProceed = true;
        payModel = null;
        studentId = 1;      
       
        // ReturnURL = "http://localhost:58567/PresentationLayer/academic/MasterCard_Fee_Response.aspx";

        #endregion

        #region Generate Master Card append URL

        StringBuilder billRequest = new StringBuilder();
        //billRequest.Append(url).Append("|");
        billRequest.Append(pwd).Append("|");
        billRequest.Append(result).Append("|");
        billRequest.Append(orderId).Append("|");
        billRequest.Append(resultStatus).Append("|");
        billRequest.Append(orderIdInt).Append("|");
        billRequest.Append(merchantId).Append("|");
        billRequest.Append(currency).Append("|");
        billRequest.Append(amount).Append("|");
        billRequest.Append(amountWithSc).Append("|");
        billRequest.Append(email).Append("|");
        billRequest.Append(canProceed).Append("|");
        billRequest.Append(payModel).Append("|");
        billRequest.Append(studentId).Append("|");
       
        billRequest.Append(ReturnURL);

        string data = billRequest.ToString();

        //String hash = String.Empty;
        //hash = GetHMACSHA256(data, ChecksumKey);
        //hash = hash.ToUpper();

        string msg = data;// +"|" + hash;

        #endregion

        #region Post to Master Card Payment Gateway

        //string PaymentURL = ConfigurationManager.AppSettings["BillDeskURL"] + msg;
        //  string PaymentURL = "https://migs.mastercard.com.au/vpcpay?" + msg;
        //string PaymentURL = "https://test-bankofceylon.mtf.gateway.mastercard.com/api/nvp/version/57?" + msg;

        string PaymentURL = "https://test-bankofceylon.mtf.gateway.mastercard.com/api/nvp/version/60?" + msg;


        //Response.Redirect(PaymentURL, false);
        Response.Write("<form name='s1_2' id='s1_2' action='" + PaymentURL + "' method='post'> ");
        Response.Write("<script type='text/javascript' language='javascript' >document.getElementById('s1_2').submit();");
        Response.Write("</script>");
        Response.Write("<script language='javascript' >");
        Response.Write("</script>");
        Response.Write("</form> ");
        Response.Write("<script>window.open(" + PaymentURL + ",'_blank');</script>");
        #endregion
    }

    //protected void PostPayment(double Amount, string orderid)
    //{
    //    #region Declarations
    //    string feeAmount = string.Empty;
    //    string Transacionid = "NA";
    //    string TransactionFor = string.Empty;
    //    string TSPLTxnCode = string.Empty;
    //    string TSPLtxtITC = string.Empty;
    //    #endregion

    //    #region Get Payment Details
    //    feeAmount = Amount.ToString();// (ViewState["Final_Amt"]).ToString();"1"; //
    //    #endregion

    //    #region Payment Log for Different Transaction Id
    //    string TransactionCode = string.Empty;
    //    TransactionCode = orderid.ToString(); //lblOrderID.Text; // This may be configured from Database for Different Running Number
    //    #endregion

    //    #region MasterCard Payment Data Declaration
    //    string vpc_Version = string.Empty;
    //    string vpc_Command = string.Empty;
    //    string Authentication = string.Empty;
    //    string vpc_AccessCode = string.Empty;
    //    string Order_Details = string.Empty;
    //    string vpc_OrderInfo = string.Empty;
    //    string vpc_Amount = string.Empty;
    //    string vpc_Card	 =string.Empty;
    //    string vpc_CardExp	= string.Empty;
    //    string vpc_CardSecurityCode = string.Empty;

    //    string MerchantID = string.Empty;
    //    string UniTranNo = string.Empty;
    //    string NA1 = string.Empty;
    //    string txn_amount = string.Empty;
    //    string NA2 = string.Empty;
    //    string NA3 = string.Empty;
    //    string na4 = string.Empty;
    //    string CurrencyType = string.Empty;
    //    string NA5 = string.Empty;
    //    string TypeField1 = string.Empty;
    //    string SecurityID = string.Empty;
    //    string NA6 = string.Empty;
    //    string NA7 = string.Empty;
    //    string TypeField2 = string.Empty;
    //    string additional_info1 = string.Empty;
    //    string additional_info2 = string.Empty;
    //    string additional_info3 = string.Empty;
    //    string additional_info4 = string.Empty;
    //    string additional_info5 = string.Empty;
    //    string additional_info6 = string.Empty;
    //    string additional_info7 = string.Empty;
    //    string ReturnURL = string.Empty;
    //    string ChecksumKey = string.Empty;
    //    #endregion

    //    #region Set Bill Desk Param Data
    //    MerchantID = "700182200072";//ConfigurationManager.AppSettings["MerchantID"];
    //    vpc_Version = "1";
    //    vpc_Command = "Pay";       
    //    Authentication = MerchantID;
    //    vpc_AccessCode = "004dc01e2adfd7ec414ec6255a16e505";
    //    Order_Details = TransactionCode;
    //    vpc_OrderInfo = TransactionCode;
    //    vpc_Amount = feeAmount.ToString();
    //    vpc_Card = "123456789";
    //    vpc_CardExp = "2101";//Card expiry date expressed in yyMM format. For example, Jan 2021 is expressed as 2101.
    //    vpc_CardSecurityCode ="456";

    //    //UniTranNo = TransactionCode;
    //    //txn_amount = feeAmount;
    //    //CurrencyType = "INR";
    //    //SecurityID = ConfigurationManager.AppSettings["SecurityCode"];
    //    //additional_info1 = ViewState["STUDNAME"].ToString(); // Project Name
    //    //additional_info2 = ViewState["IDNO"].ToString();  // Project Code
    //    //additional_info3 = ddlReceiptType.SelectedValue; //ViewState["RECIEPT"].ToString(); // Transaction for??
    //    //additional_info4 = ViewState["info"].ToString(); // Payment Reason
    //    //additional_info5 = feeAmount; // Amount Passed
    //    //additional_info6 = ViewState["basicinfo"].ToString();  // basic details like regno/enroll no/branchname
    //    //additional_info7 = ViewState["SESSIONNO"].ToString();

    //  //  ReturnURL = "https://svce.mastersofterp.in/ACADEMIC/FeesPay_Response.aspx";
    //  //  ReturnURL = "http://localhost:50139/PresentationLayer/ACADEMIC/FeesPay_Response.aspx";
    //    ReturnURL = "http://localhost:58567/PresentationLayer/academic/MasterCard_Fee_Response.aspx";

    //    #endregion

    //    #region Generate Master Card Check Sum

    //    StringBuilder billRequest = new StringBuilder();
    //    billRequest.Append(MerchantID).Append("|");
    //    billRequest.Append(vpc_Version).Append("|");
    //    billRequest.Append("Pay").Append("|");        
    //    billRequest.Append(Authentication).Append("|");
    //    billRequest.Append(vpc_AccessCode).Append("|");
    //    billRequest.Append(Order_Details).Append("|");
    //    billRequest.Append(vpc_Amount).Append("|");
    //    billRequest.Append(vpc_Card).Append("|");
    //    billRequest.Append(vpc_CardExp).Append("|");
    //    billRequest.Append(vpc_CardSecurityCode).Append("|");
    //    //billRequest.Append("NA").Append("|");
    //    //billRequest.Append("NA").Append("|");
    //    //billRequest.Append("F").Append("|");
    //    //billRequest.Append(additional_info1).Append("|");
    //    //billRequest.Append(additional_info2).Append("|");
    //    //billRequest.Append(additional_info3).Append("|");
    //    //billRequest.Append(additional_info4).Append("|");
    //    //billRequest.Append(additional_info5).Append("|");
    //    //billRequest.Append(additional_info6).Append("|");
    //    //billRequest.Append(additional_info7).Append("|");
    //    billRequest.Append(ReturnURL);

    //    string data = billRequest.ToString();

    //    //String hash = String.Empty;
    //    //hash = GetHMACSHA256(data, ChecksumKey);
    //    //hash = hash.ToUpper();

    //    string msg = data;// +"|" + hash;

    //    #endregion

    //    #region Post to BillDesk Payment Gateway

    //    //string PaymentURL = ConfigurationManager.AppSettings["BillDeskURL"] + msg;
    //  //  string PaymentURL = "https://migs.mastercard.com.au/vpcpay?" + msg;
    //    string PaymentURL = "https://test-bankofceylon.mtf.gateway.mastercard.com/api/nvp/version/57?" + msg;


    //    //Response.Redirect(PaymentURL, false);
    //    Response.Write("<form name='s1_2' id='s1_2' action='" + PaymentURL + "' method='post'> ");
    //    Response.Write("<script type='text/javascript' language='javascript' >document.getElementById('s1_2').submit();");
    //    Response.Write("</script>");
    //    Response.Write("<script language='javascript' >");
    //    Response.Write("</script>");
    //    Response.Write("</form> ");
    //    Response.Write("<script>window.open(" + PaymentURL + ",'_blank');</script>");
    //    #endregion
    //}
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
    protected void btnPay_Click(object sender, EventArgs e)
    {
        #region "Online Payment"
        try
        {
            Random rnd = new Random();
            int ir = rnd.Next(01, 10000);
            string lblOrderID = Convert.ToString(Convert.ToString(Session["USERNO"]) + Convert.ToString(ir));

            double Amount = 500.00;
            string orderid = lblOrderID;
            PostPayment(Amount, orderid);
        }
        catch (Exception ex)
        {
            throw;
        }
        #endregion
    }
}