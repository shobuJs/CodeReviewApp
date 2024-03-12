using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Data.Common;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Grouping_Automation_Criteria : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController ObjCourse = new CourseController();

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
                {
                }
                BindDropDown();
                BindList();
                ViewState["action"] = "add";
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
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
                Response.Redirect("~/notauthorized.aspx?page=SectionAllotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SectionAllotment.aspx");
        }
    }
    protected void BindDropDown()
    {
        try
        {
            objCommon.FillListBox(ddlfaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_NAME ");
            objCommon.FillListBox(ddlCampus, "ACD_CAMPUS", "CAMPUSNO", "CAMPUSNAME", "CAMPUSNO > 0", "CAMPUSNO ");
            objCommon.FillListBox(ddlWDWE, "ACD_BATCH_SLIIT", "WEEKNO", "WEEKDAYSNAME", "WEEKNO > 0", "");
            objCommon.FillListBox(ddlUGStream, "ACD_STREAM", "STREAMNO", "STREAMNAME", "", "STREAMNAME");
            objCommon.FillDropDownList(ddlUGSyllabus, "ACD_ALTYPE", "ALTYPENO", "ALTYPENAME", "ALTYPENO>0", "ALTYPENAME");
            objCommon.FillDropDownList(ddlAllocationType, "ACD_ORIANTATION_GROUP_MASTER", "ORIANTATION_NO", "ORIANTATION_TYPE", "ORIANTATION_NO > 0", "");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_Create_Criteria->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlfaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string college = "";

            foreach (ListItem item in ddlfaculty.Items)
            {
                if (item.Selected == true)
                {
                    college += item.Value + ',';
                }

            }
            if (!string.IsNullOrEmpty(college))
            {
                college = college.Substring(0, college.Length - 1);
            }
            else
            {
                college = "0";
            }

            objCommon.FillListBox(ddlprogram, "ACD_COLLEGE_DEGREE_BRANCH S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO)INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=S.BRANCHNO)INNER JOIN ACD_AFFILIATED_UNIVERSITY A ON(A.AFFILIATED_NO=S.AFFILIATED_NO)", "DISTINCT CONCAT(S.DEGREENO,',',S.BRANCHNO,',',S.AFFILIATED_NO)AS ID", "(D.DEGREENAME +'-'+ B.LONGNAME + '-'+ A.AFFILIATED_LONGNAME)AS DEGBRANCH", "S.COLLEGE_ID IN(SELECT VALUE FROM DBO.SPLIT('" + college + "',','))", "ID");
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            int CRIT_NO = 0;
            string degreeno = "";
            string branchno = "";
            string affliatedno = "";
            string program = "";
            string[] pgm = new string[] { };
            string college = "";
            string campus = "";
            string weeks = "";
            string stream = "";
            foreach (ListItem item in ddlfaculty.Items)
            {
                if (item.Selected == true)
                {
                    college += item.Value + ',';
                }

            }
            if (!string.IsNullOrEmpty(college))
            {
                college = college.Substring(0, college.Length - 1);
            }
            else
            {
                objCommon.DisplayMessage(this.updGroupingAutomation, "Please Select faculty/School Name", this.Page);
                return;
            }

            foreach (ListItem item in ddlprogram.Items)
            {
                if (item.Selected == true)
                {
                    program += item.Value + ',';
                }
            }
            if (!string.IsNullOrEmpty(program))
            {
                program = program.Substring(0, program.Length - 1);
                pgm = program.Split(',');
                for (int i = 0; i < pgm.Length; i += 3)
                {

                    degreeno += pgm[i] + ",";

                }
                for (int j = 1; j < pgm.Length; j += 3)
                {
                    branchno += pgm[j] + ",";
                }
                for (int k = 2; k < pgm.Length; k += 3)
                {
                    affliatedno += pgm[k] + ",";
                }
                degreeno = degreeno.TrimEnd(',');
                branchno = branchno.TrimEnd(',');
                affliatedno = affliatedno.TrimEnd(',');
            }

            else
            {
                objCommon.DisplayMessage(this.updGroupingAutomation, "Please Select Program", this.Page);
                return;
            }

            foreach (ListItem item in ddlCampus.Items)
            {
                if (item.Selected == true)
                {
                    campus += item.Value + ',';
                }

            }
            if (!string.IsNullOrEmpty(campus))
            {
                campus = campus.Substring(0, campus.Length - 1);
            }
            else
            {
                objCommon.DisplayMessage(this.updGroupingAutomation, "Please Select Campus", this.Page);
                return;
            }

            foreach (ListItem item in ddlWDWE.Items)
            {
                if (item.Selected == true)
                {
                    weeks += item.Value + ',';
                }

            }
            if (!string.IsNullOrEmpty(weeks))
            {
                weeks = weeks.Substring(0, weeks.Length - 1);
            }
            else
            {
                objCommon.DisplayMessage(this.updGroupingAutomation, "Please Select  Week Day / Week End", this.Page);
                return;
            }
            foreach (ListItem item in ddlUGStream.Items)
            {
                if (item.Selected == true)
                {
                    stream += item.Value + ',';
                }

            }
            if (!string.IsNullOrEmpty(stream))
            {
                stream = stream.Substring(0, stream.Length - 1);
            }
            else
            {
                objCommon.DisplayMessage(this.updGroupingAutomation, "Please Select  UG (A/L) Stream", this.Page);
                return;
            }

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int CRITNo = Convert.ToInt32(objCommon.LookUp("ACD_GROUPING_AUTOMATION_CRITERIA", "isnull(max(CRITERIA_NO),0)", ""));
                    CRIT_NO = CRITNo + 1;
                    CustomStatus cs = CustomStatus.Others;
                    cs = (CustomStatus)ObjCourse.InsertGroupingCriteria(CRIT_NO, Convert.ToString(txtCriteriaName.Text), Convert.ToString(college), Convert.ToString(degreeno), Convert.ToString(branchno), Convert.ToString(affliatedno), Convert.ToString(campus), Convert.ToString(weeks), Convert.ToInt32(ddlUGSyllabus.SelectedValue), Convert.ToString(stream), Convert.ToInt32(ddlAllocationType.SelectedValue), Convert.ToInt32(Session["userno"]));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.updGroupingAutomation, "Record Saved Successfully", this.Page);
                        BindList();
                        ddlfaculty.ClearSelection();
                        txtCriteriaName.Text = "";
                        ddlprogram.ClearSelection();
                        ddlCampus.ClearSelection();
                        ddlWDWE.ClearSelection();
                        ddlUGSyllabus.SelectedIndex = 0;
                        ddlUGStream.ClearSelection();
                        ddlAllocationType.SelectedIndex = 0;

                    }
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(this.updGroupingAutomation, "Record Alredy Exists", this.Page);
                        return;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updGroupingAutomation, "Error", this.Page);
                    }
                }
                else if (ViewState["action"].ToString().Equals("edit"))
                {
                    CustomStatus cs = CustomStatus.Others;
                    cs = (CustomStatus)ObjCourse.UpdateGroupingCriteria(Convert.ToInt32(ViewState["CRITERIA_NO"]), Convert.ToString(txtCriteriaName.Text), Convert.ToString(college), Convert.ToString(degreeno), Convert.ToString(affliatedno), Convert.ToString(branchno), Convert.ToString(campus), Convert.ToString(weeks), Convert.ToInt32(ddlUGSyllabus.SelectedValue), Convert.ToString(stream), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlAllocationType.SelectedValue));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.updGroupingAutomation, "Record Updated Successfully", this.Page);
                        ViewState["action"] = "add";
                        btnSubmit.Text = "Submit";
                        BindList();
                        ddlfaculty.ClearSelection();
                        txtCriteriaName.Text = "";
                        ddlprogram.ClearSelection();
                        ddlCampus.ClearSelection();
                        ddlWDWE.ClearSelection();
                        ddlUGSyllabus.SelectedIndex = 0;
                        ddlUGStream.ClearSelection();
                        ddlAllocationType.SelectedIndex = 0;

                    }
                    else
                    {
                        objCommon.DisplayMessage(Page, "Error", this.Page);
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void clear()
    {
        ViewState["action"] = "add";
        btnSubmit.Text = "Submit";
        ddlfaculty.ClearSelection();
        txtCriteriaName.Text = "";
        ddlprogram.ClearSelection();
        ddlCampus.ClearSelection();
        ddlWDWE.ClearSelection();
        ddlUGSyllabus.SelectedIndex = 0;
        ddlUGStream.ClearSelection();
        ddlAllocationType.SelectedIndex = 0;
        //Panel6.Visible = false;
        //LvAndDetails.DataSource = null;
        //LvAndDetails.DataBind();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    private void BindList()
    {
        DataSet ds = ObjCourse.GetGroupingCriteria();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            Panel6.Visible = true;
            LvAndDetails.DataSource = ds;
            LvAndDetails.DataBind();
        }
        else
        {
            Panel6.Visible = false;
            LvAndDetails.DataSource = null;
            LvAndDetails.DataBind();
        }
    }
    protected void lnkedit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEdit = sender as LinkButton;
            //ViewState["action"] = "edit";
            int criteria_no = int.Parse(btnEdit.CommandArgument);
            ViewState["CRITERIA_NO"] = criteria_no;

            CustomStatus cs = CustomStatus.Others;
            cs = (CustomStatus)ObjCourse.UpdateGroupingCriteria(Convert.ToInt32(criteria_no), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, string.Empty, Convert.ToInt32(Session["userno"]), 0);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.updGroupingAutomation, "Record Deleted Successfully", this.Page);
                BindList();
                ddlfaculty.ClearSelection();
                txtCriteriaName.Text = "";
                ddlprogram.ClearSelection();
                ddlCampus.ClearSelection();
                ddlWDWE.ClearSelection();
                ddlUGSyllabus.SelectedIndex = 0;
                ddlUGStream.ClearSelection();
                ddlAllocationType.SelectedIndex = 0;
            }
            else
            {
                objCommon.DisplayMessage(Page, "Error", this.Page);
            }
            //btnSubmit.Text = "Update";
            //ShowDetails(criteria_no);
        }
        catch (Exception ex)
        {
        }
    }
    private void ShowDetails(int criteria_no)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_GROUPING_AUTOMATION_CRITERIA", "CRITERIA_NAME,COLLEGE_ID,DEGREENO BRANCHNO,CONCAT(DEGREENO,',',BRANCHNO,',',AFFILIATED_NO)AS PROGRAM,WEEKSNO,CAMPUSNO,AL_TYPE_NO,AL_STREAM_NO,ALLOCATION_NO", "", "CRITERIA_NO=" + criteria_no, "CRITERIA_NO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtCriteriaName.Text = ds.Tables[0].Rows[0]["CRITERIA_NAME"].ToString();
                ddlfaculty.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                ddlUGSyllabus.SelectedValue = ds.Tables[0].Rows[0]["AL_TYPE_NO"].ToString();
                ddlUGStream.SelectedValue = ds.Tables[0].Rows[0]["AL_STREAM_NO"].ToString();
                string program = (ds.Tables[0].Rows[0]["PROGRAM"].ToString());
                ddlfaculty_SelectedIndexChanged(new object(), new EventArgs());
                string[] pgm = program.Split('&');
                for (int j = 0; j < pgm.Length; j++)
                {
                    for (int i = 0; i < ddlprogram.Items.Count; i++)
                    {
                        if (pgm[j] == ddlprogram.Items[i].Value)
                        {
                            ddlprogram.Items[i].Selected = true;
                        }
                    }
                }
                string campus = "";
                campus = ds.Tables[0].Rows[0]["CAMPUSNO"].ToString();
                string[] mem = campus.Split(',');
                for (int j = 0; j < mem.Length; j++)
                {
                    for (int i = 0; i < ddlCampus.Items.Count; i++)
                    {
                        if (mem[j] == ddlCampus.Items[i].Value)
                        {
                            ddlCampus.Items[i].Selected = true;
                        }
                    }
                }
                string weeks = "";
                weeks = ds.Tables[0].Rows[0]["WEEKSNO"].ToString();
                string[] week = weeks.Split(',');
                for (int j = 0; j < week.Length; j++)
                {
                    for (int i = 0; i < ddlWDWE.Items.Count; i++)
                    {
                        if (week[j] == ddlWDWE.Items[i].Value)
                        {
                            ddlWDWE.Items[i].Selected = true;
                        }
                    }
                }
                ddlAllocationType.SelectedValue = ds.Tables[0].Rows[0]["ALLOCATION_NO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseCreation.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}