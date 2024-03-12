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
using System.Net;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_Course_Equivalance : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
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
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Page Authorization
                    this.CheckPageAuthorization();
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    string host = Dns.GetHostName();
                    IPHostEntry ip = Dns.GetHostEntry(host);
                    ViewState["ipAddress"] = ip.AddressList[0].ToString();
                }
                FillDropdown();
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_Course_Equivalance.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=Course_Equivalance.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Course_Equivalance.aspx");
        }
    }

    private void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND FLOCK=1", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0 AND DEGREENO > 0", "DEGREENO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_Course_Equivalance.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region "SelectedIndexChanged"
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_Course_Equivalance.ddlSession_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex <= 0)
            {
                ddlBranch.SelectedIndex = 0;
                ddlScheme.SelectedIndex = 0;
                ddlSemester.SelectedIndex = 0;

            }
            // Fills the Branch DropDown for the specific Branch assigned to the teacher.
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");
            ddlBranch.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_Course_Equivalance.ddlDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex <= 0)
            {
                ddlScheme.SelectedIndex = 0;
                ddlSemester.SelectedIndex = 0;
            }
            else
            {
                this.BindEquivalanceCourse();
            }
            //FOR DEGREE IS BE AND BRANCH IS FIRST YEAR SHOW TWO SCHEMES ONLY ie. RTM AND AUTONOMOUS.
            if (ddlDegree.SelectedValue == "1" && ddlBranch.SelectedValue == "99")
            {
                ddlScheme.Items.Clear();
                ddlScheme.Items.Add(new ListItem("Please Select", "0"));
                ddlScheme.Items.Add(new ListItem("FIRST YEAR[AUTONOMOUS]", "1"));
                ddlScheme.Focus();

            }
            else
            {
                // FOR OTHER THAN BRANCH FIRST YEAR BE FILL SCHEMES FROM ACD_SCHEME
                objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B INNER JOIN ACD_SCHEME S ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SCHEMETYPE = 1", "B.BRANCHNO");
                ddlScheme.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_Course_Equivalance.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex <= 0)
            {
                ddlSemester.SelectedIndex = 0;
            }
            //IF SCHEME IS FIRSE YEAR RTM, THEN SHOW ONLY IST AND SECOND SEMESTER
            if (ddlScheme.SelectedItem.Text == "FIRST YEAR [R.T.M]")
            {
                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Please Select", "0"));
                ddlSemester.Items.Add(new ListItem("I", "1"));
                ddlSemester.Items.Add(new ListItem("II", "2"));
            }
            else
            {
                //SHOWS ALL THE SEMESTERS FORM ACD_SEMESTER TABLE 
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_Course_Equivalance.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");

        }

    }

    #endregion  "SelectedIndexChanged"

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlCourse_auto, "ACD_COURSE", "COURSENO", "CCODE +'-'+ COURSE_NAME AS COURSE_NAME", " SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "COURSENO");
        }
    }
    protected void ddlCourse_auto_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCourse_auto.SelectedIndex > 0)
        {
            DataSet ds = objCommon.FillDropDown("ACD_COURSE", "COURSENO", "CCODE +'-'+ COURSE_NAME AS COURSE_NAME, DBO.FN_DESC('SEMESTER', SEMESTERNO) SEMESTERNAME, SEMESTERNO, SCHEMENO,CCODE", " SCHEMENO IN (SELECT SCHEMENO FROM ACD_SCHEME WHERE BRANCHNO = " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SCHEMETYPE = 2)", " SEMESTERNO, COURSENO");

            lvNITCourse.DataSource = ds;
            lvNITCourse.DataBind();

            pnlRtmCourse.Visible = true;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string Old_Ccode = string.Empty;
        string New_Ccode = string.Empty;
        string New_Courseno = string.Empty;
        int New_schemeno = 0;
        int Course = 0;
        int Old_Schemeno = Convert.ToInt32(ddlScheme.SelectedValue);
        int Old_Courseno = Convert.ToInt32(ddlCourse_auto.SelectedValue);
        string[] ccode = ddlCourse_auto.SelectedItem.Text.Split('-');
        string old_ccode = ccode[0].ToString();

        foreach (ListViewDataItem item in lvNITCourse.Items)
        {
            CheckBox chkSelect = item.FindControl("chkSelect") as CheckBox;
            Label lblCourse = item.FindControl("lblCourse") as Label;
            Label lblSemester = item.FindControl("lblSemester") as Label;
            Label lblScheme = item.FindControl("lblScheme") as Label;
            if (chkSelect.Checked == true)
            {
                New_Courseno = New_Courseno + lblCourse.ToolTip.ToString() + ",";

                New_Ccode = New_Ccode + lblScheme.ToolTip.ToString() + ",";
                New_schemeno = Convert.ToInt32(lblScheme.Text.ToString());
                Course++;
            }
        }
        if (chkNonequi.Checked == false)
        {
            if (New_Courseno != "")
            {
                if (New_Courseno.Substring(New_Courseno.Length - 1) == ",")
                    New_Courseno = New_Courseno.Substring(0, New_Courseno.Length - 1);
            }

            if (New_Ccode != "")
            {
                if (New_Ccode.Substring(New_Ccode.Length - 1) == ",")
                    New_Ccode = New_Ccode.Substring(0, New_Ccode.Length - 1);
            }

        }
        else
        {
            New_Courseno = "0";
            New_Ccode = "0";
            Course = 1;
        }
        CustomStatus result = CustomStatus.Others;
        if (New_Courseno == "")
        {
            objCommon.DisplayMessage(UpdatePanel1, "Please Select Equivalence Course!", this.Page);
        }
        else
        {
            if (Course == 1)
            {
                result = (CustomStatus)objExamController.AddEquivalanceCourses(Old_Schemeno, New_schemeno, New_Ccode, New_Courseno, old_ccode, Old_Courseno, Session["colcode"].ToString(), ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"]));

                if (result == CustomStatus.RecordSaved)
                    objCommon.DisplayMessage(UpdatePanel1, "Record Saved successfully!", this.Page);
                else
                    objCommon.DisplayMessage(UpdatePanel1, "Record Saved failed!", this.Page);

                this.BindEquivalanceCourse();
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Please Select Only One NIT Equivalent Course!", this.Page);
            }
        }
    }


    private void BindEquivalanceCourse()
    {
        DataSet ds = objExamController.GetEquivalanceCourse(Convert.ToInt32(ddlBranch.SelectedValue));
        lvEquivalanceCourse.DataSource = ds;
        lvEquivalanceCourse.DataBind();
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Equivalance_Course_List_Report", "rptEquivalance_Course_List.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_Course_Equivalance.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void chkNonequi_CheckedChanged(object sender, EventArgs e)
    {
        if (chkNonequi.Checked == true)
        {
            lvNITCourse.Enabled = false;
        }
        else
        {
            lvNITCourse.Enabled = true;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {

        int i = 0;
        foreach (ListViewDataItem item in lvEquivalanceCourse.Items)
        {
            CheckBox chkCourse = item.FindControl("chkCourse") as CheckBox;
            Label lblCourse = item.FindControl("lbl") as Label;
            if (chkCourse.Checked == true)
            {
                int a = Convert.ToInt32(chkCourse.ToolTip.ToString());
                string b = lblCourse.Text;
                int c = Convert.ToInt32(b);
                CustomStatus cs = (CustomStatus)objExamController.DeleteEquivalence(a, c);
                i++;
            }
        }
        if (i > 0)
        {
            objCommon.DisplayMessage(this.UpdatePanel1, "Record Deleted Successfully", this.Page);
            this.BindEquivalanceCourse();

        }
        else
        {
            objCommon.DisplayMessage(this.UpdatePanel1, "Please Select the Record to be Deleted!!", this.Page);
        }
    }
}
