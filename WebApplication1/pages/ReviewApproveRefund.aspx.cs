//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : REVIEW APPROVAL AND REFUND BY ADMIN                                     
// CREATION DATE : 06-MAY-2020
// ADDED BY      : MR.PANKAJ NAKHALE
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
using System.Drawing;


public partial class ACADEMIC_ReviewApproveRefund : System.Web.UI.Page
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
                {
                    return;
                }
                else
                {
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
        }

        //Set the Page Title
        Page.Title = Session["coll_name"].ToString();
        divMsg.InnerHtml = string.Empty;
    }

    private void PopulateDropDownList()
    {

        // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND EXAMTYPE=1 and flock=1", "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1 and PAGE_LINK = '" + Request.QueryString["pageno"].ToString() + "')", "SESSIONNO DESC");
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

                ViewState["SESSIONNO"] = Convert.ToInt32(ddlSession.SelectedValue);

                ViewState["idno"] = feeController.GetStudentIdByEnrollmentNo(txtRollNo.Text.Trim());

                tblSession.Visible = true;
                int count = 0;
                count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_REVIEW_DATA", "count(*)", "SESSIONNO=" + ViewState["SESSIONNO"] + " and IDNO=" + ViewState["idno"]));
                if (count > 0)
                {
                    if (!string.IsNullOrEmpty(ViewState["idno"].ToString()))
                    {

                        this.ShowDetails();
                        BindCourseListForReviewApply();
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updDetails, "This Student Not Done Review Registration. So Please Done Review Registration!", this.Page);
                    return;
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
                    lblDegree.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString();
                    lblDegree.ToolTip = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    tblInfo.Visible = true;
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
            dsCurrCourses = objSC.GetCourseList_ReviewAppliedStudent(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ViewState["SESSIONNO"]));

            if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
            {
                divAllCoursesFromHist.Visible = true;
                lvCurrentSubjects.DataSource = dsCurrCourses.Tables[0];
                lvCurrentSubjects.DataBind();
                lvCurrentSubjects.Visible = true;

                foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
                {
                    Button btnApprove = dataitem.FindControl("btnApprove") as Button;

                    Button btnRefund = dataitem.FindControl("btnRefund") as Button;

                    //HiddenField hfrecon = dataitem.FindControl("hfrecon") as HiddenField;

                    HiddenField hfSem = dataitem.FindControl("hfSem") as HiddenField; // for approve
                    HiddenField hfCourseName = dataitem.FindControl("hfCourseName") as HiddenField; // for Refund

                    //for approve
                    if (Convert.ToInt32(hfSem.Value) == 1)
                    {
                        btnApprove.Enabled = false;
                        btnApprove.BackColor = Color.DarkOrange;
                        btnApprove.BorderColor = Color.DarkOrange;
                    }
                    else
                    {
                        btnApprove.Enabled = true;
                    }
                    // for Refund
                    if (Convert.ToInt32(hfCourseName.Value) == 1)
                    {
                        btnRefund.Enabled = false;
                        btnRefund.BackColor = Color.DarkOrange;
                        btnRefund.BorderColor = Color.DarkOrange;

                    }
                    else
                    {
                        if (Convert.ToInt32(hfSem.Value) == 1)
                        {
                            btnRefund.Enabled = true;
                        }
                        else
                        {
                            btnRefund.Enabled = false;
                        }
                    }
                }

            }
            else
            {

                lvCurrentSubjects.DataSource = null;
                lvCurrentSubjects.DataBind();
                lvCurrentSubjects.Visible = false;
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

        lblErrorMsg.Text = string.Empty;


        CourseAmt = 0;

        divNote.Visible = false;

        divAllCoursesFromHist.Visible = false;


        //divRegisteredCoursesTotalAmt.Visible = false;

        txtRollNo.Text = string.Empty;
        ddlSession.SelectedIndex = 0;

        txtRollNo.Enabled = true;
        ddlSession.Enabled = true;
    }

    #endregion


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
            //int ret = objSReg.InsertReviewApplyReg(objSR, Convert.ToDecimal(lblTotalAmount.Text), DDNO);
            //if (ret == 1)
            //{
            //    objCommon.DisplayMessage(updDetails, "Review Registration Details Applied Successfully. Print the Registration Slip.", this.Page);
            //    foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            //    {
            //        CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
            //        TextBox txtddno = dataitem.FindControl("txtddno") as TextBox;
            //        cbRow.Visible = false;
            //        txtddno.Enabled = false;
            //    }
            //    btnPrintRegSlip.Visible = true;
            //    txtRollNo.Enabled = false;
            //    btnSubmit.Visible = false;

            //    foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            //    {
            //        CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
            //        if (cbRow.Checked == true)
            //        {
            //            cbRow.Enabled = true;
            //        }
            //    }
            //}
            //else
            //    objCommon.DisplayMessage(updDetails, "Review Registration Failed! Error in saving record.", this.Page);

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

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        Button btnApprove = (Button)(sender);
        objSR.IDNO = Convert.ToInt32(lblName.ToolTip);
        string DDNO = btnApprove.CommandArgument;
        objSR.SEMESTERNO = Convert.ToInt32(btnApprove.ToolTip);
        objSR.COURSENO = Convert.ToInt32(btnApprove.CommandName);
        int Approve = 0;
        int Flag = 1;//used for Approve
        Approve = objSC.ApproveCourseReview(objSR, DDNO, Flag);

        if (Approve == 1)
        {
            objCommon.DisplayMessage(updDetails, "Course Approve Successfully..", this.Page);
            BindCourseListForReviewApply();
        }
        else
        {
            objCommon.DisplayMessage(updDetails, "Course Approval Failed!", this.Page);
        }

    }
    protected void btnRefund_Click(object sender, EventArgs e)
    {
        Button btnApprove = (Button)(sender);
        objSR.IDNO = Convert.ToInt32(lblName.ToolTip);
        string DDNO = btnApprove.CommandArgument;
        objSR.SEMESTERNO = Convert.ToInt32(btnApprove.ToolTip);
        objSR.COURSENO = Convert.ToInt32(btnApprove.CommandName);
        int Approve = 0;
        int Flag = 2;//used for Refund
        int count = 0;
        count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_REVIEW_DATA", "count(*)", "IDNO=" + objSR.IDNO + " AND SEMESTERNO=" + objSR.SEMESTERNO + " AND COURSENO=" + objSR.COURSENO + " AND DD_NO='" + DDNO + "' and APPROVE=1"));
        if (count > 0)
        {
            Approve = objSC.ApproveCourseReview(objSR, DDNO, Flag);
        }
        else
        {
            objCommon.DisplayMessage(updDetails, "You Are Not Able To Directly Refund. You Should First Approve !", this.Page);
            return;
        }

        if (Approve == 2)
        {
            objCommon.DisplayMessage(updDetails, "Course Refund Successfully..", this.Page);
            BindCourseListForReviewApply();
        }
        else
        {
            objCommon.DisplayMessage(updDetails, "Course Refund Failed!", this.Page);
            return;
        }
    }
}


