using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Web.Configuration;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Security;
using System.Diagnostics;

using System.Collections.Generic;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using EASendMail;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;

public partial class Projects_Withdrawal_Aproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController stud = new StudentController();
    FeeCollectionController feecollection = new FeeCollectionController();
    DocumentControllerAcad objdocContr = new DocumentControllerAcad();
    UserController user = new UserController();
    EmailSmsWhatsaap Email = new EmailSmsWhatsaap();
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
                    this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {

                    }
                    string collge = objCommon.LookUp("USER_ACC", "UA_COLLEGE_NOS", "UA_NO=" + Session["userno"]);
                    objCommon.FillDropDownList(ddlWithdrawalType, "ACD_REFUND_REQUEST_TYPE", "SRNO", "REQUEST_TYPE", "SRNO NOT IN (10)", "");
                    objCommon.FillDropDownList(ddlfaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + collge + ")", "");

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

    private void CheckPageAuthorization()
    {
        // Request.QueryString["pageno"] = "ACADEMIC/EXAMINATION/ExamRegistration_New.aspx ?pageno=1801";
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Withdrawal_Aproval.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Withdrawal_Aproval.aspx");
        }
    }

    private void binddata()
    {
        try
        {
            if (rdbFilter.SelectedValue == "")
            {
                objCommon.DisplayMessage(this.Page, "Please Select Status", this.Page);
            }
            else
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
                    ds = feecollection.GetWithapplicationData(1, Convert.ToInt32(ddlWithdrawalType.SelectedValue), date[1], date[0],Convert.ToInt32(ddlfaculty.SelectedValue));
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                        ViewState["name"] = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                        ViewState["regno"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                        ViewState["EMAILID"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                        //ViewState["STATUS"] = ds.Tables[0].Rows[0]["STATUS"].ToString();

                        //btnSave.Visible = true;
                        btnCancel.Visible = true;
                        divwithapti.Visible = true;
                        lvwithap.DataSource = ds;
                        lvwithap.DataBind();
                    }

                    else
                    {
                        // btnSave.Visible = false;
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
                    ds = feecollection.GetWithapplicationData(2, Convert.ToInt32(ddlWithdrawalType.SelectedValue), date[1], date[0], Convert.ToInt32(ddlfaculty.SelectedValue));
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                        ViewState["name"] = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                        ViewState["regno"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                        ViewState["EMAILID"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                        //ViewState["STATUS"] = ds.Tables[0].Rows[0]["STATUS"].ToString();
                        //btnSave.Visible = true;
                        btnCancel.Visible = true;
                        divwithapti.Visible = true;
                        lvwithap.DataSource = ds;
                        lvwithap.DataBind();
                    }

                    else
                    {
                        //btnSave.Visible = false;
                        btnCancel.Visible = true;
                        hdnDate.Value = null;
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
                    ds = feecollection.GetWithapplicationData(3, Convert.ToInt32(ddlWithdrawalType.SelectedValue), date[1], date[0], Convert.ToInt32(ddlfaculty.SelectedValue));
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                        ViewState["name"] = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                        ViewState["regno"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                        ViewState["EMAILID"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                        //ViewState["STATUS"] = ds.Tables[0].Rows[0]["STATUS"].ToString();
                        //btnSave.Visible = true;
                        btnCancel.Visible = true;
                        divwithapti.Visible = true;
                        lvwithap.DataSource = ds;
                        lvwithap.DataBind();
                    }

                    else
                    {
                        //btnSave.Visible = false;
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
            ViewState["idno"] = Idno;
            int srno = int.Parse(lnk.CommandName);
            ViewState["SRNO"] = srno;
            DataSet ds = null;
            int status = Convert.ToInt32(lnk.ToolTip);
            ViewState["status"] = status;
            hdfSrnoWithDrwal.Value = Convert.ToString(srno);
            hdfIdnoWithDrwal.Value = Convert.ToString(Idno);
            ds = feecollection.GetWithApplicationDetails(Idno, srno, status);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lblStdID.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                TxtRemarkStu.Text = ds.Tables[0].Rows[0]["STU_REMARK"].ToString();
                ViewState["regno"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                lblStdName.Text = ds.Tables[0].Rows[0]["STUDFIRSTNAME"].ToString();
                ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                txtRequestDescription.Text = ds.Tables[0].Rows[0]["REASON"].ToString();
                ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["STATUS_BY_WITH_REFUND"].ToString();
                txtremark.Text = ds.Tables[0].Rows[0]["REMARK_BY_REFUND"].ToString();
                lblRequestDate.Text = ds.Tables[0].Rows[0]["STUDENT_APPLIED_DATE"].ToString();
                lblSemester.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                lblrequestedtype.Text = ds.Tables[0].Rows[0]["REQUEST_TYPE"].ToString();
                lblpaid.Text = ds.Tables[0].Rows[0]["FEEPAID"].ToString();
                lblFaculty.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                lblProgram.Text = ds.Tables[0].Rows[0]["PROGRAM"].ToString();
                lblrefund.Text = ds.Tables[0].Rows[0]["REFUND_AMOUNT"].ToString();
                lblnonrefund.Text = ds.Tables[0].Rows[0]["NONREFUND_AMOUNT"].ToString();
                TxtPreviousrefund.Text = ds.Tables[0].Rows[0]["PAIDREFUND"].ToString();
                TxtRemainingRefund.Text = ds.Tables[0].Rows[0]["REMAININGREFUND"].ToString();
                ViewState["FEEPAID"] = ds.Tables[0].Rows[0]["FEEPAID"].ToString();
                ViewState["REMAININGREFUND"] = ds.Tables[0].Rows[0]["REMAININGREFUND"].ToString();
                ViewState["PAIDREFUND"] = ds.Tables[0].Rows[0]["PAIDREFUND"].ToString();
                 ViewState["DEGREENO"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                 ViewState["BRANCHNO"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                 ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                 ViewState["AFFILIATED_NO"] = ds.Tables[0].Rows[0]["AFFILIATED_NO"].ToString();
                 ViewState["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                 ViewState["OPERATOR_APPROVAL"] = ds.Tables[0].Rows[0]["OPERATOR_APPROVAL"].ToString();
                ddlrefundstatus.SelectedValue = ds.Tables[0].Rows[0]["REFUND_WIHDRWAL_STATUS"].ToString();
                
                if (ddlrefundstatus.SelectedValue == "1")
                {
                    ddlPolicyName.Enabled = false;
                    btnCalculateRefund.Enabled = false;
                }
                else
                {
                    ddlPolicyName.Enabled = true;
                    btnCalculateRefund.Enabled = true;
                }
                if (Convert.ToInt32(ViewState["OPERATOR_APPROVAL"]) == 1)
                {
                    btnCalculateRefund.Enabled = false;
                    lblrefund.Enabled = false;
                    lblnonrefund.Enabled = false;
                }
                else
                {
                    btnCalculateRefund.Enabled = true;
                    lblrefund.Enabled = true;
                    lblnonrefund.Enabled = true;
                }
                this.objCommon.FillDropDownList(ddlPolicyName, "ACD_REFUND_POLICY_ALLOCATION PA INNER JOIN ACD_REFUND_POLICY P ON REFUNDPOLICY_ID=PA.POLICY_ID", " DISTINCT REFUNDPOLICY_ID", "REFUNDPOLICY_NAME", "REFUNDPOLICY_ID > 0 AND ISNULL(STATUS,0)=1  AND COLLEGE_ID =" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + "AND	DEGREENO=" +  Convert.ToInt32(ViewState["DEGREENO"])+ "AND	BRANCHNO=" + Convert.ToInt32(ViewState["BRANCHNO"]) +"AND	SEMESTERNO=" + Convert.ToInt32(ViewState["SEMESTERNO"]) +"AND AFFILIATED_NO="+  Convert.ToInt32(ViewState["AFFILIATED_NO"]), "REFUNDPOLICY_ID");
                ddlPolicyName.SelectedValue = ds.Tables[0].Rows[0]["POLICY_ID"].ToString();
                //if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                //{
                //    ViewState["STUDAPPLIEDDATE"] = ds.Tables[1].Rows[0]["STUDENT_APPLIED_DATE"].ToString();
                //    ViewState["TOTAL_AMT"] = ds.Tables[1].Rows[0]["TOTAL_AMT"].ToString();
                //}
                //if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                //{
                //    ViewState["days"] = ds.Tables[3].Rows[0]["NOOFDAYS"].ToString();
                //    ViewState["per"] = ds.Tables[3].Rows[0]["PERCENTAGE"].ToString();
                //    ViewState["RULEDATE"] = ds.Tables[3].Rows[0]["WITHEFFECTFROM"].ToString();
                //    decimal test = ((Convert.ToDecimal(ViewState["TOTAL_AMT"]) * Convert.ToInt32(ViewState["per"])) / 100);
                //    if (Convert.ToString(test) == "")
                //    {
                //        txtExpectedRefund.Text = "0.00";
                //    }
                //    else
                //    {
                //        txtExpectedRefund.Text = test.ToString();
                //    }
                //    if (txtExpectedRefund.Text == "0")
                //    {
                //        ddlstatus.SelectedValue = "1";

                //    }
                //    else if (Convert.ToDecimal(txtExpectedRefund.Text) > Convert.ToDecimal(25000.00))
                //    {
                //        ddlstatus.SelectedValue = "2";
                //    }
                //    else if (Convert.ToDecimal(txtExpectedRefund.Text) < Convert.ToDecimal(25000.00))
                //    {
                //        ddlstatus.SelectedValue = "3";
                //    }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Veiw_Finance", "$(document).ready(function () {$('#Veiw_Finance').modal();});", true);
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Veiw_Finance", "$(document).ready(function () {$('#Veiw_Finance').modal();});", true);
                //    txtExpectedRefund.Text = "0.00";
                //    if (txtExpectedRefund.Text == "0.00")
                //    {
                //        ddlstatus.SelectedValue = "1";
                //    }
                //    else if (Convert.ToDecimal(txtExpectedRefund.Text) > Convert.ToDecimal(25000.00))
                //    {
                //        ddlstatus.SelectedValue = "2";
                //    }
                //    else if (Convert.ToDecimal(txtExpectedRefund.Text) < Convert.ToDecimal(25000.00))
                //    {
                //        ddlstatus.SelectedValue = "3";
                //    }

                //}
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Veiw_Finance", "$(document).ready(function () {$('#Veiw_Finance').modal();});", true);
                ViewState["BlobImage"] = null;
                hdfSrnoWithDrwal.Value = null;
                hdfIdnoWithDrwal.Value = null;
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        binddata();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnSubmit1_Click(object sender, EventArgs e)
    {
        DataSet dsUserContact = null;
        string message = "";
        string subject = "";
        string status = string.Empty;
        string status1 = Convert.ToString(ddlStatus.SelectedItem);
        dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(2938));
        int ua_no = Convert.ToInt32(Session["userno"]);
        if (Convert.ToInt32(ViewState["status"]) == 3)
        {
            lblrefund.Text = "0.00";
            lblnonrefund.Text = "0.00";
        }
        else
        {
            if (lblrefund.Text == "")
            {
                objCommon.DisplayMessage(this.updmodal, "Please Calculate Refund!", this.Page);
                return;
            }
            if (lblnonrefund.Text == "")
            {
                lblnonrefund.Text = "0.00";
            }
           
        }
        CustomStatus cs = (CustomStatus)feecollection.InsertStudentWithApproval(Convert.ToInt32(ddlStatus.SelectedValue), txtremark.Text, Convert.ToDecimal(lblrefund.Text), Convert.ToDecimal(lblnonrefund.Text), Convert.ToDecimal(lblpaid.Text), Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ViewState["status"]), Convert.ToInt32(ViewState["SRNO"]), Convert.ToString(TxtRemarkStu.Text), Convert.ToInt32(ddlrefundstatus.SelectedValue), Convert.ToInt32(ddlPolicyName.SelectedValue));
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this.updmodal, "Record Saved Successfully!", this.Page);
            string uaims_constr = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
            SqlParameter[] objParams = new SqlParameter[4];
            objParams[0] = new SqlParameter("@P_EMAILID", "");
            objParams[1] = new SqlParameter("@P_MOBILENO", "");
            objParams[2] = new SqlParameter("@P_USERNAME", "");
            objParams[3] = new SqlParameter("@P_PAGENO", (2938));
            dsUserContact = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_EMAIL_AND_USER_DETAILS", objParams);

            subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
            message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
            MailMessage msgsPara = new MailMessage();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(message);
            msgsPara.BodyEncoding = Encoding.UTF8;
            msgsPara.Body = Convert.ToString(sb);
            msgsPara.Body = msgsPara.Body.Replace("[USERFIRSTNAME]", ViewState["name"].ToString());
            msgsPara.Body = msgsPara.Body.Replace("[USERNAME]", ViewState["regno"].ToString());
            Task<string> task = Email.Execute(msgsPara.Body, ViewState["EMAILID"].ToString(), subject, null, "");
            //subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
            //message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
            //Executeadmin(message, Convert.ToString(ViewState["EMAILID"]), subject, Convert.ToString(ViewState["name"]), Convert.ToString(ViewState["regno"]), status1, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
            clear();
        }
        else
        {
            objCommon.DisplayMessage(this.updmodal, "Error!!", this.Page);
        }
    }
    static async Task Executeadmin(string message, string toSendAddress, string Subject, string firstname, string username, string matter, string sendemail, string emailpass)
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
        //Response.Redirect(Request.Url.ToString());
        txtremark.Text = "";
        TxtRemarkStu.Text = "";
        ddlPolicyName.SelectedIndex = 0;
        ddlrefundstatus.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        rdbFilter.ClearSelection();
        ddlWithdrawalType.SelectedIndex = 0;
        divwithapti.Visible = false;
        lvwithap.DataSource=null;
        lvwithap.DataBind();
    }
    protected void btnCalculateRefund_Click(object sender, EventArgs e)
    {
        DataSet ds = null;
        ds = objdocContr.GetCalculateData(Convert.ToString(ViewState["regno"]), Convert.ToInt32(ViewState["status"]), Convert.ToInt32(ViewState["SRNO"]), Convert.ToInt32(ddlPolicyName.SelectedValue));
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["STUDAPPLIEDDATE"] = ds.Tables[0].Rows[0]["STUDENT_APPLIED_DATE"].ToString();
            ViewState["TOTAL_AMT"] = ds.Tables[0].Rows[0]["TOTAL_AMT"].ToString();

            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                ViewState["RULEDATE"] = ds.Tables[1].Rows[0]["WITHEFFECTFROM"].ToString();
            }
            else
            {

                lblrefund.Text = "0.00";
                lblnonrefund.Text = "0.00";
                objCommon.DisplayMessage(this.Page, "Refund Policy Does Not Map !!!", this.Page);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Veiw_Finance", "$(document).ready(function () {$('#Veiw_Finance').modal();});", true);
            }
           if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
            {
                ViewState["days"] = ds.Tables[2].Rows[0]["NOOFDAYS"].ToString();
                ViewState["per"] = ds.Tables[2].Rows[0]["PERCENTAGE"].ToString();
                DateTime dt1 = DateTime.Parse(Convert.ToDateTime(ViewState["RULEDATE"]).ToString("dd/MM/yyyy"));
                DateTime dt2 = DateTime.Parse(Convert.ToDateTime(ViewState["STUDAPPLIEDDATE"]).ToString("dd/MM/yyyy"));
                TimeSpan ts = dt2.Subtract(dt1);
                int days = ts.Days;
                decimal test = 0.00m;
                if (dt1 > dt2)
                {
                    if (Convert.ToString(ViewState["PAIDREFUND"]) == "")
                    {
                        test = ((Convert.ToDecimal(ViewState["TOTAL_AMT"])));
                    }
                    else
                    {
                        decimal amount = ((Convert.ToDecimal(ViewState["FEEPAID"]) - Convert.ToDecimal(ViewState["PAIDREFUND"])));
                        test = (Convert.ToDecimal(amount));
                    }
                }
                else
                {

                    if (Convert.ToString(ViewState["PAIDREFUND"]) == "")
                    {
                        test = ((Convert.ToDecimal(ViewState["TOTAL_AMT"]) * Convert.ToInt32(ViewState["per"])) / 100);
                    }
                    else
                    {
                        decimal amount = ((Convert.ToDecimal(ViewState["FEEPAID"]) - Convert.ToDecimal(ViewState["PAIDREFUND"])));
                        test = ((Convert.ToDecimal(amount) * Convert.ToInt32(ViewState["per"])) / 100);
                    }
                }
               
                string formattedMoneyValue = String.Format("{0:C}", test);
                lblrefund.Text = test.ToString();
                if (Convert.ToString(ViewState["PAIDREFUND"]) == "")
                {

                    lblnonrefund.Text = Convert.ToString(Convert.ToDecimal(ViewState["TOTAL_AMT"]) - Convert.ToDecimal(test));
                }
                else
                {
                    decimal amount = ((Convert.ToDecimal(ViewState["FEEPAID"]) - Convert.ToDecimal(ViewState["PAIDREFUND"])));
                    lblnonrefund.Text = Convert.ToString(Convert.ToDecimal(amount) - Convert.ToDecimal(test));
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Veiw_Finance", "$(document).ready(function () {$('#Veiw_Finance').modal();});", true);
            }
           else
           {

               lblrefund.Text = "0.00";
               lblnonrefund.Text = "0.00";
               objCommon.DisplayMessage(this.Page, "No Of Days of Policy Does Not Define !!!", this.Page);
               ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Veiw_Finance", "$(document).ready(function () {$('#Veiw_Finance').modal();});", true);
           }

        }
        else
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
                string extension = Path.GetExtension(img.ToString());
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

                if (extension == ".pdf")
                {

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
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
                    ViewState["filePath_Show"] = filePath;
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                    string Base64String = Convert.ToBase64String(bytes);
                    Session["Base64String"] = Base64String;
                    Session["Base64StringType"] = "application/pdf";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
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
            else
            {
                objCommon.DisplayMessage(this.Page, "Sorry, File not found !!!", this.Page);
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlrefundstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlrefundstatus.SelectedValue == "1")
        {
            btnCalculateRefund.Enabled = false;
            ddlPolicyName.Enabled = false;
            lblrefund.Text = "0.00";
            lblnonrefund.Text = "0.00";
        }
        else
        {
            btnCalculateRefund.Enabled = true;
            ddlPolicyName.Enabled = true;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Veiw_Finance", "$(document).ready(function () {$('#Veiw_Finance').modal();});", true);
    }
    protected void ddlPolicyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblrefund.Text = "";
        lblnonrefund.Text = "";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Veiw_Finance", "$(document).ready(function () {$('#Veiw_Finance').modal();});", true);
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