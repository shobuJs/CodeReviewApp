//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC 
// PAGE NAME     : PHD COURSE REGISTRATION                                      
// CREATION DATE : 19-APRIL-2013 
// ADDED BY      : ASHISH DHAKATE                                                
// ADDED DATE    : 
// MODIFIED BY   : 
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

public partial class ACADEMIC_CourseRegistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    int retCnt;

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
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

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
                ////Page Authorization
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                this.PopulateDropDownList();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
        }

        divCourses.Visible = true;

        divMsg.InnerHtml = string.Empty;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.ShowDetails();
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (Convert.ToInt32(ddlOfferedCourse.SelectedValue) > 0 && Convert.ToInt32(ddlOfferedCourse.SelectedValue) > 1)
        {


            //Add registered 
            StudentRegist objSR = new StudentRegist();
            objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objSR.IDNO = Convert.ToInt32(lblName.ToolTip);
            if (rbYes.Checked == true)
                objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip) + 1;
            else
                objSR.SEMESTERNO = 0;
            objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);

            objSR.IPADDRESS = ViewState["ipAddress"].ToString();
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.REGNO = txtRollNo.Text.Trim();
            objSR.ROLLNO = txtRollNo.Text.Trim();
            objSR.COURSENOS = ddlOfferedCourse.SelectedValue;
            double credits = Convert.ToDouble(objCommon.LookUp("ACD_COURSE", "CREDITS", "COURSENO=" + ddlOfferedCourse.SelectedValue));
            objSR.CREDITS = Convert.ToInt32(credits);



            int ret = objSReg.AddAddlRegisteredSubjectsPhd(objSR);

            if (ret > 0)
            {
                //SEMESTER PROMOTION FOR SELECTED STUDENT
                if (rbYes.Checked == true)
                {
                    int currentSemesterNo = int.Parse(lblSemester.ToolTip) + 1;
                }
                objCommon.DisplayMessage("Course Registration is successfull.", this.Page);

            }
            ShowDetails();
        }

        else
        {
            StudentRegist objSR = new StudentRegist();
            //Add the new course for the Phd 

            if (Convert.ToInt32(ddlSelectCourse.SelectedValue) > 0)
            {
                string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlSelectCourse.SelectedValue);
                int cnt = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "COUNT(COURSENO)", "CCODE='" + ccode + "' AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip)));
                if (cnt > 0)
                {
                    objCommon.DisplayMessage("Already present this course", this.Page);

                }
                else
                {
                    objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
                    objSR.COURSENOS = ddlSelectCourse.SelectedValue;

                    int retstatus = objSReg.AddAddCoursesForPhd(objSR);

                    //Add registered 

                    objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                    objSR.IDNO = Convert.ToInt32(lblName.ToolTip);
                    if (rbYes.Checked == true)
                        objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip) + 1;
                    else
                        objSR.SEMESTERNO = 0;
                    objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);

                    objSR.IPADDRESS = ViewState["ipAddress"].ToString();
                    objSR.COLLEGE_CODE = Session["colcode"].ToString();
                    objSR.REGNO = txtRollNo.Text.Trim();
                    objSR.ROLLNO = txtRollNo.Text.Trim();
                    string courseno = objCommon.LookUp("ACD_COURSE", "MAX(COURSENO)", "");
                    objSR.COURSENOS = courseno;
                    double credits = Convert.ToDouble(objCommon.LookUp("ACD_COURSE", "CREDITS", "COURSENO=" + courseno));
                    objSR.CREDITS = Convert.ToInt32(credits);



                    int ret = objSReg.AddAddlRegisteredSubjectsPhd(objSR);

                    if (ret > 0)
                    {
                        //SEMESTER PROMOTION FOR SELECTED STUDENT
                        if (rbYes.Checked == true)
                        {
                            int currentSemesterNo = int.Parse(lblSemester.ToolTip) + 1;
                        }


                        objCommon.DisplayMessage("Course Registration is successfull.", this.Page);
                    }
                    ShowDetails();
                }

            }
        }
    }

    #region
    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlPayType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO > 0", "PAYTYPENO");
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CourseRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CourseRegistration.aspx");
        }
    }
    private void ShowDetails()
    {
        try
        {
            if (txtRollNo.Text.Trim() == string.Empty || Convert.ToInt32(ddlSession.SelectedValue) == 0)
            {
                objCommon.DisplayMessage("Please Enter Student Roll No. and Please Select Session", this.Page);
                return;
            }

            string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");
            string sessionno = ddlSession.SelectedValue;

            if (string.IsNullOrEmpty(idno))
            {
                objCommon.DisplayMessage("Student with Roll No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                return;
            }
            string branchno = objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + idno);
            string deptno = objCommon.LookUp("ACD_BRANCH", "DEPTNO", "BRANCHNO=" + branchno);
            int hoddeptno = Convert.ToInt32(Session["userdeptno"]);

            if (Convert.ToInt32(deptno) == hoddeptno || hoddeptno == 0)
            {

                DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO)", "S.IDNO,DG.DEGREENAME", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE S.PHYSICALLY_HANDICAPPED WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH", "S.IDNO = " + idno, string.Empty);
                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        //Show Student Details..
                        lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                        lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                        lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";


                        lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["ROLLNO"].ToString();
                        lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                        lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                        lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                        lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                        lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                        lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                        ddlSemester.SelectedValue = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();

                        lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                        lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();

                        //Payment Type..
                        ddlPayType.SelectedValue = dsStudent.Tables[0].Rows[0]["PTYPE"].ToString();

                        //physically hadicapped
                        lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();

                        string fees_amt = string.Empty;
                        if (!string.IsNullOrEmpty(dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString()) &&
                            !string.IsNullOrEmpty(dsStudent.Tables[0].Rows[0]["PTYPE"].ToString()))
                        {
                            string semesterNo = string.Empty;
                            semesterNo = lblSemester.ToolTip;

                            fees_amt = objCommon.LookUp("ACD_STANDARD_FEES", "ISNULL(SUM(ISNULL(SEMESTER" + semesterNo + ",0)),0)", "BATCHNO = " + lblAdmBatch.ToolTip + " AND DEGREENO = " + dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND PAYTYPENO = " + dsStudent.Tables[0].Rows[0]["PTYPE"].ToString() + " AND RECIEPT_CODE = 'TF'");
                            if (fees_amt == "0")
                                btnSubmit.Enabled = false;
                            else
                                btnSubmit.Enabled = true;
                        }
                        else
                        {
                            btnSubmit.Enabled = false;
                            objCommon.DisplayMessage("Please Update the Admission Batch and Payment Type Category.", this.Page);
                        }

                        lblFeeAmount.Text = fees_amt;

                        tblInfo.Visible = true;

                        //Show Already saved in  Course table courses..

                        objCommon.FillDropDownList(ddlOfferedCourse, "ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) LEFT OUTER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.IDNO = " + idno + ")", "DISTINCT C.COURSENO", "(C.CCODE +'-'+C.COURSE_NAME) AS COURSE_NAME", "C.SCHEMENO = " + lblScheme.ToolTip + " AND C.ELECT=0 ", "C.COURSENO");

                        //Show History of Courses..
                        DataSet dsHistCourses = objCommon.FillDropDown("ACD_STUDENT_RESULT R INNER JOIN ACD_SEMESTER S ON (R.SEMESTERNO = S.SEMESTERNO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = R.SESSIONNO)", "SM.SESSIONNO,R.SEMESTERNO,R.IDNO,R.SCHEMENO", "SM.SESSION_NAME,S.SEMESTERNAME,R.CCODE,R.COURSENAME,R.GRADE,R.CREDITS", "R.IDNO = " + idno, "R.SESSIONNO,R.SEMESTERNO,R.CCODE");
                        lvHistory.DataSource = dsHistCourses.Tables[0];
                        lvHistory.DataBind();

                        divCourses.Visible = true;

                        //check the previous semester n-4 RULE for course registration for B.Tech and B.Arch..
                        string degreeno = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();

                        int dcrcnt = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(*)", "SESSIONNO= " + Convert.ToInt32(ddlSession.SelectedValue) + " AND IDNO=" + Convert.ToInt32(idno)));

                        if (dcrcnt == 0)
                        {
                            retCnt = objSReg.CheckN4Rule(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(degreeno));
                        }

                    }
                    else
                    {
                        objCommon.DisplayMessage("Student with Roll No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                        divCourses.Visible = false;
                        return;

                    }
                }
            }
            else
            {
                objCommon.DisplayMessage("Student with Roll No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                divCourses.Visible = false;
                return;
            }

            //Check the fees collect or not

            string dcr = objCommon.LookUp("ACD_DCR", "COUNT(*)", "SESSIONNO= " + ddlSession.SelectedValue + " AND IDNO=" + Convert.ToInt32(idno));

            if (Convert.ToInt32(dcr) > 0)
            {
                btnSubmit.Enabled = true;
            }
            else
            {
                btnSubmit.Enabled = false;

            }


            if (retCnt > 0)
            {
                btnSubmit.Enabled = false;
                btnPrintRegSlip.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private FeeDemand GetDemandCriteria()
    {
        FeeDemand demandCriteria = new FeeDemand();
        try
        {
            demandCriteria.SessionNo = int.Parse(ddlSession.SelectedValue);
            demandCriteria.ReceiptTypeCode = "TF";
            demandCriteria.BranchNo = int.Parse(lblBranch.ToolTip);
            demandCriteria.SemesterNo = int.Parse(lblSemester.ToolTip) + 1;
            demandCriteria.PaymentTypeNo = int.Parse(ddlPayType.SelectedValue);
            demandCriteria.UserNo = int.Parse(Session["userno"].ToString());
            demandCriteria.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return demandCriteria;
    }

    private FeeDemand GetDcrCriteria()
    {
        FeeDemand dcrCriteria = new FeeDemand();
        try
        {
            dcrCriteria.SessionNo = int.Parse(ddlSession.SelectedValue);
            dcrCriteria.ReceiptTypeCode = "TF";
            dcrCriteria.BranchNo = int.Parse(lblBranch.ToolTip);
            dcrCriteria.SemesterNo = int.Parse(lblSemester.ToolTip) + 1;
            dcrCriteria.PaymentTypeNo = int.Parse(ddlPayType.SelectedValue);
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
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    protected void btnModifyPType_Click(object sender, EventArgs e)
    {
        try
        {
            objSReg.UpdatePaymentCategory(lblName.ToolTip, ddlPayType.SelectedValue, ddlSemester.SelectedValue);
            objCommon.DisplayMessage("Payment Category Updated Successfully!", this.Page);
            this.ShowDetails();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.btnModifyPType_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnPrintChallan_Click(object sender, EventArgs e)
    {
        try
        {

            //Create Demand and Print the Challan..
            DemandModificationController dmController = new DemandModificationController();
            FeeDemand demandCriteria = this.GetDemandCriteria();
            int selectSemesterNo = Int32.Parse(lblSemester.ToolTip);
            string studentIDs = lblName.ToolTip;

            // Check Condition Odd Even 
            string odd_even = objCommon.LookUp("ACD_SEMESTER", "ODD_EVEN", "SEMESTERNO=" + selectSemesterNo);
            string cur_odd_even = objCommon.LookUp("ACD_SESSION_MASTER", "ODD_EVEN", "SESSIONNO=" + Convert.ToInt32(Session["currentsession"]));
            if (odd_even == cur_odd_even)
            {
                objCommon.DisplayMessage("Student does not allow to Promote semester!!", this.Page);
                return;
            }

            bool overwriteDemand = true;
            //SEMESTER PROMOTION FOR SELECTED STUDENT
            int currentSemesterNo = selectSemesterNo + 1;
            objSReg.UpdateSemesterPromotionNo(studentIDs, currentSemesterNo);


            string demandno = objCommon.LookUp("ACD_DEMAND", "COUNT(*)", "IDNO=" + studentIDs.ToString() + " AND SEMESTERNO=" + currentSemesterNo);
            if (Convert.ToInt32(demandno) <= 0)
            {
                string response = dmController.CreateDemandForStudents(studentIDs, demandCriteria, selectSemesterNo, overwriteDemand);
            }

            //Create DCR and print Challan
            string receiptno = this.GetNewReceiptNo();
            FeeDemand dcr = this.GetDcrCriteria();
            string dcritem = dmController.CreateDcrForStudents(studentIDs, dcr, selectSemesterNo, overwriteDemand, receiptno);


            //Print Challan..

            string dcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(studentIDs) + " AND SEMESTERNO=" + currentSemesterNo);

            if (dcrNo != string.Empty && studentIDs != string.Empty)
            {
                this.ShowReport("FeeCollectionReceiptForCourseRegister1.rpt", Convert.ToInt32(dcrNo), Convert.ToInt32(studentIDs), "1");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_CourseRegistration.btnPrintChallan_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void lbReport_Click(object sender, EventArgs e)
    {
        //Show Tabulation Sheet
        LinkButton btn = sender as LinkButton;
        string sessionNo = (btn.Parent.FindControl("hdfSession") as HiddenField).Value;
        string semesterNo = (btn.Parent.FindControl("hdfSemester") as HiddenField).Value;
        string schemeNo = (btn.Parent.FindControl("hdfScheme") as HiddenField).Value;
        string IdNo = (btn.Parent.FindControl("hdfIDNo") as HiddenField).Value;

        this.ShowTRReport("Tabulation_Sheet", "rptTabulationRegistar.rpt", sessionNo, schemeNo, semesterNo, IdNo);
    }

    private void ShowReport(string rptName, int dcrNo, int studentNo, string copyNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
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

        string param = "@P_IDNO=" + studentNo.ToString() + ",@P_DCRNO=" + dcrNo + ",CopyNo=" + copyNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;
    }

    //get the new receipt No.
    private string GetNewReceiptNo()
    {
        string receiptNo = string.Empty;

        try
        {
            string demandno = objCommon.LookUp("ACD_DEMAND", "MAX(DM_NO)", "");
            DataSet ds = feeController.GetNewReceiptData("B", Int32.Parse(Session["userno"].ToString()), "TF");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString());
                receiptNo = dr["PRINTNAME"].ToString() + "/" + "B" + "/" + DateTime.Today.Year.ToString().Substring(2, 2) + "/" + dr["FIELD"].ToString() + demandno;

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

    private void ShowReport(string reportTitle, string rptFileName)
    {
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int idno = Convert.ToInt32(lblName.ToolTip);
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@UserName=" + Session["username"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void rbYes_CheckedChanged(object sender, EventArgs e)
    {

        //SEMESTER PROMOTION FOR SELECTED STUDENT
        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");
        int currentSemesterNo = Int32.Parse(lblSemester.ToolTip) + 1;
        lblSemester.Text = objCommon.LookUp("ACD_SEMESTER", "SEMESTERNAME", "SEMESTERNO=" + currentSemesterNo);


        DataSet dsCurrCourses = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) LEFT OUTER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.IDNO = " + idno + ")", "DISTINCT SR.IDNO,C.COURSENO", "C.CCODE,C.COURSE_NAME,C.SUBID,C.ELECT,C.CREDITS,S.SUBNAME", "C.SCHEMENO = " + lblScheme.ToolTip + " AND C.ELECT=0 AND C.SEMESTERNO = " + currentSemesterNo, "C.CCODE");
        btnSubmit.Enabled = true;
        rbNo.Enabled = false;

    }
    protected void btnPrePrintClallan_Click(object sender, EventArgs e)
    {
        string studentIDs = lblName.ToolTip;
        int selectSemesterNo = Int32.Parse(lblSemester.ToolTip);

        string dcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(studentIDs) + " AND SEMESTERNO=" + selectSemesterNo);

        if (dcrNo != string.Empty && studentIDs != string.Empty)
        {
            this.ShowReport("FeeCollectionReceiptForCourseRegister1.rpt", Convert.ToInt32(dcrNo), Convert.ToInt32(studentIDs), "1");
        }
    }
    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        ShowReport("PhdRegistrationSlip", "rptPhdPreRegslip.rpt");
    }


    protected void ddlOfferedCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        int deptno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DEPTNO", "SCHEMENO=" + lblScheme.ToolTip));
        int semesterno = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "MAX(SEMESTERNO)", "BOS_DEPTNO=" + deptno));

        if (Convert.ToInt32(ddlOfferedCourse.SelectedValue) == 1)
        {
            trDeptCourse.Visible = true;
            objCommon.FillDropDownList(ddlSelectCourse, "ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) LEFT OUTER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO AND SR.SESSIONNO = " + ddlSession.SelectedValue + ")", "DISTINCT C.COURSENO", "(C.CCODE +'-'+C.COURSE_NAME) AS COURSE_NAME", "C.BOS_DEPTNO=" + deptno + " AND C.SEMESTERNO=" + semesterno + " AND C.ELECT=0 ", "C.COURSENO");
        }
        else
        {
            trDeptCourse.Visible = false;
        }

        if (Convert.ToInt32(ddlOfferedCourse.SelectedValue) == 2)
        {
            objCommon.FillDropDownList(ddlSubjectType, "ACD_SUBJECTTYPE", "SUBID", "SUBNAME", "SUBID < 3", "SUBID");
            //FILL PARENT DEPARTMENT LIST
            objCommon.FillDropDownList(ddlBosDept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO <>0 AND DEPTNO IS NOT NULL ", "DEPTNAME");
            trAddCourse.Visible = true;
        }
        else
        {
            trAddCourse.Visible = false;
        }

    }


    protected void btnLock_Click(object sender, EventArgs e)
    {
    }


    protected void btnSub_Click(object sender, EventArgs e)
    {
        try
        {
            CourseController objCourse = new CourseController();
            Course objc = new Course();
            objc.CCode = txtCCode.Text.Trim();
            objc.CourseName = txtCourseName.Text.Replace("'", "").Trim();
            objc.SchemeNo = Convert.ToInt32(lblScheme.ToolTip);
            objc.SubID = Convert.ToInt32(ddlSubjectType.SelectedValue);
            objc.CollegeCode = Session["colcode"].ToString();
            objc.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            objc.Deptno = Convert.ToInt32(ddlBosDept.SelectedValue);
            objc.Credits = Convert.ToInt32(txtCredit.Text);

            CustomStatus cs = (CustomStatus)objCourse.AddNewPhdCourse(objc);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage("Course Added Successfully!!", this.Page);
                clear();

            }
            else
            {
                objCommon.DisplayMessage("Error!!", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_courseMaster.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void clear()
    {
        tblInfo.Visible = false;
        trAddCourse.Visible = false;
        txtRollNo.Text = string.Empty;
        txtCCode.Text = string.Empty;
        txtCourseName.Text = string.Empty;
        ddlBosDept.SelectedIndex = 0;
        ddlSubjectType.SelectedIndex = 0;
        txtCredit.Text = string.Empty;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}


