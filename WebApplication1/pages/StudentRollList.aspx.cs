//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : STUDENT ROLL AND SUBJECT WISE ROLL LIST                               
// CREATION DATE : 27-OCT-2010                                                          
// CREATED BY    : MANGESH MOHATKAR                                                  
// MODIFIED DATE : 17-JAN-2011                                                                     
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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Academic_StudentRoolist : System.Web.UI.Page
{
    IITMS.UAIMS.Common objCommon = new IITMS.UAIMS.Common();
    IITMS.UAIMS_Common objUCommon = new IITMS.UAIMS_Common();

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
                    CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    PopulateDropdown();

                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Student_RollList_Report", "StudentRollList.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void PopulateDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNO");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            objCommon.FillDropDownList(ddlSessionNo, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.PopulateDropdown()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
            ddlBranch.Focus();

            ddlSchemeNo.Items.Clear();
            ddlSchemeNo.Items.Add("Please Select");
            ddlBranch.Focus();
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlSchemeNo.Items.Clear();
            objCommon.FillDropDownList(ddlSchemeNo, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO");
            ddlSchemeNo.Focus();
            ddlSchemeNo.Focus();
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=SESSIONNAME=" + Session["sessionname"].ToString() + ",@p_college_code=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSessionNo.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlSchemeNo.SelectedValue) + ",@username=" + Session["username"].ToString() + ",IPADDRESS=" + Request.ServerVariables["REMOTE_HOST"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlSchemeNo.SelectedIndex = 0;
            ddlSessionNo.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSubjectReport_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objSController = new StudentController();
            DataSet ds = objSController.GetStudentSubjectsOffered(Convert.ToInt32(ddlSessionNo.SelectedValue), Convert.ToInt32(ddlSchemeNo.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue));
            if (ds.Tables.Count <= 0)
            {
                ShowMessage("No Students found for current selection");
                return;
            }
            else
            {
                if (ddlBranch.SelectedValue == "99")
                    ShowReportForStudentSubjectsOfferedFirstYear("STUDENT_SUBJECTS_OFFERED", "StudentOfferedCourses2FirstYear.rpt");
                else
                    ShowReportForStudentSubjectsOffered("STUDENT_SUBJECTS_OFFERED", "StudentOfferedCourses2.rpt");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.btnSubjectReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportForStudentSubjectsOffered(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&pathForStudentSubjects=~,Reports,Academic," + rptFileName + "&@P_SESSIONNO=" + Convert.ToInt32(ddlSessionNo.SelectedValue) + "&@P_SCHEMENO=" + Convert.ToInt32(ddlSchemeNo.SelectedValue) + "&@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "&@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);
            url += "&paramForStudentSubjects=collegename=" + Session["coll_name"].ToString() + ",username=" + Session["userfullname"].ToString() + ",@P_SESSIONYEAR=" + ddlSessionNo.SelectedItem.Text;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportForStudentSubjectsOfferedFirstYear(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&pathForStudentSubjects=~,Reports,Academic," + rptFileName + "&@P_SESSIONNO=" + Convert.ToInt32(ddlSessionNo.SelectedValue) + "&@P_SCHEMENO=" + Convert.ToInt32(ddlSchemeNo.SelectedValue) + "&@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "&@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue);
            url += "&paramForStudentSubjects=collegename=" + Session["coll_name"].ToString() + ",username=" + Session["userfullname"].ToString() + ",@P_SESSIONYEAR=" + ddlSessionNo.SelectedItem.Text + ",@section=" + ddlSection.SelectedValue;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportForExamRegistered(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&pathForStudentExamSubjects=~,Reports,Academic," + rptFileName + "&@P_SESSIONNO=" + Convert.ToInt32(ddlSessionNo.SelectedValue) + "&@P_SCHEMENO=" + Convert.ToInt32(ddlSchemeNo.SelectedValue) + "&@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue);
            url += "&paramForStudentExamSubjects=username=" + Session["userfullname"].ToString() + ",@P_SESSIONYEAR=" + ddlSessionNo.SelectedItem.Text;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentIDCard.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentIDCard.aspx");
        }
    }

    protected void btnExamReport_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objSController = new StudentController();
            DataSet ds = objSController.GetStudentExamSubjectsRegistered(Convert.ToInt32(ddlSessionNo.SelectedValue), Convert.ToInt32(ddlSchemeNo.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));
            if (ds.Tables.Count <= 0)
            {
                ShowMessage("No Students found for current selection");
                return;
            }
            else
                ShowReportForExamRegistered("STUDENT_EXAM_REGISTERED", "StudentExamOfferedCourses.rpt");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.btnExamReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            if (ddlBranch.SelectedValue == "99")
                objCommon.FillDropDownList(ddlSection, "ACD_STUDENT S INNER JOIN ACD_SECTION SC ON (S.SECTIONNO = SC.SECTIONNO)", "DISTINCT SC.SECTIONNO", "SC.SECTIONNAME", "S.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.SCHEMENO = " + ddlSchemeNo.SelectedValue, "SC.SECTIONNO");
            else
            {
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
            }
            ddlSection.Focus();
        }
        else
        {
            ddlSection.Items.Clear();
            ddlSemester.SelectedIndex = 0;
        }
    }
}
