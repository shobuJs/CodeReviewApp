//=================================================================================
// PROJECT NAME  : RFC-SVCE                                                      
// MODULE NAME   : ACADEMIC - HIGHER STUDY DETAILS ENTRY                                           
// CREATION DATE : 20-JAN-2020                                                     
// CREATED BY    : RAJU BITODE                                                
// MODIFIED BY   :                                                     
// MODIFIED DESC : 
//=================================================================================

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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Linq;
using System.IO;

using System.Net.NetworkInformation;
using System.Diagnostics;
using Newtonsoft.Json.Linq;




public partial class ACADEMIC_HigherStudies : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController objCStud = new StudentController();
    Student objEstud = new Student();

    #region Page load
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
                // set session for checkbox in listview for selecting only 2 student.
                Session["chkStudent"] = 0;
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

                    //CheckActivity();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    this.PopulateDropDownList();

                    //CHECK THE STUDENT LOGIN
                    string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]) + " and ua_no=" + Convert.ToInt32(Session["userno"]));
                    ViewState["usertype"] = ua_type;

                    if (ViewState["usertype"].ToString() == "2")
                    {
                        tblInfo.Visible = true;
                        DivDetails.Visible = true;
                        pnlShow.Visible = true;
                        DivSearch.Visible = false;
                        ShowDetails();
                    }
                    else if (ViewState["usertype"].ToString() == "3")
                    {
                        DivSearch.Visible = true;
                        pnlShow.Visible = true;
                    }
                    else
                    {
                        DivSearch.Visible = true;
                        pnlShow.Visible = true;
                    }

                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    //  ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];


                }
            }
            //divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AutoAssignValuer.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=HigherStudies.aspx");
            }
            Common objCommon = new Common();
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HigherStudies.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            DataSet dsCountry, dsState;
            dsCountry = objCommon.FillDropDown("ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO > 0", "COUNTRYNO");
            dsState = objCommon.FillDropDown("ACD_STATE", "STATENO", "STATENAME", "STATENO > 0", "STATENAME");
            FillDropDown(dsCountry, ddlCountry);
            FillDropDown(dsState, ddlState);
            DataTable dt1 = dsState.Tables[0];
            ViewState["State"] = dt1;

            DataTable dt = dsCountry.Tables[0];
            ViewState["Country"] = dt;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_REPORTS_StudentResultList.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void FillDropDown(DataSet ds, DropDownList ddlList)
    {
        ddlList.Items.Clear();
        ddlList.Items.Add("Please Select");
        ddlList.SelectedItem.Value = "0";

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlList.DataSource = ds;
            ddlList.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlList.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlList.DataBind();
            ddlList.SelectedIndex = 0;
        }
    }
    // used for  to check usertype Activity is stop or not
    private void CheckActivity()
    {
        string sessionno = string.Empty;
        sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "FLOCK=1");
        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "max(SA.SESSION_NO)SESSION_NO", "AM.ACTIVITY_CODE = 'EXAMREG'");

        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                //pnlShow.Visible = true;
                firstDiv.Visible = false;

            }
        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            //pnlShow.Visible = true;
            firstDiv.Visible = false;
        }
        dtr.Close();
    }

    #endregion Page load

    #region Dropdown

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (txtRollNo.Text == string.Empty)
        {
            tblInfo.Visible = false;
            DivDetails.Visible = false;
            objCommon.DisplayMessage(this.Page, "Please Enter Registration  No...!!", this.Page);
            return;
        }
        DataSet DsInfo = objCommon.FillDropDown("ACD_STUDENT", "IDNO,SEMESTERNO", "FAC_ADVISOR", "REGNO = '" + txtRollNo.Text.Trim() + "'", "");

        if (DsInfo.Tables[0].Rows.Count > 0)        //if & else Condition Added by Naresh Beerla for Regno Not Found on 14062020
        {
            ViewState["idno"] = DsInfo.Tables[0].Rows[0]["IDNO"].ToString();
            tblInfo.Visible = true;
            DivDetails.Visible = true;
            ShowDetails();
        }
        else
        {
            tblInfo.Visible = false;
            DivDetails.Visible = false;
            objCommon.DisplayMessage(this.Page, "Entered Registration  No : " + txtRollNo.Text.Trim() + " is not Found.!!", this.Page); // Added by Naresh Beerla on 14062020
            return;
        }

    }

    private void ShowDetails()
    {
        try
        {
            ClearDataTable();
            DataSet dsStudent = null;
            string idno;
            int yearwise = 0;
            if (!Session["usertype"].ToString().Equals("2") && txtRollNo.Text.Trim() == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Student Roll No.", this.Page);
                return;
            }
            if (Session["usertype"].ToString().Equals("2"))     //Student 
            {
                idno = Session["idno"].ToString();

                yearwise = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO)", "ISNULL(D.YEARWISE,0)", "S.IDNO=" + idno) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO)", "ISNULL(D.YEARWISE,0)", "S.IDNO=" + idno));

                dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_YEAR Y ON (Y.YEAR=S.YEAR) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SECTION SE ON (SE.SECTIONNO = s.SECTIONNO)", "S.IDNO,S.ROLLNO,DG.DEGREENAME,SECTIONNAME, Y.YEARNAME, S.SECTIONNO", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.REGNO as ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE ISNULL(S.PHYSICALLY_HANDICAPPED,0) WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH,IDTYPE,(CASE ISNULL(S.IDTYPE,0) WHEN '3' THEN 'DIRECT SECOND YEAR ' ELSE  'REGULAR'   END) AS STUD_TYPE", "S.IDNO='" + idno + "'", string.Empty);
            }
            else
            {
                idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");
                yearwise = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO)", "ISNULL(D.YEARWISE,0)", "S.IDNO=" + idno) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO)", "ISNULL(D.YEARWISE,0)", "S.IDNO=" + idno));
                dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_YEAR Y ON (Y.YEAR=S.YEAR) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SECTION SE ON (SE.SECTIONNO = s.SECTIONNO)", "S.IDNO,S.ROLLNO,DG.DEGREENAME,SECTIONNAME, Y.YEARNAME,S.SECTIONNO", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.REGNO as ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE ISNULL(S.PHYSICALLY_HANDICAPPED,0) WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH,IDTYPE,(CASE ISNULL(S.IDTYPE,0) WHEN '3' THEN 'DIRECT SECOND YEAR ' ELSE  'REGULAR'   END) AS STUD_TYPE", "S.IDNO='" + idno + "'", string.Empty);
            }

            if (string.IsNullOrEmpty(idno))
            {
                objCommon.DisplayMessage(this.Page, "Student with Roll No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                return;
            }

            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    btnSubmit.Visible = true; btn_Cancel.Visible = true;
                    btnSubmit.Enabled = false;
                    //Show Student Details..
                    lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                    lblSection.Text = dsStudent.Tables[0].Rows[0]["SECTIONNAME"].ToString();
                    lblSection.ToolTip = dsStudent.Tables[0].Rows[0]["SECTIONNO"].ToString();

                    lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                    lblDegree.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString();
                    lblDegree.ToolTip = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();

                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();

                    lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();

                    lblSemester.Text = yearwise == 1 ? dsStudent.Tables[0].Rows[0]["YEARNAME"].ToString() : dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();

                    #region getUpdatedDetails

                    this.GetUpdatedDetails();

                    #endregion getUpdatedDetails
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Student with Registration No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void GetUpdatedDetails()
    {
        try
        {
            DataSet dsHighStudies = objCommon.FillDropDown("ACD_STUDENT_HIGHER_STUDIES", "[RECNO]", "[IDNO],[InstituteName],[COUNTRYNO],[STATENO],[PROGRAMNAME],[FINANCIAL_ADD_DETAILS] ,[ENTRANCE_SCORE],[COUNTRYNAME],[STATENAME]", "idno=" + Convert.ToInt32(lblName.ToolTip), "");
            if (dsHighStudies != null && dsHighStudies.Tables.Count > 0)
            {
                if (dsHighStudies.Tables[0].Rows.Count > 0)
                {
                    //Show Student Details..                    
                    //     txtUnivercityNm.Text = dsHighStudies.Tables[0].Rows[0]["InstituteName"].ToString();
                    //    ddlCountry.SelectedValue = dsHighStudies.Tables[0].Rows[0]["COUNTRYNO"].ToString();
                    //    ddlState.SelectedValue = dsHighStudies.Tables[0].Rows[0]["STATENO"].ToString();
                    //     txtProgramNm.Text = dsHighStudies.Tables[0].Rows[0]["PROGRAMNAME"].ToString();
                    //    txtFinancialAds.Text = dsHighStudies.Tables[0].Rows[0]["FINANCIAL_ADD_DETAILS"].ToString();
                    //  txtMarksScore.Text = dsHighStudies.Tables[0].Rows[0]["ENTRANCE_SCORE"].ToString();
                    Session["HigherStudiesTable"] = dsHighStudies.Tables[0];
                    lvHigherstudies.DataSource = dsHighStudies;
                    lvHigherstudies.DataBind();
                    btnSubmit.Text = "Submit";
                }
                else
                {
                    lvHigherstudies.DataSource = null;
                    lvHigherstudies.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_HigherStudies.GetUpdatedDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    #endregion Dropdown

    #region Transaction

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CustomStatus ret = new CustomStatus();
        try
        {
            ret = (CustomStatus)objCStud.InsertUpdateHigherStudies((DataTable)Session["HigherStudiesTable"], Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(Session["userno"].ToString()), ViewState["ipAddress"].ToString());
            if (ret == CustomStatus.RecordSaved)
            {
                objCommon.DisplayMessage(this.Page, "Higher Studies Details Saved Successfully!!", this.Page);
                ClearAll();
                if (ViewState["usertype"].ToString() == "2")
                {
                    this.GetUpdatedDetails();
                }
            }
            btnSubmit.Text = "Submit";
        }
        catch (Exception ex)
        {
        }
    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        ClearAll();
        ClearDataTable();       //added to clear the DataTable
        ClearHigherFields();
        GetUpdatedDetails();    //Binds if Data is Present
    }

    private void ClearAll()
    {
        txtUnivercityNm.Text = string.Empty;
        ddlCountry.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        txtProgramNm.Text = string.Empty;
        txtFinancialAds.Text = string.Empty;
        txtMarksScore.Text = string.Empty;
        txtOtherState.Text = string.Empty;
        txtOtherCountry.Text = string.Empty;
        btnSubmit.Enabled = false;
    }

    #endregion Transaction
    // Adds the Multiple Records and Bind in DataTable & ListView 
    protected void btnHigherAdd_Click(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        dt = (DataTable)Session["HigherStudiesTable"];
        if (dt == null)
        {
            ClearDataTable();
            dt = this.createTable();
        }
        DataRow dr = dt.NewRow();
        dr["RECNO"] = 0;
        dr["IDNO"] = Convert.ToInt32(lblName.ToolTip);
        dr["INSTITUTENAME"] = txtUnivercityNm.Text;
        if (ddlCountry.SelectedIndex > 0)
        {
            dr["COUNTRYNO"] = ddlCountry.SelectedValue;
        }
        else
        {
            dr["COUNTRYNO"] = "0";
        }
        if (ddlState.SelectedIndex > 0)
        {
            dr["STATENO"] = ddlState.SelectedValue;
        }
        else
        {
            dr["STATENO"] = "0";
        }
        dr["PROGRAMNAME"] = txtProgramNm.Text;
        dr["FINANCIAL_ADD_DETAILS"] = txtFinancialAds.Text;
        dr["ENTRANCE_SCORE"] = Convert.ToDouble(txtMarksScore.Text);
        if (ddlCountry.SelectedIndex > 0)
        {
            dr["COUNTRYNAME"] = ddlCountry.SelectedItem.ToString();
        }
        else
            dr["COUNTRYNAME"] = txtOtherCountry.Text;
        if (ddlState.SelectedIndex > 0)
        {
            dr["STATENAME"] = ddlState.SelectedItem.ToString();
        }
        else
        {
            dr["STATENAME"] = txtOtherState.Text;
        }
        dt.Rows.Add(dr);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["RECNO"] = i;
        }
        Session["HigherStudiesTable"] = dt;
        lvHigherstudies.DataSource = dt;
        lvHigherstudies.DataBind();
        lvHigherstudies.Enabled = true;
        btnSubmit.Enabled = true;
        ClearHigherFields();
    }
    //Creates the DataTable of Particular Columns
    public DataTable createTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("RECNO", typeof(int));
        dtResult.Columns.Add("IDNO", typeof(int));
        dtResult.Columns.Add("INSTITUTENAME", typeof(string));
        dtResult.Columns.Add("COUNTRYNO", typeof(int));
        dtResult.Columns.Add("STATENO", typeof(int));
        dtResult.Columns.Add("PROGRAMNAME", typeof(string));
        dtResult.Columns.Add("FINANCIAL_ADD_DETAILS", typeof(string));
        dtResult.Columns.Add("ENTRANCE_SCORE", typeof(double));
        dtResult.Columns.Add("COUNTRYNAME", typeof(string));
        dtResult.Columns.Add("STATENAME", typeof(string));
        return dtResult;
    }
    // Clear the DataTable 
    public void ClearDataTable()
    {
        try
        {
            Session["HigherStudiesTable"] = null;
            ViewState["actionCo"] = null;
            ViewState["RECNO"] = null;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_HigherStudies.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    //Clear the Higher Studies Fields 
    public void ClearHigherFields()
    {
        txtUnivercityNm.Text = txtProgramNm.Text = txtFinancialAds.Text = txtMarksScore.Text = string.Empty;
        ddlCountry.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
    }
    //Delete data from the DataTable in ListView 
    protected void btnHigherdelete_Click(object sender, ImageClickEventArgs e)
    {
        btnSubmit.Enabled = true;
        try
        {
            ImageButton btnDel = sender as ImageButton;

            int RECNO = int.Parse(btnDel.CommandName);
            DataTable dt = (DataTable)Session["HigherStudiesTable"];
            dt.Rows.RemoveAt(RECNO);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["RECNO"] = i;
            }
            lvHigherstudies.DataSource = dt;
            lvHigherstudies.DataBind();
            btnSubmit.Text = "Update";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "HigherStudies.btnHigherdelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // To get Data into the above Fields for Update any Changes
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        int RECNO = Convert.ToInt32(btn.CommandName);
        DataTable ds = (DataTable)Session["HigherStudiesTable"];
        DataRow[] FoundRow = ds.Select("RECNO=" + RECNO);
        if (FoundRow.Length > 0)
        {
            DataTable dt = FoundRow.CopyToDataTable();
            txtUnivercityNm.Text = dt.Rows[0]["InstituteName"].ToString();
            ddlCountry.SelectedValue = dt.Rows[0]["COUNTRYNO"].ToString();
            ddlState.SelectedValue = dt.Rows[0]["STATENO"].ToString();
            txtProgramNm.Text = dt.Rows[0]["PROGRAMNAME"].ToString();
            txtFinancialAds.Text = dt.Rows[0]["FINANCIAL_ADD_DETAILS"].ToString();
            txtMarksScore.Text = dt.Rows[0]["ENTRANCE_SCORE"].ToString();

            ds.Rows.RemoveAt(RECNO);
            Session["HigherStudiesTable"] = ds;
            for (int i = 0; i < ds.Rows.Count; i++)
            {
                ds.Rows[i]["RECNO"] = i;
            }
            lvHigherstudies.DataSource = ds;
            lvHigherstudies.DataBind();
            lvHigherstudies.Enabled = false;

        }
    }

}
