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
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using _NVP;
using System.Collections.Specialized;
using EASendMail;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;

using Microsoft.WindowsAzure.Storage;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.Script.Serialization;
using RestSharp;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
//using DynamicPDFViewer;

public partial class Applicant_Preview : System.Web.UI.Page
{
    Common objCommon = new Common();
    UserController user = new UserController();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    int idno = 0; decimal TotalSum = 0; String tspl_txn_id = string.Empty;
    string session = string.Empty;
    IITMS.UAIMS.BusinessLayer.BusinessEntities.DocumentAcad objdocument = new DocumentAcad();
    DocumentControllerAcad objdocContr = new DocumentControllerAcad();
    NewUser objnu = new NewUser();
    NewUserController objnuc = new NewUserController();
    StudentFees objStudentFees = new StudentFees();
    NewUserController ObjNuc = new NewUserController();
    private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string username = string.Empty;
    //PDFDynamicViewer pdf = new PDFDynamicViewer();

    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
    string blob_ContainerNameAdd = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameAdd"].ToString();
    string blob_ContainerNamePhoto = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNamePhoto"].ToString();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

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
                    if (Convert.ToString(ViewState["action"]) == "")
                    {
                        ViewState["action"] = "add";
                    }

                    // Check User Authority 

                    Page.Title = Session["coll_name"].ToString();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                    if (Request.QueryString["userno"].ToString() != null || Request.QueryString["userno"].ToString() != "")
                    {
                        string idno = (objCommon.LookUp("ACD_STUDENT", "IDNO", "USERNO=" + (Request.QueryString["userno"]) + ""));
                        ViewState["ID_NO"] = idno;
                        ViewState["USERNO"] = Request.QueryString["userno"].ToString();
                    }


                    this.CheckPageAuthorization();
                    BindStudentInfo();
                    BindStudentDoclist();

                    Page.Title = Session["coll_name"].ToString();

                }

            }
            string FinalStatus = "";
            FinalStatus = objCommon.LookUp("ACD_STUDENT", "ISNULL(ENROLLNO,0)", "ISNULL(CAN,0) = 0 AND ISNULL(ADMCAN,0) = 0 AND IDNO=" + Convert.ToInt32(ViewState["ID_NO"]));
            if (FinalStatus.ToString() == "0" || FinalStatus.ToString() == string.Empty)
            {
                lnkSendEmail.Enabled = true;
                lnkGeneratereport.Visible = false;
                lnkPrintReport.Visible = false;
                btnFrontBackReport.Visible = false;
            }
            else
            {
                lnkSendEmail.Enabled = true;
                lnkGeneratereport.Visible = true;
                lnkPrintReport.Visible = true;
                //btnFrontBackReport.Visible = true;
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


    protected void BindStudentInfo()
    {
        DataSet ds = null;
        string SP_Name2 = "PKG_GET_STUDENT_PROGRAM_CONFIRMATION_STATUS";
        string SP_Parameters2 = "@P_COMMAND_TYPE,@P_USERNO";
        string Call_Values2 = "" + 1 + "," + Convert.ToInt32(ViewState["USERNO"].ToString()) + "";
        ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
        {
            lblRegNo.Text = ds.Tables[0].Rows[0]["USERNAME"].ToString();
            lblStudName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            lblSex.Text = ds.Tables[0].Rows[0]["GENDER"].ToString();
            lblYear.Text = ds.Tables[0].Rows[0]["YEAR_NAME"].ToString();
            lblBatch.Text = ds.Tables[0].Rows[0]["BATCHNAME"].ToString();
            lblSemester.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
            lblMobileNo.Text = ds.Tables[0].Rows[0]["MOBILENO"].ToString();
            lblEmailID.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
            lblStudentType.Text = ds.Tables[0].Rows[0]["STUDENT_TYPE"].ToString();
            lblFaculty.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
            lblProgram.Text = ds.Tables[0].Rows[0]["PROGRAM"].ToString();
            lblAcceptanceDate.Text = ds.Tables[0].Rows[0]["PROGRAM_ACCEPT_DATE"].ToString();
            ViewState["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            ViewState["DEGREENO"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            ViewState["BRANCHNO"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
            ViewState["ADMBATCH"] = ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
            ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
            ViewState["YEAR"] = ds.Tables[0].Rows[0]["YEAR_NAME"].ToString();
            ViewState["STUDNAME"] = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            ViewState["USERNAME"] = ds.Tables[0].Rows[0]["USERNAME"].ToString();
            ViewState["PROGRAM"] = ds.Tables[0].Rows[0]["PROGRAM"].ToString();
            ViewState["EMAILID"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();

        }
        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
        {

            lvStudentFees.DataSource = ds.Tables[1];
            lvStudentFees.DataBind();
        }
        else
        {
            lvStudentFees.DataSource = null;
            lvStudentFees.DataBind();
        }
    }


    private void CheckPageAuthorization()
    {
        // Request.QueryString["pageno"] = "ACADEMIC/EXAMINATION/ExamRegistration_New.aspx ?pageno=1801";
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=OnlinePayment_StudentLogin.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OnlinePayment_StudentLogin.aspx");
        }
    }

    public void BindStudentDoclist()
    {
        try
        {
            int CountDoc = 0;
            int CountUplo = 0;
            int USERNO1 = Convert.ToInt32(Session["USERNO"].ToString());// ((UserDetails)(Session["user"])).UserNo;
            DataSet ds = new DataSet();
            string SP_Name1 = "PKG_GET_STUDENT_PROGRAM_CONFIRMATION_STATUS";
            string SP_Parameters1 = "@P_USERNO,@P_COMMAND_TYPE";
            string Call_Values1 = "" + Convert.ToInt32(ViewState["USERNO"]) + "," + 2 + "";
            ds = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvDocument.DataSource = ds;
                lvDocument.DataBind();
                lvDocument.Visible = true;
                //divlkuploaddocument.Attributes.Add("class", "finished");
                foreach (ListViewDataItem item in lvDocument.Items)
                {
                    LinkButton lnk = item.FindControl("lnkViewDoc") as LinkButton;
                    Label lblStatus = item.FindControl("lblStatus") as Label;
                    Label lblRemark = item.FindControl("lblRemark") as Label;
                    Label lblDocFileName = item.FindControl("lblDocFileName") as Label;
                    Label lblDocNo = item.FindControl("lblDocNo") as Label;
                    Label lblMande = item.FindControl("lblMande") as Label;

                    DropDownList ddlstatus = item.FindControl("ddlstatus") as DropDownList;
                    if (lblStatus.Text == "1")
                    {
                        CountDoc++;
                    }
                    ddlstatus.SelectedValue = lblStatus.Text;
                    if (lblDocFileName.Text == string.Empty)
                    {
                        lnk.Visible = false;
                    }
                    else
                    {
                        CountUplo++;
                        lnk.Visible = true;
                    }

                }
            }
            else
            {
                lvDocument.DataSource = null;
                lvDocument.DataBind();
                lvDocument.Visible = false;
            }
            if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
            {
                lblstatusConfi.Text = "Confirm";
                lblEnrollmentno.Text = ds.Tables[2].Rows[0]["ENROLLNO"].ToString();
                lnkCertiAdmis.Visible = true;
                btnDocumentVarify.Visible = false;
            }
            else
            {
                lnkCertiAdmis.Visible = false;
            }
            if (CountUplo > 0)
            {
                if (CountDoc == CountUplo)
                {
                    DivButton.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ListofDocuments.bindlist-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }


    protected void lnkViewDoc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            int value = int.Parse(lnk.CommandArgument);
            string FileName = lnk.CommandName.ToString();
            if (value == 0)
            {
                irm1.Visible = false;
                ImageViewer.Visible = true;
                ltEmbed.Visible = false;
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
                CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerNameAdd);
                string img = lnk.CommandName.ToString();
                var ImageName = img;
                if (img != null || img != "")
                {
                    var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                    string filePath = directoryPath + "\\" + ImageName;
                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    ViewState["filePath_Show"] = filePath;
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);

                    byte[] bytes = System.IO.File.ReadAllBytes(filePath);

                    string Base64String = Convert.ToBase64String(bytes);
                    Session["Base64String"] = Base64String;
                    Session["Base64StringType"] = "image/png";
                    hdfImagePath.Value = "data:image/png;base64," + Base64String;
                    imageViewerContainer.Visible = false;
                    ImageViewer.ImageUrl = string.Format("data:image/png;base64," + Base64String);
                    ScriptManager.RegisterClientScriptBlock(this.updModel, this.GetType(), "Popup", "$('#myModal22').modal('show')", true);
                    // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
                }
            }
            else
            {
                imageViewerContainer.Visible = false;
                irm1.Visible = true;
                ImageViewer.Visible = false;
                ltEmbed.Visible = false;
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
                CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerNameAdd);
                string img = lnk.CommandName.ToString();
                var ImageName = img;
                if (img != null || img != "")
                {

                    DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

                    var Newblob = blobContainer.GetBlockBlobReference(ImageName);

                    string filePath = directoryPath + "\\" + ImageName;


                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    ViewState["filePath_Show"] = filePath;
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);

                    byte[] bytes = System.IO.File.ReadAllBytes(filePath);

                    string Base64String = Convert.ToBase64String(bytes);
                    Session["Base64String"] = Base64String;
                    Session["Base64StringType"] = "application/pdf";
                    ScriptManager.RegisterClientScriptBlock(this.updModel, this.GetType(), "Popup", "$('#myModal22').modal('show')", true);
                    //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal(show);});", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> Swal.fire({html: 'Sorry, File not found !!!', icon: 'warning' });</script>", false);

                }

            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void lnkCloseModel_Click(object sender, EventArgs e)
    {
        try
        {
            if ((System.IO.File.Exists(Convert.ToString(ViewState["filePath_Show"]))))
            {
                System.IO.File.Delete(Convert.ToString(ViewState["filePath_Show"]));
            }
        }
        catch (Exception ex)
        {

        }
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

    //ADDED BY ROSHAN PATIL 17/07/2023
    protected void EmailSmsWhatssppSend(int Page_No, string toSendAddre, string Name, string USERNO)
    {
        EmailSmsWhatsaap Email = new EmailSmsWhatsaap();
        DataSet ds = objCommon.FillDropDown("ACCESS_LINK", "isnull(EMAIL,0) EMAIL,isnull(SMS,0) as SMS,isnull(WHATSAAP,0) as WHATSAAP", "", "AL_NO=" + Page_No, "");
        string Res = ""; string Message = "";
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["EMAIL"].ToString()) == 1)//For Email Send 
        {
            UserController objUC = new UserController();
            DataSet dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", USERNO, 012879);
            if (dsUserContact.Tables[1] != null && dsUserContact.Tables[1].Rows.Count > 0)
            {

                string Subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                Message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                string toSendAddress = toSendAddre.ToString();// 
                MailMessage msgsPara = new MailMessage();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Message);
                msgsPara.BodyEncoding = Encoding.UTF8;
                msgsPara.Body = Convert.ToString(sb);
                msgsPara.Body = msgsPara.Body.Replace("{User}", Name.ToString());
                MemoryStream Attachment = null; string AttachmentName = "";
                Task<string> task = Email.Execute(msgsPara.Body, toSendAddress, Subject, Attachment, AttachmentName.ToString());
                Res = task.Result;

            }
        }
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["SMS"].ToString()) == 1) //For SMS Send 
        {

        }
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["WHATSAAP"].ToString()) == 1)//For Whatsaap Send 
        {

        }
    }




    protected void lnkViewSlip_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            int value = int.Parse(lnk.CommandArgument);
            ImageViewer.Visible = false;
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
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerNameAdd);
            string img = lnk.CommandName.ToString();
            var ImageName = img;
            if (img != null || img != "")
            {
                string extension = Path.GetExtension(img.ToString());
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

                var Newblob = blobContainer.GetBlockBlobReference(ImageName);

                string filePath = directoryPath + "\\" + ImageName;


                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                ViewState["filePath_Show"] = filePath;
                Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);

                if (extension == ".pdf")
                {
                    byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                    string Base64String = Convert.ToBase64String(bytes);
                    Session["Base64String"] = Base64String;
                    Session["Base64StringType"] = "application/pdf";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
                }
                else
                {
                    byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                    string Base64String = Convert.ToBase64String(bytes);
                    Session["Base64String"] = Base64String;
                    Session["Base64StringType"] = "image/png";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);

                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Sorry, File not found !!!", this.Page);
            }

        }
        catch (Exception ex)
        {
        }

    }

    protected int Year(string YearName)
    {
        int YearNo = 0;
        if (YearName.ToString() == "I Year" || YearName.ToString() == "I YEAR")
        {
            YearNo = 1;
        }
        else if (YearName.ToString() == "II Year" || YearName.ToString() == "II YEAR")
        {
            YearNo = 2;
        }
        else if (YearName.ToString() == "III Year" || YearName.ToString() == "III YEAR")
        {
            YearNo = 3;
        }
        else if (YearName.ToString() == "IV Year" || YearName.ToString() == "IV YEAR")
        {
            YearNo = 4;
        }
        else if (YearName.ToString() == "V Year" || YearName.ToString() == "V YEAR")
        {
            YearNo = 5;
        }
        else if (YearName.ToString() == "VI Year" || YearName.ToString() == "VI YEAR")
        {
            YearNo = 6;
        }
        return YearNo;
    }
    protected void lnkSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            int YearNo = Year(ViewState["YEAR"].ToString());
            string PassEncript = "AhpAuCYE/ihsXfIDX3uyGkO1ObCoQIF9UeeUG6VOlQwQ2fakmRKoUvOLE97YFgj7gb+Oz9mFciM1a4waMl83X33pVXQoqAODFkCOiJlOHIPE1J2nPQtJdd93jW6jvC5FYbq9ql6ipA6mSv226d7FRtzIkzIT7tK0xMBG4SJxbbs=";
            string password = "UsaErp@123";
            CustomStatus cs = CustomStatus.Others;
            int Pay = Convert.ToInt32(objCommon.LookUp("ACD_DCR_ONLINE", "COUNT(1)", "RECON=1 AND RECIEPT_CODE='DP' AND USERNO=" + Convert.ToInt32(Request.QueryString["userno"]) + "AND SEMESTERNO=" + Convert.ToInt32(ViewState["SEMESTERNO"])));
            if (Pay > 0)
            {
                cs = (CustomStatus)objdocContr.StudentEnrollementConformation(Convert.ToInt32(Request.QueryString["userno"]), PassEncript, Convert.ToInt32(ViewState["SEMESTERNO"]), YearNo, Convert.ToInt32(ViewState["COLLEGE_ID"]), Convert.ToInt32(ViewState["DEGREENO"]), Convert.ToInt32(ViewState["BRANCHNO"]), Convert.ToInt32(ViewState["ADMBATCH"]), Convert.ToInt32(Session["userno"]));
                //cs = (CustomStatus)objdocContr.UpdateFinalUserConformation(Convert.ToInt32(Request.QueryString["userno"]), 0, 0, Convert.ToInt32(Session["userno"]));
                if (cs == CustomStatus.RecordSaved)
                {
                    BindStudentInfo();
                    BindStudentDoclist();
                    objCommon.DisplayMessage(this.Page, "Admission Confirm Successfully !!!", this.Page);


                    //ShowConfirmationReport("Final_Confirmation_Report", "AdmisionConfirmSlip.rpt", Convert.ToInt32(ViewState["ID_NO"]));
                    string FinalStatus = "";
                    ViewState["ID_NO"] = objCommon.LookUp("ACD_STUDENT", "ISNULL(MAX(IDNO),0)", "ISNULL(CAN,0) = 0 AND ISNULL(ADMCAN,0) = 0 AND USERNO=" + Convert.ToInt32(Request.QueryString["userno"]));
                    FinalStatus = objCommon.LookUp("ACD_STUDENT", "ISNULL(ENROLLNO,0)", "ISNULL(CAN,0) = 0 AND ISNULL(ADMCAN,0) = 0 AND USERNO=" + Convert.ToInt32(Request.QueryString["userno"]));
                    if (FinalStatus.ToString() == "0" || FinalStatus.ToString() == string.Empty)
                    {
                        lnkSendEmail.Enabled = true;
                        lnkGeneratereport.Visible = false;
                        lnkPrintReport.Visible = false;
                        btnFrontBackReport.Visible = false;
                    }
                    else
                    {
                        lnkSendEmail.Enabled = true;
                        lnkGeneratereport.Visible = true;
                        lnkPrintReport.Visible = true;
                        //btnFrontBackReport.Visible = true;
                    }
                    EmailSmsWhatssppSend(Convert.ToInt32(1001), Convert.ToString(Request.QueryString["userno"]), ViewState["EMAILID"].ToString(), ViewState["STUDNAME"].ToString(), ViewState["PROGRAM"].ToString(), "USA", lblAcceptanceDate.Text, ViewState["USERNAME"].ToString(), password.ToString(), Convert.ToInt32(ViewState["ID_NO"]));


                }
                else if (cs == CustomStatus.RecordExist)
                {
                    objCommon.DisplayMessage(this.Page, "Admission Already Confirm!!!", this.Page);
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Error !!!", this.Page);
                    return;
                }

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                DynamicPdfViewer();
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Payment Not Received !!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void EmailSmsWhatssppSend(int Page_No, string userno, string toEmailId, string name, string Program, string Campus, string date, string UserId, string Password, int IDNO)
    {
        EmailSmsWhatsaap Email = new EmailSmsWhatsaap();
        DataSet ds = objCommon.FillDropDown("ACCESS_LINK", "isnull(EMAIL,0) EMAIL,isnull(SMS,0) as SMS,isnull(WHATSAAP,0) as WHATSAAP", "", "AL_NO=" + Convert.ToInt32(Request.QueryString["pageno"].ToString()), "");
        string Res = "";
        string College_Code = "52";
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["EMAIL"].ToString()) == 1)//For Email Send 
        {
            int PageNo = Page_No;
            //int PageNo = 33;
            UserController objUC = new UserController();
            DataSet dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", userno.ToString(), PageNo);
            if (dsUserContact.Tables[1] != null && dsUserContact.Tables[1].Rows.Count > 0)
            {
                string Subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                string Message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                string toSendAddress = toEmailId.ToString();// 
                MailMessage msgsPara = new MailMessage();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Message);
                msgsPara.BodyEncoding = Encoding.UTF8;
                msgsPara.Body = Convert.ToString(sb);
                msgsPara.Body = msgsPara.Body.Replace("[USERFIRSTNAME]", name.ToString());
                msgsPara.Body = msgsPara.Body.Replace("[PROGRAM]", Program.ToString());
                msgsPara.Body = msgsPara.Body.Replace("[CAMPUS]", Campus.ToString());
                msgsPara.Body = msgsPara.Body.Replace("[Username]", UserId.ToString());
                msgsPara.Body = msgsPara.Body.Replace("[Password]", Password.ToString());
                msgsPara.Body = msgsPara.Body.Replace("[Date]", date.ToString());

                MemoryStream Attachment = null; string AttachmentName = "Certificate_Admission.pdf";

                Attachment = ShowGeneralExportReportForMailForApplication("Reports,Academic,Certi_Admission.rpt", "@P_IDNO=" + Convert.ToInt32(IDNO) + ",@P_COLLEGE_CODE=" + College_Code.ToString() + "");

                Task<string> task = Email.Execute(msgsPara.Body, toSendAddress, Subject, Attachment, AttachmentName.ToString());
                Res = task.Result;
                // objCommon.DisplayMessage(this, Res.ToString(), this.Page);
            }
        }
    }

    static private MemoryStream ShowGeneralExportReportForMailForApplication(string path, string paramString)
    {
        MemoryStream oStream;
        ReportDocument customReport;
        customReport = new ReportDocument();
        string reportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Academic/Certi_Admission.rpt");
        customReport.Load(reportPath);
        char ch = ',';
        string[] val = paramString.Split(ch);
        if (customReport.ParameterFields.Count > 0)
        {
            for (int i = 0; i < val.Length; i++)
            {
                int indexOfEql = val[i].IndexOf('=');
                int indexOfStar = val[i].IndexOf('*');
                string paramName = string.Empty;
                string value = string.Empty;
                string reportName = "MainRpt";
                paramName = val[i].Substring(0, indexOfEql);

                if (indexOfStar > 0)
                {
                    value = val[i].Substring(indexOfEql + 1, ((indexOfStar - 1) - indexOfEql));
                    reportName = val[i].Substring(indexOfStar + 1);
                }
                else
                {

                    value = val[i].Substring(indexOfEql + 1);
                }

                if (reportName == "MainRpt")
                {
                    if (value == "null")
                    {
                        customReport.SetParameterValue(paramName, null);
                    }
                    else
                        customReport.SetParameterValue(paramName, value);
                }
                else
                    customReport.SetParameterValue(paramName, value, reportName);
            }
        }

        ConfigureCrystalReports(customReport);
        for (int i = 0; i < customReport.Subreports.Count; i++)
        {
            ConfigureCrystalReports(customReport.Subreports[i]);
        }

        oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        return oStream;
    }
    static private void ConfigureCrystalReports(ReportDocument customReport)
    {
        ConnectionInfo connectionInfo = Common.GetCrystalConnection();
        Common.SetDBLogonForReport(connectionInfo, customReport);
    }
    private void ShowConfirmationReport(string reportTitle, string rptFileName, int idno)
    {
        try
        {
            string colgname = "SLIIT";// ViewState["College_Name"].ToString().Replace(" ", "_");

            string exporttype = "pdf";
            string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
            //    Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/commonreport.aspx?";
            url += "exporttype=" + exporttype;

            url += "&filename=" + colgname + "-" + reportTitle + "." + exporttype;

            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno;
            // url += "&param=@P_USERNO=" + ((UserDetails)(Session["user"])).UserNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_REPORTS_gradeCard .ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void lnkGeneratereport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("StatusSlip", "rptCourseStatusSlip.rpt");
        }
        catch (Exception ex)
        {

        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
            string exporttype = "pdf";
            string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;

            url += "&filename=" + reportTitle + "." + rptFileName;

            url += "&path=~,Reports,Academic," + "OfferLetterBulk.rpt";
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(ViewState["ID_NO"].ToString());
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "OnlinepaymentStudentLogin.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void lnkPrintReport_Click(object sender, EventArgs e)
    {
        try
        {

            DynamicPdfViewer();
            //ShowConfirmationReport("Final_Confirmation_Report", "AdmisionConfirmSlip.rpt", Convert.ToInt32(ViewState["ID_NO"]));
        }
        catch (Exception ex)
        {

        }
    }
    protected void DynamicPdfViewer()
    {
        try
        {
            Document document = new Document(PageSize.A4, 88f, 88f, 10f, 10f);
            Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable table = null;

                document.Open();

                //Header Table
                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.8f, 0.0f });

                //Company Logo
                cell = ImageCell("~/IMAGES/SLIIT_logo.png", 30f, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);

                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);

                //document.Add(table);
                document.Add(table);
                //Photo
                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.8f, 0.0f });

                string SP_Name1 = "PKG_GET_CONFIRM_STUDENT_DATA";
                string SP_Parameters1 = "@P_IDNO";
                string Call_Values1 = "" + Convert.ToInt32(ViewState["ID_NO"]);

                DataSet que_out1 = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);

                string s = Convert.ToBase64String((byte[])que_out1.Tables[0].Rows[0]["PHOTO"]);
                byte[] imageBytes = Convert.FromBase64String(s);

                cell = ImageCellByte(imageBytes, 11f, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);

                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);

                document.Add(table);

                table = new PdfPTable(3);
                table.SetWidths(new float[] { 0.3f, 0.1f, 0.5f });
                table.TotalWidth = 410f;
                table.LockedWidth = true;
                table.SpacingBefore = 10f;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;

                table.AddCell(PhraseCell(new Phrase("Intake", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["BATCHNAME"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Student Registration No", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["ENROLLNO"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Name with Initials", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["NAME_INITIAL"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Name in Full", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["STUDNAME"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("NIC / Passport", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["NICPASS"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Address", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["LADDRESS"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Contact No", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["STUDENTMOBILE"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Email", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["EMAILID"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("SLIIT Email", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["COLLEGE_EMAIL"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Programme", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["PROGRAM_NAME"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Campus", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["CAMPUSNAME"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Batch", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["WEEKDAYSNAME"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Date of Registration", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["REGDATE"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Orientation Group", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase("", FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                document.Add(table);
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=StudentSummarySheet.pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
            }
        }
        catch (Exception ex)
        {

        }
    }
    private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, Color color)
    {
        PdfContentByte contentByte = writer.DirectContent;
        contentByte.SetColorStroke(color);
        contentByte.MoveTo(x1, y1);
        contentByte.LineTo(x2, y2);
        contentByte.Stroke();
    }
    private static PdfPCell PhraseCell(Phrase phrase, int align)
    {
        PdfPCell cell = new PdfPCell(phrase);
        cell.BorderColor = Color.WHITE;
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 2f;
        cell.PaddingTop = 0f;
        return cell;
    }
    private static PdfPCell ImageCell(string path, float scale, int align)
    {
        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path));
        image.ScalePercent(scale);
        PdfPCell cell = new PdfPCell(image);
        cell.BorderColor = Color.WHITE;
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 0f;
        cell.PaddingTop = 20f;
        return cell;
    }

    private static PdfPCell ImageCellByte(byte[] imageBytes, float scale, int align)
    {
        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageBytes);
        //image.ScalePercent(15f,15f);
        //var reader = new PdfReader(imageBytes);
        image.ScaleAbsolute(95f, 95f); // Set image size.
        //image.SetAbsolutePosition(reader.GetPageSize(1).Width / 2 - 100, 50);// Set image position.
        PdfPCell cell = new PdfPCell(image);
        cell.BorderColor = Color.WHITE;
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 0f;
        cell.PaddingTop = 20f;
        //cell.Width = 20f;

        return cell;
    }


    public int Blob_UploadDepositSlip(string ConStr, string ContainerName, string DocName, FileUpload FU, byte[] ChallanCopy)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;

        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.Properties.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            if (!cblob.Exists())
            {
                using (Stream stream = new MemoryStream(ChallanCopy))
                {
                    cblob.UploadFromStream(stream);
                }
            }
            //cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }
    public void DeleteIFExits(string FileName)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }
    protected void lnkClose_Click(object sender, EventArgs e)
    {
        try
        {
            if ((System.IO.File.Exists(Convert.ToString(ViewState["filePath_Show"]))))
            {
                System.IO.File.Delete(Convert.ToString(ViewState["filePath_Show"]));
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnFrontBackReport_Click(object sender, EventArgs e)
    {
        try
        {

            ShowReport(Convert.ToString(ViewState["ID_NO"]), "Student_ID_Card_Report", "CopyofStudentIDCardFrontBack.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentIDCardReport.btnPrintReport_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }
    private void ShowReport(string param, string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));         
            //url += "Reports/CommonReport.aspx?";
            string regno = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO=" + param);
            string exporttype = "pdf";
            string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";

            url += "exporttype=" + exporttype;
            url += "&filename=" + regno + "." + exporttype;
            //url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_COLLEGE_ID=" + ViewState["COLLEGE_ID"] + ",@P_Valid_Upto=" + DateTime.Now.ToString("dd/MM/yyyy");

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "StudentIDCardReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnRefundInitiated_Click(object sender, EventArgs e)
    {
        idno = idno = Convert.ToInt32(Convert.ToInt32(ViewState["ID_NO"]));
        int faculty = Convert.ToInt32(ViewState["COLLEGE_ID"]);
        int program = Convert.ToInt32(ViewState["BRANCHNO"]);
        int degreeno = Convert.ToInt32(ViewState["degreeno"]);
        int admbatch = Convert.ToInt32(ViewState["batchno"]);
        int semesterno = Convert.ToInt32(ViewState["SEMESTERNO"]);
        string Status = "Withdrawal of Registration";
        int StatusNo = 1;
        string REGNO = Convert.ToString(ViewState["REGNO"]);
        var todaydate = DateTime.Now;
        CustomStatus cs = CustomStatus.Others;
        cs = (CustomStatus)feeController.InsertStudentWithdrawApplication(idno, faculty, program, REGNO, degreeno, "", "", Status, StatusNo, todaydate);
        DataSet dsUserContact = null;
        string message = "";
        string subject = "";
        string filename = "";
        dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(2995));
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
            message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
            //  ExecuteRefund(message, Convert.ToString(ViewState["EMAILID"]), subject, Convert.ToString(ViewState["NAME_WITH_INITIAL"]), Convert.ToString(ViewState["REGNO"]), filename, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
            objCommon.DisplayMessage(this.Page, "Record Added Successfully !!!", this.Page);
            return;
        }
        if (cs.Equals(CustomStatus.TransactionFailed))
        {
            objCommon.DisplayMessage(this.Page, "Transaction Failed !!!", this.Page);
            return;
        }
    }
    protected void lnkCertiAdmis_Click(object sender, EventArgs e)
    {
        ShowReportAdm("Certificate Admission", "Certi_Admission.rpt");
    }
    private void ShowReportAdm(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            //string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(ViewState["ID_NO"]);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentReconsilReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnDocumentVarify_Click(object sender, EventArgs e)
    {
        try
        {
            DocumentControllerAcad objDoc = new DocumentControllerAcad();
            DocumentAcad objDocno = new DocumentAcad();
            string docnos = "", docnames = "", status = "", remark = ""; int Count = 0, reject = 0, DocCount = 0;
            DataSet dsUserContact = null;
            UserController objUC = new UserController();
          
            foreach (ListViewDataItem item in lvDocument.Items)
            {
                Label docno = item.FindControl("lblDocNo") as Label;
                Label DocName = item.FindControl("lblname") as Label;
                TextBox txtRemark = item.FindControl("txtremark") as TextBox;
                DropDownList ddlStatus = item.FindControl("ddlstatus") as DropDownList;
                HiddenField hdfMandatory = item.FindControl("hdfMandatory") as HiddenField;
                LinkButton lnkViewDoc = item.FindControl("lnkViewDoc") as LinkButton;

                if (lnkViewDoc.Visible == true)
                {
                    DocCount++;
                    if (ddlStatus.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Select Status for " + DocName.Text + " !!!", this.Page);
                        return;
                    }
                    if (Convert.ToString(txtRemark.Text) == string.Empty)
                    {
                        objCommon.DisplayMessage(this.Page, "Please Enter Remark for " + DocName.Text + " !!!", this.Page);
                        return;
                    }
                    Count++;
                    status += ddlStatus.SelectedValue + '$';
                    docnos += docno.Text + '$';
                    docnames += DocName.Text + '$';
                    remark += txtRemark.Text + '$';
                    if (ddlStatus.SelectedValue == "2")
                    {
                        reject = 1;
                    }
                }
            }
            if (DocCount == 0)
            {
                objCommon.DisplayMessage(this.Page, "Document Not Uploaded !!!", this.Page);
                return;

            }
            if (Count > 0)
            {
                docnos = docnos.TrimEnd('$');
                docnames = docnames.TrimEnd('$');
                status = status.TrimEnd('$');
                remark = remark.TrimEnd('$');


                if (reject == 1)
                {
                    EmailSmsWhatssppSend(Convert.ToInt32(Request.QueryString["pageno"]), lblEmailID.Text, lblStudName.Text, Convert.ToString(ViewState["USERNO"]));
                }
                CustomStatus cs = CustomStatus.Others;
                cs = (CustomStatus)objDoc.UpdateStudentDocumentStatus(Convert.ToInt32(ViewState["USERNO"]), docnos, docnames, status, remark, Convert.ToInt32(Session["userno"]));
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    BindStudentDoclist();
                    objCommon.DisplayMessage(this.Page, "Document Verified Successfully !!!", this.Page);
                    return;
                }
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    BindStudentDoclist();
                    objCommon.DisplayMessage(this.Page, "Document Verified Successfully !!!", this.Page);
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Server Error", this.Page);
                    return;
                }
            }

        }

        catch (Exception ex)
        {

        }
    }
}