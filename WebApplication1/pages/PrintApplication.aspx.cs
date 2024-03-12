using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PrintApplication : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    int idno;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, string.Empty);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["coll_name"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                if (Session["usertype"].ToString() == "2")
                {
                    idno = Convert.ToInt32(Session["idno"] == null ? 0 : Session["idno"]);
                    
                }

            }

        }

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Student Registration", "UserInfo.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            
           // Session["idno"] = objCommon.LookUp("USER_ACC", "ISNULL(UA_IDNO,0)", "UA_NO = '" + Session["userno"].ToString().Trim() + "'");
        

                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_IDNO=" + Session["studentID"] + ",@P_COLLEGE_CODE=" + Session["colcode"];


                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";
           

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }


}