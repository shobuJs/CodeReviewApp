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

public partial class ACADEMIC_AdmissionDetails : System.Web.UI.Page
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


                if (ViewState["usertype"].ToString() == "2")
                {
                    divadmissiondetailstreeview.Visible = false;
                    AdmDetails.Visible = false;
                    divhome.Visible = false;
                    // btnGohome.Visible = false;
                }
                else
                {
                    divadmissiondetailstreeview.Visible = true;
                    AdmDetails.Visible = true;
                    txtDateOfAdmission.Enabled = true;
                    ddlSchoolCollege.Enabled = true;
                    ddlDegree.Enabled = true;
                    ddlBranch.Enabled = true;
                    ddlBatch.Enabled = true;
                    ddlStateOfEligibility.Enabled = true;
                    ddlYear.Enabled = true;
                    ddlSemester.Enabled = true;
                    ddlAllotedCategory.Enabled = true;
                    ddlclaim.Enabled = true;
                    divhome.Visible = true;

                    //Shrikant Ramekar added 21 feb 2019
                    txtSRNO.Enabled = true;
                    ddlSRCatg.Enabled = true;
                    ddlcolcode.Enabled = true;
                    ddlPaymentTp.Enabled = true;




                    // btnGohome.Visible = true;
                }

                this.FillDropDown();
                ShowStudentDetails();

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
                Response.Redirect("~/notauthorized.aspx?page=AdmissionDetails.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AdmissionDetails.aspx");
        }
    }

    private void DocumentRequaired(int payType)
    {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.GetStudentDocListConfirm(Convert.ToInt32(Session["stuinfoidno"]), Convert.ToInt16(ddlDegree.SelectedValue), payType); //FOR BE & BE PTDP SAM DOC LIST
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                chkDoc.DataTextField = "DOC_NAME";
                chkDoc.DataValueField = "DOCUMENTNO";
                chkDoc.DataSource = ds.Tables[0];
                chkDoc.DataBind();
                for (int i = 0; i < chkDoc.Items.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["STATUS"].ToString() == "1")
                    {
                        chkDoc.Items[i].Selected = true;
                    }
                }
            }
        }
        chkDoc.Visible = true;
    }

    private void ShowStudentDetails()
    {
        StudentController objSC = new StudentController();
        DataTableReader dtr = null;
        Student objS = new Student();
        if (ViewState["usertype"].ToString() == "2")
        {
            dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["idno"]));
            divAdmissionDetails.Visible = false;


        }
        else
        {
            dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["stuinfoidno"]));
            divAdmissionDetails.Visible = true;
            trAdmission.Visible = true;

        }
        if (dtr != null)
        {
            if (dtr.Read())
            {
                if (ViewState["usertype"].ToString() == "2")
                {
                    objS.IdNo = Convert.ToInt32(Session["idno"]);
                }
                else
                {
                    objS.IdNo = Convert.ToInt32(Session["stuinfoidno"]);
                }
                txtDateOfAdmission.Text = dtr["ADMDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");

                this.objCommon.FillDropDownList(ddlSchoolCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "", "COLLEGE_NAME");
                ddlSchoolCollege.SelectedValue = dtr["COLLEGE_ID"] == null ? "0" : dtr["COLLEGE_ID"].ToString();

                this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlSchoolCollege.SelectedValue, "D.DEGREENO");
                ddlDegree.SelectedValue = dtr["DEGREENO"] == null ? "0" : dtr["DEGREENO"].ToString();

                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", string.Empty, "BRANCHNO");
                ddlBranch.SelectedValue = dtr["BRANCHNO"] == null ? "0" : dtr["BRANCHNO"].ToString();

                ddlBatch.SelectedValue = dtr["ADMBATCH"] == null ? "0" : dtr["ADMBATCH"].ToString();

                ddlYear.SelectedValue = dtr["YEAR"] == null ? "0" : dtr["YEAR"].ToString();
                ddlSemester.SelectedValue = dtr["SEMESTERNO"] == null ? "0" : dtr["SEMESTERNO"].ToString();


                txtSRNO.Text = dtr["ENROLLNO"] == null ? "0" : dtr["ENROLLNO"].ToString(); // Added Shirikant Ramekar 21 feb 19


                ddlcolcode.SelectedValue = dtr["COLLEGE_JSS"] == null ? "0" : dtr["COLLEGE_JSS"].ToString();

                ddlPaymentTp.SelectedValue = dtr["PAYTYPENO"] == null ? "0" : dtr["PAYTYPENO"].ToString();
                ddlSRCatg.SelectedValue = dtr["CATEGORYNO"] == null ? "0" : dtr["CATEGORYNO"].ToString();



                ddlclaim.SelectedValue = dtr["CLAIMID"] == null ? "0" : dtr["CLAIMID"].ToString();

                ddlAllotedCategory.SelectedValue = dtr["ADMCATEGORYNO"] == null ? "0" : dtr["ADMCATEGORYNO"].ToString();

                if ((ddlDegree.SelectedValue) == "1")
                {
                    if (Convert.ToInt32(ddlAllotedCategory.SelectedValue) == 3)
                    {
                    }
                }

                if ((ddlAllotedCategory.SelectedValue) == "1" || (ddlAllotedCategory.SelectedValue) == "2")
                {
                    this.DocumentRequaired(4);
                }
                else
                {
                    this.DocumentRequaired(3);
                }
            }
        }


    }

    private void FillDropDown()
    {
        try
        {
            Student objS = new Student();
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENAME");
            objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME DESC");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNAME");
            objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");
            objCommon.FillDropDownList(ddlStateOfEligibility, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
            objCommon.FillDropDownList(ddlAllotedCategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO>0", "CATEGORY");
            objCommon.FillDropDownList(ddlclaim, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO>0", "CATEGORY");

            objCommon.FillDropDownList(ddlSRCatg, "ACD_SRCATEGORY", "srcategoryno", "srcategory", "srcategoryno > 0", "srcategoryno");
            objCommon.FillDropDownList(ddlPaymentTp, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0", "PAYTYPENO");

            objS.CollegeJss = ddlcolcode.SelectedItem.Text;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AdmissionDetails.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSchoolCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlSchoolCollege.SelectedValue, "D.DEGREENO");
            ddlDegree.Focus();
        }
        else
        {
            ddlSchoolCollege.SelectedIndex = 0;
        }

    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchoolCollege.SelectedValue), "B.LONGNAME");
        ddlBranch.Focus();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0 and yearno=" + ddlYear.SelectedValue, "SEMESTERNO");
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
                if (!txtDateOfAdmission.Text.Trim().Equals(string.Empty)) objS.AdmDate = Convert.ToDateTime(txtDateOfAdmission.Text.Trim());
                objS.College_ID = Convert.ToInt32(ddlSchoolCollege.SelectedValue);
                objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
                objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                objS.BatchNo = Convert.ToInt32(ddlBatch.SelectedValue);
                objS.Year = Convert.ToInt32(ddlYear.SelectedValue);
                objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
                objS.AdmCategoryNo = Convert.ToInt32(ddlAllotedCategory.SelectedValue);
                objS.ClaimType = Convert.ToInt32(ddlclaim.SelectedValue);

                objS.EnrollNo = txtSRNO.Text.Trim();
                objS.CategoryNo = Convert.ToInt32(ddlSRCatg.SelectedValue);

                if ((ddlcolcode.SelectedValue) == "0")
                {
                    objS.CollegeJss = "";
                }
                else
                {
                    objS.CollegeJss = ddlcolcode.SelectedValue;
                }

                objS.PType = Convert.ToInt32(ddlPaymentTp.SelectedValue);
                objS.CollegeCode = Session["colcode"].ToString();



                CustomStatus cs = (CustomStatus)objSC.UpdateStudentAdmissionDetails(objS, Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]));

                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ShowStudentDetails();

                    divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('Admission Details Updated Successfully!!'); </script>";

                    Response.Redirect("~/academic/DASAStudentInformation.aspx", false);
                }
                else
                {
                    objCommon.DisplayMessage(updAdmissionDetails, "Error Occured While Updating Admission Details!!", this.Page);
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
                objCommon.DisplayMessage(this.updAdmissionDetails, "Please Search Enrollment No!!", this.Page);
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
            ScriptManager.RegisterClientScriptBlock(this.updAdmissionDetails, this.updAdmissionDetails.GetType(), "controlJSScript", sb.ToString(), true);
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