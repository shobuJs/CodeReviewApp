//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : COURSE ALLOTMENT                                      
// CREATION DATE : 15-OCT-2011
// ADDED BY      : ASHISH DHAKATE                                                  
// MODIFIED DATE : 12-DEC-2010 
// MODIFIED BY   : 
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
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_BulkRegistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //DataSet DetainedSubjects, CurrentSubjects, PreviousData;


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
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");
                ddlSession.SelectedIndex = 1;
                this.PopulateDropDown();

                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
        }
        divMsg.InnerHtml = string.Empty;
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StudentRegistration objSRegist = new StudentRegistration();
        StudentRegist objSR = new StudentRegist();

        try
        {
            int count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(*)", "SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + ddlScheme.SelectedValue + " AND SEMESTERNO=2" + "AND PREV_STATUS=0"));
            if (count > 0)
            {
                objCommon.DisplayMessage(updBulkReg, "Student(s) Already Registered Courses!!", this.Page);
                clear();
            }
            else
            {

                foreach (ListViewDataItem dataitem in lvStudent.Items)
                {
                    //Get Student Details from lvStudent
                    CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
                    if (cbRow.Checked == true)
                    {
                        objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                        objSR.IDNO = Convert.ToInt32(((dataitem.FindControl("lblIDNo")) as Label).Text);
                        objSR.REGNO = ((dataitem.FindControl("lblIDNo")) as Label).ToolTip;
                        objSR.SEMESTERNO = 2;
                        objSR.SCHEMENO = Convert.ToInt32(ddlScheme.SelectedValue);
                        objSR.IPADDRESS = ViewState["ipAddress"].ToString();
                        objSR.COLLEGE_CODE = Session["colcode"].ToString();
                        objSR.UA_NO = Convert.ToInt32(Session["userno"]);


                        //Get Course Details
                        foreach (ListViewDataItem dataitemCourse in lvCourse.Items)
                        {
                            if (((dataitemCourse.FindControl("cbRow")) as CheckBox).Checked == true)
                            {
                                objSR.COURSENOS += ((dataitemCourse.FindControl("lblCCode")) as Label).ToolTip + "$";
                                Label elective = (dataitemCourse.FindControl("lblCourseName")) as Label;
                                if (elective.ToolTip == "False")
                                {
                                    objSR.ELECTIVE += "0" + "$";
                                }
                                else
                                {
                                    objSR.ELECTIVE += "1" + "$";
                                }
                            }
                        }

                        objSR.ACEEPTSUB = "1";

                        //Register Single Student
                        int prev_status = 0;    //Regular Courses
                        CustomStatus cs = (CustomStatus)objSRegist.AddRegisteredSubjectsBulk(objSR, prev_status);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objSR.COURSENOS = string.Empty;
                            objSR.ACEEPTSUB = string.Empty;
                        }
                    }
                }

                objCommon.DisplayMessage(updBulkReg, "Student(s) Registered Successfully!!", this.Page);
                clear();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_BulkRegistration.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void clear()
    {
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        ddlSchemeType.SelectedIndex = 0;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        pnlCourses.Visible = false;
        pnlStudents.Visible = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        pnlCourses.Visible = false;
        pnlStudents.Visible = false;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        string uano = Session["userno"].ToString();
        string uatype = Session["usertype"].ToString();
        string dept = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(uano));

        if (ddlDegree.SelectedIndex > 0)
        {
            if (uatype == "1")
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
                ddlSchemeType.Focus();
            }
            else
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEPTNO=" + Convert.ToInt32(dept) + " AND DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
                ddlSchemeType.Focus();
            }
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlDegree.SelectedIndex = 0;
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO IN(1,3) AND BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");
            ddlScheme.Focus();

        }
        else
        {
            ddlScheme.Items.Clear();
            ddlBranch.SelectedIndex = 0;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex <= 0 || ddlScheme.SelectedIndex <= 0)
        {
            objCommon.DisplayMessage("Please Select Branch/Scheme", this.Page);
            return;
        }

        this.BindListView();

    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            if (ddlBranch.SelectedValue == "99")
                objCommon.FillDropDownList(ddlSection, "ACD_STUDENT S INNER JOIN ACD_SECTION SC ON (S.SECTIONNO = SC.SECTIONNO)", "DISTINCT S.SECTIONNO", "SC.SECTIONNAME", "S.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.SCHEMENO = " + ddlScheme.SelectedValue, "S.SECTIONNO");
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

        lvCourse.DataSource = null;
        lvCourse.DataBind();
        pnlCourses.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        pnlStudents.Visible = false;
    }

    #region User Defined Methods
    private void BindListView()
    {

        DataSet ds = null;


        ds = objCommon.FillDropDown("ACD_STUDENT", "IDNO", "ROLLNO,ENROLLNO,STUDNAME,REGNO", "SCHEMENO = " + ddlScheme.SelectedValue + " AND BRANCHNO = " + ddlBranch.SelectedValue + " AND SEMESTERNO=2", "ROLLNO,REGNO");

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds.Tables[0];
                lvStudent.DataBind();
                pnlStudents.Visible = true;
            }
            else
            {
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                pnlStudents.Visible = false;
            }
        }
        else
        {
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            pnlStudents.Visible = false;
        }

        //Get course list as per scheme and semester
        DataSet dsCourse = objCommon.FillDropDown("ACD_COURSE", "COURSENO", "CCODE,COURSE_NAME,ELECT", "SCHEMENO = " + ddlScheme.SelectedValue + " AND SEMESTERNO = 2", "COURSENO");
        if (dsCourse != null && dsCourse.Tables.Count > 0)
        {
            if (dsCourse.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = dsCourse;
                lvCourse.DataBind();
                pnlCourses.Visible = true;
            }
            else
            {
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                pnlCourses.Visible = false;
            }
        }
        else
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            pnlCourses.Visible = false;
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO IN (1,3)", "BRANCHNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_BulkRegistration.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkRegistration.aspx");
        }
    }
    #endregion
    protected void lvCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label elective = (e.Item.FindControl("lblCourseName")) as Label;
        if (elective.ToolTip == "False")
        {
            ((e.Item.FindControl("cbRow")) as CheckBox).Checked = true;
        }
        else
        {
            ((e.Item.FindControl("cbRow")) as CheckBox).Checked = false;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        LinkButton printButton = sender as LinkButton;
        int idno = Int32.Parse(printButton.CommandArgument);
        ViewState["IDNO"] = idno;
        int count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(*)", "IDNO=" + idno + " AND SESSIONNO = " + ddlSession.SelectedValue));
        if (count > 0)
        {
            objCommon.DisplayMessage("This Student is already registered for session " + ddlSession.SelectedItem.Text, this.Page);
            ShowReport("Registered_Courses", "rptPreRegSlip.rpt");
        }
        else
        {
            objCommon.DisplayMessage("This Student is not registered for session " + ddlSession.SelectedItem.Text, this.Page);
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
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_IDNO=" + ViewState["IDNO"] + ",@UserName=" + Session["username"];
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

}
