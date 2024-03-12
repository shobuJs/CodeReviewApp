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

public partial class ACADEMIC_Program_BasedResult : System.Web.UI.Page
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
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }
            }
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
        objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "ISNULL(ACTIVE,0)=1", "BATCHNO DESC");

        objCommon.FillDropDownList(ddlStudyOne, "ACD_UA_SECTION", "DISTINCT UA_SECTION", "UA_SECTIONNAME", "", "UA_SECTION");
        objCommon.FillDropDownList(ddlIntakeOne, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "ISNULL(ACTIVE,0)=1", "BATCHNO DESC");
    }

    protected void ddlStudyOne_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlProgramOne.SelectedIndex = 0;
        Panel1.Visible = false;
        btnSubmitOne.Visible = false;
        LvProgramList.DataSource = null;
        LvProgramList.DataBind();
        objCommon.FillDropDownList(ddlProgramOne, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON B.BRANCHNO=CDB.BRANCHNO INNER JOIN ACD_DEGREE D ON D.DEGREENO=CDB.DEGREENO INNER JOIN ACD_SCREENING_CONFIGURATION S ON S.DEGREENO=CDB.DEGREENO AND S.BRANCHNO = CDB.BRANCHNO", "DISTINCT CONCAT(CDB.DEGREENO,',',CDB.BRANCHNO)PROGRAM_NO", "(DEGREENAME + ' ' + LONGNAME)PROGRAM", "ISNULL(ACTIVE,0)=1 AND  SCREENO IN (2) AND CDB.UGPGOT=" + Convert.ToInt32(ddlStudyOne.SelectedValue), "PROGRAM_NO");
        
    }

    private void BindData()
    {
        try
        {
            string[] program;
            if (ddlProgramOne.SelectedValue == "0")
            {
                program = "0,0".Split(',');
            }
            else
            {
                program = ddlProgramOne.SelectedValue.Split(',');
            }
            string SP_Name3 = "PKG_ACD_GET_INSERT_PROGRAM_BASED_INTERVIEW_SCHEDULE";
            string SP_Parameters3 = "@P_ADMBATCH,@P_STUDY_LEVEL,@P_DEGREENO,@P_BRANCHNO,@P_COMMAND_TYPE,@P_OUTPUT";
            string Call_Values3 = "" + Convert.ToInt32(ddlIntakeOne.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlStudyOne.SelectedValue.ToString()) + "," + Convert.ToInt32(program[0]) + "," + Convert.ToInt32(program[1]) + "," + 1 + "," + 0 + "";
            DataSet Ds = objCommon.DynamicSPCall_Select(SP_Name3, SP_Parameters3, Call_Values3);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
                Panel1.Visible = true;
                btnSendSchedule.Visible = true;
                btnSubmitOne.Visible = true;
                LvProgramList.DataSource = Ds;
                LvProgramList.DataBind();
            }
            else
            {
                Panel1.Visible = false;
                btnSendSchedule.Visible = false;
                btnSubmitOne.Visible = false;
                LvProgramList.DataSource = null;
                LvProgramList.DataBind();
                objCommon.DisplayMessage(this.updPgmResult, "No Record Found!", this.Page);
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
    protected void btnShowOne_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnSendSchedule_Click(object sender, EventArgs e)
    {
        int countEmail = 0,vALIDATE=0;
        string[] program;
        if (ddlProgramOne.SelectedValue == "0")
        {
            program = "0,0".Split(',');
        }
        else
        {
            program = ddlProgramOne.SelectedValue.Split(',');
        }
        foreach (ListViewDataItem item in LvProgramList.Items)
        {
            CheckBox Chk = item.FindControl("chktransfer") as CheckBox;
            Label lblScheduleDate = item.FindControl("lblScheduleDate") as Label;
            Label lblScheduleTime = item.FindControl("lblScheduleTime") as Label;
            Label lblScheduleVenue = item.FindControl("lblScheduleVenue") as Label;
            Label lblUserName = item.FindControl("lblUserName") as Label;
            HiddenField hdfemailid = item.FindControl("hdfemailid") as HiddenField;
            HiddenField hdfSemester = item.FindControl("hdfSemester") as HiddenField;
            HiddenField hdfAdmbatch = item.FindControl("hdfAdmbatch") as HiddenField;
            HiddenField hdfDate = item.FindControl("hdfDate") as HiddenField;
            Label lblProgram = item.FindControl("lblProgram") as Label;
            HiddenField hdfCollege = item.FindControl("hdfCollege") as HiddenField;
            
            if (Chk.Checked == true)
            {
                countEmail++;

                string SP_Name3 = "PKG_ACD_PROGRAM_GET_SCEDULE_LOG";
                string SP_Parameters3 = "@P_USERNO,@P_DEGREENO,@P_BRANCHNO";
                string Call_Values3 = "" + Convert.ToInt32(Chk.ToolTip) + "," + Convert.ToInt32(program[0]) + "," + Convert.ToInt32(program[1]);
                DataSet Ds = objCommon.DynamicSPCall_Select(SP_Name3, SP_Parameters3, Call_Values3);

                if (lblScheduleDate.Text == "" || lblScheduleTime.Text == "" || lblScheduleVenue.Text == "")
                {
                    objCommon.DisplayMessage(this.updPgmResult, "Schedule not define!", this.Page);
                    BindData();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
                    return;
                }
                if (Ds.Tables[0].Rows.Count > 0)
                {
                   vALIDATE = Convert.ToInt32(Ds.Tables[0].Rows[0]["RESULT"]);
                   if (vALIDATE == 1)
                   {
                       objCommon.DisplayMessage(this.updPgmResult, "Result already declared!", this.Page);
                       BindData();
                       ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
                       return;
                   }
                }
                EmailSchedule(Convert.ToInt32(Request.QueryString["pageno"]), hdfemailid.Value, lblUserName.Text, hdfCollege.Value, hdfSemester.Value, lblScheduleDate.Text, lblScheduleTime.Text, lblScheduleVenue.Text, hdfDate.Value, hdfAdmbatch.Value);
            }
            Chk.Checked = false;     
        }
        if (countEmail == 0)
        {
            objCommon.DisplayMessage(this.updPgmResult, "Please select at least one checkbox!", this.Page);
            BindData();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            return;
        }
    }
    protected void EmailSchedule(int Page_No, string toSendAddre, string Name, string program, string semester, string date, string Time, string Venue,string Todaydate,string batchname)
    {
        EmailSmsWhatsaap Email = new EmailSmsWhatsaap();
        DataSet ds = objCommon.FillDropDown("ACCESS_LINK", "isnull(EMAIL,0) EMAIL,isnull(SMS,0) as SMS,isnull(WHATSAAP,0) as WHATSAAP", "", "AL_NO=" + Convert.ToInt32(Request.QueryString["pageno"].ToString()), "");
        string Res = ""; string Message = "";
        int PageNo = Page_No;
        UserController objUC = new UserController();
        DataSet dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", "0", PageNo);
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
            msgsPara.Body = msgsPara.Body.Replace("{Y1S1 Fees}", semester.ToString());
            msgsPara.Body = msgsPara.Body.Replace("{Date}", date.ToString());
            msgsPara.Body = msgsPara.Body.Replace("{Time}", Time.ToString());
            msgsPara.Body = msgsPara.Body.Replace("{Venue}", Venue.ToString());
            msgsPara.Body = msgsPara.Body.Replace("{getdate}", Todaydate.ToString());
            msgsPara.Body = msgsPara.Body.Replace("{admbatch}", batchname.ToString());
            MemoryStream Attachment = null; string AttachmentName = "";
            Task<string> task = Email.Execute(msgsPara.Body, toSendAddress, Subject, Attachment, AttachmentName.ToString());
            Res = task.Result;
            if (Res == "Email Send Succesfully")
            {
                objCommon.DisplayMessage(this.updPgmResult, "Email Sent Successfully!", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            }
            else
            {
                objCommon.DisplayMessage(this.updPgmResult, "Error!", this.Page);
            }
        }
    }
    protected void btnCancelOne_Click(object sender, EventArgs e)
    {
        ddlIntakeOne.SelectedIndex = 0;
        ddlStudyOne.SelectedIndex = 0;
        ddlProgramOne.SelectedIndex = 0;
        TxtDate.Text = "";
        txtfrom.Text = "";
        txtTo.Text = "";
        TxtVenue.Text = "";
        btnSubmitOne.Visible = false;
        btnSendSchedule.Visible = false;
        Panel1.Visible = false;
        btnSubmit.Visible = false;
        btnOffer.Visible = false;
        LvProgramList.DataSource = null;
        LvProgramList.DataBind();
    }
    protected void btnSubmitOne_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            string[] program;
            if (ddlProgramOne.SelectedValue == "0")
            {
                program = "0,0".Split(',');
            }
            else
            {
                program = ddlProgramOne.SelectedValue.Split(',');
            }
            string Userno = "";
            foreach (ListViewDataItem items in LvProgramList.Items)
            {
                CheckBox Chk = items.FindControl("chktransfer") as CheckBox;
                if (Chk.Checked == true)
                {
                    Userno += Chk.ToolTip + "$";
                    count++;
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(this.updPgmResult, "Please Selct at least one checkbox!", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
                return;
            }
            Userno = Userno.TrimEnd('$');
            string Time = "";
            Time =(txtfrom.Text + '-' + txtTo.Text);
            string SP_Name1 = "PKG_ACD_GET_INSERT_PROGRAM_BASED_INTERVIEW_SCHEDULE";
            string SP_Parameters1 = "@P_ADMBATCH,@P_STUDY_LEVEL,@P_DEGREENO,@P_BRANCHNO,@P_DATE,@P_TIME,@P_VENUE,@P_USERNO,@P_UA_NO,@P_COMMAND_TYPE,@P_OUTPUT";
            string Call_Values1 = "" + Convert.ToInt32(ddlIntakeOne.SelectedValue) + "," + Convert.ToInt32(ddlStudyOne.SelectedValue) + "," + Convert.ToInt32(program[0]) + "," + Convert.ToInt32(program[1]) + "," + TxtDate.Text + "," + Time + "," + TxtVenue.Text + "," + Userno + "," + Convert.ToInt32(Session["userno"]) + "," + 2 + ",0";
            string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//insert
            if (que_out1 == "1")
            {
                objCommon.DisplayMessage(this.updPgmResult, "Record Saved Successfully!", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
                TxtDate.Text = "";
                txtfrom.Text = "";
                txtTo.Text = "";
                TxtVenue.Text = "";
                BindData();
                return;
            }
            if (que_out1 == "3")
            {
                objCommon.DisplayMessage(this.updPgmResult, "Schedule not updated beacuse result will be declare!", this.Page);
                BindData();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            }
            else
            {
                objCommon.DisplayMessage(this.updPgmResult, "Error!", this.Page);
                BindData();
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
    protected void ddlIntakeOne_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStudyOne.SelectedIndex = 0;
        ddlStudyOne.SelectedIndex = 0;
        ddlProgramOne.SelectedIndex = 0;
        Panel1.Visible = false;
        btnSubmitOne.Visible = false;
        LvProgramList.DataSource = null;
        LvProgramList.DataBind();
    }
    protected void ddlProgramOne_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        btnSubmitOne.Visible = false;
        LvProgramList.DataSource = null;
        LvProgramList.DataBind();
    }
    protected void ddlIntake_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStudyLevel.SelectedIndex = 0;
        ddlPreference.SelectedIndex = 0;
        ddlResult.SelectedIndex = 0;

        Panel1.Visible = false;
        LvProgramList.DataSource = null;
        LvProgramList.DataBind();
    }
    protected void ddlStudyLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPreference.SelectedIndex = 0;
        ddlResult.SelectedIndex = 0;
    }
    private void BindDataTwo()
    {
        try
        {
            panel11.Visible = false;
            LvUniBased.DataSource = null;
            LvUniBased.DataBind();
            ViewState["DYNAMIC_DATASET"] = null;
            string SP_Name3 = "PKG_ACD_GET_INSERT_PROGRAM_BASED_RESULT";
            string SP_Parameters3 = "@P_INTAKE,@P_STUDY_LEVEL,@P_BRANCH_PREF,@P_COMMAND_TYPE,@P_OUTPUT";
            string Call_Values3 = "" + Convert.ToInt32(ddlIntake.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlStudyLevel.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlPreference.SelectedValue) + "," + 1 + "," + 0 + "";
            DataSet Ds = objCommon.DynamicSPCall_Select(SP_Name3, SP_Parameters3, Call_Values3);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test6();", true);
                //Panel1.Visible = true;
                btnSubmit.Visible = true;
                //btnOffer.Visible = true;
                panel11.Visible = true;
                LvUniBased.DataSource = Ds;
                LvUniBased.DataBind();
                ViewState["DYNAMIC_DATASET"] = Ds.Tables[0];
            }
            else
            {
                //Panel1.Visible = false;
                btnSubmit.Visible = false;
                btnOffer.Visible = false;
                panel11.Visible = false;
                LvUniBased.DataSource = null;
                LvUniBased.DataBind();
                objCommon.DisplayMessage(this.updPgmResultTwo, "No Record Found!", this.Page);
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
                    //lblresult1.Text = "-";
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
                    //lblresult2.Text = "-";
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
                    //lblresult3.Text = "-";
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
    protected void OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        (LvUniBased.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["DYNAMIC_DATASET"];
        panel11.Visible = true;
        LvUniBased.DataSource = dt;
        LvUniBased.DataBind();
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test6();", true);
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindDataTwo();
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
                objCommon.DisplayMessage(this.updPgmResultTwo, "Please Selct at least one checkbox!", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test6();", true);
                return;
            }
            if (Blank_Count == count)
            {
                objCommon.DisplayMessage(this.updPgmResultTwo, "Result not updated for Blank Preference!", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test6();", true);
                return;
            }
            Userno = Userno.TrimEnd('$');
            string SP_Name1 = "PKG_ACD_GET_INSERT_PROGRAM_BASED_RESULT";
            string SP_Parameters1 = "@P_INTAKE,@P_STUDY_LEVEL,@P_RESULT,@P_USERNO,@P_COMMAND_TYPE,@P_BRANCH_PREF,@P_UA_NO,@P_OUTPUT";
            string Call_Values1 = "" + Convert.ToInt32(ddlIntake.SelectedValue) + "," + Convert.ToInt32(ddlStudyLevel.SelectedValue) + "," +
                Convert.ToInt32(ddlResult.SelectedValue) + "," + Userno + "," + 2 + "," + Convert.ToInt32(ddlPreference.SelectedValue) + "," + Convert.ToInt32(Session["userno"]) + ",0";

            string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//insert
            if (que_out1 == "1")
            {
                objCommon.DisplayMessage(this.updPgmResultTwo, "Record Saved Successfully!", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test6();", true);
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
                            { Program = lblprogram2.Text;  }
                            else if (ddlPreference.SelectedValue == "3")
                            { Program = lblProgram3.Text; }

                            EmaiLDiffered(3598, hdfEmailid.Value, lblStudname.Text, Program, hdfintake.Value);
                        }
                    }
                }
                if (ddlResult.SelectedValue == "1")
                {
                    OffereSend();
                }
                ddlPreference.SelectedIndex = 0;
                ddlResult.SelectedIndex = 0;
                BindDataTwo();          
                return;
            }
            else if (que_out1 == "3")
            {
                objCommon.DisplayMessage(this.updPgmResultTwo, "Screen Configuration not Define for Program!", this.Page);
                ddlPreference.SelectedIndex = 0;
                ddlResult.SelectedIndex = 0;
                BindDataTwo();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test6();", true);
                return;
            }
            else if (que_out1 == "4")
            {
                objCommon.DisplayMessage(this.updPgmResultTwo, "Result cannot update beacuse Deposite slip already upload!", this.Page);
                ddlPreference.SelectedIndex = 0;
                ddlResult.SelectedIndex = 0;
                BindData();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test6();", true);
                return;
            }
            else if (que_out1 == "7")
            {
                objCommon.DisplayMessage(this.updPgmResultTwo, "Result cannot update beacuse Schedule not define!", this.Page);
                ddlPreference.SelectedIndex = 0;
                ddlResult.SelectedIndex = 0;
                BindData();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test6();", true);
                return;
            }
            else if (que_out1 == "8")
            {
                objCommon.DisplayMessage(this.updPgmResultTwo, "Preference 1 result is not declared yet!", this.Page);
                ddlPreference.SelectedIndex = 0;
                ddlResult.SelectedIndex = 0;
                BindData();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test6();", true);
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.updPgmResultTwo, "Error!", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test6();", true);
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
    protected void EmaiLDiffered(int Page_No, string toSendAddre, string Name, string program ,string Yearname)
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
            //if (Res == "Email Send Succesfully")
            //{
            //    objCommon.DisplayMessage(this.updPgmResult, "Email Send Successfully!!", this.Page);
            //}
            //else
            //{
            //    objCommon.DisplayMessage(this.updPgmResult, "Error!!", this.Page);
            //}
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlIntake.SelectedIndex = 0;
        ddlStudyLevel.SelectedIndex = 0;
        ddlPreference.SelectedIndex = 0;
        ddlResult.SelectedIndex = 0;
        btnSubmit.Visible = false;
        btnOffer.Visible = false;
        panel11.Visible = false;
        LvUniBased.DataSource = null;
        LvUniBased.DataBind();
    }
    protected void btnOffer_Click(object sender, EventArgs e)
    {
        OffereSend();

    }
    private void OffereSend()
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
            HiddenField hdfadmfee1 = items.FindControl("hdfadmfee1") as HiddenField;
            HiddenField hdfadmfee2 = items.FindControl("hdfadmfee2") as HiddenField;
            HiddenField hdfadmfee3 = items.FindControl("hdfadmfee3") as HiddenField;
            HiddenField hdfdownfee1 = items.FindControl("hdfdownfee1") as HiddenField;
            HiddenField hdfdownfee2 = items.FindControl("hdfdownfee2") as HiddenField;
            HiddenField hdfdownfee3 = items.FindControl("hdfdownfee3") as HiddenField;
            Label lblStudname = items.FindControl("lblStudname") as Label;
            HiddenField hdfEmailid = items.FindControl("hdfEmailid") as HiddenField;
            HiddenField hdfintake = items.FindControl("hdfintake") as HiddenField;
            HiddenField hdfGetdate = items.FindControl("hdfGetdate") as HiddenField;
            HiddenField hdfCollege = items.FindControl("hdfCollege") as HiddenField;
            HiddenField hdfSrno = items.FindControl("hdfSrno") as HiddenField;
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

                    Program = lblprogram1.Text;
                    if (offerstatus == "4")
                    {
                        EmaiLOffer(36181, hdfEmailid.Value, lblStudname.Text, Program, hdfintake.Value, hdfGetdate.Value, hdfCollege.Value, hdfadmfee1.Value, hdfdownfee1.Value);
                    }
                    else if (offerstatus == "2")
                    {
                        EmaiLOfferUniversity(36182, hdfEmailid.Value, lblStudname.Text, Program, hdfintake.Value, hdfGetdate.Value, CollegeName, hdfadmfee1.Value, hdfdownfee1.Value);
                        EmaiLOffer(36181, hdfEmailid.Value, lblStudname.Text, Program, hdfintake.Value, hdfGetdate.Value, hdfCollege.Value, hdfadmfee1.Value, hdfdownfee1.Value);
                    }
                }

                else if (ddlPreference.SelectedValue == "2")
                {

                    Program = lblprogram2.Text;
                    if (offerstatus == "4")
                    {
                        EmaiLOffer(36181, hdfEmailid.Value, lblStudname.Text, Program, hdfintake.Value, hdfGetdate.Value, hdfCollege.Value, hdfadmfee2.Value, hdfdownfee2.Value);
                    }
                    else if (offerstatus == "2")
                    {
                        EmaiLOfferUniversity(36182, hdfEmailid.Value, lblStudname.Text, Program, hdfintake.Value, hdfGetdate.Value, CollegeName, hdfadmfee2.Value, hdfdownfee2.Value);
                        EmaiLOffer(36181, hdfEmailid.Value, lblStudname.Text, Program, hdfintake.Value, hdfGetdate.Value, hdfCollege.Value, hdfadmfee2.Value, hdfdownfee2.Value);
                    }
                }
                else if (ddlPreference.SelectedValue == "3")
                {

                    Program = lblProgram3.Text;
                    if (offerstatus == "4")
                    {
                        EmaiLOffer(36181, hdfEmailid.Value, lblStudname.Text, Program, hdfintake.Value, hdfGetdate.Value, hdfCollege.Value, hdfadmfee3.Value, hdfdownfee3.Value);
                    }
                    else if (offerstatus == "2")
                    {
                        EmaiLOfferUniversity(36182, hdfEmailid.Value, lblStudname.Text, Program, hdfintake.Value, hdfGetdate.Value, CollegeName, hdfadmfee3.Value, hdfdownfee3.Value);
                        EmaiLOffer(36181, hdfEmailid.Value, lblStudname.Text, Program, hdfintake.Value, hdfGetdate.Value, hdfCollege.Value, hdfadmfee3.Value, hdfdownfee3.Value);
                    }
                }
                string SP_Name1 = "PKG_ACD_PROGRAM_UNIVERSITY_OFFER_SEND_LOG";
                string SP_Parameters1 = "@P_COMMAND_TYPE,@P_USERNO,@P_SRNO,@P_OUTPUT";
                string Call_Values1 = "" + 1 + "," + Convert.ToInt32(Chk.ToolTip) + "," + Convert.ToInt32(hdfSrno.Value) + ",0";
                string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//grade allotment
                Chk.Checked = false;
                //ddlPreference.SelectedIndex = 0;
            }

        }

    }
    protected void EmaiLOffer(int Page_No, string toSendAddre, string Name, string program, string Yearname, string Getdate, string College, string Admfee, string DownFee)
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
            msgsPara.Body = msgsPara.Body.Replace("{AdmFee}", Admfee.ToString());
            msgsPara.Body = msgsPara.Body.Replace("{DownFee}", DownFee.ToString());
            MemoryStream Attachment = null; string AttachmentName = "";
            Task<string> task = Email.Execute(msgsPara.Body, toSendAddress, Subject, Attachment, AttachmentName.ToString());
            Res = task.Result;
            if (Res == "Email Send Succesfully")
            {
                objCommon.DisplayMessage(this.updPgmResult, "Email Sent Successfully!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updPgmResult, "Error!", this.Page);
            }
        }
    }
     protected void EmaiLOfferUniversity(int Page_No, string toSendAddre, string Name, string program, string Yearname, string Getdate, string College ,string Admfee,string DownFee)
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
            msgsPara.Body = msgsPara.Body.Replace("{College}", College.ToString());
            msgsPara.Body = msgsPara.Body.Replace("{AdmFee}", Admfee.ToString());
            msgsPara.Body = msgsPara.Body.Replace("{DownFee}", DownFee.ToString());
            MemoryStream Attachment = null; string AttachmentName = "";
            Task<string> task = Email.Execute(msgsPara.Body, toSendAddress, Subject, Attachment, AttachmentName.ToString());
            Res = task.Result;
            if (Res == "Email Send Succesfully")
            {
                objCommon.DisplayMessage(this.updPgmResult, "Email Sent Successfully!", this.Page);
                BindData();
            }
            else
            {
                objCommon.DisplayMessage(this.updPgmResult, "Error!", this.Page);
            }
        }
    }

     protected void FilterData1_TextChanged(object sender, EventArgs e)
     {
         try
         {
             string[] program;
             if (ddlProgramOne.SelectedValue == "0")
             {
                 program = "0,0".Split(',');
             }
             else
             {
                 program = ddlProgramOne.SelectedValue.Split(',');
             }
             string SP_Name3 = "PKG_ACD_GET_INSERT_PROGRAM_BASED_INTERVIEW_SCHEDULE";
             string SP_Parameters3 = "@P_ADMBATCH,@P_STUDY_LEVEL,@P_DEGREENO,@P_BRANCHNO,@P_COMMAND_TYPE,@P_USERNO,@P_OUTPUT";
             string Call_Values3 = "" + Convert.ToInt32(ddlIntakeOne.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlStudyOne.SelectedValue.ToString()) + "," + Convert.ToInt32(program[0]) + "," + Convert.ToInt32(program[1]) + "," + 1 + "," +FilterData1.Text + "," + 0 + "";
             DataSet Ds = objCommon.DynamicSPCall_Select(SP_Name3, SP_Parameters3, Call_Values3);
             if (Ds.Tables[0].Rows.Count > 0)
             {
                 ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
                 Panel1.Visible = true;
                 btnSendSchedule.Visible = true;
                 btnSubmitOne.Visible = true;
                 LvProgramList.DataSource = Ds;
                 LvProgramList.DataBind();
             }
             else
             {
                 Panel1.Visible = false;
                 btnSendSchedule.Visible = false;
                 btnSubmitOne.Visible = false;
                 LvProgramList.DataSource = null;
                 LvProgramList.DataBind();
                 objCommon.DisplayMessage(this.updPgmResult, "No Record Found!", this.Page);
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


     protected void FilterData2_TextChanged(object sender, EventArgs e)
     {
         try
         {

             LvUniBased.DataSource = null;
             LvUniBased.DataBind();
             ViewState["DYNAMIC_DATASET"] = null;
             string SP_Name3 = "PKG_ACD_GET_INSERT_PROGRAM_BASED_RESULT";
             string SP_Parameters3 = "@P_INTAKE,@P_STUDY_LEVEL,@P_BRANCH_PREF,@P_COMMAND_TYPE,@P_USERNO,@P_OUTPUT";
             string Call_Values3 = "" + Convert.ToInt32(ddlIntake.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlStudyLevel.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlPreference.SelectedValue) + "," + 1 + "," + FilterData2.Text + "," + 0 + "";
             DataSet Ds = objCommon.DynamicSPCall_Select(SP_Name3, SP_Parameters3, Call_Values3);
             if (Ds.Tables[0].Rows.Count > 0)
             {
                 ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
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
                 btnSubmit.Visible = false;
                 LvUniBased.DataSource = null;
                 LvUniBased.DataBind();
                 objCommon.DisplayMessage(this.updPgmResultTwo, "No Record Found!", this.Page);
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