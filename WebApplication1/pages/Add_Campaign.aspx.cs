
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
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
public partial class Add_Campaign : System.Web.UI.Page
{
    CourseController objCourse = new CourseController();
    UAIMS_Common objUCommon = new UAIMS_Common();
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
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {

                //Page Authorization
                this.CheckPageAuthorization();
                if (Session["usertype"].ToString() == "1" && Session["username"].ToString().ToUpper().Contains("PRINCIPAL"))
                {
                    // divShow.Visible=true;
                }
                else
                {
                    // divShow.Visible=false;
                }

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                }
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
            }
            objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNAME DESC");
            objCommon.FillDropDownList(ddlStudyLevel, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION > 0", "UA_SECTION");


        }

    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Add_Campaign.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Add_Campaign.aspx");
        }
    }
    protected void ddlStudyLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStudyLevel.SelectedValue != "0")
        {
            ListViewDataBind();
        }
        else
        {
            lvDetails.DataSource = null;
            lvDetails.DataBind();
        }
    }
    protected void ListViewDataBind()
    {
        DataSet ds = objCourse.GetDegreeBranchCanpaign(Convert.ToInt32(ddlStudyLevel.SelectedValue));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvDetails.DataSource = ds;
            lvDetails.DataBind();

        }
        else
        {
            lvDetails.DataSource = null;
            lvDetails.DataBind();
            objCommon.DisplayMessage(this, "Record Not Found...", this.Page);
            return;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //string SP_Name1 = "PKG_GET_FACULTY_WISE_COURSE_LIST_HOD_COUNTRECORD";
        //string SP_Parameters1 = "@P_SESSIONNO,@P_UA_NO";
        //string Call_Values1 = "" + Convert.ToInt32(ddlSession.SelectedValue.ToString()) + "," + Convert.ToInt32(Session["userno"].ToString()) + "";
        //DataSet ds = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);
        int College_id = 0; int Degreeno = 0; int Branchno = 0;
        int Afilated = 0; 
        int Intake = Convert.ToInt32(ddlIntake.SelectedValue);
        int UGPG = Convert.ToInt32(ddlStudyLevel.SelectedValue);
        CustomStatus cs = 0;
        int Count = lvDetails.Items.Count;
        if (Count == 0)
        {
            lvDetails.DataSource = null;
            lvDetails.DataBind();
        }
        foreach (ListViewDataItem dataitem in lvDetails.Items)
        {
            int TotalSIgnUp = 0; int TotalAppFilled = 0;
            int TotalPaid = 0; int Admitted = 0;
            Label lblCollege_id = dataitem.FindControl("lblCollege_id") as Label;
            Label lblDegreeno = dataitem.FindControl("lblDegreeno") as Label;
            Label lblBranchno = dataitem.FindControl("lblBranchno") as Label;
            Label lblAfilated = dataitem.FindControl("lblAfilated") as Label;
            TextBox txtTotalSIgnUp = dataitem.FindControl("txtTotalSIgnUp") as TextBox;
            TextBox txtTotalAppFilled = dataitem.FindControl("txtTotalAppFilled") as TextBox;
            TextBox txtTotalPaid = dataitem.FindControl("txtTotalPaid") as TextBox;
            TextBox txtAd = dataitem.FindControl("txtAd") as TextBox;
            College_id = Convert.ToInt32(lblCollege_id.Text);
            Degreeno = Convert.ToInt32(lblDegreeno.Text);
            Branchno = Convert.ToInt32(lblBranchno.Text);
            Afilated = Convert.ToInt32(lblAfilated.Text);
            if (txtTotalSIgnUp.Text != "")
            {
                TotalSIgnUp = Convert.ToInt32(txtTotalSIgnUp.Text);
            }
            if (txtTotalAppFilled.Text != "")
            {
                TotalAppFilled = Convert.ToInt32(txtTotalAppFilled.Text);
            }
            if (txtTotalPaid.Text != "")
            {
                TotalPaid = Convert.ToInt32(txtTotalPaid.Text);
            }
            if (txtAd.Text != "")
            {
                Admitted = Convert.ToInt32(txtAd.Text);
            }
            
          
          

            cs = (CustomStatus)objCourse.InsertCampaignData(Intake, UGPG,College_id, Degreeno, Branchno, Afilated, TotalSIgnUp, TotalAppFilled, TotalPaid, Admitted);

        }
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            ListViewDataBind(); 
            objCommon.DisplayMessage(this, "Record Saved Successfully.", this.Page);
            return;
        }
        else if (cs.Equals(CustomStatus.TransactionFailed))
        {
            ListViewDataBind(); 
            objCommon.DisplayMessage(this, "Failed To Save Record ", this.Page);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());   
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        string SP_Name1 = "PKG_ACD_GET_COURSE_DEGREE_CAMPAIGN_EXCEL";
        string SP_Parameters1 = "@P_INTAKE,@P_UGPG";
        string Call_Values1 = "" + Convert.ToInt32(ddlIntake.SelectedValue) + "," + Convert.ToInt32(ddlStudyLevel.SelectedValue)+"";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Campaignlist.xls");
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            DataGrid datagrid = new DataGrid();

            if (ds.Tables.Count > 0)
            {
                datagrid.DataSource = ds.Tables[0];
                datagrid.DataBind();
            }
            datagrid.HeaderStyle.Font.Bold = true;
            datagrid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this, "Record Not found.", this.Page);
            return;
        }
    }
}