using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_Equivalence : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    CourseController objCC = new CourseController();

    #region Page Action
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
        try
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

                        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                   // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "", "SESSIONNO DESC");
                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    objCommon.FillDropDownList(ddlNewScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "", "SCHEMENAME");
                }
            }
            divMsg.InnerHtml = string.Empty;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_CertificateMaster.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");

        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CertificateMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CertificateMaster.aspx");
        }
    }
    #endregion

    protected void ddlOldScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlOldScheme.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlOldCourse, "ACD_STUDENT_RESULT", "DISTINCT COURSENO", "CCODE+' - '+COURSENAME AS COURSE_NAME", "SCHEMENO=" + ddlOldScheme.SelectedValue + " AND IDNO=" + Convert.ToInt32(ViewState["idno"]), "COURSENO");
        }
    }

    protected void ddlNewScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlNewScheme.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE", "COURSENO", "CCODE+' - '+COURSE_NAME AS COURSE_NAME", "SCHEMENO=" + ddlNewScheme.SelectedValue, "CCODE, SEMESTERNO");
        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    private void BindListview()
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("ACD_COURSE_EQUIVALENCE E INNER JOIN ACD_SCHEME S ON (S.SCHEMENO=E.OLD_SCHEMENO) INNER JOIN ACD_SCHEME R ON R.SCHEMENO=E.NEW_SCHEMENO", "EQNO,IDNO", "OLD_COURSENO,NEW_COURSENO,OLD_CCODE,S.SCHEMENAME AS OLD_SCHEME,OLD_CREDITS,NEW_COURSENO,NEW_CCODE,R.SCHEMENAME AS NEW_SCHEME, NEW_CREDITS,ISNULL(CREG_STATUS,0)CREG_STATUS", "ISNULL(E.CANCEL,0)=0 AND IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND SESSIONNO= " + Convert.ToInt32(ddlSession.SelectedValue), "");
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = ds;
                lvCourse.DataBind();
                pnlCourse.Visible = true;
            }
            else
            {
                lvCourse.DataSource = ds;
                lvCourse.DataBind();
                pnlCourse.Visible = false;
            }
        }
        else
        {
            lvCourse.DataSource = ds;
            lvCourse.DataBind();
        }

    }

    private void ShowDetails(int eqno)
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("ACD_COURSE_EQUIVALENCE", "IDNO,OLD_COURSENO,NEW_COURSENO", "SESSIONNO,OLD_SCHEMENO,NEW_SCHEMENO", "EQNO=" + eqno, "");

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSession.SelectedValue = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
                ddlOldScheme.SelectedValue = ds.Tables[0].Rows[0]["OLD_SCHEMENO"].ToString();
                ddlNewScheme.SelectedValue = ds.Tables[0].Rows[0]["NEW_SCHEMENO"].ToString();
                objCommon.FillDropDownList(ddlOldCourse, "ACD_COURSE", "COURSENO", "CCODE+' - '+COURSE_NAME", "SCHEMENO=" + ddlOldScheme.SelectedValue, "COURSENO");
                ddlOldCourse.SelectedValue = ds.Tables[0].Rows[0]["OLD_COURSENO"].ToString();
                objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE", "COURSENO", "CCODE+' - '+COURSE_NAME", "SCHEMENO=" + ddlNewScheme.SelectedValue, "COURSENO");
                ddlNewCourse.SelectedValue = ds.Tables[0].Rows[0]["NEW_COURSENO"].ToString();
            }
            else
            {
                ddlSession.SelectedValue = "0";
                ddlOldScheme.SelectedValue = "0";
                ddlNewScheme.SelectedValue = "0";
                ddlOldCourse.SelectedValue = "0";
                ddlNewCourse.SelectedValue = "0";
            }
        }
        else
        {
            ddlSession.SelectedValue = "0";
            ddlOldScheme.SelectedValue = "0";
            ddlNewScheme.SelectedValue = "0";
            ddlOldCourse.SelectedValue = "0";
            ddlNewCourse.SelectedValue = "0";
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            string regno = string.Empty;
            string idno = string.Empty;
            regno = txtRegno.Text.Trim();
            if (regno != string.Empty)
            {
                idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + regno + "' OR ENROLLNO='" + regno + "'");

                if (idno != string.Empty)
                {
                    ViewState["idno"] = idno;
                    pnlCourseList.Visible = true;
                    objCommon.FillDropDownList(ddlOldScheme, "ACD_SCHEME S INNER JOIN ACD_STUDENT_RESULT SR ON (S.SCHEMENO=SR.SCHEMENO)", "DISTINCT S.SCHEMENO", "SCHEMENAME", "IDNO=" + idno, "SCHEMENAME");
                    BindListview();
                    ddlSession.Enabled = false;
                    txtRegno.Enabled = false;
                    ddlNewCourse.SelectedIndex = 0;
                    ddlNewScheme.SelectedIndex = 0;
                    ddlOldCourse.SelectedIndex = 0;

                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Student Not Found for Entered Univ. Reg. No. Or TAN/PAN!", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Please Enter Univ. Reg. No. Or TAN/PAN!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_CertificateMaster.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int old_scheme = 0;
            int new_scheme = 0;
            int old_course = 0;
            int new_course = 0;
            int sessionno = 0;
            int chkOldReg = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(COURSENO)", "IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0)=1 AND COURSENO=" + ddlOldCourse.SelectedValue));
            if (chkOldReg == 0)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Can not allot new course to student because registration for selected old course is not found in any session.", this.Page);
                return;
            }

            string courseno = objCommon.LookUp("ACD_COURSE_EQUIVALENCE", "OLD_COURSENO", "IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND OLD_COURSENO=" + ddlOldCourse.SelectedValue);
            string[] oldcname = ddlOldCourse.SelectedItem.Text.Split('-');
            string old_ccode = oldcname[0];
            string[] newcname = ddlNewCourse.SelectedItem.Text.Split('-');
            string new_ccode = newcname[0];
            if (ddlSession.SelectedValue != "") sessionno = Convert.ToInt32(ddlSession.SelectedValue); else sessionno = 0;
            if (ddlOldScheme.SelectedValue != "") old_scheme = Convert.ToInt32(ddlOldScheme.SelectedValue); else old_scheme = 0;
            if (ddlNewScheme.SelectedValue != "") new_scheme = Convert.ToInt32(ddlNewScheme.SelectedValue); else new_scheme = 0;
            if (ddlOldCourse.SelectedValue != "") old_course = Convert.ToInt32(ddlOldCourse.SelectedValue); else old_course = 0;
            if (ddlNewCourse.SelectedValue != "") new_course = Convert.ToInt32(ddlNewCourse.SelectedValue); else new_course = 0;
            string ip = ViewState["ipAddress"].ToString();
            string colcode = Session["colcode"] == null ? "0" : Session["colcode"].ToString();
            int uano = Convert.ToInt32(Session["userno"]);
            if (courseno != ddlOldCourse.SelectedValue)
            {
                CustomStatus cs = (CustomStatus)objCC.InsertEquivalenceCourses(Convert.ToInt32(ViewState["idno"]), old_scheme, new_scheme, old_course, new_course, old_ccode, new_ccode, sessionno, ip, colcode, uano);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Record saved successfully.", this.Page);
                }
                else if (cs.Equals(CustomStatus.RecordNotFound))
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Can not allot new course to student because registration for selected old course is not found in any session.", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Failed to save record.", this.Page);
                }
                lvCourse.Visible = true;
                BindListview();
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Can not allot course because the course you are selected is alredy registered.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_CertificateMaster.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlNewCourse.SelectedIndex = 0;
        ddlNewScheme.SelectedIndex = 0;
        ddlOldCourse.SelectedIndex = 0;
        ddlOldScheme.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        pnlCourseList.Visible = false;
        txtRegno.Text = string.Empty;
        ddlSession.Enabled = true;
        txtRegno.Enabled = true;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int eqno = Convert.ToInt32(btnEdit.CommandArgument);
            this.ShowDetails(eqno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_courseRegistration.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
  
    protected void ddlNewCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        double creditsold = 0.00, creditsnew = 0.00;

        creditsold = Convert.ToDouble(objCommon.LookUp("ACD_COURSE", "ISNULL(CREDITS,0)", "COURSENO=" + Convert.ToInt32(ddlOldCourse.SelectedValue)));
        creditsnew = Convert.ToDouble(objCommon.LookUp("ACD_COURSE", "ISNULL(CREDITS,0)", "COURSENO=" + Convert.ToInt32(ddlNewCourse.SelectedValue)));
        if (creditsold != creditsnew)
        {
            objCommon.DisplayMessage(UpdatePanel1, "The credits for old course and new course should be same.", this.Page);
            ddlNewCourse.SelectedIndex = 0;
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSession.SelectedIndex = 0;
        ddlSession.Items.Clear();

        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND COLLEGE_ID=" + ddlCollege.SelectedValue, "SESSIONNO DESC");
        }
        else
        {
            ddlSession.Items.Insert(0, "Please Select");
        }
    }

}
