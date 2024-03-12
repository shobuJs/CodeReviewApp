using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
//using SFA;
//using paytm;
using System.IO;
using System.Xml;
using System.Text;
using System.Net;
using System.Configuration;
using System.Web;
using System.Drawing;

using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Diagnostics;

using System.Data.SqlClient;
using System.Data;
using DotNetIntegrationKit;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using CCA.Util;

public partial class ACADEMIC_Feespayment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    ExamController objExam = new ExamController();
    FeeCollectionController ObjNuc = new FeeCollectionController();
    StudentFees objStudentFees = new StudentFees();
    ActivityController objActController = new ActivityController();
    string Semesterno = string.Empty;
    string Degreeno = string.Empty;
    string branchno = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (((Session["IDNO"]) == null))
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!Page.IsPostBack)
            {
                int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DISTINCT COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"])));
                ViewState["COLLEGE_ID"] = clg_id;
                GetStudentDeatlsForActivity();
                if (CheckActivity() == false)
                {
                    fees.Visible = false;
                    return;
                }
                else
                {
                    try
                    {
                        //Set the Page Title
                        Page.Title = Session["coll_name"].ToString();
                        getDetails();


                        lblSession.ToolTip = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO)", "DISTINCT SESSION_NO", "STARTED = 1 AND SHOW_STATUS =1 and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%'AND SA.CLG_ID=" + ViewState["COLLEGE_ID"].ToString() + "");

                        this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE ART INNER JOIN ACD_DEMAND D ON ART.RECIEPT_CODE=D.RECIEPT_CODE", "DISTINCT ART.RECIEPT_CODE", "ART.RECIEPT_TITLE", "ART.RCPTTYPENO IN (1,2,3) AND ISNULL(D.CAN,0)=0 AND SESSIONNO=" + lblSession.ToolTip + " AND IDNO=" + Convert.ToInt32(Session["IDNO"]), "RECIEPT_TITLE");

                        lblSession.Text = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSION_PNAME", "SESSIONNO=" + lblSession.ToolTip);

                        try
                        {
                            int session = 0;
                            session = Convert.ToInt32(objCommon.LookUp("acd_demand", "DISTINCT sessionno", "IDNO=" + Convert.ToInt32(Session["IDNO"]) + " AND sessionno =" + Convert.ToInt32(lblSession.ToolTip)));
                            if (session == Convert.ToInt32(lblSession.ToolTip))
                            {
                                double DemandAmount = Convert.ToDouble(objCommon.LookUp("ACD_DEMAND", "sum(TOTAL_AMT)", "IDNO=" + Convert.ToInt32(Session["IDNO"]) + " AND sessionno =" + Convert.ToInt32(lblSession.ToolTip)));
                                lbltotalfees.Text = DemandAmount.ToString();
                                BindInstallmentDetailsInListview();
                            }
                            else
                            {
                                objCommon.DisplayMessage("Demand not created for current session. So please contact to Administrator for demand creation...", this.Page);
                                ddlReceiptType.Enabled = false;
                            }
                        }
                        catch
                        {
                            objCommon.DisplayMessage("Demand not created for current session. So please contact to Administrator for demand creation...", this.Page);
                            ddlReceiptType.Enabled = false;
                        }


                        //double DemandAmount = Convert.ToDouble(objCommon.LookUp("ACD_DEMAND", "sum(TOTAL_AMT)", "IDNO=" + Convert.ToInt32(Session["IDNO"]) + " AND sessionno =" + Convert.ToInt32(lblSession.ToolTip)));
                        //lbltotalfees.Text = DemandAmount.ToString();
                        //BindInstallmentDetailsInListview();
                    }
                    catch { }
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void GetStudentDeatlsForActivity()
    {
        try
        {
            DataSet ds;
            ds = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO,BRANCHNO,SEMESTERNO", "STUDNAME", "IDNO=" + Convert.ToInt32(Session["idno"]), "IDNO");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Degreeno = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                branchno = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                Semesterno = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                CheckActivity();
            }
            else
            {
                objCommon.DisplayMessage("This Activity has not been Started for" + Semesterno + "rd sem.Please Contact Admin.!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_StudentCondonationFees.GetStudentDeatlsForEligibilty --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void getDetails()
    {
        DataSet ds = objExam.GetStudentDetail(Convert.ToInt32(Session["IDNO"]));

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {


                lblname.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                lblapp.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                //lblapp.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();   
                lblbranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                lblmobile.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                lblEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();

                ViewState["degreeno"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                ViewState["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                //// For PhD Students sem
                //if (Convert.ToInt16(ds.Tables[0].Rows[0]["DEGREENO"].ToString()) == 3)   //  DEGREE = PhD
                //{
                //    int evenodd = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "ODD_EVEN", "FLOCK=0 AND EXAMTYPE=1"));
                //    if (evenodd == 1)
                //    {
                //        ddlsem.Items.Clear();
                //        ddlsem.Items.Add("AUTUMN SEMESTER");
                //        ddlsem.SelectedItem.Value = "5";//3
                //        lblSem.Text = "AUTUMN SEMESTER";
                //        lblSem.ToolTip = "5";
                //    }
                //    else
                //    {
                //        ddlsem.Items.Clear();
                //        ddlsem.Items.Add("SPRING SEMESTER");
                //        ddlsem.SelectedItem.Value = "6";//4
                //        lblSem.Text = "SPRING SEMESTER";
                //        lblSem.ToolTip = "6";
                //    }

                //}
                //else
                //{


                //ViewState["YearWise"] = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "ISNULL(YEARWISE,0)YEARWISE", "DEGREENO=" + ViewState["degreeno"].ToString()));
                //if (ViewState["YearWise"].ToString() == "1")
                //{
                //   // objCommon.FillDropDownList(ddlsem, "ACD_YEAR", "SEM", "YEARNAME", "SEM<>0", "SEM");
                //    lblSem.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                //    lblYear.Text = ds.Tables[0].Rows[0]["YEARNAME"].ToString();
                //}
                //else
                //{
                //}

                //ViewState["YearWise"] = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "ISNULL(YEARWISE,0)YEARWISE", "DEGREENO=" + ViewState["degreeno"].ToString()));
                //if (ViewState["YearWise"].ToString() == "1")
                //{
                //    objCommon.FillDropDownList(ddlSemester, "ACD_YEAR", "SEM", "YEARNAME", "SEM<>0", "SEM");
                //}
                //else
                //{
                //    objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO<>0", "SEMESTERNO");
                //}

                lblSem.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                lblYear.Text = ds.Tables[0].Rows[0]["YEARNAME"].ToString();

                //lblSem.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                lblSem.ToolTip = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                //objCommon.FillDropDownList(ddlsem, "ACD_SEMESTER SM INNER JOIN ACD_STUDENT S ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "SM.SEMESTERNAME", "IDNO=" + Convert.ToInt32(Session["IDNO"]), "S.SEMESTERNO");
                //}
            }
        }
    }

    private void BindInstallmentDetailsInListview()
    {
        try
        {
            string receiptType = ddlReceiptType.SelectedItem.Text;
            int idno = Convert.ToInt32(Session["IDNO"]);
            int session = Convert.ToInt32(lblSession.ToolTip);
            DataSet ds = ObjNuc.GetFeesPaymentData(idno, receiptType, session);
            if (ds != null && ds.Tables.Count > 0)
            {
                lvFeesPayment.DataSource = ds;
                lvFeesPayment.DataBind();
                //coment by pankaj 19032020
                foreach (ListViewDataItem dataitem in lvFeesPayment.Items)
                {

                    Button btnOnlinePay = dataitem.FindControl("btnOnlinePay") as Button;

                    Button btnChallan = dataitem.FindControl("btnChallan") as Button;

                    //HiddenField hfrecon = dataitem.FindControl("hfrecon") as HiddenField;
                    Label lblReconVal = dataitem.FindControl("lblReconVal") as Label;

                    //HiddenField hdftransactionmode = dataitem.FindControl("hdftransactionmode") as HiddenField;

                    if (Convert.ToInt32(lblReconVal.ToolTip) == 1)
                    {
                        btnOnlinePay.Enabled = false;
                        btnChallan.Enabled = false;

                        btnOnlinePay.BackColor = Color.DarkOrange;
                        btnOnlinePay.BorderColor = Color.DarkOrange;

                        btnChallan.BackColor = Color.DarkOrange;
                        btnChallan.BorderColor = Color.DarkOrange;
                    }
                    else
                    {
                        btnOnlinePay.Enabled = true;
                        btnChallan.Enabled = true;
                    }


                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Activity_SessionActivityDefinition.LoadDefinedSessionActivities() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private bool CheckActivity()
    {
        try
        {
            bool ret = true;
            string sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND SA.CLG_ID = " + ViewState["COLLEGE_ID"].ToString() + ")");
            if (sessionno != "")
            {
                DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), Convert.ToString(Degreeno), Convert.ToString(branchno), Convert.ToString(Semesterno));
                if (dtr.Read())
                {
                    if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                    {
                        objCommon.DisplayMessage(pnlTransferCredit, "This Activity has been Stopped. Contact Admin.!!", this.Page);
                        ret = false;
                    }

                    if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                    {
                        objCommon.DisplayMessage(pnlTransferCredit, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                        ret = false;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(pnlTransferCredit, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                    ret = false;
                }
                dtr.Close();
                return ret;
            }
            else
            {
                objCommon.DisplayMessage(pnlTransferCredit, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                ret = false;
                return ret;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.ShowError(Page, "ACADEMIC_Feespayment.CheckActivity() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            else
            {
                objCommon.ShowError(Page, "Server Unavailable.");
                return false;
            }
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                //Response.Redirect("~/notauthorized.aspx?page=~/Feespayment.aspx");
                Response.Redirect("~/Default.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            //Response.Redirect("~/notauthorized.aspx?page=~/Feespayment.aspx");
            Response.Redirect("~/Default.aspx");
        }
    }


    private void GetSessionValues()
    {
        Session["FirstName"] = lblname.Text;
        Session["RegNo"] = lblapp.Text;
        Session["MobileNo"] = lblmobile.Text;
        Session["EMAILID"] = lblEmail.Text;
        Session["OrderID"] = lblOrderID.Text;
        Session["TOTAL_AMT"] = lbltotalfees.Text;
    }

    private void CreateCustomerRef()
    {
        Random rnd = new Random();
        int ir = rnd.Next(01, 10000);
        //lblOrderID.Text = Convert.ToString(Convert.ToString(Session["USERNO"]) + Convert.ToString(ir));
        lblOrderID.Text = Convert.ToString(Session["IDNO"] + Convert.ToString(ir));
        ViewState["ORDERID"] = lblOrderID.Text;
    }


    public static string byteToHexString(byte[] byData)
    {
        StringBuilder sb = new StringBuilder((byData.Length * 2));
        for (int i = 0; (i < byData.Length); i++)
        {
            int v = (byData[i] & 255);
            if ((v < 16))
            {
                sb.Append('0');
            }

            sb.Append(v.ToString("X"));

        }

        return sb.ToString();
    }
    protected void ddlsem_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindInstallmentDetailsInListview();
        if (ddlReceiptType.SelectedIndex == 0)
        {
            lblRecpttype.Visible = false;
            lblRecpttypeAmount.Visible = false;
            Label4.Visible = false;
        }
        else
        {
            lblRecpttype.Visible = true;
            lblRecpttypeAmount.Visible = true;
            Label4.Visible = true;
        }
        int ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["IDNO"]) + " AND SESSIONNO =" + Convert.ToInt32(lblSession.ToolTip) + " AND SEMESTERNO =" + Convert.ToInt32(ddlsem.SelectedValue) + " AND RECIEPT_CODE = '" + ddlReceiptType.SelectedValue + "' AND RECON = 1 AND ISNULL(CAN,0)=0"));

        if (ifPaidAlready > 0)
        {
            objCommon.DisplayMessage("Fee has been paid already. Can't proceed with the transaction !", this);
            lblMsg.Text = "Fee has been paid already. Can't proceed with the transaction !";
            btnPAY.Visible = false;
            btnReports.Visible = true;
            return;
        }
        else
        {
            btnReports.Visible = false;
            lblMsg.Text = string.Empty;
        }

        //gettotal amount
        //check this is installment person or not
        int IsInstallment = Convert.ToInt32(objCommon.LookUp("ACD_STUD_INSTALMENT_AMOUNT", "COUNT(1)", "IDNO=" + Convert.ToInt32(Session["IDNO"]) + " AND SESSION_NO =" + Convert.ToInt32(lblSession.ToolTip)));
        if (ddlReceiptType.SelectedValue == "TF" || ddlReceiptType.SelectedValue == "HF" || ddlReceiptType.SelectedValue == "TPF")
        {
            if (ifPaidAlready > 0)
            {

            }
            else
            {

                string receipt_type = ddlReceiptType.SelectedValue;
                int recept = Convert.ToInt32(objCommon.LookUp("ACD_Reciept_Type", "RCPTTYPENO", "RECIEPT_CODE ='" + receipt_type + "'"));

                Label4.Visible = true;

                string receiptname = ddlReceiptType.SelectedItem.Text;
                lblRecpttype.Text = receiptname;


                foreach (ListViewDataItem dataitem in lvFeesPayment.Items)
                {
                    Label lblFINAL_TOTAL_AMT = dataitem.FindControl("lblFINAL_TOTAL_AMT") as Label;
                    lblRecpttypeAmount.Text = lblFINAL_TOTAL_AMT.Text.ToString();
                }

                //double DemandAmount = Convert.ToDouble(objCommon.LookUp("ACD_DEMAND d inner join ACD_RECIEPT_TYPE rt on rt.reciept_code=d.reciept_code", "d.TOTAL_AMT", "IDNO=" + Convert.ToInt32(Session["IDNO"]) + " AND sessionno =" + Convert.ToInt32(lblSession.ToolTip) + " and rt.RCPTTYPENO =" + recept));
                //lblRecpttypeAmount.Text = DemandAmount.ToString();

            }
        }

        // for adding late fee in demand
        double LateFee = 0;//comment by pankaj nakhale 11 march 2020    

        lblLateFee.Text = LateFee.ToString("0.00");
        btnPAY.Visible = true;
        btnReports.Visible = false;

        ////////////////////////////////// add for giving installment details on based on  receipt type code by pankaj nakhale 13 march 2020 ///////////////////////

    }


    //ADDED ON [17 march 2020]GENERATE REPORT AFTER ONLINE PAYMENT DONE SUCCESSFULLY. br pankaj nakhale
    private void ShowReport_NEW(string reportTitle, string rptFileName, int OrderId)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_ORDER_ID=" + OrderId + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Atom_Response.ShowReport_NEW() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //protected void PostOnlinePayment(double Amount, int orderid)
    //{
    //    #region Declarations
    //    string feeAmount = string.Empty;
    //    string Transacionid = "NA";
    //    string TransactionFor = string.Empty;
    //    string TSPLTxnCode = string.Empty;
    //    string TSPLtxtITC = string.Empty;
    //    #endregion

    //    #region Get Payment Details
    //    feeAmount = Amount.ToString();// (ViewState["Final_Amt"]).ToString();"1"; //
    //    #endregion

    //    #region Payment Log for Different Transaction Id
    //    string TransactionCode = string.Empty;
    //    TransactionCode = orderid.ToString(); //lblOrderID.Text; // This may be configured from Database for Different Running Number
    //    #endregion

    //    #region BillDesk Data Declaration
    //    string MerchantID = string.Empty;
    //    string UniTranNo = string.Empty;
    //    string NA1 = string.Empty;
    //    string txn_amount = string.Empty;
    //    string NA2 = string.Empty;
    //    string NA3 = string.Empty;
    //    string NA4 = string.Empty;
    //    string CurrencyType = string.Empty;
    //    string NA5 = string.Empty;
    //    string TypeField1 = string.Empty;
    //    string SecurityID = string.Empty;
    //    string NA6 = string.Empty;
    //    string NA7 = string.Empty;
    //    string TypeField2 = string.Empty;
    //    string additional_info1 = string.Empty;
    //    string additional_info2 = string.Empty;
    //    string additional_info3 = string.Empty;
    //    string additional_info4 = string.Empty;
    //    string additional_info5 = string.Empty;
    //    string additional_info6 = string.Empty;
    //    string additional_info7 = string.Empty;
    //    string ReturnURL = string.Empty;
    //    string ChecksumKey = string.Empty;
    //    #endregion

    //    #region Set Bill Desk Param Data
    //    MerchantID = ConfigurationManager.AppSettings["MerchantID"];
    //    UniTranNo = TransactionCode;
    //    txn_amount = feeAmount;
    //    CurrencyType = "INR";
    //    SecurityID = ConfigurationManager.AppSettings["SecurityCode"];
    //    additional_info1 = ViewState["STUDNAME"].ToString(); // Project Name
    //    additional_info2 = ViewState["IDNO"].ToString();  // Project Code
    //    additional_info3 = ddlReceiptType.SelectedValue; //ViewState["RECIEPT"].ToString(); // Transaction for??
    //    additional_info4 = ViewState["info"].ToString(); // Payment Reason
    //    additional_info5 = feeAmount; // Amount Passed
    //    additional_info6 = ViewState["basicinfo"].ToString();  // basic details like regno/enroll no/branchname
    //    additional_info7 = ViewState["SESSIONNO"].ToString(); 

    //    ReturnURL = "https://svce.mastersofterp.in/ACADEMIC/FeesPay_Response.aspx";

    //    //ReturnURL = "http://localhost:54865/PresentationLayer/ACADEMIC/FeesPay_Response.aspx";
    //    ChecksumKey = ConfigurationManager.AppSettings["ChecksumKey"];
    //    #endregion

    //    #region Generate Bill Desk Check Sum

    //    StringBuilder billRequest = new StringBuilder();
    //    billRequest.Append(MerchantID).Append("|");
    //    billRequest.Append(UniTranNo).Append("|");
    //    billRequest.Append("NA").Append("|");
    //    billRequest.Append(txn_amount).Append("|");
    //    billRequest.Append("NA").Append("|");
    //    billRequest.Append("NA").Append("|");
    //    billRequest.Append("NA").Append("|");
    //    billRequest.Append(CurrencyType).Append("|");
    //    billRequest.Append("DIRECT").Append("|");
    //    billRequest.Append("R").Append("|");
    //    billRequest.Append(SecurityID).Append("|");
    //    billRequest.Append("NA").Append("|");
    //    billRequest.Append("NA").Append("|");
    //    billRequest.Append("F").Append("|");
    //    billRequest.Append(additional_info1).Append("|");
    //    billRequest.Append(additional_info2).Append("|");
    //    billRequest.Append(additional_info3).Append("|");
    //    billRequest.Append(additional_info4).Append("|");
    //    billRequest.Append(additional_info5).Append("|");
    //    billRequest.Append(additional_info6).Append("|");
    //    billRequest.Append(additional_info7).Append("|");
    //    billRequest.Append(ReturnURL);

    //    string data = billRequest.ToString();

    //    String hash = String.Empty;
    //    hash = GetHMACSHA256(data, ChecksumKey);
    //    hash = hash.ToUpper();

    //    string msg = data + "|" + hash;

    //    #endregion

    //    #region Post to BillDesk Payment Gateway

    //    string PaymentURL = ConfigurationManager.AppSettings["BillDeskURL"] + msg;

    //    //Response.Redirect(PaymentURL, false);
    //    Response.Write("<form name='s1_2' id='s1_2' action='" + PaymentURL + "' method='post'> ");
    //    Response.Write("<script type='text/javascript' language='javascript' >document.getElementById('s1_2').submit();");
    //    Response.Write("</script>");
    //    Response.Write("<script language='javascript' >");
    //    Response.Write("</script>");
    //    Response.Write("</form> ");
    //    Response.Write("<script>window.open(" + PaymentURL + ",'_blank');</script>");
    //    #endregion
    //}



    //public string GetHMACSHA256(string text, string key)
    //{
    //    UTF8Encoding encoder = new UTF8Encoding();

    //    byte[] hashValue;
    //    byte[] keybyt = encoder.GetBytes(key);
    //    byte[] message = encoder.GetBytes(text);

    //    HMACSHA256 hashString = new HMACSHA256(keybyt);
    //    string hex = "";

    //    hashValue = hashString.ComputeHash(message);
    //    foreach (byte x in hashValue)
    //    {
    //        hex += String.Format("{0:x2}", x);
    //    }
    //    return hex;
    //}

    protected void btnOnlinePay_Click(object sender, EventArgs e)
    {
        #region "Online Payment"
        try
        {
            if (ddlReceiptType.SelectedIndex > 0)
            {

                Button btnOnlinePay = (Button)(sender);
                Button btnOnlinePayy = (Button)(sender);
                int InstallmentNo = Convert.ToInt32(btnOnlinePay.CommandArgument);
                double Amount = Convert.ToDouble(btnOnlinePayy.CommandName);

                ListViewItem item = (ListViewItem)(sender as Control).NamingContainer;
                //Find the label control
                Label lblAmount = (Label)item.FindControl("lblAmount");
                Label lblLateFee = (Label)item.FindControl("lblLateFee");
                Label lblReceiptCode = (Label)item.FindControl("lblReceiptCode");
                //HiddenField hfFinalAmt = (HiddenField)item.FindControl("hfFinalAmt");
                //HiddenField hfLateFee = (HiddenField)item.FindControl("hfLateFee");
                //HiddenField hfAmtWithoutLateFee = (HiddenField)item.FindControl("hfAmtWithoutLateFee");
                string payment_mode = "O";

                CreateCustomerRef();
                GetSessionValues();

                string receipt_type = ddlReceiptType.SelectedValue;
                if (receipt_type == "")
                {
                    objCommon.DisplayMessage(pnlTransferCredit, "Something Went Wrong !.", this.Page);
                    return;
                }

                //lblOrderID.Text = "50658706";

                //int order_id_count =Convert.ToInt32(objCommon.LookUp("ACD_DCR", "distinct count(order_id)", "IDNO=" + Session["IDNO"].ToString() + " AND RECIEPT_CODE='" + ddlReceiptType.SelectedValue + "' AND SESSIONNO=" + lblSession.ToolTip + " AND ORDER_ID= '" + lblOrderID.Text + "'"));
                int order_id_count = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(distinct order_id)", "ORDER_ID= '" + lblOrderID.Text + "'"));
                if (order_id_count > 0)
                {
                    objCommon.DisplayMessage(pnlTransferCredit, "Generated Order Id Already Exists...Please Try Again !!.", this.Page);
                    return;
                }



                int recept = Convert.ToInt32(objCommon.LookUp("ACD_Reciept_Type", "RCPTTYPENO", "RECIEPT_CODE ='" + lblReceiptCode.Text + "'"));
                int idno = Convert.ToInt32(Session["IDNO"]);
                int session = Convert.ToInt32(lblSession.ToolTip);


                //ViewState["AMOUNT"] = hfAmtWithoutLateFee.Value;
                //ViewState["LATE_FEE"] = hfLateFee.Value;
                //ViewState["FINAL_TOTALAMOUNT"] = hfFinalAmt.Value;

                //Session["FINAL_TOTALAMOUNT"] = ViewState["FINAL_TOTALAMOUNT"].ToString();
                Session["FINAL_TOTALAMOUNT"] = Amount;
                Session["AMOUNT"] = lblAmount.Text;
                Session["LATE_FEE"] = lblLateFee.Text;


                objStudentFees.Amount = Amount;
                objStudentFees.UserNo = (Convert.ToInt32(Session["IDNO"]));
                objStudentFees.SessionNo = lblSession.ToolTip;
                objStudentFees.OrderID = lblOrderID.Text;
                objStudentFees.TransDate = System.DateTime.Today;
                objStudentFees.BranchName = lblbranch.Text;
                objStudentFees.SessionNo = session.ToString();

                ObjNuc.InsertPendingAmountInDCR(Convert.ToInt32(Session["IDNO"]), Convert.ToInt32(lblSem.ToolTip), lblReceiptCode.Text, Convert.ToDouble(Session["AMOUNT"].ToString()), payment_mode,
                    Convert.ToInt32(lblOrderID.Text), session, recept, InstallmentNo, Convert.ToDouble(Session["LATE_FEE"].ToString()), Convert.ToDouble(Session["FINAL_TOTALAMOUNT"].ToString()));
                int result = 0;

                result = ObjNuc.SubmitFeesofStudent(objStudentFees, 1, 2, lblReceiptCode.Text, Convert.ToInt32(lblSem.ToolTip)
                    , Convert.ToDouble(Session["LATE_FEE"].ToString()), Convert.ToDouble(Session["FINAL_TOTALAMOUNT"].ToString()));
                int orderid = Convert.ToInt32(objStudentFees.OrderID);
                if (result > 0)
                {

                    DataSet d = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON B.BRANCHNO=S.BRANCHNO", "IDNO", "ISNULL(REGNO,'')REGNO,ISNULL(ENROLLNO,'')ENROLLNO,ISNULL(STUDNAME,'')STUDNAME,ISNULL(STUDENTMOBILE,'')STUDENTMOBILE,ISNULL(EMAILID,'')EMAILID,ISNULL(B.SHORTNAME,'')SHORTNAME", "IDNO = '" + Convert.ToInt32(Session["IDNO"]) + "'", "");
                    ViewState["STUDNAME"] = (d.Tables[0].Rows[0]["STUDNAME"].ToString());
                    ViewState["IDNO"] = (d.Tables[0].Rows[0]["IDNO"].ToString());
                    ViewState["EMAILID"] = (d.Tables[0].Rows[0]["EMAILID"].ToString());
                    ViewState["MOBILENO"] = (d.Tables[0].Rows[0]["STUDENTMOBILE"].ToString());
                    ViewState["REGNO"] = (d.Tables[0].Rows[0]["REGNO"].ToString());
                    ViewState["SESSIONNO"] = objStudentFees.SessionNo;
                    ViewState["SEM"] = Convert.ToInt32(lblSem.ToolTip);
                    ViewState["RECIEPT_CODE"] = lblReceiptCode.Text;
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

                    ViewState["info"] = ViewState["REGNO"] + "," + ViewState["ENROLLNO"] + "," + ViewState["SHORTNAME"] + "," + ViewState["SEM"] + "," + ViewState["MOBILENO"];
                   // ViewState["LateFee"] = ViewState["LATE_FEE"].ToString();
                    //ViewState["basicinfo"] = ViewState["ENROLLNO"];

                    //bill desk payment gateway
                    //PostOnlinePayment(Amount, orderid);

                    //call function of ccavenue payment gateway
                    SendTransactionCCAvenueHDFCAPI();
                }
                else
                {
                    objCommon.DisplayMessage(pnlTransferCredit, "Transaction Failed !.", this.Page);
                    return;
                }

            }
            else
            {
                objCommon.DisplayMessage(pnlTransferCredit, "Please Select Fees Type For Paying Fees..", this.Page);
            }

        }
        catch (Exception ex)
        {
            throw;
        }
        #endregion
    }


    #region "CC Avenue HDFC"

    CCACrypto ccaCrypto = new CCACrypto();

    int idno = 0; decimal TotalSum = 0;
    String tspl_txn_id = string.Empty;
    string session = string.Empty;

    public string action1 = string.Empty;
    public string hash1 = string.Empty;
    public string txnid1 = string.Empty;

    string ccaRequest = "";
    public string iframeSrc = "";
    public string strEncRequest = "";


    public void SendTransactionCCAvenueHDFCAPI()
    {
        try
        {
            //key.Value = ConfigurationManager.AppSettings["MERCHANT_KEY"];
            //Workingkey.Value = ConfigurationManager.AppSettings["workingKey"];
            //accesskey.Value = ConfigurationManager.AppSettings["strAccessCode"];
            //successurl.Value = ConfigurationManager.AppSettings["success_url"];
            //failurl.Value = ConfigurationManager.AppSettings["failure_url"];

            //string amt = ViewState["AMOUNT"].ToString();
            string[] hashVarsSeq;
            string hash_string = string.Empty;


            if (string.IsNullOrEmpty(Request.Form["txnid"])) // generating txnid
            {
                Random rnd = new Random();
                string strHash = Generatehash512(rnd.ToString() + DateTime.Now);
                txnid1 = strHash.ToString().Substring(0, 20);
            }
            else
            {
                txnid1 = Request.Form["txnid"];
            }


            Session["ORDERID"] = ViewState["ORDERID"].ToString();
           // Session["FINAL_TOTALAMOUNT"] = ViewState["FINAL_TOTALAMOUNT"].ToString();
            Session["STUDNAME"] = ViewState["STUDNAME"].ToString();
            Session["IDNO_FINAL"] = ViewState["IDNO"].ToString();
            Session["SESSIONNO"] = ViewState["SESSIONNO"].ToString();
            Session["RECIEPT_CODE"] = ViewState["RECIEPT_CODE"].ToString();
            Session["info"] = ViewState["info"].ToString();
            //Session["LateFee"] = ViewState["LateFee"].ToString();
            Session["REGNO"] = ViewState["REGNO"].ToString();

            //int orderid = Convert.ToInt32(Session["ORDERID"].ToString());
            //decimal amt2 = Convert.ToDecimal(Session["FINAL_TOTALAMOUNT"].ToString());

            ccaRequest = ccaRequest + "merchant_id=" + ConfigurationManager.AppSettings["MERCHANT_KEY"] + "&";
            ccaRequest = ccaRequest + "order_id=" + Session["ORDERID"].ToString() + "&";
            ccaRequest = ccaRequest + "amount=" + Session["FINAL_TOTALAMOUNT"].ToString() + "&";
            ccaRequest = ccaRequest + "currency=INR&";
            ccaRequest = ccaRequest + "redirect_url=" + ConfigurationManager.AppSettings["success_url"] + "&";
            ccaRequest = ccaRequest + "cancel_url=" + ConfigurationManager.AppSettings["failure_url"] + "&";
            ccaRequest = ccaRequest + "billing_name=" + Session["STUDNAME"].ToString() + "&";
            ccaRequest = ccaRequest + "merchant_param1=" + Session["IDNO_FINAL"].ToString() + "&";
            ccaRequest = ccaRequest + "merchant_param2=" + Session["SESSIONNO"].ToString() + "&";
            ccaRequest = ccaRequest + "merchant_param3=" + Session["RECIEPT_CODE"].ToString() + "&";
            ccaRequest = ccaRequest + "merchant_param4=" + Session["info"].ToString() + "&";
            ccaRequest = ccaRequest + "merchant_param5=" + Session["LATE_FEE"].ToString() + "" + "&";
            strEncRequest = ccaCrypto.Encrypt(ccaRequest, ConfigurationManager.AppSettings["workingKey"]);
            //iframeSrc = "https://secure.ccavenue.com/transaction/transaction.do?command=initiateTransaction&encRequest=" + strEncRequest + "&access_code=" + accesskey.Value;
            iframeSrc = "https://test.ccavenue.com/transaction/transaction.do?command=initiateTransaction&encRequest=" + strEncRequest + "&access_code=" + ConfigurationManager.AppSettings["strAccessCode"];
            if (!string.IsNullOrEmpty(ccaRequest))
            {
                //hash.Value = hash1;
                //txnid.Value = txnid1;
                System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
                data.Add("txnid", txnid1);
                data.Add("key", ConfigurationManager.AppSettings["MERCHANT_KEY"]);
                data.Add("order_id", Session["ORDERID"].ToString());
                string AmountForm = Convert.ToDecimal(Session["FINAL_TOTALAMOUNT"].ToString().Trim()).ToString("g29");// eliminating trailing zeros
                data.Add("amount", AmountForm);
                data.Add("currency", "INR");
                data.Add("firstname", Session["STUDNAME"].ToString().Trim());
                data.Add("productinfo", Convert.ToString(1));
                data.Add("programme", "Online Fees Payment");
                data.Add("registration_no", Session["REGNO"].ToString());
                data.Add("redirect_url", ConfigurationManager.AppSettings["success_url"]);
                data.Add("cancel_url", ConfigurationManager.AppSettings["failure_url"]);
                string strForm = PreparePOSTForm(iframeSrc, data);
                Page.Controls.Add(new LiteralControl(strForm));
            }
            else
            {
                //no hash
            }



            //ccaRequest = ccaRequest + "merchant_id=" + ConfigurationManager.AppSettings["MERCHANT_KEY"] + "&";
            //ccaRequest = ccaRequest + "order_id=" + ViewState["ORDERID"].ToString() + "&";
            //ccaRequest = ccaRequest + "amount=" + ViewState["FINAL_TOTALAMOUNT"].ToString() + "&";
            //ccaRequest = ccaRequest + "currency=INR&";
            //ccaRequest = ccaRequest + "redirect_url=" + ConfigurationManager.AppSettings["success_url"] + "&";
            //ccaRequest = ccaRequest + "cancel_url=" + ConfigurationManager.AppSettings["failure_url"] + "&";
            //ccaRequest = ccaRequest + "billing_name=" + ViewState["STUDNAME"].ToString() + "&";
            //ccaRequest = ccaRequest + "merchant_param1=" + ViewState["IDNO"].ToString() + "&";
            //ccaRequest = ccaRequest + "merchant_param2=" + ViewState["SESSIONNO"].ToString() + "&";
            //ccaRequest = ccaRequest + "merchant_param3=" + ViewState["RECIEPT_CODE"].ToString() + "&";
            //ccaRequest = ccaRequest + "merchant_param4=" + ViewState["info"].ToString() + "&";
            //ccaRequest = ccaRequest + "merchant_param5=" + ViewState["LateFee"].ToString() + "" + "&";
            //strEncRequest = ccaCrypto.Encrypt(ccaRequest, ConfigurationManager.AppSettings["workingKey"]);
            ////iframeSrc = "https://secure.ccavenue.com/transaction/transaction.do?command=initiateTransaction&encRequest=" + strEncRequest + "&access_code=" + accesskey.Value;
            //iframeSrc = "https://test.ccavenue.com/transaction/transaction.do?command=initiateTransaction&encRequest=" + strEncRequest + "&access_code=" + ConfigurationManager.AppSettings["strAccessCode"];
            //if (!string.IsNullOrEmpty(ccaRequest))
            //{
            //    //hash.Value = hash1;
            //    //txnid.Value = txnid1;
            //    System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
            //    data.Add("txnid", txnid1);
            //    data.Add("key", ConfigurationManager.AppSettings["MERCHANT_KEY"]);
            //    data.Add("order_id", ViewState["ORDERID"].ToString());
            //    string AmountForm = Convert.ToDecimal(ViewState["FINAL_TOTALAMOUNT"].ToString().Trim()).ToString("g29");// eliminating trailing zeros
            //    data.Add("amount", AmountForm);
            //    data.Add("currency", "INR");
            //    data.Add("firstname", ViewState["STUDNAME"].ToString().Trim());
            //    data.Add("productinfo", Convert.ToString(1));
            //    data.Add("programme", "Online Fees Payment");
            //    data.Add("registration_no", ViewState["REGNO"].ToString());
            //    data.Add("redirect_url", ConfigurationManager.AppSettings["success_url"]);
            //    data.Add("cancel_url", ConfigurationManager.AppSettings["failure_url"]);
            //    string strForm = PreparePOSTForm(iframeSrc, data);
            //    Page.Controls.Add(new LiteralControl(strForm));
            //}
            //else
            //{
            //    //no hash
            //}

        }

        catch (Exception ex)
        {
            Response.Write("<span style='color:red'>" + ex.Message + "</span>");
        }

    }

    public string Generatehash512(string text)
    {

        byte[] message = Encoding.UTF8.GetBytes(text);

        UnicodeEncoding UE = new UnicodeEncoding();
        byte[] hashValue;
        SHA512Managed hashString = new SHA512Managed();
        string hex = "";
        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;

    }

    private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
    {
        //Set a name for the form
        string formID = "PostForm";
        //Build the form using the specified data to be posted.
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" +
                       formID + "\" action=\"" + url +
                       "\" method=\"POST\" >");
        strForm.Append("<input type=\"hidden\" id=\"encRequest\" name=\"encRequest\" value=\"" + strEncRequest + "\">");
        strForm.Append("<input type=\"hidden\" name=\"access_code\" id=\"access_code\" value=\"" + ConfigurationManager.AppSettings["strAccessCode"] + "\">");
        strForm.Append("</form>");
        //  Build the JavaScript which will do the Posting operation.
        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        strScript.Append("var v" + formID + " = document." + formID + ";");
        strScript.Append("v" + formID + ".submit();");
        strScript.Append("</script>");
        //Return the form and the script concatenated.
        //(The order is important, Form then JavaScript)
        return strForm.ToString() + strScript.ToString();
    }

    #endregion

    //this is used for get reciept for all payment type added byt pankaj nakhale 17 march 2020
    protected void btnReceipt_Click(object sender, EventArgs e)
    {
        if (ddlReceiptType.SelectedIndex > 0)
        {
            Button btnReceipt = (Button)(sender);
            string order_id = Convert.ToString(btnReceipt.CommandArgument);
            this.ShowReport_receiptTypewise("Payment_Details", "PaymentReceiptInstallment_all_receipt_type.rpt", order_id);
        }
        else
        {
            objCommon.DisplayMessage(pnlTransferCredit, "Please Select Receipt Type For Receipt Generation..", this.Page);
            //objCommon.ShowError(this.Page, "Please Select Fees Type For paying the Fees..");
        }
    }
 


    protected void btnChallan_Click(object sender, EventArgs e)
    {
        if (ddlReceiptType.SelectedIndex > 0)
        {
            ////////////////////////for challan insert added by pankaj 18032020///////////////////////////////////
            Button btnChallan = (Button)(sender);
            Button btnChallann = (Button)(sender);
            int InstallmentNo = Convert.ToInt32(btnChallan.CommandArgument);
            double Amount = Convert.ToDouble(btnChallann.CommandName);
            string payment_mode = "C";

            ListViewItem item = (ListViewItem)(sender as Control).NamingContainer;
            //Find the label control
            HiddenField hfFinalAmt = (HiddenField)item.FindControl("hfFinalAmt");
            HiddenField hfLateFee = (HiddenField)item.FindControl("hfLateFee");
            HiddenField hfAmtWithoutLateFee = (HiddenField)item.FindControl("hfAmtWithoutLateFee");

            CreateCustomerRef();
            GetSessionValues();

            string receipt_type = ddlReceiptType.SelectedValue;
            int recept = Convert.ToInt32(objCommon.LookUp("ACD_Reciept_Type", "RCPTTYPENO", "RECIEPT_CODE ='" + receipt_type + "'"));
            int idno = Convert.ToInt32(Session["IDNO"]);
            int session = Convert.ToInt32(lblSession.ToolTip);


            ViewState["AMOUNT"] = hfAmtWithoutLateFee.Value;
            ViewState["LATE_FEE"] = hfLateFee.Value;
            ViewState["FINAL_TOTALAMOUNT"] = hfFinalAmt.Value;


            objStudentFees.Amount = Amount;
            objStudentFees.UserNo = (Convert.ToInt32(Session["IDNO"]));
            objStudentFees.SessionNo = lblSession.ToolTip;
            objStudentFees.OrderID = lblOrderID.Text;
            objStudentFees.TransDate = System.DateTime.Today;
            objStudentFees.BranchName = lblbranch.Text;
            objStudentFees.SessionNo = session.ToString();



            int DCRNOTEMP = ObjNuc.InsertAmountInDCR_forBankChallan(Convert.ToInt32(Session["IDNO"]), Convert.ToInt32(lblSem.ToolTip),
                ddlReceiptType.SelectedValue.ToString(), Convert.ToDouble(ViewState["AMOUNT"].ToString()), payment_mode, Convert.ToInt32(lblOrderID.Text), session, recept,
                InstallmentNo, Convert.ToDouble(ViewState["LATE_FEE"].ToString()), Convert.ToDouble(ViewState["FINAL_TOTALAMOUNT"].ToString()));

            this.ShowReport_BankChallan_forInstallmentStudent("Payment_Details", "rptBankChallan_ForInstallmentStudent.rpt", DCRNOTEMP);

            BindInstallmentDetailsInListview();
        }
        else
        {
            objCommon.DisplayMessage(pnlTransferCredit, "Please Select Fees Type For Paying  Fees..", this.Page);
        }
    }


    //ADDED ON [17 march 2020]GENERATE REPORT AFTER ONLINE PAYMENT DONE SUCCESSFULLY.
    private void ShowReport_receiptTypewise(string reportTitle, string rptFileName,string _Order_id)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_TRANSACTION_NO=" + ViewState["Vmer_txn"] + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_IDNO=" + Convert.ToInt32(Session["IDNO"]) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_RECIEPT_CODE=" + ddlReceiptType.SelectedValue.ToString() + ",@P_SESSIONNO=" + lblSession.ToolTip.ToString() + ",@P_COLLEGE_ID=" + ViewState["COLLEGE_ID"].ToString() + ",@P_ORDER_ID=" + _Order_id.ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Atom_Response.ShowReport_NEW() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    
    
    private void ShowReport_BankChallan_forInstallmentStudent(string reportTitle, string rptFileName, int DCRNOTEMP)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_IDNO=" + Convert.ToInt32(Session["IDNO"]) + ",@P_SESSIONNO=" + lblSession.ToolTip + ",@P_RECEIPTTYPE=" + ddlReceiptType.SelectedValue + ",@P_tempdcr=" + DCRNOTEMP + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Atom_Response.ShowReport_NEW() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

}