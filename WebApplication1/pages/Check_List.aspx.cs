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
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using EASendMail;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;

public partial class MockUps_Check_List : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
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
                objUaimsCommon.ShowError(Page, "Academic_FeesRefundReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
        objCommon.FillDropDownList(ddlAcademicSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND IS_ACTIVE=1", "SESSIONNO");
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND STATUS=1", "SEMESTERNO");

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        string SP_Name2 = "PKG_ACD_GET_CHECK_LIST";
        string SP_Parameters2 = "@P_SEMESTERNO,@P_SESSIONNO";
        string Call_Values2 = "" + Convert.ToInt32(ddlSemester.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlAcademicSession.SelectedValue) + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlChecklist.Visible = true;
            lvCheckList.DataSource = ds;
            lvCheckList.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvCheckList);//Set label 
            objCommon.SetListViewHeaderLabel(Convert.ToString(Request.QueryString["pageno"]), lvCheckList);
        }
        else
        {
            pnlChecklist.Visible = false;
            lvCheckList.DataSource = null;
            lvCheckList.DataBind();
            ddlAcademicSession.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlAcademicSession.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
    }
}