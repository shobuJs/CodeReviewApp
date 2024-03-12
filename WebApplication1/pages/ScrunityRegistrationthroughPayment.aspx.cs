using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using System.Data;
using DotNetIntegrationKit;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml;
using System.Net;
using _NVP;
using System.Collections.Specialized;
using EASendMail;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;

using Microsoft.WindowsAzure.Storage;
using System.Web.Services;
using System.Web.Script.Services;

public partial class ACADEMIC_ScrunityRegistrationthroughPayment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    StudentController objSC = new StudentController();
    StudentRegist objSR = new StudentRegist();

    FeeCollectionController feeController = new FeeCollectionController();
    string Semesterno = string.Empty;
    string Degreeno = string.Empty;
    string branchno = string.Empty;
    string college_id = string.Empty;
    int counter = 0;
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

                //Set the Page Title
                 Page.Title = Session["coll_name"].ToString();

                //To Get IP Address of user 
                 ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                objCommon.SetLabelData("1");
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));

                if (Session["usertype"].ToString() == "2")
                {
                    ViewState["action"] = "add";
                    divNote.Visible = true;
                    pnlSearch.Visible = false;
                    totamtpay.Text = "";
                }
                else if (Session["usertype"].ToString() == "1")
                {
                    ViewState["action"] = "add";
                    pnlSearch.Visible = false;
                    btnSubmit.Visible = false;
                }
            }

        }
    }
    #endregion
    #region Methods



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
    #endregion

   
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "1")
        {
            pnlSearch.Visible = true;
            divCourses.Visible = false;

        }
        if (Session["usertype"].ToString() == "2")
        {
            divCourses.Visible = true;

        }

        divNote.Visible = false;
        //divCourses.Visible = true;
        ShowDetails();
        btnPrcdToPay.Visible = false;
    }
    private void ShowDetails()
    {
        int idno = 0;
        //btnSubmit.Attributes.Add("Style", "color:yellow");

        if (Session["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
            ViewState["idno"] = idno;

        }
        else if (Session["usertype"].ToString() == "1")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
            btnSubmit.Visible = false;
        }
        try
        {
            if (idno > 0)
            {

                string SP_Name1 = "PKG_STUDENT_SP_RET_STUDENT_BYID_FOR_EXAM_SCRUTINY";
                string SP_Parameters1 = "@P_IDNO";
                string Call_Values1 = "" + idno;

                DataSet dsStudent = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);

                if (dsStudent != null && dsStudent.Tables[0].Rows.Count > 0)
                {
                        //if (dsStudent != null && dsStudent.Tables[1].Rows.Count > 0)
                        //{
                            //if (dsStudent.Tables[1].Rows[0]["SESSIONNO"].ToString() == "0")
                            //{
                            //    objCommon.DisplayMessage(updDetails, "Scrunity Registration Activity Stop, Please Contact Registry Team !!!", this.Page);
                            //    return;
                            //}
                            //else
                            //{
                                divCourses.Visible = true;

                                lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                                lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                                lblFatherName.Text = dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                                lblMotherName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                                lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                                lblsession.Text = dsStudent.Tables[0].Rows[0]["SESSION_NAME"].ToString();
                                //lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                                lblScheme.Text = dsStudent.Tables[0].Rows[0]["PROGRAM"].ToString();
                                lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                                lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                                lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                                lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                                lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                                //lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();
                                hdfCategory.Value = dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                                hdfDegreeno.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                                lblCollege.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                                hdnCollege.Value = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                                hdnEmailID.Value = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString();
                                hdnMobileNo.Value = dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                                ViewState["COLLEGE_ID"] = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                                ViewState["DEGREENO"] = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                                ViewState["BRANCHNO"] = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                                ViewState["SEMESTERNO"] = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                                // lblCollege.ToolTip = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                                ViewState["IDNO"] = lblName.ToolTip;
                                ViewState["SESSIONNO"] = dsStudent.Tables[1].Rows[0]["SESSIONNO"].ToString();

                                BindCourseListForReval();

                                FillExamFees();
                            //}
                        //}
                        //else
                        //{
                        //    objCommon.DisplayMessage(updDetails, "Scrunity Registration Activity Stop, Please Contact Registry Team !!!", this.Page);
                        //    return;
                        //}
                }
                else
                {
                    //if (dsStudent.Tables[1].Rows[0]["SESSIONNO"].ToString() == "0")
                    //{
                    //    objCommon.DisplayMessage(updDetails, "Scrunity Registration Activity Stop, Please Contact Registry Team !!!", this.Page);
                    //    return;
                    //}
                    //else
                    //{
                    //    objCommon.DisplayMessage(updDetails, "Student Details Not Found !!!", this.Page);
                    //}
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
    private void FillExamFees()
    {
        ActivityController objActController = new ActivityController();
        int feeitemid = 0;
       
        feeitemid = 1;

        DataSet ds = objActController.GetFeeItemAmounntENDSEM(Convert.ToInt32(ViewState["SESSIONNO"]), feeitemid);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            string amountth = ds.Tables[0].Rows[0]["AMOUNT"].ToString();
            int index = amountth.IndexOf('.');
            string result = amountth.Substring(0, index);
            hdncoursefee.Value = amountth.ToString();
            ViewState["fees"] = hdncoursefee.Value;
        }
        feeitemid = 5;
        //    ds = objActController.GetFeeItemAmounntENDSEM(sessionno, feeitemid, scheme);
        ds = objActController.GetFeeItemAmounntENDSEM(Convert.ToInt32(ViewState["SESSIONNO"]), feeitemid);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            string amountth = ds.Tables[0].Rows[0]["AMOUNT"].ToString();
            int index = amountth.IndexOf('.');
            string result = amountth.Substring(0, index);
            hdncoursefee.Value = amountth.ToString();
            lblpaper.Text = result;
            lblpaper.Visible = true;

        }


    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtEnrollno.Text.Trim() == string.Empty)
        {
            objCommon.DisplayMessage(this.updDetails, "Please Enter Student Id.", this.Page);
        }
        else
        {
                ShowDetails();
        }
    }
    private void BindCourseListForReval()
    {
        DataSet dsCurrCourses = null;
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO=" + ViewState["SESSIONNO"].ToString(), "");
        ddlSession.SelectedValue = ViewState["SESSIONNO"].ToString();
        dsCurrCourses = objSC.GetCourseFor_RevalPhotocopyChallenge(Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(ViewState["SESSIONNO"].ToString()), Convert.ToInt32(ViewState["SEMESTERNO"]), Convert.ToInt32(ViewState["DEGREENO"]), Convert.ToInt32(lblScheme.ToolTip));
        if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
        {

            lvCurrentSubjects.DataSource = dsCurrCourses.Tables[0];
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = true;
            trNote.Visible = true;

            if (Session["usertype"].ToString() == "2")
            {
                btnSubmit.Visible = true;
                 
            }
            else
            btnSubmit.Visible = false;
            btnRemoveList.Visible = true;

            if (!String.IsNullOrEmpty(dsCurrCourses.Tables[0].Rows[0]["TOTAL_AMT"].ToString()) && dsCurrCourses.Tables[0].Rows[0]["TOTAL_AMT"].ToString() != "0")
            {
                lblSelectedCourseFee.Text = dsCurrCourses.Tables[0].Rows[0]["TOTAL_AMT"].ToString();
                hdnSelectedCourseFee.Value = dsCurrCourses.Tables[0].Rows[0]["TOTAL_AMT"].ToString();
                ViewState["AMOUNT"] = dsCurrCourses.Tables[0].Rows[0]["TOTAL_AMT"].ToString();
            }
            else
            {
                lblSelectedCourseFee.Text = "0";
                hdnSelectedCourseFee.Value = "0";
            }
        }
        else
        {

            lvCurrentSubjects.Visible = false;
            trNote.Visible = false;
            lvCurrentSubjects.DataSource = null;
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = false;
            objCommon.DisplayMessage(updDetails, "No Course found in Allotted Scheme and Semester.\\n In case of any query contact Admin", this.Page);
            btnSubmit.Visible = false;
            btnRemoveList.Visible = false;
            totamtpay.Text = string.Empty;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            SubmitCourses();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ScrunityRegistrationthroughPayment.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    public void SubmitCourses()
    {
        try
        {
            int result = 0;
            Boolean selection = false;
            int opertion = 0;
            int ApproveStatus = 0;
            string RECHECKORREASS = string.Empty;string CA_Marks = string.Empty;string amount=string.Empty;
            string sems = string.Empty;
            if (lvCurrentSubjects.Items.Count > 0)
            {
                foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
                {
                    //Get Student Details from lvStudent
                    CheckBox chkrechecking = dataitem.FindControl("chkRedressal") as CheckBox;
                    if (chkrechecking.Checked && chkrechecking.Enabled == true)
                    {
                        selection = true;
                        objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + "$";
                        string ext = (dataitem.FindControl("hdfmarks") as HiddenField).Value.ToString() == "" ? "0" : (dataitem.FindControl("hdfmarks") as HiddenField).Value.ToString();
                        objSR.EXTERMARKS += ext + "$";
                        objSR.CCODES += (dataitem.FindControl("lblCCode") as Label).Text + "$";
                        objSR.GRADES += (dataitem.FindControl("hdfgrade") as HiddenField).Value + "$";
                        RECHECKORREASS += 1 + "$";
                        amount += ViewState["fees"] + "$";
                        objSR.SCHEMENO = Convert.ToInt32((dataitem.FindControl("hdnschemeno") as HiddenField).Value);
                        CA_Marks += (dataitem.FindControl("hdnCAmarks") as HiddenField).Value.ToString() == "" ? "0" : (dataitem.FindControl("hdnCAmarks") as HiddenField).Value.ToString() + "$";
                    }
                }
                if (!selection)
                {
                    objSR.COURSENOS = "0";
                    objSR.EXTERMARKS = "0";
                    objSR.CCODES = "0";
                    CA_Marks = "0";
                    objCommon.DisplayMessage(this.updDetails, "Please Select atleast one course in course list.", this.Page);
                    return;
                }

                objSR.COURSENOS = objSR.COURSENOS.TrimEnd('$');
                objSR.EXTERMARKS = objSR.EXTERMARKS.TrimEnd('$');
                objSR.CCODES = objSR.CCODES.TrimEnd('$');
                RECHECKORREASS = RECHECKORREASS.TrimEnd('$');
                objSR.SESSIONNO = Convert.ToInt32(ViewState["SESSIONNO"].ToString());
                objSR.IDNO = Convert.ToInt32(ViewState["IDNO"]);
                objSR.IPADDRESS = ViewState["ipAddress"].ToString();
                objSR.COLLEGE_CODE = Session["colcode"].ToString();
                objSR.USERTTYPE = Convert.ToInt32(Session["usertype"]);
                objSR.UA_NO = Convert.ToInt32(Session["userno"]);
                objSR.GRADES = objSR.GRADES.TrimEnd('$');
                objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip);
                CA_Marks = CA_Marks.TrimEnd('$');
                amount = amount.TrimEnd('$');
                double totalamt = Convert.ToDouble(hdnSelectedCourseFee.Value);    //Convert.ToDouble(hdnSelectedCourseFee.Value);
                ApproveStatus = Session["usertype"].ToString() == "2" ? 0 : 1;
                if (ViewState["action"] == "add")
                {
                    opertion = 0;
                }
                else
                {
                    opertion = 1;
                }
                result = objSReg.AddUpdateScrutinyRegisterationthroughPayment(objSR, ApproveStatus, opertion, RECHECKORREASS, 1, totalamt, "SRF", CA_Marks, amount);
                if (result > 0)
                {
                    objCommon.DisplayMessage(this.updDetails, "Selected Courses Saved Sucessfully !!!", this.Page);
                    BindCourseListForReval();
                    btnReport.Visible = true; btnPayment.Visible = true;
                    SubmitData();
                }
                else
                {
                    objCommon.DisplayMessage(updDetails, "Failed To Registered Courses", this.Page);
                }
            }

        }

        catch (Exception ex)
        {
            if (Session["usertype"].ToString() == "1")
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ScrunityRegistrationthroughPayment.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
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
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            int sessionno = Convert.ToInt32(Session["SESSIONNO"]);
            int idno = 0;
            if (Session["usertype"].ToString() == "2")
            {
                idno = Convert.ToInt32(Session["idno"]);
            }
            else if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "3")
            {
                idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
            }

            string SP_Name2 = "PKG_RPT_GET_COURSE_FOR_REVALUATION";
            string SP_Parameters2 = "@P_SESSIONNO,@P_IDNO";
            string Call_Values2 = "" + Convert.ToInt32(Session["Session_No"].ToString()) + "," + idno + "";
            DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (dsStudList.Tables[0].Rows.Count > 0 && dsStudList != null)
            {
                ShowReport("ExamRegistrationSlip", "rptApplicationforRevaluation.rpt");
            }
            else
            {
                objCommon.DisplayMessage(this.updDetails, "Courses are not found", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ScrunityRegistrationthroughPayment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
       
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        int sessionno = Convert.ToInt32(Session["SESSIONNO"]);
        int idno = 0;
        if (Session["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "3")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
        }
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + Convert.ToInt32(Session["SESSIONNO"].ToString()) + "";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updDetails, this.updDetails.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ScrunityRegistrationthroughPayment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void lvCurrentSubjects_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        CheckBox chkAccept = e.Item.FindControl("chkRedressal") as CheckBox;
        //HiddenField hdnRevalApprove = e.Item.FindControl("hdnRevalApprove") as HiddenField;
        //if (hdnRevalApprove.Value == "1" && Session["usertype"].ToString() == "2")   
        HiddenField hnRecheck = e.Item.FindControl("RECHECKORREASS") as HiddenField;
        HiddenField hdfrecon = e.Item.FindControl("hdfrecon") as HiddenField;

        if (chkAccept.Checked == true && Session["usertype"].ToString() == "2" && Convert.ToInt32(hdfrecon.Value) == 1)  
        {
            lvCurrentSubjects.Enabled = false;
            btnSubmit.Enabled = false;
            btnReport.Visible = true;
            btnPayment.Visible = true;
            lblRegStatus.Visible = true;
            lblRegStatus.Text = "You can not change the Modules as it's approved.";
        }
        else if (Session["usertype"].ToString() == "1" && Convert.ToInt32(hdfrecon.Value) == 1)
        {
            lvCurrentSubjects.Enabled = false;
            btnReport.Visible = true;
            btnPayment.Visible = true;
            lblRegStatus.Visible = false;
         
        }
        if (chkAccept.Checked == true)
        {
            btnPayment.Visible = true;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(HttpContext.Current.Request.Url.ToString());
    }
    protected void btnRemoveList_Click(object sender, EventArgs e)
    {
        lvCurrentSubjects.DataSource = null;
        lvCurrentSubjects.Visible = false;
        btnSubmit.Visible = false;
        btnRemoveList.Visible = false;
        Response.Redirect(HttpContext.Current.Request.Url.ToString());
    }


    protected void btnPayment_Click(object sender, EventArgs e)
    {
        try
        {
            SubmitData();
        }
        catch (Exception ex)
        { 
        
        }
    }

    private void CreateCustomerRef()
    {
        Random rnd = new Random();
        int ir = rnd.Next(01, 10000);

        lblOrderID.Text = Convert.ToString(Convert.ToInt32(Session["idno"]) + Convert.ToString(ViewState["SESSIONNO"]) + Convert.ToString(ViewState["SEMESTERNO"]) + ir);
        txtOrderid.Text = lblOrderID.Text;
        Session["ERPORDERIDRESPONSE"] = lblOrderID.Text;
    }

    private void SubmitData()
    {
        try
        {
            if (Convert.ToString(ViewState["AMOUNT"]) == "0" || Convert.ToString(ViewState["AMOUNT"]) == "0.00" || Convert.ToString(ViewState["AMOUNT"]) == "")
            {
                objCommon.DisplayMessage(this.Page, "Total Amount Cannot be Less than or Equal to Zero !!", this.Page);
                return;
            }
            CreateCustomerRef();
            
            int result = 0;
            int DM_NO = 0;

            DM_NO = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "DM_NO", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + " AND SESSIONNO=" + Convert.ToInt32(ViewState["SESSIONNO"]) + " AND ISNULL(CAN,0) = 0 AND ISNULL(DELET,0) = 0 AND SEMESTERNO=" + Convert.ToInt32(ViewState["SEMESTERNO"]) + "and RECIEPT_CODE=" + "'" + Convert.ToString("SRF") + "'"));
            if (DM_NO > 0)
            {
                result = feeController.InsertProrataModuleRegistration_DCR(DM_NO, Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(ViewState["SESSIONNO"]), Convert.ToInt32(ViewState["SEMESTERNO"]), Convert.ToString(lblOrderID.Text), 1, Convert.ToInt32(1));
            }
            else
            {
                objCommon.DisplayUserMessage(this.Page, "Demand not created!", this.Page);
                return;
            }
            if (result > 0)
            {
                DataSet ds = null;
                ds = feeController.GetOnlineTrasactionOnlineOrderID(Convert.ToInt32(ViewState["idno"]), Convert.ToString(lblOrderID.Text));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string serviceCharge = "";

                    if (Convert.ToString(ds.Tables[0].Rows[0]["ORDER_ID"]) == lblOrderID.Text)
                    {

                        decimal perCharge = (Convert.ToDecimal(ViewState["AMOUNT"]) * Convert.ToDecimal(ds.Tables[0].Rows[0]["SERVICE_CHARGE_PER"].ToString())) / 100;
                            hdfServiceCharge.Value = Convert.ToString(perCharge);
                            serviceCharge = Convert.ToDecimal(hdfServiceCharge.Value).ToString("N");
                            txtOrderid.Text = lblOrderID.Text;
                            txtAmountPaid.Text = ViewState["AMOUNT"].ToString();

                            txtServiceCharge.Text = serviceCharge;//hdfServiceCharge.Value;
                            decimal text = Convert.ToDecimal(ViewState["AMOUNT"]) + Convert.ToDecimal(hdfServiceCharge.Value);
                            string FinalAmount = text.ToString("N");
                            txtAmountPaid.Text = FinalAmount;//text.ToString();
                            txtTotalPayAmount.Text = Convert.ToDecimal(ViewState["AMOUNT"]).ToString("N");//hdfAmount.Value;
                            ViewState["FinalAmountPaid"] = text;
                            SendTransaction();
                        
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#myModal33').modal('show')", true);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Something went wrong , Please try again !", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Failed To Done Online Payment.", this.Page);
                }
            }
            else
            {
                lblStatus.Visible = true;
                objCommon.DisplayUserMessage(Page, "Failed To Done Online Payment.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(this.Page, "Demand not created!", this.Page);
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.SubmitData-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void SendTransaction()
    {
        System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)768 | (System.Net.SecurityProtocolType)3072;
        String result = null;
        String gatewayCode = null;
        String response = null;

        // get the request form and make sure to UrlDecode each value in case special characters used
        NameValueCollection formdata = new NameValueCollection();

        Merchant merchant = new Merchant();

        // [Snippet] howToConfigureURL - start
        StringBuilder url = new StringBuilder();
        if (!merchant.GatewayHost.StartsWith("http"))
            url.Append("https://");
        url.Append(merchant.GatewayHost);

        merchant.GatewayUrl = url.ToString();
        // [Snippet] howToConfigureURL - end

        Connection connection = new Connection(merchant);

        // [Snippet] howToConvertFormData -- start
        StringBuilder data = new StringBuilder();
        data.Append("merchant=" + merchant.MerchantId);
        data.Append("&apiUsername=" + merchant.Username);
        data.Append("&apiPassword=" + merchant.Password);

        // add each key and value in the form data
        formdata.Add("apiOperation", "CREATE_CHECKOUT_SESSION");

        //formdata.Add("interaction.returnUrl", "http://localhost:55158/PresentationLayer/OnlineResponse.aspx");

        string returnurl = System.Configuration.ConfigurationManager.AppSettings["ReturnUrl"];
        formdata.Add("interaction.returnUrl", returnurl);
        // formdata.Add("interaction.returnUrl", "http://localhost:59566/PresentationLayer/OnlineResponse.aspx");

        formdata.Add("interaction.operation", "PURCHASE");
        formdata.Add("order.id", lblOrderID.Text);
        formdata.Add("order.currency", "LKR");
        formdata.Add("order.amount", Convert.ToString(ViewState["FinalAmountPaid"]));
        formdata.Add("order.description", Convert.ToString(Session["idno"]));

        foreach (string key in formdata)
        {
            data.Append("&" + key.ToString() + "=" + HttpUtility.UrlEncode(formdata[key], System.Text.Encoding.GetEncoding("ISO-8859-1")));
        }
        // [Snippet] howToConvertFormData -- end

        response = connection.SendTransaction(data.ToString());

        // [Snippet] howToParseResponse - start
        NameValueCollection respValues = new NameValueCollection();
        if (response != null && response.Length > 0)
        {
            String[] responses = response.Split('&');
            foreach (String responseField in responses)
            {
                String[] field = responseField.Split('=');
                respValues.Add(field[0], HttpUtility.UrlDecode(field[1]));
            }
        }
        // [Snippet] howToParseResponse - end

        result = respValues["result"];

        // Form error string if error is triggered
        if (result != null && result.Equals("ERROR"))
        {
            String errorMessage = null;
            String errorCode = null;

            String failureExplanations = respValues["explanation"];
            String supportCode = respValues["supportCode"];

            if (failureExplanations != null)
            {
                errorMessage = failureExplanations;
            }
            else if (supportCode != null)
            {
                errorMessage = supportCode;
            }
            else
            {
                errorMessage = "Reason unspecified.";
            }

            String failureCode = respValues["failureCode"];
            if (failureCode != null)
            {
                errorCode = "Error (" + failureCode + ")";
            }
            else
            {
                errorCode = "Error (UNSPECIFIED)";
            }
        }

        // error or not display what response values can
        gatewayCode = respValues["response.gatewayCode"];
        if (gatewayCode == null)
        {
            gatewayCode = "Response not received.";
        }

        // build table of NVP results and add to panel for results

        int shade = 0;
        foreach (String key in respValues)
        {
            if (key == "session.id")
            {
                Session["ERPPaymentSessionSCRUTINY"] = respValues[key];
            }
            if (key == "successIndicator")
            {
                Session["ERPsuccessIndicator"] = respValues[key];
            }
            if (key == "session.version")
            {
                Session["ERPsessionversion"] = respValues[key];
            }
        }
        string SP_Name1 = "PKG_ACD_UPDATE_PAYMENT_SUCCESS_INDICATOR";
        string SP_Parameters1 = "@P_IDNO,@P_ERPSUCCESSINDICATOR,@P_ERPSESSIONVERSION,@P_ORDER_ID,@P_OUTPUT";
        string Call_Values1 = "" + Convert.ToInt32(Session["idno"]) + "," + Convert.ToString(Session["ERPsuccessIndicator"]) + "," +
        Convert.ToString(Session["ERPsessionversion"]) + "," + Convert.ToString(lblOrderID.Text) + ",0";

        string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);
    }
    protected void lnkPay_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "Checkout.showPaymentPage();", true);
    }
}

