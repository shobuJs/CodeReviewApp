using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using DynamicAL_v2;

public partial class Academic_Data_Correction_Facility : System.Web.UI.Page
{
    Common objCommon = new Common();
    StudentController objSC = new StudentController();
    FeeCollectionController feeController = new FeeCollectionController();
    UserController objUC = new UserController();

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
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    ViewState["freshmanTeansfeer"] = 0;
                    //PopulateDropDown();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Bulkstudentupdate.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");

        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkStudentUpdate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkStudentUpdate.aspx");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            ViewState["Categery"] = null;
            ds = objSC.GetOnlineSearchDetailsForDataCorrection(txtSearch.Text, Convert.ToInt32(rdSearch.SelectedValue));

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
            }
            else
            {
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                objCommon.DisplayMessage(updSearchStudent, "Record Not Found", this.Page);
                return;
            }
            //ddlSession.SelectedIndex = 0;
            ds.Dispose();
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearch.Text = "";
            rdSearch.SelectedValue = null;

            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
        catch (Exception ex)
        {
        }
    }
    protected void lnkUsername_Click(object sender, EventArgs e)
    {
        LinkButton lnkUsername = sender as LinkButton;
        int idno = int.Parse(lnkUsername.CommandArgument);
        txtStudentName.Text = lnkUsername.CommandName.Trim();
        Session["idno"] = idno;
        ViewState["USER_NO"] = Convert.ToInt32(Session["idno"]);
        lnkStudentDetails_Click(null, null);
    }
    protected void lnkStudentDetails_Click(object sender, EventArgs e)
    {
        this.ShowStudentDetails();
    }
    private void ShowStudentDetails()
    {
        try
        {
            int userno = Convert.ToInt32(Session["idno"]);
            DataSet dsStudent = objSC.GetStudentDetailsofDataCorrection(userno);
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                Category.Visible = true;
                if (ViewState["Categery"] == null)
                {
                    ddlCategory.SelectedValue = "1";
                }
                else
                {
                    ddlCategory.SelectedValue = Convert.ToString(ViewState["Categery"]);
                }
                if (ddlCategory.SelectedValue == "1")
                {
                    Personal_Details.Visible = true;
                    Address_Details.Visible = false;
                    Educational_Details.Visible = false;
                    Program_Campus_Details.Visible = false;
                }
                else if (ddlCategory.SelectedValue == "2")
                {
                    Personal_Details.Visible = false;
                    Address_Details.Visible = true;
                    Educational_Details.Visible = false;
                    Program_Campus_Details.Visible = false;
                }
                else if (ddlCategory.SelectedValue == "3")
                {
                    Personal_Details.Visible = false;
                    Address_Details.Visible = false;
                    Educational_Details.Visible = true;
                    Program_Campus_Details.Visible = false;
                }
                else if (ddlCategory.SelectedValue == "4")
                {
                    Personal_Details.Visible = false;
                    Address_Details.Visible = false;
                    Educational_Details.Visible = false;
                    Program_Campus_Details.Visible = true;
                }
                PopulateDropDown();
                FillDropDown();
                TxtFirstName.Text = dsStudent.Tables[0].Rows[0]["FIRSTNAME"].ToString();
                TxtLastName.Text = dsStudent.Tables[0].Rows[0]["LASTNAME"].ToString();
                TxtMiddleName.Text = dsStudent.Tables[0].Rows[0]["MIDDLENAME"].ToString();
                rdoGender.SelectedValue = dsStudent.Tables[0].Rows[0]["GENDER"].ToString();
                ddlReligion.SelectedValue = dsStudent.Tables[0].Rows[0]["RELIGIONNO"].ToString();
                txtDateOfBirth.Text = dsStudent.Tables[0].Rows[0]["DOB"].ToString();
                TxtPlaceofBirth.Text = dsStudent.Tables[0].Rows[0]["PLCAOFBIRTH"].ToString();
                RadioCitizen.SelectedValue = dsStudent.Tables[0].Rows[0]["CITIZEN_NO"].ToString();
                TxtMobileNo.Text = dsStudent.Tables[0].Rows[0]["MOBILE"].ToString();
                TxtEmail.Text = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString();
                TxtPassNo.Text = dsStudent.Tables[0].Rows[0]["PASSPORTNO"].ToString();
                TxtAcrNo.Text = dsStudent.Tables[0].Rows[0]["ACRNO"].ToString();
                TxtDateIssue.Text = dsStudent.Tables[0].Rows[0]["ACR_DATE_ISSUE"].ToString();
                TxtPlaceIssue.Text = dsStudent.Tables[0].Rows[0]["ACR_PLACE_OF_ISSUE"].ToString();
                TxtFathername.Text = dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                TxtMotherName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                ddlOccupationFather.SelectedValue = dsStudent.Tables[0].Rows[0]["FATHER_OCCUPATIONNO"].ToString();
                ddlOccupationFMother.SelectedValue = dsStudent.Tables[0].Rows[0]["MOTHER_OCCUPATIONNO"].ToString();
                ddlStudenttype.SelectedValue = dsStudent.Tables[0].Rows[0]["FRESHMAN_TRANSFEREE"].ToString();
                ddlCapitaIncome.SelectedValue = dsStudent.Tables[0].Rows[0]["HOUSEHOLD_INCOMENO"].ToString();

                txtCurrentAddress.Text = dsStudent.Tables[0].Rows[0]["LADDRESS"].ToString();
                ddlCurrentCountry.SelectedValue = dsStudent.Tables[0].Rows[0]["LCOUNTRY"].ToString();
                ddlCurrentCountry_SelectedIndexChanged(new object(), new EventArgs());
                ddlCurrentProvince.SelectedValue = dsStudent.Tables[0].Rows[0]["LPROVINCE"].ToString();
                ddlCurrentProvince_SelectedIndexChanged(new object(), new EventArgs());
                ddlCurrentCity.SelectedValue = dsStudent.Tables[0].Rows[0]["LCITY"].ToString();
                txtCurrentPin.Text = dsStudent.Tables[0].Rows[0]["LPIN"].ToString();

                txtPermAddress.Text = dsStudent.Tables[0].Rows[0]["PADDRESS"].ToString();
                ddlPCon.SelectedValue = dsStudent.Tables[0].Rows[0]["PCOUNTRY"].ToString();
                ddlPCon_SelectedIndexChanged(new object(), new EventArgs());
                ddlPermanentState.SelectedValue = dsStudent.Tables[0].Rows[0]["LPROVINCE"].ToString();
                ddlPermanentState_SelectedIndexChanged(new object(), new EventArgs());
                ddlPermanentCity.SelectedValue = dsStudent.Tables[0].Rows[0]["PCITY"].ToString();
                txtPermPIN.Text = dsStudent.Tables[0].Rows[0]["PPIN"].ToString();

                
                int StudentCount = Convert.ToInt32(objCommon.LookUp("ACD_FEES_LOG", "COUNT(1)", "USERNO=" + userno));
                if (StudentCount >0)
                {
                    btnSubmitProgramCampus.Enabled = false;
                    btnSubmitEducationalDetails.Enabled = false;
                    ddlStudenttype.Enabled = false;
                }
                else
                {
                    btnSubmitProgramCampus.Enabled = true;
                    btnSubmitEducationalDetails.Enabled = true;
                    ddlStudenttype.Enabled = true;
                }
                if (dsStudent.Tables[0].Rows[0]["FRESHMAN_TRANSFEREE"].ToString() == "1")
                {
                   
                    objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                    txtPreSchool.Text = dsStudent.Tables[0].Rows[0]["PREVIOUS_SCHOOL"].ToString();
                    txtPreProgram.Text = dsStudent.Tables[0].Rows[0]["PREVIOUS_PROGRAM"].ToString();
                    ddlSemester.SelectedValue = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    divTransferee.Visible = true;
                    divfreshman.Visible = false;
                    DataSet ds = objSC.GetStudentPreference(2, userno);
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlProgram.Items.Clear();
                        ddlProgram.Items.Insert(0, new ListItem("Please Select", "0"));
                        ddlProgram.DataSource = ds.Tables[0];
                        ddlProgram.DataValueField = ds.Tables[0].Columns["PREFERENCESNO"].ToString();
                        ddlProgram.DataTextField = ds.Tables[0].Columns["PREFERENCES"].ToString();
                        ddlProgram.DataBind();
                        string STATUS = ds.Tables[3].Rows[0]["STATUS"].ToString();
                    }
                    else
                    {
                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                        {
                            ddlProgram.Items.Clear();
                            ddlProgram.Items.Insert(0, new ListItem("Please Select", "0"));
                            ddlProgram.DataSource = ds.Tables[1];
                            ddlProgram.DataValueField = ds.Tables[1].Columns["PREFERENCESNO"].ToString();
                            ddlProgram.DataTextField = ds.Tables[1].Columns["PREFERENCES"].ToString();
                            ddlProgram.DataBind();

                            string STATUS = ds.Tables[3].Rows[0]["STATUS"].ToString();
                        }
                    }
                    string PREF1 = (objCommon.LookUp("ACD_USER_BRANCH_PREF", "(CONVERT(NVARCHAR(50),COLLEGE_ID)+ '$' + CONVERT(NVARCHAR(50),AREA_INT_NO) + '$'+CONVERT(NVARCHAR(50),DEGREENO) + '$' + CONVERT(NVARCHAR(50),BRANCHNO)+ '$' + CONVERT(NVARCHAR(50),CAMPUSNO)+ '$' + CONVERT(NVARCHAR(50),ADMBATCH))PREFERENCESNO", "USERNO=" + userno + "AND BRANCH_PREF=1"));

                    if (PREF1.ToString() != string.Empty)
                    {
                        ddlProgram.SelectedValue = PREF1.ToString();
                    }
                }
                else
                {
                    divTransferee.Visible = false;
                    divfreshman.Visible = true;
                }

                if (txtPermAddress.Text != "")
                {
                    if (txtPermAddress.Text == txtCurrentAddress.Text && ddlPCon.SelectedValue == ddlCurrentCountry.SelectedValue &&
                    ddlPermanentState.SelectedValue == ddlCurrentProvince.SelectedValue && ddlPermanentCity.SelectedValue == ddlCurrentCity.SelectedValue
                    && txtPermPIN.Text == txtCurrentPin.Text)
                    // if (dsStudent.Tables[0].Rows[0]["LADDRESS"].ToString() == dsStudent.Tables[0].Rows[0]["PADDRESS"].ToString() &&
                    //dsStudent.Tables[0].Rows[0]["LCOUNTRY"].ToString() == dsStudent.Tables[0].Rows[0]["PCOUNTRY"].ToString() &&
                    //dsStudent.Tables[0].Rows[0]["LPROVINCE"].ToString() == dsStudent.Tables[0].Rows[0]["PPROVINCE"].ToString() &&
                    //dsStudent.Tables[0].Rows[0]["LCITY"].ToString() == dsStudent.Tables[0].Rows[0]["PCITY"].ToString()
                    //&& dsStudent.Tables[0].Rows[0]["LPIN"].ToString() == dsStudent.Tables[0].Rows[0]["PPIN"].ToString())
                    {
                        chkcopy.Checked = true;
                    }
                    else
                    {
                        chkcopy.Checked = false;
                    }
                }
                ddlMobileCodeStudent.SelectedValue = dsStudent.Tables[0].Rows[0]["MOBILECODE"].ToString();
                this.ListViewDataBind();
                this.ListViewDataBindProgram();
               // FillDropDown();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Semester_Registration.ShowStudentDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void PopulateDropDown()
    {
        try
        {
            DataSet ds = null;
            ds = objSC.BindAllDropDown("0");
            ddlReligion.Items.Clear();
            ddlReligion.Items.Add("Please Select");
            ddlReligion.SelectedItem.Value = "0";
            ddlCapitaIncome.Items.Clear();
            ddlCapitaIncome.Items.Add("Please Select");
            ddlCapitaIncome.SelectedItem.Value = "0";
            ddlOccupationFather.Items.Clear();
            ddlOccupationFather.Items.Add("Please Select");
            ddlOccupationFather.SelectedItem.Value = "0";
            ddlOccupationFMother.Items.Clear();
            ddlOccupationFMother.Items.Add("Please Select");
            ddlOccupationFMother.SelectedItem.Value = "0";
            if (ds.Tables[3].Rows.Count > 0)
            {
                ddlReligion.DataSource = ds.Tables[3];
                ddlReligion.DataValueField = "RELIGIONNO";
                ddlReligion.DataTextField = "RELIGION";
                ddlReligion.DataBind();
                ddlReligion.SelectedIndex = 0;
            }
            if (ds.Tables[4].Rows.Count > 0)
            {
                ddlCapitaIncome.DataSource = ds.Tables[4];
                ddlCapitaIncome.DataValueField = "INCOME_NO";
                ddlCapitaIncome.DataTextField = "ANNUAL_INCOME";
                ddlCapitaIncome.DataBind();
                ddlCapitaIncome.SelectedIndex = 0;
            }
            if (ds.Tables[5].Rows.Count > 0)
            {
                ddlOccupationFMother.DataSource = ds.Tables[5];
                ddlOccupationFMother.DataValueField = "OCCUPATION";
                ddlOccupationFMother.DataTextField = "OCCNAME";
                ddlOccupationFMother.DataBind();
                ddlOccupationFMother.SelectedIndex = 0;
                ddlOccupationFather.DataSource = ds.Tables[5];
                ddlOccupationFather.DataValueField = "OCCUPATION";
                ddlOccupationFather.DataTextField = "OCCNAME";
                ddlOccupationFather.DataBind();
                ddlOccupationFather.SelectedIndex = 0;
            }
            objCommon.FillDropDownList(ddlCurrentCountry, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO>0", "COUNTRYNAME");
            objCommon.FillDropDownList(ddlPCon, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO>0", "COUNTRYNAME");
            objCommon.FillDropDownList(ddlProgramType, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "UA_SECTION");
            objCommon.FillDropDownList(ddlProgramTypes, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "UA_SECTION");
        }
        catch (Exception ex)
        {

        }
    }
    protected void FillDropDown()
    {
        DataSet ds = objUC.GetRegistrationBulkDetails(Convert.ToString(0), Convert.ToString(0), 0);

        ddlMobileCodeStudent.DataSource = ds.Tables[0];
        ddlMobileCodeStudent.DataValueField = "MOBILE_CODE";
        ddlMobileCodeStudent.DataTextField = "COUNTRYNAME";
        ddlMobileCodeStudent.DataBind();
        ddlMobileCodeStudent.SelectedValue = "63";


    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Category.Visible = true;
        if (ddlCategory.SelectedValue == "1")
        {
            Personal_Details.Visible = true;
            Address_Details.Visible = false;
            Educational_Details.Visible = false;
            Program_Campus_Details.Visible = false;
        }
        else if (ddlCategory.SelectedValue == "2")
        {
            Personal_Details.Visible = false;
            Address_Details.Visible = true;
            Educational_Details.Visible = false;
            Program_Campus_Details.Visible = false;
        }
        else if (ddlCategory.SelectedValue == "3")
        {
            Personal_Details.Visible = false;
            Address_Details.Visible = false;
            Educational_Details.Visible = true;
            Program_Campus_Details.Visible = false;
            this.ListViewDataBind();
            ViewState["ddlstudyProgram"] = null;
        }
        else if (ddlCategory.SelectedValue == "4")
        {
            Personal_Details.Visible = false;
            Address_Details.Visible = false;
            Educational_Details.Visible = false;
            Program_Campus_Details.Visible = true;
            this.ListViewDataBindProgram();
            ViewState["ddlstudyEducation"] = null;
        }
    }
    protected void btnSubmitPersonalDetails_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Categery"] = ddlCategory.SelectedValue;
            CustomStatus cs = (CustomStatus)objSC.UpdateDataCorrection(Convert.ToInt32(Session["idno"]), 1, Convert.ToString(TxtFirstName.Text), Convert.ToString(TxtMiddleName.Text), Convert.ToString(TxtLastName.Text),
                Convert.ToInt32(rdoGender.SelectedValue), Convert.ToInt32(ddlReligion.SelectedValue), Convert.ToDateTime(txtDateOfBirth.Text), Convert.ToString(TxtPlaceofBirth.Text),
                Convert.ToInt32(RadioCitizen.SelectedValue), Convert.ToString(TxtMobileNo.Text), Convert.ToString(TxtEmail.Text), Convert.ToString(TxtAcrNo.Text),
                Convert.ToString(TxtDateIssue.Text), Convert.ToString(TxtPlaceIssue.Text), Convert.ToString(TxtPassNo.Text), Convert.ToString(TxtFathername.Text),
                Convert.ToInt32(ddlOccupationFather.SelectedValue), Convert.ToString(TxtMotherName.Text), Convert.ToInt32(ddlOccupationFMother.SelectedValue), Convert.ToInt32(ddlCapitaIncome.SelectedValue),
                Convert.ToString(txtCurrentAddress.Text), Convert.ToInt32(ddlCurrentCountry.SelectedValue), Convert.ToInt32(ddlCurrentProvince.SelectedValue), Convert.ToInt32(ddlCurrentCity.SelectedValue),
                Convert.ToString(txtCurrentPin.Text), Convert.ToString(txtPermAddress.Text), Convert.ToInt32(ddlPCon.SelectedValue), Convert.ToInt32(ddlPermanentState.SelectedValue), Convert.ToInt32(ddlPermanentCity.SelectedValue),
                Convert.ToString(txtPermPIN.Text), Convert.ToInt32(ddlStudenttype.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlMobileCodeStudent.SelectedValue));

            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayUserMessage(this.Page, "Record Updated Succesfully!!!", this.Page);
                this.ShowStudentDetails();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnCancelPersonalDetails_Click(object sender, EventArgs e)
    {
        Category.Visible = false;
        Personal_Details.Visible = false;
        Address_Details.Visible = false;
        Educational_Details.Visible = false;
        Program_Campus_Details.Visible = false;
        txtStudentName.Text = "";
        txtSearch.Text = "";
        lvStudent.DataSource = null;
        lvStudent.DataBind();
    }
    protected void ddlCurrentCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlCurrentProvince, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0 AND ACTIVESTATUS = 1 AND COUNTRYNO=" + ddlCurrentCountry.SelectedValue + "", "STATENAME");
    }
    protected void ddlCurrentProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlCurrentCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 AND ACTIVESTATUS = 1 and STATENO=" + ddlCurrentProvince.SelectedValue, "CITY");
    }
    protected void ddlPCon_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlPermanentState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0 AND ACTIVESTATUS = 1 AND COUNTRYNO=" + ddlPCon.SelectedValue + "", "STATENAME");
    }
    protected void ddlPermanentState_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlPermanentCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 AND ACTIVESTATUS = 1 and STATENO=" + ddlPermanentState.SelectedValue, "CITY");
    }
    protected void ListViewDataBind()
    {
        int UserNo = Convert.ToInt32(Session["idno"]);
        int UgPg = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "UGPGOT", "USERNO=" + Convert.ToInt32(Session["idno"])));
        if (ViewState["ddlstudyEducation"] == null)
        {
            ddlProgramType.SelectedValue = UgPg.ToString();
        }
        else
        {
            ddlProgramType.SelectedValue = Convert.ToString(ViewState["ddlstudyEducation"]);
        }
        DataSet dss = objCommon.FillDropDown("ACD_USER_REGISTRATION", "FRESHMAN_TRANSFEREE", "UGPGOT", "USERNO=" + UserNo, "");
        if (dss.Tables[0] != null && dss.Tables[0].Rows.Count > 0)
        {
            ViewState["FRESHER_TRANSFER"] = dss.Tables[0].Rows[0]["FRESHMAN_TRANSFEREE"].ToString();
        }
        int COUNT = Convert.ToInt32(objCommon.LookUp("ACD_USER_LAST_QUALIFICATION", "COUNT(USERNO)", "USERNO=" + UserNo));
        if (COUNT == 0)
        {
            DataSet ds = objCommon.FillDropDown("ACD_UA_SECTION", "*,'' as SCHOOL_NAME,'' as SCHOOL_ADDRESS,'' as SCHOOL_REGION,'' as YEAR_ATTENDED,'' as SCHOOL_TYPE_NO", "UA_SECTIONNAME", "UA_SECTION>" + Convert.ToInt32(ddlProgramType.SelectedValue), "UA_SECTION desc");
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvLevellist.DataSource = ds;
                lvLevellist.DataBind();
            }
            else
            {
                lvLevellist.DataSource = null;
                lvLevellist.DataBind();
            }
        }
        else
        {
            //DataSet ds = objCommon.FillDropDown("ACD_UA_SECTION S LEFT JOIN ACD_USER_LAST_QUALIFICATION UL ON (S.UA_SECTION=UL.UGPGOT)", "UA_SECTION", "UA_SECTIONNAME,SCHOOL_NAME,SCHOOL_ADDRESS,SCHOOL_REGION,YEAR_ATTENDED,SCHOOL_TYPE,SCHOOL_TYPE_NO", "UA_SECTION>" + Convert.ToInt32(ddlProgramType.SelectedValue) + "AND USERNO=" + UserNo, "UA_SECTION desc");
            DataSet ds = objCommon.FillDropDown("ACD_USER_LAST_QUALIFICATION UL right JOIN ACD_UA_SECTION S ON S.UA_SECTION=UL.UGPGOT and  USERNO=" + UserNo, "UA_SECTION", "UA_SECTIONNAME,SCHOOL_NAME,SCHOOL_ADDRESS,SCHOOL_REGION,YEAR_ATTENDED,SCHOOL_TYPE,SCHOOL_TYPE_NO", "UA_SECTION>" + Convert.ToInt32(ddlProgramType.SelectedValue), "UA_SECTION desc");

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvLevellist.DataSource = ds;
                lvLevellist.DataBind();
                foreach (ListViewDataItem dataitem in lvLevellist.Items)
                {
                    Label lblTypeNo = dataitem.FindControl("lblTypeNo") as Label;
                    DropDownList ddlType = dataitem.FindControl("ddlType") as DropDownList;
                    ddlType.SelectedValue = lblTypeNo.Text;
                }
            }
        }
    }
    protected void ListViewDataBindProgram()
    {

        int UgPg = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "UGPGOT", "USERNO=" + Convert.ToInt32(Session["idno"])));
        if (ViewState["ddlstudyProgram"] == null)
        {
            ddlProgramTypes.SelectedValue = UgPg.ToString();
        }
        else
        {
            ddlProgramTypes.SelectedValue = Convert.ToString(ViewState["ddlstudyProgram"]);
        }

        //DataSet ds = objSC.Getdetailsofbranch(Convert.ToInt32(ddlProgramTypes.SelectedValue), Convert.ToInt32(Session["idno"]), Convert.ToInt32(0), Convert.ToInt32(0), Convert.ToInt32(0));
        string SP_Name2 = "PKG_ACD_GET_UGPG_DATA_FORPROGRAM_DATA_CORRECTION";
        string SP_Parameters2 = "@P_UGPG,@P_USERNO,@P_CAMPUSNO,@P_DISCIPLINE,@P_AFFILIATED";
        string Call_Values2 = "" + Convert.ToInt32(ddlProgramTypes.SelectedValue) + "," + Convert.ToInt32(Session["idno"]) + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(0) + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            ddlPrefrence1.Items.Clear();
            ddlPrefrence1.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlPrefrence1.DataSource = ds.Tables[0];
            ddlPrefrence1.DataValueField = ds.Tables[0].Columns["PREFERENCESNO"].ToString();
            ddlPrefrence1.DataTextField = ds.Tables[0].Columns["PREFERENCES"].ToString();
            ddlPrefrence1.DataBind();

            ddlPrefrence2.Items.Clear();
            ddlPrefrence2.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlPrefrence2.DataSource = ds.Tables[0];
            ddlPrefrence2.DataValueField = ds.Tables[0].Columns["PREFERENCESNO"].ToString();
            ddlPrefrence2.DataTextField = ds.Tables[0].Columns["PREFERENCES"].ToString();
            ddlPrefrence2.DataBind();

            ddlPrefrence3.Items.Clear();
            ddlPrefrence3.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlPrefrence3.DataSource = ds.Tables[0];
            ddlPrefrence3.DataValueField = ds.Tables[0].Columns["PREFERENCESNO"].ToString();
            ddlPrefrence3.DataTextField = ds.Tables[0].Columns["PREFERENCES"].ToString();
            ddlPrefrence3.DataBind();

            string STATUS = ds.Tables[3].Rows[0]["STATUS"].ToString();
            ViewState["BRANCHPREF"] = ds.Tables[0].Rows[0]["BRPREF"].ToString();
            ViewState["BRANCH_PREF0"] = ds.Tables[0].Rows[0]["BRANCH_PREF"].ToString();
            int i = 0;
            ViewState["BRANCH_PREF1"] = "";
            ViewState["BRANCH_PREF2"] = "";
            for (i = 0; i < Convert.ToInt32(ds.Tables[0].Rows.Count); i++)
            {
                if (i == 1)
                {
                    if (ds.Tables[0].Rows[1]["BRANCH_PREF"].ToString() == "")
                    {
                    }
                    else
                    {
                        ViewState["BRANCH_PREF1"] = ds.Tables[0].Rows[1]["BRANCH_PREF"].ToString();
                    }
                }
                if (i == 2)
                {
                    if (ds.Tables[0].Rows[1]["BRANCH_PREF"].ToString() == "")
                    {
                    }
                    else
                    {
                        ViewState["BRANCH_PREF2"] = ds.Tables[0].Rows[2]["BRANCH_PREF"].ToString();
                    }
                }

            }
        }
        string PREF1 = (objCommon.LookUp("ACD_USER_BRANCH_PREF", "(CONVERT(NVARCHAR(50),COLLEGE_ID)+ ',' + CONVERT(NVARCHAR(50),AREA_INT_NO) + ','+CONVERT(NVARCHAR(50),DEGREENO) + ',' + CONVERT(NVARCHAR(50),BRANCHNO)+ ',' + CONVERT(NVARCHAR(50),CAMPUSNO))PREFERENCESNO", "USERNO=" + Convert.ToInt32(Session["idno"]) + "AND BRANCH_PREF=1"));
        string PREF2 = (objCommon.LookUp("ACD_USER_BRANCH_PREF", "(CONVERT(NVARCHAR(50),COLLEGE_ID)+ ',' + CONVERT(NVARCHAR(50),AREA_INT_NO) + ','+CONVERT(NVARCHAR(50),DEGREENO) + ',' + CONVERT(NVARCHAR(50),BRANCHNO)+ ',' + CONVERT(NVARCHAR(50),CAMPUSNO))PREFERENCESNO", "USERNO=" + Convert.ToInt32(Session["idno"]) + "AND BRANCH_PREF=2"));
        string PREF3 = (objCommon.LookUp("ACD_USER_BRANCH_PREF", "(CONVERT(NVARCHAR(50),COLLEGE_ID)+ ',' + CONVERT(NVARCHAR(50),AREA_INT_NO) + ','+CONVERT(NVARCHAR(50),DEGREENO) + ',' + CONVERT(NVARCHAR(50),BRANCHNO)+ ',' + CONVERT(NVARCHAR(50),CAMPUSNO))PREFERENCESNO", "USERNO=" + Convert.ToInt32(Session["idno"]) + "AND BRANCH_PREF=3"));

        if (ViewState["BRANCHPREF"].ToString() == "")
        {
        }
        else
        {
            if (PREF1.ToString() != "" || PREF1.ToString() != "" || PREF1.ToString() != "")
            {
                if (UgPg == Convert.ToInt32(ddlProgramTypes.SelectedValue))
                {
                    if (ViewState["BRANCH_PREF0"].ToString() == "")
                    {
                    }
                    else
                    {
                        if (PREF1.ToString() != string.Empty)
                        {
                            ddlPrefrence1.SelectedValue = PREF1.ToString();
                        }
                    }
                    if (ViewState["BRANCH_PREF1"].ToString() == "")
                    {
                    }
                    else
                    {
                        if (ViewState["BRANCH_PREF1"].ToString() == "3")
                        {
                            if (PREF3.ToString() != string.Empty)
                            {
                                ddlPrefrence3.SelectedValue = PREF3.ToString();
                            }
                        }
                        else
                        {
                            if (PREF2.ToString() != string.Empty)
                            {
                                ddlPrefrence2.SelectedValue = PREF2.ToString();
                            }
                        }
                    }

                    if (ViewState["BRANCH_PREF2"].ToString() == "")
                    {
                    }
                    else
                    {

                        if (PREF3.ToString() != string.Empty)
                        {
                            ddlPrefrence3.SelectedValue = PREF3.ToString();
                        }
                    }
                }
            }
        }
    }
    protected void btnSubmitAddress_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Categery"] = ddlCategory.SelectedValue;
            CustomStatus cs = (CustomStatus)objSC.UpdateDataCorrection(Convert.ToInt32(Session["idno"]), 2, Convert.ToString(TxtFirstName.Text), Convert.ToString(TxtMiddleName.Text), Convert.ToString(TxtLastName.Text),
                Convert.ToInt32(rdoGender.SelectedValue), Convert.ToInt32(ddlReligion.SelectedValue), Convert.ToDateTime(txtDateOfBirth.Text), Convert.ToString(TxtPlaceofBirth.Text),
                Convert.ToInt32(RadioCitizen.SelectedValue), Convert.ToString(TxtMobileNo.Text), Convert.ToString(TxtEmail.Text), Convert.ToString(TxtAcrNo.Text),
                Convert.ToString(TxtDateIssue.Text), Convert.ToString(TxtPlaceIssue.Text), Convert.ToString(TxtPassNo.Text), Convert.ToString(TxtFathername.Text),
                Convert.ToInt32(ddlOccupationFather.SelectedValue), Convert.ToString(TxtMotherName.Text), Convert.ToInt32(ddlOccupationFMother.SelectedValue), Convert.ToInt32(ddlCapitaIncome.SelectedValue),
                Convert.ToString(txtCurrentAddress.Text), Convert.ToInt32(ddlCurrentCountry.SelectedValue), Convert.ToInt32(ddlCurrentProvince.SelectedValue), Convert.ToInt32(ddlCurrentCity.SelectedValue),
                Convert.ToString(txtCurrentPin.Text), Convert.ToString(txtPermAddress.Text), Convert.ToInt32(ddlPCon.SelectedValue), Convert.ToInt32(ddlPermanentState.SelectedValue), Convert.ToInt32(ddlPermanentCity.SelectedValue),
                Convert.ToString(txtPermPIN.Text), Convert.ToInt32(ddlStudenttype.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlMobileCodeStudent.SelectedValue));

            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayUserMessage(this.Page, "Record Updated Succesfully!!!", this.Page);
                this.ShowStudentDetails();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnCancelAddress_Click(object sender, EventArgs e)
    {
        Category.Visible = false;
        Personal_Details.Visible = false;
        Address_Details.Visible = false;
        Educational_Details.Visible = false;
        Program_Campus_Details.Visible = false;
        txtStudentName.Text = "";
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        txtSearch.Text = "";
        chkcopy.Checked = false;
    }
    protected void btnSubmitEducationalDetails_Click(object sender, EventArgs e)
    {
        CustomStatus cs = 0;
        ViewState["Categery"] = ddlCategory.SelectedValue;
        int StudyLevel = Convert.ToInt32(ddlProgramType.SelectedValue);
        int UserNo = Convert.ToInt32(Session["idno"]);
        if (StudyLevel == 6)
        {
            cs = (CustomStatus)objSC.UpdateUserEdcucationalDetails(UserNo, "0", "0", "0", "0", "0", "0", "PRE_ELEME", StudyLevel);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayUserMessage(this.Page, "Record Updated Succesfully!!!", this.Page);
                this.ShowStudentDetails();
            }
        }
        string SectionNo = string.Empty;
        string NameOfSchool = string.Empty;
        string Address = string.Empty;
        string Region = string.Empty;
        string YearAttended = string.Empty;
        string Type = string.Empty;
        string TypeNo = string.Empty;

        foreach (ListViewDataItem dataitem in lvLevellist.Items)
        {
            Label lblSectionNo = dataitem.FindControl("lblSectionNo") as Label;
            TextBox txtNameOfSchool = dataitem.FindControl("txtNameOfSchool") as TextBox;
            TextBox txtAddress = dataitem.FindControl("txtAddress") as TextBox;
            TextBox txtRegion = dataitem.FindControl("txtRegion") as TextBox;
            TextBox txtYearAttended = dataitem.FindControl("txtYearAttended") as TextBox;
            DropDownList ddlType = dataitem.FindControl("ddlType") as DropDownList;
            if (txtNameOfSchool.Text == "" || txtAddress.Text == "" || txtRegion.Text == "" || txtYearAttended.Text == "" || ddlType.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this, "All Field Are Mandatory...", this.Page);
                return;
            }
            SectionNo += lblSectionNo.Text + "$";
            NameOfSchool += txtNameOfSchool.Text + "$";
            Address += txtAddress.Text + "$";
            Region += txtRegion.Text + "$";
            YearAttended += txtYearAttended.Text + "$";
            TypeNo += ddlType.SelectedValue + "$";
            Type += ddlType.SelectedItem.Text + "$";

        }
        cs = (CustomStatus)objSC.UpdateUserEdcucationalDetails(UserNo, NameOfSchool, Address, Region, YearAttended, Type, TypeNo, SectionNo, StudyLevel);

        if (cs.Equals(CustomStatus.RecordUpdated))
        {
            if (ddlProgramType.SelectedValue != ddlProgramTypes.SelectedValue)
            {
                objCommon.DisplayUserMessage(this.Page, "Record Updated Succesfully and Also Update Program & Campus Details!!!", this.Page);
            }
            else
            {
                objCommon.DisplayUserMessage(this.Page, "Record Updated Succesfully!!!", this.Page);
            }
            this.ShowStudentDetails();
        }
        else if (cs.Equals(CustomStatus.TransactionFailed))
        {
            objCommon.DisplayMessage(this, "Server Error", this.Page);
            return;
        }
    }
    protected void btnCancelEducationalDetails_Click(object sender, EventArgs e)
    {
        Category.Visible = false;
        Personal_Details.Visible = false;
        Address_Details.Visible = false;
        Educational_Details.Visible = false;
        Program_Campus_Details.Visible = false;
        txtStudentName.Text = "";
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        txtSearch.Text = "";
    }
    protected void btnSubmitProgramCampus_Click(object sender, EventArgs e)
    {
        ViewState["Categery"] = ddlCategory.SelectedValue;
        User objU = new User();
        string College_Id = string.Empty;
        string AREA = string.Empty;
        string DegreeNo = string.Empty;
        string Branchno = string.Empty;
        string CompusNo = string.Empty;
        string AdmBatch = string.Empty;
        string Branch_Prefer = string.Empty;
        if (ddlPrefrence1.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this, "Please Select First Preference", this.Page);
            return;
        }
        string Preference1 = ddlPrefrence1.SelectedValue;
        string Preference2 = ddlPrefrence2.SelectedValue;
        string Preference3 = ddlPrefrence3.SelectedValue;
        string Program = ddlProgram.SelectedValue;
        string[] Prefer1 = Preference1.Split(',');
        string[] Prefer2 = Preference2.Split(',');
        string[] Prefer3 = Preference3.Split(',');
        string[] Program4 = Program.Split('$');
        if (ddlPrefrence1.SelectedValue != "0")
        {

            College_Id = Prefer1[0].ToString();
            AREA = Prefer1[1].ToString();
            DegreeNo = Prefer1[2].ToString();
            Branchno = Prefer1[3].ToString();
            CompusNo = Prefer1[4].ToString();
            //AdmBatch = Prefer1[5].ToString();
            Branch_Prefer = "1";
        }
        if (ddlPrefrence2.SelectedValue != "0")
        {
            College_Id = Prefer1[0].ToString() + "," + Prefer2[0].ToString();
            AREA = Prefer1[1].ToString() + "," + Prefer2[1].ToString();
            DegreeNo = Prefer1[2].ToString() + "," + Prefer2[2].ToString();
            Branchno = Prefer1[3].ToString() + "," + Prefer2[3].ToString();
            CompusNo = Prefer1[4].ToString() + "," + Prefer2[4].ToString();
            //AdmBatch = Prefer1[5].ToString() + "," + Prefer2[5].ToString();
            Branch_Prefer = "1,2";
        }
        if (ddlPrefrence3.SelectedValue != "0")
        {
            if (ddlPrefrence2.SelectedValue == "0")
            {
                College_Id = Prefer1[0].ToString() + "," + Prefer2[0].ToString() + "," + Prefer3[0].ToString();
                AREA = Prefer1[1].ToString() + "," + "0" + "," + Prefer3[1].ToString();
                DegreeNo = Prefer1[2].ToString() + "," + "0" + "," + Prefer3[2].ToString();
                Branchno = Prefer1[3].ToString() + "," + "0" + "," + Prefer3[3].ToString();
                CompusNo = Prefer1[4].ToString() + "," + "0" + "," + Prefer3[4].ToString();
                //AdmBatch = Prefer1[5].ToString() + "," + "0" + "," + Prefer3[5].ToString();
                Branch_Prefer = "1,2,3";
            }
            else
            {

                College_Id = Prefer1[0].ToString() + "," + Prefer2[0].ToString() + "," + Prefer3[0].ToString();
                AREA = Prefer1[1].ToString() + "," + Prefer2[1].ToString() + "," + Prefer3[1].ToString();
                DegreeNo = Prefer1[2].ToString() + "," + Prefer2[2].ToString() + "," + Prefer3[2].ToString();
                Branchno = Prefer1[3].ToString() + "," + Prefer2[3].ToString() + "," + Prefer3[3].ToString();
                CompusNo = Prefer1[4].ToString() + "," + Prefer2[4].ToString() + "," + Prefer3[4].ToString();
                //AdmBatch = Prefer1[5].ToString() + "," + Prefer2[5].ToString() + "," + Prefer3[5].ToString();
                Branch_Prefer = "1,2,3";
            }
        }
        if (ddlProgram.SelectedValue != "0")
        {


            //College_Id = Prefer1[0].ToString() + "," + Prefer2[0].ToString() + "," + Prefer3[0].ToString();
            //AREA = Prefer1[1].ToString() + "," + Prefer2[1].ToString() + "," + Prefer3[1].ToString();
            //DegreeNo = Prefer1[2].ToString() + "," + Prefer2[2].ToString() + "," + Prefer3[2].ToString();
            //Branchno = Prefer1[3].ToString() + "," + Prefer2[3].ToString() + "," + Prefer3[3].ToString();
            //CompusNo = Prefer1[4].ToString() + "," + Prefer2[4].ToString() + "," + Prefer3[4].ToString();
            ////AdmBatch = Prefer1[5].ToString() + "," + Prefer2[5].ToString() + "," + Prefer3[5].ToString();
            //Branch_Prefer = "1,2,3";

            College_Id = Convert.ToString(Program4[0]);
            AREA = Convert.ToString(Program4[1]);
            DegreeNo = Convert.ToString(Program4[2]);
            Branchno = Convert.ToString(Program4[3]);
            CompusNo = Convert.ToString(Program4[4]);
            Branch_Prefer = "1";

        }
        else
        {
            if ((ddlPrefrence2.SelectedValue == "0" && ddlPrefrence3.SelectedValue == "0"))
            {
                College_Id = Prefer1[0].ToString() + "," + Prefer2[0].ToString() + "," + Prefer3[0].ToString();
                AREA = Prefer1[1].ToString() + "," + "0" + "," + "0";
                DegreeNo = Prefer1[2].ToString() + "," + "0" + "," + "0";
                Branchno = Prefer1[3].ToString() + "," + "0" + "," + "0";
                CompusNo = Prefer1[4].ToString() + "," + "0" + "," + "0";
                //AdmBatch = Prefer1[5].ToString() + "," + "0" + "," + "0";
                Branch_Prefer = "1,2,3";
            }
            else if ((ddlPrefrence2.SelectedValue == "0"))
            {
                College_Id = Prefer1[0].ToString() + "," + Prefer2[0].ToString() + "," + Prefer3[0].ToString();
                AREA = Prefer1[1].ToString() + "," + "0" + "," + Prefer3[1].ToString();
                DegreeNo = Prefer1[2].ToString() + "," + "0" + "," + Prefer3[2].ToString();
                Branchno = Prefer1[3].ToString() + "," + "0" + "," + Prefer3[3].ToString();
                CompusNo = Prefer1[4].ToString() + "," + "0" + "," + Prefer3[4].ToString();
                //AdmBatch = Prefer1[5].ToString() + "," + "0" + "," + Prefer3[5].ToString();
                Branch_Prefer = "1,2,3";
            }
            else if (ddlPrefrence3.SelectedValue == "0")
            {
                College_Id = Prefer1[0].ToString() + "," + Prefer2[0].ToString() + "," + Prefer3[0].ToString();
                AREA = Prefer1[1].ToString() + "," + Prefer2[1].ToString() + "," + "0";
                DegreeNo = Prefer1[2].ToString() + "," + Prefer2[2].ToString() + "," + "0";
                Branchno = Prefer1[3].ToString() + "," + Prefer2[3].ToString() + "," + "0";
                CompusNo = Prefer1[4].ToString() + "," + Prefer2[4].ToString() + "," + "0";
                //AdmBatch = Prefer1[5].ToString() + "," + Prefer2[5].ToString() + "," + "0";
                Branch_Prefer = "1,2,3";
            }
        }
        objU.USERNO = Convert.ToInt32(Session["idno"]);
        objU.UGPG = Convert.ToInt32(ddlProgramTypes.SelectedValue);
        objU.Campus = 1;
        CustomStatus cs = CustomStatus.Others;
        ViewState["freshmanTeansfeer"] = (objCommon.LookUp("acd_user_registration", "isnull(FRESHMAN_TRANSFEREE,0)FRESHMAN_TRANSFEREE", "USERNO=" + objU.USERNO));
        string Adm = (objCommon.LookUp("acd_user_registration", "ADMBATCH", "USERNO=" + objU.USERNO));
        if (Convert.ToInt32(ViewState["freshmanTeansfeer"]) == 0)
        {
            cs = (CustomStatus)objSC.UpdateBranchPreferenceDataEntry(objU, College_Id, DegreeNo, Branchno, AREA, CompusNo, Adm, Branch_Prefer, Convert.ToInt32(Session["userno"]), objU.UGPG);

            if (cs.Equals(CustomStatus.RecordSaved))
            {

                if (ddlProgramType.SelectedValue != ddlProgramTypes.SelectedValue)
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Updated Succesfully and Also Update Educational Details!!!", this.Page);
                }
                else
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Updated Succesfully!!!", this.Page);
                }
                objCommon.DisplayUserMessage(this.Page, "Record Updated Succesfully!!!", this.Page);
                this.ListViewDataBindProgram();
                this.ShowStudentDetails();
            }
            else
            {
                objCommon.DisplayMessage(this, "Server Error", this.Page);
                return;
            }
        }
        else
        {

            string retstatus = objSC.AddBranchPreferenceTranferee(Convert.ToInt32(objU.USERNO), College_Id, DegreeNo, Branchno, AREA, Branch_Prefer, "", "", "", Convert.ToString(txtPreSchool.Text), Convert.ToString(txtPreProgram.Text), Convert.ToInt32(ddlSemester.SelectedValue));
            if (retstatus.ToString() == "1")
            {
                if (ddlProgramType.SelectedValue != ddlProgramTypes.SelectedValue)
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Updated Succesfully and Also Update Educational Details!!!", this.Page);
                }
                else
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Updated Succesfully!!!", this.Page);
                }
                objCommon.DisplayUserMessage(this.Page, "Record Updated Succesfully!!!", this.Page);
                this.ListViewDataBindProgram();
                this.ShowStudentDetails();
            }
            
        }
        

    }
    protected void btnCancelProgramCampus_Click(object sender, EventArgs e)
    {
        Category.Visible = false;
        Personal_Details.Visible = false;
        Address_Details.Visible = false;
        Educational_Details.Visible = false;
        Program_Campus_Details.Visible = false;
        txtStudentName.Text = "";
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        txtSearch.Text = "";
    }
    protected void ddlProgramTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCategory.SelectedValue = "4";
        ViewState["ddlstudyProgram"] = ddlProgramTypes.SelectedValue;
        this.ListViewDataBindProgram();
    }
    protected void ddlProgramType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ddlstudyEducation"] = ddlProgramType.SelectedValue;
        this.ListViewDataBind();
    }
    protected void ddlPrefrence1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string drop2 = ""; string drop3 = "";
        string SP_Name2 = "PKG_ACD_GET_UGPG_DATA_FORPROGRAM_DATA_CORRECTION";
        string SP_Parameters2 = "@P_UGPG,@P_USERNO,@P_CAMPUSNO,@P_DISCIPLINE,@P_AFFILIATED";
        string Call_Values2 = "" + Convert.ToInt32(ddlProgramTypes.SelectedValue) + "," + Convert.ToInt32(Session["idno"]) + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(0) + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            drop2 = ddlPrefrence2.SelectedValue;
            drop3 = ddlPrefrence3.SelectedValue;
            ddlPrefrence2.Items.Clear();
            ddlPrefrence2.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlPrefrence2.DataSource = ds.Tables[0];
            ddlPrefrence2.DataValueField = ds.Tables[0].Columns["PREFERENCESNO"].ToString();
            ddlPrefrence2.DataTextField = ds.Tables[0].Columns["PREFERENCES"].ToString();
            ddlPrefrence2.DataBind();

            ddlPrefrence3.Items.Clear();
            ddlPrefrence3.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlPrefrence3.DataSource = ds.Tables[0];
            ddlPrefrence3.DataValueField = ds.Tables[0].Columns["PREFERENCESNO"].ToString();
            ddlPrefrence3.DataTextField = ds.Tables[0].Columns["PREFERENCES"].ToString();
            ddlPrefrence3.DataBind();

            ListItem itemToRemove = ddlPrefrence1.Items.FindByValue(ddlPrefrence1.SelectedValue);
            ddlPrefrence2.Items.Remove(itemToRemove);
            ddlPrefrence3.Items.Remove(itemToRemove);

            if (ddlPrefrence2.Items.FindByValue(drop2) != null)
            {
                ddlPrefrence2.SelectedValue = drop2;
            }

            if (ddlPrefrence3.Items.FindByValue(drop3) != null)
            {
                ddlPrefrence3.SelectedValue = drop3;
            }
            if (ddlPrefrence1.SelectedValue == "0")
            {
                ddlPrefrence2.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlPrefrence3.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }
    }
    protected void ddlPrefrence2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string drop1 = ""; string drop3 = "";
        string SP_Name2 = "PKG_ACD_GET_UGPG_DATA_FORPROGRAM_DATA_CORRECTION";
        string SP_Parameters2 = "@P_UGPG,@P_USERNO,@P_CAMPUSNO,@P_DISCIPLINE,@P_AFFILIATED";
        string Call_Values2 = "" + Convert.ToInt32(ddlProgramTypes.SelectedValue) + "," + Convert.ToInt32(Session["idno"]) + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(0) + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            drop1 = ddlPrefrence1.SelectedValue;
            drop3 = ddlPrefrence3.SelectedValue;
            ddlPrefrence1.Items.Clear();
            ddlPrefrence1.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlPrefrence1.DataSource = ds.Tables[0];
            ddlPrefrence1.DataValueField = ds.Tables[0].Columns["PREFERENCESNO"].ToString();
            ddlPrefrence1.DataTextField = ds.Tables[0].Columns["PREFERENCES"].ToString();
            ddlPrefrence1.DataBind();

            ddlPrefrence3.Items.Clear();
            ddlPrefrence3.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlPrefrence3.DataSource = ds.Tables[0];
            ddlPrefrence3.DataValueField = ds.Tables[0].Columns["PREFERENCESNO"].ToString();
            ddlPrefrence3.DataTextField = ds.Tables[0].Columns["PREFERENCES"].ToString();
            ddlPrefrence3.DataBind();

            ListItem itemToRemove = ddlPrefrence2.Items.FindByValue(ddlPrefrence2.SelectedValue);
            ddlPrefrence1.Items.Remove(itemToRemove);
            ddlPrefrence3.Items.Remove(itemToRemove);

            if (ddlPrefrence1.Items.FindByValue(drop1) != null)
            {
                ddlPrefrence1.SelectedValue = drop1;
            }

            if (ddlPrefrence3.Items.FindByValue(drop3) != null)
            {
                ddlPrefrence3.SelectedValue = drop3;
            }
            if (ddlPrefrence2.SelectedValue == "0")
            {
                ddlPrefrence3.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlPrefrence1.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }
    }
    protected void ddlPrefrence3_SelectedIndexChanged(object sender, EventArgs e)
    {
        string drop1 = ""; string drop2 = "";
        string SP_Name2 = "PKG_ACD_GET_UGPG_DATA_FORPROGRAM_DATA_CORRECTION";
        string SP_Parameters2 = "@P_UGPG,@P_USERNO,@P_CAMPUSNO,@P_DISCIPLINE,@P_AFFILIATED";
        string Call_Values2 = "" + Convert.ToInt32(ddlProgramTypes.SelectedValue) + "," + Convert.ToInt32(Session["idno"]) + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(0) + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            drop1 = ddlPrefrence1.SelectedValue;
            drop2 = ddlPrefrence2.SelectedValue;
            ddlPrefrence1.Items.Clear();
            ddlPrefrence1.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlPrefrence1.DataSource = ds.Tables[0];
            ddlPrefrence1.DataValueField = ds.Tables[0].Columns["PREFERENCESNO"].ToString();
            ddlPrefrence1.DataTextField = ds.Tables[0].Columns["PREFERENCES"].ToString();
            ddlPrefrence1.DataBind();

            ddlPrefrence2.Items.Clear();
            ddlPrefrence2.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlPrefrence2.DataSource = ds.Tables[0];
            ddlPrefrence2.DataValueField = ds.Tables[0].Columns["PREFERENCESNO"].ToString();
            ddlPrefrence2.DataTextField = ds.Tables[0].Columns["PREFERENCES"].ToString();
            ddlPrefrence2.DataBind();
            ListItem itemToRemove = ddlPrefrence3.Items.FindByValue(ddlPrefrence3.SelectedValue);
            ddlPrefrence1.Items.Remove(itemToRemove);
            ddlPrefrence2.Items.Remove(itemToRemove);

            if (ddlPrefrence1.Items.FindByValue(drop1) != null)
            {
                ddlPrefrence1.SelectedValue = drop1;
            }

            if (ddlPrefrence2.Items.FindByValue(drop2) != null)
            {
                ddlPrefrence2.SelectedValue = drop2;
            }
            if (ddlPrefrence3.SelectedValue == "0")
            {
                ddlPrefrence2.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlPrefrence1.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }
    }
    protected void chkcopy_CheckedChanged(object sender, EventArgs e)
    {
        if (chkcopy.Checked == true)
        {
            if (txtCurrentAddress.Text == "")
            {
                objCommon.DisplayUserMessage(this.Page, "Please enter  Current Address!!!", this.Page);
                chkcopy.Checked = false;
                return;
            }
            else if (ddlCurrentCountry.SelectedValue == "0")
            {
                objCommon.DisplayUserMessage(this.Page, "Please enter  Current Country!!!", this.Page);
                chkcopy.Checked = false;
                return;
            }
            else if (ddlCurrentProvince.SelectedValue == "0")
            {
                objCommon.DisplayUserMessage(this.Page, "Please enter  Current Country!!!", this.Page);
                chkcopy.Checked = false;
                return;
            }
            else if (ddlCurrentCity.SelectedValue == "0")
            {
                objCommon.DisplayUserMessage(this.Page, "Please enter  Current City/Village!!!", this.Page);
                chkcopy.Checked = false;
                return;
            }
            if (txtCurrentPin.Text == "")
            {
                objCommon.DisplayUserMessage(this.Page, "Please enter  Current ZIP/PIN!!!", this.Page);
                chkcopy.Checked = false;
                return;
            }
            txtPermAddress.Text = txtCurrentAddress.Text;
            ddlPCon.SelectedValue = ddlCurrentCountry.SelectedValue;
            ddlPCon_SelectedIndexChanged(new object(), new EventArgs());
            ddlPermanentState.SelectedValue = ddlCurrentProvince.SelectedValue;
            ddlPermanentState_SelectedIndexChanged(new object(), new EventArgs());
            ddlPermanentCity.SelectedValue = ddlCurrentCity.SelectedValue;
            txtPermPIN.Text = txtCurrentPin.Text;
        }
        else
        {
            txtPermAddress.Text = "";
            ddlPCon.SelectedValue = "0";
            ddlPermanentState.SelectedValue = "0";
            ddlPermanentCity.SelectedValue = "0";
            txtPermPIN.Text = "";
        }
    }
}