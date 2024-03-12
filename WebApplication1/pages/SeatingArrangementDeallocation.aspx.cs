//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : Examination                                                             
// PAGE NAME     : Seating Arrangement Deallocation                                                         
// CREATION DATE : 10-FEB-2012                                                        3
// CREATED BY    : UMESH K. GANORKAR                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections;
public partial class ACADEMIC_SeatingArrangementDeallocation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    SeatingController objSc = new SeatingController();

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
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                this.CheckPageAuthorization();

                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                this.PopulateDropDown();
                divMsg.InnerHtml = string.Empty;
            }
        }
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SeatingArrangementDeallocation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SeatingArrangementDeallocation.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            //Term
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");

            //Exam Slot
            objCommon.FillDropDownList(ddlSlot, "ACD_EXAM_TT_SLOT", "SLOTNO", "SLOTNAME", "SLOTNO > 0", "SLOTNO");

            //Exam Slot
            objCommon.FillDropDownList(ddlRoom, "ACD_ROOM", "ROOMNO", "ROOMNAME", "ROOMNO > 0", "ROOMNO");


            for (int i = 1; i <= 80; i++)
            {
                ddlDay.Items.Add(new ListItem(i.ToString()));
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.PopulateDropDown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTotStud.Text = "0";
    }

    protected void ddlExTTType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlDay_SelectedIndexChanged(object sender, EventArgs e)
    {
        //we gets days no. as well as Exam dates on  LvDates bindlistview 
        DataSet dsBC = objExamController.BindDate(Convert.ToInt32(ddlDay.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlExTTType.SelectedValue));
        if (dsBC != null && dsBC.Tables[0].Rows.Count > 0)
        {
            txtExamDate.Text = dsBC.Tables[0].Rows[0][0].ToString();

            txtExamDate.Enabled = false;
            ddlSlot.SelectedIndex = 0;
            ddlRoom.SelectedIndex = 0;
        }
        else
        {
            txtExamDate.Text = "";
            txtExamDate.Enabled = true;
            ddlSlot.SelectedIndex = 0;
            ddlRoom.SelectedIndex = 0;
        }
    }

    protected void ddlSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCoursesList();
        ddlRoom.SelectedIndex = 0;


    }

    private void BindCoursesList()
    {
        try
        {

            DataSet ds = objSc.GetCoursesFor_SeatDeallocation(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToString(txtExamDate.Text), Convert.ToInt32(ddlSlot.SelectedValue));//, Convert.ToInt32(ddlExTTType.SelectedValue)
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlCourse.Items.Clear();
                    ddlCourse.Items.Add(new ListItem("Please Select", "0"));
                    ddlCourse.DataTextField = "COURSE";
                    ddlCourse.DataValueField = "COURSENO";
                    ddlCourse.DataSource = ds.Tables[0];
                    ddlCourse.DataBind();
                }
                else
                {
                    ddlCourse.Items.Clear();
                    ddlCourse.Items.Add(new ListItem("Please Select", "0"));

                    objCommon.DisplayMessage("No Course Found!", this.Page);
                    ddlCourse.Focus();
                }
            }
            else
            {
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_teacherallotment.ddlDepartment_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfTot.Value = "0";
        txtTotStud.Text = "0";
        ddlRoom.SelectedIndex = 0;

        if (ddlCourse.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlExTTType, "ACD_COURSE C  INNER JOIN ACD_SCHEME SS ON (SS.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_EXAM_NAME EN ON (EN.PATTERNNO=SS.PATTERNNO)", "EXAMNO", "EXAMNAME", "EXAMNAME<>'' AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue), "EXAMNO");
        }
        else
        {
            objCommon.DisplayMessage("Please Select Course!", this.Page);
            ddlCourse.Focus();
        }
    }

    private void BindStudentList()
    {
        try
        {
            try
            {
                DataSet ds = objSc.GetStudentsForSeatingArrangementDeallocation(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlExTTType.SelectedValue), txtExamDate.Text, Convert.ToInt32(ddlSlot.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlRoom.SelectedValue));//, Convert.ToInt32(ddlDay.SelectedValue)
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lvStudent.DataSource = ds.Tables[0];
                        lvStudent.DataBind();
                        int i = 0;
                        foreach (ListViewDataItem item in lvStudent.Items)
                        {
                            CheckBox cbrow = item.FindControl("cbRow") as CheckBox;
                            {
                                cbrow.Checked = false;
                            }
                            i++;
                        }
                        hdfTot.Value = ds.Tables[0].Rows.Count.ToString();
                    }
                    else
                    {
                        lvStudent.DataSource = null;
                        lvStudent.DataBind();
                        hdfTot.Value = "0";
                    }
                }
                else
                {
                    objCommon.DisplayMessage("No Student Found For This selection!", this.Page);
                }

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Academic_Examination.BindListView-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_teacherallotment.BindStudentList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlRoom_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindStudentList();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string idnos = string.Empty;
            string regnos = string.Empty;
            int i = 0;

            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("cbRow") as CheckBox;
                HiddenField regno = item.FindControl("hdfRegno") as HiddenField;
                if (i < Convert.ToInt32(txtTotStud.Text))
                    chk.Checked = true;

                i++;

                if (chk.Checked)
                {
                    idnos += chk.ToolTip + ",";
                    regnos += regno.Value + ",";
                }
            }
            if (idnos == string.Empty || regnos == string.Empty)
            {
                objCommon.DisplayMessage(this.updExamdate, "Please Select Students from List..", this.Page);
                return;
            }
            CustomStatus cs = (CustomStatus)objSc.SeatArrangementDeallocation(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlExTTType.SelectedValue), Convert.ToInt32(ddlDay.SelectedValue), Convert.ToDateTime(txtExamDate.Text), Convert.ToInt32(ddlSlot.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlRoom.SelectedValue), idnos, regnos, Session["colcode"].ToString());
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updExamdate, "Seating Arrangement Deallocation Done...!!", this.Page);

                txtTotStud.Text = string.Empty;
                lvStudent.DataSource = null;
                lvStudent.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(this.updExamdate, "Error while Seating Arrangement Deallocation..", this.Page);
            }

            this.BindStudentList();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_EndSemesterAttendanceSheet.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("SeatingArrangement", "rptSeatArrangementDeallocation.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CoursewiseStudentReport2.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_EXAMNO=" + ddlExTTType.SelectedValue + ",@P_DAYNO=" + ddlDay.SelectedValue + ",@P_EXAMDATE=" + txtExamDate.Text + ",@P_SLOTNO=" + ddlSlot.SelectedValue + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_ROOMNO=" + ddlRoom.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ROOMNAME=" + ddlRoom.SelectedItem.Text + " ";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updExamdate, this.updExamdate.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}
