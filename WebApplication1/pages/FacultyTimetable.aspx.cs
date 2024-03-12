//=================================================================================
// PROJECT NAME  : UAIMS [GHRCE]                                                         
// MODULE NAME   : TIMETABLE REPORT [ROOMWISE & FACULTYWISE]                                              
// CREATION DATE : 28-SEP-2011
// CREATED BY    : UMESH K. GANORKAR                               
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

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

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Academic_FacultyTimetable : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    #region Page Event
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
                trFaculty.Visible = false;
                this.PopulateDropDownList();
            }

            if (Convert.ToInt32(Session["usertype"]) == 1 || Convert.ToInt32(Session["usertype"]) == 3)
            {
                updTimeTable.Visible = true;
            }
            else
            {
                updTimeTable.Visible = false;
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
                Response.Redirect("~/notauthorized.aspx?page=RoomwiseTimetable.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RoomwiseTimetable.aspx");
        }
    }
    #endregion Page Event

    #region DropDownList

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSessionAuto, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");
            if (Convert.ToInt16(Session["usertype"]) == 1)
            {
                trFaculty.Visible = true;
            }
            else
            {
                if (Convert.ToInt16(Session["usertype"]) == 3)
                {
                    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEPTNO= " + Session["userdeptno"] + " ", "SCHEMENO DESC");
                }
            }
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_TIMETABLE_RoomwiseTimetable.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion DropDownList

    #region CLICK EVENT
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlSessionAuto.SelectedIndex = 0;
        ddlFaculty.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
    }

    protected void btnFacultyCancel_Click(object sender, EventArgs e)
    {
        ddlSessionAuto.SelectedIndex = 0;
        ddlFaculty.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
    }
    protected void btnFacultyReport_Click(object sender, EventArgs e)
    {
        ShowReportFaculty("TimeTable_Report", "rptFacultywiseTimetable.rpt");
    }
    #endregion CLICK EVENT

    #region Show Report


    private void ShowReportFaculty(string reportTitle, string rptFileName)
    {
        try
        {
            //Shows report Roomwise
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            if (Convert.ToInt16(Session["usertype"]) == 3)
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSessionAuto.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SECTIONNO=" + ddlSection.SelectedValue + ",@V_VERSION=2,@P_UA_NO=" + Convert.ToInt16(Session["userno"]);
            }
            else
            {
                if (Convert.ToInt16(Session["usertype"]) == 1)
                {
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSessionAuto.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SECTIONNO=" + ddlSection.SelectedValue + ",@V_VERSION=2,@P_UA_NO=" + ddlFaculty.SelectedValue;
                }
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updTimeTable, this.updTimeTable.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_TIMETABLE_RoomwiseTimetable.ShowReportFaculty() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion Show Report

    protected void ddlSemester_SelectedIndexChanged1(object sender, EventArgs e)
    {
        ddlSection.SelectedIndex = 0;
        objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + ddlScheme.SelectedValue + " AND SR.SEMESTERNO =" + ddlSemester.SelectedValue + " AND SR.SECTIONNO > 0", "SC.SECTIONNAME");
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFaculty.SelectedIndex > 0)
        {
            if (Convert.ToInt16(Session["usertype"]) == 1)
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME S INNER JOIN ACD_COURSE_TEACHER CT ON (CT.SCHEMENO = S.SCHEMENO)", "DISTINCT S.SCHEMENO", "SCHEMENAME", "S.SCHEMENO >0 AND " + ddlFaculty.SelectedValue + " IN (SELECT VALUE FROM DBO.SPLIT(CT.ADTEACHER,','))", "S.SCHEMENO DESC");
            else
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME S INNER JOIN ACD_COURSE_TEACHER CT ON (CT.SCHEMENO = S.SCHEMENO)", "DISTINCT S.SCHEMENO", "SCHEMENAME", "S.SCHEMENO >0 AND " + Session["userno"].ToString() + " IN (SELECT VALUE FROM DBO.SPLIT(CT.ADTEACHER,',')) AND S.DEPTNO= " + Session["userdeptno"].ToString(), "S.SCHEMENO DESC");
        }
    }

    protected void ddlSessionAuto_SelectedIndexChanged1(object sender, EventArgs e)
    {
        //For Faculty DropDownList in Facultywise tab...
        if (Convert.ToInt32(Session["usertype"]) == 1)
        {
            objCommon.FillDropDownList(ddlFaculty, "USER_ACC A INNER JOIN ACD_STUDENT_RESULT B ON (A.UA_NO = B.UA_NO)", "DISTINCT A.UA_NO", "UA_FULLNAME", "B.UA_NO > 0 AND B.SESSIONNO = " + ddlSessionAuto.SelectedValue, "A.UA_NO");
            ddlFaculty.Focus();
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                ddlSemester.Items.Clear();
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.SCHEMENO=" + ddlScheme.SelectedValue, "SM.SEMESTERNO");
                ddlSemester.Focus();
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
            }
            else
            {
                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Please Select", "0"));
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_BulkRegistration.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
