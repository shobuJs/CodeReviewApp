//======================================================================================
// PROJECT NAME   : JSS                                                      
// MODULE NAME    : ACADEMIC                                                            
// PAGE NAME      : Coursewise Consolidate Report
// CREATION DATE  : 19/04/2018                                                         
// CREATED BY     : Snehal Wankhede                                                
// MODIFIED DATE  :                                                                      
// MODIFIED DESC  :                                                                      
//======================================================================================


using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.IO;


using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_Coursewise_Consolidate_Report : System.Web.UI.Page
{
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController excol = new ExamController();

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

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");

                int mul_col_flag = Convert.ToInt32(objCommon.LookUp("REFF", "MUL_COL_FLAG", "CollegeName='" + Page.Title + "'"));
                if (mul_col_flag == 0)
                {
                    objCommon.FillDropDownList(ddlCollegeName, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    ddlCollegeName.SelectedIndex = 1;
                }
                else
                {
                    objCommon.FillDropDownList(ddlCollegeName, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    ddlCollegeName.SelectedIndex = 0;
                }

                //if (Session["usertype"].ToString() != "1")
                //    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.DEPTNO=" + Session["userdeptno"].ToString(), "D.DEGREENAME");
                //else
                //    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");

                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");

               
                ddlSession.Focus();
            }

        }
        else
        {
            if (Request.Params["__EVENTTARGET"] != null &&
                   Request.Params["__EVENTTARGET"].ToString() != string.Empty)
            {
                if (Request.Params["__EVENTTARGET"].ToString() == "GenerateReport")
                    this.GenerateReport();
            }
        }

        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MarksEntryNotDone.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MarksEntryNotDone.aspx");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlBranch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlCollegeName.SelectedIndex = 0;

    }
    protected void btnReport1_Click(object sender, EventArgs e)
    {
        if (rdbExporttyye.SelectedIndex == 0)
        {
            ShowReport("REPORT", "rptTabulationPG_New.rpt");
        }
        else
        {
            objCommon.DisplayMessage(this.updTeacher, "Please Select PDF as Export Type!!", this.Page);
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGEID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue);//+ ",username=" + Session["username"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updTeacher, this.updTeacher.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MarksEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() != "1")
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
        }
        else
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
        }
        ddlBranch.Focus();
    }


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT B ON (A.IDNO=B.IDNO) INNER JOIN ACD_SEMESTER S ON(S.SEMESTERNO=A.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "SEMESTERNAME", "A.SEMESTERNO > 0 AND A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND B.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + "AND B.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND B.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue), "A.SEMESTERNO ASC");
            if (Session["usertype"].ToString() != "1")
            {
                objCommon.FillDropDownList(ddlscheme, "ACD_BRANCH B INNER JOIN ACD_SCHEME S ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND S.DEGREENO = " + ddlDegree.SelectedValue + " AND S.DEPTNO =" + Session["userdeptno"].ToString(), "B.BRANCHNO");
            }
            else
            {
                objCommon.FillDropDownList(ddlscheme, "ACD_BRANCH B INNER JOIN ACD_SCHEME S ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND S.DEGREENO = " + ddlDegree.SelectedValue, "B.BRANCHNO");
            }
        }
        else
        {
            objCommon.DisplayMessage("Please Select Branch!", this.Page);
            ddlBranch.Focus();
        }
    }
    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlscheme.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT B ON (A.IDNO=B.IDNO) INNER JOIN ACD_SEMESTER S ON(S.SEMESTERNO=A.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "SEMESTERNAME", "S.SEMESTERNO > 0 AND A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND B.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + "AND B.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND B.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "AND A.SCHEMENO=" + Convert.ToInt32(ddlscheme.SelectedValue), "A.SEMESTERNO ASC");
        }
    }

    public void GenerateReport()
    {
        try
        {
            if (rdbExporttyye.SelectedIndex == 0)
            {
                this.ShowReportConsolidated("CONSOLIDATEDREPORT", "rptConsolidatedsemwisereport.rpt");
            }
            else
            {
                this.ExportConsolidatedandMarksDataExcel();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnconmksrpt_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            DataSet ds = excol.GetConsolidatedMarksandAttendanceList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCollegeName.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlscheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlsectionno.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"if(confirm('The attendance is processed on " + ds.Tables[0].Rows[0]["ATTENDANCE_PROCESS_DATE"] + " ... Do you want to generate the same dated report ?? if not then please calculate attendance and proceed... '))");
                sb.Append(@"{__doPostBack('GenerateReport', '');}");
                ScriptManager.RegisterClientScriptBlock(this.updTeacher, this.updTeacher.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(this.updTeacher, "No Record Found For Your Selection!...Please calculate the attendance and marks for consolidated report..", this.Page);
            }

        }
        catch (Exception ex)
        { }
    }
    private void ShowReportConsolidated(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlscheme.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlsectionno.SelectedValue);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updTeacher, this.updTeacher.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MarksEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ExportConsolidatedandMarksDataExcel()
    {
        try
        {
            DataTable dt = new DataTable();
            DataSet ds = excol.GetConsolidatedMarksandAttendanceList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCollegeName.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlscheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlsectionno.SelectedValue));
            if (ds.Tables[0].Rows.Count == 0)
            {
                objCommon.DisplayMessage(this.updTeacher, "No Record Found For Your Selection!", this.Page);
            }
            else
            {
                dt = ds.Tables[0];
                if (dt.Columns.Contains("ROLNO"))
                {
                    dt.Columns.Remove("ROLNO");
                }
                if (dt.Columns.Contains("ENROLLMENT_NO"))
                {
                    dt.Columns.Remove("ENROLLMENT_NO");
                }
                if (dt.Columns.Contains("COURSEGROUP"))
                {
                    dt.Columns.Remove("COURSEGROUP");
                }
                GridView GVDayWiseAtt = new GridView();
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=" + ddlSession.SelectedItem.Text.Replace(" ", "_") + "_" + ddlDegree.SelectedItem.Text.Replace(" ", "_") + "_" + ddlBranch.SelectedItem.Text.Replace(" ", "_") + "_" + ddlSemester.SelectedItem.Text.Replace(" ", "_") + "_StudentAttendance_Marks_Excel_Report" + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnconmksrptexcel_Click(object sender, EventArgs e)
    {
        this.ExportConsolidatedandMarksDataExcel();
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlSemester.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlsectionno, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + ddlscheme.SelectedValue + " AND SR.SEMESTERNO =" + ddlSemester.SelectedValue + " AND SR.SECTIONNO > 0", "SC.SECTIONNAME");
        }
    }
    protected void btnconcalculate_Click(object sender, EventArgs e)
    {
        try
        {
            if (excol.Insdataconsolmarks(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCollegeName.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlscheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlsectionno.SelectedValue)) == 1)
            {
                objCommon.DisplayMessage(this.updTeacher, "Attendance and Marks are calculated successfully.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updTeacher, "No Record Found For Your Selection !!!", this.Page);
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnconsolexcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            DataSet ds = excol.GetConsolidatedMarksandAttendanceListExcel(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCollegeName.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlscheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlsectionno.SelectedValue));
            if (ds.Tables[0].Rows.Count == 0)
            {
                objCommon.DisplayMessage(this.updTeacher, "No Record Found For Your Selection!", this.Page);
            }
            else
            {
                dt = ds.Tables[0];

                GridView GVDayWiseAtt = new GridView();
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=" + ddlSession.SelectedItem.Text.Replace(" ", "_") + "_" + ddlDegree.SelectedItem.Text.Replace(" ", "_") + "_" + ddlBranch.SelectedItem.Text.Replace(" ", "_") + "_" + ddlSemester.SelectedItem.Text.Replace(" ", "_") + "_Student_Marks_Excel_Report" + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlSession.SelectedIndex > 0)
        {
            if (Session["usertype"].ToString() != "1")
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.DEPTNO=" + Session["userdeptno"].ToString(), "D.DEGREENAME");
            else
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
        }
        else
        {
            ddlDegree.SelectedIndex = 0;         
        }
    }
}