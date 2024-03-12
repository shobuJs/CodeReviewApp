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
using System.Data.SqlClient;
using System.Data;
using DotNetIntegrationKit;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml;
using System.Net;
using _NVP;
using System.Collections.Specialized;
using EASendMail;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;

using Microsoft.WindowsAzure.Storage;

public partial class ACADEMIC_ExtraCharge : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    decimal TotalSum = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["userno"] == null || Session["username"] == null ||
                  Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            if (!Page.IsPostBack)
            {

                // Check User Session
                if (Session["userno"] == null || Session["username"] == null || Session["idno"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    this.CheckPageAuthorization();
                    ShowStudentDetails();
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // dynamic page header addded by sarang 09/07/2021
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page and maintain user activity
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ExtraCharge.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ExtraCharge.aspx");
        }
    }

    private void ShowStudentDetails()
    {
        try
        {
            if (Session["usertype"].ToString().Equals("2"))     //Student 
            {
                DataSet dsStudent = feeController.GetStudentInfoById_ForOnlinePayment(Convert.ToInt32(Session["idno"]));
                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    DataRow dr = dsStudent.Tables[0].Rows[0];

                    string fullName = dr["STUDNAME"].ToString();
                    string[] names = fullName.Split(' ');
                    string name = names.First();
                    string lasName = names.Last();
                    lblStudName.Text = fullName;

                    lblSex.Text = dr["SEX"].ToString() == string.Empty ? string.Empty : dr["SEX"].ToString();
                    lblRegNo.Text = dr["REGNO"].ToString() == string.Empty ? string.Empty : dr["REGNO"].ToString();
                    lblDegree.Text = dr["DEGREENAME"].ToString() == string.Empty ? string.Empty : dr["DEGREENAME"].ToString();
                    lblFaculty.Text = dr["COLLEGENAME"].ToString() == string.Empty ? string.Empty : dr["COLLEGENAME"].ToString();
                    lblBranch.Text = dr["BRANCH_NAME"].ToString() == string.Empty ? string.Empty : dr["BRANCH_NAME"].ToString();
                    lblYear.Text = dr["YEARNAME"].ToString() == string.Empty ? string.Empty : dr["YEARNAME"].ToString();
                    lblSemester.Text = dr["SEMESTERNAME"].ToString() == string.Empty ? string.Empty : dr["SEMESTERNAME"].ToString();
                    lblBatch.Text = dr["BATCHNAME"].ToString() == string.Empty ? string.Empty : dr["BATCHNAME"].ToString();
                    lblMobileNo.Text = dr["STUDENTMOBILE"].ToString() == string.Empty ? string.Empty : dr["STUDENTMOBILE"].ToString();
                    lblEmailID.Text = dr["EMAILID"].ToString() == string.Empty ? string.Empty : dr["EMAILID"].ToString();
                    ViewState["degreeno"] = dr["DEGREENO"].ToString();
                    ViewState["BRANCHNO"] = dr["BRANCHNO"].ToString();
                    ViewState["batchno"] = Convert.ToInt32(dr["ADMBATCH"].ToString());
                    ViewState["semesterno"] = Convert.ToInt32(dr["SEMESTERNO"].ToString());
                    ViewState["paytypeno"] = Convert.ToInt32(dr["PTYPE"].ToString());
                    ViewState["RECIEPT_CODE"] = dr["RECIEPT_CODE"].ToString() == string.Empty ? string.Empty : dr["RECIEPT_CODE"].ToString();
                    ViewState["SESSIONNO"] = dr["SESSIONNO"].ToString() == string.Empty ? string.Empty : dr["SESSIONNO"].ToString();
                    ViewState["COLLEGE_ID"] = dr["COLLEGE_ID"].ToString();
                    ViewState["RECON"] = dr["RECON"].ToString();
                    ViewState["NICPASS"] = dr["NICPASS"].ToString();
                    ViewState["UGPGOTONLINE"] = dr["UGPGOT"].ToString();

                    string Amount = objCommon.LookUp("ACD_DCR D INNER JOIN ACD_DEMAND DA ON D.IDNO = DA.IDNO", "DISTINCT CONVERT(NUMERIC(10,2),(DA.TOTAL_AMT * (SELECT DISTINCT SERVICE_CHARGE FROM REFF)/100))", "RECON = 1 AND ap_SecureHash LIKE '%CAPTURED%' AND ISNULL(DA.CAN,0) = 0 AND ISNULL(DA.DELET,0) = 0 AND D.IDNO =" + Convert.ToInt32(Session["idno"]));
                    //string ServiceCharge = objCommon.LookUp("REFF", "DISTINCT SERVICE_CHARGE", "");
                    //decimal caluclateServiceCharge = (Convert.ToDecimal(Amount) * Convert.ToDecimal(ServiceCharge) / 100);
                    //decimal text = Convert.ToDecimal(Amount) + Convert.ToDecimal(caluclateServiceCharge);

                    if (Amount == string.Empty || Amount == "" || Amount == "0" || Amount == "0.00")
                    {
                        lblAmount.Text = "0";
                        btnSubmit.Visible = true;
                        ViewState["FinalAmountPaid"] = "0";
                    }
                    else
                    {
                        //lblAmount.Text = Convert.ToString(text.ToString().TrimEnd('0'));
                        lblAmount.Text = Amount;
                        btnSubmit.Visible = true;
                        ViewState["FinalAmountPaid"] = Amount;
                    }
                }
                else
                {
                    objCommon.DisplayUserMessage(UpdatePanel1, "Record Not Found.", this.Page);
                    btnSubmit.Visible = false;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "Record Not Found.This Page is use for Online Payment for Student Login!!", this.Page);
                btnSubmit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.ShowStudentDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)   ////Student Payment Click Button
    {
        try
        {


            if (lblAmount.Text == "0" || lblAmount.Text == "" || lblAmount.Text == "0.00")
            {
                objCommon.DisplayMessage(UpdatePanel1, "Service Charge Amount Cannot be Less than or Equal to Zero !!", this.Page);
                return;
            }
            else if (Convert.ToString(ViewState["FinalAmountPaid"]) == "0" || Convert.ToString(ViewState["FinalAmountPaid"]) == "" || Convert.ToString(ViewState["FinalAmountPaid"]) == null)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Service Charge Amount Cannot be Less than or Equal to Zero !!", this.Page);
                return;
            }
            else
            {
                SubmitData();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void CreateCustomerRef()
    {
        Random rnd = new Random();
        int ir = rnd.Next(01, 10000);

        lblOrderID.Text = "EXT" + Convert.ToString(Convert.ToInt32(Session["idno"]) + Convert.ToString(ViewState["SESSIONNO"]) + Convert.ToString(ViewState["semesterno"]) + ir);
        Session["ERPORDERIDRESPONSE"] = lblOrderID.Text;
    }
    private void SubmitData()
    {
        try
        {
            CreateCustomerRef();

            string SP_Name1 = "PKG_ACD_INSERT_EXTRA_CHARGE_AMOUNT";
            string SP_Parameters1 = "@P_IDNO,@P_PAID_AMOUNT,@P_ORDER_ID,@P_OUT";
            string Call_Values1 = "" + Convert.ToInt32(Session["idno"]) + "," + Convert.ToDecimal(ViewState["FinalAmountPaid"].ToString()) + "," + 
                Convert.ToString(lblOrderID.Text) + "," + ",0";

            string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);

            if (que_out1 == "1")
            {
                DataSet ds = null;
                ds = feeController.GetOnlineTrasactionOnlineOrderID(Convert.ToInt32(Session["idno"]), Convert.ToString(lblOrderID.Text));

                if (ds.Tables[0].Rows.Count > 0)
                {

                    if (Convert.ToString(ds.Tables[0].Rows[0]["ORDER_ID"]) == lblOrderID.Text)
                    {
                        SendTransaction();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "Checkout.showPaymentPage()", true);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Something went wrong , Please try again !", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Failed To Done Online Payment.", this.Page);
                }
            }
            else
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "Failed To Done Online Payment.", this.Page);
                return;
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.SubmitData-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void SendTransaction()
    {
        System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)768 | (System.Net.SecurityProtocolType)3072;
        String result = null;
        String gatewayCode = null;
        String response = null;

        // get the request form and make sure to UrlDecode each value in case special characters used
        NameValueCollection formdata = new NameValueCollection();
        //foreach (String key in Request.Form)
        //{
        //    formdata.Add(key, HttpUtility.UrlDecode(Request.Form[key]));
        //}

        Merchant merchant = new Merchant();

        // [Snippet] howToConfigureURL - start
        StringBuilder url = new StringBuilder();
        if (!merchant.GatewayHost.StartsWith("http"))
            url.Append("https://");
        url.Append(merchant.GatewayHost);
        //url.Append("/api/nvp/version/");
        //url.Append(merchant.Version);

        merchant.GatewayUrl = url.ToString();
        // [Snippet] howToConfigureURL - end

        Connection connection = new Connection(merchant);

        // [Snippet] howToConvertFormData -- start
        StringBuilder data = new StringBuilder();
        data.Append("merchant=" + merchant.MerchantId);
        data.Append("&apiUsername=" + merchant.Username);
        data.Append("&apiPassword=" + merchant.Password);

        // add each key and value in the form data
        formdata.Add("apiOperation", "CREATE_CHECKOUT_SESSION");
        //formdata.Add("apiUsername", "merchant.700182200072");
        //formdata.Add("apiPassword", "004dc01e2adfd7ec414ec6255a16e505");
        //formdata.Add("merchant", "700182200072");

        //formdata.Add("interaction.returnUrl", "http://localhost:55158/PresentationLayer/OnlineResponse.aspx");

        string returnurl = System.Configuration.ConfigurationManager.AppSettings["ReturnUrl"];
        formdata.Add("interaction.returnUrl", returnurl);
        // formdata.Add("interaction.returnUrl", "http://localhost:59566/PresentationLayer/OnlineResponse.aspx");

        formdata.Add("interaction.operation", "PURCHASE");
        formdata.Add("order.id", lblOrderID.Text);
        formdata.Add("order.currency", "LKR");
        formdata.Add("order.amount", Convert.ToString(ViewState["FinalAmountPaid"]));
        formdata.Add("order.description", Convert.ToString(Session["idno"]));
        //url.Append("/apiOperation/");
        //url.Append("CREATE_CHECKOUT_SESSION");
        //url.Append("/apiUsername/");
        //url.Append("merchant.700182200072");
        //url.Append("/apiPassword/");
        //url.Append("004dc01e2adfd7ec414ec6255a16e505");
        //url.Append("/merchant/");
        //url.Append("700182200072");
        //url.Append("interaction.operation");
        //url.Append("/PURCHASE/");
        //url.Append("order.id");
        //url.Append("123456789");
        //url.Append("/order.currency/");
        //url.Append("LKR");
        //url.Append("/order.amount/");
        //url.Append("1000");
        //url.Append("/order.description/");
        //url.Append("\"d2564\"");
        foreach (string key in formdata)
        {
            data.Append("&" + key.ToString() + "=" + HttpUtility.UrlEncode(formdata[key], System.Text.Encoding.GetEncoding("ISO-8859-1")));
        }
        // [Snippet] howToConvertFormData -- end

        response = connection.SendTransaction(data.ToString());

        // [Snippet] howToParseResponse - start
        NameValueCollection respValues = new NameValueCollection();
        if (response != null && response.Length > 0)
        {
            String[] responses = response.Split('&');
            foreach (String responseField in responses)
            {
                String[] field = responseField.Split('=');
                respValues.Add(field[0], HttpUtility.UrlDecode(field[1]));
            }
        }
        // [Snippet] howToParseResponse - end

        result = respValues["result"];

        // Form error string if error is triggered
        if (result != null && result.Equals("ERROR"))
        {
            String errorMessage = null;
            String errorCode = null;

            String failureExplanations = respValues["explanation"];
            String supportCode = respValues["supportCode"];

            if (failureExplanations != null)
            {
                errorMessage = failureExplanations;
            }
            else if (supportCode != null)
            {
                errorMessage = supportCode;
            }
            else
            {
                errorMessage = "Reason unspecified.";
            }

            String failureCode = respValues["failureCode"];
            if (failureCode != null)
            {
                errorCode = "Error (" + failureCode + ")";
            }
            else
            {
                errorCode = "Error (UNSPECIFIED)";
            }

            // now add the values to result fields in panels
            //lblErrorCode.Text = errorCode;
            //lblErrorMessage.Text = errorMessage;
            //pnlError.Visible = true;
        }

        // error or not display what response values can
        gatewayCode = respValues["response.gatewayCode"];
        if (gatewayCode == null)
        {
            gatewayCode = "Response not received.";
        }
        //lblGateWayCode.Text = gatewayCode;
        //lblResult.Text = result;

        // build table of NVP results and add to panel for results

        int shade = 0;
        foreach (String key in respValues)
        {

            if (key == "session.id")
            {
                Session["ERPPaymentSession"] = respValues[key];
            }
            if (key == "successIndicator")
            {
                Session["ERPsuccessIndicator"] = respValues[key];
            }
            if (key == "session.version")
            {
                Session["ERPsessionversion"] = respValues[key];
            }
        }
        string SP_Name1 = "PKG_ACD_UPDATE_PAYMENT_SUCCESS_INDICATOR";
        string SP_Parameters1 = "@P_IDNO,@P_ERPSUCCESSINDICATOR,@P_ERPSESSIONVERSION,@P_ORDER_ID,@P_OUTPUT";
        string Call_Values1 = "" + Convert.ToInt32(Session["idno"]) + "," + Convert.ToString(Session["ERPsuccessIndicator"]) + "," +
        Convert.ToString(Session["ERPsessionversion"]) + "," + Convert.ToString(lblOrderID.Text) + ",0";

        string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);
    }
}