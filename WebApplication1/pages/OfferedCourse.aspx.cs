using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_OfferedCourse : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    UAIMS_Common objUCommon = new UAIMS_Common();


    #region Page Events

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

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                    PopulateDropDownList();
                    btnPrint.Visible = false;
                    btnAd.Visible = false;
                    btnCancel.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=OfferedCourse.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
        }
    }
    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "distinct SESSIONNO", "SESSION_PNAME", string.Empty, "SESSIONNO DESC");
        if (Session["usertype"].ToString() != "1")
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "distinct SCHEMENO", "SCHEMENAME", "SCHEMENO>0 AND DEPTNO=" + Session["userdeptno"].ToString(), "SCHEMENAME");
        else
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "distinct SCHEMENO", "SCHEMENAME", "SCHEMENO>0", "SCHEMENAME");
    }
    #endregion

    protected void btnAd_Click(object sender, EventArgs e)
    {
        string offcourse = string.Empty;
        string sem = string.Empty;
        string sqno = string.Empty;
        try
        {
            CourseController objCC = new CourseController();

            int SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);

            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {
                CheckBox chkBox = dataitem.FindControl("chkoffered") as CheckBox;
                DropDownList ddl = dataitem.FindControl("ddlsem") as DropDownList;
                TextBox txtseqno = dataitem.FindControl("txtseqno") as TextBox;


                if (chkBox.Checked == true)
                {
                    if (txtseqno.Text == "")
                    {

                        objCommon.DisplayMessage(this.updpnl, "Please enter Seq.no", this.Page);
                        goto noresult;
                    }

                    offcourse += chkBox.ToolTip + ",";
                    sem += ddl.SelectedValue + ",";
                    sqno += txtseqno.Text + ",";
                }
            }

            CustomStatus cs = (CustomStatus)objCC.UpdateOfferedCourse(SchemeNo, offcourse, sem, sqno);

            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                BindListView();
                objCommon.DisplayMessage(this.updpnl, "Offered Courses saved Successfully.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updpnl, "Error in Saving", this.Page);
            }
        noresult:
            { }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Offered_Course.btnAd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListView()
    {
        try
        {
            int schemeno = int.Parse(ddlScheme.SelectedValue);

            DataSet dsfaculty = null;
            CourseController objStud = new CourseController();

            dsfaculty = objStud.GetCourseOffered(schemeno);
            if (dsfaculty != null && dsfaculty.Tables.Count > 0 && dsfaculty.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = dsfaculty;
                lvCourse.DataBind();
                pnlCourse.Visible = true;
            }
            else
            {
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                objCommon.DisplayMessage(this.updpnl, "Record Not Found!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Offered_Course.BindListView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lvCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        DropDownList ddl = dataitem.FindControl("ddlsem") as DropDownList;
        Label lblSem = dataitem.FindControl("LblSemNo") as Label;
        CheckBox ChkOffer = dataitem.FindControl("chkoffered") as CheckBox;
        objCommon.FillDropDownList(ddl, "ACD_SEMESTER ", "distinct SEMESTERNO", "SEMESTERNAME", string.Empty, "SEMESTERNO");
        ddl.SelectedValue = lblSem.Text;
        ChkOffer.Checked = lblSem.ToolTip.Equals("1") ? true : false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        cancel();
    }
    protected void cancel()
    {
        ddlSession.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue != "0")
        {
            string[] sno = ddlScheme.SelectedValue.Split('-');
            ShowReport("Check_List", "rptSubjectCourseListSchemewiseOfferedCourses.rpt");
        }
        else
        {
            objCommon.DisplayMessage(this.updpnl, "Please Select Scheme", this.Page);
            return;
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

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + 0;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_RollListForScrutiny.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAd.Visible = true;
        btnCancel.Visible = true;
        btnPrint.Visible = true;
        BindListView();
    }
}
