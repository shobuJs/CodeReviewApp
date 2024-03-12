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


public partial class ACADEMIC_AddressDetails : System.Web.UI.Page
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

                ViewState["usertype"] = Session["usertype"];
                this.FillDropDown();
                if (ViewState["usertype"].ToString() == "2")
                {
                    ShowStudentDetails();
                    divadmissiondetails.Visible = false;
                    divhome.Visible = false;
                }
                else
                {
                    ShowStudentDetails();
                    divadmissiondetails.Visible = true;
                    divhome.Visible = true;
                }
                hdn_Pdistrict.Value = "";

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
                Response.Redirect("~/notauthorized.aspx?page=AddressDetails.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AddressDetails.aspx");
        }
    }


    public void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlLocalCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITY");
            objCommon.FillDropDownList(ddlLocalState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
            objCommon.FillDropDownList(ddlPermCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITY");
            objCommon.FillDropDownList(ddlPermState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
            objCommon.FillDropDownList(ddlPdistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0", "DISTRICTNAME");
            objCommon.FillDropDownList(ddlLdistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0", "DISTRICTNAME");
            ddlPdistrict.SelectedItem.Text = "Please Select";
            ddlLdistrict.SelectedItem.Text = "Please Select";
        }
        catch (Exception Ex)
        {

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
                txtCorresAddress.Text = dtr["CORRESPONDANCE_ADDRESS"] == null ? string.Empty : dtr["CORRESPONDANCE_ADDRESS"].ToString();
                txtCorresPin.Text = dtr["CORRESPONDANCE_PIN"] == null ? string.Empty : dtr["CORRESPONDANCE_PIN"].ToString();
                txtCorresMob.Text = dtr["CORRESPONDANCE_MOB"] == null ? string.Empty : dtr["CORRESPONDANCE_MOB"].ToString();
                txtLocalAddress.Text = dtr["LADDRESS"] == null ? string.Empty : dtr["LADDRESS"].ToString();
                ddlLocalCity.SelectedValue = dtr["LCITY"] == null ? "0" : dtr["LCITY"].ToString();
                if (dtr["LSTATE"].ToString() == "0" || dtr["LSTATE"] == null)
                {
                }
                else
                {
                    ddlLocalState.SelectedValue = dtr["LSTATE"].ToString();
                }

                if (dtr["LDISTRICT"].ToString() == "0" || dtr["LDISTRICT"] == null)
                {
                }
                else
                {
                    ddlLdistrict.SelectedValue = dtr["LDISTRICT"].ToString();
                }
                txtLocalLandlineNo.Text = dtr["LTELEPHONE"] == null ? string.Empty : dtr["LTELEPHONE"].ToString();
                txtLocalEmail.Text = dtr["LEMAIL"] == null ? string.Empty : dtr["LEMAIL"].ToString();
                txtLocalPIN.Text = dtr["LPINCODE"] == null ? string.Empty : dtr["LPINCODE"].ToString();
                txtLocalMobileNo.Text = dtr["LMOBILE"] == null ? string.Empty : dtr["LMOBILE"].ToString();
                txtPermAddress.Text = dtr["PADDRESS"] == null ? string.Empty : dtr["PADDRESS"].ToString();
                ddlPermCity.SelectedValue = dtr["PCITY"] == null ? "0" : dtr["PCITY"].ToString();
                if (dtr["PSTATE"].ToString() == "0" || dtr["PSTATE"] == null)
                {
                }
                else
                {
                    ddlPermState.SelectedValue = dtr["PSTATE"].ToString();
                }

                if (dtr["PDISTRICT"].ToString() == "0" || dtr["PDISTRICT"] == null)
                {
                }
                else
                {
                    ddlPdistrict.SelectedValue = dtr["PDISTRICT"].ToString();
                }
                txtLocalNo.Text = dtr["PTELEPHONE"] == null ? string.Empty : dtr["PTELEPHONE"].ToString();
                txtPermEmailId.Text = dtr["PEMAIL"] == null ? string.Empty : dtr["PEMAIL"].ToString();
                txtPermPIN.Text = dtr["PPINCODE"] == null ? string.Empty : dtr["PPINCODE"].ToString();
                txtpostoff.Text = dtr["LPOSTOFF"] == null ? string.Empty : dtr["LPOSTOFF"].ToString();
                txtpolicestation.Text = dtr["LPOLICESTATION"] == null ? string.Empty : dtr["LPOLICESTATION"].ToString();
                txtTehsil.Text = dtr["PTEHSIL"] == null ? string.Empty : dtr["PTEHSIL"].ToString();
                txtpermpostOff.Text = dtr["PPOSTOFF"] == null ? string.Empty : dtr["PPOSTOFF"].ToString();
                txtPermPoliceStation.Text = dtr["PPOLICEOFF"] == null ? string.Empty : dtr["PPOLICEOFF"].ToString();
                txtMobileNo.Text = dtr["PMOBILE"] == null ? string.Empty : dtr["PMOBILE"].ToString();
                txtGuardianAddress.Text = dtr["GADDRESS"] == null ? string.Empty : dtr["GADDRESS"].ToString();
                txtGoccupationName.Text = dtr["GOCCUPATIONNAME"] == null ? string.Empty : dtr["GOCCUPATIONNAME"].ToString();
                txtGuardianLandline.Text = dtr["GPHONE"] == null ? string.Empty : dtr["GPHONE"].ToString();
                txtguardianEmail.Text = dtr["GEMAIL"] == null ? string.Empty : dtr["GEMAIL"].ToString();
                txtGuardianName.Text = dtr["GUARDIANNAME"] == null ? string.Empty : dtr["GUARDIANNAME"].ToString();
                txtAnnualIncome.Text = dtr["ANNUAL_INCOME"] == null ? string.Empty : dtr["ANNUAL_INCOME"].ToString();
                txtRelationWithGuardian.Text = dtr["RELATION_GUARDIAN"] == null ? string.Empty : dtr["RELATION_GUARDIAN"].ToString();
                txtGDesignation.Text = dtr["GUARDIAN_DESIG"] == null ? string.Empty : dtr["GUARDIAN_DESIG"].ToString();
            }
        }
    }

    protected void ddlLocalState_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlLdistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0 and stateno=" + ddlLocalState.SelectedValue, "DISTRICTNAME");
    }
    protected void ddlPermState_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlPdistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0 and stateno=" + ddlPermState.SelectedValue, "DISTRICTNAME");
        if (hdn_Pdistrict.Value != "")//*****************
        {
            ddlPdistrict.SelectedIndex = Convert.ToInt32(hdn_Pdistrict.Value);
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



                if (!txtLocalAddress.Text.Trim().Equals(string.Empty)) objSAddress.LADDRESS = Convert.ToString(txtLocalAddress.Text.Trim());
                objSAddress.LCITY = Convert.ToInt32(ddlLocalCity.SelectedValue);
                objSAddress.LSTATE = Convert.ToInt32(ddlLocalState.SelectedValue);

                if (!txtLocalPIN.Text.Trim().Equals(string.Empty)) objSAddress.LPINCODE = txtLocalPIN.Text.Trim();
                if (!txtLocalLandlineNo.Text.Trim().Equals(string.Empty)) objSAddress.LTELEPHONE = txtLocalLandlineNo.Text.Trim();
                if (!txtLocalMobileNo.Text.Trim().Equals(string.Empty)) objSAddress.LMOBILE = txtLocalMobileNo.Text.Trim();
                if (!txtpostoff.Text.Trim().Equals(string.Empty)) objSAddress.LPOSTOFF = txtpostoff.Text.Trim();
                if (!txtpolicestation.Text.Trim().Equals(string.Empty)) objSAddress.LPOLICESTATION = txtpolicestation.Text.Trim();

                //Permenent Address
                if (!txtPermAddress.Text.Trim().Equals(string.Empty)) objSAddress.PADDRESS = txtPermAddress.Text.Trim();
                objSAddress.PCITY = Convert.ToInt32(ddlPermCity.SelectedValue);
                objSAddress.PSTATE = Convert.ToInt32(ddlPermState.SelectedValue);
                objSAddress.PDISTRICT = ddlPdistrict.SelectedValue;
                if (chkcopypermanentadress.Checked == true)
                {
                    objSAddress.LDISTRICT = ddlPdistrict.SelectedValue;
                }
                else
                {
                    objSAddress.LDISTRICT = ddlLdistrict.SelectedValue;
                }
                if (!txtPermPIN.Text.Trim().Equals(string.Empty)) objSAddress.PPINCODE = txtPermPIN.Text.Trim();
                if (!txtLocalNo.Text.Trim().Equals(string.Empty)) objSAddress.PTELEPHONE = txtLocalNo.Text.Trim();
                if (!txtMobileNo.Text.Trim().Equals(string.Empty)) objSAddress.PMOBILE = txtMobileNo.Text.Trim();
                if (!txtTehsil.Text.Trim().Equals(string.Empty)) objSAddress.LTEHSIL = txtTehsil.Text.Trim();
                if (!txtpermpostOff.Text.Trim().Equals(string.Empty)) objSAddress.PPOSTOFF = txtpermpostOff.Text.Trim();
                if (!txtPermPoliceStation.Text.Trim().Equals(string.Empty)) objSAddress.PPOLICESTATION = txtPermPoliceStation.Text.Trim();

                //Guardian's Address
                if (!txtGuardianAddress.Text.Trim().Equals(string.Empty)) objSAddress.GADDRESS = txtGuardianAddress.Text.Trim();
                if (!txtGuardianName.Text.Trim().Equals(string.Empty)) objSAddress.GUARDIANNAME = txtGuardianName.Text.Trim();
                if (!txtGuardianLandline.Text.Trim().Equals(string.Empty)) objSAddress.GPHONE = txtGuardianLandline.Text.Trim();
                objSAddress.ANNUAL_INCOME = txtAnnualIncome.Text.Trim();
                if (!txtRelationWithGuardian.Text.Trim().Equals(string.Empty)) objSAddress.RELATION_GUARDIAN = txtRelationWithGuardian.Text.Trim();
                if (!txtGoccupationName.Text.Trim().Equals(string.Empty)) objSAddress.GOCCUPATIONNAME = txtGoccupationName.Text.Trim();
                if (!txtGDesignation.Text.Trim().Equals(string.Empty)) objSAddress.GUARDIANDESIGNATION = txtGDesignation.Text.Trim();



                CustomStatus cs = (CustomStatus)objSC.UpdateStudentAddressDetails(objS, objSAddress, Convert.ToInt32(Session["usertype"]));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ShowStudentDetails();
                    if (ViewState["usertype"].ToString() == "2")
                    {
                        divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('Address Details Updated Successfully!!'); </script>";
                        Response.Redirect("~/academic/DASAStudentInformation.aspx", false);
                    }
                    else
                    {
                        divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('Address Details Updated Successfully!!'); </script>";
                        Response.Redirect("~/academic/AdmissionDetails.aspx", false);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updAddressDetails, "Error Occured While Updating Address Details!!", this.Page);
                }

            }
            else
            {
                objCommon.DisplayMessage("You Are Not Authorised Person For This Form.Contact To Administrator.", this.Page);

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
                objCommon.DisplayMessage(this.updAddressDetails, "Please Search Enrollment No!!", this.Page);
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
            ScriptManager.RegisterClientScriptBlock(this.updAddressDetails, this.updAddressDetails.GetType(), "controlJSScript", sb.ToString(), true);
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
}