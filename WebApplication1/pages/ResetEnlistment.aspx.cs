//======================================================================================
// MODULE NAME   : ACADEMIC
// PAGE NAME     : ResetEnlistment.aspx
// CREATION DATE : 30-MAY-2023
// CREATED BY    : SHAHBAZ AHMAD
//======================================================================================

using IITMS.UAIMS;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ACADEMIC_ResetEnlistment : System.Web.UI.Page
{
   
    #region Page Events
    Common objCommon = new Common();
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
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
                }
                else
                {
                    lblDynamicPageTitle.Text = "Reset Enlistment";
                }
            }
            FillDropDown();
            objCommon.SetLabelData("0");

        }


    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=schememaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=schememaster.aspx");
        }
    }

    #endregion

    #region Bulk Reset
    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND ISNULL(ACTIVE,0)=1 AND COLLEGE_ID IN (" + Session["college_nos"].ToString() + ")", "COLLEGE_NAME");

        //for 2nd tab
        objCommon.FillDropDownList(ddlSessionSingle, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND FLOCK=1 ", "SESSIONNO DESC");
    }
    private void BindlvBulk()
    {
        int collegeID = Convert.ToInt32(ddlCollege.SelectedValue);
        int sessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        int schemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
        int semesterNo = Convert.ToInt32(ddlsemester.SelectedValue);
        string name_bulk = "PKD_ACD_GET_STUDENTS_RESET_ENLISTMENT";
        string para_bulk = "@P_COLLEGE_ID,@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO";
        string call_bulk = "" + collegeID + "," + sessionNo + "," + schemeNo + "," + semesterNo;
        DataSet ds = new DataSet();
        ds = objCommon.DynamicSPCall_Select(name_bulk, para_bulk, call_bulk);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvResetBulk.DataSource = ds;
            lvResetBulk.DataBind();
            pnllvBulk.Visible = true;
            objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvResetBulk);//Set label

            ScriptManager.RegisterStartupScript(this, GetType(), "key", "$('#tblStudentBulk').DataTable({paging: false,ordering: true,info: false,scrollY: '320px', scrollCollapse: true,});", true);
        }
        else
        {
            objCommon.DisplayMessage(updBulk, "Record Not Found!", this.Page);
            pnllvBulk.Visible = false;
            lvResetBulk.DataSource = null;
            lvResetBulk.DataBind();

        }
    }
    private void ResetDropDown(DropDownList ddl)
    {
        ddl.Items.Clear();
        ddl.Items.Add(new ListItem("Please Select", "0"));
    }
    private void clear()
    {
        ddlCollege.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlsemester.SelectedIndex = 0;
        ResetDropDown(ddlSession);
        ResetDropDown(ddlScheme);
        ResetDropDown(ddlsemester);
        lvResetBulk.DataSource = null;
        lvResetBulk.DataBind();
        pnllvBulk.Visible = false;
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSession, @"ACD_SESSION_MASTER SM 
		INNER JOIN ACD_SESSION_MAPPING SMP ON(SMP.SESSIONNO=SM.SESSIONNO)", "SM.SESSIONNO", "SM.SESSION_NAME", "ISNULL(SMP.STATUS,0)=1 AND FLOCK=1 AND SMP.COLLEGE_ID=" + ddlCollege.SelectedValue.ToString(), "SMP.SESSIONNO DESC");
            
            ddlSession.Focus();
            ddlSession.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlsemester.SelectedIndex = 0;
        }
        else
        {
            ResetDropDown(ddlSession);

        }
        ResetDropDown(ddlScheme);
        ResetDropDown(ddlsemester);
        pnllvBulk.Visible = false;
        lvResetBulk.DataSource = null;
        lvResetBulk.DataBind();


    }
    
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME SM INNER JOIN ACD_STUDENT_RESULT SR ON(SM.SCHEMENO=SR.SCHEMENO AND SM.DEGREENO=SR.DEGREENO AND SM.COLLEGE_ID=SR.COLLEGE_ID)", "DISTINCT SM.SCHEMENO", "SM.SCHEMENAME", "SR.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND SR.SESSIONNO=" + ddlSession.SelectedValue, "SM.SCHEMENAME");
            ddlScheme.Focus();
            ddlScheme.SelectedIndex = 0;
            ddlsemester.SelectedIndex = 0;
        }
        else
        {
            ResetDropDown(ddlScheme);
            ResetDropDown(ddlsemester);
        }
        pnllvBulk.Visible = false;
        lvResetBulk.DataSource = null;
        lvResetBulk.DataBind();

    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER SM INNER JOIN ACD_STUDENT_RESULT SR ON(SM.SEMESTERNO=SR.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "SR.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND SR.SESSIONNO=" + ddlSession.SelectedValue + " AND SR.SCHEMENO=" + ddlScheme.SelectedValue, "SM.SEMESTERNO");
            ddlsemester.Focus();
            ddlsemester.SelectedIndex = 0;
        }
        else
            ResetDropDown(ddlsemester);
        pnllvBulk.Visible = false;
        lvResetBulk.DataSource = null;
        lvResetBulk.DataBind();
    }

    protected void ddlsemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsemester.SelectedIndex > 0)
        {
            this.BindlvBulk();
        }
        else
        {
            lvResetBulk.DataSource = null;
            lvResetBulk.DataBind();
            pnllvBulk.Visible = false;
        }
    }
    
    protected void btnCancelBulk_Click(object sender, EventArgs e)
    {
        this.clear();
    }
   
    protected void btnResetBulk_Click(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex <= 0)
        {
            string lblcollege = lblDYCollege.Text.ToString() == string.Empty ? "College" : lblDYCollege.Text.ToString();
            objCommon.DisplayMessage(updSingle, "Please Select " + lblcollege.ToString() + ".", this.Page);
            ddlCollege.Focus();
            return;
        }
        if (ddlSession.SelectedIndex <= 0)
        {
            string lblsession = lblDYddlSession.Text.ToString() == string.Empty ? "Session" : lblDYddlSession.Text.ToString();
            objCommon.DisplayMessage(updSingle, "Please Select " + lblsession.ToString() +".", this.Page);
            ddlSession.Focus();
            return;
        }
        if (ddlScheme.SelectedIndex <= 0)
        {
            string lblscheme = lblDYScheme.Text.ToString() == string.Empty ? "Curriculum" : lblDYScheme.Text.ToString();
            objCommon.DisplayMessage(updSingle, "Please Select " + lblscheme.ToString() + ".", this.Page);
            ddlScheme.Focus();
            return;
        }
        if (ddlsemester.SelectedIndex <= 0)
        {
            string lblsemester = lblDYSemester.Text.ToString() == string.Empty ? "Semester" : lblDYSemester.Text.ToString();
            objCommon.DisplayMessage(updSingle, "Please Select " + lblsemester.ToString() + ".", this.Page);
            ddlsemester.Focus();
            return;
        }
        int collegeID = Convert.ToInt32(ddlCollege.SelectedValue);
        int sessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        int schemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
        int semesterNo = Convert.ToInt32(ddlsemester.SelectedValue);

        string idno = string.Empty;
        int count = 0;
        foreach (ListViewDataItem items in lvResetBulk.Items)
        {
            CheckBox chk = items.FindControl("chkStd") as CheckBox;
            HiddenField hdfidno = items.FindControl("hdfidno") as HiddenField;
            if (chk.Checked == true)
            {
                idno += hdfidno.Value + "$";
                count++;
            }
            
        }
        if (count == 0)
        {
            objCommon.DisplayMessage(updBulk, "Please Select At Least One Student!", this.Page);
            BindlvBulk();
            return;
        }
        string sp_name = "PKG_ACD_RESET_ENLISTMENT";
        string sp_para = "@P_IDNO,@P_COLLEGE_ID,@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_UA_NO,@P_IPADD,@P_OUTPUT";
        string sp_call = "" + idno + "," + collegeID + "," + sessionNo + "," + schemeNo + "," + semesterNo + "," + Convert.ToInt32(Session["userno"]) + "," + Session["ipAddress"].ToString() + "," + 0;
        CustomStatus ret = (CustomStatus)Convert.ToInt32(objCommon.DynamicSPCall_IUD(sp_name, sp_para, sp_call, true, 1));

        if (ret.Equals(CustomStatus.RecordUpdated))
        {
            objCommon.DisplayMessage(updBulk, "Enlistment Reset Successfully!", this.Page);
            BindlvBulk();
        }
        else
        {
            objCommon.DisplayMessage(updBulk, "Failed to Reset Enlistment!", this.Page);
            BindlvBulk();
        }

    }
    #endregion

    #region Searc Single Student
    protected void ddlSessionSingle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSessionSingle.SelectedIndex > 0)
            txtID.Focus();
        else
            ddlSessionSingle.Focus();

        txtID.Text = string.Empty;
        lvCourseDetailsSingle.DataSource = null;
        lvCourseDetailsSingle.DataBind();
        pnlDetails.Visible = false;
        btnResetSingle.Visible = false;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlSessionSingle.SelectedIndex <= 0)
        {
            string lblsession = lblDYddlSession1.Text.ToString();

            objCommon.DisplayMessage(updSingle, "Please Select" + lblsession.ToString() == string.Empty ? "Session" : lblsession.ToString() + "", this.Page);
            return;
        }
        if (txtID.Text == string.Empty)
        {
            string lblid = lblStudentId.Text.ToString();
            objCommon.DisplayMessage(updSingle, "Please Enter" + lblid.ToString() + "", this.Page);
            return;
        }
        DataSet ds = new DataSet();
        string sp_name = "PKG_ACD_GET_STUDENT_RESET_ENLISTMENT_SINGLE";
        string sp_param = "@P_REGNO,@P_SESSIONNO";
        string sp_call = "" + txtID.Text.ToString() + "," + ddlSessionSingle.SelectedValue.ToString();
        ds = objCommon.DynamicSPCall_Select(sp_name, sp_param, sp_call);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblStudentIdSingle.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
            lblNameSingle.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            lblFacultySingle.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
            lblProgramSingle.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
            lblSemesterSingle.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
            hdfsrid.Value = ds.Tables[0].Rows[0]["IDNO"].ToString();
            hdfclgid.Value = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            hdfscheme.Value = ds.Tables[0].Rows[0]["SCHEMENO"].ToString();
            hdfsem.Value = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
            btnResetSingle.Visible = true;
            pnlDetails.Visible = true;
            lvCourseDetailsSingle.DataSource = ds;
            lvCourseDetailsSingle.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvCourseDetailsSingle);
        }
        else
        {
            objCommon.DisplayMessage(updSingle, "Record Not Found!", this.Page);
            btnResetSingle.Visible = false;
            lvCourseDetailsSingle.DataSource = null;
            lvCourseDetailsSingle.DataBind();
            pnlDetails.Visible = false;
            return;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlSessionSingle.SelectedIndex = 0;
        txtID.Text = string.Empty;
        lvCourseDetailsSingle.DataSource = null;
        lvCourseDetailsSingle.DataBind();
        pnlDetails.Visible = false;
        btnResetSingle.Visible = false;
        hdfsrid.Value = string.Empty;
        hdfsem.Value = string.Empty;
        hdfscheme.Value = string.Empty;
        hdfclgid.Value = string.Empty;
    }

    protected void btnResetSingle_Click(object sender, EventArgs e)
    {
        if (ddlSessionSingle.SelectedIndex <= 0)
        {
            string lblsession = lblDYddlSession1.Text.ToString();

            objCommon.DisplayMessage(updSingle, "Please Select" + lblsession.ToString() == string.Empty ? "Session" : lblsession.ToString() + "", this.Page);
            return;
        }
        int collegeID = Convert.ToInt32(hdfclgid.Value);
        int sessionNo = Convert.ToInt32(ddlSessionSingle.SelectedValue);
        int schemeNo = Convert.ToInt32(hdfscheme.Value.ToString());
        int semesterNo = Convert.ToInt32(hdfsem.Value.ToString());

        string idno = hdfsrid.Value.ToString()+"$";
        string sp_name = "PKG_ACD_RESET_ENLISTMENT";
        string sp_para = "@P_IDNO,@P_COLLEGE_ID,@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_UA_NO,@P_IPADD,@P_OUTPUT";
        string sp_call = "" + idno + "," + collegeID + "," + sessionNo + "," + schemeNo + "," + semesterNo + "," + Convert.ToInt32(Session["userno"]) + "," + Session["ipAddress"].ToString()+"," + 0;
        CustomStatus ret = (CustomStatus)Convert.ToInt32(objCommon.DynamicSPCall_IUD(sp_name, sp_para, sp_call, true, 1));

        if (ret.Equals(CustomStatus.RecordUpdated))
        {
            objCommon.DisplayMessage(updSingle, "Enlistment Reset Successfully!", this.Page);
            btnClear_Click(sender, e);
        }
        else
        {
            objCommon.DisplayMessage(updSingle, "Failed to Reset Enlistment!", this.Page);
             btnClear_Click(sender, e);
        }


    }
    #endregion



  
}