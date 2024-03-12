using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using System.Data;
using _NVP;
using System.Collections.Specialized;

using EASendMail;

using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Threading.Tasks;

public partial class ACADEMIC_ModifiedLead : System.Web.UI.Page
{
    StudentController objSC = new StudentController();
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    NewUserController objnuc = new NewUserController();

    NewUser objnu = new NewUser();
    User objU = new User();
    OnlineAdmBranchController objBC = new OnlineAdmBranchController();
    OnlineAdmStudentFees objStudentFees = new OnlineAdmStudentFees();
    OnlineAdmStudentController ObjNucFees = new OnlineAdmStudentController();
    UserController user = new UserController();

    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

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

                Page.Title = Session["coll_name"].ToString();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                FillPersonalDetailsDropDown();
                Session["Employment_History"] = null;
                Session["Referees"] = null;
                ViewState["REFEREEES_SRNO"] = null;
                ViewState["EMPLOYEE_SRNO"] = null;
                Session["Employment_History_PG"] = null;
                Session["Referees_PG"] = null;
                ViewState["REFEREEES_SRNO_PG"] = null;
                ViewState["EMPLOYEE_SRNO_PG"] = null;
                ViewState["actionPop"] = "Add";
                BindListViewApplication();
                Session["DdlIntakeValue"] = null;
                txtRemark.Attributes.Add("maxlenght", txtRemark.MaxLength.ToString());
                ClearAllFields();
                ViewState["actionPop"] = "Add";
                BindListViewApplication();
                Session["DdlIntakeValue"] = null;
                txtRemark.Attributes.Add("maxlenght", txtRemark.MaxLength.ToString());
            }
            if (Request.Params["__EVENTTARGET"] != null &&
                    Request.Params["__EVENTTARGET"].ToString() != string.Empty)
            {
                if (Request.Params["__EVENTTARGET"].ToString() == "RemoveNormalDegree")
                    this.DeleteApplyProgram(Convert.ToInt32(ViewState["UserStudentUserno"]), 1);
                else if (Request.Params["__EVENTTARGET"].ToString() == "RemoveNormalArchDegree")
                    this.DeleteApplyProgram(Convert.ToInt32(ViewState["UserStudentUserno"]), 2);
            }
        }
        // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ModifiedLead.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ModifiedLead.aspx");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            ds = objSC.GetOnlineSearchDetails(txtSearch.Text, Convert.ToInt32(rdSearch.SelectedValue));

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
            ds.Dispose();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ClearAllFields()
    {
        divOfflinePaymentdone.Visible = false; divOfflineNote.Visible = false;
        lvLeadDetails.DataSource = null; lvLeadDetails.DataBind();
        //lvRefreesPG.DataSource = null; lvRefreesPG.DataBind(); lvEmploymentHistoryPG.DataSource = null; lvEmploymentHistoryPG.DataBind();

        //lvBranchPreferred.DataSource = null; lvBranchPreferred.DataBind();
        lvPaymentDetails.DataSource = null; lvPaymentDetails.DataBind(); lvDepositSlip.DataSource = null; lvDepositSlip.DataBind(); lvLeadList.DataSource = null; lvLeadList.DataBind();
        lvlist.DataSource = null; lvlist.DataBind(); lvDetails.DataSource = null; lvlist.DataBind();
        txtRemark.Text = ""; txtApplicatio.Text = ""; ddlIntake.SelectedValue = null;// txtNameInitial.Text = ""; 
        txtFirstName.Text = ""; txtPerlastname.Text = ""; rdbQuestion.SelectedValue = null; txtDateOfBirth.Text = "";
        rdbLeftRight.SelectedValue = null; txtEmail.Text = ""; txtPersonalPassprtNo.Text = ""; txtOnlineNIC.Text = ""; rdPersonalGender.SelectedValue = null; ddlOnlineMobileCode.SelectedValue = null; txtMobile.Text = "";
        txtHomeTel.Text = ""; ddlHomeMobileCode.SelectedValue = null; txtPermAddress.Text = ""; ddlPermanentState.SelectedValue = null; ddlPCon.SelectedValue = null; ddlPTahsil.SelectedValue = null;

        //txtHighestEducationPG.Text = ""; txtUniversityPG.Text = ""; txtQualificationAwardPG.Text = ""; txtSpecializationPG.Text = ""; txtGPAPG.Text = ""; txtProfessionalPG.Text = ""; txtProfessionalUniversityPG.Text = "";
        //txtAwardDatePG.Text = ""; txtSpecilizationQualificationPG.Text = ""; 

        ddlProgramTypes.SelectedValue = null;
        //ddlCenter.SelectedValue = null; ddlAreaofIntrest.SelectedValue = null; ddlAwarding.SelectedValue = null; 
        ddlCampusDetails.SelectedValue = null; //ddlWeekDays.SelectedValue = null;
        ddlApptituteCenter.SelectedValue = null;
        ddlMode.SelectedValue = null; ddlApptituteMedium.SelectedValue = null; lblExamcenter.Text = ""; rdPaymentOption.SelectedValue = null; lblOrderIDERP.Text = "";
        txtAmount.Text = ""; hdfAmount.Value = null; txtchallanAmount.Text = ""; hdfServiceCharge.Value = null; hdfTotalAmount.Value = null; hdnEnqueryno.Value = null;
        txtChallanId.Text = ""; txtchallanAmount.Text = ""; txtTransactionNo.Text = ""; txtPaymentdate.Text = ""; ddlbank.SelectedValue = null; txtBranchName.Text = "";
        txt_Remark.Text = string.Empty; ddlLeadStatus.SelectedValue = null; txtEndDate.Text = string.Empty; ddlIntake.SelectedValue = null; txtRemark.Text = "";
    }
    protected void lnkUsername_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAllFields();
            Session["DdlIntakeValue"] = null;

            lvStudent.DataSource = null;
            lvStudent.DataBind();
            txtSearch.Text = "";
            rdSearch.SelectedValue = null;
            LinkButton lnkUsername = sender as LinkButton;
            ViewState["UserStudentUserno"] = lnkUsername.CommandArgument.ToString();
            ViewState["UserStudentUGPGOT"] = lnkUsername.CommandName.ToString();
            objCommon.FillDropDownList(ddlProgramTypes, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "", "");
            int UgPg = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "UGPGOT", "USERNO=" + Convert.ToInt32(ViewState["UserStudentUserno"])));
            ddlProgramTypes.SelectedValue = UgPg.ToString();
            BindPersonalDetails();
            ListViewDataBind();

            FillDropDownOnline();
            FillSaveDetails();
            AppliedProgramDetails();
            fillDetails();

            StudentController objUCS = new StudentController();
            DataSet dsProgress = null;
            dsProgress = objUCS.GetStudentTrackRecord(Convert.ToInt32(ViewState["UserStudentUserno"]));
            if (dsProgress.Tables[0] != null && dsProgress.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(dsProgress.Tables[0].Rows[0]["APPLICATION_STATUS"]) == "1")
                {
                    iOnlineAdmSub.Attributes.Add("class", "fa fa-check");
                }
                else
                {
                    iOnlineAdmSub.Attributes.Add("class", "fa fa-times");
                }
                if (Convert.ToString(dsProgress.Tables[0].Rows[0]["PAY_SETAILS"]) == "1")
                {
                    iStudDocUpload.Attributes.Add("class", "fa fa-check");
                }
                else
                {
                    iStudDocUpload.Attributes.Add("class", "fa fa-times");
                }
                if (Convert.ToInt32(dsProgress.Tables[0].Rows[0]["MERITNO"]) > 0)
                {
                    iAdmSecAprvl.Attributes.Add("class", "fa fa-check");
                }
                else
                {
                    iAdmSecAprvl.Attributes.Add("class", "fa fa-times");
                }
                if (Convert.ToInt32(dsProgress.Tables[0].Rows[0]["IDNO"]) > 0)
                {
                    iFinSecAprvl.Attributes.Add("class", "fa fa-check");
                }
                else
                {
                    iFinSecAprvl.Attributes.Add("class", "fa fa-times");
                }
            }
            else
            {
                iOnlineAdmSub.Attributes.Remove("class");
                iStudDocUpload.Attributes.Remove("class");
                iAdmSecAprvl.Attributes.Remove("class");
                iFinSecAprvl.Attributes.Remove("class");

                iOnlineAdmSub.Attributes.Add("class", "fa fa-times");
                iStudDocUpload.Attributes.Add("class", "fa fa-times");
                iAdmSecAprvl.Attributes.Add("class", "fa fa-times");
                iFinSecAprvl.Attributes.Add("class", "fa fa-times");

                //iFeePayByStud.Attributes.Add("class", "fa fa-times");
            }
            dsProgress.Dispose();
            BindListViewApplication();
            BindData();
            ListViewDataBindProgra();
            string Degreeno = objCommon.LookUp("ACD_STUDENT", "ISNULL(DEGREENO,0)", "ISNULL(CAN,0)=0 AND ISNULL(ADMCAN,0)=0 AND USERNO=" + Convert.ToInt32(ViewState["UserStudentUserno"]));
            if (Degreeno.ToString() == string.Empty || Convert.ToInt32(Degreeno.ToString()) <= 0)
            {
                btnSubmitAllDetails.Enabled = true;
                btnSubmitAllDetails.ToolTip = "";
            }
            else
            {
                btnSubmitAllDetails.Enabled = false;
                btnSubmitAllDetails.ToolTip = "Offer Accepted";
            }
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
        }
        catch (Exception ex)
        {

        }
    }
    // --------------------------------- Personal Details Start ----------------------------------------//
    protected void FillPersonalDetailsDropDown()
    {
        try
        {
            txtPermAddress.Attributes.Add("maxlength", txtPermAddress.MaxLength.ToString());
            objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "DISTINCT BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlOnlineMobileCode, "ACD_COUNTRY", "DISTINCT COUNTRYNO", "(MOBILE_CODE + ' - ' + COUNTRYNAME)COUNTRYNAME", "COUNTRYNO > 0", "COUNTRYNO");
            objCommon.FillDropDownList(ddlHomeMobileCode, "ACD_COUNTRY", "DISTINCT COUNTRYNO", "(MOBILE_CODE + ' - ' + COUNTRYNAME)COUNTRYNAME", "COUNTRYNO > 0", "COUNTRYNO");
            objCommon.FillDropDownList(ddlPCon, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO>0", "COUNTRYNAME");
            // objCommon.FillDropDownList(ddlPermanentState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
            objCommon.FillDropDownList(ddlbank, "ACD_BANK", "DISTINCT BANKNO", "BANKNAME", "BANKNO > 0", "BANKNO");
            objCommon.FillDropDownList(ddlLeadStatus, "ACD_LEAD_STAGE", "LEADNO", "LEAD_STAGE_NAME", "LEADNO > 0", "LEADNO");
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlPermanentState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPermanentState.SelectedValue != "0")
            {
                objCommon.FillDropDownList(ddlPTahsil, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 AND ACTIVESTATUS = 1 and STATENO=" + ddlPermanentState.SelectedValue, "CITY");
                // objCommon.FillDropDownList(ddlPTahsil, "ACD_DISTRICT", "DISTINCT DISTRICTNO", "DISTRICTNAME", "STATENO=" + ddlPermanentState.SelectedValue, "DISTRICTNO");
            }
            else
            {
                ddlPTahsil.SelectedValue = "0";
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void BindPersonalDetails()
    {
        try
        {
            FillPersonalDetailsDropDown();
            DataSet ds = objnuc.GetStudPersonalDetails(Convert.ToInt32(ViewState["UserStudentUserno"]));
            txtDateOfBirth.Text = "";
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                txtMiddleName.Text = ds.Tables[0].Rows[0]["MIDDLENAME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["MIDDLENAME"].ToString();
                txtRemark.Text = ds.Tables[0].Rows[0]["LEAD_REMARK"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["LEAD_REMARK"].ToString();
                txtApplicatio.Text = ds.Tables[0].Rows[0]["USERNAME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["USERNAME"].ToString();
                //txtIntake.Text = ds.Tables[0].Rows[0]["BATCHNAME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["BATCHNAME"].ToString();
                ddlIntake.SelectedValue = ds.Tables[0].Rows[0]["ADMBATCH"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
                Session["DdlIntakeValue"] = ds.Tables[0].Rows[0]["ADMBATCH"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
                // txtNameInitial.Text = ds.Tables[0].Rows[0]["NAME_INITIAL"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["NAME_INITIAL"].ToString();
                txtFirstName.Text = ds.Tables[0].Rows[0]["FIRSTNAME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["FIRSTNAME"].ToString();
                txtFatherName.Text = ds.Tables[0].Rows[0]["FATHERNAME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["FATHERNAME"].ToString();
                txtMothersName.Text = ds.Tables[0].Rows[0]["MOTHERNAME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                txtPerlastname.Text = ds.Tables[0].Rows[0]["LASTNAME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["LASTNAME"].ToString();
                rdbQuestion.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["CITIZEN_NO"]) == null ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["CITIZEN_NO"]);

                if (Convert.ToString(ds.Tables[0].Rows[0]["DOB"]) != string.Empty)
                {
                    txtDateOfBirth.Text = Convert.ToString(Convert.ToDateTime(ds.Tables[0].Rows[0]["DOB"].ToString()));
                }
                //if (Convert.ToString(ds.Tables[0].Rows[0]["LEFT_RIGHT_HANDED"]) != "0")
                //{
                //    rdbLeftRight.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["LEFT_RIGHT_HANDED"]) == null ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["LEFT_RIGHT_HANDED"]);
                //}
                //txtDateOfBirth.Text = ds.Tables[0].Rows[0]["DOB"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["DOB"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["EMAILID"].ToString();
                txtPersonalPassprtNo.Text = ds.Tables[0].Rows[0]["PASSPORTNO"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["PASSPORTNO"].ToString();
                txtOnlineNIC.Text = ds.Tables[0].Rows[0]["NIC"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["NIC"].ToString();
                rdPersonalGender.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["GENDER"]) == null ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["GENDER"]);
                if (ds.Tables[0].Rows[0]["MOBILECODE"].ToString() != string.Empty)
                    ddlOnlineMobileCode.SelectedValue = ds.Tables[0].Rows[0]["MOBILECODE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["MOBILECODE"].ToString();
                txtMobile.Text = ds.Tables[0].Rows[0]["MOBILE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["MOBILE"].ToString();
                TxtACRNo.Text = ds.Tables[0].Rows[0]["ACRNO"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["ACRNO"].ToString();
                txtACRDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["ACR_DATE_ISSUE"].ToString());
                txtHomeTel.Text = ds.Tables[0].Rows[0]["HOME_MOBILENO"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["HOME_MOBILENO"].ToString();
                //if (ds.Tables[0].Rows[0]["HOMETELCODE"].ToString() != string.Empty)
                //    ddlHomeMobileCode.SelectedValue = ds.Tables[0].Rows[0]["HOMETELCODE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["HOMETELCODE"].ToString();
            }
            else
            {
                Session["DdlIntakeValue"] = null;
            }
            DataSet ds1 = objnuc.GetAddressDetails(Convert.ToInt32(ViewState["UserStudentUserno"]));
            if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {
                txtPermAddress.Text = ds1.Tables[0].Rows[0]["PADDRESS"].ToString();
                ddlPCon.SelectedValue = ds1.Tables[0].Rows[0]["PCOUNTRY"].ToString();
                ddlPCon_SelectedIndexChanged(new object(), new EventArgs());
                ddlPermanentState.SelectedValue = ds1.Tables[0].Rows[0]["PPROVINCE"].ToString();
                //objCommon.FillDropDownList(ddlPTahsil, "ACD_DISTRICT", "DISTINCT DISTRICTNO", "DISTRICTNAME", "STATENO=" + ddlPermanentState.SelectedValue, "DISTRICTNO");
                ddlPermanentState_SelectedIndexChanged(new object(), new EventArgs());
                ddlPTahsil.SelectedValue = ds1.Tables[0].Rows[0]["PCITY"] == DBNull.Value ? string.Empty : ds1.Tables[0].Rows[0]["PCITY"].ToString();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PersonalandBankdetails.BindData-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // --------------------------------- Personal Details End ----------------------------------------//

    // --------------------------------- UG Education Details Start -----------------------------------//

    protected void ListViewDataBind()
    {
        int UserNo = Convert.ToInt32(ViewState["UserStudentUserno"]);
        int COUNT = Convert.ToInt32(objCommon.LookUp("ACD_USER_LAST_QUALIFICATION", "COUNT(USERNO)", "USERNO=" + UserNo));
        if (COUNT == 0)
        {
            DataSet ds = objCommon.FillDropDown("ACD_UA_SECTION", "*,'' as SCHOOL_NAME,'' as SCHOOL_ADDRESS,'' as SCHOOL_REGION,'' as YEAR_ATTENDED,'' as SCHOOL_TYPE_NO", "UA_SECTIONNAME", "UA_SECTION>" + Convert.ToInt32(ddlProgramTypes.SelectedValue), "UA_SECTION desc");
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
            DataSet ds = objCommon.FillDropDown("ACD_UA_SECTION S INNER JOIN ACD_USER_LAST_QUALIFICATION UL ON (S.UA_SECTION=UL.UGPGOT)", "UA_SECTION", "UA_SECTIONNAME,SCHOOL_NAME,SCHOOL_ADDRESS,SCHOOL_REGION,YEAR_ATTENDED,SCHOOL_TYPE,SCHOOL_TYPE_NO", "UA_SECTION>" + Convert.ToInt32(ddlProgramTypes.SelectedValue) + "AND USERNO=" + UserNo, "UA_SECTION desc");
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

    // --------------------------------- UG Education Details End -----------------------------------//

    // --------------------------------- PG Education Details Start ---------------------------------//
    private void BindlistViewPG(int USERNO)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_USER_LAST_QUALIFICATION AL", "AL.STLQNO", "*", "USERNO=" + USERNO + "", "AL.STLQNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //txtHighestEducationPG.Text = ds.Tables[0].Rows[0]["HIGHESTEDUCATION"].ToString();
                //txtUniversityPG.Text = ds.Tables[0].Rows[0]["UNIVERSITYINSTITUTE"].ToString();
                //txtQualificationAwardPG.Text = ds.Tables[0].Rows[0]["QUALAWARDYEAR"].ToString();
                //txtSpecializationPG.Text = ds.Tables[0].Rows[0]["SPECILIZOFQUAL"].ToString();
                //txtGPAPG.Text = ds.Tables[0].Rows[0]["GPAOFQUAL"].ToString();
                //txtProfessionalPG.Text = ds.Tables[0].Rows[0]["PROFQUAL"].ToString();
                //txtProfessionalUniversityPG.Text = ds.Tables[0].Rows[0]["PROFUNIVINSTIT"].ToString();
                //txtAwardDatePG.Text = ds.Tables[0].Rows[0]["QUALAWARDOFDATE"].ToString();
                //txtSpecilizationQualificationPG.Text = ds.Tables[0].Rows[0]["SPECILOFQUAL"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EducationDetailsPG.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private DataTable GetEmploymentHistoryDataTablePG()
    {
        DataTable dt = new DataTable();

        try
        {
            dt.Columns.Add(new DataColumn("SRNO", typeof(string)));
            dt.Columns.Add(new DataColumn("USERNO", typeof(int)));
            dt.Columns.Add(new DataColumn("DURATION", typeof(string)));
            dt.Columns.Add(new DataColumn("START_DURATION", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("END_DURATION", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("POSITION", typeof(string)));
            dt.Columns.Add(new DataColumn("DETAILS", typeof(string)));
        }
        catch (Exception ex)
        {

        }
        return dt;
    }





    private DataTable GetRefereesDataTablePG()
    {
        DataTable dt = new DataTable();
        try
        {
            dt.Columns.Add(new DataColumn("SRNO", typeof(string)));
            dt.Columns.Add(new DataColumn("USERNO", typeof(int)));
            dt.Columns.Add(new DataColumn("NAME", typeof(string)));
            dt.Columns.Add(new DataColumn("POSITION", typeof(string)));
            dt.Columns.Add(new DataColumn("ADDRESS", typeof(string)));
            dt.Columns.Add(new DataColumn("CONTACT", typeof(string)));
        }
        catch (Exception ex)
        {

        }
        return dt;
    }
    protected void BindUserDataPG()
    {
        try
        {
            DataSet ds = null;
            ds = objnuc.getPHDEdcucationalDetails(Convert.ToInt32(ViewState["UserStudentUserno"]));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                //lvRefreesPG.DataSource = ds.Tables[0];
                //lvRefreesPG.DataBind();
                Session.Add("Referees_PG", ds.Tables[0]);
            }
            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                //    lvEmploymentHistoryPG.DataSource = ds.Tables[1];
                //    lvEmploymentHistoryPG.DataBind();
                Session.Add("Employment_History_PG", ds.Tables[1]);
            }
        }
        catch (Exception ex)
        {

        }
    }
    // --------------------------------- PG Education Details End ----------------------------------//

    // --------------------------------- PHD Education Details Start -------------------------------//

    private DataTable GetEmploymentHistoryDataTable()
    {
        DataTable dt = new DataTable();
        try
        {
            dt.Columns.Add(new DataColumn("SRNO", typeof(string)));
            dt.Columns.Add(new DataColumn("USERNO", typeof(int)));
            dt.Columns.Add(new DataColumn("DURATION", typeof(string)));
            dt.Columns.Add(new DataColumn("START_DURATION", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("END_DURATION", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("POSITION", typeof(string)));
            dt.Columns.Add(new DataColumn("DETAILS", typeof(string)));
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    private DataTable GetRefereesDataTable()
    {
        DataTable dt = new DataTable();
        try
        {
            dt.Columns.Add(new DataColumn("SRNO", typeof(string)));
            dt.Columns.Add(new DataColumn("USERNO", typeof(int)));
            dt.Columns.Add(new DataColumn("NAME", typeof(string)));
            dt.Columns.Add(new DataColumn("POSITION", typeof(string)));
            dt.Columns.Add(new DataColumn("ADDRESS", typeof(string)));
            dt.Columns.Add(new DataColumn("CONTACT", typeof(string)));
        }
        catch (Exception ex) { }
        return dt;
    }

    // ---------------------------------- PHD Education Details End ---------------------------------//

    // ---------------------------------- Apply Program Strat ---------------------------------------//
    protected void ListViewDataBindProgra()
    {
        string PREF1 = "0", PREF2 = "0", PREF3="0";
        DataSet ds = objBC.Getdetailsofbranch(Convert.ToInt32(ddlProgramTypes.SelectedValue), (Convert.ToInt32(ViewState["UserStudentUserno"])), Convert.ToInt32(0), Convert.ToInt32(0), Convert.ToInt32(0));
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            ddlPrefrence1.Items.Clear();
            ddlPrefrence1.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlPrefrence1.DataSource = ds.Tables[0];
            ddlPrefrence1.DataValueField = ds.Tables[0].Columns["PREFERENCESNO"].ToString();
            ddlPrefrence1.DataTextField = ds.Tables[0].Columns["PREFERENCES"].ToString();
            ddlPrefrence1.DataBind();

            string STATUS = ds.Tables[3].Rows[0]["STATUS"].ToString();

            if (STATUS != "0")
            {
                divPreference1.Style.Add("pointer-events", "none");
                divPreference2.Style.Add("pointer-events", "none");
                divPreference3.Style.Add("pointer-events", "none");
            }
            else
            {
                divPreference1.Style.Add("pointer-events", "block");
                divPreference2.Style.Add("pointer-events", "block");
                divPreference3.Style.Add("pointer-events", "block");
            }

        }
         PREF1 = (objCommon.LookUp("ACD_USER_BRANCH_PREF", "isnull((CONVERT(NVARCHAR(500),COLLEGE_ID)+ ',' + CONVERT(NVARCHAR(500),AREA_INT_NO) + ','+CONVERT(NVARCHAR(500),DEGREENO) + ',' + CONVERT(NVARCHAR(500),BRANCHNO)+ ',' + CONVERT(NVARCHAR(500),CAMPUSNO)+ ',' + CONVERT(NVARCHAR(500),ADMBATCH)) ,0) as PREFERENCESNO", "USERNO=" + Convert.ToInt32(ViewState["UserStudentUserno"]) + "AND BRANCH_PREF=1"));
         PREF2 = (objCommon.LookUp("ACD_USER_BRANCH_PREF", "isnull((CONVERT(NVARCHAR(500),COLLEGE_ID)+ ',' + CONVERT(NVARCHAR(500),AREA_INT_NO) + ','+CONVERT(NVARCHAR(500),DEGREENO) + ',' + CONVERT(NVARCHAR(500),BRANCHNO)+ ',' + CONVERT(NVARCHAR(500),CAMPUSNO)+ ',' + CONVERT(NVARCHAR(500),ADMBATCH)),0) as PREFERENCESNO", "USERNO=" + Convert.ToInt32(ViewState["UserStudentUserno"]) + "AND BRANCH_PREF=2"));
         PREF3 = (objCommon.LookUp("ACD_USER_BRANCH_PREF", "isnull((CONVERT(NVARCHAR(500),COLLEGE_ID)+ ',' + CONVERT(NVARCHAR(500),AREA_INT_NO) + ','+CONVERT(NVARCHAR(500),DEGREENO) + ',' + CONVERT(NVARCHAR(500),BRANCHNO)+ ',' + CONVERT(NVARCHAR(500),CAMPUSNO)+ ',' + CONVERT(NVARCHAR(500),ADMBATCH)),0) as PREFERENCESNO", "USERNO=" + Convert.ToInt32(ViewState["UserStudentUserno"]) + "AND BRANCH_PREF=3"));
        if (PREF1.ToString() != string.Empty)
        {
            ddlPrefrence1.SelectedValue = PREF1.ToString();
            ddlPrefrence1_SelectedIndexChanged(new object(), new EventArgs());
        }
        if (PREF2.ToString() != string.Empty)
        {
            ddlPrefrence2.SelectedValue = PREF2.ToString();
            ddlPrefrence2_SelectedIndexChanged(new object(), new EventArgs());
        }
        if (PREF3.ToString() != string.Empty)
        {
            ddlPrefrence3.SelectedValue = PREF3.ToString();
        }

    }

    protected void ddlPrefrence1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPrefrence2.Items.Clear();
        ddlPrefrence2.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlPrefrence3.Items.Clear();
        ddlPrefrence3.Items.Insert(0, new ListItem("Please Select", "0"));
        if (ddlPrefrence1.SelectedValue != "0")
        {
            string SP_Name2 = "PKG_ACD_GET_UGPG_DATA_USA_FORPROGRAM";
            string SP_Parameters2 = "@P_UGPG,@P_PREFERENCESNO,@P_COMMANDBYPE";
            string temp2 = ddlPrefrence1.SelectedValue.Replace(",", "$");
            string Call_Values2 = "" + Convert.ToInt32(ddlProgramTypes.SelectedValue) + "," + temp2 + "," + 1 + "";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPrefrence2.Items.Clear();
                ddlPrefrence2.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlPrefrence2.DataSource = ds.Tables[0];
                ddlPrefrence2.DataValueField = ds.Tables[0].Columns["PREFERENCESNO"].ToString();
                ddlPrefrence2.DataTextField = ds.Tables[0].Columns["PREFERENCES"].ToString();
                ddlPrefrence2.DataBind();
            }
        }
        else
        {
            ddlPrefrence3.Items.Clear();
            ddlPrefrence3.Items.Insert(0, new ListItem("Please Select", "0"));
        }

    }
    protected void ddlPrefrence2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPrefrence3.Items.Clear();
        ddlPrefrence3.Items.Insert(0, new ListItem("Please Select", "0"));
        if (ddlPrefrence2.SelectedValue != "0")
        {
            string SP_Name2 = "PKG_ACD_GET_UGPG_DATA_USA_FORPROGRAM";
            string SP_Parameters2 = "@P_UGPG,@P_PREFERENCESNO,@P_COMMANDBYPE";
            string temp2 = ddlPrefrence2.SelectedValue.Replace(",", "$") + "#" + ddlPrefrence1.SelectedValue.Replace(",", "$");

            string Call_Values2 = "" + Convert.ToInt32(ddlProgramTypes.SelectedValue) + "," + temp2 + "," + 0 + "";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPrefrence3.Items.Clear();
                ddlPrefrence3.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlPrefrence3.DataSource = ds.Tables[0];
                ddlPrefrence3.DataValueField = ds.Tables[0].Columns["PREFERENCESNO"].ToString();
                ddlPrefrence3.DataTextField = ds.Tables[0].Columns["PREFERENCES"].ToString();
                ddlPrefrence3.DataBind();
            }
        }
        else
        {
            ddlPrefrence3.Items.Clear();
            ddlPrefrence3.Items.Insert(0, new ListItem("Please Select", "0"));
        }
    }



    // ---------------------------------- Apply Program End -----------------------------------------//

    // ---------------------------------- Campus Details Start --------------------------------------//

    public void FillDropDownOnline()
    {
        try
        {
            DataSet ds = null;

            ds = objBC.DynamicFillDropDownListsandBindDetails(Convert.ToInt32(ViewState["UserStudentUserno"]), 1);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCampusDetails.Items.Clear();
                ddlCampusDetails.Items.Add("Please Select");
                ddlCampusDetails.SelectedItem.Value = "0";

                //ddlWeekDays.Items.Clear();
                //ddlWeekDays.Items.Add("Please Select");
                //ddlWeekDays.SelectedItem.Value = "0";

                ddlApptituteCenter.Items.Clear();
                ddlApptituteCenter.Items.Add("Please Select");
                ddlApptituteCenter.SelectedItem.Value = "0";

                ddlMode.Items.Clear();
                ddlMode.Items.Add("Please Select");
                ddlMode.SelectedItem.Value = "0";

                ddlApptituteMedium.Items.Clear();
                ddlApptituteMedium.Items.Add("Please Select");
                ddlApptituteMedium.SelectedItem.Value = "0";


                ddlCampusDetails.DataSource = ds.Tables[0];
                ddlCampusDetails.DataValueField = "CAMPUSNO";
                ddlCampusDetails.DataTextField = "CAMPUSNAME";
                ddlCampusDetails.DataBind();
                ddlCampusDetails.SelectedIndex = 0;

                //ddlWeekDays.DataSource = ds.Tables[1];
                //ddlWeekDays.DataValueField = "WEEKNO";
                //ddlWeekDays.DataTextField = "WEEKDAYSNAME";
                //ddlWeekDays.DataBind();
                //ddlWeekDays.SelectedIndex = 0;

                ddlApptituteCenter.DataSource = ds.Tables[2];
                ddlApptituteCenter.DataValueField = "APTITUDE_CENTER_NO";
                ddlApptituteCenter.DataTextField = "APTITUDE_CENTER_NAME";
                ddlApptituteCenter.DataBind();
                ddlApptituteCenter.SelectedIndex = 0;

                ddlMode.DataSource = ds.Tables[3];
                ddlMode.DataValueField = "SRNO";
                ddlMode.DataTextField = "MODE_NAME";
                ddlMode.DataBind();
                ddlMode.SelectedIndex = 0;

                ddlApptituteMedium.DataSource = ds.Tables[4];
                ddlApptituteMedium.DataValueField = "SRNO";
                ddlApptituteMedium.DataTextField = "MEDIUM_NAME";
                ddlApptituteMedium.DataBind();
                ddlApptituteMedium.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "domain.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void FillSaveDetails()
    {
        try
        {
            int UserNo = Convert.ToInt32(ViewState["UserStudentUserno"]);
            DataSet DSS = objCommon.FillDropDown("ACD_USER_REGISTRATION", "DISTINCT ADMBATCH", "UGPGOT", "USERNO=" + UserNo, "");

            objCommon.FillDropDownList(ddlExamDate, "ACD_ADMISSION_TEST_SCHEDULING A", "DISTINCT EXAMDATE_NO", "EXAM_DATE", "ACTIVE=1 AND ADMBATCH_NO=" + Convert.ToString(DSS.Tables[0].Rows[0]["ADMBATCH"]) + " AND UG_PG=" + Convert.ToString(DSS.Tables[0].Rows[0]["UGPGOT"]) + "AND CAPACITY<>(SELECT COUNT(EXAMDATE_NO) FROM ACD_APTITUDE_EXAM_CENTER B WHERE B.EXAMDATE_NO=A.EXAMDATE_NO )", "A.EXAMDATE_NO");
            DataSet ds = null;
            ds = objBC.DynamicFillDropDownListsandBindDetails(Convert.ToInt32(ViewState["UserStudentUserno"]), 2);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCampusDetails.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["CAMPUSNO"]) == string.Empty ? "0" : Convert.ToString(ds.Tables[0].Rows[0]["CAMPUSNO"]);
                // ddlWeekDays.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["WEEKDAYNO"].ToString()) == string.Empty ? "0" : Convert.ToString(ds.Tables[0].Rows[0]["WEEKDAYNO"].ToString());
                ddlApptituteCenter.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["APTITUDE_CENTER_NO"].ToString()) == string.Empty ? "0" : Convert.ToString(ds.Tables[0].Rows[0]["APTITUDE_CENTER_NO"].ToString());
                ddlMode.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["APTITUDE_MODE_NO"].ToString()) == string.Empty ? "0" : Convert.ToString(ds.Tables[0].Rows[0]["APTITUDE_MODE_NO"].ToString());
                ddlExamDate.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["EXAMDATE_NO"].ToString()) == string.Empty ? "0" : Convert.ToString(ds.Tables[0].Rows[0]["EXAMDATE_NO"].ToString());
                ddlExamDate_SelectedIndexChanged(new object(), new EventArgs());
                ddlTimeSlot.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["SCHEDULING_NO"].ToString()) == string.Empty ? "0" : Convert.ToString(ds.Tables[0].Rows[0]["SCHEDULING_NO"].ToString());
                ddlTimeSlot_SelectedIndexChanged(new object(), new EventArgs());
                ddlVenue.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["VENUE_NO"].ToString()) == string.Empty ? "0" : Convert.ToString(ds.Tables[0].Rows[0]["VENUE_NO"].ToString());
                rdoSpecialNeeds.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["SPECIAL_NEED"].ToString()) == string.Empty ? "0" : Convert.ToString(ds.Tables[0].Rows[0]["SPECIAL_NEED"].ToString());
                txtYes.Text = Convert.ToString(ds.Tables[0].Rows[0]["REASON"].ToString()) == string.Empty ? "0" : Convert.ToString(ds.Tables[0].Rows[0]["REASON"].ToString());
                rdoSpecialNeeds_SelectedIndexChanged(new object(), new EventArgs());

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void AppliedProgramDetails()
    {
        try
        {
            DataSet ds = null;
            lblExamcenter.Text = "";
            ds = objBC.getExamEnterDate(Convert.ToInt32(ViewState["UserStudentUserno"]));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lblExamcenter.Text += "<i class='fa fa-bullseye' style='color:black;'></i>  " + ds.Tables[0].Rows[i]["DEGREENAME"].ToString() + "   Exam Date :- " + ds.Tables[0].Rows[i]["ADMEXAMDATE"].ToString() + "<br>";
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void rdoSpecialNeeds_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoSpecialNeeds.SelectedValue == "0")
        {
            DivSpecial.Visible = true;
        }
        else
        {
            DivSpecial.Visible = false;
            txtYes.Text = "";
        }
    }

    protected void ddlExamDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTimeSlot.Items.Clear();
        ddlTimeSlot.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlVenue.Items.Clear();
        ddlVenue.Items.Insert(0, new ListItem("Please Select", "0"));
        int UserNo = Convert.ToInt32(ViewState["UserStudentUserno"]);
        DataSet DSS = objCommon.FillDropDown("ACD_USER_REGISTRATION", "DISTINCT ADMBATCH", "UGPGOT", "USERNO=" + UserNo, "");
        objCommon.FillDropDownList(ddlTimeSlot, "ACD_ADMISSION_TEST_SCHEDULING", "DISTINCT SCHEDULING_NO", "(TIME_FROM + '-' + TIME_TO)TIME_SLOT", "ACTIVE=1 AND EXAMDATE_NO=" + Convert.ToInt32(ddlExamDate.SelectedValue) + "and ADMBATCH_NO=" + Convert.ToString(DSS.Tables[0].Rows[0]["ADMBATCH"]) + "AND UG_PG=" + Convert.ToString(DSS.Tables[0].Rows[0]["UGPGOT"]), "");
    }
    protected void ddlTimeSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
        int UserNo = Convert.ToInt32(ViewState["UserStudentUserno"]);
        DataSet DSS = objCommon.FillDropDown("ACD_USER_REGISTRATION", "DISTINCT ADMBATCH", "UGPGOT", "USERNO=" + UserNo, "");
        objCommon.FillDropDownList(ddlVenue, "ACD_ADMISSION_TEST_SCHEDULING", "DISTINCT VENUE_NO", "VENUE_NAME", "ACTIVE=1 AND SCHEDULING_NO=" + Convert.ToInt32(ddlTimeSlot.SelectedValue) + "and ADMBATCH_NO=" + Convert.ToString(DSS.Tables[0].Rows[0]["ADMBATCH"]) + "AND UG_PG=" + Convert.ToString(DSS.Tables[0].Rows[0]["UGPGOT"]), "");
    }
    // ---------------------------------- Campus Details End ----------------------------------------//

    // --------------------------------- Payment Details Start-------------------------------------------//

    private void CreateCustomerRefs()
    {
        Random rnd = new Random();
        int ir = rnd.Next(01, 10000);
        Random rnd1 = new Random();
        int ir1 = rnd1.Next(01, 99999);
        lblOrderID.Text = Convert.ToString(Convert.ToInt32(Session["USERNO"]) + Convert.ToString(ir1) + Convert.ToString(ir));
        Session["ORDERIDRESPONSE"] = lblOrderID.Text;
    }
    private void SubmitData()
    {
        try
        {

            Random random = new Random();
            //  string Order_id = hdfIdno.Value + (Convert.ToString(random.Next(01, 10000)));

            CreateCustomerRefs();

            string session = objCommon.LookUp("ACD_ADMISSION_CONFIG", "MAX(ADMBATCH)", string.Empty);
            int PAYMENTTYPE = 0;

            objStudentFees.UserNo = Convert.ToInt32(ViewState["UserStudentUserno"]);
            objStudentFees.SessionNo = session;
            objStudentFees.OrderID = lblOrderID.Text;
            objStudentFees.Amount = Convert.ToDouble(hdfAmount.Value);
            objStudentFees.FeeType = "Online";
            PAYMENTTYPE = 2;

            // Additional Parameters
            objStudentFees.TransDate = System.DateTime.Today;
            objStudentFees.Bankno = 0;
            objStudentFees.BranchName = "";
            objStudentFees.Ddno = 0;
            objStudentFees.Catapplied = false;
            objStudentFees.Unitno = 0;
            objStudentFees.TransID = "";

            int result = 0;

            result = ObjNucFees.SubmitFeesofStudent(objStudentFees, PAYMENTTYPE, 2);   //PAYMENTTYPE  FOR ONLINE , GATEWAYID 2 FOR ONLINE

            if (result > 0)
            {

                DataSet ds = null;
                ds = objnuc.GetOnlineTrasactionOnlineOrderID((ViewState["UserStudentUserno"].ToString()), Convert.ToString(lblOrderID.Text));





                if (ds.Tables[0].Rows.Count > 0)
                {

                    if (Convert.ToString(ds.Tables[0].Rows[0]["ORDER_ID"]) == lblOrderID.Text)
                    {
                        hdfServiceCharge.Value = Convert.ToString(ds.Tables[0].Rows[0]["SERVICE_CHARGE"]);
                        txtOrderid.Text = lblOrderID.Text;
                        txtAmountPaid.Text = hdfAmount.Value;
                        txtServiceCharge.Text = hdfServiceCharge.Value;
                        decimal text = Convert.ToDecimal(hdfAmount.Value) + Convert.ToDecimal(hdfServiceCharge.Value);
                        txtTotalPayAmount.Text = Convert.ToString(text);
                        hdfTotalAmount.Value = Convert.ToString(text);
                        SendTransaction();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#myModal33').modal('show')", true);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Something went wrong , Please try again !", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Failed To Done Online Payment.", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.SubmitData-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnGenerateChallan_Click(object sender, EventArgs e)
    {
        try
        {
            string session = objCommon.LookUp("ACD_ADMISSION_CONFIG", "MAX(ADMBATCH)", string.Empty);
            int PAYMENTTYPE = 0;
            if (rdPaymentOption.SelectedValue == "0")
            {
                objStudentFees.UserNo = Convert.ToInt32(ViewState["UserStudentUserno"]);
                objStudentFees.SessionNo = session;
                objStudentFees.OrderID = lblOrderID.Text;
                objStudentFees.Amount = Convert.ToDouble(hdfAmount.Value);
                objStudentFees.FeeType = "Offonline";
                PAYMENTTYPE = 1;

                // Additional Parameters
                objStudentFees.TransDate = System.DateTime.Today;
                objStudentFees.Bankno = 0;
                objStudentFees.BranchName = "";
                objStudentFees.Ddno = 0;
                objStudentFees.Catapplied = false;
                objStudentFees.Unitno = 0;
                objStudentFees.TransID = "";
            }
            int result = 0;

            result = ObjNucFees.SubmitFeesofStudent(objStudentFees, PAYMENTTYPE, 1);
            if (result > 0)
            {

                //objCommon.DisplayMessage(this.Page, "Challan Generated Successfully !!!", this.Page);
                //btnChallan.Visible = true;
                fillDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#modelBank').modal('show')", true);
                if (rdPaymentOption.SelectedValue == "0")
                {
                    btnPayment.Style.Add("display", "none");

                }
                else
                {
                    btnPayment.Visible = true;
                }


                //this.ShowReportNew("PaymentReceipt.rpt", ((UserDetails)(Session["user"])).UserNo);
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void fillDetails()
    {
        try
        {
            FeeCollectionController objFeeCont = new FeeCollectionController();
            DataSet ds = null;
            ds = objFeeCont.getPaymentDetails(Convert.ToInt32(ViewState["UserStudentUserno"]));
            if (Convert.ToString(Session["PaymentStatus"]) == "1")
            {
                if (ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
                {
                    if (Convert.ToInt32(ds.Tables[5].Rows[0]["COUNT_MERIT"]) > 0)
                    {
                        btnChallan.Visible = true;
                    }
                    else
                    {
                        btnChallan.Visible = false;
                    }
                }
                else
                {
                    btnChallan.Visible = false;
                }
            }

            if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
            {
                lvBankDetails.DataSource = ds.Tables[2];
                lvBankDetails.DataBind();
            }
            else
            {
                lvBankDetails.DataSource = null;
                lvBankDetails.DataBind();
            }
            if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
            {
                lvDepositSlip.DataSource = ds.Tables[3];
                lvDepositSlip.DataBind();
                /////divOfflineNote.Visible = true;
                //var listItem = rdPaymentOption.Items.FindByValue("1");
                //listItem.Attributes.Add("class", "disable");
                //btnPayment.Visible = false;
                if (Convert.ToString(Session["PaymentStatus"]) == "1")
                {
                    //divOfflineNote.Visible = false;
                    //divOfflinePaymentdone.Visible = true;
                }
                else
                {
                    //divOfflineNote.Visible = true;
                    //divOfflinePaymentdone.Visible = false;
                }
            }
            else
            {
                lvDepositSlip.DataSource = null;
                lvDepositSlip.DataBind();
                divOfflineNote.Visible = false;
                var listItem = rdPaymentOption.Items.FindByValue("1");
                //listItem.Attributes.Remove("class");
                btnPayment.Visible = true;
            }
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                txtUserName.Text = Convert.ToString(ds.Tables[0].Rows[0]["USERNAME"]);
                txtEmail.Text = Convert.ToString(ds.Tables[0].Rows[0]["EMAILID"]);
                txtmobilecode.Text = Convert.ToString(ds.Tables[0].Rows[0]["MOBILECODE"]);
                txtMobile.Text = Convert.ToString(ds.Tables[0].Rows[0]["MOBILENO"]);
                txtAmount.Text = Convert.ToString(ds.Tables[0].Rows[0]["FEES"]);
                hdfAmount.Value = Convert.ToString(ds.Tables[0].Rows[0]["FEES"]);
                txtchallanAmount.Text = Convert.ToString(ds.Tables[0].Rows[0]["FEES"]);
                //if (Convert.ToString(ds.Tables[0].Rows[0]["PAY_LATER"]) == "1")
                //{
                //    btnSavePayLater.Visible = false;
                //}
                //else
                //{
                //    btnSavePayLater.Visible = true;
                //}
            }
            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                lvPaymentDetails.DataSource = ds.Tables[1];
                lvPaymentDetails.DataBind();
                string[] degreenames1; string[] degreenames2; int count = 0; int count1 = 0;
                degreenames1 = ds.Tables[1].Rows[0]["DEGREE_NAMES"].ToString().Split(',');
                degreenames2 = ds.Tables[1].Rows[0]["DEGREE_NAMES1"].ToString().Split(',');
                foreach (ListViewDataItem lvHead in lvPaymentDetails.Items)
                {
                    Label lblDegrees = lvHead.FindControl("lblDegreeNames") as Label;
                    Label lblAmount = lvHead.FindControl("lblAmount") as Label;

                    Label lblArchDegrees = lvHead.FindControl("lblArchDegrees") as Label;
                    Label lblArchAmount = lvHead.FindControl("lblArchAmount") as Label;
                    Label lblRemove = lvHead.FindControl("lblRemove") as Label;
                    Label lblRemoveArch = lvHead.FindControl("lblRemoveArch") as Label;

                    HiddenField hdfDegree1 = lvHead.FindControl("hdfRemoveDegree") as HiddenField;
                    HiddenField hdfArchDegree = lvHead.FindControl("hdfRemoveArch") as HiddenField;

                    if (txtAmount.Text == "" || txtAmount.Text == "0.00")
                    {
                        lblAmount.Text = Convert.ToString(ds.Tables[1].Rows[0]["AMOUNT2"]);

                    }
                    else
                    {
                        lblAmount.Text = txtAmount.Text;
                    }
                    //lblAmount.Text += Convert.ToString(ds.Tables[1].Rows[0]["AMOUNT1"]);
                    if (Convert.ToString(Session["PaymentStatus"]) == "1")
                    {
                        lblRemove.Text = "";
                        divPayment.Visible = true;
                        divOfflinePaymentdone.Visible = false;
                    }
                    else
                    {
                        lblRemove.Text = "<i class='fa fa-times-circle' style='color:red;'></i>  ";
                        divPayment.Visible = false;
                        //divOfflinePaymentdone.Visible = false;
                    }
                    for (int i = 0; i < degreenames1.Length; i++)
                    {
                        if (degreenames1[i] != string.Empty)
                        {
                            count++;
                            lblDegrees.Text += "<i class='fa fa-bullseye' style='color:black;'></i>  " + degreenames1[i] + "<br/>";
                            hdfDegree1.Value = Convert.ToString("1");
                            lblAmount.Text += "" + "<br/>";
                            lblRemove.Text += "" + "<br/>";
                        }

                    }
                    if (count == 0)
                    {
                        lblRemove.Text = "";
                    }
                    //lblArchAmount.Text = Convert.ToString(ds.Tables[1].Rows[0]["AMOUNT2"]);

                    for (int i = 0; i < degreenames2.Length; i++)
                    {
                        if (degreenames2[0] != string.Empty)
                        {
                            if (degreenames2[i] != string.Empty)
                            {
                                count1++;
                                lblArchDegrees.Text += "<i class='fa fa-bullseye' style='color:black;'></i>  " + degreenames2[i] + "<br/>";
                                lblArchAmount.Text += "" + "<br/>";
                                hdfArchDegree.Value = Convert.ToString("2");
                            }

                        }
                    }
                    if (count > 0 && count1 > 0)
                    {
                        lblRemove.Text += "<hr />";
                        lblDegrees.Text += "<hr />";
                        lblAmount.Text += "<hr />";
                    }
                    if (count1 > 0)
                    {
                        if (Convert.ToString(Session["PaymentStatus"]) == "1")
                        {
                            lblRemoveArch.Text = "";
                        }
                        else
                        {
                            lblRemoveArch.Text += "<i class='fa fa-times-circle' style='color:red;'></i>  ";
                        }
                    }
                    //if (Convert.ToString(ds.Tables[1].Rows[0]["MODE"]) == "0")
                    //{
                    //    btnChallan.Visible = false;
                    //}
                    //else if (Convert.ToString(ds.Tables[1].Rows[0]["MODE"]) == "1")
                    //{
                    //    btnChallan.Visible = true;
                    //}
                    //else if (Convert.ToString(ds.Tables[1].Rows[0]["MODE"]) == "2")
                    //{
                    //    btnChallan.Visible = false;
                    //}
                }
            }
            if (ds.Tables[7] != null && ds.Tables[7].Rows.Count > 0)
            {
                if (Convert.ToString(ds.Tables[7].Rows[0]["RECON"]) == "1")
                {
                    divPayment.Visible = true;
                    divOfflinePaymentdone.Visible = false;
                }
                else
                {
                    divPayment.Visible = false;
                    divOfflinePaymentdone.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void CreateCustomerRef()
    {
        try
        {
            Random rnd = new Random();
            int ir = rnd.Next(01, 10000);
            Random rnd1 = new Random();
            int ir1 = rnd1.Next(01, 99999);
            lblOrderIDERP.Text = Convert.ToString(Convert.ToInt32(Session["USERNO"]) + Convert.ToString(ir1) + Convert.ToString(ir));
            Session["ORDERIDRESPONSE"] = lblOrderIDERP.Text;
        }
        catch (Exception ex) { }
    }

    private void ShowReportNew(string rptName, int USERNO)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Payment_Details";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=@P_USERNO=" + Convert.ToInt32(ViewState["UserStudentUserno"]) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Default.ShowReportNew() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void SendTransaction()
    {
        try
        {
            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)768 | (System.Net.SecurityProtocolType)3072;
            String result = null;
            String gatewayCode = null;
            String response = null;

            // get the request form and make sure to UrlDecode each value in case special characters used
            NameValueCollection formdata = new NameValueCollection();
            //foreach (String key in Request.Form)
            //{
            //    formdata.Add(key, HttpUtility.UrlDecode(Request.Form[key]));
            //}

            Merchant merchant = new Merchant();

            // [Snippet] howToConfigureURL - start
            StringBuilder url = new StringBuilder();
            if (!merchant.GatewayHost.StartsWith("http"))
                url.Append("https://");
            url.Append(merchant.GatewayHost);
            //url.Append("/api/nvp/version/");
            //url.Append(merchant.Version);

            merchant.GatewayUrl = url.ToString();
            // [Snippet] howToConfigureURL - end

            Connection connection = new Connection(merchant);

            // [Snippet] howToConvertFormData -- start
            StringBuilder data = new StringBuilder();
            data.Append("merchant=" + merchant.MerchantId);
            data.Append("&apiUsername=" + merchant.Username);
            data.Append("&apiPassword=" + merchant.Password);


            formdata.Add("apiOperation", "CREATE_CHECKOUT_SESSION");

            formdata.Add("interaction.returnUrl", System.Configuration.ConfigurationManager.AppSettings["ReturnUrlAdmissionPortal"]);

            formdata.Add("interaction.operation", "PURCHASE");
            formdata.Add("order.id", lblOrderIDERP.Text);
            formdata.Add("order.currency", "LKR");
            formdata.Add("order.amount", hdfTotalAmount.Value);
            formdata.Add("order.description", Convert.ToString(ViewState["UserStudentUserno"]));

            foreach (string key in formdata)
            {
                data.Append("&" + key.ToString() + "=" + HttpUtility.UrlEncode(formdata[key], System.Text.Encoding.GetEncoding("ISO-8859-1")));
            }
            // [Snippet] howToConvertFormData -- end

            response = connection.SendTransaction(data.ToString());

            // [Snippet] howToParseResponse - start
            NameValueCollection respValues = new NameValueCollection();
            if (response != null && response.Length > 0)
            {
                String[] responses = response.Split('&');
                foreach (String responseField in responses)
                {
                    String[] field = responseField.Split('=');
                    respValues.Add(field[0], HttpUtility.UrlDecode(field[1]));
                }
            }
            // [Snippet] howToParseResponse - end

            result = respValues["result"];

            // Form error string if error is triggered
            if (result != null && result.Equals("ERROR"))
            {
                String errorMessage = null;
                String errorCode = null;

                String failureExplanations = respValues["explanation"];
                String supportCode = respValues["supportCode"];

                if (failureExplanations != null)
                {
                    errorMessage = failureExplanations;
                }
                else if (supportCode != null)
                {
                    errorMessage = supportCode;
                }
                else
                {
                    errorMessage = "Reason unspecified.";
                }

                String failureCode = respValues["failureCode"];
                if (failureCode != null)
                {
                    errorCode = "Error (" + failureCode + ")";
                }
                else
                {
                    errorCode = "Error (UNSPECIFIED)";
                }

                // now add the values to result fields in panels
                //lblErrorCode.Text = errorCode;
                //lblErrorMessage.Text = errorMessage;
                //pnlError.Visible = true;
            }

            // error or not display what response values can
            gatewayCode = respValues["response.gatewayCode"];
            if (gatewayCode == null)
            {
                gatewayCode = "Response not received.";
            }
            //lblGateWayCode.Text = gatewayCode;
            //lblResult.Text = result;

            // build table of NVP results and add to panel for results
            HtmlTable dTable = new HtmlTable();
            dTable.Width = "100%";
            dTable.CellPadding = 2;
            dTable.CellSpacing = 0;
            dTable.Border = 1;
            dTable.BorderColor = "#cccccc";

            int shade = 0;
            foreach (String key in respValues)
            {
                HtmlTableRow dtRow = new HtmlTableRow();
                if (++shade % 2 == 0) dtRow.Attributes.Add("class", "shade");

                HtmlTableCell dtLeft = new HtmlTableCell();
                HtmlTableCell dtRight = new HtmlTableCell();

                dtLeft.Align = "right";
                dtLeft.Width = "50%";
                dtLeft.InnerHtml = "<strong><i>" + key + ":</i></strong>";  // add field name to table
                if (key == "session.id")
                {
                    Session["PaymentSession"] = respValues[key];
                }
                dtRight.Width = "50%";
                dtRight.InnerText = respValues[key]; // add value to table

                dtRow.Controls.Add(dtLeft);
                dtRow.Controls.Add(dtRight);
                dTable.Controls.Add(dtRow);
            }
        }
        catch (Exception ex) { }
    }
    protected void btnPayment_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkConfirmPayment.Checked == true)
            {
                if (hdfAmount.Value.ToString() != "0.00")
                {
                    if (hdfAmount.Value.ToString() != "0")
                    {
                        if (hdfAmount.Value.ToString() != string.Empty)
                        {
                            SubmitData();
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "Amount is less than Zero Please Contact Online Admission Department", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Amount is less than Zero Please Contact Online Admission Department", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Amount is less than Zero Please Contact Online Admission Department", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please Confirm Information Before Payment", this.Page);
                return;
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnChallan_Click(object sender, EventArgs e)
    {
        this.ShowReportNew("OfferLetterBulk.rpt", Convert.ToInt32(ViewState["UserStudentUserno"]));
    }
    protected void btnApplicationForm_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("UserInfoFC.rpt");
        }
        catch (Exception ex)
        {

        }
    }
    private void ShowReport(string rptFileName)
    {
        try
        {
            string colgname = "SLIIT";// ViewState["College_Name"].ToString().Replace(" ", "_");

            string exporttype = "pdf";
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("")));
            url += "Reports/commonreport.aspx?";
            url += "exporttype=" + exporttype;

            url += "&filename=" + colgname + "_" + Convert.ToInt32(ViewState["UserStudentUserno"]) + "." + exporttype;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_USERNO=" + Convert.ToInt32(ViewState["UserStudentUserno"]) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            // url += "&param=@P_USERNO=" + ((UserDetails)(Session["user"])).UserNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Default2.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }
    public void DeleteIFExits(string FileName)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }
    public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU, byte[] ChallanCopy)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;

        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.Properties.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            if (!cblob.Exists())
            {
                using (Stream stream = new MemoryStream(ChallanCopy))
                {
                    cblob.UploadFromStream(stream);
                }
            }
            //cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }
    protected void btnChallanSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string UserNo = ViewState["UserStudentUserno"].ToString();
            FeeCollectionController objFeeCont = new FeeCollectionController();
            GenerateChallan();
            if (FuChallan.HasFile)
            {
                byte[] ChallanCopy = null;
                ChallanCopy = objCommon.GetImageData(FuChallan);
                string Ext = Path.GetExtension(FuChallan.FileName);
                CustomStatus cs = CustomStatus.Others;
                int retval = Blob_Upload(blob_ConStr, blob_ContainerName, Convert.ToInt32(ViewState["UserStudentUserno"]) + "_doc_" + "onlineadmission", FuChallan, ChallanCopy);
                if (retval == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                    var listItem = rdPaymentOption.Items.FindByValue("1");
                    listItem.Attributes.Add("class", "disable");
                    return;
                }
                cs = (CustomStatus)objFeeCont.InsertChallanCopyDetails(Convert.ToInt32(ViewState["UserStudentUserno"]), txtChallanId.Text, txtchallanAmount.Text, ChallanCopy, txtTransactionNo.Text, txtPaymentdate.Text, Convert.ToString(ddlbank.SelectedItem.Text), Convert.ToString(txtBranchName.Text), Convert.ToInt32(ViewState["UserStudentUserno"]) + "_doc_" + "onlineadmission" + Ext);

                //STARTED by aashna 12-01-2022
                DataSet dsUserContact = null;
                string message = "";
                string subject = "";
                string date = "";
                string amount = "";
                string intake = "";
                string EMAIL = "";
                string tranid = "";
                string NAME = "";
                string bankname = "";
                string branch = "";
                string username = "";
                DataSet ds = objFeeCont.GETEMAILDETAILS(Convert.ToInt32(ViewState["UserStudentUserno"]));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    date = ds.Tables[0].Rows[0]["TRANSDATE"].ToString();
                    amount = ds.Tables[0].Rows[0]["AMOUNT"].ToString();
                    intake = ds.Tables[0].Rows[0]["BATCHNAME"].ToString();
                    EMAIL = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                    tranid = ds.Tables[0].Rows[0]["ORDER_ID"].ToString();
                    NAME = ds.Tables[0].Rows[0]["NAME"].ToString();
                    bankname = ds.Tables[0].Rows[0]["BANK_NAME"].ToString();
                    branch = ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();
                    username = ds.Tables[0].Rows[0]["USERNAME"].ToString();
                }
                //string EMAIL = "roshan.pannase@mastersofterp.co.in";
                //string FINANCEMAIL = "roshan.pannase@mastersofterp.co.in";
                string FINANCEMAIL = objCommon.LookUp("REFF", "FINANCE_EMAIL", "");
                //string NAME = Convert.ToString(objCommon.LookUp("ACD_USER_REGISTRATION", "FIRSTNAME + ' ' + LASTNAME", "USERNO='" + (Convert.ToInt32(((UserDetails)(Session["user"])).UserNo)) + "'"));
                dsUserContact = user.GetEmailTamplateandUserDetails("", "", "", Convert.ToInt32(2));
                message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                Execute(message, EMAIL, subject, NAME, username, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
                dsUserContact = user.GetEmailTamplateandUserDetails("", "", "", Convert.ToInt32(1));
                message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                Execute1(message, FINANCEMAIL, subject, NAME, date, amount, bankname, branch, username, intake, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();





                if (cs == CustomStatus.RecordSaved)
                {
                    objCommon.DisplayMessage(updChallan, "Deposit Data Saved Successfully !!!", this.Page);
                    txtChallanId.Text = "";
                    //txtchallanAmount.Text = "";
                    //rdPaymentOption.SelectedValue = null;
                    ddlbank.SelectedValue = null;
                    txtBranchName.Text = "";
                    txtPaymentdate.Text = "";
                    fillDetails();
                    txtchallanAmount.Text = txtAmount.Text;
                    rdPaymentOption.SelectedValue = "2";
                }
                else if (cs == CustomStatus.RecordUpdated)
                {
                    objCommon.DisplayMessage(updChallan, "Deposit Data Updated Successfully !!!", this.Page);
                    txtChallanId.Text = "";
                    //txtchallanAmount.Text = "";
                    //rdPaymentOption.SelectedValue = null;
                    ddlbank.SelectedValue = null;
                    txtBranchName.Text = "";
                    txtPaymentdate.Text = "";
                    fillDetails();
                    rdPaymentOption.SelectedValue = "2";
                }
                else
                {
                    objCommon.DisplayMessage(updChallan, "Error !!!", this.Page);
                    txtChallanId.Text = "";
                    //txtchallanAmount.Text = "";
                }
            }
            else
            {
                objCommon.DisplayMessage(updChallan, "Please Upload Deposit Slip !!!", this.Page);
                //rdPaymentOption.SelectedValue = null;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void DeleteApplyProgram(int Userno, int Status)
    {
        try
        {
            CustomStatus cs = CustomStatus.Others;
            cs = (CustomStatus)ObjNucFees.DeleteApplyDegrees(Userno, Status);
            if (cs == CustomStatus.RecordDeleted)
            {
                objCommon.DisplayMessage(updChallan, "Apply program deleted Successfully !!!", this.Page);
                fillDetails();
                rdPaymentOption.SelectedValue = null;
            }
            else if (cs == CustomStatus.RecordFound)
            {
                objCommon.DisplayMessage(updChallan, "Payment already done unable to delete applied program !!!", this.Page);
                fillDetails();
                rdPaymentOption.SelectedValue = null;
            }
            else if (cs == CustomStatus.RecordExist)
            {
                objCommon.DisplayMessage(updChallan, "Deposit copy already uploaded unable to delete applied program !!!", this.Page);
                fillDetails();
                rdPaymentOption.SelectedValue = null;
            }
            else if (cs == CustomStatus.Others)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect script", "alert('All applied program deleted Successfully');", true);
                fillDetails();
                rdPaymentOption.SelectedValue = null;
            }
            else
            {
                objCommon.DisplayMessage(updChallan, "Error !!!", this.Page);
                fillDetails();
                rdPaymentOption.SelectedValue = null;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void GenerateChallan()
    {
        try
        {
            string session = objCommon.LookUp("ACD_ADMISSION_CONFIG", "MAX(ADMBATCH)", string.Empty);
            int PAYMENTTYPE = 0;
            if (rdPaymentOption.SelectedValue == "0")
            {
                objStudentFees.UserNo = Convert.ToInt32(ViewState["UserStudentUserno"]);
                objStudentFees.SessionNo = session;
                objStudentFees.OrderID = lblOrderIDERP.Text;
                objStudentFees.Amount = Convert.ToDouble(hdfAmount.Value);
                objStudentFees.FeeType = "Offonline";
                PAYMENTTYPE = 1;

                // Additional Parameters
                objStudentFees.TransDate = System.DateTime.Today;
                objStudentFees.Bankno = 0;
                objStudentFees.BranchName = "";
                objStudentFees.Ddno = 0;
                objStudentFees.Catapplied = false;
                objStudentFees.Unitno = 0;
                objStudentFees.TransID = "";
            }
            int result = 0;

            result = ObjNucFees.SubmitFeesofStudent(objStudentFees, PAYMENTTYPE, 1);
            if (result > 0)
            {

                //objCommon.DisplayMessage(this.Page, "Challan Generated Successfully !!!", this.Page);

                fillDetails();
                if (rdPaymentOption.SelectedValue == "0")
                {
                    btnPayment.Style.Add("display", "none");

                }
                else
                {
                    btnPayment.Visible = true;
                }

                //this.ShowReportNew("PaymentReceipt.rpt", ((UserDetails)(Session["user"])).UserNo);
            }
        }
        catch (Exception ex)
        {

        }
    }
    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            //var listItem = rdPaymentOption.Items.FindByValue("1");
            //listItem.Attributes.Add("class", "disable");
            //ImageViewer.ImageUrl = "~/showEmpImage.aspx?id=" + ((UserDetails)(Session["user"])).UserNo + "&type=OnlineStudent";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);

            string Url = string.Empty;
            string directoryPath = string.Empty;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/DownloadImg" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = lnk.CommandArgument.ToString();
            var ImageName = img;
            if (img != null || img != "")
            {
                string extension = Path.GetExtension(img.ToString());
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
                if (extension == ".pdf")
                {
                    ImageViewer.Visible = false;
                    ltEmbed.Visible = true;

                    var Newblob = blobContainer.GetBlockBlobReference(ImageName);

                    string filePath = directoryPath + "\\" + ImageName;


                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);


                    string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"765px\" height=\"400px\">";
                    embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    embed += "</object>";
                    ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
                }
                else
                {
                    ImageViewer.Visible = true;
                    ltEmbed.Visible = false;
                    ImageViewer.ImageUrl = Convert.ToString(dtBlobPic.Rows[0]["uri"]);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Sorry, File not found !!!", this.Page);
            }

        }
        catch (Exception ex)
        {

        }
    }


    //START BY AASHNA 12-01-2022

    static async Task Execute1(string message, string toEmailId, string subject, string sudname, string date, string amount, string bank, string branch, string username, string intake, string sendemail, string emailpass)
    {

        try
        {
            SmtpMail oMail = new SmtpMail("TryIt");

            oMail.From = sendemail;
            oMail.To = toEmailId;
            oMail.Subject = subject;
            oMail.HtmlBody = message;
            oMail.HtmlBody = oMail.HtmlBody.Replace("{Intake Name}", intake.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("{Application ID}", username.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("{Candidate Name}", sudname.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("{Amount}", amount.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("{Payment Date}", date.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("{Bank Name}", bank.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("{Bank Branch Name}", branch.ToString());

            //SmtpServer oServer = new SmtpServer("smtp.live.com");
            SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022


            oServer.User = sendemail;
            oServer.Password = emailpass;

            oServer.Port = 587;

            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();

            oSmtp.SendMail(oServer, oMail);

        }
        catch (Exception ex)
        {

        }
    }

    static async Task Execute(string message, string toEmailId, string subject, string sudname, string username, string sendemail, string emailpass)
    {

        try
        {
            SmtpMail oMail = new SmtpMail("TryIt");

            oMail.From = sendemail;
            oMail.To = toEmailId;
            oMail.Subject = subject;
            oMail.HtmlBody = message;
            oMail.HtmlBody = oMail.HtmlBody.Replace("[UA_FULLNAME]", sudname.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("{Application ID}", username.ToString());
            //SmtpServer oServer = new SmtpServer("smtp.live.com");
            SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022

            oServer.User = sendemail;
            oServer.Password = emailpass;

            oServer.Port = 587;

            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();

            oSmtp.SendMail(oServer, oMail);

        }
        catch (Exception ex)
        {

        }
    }

    //END BY AASHNA 12-01-2022



    protected void btnSavePayLater_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdPaymentOption.SelectedValue != "2")
            {
                objCommon.DisplayMessage(this, "Please Select Pay in Cash Option !!!", this.Page);
                return;
            }
            else
            {
                objStudentFees.UserNo = Convert.ToInt32(ViewState["UserStudentUserno"]);
                string session = objCommon.LookUp("ACD_ADMISSION_CONFIG", "MAX(ADMBATCH)", string.Empty);
                objStudentFees.SessionNo = "0";
                objStudentFees.OrderID = "0";
                objStudentFees.Amount = Convert.ToDouble(txtAmount.Text);
                objStudentFees.FeeType = "payleter";
                int PAYMENTTYPE = 4;

                // Additional Parameters
                objStudentFees.TransDate = System.DateTime.Today;
                objStudentFees.Bankno = 0;
                objStudentFees.BranchName = "";
                objStudentFees.Ddno = 0;
                objStudentFees.Catapplied = false;
                objStudentFees.Unitno = 0;
                objStudentFees.TransID = "";

                int result = 0;

                result = ObjNucFees.SubmitFeesofStudent(objStudentFees, PAYMENTTYPE, 0);
                if (result == 1)
                {
                    //Session["UserName"] = null;
                    //Session.RemoveAll();
                    //Response.Redirect("~/default.aspx");
                    divOfflinePaymentdone.Visible = true;
                    objCommon.DisplayMessage(this, "Details Saved Successfully !!!", this.Page);

                }
                btnPayment.Style.Add("display", "none");
                fillDetails();
                // rdPaymentOption.SelectedIndex = 0;

            }
        }
        catch (Exception ex)
        {

        }
    }
    // --------------------------------- Payment Details End ----------------------------------------//


    protected void btnSubmitAllDetails_Click(object sender, EventArgs e)
    {
        try
        {
            BranchController objBC = new BranchController();
            //START Personal Details 
            objnu.UserNo = Convert.ToInt32(ViewState["UserStudentUserno"]);

            objnu.FIRSTNAME = txtFirstName.Text.Trim();
            objnu.LASTNAME = txtPerlastname.Text.Trim();
            objnu.MIDDLENAME = txtMiddleName.Text.Trim();
            objnu.GENDER = rdPersonalGender.SelectedValue;
            //  objnu.RELIGION = Convert.ToInt32(ddlReligion.SelectedValue);
            if (Convert.ToDateTime(txtDateOfBirth.Text).Year > 2020 || txtDateOfBirth.Text.Length < 1)
            {
                objCommon.DisplayMessage(this, "Date of Birth is not valid", this.Page);
                return;
            }
            else
            {
                objnu.DOB = Convert.ToDateTime(txtDateOfBirth.Text);
            }

            objnu.NATIONALITY = Convert.ToInt32(rdbQuestion.SelectedValue);
            objnu.MobileCode = ddlOnlineMobileCode.SelectedValue;   //txtmobilecode.Text;
            objnu.MOBILENO = txtMobile.Text.ToString();
            objnu.EMAILID = txtEmail.Text.ToString();
            objnu.PASSPORT = txtPersonalPassprtNo.Text;
            objnu.FATHERNAME = txtFatherName.Text.Trim();
            objnu.MOTHERNAME = txtMothersName.Text;
            int UANO = Convert.ToInt32(Session["userno"]);
            string ACRNo = TxtACRNo.Text;
            string Dateissue = txtACRDate.Text;




            //Start Education Details
            CustomStatus cs = 0;
            int StudyLevel = Convert.ToInt32(ddlProgramTypes.SelectedValue);
            int UserNo = Convert.ToInt32(ViewState["UserStudentUserno"]);
            if (StudyLevel == 6)
            {
                cs = (CustomStatus)objnuc.Insert_UpdateUserEdcucationalDetails(UserNo, "0", "0", "0", "0", "0", "0", "PRE_ELEME");
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                }
            }
            else
            {
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
                String cs2 = objnuc.SubmitPersonalDetails(objnu, UANO, ACRNo, Dateissue, txtPermAddress.Text, Convert.ToInt32(ddlPCon.SelectedValue), Convert.ToInt32(ddlPermanentState.SelectedValue), Convert.ToInt32(ddlPTahsil.SelectedValue));
                cs = (CustomStatus)objnuc.Insert_UpdateUserEdcucationalDetails(UserNo, NameOfSchool, Address, Region, YearAttended, Type, TypeNo, SectionNo);
            }
            //End Education Details

            // start Program Apply 
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
            string[] Prefer1 = Preference1.Split(',');
            string[] Prefer2 = Preference2.Split(',');
            string[] Prefer3 = Preference3.Split(',');
            if (ddlPrefrence1.SelectedValue != "0")
            {
                College_Id = Prefer1[0].ToString();
                AREA = Prefer1[1].ToString();
                DegreeNo = Prefer1[2].ToString();
                Branchno = Prefer1[3].ToString();
                CompusNo = Prefer1[4].ToString();
                AdmBatch = Prefer1[5].ToString();
                Branch_Prefer = "1";
            }
            if (ddlPrefrence2.SelectedValue != "0")
            {
                College_Id = Prefer1[0].ToString() + "," + Prefer2[0].ToString();
                AREA = Prefer1[1].ToString() + "," + Prefer2[1].ToString();
                DegreeNo = Prefer1[2].ToString() + "," + Prefer2[2].ToString();
                Branchno = Prefer1[3].ToString() + "," + Prefer2[3].ToString();
                CompusNo = Prefer1[4].ToString() + "," + Prefer2[4].ToString();
                AdmBatch = Prefer1[5].ToString() + "," + Prefer2[5].ToString();
                Branch_Prefer = "1,2";
            }
            if (ddlPrefrence3.SelectedValue != "0")
            {
                College_Id = Prefer1[0].ToString() + "," + Prefer2[0].ToString() + "," + Prefer3[0].ToString();
                AREA = Prefer1[1].ToString() + "," + Prefer2[1].ToString() + "," + Prefer3[1].ToString();
                DegreeNo = Prefer1[2].ToString() + "," + Prefer2[2].ToString() + "," + Prefer3[2].ToString();
                Branchno = Prefer1[3].ToString() + "," + Prefer2[3].ToString() + "," + Prefer3[3].ToString();
                CompusNo = Prefer1[4].ToString() + "," + Prefer2[4].ToString() + "," + Prefer3[4].ToString();
                AdmBatch = Prefer1[5].ToString() + "," + Prefer2[5].ToString() + "," + Prefer3[5].ToString();
                Branch_Prefer = "1,2,3";
            }
            objU.USERNO = Convert.ToInt32(ViewState["UserStudentUserno"]);
            objU.UGPG = Convert.ToInt32(ddlProgramTypes.SelectedValue);
            objU.Campus = 1;
            CustomStatus css = CustomStatus.Others;
            css = (CustomStatus)objBC.AddBranchPreference(objU, College_Id, DegreeNo, Branchno, AREA, CompusNo, AdmBatch, Branch_Prefer, UANO);

            // End Program Apply 

            //////////////////////////////////////////////////// Remark Save  //////////////////////////////////////////////////////////////////////
            int resultRemark = 0;
            resultRemark = objnuc.UpdateIntakeForSingleStudent(Convert.ToInt32(ViewState["UserStudentUserno"]), Convert.ToInt32(ddlIntake.SelectedValue), Convert.ToInt32(2), Convert.ToInt32(Session["userno"]), txtRemark.Text.ToString());

            // Start Study Campus  

            int Mode = 0;
            objU.USERNO = Convert.ToInt32(ViewState["UserStudentUserno"]);
            objU.Campus = Convert.ToInt32(ddlCampusDetails.SelectedValue);
            objU.WeekDays = 0;
            objU.AptitudeCenter = Convert.ToInt32(ddlApptituteCenter.SelectedValue);
            Mode = Convert.ToInt32(ddlMode.SelectedValue);
            objU.AptitudeMedium = Convert.ToInt32(ddlApptituteMedium.SelectedValue);

            if (rdoSpecialNeeds.SelectedValue == "0")
            {
                if (txtYes.Text == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter Reason Of Has Special Needs!", this.Page);
                    return;
                }
            }

            CustomStatus cs1 = CustomStatus.Others;

            cs1 = (CustomStatus)objBC.AddExamCenterandaptitutetest(objU, Mode, Convert.ToString(ddlExamDate.SelectedItem), Convert.ToString(ddlTimeSlot.SelectedItem), Convert.ToInt32(ddlVenue.SelectedValue), Convert.ToString(ddlVenue.SelectedItem), Convert.ToInt32(rdoSpecialNeeds.SelectedValue), Convert.ToString(txtYes.Text), Convert.ToInt32(ddlTimeSlot.SelectedValue), Convert.ToInt32(ddlExamDate.SelectedValue));
            // End Study Campus  
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                if (css.Equals(CustomStatus.RecordSaved))
                {
                    if (cs1.Equals(CustomStatus.RecordUpdated))
                    {
                        txtRemark.Text = "";
                        FillSaveDetails();
                        BindPersonalDetails();
                        ListViewDataBind();
                        ListViewDataBindProgra();
                        objCommon.DisplayMessage(this, "Record Saved Successfully", this.Page);
                        return;
                    }
                    else
                    {
                        txtRemark.Text = "";
                        FillSaveDetails();
                        BindPersonalDetails();
                        ListViewDataBind();
                        ListViewDataBindProgra();
                    }
                }
                else if (css.Equals(CustomStatus.RecordFound))
                {
                    if (cs1.Equals(CustomStatus.RecordUpdated))
                    {
                        txtRemark.Text = "";
                        FillSaveDetails();
                        BindPersonalDetails();
                        ListViewDataBind();
                        ListViewDataBindProgra();
                        objCommon.DisplayMessage(this, "Record Saved Successfully", this.Page);
                        return;
                    }
                    else
                    {
                        txtRemark.Text = "";
                        FillSaveDetails();
                        BindPersonalDetails();
                        ListViewDataBind();
                        ListViewDataBindProgra();
                    }
                }
            }
            else
            {
                txtRemark.Text = "";
                FillSaveDetails();
                BindPersonalDetails();
                ListViewDataBind();
                ListViewDataBindProgra();
            }

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "domain.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void ClearControls()
    {
        txt_Remark.Text = string.Empty;
        ddlLeadStatus.SelectedIndex = 0;
        txtEndDate.Text = string.Empty;
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    protected void btn_SubmitModal_Click(object sender, EventArgs e)
    {
        try
        {
            MappingController objmp = new MappingController();
            //ViewState["action"] = "Edit";
            if (ddlLeadStatus.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updLeadStatus, "Please select Lead Status", this.Page);
                return;
            }
            if (txt_Remark.Text == "")
            {
                objCommon.DisplayMessage(updLeadStatus, "Please Enter Remarks", this.Page);
                return;
            }

            int enqueryNo = 0, action = 0;
            string Action = Convert.ToString(ViewState["actionPop"]);
            if (Action == "Edit")
            {
                enqueryNo = Convert.ToInt32(hdnEnqueryno.Value);
                action = 2;
            }
            else
            {
                enqueryNo = 0;
                action = 1;
            }

            if (Convert.ToString(ViewState["UserStudentUserno"]) != string.Empty)
            {
                int ck1 = objmp.AddLeadStatus(Convert.ToInt32(ddlLeadStatus.SelectedValue), (ViewState["UserStudentUserno"].ToString()), Convert.ToInt32((Session["userno"]).ToString()), txt_Remark.Text, enqueryNo, action, (Convert.ToString(txtEndDate.Text)));
                if (ck1 == 1)
                {
                    objCommon.DisplayMessage(updLeadStatus, "Record Saved Successfully", this.Page);

                    //BindListView();
                    BindListViewApplication();
                    ClearControls();
                    ViewState["actionPop"] = "Add";
                    //this.bindColDept();Session["userno"]
                    //Clear();
                    // return;

                }
                if (ck1 == 2)
                {
                    objCommon.DisplayMessage(updLeadStatus, "Record Updated Successfully", this.Page);
                    //BindListView();
                    BindListViewApplication();
                    ClearControls();
                    ViewState["actionPop"] = "Add";
                    //this.bindColDept();Session["userno"]
                    //Clear();
                    // return;
                }
                else
                {
                    objCommon.DisplayMessage(updLeadStatus, "Few or All Record already Exist", this.Page);
                    //BindListView();
                    ClearControls();
                    ViewState["actionPop"] = "Add";
                    //this.bindColDept();
                    //Clear();
                    //  return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updLeadStatus, "Please Search Student First !!!", this.Page);
            }
        }
        catch (Exception ex) { }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["actionPop"] = "Edit";
            int enqueryno = int.Parse(btnEdit.CommandArgument);
            hdnEnqueryno.Value = Convert.ToString(enqueryno);
            DataSet leadstagedata = objCommon.FillDropDown("ACD_LEAD_STUDENT_ENQUIRY_FOLLOWUP EF INNER JOIN ACD_LEAD_STAGE LS ON (EF.ENQUIRYSTATUS=LS.LEADNO)", "ENQUIRYNO", "ENQUIRYNO,convert(varchar, NEXTFOLLOUP_DATE, 3)AS NEXTDATE,LEAD_UA_NO,ENQUIRYSTATUS,LEAD_STAGE_NAME,ENQUIRYSTATUS_DATE,REMARKS", "ENQUIRYNO=" + enqueryno + "", "");
            if (leadstagedata.Tables[0].Rows.Count > 0)
            {
                ddlLeadStatus.SelectedValue = leadstagedata.Tables[0].Rows[0]["ENQUIRYSTATUS"].ToString();
                txt_Remark.Text = leadstagedata.Tables[0].Rows[0]["REMARKS"].ToString();
                txtEndDate.Text = leadstagedata.Tables[0].Rows[0]["NEXTDATE"].ToString();
            }
            else
            {

            }
        }
        catch (Exception ex) { }
    }
    private void BindListViewApplication()
    {
        try
        {
            MappingController objmp = new MappingController();
            // String uano = (objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME=" + Convert.ToString(ViewState["STUDENT_USERNO"]) + ""));
            DataSet ds = objmp.GetLeadFolloup(Convert.ToInt32(ViewState["UserStudentUserno"]));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables != null)
            {
                lvlist.DataSource = ds;
                lvlist.DataBind();
                lvLeadList.DataSource = ds;
                lvLeadList.DataBind();

            }
            else
            {
                lvlist.DataSource = ds;
                lvlist.DataBind();
                lvLeadList.DataSource = null;
                lvLeadList.DataBind();

            }
            ds.Dispose();
        }
        catch (Exception ex)
        {

        }
    }
    private void BindData()
    {
        try
        {
            DataSet ds = objSC.getAppicantData(Convert.ToString(ViewState["UserStudentUserno"]));

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvDetails.DataSource = ds.Tables[0];
                lvDetails.DataBind();
            }
            else
            {
                lvDetails.DataSource = null;
                lvDetails.DataBind();
            }
            if (ds.Tables[0] != null && ds.Tables[1].Rows.Count > 0)
            {
                lvLeadDetails.DataSource = ds.Tables[1];
                lvLeadDetails.DataBind();
            }
            else
            {
                lvLeadDetails.DataSource = null;
                lvLeadDetails.DataBind();
            }
            ds.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void btnSubmitIntake_Click(object sender, EventArgs e)
    {
        try
        {
            int result = 0;
            result = objnuc.UpdateIntakeForSingleStudent(Convert.ToInt32(ViewState["UserStudentUserno"]), Convert.ToInt32(ddlIntake.SelectedValue), Convert.ToInt32(1), Convert.ToInt32(Session["userno"]), txtRemark.Text.ToString());
            if (result == 1)
            {
                objCommon.DisplayMessage(updAllOnlineAdmDetails, "Intake Change Successfully !!!", this.Page);
                btnSubmitIntake.Visible = false;
            }
            else
            {
                objCommon.DisplayMessage(updAllOnlineAdmDetails, "Error !!!", this.Page);
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlIntake_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["DdlIntakeValue"]) != string.Empty)
            {
                if (Convert.ToString(Session["DdlIntakeValue"]) == Convert.ToString(ddlIntake.SelectedValue))
                {
                    btnSubmitIntake.Visible = false;
                }
                else
                {
                    btnSubmitIntake.Visible = true;
                }
            }
            else
            {
                btnSubmitIntake.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlPCon_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPCon.SelectedValue != "0")
        {
            objCommon.FillDropDownList(ddlPermanentState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0 and COUNTRYNO=" + ddlPCon.SelectedValue, "STATENAME");
        }
        else
        {
            ddlPermanentState.SelectedValue = "0";
        }
    }
}