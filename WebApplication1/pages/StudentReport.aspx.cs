//======================================================================================
// PROJECT NAME  : RFC SVCE                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Student Report                                    
// CREATION DATE : 24/05/19
// CREATED BY    : Ankush T                                                
// MODIFIED DATE : 
// MODIFIED BY   : 
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


public partial class Academic_StudentReport : System.Web.UI.Page
{
    IITMS.UAIMS.Common objCommon = new IITMS.UAIMS.Common();
    IITMS.UAIMS_Common objUCommon = new IITMS.UAIMS_Common();
    //USED FOR INITIALSING THE MASTER PAGE
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    //USED FOR BYDEFAULT LOADING THE default PAGE
    protected void Page_Load(object sender, EventArgs e)
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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }

            }
        }
        divMsg.InnerHtml = "";
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentReport.aspx");
        }
    }
    //Button Search used for display Student details in lvStudentRecords list view.
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        StudentController studCont = new StudentController();
        string searchText = txtSearchText.Text.Trim();
        string searchBy = (rdoRegno.Checked ? "REGNO" : rdoEnrollmentNo.Checked ? "ENROLLNO" : (rdoTan.Checked ? "TAN" : "NAME"));
        DataSet ds = studCont.RetrieveStudentDetails(searchText, searchBy); ;
        if (ds != null && ds.Tables.Count > 0)
        {
            lvStudentRecords.DataSource = ds.Tables[0];
            lvStudentRecords.DataBind();
        }
    }
    //if check radio button Vertical report then display STUDENT ADMISSION DETAILS  report in StudentReport1.rpt file  ELSE check radio button Horizontal report then display STUDENT ADMISSION DETAILS  report in studentReportHorizontal.rpt file 
    protected void btnShowReport(object sender, EventArgs e)
    {
        if (rdoVerticalReport.Checked)
        {
            ImageButton btnShowRpt = sender as ImageButton;
            string param = this.GetParamsForSingleStudent(btnShowRpt.CommandArgument);
            this.ShowReport(param, "STUDENTADMISSIONDETAILS", "StudentReport1.rpt");
        }
        else
        {
            ImageButton btnShowRpt = sender as ImageButton;
            string param = this.GetParamsForSingleStudent(btnShowRpt.CommandArgument);
            this.ShowReport(param, "STUDENTADMISSIONDETAILS", "studentReportHorizontal.rpt");
        }
    }
    //if check radio button Vertical report then display STUDENT ADMISSION DETAILS  report in StudentReport1.rpt file  ELSE check radio button Horizontal report then display STUDENT ADMISSION DETAILS  report in studentReportHorizontal.rpt file 
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (rdoVerticalReport.Checked)
        {
            ImageButton btnShowRpt = sender as ImageButton;
            string param = this.GetParamsForSingleStudent(btnShowRpt.CommandArgument);
            this.ShowReport(param, "STUDENTADMISSIONDETAILS", "StudentReport1.rpt");
        }
        else
        {
            ImageButton btnShowRpt = sender as ImageButton;
            string param = this.GetParamsForSingleStudent(btnShowRpt.CommandArgument);
            this.ShowReport(param, "STUDENTADMISSIONDETAILS", "studentReportHorizontal.rpt");
        }
    }
    //showing the report in pdf formate as per as  selection of report name  or file name.
    private void ShowReport(string param, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=collegename=" + Session["coll_name"].ToString() + ",username=" + Session["userfullname"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + param;
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

    private string GetParamsForAllStudent()
    {
        return string.Empty;
    }

    private string GetParamsForSingleStudent(string idno)
    {
        string param = "@P_IDNO=" + idno;
        return param;
    }
    //refresh or reload the Current page
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}
