using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Web.Configuration;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Security;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.Specialized;

public partial class MockUps_Promissory_Note_Approval : System.Web.UI.Page
{
    private Common objCommon = new Common();
    StandardFeeController objsfc = new StandardFeeController();

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
                    this.CheckPageAuthorization();
                    ViewState["action"] = "add";
                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FeesDetails.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeesDetails.aspx");
        }
    }
    protected void rdobtn_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string SP_Name = "PKG_ACD_INSERT_UPDATE_GET_STUDENT_PROMISSORY_DETAILS";
            string SP_Parameters = "@P_STATUS,@P_COMMAND_TYPE";
            string Call_Values = "" + Convert.ToInt32(rdobtn.SelectedValue) + "," + 1 + "";

                DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    lvPromissoryNote.DataSource = ds.Tables[0];
                    lvPromissoryNote.DataBind();
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Record Not Found !!!", this.Page);
                    lvPromissoryNote.DataSource = null;
                    lvPromissoryNote.DataBind();
                }
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void lnkEnroll_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            lblStudentID.Text = lnk.CommandArgument.Split('$')[0];
            lblStudentName.Text = lnk.CommandArgument.Split('$')[1];
            lblCollege.Text = lnk.CommandArgument.Split('$')[2];
            lblMobile.Text = lnk.CommandArgument.Split('$')[3];
            lblEmail.Text = lnk.CommandArgument.Split('$')[4];
            lblProgram.Text = lnk.CommandArgument.Split('$')[5];
            lblDueAmt.Text = lnk.CommandArgument.Split('$')[6];
            ddlStatus.SelectedValue = lnk.CommandArgument.Split('$')[8];
            txtReason.InnerText = lnk.CommandArgument.Split('$')[7];
            txtPromissoryDate.Text = lnk.CommandArgument.Split('$')[9];
            Remark.InnerText = lnk.CommandArgument.Split('$')[10];
            ViewState["SRNO"] = lnk.CommandName.ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#StudentModal').modal('show')", true);
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string Call_Values1 = "";
            string SP_Name1 = "PKG_ACD_INSERT_UPDATE_GET_STUDENT_PROMISSORY_DETAILS";
            string SP_Parameters1 = "@P_DUE_DATE,@P_STATUS,@P_REMARK,@P_COMMAND_TYPE,@P_UA_NO,@P_SRNO,@P_OUTPUT";
            Call_Values1 = "" + txtPromissoryDate.Text + "," + ddlStatus.SelectedValue + "," +
            Remark.InnerText.Replace(",","") + "," + 2 + "," + Session["userno"].ToString() + "," + ViewState["SRNO"].ToString() + ",0";

            string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);

            if (que_out1 == "1")
            {
                objCommon.DisplayMessage(this.Page, "Promissory Note Updated Successfully!", this.Page);
                rdobtn_SelectedIndexChanged(new object(), new EventArgs());
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Something Wents Wrong", this.Page);
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btn_excel_download_Click(object sender, EventArgs e)
    {
        try
        {
            //string SP_Name2 = "PKG_ACD_STUDENT_PROMISSORY";
            //string SP_Parameters2 = null;   
            //string Call_Values2 = null;     

            //DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);


            DataSet ds = objsfc.GetPromissoryDetails();
            DataGrid dg = new DataGrid();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                RenameColumnIfExists(dt, "INSTALLMENT NO1", "INSTALLMENT NO 2");
                RenameColumnIfExists(dt, "FEES PAID1", "FEES PAID 2");
                RenameColumnIfExists(dt, "DUE AMOUNT1", "DUE AMOUNT 2");
                RenameColumnIfExists(dt, "PROMISSORY NOTE STATUS1", "PROMISSORY NOTE STATUS 2");
                RenameColumnIfExists(dt, "PROMISSORY DATE OF PAYMENT1", "PROMISSORY DATE OF PAYMENT 2");

                RenameColumnIfExists(dt, "INSTALLMENT NO2", "INSTALLMENT NO 3");
                RenameColumnIfExists(dt, "FEES PAID2", "FEES PAID 3");
                RenameColumnIfExists(dt, "DUE AMOUNT2", "DUE AMOUNT 3");
                RenameColumnIfExists(dt, "PROMISSORY NOTE STATUS2", "PROMISSORY NOTE STATUS 3");
                RenameColumnIfExists(dt, "PROMISSORY DATE OF PAYMENT2", "PROMISSORY DATE OF PAYMENT 3");

                string attachment = "attachment; filename=" + "PromissoryReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/" + "ms-excel";
                System.IO.StringWriter sw = new System.IO.StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                dg.DataSource = ds.Tables[0];
                dg.DataBind();
                dg.HeaderStyle.Font.Bold = true;
                dg.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Record Not Found !!!", this.Page);
                return;
            }


        }
        catch (Exception)
        {
            throw;
        }
    }

    private void RenameColumnIfExists(DataTable dt, string oldName, string newName)
    {
        if (dt.Columns.Contains(oldName))
            dt.Columns[oldName].ColumnName = newName;
    }
}