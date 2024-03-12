using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Web.Configuration;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Security;
using System.Diagnostics;

using System.Collections.Generic;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using EASendMail;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;


public partial class MockUps_Slot_Monitoring : System.Web.UI.Page
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
                    //this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                    ViewState["action"] = "add";
                    BindDropdown();
                    objCommon.SetLabelData("0");//for label
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeesRefundReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Slot_Monitoring.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Slot_Monitoring.aspx");
        }
    }
    private void BindDropdown()
    {
        string collge = objCommon.LookUp("USER_ACC", "UA_COLLEGE_NOS", "UA_NO=" + Session["userno"]);
        objCommon.FillListBox(lstCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + collge + ")" +"AND COLLEGE_ID>0 AND ACTIVE=1", "COLLEGE_ID");
        objCommon.FillDropDownList(ddlAcademicSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND IS_ACTIVE=1", "SESSIONNO");
        objCommon.FillDropDownList(ddlCampus, "ACD_CAMPUS", "CAMPUSNO", "CAMPUSNAME", "ACTIVE=1", "CAMPUSNO"); 
        objCommon.FillDropDownList(ddlSemster, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "STATUS=1", "SEMESTERNO");
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            string college = "";
            foreach (ListItem item in lstCollege.Items)
            {
                if (item.Selected == true)
                {
                    college += item.Value + '$';
                }
            }
            if (!string.IsNullOrEmpty(college))
            {
                college = college.Substring(0, college.Length - 1);
            }
            else
            {
                college = "0";
            }
            string[] program;
            if (ddlCourse.SelectedValue == "0")
            {
                program = "0,0".Split(',');
            }
            else
            {
                program = ddlCourse.SelectedValue.Split(',');
            }
            string SP_Name2 = "PR_SECTION_MONITORING_GRID_VIEW_REPORT";
            string SP_Parameters2 = "@P_SEMESTERNO,@P_CDBNO,@P_DEGREENO,@P_BRANCHNO,@P_COLLEGE_ID,@P_SESSIONNO,@P_CAMPUS_NO";
            string Call_Values2 = "" + Convert.ToInt32(ddlSemster.SelectedValue.ToString()) + "," + 0  + "," + Convert.ToInt32(program[0])
              + "," + Convert.ToInt32(program[1]) + "," + college + "," +Convert.ToInt32(ddlAcademicSession.SelectedValue) + "," + Convert.ToInt32(ddlCampus.SelectedValue) + "";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlSlot.Visible = true;
                lvSlots.DataSource = ds;
                lvSlots.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(UpdRole, "No Record Found", this.Page);
                Clear();
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void Clear()
    {
        ddlAcademicSession.SelectedIndex = 0;
        lstCollege.ClearSelection();
        ddlCampus.SelectedIndex = 0;
        ddlSemster.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        pnlSlot.Visible = false;
        lvSlots.DataSource = null;
        lvSlots.DataBind();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();

    }
    protected void lnkview_Click(object sender, EventArgs e)
    {

        LinkButton lnk = sender as LinkButton;
        string [] couresnosectiono= (lnk.CommandArgument.Split('-'));
        int CourseNo = Convert.ToInt32(couresnosectiono[0]);
        int SectionNo =  Convert.ToInt32(couresnosectiono[1]);
        int SessionNo = int.Parse(lnk.CommandName);
        int SemesterNo = int.Parse(lnk.ToolTip);
        string SP_Name2 = "PKG_SP_GET_SLOT_MONITORING_TIME_TABLE";
        string SP_Parameters2 = "@P_SESSIONNO,@P_SEMESTERNO,@P_COURSENO,@P_SECTIONNO";
        string Call_Values2 = "" + SessionNo + "," + SemesterNo + "," + CourseNo + "," +SectionNo + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);

        if (ds.Tables[0].Rows.Count > 0)
        {
            PanelTimeSlot.Visible = true;
            LvTimeSlot.DataSource = ds;
            LvTimeSlot.DataBind();
        }
        else
        {
            PanelTimeSlot.Visible = false;
            LvTimeSlot.DataSource = null;
            LvTimeSlot.DataBind();
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ViewModal", "$(document).ready(function () {$('#ViewModal').modal();});", true);
    }
    protected void lstCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        string college = "";
        foreach (ListItem item in lstCollege.Items)
        {
            if (item.Selected == true)
            {
                college += item.Value + '$';
            }
        }
        if (!string.IsNullOrEmpty(college))
        {
            college = college.Substring(0, college.Length - 1);
        }
        else
        {
            college = "0";
        }
        string SP_Name2 = "PKG_ACD_GET_PROGRAM_DETAILS_FOR_SLOT_MONITORING";
        string SP_Parameters2 = "@P_COLLEGE_ID";
        string Call_Values2 = "" + college + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlCourse.Items.Clear();
            ddlCourse.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlCourse.DataSource = ds.Tables[0];
            ddlCourse.DataValueField = "ID";
            ddlCourse.DataTextField = "NAME";
            ddlCourse.DataBind();
        }
    }
}