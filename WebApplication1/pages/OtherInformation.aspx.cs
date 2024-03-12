using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Linq;


public partial class ACADEMIC_OtherInformation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
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
                this.FillDropDown();
                ViewState["usertype"] = Session["usertype"];
                if (ViewState["usertype"].ToString() == "2")
                {
                    divadmissiondetailstreeview.Visible = false;
                    divhome.Visible = false;
                }
                else
                {
                    divadmissiondetailstreeview.Visible = true;
                    divhome.Visible = true;
                }
                this.ShowStudentDetails();
                this.bindexpdetails();
            }

            this.FillDropDown();
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=OtherInformation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OtherInformation.aspx");
        }
    }


    private void ShowStudentDetails()
    {
        StudentController objSC = new StudentController();
        DataTableReader dtr = null;
        if (ViewState["usertype"].ToString() == "2")
        {
            dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["idno"]));

        }
        else
        {
            dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["stuinfoidno"]));
        }

        if (dtr != null)
        {
            if (dtr.Read())
            {
                objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO>0", "BANKNAME");
                txtBirthPlace.Text = dtr["BIRTH_PLACE"] == null ? string.Empty : dtr["BIRTH_PLACE"].ToString();
                objCommon.FillDropDownList(ddlMotherToungeNo, "ACD_MTONGUE", "MTONGUENO", "MTONGUE", "MTONGUENO>0", "MTONGUE");
                ddlMotherToungeNo.SelectedValue = dtr["MTOUNGENO"] == null ? "0" : dtr["MTOUNGENO"].ToString();
                txtOtherLangauge.Text = dtr["OTHER_LANG"] == null ? string.Empty : dtr["OTHER_LANG"].ToString();
                txtBirthVillage.Text = dtr["BIRTH_VILLAGE"] == null ? string.Empty : dtr["BIRTH_VILLAGE"].ToString();

                txtBirthTaluka.Text = dtr["BIRTH_TALUKA"] == null ? string.Empty : dtr["BIRTH_TALUKA"].ToString();
                txtBirthDistrict.Text = dtr["BIRTH_DISTRICT"] == null ? string.Empty : dtr["BIRTH_DISTRICT"].ToString();
                txtBirthState.Text = dtr["BIRTH_STATE"] == null ? string.Empty : dtr["BIRTH_STATE"].ToString();
                txtBirthPinCode.Text = dtr["BIRTH_PINCODE"] == null ? string.Empty : dtr["BIRTH_PINCODE"].ToString();
                txtHeight.Text = dtr["HEIGHT"] == null ? "0" : dtr["HEIGHT"].ToString();
                txtWeight.Text = dtr["WEIGHT"] == null ? "0" : dtr["WEIGHT"].ToString();

                txtIdentiMark.Text = dtr["IDENTI_MARK"] == null ? string.Empty : dtr["IDENTI_MARK"].ToString();
                ddlBank.SelectedValue = dtr["BANKNO"] == DBNull.Value ? "0" : dtr["BANKNO"].ToString();
                txtAccNo.Text = dtr["ACC_NO"] == null ? string.Empty : dtr["ACC_NO"].ToString();
                txtPassportNo.Text = dtr["PASSPORTNO"] == null ? string.Empty : dtr["PASSPORTNO"].ToString();

                if (dtr["URBAN"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(dtr["URBAN"]) == true)
                    {
                        rdobtn_urban.SelectedValue = "Y";
                    }
                    else
                    {
                        rdobtn_urban.SelectedValue = "N";
                    }
                }

            }
        }
    }

    private void FillDropDown()
    {
        try
        {

            objCommon.FillDropDownList(ddlMotherToungeNo, "ACD_MTONGUE", "MTONGUENO", "MTONGUE", "MTONGUENO>0", "MTONGUE");
            ddlMotherToungeNo.SelectedIndex = 11;
            objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO>0", "BANKNAME");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_OtherInformation.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        Student objS = new Student();
        StudentPhoto objSPhoto = new StudentPhoto();
        StudentAddress objSAddress = new StudentAddress();
        StudentQualExm objSQualExam = new StudentQualExm();
        try
        {
            if (ViewState["usertype"].ToString() == "2" || ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7" || ViewState["usertype"].ToString() == "5")
            {
                if (ViewState["usertype"].ToString() == "2")
                {
                    objS.IdNo = Convert.ToInt32(Session["idno"]);
                }
                else
                {
                    objS.IdNo = Convert.ToInt32(Session["stuinfoidno"]);
                }
                if (!txtBirthPlace.Text.Trim().Equals(string.Empty)) objS.BirthPlace = txtBirthPlace.Text.Trim();
                objS.MToungeNo = Convert.ToInt32(ddlMotherToungeNo.SelectedValue);
                if (!txtOtherLangauge.Text.Trim().Equals(string.Empty)) objS.OtherLanguage = txtOtherLangauge.Text.Trim();
                if (!txtBirthVillage.Text.Trim().Equals(string.Empty)) objS.Birthvillage = txtBirthVillage.Text.Trim();
                if (!txtBirthTaluka.Text.Trim().Equals(string.Empty)) objS.Birthtaluka = txtBirthTaluka.Text.Trim();
                if (!txtBirthDistrict.Text.Trim().Equals(string.Empty)) objS.Birthdistrict = txtBirthDistrict.Text.Trim();
                if (!txtBirthState.Text.Trim().Equals(string.Empty)) objS.Birthdistate = txtBirthState.Text.Trim();
                if (!txtBirthPinCode.Text.Trim().Equals(string.Empty)) objS.BirthPinCode = txtBirthPinCode.Text.Trim();
                if (!txtHeight.Text.Trim().Equals(string.Empty)) objS.Height = Convert.ToDecimal(txtHeight.Text.Trim());
                if (!txtWeight.Text.Trim().Equals(string.Empty)) objS.Weight = Convert.ToDecimal(txtWeight.Text.Trim());
                if (rdobtn_urban.SelectedValue == "Y")//**********
                    objS.Urban = true;
                else
                    objS.Urban = false;
                if (!txtIdentiMark.Text.Trim().Equals(string.Empty)) objS.IdentyMark = txtIdentiMark.Text.Trim();
                objS.BankNo = Convert.ToInt32(ddlBank.SelectedValue);
                if (!txtAccNo.Text.Trim().Equals(string.Empty)) objS.AccNo = txtAccNo.Text.Trim();
                if (!txtPassportNo.Text.Trim().Equals(string.Empty)) objS.PassportNo = txtPassportNo.Text.Trim();
                if (!txtworkexp.Text.Trim().Equals(string.Empty)) objS.WorkExp = txtworkexp.Text.Trim();
                if (!txtdesignation.Text.Trim().Equals(string.Empty)) objS.Designation = txtdesignation.Text.Trim();
                if (!txtorgwork.Text.Trim().Equals(string.Empty)) objS.OrgLastWork = txtorgwork.Text.Trim();
                if (!txttotalexp.Text.Trim().Equals(string.Empty)) objS.TotalWorkExp = txttotalexp.Text.Trim();
                CustomStatus cs = (CustomStatus)objSC.UpdateStudentOtherInformation(objS, Convert.ToInt32(Session["usertype"]));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ShowStudentDetails();

                    objCommon.DisplayMessage(this.updotherinformation, "Other Information Updated Successfully!!", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.updotherinformation, "Error Occured While Updating Other Information!!", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updotherinformation, "You Are Not Authorised Person For This Form.Contact To Administrator.", this.Page);
            }
        }
        catch (Exception Ex)
        {

        }
    }
    protected void btnGohome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/StudentInfoEntryNew.aspx?pageno=2219");
    }

    protected void lnkPersonalDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/PersonalDetails.aspx");
    }
    protected void lnkAddressDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/AddressDetails.aspx");
    }
    protected void lnkAdmissionDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/AdmissionDetails.aspx");
    }

    protected void lnkDasaStudentInfo_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/academic/DASAStudentInformation.aspx");
    }
    protected void lnkQualificationDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/QualificationDetails.aspx");
    }
    protected void lnkotherinfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/OtherInformation.aspx");
    }
    protected void lnkprintapp_Click(object sender, EventArgs e)
    {
        GEC_Student objGecStud = new GEC_Student();
        if (ViewState["usertype"].ToString() == "2")
        {
            objGecStud.RegNo = Session["idno"].ToString();
            string output = objGecStud.RegNo;
            ShowReport("Admission_Form_Report_M.TECH", "Admission_Slip_Confirm_PHD_General.rpt", output);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                objGecStud.RegNo = Session["stuinfoidno"].ToString();
                string output = objGecStud.RegNo;
                ShowReport("Admission_Form_Report_M.TECH", "Admission_Slip_Confirm_PHD_General.rpt", output);
            }
            else
            {
                objCommon.DisplayMessage(this.updotherinformation, "Please Search Enrollment No!!", this.Page);
            }
        }


    }
    private void ShowReport(string reportTitle, string rptFileName, string regno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Admission Form Report " + Session["stuinfoenrollno"].ToString();
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(regno) + "";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updotherinformation, this.updotherinformation.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void lnkGoHome_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "1")
        {
            Session["stuinfoidno"] = null;
            Session["stuinfoenrollno"] = null;
            Session["stuinfofullname"] = null;
            Response.Redirect("~/academic/StudentInfoEntry.aspx?pageno=74");
        }
        else
        {
            Response.Redirect("~/academic/StudentInfoEntry.aspx?pageno=74");
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        Student objS = new Student();
        int expno = 0;
        try
        {
            if (ViewState["usertype"].ToString() == "2" || ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7" || ViewState["usertype"].ToString() == "5")
            {
                if (ViewState["usertype"].ToString() == "2")
                {
                    objS.IdNo = Convert.ToInt32(Session["idno"]);
                }
                else
                {
                    objS.IdNo = Convert.ToInt32(Session["stuinfoidno"]);
                }
                if (!txtworkexp.Text.Trim().Equals(string.Empty)) objS.WorkExp = txtworkexp.Text.Trim();
                if (!txtdesignation.Text.Trim().Equals(string.Empty)) objS.Designation = txtdesignation.Text.Trim();
                if (!txtorgwork.Text.Trim().Equals(string.Empty)) objS.OrgLastWork = txtorgwork.Text.Trim();
                if (!txttotalexp.Text.Trim().Equals(string.Empty)) objS.TotalWorkExp = txttotalexp.Text.Trim();
                if (!txtepfno.Text.Trim().Equals(string.Empty)) objS.EpfNo = txtepfno.Text.Trim();
                if (txtworkexp.ToolTip != "")
                {
                    expno = Convert.ToInt32(txtworkexp.ToolTip);
                }
                CustomStatus cs = (CustomStatus)objSC.UpdateStudentWorkexp(objS, expno);
                if (cs.Equals(CustomStatus.RecordSaved) || cs.Equals(CustomStatus.RecordUpdated))
                {
                    this.bindexpdetails();
                }
                txtworkexp.Text = string.Empty;
                txtdesignation.Text = string.Empty;
                txtorgwork.Text = string.Empty;
                txtworkexp.ToolTip = string.Empty;
                btnadd.Text = "Add";
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void bindexpdetails()
    {
        try
        {
            StudentController objSC = new StudentController();
            DataSet ds = null;
            DataTable dt = null;
            if (ViewState["usertype"].ToString() == "2")
            {
                ds = objSC.GetStudentExpDetails(Convert.ToInt32(Session["idno"]));

            }
            else
            {
                ds = objSC.GetStudentExpDetails(Convert.ToInt32(Session["stuinfoidno"]));
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
                Session["expdetails"] = dt;
                lvExperience.DataSource = ds;
                lvExperience.DataBind();
            }
            else
            {
                lvExperience.DataSource = null;
                lvExperience.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void btnEditexpDetail_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        DataTable dt;
        if (Session["expdetails"] != null && ((DataTable)Session["expdetails"]) != null)
        {
            dt = ((DataTable)Session["expdetails"]);
            DataRow dr = this.GetEditableDataRow(dt, btnEdit.CommandArgument);
            txtworkexp.Text = dr["WORK_EXP"].ToString();
            txtorgwork.Text = dr["ORG_LAST_WORK"].ToString();
            txtdesignation.Text = dr["DESIGNATION"].ToString();
            txtworkexp.ToolTip = dr["EXP_INC"].ToString();
            txtepfno.Text = dr["EPFNO"].ToString();

            dt.Rows.Remove(dr);
            this.bindexpdetails();
            btnadd.Text = "Update";
        }
    }

    private DataRow GetEditableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["EXP_INC"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.GetEditableDataRow() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dataRow;
    }
    protected void btnDeleteworkDetail_Click(object sender, ImageClickEventArgs e)
    {
        StudentController objSC = new StudentController();
        ImageButton btnEdit = sender as ImageButton;
        DataTable dt;
        if (Session["expdetails"] != null && ((DataTable)Session["expdetails"]) != null)
        {
            dt = ((DataTable)Session["expdetails"]);

            CustomStatus cs = (CustomStatus)objSC.GetdeleteDataRow(Convert.ToInt32(btnEdit.ToolTip), Convert.ToInt32(btnEdit.CommandArgument));
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                this.bindexpdetails();
            }


        }
    }
}