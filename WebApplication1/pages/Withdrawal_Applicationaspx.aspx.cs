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

public partial class Projects_Withdrawal_Applicationaspx : System.Web.UI.Page
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
                    this.CheckPageAuthorization();

                    if (Convert.ToString(ViewState["action"]) == "")
                    {
                        ViewState["action"] = "add";
                    }

                    // Check User Authority 


                    ViewState["userno"] = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + Session["username"].ToString() + "'");

                    Page.Title = Session["coll_name"].ToString();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                 
                    if (Session["StudentPayDetail"] != null)
                    {
                        Session["StudentPayDetail"] = null;
                    }
                    //Set the Page Title

                    Page.Title = Session["coll_name"].ToString();
                    if (Session["usertype"].ToString().Equals("2"))     //Student 
                    {
                        objCommon.FillDropDownList(ddlWithdrawalType, "ACD_REFUND_REQUEST_TYPE", "SRNO", "REQUEST_TYPE", "SRNO NOT IN (10)", "");
                        ShowStudentDetails();
                        BindListView();
                        //ShowStudentBankDetail();
                        int userno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DISTINCT USERNO", "IDNO=" + Convert.ToInt32(Session["idno"])));
                        ViewState["USER_NO"] = userno;


                    }
                    else
                    {
                        objCommon.DisplayUserMessage(updwithapp, "Record Not Found.This Page is use for only Student Login!!", this.Page);
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
                Response.Redirect("~/notauthorized.aspx?page=Withdrawal_Applicationaspx.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Withdrawal_Applicationaspx.aspx");
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
                    //btnCalculate.Visible = false;
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
                    //lblfeepaid.Text = dr["PAID"].ToString() == string.Empty ? string.Empty : dr["PAID"].ToString();
                    //lblexpectedrefund.Text = dr["EXPECTED_REFUND_BY_ADMIN"].ToString() == string.Empty ? string.Empty : dr["EXPECTED_REFUND_BY_ADMIN"].ToString();
                    //lblbalance.Text = dr["TOTALBALANCE"].ToString() == string.Empty ? string.Empty : dr["TOTALBALANCE"].ToString();
                    lblSemester.Text = dr["SEMESTERNAME"].ToString() == string.Empty ? string.Empty : dr["SEMESTERNAME"].ToString();

                    Session["PAID"] = dr["PAID"].ToString();
                    Session["TOTALBALANCE"] = dr["TOTALBALANCE"].ToString();
                    Session["DEGREENO"] = dr["DEGREENO"].ToString();
                    Session["BRANCHNO"] = dr["BRANCHNO"].ToString();
                    Session["COLLEGE_ID"] = dr["COLLEGE_ID"].ToString();
                    Session["REGNO"] = dr["REGNO"].ToString();
                    Session["ADMBATCH"] = dr["admbatch"].ToString();
                    Session["SEMESTERNO"] = dr["SEMESTERNO"].ToString();

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
        int COUNT=0;
        int countcancelsem = 0;
        int countcancelmodule = 0;
        EmailSmsWhatsaap Email = new EmailSmsWhatsaap();
        if (ddlWithdrawalType.SelectedValue == "2")
        {
            countcancelsem = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_PROGRAM_CANCEL", "COUNT(1)", "ISNULL(FINANCE_APPROVAL,0)=1 AND ISNULL(STATUS_NO,0)=" + ddlWithdrawalType.SelectedValue + "AND IDNO=" + Session["idno"]));
            if (countcancelsem > 0)
            {
                objCommon.DisplayMessage(this.Page, "You can not Apply More than One Request For " + ddlWithdrawalType.SelectedItem + " !!!", this.Page);
                clear();
                return;
            }
        }
        if (ddlWithdrawalType.SelectedValue == "4")
        {
            countcancelmodule = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_PROGRAM_CANCEL", "COUNT(1)", "ISNULL(FINANCE_APPROVAL,0)=1 AND ISNULL(STATUS_NO,0)=" + ddlWithdrawalType.SelectedValue + "AND IDNO=" + Session["idno"]));
            if (countcancelmodule > 0)
            {
                objCommon.DisplayMessage(this.Page, "You can not Apply More than One Request For " + ddlWithdrawalType.SelectedItem + " !!!", this.Page);
                clear();
                return;
            }
        }
        int admcan = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COUNT(1)", "ISNULL(CAN,0)=1 AND ISNULL(ADMCAN,0)=1 AND IDNO=" + Session["idno"]));
        if (admcan > 0)
        {
            objCommon.DisplayMessage(this.Page, "You can not Apply For " + ddlWithdrawalType.SelectedItem + " Request !!!", this.Page);
            clear();
            return;
        }
        else
        {
            if (ddlWithdrawalType.SelectedValue == "4")
            {
                COUNT = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1)", "RECIEPT_CODE='BF'AND IDNO=" + Session["idno"] + "AND PAY_SERVICE_TYPE in(1,2) AND RECON=1 AND CAN=0 AND DELET=0" + "AND SEMESTERNO=" + Convert.ToInt32(Session["SEMESTERNO"])));

            }
            else
            {

                COUNT = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1)", "RECIEPT_CODE='TF'AND IDNO=" + Session["idno"] + "AND PAY_SERVICE_TYPE in(1,2) AND RECON=1 AND CAN=0 AND DELET=0" + "AND SEMESTERNO=" + Convert.ToInt32(Session["SEMESTERNO"])));
            }
            int marks = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "IDNO=" + Session["idno"] + "AND REGISTERED=1 AND EXAM_REGISTERED=1 AND PREV_STATUS=0" + "AND SEMESTERNO=" + Convert.ToInt32(Session["SEMESTERNO"]) + "AND (INTERMARK is not NULL or S1MARK is not NULL) AND  EXTERMARK is not NULL"));
            if (COUNT == 0)
            {
                objCommon.DisplayMessage(this.Page, "You can not Apply For " + ddlWithdrawalType.SelectedItem + " Request !!!", this.Page);
                clear();
                return;
            }
            else if (marks > 0)
            {
                objCommon.DisplayMessage(this.Page, "You can not Apply Because Mark Entry Done!!!", this.Page);
                clear();
                return;
            }
            else
            {

                if (ddlWithdrawalType.SelectedValue == "3")
                {
                    int regcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "EXAM_REGISTERED=1 AND REGISTERED=1 AND PREV_STATUS=0 AND IDNO=" + Session["idno"] + "AND SEMESTERNO=" + Convert.ToInt32(Session["SEMESTERNO"])));
                    if (regcount == 0)
                    {
                        objCommon.DisplayMessage(this.Page, "You can not Apply For " + ddlWithdrawalType.SelectedItem + " Request !!!", this.Page);
                        clear();
                        return;
                    }

                }
                idno = idno = Convert.ToInt32(Session["idno"]);
                int faculty = Convert.ToInt32(Session["COLLEGE_ID"]);
                int program = Convert.ToInt32(Session["BRANCHNO"]);
                int degreeno = Convert.ToInt32(Session["DEGREENO"]);
                int admbatch = Convert.ToInt32(Session["ADMBATCH"]);
                int semesterno = Convert.ToInt32(Session["SEMESTERNO"]);
                string Status = Convert.ToString(ddlWithdrawalType.SelectedItem);
                int StatusNo = Convert.ToInt32(ddlWithdrawalType.SelectedValue);
                string Reason = txtReasonWithdrawal.Text;
                string REGNO = "";
                if (Session["REGNO"] == "" || Session["REGNO"] == null)
                {
                    REGNO = "";
                }
                else
                {
                    REGNO = (Session["REGNO"]).ToString();
                }
                var todaydate = DateTime.Now;
                string filename = "";
                string docname = "";
                if (FileUpload1.HasFile)
                {
                    string contentType = contentType = FileUpload1.PostedFile.ContentType;
                    string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                    HttpPostedFile file = FileUpload1.PostedFile;
                    filename = FileUpload1.PostedFile.FileName;
                    if (ext == ".pdf" || ext == ".PDF" || ext == ".jpg" || ext == ".JPG" || ext == ".png" || ext == ".PNG" || ext == ".jpeg" || ext == ".JPEG")
                    {
                        if (ddlWithdrawalType.SelectedValue == "1")
                        {
                            docname = idno + "_doc_" + "Admission Withdrawal" + ext;
                            int retval = Blob_Upload(blob_ConStr, blob_ContainerName, idno + "_doc_" + "Admission Withdrawal" + "", FileUpload1);
                        }
                        else if (ddlWithdrawalType.SelectedValue == "2")
                        {
                            docname = idno + "_doc_" + "Semester Registration" + ext;
                            int retval = Blob_Upload(blob_ConStr, blob_ContainerName, idno + "_doc_" + "Semester Registration" + "", FileUpload1);
                        }
                        else if (ddlWithdrawalType.SelectedValue == "3")
                        {
                            docname = idno + "_doc_" + "Postponement" + ext;
                            int retval = Blob_Upload(blob_ConStr, blob_ContainerName, idno + "_doc_" + "Postponement" + "", FileUpload1);
                        }
                        else if (ddlWithdrawalType.SelectedValue == "4")
                        {
                            docname = idno + "_doc_" + "Pro-Rata" + ext;
                            int retval = Blob_Upload(blob_ConStr, blob_ContainerName, idno + "_doc_" + "Pro-Rata" + "", FileUpload1);
                        }
                    }

                }
                else
                {
                    docname = "";
                }
                if (ddlWithdrawalType.SelectedValue == "4")
                {
                    int PRORATPAY = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1)", "IDNO=" + Session["idno"] + "AND RECIEPT_CODE='BF'AND PAY_SERVICE_TYPE in(1,2) AND RECON=1 AND CAN=0 AND DELET=0" + "AND SEMESTERNO=" + Convert.ToInt32(Session["SEMESTERNO"])));
                    int prorata = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "IDNO=" + idno + "AND REGISTERED=1 AND EXAM_REGISTERED=1 AND PREV_STATUS=0 AND REGID=2" + "AND SEMESTERNO=" + Convert.ToInt32(Session["SEMESTERNO"]) + "AND (INTERMARK is not NULL or S1MARK is not NULL) AND  EXTERMARK is not NULL"));
                    if (PRORATPAY == 0)
                    {
                        objCommon.DisplayMessage(this.Page, "You can not Apply For " + ddlWithdrawalType.SelectedItem + " Request !!!", this.Page);
                        clear();
                        return;
                    }
                    else if (prorata > 0)
                    {
                        objCommon.DisplayMessage(this.Page, "You can not Apply For " + ddlWithdrawalType.SelectedItem + " Request !!!", this.Page);
                        clear();
                        return;
                    }


                }
                else
                {
                    DataSet dsUserContact = null;
                    string message = "";
                    string subject = "";
                    dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));

                    CustomStatus cs = CustomStatus.Others;
                    cs = (CustomStatus)feeController.InsertStudentWithdrawApplication(idno, faculty, program, REGNO, degreeno, docname, Reason, Status, StatusNo, todaydate);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.Page, "Record Added Successfully !!!", this.Page);
                        subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                        message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                        string toSendAddress = ViewState["EMAILID"].ToString();// 
                        MailMessage msgsPara = new MailMessage();
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine(message);
                        msgsPara.BodyEncoding = Encoding.UTF8;
                        msgsPara.Body = Convert.ToString(sb);
                        msgsPara.Body = msgsPara.Body.Replace("[USERFIRSTNAME]", ViewState["name"].ToString());
                        msgsPara.Body = msgsPara.Body.Replace("[USERNAME]", ViewState["regno"].ToString());
                        MemoryStream oAttachment1 = null; string AttachmentName = "";
                        Task<string> task = Email.Execute(msgsPara.Body, toSendAddress, subject, oAttachment1, AttachmentName.ToString());
                        string Res = task.Result;
                        //Execute(message, Convert.ToString(ViewState["EMAILID"]), subject, Convert.ToString(ViewState["name"]), Convert.ToString(ViewState["regno"]), filename, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();                   
                        BindListView();
                        clear();
                        return;
                    }
                    if (cs.Equals(CustomStatus.TransactionFailed))
                    {
                        objCommon.DisplayMessage(this.Page, "Transaction Failed !!!", this.Page);
                        // bindlist();
                        return;
                    }
                }
                if (ddlWithdrawalType.SelectedValue == "2")
                {
                    string date = objCommon.LookUp("ACD_DEFINE_TOTAL_CREDIT", "COMMENCEMENT_DATE", "COLLEGE_ID=" + faculty + "AND BRANCHNO=" + program + "AND DEGREENO=" + degreeno + "AND ADMBATCH=" + admbatch + "AND SEMESTERNO=" + semesterno);
                    if (date == "")
                    {
                        date = null;
                    }
                    int count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "IDNO=" + idno + "AND REGISTERED=1 AND EXAM_REGISTERED=1 AND PREV_STATUS=0" + "AND SEMESTERNO=" + Convert.ToInt32(Session["SEMESTERNO"])));
                    if (date == null)
                    {
                        date = DateTime.Today.ToString();
                    }
                    if ((DateTime.Today > Convert.ToDateTime(date)))
                    {
                        objCommon.DisplayMessage(this.Page, "You can not Apply For " + ddlWithdrawalType.SelectedItem + " Request !!!", this.Page);
                        clear();
                        return;

                    }
                    else
                    {
                        if (count == 0)
                        {
                            objCommon.DisplayMessage(this.Page, "You can not Apply For " + ddlWithdrawalType.SelectedItem + " Request !!!", this.Page);
                            clear();
                            return;
                        }

                        DataSet dsUserContact = null;
                        string message = "";
                        string subject = "";
                        dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));

                        CustomStatus cs = CustomStatus.Others;
                        cs = (CustomStatus)feeController.InsertStudentWithdrawApplication(idno, faculty, program, REGNO, degreeno, docname, Reason, Status, StatusNo, todaydate);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(this.Page, "Record Added Successfully !!!", this.Page);
                            subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                            message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                            string toSendAddress = ViewState["EMAILID"].ToString();// 
                            MailMessage msgsPara = new MailMessage();
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine(message);
                            msgsPara.BodyEncoding = Encoding.UTF8;
                            msgsPara.Body = Convert.ToString(sb);
                            msgsPara.Body = msgsPara.Body.Replace("[USERFIRSTNAME]", ViewState["name"].ToString());
                            msgsPara.Body = msgsPara.Body.Replace("[USERNAME]", ViewState["regno"].ToString());
                            MemoryStream oAttachment1 = null; string AttachmentName = "";
                            Task<string> task = Email.Execute(msgsPara.Body, toSendAddress, subject, oAttachment1, AttachmentName.ToString());
                            string Res = task.Result;
                            //Execute(message, Convert.ToString(ViewState["EMAILID"]), subject, Convert.ToString(ViewState["name"]), Convert.ToString(ViewState["regno"]), filename, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();                      
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
                }
                else
                {

                    DataSet dsUserContact = null;
                    string message = "";
                    string subject = "";
                    dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));

                    CustomStatus cs = CustomStatus.Others;
                    cs = (CustomStatus)feeController.InsertStudentWithdrawApplication(idno, faculty, program, REGNO, degreeno, docname, Reason, Status, StatusNo, todaydate);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.Page, "Record Added Successfully !!!", this.Page);
                        subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                        message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                        string toSendAddress = ViewState["EMAILID"].ToString();// 
                        MailMessage msgsPara = new MailMessage();
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine(message);
                        msgsPara.BodyEncoding = Encoding.UTF8;
                        msgsPara.Body = Convert.ToString(sb);
                        msgsPara.Body = msgsPara.Body.Replace("[USERFIRSTNAME]", ViewState["name"].ToString());
                        msgsPara.Body = msgsPara.Body.Replace("[USERNAME]", ViewState["regno"].ToString());
                        MemoryStream oAttachment1 = null; string AttachmentName = "";
                        Task<string> task = Email.Execute(msgsPara.Body, toSendAddress, subject, oAttachment1, AttachmentName.ToString());
                        string Res = task.Result;
                        //Execute(message, Convert.ToString(ViewState["EMAILID"]), subject, Convert.ToString(ViewState["name"]), Convert.ToString(ViewState["regno"]), filename, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();

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
            }
        }
    }

    private void BindListView()
    {
        try
        {
            //int srno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_PROGRAM_CANCEL","SRNO","IDNO=" + Convert.ToInt32(Session["idno"])+ "AND STATUS_NO="+Convert.ToInt32(ddlWithdrawalType.SelectedValue)));
            DataSet ds = feeController.GetWithdrwalapplicationData(Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlWithdrawalType.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                ViewState["status"] = ds.Tables[0].Rows[0]["WITH_POST_APPROVAL"].ToString();
                lvlWithdra.DataSource = ds;
                lvlWithdra.DataBind();

            }
            foreach (ListViewDataItem dataitem in lvlWithdra.Items)
            {
                LinkButton lnkstuddoc = dataitem.FindControl("lnkstuddoc") as LinkButton;
                Label lbladminstatus = dataitem.FindControl("lbladminstatus") as Label;
                Label lblstatus = dataitem.FindControl("lblstatus") as Label;
                Label lblwithrefundstatus = dataitem.FindControl("lblwithrefundstatus") as Label;
                Label lbldocument = dataitem.FindControl("lbldocument") as Label;

                if (lbldocument.Text == "" || lbldocument.Text == null)
                {
                    lnkstuddoc.Visible = false;
                }
                else
                {
                    lnkstuddoc.Visible = true;
                }
                if (lblstatus.Text == "Approved" && lblwithrefundstatus.Text=="2")
                {
                    lbladminstatus.Visible = true;
                    lbladminstatus.Text = "Your Request Is Approved Now You Can Apply For Refund Using My Wallet.";

                }
                else if (lblstatus.Text == "Approved" && lblwithrefundstatus.Text == "1")
                {
                    lbladminstatus.Visible = true;
                    lbladminstatus.Text = "Your Request Is Approved But  You Can Not Apply For Refund.";

                }
                else if (lblstatus.Text == "Reject")
                {
                    lbladminstatus.Visible = true;
                    lbladminstatus.Text = "Your Request Is Reject You Can Not Apply For Refund.";
                }
                else
                {
                    lbladminstatus.Text = "";
                    lbladminstatus.Visible = false;
                }
            }
        }
        catch(Exception ex)
        {
        }
    }
    private void clear()
    {
        ddlWithdrawalType.SelectedIndex = 0;
        txtReasonWithdrawal.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    static async Task Execute(string message, string toSendAddress, string Subject, string firstname, string username, string filename, string sendemail, string emailpass)
    {
        try
        {
            //SmtpMail oMail = new SmtpMail("TryIt");
            //oMail.From = sendemail;
            //oMail.To = toSendAddress;
            //oMail.Subject = Subject;
            //oMail.HtmlBody = message;
            //oMail.HtmlBody = oMail.HtmlBody.Replace("[USERFIRSTNAME]", firstname.ToString());
            //oMail.HtmlBody = oMail.HtmlBody.Replace("[USERNAME]", username.ToString());
            //SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022
            //oServer.User = sendemail;
            //oServer.Password = emailpass;

            //oServer.Port = 587;

            //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            //EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();

            //oSmtp.SendMail(oServer, oMail);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            MailMessage msg = new MailMessage();
            msg.To.Add(new System.Net.Mail.MailAddress(toSendAddress));
            msg.From = new System.Net.Mail.MailAddress(sendemail);
            msg.Subject = Subject;
            StringBuilder sb = new StringBuilder();
            msg.Body = message;
            msg.Body = msg.Body.Replace("[USERFIRSTNAME]", firstname.ToString());
            msg.Body = msg.Body.Replace("[USERNAME]", username.ToString());
           
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(sendemail, emailpass);

            client.Port = 587; // You can use Port 25 if 587 is blocked (mine is!)
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
    protected void lnkstuddoc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            int value = int.Parse(lnk.CommandArgument);
            string FileName = lnk.CommandName.ToString();
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
            string img = FileName;
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
                objCommon.DisplayMessage(updwithapp, "Sorry, File not found !!!", this.Page);
            }
                
        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkfinancedoc_Click(object sender, EventArgs e)
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
        string img = ViewState["BlobImagefinance"].ToString();
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
            objCommon.DisplayMessage(updwithapp, "Sorry, File not found !!!", this.Page);
        }
                
    }
}