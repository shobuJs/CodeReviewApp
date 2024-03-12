//=================================================================================
// PROJECT NAME  : RFC-SVCE                                
// MODULE NAME   : OFFERED COURSE
// CREATION DATE : 10-MAY-2019
// CREATED BY    : RAJU BITODE                             
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class ACADEMIC_SessionWiseOfferedCoure : System.Web.UI.Page
{
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    #region Page Load
    protected void Page_PreInit(object sender, EventArgs e)
    {
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
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Populate the DropDownList 
                PopulateDropDownList();
            }
            Session["reportdate"] = null;
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Offered_Course.aspx");
            }
            Common objCommon = new Common();
            // objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Offered_Course.aspx");
        }
    }
    private void PopulateDropDownList()
    {
        //Fills Session DropDown.
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0", "SESSIONNO DESC");
        //Degree Name
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0 AND DEGREENO > 0", "DEGREENO");
        objCommon.FillDropDownList(ddlToTerm, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 and SEMESTERNO<9", "");
    }
  
    #endregion

    #region dropdown
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDegree.SelectedValue = "0";
        ddlBranch.SelectedValue = "0";
        ddlScheme.SelectedValue = "0";
        ddlSemester.SelectedValue = "0";
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvCourse.Visible = false;
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON B.BRANCHNO=A.BRANCHNO", "B.BRANCHNO", "LONGNAME", "B.BRANCHNO<>99 AND DEGREENO = " + ddlDegree.SelectedValue, "B.BRANCHNO");
          //  objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");
            ddlBranch.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B INNER JOIN ACD_SCHEME S ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "S.DEGREENO = " + ddlDegree.SelectedValue + " AND B.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue), "B.BRANCHNO");
              //objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B INNER JOIN ACD_SCHEME S ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue), "B.BRANCHNO");
                ddlScheme.Focus();
            }
            else
            {
                ddlScheme.Items.Clear();
                ddlBranch.SelectedIndex = 0;

                lvCourse.DataSource = null;
                lvCourse.DataBind();
                pnlCourse.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSemester, "ACD_COURSE c INNER JOIN ACD_SEMESTER S ON (c.SEMESTERNO=S.SEMESTERNO)", "DISTINCT c.SEMESTERNO", "SEMESTERNAME", "SCHEMENO=" + ddlScheme.SelectedValue, "c.SEMESTERNO");
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindListView();
    }
    protected void ddlToTerm_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDegree.SelectedValue = "0";
        ddlBranch.SelectedValue = "0";
        ddlScheme.SelectedValue = "0";
        ddlSemester.SelectedValue = "0";
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvCourse.Visible = false;
    }
    #endregion

    #region clickEvent
    protected void btnAd_Click(object sender, EventArgs e)
    {
        string offcourse = string.Empty;
        string offered_status = string.Empty;
        string sem = string.Empty;
        int sessionno = 0;

        int userno = 0;
        try
        {
            CourseController objCC = new CourseController();
            if (Session["userno"].ToString() != string.Empty)
            {
                userno = int.Parse(Session["userno"].ToString());
            }
            else
            {
                Response.Redirect("~/default.aspx", false);
            }

            int SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
            int SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            sessionno = Convert.ToInt32(ddlSession.SelectedValue);

            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {
                CheckBox chkBox = dataitem.FindControl("chkoffered") as CheckBox;
                HiddenField hfcourse = dataitem.FindControl("hf_course") as HiddenField;

                offcourse += hfcourse.Value + ",";
                if (chkBox.Checked)
                    offered_status += "1,";
                else

                    offered_status += "0,";
            }

            if (offcourse != string.Empty)
            {
                CustomStatus cs = (CustomStatus)objCC.UpdateOfferedCourse(SchemeNo, offcourse, SemesterNo, sessionno, userno, offered_status, Convert.ToInt32(ddlToTerm.SelectedValue));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    BindListView();
                    objCommon.DisplayMessage(updpnl, "Offered subjects saved successfully", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(updpnl, "Please offer atleast one subject", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Offered_Course.btnAd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlSemester.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        pnlCourse.Visible = false;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
    }
    private void BindListView()
    {
        try
        {
            int schemeno = int.Parse(ddlScheme.SelectedValue);
            DataSet ds = null;
            CourseController objStud = new CourseController();
            ds = objStud.GetCourseOffered(schemeno, Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlToTerm.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = ds;
                lvCourse.DataBind();
                lvCourse.Visible = true;
                pnlCourse.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Offered_Course.BindListView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ShowReport("OFFERED_COURSE_LIST", "rptOfferedCourse.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            if (Session["colcode"] == null && Session["username"] == null &&
                  Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx", false);
                return;
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_USERNAME=" + Session["userfullname"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_TO_TERM=" + ddlToTerm.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",ToTerm=" + ddlToTerm.SelectedItem.Text;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentLocalAddressLabel.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion
}

