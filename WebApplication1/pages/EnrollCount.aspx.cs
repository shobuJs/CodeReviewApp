using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Web.Configuration;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Security;
using mastersofterp;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using EASendMail;

public partial class ACADEMIC_EnrollCount : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
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
                    this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    if (Request.QueryString["RecType"] != null && Request.QueryString["RecType"].ToString() != null)
                        ViewState["ReceiptType"] = Request.QueryString["RecType"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                    objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "", "BATCHNO DESC");
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                }
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACDEMIC_COLLEGE_MASTER.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=EnrollCount.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=EnrollCount.aspx");
        }
    }

    private void BindList()
    {
        try
        {
            DataSet ds = objSC.GetEnrollCount(Convert.ToInt32(ddlIntake.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                DivStatus.Visible = true;
                divenroll.Visible = true;
                lbldoc.Text = ds.Tables[0].Rows[0]["DOCUPLOAD"].ToString();
                lblhund.Text = ds.Tables[0].Rows[0]["HUNDREAD"].ToString();
                lbltwentyfive.Text = ds.Tables[0].Rows[0]["TWENTYFIVE"].ToString();
                lblzero.Text = ds.Tables[0].Rows[0]["ZEROPER"].ToString();
                lblMark.Text = ds.Tables[0].Rows[0]["MARKENTRY"].ToString();
                lblmerit.Text = ds.Tables[0].Rows[0]["MERITLIST"].ToString();
                lblCompayof.Text = ds.Tables[0].Rows[0]["COMPAYOF"].ToString();
                lblincof.Text = ds.Tables[0].Rows[0]["INCPAYOF"].ToString();
                lblComponn.Text = ds.Tables[0].Rows[0]["COMPAYON"].ToString();
                lbltotalenroll.Text = ds.Tables[0].Rows[0]["TOTALENROLLED"].ToString();
                lblpgmtransfer.Text = ds.Tables[0].Rows[0]["PGMTRANSFER"].ToString();
                lbldirectreg.Text = ds.Tables[0].Rows[0]["DIRECTADM"].ToString();
                lbltotalapp.Text = ds.Tables[0].Rows[0]["TOTALAPPLIED"].ToString();
                lbladmcan.Text = ds.Tables[0].Rows[0]["ADMCANCEL"].ToString();
                lblintaketransfer.Text = ds.Tables[0].Rows[0]["INTAKETRANSFER"].ToString();
                lblpost.Text = ds.Tables[0].Rows[0]["POSTPONEMENT"].ToString();
                pnllst.Visible = true;
                LvenrollCount.DataSource = ds;
                LvenrollCount.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Record Not Found", this.Page);
                DivStatus.Visible = false;
                divenroll.Visible = false;
                pnllst.Visible = false;
                LvenrollCount.DataSource = null;
                LvenrollCount.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_COLLEGE_MASTER.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }   
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlIntake.SelectedIndex = 0;
        DivStatus.Visible = false;
        divenroll.Visible = false;
        pnllst.Visible = false;
        LvenrollCount.DataSource = null;
        LvenrollCount.DataBind();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindList();
    }
}