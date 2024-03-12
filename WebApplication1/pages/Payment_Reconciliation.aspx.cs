using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

public partial class ACADEMIC_Payment_Reconcilation : System.Web.UI.Page
{
    Common objCommon = new Common();
    MappingController objmp = new MappingController();

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
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();
                FillDowpDowns();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

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
                Response.Redirect("~/notauthorized.aspx?page=Payment_Reconciliation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Payment_Reconciliation.aspx");
        }
    }
    protected void FillDowpDowns()
    {
        try
        {
            DataSet ds = new DataSet();
           // ds = objmp.BindMultiDropDown("0");
            ds = objmp.BindMultiDropDown(Convert.ToString(Request.QueryString["pageno"]));


            if (ds.Tables[0].Rows.Count > 0)
            {
                lblDynamicPageTitle.InnerText = ds.Tables[0].Rows[0]["AL_LINK"].ToString();
            }

            ddlStudyLevel.Items.Clear();
            ddlStudyLevel.Items.Add("Please Select");
            ddlStudyLevel.SelectedItem.Value = "0";

            ddlIntake.Items.Clear();
            ddlIntake.Items.Add("Please Select");
            ddlIntake.SelectedItem.Value = "0";

            ddlPaymentStatus.Items.Clear();
            ddlPaymentStatus.Items.Add("Please Select");
            ddlPaymentStatus.SelectedItem.Value = "0";

            ddlBank.Items.Clear();
            ddlBank.Items.Add("Please Select");
            ddlBank.SelectedItem.Value = "0";

            ddlSemester.Items.Clear();
            ddlSemester.Items.Add("Please Select");
            ddlSemester.SelectedItem.Value = "0";

            ddlProgram.Items.Clear();

            ddlSession.Items.Clear();
            ddlSession.Items.Add("Please Select");
            ddlSession.SelectedItem.Value = "0";

            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                ddlStudyLevel.DataSource = ds.Tables[1];
                ddlStudyLevel.DataValueField = "UA_SECTION";
                ddlStudyLevel.DataTextField = "UA_SECTIONNAME";
                ddlStudyLevel.DataBind();
            }
            if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
            {
                ddlIntake.DataSource = ds.Tables[2];
                ddlIntake.DataValueField = "BATCHNO";
                ddlIntake.DataTextField = "BATCHNAME";
                ddlIntake.DataBind();
            }
            if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
            {
                ddlPaymentStatus.DataSource = ds.Tables[3];
                ddlPaymentStatus.DataValueField = "SRNO";
                ddlPaymentStatus.DataTextField = "RECON_TYPE";
                ddlPaymentStatus.DataBind();
            }
            if (ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
            {
                ddlBank.DataSource = ds.Tables[4];
                ddlBank.DataValueField = "BANKNO";
                ddlBank.DataTextField = "BANKNAME";
                ddlBank.DataBind();
            }
            if (ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
            {
                ddlSemester.DataSource = ds.Tables[5];
                ddlSemester.DataValueField = "SEMESTERNO";
                ddlSemester.DataTextField = "SEMESTERNAME";
                ddlSemester.DataBind();
            }
            if (ds.Tables[6] != null && ds.Tables[6].Rows.Count > 0)
            {
                ddlProgram.DataSource = ds.Tables[6];
                ddlProgram.DataValueField = "DEGREE_BRANCH_NO";
                ddlProgram.DataTextField = "PROGRAMNAME";
                ddlProgram.DataBind();
            }
            if (ds.Tables[7] != null && ds.Tables[7].Rows.Count > 0)
            {
                ddlSession.DataSource = ds.Tables[7];
                ddlSession.DataValueField = "SESSIONNO";
                ddlSession.DataTextField = "SESSION_NAME";
                ddlSession.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string BindDynamicDataTable(int CheckStatus)
    {
        MappingController objmp = new MappingController();
        DataTable dataTable = new DataTable();

        try
        {
            DataSet ds = new DataSet();
            if (CheckStatus == 1)
                ds = objmp.GetApplicationCount();
            if (CheckStatus == 2)
                ds = objmp.GetEnrollmentCount();
            if (CheckStatus == 3)
                ds = objmp.GetSemesterCount();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dataTable = ds.Tables[0]; // Assign the first table from the DataSet to dataTable
            }
        }
        catch (Exception ex)
        {

        }

        string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
        return json;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ShowBindDynamicDataTable(int CheckStatus, string Dynamicvalues, int FilterOption)
    {
        MappingController objmp = new MappingController();
        Common objCommon = new Common();
        DataTable dataTable = new DataTable();
        DataSet FillDropDown = new DataSet();
        try
        {
            string[] Dynamicvalue = Dynamicvalues.Split('$');

            DataSet ds = new DataSet();
            if (CheckStatus == 1)
                if (FilterOption == 1)
                    ds = objmp.GETONLINEADM(Convert.ToInt32(Dynamicvalue[1]), Convert.ToInt32(Dynamicvalue[2]), Convert.ToInt32(Dynamicvalue[0]), Convert.ToInt32(Dynamicvalue[3]));
                else
                    ds = objmp.GETORDERID(Convert.ToInt32(Dynamicvalue[1]), Convert.ToInt32(Dynamicvalue[0]), Convert.ToInt32(0));
            else if (CheckStatus == 2)
                if (FilterOption == 1)
                    ds = objmp.GETREGLISTONE(Convert.ToInt32(Dynamicvalue[1]), Convert.ToInt32(Dynamicvalue[2]), Convert.ToInt32(Dynamicvalue[0]), Convert.ToInt32(Dynamicvalue[3]),0,1000,string.Empty);
                else
                    ds = objmp.getreglisttwo(Convert.ToInt32(Dynamicvalue[1]), Convert.ToInt32(Dynamicvalue[0]), Convert.ToInt32(0));
            else if (CheckStatus == 3)
                if (FilterOption == 1)
                    ds = objmp.GETSEMESTERDATAOFF(Convert.ToInt32(Dynamicvalue[1]), Convert.ToInt32(Dynamicvalue[3]), Convert.ToInt32(Dynamicvalue[0]), Convert.ToInt32(Dynamicvalue[2]), Convert.ToInt32(Dynamicvalue[4]), Dynamicvalue[5], Dynamicvalue[6]);
                else
                    ds = objmp.GETSEMESTERDATA(Convert.ToInt32(Dynamicvalue[1]), Convert.ToInt32(Dynamicvalue[2]), Convert.ToInt32(Dynamicvalue[0]));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dataTable = ds.Tables[0]; // Assign the first table from the DataSet to dataTable
                FillDropDown = objCommon.FillDropDown("ACD_BANK", "DISTINCT BANKNO", "BANK_SHORTNAME", "BANKNO > 0", "BANKNO");
            }
        }
        catch (Exception ex)
        {

        }

        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "dataTable", dataTable },
            { "fillDropDown", FillDropDown }
        };
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        return json;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static string SubmitDynamicDataTable(int CheckStatus, int FilterOption ,string Payment)
    public static string SubmitDynamicDataTable(string Payment,int CommanDType)
    {
        MappingController objmp = new MappingController();
        Common objCommon = new Common();
        DataTable dataTable = new DataTable();
        DataSet FillDropDown = new DataSet();
        int Result = 0;
        try
        {
            var serializeData = JsonConvert.DeserializeObject<List<StudentFees.Payment>>(Payment);
            DataSet ds = new DataSet();
            DataTable DtPaymnet = new DataTable();
            DtPaymnet.Clear();
            if (CommanDType == 1)
            {
                DtPaymnet.Columns.Add("USERNO");
                DtPaymnet.Columns.Add("AMOUNT");
                DtPaymnet.Columns.Add("DATE");
                DtPaymnet.Columns.Add("STATUS");
                DtPaymnet.Columns.Add("REMARK");
                DtPaymnet.Columns.Add("BANK");
                foreach (var data in serializeData)
                {
                    DataRow QueData = DtPaymnet.NewRow();
                    QueData["USERNO"] = data.USERNO.TrimEnd('$');
                    QueData["AMOUNT"] = data.AMOUNT.TrimEnd('$');
                    QueData["DATE"] = data.DATE.TrimEnd('$');
                    QueData["STATUS"] = data.STATUS.TrimEnd('$');
                    QueData["REMARK"] = data.REMARK.TrimEnd('$');
                    QueData["BANK"] = data.BANK.TrimEnd('$');
                    DtPaymnet.Rows.Add(QueData);
                }
            }
            else
            {
                DtPaymnet.Columns.Add("USERNO");
                DtPaymnet.Columns.Add("AMOUNT");
                DtPaymnet.Columns.Add("DATE");
                DtPaymnet.Columns.Add("STATUS");
                DtPaymnet.Columns.Add("REMARK");
                DtPaymnet.Columns.Add("DCRTEMPNO");
                DtPaymnet.Columns.Add("BANK");
                foreach (var data in serializeData)
                {
                    DataRow QueData = DtPaymnet.NewRow();
                    QueData["USERNO"] = data.USERNO.TrimEnd('$');
                    QueData["AMOUNT"] = data.AMOUNT.TrimEnd('$');
                    QueData["DATE"] = data.DATE.TrimEnd('$');
                    QueData["STATUS"] = data.STATUS.TrimEnd('$');
                    QueData["REMARK"] = data.REMARK.TrimEnd('$');
                    QueData["DCRTEMPNO"] = data.DCRTEMPNO.TrimEnd('$');
                    QueData["BANK"] = data.BANK.TrimEnd('$');
                    DtPaymnet.Rows.Add(QueData);
                }
            }
            if (CommanDType == 1)
            {
                Result = objmp.InsertOnlineRegistration(DtPaymnet, 1);
            }
            else if (CommanDType == 2)
            {
                Result = objmp.InsertAdmissionHigherRegistration(DtPaymnet, 1);
            }
            else if (CommanDType == 3)
            {
                Result = objmp.InsertAdmissionHigherRegistration(DtPaymnet, 1);
            }
        }
        catch (Exception ex)
        {

        }
        string json = JsonConvert.SerializeObject(Result, Formatting.Indented);
        return json;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetBlobImage(string ImageName)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(ImageName))
            {
                string fileExtension = Path.GetExtension(ImageName).ToUpper();
                if (fileExtension == ".JPG" || fileExtension == ".JPEG" || fileExtension == ".PNG" || fileExtension == ".PDF")
                {
                    // Retrieve connection string and container name from configuration
                    string connectionString = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"];
                    string containerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"];

                    // Create a CloudStorageAccount object
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

                    // Create a CloudBlobClient object
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                    // Get reference to the container
                    CloudBlobContainer container = blobClient.GetContainerReference(containerName);

                    // Get reference to the Blob
                    CloudBlockBlob blob = container.GetBlockBlobReference(ImageName);

                    // Download the Blob content
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        blob.DownloadToStream(memoryStream);
                        byte[] bytes = memoryStream.ToArray();
                        return Convert.ToBase64String(bytes);
                    }
                }
                else
                {
                    return "InvalidFileType";
                }
            }
            else
            {
                return "EmptyImageName";
            }
        }
        catch (Exception ex)
        {
            // Log the exception for debugging
            return "Error: " + ex.Message;
        }
    }
}
