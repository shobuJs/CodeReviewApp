using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Net;
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

using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

using System.Net.Mail;
using Newtonsoft.Json;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net;
using System.IO;
using CCA.Util;
using System.Collections.Specialized;
using System.Xml;

// using System.Net;
// Use SecurityProtocolType.Ssl3 if needed for compatibility reasons

public partial class ACADEMIC_OnlinePaymentReconcilation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FeeCollectionController objFee = new FeeCollectionController();
    StudentController objSC = new StudentController();
    //VAS -Integration Kit (CC) - MNR EDUCATIONAL TRUST(MNR MEDICAL COLLEGE AND HOSPITAL) - Account ID - 318083
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
        try
        {
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            if (!Page.IsPostBack)
            {
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help

                    //Page Authorization
                    //this.CheckPageAuthorization();

                    PopulateDropDownList();

                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
            }
            divMsg.InnerHtml = string.Empty;
           div_Studentdetail.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_OnlinePaymentReconcilation.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");

        }    
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=OnlinePaymentReconcilation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OnlinePaymentReconcilation.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_ID");


            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO desc");

           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "OnlinePaymentReconcilation.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND COLLEGE_ID=" + ddlCollege.SelectedValue + "", "SESSIONNO desc");

            }
            
                ddlSession.SelectedIndex = 0;
                ddlReceiptType.SelectedIndex = 0;
                txtAppID.Text = "";
                div_Studentdetail.Visible = false;
                lvstudList.Visible = false;
                lvstudList.DataSource = null; 
                lvstudList.DataBind();
            
        }
        catch { }
    }


    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_TITLE", "RCPTTYPENO > 0 AND RECIEPT_CODE IN('PRF','RF','AEF','TF','TPF','HF','CF')", "RCPTTYPENO");
            }
           
                ddlReceiptType.SelectedIndex = 0;
                txtAppID.Text = "";
                div_Studentdetail.Visible = false;
                lvstudList.Visible = false;
                lvstudList.DataSource = null;
                lvstudList.DataBind();
           

        }
        catch { }
    }

    protected void ddlReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlReceiptType.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND COLLEGE_ID=" + ddlCollege.SelectedValue + "", "SESSIONNO desc");
            }
            

                txtAppID.Text = "";
                div_Studentdetail.Visible = false;
                lvstudList.Visible = false;
                lvstudList.DataSource = null;
                lvstudList.DataBind();
           

        }
        catch { }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            this.BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "OnlinePaymentReconcilation.btnShow_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListView()
    {
        try
        {
            string rec_code = string.Empty;
            rec_code = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO = " + ddlReceiptType.SelectedValue + "");

            DataSet ds = objFee.GetFailedTransactionsOfStudent(Convert.ToInt32(ddlSession.SelectedValue),rec_code,txtAppID.Text.Trim());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                div_Studentdetail.Visible = true;
                lvstudList.DataSource = ds.Tables[0];
                lvstudList.DataBind();
                lvstudList.Visible = true;
                lblStudName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                lblStudEnrollNo.Text = ds.Tables[0].Rows[0]["ENROLLNMENTNO"].ToString() + " / " + ds.Tables[0].Rows[0]["REGNO"].ToString();
               // lblStudClg.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                lblStudDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                lblStudBranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                foreach (ListViewDataItem lvItem in lvstudList.Items)
                {
                    Button btnreq = (Button)lvItem.FindControl("btnRequest");
                    HiddenField flag = (HiddenField)lvItem.FindControl("hdfFlag");
                    if (flag.Value.ToString() == "0")
                    {
                        btnreq.Enabled = true;
                    }
                    else
                    {
                        btnreq.Enabled = false;
                    }
                   // btnreq.Enabled = true;
                }

                ddlCollege.Enabled = false;
                ddlSession.Enabled = false;
                ddlReceiptType.Enabled = false;
                txtAppID.Enabled = false;
            }
            else
            {
                objCommon.DisplayMessage("Record Not Found", this.Page);
                lvstudList.DataSource = null;
                lvstudList.DataBind();
                lvstudList.Visible = false;
                div_Studentdetail.Visible = false;
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "OnlinePaymentReconcilation.BindListView() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        
    }

 
    protected void lvstudList_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
      //  ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        if (e.CommandName == "getdata")
        {
            if (e.CommandSource is Button)
            {
                   string MerchantCode = ConfigurationManager.AppSettings["MERCHANT_KEY"];
                   string strAccessCode = ConfigurationManager.AppSettings["workingKey"]; // put the access code in the quotes provided here.
                   string workingKey = ConfigurationManager.AppSettings["strAccessCode"];//put in the 32bit alpha numeric key in the quotes provided here 	

                ListViewDataItem item = (e.CommandSource as Button).NamingContainer as ListViewDataItem;
                string reval = e.CommandArgument.ToString();
                HiddenField tempIdNo = (HiddenField)item.FindControl("hdfTempIdNo");
                Label lblOrder_ID = (Label)item.FindControl("lblORDERID");
                if (reval == tempIdNo.Value)
                {

                    int count = 0;

                    string rec_code1 = string.Empty;
                    rec_code1 = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO = " + ddlReceiptType.SelectedValue + "");
                    DataSet dsorder_id = objCommon.FillDropDown("ACD_FEES_LOG", "ORDER_ID", "", "SESSIONNO=" + ddlSession.SelectedValue + " AND USERNO=" + tempIdNo.Value + " AND RECEIPT_CODE='"+ rec_code1 +"' AND ISNULL(RECON,0)=0", "");
                    for (int i = 0; i < dsorder_id.Tables[0].Rows.Count; i++)
                    {
                        string order_id = dsorder_id.Tables[0].Rows[i]["ORDER_ID"].ToString();
                        Label receiptCode = (Label)item.FindControl("lblMsg");
                        string rec_code = receiptCode.ToolTip;


                        try
                        {
                            string accessCode = strAccessCode;//from avenues
                             //workingKey = workingKey;// from avenues

                            //string orderStatusQuery = "310006885641|" + order_id + "|"; // Ex.= CCAvenue Reference No.|Order No.| //1616403693200
                            string orderStatusQuery = "" + lblOrder_ID.ToolTip + "|" + order_id + "|"; // Ex.= CCAvenue Reference No.|Order No.| //1616403693200
                            string encQuery = "";

                            //string queryUrl = "https://login.ccavenue.com/apis/servlet/DoWebTrans";
                            string queryUrl = "https://logintest.ccavenue.com/apis/servlet/DoWebTrans";

                            CCACrypto ccaCrypto = new CCACrypto();
                            encQuery = ccaCrypto.Encrypt(orderStatusQuery, workingKey);

                            // make query for the status of the order to ccAvenues change the command param as per your need
                            string authQueryUrlParam = "enc_request=" + encQuery + "&access_code=" + accessCode + "&command=orderStatusTracker&request_type=STRING&response_type=STRING";

                            // Url Connection
                            String message = postPaymentRequestToGateway(queryUrl, authQueryUrlParam);
                            //Response.Write(message);
                            NameValueCollection param = getResponseMap(message);
                            String status = "";
                            String encRes = "";
                            if (param != null && param.Count == 2)
                            {
                                for (int j = 0; j < param.Count; j++)
                                {
                                    if ("status".Equals(param.Keys[j]))
                                    {
                                        status = param[j];
                                    }
                                    if ("enc_response".Equals(param.Keys[j]))
                                    {
                                        encRes = param[j];
                                        //Response.Write(encResXML);
                                    }
                                }
                                if (!"".Equals(status) && status.Equals("0"))
                                {
                                    String ResString = ccaCrypto.Decrypt(encRes, workingKey);
                                    ///Response.Write(ResString);
                                   
                                    //String ResString = "0|Shipped|310006885641|1612950050946|Y|AASHRITHA VANGARI|||||||MasterCard Debit Card|INR|2021-02-10 15:10:19.357||PC|NA||AVN|59.97.236.2||28328490||OPTDBCRD||||||||Shipped|2021-02-10 15:11:27.347|0.0|||1.0|1.0|0.0|0.0|0.01|1.0|0.0|0";
                                    lblNote1.Text = string.Empty;
                                    lblNote1.Text = ResString;

                                    string[] segments = ResString.Split('|');
                                    string status_code = "";
                                    string aptransid = "";
                                    string transid = "";
                                    string orderno = "";
                                    //decimal orderamt = 0.00M;
                                    string orderamt = "";
                                    status_code = segments[0].ToString();
                                    transid  = segments[2].ToString();
                                    aptransid = segments[3].ToString();
                                    orderno = segments[22].ToString();
                                    //orderamt = Convert.ToDecimal( segments[34].ToString());
                                    orderamt = (segments[37].ToString());
                                    string response = "";
                                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
                                    if (con.State == ConnectionState.Open)
                                    {
                                        con.Close();
                                    }
                                    con.Open();
                                    string res = "CCAVENUE Query API";
                                    SqlCommand cmd = new SqlCommand("Insert into RESPONSE_QUERY (QUERY,RESPONSE,IDNO,ORDER_ID,STATUS) values ('" + lblNote1.Text.ToString() + "','" + res + "','" + tempIdNo.Value + "','" + order_id + "','" + response + "')", con);
                                    cmd.Connection = con;
                                    cmd.ExecuteNonQuery();
                                    con.Close();

                                    

                                    string txnMessage = string.Empty;
                                    string txnStatus = string.Empty;


                                    if (status_code == "0")
                                    {
                                        txnMessage = "Success";
                                        txnStatus = "Success";
                                    }
                                    else
                                    {
                                        txnMessage = "Fail/Pending";
                                        txnStatus = "Fail/Pending";
                                    }

                                    //0|Shipped|310006885641|1612950050946|Y|AASHRITHA VANGARI|||||||MasterCard Debit Card|INR|2021-02-10 15:10:19.357||PC|NA||AVN|59.97.236.2||28328490||OPTDBCRD||||||||Shipped|2021-02-10 15:11:27.347|0.0|||1.0|1.0|0.0|0.0|0.01|1.0|0.0|0

                                    //Format:
//status|order_status|reference_no|order_bank_ref_no|order_bank_response| 
//order_bill_name|order_bill_email|order_bill_address|order_bill_city|order_bill_state|order_bill_co 
//untry|order_bill_telephone_no|order_bill_city_zip|order_card_name|order_currency|order_date_ 
//time|order_delivery_details|order_device_type|order_fraud_status|order_gateway_id|order_iP|or 
//der_no| order_notes|order_option_type|order_shiping_name|order_ship_email|order_ship_address|order
//_ship_city|order_ship_state|order_ship_country|order_ship_telephone_no|order_ship_zip|order_ 
//status_date_time|order_TDS|order_amount|order_capture_amount|order_discount|order_fee_fla 
//t|order_fee_perc|order_fee_perc_value|order_gross_amount|order_tax|Merchant_param1|
//Merchant_param2| Merchant_param3| Merchant_param4| Merchant_param5|

//Example:
//0|Successful|204000163514|068406|Transaction Successful|Shashi|gzpmgexii@i.softbank.jp|Room no 
//1101, near Railway station Ambad|Indore|MP|India|9595226054|425001|MasterCard|INR|2015-
//09-18 12:53:40.407||PC|NA|ICICI|192.168.2.182|64807533|order will be 
//shipped|OPTCRDC|Shashi||Room no 1101, near Railway station 
//Ambad|Indore|MP|India|9595226054|425001|2015-09-18 
//12:54:15.357|0.0|1.0|0.0|0.0|0.0|2.3|0.02|1.0|0.0028|Mobile No9595226054|Flight from 
//Dehli|ToMumbai|Mobile No9595226054|Mobile No9595226054|

                                    div_Studentdetail.Visible = true;
                                    FeeCollectionController objFeesCnt = new FeeCollectionController();
                                    if (status_code == "0")// && status_msg.ToUpper().Contains("SUCCESS")
                                    {

                                        int retval = 0;
                                        if (rec_code == "PRF")
                                        {
                                            retval = objFeesCnt.OnlinePaymentUpdationRecon(aptransid, orderno, orderamt, status_code, txnStatus, lblNote1.Text, txnMessage, transid, 1, Convert.ToInt32(tempIdNo.Value), Convert.ToInt32(ddlSession.SelectedValue), rec_code);
                                        }
                                        else if (rec_code == "RF")
                                        {
                                            retval = objFeesCnt.OnlinePaymentUpdationRecon(aptransid, orderno, orderamt, status_code, txnStatus, lblNote1.Text, txnMessage, transid, 2, Convert.ToInt32(tempIdNo.Value), Convert.ToInt32(ddlSession.SelectedValue), rec_code);
                                        }
                                        else if (rec_code == "AEF")
                                        {
                                            retval = objFeesCnt.OnlinePaymentUpdationRecon(aptransid, orderno, orderamt, status_code, txnStatus, lblNote1.Text, txnMessage, transid, 3, Convert.ToInt32(tempIdNo.Value), Convert.ToInt32(ddlSession.SelectedValue), rec_code);
                                        }
                                        else if (rec_code == "CF")
                                        {
                                            retval = objFeesCnt.OnlinePaymentUpdationRecon(aptransid, orderno, orderamt, status_code, txnStatus, lblNote1.Text, txnMessage, transid, 4, Convert.ToInt32(tempIdNo.Value), Convert.ToInt32(ddlSession.SelectedValue), rec_code);
                                        }
                                        else if (rec_code == "TF" || rec_code == "TPF" || rec_code == "HF")
                                        {
                                            retval = objFeesCnt.OnlinePaymentUpdationRecon(aptransid, orderno, orderamt, status_code, txnStatus, lblNote1.Text, txnMessage, transid, 5, Convert.ToInt32(tempIdNo.Value), Convert.ToInt32(ddlSession.SelectedValue), rec_code);
                                        }
                                        if (retval == -99)
                                        {
                                            objCommon.DisplayMessage(updLists, "Error occured", this.Page);
                                            return;
                                        }
                                        else
                                        {
                                            BindListView();
                                            //objCommon.DisplayMessage(updLists, "Request success", this.Page);
                                        }
                                       count = 0;
                                        objCommon.DisplayUserMessage(updLists, "Successful Transaction (Success)", this);
                                        lblNote1.Text = string.Empty;
                                        return;
                                    }
                                    else
                                    {
                                        //objCommon.DisplayUserMessage(updLists, "Unable to Process !!", this);
                                        count = count + 1;
                                    }

                                }
                                else if (!"".Equals(status) && status.Equals("1"))
                                {
                                    Console.WriteLine("failure response from ccAvenues: " + encRes);
                                }

                            }

                        }
                        catch (Exception exp)
                        {
                            Response.Write("Exception " + exp);

                        }
                       
                    }

                    if (count > 0)
                    {
                        objCommon.DisplayUserMessage(updLists, "Failed/Cancel/Pending/Invalid Transactions", this);
                    }

                }
            }
        }
    }



    private string postPaymentRequestToGateway(String queryUrl, String urlParam)
    {

        String message = "";
        try
        {
            StreamWriter myWriter = null;// it will open a http connection with provided url
            WebRequest objRequest = WebRequest.Create(queryUrl);//send data using objxmlhttp object
            objRequest.Method = "POST";
            //objRequest.ContentLength = TranRequest.Length;
            objRequest.ContentType = "application/x-www-form-urlencoded";//to set content type
            myWriter = new System.IO.StreamWriter(objRequest.GetRequestStream());
            myWriter.Write(urlParam);//send data
            myWriter.Close();//closed the myWriter object

            // Getting Response
            System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();//receive the responce from objxmlhttp object 
            using (System.IO.StreamReader sr = new System.IO.StreamReader(objResponse.GetResponseStream()))
            {
                message = sr.ReadToEnd();
                //Response.Write(message);
            }
        }
        catch (Exception exception)
        {
            Console.Write("Exception occured while connection." + exception);
        }
        return message;

    }

    private NameValueCollection getResponseMap(String message)
    {
        NameValueCollection Params = new NameValueCollection();
        if (message != null || !"".Equals(message))
        {
            string[] segments = message.Split('&');
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
        }
        return Params;
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlCollege.Enabled = true;
        ddlSession.Enabled = true;
        ddlReceiptType.Enabled = true;
        txtAppID.Enabled = true;
        Response.Redirect(Request.Url.ToString());

    }


   
}