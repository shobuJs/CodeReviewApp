
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class ACADEMIC_Changeble_Reval : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();
    Exam objExam = new Exam();
    ActivityController objActController = new ActivityController();
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
                this.CheckPageAuthorization();

                //Set the Page Title
                this.Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                PopulateDDL();
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
                //ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Changeble_Reval.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Changeble_Reval.aspx");
        }
    }

    private void PopulateDDL()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlfaculty, " ACD_COLLEGE_MASTER CM INNER JOIN USER_ACC UA ON (CM.COLLEGE_ID IN (SELECT CAST(VALUE AS INT) FROM DBO.SPLIT(UA.UA_COLLEGE_NOS,',')))INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON(CM.COLLEGE_ID=CDB.COLLEGE_ID)", "DISTINCT CM.COLLEGE_ID", "COLLEGE_NAME", "UA_NO =" + Session["userno"], "CM.COLLEGE_ID");

    }
    protected void ddlfaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlStudents, "ACD_STUDENT S INNER JOIN ACD_REVAL_RESULT R ON (S.IDNO=R.IDNO)", "DISTINCT S.IDNO", "S.ENROLLNO + ' - ' + S.NAME_WITH_INITIAL AS NAME", "ISNULL(REV_APPROVE_STAT,0)=1 AND R.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND COLLEGE_ID=" + ddlfaculty.SelectedValue, "");

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        string[] course;
        course = ddlmodule.SelectedItem.ToString().Split('-');
        DataSet dsStudent = objMarksEntry.GetStudentsForChangeble(Convert.ToInt32(ddlSession.SelectedValue),Convert.ToInt32(ddlStudents.SelectedValue),Convert.ToInt32(ddlfaculty.SelectedValue));
        if (dsStudent.Tables.Count > 0)
        {
            btnSave.Visible = true;
            Panel1.Visible = true;
            lvreval.DataSource = dsStudent;
            lvreval.DataBind();
        }
        else
        {
            btnSave.Visible = false;
            Panel1.Visible = false;
            lvreval.DataSource = null;
            lvreval.DataBind();
        }
        foreach (ListViewDataItem dataitem in lvreval.Items)
        {
            CheckBox chkBox = dataitem.FindControl("chkcheck") as CheckBox;
            RadioButtonList ddlstatus = dataitem.FindControl("status") as RadioButtonList;
            Label lblcheck = dataitem.FindControl("lblchange") as Label;
            ddlstatus.SelectedValue = lblcheck.Text;
            if (lblcheck.Text == "")
            {
                chkBox.Checked = false;
            }
            else
            {
               
                chkBox.Checked = true;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        CustomStatus cs = 0;
        int COUNT = 0;
        string couresno = "";
        string changeble = "";

        foreach (ListViewDataItem dataitem in lvreval.Items)
        {
            CheckBox chkBox = dataitem.FindControl("chkcheck") as CheckBox;
            RadioButtonList ddlstatus = dataitem.FindControl("status") as RadioButtonList;
            HiddenField hdfcourseno = dataitem.FindControl("hdfcourseno") as HiddenField;
            if (chkBox.Checked == true)
            {
                if (ddlstatus.SelectedValue == "")
                {
                    objCommon.DisplayMessage(this.Page, "Please Select Status", this.Page);
                    return;
                }

                COUNT++;
                couresno += hdfcourseno.Value + ',';
                changeble += Convert.ToString(ddlstatus.SelectedValue) + ',';
            }
            else if (ddlstatus.SelectedValue != "" && chkBox.Checked == false)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Student", this.Page);
                return;
            }
        }
        couresno = couresno.TrimEnd(',');
        changeble = changeble.TrimEnd(',');
        if (COUNT == 0)
        {
            objCommon.DisplayMessage(this.Page, "Please Select at least One Student", this.Page);
            return;
        }
        cs = (CustomStatus)objMarksEntry.InsertChangeble(Convert.ToInt32(ddlStudents.SelectedValue),changeble,couresno);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            
            objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
            clear();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Error in Saving", this.Page);
        }

    }
    private void clear()
    {
        ddlStudents.SelectedIndex = 0;
        btnSave.Visible = false;
        ddlSession.SelectedIndex = 0;
        ddlfaculty.SelectedIndex = 0;
        ddlmodule.SelectedIndex = 0;
        Panel1.Visible = false;
        lvreval.DataSource = null;
        lvreval.DataBind();
    }
    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string[] course;
            course = ddlmodule.SelectedItem.ToString().Split('-');
            DataSet ds = objMarksEntry.GetStudentsForRevalReport(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlStudents.SelectedValue), course[0], Convert.ToInt32(ddlfaculty.SelectedValue));
            GridView gvStudData = new GridView();
            gvStudData.DataSource = ds;
            gvStudData.DataBind();
            string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
            string attachment = "attachment; filename=" + "_" + "RevalutionStatusReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Response.Write(FinalHead);
            gvStudData.RenderControl(htw);
            //string a = sw.ToString().Replace("_", " ");
            Response.Write(sw.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_CancelReciept.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void lnkstuddoc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            int coursrno = int.Parse(lnk.CommandArgument);
            DataSet ds = null;
            ds = objMarksEntry.GetmarksForrevalStatus(Convert.ToInt32(ddlSession.SelectedValue),Convert.ToInt32(ddlStudents.SelectedValue),Convert.ToInt32(coursrno),0);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lbloveralper.Text = ds.Tables[0].Rows[0]["OVERALL_PERCENTAGE"].ToString();
                lblStudentID.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
                lblname.Text = ds.Tables[0].Rows[0]["NAME_WITH_INITIAL"].ToString();
                lblpgm.Text = ds.Tables[0].Rows[0]["PGM"].ToString();
                lblclg.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                lblsemester.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                lblmodulename.Text = ds.Tables[0].Rows[0]["COURSE_NAME"].ToString();
                marks.Visible = true;
                lvmarksdetails.DataSource = ds;
                lvmarksdetails.DataBind();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
            }
        }
        catch (Exception ex)
        {

        }
    }
}