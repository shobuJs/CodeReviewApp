//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : COURSE TEACHER ALLOTMMENT                                            
// CREATION DATE : 05-JULY-2011                                                          
// CREATED BY    :RENUKA A.                                                
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Academic_courseAllot : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string deptno = string.Empty;

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
                if (Session["usertype"].ToString() != "1")
                {
                    string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());

                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID IN (" + Session["college_nos"].ToString() + ") AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
                    ViewState["DEPTNO"] = "0";
                }
                else
                {
                    ViewState["DEPTNO"] = "0";
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID IN (" + Session["college_nos"].ToString() + ")", "D.DEGREENO");
                }

                PopulateDropDownList();
                FillTeacher();
                btnPrint.Enabled = false;
                BindListView();
            }
            Session["reportdata"] = null;
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
                Response.Redirect("~/notauthorized.aspx?page=courseAllot.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=courseAllot.aspx");
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");//AND SR.PREV_STATUS = 0
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                lvCourse.Visible = false;
                dvCourse.Visible = false;
            }
            else
            {
                ddlSem.Items.Clear();
                ddlScheme.SelectedIndex = 0;
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                lvCourse.Visible = false;
                dvCourse.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_courseAllot.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillCourse()
    {
        try
        {
            CourseController objCC = new CourseController();
            DataSet dsCourse = objCC.GetCourseForCourseAllotment(Convert.ToInt32(ddlScheme.SelectedValue));

            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));

            if (dsCourse.Tables.Count > 0)
            {
                ddlCourse.DataValueField = dsCourse.Tables[0].Columns[0].ColumnName;
                ddlCourse.DataTextField = dsCourse.Tables[0].Columns[1].ColumnName;
                ddlCourse.DataSource = dsCourse;
                ddlCourse.DataBind();
            }
            else
            {
                ddlCourse.DataSource = null;
                ddlCourse.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_courseAllot.FillCourse-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListView()
    {
        try
        {
            CourseController objCC = new CourseController();
            if (ddlScheme.SelectedIndex > 0)
            {
                DataSet ds = objCC.GetCourseAllotmentSectionwise(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvCourse.DataSource = ds;
                    lvCourse.DataBind();
                    lvCourse.Visible = true;
                    btnPrint.Enabled = true;
                    dvCourse.Visible = true;
                }
                else
                {
                    lvCourse.DataSource = null;
                    lvCourse.DataBind();
                    lvCourse.Visible = false;
                    btnPrint.Enabled = false;
                    dvCourse.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", " DEGREENO =" + ddlDegree.SelectedValue + " and BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                lvCourse.Visible = false;
                dvCourse.Visible = false;
            }
            else
            {
                ddlScheme.Items.Clear();
                ddlBranch.SelectedIndex = 0;
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                lvCourse.Visible = false;
                dvCourse.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        ddlScheme.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        ddlTeacher.SelectedIndex = 0;
        txtTot.Text = string.Empty;
        lblStatus.Text = string.Empty;
        btnPrint.Enabled = false;
        dvAdt.Visible = false;
        lvAdTeacher.DataSource = null;
        lvAdTeacher.DataBind();
        ddltheorypractical.SelectedIndex = 0;
        ddlSubjectType.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlDeptName.SelectedIndex = 0;
        ddlTeacher.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        dvCourse.Visible = false;
    }

    protected void btnAd_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            int subid = 0;
            int dup = 0;
            CourseController objCC = new CourseController();
            Student_Acd objStudent = new Student_Acd();
            foreach (ListViewDataItem dataitem in lvAdTeacher.Items)
            {
                CheckBox chkIDNo = dataitem.FindControl("chkIDNo") as CheckBox;
                if (chkIDNo.Checked == true)
                {
                    objStudent.AdTeacher += chkIDNo.ToolTip + ",";
                }
                else
                    count++;

                if (lvAdTeacher.Items.Count == count)
                    objStudent.AdTeacher = (ddlTeacher.SelectedValue);
            }

            if (objStudent.AdTeacher.Contains(ddlTeacher.SelectedValue))
            {
                String str = objStudent.AdTeacher;
                dup = Convert.ToInt32(ddlTeacher.SelectedValue);
                var uniques = str.Split(',').Reverse().Distinct().Take(dup).Reverse().Take(dup).ToList();
                objStudent.AdTeacher = string.Join(",", uniques.ToArray());
            }
            else
                objStudent.AdTeacher += ddlTeacher.SelectedValue + ",";


            if (objStudent.AdTeacher.Length > 0)
            {
                if (objStudent.AdTeacher.Substring(objStudent.AdTeacher.Length - 1) == ",")
                    objStudent.AdTeacher = objStudent.AdTeacher.Substring(0, objStudent.AdTeacher.Length - 1);
            }

            objStudent.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objStudent.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
            objStudent.Sem = ddlSem.SelectedValue;
            objStudent.Sectionno = Convert.ToInt32(ddlSection.SelectedValue);
            objStudent.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
            objStudent.UA_No = Convert.ToInt32(ddlTeacher.SelectedValue);

            subid = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "subid", "CourseNo=" + Convert.ToInt32(ddlCourse.SelectedValue)));
            objStudent.Pract_Theory = Convert.ToInt32(subid);
            objStudent.Th_Pr = Convert.ToInt32(ddltheorypractical.SelectedValue);

            count = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " And th_pr =" + Convert.ToInt32(ddltheorypractical.SelectedValue) + " AND SECTIONNO = " + Convert.ToInt32(ddlSection.SelectedValue)));
            if (count >= 1)
            {
                objCommon.DisplayMessage(updpnl, "This Course is Already alloted to another faculty !", this.Page);
            }
            else
            {
                if (objCC.AddCourseAllot(objStudent) == 1)
                {
                    objCommon.DisplayMessage(updpnl, "Course Allotted Successfully.", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(updpnl, "Course Successfully Alloted to Teacher.", this.Page);
                }
            }
            BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.btnAd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");

            //Fill Department
            if (Session["usertype"].ToString() != "1")
                objCommon.FillDropDownList(ddlDeptName, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO >0 AND DEPTNO =" + Session["userdeptno"].ToString(), "DEPTNAME");
            else
                objCommon.FillDropDownList(ddlDeptName, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO >0", "DEPTNAME");

            ddlDeptName.SelectedValue = deptno;
            ddlSession.SelectedIndex = 1;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillTeacher()
    {
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_DEPTNO", Convert.ToInt32(ddlDeptName.SelectedValue));
            DataTable dtTeacher = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_ALL_FACULTIES_BY_DEPT", objParams).Tables[0];
            //DropDownList
            ddlTeacher.Items.Clear();
            ddlTeacher.Items.Add(new ListItem("Please Select", "0"));
            if (dtTeacher.Rows.Count > 0)
            {
                ddlTeacher.DataSource = dtTeacher;
                ddlTeacher.DataTextField = dtTeacher.Columns["UA_FULLNAME"].ToString();
                ddlTeacher.DataValueField = dtTeacher.Columns["UA_NO"].ToString();
                ddlTeacher.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.FillTeacher-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillAdTeacher()
    {
        try
        {
            //Populating Faculty dropdownlist
            SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_DEPTNO", Convert.ToInt32(ddlDeptName.SelectedValue));

            DataTable dtFaculty = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_ALL_FACULTIES_BY_DEPT", objParams).Tables[0];
            if (dtFaculty.Rows.Count > 0)
            {
                lvAdTeacher.DataSource = dtFaculty;
                lvAdTeacher.DataBind();
                lvAdTeacher.Visible = true;

            }
            else
            {
                lvAdTeacher.DataSource = null;
                lvAdTeacher.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.FillAdTeacher-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnDel = sender as ImageButton;
        CourseController objCC = new CourseController();
        Student_Acd objSA = new Student_Acd();
        objSA.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        objSA.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
        objSA.UA_No = Convert.ToInt32(btnDel.AlternateText);
        objSA.CourseNo = Convert.ToInt32(btnDel.CommandArgument);
        objSA.sub_id = Convert.ToInt32((((sender as ImageButton).Parent as ListViewDataItem).FindControl("hdfsubid") as HiddenField).Value);
        objSA.Th_Pr = Convert.ToInt32((((sender as ImageButton).Parent as ListViewDataItem).FindControl("hdfthpr") as HiddenField).Value);
        objSA.Sectionno = Convert.ToInt32((((sender as ImageButton).Parent as ListViewDataItem).FindControl("hdfSecNo") as HiddenField).Value);
        if (Convert.ToInt16(objCC.DeleteCourseAllot(objSA)) == Convert.ToInt16(CustomStatus.RecordUpdated))
        {
            objCommon.DisplayMessage(updpnl, "Course Teacher Deleted Successfully.", this.Page);
            BindListView();
        }
        else if (Convert.ToInt16(objCC.DeleteCourseAllot(objSA)) == Convert.ToInt16(CustomStatus.RecordFound))
        {
            objCommon.DisplayMessage(updpnl, "Can Not Delete Course because Mark Entry has been done.", this.Page);
        }
        else
        {
            objCommon.DisplayMessage(updpnl, "Can Not Delete Course.", this.Page);
        }
    }

    public string GetAdTeachers(object obj)
    {
        DataTableReader dtr = objCommon.FillDropDown("USER_ACC", "UA_FULLNAME", "UA_NO", "UA_NO IN (" + obj.ToString() + ")", "UA_FULLNAME").CreateDataReader();
        string teachers = string.Empty;
        while (dtr.Read())
        {
            teachers += dtr["UA_FULLNAME"].ToString() + ",";
        }
        dtr.Close();

        if (teachers.Substring(teachers.Length - 1) == ",")
            teachers = teachers.Substring(0, teachers.Length - 1);

        return teachers;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                if (Session["usertype"].ToString() != "1")
                {
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO = " + Session["userdeptno"].ToString(), "A.LONGNAME");
                    lvCourse.DataSource = null;
                    lvCourse.DataBind();
                    lvCourse.Visible = false;
                    dvCourse.Visible = false;
                }
                else
                {
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
                }
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                lvCourse.Visible = false;
                dvCourse.Visible = true;
            }
            else
            {
                ddlBranch.Items.Clear();
                ddlDegree.SelectedIndex = 0;
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                lvCourse.Visible = false;
                dvCourse.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubjectType.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE", "DISTINCT COURSENO", "(CCODE + ' - ' + COURSE_NAME) COURSE_NAME ", "SCHEMENO = " + ddlScheme.SelectedValue + " AND SUBID = " + ddlSubjectType.SelectedValue + " AND SEMESTERNO = " + ddlSem.SelectedValue, "COURSE_NAME");
                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON (C.COURSENO = SR.COURSENO) INNER JOIN ACD_OFFERED_COURSE O ON (O.COURSENO = SR.COURSENO AND O.SESSIONNO = SR.SESSIONNO AND O.SEMESTERNO = SR.SEMESTERNO AND O.SCHEMENO = SR.SCHEMENO) INNER JOIN ACD_SCHEME S ON O.SCHEMENO=S.SCHEMENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ,  SR.SCHEMENO ", "SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlSem.SelectedValue + " AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND S.DEGREENO=" + ddlDegree.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue, "SR.SCHEMENO, COURSE_NAME");  
                ddlCourse.Focus();
                if (ddlSubjectType.SelectedValue == "1")
                {
                    ddltheorypractical.Items.Clear();
                    ListItem newlist = new ListItem("Please Select", "0");
                    ddltheorypractical.Items.Add(newlist);
                    ListItem newlist1 = new ListItem("Theory", "1");
                    ddltheorypractical.Items.Add(newlist1);

                }
                else if (ddlSubjectType.SelectedValue == "2")
                {
                    ddltheorypractical.Items.Clear();
                    ListItem newlist = new ListItem("Please Select", "0");
                    ddltheorypractical.Items.Add(newlist);
                    ListItem newlist1 = new ListItem("Practical", "2");
                    ddltheorypractical.Items.Add(newlist1);
                }
                else if (ddlSubjectType.SelectedValue == "3")
                {
                    ddltheorypractical.Items.Clear();
                    ListItem newlist = new ListItem("Please Select", "0");
                    ddltheorypractical.Items.Add(newlist);
                    ListItem newlist2 = new ListItem("Theory", "1");
                    ddltheorypractical.Items.Add(newlist2);
                    ListItem newlist1 = new ListItem("Practical", "2");
                    ddltheorypractical.Items.Add(newlist1);
                }
                else if (ddlSubjectType.SelectedValue == "13")
                {
                    ddltheorypractical.Items.Clear();
                    ListItem newlist = new ListItem("Please Select", "0");
                    ddltheorypractical.Items.Add(newlist);
                    ListItem newlist2 = new ListItem("Theory", "1");
                    ddltheorypractical.Items.Add(newlist2);
                    ListItem newlist1 = new ListItem("Tutorial", "3");
                    ddltheorypractical.Items.Add(newlist1);
                }
                else
                {
                    ddltheorypractical.Items.Clear();
                    ListItem newlist = new ListItem("Please Select", "0");
                    ddltheorypractical.Items.Add(newlist);
                    ListItem newlist2 = new ListItem("Theory", "1");
                    ddltheorypractical.Items.Add(newlist2);
                    ListItem newlist3 = new ListItem("Practical", "2");
                    ddltheorypractical.Items.Add(newlist3);
                    ListItem newlist1 = new ListItem("Tutorial", "3");
                    ddltheorypractical.Items.Add(newlist1);
                }
            }
            else
            {
                ddlCourse.Items.Clear();
                ddlSubjectType.SelectedIndex = 0;
            }
            dvAdt.Visible = false;
            ddltheorypractical.SelectedIndex = 0;
            lvAdTeacher.DataSource = null;
            lvAdTeacher.DataBind();
            lvAdTeacher.Visible = false;//***********
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlSubjectType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSem.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "DISTINCT SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNAME");
                objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlScheme.SelectedValue, "C.SUBID");
                ddlSubjectType.Focus();
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                lvCourse.Visible = false;
                dvCourse.Visible = false;
            }
            else
            {
                ddlSubjectType.Items.Clear();
                ddlSem.SelectedIndex = 0;
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                lvCourse.Visible = false;
                dvCourse.Visible = false;
            }
            ddlCourse.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlSem_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSubjectType.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlTeacher.SelectedIndex = 0;
            lbltotcount.Text = string.Empty;
            ddlTeacher.SelectedIndex = 0;
            BindListView();
            ddlSubjectType.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlSection_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillTeacher();
        FillAdTeacher();
        dvAdt.Visible = true;
    }

    protected void ddltheorypractical_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltheorypractical.SelectedIndex > 0 && ddltheorypractical.SelectedValue == "1")
        {
            FillAdTeacher();
        }
        else
        {
            lvAdTeacher.DataSource = null;
            lvAdTeacher.DataBind();
            dvAdt.Visible = false;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport(rdoReportType.SelectedValue, "rptCourse_Allotment1.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseAllotment.btnPrint_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlDegree.SelectedItem.Text + "_" + ddlBranch.SelectedItem.Text + "_" + ddlSem.SelectedItem.Text + "." + rdoReportType.SelectedValue;  //+ ddlSection.SelectedItem.Text +
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_USERNAME=" + Session["userfullname"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue;
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

}
