//======================================================================================
// PROJECT NAME  : RFC SVCE                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Offered Course Session Wise                                    
// CREATION DATE : 21/05/19
// CREATED BY    : Raju B                                                
// MODIFIED DATE : 
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================

using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;



public partial class ACADEMIC_OfferedCourse_Session_Wise : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    UAIMS_Common objUCommon = new UAIMS_Common();

    #region Page Events

    //USED FOR INITIALSING THE MASTER PAGE
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    //USED FOR BYDEFAULT LOADING THE default PAGE
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
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    PopulateDropDownList();
                    btnPrint.Visible = false;
                    btnAd.Visible = false;
                    btnCancel.Visible = false;

                    int mul_col_flag = Convert.ToInt32(objCommon.LookUp("REFF", "MUL_COL_FLAG", "CollegeName='" + Session["coll_name"].ToString() + "'"));
                    if (mul_col_flag == 0)
                    {
                        // objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                        objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0", "COLLEGE_ID");
                        ddlClgname.SelectedIndex = 1;
                    }
                    else
                    {
                        // objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                        objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0", "COLLEGE_ID");
                        ddlClgname.SelectedIndex = 0;
                    }
                    ViewState["degreeno"] = 0;
                    ViewState["branchno"] = 0;
                    ViewState["college_id"] = 0;
                    ViewState["schemeno"] = 0;
                }
                
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page. 
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=OfferedCourse_Session_Wise.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OfferedCourse_Session_Wise.aspx");
        }
    }

    //Bind SESSION PNAME in drop downn list
    private void PopulateDropDownList()
    {   
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");
    }

    #endregion

    #region dropdown

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {           
             ViewState["YearWise"] = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "ISNULL(YEARWISE,0)YEARWISE", "DEGREENO=" + ViewState["degreeno"].ToString()));
             if (ViewState["YearWise"].ToString() == "1")
             {
                // lblDYSemester.Text = "Semester / Year";
                 objCommon.FillDropDownList(ddlSemester, "ACD_YEAR", "SEM", "YEARNAME", "YEAR<>0", "YEAR");
             }
             else
             {
                 //lblDYSemester.Text = "Semester / Year";
                 objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
             }
        }


    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {

            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");

            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON B.BRANCHNO = CDB.BRANCHNO", "B.BRANCHNO", "B.LONGNAME", "CDB.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");

        }
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;

    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        pnlCourse.Visible = false;
        if (ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "distinct SCHEMENO", "SCHEMENAME", "BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue), "SCHEMENO");
        }

        ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;

    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        //btnAd.Visible = true;
        //btnCancel.Visible = true;
        //btnPrint.Visible = true;
        //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
        ////BindListView();
        if (ddlScheme.SelectedIndex > 0)
        {
            btnAd.Visible = true;
            btnCancel.Visible = true;
            btnPrint.Visible = true;
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            BindListView();


        }
        else
        {
            pnlCourse.Visible = false;
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add("Please Select");
            // objCommon.DisplayMessage(updpnl, "Please Select Scheme!", this.Page);
            return;
        }

        ddlSemester.SelectedIndex = 0;
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAd.Visible = true;
        btnCancel.Visible = true;
        btnPrint.Visible = true;
        BindListView();
    }

    private void BindListView()
    {
        try
        {
            int schemeno = int.Parse(ViewState["schemeno"].ToString());
            int semesterno = int.Parse(ddlSemester.SelectedValue);
            int sessionno = int.Parse(ddlSession.SelectedValue);
            DataSet dsfaculty = null;
            DataSet dsmandcourse = null;

            CourseController objStud = new CourseController();

            dsfaculty = objStud.GetCourseOffered_Sessionwise(schemeno, 0, sessionno);
            if (dsfaculty != null && dsfaculty.Tables.Count > 0 && dsfaculty.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = dsfaculty;
                lvCourse.DataBind();
                pnlCourse.Visible = true;
            }

            dsmandcourse = objStud.GetMandatoryCourseOffered(schemeno, semesterno, sessionno);
            if (dsmandcourse != null && dsmandcourse.Tables.Count > 0 && dsmandcourse.Tables[0].Rows.Count > 0)
            {
                lvMandatoryCourse.DataSource = dsmandcourse;
                lvMandatoryCourse.DataBind();
                pnlCourse.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Offered_Course.BindListView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    protected void lvCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        //ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        //DropDownList ddl = dataitem.FindControl("ddlsem") as DropDownList;
        //Label lblSem = dataitem.FindControl("LblSemNo") as Label;
        //CheckBox ChkOffer = dataitem.FindControl("chkoffered") as CheckBox;

        //objCommon.FillDropDownList(ddl, "ACD_SEMESTER", "distinct SEMESTERNO", "SEMESTERNAME", string.Empty, "SEMESTERNO");
        //ddl.SelectedValue = lblSem.Text;       
        //ChkOffer.Checked = lblSem.ToolTip.Equals("1") ? true : false;

        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        //DropDownList ddl = dataitem.FindControl("ddlsem") as DropDownList;
        //Label lblSem = dataitem.FindControl("LblSemNo") as Label;
        CheckBox ChkOffer = dataitem.FindControl("chkoffered") as CheckBox;
        HiddenField hf = dataitem.FindControl("hf_offered") as HiddenField;

        //objCommon.FillDropDownList(ddl, "ACD_SEMESTER", "distinct SEMESTERNO", "SEMESTERNAME", string.Empty, "SEMESTERNO");
        //ddl.SelectedValue = lblSem.Text;
        //ChkOffer.Checked = lblSem.ToolTip.Equals("1") ? true : false;
        ChkOffer.Checked = hf.Value.Equals("1") ? true : false;
    }

    //check offered course
    protected void chkoffered_CheckedChanged(object sender, EventArgs e)
    {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        CourseController objCC = new CourseController();
        try
        {
            CheckBox chk = (CheckBox)sender;
            ListViewItem item = (ListViewItem)chk.NamingContainer;
            ListViewDataItem dataitem = (ListViewDataItem)item;
            ////HiddenField hf = (HiddenField)sender;
            //ListViewItem hfitem = (ListViewItem)hf.NamingContainer;
            //ListViewDataItem hfdataitem = (ListViewDataItem)hfitem;

            //DropDownList ddl = dataitem.FindControl("ddlsem") as DropDownList;
            //Label lblSem = dataitem.FindControl("LblSemNo") as Label;
            CheckBox ChkOffer = dataitem.FindControl("chkoffered") as CheckBox;
            HiddenField hf = dataitem.FindControl("hf_course") as HiddenField;
            if (ChkOffer.Checked == true)
            {
                string courseno = hf.Value;
                string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + courseno);
                retStatus = objCC.CheckOfferedCourse_Session(int.Parse(ddlSession.SelectedValue), int.Parse(courseno), int.Parse(ddlSemester.SelectedValue));
                if ((CustomStatus)retStatus == CustomStatus.RecordFound)
                {
                    ChkOffer.Checked = false;
                    objCommon.DisplayMessage(updpnl, "Selected course is already offered in previous session", this.Page);
                    return;
                }
                else if ((CustomStatus)retStatus == CustomStatus.TransactionFailed)
                {
                    objCommon.DisplayMessage(updpnl, "Error!", this.Page);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Offered_Course.chkoffered_CheckedChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //used for to check offer course and save
    protected void btnAd_Click(object sender, EventArgs e)
    {
        string offcourse = string.Empty;
        string moffcourse = string.Empty;
        string sem = string.Empty;
        int sessionno = 0;
        int provisional = 0;
        int final = 0;
        int userno = 0;
        try
        {
            CourseController objCC = new CourseController();

            if (Session["userno"].ToString() != string.Empty)
            {
                userno = int.Parse(Session["userno"].ToString());
            }
            else
            {
                Response.Redirect("~/default.aspx", false);
            }

            int SchemeNo = Convert.ToInt32(ViewState["schemeno"].ToString());
            int SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            sessionno = Convert.ToInt32(ddlSession.SelectedValue);

            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {
                CheckBox chkBox = dataitem.FindControl("chkoffered") as CheckBox;
                HiddenField hfcourse = dataitem.FindControl("hf_course") as HiddenField;
                if (chkBox.Checked == true)
                {
                    offcourse += hfcourse.Value + ",";
                }
            }

            foreach (ListViewDataItem dataitem in lvMandatoryCourse.Items)
            {
                CheckBox chkmoffered = dataitem.FindControl("chkmoffered") as CheckBox;
                HiddenField hf_mcourse = dataitem.FindControl("hf_mcourse") as HiddenField;
                if (chkmoffered.Checked == true)
                {
                    moffcourse += hf_mcourse.Value + ",";
                }
            }

            if (offcourse != string.Empty)
            {
                CustomStatus cs = (CustomStatus)objCC.UpdateOfferedCourse_Session_Wise(SchemeNo, offcourse, SemesterNo, sessionno);
                cs = (CustomStatus)objCC.UpdateMandatoryOfferedCourse(SchemeNo, moffcourse, SemesterNo, sessionno);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    BindListView();
                    objCommon.DisplayMessage(updpnl, "Offered subjects saved successfully", this.Page);
                }
                else
                {
                    lblStatus.Text = "Error Occurred!";
                }
            }
            else
            {
                objCommon.DisplayMessage(updpnl, "Please offer atleast one subject", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Offered_Course.btnAd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //relaod current page
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        // ddlSession.SelectedIndex = 0;
        // ddlScheme.SelectedIndex = 0;
        //// lvCourse.Visible = false;
    }

    //showing the Offered_Courses report in rptOfferedCourse.rpt file.
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ShowReport("Offered_Courses", "rptOfferedCourse.rpt");
    }

    //showing the report in pdf formate as per as  selection of report name  or file name.
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_SESSIONNO=" + int.Parse(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + int.Parse(ViewState["schemeno"].ToString()) + ",@P_SEMESTERNO=" + int.Parse(ddlSemester.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_COLLEGE_ID=" + ViewState["college_id"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_RollListForScrutiny.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex > 0)
        {
            pnlCourse.Visible = false;
            btnAd.Visible = false;
            btnCancel.Visible = false;
            btnPrint.Visible = false;
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"]=Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO DESC");
                objCommon.SetLabelData(ViewState["college_id"].ToString());               
            }
            
        }
        else
        {
            ddlSession.SelectedIndex = 0;
            objCommon.DisplayMessage("Please Select College & Regulation", this.Page);
            ddlClgname.Focus();
        }
    }
}