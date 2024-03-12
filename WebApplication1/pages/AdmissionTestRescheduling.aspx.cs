// MODULE NAME   : Admission
// PAGE NAME     : AdmissionTestRescheduling.ASPX
// CREATION DATE : 05-June-2023
// CREATED BY    : Roshan Patil
// MODIFIED BY   :
// MODIFIED DATE  :
// MODIFIED DESC :
//======================================================================================
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdmissionTestRescheduling : System.Web.UI.Page
{
    private Common objCommon = new Common();
    private UAIMS_Common objUCommon = new UAIMS_Common();
    private ExamSchedulingController ObjESController = null;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
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
        catch (Exception)
        {
            throw;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AdmissionTestRescheduling.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AdmissionTestRescheduling.aspx");
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

    private void FilldropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlintake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ISNULL(ACTIVE,0)=1", "BATCHNAME");
            objCommon.FillDropDownList(ddlStudyLevel, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "UA_SECTION");
            objCommon.FillDropDownList(ddlVenue, "ACD_APTITUDE_CENTER", "APTITUDE_CENTER_NO", "APTITUDE_CENTER_NAME", "APTITUDE_CENTER_NO>0 and isnull(ACTIVE,0) =1", "APTITUDE_CENTER_NAME");
            objCommon.FillDropDownList(ddlMonths, "ACD_MONTH", "MONTHNO", "MONTH", "MONTHNO>0", "MONTHNO");
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void Show()
    {
        try
        {
            string SP_Name2 = "PKG_ACD_GET_TEST_RESCHEDULE";
            string SP_Parameters2 = "@P_ADMBATCH_NO,@P_UG_PG,@P_EXAMDATE_NO,@P_VENUE_NO,@P_SCHEDULING_NO,@P_MONTH";
            string Call_Values2 = "" + Convert.ToInt32(ddlintake.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlStudyLevel.SelectedValue) + "," + Convert.ToInt32(ddlExamDate.SelectedValue)
                + "," + Convert.ToInt32(ddlVenue.SelectedValue) + "," + Convert.ToInt32(ddlTimeSlot.SelectedValue) + "," + Convert.ToInt32(ddlMonths.SelectedValue) + "";
            DataSet dsFacWiseCourseList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (dsFacWiseCourseList.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                btnHallReport.Visible = true;
                // Panel1.Visible = true;
                schedule.Visible = true;
                LvReschedule.DataSource = dsFacWiseCourseList;
                LvReschedule.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            }
            else
            {
                objCommon.DisplayMessage(this, "Record Not Found !!", this.Page);
                btnSubmit.Visible = false;
                btnHallReport.Visible = false;
                schedule.Visible = false;
                //Panel1.Visible = false;
                LvReschedule.DataSource = null;
                LvReschedule.DataBind();
                ddlintake.SelectedIndex = 0;
                ddlStudyLevel.SelectedIndex = 0;
                ddlExamDate.SelectedIndex = 0;
                ddlVenue.SelectedIndex = 0;
                ddlTimeSlot.SelectedIndex = 0;
                ddlMonths.SelectedIndex = 0;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        try
        {
            (LvReschedule.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["DYNAMIC_DATASET"];
            LvReschedule.DataSource = dt;
            LvReschedule.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test6();", true);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            Show();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0; string userno = "";
            foreach (ListViewDataItem dataitem in LvReschedule.Items)
            {
                CheckBox chk = dataitem.FindControl("chkRegister") as CheckBox;
                if (chk.Checked == true)
                {
                    count++;
                    userno += chk.ToolTip + '$';
                }
            }
            userno = userno.TrimEnd('$');
            if (count == 0)
            {
                objCommon.DisplayMessage(this, "Please Select at least one checkbox !!", this.Page);
                return;
            }
            string SP_Name1 = "PKG_INSERT_ADMISSION_RESCHEDULE";
            string SP_Parameters1 = "@P_ADMBATCH_NO,@P_UG_PG,@P_EXAMDATE_NO,@P_EXAMDATE,@P_VENUE_NO,@P_SCHEDULING_NO,@P_USERNO,@P_CREATED_BY,@P_VENUE_NAME,@P_TIME_SLOT,@P_OUTPUT";
            string Call_Values1 = "" + Convert.ToInt32(ddlintake.SelectedValue) + "," + Convert.ToInt32(ddlStudyLevel.SelectedValue) + "," + Convert.ToInt32(ddlExamDate.SelectedValue) + "," + Convert.ToString(ddlExamDate.SelectedItem)
                    + "," + Convert.ToInt32(ddlVenue.SelectedValue) + "," + Convert.ToInt32(ddlTimeSlot.SelectedValue) + "," + userno + "," + Convert.ToInt32(Session["userno"].ToString()) + "," + Convert.ToString(ddlVenue.SelectedItem) + "," +
                    Convert.ToString(ddlTimeSlot.SelectedItem) + "," + ",0";

            string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//insert
            if (que_out1 == "1")
            {
                objCommon.DisplayMessage(this, "Record Saved Successfully !!", this.Page);
                foreach (ListViewDataItem dataitem in LvReschedule.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkRegister") as CheckBox;
                    Label lblEmailid = dataitem.FindControl("lblEmailid") as Label;
                    Label lblName = dataitem.FindControl("lblName") as Label;
                    if (chk.Checked == true)
                    {
                        string TimeDate = ddlExamDate.SelectedItem.Text.ToString() + " " + ddlTimeSlot.SelectedItem.Text.ToString();
                        EmailSchedule(Convert.ToInt32(Request.QueryString["pageno"]), lblEmailid.Text, lblName.Text, TimeDate.ToString());
                    }
                }
                ddlExamDate.SelectedIndex = 0;
                ddlTimeSlot.SelectedIndex = 0;
                ddlVenue.SelectedIndex = 0;
                Show();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void EmailSchedule(int Page_No, string toSendAddre, string Name, string date)
    {
        try
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
                msgsPara.Body = msgsPara.Body.Replace("{Name}", Name.ToString());
                msgsPara.Body = msgsPara.Body.Replace("{Date}", date.ToString());
                MemoryStream Attachment = null; string AttachmentName = "";
                Task<string> task = Email.Execute(msgsPara.Body, toSendAddress, Subject, Attachment, AttachmentName.ToString());
                Res = task.Result;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void Clear()
    {
        try
        {
            ddlintake.SelectedIndex = 0;
            ddlStudyLevel.SelectedIndex = 0;
            ddlVenue.SelectedIndex = 0;
            ddlExamDate.SelectedIndex = 0;
            ddlTimeSlot.SelectedIndex = 0;
            ddlVenue.SelectedIndex = 0;
            ddlMonths.SelectedValue = "0";
            //Panel1.Visible = false;
            LvReschedule.DataSource = null;
            LvReschedule.DataBind();
            btnSubmit.Visible = false;
            btnHallReport.Visible = false;
            schedule.Visible = false;
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void ddlStudyLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlExamDate, "ACD_ADMISSION_TEST_SCHEDULING", "DISTINCT EXAMDATE_NO", "EXAM_DATE", "EXAMDATE_NO>0 AND ACTIVE=1 AND ADMBATCH_NO=" + Convert.ToInt32(ddlintake.SelectedValue) + "AND CONVERT(DATETIME,CONVERT(NVARCHAR,EXAM_DATE,23) +' ' + CONVERT(NVARCHAR,TIME_FROM,23))>=CONVERT(DATETIME,GETDATE()) AND UG_PG=" + Convert.ToInt32(ddlStudyLevel.SelectedValue), "EXAMDATE_NO DESC");
            ddlTimeSlot.SelectedIndex = 0;
            ddlVenue.SelectedIndex = 0;
            ddlMonths.SelectedValue = "0";
            //Panel1.Visible = false;
            LvReschedule.DataSource = null;
            LvReschedule.DataBind();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void ddlExamDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (ListViewDataItem dataitem in LvReschedule.Items)
            {
                CheckBox chk = dataitem.FindControl("chkRegister") as CheckBox;
                if (chk.Checked == true)
                {
                    chk.Checked = false;
                }
            }
            hdnCapacity.Value = "0";
            hdnTotal.Value = "0";

            objCommon.FillDropDownList(ddlTimeSlot, "ACD_ADMISSION_TEST_SCHEDULING", "DISTINCT SCHEDULING_NO", "(TIME_FROM + '-' + TIME_TO)TIME_SCHEDULE", "SCHEDULING_NO>0 AND ACTIVE=1 AND ADMBATCH_NO=" + Convert.ToInt32(ddlintake.SelectedValue) + "AND UG_PG=" + Convert.ToInt32(ddlStudyLevel.SelectedValue) + "AND EXAMDATE_NO=" + Convert.ToInt32(ddlExamDate.SelectedValue), "SCHEDULING_NO");
            ddlVenue.SelectedIndex = 0;
            // ddlMonths.SelectedValue = "0";

            string SP_Name2 = "PKG_ACD_GET_TEST_RESCHEDULE_CAPACITY";
            string SP_Parameters2 = "@P_ADMBATCH_NO,@P_UG_PG,@P_EXAMDATE_NO";
            string Call_Values2 = "" + Convert.ToInt32(ddlintake.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlStudyLevel.SelectedValue) + "," + Convert.ToInt32(ddlExamDate.SelectedValue) + "";
            DataSet dsFacWiseCourseList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (dsFacWiseCourseList.Tables[0].Rows.Count > 0)
            {
                int TotalCount = Convert.ToInt32(dsFacWiseCourseList.Tables[0].Rows[0]["TOTAL_STUD"].ToString());
                //hdnCapacity.Value = dsFacWiseCourseList.Tables[0].Rows[0]["TOTAL_STUD"].ToString();
                int Capacity = Convert.ToInt32(dsFacWiseCourseList.Tables[0].Rows[0]["CAPACITY"].ToString());
                hdnCapacity.Value = Capacity.ToString();// "Capacity Full";
                hdnTotal.Value = TotalCount.ToString();
            }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void ddlTimeSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlVenue, "ACD_ADMISSION_TEST_SCHEDULING", "DISTINCT VENUE_NO", "VENUE_NAME", "VENUE_NO>0 AND ACTIVE=1 AND ADMBATCH_NO=" + Convert.ToInt32(ddlintake.SelectedValue) + "AND UG_PG=" + Convert.ToInt32(ddlStudyLevel.SelectedValue) + "AND EXAMDATE_NO=" + Convert.ToInt32(ddlExamDate.SelectedValue) + "AND SCHEDULING_NO=" + Convert.ToInt32(ddlTimeSlot.SelectedValue), "VENUE_NO");
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnHallReport_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = CreateDatatable_IDNO();

            int count = 0; string userno = "";
            foreach (ListViewDataItem dataitem in LvReschedule.Items)
            {
                CheckBox chk = dataitem.FindControl("chkRegister") as CheckBox;
                if (chk.Checked == true)
                {
                    count++;
                    userno += chk.ToolTip + '$';
                    DataRow dRow = dt.NewRow();
                    dRow["USERNO"] = Convert.ToString(chk.ToolTip);
                    dt.Rows.Add(dRow);
                }
            }
            userno = userno.TrimEnd('$');
            if (count == 0)
            {
                objCommon.DisplayMessage(this, "Please Select at least one checkbox !!", this.Page);
                return;
            }

            string Url = ConfigurationManager.AppSettings["WebServer"] + "Reports/Admission_permit.aspx?UserNo=" + userno.ToString();
            Response.Write("<script type='text/javascript'>window.open('" + Url + "');</script>");
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ShowReport(string param, string reportTitle, string rptFileName, DataTable dt)
    {
        try
        {
            string colgname = "Report";
            string exporttype = "pdf";

            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];

            url += "exporttype=" + exporttype;

            url += "&filename=" + colgname + "." + exporttype;

            url += "&path=~,Reports,Academic," + "AptitudeTestTicket.rpt";
            /// url += "&path=~,Reports,Academic," + "hallticket.rpt";

            DataSet ds = new DataSet();
            // Encode the XML data as a URL parameter
            ds.Tables.Add(dt);
            Session["OfferLetterIds"] = ds;

            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_XML=" + string.Empty + ",@P_ADMBATCH=" + Convert.ToInt32(ddlDirectIntake.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDirectDegree.SelectedValue) + ",@P_ENTERANCE=" + Convert.ToInt32(0);

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_XML=" + string.Empty;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentReport1.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport1("Appicant Admission List", "ApplicantsAdmissionList.rpt");
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ShowReport1(string reportTitle, string rptFileName)
    {
        try
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@Username=" + Session["username"] + ",@P_ADMBATCH=" + Convert.ToInt32(ddlintake.SelectedValue) + ",@P_UGPGOT=" + Convert.ToInt32(ddlStudyLevel.SelectedValue) + ",@P_EXAMDATE=" + Convert.ToInt32(ddlExamDate.SelectedValue) + ",@P_VENUE_NO=" + Convert.ToInt32(ddlVenue.SelectedValue) + ",@P_SCHEDULING_NO=" + Convert.ToInt32(ddlTimeSlot.SelectedValue);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updTestReSchedule, this.updTestReSchedule.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentReport1.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlintake_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LvReschedule.DataSource = null;
            LvReschedule.DataBind();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private DataTable CreateDatatable_IDNO()
    {
        DataTable dt = new DataTable();
        try
        {
            dt.TableName = "Student_USERNO";
            dt.Columns.Add("USERNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PDPGrading.CreateDatatable_GradeScheme() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dt;
    }
}