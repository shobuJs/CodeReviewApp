using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
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

public partial class StudentDataEntry : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    Student objStudent = new Student();
    StudentController objStudCont = new StudentController();
    StudentAddress objStudAddress = new StudentAddress();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();
    StudentRegistration objRegistration = new StudentRegistration();

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsPostBack)
            {
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();
                PopulateDropDownList();
                ViewState["action"] = "add";
                ViewState["brachno"] = null;
                divMsg.InnerHtml = string.Empty;

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "StudentDataEntry.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
        ViewState["action"] = "add";
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentDataEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentDataEntry.aspx");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        int count = 0;
        int branchno = 0;
        DataTableReader dtr = null;

        if (ddlExamName.SelectedValue == "1")
        {
            count = Convert.ToInt16(objCommon.LookUp("TEMP_STUDENT_DATA", "Count(*)", "[ROLL NO] =" + txtStudentRoll.Text + "  AND ADMBATCH = " + lblAdmBatch.ToolTip));
            if (count == 0)
                dtr = objCommon.FillDropDown("TEMP_STUDENT", "[NAME],0 AS EXAMYEAR,MOBILE, [ROLL NO] AS ROLL,[APPLICANT CATEGORY],  [ALLOTTED CATEGORY]", " [DEGREENO], [BRANCHNO], Gender ,DOB, [AIR OVERALL],  [BRANCH NAME], [HOME STATE], [QUOTA],PROGRAMME, [ROUND NO],[FATHERNAME],[MOTHERNAME]", "[ROLL NO] =" + txtStudentRoll.Text + " AND [GATE Reg] IS  NULL AND ADMBATCH = " + lblAdmBatch.ToolTip.ToString(), string.Empty).CreateDataReader();
            else
            {
                objCommon.DisplayMessage("Student Information Already Entered!!", this.Page);
                pnlStudent.Visible = false;
                ViewState["brachno"] = null;
                ViewState["action"] = null;
                lblRound.Text = "";

                return;
            }
        }
        if (dtr != null)
        {
            if (dtr.Read())
            {
                txtStudentName.Text = dtr["Name"] == DBNull.Value ? string.Empty : dtr["Name"].ToString();
                txtFatherName.Text = dtr["FatherName"] == DBNull.Value ? string.Empty : dtr["FatherName"].ToString();
                txtMotherName.Text = dtr["MotherName"] == DBNull.Value ? string.Empty : dtr["MotherName"].ToString();
                txtMobileNo.Text = dtr["MOBILE"] == DBNull.Value ? string.Empty : dtr["MOBILE"].ToString();

                if (dtr["Gender"] != DBNull.Value)
                {
                    if (dtr["Gender"].ToString().ToUpper().Contains("F"))
                    {
                        rdoGender.SelectedValue = "1";
                    }
                    else
                    {
                        rdoGender.SelectedValue = "0";
                    }
                }

                txtExamRollNo.Text = dtr["ROLL"] == DBNull.Value ? string.Empty : dtr["ROLL"].ToString();
                txtExamYear.Text = dtr["EXAMYEAR"] == DBNull.Value ? string.Empty : dtr["EXAMYEAR"].ToString();
                txtAllIndiaRank.Text = dtr["AIR OVERALL"] == DBNull.Value ? string.Empty : dtr["AIR OVERALL"].ToString();
                if (dtr["Quota"] == DBNull.Value)
                    ddlQuota.SelectedIndex = 0;
                else
                    ddlQuota.SelectedValue = ddlQuota.Items.FindByText(dtr["Quota"] == DBNull.Value ? "0" : dtr["Quota"].ToString()).Value;



                if (ddlExamName.SelectedValue == "1")
                {

                    if (dtr["Applicant Category"] != DBNull.Value)
                        GetBTECHCategory(dtr["Applicant Category"].ToString(), 1);
                    if (dtr["ALLOTTED CATEGORY"] != DBNull.Value)
                        GetBTECHCategory(dtr["ALLOTTED CATEGORY"].ToString(), 2);
                    txtDateOfBirth.Enabled = true;
                    txtExamPaper.Visible = false;
                    tdpaper.Visible = false;

                    if (dtr["branch name"].ToString().ToUpper().Contains("COMPUTER"))
                        branchno = 1;

                    else if (dtr["branch name"].ToString().ToUpper().Contains("ELECTRICAL"))
                        branchno = 2;
                    else if (dtr["branch name"].ToString().ToUpper().Contains("ELECTRONICS"))
                        branchno = 3;

                    ViewState["brachno"] = branchno.ToString();
                }
                else
                    if (ddlExamName.SelectedValue == "2")
                    {
                        if (dtr["Applicant Category"] != DBNull.Value)
                            GetMTECHCategory(dtr["Applicant Category"].ToString(), 1);
                        if (dtr["ALLOTTED CATEGORY"] != DBNull.Value)
                            GetMTECHCategory(dtr["ALLOTTED CATEGORY"].ToString(), 2);
                        txtDateOfBirth.Enabled = true;
                        txtExamPaper.Visible = false;
                        tdpaper.Visible = false;

                        if (dtr["branch name"].ToString().ToUpper().Contains("COMPUTER"))
                            branchno = 1;

                        else if (dtr["branch name"].ToString().ToUpper().Contains("ELECTRICAL"))
                            branchno = 2;
                        else if (dtr["branch name"].ToString().ToUpper().Contains("ELECTRONICS"))
                            branchno = 3;

                        ViewState["brachno"] = branchno.ToString();

                    }
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + Convert.ToInt32(ddlDegreesel.SelectedValue), "BRANCHNO");
                ddlBranch.SelectedValue = ViewState["brachno"].ToString();
                lblRound.Text = dtr["ROUND NO"] == DBNull.Value ? string.Empty : "Round No. = " + dtr["ROUND NO"].ToString();
                if (ddlDegreesel.SelectedIndex == 2 && branchno == 22)
                {
                    pnlStudent.Visible = true;

                }
                else if (ddlDegreesel.SelectedIndex == 1)
                {
                    pnlStudent.Visible = true;
                }
                else
                {
                    pnlStudent.Visible = false;
                    objCommon.DisplayMessage("Please Select Correct Degree!!", this.Page);

                }

                txtExamRollNo.Enabled = false;
                ddlExamName.Enabled = false;

            }
            else
                Response.Redirect("~/default.aspx");
            dtr.Close();
        }
        else
        {
            ViewState["action"] = null;
            objCommon.DisplayMessage("Your Information is already Saved!", this);
            txtExamRollNo.Enabled = false;
            txtDateOfBirth.Enabled = false;
            btnShow.Visible = false;
            btnSubmit.Visible = false;
            btnClear.Visible = false;
            pnlStudent.Visible = false;

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int hostel = 0;
        if (ViewState["action"].ToString() == "add")
        {
            objStudent.StudName = txtStudentName.Text;
            if (ddlExamName.SelectedValue == "2")
            {
                objStudent.GATE_YEAR = txtExamYear.Text;// == "" ? "0" : txtExamYear.Text;
                objStudent.GATE_REG = txtExamRollNo.Text;// == "" ? "0" : txtExamRollNo.Text;
                objStudent.GATE_SCORE = txtExamScore.Text;// == "" ? "0" : txtExamScore.Text;
                objStudent.GATE_PAPER = txtExamPaper.Text;
                objStudent.ALLINDIARANK = txtAllIndiaRank.Text == "" ? 0 : Convert.ToInt32(txtAllIndiaRank.Text);
                objStudent.DegreeNo = 5;
            }

            else
                if (ddlDegreesel.SelectedValue == "1")
                {
                    if (ddlExamName.SelectedValue == "1")
                    {
                        objStudent.YearOfExam = txtExamYear.Text;// == "" ? "0" : txtExamYear.Text;
                        objStudent.RollNo = txtExamRollNo.Text;//== "" ? "0" : txtExamRollNo.Text;
                        //objStudent.ALLINDIARANK = txtAllIndiaRank.Text == "" ? 0 : Convert.ToDouble(txtAllIndiaRank.Text);
                        objStudent.ALLINDIARANK = txtAllIndiaRank.Text == "" ? 0 : Convert.ToInt32(txtAllIndiaRank.Text);
                        objStudent.DegreeNo = 1;
                        if (!txtExamScore.Text.Trim().Equals(string.Empty)) objStudent.Score = Convert.ToDecimal(txtExamScore.Text.Trim());
                    }
                }
                else
                {
                    if (ddlExamName.SelectedValue == "2")
                    {
                        objStudent.YearOfExam = txtExamYear.Text;// == "" ? "0" : txtExamYear.Text;
                        objStudent.RollNo = txtExamRollNo.Text;//== "" ? "0" : txtExamRollNo.Text;
                        objStudent.ALLINDIARANK = txtAllIndiaRank.Text == "" ? 0 : Convert.ToInt32(txtAllIndiaRank.Text);
                        objStudent.DegreeNo = 1;
                    }

                }
            objStudent.Dob = Convert.ToDateTime(txtDateOfBirth.Text);
            objStudent.Sex = rdoGender.SelectedValue == "0" ? 'M' : 'F';
            objStudent.PMobile = txtMobileNo.Text;//== "" ? "0" : txtMobileNo.Text ;
            objStudent.CategoryNo = Convert.ToInt16(ddlApplicantCategory.SelectedValue);
            objStudent.AdmCategoryNo = Convert.ToInt16(ddlAdmissionCategory.SelectedValue);
            objStudent.BranchNo = Convert.ToInt16(ViewState["brachno"].ToString());
            objStudent.ADMQUOTANO = Convert.ToInt16(ddlQuota.SelectedValue);
            objStudent.PState = Convert.ToInt16(ddlState.SelectedValue);
            objStudent.FatherName = txtFatherName.Text;
            objStudent.MotherName = txtMotherName.Text;
            objStudent.ReligionNo = Convert.ToInt16(ddlReligion.Text);
            objStudent.Married = rdbMaritalStatus.SelectedValue == "0" ? 'N' : 'Y';
            objStudent.PH = rdbPH.SelectedValue == "2" ? "YES" : "NO";
            objStudent.NationalityNo = Convert.ToInt16(ddlNationality.SelectedValue);
            objStudAddress.PEMAIL = txtStudEmail.Text;
            objStudent.BloodGroupNo = Convert.ToInt16(ddlBloodGroupNo.SelectedValue);
            objStudent.PAddress = txtPermanentAddress.Text.Trim();
            objStudent.PCity = ddlCity.SelectedValue;
            objStudent.StateNo = Convert.ToInt16(ddlState.SelectedValue);
            objStudent.PPinCode = txtPIN.Text == "" ? "0" : txtPIN.Text;
            objStudent.StudentMobile = txtMobileNo.Text;// == "" ? "0" : txtMobileNo.Text;
            objStudent.AdmBatch = Convert.ToInt16(lblAdmBatch.ToolTip.ToString());
            objStudAddress.LADDRESS = txtPostalAddress.Text;
            objStudAddress.LSTATE = Convert.ToInt16(ddlLocalState.SelectedValue);
            objStudAddress.LMOBILE = txtGuardianMobile.Text == "" ? "0" : txtGuardianMobile.Text;
            objStudent.PMobile = txtConatctNo.Text == "" ? "0" : txtConatctNo.Text;
            objStudAddress.GEMAIL = txtGuardianEmail.Text;
            objStudent.PayTypeNO = Convert.ToInt32(ddlPaymentType.SelectedValue);
            objStudent.Remark = txtRemark.Text.Trim();
            objStudent.CsabAmount = Convert.ToDouble(txtPaidCCB.Text.Trim());

            if (chkHostel.Checked == true)
                hostel = 0;
            else
                hostel = 1;

            objStudent.DOCUMENTS = string.Empty;
            int ret = Convert.ToInt16(objStudCont.AddStudentTempData(objStudent, objStudAddress, hostel));
            if (ret > 0)
            {
                string studentIDs = ret.ToString();
                objCommon.DisplayMessage("Registration Completed and your IDNO:" + ret, this.Page);
                pnlStudent.Visible = false;
                Clear();

            }
            else
                objCommon.DisplayMessage("Error!Please Fill Data again", this.Page);
        }
        else
            Response.Redirect("~/default.aspx");
    }
    protected void PopulateDropDownList()
    {
        this.objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "", "BANKNAME");
        objCommon.FillDropDownList(ddlNationality, "ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", "NATIONALITYNO>0", "NATIONALITYNO");
        objCommon.FillDropDownList(ddlReligion, "ACD_RELIGION", "RELIGIONNO", "RELIGION", "RELIGIONNO>0", "RELIGIONNO");
        objCommon.FillDropDownList(ddlApplicantCategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO>0", "CATEGORY");
        objCommon.FillDropDownList(ddlAdmissionCategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO>0", "CATEGORY");
        objCommon.FillDropDownList(ddlReligion, "ACD_RELIGION", "RELIGIONNO", "RELIGION", "RELIGIONNO>0", "RELIGION");
        objCommon.FillDropDownList(ddlBloodGroupNo, "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME", "BLOODGRPNO>0", "BLOODGRPNAME");
        objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
        objCommon.FillDropDownList(ddlLocalState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
        objCommon.FillDropDownList(ddlLocalCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITY");
        objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITY");
        objCommon.FillDropDownList(ddlDegreesel, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO IN (1,3)", "DEGREENO");
        objCommon.FillDropDownList(ddlQuota, "ACD_QUOTA", "QUOTANO", "QUOTA", "QUOTANO>0", "QUOTANO");
        lblAdmBatch.Text = objCommon.LookUp("ACD_ADMBATCH", "TOP 1 BATCHNAME", "BATCHNO <>0  order by BATCHNO DESC");
        lblAdmBatch.ToolTip = objCommon.LookUp("ACD_ADMBATCH", "TOP 1 BATCHNO", "BATCHNO <>0  order by BATCHNO DESC");

    }
    protected void rdbExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAllIndiaRank.Text = "";
        txtExamYear.Text = "";
        txtExamRollNo.Text = "";
        txtExamScore.Text = "";
        ddlQuota.SelectedIndex = 0;
    }
    private void GetBTECHCategory(string category, int type)
    {
        if (category.Contains("G") && category.Contains("E"))
            if (type == 1)
                ddlApplicantCategory.SelectedValue = "4";
            else
                ddlAdmissionCategory.SelectedValue = "4";
        else if (category.Contains("O") && category.Contains("B"))
            if (type == 1)
                ddlApplicantCategory.SelectedValue = "3";
            else
                ddlAdmissionCategory.SelectedValue = "3";
        else if (category.Contains("S") && category.Contains("C"))
            if (type == 1)
                ddlApplicantCategory.SelectedValue = "2";
            else ddlAdmissionCategory.SelectedValue = "2";
        else if (category.ToString().Contains("S") && category.Contains("T"))
            if (type == 1)
                ddlApplicantCategory.SelectedValue = "1";
            else
                ddlAdmissionCategory.SelectedValue = "1";
        else if (category.ToString().Contains("C") && category.Contains("H"))
            if (type == 1)
                ddlApplicantCategory.SelectedValue = "5";
            else
                ddlAdmissionCategory.SelectedValue = "5";

        //PHYSICALLY HANDICAPPED
        if (category.Contains("-N"))
        {
            rdbPH.SelectedValue = "0";
        }
        else
            if (category.Contains("-Y"))
            {
                rdbPH.SelectedValue = "1";
            }
    }

    private void GetMTECHCategory(string category, int type)
    {
        if (category.Contains("BC"))
            if (type == 1)
                ddlApplicantCategory.SelectedValue = "3";
            else
                ddlAdmissionCategory.SelectedValue = "3";
        else if (category.Contains("EN"))
            if (type == 1)
                ddlApplicantCategory.SelectedValue = "4";
            else
                ddlAdmissionCategory.SelectedValue = "4";
        else if (category.Contains("SC"))
            if (type == 1)
                ddlApplicantCategory.SelectedValue = "2";
            else
                ddlAdmissionCategory.SelectedValue = "2";
        else if (category.ToString().Contains("ST"))
            if (type == 1)
                ddlApplicantCategory.SelectedValue = "1";
            else
                ddlAdmissionCategory.SelectedValue = "1";

        //PHYSICALLY HANDICAPPED
        if (category.Contains("NO"))
        {
            rdbPH.SelectedValue = "0";
        }
        else
            if (category.Contains("PH"))
            {
                rdbPH.SelectedValue = "1";
            }
    }
    private void Clear()
    {
        ddlQuota.SelectedIndex = 0;
        ddlExamName.SelectedIndex = 0;
        ddlAdmissionCategory.SelectedIndex = 0;
        ddlApplicantCategory.SelectedIndex = 0;
        ddlBloodGroupNo.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        ddlCity.SelectedIndex = 0;
        ddlExamName.SelectedIndex = 0;
        ddlLocalCity.SelectedIndex = 0;
        ddlLocalState.SelectedIndex = 0;
        ddlNationality.SelectedIndex = 0;
        ddlQuota.SelectedIndex = 0;
        ddlReligion.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;


        rdoGender.ClearSelection();
        rdbPH.ClearSelection();

        txtStudentName.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        txtExamRollNo.Text = string.Empty;
        txtExamYear.Text = string.Empty;
        txtAllIndiaRank.Text = string.Empty;
        txtDateOfBirth.Text = string.Empty;
        txtExamPaper.Text = string.Empty;
        txtExamScore.Text = string.Empty;
        txtDateOfBirth.Text = string.Empty;
        txtConatctNo.Text = string.Empty;
        txtFatherName.Text = string.Empty;
        txtGuardianEmail.Text = string.Empty;
        txtGuardianMobile.Text = string.Empty;
        txtGuardianPhone.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        txtMotherName.Text = string.Empty;
        txtPermanentAddress.Text = string.Empty;
        txtPIN.Text = string.Empty;
        txtPostalAddress.Text = string.Empty;
        txtStudEmail.Text = string.Empty;
        txtStudentName.Text = string.Empty;
        txtStudentRoll.Text = string.Empty;
        lblRound.Text = string.Empty;

        txtExamPaper.Visible = true;
        tdpaper.Visible = true;

        lblRound.Text = string.Empty;
        txtExamRollNo.Enabled = true;
        ddlExamName.Enabled = true;
        txtRemark.Text = string.Empty;
        ViewState["brachno"] = null;
        ViewState["action"] = "edit";


    }
    private string GetNewReceiptNo()
    {
        string receiptNo = string.Empty;
        try
        {
            string paymodecode = "B";
            string receiptType = "TF";
            DataSet ds = feeController.GetNewReceiptData(paymodecode, Int32.Parse(Session["userno"].ToString()), receiptType);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                String FeesSessionStartDate;
                FeesSessionStartDate = objCommon.LookUp("REFF", "RIGHT(year(Start_Year),2)", "");

                DataRow dr = ds.Tables[0].Rows[0];
                dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString()) + 1;
                receiptNo = dr["PRINTNAME"].ToString() + "/" + paymodecode + "/" + receiptType + "/" + FeesSessionStartDate + "/" + dr["FIELD"].ToString();
                ViewState["CounterNo"] = dr["COUNTERNO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return receiptNo;
    }

    private FeeDemand GetDcrCriteria()
    {
        FeeDemand dcrCriteria = new FeeDemand();
        try
        {
            dcrCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
            dcrCriteria.ReceiptTypeCode = "TF";
            dcrCriteria.BranchNo = objStudent.BranchNo;
            dcrCriteria.SemesterNo = 1;
            dcrCriteria.PaymentTypeNo = int.Parse(ddlPaymentType.SelectedValue);
            dcrCriteria.UserNo = int.Parse(Session["userno"].ToString());
            //dcrCriteria.ExcessAmount = Convert.ToDouble(txtPaidCCB.Text.Trim());
            dcrCriteria.ExcessAmount = Convert.ToDouble(txtPaidCCB.Text.Trim());
            dcrCriteria.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dcrCriteria;
    }

    private void ShowReport(string reportTitle, string rptFileName, string idno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private string GetNewReceiptNoForHostel()
    {
        string receiptNo = string.Empty;
        try
        {
            string paymodecode = "B";
            string receiptType = "HF";
            DataSet ds = feeController.GetNewReceiptData(paymodecode, Int32.Parse(Session["userno"].ToString()), receiptType);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                String FeesSessionStartDate;
                FeesSessionStartDate = objCommon.LookUp("REFF", "RIGHT(year(Start_Year),2)", "");

                DataRow dr = ds.Tables[0].Rows[0];
                dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString()) + 1;
                receiptNo = dr["PRINTNAME"].ToString() + "/" + paymodecode + "/" + receiptType + "/" + FeesSessionStartDate + "/" + dr["FIELD"].ToString();
                ViewState["CounterNo"] = dr["COUNTERNO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return receiptNo;
    }

    private FeeDemand GetDcrCriteriaFoHostel()
    {
        FeeDemand dcrCriteria = new FeeDemand();
        try
        {
            dcrCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
            dcrCriteria.ReceiptTypeCode = "HF";
            dcrCriteria.BranchNo = objStudent.BranchNo;
            dcrCriteria.SemesterNo = 1;
            dcrCriteria.PaymentTypeNo = int.Parse(ddlPaymentType.SelectedValue);
            dcrCriteria.UserNo = int.Parse(Session["userno"].ToString());
            dcrCriteria.ExcessAmount = 0;
            dcrCriteria.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dcrCriteria;
    }


    private void ShowReport(string rptName, int dcrNo, int studentNo, string copyNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=" + this.GetReportParameters(studentNo, dcrNo, copyNo);
            divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetReportParameters(int studentNo, int dcrNo, string copyNo)
    {

        string collegeCode = "15";

        string param = "@P_IDNO=" + studentNo.ToString() + ",@P_DCRNO=" + dcrNo + ",CopyNo=" + copyNo + ",@P_COLLEGE_CODE=" + collegeCode + "";
        return param;
    }



    protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
}
