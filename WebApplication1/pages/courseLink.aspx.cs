//=================================================================================
// PROJECT NAME  : PERSONNEL REQUIREMENT MANAGEMENT                                
// MODULE NAME   : TO CREATE FIRST YEAR COURSE ENTRY                               
// CREATION DATE : 3-May-2009
// CREATED BY    : NIRAJ D. PHALKE & ASHWINI BARBATE                               
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

public partial class Registration_courseLink : System.Web.UI.Page
{
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    CheckBox chkBox;

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

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                else
                    lblHelp.Text = "No Help Added";

                lblCurrentSession.Text = Session["currentsession"].ToString();

                //Populate the user dropdownlist 
                PopulateDropDownList();
            }
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            FillScheme();
            objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNO ASC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_courseLink.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillScheme()
    {
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
            DataSet ds = null;

            SqlParameter[] objParams = new SqlParameter[0];
            ds = objSQLHelper.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_NEWSCHEME", objParams);

            ddlScheme.DataSource = ds;
            ddlScheme.DataValueField = ds.Tables[0].Columns["schemeno"].ToString();
            ddlScheme.DataTextField = ds.Tables[0].Columns["schemename"].ToString();
            ddlScheme.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_courseLink.FillScheme-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex <= 0)
            {
                lblstatus.Text = "Please Select Scheme and Section";
                return;
            }

            CourseLinkController objCLC = new CourseLinkController();
            CourseLink objCL = new CourseLink();
            string ccodes = string.Empty;
            string coursenos = string.Empty;
            objCL.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
            objCL.SectionNo = Convert.ToInt32(ddlSection.SelectedValue);

            foreach (ListViewDataItem lvItem in lvCourseLink.Items)
            {
                chkBox = lvItem.FindControl("cbRow") as CheckBox;
                if (chkBox.Checked == true)
                {
                    Label show = (lvItem.FindControl("lblCCODE")) as Label;
                    ccodes += show.Text + ",";
                    coursenos += show.ToolTip + ",";
                }
            }
            objCL.CourseNos = coursenos;
            objCL.CourseCodes = ccodes;
            objCL.CollegeCode = Session["colcode"].ToString();
            CustomStatus cs = (CustomStatus)objCLC.AddCourses(objCL);
            lblstatus.Text = "Record Saved Successfully...";
            ddlScheme.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            txtTotChk.Text = "";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_courseLink.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtTotChk.Text = string.Empty;
        ddlSection.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        lblstatus.Text = string.Empty;
    }

    private void BindListView()
    {
        try
        {
            int schemeno = int.Parse(ddlScheme.SelectedValue);
            int sectionno = Convert.ToInt32(ddlSection.SelectedValue);
            CourseLinkController objCLC = new CourseLinkController();

            DataSet ds = objCLC.GetAllCourse(schemeno, sectionno);
            lvCourseLink.DataSource = ds;
            lvCourseLink.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_courseLink.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lvCourseLink_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        hdfTot.Value = (Convert.ToInt16(hdfTot.Value) + 1).ToString();
        ListViewDataItem dataItem = (ListViewDataItem)e.Item;
        CheckBox chk = dataItem.FindControl("cbRow") as CheckBox;
        if (chk.ToolTip != "0")
            chk.Checked = true;

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=courselink.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=courselink.aspx");
        }
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfTot.Value = "0";
        BindListView();
    }
}
