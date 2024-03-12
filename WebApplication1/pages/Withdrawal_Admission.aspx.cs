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
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using EASendMail;


public partial class EXAMINATION_Projects_Withdrawal_Admission : System.Web.UI.Page
{
    Common objCommon = new Common();
    UserController user = new UserController();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    int idno = 0; decimal TotalSum = 0; String tspl_txn_id = string.Empty;
    string session = string.Empty;
    IITMS.UAIMS.BusinessLayer.BusinessEntities.DocumentAcad objdocument = new DocumentAcad();
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
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
                    //Page Authorization
                    this.CheckPageAuthorization();

                    if (Convert.ToString(ViewState["action"]) == "")
                    {
                        ViewState["action"] = "add";
                    }

                    // Check User Authority 


                    ViewState["userno"] = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + Session["username"].ToString() + "'");

                    Page.Title = Session["coll_name"].ToString();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                    this.CheckPageAuthorization();
                    if (Session["StudentPayDetail"] != null)
                    {
                        Session["StudentPayDetail"] = null;
                    }
                    //Set the Page Title

                    Page.Title = Session["coll_name"].ToString();
                    if (Session["usertype"].ToString().Equals("2"))     //Student 
                    {

                        ShowStudentDetails();
                        BindListView();
                        ShowStudentBankDetail();
                        int userno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DISTINCT USERNO", "IDNO=" + Convert.ToInt32(Session["idno"])));
                        ViewState["USER_NO"] = userno;


                    }
                    else
                    {
                        //objCommon.DisplayUserMessage(updBulkReg, "Record Not Found.This Page is use for Online Payment for only Student Login!!", this.Page);
                    }
                
                }
                //objCommon.SetLabelData("0");//for label
                //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header

            }
          
            if (Request.Params["__EVENTTARGET"] != null &&
                Request.Params["__EVENTTARGET"].ToString() != string.Empty)
            {
                //if (Request.Params["__EVENTTARGET"].ToString() == "CreateDemand")
                // this.CreateDemandForCurrentFeeCriteria();
            }
            //btnSubmit.Visible = true;   
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Postponement.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
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
                Response.Redirect("~/notauthorized.aspx?page=Withdrawal_Admission.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Withdrawal_Admission.aspx");
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


    public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
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
            cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }
    private void ShowStudentDetails()
    {
        try
        {
            if (Session["usertype"].ToString().Equals("2"))     //Student 
            {
                // int idno = 24441;
                idno = Convert.ToInt32(Session["idno"]);
                //  string branchno = objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + idno);
                DataSet dsStudent = feeController.GetStudentInfoPosteponementById(idno);
                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    btnCalculate.Visible = false;
                    DataRow dr = dsStudent.Tables[0].Rows[0];
                    //lblStudName.Text = dr["STUDNAME"].ToString();

                    string fullName = dr["STUDFIRSTNAME"].ToString();
                    lblStudentName.Text = fullName;
                    ViewState["name"] = dr["STUDFIRSTNAME"].ToString();

                    string faculty = dr["COLLEGE_NAME"].ToString();
                    lblFaculty.Text = faculty;
                    ViewState["EMAILID"] = dr["EMAILID"].ToString();
                   
                    string program = dr["PROGRAM"].ToString();
                    lblProgram.Text = program;
                    ViewState["regno"] = dr["REGNO"].ToString();
                    lblStudentID.Text = dr["REGNO"].ToString() == string.Empty ? string.Empty : dr["REGNO"].ToString();
                    lblfeepaid.Text = dr["PAID"].ToString() == string.Empty ? string.Empty : dr["PAID"].ToString();
                    lblexpectedrefund.Text = dr["EXPECTED_REFUND_BY_ADMIN"].ToString() == string.Empty ? string.Empty : dr["EXPECTED_REFUND_BY_ADMIN"].ToString();
                    lblbalance.Text = dr["TOTALBALANCE"].ToString() == string.Empty ? string.Empty : dr["TOTALBALANCE"].ToString();
                    lblCurrentSemester.Text = dr["SEMESTERNAME"].ToString() == string.Empty ? string.Empty : dr["SEMESTERNAME"].ToString();

                    Session["PAID"] = dr["PAID"].ToString();
                    Session["TOTALBALANCE"] = dr["TOTALBALANCE"].ToString();
                    Session["DEGREENO"] = dr["DEGREENO"].ToString();
                    Session["BRANCHNO"] = dr["BRANCHNO"].ToString();
                    Session["COLLEGE_ID"] = dr["COLLEGE_ID"].ToString();
                    Session["REGNO"] = dr["REGNO"].ToString();
                
                }
                else
                {
                    // objCommon.DisplayUserMessage(updBulkReg, "Record Not Found.", this.Page);
                }
            }
            else
            {
                // objCommon.DisplayUserMessage(updBulkReg, "Record Not Found.This Page is use for Online Payment for Student Login!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Postponement.ShowStudentDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmitRequest_Click(object sender, EventArgs e)
    {
        idno = idno = Convert.ToInt32(Session["idno"]);
        int faculty = Convert.ToInt32(Session["COLLEGE_ID"]);
        // string Name = lblStudentName.Text;
        int program = Convert.ToInt32(Session["BRANCHNO"]);
        //string Semester = lblCurrentSemester.Text;
        string Bankname = txtBankName.Text;
        string BranchName = txtBranchName.Text;
        string AccountNo = Convert.ToString(txtAccountNumber.Text);
        string IFSC = txtIFSCCode.Text;
        string REGNO = (Session["REGNO"]).ToString();
        string Reason = txtReasonWithdrawal.Text;
       
        string feepaid = Session["PAID"].ToString();
  
        string Balance = Session["TOTALBALANCE"].ToString();
        int degreeno = Convert.ToInt32(Session["DEGREENO"]);
        string Status = "WITHDRAWL";
        int StatusNo = 1;
        var todaydate = DateTime.Now;
        string filename = "";
        string docname = "";

        if (FileUpload1.HasFile)
        {
            string contentType = contentType = FileUpload1.PostedFile.ContentType;


            string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            HttpPostedFile file = FileUpload1.PostedFile;
            filename = FileUpload1.PostedFile.FileName;
             docname = idno + "_doc_" + "Withdrawal" + ext;

             int retval = Blob_Upload(blob_ConStr, blob_ContainerName, idno + "_doc_" + "Withdrawal" + "", FileUpload1);
                  
        }
        else
        {
            docname = "";
        }
        DataSet dsUserContact = null;
        string message = "";
        string subject = "";
        dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));
        CustomStatus cs = CustomStatus.Others;
        cs = (CustomStatus)feeController.InsertStudentWithdrawAdmission(idno, faculty, program, Bankname, BranchName, AccountNo, IFSC, REGNO, degreeno, docname, Reason, Status, StatusNo, todaydate, feepaid, Balance);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
            message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
            Execute(message, Convert.ToString(ViewState["EMAILID"]), subject, Convert.ToString(ViewState["name"]), Convert.ToString(ViewState["regno"]), Convert.ToString(Session["PAID"]),txtBankName.Text, txtBranchName.Text,Convert.ToString(txtAccountNumber.Text),filename, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
            objCommon.DisplayMessage(this.Page, "Record Added Successfully !!!", this.Page);
            BindListView();
            clear();
            // bindlist();
            return;
        }
        if (cs.Equals(CustomStatus.TransactionFailed))
        {
            objCommon.DisplayMessage(this.Page, "Transaction Failed !!!", this.Page);
            // bindlist();
            return;
        }
        

    }
    static async Task Execute(string message, string toSendAddress, string Subject, string firstname, string username, string amount,string bankname,string branchname,string accountno, string filename, string sendemail, string emailpass)
    {
        try
        {
            SmtpMail oMail = new SmtpMail("TryIt");
            oMail.From = sendemail;
            oMail.To = toSendAddress;
            oMail.Subject = Subject;
            oMail.HtmlBody = message;
            oMail.HtmlBody = oMail.HtmlBody.Replace("[USERFIRSTNAME]", firstname.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[USERNAME]", username.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[BANKNAME]", bankname.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[BRANCHNAME]", branchname.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[ACOUNTNO]", accountno.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[AMOUNT]", amount.ToString());      
            SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022
            oServer.User = sendemail;
            oServer.Password = emailpass;

            oServer.Port = 587;

            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();

            oSmtp.SendMail(oServer, oMail);
            
   
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    
    private void BindListView()
    {
        int StatusNO = 1;
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT_PROGRAM_CANCEL", "SRNO", "DOCUMENT,IDNO,CONVERT(NVARCHAR(11),STUDENT_APPLIED_DATE,103)AS STUDENT_APPLIED_DATE,(CASE when  isnull(DIRECTOR_APPROVAL,0)=2 THEN 'Reject' WHEN (isnull(FINANCE_APPROVAL,0) = 1 and isnull(DIRECTOR_APPROVAL,0)=1 or isnull(STATS_BY_ADMIN,0)= 1)THEN 'Approved' WHEN (isnull(FINANCE_APPROVAL,0) = 0 or isnull(DIRECTOR_APPROVAL,0)=0 or isnull(STATS_BY_ADMIN,0)= 0 ) THEN 'Pending'	WHEN (isnull(FINANCE_APPROVAL,0) = 2 or isnull(DIRECTOR_APPROVAL,0)=2) THEN 'Reject' ELSE '-' END) AS FINANCE_APPROVAL", "IDNO=" + Convert.ToInt32(Session["idno"]), "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
            lvlWithdra.DataSource = ds;
            lvlWithdra.DataBind();

        }

    }
    private void ShowStudentBankDetail()
    {
        try
        {
            if (Session["usertype"].ToString().Equals("2"))     //Student 
            {

                int STATUS_NO = 1;

                idno = Convert.ToInt32(Session["idno"]);

                DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT_PROGRAM_CANCEL", "SRNO", "DOCUMENT,IDNO,EXPECTED_REFUND_BY_ADMIN,REASON,FEEPAID,BALANCE,BANKNAME,BRANCHADDRESS,ACCOUNTNO,IFSCCODE,REFUND_AMOUNT", "STATUS_NO=" + STATUS_NO, "");
                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    DataRow dr = dsStudent.Tables[0].Rows[0];
                    //lblStudName.Text = dr["STUDNAME"].ToString();

                    string Reason = dr["REASON"].ToString();
                    txtReason.Text = Reason;

                    string Bankname = dr["BANKNAME"].ToString();
                    lblBankName.Text = Bankname;


                    string BranchName = dr["BRANCHADDRESS"].ToString();
                    lblBranchName.Text = BranchName;

                    //   lblStudentID.Text = dr["ENROLLNO"].ToString() == string.Empty ? string.Empty : dr["ENROLLNO"].ToString();
                    
                    ViewState["BlobImage"] = dr["DOCUMENT"].ToString();
                    lblrefund.Text = dr["EXPECTED_REFUND_BY_ADMIN"].ToString() == string.Empty ? string.Empty : dr["EXPECTED_REFUND_BY_ADMIN"].ToString();
                    lblfee.Text = dr["FEEPAID"].ToString() == string.Empty ? string.Empty : dr["FEEPAID"].ToString();
                    lblbalancefee.Text = dr["BALANCE"].ToString() == string.Empty ? string.Empty : dr["BALANCE"].ToString();
                    lblAccountNumber.Text = dr["ACCOUNTNO"].ToString() == string.Empty ? string.Empty : dr["ACCOUNTNO"].ToString();
                    lblIFSCCode.Text = dr["IFSCCODE"].ToString() == string.Empty ? string.Empty : dr["IFSCCODE"].ToString();
                   // lblrefundss.Text = dr["REFUND_AMOUNT"].ToString() == string.Empty ? string.Empty : dr["REFUND_AMOUNT"].ToString();


                }
                else
                {
                    // objCommon.DisplayUserMessage(updBulkReg, "Record Not Found.", this.Page);
                }
            }
            else
            {
                // objCommon.DisplayUserMessage(updBulkReg, "Record Not Found.This Page is use for Online Payment for Student Login!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Postponement.ShowStudentDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void clear()
    {
        txtAccountNumber.Text = "";
        txtBankName.Text = "";
        txtBranchName.Text = "";
        txtIFSCCode.Text = "";
        txtReason.Text = "";
        txtReasonWithdrawal.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtAccountNumber.Text = "";
        txtBankName.Text = "";
        txtBranchName.Text = "";
        txtIFSCCode.Text = "";
        txtReason.Text = "";
    }
    protected void lnkViewDoc_Click(object sender, EventArgs e)
    {
        try
        {
                LinkButton lnk = sender as LinkButton;
                int Idno = int.Parse(lnk.CommandArgument);
                ViewState["idno"] = Idno;
                int STATUS_NO = 1;
                int srno = int.Parse(lnk.CommandName);
                //int srno = 3;
                DataSet ds = null;
                DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT_PROGRAM_CANCEL", "SRNO", "DOCUMENT,IDNO,CASE WHEN EXPECTED_REFUND_BY_ADMIN =0 THEN  EXPECTED_REFUND_BY_ADMIN ELSE REFUND_AMOUNT END AS EXPECTED_REFUND_BY_ADMIN,REASON,FEEPAID,BALANCE,BANKNAME,BRANCHADDRESS,ACCOUNTNO,IFSCCODE,REFUND_AMOUNT", "IDNO=" + Idno, "");
                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    DataRow dr = dsStudent.Tables[0].Rows[0];
                    //lblStudName.Text = dr["STUDNAME"].ToString();
                    string Reason = dr["REASON"].ToString();
                    txtReason.Text = Reason;
                    string Bankname = dr["BANKNAME"].ToString();
                    lblBankName.Text = Bankname;
                    string BranchName = dr["BRANCHADDRESS"].ToString();
                    lblBranchName.Text = BranchName;
                    //   lblStudentID.Text = dr["ENROLLNO"].ToString() == string.Empty ? string.Empty : dr["ENROLLNO"].ToString();
                    ViewState["BlobImage"] = dr["DOCUMENT"].ToString();
                    lblrefund.Text = dr["EXPECTED_REFUND_BY_ADMIN"].ToString() == string.Empty ? string.Empty : dr["EXPECTED_REFUND_BY_ADMIN"].ToString();
                    lblfee.Text = dr["FEEPAID"].ToString() == string.Empty ? string.Empty : dr["FEEPAID"].ToString();
                    lblbalancefee.Text = dr["BALANCE"].ToString() == string.Empty ? string.Empty : dr["BALANCE"].ToString();
                    lblAccountNumber.Text = dr["ACCOUNTNO"].ToString() == string.Empty ? string.Empty : dr["ACCOUNTNO"].ToString();
                    lblIFSCCode.Text = dr["IFSCCODE"].ToString() == string.Empty ? string.Empty : dr["IFSCCODE"].ToString();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "veiw", "$(document).ready(function () {$('#veiw').modal();});", true);
            }
                            
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "veiw", "$(document).ready(function () {$('#veiw').modal();});", true);
               
            }
        }
        catch (Exception ex)
        {

        }
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
    protected void lnkViewDoc_Click1(object sender, EventArgs e)
    {
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
        string img = ViewState["BlobImage"].ToString();
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

            Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);

            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);

        }
        else
        {
            objCommon.DisplayMessage(updpost, "Sorry, File not found !!!", this.Page);
        }
        
    }
}