
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
using System.IO;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

public partial class ACADEMIC_Activity_type_reports : System.Web.UI.Page
{
    Common objCommon = new Common();
    StudentController studcon = new StudentController();
    NCCActivityController Ncccon = new NCCActivityController();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //Check Session
                if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Page Authorization
                //    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Populate all the DropDownLists
                    FillDropDown();
                }
            }
            else
            {
                // Clear message div
                divMsg.InnerHtml = string.Empty;
            }
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_AdmissionReports.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page. 
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AdmissionReports.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AdmissionReports.aspx");
        }
    }

    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlActivity, "[ACD_ACTIVITY_NAME]", "DISTINCT ACTIVITY_NO", "ACTIVITY_NAME", "ACTIVITY_NO>0", "");
    }
    protected void ddlActivity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new System.Web.UI.WebControls.ListItem("Please Select", "0"));
            if (ddlActivity.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            }
            else
            {
                ddlDegree.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new System.Web.UI.WebControls.ListItem("Please Select", "0"));
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
                ddlBranch.Focus();
            }
            else
            {
                ddlDegree.SelectedIndex = 0;                
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSemester.SelectedIndex = 0;
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new System.Web.UI.WebControls.ListItem("Please Select", "0"));
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                ddlSemester.Focus();
            }
            else
            {
                ddlBranch.SelectedIndex = 0;
                ddlSemester.Focus();                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
    protected void btnNccReport_Click(object sender, EventArgs e)
    {
        ActiveStudentsExcel();
    }
    protected void btnNssReport_Click(object sender, EventArgs e)
    {
        LeftStudentsExcel();
    }
    protected void btnCampReport_Click(object sender, EventArgs e)
    {
        CampExcel();
    }

    protected void ActiveStudentsExcel()
    {
        int Activityno = Convert.ToInt32(ddlActivity.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlActivity.SelectedValue);

        DataSet ds = Ncccon.GetActiveStudents_Excel(Convert.ToInt32(ddlActivity.SelectedItem.Value),Convert.ToInt32(ddlBranch.SelectedValue),Convert.ToInt32(ddlSemester.SelectedValue));
        DataGrid dg = new DataGrid();
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string attachment = "attachment; filename=" + "ActiveStudents_ExcelReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            dg.DataSource = ds.Tables[0];
            dg.DataBind();
            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            ClearAll();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No Data Found for this Selection", this.Page);
            ClearAll();
            return;
        }
        
    }
    private void LeftStudentsExcel()
    {
        DataSet ds = Ncccon.GetLeftStudents_Excel(Convert.ToInt32(ddlActivity.SelectedItem.Value),Convert.ToInt32(ddlBranch.SelectedValue),Convert.ToDateTime(txtFromDate.Text),Convert.ToDateTime(txtTodate.Text));
        DataGrid dg = new DataGrid();
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string attachment = "attachment; filename=" + "LeftStudentsExcelReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            dg.DataSource = ds.Tables[0];
            dg.DataBind();
            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            ClearAll();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No Data Found for this Selection", this.Page);
            ClearAll();
            return;
        }

    }
    private void CampExcel()
    {
        DataSet ds = Ncccon.GetCampDetailsStudents_Excel(Convert.ToInt32(ddlActivity.SelectedItem.Value), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text));
        DataGrid dg = new DataGrid();
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string attachment = "attachment; filename=" + "CampDetailsStudentsExcelReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            dg.DataSource = ds.Tables[0];
            dg.DataBind();
            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            ClearAll();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No Data Found for this Selection", this.Page);
            ClearAll();
            return;
        }

    }


    protected void ClearAll()
    {
        
        ddlActivity.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        //ddlDegree.Items.Clear();
        //ddlDegree.Items.Add("Please Select");
        ddlSemester.SelectedIndex = 0;
       // ddlSemester.Items.Clear();
       // ddlSemester.Items.Add("Please Select");
        ddlBranch.SelectedIndex = 0;
        txtFromDate.Text = txtTodate.Text=string.Empty;
        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
}