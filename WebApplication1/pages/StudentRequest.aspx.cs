using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Academic_StudentRequest : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    static int userNO = 0;
    static string IpAddress = "";
    static string Idno = "0";
    //USED FOR INITIALSING THE MASTER PAGE
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
                    }
                   // objCommon.SetLabelData("0");//for label
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
                    userNO = Convert.ToInt32(Session["userno"].ToString());
                    IpAddress = Request.ServerVariables["REMOTE_ADDR"];
                    Idno = Session["idno"].ToString();
                }


                objCommon.FillDropDownList(ddlAcdSession, "acd_session_master sm inner join ACD_SPECIAL_REQUEST_CONFIG sr on sm.sessionno = sr.academic_session", "DISTINCT sm.SESSIONNO ", "SESSION_NAME", "", "sm.SESSIONNO");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic->StudentRequest.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetStudentDetailsRequest(int Sessionno) // Assuming Idno is the parameter passed to the method
    
    {
        StudentRequestController objController = new StudentRequestController();
        DataSet ds = objController.Get_Student_List_For_Subject_Request(Convert.ToInt32(Idno), Sessionno); // Assuming Idno is of type int
       // DataSet ds = objController.Get_Student_List_For_Subject_Request(1240); // Assuming Idno is of type int
         

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string json = JsonConvert.SerializeObject(ds.Tables[0]);
            dynamic dynamicJson = JsonConvert.DeserializeObject(json, typeof(ExpandoObject));
            return JsonConvert.SerializeObject(dynamicJson);
        }

        return "{}"; // Return an empty JSON object if no data is found
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string CheckActivityStatus(int SemesterNo, int SessionNo) // Assuming Idno is the parameter passed to the method
    {
        StudentRequestController objController = new StudentRequestController();
        DataSet ds = objController.CheckActivityStatus(SessionNo, SemesterNo); // Assuming Idno is of type int

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string json = JsonConvert.SerializeObject(ds.Tables[0]);
            dynamic dynamicJson = JsonConvert.DeserializeObject(json, typeof(ExpandoObject));
            return JsonConvert.SerializeObject(dynamicJson);
        }

        return "{}"; // Return an empty JSON object if no data is found
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetCoursesListForRequest(int SchemeNo, int SemesterNo, int SessionNo) // Assuming Idno is the parameter passed to the method
    {
        StudentRequestController objController = new StudentRequestController();
        DataSet ds = objController.Get_Courses_For_Subject_Request(Convert.ToInt32(Idno), SessionNo, SemesterNo, SchemeNo); // Assuming Idno is of type int

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string json = JsonConvert.SerializeObject(ds.Tables[0]);
            dynamic dynamicJson = JsonConvert.DeserializeObject(json, typeof(ExpandoObject));
            return JsonConvert.SerializeObject(dynamicJson);
        }

        return "{}"; // Return an empty JSON object if no data is found 
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetCoursesListForRequest2(int SchemeNo, int SemesterNo, int SessionNo) // Assuming Idno is the parameter passed to the method
    {
        StudentRequestController objController = new StudentRequestController();
        DataSet ds = objController.Get_Courses_For_Subject_Request(Convert.ToInt32(Idno), SessionNo, SemesterNo, SchemeNo); // Assuming Idno is of type int

        if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
        {
            string json = JsonConvert.SerializeObject(ds.Tables[1]);
            dynamic dynamicJson = JsonConvert.DeserializeObject(json, typeof(ExpandoObject));
            return JsonConvert.SerializeObject(dynamicJson);
        }

        return "{}"; // Return an empty JSON object if no data is found 
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetCoursesListForRequest3(int SchemeNo, int SemesterNo, int SessionNo) // Assuming Idno is the parameter passed to the method
    {
        StudentRequestController objController = new StudentRequestController();
        DataSet ds = objController.Get_Courses_For_Subject_Request(Convert.ToInt32(Idno), SessionNo, SemesterNo, SchemeNo); // Assuming Idno is of type int

        if (ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
        {
            string json = JsonConvert.SerializeObject(ds.Tables[2]);
            dynamic dynamicJson = JsonConvert.DeserializeObject(json, typeof(ExpandoObject));
            return JsonConvert.SerializeObject(dynamicJson);
        }

        return "{}"; // Return an empty JSON object if no data is found 
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static int Save_Subject_Request(int SemesterNo, int SessionNo, dynamic Courses) // Assuming Idno is the parameter passed to the method
    {
        StudentRequestController objController = new StudentRequestController();
        string json = JsonConvert.SerializeObject(Courses);
        DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
        int ret = objController.Save_Student_Subject_Request(dt, SessionNo, Convert.ToInt32(Idno), SemesterNo, IpAddress);
        return ret;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetRequestCourse(int SemesterNo, int SessionNo) // Assuming Idno is the parameter passed to the method
    {
        StudentRequestController objController = new StudentRequestController();
        DataSet ds = objController.Get_Requested_Courses(Convert.ToInt32(Idno), SessionNo, SemesterNo); // Assuming Idno is of type int

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string json = JsonConvert.SerializeObject(ds.Tables[0]);
            dynamic dynamicJson = JsonConvert.DeserializeObject(json, typeof(ExpandoObject));
            return JsonConvert.SerializeObject(dynamicJson);
        }

        return "{}"; // Return an empty JSON object if no data is found
    }

}