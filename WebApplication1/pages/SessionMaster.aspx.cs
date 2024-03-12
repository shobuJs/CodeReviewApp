//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : SECTION ALLOTMENT                                                     
// CREATION DATE : 27-Sept-2010                                                          
// CREATED BY    : NIRAJ D. PHALKE                                 
// MODIFIED DATE :                                                          
// MODIFIED DESC :                                                                      
//======================================================================================

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
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Globalization;



public partial class Academic_SessionCreate : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objCourse = new CourseController();
    int ACADEMICNO = 0;
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
                BindListView();
                divAcademic.Attributes.Remove("class");
                divSessionCreation.Attributes.Add("class", "active");
                divAcademicSession.Visible = false;
                divapliedtab.Visible = true;
                ViewState["ACADEMICNO"] = null;
            }
            //Populate the Drop Down Lists
            ViewState["SESSIONNOS"] = "ADD";
            PopulateDropDown();
            BindListView();
            ListViewDataBind();
            ListViewBindMapping();
            objCommon.FillDropDownList(ddlAcademicSession, "ACD_ACADEMIC_SESSION", "ACADEMIC_NO", "ACEDEMIC_NAME", "STATUS=1 and ACADEMIC_NO>=0", "");
            objCommon.FillDropDownList(ddlYearName, "ACD_EXAM_YEAR", "EXYEAR_NO", "YEAR_NAME", "", "YEAR_NAME desc");
            objCommon.FillDropDownList(ddlSessionCollege, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO");
            objCommon.FillListBox(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_ID");
            objCommon.SetLabelData("2");
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // dynamic page header addded by Swapnil Thakare 14/07/2021
        }
      
    }

    //Academic Session Start
    protected void lkAcademic_Click(object sender, EventArgs e)
    {
        divAcademic.Attributes.Add("class", "active");
        divSessionCreation.Attributes.Remove("class");
        divMapping.Attributes.Remove("class");
        divAcademicSession.Visible = true;
        divapliedtab.Visible = false;
        divmap.Visible = false;

    }
    protected void lkSessionCreation_Click(object sender, EventArgs e)
    {
        divAcademic.Attributes.Remove("class");
        divSessionCreation.Attributes.Add("class", "active");
        divMapping.Attributes.Remove("class");
        divAcademicSession.Visible = false;
        divapliedtab.Visible = true;
        divmap.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtAcademicName.Text == "")
            {
                objCommon.DisplayMessage(this, "Please Enter Academic Session Name.", this.Page);
                return;
            }
            string Name = txtAcademicName.Text.Trim();
            int Status = 0;

            if (hfdStat.Value == "true")
            {
                Status = 1;
            }
            if (ViewState["ACADEMICNO"] != null)
            {
                ACADEMICNO = Convert.ToInt32(ViewState["ACADEMICNO"].ToString());
            }
            CustomStatus cs = (CustomStatus)objCourse.AddAcademicSession(Name, Status, ACADEMICNO);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this, "Record Saved Successfully.", this.Page);
                ListViewDataBind();
                txtAcademicName.Text = "";
                ViewState["ACADEMICNO"] = null;
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this, "Record Update Successfully.", this.Page);
                ListViewDataBind();
                txtAcademicName.Text = "";
                ViewState["ACADEMICNO"] = null;
            }
            else
            {
                objCommon.DisplayMessage(this, "Failed To Save Record ", this.Page);
                txtAcademicName.Text = "";
                ViewState["ACADEMICNO"] = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "SessionCreate.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ListViewDataBind()
    {
        DataSet ds = objCommon.FillDropDown("ACD_ACADEMIC_SESSION", "*", "", "", "");
        if (ds != null)
        {
            lvAcademic.DataSource = ds;
            lvAcademic.DataBind();
        }
        else
        {
            lvAcademic.DataSource = null;
            lvAcademic.DataBind();
        }
        foreach (ListViewDataItem dataitem in lvAcademic.Items)
        {
            Label status = dataitem.FindControl("status") as Label;
            if (status.Text == "1")
            {
                status.Text = "Active";
                status.CssClass = "badge badge-success";
            }
            else
            {
                status.Text = "Deactive";
                status.CssClass = "badge badge-danger";
            }
        }

    }
    protected void btnEditAcademic_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int ACADEMICNNO = int.Parse(btnEdit.CommandArgument);
        DataSet ds = objCommon.FillDropDown("ACD_ACADEMIC_SESSION", "*", "", "ACADEMIC_NO=" + ACADEMICNNO, "");
        ViewState["ACADEMICNO"] = ACADEMICNNO;
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtAcademicName.Text = ds.Tables[0].Rows[0]["ACEDEMIC_NAME"].ToString();
            int status = Convert.ToInt32(ds.Tables[0].Rows[0]["STATUS"].ToString());
            if (status == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Src", "SetStat(true);", true);
            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Src", "SetStat(false);", true);
            }


        }

    }

    protected void btncanceldata_Click(object sender, EventArgs e)
    {
        txtAcademicName.Text = "";
    }


//Session Creation Start
    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillOddEven(ddlOddEven);

            //Fill the Exam Status DropDownList
            objCommon.FillExamStatus(ddlStatus);

            //  objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "SessionCreate.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void BindListView()
    {
        try
        {
            SessionController objSC = new SessionController();
            DataSet ds = objSC.GetAllSession();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlSession.Visible = true;
                lvSession.DataSource = ds;
                lvSession.DataBind();
            }
            else
            {
                pnlSession.Visible = false;
                lvSession.DataSource = null;
                lvSession.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SessionCreate.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        Response.Redirect(Request.Url.ToString());
        ClearControls();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SessionCreate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SessionCreate.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string StartEndDate = hdnDate.Value;
        string[] dates = new string[] { };
        if ((StartEndDate) == "")//GetDocs()
        {
            objCommon.DisplayMessage(this.updSession, "Please select Start Date End Date !", this.Page);
            return;
        }
        else
        {
            StartEndDate = StartEndDate.Substring(0, StartEndDate.Length - 0);
            //string[]
            dates = StartEndDate.Split('-');
        }
        string StartDate = dates[0];//Jul 15, 2021
        string EndDate = dates[1];
        //DateTime dateTime10 = Convert.ToDateTime(a);
        DateTime dtStartDate = DateTime.Parse(StartDate);
        string SDate = dtStartDate.ToString("yyyy/MM/dd");
        DateTime dtEndDate = DateTime.Parse(EndDate);
        string EDate = dtEndDate.ToString("yyyy/MM/dd");
        int AcademicNo = Convert.ToInt32(ddlAcademicSession.SelectedValue);

        int Academic_Year = Convert.ToInt32(ddlYearName.SelectedItem.Text);

        try
        {
            //if (txtStartDate.Text != string.Empty && txtEndDate.Text != string.Empty)
            //{

            //    if (Convert.ToDateTime(txtEndDate.Text) <= Convert.ToDateTime(txtStartDate.Text))
            //    {
            //        objCommon.DisplayMessage(this.updSession, "End Date should be greater than Start Date", this.Page);
            //        return;
            //    }
            if (Convert.ToDateTime(EDate) <= Convert.ToDateTime(SDate))
            {
                objCommon.DisplayMessage(this.updSession, "End Date should be greater than Start Date", this.Page);
                return;
            }
            else
            {
                //Set all properties
                SessionController objSC = new SessionController();
                IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession = new IITMS.UAIMS.BusinessLayer.BusinessEntities.Session();
                objSession.Session_PName = txtSLongName.Text.Trim();
                objSession.Session_Name = txtSShortName.Text.Trim();
                objSession.Session_SDate = Convert.ToDateTime(SDate);
                objSession.Session_EDate = Convert.ToDateTime(EDate);
                objSession.Odd_Even = Convert.ToInt32(ddlOddEven.SelectedValue);
                objSession.ExamType = Convert.ToInt32(ddlStatus.SelectedValue);
                objSession.Sessname_hindi = txtSessName_Hindi.Text.Trim();
                //objSession.Flock = (chkFlock.Checked);
                objSession.College_code = Session["colcode"].ToString();
                objSession.Flock = false;
                if (hdnStatus.Value == "true")
                {
                    objSession.Flock = true;
                }


                //objSession.College_ID = Convert.ToInt32(ddlCollege.SelectedValue);

                //Check for add or edit
                if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                {
                    //Edit 
                    objSession.SessionNo = Convert.ToInt32(ViewState["sessionno"]);
                    CustomStatus cs = (CustomStatus)objSC.UpdateSession(objSession, AcademicNo, Academic_Year);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        ClearControls();
                        BindListView();
                        objCommon.DisplayMessage(this.updSession, "Record Updated Successfully", this.Page);
                    }
                }
                else
                {
                    //Add New
                    CustomStatus cs = (CustomStatus)objSC.AddSession(objSession, AcademicNo, Academic_Year);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ClearControls();
                        BindListView();
                        objCommon.DisplayMessage(this.updSession, "Record Saved Successfully", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updSession, "Record already exist", this.Page);
                    }
                }
            }

            //else
            //{
            //    objCommon.DisplayMessage(this.updSession, "Server UnAvailable", this.Page);
            //    return;
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_SessionCreate.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int SESSIONNO = int.Parse(btnEdit.CommandArgument);
            ViewState["sessionno"] = int.Parse(btnEdit.CommandArgument);
            ViewState["edit"] = "edit";
            this.ShowDetails(SESSIONNO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SessionCreate.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ShowDetails(int Session_No)
    {
        try
        {
            SessionController objSS = new SessionController();
            SqlDataReader dr = objSS.GetSingleSession(Session_No);
            if (dr != null)
            {
                if (dr.Read())
                {
                    txtSLongName.Text = dr["SESSION_PNAME"] == null ? string.Empty : dr["SESSION_PNAME"].ToString();
                    txtSShortName.Text = dr["SESSION_NAME"] == null ? string.Empty : dr["SESSION_NAME"].ToString();
                    txtStartDate.Text = dr["SESSION_STDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["SESSION_STDATE"].ToString()).ToString("dd/MM/yyyy");
                    txtEndDate.Text = dr["SESSION_ENDDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["SESSION_ENDDATE"].ToString()).ToString("dd/MM/yyyy");
                    txtSessName_Hindi.Text = dr["SESSNAME_HINDI"] == null ? string.Empty : dr["SESSNAME_HINDI"].ToString();
                    ddlAcademicSession.SelectedValue = (dr["ACADEMIC"] == null ? string.Empty : dr["ACADEMIC"].ToString());
                    //ddlYearName.SelectedItem.Text = (dr["ACADEMIC_YEAR"] == null ? string.Empty : dr["ACADEMIC_YEAR"].ToString());
                    string Examno = (objCommon.LookUp("ACD_EXAM_YEAR", "EXYEAR_NO", "YEAR_NAME='" + (dr["ACADEMIC_YEAR"] == null ? string.Empty : dr["ACADEMIC_YEAR"].ToString()) + "'"));
                    ddlYearName.SelectedValue = Examno.ToString();
                    // string date = Convert.ToDateTime(dr["SESSION_STDATE"].ToString()).ToString("MMM-dd-yyyy") + " - " + Convert.ToDateTime(dr["SESSION_ENDDATE"].ToString()).ToString("MMM-dd-yyyy");
                    hdnDate.Value = Convert.ToDateTime(dr["SESSION_STDATE"].ToString()).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(dr["SESSION_ENDDATE"].ToString()).ToString("MMM dd, yyyy");
                    //(start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Setdate()", true);


                    if (dr["ODD_EVEN"] == null | dr["ODD_EVEN"].ToString().Equals(""))
                        ddlOddEven.SelectedIndex = 0;
                    else
                        ddlOddEven.Text = dr["ODD_EVEN"].ToString();

                    if (dr["EXAMTYPE"] == null | dr["EXAMTYPE"].ToString().Equals(""))
                        ddlStatus.SelectedIndex = 0;
                    else
                        ddlStatus.Text = dr["EXAMTYPE"].ToString();

                    if (Convert.ToString(dr["FLOCK"]) == string.Empty)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Src", "SetSessi(false);", true);
                        //chkFlock.Checked = false;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Src", "SetSessi(true);", true);
                        //chkFlock.Checked = true;
                    }

                    //if (dr["COLLEGE_ID"] == null | dr["COLLEGE_ID"].ToString().Equals(""))
                    //    ddlCollege.SelectedIndex = 0;
                    //else
                    //    ddlCollege.Text = dr["COLLEGE_ID"].ToString();

                    ScriptManager.RegisterClientScriptBlock(updSession, updSession.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
                }

            }
            if (dr != null) dr.Close();

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SessionCreate.ShowDetails_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ClearControls()
    {
        ddlSession.SelectedIndex = 0;
        ddlOddEven.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        ddlYearName.SelectedValue = "0";
        //  ddlCollege.SelectedIndex = 0;
        txtEndDate.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtSLongName.Text = string.Empty;
        txtSShortName.Text = string.Empty;
        //chkFlock.Checked = false;
        ViewState["action"] = null;
       


    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("SessionMaster", "rptSessionMaster.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString() + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updSession, this.updSession.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_RollListForScrutiny.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    protected void lkMapping_Click(object sender, EventArgs e)
    {
        divMapping.Attributes.Add("class", "active");
        divSessionCreation.Attributes.Remove("class");
        divAcademic.Attributes.Remove("class");
        divAcademicSession.Visible = false;
        divapliedtab.Visible = false;
        divmap.Visible = true;
        ddlCollege.Enabled = true;
        ddlSessionCollege.Enabled = true;
    }
     private void ClearSess()
    {
        ddlSessionCollege.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        //chkFlock.Checked = false;
        ViewState["action"] = null;
        ViewState["SESSIONNOS"] = "ADD";

        foreach (ListItem items in ddlCollege.Items)
        {
            items.Selected = false;
                items.Enabled = true;
        }
        ddlCollege.Enabled = true;
        ddlSessionCollege.Enabled = true;


    }
    protected void btnCancel2_Click(object sender, EventArgs e) // Add by Nehal [20-02-2023]
    {
        ClearSess();
        divSessionCreation.Attributes.Remove("class");
    }
    protected void btnSubmit2_Click(object sender, EventArgs e) // Add by Nehal [20-02-2023]
    {
        try
        {
            SessionController objSC = new SessionController();
            if (ddlSessionCollege.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.UPDCOLLEGEMAP, "Please Select Session", this.Page);
                return;
            }

            int count = 0;
            string College_Id = string.Empty;

            foreach (ListItem Item in ddlCollege.Items)
            {
                if (Item.Selected)
                {
                    College_Id += Item.Value + ",";
                    count++;
                }
            }
         
            int Status=0;
            if (hdnMappin.Value == "true")
            {
                Status = 1;
            }
            if (College_Id.ToString() == string.Empty)
            {
                objCommon.DisplayMessage(this, "Please Select Faculty /School Name", this.Page);
                return;
            }
            string College_Id_str = College_Id.ToString(); 
           int Sessionid = 0;
           if (ViewState["SESSIONNOS"].ToString() != "ADD")
           {
               Sessionid = Convert.ToInt32(ViewState["SESSIONNOS"].ToString());
           }
           
              int Sessionno = Convert.ToInt32(ddlSessionCollege.SelectedValue);

              CustomStatus cs = (CustomStatus)objSC.AddSessionMaster_Modified(College_Id_str, Sessionid, Sessionno, Convert.ToInt32(Session["userno"]),Status);
            if (cs.Equals(CustomStatus.RecordSaved))
            {

                objCommon.DisplayMessage(this.UPDCOLLEGEMAP, "Record Save sucessfully", this.Page);
                divSessionCreation.Attributes.Remove("class");
                divAcademic.Attributes.Remove("class");
              ClearSess();
                ListViewBindMapping();
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {

                objCommon.DisplayMessage(this.UPDCOLLEGEMAP, "Record Update sucessfully", this.Page);
                divSessionCreation.Attributes.Remove("class");
                divAcademic.Attributes.Remove("class");
                ClearSess();
                ListViewBindMapping();
            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(this.UPDCOLLEGEMAP, "Record Already Exist", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.UPDCOLLEGEMAP, "Record Already Exist", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ListViewBindMapping()
    {
        try
        {
            SessionController objSC = new SessionController();
            DataSet ds = objCommon.FillDropDown("ACD_SESSION_MAPPING SM INNER JOIN ACD_SESSION_MASTER S ON (S.SESSIONNO=SM.SESSIONNO)INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID=SM.COLLEGE_ID)", "distinct SM.SESSIONID,SM.SESSIONNO,COLLEGE_NAME,Convert(nvarchar,SESSION_STDATE,23) as SDATE,Convert(nvarchar,SESSION_ENDDATE,23) as EDATE ,SESSION_NAME,ACADEMIC_YEAR,SESSION_PNAME", "SM.SESSIONNO,SM.STATUS", "", "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvCollegeMap.DataSource = ds;
                lvCollegeMap.DataBind();
            }
            else
            {
                lvCollegeMap.DataSource = null;
                lvCollegeMap.DataBind();
            }
            foreach (ListViewDataItem dataitem in lvCollegeMap.Items)
            {
                Label Status = dataitem.FindControl("lblStatus") as Label;
                if(Status.Text=="1")
                {
                    Status.Text = "Active";
                    Status.CssClass = "badge badge-success";

                }
                else{
                    Status.Text = "Deactive";
                    Status.CssClass = "badge badge-danger";
                }
                
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SessionCreate.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }
    protected void btnEditSess_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int SESSIONNO = int.Parse(btnEdit.CommandArgument);
            ViewState["SESSIONNOS"] = int.Parse(btnEdit.CommandArgument);
            DataSet ds = objCommon.FillDropDown("ACD_SESSION_MAPPING", "SESSIONID,COLLEGE_ID,SESSIONNO,DATE,UA_NO", "STATUS", "SESSIONID=" + SESSIONNO, "");
          
            ddlSessionCollege.SelectedValue = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
            if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Src", "SetSessiMa(true);", true);
            }
            else{
                ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Src", "SetSessiMa(false);", true);
            }


            string[] couser = Convert.ToString(ds.Tables[0].Rows[0]["COLLEGE_ID"]).Split(',');

            foreach (string s in couser)
            {
                foreach (ListItem item in ddlCollege.Items)
                {
                    if (s == item.Value)
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
            foreach (ListItem items in ddlCollege.Items)
            {
                if (items.Selected == true)
                {
                    items.Enabled = true;
                }
                else
                {
                    items.Enabled = false;
                }
            }
            ddlCollege.Enabled = false;
            ddlSessionCollege.Enabled = false;
        
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SessionCreate.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
}
