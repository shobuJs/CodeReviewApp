//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Student Information Details
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
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Configuration;
using System.Globalization;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;

public partial class ACADEMIC_Comprehensive_Stud_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    //StudentRegistrationModel objStudReg = new StudentRegistrationModel();  
    StudentInformation objStudReg = new StudentInformation();
    StudentRegistrationController objStudRegC = new StudentRegistrationController();

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
            Response.Redirect("~/notauthorized.aspx?page=Student_details.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // imgPhoto.ImageUrl = "~/images/nophoto.jpg";
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["currentsession"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //     CheckPageAuthorization();

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
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                string ipaddress = ViewState["ipAddress"].ToString();
                Session["bank"] = "0";
                YearBind();
                MonthBind();

                if (ViewState["usertype"].ToString() == "2")
                {
                    pnlSearch.Visible = false;
                    ShowDetails();
                   // objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO<>0", "");  // commented on 01082020
                    objCommon.FillDropDownList(ddlProjectType, "ACD_STUDENT_PROJECT_TYPE_MASTER", "PROJECT_TYPE_NO", "PROJECT_TYPE_NAME", "PROJECT_TYPE_NO<>0", "");
                    objCommon.FillDropDownList(ddlOrganization, "ACD_STUDENT_SCHOLARSHIP_ORGANIZATION_TYPE_MASTER", "ORGANIZATION_TYPE_NO", "ORGANIZATION_TYPE_NAME", "ORGANIZATION_TYPE_NO<>0", "");
                    objCommon.FillDropDownList(ddlAuthor, "ACD_PUBLICATION_AUTHORS_MASTER", "AUTHOR_NO", "AUTHOR_NAME", "AUTHOR_NO<>0", "");
                }

                else if (ViewState["usertype"].ToString() == "3")
                {
                    pnlSearch.Visible = true;
                    objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO<>0", ""); // added on 31072020 for faculty.
                    objCommon.FillDropDownList(ddlProjectType, "ACD_STUDENT_PROJECT_TYPE_MASTER", "PROJECT_TYPE_NO", "PROJECT_TYPE_NAME", "PROJECT_TYPE_NO<>0", "");
                    objCommon.FillDropDownList(ddlOrganization, "ACD_STUDENT_SCHOLARSHIP_ORGANIZATION_TYPE_MASTER", "ORGANIZATION_TYPE_NO", "ORGANIZATION_TYPE_NAME", "ORGANIZATION_TYPE_NO<>0", "");
                    objCommon.FillDropDownList(ddlAuthor, "ACD_PUBLICATION_AUTHORS_MASTER", "AUTHOR_NO", "AUTHOR_NAME", "AUTHOR_NO<>0", "");
                }
                else
                {
                    pnlSearch.Visible = true;
                    objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO<>0", "");
                    objCommon.FillDropDownList(ddlProjectType, "ACD_STUDENT_PROJECT_TYPE_MASTER", "PROJECT_TYPE_NO", "PROJECT_TYPE_NAME", "PROJECT_TYPE_NO<>0", "");
                    objCommon.FillDropDownList(ddlOrganization, "ACD_STUDENT_SCHOLARSHIP_ORGANIZATION_TYPE_MASTER", "ORGANIZATION_TYPE_NO", "ORGANIZATION_TYPE_NAME", "ORGANIZATION_TYPE_NO<>0", "");
                    objCommon.FillDropDownList(ddlAuthor, "ACD_PUBLICATION_AUTHORS_MASTER", "AUTHOR_NO", "AUTHOR_NAME", "AUTHOR_NO<>0", "");
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
                    //lvRegStatus.DataSource = null;
                    //lvRegStatus.DataBind();
                    //lblsession.Text = string.Empty;
                    //lblsmester.Text = string.Empty;
                    divinfo.Visible = false;
                    // lvFees.DataSource = null;
                    // lvFees.DataBind();s
                    //lvCertificate.DataSource = null;
                    //lvCertificate.DataBind();
                    lblMsg.Text = string.Empty;
                }
            }
        }
        divMsg.InnerHtml = string.Empty;


    }

    private void YearBind()
    {
        //ddlYear.Items.Add("Please Select");
        //ddlYear.SelectedItem.Value = "0";
        for (int jLoop = 2018; jLoop <= DateTime.Now.Year; jLoop++)   //need to add +4 in order to increase Year
        {
            ddlYear.Items.Add(jLoop.ToString());
        }
    }


    void MonthBind()
    {
        for (int month = 1; month <= 12; month++)
        {
            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            ddlMonth.Items.Add(new ListItem(monthName, month.ToString().PadLeft(2, '0')));
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ClearAll();
        ShowDetails();
    }


    private void ShowDetails()
    {
        Clear();
        int idno = 0;
        StudentController objSC = new StudentController();
        //   DataSet  dsCertificate, dsRemark, dsRefunds, dsAttendance;

        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "3")
        {
            string no = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtEnrollmentSearch.Text.Trim() + "' OR ENROLLNO = '" + txtEnrollmentSearch.Text.Trim() + "'");

            string idn = objCommon.LookUp("acd_student", "IDNO", "fac_advisor=" + Convert.ToInt32(Session["userno"]) + " and idno=" + no);

            if (idn == "")
            {
                objCommon.DisplayMessage("No student found having Registration No. / Adm. No.: " + txtEnrollmentSearch.Text.Trim(), this.Page);
                return;
            }
            else
                idno = Convert.ToInt32(idn);
        }
        else
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());
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
                    //  BankDetails();
                    while (dtr.Read())
                    {
                        lblRegNo.Text = dtr["REGNO"].ToString();
                        //lblRegNo.ToolTip = dtr["idno"].ToString();
                        lblRegNo.ToolTip = idno.ToString();
                        //  lblRegNo.ToolTip = Session["idno"].ToString();
                        ViewState["admbatch"] = dtr["ADMBATCH"];
                        string degreeno = dtr["DEGREENO"] == null ? "0" : dtr["DEGREENO"].ToString();
                        string branchno = dtr["BRANCHNO"] == null ? "0" : dtr["BRANCHNO"].ToString();

                        DataSet ds = objCommon.FillDropDown("ACD_BRANCH A ,ACD_DEGREE B", "LONGNAME", "DEGREENAME, ISNULL(B.YEARWISE,0) AS YEARWISE", "(A.BRANCHNO=" + branchno + " OR " + branchno + "=0) AND (B.DEGREENO=" + degreeno + " OR " + degreeno + "=0)", "");
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
                        lblEnrollNo.Text = dtr["ENROLLNO"] == null ? string.Empty : dtr["ENROLLNO"].ToString();
                        string semester = string.Empty;

                        if (ds.Tables[0].Rows[0]["YEARWISE"].ToString() == "1")
                        {
                            semester=objCommon.LookUp("ACD_YEAR", "YEARNAME", "YEAR=" + dtr["YEAR"].ToString());
                            lblSemester.Text = semester;
                        }
                        else
                        {
                            semester=objCommon.LookUp("ACD_SEMESTER", "SEMESTERNAME", "SEMESTERNO=" + dtr["SEMESTERNO"].ToString());
                            lblSemester.Text = semester;
                        }

                        //  imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dtr["IDNO"].ToString() + "&type=student";
                        //   imgSign.ImageUrl = "~/showimage.aspx?id=" + dtr["IDNO"].ToString() + "&type=STUDENTSIGN";

                        //Students Current Project Details                   

                        DataSet dsProject = objStudRegC.GetStudentProjectDetails(idno, semester);
                        if (dsProject.Tables[0].Rows.Count > 0)
                        {
                            divinfo.Visible = true;
                            lvProjectStatus.Visible = true;
                            lvProjectStatus.DataSource = dsProject;
                            lvProjectStatus.DataBind();
                        }
                        else
                        {
                            divinfo.Visible = true;
                            lvProjectStatus.Visible = false;
                            lvProjectStatus.DataSource = null;
                            lvProjectStatus.DataBind();
                        }

                        //End of Students Current Project Details


                        // Students current Internship Details
                        DataSet dsInternship = objStudRegC.GetStudentInternshipDetails(idno, semester);

                        if (dsInternship.Tables[0].Rows.Count > 0)
                        {
                            LvIntership.Visible = true;
                            LvIntership.DataSource = dsInternship;
                            LvIntership.DataBind();

                        }
                        else
                        {
                            LvIntership.Visible = false;
                            LvIntership.DataSource = null;
                            LvIntership.DataBind();
                        }

                        //End of Students current Internship Details


                        //Students current Publications Details
                        DataSet dsPub = objStudRegC.GetStudentPublicationsDetails(idno, semester);
                        if (dsPub.Tables[0].Rows.Count > 0)
                        {
                            LvPublication.Visible = true;
                            LvPublication.DataSource = dsPub;
                            LvPublication.DataBind();
                        }
                        else
                        {
                            LvPublication.Visible = false;
                            LvPublication.DataSource = null;
                            LvPublication.DataBind();
                        }

                        //End of Students current Publications Details

                        //Students current Achievements Details
                        DataSet dsAchievement = objStudRegC.GetStudentAchievementDetails(idno, semester);
                        if (dsAchievement.Tables[0].Rows.Count > 0)
                        {
                            LvAchievements.Visible = true;
                            LvAchievements.DataSource = dsAchievement;
                            LvAchievements.DataBind();
                        }
                        else
                        {
                            LvAchievements.Visible = false;
                            LvAchievements.DataSource = null;
                            LvAchievements.DataBind();
                        }
                        BankDetails();
                        //End of Students current Achievements Details

                        //Students current Scholarship Details
                        DataSet dsScholarship = objStudRegC.GetStudentScholarshipDetails(idno, semester);
                        if (dsScholarship.Tables[0].Rows.Count > 0)
                        {
                            LvScholarship.Visible = true;
                            LvScholarship.DataSource = dsScholarship;
                            LvScholarship.DataBind();
                        }
                        else
                        {
                            LvScholarship.Visible = false;
                            LvScholarship.DataSource = null;
                            LvScholarship.DataBind();
                        }

                        //End of Students current Scholarship Details


                        #region Commeneted
                        //                        //Students Attendance Details

                        //                        string semesterno = dtr["SEMESTERNO"].ToString();
                        //                        string schemeno = dtr["SCHEMENO"].ToString();
                        //                        string sessionno = Session["currentsession"].ToString();

                        //                        dsAttendance = objSC.RetrieveStudentAttendanceDetails(Convert.ToInt32(sessionno), Convert.ToInt32(schemeno), Convert.ToInt32(semesterno), idno);

                        //                        if (dsAttendance.Tables[0].Rows.Count > 0)
                        //                        {
                        //                            //divattinfo.Visible = true;
                        //                            //
                        //                            //lblattsession.Text = dsAttendance.Tables[0].Rows[0]["SESSIONNAME"].ToString();
                        //                            //lblattsession.ToolTip = dsAttendance.Tables[0].Rows[0]["SESSIONNO"].ToString();
                        //                            //lblattsemester.Text = dsAttendance.Tables[0].Rows[0]["SEMESTER"].ToString();
                        //                            //lblattsemester.ToolTip = dsAttendance.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                        //                            //lblattScheme.ToolTip = dsAttendance.Tables[0].Rows[0]["SCHEMENO"].ToString();
                        //                            //lvAttendance.DataSource = dsAttendance;
                        //                            //lvAttendance.DataBind();
                        //                        }
                        //                        else
                        //                        {
                        //                            // divattinfo.Visible = false;
                        //                            // lblattsession.Text = string.Empty;
                        //                            // lblattsemester.Text = string.Empty;
                        //                            // lblattScheme.ToolTip = string.Empty;
                        //                            // lblattsession.ToolTip = string.Empty;
                        //                            // lblattsemester.ToolTip = string.Empty;
                        //                            // lvAttendance.DataSource = null;
                        //                            // lvAttendance.DataBind();
                        //                        }

                        //                        // End of Students Attendance Details


                        //                        //Student Result Details

                        //                        //dsResult = objSC.RetrieveStudentCurrentResult(idno);
                        //                        //if (dsResult.Tables[0].Rows.Count > 0)
                        //                        //{
                        //                        //    lvResult.DataSource = dsResult;
                        //                        //    lvResult.DataBind();
                        //                        //}
                        //                        //else
                        //                        //{
                        //                        //    lvResult.DataSource = null;
                        //                        //    lvResult.DataBind();
                        //                        //}

                        //                        //End of Student Result Details

                        //                        //Student Result Details New

                        //                        try
                        //                        {
                        //                            //string sscmark = objCommon.LookUp("ACD_STU_LAST_QUALEXM", "MARKS_OBTAINED", "IDNO=" + idno + " AND QUALIFYNO=1");
                        //                            //string Intermark = objCommon.LookUp("ACD_STU_LAST_QUALEXM", "MARKS_OBTAINED", "IDNO=" + idno + " AND QUALIFYNO=2");
                        //                            //DataSet dsSemester = objCommon.FillDropDown("ACD_STUDENT_RESULT_HIST", "DISTINCT DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTER", "SEMESTERNO,IDNO", "IDNO=" + idno, "SEMESTERNO DESC");
                        //                            //DataSet dsSemester = objSC.RetrieveStudentSemesterMark(idno, Convert.ToInt32(ViewState["SEMESTERNO"]));
                        //                            DataSet dsSemester = objSC.RetrieveStudentSemesterNumberResult(idno);
                        //                            if (dsSemester != null && dsSemester.Tables.Count > 0 && dsSemester.Tables[0].Rows.Count > 0)
                        //                            {

                        //                                //gvParentGrid.DataSource = dsSemester;
                        //                                //gvParentGrid.DataBind();
                        //                                //gvParentGrid.Visible = true;

                        //                            }
                        //                            else
                        //                            {
                        //                                //pnlCollege.Visible = false;
                        //                                //lvSemester.DataSource = null;
                        //                                //lvSemester.DataBind();
                        //                                //gvParentGrid.DataSource = null;
                        //                                //gvParentGrid.DataBind();
                        //                                //gvParentGrid.Visible = false;
                        //                            }
                        //                        }
                        //                        catch
                        //                        {
                        //                        }

                        //                        //End of Student Result Details New


                        //                        //Students Certificate issued Details

                        //                        dsCertificate = objSC.RetrieveStudentCertificateDetails(idno);
                        //                        if (dsCertificate.Tables[0].Rows.Count > 0)
                        //                        {
                        //                            //lvCertificate.DataSource = dsCertificate;
                        //                            //lvCertificate.DataBind();
                        //                        }
                        //                        else
                        //                        {
                        //                            //lvCertificate.DataSource = null;
                        //                            //lvCertificate.DataBind();
                        //                        }

                        //                        //  End of Students Certificate issued Details


                        //                        //Remark

                        //                        dsRemark = objSC.GetAllRemarkDetails(idno);
                        //                        if (dsRemark != null && dsRemark.Tables.Count > 0 && dsRemark.Tables[0].Rows.Count > 0)
                        //                        {
                        //                            //lvRemark.DataSource = dsRemark;
                        //                            //lvRemark.DataBind();
                        //                        }
                        //                        else
                        //                        {
                        //                            //lvRemark.DataSource = null;
                        //                            //lvRemark.DataBind();
                        //                        }

                        //                        // End of Remark


                        //                        //Refund details

                        //                        dsRefunds = objSC.GetStudentRefunds(idno);
                        //                        if (dsRefunds != null && dsRefunds.Tables.Count > 0 && dsRefunds.Tables[0].Rows.Count > 0)
                        //                        {
                        //                            //lvRefund.DataSource = dsRefunds;
                        //                            //lvRefund.DataBind();
                        //                        }
                        //                        else
                        //                        {
                        //                            //lvRefund.DataSource = null;
                        //                            //lvRefund.DataBind();
                        //                        }


                        //                        // End of Refund Details

                        #endregion
                        dtr.Close();
                    }

                }

            }
            else
            {
                // objCommon.DisplayMessage(this.updStudent,"No student found having USN no.: " + txtEnrollmentSearch.Text.Trim(), this.Page);
                objCommon.DisplayMessage("No student found having Registration No. / Adm. No.: " + txtEnrollmentSearch.Text.Trim(), this.Page);
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

    private void BankDetails()
    {
        try
        {
            DataSet dsBank = objCommon.FillDropDown("ACD_STUDENT", "IDNO", "ISNULL(BANKNO,0)BANKNO,BANK_BRANCH,IFSC_CODE,ACC_NO", "IDNO=" + lblRegNo.ToolTip + "", "");
            if (dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                if (dsBank.Tables[0].Rows[0]["BANKNO"] == "0")
                {
                    bankdetails.Visible = false;
                }
                else
                {
                    bankdetails.Visible = true;
                    ddlBank.SelectedValue = dsBank.Tables[0].Rows[0]["BANKNO"].ToString();
                    txtBankbranch.Text = dsBank.Tables[0].Rows[0]["BANK_BRANCH"].ToString();
                    txtIfsc.Text = dsBank.Tables[0].Rows[0]["IFSC_CODE"].ToString();
                    txtAcc.Text = dsBank.Tables[0].Rows[0]["ACC_NO"].ToString();
                    txtStudentBank.Text = "Bank Name : " + ddlBank.SelectedItem.Text + ", Acc_No : " + dsBank.Tables[0].Rows[0]["ACC_NO"].ToString() + ", Branch : " + dsBank.Tables[0].Rows[0]["BANK_BRANCH"].ToString() + ", IFSC Code : " + dsBank.Tables[0].Rows[0]["IFSC_CODE"].ToString();
                    txtStudentBank.ToolTip = "Student Bank Details";
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_Student_details.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
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
        //lblMName.Text = string.Empty;
        //lblDOB.Text = string.Empty;
        //lblCaste.Text = string.Empty;
        //lblCategory.Text = string.Empty;
        //lblReligion.Text = string.Empty;
        //lblNationality.Text = string.Empty;
        //lblLAdd.Text = string.Empty;
        // lblLLNo.Text = string.Empty;
        // lblMobNo.Text = string.Empty;
        // lblPAdd.Text = string.Empty;
        // imgPhoto.ImageUrl = null;
        // lvRegStatus.DataSource = null;
        // lvRegStatus.DataBind();
        //lblsession.Text = string.Empty;
        //lblsmester.Text = string.Empty;
        divinfo.Visible = false;
        //lvAttendance.DataSource = null;
        //lvAttendance.DataBind();
        //gvParentGrid.DataSource = null;
        //gvParentGrid.DataBind();
        //lvResult.DataSource = null;
        //lvResult.DataBind();
        //lvFees.DataSource = null;
        //lvFees.DataBind();
        //lvCertificate.DataSource = null;
        //lvCertificate.DataBind();

        //Commented By Hemanth G 
        //gvTestMark.DataSource = null;
        //gvTestMark.DataBind();
        //lvRemark.DataSource = null;
        //lvRemark.DataBind();
        //lvRefund.DataSource = null;
        //lvRefund.DataBind();
        lblSemester.Text = string.Empty;

        lblDegree.Text = string.Empty;
        //lblMotherName.Text = string.Empty;
        // divtestmarkinfo.Visible = false;
        // lbltestsession.Text = string.Empty;
        // lbltestsemester.Text = string.Empty;
        lblEnrollNo.Text = string.Empty;
    }

    // Commented By Hemanth G
    #region Not In Use
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
            //gvTestMark.Controls[0].Controls.AddAt(0, HeaderGridRow2);

            //---------------------------------------------------
            TableCell HeaderCell3 = new TableCell();
            HeaderCell3.Text = "CAT-1";
            HeaderGridRow2.Cells.Add(HeaderCell3);
            HeaderCell3.Attributes.Add("style", "text-align:center;color:white;");
            HeaderCell3.Attributes.Add("colspan", "2");
            // gvTestMark.Controls[0].Controls.AddAt(0, HeaderGridRow2);
            //------------------------------------------------------------
            TableCell HeaderCell4 = new TableCell();
            HeaderCell4.Text = "CAT-2";
            HeaderGridRow2.Cells.Add(HeaderCell4);
            HeaderCell4.Attributes.Add("style", "text-align:center;color:white;");
            HeaderCell4.Attributes.Add("colspan", "2");
            //  gvTestMark.Controls[0].Controls.AddAt(0, HeaderGridRow2);
            //------------------------------------------------------------
            TableCell HeaderCell5 = new TableCell();
            HeaderCell5.Text = "CAT-3";
            HeaderGridRow2.Cells.Add(HeaderCell5);
            HeaderCell5.Attributes.Add("style", "text-align:center;color:white;");
            HeaderCell5.Attributes.Add("colspan", "2");
            //gvTestMark.Controls[0].Controls.AddAt(0, HeaderGridRow2);

            //------------------------------------------------------------
            TableCell HeaderCell6 = new TableCell();
            HeaderCell6.Text = " ";
            HeaderGridRow2.Cells.Add(HeaderCell6);
            HeaderCell6.Attributes.Add("style", "text-align:center;color:white;");
            HeaderCell6.Attributes.Add("colspan", "2");
            //   gvTestMark.Controls[0].Controls.AddAt(0, HeaderGridRow2);


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

                //ShowReport("Course Wise Attendance", "StudentCourseWiseAttendanceReport.rpt", lblattsession.ToolTip, lblattScheme.ToolTip, lblattsemester.ToolTip, idno.ToString(), coursewiseno);
            }
        }
        catch { }
    }
    #endregion

    protected void btnBankClear_Click(object sender, EventArgs e)
    {
        BankCancel();
    }

    protected void btnProjectAdd_Click(object sender, EventArgs e)
    {
        ProjectInsert();
    }
    private void ProjectInsert()
    {
        try
        {
            objStudReg.IDNO = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());
            objStudReg.Semesterno = Convert.ToInt32(lblSemester.Text.ToString());
            objStudReg.ProjectName = txtNameofPrjct.Text;
            objStudReg.IndustryName = txtprjctNameindustry.Text;
            objStudReg.IndustryAddress = txtAdresIndustry.Text;
            objStudReg.Project_Details = txtPrjctDetails.Text;
            //if (txtProjectFromDate.Text.Trim().Equals(string.Empty) ? txtProjectFromDate.Text:String.Empty) ;

            // objStudReg.To_date = Convert.ToDateTime(txtProjectTodate.Text);
            //if (!txtProjectFromDate.Text.Trim().Equals(string.Empty) objStudReg.To_date = Convert.ToDateTime(txtProjectFromDate.Text)) ;
            //objStudReg.From_date =   //txtProjectFromDate.Text == string.Empty ? Convert.ToDateTime(DateTime) : Convert.ToDateTime(txtProjectFromDate.Text); //:Convert.ToDateTime(DateTime.MinValue)
            //objStudReg.To_date = txtProjectTodate.Text == string.Empty ? Convert.ToDateTime(DateTime.MinValue) : Convert.ToDateTime(txtProjectTodate.Text);
            if (!txtProjectFromDate.Text.Trim().Equals(string.Empty)) objStudReg.From_date = Convert.ToDateTime(txtProjectFromDate.Text);
            if (!txtProjectTodate.Text.Trim().Equals(string.Empty)) objStudReg.To_date = Convert.ToDateTime(txtProjectTodate.Text);

                
            objStudReg.Duration = hdfduration.Value;
            objStudReg.Grant_Received = txtGrantReceived.Text==string.Empty ? 0 : Convert.ToDouble(txtGrantReceived.Text);
            objStudReg.Supervisor_Details = txtSupervisor.Text;
            objStudReg.Mentor_College = txtMentor.Text;
            objStudReg.Project_Type = ddlProjectType.SelectedValue;
            objStudReg.Industry_Type = Convert.ToInt32(rdbProjects.SelectedValue);
            objStudReg.Ua_no = Convert.ToInt32(Session["userno"]);
            objStudReg.Ip_address = ViewState["ipAddress"].ToString();
            objStudReg.College_code = Session["colcode"].ToString();


            //Added by Naresh For Files Upload

            objStudReg.ProjectFilename = "";
            objStudReg.ProjectFilePath = "";

            if (fuProjectCertificate.HasFile)
            {
                Check50KBFileSize();

                if (validationflag == true)
                {
                    //to upload multiple sports docs details
                    uploadMultipleSportsCertificate(fuProjectCertificate, "Project");
                    if (status == 1)
                    {
                        CustomStatus cs = (CustomStatus)objStudRegC.AddProjectDetails(objStudReg);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(this.Page, "Project Details Saved Successfully !!!", this.Page);
                            ProjectCancel();
                            DataSet dsProject = objStudRegC.GetStudentProjectDetails(objStudReg.IDNO, lblSemester.Text);
                            if (dsProject.Tables[0].Rows.Count > 0)
                            {
                                divinfo.Visible = true;
                                lvProjectStatus.Visible = true;

                                lvProjectStatus.DataSource = dsProject;
                                lvProjectStatus.DataBind();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "Project Details Not Saved !!!", this.Page);
                        }
                    }
                }

            }
            else
            {
                CustomStatus cs = (CustomStatus)objStudRegC.AddProjectDetails(objStudReg);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.Page, "Project Details Saved Successfully !!!", this.Page);
                    ProjectCancel();
                    DataSet dsProject = objStudRegC.GetStudentProjectDetails(objStudReg.IDNO, lblSemester.Text);
                    if (dsProject.Tables[0].Rows.Count > 0)
                    {
                        divinfo.Visible = true;
                        lvProjectStatus.Visible = true;

                        lvProjectStatus.DataSource = dsProject;
                        lvProjectStatus.DataBind();
                    }
                    else
                    {
                        lvProjectStatus.Visible = false;
                        lvProjectStatus.DataSource = null;
                        lvProjectStatus.DataBind();
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Project Details Not Saved !!!", this.Page);
                }
            }

            //Ends here File Upload Naresh

            #region CodewithoutFileUpload
            //CustomStatus cs = (CustomStatus)objStudRegC.AddProjectDetails(objStudReg);
            //if (cs.Equals(CustomStatus.RecordSaved))
            //{
            //    objCommon.DisplayMessage(this.Page, "Project Details Saved Successfully !!!", this.Page);
            //    ProjectCancel();
            //    DataSet dsProject = objStudRegC.GetStudentProjectDetails(objStudReg.IDNO, lblSemester.Text);
            //    if (dsProject.Tables[0].Rows.Count > 0)
            //    {
            //        divinfo.Visible = true;
            //        lvProjectStatus.Visible = true;

            //        lvProjectStatus.DataSource = dsProject;
            //        lvProjectStatus.DataBind();
            //    }
            //}
            //else
            //{
            //    objCommon.DisplayMessage(this.Page, "Error !!!", this.Page);
            //}
            #endregion
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_details.btnProjectAdd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnInternship_Click(object sender, EventArgs e)
    {
        InternInsert();
    }

    private void InternInsert()
    {
        try
        {
            objStudReg.IDNO = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());
            objStudReg.Semesterno = Convert.ToInt32(lblSemester.Text.ToString());
            objStudReg.Industry_name = txtNameIndustry.Text;
            objStudReg.Address_industry = txtAddressIndustry.Text;
            objStudReg.Internship_details = txtDetailsInternship.Text;
          //  objStudReg.From_date = Convert.ToDateTime(txtFromDate.Text); //== string.Empty ? Convert.ToDateTime(DateTime.MinValue) : Convert.ToDateTime(txtFromDate.Text);
          //  objStudReg.To_date = Convert.ToDateTime(txtToDate.Text); //== string.Empty ? Convert.ToDateTime(DateTime.MinValue) : Convert.ToDateTime(txtToDate.Text);
            if (!txtFromDate.Text.Trim().Equals(string.Empty)) objStudReg.From_date = Convert.ToDateTime(txtFromDate.Text);
            if (!txtToDate.Text.Trim().Equals(string.Empty)) objStudReg.To_date = Convert.ToDateTime(txtToDate.Text);
            objStudReg.Duration = hdfinternDuration.Value;
            objStudReg.Remarks = txtRemarks.Text;
            objStudReg.Stipend = txtStipend.Text==string.Empty ? 0 : Convert.ToDouble(txtStipend.Text);
            objStudReg.Technical_person = txtTPerson.Text;
            objStudReg.Mobile_no = txtmobile.Text;
            objStudReg.Emailid = txtEmailid.Text;
            objStudReg.Ua_no = Convert.ToInt32(Session["userno"]);
            objStudReg.Ip_address = ViewState["ipAddress"].ToString();
            objStudReg.College_code = Session["colcode"].ToString();

            objStudReg.ProjectFilename = "";
            objStudReg.ProjectFilePath = "";

            if (FuIntershipCertificate.HasFile)
            {
                Check50KBFileSize();

                if (validationflag == true)
                {
                    //to upload multiple sports docs details
                    uploadMultipleSportsCertificate(FuIntershipCertificate, "Internship");
                    if (status == 1)
                    {
                        CustomStatus cs = (CustomStatus)objStudRegC.AddInternshipDetails(objStudReg);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(this.Page, "Internship Details Saved Successfully !!!", this.Page);
                            InternshipCancel();
                            DataSet dsInternship = objStudRegC.GetStudentInternshipDetails(objStudReg.IDNO, lblSemester.Text);
                            if (dsInternship.Tables[0].Rows.Count > 0)
                            {
                                LvIntership.Visible = true;
                                LvIntership.DataSource = dsInternship;
                                LvIntership.DataBind();

                            }
                            else
                            {
                                LvIntership.Visible = false;
                                LvIntership.DataSource = null;
                                LvIntership.DataBind();
                            }

                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "Internship Details Not Saved", this.Page);
                        }
                    }
                }

            }
            else
            {
                CustomStatus cs = (CustomStatus)objStudRegC.AddInternshipDetails(objStudReg);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.Page, "Internship Details Saved Successfully !!!", this.Page);
                    InternshipCancel();
                    DataSet dsInternship = objStudRegC.GetStudentInternshipDetails(objStudReg.IDNO, lblSemester.Text);
                    if (dsInternship.Tables[0].Rows.Count > 0)
                    {
                        LvIntership.Visible = true;
                        LvIntership.DataSource = dsInternship;
                        LvIntership.DataBind();

                    }
                    else
                    {
                        LvIntership.Visible = false;
                        LvIntership.DataSource = null;
                        LvIntership.DataBind();
                    }

                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Internship Details Not Saved", this.Page);
                }
            }
            #region WithoutFileuploadcode
            //CustomStatus cs = (CustomStatus)objStudRegC.AddInternshipDetails(objStudReg);
            //if (cs.Equals(CustomStatus.RecordSaved))
            //{
            //    objCommon.DisplayMessage(this.Page, "Internship Details Saved Successfully !!!", this.Page);
            //    InternshipCancel();
            //    DataSet dsInternship = objStudRegC.GetStudentInternshipDetails(objStudReg.IDNO, lblSemester.Text);
            //    if (dsInternship.Tables[0].Rows.Count > 0)
            //    {
            //        LvIntership.Visible = true;
            //        LvIntership.DataSource = dsInternship;
            //        LvIntership.DataBind();

            //    }
            //    else
            //    {
            //        LvIntership.Visible = false;
            //        LvIntership.DataSource = null;
            //        LvIntership.DataBind();
            //    }

            //}
            //else
            //{
            //    objCommon.DisplayMessage(this.Page, "Internship Details Not Saved", this.Page);
            //}
            #endregion
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_details.btnInternship_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnPublications_Click(object sender, EventArgs e)
    {
        PubInsert();
    }
    private void PubInsert()
    {
        try
        {
            objStudReg.IDNO = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());
            objStudReg.Semesterno = Convert.ToInt32(lblSemester.Text.ToString());
            objStudReg.Title_name = txtTitle.Text;
            objStudReg.Journal_name = txtJournalname.Text;
            objStudReg.Volume = txtVolume.Text;
            objStudReg.Page_no = txtPageNo.Text==string.Empty?0: Convert.ToInt32(txtPageNo.Text); //== string.Empty ? Convert.ToDateTime(DateTime.MinValue) : Convert.ToDateTime(txtFromDate.Text);
            objStudReg.Issue_no = txtIssueno.Text; //== string.Empty ? Convert.ToDateTime(DateTime.MinValue) : Convert.ToDateTime(txtToDate.Text);
            objStudReg.Authors = txtAuthors.Text;
            //   objStudReg.Date_received = Convert.ToDateTime(txtDateReceived.Text);
            //objStudReg.Award_details = txtDetailsAward.Text;
            //objStudReg.Amt_received = Convert.ToDouble(txtAmtReceived.Text);
            objStudReg.Month = ddlMonth.SelectedValue;
            objStudReg.Year = ddlYear.SelectedValue;
            //objStudReg.Author_role = ddlAuthor.SelectedValue;
            //objStudReg.Author_name = txtAuthorname.Text;
            objStudReg.Ua_no = Convert.ToInt32(Session["userno"]);
            objStudReg.Ip_address = ViewState["ipAddress"].ToString();
            objStudReg.College_code = Session["colcode"].ToString();

            objStudReg.ProjectFilename = "";
            objStudReg.ProjectFilePath = "";

            if (FuPub.HasFile)
            {
                Check50KBFileSize();

                if (validationflag == true)
                {
                    //to upload multiple sports docs details
                    uploadMultipleSportsCertificate(FuPub, "Publications");
                    if (status == 1)
                    {
                        CustomStatus cs = (CustomStatus)objStudRegC.AddPublicationsDetails(objStudReg);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(this.Page, "Publications Details Saved Successfully !!!", this.Page);
                            PubCancel();
                            lvAuthor.DataSource = null;
                            lvAuthor.DataBind();
                            lvAuthor.Visible = false;
                            DataSet dsPub = objStudRegC.GetStudentPublicationsDetails(objStudReg.IDNO, lblSemester.Text);
                            if (dsPub.Tables[0].Rows.Count > 0)
                            {
                                LvPublication.Visible = true;
                                LvPublication.DataSource = dsPub;
                                LvPublication.DataBind();
                            }
                            else
                            {
                                LvPublication.Visible = false;
                                LvPublication.DataSource = null;
                                LvPublication.DataBind();
                            }

                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "Publications Details Not Saved", this.Page);
                        }
                    }
                }

            }
            else
            {
                CustomStatus cs = (CustomStatus)objStudRegC.AddPublicationsDetails(objStudReg);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.Page, "Publications Details Saved Successfully !!!", this.Page);
                    PubCancel();
                    DataSet dsPub = objStudRegC.GetStudentPublicationsDetails(objStudReg.IDNO, lblSemester.Text);
                    if (dsPub.Tables[0].Rows.Count > 0)
                    {
                        LvPublication.Visible = true;
                        LvPublication.DataSource = dsPub;
                        LvPublication.DataBind();
                    }
                    else
                    {
                        LvPublication.Visible = false;
                        LvPublication.DataSource = null;
                        LvPublication.DataBind();
                    }

                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Publications Details Not Saved", this.Page);
                }
            }

            #region withoutFileUploadcode
            //CustomStatus cs = (CustomStatus)objStudRegC.AddPublicationsDetails(objStudReg);
            //if (cs.Equals(CustomStatus.RecordSaved))
            //{
            //    objCommon.DisplayMessage(this.Page, "Publications Details Saved Successfully !!!", this.Page);
            //    PubCancel();
            //    DataSet dsPub = objStudRegC.GetStudentPublicationsDetails(objStudReg.IDNO, lblSemester.Text);
            //    if (dsPub.Tables[0].Rows.Count > 0)
            //    {
            //        LvPublication.Visible = true;
            //        LvPublication.DataSource = dsPub;
            //        LvPublication.DataBind();
            //    }
            //    else
            //    {
            //        LvPublication.Visible = false;
            //        LvPublication.DataSource = null;
            //        LvPublication.DataBind();
            //    }

            //}
            //else
            //{
            //    objCommon.DisplayMessage(this.Page, "Publications Details Not Saved", this.Page);
            //}
            #endregion
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_details.btnPublications_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnBankDetails_Click(object sender, EventArgs e)
    {
        BankInsert();
    }

    private void bank()
    {
        objStudReg.IDNO = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());
        objStudReg.Bankno = Convert.ToInt32(ddlBank.SelectedValue);
        objStudReg.Acc_no = txtAcc.Text;
        objStudReg.Bank_branch = txtBankbranch.Text;
        objStudReg.Ifsc_code = txtIfsc.Text;
        CustomStatus cs = (CustomStatus)objStudRegC.AddBankDetails(objStudReg);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this.Page, "Bank Details updated Successfully !!!", this.Page);
            //  BankCancel();
            BankDetails();
            btnBankDetails.Text = "Add";
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Bank Details Not Saved", this.Page);
        }

    }

    private void BankInsert()
    {
        try
        {
            if (Session["bank"] == "0")
            {
                int idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());
                int Count = Convert.ToInt32(objCommon.LookUp("ACD_BANK B INNER JOIN ACD_STUDENT S ON (S.BANKNO=B.BANKNO) ", "B.BANKNO", "idno=" + idno));

                if (Count > 0)
                {
                    objCommon.DisplayMessage(this.Page, "Bank Details Already Exists", this.Page);

                    Session["bank"] = "1";
                    btnBankDetails.Text = "Update";
                    return;
                }
                else
                {
                    objStudReg.IDNO = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());
                    objStudReg.Bankno = Convert.ToInt32(ddlBank.SelectedValue);
                    objStudReg.Acc_no = txtAcc.Text;
                    objStudReg.Bank_branch = txtBankbranch.Text;
                    objStudReg.Ifsc_code = txtIfsc.Text;
                    CustomStatus cs = (CustomStatus)objStudRegC.AddBankDetails(objStudReg);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.Page, "Bank Details Added Successfully !!!", this.Page);
                        //BankCancel();
                        BankDetails();
                        btnBankDetails.Text = "Add";
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Bank Details Not Saved", this.Page);
                    }
                }
            }
            else
            {
                Session["bank"] = "0";
                bank();
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_details.btnBankDetails_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BankCancel()
    {
        ddlBank.SelectedIndex = 0;
        txtBankbranch.Text = string.Empty;
        txtAcc.Text = string.Empty;
        txtIfsc.Text = string.Empty;
    }
    protected void btnProjectCancel_Click(object sender, EventArgs e)
    {
        ProjectCancel();
    }
    private void ProjectCancel()
    {
        txtNameofPrjct.Text = string.Empty;
        txtprjctNameindustry.Text = string.Empty;
        txtAdresIndustry.Text = string.Empty;
        txtPrjctDetails.Text = string.Empty;
        txtPrjctduration.Text = string.Empty;
        txtGrantReceived.Text = string.Empty;
        txtProjectTodate.Text = string.Empty;
        txtProjectFromDate.Text = string.Empty;
        hdfduration.Value = " ";
        txtSupervisor.Text = string.Empty;
        txtMentor.Text = string.Empty;
        ddlProjectType.SelectedIndex = 0;
        rdbProjects.SelectedIndex = 0;
        lvProjectStatus.DataSource = null;
        lvProjectStatus.DataBind();
    }

    protected void btnInterncancel_Click(object sender, EventArgs e)
    {
        InternshipCancel();
    }
    private void InternshipCancel()
    {
        txtNameIndustry.Text = string.Empty;
        txtAddressIndustry.Text = string.Empty;
        txtDetailsInternship.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        txtDuration.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtStipend.Text = string.Empty;
        txtTPerson.Text = string.Empty;
        txtmobile.Text = string.Empty;
        txtEmailid.Text = string.Empty;
        LvIntership.DataSource = null;
        LvIntership.DataBind();
    }
    protected void btnPubCancel_Click(object sender, EventArgs e)
    {
        PubCancel();
    }
    private void PubCancel()
    {
        txtTitle.Text = string.Empty;
        txtJournalname.Text = string.Empty;
        txtVolume.Text = string.Empty;
        txtPageNo.Text = string.Empty;
        txtIssueno.Text = string.Empty;
        txtAuthors.Text = string.Empty;
        txtDateReceived.Text = string.Empty;
        ddlAuthor.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        ddlMonth.SelectedIndex = 0;
        txtAuthorname.Text = string.Empty;
        //txtDetailsAward.Text = string.Empty;
        //txtAmtReceived.Text = string.Empty;
        LvPublication.DataSource = null;
        LvPublication.DataBind();
    }

    protected void btnAchievement_Click(object sender, EventArgs e)
    {
        AchievementInsert();
    }
    private void AchievementInsert()
    {
        try
        {
            objStudReg.IDNO = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());
            objStudReg.Semesterno = Convert.ToInt32(lblSemester.Text.ToString());
            objStudReg.Certificate_name = txtAwardExamName.Text;
            objStudReg.Organization_name = txtOrganName.Text;
            objStudReg.Organization_address = txtOrganAddress.Text;
         //   objStudReg.Date_received = Convert.ToDateTime(txtDateReceived.Text);
            if (!txtDateReceived.Text.Trim().Equals(string.Empty)) objStudReg.Date_received = Convert.ToDateTime(txtDateReceived.Text);
            objStudReg.Award_details = txtdetailaward.Text; //== string.Empty ? Convert.ToDateTime(DateTime.MinValue) : Convert.ToDateTime(txtFromDate.Text);
            objStudReg.Amt_received = txtAmountReceived.Text==string.Empty?0:Convert.ToDouble(txtAmountReceived.Text); //== string.Empty ? Convert.ToDateTime(DateTime.MinValue) : Convert.ToDateTime(txtToDate.Text);
            objStudReg.Ua_no = Convert.ToInt32(Session["userno"]);
            objStudReg.Ip_address = ViewState["ipAddress"].ToString();
            objStudReg.College_code = Session["colcode"].ToString();

            objStudReg.ProjectFilename = "";
            objStudReg.ProjectFilePath = "";

            if (FuAchievements.HasFile)
            {
                Check50KBFileSize();

                if (validationflag == true)
                {
                    //to upload multiple sports docs details
                    uploadMultipleSportsCertificate(FuAchievements, "Achievement");
                    if (status == 1)
                    {
                        CustomStatus cs = (CustomStatus)objStudRegC.AddAchievementsDetails(objStudReg);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(this.Page, "Achievement Details Saved Successfully !!!", this.Page);
                            AchievementsCancel();
                            DataSet dsAchievement = objStudRegC.GetStudentAchievementDetails(objStudReg.IDNO, lblSemester.Text);
                            if (dsAchievement.Tables[0].Rows.Count > 0)
                            {
                                LvAchievements.Visible = true;
                                LvAchievements.DataSource = dsAchievement;
                                LvAchievements.DataBind();
                            }
                            else
                            {
                                LvAchievements.Visible = false;
                                LvAchievements.DataSource = null;
                                LvAchievements.DataBind();
                            }

                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "Achievements Details Not Saved", this.Page);
                        }
                    }
                }

            }
            else
            {
                CustomStatus cs = (CustomStatus)objStudRegC.AddAchievementsDetails(objStudReg);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.Page, "Achievement Details Saved Successfully !!!", this.Page);
                    AchievementsCancel();
                    DataSet dsAchievement = objStudRegC.GetStudentAchievementDetails(objStudReg.IDNO, lblSemester.Text);
                    if (dsAchievement.Tables[0].Rows.Count > 0)
                    {
                        LvAchievements.Visible = true;
                        LvAchievements.DataSource = dsAchievement;
                        LvAchievements.DataBind();
                    }
                    else
                    {
                        LvAchievements.Visible = false;
                        LvAchievements.DataSource = null;
                        LvAchievements.DataBind();
                    }

                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Achievements Details Not Saved", this.Page);
                }
            }


            #region WITHOUTFILEUPLOADCODE
            //CustomStatus cs = (CustomStatus)objStudRegC.AddAchievementsDetails(objStudReg);
            //if (cs.Equals(CustomStatus.RecordSaved))
            //{
            //    objCommon.DisplayMessage(this.Page, "Achievement Details Saved Successfully !!!", this.Page);
            //    AchievementsCancel();
            //    DataSet dsAchievement = objStudRegC.GetStudentAchievementDetails(objStudReg.IDNO, lblSemester.Text);
            //    if (dsAchievement.Tables[0].Rows.Count > 0)
            //    {
            //        LvAchievements.Visible = true;
            //        LvAchievements.DataSource = dsAchievement;
            //        LvAchievements.DataBind();
            //    }
            //    else
            //    {
            //        LvAchievements.Visible = false;
            //        LvAchievements.DataSource = null;
            //        LvAchievements.DataBind();
            //    }

            //}
            //else
            //{
            //    objCommon.DisplayMessage(this.Page, "Achievements Details Not Saved", this.Page);
            //}
            #endregion
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_details.btnAchievement_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnAchievementClear_Click(object sender, EventArgs e)
    {
        AchievementsCancel();
    }
    private void AchievementsCancel()
    {
        txtAwardExamName.Text = string.Empty;
        txtOrganName.Text = string.Empty;
        txtOrganAddress.Text = string.Empty;
        txtDateReceived.Text = string.Empty;
        txtdetailaward.Text = string.Empty;
        txtAmountReceived.Text = string.Empty;
        LvAchievements.DataSource = null;
        LvAchievements.DataBind();
    }


    protected void btnScholarship_Click(object sender, EventArgs e)
    {
        ScholarshipInsert();
    }

    private void ScholarshipInsert()
    {
        try
        {
            objStudReg.IDNO = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());
            objStudReg.Semesterno = Convert.ToInt32(lblSemester.Text.ToString());
            objStudReg.Scholarship_name = txtNameScholarship.Text;
            objStudReg.Organization_name = txtOrganizationname.Text;
           // objStudReg.Apply_date = Convert.ToDateTime(txtMoufrom.Text);
           // objStudReg.Sanction_date = Convert.ToDateTime(txtMouto.Text);            //== string.Empty ? Convert.ToDateTime(DateTime.MinValue) : Convert.ToDateTime(txtFromDate.Text);
            if (!txtMoufrom.Text.Trim().Equals(string.Empty)) objStudReg.Apply_date = Convert.ToDateTime(txtMoufrom.Text);
            if (!txtMouto.Text.Trim().Equals(string.Empty)) objStudReg.Sanction_date = Convert.ToDateTime(txtMouto.Text);
            objStudReg.Organization_type = ddlOrganization.SelectedValue;                
            objStudReg.Amt_applied = txtAmountApplied.Text==string.Empty?0: Convert.ToDouble(txtAmountApplied.Text);
            objStudReg.Amt_sanctioned = txtAmountsanctioned.Text==string.Empty?0: Convert.ToDouble(txtAmountsanctioned.Text);
            objStudReg.Payment_details = txtDetailspayment.Text;
            if (ddlBank.SelectedValue == "0")
            {
                objStudReg.Student_bank_details = txtStudentBank.Text = "";
            }
            else
            {
                objStudReg.Student_bank_details = txtStudentBank.Text;
            }
            objStudReg.Ua_no = Convert.ToInt32(Session["userno"]);
            objStudReg.Ip_address = ViewState["ipAddress"].ToString();
            objStudReg.College_code = Session["colcode"].ToString();
            if (chkfeewavier.Checked == true)
            {
                objStudReg.feewavier = 1;
            }
            else
            {
                objStudReg.feewavier = 0;
            }

            CustomStatus cs = (CustomStatus)objStudRegC.AddScholarshipDetails(objStudReg);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.Page, "Scholarship Details Saved Successfully !!!", this.Page);
                ScholarshipCancel();
                DataSet dsScholarship = objStudRegC.GetStudentScholarshipDetails(objStudReg.IDNO, lblSemester.Text);
                if (dsScholarship.Tables[0].Rows.Count > 0)
                {
                    LvScholarship.Visible = true;
                    LvScholarship.DataSource = dsScholarship;
                    LvScholarship.DataBind();
                }
                else
                {
                    LvScholarship.Visible = false;
                    LvScholarship.DataSource = null;
                    LvScholarship.DataBind();
                }

            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Scholarship Details Not Saved", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_details.btnScholarship_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnScholarshipCancel_Click(object sender, EventArgs e)
    {
        ScholarshipCancel();
        ClearAll();
        //Refresh Page url
        Response.Redirect(Request.Url.ToString());
    }

    private void ScholarshipCancel()
    {

        txtNameScholarship.Text = string.Empty;
        txtOrganizationname.Text = string.Empty;
        txtMoufrom.Text = string.Empty;
        txtMouto.Text = string.Empty;
        ddlOrganization.SelectedIndex = 0;
        txtAmountApplied.Text = string.Empty;
        txtAmountsanctioned.Text = string.Empty;
        txtDetailspayment.Text = string.Empty;
        // txtStudentBank.Text = string.Empty;
        chkfeewavier.Checked = false;

        LvScholarship.DataSource = null;
        LvScholarship.DataBind();
    }


    private void ClearAll()
    {
        txtNameofPrjct.Text = string.Empty;
        txtprjctNameindustry.Text = string.Empty;
        txtAdresIndustry.Text = string.Empty;
        txtPrjctDetails.Text = string.Empty;
        txtPrjctduration.Text = string.Empty;
        txtProjectTodate.Text = string.Empty;
        txtProjectFromDate.Text = string.Empty;
        txtGrantReceived.Text = string.Empty;
        txtSupervisor.Text = string.Empty;
        txtMentor.Text = string.Empty;
        ddlProjectType.SelectedIndex = 0;

        txtNameIndustry.Text = string.Empty;
        txtAddressIndustry.Text = string.Empty;
        txtDetailsInternship.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        txtDuration.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtStipend.Text = string.Empty;

        txtTitle.Text = string.Empty;
        txtJournalname.Text = string.Empty;
        txtVolume.Text = string.Empty;
        txtPageNo.Text = string.Empty;
        txtIssueno.Text = string.Empty;
        txtAuthors.Text = string.Empty;
        txtDateReceived.Text = string.Empty;
        //txtDetailsAward.Text = string.Empty;
        //txtAmtReceived.Text = string.Empty;

        txtAwardExamName.Text = string.Empty;
        txtOrganName.Text = string.Empty;
        txtOrganAddress.Text = string.Empty;
        txtDateReceived.Text = string.Empty;
        txtdetailaward.Text = string.Empty;
        txtAmountReceived.Text = string.Empty;

        txtNameScholarship.Text = string.Empty;
        txtOrganizationname.Text = string.Empty;
        txtMoufrom.Text = string.Empty;
        txtMouto.Text = string.Empty;
        ddlOrganization.SelectedIndex = 0;
        txtAmountApplied.Text = string.Empty;
        txtAmountsanctioned.Text = string.Empty;
        txtDetailspayment.Text = string.Empty;
        txtStudentBank.Text = string.Empty;
        chkfeewavier.Checked = false;

        BankCancel();


        lvProjectStatus.DataSource = null;
        lvProjectStatus.DataBind();
        LvIntership.DataSource = null;
        LvIntership.DataBind();
        LvPublication.DataSource = null;
        LvPublication.DataBind();
        LvAchievements.DataSource = null;
        LvAchievements.DataBind();
        LvScholarship.DataSource = null;
        LvScholarship.DataBind();
    }

    #region Upload Documents

    static bool validationflag = true;
    int status = 0;
    static int sportsno = 0;
    string Category = "";
    //function to upload single docs
    private void uploadSportsCertificate(FileUpload funame, string Category)
    {

        try
        {
            if (objStudReg.IDNO != 0)
            {
                //string folderPath = WebConfigurationManager.AppSettings["SVCE_STUDENT_CERTIFICATES"].ToString() + txtCCode.Text + "\\";
                //string folderPath = WebConfigurationManager.AppSettings["SVCE_STUDENT_CERTIFICATES"].ToString() + objStudReg.IDNO.ToString() + "-" + txtNameOfGameOrAchievement.Text + "\\";
                string folderPath = WebConfigurationManager.AppSettings["SVCE_STUDENT_CERTIFICATES"].ToString() + objStudReg.IDNO.ToString() + "-" + Category + "\\";
                if (funame.HasFile)
                {
                    string ext = System.IO.Path.GetExtension(funame.PostedFile.FileName);
                    HttpPostedFile file = funame.PostedFile;
                    string filename = Path.GetFileName(funame.PostedFile.FileName) + objStudReg.IDNO.ToString() + "-" + Category;
                    if (ext == ".pdf" || ext == ".png" || ext == ".jpeg" || ext == ".jpg" || ext == ".PNG" || ext == ".JPEG" || ext == ".JPG" || ext == ".PDF") //|| ext == ".doc" || ext == ".docx" 
                    {
                        //if (file.ContentLength <= 51200)// 31457280 before size  //For Allowing 50 Kb Size Files only 
                        //{
                        //if (file.ContentLength <= 102400)// 31457280 before size  //For Allowing 100 Kb Size Files only 
                        //{
                        if (file.ContentLength <= 1000000)// 102400 before size  //For Allowing 1 MB Size Files only 
                        {
                            string contentType = funame.PostedFile.ContentType;
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }
                            objStudReg.ProjectFilename = filename;
                            objStudReg.ProjectFilePath = folderPath + filename;
                            funame.PostedFile.SaveAs(folderPath + filename);
                            status = 1;
                        }
                        else
                        {
                            objCommon.DisplayMessage("Document size must not exceed 1 MB !", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage("Only .jpg,.png,.jpeg,.pdf file type allowed  !", this.Page);
                        return;
                    }

                }
                else
                {

                    objStudReg.ProjectFilename = "";
                    objStudReg.ProjectFilePath = "";
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Refresh the Page again!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            // objCommon.DisplayMessage(this, "Something went wrong ! Please contact Admin", this.Page);

        }
    }



    private void Check50KBFileSize()
    {
        try
        {
            if (objStudReg.IDNO != 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    //string folderPath = WebConfigurationManager.AppSettings["SVCE_STUDENT_CERTIFICATES"].ToString() + idno.ToString() + "-" + txtNameOfGameOrAchievement.Text + "\\";
                    string folderPath = WebConfigurationManager.AppSettings["SVCE_STUDENT_CERTIFICATES"].ToString() + objStudReg.IDNO.ToString() + "\\";

                    HttpPostedFile fu = Request.Files[i];
                    if (fu.ContentLength > 0)
                    {
                        string ext = System.IO.Path.GetExtension(fu.FileName);
                        string filename = Path.GetFileName(fu.FileName);
                        if (ext == ".pdf" || ext == ".png" || ext == ".jpeg" || ext == ".jpg" || ext == ".PNG" || ext == ".JPEG" || ext == ".JPG" || ext == ".PDF") //|| ext == ".doc" || ext == ".docx" 
                        {
                            //if (fu.ContentLength <= 51200)// 31457280 before size  //For Allowing 50 Kb Size Files only 
                            //{
                            //if (fu.ContentLength <= 102400)// 31457280 before size  //For Allowing 100 Kb Size Files only 
                            //{
                            if (fu.ContentLength <= 1000000)// 102400 before size  //For Allowing 1 Mb Size Files only 
                            {
                                validationflag = true;
                            }
                            else
                            {
                                objCommon.DisplayMessage("Document size must not exceed 1 MB !! Few File Size Large !", this.Page);
                                validationflag = false;
                                return;

                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage("Only .jpg,.png,.jpeg,.pdf file type allowed !", this.Page);
                            validationflag = false;
                            return;
                        }
                    }

                }
                objStudReg.ProjectFilename = objStudReg.ProjectFilename.TrimEnd(',');
                objStudReg.ProjectFilePath = objStudReg.ProjectFilePath.TrimEnd(',');
            }
            else
            {
                objCommon.DisplayMessage("Please Refresh the Page again!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            // objCommon.DisplayMessage(this, "Something went wrong ! Please contact Admin", this.Page);

        }
    }



    //function to upload multiple docs
    private void uploadMultipleSportsCertificate(FileUpload fun, string Category)
    {
        try
        {

            if (objStudReg.IDNO != 0)
            {
                string Regno = lblRegNo.Text;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    //string folderPath = WebConfigurationManager.AppSettings["SVCE_STUDENT_CERTIFICATES"].ToString() + idno.ToString() + "-" + txtNameOfGameOrAchievement.Text + "\\";
                    string folderPath = WebConfigurationManager.AppSettings["SVCE_STUDENT_CERTIFICATES"].ToString() + Regno.ToString() + "\\" + Convert.ToString(Category).ToString() + "\\";

                    HttpPostedFile fu = Request.Files[i];
                    if (fu.ContentLength > 0)
                    {
                        string ext = System.IO.Path.GetExtension(fu.FileName);
                        string filename = Path.GetFileName(fu.FileName);
                        if (ext == ".pdf" || ext == ".png" || ext == ".jpeg" || ext == ".jpg" || ext == ".PNG" || ext == ".JPEG" || ext == ".JPG" || ext == ".PDF") //|| ext == ".doc" || ext == ".docx" 
                        {
                            //if (fu.ContentLength <= 102400)// 31457280 before size  //For Allowing 100 Kb Size Files only 
                            //{
                            if (fu.ContentLength <= 1000000)// 102400 before size  //For Allowing 1 MB Size Files only 
                            {
                                string contentType = fu.ContentType;
                                if (!Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }
                                objStudReg.ProjectFilename += filename + ",";
                                objStudReg.ProjectFilePath += folderPath + filename + ",";
                                fu.SaveAs(folderPath + filename);
                                status = 1;
                            }
                            else
                            {
                                objCommon.DisplayMessage("Document size must not exceed 1 MB !", this.Page);
                                return;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage("Only .jpg,.png,.jpeg,.pdf file type allowed !", this.Page);
                            return;
                        }
                    }

                }
                objStudReg.ProjectFilename = objStudReg.ProjectFilename.TrimEnd(',');
                objStudReg.ProjectFilePath = objStudReg.ProjectFilePath.TrimEnd(',');
            }
            else
            {
                objCommon.DisplayMessage("Please Refresh the Page again!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            // objCommon.DisplayMessage(this, "Something went wrong ! Please contact Admin", this.Page);

        }
    }

    #endregion

    protected void lvProjectStatus_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            //  Label lblSPORTS_NAME = (Label)e.Item.FindControl("lblSPORTS_NAME");
            // Label lblregno = (Label)e.Item.FindControl("lblregno");
            int idno = Convert.ToInt32(lblRegNo.ToolTip);
            Label lblProjectName = (Label)e.Item.FindControl("lblProjectName");
            ListView lvProjctDetails = (ListView)e.Item.FindControl("lvProjctDetails");

            DataSet dsCERT = objStudRegC.GetStudentProjectDocs(Convert.ToInt32(lblProjectName.ToolTip));
            if (dsCERT.Tables[0].Rows.Count > 0 && dsCERT != null)
            {
                lvProjctDetails.DataSource = dsCERT.Tables[0];
                lvProjctDetails.DataBind();
            }
            else
            {
                // objCommon.DisplayMessage("Sports Certificate Not Found!!", this.Page);
                lvProjctDetails.DataSource = null;
                lvProjctDetails.DataBind();
            }
        }
    }

    private void download(object sender, string category)
    {
        LinkButton btndownloadfile = sender as LinkButton;
        string projectname = (btndownloadfile.CommandArgument);
        int idno = Convert.ToInt32(lblRegNo.ToolTip);
        //   string category = "Project";
        if (idno != 0)
        {
            string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).ToolTip.ToString();
            string ContentType = string.Empty;

            //To Get the physical Path of the file(test.txt)

            //string filepath = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + idno.ToString() + "-" + sportsname + "\\";
            // string filepath = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + objStudReg.IDNO.ToString() + "\\";
            string filepath = WebConfigurationManager.AppSettings["SVCE_STUDENT_CERTIFICATES"].ToString() + lblRegNo.Text.ToString() + "\\" + category.ToString() + "\\";

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
        else
        {
            objCommon.DisplayMessage("Please Refresh the Page again!!", this.Page);
            return;
        }
    }

    protected void btnDownloadFile_Click(object sender, EventArgs e)
    {
        download(sender, "Project");

        #region Working for Single File

        //LinkButton btndownloadfile = sender as LinkButton;
        //string projectname = (btndownloadfile.CommandArgument);
        //int idno = Convert.ToInt32(lblRegNo.ToolTip);
        //string Category = "Project";
        //if (idno != 0)
        //{
        //    string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).ToolTip.ToString();
        //    string ContentType = string.Empty;

        //    //To Get the physical Path of the file(test.txt)

        //    //string filepath = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + idno.ToString() + "-" + sportsname + "\\";
        //   // string filepath = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + objStudReg.IDNO.ToString() + "\\";
        //    string filepath = WebConfigurationManager.AppSettings["SVCE_STUDENT_CERTIFICATES"].ToString() + idno.ToString() + "\\" + Convert.ToString(Category).ToString() + "\\";

        //    // Create New instance of FileInfo class to get the properties of the file being downloaded
        //    FileInfo myfile = new FileInfo(filepath + filename);
        //    string file = filepath + filename;

        //    string ext = Path.GetExtension(filename);
        //    // Checking if file exists
        //    if (myfile.Exists)
        //    {
        //        Response.Clear();
        //        Response.ClearHeaders();
        //        Response.ContentType = "application/octet-stream";
        //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
        //        Response.TransmitFile(file);
        //        Response.Flush();
        //        Response.End();
        //    }
        //}
        //else
        //{
        //    objCommon.DisplayMessage("Please Refresh the Page again!!", this.Page);
        //    return;
        //}
        #endregion
    }


    //#region Attachments

    //private void BindListViewFiles(string PATH,ListView )
    //{
    //    try
    //    {
    //        pnlFile.Visible = true;
    //        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PATH);
    //        if (System.IO.Directory.Exists(PATH))
    //        {
    //            System.IO.FileInfo[] files = dir.GetFiles();

    //            if (Convert.ToBoolean(files.Length))
    //            {
    //                lvfile.DataSource = files;
    //                lvfile.DataBind();
    //                ViewState["FILE"] = files;
    //            }
    //            else
    //            {
    //                lvfile.DataSource = null;
    //                lvfile.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            lvfile.DataSource = null;
    //            lvfile.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Estt_complaint.BindListViewFiles-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    //protected void imgFileDownload_Click(object sender, ImageClickEventArgs e)
    //{
    //    ImageButton btn = sender as ImageButton;
    //    DownloadFile(btn.AlternateText);
    //}

    //public void DownloadFile(string filePath)
    //{
    //    try
    //    {
    //        string[] fname1 = filePath.ToString().Split('_');
    //        string filename = filePath.Substring(filePath.LastIndexOf("_") + 1);

    //        path = Docpath + "COMPLAINTS\\ComplaintsFiles\\ComplaintNo_" + fname1[1];
    //        FileStream sourceFile = new FileStream((path + "\\" + filePath), FileMode.Open);
    //        long fileSize = sourceFile.Length;
    //        byte[] getContent = new byte[(int)fileSize];
    //        sourceFile.Read(getContent, 0, (int)sourceFile.Length);
    //        sourceFile.Close();

    //        Response.Clear();
    //        Response.BinaryWrite(getContent);
    //        Response.ContentType = GetResponseType(filePath.Substring(filePath.IndexOf('.')));
    //        //Response.AddHeader("content-disposition", "attachment; filename=" + filePath);

    //        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
    //    }

    //    catch (Exception ex)
    //    {
    //        Response.Clear();
    //        Response.ContentType = "text/html";
    //        Response.Write("Unable to download the attachment.");
    //    }
    //}

    //private string GetResponseType(string fileExtension)
    //{
    //    switch (fileExtension.ToLower())
    //    {
    //        case ".doc":
    //            return "application/vnd.ms-word";
    //            break;

    //        case ".docx":
    //            return "application/vnd.ms-word";
    //            break;

    //        case ".xls":
    //            return "application/ms-excel";
    //            break;

    //        case ".xlsx":
    //            return "application/ms-excel";
    //            break;

    //        case ".pdf":
    //            return "application/pdf";
    //            break;

    //        case ".ppt":
    //            return "application/vnd.ms-powerpoint";
    //            break;

    //        case ".txt":
    //            return "text/plain";
    //            break;

    //        case ".jpg":
    //            return "application/{0}";
    //            break;
    //        case ".jpeg":
    //            return "application/{0}";
    //            break;
    //        case "":
    //            return "";
    //            break;

    //        default:
    //            return "";
    //            break;
    //    }
    //}

    //protected string GetFileName(object obj)
    //{
    //    string f_name = string.Empty;
    //    string[] fname = obj.ToString().Split('_');

    //    if (fname[0] == "ComplaintNo")
    //        f_name = Convert.ToString(fname[2]);

    //    if (fname[0] == "judDoc")
    //        f_name = Convert.ToString(fname[3]);

    //    return f_name;
    //}

    //protected string GetFileNameCaseNo(object obj)
    //{
    //    string f_name = string.Empty;

    //    string[] fname = obj.ToString().Split('_');

    //    if (fname[0] == "ComplaintNo")
    //        f_name = Convert.ToString(fname[1]);
    //    f_name = f_name.Replace('-', '/');
    //    if (fname[0] == "judDoc")
    //        f_name = Convert.ToString(fname[3]);
    //    return f_name;
    //}

    //protected string GetFileDate(object obj)
    //{
    //    string file_path = Convert.ToString(ViewState["FILE_PATH"] + "\\" + obj.ToString());
    //    FileInfo fileInfo = new FileInfo(file_path);

    //    DateTime creationTime = DateTime.MinValue;
    //    creationTime = fileInfo.CreationTime;

    //    string f_date = string.Empty;
    //    f_date = creationTime.ToString("dd-MMM-yyyy");
    //    return f_date;
    //}
    //#endregion

    void pubAuthorInsert()
    {
        try
        {
            objStudReg.IDNO = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());
            objStudReg.Semesterno = Convert.ToInt32(lblSemester.Text.ToString());
            objStudReg.Author_role = ddlAuthor.SelectedValue;
            objStudReg.Author_name = txtAuthorname.Text;
            objStudReg.Ua_no = Convert.ToInt32(Session["userno"]);
            objStudReg.Ip_address = ViewState["ipAddress"].ToString();
            objStudReg.College_code = Session["colcode"].ToString();


            CustomStatus cs = (CustomStatus)objStudRegC.AddPublicatonAuthorDetails(objStudReg);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.Page, "Publications Author Details Saved Successfully !!!", this.Page);
                PubAuthorCancel();
                lvAuthor.Items.Clear();
                DataSet dsAuthor = objStudRegC.GetStudentPublicationsAuthorDetails(objStudReg.IDNO, lblSemester.Text);
                if (dsAuthor.Tables[0].Rows.Count > 0)
                {
                    lvAuthor.Visible = true;
                    lvAuthor.DataSource = dsAuthor;
                    lvAuthor.DataBind();
                }
                else
                {
                    lvAuthor.Visible = false;
                    lvAuthor.DataSource = null;
                    lvAuthor.DataBind();
                }

            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Publications Author Details Not Saved", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_details.btnAddAuthor_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void PubAuthorCancel()
    {
        ddlAuthor.SelectedIndex = 0;
        txtAuthorname.Text = string.Empty;
    }


    protected void btnAddAuthor_Click(object sender, EventArgs e)
    {
        if (txtTitle.Text == "")
        {
            objCommon.DisplayMessage(this.Page, "Please Fill All the Fields Before Adding Author Details", this.Page);
            txtTitle.Focus();
            return;
        }
        else
        {
            pubAuthorInsert();
        }
    }
    protected void btnDownloadIntern_Click(object sender, EventArgs e)
    {
        download(sender, "Internship");
    }
    protected void LvIntership_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            //  Label lblSPORTS_NAME = (Label)e.Item.FindControl("lblSPORTS_NAME");
            // Label lblregno = (Label)e.Item.FindControl("lblregno");
            int idno = Convert.ToInt32(lblRegNo.ToolTip);
            Label lblIntern = (Label)e.Item.FindControl("lblIntern");
            ListView LvInternshipCert = (ListView)e.Item.FindControl("LvInternshipCert");

            DataSet dsINTERNCERT = objStudRegC.GetStudentInternshipDocs(Convert.ToInt32(lblIntern.ToolTip));
            if (dsINTERNCERT.Tables[0].Rows.Count > 0 && dsINTERNCERT != null)
            {
                LvInternshipCert.DataSource = dsINTERNCERT.Tables[0];
                LvInternshipCert.DataBind();
            }
            else
            {
                // objCommon.DisplayMessage("Sports Certificate Not Found!!", this.Page);
                LvInternshipCert.DataSource = null;
                LvInternshipCert.DataBind();
            }
        }
    }

    protected void LvAchievements_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            //  Label lblSPORTS_NAME = (Label)e.Item.FindControl("lblSPORTS_NAME");
            // Label lblregno = (Label)e.Item.FindControl("lblregno");
            int idno = Convert.ToInt32(lblRegNo.ToolTip);
            Label lblAchievement = (Label)e.Item.FindControl("lblAchievement");
            ListView LvAchievementCert = (ListView)e.Item.FindControl("LvAchievementCert");

            DataSet dsACHIEVEMENTCERT = objStudRegC.GetStudentAchievementsDocs(Convert.ToInt32(lblAchievement.ToolTip));
            if (dsACHIEVEMENTCERT.Tables[0].Rows.Count > 0 && dsACHIEVEMENTCERT != null)
            {
                LvAchievementCert.DataSource = dsACHIEVEMENTCERT.Tables[0];
                LvAchievementCert.DataBind();
            }
            else
            {
                // objCommon.DisplayMessage("Sports Certificate Not Found!!", this.Page);
                LvAchievementCert.DataSource = null;
                LvAchievementCert.DataBind();
            }
        }
    }
    protected void btnDownloadAchievement_Click(object sender, EventArgs e)
    {
        download(sender, "Achievement");
    }


    protected void LvPublication_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            //  Label lblSPORTS_NAME = (Label)e.Item.FindControl("lblSPORTS_NAME");
            // Label lblregno = (Label)e.Item.FindControl("lblregno");
            int idno = Convert.ToInt32(lblRegNo.ToolTip);
            Label lblTitle = (Label)e.Item.FindControl("lblTitle");

            ListView LvPublicationCert = (ListView)e.Item.FindControl("LvPublicationCert");

            DataSet dsPUBLICATIONCERT = objStudRegC.GetStudentPublicationDocs(Convert.ToInt32(lblTitle.ToolTip));
            if (dsPUBLICATIONCERT.Tables[0].Rows.Count > 0 && dsPUBLICATIONCERT != null)
            {
                LvPublicationCert.DataSource = dsPUBLICATIONCERT.Tables[0];
                LvPublicationCert.DataBind();
            }
            else
            {
                // objCommon.DisplayMessage("Sports Certificate Not Found!!", this.Page);
                LvPublicationCert.DataSource = null;
                LvPublicationCert.DataBind();
            }
        }
    }
    protected void btnDownloadPublication_Click(object sender, EventArgs e)
    {
        download(sender, "Publications");
    }
    private void Delete(object sender, string category, int DocNo)
    {
        int idno = Convert.ToInt32(lblRegNo.ToolTip);
        int ret = 0;
        try
        {
            if (idno != 0)
            {

                ImageButton btnDelete = sender as ImageButton;
                string filename = btnDelete.AlternateText;

                string path = WebConfigurationManager.AppSettings["SVCE_STUDENT_CERTIFICATES"].ToString() + lblRegNo.Text.ToString() + "\\" + category.ToString() + "\\";
                string DOCS_NO = string.Empty;

                if (File.Exists(path + filename))
                {
                    File.Delete(path + filename);

                    if (category == "Project")
                    {
                        ret = objStudRegC.DeleteProjectDocs(DocNo);
                    }

                    else if (category == "Internship")
                    {
                        ret = objStudRegC.DeleteInternshipDocs(DocNo);
                    }
                    else if (category == "Achievement")
                    {
                        ret = objStudRegC.DeleteAchievementsCertificateDocs(DocNo);
                    }
                    else
                    {
                        ret = objStudRegC.DeletePublicationsDocs(DocNo);
                    }
                    if (ret == 3)
                    {
                        objCommon.DisplayMessage(this, "File Deleted Successfully", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "File Not Found to Delete", this.Page);
                    }
                }

            }
            else
            {
                objCommon.DisplayMessage("Please Refresh the Page again!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "domain.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        Delete(sender, "Project", Convert.ToInt32(btn.CommandArgument));
        objStudReg.IDNO = Convert.ToInt32(lblRegNo.ToolTip);
        DataSet dsProject = objStudRegC.GetStudentProjectDetails(objStudReg.IDNO, lblSemester.Text);
        if (dsProject.Tables[0].Rows.Count > 0)
        {
            divinfo.Visible = true;
            lvProjectStatus.Visible = true;

            lvProjectStatus.DataSource = dsProject;
            lvProjectStatus.DataBind();
        }
        else
        {
            lvProjectStatus.Visible = false;
            lvProjectStatus.DataSource = null;
            lvProjectStatus.DataBind();
        }
    }

    protected void btnInternDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        Delete(sender, "Internship", Convert.ToInt32(btn.CommandArgument));
        objStudReg.IDNO = Convert.ToInt32(lblRegNo.ToolTip);
        DataSet dsInternship = objStudRegC.GetStudentInternshipDetails(objStudReg.IDNO, lblSemester.Text);
        if (dsInternship.Tables[0].Rows.Count > 0)
        {
            LvIntership.Visible = true;
            LvIntership.DataSource = dsInternship;
            LvIntership.DataBind();

        }
        else
        {
            LvIntership.Visible = false;
            LvIntership.DataSource = null;
            LvIntership.DataBind();
        }

    }
    protected void btnAchievementDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        Delete(sender, "Achievement", Convert.ToInt32(btn.CommandArgument));
        objStudReg.IDNO = Convert.ToInt32(lblRegNo.ToolTip);
        DataSet dsAchievement = objStudRegC.GetStudentAchievementDetails(objStudReg.IDNO, lblSemester.Text);
        if (dsAchievement.Tables[0].Rows.Count > 0)
        {
            LvAchievements.Visible = true;
            LvAchievements.DataSource = dsAchievement;
            LvAchievements.DataBind();
        }
        else
        {
            LvAchievements.Visible = false;
            LvAchievements.DataSource = null;
            LvAchievements.DataBind();
        }
    }
    protected void btnPublicationsDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        Delete(sender, "Publications", Convert.ToInt32(btn.CommandArgument));
        objStudReg.IDNO = Convert.ToInt32(lblRegNo.ToolTip);
        DataSet dsPub = objStudRegC.GetStudentPublicationsDetails(objStudReg.IDNO, lblSemester.Text);
        if (dsPub.Tables[0].Rows.Count > 0)
        {
            LvPublication.Visible = true;
            LvPublication.DataSource = dsPub;
            LvPublication.DataBind();
        }
        else
        {
            LvPublication.Visible = false;
            LvPublication.DataSource = null;
            LvPublication.DataBind();
        }
    }
}
