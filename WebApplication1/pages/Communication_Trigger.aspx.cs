//---------------------------------------------------------//
// CREATED BY       : SAKSHI MOHADIKAR
// CREATED DATE     : 11-01-2023
// MODIFIED BY      : 
// MODIFIED DATE    :
//---------------------------------------------------------//

using BusinessLogicLayer.BusinessEntities.Academic;
using BusinessLogicLayer.BusinessLogic.Academic;
using IITMS;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Communication_Trigger : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

    #region Page Event
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            // Set MasterPage
            if (Session["masterpage"] != null)
                objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
            else
                objCommon.SetMasterPage(Page, "");
        }
        catch (Exception ex)
        {
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                string PageId = Request.QueryString["pageno"];
                Session["PageId"] = PageId;
                HttpRuntime.Cache.Insert("PageId", PageId);
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
                    BindPageHeader();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Communication_Trigger.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void BindPageHeader()
    {
        try
        {
            Common objCmm = new Common();
            string lnk = "";
            int PageId = Convert.ToInt32(Request.QueryString["pageno"]);
            lnk = objCmm.GetPageHeaders(PageId);
            // ltrPageHeads.Text = lnk;
        }
        catch (Exception ex)
        {
        }
    }

    private void CheckPageAuthorization()
    {
        try
        {
            if (Request.QueryString["pageno"] != null)
            {
                // Check user's authrity for Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=" + Path.GetFileName(this.Page.AppRelativeVirtualPath));
                }
            }
            else
            {
                // Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=" + Path.GetFileName(this.Page.AppRelativeVirtualPath));
            }
        }
        catch (Exception ex)
        {
        }
    }
    #endregion 

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<CommunicationTrEntity> BindEvent(int CommandType)
    {
        CommunicationTrController conObj = new CommunicationTrController();
        DataSet ds = new DataSet();
        List<CommunicationTrEntity> PageListObj = new List<CommunicationTrEntity>();
        CommunicationTrEntity entObj = new CommunicationTrEntity();

        entObj.CommandType = CommandType;
        entObj.easID = 0;
        entObj.objEvent = 0;
        entObj.startDate = "";
        entObj.endDate = "";
        entObj.scheTime = "";
        entObj.status = 0;
        entObj.fetchDynaStatus = 0;
        entObj.Tomail = "";
        entObj.CCmail = "";
        entObj.BCCmail = "";
        entObj.eyeEmailID = 0;
        entObj.userNo = Convert.ToInt32(HttpContext.Current.Session["userno"].ToString());
        try
        {
            ds = conObj.CommuTriggerOpt(entObj);

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                PageListObj = (from DataRow dr in ds.Tables[0].Rows
                               select new CommunicationTrEntity
                               {
                                   acticityID = Convert.ToInt32(dr[0].ToString()),
                                   activityName = dr[1].ToString()
                               }).ToList();
            }
        }
        catch (Exception ex)
        {

        }
        return PageListObj;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<CommunicationTrEntity> SubmitData(int CommandType, int EasID, int EventID, string startDate, string endDate, string scheduleTime, int isActive, int fetchDynaStatus, string txtAreaTO,
        string txtAreaCC, string txtAreaBCC)
    {
        CommunicationTrController conObj = new CommunicationTrController();
        DataSet ds = new DataSet();
        List<CommunicationTrEntity> PageListObj = new List<CommunicationTrEntity>();
        CommunicationTrEntity entObj = new CommunicationTrEntity();

        entObj.CommandType = CommandType;
        entObj.easID = EasID;
        entObj.objEvent = EventID;
        entObj.startDate = startDate;
        entObj.endDate = endDate;
        entObj.scheTime = scheduleTime;
        entObj.status = isActive;
        entObj.fetchDynaStatus = fetchDynaStatus;
        entObj.Tomail = txtAreaTO;
        entObj.CCmail = txtAreaCC;
        entObj.BCCmail = txtAreaBCC;
        entObj.eyeEmailID = 0;
        entObj.userNo = Convert.ToInt32(HttpContext.Current.Session["userno"].ToString());

        try
        {
            ds = conObj.CommuTriggerOpt(entObj);

            if (ds.Tables[0].Rows.Count > 0)
            {
                PageListObj = (from DataRow dr in ds.Tables[0].Rows
                               select new CommunicationTrEntity
                               {
                                   CheckStatus = Convert.ToInt32(dr[0].ToString()),
                               }).ToList();
            }
        }

        catch (Exception)
        {
        }
        return PageListObj;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<CommunicationTrEntity> BindAllEmailsData(int CommandType)
    {
        CommunicationTrController conObj = new CommunicationTrController();
        DataSet ds = new DataSet();
        List<CommunicationTrEntity> PageListObj = new List<CommunicationTrEntity>();
        CommunicationTrEntity entObj = new CommunicationTrEntity();

        entObj.CommandType = CommandType;
        entObj.easID = 0;
        entObj.objEvent = 0;
        entObj.startDate = "";
        entObj.endDate = "";
        entObj.scheTime = "";
        entObj.status = 0;
        entObj.fetchDynaStatus = 0;
        entObj.Tomail = "";
        entObj.CCmail = "";
        entObj.BCCmail = "";
        entObj.eyeEmailID = 0;
        entObj.userNo = Convert.ToInt32(HttpContext.Current.Session["userno"].ToString());
        try
        {
            ds = conObj.CommuTriggerOpt(entObj);
            if (ds.Tables[0].Rows.Count > 0)
            {
                PageListObj = (from DataRow dr in ds.Tables[0].Rows
                               select new CommunicationTrEntity
                               {
                                   easID = Convert.ToInt32(dr[0].ToString()),
                                   objEvent = Convert.ToInt32(dr[1].ToString()),
                                   activityName = dr[2].ToString(),
                                   startDate = dr[3].ToString(),
                                   endDate = dr[4].ToString(),
                                   scheTime = dr[5].ToString(),
                                   Tomail = dr[6].ToString(),
                                   CCmail = dr[7].ToString(),
                                   BCCmail = dr[8].ToString(),
                                   status = Convert.ToInt32(dr[9].ToString())
                               }).ToList();
            }
        }
        catch (Exception)
        {
        }
        return PageListObj;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<CommunicationTrEntity> EditEmailsData(int command, int EasID)
    {
        CommunicationTrController conObj = new CommunicationTrController();
        DataSet ds = new DataSet();
        List<CommunicationTrEntity> PageListObj = new List<CommunicationTrEntity>();
        CommunicationTrEntity entObj = new CommunicationTrEntity();

        entObj.CommandType = command;
        entObj.easID = EasID;
        entObj.objEvent = 0;
        entObj.startDate = "";
        entObj.endDate = "";
        entObj.scheTime = "";
        entObj.status = 0;
        entObj.fetchDynaStatus = 0;
        entObj.Tomail = "";
        entObj.CCmail = "";
        entObj.BCCmail = "";
        entObj.eyeEmailID = 0;
        entObj.userNo = Convert.ToInt32(HttpContext.Current.Session["userno"].ToString());
        try
        {
            ds = conObj.CommuTriggerOpt(entObj);
            if (ds.Tables[0].Rows.Count > 0)
            {
                PageListObj = (from DataRow dr in ds.Tables[0].Rows
                               select new CommunicationTrEntity
                               {
                                   easID = Convert.ToInt32(dr[0].ToString()),
                                   objEvent = Convert.ToInt32(dr[1].ToString()),
                                   startDate = dr[2].ToString(),
                                   endDate = dr[3].ToString(),
                                   scheTime = dr[4].ToString(),
                                   status = Convert.ToInt32(dr[5].ToString()),
                                   fetchDynaStatus = Convert.ToInt32(dr[6].ToString()),
                                   Tomail = dr[7].ToString(),
                                   CCmail = dr[8].ToString(),
                                   BCCmail = dr[9].ToString()
                               }).ToList();
            }
        }
        catch (Exception)
        {
        }
        return PageListObj;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<CommunicationTrEntity> HoverEmailData(int CommandType, int eyeID, int EasID, string StartDate, string EndDate, int Event, string SchedularTime)
    {
        CommunicationTrController conObj = new CommunicationTrController();
        DataSet ds = new DataSet();
        List<CommunicationTrEntity> PageListObj = new List<CommunicationTrEntity>();
        CommunicationTrEntity entObj = new CommunicationTrEntity();

        entObj.CommandType = CommandType;
        entObj.easID = EasID;
        entObj.objEvent = Event;
        entObj.startDate = "";
        entObj.endDate = "";
        entObj.scheTime = "";
        entObj.status = 0;
        entObj.fetchDynaStatus = 0;
        entObj.Tomail = "";
        entObj.CCmail = "";
        entObj.BCCmail = "";
        entObj.eyeEmailID = eyeID;
        entObj.userNo = Convert.ToInt32(HttpContext.Current.Session["userno"].ToString());
        try
        {
            ds = conObj.CommuTriggerOpt(entObj);
            if (ds.Tables[0].Rows.Count > 0)
            {
                PageListObj = (from DataRow dr in ds.Tables[0].Rows
                               select new CommunicationTrEntity
                               {
                                   Mail = dr[0].ToString()
                               }).ToList();
            }
        }
        catch (Exception)
        {
        }
        return PageListObj;
    }
}



