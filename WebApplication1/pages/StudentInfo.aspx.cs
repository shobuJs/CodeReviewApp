//======================================================================================
// PROJECT NAME  : SVCE                                                          
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : Student Info                                               
// CREATION DATE : 20/07/19                                                       
// CREATED BY    : SatishT                                                     
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

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
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using DynamicAL_v2;



public partial class ACADEMIC_StudentInfo : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistrationModel objStudReg = new StudentRegistrationModel();
    StudentRegistrationController objStudRegC = new StudentRegistrationController();
    int idno = 0, finalSubmitCount = 0;

    DynamicControllerAL AL = new DynamicControllerAL();
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

    //USED FOR INITIALSING THE MASTER PAGE
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, string.Empty);
    }

    //USED FOR BYDEFAULT LOADING THE default PAGE
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            Session["Old_Url"] = Request.Url.AbsoluteUri.ToString();
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["coll_name"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.MaintainScrollPositionOnPostBack = true;
                ViewState["action"] = "add";
                //to show only sports selected in readiobuttonlist
                rdoType.SelectedValue = "1";
                GetSportsDetails();
                BindSportsDetails();
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                Session["studentID"] = null;

                //---------added for PG on 25072019------//
                if (ddlDegree.SelectedValue == "1" || ddlDegree.SelectedValue == "2")
                {
                    Session["degreetype"] = "1"; // for UG 1
                    hdndegree.Value = "1";
                }
                else if (ddlDegree.SelectedValue == "3" || ddlDegree.SelectedValue == "5")
                {
                    Session["degreetype"] = "2"; // for PG 2
                    hdndegree.Value = "2";
                }
                if (Session["usertype"].ToString() == "2"||Session["usertype"].ToString() == "18")
                {
                    liRemark.Visible = false;
                    divSearch.Visible = false;
                    btnSaveNext7.Text = "Save and Confirm";

                    idno = Convert.ToInt32(Session["idno"] == null ? 0 : Session["idno"]);
                    finalSubmitCount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_EXTRA_DETAILS", "COUNT(IDNO)", "CONFIRM=1 AND IDNO=" + idno));
                    int studApprove = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_ADMISSION_APPROVAL", "COUNT(IDNO)", "ADM_APP_STATUS=1 AND IDNO=" + idno));

                    FillDropDown();
                    if (idno > 0 || Session["userno"] != null)
                    {
                        Session["studentID"] = Session["idno"].ToString();
                        BindDocument();
                        ShowStudentDetails();
                        ShowLastExamDetails();

                        imgpreview.ImageUrl = "~/showimage.aspx?id=" + Session["studentID"] + "&type=student";
                        imgpreviewsign.ImageUrl = "~/showimage.aspx?id=" + Session["studentID"] + "&type=studentsign";

                        if (imgpreview.ImageUrl != "" && imgpreviewsign.ImageUrl != "")
                        {
                            ViewState["PhotoCount"] = 2;
                        }

                        //string PhtoSign = objCommon.LookUp("ACD_STUD_PHOTO", "ISNULL(PHOTO,'-')+'$'+ISNULL(STUD_SIGN,'-')", "IDNO = " + Session["idno"] + "");
                        //DataTable dtBlobPic = AL.Blob_GetById(blob_ConStr, blob_ContainerName, PhtoSign.Split('$')[0]);
                        //if (dtBlobPic.Rows.Count != 0)
                        //{
                        //    imgpreview.ImageUrl = Convert.ToString(dtBlobPic.Rows[0]["uri"]);
                        //}

                        //DataTable dtBlobSign = AL.Blob_GetById(blob_ConStr, blob_ContainerName, PhtoSign.Split('$')[1]);
                        //if (dtBlobSign.Rows.Count != 0)
                        //{
                        //    imgpreviewsign.ImageUrl = Convert.ToString(dtBlobSign.Rows[0]["uri"]);
                        //}

                    }

                    if (hdnTNEANo.Value != "2")  /// if NOT TNEA not allow to select
                    {
                        rbFirstGraduate.Enabled = false;
                        rbAICTEWaiver.Enabled = false;
                        rbDravidarWelfare.Enabled = false;
                    }
                    if (studApprove > 0)
                    {
                        btnSaveNext.Visible = false;
                        btnSaveNext2.Visible = false;
                        btnSaveNext3.Visible = false;
                        btnSaveNext4.Visible = false;
                        btnSaveNext6.Visible = false;
                        btnSaveNext7.Visible = false;
                        btnSaveNext8.Visible = false;
                        btnSaveNext9.Visible = false;
                        lblApprovedmsg.Visible = false;
                    }

                }
                else
                {
                    btnSaveNext7.Text = "Save and Next";
                    if (Session["usertype"].ToString() == "1")
                    {
                        liAddr.Attributes["class"] = "done";
                        liLastExam.Attributes["class"] = "done";
                        liUploads.Attributes["class"] = "done";
                        liEntrExam.Attributes["class"] = "done";
                        liParentDet.Attributes["class"] = "done";
                        liRemark.Attributes["class"] = "done";
                        //liExtraDet.Attributes["class"] = "done";
                        //liFeeDet.Attributes["class"] = "done";
                        //liSportDet.Attributes["class"] = "done";
                    }

                    // other than student login

                    divSearch.Visible = true;

                    ddlSemester.Enabled = false;
                    ddlDegree.Enabled = false;
                    ddlBranch.Enabled = false;
                    ddlAcademicYear.Enabled = false;
                    ddlAdmSemester.Enabled = false;

                    divVocational.Visible = false;

                    //Load data
                    if (Session["stuinfoidno"] != null && Convert.ToInt32(Session["stuinfoidno"]) > 0)
                    {
                        //IF SEATCH AND LINK CLICKED THEN SHOW DETAILS
                        //if (Request.QueryString["id"] == "1")
                        //{

                        objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");

                        finalSubmitCount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_EXTRA_DETAILS", "COUNT(IDNO)", "CONFIRM=1 AND IDNO=" + (Session["stuinfoidno"])));

                        Session["studentID"] = Session["stuinfoidno"] == null ? 0 : Session["stuinfoidno"];
                        FillDropDown();
                        BindDocument();
                        ShowStudentDetails();
                        ShowLastExamDetails();

                        //string PhtoSign = objCommon.LookUp("ACD_STUD_PHOTO", "ISNULL(PHOTO,'-')+'$'+ISNULL(STUD_SIGN,'-')", "IDNO = " + Session["stuinfoidno"] + "");
                        //DataTable dtBlobPic = AL.Blob_GetById(blob_ConStr, blob_ContainerName, PhtoSign.Split('$')[0]);
                        //if (dtBlobPic.Rows.Count != 0)
                        //{
                        //    imgpreview.ImageUrl = Convert.ToString(dtBlobPic.Rows[0]["uri"]);
                        //}

                        //DataTable dtBlobSign = AL.Blob_GetById(blob_ConStr, blob_ContainerName, PhtoSign.Split('$')[1]);
                        //if (dtBlobSign.Rows.Count != 0)
                        //{
                        //    imgpreviewsign.ImageUrl = Convert.ToString(dtBlobSign.Rows[0]["uri"]);
                        //}


                        //below code is to show Print Link on all the tabs // open for admin if form not complete

                        if (finalSubmitCount > 0) // if final tab data submit then print button active
                        {
                            tab1Print.Visible = true;
                            tab2Print.Visible = true;
                            tab3Print.Visible = true;
                            tab4Print.Visible = true;
                            tab5Print.Visible = true;
                            tab6Print.Visible = true;
                            tab7Print.Visible = true;
                            tab8Print.Visible = true;
                        }

                        if (hdnTNEANo.Value != "2")  /// if NOT TNEA not allow to select
                        {
                            rbFirstGraduate.Enabled = false;
                            rbAICTEWaiver.Enabled = false;
                            rbDravidarWelfare.Enabled = false;
                        }
                    }

                }

            }

        }
        else
        {
            if (Page.Request.Params["__EVENTTARGET"] != null)
            {
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                {
                    string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                    bindlist(arg[0], arg[1]);
                }
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                {
                    txtSearch.Text = string.Empty;
                    lvStudent.DataSource = null;

                    lvStudent.DataBind();
                    lblNoRecords.Text = string.Empty;
                }
            }
        }

        if (Convert.ToInt32(Session["usertype"].ToString()) == 3)
        {
            txtFAnnualIncome.Enabled = false;
            txtMAnnualIncome.Enabled = false;
            txtGAnnualIncome.Enabled = false;
        }
        else
        {
            txtFAnnualIncome.Enabled = true;
            txtMAnnualIncome.Enabled = true;
            txtGAnnualIncome.Enabled = true;
        }

        BindSportsDetails();//BIND SPORTS DETAILS

        if (Convert.ToInt32(Session["usertype"].ToString()) == 3)
        {
            liEntrExam.Visible = false;
            //liExtraDet.Visible = false;
            //liFeeDet.Visible = false;
            //liSportDet.Visible = false;
            //liSportDet.Visible = false;
            liLastExam.Visible = false;
            liUploads.Visible = false;
            lblStepParentDetails.Text = "4";

            // Faculty Advisor
            int isfacadvisor = objCommon.LookUp("USER_ACC", "1", "UA_NO IN(SELECT DISTINCT FAC_ADVISOR FROM ACD_STUDENT WHERE FAC_ADVISOR>0) AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString())) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("USER_ACC", "1", "UA_NO IN(SELECT DISTINCT FAC_ADVISOR FROM ACD_STUDENT WHERE FAC_ADVISOR>0) AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString())));

            if (isfacadvisor == 1)
            {
                ddlSemester.Enabled = false;
                ddlDegree.Enabled = false;
                ddlBranch.Enabled = false;
                ddlAdmissionType.Enabled = false;
                ddlAcademicYear.Enabled = false;
                ddlAdmSemester.Enabled = false;
                txtStudName.Enabled = true;
                txtSurname.Enabled = true;
                txtDob.Enabled = true;
                rdoGender.Enabled = true;
                txtAdhaarNo.Enabled = true;
                ddlBloodGroup.Enabled = true;
                rdoIsBloodDonate.Enabled = true;
                rdoPH.Enabled = true;
                ddlTypeOfPH.Enabled = true;
                txtAlergyDetail.Enabled = true;
                txtIdentityMark.Enabled = true;
                txtIdentityMark2.Enabled = true;
                rbtn_MaritalStatus.Enabled = true;
                ddlMtounge.Enabled = true;
                ddlCitizenship.Enabled = true;
                ddlReligion.Enabled = true;
                ddlCommunity.Enabled = true;
                ddlCasteName.Enabled = true;
                txtCommunityCode.Enabled = true;
                txtSubcaste.Enabled = true;
                RdoHosteller.Enabled = true;
                txtCollegeDistance.Enabled = true;
                txtKnownLanguage.Enabled = true;
                txtKnownForeignLang.Enabled = true;
                rdotransPort.Enabled = true;
                txtNativePlace.Enabled = true;
                liLastExam.Attributes["class"] = "done";
                liLastExam.Visible = true;
            }
            else
            {
                ddlAdmissionType.Enabled = false;
                txtStudName.Enabled = false;
                txtSurname.Enabled = false;
                txtDob.Enabled = false;
                rdoGender.Enabled = false;
                txtAdhaarNo.Enabled = false;
                ddlBloodGroup.Enabled = false;
                rdoIsBloodDonate.Enabled = false;
                rdoPH.Enabled = false;
                ddlTypeOfPH.Enabled = false;
                txtAlergyDetail.Enabled = false;
                txtIdentityMark.Enabled = false;
                txtIdentityMark2.Enabled = false;
                rbtn_MaritalStatus.Enabled = false;
                ddlMtounge.Enabled = false;
                ddlCitizenship.Enabled = false;
                ddlReligion.Enabled = false;
                ddlCommunity.Enabled = false;
                ddlCasteName.Enabled = false;
                txtCommunityCode.Enabled = false;
                txtSubcaste.Enabled = false;
                RdoHosteller.Enabled = false;
                txtCollegeDistance.Enabled = false;
                txtKnownLanguage.Enabled = false;
                txtKnownForeignLang.Enabled = false;
                rdotransPort.Enabled = false;
                txtNativePlace.Enabled = false;
            }
        }
        else
        {
            lblStepParentDetails.Text = "6";
        }


        if (Session["stuinfoidno"] != null)
        {
            if (Session["stuinfoidno"].ToString() == "-1")
            {
                Session["stuinfoidno"] = "0";

                //   ShowReport("StudentRegistration", "UserInfo.rpt");

                ScriptManager.RegisterStartupScript(this.updSports, this.GetType(),
                       "alert",
                       "alert('Parent Details Added Successfully!!');window.location ='StudentInfo.aspx#step-1';",
                                                          true);
                //  Response.Redirect(Request.RawUrl, false);             

               // ShowReport("StudentRegistration", "UserInfo.rpt");
            }
        }

    }

    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page. 
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentInfo.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentInfo.aspx");
        }
    }

    #region User defined Methods

    //BIND BLOOD GROUP NAME ,HANDICAPED NAME , MOTHER TONUGE,NATIONALITY NAME , CATEGORY,RELIGION,CASTE,BATCHNAME,DEGREE NAME , SEMESTER NAME,QUALI,QUOTANAME,OCCNAME,DESIGNATION IN DROP DOWNS LIST
    private void FillDropDown()
    {
        try
        {
            //Student-Admission details- TAB-1
            objCommon.FillDropDownList(ddlBloodGroup, "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME", "BLOODGRPNO>0", "BLOODGRPNAME");
            objCommon.FillDropDownList(ddlTypeOfPH, "ACD_PHYSICAL_HANDICAPPED", "HANDICAP_NO", "HANDICAP_NAME", "HANDICAP_NO>0", "HANDICAP_NO");
            objCommon.FillDropDownList(ddlMtounge, "ACD_MTONGUE", "MTONGUENO", "UPPER(MTONGUE) MTONGUE", "MTONGUENO>0", "MTONGUE");
            objCommon.FillDropDownList(ddlCitizenship, "ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", "NATIONALITYNO>0", "NATIONALITY");
            objCommon.FillDropDownList(ddlCommunity, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO>0", "CATEGORY");
            objCommon.FillDropDownList(ddlReligion, "ACD_RELIGION", "RELIGIONNO", "RELIGION", "RELIGIONNO>0", "RELIGION");
            objCommon.FillDropDownList(ddlCasteName, "ACD_CASTE", "CASTENO", "CASTE", "CASTENO>0", "CASTE");
            objCommon.FillDropDownList(ddlAcademicYear, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME DESC");

            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            int yearwise = 0;

            //string s = Session["idno"].ToString();
            //string s1 = Session["studentID"].ToString();

            int studid = Session["idno"] == null || Session["idno"].ToString() == "0" ? Convert.ToInt32(Session["studentID"].ToString()) : Convert.ToInt32(Session["idno"].ToString());

            if (studid != 0)
            {
                yearwise = objCommon.LookUp("ACD_DEGREE D INNER JOIN ACD_STUDENT S ON (S.DEGREENO=D.DEGREENO)", "ISNULL(D.YEARWISE,0) YEARWISE", "S.IDNO=" + studid) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_DEGREE D INNER JOIN ACD_STUDENT S ON (S.DEGREENO=D.DEGREENO)", "ISNULL(D.YEARWISE,0) YEARWISE", "S.IDNO=" + studid));
            }
            //  if (Session["YEARWISE"].ToString() == "1")
            if (yearwise == 1)
            {
                objCommon.FillDropDownList(ddlAdmSemester, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR"); //Fill Semester
            }
            else
            {
                objCommon.FillDropDownList(ddlAdmSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNAME"); //Fill Semester
            }

            objCommon.FillDropDownList(ddlFQualification, "ACD_QUALIFICATION", "QUALINO", "QUALI", "QUALINO>0", "QUALINO");
            objCommon.FillDropDownList(ddlMotherQualification, "ACD_QUALIFICATION", "QUALINO", "QUALI", "QUALINO>0", "QUALINO");
            objCommon.FillDropDownList(ddlGQualification, "ACD_QUALIFICATION", "QUALINO", "QUALI", "QUALINO>0", "QUALINO");

            objCommon.FillDropDownList(ddlAdmissionType, "ACD_ADMISSION_QUOTA", "ADMQUOTANO", "QUOTANAME", "ADMQUOTANO>0", "ADMQUOTANO");
            objCommon.FillDropDownList(ddlSemester, "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO>0", "IDTYPENO"); // Admission Type

            objCommon.FillDropDownList(ddlSSCLanguage, "ACD_MTONGUE", "MTONGUENO", "UPPER(MTONGUE) MTONGUE", "MTONGUENO>0", "MTONGUENO");
            objCommon.FillDropDownList(ddlSSCLanguage2, "ACD_MTONGUE", "MTONGUENO", "UPPER(MTONGUE) MTONGUE", "MTONGUENO>0", "MTONGUENO");
            objCommon.FillDropDownList(ddlHSCLanguage, "ACD_MTONGUE", "MTONGUENO", "UPPER(MTONGUE) MTONGUE", "MTONGUENO>0", "MTONGUENO");
            objCommon.FillDropDownList(ddlSSCBoardCategory, "ACD_BOARD_CATEGORY", "CATNO", "CATNAME", "CATNO>0 AND CATNO NOT IN (6,7)", "CATNO");
            objCommon.FillDropDownList(ddlHSCBoardCategory, "ACD_BOARD_CATEGORY", "CATNO", "CATNAME", "CATNO>0 AND CATNO NOT IN (4,5)", "CATNO");
            objCommon.FillDropDownList(ddlHSCOptionalSub, "ACD_HSC_OPTIONAL_SUBJECT", "SUBID", "SUBNAME", "SUBID>0", "SUBID");

            //Student Address details- TAB-2
            objCommon.FillDropDownList(ddlLcity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITY");
            objCommon.FillDropDownList(ddlPCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITY");
            objCommon.FillDropDownList(ddlGCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITY");
            objCommon.FillDropDownList(ddlLState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
            objCommon.FillDropDownList(ddlPState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
            objCommon.FillDropDownList(ddlGState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
            //Parent Details- Tab-7
            objCommon.FillDropDownList(ddlFofficeCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITY");
            objCommon.FillDropDownList(ddlMofficeCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITY");
            objCommon.FillDropDownList(ddlGofficeCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITY");
            objCommon.FillDropDownList(ddlFofficeState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
            objCommon.FillDropDownList(ddlMofficeState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
            objCommon.FillDropDownList(ddlGofficeState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
            objCommon.FillDropDownList(ddlFOccupation, "ACD_OCCUPATION", "OCCUPATION", "OCCNAME", "OCCUPATION>0", "OCCUPATION");
            objCommon.FillDropDownList(ddlMOccupation, "ACD_OCCUPATION", "OCCUPATION", "OCCNAME", "OCCUPATION>0", "OCCUPATION");
            objCommon.FillDropDownList(ddlGOccupation, "ACD_OCCUPATION", "OCCUPATION", "OCCNAME", "OCCUPATION>0", "OCCUPATION");
            objCommon.FillDropDownList(ddlFDesignation, "ACD_DESIGNATION", "DESIGNO", "DESIGNATION", "DESIGNO>0", "DESIGNO");
            objCommon.FillDropDownList(ddlMDesignation, "ACD_DESIGNATION", "DESIGNO", "DESIGNATION", "DESIGNO>0", "DESIGNO");
            objCommon.FillDropDownList(ddlGDesignation, "ACD_DESIGNATION", "DESIGNO", "DESIGNATION", "DESIGNO>0", "DESIGNO");
            objCommon.FillDropDownList(ddlFQualification, "ACD_QUALIFICATION", "QUALINO", "QUALI", "QUALINO>0", "QUALINO");
            objCommon.FillDropDownList(ddlRemarkType, "ACD_REMARK_MASTER", "REMARK_ID", "REMARK_TYPE", "REMARK_ID>0", "REMARK_ID");
            ///Added by Neha Baranwal - 02 Oct 19
            ///step - 9 Sports and Achievement 
            objCommon.FillDropDownList(ddlLevelOfParticipation, "ACD_PARTICIPATION_LEVEL_MASTER", "PARTICIPATION_NO", "PARTICIPATION_LEVEL", "PARTICIPATION_NO>0", "PARTICIPATION_NO");
            objCommon.FillDropDownList(ddlAchievement, "ACD_ACHIEVEMENT_MASTER", "ACHIEVEMENT_NO", "ACHIEVEMENT_NAME", "ACHIEVEMENT_NO>0", "ACHIEVEMENT_NO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentInfoEntry.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //BIND DOCUMENT IN LIST VIEW.
    private void BindDocument()
    {
        DataSet dslist = null;

        dslist = objStudRegC.GetDocumentList(Convert.ToInt32(Session["studentID"]));


        if (dslist != null && dslist.Tables.Count > 0 && dslist.Tables[0].Rows.Count > 0)
        {
            lvDocumentList.Visible = true;
            lvDocumentList.DataSource = dslist;
            lvDocumentList.DataBind();
            //btnuploadDocuments.Enabled = true;

        }
        else
        {
            lvDocumentList.Visible = false;
            lvDocumentList.DataSource = null;
            lvDocumentList.DataBind();
            //btnuploadDocuments.Enabled = false;

        }

    }

    //SHOWING STUDENT DETAILS , EXAM DETAILS , ETRANCE EXAM DETAILS, MOTHER DETAILS, FATHER DETAILS, GAURDIAN DETAILS, ADDRESS DETAILS,OTHER DETAILS
    private void ShowStudentDetails()
    {
        DataTableReader dtr = null;

        dtr = objStudRegC.GetStudentDetails(Convert.ToInt32(Session["studentID"]));

        if (dtr != null)
        {
            if (dtr.Read())
            {
                try
                {
                    //Student Admission Details
                    txtidno.Text = dtr["IDNO"].ToString();
                    Session["studidno"] = dtr["IDNO"].ToString();
                    txtStudName.Text = dtr["STUDNAME"].ToString();
                    Session["YEARWISE"] = dtr["YEARWISE"].ToString();
                    // Added by Pritish on 04022021
                    txtFirstname.Text = dtr["STUDFIRSTNAME"].ToString();
                    txtMiddlename.Text = dtr["STUDMIDDLENAME"].ToString();

                    txtSurname.Text = dtr["STUDLASTNAME"].ToString();
                    txtDob.Text = dtr["DOB"].ToString();
                    if (dtr["SEX"].ToString() == "M")
                    {
                        rdoGender.SelectedValue = "1";
                    }
                    else if (dtr["SEX"].ToString() == "F")
                    {
                        rdoGender.SelectedValue = "2";
                    }
                    else if (dtr["SEX"].ToString() == "T")
                    {
                        rdoGender.SelectedValue = "3";
                    }
                    //Added by Abhinay Lad [16-09-2019]
                    if (dtr["MARRIED"].ToString() == "Y")
                    {
                        rbtn_MaritalStatus.SelectedValue = "1";
                    }
                    else if (dtr["MARRIED"].ToString() == "D")
                    {
                        rbtn_MaritalStatus.SelectedValue = "3";
                    }
                    else
                    {
                        rbtn_MaritalStatus.SelectedValue = "2";
                    }
                    //End


                    string idno = objCommon.LookUp("ACD_STUD_PHOTO", "IDNO", "IDNO=" + Convert.ToInt32(txtidno.Text.Trim()));
                    if (idno != "")
                    {
                        imgpreview.ImageUrl = "~/showimage.aspx?id=" + dtr["IDNO"].ToString() + "&type=student";
                        imgpreviewsign.ImageUrl = "~/showimage.aspx?id=" + dtr["IDNO"].ToString() + "&type=studentsign";
                    }


                    txtAdhaarNo.Text = dtr["AADHARCARDNO"].ToString();
                    ddlBloodGroup.SelectedValue = dtr["BLOODGRPNO"].ToString();
                    rdoIsBloodDonate.SelectedValue = dtr["WILLINGTOBLOODDONATE"].ToString() == "Y" ? "1" : "2";
                    rdoPH.SelectedValue = dtr["PHYSICALLY_HANDICAPPED"].ToString() == "Y" ? "1" : "2";
                    ddlTypeOfPH.SelectedValue = dtr["TYPE_OF_PHYSICALLY_HANDICAP"].ToString();
                    if (ddlTypeOfPH.SelectedValue != "0")
                        ddlTypeOfPH.Enabled = true;

                    txtAlergyDetail.Text = dtr["ALERGY_DETAIL"].ToString();
                    txtIdentityMark.Text = dtr["IDENTI_MARK"].ToString();
                    txtIdentityMark2.Text = dtr["IDENTITY_MARK2"].ToString();
                    ddlMtounge.SelectedValue = dtr["MTOUNGENO"].ToString();
                    ddlCitizenship.SelectedValue = dtr["CITIZENSHIP"].ToString();
                    txtCommunityCode.Text = dtr["COMMUNITY_CODE"].ToString();
                    txtSubcaste.Text = dtr["SUB_CASTE"].ToString();
                    ddlCommunity.SelectedValue = dtr["CATEGORYNO"].ToString();
                    ddlReligion.SelectedValue = dtr["RELIGIONNO"].ToString();
                    ddlCasteName.SelectedValue = dtr["CASTE"].ToString();
                    if (Convert.ToBoolean(dtr["HOSTELER"]) == true)
                    {
                        RdoHosteller.SelectedValue = "1";  // YES
                        txtCollegeDistance.Enabled = true;
                    }
                    else
                    {
                        RdoHosteller.SelectedValue = "2"; //NO
                        txtCollegeDistance.Enabled = false;
                    }
                    txtCollegeDistance.Text = dtr["CLG_HOME_DISTANCE"].ToString();
                    txtKnownLanguage.Text = dtr["KNOWN_LANGUAGE"].ToString();
                    txtKnownForeignLang.Text = dtr["KNOWN_FLANGUAGE"].ToString();
                    rdotransPort.SelectedValue = dtr["TRANSPORT"].ToString() == "1" ? "1" : "2";
                    ddlAcademicYear.SelectedValue = dtr["ACADEMIC_YEAR"].ToString();

                    string SSSS = dtr["COLLEGE_ID"].ToString();
                    ddlCollege.SelectedValue = dtr["COLLEGE_ID"].ToString();
                    if (ddlCollege.SelectedIndex > 0)
                    {
                        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENAME");

                        ddlDegree.SelectedValue = dtr["DEGREENO"].ToString();
                        string degreeno = dtr["DEGREENO"].ToString();
                        string degreetype = string.Empty;

                        if (degreeno == "1" || degreeno == "2")
                        {
                            Session["degreetype"] = "1"; // for UG 1
                            hdndegree.Value = "1";
                        }
                        else if (degreeno == "3" || degreeno == "5")
                        {
                            Session["degreetype"] = "2"; // for PG 2
                            hdndegree.Value = "2";
                        }
                    }

                    if (ddlDegree.SelectedIndex > 0)
                    {
                        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH M ON (B.BRANCHNO = M.BRANCHNO)", "B.BRANCHNO", "LONGNAME", "B.BRANCHNO>0 AND DEGREENO=" + ddlDegree.SelectedValue + "", "LONGNAME");
                        ddlBranch.SelectedValue = dtr["BRANCHNO"].ToString();
                    }

                    ddlAdmSemester.SelectedValue = dtr["SEMESTERNO"].ToString();

                    ddlAdmissionType.SelectedValue = dtr["IDTYPE"].ToString();
                    ddlSemester.SelectedValue = dtr["IDTYPE"].ToString();  // IDTYPE // application type
                    if (ddlSemester.SelectedValue == "2")
                    {
                        hdnAdmissionType.Value = "2";  //LATERAL ENTRY
                    }
                    else
                    {
                        hdnAdmissionType.Value = "1"; // REGULAR ENTRY(OTHER THAN LATERAL)
                    }
                    txtNativePlace.Text = dtr["BIRTH_PLACE"].ToString();

                    //Address Details
                    txtLAddressLine1.Text = dtr["LADDRESS"].ToString();
                    ddlLcity.SelectedValue = dtr["LCITY"].ToString();
                    txtLOtherCity.Text = dtr["LOTHERCITY"].ToString();

                    if (!txtLOtherCity.Text.Trim().Equals(string.Empty))
                    {
                        txtLOtherCity.Visible = true;
                        ddlLcity.Visible = false;
                        lbnLOtherCity.Text = "Select City";
                    }
                    else
                    {
                        txtLOtherCity.Visible = false;
                        ddlLcity.Visible = true;
                        lbnLOtherCity.Text = "Add Other City";
                    }

                    ddlLState.SelectedValue = dtr["LSTATE"].ToString();
                    txtLPin.Text = dtr["LPINCODE"].ToString();
                    txtLSTDCode.Text = dtr["LSTDCODE"].ToString();
                    txtLLandLineNo.Text = dtr["LTELEPHONE"].ToString();
                    txtLEmail.Text = dtr["LEMAIL"].ToString();
                    txtLStudMobile.Text = dtr["LMOBILE"].ToString();

                    txtEmergencyContactNum.Text = dtr["EMERGENCY_NUMBER"].ToString();
                    txtEmergencyContactName.Text = dtr["EMERGENCY_NAME"].ToString();
                    txtEmergencyContactRelation.Text = dtr["EMERGENCY_RELATION"].ToString();
                    txtEmergencyContactEmail.Text = dtr["EMERGENCY_EMAIL"].ToString();

                    if (dtr["SENDSMS"].ToString() == "F")
                    {
                        ddlSMSSend.SelectedValue = "1";
                    }
                    else if (dtr["SENDSMS"].ToString() == "M")
                    {
                        ddlSMSSend.SelectedValue = "2";
                    }
                    else
                    {
                        ddlSMSSend.SelectedValue = "0";
                    }

                    txtPAddressLine1.Text = dtr["PADDRESS"].ToString();
                    ddlPCity.SelectedValue = dtr["PCITY"].ToString();
                    txtPOthercity.Text = dtr["POTHERCITY"].ToString();

                    if (!txtPOthercity.Text.Trim().Equals(string.Empty))
                    {
                        txtPOthercity.Visible = true;
                        ddlPCity.Visible = false;
                        lbtnPOtherCity.Text = "Select City";
                    }
                    else
                    {
                        txtPOthercity.Visible = false;
                        ddlPCity.Visible = true;
                        lbtnPOtherCity.Text = "Add Other City";
                    }

                    ddlPState.SelectedValue = dtr["PSTATE"].ToString();
                    txtPPin.Text = dtr["PPINCODE"].ToString();
                    txtPSTDCode.Text = dtr["PSTDCODE"].ToString();
                    txtPLandLine.Text = dtr["PTELEPHONE"].ToString();
                    txtGaddressLine1.Text = dtr["GADDRESS"].ToString();
                    ddlGCity.SelectedValue = dtr["GCITYNO"].ToString();
                    txtGOthercity.Text = dtr["GOTHERCITY"].ToString();

                    if (!txtGOthercity.Text.Trim().Equals(string.Empty))
                    {
                        txtGOthercity.Visible = true;
                        ddlGCity.Visible = false;
                        lnkbtnGothercity.Text = "Select City";
                    }
                    else
                    {
                        txtGOthercity.Visible = false;
                        ddlGCity.Visible = true;
                        lnkbtnGothercity.Text = "Add Other City";
                    }

                    ddlGState.SelectedValue = dtr["GSTATENO"].ToString();
                    txtGPin.Text = dtr["GPINCODE"].ToString();
                    txtGSTDCode.Text = dtr["GSTDCODE"].ToString();
                    txtGLandLineNumber.Text = dtr["GPHONE"].ToString();
                    txtGEmailId.Text = dtr["GEMAIL"].ToString();
                    txtGMobile.Text = dtr["GMOBILE"].ToString();

                    txtGRelation.Text = dtr["RELATION_GUARDIAN"].ToString();

                    //ENTRANCE EXAM DETAILS
                    txtEntranceName.Text = dtr["QUALIFYEXAMNAME"].ToString();
                    txtEntranceRollno.Text = dtr["QEXMROLLNO"].ToString();
                    txtCutOffMarks.Text = dtr["CUTOFF_MARKS"].ToString();
                    txtOverallMark.Text = dtr["OVERALL_MARKS"].ToString();
                    txtCommunityRank.Text = dtr["COMMUNITY_RANK"].ToString();
                    txtTNEAAppliactionNo.Text = dtr["TNEA_APPLICATION_NO"].ToString();
                    txtAknowledgeRecNo.Text = dtr["ACK_REC_NO"].ToString();
                    txtAdmissionOrderNo.Text = dtr["ADM_ORDER_NO"].ToString();
                    txtAdvPaymentAmt.Text = dtr["ADV_PAYMENT_AMOUNT"].ToString();
                    txtConsortiumNo.Text = dtr["CATNO"].ToString();   // ADDED BY NARESH BEERLA ON 20072020
                    txtAdmOrderDate.Text = dtr["ADM_ORDER_DATE"].ToString();
                    txtAckRecDate.Text = dtr["ACK_RECEIPT_DATE"].ToString();
                    txtOverAllRank.Text = dtr["OVERALL_RANK"].ToString();
                    txtApplicationNo.Text = dtr["DOTE_APPLICATION_NO"].ToString();
                    txtAllotmentOrderNo.Text = dtr["DOTE_ALLOTMENT_NO"].ToString();
                    txtAllotmentOrderDate.Text = dtr["DOTE_ALLOTMENT_DATE"].ToString();
                    txtPercentile.Text = dtr["PERCENTILE"].ToString();

                    //PARENT DETAILS
                    if (dtr["SINGLE_PARENT"].ToString() == "Y")
                    {
                        rdoSingleParent.SelectedValue = "1";
                        ddlSelectParent.Enabled = true;

                        if (dtr["PARENT_TYPE"].ToString().Equals("F"))
                        {
                            ddlSelectParent.SelectedValue = "1";
                        }
                        else if (dtr["PARENT_TYPE"].ToString().Equals("M"))
                        {
                            ddlSelectParent.SelectedValue = "2";
                        }
                    }
                    else
                    {
                        rdoSingleParent.SelectedValue = "2";
                        ddlSelectParent.Enabled = false;
                    }

                    if (ddlSelectParent.SelectedValue == "1")
                    {
                        divFather1.Visible = true;
                        divFather2.Visible = true;
                        divMother1.Visible = false;
                        divMother2.Visible = false;
                        lblFather2.Visible = true;
                    }
                    else if (ddlSelectParent.SelectedValue == "2")
                    {
                        divFather1.Visible = false;
                        divFather2.Visible = false;
                        divMother1.Visible = true;
                        divMother2.Visible = true;
                        lblFather2.Visible = false;
                    }
                    else
                    {
                        divFather1.Visible = true;
                        divFather2.Visible = true;
                        divMother1.Visible = true;
                        divMother2.Visible = true;
                        lblFather2.Visible = true;
                    }

                    txtFatherName.Text = dtr["FATHERNAME"].ToString();
                    txtFatherInitial.Text = dtr["FATHERLASTNAME"].ToString();
                    ddlFQualification.SelectedValue = dtr["FATHER_QUAL"].ToString();
                    ddlFOccupation.SelectedValue = dtr["OCCUPATIONNO"].ToString();
                    txtFOrganizationName.Text = dtr["FATHERJOBDETAIL"].ToString();

                    ddlFDesignation.SelectedValue = dtr["FATHER_DESIG"].ToString();
                    ddlMDesignation.SelectedValue = dtr["MOTHER_DESIG"].ToString();
                    ddlGDesignation.SelectedValue = dtr["GUARDIAN_DESIG"].ToString();
                    txtOtherDesignation.Text = dtr["OTHER_DESIG"].ToString();
                    txtMOtherDesignation.Text = dtr["M_OTHER_DESIG"].ToString();    //Added by Abhinay Lad [27-06-2019].
                    txtGOtherDesignation.Text = dtr["G_OTHER_DESIG"].ToString();    //Added by Abhinay Lad [27-06-2019].
                    if (!txtOtherDesignation.Text.Trim().Equals(string.Empty))
                    {
                        txtOtherDesignation.Visible = true;
                        ddlFDesignation.Visible = false;
                        lbtnFOtherDesig.Text = "Select Designation";
                    }
                    else
                    {
                        txtOtherDesignation.Visible = false;
                        ddlFDesignation.Visible = true;
                        lbtnFOtherDesig.Text = "Add Other Designation";
                    }
                    //For Mother
                    if (!txtMOtherDesignation.Text.Trim().Equals(string.Empty))
                    {
                        txtMOtherDesignation.Visible = true;
                        ddlMDesignation.Visible = false;
                        lbtnMOtherDesig.Text = "Select Designation";
                    }
                    else
                    {
                        txtMOtherDesignation.Visible = false;
                        ddlMDesignation.Visible = true;
                        lbtnMOtherDesig.Text = "Add Other Designation";
                    }
                    //for Gaurdian
                    if (!txtGOtherDesignation.Text.Trim().Equals(string.Empty))
                    {
                        txtGOtherDesignation.Visible = true;
                        ddlGDesignation.Visible = false;
                        lbtnGOtherDesig.Text = "Select Designation";
                    }
                    else
                    {
                        txtGOtherDesignation.Visible = false;
                        ddlGDesignation.Visible = true;
                        lbtnGOtherDesig.Text = "Add Other Designation";
                    }
                    //Add Qualification
                    ddlFQualification.SelectedValue = dtr["FATHER_QUAL"].ToString();
                    ddlMotherQualification.SelectedValue = dtr["MOTHER_QUAL"].ToString();
                    ddlGQualification.SelectedValue = dtr["GUARDIAN_QUAL"].ToString();
                    txtFOtherQaulification.Text = dtr["F_OTHER_QUAL"].ToString();
                    txtMOtherQualification.Text = dtr["M_OTHER_QUAL"].ToString();
                    txtGOtherQualification.Text = dtr["G_OTHER_QUAL"].ToString();
                    if (!txtFOtherQaulification.Text.Trim().Equals(string.Empty))
                    {
                        txtFOtherQaulification.Visible = true;
                        ddlFQualification.Visible = false;
                        lbtnFOtherQualification.Text = "Select Qualification";
                    }
                    else
                    {
                        txtFOtherQaulification.Visible = false;
                        ddlFQualification.Visible = true;
                        lbtnFOtherQualification.Text = "Add Other Qualification";
                    }
                    //For Mother
                    if (!txtMOtherQualification.Text.Trim().Equals(string.Empty))
                    {
                        txtMOtherQualification.Visible = true;
                        ddlMotherQualification.Visible = false;
                        lbtnMOtherQualification.Text = "Select Qualification";
                    }
                    else
                    {
                        txtMOtherQualification.Visible = false;
                        ddlMotherQualification.Visible = true;
                        lbtnMOtherQualification.Text = "Add Other Qualification";
                    }
                    //for Gaurdian
                    if (!txtGOtherQualification.Text.Trim().Equals(string.Empty))
                    {
                        txtGOtherQualification.Visible = true;
                        ddlGQualification.Visible = false;
                        lbtnGOtherQualification.Text = "Select Qualification";
                    }
                    else
                    {
                        txtGOtherQualification.Visible = false;
                        ddlGQualification.Visible = true;
                        lbtnGOtherQualification.Text = "Add Other Qualification";
                    }

                    txtFAnnualIncome.Text = dtr["ANNUAL_INCOME"].ToString();
                    txtFMobileNo.Text = dtr["FATHERMOBILE"].ToString();

                    txtFEmail.Text = dtr["FATHER_EMAIL"].ToString();
                    txtFPANNumber.Text = dtr["FATHER_PAN_NO"].ToString();
                    txtMEmail.Text = dtr["MOTHER_EMAIL"].ToString();
                    txtMPANNumber.Text = dtr["MOTHER_PAN_NO"].ToString();

                    txtFatherAadhar.Text = dtr["FATHER_ADDHARNO"].ToString();
                    txtFOfficeAddress.Text = dtr["ORGANISATION_ADDRESS"].ToString();
                    ddlFofficeCity.SelectedValue = dtr["FATHER_OFFICE_CITY"].ToString();
                    ddlFofficeState.SelectedValue = dtr["FATHER_OFFICE_STATE"].ToString();
                    txtFofficePin.Text = dtr["FATHER_OFFICE_PIN"].ToString();
                    txtFofficeSTD.Text = dtr["FATHER_OFFICE_STD"].ToString();
                    txtFofficeLandline.Text = dtr["FATHER_OFFICE_PHONE"].ToString();
                    txtMotherName.Text = dtr["MOTHERNAME"].ToString();
                    txtMotherInitial.Text = dtr["MOTHERLASTNAME"].ToString();
                    ddlMotherQualification.SelectedValue = dtr["MOTHER_QUAL"].ToString();
                    ddlMOccupation.SelectedValue = dtr["MOTHER_OCCUPATIONNO"].ToString();
                    txtMotherMobile.Text = dtr["MOTHERMOBILE"].ToString();
                    txtMotherAadhar.Text = dtr["MOTHER_ADDHARNO"].ToString();
                    txtMofficeAdd.Text = dtr["M_ORGANISATION_ADDRESS"].ToString();
                    ddlMofficeCity.SelectedValue = dtr["MOTHER_OFFICE_CITY"].ToString();
                    ddlMofficeState.SelectedValue = dtr["MOTHER_OFFICE_STATE"].ToString();
                    txtMofficePin.Text = dtr["MOTHER_OFFICE_PIN"].ToString();
                    txtMSTDCode.Text = dtr["MOTHER_OFFICE_STD"].ToString();
                    txtMOfficeLandLine.Text = dtr["MOTHER_OFFICE_PHONE"].ToString();
                    txtGuardianName.Text = dtr["GUARDIANNAME"].ToString();
                    txtGuardianInitial.Text = dtr["GUARDIAN_LASTNAME"].ToString();
                    ddlGQualification.SelectedValue = dtr["GUARDIAN_QUAL"].ToString();
                    ddlGOccupation.SelectedValue = dtr["GOCCUPATIONNAME"].ToString();
                    txtGWorkingOrg.Text = dtr["ORGANISATION_NAME"].ToString();
                    ddlGDesignation.SelectedValue = dtr["GUARDIAN_DESIG"].ToString();
                    txtGAnnualIncome.Text = dtr["G_ANNUAL_INCOME"].ToString();
                    txtGOfficeAddress.Text = dtr["G_ORGANISATION_ADDRESS"].ToString();
                    ddlGofficeCity.SelectedValue = dtr["G_OFFICE_CITY"].ToString();
                    ddlGofficeState.SelectedValue = dtr["G_OFFICE_STATE"].ToString();
                    txtGofficePin.Text = dtr["G_OFFICE_PIN"].ToString();
                    txtGofficeSTD.Text = dtr["G_OFFICE_STD"].ToString();
                    txtGofficeLandline.Text = dtr["G_OFFICE_PHONE"].ToString();
                    txtMOrganizationName.Text = dtr["MOTHER_ORG_NAME"].ToString();
                    txtMAnnualIncome.Text = dtr["MOTHER_ANNUAL_INCOME"].ToString();

                    //------------------Extra Details--------------------------------------//
                    txtRelativeDetails.Text = dtr["RELATIVE_DETAIL"].ToString();
                    txtReasonToChoose.Text = dtr["REASON_TO_CHOOSE"].ToString();
                    txtCommunityCertNo.Text = dtr["COMMUNITY_CERT_NO"].ToString();
                    txtCommunityCertIssueDate.Text = dtr["COMMUNITY_CERT_ISSUE_DATE"].ToString();
                    txtCommunityCertAuthority.Text = dtr["COMMUNITY_CERT_AUTHORITY"].ToString();
                    txtTransferCertNo.Text = dtr["TRANSFER_CERT_NO"].ToString();
                    txtTransferCertDate.Text = dtr["TRANSFER_CERT_DATE"].ToString();
                    txtConductCertNo.Text = dtr["CONDUCT_CERT_NO"].ToString();
                    txtConductCertDate.Text = dtr["CONDUCT_CERT_DATE"].ToString();
                    txtFirstAppearanceRegno.Text = dtr["FIRST_APPEARANCE_REGNO"].ToString();
                    txtFirstAppearanceYear.Text = dtr["FIRST_APPEARANCE_YEAR"].ToString();
                    txtSecondAppearanceNo.Text = dtr["SECOND_APPEARANCE_REGNO"].ToString();
                    txtSecondAppearanceYear.Text = dtr["SECOND_APPEARANCE_YEAR"].ToString();
                    txtThirdAppearanceRegno.Text = dtr["THIRD_APPEARANCE_REGNO"].ToString();
                    txtThirdAppearanceYear.Text = dtr["THIRT_APPEARANCE_YEAR"].ToString();
                    txtMinorityCertificateNo.Text = dtr["MINORITY_CERT_NO"].ToString();
                    txtMinorityCertIssueDate.Text = dtr["MINORITY_CERT_DATE"].ToString();
                    txtMinorityCertAuthority.Text = dtr["MINORITY_CERT_AUTHORITY"].ToString();

                    //-------------------------Fees Details------------------------------//
                    if (dtr["FIRST_GRADUATE"].ToString() == "Y")
                    {
                        rbFirstGraduate.SelectedValue = "1";
                        txtFirstGraduateCertNo.Enabled = true;
                        txtFirstGraduateCertDate.Enabled = true;
                        txtFirstGraduateCertAuth.Enabled = true;
                    }
                    else
                    {
                        rbFirstGraduate.SelectedValue = "2";
                        txtFirstGraduateCertNo.Enabled = false;
                        txtFirstGraduateCertDate.Enabled = false;
                        txtFirstGraduateCertAuth.Enabled = false;
                    }
                    txtFirstGraduateCertNo.Text = dtr["FIRST_GRADUATE_CERT_NO"].ToString();
                    txtFirstGraduateCertDate.Text = dtr["FIRST_GRADUATE_CERT_DATE"].ToString();
                    txtFirstGraduateCertAuth.Text = dtr["FIRST_GRADUATE_CERT_AUTH"].ToString();

                    if (dtr["AICTE_WAIVER"].ToString() == "Y")
                    {
                        rbAICTEWaiver.SelectedValue = "1";
                        txtAICTECertNo.Enabled = true;
                        txtAICTECertDate.Enabled = true;
                        txtAICTECertAuth.Enabled = true;
                    }
                    else
                    {
                        rbAICTEWaiver.SelectedValue = "2";
                        txtAICTECertNo.Enabled = false;
                        txtAICTECertDate.Enabled = false;
                        txtAICTECertAuth.Enabled = false;
                    }

                    txtAICTECertNo.Text = dtr["AICTE_CERT_NO"].ToString();
                    txtAICTECertDate.Text = dtr["AICTE_CERT_DATE"].ToString();
                    txtAICTECertAuth.Text = dtr["AICTE_CERT_AUTH"].ToString();
                    if (dtr["DRAVIDAR_WELFARE"].ToString() == "Y")
                    {
                        rbDravidarWelfare.SelectedValue = "1";
                        txtWelfareCertNo.Enabled = true;
                        txtWelfareCertDate.Enabled = true;
                        txtWelfareCertAuth.Enabled = true;
                    }
                    else
                    {
                        rbDravidarWelfare.SelectedValue = "2";
                        txtWelfareCertNo.Enabled = false;
                        txtWelfareCertDate.Enabled = false;
                        txtWelfareCertAuth.Enabled = false;
                    }

                    txtWelfareCertNo.Text = dtr["WELFARE_CERT_NO"].ToString();
                    txtWelfareCertDate.Text = dtr["WELFARE_CERT_DATE"].ToString();
                    txtWelfareCertAuth.Text = dtr["WELFARE_CERT_AUTH"].ToString();

                    if (Session["usertype"].ToString() != "2")
                    {
                        ddlYear.SelectedValue = dtr["SEMESTERNO"].ToString();
                        // ddlRemarkType.SelectedValue = dtr["REMARKTYPE"].ToString();
                        txtRemark.Text = dtr["REMARK"].ToString();
                    }

                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUCommon.ShowError(Page, "PresentationLayer_StudentRegistration.ShowStudentDetails-> " + ex.Message + " " + ex.StackTrace);
                    else
                        objUCommon.ShowError(Page, "Server UnAvailable");
                }
            }
        }
    }

    //SHOWING LAST EXAM DETAILS LIKE DIPLOMA ,HSC,SSC IN TEXT BOX CONTROLLERS
    private void ShowLastExamDetails()
    {
        DataTableReader dtr = null;

        dtr = objStudRegC.GetLastExamDetails(Convert.ToInt32(Session["studentID"]));

        if (dtr != null)
        {
            if (dtr.Read())
            {
                try
                {
                    //Last Exam Details//SSC
                    ddlSSCBoardCategory.SelectedValue = dtr["SSC_BOARD_CATEGORY"].ToString();
                    ddlSSCLanguage.SelectedValue = dtr["SSC_LANGUAGE"].ToString();
                    ddlSSCLanguage2.SelectedValue = dtr["SSC_LANGUAGE2"].ToString();

                    txtSSCEnglishObtMark.Text = dtr["SSC_ENG_OBTMARK"].ToString();
                    txtSSCEnglishMaxMark.Text = dtr["SSC_ENG_MAXMARK"].ToString();
                    txtSSCEnglishPer.Text = dtr["SSC_ENG_PER"].ToString();
                    txtSSCMathsObtMark.Text = dtr["SSC_MATHS_OBTMARK"].ToString();
                    txtSSCMathsMaxMark.Text = dtr["SSC_MATH_MAXMARK"].ToString();
                    txtSSCMathsPer.Text = dtr["SSC_MATH_PER"].ToString();
                    txtSSCScienceObtMark.Text = dtr["SSC_SCI_OBTMARK"].ToString();
                    txtSSCScienceMaxMark.Text = dtr["SSC_SCIENCE_MAXMARK"].ToString();
                    txtSSCSciencePer.Text = dtr["SSC_SCI_PER"].ToString();
                    txtSSCSocialScienceObtMark.Text = dtr["SSC_SOSCI_OBTMARK"].ToString();
                    txtSSCSocialScienceMaxMark.Text = dtr["SSC_SOSCI_MAXMARK"].ToString();
                    txtSSCSocialSciencePer.Text = dtr["SSC_SOSCI_PER"].ToString();
                    txtSSCTotalMarkScore.Text = dtr["SSC_TOTAL_MARKOBT"].ToString();
                    txtSSCYearofPassing.Text = dtr["SSC_YEAR_OFEXAM"].ToString();
                    txtSSCMediumOfInstruction.Text = dtr["SSC_MEDIUM_INSTRUCTION"].ToString();
                    txtSSCMarkCertNo.Text = dtr["SSC_MARK_CERTNO"].ToString();
                    txtSSCPassCertNo.Text = dtr["SSC_PASS_CERT_NO"].ToString();
                    txtSSCTMRNo.Text = dtr["SSC_TMRNO"].ToString();
                    txtSSCRegisterNo.Text = dtr["SSC_REGISTERNO"].ToString();
                    txtSSCInstituteName.Text = dtr["SSC_INSTITUTENAME"].ToString();
                    txtSSCTotalPer.Text = dtr["SSC_PER"].ToString();
                    txtSSCInstituteAddress.Text = dtr["SSC_INSTITUREADDR"].ToString();
                    txtISEComputerApplicationObtMark.Text = dtr["SSC_COMP_APP_OBTMARK"].ToString();
                    txtISEComputerApplicationMaxMark.Text = dtr["SSC_COMP_APP_MAXMARK"].ToString();
                    txtISEComputerApplicationPer.Text = dtr["SSC_COMP_APP_PER"].ToString();
                    txtISEHistoryObtMark.Text = dtr["SSC_HIS_CIV_OBT"].ToString();
                    txtISEHistoryMaxMark.Text = dtr["SSC_HIS_CIV_MAX"].ToString();
                    txtISEHistoryPer.Text = dtr["SSC_HIS_CIV_PER"].ToString();
                    txtSSCLangMaxMark.Text = dtr["SSC_LANGMAXMARK"].ToString();
                    txtSSCLang2MaxMark.Text = dtr["SSCLANGUAGE_MAXMARK2"].ToString();

                    txtSSCLangObtMark.Text = dtr["SSC_LANGOBTMARK"].ToString();
                    txtSSCLang2ObtMark.Text = dtr["SSCLANGUAGE_OBTMARK2"].ToString();

                    txtSSCLangPer.Text = dtr["SSC_LANGPER"].ToString();
                    txtSSCLang2Per.Text = dtr["SSCLANGUAGE_PER2"].ToString();

                    txtSSCTransferCertNo.Text = dtr["SSCTRANSFER_CERT_NO"].ToString();

                    //Added by Naresh Beerla [29092020]
                    txtOtherBoardSSC.Text = dtr["OTHER_BOARD_CATEGORY_SSC"].ToString();
                    rblInputSystem.SelectedValue = dtr["EVAL_TYPE_SSC"].ToString();
                    //Added by Naresh Beerla [29092020]

                    //Last Exam Details//HSC
                    ddlHSCBoardCategory.SelectedValue = dtr["HSC_BOARD_CATEGORY"].ToString();
                    //for vocational and ise board vocational subjects visible
                    if (ddlHSCBoardCategory.SelectedValue == "7" || ddlHSCBoardCategory.SelectedValue == "9")
                    {
                        divVocational.Visible = true;
                        divNonVocational.Visible = false;
                    }
                    else
                    {
                        divNonVocational.Visible = true;
                        divVocational.Visible = false;
                    }
                    ddlHSCLanguage.SelectedValue = dtr["HSC_LANGUAGE"].ToString();
                    txtHSCEnglishObtMark.Text = dtr["HSC_ENG_OBTMARK"].ToString();
                    txtHSCEnglishMaxMark.Text = dtr["HSC_ENG_MAXMARK"].ToString();
                    txtHSCEnglishPer.Text = dtr["HSC_ENG_PER"].ToString();
                    txtHSCMathsObtMark.Text = dtr["HSC_MATH_OBTMARK"].ToString();
                    txtHSCMathsMaxMark.Text = dtr["HSC_MATH_MAXMARK"].ToString();
                    txtHSCMathsPer.Text = dtr["HSC_MATH_PER"].ToString();
                    txtHSCPhysicsObtMark.Text = dtr["HSC_PHY_OBTMARK"].ToString();
                    txtHSCPhysicsMaxMark.Text = dtr["HSC_PHY_MAXMARK"].ToString();
                    txtHSCPhysicsPer.Text = dtr["HSC_PHY_PER"].ToString();
                    txtHSCChemObtMark.Text = dtr["HSC_CHE_OBTMARK"].ToString();
                    txtHSCChemMaxMark.Text = dtr["HSC_CHE_MAXMARK"].ToString();
                    txtHSCChemPer.Text = dtr["HSC_CHE_PER"].ToString();
                    ddlHSCOptionalSub.SelectedValue = dtr["HSC_OPTIONAL_SUB"].ToString();
                    txtHSCTotalMarkScore.Text = dtr["HSC_TOTAL_SCORE"].ToString();
                    txtHSCYearofPassing.Text = dtr["HSC_YEAR_OFPASING"].ToString();
                    txtHSCMediumOfInstruction.Text = dtr["HSC_MEDUIM_INSTRUCTION"].ToString();
                    txtHSCMarkCertificateNo.Text = dtr["HSC_MARK_CERTNO"].ToString();
                    txtHSCPassCertificateNo.Text = dtr["HSC_PASS_CERTNO"].ToString();
                    txtHSCTMRNo.Text = dtr["HSC_TMRNO"].ToString();
                    txtHSCRegisterNo.Text = dtr["HSC_REGISTERNO"].ToString();
                    txtHSCInstituteName.Text = dtr["HSC_INSTITUTENAME"].ToString();
                    txtHSCTotalPer.Text = dtr["HSC_PER"].ToString();
                    txtHSCInstituteAddress.Text = dtr["HSC_INSTITUTEADDR"].ToString();
                    txtVocationalTHObtMark.Text = dtr["HSC_VOCATIONAL_TH_OBTMARK"].ToString();
                    txtVocationalTHMaxMark.Text = dtr["HSC_VOCATIONAL_TH_MAXMARK"].ToString();
                    txtVocationalTHPer.Text = dtr["HSC_VOCATIONAL_TH_PER"].ToString();
                    txtVocationalPR1ObtMark.Text = dtr["HSC_VOCATIONAL_PR1_OBTMARK"].ToString();
                    txtVocationalPR1MaxMark.Text = dtr["VOCATIONAL_PR1_MAXMARK"].ToString();
                    txtVocationalPR1Per.Text = dtr["VOCATIONAL_PR1_PER"].ToString();
                    txtVocationalPR2ObtMark.Text = dtr["VOCATIONAL_PR2_OBTMARK"].ToString();
                    txtVocationalPR2MaxMark.Text = dtr["VOCATIONAL_PR2_MAXMARK"].ToString();
                    txtVocationalPR2Per.Text = dtr["VOCATIONAL_PR2_PER"].ToString();
                    txtHSCLangObtMark.Text = dtr["HSC_LANG_OBTMARK"].ToString();
                    txtHSCLangMaxMark.Text = dtr["HSC_LANGMAXMARK"].ToString();
                    txtHSCLangPer.Text = dtr["HSC_LANGPER"].ToString();
                    txtHSCTransferCertNo.Text = dtr["HSCTRANSFER_CERT_NO"].ToString();

                    txtSSCLangGdPoint.Text = Convert.ToString(dtr["SSC_LANG_GDPOINT"].ToString());

                    //   txtSSCLangGdPoint.Text = "kk";

                    txtSSCLang2GdPoint.Text = dtr["SSCLANGUAGE_GDPOINT2"].ToString();

                    txtSSCLangGrade.Text = dtr["SSC_LANG_GRADE"].ToString();
                    txtSSCLang2Grade.Text = dtr["SSCLANGUAGE_GRADE2"].ToString();

                    txtSSCEngGdpoint.Text = dtr["SSC_ENGLISH_GDPOINT"].ToString();
                    txtSSCEngGrade.Text = dtr["SSC_ENGLISH_GRADE"].ToString();
                    txtSSCMathGdPoint.Text = dtr["SSC_MATHS_GDPOINT"].ToString();
                    txtSSCMathGrade.Text = dtr["SSC_MATHS_GRADE"].ToString();
                    txtSSCSciGdPoint.Text = dtr["SSC_SCIENCE_GDPOINT"].ToString();
                    txtSSCSciGrade.Text = dtr["SSC_SCIENCE_GRADE"].ToString();
                    txtSSCSocSciGdPoint.Text = dtr["SSC_SOC_SCIENCE_GDPOINT"].ToString();
                    txtSSCSocSciGrade.Text = dtr["SSC_SOC_SCIENCE_GRADE"].ToString();
                    txtiseComputerAppGdPoint.Text = dtr["SSC_COMP_APPLICATION_GDPOINT"].ToString();
                    txtisecomputerAppGrade.Text = dtr["SSC_COMP_APPLICATION_GRADE"].ToString();
                    txtIseHistGdPoint.Text = dtr["SSC_HISTORY_GDPOINT"].ToString();
                    txtIseHistGrade.Text = dtr["SSC_HISTORY_GRADE"].ToString();
                    txtHSCLangGdPoint.Text = dtr["HSC_LANGUAGE_GDPOINT"].ToString();
                    txtHSCLangGrade.Text = dtr["HSC_LANGUAGE_GRADE"].ToString();
                    txtHSCEngGdpoint.Text = dtr["HSC_ENGLISH_GDPOINT"].ToString();
                    txtHSCEngGrade.Text = dtr["HSC_ENGLISH_GRADE"].ToString();
                    txtHSCMathGdpoint.Text = dtr["HSC_MATHS_GDPOINT"].ToString();
                    txtHSCMathGrade.Text = dtr["HSC_MATHS_GRADE"].ToString();
                    txtHSCPhyGdPoint.Text = dtr["HSC_PHYSICS_GDPOINT"].ToString();
                    txtHSCPhyGrade.Text = dtr["HSC_PHYSICS_GRADE"].ToString();
                    txtHSCChemGdPoint.Text = dtr["HSC_CHEMISTRY_GDPOINT"].ToString();
                    txtHSCChemGrade.Text = dtr["HSC_CHEMISTRY_GRADE"].ToString();
                    txtHSCOptionalMarkObt.Text = dtr["HSC_BIO"].ToString();
                    txtHSCOptionalMaxMark.Text = dtr["HSC_BIO_MAX"].ToString();
                    txtHSCOptionalPer.Text = dtr["BIO_PER"].ToString();
                    txtHSCOptionalGdPoint.Text = dtr["BIO_GDPOINT"].ToString();
                    txtHSCOptionalGrade.Text = dtr["BIO_GRADE"].ToString();
                    txtVocationalTHGdPoint.Text = dtr["HSC_THEORY_GDPOINT"].ToString();
                    txtVocationalTHGrade.Text = dtr["HSC_THEORY_GRADE"].ToString();
                    txtVocationalPR1GradePoint.Text = dtr["HSC_PR1_GDPOINT"].ToString();
                    txtVocationalPR1Grade.Text = dtr["HSC_PR1_GRADE"].ToString();
                    txtVocationalPR2Gdpoint.Text = dtr["HSC_PR2_GDPOINT"].ToString();
                    txtVocationalPR2Grade.Text = dtr["HSC_PR2_GRADE"].ToString();
                    //Added by Naresh Beerla [29092020]
                    txtOtherBoardHSC.Text = dtr["OTHER_BOARD_CATEGORY_HSC"].ToString();
                    rblInputsystemHSC.SelectedValue = dtr["EVAL_TYPE_HSC"].ToString();
                    //Added by Naresh Beerla [29092020]

                    //Added by Pritish on 25/02/2021
                    txtBotMarkObt.Text = dtr["BOT_MARK_OBT"].ToString();
                    txtBotMaxMark.Text = dtr["BOT_MAX_MARK"].ToString();
                    txtBotPerMark.Text = dtr["BOT_PER"].ToString();
                    txtBotGdPt.Text = dtr["BOT_GDPOINT"].ToString();
                    txtBotGrade.Text = dtr["BOT_GRADE"].ToString();

                    txtZooMarkObt.Text = dtr["ZOO_MARK_OBT"].ToString();
                    txtZooMaxMark.Text = dtr["ZOO_MAX_MARK"].ToString();
                    txtZooPerMark.Text = dtr["ZOO_PER"].ToString();
                    txtZooGdPt.Text = dtr["ZOO_GDPOINT"].ToString();
                    txtZooGrade.Text = dtr["ZOO_GRADE"].ToString();

                    txtCSMarkObt.Text = dtr["CS_MARK_OBT"].ToString();
                    txtCSMaxMark.Text = dtr["CS_MAX_MARK"].ToString();
                    txtCSPerMark.Text = dtr["CS_PER"].ToString();
                    txtCSGdPt.Text = dtr["CS_GDPOINT"].ToString();
                    txtCSGrade.Text = dtr["CS_GRADE"].ToString();

                    //LAST EXAM DETAILS // DIPLOMA
                    txtNameofDiploma.Text = dtr["DIPLOMA_NAME"].ToString();
                    txtDiplomaCollege.Text = dtr["DIPLOMA_COLLEGE"].ToString();
                    txtDiplomaBoard.Text = dtr["DIPLOMA_BOARD"].ToString();
                    txtDipRegisterNo.Text = dtr["DIP_REGISTER_NUMBER"].ToString();
                    txtDiplomaYear.Text = dtr["DIP_YEAR_EXAM"].ToString();
                    txtSemIObtained.Text = dtr["SEM_I_OBTMARK"].ToString();
                    txtSemIMax.Text = dtr["SEM_I_MAXMARK"].ToString();
                    txtSemIPer.Text = dtr["SEM_I_PER"].ToString();
                    txtSemIIObtained.Text = dtr["SEM_II_OBTMARK"].ToString();
                    txtSemIIMax.Text = dtr["SEM_II_MAXMARK"].ToString();
                    txtSemIIPer.Text = dtr["SEM_II_PER"].ToString();
                    txtSemIIIObtained.Text = dtr["SEM_III_OBTMARK"].ToString();
                    txtSemIIIMax.Text = dtr["SEM_III_MAXMARK"].ToString();
                    txtSemIIIPer.Text = dtr["SEM_III_PER"].ToString();
                    txtSemIVObtained.Text = dtr["SEM_IV_OBTMARK"].ToString();
                    txtSemIVMax.Text = dtr["SEM_IV_MAXMARK"].ToString();
                    txtSemIVPer.Text = dtr["SEM_IV_PER"].ToString();
                    txtSemVObtained.Text = dtr["SEM_V_OBTMARK"].ToString();
                    txtSemVMax.Text = dtr["SEM_V_MAXMARK"].ToString();
                    txtSemVPer.Text = dtr["SEM_V_PER"].ToString();
                    txtSemVIObtained.Text = dtr["SEM_VI_OBTMARK"].ToString();
                    txtSemVIMax.Text = dtr["SEM_VI_MAXMARK"].ToString();
                    txtSemVIPer.Text = dtr["SEM_VI_PER"].ToString();
                    txtSemVIIObtained.Text = dtr["SEM_VII_OBTMARK"].ToString();
                    txtSemVIIMax.Text = dtr["SEM_VII_MAXMARK"].ToString();
                    txtSemVIIPer.Text = dtr["SEM_VII_PER"].ToString();

                    txtSemVIIIObtained.Text = dtr["SEM_VIII_OBTMARK"].ToString();
                    txtSemVIIIMax.Text = dtr["SEM_VIII_MAXMARK"].ToString();
                    txtSemVIIIPer.Text = dtr["SEM_VIII_PER"].ToString();

                    ddlDipDegree.SelectedValue = dtr["DIP_DEGREE"].ToString();
                    txtSpecialization.Text = dtr["SPECIALIZATION"].ToString();
                    txtTotalMarksScore.Text = dtr["TOTAL_OBT_MARK_DIP"].ToString();
                    txtTotalMaxMark.Text = dtr["TOTAL_MAX_MARK_DIP"].ToString();
                    txtTotalPer.Text = dtr["TOTAL_PER_DIP"].ToString();
                    txtSSCTotalMaxMark.Text = dtr["SSC_TOTAL_MAX_MARK"].ToString();
                    txtHSCTotalMaxMark.Text = dtr["HSC_TOTAL_MAX_MARK"].ToString();
                    txtSSCtotalGradePoint.Text = dtr["SSC_TOTAL_GRADEPOINT"].ToString();
                    txtSSCCGPA.Text = dtr["SSC_CGPA"].ToString();
                    txtHSCtotalGradePoint.Text = dtr["HSC_TOTAL_GRADEPOINT"].ToString();
                    txtHSCCGPA.Text = dtr["HSC_CGPA"].ToString();
                    txtDIPTransferCertNo.Text = dtr["DIPTRANSFER_CERT_NO"].ToString();

                    int ua_no = Convert.ToInt32(objCommon.LookUp("USER_ACC", "ISNULL(UA_NO,0)", "UA_IDNO=" + Convert.ToInt32(Session["studentID"])));
                    hdnTNEANo.Value = objCommon.LookUp("NEW_STUDENT_REGISTRAION", "COURSE", "NEW_IDNO =" + ua_no); //FOR ENTRANCE EXAM IF TNEA THEN TNEA FIELDS MANDATORY VALIDATION//1=MGMT//2=TNEA//3=OTHER
                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUCommon.ShowError(Page, "PresentationLayer_StudentRegistration.ShowLastExamDetails-> " + ex.Message + " " + ex.StackTrace);
                    else
                        objUCommon.ShowError(Page, "Server UnAvailable");
                }

            }
        }
    }

    ////showing the report in pdf formate as per as  selection of report name  or file name.
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Session["studidno"].ToString();
            //url += "&param=@P_IDNO=" + Session["studidno"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel5, this.UpdatePanel5.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //USED FOR UPLOADING THE DOCUMENT
    //void uploadDocument()
    //{
    //    try
    //    {
    //        string idno = Session["studentID"].ToString();
    //        string studentname = Session["userfullname"].ToString();
    //        string folderPath = WebConfigurationManager.AppSettings["MNR_STUDENT_DOC"].ToString() + idno + "_" + studentname + "\\";

    //        foreach (ListViewDataItem lvitem in lvDocumentList.Items)
    //        {

    //            FileUpload fuStudPhoto = lvitem.FindControl("studentFileUpload") as FileUpload;
    //            HiddenField hidstudocno = lvitem.FindControl("docid") as HiddenField;

    //            if (fuStudPhoto.HasFile)
    //            {
    //                string contentType = contentType = fuStudPhoto.PostedFile.ContentType;
    //                if (!Directory.Exists(folderPath))
    //                {
    //                    Directory.CreateDirectory(folderPath);
    //                }

    //                string ext = System.IO.Path.GetExtension(fuStudPhoto.PostedFile.FileName);
    //                HttpPostedFile file = fuStudPhoto.PostedFile;
    //                string filename = Path.GetFileName(fuStudPhoto.PostedFile.FileName);


    //                if (file.ContentLength <= 40960)// 31457280 before size  //For Allowing 40 Kb Size Files only 
    //                {
    //                    CustomStatus cs = (CustomStatus)objStudRegC.AddUpdateStudentDocumentsDetail(Convert.ToInt32(Session["studentID"]), Convert.ToInt32(hidstudocno.Value), ext, contentType, filename, folderPath + filename);
    //                    fuStudPhoto.PostedFile.SaveAs(folderPath + filename);
    //                    if (Convert.ToInt32(cs) == 1 || Convert.ToInt32(cs) == 2)
    //                    {

    //                    }
    //                    else
    //                    {
    //                        objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
    //                    }
    //                }
    //                else
    //                {
    //                    lblmessageShow.ForeColor = System.Drawing.Color.Red;
    //                    lblmessageShow.Text = "Please Upload file Below or Equal to 40 Kb only !";
    //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    //                    return;
    //                }
    //            }
    //        }
    //        BindDocument();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "PresentationLayer_StudentRegistration.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }


    //}

    //USED FOR DOWNLOAD FILE

    void uploadDocument()
    {
        try
        {
            string IdNo = Session["stuinfoidno"].ToString();
            string idno = Session["studentID"].ToString();
            string studentname = Session["userfullname"].ToString();
            //string folderPath = WebConfigurationManager.AppSettings["SVCE_STUDENT_DOC"].ToString() + idno + "_" + studentname + "\\";
            foreach (ListViewDataItem lvitem in lvDocumentList.Items)
            {

                FileUpload fuStudPhoto = lvitem.FindControl("studentFileUpload") as FileUpload;
                HiddenField hidstudocno = lvitem.FindControl("docid") as HiddenField;

                if (fuStudPhoto.HasFile)
                {
                    string contentType = contentType = fuStudPhoto.PostedFile.ContentType;
                    //if (!Directory.Exists(folderPath))
                    //{
                    //    Directory.CreateDirectory(folderPath);
                    //}

                    string ext = System.IO.Path.GetExtension(fuStudPhoto.PostedFile.FileName);
                    HttpPostedFile file = fuStudPhoto.PostedFile;
                    string filename = IdNo + "_doc_" + hidstudocno.Value + ext;   //Path.GetFileName(fuStudPhoto.PostedFile.FileName);
                    string orignalfilename = Path.GetFileName(fuStudPhoto.PostedFile.FileName);


                    if (file.ContentLength <= 40960)// 31457280 before size  //For Allowing 40 Kb Size Files only 
                    {
                        int retval = AL.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_doc_" + hidstudocno.Value + "", fuStudPhoto);
                        if (retval == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                            return;
                        }
                        CustomStatus cs = (CustomStatus)objStudRegC.AddUpdateStudentDocumentsDetail(Convert.ToInt32(Session["studentID"]), Convert.ToInt32(hidstudocno.Value), ext, contentType, filename, orignalfilename);
                        //fuStudPhoto.PostedFile.SaveAs(folderPath + filename);
                        if (Convert.ToInt32(cs) == 1 || Convert.ToInt32(cs) == 2)
                        {

                        }
                        else
                        {
                            objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
                        }
                    }
                    else
                    {
                        lblmessageShow.ForeColor = System.Drawing.Color.Red;
                        lblmessageShow.Text = "Please Upload file Below or Equal to 40 Kb only !";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                        return;

                        //goto outer;
                    }


                }


            }
            BindDocument();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PresentationLayer_StudentRegistration.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void DownloadFile(object sender, EventArgs e)
    {
        try
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = "image/jpeg";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PresentationLayer_StudentRegistration.documentDownload-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region Control Events

    //Student/ Admission Details -- Step-1
    protected void btnSaveNext_Click(object sender, EventArgs e)
    {
        try
        {
            //Student admission Details
            objStudReg.IDNO = Convert.ToInt32(Session["studentID"]);
            if (!txtStudName.Text.Trim().Equals(string.Empty)) objStudReg.StudentName = txtStudName.Text.Trim();

            // Added by Pritish on 04022021
            if (!txtFirstname.Text.Trim().Equals(string.Empty)) objStudReg.FirstName = txtFirstname.Text.Trim();
            if (!txtMiddlename.Text.Trim().Equals(string.Empty)) objStudReg.MiddleName = txtMiddlename.Text.Trim();

            if (!txtSurname.Text.Trim().Equals(string.Empty)) objStudReg.Surname = txtSurname.Text.Trim();
            if (!txtDob.Text.Trim().Equals(string.Empty)) objStudReg.DateOfBirth = Convert.ToDateTime(txtDob.Text.Trim());

            if (rdoGender.SelectedValue == "1")
            {
                objStudReg.Gender = "M";
            }
            else if (rdoGender.SelectedValue == "2")
            {
                objStudReg.Gender = "F";
            }
            else
            {
                objStudReg.Gender = "T";
            }

            if (!txtAdhaarNo.Text.Trim().Equals(string.Empty)) objStudReg.AdhaarcardNo = txtAdhaarNo.Text.Trim();
            objStudReg.BloogGroupNo = Convert.ToInt32(ddlBloodGroup.SelectedValue);

            objStudReg.CollegeId = Convert.ToInt32(ddlCollege.SelectedValue);

            if (rdoIsBloodDonate.SelectedValue == "1")
            {
                objStudReg.IsBloodDonate = "Y";
            }
            else
            {
                objStudReg.IsBloodDonate = "N";
            }

            if (rdoPH.SelectedValue == "1")
            {
                objStudReg.IsPhysicalHandicap = "Y";
            }
            else
            {
                objStudReg.IsPhysicalHandicap = "N";
            }

            objStudReg.TypeOfHandicap = Convert.ToInt32(ddlTypeOfPH.SelectedValue);

            if (!txtAlergyDetail.Text.Trim().Equals(string.Empty)) objStudReg.AlergyDetails = txtAlergyDetail.Text.Trim();
            if (!txtIdentityMark.Text.Trim().Equals(string.Empty)) objStudReg.IdentityMark = txtIdentityMark.Text.Trim();
            if (!txtIdentityMark2.Text.Trim().Equals(string.Empty)) objStudReg.IdentityMark2 = txtIdentityMark2.Text.Trim();

            objStudReg.MotherTongue = Convert.ToInt32(ddlMtounge.SelectedValue);
            objStudReg.Citizenship = Convert.ToInt32(ddlCitizenship.SelectedValue);
            if (!txtCommunityCode.Text.Trim().Equals(string.Empty)) objStudReg.CommunityCode = txtCommunityCode.Text.Trim();
            if (!txtSubcaste.Text.Trim().Equals(string.Empty)) objStudReg.Subcaste = txtSubcaste.Text.Trim();
            objStudReg.CommunityNo = Convert.ToInt32(ddlCommunity.SelectedValue);
            objStudReg.ReligionNo = Convert.ToInt32(ddlReligion.SelectedValue);
            objStudReg.CasteNo = Convert.ToInt32(ddlCasteName.SelectedValue);
            if (RdoHosteller.SelectedValue == "1")
            {
                objStudReg.IsHosteller = true;
            }
            else
            {
                objStudReg.IsHosteller = false;
            }
            if (!txtCollegeDistance.Text.Trim().Equals(string.Empty)) objStudReg.Distance = Convert.ToInt32(txtCollegeDistance.Text.Trim());
            if (!txtKnownLanguage.Text.Trim().Equals(string.Empty)) objStudReg.LanguageKnown = txtKnownLanguage.Text.Trim();
            if (!txtKnownForeignLang.Text.Trim().Equals(string.Empty)) objStudReg.ForeignLanguageKnown = txtKnownForeignLang.Text.Trim();
            if (rdotransPort.SelectedValue == "1")
            {
                objStudReg.NeedTransport = 1;
            }
            else
            {
                objStudReg.NeedTransport = 0;
            }
            objStudReg.AcademicYearNo = Convert.ToInt32(ddlAcademicYear.SelectedValue);
            objStudReg.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            objStudReg.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objStudReg.AdmissionType = Convert.ToInt32(ddlAdmissionType.SelectedValue);
            objStudReg.UA_NO = Convert.ToInt32(Session["userno"]);
            objStudReg.Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);  // idtype
            if (!txtNativePlace.Text.Trim().Equals(string.Empty)) objStudReg.NativePlace = txtNativePlace.Text.Trim();
            //Added by Abhinay Lad [16-09-2019]
            if (rbtn_MaritalStatus.SelectedValue == "1")
            {
                objStudReg.Marital_Status = "Y";
            }
            else if (rbtn_MaritalStatus.SelectedValue == "3")
            {
                objStudReg.Marital_Status = "D";
            }
            else
            {
                objStudReg.Marital_Status = "N";
            }

            CustomStatus cs = (CustomStatus)objStudRegC.AddUpdateOnlineStudentReg(objStudReg);
            if (Convert.ToInt32(cs) == 1 || Convert.ToInt32(cs) == 2)
            {
                if (Session["usertype"] == "2")
                {
                    Response.Redirect("StudentInfo.aspx#step-2", false);
                }
                else
                {
                    Response.Redirect("StudentInfo.aspx#step-2", false);
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
            }

        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this, "Something went wrong ..Please try again !!", this);
            return;
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "PresentationLayer_StudentRegistration.btnSaveNext_Click-> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    //Address Details -- Step-2
    protected void btnSaveNext2_Click(object sender, EventArgs e)
    {
        try
        {
            //Student Address Details
            objStudReg.IDNO = Convert.ToInt32(Session["studentID"]);
            if (!txtLAddressLine1.Text.Trim().Equals(string.Empty)) objStudReg.LAddressLine1 = txtLAddressLine1.Text.Trim();
            //if drop down is visible then drop down value else text box value
            if (ddlLcity.Visible == true)
            {
                objStudReg.Lcity = Convert.ToInt32(ddlLcity.SelectedValue);
            }
            else if (!txtLOtherCity.Text.Trim().Equals(string.Empty) && txtLOtherCity.Visible == true)
            {
                objStudReg.LOthercity = txtLOtherCity.Text.Trim();
            }
            else
            {
                txtLOtherCity.Focus();
                objCommon.DisplayMessage(this, "Please enter City !", this);
                return;
            }

            objStudReg.LState = Convert.ToInt32(ddlLState.SelectedValue);
            if (!txtLPin.Text.Trim().Equals(string.Empty)) objStudReg.LPinCode = txtLPin.Text.Trim();
            if (!txtLSTDCode.Text.Trim().Equals(string.Empty)) objStudReg.LSTDCode = txtLSTDCode.Text.Trim();
            if (!txtLLandLineNo.Text.Trim().Equals(string.Empty)) objStudReg.LLandLineNo = txtLLandLineNo.Text.Trim();
            if (!txtLEmail.Text.Trim().Equals(string.Empty)) objStudReg.LEmailId = txtLEmail.Text.Trim();
            if (!txtLStudMobile.Text.Trim().Equals(string.Empty)) objStudReg.LStudentMobile = txtLStudMobile.Text.Trim();

            if (!txtEmergencyContactNum.Text.Trim().Equals(string.Empty)) objStudReg.EmergencyNum = txtEmergencyContactNum.Text.Trim();
            if (!txtEmergencyContactName.Text.Trim().Equals(string.Empty)) objStudReg.EmergencyName = txtEmergencyContactName.Text.Trim();
            if (!txtEmergencyContactRelation.Text.Trim().Equals(string.Empty)) objStudReg.EmergencyRelation = txtEmergencyContactRelation.Text.Trim();
            if (!txtEmergencyContactEmail.Text.Trim().Equals(string.Empty)) objStudReg.EmergencyEmail = txtEmergencyContactEmail.Text.Trim();

            if (ddlSMSSend.SelectedValue.ToString().Equals("0"))
            { objCommon.DisplayMessage(this, "Please select Parent to which SMS is to be Send !", this); ddlSMSSend.Focus(); return; }

            if (ddlSMSSend.SelectedValue == "1")
            {
                objStudReg.LSMSSend = "F";
            }
            else
            {
                objStudReg.LSMSSend = "M";
            }
            if (!txtPAddressLine1.Text.Trim().Equals(string.Empty)) objStudReg.PAddressLine1 = txtPAddressLine1.Text.Trim();

            //if drop down is visible then drop down value else text box value
            if (ddlPCity.Visible == true)
            {
                objStudReg.PCity = Convert.ToInt32(ddlPCity.SelectedValue);
            }
            else if (!txtPOthercity.Text.Trim().Equals(string.Empty) && txtPOthercity.Visible == true)
            {
                objStudReg.POtherCity = txtPOthercity.Text.Trim();
            }

            objStudReg.PState = Convert.ToInt32(ddlPState.SelectedValue);
            if (!txtPPin.Text.Trim().Equals(string.Empty)) objStudReg.PPinCode = txtPPin.Text.Trim();
            if (!txtPSTDCode.Text.Trim().Equals(string.Empty)) objStudReg.PSTDCode = txtPSTDCode.Text.Trim();
            if (!txtPLandLine.Text.Trim().Equals(string.Empty)) objStudReg.PLandLineNumber = txtPLandLine.Text.Trim();
            if (!txtGaddressLine1.Text.Trim().Equals(string.Empty)) objStudReg.GAddressLine1 = txtGaddressLine1.Text.Trim();
            //if drop down is visible then drop down value else text box value
            if (ddlGCity.Visible == true)
            {
                objStudReg.GCity = Convert.ToInt32(ddlGCity.SelectedValue);
            }
            else if (!txtGOthercity.Text.Trim().Equals(string.Empty) && txtGOthercity.Visible == true)
            {
                objStudReg.GOtherCity = txtGOthercity.Text.Trim();
            }

            objStudReg.GState = Convert.ToInt32(ddlGState.SelectedValue);
            if (!txtGPin.Text.Trim().Equals(string.Empty)) objStudReg.GPinCode = txtGPin.Text.Trim();
            if (!txtGSTDCode.Text.Trim().Equals(string.Empty)) objStudReg.GSTDCode = txtGSTDCode.Text.Trim();
            if (!txtGLandLineNumber.Text.Trim().Equals(string.Empty)) objStudReg.GLandLineNumber = txtGLandLineNumber.Text.Trim();
            if (!txtGEmailId.Text.Trim().Equals(string.Empty)) objStudReg.GEmailId = txtGEmailId.Text.Trim();
            if (!txtGMobile.Text.Trim().Equals(string.Empty)) objStudReg.GGaurdianMobile = txtGMobile.Text.Trim();

            if (!txtGRelation.Text.Trim().Equals(string.Empty)) objStudReg.GRelation = txtGRelation.Text.Trim();

            CustomStatus cs = (CustomStatus)objStudRegC.AddUpdateStudentRegAddressDetail(objStudReg);
            if (Convert.ToInt32(cs) == 1 || Convert.ToInt32(cs) == 2)
            {
                int i = Convert.ToInt32(Session["usertype"].ToString());

                if (Session["usertype"] == "2")
                {
                    Response.Redirect("StudentInfo.aspx#step-3", false);
                }
                else if (Convert.ToInt32(Session["usertype"].ToString()) == 3)
                {
                    Response.Redirect("StudentInfo.aspx#step-6", false);
                }
                else
                {
                    Response.Redirect("StudentInfo.aspx#step-3", false);
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
            }

        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this, "Something went wrong ..Please try again !!", this);
            return;
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "PresentationLayer_StudentRegistration.btnSaveNext2_Click-> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //Last Exam details -- Step-3
    protected void btnSaveNext3_Click(object sender, EventArgs e)
    {

        try
        {
            //Added by Naresh Beerla [29092020]
            if (ddlSSCBoardCategory.SelectedValue.ToString().Equals("1") && txtOtherBoardSSC.Text == "")
            {
                objCommon.DisplayMessage(this, "Please Enter other Board for SSLC !", this);
                txtOtherBoardSSC.Focus();
                return;
            }

            if (ddlHSCBoardCategory.SelectedValue.ToString().Equals("1") && txtOtherBoardHSC.Text == "")
            {
                objCommon.DisplayMessage(this, "Please Enter other Board for HSC !", this);
                txtOtherBoardHSC.Focus();
                return;
            }

            //Ends by Naresh Beerla [29092020]
            if (ddlHSCBoardCategory.SelectedValue.ToString().Equals("7") || ddlHSCBoardCategory.SelectedValue.ToString().Equals("9"))
            {
                if (txtVocationalTHObtMark.Text.ToString() == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter Theory Obtained Marks !", this);
                    txtVocationalTHObtMark.Focus();
                    return;
                }
                else if (txtVocationalTHMaxMark.Text.ToString() == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter Theory Max Marks !", this);
                    txtVocationalTHMaxMark.Focus();
                    return;
                }
                else if (txtVocationalPR1ObtMark.Text.ToString() == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter Practical 1 Obtained Marks !", this);
                    txtVocationalPR1ObtMark.Focus();
                    return;
                }
                else if (txtVocationalPR1MaxMark.Text.ToString() == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter Practical 1 Max Marks !", this);
                    txtVocationalPR1MaxMark.Focus();
                    return;
                }
                else if (txtVocationalPR2ObtMark.Text.ToString() == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter Practical 2 Obtained Marks !", this);
                    txtVocationalPR2ObtMark.Focus();
                    return;
                }
                else if (txtVocationalPR2MaxMark.Text.ToString() == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter Practical 2 Max Marks !", this);
                    txtVocationalPR2MaxMark.Focus();
                    return;
                }
            }
            //Last Exam Details
            objStudReg.IDNO = Convert.ToInt32(Session["studentID"]);
            objStudReg.SSCBoardCategory = Convert.ToInt32(ddlSSCBoardCategory.SelectedValue);
            objStudReg.SSCLanguage = Convert.ToInt32(ddlSSCLanguage.SelectedValue);
            objStudReg.SSCLanguage2 = Convert.ToInt32(ddlSSCLanguage2.SelectedValue);

            if (!txtSSCTransferCertNo.Text.Trim().Equals(string.Empty)) objStudReg.SSCTransferCertNo = txtSSCTransferCertNo.Text.Trim();

            if (!txtSSCLangObtMark.Text.Trim().Equals(string.Empty)) objStudReg.SSCLanguageObtMark = Convert.ToDecimal(txtSSCLangObtMark.Text.Trim());
            if (!txtSSCLang2ObtMark.Text.Trim().Equals(string.Empty)) objStudReg.SSCLanguage2ObtMark = Convert.ToDecimal(txtSSCLang2ObtMark.Text.Trim());

            if (!txtSSCLangMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.SSCLanguageMaxMark = Convert.ToDecimal(txtSSCLangMaxMark.Text.Trim());
            if (!txtSSCLang2MaxMark.Text.Trim().Equals(string.Empty)) objStudReg.SSCLanguage2MaxMark = Convert.ToDecimal(txtSSCLang2MaxMark.Text.Trim());

            if (!txtSSCLangPer.Text.Trim().Equals(string.Empty)) objStudReg.SSCLanguagePer = Convert.ToDecimal(txtSSCLangPer.Text.Trim());
            if (!txtSSCLang2Per.Text.Trim().Equals(string.Empty)) objStudReg.SSCLanguage2Per = Convert.ToDecimal(txtSSCLang2Per.Text.Trim());

            if (!txtSSCEnglishObtMark.Text.Trim().Equals(string.Empty)) objStudReg.SSCEnglishObtMark = Convert.ToDecimal(txtSSCEnglishObtMark.Text.Trim());
            if (!txtSSCEnglishMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.SSCEnglishMaxMark = Convert.ToDecimal(txtSSCEnglishMaxMark.Text.Trim());
            if (!txtSSCEnglishPer.Text.Trim().Equals(string.Empty)) objStudReg.SSCEnglishPer = Convert.ToDecimal(txtSSCEnglishPer.Text.Trim());
            if (!txtSSCMathsObtMark.Text.Trim().Equals(string.Empty)) objStudReg.SSCMathsObtMark = Convert.ToDecimal(txtSSCMathsObtMark.Text.Trim());
            if (!txtSSCMathsMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.SSCMathsMaxMark = Convert.ToDecimal(txtSSCMathsMaxMark.Text.Trim());
            if (!txtSSCMathsMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.SSCMathsMaxMark = Convert.ToDecimal(txtSSCMathsMaxMark.Text.Trim());
            if (!txtSSCMathsPer.Text.Trim().Equals(string.Empty)) objStudReg.SSCMathsPer = Convert.ToDecimal(txtSSCMathsPer.Text.Trim());
            if (!txtSSCScienceObtMark.Text.Trim().Equals(string.Empty)) objStudReg.SSCScienceObtMark = Convert.ToDecimal(txtSSCScienceObtMark.Text.Trim());
            if (!txtSSCScienceMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.SSCScienceMaxMark = Convert.ToDecimal(txtSSCScienceMaxMark.Text.Trim());
            if (!txtSSCSciencePer.Text.Trim().Equals(string.Empty)) objStudReg.SSCSciencePer = Convert.ToDecimal(txtSSCSciencePer.Text.Trim());
            if (!txtSSCSocialScienceObtMark.Text.Trim().Equals(string.Empty)) objStudReg.SSCSocialScienceObtMark = Convert.ToDecimal(txtSSCSocialScienceObtMark.Text.Trim());
            if (!txtSSCSocialScienceMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.SSCSocialScienceMaxMark = Convert.ToDecimal(txtSSCSocialScienceMaxMark.Text.Trim());
            if (!txtSSCSocialSciencePer.Text.Trim().Equals(string.Empty)) objStudReg.SSCSocialSciencePer = Convert.ToDecimal(txtSSCSocialSciencePer.Text.Trim());
            if (!txtSSCTotalMarkScore.Text.Trim().Equals(string.Empty)) objStudReg.SSCTotalMarkScore = Convert.ToDecimal(txtSSCTotalMarkScore.Text.Trim());
            if (!txtSSCTotalMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.SSCTotalofMaxMark = Convert.ToDecimal(txtSSCTotalMaxMark.Text.Trim());

            if (!txtSSCYearofPassing.Text.Trim().Equals(string.Empty)) objStudReg.SSCYearofPassing = txtSSCYearofPassing.Text.Trim();
            if (!txtSSCMediumOfInstruction.Text.Trim().Equals(string.Empty)) objStudReg.SSCMediumOfInstruction = txtSSCMediumOfInstruction.Text.Trim();
            if (!txtSSCMarkCertNo.Text.Trim().Equals(string.Empty)) objStudReg.SSCMarkCertNo = txtSSCMarkCertNo.Text.Trim();
            if (!txtSSCPassCertNo.Text.Trim().Equals(string.Empty)) objStudReg.SSCPassCertNo = txtSSCPassCertNo.Text.Trim();
            if (!txtSSCTMRNo.Text.Trim().Equals(string.Empty)) objStudReg.SSCTMRNo = txtSSCTMRNo.Text.Trim();
            if (!txtSSCRegisterNo.Text.Trim().Equals(string.Empty)) objStudReg.SSCRegisterNo = txtSSCRegisterNo.Text.Trim();
            if (!txtSSCInstituteName.Text.Trim().Equals(string.Empty)) objStudReg.SSCInstituteName = txtSSCInstituteName.Text.Trim();
            if (!txtSSCTotalPer.Text.Trim().Equals(string.Empty)) objStudReg.SSCTotalPer = Convert.ToDecimal(txtSSCTotalPer.Text.Trim());
            if (!txtSSCInstituteAddress.Text.Trim().Equals(string.Empty)) objStudReg.SSCInstituteAddress = txtSSCInstituteAddress.Text.Trim();
            if (!txtISEComputerApplicationObtMark.Text.Trim().Equals(string.Empty)) objStudReg.ISEComputerApplicationObtMark = Convert.ToDecimal(txtISEComputerApplicationObtMark.Text.Trim());
            if (!txtISEComputerApplicationMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.ISEComputerApplicationMaxMark = Convert.ToDecimal(txtISEComputerApplicationMaxMark.Text.Trim());
            if (!txtISEComputerApplicationPer.Text.Trim().Equals(string.Empty)) objStudReg.ISEComputerApplicationPer = Convert.ToDecimal(txtISEComputerApplicationPer.Text.Trim());
            if (!txtISEHistoryObtMark.Text.Trim().Equals(string.Empty)) objStudReg.ISEHistoryObtMark = Convert.ToDecimal(txtISEHistoryObtMark.Text.Trim());
            if (!txtISEHistoryMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.ISEHistoryMaxMark = Convert.ToDecimal(txtISEHistoryMaxMark.Text.Trim());
            if (!txtISEHistoryPer.Text.Trim().Equals(string.Empty)) objStudReg.ISEHistoryPer = Convert.ToDecimal(txtISEHistoryPer.Text.Trim());
            objStudReg.HSCBoardCategory = Convert.ToInt32(ddlHSCBoardCategory.SelectedValue);
            objStudReg.HSCLanguage = Convert.ToInt32(ddlHSCLanguage.SelectedValue);
            if (!txtHSCLangObtMark.Text.Trim().Equals(string.Empty)) objStudReg.HSCLanguageObtMark = Convert.ToDecimal(txtHSCLangObtMark.Text.Trim());
            if (!txtHSCLangMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.HSCLanguageMaxMark = Convert.ToDecimal(txtHSCLangMaxMark.Text.Trim());
            if (!txtHSCLangPer.Text.Trim().Equals(string.Empty)) objStudReg.HSCLanguagePer = Convert.ToDecimal(txtHSCLangPer.Text.Trim());
            if (!txtHSCEnglishObtMark.Text.Trim().Equals(string.Empty)) objStudReg.HSCEnglishObtMark = Convert.ToDecimal(txtHSCEnglishObtMark.Text.Trim());
            if (!txtHSCEnglishMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.HSCEnglishMaxMark = Convert.ToDecimal(txtHSCEnglishMaxMark.Text.Trim());
            if (!txtHSCEnglishPer.Text.Trim().Equals(string.Empty)) objStudReg.HSCEnglishPer = Convert.ToDecimal(txtHSCEnglishPer.Text.Trim());
            if (!txtHSCMathsObtMark.Text.Trim().Equals(string.Empty)) objStudReg.HSCMathsObtMark = Convert.ToDecimal(txtHSCMathsObtMark.Text.Trim());
            if (!txtHSCMathsMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.HSCMathsMaxMark = Convert.ToDecimal(txtHSCMathsMaxMark.Text.Trim());
            if (!txtHSCMathsPer.Text.Trim().Equals(string.Empty)) objStudReg.HSCMathsPer = Convert.ToDecimal(txtHSCMathsPer.Text.Trim());
            if (!txtHSCPhysicsObtMark.Text.Trim().Equals(string.Empty)) objStudReg.HSCPhysicsObtMark = Convert.ToDecimal(txtHSCPhysicsObtMark.Text.Trim());
            if (!txtHSCPhysicsMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.HSCPhysicsMaxMark = Convert.ToDecimal(txtHSCPhysicsMaxMark.Text.Trim());
            if (!txtHSCPhysicsPer.Text.Trim().Equals(string.Empty)) objStudReg.HSCPhysicsPer = Convert.ToDecimal(txtHSCPhysicsPer.Text.Trim());
            if (!txtHSCChemObtMark.Text.Trim().Equals(string.Empty)) objStudReg.HSCChemObtMark = Convert.ToDecimal(txtHSCChemObtMark.Text.Trim());
            if (!txtHSCChemMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.HSCChemMaxMark = Convert.ToDecimal(txtHSCChemMaxMark.Text.Trim());
            if (!txtHSCChemPer.Text.Trim().Equals(string.Empty)) objStudReg.HSCChemPer = Convert.ToDecimal(txtHSCChemPer.Text.Trim());
            objStudReg.HSCOptionalSub = Convert.ToInt32(2); // Biology         Convert.ToInt32(ddlHSCOptionalSub.SelectedValue);
            if (!txtHSCTotalMarkScore.Text.Trim().Equals(string.Empty)) objStudReg.HSCTotalMarkScore = Convert.ToDecimal(txtHSCTotalMarkScore.Text.Trim());
            if (!txtHSCTotalMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.HSCTotalofMaxMark = Convert.ToDecimal(txtHSCTotalMaxMark.Text.Trim());

            if (!txtHSCYearofPassing.Text.Trim().Equals(string.Empty)) objStudReg.HSCYearofPassing = txtHSCYearofPassing.Text.Trim();
            if (!txtHSCMediumOfInstruction.Text.Trim().Equals(string.Empty)) objStudReg.HSCMediumOfInstruction = txtHSCMediumOfInstruction.Text.Trim();
            if (!txtHSCMarkCertificateNo.Text.Trim().Equals(string.Empty)) objStudReg.HSCMarkCertificateNo = txtHSCMarkCertificateNo.Text.Trim();
            if (!txtHSCPassCertificateNo.Text.Trim().Equals(string.Empty)) objStudReg.HSCPassCertificateNo = txtHSCPassCertificateNo.Text.Trim();
            if (!txtHSCTMRNo.Text.Trim().Equals(string.Empty)) objStudReg.HSCTMRNo = txtHSCTMRNo.Text.Trim();
            if (!txtHSCRegisterNo.Text.Trim().Equals(string.Empty)) objStudReg.HSCRegisterNo = txtHSCRegisterNo.Text.Trim();
            if (!txtHSCInstituteName.Text.Trim().Equals(string.Empty)) objStudReg.HSCInstituteName = txtHSCInstituteName.Text.Trim();
            if (!txtHSCTotalPer.Text.Trim().Equals(string.Empty)) objStudReg.HSCTotalPer = Convert.ToDecimal(txtHSCTotalPer.Text.Trim());
            if (!txtHSCInstituteAddress.Text.Trim().Equals(string.Empty)) objStudReg.HSCInstituteAddress = txtHSCInstituteAddress.Text.Trim();
            if (!txtVocationalTHObtMark.Text.Trim().Equals(string.Empty)) objStudReg.VocationalTHObtMark = Convert.ToDecimal(txtVocationalTHObtMark.Text.Trim());
            if (!txtVocationalTHMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.VocationalTHMaxMark = Convert.ToDecimal(txtVocationalTHMaxMark.Text.Trim());
            if (!txtVocationalTHPer.Text.Trim().Equals(string.Empty)) objStudReg.VocationalTHPer = Convert.ToDecimal(txtVocationalTHPer.Text.Trim());
            if (!txtVocationalPR1ObtMark.Text.Trim().Equals(string.Empty)) objStudReg.VocationalPR1ObtMark = Convert.ToDecimal(txtVocationalPR1ObtMark.Text.Trim());
            if (!txtVocationalPR1MaxMark.Text.Trim().Equals(string.Empty)) objStudReg.VocationalPR1MaxMark = Convert.ToDecimal(txtVocationalPR1MaxMark.Text.Trim());
            if (!txtVocationalPR1Per.Text.Trim().Equals(string.Empty)) objStudReg.VocationalPR1Per = Convert.ToDecimal(txtVocationalPR1Per.Text.Trim());
            if (!txtVocationalPR2ObtMark.Text.Trim().Equals(string.Empty)) objStudReg.VocationalPR2ObtMark = Convert.ToDecimal(txtVocationalPR2ObtMark.Text.Trim());
            if (!txtVocationalPR2MaxMark.Text.Trim().Equals(string.Empty)) objStudReg.VocationalPR2MaxMark = Convert.ToDecimal(txtVocationalPR2MaxMark.Text.Trim());
            if (!txtVocationalPR2Per.Text.Trim().Equals(string.Empty)) objStudReg.VocationalPR2Per = Convert.ToDecimal(txtVocationalPR2Per.Text.Trim());
            if (!txtHSCTransferCertNo.Text.Trim().Equals(string.Empty)) objStudReg.HSCTransferCertNo = txtHSCTransferCertNo.Text.Trim();

            if (!txtSSCLangGdPoint.Text.Trim().Equals(string.Empty)) objStudReg.SSCLanguageGdPoint = Convert.ToDecimal(txtSSCLangGdPoint.Text.Trim());
            if (!txtSSCLang2GdPoint.Text.Trim().Equals(string.Empty)) objStudReg.SSCLanguage2GdPoint = Convert.ToDecimal(txtSSCLang2GdPoint.Text.Trim());

            if (!txtSSCLangGrade.Text.Trim().Equals(string.Empty)) objStudReg.SSCLanguageGrade = txtSSCLangGrade.Text.Trim();
            if (!txtSSCLang2Grade.Text.Trim().Equals(string.Empty)) objStudReg.SSCLanguage2Grade = txtSSCLang2Grade.Text.Trim();

            if (!txtSSCEngGdpoint.Text.Trim().Equals(string.Empty)) objStudReg.SSCEnglishGdPoint = Convert.ToDecimal(txtSSCEngGdpoint.Text.Trim());
            if (!txtSSCEngGrade.Text.Trim().Equals(string.Empty)) objStudReg.SSCEnglishGrade = txtSSCEngGrade.Text.Trim();
            if (!txtSSCMathGdPoint.Text.Trim().Equals(string.Empty)) objStudReg.SSCMathGdPoint = Convert.ToDecimal(txtSSCMathGdPoint.Text.Trim());
            if (!txtSSCMathGrade.Text.Trim().Equals(string.Empty)) objStudReg.SSCMathGrade = txtSSCMathGrade.Text.Trim();
            if (!txtSSCSciGdPoint.Text.Trim().Equals(string.Empty)) objStudReg.SSCScienceGdPoint = Convert.ToDecimal(txtSSCSciGdPoint.Text.Trim());
            if (!txtSSCSciGrade.Text.Trim().Equals(string.Empty)) objStudReg.SSCScienceGrade = txtSSCSciGrade.Text.Trim();
            if (!txtSSCSocSciGdPoint.Text.Trim().Equals(string.Empty)) objStudReg.SSCSocSciGdPoint = Convert.ToDecimal(txtSSCSocSciGdPoint.Text.Trim());
            if (!txtSSCSocSciGrade.Text.Trim().Equals(string.Empty)) objStudReg.SSCSocSciGrade = txtSSCSocSciGrade.Text.Trim();
            if (!txtiseComputerAppGdPoint.Text.Trim().Equals(string.Empty)) objStudReg.ISEComputerAppGdPoint = Convert.ToDecimal(txtiseComputerAppGdPoint.Text.Trim());
            if (!txtisecomputerAppGrade.Text.Trim().Equals(string.Empty)) objStudReg.ISEComputerAppGrade = txtisecomputerAppGrade.Text.Trim();
            if (!txtIseHistGdPoint.Text.Trim().Equals(string.Empty)) objStudReg.ISEHistoryGdPoint = Convert.ToDecimal(txtIseHistGdPoint.Text.Trim());
            if (!txtIseHistGrade.Text.Trim().Equals(string.Empty)) objStudReg.ISEHistoryGrade = txtIseHistGrade.Text.Trim();
            if (!txtHSCLangGdPoint.Text.Trim().Equals(string.Empty)) objStudReg.HSCLangGdPoint = Convert.ToDecimal(txtHSCLangGdPoint.Text.Trim());
            if (!txtHSCLangGrade.Text.Trim().Equals(string.Empty)) objStudReg.HSCLangGrade = txtHSCLangGrade.Text.Trim();
            if (!txtHSCEngGdpoint.Text.Trim().Equals(string.Empty)) objStudReg.HSCEnglishGdPoint = Convert.ToDecimal(txtHSCEngGdpoint.Text.Trim());
            if (!txtHSCEngGrade.Text.Trim().Equals(string.Empty)) objStudReg.HSCEnglishGrade = txtHSCEngGrade.Text.Trim();
            if (!txtHSCMathGdpoint.Text.Trim().Equals(string.Empty)) objStudReg.HSCMathGdPoint = Convert.ToDecimal(txtHSCMathGdpoint.Text.Trim());
            if (!txtHSCMathGrade.Text.Trim().Equals(string.Empty)) objStudReg.HSCMathGrade = txtHSCMathGrade.Text.Trim();
            if (!txtHSCPhyGdPoint.Text.Trim().Equals(string.Empty)) objStudReg.HSCPhyGdPoint = Convert.ToDecimal(txtHSCPhyGdPoint.Text.Trim());
            if (!txtHSCPhyGrade.Text.Trim().Equals(string.Empty)) objStudReg.HSCPhyGrade = txtHSCPhyGrade.Text.Trim();
            if (!txtHSCChemGdPoint.Text.Trim().Equals(string.Empty)) objStudReg.HSCChemGdPoint = Convert.ToDecimal(txtHSCChemGdPoint.Text.Trim());
            if (!txtHSCChemGrade.Text.Trim().Equals(string.Empty)) objStudReg.HSCChemGrade = txtHSCChemGrade.Text.Trim();

            if (!txtHSCOptionalMarkObt.Text.Trim().Equals(string.Empty)) objStudReg.HSCOptionalSubObtMark = Convert.ToDecimal(txtHSCOptionalMarkObt.Text.Trim());
            if (!txtHSCOptionalMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.HSCOptionalSubMaxMark = Convert.ToDecimal(txtHSCOptionalMaxMark.Text.Trim());
            if (!txtHSCOptionalPer.Text.Trim().Equals(string.Empty)) objStudReg.HSCOptionalSubPer = Convert.ToDecimal(txtHSCOptionalPer.Text.Trim());
            if (!txtHSCOptionalGdPoint.Text.Trim().Equals(string.Empty)) objStudReg.HSCOptionalSubGdPoint = Convert.ToDecimal(txtHSCOptionalGdPoint.Text.Trim());
            if (!txtHSCOptionalGrade.Text.Trim().Equals(string.Empty)) objStudReg.HSCOptionalSubGrade = txtHSCOptionalGrade.Text.Trim();

            //Added by Pritish on 25/02/2021
            if (!txtBotMarkObt.Text.Trim().Equals(string.Empty)) objStudReg.BotanyObtMark = Convert.ToDecimal(txtBotMarkObt.Text.Trim());
            if (!txtBotMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.BotanyMaxMark = Convert.ToDecimal(txtBotMaxMark.Text.Trim());
            if (!txtBotPerMark.Text.Trim().Equals(string.Empty)) objStudReg.BotanyPer = Convert.ToDecimal(txtBotPerMark.Text.Trim());
            if (!txtBotGdPt.Text.Trim().Equals(string.Empty)) objStudReg.BotanyGdPoint = Convert.ToDecimal(txtBotGdPt.Text.Trim());
            if (!txtBotGrade.Text.Trim().Equals(string.Empty)) objStudReg.BotanyGrade = txtBotGrade.Text.Trim();

            if (!txtZooMarkObt.Text.Trim().Equals(string.Empty)) objStudReg.ZooObtMark = Convert.ToDecimal(txtZooMarkObt.Text.Trim());
            if (!txtZooMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.ZooMaxMark = Convert.ToDecimal(txtZooMaxMark.Text.Trim());
            if (!txtZooPerMark.Text.Trim().Equals(string.Empty)) objStudReg.ZooPer = Convert.ToDecimal(txtZooPerMark.Text.Trim());
            if (!txtZooGdPt.Text.Trim().Equals(string.Empty)) objStudReg.ZooGdPoint = Convert.ToDecimal(txtZooGdPt.Text.Trim());
            if (!txtZooGrade.Text.Trim().Equals(string.Empty)) objStudReg.ZooGrade = txtZooGrade.Text.Trim();

            if (!txtCSMarkObt.Text.Trim().Equals(string.Empty)) objStudReg.CSObtMark = Convert.ToDecimal(txtCSMarkObt.Text.Trim());
            if (!txtCSMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.CSMaxMark = Convert.ToDecimal(txtCSMaxMark.Text.Trim());
            if (!txtCSPerMark.Text.Trim().Equals(string.Empty)) objStudReg.CSPer = Convert.ToDecimal(txtCSPerMark.Text.Trim());
            if (!txtCSGdPt.Text.Trim().Equals(string.Empty)) objStudReg.CSGdPoint = Convert.ToDecimal(txtCSGdPt.Text.Trim());
            if (!txtCSGrade.Text.Trim().Equals(string.Empty)) objStudReg.CSGrade = txtCSGrade.Text.Trim();

            if (!txtVocationalTHGdPoint.Text.Trim().Equals(string.Empty)) objStudReg.VocationalTHGdPoint = Convert.ToDecimal(txtVocationalTHGdPoint.Text.Trim());
            if (!txtVocationalTHGrade.Text.Trim().Equals(string.Empty)) objStudReg.VocationalTHGrade = txtVocationalTHGrade.Text.Trim();
            if (!txtVocationalPR1GradePoint.Text.Trim().Equals(string.Empty)) objStudReg.VocationalPR1GdPoint = Convert.ToDecimal(txtVocationalPR1GradePoint.Text.Trim());
            if (!txtVocationalPR1Grade.Text.Trim().Equals(string.Empty)) objStudReg.VocationalPR1Grade = txtVocationalPR1Grade.Text.Trim();
            if (!txtVocationalPR2Gdpoint.Text.Trim().Equals(string.Empty)) objStudReg.VocationalPR2GdPoint = Convert.ToDecimal(txtVocationalPR2Gdpoint.Text.Trim());
            if (!txtVocationalPR2Grade.Text.Trim().Equals(string.Empty)) objStudReg.VocationalPR2Grade = txtVocationalPR2Grade.Text.Trim();

            if (!txtDIPTransferCertNo.Text.Trim().Equals(string.Empty)) objStudReg.DIPTransferCertNo = txtDIPTransferCertNo.Text.Trim();

            if (!txtNameofDiploma.Text.Trim().Equals(string.Empty)) objStudReg.NameofDiploma = txtNameofDiploma.Text.Trim();
            if (!txtDiplomaCollege.Text.Trim().Equals(string.Empty)) objStudReg.DiplomaCollegeName = txtDiplomaCollege.Text.Trim();
            if (!txtDiplomaBoard.Text.Trim().Equals(string.Empty)) objStudReg.DiplomaBoard = txtDiplomaBoard.Text.Trim();
            if (!txtDipRegisterNo.Text.Trim().Equals(string.Empty)) objStudReg.DiplomaRegNumber = txtDipRegisterNo.Text.Trim();
            if (!txtDiplomaYear.Text.Trim().Equals(string.Empty)) objStudReg.DiplomaYearOfPassing = txtDiplomaYear.Text.Trim();
            if (!txtSemIObtained.Text.Trim().Equals(string.Empty)) objStudReg.SemIObtainedMark = Convert.ToDecimal(txtSemIObtained.Text.Trim());
            if (!txtSemIMax.Text.Trim().Equals(string.Empty)) objStudReg.SemIMaxMark = Convert.ToDecimal(txtSemIMax.Text.Trim());
            if (!txtSemIPer.Text.Trim().Equals(string.Empty)) objStudReg.SemIPer = Convert.ToDecimal(txtSemIPer.Text.Trim());
            if (!txtSemIIObtained.Text.Trim().Equals(string.Empty)) objStudReg.SemIIObtainedMark = Convert.ToDecimal(txtSemIIObtained.Text.Trim());
            if (!txtSemIIMax.Text.Trim().Equals(string.Empty)) objStudReg.SemIIMaxMark = Convert.ToDecimal(txtSemIIMax.Text.Trim());
            if (!txtSemIIPer.Text.Trim().Equals(string.Empty)) objStudReg.SemIIPer = Convert.ToDecimal(txtSemIIPer.Text.Trim());
            if (!txtSemIIIObtained.Text.Trim().Equals(string.Empty)) objStudReg.SemIIIObtainedMark = Convert.ToDecimal(txtSemIIIObtained.Text.Trim());
            if (!txtSemIIIMax.Text.Trim().Equals(string.Empty)) objStudReg.SemIIIMaxMark = Convert.ToDecimal(txtSemIIIMax.Text.Trim());
            if (!txtSemIIIPer.Text.Trim().Equals(string.Empty)) objStudReg.SemIIIPer = Convert.ToDecimal(txtSemIIIPer.Text.Trim());
            if (!txtSemIVObtained.Text.Trim().Equals(string.Empty)) objStudReg.SemIVObtainedMark = Convert.ToDecimal(txtSemIVObtained.Text.Trim());
            if (!txtSemIVMax.Text.Trim().Equals(string.Empty)) objStudReg.SemIVMaxMark = Convert.ToDecimal(txtSemIVMax.Text.Trim());
            if (!txtSemIVPer.Text.Trim().Equals(string.Empty)) objStudReg.SemIVPer = Convert.ToDecimal(txtSemIVPer.Text.Trim());
            if (!txtSemVObtained.Text.Trim().Equals(string.Empty)) objStudReg.SemVObtainedMark = Convert.ToDecimal(txtSemVObtained.Text.Trim());
            if (!txtSemVMax.Text.Trim().Equals(string.Empty)) objStudReg.SemVMaxMark = Convert.ToDecimal(txtSemVMax.Text.Trim());
            if (!txtSemVPer.Text.Trim().Equals(string.Empty)) objStudReg.SemVPer = Convert.ToDecimal(txtSemVPer.Text.Trim());
            if (!txtSemVIObtained.Text.Trim().Equals(string.Empty)) objStudReg.SemVIObtainedMark = Convert.ToDecimal(txtSemVIObtained.Text.Trim());
            if (!txtSemVIMax.Text.Trim().Equals(string.Empty)) objStudReg.SemVIMaxMark = Convert.ToDecimal(txtSemVIMax.Text.Trim());
            if (!txtSemVIPer.Text.Trim().Equals(string.Empty)) objStudReg.SemVIPer = Convert.ToDecimal(txtSemVIPer.Text.Trim());
            if (!txtSemVIIObtained.Text.Trim().Equals(string.Empty)) objStudReg.SemVIIObtainedMark = Convert.ToDecimal(txtSemVIIObtained.Text.Trim());
            if (!txtSemVIIMax.Text.Trim().Equals(string.Empty)) objStudReg.SemVIIMaxMark = Convert.ToDecimal(txtSemVIIMax.Text.Trim());
            if (!txtSemVIIPer.Text.Trim().Equals(string.Empty)) objStudReg.SemVIIPer = Convert.ToDecimal(txtSemVIIPer.Text.Trim());

            if (!txtSemVIIIObtained.Text.Trim().Equals(string.Empty)) objStudReg.SemVIIIObtainedMark = Convert.ToDecimal(txtSemVIIIObtained.Text.Trim());
            if (!txtSemVIIIMax.Text.Trim().Equals(string.Empty)) objStudReg.SemVIIIMaxMark = Convert.ToDecimal(txtSemVIIIMax.Text.Trim());
            if (!txtSemVIIIPer.Text.Trim().Equals(string.Empty)) objStudReg.SemVIIIPer = Convert.ToDecimal(txtSemVIIIPer.Text.Trim());

            if (!txtSpecialization.Text.Trim().Equals(string.Empty)) objStudReg.Specialization = txtSpecialization.Text.Trim();
            objStudReg.DipDegree = Convert.ToInt32(ddlDipDegree.SelectedValue);
            if (!txtTotalMarksScore.Text.Trim().Equals(string.Empty)) objStudReg.TotalMarkScoredDip = Convert.ToDecimal(txtTotalMarksScore.Text.Trim());
            if (!txtTotalMaxMark.Text.Trim().Equals(string.Empty)) objStudReg.TotalofMaxMarkDip = Convert.ToDecimal(txtTotalMaxMark.Text.Trim());
            if (!txtTotalPer.Text.Trim().Equals(string.Empty)) objStudReg.TotalPercentageDip = Convert.ToDecimal(txtTotalPer.Text.Trim());
            if (!txtSSCtotalGradePoint.Text.Trim().Equals(string.Empty)) objStudReg.SSCTotalGradePoint = Convert.ToDecimal(txtSSCtotalGradePoint.Text.Trim());
            if (!txtSSCCGPA.Text.Trim().Equals(string.Empty)) objStudReg.SSCCGPA = Convert.ToDecimal(txtSSCCGPA.Text.Trim());
            if (!txtHSCtotalGradePoint.Text.Trim().Equals(string.Empty)) objStudReg.HSCTotalGradePoint = Convert.ToDecimal(txtHSCtotalGradePoint.Text.Trim());
            if (!txtHSCCGPA.Text.Trim().Equals(string.Empty)) objStudReg.HSCCGPA = Convert.ToDecimal(txtHSCCGPA.Text.Trim());
            //Added by Naresh Beerla [29092020]
            if (!txtOtherBoardSSC.Text.Trim().Equals(string.Empty)) objStudReg.SSCotherBoard = txtOtherBoardSSC.Text.Trim();
            if (!txtOtherBoardHSC.Text.Trim().Equals(string.Empty)) objStudReg.HSCotherBoard = txtOtherBoardHSC.Text.Trim();
            objStudReg.SSCInputSystem = Convert.ToInt32(rblInputSystem.SelectedValue);
            objStudReg.HSCInputSystem = Convert.ToInt32(rblInputsystemHSC.SelectedValue);

            CustomStatus cs = CustomStatus.Others;
            string degreeno = "0";
            if (Session["degreetype"].ToString() == "")
            {
                degreeno = objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Session["studidno"].ToString()) == string.Empty ? "0" : objCommon.LookUp("ACD_sTUDENT", "DEGREENO", "IDNO=" + Session["studidno"].ToString());
            }

            degreeno = Session["degreetype"].ToString() == "" ? degreeno : Session["degreetype"].ToString();

            //if (Session["degreetype"].ToString() == "1") // UG
            //{
            cs = (CustomStatus)objStudRegC.AddUpdateStudentLastExamDetail(objStudReg);
            // }

            if (Convert.ToInt32(cs) == 1 || Convert.ToInt32(cs) == 2)
            {
                if (Session["usertype"] == "2")
                {
                    Response.Redirect("StudentInfo.aspx#step-4", false);
                }
                else
                {
                    Response.Redirect("StudentInfo.aspx#step-4", false);
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
            }

        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this, "Something went wrong ..Please try again !!", this);
            return;
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "PresentationLayer_StudentRegistration.btnSaveNext3_Click-> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    /// uploads -- Step-4

    //protected void btnSaveNext4_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        FileUpload fuStudPhoto = fuStudentPhoto1 as FileUpload;
    //        FileUpload fuStudSign = fuStudentSign as FileUpload;
    //        HttpPostedFile filephoto = fuStudPhoto.PostedFile;
    //        HttpPostedFile fileSign = fuStudSign.PostedFile;
    //        byte[] photo = null;
    //        byte[] sign = null;
    //        int count = 0;

    //        string RegNo = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO = " + Session["studidno"] + "");
    //        DataTable dt = new DataTable();
    //        dt.Columns.Add("ID");
    //        dt.Columns.Add("NAME");
    //        dt.Columns.Add("TYPE");

    //        if (fuStudPhoto.HasFile)
    //        {
    //            AL.Blob_Upload(blob_ConStr, blob_ContainerName, RegNo + "_spic", fuStudPhoto);
    //            dt.Rows.Add(RegNo, RegNo + "_spic" + Path.GetExtension(fuStudPhoto.FileName), 1);
    //        }
    //        if (fuStudSign.HasFile)
    //        {
    //            AL.Blob_Upload(blob_ConStr, blob_ContainerName, RegNo + "_sign", fuStudSign);
    //            dt.Rows.Add(RegNo, RegNo + "_sign" + Path.GetExtension(fuStudSign.FileName), 2);
    //        }

    //        if (filephoto.ContentLength <= 51200 && fileSign.ContentLength <= 51200)
    //        {
    //            string SP_Name = "PKG_STUD_BULK_PHOTO_UPLOAD";
    //            string SP_Parameters = "@P_TBL, @P_OPERATION";
    //            string Call_Values = "0,3";
    //            count = Convert.ToInt32(AL.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, dt, true, 2));
    //        }
    //        else
    //        {
    //            lblmessageShow.ForeColor = System.Drawing.Color.Red;
    //            lblmessageShow.Text = "Photo Size must be less than 50 kb";
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    //        }
    //        if (count == 1 || count == 2)
    //        {
    //            uploadDocument();
    //            if (Session["usertype"] == "2")
    //            {
    //                Response.Redirect("StudentInfo.aspx#step-5", false);
    //            }
    //            else
    //            {
    //                Response.Redirect("StudentInfo.aspx#step-5", false);
    //            }
    //        }
    //        else
    //        {
    //            lblmessageShow.ForeColor = System.Drawing.Color.Red;
    //            lblmessageShow.Text = "Failed to upload ! Please try again !";
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        objCommon.DisplayMessage(this, "Something went wrong ..Please try again !!", this);
    //        return;
    //    }

    //}

    /// Blob Storage
    //protected void btnSaveNext4_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        FileUpload fuStudPhoto = fuStudentPhoto1 as FileUpload;
    //        FileUpload fuStudSign = fuStudentSign as FileUpload;
    //        HttpPostedFile filephoto = fuStudPhoto.PostedFile;
    //        HttpPostedFile fileSign = fuStudSign.PostedFile;
    //        byte[] photo = null;
    //        byte[] sign = null;
    //        int count = 0;

    //        string IdNo = Session["stuinfoidno"].ToString();
    //        DataTable dt = new DataTable();
    //        dt.Columns.Add("ID");
    //        dt.Columns.Add("NAME");
    //        dt.Columns.Add("TYPE");

    //        if (fuStudPhoto.HasFile)
    //        {
    //            int retval = AL.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_spic", fuStudPhoto);
    //            if (retval == 0)
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
    //                return;
    //            }
    //            dt.Rows.Add(IdNo, IdNo + "_spic" + Path.GetExtension(fuStudPhoto.FileName), 1);
    //        }
    //        if (fuStudSign.HasFile)
    //        {
    //            int retval = AL.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_sign", fuStudSign);
    //            if (retval == 0)
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
    //                return;
    //            }
    //            dt.Rows.Add(IdNo, IdNo + "_sign" + Path.GetExtension(fuStudSign.FileName), 2);
    //        }

    //        if (filephoto.ContentLength <= 51200 && fileSign.ContentLength <= 51200)
    //        {
    //            string SP_Name = "PKG_STUD_BULK_PHOTO_UPLOAD";
    //            string SP_Parameters = "@P_TBL, @P_OPERATION";
    //            string Call_Values = "0,3";
    //            count = Convert.ToInt32(AL.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, dt, true, 2));
    //        }
    //        else
    //        {
    //            lblmessageShow.ForeColor = System.Drawing.Color.Red;
    //            lblmessageShow.Text = "Photo Size must be less than 50 kb";
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    //        }
    //        if (count == 1 || count == 2)
    //        {
    //            uploadDocument();
    //            if (Session["usertype"] == "2")
    //            {
    //                Response.Redirect("StudentInfo.aspx#step-5");
    //            }
    //            else
    //            {
    //                Response.Redirect("StudentInfo.aspx#step-5");
    //            }
    //        }
    //        else
    //        {
    //            lblmessageShow.ForeColor = System.Drawing.Color.Red;
    //            lblmessageShow.Text = "Failed to upload ! Please try again !";
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        objCommon.DisplayMessage(this, "Something went wrong ..Please try again !!", this);
    //        return;
    //    }

    //}

    public byte[] ResizePhoto(FileUpload fu)
    {
        byte[] image = null;
        if (fu.PostedFile != null && fu.PostedFile.FileName != "")
        {
            string strExtension = System.IO.Path.GetExtension(fu.FileName);

            // Resize Image Before Uploading to DataBase
            System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(fu.PostedFile.InputStream);
            int imageHeight = imageToBeResized.Height;
            int imageWidth = imageToBeResized.Width;
            int maxHeight = 240;
            int maxWidth = 320;
            imageHeight = (imageHeight * maxWidth) / imageWidth;
            imageWidth = maxWidth;

            if (imageHeight > maxHeight)
            {
                imageWidth = (imageWidth * maxHeight) / imageHeight;
                imageHeight = maxHeight;
            }

            // Saving image to smaller size and converting in byte[]
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageToBeResized, imageWidth, imageHeight);
            System.IO.MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            image = new byte[stream.Length + 1];
            stream.Read(image, 0, image.Length);
        }
        return image;
    }

    protected void btnSaveNext4_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objEc = new StudentController();
            Student objstud = new Student();
            int count = 0;
            int exist = 0;
            CustomStatus cs = new CustomStatus();
            CustomStatus cs1 = new CustomStatus();

            if (fuStudentPhoto1.HasFile)
            {
                objstud.StudPhoto = this.ResizePhoto(fuStudentPhoto1);
                objstud.IdNo = Convert.ToInt32(Session["studentID"].ToString());

                cs = (CustomStatus)objStudRegC.UpdateStudPhoto(objstud);

                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage("Student Photo Upload Successfully!!", this.Page);
                    count++;
                }
                else
                {
                    objCommon.DisplayMessage("Error!!", this.Page);
                }
            }
            else
            {
                string photo = objCommon.LookUp("ACD_STUD_PHOTO", "IDNO", "IDNO=" + Convert.ToInt32(txtidno.Text.Trim()) + " AND PHOTO IS NOT NULL");

                if (photo == "")
                {
                    exist++;
                    objCommon.DisplayMessage("Please Select Photo to Upload !!", this.Page);
                    return;
                }
            }

            //   CustomStatus cs = (CustomStatus)objStudRegC.UpdateStudPhoto(objstud);   


            if (fuStudentSign.HasFile)
            {
                objstud.StudSign = this.ResizePhoto(fuStudentSign);
                objstud.IdNo = Convert.ToInt32(Session["studentID"].ToString());

                cs1 = (CustomStatus)objStudRegC.UpdateStudSign(objstud);

                if (cs1.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage("Student Sign Upload Successfully!!", this.Page);
                    count++;
                }
                else
                {
                    objCommon.DisplayMessage("Error!!", this.Page);
                }
            }
            else
            {
                string sign = objCommon.LookUp("ACD_STUD_PHOTO", "IDNO", "IDNO=" + Convert.ToInt32(txtidno.Text.Trim()) + " AND STUD_SIGN IS NOT NULL");

                if (sign == "")
                {
                    exist++;
                    objCommon.DisplayMessage("Please Select Sign to Upload !!", this.Page);
                    return;
                }
            }

            // CustomStatus cs1 = (CustomStatus)objStudRegC.UpdateStudSign(objstud);     

            uploadDocument();

            if (count > 0 || exist == 0)
            {
                if (Session["usertype"] == "2")
                {
                    Response.Redirect("StudentInfo.aspx#step-5", false);
                }
                else
                {
                    Response.Redirect("StudentInfo.aspx#step-5", false);
                }
            }
            else
            {
                lblmessageShow.ForeColor = System.Drawing.Color.Red;
                lblmessageShow.Text = "Failed to upload ! Please try again !";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            }

        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this, "Something went wrong ..Please try again !!", this);
            return;
        }
    }

    protected void btnSaveNext6_Click(object sender, EventArgs e)
    {
        try
        {
            //Entrance Details
            objStudReg.IDNO = Convert.ToInt32(Session["studentID"]);

            if (rbEntrance.SelectedValue == "1")
            {
                if (!txtEntranceName.Text.Trim().Equals(string.Empty)) objStudReg.EntranceName = txtEntranceName.Text.Trim();
                if (!txtEntranceRollno.Text.Trim().Equals(string.Empty)) objStudReg.EntranceRollno = txtEntranceRollno.Text.Trim();

                if (!txtCutOffMarks.Text.Trim().Equals(string.Empty)) objStudReg.CutOffMarks = Convert.ToDecimal(txtCutOffMarks.Text.Trim());
                if (!txtOverallMark.Text.Trim().Equals(string.Empty)) objStudReg.OverAllMarks = Convert.ToDecimal(txtOverallMark.Text.Trim());
                if (!txtCommunityRank.Text.Trim().Equals(string.Empty)) objStudReg.CommunityRank = Convert.ToInt32(txtCommunityRank.Text.Trim());
                if (!txtOverAllRank.Text.Trim().Equals(string.Empty)) objStudReg.OverAllRank = Convert.ToInt32(txtOverAllRank.Text.Trim());
                if (!txtTNEAAppliactionNo.Text.Trim().Equals(string.Empty)) objStudReg.TNEAApplicationNo = txtTNEAAppliactionNo.Text.Trim();
                if (!txtAknowledgeRecNo.Text.Trim().Equals(string.Empty)) objStudReg.AcknowledgeRecNo = txtAknowledgeRecNo.Text.Trim();
                if (!txtAdmissionOrderNo.Text.Trim().Equals(string.Empty)) objStudReg.AdmOrderNo = txtAdmissionOrderNo.Text.Trim();
                if (!txtAdvPaymentAmt.Text.Trim().Equals(string.Empty)) objStudReg.AdvPaymentAmt = Convert.ToDecimal(txtAdvPaymentAmt.Text.Trim());
                if (!txtAdmOrderDate.Text.Trim().Equals(string.Empty)) objStudReg.AdmOrderDate = Convert.ToDateTime(txtAdmOrderDate.Text.Trim());
                if (!txtAckRecDate.Text.Trim().Equals(string.Empty)) objStudReg.AcknowledgeRecDate = Convert.ToDateTime(txtAckRecDate.Text.Trim());
                if (!txtApplicationNo.Text.Trim().Equals(string.Empty)) objStudReg.DoteApplicationNo = txtApplicationNo.Text.Trim();
                if (!txtAllotmentOrderNo.Text.Trim().Equals(string.Empty)) objStudReg.DoteAllotmentOrderNo = txtAllotmentOrderNo.Text.Trim();
                if (!txtAllotmentOrderDate.Text.Trim().Equals(string.Empty)) objStudReg.DoteAllotmentOrderDate = Convert.ToDateTime(txtAllotmentOrderDate.Text.Trim());

                if (!txtPercentile.Text.Trim().Equals(string.Empty)) objStudReg.Percentile = Convert.ToDecimal(txtPercentile.Text.Trim());

                CustomStatus cs = (CustomStatus)objStudRegC.AddUpdateStudentEntranceDetail(objStudReg);
                if (Convert.ToInt32(cs) == 1 || Convert.ToInt32(cs) == 2)
                {
                    if (Session["usertype"] == "2")
                    {
                        Response.Redirect("StudentInfo.aspx#step-6", false);
                    }
                    else
                    {
                        Response.Redirect("StudentInfo.aspx#step-6", false);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
                }
            }
            else
            {
                Response.Redirect("StudentInfo.aspx#step-6", false);
            }

        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this, "Something went wrong ..Please try again !!", this);
            return;
        }
    }

    //parent details -- Step-6
    protected void btnSaveNext7_Click(object sender, EventArgs e)
    {
        try
        {
            //Added by Abhinay Lad [29-06-2019]

            // IF Single Parent == YES
            if (rdoSingleParent.SelectedValue.ToString().Equals("1"))
            {
                // Check Parent Type
                if (ddlSelectParent.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(this, "Please Select Parent !", this);
                    ddlSelectParent.Focus();
                    return;
                }
                // if Parent type == Father
                else if (ddlSelectParent.SelectedValue.ToString().Equals("1"))
                {
                    if (txtFatherName.Text == "")
                    {
                        objCommon.DisplayMessage(this, "Please Enter Father Name !", this);
                        txtFatherName.Focus();
                        return;
                    }
                    else if (txtFatherInitial.Text == "")
                    {
                        objCommon.DisplayMessage(this, "Please Enter Father Initial !", this);
                        txtFatherInitial.Focus();
                        return;
                    }
                    else if (txtFOtherQaulification.Visible == true)
                    {
                        if (txtFOtherQaulification.Text == "")
                        {
                            objCommon.DisplayMessage(this, "Please Enter Father Qualification !", this);
                            txtFOtherQaulification.Focus();
                            return;
                        }
                    }
                    else if (ddlFQualification.Visible == true)
                    {
                        if (ddlFQualification.SelectedValue.ToString().Equals("0"))
                        {
                            objCommon.DisplayMessage(this, "Please Select Father Qualification !", this);
                            ddlFQualification.Focus();
                            return;
                        }
                    }
                    if (ddlFOccupation.SelectedValue.ToString().Equals("0"))
                    {
                        objCommon.DisplayMessage(this, "Please Enter Father Occupation !", this);
                        ddlFOccupation.Focus();
                        return;
                    }
                    else if (txtFAnnualIncome.Text == "")
                    {
                        objCommon.DisplayMessage(this, "Please Enter Father Annual Income !", this);
                        txtFAnnualIncome.Focus();
                        return;
                    }
                    else if (txtFMobileNo.Text == "")
                    {
                        objCommon.DisplayMessage(this, "Please Enter Father Mobile Number !", this);
                        txtFMobileNo.Focus();
                        return;
                    }
                    else if (txtFMobileNo.Text.Length != 10)
                    {
                        objCommon.DisplayMessage(this, "Please Enter Valid Father Mobile Number !", this);
                        txtFMobileNo.Focus();
                        return;
                    }
                    else if (txtFatherAadhar.Text == "")
                    {
                        objCommon.DisplayMessage(this, "Please Enter Father Aadhar Card Number!", this);
                        txtFatherAadhar.Focus();
                        return;
                    }
                }
                // if Parent Type == Mother
                else if (ddlSelectParent.SelectedValue.ToString().Equals("2"))
                {
                    if (txtMotherName.Text == "")
                    {
                        objCommon.DisplayMessage(this, "Please Enter Mother Name !", this);
                        txtMotherName.Focus();
                        return;
                    }
                    else if (txtMotherInitial.Text == "")
                    {
                        objCommon.DisplayMessage(this, "Please Enter Mother Initial !", this);
                        txtMotherInitial.Focus();
                        return;
                    }
                    else if (txtMotherMobile.Text == "")
                    {
                        objCommon.DisplayMessage(this, "Please Enter Mother Mobile Number !", this);
                        txtMotherMobile.Focus();
                        return;
                    }
                    else if (txtMotherMobile.Text.Length != 10)
                    {
                        objCommon.DisplayMessage(this, "Please Enter Valid Mother Mobile Number !", this);
                        txtMotherMobile.Focus();
                        return;
                    }
                    else if (txtMotherAadhar.Text == "")
                    {
                        objCommon.DisplayMessage(this, "Please Enter Mother Aadhar Card Number !", this);
                        txtMotherAadhar.Focus();
                        return;
                    }
                }
            }
            // IF Single Parent == NO
            else
            {
                // for Father
                if (txtFatherName.Text == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter Father Name !", this);
                    txtFatherName.Focus();
                    return;
                }
                else if (txtFatherInitial.Text == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter Father Initial !", this);
                    txtFatherInitial.Focus();
                    return;
                }
                else if (txtFOtherQaulification.Visible == true)
                {
                    if (txtFOtherQaulification.Text == "")
                    {
                        objCommon.DisplayMessage(this, "Please Enter Father Qualification !", this);
                        txtFOtherQaulification.Focus();
                        return;
                    }
                }
                else if (ddlFQualification.Visible == true)
                {
                    if (ddlFQualification.SelectedValue.ToString().Equals("0"))
                    {
                        objCommon.DisplayMessage(this, "Please Select Father Qualification !", this);
                        ddlFQualification.Focus();
                        return;
                    }
                }
                if (ddlFOccupation.SelectedValue.ToString().Equals("0"))
                {
                    objCommon.DisplayMessage(this, "Please Enter Father Occupation !", this);
                    ddlFOccupation.Focus();
                    return;
                }
                else if (txtFAnnualIncome.Text == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter Father Annual Income !", this);
                    txtFAnnualIncome.Focus();
                    return;
                }
                else if (txtFMobileNo.Text == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter Father Mobile Number !", this);
                    txtFMobileNo.Focus();
                    return;
                }
                else if (txtFMobileNo.Text.Length != 10)
                {
                    objCommon.DisplayMessage(this, "Please Enter Valid Father Mobile Number !", this);
                    txtFMobileNo.Focus();
                    return;
                }
                else if (txtFatherAadhar.Text == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter Father Aadhar Card Number!", this);
                    txtFatherAadhar.Focus();
                    return;
                }
                // for Mother
                else if (txtMotherName.Text == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter Mother Name !", this);
                    txtMotherName.Focus();
                    return;
                }
                else if (txtMotherInitial.Text == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter Mother Initial !", this);
                    txtMotherInitial.Focus();
                    return;
                }
                else if (txtMotherMobile.Text == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter Mother Mobile Number !", this);
                    txtMotherMobile.Focus();
                    return;
                }
                else if (txtMotherMobile.Text.Length != 10)
                {
                    objCommon.DisplayMessage(this, "Please Enter Valid Mother Mobile Number !", this);
                    txtMotherMobile.Focus();
                    return;
                }
                else if (txtMotherAadhar.Text == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter Mother Aadhar Card Number !", this);
                    txtMotherAadhar.Focus();
                    return;
                }
            }


            //Parent Details
            objStudReg.IDNO = Convert.ToInt32(Session["studentID"]);
            if (rdoSingleParent.SelectedValue == "1")
            {
                objStudReg.SingleParent = "Y";

                if (ddlSelectParent.SelectedValue.ToString().Equals("1"))
                {
                    objStudReg.Parent_Type = "F".ToString().Trim();
                }
                else
                {
                    objStudReg.Parent_Type = "M".ToString().Trim();
                }
            }
            else
            {
                objStudReg.SingleParent = "N";
            }

            if (!txtFatherName.Text.Trim().Equals(string.Empty)) objStudReg.FatherName = txtFatherName.Text.Trim();

            if (!txtFatherInitial.Text.Trim().Equals(string.Empty)) objStudReg.FatherLastName = txtFatherInitial.Text.Trim();
            objStudReg.FatherQualification = Convert.ToInt32(ddlFQualification.SelectedValue);
            objStudReg.FatherOccupation = Convert.ToInt32(ddlFOccupation.SelectedValue);
            if (!txtFOrganizationName.Text.Trim().Equals(string.Empty)) objStudReg.FatherOrgName = txtFOrganizationName.Text.Trim();

            if (ddlFDesignation.Visible == true)
            {
                objStudReg.FatherDesig = Convert.ToInt32(ddlFDesignation.SelectedValue);
            }
            else if (!txtOtherDesignation.Text.Trim().Equals(string.Empty) && txtOtherDesignation.Visible == true)
            {
                objStudReg.FatherOtherDesig = txtOtherDesignation.Text.Trim();
            }

            if (ddlFQualification.Visible == true)
            {
                objStudReg.FatherQualification = Convert.ToInt32(ddlFQualification.SelectedValue);
            }
            else if (!txtFOtherQaulification.Text.Trim().Equals(string.Empty) && txtFOtherQaulification.Visible == true)
            {
                objStudReg.FatherOtherQual = txtFOtherQaulification.Text.Trim();
            }

            // FOR MOTHER
            if (ddlMDesignation.Visible == true)
            {
                objStudReg.MotherDesig = Convert.ToInt32(ddlMDesignation.SelectedValue);
            }
            else if (!txtMOtherDesignation.Text.Trim().Equals(string.Empty) && txtMOtherDesignation.Visible == true)
            {
                objStudReg.MotherOtherDesig = txtMOtherDesignation.Text.Trim();
            }

            if (ddlMotherQualification.Visible == true)
            {
                objStudReg.MotherQual = Convert.ToInt32(ddlMotherQualification.SelectedValue);
            }
            else if (!txtMOtherQualification.Text.Trim().Equals(string.Empty) && txtMOtherQualification.Visible == true)
            {
                objStudReg.MotherOtherQual = txtMOtherQualification.Text.Trim();
            }

            // FOR GUARDIAN
            if (ddlGDesignation.Visible == true)
            {
                objStudReg.GuardianDesignation = Convert.ToInt32(ddlGDesignation.SelectedValue);
            }
            else if (!txtGOtherDesignation.Text.Trim().Equals(string.Empty) && txtGOtherDesignation.Visible == true)
            {
                objStudReg.GuardianOtherDesig = txtGOtherDesignation.Text.Trim();
            }

            if (ddlGQualification.Visible == true)
            {
                objStudReg.GuardianQual = Convert.ToInt32(ddlGQualification.SelectedValue);
            }
            else if (!txtGOtherQualification.Text.Trim().Equals(string.Empty) && txtGOtherQualification.Visible == true)
            {
                objStudReg.GuardianOtherQual = txtGOtherQualification.Text.Trim();
            }

            if (!txtFAnnualIncome.Text.Trim().Equals(string.Empty)) objStudReg.FatherAnnualIncome = txtFAnnualIncome.Text.Trim();
            if (!txtFMobileNo.Text.Trim().Equals(string.Empty)) objStudReg.FatherMobile = txtFMobileNo.Text.Trim();

            if (!txtFEmail.Text.Trim().Equals(string.Empty)) objStudReg.FEmailId = txtFEmail.Text.Trim();
            if (!txtFPANNumber.Text.Trim().Equals(string.Empty)) objStudReg.FPANNumber = txtFPANNumber.Text.Trim();
            if (!txtMEmail.Text.Trim().Equals(string.Empty)) objStudReg.MEmailId = txtMEmail.Text.Trim();
            if (!txtMPANNumber.Text.Trim().Equals(string.Empty)) objStudReg.MPANNumber = txtMPANNumber.Text.Trim();

            if (!txtFatherAadhar.Text.Trim().Equals(string.Empty)) objStudReg.FatherAdhaarcardNo = txtFatherAadhar.Text.Trim();
            if (!txtFOfficeAddress.Text.Trim().Equals(string.Empty)) objStudReg.FatherOrgAddress = txtFOfficeAddress.Text.Trim();
            objStudReg.FatherOrgCity = Convert.ToInt32(ddlFofficeCity.SelectedValue);
            objStudReg.FatherOrgState = Convert.ToInt32(ddlFofficeState.SelectedValue);
            if (!txtFofficePin.Text.Trim().Equals(string.Empty)) objStudReg.FatherOrgPin = txtFofficePin.Text.Trim();
            if (!txtFofficeSTD.Text.Trim().Equals(string.Empty)) objStudReg.FatherOrgSTD = txtFofficeSTD.Text.Trim();
            if (!txtFofficeLandline.Text.Trim().Equals(string.Empty)) objStudReg.FatherOrgPhone = txtFofficeLandline.Text.Trim();
            if (!txtMotherName.Text.Trim().Equals(string.Empty)) objStudReg.MotherName = txtMotherName.Text.Trim();
            if (!txtMotherInitial.Text.Trim().Equals(string.Empty)) objStudReg.MotherLastName = txtMotherInitial.Text.Trim();
            objStudReg.MotherQual = Convert.ToInt32(ddlMotherQualification.SelectedValue);
            objStudReg.MotherOccupation = Convert.ToInt32(ddlMOccupation.SelectedValue);
            if (!txtMotherMobile.Text.Trim().Equals(string.Empty)) objStudReg.MotherMobile = txtMotherMobile.Text.Trim();
            if (!txtMotherAadhar.Text.Trim().Equals(string.Empty)) objStudReg.MotherAdhaarcardNo = txtMotherAadhar.Text.Trim();
            if (!txtMofficeAdd.Text.Trim().Equals(string.Empty)) objStudReg.MotherOrgAdd = txtMofficeAdd.Text.Trim();
            objStudReg.MotherOrgCity = Convert.ToInt32(ddlMofficeCity.SelectedValue);
            objStudReg.MotherOrgState = Convert.ToInt32(ddlMofficeState.SelectedValue);
            if (!txtMofficePin.Text.Trim().Equals(string.Empty)) objStudReg.MotherOrgPin = txtMofficePin.Text.Trim();
            if (!txtMSTDCode.Text.Trim().Equals(string.Empty)) objStudReg.MotherOrgSTD = txtMSTDCode.Text.Trim();
            if (!txtMOfficeLandLine.Text.Trim().Equals(string.Empty)) objStudReg.MotherOrgPhone = txtMOfficeLandLine.Text.Trim();
            if (!txtGuardianName.Text.Trim().Equals(string.Empty)) objStudReg.GaurdianName = txtGuardianName.Text.Trim();
            if (!txtGuardianInitial.Text.Trim().Equals(string.Empty)) objStudReg.GaurdianLastName = txtGuardianInitial.Text.Trim();
            objStudReg.GuardianQual = Convert.ToInt32(ddlGQualification.SelectedValue);
            objStudReg.GuardianOccupation = Convert.ToInt32(ddlGOccupation.SelectedValue);
            if (!txtGWorkingOrg.Text.Trim().Equals(string.Empty)) objStudReg.GuardianOrgName = txtGWorkingOrg.Text.Trim();
            objStudReg.GuardianDesignation = Convert.ToInt32(ddlGDesignation.SelectedValue);
            if (!txtGAnnualIncome.Text.Trim().Equals(string.Empty)) objStudReg.GuardianAnnualIncome = txtGAnnualIncome.Text.Trim();
            if (!txtGOfficeAddress.Text.Trim().Equals(string.Empty)) objStudReg.GuardianOrgAdd = txtGOfficeAddress.Text.Trim();
            objStudReg.GuardianOrgCity = Convert.ToInt32(ddlGofficeCity.SelectedValue);
            objStudReg.GuardianOrgState = Convert.ToInt32(ddlGofficeState.SelectedValue);
            if (!txtGofficePin.Text.Trim().Equals(string.Empty)) objStudReg.GuardianOrgPin = txtGofficePin.Text.Trim();
            if (!txtGofficeSTD.Text.Trim().Equals(string.Empty)) objStudReg.GuardianOrgSTD = txtGofficeSTD.Text.Trim();
            if (!txtGofficeLandline.Text.Trim().Equals(string.Empty)) objStudReg.GuardianOrgPhone = txtGofficeLandline.Text.Trim();
            if (!txtMOrganizationName.Text.Trim().Equals(string.Empty)) objStudReg.MotherWorkingOrg = txtMOrganizationName.Text.Trim();
            if (!txtMAnnualIncome.Text.Trim().Equals(string.Empty)) objStudReg.MotherAnnualIncome = txtMAnnualIncome.Text.Trim();

            CustomStatus cs = (CustomStatus)objStudRegC.AddUpdateParentDetail(objStudReg);
            if (Convert.ToInt32(cs) == 1 || Convert.ToInt32(cs) == 2)
            {
                if (Session["usertype"] == "2")
                {
                    ViewState["action"] = "add";
                    ClearControls();
                    Session["stuinfoidno"] = "-1";
                    ShowReport("StudentRegistration", "UserInfo.rpt");

                    ScriptManager.RegisterStartupScript(this.updSports, this.GetType(),
                           "alert",
                           "alert('Parent Details Added Successfully!!');window.location ='StudentInfo.aspx#step-1';",
                                                                  true);
                    ShowReport("StudentRegistration", "UserInfo.rpt");
                }
                else if (Convert.ToInt32(Session["usertype"].ToString()) == 3)
                {
                    // Session["stuinfoidno"] = "-1";
                    //  ShowReport("StudentRegistration", "UserInfo.rpt");
                    Response.Redirect("StudentInfo.aspx#step-7", false);
                    //ScriptManager.RegisterStartupScript(this.updSports, this.GetType(),
                    //               "alert",
                    //               "alert('Parent Details Added Successfully!!');window.location ='StudentInfo.aspx#step-1';", true);
                    //   ShowReport("StudentRegistration", "UserInfo.rpt");
                }
                else
                {
                    Response.Redirect("StudentInfo.aspx#step-7", false);
                    //ViewState["action"] = "add";
                    //ClearControls();
                    //Session["stuinfoidno"] = "-1";

                    //ShowReport("StudentRegistration", "UserInfo.rpt");

                    //ScriptManager.RegisterStartupScript(this.updSports, this.GetType(),
                    //       "alert",
                    //       "alert('Parent Details Added Successfully!!');window.location ='StudentInfo.aspx#step-1';",
                    //                                              false);


                    //ShowReport("StudentRegistration", "UserInfo.rpt");

                    //Response.Redirect(Request.Url.ToString());
                    // Response.Redirect(Request.RawUrl,false);
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
            }

        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this, "Something went wrong ..Please try again !!", this);
            return;
        }

    }

    //Extra Details -- Step-7
    protected void btnSaveNext8_Click(object sender, EventArgs e)
    {
        try
        {
            objStudReg.IDNO = Convert.ToInt32(Session["studentID"]);
            if (!txtRelativeDetails.Text.Trim().Equals(string.Empty))
            {
                objStudReg.RelativeDetails = txtRelativeDetails.Text.Trim();
            }
            else
            {
                objStudReg.RelativeDetails = "";
            }
            if (!txtReasonToChoose.Text.Trim().Equals(string.Empty))
            {
                objStudReg.ReasonToChoose = txtReasonToChoose.Text.Trim();
            }
            else
            {
                objStudReg.ReasonToChoose = "";
            }
            if (!txtCommunityCertNo.Text.Trim().Equals(string.Empty)) objStudReg.CommunityCertNo = txtCommunityCertNo.Text.Trim();
            if (!txtCommunityCertIssueDate.Text.Trim().Equals(string.Empty)) objStudReg.CommunityCertIsueDate = Convert.ToDateTime(txtCommunityCertIssueDate.Text.Trim());
            if (!txtCommunityCertAuthority.Text.Trim().Equals(string.Empty)) objStudReg.CommunityCertAuthority = txtCommunityCertAuthority.Text.Trim();
            if (!txtTransferCertNo.Text.Trim().Equals(string.Empty)) objStudReg.TransferCertNo = txtTransferCertNo.Text.Trim();
            if (!txtTransferCertDate.Text.Trim().Equals(string.Empty)) objStudReg.TransferCertDate = Convert.ToDateTime(txtTransferCertDate.Text.Trim());
            if (!txtConductCertNo.Text.Trim().Equals(string.Empty)) objStudReg.ConductCertNo = txtConductCertNo.Text.Trim();
            if (!txtConductCertDate.Text.Trim().Equals(string.Empty)) objStudReg.ConductCertDate = Convert.ToDateTime(txtConductCertDate.Text.Trim());
            if (!txtFirstAppearanceRegno.Text.Trim().Equals(string.Empty)) objStudReg.FirstAppearanceRegno = txtFirstAppearanceRegno.Text.Trim();
            if (!txtFirstAppearanceYear.Text.Trim().Equals(string.Empty)) objStudReg.FirstAppearanceYear = txtFirstAppearanceYear.Text.Trim();
            if (!txtSecondAppearanceNo.Text.Trim().Equals(string.Empty)) objStudReg.SecondAppearanceRegno = txtSecondAppearanceNo.Text.Trim();
            if (!txtSecondAppearanceYear.Text.Trim().Equals(string.Empty)) objStudReg.SecondAppearanceYear = txtSecondAppearanceYear.Text.Trim();
            if (!txtThirdAppearanceRegno.Text.Trim().Equals(string.Empty)) objStudReg.ThirdAppearanceRegno = txtThirdAppearanceRegno.Text.Trim();
            if (!txtThirdAppearanceYear.Text.Trim().Equals(string.Empty)) objStudReg.ThirdAppearanceYear = txtThirdAppearanceYear.Text.Trim();

            if (ddlCommunity.SelectedValue.ToString().Equals("4") || ddlCommunity.SelectedValue.ToString().Equals("8"))
            {
                if (txtMinorityCertificateNo.Text == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter Minority Certificate no !", this);
                    txtMinorityCertificateNo.Focus();
                    return;
                }
                else if (txtMinorityCertIssueDate.Text == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter Minority Certificate Issue Date !", this);
                    txtMinorityCertIssueDate.Focus();
                    return;
                }
                else if (txtMinorityCertAuthority.Text == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter Issuing Authority !", this);
                    txtMinorityCertAuthority.Focus();
                    return;
                }
            }

            if (!txtMinorityCertificateNo.Text.Trim().Equals(string.Empty)) objStudReg.MinorityCertificateNo = txtMinorityCertificateNo.Text.Trim();
            else
                objStudReg.MinorityCertificateNo = string.Empty;
            if (!txtMinorityCertIssueDate.Text.Trim().Equals(string.Empty)) objStudReg.MinorityIssueDate = Convert.ToDateTime(txtMinorityCertIssueDate.Text.Trim());
            if (!txtMinorityCertAuthority.Text.Trim().Equals(string.Empty)) objStudReg.MinorityCertAuthority = txtMinorityCertAuthority.Text.Trim();
            else
                objStudReg.MinorityCertAuthority = string.Empty;
            CustomStatus cs = (CustomStatus)objStudRegC.AddUpdateExtraDetail(objStudReg);
            if (Convert.ToInt32(cs) == 1 || Convert.ToInt32(cs) == 2)
            {
                if (Session["usertype"] == "2")
                {
                    Response.Redirect("StudentInfo.aspx#step-8", false);
                }
                else
                {
                    Response.Redirect("StudentInfo.aspx#step-8", false);
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
            }


        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this, "Something went wrong ..Please try again !!", this);
            return;
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "PresentationLayer_StudentRegistration.btnSaveNext8_Click-> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    //Fees Concession -- Step-8
    protected void btnSaveNext9_Click(object sender, EventArgs e)
    {
        try
        {
            objStudReg.IDNO = Convert.ToInt32(Session["studentID"]);
            if (rbFirstGraduate.SelectedValue == "1")
            {
                objStudReg.FirstGraduate = "Y";
            }
            else
            {
                objStudReg.FirstGraduate = "N";
            }
            if (!txtFirstGraduateCertNo.Text.Trim().Equals(string.Empty)) objStudReg.FirstGraduateCertNo = txtFirstGraduateCertNo.Text.Trim();
            if (!txtFirstGraduateCertDate.Text.Trim().Equals(string.Empty)) objStudReg.FirstGraduateCertDate = Convert.ToDateTime(txtFirstGraduateCertDate.Text.Trim());
            if (!txtFirstGraduateCertAuth.Text.Trim().Equals(string.Empty)) objStudReg.FirstGraduateCertAuth = txtFirstGraduateCertAuth.Text.Trim();
            if (rbAICTEWaiver.SelectedValue == "1")
            {
                objStudReg.AICTEWaiver = "Y";
            }
            else
            {
                objStudReg.AICTEWaiver = "N";
            }
            if (!txtAICTECertNo.Text.Trim().Equals(string.Empty)) objStudReg.AICTECertNo = txtAICTECertNo.Text.Trim();
            if (!txtAICTECertDate.Text.Trim().Equals(string.Empty)) objStudReg.AICTECertDate = Convert.ToDateTime(txtAICTECertDate.Text.Trim());
            if (!txtAICTECertAuth.Text.Trim().Equals(string.Empty)) objStudReg.AICTECertAuth = txtAICTECertAuth.Text.Trim();
            if (rbDravidarWelfare.SelectedValue == "1")
            {
                objStudReg.DravidarWelfare = "Y";
            }
            else
            {
                objStudReg.DravidarWelfare = "N";
            }
            if (!txtWelfareCertNo.Text.Trim().Equals(string.Empty)) objStudReg.WelfareCertNo = txtWelfareCertNo.Text.Trim();
            if (!txtWelfareCertDate.Text.Trim().Equals(string.Empty)) objStudReg.WelfareCertDate = Convert.ToDateTime(txtWelfareCertDate.Text.Trim());
            if (!txtWelfareCertAuth.Text.Trim().Equals(string.Empty)) objStudReg.WelfareCertAuth = txtWelfareCertAuth.Text.Trim();

            CustomStatus cs = (CustomStatus)objStudRegC.AddUpdateFeesDetail(objStudReg);
            if (Convert.ToInt32(cs) == 1 || Convert.ToInt32(cs) == 2)
            {
                ShowStudentDetails();
                //btnReport.Visible = true;
                tab8Print.Visible = true;
                //Response.Redirect("StudentRegistration.aspx#step-9");
                lblmessageShow.ForeColor = System.Drawing.Color.Green;
                lblmessageShow.Text = "Information Saved successfully !";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                //objCommon.DisplayMessage(this, "Saved successfully !", this);
            }
            else
            {
                objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
            }


        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this, "Something went wrong ..Please try again !!", this);
            return;
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "PresentationLayer_StudentRegistration.btnSaveNext9_Click-> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH M ON (B.BRANCHNO = M.BRANCHNO)", "B.BRANCHNO", "LONGNAME", "B.BRANCHNO>0 AND DEGREENO=" + ddlDegree.SelectedValue + "", "LONGNAME");

            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this, "Something went wrong ..Please try again !!", this);
            return;
        }
    }

    protected void lvDocumentList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        CheckBox chkDocuments = e.Item.FindControl("chkOriCopy") as CheckBox;
        if (chkDocuments.Checked == true)
            hdnDocCount.Value = "1";
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("StudentRegistration", "UserInfo.rpt");
    }

    //Add other city for Local address switch controls
    protected void lbnLOtherCity_Click(object sender, EventArgs e)
    {
        if (txtLOtherCity.Visible == true)
        {
            ddlLcity.SelectedIndex = 0;
            ddlLcity.Visible = true;
            txtLOtherCity.Text = string.Empty;
            txtLOtherCity.Visible = false;
            lbnLOtherCity.Text = "Add Other City";

        }
        else
        {
            ddlLcity.SelectedIndex = 0;
            ddlLcity.Visible = false;
            txtLOtherCity.Visible = true;
            lbnLOtherCity.Text = "Select City";

        }
    }

    protected void ddlLcity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLcity.SelectedIndex > 0 && (ddlPCity.SelectedItem.Text == "OTHER" || ddlPCity.SelectedItem.Text == "Other"))
        {
            ddlLcity.SelectedIndex = 0;
            ddlLcity.Visible = false;
            txtLOtherCity.Visible = true;
            lbnLOtherCity.Text = "Select Designation";
        }
    }

    //Add other father designation switch controls
    protected void lbtnFOtherDesig_Click(object sender, EventArgs e)
    {
        if (txtOtherDesignation.Visible == true)
        {
            ddlFDesignation.SelectedIndex = 0;
            ddlFDesignation.Visible = true;
            txtOtherDesignation.Text = string.Empty;
            txtOtherDesignation.Visible = false;
            lbtnFOtherDesig.Text = "Add Other Designation";
        }
        else
        {
            ddlFDesignation.SelectedIndex = 0;
            ddlFDesignation.Visible = false;
            txtOtherDesignation.Visible = true;
            lbtnFOtherDesig.Text = "Select Designation";
        }
    }

    protected void ddlFDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFDesignation.SelectedIndex > 0 && ddlFDesignation.SelectedValue == "1")
        {
            ddlFDesignation.SelectedIndex = 0;
            ddlFDesignation.Visible = false;
            txtOtherDesignation.Visible = true;
            lbtnFOtherDesig.Text = "Select Designation";
        }
    }

    //Add other city for permanent address switch controls
    protected void lbtnPOtherCity_Click(object sender, EventArgs e)
    {
        if (txtPOthercity.Visible == true)
        {
            ddlPCity.SelectedIndex = 0;
            ddlPCity.Visible = true;
            txtPOthercity.Text = string.Empty;
            txtPOthercity.Visible = false;
            lbtnPOtherCity.Text = "Add Other City";

        }
        else
        {
            ddlPCity.SelectedIndex = 0;
            ddlPCity.Visible = false;
            txtPOthercity.Visible = true;
            lbtnPOtherCity.Text = "Select City";

        }
    }

    protected void ddlPCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPCity.SelectedIndex > 0 && (ddlPCity.SelectedItem.Text == "OTHER" || ddlPCity.SelectedItem.Text == "Other"))
        {
            ddlPCity.SelectedIndex = 0;
            ddlPCity.Visible = false;
            txtPOthercity.Visible = true;
            lbtnPOtherCity.Text = "Select City";
        }
    }

    //Add other city for guardian address switch controls
    protected void lnkbtnGothercity_Click(object sender, EventArgs e)
    {
        if (txtGOthercity.Visible == true)
        {
            ddlGCity.SelectedIndex = 0;
            ddlGCity.Visible = true;
            txtGOthercity.Text = string.Empty;
            txtGOthercity.Visible = false;
            lnkbtnGothercity.Text = "Add Other City";

        }
        else
        {
            ddlGCity.SelectedIndex = 0;
            ddlGCity.Visible = false;
            txtGOthercity.Visible = true;
            lnkbtnGothercity.Text = "Select City";

        }
    }

    protected void ddlGCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGCity.SelectedIndex > 0 && (ddlPCity.SelectedItem.Text == "OTHER" || ddlPCity.SelectedItem.Text == "Other"))
        {
            ddlGCity.SelectedIndex = 0;
            ddlGCity.Visible = false;
            txtGOthercity.Visible = true;
            lnkbtnGothercity.Text = "Select City";
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {

            if (ddlSemester.SelectedValue == "2")
            {
                hdnAdmissionType.Value = "2";
                ddlAcademicYear.SelectedValue = objCommon.LookUp("ACD_ADMBATCH", "MAX(BATCHNO)-1", "BATCHNO>0");

            }
            else
            {
                hdnAdmissionType.Value = "1";
                ddlAcademicYear.SelectedValue = objCommon.LookUp("ACD_ADMBATCH", "MAX(BATCHNO)", "BATCHNO>0");

            }


        }

    }

    protected void ddlHSCBoardCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlHSCBoardCategory.SelectedIndex > 0)
        {
            if (ddlHSCBoardCategory.SelectedValue == "7" || ddlHSCBoardCategory.SelectedValue == "9" || ddlHSCBoardCategory.SelectedValue == "0")
            {
                clearHSCNonVocational();
                ddlHSCOptionalSub.SelectedIndex = 0;
                divNonVocational.Visible = false;
                divVocational.Visible = true;
            }
            else
            {
                clearHSCVocational();
                divVocational.Visible = false;
                divNonVocational.Visible = true;
            }

            if (ddlHSCBoardCategory.SelectedValue == "1")
            {
                txtOtherBoardHSC.Enabled = true;
            }
            else
            {
                txtOtherBoardHSC.Enabled = false;
            }
        }
    }

    private void clearHSCVocational()
    {
        txtVocationalTHObtMark.Text = string.Empty;
        txtVocationalTHMaxMark.Text = string.Empty;
        txtVocationalTHPer.Text = string.Empty;
        txtVocationalTHGdPoint.Text = string.Empty;
        txtVocationalTHGrade.Text = string.Empty;

        txtVocationalPR1ObtMark.Text = string.Empty;
        txtVocationalPR1MaxMark.Text = string.Empty;
        txtVocationalPR1Per.Text = string.Empty;
        txtVocationalPR1GradePoint.Text = string.Empty;
        txtVocationalPR1Grade.Text = string.Empty;

        txtVocationalPR2ObtMark.Text = string.Empty;
        txtVocationalPR2MaxMark.Text = string.Empty;
        txtVocationalPR2Per.Text = string.Empty;
        txtVocationalPR2Gdpoint.Text = string.Empty;
        txtVocationalPR2Grade.Text = string.Empty;

        txtHSCTotalMarkScore.Text = string.Empty;
        txtHSCTotalMaxMark.Text = string.Empty;
        txtHSCTotalPer.Text = string.Empty;

    }

    private void clearHSCNonVocational()
    {
        txtHSCPhysicsObtMark.Text = string.Empty;
        txtHSCPhysicsMaxMark.Text = string.Empty;
        txtHSCPhysicsPer.Text = string.Empty;
        txtHSCPhyGdPoint.Text = string.Empty;
        txtHSCPhyGrade.Text = string.Empty;


        txtHSCChemObtMark.Text = string.Empty;
        txtHSCChemMaxMark.Text = string.Empty;
        txtHSCChemPer.Text = string.Empty;
        txtHSCChemGdPoint.Text = string.Empty;
        txtHSCChemGrade.Text = string.Empty;

        txtHSCOptionalMarkObt.Text = string.Empty;
        txtHSCOptionalMaxMark.Text = string.Empty;
        txtHSCOptionalPer.Text = string.Empty;
        txtHSCOptionalGdPoint.Text = string.Empty;
        txtHSCOptionalGrade.Text = string.Empty;

        txtHSCTotalMarkScore.Text = string.Empty;
        txtHSCTotalMaxMark.Text = string.Empty;
        txtHSCTotalPer.Text = string.Empty;

        txtBotMarkObt.Text = string.Empty;
        txtBotMaxMark.Text = string.Empty;
        txtBotPerMark.Text = string.Empty;
        txtBotGdPt.Text = string.Empty;
        txtBotGrade.Text = string.Empty;

        txtZooMarkObt.Text = string.Empty;
        txtZooMaxMark.Text = string.Empty;
        txtZooPerMark.Text = string.Empty;
        txtZooGdPt.Text = string.Empty;
        txtZooGrade.Text = string.Empty;

        txtCSMarkObt.Text = string.Empty;
        txtCSMaxMark.Text = string.Empty;
        txtCSPerMark.Text = string.Empty;
        txtCSGdPt.Text = string.Empty;
        txtCSGrade.Text = string.Empty;
    }

    protected void RdoHosteller_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RdoHosteller.SelectedValue == "1")
        {
            txtCollegeDistance.Enabled = true;
        }
        else
        {
            txtCollegeDistance.Enabled = false;
            txtCollegeDistance.Text = string.Empty;
        }
    }

    protected void lbtnMOtherQualification_Click(object sender, EventArgs e)
    {
        if (txtMOtherQualification.Visible == true)
        {
            ddlMotherQualification.SelectedIndex = 0;
            ddlMotherQualification.Visible = true;
            txtMOtherQualification.Text = string.Empty;
            txtMOtherQualification.Visible = false;
            lbtnMOtherQualification.Text = "Add Other Qualification";
        }
        else
        {
            ddlMotherQualification.SelectedIndex = 0;
            ddlMotherQualification.Visible = false;
            txtMOtherQualification.Visible = true;
            lbtnMOtherQualification.Text = "Select Qualification";
        }
    }

    protected void lbtnFOtherQualification_Click(object sender, EventArgs e)
    {
        if (txtFOtherQaulification.Visible == true)
        {
            ddlFQualification.SelectedIndex = 0;
            ddlFQualification.Visible = true;
            txtFOtherQaulification.Text = string.Empty;
            txtFOtherQaulification.Visible = false;
            lbtnFOtherQualification.Text = "Add Other Qualification";
        }
        else
        {
            ddlFQualification.SelectedIndex = 0;
            ddlFQualification.Visible = false;
            txtFOtherQaulification.Visible = true;
            lbtnFOtherQualification.Text = "Select Qualification";
        }
    }

    protected void ddlMDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMDesignation.SelectedIndex > 0 && ddlMDesignation.SelectedValue == "1")
        {
            ddlMDesignation.SelectedIndex = 0;
            ddlMDesignation.Visible = false;
            txtMOtherDesignation.Visible = true;
            lbtnMOtherDesig.Text = "Select Designation";
        }
    }

    protected void lbtnMOtherDesig_Click(object sender, EventArgs e)
    {
        if (txtMOtherDesignation.Visible == true)
        {
            ddlMDesignation.SelectedIndex = 0;
            ddlMDesignation.Visible = true;
            txtMOtherDesignation.Text = string.Empty;
            txtMOtherDesignation.Visible = false;
            lbtnMOtherDesig.Text = "Add Other Designation";

        }
        else
        {
            ddlMDesignation.SelectedIndex = 0;
            ddlMDesignation.Visible = false;
            txtMOtherDesignation.Visible = true;
            lbtnMOtherDesig.Text = "Select Designation";

        }
    }

    protected void lbtnGOtherQualification_Click(object sender, EventArgs e)
    {
        if (txtGOtherQualification.Visible == true)
        {
            ddlGQualification.SelectedIndex = 0;
            ddlGQualification.Visible = true;
            txtGOtherQualification.Text = string.Empty;
            txtGOtherQualification.Visible = false;
            lbtnGOtherQualification.Text = "Add Other Qualification";
        }
        else
        {
            ddlGQualification.SelectedIndex = 0;
            ddlGQualification.Visible = false;
            txtGOtherQualification.Visible = true;
            lbtnGOtherQualification.Text = "Select Qualification";
        }
    }

    protected void lbtnGOtherDesig_Click(object sender, EventArgs e)
    {
        if (txtGOtherDesignation.Visible == true)
        {
            ddlGDesignation.SelectedIndex = 0;
            ddlGDesignation.Visible = true;
            txtGOtherDesignation.Text = string.Empty;
            txtGOtherDesignation.Visible = false;
            lbtnGOtherDesig.Text = "Add Other Designation";

        }
        else
        {
            ddlGDesignation.SelectedIndex = 0;
            ddlGDesignation.Visible = false;
            txtGOtherDesignation.Visible = true;
            lbtnGOtherDesig.Text = "Select Designation";

        }
    }

    protected void chkCommToPermanent_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCommToPermanent.Checked)
        {
            txtPAddressLine1.Text = txtLAddressLine1.Text;
            txtPPin.Text = txtLPin.Text;
            txtPSTDCode.Text = txtLSTDCode.Text;
            txtPLandLine.Text = txtLLandLineNo.Text;
            ddlPState.SelectedValue = ddlLState.SelectedValue;
            if (lbnLOtherCity.Text.Equals("Select City") && !lbtnPOtherCity.Text.Equals("Select City"))
            {
                lbtnPOtherCity_Click(sender, e);

            }
            else if (!lbnLOtherCity.Text.Equals("Select City") && lbtnPOtherCity.Text.Equals("Select City"))
            {
                lbtnPOtherCity_Click(sender, e);
            }
            txtPOthercity.Text = txtLOtherCity.Text;
            ddlPCity.SelectedValue = ddlLcity.SelectedValue;
        }
        else
        {
            txtPAddressLine1.Text = string.Empty;
            txtPPin.Text = string.Empty;
            txtPSTDCode.Text = string.Empty;
            txtPLandLine.Text = string.Empty;
            txtPOthercity.Text = string.Empty;
            ddlPState.SelectedIndex = 0;
            ddlPCity.SelectedIndex = 0;
        }
    }

    protected void chkCommToGuardian_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCommToGuardian.Checked)
        {
            txtGaddressLine1.Text = txtLAddressLine1.Text;
            txtGPin.Text = txtLPin.Text;
            txtGSTDCode.Text = txtLSTDCode.Text;
            txtGLandLineNumber.Text = txtLLandLineNo.Text;
            ddlGState.SelectedValue = ddlLState.SelectedValue;
            if (lbnLOtherCity.Text.Equals("Select City") && !lnkbtnGothercity.Text.Equals("Select City"))
            {
                lnkbtnGothercity_Click(sender, e);

            }
            else if (!lbnLOtherCity.Text.Equals("Select City") && lnkbtnGothercity.Text.Equals("Select City"))
            {
                lnkbtnGothercity_Click(sender, e);
            }
            txtGOthercity.Text = txtLOtherCity.Text;
            ddlGCity.SelectedValue = ddlLcity.SelectedValue;
        }
        else
        {
            txtGaddressLine1.Text = string.Empty;
            txtGPin.Text = string.Empty;
            txtGSTDCode.Text = string.Empty;
            txtGLandLineNumber.Text = string.Empty;
            txtGOthercity.Text = string.Empty;
            ddlGState.SelectedIndex = 0;
            ddlGCity.SelectedIndex = 0;
        }
    }

    protected void ddlFQualification_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFQualification.SelectedValue == "1")
        {
            lbtnFOtherQualification_Click(sender, e);
            UpdatePanel1.Update();
        }
    }

    protected void ddlMotherQualification_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMotherQualification.SelectedValue == "1")
        {
            lbtnMOtherQualification_Click(sender, e);
            UpdatePanel1.Update();
        }
    }

    protected void ddlGQualification_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGQualification.SelectedValue == "1")
        {
            lbtnGOtherQualification_Click(sender, e);
            UpdatePanel1.Update();
        }
    }

    protected void ddlGDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGDesignation.SelectedValue == "1")
        {
            lbtnGOtherDesig_Click(sender, e);
            UpdatePanel1.Update();
        }
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
        {
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        }
        else
        {
            if (Request.Url.ToString().Contains("74"))
            {
                url = Request.Url.ToString();
            }
            else
            {
                url = Request.Url.ToString() + "?pageno=74";
            }
            //  url = "http://localhost:58682/PresentationLayer/academic/StudentInfo.aspx?pageno=74";
            //htp://localhost:58682/PresentationLayer/academic/StudentInfo.aspx
        }

        Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

        Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
        Session["stuinfofullname"] = lnk.Text.Trim();
        Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);

        ViewState["Search"] = 1;

        //Response.Redirect("~/academic/StudentInfo.aspx?id=1"); // for query string use to clear form for first load
        //Response.Redirect("StudentInfo.aspx#step-1",false);
        Response.Redirect(url, false);
    }

    private void bindlist(string category, string searchtext)
    {
        StudentController objSC = new StudentController();

        DataSet ds = objSC.SearchStudent(searchtext, category, Convert.ToInt32(Session["usertype"].ToString()), Convert.ToInt32(Session["userno"].ToString()));

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lblNoRecords.Text = "Total Records : 0";
        }
    }

    protected void rdoPH_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoPH.SelectedValue == "1")
        {
            ddlTypeOfPH.Enabled = true;
        }
        else
        {
            ddlTypeOfPH.SelectedIndex = 0;
            ddlTypeOfPH.Enabled = false;
        }
    }

    protected void rdoSingleParent_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoSingleParent.SelectedValue == "1")
        {
            ddlSelectParent.Enabled = true;
        }
        else
        {
            ddlSelectParent.SelectedIndex = 0;
            ddlSelectParent.Enabled = false;
            divFather1.Visible = true;
            divFather2.Visible = true;
            divMother1.Visible = true;
            divMother2.Visible = true;
            lblFather2.Visible = true;
        }
    }

    #endregion

    protected void rbtn_MaritalStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    #region "Step-9: Sports Details"
    /// <summary>
    /// step--999
    /// Added by NEha Baranwal (02/10/2019)
    /// Sports And Achievement
    /// Added by Neha Baranwal
    /// </summary>

    Student objStudent = new Student();
    StudentController objSC = new StudentController();
    int status = 0;
    static int sportsno = 0;

    //USED FOR BIND SPORT DETAILS IN LIST VIEW.
    public void BindSportsDetails()
    {
        if (Session["studentID"] != null)
        {
            DataSet ds = null;
            ds = objSC.GetAllSportsDetails(Convert.ToInt32(Session["studentID"].ToString()));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSportsDetails.DataSource = ds.Tables[0];
                lvSportsDetails.DataBind();
            }
            else
            {
                lvSportsDetails.DataSource = null;
                lvSportsDetails.DataBind();
            }
        }
    }

    //USED FOR UPLOAD SPORTS CERTIFICATE
    private void uploadSportsCertificate()
    {

        try
        {
            if (Session["studentID"] != null)
            {
                //string folderPath = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + txtCCode.Text + "\\";
                string folderPath = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + Session["studentID"].ToString() + "-" + txtNameOfGameOrAchievement.Text + "\\";
                if (fuSportsCertificate.HasFile)
                {
                    string ext = System.IO.Path.GetExtension(fuSportsCertificate.PostedFile.FileName);
                    HttpPostedFile file = fuSportsCertificate.PostedFile;
                    string filename = Path.GetFileName(fuSportsCertificate.PostedFile.FileName);
                    if (ext == ".pdf" || ext == ".png" || ext == ".jpeg" || ext == ".jpg" || ext == ".PNG" || ext == ".JPEG" || ext == ".JPG" || ext == ".PDF") //|| ext == ".doc" || ext == ".docx" 
                    {
                        if (file.ContentLength <= 51200)// 31457280 before size  //For Allowing 50 Kb Size Files only 
                        {
                            string contentType = fuSportsCertificate.PostedFile.ContentType;
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }
                            objStudent.sportsFilename = filename;
                            objStudent.sportsFilepath = folderPath + filename;
                            fuSportsCertificate.PostedFile.SaveAs(folderPath + filename);
                            status = 1;
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updSports, "Document size must not exceed 50 Kb !", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updSports, "Only .jpg,.png,.jpeg,.pdf file type allowed  !", this.Page);
                        return;
                    }

                }
                else
                {

                    objStudent.sportsFilename = "";
                    objStudent.sportsFilepath = "";
                }
            }
        }
        catch (Exception ex)
        {
            // objCommon.DisplayMessage(this, "Something went wrong ! Please contact Admin", this.Page);

        }
    }

    //RESET ALL THE CONTROLLERS
    private void ClearControls()
    {
        ViewState["action"] = "add";


        txtNameOfGameOrAchievement.Text = "";
        txtDate.Text = "";
        txtVenue.Text = "";
        ddlLevelOfParticipation.SelectedIndex = 0;
        ddlAchievement.SelectedIndex = 0;

        lvSportsCertificate.DataSource = null;
        lvSportsCertificate.DataBind();
        lvSportsDetails.DataSource = null;
        lvSportsDetails.DataBind();
        //lblStatus.Text = string.Empty;

        DataSet ds = null;
    }

    //SHOWING SPORT DETAILS
    private void ShowDetails(int sportsno)
    {
        try
        {
            SqlDataReader dr = objSC.GetSportsDetails(sportsno);
            if (dr != null)
            {
                if (dr.Read())
                {
                    txtNameOfGameOrAchievement.Text = dr["SPORTS_NAME"] == DBNull.Value ? string.Empty : dr["SPORTS_NAME"].ToString();
                    txtDate.Text = dr["SPORTS_DATE"] == DBNull.Value ? "0" : dr["SPORTS_DATE"].ToString();
                    txtVenue.Text = dr["SPORTS_VENUE"] == DBNull.Value ? "0" : dr["SPORTS_VENUE"].ToString();
                    ddlLevelOfParticipation.SelectedValue = dr["PARTICIPATION_NO"] == DBNull.Value ? "-1" : dr["PARTICIPATION_NO"].ToString();
                    ddlAchievement.SelectedValue = dr["ACHIEVEMENT_NO"] == DBNull.Value ? "-1" : dr["ACHIEVEMENT_NO"].ToString();


                    //to get uploaded Certificates
                    DataSet dsCERT = new DataSet();

                    dsCERT = objSC.GetCertificatesBySportsNo(sportsno);
                    if (dsCERT.Tables[0].Rows.Count > 0)
                    {
                        lvSportsCertificate.DataSource = dsCERT;
                        lvSportsCertificate.DataBind();
                    }
                    else
                    {
                        lvSportsCertificate.DataSource = null;
                        lvSportsCertificate.DataBind();
                    }
                }
                dr.Close();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Studentinfo.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //USED FOR SAVE SPORT DETAILS
    protected void btnSaveNext10_Click(object sender, EventArgs e)
    {

        try
        {
            if (ViewState["action"] != null && Session["studentID"] != null)
            {

                objStudent.sportsType = rdoType.SelectedValue;
                objStudent.sportsName = txtNameOfGameOrAchievement.Text;
                objStudent.sportsVenue = txtVenue.Text;
                objStudent.sportsDate = Convert.ToDateTime(txtDate.Text);
                objStudent.participationNo = Convert.ToInt32(ddlLevelOfParticipation.SelectedValue);
                objStudent.achievementNo = Convert.ToInt32(ddlAchievement.SelectedValue);
                objStudent.idno = Convert.ToInt32(Session["studentID"]);

                if (ViewState["action"].ToString().Equals("edit"))
                {
                    objStudent.sportsNo = sportsno;


                    //to upload the course material
                    uploadSportsCertificate();

                    if (fuSportsCertificate.HasFile)
                    {
                        if (status == 1)
                        {
                            CustomStatus cs = (CustomStatus)objSC.UpdateSportsDetails(objStudent);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                // objCommon.DisplayMessage(this.updSports, "Sports Details Modified Successfully!!", this.Page);
                                ViewState["action"] = "add";
                                ClearControls();
                                BindSportsDetails();

                                ScriptManager.RegisterStartupScript(this.updSports, this.GetType(),
                                     "alert",
                                 "alert('Sports Details Modified Successfully!!');window.location ='StudentInfo.aspx#step-9';",
                                 true);
                            }
                            else
                                objCommon.DisplayMessage(this.updSports, "Error!!", this.Page);

                        }
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objSC.UpdateSportsDetails(objStudent);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            // objCommon.DisplayMessage(this.updSports, "Sports Details Modified Successfully!!", this.Page);
                            ViewState["action"] = "add";
                            ClearControls();
                            BindSportsDetails();

                            ScriptManager.RegisterStartupScript(this.updSports, this.GetType(),
                             "alert",
                             "alert('Sports Details Modified Successfully!!');window.location ='StudentInfo.aspx#step-9';",
                                                                    true);
                        }
                        else
                            objCommon.DisplayMessage(this.updSports, "Error!!", this.Page);

                    }

                }
                else
                {
                    //to upload the course material
                    uploadSportsCertificate();

                    if (fuSportsCertificate.HasFile)
                    {
                        if (status == 1)
                        {
                            CustomStatus cs = (CustomStatus)objSC.AddSportsDetails(objStudent);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                // objCommon.DisplayMessage(this.updSports, "Sports Details Added Successfully!!", this.Page);
                                ViewState["action"] = "add";
                                // FillDropDownCourse();
                                ClearControls();
                                BindSportsDetails();

                                ScriptManager.RegisterStartupScript(this.updSports, this.GetType(),
                                   "alert",
                                   "alert('Sports Details Added Successfully!!');window.location ='StudentInfo.aspx#step-9';",
                                                                          true);

                            }
                            else
                                objCommon.DisplayMessage(this.updSports, "Error!!", this.Page);


                        }
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objSC.AddSportsDetails(objStudent);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            // objCommon.DisplayMessage(this.updSports, "Sports Details Added Successfully!!", this.Page);
                            ViewState["action"] = "add";
                            // FillDropDownCourse();
                            ClearControls();
                            BindSportsDetails();
                            ScriptManager.RegisterStartupScript(this.updSports, this.GetType(),
                                   "alert",
                                   "alert('Sports Details Added Successfully!!');window.location ='StudentInfo.aspx#step-9';",
                                                                          true);

                        }
                        else
                            objCommon.DisplayMessage(this.updSports, "Error!!", this.Page);


                    }

                }

                // Response.Redirect("StudentInfo.aspx#step-9");
                //string url = "StudentInfo.aspx#step-9";
                //Response.Redirect(url, false);
            }

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_courseMaster.btnSaveNext10_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    //USED FOR SET SPORT DETAILS 
    public void GetSportsDetails()
    {
        if (rdoType.SelectedValue == "1")
        {
            divSportsDetails.Visible = true;

            lblNameOf.Text = "Name Of The Game :";
            lblDate.Text = "Match Date :";
            divSportsBtnDetails.Visible = true;
            lvSportsDetails.Visible = true;
        }
        else
        {

            divSportsDetails.Visible = false;

            lblNameOf.Text = "Name Of The Game :";
            lblDate.Text = "Match Date :";
            divSportsBtnDetails.Visible = false;
            lvSportsDetails.Visible = false;
            //divSportsDetails.Visible = true;
            //lblNameOf.Text = "Name Of The Achievement :";
            //lblDate.Text = "Achievement Date :";
            //divSportsBtnDetails.Visible = true;
        }
    }

    protected void rdoType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSportsDetails();
    }

    #endregion

    //USED FOR EDIT THE SPORTS DETAILS
    protected void btnSportsEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            sportsno = int.Parse(btnEdit.CommandArgument);
            ShowDetails(sportsno);
            ViewState["action"] = "edit";
            txtNameOfGameOrAchievement.Focus();
            //  Response.Redirect("StudentInfo.aspx#step-9");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_StudentInfo.btnSportsEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //51200 for 50 kb
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        if (Session["studentID"] != null)
        {
            string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).CommandArgument.ToString();
            string ContentType = string.Empty;

            //To Get the physical Path of the file(test.txt)

            string filepath = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + Session["studentID"].ToString() + "-" + txtNameOfGameOrAchievement.Text + "\\";
            // string filepath = WebConfigurationManager.AppSettings["SVCE_SUBJECT_DOC"].ToString()+ "\\";

            // Create New instance of FileInfo class to get the properties of the file being downloaded
            FileInfo myfile = new FileInfo(filepath + filename);
            string file = filepath + filename;

            string ext = Path.GetExtension(filename);
            // Checking if file exists
            if (myfile.Exists)
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.TransmitFile(file);
                Response.Flush();
                Response.End();
            }
        }

    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENAME");
        }
        else
        {
            ddlDegree.Items.Clear();
            ddlDegree.Items.Insert(0, "--Please Select--");
        }
    }

    protected void ddlSelectParent_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSelectParent.SelectedValue == "1")
        {
            divFather1.Visible = true;
            divFather2.Visible = true;
            divMother1.Visible = false;
            divMother2.Visible = false;
            lblFather2.Visible = true;
        }
        else if (ddlSelectParent.SelectedValue == "2")
        {
            divFather1.Visible = false;
            divFather2.Visible = false;
            divMother1.Visible = true;
            divMother2.Visible = true;
            lblFather2.Visible = false;
        }
        else
        {
            divFather1.Visible = true;
            divFather2.Visible = true;
            divMother1.Visible = true;
            divMother2.Visible = true;
            lblFather2.Visible = true;
        }
    }

    protected void ddlSSCLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSSCLanguage.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSSCLanguage2, "ACD_MTONGUE", "MTONGUENO", "UPPER(MTONGUE) MTONGUE", "MTONGUENO>0 AND MTONGUENO!=" + Convert.ToInt32(ddlSSCLanguage.SelectedValue), "MTONGUENO");
        }
        else
        {
            objCommon.FillDropDownList(ddlSSCLanguage2, "ACD_MTONGUE", "MTONGUENO", "UPPER(MTONGUE) MTONGUE", "MTONGUENO>0", "MTONGUENO");
        }
    }

    protected void rbEntrance_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbEntrance.SelectedValue == "1")
        {
            //   divEntrance.Visible = true;
            divEntrance.Style["visibility"] = "visible";
            //    divEntrance.Style["display"] = "none"; 
        }
        else
        {
            //  divEntrance.Visible = false;
            // divEntrance.Attributes.Add("class", "visibility:hidden");
            divEntrance.Style["visibility"] = "hidden";
        }
    }

    protected void ddlHSCBoardCategory_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlHSCBoardCategory.SelectedIndex > 0)
        {
            if (ddlHSCBoardCategory.SelectedValue == "7" || ddlHSCBoardCategory.SelectedValue == "9" || ddlHSCBoardCategory.SelectedValue == "0")
            {
                clearHSCNonVocational();
                ddlHSCOptionalSub.SelectedIndex = 0;
                divNonVocational.Visible = false;
                divVocational.Visible = true;
            }
            else
            {
                clearHSCVocational();
                divVocational.Visible = false;
                divNonVocational.Visible = true;
            }

            if (ddlHSCBoardCategory.SelectedValue == "1")
            {
                txtOtherBoardHSC.Enabled = true;
            }
            else
            {
                txtOtherBoardHSC.Enabled = false;
            }
        }
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlRemarkType.SelectedIndex = 0;
        txtRemark.Text = string.Empty;
        if (ddlYear.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlRemarkType, "ACD_REMARK_MASTER", "REMARK_ID", "REMARK_TYPE", "REMARK_ID>0", "REMARK_ID");
        }
        else
        {
            ddlRemarkType.Items.Clear();
            ddlRemarkType.Items.Insert(0, "Please Select");
        }
    }

    protected void btnSaveNextRemark_Click(object sender, EventArgs e)
    {
        if (txtRemark.Text != string.Empty)
        {
            if (ddlYear.SelectedIndex == 0 && ddlRemarkType.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this, "Please Select Year! \\n Please Select Remark Type!", this);
                return;
            }
            else if (ddlRemarkType.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this, "Please Select Remark Type!", this);
                return;
            }

            int studid = Session["idno"] == null || Session["idno"].ToString() == "0" ? Convert.ToInt32(Session["studentID"].ToString()) : Convert.ToInt32(Session["idno"].ToString());

            CustomStatus cs = (CustomStatus)objStudRegC.AddUpdateStudRemark(studid, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlRemarkType.SelectedValue), txtRemark.Text);
            if (Convert.ToInt32(cs) == 1 || Convert.ToInt32(cs) == 2)
            {
                if (Session["usertype"] != "2")
                {
                    ViewState["action"] = "add";
                    ClearControls();
                    Session["stuinfoidno"] = "-1";
                    ShowReport("StudentRegistration", "UserInfo.rpt");

                    ScriptManager.RegisterStartupScript(this.UpdatePanel9, this.GetType(),
                           "alert",
                           "alert('Remark Added Successfully!!');window.location ='StudentInfo.aspx#step-1';",
                                                                  true);
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
            }

        }
        else
        {
            ViewState["action"] = "add";
            ClearControls();
            Session["stuinfoidno"] = "-1";
            ShowReport("StudentRegistration", "UserInfo.rpt");

            ScriptManager.RegisterStartupScript(this.UpdatePanel9, this.GetType(),
                   "alert",
                   "alert('Student Information Added Successfully!!');window.location ='StudentInfo.aspx#step-1';",
                                                          true);
        }
    }

    protected void ddlRemarkType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int studid = Session["idno"] == null || Session["idno"].ToString() == "0" ? Convert.ToInt32(Session["studentID"].ToString()) : Convert.ToInt32(Session["idno"].ToString());
        if (ddlRemarkType.SelectedIndex > 0)
        {
            txtRemark.Text = objCommon.LookUp("ACD_STUDENT_REMARK", "REMARK", "IDNO=" + studid + " AND YEAR=" + ddlYear.SelectedValue + " AND REMARK_TYPE=" + ddlRemarkType.SelectedValue) == string.Empty ? "" : objCommon.LookUp("ACD_STUDENT_REMARK", "REMARK", "IDNO=" + studid + " AND YEAR=" + ddlYear.SelectedValue + " AND REMARK_TYPE=" + ddlRemarkType.SelectedValue);
        }
    }

}