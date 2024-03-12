using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using Newtonsoft.Json;
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

using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.IO;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;
using IITMS.SQLServer.SQLDAL;
using System.Web.Configuration;
using IITMS.SQLServer.SQLDAL;
using ClosedXML.Excel;
using System.Data.OleDb;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;

public partial class Academic_ReStudyApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    static int userNO = 0;
    static string IpAddress = "";
    static string Idno = "0";

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
                    //this.CheckPageAuthorization();
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
                    Idno = Session["idno"].ToString();

                    objCommon.FillDropDownList(ddlAcdSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "ISNULL(FLOCK,0)=1", "SESSIONNO");
                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "ISNULL(ACTIVE,0)=1", "COLLEGE_ID");
                    objCommon.FillListBox(lstSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "ISNULL(STATUS,0)=1", "SEMESTERNO");


                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic->Academic_ReStudyApproval.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
    public static string BindDropDown(string Type, int CollegeID, string program) // Assuming Idno is the parameter passed to the method
    {
        SpecialClassApproveController objController = new SpecialClassApproveController();
        DataSet ds = objController.Get_DropDown_List(Type, CollegeID, 1, 28); // Assuming Idno is of type int

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
    public static string GetCoursesForApproval(int Sessionno, int CollegeId, string DegreeNo, string BranchNo, string Curriculum, string SemesterNo) // Assuming Idno is the parameter passed to the method
    {
        SpecialClassApproveController objController = new SpecialClassApproveController();
        DataSet ds = objController.Get_Course_List_For_Special_Class(Sessionno, CollegeId, DegreeNo.TrimEnd('$'), BranchNo.TrimEnd('$'), Curriculum, SemesterNo); // Assuming Idno is of type int

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
     public static string Save_Special_Approve( string dt_Courses)
    {
        GradeMasterEntity.FAGrade objComn = new GradeMasterEntity.FAGrade();
        SpecialClassApproveController objController = new SpecialClassApproveController();
        List<IITMS.UAIMS.BusinessLayer.BusinessEntities.GradeMasterEntity.SpecialClass> BindList = new List<IITMS.UAIMS.BusinessLayer.BusinessEntities.GradeMasterEntity.SpecialClass>();
        try
        {
            var serializeData = JsonConvert.DeserializeObject<List<GradeMasterEntity.SpecialClass>>(dt_Courses);
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("COURSENO");
            dt.Columns.Add("SESSIONNO");
            dt.Columns.Add("COLLEGE_ID");
            dt.Columns.Add("ISAPROVED");
            dt.Columns.Add("SEMESTERNO");
            foreach (var data in serializeData)
            {
                DataRow QueData = dt.NewRow();
                QueData["COURSENO"] = data.CourseNo;
                QueData["SESSIONNO"] = data.Sessionno;
                QueData["COLLEGE_ID"] = data.College_Id;
                QueData["ISAPROVED"] = data.ISAPROVED;
                QueData["SEMESTERNO"] = data.Semesterno;

                dt.Rows.Add(QueData);
            }
            DataSet ds = objController.Save_Specail_Class_Approve(dt, IpAddress, Convert.ToInt32(userNO));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {

                string json1 = JsonConvert.SerializeObject(ds.Tables[0]);
                dynamic dynamicJson = JsonConvert.DeserializeObject(json1, typeof(ExpandoObject));
                return JsonConvert.SerializeObject(dynamicJson);
            }
        }
        catch (Exception ex)
        {

        }


        return "{}";
    }
    //Popup Patch 17-11-2023
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string Get_Requested_Student_List(int Sessionno, int Courseno, int Isapproved) // Assuming Idno is the parameter passed to the method
    {
        SpecialClassApproveController objController = new SpecialClassApproveController();
        DataSet ds = objController.Get_Requested_Student_List(Sessionno, Courseno, Isapproved); // Assuming Idno is of type int
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string json = JsonConvert.SerializeObject(ds.Tables[0]);
            dynamic dynamicJson = JsonConvert.DeserializeObject(json, typeof(ExpandoObject));
            return JsonConvert.SerializeObject(dynamicJson);
        }

        return "{}"; // Return an empty JSON object if no data is found
    }
    //End
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillListBox(lstProgram, "ACD_COLLEGE_DEGREE_BRANCH S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=S.BRANCHNO)", "DISTINCT CONCAT(S.DEGREENO,',',S.BRANCHNO)AS ID", "(D.DEGREENAME +'-'+ B.LONGNAME)AS DEGBRANCH", "S.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "ID");
    }


    protected void lstProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstProgram.SelectedValue != "")
        {
            string[] pgm = new string[] { };
            string degreeno = "";
            string branchno = "";
            string program = "";
            foreach (ListItem item in lstProgram.Items)
            {
                if (item.Selected == true)
                {
                    program += item.Value + ',';
                }
            }
            if (!string.IsNullOrEmpty(program))
            {
                pgm = program.Split(',');
                for (int i = 0; i < pgm.Length; i += 2)
                {
                    degreeno += pgm[i] + "$";

                }
                for (int j = 1; j < pgm.Length; j += 2)
                {
                    branchno += pgm[j] + "$";
                }
                degreeno = degreeno.TrimEnd('$');
                branchno = branchno.TrimEnd('$');
            }

            else
            {
                program = "0";
            }
            string SP_Name2 = "PKG_ACD_GET_SCHEME_FOR_SPECIAL_CLASS";
            string SP_Parameters2 = "@p_degreeno,@p_BRANCHNO";
            string Call_Values2 = "" + degreeno + "," + branchno + "";
            DataSet ds1 = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                lstCurriculum.Items.Clear();
                lstCurriculum.DataSource = ds1.Tables[0];
                lstCurriculum.DataValueField = "SCHEMENO";
                lstCurriculum.DataTextField = "SCHEMENAME";
                lstCurriculum.DataBind();

            }
        }

        else
        {
            lstCurriculum.Items.Clear();
        }
    }
}