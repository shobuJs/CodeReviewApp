//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : COURSE WISE SECTION ALLOTMENT                                                     
// CREATION DATE : 27-DEC-2018                                                          
// CREATED BY    : HEMANTH G  
// DESCRIPTION   : THIS PAGE IS USED TO UPDATE SECTION COURSE WISE SECTION ALLOTMENT STORED IN ACD_STUDENT_RESULT                              
// MODIFIED DATE :                                                          
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_CourseWiseSectionAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["userdeptno"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //PopulateDropDownList();

                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                PopulateDropDownList();
            }
        }
        objCommon.SetLabelData("0");//for label
        objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
    }
    private void PopulateDropDownList()
    {
        try
        {
            // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND FLOCK=1", "SESSIONNO DESC");

            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            ddlSession.SelectedIndex = 1;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseWiseSectionAllotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlColg.SelectedValue + "", "DEGREENO");

            int chkActivity = 0;

            chkActivity = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER SM,USER_ACC UA CROSS APPLY DBO.SPLIT(UA.UA_COLLEGE_NOS,',') C", "count(*)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND SA.CLG_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " AND UA_NO=" + Session["userno"] + ")"));

            if (chkActivity > 0)
            {
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER SM,USER_ACC UA CROSS APPLY DBO.SPLIT(UA.UA_COLLEGE_NOS,',') C", "DISTINCT ISNULL(SESSIONNO,0)SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND SA.CLG_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " AND UA_NO=" + Session["userno"] + ")", "SESSIONNO DESC");

                //  ddlSession.SelectedIndex = 1;
                objCommon.FillDropDownList(ddlProgram, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO) INNER JOIN ACD_BRANCH BR ON (BR.BRANCHNO=B.BRANCHNO)", "DISTINCT CONVERT(NVARCHAR,D.DEGREENO)+','+CONVERT(NVARCHAR,B.BRANCHNO)", "(DEGREENAME+' - '+BR.LONGNAME) AS PROGRAM", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlColg.SelectedValue, "");
               // objCommon.FillDropDownList(ddlProgram, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlColg.SelectedValue + "", "DEGREENO");

                ddlSession.Enabled = true;
                ddlProgram.Enabled = true;
            }
            else
            {

                objCommon.DisplayMessage(this.updpnlSection, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);

                ddlSession.Enabled = false;
                ddlProgram.Enabled = false;
                ddlSession.SelectedIndex = 0;
                ddlProgram.SelectedIndex = 0;

            }
            //  CheckPageAuthorization();
            //  CheckActivity();



        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseWiseSectionAllotment.ddlColg_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string[] Program = ddlProgram.SelectedValue.Split(',');
            int degreeno = Convert.ToInt32(Program[0]);
            int branchno = Convert.ToInt32(Program[1]);
            if (ddlProgram.SelectedValue != "0")
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", " DEGREENO =" + degreeno + " and BRANCHNO = " + branchno, "SCHEMENO DESC");
                ddlSem.Items.Clear();
                ddlSem.Items.Add("Please Select");
                ddlSem.SelectedItem.Value = "0";
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("Please Select");
                ddlCourse.SelectedItem.Value = "0";

            }
            else
            {
                ddlScheme.Items.Clear();
                ddlSem.Items.Clear();
                ddlCourse.Items.Clear();
                ddlProgram.SelectedIndex = 0;

            }
            pnlStudent.Visible = false;
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            btnSubmit.Enabled = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseWiseSectionAllotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSem, "ACD_COURSE AC INNER JOIN ACD_SEMESTER S ON (AC.SEMESTERNO = S.SEMESTERNO) INNER JOIN ACD_STUDENT_RESULT SR ON(SR.SCHEMENO=AC.SCHEMENO AND SR.SEMESTERNO=AC.SEMESTERNO)", "DISTINCT AC.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SR.SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedValue) + "", "AC.SEMESTERNO");//AND SR.PREV_STATUS = 0
            }
            else
            {
                ddlSem.Items.Clear();
                ddlScheme.SelectedIndex = 0;
            }
            pnlStudent.Visible = false;
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            btnSubmit.Enabled = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseWiseSectionAllotment.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT SR INNER JOIN ACD_COURSE AC ON(SR.COURSENO=AC.COURSENO AND SR.SCHEMENO=AC.SCHEMENO)", "DISTINCT AC.COURSENO", "AC.CCODE+ ' - ' +AC.COURSE_NAME", "SR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SR.SCHEMENO = " + ddlScheme.SelectedValue + "AND SR.SEMESTERNO = " + Convert.ToInt32(ddlSem.SelectedValue) + "AND SR.EXAM_REGISTERED=1 AND ISNULL(SR.CANCEL,0)=0", "AC.COURSENO");
            }
            else
            {
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("Please Select");
                ddlCourse.SelectedItem.Value = "0";
            }
            pnlStudent.Visible = false;
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            btnSubmit.Enabled = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseWiseSectionAllotment.ddlSem_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.BindListView();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlColg.SelectedIndex = 0;
        ddlProgram.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        pnlStudent.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        btnSubmit.Enabled = false;
        ddlSession.SelectedIndex = 0;
        ddlSession.Enabled = false;
        ddlProgram.Enabled = false;
    }
    private void BindListView()
    {
        try
        {
            string[] Program = ddlProgram.SelectedValue.Split(',');
            int degreeno = Convert.ToInt32(Program[0]);
            int branchno = Convert.ToInt32(Program[1]);        
            DataSet ds = null;

            ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR INNER JOIN ACD_COURSE AC ON(SR.COURSENO=AC.COURSENO AND SR.SCHEMENO=AC.SCHEMENO ) INNER JOIN ACD_STUDENT ST ON(ST.IDNO=SR.IDNO AND ST.SCHEMENO=SR.SCHEMENO) LEFT OUTER JOIN ACD_SECTION SC ON (SR.SECTIONNO = SC.SECTIONNO)", "DISTINCT ST.IDNO", "ST.REGNO,ST.STUDNAME,ISNULL(SR.SECTIONNO,0)SECTIONNO,SC.SECTIONNAME,SR.SESSIONNO,SR.SCHEMENO,SR.SEMESTERNO,SR.COURSENO", "SR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ST.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + "AND ST.DEGREENO= " + degreeno + "AND ST.BRANCHNO=" + branchno + "AND SR.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + "AND SR.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND SR.EXAM_REGISTERED=1 AND ISNULL(SR.CANCEL,0)=0", "ST.REGNO");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlStudent.Visible = true;
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                btnSubmit.Enabled = true;
            }
            else
            {
                pnlStudent.Visible = false;
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                btnSubmit.Enabled = false;
                objCommon.DisplayMessage(this.updpnlSection, "No Students found for selected criteria!", this.Page);
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseWiseSectionAllotment.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlsec = e.Item.FindControl("ddlsec") as DropDownList;
            DataSet ds = objCommon.FillDropDown("ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNAME");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTableReader dtr = ds.Tables[0].CreateDataReader();
                while (dtr.Read())
                {
                    ddlsec.Items.Add(new ListItem(dtr["SECTIONNAME"].ToString(), dtr["SECTIONNO"].ToString()));
                }
            }
            //if (Convert.ToInt32(ddlsec.ToolTip) == 0)
            //{
            //    ddlsec.Enabled = true;
            //}
            //else
            //{
            //    ddlsec.Enabled = false;
            //}
            ddlsec.SelectedValue = ddlsec.ToolTip;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseWiseSectionAllotment.lvStudents_ItemDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objSC = new StudentController();
            string studids = string.Empty;
            string sections = string.Empty;
            string rollnos = string.Empty;
            int sessiono = Convert.ToInt32(ddlSession.SelectedValue);
            int college_id = Convert.ToInt32(ddlColg.SelectedValue);
            //int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            //int branchno = Convert.ToInt32(ddlBranch.SelectedValue);
            string[] Program = ddlProgram.SelectedValue.Split(',');
            int degreeno = Convert.ToInt32(Program[0]);
            int branchno = Convert.ToInt32(Program[1]);        
            int schemeno = Convert.ToInt32(ddlScheme.SelectedValue);
            int semesterno = Convert.ToInt32(ddlSem.SelectedValue);
            int courseno = Convert.ToInt32(ddlCourse.SelectedValue);
            int userno = Convert.ToInt32(Session["userno"]);

            if (sessiono > 0 && college_id > 0 && degreeno > 0 & branchno > 0 && schemeno > 0 && semesterno > 0 && courseno > 0)
            {
                foreach (ListViewDataItem lvItem in lvStudents.Items)
                {
                    if (Convert.ToInt32((lvItem.FindControl("ddlsec") as DropDownList).SelectedValue) > 0 && (lvItem.FindControl("ddlsec") as DropDownList).Enabled == true)
                    {
                        studids += (lvItem.FindControl("cbRow") as CheckBox).ToolTip + "$";
                        sections += (lvItem.FindControl("ddlsec") as DropDownList).SelectedValue + "$";
                    }
                }
                if (studids.Length <= 0 && sections.Length <= 0)
                {
                    objCommon.DisplayMessage(this.updpnlSection, "Please Select Student/Section", this.Page);
                    return;
                }
                if (objSC.UpdateStudentCourseWiseSectionManual(sessiono, college_id, degreeno, branchno, schemeno, semesterno, courseno, studids, sections, userno) == Convert.ToInt32(CustomStatus.RecordUpdated))
                {
                    this.BindListView();
                    objCommon.DisplayMessage(this.updpnlSection, "Student Section Alloted Successfully!!!", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.updpnlSection, "Server Error...", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlSection, "Please Select Session/College/Degree/Branch/Scheme/Semester/Course!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseWiseSectionAllotment.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlStudent.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        btnSubmit.Enabled = false;
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    objCommon.FillDropDownList(ddlProgram, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO) INNER JOIN ACD_BRANCH BR ON (BR.BRANCHNO=B.BRANCHNO)", "DISTINCT CONVERT(NVARCHAR,D.DEGREENO)+','+CONVERT(NVARCHAR,B.BRANCHNO)", "(DEGREENAME+' - '+BR.LONGNAME) AS PROGRAM", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlColg.SelectedValue, "");
        //   // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlColg.SelectedValue + "", "DEGREENO");
        //}
        //catch (Exception ex)
        //{

        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "ACADEMIC_CourseWiseSectionAllotment.ddlColg_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
    }
}