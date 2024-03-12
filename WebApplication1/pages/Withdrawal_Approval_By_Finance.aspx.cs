using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Web.Services;
using System.Web.Script.Services;

using System.Text;

using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

using System.Net;
using System.Net.Mail;
using DynamicAL_v2;
using _NVP;
using System.Collections.Specialized;

using EASendMail;

using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Web.UI.WebControls;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web.Configuration;

using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

using System.Linq;
using System.Collections.Generic;

using System.Web.Script.Serialization;
using Newtonsoft.Json;
using RestSharp;
public partial class EXAMINATION_Projects_Withdrawal_Approval_By_Finance : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController stud = new StudentController();
    DocumentControllerAcad objdocContr = new DocumentControllerAcad();
    UserController user = new UserController();
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                    string collge = objCommon.LookUp("USER_ACC", "UA_COLLEGE_NOS", "UA_NO=" + Session["userno"]);
                    objCommon.FillDropDownList(ddlfaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + collge + ")", "");
                    this.objCommon.FillDropDownList(ddltypedire, "ACD_REFUND_REQUEST_TYPE", "SRNO", "REQUEST_TYPE", "SRNO NOT IN (10)", "");
                }
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACDEMIC_COLLEGE_MASTER.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    private void binddata()
    {
        if (rdbFilter.SelectedValue == "1")
        {
            DataSet ds = null; string[] date;

            if (Convert.ToString(hdnDate.Value) == string.Empty || Convert.ToString(hdnDate.Value) == "0")
            {
                date = "0,0".Split(',');
            }
            else
            {
                date = hdnDate.Value.ToString().Split('-');
                date[0] = Convert.ToDateTime(date[0]).ToString("dd/MM/yyyy");
                date[1] = Convert.ToDateTime(date[1]).ToString("dd/MM/yyyy");
            }
            ds = stud.getwithapprovaldataforfinance(1, date[1], date[0],Convert.ToInt32(ddltypedire.SelectedValue),Convert.ToInt32(ddlfaculty.SelectedValue));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                ViewState["name"] = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                ViewState["regno"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                ViewState["EMAILID"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();

               // btnSubmit.Visible = true;
                btnCancel.Visible = true;
                divwithapti.Visible = true;
                lvwithap.DataSource = ds;
                lvwithap.DataBind();
            }

            else
            {
                hdnDate.Value=null;
                btnCancel.Visible = true;
                lvwithap.DataSource = null;
                lvwithap.DataBind();
                objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
            }
            foreach (ListViewDataItem lvitem in lvwithap.Items)
            {
                Label labelstatus = lvitem.FindControl("labelstatus") as Label;
                LinkButton lnkstuddoc = lvitem.FindControl("lnkstuddoc") as LinkButton;
                if (lnkstuddoc.ToolTip == "" || lnkstuddoc.ToolTip == null)
                {
                    lnkstuddoc.Visible = false;
                }
                else
                {
                    lnkstuddoc.Visible = true;
                }
            }
        }
        else if (rdbFilter.SelectedValue == "2")
        {
            DataSet ds = null; string[] date;

            if (Convert.ToString(hdnDate.Value) == string.Empty || Convert.ToString(hdnDate.Value) == "0")
            {
                date = "0,0".Split(',');
            }
            else
            {
                date = hdnDate.Value.ToString().Split('-');
                date[0] = Convert.ToDateTime(date[0]).ToString("dd/MM/yyyy");
                date[1] = Convert.ToDateTime(date[1]).ToString("dd/MM/yyyy");
            }
            ds = stud.getwithapprovaldataforfinance(2, date[1], date[0], Convert.ToInt32(ddltypedire.SelectedValue), Convert.ToInt32(ddlfaculty.SelectedValue));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                ViewState["name"] = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                ViewState["regno"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                ViewState["EMAILID"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();

                hdnDate.Value = null;
                btnCancel.Visible = true;
                divwithapti.Visible = true;
                lvwithap.DataSource = ds;
                lvwithap.DataBind();
            }

            else
            {
                hdnDate.Value = null;
                btnCancel.Visible = true;
                lvwithap.DataSource = null;
                lvwithap.DataBind();
                objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
            }
            foreach (ListViewDataItem lvitem in lvwithap.Items)
            {
                Label labelstatus = lvitem.FindControl("labelstatus") as Label;
                LinkButton lnkstuddoc = lvitem.FindControl("lnkstuddoc") as LinkButton;
                if (lnkstuddoc.ToolTip == "" || lnkstuddoc.ToolTip == null)
                {
                    lnkstuddoc.Visible = false;
                }
                else
                {
                    lnkstuddoc.Visible = true;
                }
            }
        }
        else if (rdbFilter.SelectedValue == "3")
        {
            DataSet ds = null; string[] date;

            if (Convert.ToString(hdnDate.Value) == string.Empty || Convert.ToString(hdnDate.Value) == "0")
            {
                date = "0,0".Split(',');
            }
            else
            {
                date = hdnDate.Value.ToString().Split('-');
                date[0] = Convert.ToDateTime(date[0]).ToString("dd/MM/yyyy");
                date[1] = Convert.ToDateTime(date[1]).ToString("dd/MM/yyyy");
            }
            ds = stud.getwithapprovaldataforfinance(3, date[1], date[0], Convert.ToInt32(ddltypedire.SelectedValue), Convert.ToInt32(ddlfaculty.SelectedValue));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                ViewState["name"] = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                ViewState["regno"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                ViewState["EMAILID"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();

                //btnSubmit.Visible = true;
                btnCancel.Visible = true;
                divwithapti.Visible = true;
                lvwithap.DataSource = ds;
                lvwithap.DataBind();
            }
            else
            {
                hdnDate.Value = null;
                btnCancel.Visible = true;
                lvwithap.DataSource = null;
                lvwithap.DataBind();
                objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
            }
            foreach (ListViewDataItem lvitem in lvwithap.Items)
            {
                Label labelstatus = lvitem.FindControl("labelstatus") as Label;
                LinkButton lnkstuddoc = lvitem.FindControl("lnkstuddoc") as LinkButton;
                if (lnkstuddoc.ToolTip == "" || lnkstuddoc.ToolTip == null)
                {
                    lnkstuddoc.Visible = false;
                }
                else
                {
                    lnkstuddoc.Visible = true;
                }
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsUserContact = null;
            string message = "";
            string subject = "";
            dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));
            int ua_no = Convert.ToInt32(Session["userno"]);
            CustomStatus cs = 0;
            foreach (ListViewDataItem dataitem in lvwithap.Items)
            {

                string status = string.Empty;
                int idno = 0;
                string status1 = "";
                CheckBox chkBox = dataitem.FindControl("chktransfer") as CheckBox;
                DropDownList dstatus = dataitem.FindControl("ddlstatus") as DropDownList;

                if (chkBox.Checked == true)
                {
                    idno = Convert.ToInt32(chkBox.ToolTip);
                    status = dstatus.Text;
                    status1 = Convert.ToString(dstatus.SelectedItem);

                    cs = (CustomStatus)stud.UPDFINANCESTATUS(idno, status);
                    cs = (CustomStatus)stud.Updwith(idno, status);
                    subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                    message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                    Execute(message, Convert.ToString(dsUserContact.Tables[2].Rows[0]["FINANCE_EMAIL"]), subject, Convert.ToString(ViewState["name"]), Convert.ToString(ViewState["regno"]), status1, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
                    clear();

                }
            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
               
                objCommon.DisplayMessage(this.Page, "Record saved successfully.", this.Page);
                lvwithap.DataSource = null;
                lvwithap.DataBind();
                btnSubmit.Visible = false;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please Select At least One checkbox", this.Page);
            }

        noresult:
            { }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Offered_Course.btnAd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    static async Task Execute(string message, string toSendAddress, string Subject, string firstname, string username, string matter, string sendemail, string emailpass)
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
            //oMail.HtmlBody = oMail.HtmlBody.Replace("[matter]", matter.ToString());
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
    private void clear()
    {
        rdbFilter.ClearSelection();
        ddltypedire.SelectedIndex = 0;
        divwithapti.Visible = false;
        lvwithap.DataSource = null;
        lvwithap.DataBind();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        binddata();
    }
    private void bindrefundamountdetails()
    {
        try
        {

            DataSet ds = stud.GetRefundAmountDetails(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ViewState["srno"]), Convert.ToInt32(ViewState["request_type"]));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                refund.Visible = true;
                lvrefunddetails.DataSource = ds;
                lvrefunddetails.DataBind();
            }
            else
            {
                refund.Visible = true;
                lvrefunddetails.DataSource = ds;
                lvrefunddetails.DataBind();
            }
        }
        catch(Exception ex)
        {
        }
    }
    
    protected void lnkViewDoc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            int Idno = int.Parse(lnk.CommandArgument);
            int srno = int.Parse(lnk.CommandName);
            DataSet ds = null;
            hdfSrnoWithDrwal.Value = Convert.ToString(srno);
            ViewState["srno"] = srno;
            hdfIdnoWithDrwal.Value = Convert.ToString(Idno);
            ds = objdocContr.GetAllWidrawlStudentDetails(Idno, srno, 0, "0", "0", 1, 4,"");
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                refundamount.Text = ds.Tables[0].Rows[0]["REFUND_AMOUNT"].ToString();
                nonrefundamount.Text = ds.Tables[0].Rows[0]["NONREFUND_AMOUNT"].ToString();
                lblStudentID.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                ViewState["regno"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                ViewState["idno"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
                lblStudentName.Text = ds.Tables[0].Rows[0]["STUDFIRSTNAME"].ToString();
                lblDate.Text = ds.Tables[0].Rows[0]["STUDENT_APPLIED_DATE"].ToString();
                txtRequestDescription.Text = ds.Tables[0].Rows[0]["REASON"].ToString();
                lblExpectedRefund.Text = ds.Tables[0].Rows[0]["REMARK_BY_DIRECTOR"].ToString();
                txtCalculateRefund.Text = ds.Tables[0].Rows[0]["CALCULATED_AMOUNT"].ToString();
                txtAmountRefund.Text = ds.Tables[0].Rows[0]["TOTAL_REFUND_AMOUNT"].ToString();
                ViewState["refund_amount"] = ds.Tables[0].Rows[0]["EXPECTED_REFUND_BY_ADMIN"].ToString();
                ddlstabyfinance.SelectedValue = ds.Tables[0].Rows[0]["FINANCE_APPROVAL"].ToString();               
                ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                lblFeesPaid.Text = ds.Tables[0].Rows[0]["OPERATOR_REMARK"].ToString();
                lblFeesBalance.Text = ds.Tables[0].Rows[0]["REMARK_BY_ADMIN"].ToString();
                txtremarkfi.Text = ds.Tables[0].Rows[0]["STUDENT_REMARK_BY_O_M_D_F"].ToString();
                txtRequestDate.Text = ds.Tables[0].Rows[0]["REFUND_DATE"].ToString();
                lblBankName.Text = ds.Tables[0].Rows[0]["BANKNAME"].ToString();
                lblBranchName.Text = ds.Tables[0].Rows[0]["BANKBRANCH"].ToString();
                lblAccountNumber.Text = ds.Tables[0].Rows[0]["ACCOUNTNO"].ToString();
                lblIFSCCode.Text = ds.Tables[0].Rows[0]["BRANCHCODE"].ToString();

                lblopratordate.Text = ds.Tables[0].Rows[0]["OPERATOR_APPROVED_DATE"].ToString();
                lblmandate.Text = ds.Tables[0].Rows[0]["MANAGER_APPROVED_DATE"].ToString();
                lbldirectordate.Text = ds.Tables[0].Rows[0]["DIRECTOR_APPROVED_DATE"].ToString();

                reqtype.Text = ds.Tables[0].Rows[0]["STATUS_NO"].ToString();
                ViewState["statusno"] = ds.Tables[0].Rows[0]["STATUS_NO"].ToString();
                ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                ViewState["request_type"] = ds.Tables[0].Rows[0]["STATUS_NO"].ToString();
                ViewState["REQUEST_TYPE_NAME"] = ds.Tables[0].Rows[0]["REQUEST_TYPE"].ToString();
                ViewState["FINANCE_APPROVAL"] = ds.Tables[0].Rows[0]["FINANCE_APPROVAL"].ToString();
                if (Convert.ToInt32(ViewState["FINANCE_APPROVAL"]) == 1)
                {
                    txtAmountRefund.Enabled = false;
                    ddlstabyfinance.Enabled = false;
                    txtremarkfi.Enabled = false;
                }
                else
                {
                    txtAmountRefund.Enabled = true;
                    ddlstabyfinance.Enabled = true;
                    txtremarkfi.Enabled = true;
                }

                bindrefundamountdetails();
                bindPriviousRefundDire();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);             
                ViewState["BlobImage"] = null;
                hdfSrnoWithDrwal.Value = null;
                hdfIdnoWithDrwal.Value = null;
            }
        }
       catch (Exception ex)
        {

        }
        
    }
    private void bindPriviousRefundDire()
    {
        try
        {

            string SP_Name2 = "PKG_ACD_GET_REFUND_AMOUNT_LIST";
            string SP_Parameters2 = "@P_IDNO,@P_REQUEST_TYPE";
            string Call_Values2 = "" + Convert.ToInt32(ViewState["idno"]) + "," + Convert.ToInt32(ViewState["request_type"]) + "";
            DataSet dsFacWiseCourseList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (dsFacWiseCourseList.Tables[0].Rows.Count > 0)
            {
                divpaydire.Visible = true;
                LvpayDire.DataSource = dsFacWiseCourseList;
                LvpayDire.DataBind();
            }
            else
            {
                divpaydire.Visible = false;
                LvpayDire.DataSource = null;
                LvpayDire.DataBind();
            }
        }
        catch (Exception ex)
        {
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
    protected void btnSubmitWithdrawalApprovalFinance_Click(object sender, EventArgs e)
    {
        try
        {
            int count =0;
            CustomStatus cs = CustomStatus.Others;
            DataSet dsUserContact = null;
            string message = "";
            string subject = "";
            string status1 = Convert.ToString(ddlstabyfinance.SelectedItem);
            dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));
            int idno = Convert.ToInt32(hdfIdnoWithDrwal.Value);
            string filename = "";
            string docname = "";
            if (fuDocument.HasFile)
            {
                string contentType = contentType = fuDocument.PostedFile.ContentType;
                string ext = System.IO.Path.GetExtension(fuDocument.PostedFile.FileName);
                HttpPostedFile file = fuDocument.PostedFile;
                 filename = fuDocument.PostedFile.FileName;
                 docname = idno + "_doc_" + "Withdrawal" + ext;
                 if (ext == ".pdf" || ext == ".PDF" || ext == ".jpg" || ext == ".JPG" || ext == ".png" || ext == ".PNG" || ext == ".jpeg" || ext == ".JPEG")
                 {
                     int retval = Blob_Upload(blob_ConStr, blob_ContainerName, idno + "_doc_" + "Withdrawal" + "", fuDocument);
                     if (retval == 0)
                     {
                         ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                         return;
                     }
                 }
            }
            else
            {
                docname = "";
            }
            if (txtAmountRefund.Text == "")
            {
                txtAmountRefund.Text = "0";
            }
            foreach (ListViewDataItem dataitem in lvrefunddetails.Items)
            {
                CheckBox chkBox = dataitem.FindControl("chktransfer") as CheckBox;
                TextBox txtrefundamt = dataitem.FindControl("txtrefundamt") as TextBox;
                HiddenField hdfdcr = dataitem.FindControl("hdfdcr") as HiddenField;
                HiddenField hdftempdcr_no = dataitem.FindControl("hdftempdcr_no") as HiddenField;
                if (txtrefundamt.Text == "" && txtrefundamt.Text == "")
                {
                    objCommon.DisplayMessage(this.updfinance, "Please Enter Refund Amount", this.Page);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "financemodal", "$(document).ready(function () {$('#financemodal').modal();});", true);                
                    return;
                }
                if (chkBox.Checked == true && chkBox.Enabled == false)
                {
                    count++;
                    decimal refundamt = 0.0m;
                    
                    refundamt = Convert.ToDecimal(txtrefundamt.Text);
                    cs = (CustomStatus)objdocContr.UpdateWithdrawlFINANCEDetails(Convert.ToInt32(hdfIdnoWithDrwal.Value), Convert.ToInt32(hdfSrnoWithDrwal.Value), 0, 0, txtremarkfi.Text, Convert.ToInt32(Session["userno"]), Convert.ToInt32(refundamount.Text), docname, Convert.ToString(txtCalculateRefund.Text), Convert.ToString(txtRequestDescription.Text), Convert.ToInt32(ddlstabyfinance.SelectedValue), Convert.ToDecimal(txtrefundamt.Text), Convert.ToString(hdfdcr.Value), Convert.ToDecimal(txtAmountRefund.Text), Convert.ToInt32(ViewState["SEMESTERNO"]), Convert.ToInt32(ViewState["statusno"]));
                    string SP_Name = "PKG_GET_DETAILS_FOR_API";
                    string SP_Parameters = "@P_IDNO,@P_TEMP_DCR_NO";
                    string Call_Values = "" + Convert.ToInt32(hdfIdnoWithDrwal.Value) + "," + Convert.ToInt32(hdftempdcr_no.Value) + "";
                    DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
                    if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                    {
                        //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                        var clientUrl = new RestClient("http://172.16.68.55:7048/DynamicsNAV80/OData/Company('SLIIT')/studentreceipts?$format=json");
                        clientUrl.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Authorization", "Basic SU5UVVNFUjpmRGVWK2JOTGFlQlZuZVFZaFNiczBMZ3RpQjdHNWlDZDJxT1B1dS9KVXRzPQ==");
                        request.AddHeader("Content-Type", "application/json");
                        request.AddParameter("application/json", "{\r\n\"Journal_Template\": \"" + ds.Tables[0].Rows[0]["JOURNAL_TEMPLATE_NAME"].ToString() + "\",\r\n \"Journal_Batch\": \"" + ds.Tables[0].Rows[0]["JOUNRAL_BATCH"].ToString() + "\",\r\n \"Student_No_Referance\": \"" + ds.Tables[0].Rows[0]["REGISTRATION_NO"].ToString() + "\",\r\n \"SLR_No\": \"" + ds.Tables[0].Rows[0]["APPLICATION_NO"].ToString() + "\",\r\n \"Posting_Date\": \"" + ds.Tables[0].Rows[0]["POSTING_DATE"].ToString() + "\",\r\n \"Document_Type\": \"" + ds.Tables[0].Rows[0]["DOCUMENT_TYPE"].ToString() + "\",\r\n \"Description\": \"" + ds.Tables[0].Rows[0]["NAME_WITH_INITIAL"].ToString() + "\",\r\n \"Amount\": \"" + ds.Tables[0].Rows[0]["AMOUNT"].ToString() + "\",\r\n \"Bal_Account_No\": \"" + ds.Tables[0].Rows[0]["BAL_ACCOUNT_TYPE"].ToString() + "\",\r\n \"Bank_Deposit_Date\": \"" + ds.Tables[0].Rows[0]["BANK_DEPOSIT_DATE"].ToString() + "\",\r\n \"Narration\": \"" + ds.Tables[0].Rows[0]["NARRATION"].ToString() + "\",\r\n \"Reference\": \"" + ds.Tables[0].Rows[0]["DOCUMENT_NO"].ToString() + "\",\r\n \"Receipt_Type\": \"" + ds.Tables[0].Rows[0]["RECEIPT_TYPE"].ToString() + "\",\r\n \"External_Document_No\": \"" + ds.Tables[0].Rows[0]["EXTERNAL_DOCUMENT_NO"].ToString() + "\"\r\n}", RestSharp.ParameterType.RequestBody);
                        IRestResponse response = clientUrl.Execute(request);
                        string parameter = "Journal_Template:" + ds.Tables[0].Rows[0]["JOURNAL_TEMPLATE_NAME"].ToString() + " Journal_Batch:" + ds.Tables[0].Rows[0]["JOUNRAL_BATCH"].ToString() + " Student_No_Referance:" + ds.Tables[0].Rows[0]["REGISTRATION_NO"].ToString() + " SLR_No:" + ds.Tables[0].Rows[0]["APPLICATION_NO"].ToString() + " Posting_Date:" + ds.Tables[0].Rows[0]["POSTING_DATE"].ToString() + " Document_Type:" + ds.Tables[0].Rows[0]["DOCUMENT_TYPE"].ToString() + " Description:" + ds.Tables[0].Rows[0]["NAME_WITH_INITIAL"].ToString() + " Amount:" + Convert.ToDecimal(txtAmountRefund.Text) + " Bal_Account_No:" + ds.Tables[0].Rows[0]["BAL_ACCOUNT_TYPE"].ToString() + " Bank_Deposit_Date:" + ds.Tables[0].Rows[0]["BANK_DEPOSIT_DATE"].ToString() + " Narration:" + ds.Tables[0].Rows[0]["NARRATION"].ToString() + " Reference:" + ds.Tables[0].Rows[0]["DOCUMENT_NO"].ToString() + " Receipt_Type:" + ds.Tables[0].Rows[0]["RECEIPT_TYPE"].ToString() + " External_Document_No:" + ds.Tables[0].Rows[0]["EXTERNAL_DOCUMENT_NO"].ToString();
                        string SP_Name1 = "PKG_ACD_RESPONSE_FOR_STUDENT_API";

                        string SP_Parameters1 = "@P_IDNO,@P_RESULT,@P_STATUS,@P_RESPONCE_VALUE,@P_OUTPUT";
                        string Call_Values1 = "" + Convert.ToInt32(Convert.ToInt32(hdfIdnoWithDrwal.Value)) + "," + Convert.ToString(response.StatusDescription) + "," + Convert.ToString(ViewState["REQUEST_TYPE_NAME"]) + "," + Convert.ToString(parameter) + ",0";
                       
                        string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);
                    }
                
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(this.updfinance, "Please Select At least One checkbox", this.Page);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
            }
            
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updfinance, "Record Saved Successfully", this.Page);             
                subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                Execute(message, Convert.ToString(dsUserContact.Tables[2].Rows[0]["FINANCE_EMAIL"]), subject, Convert.ToString(ViewState["name"]), Convert.ToString(ViewState["regno"]), status1, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
                clear();
            }
            else
            {
                //objCommon.DisplayMessage(this.updfinance, "Please Select At least One checkbox", this.Page);
               
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
            string extension = Path.GetExtension(img.ToString());
            if (extension == ".pdf")
            {
                if (img != null || img != "")
                {
                    DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
                    var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                    string filePath = directoryPath + "\\" + ImageName;
                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    //Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
                    //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    //embed += "</object>";
                    //ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "financemodal", "$(document).ready(function () {$('#financemodal').modal();});", true);
                    ViewState["filePath_Show"] = filePath;
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                    string Base64String = Convert.ToBase64String(bytes);
                    Session["Base64String"] = Base64String;
                    Session["Base64StringType"] = "application/pdf";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "financemodal", "$(document).ready(function () {$('#financemodal').modal();});", true);
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Sorry, File not found !!!", this.Page);
                }
            }
           
            else
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "financemodal", "$(document).ready(function () {$('#financemodal').modal();});", true);
                //hdfImagePath.Value = null;
                //ImageViewer1.Visible = false;
                //ltEmbed1.Visible = false;
                //imageViewerContainer.Visible = true;
                //var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                //string filePath = directoryPath + "\\" + ImageName;
                //if ((System.IO.File.Exists(filePath)))
                //{
                //    System.IO.File.Delete(filePath);
                //}

                //Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                //hdfImagePath.Value = ResolveUrl("~/DownloadImg/" + ImageName);
                //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "myModal33", "$(document).ready(function () {$('#myModal33').modal();});", true);
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnCancelWithdrawalApprovalFinance_Click(object sender, EventArgs e)
    {
        txtAmountRefund.Text = "";
        txtRequestDate.Text = "";
    }
    protected void btnCalculateRefund_Click(object sender, EventArgs e)
    {
        DataSet ds = null;

        ds = objdocContr.GetCalculateData(Convert.ToString(ViewState["regno"]),1,1,1);
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
         {
             ViewState["STUDAPPLIEDDATE"] = ds.Tables[0].Rows[0]["STUDENT_APPLIED_DATE"].ToString();
             ViewState["TOTAL_AMT"] = ds.Tables[0].Rows[0]["TOTAL_AMT"].ToString();                      
             if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
             {
                 ViewState["RULEDATE"] = ds.Tables[1].Rows[0]["WITHEFFECTFROM"].ToString();
             }
             if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
             {
                 ViewState["days"] = ds.Tables[2].Rows[0]["NOOFDAYS"].ToString();
                 ViewState["per"] = ds.Tables[2].Rows[0]["PERCENTAGE"].ToString();
                 DateTime dt1 = DateTime.Parse(Convert.ToDateTime(ViewState["RULEDATE"]).ToString("dd/MM/yyyy"));
                 DateTime dt2 = DateTime.Parse(Convert.ToDateTime(ViewState["STUDAPPLIEDDATE"]).ToString("dd/MM/yyyy"));
                 TimeSpan ts = dt2.Subtract(dt1);
                 int days = ts.Days;
                 decimal test = ((Convert.ToDecimal(ViewState["TOTAL_AMT"]) * Convert.ToInt32(ViewState["per"])) / 100);
                 txtCalculateRefund.Text = test.ToString();
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
             }
             else
             {              
                 txtCalculateRefund.Text = "0.00";
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
             }
             
         }
        
    }
    protected void lnkdetails_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        int srno = int.Parse(lnk.CommandArgument);
        int idno = int.Parse(lnk.CommandName);
        string username = (lnk.ToolTip);
        ShowReport("Finance Details", "FinanceDetails.rpt", idno, srno, username);
    }
    private void ShowReport(string reportTitle, string rptFileName,int idno,int srno,string username)
    {
        try
        { 
            //string url = "http://localhost:59566/PresentationLayer/Reports/CommonReport.aspx?";
            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SRNO=" + srno  ;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
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
}