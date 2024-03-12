using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class MockUps_Change_Section : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
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
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority
                    //this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                    ViewState["action"] = "add";
                    BindDropdown();
                    objCommon.SetLabelData("0");//for label
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeesRefundReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=Slot_Monitoring.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Slot_Monitoring.aspx");
        }
    }
    private void BindDropdown()
    {
        string collge = objCommon.LookUp("USER_ACC", "UA_COLLEGE_NOS", "UA_NO=" + Session["userno"]);
        objCommon.FillDropDownList(ddlAcademicSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND IS_ACTIVE=1", "SESSIONNO");
        objCommon.FillDropDownList(ddlSessionSub, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND IS_ACTIVE=1", "SESSIONNO");
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + collge + ")" + " AND ACTIVE=1", "COLLEGE_ID");
        objCommon.FillDropDownList(ddlCollegeSub, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + collge + ")" + " AND ACTIVE=1", "COLLEGE_ID");
        objCommon.FillDropDownList(ddlCampus, "ACD_CAMPUS", "CAMPUSNO", "CAMPUSNAME", "ACTIVE=1", "CAMPUSNO");
        objCommon.FillDropDownList(ddlCampusSub, "ACD_CAMPUS", "CAMPUSNO", "CAMPUSNAME", "ACTIVE=1", "CAMPUSNO");           
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlCurriculum, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMENO>0 AND COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "SCHEMENO");
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        string SP_Name2 = "PKG_ACD_GET_SECTION_DETAILS";
        string SP_Parameters2 = "@P_COMMAND_TYPE,@P_SESSIONNO,@P_COLLEGE_ID,@P_CAMPUS_NO,@P_CURRICULUM_NO,@P_SEMESTERNO,@P_COURSENO";
        string Call_Values2 = "" + 1 + "," +Convert.ToInt32(ddlAcademicSession.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlCollege.SelectedValue) + "," + Convert.ToInt32(ddlCampus.SelectedValue) + "," +
            Convert.ToInt32(ddlCurriculum.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + 0 + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (ds.Tables[0].Rows.Count > 0)
        {
            btnSubmit.Visible = true;
            pnlBlock.Visible = true;
            lvBlockSection.DataSource = ds;
            lvBlockSection.DataBind();
        }
        else
        {
            Clear();
            btnSubmit.Visible = false;
            pnlBlock.Visible = false;
            lvBlockSection.DataSource = null;
            lvBlockSection.DataBind();
            objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlSection.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.Page, "Please Select Block Section", this.Page);
            return;
        }
        int uano = Convert.ToInt32(Session["userno"]);
        string idno = "",section="";
        int count = 0;
        CustomStatus cs = 0;
        foreach (ListViewDataItem lvItem in lvBlockSection.Items)
        {
            CheckBox chkBox = lvItem.FindControl("chkSection") as CheckBox;
            HiddenField hdfsemesterno = lvItem.FindControl("hdfsemesterno") as HiddenField;
            HiddenField hdfsessionno = lvItem.FindControl("hdfsessionno") as HiddenField;
            HiddenField hdfsectionno = lvItem.FindControl("hdfsectionno") as HiddenField;
            if (chkBox.Checked == true)
            {
                count++;
                idno += chkBox.ToolTip + "$";
                section += hdfsectionno.Value + "$";
            }
        }
        idno = idno.TrimEnd('$');
        section = section.TrimEnd('$');
        if (count == 0)
        {
            objCommon.DisplayMessage(this.Page, "Please Select At least One checkbox", this.Page);
        }
        else
        {
            string SP_Name1 = "PKG_ACD_UPDATE_SECTION_DETAILS";
            string SP_Parameters1 = "@P_COMMAND_TYPE,@P_SESSIONNO,@P_COLLEGE_ID,@P_CURRICULUM_NO,@P_SEMESTERNO,@P_COURSENO,@P_OLDSECTIONNO,@P_SECTIONNO,@P_IDNO,@P_UA_NO,@P_OUTPUT";
            string Call_Values1 = "" + 1 + "," + Convert.ToInt32(ddlAcademicSession.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlCollege.SelectedValue) + "," +
                Convert.ToInt32(ddlCurriculum.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + 0 + "," + section + "," +Convert.ToInt32(ddlSection.SelectedValue) + "," +
               idno+ ","+ Convert.ToInt32(Session["userno"].ToString())  + ",0";
            string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//insert
            if (que_out1 == "1")
            {
                objCommon.DisplayMessage(this.Page, "Record Saved Successfully!", this.Page);
                Clear();
            }
            if (que_out1 == "3")
            {
                objCommon.DisplayMessage(this.Page, "Section capacity full please contact OTR team !!!!", this.Page);               
                Clear();
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Error!", this.Page);
            }
        }
    }
    private void Clear()
    {
        ddlAcademicSession.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlCurriculum.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlCampus.SelectedIndex = 0;
        pnlBlock.Visible = false;
        lvBlockSection.DataSource = null;
        lvBlockSection.DataBind();
        btnSubmit.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void ddlCollegeSub_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
        objCommon.FillDropDownList(ddlCurriculumSub, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMENO>0 AND COLLEGE_ID=" + Convert.ToInt32(ddlCollegeSub.SelectedValue), "SCHEMENO");
    }
    protected void btnShowSub_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
        string SP_Name2 = "PKG_ACD_GET_SECTION_DETAILS";
        string SP_Parameters2 = "@P_COMMAND_TYPE,@P_SESSIONNO,@P_COLLEGE_ID,@P_CAMPUS_NO,@P_CURRICULUM_NO,@P_SEMESTERNO,@P_COURSENO";
        string Call_Values2 = "" + 2 + "," + Convert.ToInt32(ddlSessionSub.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlCollegeSub.SelectedValue) + "," + Convert.ToInt32(ddlCampusSub.SelectedValue) + "," +
            Convert.ToInt32(ddlCurriculumSub.SelectedValue) + "," + Convert.ToInt32(ddlSemSub.SelectedValue) + "," + Convert.ToInt32(ddlSubject.SelectedValue) + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (ds.Tables[0].Rows.Count > 0)
        {
            btnSubmitSub.Visible = true;
            Panel2.Visible = true;
            LvSubSection.DataSource = ds;
            LvSubSection.DataBind();
        }
        else
        {
            ClearSub();
            btnSubmitSub.Visible = false;
            Panel2.Visible = false;
            LvSubSection.DataSource = null;
            LvSubSection.DataBind();
            objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
        }
    }
    protected void btnSubmitSub_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
        if (ddlSectionSub.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.Page, "Please Select Section", this.Page);
            return;
        }
        int uano = Convert.ToInt32(Session["userno"]);
        string idno = "", section = "";
        int count = 0;
        CustomStatus cs = 0;
        foreach (ListViewDataItem lvItem in LvSubSection.Items)
        {
            CheckBox chkBox = lvItem.FindControl("chkSection") as CheckBox;
            HiddenField hdfsemesterno = lvItem.FindControl("hdfsemesterno") as HiddenField;
            HiddenField hdfsessionno = lvItem.FindControl("hdfsessionno") as HiddenField;
            HiddenField hdfsectionno = lvItem.FindControl("hdfsectionno") as HiddenField;
            if (chkBox.Checked == true)
            {
                count++;
                idno += chkBox.ToolTip + "$";
                section += hdfsectionno.Value + "$";
            }
        }
        idno = idno.TrimEnd('$');
        section = section.TrimEnd('$');
        if (count == 0)
        {
            objCommon.DisplayMessage(this.Page, "Please Select At least One checkbox", this.Page);
        }
        else
        {
            string SP_Name1 = "PKG_ACD_UPDATE_SECTION_DETAILS";
            string SP_Parameters1 = "@P_COMMAND_TYPE,@P_SESSIONNO,@P_COLLEGE_ID,@P_CURRICULUM_NO,@P_SEMESTERNO,@P_COURSENO,@P_OLDSECTIONNO,@P_SECTIONNO,@P_IDNO,@P_UA_NO,@P_OUTPUT";
            string Call_Values1 = "" + 2 + "," + Convert.ToInt32(ddlSessionSub.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlCollegeSub.SelectedValue) + "," +
                Convert.ToInt32(ddlCurriculumSub.SelectedValue) + "," + Convert.ToInt32(ddlSemSub.SelectedValue) + "," + Convert.ToInt32(ddlSubject.SelectedValue) + "," + section + "," + Convert.ToInt32(ddlSectionSub.SelectedValue) + "," +
               idno + "," + Convert.ToInt32(Session["userno"].ToString()) + ",0";
            string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//insert
            if (que_out1 == "1")
            {
                objCommon.DisplayMessage(this.Page, "Record Saved Successfully!", this.Page);
            }
            else if (que_out1 == "3")
            {
                objCommon.DisplayMessage(this.Page, "Section capacity full please contact OTR team !!!!", this.Page);
                
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Error!", this.Page);
            }
            ClearSub();
        }
    }
    private void ClearSub()
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
        ddlSessionSub.SelectedIndex = 0;
        ddlCampusSub.SelectedIndex = 0;
        ddlCollegeSub.SelectedIndex = 0;
        ddlCurriculumSub.SelectedIndex = 0;
        ddlSemSub.SelectedIndex = 0;
        ddlSubject.SelectedIndex = 0;
        ddlSectionSub.SelectedIndex = 0;
        Panel2.Visible = false;
        LvSubSection.DataSource = null;
        LvSubSection.DataBind();
        btnSubmitSub.Visible = false;
    }
    protected void btnClearSub_Click(object sender, EventArgs e)
    {
       ClearSub();
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        string SP_Name2 = "PKG_ACD_GET_SECTIONS";
        string SP_Parameters2 = "@P_SESSIONNO,@P_COLLEGE_ID,@P_SCHEMENO,@P_SEMESTERNO,@P_COURSENO";
        string Call_Values2 = "" + Convert.ToInt32(ddlAcademicSession.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlCollege.SelectedValue) + "," +
            Convert.ToInt32(ddlCurriculum.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + 0 + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlSection.Items.Clear();
            ddlSection.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlSection.DataSource = ds.Tables[0];
            ddlSection.DataValueField = "SECTIONNO";
            ddlSection.DataTextField = "SECTIONNAME";
            ddlSection.DataBind();
        }
        else
        {
            ddlSection.Items.Clear();
            ddlSection.Items.Insert(0, new ListItem("Please Select", "0"));
        }
    }
    protected void ddlSemSub_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
        if (ddlSemSub.SelectedValue != "0")
        {
            objCommon.FillDropDownList(ddlSubject, "ACD_COURSE S INNER JOIN ACD_STUDENT_RESULT R ON S.COURSENO=R.COURSENO", "DISTINCT S.COURSENO", "(S.CCODE + '-' + S.COURSE_NAME)COURSENAME", "S.ACTIVE=1 AND R.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeSub.SelectedValue) + "AND R.SESSIONNO=" + Convert.ToInt32(ddlSessionSub.SelectedValue) + "AND R.SCHEMENO=" + Convert.ToInt32(ddlCurriculumSub.SelectedValue) + "AND R.SEMESTERNO=" + Convert.ToInt32(ddlSemSub.SelectedValue), "S.COURSENO");
        }
        else
        {
            ddlSubject.Items.Clear();
            ddlSubject.Items.Insert(0, new ListItem("Please Select", "0"));
        }
    }
    protected void ddlCurriculumSub_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
        objCommon.FillDropDownList(ddlSemSub, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT R ON S.SEMESTERNO=R.SEMESTERNO", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.STATUS=1 AND R.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeSub.SelectedValue) + "AND R.SESSIONNO=" + Convert.ToInt32(ddlSessionSub.SelectedValue) + "AND SCHEMENO=" +Convert.ToInt32(ddlCurriculumSub.SelectedValue) , "S.SEMESTERNO");
    }
    protected void ddlCurriculum_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT R ON S.SEMESTERNO=R.SEMESTERNO", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.STATUS=1 AND R.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + "AND R.SESSIONNO=" + Convert.ToInt32(ddlAcademicSession.SelectedValue) + "AND SCHEMENO=" + Convert.ToInt32(ddlCurriculum.SelectedValue), "S.SEMESTERNO");
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
        string SP_Name2 = "PKG_ACD_GET_SECTIONS";
        string SP_Parameters2 = "@P_SESSIONNO,@P_COLLEGE_ID,@P_SCHEMENO,@P_SEMESTERNO,@P_COURSENO";
        string Call_Values2 = "" + Convert.ToInt32(ddlSessionSub.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlCollegeSub.SelectedValue) + "," +
            Convert.ToInt32(ddlCurriculumSub.SelectedValue) + "," + Convert.ToInt32(ddlSemSub.SelectedValue) + "," + Convert.ToInt32(ddlSubject.SelectedValue) + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlSectionSub.Items.Clear();
            ddlSectionSub.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlSectionSub.DataSource = ds.Tables[0];
            ddlSectionSub.DataValueField = "SECTIONNO";
            ddlSectionSub.DataTextField = "SECTIONNAME";
            ddlSectionSub.DataBind();           
        }
        else
        {
            ddlSectionSub.Items.Clear();
            ddlSectionSub.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlSubject.Items.Clear();
            ddlSubject.Items.Insert(0, new ListItem("Please Select", "0"));
        }
    }
}