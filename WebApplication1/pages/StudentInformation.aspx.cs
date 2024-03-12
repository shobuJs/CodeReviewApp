//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : NEW STUDENT
// CREATION DATE :                                                        
// CREATED BY    :                           
// MODIFIED DATE : 
// modified by   : 
// MODIFIED DESC :                                                                      
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

public partial class Academic_StudentInformation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                FillDropDown();
                ShowStudentDetails(Convert.ToInt32(Session["idno"]));
                ShowRegisteredCourses(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["currentsession"]));
                ShowFailedCourses(Convert.ToInt32(Session["idno"]));
                BindFeeList(Convert.ToInt32(Session["idno"]));

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
                Response.Redirect("~/notauthorized.aspx?page=StudentInformation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentInformation.aspx");
        }
    }


    private void ShowStudentDetails(int idno)
    {
        StudentController objSC = new StudentController();
        DataTableReader dtr = objSC.GetStudentDetails(idno);

        if (dtr != null)
        {
            if (dtr.Read())
            {
                //Personal Details
                txtRegNo.Text = dtr["REGNO"].ToString();
                txtStudentName.Text = dtr["STUDNAME"] == DBNull.Value ? string.Empty : dtr["STUDNAME"].ToString();
                txtFatherName.Text = dtr["FATHERNAME"] == DBNull.Value ? string.Empty : dtr["FATHERNAME"].ToString();
                txtMotherName.Text = dtr["MOTHERNAME"] == DBNull.Value ? string.Empty : dtr["MOTHERNAME"].ToString();
                txtDateOfBirth.Text = dtr["DOB"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dtr["DOB"]).ToString("dd/MMM/yyyy");
                txtStudentEmail.Text = dtr["EMAILID"] == DBNull.Value ? string.Empty : dtr["EMAILID"].ToString();

                if (dtr["SEX"].ToString().Equals("M"))
                {
                    rdoMale.Checked = true;
                    rdoFemale.Checked = false;
                }
                else
                {
                    rdoFemale.Checked = true;
                    rdoMale.Checked = false;
                }

                if (dtr["MARRIED"].ToString().Equals("N"))
                {
                    rdoSingle.Checked = true;
                    rdoMarried.Checked = false;
                }
                else
                {
                    rdoSingle.Checked = false;
                    rdoMarried.Checked = true;
                }

                ddlBloodGroupNo.Text = dtr["BLOODGRPNO"] == null ? "0" : dtr["BLOODGRPNO"].ToString();
                ddlNationality.Text = dtr["NATIONALITYNO"] == null ? "0" : dtr["NATIONALITYNO"].ToString();
                if (ddlCaste.Items.FindByText(dtr["CASTE"].ToString()) != null)
                    ddlCaste.SelectedValue = ddlCaste.Items.FindByText((dtr["CASTE"] == null) ? "0" : dtr["CASTE"].ToString()).Value;
                else
                    ddlCaste.SelectedValue = "0";

                ddlCasteCategory.Text = dtr["CATEGORYNO"] == null ? "0" : dtr["CATEGORYNO"].ToString();
                ddlReligion.Text = dtr["RELIGIONNO"] == null ? "0" : dtr["RELIGIONNO"].ToString();

                imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dtr["IDNO"].ToString() + "&type=student";
                
                //Address Details
                txtLocalAddress.Text = dtr["LADDRESS"] == null ? string.Empty : dtr["LADDRESS"].ToString();
                ddlLocalCity.Text = dtr["RELIGIONNO"] == null ? "0" : dtr["RELIGIONNO"].ToString();
                txtLdistrict.Text = dtr["LDISTRICT"] == null ? string.Empty : dtr["LDISTRICT"].ToString();
                txtLocalLandlineNo.Text = dtr["LTELEPHONE"] == null ? string.Empty : dtr["LTELEPHONE"].ToString();
                txtLocalEmail.Text = dtr["LEMAIL"] == null ? string.Empty : dtr["LEMAIL"].ToString();
                txtLocalPIN.Text = dtr["LPINCODE"] == null ? string.Empty : dtr["LPINCODE"].ToString();
                ddlLocalState.Text = dtr["LSTATE"] == null ? "0" : dtr["LCITY"].ToString();
                txtLocalMobileNo.Text = dtr["LMOBILE"] == null ? string.Empty : dtr["LMOBILE"].ToString();
                
                //Admission Details
                txtDateOfAdmission.Text = dtr["ADMDATE"] == null ? string.Empty : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MMM/yyyy");
                ddlDegree.Text = dtr["DEGREENO"] == null ? "0" : dtr["DEGREENO"].ToString();
                ddlBranch.Text = dtr["BRANCHNO"] == null ? "0" : dtr["BRANCHNO"].ToString();
                ddlYear.Text = dtr["YEAR"] == null ? "0" : dtr["YEAR"].ToString();
                ddlPaymentType.Text = dtr["PTYPE"] == null ? "0" : dtr["PTYPE"].ToString();

                if (Convert.ToBoolean(dtr["HOSTELER"]) == true)
                {
                    rdoHostelerYes.Checked = true;
                    rdoHostelerNo.Checked = false;
                }
                else
                {
                    rdoHostelerNo.Checked = true;
                    rdoHostelerYes.Checked = false;
                }
                ddlBatch.Text = dtr["ADMBATCH"] == null ? "0" : dtr["ADMBATCH"].ToString();
                ddlSemester.Text = dtr["SEMESTERNO"] == null ? "0" : dtr["SEMESTERNO"].ToString();
                ddlStateOfEligibility.Text = dtr["STATENO"] == null ? "0" : dtr["STATENO"].ToString();
            }
        }
    }

    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlNationality, "ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", string.Empty, "NATIONALITYNO");
        objCommon.FillDropDownList(ddlReligion, "ACD_RELIGION", "RELIGIONNO", "RELIGION", string.Empty, "RELIGIONNO");
        objCommon.FillDropDownList(ddlLocalCity, "ACD_CITY", "CITYNO", "CITY", string.Empty, "CITY");
        objCommon.FillDropDownList(ddlLocalState, "ACD_STATE", "STATENO", "STATENAME", string.Empty, "STATENAME");
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", string.Empty, "DEGREENAME");
        objCommon.FillDropDownList(ddlBatch, "acd_admbatch", "BATCHNO", "BATCHNAME", string.Empty, "BATCHNAME");
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", string.Empty, "BRANCHNO");
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", string.Empty, "SEMESTERNAME");
        objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", string.Empty, "YEAR");
        objCommon.FillDropDownList(ddlStateOfEligibility, "ACD_STATE", "STATENO", "STATENAME", string.Empty, "STATENAME");
        objCommon.FillDropDownList(ddlCasteCategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", string.Empty, "CATEGORY");
        objCommon.FillDropDownList(ddlCaste, "ACD_CASTE", "CASTENO", "CASTE", string.Empty, "CASTE");
        objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", string.Empty, "PAYTYPENO");
        objCommon.FillDropDownList(ddlBloodGroupNo, "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME", string.Empty, "BLOODGRPNAME");
    }

    private void ShowRegisteredCourses(int idno, int sessionno)
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentInformation.ShowRegisteredCourses-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindFeeList(int idno)
    {
        try
        {
            StudentController objSC = new StudentController();
            DataSet ds = objSC.GetFeesInfo(idno);
            if (ds != null && ds.Tables.Count > 0)
            {
                lvPaidReceipts.DataSource = ds;
                lvPaidReceipts.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_studentPreRegist.BindFeeList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowFailedCourses(int idno)
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ExamRegistration.ShowFailedCourses-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
