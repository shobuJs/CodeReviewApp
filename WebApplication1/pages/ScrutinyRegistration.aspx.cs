//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Scrutiny REGISTRATION BY STUDENT AND ADMIN                                     
// CREATION DATE : 09-APRIL-2016
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

public partial class ACADEMIC_ScrutinyRegistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    StudentController objSC = new StudentController();
    StudentRegist objSR = new StudentRegist();
    ActivityController objActController = new ActivityController();

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

                this.PopulateDropDownList();

                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                string IPADDRESS = string.Empty;

                IPADDRESS = ip.AddressList[0].ToString();
                ViewState["ipAddress"] = IPADDRESS;

                //Check for Activity On/Off for Scrutiny registration.
                if (CheckActivity())
                {
                    ViewState["action"] = "add";
                    ViewState["idno"] = "0";

                    LoadFacultyPanel();
                }
                else
                {
                    divCourses.Visible = false;
                }
            }
        }

        //Set the Page Title
        Page.Title = Session["coll_name"].ToString();
        divMsg.InnerHtml = string.Empty;
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

    private bool CheckActivity()
    {
        bool ret = true;
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                ret = false;
            }

            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                ret = false;
            }
        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            ret = false;
        }
        dtr.Close();
        return ret;
    }

    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'  and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
        ddlSession.SelectedIndex = 1;
        ddlSession.Focus();
    }

    private void LoadFacultyPanel()
    {
        divCourses.Visible = true;
        tblSession.Visible = true;
        txtRollNo.Text = string.Empty;
    }

    #endregion

    #region Show Functionality

    protected void btnShow_Click(object sender, EventArgs e)
    {
        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");

        if (idno == "")
        {
            objCommon.DisplayMessage("Student Not Found for Entered Registration No.[" + txtRollNo.Text.Trim() + "]", this.Page);
        }

        ViewState["idno"] = idno;

        if (string.IsNullOrEmpty(ViewState["idno"].ToString()) || ViewState["idno"].ToString() == "0")
        {
            objCommon.DisplayMessage("Student with Registration No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
            return;
        }
        this.ShowDetails();
        btnSubmit.Visible = false;
        btnPrintRegSlip.Visible = false;
        ViewState["action"] = "edit";
        FillSemester();
        ddlSemester.Enabled = true;
        if (ddlSemester.Items.Count == 2)
        {
            ddlSemester.SelectedIndex = 1;
            BindCourseListForReval();
            IsRevaluationApproved();
        }
        else
        {
            lvCurrentSubjects.DataSource = null;
            lvCurrentSubjects.DataBind();
        }
        txtRollNo.Enabled = false;
    }
    //Show Selected Student Information 
    private void ShowDetails()
    {
        try
        {
            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_COLLEGE_MASTER COLL ON (S.COLLEGE_ID = COLL.COLLEGE_ID) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO)", "S.IDNO,DG.DEGREENAME", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO,S.SEMESTERNO,ISNULL(S.SCHEMENO,0)SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE S.PHYSICALLY_HANDICAPPED WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH, COLL.COLLEGE_NAME", "S.IDNO = " + ViewState["idno"].ToString(), string.Empty);
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                    lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                    lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";
                    lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                    lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                    lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();
                    lblCollegeName.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
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

    private void FillSemester()
    {
        objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO=S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND IDNO=" + ViewState["idno"].ToString(), "SR.SEMESTERNO");
    }

    private void BindCourseListForReval()
    {
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);

        DataSet dsCurrCourses = null;

        //Show Courses for Scrutiny
        dsCurrCourses = objSC.GetCourseFor_Reval(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), 2);
        if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
        {
            btnSubmit.Visible = true;
            lvCurrentSubjects.DataSource = dsCurrCourses.Tables[0];
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = true;
            checkSubject();

        }
        else
        {
            btnSubmit.Visible = false;
            lvCurrentSubjects.DataSource = null;
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = false;
            objCommon.DisplayMessage("No Course found in Allotted Scheme and Semester.\\nIn case of any query contact administrator.", this.Page);
        }
    }

    private void checkSubject()
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("ACD_REVAL_RESULT", "courseno", "idno", "APP_TYPE = 'SCRUTINY' AND IDNO=" + Convert.ToInt32(ViewState["idno"]) + "AND ISNULL(CANCEL,0)=0 AND sessionno=" + Convert.ToInt32(ddlSession.SelectedValue), "courseno");

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
                            i++;
                        }
                    }
                }
            }
        }
    }

    private void IsRevaluationApproved()
    {
        string ApproveStatus = objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(CCODE)", "ISNULL(REV_APPROVE_STAT,0)=1 AND APP_TYPE = 'SCRUTINY' AND ISNULL(CANCEL,0)=0 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue));
        if (ApproveStatus != "0")
        {
            btnPrintRegSlip.Visible = true;
        }
        else
        {
            btnPrintRegSlip.Visible = false;
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            BindCourseListForReval();
            IsRevaluationApproved();
        }
        else
        {
            btnSubmit.Visible = false;
            btnPrintRegSlip.Visible = false;
            lvCurrentSubjects.DataSource = null;
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = false;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["idno"] = "0";
        divCourses.Visible = true;
        ddlSession.Enabled = false;
        txtRollNo.Text = string.Empty;
        txtRollNo.Enabled = true;
        lvCurrentSubjects.DataSource = null;
        lvCurrentSubjects.DataBind();
        tblInfo.Visible = false;
        btnSubmit.Visible = false;
        lblErrorMsg.Text = string.Empty;
    }

    #endregion

    #region Submit Functionality

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            SubmitCourses();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void SubmitCourses()
    {
        try
        {
            int result = 0;
            Boolean selection = false;
            int opertion = 0;
            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {
                //Get Student Details from lvStudent
                CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                if (cbRow.Checked == true)
                {
                    selection = true;
                    objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + "$";
                    objSR.EXTERMARKS += ((dataitem.FindControl("lblExtermark")) as Label).Text + "$";
                    objSR.CCODES += (dataitem.FindControl("lblCCode") as Label).Text + "$";
                }
                objSR.SCHEMENO = Convert.ToInt32((dataitem.FindControl("lblSEMSCHNO") as Label).ToolTip);
            }

            if (!selection)
            {
                objSR.COURSENOS = "0";
                objSR.EXTERMARKS = "0";
                objSR.CCODES = "0";
            }
            objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objSR.IDNO = Convert.ToInt32(ViewState["idno"]);
            objSR.IPADDRESS = ViewState["ipAddress"].ToString();
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.UA_NO = Convert.ToInt32(Session["userno"]);
            objSR.SEMESTERNO = Convert.ToInt32(ddlSemester.SelectedValue);

            if (ViewState["action"].ToString() == "add")
            {
                opertion = 0;
            }
            else
            {
                opertion = 1;
            }
            result = objSReg.AddUpdateRevalRegisteration(objSR, opertion, 0, "SCRUTINY");
            if (result > 0)
            {

                objCommon.DisplayMessage("Student registered successfully for scrutiny.", this.Page);
                ShowReport("Scrutiny RegistrationSlip", "rptApplicationforRevaluation.rpt");
                btnPrintRegSlip.Visible = true;
                BindCourseListForReval();

            }
            else
            {
                objCommon.DisplayMessage("No course registered.", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Session["usertype"].ToString() == "1")
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

    private void IsRevaluationRegistered()//Added by Prity on date 11-12-2015
    {
        string RegisteredStatus = objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(CCODE)", "REV_APPROVE_STAT=0 AND APP_TYPE = 'SCRUTINY' AND ISNULL(CANCEL,0)=0 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue));
        if (RegisteredStatus != "0")
        {
            btnPrintRegSlip.Visible = true;
        }
        else
        {
            btnPrintRegSlip.Visible = false;
        }
    }

    #endregion

    #region Report Functionality

    //Show Revauation Registertion Slip
    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        ShowReport("Scrutiny RegistrationSlip", "rptApplicationforRevaluation.rpt");
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
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(ViewState["idno"]) + ",@P_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_UA_TYPE=" + Session["usertype"].ToString() + ",@P_SEMESTER_NO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_OPERATION_FLAG=2";
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
