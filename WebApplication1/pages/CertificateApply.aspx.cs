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
using System.Data;
using System.IO;
using System.Collections.Specialized;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DotNetIntegrationKit;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using System.Xml;
using System.Net;
using _NVP;
using EASendMail;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using IITMS.SQLServer.SQLDAL;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class CertificateApply : System.Web.UI.Page
{
    DocumentContro doc = new DocumentContro();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    string username = string.Empty;
    UserController user = new UserController();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
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
            if (Session["userno"] == null || Session["username"] == null || Session["idno"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                this.CheckPageAuthorization();

                if (Session["usertype"].ToString() == "2" )
                {
                    DivData.Visible = true;
                }
                else
                {
                    // divShow.Visible=false;
                }

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_MASTER", "DOC_NO", "DOC_NAME", "", "");
                BindData();
                //objCommon.SetLabelData("0");//for label
                //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
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
                Response.Redirect("~/notauthorized.aspx?page=CertificateApply.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CertificateApply.aspx");
        }
    }

    protected void BindData()
    {
        string SP_Name2 = "PKG_GET_APPLY_FOR_STUDENT_DOCUMENT_INFO";
        string SP_Parameters2 = "@P_IDNO,@P_FILTER";
        string Call_Values2 = "" + Convert.ToInt32(Session["idno"]) + "," + 0 + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (ds.Tables[0].Rows.Count > 0 || ds.Tables[0].Rows.Count == null)
        {
            lblStudentName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
            lblStdID.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
            lblDOB.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
            lblGender.Text = ds.Tables[0].Rows[0]["GENDER"].ToString();
            lblMobileNo.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
            lblEmailID.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
            lblProgram.Text = ds.Tables[0].Rows[0]["PROGRAM"].ToString();
            lblSem.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
            ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();

            if (ds.Tables[0].Rows[0]["PHOTO"].ToString() != "")
            {
                byte[] bytes = (byte[])ds.Tables[0].Rows[0]["PHOTO"];
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                imgPhoto.ImageUrl = "data:image/png;base64," + base64String;
            }
            
            


        }
        if (ds.Tables[1].Rows.Count > 0 || ds.Tables[1].Rows.Count == null)
        {
            lvDocument.DataSource = ds.Tables[1];
            lvDocument.DataBind();
        }
        else
        {
            lvDocument.DataSource = null;
            lvDocument.DataBind();
        }
    }

    protected void ddlDocument_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal Amount = Convert.ToDecimal(objCommon.LookUp("ACD_DOCUMENT_MASTER", "AMOUNT", "DOC_NO=" + Convert.ToInt32(ddlDocument.SelectedValue)));
        if (Amount == 0)
        {
            btnSubmit.Visible = true;
            btnSubmitPay.Visible = false;
            DivButton.Visible = true;
        }
        else
        {
            btnSubmitPay.Visible = true;
            btnSubmit.Visible = false;
            DivButton.Visible = true;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string Regno = lblStdID.Text;
        int Idno = Convert.ToInt32(Session["idno"]);
        int SemesterNo = Convert.ToInt32(ViewState["SEMESTERNO"]);
        decimal Amount = Convert.ToDecimal(objCommon.LookUp("ACD_DOCUMENT_MASTER", "AMOUNT", "DOC_NO=" + Convert.ToInt32(ddlDocument.SelectedValue)));

        string SP_Name1 = "PKG_ACAD_INSERT_APPLY_FOR_DOCUMENT";
        string SP_Parameters1 = "@P_REGNO,@P_IDNO,@P_SEMESTERNO,@P_DOC_NO,@P_PAID_AMOUNT,@P_PAID_STATUS,@P_OUT";
        string Call_Values1 = "" + Regno.ToString() + "," + Idno + "," + SemesterNo + "," + Convert.ToInt32(ddlDocument.SelectedValue) + "," + Amount + "," + 0 + "," + 0 + "";

        string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);
        if (que_out1 == "1")
        {
            BindData();
            objCommon.DisplayMessage(this, "Record Saved Successfully.. !!", this.Page);
            return;
        }
        else if (que_out1 == "5")
        {

            objCommon.DisplayMessage(this, "Record Alredy Exists.. !!", this.Page);
            return;
        }
        else
        {
            objCommon.DisplayMessage(this, "Server Error.. !!", this.Page);
            return;
        }
    }
    private void CreateCustomerRef()
    {
        Random rnd = new Random();
        int ir = rnd.Next(01, 10000);
        string Orderid = Convert.ToString((Convert.ToInt32(Session["idno"].ToString())) + (Convert.ToInt32(ddlDocument.SelectedValue)) + (Convert.ToString(ViewState["SEMESTERNO"].ToString())) + ir);
        txtOrderID.Text = Orderid.ToString();
        ViewState["OrderId"] = txtOrderID.Text;
    }
    protected void btnSubmitPay_Click(object sender, EventArgs e)
    {
        txtAmountPaid.Text = "";txtServiceCharge.Text = "";
        txtTotal.Text = "";lblStdID.Text = "";
        CreateCustomerRef();
        decimal TotalAmt = 0.00m;
        decimal Amount = Convert.ToDecimal(objCommon.LookUp("ACD_DOCUMENT_MASTER", "AMOUNT", "DOC_NO=" + Convert.ToInt32(ddlDocument.SelectedValue)));
        decimal Service_charge = Convert.ToDecimal(objCommon.LookUp("reff", "SERVICE_CHARGE", ""));
        txtAmountPaid.Text = Amount.ToString();
        decimal Service = (Amount * Service_charge) / 100;
        txtServiceCharge.Text = Service.ToString().Substring(0, TotalAmt.ToString().Length - 0);
        TotalAmt = Amount + Service;
        txtTotal.Text = TotalAmt.ToString().Substring(0, TotalAmt.ToString().Length - 2);
        ViewState["TotalAmount"] = txtTotal.Text;
        string Regno = lblStdID.Text;
        int Idno = Convert.ToInt32(Session["idno"]);
        int SemesterNo = Convert.ToInt32(ViewState["SEMESTERNO"]);
        string SP_Name1 = "PKG_ACAD_INSERT_APPLY_FOR_DOCUMENT";
        string SP_Parameters1 = "@P_REGNO,@P_IDNO,@P_SEMESTERNO,@P_DOC_NO,@P_PAID_AMOUNT,@P_PAID_STATUS,@P_ORDERID,@P_SERVICE_CHARGE,@P_OUT";
        string Call_Values1 = "" + Regno.ToString() + "," + Idno + "," + SemesterNo + "," + Convert.ToInt32(ddlDocument.SelectedValue) + "," + Amount + "," + 1 + "," + txtOrderID.Text + "," + Convert.ToDecimal(txtServiceCharge.Text) + "," + 0 + "";

        string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);
        if (que_out1 == "1")
        {
            BindData();
            SendTransaction();
        }
        else if (que_out1 == "5")
        {

            objCommon.DisplayMessage(this, "Record Alredy Exists.. !!", this.Page);
            return;
        }
        else
        {
            objCommon.DisplayMessage(this, "Server Error.. !!", this.Page);
            return;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#myModalPay').modal('show')", true);
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

        // formdata.Add("interaction.returnUrl", "http://localhost:55158/PresentationLayer/OnlineResponse.aspx");


        formdata.Add("interaction.returnUrl", System.Configuration.ConfigurationManager.AppSettings["ReturnUrl"]);

        formdata.Add("interaction.operation", "PURCHASE");
        formdata.Add("order.id", ViewState["OrderId"].ToString());
        formdata.Add("order.currency", "LKR");
        formdata.Add("order.amount", (ViewState["TotalAmount"].ToString()));
        formdata.Add("order.description", (Session["idno"].ToString()));
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

        foreach (String key in respValues)
        {
            if (key == "session.id")
            {
                Session["ERPConvocationPaymentSession"] = respValues[key];
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
        Convert.ToString(Session["ERPsessionversion"]) + "," + Convert.ToString(ViewState["OrderId"].ToString()) + ",0";

        string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);


        HtmlTable dTable = new HtmlTable();
        dTable.Width = "100%";
        dTable.CellPadding = 2;
        dTable.CellSpacing = 0;
        dTable.Border = 1;
        dTable.BorderColor = "#cccccc";

        int shade = 0;
        foreach (String key in respValues)
        {
            HtmlTableRow dtRow = new HtmlTableRow();
            if (++shade % 2 == 0) dtRow.Attributes.Add("class", "shade");

            HtmlTableCell dtLeft = new HtmlTableCell();
            HtmlTableCell dtRight = new HtmlTableCell();

            dtLeft.Align = "right";
            dtLeft.Width = "50%";
            dtLeft.InnerHtml = "<strong><i>" + key + ":</i></strong>";  // add field name to table
            if (key == "session.id")
            {
                Session["ERPConvocationPaymentSession"] = respValues[key];
            }
            dtRight.Width = "50%";
            dtRight.InnerText = respValues[key]; // add value to table

            dtRow.Controls.Add(dtLeft);
            dtRow.Controls.Add(dtRight);
            dTable.Controls.Add(dtRow);
        }
    }

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }
    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }
    private string GetContentType(string fileName)
    {
        string strcontentType = "application/octetstream";
        string ext = System.IO.Path.GetExtension(fileName).ToLower();
        Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
        if (registryKey != null && registryKey.GetValue("Content Type") != null)
            strcontentType = registryKey.GetValue("Content Type").ToString();
        return strcontentType;
    }
    protected void btnDownload_Click(object sender, System.EventArgs e)
    {
        LinkButton STUD = sender as LinkButton;
        int IDNO = int.Parse(STUD.CommandArgument);
        string FileName = STUD.CommandName;

        string Url = string.Empty;
        string directoryPath = string.Empty;
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
        CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
        string directoryName = "~/DownloadImg" + "/";
        directoryPath = Server.MapPath(directoryName);

        if (!Directory.Exists(directoryPath.ToString()))
        {

            Directory.CreateDirectory(directoryPath.ToString());
        }
        CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
        string img = FileName.ToString();
        var ImageName = FileName;
        if (img.ToString() != string.Empty || img.ToString() != "")
        {

            string extension = Path.GetExtension(img.ToString());
            DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
            if (extension == ".pdf")
            {
                ImageViewer.Visible = false;
                ltEmbed.Visible = true;
                imageViewerContainer.Visible = false;

                var Newblob = blobContainer.GetBlockBlobReference(ImageName);

                string filePath = directoryPath + "\\" + ImageName;


                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }

                Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                hdfImagePath.Value = null;

                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"765px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
           
             
            }
        }
    }
}