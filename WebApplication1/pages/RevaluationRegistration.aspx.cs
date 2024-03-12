//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : REVALUATION REGISTRATION BY STUDENT AND ADMIN                                     
// CREATION DATE : 05-APRIL-2016
// ADDED BY      : MR.MANISH WALDE
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
using System.Net;
using System.Data;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Diagnostics;



public partial class ACADEMIC_RevaluationRegistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    StudentController objSC = new StudentController();
    StudentRegist objSR = new StudentRegist();
    ActivityController objActController = new ActivityController();
    FeeCollectionController ObjFCC = new FeeCollectionController();
    StudentFees objStudentFees = new StudentFees();

    #region Page Load

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
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

                //this.PopulateDropDownList();

                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                string IPADDRESS = string.Empty;

                IPADDRESS = ip.AddressList[0].ToString();
                ViewState["ipAddress"] = IPADDRESS;
                ViewState["idno"] = "0";

                //Check for Activity On/Off for Reval registration.
                if (CheckActivity() == false)
                    return;
                if (Session["usertype"].ToString() == "2")
                {
                    //if (CheckActivityStudent() == false)
                    //    return;

                    tblSession.Visible = false;
                    divCourses.Visible = true;
                    CheckRevaluationEligibility();


                }
                else
                {


                    divCourses.Visible = true;
                    tblSession.Visible = true;
                }


                PopulateDropDownList();



            }
        }

        //Set the Page Title
        Page.Title = Session["coll_name"].ToString();
        divMsg.InnerHtml = string.Empty;
    }

    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'  and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");

        //ddlSession.SelectedIndex = 1;
        ddlSession.Focus();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        CheckRevaluationEligibility();
    }

    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    ViewState["idno"] = "0";
    //    divCourses.Visible = true;
    //    //ddlSession.Enabled = false;
    //    ddlSession.SelectedIndex = 0;
    //    txtRollNo.Text = string.Empty;
    //    txtRollNo.Enabled = true;
    //    ddlSession.Enabled = true;
    //    lvCurrentSubjects.DataSource = null;
    //    lvCurrentSubjects.DataBind();
    //    tblInfo.Visible = false;
    //    divSem.Visible = false;
    //    btnSubmit.Visible = false;
    //    lblErrorMsg.Text = string.Empty;
    //    lblTotalAmount.Text = "0";
    //    CourseAmt = 0;
    //    divTotalCourseAmount.Visible = false;
    //    divNote.Visible = false;
    //    btnPrintRegSlip.Visible = false;
    //    divAllCoursesFromHist.Visible = false;
    //    divRegCourses.Visible = false;
    //}

    public void CheckRevaluationEligibility()
    {
        try
        {
            if (!string.IsNullOrEmpty(Session["usertype"].ToString()))
            {
                FeeCollectionController feeController = new FeeCollectionController();
                if (Session["usertype"].ToString() == "2")
                {
                    string SESSIONNO = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
                    ViewState["SESSIONNO"] = Convert.ToInt32(SESSIONNO);

                    ViewState["idno"] = Convert.ToInt32(Session["idno"]);

                    tblSession.Visible = false;
                }
                else
                {
                    ViewState["SESSIONNO"] = Convert.ToInt32(ddlSession.SelectedValue);

                    ViewState["idno"] = feeController.GetStudentIdByEnrollmentNo(txtRollNo.Text.Trim());

                    tblSession.Visible = true;
                }



                if (!string.IsNullOrEmpty(ViewState["SESSIONNO"].ToString()) && !string.IsNullOrEmpty(ViewState["idno"].ToString()))
                {
                    // Commented by Pritish S. on 20/10/2020

                    //string RECON = (objCommon.LookUp("ACD_DCR", "DISTINCT RECON", "SESSIONNO=" + ViewState["SESSIONNO"] + " AND IDNO=" + ViewState["idno"] + " AND RECIEPT_CODE='PRF'"));//photocopy recon=1 then only eligible for revaluation

                    //if (RECON == "1" || RECON == "True")  //to check recon 1 or not of photocopy // Commented by Pritish S. on 20/10/2020
                    if (1 == 1)
                    {

                        if (Session["usertype"].ToString() == "2")
                        {
                            this.ShowDetails();

                            btnSubmit.Visible = false;
                            btnPrintRegSlip.Visible = false;
                            //txtRollNo.Enabled = false;
                            lblTotalAmount.Text = "0";
                            CourseAmt = 0;
                            divTotalCourseAmount.Visible = false;
                            ddlSemester.Enabled = false;
                            divSem.Visible = false;
                            divNote.Visible = true;
                            divRegCourses.Visible = false;


                            ViewState["action"] = "add";
                            divCourses.Visible = true;
                            BindCourseListForPHOTOCOPY();
                        }
                        else
                        {
                            //to check already record or not of that particular student
                            string RevalCount = objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(DISTINCT 1)", "SESSIONNO=" + Convert.ToInt32(ViewState["SESSIONNO"]) + " AND IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND ISNULL(CANCEL,0)=0 and APP_TYPE='REVAL' ");

                            if (RevalCount == "1")
                            {
                                string RECON1 = objCommon.LookUp("ACD_DCR", "Distinct isnull(RECON,0) RECON", "SESSIONNO=" + Convert.ToInt32(ViewState["SESSIONNO"]) + " AND IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND ISNULL(CAN,0)=0 and RECIEPT_CODE='RF' ");

                                if (RECON1 == "1" || RECON1 == "True")
                                {
                                    this.ShowDetails();
                                    BindCourseListForPHOTOCOPY();
                                    ddlSemester.Enabled = false;
                                    divSem.Visible = false;
                                    divNote.Visible = true;

                                    txtRollNo.Enabled = false;
                                    ddlSession.Enabled = false;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(updDetails, "Revaluation Registration is Pending of this Student!", this.Page);

                                    ddlSession.SelectedIndex = 0;
                                    txtRollNo.Text = "";
                                    ddlSession.Focus();

                                    btnSubmit.Visible = false;
                                    btnPrintRegSlip.Visible = false;
                                    lvCurrentSubjects.DataSource = null;
                                    lvCurrentSubjects.DataBind();
                                    lvCurrentSubjects.Visible = false;
                                    tblInfo.Visible = false;
                                    divRegCourses.Visible = false;
                                    divNote.Visible = false;
                                    lblTotalAmount.Text = "0";
                                    CourseAmt = 0;
                                    divTotalCourseAmount.Visible = false;

                                    txtRollNo.Enabled = true;
                                    ddlSession.Enabled = true;
                                    return;
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage(updDetails, "Revaluation Registration is Pending of this Student!", this.Page);
                                ddlSession.SelectedIndex = 0;
                                txtRollNo.Text = "";
                                ddlSession.Focus();

                                btnSubmit.Visible = false;
                                btnPrintRegSlip.Visible = false;
                                lvCurrentSubjects.DataSource = null;
                                lvCurrentSubjects.DataBind();
                                lvCurrentSubjects.Visible = false;
                                tblInfo.Visible = false;
                                divRegCourses.Visible = false;
                                divNote.Visible = false;
                                lblTotalAmount.Text = "0";
                                CourseAmt = 0;
                                divTotalCourseAmount.Visible = false;

                                txtRollNo.Enabled = true;
                                ddlSession.Enabled = true;
                                return;
                            }
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updDetails, "Not Eligible For Revaluation Because You have not Applied or confirmed your photocopy details yet !!!", this.Page);
                    }
                }
            }
        }
        catch { }
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            objCommon.RecordActivity(int.Parse(Session["userno"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PhotoCopyRegistration.aspx");
            }

        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PhotoCopyRegistration.aspx");
        }
    }




    private bool CheckActivity()
    {
        try
        {
            bool ret = true;
            string sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
            if (sessionno != "")
            {

                DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno.ToString()), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

                if (dtr.Read())
                {
                    if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                    {
                        objCommon.DisplayMessage(updDetails, "This Activity has been Stopped. Contact Admin.!!", this.Page);
                        ret = false;
                    }

                    if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                    {
                        objCommon.DisplayMessage(updDetails, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                        ret = false;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updDetails, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                    ret = false;
                }
                dtr.Close();
                return ret;
            }
            else
            {
                objCommon.DisplayMessage(updDetails, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                ret = false;
                return ret;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.ShowError(Page, "ACADEMIC_RevaluationRegistration.CheckActivity() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            else
            {
                objCommon.ShowError(Page, "Server Unavailable.");
                return false;
            }
        }

    }





    private bool CheckActivityStudent()
    {
        try
        {
            bool ret = true;
            string sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
            if (sessionno != "")
            {
                string degreeno = objCommon.LookUp("ACD_STUDENT", "DISTINCT DEGREENO", "DEGREENO > 0 AND ISNULL(ADMCAN,0)=0 AND IDNO=" + Convert.ToInt32(Session["idno"]));

                string branchno = objCommon.LookUp("ACD_STUDENT", "DISTINCT BRANCHNO", "BRANCHNO > 0 AND ISNULL(ADMCAN,0)=0 AND IDNO=" + Convert.ToInt32(Session["idno"]));

                string semesterno = objCommon.LookUp("ACD_STUDENT", "DISTINCT SEMESTERNO", "SEMESTERNO > 0 AND ISNULL(ADMCAN,0)=0 AND IDNO=" + Convert.ToInt32(Session["idno"]));


                DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno.ToString()), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), degreeno, branchno, semesterno);

                if (dtr.Read())
                {
                    if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                    {
                        objCommon.DisplayMessage(updDetails, "This Activity has been Stopped. Contact Admin.!!", this.Page);
                        ret = false;
                    }

                    if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                    {
                        objCommon.DisplayMessage(updDetails, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                        ret = false;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updDetails, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                    ret = false;
                }
                dtr.Close();
                return ret;
            }
            else
            {
                objCommon.DisplayMessage(updDetails, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                ret = false;
                return ret;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.ShowError(Page, "ACADEMIC_RevaluationRegistration.CheckActivity() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            else
            {
                objCommon.ShowError(Page, "Server Unavailable.");
                return false;
            }
        }

    }



    //private void PopulateDropDownList()
    //{
    //    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'  and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
    //    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");

    //    //ddlSession.SelectedIndex = 1;
    //    ddlSession.Focus();
    //}



    #endregion

    #region Show Functionality


    //Show Selected Student Information 
    private void ShowDetails()
    {
        try
        {
            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_COLLEGE_MASTER COLL ON (S.COLLEGE_ID = COLL.COLLEGE_ID) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO)", "S.IDNO,DG.DEGREENAME", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO,S.SEMESTERNO,ISNULL(S.SCHEMENO,0)SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE S.PHYSICALLY_HANDICAPPED WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH, COLL.COLLEGE_NAME,S.STUDENTMOBILE", "ISNULL(S.ADMCAN,0)=0 AND S.IDNO = " + ViewState["idno"].ToString(), string.Empty);
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                    lblFatherName.Text = dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                    lblMotherName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                    lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["ENROLLNO"].ToString();
                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                    lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                    lblPH.Text = dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                    lblCollegeName.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                    hfDegreeNo.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();

                    tblInfo.Visible = true;
                    divSem.Visible = true;
                    divCourses.Visible = true;
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

    //private void FillSemester()
    //{
    //    objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO=S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND IDNO=" + ViewState["idno"].ToString(), "SR.SEMESTERNO");
    //}

    private void BindCourseListForPHOTOCOPY()
    {
        //int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        if (ViewState["idno"].ToString() != "0")
        {
            DataSet dsCurrCourses = null;

            //Show Courses for reval
           // dsCurrCourses = objSC.GetCourseFor_RevalOrPhotoCopy(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ViewState["SESSIONNO"]), 2); // Commented by Pritish S. on 20/10/2020 // For Photocopy apply
            dsCurrCourses = objSC.GetCourseFor_RevalOrPhotoCopy(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ViewState["SESSIONNO"]), 1); // Direct apply

            if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
            {
                divAllCoursesFromHist.Visible = true;
                lvCurrentSubjects.DataSource = dsCurrCourses.Tables[0];
                lvCurrentSubjects.DataBind();
                lvCurrentSubjects.Visible = true;

                string RECON = objCommon.LookUp("ACD_DCR", "Distinct isnull(RECON,0) RECON", "SESSIONNO=" + Convert.ToInt32(ViewState["SESSIONNO"]) + " AND IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND ISNULL(CAN,0)=0 and RECIEPT_CODE='RF' ");

                if (RECON == "1" || RECON == "True")
                {
                    if (Session["usertype"].ToString() == "2")
                    {
                        checkSubject();
                    }
                    else
                    {
                        checkSubjectForAdmin();
                    }


                    string subcount = objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(DISTINCT 1)", "SESSIONNO=" + Convert.ToInt32(ViewState["SESSIONNO"]) + " AND IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND ISNULL(CANCEL,0)=0 AND APP_TYPE='REVAL' ");
                    if (subcount == "1")
                    {
                        // string TOTALAMOUNT = objCommon.LookUp("ACD_DCR", "SUM(TOTAL_AMT)", "SESSIONNO=" + Convert.ToInt32(ViewState["SESSIONNO"]) + " AND IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND ISNULL(CAN,0)=0 and RECIEPT_CODE='RF' ");
                        string TOTALAMOUNT = objCommon.LookUp("ACD_REVAL_RESULT R INNER JOIN ACD_SCHEME S ON S.SCHEMENO=R.SCHEMENO INNER JOIN ACD_REVAL_FEE_DEFINE RF ON RF.DEGREENO = S.DEGREENO", "(COUNT(COURSENO) * REVAL_FEE) TOTAL_AMOUNT", " R.IDNO = " + Convert.ToInt32(ViewState["idno"]) + " AND R.SESSIONNO = " + Convert.ToInt32(ViewState["SESSIONNO"]) + "AND APP_TYPE='REVAL' AND ISNULL(CANCEL,0)=0 GROUP BY REVAL_FEE");

                        divTotalCourseAmount.Visible = true;
                        lblTotalAmount.Text = TOTALAMOUNT;

                        if (Session["usertype"].ToString() == "2")
                        {
                            btnSubmit.Visible = false;
                            btnPrintRegSlip.Visible = true;
                            divRegCourses.Visible = false;
                        }
                        else
                        {
                            int count = 0;
                            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
                            {
                                CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                                if (cbRow.Checked == true)
                                {
                                    count = Convert.ToInt16(count) + Convert.ToInt16(1);
                                }
                            }

                            if (count == 5)
                            {
                                btnSubmit.Visible = false;
                                btnPrintRegSlip.Visible = true;
                                divRegCourses.Visible = false;
                                checkSubject();
                            }
                            else
                            {
                                btnSubmit.Visible = true;
                                btnPrintRegSlip.Visible = false;
                                divRegCourses.Visible = false;
                            }
                        }

                    }
                    else
                    {
                        btnSubmit.Visible = true;
                        btnPrintRegSlip.Visible = false;

                        //divRegisteredCoursesTotalAmt.Visible = false;
                        divRegCourses.Visible = false;

                    }
                }
                else
                {
                    btnSubmit.Visible = true;
                    btnPrintRegSlip.Visible = false;

                    // btnCancel.Visible = true;
                    //divRegisteredCoursesTotalAmt.Visible = false;
                    divRegCourses.Visible = false;
                }

            }
            else
            {
                btnSubmit.Visible = false;
                lvCurrentSubjects.DataSource = null;
                lvCurrentSubjects.DataBind();
                lvCurrentSubjects.Visible = false;

                //divRegisteredCoursesTotalAmt.Visible = false;
                divRegCourses.Visible = false;
                //btnCancel.Visible = true;
                objCommon.DisplayMessage(updDetails, "No Course found in Allotted Scheme and Semester.\\nIn case of any query contact administrator.", this.Page);
            }
        }
        else
        {
            Response.Redirect("~/default.aspx");
        }
    }

    private void checkSubject()
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("ACD_REVAL_RESULT", "COURSENO", "IDNO", "APP_TYPE = 'REVAL' AND IDNO=" + Convert.ToInt32(ViewState["idno"]) + "AND ISNULL(CANCEL,0)=0 AND sessionno=" + Convert.ToInt32(ViewState["SESSIONNO"]), "COURSENO");

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (ListViewDataItem item in lvCurrentSubjects.Items)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
                        if (chkAccept.ToolTip == ds.Tables[0].Rows[i]["courseno"].ToString())
                        {
                            chkAccept.Checked = true;
                            chkAccept.Enabled = false;
                            chkAccept.BackColor = System.Drawing.Color.Red;
                            i++;
                        }
                        else
                        {
                            chkAccept.Enabled = false;
                        }
                    }
                }
            }
        }
    }
    private void checkSubjectForAdmin()
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("ACD_REVAL_RESULT", "COURSENO", "IDNO", "APP_TYPE = 'REVAL' AND IDNO=" + Convert.ToInt32(ViewState["idno"]) + "AND ISNULL(CANCEL,0)=0 AND sessionno=" + Convert.ToInt32(ViewState["SESSIONNO"]), "COURSENO");

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (ListViewDataItem item in lvCurrentSubjects.Items)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
                        if (chkAccept.ToolTip == ds.Tables[0].Rows[i]["courseno"].ToString())
                        {
                            chkAccept.Checked = true;
                            chkAccept.Enabled = false;
                            chkAccept.BackColor = System.Drawing.Color.Red;
                            i++;
                        }
                        //else
                        //{
                        //    chkAccept.Enabled = false;
                        //}
                    }
                }
            }
        }
    }


    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            BindCourseListForPHOTOCOPY();
            //IsPHOTOCOPYApproved();
        }
        else
        {
            btnSubmit.Visible = false;
            btnPrintRegSlip.Visible = false;
            lvCurrentSubjects.DataSource = null;
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = false;
        }
        lblTotalAmount.Text = "0";
        CourseAmt = 0;
        divTotalCourseAmount.Visible = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["idno"] = "0";
        divCourses.Visible = true;
        //ddlSession.Enabled = false;
        // ddlSession.SelectedIndex = 0;
        //txtRollNo.Text = string.Empty;
        //txtRollNo.Enabled = true;
        lvCurrentSubjects.DataSource = null;
        lvCurrentSubjects.DataBind();
        tblInfo.Visible = false;
        divSem.Visible = false;
        btnSubmit.Visible = false;
        lblErrorMsg.Text = string.Empty;

        lblTotalAmount.Text = "0";
        CourseAmt = 0;
        divTotalCourseAmount.Visible = false;
        divNote.Visible = false;
        btnPrintRegSlip.Visible = false;

        divAllCoursesFromHist.Visible = false;

        divRegCourses.Visible = false;
        //divRegisteredCoursesTotalAmt.Visible = false;

        txtRollNo.Text = string.Empty;
        ddlSession.SelectedIndex = 0;

        txtRollNo.Enabled = true;
        ddlSession.Enabled = true;
    }

    #endregion

    #region Submit Functionality

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            string courseno = "";
            courseno = getcourseno();
            if (courseno == "0")
            {
                objCommon.DisplayMessage(updDetails, "Please Select At least One Subject from list!!", this.Page);
                return;
            }

            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {
                CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                if (cbRow.Checked == true && cbRow.Enabled == true)
                {
                    count = Convert.ToInt16(count) + Convert.ToInt16(1);
                }
            }
            if (count <= 5)
            {
                SubmitCourses();


            }
            else
            {
                objCommon.DisplayMessage(updDetails, "Please Select only 5 Subjects !! You Selected : " + count + " Subjects !!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_RevaluationRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //to get courseno having Approval_status as Not Applied
    private string getcourseno()
    {
        try
        {
            string retCNO = string.Empty;
            foreach (ListViewDataItem item in lvCurrentSubjects.Items)
            {
                CheckBox cbRow = item.FindControl("chkAccept") as CheckBox;
                if (cbRow.Checked && cbRow.Enabled)
                {
                    if (retCNO.Length == 0) retCNO = ((item.FindControl("lblCCode")) as Label).ToolTip.ToString();
                    else
                        retCNO += "," + ((item.FindControl("lblCCode")) as Label).ToolTip.ToString();
                }
            }
            if (retCNO.Equals(""))
            {
                return "0";
            }
            else
            {
                return retCNO;
            }
        }
        catch { return null; }
    }

    public void SubmitCourses()
    {
        try
        {
            int result = 0;
            Boolean selection = false;
            int opertion = 0;
            string COURSENOS = string.Empty, EXTERMARKS = string.Empty, CCODES = string.Empty, SEMESTERNOS = string.Empty, GRADES = string.Empty;
            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {
                //Get Student Details from lvStudent
                CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                if (cbRow.Checked == true && cbRow.Enabled == true)
                {
                    selection = true;
                    COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + "$";
                    GRADES += ((dataitem.FindControl("lblExtermark")) as Label).Text + "$";
                    CCODES += (dataitem.FindControl("lblCCode") as Label).Text + "$";
                    SEMESTERNOS += (dataitem.FindControl("lblSEMSCHNO") as Label).ToolTip + "$";
                    EXTERMARKS += ((dataitem.FindControl("lblMarks")) as Label).Text + "$";
                }
                objSR.SCHEMENO = Convert.ToInt32((dataitem.FindControl("lblCourseName") as Label).ToolTip);
            }

            objSR.COURSENOS = COURSENOS.TrimEnd('$');
            objSR.EXTERMARKS = GRADES.TrimEnd('$');
            objSR.CCODES = CCODES.TrimEnd('$');
            objSR.SEMESTERNOS = SEMESTERNOS.TrimEnd('$');
            EXTERMARKS = EXTERMARKS.TrimEnd('$');
            if (!selection)
            {
                objSR.COURSENOS = "0";
                objSR.EXTERMARKS = "0";
                objSR.CCODES = "0";
                objSR.SEMESTERNOS = "0";
            }

            objSR.SESSIONNO = Convert.ToInt32(ViewState["SESSIONNO"]);
            objSR.IDNO = Convert.ToInt32(ViewState["idno"]);
            objSR.IPADDRESS = Session["ipAddress"].ToString();
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.UA_NO = Convert.ToInt32(Session["userno"]);
            //objSR.SEMESTERNO = Convert.ToInt32(ddlSemester.SelectedValue);


            ////to generate demand and dcr
            //DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_COLLEGE_MASTER COLL ON (S.COLLEGE_ID = COLL.COLLEGE_ID) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO)", "S.IDNO,DG.DEGREENAME", "S.STUDNAME,S.REGNO,S.ENROLLNO,S.SEMESTERNO,ISNULL(S.SCHEMENO,0)SCHEMENO,B.BRANCHNO,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,YEAR,PTYPE", "S.IDNO = " + ViewState["idno"].ToString(), string.Empty);




            if (Session["usertype"].ToString() == "2")//for student
            {
               // string IDNO= objSR.IDNO;    
               // int SESSIONNO = objSR.SESSIONNO;   
               // int SCHEMENO= objSR.SCHEMENO;  
               // string COURSENOS=objSR.COURSENOS;    
               // =objSR.IPADDRESS     
               // =objSR.SEMESTERNOS   
               // =objSR.COLLEGE_CODE  
               // =objSR.UA_NO         
               // =objSR.EXTERMARKS    
               // =objSR.CCODES        
               //string App_Type            =
               //int Total_Exter_Marks   =
               // int User_Type           =


                result = objSReg.AddPhotoCopyRegisteration(objSR, "REVAL", EXTERMARKS, Convert.ToInt32(Session["usertype"]));
            }
            else //for admin
            {
                result = objSReg.AddPhotoRevalRegByAdmin(objSR, "REVAL", EXTERMARKS, Convert.ToInt32(Session["usertype"]));
            }



            if (result > 0)
            {
                objCommon.DisplayMessage(updDetails, "Revaluation Details Applied Successfully.But will be confirm only after Successful Payment.", this.Page);


                lblTotalAmount.Text = "0";
                CourseAmt = 0;
                divTotalCourseAmount.Visible = false;
                btnSubmit.Visible = false;

                /////////////////////////////////////////////
                //to hide all courses
                //BindCourseListForPHOTOCOPY();
                divAllCoursesFromHist.Visible = false;


                /////////////////////////////////////////////



                btnPayOnline.Visible = true;
                btnChallan.Visible = true;
                divRegCourses.Visible = true;
                LoadTotalRegisteredAmount();
                divTotalCourseAmount.Visible = true;

                //divRegisteredCoursesTotalAmt.Visible = true;
                //ShowReport("Photo Copy Registration Slip", "rptPhotoRevaluation.rpt");


                if (Session["usertype"].ToString() == "2")
                {
                    tblSession.Visible = false;
                    btnPayOnline.Visible = true;
                    btnChallan.Visible = true;
                    //to show registeredcourses
                    BindRegisteredCoursesofPHOTOCOPY();
                }
                else
                {
                    tblSession.Visible = true;
                    btnPayOnline.Visible = false;
                    btnChallan.Visible = false;
                    BindCourseListForPHOTOCOPY();
                }



            }
            else
            {
                objCommon.DisplayMessage("No Subjects Registered.", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Session["usertype"].ToString() == "1")
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_RevaluationRegistration.SubmitCourses() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server Unavailable.");
            }
            else
            {
                objCommon.DisplayMessage(updDetails, "Transaction Failed...", this.Page);
                return;
            }
        }
    }


    public void LoadTotalRegisteredAmount()
    {
        decimal RegTotalAmt = 0.00M;
        // RegTotalAmt = Convert.ToDecimal(objCommon.LookUp("ACD_DCR", "SUM(TOTAL_AMT)", " IDNO = " + Convert.ToInt32(ViewState["idno"]) + " AND SESSIONNO = " + Convert.ToInt32(ViewState["SESSIONNO"]) + "  AND ISNULL(CAN,0)=0 AND RECIEPT_CODE='RF'"));
        RegTotalAmt = Convert.ToDecimal(objCommon.LookUp("ACD_REVAL_RESULT R INNER JOIN ACD_SCHEME S ON S.SCHEMENO=R.SCHEMENO INNER JOIN ACD_REVAL_FEE_DEFINE RF ON RF.DEGREENO = S.DEGREENO", "(COUNT(COURSENO) * REVAL_FEE) TOTAL_AMOUNT", " R.IDNO = " + Convert.ToInt32(ViewState["idno"]) + " AND R.SESSIONNO = " + Convert.ToInt32(ViewState["SESSIONNO"]) + "AND APP_TYPE='REVAL' AND ISNULL(CANCEL,0)=0 GROUP BY REVAL_FEE"));
        lblTotalAmount.Text = RegTotalAmt.ToString();
    }


    decimal reval_Amt = 0.00M;
    public void LoadRevalFeeAmount()
    {
        if (!string.IsNullOrEmpty(hfDegreeNo.Value))
        {
            //to calculate reval fee degree wise
            reval_Amt = Convert.ToDecimal(objCommon.LookUp("ACD_REVAL_FEE_DEFINE", "REVAL_FEE", " DEGREENO = " + hfDegreeNo.Value + ""));
        }

    }

    static decimal CourseAmt = 0;
    static decimal CourseCount = 0;
    protected void chkAccept_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            divTotalCourseAmount.Visible = true;
            LoadRevalFeeAmount();
            CheckBox chk = sender as CheckBox;

            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {
                CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                Label lblExtermark = dataitem.FindControl("lblExtermark") as Label;

                if (cbRow.Checked == true)
                {
                    CourseCount = CourseCount + 1;

                    if (CourseCount <= 5)
                        CourseAmt = Convert.ToDecimal(CourseAmt) + Convert.ToDecimal(reval_Amt);

                    if (CourseCount > 5)
                    {
                        chk.Checked = false;
                    }
                }

            }

            lblTotalAmount.Text = CourseAmt.ToString();

            if (CourseCount > 5)
            {
                objCommon.DisplayMessage(updDetails, "Maximum 5 Subjects Limit Reached.", this.Page);
            }

            CourseAmt = 0;
            CourseCount = 0;
        }
        catch { }
    }

    #endregion

    #region Report Functionality

    //Show Revauation Registertion Slip
    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Revaluation Registration Slip", "rptPhotoRevaluation.rpt");
        }
        catch { }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        // int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int idno = Convert.ToInt32(lblName.ToolTip);
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(ViewState["idno"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ViewState["SESSIONNO"]) + ",@P_REVAL_TYPE=2";//2 for reval
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

    #endregion


    #region "Online Payment Functions and transactions"

    private void CreateCustomerRef()
    {
        Random rnd = new Random();
        int ir = rnd.Next(01, 10000);
        lblOrderID.Text = Convert.ToString(Convert.ToString(ViewState["idno"]) + Convert.ToString(ir));
    }

    private void GetSessionValues()
    {
        ViewState["FirstName"] = lblName.Text;
        //ViewState["RegNo"] = lblapp.Text;
        ViewState["MobileNo"] = lblPH.Text;
        //ViewState["EMAILID"] = lblEmail.Text;
        ViewState["OrderID"] = lblOrderID.Text;
        ViewState["TOTAL_AMT"] = lblTotalAmount.Text;
        //ViewState["TOTAL_AMT"] = "1";
    }

    //----  BILL DESK PAYMENT GATEWAY ----------------//
    protected void PostOnlinePayment()
    {
        #region Declarations
        string feeAmount = string.Empty;
        string Transacionid = "NA";
        string TransactionFor = string.Empty;
        string TSPLTxnCode = string.Empty;
        string TSPLtxtITC = string.Empty;
        #endregion

        #region Get Payment Details
        feeAmount = (ViewState["Final_Amt"]).ToString();
        #endregion

        #region Payment Log for Different Transaction Id
        string TransactionCode = string.Empty;
        TransactionCode = lblOrderID.Text; // This may be configured from Database for Different Running Number
        #endregion

        #region BillDesk Data Declaration
        string MerchantID = string.Empty;
        string UniTranNo = string.Empty;
        string NA1 = string.Empty;
        string txn_amount = string.Empty;
        string NA2 = string.Empty;
        string NA3 = string.Empty;
        string NA4 = string.Empty;
        string CurrencyType = string.Empty;
        string NA5 = string.Empty;
        string TypeField1 = string.Empty;
        string SecurityID = string.Empty;
        string NA6 = string.Empty;
        string NA7 = string.Empty;
        string TypeField2 = string.Empty;
        string additional_info1 = string.Empty;
        string additional_info2 = string.Empty;
        string additional_info3 = string.Empty;
        string additional_info4 = string.Empty;
        string additional_info5 = string.Empty;
        string additional_info6 = string.Empty;
        string additional_info7 = string.Empty;
        string ReturnURL = string.Empty;
        string ChecksumKey = string.Empty;
        #endregion

        #region Set Bill Desk Param Data
        MerchantID = ConfigurationManager.AppSettings["MerchantID"];
        UniTranNo = TransactionCode;
        txn_amount = feeAmount;
        CurrencyType = "INR";
        SecurityID = ConfigurationManager.AppSettings["SecurityCode"];
        additional_info1 = ViewState["STUDNAME"].ToString(); // Project Name
        additional_info2 = ViewState["IDNO"].ToString();  // Project Code
        additional_info3 = ViewState["RECIEPT"].ToString(); // Transaction for??
        additional_info4 = ViewState["info"].ToString(); // Payment Reason
        additional_info5 = feeAmount; // Amount Passed
        additional_info6 = ViewState["basicinfo"].ToString(); // to get basic stud details
        additional_info7 = ViewState["SESSIONNO"].ToString();


        ReturnURL = "https://svce.mastersofterp.in/Academic/PhotoReval_Response.aspx";

        //ReturnURL = "https://svce.mastersofterp.in/Academic/PhotoReval_Response.aspx?id=" + ViewState["IDNO"].ToString();
        //ReturnURL = "https://svce.mastersofterp.in/Academic/PhotoReval_Response.aspx?id=" + ViewState["IDNO"].ToString();
        //ReturnURL = "http://localhost:52072/PresentationLayer/Academic/PhotoReval_Response.aspx?id=" + ViewState["IDNO"].ToString();
        //ReturnURL = "http://localhost:52072/PresentationLayer/Academic/PhotoReval_Response.aspx"; 
        //ReturnURL = "https://svcetest.mastersofterp.in/Academic/PhotoReval_Response.aspx";
        ChecksumKey = ConfigurationManager.AppSettings["ChecksumKey"];
        #endregion

        #region Generate Bill Desk Check Sum

        StringBuilder billRequest = new StringBuilder();
        billRequest.Append(MerchantID).Append("|");
        billRequest.Append(UniTranNo).Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append(txn_amount).Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append(CurrencyType).Append("|");
        billRequest.Append("DIRECT").Append("|");
        billRequest.Append("R").Append("|");
        billRequest.Append(SecurityID).Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append("F").Append("|");
        billRequest.Append(additional_info1).Append("|");
        billRequest.Append(additional_info2).Append("|");
        billRequest.Append(additional_info3).Append("|");
        billRequest.Append(additional_info4).Append("|");
        billRequest.Append(additional_info5).Append("|");
        billRequest.Append(additional_info6).Append("|");
        billRequest.Append(additional_info7).Append("|");
        billRequest.Append(ReturnURL);

        string data = billRequest.ToString();

        String hash = String.Empty;
        hash = GetHMACSHA256(data, ChecksumKey);
        hash = hash.ToUpper();

        string msg = data + "|" + hash;

        #endregion

        #region Post to BillDesk Payment Gateway

        string PaymentURL = ConfigurationManager.AppSettings["BillDeskURL"] + msg;

        //Response.Redirect(PaymentURL, false);
        Response.Write("<form name='s1_2' id='s1_2' action='" + PaymentURL + "' method='post'> ");
        Response.Write("<script type='text/javascript' language='javascript' >document.getElementById('s1_2').submit();");
        Response.Write("</script>");
        Response.Write("<script language='javascript' >");
        Response.Write("</script>");
        Response.Write("</form> ");
        Response.Write("<script>window.open(" + PaymentURL + ",'_blank');</script>");
        #endregion
    }

    public string GetHMACSHA256(string text, string key)
    {
        UTF8Encoding encoder = new UTF8Encoding();

        byte[] hashValue;
        byte[] keybyt = encoder.GetBytes(key);
        byte[] message = encoder.GetBytes(text);

        HMACSHA256 hashString = new HMACSHA256(keybyt);
        string hex = "";

        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }


    protected void btnPayOnline_Click(object sender, EventArgs e)
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////
        #region "Online Payment"
        try
        {
            int ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1) PAY_COUNT", "IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(ViewState["SESSIONNO"]) + " AND RECIEPT_CODE = 'RF' AND RECON = 1 AND ISNULL(CAN,0)=0"));



            if (ifPaidAlready > 0)
            {
                objCommon.DisplayMessage(updDetails, "Revaluation Fee has been paid already. Can't proceed with the transaction !", this);
                btnPayOnline.Visible = false;
                btnChallan.Visible = false;
                return;
            }

            int result = 0;
            CreateCustomerRef();
            GetSessionValues();

            ViewState["Final_Amt"] = lblTotalAmount.Text.ToString();

            //ViewState["Final_Amt"] = "1";


            if (Convert.ToDouble(ViewState["Final_Amt"]) == 0)
            {
                objCommon.DisplayMessage(updDetails, "You are not eligible for Fee Payment !", this);
                return;
            }

            objStudentFees.UserNo = Convert.ToInt32(ViewState["idno"]);
            objStudentFees.Amount = Convert.ToDouble(ViewState["Final_Amt"]);
            objStudentFees.SessionNo = (ViewState["SESSIONNO"].ToString());
            objStudentFees.OrderID = lblOrderID.Text;

            //insert in acd_fees_log
            result = ObjFCC.AddPhotoRevalFeeLog(objStudentFees, 1, 1, "RF", 2); //2 for reval

            if (result > 0)
            {

                // DataSet d = objCommon.FillDropDown("ACD_STUDENT", "IDNO", "REGNO,STUDNAME,STUDENTMOBILE,EMAILID", "IDNO = '" + ViewState["idno"] + "'", "");
                DataSet d = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON B.BRANCHNO=S.BRANCHNO", "IDNO", "ISNULL(REGNO,'')REGNO,ISNULL(ENROLLNO,'')ENROLLNO,ISNULL(STUDNAME,'')STUDNAME,ISNULL(STUDENTMOBILE,'')STUDENTMOBILE,ISNULL(EMAILID,'')EMAILID,ISNULL(B.SHORTNAME,'')SHORTNAME", "IDNO = '" + Convert.ToInt32(ViewState["idno"]) + "'", "");
                ViewState["STUDNAME"] = (d.Tables[0].Rows[0]["STUDNAME"].ToString());
                ViewState["IDNO"] = (d.Tables[0].Rows[0]["IDNO"].ToString());
                ViewState["EMAILID"] = (d.Tables[0].Rows[0]["EMAILID"].ToString());
                ViewState["MOBILENO"] = (d.Tables[0].Rows[0]["STUDENTMOBILE"].ToString());
                ViewState["REGNO"] = (d.Tables[0].Rows[0]["REGNO"].ToString());
                //ViewState["SESSIONNO"] = ddlSession.SelectedValue;
                ViewState["SEM"] = lblSemester.ToolTip.ToString();
                ViewState["RECIEPT"] = "RF";

                ViewState["ENROLLNO"] = (d.Tables[0].Rows[0]["ENROLLNO"].ToString());
                ViewState["SHORTNAME"] = (d.Tables[0].Rows[0]["SHORTNAME"].ToString());

                if (d.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() == "" || d.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() == string.Empty)
                {
                    ViewState["MOBILENO"] = "NA";
                }
                if (d.Tables[0].Rows[0]["REGNO"].ToString() == "" || d.Tables[0].Rows[0]["REGNO"].ToString() == string.Empty)
                {
                    ViewState["REGNO"] = "NA";
                }
                if (d.Tables[0].Rows[0]["ENROLLNO"].ToString() == "" || d.Tables[0].Rows[0]["ENROLLNO"].ToString() == string.Empty)
                {
                    ViewState["ENROLLNO"] = "NA";
                }
                string info = string.Empty;
                //ViewState["info"] = "RF" + ViewState["REGNO"] + "," + ViewState["SESSIONNO"] + "," + ViewState["SEM"] + "," + ViewState["MOBILENO"];
                //ViewState["info"] = ViewState["SEM"] + "," + ViewState["MOBILENO"];
                //ViewState["basicinfo"] = ViewState["REGNO"] + "," + ViewState["ENROLLNO"] + "," + ViewState["SHORTNAME"];

                ViewState["info"] = ViewState["REGNO"] + "," + ViewState["SHORTNAME"] + "," + ViewState["SEM"] + "," + ViewState["MOBILENO"];
                ViewState["basicinfo"] = ViewState["ENROLLNO"];

                PostOnlinePayment();
            }
            else
            {
                objCommon.DisplayMessage(updDetails, "Transaction Failed !.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        #endregion
    }


    #endregion


    #region "Registered Subjects For Revaluation"

    private void BindRegisteredCoursesofPHOTOCOPY()
    {
        lvFinalCourses.DataSource = null;
        lvFinalCourses.DataBind();

        DataSet dsRegCourses = null;
        //3 for showing registered courses
        //Show Reg. Courses for Revaluation
        dsRegCourses = objSC.GetCourseFor_RevalOrPhotoCopy(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ViewState["SESSIONNO"]), 3); //3 for to show registered courses for reval
        if (dsRegCourses != null && dsRegCourses.Tables.Count > 0 && dsRegCourses.Tables[0].Rows.Count > 0)
        {
            lvFinalCourses.DataSource = dsRegCourses.Tables[0];
            lvFinalCourses.DataBind();
            pnlFinalCourses.Visible = true;
        }
        else
        {
            lvFinalCourses.DataSource = null;
            lvFinalCourses.DataBind();
            pnlFinalCourses.Visible = false;
            objCommon.DisplayMessage(updDetails, "No Registered Subjects found in Allotted Scheme and Semester.\\nIn case of any query contact administrator.", this.Page);
        }

    }
    #endregion


    #region "Print Challan"
    protected void btnChallan_Click(object sender, EventArgs e)
    {
        try
        {
            int CheckRecon = 0;
            string pay_mode = string.Empty;
            string Pay_mode_Details = string.Empty;
            CheckRecon = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(*)", "IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(ViewState["SESSIONNO"]) + "AND RECIEPT_CODE ='RF' AND ISNULL(CAN,0) = 0 AND ISNULL(RECON,0) = 1 "));

            pay_mode = (objCommon.LookUp("ACD_DCR", "ISNULL(PAY_MODE_CODE,'') PAY_MODE_CODE", "IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(ViewState["SESSIONNO"]) + "AND RECIEPT_CODE ='RF' AND ISNULL(CAN,0) = 0"));

            //to check payment already done or not
            if (CheckRecon == 1)
            {
                if (pay_mode == "C")
                {
                    Pay_mode_Details = "Cash Payment";
                }
                else if (pay_mode == "O")
                {
                    Pay_mode_Details = "Online Payment";
                }
                objCommon.DisplayMessage(updDetails, "Revaluation Fees Already Done For this Session through " + Pay_mode_Details + "..!", this.Page);

                btnPayOnline.Visible = false;
                btnChallan.Visible = false;
                return;
            }



            //to generate challan
            //if (pay_mode != "C")
            //{
            //to update challan details
            int status = objSC.UpdateChallanDetails(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ViewState["SESSIONNO"]), 2); //2 for reval
            if (status == 1)
            {
                this.ShowReport("Payment_Details", "rptPhotoRevalChallanSummary.rpt", "RF");
                //btnPayOnline.Visible = false;
                //btnChallan.Visible = false;
            }
            else
            {
                objCommon.DisplayMessage(updDetails, "Something went Wrong!", this.Page);
            }
            //}
            //else
            //{
            //    objCommon.DisplayMessage(updDetails, "Challan Already Generated!", this.Page);
            //}



        }
        catch { }


    }

    //used for to Showing the report on hostel challan fees and challan fees.
    private void ShowReport(string reportTitle, string rptFileName, string Reciepttype)
    {
        try
        {
            //string dcrno = string.Empty;

            //dcrno = objCommon.LookUp("ACD_DEMAND", "distinct DM_NO ", "IDNO=" + Convert.ToInt32(ViewState["idno"].ToString()) + " AND SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) +  "AND RECIEPT_CODE ='" + Reciepttype + "' AND ISNULL(CAN,0) = 0");


            //if (!string.IsNullOrEmpty(dcrno))
            //{
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_IDNO=" + Convert.ToInt32(ViewState["idno"].ToString()) + ",@P_SESSIONNO=" + Convert.ToInt32(ViewState["SESSIONNO"]) + ",@P_RECEIPTTYPE=" + Reciepttype + ",@P_CHALLAN_TYPE=2,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            //@P_CHALLAN_TYPE = 1 --- for photo copy and  2 --for reval
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(updDetails, updDetails.GetType(), "controlJSScript", sb.ToString(), true);

            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PhotoCopyRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion
}


