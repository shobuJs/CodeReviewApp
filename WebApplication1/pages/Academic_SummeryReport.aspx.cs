using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Data;


public partial class ACADEMIC_Academic_SummeryReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

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
                if (Session["userno"] == null || Session["username"] == null ||
                      Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Page Authorization
                    this.CheckPageAuthorization();

                    Page.Title = Session["coll_name"].ToString();
                    ViewState["ReportType"] = null;
                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO desc");
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_Academic_SummeryReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=Academic_SummeryReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Academic_SummeryReport.aspx");
        }
    }

    private void Export()
    {
        string attachment = "attachment; filename=" + "Student_Strength.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        DataSet dsfee = objSC.GetStudentStrengthDetails(Convert.ToInt32(ddlSession.SelectedValue), ViewState["ReportType"].ToString());
        DataGrid dg = new DataGrid();
        if (dsfee.Tables.Count > 0)
        {
            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }


    protected void btnShowReports_Click(object sender, EventArgs e)
    {
        try
        {
            string ReportType = string.Empty;
            foreach (ListItem item in chkReportType.Items)
            {
                if (item.Selected == true)
                {
                    ReportType = ReportType + item.Text.ToString() + ",";
                }
            }

            if (ReportType != "")
            {
                if (ReportType.Substring(ReportType.Length - 1) == ",")
                {
                    ReportType = ReportType.Substring(0, ReportType.Length - 1);

                }
            }
            else
            {
                dvGrid.Visible = false;
                objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Atleast One Report Type!", this.Page);
                btnShowReports.Visible = false;
                lstDetails.DataSource = null;
                lstDetails.DataBind();
                return;
            }
            ViewState["ReportType"] = ReportType + " ";
            dvGrid.Visible = true;
            Export();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_Academic_SummeryReport.btnShowReports_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        string ReportType = string.Empty;
        foreach (ListItem item in chkReportType.Items)
        {
            if (item.Selected == true)
            {
                ReportType = ReportType + item.Text.ToString() + ",";
            }
        }
        if (ReportType != "")
        {
            if (ReportType.Substring(ReportType.Length - 1) == ",")
            {
                ReportType = ReportType.Substring(0, ReportType.Length - 1);

            }
        }
        else
        {
            dvGrid.Visible = false;
            objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Atleast One Report Type!", this.Page);
            btnShowReports.Visible = false;
            lstDetails.DataSource = null;
            lstDetails.DataBind();
            return;
        }
        ViewState["ReportType"] = ReportType + " ";

        DataSet ds = objSC.GetStudentStrengthDetails(Convert.ToInt32(ddlSession.SelectedValue), ViewState["ReportType"].ToString());
        if (ds.Tables.Count > 0 && ds.Tables != null)
        {
            // panell.Visible = true;
            btnShowReports.Visible = true;
            lstDetails.DataSource = ds;
            lstDetails.DataBind();
            dvGrid.Visible = true;
        }
        else
        {
            //  panell.Visible = false;
            btnShowReports.Visible = false;
            lstDetails.DataSource = null;
            lstDetails.DataBind();
            dvGrid.Visible = false;
        }
    }

}