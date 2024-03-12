//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : CONFIGURATION PAGE                                                       
// CREATION DATE : 30-MARCH-2012                                                       
// CREATED BY    : ASHISH DHAKATE                                                 
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_UaimsConfiguration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
            BindListView();
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BatchMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BatchMaster.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            
                ConfigController objBc = new ConfigController();
                DataSet ds = objBc.GetAllEvents();
                lvConfiguration.DataSource = ds;
                lvConfiguration.DataBind();
                
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_UaimsConfiguration.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ConfigController objCC = new ConfigController();
        Config objConfig = new Config();
        string EventNo = string.Empty;
        string Status = string.Empty;
        foreach (ListViewDataItem dataItem in lvConfiguration.Items)
        {
            if ((dataItem.FindControl("chkYes") as CheckBox).Checked == true)
            {
                {
                    EventNo = (dataItem.FindControl("hidEventNoY") as HiddenField).Value;
                    objConfig.EventNo = Convert.ToInt32(EventNo);
                    objConfig.Status = "Y";
                    objConfig.CollegeCode = Session["colcode"].ToString();

                    CustomStatus cs = (CustomStatus)objCC.AddConfig(objConfig);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {

                        objCommon.DisplayMessage("Record Saved Successfully!", this.Page);
                    }
                    else
                    {
                    }
                }
            }
            if ((dataItem.FindControl("chkNo") as CheckBox).Checked == true)
            {
                EventNo = (dataItem.FindControl("hidEventNoN") as HiddenField).Value;
                objConfig.EventNo = Convert.ToInt32(EventNo);
                objConfig.Status = "N";
                objConfig.CollegeCode = Session["colcode"].ToString();

                CustomStatus cs = (CustomStatus)objCC.AddConfig(objConfig);

                if (cs.Equals(CustomStatus.RecordSaved))
                {

                    objCommon.DisplayMessage("Record Saved Successfully!", this.Page);
                }
                else
                {
                }
            }
           
        }
    }
}
