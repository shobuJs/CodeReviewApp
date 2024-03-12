using IITMS;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using Newtonsoft.Json;

public partial class Academic_SpclRequestConfig : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    static int userNO = 0;
    static string IpAddress = "";
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
                //string PageId = Request.QueryString["pageno"];
                //Session["PageId"] = PageId;
                //HttpRuntime.Cache.Insert("PageId", PageId);
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
                    }
                    objCommon.SetLabelData("0");//for label
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
               
                    userNO = Convert.ToInt32(Session["userno"].ToString());
                    IpAddress = Request.ServerVariables["REMOTE_ADDR"];
                    BindPageHeader();
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic->FreshmanProjections.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=Module_Rule_Book.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Module_Rule_Book.aspx");
        }
    }

    public void BindPageHeader()
    {
        Common objCmm = new Common();
        string lnk = "";
        int PageId = Convert.ToInt32(Request.QueryString["pageno"]);
        lnk = objCmm.GetPageHeaders(PageId);
        //ltrPageHeads.Text = lnk;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<SpecialRequestConfiguration.BindDropDown> BindDropDown(string Type)
    {
        List<SpecialRequestConfiguration.BindDropDown> BindDropDownList = new List<SpecialRequestConfiguration.BindDropDown>();
        SpecialRequestConfigController objController = new SpecialRequestConfigController();
        DataSet ds = new DataSet();
        try
        {
            ds = objController.Get_Drop_Down(Type);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                BindDropDownList = (from DataRow dr in ds.Tables[0].Rows
                                    select new SpecialRequestConfiguration.BindDropDown
                                    {
                                        Id = Convert.ToInt32(dr[0].ToString()),
                                        Name = dr[1].ToString()
                                    }).ToList();
            }
        }
        catch (Exception ex)
        {

        }
        return BindDropDownList;
    }

    //Show Data after update
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<SpecialRequestConfiguration.SpecialRequestConfig> GetSpecialConfig(int ConfigId)
    {
        SpecialRequestConfigController objController = new SpecialRequestConfigController();
       
        DataSet ds = objController.Get_Special_Request_Config(ConfigId); // Assuming Save_Special_Request_Config method accepts SpecialRequestConfiguration.SpecialRequestConfig
        List<SpecialRequestConfiguration.SpecialRequestConfig> ListSpclConfig = new List<SpecialRequestConfiguration.SpecialRequestConfig>();

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            //var settings = new JsonSerializerSettings
            //{
            //    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat // Retain the date format as it is
            //};

            string json = JsonConvert.SerializeObject(ds.Tables[0]);
            ListSpclConfig = JsonConvert.DeserializeObject<List<SpecialRequestConfiguration.SpecialRequestConfig>>(json);
        }

        return ListSpclConfig;
    }

    //save data
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<SpecialRequestConfiguration.SpecialRequestConfig> SaveSpecialRequestConfig(List<SpecialRequestConfiguration.SpecialRequestConfig> SaveConfig)
    {
        SpecialRequestConfigController objController = new SpecialRequestConfigController();
        foreach (var config in SaveConfig)
        {
            config.IPADDRESS = IpAddress;
            config.USERNO = userNO;
        }
        string json1 = JsonConvert.SerializeObject(SaveConfig);
        List<SpecialRequestConfiguration.SpecialRequestConfig> spclConfigList = JsonConvert.DeserializeObject<List<SpecialRequestConfiguration.SpecialRequestConfig>>(json1);

        DataSet ds = objController.Save_Special_Request_Config(spclConfigList[spclConfigList.Count - 1]); // Assuming Save_Special_Request_Config method accepts SpecialRequestConfiguration.SpecialRequestConfig
        List<SpecialRequestConfiguration.SpecialRequestConfig> ListSpclConfig = new List<SpecialRequestConfiguration.SpecialRequestConfig>();

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string json = JsonConvert.SerializeObject(ds.Tables[0]);
            ListSpclConfig = JsonConvert.DeserializeObject<List<SpecialRequestConfiguration.SpecialRequestConfig>>(json);
        }

        return ListSpclConfig;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string CheckFauorateLink()
    {
        Common objCmm = new Common();
        int PageId = Convert.ToInt32(HttpContext.Current.Session["PageId"].ToString());
        int UANO = Convert.ToInt32(HttpContext.Current.Session["userno"].ToString());
        string chk = objCmm.GetFaviourateLinksCSS(PageId, UANO);
        return chk;
    }
}