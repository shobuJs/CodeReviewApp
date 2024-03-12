//======================================================================================
// PROJECT NAME  : RFC SVCE                                                          
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : Holiday Master                                                  
// CREATION DATE : 10/8/2019                                                       
// CREATED BY    : NEHA BARANWAL                                                     
// MODIFIED DATE : 28-Nov-19                                                                    
// MODIFIED BY   : NEHA BARANWAL                                                                      
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_HolidayMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();

    #region PageLoad
    //USED FOR INITIALSING THE MASTER PAGE
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    //USED FOR BYDEFAULT LOADING THE default PAGE
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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                PopulateDropDown();
            }
            //Bind all Holidays List
            BindListView();
        }
    }

    //bind SESSION_PNAME in drop down list
    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND EXAMTYPE=1", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlSlotType, "ACD_SLOTTYPE", "SLOTTYPENO", "SLOTTYPE_NAME", "SLOTTYPENO > 0", "SLOTTYPENO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "SessionCreate.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page.z 
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=HolidayMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HolidayMaster.aspx");
        }
    }

    //bind holiday in list view
    private void BindListView()
    {
        try
        {
            DataSet ds = objAttC.GetHoliday();
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnllvHday.Visible = true;
                lvHday.DataSource = ds;
                lvHday.DataBind();
                DateTime Today = DateTime.Today;
                foreach (ListViewDataItem lvitem1 in lvHday.Items)
                {
                    ImageButton btnhd = lvitem1.FindControl("btnEdit") as ImageButton;
                    Label lblhd = lvitem1.FindControl("lblhdate") as Label;
                    DateTime dthddate = Convert.ToDateTime(lblhd.ToolTip.ToString());
                    if (dthddate < Today)
                    { 
                        btnhd.Enabled = false;
                        btnhd.ImageUrl = "~/images/check1.jpg";
                        btnhd.Width=20;
                        btnhd.Height = 20;
                    }
                    else 
                    { 
                        btnhd.Enabled = true;
                        btnhd.ImageUrl = "~/images/edit.gif";
                    }

                }
            }
            else
            {
                pnllvHday.Visible = false;
                lvHday.DataSource = null;
                lvHday.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_HolidayMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    //bind holiday as per as selection of drop down list
    private void BindFilterListView()
    {
        try
        {
            DataSet ds = objAttC.GetFilterWiseHolidayMaster(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlBatches.SelectedValue), Convert.ToInt32(ddlChangeTimetableDays.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnllvHday.Visible = true;
                lvHday.DataSource = ds;
                lvHday.DataBind();

                DateTime Today = DateTime.Today;
                foreach (ListViewDataItem lvitem1 in lvHday.Items)
                {
                    ImageButton btnhd = lvitem1.FindControl("btnEdit") as ImageButton;
                    Label lblhd = lvitem1.FindControl("lblhdate") as Label;
                    DateTime dthddate = Convert.ToDateTime(lblhd.ToolTip.ToString());
                    if (dthddate < Today)
                    {
                        btnhd.Enabled = false;
                        btnhd.ImageUrl = "~/images/check1.jpg";
                        btnhd.Width = 20;
                        btnhd.Height = 20;
                    }
                    else
                    {
                        btnhd.Enabled = true;
                        btnhd.ImageUrl = "~/images/edit.gif";
                    }

                }
            }
            else
            {
                pnllvHday.Visible = false;
                lvHday.DataSource = null;
                lvHday.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HolidayMaster.BindFilterListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    #endregion PageLoad

    #region "SelectionIndexChanged"
    //On select of Session bind DEGREE NAME in drop down list
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            ddlSubjectType.SelectedIndex = 0;
            ddlBatches.SelectedIndex = 0;
            ddlSlotType.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            txtHdayDate.Text = "";
            txtHdayName.Text = "";
            ddlChangeTimetableDays.SelectedIndex = 0;
            txtReason.Text = "";
            chkSelectdSlots.ClearSelection();
            chkSelectdSlots.Items.Clear();
            LoadSlots();
            BindFilterListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HolidayMaster.ddlSession_SelectedIndexChanged-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    //On select of Degree bind Branch name in drop down list
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON B.BRANCHNO=A.BRANCHNO", "B.BRANCHNO", "LONGNAME", "B.BRANCHNO<>99 AND DEGREENO = " + ddlDegree.SelectedValue, "B.BRANCHNO");
                //on faculty login to get only those dept which is related to logged in faculty
                //if (Convert.ToInt32(Session["BRANCH_FILTER"]) == 1)
                //{
                // objCommon.FilterDataByBranch(ddlBranch, Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userdeptno"]), Convert.ToInt32(Session["BRANCH_FILTER"]));
                //}
                //bind Branch NO in drop down list
                DataSet ds = objCommon.FillDropDown("ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
                string BranchNos = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    BranchNos += ds.Tables[0].Rows[i]["BranchNo"].ToString() + ",";
                }
                DataSet dsReff = objCommon.FillDropDown("REFF", "*", "", string.Empty, string.Empty);
                //on faculty login to get only those dept which is related to logged in faculty
                objCommon.FilterDataByBranch(ddlBranch, dsReff.Tables[0].Rows[0]["FILTER_USER_TYPE"].ToString(), BranchNos, Convert.ToInt32(dsReff.Tables[0].Rows[0]["BRANCH_FILTER"].ToString()), Convert.ToInt32(Session["usertype"]));
                ddlBranch.Focus();
            }
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.Focus();
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            ddlSubjectType.SelectedIndex = 0;
            ddlBatches.SelectedIndex = 0;
            ddlSlotType.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            txtHdayDate.Text = "";
            txtHdayName.Text = "";
            ddlChangeTimetableDays.SelectedIndex = 0;
            txtReason.Text = "";
            chkSelectdSlots.ClearSelection();
            chkSelectdSlots.Items.Clear();
            LoadSlots();
            BindFilterListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HolidayMaster.ddlDegree_SelectedIndexChanged-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    ////On select of Branch bind Scheme name in drop down list
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                ddlScheme.Items.Clear();
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");
                ddlScheme.Focus();
                ddlSem.Items.Add(new ListItem("Please Select", "0"));
                if (ddlBranch.SelectedIndex > 0)
                {
                    this.FillDatesDropDown(ddlSem, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
                }
                ddlScheme.SelectedIndex = 0;
                ddlSem.SelectedIndex = 0;
                ddlSection.SelectedIndex = 0;
                ddlSubjectType.SelectedIndex = 0;
                ddlBatches.SelectedIndex = 0;
                ddlExistingDates.SelectedIndex = 0;
                ddlSlotType.SelectedIndex = 0;
            }
            else
            {
                ddlSem.Items.Clear();
                ddlSem.Items.Add(new ListItem("Please Select", "0"));
            }
            ddlScheme.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            ddlSubjectType.SelectedIndex = 0;
            ddlBatches.SelectedIndex = 0;
            txtHdayDate.Text = "";
            txtHdayName.Text = "";
            ddlSlotType.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            ddlChangeTimetableDays.SelectedIndex = 0;
            txtReason.Text = "";
            chkSelectdSlots.ClearSelection();
            chkSelectdSlots.Items.Clear();
            LoadSlots();
            BindFilterListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HolidayMaster.ddlBranch_SelectedIndexChanged-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    ////On select of Scheme bind filterd Holidays in list view
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            ddlSubjectType.SelectedIndex = 0;
            ddlBatches.SelectedIndex = 0;
            txtHdayDate.Text = "";
            txtHdayName.Text = "";
            ddlSlotType.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            ddlChangeTimetableDays.SelectedIndex = 0;
            txtReason.Text = "";
            chkSelectdSlots.ClearSelection();
            chkSelectdSlots.Items.Clear();
            LoadSlots();
            BindFilterListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HolidayMaster.ddlScheme_SelectedIndexChanged-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    //bind Semester name in drop down list and Holidays list bind in list view
    private void FillDatesDropDown(DropDownList ddlsemester, int sessionno, int degree)
    {
        try
        {
            DataSet ds = objAttC.GetSemesterDurationwise(sessionno, degree);
            ddlsemester.Items.Clear();
            ddlsemester.Items.Add("Please Select");
            ddlsemester.SelectedItem.Value = "0";
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlsemester.DataSource = ds;
                ddlsemester.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlsemester.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlsemester.DataBind();
                ddlsemester.SelectedIndex = 0;
            }
            txtHdayDate.Text = "";
            txtHdayName.Text = "";
            ddlSlotType.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            ddlChangeTimetableDays.SelectedIndex = 0;
            txtReason.Text = "";
            chkSelectdSlots.DataSource = null;
            chkSelectdSlots.DataBind();
            LoadSlots();
            BindFilterListView();
        }
        catch { }
    }
    //On select of Semester bind SubjectType  name in drop down list
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSem.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSubjectType, "ACD_OFFERED_COURSE OC INNER JOIN ACD_COURSE C ON OC.COURSENO=C.COURSENO INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SUBID<>9 and OC.SCHEMENO = " + ddlScheme.SelectedValue + " AND OC.SEMESTERNO = " + ddlSem.SelectedValue, "C.SUBID");
            }

            ddlSection.SelectedIndex = 0;
            ddlSubjectType.SelectedIndex = 0;
            ddlBatches.SelectedIndex = 0;
            txtHdayDate.Text = "";
            txtHdayName.Text = "";
            ddlSlotType.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            ddlChangeTimetableDays.SelectedIndex = 0;
            txtReason.Text = "";
            chkSelectdSlots.ClearSelection();
            chkSelectdSlots.Items.Clear();
            LoadSlots();
            BindFilterListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HolidayMaster.ddlSem_SelectedIndexChanged-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    ////On select of Subject Type bind Section name in drop down list
    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubjectType.SelectedIndex > 0)
            {
                if (ddlSem.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + ddlScheme.SelectedValue + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SECTIONNO > 0", "SC.SECTIONNAME");
                }
                if (Convert.ToInt32(ddlSubjectType.SelectedValue) == 1)
                {
                    Batch.Visible = false;
                }
                else
                {
                    Batch.Visible = true;
                }
                ddlSection.SelectedIndex = 0;
                ddlBatches.SelectedIndex = 0;
                txtHdayDate.Text = "";
                txtHdayName.Text = "";
                ddlSlotType.SelectedIndex = 0;
                ddlExistingDates.SelectedIndex = 0;
                ddlChangeTimetableDays.SelectedIndex = 0;
                txtReason.Text = "";
                chkSelectdSlots.ClearSelection();
                chkSelectdSlots.Items.Clear();
                LoadSlots();

                BindFilterListView();
            }
            else
            {
                if (ddlSem.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + ddlScheme.SelectedValue + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SECTIONNO > 0", "SC.SECTIONNAME");
                }
                Batch.Visible = false;
                ddlSection.SelectedIndex = 0;
                ddlBatches.SelectedIndex = 0;
                txtHdayDate.Text = "";
                txtHdayName.Text = "";
                ddlSlotType.SelectedIndex = 0;
                ddlExistingDates.SelectedIndex = 0;
                ddlChangeTimetableDays.SelectedIndex = 0;
                txtReason.Text = "";
                chkSelectdSlots.ClearSelection();
                chkSelectdSlots.Items.Clear();
                LoadSlots();
                BindFilterListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HolidayMaster.ddlSubjectType_SelectedIndexChanged-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }

        ddlSection.SelectedIndex = 0;
        txtHdayDate.Text = "";
        txtHdayName.Text = "";
        ddlSlotType.SelectedIndex = 0;
        ddlExistingDates.SelectedIndex = 0;
        ddlChangeTimetableDays.SelectedIndex = 0;
        txtReason.Text = "";
        chkSelectdSlots.ClearSelection();
        chkSelectdSlots.Items.Clear();
        LoadSlots();
    }
    //On select of Section bind Batches name in drop down list
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSection.SelectedIndex > 0)
            {
                LoadExisitingDates();
                //ddlBatchesAT.Items.Clear();
                ddlBatches.DataSource = null;
                ddlBatches.DataBind();
                ddlBatches.SelectedIndex = -1;
                objCommon.FillDropDownList(ddlBatches, "ACD_BATCH B LEFT JOIN ACD_COURSE_TEACHER T on (T.BATCHNO=B.BATCHNO)", "distinct T.BATCHNO", "B.BATCHNAME", " B.BATCHNO>0 and T.SESSIONNO = " + ddlSession.SelectedValue + " AND T.SCHEMENO = " + ddlScheme.SelectedValue + " AND T.SEMESTERNO  = " + ddlSem.SelectedValue + " and T.SectionNo=" + ddlSection.SelectedValue, "T.BATCHNO");
                ddlBranch.Focus();
                //bind Day name in ddlChangeTimetableDays drop down list
                objCommon.FillDropDownList(ddlChangeTimetableDays, "ACD_DAY_MASTER", "distinct DAY_NO", "DAY_NAME", "", "DAY_NO");

                txtHdayDate.Text = "";
                txtHdayName.Text = "";
                ddlSlotType.SelectedIndex = 0;
                ddlExistingDates.SelectedIndex = 0;
                ddlChangeTimetableDays.SelectedIndex = 0;
                txtReason.Text = "";
                chkSelectdSlots.ClearSelection();
                chkSelectdSlots.Items.Clear();
                LoadSlots();
            }
            else
            {
                ddlBatches.DataSource = null;
                ddlBatches.DataBind();
                ddlBatches.SelectedIndex = 0;
                txtHdayDate.Text = "";
                txtHdayName.Text = "";
                ddlSlotType.SelectedIndex = 0;
                ddlExistingDates.SelectedIndex = 0;
                ddlChangeTimetableDays.SelectedIndex = 0;
                txtReason.Text = "";
                chkSelectdSlots.ClearSelection();
                chkSelectdSlots.Items.Clear();
                LoadSlots();
            }
            BindFilterListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HolidayMaster.ddlSection_SelectedIndexChanged-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }

    }
    //on selection of Batches  holiday name , date , Reason will be blank and bind Holiodays list in List view
    protected void ddlBatches_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadExisitingDates();
            txtHdayDate.Text = "";
            txtHdayName.Text = "";
            ddlSlotType.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            ddlChangeTimetableDays.SelectedIndex = 0;
            txtReason.Text = "";
            chkSelectdSlots.ClearSelection();
            chkSelectdSlots.Items.Clear();
            LoadSlots();
            BindFilterListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HolidayMaster.ddlBatches_SelectedIndexChanged-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void ddlSlotType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            txtHdayDate.Text = "";

            ddlChangeTimetableDays.SelectedIndex = 0;
            txtHdayName.Text = "";
            txtReason.Text = "";
            chkSelectdSlots.ClearSelection();
            chkSelectdSlots.Items.Clear();
            LoadSlots();
            BindFilterListView();

            LoadExisitingDates();
            ddlExistingDates.SelectedIndex = 0;
        }
        catch { }
    }

    //to load existing dates
    public void LoadExisitingDates()
    {
        try
        {
            if (ddlSession.SelectedIndex > 0 && ddlScheme.SelectedIndex > 0 && ddlSem.SelectedIndex > 0 && ddlSection.SelectedIndex > 0 && ddlSlotType.SelectedIndex > 0)
            {
                ddlExistingDates.Items.Clear();
                ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
                ddlExistingDates.SelectedIndex = 0;
                DataSet dsGetExisitingDates = objCommon.FillDropDown("ACD_TIME_TABLE_CONFIG TT INNER JOIN  ACD_COURSE_TEACHER CT ON CT.CT_NO=TT.CTNO INNER JOIN ACD_ACADEMIC_TT_SLOT TTS ON TTS.SLOTNO=TT.SLOTNO INNER JOIN ACD_SLOTTYPE ST ON ST.SLOTTYPENO=TTS.SLOTTYPE", "DISTINCT CAST(convert(varchar(10),START_DATE,103) AS NVARCHAR(10))+' - '+CAST(convert(varchar(10),END_DATE,103) AS NVARCHAR(10))  AS EXISTINGDATES ", "START_DATE,END_DATE,MONTH(START_DATE) as STARTDATEMONTH", "ISNULL(TT.CANCEL,0)=0 AND CAST(convert(varchar(10),START_DATE,103) AS NVARCHAR(10))+' - '+CAST(convert(varchar(10),END_DATE,103) AS NVARCHAR(10)) IS NOT NULL AND CT.SESSIONNO=" + ddlSession.SelectedValue + " and SCHEMENO=" + ddlScheme.SelectedValue + " and SEMESTERNO=" + ddlSem.SelectedValue + " and SECTIONNO=" + ddlSection.SelectedValue + "and SLOTTYPE=" + ddlSlotType.SelectedValue, "MONTH(START_DATE) ");
                if (dsGetExisitingDates.Tables[0].Rows.Count > 0)
                {
                    ddlExistingDates.DataSource = dsGetExisitingDates.Tables[0];
                    ddlExistingDates.DataTextField = "EXISTINGDATES";
                    ddlExistingDates.DataBind();
                }
                else
                {
                    ddlExistingDates.DataSource = null;
                    ddlExistingDates.DataBind();
                }
            }
        }
        catch { }
    }

    protected void ddlExistingDates_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlExistingDates.SelectedIndex > 0)
            {
                string myStr = ddlExistingDates.SelectedItem.ToString();
                string[] ssizes = myStr.Split(' ');
                string startdate = ssizes[0].ToString();
                string enddate = ssizes[2].ToString();

                ddlChangeTimetableDays.Items.Clear();
                ddlChangeTimetableDays.Items.Add(new ListItem("Please Select", "0"));
                ddlChangeTimetableDays.SelectedIndex = 0;
                DataSet dsTimeTableDays = objCommon.FillDropDown("ACD_DAY_MASTER D INNER JOIN ACD_TIME_TABLE_CONFIG T ON T.DAYNO=D.DAY_NO INNER JOIN ACD_COURSE_TEACHER C ON C.CT_NO=T.CTNO INNER JOIN ACD_ACADEMIC_TT_SLOT TTS ON TTS.SLOTNO=T.SLOTNO INNER JOIN ACD_SLOTTYPE ST ON ST.SLOTTYPENO=TTS.SLOTTYPE", "DISTINCT DAYNO", "DAY_NAME", "ISNULL(T.CANCEL,0)=0 AND T.TIME_TABLE_DATE BETWEEN CONVERT(DATE,'" + Convert.ToDateTime(startdate).ToString("dd-MM-yyyy") + "',103) AND CONVERT(DATE,'" + Convert.ToDateTime(enddate).ToString("dd-MM-yyyy") + "',103) and C.SESSIONNO=" + ddlSession.SelectedValue + " and SCHEMENO=" + ddlScheme.SelectedValue + " and SEMESTERNO=" + ddlSem.SelectedValue + " and SECTIONNO=" + ddlSection.SelectedValue + " AND  TTS.SLOTTYPE = " + ddlSlotType.SelectedValue, "DAYNO");
                if (dsTimeTableDays.Tables[0].Rows.Count > 0)
                {
                    ddlChangeTimetableDays.DataSource = dsTimeTableDays.Tables[0];
                    ddlChangeTimetableDays.DataTextField = "DAY_NAME";
                    ddlChangeTimetableDays.DataValueField = "DAYNO";
                    ddlChangeTimetableDays.DataBind();
                }
                else
                {
                    ddlChangeTimetableDays.DataSource = null;
                    ddlChangeTimetableDays.DataBind();
                }

                ddlChangeTimetableDays.SelectedIndex = 0;
                txtHdayDate.Text = "";
                txtHdayName.Text = "";
                txtReason.Text = "";
                chkSelectdSlots.ClearSelection();
                chkSelectdSlots.Items.Clear();
                LoadSlots();
                BindFilterListView();
            }
            else
            {
                ddlExistingDates.SelectedIndex = 0;
                ddlChangeTimetableDays.Items.Clear();
                ddlChangeTimetableDays.Items.Add(new ListItem("Please Select", "0"));
                ddlChangeTimetableDays.SelectedIndex = 0;
                ddlChangeTimetableDays.SelectedIndex = 0;
                txtHdayDate.Text = "";
                txtHdayName.Text = "";
                ddlChangeTimetableDays.SelectedIndex = 0;
                txtReason.Text = "";
                chkSelectdSlots.ClearSelection();
                chkSelectdSlots.Items.Clear();
                LoadSlots();
                BindFilterListView();
            }
        }
        catch { }
    }



    //Slot details display like Slot Time i.e. From Time to To Time
    public void LoadSlots()
    {
        try
        {
            if (ddlSession.SelectedIndex > 0 && ddlScheme.SelectedIndex > 0 && ddlSem.SelectedIndex > 0 && ddlSection.SelectedIndex > 0 && ddlSlotType.SelectedIndex > 0 && ddlExistingDates.SelectedIndex > 0 && ddlChangeTimetableDays.SelectedIndex > 0)
            {
                string myStr = ddlExistingDates.SelectedItem.ToString();
                string[] ssizes = myStr.Split(' ');
                string startdate = ssizes[0].ToString();
                string enddate = ssizes[2].ToString();
                DataSet dsSlots = new DataSet();
                chkSelectdSlots.ClearSelection();
                dsSlots = objCommon.FillDropDown("ACD_TIME_TABLE_CONFIG TTC INNER JOIN ACD_COURSE_TEACHER CT ON CT.CT_NO=TTC.CTNO INNER JOIN ACD_ACADEMIC_TT_SLOT TTS ON TTS.SLOTNO=TTC.SLOTNO", "DISTINCT TTC.SLOTNO", "TTS.TIMEFROM+' - '+TTS.TIMETO AS TIMESLOT", "CAST(convert(varchar(10),START_DATE,103) AS NVARCHAR(10))+' - '+CAST(convert(varchar(10),END_DATE,103) AS NVARCHAR(10)) IS NOT NULL AND CT.SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + ddlScheme.SelectedValue + " AND SEMESTERNO=" + ddlSem.SelectedValue + " AND SLOTTYPE=" + ddlSlotType.SelectedValue + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND ( (ISNULL(BATCHNO,0)=" + ddlBatches.SelectedValue + ") OR (" + ddlBatches.SelectedValue + " = 0) ) AND DAYNO=" + ddlChangeTimetableDays.SelectedValue + " AND TTC.TIME_TABLE_DATE BETWEEN CONVERT(DATE,'" + Convert.ToDateTime(startdate).ToString("dd-MM-yyyy") + "',103) AND CONVERT(DATE,'" + Convert.ToDateTime(enddate).ToString("dd-MM-yyyy") + "',103) AND ISNULL(CT.CANCEL,0) = 0 AND ISNULL(TTC.CANCEL,0) = 0", "TTC.SLOTNO");
                if (dsSlots.Tables[0].Rows.Count > 0)
                {
                    chkSelectdSlots.DataSource = dsSlots.Tables[0];
                    chkSelectdSlots.DataValueField = "SLOTNO";
                    chkSelectdSlots.DataTextField = "TIMESLOT";
                    chkSelectdSlots.DataBind();
                }
                else
                {
                    chkSelectdSlots.ClearSelection();
                    chkSelectdSlots.DataSource = dsSlots.Tables[0];
                    chkSelectdSlots.DataBind();

                }
            }
        }
        catch { }

    }


    //on select of time table change day display Slot Details Time wise and Bind Holidays details in list view.
    protected void ddlChangeTimetableDays_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex > 0 && ddlScheme.SelectedIndex > 0 && ddlSem.SelectedIndex > 0 && ddlSubjectType.SelectedIndex > 0 && ddlSection.SelectedIndex > 0 && ddlChangeTimetableDays.SelectedIndex > 0)
            {
                LoadSlots();

                BindFilterListView();
            }
            else
            {
                txtHdayDate.Text = "";
                txtReason.Text = "";
                chkSelectdSlots.ClearSelection();
                chkSelectdSlots.Items.Clear();

                BindFilterListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HolidayMaster.ddlChangeTimetableDays_SelectedIndexChanged-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void txtHdayDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlExistingDates.SelectedIndex > 0)
            {
                string myStr = ddlExistingDates.SelectedItem.ToString();
                string[] ssizes = myStr.Split(' ');
                string startdate = ssizes[0].ToString();
                string enddate = ssizes[2].ToString();

                if (Convert.ToDateTime(txtHdayDate.Text) < Convert.ToDateTime(startdate))
                {
                    objCommon.DisplayMessage(this.Page, "Selected Date should be in between " + startdate + " - " + enddate + " Existing Dates", this.Page);
                    lvHday.DataSource = null;
                    lvHday.DataBind();
                    BindFilterListView();
                    txtHdayDate.Text = string.Empty;
                    txtHdayDate.Focus();
                }
                else if (Convert.ToDateTime(txtHdayDate.Text) > Convert.ToDateTime(enddate))
                {
                    objCommon.DisplayMessage(this.Page, "Selected Date should be in between " + startdate + " - " + enddate + " Existing Dates", this.Page);
                    lvHday.DataSource = null;
                    lvHday.DataBind();
                    BindFilterListView();
                    txtHdayDate.Text = string.Empty;
                    txtHdayDate.Focus();
                }
            }
        }
        catch { }
    }
    #endregion SelectionIndexChanged

    #region Transaction
    //used for top add and updated the holiday
    AcdAttendanceModel objAC = new AcdAttendanceModel();
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int HdayNo = 0;
            if (txtHdayDate.Text != string.Empty)
            {
                //validate date and day will be matched or not
                string SelectedDateDay = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.GetDayName(DateTime.Parse(txtHdayDate.Text).DayOfWeek);

                if (ddlChangeTimetableDays.SelectedItem.Text != SelectedDateDay)
                {
                    objCommon.DisplayMessage(this.Page, "Please Select Proper Restricted Holiday Date As Per Selected Day", this.Page);
                    txtHdayDate.Text = string.Empty;
                    txtHdayDate.Focus();
                    return;
                }
                else
                {
                    string myStr = ddlExistingDates.SelectedItem.ToString();
                    string[] ssizes = myStr.Split(' ');
                    string startdate = ssizes[0].ToString();
                    string enddate = ssizes[2].ToString();

                    if (Convert.ToDateTime(txtHdayDate.Text) < Convert.ToDateTime(startdate))
                    {
                        objCommon.DisplayMessage(this.Page, "Selected Date should be in between " + startdate + " - " + enddate + " Existing Dates", this.Page);
                        txtHdayDate.Text = string.Empty;
                        txtHdayDate.Focus();
                        return;
                    }
                    else if (Convert.ToDateTime(txtHdayDate.Text) > Convert.ToDateTime(enddate))
                    {
                        objCommon.DisplayMessage(this.Page, "Selected Date should be in between " + startdate + " - " + enddate + " Existing Dates", this.Page);
                        txtHdayDate.Text = string.Empty;
                        txtHdayDate.Focus();
                        return;
                    }
                    else
                    {
                    string Slots = "";
                    foreach (ListItem s in chkSelectdSlots.Items)
                    {
                        if (s.Selected)
                        {
                            Slots += s.Value + ",";// your final output will be in this one try this
                        }
                    }
                    if (Slots == "")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Select Slots", this.Page);
                        return;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Slots))
                            Slots = Slots.Substring(0, Slots.Length - 1);

                        string hdayName = string.Empty;
                        DateTime hdayDate = DateTime.Now;
                        int hdayLock = 0;

                        hdayName = txtHdayName.Text.Trim();
                        hdayDate = Convert.ToDateTime(txtHdayDate.Text);

                        //if (rblhdayLock.Checked == true)
                        //    hdayLock = 1;
                        //else
                        //    hdayLock = 0;

                        if (ViewState["HdayNo"] != null)
                            HdayNo = Convert.ToInt32(ViewState["HdayNo"]);

                        objAC.holidayNo = HdayNo;
                        objAC.holidayname = hdayName;
                        objAC.holidaydate = hdayDate;
                        objAC.lockHoliday = hdayLock;

                        objAC.HsessionNo = Convert.ToInt32(ddlSession.SelectedValue);
                        objAC.HdegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
                        objAC.HbranchNo = Convert.ToInt32(ddlBranch.SelectedValue);

                        objAC.HschemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
                        objAC.HsemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
                        objAC.HsubId = Convert.ToInt32(ddlSubjectType.SelectedValue);

                        objAC.HSectionNo = Convert.ToInt32(ddlSection.SelectedValue);
                        objAC.HBatchNo = Convert.ToInt32(ddlBatches.SelectedValue);
                        objAC.HdayNo = Convert.ToInt32(ddlChangeTimetableDays.SelectedValue);

                        objAC.Hslots = Slots;
                        objAC.Hreason = (txtReason.Text);
                        //Check for add or edit
                        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                        {
                            //Edit and Update
                            CustomStatus cs = (CustomStatus)objAttC.UpdateHDay(objAC);

                            if (cs.Equals(CustomStatus.DuplicateRecord))    //Added by Abhinay Lad [17-06-2019]
                            {
                                ClearControls();
                                objCommon.DisplayMessage(this.Page, "Holiday Already Added for Same Name and Date !!! Try to Update another one.", this.Page);
                            }
                            else if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                ClearControls();
                                objCommon.DisplayMessage(this.Page, "Holiday Updated successfully", this.Page);
                            }
                            else
                            {
                                objCommon.DisplayMessage(this.Page, "Something went Wrong !!! Please Try after some Time.", this.Page);
                            }
                        }
                        else
                        {
                            //Add New
                            CustomStatus cs = (CustomStatus)objAttC.AddHDay(objAC);

                            if (cs.Equals(CustomStatus.DuplicateRecord))    //Added by Abhinay Lad [17-06-2019]
                            {
                                ClearControls();
                                objCommon.DisplayMessage(this, "Holiday Already Added for Same Name and Date !!! Try to make another one.", this.Page);
                            }
                            else if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                ClearControls();
                                objCommon.DisplayMessage(this.Page, "Holiday added successfully", this.Page);
                            }
                            else
                            {
                                objCommon.DisplayMessage(this.Page, "Something went Wrong !!! Please Try after some Time.", this.Page);
                            }
                        }
                    }//inner else closing
                   }
                }//outer else closing
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Holiday Date", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_HolidayMaster.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    //Edit holiday if you want to update
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int srno = int.Parse(btnEdit.CommandArgument);
            ViewState["HdayNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["edit"] = "edit";
            this.ShowDetails(srno);
            BindFilterListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_HolidayMaster.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    //gettting holiday and Bind this is on All controllers like Drop down list and text boxes
    private void ShowDetails(int hdayNo)
    {
        try
        {
            SqlDataReader dr = objAttC.GetHoliday(hdayNo);
            if (dr != null)
            {
                if (dr.Read())
                {
                    //if (dr["LOCK"].ToString() == "1")
                    //{
                    //    rblhdayLock.Checked = true;
                    //    rblhdayUnLock.Checked = false;
                    //}
                    //else
                    //{
                    //    rblhdayLock.Checked = false;
                    //    rblhdayUnLock.Checked = true;
                    //}
                    ddlSession.Text = dr["SESSIONNO"] == DBNull.Value ? "0" : dr["SESSIONNO"].ToString();
                    //bind degree name in drop down list
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
                    ddlDegree.SelectedValue = dr["DEGREENO"] == DBNull.Value ? "0" : dr["DEGREENO"].ToString();
                    //bind branch name in drop list
                    objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON B.BRANCHNO=A.BRANCHNO", "B.BRANCHNO", "LONGNAME", "B.BRANCHNO<>99 AND DEGREENO = " + ddlDegree.SelectedValue, "B.BRANCHNO");
                    DataSet ds = objCommon.FillDropDown("ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
                    string BranchNos = "";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        BranchNos += ds.Tables[0].Rows[i]["BranchNo"].ToString() + ",";
                    }
                    DataSet dsReff = objCommon.FillDropDown("REFF", "*", "", string.Empty, string.Empty);
                    //on faculty login to get only those dept which is related to logged in faculty
                    objCommon.FilterDataByBranch(ddlBranch, dsReff.Tables[0].Rows[0]["FILTER_USER_TYPE"].ToString(), BranchNos, Convert.ToInt32(dsReff.Tables[0].Rows[0]["BRANCH_FILTER"].ToString()), Convert.ToInt32(Session["usertype"]));

                    ddlBranch.SelectedValue = dr["BRANCHNO"] == DBNull.Value ? "0" : dr["BRANCHNO"].ToString();
                    //bind scheme name in drop down list
                    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");
                    ddlScheme.SelectedValue = dr["SCHEMENO"] == DBNull.Value ? "0" : dr["SCHEMENO"].ToString();

                    if (ddlBranch.SelectedIndex > 0)
                    {//bind semester name in drop down list
                        this.FillDatesDropDown(ddlSem, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
                    }
                    ddlSem.SelectedValue = dr["SEMESTERNO"] == DBNull.Value ? "0" : dr["SEMESTERNO"].ToString();

                    //bind Section name in drop down list
                    objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + ddlScheme.SelectedValue + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SECTIONNO > 0", "SC.SECTIONNAME");
                    ddlSection.SelectedValue = dr["SECTIONNO"] == DBNull.Value ? "0" : dr["SECTIONNO"].ToString();

                    //bind subject type in drop down list
                    if (ddlSem.SelectedIndex > 0)
                    {
                        objCommon.FillDropDownList(ddlSubjectType, "ACD_OFFERED_COURSE OC INNER JOIN ACD_COURSE C ON OC.COURSENO=C.COURSENO INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SUBID<>9 and OC.SCHEMENO = " + ddlScheme.SelectedValue + " AND OC.SEMESTERNO = " + ddlSem.SelectedValue, "C.SUBID");
                    }
                    ddlSubjectType.SelectedValue = dr["SUBID"] == DBNull.Value ? "0" : dr["SUBID"].ToString();

                    //bind batch name in drop down list
                    if (Convert.ToInt32(ddlSubjectType.SelectedValue) == 1)
                    {
                        Batch.Visible = false;
                    }
                    else
                    {
                        Batch.Visible = true;
                        objCommon.FillDropDownList(ddlBatches, "ACD_BATCH B LEFT JOIN ACD_COURSE_TEACHER T on(T.BATCHNO=B.BATCHNO)", "distinct T.BATCHNO", "B.BATCHNAME", " B.BATCHNO>0 and T.SESSIONNO = " + ddlSession.SelectedValue + " AND T.SCHEMENO = " + ddlScheme.SelectedValue + " AND T.SEMESTERNO  = " + ddlSem.SelectedValue + " and T.SectionNo=" + ddlSection.SelectedValue, "T.BATCHNO");

                        ddlBatches.SelectedValue = dr["BATCHNO"] == DBNull.Value ? "0" : dr["BATCHNO"].ToString();
                    }

                    //to get slot type
                    objCommon.FillDropDownList(ddlSlotType, "ACD_SLOTTYPE", "SLOTTYPENO", "SLOTTYPE_NAME", "SLOTTYPENO > 0", "SLOTTYPENO");
                    DataSet dsLoadSlots = objCommon.FillDropDown("ACD_HOLIDAY_MASTER HM CROSS APPLY (SELECT * FROM [DBO].[SPLIT]  (SLOTS,',') )A INNER JOIN ACD_ACADEMIC_TT_SLOT TTS ON TTS.SLOTNO=A.VALUE INNER JOIN ACD_SLOTTYPE ST ON ST.SLOTTYPENO=TTS.SLOTTYPE", "SLOTTYPE", "SLOTTYPE_NAME", " HM.SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + ddlScheme.SelectedValue + " AND SEMESTERNO=" + ddlSem.SelectedValue + " AND SECTIONNO=" + ddlSection.SelectedValue + "", "SLOTTYPE");
                    if (dsLoadSlots.Tables[0].Rows.Count > 0)
                    {
                        ddlSlotType.SelectedValue = dsLoadSlots.Tables[0].Rows[0]["SLOTTYPE"].ToString();
                    }
                    else
                    {
                        ddlSlotType.SelectedIndex = 0;
                    }

                    txtHdayDate.Text = dr["HOLIDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["HOLIDATE"].ToString()).ToString("dd/MM/yyyy");
                    txtHdayName.Text = dr["HOLIDAY_NAME"] == null ? string.Empty : dr["HOLIDAYNAME"].ToString();

                    //to get exisitng dates
                    LoadExisitingDates();
                    DataSet dsLoadExistingDates = objCommon.FillDropDown("ACD_HOLIDAY_MASTER HM CROSS APPLY (SELECT *  FROM [DBO].[SPLIT]  (SLOTS,',') )A 	INNER JOIN  ACD_COURSE_TEACHER CT ON CT.SESSIONNO=HM.SESSIONNO AND CT.SCHEMENO=HM.SCHEMENO  AND  CT.SEMESTERNO=HM.SEMESTERNO  AND CT.SECTIONNO=HM.SECTIONNO  AND ISNULL(HM.BATCHNO,0)=ISNULL(CT.BATCHNO,0) AND HM.SUBID = CT.SUBID INNER JOIN ACD_TIME_TABLE_CONFIG T ON T.CTNO=CT.CT_NO INNER JOIN ACD_ACADEMIC_TT_SLOT TTS ON TTS.SLOTNO=T.SLOTNO INNER JOIN ACD_SLOTTYPE ST ON ST.SLOTTYPENO=TTS.SLOTTYPE ", "DISTINCT CAST(convert(varchar(10),START_DATE,103) AS NVARCHAR(10))+' - '+CAST(convert(varchar(10),END_DATE,103) AS NVARCHAR(10))  AS EXISTINGDATES", "", " HM.SESSIONNO=" + ddlSession.SelectedValue + " AND HM.SCHEMENO=" + ddlScheme.SelectedValue + " AND HM.SEMESTERNO=" + ddlSem.SelectedValue + " AND HM.SECTIONNO=" + ddlSection.SelectedValue + "AND SLOTTYPE=" + ddlSlotType.SelectedValue + " AND (CONVERT(DATE,'" + Convert.ToDateTime(txtHdayDate.Text).ToString("dd-MM-yyyy") + "',103) BETWEEN  T.START_DATE AND T.END_DATE)", "");
                    if (dsLoadExistingDates.Tables[0].Rows.Count > 0)
                    {
                        ddlExistingDates.SelectedValue = dsLoadExistingDates.Tables[0].Rows[0]["EXISTINGDATES"].ToString();
                    }
                    else
                    {
                        ddlExistingDates.SelectedIndex = 0;
                    }



                    //bind Day name in drop down list
                    objCommon.FillDropDownList(ddlChangeTimetableDays, "ACD_DAY_MASTER", "distinct DAY_NO", "DAY_NAME", "", "DAY_NO");
                    ddlChangeTimetableDays.SelectedValue = dr["DAYNO"] == DBNull.Value ? "0" : dr["DAYNO"].ToString();

                    LoadSlots();
                    txtReason.Text = dr["REASON"] == null ? string.Empty : dr["REASON"].ToString();


                    string SLOTS = dr["SLOTS"] == DBNull.Value ? string.Empty : dr["SLOTS"].ToString();
                    string[] slotsid = SLOTS.Split(',');

                    for (int i = 0; i < slotsid.Length; i++)
                    {
                        foreach (ListItem item in chkSelectdSlots.Items)
                        {
                            //string ABC = userTypeIds[i];
                            if (item.Value == slotsid[i].ToString())
                                item.Selected = true;
                        }
                    }
                }
            }
            if (dr != null) dr.Close();

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_HolidayMaster.ShowDetails_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }


    //bind list view and filter list view
    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{
    //    BindListView();
    //    BindFilterListView();
    //}


    //used for cleare controlers
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }


    //reset controllers
    private void ClearControls()
    {
        txtHdayName.Text = string.Empty;
        txtHdayDate.Text = string.Empty;
        // rblhdayUnLock.Checked = true;
        ViewState["action"] = null;
        ddlSession.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlSubjectType.SelectedIndex = 0;
        ddlBatches.SelectedIndex = 0;
        ddlSlotType.SelectedIndex = 0;
        ddlExistingDates.SelectedIndex = 0;
        txtHdayDate.Text = "";
        txtHdayName.Text = "";
        ddlChangeTimetableDays.SelectedIndex = 0;
        txtReason.Text = "";
        chkSelectdSlots.ClearSelection();
        chkSelectdSlots.Items.Clear();
        LoadSlots();
        BindFilterListView();
    }
    #endregion Transaction
       
}