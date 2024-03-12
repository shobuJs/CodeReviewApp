using System.Linq;
using System.Web;
using System.Xml.Linq;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Web.Configuration;
public partial class AdmissionTestScheduling : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamSchedulingController ObjESController = null;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
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
                }
            }
            objCommon.SetLabelData("0");//for label
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
            ViewState["action"] = "add";
            FilldropDown();
            BindDetails();
        }
    }
    private void FilldropDown()
    {
        objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME");
        objCommon.FillListBox(ddlStudyLevel, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "UA_SECTIONNAME");
        objCommon.FillDropDownList(ddlVenue, "ACD_APTITUDE_CENTER", "APTITUDE_CENTER_NO", "APTITUDE_CENTER_NAME", "APTITUDE_CENTER_NO>0 and isnull(ACTIVE,0) =1", "APTITUDE_CENTER_NAME");
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CollegeMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CollegeMaster.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string[] Time;
        Time = txtTimeSlot.Text.Split('-');
        string timefrom = Time[0];
        string timeto = Time[1];
        //DateTime ExamDate = Convert.ToDateTime(txtDate.Text);
        //DateTime ExamDate = DateTime.Now;
        int Active = 0;
        if (hfdStat.Value == "true")
            Active = 1;
        else
            Active = 0;
        string Stydy = "";

        foreach (ListItem item in ddlStudyLevel.Items)
        {
            if (item.Selected == true)
            {
                Stydy += item.Value + '$';
            }

        }

        if (!string.IsNullOrEmpty(Stydy))
        {
            Stydy = Stydy.Substring(0, Stydy.Length - 1);
        }
        else
        {
            objCommon.DisplayMessage(this, "Please select Study Level !!", this.Page);
            return;
        }
        if (ViewState["action"] != null)
        {
            if (ViewState["action"].ToString().Equals("add"))
            {
                string SP_Name1 = "PKG_INSERT_ADMISSION_SCHEDULE";
                string SP_Parameters1 = "@P_ADMBATCH_NO,@P_UG_PG,@P_EXAM_DATE,@P_TIME_FROM,@P_TIME_TO,@P_VENUE_NO,@P_VENUE_NAME,@P_CAPACITY,@P_ACTIVE,@P_CREATED_BY,@P_OUTPUT";
                string Call_Values1 = "" + Convert.ToInt32(ddlIntake.SelectedValue) + "," + Stydy + "," + Convert.ToString(txtDate.Text) + "," +
                        Convert.ToString(timefrom) + "," + (timeto.ToString()) + "," + Convert.ToInt32(ddlVenue.SelectedValue) + "," + Convert.ToString(ddlVenue.SelectedItem) + "," +
                        Convert.ToString(txtCapacity.Text) + "," + Convert.ToInt32(Active) + "," + Convert.ToInt32(Session["userno"].ToString()) + ",0";

                string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//insert
                if (que_out1 == "1")
                {
                    objCommon.DisplayMessage(this, "Record Saved Successfully !!", this.Page);
                    BindDetails();
                    Clear();
                    return;
                }
                else if (que_out1 == "2")
                {
                    objCommon.DisplayMessage(this, "Record Already Exists !!", this.Page);
                    Clear();
                    return;
                }
            }
            else if (ViewState["action"].ToString().Equals("edit"))
            {
                string SP_Name1 = "PKG_UPDATE_ADMISSION_SCHEDULE";
                string SP_Parameters1 = "@P_ADMBATCH_NO,@P_UG_PG,@P_EXAM_DATE,@P_TIME_FROM,@P_TIME_TO,@P_VENUE_NO,@P_VENUE_NAME,@P_CAPACITY,@P_ACTIVE,@P_CREATED_BY,@P_EXAM_NO,@P_SCHEDULING_NO,@P_OUTPUT";
                string Call_Values1 = "" + Convert.ToInt32(ddlIntake.SelectedValue) + "," + Convert.ToInt32(ddlStudyLevel.SelectedValue) + "," + Convert.ToString(txtDate.Text) + "," +
                        Convert.ToString(timefrom) + "," + (timeto.ToString()) + "," + Convert.ToInt32(ddlVenue.SelectedValue) + "," + Convert.ToString(ddlVenue.SelectedItem) + "," +
                        Convert.ToString(txtCapacity.Text) + "," + Convert.ToInt32(Active) + "," + Convert.ToInt32(Session["userno"].ToString()) + "," + Convert.ToInt32(ViewState["DATE_no"]) + "," + Convert.ToInt32(ViewState["Schedule_no"]) + ",0";

                string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//insert
                if (que_out1 == "1")
                {
                    objCommon.DisplayMessage(this, "Record Updated Successfully !!", this.Page);
                    ViewState["action"] = "add";
                    BindDetails();
                    Clear();
                    return;
                }
                else if (que_out1 == "2")               
                {
                    objCommon.DisplayMessage(this, "Schedule Cant be Modified because it is opted by Students. !!", this.Page);                    
                    Clear();
                    BindDetails();
                    return;
                }
              
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlIntake.SelectedIndex = 0;
        ddlStudyLevel.SelectedIndex = 0;
        ddlVenue.SelectedIndex = 0;
        txtCapacity.Text = "";
        txtDate.Text = "";
        txtTimeSlot.Text = "";
    }
    private void Clear()
    {
        ddlIntake.SelectedIndex = 0;
        ddlStudyLevel.ClearSelection();
        ddlVenue.SelectedIndex = 0;
        txtCapacity.Text = "";
        txtDate.Text = "";
        txtTimeSlot.Text = "";
    }
    private void BindDetails()
    {
        string SP_Name2 = "PKG_GET_ADMISSION_SCHEDULE";
        string SP_Parameters2 = "@P_OUTPUT";
        string Call_Values2 = "" + 0 + "";
        DataSet dsFacWiseCourseList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (dsFacWiseCourseList.Tables[0].Rows.Count > 0)
        {
            Panel1.Visible = true;
            Lvschedule.DataSource = dsFacWiseCourseList;
            Lvschedule.DataBind();
        }
        else
        {
            Panel1.Visible = false;
            Lvschedule.DataSource = null;
            Lvschedule.DataBind();
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton imageButton = sender as ImageButton;
            int Schedule_no = int.Parse(imageButton.CommandArgument);
            int DATE_NO = int.Parse(imageButton.CommandArgument);
            this.ViewState["Schedule_no"] = (object)int.Parse(imageButton.CommandArgument);
            this.ShowDetails(Schedule_no);
            this.ViewState["DATE_no"] = DATE_NO;
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(this.Session["error"]))
                this.objUCommon.ShowError(this.Page, "ACADEMIC_MASTERS_RoomMaster.btnEdit_Click-> " + ex.Message + ex.StackTrace);
            else
                this.objUCommon.ShowError(this.Page, "Server Unavailable");
        }
    }
    private void ShowDetails(int Schedule_no)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_ADMISSION_TEST_SCHEDULING", "SCHEDULING_NO,ADMBATCH_NO,UG_PG,EXAM_DATE,TIME_FROM,TIME_TO,VENUE_NO,(TIME_FROM +'-'+ TIME_TO)TIME_SLOT", "VENUE_NAME,CAPACITY,ACTIVE,CREATED_BY,CREATED_DATE,EXAMDATE_NO", "SCHEDULING_NO=" + Schedule_no, "SCHEDULING_NO");
            if (ds != null && ds.Tables.Count > 0)
            {
                ddlIntake.SelectedValue = ds.Tables[0].Rows[0]["ADMBATCH_NO"].ToString();
                ddlStudyLevel.SelectedValue = ds.Tables[0].Rows[0]["UG_PG"].ToString();
                txtDate.Text = ds.Tables[0].Rows[0]["EXAM_DATE"].ToString();
                txtTimeSlot.Text = ds.Tables[0].Rows[0]["TIME_SLOT"].ToString();
                ddlVenue.Text = ds.Tables[0].Rows[0]["VENUE_NO"].ToString();
                txtCapacity.Text = ds.Tables[0].Rows[0]["CAPACITY"].ToString();
                if (ds.Tables[0].Rows[0]["ACTIVE"].ToString().Trim() == "1")
                {
                    //chkActive.Checked = true;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(true);", true);
                }
                else
                {
                    //chkActive.Checked = false;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(false);", true);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_RoomMaster.ShowDetails_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
}