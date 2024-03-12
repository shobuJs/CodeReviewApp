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
using _NVP;
using System.Collections.Specialized;
using EASendMail;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;

using Microsoft.WindowsAzure.Storage;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Security;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml;
using System.Net;
using System.Net.Security;
using mastersofterp;
using EASendMail;
using System.Text.RegularExpressions;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

using CrystalDecisions.Shared;
using System.Text;
using System.Net.Mail;
public partial class CertificateApproveRequest : System.Web.UI.Page
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
                if (Session["usertype"].ToString() == "1" && Session["username"].ToString().ToUpper().Contains("PRINCIPAL"))
                {
                    // divShow.Visible=true;
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
                objCommon.FillDropDownList(ddlDocumentTrack, "ACD_DOCUMENT_MASTER DM INNER JOIN ACD_DOCUMENT_REQUEST_APPROVAL DR ON (DR.DOC_NO=DM.DOC_NO)", "distinct DM.DOC_NO", "DOC_NAME", "", "");
                objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "", "COLLEGE_NAME");
                objCommon.FillDropDownList(ddlFacutlyTrack, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "", "COLLEGE_NAME");

                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
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
                Response.Redirect("~/notauthorized.aspx?page=CertificateApproveRequest.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CertificateApproveRequest.aspx");
        }
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlProgram, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_DEGREE D ON (CD.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=CD.BRANCHNO) INNER JOIN ACD_STUDENT S ON (S.DEGREENO=CD.DEGREENO AND S.BRANCHNO=CD.BRANCHNO) INNER JOIN ACD_APPLY_FOR_DOCUMENT AD ON (S.IDNO=AD.IDNO)", "DISTINCT CONVERT(NVARCHAR(10),D.DEGREENO)+','+CONVERT(NVARCHAR(10),B.BRANCHNO) AS PROGRAMNO", "D.DEGREENAME +' - '+B.LONGNAME AS PRGRAMNAME", "CD.COLLEGE_ID=" + Convert.ToInt32(ddlFaculty.SelectedValue), "");
        lvDocument.DataSource = null;
        lvDocument.DataBind();
        ddlProgram.SelectedValue = "0";
        ddlDocument.SelectedValue = "0";
        ddlStatus.SelectedValue = "0";
        DivButton.Visible = false;
    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        DivButton.Visible = false;
        lvDocument.DataSource = null;
        lvDocument.DataBind();
        ddlStatus.SelectedValue = "0";
        ddlDocument.SelectedValue = "0";
    }
    protected void ddlDocument_SelectedIndexChanged(object sender, EventArgs e)
    {
        DivButton.Visible = false;
        lvDocument.DataSource = null;
        lvDocument.DataBind();
        ddlStatus.SelectedValue = "0";
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFaculty.SelectedValue != "0" && ddlProgram.SelectedValue != "0" && ddlDocument.SelectedValue != "0")
        {
            string[] splitValue;
            splitValue = ddlProgram.SelectedValue.Split(',');
            int Degreeno = Convert.ToInt32(splitValue[0]);
            int Brachno = Convert.ToInt32(splitValue[1]);
            string SP_Name2 = "PKG_GET_DOCUMENT_REQUEST_APPROVAL";
            string SP_Parameters2 = "@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO,@P_DOC_NO,@P_STATUS,@P_FILTER";
            string Call_Values2 = "" + Convert.ToInt32(ddlFaculty.SelectedValue) + "," + Degreeno + "," + Brachno + "," + Convert.ToInt32(ddlDocument.SelectedValue) + "," + Convert.ToInt32(ddlStatus.SelectedValue) + "," + 0 + "";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (ds.Tables[0].Rows.Count > 0 || ds.Tables[0].Rows.Count == null)
            {
                DivButton.Visible = true;
                lvDocument.DataSource = ds;
                lvDocument.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            }
            else
            {
                DivButton.Visible = false;
                lvDocument.DataSource = ds;
                lvDocument.DataBind();
                objCommon.DisplayMessage(this, "Record Not Found", this.Page);

            }
        }
        foreach (ListViewDataItem dataitem in lvDocument.Items)
        {
            DropDownList ddIssue = dataitem.FindControl("ddlIssueMode") as DropDownList;
            Label lblIssue = dataitem.FindControl("lblIssue") as Label;
            ddIssue.SelectedValue = lblIssue.Text;
        }
    }
    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
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

    protected void btnEmail_Click(object sender, EventArgs e)
    {

        LinkButton STUD = sender as LinkButton;
        int idno = int.Parse(STUD.CommandArgument);
        string IDNOS = idno.ToString() + "$";
        string FileName = STUD.CommandName;
        string StudentName = STUD.ToolTip;
        string[] splitValue;
        splitValue = ddlProgram.SelectedValue.Split(',');
        int Degreeno = Convert.ToInt32(splitValue[0]);
        int Brachno = Convert.ToInt32(splitValue[1]);
        DataSet dsUserContact = null; string message = "";
        UserController objUC = new UserController();
        string Email = objCommon.LookUp("acd_student", "EMAILID", "idno=" + idno);
        dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", "0", Convert.ToInt32(25413));
        string Subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
        message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
        string SP_Name1 = "PKG_INSERT_DOCUMENT_REQUEST_APPROVAL";
        string SP_Parameters1 = "@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO,@P_DOC_NO,@P_FILTER,@P_IDNO,@P_UA_NO,@P_OUT";
        string Call_Values1 = "" + Convert.ToInt32(ddlFaculty.SelectedValue) + "," + Degreeno + "," + Brachno + "," + Convert.ToInt32(ddlDocument.SelectedValue) + "," + 0 + "," + IDNOS.ToString() + "," + Convert.ToInt32(Session["userno"]) + "," + 0 + "";

        string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);
        if (que_out1 == "1")
        {
            Execute(message, Email, Subject, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"]), StudentName.ToString(), FileName.ToString());
            objCommon.DisplayMessage(this, "Email Send Successfully.. !!", this.Page);
            return;
        }
        else if (que_out1 == "5")
        {
            objCommon.DisplayMessage(this, "Alredy Uploaded.. !!", this.Page);
            return;
        }
        else
        {
            objCommon.DisplayMessage(this, "Server Error.. !!", this.Page);
            return;
        }


    }

    protected void Execute(string message, string toSendAddress, string Subject, string ReffEmail, string reffPassword, string StudName, string FileName)
    {
        try
        {
            string Url = string.Empty;
            string directoryPath = string.Empty;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/DownloadImg" + "/";
            directoryPath = Server.MapPath(directoryName);
            string filePath = "";
            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = FileName.ToString();
            var ImageName = FileName;

            string extension = Path.GetExtension(img.ToString());
            DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
            if (extension == ".pdf")
            {
                ImageViewer.Visible = false;
                ltEmbed.Visible = true;
                imageViewerContainer.Visible = false;

                var Newblob = blobContainer.GetBlockBlobReference(ImageName);

                filePath = directoryPath + "\\" + ImageName;

                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
            }

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            MailMessage msg = new MailMessage();
            msg.To.Add(new System.Net.Mail.MailAddress(toSendAddress));
            msg.From = new System.Net.Mail.MailAddress(ReffEmail);
            msg.Subject = Subject;
            StringBuilder sb = new StringBuilder();
            msg.Body = message;

            msg.Body = msg.Body.Replace("[UA_FULLNAME]", StudName.ToString());
            byte[] file;
            file = System.IO.File.ReadAllBytes(filePath);

            //MemoryStream file = new MemoryStream(PDFGenerate("This is pdf file text", filePath.ToArray());

            //file.Seek(0, SeekOrigin.Begin);
            //Attachment data = new Attachment(file, "RunTime_Attachment.pdf", "application/pdf");  
            //var bytesRpt = ImageName.ToArray();
            ////var fileRpt = Convert.ToBase64String(bytesRpt);
            //byte[] test =  bytesRpt;

            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment(new MemoryStream(file), FileName.ToString());
            msg.Attachments.Add(attachment);
            msg.BodyEncoding = Encoding.UTF8;

            //msg.Body = Convert.ToString(sb);
            msg.IsBodyHtml = true;

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.UseDefaultCredentials = false;

            client.Credentials = new System.Net.NetworkCredential(ReffEmail, reffPassword);

            client.Port = 587; // You can use Port 25 if 587 is blocked (mine is)
            client.Host = "smtp-mail.outlook.com"; // "smtp.live.com";
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.TargetName = "STARTTLS/smtp.office365.com";
            client.EnableSsl = true;
            try
            {
                client.Send(msg);
                //lblText.Text = "Message Sent Succesfully";
            }
            catch (Exception ex)
            {
                //lblText.Text = ex.ToString();
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string[] splitValue;
        splitValue = ddlProgram.SelectedValue.Split(',');
        int Degreeno = Convert.ToInt32(splitValue[0]);
        int Brachno = Convert.ToInt32(splitValue[1]);
        int COUNT = 0; string que_out1 = string.Empty;
        string filename = "", Idno = "", IssueMode = "";
        byte[] FuProposals = null;
        foreach (ListViewDataItem dataitem in lvDocument.Items)
        {
            FileUpload FuDocument = dataitem.FindControl("FuDocument") as FileUpload;
            Label lblIdno = dataitem.FindControl("lblIdno") as Label;
            CheckBox ckh = dataitem.FindControl("Chk") as CheckBox;
            DropDownList ddlIssueMode = dataitem.FindControl("ddlIssueMode") as DropDownList;
            if (ckh.Checked == true && ckh.Enabled == true)
            {
                if (ddlIssueMode.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(this, "Please Select Issue Mode", this.Page);
                    return;
                }
                if (FuDocument.HasFile)
                {
                    Idno += lblIdno.Text + "$";
                    IssueMode += ddlIssueMode.Text + "$";
                    COUNT++;
                    string fileExtension = System.IO.Path.GetExtension(FuDocument.FileName).ToString().ToLower();
                    string contentType = contentType = FuDocument.PostedFile.ContentType;
                    if (fileExtension.ToString() != ".pdf" )
                    {
                        objCommon.DisplayMessage(this, "Please Select PDF File", this.Page);
                        return;

                    }
                  
                    FuProposals = objCommon.GetImageData(FuDocument);

                    int Upload_Status = Convert.ToInt32(objCommon.LookUp("ACD_APPLY_FOR_DOCUMENT", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(lblIdno.Text) + "AND DOC_NO=" + ddlDocument.SelectedValue + "AND DOC_NAME IS NOT NULL"));
                    filename += Convert.ToInt32(lblIdno.Text) + "_" + Degreeno + "_Document_" + ddlDocument.SelectedItem.Text + fileExtension + "$";

                    if (Upload_Status == 0)
                    {
                        int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, Convert.ToInt32(lblIdno.Text) + "_" + Degreeno + "_Document_" + ddlDocument.SelectedItem.Text, FuDocument, FuProposals);
                        if (retval == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                            return;
                        }
                    }

                }
                else
                {
                    objCommon.DisplayMessage(this, "Please Select File", this.Page);
                    return;
                }
            }

        }
        if (COUNT == 0)
        {
            objCommon.DisplayMessage(this, "Please Select At List One Student !!", this.Page);
            return;
        }
        string SP_Name1 = "PKG_INSERT_DOCUMENT_REQUEST_APPROVAL";
        string SP_Parameters1 = "@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO,@P_DOC_NO,@P_FILTER,@P_DOC_NAME,@P_IDNO,@P_ISSUE_MODE,@P_UA_NO,@P_OUT";
        string Call_Values1 = "" + Convert.ToInt32(ddlFaculty.SelectedValue) + "," + Degreeno + "," + Brachno + "," + Convert.ToInt32(ddlDocument.SelectedValue) + "," + 0 + "," + filename.ToString() + "," + Idno.ToString() + "," + IssueMode.ToString() + "," + Convert.ToInt32(Session["userno"]) + "," + 0 + "";

        que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);
        if (que_out1 == "1")
        {
            objCommon.DisplayMessage(this, "Record Saved Successfully.. !!", this.Page);
            return;
        }
        else if (que_out1 == "5")
        {
            objCommon.DisplayMessage(this, "Alredy Uploaded.. !!", this.Page);
            return;
        }
        else
        {
            objCommon.DisplayMessage(this, "Server Error.. !!", this.Page);
            return;
        }

    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        LinkButton STUD = sender as LinkButton;
        int IDNO = int.Parse(STUD.CommandArgument);
        int DocNo = Convert.ToInt32(STUD.CommandName);
        int Semesterno = Convert.ToInt32(STUD.ToolTip);
        if (DocNo == 1)
        {
            try
            {
                ShowReport("Bonafide_Certificate", "rptBonafide_Certificate.rpt", IDNO, Semesterno);
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "ACADEMIC_Apply_Convocation.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
        else
        {
            objCommon.DisplayMessage(this, "Document Not Available", this.Page);
        }
    }
    private void ShowReport(string reportTitle, string rptFileName, int Idno, int Semesterno)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Idno + ",@P_COLLEGEID=" + Convert.ToInt32(ddlFaculty.SelectedValue) + ",@P_DEGREENO=" + 0 + ",@P_BRANCHNO=" + 0 + ",@P_SEMESTERNO=" + Semesterno;

            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Apply_Convocation.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    //New Tab Track Request
    protected void ddlFacutlyTrack_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlPragramTrack, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_DEGREE D ON (CD.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=CD.BRANCHNO) INNER JOIN ACD_STUDENT S ON (S.DEGREENO=CD.DEGREENO AND S.BRANCHNO=CD.BRANCHNO) INNER JOIN ACD_DOCUMENT_REQUEST_APPROVAL AD ON (S.IDNO=AD.IDNO)", "DISTINCT CONVERT(NVARCHAR(10),D.DEGREENO)+','+CONVERT(NVARCHAR(10),B.BRANCHNO) AS PROGRAMNO", "D.DEGREENAME +' - '+B.LONGNAME AS PRGRAMNAME", "CD.COLLEGE_ID=" + Convert.ToInt32(ddlFacutlyTrack.SelectedValue), "");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {

        string[] splitValue;
        splitValue = ddlPragramTrack.SelectedValue.Split(',');
        int Degreeno = Convert.ToInt32(splitValue[0]);
        int Brachno = Convert.ToInt32(splitValue[1]);
        string SP_Name2 = "PKG_GET_DOCUMENT_REQUEST_APPROVAL";
        string SP_Parameters2 = "@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO,@P_DOC_NO,@P_STATUS,@P_FILTER";
        string Call_Values2 = "" + Convert.ToInt32(ddlFacutlyTrack.SelectedValue) + "," + Degreeno + "," + Brachno + "," + Convert.ToInt32(ddlDocumentTrack.SelectedValue) + "," + Convert.ToInt32(ddlStatusTrack.SelectedValue) + "," + 1 + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (ds.Tables[0].Rows.Count > 0 || ds.Tables[0].Rows.Count == null)
        {
            lvtrackRequest.DataSource = ds;
            lvtrackRequest.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);

        }
        else
        {
         
            lvtrackRequest.DataSource = ds;
            lvtrackRequest.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
            objCommon.DisplayMessage(this, "Record Not Found", this.Page);

        }
    }

    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        ddlPragramTrack.SelectedValue = "0";
        ddlFacutlyTrack.SelectedValue = "0";
        ddlDocumentTrack.SelectedValue = "0";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
    }
}