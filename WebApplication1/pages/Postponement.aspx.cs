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



public partial class EXAMINATION_Projects_Postponement : System.Web.UI.Page
{
    Common objCommon = new Common();
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
                    if (Convert.ToString(ViewState["action"]) == "")
                    {
                        ViewState["action"] = "add";
                    }

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
                        
                        hdsrno.Value = "0";
                        int userno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DISTINCT USERNO", "IDNO=" + Convert.ToInt32(Session["idno"])));
                        ViewState["USER_NO"] = userno;
                        
                        
                    }
                    else
                    {
                        //objCommon.DisplayUserMessage(updBulkReg, "Record Not Found.This Page is use for Online Payment for only Student Login!!", this.Page);
                    }
                  
                }

                objCommon.SetLabelData("0");
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); 
            }
         
            if (Request.Params["__EVENTTARGET"] != null &&
                Request.Params["__EVENTTARGET"].ToString() != string.Empty)
            {
                //if (Request.Params["__EVENTTARGET"].ToString() == "CreateDemand")
                   // this.CreateDemandForCurrentFeeCriteria();
            }
            //btnSubmit.Visible = true;  
            objCommon.SetLabelData("0");//for label
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
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
                Response.Redirect("~/notauthorized.aspx?page=Postponement.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Postponement.aspx");
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
            int userno = Convert.ToInt32(Session["userno"]);
            if (Session["usertype"].ToString().Equals("2"))     //Student 
            {
       
                idno = Convert.ToInt32(Session["idno"]);
                idno = Convert.ToInt32(Session["idno"]);
                DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_COLLEGE_MASTER CM ON (S.COLLEGE_ID=CM.COLLEGE_ID)INNER JOIN  ACD_SEMESTER SE ON (S.SEMESTERNO=SE.SEMESTERNO)INNER JOIN  ACD_BRANCH B ON (B.BRANCHNO=S.BRANCHNO)",
                        "IDNO", "REGNO,STUDNAME,COLLEGE_NAME as College,SEMESTERNAME AS SEMESTER,LONGNAME ", "IDNO=" + idno, "");

                DataRow drs = ds.Tables[0].Rows[0];


                string fullName = drs["STUDNAME"].ToString();
                lblStudentn.Text = fullName;

                string faculty = drs["College"].ToString();
                lblFacultyname.Text = faculty;
                string program = drs["LONGNAME"].ToString();
                lblPrograms.Text = program;
                lblCurrentSemester.Text = drs["SEMESTER"].ToString() == string.Empty ? string.Empty : drs["SEMESTER"].ToString();
                lblStudent_id.Text = drs["REGNO"].ToString() == string.Empty ? string.Empty : drs["REGNO"].ToString();
                Session["REGNO"]=drs["REGNO"].ToString() == string.Empty ? string.Empty : drs["REGNO"].ToString();
             
                DataSet dsStudent = feeController.GetStudentInfoPosteponementById(idno);
                if (dsStudent != null && dsStudent.Tables.Count > 0 && dsStudent.Tables[0].Rows.Count > 0)
                {
                    //lblStudName.Text = dr["STUDNAME"].ToString();
                    //string fullName = dr["NAME"].ToString();
                    //lblStudentn.Text = fullName;
                    //string faculty = dr["COLLEGE_NAME"].ToString();
                    //lblFacultyname.Text = faculty;
                    //lblStudent_id.Text = dr["ENROLLNO"].ToString() == string.Empty ? string.Empty : dr["ENROLLNO"].ToString();
                    //lblCurrentSemester.Text = dr["SEMESTERNAME"].ToString() == string.Empty ? string.Empty : dr["SEMESTERNAME"].ToString();

                    DataRow dr = dsStudent.Tables[0].Rows[0];
               
                   // string program = dr["PROGRAM"].ToString();
                   // lblPrograms.Text = program;
                    lblfeepaid.Text = dr["PAID"].ToString() == string.Empty ? string.Empty : dr["PAID"].ToString();
                    lblbalance.Text = dr["TOTALBALANCE"].ToString() == string.Empty ? string.Empty : dr["TOTALBALANCE"].ToString();
                    Session["PAID"] = dr["PAID"].ToString();
                    Session["TOTALBALANCE"] = dr["TOTALBALANCE"].ToString();
                    Session["DEGREENO"] = dr["DEGREENO"].ToString();
                    Session["BRANCHNO"] = dr["BRANCHNO"].ToString();
                    Session["COLLEGE_ID"] = dr["COLLEGE_ID"].ToString();
                    Session["ENROLLNO"] = dr["ENROLLNO"].ToString();
                  
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Not Define Fees Paid And Balance Fees !!!", this.Page);
                }
            }
            else
            {
               
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string feepaid = "";
        string Balance = "";
        idno = Convert.ToInt32(Session["idno"]);
         DataSet dsStudent = feeController.GetStudentInfoPosteponementById(idno);
         if (dsStudent != null && dsStudent.Tables.Count > 0 && dsStudent.Tables[0].Rows.Count > 0)
         {
             DataRow dr = dsStudent.Tables[0].Rows[0];
             feepaid = dr["PAID"].ToString() == string.Empty ? string.Empty : dr["PAID"].ToString();
             Balance = dr["TOTALBALANCE"].ToString() == string.Empty ? string.Empty : dr["TOTALBALANCE"].ToString();
         
       //idno = Convert.ToInt32(Session["idno"]);
       int faculty = Convert.ToInt32(Session["COLLEGE_ID"]);
      // string Name = lblStudentName.Text;
       int program = Convert.ToInt32(Session["BRANCHNO"]);
       //string Semester = lblCurrentSemester.Text;
       string Bankname = txtBankName.Text;
       string BranchName = txtBranchName.Text;
       int AccountNo = Convert.ToInt32(txtAccountNumber.Text);
       string IFSC = txtIFSCCode.Text;
       string REGNO = (Session["REGNO"]).ToString();
       string Reason = txtReasonWithdrawal.Text;
       int Refund = Convert.ToInt32(lblRefund.Text);
       int degreeno = Convert.ToInt32(Session["DEGREENO"]);
       string Status = "POSTPONEMENT";
       int StatusNo = 2;
       var todaydate = DateTime.Now;
       int AbjustPayment = 0;
       if (chkAbPa.Checked == true)
       {
           AbjustPayment = 1;
       }
 
       if (fuDocument.HasFile)
       {
           string contentType = contentType = fuDocument.PostedFile.ContentType;
         

           string ext = System.IO.Path.GetExtension(fuDocument.PostedFile.FileName);
           HttpPostedFile file = fuDocument.PostedFile;
           string filename = fuDocument.PostedFile.FileName;
           string docname = idno + "_doc_" + "Postponement" + ext;

           int retval = Blob_Upload(blob_ConStr, blob_ContainerName, idno + "_doc_" + "Postponement" + "", fuDocument);
           if (retval == 0)
           {
               ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
               return;
           }

           if (hdsrno.Value != null)
           {
               if (hdsrno.Value.ToString().Equals("0"))
               {
                   CustomStatus cs = CustomStatus.Others;
                   cs = (CustomStatus)feeController.InsertPostponement(idno, faculty, program, Bankname, BranchName, AccountNo, IFSC, REGNO, degreeno, docname, Reason, Status, StatusNo, todaydate, feepaid, Balance, AbjustPayment);
                   if (cs.Equals(CustomStatus.RecordSaved))
                   {
                       objCommon.DisplayMessage(this.Page, "Record Added Successfully !!!", this.Page);
                       BindListView();
                       ShowStudentBankDetail();
                       Clear();
                       return;
                   }
                   if (cs.Equals(CustomStatus.TransactionFailed))
                   {
                       objCommon.DisplayMessage(this.Page, "Transaction Failed !!!", this.Page);
                       BindListView();
                       ShowStudentBankDetail();
                       return;
                   }
               }
               else
               {
                   if (hdsrno.Value != null)
                   {
                       int Srno = 0;
                       Srno = Convert.ToInt32(hdsrno.Value);
                       CustomStatus cs = CustomStatus.Others;
                       cs = (CustomStatus)feeController.UpdatePostponement(Srno, idno, faculty, program, Bankname, BranchName, AccountNo, IFSC, REGNO, degreeno, docname, Reason, Status, StatusNo, todaydate, feepaid, Balance, AbjustPayment);
                       if (cs.Equals(CustomStatus.RecordSaved))
                       {
                           objCommon.DisplayMessage(this.Page, "Record Update Successfully !!!", this.Page);
                           BindListView();
                           ShowStudentBankDetail();
                           Clear();
                           return;
                       }
                       if (cs.Equals(CustomStatus.TransactionFailed))
                       {
                           objCommon.DisplayMessage(this.Page, "Transaction Failed !!!", this.Page);
                           BindListView();
                           ShowStudentBankDetail();
                           return;
                       }
                   }
               }
           }
       }
       }
  else
  {
      objCommon.DisplayMessage(this.Page, "Not Define Fees Paid And Balance Fees !!!", this.Page);
  }


    }
    private void BindListView()
    { 
          string RegNo=Session["REGNO"].ToString();
             int StatusNO=2;                                                                                                                                                                                                                           
             DataSet ds = objCommon.FillDropDown("ACD_STUDENT_PROGRAM_CANCEL", "SRNO", "IDNO,REGNO,DOCUMENT,CONVERT(varchar, [STUDENT_APPLIED_DATE], 34) AS Date,(CASE WHEN ADMIN_APPROVAL = 1 THEN 'Approved' WHEN ADMIN_APPROVAL = 0 THEN 'Pending' WHEN ADMIN_APPROVAL=2 THEN 'Reject'  ELSE '-' END) AS ADMIN_APPROVAL", "STATUS_NO='" + StatusNO +"' AND REGNO='" + RegNo +"'" , "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvBranch.DataSource = ds;
                lvBranch.DataBind();
             
            }
           
    }
    private void ShowStudentBankDetail()
    {
        try
        {
            if (Session["usertype"].ToString().Equals("2"))     //Student 
            {
              
             int  STATUS_NO=2;

                idno = Convert.ToInt32(Session["idno"]);

                DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT_PROGRAM_CANCEL", "SRNO", "REASON,FEEPAID,BALANCE,BANKNAME,BRANCHADDRESS,ACCOUNTNO,IFSCCODE", "STATUS_NO=" + STATUS_NO, "");
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

                    lblfee.Text = dr["FEEPAID"].ToString() == string.Empty ? string.Empty : dr["FEEPAID"].ToString();
                    lblbalancefee.Text = dr["BALANCE"].ToString() == string.Empty ? string.Empty : dr["BALANCE"].ToString();
                    lblAccountNumber.Text = dr["ACCOUNTNO"].ToString() == string.Empty ? string.Empty : dr["ACCOUNTNO"].ToString();
                    lblIFSCCode.Text = dr["IFSCCODE"].ToString() == string.Empty ? string.Empty : dr["IFSCCODE"].ToString();


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

    private void Clear()
    {
        txtAccountNumber.Text = "";
        txtBankName.Text = "";
        txtBranchName.Text = "";
        txtIFSCCode.Text = "";
        txtReason.Text = "";
        chkAbPa.Checked = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnedit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //ddlALType.SelectedValue = null;
            ImageButton btnEditS = sender as ImageButton;
            int SRNO = int.Parse(btnEditS.CommandArgument);
            //   Label1.Text = string.Empty;
            // ViewState["ALLOCATIONNO"] = ALLOCATIONNO;
            hdsrno.Value = int.Parse(btnEditS.CommandArgument).ToString();
            ShowRuleDetails(SRNO);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Postponement.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ShowRuleDetails(int SRNO)
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("ACD_STUDENT_PROGRAM_CANCEL", "*", "", "SRNO=" + SRNO, string.Empty);
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtReasonWithdrawal.Text = ds.Tables[0].Rows[0]["REASON"].ToString();
                txtBankName.Text = ds.Tables[0].Rows[0]["BANKNAME"].ToString();
                txtBranchName.Text = ds.Tables[0].Rows[0]["BRANCHADDRESS"].ToString();
                txtAccountNumber.Text = ds.Tables[0].Rows[0]["ACCOUNTNO"].ToString();
                txtIFSCCode.Text = ds.Tables[0].Rows[0]["IFSCCODE"].ToString();
                int abjust = Convert.ToInt32(ds.Tables[0].Rows[0]["ABJUST_PAYMENT"].ToString());
                int chkAbPay=1;
                if (chkAbPay == abjust)
                {
                    chkAbPa.Checked = true;
                }
                else
                {
                    chkAbPa.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Postponement.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
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
    private string GetContentType(string fileName)
    {
        string strcontentType = "application/octetstream";
        string ext = System.IO.Path.GetExtension(fileName).ToLower();
        Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
        if (registryKey != null && registryKey.GetValue("Content Type") != null)
            strcontentType = registryKey.GetValue("Content Type").ToString();
        return strcontentType;
    }

    protected void lnkViewDoc_Click1(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
         

          
            string FileName = lnk.CommandName.ToString();

            int value = int.Parse(lnk.CommandArgument);
            if (value == 0)
            {
                ImageViewer.Visible = true;
                //iframe1.Visible = false;
                ImageViewer.ImageUrl = "~/showimage.aspx?id=" + Convert.ToInt32(Session["idno"]) + "&type=STUDENT";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
            }
            else
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
        catch (Exception ex)
        {

        }

    }
}