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

public partial class ACADEMIC_UploadVIPData : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    protected void Page_Load(object sender, EventArgs e)
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
                //this.CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }

            }

            ViewState["action"] = "add";
            objCommon.SetLabelData("0");//for label
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header

        }
    }
    protected void lbExcelFormat_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/vnd.ms-excel";
        string path = Server.MapPath("~/ExcelFormat/Preformat_Of_Employe_ entry_data_sheet.xls");
        Response.AddHeader("Content-Disposition", "attachment;filename=\"Preformat_Of_Employe_ entry_data_sheet.xls\"");
        Response.TransmitFile(path);
        Response.End();
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {

            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Grade.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=Grade.aspx");
        }
    }
}