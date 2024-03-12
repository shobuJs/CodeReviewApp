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


public partial class LoanApply : System.Web.UI.Page
{
    private Common objCommon = new Common();
    private UAIMS_Common objUaimsCommon = new UAIMS_Common();
    private FeeCollectionController feeController = new FeeCollectionController();
    StudentController Stud = new StudentController();
    private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
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
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                    ViewState["action"] = "add";
                    //objCommon.SetLabelData("0");//for label
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
                    if (Session["usertype"].ToString() == "2")
                    {
                        ShowStudentDetails();
                        binddata();

                    }
                    else
                    {
                        objCommon.DisplayMessage(updloan, "This Page Is For Student Login", this.Page);
                        return;
                    }


                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeesRefundReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=LoanApply.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=LoanApply.aspx");
        }
    }

    private void ShowStudentDetails()
    {
        try
        {
            int idno = Convert.ToInt32(Session["idno"]);
            DataSet dsStudent = feeController.GetStudentInfoById_ForOnlinePayment(idno);
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                DataRow dr = dsStudent.Tables[0].Rows[0];
                lblStudentName1.Text = dr["STUDNAME"].ToString() == string.Empty ? string.Empty : dr["STUDNAME"].ToString();
                lblStdID.Text = dr["REGNO"].ToString() == string.Empty ? string.Empty : dr["REGNO"].ToString();
                lblSem.Text = dr["SEMESTERNAME"].ToString() == string.Empty ? string.Empty : dr["SEMESTERNAME"].ToString();
                lblMobileNo.Text = dr["STUDENTMOBILE"].ToString() == string.Empty ? string.Empty : dr["STUDENTMOBILE"].ToString();
                lblEmailID.Text = dr["EMAILID"].ToString() == string.Empty ? string.Empty : dr["EMAILID"].ToString();
                lblProgram1.Text = dr["PROGRAM"].ToString() == string.Empty ? string.Empty : dr["PROGRAM"].ToString();
                lblGender.Text = dr["SEX1"].ToString() == string.Empty ? string.Empty : dr["SEX1"].ToString();
                lblDOB1.Text = dr["DOB"].ToString() == string.Empty ? string.Empty : dr["DOB"].ToString();
                txtAppAmt.Text = dr["demandamt"].ToString() == string.Empty ? string.Empty : dr["demandamt"].ToString();

                if (dr["PHOTO"].ToString() != "")
                {
                    imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dr["IDNO"].ToString() + "&type=student";
                }

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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int idno = Convert.ToInt32(Session["idno"]);
            //decimal loanamt = Convert.ToDecimal(TxtLoanAmt.Text);
            string demandamt = (txtAppAmt.Text);
            string Reason = TxtLoanReason.Text;
            string filename = "";
            string docname = "";
            if (FileUpload1.HasFile)
            {
                string contentType = contentType = FileUpload1.PostedFile.ContentType;
                string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                HttpPostedFile file = FileUpload1.PostedFile;
                filename = FileUpload1.PostedFile.FileName;
                docname = idno + "_doc_" + "LoanApplication" + ext;

                int retval = Blob_Upload(blob_ConStr, blob_ContainerName, idno + "_doc_" + "LoanApplication" + "", FileUpload1);
                if (retval == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                    return;
                }
            }
            else
            {
                docname = "";
            }
            string SP_Name1 = "PKG_ACD_INSERT_LOAN_APPLICATION_FOR_STUDENT";
            string SP_Parameters1 = "@P_IDNO,@P_DEMAND_AMT,@P_LOAN_AMT,@P_LOAN_REASON,@P_DOCUMNET,@P_SCH_LOAN_NO,@P_OUTPUT";
            string Call_Values1 = "" + Convert.ToInt32(Session["idno"]) + "," + demandamt + "," + Convert.ToDecimal(TxtLoanAmt.Text) + "," +
               Reason + "," + docname + "," + Convert.ToInt32(ViewState["SRNO"]) + ",0";
            string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//insert
            if (que_out1 == "1")
            {
                objCommon.DisplayMessage(updloan, "Record Saved Successfully", this.Page);
                binddata();
                TxtLoanAmt.Text = "";
                TxtLoanReason.Text = "";
                return;
            }
        }
            catch (Exception ex)
        {
        }
               
    }
    private void binddata()
    {

        string SP_Name2 = "PKG_ACD_GET_LOAN_APPLICATION_DETAILS";
        string SP_Parameters2 = "@P_IDNO";
        string Call_Values2 = "" + Convert.ToInt32(Session["idno"]) + "";
        DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (dsStudList.Tables[0].Rows.Count > 0)
        {
            ViewState["SRNO"] = dsStudList.Tables[0].Rows[0]["SCH_LOAN_NO"].ToString();
            Panel2.Visible = true;
            LvLoan.DataSource = dsStudList;
            LvLoan.DataBind();
        }
        else
        {
            Panel2.Visible = false;
            LvLoan.DataSource = null;
            LvLoan.DataBind();
        }
       
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        TxtLoanAmt.Text = "";
        TxtLoanReason.Text="";
    }
}