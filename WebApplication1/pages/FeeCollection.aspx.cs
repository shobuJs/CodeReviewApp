//======================================================================================
// PROJECT NAME  : PHINMA
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Fee Collection
// CREATION DATE : 02-August-2023
// CREATED BY    : Swapnil Thakare
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections.Generic;
using AjaxControlToolkit;
using mastersofterp_MAKAUAT;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

public partial class Academic_FeeCollection : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

    FeeCollectionCounterController feeController = new FeeCollectionCounterController();
    int flag = 0;
    static int ReconCnt = 0;
    #region Page Events

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                  
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    Session["OrgId"] = 0;
                    // Fill Dropdown lists                
                    this.objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO>0", "BANKNAME");
                    this.objCommon.FillDropDownList(ddlBankT, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO>0", "BANKNAME");
           
                    this.objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNAME <> '-' AND SEMESTERNO > 0", "SEMESTERNO");//*******
                   
                    this.objCommon.FillDropDownList(ddlPaytype, "ACD_PAYTYPE", "PTYPE_CODE", "PTYPENAME", "ACTIVESTATUS=1", "PAYNO");
                    this.objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "", "");
                    this.objCommon.FillDropDownList(ddlScholarship, "ACD_SCHOLORSHIPTYPE", "SCHOLORSHIPTYPENO", "SCHOLORSHIPNAME", "", "");
                    this.objCommon.FillDropDownList(ddlAmtPaidBank, "ACD_BANK", "BANKNO", "BANKNAME", "", "BANKNAME");
                    this.objCommon.FillDropDownList(ddlSearchPanel, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 ", "SRNO");

                    Session["currentsession"] = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO) ", "FLOCK=1"));

                    ddlSearchPanel.SelectedIndex = 1;
                    ddlSearchPanel_SelectedIndexChanged(sender, e);
                    /// Set mode of Payment (like Cash, Bank, ATM, etc.)
                    if (Request.QueryString["PaymentMode"] != null && Request.QueryString["PaymentMode"].ToString() != null)
                        ViewState["PaymentMode"] = Request.QueryString["PaymentMode"].ToString();

                    /// Set receipt type for fee collection
                    if (Request.QueryString["RecType"] != null && Request.QueryString["RecType"].ToString() != null)
                        ViewState["ReceiptType"] = Request.QueryString["RecType"].ToString();

                    /// Set Page Header
                    if (Request.QueryString["RecTitle"] != null && Request.QueryString["RecTitle"].ToString() != null && Request.QueryString["RecTitle"] != string.Empty)
                        // spanPageHeader.InnerText = Request.QueryString["RecTitle"].ToUpper().ToString();
                        if (Request.QueryString["Title"] != null && Request.QueryString["Title"].ToString() != null && Request.QueryString["Title"] != string.Empty)
                            // spanPageHeader.InnerText += " " + Request.QueryString["Title"].ToUpper().ToString();

                            // clear dd details table from session
                            Session["DD_Info"] = null;

                    /// If query string contains student id param
                    /// it means this page has been requested by student search result
                    /// Show information for the student




                }
                txtEnrollNo.Focus();
                //ceDateOfReporting.EndDate = DateTime.Now.Date;   //to dissable future  Date
            }
            else
            {
                // Clear message div
                divMsg.InnerHtml = string.Empty;

                /// if postback has been done implicitly
                /// then call correspinding methods.
                if (Request.Params["__EVENTTARGET"] != null &&
                    Request.Params["__EVENTTARGET"].ToString() != string.Empty)
                {
                    if (Request.Params["__EVENTTARGET"].ToString() == "CreateDemand")
                        this.CreateDemandForCurrentFeeCriteria();
                    else if (Request.Params["__EVENTTARGET"].ToString() == "btnSearch")
                        this.ShowSearchResults(Request.Params["__EVENTARGUMENT"].ToString());
                }

                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnclear"))
                {

                }
            }
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            objCommon.SetLabelData("2");
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // dynamic page header addded by Swapnil Thakare 14/07/2021

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

        login_div.Attributes.Add("style", "display:none");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
        }
    }

    #endregion

    #region Search Student

    protected void btnShowInfo_Click(object sender, EventArgs e)
    {
        try
        {
            FeeCollectionCounterController SFB = new FeeCollectionCounterController();
            int IDNO = 0;
            if (txtEnrollNo.Text.Trim() != string.Empty)
            {
                IDNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(IDNO,0)", "ENROLLNO='" + txtEnrollNo.Text + "'") == "" ? "0" : objCommon.LookUp("ACD_STUDENT", "ISNULL(IDNO,0)", "ENROLLNO='" + txtEnrollNo.Text + "'"));
                if (IDNO == 0)
                {
                    objCommon.DisplayUserMessage(updFee, "Record not found!!", this.Page);
                    return;
                }
                ViewState["StudId"] = IDNO;


                if (ddlSemester.SelectedIndex > 0)
                {
                    // DataSet dsDueFee = objCommon.FillDropDown("ACD_DEMAND D  LEFT OUTER JOIN (SELECT IDNO,SEMESTERNO, SUM(ISNULL(DR.TOTAL_AMT,0)) DRTOTAL_AMT FROM  ACD_DCR DR WHERE IDNO=" + IDNO + "  AND RECON=1 AND PAY_MODE_CODE<>'SA' AND ISNULL(SCH_ADJ_AMT,0)=0 GROUP BY IDNO,SEMESTERNO )A  ON (A.IDNO=D.IDNO AND A.SEMESTERNO=D.SEMESTERNO)", "DISTINCT D.IDNO,D.SEMESTERNO,DBO.FN_DESC('SEMESTER',D.SEMESTERNO)SEMESTERNAME", "ISNULL(DRTOTAL_AMT,0)DRTOTAL_AMT,(ISNULL(D.TOTAL_AMT,0)) DTOTAL_AMT, (CASE WHEN D.RECIEPT_CODE = 'TF' THEN 'Admission Fees' ELSE 'Other Fees' END) FEE_TITLE", " D.IDNO = " + IDNO + " AND D.SEMESTERNO<" + ddlSemester.SelectedValue + " AND ISNULL(CAN,0)=0 AND ISNULL(DELET,0)=0", string.Empty);

                    string paytypmode = ViewState["paymentmode"].ToString();

                    if (paytypmode.Equals("B") || paytypmode.Equals("C"))
                    {

                        DataSet dsDueFee = objCommon.FillDropDown("ACD_DEMAND D  LEFT OUTER JOIN (SELECT IDNO,SEMESTERNO, SUM(ISNULL(DR.TOTAL_AMT,0)) DRTOTAL_AMT FROM  ACD_DCR DR WHERE IDNO=" + IDNO + "  AND RECON=1 GROUP BY IDNO,SEMESTERNO )A  ON (A.IDNO=D.IDNO AND A.SEMESTERNO=D.SEMESTERNO)", "DISTINCT D.IDNO,D.SEMESTERNO,DBO.FN_DESC('SEMESTER',D.SEMESTERNO)SEMESTERNAME", "ISNULL(DRTOTAL_AMT,0)DRTOTAL_AMT,(ISNULL(D.TOTAL_AMT,0)) DTOTAL_AMT, (CASE WHEN D.RECIEPT_CODE = 'TF' THEN 'Admission Fees' ELSE 'Other Fees' END) FEE_TITLE", " D.IDNO = " + IDNO + " AND D.SEMESTERNO<" + ddlSemester.SelectedValue + " AND ISNULL(CAN,0)=0 AND ISNULL(DELET,0)=0", string.Empty);
                        if (dsDueFee.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsDueFee.Tables[0].Rows.Count; i++)
                            {
                                if (dsDueFee.Tables[0].Rows[i]["DRTOTAL_AMT"].ToString() != dsDueFee.Tables[0].Rows[i]["DTOTAL_AMT"].ToString())
                                {
                                    objCommon.DisplayUserMessage(updFee, " Please Pay the Fees of " + dsDueFee.Tables[0].Rows[i]["FEE_TITLE"].ToString() + " Semester " + dsDueFee.Tables[0].Rows[i]["SEMESTERNAME"].ToString(), this.Page);
                                    return;
                                }

                            }
                        }
                    }


                    int ISCounter = Convert.ToInt32(objCommon.LookUp("ACD_COUNTER_REF", "COUNT(*)", "RECEIPT_PERMISSION IN('" + ViewState["ReceiptType"] + "')  AND UA_NO=" + Session["userno"]));//AND (REC1<>0 OR REC2<>0 OR REC3<>0 OR REC4<>0 OR REC5<>0)
                    if (ISCounter != 0)
                    {
                        int studentId = feeController.GetStudentIdByEnrollmentNo(txtEnrollNo.Text.Trim());
                        int semester = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + studentId + ""));

                        if (studentId > 0)
                        {
                            if (semester != 1)
                            {
                                int sessionno = 0;
                                int sessionmax = 0;
                                string feedback = string.Empty;
                                sessionno = Convert.ToInt16(Session["currentsession"].ToString());

                                sessionmax = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO) ", "EXAMTYPE=1 AND SESSIONNO < " + sessionno));

                                feedback = objCommon.LookUp("REFF", "Feedback_Status", "");

                                if (feedback == "True")
                                {
                                    int Feedback = Convert.ToInt16(SFB.FeedbackCount(studentId, sessionmax));

                                    if (Feedback == 1)
                                    {
                                        this.DisplayInformation(studentId);
                                    }
                                    else
                                    {
                                        objCommon.DisplayUserMessage(updFee, "Student has not provided Feedback.", this.Page);
                                    }
                                }
                                else
                                {
                                    this.DisplayInformation(studentId);
                                }
                            }
                            else
                            {
                                this.DisplayInformation(studentId);
                            }
                        }
                        else
                            objCommon.DisplayUserMessage(updFee, "No student found with given enrollment number.", this.Page);
                        return;
                    }

                    else
                        objCommon.DisplayUserMessage(updFee, "Counter is Not Assign To Generate Receipt No. Please Assign Counter For User := " + Session["userfullname"], this.Page);
                    return;
                }
                else
                    objCommon.DisplayUserMessage(updFee, "Please select semester.", this.Page);
                return;
            }
            else
                objCommon.DisplayUserMessage(updFee, "Please enter enrollment number.", this.Page);
            return;
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.btnShowInfo_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            string url = string.Empty;
            if (Request.Url.ToString().IndexOf("&id=") > 0)
                url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
            else
                url = Request.Url.ToString();

            Response.Redirect(url + "&id=" + lnk.CommandArgument);
            ddlPaytype.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowSearchResults(string searchParams)
    {
        try
        {
            //StudentSearch objSearch = new StudentSearch();
            FeeCollectionRegister objSearch = new FeeCollectionRegister();

            string[] paramCollection = searchParams.Split(',');
            if (paramCollection.Length > 2)
            {
                for (int i = 0; i < paramCollection.Length; i++)
                {
                    string paramName = paramCollection[i].Substring(0, paramCollection[i].IndexOf('='));
                    string paramValue = paramCollection[i].Substring(paramCollection[i].IndexOf('=') + 1);

                    switch (paramName)
                    {
                        case "Name":
                            objSearch.StudentName = paramValue;
                            break;
                        case "EnrollNo":
                            objSearch.EnrollmentNo = paramValue;
                            break;
                        case "IdNo":
                            objSearch.IdNo = paramValue;
                            break;
                        case "SRNO":
                            objSearch.Srno = paramValue;
                            break;
                        case "DegreeNo":
                            objSearch.DegreeNo = int.Parse(paramValue);
                            break;
                        case "BranchNo":
                            objSearch.BranchNo = int.Parse(paramValue);
                            break;
                        case "YearNo":
                            objSearch.YearNo = int.Parse(paramValue);
                            break;
                        case "SemNo":
                            objSearch.SemesterNo = int.Parse(paramValue);
                            break;
                        default:
                            break;
                    }
                }
            }
            DataSet ds = feeController.GetStudents(objSearch);
            //lvStudent.DataSource = ds;
            //lvStudent.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion

    #region Display Student Info, Previous Receipts, Current Receipt and Fee Demand Information

    public void Installment_Data(int studentId)
    {
        decimal installmentAmount = 0;
        ViewState["INSTALLMENTAMOUNT"] = 0;
        DataSet dsInstallment = feeController.GetStudentInstallmentDetailsForFeeCollection(studentId, Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(ViewState["ReceiptType"]));
        int COLLEGE_ID = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + studentId + ""));
        // COLLEGE_ID = 2 AND A.DEGREENO = 5 AND A.BRANCHNO
        DataSet StudInfo = objCommon.FillDropDown("ACD_STUDENT", "DISTINCT IDNO", "SEMESTERNO,COLLEGE_ID,DEGREENO,BRANCHNO", "IDNO=" + studentId + "", string.Empty);

        if (dsInstallment != null && dsInstallment.Tables.Count > 0 && dsInstallment.Tables[0].Rows.Count > 0)
        {
            lstInstallments.DataSource = dsInstallment;
            lstInstallments.DataBind();

            divInstallment.Visible = false;
            //      objCommon.FillDropDownList(ddlInstallment, "ACD_FEES_INSTALLMENT", "INSTALL_NO", "CONCAT(INSTALMENT_NO,' - ' ,INSTALL_AMOUNT, ' - ' ,DUE_DATE ,'_ Sem - ',SEMESTERNO ) as Installment", "IDNO=" + Convert.ToInt32(studentId) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + " AND RECIPTCODE='" + Convert.ToString(ViewState["ReceiptType"]) + "' AND DCR_NO is null AND INSTALL_STATUS=1 and isnull(RECON,0)=0 ", "INSTALL_NO"); dsDueFee.Tables[0].Rows[i]["DRTOTAL_AMT"].ToString()

           // objCommon.FillDropDownList(ddlInstallment, "ACD_FEES_CONFIGURATION A INNER JOIN ACD_FEES_CONFIGURATION_DETAILS B ON A.FEES_CONFIG_NO=B.FEES_CONFIG_NO INNER JOIN ACD_FEES_INSTALLMENT C ON C.INSTALMENT_NO = B.FEES_INSTALL_NO AND C.IDNO =" + Convert.ToInt32(studentId) + " AND C.SEMESTERNO = " + Convert.ToInt32(ViewState["semesterno"]) + "", "C.INSTALL_NO", "CONCAT(C.INSTALMENT_NO,' - ' ,C.INSTALL_AMOUNT, ' - ' ,C.DUE_DATE ,'_ Sem - ',C.SEMESTERNO ) as Installment", " A.SESSIONNO = " + Convert.ToInt32(ViewState["SESSION_NO"].ToString()) + " AND A.COLLEGE_ID = " + Convert.ToInt32(StudInfo.Tables[0].Rows[0]["COLLEGE_ID"].ToString()) + " AND A.DEGREENO = " + Convert.ToInt32(StudInfo.Tables[0].Rows[0]["DEGREENO"].ToString()) + " AND A.BRANCHNO = " + Convert.ToInt32(StudInfo.Tables[0].Rows[0]["BRANCHNO"].ToString()) + " AND ISNULL(B.CAN,0) = 0 AND C.IDNO=" + Convert.ToInt32(studentId) + " AND RECIPTCODE='" + Convert.ToString(ViewState["ReceiptType"]) + "' AND C.DCR_NO is null AND C.INSTALL_STATUS=1 and isnull(C.RECON,0)=0 ", "B.INSTALL_NO");


            //ddlInstallment.SelectedIndex = 1;
            txtTotalAmount.Enabled = true;
            divInstallmentNote.Visible = false;

            //dsInstallment.Tables[0].DefaultView.RowFilter = "INSTALL_NO = " + ddlInstallment.SelectedValue;

            dsInstallment.Tables[0].DefaultView.RowFilter = "RECON = FALSE";

            DataTable dt = (dsInstallment.Tables[0].DefaultView).ToTable();
            if (dt.Rows.Count >= 1)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToDecimal(dt.Rows[i]["PAID_AMOUNT"]) > 0)
                    {
                        installmentAmount = Convert.ToDecimal(dt.Rows[i]["PAID_AMOUNT"]);// Tables[0].Col["INSTALL_AMOUNT"].ToString());
                        break;
                    }

                }

               // installmentAmount = Convert.ToDecimal(dt.Rows[0]["INSTALL_AMOUNT"]);// Tables[0].Col["INSTALL_AMOUNT"].ToString());
            }
            else
            {
                installmentAmount = 0;
            }


            if (installmentAmount !=0)
            {
                txtTotalAmount.Text = installmentAmount.ToString();
                ViewState["INSTALLMENTAMOUNT"] = installmentAmount.ToString();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "CallMyFunction", "DevideTotalAmountFromcodebehind()", true);
               // ddlInstallment.Enabled = true;
                divCashInstallment.Visible = true;
                rdbPaymentOption.SelectedValue = "2";
                txtTotalAmount.Enabled = true;
            }
            else
            {
                divInstallment.Visible = false;
                //txtTotalAmount.Enabled = true;
                txtTotalAmount.Enabled = false;
                rdbPaymentOption.SelectedValue = "1";

            }    

            //if (Convert.ToDecimal(ViewState["BalAmount"].ToString()) >= installmentAmount)
            //{
            //    txtTotalAmount.Text = installmentAmount.ToString();
            //    ViewState["INSTALLMENTAMOUNT"] = installmentAmount.ToString();
            //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "CallMyFunction", "DevideTotalAmountFromcodebehind()", true);
            //    ddlInstallment.Enabled = false;
            //    divCashInstallment.Visible = true;
            //    rdbPaymentOption.SelectedValue = "2";
            //}
            //else 
            //{
            //    divInstallment.Visible = false;
            //    //txtTotalAmount.Enabled = true;
            //    txtTotalAmount.Enabled = false;
            //    rdbPaymentOption.SelectedValue = "1";

            //}     
        }
        else
        {
            divInstallmentNotFound.Visible = false;
            divInstallment.Visible = false;
            txtTotalAmount.Enabled = false;
            //txtTotalAmount.Enabled = true;
            rdbPaymentOption.SelectedValue = "1";
        }

       


    }

    private void DisplayInformation(int studentId)
    {
        try
        {


            #region Student Installment Details
            if (rdbPaymentOption.SelectedValue == "")
            {
                Installment_Data(studentId);

            }
            else if (rdbPaymentOption.SelectedValue == "2")
            {
                Installment_Data(studentId);
            }
            else if (rdbPaymentOption.SelectedValue == "1")
            {
                divInstallment.Visible = false;
             //   txtTotalAmount.Enabled = true;
                divInstallmentNote.Visible = false;
                //   divCashInstallment.Visible = false;
            }

            #endregion


           // #region Student Installment Details
           // decimal installmentAmount = 0;
           // DataSet dsInstallment = feeController.GetStudentInstallmentDetailsForFeeCollection(studentId, Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(ViewState["ReceiptType"]));
           // int COLLEGE_ID = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + studentId + ""));
           //// COLLEGE_ID = 2 AND A.DEGREENO = 5 AND A.BRANCHNO
           // DataSet StudInfo = objCommon.FillDropDown("ACD_STUDENT", "DISTINCT IDNO","SEMESTERNO,COLLEGE_ID,DEGREENO,BRANCHNO","IDNO=" + studentId +"", string.Empty);
           
           // if (dsInstallment != null && dsInstallment.Tables.Count > 0 && dsInstallment.Tables[0].Rows.Count > 0)
           // {
           //     divInstallment.Visible = true;
           //     //      objCommon.FillDropDownList(ddlInstallment, "ACD_FEES_INSTALLMENT", "INSTALL_NO", "CONCAT(INSTALMENT_NO,' - ' ,INSTALL_AMOUNT, ' - ' ,DUE_DATE ,'_ Sem - ',SEMESTERNO ) as Installment", "IDNO=" + Convert.ToInt32(studentId) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + " AND RECIPTCODE='" + Convert.ToString(ViewState["ReceiptType"]) + "' AND DCR_NO is null AND INSTALL_STATUS=1 and isnull(RECON,0)=0 ", "INSTALL_NO"); dsDueFee.Tables[0].Rows[i]["DRTOTAL_AMT"].ToString()

           //     objCommon.FillDropDownList(ddlInstallment, "ACD_FEES_CONFIGURATION A INNER JOIN ACD_FEES_CONFIGURATION_DETAILS B ON A.FEES_CONFIG_NO=B.FEES_CONFIG_NO INNER JOIN ACD_FEES_INSTALLMENT C ON C.INSTALMENT_NO = B.FEES_INSTALL_NO AND C.IDNO =" + Convert.ToInt32(studentId) + " AND C.SEMESTERNO = " + Convert.ToInt32(ViewState["semesterno"]) + "", "C.INSTALL_NO", "CONCAT(C.INSTALMENT_NO,' - ' ,C.INSTALL_AMOUNT, ' - ' ,C.DUE_DATE ,'_ Sem - ',C.SEMESTERNO ) as Installment", " A.SESSIONNO = " + Convert.ToInt32(ViewState["SESSION_NO"].ToString()) + " AND A.COLLEGE_ID = " + Convert.ToInt32(StudInfo.Tables[0].Rows[0]["COLLEGE_ID"].ToString()) + " AND A.DEGREENO = " + Convert.ToInt32(StudInfo.Tables[0].Rows[0]["DEGREENO"].ToString()) + " AND A.BRANCHNO = " + Convert.ToInt32(StudInfo.Tables[0].Rows[0]["BRANCHNO"].ToString()) + " AND ISNULL(B.CAN,0) = 0 AND C.IDNO=" + Convert.ToInt32(studentId) + " AND RECIPTCODE='" + Convert.ToString(ViewState["ReceiptType"]) + "' AND C.DCR_NO is null AND C.INSTALL_STATUS=1 and isnull(C.RECON,0)=0 ", "B.INSTALL_NO");


           //     ddlInstallment.SelectedIndex = 1;
           //     txtTotalAmount.Enabled = false;
           //     divInstallmentNote.Visible = true;
           //     dsInstallment.Tables[0].DefaultView.RowFilter = "INSTALL_NO = " + ddlInstallment.SelectedValue;
           //     DataTable dt = (dsInstallment.Tables[0].DefaultView).ToTable();
           //     installmentAmount = Convert.ToDecimal(dt.Rows[0]["INSTALL_AMOUNT"]);// Tables[0].Col["INSTALL_AMOUNT"].ToString());
           //     txtTotalAmount.Text = installmentAmount.ToString();
           //     ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "CallMyFunction", "DevideTotalAmountFromcodebehind()", true);
           
           //     ddlInstallment.Enabled = false;

           // }
           // else
           // {
           //     divInstallment.Visible = false;
           //     txtTotalAmount.Enabled = true;
           //     divInstallmentNote.Visible = false;
           // }
           // #endregion

            #region Display Student Information
            /// Display student's personal and academic data in 
            /// student information section
            /// 

            DataSet ds = feeController.GetStudentInfoById(studentId, Convert.ToInt32(Session["OrgId"]));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                // show student information
                this.PopulateStudentInfoSection(dr);
                divStudInfo.Style["display"] = "block";
            }
            else
            {
                objCommon.DisplayUserMessage(updFee, "Unable to retrieve student's record.", this.Page);
                return;
            }

            divPreviousReceipts.Visible = true;
            DataSet dssem = feeController.GetPreviousReceiptsData(Convert.ToInt32(studentId), Convert.ToInt32(ViewState["semesterno"]));

            if (dssem != null && ds.Tables.Count > 0 && dssem.Tables[0].Rows.Count > 0)
            {
                // Bind list view with paid receipt data 
                lvFeesDetails.DataSource = dssem;
                lvFeesDetails.DataBind();
                divFeesDetails.Visible = true;
                ViewState["DsPreviousFeeDetail"] = dssem;
            }

            #endregion

            #region Display Previous Receipts Information
            /// Display student's previously paid receipt information.
            /// These are the receipts(i.e. Fee) paid by the student during 
            /// previous semesters or part payment for current semester
            ds = feeController.GetPaidReceiptsInfoByStudId(studentId, Convert.ToInt32(ViewState["semesterno"]));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // Bind list view with paid receipt data 
                lvPaidReceipts.DataSource = ds;
                lvPaidReceipts.DataBind();
                // btnReport.Enabled = true;
            }
            else//MAKE CHANGE HERE
            {
                divHidPreviousReceipts.InnerHtml = "No Previous Receipt Found.<br/>";
            }
            divPreviousReceipts.Visible = true;
            #endregion

            #region Display Current Receipt Information
            // Set Receipt No.
            string s = this.GetNewReceiptNo();//for receipt no
            txtReceiptNo.Text = s;
            // Set default dates
            txtReceiptDate.Text = DateTime.Today.ToShortDateString();
            txtDDDate.Text = DateTime.Today.ToShortDateString();
            transdate.Text = DateTime.Today.ToShortDateString();
            divCurrentReceiptInfo.Visible = true;
            #endregion

            #region Display Fee Demand
            /// Fill and show fee items with demanded 
            /// fee amount applicable to student.
            //int semNo = Int32.Parse((GetViewStateItem("SemesterNo") != string.Empty) ? GetViewStateItem("SemesterNo") : "0");
            /////semNo = 1;
            int semNo = int.Parse(ViewState["semesterno"].ToString());// Convert.ToInt32(ddlSemester.SelectedValue);

            if (semNo == 0)
            {
                //semNo = 1;
                ddlSemester.SelectedValue = ViewState["semesterno"].ToString();
                semNo = Convert.ToInt32(ddlSemester.SelectedValue);

            }
            this.PopulateFeeItemsSection(semNo, System.DateTime.Now);
            //********divFeeCriteria.Visible = true;
            #endregion

            /// Only for GP Mumbai:
            /// Mostly cash payment is done in the college 
            /// hence setting pay type to cash(C) by default.
            //  ddlPaytype.SelectedValue = "C";


            string paytypmode = ViewState["paymentmode"].ToString();

            if (paytypmode.Equals("B"))
            {
                ddlPaytype.SelectedValue = "D";
                //txtPayType.Text = "D";
                divDDDetails.Style.Add("display", "block");
                txtDDNo.Focus();
                ddlPaytype.Focus();
            }
            else
            {
                ddlPaytype.SelectedValue = "C";
                //txtPayType.Text = "C";
                ddlPaytype.Focus();
                // divDDDetails.Style.Add("display", "none");
            }

// Temporary added            
           // string SchAmt ="0";
          //  string SchAdjAmt = "0";
            string SchAmt = (objCommon.LookUp("ACD_STUDENT_SCHOLARSHIP_DATA", "ISNULL(SHOLARSHIP_AMOUNT,0)", "IDNO=" + Convert.ToInt32(studentId) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));
            string SchAdjAmt = (objCommon.LookUp("ACD_STUDENT_SCHOLARSHIP_DATA", "ISNULL(NET_PAYABLE,0)", "IDNO=" + Convert.ToInt32(studentId) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));
            if (SchAdjAmt != string.Empty && SchAdjAmt != "0")
            {
                lblSchAdjAmt.Text = SchAdjAmt;
                lblScholarship.Attributes["style"] = "color:green;font-weight:bold";
                lblScholarship.Visible = true;
                lblSchAdjAmt.Attributes["style"] = "color:green;font-weight:bold";
                dvScholar.Visible = true;
            }
            else if (SchAmt != "0" && SchAmt != string.Empty)
            {
                lblSchAdjAmt.Text = "Scholarship Amount is Pending for this Student,Amount is" + SchAmt;
                lblScholarship.Visible = false;
                lblSchAdjAmt.Attributes["style"] = "color:red;font-weight:bold";
                dvScholar.Visible = true;
            }
            else
            {
                lblScholarship.Visible = false;
                dvScholar.Visible = false;
            }

         //   string schamt = "0";
            string schamt = (objCommon.LookUp("ACD_STUDENT_SCHOLARSHIP_DATA", "ISNULL(SHOLARSHIP_AMOUNT,0)", "IDNO=" + Convert.ToInt32(studentId) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));
            if (schamt == string.Empty && schamt == "0")
            {
                dvScholar.Visible = false;
            }




            /// Hide search student div once a student's data is populated,
            /// user should not select other student to show information, 
            /// unless and until user clicks the cancel button.
            if (flag == 1)
            {
                divStudentSearch.Visible = true;
            }
            else
            {
                divStudentSearch.Visible = false;
            }

            //ADD 12/04/2012
            //check the status in configuration page
            string chkConfig = objCommon.LookUp("ACD_CONFIG", "STATUS", "CONFIGNO=1");
            if (chkConfig == "N")
            {
                ItemsEnabled();
            }
            //DisplayExcessAmount();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.DisplayStudentInfo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void PopulateStudentInfoSection(DataRow dr)
    {
        try
        {
            #region Bind data to labels
            lblStudentName1.Text = dr["STUDNAME"].ToString();
            ViewState["STUDNAME"] = dr["STUDNAME"].ToString();
            lblSex.Text = dr["SEX"].ToString();
            lblRegistrationNo1.Text = dr["REGNO"].ToString();
            ViewState["REGNO"] = dr["REGNO"].ToString();
            lblDateOfAdm.Text = ((dr["ADMDATE"].ToString().Trim() != string.Empty) ? Convert.ToDateTime(dr["ADMDATE"].ToString()).ToShortDateString() : dr["ADMDATE"].ToString());
            //lblPaymentType.Text = dr["PAYTYPENAME"].ToString();
            lblDegreeName.Text = dr["DEGREENAME"].ToString();
            lblCourse.Text = dr["BRANCH_NAME"].ToString();
            //lblYear.Text = dr["YEARNAME"].ToString();
            lblCurSemester.Text = dr["SEMESTERNAME"].ToString();
            lblIntake1.Text = dr["BATCHNAME"].ToString();
            lblAdmissionstatus.Text = dr["ADMISSION_STATUS"].ToString();
            #endregion

            #region Show Student's Data Selected in DDLs
          

            if (ddlPaymentType.Items.FindByValue(dr["PTYPE"].ToString()) != null)
                ddlPaymentType.SelectedValue = dr["PTYPE"].ToString();
            else
                ddlPaymentType.SelectedIndex = 0;

            if (ddlScholarship.Items.FindByValue(dr["SCHOLORSHIPTYPENO"].ToString()) != null)
                ddlScholarship.SelectedValue = dr["SCHOLORSHIPTYPENO"].ToString();
            else
                ddlScholarship.SelectedIndex = 0;
            #endregion

            #region Secure imporatant data
            /// Save important data in view state to be used 
            /// in further transactions for this student 
            /// and also while saving the fee collection record.
            ViewState["StudentId"] = dr["IDNO"].ToString();
            ViewState["DegreeNo"] = dr["DEGREENO"].ToString();
            ViewState["BranchNo"] = dr["BRANCHNO"].ToString();
            ViewState["YearNo"] = dr["YEAR"].ToString();
           // ViewState["semesterno"] = dr["SEMESTERNO"].ToString();
            //ddlSemester.SelectedValue;
            ViewState["AdmBatchNo"] = dr["ADMBATCH"].ToString();
            ViewState["PaymentTypeNo"] = dr["PTYPE"].ToString();
            #endregion

            this.objCommon.FillDropDownList(ddlCurrency, "ACD_CURRENCY_TITLE A INNER JOIN ACD_CURRENCY B ON (A.CUR_NO = B.CUR_NO) ", "distinct A.CUR_NO", "B.CUR_NAME", "RECIEPT_CODE='" + ViewState["ReceiptType"] + "' AND PAYTYPENO=" + Convert.ToInt16(ViewState["PaymentTypeNo"]) + "", "A.CUR_NO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.PopulateStudentInfoSection() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void PopulateFeeItemsSection(int semesterNo, DateTime TransDate)
    {
        try
        {
            int status = 0;
            int Payment_Option = 0;
            /// Get fee demand of the student for given semester no.
            /// if demand found then display fee items. Use status variable to 
            /// flag the demand status. status = -99 means demand not found.
            int studId = Int32.Parse((GetViewStateItem("StudentId") != string.Empty) ? GetViewStateItem("StudentId") : "0");
     
            DataSet ds = null;
            if (rdbPaymentOption.SelectedValue == "1")
            {
                Payment_Option = 1;
            }
    
            string datetm = TransDate.ToString("dd-MMM-yyyy");
            ds = GetFeeItems_Data(Convert.ToInt32(Session["currentsession"]), studId, semesterNo, GetViewStateItem("ReceiptType"), Convert.ToInt32(ddlExamType.SelectedValue), Convert.ToInt32(ddlCurrency.SelectedValue), Convert.ToInt16(ViewState["PaymentTypeNo"]), ref status, datetm,Payment_Option);
            if (status != -99 && ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string CollegeId = objCommon.LookUp("ACD_DEMAND D INNER JOIN ACD_STUDENT S ON(D.IDNO=S.IDNO)", "S.COLLEGE_ID", "D.IDNO=" + Convert.ToInt32(ViewState["StudentId"]) + " AND RECIEPT_CODE='TF' AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + " AND D.SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]));

                if (CollegeId == "")
                {
                    CollegeId = "0";
                }

                ViewState["COLLEGE_ID"] = CollegeId;
           
                string FeeHead = objCommon.LookUp("ACD_FEE_TITLE", "ISNULL(FEE_HEAD,'')", "RECIEPT_CODE='" + GetViewStateItem("ReceiptType") + "'");  //ISNULL(ISREACTIVATESTUDENT,0)=1 AND 

                if (!string.IsNullOrEmpty(FeeHead))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        // below control added by Shailendra K on dated 18.05.2023 as per ticket No: 43569 discussed with Dr.Manoj sir.
                        lblCalculateLateFine.Visible = ((dr["FEE_HEAD"].ToString().ToUpper().Contains("LATE FEE"))) ? true : false;

                        if ((dr["FEE_HEAD"].ToString().Trim().ToUpper().Contains(FeeHead)))
                        {
                            if (Convert.ToDouble(dr["TOTAL_DEMAND"]) <= 0)
                                dr.Delete();
                        }
                    }
                    ds.Tables[0].AcceptChanges();
                }

                /// Bind fee items list view with the data source found.
                lvFeeItems.DataSource = ds;
                lvFeeItems.DataBind();

                if (rdbPaymentOption.SelectedValue == "1")
                {
                    if (Convert.ToDecimal(ViewState["BalAmount"].ToString()) > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "CallMyFunction", "DevideTotalAmountFromcodebehind()", true);
                    }
                 
                }

                string RecieptCode = ds.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
                //if (RecieptCode == "TF" || RecieptCode == "EF" || RecieptCode == "HF" || RecieptCode == "BCA" || RecieptCode == "MBA" || RecieptCode == "PG" || RecieptCode == "EVF" ||
                //    RecieptCode == "PGF" || RecieptCode == "BMF" || RecieptCode == "BHE" || RecieptCode == "PDF" || RecieptCode == "MIS" || RecieptCode == "UNG" || RecieptCode == "OF" || RecieptCode == "IF")
                if ((RecieptCode != null || RecieptCode != ""))
                {

                    /// //Show remark for current fee demand   
                    txtRemark.Text = ds.Tables[0].Rows[0]["PARTICULAR"].ToString();
                    //txtFeeBalance.Text = ds.Tables[0].Rows[0]["EXCESS_AMT"].ToString();


                    decimal excs = Convert.ToDecimal(ds.Tables[0].Rows[0]["EXCESS_AMT"].ToString());
                    txtFeeBalance.Text = Math.Round(excs).ToString();

                    /// Set FeeCatNo from datasource
                    ViewState["FeeCatNo"] = ds.Tables[0].Rows[0]["FEE_CAT_NO"].ToString();

                    /// Show total fee amount to be paid by the student in total amount textbox.
                    /// This total fee amount can be changed by user according to the student's current 
                    /// payment amount (i.e. student can do part payment of Fee also).
                //    string DownPayAmount = objCommon.LookUp("ACD_DCR D ", "ISNULL(F1,0)", "D.IDNO=" + Convert.ToInt32(ViewState["StudentId"]) + " AND PAY_SERVICE_TYPE IN(5) AND RECON = 1 AND RECIEPT_CODE='DP' AND D.SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]));
                    lblamtpaid.Text = this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString();
                    double totalamt = Convert.ToDouble(this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString());
                    if (totalamt < 1)
                    {
                        DataSet dssem = feeController.GetPreviousReceiptsData(Convert.ToInt32(studId), Convert.ToInt32(ViewState["semesterno"]));

                        txtTotalAmountShow.Text = dssem.Tables[0].Rows[0]["BADMFEE"].ToString();
                        txtTotalAmount.Enabled = true;
                    }
                    else
                    {
                        DataSet dssem = feeController.GetPreviousReceiptsData(Convert.ToInt32(studId), Convert.ToInt32(ViewState["semesterno"]));
                       // txtTotalAmountShow.Text = this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString();
                        txtTotalAmountShow.Text = dssem.Tables[0].Rows[0]["BADMFEE"].ToString();
                        if (Convert.ToDecimal(txtTotalAmountShow.Text) < (1))
                        {
                            txtTotalAmount.Enabled = true;
                            txtTotalAmount.Text = "";
                            divInstallment.Visible = false;
                        }
                        //else
                        //{ 
                        //    txtTotalAmount.Enabled = false; 
                        //}
                    }
                 //   txtTotalFeeAmount.Text = Math.Round(number, 2)txtTotalAmountShow.Text;


                    txtTotalFeeAmount.Text = Convert.ToString(Convert.ToDecimal(txtTotalAmountShow.Text));
                    if (rdbPaymentOption.SelectedValue == "1")
                    {
                        txtTotalAmount.Text=Convert.ToString(Convert.ToDecimal(txtTotalAmountShow.Text));
                    }
                }
                btnSubmit.Enabled = true;
                divFeeItems.Visible = true;


            }
            else
            {
                /// As no demand record found, ask user if he want to create one.
                //this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'>";
                //this.divMsg.InnerHtml += " if(confirm('No demand found for semester " + ddlSemester.SelectedItem.Text + ".\\nDo you want to create demand for this semester?'))";
                //this.divMsg.InnerHtml += " if(confirm('No demand found for semester " + ddlSemester.SelectedItem.Text + ".'))";
                //this.divMsg.InnerHtml += "{__doPostBack('CreateDemand', '');}</script>";

                /// If fee items are not available then disable
                /// submit button and hide divFeeItems.
                /// 
                flag = 1;
                objCommon.DisplayUserMessage(updFee, "No demand found for semester " + ddlSemester.SelectedItem.Text, this.Page);
                divStudentSearch.Visible = true;
                divStudInfo.Visible = false;
                divCurrentReceiptInfo.Visible = false;
                divFeeItems.Visible = false;
                btnSubmit.Enabled = false;
                divPreviousReceipts.Visible = false;
                return;
            }
            DisplayExcessAmount(studId);//sunita
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.PopulateFeeItemsSection() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private double GetTotalFeeDemandAmount(DataTable dt)
    {
        double totalFeeAmt = 0.00;
        double TotalPrevDegreePaidAmount = 0.00;
        string DownPayAmount = objCommon.LookUp("ACD_DCR D ", "ISNULL(F1,0)", "D.IDNO=" + Convert.ToInt32(ViewState["StudentId"]) + " AND PAY_SERVICE_TYPE IN(5) AND RECON = 1 AND RECIEPT_CODE='DP' AND D.SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]));
        DataSet CurrentDeamnd = objCommon.FillDropDown("ACD_DEMAND D ", "DISTINCT D.IDNO,D.SEMESTERNO,DBO.FN_DESC('SEMESTER',D.SEMESTERNO)SEMESTERNAME", "ISNULL(D.DEGREENO,0)DEGREENO,ISNULL(D.BRANCHNO,0)BRANCHNO,ISNULL(COLLEGE_ID,0)COLLEGE_ID", " D.IDNO = " + Convert.ToInt32(ViewState["StudentId"]) + " AND D.SEMESTERNO = " + Convert.ToInt32(ViewState["semesterno"].ToString()) + " AND ISNULL(CAN,0)=0 AND ISNULL(DELET,0)=0 and RECIEPT_CODE='TF'", string.Empty);
        string PrevDegreePaidAmount = objCommon.LookUp("ACD_DCR D ", "ISNULL(SUM(TOTAL_AMT),0)", "D.IDNO=" + Convert.ToInt32(ViewState["StudentId"]) + " AND RECON = 1 AND RECIEPT_CODE='TF' AND D.SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + " AND (D.DEGREENO != " + CurrentDeamnd.Tables[0].Rows[0]["DEGREENO"].ToString() + " OR BRANCHNO != " + CurrentDeamnd.Tables[0].Rows[0]["BRANCHNO"].ToString() + " OR COLLEGE_ID != " + CurrentDeamnd.Tables[0].Rows[0]["COLLEGE_ID"].ToString() + ")");

        //string PaidAmount = objCommon.LookUp("ACD_DCR D", "SUM(F1) AS PAIDAMOUNT", "D.IDNO=" + Convert.ToInt32(ViewState["StudentId"]) + " D.SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]));
        //string TotalDemand = objCommon.LookUp("ACD_DEMAND D", "STOTAL_AMT", "D.IDNO=" + Convert.ToInt32(ViewState["StudentId"]) + " D.SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]));

        if (DownPayAmount == "" || DownPayAmount == string.Empty)
        {
            DownPayAmount = "0";
        }
        if (PrevDegreePaidAmount != "" && PrevDegreePaidAmount != "0")
        {
            TotalPrevDegreePaidAmount = Convert.ToDouble(PrevDegreePaidAmount);
        }
        
        try
        {
            foreach (DataRow dr in dt.Rows)
                totalFeeAmt += ((dr["AMOUNT"].ToString().Trim() != string.Empty) ? Convert.ToDouble(dr["AMOUNT"].ToString()) : 0.00);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.GetTotalFeeDemandAmount() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        totalFeeAmt= totalFeeAmt - Convert.ToDouble(DownPayAmount)-TotalPrevDegreePaidAmount;

        return totalFeeAmt;
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            /// Repopulate fee demand data into fee items
            /// for selected semester.
            if (ddlSemester.SelectedIndex > 0)
                this.PopulateFeeItemsSection(Int32.Parse(ddlSemester.SelectedValue), System.DateTime.Now);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.ddlSemester_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion

    #region Displaying Demand Draft Details

    protected void btnSaveDD_Info_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt;
            string ddno = "";
            string bnknm = "";
            bool flag = false;
            double Total_DD_Amount = 0.0;
            DateTime TranDate = !string.IsNullOrEmpty(txtDDDate.Text.Trim()) ? Convert.ToDateTime(txtDDDate.Text.Trim()) : System.DateTime.Now;
            if (Session["DD_Info"] != null && ((DataTable)Session["DD_Info"]) != null)
            {
                int count = Convert.ToInt32(objCommon.LookUp("ACD_DCR_TRAN", "count(DD_NO)", "DD_NO='" + txtDDNo.Text + "' AND DD_BANK='" + ddlBank.SelectedItem.Text + "'"));
                dt = ((DataTable)Session["DD_Info"]);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddno = dt.Rows[i]["DD_NO"].ToString();
                    bnknm = dt.Rows[i]["DD_BANK"].ToString();

                    if (ddno.Equals(txtDDNo.Text) && bnknm.Equals(ddlBank.SelectedItem.Text))
                    {
                        // flag = true;
                        // return;
                        objCommon.DisplayMessage(this.updFee, "Same DD number with Same bank is already exists", this.Page);
                        return;
                    }
                }

                if (flag.Equals(true))
                {
                    objCommon.DisplayUserMessage(updFee, "Same DD number with Same bank is already exists !!!!!", this.Page);
                    this.ClearControls_DemandDraftDetails();
                }
                else
                {
                    dt = ((DataTable)Session["DD_Info"]);
                    DataRow dr = dt.NewRow();
                    dr["DD_NO"] = txtDDNo.Text.Trim();
                    dr["DD_DT"] = txtDDDate.Text.Trim();
                    dr["DD_CITY"] = txtDDCity.Text.Trim();
                    dr["DD_BANK_NO"] = ddlBank.SelectedValue;
                    dr["DD_BANK"] = ddlBank.SelectedItem.Text;
                    //dr["DD_AMOUNT"] = Convert.ToDouble(HdnTotalAmount.Value);
                    dr["DD_AMOUNT"] = Convert.ToDouble(txtDDAmount.Text);
                    dt.Rows.Add(dr);
                    Session["DD_Info"] = dt;
                    this.BindListView_DemandDraftDetails(dt);

                    // add the two DD for the same semester add the previous DD amount and current DD amount// 25/05/2012
           
                    foreach (DataRow datarow in dt.Rows)
                    {
                        if (datarow["DD_AMOUNT"].ToString() != string.Empty)
                        {
                            Total_DD_Amount = Total_DD_Amount + Convert.ToDouble(datarow["DD_AMOUNT"].ToString());

                        }
                    }

                    txtTotalAmount.Text = Total_DD_Amount.ToString();

                    // 
                    this.ClearControls_DemandDraftDetails();
                    txtTotalAmount.Focus();
                }
            }
            else
            {
 
                dt = this.GetDemandDraftDataTable();
                DataRow dr = dt.NewRow();
                dr["DD_NO"] = txtDDNo.Text.Trim();
                dr["DD_DT"] = txtDDDate.Text.Trim();
                dr["DD_CITY"] = txtDDCity.Text.Trim();
                dr["DD_BANK_NO"] = ddlBank.SelectedValue;
                dr["DD_BANK"] = ddlBank.SelectedItem.Text;
                dr["DD_AMOUNT"] = Convert.ToDouble(txtDDAmount.Text);
                dt.Rows.Add(dr);
                Session.Add("DD_Info", dt);
                this.BindListView_DemandDraftDetails(dt);

                //Enter the DD amount then add the DD amount to total amount textbox //add code 25/05/2012
                txtTotalAmount.Text = Convert.ToString(txtDDAmount.Text);//txtDDAmount.Text.Trim();
                this.divMsg.InnerHtml = " <script type='text/javascript' language='javascript'> UpdateCash_DD_Amount();  </script> ";
                this.ClearControls_DemandDraftDetails();
                txtTotalAmount.Focus();
          
            }
        
            ModifyDemandOnTransDate(TranDate);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.btnSaveDD_Info_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void btnEditDDInfo_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            DataTable dt;
            if (Session["DD_Info"] != null && ((DataTable)Session["DD_Info"]) != null)
            {
                dt = ((DataTable)Session["DD_Info"]);
                DataRow dr = this.GetEditableDataRow(dt, btnEdit.CommandArgument);
                txtDDNo.Text = dr["DD_NO"].ToString();
                txtDDDate.Text = dr["DD_DT"].ToString();
                txtDDCity.Text = dr["DD_CITY"].ToString();
                ddlBank.SelectedValue = dr["DD_BANK_NO"].ToString();
                txtDDAmount.Text = dr["DD_AMOUNT"].ToString();
                dt.Rows.Remove(dr);
                Session["DD_Info"] = dt;
                this.BindListView_DemandDraftDetails(dt);
                // for Edit the data to maintain the Total amount // 25/04/2012
          //      txtTotalAmount.Text = (Convert.ToDouble(txtTotalAmount.Text.Trim()) - Convert.ToDouble(txtDDAmount.Text.Trim())).ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.btnEditDDInfo_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnDeleteDDInfo_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            DataTable dt;
            if (Session["DD_Info"] != null && ((DataTable)Session["DD_Info"]) != null)
            {
                dt = ((DataTable)Session["DD_Info"]);
                dt.Rows.Remove(this.GetEditableDataRow(dt, btnDelete.CommandArgument));
                Session["DD_Info"] = dt;
                this.BindListView_DemandDraftDetails(dt);

                // This code add for delete the DD amount to duduct the amount for total amount.// 26/04/2012
                if (dt.Rows.Count == 0)
                {
                    txtTotalAmount.Text = "";
                    txtTotalDDAmount.Text = "0";
                }
                else
                {
                    string ddAmt = dt.Rows[0]["DD_AMOUNT"].ToString();
                    txtTotalAmount.Text = ddAmt;

                }
                txtTotalAmount.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.btnDeleteDDInfo_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListView_DemandDraftDetails(DataTable dt)
    {
        try
        {
            divDDDetails.Style["display"] = "block";
            divFeeItems.Style["display"] = "block";
            lvDemandDraftDetails.DataSource = dt;
            lvDemandDraftDetails.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.BindListView_DemandDraftDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DataTable GetDemandDraftDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("DD_NO", typeof(string)));
        dt.Columns.Add(new DataColumn("DD_DT", typeof(DateTime)));
        dt.Columns.Add(new DataColumn("DD_CITY", typeof(string)));
        dt.Columns.Add(new DataColumn("DD_BANK_NO", typeof(int)));
        dt.Columns.Add(new DataColumn("DD_BANK", typeof(string)));
        dt.Columns.Add(new DataColumn("DD_AMOUNT", typeof(Double)));
        return dt;
    }

    private DataRow GetEditableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["DD_NO"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.GetEditableDataRow() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return dataRow;
    }

    private void ClearControls_DemandDraftDetails()
    {
        txtDDNo.Text = string.Empty;
        txtDDAmount.Text = string.Empty;
        txtDDCity.Text = string.Empty;
        txtDDDate.Text = DateTime.Today.ToShortDateString();
        ddlBank.SelectedIndex = 0;
    }
    #endregion

    #region Saving Transaction

    private void CreateDemandnew(FeeCollectionRegister feeDemand, int paymentTypeNoOld)
    {
        FeeCollectionRegister dcr = this.Bind_FeeCollectionData();
        feeController.CreateNewDemandmis(feeDemand, paymentTypeNoOld, ref dcr);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            btnSubmit.Enabled = false;
            /// if payment type is (D)demand draft
            /// then validate DD data and other submission data.
            /// 


            if (Convert.ToDecimal(txtTotalAmount.Text) < 1)
            {
               objCommon.DisplayUserMessage(this.updFee, "Please Enter Valid Amount", this.Page);
                return;
            }
            if (ReconCnt > 0)
            {
                objCommon.DisplayUserMessage(this.updFee, "Payment Reconciliation is pending for student! Please approve the same to accept more payment.", this.Page);
                return;
            }
            //if (txtTotalAmount.Text == string.Empty || txtTotalAmount.Text == "" || txtTotalAmount.Text=="0.0" || txtTotalAmount.Text=="0")
            //{
            //    objCommon.DisplayUserMessage(this.updFee, "Please Enter Valid Amount", this.Page);
            //    return;
            //}

            //else
            //{
            //}

            if (trExcesschk.Visible == true)
            {
                if (chkAllowExcessFee.Checked == true)
                {
                    string chk = "1";
                    FeeCollectionRegister dcr = new FeeCollectionRegister();
                    dcr.StudentId = (GetViewStateItem("StudentId") != string.Empty) ? Convert.ToInt32(GetViewStateItem("StudentId")) : 0;
                    dcr.SemesterNo = ((ddlSemester.SelectedIndex > 0 && ddlSemester.SelectedValue != string.Empty) ? Int32.Parse(ddlSemester.SelectedValue) : 1);
                    dcr.SemesterNo = dcr.SemesterNo;

                    double ExcessAmount = Math.Round(Convert.ToDouble(txtExcessAmount.Text)) - Convert.ToDouble(txtTotalAmount.Text);
                    string dcrno = objCommon.LookUp("ACD_DCR", "ISNULL(MAX(DCR_NO),0)", " ISNULL(EXCESS_AMOUNT,0) > 0 AND IDNO=" + dcr.StudentId + " AND ISNULL(RECON,0)=1 AND ISNULL(CAN,0)=0");
                    feeController.UpdateExcessStatus(dcrno, chk, ExcessAmount);
                }
                else
                {
                    string chk = "0";
                    FeeCollectionRegister dcr = new FeeCollectionRegister();
                    dcr.StudentId = (GetViewStateItem("StudentId") != string.Empty) ? Convert.ToInt32(GetViewStateItem("StudentId")) : 0;
                    dcr.SemesterNo = ((ddlSemester.SelectedIndex > 0 && ddlSemester.SelectedValue != string.Empty) ? Int32.Parse(ddlSemester.SelectedValue) : 1);
                    dcr.SemesterNo = dcr.SemesterNo;
                    double ExcessAmount =Math.Round(Convert.ToDouble(txtExcessAmount.Text));
                    string dcrno = objCommon.LookUp("ACD_DCR", "ISNULL(MAX(DCR_NO),0)", " ISNULL(EXCESS_AMOUNT,0) > 0 AND IDNO=" + dcr.StudentId + " AND ISNULL(RECON,0)=1 AND ISNULL(CAN,0)=0");
                    feeController.UpdateExcessStatus(dcrno, chk, ExcessAmount);
                }
            }
            if (ddlPaytype.SelectedValue.ToUpper() == "D")
            {
                string msg = this.ValidateDD_Details();

                if (msg != string.Empty)
                {
                    this.ShowMessage(msg);
                    objCommon.DisplayUserMessage(this.updFee, "You have entered pay type as demand draft but no demand details has been entered. \nPlease enter demand draft details.", this.Page);

                    return;
                }
                else
                {
                    this.SaveFeeCollection();
                }
            } // if pay type is C (Cash) then validate submission data.
            else if (ddlPaytype.SelectedValue.ToUpper() == "C")
            {
                string msg = string.Empty;
                this.ValidateSubmissionData(ref msg);

                if (msg != string.Empty)
                {
                    this.ShowMessage(msg);
                    return;
                }
                else
                {
                    this.SaveFeeCollection();
                }
            }// if pay type is T (Transfer Payment To Online Transfer.) then validate submission data.
            else if (ddlPaytype.SelectedValue.ToUpper() == "T")
            {
                string msg = string.Empty;
                this.ValidateSubmissionData(ref msg);

                if (msg != string.Empty)
                {
                    this.ShowMessage(msg);
                    return;
                }
                else
                {
                    this.SaveFeeCollection();
                }
            }
            else
            {
                //this.ShowMessage("Please select pay type.");
                string msg = string.Empty;
                this.ValidateSubmissionData(ref msg);

                if (msg != string.Empty)
                {
                    this.ShowMessage(msg);
                    return;
                }
                else
                {
                    this.SaveFeeCollection();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region Data Validation
    private string ValidateDD_Details()
    {
        string msg = string.Empty;

        if (Session["DD_Info"] == null || ((DataTable)Session["DD_Info"]).Rows.Count < 1)
            msg = "You have entered pay type as demand draft but no demand details has been entered. \\nPlease enter demand draft details.";

        this.ValidateSubmissionData(ref msg);
        return msg;
    }

    private string ValidateSubmissionData(ref string msg)
    {

        if (txtReceiptDate.Text.Trim() == string.Empty)
        {
            if (msg.Length > 0)
                msg += "\\n";
            msg += "Please enter receipt date.";
        }
        if (txtTotalAmount.Text.Trim() == string.Empty)
        {
            if (msg.Length > 0)
                msg += "\\n";
            msg += "Please enter total fee amount to be paid.";
        }
        if ((txtExcessAmount.Text.Trim() != string.Empty || txtExcessAmount.Text.Trim() != "0.00") && txtExcessAmount.Visible == true)
        {
            if (chkAllowExcessFee.Checked == false && chkAllowExcessFee.Visible == true)
            {
                if (msg.Length > 0)
                    msg += "\\n";
                msg += "Please Select Allow Deposits Checkbox Because Fee Collecion Option is Adjustment Receipt Type.";
            }
        }
        if (ddlPaytype.SelectedValue.Trim() == string.Empty)
        {
            if (msg.Length > 0)
                msg += "\\n";
            msg += "Please enter pay type.";
        }

        return msg;
    }
    #endregion

    private void SaveFeeCollection()
    {
        try
        {
            ///Bind all fee collection transaction related data
            FeeCollectionRegister dcr = this.Bind_FeeCollectionData();

            int chk;
            if (chkAllowExcessFee.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            int ORGID = Convert.ToInt32(Session["OrgId"]);
            if (feeController.SaveFeeCollection_Transaction(ref dcr, chk, ViewState["ipAddress"].ToString(), ORGID, Convert.ToDateTime(ViewState["TRANSDATE"])))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "functionConfirm", "confirmmsg();", true);
                this.btnReport.Enabled = true;
                ///Save DCR_NO to be used to show report later.
                Session["DD_Info"] = null;
                ViewState["DcrNo"] = dcr.DcrNo.ToString();
                Session["DCRNO"] = dcr.DcrNo.ToString();
                ViewState["primeRpt"] = dcr.DcrNo.ToString();
                btnSubmit.Enabled = false;
                int STUDIDNO = 0;
                STUDIDNO = Convert.ToInt32(Session["IDNO"]);
                string StudName = objCommon.LookUp("ACD_STUDENT", "STUDNAME", "IDNO='" + STUDIDNO + "'");
                string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO='" + STUDIDNO + "'");
                string EMAIL = objCommon.LookUp("ACD_STUDENT", "EMAILID", "IDNO='" + STUDIDNO + "'");

               // string InstallNo = objCommon.LookUp("ACD_DCR", "ISNULL(INSTALL_NO,0)", "DCR_NO='" + dcr.DcrNo + "'");
               //
               //
               // string InstallNo = objCommon.LookUp("ACD_DCR", "ISNULL(INSTALL_NO,0)", "DCR_NO='" + dcr.DcrNo + "'");
                 

                //return;
                //objCommon.DisplayMessage("Data Saved Successfully", this.Page);
                string enroll = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO=" + Convert.ToInt32(dcr.StudentId));
                int count = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(*)", "IDNO=" + Convert.ToInt32(dcr.StudentId)));
                if (enroll.Equals(""))
                {
                objCommon.DisplayUserMessage(updFee, "Transaction Saved Successfully.", this.Page);
                PreviousReciepts(Convert.ToInt32(dcr.StudentId),Convert.ToInt32(ViewState["semesterno"]));
               // rdbPaymentOption.Enabled = false;
                }
                else if (enroll != string.Empty && count == 1)
                {
                    objCommon.DisplayUserMessage(updFee, "Transaction Saved Successfully. Now You Can Print Fees Receipt To Click On Receipt Report Button.", this.Page);
                }
                else if (enroll != string.Empty && count > 1)
                {
                objCommon.DisplayUserMessage(updFee, "Transaction Saved Successfully", this.Page);
                PreviousReciepts(Convert.ToInt32(dcr.StudentId),Convert.ToInt32(ViewState["semesterno"]));
                //rdbPaymentOption.Enabled = false;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(updFee, "same DD number with same bank is already exists !!!!!", this.Page);
            }
        }
        // }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.SaveFeeCollection() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void PreviousReciepts(int studentId,int semsterno)
    {
        DataSet ds = null;
        ds = feeController.GetPaidReceiptsInfoByStudId(studentId, semsterno);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            // Bind list view with paid receipt data 
            lvPaidReceipts.DataSource = ds;
            lvPaidReceipts.DataBind();
            // btnReport.Enabled = true;
        }
        else//MAKE CHANGE HERE
        {
            divHidPreviousReceipts.InnerHtml = "No Previous Receipt Found.<br/>";
        }
        divPreviousReceipts.Visible = true;
    }

    private void ShowReport_ForCash(string rptName, int dcrNo, int studentNo)
    {
        try
        {
            //if (ViewState["primeRpt"] != null)
            //{
            //    int prmdcr = Convert.ToInt32(ViewState["primeRpt"]) + 1;
            //    //" + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]) + ",
            //    //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            //    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //    url += "Reports/CommonReport.aspx?";
            //    url += "pagetitle=Fee_Collection_Receipt";
            //    url += "&path=~,Reports,Academic," + rptName;
            //    //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_UA_NAME=" + Session["UAFULLNAME"].ToString() +
            //    //"," + this.GetReportParameters(dcrNo, studentNo);
            //    url += "&param=@P_DCR_NO=" + prmdcr + "," + "@P_IDNO=" + studentNo.ToString();
            //    // url += this.GetReportParameters(dcrNo, studentNo);
            //    divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            //    divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //    divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";

            //    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    //ScriptManager.RegisterClientScriptBlock(this.updEdit, this.updEdit.GetType(), "controlJSScript", sb.ToString(), true);
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //    sb.Append(@"window.open('" + url + "','','" + features + "');");
            //    ScriptManager.RegisterClientScriptBlock(this.updFee, this.updFee.GetType(), "controlJSScript", sb.ToString(), true);
            //    ViewState["primeRpt"] = null;
            //}
            //else
            //{

                //" + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]) + ",
                //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=Fee_Collection_Receipt";
                url += "&path=~,Reports,Academic," + rptName;
                //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_UA_NAME=" + Session["UAFULLNAME"].ToString() +
                //"," + this.GetReportParameters(dcrNo, studentNo);
                url += "&param=@P_DCR_NO=" + dcrNo.ToString() + "," + "@P_IDNO=" + studentNo.ToString();
                // url += this.GetReportParameters(dcrNo, studentNo);
                divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
                divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";

                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //ScriptManager.RegisterClientScriptBlock(this.updEdit, this.updEdit.GetType(), "controlJSScript", sb.ToString(), true);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");
                ScriptManager.RegisterClientScriptBlock(this.updFee, this.updFee.GetType(), "controlJSScript", sb.ToString(), true);
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
   
    private void ShowExamFeeRec(string rptName, int dcrNo, int idNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Exam_Fee_Receipt_Cum_Hall_Ticket";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idNo.ToString() + ",UserName=" + Session["userfullname"].ToString() + ",@P_SESSIONNO=" + Session["currentsession"].ToString() + ",@P_DCR_NO=" + dcrNo.ToString() + ",SessionName=" + Session["sessionname"].ToString();
            divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.ShowExamFeeRec() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //private DailyCollectionRegister Bind_FeeCollectionData()
     private FeeCollectionRegister Bind_FeeCollectionData()
    {
        /// Bind transaction related data from various controls.
        FeeCollectionRegister dcr = new FeeCollectionRegister();
        try
        {
            dcr.StudentId = (GetViewStateItem("StudentId") != string.Empty) ? Convert.ToInt32(GetViewStateItem("StudentId")) : 0;
            dcr.EnrollmentNo = lblRegistrationNo1.Text;
            dcr.StudentName = lblStudentName1.Text;
            dcr.BranchNo = (GetViewStateItem("BranchNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("BranchNo")) : 0;
            dcr.BranchName = lblCourse.Text;
            dcr.YearNo = (GetViewStateItem("YearNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("YearNo")) : 0;
            dcr.DegreeNo = (GetViewStateItem("DegreeNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("DegreeNo")) : 0;
            dcr.SemesterNo = Convert.ToInt32(ViewState["semesterno"].ToString()); 
            int examType = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "FLOCK=1"));

            if (ViewState["ReceiptType"].ToString() == "EF" || ViewState["ReceiptType"].ToString() == "TF")
            {
                dcr.SessionNo = Convert.ToInt32(ViewState["SESSION_NO"]);
            }
            else
            {
                dcr.SessionNo = Convert.ToInt32(Session["currentsession"].ToString());
            }

            dcr.Currency = ((ddlCurrency.SelectedIndex > 0 && ddlCurrency.SelectedValue != string.Empty) ? Int32.Parse(ddlCurrency.SelectedValue) : 1);
            dcr.FeeHeadAmounts = this.GetFeeItems();

            if (transdate.Text != string.Empty)
            {
                DateTime transactiondate = Convert.ToDateTime(transdate.Text);
                //((transdate.Text.Trim() != string.Empty) ? Convert.ToDateTime(transdate.Text) : DateTime.MinValue);
                ViewState["TRANSDATE"] = Convert.ToDateTime(transdate.Text);
            }
            else
            {
                ViewState["TRANSDATE"] = DateTime.Today.ToShortDateString();//DateTime.ParseExact(transdate.Text, "dd/MM/yyyy", null);
                //Convert.ToDateTime("10/01/2000:00:00");   
            }

            //dcr.TotalAmount = (txtTotalFeeAmount.Text.Trim() != string.Empty) ? Convert.ToDouble(txtTotalFeeAmount.Text) : 0.00;
            dcr.TotalAmount = (txtTotalAmount.Text.Trim() != string.Empty) ? Convert.ToDouble(txtTotalAmount.Text) : 0.00;

            DemandDrafts[] dds = null;
            dcr.DemandDraftAmount = this.GetTotalDDAmountAndSetCompleteDetails(ref dds);
            dcr.PaidDemandDrafts = dds;

            dcr.CashAmount = this.GetCashAmount(dcr.DemandDraftAmount);
            dcr.CounterNo = (GetViewStateItem("CounterNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("CounterNo")) : 0;
            dcr.ReceiptTypeCode = GetViewStateItem("ReceiptType");

            dcr.ReceiptNo = txtReceiptNo.Text.Trim();

            dcr.InstallmentNo = 0;

            //if (ddlInstallment.SelectedIndex > 0)
            //{
            //    dcr.InstallmentFlag = 1;
            //    dcr.InstallmentNo = Convert.ToInt32(ddlInstallment.SelectedValue);
            //}
            //else
            //{
            //    dcr.InstallmentFlag = 0;
            //    dcr.InstallmentNo = 0;
            //}



            dcr.ReceiptDate = ((txtReceiptDate.Text.Trim() != string.Empty) ? Convert.ToDateTime(txtReceiptDate.Text) : DateTime.MinValue);
            dcr.PaymentModeCode = GetViewStateItem("paymentmode");
            dcr.PaymentType = ddlPaytype.SelectedValue.Trim();
            dcr.FeeCatNo = (GetViewStateItem("FeeCatNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("FeeCatNo")) : 0;
            /// For Cash(C) or ATM(A) transaction cancel status should be false.
            /// In case of bank chalan (B) cancel status is by default true, because
            /// we have just printing the chalan we have not yet received the money.
            dcr.IsCancelled = (GetViewStateItem("paymentmode") == "C" || GetViewStateItem("paymentmode") == "A") ? false : true;

            /// For Cash(C) or ATM(A) transaction reconciliation status should be true.
            /// In case of bank chalan (B) reconciliation status is by default false, because
            /// we have just printing the chalan we have not yet received the money.
            dcr.IsReconciled = (GetViewStateItem("paymentmode") == "C" || GetViewStateItem("paymentmode") == "A") ? true : false;

            // Applicable only for bank chalan 
            if (GetViewStateItem("paymentmode") == "B")
                dcr.ChallanDate = DateTime.Today;

            /// This status is used to mark/flag unpaid/not received bank chalans.
            /// Default is false. if unpaid then will be marked as true.
            dcr.IsDeleted = false;
            dcr.CompanyCode = string.Empty;
            dcr.RpEntry = string.Empty;
            dcr.UserNo = Convert.ToInt32(Session["userno"].ToString());
            dcr.PrintDate = DateTime.Today;
            dcr.Remark = txtRemark.Text.Trim();
            dcr.ExamType = Convert.ToInt32(ddlExamType.SelectedValue);
            dcr.CollegeCode = Session["colcode"].ToString();

            //this is add to excess amount maintain. date: 10/04/2012
            // check the status of configuration page

            
            dcr.ExcessAmount = Convert.ToDouble(txtFeeBalance.Text);
            dcr.CreditDebitNo = txtCreditDebit.Text.Trim();
            dcr.TransReffNo = txtTransReff.Text.Trim();

            dcr.BankId = Convert.ToInt32(ddlAmtPaidBank.SelectedValue);

            if (chkPaytm.Checked == true)
                dcr.IsPaytm = 1;
            else
                dcr.IsPaytm = 0;


            //*****************
            foreach (ListViewDataItem item in lvFeeItems.Items)
            {
                string fee_head = string.Empty;//***************
                fee_head = ((HiddenField)item.FindControl("hdnfld_FEE_LONGNAME")).Value;//*****************
                string feeAmt = ((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim();

                if (fee_head.ToUpper() == "LATE FEE")
                {
                    if (feeAmt != null && feeAmt != string.Empty)
                    {
                        dcr.Late_fee = Convert.ToDouble(feeAmt);
                    }
                }
            }
            //*****************

            //}
            //else
            //{
            //    dcr.ExcessAmount = 0.00;
            //    objCommon.DisplayMessage("Excess amount cannot maintain. Beacause not maintain the Uaims Configuration status", this.Page);
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.Bind_FeeCollectionData() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return dcr;
    }

    private double GetCashAmount(double totalDDAmt)
    {
        double cashAmount = 0.00;
        try
        {
            /// if payment type is cash then total paid amount is equal to cash amount
            if (ddlPaytype.SelectedValue.Trim() == "C")
            { 
                cashAmount = (txtTotalCashAmt.Text.Trim() != string.Empty) ? Convert.ToDouble(txtTotalCashAmt.Text) : 0.00;

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "CallMyFunction", "DevideTotalAmountFromcodebehind()", true);
            }
            else
            {
                /// for demand draft payment type, cash amount is equal to total paid 
                /// amount minus total dd amount.
                /// This will handle the scenario if payment is done by dd as well as cash and also if
                /// payment type is D but total paid amount is
                /// greater than the total demand.
                if (txtTotalAmount.Text.Trim() != string.Empty)
                {
                    cashAmount = Convert.ToDouble(txtTotalCashAmt.Text);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.GetCashAmount() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return cashAmount;
    }

    private double GetTotalDDAmountAndSetCompleteDetails(ref DemandDrafts[] paidDemandDrafts)
    {
        /// This method not only return the total of dd amounts of all paid dds
        /// but also initializes the complete information
        /// of each demand draft into referenced DemandDrafts array.

        double totalDdAmt = 0.00;
        try
        {
            /// Collect demand draft details only if the pay type
            /// is D (i.e. Demand draft)
            if (ddlPaytype.SelectedValue.Trim() == "D" || ddlPaytype.SelectedValue.Trim() == "T" || ddlPaytype.SelectedValue.Trim() == "C")
            {
                if (ddlPaytype.SelectedValue == "C")
                {
                    Session["DD_Info"] = null;
                }
                if (Session["DD_Info"] != null && ((DataTable)Session["DD_Info"]) != null)
                {

                    DataTable dt = ((DataTable)Session["DD_Info"]);

                    paidDemandDrafts = new DemandDrafts[dt.Rows.Count];
                    int index = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        DemandDrafts dd = new DemandDrafts();
                        dd.DemandDraftNo = dr["DD_NO"].ToString();
                        dd.DemandDraftCity = dr["DD_CITY"].ToString();
                        dd.DemandDraftBank = dr["DD_BANK"].ToString();
                        dd.BankNo = (dr["DD_BANK_NO"].ToString() != string.Empty ? int.Parse(dr["DD_BANK_NO"].ToString()) : 0);

                        string ddDate = dr["DD_DT"].ToString();
                        if (ddDate != null && ddDate != string.Empty)
                            dd.DemandDraftDate = Convert.ToDateTime(ddDate);

                        string amount = dr["DD_AMOUNT"].ToString();
                        if (amount != null && amount != string.Empty)
                        {
                            dd.DemandDraftAmount = Convert.ToDouble(amount);
                            totalDdAmt += dd.DemandDraftAmount;
                        }
                        /// Set cheque/dd details in paid cheque/dd collection
                        paidDemandDrafts[index] = dd;
                        index++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.GetTotalDDAmountAndSetCompleteDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return totalDdAmt;
    }

    private FeeHeadAmounts GetFeeItems()
    {
        FeeHeadAmounts feeHeadAmts = new FeeHeadAmounts();
        try
        {
            foreach (ListViewDataItem item in lvFeeItems.Items)
            {
                int feeHeadNo = 0;
                double feeAmount = 0.00;

                string fee_head = string.Empty;//***************
                fee_head = ((HiddenField)item.FindControl("hdnfld_FEE_LONGNAME")).Value;//*****************

                if (fee_head != "LATE FEE")//*****************
                {
                    string feeHeadSrNo = ((Label)item.FindControl("lblFeeHeadSrNo")).Text;
                    if (feeHeadSrNo != null && feeHeadSrNo != string.Empty)
                        feeHeadNo = Convert.ToInt32(feeHeadSrNo);

                    string feeAmt = ((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim();
                    if (feeAmt != null && feeAmt != string.Empty)
                        feeAmount = Convert.ToDouble(feeAmt);

                    feeHeadAmts[feeHeadNo - 1] = feeAmount;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.GetFeeItems() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return feeHeadAmts;
    }

    #endregion

    #region Show Report

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (GetViewStateItem("DcrNo") != string.Empty && GetViewStateItem("StudentId") != string.Empty)
            {
                Session["DCRNO"] = Int32.Parse(GetViewStateItem("DcrNo"));
                string recipt_code = Convert.ToString(objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "DCR_NO = " + Int32.Parse(GetViewStateItem("DcrNo") + "")));
                Session["UAFULLNAME"] = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                int ReportFlag = 0;
                Session["CANCEL_REC"] = 0;
                if (Convert.ToString(ViewState["paymentmode"]) == "C")
                {               
                    this.ShowReport_ForCash("FeesReceiptApplicationForm.rpt", Int32.Parse(GetViewStateItem("DcrNo")), Int32.Parse(GetViewStateItem("StudentId")));
                    PreviousReciepts(Convert.ToInt32(GetViewStateItem("StudentId")),Convert.ToInt32(ViewState["semesterno"]));
                   btnBack_Click(sender, e);

                }
                
                else
                {
                    this.ShowReport("FeesReceiptApplicationForm.rpt", Int32.Parse(GetViewStateItem("DcrNo")), Int32.Parse(GetViewStateItem("StudentId")), "1", Session["UAFULLNAME"].ToString(), Convert.ToInt32(Session["CANCEL_REC"]));
                    PreviousReciepts(Convert.ToInt32(GetViewStateItem("StudentId")), Convert.ToInt32(ViewState["semesterno"]));
                    btnBack_Click(sender, e);
                }

               
            }

            //int studid = Int32.Parse(GetViewStateItem("StudentId"));
            //Installment_Data(studid);
            //LoadFeeCollectionOptions();
            //DisplayInformation(studid);
           

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnPrintReceipt_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnPrint = sender as ImageButton;
            //Label lblreconstatus = e.Item.FindControl("btnprintrefund") as ImageButton;
            HiddenField hdnReconStatus = btnPrint.FindControl("hdnReconStatus") as HiddenField;
            int srno = int.Parse(btnPrint.CommandArgument);
           
            Session["DCRNO"] = int.Parse(btnPrint.CommandArgument);
            Session["UAFULLNAME"] = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"]));

            int ReportFlag = 0;
          
            if (btnPrint.ToolTip == "True")
            {
                Session["CANCEL_REC"] = 1;
            }
            else if (btnPrint.ToolTip == "False")
            {
                Session["CANCEL_REC"] = 0;
            }
            else
            {
                Session["CANCEL_REC"] = 0;
            }

            if (btnPrint.CommandArgument != string.Empty && GetViewStateItem("StudentId") != string.Empty)
            {

            if (Convert.ToString(ViewState["paymentmode"]) == "C")
                {
                    this.ShowReport_ForCash("FeesReceiptApplicationForm.rpt", Int32.Parse(btnPrint.CommandArgument), Int32.Parse(GetViewStateItem("StudentId"))); 
                }
            else
                {
                }

            }
        }
        catch
        {
            throw;
        }
    }

    private void ShowReport(string rptName, int dcrNo, int studentNo, string copyNo, string UA_NAME, int Cancel)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_UA_NAME=" + Session["username"].ToString() + "," + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]) +
            "," + this.GetReportParameters(dcrNo, studentNo);
            divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updFee, this.updFee.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetReportParameters(int dcrNo, int studentNo)
    {
      
        string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt";
        return param;

    }
    #endregion

    #region Create Fee Demand

    private void CreateDemand(FeeCollectionRegister feeDemand, int paymentTypeNoOld)
    {
        try
        {
            if (feeController.CreateNewDemand(feeDemand, paymentTypeNoOld))
            {
                this.PopulateFeeItemsSection(feeDemand.SemesterNo, System.DateTime.Now);
            }
            else
            {
                objCommon.DisplayUserMessage(updFee, "Standard fee is not defined.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.btnCreateDemand_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CreateDemandForCurrentFeeCriteria()
    {
        try
        {
            FeeCollectionRegister feeDemand = new FeeCollectionRegister();
            feeDemand.StudentId = ((GetViewStateItem("StudentId") != string.Empty) ? Convert.ToInt32(GetViewStateItem("StudentId")) : 0);
            feeDemand.StudentName = lblStudentName1.Text;
            feeDemand.EnrollmentNo = lblRegistrationNo1.Text;
            int examType = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "FLOCK=1"));

            if (examType == 1)
            {
                feeDemand.SessionNo = Convert.ToInt32(Session["currentsession"]);
            }
            else
            {
                feeDemand.SessionNo = Convert.ToInt32(Session["currentsession"]) + 1;
            }

            // feeDemand.SessionNo = ((GetViewStateItem("SessionNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("SessionNo")) : 0);
            feeDemand.BranchNo = ((GetViewStateItem("BranchNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("BranchNo")) : 0);
            feeDemand.DegreeNo = ((GetViewStateItem("DegreeNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("DegreeNo")) : 0);
            feeDemand.SemesterNo = ((ddlSemester.SelectedIndex > 0 && ddlSemester.SelectedValue != string.Empty) ? Int32.Parse(ddlSemester.SelectedValue) : 1);
            feeDemand.AdmBatchNo = ((GetViewStateItem("AdmBatchNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("AdmBatchNo")) : 0);
            feeDemand.ReceiptTypeCode = GetViewStateItem("ReceiptType");
            feeDemand.PaymentTypeNo = ((GetViewStateItem("PaymentTypeNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("PaymentTypeNo")) : 0);
            feeDemand.CounterNo = ((GetViewStateItem("CounterNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("CounterNo")) : 0);
            feeDemand.UserNo = ((Session["userno"].ToString() != string.Empty) ? Convert.ToInt32(Session["userno"].ToString()) : 0);
            feeDemand.CollegeCode = ((Session["colcode"].ToString() != string.Empty) ? Session["colcode"].ToString() : string.Empty);
            int paymentTypeNoOld = ((GetViewStateItem("PaymentTypeNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("PaymentTypeNo")) : 0);

            //this.CreateDemand(feeDemand, paymentTypeNoOld);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.CreateDemandForCurrentFeeCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void CreateDemandForCurrentFeeCriteriamis()
    {
        try
        {
            //FeeDemand feeDemand = new FeeDemand();
            FeeCollectionRegister feeDemand = new FeeCollectionRegister();
            feeDemand.StudentId = ((GetViewStateItem("StudentId") != string.Empty) ? Convert.ToInt32(GetViewStateItem("StudentId")) : 0);
            feeDemand.StudentName = lblStudentName1.Text;
            feeDemand.EnrollmentNo = lblRegistrationNo1.Text;
            int examType = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "FLOCK=1"));

            if (examType == 1)
            {
                feeDemand.SessionNo = Convert.ToInt32(Session["currentsession"]);
            }
            else
            {
                feeDemand.SessionNo = Convert.ToInt32(Session["currentsession"]) + 1;
            }

            // feeDemand.SessionNo = ((GetViewStateItem("SessionNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("SessionNo")) : 0);
            feeDemand.BranchNo = ((GetViewStateItem("BranchNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("BranchNo")) : 0);
            feeDemand.DegreeNo = ((GetViewStateItem("DegreeNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("DegreeNo")) : 0);
            feeDemand.SemesterNo = ((ddlSemester.SelectedIndex > 0 && ddlSemester.SelectedValue != string.Empty) ? Int32.Parse(ddlSemester.SelectedValue) : 1);
            feeDemand.AdmBatchNo = ((GetViewStateItem("AdmBatchNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("AdmBatchNo")) : 0);
            feeDemand.ReceiptTypeCode = GetViewStateItem("ReceiptType");
            feeDemand.PaymentTypeNo = ((GetViewStateItem("PaymentTypeNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("PaymentTypeNo")) : 0);
            feeDemand.CounterNo = ((GetViewStateItem("CounterNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("CounterNo")) : 0);
            feeDemand.UserNo = ((Session["userno"].ToString() != string.Empty) ? Convert.ToInt32(Session["userno"].ToString()) : 0);
            feeDemand.CollegeCode = ((Session["colcode"].ToString() != string.Empty) ? Session["colcode"].ToString() : string.Empty);
            int paymentTypeNoOld = ((GetViewStateItem("PaymentTypeNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("PaymentTypeNo")) : 0);

            //this.CreateDemandnew(feeDemand, paymentTypeNoOld);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.CreateDemandForCurrentFeeCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CreateDemandForModifiedFeeCriteria()
    {
        try
        {
            //FeeDemand feeDemand = new FeeDemand();
            FeeCollectionRegister feeDemand = new FeeCollectionRegister();
            feeDemand.StudentId = ((GetViewStateItem("StudentId") != string.Empty) ? Convert.ToInt32(GetViewStateItem("StudentId")) : 0);
            feeDemand.StudentName = lblStudentName1.Text;
            feeDemand.EnrollmentNo = lblRegistrationNo1.Text;
            int examType = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "FLOCK=1"));

            if (examType == 1)
            {
                feeDemand.SessionNo = ((GetViewStateItem("SessionNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("SessionNo")) : 0);
            }
            else
            {
                feeDemand.SessionNo = ((GetViewStateItem("SessionNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("SessionNo")) + 1 : 0);
            }

            feeDemand.BranchNo = ((GetViewStateItem("BranchNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("BranchNo")) : 0);
            feeDemand.DegreeNo = ((GetViewStateItem("DegreeNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("DegreeNo")) : 0);
            feeDemand.SemesterNo = ((ddlSemester.SelectedIndex > 0 && ddlSemester.SelectedValue != string.Empty) ? Int32.Parse(ddlSemester.SelectedValue) : 0);
            feeDemand.AdmBatchNo = ((GetViewStateItem("AdmBatchNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("AdmBatchNo")) : 0);
            feeDemand.ReceiptTypeCode = GetViewStateItem("ReceiptType");
            feeDemand.PaymentTypeNo = ((ddlPaymentType.SelectedIndex > 0) ? Convert.ToInt32(ddlPaymentType.SelectedValue) : 0);
            feeDemand.CounterNo = ((GetViewStateItem("CounterNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("CounterNo")) : 0);
            feeDemand.UserNo = ((Session["userno"].ToString() != string.Empty) ? Convert.ToInt32(Session["userno"].ToString()) : 0);
            feeDemand.CollegeCode = ((Session["colcode"].ToString() != string.Empty) ? Session["colcode"].ToString() : string.Empty);
            int paymentTypeNoOld = ((GetViewStateItem("PaymentTypeNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("PaymentTypeNo")) : 0);

            //this.CreateDemand(feeDemand, paymentTypeNoOld);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.CreateDemandForCurrentFeeCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion

    #region Private Methods
    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    private string GetNewReceiptNo()
    {
        string receiptNo = string.Empty;
        string srno = "0";
        try
        {
            if (Convert.ToInt32(Session["OrgId"].ToString()) == 2)
            {
                int dcrno = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "ISNULL(MAX(DCR_NO),0) DCR_NO", ""));
                if (dcrno == 0)
                {
                    srno = "0";
                }
                else
                {
                    srno = (objCommon.LookUp("ACD_DCR", "right(REC_NO, charindex('/', reverse(REC_NO)) - 1) SRNO", "DCR_NO = (select ISNULL(MAX(DCR_NO),0) DCR_NO from ACD_DCR WHERE REC_NO IS NOT NULL)"));
                    srno = srno == string.Empty ? "0" : srno;
                }
                string currentdate = DateTime.Now.ToString("dd-MM-yyyy");
                receiptNo = "BSACIST/" + currentdate + "/" + GetViewStateItem("paymentmode") + '/' + Convert.ToString(srno + 1);
            }
            else
            {
                DataSet ds = feeController.GetNewReceiptData(GetViewStateItem("paymentmode"), Int32.Parse(Session["userno"].ToString()), ViewState["ReceiptType"].ToString());
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    String FeesSessionStartDate;
                    FeesSessionStartDate = objCommon.LookUp("REFF", "RIGHT(year(Start_Year),2)", "");
                    DataRow dr = ds.Tables[0].Rows[0];
                    dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString()) + 1;
                    receiptNo = dr["PRINTNAME"].ToString() + "/" + GetViewStateItem("paymentmode") + "/" + ViewState["ReceiptType"].ToString() + "/" + FeesSessionStartDate + "/" + dr["FIELD"].ToString();
                    // save counter no in hidden field to be used while saving the record
                    ViewState["CounterNo"] = dr["COUNTERNO"].ToString();
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return receiptNo;
    }

    private string GetViewStateItem(string itemName)
    {
        if (ViewState.Count > 0 &&
            ViewState[itemName] != null &&
            ViewState[itemName].ToString() != null &&
            ViewState[itemName].ToString().Trim() != string.Empty)
            return ViewState[itemName].ToString();
        else
            return string.Empty;
    }
    #endregion

    #region Refresh or Reload Page

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //// Reload/refresh complete page. 
        //if (Request.Url.ToString().IndexOf("&id=") > 0)
        //{
        //    Response.Redirect(Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("&id=")));
        //}
        //else
        //{
        Response.Redirect(Request.Url.ToString());
        //}
    }

    #endregion

    #region Update Fee Criteria
    protected void btnUpdateFeeCriteria_Click(object sender, EventArgs e)
    {
        try
        {
            int paymentTypeNo = (ddlPaymentType.SelectedIndex > 0 ? Int32.Parse(ddlPaymentType.SelectedValue) : 0);
            int scholarshipNo = (ddlScholarship.SelectedIndex > 0 ? Int32.Parse(ddlScholarship.SelectedValue) : 0);
            int studentId = (GetViewStateItem("StudentId") != string.Empty ? int.Parse(GetViewStateItem("StudentId")) : 0);
            if (feeController.UpdateFeesCriteria(paymentTypeNo, scholarshipNo, studentId))
            {
                this.CreateDemandForModifiedFeeCriteria();
                /// Set view state variables
                ViewState["PaymentTypeNo"] = paymentTypeNo;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.btnUpdateFeeCriteria_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

   

    private FeeHeadAmounts ItemsEnabled()
    {
        FeeHeadAmounts feeHeadAmts = new FeeHeadAmounts();
        try
        {
            trExcessAmt.Visible = false;
            trExcesschk.Visible = false;
            trNote.Visible = false; //Sunita
            foreach (ListViewDataItem item in lvFeeItems.Items)
            {
                ((TextBox)item.FindControl("txtFeeItemAmount")).Enabled = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.ItemsEnabled() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return feeHeadAmts;
    }

    private void DisplayExcessAmount(int id)
    {
        int studentId = id;

        string idtype = objCommon.LookUp(" ACD_STUDENT ", " IDTYPE ", " IDNO = " + studentId);

        string paytypmode = ViewState["paymentmode"].ToString();
        //int dcr_no = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "ISNULL(MAX(DCR_NO),0)", " ISNULL(EXCESS_AMOUNT,0) > 0 AND IDNO=" + studentId + " AND ISNULL(RECON,0)=1 AND ISNULL(CAN,0)=0"));

        int dcr_no = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "ISNULL(MAX(DCR_NO),0)", "IDNO=" + studentId + " AND ISNULL(RECON,0)=1 AND ISNULL(CAN,0)=0"));


        if (dcr_no == 0)
        {
            trExcessAmt.Visible = false;
            trExcesschk.Visible = false;
            trNote.Visible = false; //Sunita
        }
        else
        {
            if (paytypmode == "A")
            {
                // txtExcessAmount.Text = objCommon.LookUp("ACD_DCR", "ISNULL(SUM(ISNULL(EXCESS_AMOUNT,0)),0)", " ISNULL(EXCESS_AMOUNT,0) > 0 AND IDNO=" + studentId + " AND DCR_NO=" + dcr_no);

                string EXCAMT = objCommon.LookUp("ACD_DCR", "ISNULL(EXCESS_AMOUNT,0)EXCESS_AMT", " ISNULL(EXCESS_AMOUNT,0) > 0 AND IDNO=" + studentId + "AND DCR_NO=" + dcr_no + " AND CAN=0 AND RECON=1;");
                double excamt= Math.Round(Convert.ToDouble(EXCAMT));
                txtExcessAmount.Text = excamt.ToString();

               // txtExcessAmount.Text = objCommon.LookUp("ACD_DCR", "ISNULL(EXCESS_AMOUNT,0)EXCESS_AMT", " ISNULL(EXCESS_AMOUNT,0) > 0 AND IDNO=" + studentId + "AND DCR_NO=" + dcr_no + " AND CAN=0 AND RECON=1;");
                
                trExcessAmt.Visible = true;
                trExcesschk.Visible = true;
                trNote.Visible = true; //Sunita
                hdAdjustExcess.Value = "ADJUSTMENT";
            }
            else
            {
               // txtExcessAmount.Text = objCommon.LookUp("ACD_DCR", "ISNULL(SUM(ABS(EXCESS_AMOUNT)),0)EXCESS_AMT", " ISNULL(EXCESS_AMOUNT,0) > 0 AND IDNO=" + studentId + " AND CAN=0 AND RECON=1;");

                string PaidAmount = objCommon.LookUp("ACD_DCR", "SUM(isnull(TOTAL_AMT,0)) AS PAIDAMOUNT", "RECIEPT_CODE='TF' AND IDNO=" + Convert.ToInt32(ViewState["StudentId"]) + "AND CAN=0 AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]));
                string TotalDemand = objCommon.LookUp("ACD_DEMAND", "TOTAL_AMT", "RECIEPT_CODE='TF' AND CAN=0 AND  IDNO=" + Convert.ToInt32(ViewState["StudentId"]) + "AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]));

                string InstAmt = objCommon.LookUp("ACD_DCR", "SUM(isnull(F1,0)) AS PAIDAMOUNT", "RECIEPT_CODE='DP' AND IDNO=" + Convert.ToInt32(ViewState["StudentId"]) + "AND CAN=0 AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]));

                if (PaidAmount == string.Empty || PaidAmount == "")
                {
                    PaidAmount = "0";
                }
                if (TotalDemand == string.Empty || TotalDemand == "")
                {
                    TotalDemand = "0";
                }
                if (InstAmt == string.Empty || InstAmt == "")
                {
                    InstAmt = "0";
                }

                double TtlPaidAmt = Convert.ToDouble(PaidAmount) + Convert.ToDouble(InstAmt);

                if (TtlPaidAmt > Convert.ToDouble(TotalDemand))
                {
                    double excsamt =TtlPaidAmt - Convert.ToDouble(TotalDemand);

                    txtExcessAmount.Text = Math.Round(excsamt).ToString();
                    trExcessAmt.Visible = true;
                    trExcesschk.Visible = false;
                    trNote.Visible = false; //Sunita
                    hdAdjustExcess.Value = "";


                }
                else
                {
                    txtExcessAmount.Text = "0";
                    trExcessAmt.Visible = true;
                    trExcesschk.Visible = false;
                    trNote.Visible = false; //Sunita
                    hdAdjustExcess.Value = "";
                }
                //if (Convert.ToDouble(PaidAmount) > Convert.ToDouble(TotalDemand))
                //{
                //    double excsamt = Convert.ToDouble(PaidAmount) - Convert.ToDouble(TotalDemand);

                //    txtExcessAmount.Text =Math.Round(excsamt,2).ToString();
                //    trExcessAmt.Visible = true;
                //    trExcesschk.Visible = false;
                //    trNote.Visible = false; //Sunita
                //    hdAdjustExcess.Value = "";


                //}
                //else
                //{
                //    txtExcessAmount.Text = "0";
                //    trExcessAmt.Visible = true;
                //    trExcesschk.Visible = false;
                //    trNote.Visible = false; //Sunita
                //    hdAdjustExcess.Value = "";
                //}
                    

                //txtExcessAmount.Text = objCommon.LookUp("ACD_DCR", "ISNULL(SUM(ABS(EXCESS_AMOUNT)),0)EXCESS_AMT", " ISNULL(EXCESS_AMOUNT,0) > 0 AND IDNO=" + studentId + " AND CAN=0 AND RECON=1;");

                //trExcessAmt.Visible = true;
                //trExcesschk.Visible = false;
                //trNote.Visible = false; //Sunita
                //hdAdjustExcess.Value = "";
            }
        }
    }

  

    protected void chkAllowExcessFee_CheckedChanged(object sender, EventArgs e) //Sunita
    {
        //allow the excess fee to checked the checkbox
        if (txtExcessAmount.Text != string.Empty)//*************
        {
            if (txtTotalAmount.Text != string.Empty)//*************
            {
                //if (ddlSemester.SelectedValue == "1")
                //{
                if (chkAllowExcessFee.Checked == true)
                {
                    string chk = "1";
                 // DailyCollectionRegister dcr = new DailyCollectionRegister();
                    FeeCollectionRegister dcr = new FeeCollectionRegister();

                    dcr.StudentId = (GetViewStateItem("StudentId") != string.Empty) ? Convert.ToInt32(GetViewStateItem("StudentId")) : 0;
                    dcr.SemesterNo = ((ddlSemester.SelectedIndex > 0 && ddlSemester.SelectedValue != string.Empty) ? Int32.Parse(ddlSemester.SelectedValue) : 1);
                    //dcr.SemesterNo = dcr.SemesterNo - 1; commented by sunita
                    dcr.SemesterNo = dcr.SemesterNo;
                    //txtTotalAmount.Text = txtTotalAmountShow.Text;
                    double ExcessAmount = Math.Round(Convert.ToDouble(txtExcessAmount.Text)) - Convert.ToDouble(txtTotalAmount.Text);
                    //txtFeeBalance.Text = Convert.ToString(ExcessAmount); // Sunita
                    //string dcrno = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + dcr.StudentId + " AND SEMESTERNO" + dcr.SemesterNo);//commented by sunita

                    string dcrno = objCommon.LookUp("ACD_DCR", "ISNULL(MAX(DCR_NO),0)", " ISNULL(EXCESS_AMOUNT,0) >= 0 AND IDNO=" + dcr.StudentId + " AND ISNULL(RECON,0)=1 AND ISNULL(CAN,0)=0");

                    //txtTotalCashAmt.Text = (Convert.ToDouble(txtTotalCashAmt.Text) - Convert.ToDouble(txtExcessAmount.Text)).ToString(); //commented sunita
                    txtTotalCashAmt.Text = txtTotalAmount.Text;
                    if (Convert.ToDouble(txtTotalAmount.Text) > Math.Round(Convert.ToDouble((txtExcessAmount.Text))))
                    {
                        txtFeeBalance.Text = "0.00";
                    }
                    else
                    {
                        txtFeeBalance.Text = Convert.ToString(ExcessAmount); // Sunita
                    }
                    chkAllowExcessFee.Enabled = false;
                }
                else
                {
                    string chk = "0";
                    //DailyCollectionRegister dcr = new DailyCollectionRegister();

                    FeeCollectionRegister dcr = new FeeCollectionRegister();
                    dcr.StudentId = (GetViewStateItem("StudentId") != string.Empty) ? Convert.ToInt32(GetViewStateItem("StudentId")) : 0;
                    dcr.SemesterNo = ((ddlSemester.SelectedIndex > 0 && ddlSemester.SelectedValue != string.Empty) ? Int32.Parse(ddlSemester.SelectedValue) : 1);
                    //dcr.SemesterNo = dcr.SemesterNo - 1;
                    dcr.SemesterNo = dcr.SemesterNo;
                    double ExcessAmount = Math.Round(Convert.ToDouble(txtExcessAmount.Text));
                    txtFeeBalance.Text = Convert.ToString(ExcessAmount); // Sunita
                    //string dcrno = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + dcr.StudentId + " AND SEMESTERNO=" + dcr.SemesterNo);
                    //string dcrno = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + dcr.StudentId);//commented sunita
                    string dcrno = objCommon.LookUp("ACD_DCR", "ISNULL(MAX(DCR_NO),0)", " ISNULL(EXCESS_AMOUNT,0) >= 0 AND IDNO=" + dcr.StudentId + " AND ISNULL(RECON,0)=1 AND ISNULL(CAN,0)=0");

                    txtTotalCashAmt.Text = (Convert.ToDouble(txtTotalCashAmt.Text) + Math.Round(Convert.ToDouble(txtExcessAmount.Text))).ToString();

                    if (Convert.ToDouble(txtTotalAmount.Text) < Convert.ToDouble((txtTotalAmountShow.Text)))
                    {
                        txtFeeBalance.Text = "0.00";
                        txtTotalCashAmt.Text = txtTotalAmount.Text;
                    }
                    else
                    {
                        txtFeeBalance.Text = Convert.ToString(ExcessAmount); // Sunita
                    }
                }
                //}
            }
            else//***********
            {
                chkAllowExcessFee.Checked = false;
                objCommon.DisplayUserMessage(updFee, "Amount To Be Paid Should Not Be Empty.", this.Page);
            }
        }
        else//***********
        {
            chkAllowExcessFee.Checked = false;
            objCommon.DisplayUserMessage(updFee, "Excess Fee Is Not Present!", this.Page);
        }
    }

    protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        // txtReceiptNo.Text = this.GetNewReceiptNo();

        int semNo = Convert.ToInt32(ddlSemester.SelectedValue);
        if (semNo == 0)
        {
            semNo = 1;
        }
        this.PopulateFeeItemsSection(semNo, System.DateTime.Now);
        txtTotalCashAmt.Text = "0";
    }

    protected void btnNewFee_Click(object sender, EventArgs e)
    {
        // Response.Redirect("FeeCollectionOptions.aspx");
        Response.Redirect("~/Academic/FeeCollection.aspx?pageno=63", false);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Academic/FeeCollection.aspx?pageno=3622", false);
    }

    protected void txtReceiptDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime attdate = Convert.ToDateTime(txtReceiptDate.Text.Trim());
            if (attdate > DateTime.Now)
            {
                objCommon.DisplayMessage(this.updFee, "Future Date Not Accepted!", this.Page);
                txtReceiptDate.Text = "";
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }

    #region SearchPanel
    protected void ClearSelection()
    {
        ddlReceiptType.SelectedIndex = -1;
        txtSearchPanel.Text = "";
        ddlDropdown.SelectedIndex = -1;
        ddlSemester.SelectedIndex = -1;
        lvStudentPanel.DataSource = null;
        lvStudentPanel.DataBind();
        //pnltextbox.Visible = false;
        ////txtSearch.Visible = false;
        //pnlDropdown.Visible = false;


    }
    protected void ddlSearchPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlLV.Visible = false;
            lblNoRecords.Visible = false;
            lvStudentPanel.DataSource = null;
            lvStudentPanel.DataBind();
            if (ddlSearchPanel.SelectedIndex > 0)
            {
                DataSet ds = feeController.GetSearchDropdownDetails(ddlSearchPanel.SelectedItem.Text);
              //  DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearchPanel.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                    {
                        pnltextbox.Visible = false;
                        txtSearchPanel.Visible = false;
                        pnlDropdown.Visible = true;
                        lblSearchString.Text = "Search " + ddlSearchPanel.SelectedItem.Text + "";
                        divpanel.Attributes.Add("style", "display:block");
                        divDropDown.Attributes.Add("style", "display:block");
                        //divSearchPanel.Attributes.Add("style", "display:block");
                        divStudInfo.Attributes.Add("style", "display:none");
                        divtxt.Attributes.Add("style", "display:none");
                        //divtxt.Visible = false;
                        lblDropdown.Text = ddlSearchPanel.SelectedItem.Text;
                        
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);

                    }
                    else
                    {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtSearchPanel.Visible = true;
                        // pnlDropdown.Visible = false;
                        divDropDown.Attributes.Add("style", "display:none");
                        divStudInfo.Attributes.Add("style", "display:none");
                        divtxt.Attributes.Add("style", "display:block");
                        divpanel.Attributes.Add("style", "display:block");
                        lblSearchString.Text = "Search " + ddlSearchPanel.SelectedItem.Text + "";
                        
                    }
                }
            }
            else
            {

                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;
                divpanel.Attributes.Add("style", "display:none");

            }
            ClearSelection();
        }
        catch
        {
            throw;
        }
    }
    private void bindlist(string category, string searchtext)
    {
        FeeCollectionCounterController objSC = new FeeCollectionCounterController();
        DataSet ds = objSC.RetrieveStudentDetailsFeeCollection(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlLV.Visible = true;
            lvStudentPanel.Visible = true;
            lvStudentPanel.DataSource = ds;
            lvStudentPanel.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
          //  objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentPanel);//Set label - 
        }
        else
        {
            ddlSearchPanel.ClearSelection();
            txtSearchPanel.Text = "";
            lblNoRecords.Text = "Total Records : 0";
            objCommon.DisplayUserMessage(this.Page, "Record Not Found,Kindly Check for Demand is Created or Not.", this.Page);
            lvStudentPanel.Visible = false;
            lvStudentPanel.DataSource = null;
            lvStudentPanel.DataBind();
        }
    }
    protected void btnClosePanel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

    }
    protected void btnSearchPanel_Click(object sender, EventArgs e)
    {
        lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
        }
        else
        {
            value = txtSearchPanel.Text;
        }
        bindlist(ddlSearchPanel.SelectedItem.Text, value);
    }
    protected void lnkIdPanel_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            string url = string.Empty;

            Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;
            Label lblSem = lnk.Parent.FindControl("lblSemester") as Label;
            Label lblreceipttype = lnk.Parent.FindControl("lblReceipttype") as Label;
            Label lblSessionno = lnk.Parent.FindControl("lblsession") as Label;
            Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
            Session["stuinfofullname"] = lnk.Text.Trim();
            int idno = Convert.ToInt32(lnk.CommandArgument);
            Session["IDNO"] = Convert.ToInt32(lnk.CommandArgument);
            hdnID.Value = idno.ToString();
            txtSearchPanel.Text = lblenrollno.Text;
            ViewState["semesterno"] = lblSem.ToolTip;
 //           lblseletedsem.Text = lblSem.Text;

            Label lblBalance = lnk.Parent.FindControl("lblBalance") as Label;

            ViewState["BalAmount"] = lblBalance.Text.Trim();

            ViewState["ReceiptType"] = lblreceipttype.ToolTip;
            if (ViewState["ReceiptType"].ToString() == "EF" || ViewState["ReceiptType"].ToString() == "TF")
            {
                ViewState["SESSION_NO"] = Convert.ToInt32(lblSessionno.ToolTip);
            }
            this.LoadFeeCollectionOptions();

            //Load Payment Reconciliation

            BindPaymentReconciliation(Convert.ToInt32(Session["IDNO"]));
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollectionOptions.ddlReceiptType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindPaymentReconciliation(int Idno)
    {
        DataSet ds = feeController.GetPaymentReconciliation(Idno);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
           // divlkonlineadm.Attributes.Remove("class");
            Panel1.Visible = true;
            lvFeeReconciliation.DataSource = ds;
            lvFeeReconciliation.DataBind();
            ReconCnt = 1;
            
            //btnsubsem.Visible = true;
         
        }
        else
        {
           // objCommon.DisplayMessage(this.updPayRecon, "Record Not Found", this.Page);
            Panel1.Visible = false;
            lvFeeReconciliation.DataSource = null;
            lvFeeReconciliation.DataBind();
            ReconCnt=0;
           // btnsubsem.Visible = false;
            //Clearsem();

        }
    }
    protected void GetStudDetails()
    {
     //   StudentFeedBackController SFB = new StudentFeedBackController();
        FeeCollectionCounterController SFB = new FeeCollectionCounterController();

        //if (txtSearch.Text.Trim() != string.Empty)
        //{
        if (ddlSemester.SelectedIndex > 0)
        {
            int ISCounter = Convert.ToInt32(objCommon.LookUp("ACD_COUNTER_REF", "COUNT(*)", "RECEIPT_PERMISSION IN('" + ddlReceiptType.SelectedValue + "')  AND UA_NO=" + Session["userno"]));//AND (REC1<>0 OR REC2<>0 OR REC3<>0 OR REC4<>0 OR REC5<>0)
            if (ISCounter != 0)
            {
                //int studentId = feeController.GetStudentIdByEnrollmentNo(txtSearch.Text.Trim());
                int studentId = Convert.ToInt32(hdnID.Value);
                //hdnID.Value = studentId.ToString();
                int semester = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + studentId + ""));

                if (studentId > 0)
                {

                }
                else
                    objCommon.DisplayUserMessage(updFee, "No student found with given enrollment number.", this.Page);
                return;
            }
            else
            {
                objCommon.DisplayUserMessage(updFee, "Counter is Not Assign To Generate Receipt No. Please Assign Counter For User := " + Session["userfullname"], this.Page);
                return;
            }
        }
        else
            objCommon.DisplayUserMessage(updFee, "Please select semester.", this.Page);
        return;
       

    }
    private void LoadFeeCollectionOptions()
    {
        try
        {
            DataSet ds = feeController.GetFeeCollectionModes();
           
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // GetStudDetails();
                string URL = ds.Tables[0].Rows[0]["LINK_URL"].ToString();
                string[] mode = URL.Split('?')[1].Split('&')[0].Split('=');
                string Paymode = mode.Length > 0 ? mode[1] : "";
                ViewState["paymentmode"] = Paymode;
                ViewState["feecollectmode"] = ds.Tables[0];
                if (ds.Tables[0].Rows.Count >= 1)
                {

                    DataTable dt = (DataTable)ViewState["feecollectmode"];
                    DataRow dtRow = dt.NewRow();
                    string condition = ("PAY_MODE_NO=1");
                    DataRow[] dtrow1 = dt.Select(condition);
                    studentinfo(hdnID.Value, Convert.ToInt32(ViewState["semesterno"]), Paymode);
                    divSearchPanel.Visible = false;



                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "hideSearchpanel", " $('#ctl00_ContentPlaceHolder1_divSearchPanel').hide();", true);
                    studentinfo(hdnID.Value, Convert.ToInt32(ViewState["semesterno"]), Paymode);
                }

            }
            else
            {
                //  objCommon.DisplayUserMessage(updEdit,"No Data Found.", this.Page);
                string str = "@alert('No Data Found.');";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), str, "alert", true);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", str, true);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollectionOptions.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void studentinfo(string id, int semesterno, string Paymode)
    {
        if (id != null && id != string.Empty)
        {
          //  StudentFeedBackController SFB = new StudentFeedBackController();
            FeeCollectionCounterController SFB = new FeeCollectionCounterController();
            int studId = int.Parse(id);
            //this.DisplayInformation(studId);
            int semester = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + studId + ""));
            //ddlSemester.SelectedValue = ViewState["semesterno"].ToString();
            if (studId > 0)
            {
                if (semester != 0)
                {
                    int sessionno = 0;
                    int sessionmax = 0;
                    string feedback = string.Empty;
                    if (ViewState["ReceiptType"].ToString() == "EF" || ViewState["ReceiptType"].ToString() == "TF")
                    {
                        sessionno = Convert.ToInt16(ViewState["SESSION_NO"].ToString());
                        Session["currentsession"] = Convert.ToInt16(ViewState["SESSION_NO"].ToString());
                    }
                    else
                    {
                        sessionno = Convert.ToInt16(Session["currentsession"].ToString());
                    }

                    sessionmax = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "ISNULL(MAX(SESSIONNO),0)  ", "EXAMTYPE=1 AND SESSIONNO < " + sessionno));

                    feedback = objCommon.LookUp("REFF", "Feedback_Status", "");

                    if (feedback == "True")
                    {
                        int Feedback = Convert.ToInt16(SFB.FeedbackCount(studId, sessionmax));

                        if (Feedback == 1)
                        {
                            this.DisplayInformation(studId);
                        }
                        else
                        {
                            objCommon.DisplayUserMessage(updFee, "Student has not provided Feedback.", this.Page);
                        }
                    }
                    else
                    {
                        this.DisplayInformation(studId);
                    }
                }
                else
                {
                    this.DisplayInformation(studId);
                }
            }
            else
                objCommon.DisplayUserMessage(updFee, "No student found with given enrollment number.", this.Page);
        }
    }
    #endregion
   
    protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["feecollectmode"];
        DataRow dtRow = dt.NewRow();
        if (dt != null && dt.Rows.Count > 0)
        {
            //string condition = ("PAY_MODE_NO=" + ddlMode.SelectedValue);
            string condition = ("PAY_MODE_NO=1");
            DataRow[] dtrow1 = dt.Select(condition);
            string URL = dtrow1[0]["LINK_URL"].ToString();
            string[] mode = URL.Split('?')[1].Split('&')[0].Split('=');
            string Paymode = mode.Length > 0 ? mode[1] : "";
            ViewState["paymentmode"] = Paymode;
            studentinfo(hdnID.Value, Convert.ToInt32(ViewState["semesterno"]), Paymode);
            divSearchPanel.Visible = false;
            // ScriptManager.RegisterStartupScript(this, GetType(), "hideSearchpanel", " $('#divSearchPanel').hide();", true);
        }
    }
    private void ShowReport_MIT(string reportTitle, string rptFileName, string UANAME)
    {
        try
        {
            int SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Session["IDNO"] + ",@P_DCRNO=" + Session["DCRNO"] + ",@P_UA_NAME=" + Session["UAFULLNAME"].ToString();
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void CreateUser(string student_Name, string RRNO, string Email)
    {
        try
        {
            string UA_PWD = string.Empty;
            string UA_ACC = string.Empty;
            string password = string.Empty;
            int IDNO = 0;
            if (Convert.ToInt32(Session["OrgId"].ToString()) == 3)
            {

                string PasswordName = GenearteFourLengthPassword();
                Session["UAR_PASS"] = PasswordName;
                UA_PWD = PasswordName;
                UA_PWD = clsTripleLvlEncyrpt.ThreeLevelEncrypt(UA_PWD);
                UA_ACC = "0,500,76";
                IDNO = Convert.ToInt32(Session["IDNO"]);

                //User_AccController objACC = new User_AccController();
                FeeCollectionCounterController objACC = new FeeCollectionCounterController();
                UserAcc objUA = new UserAcc();
               // FeeCollectionRegister objUA = new FeeCollectionRegister();

                //string STUDIDNO=Convert.ToInt32(Session["IDNO"]);
                objUA.UA_IDNo = Convert.ToInt32(Session["IDNO"]);
                //objUA.UA_IDNo = STUDIDNO;

                // id = objUA.UA_IDNo;

                string UA_NAME = objCommon.LookUp("USER_ACC", "UA_NAME", "UA_IDNO='" + Session["IDNO"].ToString() + "'");
                objUA.UA_Name = UA_NAME;
                string pwd = string.Empty;
                string getpwd = objCommon.LookUp("User_Acc", "UA_PWD", "UA_NAME='" + objUA.UA_Name + "'");
                //string strPwd = clsTripleLvlEncyrpt.ThreeLevelDecrypt(getpwd);
                objUA.UA_Pwd = Session["UAR_PASS"].ToString();
                int ret = 0;

              //  objUA.OrganizationId = Convert.ToInt32(Session["OrgId"].ToString());
                ret = objACC.CopyStudentUserPwdRandom(objUA);
            }
            else
            {

                UA_PWD = clsTripleLvlEncyrpt.ThreeLevelEncrypt(RRNO);
                UA_ACC = "0,500,76";
                IDNO = Convert.ToInt32(Session["IDNO"]);
            }

            CustomStatus CS = (CustomStatus)feeController.CreateUser(RRNO, UA_PWD, student_Name, Email, UA_ACC, IDNO);
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    public static string GenearteFourLengthPassword()
    {
        string allowedChars = "";
        // allowedChars += "Slit@123";
        //allowedChars += "s,l,i,t";
        allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
        char[] sep = { ',' };
        string[] arr = allowedChars.Split(sep);
        string passwordString = "";
        string temp = "";
        Random rand = new Random();

        for (int i = 0; i < 4; i++)
        {
            temp = arr[rand.Next(0, arr.Length)];
            passwordString += temp;
        }
        return passwordString;
    }


    private void ShowReport(int refundNo)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Refund_Voucher";
            url += "&path=~,Reports,Academic,RefundVoucher.rpt";
            url += "&param=@P_REFUND_NO=" + refundNo.ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ModifyDemandOnTransDate(DateTime TransDate)
    {

        foreach (ListViewDataItem item in lvFeeItems.Items)
        {
            HiddenField hf_FeedHead = item.FindControl("hfFee_hd") as HiddenField;
            if (!string.IsNullOrEmpty(hf_FeedHead.Value) && hf_FeedHead.Value == "Late Fee")
            {
                Label lblTotaldemandamt = item.FindControl("lblTotaldemandamt") as Label;
                if (Convert.ToDouble(lblTotaldemandamt.Text) > 0.00)
                {
                    PopulateFeeItemsSection(Convert.ToInt32(ViewState["semesterno"]), TransDate);
                }
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "function1", "DevideTotalAmount();", true);
    }

    public DataSet GetFeeItems_Data(int sessionNo, int studentId, int semesterNo, string receiptType, int examtype, int currency, int payTypeNo, ref int status, string TransDate,int Payment_Option)
    {
        DataSet ds = null;
        try
        {
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objDataAccess = new SQLHelper(_connectionString);
            SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_RECEIPT_CODE", receiptType),
                    new SqlParameter("@P_EXAMTYPE", examtype),
                    new SqlParameter("@P_CURRENCY",currency),
                    new SqlParameter("@P_PAYTYPENO",payTypeNo),
                    new SqlParameter("@P_TRANS_DATE",TransDate), // ADDED BY SHAILENDRA K. ON DATED 29.04.2023 AS PER DR. MANOJ SIR SUGGESTION CALCULATING LATE FINE ON TRANS DATE.
                    new SqlParameter("@P_PAYMENT_OPTION",Payment_Option), 
                    new SqlParameter("@P_OUT", status)
                };
            sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
            ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_FEE_ITEMS_AMOUNT", sqlParams);
        }
        catch (Exception ex)
        {
            throw new IITMSException("Academic_FeeCollection.GetFeeItems_Data() --> " + ex.Message + " " + ex.StackTrace);
        }
        return ds;
    }
    protected void lblCalculateLateFine_Click(object sender, EventArgs e)
    {
        DateTime TransDate = Convert.ToDateTime(transdate.Text);
        ModifyDemandOnTransDate(TransDate);
    }


    protected void rdbPaymentOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbPaymentOption.SelectedValue == "1")
        {
            txtTotalAmount.Enabled = false;
            //txtTotalAmount.Text = txtTotalAmountShow.Text;
            //txtTotalCashAmt.Text = txtTotalAmountShow.Text;
            //txtTotalFeeAmount.Text = txtTotalAmountShow.Text;

            //int semNo = int.Parse(ViewState["semesterno"].ToString());// Convert.ToInt32(ddlSemester.SelectedValue);

            //if (semNo == 0)
            //{
            //    ddlSemester.SelectedValue = ViewState["semesterno"].ToString();
            //    semNo = Convert.ToInt32(ddlSemester.SelectedValue);
            //}
            //this.PopulateFeeItemsSection(semNo, System.DateTime.Now);

            this.LoadFeeCollectionOptions();
           // ddlInstallment.Visible = false;
           // ddlInstallment.SelectedValue = "0";
        }
        else if (rdbPaymentOption.SelectedValue == "2")
        {
            txtTotalAmount.Enabled = true;
            //txtTotalAmount.Text = ViewState["INSTALLMENTAMOUNT"].ToString();
            //txtTotalCashAmt.Text = ViewState["INSTALLMENTAMOUNT"].ToString();
            //txtTotalFeeAmount.Text = ViewState["INSTALLMENTAMOUNT"].ToString();
            this.LoadFeeCollectionOptions();
           // ddlInstallment.Visible = true;
        }

    }


    protected void btnReload_Click(object sender, EventArgs e)
    {

        int studid = Int32.Parse(GetViewStateItem("StudentId"));
        //Installment_Data(studid);
        Installment_Data(studid);
        LoadFeeCollectionOptions();
        DisplayInformation(studid);

    }
    
    protected void btnRecon_Click(object sender, EventArgs e)
    {
        try
        {
           
           Button lnkRecon = (Button)(sender);
           string IDNO = lnkRecon.CommandArgument.ToString();
           //Label myLabel = (Label)lvFeeReconciliation.Items.FindControl("myLabel");
           Label lblDcrTempNo = (Label)lvFeeReconciliation.Items[0].FindControl("lblDcrTempNo");
           TextBox txtsemamount = (TextBox)lvFeeReconciliation.Items[0].FindControl("txtsemamount");
           TextBox txtsemdate= (TextBox)lvFeeReconciliation.Items[0].FindControl("txtsemdate");
           DropDownList ddlstatussem = (DropDownList)lvFeeReconciliation.Items[0].FindControl("ddlstatussem");
           TextBox txtsemremark= (TextBox)lvFeeReconciliation.Items[0].FindControl("txtsemremark");
           Label lblbank = (Label)lvFeeReconciliation.Items[0].FindControl("lblbank");
            
           int UANO=Convert.ToInt32(Session["userno"].ToString());
           string DCRTEMPNO = lblDcrTempNo.Text;
           string TotalAMT = txtsemamount.Text;
           string ReconDate=txtsemdate.Text;
           string prostatus=ddlstatussem.SelectedValue;
           string remark=txtsemremark.Text;
           string Bank = lblbank.Text;
           //if (feeController.RecounsilationTran(Convert.ToInt32(IDNO), TotalAMT, txtsemdate.Text, ddlstatussem.SelectedItem.Text, txtsemremark.Text, Convert.ToInt32(DCRTEMPNO), "", UANO))
           //{

           if (prostatus == "0")
           {
               objCommon.DisplayMessage(this.Page, "Please Select Status", this.Page);
               return;
           }

           if (feeController.RecounsilationTran(Convert.ToInt32(IDNO), TotalAMT, ReconDate, prostatus, remark, Convert.ToInt32(DCRTEMPNO), Bank, UANO))       
           {
               objCommon.DisplayMessage(this.Page, "Transaction Saved Successfully !!!", this.Page);
               BindPaymentReconciliation(Convert.ToInt32(IDNO));
               btnReload_Click(sender,e);
           }


            
        }
        catch (Exception ex)
        { 
        
        }
    }


    protected void lnksem_Click(object sender, EventArgs e)
    {        
        try
        {
            //imageViewerContainer.Visible = false;
            //irm1.Visible = true;
            //divlkonlineadm.Attributes.Remove("class");
            //foreach (ListViewDataItem dataitem in lvsemester.Items)
            //{
            LinkButton lnksem = (LinkButton)(sender);
            //CheckBox chk = dataitem.FindControl("chreg") as CheckBox;
            //HiddenField hdf = dataitem.FindControl("hdnDocno") as HiddenField;
            //int userno = Convert.ToInt32(chk.ToolTip);
            //if (Convert.ToInt32(lnksem.ToolTip) == userno)
            //{
            string Url = string.Empty;
            string directoryPath = string.Empty;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/DownloadImg" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = lnksem.CommandArgument.ToString();
            var ImageName = img;
            if (img.ToString() != string.Empty || img.ToString() != "")
            {
                string extension = Path.GetExtension(img.ToString());
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
                if (extension == ".pdf")
                {
                    //Change by Ashish
                    var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                    string filePath = directoryPath + "\\" + ImageName;
                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    ViewState["filePath_Show"] = filePath;
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);

                    byte[] bytes = System.IO.File.ReadAllBytes(filePath);

                    string Base64String = Convert.ToBase64String(bytes);
                    Session["Base64String"] = Base64String;
                    Session["Base64StringType"] = "application/pdf";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
                  
                }
                else
                {
                    var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                    string filePath = directoryPath + "\\" + ImageName;
                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    ViewState["filePath_Show"] = filePath;
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);

                    byte[] bytes = System.IO.File.ReadAllBytes(filePath);

                    string Base64String = Convert.ToBase64String(bytes);
                    Session["Base64String"] = Base64String;
                    Session["Base64StringType"] = "image/png";
                    //hdfImagePath.Value = "data:image/png;base64," + Base64String;
                    //imageViewerContainer.Visible = true;
                    //irm1.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
                   

                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Sorry, File not found !!!", this.Page);
            }
          
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Sorry, File not found !!!", this.Page);
        }
    
    }

    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }
    protected void lnkClose_Click(object sender, EventArgs e)
    {
        try
        {
            if ((System.IO.File.Exists(Convert.ToString(ViewState["filePath_Show"]))))
            {
                System.IO.File.Delete(Convert.ToString(ViewState["filePath_Show"]));
            }
        }
        catch (Exception ex)
        {

        }
    }


}