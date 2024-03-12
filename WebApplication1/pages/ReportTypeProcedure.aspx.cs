//---------------------------------------------------------//
// CREATED BY   : SAKSHI MOHADIKAR
// CREATED DATE : 20/11/2023
//---------------------------------------------------------//
using BusinessLogicLayer.BusinessEntities.Academic;
using BusinessLogicLayer.BusinessLogic.Academic;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Academic_ReportTypeProcedure : System.Web.UI.Page
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
                Response.Redirect("~/notauthorized.aspx?page=ReportTypeProcedure.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ReportTypeProcedure.aspx");
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<ReportTypeEntity> GetDistinctPageNames(int CheckStatus)
    {
        ReportTypeController conObj = new ReportTypeController();
        DataSet ds = new DataSet();
        List<ReportTypeEntity> PageListObj = new List<ReportTypeEntity>();
        ReportTypeEntity entObj = new ReportTypeEntity();

        entObj.CheckStatus = CheckStatus;
        entObj.PageID = 0;
        entObj.UserNo = Convert.ToInt32(HttpContext.Current.Session["userno"].ToString());
        entObj.ReportId = 0;
        entObj.ReportName = "";
        entObj.Status = 0;
        entObj.ProcedureName = "";
        entObj.SqNo = 0;
        entObj.Session = 0;
        entObj.Campus = 0;
        entObj.College = 0;
        entObj.Course = 0;
        entObj.Semester = 0;
        entObj.Subject_Type = 0;
        entObj.Subject = 0;
        try
        {
            ds = conObj.Insert_Update_Show_Page(entObj);

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                PageListObj = (from DataRow dr in ds.Tables[0].Rows
                               select new ReportTypeEntity
                               {
                                   PageID = Convert.ToInt32(dr[0].ToString()),
                                   PageName = dr[1].ToString(),
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
    public static List<ReportTypeEntity> SubmitData(int CheckStatus, int PageID, int ReportID, string ReportName, string ProcedureName, int SequenceNo, int isActive, int Session, int Campus, int College, int Course, int Semester, int SubjectType, int Subject)
    {
        ReportTypeController conObj = new ReportTypeController();
        DataSet ds = new DataSet();
        List<ReportTypeEntity> PageListObj = new List<ReportTypeEntity>();
        ReportTypeEntity entObj = new ReportTypeEntity();

        entObj.CheckStatus = CheckStatus;
        entObj.PageID = PageID;
        entObj.UserNo = Convert.ToInt32(HttpContext.Current.Session["userno"].ToString());
        entObj.ReportId = ReportID ;
        entObj.ReportName = ReportName;
        entObj.Status = isActive;
        entObj.ProcedureName = ProcedureName;
        entObj.SqNo = SequenceNo;
        entObj.Session = Session;
        entObj.Campus = Campus;
        entObj.College = College;
        entObj.Course = Course;
        entObj.Semester = Semester;
        entObj.Subject_Type = SubjectType;
        entObj.Subject = Subject;

        try
        {
            ds = conObj.Insert_Update_Show_Page(entObj);

            if (ds.Tables[0].Rows.Count > 0)
            {
                PageListObj = (from DataRow dr in ds.Tables[0].Rows
                               select new ReportTypeEntity
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
    public static List<ReportTypeEntity> EditData(int CheckStatus, int ReportNo)
    {
        ReportTypeController conObj = new ReportTypeController();
        DataSet ds = new DataSet();
        List<ReportTypeEntity> PageListObj = new List<ReportTypeEntity>();
        ReportTypeEntity entObj = new ReportTypeEntity();

        entObj.CheckStatus = CheckStatus;
        entObj.UserNo = 0;
        entObj.PageID = 0;        
        entObj.ReportId = ReportNo;
        entObj.ReportName = "";
        entObj.Status = 0;
        entObj.ProcedureName = "";
        entObj.SqNo = 0;
        entObj.Session = 0;
        entObj.Campus = 0;
        entObj.College = 0;
        entObj.Course = 0;
        entObj.Semester = 0;
        entObj.Subject_Type = 0;
        entObj.Subject = 0;
        try
        {
            ds = conObj.Insert_Update_Show_Page(entObj);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                PageListObj = (from DataRow dr in ds.Tables[0].Rows
                               select new ReportTypeEntity
                               {
                                   PageID = Convert.ToInt32(dr["PAGE_NO"].ToString()), 
                                   ReportId = Convert.ToInt32(dr["RPT_NO"].ToString()), 
                                   ReportName = dr["REPORT_NAME"].ToString(),
                                   ProcedureName = dr["PROCEDURE_NAME"].ToString(),
                                   SqNo = Convert.ToInt32(dr["SQ_NO"].ToString()),
                                   Session = Convert.ToInt32(dr["SESSION_MANDATORY"].ToString()),
                                   College = Convert.ToInt32(dr["COLLEGE_MANDATORY"].ToString()),
                                   Course = Convert.ToInt32(dr["PROGRAM_MANDATORY"].ToString()),
                                   Semester = Convert.ToInt32(dr["SEMESTER_MANDATORY"].ToString()),
                                   Subject_Type = Convert.ToInt32(dr["STDUYTYPE_MANDATORY"].ToString()),
                                   Subject = Convert.ToInt32(dr["SUBJECT_MANDATORY"].ToString()),
                                   Status = Convert.ToInt32(dr["STATUS"].ToString())
                               }).ToList();
            }
        }
        catch (Exception)
        {
            throw;
        }

        return PageListObj;
    }
    
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<ReportTypeEntity> BindData(int commmand)
    {
        ReportTypeController conObj = new ReportTypeController();
        DataSet ds = new DataSet();
        List<ReportTypeEntity> PageListObj = new List<ReportTypeEntity>();
        ReportTypeEntity entObj = new ReportTypeEntity();

        entObj.CheckStatus = commmand;
        entObj.UserNo = 0;
        entObj.PageID = 0;
        entObj.ReportId = 0;
        entObj.ReportName = "";
        entObj.Status = 0;
        entObj.ProcedureName = "";
        entObj.Session = 0;
        entObj.Campus = 0;
        entObj.College = 0;
        entObj.Course = 0;
        entObj.Semester = 0;
        entObj.Subject_Type = 0;
        entObj.Subject = 0;
        try
        {
            ds = conObj.Insert_Update_Show_Page(entObj);
            if (ds.Tables[0].Rows.Count > 0)
            {
                PageListObj = (from DataRow dr in ds.Tables[0].Rows
                               select new ReportTypeEntity
                               {
                                   ReportId = Convert.ToInt32(dr[0].ToString()),
                                   ReportName = dr[1].ToString(),
                                   PageID = Convert.ToInt32(dr[2].ToString()),
                                   PageName = dr[3].ToString(),
                                   ProcedureName = dr[4].ToString(),
                                   Status = Convert.ToInt32(dr[5].ToString()),
                                   SqNo = Convert.ToInt32(dr[6].ToString()),
                                   Session = Convert.ToInt32(dr[7].ToString()),
                                   College = Convert.ToInt32(dr[8].ToString()),
                                   Course = Convert.ToInt32(dr[9].ToString()),
                                   Semester = Convert.ToInt32(dr[10].ToString()),
                                   Subject_Type = Convert.ToInt32(dr[11].ToString()),
                                   Subject = Convert.ToInt32(dr[12].ToString()),
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
    public static List<ReportTypeEntity> GetSeqValue(int CheckStatus, int SelectedValue)
    {
        ReportTypeController conObj = new ReportTypeController();
        DataSet ds = new DataSet();
        List<ReportTypeEntity> PageListObj = new List<ReportTypeEntity>();
        ReportTypeEntity entObj = new ReportTypeEntity();

        entObj.CheckStatus = CheckStatus;
        entObj.UserNo = 0;
        entObj.PageID = SelectedValue;
        entObj.ReportId = 0;
        entObj.ReportName = "";
        entObj.Status = 0;
        entObj.ProcedureName = "";
        entObj.Session = 0;
        entObj.Campus = 0;
        entObj.College = 0;
        entObj.Course = 0;
        entObj.Semester = 0;
        entObj.Subject_Type = 0;
        entObj.Subject = 0;
        entObj.SqNo = 0;
        try
        {
            ds = conObj.Insert_Update_Show_Page(entObj);

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                PageListObj = (from DataRow dr in ds.Tables[0].Rows
                               select new ReportTypeEntity
                               {
                                   SqNo = Convert.ToInt32(dr[0].ToString())
                               }).ToList();
            }
        }
        catch (Exception)
        {

        }
        return PageListObj;
    }

}