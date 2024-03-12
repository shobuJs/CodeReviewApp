//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : SUDENT STRENGTH REPORT AND CATEGORY STRENGTH REPORT                             
// CREATION DATE : 22-JULY-2009                                                          
// CREATED BY    : MANGESH BARMATE    
// ADDED BY      : ASHISH DHAKATE
// ADDED DATE    : 20 DEC 2011                                              
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class Academic_StudentStrengthReport : System.Web.UI.Page
{
    IITMS.UAIMS.Common objCommon = new IITMS.UAIMS.Common();
    IITMS.UAIMS_Common objUCommon = new IITMS.UAIMS_Common();

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
                GetDegree();
            }
            divMsg.InnerHtml = "";
        }
        catch (Exception ex)
        {
          if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentStrengthReport.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
       
    }
   
    private void GetDegree()
    {
        StudentController studCont = new StudentController();
        DataSet ds = studCont.GetStudentDegreeFill();
        if (ds != null && ds.Tables.Count > 0)
        {
            lvDegree.DataSource = ds.Tables[0];
            lvDegree.DataBind();
        }
    }
    
    private void ShowReportForStudMaleAndFemaleOnDegree(string param, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",username=" + Session["userfullname"].ToString() + ",currentSession=" + DateTime.Today.Year.ToString() + "-" + (DateTime.Today.Year + 1).ToString() + ",@P_DEGREENO=" + param;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentStrengthReport.ShowReportForStudMaleAndFemaleOnDegree() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    
    private void ShowReport(string param, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=collegename=" + Session["coll_name"].ToString() + ",username=" + Session["userfullname"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString()+" ,@P_DEGREENO=" + param; ;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentReport1.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            this.ShowReportForStudMaleAndFemaleOnDegree(GetParameterForDegreeNo(), "STUDENTSTRENGTHREPORT", "StudentStrengthDetails.rpt");
        }
        catch (Exception ex)
        {
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "StudentStrengthDetails.btnShowReport_Click --> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server Unavailable.");
            }
        }
    }
    private string GetParameterForDegreeNo()
    {
        string strDegreeNo = string.Empty;
        foreach (ListViewDataItem item in lvDegree.Items)
        {
            if ((item.FindControl("chkCources") as CheckBox).Checked)
            {
                if (strDegreeNo.Length > 0)
                    strDegreeNo += ".";
                strDegreeNo += (item.FindControl("hidDegreeNo") as HiddenField).Value;
            }
        }
        return strDegreeNo;
    }
    protected void lvDegree_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnCategory_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport(GetParameterForDegreeNo(), "STUDENT_STRENGTH_BY_CATEGORY", "StudentStrengthByCategory1.rpt");
        }
        catch (Exception ex)
        {
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "StudentStrengthByCategory.btnCategory_Click --> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server Unavailable.");
            }
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=studentStrengthReport.aspx");
        }
    }
}