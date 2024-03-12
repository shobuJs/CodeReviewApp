//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Comprehensive Student Report
// CREATION DATE : 
// CREATED BY    : AMIT YADAV
// MODIFIED BY   : ASHISH DHAKATE
// MODIFIED DATE : 14/02/2012
// MODIFIED DESC : 
//======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
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
using DynamicAL_v2;



public partial class ACADEMIC_Comprehensive_Stud_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    DynamicControllerAL AL = new DynamicControllerAL();
    AcdAttendanceController objAttC = new AcdAttendanceController();

    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            imgPhoto.ImageUrl = "~/images/nophoto.jpg";
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["currentsession"] == null)
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
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]) + " and ua_no=" + Convert.ToInt32(Session["userno"]));
                ViewState["usertype"] = ua_type;

                if (ViewState["usertype"].ToString() == "2" || ViewState["usertype"].ToString() == "18")
                {
                    pnlSearch.Visible = false;
                    ShowDetails();
                }

                else if (ViewState["usertype"].ToString() == "3")
                {
                    pnlSearch.Visible = true;
                }
                else
                {
                    pnlSearch.Visible = true;
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
                }

                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                {
                    txtEnrollmentSearch.Text = string.Empty;
                    lvRegStatus.DataSource = null;
                    lvRegStatus.DataBind();
                    lblsession.Text = string.Empty;
                    lblsmester.Text = string.Empty;
                    divinfo.Visible = false;
                    lvFees.DataSource = null;
                    lvFees.DataBind();
                    lvCertificate.DataSource = null;
                    lvCertificate.DataBind();
                    lblMsg.Text = string.Empty;
                }
            }
        }
        divMsg.InnerHtml = string.Empty;

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (rdoSearchAll.Checked == true)
        {
            StudentController studCont = new StudentController();
            string searchText = txtEnrollmentSearch.Text.Trim();
            string searchBy = "name";
            DataSet ds1 = studCont.RetrieveStudentDetailsForStudentDetails(searchText, searchBy, Session["college_nos"].ToString()); //ADDED BY NARESH BEERLA ON 28012021 AS PER Session["college_nos"]
            DataSet ds = new DataSet();

            if (ViewState["usertype"].ToString() == "18")
            {
                var where = "UA_IDNO=" + Session["idno"].ToString();
                var order = "NAME";

                ds.Merge(ds1.Tables[0].Select(where, order));
            }
            else if (ViewState["usertype"].ToString() == "3")
            {
                var where = "UA_DEPTNO=" + Session["userdeptno"].ToString();
                var order = "NAME";

                ds.Merge(ds1.Tables[0].Select(where, order));
            }
            else
            {
                ds.Merge(ds1.Tables[0].Select("", ""));
            }


            if (ds != null && ds.Tables.Count > 0)
            {
                div1.Visible = true;
                div2.Visible = false;
                div4.Visible = false;
                div5.Visible = false;
                div6.Visible = false;
                div7.Visible = false;
                div8.Visible = false;
                div9.Visible = false;
                div10.Visible = false;
                div11.Visible = false;
                div12.Visible = false;
                div13.Visible = false;
                div14.Visible = false;
                lvStudentRecords.DataSource = ds.Tables[0];
                lvStudentRecords.DataBind();
            }
            else
            {
                objCommon.DisplayMessage("No student found having Name : " + txtEnrollmentSearch.Text.Trim(), this.Page);
                return;
            }
        }
        else
        {
            ShowDetails();
        }
    }

    private void ShowDetails()
    {
        Clear();
        int idno = 0;
        StudentController objSC = new StudentController();
        DataSet dsregistration, dsResult, dsFees, dsCertificate, dsRemark, dsRefunds, dsTestMarks, dsAttendance;
        FeeCollectionController feeController = new FeeCollectionController();
        if (ViewState["usertype"].ToString() == "2" || ViewState["usertype"].ToString() == "18")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "3")
        {
            string no = "", idn = "";
            no = objCommon.LookUp("ACD_STUDENT", "ISNULL(IDNO,0)", "REGNO = '" + txtEnrollmentSearch.Text.Trim() + "' OR ENROLLNO = '" + txtEnrollmentSearch.Text.Trim() + "'");

            if (no != "")
            {
                idn = objCommon.LookUp("acd_student", "IDNO", "fac_advisor=" + Convert.ToInt32(Session["userno"]) + " and idno=" + no);
                if (idn == "")
                {
                    objCommon.DisplayMessage("No student found having Univ. Reg. No./ TAN/PAN : " + txtEnrollmentSearch.Text.Trim(), this.Page);
                    return;
                }
                else
                    idno = Convert.ToInt32(idn);
            }
            else
            {
                objCommon.DisplayMessage("No student found having Univ. Reg. No./ TAN/PAN: " + txtEnrollmentSearch.Text.Trim(), this.Page);
                return;
            }
        }
        else
        {
            idno = feeController.GetStudentIdByEnrollmentNoForStudentDeatils(txtEnrollmentSearch.Text.Trim(), Session["college_nos"].ToString()); //ADDED BY NARESH BEERLA ON 28012021 AS PER Session["college_nos"]
        }

        try
        {
            if (idno > 0)
            {
                DataTableReader dtr = objSC.GetStudentDetails(idno);

                //on faculty login to get only those dept which is related to logged in faculty
                DataSet dsBranches = new DataSet();
                dsBranches = objCommon.FillDropDown("ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
                string BranchNos = "";
                for (int i = 0; i < dsBranches.Tables[0].Rows.Count; i++)
                {
                    BranchNos += dsBranches.Tables[0].Rows[i]["BranchNo"].ToString() + ",";
                }
                DataSet dsReff = objCommon.FillDropDown("REFF", "*", "", string.Empty, string.Empty);
                dtr = objCommon.FilterDataByBranch(ref dtr, (dsReff.Tables[0].Rows[0]["FILTER_USER_TYPE"].ToString()), BranchNos, Convert.ToInt32(dsReff.Tables[0].Rows[0]["BRANCH_FILTER"].ToString()), Convert.ToInt32(Session["usertype"]));

                if (dtr.HasRows == false)
                {
                    objCommon.DisplayMessage("You can search students of your branch only", this.Page);
                    return;
                }
                if (dtr != null && (dtr.HasRows))
                {
                    while (dtr.Read())
                    {
                        string yearwise = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO)", "CASE WHEN ISNULL(D.YEARWISE,0)=1 THEN 'Year : ' ELSE 'Semester : ' END", "IDNO=" + idno) == string.Empty ? "" : objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO)", "CASE WHEN ISNULL(D.YEARWISE,0)=1 THEN 'Year : ' ELSE 'Semester : ' END", "IDNO=" + idno);
                        div2.Visible = true;
                        lblRegNo.Text = dtr["REGNO"].ToString();
                        ViewState["admbatch"] = dtr["ADMBATCH"];
                        string degreeno = dtr["DEGREENO"] == null ? "0" : dtr["DEGREENO"].ToString();
                        string branchno = dtr["BRANCHNO"] == null ? "0" : dtr["BRANCHNO"].ToString();

                        DataSet ds = objCommon.FillDropDown("ACD_BRANCH A ,ACD_DEGREE B", "LONGNAME", "DEGREENAME", "(A.BRANCHNO=" + branchno + " OR " + branchno + "=0) AND (B.DEGREENO=" + degreeno + " OR " + degreeno + "=0)", "");
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            lblBranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                            lblDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                        }
                        else
                        {
                            lblBranch.Text = string.Empty;
                            lblDegree.Text = string.Empty;
                        }

                        lblName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                        lblMName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString();
                        lblMotherName.Text = dtr["MOTHERNAME"] == null ? string.Empty : dtr["MOTHERNAME"].ToString();
                        lblDOB.Text = dtr["DOB"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["DOB"]).ToString("dd/MM/yyyy");
                        string caste = objCommon.LookUp("ACD_CASTE", "CASTE", "CASTENO=" + dtr["CASTE"].ToString());
                        lblCaste.Text = caste;
                        string BloodGroup = objCommon.LookUp("ACD_BLOODGRP", "BLOODGRPNAME", "BLOODGRPNO=" + dtr["BLOODGRPNO"].ToString());
                        lblBlood.Text = BloodGroup;
                        string category = objCommon.LookUp("ACD_CATEGORY", "CATEGORY", "CATEGORYNO=" + dtr["CATEGORYNO"].ToString());
                        lblCategory.Text = category;
                        string religion = objCommon.LookUp("ACD_RELIGION", "RELIGION", "RELIGIONNO=" + dtr["RELIGIONNO"].ToString());
                        lblReligion.Text = religion;
                        string nation = objCommon.LookUp("ACD_NATIONALITY", "NATIONALITY", "NATIONALITYNO=" + dtr["NATIONALITYNO"].ToString());
                        lblNationality.Text = nation;
                        lblLAdd.Text = dtr["LADDRESS"] == null ? string.Empty : dtr["LADDRESS"].ToString();

                        if (dtr["LCITY"].ToString() == "")
                        {
                            lblCity.Text = "";
                        }
                        else
                        {
                            string lcity = objCommon.LookUp("ACD_CITY", "CITY", "CITYNO=" + dtr["LCITY"].ToString());
                            lblCity.Text = lcity;
                        }

                        //lblLLNo.Text = dtr["LTELEPHONE"] == null ? string.Empty : dtr["LTELEPHONE"].ToString();
                        lblMobNo.Text = dtr["STUDENTMOBILE"] == null ? string.Empty : dtr["STUDENTMOBILE"].ToString();
                        lblPAdd.Text = dtr["PADDRESS"] == null ? string.Empty : dtr["PADDRESS"].ToString();
                        lblEnrollNo.Text = dtr["ENROLLNO"] == null ? string.Empty : dtr["ENROLLNO"].ToString();
                        // string semester = objCommon.LookUp("ACD_SEMESTER", "SEMESTERNAME", "SEMESTERNO=" + dtr["SEMESTERNO"].ToString());

                        if (dtr["YEARWISE"].ToString() == "1")
                        {
                            lblSemester.Text = dtr["YEARNAME"].ToString();
                        }
                        else
                        {
                            lblSemester.Text = dtr["SEMESTERNAME"].ToString();
                        }

                        //string semester = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_YEAR Y ON (Y.YEAR=S.YEAR) INNER JOIN ACD_SEMESTER SEM ON (SEM.SEMESTERNO=S.SEMESTERNO)", "CASE WHEN ISNULL(D.YEARWISE,0)=1 THEN CAST(Y.YEARNAME AS NVARCHAR(100)) ELSE CAST(SEM.SEMESTERNAME AS NVARCHAR(16)) END", "IDNO="+idno);
                        //lblSemester.Text = semester;

                        if (dtr["PCITY"].ToString() == "")
                        {
                            lblPCity.Text = "";
                        }
                        else
                        {
                            string pcity = objCommon.LookUp("ACD_CITY", "CITY", "CITYNO=" + dtr["PCITY"].ToString());
                            lblPCity.Text = pcity;
                        }

                        lblAadharNo.Text = dtr["ADDHARCARDNO"] == null ? string.Empty : dtr["ADDHARCARDNO"].ToString();
                        lblMailID.Text = dtr["EMAILID"] == null ? string.Empty : dtr["EMAILID"].ToString();


                        //string PhtoSign = objCommon.LookUp("ACD_STUD_PHOTO", "ISNULL(PHOTO,'-')+'$'+ISNULL(STUD_SIGN,'-')", "IDNO = " + idno + "");
                        //DataTable dtBlobPic = AL.Blob_GetById(blob_ConStr, blob_ContainerName, PhtoSign.Split('$')[0]);
                        //if (dtBlobPic.Rows.Count != 0)
                        //{
                        //    imgPhoto.ImageUrl = Convert.ToString(dtBlobPic.Rows[0]["uri"]);
                        //}

                        //DataTable dtBlobSign = AL.Blob_GetById(blob_ConStr, blob_ContainerName, PhtoSign.Split('$')[1]);
                        //if (dtBlobSign.Rows.Count != 0)
                        //{
                        //    imgSign.ImageUrl = Convert.ToString(dtBlobSign.Rows[0]["uri"]);
                        //}

                        if (dtr["PHOTO"].ToString() != "")
                        {
                            imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dtr["IDNO"].ToString() + "&type=student";
                        }

                        if (dtr["STUD_SIGN"].ToString() != "")
                        {
                            imgSign.ImageUrl = "~/showimage.aspx?id=" + dtr["IDNO"].ToString() + "&type=studentsign";
                        }


                        //Students Current Registration Details

                        dsregistration = objSC.RetrieveStudentCurrentRegDetails(idno);
                        if (dsregistration.Tables[0].Rows.Count > 0)
                        {
                            div4.Visible = true;
                            divinfo.Visible = true;
                            lblsession.Text = dsregistration.Tables[0].Rows[0]["SESSION"].ToString();
                            lblRegSemYear.Text = yearwise.ToString();

                            if (dtr["YEARWISE"].ToString() == "1")
                            {
                                lblsmester.Text = dsregistration.Tables[0].Rows[0]["YEARNAME"].ToString();
                            }
                            else
                            {
                                lblsmester.Text = dsregistration.Tables[0].Rows[0]["SEMESTER"].ToString();
                            }

                            // lblsmester.Text = dsregistration.Tables[0].Rows[0]["SEMESTER"].ToString();
                            lvRegStatus.DataSource = dsregistration;
                            lvRegStatus.DataBind();
                        }
                        else
                        {
                            lblsession.Text = string.Empty;
                            lblsmester.Text = string.Empty;
                            divinfo.Visible = false;
                            lvRegStatus.DataSource = null;
                            lvRegStatus.DataBind();
                            div4.Visible = false;
                        }

                        //End of Students Current Registration Details


                        // Students class Test Details



                        dsTestMarks = objSC.RetrieveStudentClassTestMarks(idno);

                        if (dsTestMarks.Tables[0].Rows.Count > 0)
                        {
                            lblSemYear.Text = yearwise.ToString();
                            divtestmarkinfo.Visible = true;
                            lbltestsession.Text = dsTestMarks.Tables[0].Rows[0]["SESSION"].ToString();

                            if (dtr["YEARWISE"].ToString() == "1")
                            {
                                lbltestsemester.Text = dsTestMarks.Tables[0].Rows[0]["YEARNAME"].ToString();
                            }
                            else
                            {
                                lbltestsemester.Text = dsTestMarks.Tables[0].Rows[0]["YEARSEM"].ToString();
                            }

                            //  lbltestsemester.Text = dsTestMarks.Tables[0].Rows[0]["YEARSEM"].ToString();


                            DataTable dt = dsTestMarks.Tables[0];

                            if (dt.Columns.Contains("SESSION"))
                            {
                                dt.Columns.Remove("SESSION");
                            }
                            if (dt.Columns.Contains("SEMESTER"))
                            {
                                dt.Columns.Remove("SEMESTER");
                            }

                            gvTestMark.DataSource = dt;
                            gvTestMark.DataBind();
                        }
                        else
                        {
                            divtestmarkinfo.Visible = false;
                            lbltestsession.Text = string.Empty;
                            lbltestsemester.Text = string.Empty;
                            gvTestMark.DataSource = null;
                            gvTestMark.DataBind();
                            divtestmarkinfo.Visible = false;
                        }

                        //End of Students class Test Details


                        //Students Fees Details

                        dsFees = objSC.RetrieveStudentFeesDetails(idno);
                        if (dsFees.Tables[0].Rows.Count > 0)
                        {
                            lvFees.DataSource = dsFees;
                            lvFees.DataBind();
                        }
                        else
                        {
                            lvFees.DataSource = new DataTable();
                            lvFees.DataBind();
                        }

                        //End of Students Fees Details


                        //Students Attendance Details

                        string semesterno = dtr["SEMESTERNO"].ToString();
                        string schemeno = dtr["SCHEMENO"].ToString();
                        string sessionno = Session["currentsession"].ToString();

                        dsAttendance = objSC.RetrieveStudentAttendanceDetails(Convert.ToInt32(sessionno), Convert.ToInt32(schemeno), Convert.ToInt32(semesterno), idno);

                        if (dsAttendance.Tables[0].Rows.Count > 0)
                        {
                            div5.Visible = true;
                            divattinfo.Visible = true;
                            lblAttSemYear.Text = yearwise.ToString();
                            lblattsession.Text = dsAttendance.Tables[0].Rows[0]["SESSIONNAME"].ToString();
                            lblattsession.ToolTip = dsAttendance.Tables[0].Rows[0]["SESSIONNO"].ToString();

                            if (dtr["YEARWISE"].ToString() == "1")
                            {
                                lblattsemester.Text = dsAttendance.Tables[0].Rows[0]["YEARNAME"].ToString();
                            }
                            else
                            {
                                lblattsemester.Text = dsAttendance.Tables[0].Rows[0]["SEMESTER"].ToString();
                            }

                            // lblattsemester.Text = dsAttendance.Tables[0].Rows[0]["SEMESTER"].ToString();

                            lblattsemester.ToolTip = dsAttendance.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                            lblattScheme.ToolTip = dsAttendance.Tables[0].Rows[0]["SCHEMENO"].ToString();
                            lvAttendance.DataSource = dsAttendance;
                            lvAttendance.DataBind();
                        }
                        else
                        {
                            divattinfo.Visible = false;
                            lblattsession.Text = string.Empty;
                            lblattsemester.Text = string.Empty;
                            lblattScheme.ToolTip = string.Empty;
                            lblattsession.ToolTip = string.Empty;
                            lblattsemester.ToolTip = string.Empty;
                            lvAttendance.DataSource = null;
                            lvAttendance.DataBind();
                        }

                        // End of Students Attendance Details


                        //Student Result Details

                        //dsResult = objSC.RetrieveStudentCurrentResult(idno);
                        //if (dsResult.Tables[0].Rows.Count > 0)
                        //{
                        //    lvResult.DataSource = dsResult;
                        //    lvResult.DataBind();
                        //}
                        //else
                        //{
                        //    lvResult.DataSource = null;
                        //    lvResult.DataBind();
                        //}

                        //End of Student Result Details

                        //Student Result Details New

                        try
                        {
                            //string sscmark = objCommon.LookUp("ACD_STU_LAST_QUALEXM", "MARKS_OBTAINED", "IDNO=" + idno + " AND QUALIFYNO=1");
                            //string Intermark = objCommon.LookUp("ACD_STU_LAST_QUALEXM", "MARKS_OBTAINED", "IDNO=" + idno + " AND QUALIFYNO=2");
                            //DataSet dsSemester = objCommon.FillDropDown("ACD_STUDENT_RESULT_HIST", "DISTINCT DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTER", "SEMESTERNO,IDNO", "IDNO=" + idno, "SEMESTERNO DESC");
                            //DataSet dsSemester = objSC.RetrieveStudentSemesterMark(idno, Convert.ToInt32(ViewState["SEMESTERNO"]));
                            DataSet dsSemester = objSC.RetrieveStudentSemesterNumberResult(idno);
                            if (dsSemester != null && dsSemester.Tables.Count > 0 && dsSemester.Tables[0].Rows.Count > 0)
                            {

                                gvParentGrid.DataSource = dsSemester;
                                gvParentGrid.DataBind();
                                gvParentGrid.Visible = true;

                            }
                            else
                            {
                                //pnlCollege.Visible = false;
                                //lvSemester.DataSource = null;
                                //lvSemester.DataBind();
                                gvParentGrid.DataSource = null;
                                gvParentGrid.DataBind();
                                //gvParentGrid.Visible = false;
                            }
                        }
                        catch
                        {
                        }

                        //End of Student Result Details New


                        //Students Certificate issued Details

                        dsCertificate = objSC.RetrieveStudentCertificateDetails(idno);
                        if (dsCertificate.Tables[0].Rows.Count > 0)
                        {
                            lvCertificate.DataSource = dsCertificate;
                            lvCertificate.DataBind();
                        }
                        else
                        {
                            lvCertificate.DataSource = null;
                            lvCertificate.DataBind();
                        }

                        //  End of Students Certificate issued Details



                        //Remark

                        dsRemark = objSC.GetAllRemarkDetails(idno);
                        if (dsRemark != null && dsRemark.Tables.Count > 0 && dsRemark.Tables[0].Rows.Count > 0)
                        {
                            lvRemark.DataSource = dsRemark;
                            lvRemark.DataBind();
                        }
                        else
                        {
                            lvRemark.DataSource = null;
                            lvRemark.DataBind();
                        }

                        // End of Remark


                        //Refund details

                        dsRefunds = objSC.GetStudentRefunds(idno);
                        if (dsRefunds != null && dsRefunds.Tables.Count > 0 && dsRefunds.Tables[0].Rows.Count > 0)
                        {
                            lvRefund.DataSource = dsRefunds;
                            lvRefund.DataBind();
                        }
                        else
                        {
                            lvRefund.DataSource = null;
                            lvRefund.DataBind();
                        }



                        DataSet ds1 = null;
                        ds1 = objSC.GetAllSportsDetails(Convert.ToInt32(idno));
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            lvSports.DataSource = ds1.Tables[0];
                            lvSports.DataBind();
                        }
                        else
                        {
                            lvSports.DataSource = null;
                            lvSports.DataBind();
                        }

                        DataSet dsScollerShip = new DataSet();
                        dsScollerShip = objCommon.FillDropDown("ACD_STUDENT_SCHOLARSHIP_DETAILS", "SCHOLARSHIP_NAME", "ORGANIZATION_NAME,CONVERT(varchar, SANCTIONED_DATE, 101) as SANCTIONED_DATE,AMT_SANCTIONED", "IDNO=" + idno + "", "SCHOLARSHIP_NO");
                        if (dsScollerShip.Tables[0].Rows.Count > 0)
                        {
                            lvScholarship.DataSource = dsScollerShip.Tables[0];
                            lvScholarship.DataBind();
                        }
                        else
                        {
                            lvScholarship.DataSource = null;
                            lvScholarship.DataBind();
                        }

                        DataSet dsDisciplinary = new DataSet();
                        dsDisciplinary = objCommon.FillDropDown("ACD_STUDENT_ACTION A INNER JOIN ACD_STUDENT_ACTION_IDNO B ON (A.EVENTID = B.EVENTID) LEFT JOIN ACD_EVENTCATEOGRY EC ON (EC.EVENTCATENO=A.EVENTCATENO)", "CONVERT(varchar, A.EVENTDATE, 101) as EVENTDATE", "A.ACTIONTAKEN,A.EVENTDETAIL,EC.EVENTCATENAME", "B.IDNO=" + idno + "", "A.EVENTCATENO");
                        if (dsDisciplinary.Tables[0].Rows.Count > 0)
                        {
                            lvDisciplinary.DataSource = dsDisciplinary.Tables[0];
                            lvDisciplinary.DataBind();
                        }
                        else
                        {
                            lvDisciplinary.DataSource = null;
                            lvDisciplinary.DataBind();
                        }


                        DataSet dsStudRemark = new DataSet();
                        dsStudRemark = objCommon.FillDropDown("ACD_STUDENT_REMARK SR INNER JOIN ACD_REMARK_MASTER RM ON (RM.REMARK_ID=SR.REMARK_TYPE) INNER JOIN ACD_YEAR Y ON (Y.YEAR=SR.YEAR)", "SR.IDNO, SR.YEAR", "RM.REMARK_TYPE, SR.REMARK, YEARNAME, RM.REMARK_ID", "SR.IDNO=" + idno + "", "SR.YEAR, RM.REMARK_ID");
                        if (dsStudRemark.Tables[0].Rows.Count > 0)
                        {
                            lvStudRemark.DataSource = dsStudRemark.Tables[0];
                            lvStudRemark.DataBind();
                        }
                        else
                        {
                            lvStudRemark.DataSource = null;
                            lvStudRemark.DataBind();
                        }


                        //==================== Bind Student Memo =====================//
                        DataSet dsStudMemo = new DataSet();
                        dsStudMemo = objAttC.GetAllStudentMemoDetails(Convert.ToInt32(idno));
                        if (dsStudMemo.Tables[0].Rows.Count > 0)
                        {
                            lvStudentMemo.DataSource = dsStudMemo.Tables[0];
                            lvStudentMemo.DataBind();
                            fuDocUpload.Enabled = true;
                            btnUploadMemo.Enabled = true;
                            if (dsStudMemo.Tables[0].Rows[0]["UPLOAD_STATUS"].ToString()=="1")
                            {
                                fuDocUpload.Enabled = false;
                                btnUploadMemo.Enabled = false;
                            }
                        }
                        else
                        {
                            lvStudentMemo.DataSource = null;
                            lvStudentMemo.DataBind();
                            fuDocUpload.Enabled = false;
                            btnUploadMemo.Enabled = false;
                        }
                        //==================== End Student Memo =====================//

                        // End of Refund Details
                    }
                    dtr.Close();
                }

            }
            else
            {
                // objCommon.DisplayMessage(this.updStudent,"No student found having USN no.: " + txtEnrollmentSearch.Text.Trim(), this.Page);
                objCommon.DisplayMessage("No student found having Univ. Reg. No./ TAN/PAN : " + txtEnrollmentSearch.Text.Trim(), this.Page);
                lblEnrollNo.Text = string.Empty;
                lblSemester.Text = string.Empty;

                txtEnrollmentSearch.Focus();
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Comprehensive_Stud_Report.btnSearch_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lbReport_Click(object sender, EventArgs e)
    {
        //////Show Tabulation Sheet
        LinkButton btn = sender as LinkButton;
        string sessionNo = (btn.Parent.FindControl("hdfSession") as HiddenField).Value;
        string semesterNo = (btn.Parent.FindControl("hdfSemester") as HiddenField).Value;
        string schemeNo = (btn.Parent.FindControl("hdfScheme") as HiddenField).Value;
        string IdNo = (btn.Parent.FindControl("hdfIDNo") as HiddenField).Value;

        this.ShowTRReport("Tabulation_Sheet", "rptTabulationRegistar.rpt", sessionNo, schemeNo, semesterNo, IdNo);
    }

    private void ShowTRReport(string reportTitle, string rptFileName, string sessionNo, string schemeNo, string semesterNo, string idNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + sessionNo + ",@P_SCHEMENO=" + schemeNo + ",@P_SEMESTERNO=" + semesterNo + ",@P_IDNO=" + idNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void gvParentGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            StudentController objSC = new StudentController();
            int admbacth = Convert.ToInt32(ViewState["admbatch"]);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = (GridView)e.Row.FindControl("gvChildGrid");
                // GridView gv1 = (GridView)e.Row.FindControl("gvChildGrid1");

                HiddenField idno = e.Row.FindControl("hdfIDNo") as HiddenField;
                HiddenField SemesterNo = e.Row.FindControl("hdfSemester") as HiddenField;
                HiddenField SessionNo = e.Row.FindControl("hdfSession") as HiddenField;
                //  HtmlControl htmlDivControl = (HtmlControl)Page.FindControl("aayushi");
                HtmlGenericControl div = e.Row.FindControl("divc1") as HtmlGenericControl;
                //    HtmlGenericControl div1 = e.Row.FindControl("divc2") as HtmlGenericControl;
                try
                {
                    DataSet ds = objSC.RetrieveStudentCurrentResultFORGRADE(Convert.ToInt32(idno.Value), Convert.ToInt32(SemesterNo.Value), Convert.ToInt32(SessionNo.Value));
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        //if (admbacth <= 4)
                        //{
                        gv.DataSource = ds;
                        gv.DataBind();
                        gv.Visible = true;
                        // div1.Visible = false;
                        div.Visible = true;

                        //}
                        //else
                        //{
                        //    gv1.DataSource = ds;
                        //    gv1.DataBind();
                        //    gv1.Visible = true;
                        //   // div1.Visible = true;
                        //    div.Visible = false;
                        //}

                    }
                    else
                    {
                        gv.DataSource = null;
                        gv.DataBind();
                        gv.Visible = false;
                    }


                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUaimsCommon.ShowError(Page, "Academic_Reports_Comprehensive_Stud_Report.lvCollege_ItemDatabound() --> " + ex.Message + " " + ex.StackTrace);
                    else
                        objUaimsCommon.ShowError(Page, "Server Unavailable.");
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Comprehensive_Stud_Report.btnSearch_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        lblRegNo.Text = string.Empty;
        lblBranch.Text = string.Empty;
        lblName.Text = string.Empty;
        lblMName.Text = string.Empty;
        lblDOB.Text = string.Empty;
        lblCaste.Text = string.Empty;
        lblCategory.Text = string.Empty;
        lblReligion.Text = string.Empty;
        lblNationality.Text = string.Empty;
        lblLAdd.Text = string.Empty;
        // lblLLNo.Text = string.Empty;
        lblMobNo.Text = string.Empty;
        lblPAdd.Text = string.Empty;
        imgPhoto.ImageUrl = null;
        lvRegStatus.DataSource = null;
        lvRegStatus.DataBind();
        lblsession.Text = string.Empty;
        lblsmester.Text = string.Empty;
        divinfo.Visible = false;
        lvAttendance.DataSource = null;
        lvAttendance.DataBind();
        gvParentGrid.DataSource = null;
        gvParentGrid.DataBind();
        //lvResult.DataSource = null;
        //lvResult.DataBind();
        lvFees.DataSource = null;
        lvFees.DataBind();
        lvCertificate.DataSource = null;
        lvCertificate.DataBind();

        //Commented By Hemanth G 
        gvTestMark.DataSource = null;
        gvTestMark.DataBind();
        lvRemark.DataSource = null;
        lvRemark.DataBind();
        lvRefund.DataSource = null;
        lvRefund.DataBind();
        lblSemester.Text = string.Empty;

        lblDegree.Text = string.Empty;
        lblMotherName.Text = string.Empty;
        divtestmarkinfo.Visible = false;
        lbltestsession.Text = string.Empty;
        lbltestsemester.Text = string.Empty;
        lblEnrollNo.Text = string.Empty;
    }

    // Commented By Hemanth G
    protected void gvTestMark_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;

        }
    }

    // Commented By Hemanth G
    protected void gvTestMark_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid2 = (GridView)sender;
            GridViewRow HeaderGridRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);



            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = " ";
            HeaderGridRow2.Cells.Add(HeaderCell2);
            HeaderCell2.Attributes.Add("style", "text-align:center;color:white;");
            HeaderCell2.Attributes.Add("colspan", "1");
            gvTestMark.Controls[0].Controls.AddAt(0, HeaderGridRow2);

            //---------------------------------------------------
            TableCell HeaderCell3 = new TableCell();
            HeaderCell3.Text = "CAT-1";
            HeaderGridRow2.Cells.Add(HeaderCell3);
            HeaderCell3.Attributes.Add("style", "text-align:center;color:white;");
            HeaderCell3.Attributes.Add("colspan", "2");
            gvTestMark.Controls[0].Controls.AddAt(0, HeaderGridRow2);
            //------------------------------------------------------------
            TableCell HeaderCell4 = new TableCell();
            HeaderCell4.Text = "CAT-2";
            HeaderGridRow2.Cells.Add(HeaderCell4);
            HeaderCell4.Attributes.Add("style", "text-align:center;color:white;");
            HeaderCell4.Attributes.Add("colspan", "2");
            gvTestMark.Controls[0].Controls.AddAt(0, HeaderGridRow2);
            //------------------------------------------------------------
            TableCell HeaderCell5 = new TableCell();
            HeaderCell5.Text = "CAT-3";
            HeaderGridRow2.Cells.Add(HeaderCell5);
            HeaderCell5.Attributes.Add("style", "text-align:center;color:white;");
            HeaderCell5.Attributes.Add("colspan", "2");
            gvTestMark.Controls[0].Controls.AddAt(0, HeaderGridRow2);

            //------------------------------------------------------------
            TableCell HeaderCell6 = new TableCell();
            HeaderCell6.Text = " ";
            HeaderGridRow2.Cells.Add(HeaderCell6);
            HeaderCell6.Attributes.Add("style", "text-align:center;color:white;");
            HeaderCell6.Attributes.Add("colspan", "2");
            gvTestMark.Controls[0].Controls.AddAt(0, HeaderGridRow2);


            //------------------------------------------------------------
            //TableCell HeaderCell6 = new TableCell();
            //HeaderCell6.Text = " ";
            //HeaderGridRow2.Cells.Add(HeaderCell6);
            //HeaderCell6.Attributes.Add("style", "text-align:center;color:black;");
            //HeaderCell6.Attributes.Add("colspan", "1");
            //gvTestMark.Controls[0].Controls.AddAt(0, HeaderGridRow2);

            ////---------------------------------------------------------
            ////GridView HeaderGrid = (GridView)sender;
            ////GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            ////TableCell HeaderCell = new TableCell();
            ////HeaderCell.Text = " CAT Examination Marks Details";
            ////HeaderGridRow.Cells.Add(HeaderCell);
            //////  HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#f0f8ff");
            ////// HeaderCell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#f0f8ff");
            ////HeaderCell.Attributes.Add("style", "text-align:center;color:balck;");
            ////HeaderCell.Attributes.Add("colspan", "9");

            ////gvTestMark.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }

    private void ShowReport(string reportTitle, string rptFileName, string sessionNo, string schemeNo, string semesterNo, string idNo, int Courseno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + sessionNo + ",@P_SCHEMENO=" + schemeNo + ",@P_SEMESTERNO=" + semesterNo + ",@P_IDNO=" + idNo + ",@P_COURSENO=" + Courseno + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lnkCourseWiseAttendance_Command(object sender, CommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument != null)
            {

                int idno = 0;
                FeeCollectionController feeController = new FeeCollectionController();
                if (ViewState["usertype"].ToString() == "2")
                {
                    idno = Convert.ToInt32(Session["idno"]);
                }
                else
                {
                    idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());
                }
                LinkButton btn = (LinkButton)(sender);
                int coursewiseno = Convert.ToInt32(btn.CommandArgument);

                ShowReport("Course Wise Attendance", "StudentCourseWiseAttendanceReport.rpt", lblattsession.ToolTip, lblattScheme.ToolTip, lblattsemester.ToolTip, idno.ToString(), coursewiseno);
            }
        }
        catch { }
    }

    protected void lnkbtnStudGC_Click(object sender, EventArgs e)
    {
        //////Show Tabulation Sheet
        LinkButton btn = sender as LinkButton;
        //string hdfSemester = (btn.Parent.FindControl("hdfSemester") as HiddenField).Value;
        string IdNo = (btn.Parent.FindControl("hdfIDNogc") as HiddenField).Value;
        this.ShowTranscript("Transcript Report", "rptStudentGradeCard.rpt", IdNo);
    }

    private void ShowTranscript(string reportTitle, string rptFileName, string IdNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + IdNo + ",@P_SEMESTERNO=" + 0 + "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_TranscriptReport.btnTranscript_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lnkNAME_Click(object sender, EventArgs e)
    {
        txtEnrollmentSearch.Text = string.Empty;
        LinkButton lnkNAME = (LinkButton)(sender);
        txtEnrollmentSearch.Text = lnkNAME.CommandName;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        div1.Visible = false;
        div2.Visible = true;
        div4.Visible = true;
        div5.Visible = true;
        div6.Visible = true;
        div7.Visible = true;
        div8.Visible = true;
        div9.Visible = true;
        div10.Visible = true;
        div11.Visible = true;
        div12.Visible = true;
        div13.Visible = true;
        div14.Visible = true;
        ShowDetails();
        rdoSearchAll.Checked = false;
        rdoAll.Checked = true;
    }

    #region Student Memo

    protected void lnkbtnMemo_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton btn = sender as LinkButton;
            int sessionNo = Convert.ToInt32((btn.Parent.FindControl("hdfSession") as HiddenField).Value);
            int degreno = Convert.ToInt32((btn.Parent.FindControl("hdfDegree") as HiddenField).Value);
            int branchno = Convert.ToInt32((btn.Parent.FindControl("hdfBranch") as HiddenField).Value);
            int schemeNo = Convert.ToInt32((btn.Parent.FindControl("hdfScheme") as HiddenField).Value);
            int semesterNo = Convert.ToInt32((btn.Parent.FindControl("hdfSemester") as HiddenField).Value);
            string IdNo = Convert.ToString((btn.Parent.FindControl("hdfIDNo") as HiddenField).Value);
            int Section = Convert.ToInt32((btn.Parent.FindControl("hdfSection") as HiddenField).Value);
            int clgID = Convert.ToInt32((btn.Parent.FindControl("hdfclgID") as HiddenField).Value);
            int collegeCode = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_MASTER", "CODE", "COLLEGE_ID=" + clgID));

            string reportTitle = "Student Memo";
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + "rptStudentMemo.rpt";
            url += "&param=@P_SESSIONNO=" + sessionNo + ",@P_DEGREENO=" + degreno + ",@P_BRANCHNO=" + branchno + ",@P_SCHEMENO=" + schemeNo
                + ",@P_SEMESTERNO=" + semesterNo + ",@P_SECTIONNO=" + Section
                + ",@P_IDNOS=" + IdNo.Trim() + ",@P_COLLEGE_CODE=" + collegeCode + ",@P_COLLEGE_ID=" + clgID;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "lnkbtnMemo_Click.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnUploadMemo_Click(object sender, EventArgs e)
    {
        try
        {

            bool flag = false;
            string semNo = objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + Convert.ToInt32(Session["IDNO"].ToString()));
            int session = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_MEMO", "max(SESSIONNO)SESSIONNO", "IDNO=" + Convert.ToInt32(Session["IDNO"].ToString()) + " AND SEMESTERNO=" + semNo));
            int clgID = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_MEMO", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["IDNO"].ToString())));
            string path = MapPath("~/StudentMemo/");

            string DocFileName = string.Empty;

            string[] validFileTypes = { "pdf" };// "doc", "docx", "jpg", "jpeg", , "rtf"
            string ext = string.Empty; //System.IO.Path.GetExtension(fuDocUpload.PostedFile.FileName);

            if (fuDocUpload.HasFile)
            {
                ext = System.IO.Path.GetExtension(fuDocUpload.PostedFile.FileName);// GET FILE EXT.

                for (int j = 0; j < validFileTypes.Length; j++)
                {
                    if (ext == "." + validFileTypes[j])//IF YES THEN UPLOAD..
                    {
                        int fileSize = fuDocUpload.PostedFile.ContentLength;// Get actul file size..
                        if (fileSize < 524288)//(0.5mb) 1mb(1048576)
                        {
                            string[] array1 = Directory.GetFiles(path);
                            foreach (string str in array1)
                            {
                                if ((path + "\\" + Session["IDNO"].ToString() + "_" + semNo + "_" + fuDocUpload.FileName.Replace(" ", "_")).Equals(str))
                                {
                                    objCommon.DisplayMessage(this, "File Already Exists!", this);
                                    return;
                                }
                            }
                            string fName = Session["IDNO"].ToString() + "_" + semNo + "_" + fuDocUpload.FileName.Replace(" ", "_");
                            fuDocUpload.SaveAs(path + "/" + Session["IDNO"].ToString() + "_" + semNo + "_" + fuDocUpload.FileName.Replace(" ", "_"));

                            int i = objAttC.UploadStudMemo(session, Convert.ToInt32(semNo), Convert.ToInt32(Session["IDNO"]), fName, clgID);
                            if (i != null && i != -99)
                            {
                                objCommon.DisplayMessage(this, "File Uploaded SuccessFully...!", this);
                                ShowDetails();
                            }
                            else
                            {
                                objCommon.DisplayMessage("Error!!", this.Page);
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this, "Filesize of Document is too large. Maximum file size permitted is 0.5MB", this);
                            fuDocUpload.Focus();
                            return;
                        }
                    }
                }
                if (flag == false)
                {
                    objCommon.DisplayMessage(this, "Invalid File Format,Please Upload Valid file!!", this);
                    fuDocUpload.Focus();
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "Select File to Upload!", this);
                fuDocUpload.Focus();
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Administration_courseMaster.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Directory Not Found Exception!!");
        }

        #region cmt
        //try
        //{
        //    string path = MapPath("~/ExcelData/");

        //    if (btnBrowse.HasFile)
        //    {
        //        string extension = Path.GetExtension(btnBrowse.PostedFile.FileName);

        //        string filename = btnBrowse.FileName.ToString();
        //        if (filename.Contains(".xls") || filename.Contains(".xlsx"))
        //        {
        //            DataTable dt = AL.Blob_GetAllBlobs(blob_ConStr, blob_ContainerName);

        //            filename = filename.Replace(".xlsx", DateTime.Now.ToString("dd-MM-yyyy-hmm") + Session["username"].ToString());
        //            filename = filename.Replace(".xls", DateTime.Now.ToString("dd-MM-yyyy-hmm") + Session["username"].ToString());

        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                if (dt.Rows[i]["Id"].ToString().Split('.')[0] == filename)
        //                {
        //                    objCommon.DisplayMessage(this.Page, "File with similar name already exists!", this);
        //                    return;
        //                }
        //            }

        //            ViewState["FileName"] = filename; ;
        //            //Upload file to Blob Storage
        //            btnBrowse.SaveAs(path + filename);


        //            int retval = AL.Blob_Upload(blob_ConStr, blob_ContainerName, filename, btnBrowse);
        //            if (retval == 0)
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
        //                return;
        //            } 

        //            //if (!CheckFormatAndImport(extension, path + filename))                   
        //            //{
        //            //    objCommon.DisplayMessage(this.Page, "File is not in correct format!!Please check if the data is saved in SHEET1. Else check if  the column names have changed!", this.Page);
        //            //}
        //        }
        //        else
        //        {
        //            objCommon.DisplayMessage(this.Page, "Only Excel Sheet is Allowed!", this);
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        objCommon.DisplayMessage(this.Page, "Select File to Upload!", this);
        //        return;
        //    }
        //   // this.BindTeachingPlan();
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUaimsCommon.ShowError(this.Page, " ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUaimsCommon.ShowError(Page, "Server UnAvailable");

        //    //objCommon.DisplayMessage(updTeach, "Error!!", this);
        //}
        #endregion cmt
    }

    //private bool CheckFormatAndImport(string extension, string excelPath)   
    //{

    //    string sConnectionString = string.Empty;

    //    switch (extension)
    //    {
    //        case ".xls": //Excel 97-03
    //            sConnectionString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
    //            break;
    //        case ".xlsx": //Excel 07 or higher
    //            sConnectionString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
    //            break;
    //    }

    //    sConnectionString = string.Format(sConnectionString, excelPath);

    //    string Message = string.Empty;
    //    int count = 0;
    //    OleDbConnection objCon = new OleDbConnection(sConnectionString);


    //    try
    //    {
    //        objCon.Open();
    //        OleDbCommand objcmd = new OleDbCommand("Select * from [SHEET1$] ", objCon);

    //        OleDbDataAdapter objda = new OleDbDataAdapter(objcmd);
    //        objda.SelectCommand = objcmd;
    //        DataSet ds = new DataSet();

    //        objda.Fill(ds);

    //        int i = ds.Tables[0].Rows.Count;
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
    //            {
    //                if (ds.Tables[0].Rows[i]["DATE"].ToString() != "" && ds.Tables[0].Rows[i]["DATE"].ToString() != null)
    //                {
    //                    #region cmt...
    //                    objAttModel.Date = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["DATE"].ToString()).ToString("yyyy-MM-dd"));

    //                    //string section = objCommon.LookUp("ACD_SECTION", "SECTIONNO", "SECTIONNAME LIKE '%" + ds.Tables[0].Rows[i]["SECTIONNAME"].ToString().Trim() + "%'");
    //                    //objAttModel.Sectionno = Convert.ToInt16(section == "" ? "0" : section);

    //                    string section = "0";
    //                    //objAttModel.Sectionno = Convert.ToInt16(section == "" ? "0" : section);
    //                    objAttModel.Topic_Covered = ds.Tables[0].Rows[i]["TOPIC_COVERED"].ToString().Trim();
    //                    objAttModel.UnitNo = Convert.ToInt16(ds.Tables[0].Rows[i]["UNIT_NO"] == DBNull.Value ? "0" : ds.Tables[0].Rows[i]["UNIT_NO"].ToString().Trim());
    //                    objAttModel.Lecture_No = Convert.ToInt16(ds.Tables[0].Rows[i]["LECTURE_NO"] == DBNull.Value ? "0" : ds.Tables[0].Rows[i]["LECTURE_NO"].ToString().Trim());
    //                    objAttModel.Remark = ds.Tables[0].Rows[i]["REMARK"].ToString().Trim();

    //                    string batch = objCommon.LookUp("ACD_BATCH", "BATCHNO", "BATCHNAME LIKE '%" + ds.Tables[0].Rows[i]["BATCH"].ToString().Trim() + "%'");
    //                    objAttModel.BatchNo = Convert.ToInt16(batch == "" ? "0" : batch);

    //                    string SlotTeaching = objCommon.LookUp("ACD_ACADEMIC_TT_SLOT", "SLOTNO", "SLOTNAME LIKE '%" + ds.Tables[0].Rows[i]["SLOTNAME"].ToString().Trim() + "%' AND COLLEGE_ID=" + ViewState["college_id"].ToString());
    //                    objAttModel.SlotTeaching = SlotTeaching == "" ? "0" : SlotTeaching;

    //                    objAttModel.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
    //                    objAttModel.Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);

    //                    if (!(Convert.ToInt32(Session["usertype"]) > 1))
    //                        objAttModel.Schemeno = Convert.ToInt32(ViewState["schemeno"].ToString());
    //                    else
    //                        objAttModel.Schemeno = Convert.ToInt32(ViewState["schemeno"].ToString());//NOTE : FOR TEACHER LOGIN WE GO BY UNIQUE COURSE NO.

    //                    if (Convert.ToInt32(ddlSection.SelectedValue) > 0)
    //                        objAttModel.Sectionno = Convert.ToInt32(ddlSection.SelectedValue);
    //                    else
    //                    {
    //                        section = objCommon.LookUp("ACD_SECTION", "SECTIONNO", "SECTIONNAME LIKE '%" + ds.Tables[0].Rows[i]["SECTIONNAME"].ToString().Trim() + "%'");
    //                        objAttModel.Sectionno = Convert.ToInt16(section == "" ? "0" : section);
    //                    }
    //                    objAttModel.BatchNo = Convert.ToInt32(ddlBatch.SelectedValue);
    //                    //if (ddlSession.SelectedIndex <= 0)
    //                    //    objAttModel.Sessionno = Convert.ToInt32(Session["currentsession"].ToString());
    //                    //else
    //                    objAttModel.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
    //                    objAttModel.CollegeId = Convert.ToInt32(ViewState["college_id"].ToString());

    //                    objAttModel.UA_No = Convert.ToInt16(Session["userno"].ToString());
    //                    int Istutorial = ddlSubjectType.SelectedValue == "100" ? 1 : 0;

    //                    if (CheckDuplicateUploadEntry(objAttModel.UnitNo, objAttModel.Lecture_No, objAttModel.Sectionno, objAttModel.BatchNo, objAttModel.Slot) == true)
    //                    {
    //                        objCommon.DisplayMessage(this.updTeach, "Entry for this Unit No. and Topic Code is Already Done!", this.Page);
    //                        return false;
    //                    }
    //                    CustomStatus cs = (CustomStatus)objAttC.AddTeachingPlan(objAttModel, Istutorial);
    //                    if (cs.Equals(CustomStatus.RecordSaved))
    //                    {
    //                        objCommon.DisplayMessage(this.updTeach, "Teaching Plan Saved Successfully!", this.Page);
    //                    }
    //                    this.ResetDateDropdown();
    //                    this.BindTopiccodeUnit();

    //                    #endregion
    //                }
    //            }
    //        }
    //        if (count < i)
    //        {
    //            objCommon.DisplayMessage(updTeach, "Please check the format of file & upload again!", this.Page);
    //            return false;
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(updTeach, "Data Saved Successfully!", this.Page);
    //            return true;
    //        }
    //        File.Delete(excelPath + extension);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, ex.ToString() + "Please Check if the data is saved in sheet1 of the file you are uploading or the file is in correct format!! ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(this.Page, "Server UnAvailable");
    //        return false;
    //    }
    //    finally
    //    {
    //        objCon.Close();
    //        objCon.Dispose();
    //    }
    //}

    #endregion Student Memo

    #region Remark



    #endregion Remark
}
