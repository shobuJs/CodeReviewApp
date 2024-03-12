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

public partial class EXAMINATION_Projects_Withdrawal_Approval : System.Web.UI.Page
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
                    this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    string collge = objCommon.LookUp("USER_ACC", "UA_COLLEGE_NOS", "UA_NO=" + Session["userno"]);
                    objCommon.FillDropDownList(ddlfaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + collge + ")", "");
                    objCommon.FillDropDownList(ddlfacultymana, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + collge + ")", "");
                    objCommon.FillDropDownList(ddlfacultyDire, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + collge + ")", "");
                    this.objCommon.FillDropDownList(ddlPolicyName, "ACD_REFUND_POLICY", "REFUNDPOLICY_ID", "REFUNDPOLICY_NAME", "REFUNDPOLICY_ID > 0 AND ISNULL(STATUS,0)=1", "REFUNDPOLICY_ID");
                    this.objCommon.FillDropDownList(ddltypeop, "ACD_REFUND_REQUEST_TYPE", "SRNO", "REQUEST_TYPE", "SRNO NOT IN (10)", "");
                    this.objCommon.FillDropDownList(ddltypemana, "ACD_REFUND_REQUEST_TYPE", "SRNO", "REQUEST_TYPE", "SRNO NOT IN (10)", "");
                    this.objCommon.FillDropDownList(ddltypedire, "ACD_REFUND_REQUEST_TYPE", "SRNO", "REQUEST_TYPE", "SRNO NOT IN (10)", "");
                    if (Session["usertype"].ToString().Equals("7"))     //Student 
                    {
                        divoperator.Visible = true;
                    }
                    else
                    {
                        string type = Convert.ToString(objCommon.LookUp("USER_ACC", "ISNULL(UA_ROLENO,0)", "UA_ROLENO IS NOT NULL AND UA_NO=" + Session["userno"]));
                        string[] Array = type.Split(',');
                        for (int i = 0; i < Array.Length; i++)
                        {
                            if (Array[i] == "4")
                            {
                                ViewState["USER_ROLE"] = "4";
                                break;
                            }
                            if (Array[i] == "5")
                            {
                                ViewState["USER_ROLE"] = "5";
                                break;
                            }
                            if (Array[i] == "6")
                            {
                                ViewState["USER_ROLE"] = "6";
                                break;
                            }
                        }
                        if (Convert.ToString(ViewState["USER_ROLE"]) == string.Empty || Convert.ToString(ViewState["USER_ROLE"]) == null)
                        {
                            objCommon.DisplayMessage(this.Page, "You Are Not Authorized To use this Page.", this.Page);
                            return;
                        }
                        else
                        {
                            //int directorype = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_ROLENO", "UA_ROLENO='4'"));
                            if (Request.QueryString["pageno"] != null)
                            {
                            }
                            //if (Session["usertype"].ToString().Equals("7"))     //Student 
                            //{
                            //    divoperator.Visible = true;
                            //}
                            //else if (Session["usertype"].ToString().Equals("1"))
                            //{
                            //    manager.Visible = true;
                            //}
                            //else if(Session["usertype"].ToString().Equals("11"))
                            //{
                            //    divdirector.Visible = true;
                            //}

                            if (ViewState["USER_ROLE"].ToString().Equals("5") || ViewState["USER_ROLE"].ToString().Equals("6"))
                            {
                                manager.Visible = true;
                            }
                            else if (ViewState["USER_ROLE"].ToString().Equals("4"))
                            {
                                divdirector.Visible = true;
                            }
                        }
                    }
                    //else
                    //{
                    //    objCommon.DisplayUserMessage(updwithapp, "Record Not Found.This Page is use for only Student Login!!", this.Page);
                    //}
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
                Response.Redirect("~/notauthorized.aspx?page=Withdrawal_Approval.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Withdrawal_Approval.aspx");
        }
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
        catch (Exception ex)
        {
        }
    }
    private void bindPriviousRefund()
    {
        try
        {

            string SP_Name2 = "PKG_ACD_GET_REFUND_AMOUNT_LIST";
            string SP_Parameters2 = "@P_IDNO,@P_REQUEST_TYPE";
            string Call_Values2 = "" + Convert.ToInt32(ViewState["idno"]) + "," + Convert.ToInt32(ViewState["request_type"]) + "";
            DataSet dsFacWiseCourseList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (dsFacWiseCourseList.Tables[0].Rows.Count > 0)
            {
                DivPayment.Visible = true;
                LvPayment.DataSource = dsFacWiseCourseList;
                LvPayment.DataBind();
            }
            else
            {
                DivPayment.Visible = false;
                LvPayment.DataSource = null;
                LvPayment.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void bindPriviousRefundmana()
    {
        try
        {

            string SP_Name2 = "PKG_ACD_GET_REFUND_AMOUNT_LIST";
            string SP_Parameters2 = "@P_IDNO,@P_REQUEST_TYPE";
            string Call_Values2 = "" + Convert.ToInt32(ViewState["idno"]) + "," + Convert.ToInt32(ViewState["request_type"]) + "";
            DataSet dsFacWiseCourseList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (dsFacWiseCourseList.Tables[0].Rows.Count > 0)
            {
                divpayma.Visible = true;
                Lvpayma.DataSource = dsFacWiseCourseList;
                Lvpayma.DataBind();
            }
            else
            {
                divpayma.Visible = false;
                Lvpayma.DataSource = null;
                Lvpayma.DataBind();
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
    private void bindrefundamountdetailsdirec()
    {
        try
        {

            DataSet ds = stud.GetRefundAmountDetails(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ViewState["SRNO"]),Convert.ToInt32(ViewState["request_type"]));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                divrefunddire.Visible = true;
                lvrefunddire.DataSource = ds;
                lvrefunddire.DataBind();
            }
            else
            {
                divrefunddire.Visible = true;
                lvrefunddire.DataSource = ds;
                lvrefunddire.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void bindrefundamountdetailsMana()
    {
        try
        {

            DataSet ds = stud.GetRefundAmountDetails(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ViewState["srno"]), Convert.ToInt32(ViewState["request_type"]));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                divmanref.Visible = true;
                lvrefundmana.DataSource = ds;
                lvrefundmana.DataBind();
            }
            else
            {
                divmanref.Visible = true;
                lvrefundmana.DataSource = ds;
                lvrefundmana.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }
    
    protected void btnshow_Click(object sender, EventArgs e)
    {
        binddataopdirector();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnSave_Click(object sender, EventArgs e)
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
                string status1 = "";
                int idno = 0;
                CheckBox chkBox = dataitem.FindControl("chktransfer") as CheckBox;
                DropDownList dstatus = dataitem.FindControl("ddlstatus") as DropDownList;

                if (chkBox.Checked == true)
                {
                    idno = Convert.ToInt32(chkBox.ToolTip);
                    status = dstatus.Text;
                    status1 = Convert.ToString(dstatus.SelectedItem);
                    cs = (CustomStatus)stud.Updwith(idno, status);
                    subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                    message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                    Execute(message, Convert.ToString(ViewState["EMAILID"]), subject, Convert.ToString(ViewState["name"]), Convert.ToString(ViewState["regno"]), status1, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();

                }
            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                //BINDLISRVIEW();              
                objCommon.DisplayMessage(this.Page, "Record saved successfully.", this.Page);
                btnSave.Visible = false;
                lvwithap.DataSource = null;
                lvwithap.DataBind();
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
    static async Task Execute(string message, string toSendAddress, string Subject, string firstname, string username, string matter,string sendemail, string emailpass)
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
    
    protected void lnkViewDoc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            int Idno = int.Parse(lnk.CommandArgument);
            ViewState["idno"]=Idno;
            int srno = int.Parse(lnk.CommandName);
            ViewState["SRNO"] = srno;
            string username = lnk.ToolTip;
            DataSet ds = null;
            hdfSrnoWithDrwal.Value = Convert.ToString(srno);
            hdfIdnoWithDrwal.Value = Convert.ToString(Idno);
            string status = objCommon.LookUp("ACD_STUDENT_PROGRAM_CANCEL", "STATUS_NO", "IDNO=" + Idno + "AND SRNO=" + srno);            
            ds = objdocContr.GetAllWidrawlStudentDetails(Idno, srno, 0, "0", "0", 1, 4, username);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
               
                TxtRemarkstuddire.Text = ds.Tables[0].Rows[0]["STUDENT_REMARK_BY_O_M_D_F"].ToString();
                TxtTotalredire.Text = ds.Tables[0].Rows[0]["TOTAL_REFUND_AMOUNT"].ToString();
                lblStdID.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                ViewState["regno"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                lblStdName.Text = ds.Tables[0].Rows[0]["STUDFIRSTNAME"].ToString();
                ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                lblrefunddire.Text = ds.Tables[0].Rows[0]["REFUND_AMOUNT"].ToString();
                nonlblrefunddire.Text = ds.Tables[0].Rows[0]["NONREFUND_AMOUNT"].ToString();
                txtdfdef.Text = ds.Tables[0].Rows[0]["REASON"].ToString();
                ddlstabydirector.SelectedValue = ds.Tables[0].Rows[0]["DIRECTOR_APPROVAL"].ToString();
                txtsasdf.Text = ds.Tables[0].Rows[0]["REMARK_BY_DIRECTOR"].ToString();
                fbfgb.Text = ds.Tables[0].Rows[0]["STUDENT_APPLIED_DATE"].ToString();
                ViewState["FINANCE_APPROVAL"] = ds.Tables[0].Rows[0]["FINANCE_APPROVAL"].ToString();
                //ViewState["STUDAPPLIEDDATE"] = ds.Tables[1].Rows[0]["STUDENT_APPLIED_DATE"].ToString();
                //ViewState["TOTAL_AMT"] = ds.Tables[1].Rows[0]["TOTAL_AMT"].ToString();
                //ViewState["days"] = ds.Tables[3].Rows[0]["NOOFDAYS"].ToString();
                //ViewState["per"] = ds.Tables[3].Rows[0]["PERCENTAGE"].ToString();
                //ViewState["RULEDATE"] = ds.Tables[3].Rows[0]["WITHEFFECTFROM"].ToString();
                lblBankName.Text = ds.Tables[0].Rows[0]["BANKNAME"].ToString();
                lblBranchName.Text = ds.Tables[0].Rows[0]["BANKBRANCH"].ToString();
                lblAccountNumber.Text = ds.Tables[0].Rows[0]["ACCOUNTNO"].ToString();
                lblIFSCCode.Text = ds.Tables[0].Rows[0]["BRANCHCODE"].ToString();
                ViewState["request_type"] = ds.Tables[0].Rows[0]["STATUS_NO"].ToString();
                bindrefundamountdetailsdirec();
                bindPriviousRefundDire();
                ds = objdocContr.GetExcessStudentDetails(Idno, srno);
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtExpectedRefund.Text = ds.Tables[0].Rows[0]["REFUND_AMOUNT"].ToString();
                    txtFeesPaid.Text = ds.Tables[0].Rows[0]["FEEPAID"].ToString();
                    txtFeesBalance.Text = ds.Tables[0].Rows[0]["BALANCE"].ToString();
                }
                else
                {
                    txtFeesPaid.Text = ds.Tables[0].Rows[0]["FEEPAID"].ToString();
                    txtFeesBalance.Text = ds.Tables[0].Rows[0]["BALANCE"].ToString();
                    txtExpectedRefund.Text = ds.Tables[0].Rows[0]["REFUND_AMOUNT"].ToString();
                }
                ddlstatus.SelectedValue = "3";
                if (Convert.ToInt32(ViewState["FINANCE_APPROVAL"]) == 1)
                {
                    txtsasdf.Enabled = false;
                    TxtTotalredire.Enabled = false;
                    TxtRemarkstuddire.Enabled = false;
                    ddlstabydirector.Enabled = false;
                }
                else
                {
                    txtsasdf.Enabled = true;
                    TxtTotalredire.Enabled = true;
                    TxtRemarkstuddire.Enabled = true;
                    ddlstabydirector.Enabled = true;
                }

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
                //    ddlstatus.SelectedValue = "3";
                //    if (Convert.ToString(test) == "")
                //    {
                //        txtExpectedRefund.Text = "0.00";
                //    }
                //    else
                //    {
                //        txtExpectedRefund.Text = test.ToString();
                //    }
                //if (txtExpectedRefund.Text == "0")
                //{
                //    ddlstatus.SelectedValue = "1";

                //}
                //else if (Convert.ToDecimal(txtExpectedRefund.Text) > Convert.ToDecimal(25000))
                //{
                //    ddlstatus.SelectedValue = "2";
                //}
                //else if (Convert.ToDecimal(txtExpectedRefund.Text) < Convert.ToDecimal(25000))
                //{
                //    ddlstatus.SelectedValue = "3";
                //}
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Veiw_Finance", "$(document).ready(function () {$('#Veiw_Finance').modal();});", true);
                //}
                //else
                //{
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Veiw_Finance", "$(document).ready(function () {$('#Veiw_Finance').modal();});", true);
                //    txtExpectedRefund.Text = "0.00";
                //    //if (txtExpectedRefund.Text == "0.00")
                //    //{
                //    //    ddlstatus.SelectedValue = "1";
                //    //}
                //    //else if (Convert.ToDecimal(txtExpectedRefund.Text) > Convert.ToDecimal(25000.00))
                //    //{
                //    //    ddlstatus.SelectedValue = "2";
                //    //}
                //    //else if (Convert.ToDecimal(txtExpectedRefund.Text) < Convert.ToDecimal(25000.00))
                //    //{
                //    //    ddlstatus.SelectedValue = "3";
                //    //}
                   
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
    protected void btnSubmit1_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            CustomStatus cs = CustomStatus.Others;
            DataSet dsUserContact = null;
            string message = "";
            string subject = "";
            string status = string.Empty;
            string status1 = "Approved";
            dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));

            int ua_no = Convert.ToInt32(Session["userno"]);
            if (lblexpectedrefmana.Text == "")
            {
                lblexpectedrefmana.Text = "0";
            }
            foreach (ListViewDataItem dataitem in lvrefunddire.Items)
            {
                CheckBox chkBox = dataitem.FindControl("chktransfer") as CheckBox;
                TextBox txtrefundamt = dataitem.FindControl("txtrefundamt") as TextBox;
                HiddenField hdfdcr = dataitem.FindControl("hdfdcr") as HiddenField;
                if (chkBox.Checked == true && chkBox.Enabled == false)
                {
                    count++;
                    decimal refundamt = 0.0m;
                    if (txtrefundamt.Text == "" && txtrefundamt.Text == "")
                    {
                        objCommon.DisplayMessage(this.updmodal, "Please Enter Refund Amount", this.Page);
                        return;
                    }
                    refundamt = Convert.ToDecimal(txtrefundamt.Text);
                    cs = (CustomStatus)stud.UpdDetailsByDirector(Convert.ToInt32(ViewState["idno"]), txtsasdf.Text, Convert.ToInt32(ddlstabydirector.SelectedValue), Convert.ToInt32(ViewState["SRNO"]), Convert.ToInt32(refundamt), Convert.ToDecimal(TxtTotalredire.Text), Convert.ToString(TxtRemarkstuddire.Text), Convert.ToString(hdfdcr.Value));
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(this.updmodal, "Please Select At least One checkbox", this.Page);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "view_operator", "$(document).ready(function () {$('#view_operator').modal();});", true);
            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updmodal, "Record Saved Successfully!", this.Page);
                subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                Execute(message, Convert.ToString(ViewState["EMAILID"]), subject, Convert.ToString(ViewState["name"]), Convert.ToString(ViewState["regno"]), status1, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
                rdbFilter.ClearSelection();
                ddltypedire.SelectedIndex = 0;
                divwithapti.Visible = false;
                lvwithap.DataSource = null;
                lvwithap.DataBind();
                //clear();         
            }
            else
            {
               // objCommon.DisplayMessage(this.updmodal, "Please Select At least One checkbox", this.Page);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "view_operator", "$(document).ready(function () {$('#view_operator').modal();});", true);

            }

        }
        catch (Exception ex)
        {
        }
    }
    private void clear()
    {
        txtsasdf.Text = "";
        txtdfdef.Text = "";
        txtFeesPaid.Text = "";
        txtFeesBalance.Text = "";
        txtExpectedRefund.Text = "";
        btnSave.Visible = false;
        rdbFilter.ClearSelection();
        divwithapti.Visible = false;
        lvwithap.DataSource = null;
        lvwithap.DataBind();
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        rdbFilter.ClearSelection();
        ddltypedire.SelectedIndex = 0;
        divwithapti.Visible = false;
        lvwithap.DataSource = null;
        lvwithap.DataBind();
    }
    //protected void txtExpectedRefund_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtExpectedRefund.Text == "25000.00")
    //    {
            
    //        ddlstatus.SelectedValue = "1";
    //    }
    //    //else if (txtExpectedRefund.Text  "0")
    //    //{
    //    //}
    //    //else if (txtExpectedRefund.Text == "0")
    //    //{
    //    //}
    //}
    private void binddataopdirector()
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
            ds = stud.getwithapprovaldataByDirector(1, date[1], date[0], Convert.ToInt32(ddltypedire.SelectedValue),Convert.ToInt32(ddlfacultyDire.SelectedValue));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                ViewState["name"] = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                ViewState["regno"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                ViewState["EMAILID"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();

                
                btnCancel.Visible = true;
                divwithapti.Visible = true;
                lvwithap.DataSource = ds;
                lvwithap.DataBind();
            }

            else
            {
                btnCancel.Visible = true;
                hdnDate.Value = null;
                lvwithap.DataSource = null;
                lvwithap.DataBind();
                objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
            }
            foreach (ListViewDataItem lvitem in lvwithap.Items)
            {
                Label labelstatus = lvitem.FindControl("labelstatus") as Label;
                LinkButton lnkstuddoc = lvitem.FindControl("lnkstuddocdire") as LinkButton;
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
            ds = stud.getwithapprovaldataByDirector(2, date[1], date[0], Convert.ToInt32(ddltypedire.SelectedValue), Convert.ToInt32(ddlfacultyDire.SelectedValue));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                ViewState["name"] = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                ViewState["regno"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                ViewState["EMAILID"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                //btnSave.Visible = true;
                //btnCancel.Visible = true;
                divwithapti.Visible = true;
                lvwithap.DataSource = ds;
                lvwithap.DataBind();
            }

            else
            {
                btnCancel.Visible = true;
                hdnDate.Value = null;
                lvwithap.DataSource = null;
                lvwithap.DataBind();
                objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
            }
            foreach (ListViewDataItem lvitem in lvwithap.Items)
            {
                Label labelstatus = lvitem.FindControl("labelstatus") as Label;
                LinkButton lnkstuddoc = lvitem.FindControl("lnkstuddocdire") as LinkButton;
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
            ds = stud.getwithapprovaldataByDirector(3, date[1], date[0], Convert.ToInt32(ddltypedire.SelectedValue), Convert.ToInt32(ddlfacultyDire.SelectedValue));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                ViewState["name"] = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                ViewState["regno"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                ViewState["EMAILID"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                //btnSave.Visible = true;
                btnCancel.Visible = true;
                divwithapti.Visible = true;
                lvwithap.DataSource = ds;
                lvwithap.DataBind();
            }

            else
            {
                btnCancel.Visible = true;
                hdnDate.Value = null;
                lvwithap.DataSource = null;
                lvwithap.DataBind();
                objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
            }
            foreach (ListViewDataItem lvitem in lvwithap.Items)
            {
                Label labelstatus = lvitem.FindControl("labelstatus") as Label;
                LinkButton lnkstuddoc = lvitem.FindControl("lnkstuddocdire") as LinkButton;
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

                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);

                    //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
                    //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    //embed += "</object>";
                    //ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                    ViewState["filePath_Show"] = filePath;
                   // Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
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
    protected void lnkstuddocmana_Click(object sender, EventArgs e)
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
                    //ltEmbedmana.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "manamodal", "$(document).ready(function () {$('#manamodal').modal();});", true);
                    ViewState["filePath_Show"] = filePath;
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                    string Base64String = Convert.ToBase64String(bytes);
                    Session["Base64String"] = Base64String;
                    Session["Base64StringType"] = "application/pdf";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "manamodal", "$(document).ready(function () {$('#manamodal').modal();});", true);


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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "manamodal", "$(document).ready(function () {$('#manamodal').modal();});", true);
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
    protected void lnkstuddocdire_Click(object sender, EventArgs e)
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
                    //Literal1.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "diremodal", "$(document).ready(function () {$('#diremodal').modal();});", true);
                    ViewState["filePath_Show"] = filePath;
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                    string Base64String = Convert.ToBase64String(bytes);
                    Session["Base64String"] = Base64String;
                    Session["Base64StringType"] = "application/pdf";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "diremodal", "$(document).ready(function () {$('#diremodal').modal();});", true);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "diremodal", "$(document).ready(function () {$('#diremodal').modal();});", true);
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
   
    static async Task Executedirector(string message, string toSendAddress, string Subject, string firstname, string username, string matter, string sendemail, string emailpass)
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
    protected void cancelop_Click(object sender, EventArgs e)
    {

    }
    protected void btnshowope_Click(object sender, EventArgs e)
    {
        binddataop();
    }
  
    protected void btnsubop_Click(object sender, EventArgs e)
    {
        try
        {
            CustomStatus cs = CustomStatus.Others;

            if (txtAmountRefund.Text == "")
            {
                txtAmountRefund.Text = "0";
            }
            foreach (ListViewDataItem dataitem in lvrefunddetails.Items)
            {
                CheckBox chkBox = dataitem.FindControl("chktransfer") as CheckBox;
                TextBox txtrefundamt = dataitem.FindControl("txtrefundamt") as TextBox;
                HiddenField hdfdcr = dataitem.FindControl("hdfdcr") as HiddenField;
                if (chkBox.Checked == false)
                    continue;

                decimal refundamt = 0.0m;
                if (txtrefundamt.Text == "" && txtrefundamt.Text == "")
                {
                    objCommon.DisplayMessage(this.updmodal, "Please Enter Refund Amount", this.Page);
                    return;
                }
                refundamt = Convert.ToDecimal(txtrefundamt.Text);
                cs = (CustomStatus)stud.UpdDetailsByOperator(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ViewState["srno"]), Convert.ToString(txtremarkop.Text), Convert.ToInt32(refundamt), Convert.ToDecimal(txtAmountRefund.Text), Convert.ToString(TxtreStudent.Text), Convert.ToInt32(ddlPolicyName.SelectedValue), Convert.ToString(hdfdcr.Value));
            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updmodal, "Record Saved Successfully!", this.Page);
                rdooperator.ClearSelection();
                TxtreStudent.Text="";
                txtAmountRefund.Text = "";
                ddltypeop.SelectedIndex = 0;
                divop.Visible = false;
                lvop.DataSource = null;
                lvop.DataBind();
               
            }
            else
            {
                //objCommon.DisplayMessage(this.updmodal, "Please Select At least One checkbox", this.Page);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "view_operator", "$(document).ready(function () {$('#view_operator').modal();});", true);

            }
        }
        catch (Exception ex)
        {

        }

    }
    protected void btncancelop_Click(object sender, EventArgs e)
    {
        rdooperator.ClearSelection();
        ddltypeop.SelectedIndex = 0;
        divop.Visible = false;
        lvop.DataSource = null;
        lvop.DataBind();
    }
    private void binddataop()
    {
        if (rdooperator.SelectedValue == "1")
        {
            DataSet ds = null; string[] date;

            if (Convert.ToString(dateoprator.Value) == string.Empty || Convert.ToString(dateoprator.Value) == "0")
            {
                date = "0,0".Split(',');
            }
            else
            {
                date = dateoprator.Value.ToString().Split('-');
                date[0] = Convert.ToDateTime(date[0]).ToString("dd/MM/yyyy");
                date[1] = Convert.ToDateTime(date[1]).ToString("dd/MM/yyyy");
            }
            ds = stud.getwithapprovaldataByOperator(1, date[1], date[0],Convert.ToInt32(ddltypeop.SelectedValue),Convert.ToInt32(ddlfaculty.SelectedValue));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                ViewState["name"] = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                ViewState["regno"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                ViewState["EMAILID"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();

                //btnSave.Visible = true;
                btncancelop.Visible = true;
                divop.Visible = true;
                lvop.DataSource = ds;
                lvop.DataBind();
            }


            else
            {
                dateoprator.Value = null;
                btncancelop.Visible = true;
                lvop.DataSource = null;
                lvop.DataBind();
                objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
            }
            foreach (ListViewDataItem lvitem in lvop.Items)
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
        else if (rdooperator.SelectedValue == "2")
        {
            DataSet ds = null; string[] date;

            if (Convert.ToString(dateoprator.Value) == string.Empty || Convert.ToString(dateoprator.Value) == "0")
            {
                date = "0,0".Split(',');
            }
            else
            {

                date = dateoprator.Value.ToString().Split('-');
                date[0] = Convert.ToDateTime(date[0]).ToString("dd/MM/yyyy");
                date[1] = Convert.ToDateTime(date[1]).ToString("dd/MM/yyyy");
            }
            ds = stud.getwithapprovaldataByOperator(2, date[1], date[0], Convert.ToInt32(ddltypeop.SelectedValue), Convert.ToInt32(ddlfaculty.SelectedValue));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                ViewState["name"] = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                ViewState["regno"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                ViewState["EMAILID"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                //btnSave.Visible = true;
                //btnCancel.Visible = true;
                divop.Visible = true;
                lvop.DataSource = ds;
                lvop.DataBind();
            }

            else
            {
                dateoprator.Value = null;
                btncancelop.Visible = true;
                lvop.DataSource = null;
                lvop.DataBind();
                objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
            }
            foreach (ListViewDataItem lvitem in lvop.Items)
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
        else if (rdooperator.SelectedValue == "3")
        {
            DataSet ds = null; string[] date;

            if (Convert.ToString(dateoprator.Value) == string.Empty || Convert.ToString(dateoprator.Value) == "0")
            {
                date = "0,0".Split(',');
            }
            else
            {
                date = dateoprator.Value.ToString().Split('-');
                date[0] = Convert.ToDateTime(date[0]).ToString("dd/MM/yyyy");
                date[1] = Convert.ToDateTime(date[1]).ToString("dd/MM/yyyy");
            }
            ds = stud.getwithapprovaldataByOperator(3, date[1], date[0], Convert.ToInt32(ddltypeop.SelectedValue), Convert.ToInt32(ddlfaculty.SelectedValue));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                ViewState["name"] = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                ViewState["regno"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                ViewState["EMAILID"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                //btnSave.Visible = true;
                btncancelop.Visible = true;
                divop.Visible = true;
                lvop.DataSource = ds;
                lvop.DataBind();
            }

            else
            {
                dateoprator.Value = null;
                btncancelop.Visible = true;
                lvop.DataSource = null;
                lvop.DataBind();
                objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
            }
            foreach (ListViewDataItem lvitem in lvop.Items)
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
    protected void lnkViewDocOp_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            int Idno = int.Parse(lnk.CommandArgument);
            ViewState["idno"] = Idno;
            int srno = int.Parse(lnk.CommandName);
            ViewState["srno"] = srno;
            string username = lnk.ToolTip;
            DataSet ds = null;
            hdfsrnoop.Value = Convert.ToString(srno);
            hdfidnoop.Value = Convert.ToString(Idno);
            string status = objCommon.LookUp("ACD_STUDENT_PROGRAM_CANCEL", "STATUS_NO", "IDNO=" + Idno + "AND SRNO=" + srno);
           
            ds = objdocContr.GetAllWidrawlStudentDetails(Idno, srno, 0, "0", "0", 1, 4, username);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                    ddlstatusop.SelectedValue = "1";
                    TxtreStudent.Text =    ds.Tables[0].Rows[0]["STUDENT_REMARK_BY_O_M_D_F"].ToString();
                    txtAmountRefund.Text = ds.Tables[0].Rows[0]["TOTAL_REFUND_AMOUNT"].ToString();
                    refundamount.Text = ds.Tables[0].Rows[0]["REFUND_AMOUNT"].ToString();
                    nonrefundamount.Text = ds.Tables[0].Rows[0]["NONREFUND_AMOUNT"].ToString();
                    lblappop.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                    ViewState["regno"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                    lblappname.Text = ds.Tables[0].Rows[0]["STUDFIRSTNAME"].ToString();
                    ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                    ViewState["FINANCE_APPROVAL"] = ds.Tables[0].Rows[0]["FINANCE_APPROVAL"].ToString();
                    txtdfdef.Text = ds.Tables[0].Rows[0]["REASON"].ToString();
                    txtremarkop.Text = ds.Tables[0].Rows[0]["OPERATOR_REMARK"].ToString();
                    txtsasdf.Text = ds.Tables[0].Rows[0]["REMARK_BY_ADMIN"].ToString();
                    lbldateop.Text = ds.Tables[0].Rows[0]["STUDENT_APPLIED_DATE"].ToString();
                    lblbanknameop.Text = ds.Tables[0].Rows[0]["BANKNAME"].ToString();
                    lblbranchop.Text = ds.Tables[0].Rows[0]["BANKBRANCH"].ToString();
                    lblaccnuop.Text = ds.Tables[0].Rows[0]["ACCOUNTNO"].ToString();
                    lblcodeop.Text = ds.Tables[0].Rows[0]["BRANCHCODE"].ToString();
                    ViewState["request_type"] = ds.Tables[0].Rows[0]["STATUS_NO"].ToString();
                    if (status == "10")
                    {
                        ds = objdocContr.GetExcessStudentDetails(Idno, srno);
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {
                            txtrefundop.Text = ds.Tables[0].Rows[0]["REFUND_AMOUNT"].ToString();
                            txtfeesop.Text = ds.Tables[0].Rows[0]["FEEPAID"].ToString();
                            txtbalanceop.Text = ds.Tables[0].Rows[0]["BALANCE"].ToString();
                            if (Convert.ToString(txtrefundop.Text) == "")
                            {
                                txtrefundop.Text = "0.00";
                            }
                            else
                            {
                                txtrefundop.Text = txtrefundop.Text = ds.Tables[0].Rows[0]["REFUND_AMOUNT"].ToString();
                            }
                        }
                    }
                    else
                    {

                    txtrefundop.Text = ds.Tables[0].Rows[0]["REFUND_AMOUNT"].ToString();
                    txtfeesop.Text = ds.Tables[0].Rows[0]["FEEPAID"].ToString();
                    txtbalanceop.Text = ds.Tables[0].Rows[0]["BALANCE"].ToString();
                    ddlstatusop.SelectedValue = "1";
                    if (Convert.ToString(txtrefundop.Text) == "")
                    {
                        txtrefundop.Text = "0.00";
                    }
                    else
                    {
                        txtrefundop.Text = txtrefundop.Text = ds.Tables[0].Rows[0]["REFUND_AMOUNT"].ToString();
                    }
                    
                }
                    bindrefundamountdetails();
                    bindPriviousRefund();
                    if (Convert.ToInt32(ViewState["FINANCE_APPROVAL"]) == 1)
                    {
                        txtremarkop.Enabled=false;
                        txtAmountRefund.Enabled=false;
                        TxtreStudent.Enabled = false;
                    }
                    else
                    {
                        txtremarkop.Enabled = true;
                        txtAmountRefund.Enabled = true;
                        TxtreStudent.Enabled = true;
                    }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "view_operator", "$(document).ready(function () {$('#view_operator').modal();});", true);
                
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
                //    ddlstatusop.SelectedValue = "1";
                //    if (Convert.ToString(test) == "")
                //    {
                //        txtrefundop.Text = "0.00";
                //    }
                //    else
                //    {
                //        txtrefundop.Text = test.ToString();
                //    }
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "view_operator", "$(document).ready(function () {$('#view_operator').modal();});", true);
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "view_operator", "$(document).ready(function () {$('#view_operator').modal();});", true);
                //    txtrefundop.Text = "0.00";
                //    ddlstatusop.SelectedValue = "1";
                //}
               
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "view_operator", "$(document).ready(function () {$('#view_operator').modal();});", true);
                ViewState["BlobImage"] = null;
                hdfSrnoWithDrwal.Value = null;
                hdfIdnoWithDrwal.Value = null;

            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnshowoperatmana_Click(object sender, EventArgs e)
    {
        binddataMana();
    }
    protected void btncancelomana_Click(object sender, EventArgs e)
    {
        rdooperatmana.ClearSelection();
        ddltypemana.SelectedIndex = 0;
        divmana.Visible = false;
        lvmanager.DataSource = null;
        lvmanager.DataBind();
    }
    protected void submitmana_Click(object sender, EventArgs e)
    {
        try
        {
            int count=0;
            CustomStatus cs = 0;
            DataSet dsUserContact = null;
            string message = "";
            string subject = "";
            string status = string.Empty;
            string status1 = "Approved";
            dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));

            int ua_no = Convert.ToInt32(Session["userno"]);
            if (lblexpectedrefmana.Text == "")
            {
                lblexpectedrefmana.Text = "0";
            }

            foreach (ListViewDataItem dataitem in lvrefundmana.Items)
            {
                CheckBox chkBox = dataitem.FindControl("chktransfer") as CheckBox;
                TextBox txtrefundamt = dataitem.FindControl("txtrefundamt") as TextBox;
                HiddenField hdfdcr = dataitem.FindControl("hdfdcr") as HiddenField;
                if (chkBox.Checked == true)
                {
                    //continue;
                    count++;
                    decimal refundamt = 0.0m;
                    if (txtrefundamt.Text == "" && txtrefundamt.Text == "")
                    {
                        objCommon.DisplayMessage(this.updmodal, "Please Enter Refund Amount", this.Page);
                        return;
                    }
                    refundamt = Convert.ToDecimal(txtrefundamt.Text);
                    cs = (CustomStatus)stud.UpdDetailsByAdmin(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(refundamt), remarkmana.Text, Convert.ToInt32(ddlstatusmana.SelectedValue), (txtdesmana.Text), Convert.ToInt32(ViewState["srno"]), Convert.ToDecimal(Txttotalrefundmanage.Text), Convert.ToString(TxtRemarkstmana.Text), Convert.ToString(hdfdcr.Value));

                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(this.updmodal, "Please Select At least One checkbox", this.Page);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divmanager", "$(document).ready(function () {$('#divmanager').modal();});", true);
            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updmodal, "Record Saved Successfully!", this.Page);
                subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                Execute(message, Convert.ToString(ViewState["EMAILID"]), subject, Convert.ToString(ViewState["name"]), Convert.ToString(ViewState["regno"]), status1, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
                rdooperatmana.ClearSelection();
                ddltypemana.SelectedIndex = 0;
                divmana.Visible = false;
                lvmanager.DataSource = null;
                lvmanager.DataBind();
                //clear();

            }

            else
            {

                //objCommon.DisplayMessage(this.updmodal, "Please Select At least One checkbox", this.Page);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divmanager", "$(document).ready(function () {$('#divmanager').modal();});", true);
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void binddataMana()
    {
        if (rdooperatmana.SelectedValue == "1")
        {
            DataSet ds = null; string[] date;

            if (Convert.ToString(hdfdatemana.Value) == string.Empty || Convert.ToString(hdfdatemana.Value) == "0")
            {
                date = "0,0".Split(',');
            }
            else
            {
                date = hdfdatemana.Value.ToString().Split('-');
                date[0] = Convert.ToDateTime(date[0]).ToString("dd/MM/yyyy");
                date[1] = Convert.ToDateTime(date[1]).ToString("dd/MM/yyyy");
            }
            ds = stud.getwithapprovaldata(1, date[1], date[0], Convert.ToInt32(ddltypemana.SelectedValue), Convert.ToInt32(ddlfacultymana.SelectedValue));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                ViewState["name"] = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                ViewState["regno"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                ViewState["EMAILID"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();

                //btnSave.Visible = true;
                btncancelomana.Visible = true;
                divmana.Visible = true;
                lvmanager.DataSource = ds;
                lvmanager.DataBind();
            }

            else
            {
                hdfdatemana.Value = null;
                btncancelomana.Visible = true;
                lvmanager.DataSource = null;
                lvmanager.DataBind();
                objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
            }
            foreach (ListViewDataItem lvitem in lvmanager.Items)
            {
                Label labelstatus = lvitem.FindControl("labelstatus") as Label;
                LinkButton lnkstuddoc = lvitem.FindControl("lnkstuddocmana") as LinkButton;
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
        else if (rdooperatmana.SelectedValue == "2")
        {
            DataSet ds = null; string[] date;

            if (Convert.ToString(hdfdatemana.Value) == string.Empty || Convert.ToString(hdfdatemana.Value) == "0")
            {
                date = "0,0".Split(',');
            }
            else
            {

                date = hdfdatemana.Value.ToString().Split('-');
                date[0] = Convert.ToDateTime(date[0]).ToString("dd/MM/yyyy");
                date[1] = Convert.ToDateTime(date[1]).ToString("dd/MM/yyyy");
            }
            ds = stud.getwithapprovaldata(2, date[1], date[0], Convert.ToInt32(ddltypemana.SelectedValue), Convert.ToInt32(ddlfacultymana.SelectedValue));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                ViewState["name"] = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                ViewState["regno"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                ViewState["EMAILID"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                //btnSave.Visible = true;
                //btnCancel.Visible = true;
                divmana.Visible = true;
                lvmanager.DataSource = ds;
                lvmanager.DataBind();
            }

            else
            {
                hdfdatemana.Value = null;
                btncancelomana.Visible = true;
                lvmanager.DataSource = null;
                lvmanager.DataBind();
                objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
            }
            foreach (ListViewDataItem lvitem in lvmanager.Items)
            {
                Label labelstatus = lvitem.FindControl("labelstatus") as Label;
                LinkButton lnkstuddoc = lvitem.FindControl("lnkstuddocmana") as LinkButton;
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
        else if (rdooperatmana.SelectedValue == "3")
        {
            DataSet ds = null; string[] date;

            if (Convert.ToString(hdfdatemana.Value) == string.Empty || Convert.ToString(hdfdatemana.Value) == "0")
            {
                date = "0,0".Split(',');
            }
            else
            {
                date = hdfdatemana.Value.ToString().Split('-');
                date[0] = Convert.ToDateTime(date[0]).ToString("dd/MM/yyyy");
                date[1] = Convert.ToDateTime(date[1]).ToString("dd/MM/yyyy");
            }
            ds = stud.getwithapprovaldata(3, date[1], date[0], Convert.ToInt32(ddltypemana.SelectedValue), Convert.ToInt32(ddlfacultymana.SelectedValue));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                ViewState["name"] = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                ViewState["regno"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                ViewState["EMAILID"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                //btnSave.Visible = true;
                btncancelomana.Visible = true;
                divmana.Visible = true;
                lvmanager.DataSource = ds;
                lvmanager.DataBind();
            }

            else
            {
                hdfdatemana.Value = null;
                btncancelomana.Visible = true;
                lvmanager.DataSource = null;
                lvmanager.DataBind();
                objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
            }
            foreach (ListViewDataItem lvitem in lvmanager.Items)
            {
                Label labelstatus = lvitem.FindControl("labelstatus") as Label;
                LinkButton lnkstuddoc = lvitem.FindControl("lnkstuddocmana") as LinkButton;
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
    protected void lnkViewDocMana_Click(object sender, EventArgs e)   
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            int Idno = int.Parse(lnk.CommandArgument);
            ViewState["idno"] = Idno;
            int srno = int.Parse(lnk.CommandName);
            ViewState["srno"] = srno;
            string username = lnk.ToolTip;
            DataSet ds = null;
            hdfsrnomana.Value = Convert.ToString(srno);
            hdfidnomana.Value = Convert.ToString(Idno);
            string status = objCommon.LookUp("ACD_STUDENT_PROGRAM_CANCEL", "STATUS_NO", "IDNO=" + Idno + "AND SRNO=" + srno);
            ds = objdocContr.GetAllWidrawlStudentDetails(Idno, srno, 0, "0", "0", 1, 4, username);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                TxtRemarkstmana.Text = ds.Tables[0].Rows[0]["STUDENT_REMARK_BY_O_M_D_F"].ToString();
                Txttotalrefundmanage.Text = ds.Tables[0].Rows[0]["TOTAL_REFUND_AMOUNT"].ToString();
                lblappmana.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                ViewState["FINANCE_APPROVAL"] = ds.Tables[0].Rows[0]["FINANCE_APPROVAL"].ToString();
                ViewState["regno"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                lblstumana.Text = ds.Tables[0].Rows[0]["STUDFIRSTNAME"].ToString();
                ViewState["BlobImage"] = ds.Tables[0].Rows[0]["DOCUMENT"].ToString();
                lblrefundmana.Text = ds.Tables[0].Rows[0]["REFUND_AMOUNT"].ToString();
                lblnonrefundmana.Text = ds.Tables[0].Rows[0]["NONREFUND_AMOUNT"].ToString();
                txtdesmana.Text = ds.Tables[0].Rows[0]["REASON"].ToString();
                //ddlstatus.SelectedValue = ds.Tables[0].Rows[0]["STATS_BY_ADMIN"].ToString();
                remarkmana.Text = ds.Tables[0].Rows[0]["REMARK_BY_ADMIN"].ToString();
                lbldatemana.Text = ds.Tables[0].Rows[0]["STUDENT_APPLIED_DATE"].ToString();
                lblbankmana.Text = ds.Tables[0].Rows[0]["BANKNAME"].ToString();
                lblbranchmana.Text = ds.Tables[0].Rows[0]["BANKBRANCH"].ToString();
                lblaccopmana.Text = ds.Tables[0].Rows[0]["ACCOUNTNO"].ToString();
                lblcodemana.Text = ds.Tables[0].Rows[0]["BRANCHCODE"].ToString();
                ViewState["request_type"] = ds.Tables[0].Rows[0]["STATUS_NO"].ToString();
                if (status == "10")
                {
                    ds = objdocContr.GetExcessStudentDetails(Idno, srno);
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        lblexpectedrefmana.Text = ds.Tables[0].Rows[0]["REFUND_AMOUNT"].ToString();
                        lblfeesmana.Text = ds.Tables[0].Rows[0]["FEEPAID"].ToString();
                        lblbalancemana.Text = ds.Tables[0].Rows[0]["BALANCE"].ToString();
                        if (Convert.ToString(lblexpectedrefmana.Text) == "")
                        {
                            lblexpectedrefmana.Text = "0.00";
                        }
                        else
                        {
                            lblexpectedrefmana.Text = ds.Tables[0].Rows[0]["REFUND_AMOUNT"].ToString();
                        }
                        if (lblexpectedrefmana.Text == "0.00")
                        {
                            ddlstatusmana.SelectedValue = "1";
                        }
                        else if (Convert.ToDecimal(lblexpectedrefmana.Text) > Convert.ToDecimal(25000.00))
                        {
                            ddlstatusmana.SelectedValue = "2";
                        }
                        else if (Convert.ToDecimal(lblexpectedrefmana.Text) <= Convert.ToDecimal(25000.00))
                        {
                            ddlstatusmana.SelectedValue = "3";
                        }
                    }
                }
                else
                {

                    lblfeesmana.Text = ds.Tables[0].Rows[0]["FEEPAID"].ToString();
                    lblbalancemana.Text = ds.Tables[0].Rows[0]["BALANCE"].ToString();
                    lblexpectedrefmana.Text = ds.Tables[0].Rows[0]["REFUND_AMOUNT"].ToString();
                    if (Convert.ToString(lblexpectedrefmana.Text) == "")
                    {
                        lblexpectedrefmana.Text = "0.00";
                    }
                    else
                    {
                        lblexpectedrefmana.Text = ds.Tables[0].Rows[0]["REFUND_AMOUNT"].ToString();
                    }
                    if (lblexpectedrefmana.Text == "0.00")
                    {
                        ddlstatusmana.SelectedValue = "1";
                    }
                    else if (Convert.ToDecimal(lblexpectedrefmana.Text) > Convert.ToDecimal(25000.00))
                    {
                        ddlstatusmana.SelectedValue = "2";
                    }
                    else if (Convert.ToDecimal(lblexpectedrefmana.Text) <= Convert.ToDecimal(25000.00))
                    {
                        ddlstatusmana.SelectedValue = "3";
                    }
                }
                bindrefundamountdetailsMana();
                bindPriviousRefundmana();
                 if (Convert.ToInt32(ViewState["FINANCE_APPROVAL"]) == 1)
                    {
                        Txttotalrefundmanage.Enabled = false;
                        TxtRemarkstmana.Enabled = false;
                        remarkmana.Enabled = false;
                    }
                    else
                    {
                        Txttotalrefundmanage.Enabled = true;
                        TxtRemarkstmana.Enabled = true;
                        remarkmana.Enabled = true;
                    }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divmanager", "$(document).ready(function () {$('#divmanager').modal();});", true);
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
                //    ddlstatus.SelectedValue = "1";
                //    if (Convert.ToString(test) == "")
                //    {
                //        lblexpectedrefmana.Text = "0.00";
                //    }
                //    else
                //    {
                //        lblexpectedrefmana.Text = test.ToString();
                //    }
                //    if (lblexpectedrefmana.Text == "0")
                //    {
                //        ddlstatusmana.SelectedValue = "1";
                //    }
                //    else if (Convert.ToDecimal(lblexpectedrefmana.Text) > Convert.ToDecimal(25000.00))
                //    {
                //        ddlstatusmana.SelectedValue = "2";
                //    }
                //    else if (Convert.ToDecimal(lblexpectedrefmana.Text) <= Convert.ToDecimal(25000.00))
                //    {
                //        ddlstatusmana.SelectedValue = "3";
                //    }
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divmanager", "$(document).ready(function () {$('#divmanager').modal();});", true);
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divmanager", "$(document).ready(function () {$('#divmanager').modal();});", true);
                //    lblexpectedrefmana.Text = "0.00";
                //    if (lblexpectedrefmana.Text == "0.00")
                //    {
                //        ddlstatusmana.SelectedValue = "1";
                //    }
                //    else if (Convert.ToDecimal(lblexpectedrefmana.Text) > Convert.ToDecimal(25000.00))
                //    {
                //        ddlstatusmana.SelectedValue = "2";
                //    }
                //    else if (Convert.ToDecimal(lblexpectedrefmana.Text) < Convert.ToDecimal(25000.00))
                //    {
                //        ddlstatusmana.SelectedValue = "3";
                //    }

                //}
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divmanager", "$(document).ready(function () {$('#divmanager').modal();});", true);
                ViewState["BlobImage"] = null;
                hdfSrnoWithDrwal.Value = null;
                hdfIdnoWithDrwal.Value = null;

            }

        }
        catch (Exception ex)
        {

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
    protected void btnClose_Click(object sender, EventArgs e)
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

    protected void btnClose11_Click(object sender, EventArgs e)
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