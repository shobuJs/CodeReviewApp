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

public partial class ACADEMIC_CoursewiseLevel : System.Web.UI.Page
{

    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    CourseController CController = new CourseController();

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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
            // degreename
            lblSession.Text = Session["sessionname"].ToString();
            lblSession.ToolTip = Session["currentsession"].ToString();

            PopulateDropDownList();
            ViewState["action"] = "add";
            BindDLevelCreaionList();

        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlTerm, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"].ToString()), "SESSION_NAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_LevelMaster.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindDLevelCreaionList()
    {
        try
        {
            DataSet dsLevel = CController.GetLevelName();
            if (dsLevel != null && dsLevel.Tables.Count > 0 && dsLevel.Tables[0].Rows.Count > 0)
            {
                lvLevelCreation.DataSource = dsLevel;
                lvLevelCreation.DataBind();
            }

            else
            {
                lvLevelCreation.DataSource = null;
                lvLevelCreation.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_LevelMaster.BindDLevelCreaionList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int levelno = int.Parse(btnEdit.CommandArgument);

            ShowDetail(levelno);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_LevelMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int levelno)
    {

        SqlDataReader dr = CController.GetLevelNo(levelno);

        //Show Complaint type Detail
        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["levelno"] = levelno.ToString();
                txtLevel.Text = dr["LEVEL_DESC"] == null ? string.Empty : dr["LEVEL_DESC"].ToString();
                txtAdmBatch.Text = dr["ADMBATCH"] == null ? string.Empty : dr["ADMBATCH"].ToString();
                txtNoOfCourses.Text = dr["NO_COURSES"] == null ? string.Empty : dr["NO_COURSES"].ToString();
                txtTheory.Text = dr["CP_TH"] == null ? string.Empty : dr["CP_TH"].ToString();
                txtPractical.Text = dr["CP_PR"] == null ? string.Empty : dr["CP_PR"].ToString();
                txtThMarks.Text = dr["MARKS_TH"] == null ? string.Empty : dr["MARKS_TH"].ToString();
                txtPrMarks.Text = dr["MARKS_PR"] == null ? string.Empty : dr["MARKS_PR"].ToString();

            }
        }
        if (dr == null) dr.Close();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ControlClear();
        BindDLevelCreaionList();
    }

    private void ControlClear()
    {
        if (ddlTerm.Items.Count > 0)
            ddlTerm.SelectedIndex = 0;

        txtLevel.Text = string.Empty;
        txtNoOfCourses.Text = string.Empty;
        txtPractical.Text = string.Empty;
        txtPrMarks.Text = string.Empty;
        txtTheory.Text = string.Empty;
        txtThMarks.Text = string.Empty;
        txtAdmBatch.Text = string.Empty;
        ViewState["action"] = "add";
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            string lvlNO = "-100";
            lvlNO = objCommon.LookUp("ACD_COURSE_LEVEL", "LEVELNO", "ADMBATCH=" + Convert.ToInt32(txtAdmBatch.Text) + "AND LEVEL_DESC='" + txtLevel.Text + "'");
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)CController.AddLevel(txtLevel.Text.ToString(), Convert.ToInt32(txtAdmBatch.Text), Convert.ToInt32(txtNoOfCourses.Text), Convert.ToInt32(txtTheory.Text), Convert.ToInt32(txtPractical.Text), Convert.ToInt32(txtThMarks.Text), Convert.ToInt32(txtPrMarks.Text), Session["colcode"].ToString());
                    if (cs.Equals(CustomStatus.TransactionFailed))
                        objCommon.DisplayMessage(this.pnlLevel, "Error in Level Create Process!!", this.Page);
                    else
                        if (cs.Equals(CustomStatus.DuplicateRecord))
                            objCommon.DisplayMessage(this.pnlLevel, "Already Exist Level Name!!", this.Page);
                        else
                        {
                            objCommon.DisplayMessage(this.pnlLevel, "New Level Created Successfully!!", this.Page);
                            ControlClear();
                            BindDLevelCreaionList();
                        }
                }
                else
                {
                    //edit
                    if (lvlNO == "")
                    {
                        if (ViewState["levelno"] != null)
                        {
                            int levelno = Convert.ToInt32(ViewState["levelno"].ToString());
                            CustomStatus cs = (CustomStatus)CController.UpdateLevel(levelno, txtLevel.Text.ToString(), Convert.ToInt32(txtAdmBatch.Text), Convert.ToInt32(txtNoOfCourses.Text), Convert.ToInt32(txtTheory.Text), Convert.ToInt32(txtPractical.Text), Convert.ToInt32(txtThMarks.Text), Convert.ToInt32(txtPrMarks.Text));
                            if (cs.Equals(CustomStatus.RecordUpdated))
                                objCommon.DisplayMessage(this.pnlLevel, "Level Update Successfully!!", this.Page);

                            ControlClear();
                            BindDLevelCreaionList();

                        }

                    }
                    else
                        objCommon.DisplayMessage(this.pnlLevel, "Level With this Name and Admission Batch already exist!!", this.Page);
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_DivisionMaster.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=CoursewiseLevel.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CoursewiseLevel.aspx");
        }
    }
}
