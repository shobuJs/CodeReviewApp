//======================================================================================
// PROJECT NAME  : SMS-SERVICE                                                                
// MODULE NAME   : SMS                                                             
// PAGE NAME     : SMS SERVICE                                              
// CREATION DATE : 01-NOV-2012                                                      
// CREATED BY    : PAVAN RAUT                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SmsLayer;
using IITMS.UAIMS;

namespace SmsWebApps
{
    public partial class SmsService : System.Web.UI.Page
    {
        SmsLayer.Sms objsms = new SmsLayer.Sms();
        static string smsConstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        SmsController objSmsC = new SmsController(smsConstring);
        Common_sms objCommon = new Common_sms();
        Common objCommons = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckPageAuthorization();
            this.BindService();
            this.BindMessageServer();
            this.BindPendingSms();
            if (!Page.IsPostBack)
            {
                
                objCommon.FillDropDownList(ddlSmsCode, "SMS_TEMPLATE_MASTER", "TEMPLETEID", "SMSCODE", "", "");
                Session["action"] = "add";
            }
        }
        private void CheckPageAuthorization()
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=SmsService.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=SmsService.aspx");
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            objsms.DisplayName = txtDisplayName.Text;
            objsms.ServiceName = txtServiceUrl.Text;
            objsms.Usename = txtUsername.Text;
            objsms.Password = txtPassword.Text;
            objsms.Active = Convert.ToBoolean(rbActive.SelectedValue);
            CustomStatus_sms result = CustomStatus_sms.Others;
            if (Session["action"].ToString() == "edit")
            {
                result = (CustomStatus_sms)objSmsC.UpdateSmsService(objsms);
            }
            else
            {
                result = (CustomStatus_sms)objSmsC.AddSmsService(objsms);
            }
            if (result == CustomStatus_sms.RecordSaved)
            {
                objCommon.DisplayMessage("Sms Service Added Successfully!", this.Page);
            }

            this.BindService();
        }

        public void BindService()
        {
            DataSet ds = objCommon.FillDropDown("SMS_SERVICE_MASTER", "SERVICEID,DISPLAYNAME, SERVICENAME", "USERNAME, PASSWORD, (CASE ACTIVE WHEN 1 THEN 'YES' ELSE 'NO' END)ACTIVE", "", "");
            lvSmsService.DataSource = ds;
            lvSmsService.DataBind();
        }

        public void BindMessageServer()
        {
            DataSet ds = objCommon.FillDropDown("SMS_MESSENGER_SERVER", "MSGSERVERID, SERVERNAME, SERVERIP", "SERVERPORT,(CASE ACTIVEFLAG WHEN 1 THEN 'YES' ELSE 'NO' END)ACTIVEFLAG, PENDINGSMS, NOOFRETRY, WEBSERVICE", "", "");
            lvMsgServer.DataSource = ds;
            lvMsgServer.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
            tcSmsService.TabIndex = 0;
        }

        protected void btnAddMessageServer_Click(object sender, EventArgs e)
        {
            objsms.MsgServerName = txtMessageServerName.Text;
            objsms.MsgServerIP = txtIPAdd.Text;
            objsms.MsgPort = Convert.ToInt32(txtPort.Text);
            objsms.MsgWebService = txtWebService.Text;
            objsms.Activeflag = Convert.ToBoolean(rbActiveFlag.SelectedValue);
            objsms.PendingSms = Convert.ToInt32(txtPedingSms.Text);
            objsms.NoOfRetry = Convert.ToInt32(txtNoofTry.Text);

            CustomStatus_sms result = (CustomStatus_sms)objSmsC.AddMsgServer(objsms);
            if (result == CustomStatus_sms.RecordSaved)
            {
                objCommon.DisplayMessage("Message Server Added Successfully!", this.Page);
            }
            this.BindMessageServer();
        }

        protected void btnCancelMsg_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
            tcSmsService.TabIndex = 1;
        }


        protected void btnSendSms_Click(object sender, EventArgs e)
        {
            // Sms Parameter
            objsms.Smscode = ddlSmsCode.SelectedItem.Text.ToString();
            objsms.V1 = txtSV1.Text;
            objsms.V2 = txtSV2.Text == string.Empty ? "" : txtSV2.Text;
            objsms.V3 = txtSV3.Text == string.Empty ? "" : txtSV3.Text;
            objsms.V4 = txtSV4.Text == string.Empty ? "" : txtSV4.Text;
            objsms.V5 = txtSV5.Text == string.Empty ? "" : txtSV5.Text;
            objsms.V6 = txtSV6.Text == string.Empty ? "" : txtSV6.Text;
            objsms.V7 = txtSV2.Text == string.Empty ? "" : txtSV7.Text;
            objsms.V8 = txtSV2.Text == string.Empty ? "" : txtSV8.Text;
            objsms.V9 = txtSV2.Text == string.Empty ? "" : txtSV9.Text;
            objsms.V10 = txtSV2.Text == string.Empty ? "" : txtSV10.Text;

            // Parameter required to save sms in Save Sms table and Pending Sms table if required.
            objsms.Ua_no = 1;
            objsms.Mobileno = txtMobileNo.Text;
            objsms.SendingDate = DateTime.Now;

            string Msg = string.Empty;
            Msg = objSmsC.SendSms(objsms);
            objCommon.DisplayMessage(Msg, this.Page);

        }

        protected void btnSendSmsCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
            tcSmsService.TabIndex = 2;
        }

        protected void ddlSmsCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(objCommon.LookUp("SMS_TEMPLATE_MASTER", "PARAMETER_COUNT", "TEMPLETEID =" + Convert.ToInt32(ddlSmsCode.SelectedValue)));
            txtSmsCodePara.Text = count.ToString();
            if (count == 2)
            {
                Smstr1.Visible = true;
                Smstr2.Visible = false;
                Smstr3.Visible = false;
                Smstr4.Visible = false;
                Smstr5.Visible = false;
                Smstr6.Visible = false;
                Smstr7.Visible = false;
                Smstr8.Visible = false;
                Smstr9.Visible = false;
            }
            if (count == 3)
            {
                Smstr1.Visible = true;
                Smstr2.Visible = true;
                Smstr3.Visible = false;
                Smstr4.Visible = false;
                Smstr5.Visible = false;
                Smstr6.Visible = false;
                Smstr7.Visible = false;
                Smstr8.Visible = false;
                Smstr9.Visible = false;
            }
            if (count == 4)
            {
                Smstr1.Visible = true;
                Smstr2.Visible = true;
                Smstr3.Visible = true;
                Smstr4.Visible = false;
                Smstr5.Visible = false;
                Smstr6.Visible = false;
                Smstr7.Visible = false;
                Smstr8.Visible = false;
                Smstr9.Visible = false;
            }
            if (count == 5)
            {
                Smstr1.Visible = true;
                Smstr2.Visible = true;
                Smstr3.Visible = true;
                Smstr4.Visible = true;
                Smstr5.Visible = false;
                Smstr6.Visible = false;
                Smstr7.Visible = false;
                Smstr8.Visible = false;
                Smstr9.Visible = false;
            }
            if (count == 6)
            {
                Smstr1.Visible = true;
                Smstr2.Visible = true;
                Smstr3.Visible = true;
                Smstr4.Visible = true;
                Smstr5.Visible = true;
                Smstr6.Visible = false;
                Smstr7.Visible = false;
                Smstr8.Visible = false;
                Smstr9.Visible = false;
            }
            if (count == 7)
            {
                Smstr1.Visible = true;
                Smstr2.Visible = true;
                Smstr3.Visible = true;
                Smstr4.Visible = true;
                Smstr5.Visible = true;
                Smstr6.Visible = true;
                Smstr7.Visible = false;
                Smstr8.Visible = false;
                Smstr9.Visible = false;
            }
            if (count == 8)
            {
                Smstr1.Visible = true;
                Smstr2.Visible = true;
                Smstr3.Visible = true;
                Smstr4.Visible = true;
                Smstr5.Visible = true;
                Smstr6.Visible = true;
                Smstr7.Visible = true;
                Smstr8.Visible = false;
                Smstr9.Visible = false;
            }
            if (count == 9)
            {
                Smstr1.Visible = true;
                Smstr2.Visible = true;
                Smstr3.Visible = true;
                Smstr4.Visible = true;
                Smstr5.Visible = true;
                Smstr6.Visible = true;
                Smstr7.Visible = true;
                Smstr8.Visible = true;
                Smstr9.Visible = false;
            }
            if (count == 10)
            {
                Smstr1.Visible = true;
                Smstr2.Visible = true;
                Smstr3.Visible = true;
                Smstr4.Visible = true;
                Smstr5.Visible = true;
                Smstr6.Visible = true;
                Smstr7.Visible = true;
                Smstr8.Visible = true;
                Smstr9.Visible = true;
            }
        }


        protected void btnCancel_pendingSms_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
            tcSmsService.TabIndex = 3;
        }

        public void BindPendingSms()
        {
            DataSet ds = objCommon.FillDropDown("SMS_SAVE_PENDING_MESSAGE", "MsgID,MSG_CONTENT", "MOBILENO,CONVERT(NVARCHAR(30),SENDING_DATE,103) SENDING_DATE", "STATUS = 0 AND ISNULL(CANCEL,0) = 0", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvPendingSms.DataSource = ds;
                lvPendingSms.DataBind();
                lblPendingSmsStatus.Visible = false;
            }
            else
            {
                lvPendingSms.DataSource = null;
                lvPendingSms.DataBind();
                btnSendPendingSms.Enabled = false;
                btnCancel_pendingSms.Enabled = false;
                lblPendingSmsStatus.Text = "No pending Sms available ...";
                lblPendingSmsStatus.Visible = true;
            }
        }

        protected void btnSendPendingSms_Click(object sender, EventArgs e)
        {
            string result = string.Empty;
            int sendCount = 0;
            foreach (ListViewDataItem item in lvPendingSms.Items)
            {
                CheckBox chkSelect = item.FindControl("chkSelect") as CheckBox;
                HiddenField hdfMsgID = item.FindControl("hdfMsgID") as HiddenField;
                Label lblSms = item.FindControl("lblSms") as Label;
                Label lblMobileno = item.FindControl("lblMobileno") as Label;
                objsms.Ua_no = 1;
                objsms.Msg_content = lblSms.Text;
                objsms.Mobileno = lblMobileno.Text;
                objsms.Module_code = "BULKSMS";
                objsms.SendingDate = DateTime.Now;
                if (chkSelect.Checked == true)
                {
                    result = objSmsC.SendBulkSms(objsms);
                    if (result.ToString().Contains("Message Submitted"))
                    {
                        sendCount++;
                    }
                }
            }
            if (sendCount > 0)
                objCommon.DisplayMessage(sendCount + " Peding Sms Sent!", this.Page);
            else
                objCommon.DisplayMessage("No Peding Sms Sent!", this.Page);
        }

        protected void btnSubmitBulkSms_Click(object sender, EventArgs e)
        {
            string result = string.Empty;

            try
            {
                objsms.Module_code = "BULKSMS";

                // Parameter required to save sms in Save Sms table and Pending Sms table if required.
                objsms.Ua_no = 1;
                objsms.Mobileno = txtBulkMobileno.Text;
                objsms.Msg_content = txtBulkSms.Text;
                objsms.SendingDate = DateTime.Now;
                result = objSmsC.SendBulkSms(objsms);

                objCommon.DisplayMessage(result, this.Page);
            }
            catch (Exception ex)
            {
                objCommon.DisplayMessage("Error:" + ex.Message, this.Page);
            }
        }

        protected void btnCancelBulkSms_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
            tcSmsService.TabIndex = 4;
        }


        protected void btnEdit_Click1(object sender, ImageClickEventArgs e)
        {
            ImageButton btnEdit = sender as ImageButton;
            int serviceid = (btnEdit.CommandName != string.Empty ? int.Parse(btnEdit.CommandName) : 0);

            DataSet ds = objCommon.FillDropDown("SMS_SERVICE_MASTER", "SERVICEID, SERVICENAME", "ACTIVE, DISPLAYNAME, USERNAME, PASSWORD", "SERVICEID =" + serviceid, "");

            txtDisplayName.Text = ds.Tables[0].Rows[0]["DISPLAYNAME"].ToString();
            txtServiceUrl.Text = ds.Tables[0].Rows[0]["SERVICENAME"].ToString();
            txtUsername.Text = ds.Tables[0].Rows[0]["USERNAME"].ToString();
            txtPassword.Text = ds.Tables[0].Rows[0]["PASSWORD"].ToString();
            rbActive.SelectedValue = ds.Tables[0].Rows[0]["ACTIVE"].ToString();
            Session["action"] = "edit";
        }
    }

}
