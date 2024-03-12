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


public partial class ACADEMIC_ReviewRegistration : System.Web.UI.Page
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

                PopulateDropDownList();

                if (Session["usertype"].ToString() == "2")
                {
                    tblSession.Visible = false;
                    divCourses.Visible = true;
                    CheckRevaluationEligibility();
                }
                else
                {
                    divCourses.Visible = true;
                    tblSession.Visible = true;
                }

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

        ddlSession.SelectedIndex = 1;
        ddlSession.Enabled = false;
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

                    this.ShowDetails();
                    int count = 0;
                    if (Session["usertype"].ToString() == "2")
                    {
                         count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_REVIEW_DATA", "count(*)", "SESSIONNO=" + ViewState["SESSIONNO"] + " and IDNO=" + Convert.ToInt32(lblName.ToolTip) + " and SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip)));
                    }
                    else
                    {
                         count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_REVIEW_DATA", "count(*)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " and IDNO=" + Convert.ToInt32(lblName.ToolTip) + " and SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip)));
                    }                    
                   //  BindCourseListForReviewApply();
                    ddlSemester.Enabled = false;
                    divSem.Visible = false;
                    divNote.Visible = true;

                    txtRollNo.Enabled = false;
                    ddlSession.Enabled = false;

                    // btnSubmit.Visible = true;

                    if (count < 1)
                    {
                        BindCourseListForReviewApply();
                        btnSubmit.Visible = true;
                    }
                    else
                    {
                        BindCourseListForReviewApplied();
                        btnPrintRegSlip.Visible = true;
                        divSem.Visible = false;
                        //return;                       
                        foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
                        {
                            CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                            TextBox txtddno = dataitem.FindControl("txtddno") as TextBox; 
                            cbRow.Visible = false;
                            txtddno.Enabled = false;
                        }
                        decimal totamount = 0;
                        if (Session["usertype"].ToString() == "2")
                        {                           
                            totamount = Convert.ToDecimal(objCommon.LookUp("ACD_STUDENT_REVIEW_DATA", "sum(AMOUNT)", "SESSIONNO=" + ViewState["SESSIONNO"] + " and IDNO=" + Convert.ToInt32(lblName.ToolTip) + " and SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip)));                           
                        }
                        else
                        {
                            totamount = Convert.ToDecimal(objCommon.LookUp("ACD_STUDENT_REVIEW_DATA", "sum(AMOUNT)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " and IDNO=" + Convert.ToInt32(lblName.ToolTip) + " and SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip)));
                        }                        
                        divTotalCourseAmount.Visible = true;
                        lblTotalAmount.Text = totamount.ToString();
                        objCommon.DisplayMessage(updDetails, "Review Registration Allready Applied . You Are Able To Print the Review Registration Slip.", this.Page);
                       
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
                objCommon.ShowError(Page, "ACADEMIC_PhotoCopyRegistration.CheckActivity() --> " + ex.Message + " " + ex.StackTrace);
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

    private void BindCourseListForReviewApply()
    {
        //int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        if (ViewState["idno"].ToString() != "0")
        {
            DataSet dsCurrCourses = null;

            //Show Courses for reval
            dsCurrCourses = objSC.GetCourseFor_ReviewApply(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ViewState["SESSIONNO"]));

            if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
            {
                divAllCoursesFromHist.Visible = true;
                lvCurrentSubjects.DataSource = dsCurrCourses.Tables[0];
                lvCurrentSubjects.DataBind();
                lvCurrentSubjects.Visible = true;

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

    private void BindCourseListForReviewApplied()
    {
        //int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        if (ViewState["idno"].ToString() != "0")
        {
            DataSet dsCurrCourses = null;

            //Show Courses for reval
            dsCurrCourses = objSC.GetCourseFor_ReviewAppliedCourse(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ViewState["SESSIONNO"]));

            if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
            {
                divAllCoursesFromHist.Visible = true;
                lvCurrentSubjects.DataSource = dsCurrCourses.Tables[0];
                lvCurrentSubjects.DataBind();
                lvCurrentSubjects.Visible = true;

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
            else
            {
                SubmitCourses();
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
            string DDNO = string.Empty;
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
                    DDNO += ((dataitem.FindControl("txtddno")) as TextBox).Text + "$";
                }
                objSR.SCHEMENO = Convert.ToInt32((dataitem.FindControl("lblCourseName") as Label).ToolTip);
            }

            objSR.COURSENOS = COURSENOS.TrimEnd('$');
            objSR.EXTERMARKS = GRADES.TrimEnd('$');
            objSR.CCODES = CCODES.TrimEnd('$');
            objSR.SEMESTERNOS = SEMESTERNOS.TrimEnd('$');
            EXTERMARKS = EXTERMARKS.TrimEnd('$');
            DDNO = DDNO.TrimEnd('$');
            if (!selection)
            {
                objSR.COURSENOS = "0";
                objSR.EXTERMARKS = "0";
                objSR.CCODES = "0";
                objSR.SEMESTERNOS = "0";
                DDNO = "0";
            }

            objSR.SESSIONNO = Convert.ToInt32(ViewState["SESSIONNO"]);
            objSR.IDNO = Convert.ToInt32(ViewState["idno"]);
            objSR.IPADDRESS = Session["ipAddress"].ToString();
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.UA_NO = Convert.ToInt32(Session["userno"]);
            int ret = objSReg.InsertReviewApplyReg(objSR, Convert.ToDecimal(lblTotalAmount.Text),DDNO);
            if (ret == 1)
            {
                objCommon.DisplayMessage(updDetails, "Review Registration Details Applied Successfully. Print the Registration Slip.", this.Page);
                foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
                {
                    CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                    TextBox txtddno = dataitem.FindControl("txtddno") as TextBox;
                    cbRow.Visible = false;
                    txtddno.Enabled = false;
                }
                btnPrintRegSlip.Visible = true;
                txtRollNo.Enabled = false;
                btnSubmit.Visible = false;

                foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
                {
                    CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                    if (cbRow.Checked == true)
                    {
                        cbRow.Enabled = true;
                    }
                }
            }
            else
                objCommon.DisplayMessage(updDetails, "Review Registration Failed! Error in saving record.", this.Page);
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_RevaluationRegistration.SubmitCourses() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");

        }
    }  

    decimal reval_Amt = 3000;
    static decimal CourseAmt = 0;
    static decimal CourseCount = 0;
    protected void chkAccept_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            divTotalCourseAmount.Visible = true;
            //LoadRevalFeeAmount();
            CheckBox chk = sender as CheckBox;

            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {
                CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                Label lblExtermark = dataitem.FindControl("lblExtermark") as Label;

                if (cbRow.Checked == true)
                {
                     CourseAmt = Convert.ToDecimal(CourseAmt) + Convert.ToDecimal(reval_Amt);
                }

            }

            lblTotalAmount.Text = CourseAmt.ToString();           
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
            ShowReport("Review Registration Slip", "rptReviewRegistration.rpt");
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
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(ViewState["idno"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ViewState["SESSIONNO"]);//2 for reval
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
}


