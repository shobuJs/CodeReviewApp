using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Text;

public partial class ACADEMIC_CampusWiseIntake : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    MappingController objmp = new MappingController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Config objConfig = new Config();
    // string DepartNos = string.Empty;


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
                this.CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and isnull(ACTIVE,0)=1", "COLLEGE_ID");
                //objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'') COLLEGE_NAME", "COLLEGE_ID > 0 and isnull(ACTIVE,0)=1", "COLLEGE_NAME");
                ddlColg.SelectedIndex = 0;
                objCommon.FillDropDownList(ddlProgClassification, "ACD_UA_SECTION", "UA_SECTION", "ISNULL(UA_SECTIONNAME,'') UA_SECTIONNAME", "UA_SECTION > 0", "UA_SECTION");

                objCommon.FillDropDownList(ddlClassification, "ACD_AREA_OF_INTEREST", "AREA_INT_NO", "AREA_INT_NAME", "AREA_INT_NO > 0", "AREA_INT_NAME");

                //Added by swapnil thakare on dated 12-10-2021
                objCommon.FillDropDownList(ddlCollegeSchNameC, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and isnull(ACTIVE,0)=1", "COLLEGE_ID");
                //objCommon.FillDropDownList(ddlCollegeSchNameC, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'') COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_NAME");
                objCommon.FillDropDownList(ddlStudyLevelC, "ACD_UA_SECTION", "UA_SECTION", "ISNULL(UA_SECTIONNAME,'') UA_SECTIONNAME", "UA_SECTION > 0", "UA_SECTION");
                objCommon.FillDropDownList(ddlClassificationC, "ACD_AREA_OF_INTEREST", "AREA_INT_NO", "AREA_INT_NAME", "AREA_INT_NO > 0", "AREA_INT_NO");

                ddlCollegeSchNameC.SelectedIndex = 0;
                ddlStudyLevelC.SelectedIndex = 0;
                ddlClassificationC.SelectedIndex = 0;

                //end
                objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "DISTINCT BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVE=1", "BATCHNO DESC");

                objCommon.FillDropDownList(ddlIntakeC,  "ACD_ADMBATCH", "DISTINCT BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVE=1", "BATCHNO DESC");




                ddlProgClassification.SelectedIndex = 0;
              //  ddlAdmBatch.SelectedIndex = 0;
              //  DataSet ds = objmp.GetBatchMonthYear();
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    ddlBatchYearMonth.DataSource = ds;
                //    ddlBatchYearMonth.DataValueField = ds.Tables[0].Columns[0].ToString();
                //    ddlBatchYearMonth.DataTextField = ds.Tables[0].Columns[1].ToString();
                //    ddlBatchYearMonth.DataBind();
                //    ddlBatchYearMonth.SelectedIndex = 0;

                //    ddlIntakeC.DataSource = ds;
                //    ddlIntakeC.DataValueField = ds.Tables[0].Columns[0].ToString();
                //    ddlIntakeC.DataTextField = ds.Tables[0].Columns[1].ToString();
                //    ddlIntakeC.DataBind();
                //    ddlIntakeC.SelectedIndex = 0;
                //}
              //  else
              //  {
                //    ddlBatchYearMonth.Items.Clear();
                //    ddlBatchYearMonth.Items.Add(new ListItem("Please Select", "0"));
                //    ddlIntakeC.Items.Clear();
                //    ddlIntakeC.Items.Add(new ListItem("Please Select", "0"));
                //}
                //ddlBatchYearMonth.SelectedIndex = 0;
                ddlIntakeC.SelectedIndex = 0;

           // }
            ViewState["action"] = "add";
            objCommon.SetLabelData(ddlColg.SelectedValue);
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // dynamic page header addded by sarang 09/07/2021

        }

        if (ViewState["ScriptTbl"] != null)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Src", ViewState["ScriptTbl"].ToString(), true);
        }
            }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DepartmentMapping.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DepartmentMapping.aspx");
        }
    }
    #endregion


    protected void btnShow_Click(object sender, EventArgs e)
    {
        divUGPG.Visible = true;
        updGradeEntry.Visible = true;
        ShowDetails(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlProgClassification.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClassification.SelectedValue));
      //  ShowDetails(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlProgClassification.SelectedValue), Convert.ToInt32(ddlBatchYearMonth.SelectedValue));
    }
    protected void btnShowCopy_Click(object sender, EventArgs e)
    {
        divUGPG.Visible = true;
        updProgramCopy.Visible = true;
        ShowDetailsCopy(Convert.ToInt32(ddlCollegeSchNameC.SelectedValue), Convert.ToInt32(ddlStudyLevelC.SelectedValue), Convert.ToInt32(ddlIntakeC.SelectedValue), Convert.ToInt32(ddlClassificationC.SelectedValue));

        //ShowDetails(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlProgClassification.SelectedValue), Convert.ToInt32(ddlBatchYearMonth.SelectedValue));
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.ToString());
        ddlAdmBatch.SelectedIndex = 0;
        ddlClassification.SelectedIndex = 0;
        ddlColg.SelectedIndex = 0;
        ddlProgClassification.SelectedIndex = 0;
        lvCampusWiseIntake.DataSource = null;
        lvCampusWiseIntake.DataBind();
        btnCancel.Visible = false;
        btnSave.Visible = false;
    }

    private void ShowDetails(int clg, int ugpg, int admbatch, int classification_no)
    {
        try
        {          
            string HtmlString = string.Empty;
            int i = 0;

            DataSet ds = objmp.GetCampusForAllot(clg, ugpg, admbatch, classification_no);
            lvCampusWiseIntake.DataSource = ds;
            lvCampusWiseIntake.DataBind();
           
            if (ds.Tables[0].Rows.Count > 0)
            {
                Label lblCAMPUS1 = this.lvCampusWiseIntake.Controls[0].FindControl("lblCAMPUS1") as Label;   
                Label lblCAMPUS2 = this.lvCampusWiseIntake.Controls[0].FindControl("lblCAMPUS2") as Label;
                Label lblCAMPUS3 = this.lvCampusWiseIntake.Controls[0].FindControl("lblCAMPUS3") as Label;
                Label lblCAMPUS4 = this.lvCampusWiseIntake.Controls[0].FindControl("lblCAMPUS4") as Label;
                Label lblCAMPUS5 = this.lvCampusWiseIntake.Controls[0].FindControl("lblCAMPUS5") as Label;
                Label lblCAMPUS6 = this.lvCampusWiseIntake.Controls[0].FindControl("lblCAMPUS6") as Label;
                Label lblCAMPUS7 = this.lvCampusWiseIntake.Controls[0].FindControl("lblCAMPUS7") as Label;
                Label lblCAMPUS8 = this.lvCampusWiseIntake.Controls[0].FindControl("lblCAMPUS8") as Label;
                Label lblCAMPUS9 = this.lvCampusWiseIntake.Controls[0].FindControl("lblCAMPUS9") as Label;
                Label lblCAMPUS10 = this.lvCampusWiseIntake.Controls[0].FindControl("lblCAMPUS10") as Label;

                lblCAMPUS1.Text = "" + ds.Tables[0].Rows[0]["CAMPUS1"].ToString() + "";
                lblCAMPUS1.ToolTip = "" + ds.Tables[0].Rows[0]["CAMPUSNO1"].ToString() + "";
                lblCAMPUS2.Text = "" + ds.Tables[0].Rows[0]["CAMPUS2"].ToString() + "";
                lblCAMPUS2.ToolTip = "" + ds.Tables[0].Rows[0]["CAMPUSNO2"].ToString() + "";
                lblCAMPUS3.Text = "" + ds.Tables[0].Rows[0]["CAMPUS3"].ToString() + "";
                lblCAMPUS3.ToolTip = "" + ds.Tables[0].Rows[0]["CAMPUSNO3"].ToString() + "";
                lblCAMPUS4.Text = "" + ds.Tables[0].Rows[0]["CAMPUS4"].ToString() + "";
                lblCAMPUS4.ToolTip = "" + ds.Tables[0].Rows[0]["CAMPUSNO4"].ToString() + "";
                lblCAMPUS5.Text = "" + ds.Tables[0].Rows[0]["CAMPUS5"].ToString() + "";
                lblCAMPUS5.ToolTip = "" + ds.Tables[0].Rows[0]["CAMPUSNO5"].ToString() + "";
                lblCAMPUS6.Text = "" + ds.Tables[0].Rows[0]["CAMPUS6"].ToString() + "";
                lblCAMPUS6.ToolTip = "" + ds.Tables[0].Rows[0]["CAMPUSNO6"].ToString() + "";
                lblCAMPUS7.Text = "" + ds.Tables[0].Rows[0]["CAMPUS7"].ToString() + "";
                lblCAMPUS7.ToolTip = "" + ds.Tables[0].Rows[0]["CAMPUSNO7"].ToString() + "";
                lblCAMPUS8.Text = "" + ds.Tables[0].Rows[0]["CAMPUS8"].ToString() + "";
                lblCAMPUS8.ToolTip = "" + ds.Tables[0].Rows[0]["CAMPUSNO8"].ToString() + "";
                lblCAMPUS9.Text = "" + ds.Tables[0].Rows[0]["CAMPUS9"].ToString() + "";
                lblCAMPUS9.ToolTip = "" + ds.Tables[0].Rows[0]["CAMPUSNO9"].ToString() + "";
                lblCAMPUS10.Text = "" + ds.Tables[0].Rows[0]["CAMPUS10"].ToString() + "";
                lblCAMPUS10.ToolTip = "" + ds.Tables[0].Rows[0]["CAMPUSNO10"].ToString() + "";

                //divFooter.Visible = true;
                btnSave.Visible = true;
                btnCancel.Visible = true;

             
            }
            else
            {
                objCommon.DisplayMessage(this.updGradeEntry, "No record found!", this.Page);
               // divFooter.Visible = false;
                btnSave.Visible = false;
                btnCancel.Visible = false;
                return;
            }

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Master_CollegeMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ShowDetailsCopy(int clg, int ugpg, int admbatch, int classification_no)
    {
        try
        {
            //For Copy Offer course from one intake to other
            string HtmlString = string.Empty;
            int i = 0;
            DataSet ds = objmp.GetCampusForAllot(clg, ugpg, admbatch, classification_no);
            lvOfferCopy.DataSource = ds;
            lvOfferCopy.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                Label lblCAMPUS1 = this.lvOfferCopy.Controls[0].FindControl("lblCAMPUS1") as Label;
                Label lblCAMPUS2 = this.lvOfferCopy.Controls[0].FindControl("lblCAMPUS2") as Label;
                Label lblCAMPUS3 = this.lvOfferCopy.Controls[0].FindControl("lblCAMPUS3") as Label;
                Label lblCAMPUS4 = this.lvOfferCopy.Controls[0].FindControl("lblCAMPUS4") as Label;
                Label lblCAMPUS5 = this.lvOfferCopy.Controls[0].FindControl("lblCAMPUS5") as Label;
                Label lblCAMPUS6 = this.lvOfferCopy.Controls[0].FindControl("lblCAMPUS6") as Label;
                Label lblCAMPUS7 = this.lvOfferCopy.Controls[0].FindControl("lblCAMPUS7") as Label;
                Label lblCAMPUS8 = this.lvOfferCopy.Controls[0].FindControl("lblCAMPUS8") as Label;
                Label lblCAMPUS9 = this.lvOfferCopy.Controls[0].FindControl("lblCAMPUS9") as Label;
                Label lblCAMPUS10 = this.lvOfferCopy.Controls[0].FindControl("lblCAMPUS10") as Label;

                lblCAMPUS1.Text = "" + ds.Tables[0].Rows[0]["CAMPUS1"].ToString() + "";
                lblCAMPUS1.ToolTip = "" + ds.Tables[0].Rows[0]["CAMPUSNO1"].ToString() + "";
                lblCAMPUS2.Text = "" + ds.Tables[0].Rows[0]["CAMPUS2"].ToString() + "";
                lblCAMPUS2.ToolTip = "" + ds.Tables[0].Rows[0]["CAMPUSNO2"].ToString() + "";
                lblCAMPUS3.Text = "" + ds.Tables[0].Rows[0]["CAMPUS3"].ToString() + "";
                lblCAMPUS3.ToolTip = "" + ds.Tables[0].Rows[0]["CAMPUSNO3"].ToString() + "";
                lblCAMPUS4.Text = "" + ds.Tables[0].Rows[0]["CAMPUS4"].ToString() + "";
                lblCAMPUS4.ToolTip = "" + ds.Tables[0].Rows[0]["CAMPUSNO4"].ToString() + "";
                lblCAMPUS5.Text = "" + ds.Tables[0].Rows[0]["CAMPUS5"].ToString() + "";
                lblCAMPUS5.ToolTip = "" + ds.Tables[0].Rows[0]["CAMPUSNO5"].ToString() + "";
                lblCAMPUS6.Text = "" + ds.Tables[0].Rows[0]["CAMPUS6"].ToString() + "";
                lblCAMPUS6.ToolTip = "" + ds.Tables[0].Rows[0]["CAMPUSNO6"].ToString() + "";
                lblCAMPUS7.Text = "" + ds.Tables[0].Rows[0]["CAMPUS7"].ToString() + "";
                lblCAMPUS7.ToolTip = "" + ds.Tables[0].Rows[0]["CAMPUSNO7"].ToString() + "";
                lblCAMPUS8.Text = "" + ds.Tables[0].Rows[0]["CAMPUS8"].ToString() + "";
                lblCAMPUS8.ToolTip = "" + ds.Tables[0].Rows[0]["CAMPUSNO8"].ToString() + "";
                lblCAMPUS9.Text = "" + ds.Tables[0].Rows[0]["CAMPUS9"].ToString() + "";
                lblCAMPUS9.ToolTip = "" + ds.Tables[0].Rows[0]["CAMPUSNO9"].ToString() + "";
                lblCAMPUS10.Text = "" + ds.Tables[0].Rows[0]["CAMPUS10"].ToString() + "";
                lblCAMPUS10.ToolTip = "" + ds.Tables[0].Rows[0]["CAMPUSNO10"].ToString() + "";

                //divFooter.Visible = true;
                btnCancelCopy.Visible = true;
                lnkSubmit.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(this.updGradeEntry, "No record found!", this.Page);
                // divFooter.Visible = false;
                btnCancelCopy.Visible = false;
                lnkSubmit.Visible = false;
                return;
            }

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Master_CollegeMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void chkCampus_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlProgClassification.SelectedIndex=0;
        ddlClassification.SelectedIndex = 0;
        lvCampusWiseIntake.DataSource = null;
        lvCampusWiseIntake.DataBind();
        btnSave.Visible = false;
        btnCancel.Visible = false;

        btnCancelCopy.Visible = false;
        lnkSubmit.Visible = false;


        lvOfferCopy.DataSource = null;
        lvOfferCopy.DataBind();
        ddlIntakeC.SelectedIndex = 0;
        ddlClassificationC.SelectedIndex = 0;
        ddlStudyLevelC.SelectedIndex = 0;

        ddlToIntake.Items.Clear();
    }

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlProgClassification.SelectedIndex = 0;
        ddlClassification.SelectedIndex = 0;
        lvCampusWiseIntake.DataSource = null;
        lvCampusWiseIntake.DataBind();
        btnSave.Visible = false;
        btnCancel.Visible = false;


        lvOfferCopy.DataSource = null;
        lvOfferCopy.DataBind();
        btnCancelCopy.Visible = false;
        lnkSubmit.Visible = false;


        //objCommon.FillDropDownList(ddlToIntake, "ACD_ADMBATCH A LEFT JOIN ACD_MONTH M ON A.STARTMONTHNO=M.MONTHNO LEFT JOIN ACD_MONTH E ON A.ENDMONTHNO =E.MONTHNO LEFT JOIN ACD_EXAM_YEAR Y ON Y.EXYEAR_NO = A.YEAR", "A.BATCHNO", "(M.MONTH+' - '+E.MONTH+' - '+YEAR_NAME) ADMBATCH", " A.BATCHNO > 0 AND ACTIVE=1 AND A.BATCHNO NOT IN('" + ddlIntakeC.SelectedValue + "')", "A.BATCHNO");


        objCommon.FillDropDownList(ddlToIntake, "ACD_ADMBATCH A LEFT JOIN ACD_MONTH M ON A.STARTMONTHNO=M.MONTHNO LEFT JOIN ACD_MONTH E ON A.ENDMONTHNO =E.MONTHNO LEFT JOIN ACD_EXAM_YEAR Y ON Y.EXYEAR_NO = A.YEAR", "A.BATCHNO", "(BATCHNAME) ADMBATCH", " A.BATCHNO > 0 AND ACTIVE=1 AND A.BATCHNO NOT IN('" + ddlIntakeC.SelectedValue + "')", "A.BATCHNO DESC");
 

    }

    protected void ddlProgClassification_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlClassification.SelectedIndex = 0;
        lvCampusWiseIntake.DataSource = null;
        lvCampusWiseIntake.DataBind();
        btnSave.Visible = false;
        btnCancel.Visible = false;

        lvOfferCopy.DataSource = null;
        lvOfferCopy.DataBind();
        btnCancelCopy.Visible = false;
        lnkSubmit.Visible = false;
    }

    protected void ddlClassification_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCampusWiseIntake.DataSource = null;
        lvCampusWiseIntake.DataBind();
        btnSave.Visible = false;
        btnCancel.Visible = false;

        lvOfferCopy.DataSource = null;
        lvOfferCopy.DataBind();
        btnCancelCopy.Visible = false;
        lnkSubmit.Visible = false;
    }
    // 

    protected void btnSave_Click(object sender, EventArgs e)
    {
         try
        {
            int SaveCount = 0;
            int CheckBoxCount = 0;
            foreach (var item in lvCampusWiseIntake.Items)
            {
                string Degreeno = string.Empty; string Branchno = string.Empty;
                HiddenField hdnDegreeno; HiddenField hdnBranchno;

                string WD1 = string.Empty; string WD2 = string.Empty; string WD3 = string.Empty; string WD4 = string.Empty;
                string WD5 = string.Empty; string WD6 = string.Empty; string WD7 = string.Empty;
                string WD8 = string.Empty; string WD9 = string.Empty; string WD10 = string.Empty;

                string WO1 = string.Empty; string WO2 = string.Empty; string WO3 = string.Empty; string WO4 = string.Empty;
                string WO5 = string.Empty; string WO6 = string.Empty; string WO7 = string.Empty;
                string WO8 = string.Empty; string WO9 = string.Empty; string WO10 = string.Empty;

                string Campus1 = string.Empty; string Campus2 = string.Empty; string Campus3 = string.Empty; string Campus4 = string.Empty;
                string Campus5 = string.Empty; string Campus6 = string.Empty; string Campus7 = string.Empty;
                string Campus8 = string.Empty; string Campus9 = string.Empty; string Campus10 = string.Empty;

                string CAMPUSNO1 = string.Empty; string CAMPUSNO2 = string.Empty; string CAMPUSNO3 = string.Empty; string CAMPUSNO4 = string.Empty; string CAMPUSNO5 = string.Empty; string CAMPUSNO6 = string.Empty; string CAMPUSNO7 = string.Empty; string CAMPUSNO8 = string.Empty; string CAMPUSNO9 = string.Empty; string CAMPUSNO10 = string.Empty;

                string Campus1N = string.Empty; string Campus2N = string.Empty; string Campus3N = string.Empty; string Campus4N = string.Empty;
                string Campus5N = string.Empty; string Campus6N = string.Empty; string Campus7N = string.Empty;
                string Campus8N = string.Empty; string Campus9N = string.Empty; string Campus10N = string.Empty;

                //string CAMPUSNO1N = string.Empty; string CAMPUSNO2N = string.Empty; string CAMPUSNO3N = string.Empty; string CAMPUSNO4N = string.Empty; string CAMPUSNO5N = string.Empty; string CAMPUSNO6N = string.Empty; string CAMPUSNO7N = string.Empty; string CAMPUSNO8N = string.Empty; string CAMPUSNO9N = string.Empty; string CAMPUSNO10N = string.Empty;

                Label lblDegreeno; Label lblBranchno; //CheckBoxList chkCampus;//PlaceHolder phRecords;
               
                TextBox txtCampus1; TextBox txtCampus2; TextBox txtCampus3; TextBox txtCampus4; TextBox txtCampus5; TextBox txtCampus6;
                TextBox txtCampus7; TextBox txtCampus8; TextBox txtCampus9; TextBox txtCampus10;

                TextBox txtCampus1N; TextBox txtCampus2N; TextBox txtCampus3N; TextBox txtCampus4N; TextBox txtCampus5N; TextBox txtCampus6N;
                TextBox txtCampus7N; TextBox txtCampus8N; TextBox txtCampus9N; TextBox txtCampus10N;

                Label lblWD1; Label lblWD2; Label lblWD3; Label lblWD4; Label lblWD5; Label lblWD6; Label lblWD7; Label lblWD8; Label lblWD9; 
                Label lblWD10;
                Label lblWE1; Label lblWE2; Label lblWE3; Label lblWE4; Label lblWE5; Label lblWE6; Label lblWE7; Label lblWE8; Label lblWE9; 
                Label lblWE10;
                //for checkbox list start

                Label degree = item.FindControl("lblDegreeHidden") as Label;
                Label branch = item.FindControl("lblBranchHidden") as Label;
                //hdnDegreeno = item.FindControl("hdnDegreeno") as HiddenField;
                //lblDegreeno = item.FindControl("lblDegreeno") as Label;
                //Degreeno = hdnDegreeno.Value; // lblDegreeno.ToolTip;
                Degreeno = degree.Text;
                //for checkbox list start
                //hdnBranchno = item.FindControl("hdnBranchNo") as HiddenField;
                //lblBranchno = item.FindControl("lblBranchno") as Label;
                //Branchno = hdnBranchno.Value; // lblBranchno.ToolTip;
                Branchno = branch.Text;
                //1   

                lblWD1 = item.FindControl("lblWD1") as Label;
                lblWE1 = item.FindControl("lblWE1") as Label;
                txtCampus1 = item.FindControl("txtCampus1") as TextBox; 
                txtCampus1N = item.FindControl("txtCampus1N") as TextBox; // get
                CAMPUSNO1 = txtCampus1.ToolTip == string.Empty ? "0" : txtCampus1.ToolTip;

                if (txtCampus1.Text.Trim() != string.Empty || txtCampus1.Text.Trim() != "" || txtCampus1N.Text.Trim() != string.Empty || txtCampus1N.Text.Trim() != "")
                {
                    CheckBoxCount = 1;
                    WD1 = lblWD1.ToolTip == string.Empty ? "0" : lblWD1.ToolTip;
                    Campus1 = txtCampus1.Text.Trim() == string.Empty ? "0" : txtCampus1.Text;
                    WO1 = lblWE1.ToolTip == string.Empty ? "0" : lblWE1.ToolTip;
                    Campus1N = txtCampus1N.Text.Trim() == string.Empty ? "0" : txtCampus1N.Text;
       
                    if (txtCampus1N.Text.Trim() != string.Empty || txtCampus1N.Text.Trim() != "")
                    {
                        WO1 = lblWE1.ToolTip == string.Empty ? "0" : lblWE1.ToolTip;
                        Campus1N = txtCampus1N.Text.Trim() == string.Empty ? "0" : txtCampus1N.Text;
                    }
                    else
                    {
                        WO1 = "0";
                        Campus1N = "0";
                    }

                    if (txtCampus1.Text.Trim() != string.Empty || txtCampus1.Text.Trim() != "")
                    {
                        WD1 = lblWD1.ToolTip == string.Empty ? "0" : lblWD1.ToolTip;
                        Campus1 = txtCampus1.Text.Trim() == string.Empty ? "0" : txtCampus1.Text;
                        //WO1 = "0";
                        //Campus1N = "0";
                    }
                    else
                    {
                        WD1 = "0";
                        Campus1 = "0";
                    }
                }
                else
                {
                    WD1 = "0";
                    Campus1 = "0";
                    WO1 = "0";
                    Campus1N = "0";
                }


                //2  

                lblWD2 = item.FindControl("lblWD2") as Label;
                lblWE2 = item.FindControl("lblWE2") as Label;
                txtCampus2 = item.FindControl("txtCampus2") as TextBox;
                txtCampus2N = item.FindControl("txtCampus2N") as TextBox; // get
                CAMPUSNO2 = txtCampus2.ToolTip == string.Empty ? "0" : txtCampus2.ToolTip;

                if (txtCampus2.Text.Trim() != string.Empty || txtCampus2.Text.Trim() != "" || txtCampus2N.Text.Trim() != string.Empty || txtCampus2N.Text.Trim() != "")
                {
                    CheckBoxCount = 1;
                    WD2 = lblWD2.ToolTip == string.Empty ? "0" : lblWD2.ToolTip;
                    Campus2 = txtCampus2.Text.Trim() == string.Empty ? "0" : txtCampus2.Text;
                    WO2 = lblWE2.ToolTip == string.Empty ? "0" : lblWE2.ToolTip;
                    Campus2N = txtCampus2N.Text.Trim() == string.Empty ? "0" : txtCampus2N.Text;

                    if (txtCampus2N.Text.Trim() != string.Empty || txtCampus2N.Text.Trim() != "")
                    {
                        WO2 = lblWE2.ToolTip == string.Empty ? "0" : lblWE2.ToolTip;
                        Campus2N = txtCampus2N.Text.Trim() == string.Empty ? "0" : txtCampus2N.Text;
                    }
                    else
                    {
                        WO2 = "0";
                        Campus2N = "0";
                    }

                    if (txtCampus2.Text.Trim() != string.Empty || txtCampus2.Text.Trim() != "")
                    {
                        WD2 = lblWD2.ToolTip == string.Empty ? "0" : lblWD2.ToolTip;
                        Campus2 = txtCampus2.Text.Trim() == string.Empty ? "0" : txtCampus2.Text;
                    }
                    else
                    {
                        WD2 = "0";
                        Campus2 = "0";
                    }
                }
                else
                {
                    WD2 = "0";
                    Campus2 = "0";
                    WO2 = "0";
                    Campus2N = "0";
                }

                //3   

                lblWD3 = item.FindControl("lblWD3") as Label;
                lblWE3 = item.FindControl("lblWE3") as Label;
                txtCampus3 = item.FindControl("txtCampus3") as TextBox;
                txtCampus3N = item.FindControl("txtCampus3N") as TextBox; // get
                CAMPUSNO3 = txtCampus3.ToolTip == string.Empty ? "0" : txtCampus3.ToolTip;

                if (txtCampus3.Text.Trim() != string.Empty || txtCampus3.Text.Trim() != "" || txtCampus3N.Text.Trim() != string.Empty || txtCampus3N.Text.Trim() != "")
                {
                    CheckBoxCount = 1;
                    WD3 = lblWD3.ToolTip == string.Empty ? "0" : lblWD3.ToolTip;
                    Campus3 = txtCampus3.Text.Trim() == string.Empty ? "0" : txtCampus3.Text;
                    WO3 = lblWE3.ToolTip == string.Empty ? "0" : lblWE3.ToolTip;
                    Campus3N = txtCampus3N.Text.Trim() == string.Empty ? "0" : txtCampus3N.Text;

                    if (txtCampus3N.Text.Trim() != string.Empty || txtCampus3N.Text.Trim() != "")
                    {
                        WO3 = lblWE3.ToolTip == string.Empty ? "0" : lblWE3.ToolTip;
                        Campus3N = txtCampus3N.Text.Trim() == string.Empty ? "0" : txtCampus3N.Text;
                    }
                    else
                    {
                        WO3 = "0";
                        Campus3N = "0";
                    }

                    if (txtCampus3.Text.Trim() != string.Empty || txtCampus3.Text.Trim() != "")
                    {
                        WD3 = lblWD3.ToolTip == string.Empty ? "0" : lblWD3.ToolTip;
                        Campus3 = txtCampus3.Text.Trim() == string.Empty ? "0" : txtCampus3.Text;
                    }
                    else
                    {
                        WD3 = "0";
                        Campus3 = "0";
                    }
                }
                else
                {
                    WD3 = "0";
                    Campus3 = "0";
                    WO3 = "0";
                    Campus3N = "0";
                }


                //4   

                lblWD4 = item.FindControl("lblWD4") as Label;
                lblWE4 = item.FindControl("lblWE4") as Label;
                txtCampus4 = item.FindControl("txtCampus4") as TextBox;
                txtCampus4N = item.FindControl("txtCampus4N") as TextBox; // get
                CAMPUSNO4 = txtCampus4.ToolTip == string.Empty ? "0" : txtCampus4.ToolTip;

                if (txtCampus4.Text.Trim() != string.Empty || txtCampus4.Text.Trim() != "" || txtCampus4N.Text.Trim() != string.Empty || txtCampus4N.Text.Trim() != "")
                {
                    CheckBoxCount = 1;
                    WD4 = lblWD4.ToolTip == string.Empty ? "0" : lblWD4.ToolTip;
                    Campus4 = txtCampus4.Text.Trim() == string.Empty ? "0" : txtCampus4.Text;
                    WO4 = lblWE4.ToolTip == string.Empty ? "0" : lblWE4.ToolTip;
                    Campus4N = txtCampus4N.Text.Trim() == string.Empty ? "0" : txtCampus4N.Text;

                    if (txtCampus4N.Text.Trim() != string.Empty || txtCampus4N.Text.Trim() != "")
                    {
                        WO4 = lblWE4.ToolTip == string.Empty ? "0" : lblWE4.ToolTip;
                        Campus4N = txtCampus4N.Text.Trim() == string.Empty ? "0" : txtCampus4N.Text;
                    }
                    else
                    {
                        WO4 = "0";
                        Campus4N = "0";
                    }

                    if (txtCampus4.Text.Trim() != string.Empty || txtCampus4.Text.Trim() != "")
                    {
                        WD4 = lblWD4.ToolTip == string.Empty ? "0" : lblWD4.ToolTip;
                        Campus4 = txtCampus4.Text.Trim() == string.Empty ? "0" : txtCampus4.Text;
                    }
                    else
                    {
                        WD4 = "0";
                        Campus4 = "0";
                    }
                }
                else
                {
                    WD4 = "0";
                    Campus4 = "0";
                    WO4 = "0";
                    Campus4N = "0";
                }


                //5   

                lblWD5 = item.FindControl("lblWD5") as Label;
                lblWE5 = item.FindControl("lblWE5") as Label;
                txtCampus5 = item.FindControl("txtCampus5") as TextBox;
                txtCampus5N = item.FindControl("txtCampus5N") as TextBox; // get
                CAMPUSNO5 = txtCampus5.ToolTip == string.Empty ? "0" : txtCampus5.ToolTip;

                if (txtCampus5.Text.Trim() != string.Empty || txtCampus5.Text.Trim() != "" || txtCampus5N.Text.Trim() != string.Empty || txtCampus5N.Text.Trim() != "")
                {
                    CheckBoxCount = 1;
                    WD5 = lblWD5.ToolTip == string.Empty ? "0" : lblWD5.ToolTip;
                    Campus5 = txtCampus5.Text.Trim() == string.Empty ? "0" : txtCampus5.Text;
                    WO5 = lblWE5.ToolTip == string.Empty ? "0" : lblWE5.ToolTip;
                    Campus5N = txtCampus5N.Text.Trim() == string.Empty ? "0" : txtCampus5N.Text;

                    if (txtCampus5N.Text.Trim() != string.Empty || txtCampus5N.Text.Trim() != "")
                    {
                        WO5 = lblWE5.ToolTip == string.Empty ? "0" : lblWE5.ToolTip;
                        Campus5N = txtCampus5N.Text.Trim() == string.Empty ? "0" : txtCampus5N.Text;
                    }
                    else
                    {
                        WO5 = "0";
                        Campus5N = "0";
                    }

                    if (txtCampus5.Text.Trim() != string.Empty || txtCampus5.Text.Trim() != "")
                    {
                        WD5 = lblWD5.ToolTip == string.Empty ? "0" : lblWD5.ToolTip;
                        Campus5 = txtCampus5.Text.Trim() == string.Empty ? "0" : txtCampus5.Text;
                    }
                    else
                    {
                        WD5 = "0";
                        Campus5 = "0";
                    }
                }
                else
                {
                    WD5 = "0";
                    Campus5 = "0";
                    WO5 = "0";
                    Campus5N = "0";
                }


                //6  

                lblWD6 = item.FindControl("lblWD6") as Label;
                lblWE6 = item.FindControl("lblWE6") as Label;
                txtCampus6 = item.FindControl("txtCampus6") as TextBox;
                txtCampus6N = item.FindControl("txtCampus6N") as TextBox; // get
                CAMPUSNO6 = txtCampus6.ToolTip == string.Empty ? "0" : txtCampus6.ToolTip;

                if (txtCampus6.Text.Trim() != string.Empty || txtCampus6.Text.Trim() != "" || txtCampus6N.Text.Trim() != string.Empty || txtCampus6N.Text.Trim() != "")
                {
                    CheckBoxCount = 1;
                    WD6 = lblWD6.ToolTip == string.Empty ? "0" : lblWD6.ToolTip;
                    Campus6 = txtCampus6.Text.Trim() == string.Empty ? "0" : txtCampus6.Text;
                    WO6 = lblWE6.ToolTip == string.Empty ? "0" : lblWE6.ToolTip;
                    Campus6N = txtCampus6N.Text.Trim() == string.Empty ? "0" : txtCampus6N.Text;

                    if (txtCampus6N.Text.Trim() != string.Empty || txtCampus6N.Text.Trim() != "")
                    {
                        WO6 = lblWE6.ToolTip == string.Empty ? "0" : lblWE6.ToolTip;
                        Campus6N = txtCampus6N.Text.Trim() == string.Empty ? "0" : txtCampus6N.Text;
                    }
                    else
                    {
                        WO6 = "0";
                        Campus6N = "0";
                    }

                    if (txtCampus6.Text.Trim() != string.Empty || txtCampus6.Text.Trim() != "")
                    {
                        WD6 = lblWD6.ToolTip == string.Empty ? "0" : lblWD6.ToolTip;
                        Campus6 = txtCampus6.Text.Trim() == string.Empty ? "0" : txtCampus6.Text;
                    }
                    else
                    {
                        WD6 = "0";
                        Campus6 = "0";
                    }
                }
                else
                {
                    WD6 = "0";
                    Campus6 = "0";
                    WO6 = "0";
                    Campus6N = "0";
                }


                //7  

                lblWD7 = item.FindControl("lblWD7") as Label;
                lblWE7 = item.FindControl("lblWE7") as Label;
                txtCampus7 = item.FindControl("txtCampus7") as TextBox;
                txtCampus7N = item.FindControl("txtCampus7N") as TextBox; // get
                CAMPUSNO7 = txtCampus7.ToolTip == string.Empty ? "0" : txtCampus7.ToolTip;

                if (txtCampus7.Text.Trim() != string.Empty || txtCampus7.Text.Trim() != "" || txtCampus7N.Text.Trim() != string.Empty || txtCampus7N.Text.Trim() != "")
                {
                    CheckBoxCount = 1;
                    WD7 = lblWD7.ToolTip == string.Empty ? "0" : lblWD7.ToolTip;
                    Campus7 = txtCampus7.Text.Trim() == string.Empty ? "0" : txtCampus7.Text;
                    WO7 = lblWE7.ToolTip == string.Empty ? "0" : lblWE7.ToolTip;
                    Campus7N = txtCampus7N.Text.Trim() == string.Empty ? "0" : txtCampus7N.Text;

                    if (txtCampus7N.Text.Trim() != string.Empty || txtCampus7N.Text.Trim() != "")
                    {
                        WO7 = lblWE7.ToolTip == string.Empty ? "0" : lblWE7.ToolTip;
                        Campus7N = txtCampus7N.Text.Trim() == string.Empty ? "0" : txtCampus7N.Text;
                    }
                    else
                    {
                        WO7 = "0";
                        Campus7N = "0";
                    }

                    if (txtCampus7.Text.Trim() != string.Empty || txtCampus7.Text.Trim() != "")
                    {
                        WD7 = lblWD7.ToolTip == string.Empty ? "0" : lblWD7.ToolTip;
                        Campus7 = txtCampus7.Text.Trim() == string.Empty ? "0" : txtCampus7.Text;
                    }
                    else
                    {
                        WD7 = "0";
                        Campus7 = "0";
                    }
                }
                else
                {
                    WD7 = "0";
                    Campus7 = "0";
                    WO7 = "0";
                    Campus7N = "0";
                }


                //8  

                lblWD8 = item.FindControl("lblWD8") as Label;
                lblWE8 = item.FindControl("lblWE8") as Label;
                txtCampus8 = item.FindControl("txtCampus8") as TextBox;
                txtCampus8N = item.FindControl("txtCampus8N") as TextBox; // get
                CAMPUSNO8 = txtCampus8.ToolTip == string.Empty ? "0" : txtCampus8.ToolTip;

                if (txtCampus8.Text.Trim() != string.Empty || txtCampus8.Text.Trim() != "" || txtCampus8N.Text.Trim() != string.Empty || txtCampus8N.Text.Trim() != "")
                {
                    CheckBoxCount = 1;
                    WD8 = lblWD8.ToolTip == string.Empty ? "0" : lblWD8.ToolTip;
                    Campus8 = txtCampus8.Text.Trim() == string.Empty ? "0" : txtCampus8.Text;
                    WO8 = lblWE8.ToolTip == string.Empty ? "0" : lblWE8.ToolTip;
                    Campus8N = txtCampus8N.Text.Trim() == string.Empty ? "0" : txtCampus8N.Text;

                    if (txtCampus8N.Text.Trim() != string.Empty || txtCampus8N.Text.Trim() != "")
                    {
                        WO8 = lblWE8.ToolTip == string.Empty ? "0" : lblWE8.ToolTip;
                        Campus8N = txtCampus8N.Text.Trim() == string.Empty ? "0" : txtCampus8N.Text;
                    }
                    else
                    {
                        WO8 = "0";
                        Campus8N = "0";
                    }

                    if (txtCampus8.Text.Trim() != string.Empty || txtCampus8.Text.Trim() != "")
                    {
                        WD8 = lblWD8.ToolTip == string.Empty ? "0" : lblWD8.ToolTip;
                        Campus8 = txtCampus8.Text.Trim() == string.Empty ? "0" : txtCampus8.Text;
                    }
                    else
                    {
                        WD8 = "0";
                        Campus8 = "0";
                    }
                }
                else
                {
                    WD8 = "0";
                    Campus8 = "0";
                    WO8 = "0";
                    Campus8N = "0";
                }


                //9  

                lblWD9 = item.FindControl("lblWD9") as Label;
                lblWE9 = item.FindControl("lblWE9") as Label;
                txtCampus9 = item.FindControl("txtCampus9") as TextBox;
                txtCampus9N = item.FindControl("txtCampus9N") as TextBox; // get
                CAMPUSNO9 = txtCampus9.ToolTip == string.Empty ? "0" : txtCampus9.ToolTip;

                if (txtCampus9.Text.Trim() != string.Empty || txtCampus9.Text.Trim() != "" || txtCampus9N.Text.Trim() != string.Empty || txtCampus9N.Text.Trim() != "")
                {
                    CheckBoxCount = 1;
                    WD9 = lblWD9.ToolTip == string.Empty ? "0" : lblWD9.ToolTip;
                    Campus9 = txtCampus9.Text.Trim() == string.Empty ? "0" : txtCampus9.Text;
                    WO9 = lblWE9.ToolTip == string.Empty ? "0" : lblWE9.ToolTip;
                    Campus9N = txtCampus9N.Text.Trim() == string.Empty ? "0" : txtCampus9N.Text;

                    if (txtCampus9N.Text.Trim() != string.Empty || txtCampus9N.Text.Trim() != "")
                    {
                        WO9 = lblWE9.ToolTip == string.Empty ? "0" : lblWE9.ToolTip;
                        Campus9N = txtCampus9N.Text.Trim() == string.Empty ? "0" : txtCampus9N.Text;
                    }
                    else
                    {
                        WO9 = "0";
                        Campus9N = "0";
                    }

                    if (txtCampus9.Text.Trim() != string.Empty || txtCampus9.Text.Trim() != "")
                    {
                        WD9 = lblWD9.ToolTip == string.Empty ? "0" : lblWD9.ToolTip;
                        Campus9 = txtCampus9.Text.Trim() == string.Empty ? "0" : txtCampus9.Text;
                    }
                    else
                    {
                        WD9 = "0";
                        Campus9 = "0";
                    }
                }
                else
                {
                    WD9 = "0";
                    Campus9 = "0";
                    WO9 = "0";
                    Campus9N = "0";
                }


                //10 

                lblWD10 = item.FindControl("lblWD10") as Label;
                lblWE10 = item.FindControl("lblWE10") as Label;
                txtCampus10 = item.FindControl("txtCampus10") as TextBox;
                txtCampus10N = item.FindControl("txtCampus10N") as TextBox; // get
                CAMPUSNO10 = txtCampus10.ToolTip == string.Empty ? "0" : txtCampus10.ToolTip;

                if (txtCampus10.Text.Trim() != string.Empty || txtCampus10.Text.Trim() != "" || txtCampus10N.Text.Trim() != string.Empty || txtCampus10N.Text.Trim() != "")
                {
                    CheckBoxCount = 1;
                    WD10 = lblWD10.ToolTip == string.Empty ? "0" : lblWD10.ToolTip;
                    Campus10 = txtCampus10.Text.Trim() == string.Empty ? "0" : txtCampus10.Text;
                    WO10 = lblWE10.ToolTip == string.Empty ? "0" : lblWE10.ToolTip;
                    Campus10N = txtCampus10N.Text.Trim() == string.Empty ? "0" : txtCampus10N.Text;

                    if (txtCampus10N.Text.Trim() != string.Empty || txtCampus10N.Text.Trim() != "")
                    {
                        WO10 = lblWE10.ToolTip == string.Empty ? "0" : lblWE10.ToolTip;
                        Campus10N = txtCampus10N.Text.Trim() == string.Empty ? "0" : txtCampus10N.Text;
                    }
                    else
                    {
                        WO10 = "0";
                        Campus10N = "0";
                    }

                    if (txtCampus10.Text.Trim() != string.Empty || txtCampus10.Text.Trim() != "")
                    {
                        WD10 = lblWD10.ToolTip == string.Empty ? "0" : lblWD10.ToolTip;
                        Campus10 = txtCampus10.Text.Trim() == string.Empty ? "0" : txtCampus10.Text;
                    }
                    else
                    {
                        WD10 = "0";
                        Campus10 = "0";
                    }
                }
                else
                {
                    WD10 = "0";
                    Campus10 = "0";
                    WO10 = "0";
                    Campus10N = "0";
                }

               
                int ck = 0;
                ck = objmp.AddCampusIntake(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlProgClassification.SelectedValue), Convert.ToInt32(Degreeno), Convert.ToInt32(Branchno), 1, Convert.ToInt32(WD1), Convert.ToInt32(WD2), Convert.ToInt32(WD3), Convert.ToInt32(WD4), Convert.ToInt32(WD5), Convert.ToInt32(WD6), Convert.ToInt32(WD7), Convert.ToInt32(WD8), Convert.ToInt32(WD9), Convert.ToInt32(WD10), Convert.ToInt32(Campus1), Convert.ToInt32(Campus2), Convert.ToInt32(Campus3), Convert.ToInt32(Campus4), Convert.ToInt32(Campus5), Convert.ToInt32(Campus6), Convert.ToInt32(Campus7), Convert.ToInt32(Campus8), Convert.ToInt32(Campus9), Convert.ToInt32(Campus10), Convert.ToInt32(CAMPUSNO1), Convert.ToInt32(CAMPUSNO2), Convert.ToInt32(CAMPUSNO3), Convert.ToInt32(CAMPUSNO4), Convert.ToInt32(CAMPUSNO5), Convert.ToInt32(CAMPUSNO6), Convert.ToInt32(CAMPUSNO7), Convert.ToInt32(CAMPUSNO8), Convert.ToInt32(CAMPUSNO9), Convert.ToInt32(CAMPUSNO10), Convert.ToInt32(WO1), Convert.ToInt32(WO2), Convert.ToInt32(WO3), Convert.ToInt32(WO4), Convert.ToInt32(WO5), Convert.ToInt32(WO6), Convert.ToInt32(WO7), Convert.ToInt32(WO8), Convert.ToInt32(WO9), Convert.ToInt32(WO10), Convert.ToInt32(Campus1N), Convert.ToInt32(Campus2N), Convert.ToInt32(Campus3N), Convert.ToInt32(Campus4N), Convert.ToInt32(Campus5N), Convert.ToInt32(Campus6N), Convert.ToInt32(Campus7N), Convert.ToInt32(Campus8N), Convert.ToInt32(Campus9N), Convert.ToInt32(Campus10N), Convert.ToInt32(Session["userno"].ToString()));

                if (CheckBoxCount > 0)
                {
                   
                    if (ck == 1)
                    {
                        SaveCount ++;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updGradeEntry, "Few or All Record already Exist", this.Page);
                        return;
                    }
                }
            }

            if (SaveCount > 0)
            {
                objCommon.DisplayMessage(this.updGradeEntry, "Record Save Successfully", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updGradeEntry, "Please Enter Campus Intake Details !", this.Page);
                  return;
            }

            ShowDetails(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlProgClassification.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClassification.SelectedValue));
              
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Master_CollegeMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    
    }



    protected void btnSaveCopy_Click(object sender, EventArgs e)
    {

        try
        {
            int ck1 = 0;
            ck1 = objmp.addCampusIntakeCopy(Convert.ToInt32(ddlIntakeC.SelectedValue), Convert.ToInt32(ddlToIntake.SelectedValue), Convert.ToInt32(Session["userno"].ToString()));

            if (ck1 == 1)
            {
  
                objCommon.DisplayMessage(this.updGradeEntry, "Record Copy Successfully", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updGradeEntry, "Few or All Record already Exist", this.Page);
                return;
            }
            //int SaveCount = 0;
            //int CheckBoxCount = 0;
            //foreach (var item in lvOfferCopy.Items)
            //{
            //    string Degreeno = string.Empty; string Branchno = string.Empty;

            //    string WD1 = string.Empty; string WD2 = string.Empty; string WD3 = string.Empty; string WD4 = string.Empty;
            //    string WD5 = string.Empty; string WD6 = string.Empty; string WD7 = string.Empty;
            //    string WD8 = string.Empty; string WD9 = string.Empty; string WD10 = string.Empty;

            //    string WO1 = string.Empty; string WO2 = string.Empty; string WO3 = string.Empty; string WO4 = string.Empty;
            //    string WO5 = string.Empty; string WO6 = string.Empty; string WO7 = string.Empty;
            //    string WO8 = string.Empty; string WO9 = string.Empty; string WO10 = string.Empty;

            //    string Campus1 = string.Empty; string Campus2 = string.Empty; string Campus3 = string.Empty; string Campus4 = string.Empty;
            //    string Campus5 = string.Empty; string Campus6 = string.Empty; string Campus7 = string.Empty;
            //    string Campus8 = string.Empty; string Campus9 = string.Empty; string Campus10 = string.Empty;

            //    string CAMPUSNO1 = string.Empty; string CAMPUSNO2 = string.Empty; string CAMPUSNO3 = string.Empty; string CAMPUSNO4 = string.Empty; string CAMPUSNO5 = string.Empty; string CAMPUSNO6 = string.Empty; string CAMPUSNO7 = string.Empty; string CAMPUSNO8 = string.Empty; string CAMPUSNO9 = string.Empty; string CAMPUSNO10 = string.Empty;

            //    string Campus1N = string.Empty; string Campus2N = string.Empty; string Campus3N = string.Empty; string Campus4N = string.Empty;
            //    string Campus5N = string.Empty; string Campus6N = string.Empty; string Campus7N = string.Empty;
            //    string Campus8N = string.Empty; string Campus9N = string.Empty; string Campus10N = string.Empty;

            //    //string CAMPUSNO1N = string.Empty; string CAMPUSNO2N = string.Empty; string CAMPUSNO3N = string.Empty; string CAMPUSNO4N = string.Empty; string CAMPUSNO5N = string.Empty; string CAMPUSNO6N = string.Empty; string CAMPUSNO7N = string.Empty; string CAMPUSNO8N = string.Empty; string CAMPUSNO9N = string.Empty; string CAMPUSNO10N = string.Empty;

            //    Label lblDegreeno; Label lblBranchno; //CheckBoxList chkCampus;//PlaceHolder phRecords;
            //    HiddenField hdnDegreeno; HiddenField hdnBranchno;

            //    TextBox txtCampus1; TextBox txtCampus2; TextBox txtCampus3; TextBox txtCampus4; TextBox txtCampus5; TextBox txtCampus6;
            //    TextBox txtCampus7; TextBox txtCampus8; TextBox txtCampus9; TextBox txtCampus10;

            //    TextBox txtCampus1N; TextBox txtCampus2N; TextBox txtCampus3N; TextBox txtCampus4N; TextBox txtCampus5N; TextBox txtCampus6N;
            //    TextBox txtCampus7N; TextBox txtCampus8N; TextBox txtCampus9N; TextBox txtCampus10N;

            //    Label lblWD1; Label lblWD2; Label lblWD3; Label lblWD4; Label lblWD5; Label lblWD6; Label lblWD7; Label lblWD8; Label lblWD9;
            //    Label lblWD10;
            //    Label lblWE1; Label lblWE2; Label lblWE3; Label lblWE4; Label lblWE5; Label lblWE6; Label lblWE7; Label lblWE8; Label lblWE9;
            //    Label lblWE10;
            //    //for checkbox list start

            //    hdnDegreeno = item.FindControl("hdnDegreeno") as HiddenField;
            //    //lblDegreeno = item.FindControl("lblDegreeno") as Label;
            //    Degreeno = hdnDegreeno.Value; // lblDegreeno.ToolTip;

            //    //for checkbox list start
            //    hdnBranchno = item.FindControl("hdnBranchNo") as HiddenField;
            //    //lblBranchno = item.FindControl("lblBranchno") as Label;
            //    Branchno = hdnBranchno.Value; // lblBranchno.ToolTip;

            //    CheckBox chkSelect = item.FindControl("chkSelect") as CheckBox;

            //    if (chkSelect.Checked == true)
            //    {

            //        //1   

            //        lblWD1 = item.FindControl("lblWD1") as Label;
            //        lblWE1 = item.FindControl("lblWE1") as Label;
            //        txtCampus1 = item.FindControl("txtCampus1") as TextBox;
            //        txtCampus1N = item.FindControl("txtCampus1N") as TextBox; // get
            //        CAMPUSNO1 = txtCampus1.ToolTip == string.Empty ? "0" : txtCampus1.ToolTip;

            //        if (txtCampus1.Text.Trim() != string.Empty || txtCampus1.Text.Trim() != "" || txtCampus1N.Text.Trim() != string.Empty || txtCampus1N.Text.Trim() != "")
            //        {
            //            CheckBoxCount = 1;
            //            WD1 = lblWD1.ToolTip == string.Empty ? "0" : lblWD1.ToolTip;
            //            Campus1 = txtCampus1.Text.Trim() == string.Empty ? "0" : txtCampus1.Text;
            //            WO1 = lblWE1.ToolTip == string.Empty ? "0" : lblWE1.ToolTip;
            //            Campus1N = txtCampus1N.Text.Trim() == string.Empty ? "0" : txtCampus1N.Text;

            //            if (txtCampus1N.Text.Trim() != string.Empty || txtCampus1N.Text.Trim() != "")
            //            {
            //                WO1 = lblWE1.ToolTip == string.Empty ? "0" : lblWE1.ToolTip;
            //                Campus1N = txtCampus1N.Text.Trim() == string.Empty ? "0" : txtCampus1N.Text;
            //            }
            //            else
            //            {
            //                WO1 = "0";
            //                Campus1N = "0";
            //            }

            //            if (txtCampus1.Text.Trim() != string.Empty || txtCampus1.Text.Trim() != "")
            //            {
            //                WD1 = lblWD1.ToolTip == string.Empty ? "0" : lblWD1.ToolTip;
            //                Campus1 = txtCampus1.Text.Trim() == string.Empty ? "0" : txtCampus1.Text;
            //                //WO1 = "0";
            //                //Campus1N = "0";
            //            }
            //            else
            //            {
            //                WD1 = "0";
            //                Campus1 = "0";
            //            }
            //        }
            //        else
            //        {
            //            WD1 = "0";
            //            Campus1 = "0";
            //            WO1 = "0";
            //            Campus1N = "0";
            //        }


            //        //2  

            //        lblWD2 = item.FindControl("lblWD2") as Label;
            //        lblWE2 = item.FindControl("lblWE2") as Label;
            //        txtCampus2 = item.FindControl("txtCampus2") as TextBox;
            //        txtCampus2N = item.FindControl("txtCampus2N") as TextBox; // get
            //        CAMPUSNO2 = txtCampus2.ToolTip == string.Empty ? "0" : txtCampus2.ToolTip;

            //        if (txtCampus2.Text.Trim() != string.Empty || txtCampus2.Text.Trim() != "" || txtCampus2N.Text.Trim() != string.Empty || txtCampus2N.Text.Trim() != "")
            //        {
            //            CheckBoxCount = 1;
            //            WD2 = lblWD2.ToolTip == string.Empty ? "0" : lblWD2.ToolTip;
            //            Campus2 = txtCampus2.Text.Trim() == string.Empty ? "0" : txtCampus2.Text;
            //            WO2 = lblWE2.ToolTip == string.Empty ? "0" : lblWE2.ToolTip;
            //            Campus2N = txtCampus2N.Text.Trim() == string.Empty ? "0" : txtCampus2N.Text;

            //            if (txtCampus2N.Text.Trim() != string.Empty || txtCampus2N.Text.Trim() != "")
            //            {
            //                WO2 = lblWE2.ToolTip == string.Empty ? "0" : lblWE2.ToolTip;
            //                Campus2N = txtCampus2N.Text.Trim() == string.Empty ? "0" : txtCampus2N.Text;
            //            }
            //            else
            //            {
            //                WO2 = "0";
            //                Campus2N = "0";
            //            }

            //            if (txtCampus2.Text.Trim() != string.Empty || txtCampus2.Text.Trim() != "")
            //            {
            //                WD2 = lblWD2.ToolTip == string.Empty ? "0" : lblWD2.ToolTip;
            //                Campus2 = txtCampus2.Text.Trim() == string.Empty ? "0" : txtCampus2.Text;
            //            }
            //            else
            //            {
            //                WD2 = "0";
            //                Campus2 = "0";
            //            }
            //        }
            //        else
            //        {
            //            WD2 = "0";
            //            Campus2 = "0";
            //            WO2 = "0";
            //            Campus2N = "0";
            //        }

            //        //3   

            //        lblWD3 = item.FindControl("lblWD3") as Label;
            //        lblWE3 = item.FindControl("lblWE3") as Label;
            //        txtCampus3 = item.FindControl("txtCampus3") as TextBox;
            //        txtCampus3N = item.FindControl("txtCampus3N") as TextBox; // get
            //        CAMPUSNO3 = txtCampus3.ToolTip == string.Empty ? "0" : txtCampus3.ToolTip;

            //        if (txtCampus3.Text.Trim() != string.Empty || txtCampus3.Text.Trim() != "" || txtCampus3N.Text.Trim() != string.Empty || txtCampus3N.Text.Trim() != "")
            //        {
            //            CheckBoxCount = 1;
            //            WD3 = lblWD3.ToolTip == string.Empty ? "0" : lblWD3.ToolTip;
            //            Campus3 = txtCampus3.Text.Trim() == string.Empty ? "0" : txtCampus3.Text;
            //            WO3 = lblWE3.ToolTip == string.Empty ? "0" : lblWE3.ToolTip;
            //            Campus3N = txtCampus3N.Text.Trim() == string.Empty ? "0" : txtCampus3N.Text;

            //            if (txtCampus3N.Text.Trim() != string.Empty || txtCampus3N.Text.Trim() != "")
            //            {
            //                WO3 = lblWE3.ToolTip == string.Empty ? "0" : lblWE3.ToolTip;
            //                Campus3N = txtCampus3N.Text.Trim() == string.Empty ? "0" : txtCampus3N.Text;
            //            }
            //            else
            //            {
            //                WO3 = "0";
            //                Campus3N = "0";
            //            }

            //            if (txtCampus3.Text.Trim() != string.Empty || txtCampus3.Text.Trim() != "")
            //            {
            //                WD3 = lblWD3.ToolTip == string.Empty ? "0" : lblWD3.ToolTip;
            //                Campus3 = txtCampus3.Text.Trim() == string.Empty ? "0" : txtCampus3.Text;
            //            }
            //            else
            //            {
            //                WD3 = "0";
            //                Campus3 = "0";
            //            }
            //        }
            //        else
            //        {
            //            WD3 = "0";
            //            Campus3 = "0";
            //            WO3 = "0";
            //            Campus3N = "0";
            //        }


            //        //4   

            //        lblWD4 = item.FindControl("lblWD4") as Label;
            //        lblWE4 = item.FindControl("lblWE4") as Label;
            //        txtCampus4 = item.FindControl("txtCampus4") as TextBox;
            //        txtCampus4N = item.FindControl("txtCampus4N") as TextBox; // get
            //        CAMPUSNO4 = txtCampus4.ToolTip == string.Empty ? "0" : txtCampus4.ToolTip;

            //        if (txtCampus4.Text.Trim() != string.Empty || txtCampus4.Text.Trim() != "" || txtCampus4N.Text.Trim() != string.Empty || txtCampus4N.Text.Trim() != "")
            //        {
            //            CheckBoxCount = 1;
            //            WD4 = lblWD4.ToolTip == string.Empty ? "0" : lblWD4.ToolTip;
            //            Campus4 = txtCampus4.Text.Trim() == string.Empty ? "0" : txtCampus4.Text;
            //            WO4 = lblWE4.ToolTip == string.Empty ? "0" : lblWE4.ToolTip;
            //            Campus4N = txtCampus4N.Text.Trim() == string.Empty ? "0" : txtCampus4N.Text;

            //            if (txtCampus4N.Text.Trim() != string.Empty || txtCampus4N.Text.Trim() != "")
            //            {
            //                WO4 = lblWE4.ToolTip == string.Empty ? "0" : lblWE4.ToolTip;
            //                Campus4N = txtCampus4N.Text.Trim() == string.Empty ? "0" : txtCampus4N.Text;
            //            }
            //            else
            //            {
            //                WO4 = "0";
            //                Campus4N = "0";
            //            }

            //            if (txtCampus4.Text.Trim() != string.Empty || txtCampus4.Text.Trim() != "")
            //            {
            //                WD4 = lblWD4.ToolTip == string.Empty ? "0" : lblWD4.ToolTip;
            //                Campus4 = txtCampus4.Text.Trim() == string.Empty ? "0" : txtCampus4.Text;
            //            }
            //            else
            //            {
            //                WD4 = "0";
            //                Campus4 = "0";
            //            }
            //        }
            //        else
            //        {
            //            WD4 = "0";
            //            Campus4 = "0";
            //            WO4 = "0";
            //            Campus4N = "0";
            //        }


            //        //5   

            //        lblWD5 = item.FindControl("lblWD5") as Label;
            //        lblWE5 = item.FindControl("lblWE5") as Label;
            //        txtCampus5 = item.FindControl("txtCampus5") as TextBox;
            //        txtCampus5N = item.FindControl("txtCampus5N") as TextBox; // get
            //        CAMPUSNO5 = txtCampus5.ToolTip == string.Empty ? "0" : txtCampus5.ToolTip;

            //        if (txtCampus5.Text.Trim() != string.Empty || txtCampus5.Text.Trim() != "" || txtCampus5N.Text.Trim() != string.Empty || txtCampus5N.Text.Trim() != "")
            //        {
            //            CheckBoxCount = 1;
            //            WD5 = lblWD5.ToolTip == string.Empty ? "0" : lblWD5.ToolTip;
            //            Campus5 = txtCampus5.Text.Trim() == string.Empty ? "0" : txtCampus5.Text;
            //            WO5 = lblWE5.ToolTip == string.Empty ? "0" : lblWE5.ToolTip;
            //            Campus5N = txtCampus5N.Text.Trim() == string.Empty ? "0" : txtCampus5N.Text;

            //            if (txtCampus5N.Text.Trim() != string.Empty || txtCampus5N.Text.Trim() != "")
            //            {
            //                WO5 = lblWE5.ToolTip == string.Empty ? "0" : lblWE5.ToolTip;
            //                Campus5N = txtCampus5N.Text.Trim() == string.Empty ? "0" : txtCampus5N.Text;
            //            }
            //            else
            //            {
            //                WO5 = "0";
            //                Campus5N = "0";
            //            }

            //            if (txtCampus5.Text.Trim() != string.Empty || txtCampus5.Text.Trim() != "")
            //            {
            //                WD5 = lblWD5.ToolTip == string.Empty ? "0" : lblWD5.ToolTip;
            //                Campus5 = txtCampus5.Text.Trim() == string.Empty ? "0" : txtCampus5.Text;
            //            }
            //            else
            //            {
            //                WD5 = "0";
            //                Campus5 = "0";
            //            }
            //        }
            //        else
            //        {
            //            WD5 = "0";
            //            Campus5 = "0";
            //            WO5 = "0";
            //            Campus5N = "0";
            //        }


            //        //6  

            //        lblWD6 = item.FindControl("lblWD6") as Label;
            //        lblWE6 = item.FindControl("lblWE6") as Label;
            //        txtCampus6 = item.FindControl("txtCampus6") as TextBox;
            //        txtCampus6N = item.FindControl("txtCampus6N") as TextBox; // get
            //        CAMPUSNO6 = txtCampus6.ToolTip == string.Empty ? "0" : txtCampus6.ToolTip;

            //        if (txtCampus6.Text.Trim() != string.Empty || txtCampus6.Text.Trim() != "" || txtCampus6N.Text.Trim() != string.Empty || txtCampus6N.Text.Trim() != "")
            //        {
            //            CheckBoxCount = 1;
            //            WD6 = lblWD6.ToolTip == string.Empty ? "0" : lblWD6.ToolTip;
            //            Campus6 = txtCampus6.Text.Trim() == string.Empty ? "0" : txtCampus6.Text;
            //            WO6 = lblWE6.ToolTip == string.Empty ? "0" : lblWE6.ToolTip;
            //            Campus6N = txtCampus6N.Text.Trim() == string.Empty ? "0" : txtCampus6N.Text;

            //            if (txtCampus6N.Text.Trim() != string.Empty || txtCampus6N.Text.Trim() != "")
            //            {
            //                WO6 = lblWE6.ToolTip == string.Empty ? "0" : lblWE6.ToolTip;
            //                Campus6N = txtCampus6N.Text.Trim() == string.Empty ? "0" : txtCampus6N.Text;
            //            }
            //            else
            //            {
            //                WO6 = "0";
            //                Campus6N = "0";
            //            }

            //            if (txtCampus6.Text.Trim() != string.Empty || txtCampus6.Text.Trim() != "")
            //            {
            //                WD6 = lblWD6.ToolTip == string.Empty ? "0" : lblWD6.ToolTip;
            //                Campus6 = txtCampus6.Text.Trim() == string.Empty ? "0" : txtCampus6.Text;
            //            }
            //            else
            //            {
            //                WD6 = "0";
            //                Campus6 = "0";
            //            }
            //        }
            //        else
            //        {
            //            WD6 = "0";
            //            Campus6 = "0";
            //            WO6 = "0";
            //            Campus6N = "0";
            //        }


            //        //7  

            //        lblWD7 = item.FindControl("lblWD7") as Label;
            //        lblWE7 = item.FindControl("lblWE7") as Label;
            //        txtCampus7 = item.FindControl("txtCampus7") as TextBox;
            //        txtCampus7N = item.FindControl("txtCampus7N") as TextBox; // get
            //        CAMPUSNO7 = txtCampus7.ToolTip == string.Empty ? "0" : txtCampus7.ToolTip;

            //        if (txtCampus7.Text.Trim() != string.Empty || txtCampus7.Text.Trim() != "" || txtCampus7N.Text.Trim() != string.Empty || txtCampus7N.Text.Trim() != "")
            //        {
            //            CheckBoxCount = 1;
            //            WD7 = lblWD7.ToolTip == string.Empty ? "0" : lblWD7.ToolTip;
            //            Campus7 = txtCampus7.Text.Trim() == string.Empty ? "0" : txtCampus7.Text;
            //            WO7 = lblWE7.ToolTip == string.Empty ? "0" : lblWE7.ToolTip;
            //            Campus7N = txtCampus7N.Text.Trim() == string.Empty ? "0" : txtCampus7N.Text;

            //            if (txtCampus7N.Text.Trim() != string.Empty || txtCampus7N.Text.Trim() != "")
            //            {
            //                WO7 = lblWE7.ToolTip == string.Empty ? "0" : lblWE7.ToolTip;
            //                Campus7N = txtCampus7N.Text.Trim() == string.Empty ? "0" : txtCampus7N.Text;
            //            }
            //            else
            //            {
            //                WO7 = "0";
            //                Campus7N = "0";
            //            }

            //            if (txtCampus7.Text.Trim() != string.Empty || txtCampus7.Text.Trim() != "")
            //            {
            //                WD7 = lblWD7.ToolTip == string.Empty ? "0" : lblWD7.ToolTip;
            //                Campus7 = txtCampus7.Text.Trim() == string.Empty ? "0" : txtCampus7.Text;
            //            }
            //            else
            //            {
            //                WD7 = "0";
            //                Campus7 = "0";
            //            }
            //        }
            //        else
            //        {
            //            WD7 = "0";
            //            Campus7 = "0";
            //            WO7 = "0";
            //            Campus7N = "0";
            //        }


            //        //8  

            //        lblWD8 = item.FindControl("lblWD8") as Label;
            //        lblWE8 = item.FindControl("lblWE8") as Label;
            //        txtCampus8 = item.FindControl("txtCampus8") as TextBox;
            //        txtCampus8N = item.FindControl("txtCampus8N") as TextBox; // get
            //        CAMPUSNO8 = txtCampus8.ToolTip == string.Empty ? "0" : txtCampus8.ToolTip;

            //        if (txtCampus8.Text.Trim() != string.Empty || txtCampus8.Text.Trim() != "" || txtCampus8N.Text.Trim() != string.Empty || txtCampus8N.Text.Trim() != "")
            //        {
            //            CheckBoxCount = 1;
            //            WD8 = lblWD8.ToolTip == string.Empty ? "0" : lblWD8.ToolTip;
            //            Campus8 = txtCampus8.Text.Trim() == string.Empty ? "0" : txtCampus8.Text;
            //            WO8 = lblWE8.ToolTip == string.Empty ? "0" : lblWE8.ToolTip;
            //            Campus8N = txtCampus8N.Text.Trim() == string.Empty ? "0" : txtCampus8N.Text;

            //            if (txtCampus8N.Text.Trim() != string.Empty || txtCampus8N.Text.Trim() != "")
            //            {
            //                WO8 = lblWE8.ToolTip == string.Empty ? "0" : lblWE8.ToolTip;
            //                Campus8N = txtCampus8N.Text.Trim() == string.Empty ? "0" : txtCampus8N.Text;
            //            }
            //            else
            //            {
            //                WO8 = "0";
            //                Campus8N = "0";
            //            }

            //            if (txtCampus8.Text.Trim() != string.Empty || txtCampus8.Text.Trim() != "")
            //            {
            //                WD8 = lblWD8.ToolTip == string.Empty ? "0" : lblWD8.ToolTip;
            //                Campus8 = txtCampus8.Text.Trim() == string.Empty ? "0" : txtCampus8.Text;
            //            }
            //            else
            //            {
            //                WD8 = "0";
            //                Campus8 = "0";
            //            }
            //        }
            //        else
            //        {
            //            WD8 = "0";
            //            Campus8 = "0";
            //            WO8 = "0";
            //            Campus8N = "0";
            //        }


            //        //9  

            //        lblWD9 = item.FindControl("lblWD9") as Label;
            //        lblWE9 = item.FindControl("lblWE9") as Label;
            //        txtCampus9 = item.FindControl("txtCampus9") as TextBox;
            //        txtCampus9N = item.FindControl("txtCampus9N") as TextBox; // get
            //        CAMPUSNO9 = txtCampus9.ToolTip == string.Empty ? "0" : txtCampus9.ToolTip;

            //        if (txtCampus9.Text.Trim() != string.Empty || txtCampus9.Text.Trim() != "" || txtCampus9N.Text.Trim() != string.Empty || txtCampus9N.Text.Trim() != "")
            //        {
            //            CheckBoxCount = 1;
            //            WD9 = lblWD9.ToolTip == string.Empty ? "0" : lblWD9.ToolTip;
            //            Campus9 = txtCampus9.Text.Trim() == string.Empty ? "0" : txtCampus9.Text;
            //            WO9 = lblWE9.ToolTip == string.Empty ? "0" : lblWE9.ToolTip;
            //            Campus9N = txtCampus9N.Text.Trim() == string.Empty ? "0" : txtCampus9N.Text;

            //            if (txtCampus9N.Text.Trim() != string.Empty || txtCampus9N.Text.Trim() != "")
            //            {
            //                WO9 = lblWE9.ToolTip == string.Empty ? "0" : lblWE9.ToolTip;
            //                Campus9N = txtCampus9N.Text.Trim() == string.Empty ? "0" : txtCampus9N.Text;
            //            }
            //            else
            //            {
            //                WO9 = "0";
            //                Campus9N = "0";
            //            }

            //            if (txtCampus9.Text.Trim() != string.Empty || txtCampus9.Text.Trim() != "")
            //            {
            //                WD9 = lblWD9.ToolTip == string.Empty ? "0" : lblWD9.ToolTip;
            //                Campus9 = txtCampus9.Text.Trim() == string.Empty ? "0" : txtCampus9.Text;
            //            }
            //            else
            //            {
            //                WD9 = "0";
            //                Campus9 = "0";
            //            }
            //        }
            //        else
            //        {
            //            WD9 = "0";
            //            Campus9 = "0";
            //            WO9 = "0";
            //            Campus9N = "0";
            //        }


            //        //10 

            //        lblWD10 = item.FindControl("lblWD10") as Label;
            //        lblWE10 = item.FindControl("lblWE10") as Label;
            //        txtCampus10 = item.FindControl("txtCampus10") as TextBox;
            //        txtCampus10N = item.FindControl("txtCampus10N") as TextBox; // get
            //        CAMPUSNO10 = txtCampus10.ToolTip == string.Empty ? "0" : txtCampus10.ToolTip;

            //        if (txtCampus10.Text.Trim() != string.Empty || txtCampus10.Text.Trim() != "" || txtCampus10N.Text.Trim() != string.Empty || txtCampus10N.Text.Trim() != "")
            //        {
            //            CheckBoxCount = 1;
            //            WD10 = lblWD10.ToolTip == string.Empty ? "0" : lblWD10.ToolTip;
            //            Campus10 = txtCampus10.Text.Trim() == string.Empty ? "0" : txtCampus10.Text;
            //            WO10 = lblWE10.ToolTip == string.Empty ? "0" : lblWE10.ToolTip;
            //            Campus10N = txtCampus10N.Text.Trim() == string.Empty ? "0" : txtCampus10N.Text;

            //            if (txtCampus10N.Text.Trim() != string.Empty || txtCampus10N.Text.Trim() != "")
            //            {
            //                WO10 = lblWE10.ToolTip == string.Empty ? "0" : lblWE10.ToolTip;
            //                Campus10N = txtCampus10N.Text.Trim() == string.Empty ? "0" : txtCampus10N.Text;
            //            }
            //            else
            //            {
            //                WO10 = "0";
            //                Campus10N = "0";
            //            }

            //            if (txtCampus10.Text.Trim() != string.Empty || txtCampus10.Text.Trim() != "")
            //            {
            //                WD10 = lblWD10.ToolTip == string.Empty ? "0" : lblWD10.ToolTip;
            //                Campus10 = txtCampus10.Text.Trim() == string.Empty ? "0" : txtCampus10.Text;
            //            }
            //            else
            //            {
            //                WD10 = "0";
            //                Campus10 = "0";
            //            }
            //        }
            //        else
            //        {
            //            WD10 = "0";
            //            Campus10 = "0";
            //            WO10 = "0";
            //            Campus10N = "0";
            //        }



                   
                    //ck = objmp.AddCampusIntakeOfferCopy(Convert.ToInt32(ddlCollegeSchNameC.SelectedValue), Convert.ToInt32(ddlToIntake.SelectedValue), Convert.ToInt32(ddlStudyLevelC.SelectedValue), Convert.ToInt32(Degreeno), Convert.ToInt32(Branchno), 1, Convert.ToInt32(WD1), Convert.ToInt32(WD2), Convert.ToInt32(WD3), Convert.ToInt32(WD4), Convert.ToInt32(WD5), Convert.ToInt32(WD6), Convert.ToInt32(WD7), Convert.ToInt32(WD8), Convert.ToInt32(WD9), Convert.ToInt32(WD10), Convert.ToInt32(Campus1), Convert.ToInt32(Campus2), Convert.ToInt32(Campus3), Convert.ToInt32(Campus4), Convert.ToInt32(Campus5), Convert.ToInt32(Campus6), Convert.ToInt32(Campus7), Convert.ToInt32(Campus8), Convert.ToInt32(Campus9), Convert.ToInt32(Campus10), Convert.ToInt32(CAMPUSNO1), Convert.ToInt32(CAMPUSNO2), Convert.ToInt32(CAMPUSNO3), Convert.ToInt32(CAMPUSNO4), Convert.ToInt32(CAMPUSNO5), Convert.ToInt32(CAMPUSNO6), Convert.ToInt32(CAMPUSNO7), Convert.ToInt32(CAMPUSNO8), Convert.ToInt32(CAMPUSNO9), Convert.ToInt32(CAMPUSNO10), Convert.ToInt32(WO1), Convert.ToInt32(WO2), Convert.ToInt32(WO3), Convert.ToInt32(WO4), Convert.ToInt32(WO5), Convert.ToInt32(WO6), Convert.ToInt32(WO7), Convert.ToInt32(WO8), Convert.ToInt32(WO9), Convert.ToInt32(WO10), Convert.ToInt32(Campus1N), Convert.ToInt32(Campus2N), Convert.ToInt32(Campus3N), Convert.ToInt32(Campus4N), Convert.ToInt32(Campus5N), Convert.ToInt32(Campus6N), Convert.ToInt32(Campus7N), Convert.ToInt32(Campus8N), Convert.ToInt32(Campus9N), Convert.ToInt32(Campus10N), Convert.ToInt32(Session["userno"].ToString()));

                //    if (CheckBoxCount > 0)
                //    {

                //        if (ck == 1)
                //        {
                //            SaveCount++;
                //        }
                //        else
                //        {
                //            objCommon.DisplayMessage(this.updGradeEntry, "Few or All Record already Exist", this.Page);
                //            return;
                //        }
                //    }
                //}
            //} //End of chkOfferCopy

            //    if (SaveCount > 0)
            //    {
            //        objCommon.DisplayMessage(this.updGradeEntry, "Record Save Successfully", this.Page);
            //    }
            //    else
            //    {
            //        objCommon.DisplayMessage(this.updGradeEntry, "Please Select Campus Intake For Copy !", this.Page);
            //        return;
            //    }



            
              //  ShowDetails(Convert.ToInt32(ddlCollegeSchNameC.SelectedValue), Convert.ToInt32(ddlProgClassification.SelectedValue), Convert.ToInt32(ddlIntakeC.SelectedValue), Convert.ToInt32(ddlClassification.SelectedValue));
            
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Master_CollegeMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void lvCampusWiseIntake_ItemDataBound1(object sender, ListViewItemEventArgs e)
    {
        if ((e.Item.ItemType == ListViewItemType.DataItem))
        {
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;
            DataRow dr = ((DataRowView)dataItem.DataItem).Row;
            string camp1 = dr["CAMPUSNO1"].ToString();
            string camp2 = dr["CAMPUSNO2"].ToString();
            string camp3 = dr["CAMPUSNO3"].ToString();
            string camp4 = dr["CAMPUSNO4"].ToString();
            string camp5 = dr["CAMPUSNO5"].ToString();
            string camp6 = dr["CAMPUSNO6"].ToString();
            string camp7 = dr["CAMPUSNO7"].ToString();
            string camp8 = dr["CAMPUSNO8"].ToString();
            string camp9 = dr["CAMPUSNO9"].ToString();
            string camp10 = dr["CAMPUSNO10"].ToString();

            string Script = "";
            Script += "var arrOfHiddenColumns = [];";

            if (camp1 == "0")
            {
                Script += "$('#myTable th:nth-child(2)').hide();$('#myTable td:nth-child(2)').hide();";
                Script += "arrOfHiddenColumns.push(1);";
            }
            if (camp2 == "0")
            {
                Script += "$('#myTable th:nth-child(3)').hide();$('#myTable td:nth-child(3)').hide();";
                Script += "arrOfHiddenColumns.push(2);";
            }
            if (camp3 == "0")
            {
                Script += "$('#myTable th:nth-child(4)').hide();$('#myTable td:nth-child(4)').hide();";
                Script += "arrOfHiddenColumns.push(3);";
            }
            if (camp4 == "0")
            {
                Script += "$('#myTable th:nth-child(5)').hide();$('#myTable td:nth-child(5)').hide();";
                Script += "arrOfHiddenColumns.push(4);";
            }
            if (camp5 == "0")
            {
                Script += "$('#myTable th:nth-child(6)').hide();$('#myTable td:nth-child(6)').hide();";
                Script += "arrOfHiddenColumns.push(5);";
            }
            if (camp6 == "0")
            {
                Script += "$('#myTable th:nth-child(7)').hide();$('#myTable td:nth-child(7)').hide();";
                Script += "arrOfHiddenColumns.push(6);";
            }
            if (camp7 == "0")
            {
                Script += "$('#myTable th:nth-child(8)').hide();$('#myTable td:nth-child(8)').hide();";
                Script += "arrOfHiddenColumns.push(7);";
            }
            if (camp8 == "0")
            {
                Script += "$('#myTable th:nth-child(9)').hide();$('#myTable td:nth-child(9)').hide();";
                Script += "arrOfHiddenColumns.push(8);";
            }
            if (camp9 == "0")
            {
                Script += "$('#myTable th:nth-child(10)').hide();$('#myTable td:nth-child(10)').hide();";
                Script += "arrOfHiddenColumns.push(9);";
            }
            if (camp10 == "0")
            {
                Script += "$('#myTable th:nth-child(11)').hide();$('#myTable td:nth-child(11)').hide();";
                Script += "arrOfHiddenColumns.push(10);";

            }

           ViewState["ScriptTbl"] = Script;
           ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Src", Script, true);

           //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Src", Script, true);
        }
    }


    protected void lvOfferCopy_ItemDataBound1(object sender, ListViewItemEventArgs e)
    {
        if ((e.Item.ItemType == ListViewItemType.DataItem))
        {
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;
            DataRow dr = ((DataRowView)dataItem.DataItem).Row;
            string camp1 = dr["CAMPUSNO1"].ToString();
            string camp2 = dr["CAMPUSNO2"].ToString();
            string camp3 = dr["CAMPUSNO3"].ToString();
            string camp4 = dr["CAMPUSNO4"].ToString();
            string camp5 = dr["CAMPUSNO5"].ToString();
            string camp6 = dr["CAMPUSNO6"].ToString();
            string camp7 = dr["CAMPUSNO7"].ToString();
            string camp8 = dr["CAMPUSNO8"].ToString();
            string camp9 = dr["CAMPUSNO9"].ToString();
            string camp10 = dr["CAMPUSNO10"].ToString();

            string Script = "";
            Script += "var arrOfHiddenColumns = [];";

            if (camp1 == "0")
            {
                Script += "$('#myTable2 th:nth-child(3)').hide();$('#myTable2 td:nth-child(2)').hide();";
                Script += "arrOfHiddenColumns.push(2);";
            }
            if (camp2 == "0")
            {
                Script += "$('#myTable2 th:nth-child(4)').hide();$('#myTable2 td:nth-child(3)').hide();";
                Script += "arrOfHiddenColumns.push(3);";
            }
            if (camp3 == "0")
            {
                Script += "$('#myTable2 th:nth-child(5)').hide();$('#myTable2 td:nth-child(4)').hide();";
                Script += "arrOfHiddenColumns.push(4);";
            }
            if (camp4 == "0")
            {
                Script += "$('#myTable2 th:nth-child(6)').hide();$('#myTable2 td:nth-child(5)').hide();";
                Script += "arrOfHiddenColumns.push(5);";
            }
            if (camp5 == "0")
            {
                Script += "$('#myTable2 th:nth-child(7)').hide();$('#myTable2 td:nth-child(6)').hide();";
                Script += "arrOfHiddenColumns.push(6);";
            }
            if (camp6 == "0")
            {
                Script += "$('#myTable2 th:nth-child(8)').hide();$('#myTable2 td:nth-child(7)').hide();";
                Script += "arrOfHiddenColumns.push(7);";
            }
            if (camp7 == "0")
            {
                Script += "$('#myTable2 th:nth-child(9)').hide();$('#myTable2 td:nth-child(9)').hide();";
                Script += "arrOfHiddenColumns.push(8);";
            }
            if (camp8 == "0")
            {
                Script += "$('#myTable2 th:nth-child(10)').hide();$('#myTable2 td:nth-child(10)').hide();";
                Script += "arrOfHiddenColumns.push(9);";
            }
            if (camp9 == "0")
            {
                Script += "$('#myTable2 th:nth-child(11)').hide();$('#myTable2 td:nth-child(11)').hide();";
                Script += "arrOfHiddenColumns.push(10);";
            }
            if (camp10 == "0")
            {
                Script += "$('#myTable2 th:nth-child(12)').hide();$('#myTable2 td:nth-child(12)').hide();";
                Script += "arrOfHiddenColumns.push(11);";
            }

            ViewState["ScriptTbl"] = Script;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Src", Script, true);
        }
    }

    public void clearTabData()

    {
        ddlColg.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        ddlProgClassification.SelectedIndex = 0;
        ddlClassification.SelectedIndex = 0;

        lvCampusWiseIntake.DataSource = null;
        lvCampusWiseIntake.DataBind();
        btnCancel.Visible = false;
        btnSave.Visible = false;

    }

    public void ClearOfferCopy()
    {

        ddlCollegeSchNameC.SelectedIndex = 0;
        ddlIntakeC.SelectedIndex = 0;
        ddlStudyLevelC.SelectedIndex = 0;
        ddlClassificationC.SelectedIndex = 0;
        ddlToIntake.Items.Clear();

        lvOfferCopy.DataSource = null;
        lvOfferCopy.DataBind();
        btnCancelCopy.Visible = false;
        lnkSubmit.Visible = false;
      
    }


    protected void lkAnnouncement_Click(object sender, EventArgs e)
    {
        divReports.Visible = false;
        divAnnounce.Visible = true;
        divlkReports.Attributes.Remove("class");
        divlkAnnouncement.Attributes.Add("class", "active");
        //lvShowReport.Visible = false;

        clearTabData();
        ClearOfferCopy();
        ViewState["ScriptTbl"] = null;
    }


    protected void lkReports_Click(object sender, EventArgs e)
    {
        divAnnounce.Visible = false;
        divReports.Visible = true;
        divlkAnnouncement.Attributes.Remove("class");
        divlkReports.Attributes.Add("class", "active");

        clearTabData();
        ClearOfferCopy();
        ViewState["ScriptTbl"] =null;
    }

    protected void btnCancelCopy_Click(object sender,EventArgs e)
    {
        ClearOfferCopy();
    }
}
