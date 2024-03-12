//---------------------------------------------//
// CREATED BY   : SAKSHI MOHADIKAR
// CREATED DATE : 23/11/2023
//---------------------------------------------//

using BusinessLogicLayer.BusinessEntities.Academic;
using BusinessLogicLayer.BusinessLogic.Academic;
using IITMS;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;

public partial class Academic_TabMaster : System.Web.UI.Page
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
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ReportTypeProcedure.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TabMaster.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TabMaster.aspx");
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<TabEntity> GetTabNames(int command)
    {
        TabController tabObj = new TabController();
        DataSet ds = new DataSet();
        List<TabEntity> tabLstObj = new List<TabEntity>();
        TabEntity entObj = new TabEntity();

        entObj.commandType = command;
        entObj.tabNo = 0;
        entObj.status = 0;
        entObj.userNo = Convert.ToInt32(HttpContext.Current.Session["userno"].ToString());
        entObj.tempValue = 0;

        try
        {
            ds = tabObj.Tab_Optn_Page(entObj);

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                tabLstObj = (from DataRow dr in ds.Tables[0].Rows
                               select new TabEntity
                               {
                                   tabNo = Convert.ToInt32(dr["SRNO"].ToString()),
                                   tabName = dr["TAB_NAME"].ToString()
                               }).ToList();
            }
        }
        catch (Exception)
        {

        }
        return tabLstObj;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<TabEntity> BindData(int commmand)
    {
        TabController tabObj = new TabController();
        DataSet ds = new DataSet();
        List<TabEntity> tabLstObj = new List<TabEntity>();
        TabEntity entObj = new TabEntity();

        entObj.commandType = commmand;
        entObj.tabNo = 0;
        entObj.status = 0;
        entObj.userNo = Convert.ToInt32(HttpContext.Current.Session["userno"].ToString());
        entObj.tempValue = 0;
        
        try
        {
            ds = tabObj.Tab_Optn_Page(entObj);
            if (ds.Tables[0].Rows.Count > 0)
            {
                tabLstObj = (from DataRow dr in ds.Tables[0].Rows
                             select new TabEntity
                               {
                                   tabNo = Convert.ToInt32(dr[0].ToString()),
                                   tabName = dr[1].ToString(),
                                   status = Convert.ToInt32(dr[2].ToString()),
                                   userName = dr[3].ToString(),
                                   createdDate = Convert.ToDateTime(dr[4]).ToShortDateString()
                               }).ToList();
            }
        }
        catch (Exception)
        {
        }
        return tabLstObj;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<TabEntity> submitData(int command, int tabID, int update, int isActive)
    {
        TabController tabObj = new TabController();
        DataSet ds = new DataSet();
        List<TabEntity> tabLstObj = new List<TabEntity>();
        TabEntity entObj = new TabEntity();

        entObj.commandType = command;
        entObj.tabNo = tabID;
        entObj.tempValue = update;
        entObj.status = isActive;
        entObj.userNo = Convert.ToInt32(HttpContext.Current.Session["userno"].ToString());        

        try
        {
            ds = tabObj.Tab_Optn_Page(entObj);
            if (ds.Tables[0].Rows.Count > 0)
            {
                tabLstObj = (from DataRow dr in ds.Tables[0].Rows
                             select new TabEntity
                               {
                                   CheckStatus = Convert.ToInt32(dr[0].ToString())
                               }).ToList();
            }
        }
        catch (Exception)
        {
        }
        return tabLstObj;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<TabEntity> EditTab(int Command, int tabNo)
    {
        TabController tabObj = new TabController();
        DataSet ds = new DataSet();
        List<TabEntity> tabLstObj = new List<TabEntity>();
        TabEntity entObj = new TabEntity();

        entObj.commandType = Command;
        entObj.tabNo = tabNo;
        entObj.status = 0;
        entObj.userNo = Convert.ToInt32(HttpContext.Current.Session["userno"].ToString());
        entObj.tempValue = 0;

        try
        {
            ds = tabObj.Tab_Optn_Page(entObj);
            if (ds.Tables[0].Rows.Count > 0)
            {
                tabLstObj = (from DataRow dr in ds.Tables[0].Rows
                             select new TabEntity
                             {
                                 tabNo = Convert.ToInt32(dr["SRNO"].ToString()),
                                 status = Convert.ToInt32(dr["TAB_STATUS"].ToString())
                             }).ToList();
            }
        }
        catch (Exception)
        {
        }
        return tabLstObj;
    }

}