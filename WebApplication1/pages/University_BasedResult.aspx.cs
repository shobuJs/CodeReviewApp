using System.Linq;
using System.Web;
using System.Xml.Linq;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Web.Configuration;
using IITMS.SQLServer.SQLDAL;
using ClosedXML.Excel;
using System.Data.OleDb;
using System.Web.Mail;
using System.Text;
using System.Threading.Tasks;

public partial class ACADEMIC_University_BasedResult : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
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
                //Page Authorization
                CheckPageAuthorization();
                LvUniBased.DataSource = null;
                LvUniBased.DataBind();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }
            }
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            objCommon.SetLabelData("0");//for label
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
            ViewState["action"] = "add";
            FilldropDown();
        }

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CollegeMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CollegeMaster.aspx");
        }
    }

    private void FilldropDown()
    {
        objCommon.FillDropDownList(ddlStudyLevel, "ACD_UA_SECTION", "DISTINCT UA_SECTION", "UA_SECTIONNAME", "", "UA_SECTION");
        objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "DISTINCT BATCHNO", "BATCHNAME", "ISNULL(ACTIVE,0)=1", "BATCHNO DESC");
    }
    private void BindData()
    {
        try
        {

            LvUniBased.DataSource = null;
            LvUniBased.DataBind();
            ViewState["DYNAMIC_DATASET"] = null;
            string SP_Name3 = "PKG_ACD_GET_INSERT_UNIVERSITY_BASED_RESULT";
            string SP_Parameters3 = "@P_INTAKE,@P_STUDY_LEVEL,@P_BRANCH_PREF,@P_COMMAND_TYPE,@P_OUTPUT";
            string Call_Values3 = "" + Convert.ToInt32(ddlIntake.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlStudyLevel.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlPreference.SelectedValue) + "," + 1 + "," + 0 + "";
            DataSet Ds = objCommon.DynamicSPCall_Select(SP_Name3, SP_Parameters3, Call_Values3);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
                search.Visible = true;
                //Panel1.Visible = true;
                //btnOffer.Visible = true;
                btnSubmit.Visible = true;
                LvUniBased.DataSource = Ds;
                LvUniBased.DataBind();
                ViewState["DYNAMIC_DATASET"] = Ds.Tables[0];
            }
            else
            {
                //Panel1.Visible = false;
               // btnOffer.Visible = false;
                search.Visible = false;
                btnSubmit.Visible = false;
                LvUniBased.DataSource = null;
                LvUniBased.DataBind();
                objCommon.DisplayMessage(this.UpdUniResult, "No Record Found!", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            }
            foreach (ListViewDataItem dataitem in LvUniBased.Items)
            {
                Label lblresult1 = dataitem.FindControl("lblresult1") as Label;
                Label lblresult2 = dataitem.FindControl("lblresult2") as Label;
                Label lblresult3 = dataitem.FindControl("lblresult3") as Label;
                if (lblresult1.Text == "Pass")
                {
                    lblresult1.Text = "Pass";
                    lblresult1.CssClass = "badge badge-success";
                }
                else if (lblresult1.Text == "Deferred")
                {
                    lblresult1.Text = "Deferred";
                    lblresult1.CssClass = "badge badge-danger";
                }
                else
                {
                    lblresult1.Text = "-";
                    lblresult1.CssClass = "badge badge-Warning";
                }
                if (lblresult2.Text == "Pass")
                {
                    lblresult2.Text = "Pass";
                    lblresult2.CssClass = "badge badge-success";
                }
                else if (lblresult2.Text == "Deferred")
                {
                    lblresult2.Text = "Deferred";
                    lblresult2.CssClass = "badge badge-danger";
                }
                else
                {
                    lblresult2.Text = "-";
                    lblresult2.CssClass = "badge badge-Warning";
                }
                if (lblresult3.Text == "Pass")
                {
                    lblresult3.Text = "Pass";
                    lblresult3.CssClass = "badge badge-success";
                }
                else if (lblresult3.Text == "Deferred")
                {
                    lblresult3.Text = "Deferred";
                    lblresult3.CssClass = "badge badge-danger";
                }
                else
                {
                    lblresult3.Text = "-";
                    lblresult3.CssClass = "badge badge-Warning";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_University_BasedResult.BindData() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0, Blank_Count=0;
            string Userno = "";
            foreach (ListViewDataItem items in LvUniBased.Items)
            {
                CheckBox Chk = items.FindControl("chktransfer") as CheckBox;
                Label lblprogram1 = items.FindControl("lblprogram1") as Label;
                Label lblprogram2 = items.FindControl("lblprogram2") as Label;
                Label lblProgram3 = items.FindControl("lblProgram3") as Label;
                if (Chk.Checked == true)
                {
                    Userno += Chk.ToolTip + "$";
                    count++;
                    if (ddlPreference.SelectedValue == "1")
                    {
                        if (lblprogram1.Text == "")
                        {
                            Blank_Count++;
                        }
                    }
                    if (ddlPreference.SelectedValue == "2")
                    {
                        if (lblprogram2.Text == "")
                        {
                            Blank_Count++;
                        }
                    }
                    if (ddlPreference.SelectedValue == "3")
                    {
                        if (lblProgram3.Text == "")
                        {
                            Blank_Count++;
                        }
                    }
                }
                
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(this.UpdUniResult, "Please Select at least one checkbox!", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
                return;
            }
            if (Blank_Count == count)
            {
                objCommon.DisplayMessage(this.UpdUniResult, "Result not updated for Blank Preference!", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
                return;
            }
            Userno = Userno.TrimEnd('$');
            string SP_Name1 = "PKG_ACD_GET_INSERT_UNIVERSITY_BASED_RESULT";
            string SP_Parameters1 = "@P_INTAKE,@P_STUDY_LEVEL,@P_RESULT,@P_USERNO,@P_COMMAND_TYPE,@P_BRANCH_PREF,@P_UA_NO,@P_OUTPUT";
            string Call_Values1 = "" + Convert.ToInt32(ddlIntake.SelectedValue) + "," + Convert.ToInt32(ddlStudyLevel.SelectedValue) + "," +
                Convert.ToInt32(ddlResult.SelectedValue) + "," + Userno + "," + 2 + "," + Convert.ToInt32(ddlPreference.SelectedValue) + "," + Convert.ToInt32(Session["userno"]) + ",0";

            string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//insert
            if (que_out1 == "1")
            {
                objCommon.DisplayMessage(this.UpdUniResult, "Record Saved Successfully!", this.Page);
                if (ddlResult.SelectedValue == "2")
                {
                    string Program = "";
                    foreach (ListViewDataItem items in LvUniBased.Items)
                    {
                        CheckBox Chk = items.FindControl("chktransfer") as CheckBox;
                        Label lblprogram1 = items.FindControl("lblprogram1") as Label;
                        Label lblprogram2 = items.FindControl("lblprogram2") as Label;
                        Label lblProgram3 = items.FindControl("lblProgram3") as Label;
                        Label lblStudname = items.FindControl("lblStudname") as Label;
                        HiddenField hdfEmailid = items.FindControl("hdfEmailid") as HiddenField;
                        HiddenField hdfintake = items.FindControl("hdfintake") as HiddenField;
                        if (Chk.Checked == true)
                        {
                            if (ddlPreference.SelectedValue == "1")
                            {
                                Program = lblprogram1.Text;
                            }
                            else if (ddlPreference.SelectedValue == "2")
                            { Program = lblprogram2.Text; }
                            else if (ddlPreference.SelectedValue == "3")
                            { Program = lblProgram3.Text; }

                            EmaiLDiffered(3598, hdfEmailid.Value, lblStudname.Text, Program, hdfintake.Value);
                        }
                    }
                }
               
                if (ddlResult.SelectedValue == "1")
                {
                    OfferSend();
                }
                ddlPreference.SelectedIndex = 0;
                ddlResult.SelectedIndex = 0;
                BindData();
                return;
            }
            else if (que_out1 == "3")
            {
                objCommon.DisplayMessage(this.UpdUniResult, "Screen Configuration not Define for University!", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
                ddlPreference.SelectedIndex = 0;
                ddlResult.SelectedIndex = 0;
                BindData();
                return;
            }
            else if (que_out1 == "4")
            {
                objCommon.DisplayMessage(this.UpdUniResult, "Result cannot update beacuse Deposite slip already upload!", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
                ddlPreference.SelectedIndex = 0;
                ddlResult.SelectedIndex = 0;
                BindData();
                return;
            }
            else if (que_out1 == "7")
            {
                objCommon.DisplayMessage(this.UpdUniResult, "Preference 1 result is not declared yet!", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
                ddlPreference.SelectedIndex = 0;
                ddlResult.SelectedIndex = 0;
                BindData();
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.UpdUniResult, "Error!", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_University_BasedResult.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void EmaiLDiffered(int Page_No, string toSendAddre, string Name, string program, string Yearname)
    {
        EmailSmsWhatsaap Email = new EmailSmsWhatsaap();
        string Res = ""; string Message = "";
        UserController objUC = new UserController();
        DataSet dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", "0", 3598);
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
            msgsPara.Body = msgsPara.Body.Replace("{Program Name}", program.ToString());
            msgsPara.Body = msgsPara.Body.Replace("{Name}", Name.ToString());
            msgsPara.Body = msgsPara.Body.Replace("{Academic year}", Yearname.ToString());
            MemoryStream Attachment = null; string AttachmentName = "";
            Task<string> task = Email.Execute(msgsPara.Body, toSendAddress, Subject, Attachment, AttachmentName.ToString());
            Res = task.Result;

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlIntake.SelectedIndex = 0;
        ddlStudyLevel.SelectedIndex = 0;
        ddlPreference.SelectedIndex = 0;
        ddlResult.SelectedIndex = 0;
        //btnOffer.Visible = false;
        btnSubmit.Visible = false;
        LvUniBased.DataSource = null;
        LvUniBased.DataBind();
    }
    protected void OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        (LvUniBased.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["DYNAMIC_DATASET"];
        LvUniBased.DataSource = dt;
        LvUniBased.DataBind();
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
    }
    private void OfferSend()
    {
        string Program = "", offerstatus = "", CollegeName = "";
        int count = 0, Deferred_Count = 0, Blank_Count = 0, hold = 0;
        foreach (ListViewDataItem items in LvUniBased.Items)
        {

            CheckBox Chk = items.FindControl("chktransfer") as CheckBox;
            Label lblprogram1 = items.FindControl("lblprogram1") as Label;
            Label lblprogram2 = items.FindControl("lblprogram2") as Label;
            Label lblProgram3 = items.FindControl("lblProgram3") as Label;
            //Label lblresult1 = items.FindControl("lblresult1") as Label;
            //Label lblresult2 = items.FindControl("lblresult2") as Label;
            //Label lblresult3 = items.FindControl("lblresult3") as Label;
            Label lblStudname = items.FindControl("lblStudname") as Label;
            HiddenField hdfEmailid = items.FindControl("hdfEmailid") as HiddenField;
            HiddenField hdfintake = items.FindControl("hdfintake") as HiddenField;
            HiddenField hdfGetdate = items.FindControl("hdfGetdate") as HiddenField;
            HiddenField hdfCollege = items.FindControl("hdfCollege") as HiddenField;
            HiddenField hdfSrno = items.FindControl("hdfSrno") as HiddenField;
            HiddenField hdfadmfee1 = items.FindControl("hdfadmfee1") as HiddenField;
            HiddenField hdfadmfee2 = items.FindControl("hdfadmfee2") as HiddenField;
            HiddenField hdfadmfee3 = items.FindControl("hdfadmfee3") as HiddenField;
            HiddenField hdfdownfee1 = items.FindControl("hdfdownfee1") as HiddenField;
            HiddenField hdfdownfee2 = items.FindControl("hdfdownfee2") as HiddenField;
            HiddenField hdfdownfee3 = items.FindControl("hdfdownfee3") as HiddenField;
            /// HiddenField hdfScreen = items.FindControl("hdfScreen") as HiddenField;
            if (Chk.Checked == true)
            {
                count++;
                string SP_Name3 = "PKG_ACD_GET_OFFER_LETTER_SEND_STATUS";
                string SP_Parameters3 = "@P_INTAKE,@P_STUDY_LEVEL,@P_USERNO,@P_BRANCH_PREF";
                string Call_Values3 = "" + Convert.ToInt32(ddlIntake.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlStudyLevel.SelectedValue.ToString()) + "," + Convert.ToInt32(Chk.ToolTip) + "," + Convert.ToInt32(ddlPreference.SelectedValue);
                DataSet Ds = objCommon.DynamicSPCall_Select(SP_Name3, SP_Parameters3, Call_Values3);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    offerstatus = Ds.Tables[0].Rows[0]["offereresult"].ToString();
                    CollegeName = Ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                }
                if (ddlPreference.SelectedValue == "1")
                {
                    //if (ddlResult.SelectedValue=="1")
                    //{
                        Program = lblprogram1.Text;
                        if (offerstatus == "3" || offerstatus == "2" || offerstatus == "5")
                        {
                            EmaiLOfferUniversity(36182, hdfEmailid.Value, lblStudname.Text, Program, hdfintake.Value, hdfGetdate.Value, CollegeName, hdfadmfee1.Value, hdfdownfee1.Value);
                        }
                        else
                        {
                            hold++;
                        }
                    //}
                    //else if (lblresult1.Text == "Deferred")
                    //{
                    //    Deferred_Count++;
                    //}
                    //else
                    //{
                    //    Blank_Count++;
                    //}
                }

                else if (ddlPreference.SelectedValue == "2")
                {
                    //if (ddlResult.SelectedValue == "1")
                    //{
                        Program = lblprogram2.Text;
                        if (offerstatus == "3" || offerstatus == "2" || offerstatus == "5")
                        {
                            EmaiLOfferUniversity(36182, hdfEmailid.Value, lblStudname.Text, Program, hdfintake.Value, hdfGetdate.Value, CollegeName, hdfadmfee2.Value, hdfdownfee2.Value);
                        }
                        else
                        {
                            hold++;
                        }

                    //}
                    //else if (lblresult2.Text == "Deferred")
                    //{
                    //    Deferred_Count++;
                    //}
                    //else
                    //{
                    //    Blank_Count++;
                    //}
                }
                else if (ddlPreference.SelectedValue == "3")
                {
                    //if (lblresult3.Text == "Pass")
                    //{
                        Program = lblProgram3.Text;
                        if (offerstatus == "3" || offerstatus == "2" || offerstatus == "5")
                        {
                            EmaiLOfferUniversity(36182, hdfEmailid.Value, lblStudname.Text, Program, hdfintake.Value, hdfGetdate.Value, CollegeName, hdfadmfee3.Value, hdfdownfee3.Value);
                        }
                        else
                        {
                            hold++;
                        }

                    //}
                    //else if (lblresult3.Text == "Deferred")
                    //{
                    //    Deferred_Count++;
                    //}
                    //else
                    //{
                    //    Blank_Count++;
                    //}
                }

                string SP_Name1 = "PKG_ACD_PROGRAM_UNIVERSITY_OFFER_SEND_LOG";
                string SP_Parameters1 = "@P_COMMAND_TYPE,@P_USERNO,@P_SRNO,@P_OUTPUT";
                string Call_Values1 = "" + 2 + "," + Convert.ToInt32(Chk.ToolTip) + "," + Convert.ToInt32(hdfSrno.Value) + ",0";
                string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//grade allotment

            }

        }

    }
    protected void btnOffer_Click(object sender, EventArgs e)
    {
        OfferSend();
    }
    protected void EmaiLOfferProgram(int Page_No, string toSendAddre, string Name, string program, string Yearname, string Getdate, string College)
    {
        EmailSmsWhatsaap Email = new EmailSmsWhatsaap();
        string Res = ""; string Message = "";
        UserController objUC = new UserController();
        DataSet dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", "0", 36181);
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
            msgsPara.Body = msgsPara.Body.Replace("{Program}", program.ToString());
            msgsPara.Body = msgsPara.Body.Replace("{getdate}", Getdate.ToString());
            msgsPara.Body = msgsPara.Body.Replace("{name}", Name.ToString());
            msgsPara.Body = msgsPara.Body.Replace("{admbatch}", Yearname.ToString());
            msgsPara.Body = msgsPara.Body.Replace("{College}", College.ToString());
            MemoryStream Attachment = null; string AttachmentName = "";
            Task<string> task = Email.Execute(msgsPara.Body, toSendAddress, Subject, Attachment, AttachmentName.ToString());
            Res = task.Result;
            if (Res == "Email Send Succesfully")
            {
                objCommon.DisplayMessage(this.UpdUniResult, "Email Sent Successfully!", this.Page);
                BindData();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            }
            else
            {
                objCommon.DisplayMessage(this.UpdUniResult, "Error!", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            }
        }
    }
    protected void EmaiLOfferUniversity(int Page_No, string toSendAddre, string Name, string program, string Yearname, string Getdate, string College,string Admfee,string DownFee)
    {
        EmailSmsWhatsaap Email = new EmailSmsWhatsaap();
        string Res = ""; string Message = "";
        UserController objUC = new UserController();
        DataSet dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", "0", 36182);
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
            msgsPara.Body = msgsPara.Body.Replace("{Program}", program.ToString());
            msgsPara.Body = msgsPara.Body.Replace("{getdate}", Getdate.ToString());
            msgsPara.Body = msgsPara.Body.Replace("{name}", Name.ToString());
            msgsPara.Body = msgsPara.Body.Replace("{admbatch}", Yearname.ToString());
            msgsPara.Body = msgsPara.Body.Replace("{AdmFee}", Admfee.ToString());
            msgsPara.Body = msgsPara.Body.Replace("{DownFee}", DownFee.ToString());
            msgsPara.Body = msgsPara.Body.Replace("{College}", College.ToString());
            MemoryStream Attachment = null; string AttachmentName = "";
            Task<string> task = Email.Execute(msgsPara.Body, toSendAddress, Subject, Attachment, AttachmentName.ToString());
            Res = task.Result;
            if (Res == "Email Send Succesfully")
            {
                objCommon.DisplayMessage(this.UpdUniResult, "Email Sent Successfully!", this.Page);
                BindData();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            }
            else
            {
                objCommon.DisplayMessage(this.UpdUniResult, "Error!", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            }
        }
    }
    protected void FilterData_TextChanged(object sender, EventArgs e)
    {
        try
        {

            LvUniBased.DataSource = null;
            LvUniBased.DataBind();
            ViewState["DYNAMIC_DATASET"] = null;
            string SP_Name3 = "PKG_ACD_GET_INSERT_UNIVERSITY_BASED_RESULT";
            string SP_Parameters3 = "@P_INTAKE,@P_STUDY_LEVEL,@P_BRANCH_PREF,@P_COMMAND_TYPE,@P_USERNO,@P_OUTPUT";
            string Call_Values3 = "" + Convert.ToInt32(ddlIntake.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlStudyLevel.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlPreference.SelectedValue) + "," + 1 + "," + FilterData.Text + "," + 0 + "";
            DataSet Ds = objCommon.DynamicSPCall_Select(SP_Name3, SP_Parameters3, Call_Values3);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
                search.Visible = true;
                //Panel1.Visible = true;
                //btnOffer.Visible = true;
                btnSubmit.Visible = true;
                LvUniBased.DataSource = Ds;
                LvUniBased.DataBind();
                ViewState["DYNAMIC_DATASET"] = Ds.Tables[0];
            }
            else
            {
                //Panel1.Visible = false;
                // btnOffer.Visible = false;
                search.Visible = false;
                btnSubmit.Visible = false;
                LvUniBased.DataSource = null;
                LvUniBased.DataBind();
                objCommon.DisplayMessage(this.UpdUniResult, "No Record Found!", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            }
            foreach (ListViewDataItem dataitem in LvUniBased.Items)
            {
                Label lblresult1 = dataitem.FindControl("lblresult1") as Label;
                Label lblresult2 = dataitem.FindControl("lblresult2") as Label;
                Label lblresult3 = dataitem.FindControl("lblresult3") as Label;
                if (lblresult1.Text == "Pass")
                {
                    lblresult1.Text = "Pass";
                    lblresult1.CssClass = "badge badge-success";
                }
                else if (lblresult1.Text == "Deferred")
                {
                    lblresult1.Text = "Deferred";
                    lblresult1.CssClass = "badge badge-danger";
                }
                else
                {
                    lblresult1.Text = "-";
                    lblresult1.CssClass = "badge badge-Warning";
                }
                if (lblresult2.Text == "Pass")
                {
                    lblresult2.Text = "Pass";
                    lblresult2.CssClass = "badge badge-success";
                }
                else if (lblresult2.Text == "Deferred")
                {
                    lblresult2.Text = "Deferred";
                    lblresult2.CssClass = "badge badge-danger";
                }
                else
                {
                    lblresult2.Text = "-";
                    lblresult2.CssClass = "badge badge-Warning";
                }
                if (lblresult3.Text == "Pass")
                {
                    lblresult3.Text = "Pass";
                    lblresult3.CssClass = "badge badge-success";
                }
                else if (lblresult3.Text == "Deferred")
                {
                    lblresult3.Text = "Deferred";
                    lblresult3.CssClass = "badge badge-danger";
                }
                else
                {
                    lblresult3.Text = "-";
                    lblresult3.CssClass = "badge badge-Warning";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_University_BasedResult.BindData() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}