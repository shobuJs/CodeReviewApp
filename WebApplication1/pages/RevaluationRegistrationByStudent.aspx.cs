//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : REVALUATION REGISTRATION BY STUDENT                                      
// CREATION DATE : 26-02-2013
// ADDED BY      : SANJAY S RATNAPARKHI                                             
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

public partial class ACADEMIC_RevaluationRegistrationByStudent : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();

    string revalday = System.Configuration.ConfigurationManager.AppSettings["revaluation"].ToString();

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

                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]));
                ViewState["usertype"] = ua_type;

                if (ViewState["usertype"].ToString() == "2")
                {
                    divCourses.Visible = false;

                }
                else
                {
                }

                btnSubmit.Visible = false;
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }

        }

        divMsg.InnerHtml = string.Empty;
    }

    private void CheckActivity()
    {
        string sessionno = string.Empty;
        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.ACTIVITY_CODE = 'EXAMREG'");

        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                pnlStart.Visible = false;

            }

            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                pnlStart.Visible = false;
            }

        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            pnlStart.Visible = false;
        }
        dtr.Close();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int idno;
            StudentRegistration objSRegist = new StudentRegistration();
            StudentRegist objSR = new StudentRegist();

            if (ViewState["usertype"].ToString() == "2")
            {
                idno = Convert.ToInt32(Session["idno"]);
            }
            else
            {
                idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'"));
            }


            objSR.SESSIONNO = Convert.ToInt32(Session["currentsession"]) - 1;
            objSR.IDNO = idno;

            objSR.SEMESTERNO = Convert.ToInt32(ddlBackLogSem.SelectedValue);
            objSR.IPADDRESS = ViewState["ipAddress"].ToString();
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.UA_NO = Convert.ToInt32(Session["userno"]);

            objSR.COURSENOS = string.Empty;
            int status = 0;
            if (lvFailCourse.Items.Count > 0)
            {
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    if (chk.Checked == true)
                        status++;
                }
            }
            else
            {
                status = -1;
            }
            int noOfSub = 0;

            if (status > 0)
            {
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    //Get Student Details from lvStudent
                    CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                    if (cbRow.Checked == true)
                    {
                        objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + "$";
                        objSR.EXTERMARKS += ((dataitem.FindControl("lblExtermark")) as Label).Text + "$";
                        noOfSub++;
                    }

                    objSR.SCHEMENO = Convert.ToInt32((dataitem.FindControl("lblSEMSCHNO") as Label).ToolTip);
                }
                if (noOfSub > 2)
                {
                    objCommon.DisplayMessage("Only 2 subjects will be allow for revaluation process", this.Page);
                    return;
                }
                CustomStatus cs = (CustomStatus)objSRegist.AddRevalautionRegisteredSubjects(objSR);

                if (cs == CustomStatus.RecordSaved)
                {
                    objCommon.DisplayMessage("Student Revaluation Registration Successfully!!", this.Page);
                    ShowReport("RevaluationRegistrationSlip", "rptRevaluation.rpt");

                }
            }

            if (status > 0)
            {

                int branchno = Convert.ToInt32(lblBranch.ToolTip);
                int admbatch = Convert.ToInt32(lblAdmBatch.ToolTip);
                int degreeno = Convert.ToInt32(hdfDegreeno.Value);
                int categoryno = Convert.ToInt32(hdfCategory.Value);
                if (categoryno == 0)
                {
                    categoryno = 4;
                }
                int semesterno = Convert.ToInt32(lblSemester.ToolTip);
                double ExamAmt = Convert.ToDouble(200 * noOfSub);
                int studentIDs = idno;
                bool overwriteDemand = false;

                string receiptno = this.GetNewReceiptNo();
                FeeDemand dcr = this.GetDcrCriteria();
                string dcritem = string.Empty;
                double CalLateExmAmt = 0;
                dcritem = dmController.CreateDcrForBacklogStudents(studentIDs, dcr, Convert.ToInt32(ddlBackLogSem.SelectedValue), overwriteDemand, receiptno, ExamAmt, CalLateExmAmt);

                if (dcritem != "-99")
                {
                    objCommon.DisplayMessage("Record Saved Successfully", this.Page);
                    ddlBackLogSem.SelectedIndex = 0;
                    lvFailCourse.DataSource = null;
                    lvFailCourse.DataBind();
                    btnSubmit.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            if (ViewState["usertype"].ToString() == "1")
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_RevaluationRegistrationByStudent.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server Unavailable.");
            }
            else
            {
                objCommon.DisplayMessage("Transaction Failed...", this.Page);
                return;
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RevaluationRegistrationByStudent.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RevaluationRegistrationByStudent.aspx");
        }
    }

    private void ShowDetails()
    {
        string idno;
        int sessionno = Convert.ToInt32(Session["currentsession"]) - 1;
        StudentController objSC = new StudentController();

        try
        {

            if (txtRollNo.Text.Trim() == string.Empty)
            {
                objCommon.DisplayMessage("Please Enter Student Roll No.", this.Page);
                divCourses.Visible = false;
                return;
            }

            idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");

            if (string.IsNullOrEmpty(idno))
            {
                objCommon.DisplayMessage("Student with Roll No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                divCourses.Visible = false;
                return;
            }

            if (Convert.ToInt32(idno) > 0)
            {
                DataSet dsStudent = objSC.GetStudentDetailsExam(Convert.ToInt32(idno));

                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                        lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                        lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";

                        lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["ENROLLNO"].ToString();
                        lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                        lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                        lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                        lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                        lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                        lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                        lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                        lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                        hdfCategory.Value = dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                        hdfDegreeno.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();

                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";

                        objCommon.FillDropDownList(ddlBackLogSem, "ACD_TRRESULT T", "SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTERNAME", "IDNO = " + idno + " AND  SESSIONNO =" + sessionno, "SEMESTERNO");


                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_RevaluationRegistrationByStudent.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowDetailsStud()
    {
        string idno;
        int sessionno = Convert.ToInt32(Session["currentsession"]) - 1;
        StudentController objSC = new StudentController();

        try
        {


            idno = Session["idno"].ToString();

            if (Convert.ToInt32(idno) > 0)
            {
                DataSet dsStudent = objSC.GetStudentDetailsExam(Convert.ToInt32(idno));

                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                        lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                        lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";

                        lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["ENROLLNO"].ToString();
                        lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                        lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                        lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                        lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                        lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                        lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                        lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                        lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                        hdfCategory.Value = dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                        hdfDegreeno.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();

                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";

                        objCommon.FillDropDownList(ddlBackLogSem, "ACD_TRRESULT T", "SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTERNAME", "IDNO = " + idno + "  AND SESSIONNO =" + sessionno, "SEMESTERNO");


                    }
                }
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

    protected void btnProceed_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "2")
        {
            divStud.Visible = false;
            divCourses.Visible = true;
            divNote.Visible = false;
            ShowDetailsStud();
        }
        else
        {
            divNote.Visible = false;

            divStud.Visible = true;
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        int idno;
        int sessionno = Convert.ToInt32(Session["currentsession"]) - 1;

        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else
        {
            idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'"));
        }

        int semesterno = Convert.ToInt32(ddlBackLogSem.SelectedValue);
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@P_SEMESTERNO=" + semesterno + ",@P_SCHEMENO=" + lblScheme.ToolTip;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (ViewState["usertype"].ToString() == "1")
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_RevaluationRegistrationByStudent.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server Unavailable.");
            }
            else
            {
                objCommon.DisplayMessage("Transaction Failed...", this.Page);
                return;
            }
        }
    }

    protected void btnShow_Click1(object sender, EventArgs e)
    {
        int sessionno = Convert.ToInt32(Session["currentsession"]) - 1;
        try
        {
            string chkpublish = string.Empty;
            chkpublish = objCommon.LookUp("RESULT_PROCESS_LOG", "isnull(RES_DECL_STATUS,0)RES_DECL_STATUS", "SESSIONNO = " + sessionno + " AND SCHEMENO = " + Convert.ToInt32(lblScheme.ToolTip) + " AND SEMESTERNO = " + Convert.ToInt32(ddlBackLogSem.SelectedValue));
            if (chkpublish == "0" || chkpublish == "")
            {
                objCommon.DisplayMessage("Result Not Published yet!!", this.Page);
            }
            else
            {
                if (ViewState["usertype"].ToString() == "2")
                {

                    DateTime today = DateTime.Now;
                    string publishdate = objCommon.LookUp("RESULT_PROCESS_LOG", "DISTINCT res_decl_date", "SESSIONNO=" + sessionno + " AND SCHEMENO=" + lblScheme.ToolTip + " AND SEMESTERNO=" + ddlBackLogSem.SelectedValue);
                    if (publishdate == "")
                    {
                        objCommon.DisplayMessage("Result Not Published Yet!!", this.Page);
                    }
                    else
                    {
                        if (Convert.ToInt32((today - Convert.ToDateTime(publishdate)).TotalDays) <= Convert.ToInt32(revalday))
                        {
                            this.showCourses();
                        }
                        else
                        {
                            objCommon.DisplayMessage("Revaluation Registration Date is Closed. Please Contact Examination Section", this.Page);
                            divNote.Visible = true;
                            divCourses.Visible = false;
                        }
                    }

                }
                else
                    this.showCourses();
            }

        }
        catch (Exception ex)
        {
            if (ViewState["usertype"].ToString() == "1")
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_RevaluationRegistrationByStudent.btnShow_Click1() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server Unavailable.");
            }
            else
            {
                objCommon.DisplayMessage("Transaction Failed...", this.Page);
                return;
            }
        }
    }

    public void showCourses()
    {
        //Fail subjects List
        int idno;
        //temp solution given because current session will be define in default page
        int sessionno = Convert.ToInt32(Session["currentsession"]) - 1;
        StudentController objSC = new StudentController();
        DataSet dsFailSubjects;
        DataSet dsDetainedStudent = null;

        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else
        {
            idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'"));
        }
        int semesterno = Convert.ToInt32(ddlBackLogSem.SelectedValue);
        string checks = objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(REVALNO)", "IDNO=" + idno + " AND SESSIONNO=" + sessionno + " AND SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip));
        if (checks == "2")
        {
            objCommon.DisplayMessage("Selected Semester Revaluation Already Done", this.Page);
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();
            return;
        }
        dsFailSubjects = objSC.GetStudentFailExamSubjects_For_Revalution(idno, sessionno, semesterno);
        if (dsFailSubjects.Tables[0].Rows.Count > 0)
        {
            lvFailCourse.DataSource = dsFailSubjects;
            lvFailCourse.DataBind();
            lvFailCourse.Visible = true;
            btnSubmit.Visible = true;
            checkSubject();
        }
        else
        {
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();
            lvFailCourse.Visible = false;
        }

        string check = objCommon.LookUp("ACD_TRRESULT", "count(IDNO)", "SEMESTERNO = " + semesterno + " AND IDNO=" + idno + " AND PASSFAIL='FAIL IN AGGREGATE'");

        if (check != "0")
        {
            dsDetainedStudent = objSC.GetStudentDetained(idno, sessionno, semesterno);
            if (dsDetainedStudent.Tables[0].Rows.Count > 0)
            {

            }
        }
        else
        {

        }

        btnSubmit.Visible = true;
    }

    //get the new receipt No.
    private string GetNewReceiptNo()
    {
        string receiptNo = string.Empty;

        try
        {
            string demandno = objCommon.LookUp("ACD_DCR", "MAX(DCR_NO)", "");
            DataSet ds = feeController.GetNewReceiptData("B", Int32.Parse(Session["userno"].ToString()), "TF");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString());
                receiptNo = dr["PRINTNAME"].ToString() + "/" + "B" + "/" + DateTime.Today.Year.ToString().Substring(2, 2) + "/" + dr["FIELD"].ToString() + demandno;

                // save counter no in hidden field to be used while saving the record
                ViewState["CounterNo"] = dr["COUNTERNO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (ViewState["usertype"].ToString() == "1")
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_RevaluationRegistrationByStudent.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server Unavailable.");
            }
            else
            {
                objCommon.DisplayMessage("Transaction Failed...", this.Page);
            }
        }
        return receiptNo;
    }

    private FeeDemand GetDcrCriteria()
    {
        FeeDemand dcrCriteria = new FeeDemand();
        Student objS = new Student();
        try
        {
            dcrCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]) - 1;
            dcrCriteria.ReceiptTypeCode = "EFR";
            dcrCriteria.BranchNo = Convert.ToInt32(lblBranch.ToolTip);
            dcrCriteria.SemesterNo = Convert.ToInt32(ddlBackLogSem.SelectedValue);
            dcrCriteria.PaymentTypeNo = 1;
            dcrCriteria.UserNo = int.Parse(Session["userno"].ToString());
            dcrCriteria.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (ViewState["usertype"].ToString() == "1")
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_RevaluationRegistrationByStudent.GetDcrCriteria() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server Unavailable.");
            }
            else
            {
                objCommon.DisplayMessage("Transaction Failed...", this.Page);
            }
        }
        return dcrCriteria;
    }

    private FeeDemand GetDemandCriteria()
    {
        FeeDemand demandCriteria = new FeeDemand();
        try
        {
            demandCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]) - 1;
            demandCriteria.ReceiptTypeCode = "EFR";
            demandCriteria.BranchNo = int.Parse(lblBranch.ToolTip);
            demandCriteria.SemesterNo = Convert.ToInt32(ddlBackLogSem.SelectedValue);
            demandCriteria.PaymentTypeNo = 6;
            demandCriteria.UserNo = int.Parse(Session["userno"].ToString());
            demandCriteria.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (ViewState["usertype"].ToString() == "1")
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_RevaluationRegistrationByStudent.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server Unavailable.");
            }
            else
            {
                objCommon.DisplayMessage("Transaction Failed...", this.Page);
            }
        }
        return demandCriteria;
    }

    protected void btnShowstud_Click(object sender, EventArgs e)
    {

        divCourses.Visible = true;
        ShowDetails();


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divCourses.Visible = false;
        txtRollNo.Text = string.Empty;
    }

    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void checkSubject()
    {
        int sessionno = (Convert.ToInt32(Session["currentsession"]) - 1);
        DataSet ds = null;
        ds = objCommon.FillDropDown("ACD_REVAL_RESULT", "courseno", "idno", "IDNO=" + Convert.ToInt32(lblName.ToolTip) + " and sessionno=" + sessionno + " AND SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip), "courseno");
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (ListViewDataItem item in lvFailCourse.Items)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
                    if (chkAccept.ToolTip == ds.Tables[0].Rows[i]["courseno"].ToString())
                    {
                        chkAccept.Checked = true;
                        i++;
                    }
                }
            }
        }
    }
}
