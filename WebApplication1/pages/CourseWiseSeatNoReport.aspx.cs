//======================================================================================
// PROJECT NAME  : GEC UAIMS                                                            
// MODULE NAME   : COURSE WISE STUDENT SEAT NO REPOPRT                                  
// CREATION DATE : 08-MAY-2010                                                          
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class ACADEMIC_CourseWiseSeatNoReport : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                if (Request.QueryString["pageno"] != null)
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                lblCurSession.Text = Session["currentsession"].ToString();
                PopulateDropDownList();

            }
            divMsg.InnerHtml = string.Empty;
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CourseWiseSeatNoReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CourseWiseSeatNoReport.aspx");
        }
    }

    #endregion

    #region Form Events

    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWiseSeatNoReport.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }


    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEPTNO = " + ddlDepartment.SelectedValue, "SCHEMENO");
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Examination Seat No Report", "SeatingArrangement.rpt");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWiseSeatNoReport.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region Private Methods


    private void Clear()
    {
        ddlDepartment.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSION_NAME");
            //semester
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNAME");
            //SCHEME
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMENO>0", "SCHEMENO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BulkHallTicketReport.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");

            }
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
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourseName.SelectedValue) + ",@P_PREVSTATUS=" + Convert.ToInt32(ddlExamType.SelectedValue) + ",IPADDRESS=" + ViewState["ipAddress"].ToString() + ",@USERNAME=" + Session["userfullname"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_hallticket.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
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

    #endregion

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        //COURSE NAME
        objCommon.FillDropDownList(ddlCourseName, "ACD_STUDENT_RESULT", "distinct COURSENO", "CCODE + ' - ' + COURSENAME", "COURSENO>0 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "COURSENO");

    }
}
