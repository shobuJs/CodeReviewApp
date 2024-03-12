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
public partial class ACADEMIC_Remaining_Fees_Collection : System.Web.UI.Page
{
    Common objCommon = new Common();
    UserController user = new UserController();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    int idno = 0; decimal TotalSum = 0; String tspl_txn_id = string.Empty;
    string session = string.Empty;
    IITMS.UAIMS.BusinessLayer.BusinessEntities.DocumentAcad objdocument = new DocumentAcad();
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
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
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }

                    if (Session["usertype"].ToString().Equals("2"))     //Student 
                    {
                       
                        ViewState["action"] = "add";
                        ShowStudentDetails();
                        GetPreviousReceipt();
                        fillDetails();
                        //bindpaymentdata();
                        BindInstallment();
                        objCommon.FillDropDownList(ddlbank, "ACD_BANK", "DISTINCT BANKNO", "BANKNAME", "BANKNO > 0", "BANKNO");
                        objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO>0 AND RCPTTYPENO IN(1,2)", "RECIEPT_CODE");//RCPTTYPENO
                        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO <=(SELECT SEMESTERNO + 1 FROM ACD_STUDENT WITH (NOLOCK) WHERE IDNO= "+ Convert.ToInt32(Session["idno"]) +") AND SEMESTERNO>0", "SEMESTERNO DESC");
                    
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(updwithapp, "Record Not Found.This Page is use for Online Payment for only Student Login!!", this.Page);
                    }
                    //objCommon.SetLabelData("0");//for label
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
                }

            }
           

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACDEMIC_COLLEGE_MASTER.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Remaining_Fees_Collection.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Remaining_Fees_Collection.aspx");
        }
    }

    private void ShowStudentDetails()
    {
        try
        {
            int idno = 0;
            if (Session["usertype"].ToString().Equals("2"))     //Student 
            {
                idno = Convert.ToInt32(Session["idno"]);
                //  string branchno = objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + idno);
                DataSet dsStudent = feeController.GetStudentInfoById_ForOnlinePayment(idno);
                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    DataRow dr = dsStudent.Tables[0].Rows[0];
                    //lblStudName.Text = dr["STUDNAME"].ToString();

                    string fullName = dr["STUDNAME"].ToString();
                    string[] names = fullName.Split(' ');
                    string name = names.First();
                    string lasName = names.Last();
                    lblStudName.Text = fullName;
                    // lblStudLastName.Text = lasName;

                    lblSex.Text = dr["SEX"].ToString() == string.Empty ? string.Empty : dr["SEX"].ToString();
                    //lblRegNo.Text = dr["REGNO"].ToString();
                    // lblDateOfAdm.Text = ((dr["ADMDATE"].ToString().Trim() != string.Empty) ? Convert.ToDateTime(dr["ADMDATE"].ToString()).ToShortDateString() : dr["ADMDATE"].ToString());
                    lblRegNo.Text = dr["REGNO"].ToString() == string.Empty ? string.Empty : dr["REGNO"].ToString();
                    lblPaymentType.Text = dr["PAYTYPENAME"].ToString() == string.Empty ? string.Empty : dr["PAYTYPENAME"].ToString();
                    lblDegree.Text = dr["DEGREENAME"].ToString() + '-' + dr["BRANCH_NAME"].ToString().ToString();
                    //lblBranchs.Text = dr["BRANCH_NAME"].ToString() == string.Empty ? string.Empty : dr["BRANCH_NAME"].ToString();
                    lblYear.Text = dr["YEARNAME"].ToString() == string.Empty ? string.Empty : dr["YEARNAME"].ToString();
                    lblSemester.Text = dr["SEMESTERNAME"].ToString() == string.Empty ? string.Empty : dr["SEMESTERNAME"].ToString();
                    lblBatch.Text = dr["BATCHNAME"].ToString() == string.Empty ? string.Empty : dr["BATCHNAME"].ToString();
                    lblMobileNo.Text = dr["STUDENTMOBILE"].ToString() == string.Empty ? string.Empty : dr["STUDENTMOBILE"].ToString();
                    lblEmailID.Text = dr["EMAILID"].ToString() == string.Empty ? string.Empty : dr["EMAILID"].ToString();
                    //lblBranchs.ToolTip = dr["BRANCHNO"].ToString();
                    lblDegree.ToolTip = dr["DEGREENO"].ToString();
                    ViewState["degreeno"] = dr["DEGREENO"].ToString();
                    ViewState["BRANCHNO"] = dr["BRANCHNO"].ToString();
                    ViewState["YEAR"] = dr["YEAR"].ToString();
                    ViewState["batchno"] = Convert.ToInt32(dr["ADMBATCH"].ToString());
                    ViewState["semesterno"] = Convert.ToInt32(dr["SEMESTERNO"].ToString());
                    ViewState["paytypeno"] = Convert.ToInt32(dr["PTYPE"].ToString());
                    ViewState["RECIEPT_CODE"] = dr["RECIEPT_CODE"].ToString() == string.Empty ? string.Empty : dr["RECIEPT_CODE"].ToString();
                    ViewState["SESSIONNO"] = dr["SESSIONNO"].ToString() == string.Empty ? string.Empty : dr["SESSIONNO"].ToString();
                    lblCollege.Text = dr["COLLEGENAME"].ToString() == string.Empty ? string.Empty : dr["COLLEGENAME"].ToString();
                    hdnCollege.Value = dr["COLLEGE_ID"].ToString();
                    ViewState["COLLEGE_ID"] = hdnCollege.Value;
                    //ViewState["HOSTEL_SESSIONNO"] = objCommon.LookUp("ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "FLOCK = 1");


                }
                else
                {
                    objCommon.DisplayUserMessage(updwithapp, "Record Not Found.", this.Page);
                }
            }
            else
            {
                objCommon.DisplayUserMessage(updwithapp, "Record Not Found.This Page is use for Online Payment for Student Login!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.ShowStudentDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lvStudentFees_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Label lblAmount = e.Item.FindControl("lblAmount") as Label;
            TotalSum = Convert.ToDecimal(lblAmount.Text);
            ViewState["demandamounttobepaid"] = TotalSum;
            txtchallanAmount.Text = TotalSum.ToString();
            //txtOrderid.Text = "11";
        }
    }
    protected void lvStudentFees_PreRender(object sender, EventArgs e)
    {
        DataSet ds = null;
        session = objCommon.LookUp("ACD_DEMAND", "(isnull(SESSIONNO,0))", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND ISNULL(CAN,0) = 0 AND ISNULL(DELET,0) = 0 AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + " AND PAYTYPENO =" + Convert.ToInt32(ViewState["paytypeno"]) + " AND ADMBATCHNO=" + Convert.ToInt32(ViewState["batchno"]) + "and RECIEPT_CODE=" + "'" + Convert.ToString(ddlReceiptType.SelectedValue) + "'");
        if (session != "")
        {
            ds = feeController.GetFeeItems_Data_ForOnlinePayment(Convert.ToInt32(session), Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(ddlReceiptType.SelectedValue), Convert.ToInt32(ViewState["paytypeno"]), Convert.ToInt32(ViewState["batchno"]));//, ref status);
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            btnSubmit.Visible = true;
            btnSubmit.Enabled = true;
            btnCancel.Visible = true; 
            pnlStudentsFees.Visible = true;
            //TRSPayOption.Visible = true; 
            TRNote.Visible = true;
            divViewpayment.Visible = true;
            lvStudentFees.DataSource = ds;
            lvStudentFees.DataBind();

        }
        Label lbltotal = this.lvStudentFees.FindControl("lbltotal") as Label;
        ViewState["Amount"] = TotalSum.ToString();
        if (TotalSum.ToString() != "0")
            lbltotal.Text = TotalSum.ToString();
        txtAmount.Text = TotalSum.ToString();
        hdfAmount.Value = txtAmount.Text;
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null; ;
            int status = 0;
            string session = "";
            if (ddlReceiptType.SelectedIndex > 0)
            {
                if (ddlSemester.SelectedIndex > 0)
                {
                    if (ddlReceiptType.SelectedValue == "HF")
                    {
                        if (ViewState["HOSTEL_SESSIONNO"] != "")
                        ds = feeController.GetFeeItems_Data_ForOnlinePayment(Convert.ToInt32(ViewState["HOSTEL_SESSIONNO"]), Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToString(ddlReceiptType.SelectedValue), Convert.ToInt32(ViewState["paytypeno"]), Convert.ToInt32(ViewState["batchno"]));//, ref status);
                    }
                    else
                    {
                        session = objCommon.LookUp("ACD_DEMAND", "(isnull(SESSIONNO,0))", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND PAYTYPENO =" + Convert.ToInt32(ViewState["paytypeno"]) + "  AND ADMBATCHNO=" + Convert.ToInt32(ViewState["batchno"]) + "and RECIEPT_CODE='"+ ddlReceiptType.SelectedValue+ "'");
                        string idno = objCommon.LookUp("ACD_DCR", "count(idno)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND YEAR=" + Convert.ToInt32(ddlSemester.SelectedValue) + "  and degreeno=" + Convert.ToInt32(lblDegree.ToolTip) + "and branchno=" + Convert.ToInt32(ViewState["BRANCHNO"]) + " and RECIEPT_CODE='" + ddlReceiptType.SelectedValue + "'");
                        if (idno != "0")
                        {
                            ds = feeController.GetFeeItems_Data_ForOnlinePayment(Convert.ToInt32(session), Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["semesterno"].ToString()), Convert.ToString(ddlReceiptType.SelectedValue), Convert.ToInt32(ViewState["paytypeno"]), Convert.ToInt32(ViewState["batchno"]));//, ref status);

                        }
                        else
                        {
                            session = objCommon.LookUp("ACD_DEMAND", "(isnull(SESSIONNO,0))", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND PAYTYPENO =" + Convert.ToInt32(ViewState["paytypeno"]) + "  AND ADMBATCHNO=" + Convert.ToInt32(ViewState["batchno"]) + "and RECIEPT_CODE='" + ddlReceiptType.SelectedValue + "'");
                            if (session != "")
                            {
                                ds = feeController.GetFeeItems_Data_ForOnlinePayment(Convert.ToInt32(session), Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["semesterno"].ToString()), Convert.ToString(ddlReceiptType.SelectedValue), Convert.ToInt32(ViewState["paytypeno"]), Convert.ToInt32(ViewState["batchno"]));//, ref status);
                            }
                            else
                            {

                                CheckPrevDemand();
                            }
                        }

                    }
                    if (status != -99 && ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        payoption.Visible = true;
                        btnCancel.Visible = true;
                        divViewpayment.Visible = true;
                        pnlStudentsFees.Visible = true;
                        lvStudentFees.DataSource = ds;
                        lvStudentFees.DataBind();
                        divOfflinePay.Visible = true;
                        payoption.Visible = true;
                    }
                    //else
                    //{
                    //    CheckPrevDemand();

                    //}
                }
                else
                {
                    btnSubmit.Visible = false; btnCancel.Visible = false; pnlStudentsFees.Visible = false; payoption.Visible = false; divShowPay.Visible = false; divOfflinePay.Visible = false;
                    objCommon.DisplayUserMessage(updwithapp, "Please Select Semester.", this.Page);
                    ddlReceiptType.Focus();
                }
            }
            else
            {
                btnSubmit.Visible = false; btnCancel.Visible = false; pnlStudentsFees.Visible = false; payoption.Visible = false; divShowPay.Visible = false; divOfflinePay.Visible = false;
                objCommon.DisplayUserMessage(updwithapp, "Please Select Receipt Type.", this.Page);
                ddlReceiptType.Focus();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void CheckPrevDemand()
    {
        btnSubmit.Visible = false; btnCancel.Visible = false; pnlStudentsFees.Visible = false;
        DataSet ds = null;
        session = objCommon.LookUp("ACD_DEMAND", "(isnull(SESSIONNO,0))", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND YEAR=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND PAYTYPENO =" + Convert.ToInt32(ViewState["paytypeno"]) + " and CUR_NO=" + ddlReceiptType.SelectedValue + " AND ADMBATCHNO=" + Convert.ToInt32(ViewState["batchno"]) + "and RECIEPT_CODE=" + "'" + Convert.ToString("RF") + "'");
        string idno = objCommon.LookUp("ACD_DCR", "count(idno)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND YEAR=" + Convert.ToInt32(ddlSemester.SelectedValue) + "  and CUR_NO=" + ddlReceiptType.SelectedValue + "and degreeno=" + Convert.ToInt32(lblDegree.ToolTip) + "and branchno=" + Convert.ToInt32(ViewState["BRANCHNO"]) + " and RECIEPT_CODE=" + "'" + Convert.ToString("RF") + "'");
        if (idno != "0")
        {
            ds = feeController.GetFeeItems_Data_ForOnlinePayment_Roya(Convert.ToInt32(session), Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["semesterno"].ToString()), Convert.ToString("RF"), Convert.ToInt32(ViewState["paytypeno"]), Convert.ToInt32(ViewState["batchno"]), Convert.ToInt32(ddlReceiptType.SelectedValue));//, ref status);

        }
        else
        {
            session = objCommon.LookUp("ACD_DEMAND", "(isnull(SESSIONNO,0))", "IDNO=" + Convert.ToInt32(Session["idno"]) + " and CUR_NO=" + ddlReceiptType.SelectedValue + " AND YEAR=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND PAYTYPENO =" + Convert.ToInt32(ViewState["paytypeno"]) + " AND ADMBATCHNO=" + Convert.ToInt32(ViewState["batchno"]) + "and RECIEPT_CODE=" + "'" + Convert.ToString("RF") + "'");
            if (session != "")
            {
                ds = feeController.GetFeeItems_Data_ForOnlinePayment_Roya(Convert.ToInt32(session), Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["semesterno"].ToString()), Convert.ToString("RF"), Convert.ToInt32(ViewState["paytypeno"]), Convert.ToInt32(ViewState["batchno"]), Convert.ToInt32(ddlReceiptType.SelectedValue));//, ref status);
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    lvStudentFees.DataSource = ds;
                    lvStudentFees.DataBind();
                    pnlStudentsFees.Visible = true;
                    payoption.Visible = true;
                    divShowPay.Visible = false;
                    divOfflinePay.Visible = true;
                    payoption.Visible = true;
                }
                else
                {
                    objCommon.DisplayUserMessage(updwithapp, "Error!!!", this.Page);
                }
            }
            else if (ddlReceiptType.SelectedValue != "HF")
            {
                this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'>";
                //this.divMsg.InnerHtml += " if(confirm('No demand found for semester " + lblSemester.Text + ".\\nDo you want to create demand for this semester?'))";

                this.divMsg.InnerHtml += " if(confirm('Do you want to confirm?'))";
                this.divMsg.InnerHtml += "{__doPostBack('CreateDemand', '');}</script>";
            }
            else
            {
                objCommon.DisplayUserMessage(updwithapp, "No Room Alloted!!!", this.Page);
            }
        }
    }
    private void CreateDemand(FeeDemand feeDemand, int paymentTypeNoOld)
    {
        try
        {
            if (feeController.CreateNewDemand_RoyalFEES(feeDemand, paymentTypeNoOld, Convert.ToInt32(ddlReceiptType.SelectedValue)))
            {
                this.PopulateFeeItemsSection(feeDemand.SemesterNo);
            }
            else
            {
                objCommon.DisplayUserMessage(updwithapp, "Standard Fees Not Created", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.btnCreateDemand_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void PopulateFeeItemsSection(int semesterNo)
    {
        try
        {
            DataSet ds = null;
            int status = 0;
            string session = "";
            if (ddlReceiptType.SelectedIndex > 0)
            {
                if (ddlSemester.SelectedIndex > 0)
                {
                    session = objCommon.LookUp("ACD_DEMAND", "(isnull(SESSIONNO,0))", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND YEAR=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND PAYTYPENO =" + Convert.ToInt32(ViewState["paytypeno"]) + " and CUR_NO=" + ddlReceiptType.SelectedValue + " AND ADMBATCHNO=" + Convert.ToInt32(ViewState["batchno"]) + "and RECIEPT_CODE=" + "'" + Convert.ToString("RF") + "'");
                    string idno = objCommon.LookUp("ACD_DCR", "count(idno)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND YEAR=" + Convert.ToInt32(ddlSemester.SelectedValue) + "  and CUR_NO=" + ddlReceiptType.SelectedValue + "and degreeno=" + Convert.ToInt32(lblDegree.ToolTip) + "and branchno=" + Convert.ToInt32(ViewState["BRANCHNO"]) + " and RECIEPT_CODE=" + "'" + Convert.ToString("RF") + "'");
                    if (idno != "0")
                    {
                        ds = feeController.GetFeeItems_Data_ForOnlinePayment_Roya(Convert.ToInt32(session), Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["semesterno"].ToString()), Convert.ToString("RF"), Convert.ToInt32(ViewState["paytypeno"]), Convert.ToInt32(ViewState["batchno"]), Convert.ToInt32(ddlReceiptType.SelectedValue));//, ref status);

                    }
                    else
                    {
                        session = objCommon.LookUp("ACD_DEMAND", "(isnull(SESSIONNO,0))", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND ISNULL(CAN,0) = 0 AND ISNULL(DELET,0) = 0 AND YEAR=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND PAYTYPENO =" + Convert.ToInt32(ViewState["paytypeno"]) + " AND ADMBATCHNO=" + Convert.ToInt32(ViewState["batchno"]) + "and RECIEPT_CODE=" + "'" + Convert.ToString("RF") + " AND CUR_NO=" + Convert.ToInt32(ddlReceiptType.SelectedValue) + "'");
                        if (session != "")
                        {
                            //CreateDemandForCurrentFeeCriteriamis();
                            ds = feeController.GetFeeItems_Data_ForOnlinePayment_Roya(Convert.ToInt32(session), Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["semesterno"].ToString()), Convert.ToString("RF"), Convert.ToInt32(ViewState["paytypeno"]), Convert.ToInt32(ViewState["batchno"]), Convert.ToInt32(ddlReceiptType.SelectedValue));//, ref status);
                        }
                        else
                        {
                            CheckPrevDemand();
                        }
                    }
                    if (status != -99 && ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        btnCancel.Visible = true; pnlStudentsFees.Visible = true;
                        lvStudentFees.DataSource = ds;
                        lvStudentFees.DataBind();
                        payoption.Visible = true;
                        divShowPay.Visible = false;
                        divOfflinePay.Visible = true;
                        payoption.Visible = true;
                    }

                    //else
                    //{
                    //    CheckPrevDemand();
                    //}
                }
                else
                {
                    btnSubmit.Visible = false; btnCancel.Visible = false; pnlStudentsFees.Visible = false; payoption.Visible = false; divShowPay.Visible = false; divOfflinePay.Visible = false;
                    objCommon.DisplayUserMessage(updwithapp, "Please Select Semester.", this.Page);
                    ddlReceiptType.Focus();
                }
            }


            else
            {
                btnSubmit.Visible = false; btnCancel.Visible = false; pnlStudentsFees.Visible = false; payoption.Visible = false; divShowPay.Visible = false; divOfflinePay.Visible = false;
                objCommon.DisplayUserMessage(updwithapp, "Please Select Receipt Type.", this.Page);
                ddlReceiptType.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.ddlReceiptType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void CreateDemandForCurrentFeeCriteria()
    {
        try
        {
            FeeDemand feeDemand = new FeeDemand();
            feeDemand.StudentId = Convert.ToInt32(Session["idno"]);
            feeDemand.StudentName = lblStudName.Text;
            feeDemand.EnrollmentNo = lblRegNo.Text;
            feeDemand.SessionNo = Convert.ToInt32(Session["currentsession"].ToString());
            feeDemand.BranchNo = Convert.ToInt32(ViewState["BRANCHNO"]);
            feeDemand.DegreeNo = Convert.ToInt32(lblDegree.ToolTip);
            feeDemand.SemesterNo = ((ddlSemester.SelectedIndex > 0 && ddlSemester.SelectedValue != string.Empty) ? Int32.Parse(ddlSemester.SelectedValue) : 1);
            feeDemand.AdmBatchNo = Convert.ToInt32(ViewState["batchno"]);
            feeDemand.ReceiptTypeCode = "TF";
            feeDemand.PaymentTypeNo = Convert.ToInt32(ViewState["paytypeno"]);
            feeDemand.CounterNo = 1;
            feeDemand.UserNo = ((Session["userno"].ToString() != string.Empty) ? Convert.ToInt32(Session["userno"].ToString()) : 0);
            feeDemand.CollegeCode = ((Session["colcode"].ToString() != string.Empty) ? Session["colcode"].ToString() : string.Empty);
            int paymentTypeNoOld = 1;

            this.CreateDemand(feeDemand, paymentTypeNoOld);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.CreateDemandForCurrentFeeCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReceiptType.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlSemester, "ACD_OTHER_CURRENCEY_FEES OC INNER JOIN ACD_YEAR C ON OC.YEAR = C.YEAR", "DISTINCT OC.YEAR", "YEARNAME", "ADMBATCH = " + Convert.ToInt32(ViewState["batchno"]) + " AND COLLEGE_ID =" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + " AND DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + " AND BRANCHNO=" + Convert.ToInt32(ViewState["BRANCHNO"]) + " AND OC.YEAR=" + Convert.ToInt32(ViewState["YEAR"]) + " AND CUR_NO=" + Convert.ToInt32(ddlReceiptType.SelectedValue), "");
            divsemester.Visible = true;
            ddlSemester.SelectedValue = "0";
            if (ddlSemester.SelectedIndex > 0)
            {

            }
            else
            {
                btnSubmit.Visible = false; btnCancel.Visible = false; pnlStudentsFees.Visible = false;
                ddlSemester.Focus();
            }
        }
    }
    protected void rdPaymentOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdPaymentOption.SelectedValue == "0")
        {
            divUploadChallan.Visible = true;
            divUploadgENChallan.Visible = true;
            BTNPAY.Visible = false;
            btnCancel.Visible = false;
            divShowPay.Visible = false;
            divOfflinePay.Visible = true;
            payoption.Visible = true;
            DIVPAY.Visible = false;
            //ViewState["Amount"] = TotalSum.ToString();
            //ViewState["demandamounttobepaid"] = TotalSum;
        }
        else
        {
            divUploadgENChallan.Visible = false;
            divUploadChallan.Visible = false;
            BTNPAY.Visible = true;
            btnCancel.Visible = false;
            divShowPay.Visible = true;
            divOfflinePay.Visible = false;
            DIVPAY.Visible = true;
            //ViewState["Amount"] = TotalSum.ToString();
            //ViewState["demandamounttobepaid"] = TotalSum;
        }
    }
    protected void fillDetails()
    {
        try
        {
            DataSet ds = null;
            ds = feeController.getPaymentDetails(Convert.ToInt32(Session["userno"]));
            if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
            {
                lvBankDetails.DataSource = ds.Tables[2];
                lvBankDetails.DataBind();
            }
            else
            {
                lvBankDetails.DataSource = null;
                lvBankDetails.DataBind();
            }
            if (ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
            {
                lvDepositSlip.DataSource = ds.Tables[4];
                lvDepositSlip.DataBind();              
            }
            else
            {
                lvDepositSlip.DataSource = null;
                lvDepositSlip.DataBind();               
            }
        }
        catch (Exception ex)
        {

        }
    }
    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }
    public int Blob_UploadDepositSlip(string ConStr, string ContainerName, string DocName, FileUpload FU, byte[] ChallanCopy)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;

        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.Properties.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            if (!cblob.Exists())
            {
                using (Stream stream = new MemoryStream(ChallanCopy))
                {
                    cblob.UploadFromStream(stream);
                }
            }
            //cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }
    protected string GetSession()
    {
        try
        {
            session = objCommon.LookUp("ACD_DEMAND", "(isnull(SESSIONNO,0))", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + " AND PAYTYPENO =" + Convert.ToInt32(ViewState["paytypeno"]) + " AND ADMBATCHNO=" + Convert.ToInt32(ViewState["batchno"]) + "and RECIEPT_CODE='"+ddlReceiptType.SelectedValue+"'");
        }
        catch (Exception Ex)
        {
        }
        return session;
    }
   
    protected void btnPrintReceipt_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnPrint = sender as ImageButton;
            Session["dcrno"] = btnPrint.CommandArgument;
            this.ShowReport("FeeCollectionReceipt.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["idno"]));


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.btnPrintReceipt_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private string GetReportParameters(int dcrNo, int studentNo)
    {
        string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;
    }
    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }
    protected void btnedit_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEdit = sender as ImageButton;

        ViewState["action"] = "Edit";
        int srno = int.Parse(btnEdit.CommandArgument);
        ViewState["srno"] = srno;
        DataSet chkds = objCommon.FillDropDown("ACD_DCR_TEMP", "TEMP_DCR_NO", "Format(REC_DT,'dd/MM/yyyy') AS REC_DATE,*", "TEMP_DCR_NO=" + srno, string.Empty);
        if (chkds.Tables[0].Rows.Count > 0)
        {
            ddlbank.SelectedValue = chkds.Tables[0].Rows[0]["BANK_NO"].ToString();
            txtBranchName.Text = chkds.Tables[0].Rows[0]["BRANCH_NAME"].ToString().Trim();
            txtchallanAmount.Text = chkds.Tables[0].Rows[0]["TOTAL_AMT"].ToString().Trim();
            txtPaymentdate.Text = chkds.Tables[0].Rows[0]["REC_DATE"].ToString().Trim();
            ViewState["ORDER_ID"] = chkds.Tables[0].Rows[0]["ORDER_ID"].ToString().Trim();
            Session["EDITCHALLANCOPYDETAILS"] = string.Empty;//(byte[])chkds.Tables[0].Rows[0]["CHALLAN_COPY"];

            //ddlSection.SelectedValue = chkds.Tables[0].Rows[0]["UGPGOT"].ToString().Trim();
            //rdoSpecilization.SelectedValue = chkds.Tables[0].Rows[0]["ISSPECIALIZATION1"].ToString().Trim(); // ADDED BY SWAPNIL THAKARE ON 09-07-2021 
            //if (chkds.Tables[0].Rows[0]["ACTIVE1"].ToString().Trim() == "1")
            //{
            //    //chkActive.Checked = true;
            //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(true);", true);
            //}
            //else
            //{
            //    //chkActive.Checked = false;
            //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(false);", true);
            //}

            //ddlClassification.SelectedValue = chkds.Tables[0].Rows[0]["AREA_INT_NO1"].ToString();  //ADDED BY SWAPNIL THAKARE ON DATED 28-07-2021
            //ddlCollegeName.Enabled = false;
            //ddlDegreeName.Enabled = false;
        }
    }
    private void ShowReport(string rptName, int dcrNo, int studentNo)
    {
        try
        {
            string url = "https://sims.sliit.lk/reports/commonreport.aspx?";
            //string url = "https://erptest.sliit.lk/reports/commonreport.aspx?";
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=" + this.GetReportParameters(dcrNo, studentNo);
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
    public void SubmitOfflineData(byte[] ChallanCopy)
    {
        try
        {
            string session = string.Empty;
            session = GetSession();

            int result = 0;
            int DM_NO = 0;

            string Ext = Path.GetExtension(FuChallan.FileName);
            DM_NO = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "DM_NO", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO=" + Convert.ToInt32(session) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + "and RECIEPT_CODE='"+ ddlReceiptType.SelectedValue+"'"));
            int Count = Convert.ToInt32(objCommon.LookUp("ACD_DCR_TEMP", "COUNT(*)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND PAY_SERVICE_TYPE=2"));
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {

                    if (DM_NO > 0)
                    {
                        result = feeController.InsertOfflinePayment_DCR_TEMP(DM_NO, Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["SESSIONNO"]), Convert.ToDateTime(txtPaymentdate.Text), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(Session["ERPORDERIDRESPONSE"]), 2, Convert.ToInt32(2), ChallanCopy, Convert.ToInt32(ddlbank.SelectedValue), Convert.ToString(ddlbank.SelectedItem.Text), txtBranchName.Text, txtchallanAmount.Text, Convert.ToInt32(Session["idno"]) + "_ERP_" + Count + "_RemainingDeposit_Slip" + Ext, string.Empty);
                    }
                    else
                    {
                        divOfflinePay.Visible = false; pnlStudentsFees.Visible = false; ddlReceiptType.SelectedValue = "0"; ddlSemester.SelectedValue = "0";
                        objCommon.DisplayUserMessage(this.updwithapp, "Demand not created!", this.Page);
                        return;
                    }
                    if (result > 0)
                    {
                        // email();
                        objCommon.DisplayMessage(this.Page, "Deposit Data Saved Succesfully !!!", this.Page);
                        ViewState["action"] = "add";
                        txtChallanId.Text = "";
                        //txtchallanAmount.Text = "";
                        ddlbank.SelectedValue = null;
                        txtBranchName.Text = "";
                        txtPaymentdate.Text = "";
                        divUploadChallan.Visible = true;
                        fillDetails();
                        GetPreviousReceipt();
                        divOfflinePay.Visible = true; pnlStudentsFees.Visible = false; ddlReceiptType.SelectedValue = "0"; ddlSemester.SelectedValue = "0";
                    }
                    else
                    {
                        divOfflinePay.Visible = false; pnlStudentsFees.Visible = false; ddlReceiptType.SelectedValue = "0"; ddlSemester.SelectedValue = "0";
                        objCommon.DisplayUserMessage(updwithapp, "Failed To Done Offline Payment.", this.Page);
                        return;
                    }
                }


                else if (ViewState["action"].ToString().Equals("Edit")) // ViewState["ORDER_ID"]
                {
                    result = feeController.UpdateOfflinePayment_DCR_TEMP(Convert.ToInt32(ViewState["srno"].ToString()), DM_NO, Convert.ToInt32(Session["idno"].ToString()), Convert.ToInt32(ViewState["SESSIONNO"].ToString()), Convert.ToDateTime(txtPaymentdate.Text), Convert.ToInt32(ViewState["semesterno"].ToString()), Convert.ToString(ViewState["ORDER_ID"].ToString()), Convert.ToInt32(2), Convert.ToInt32(2), ChallanCopy, Convert.ToInt32(ddlbank.SelectedValue), Convert.ToString(ddlbank.SelectedItem.Text), txtBranchName.Text, txtchallanAmount.Text, Convert.ToInt32(Session["idno"]) + "_ERP_" + Count + "_OtherDeposit_Slip" + Ext);
                    if (result > 0)
                    {

                        objCommon.DisplayMessage(this.Page, "Deposit Data Updated Succesfully!", this.Page);
                        divUploadChallan.Visible = true;
                        ViewState["action"] = "add";
                        txtChallanId.Text = "";
                        //txtchallanAmount.Text = "";
                        ddlbank.SelectedValue = null;
                        txtBranchName.Text = "";
                        txtPaymentdate.Text = "";
                        fillDetails();
                        GetPreviousReceipt();
                        divOfflinePay.Visible = true; payoption.Visible = true;  pnlStudentsFees.Visible = false; ddlReceiptType.SelectedValue = "0"; ddlSemester.SelectedValue = "0";
                    }
                    else
                    {
                        divOfflinePay.Visible = true; payoption.Visible = true;  pnlStudentsFees.Visible = false; ddlReceiptType.SelectedValue = "0"; ddlSemester.SelectedValue = "0";
                        objCommon.DisplayUserMessage(updwithapp, "Failed To Update Offline Payment.", this.Page);
                        return;
                    }

                }
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.SubmitData-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void DeleteIFExits(string FileName)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }
    private void CreateCustomerRef()
    {
        Random rnd = new Random();
        int ir = rnd.Next(01, 10000);
        // lblOrderID.Text = Convert.ToString(Convert.ToInt32(Session["idno"]) + Convert.ToInt32(ViewState["SESSIONNO"]) + Convert.ToInt32(ViewState["semesterno"]) + ir);

        lblOrderID.Text = Convert.ToString(Convert.ToInt32(Session["idno"]) + Convert.ToString(ViewState["SESSIONNO"]) + Convert.ToString(ViewState["semesterno"]) + ir);
        txtOrderid.Text = lblOrderID.Text;
        Session["ERPORDERIDRESPONSE"] = lblOrderID.Text;
    }
    protected void btnChallanSubmit_Click(object sender, EventArgs e)
    {
        byte[] ChallanCopy = null;
        try
        {
            CreateCustomerRef();
            string idno = objCommon.LookUp("ACD_DCR", "count(idno)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND YEAR=" + Convert.ToInt32(ddlSemester.SelectedValue) + " and degreeno=" + Convert.ToInt32(lblDegree.ToolTip) + "and branchno=" + Convert.ToInt32(ViewState["BRANCHNO"]) + " and RECIEPT_CODE=" + "'" + Convert.ToString("RF") + "'");
            if (idno != "0")
            {
                objCommon.DisplayMessage(updChallan, "Payment Already Done !!!", this.Page);
                ddlReceiptType.SelectedIndex = 0;
                ddlSemester.SelectedIndex = 0;
                pnlStudentsFees.Visible = false;
                lvStudentFees.DataSource = null;
                lvStudentFees.DataBind();
            }
            else
            {
                if (txtchallanAmount.Text == "0" || txtchallanAmount.Text == "" || txtchallanAmount.Text == "0.00")
                {
                    divOfflinePay.Visible = false; pnlStudentsFees.Visible = false; ddlReceiptType.SelectedValue = "0"; ddlSemester.SelectedValue = "0";
                    objCommon.DisplayMessage(updwithapp, "Total Amount Cannot be Less than or Equal to Zero !!", this.Page);
                    return;
                }
                else
                {
                    if (Convert.ToDecimal(ViewState["demandamounttobepaid"].ToString()) < Convert.ToDecimal(txtchallanAmount.Text))
                    {
                        divOfflinePay.Visible = false; pnlStudentsFees.Visible = false; ddlReceiptType.SelectedValue = "0"; ddlSemester.SelectedValue = "0";
                        objCommon.DisplayMessage(updwithapp, "Paid amount should not be greater than applicable amount of the program !!!", this.Page);
                        return;
                    }
                }
                string session = string.Empty;
                session = GetSession();
                int Count = Convert.ToInt32(objCommon.LookUp("ACD_DCR_TEMP", "COUNT(*)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND PAY_SERVICE_TYPE=2"));

                if (FuChallan.HasFile)
                {
                    if (ViewState["action"].ToString() != "Edit")
                    {
                        ChallanCopy = objCommon.GetImageData(FuChallan);
                        CustomStatus cs = CustomStatus.Others;
                        //cs = (CustomStatus)feeController.InsertChallanCopyDetailserp(Convert.ToInt32(Session["idno"]), txtChallanId.Text, txtchallanAmount.Text, ChallanCopy, txtTransactionNo.Text, txtPaymentdate.Text, Convert.ToString(ddlbank.SelectedItem.Text), Convert.ToString(txtBranchName.Text));

                        int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, Convert.ToInt32(Session["idno"]) + "_ERP_" + Count + "_OtherDeposit_Slip", FuChallan, ChallanCopy);
                        if (retval == 0)
                        {
                            divOfflinePay.Visible = false; pnlStudentsFees.Visible = false; ddlReceiptType.SelectedValue = "0"; ddlSemester.SelectedValue = "0";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                            return;
                        }
                        //Added for Submit data in ACD_DCR_TEMP
                        SubmitOfflineData(ChallanCopy);

                    }
                    else
                    {
                        // objCommon.DisplayMessage(updChallan, "Please Upload Deposit Slip !!!", this.Page);
                        if (ViewState["action"].ToString().Equals("Edit"))
                        {
                            //ChallanCopy = (byte[])Session["EDITCHALLANCOPYDETAILS"];
                            ChallanCopy = objCommon.GetImageData(FuChallan);
                            int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, Convert.ToInt32(Session["idno"]) + "_ERP_" + Count + "_OtherDeposit_Slip", FuChallan, ChallanCopy);
                            if (retval == 0)
                            {
                                divOfflinePay.Visible = false; pnlStudentsFees.Visible = false; ddlReceiptType.SelectedValue = "0"; ddlSemester.SelectedValue = "0";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                return;
                            }
                            ViewState["action"] = "Edit";
                            SubmitOfflineData(ChallanCopy);
                            Session["EDITCHALLANCOPYDETAILS"] = null;
                        }
                    }
                }
                else
                {
                    divOfflinePay.Visible = true;
                    payoption.Visible = true;
                    pnlStudentsFees.Visible = true;
                    //ddlReceiptType.SelectedValue = "0"; ddlSemester.SelectedValue = "0";
                    objCommon.DisplayMessage(updChallan, "Please Upload Deposit Slip !!!", this.Page);
                    return;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void BTNPAY_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtAmount.Text == "0" || txtAmount.Text == "" || txtAmount.Text == "0.00")
            {
                objCommon.DisplayMessage(updwithapp, "Total Amount Cannot be Less than or Equal to Zero !!", this.Page);
            }
            else
            {
                hdfAmount.Value = txtAmount.Text;
                SubmitData();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void GetPreviousReceipt()
    {
        DataSet ds = feeController.GetPaidReceiptsInfoByStudId_FORPAYMENT(Convert.ToInt32(Session["idno"]));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvPaidReceipts.DataSource = ds;
            lvPaidReceipts.DataBind();
            //divacceptance.Attributes.Add("class", "finished");
            //divlkPayment.Attributes.Add("class", "finished");
        }
        else
        {
            lvPaidReceipts.DataSource = null;
            lvPaidReceipts.DataBind();
        }
    }
    protected void lvPaidReceipts_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            switch (e.Item.ItemType.ToString())
            {

            };
        }
    }
    protected void BindInstallment()
    {
        try
        {
            MappingController objmap = new MappingController();
            DataSet ds = null;
            ds = objmap.GetStudentInstallmentDetails(Convert.ToInt32(Session["idno"]));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                //rpInstallment.DataSource = ds.Tables[0];
                //rpInstallment.DataBind();
                ViewState["INSTALLMENT_DETAILS"] = "1";
                //showOnlinePaymentDetails.Visible = false;
                //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //{
                //    LinkButton lnkPay = rpInstallment.Items[i].FindControl("lnkPay") as LinkButton;
                //    if (Convert.ToString(ds.Tables[0].Rows[i]["RECON"]) == "1")
                //    {
                //        lnkPay.Visible = false;
                //    }
                //    else
                //    {
                //        if (Convert.ToString(ds.Tables[0].Rows[i]["DATE_STATUS"]) == "1")
                //        {
                //            lnkPay.Visible = false;
                //        }
                //        else
                //        {
                //            lnkPay.Visible = true;
                //            break;
                //        }
                //    }
                //}
            }
            else
            {
                //showOnlinePaymentDetails.Visible = true;
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void SubmitData()
    {
        try
        {

            CreateCustomerRef();
            string session = string.Empty;
            session = GetSession();

            int result = 0;
            int DM_NO = 0;
            DM_NO = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "DM_NO", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO=" + Convert.ToInt32(session) + " AND ISNULL(CAN,0) = 0 AND ISNULL(DELET,0) = 0 AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + "and RECIEPT_CODE=" + "'" + Convert.ToString(ddlReceiptType.SelectedValue) + "'"));
            if (DM_NO > 0)
            {
                if (ddlReceiptType.SelectedValue == "HF")
                {
                    // result = feeController.InsertOnlinePayment_DCR(DM_NO, Convert.ToInt32(Session["idno"]), Convert.ToInt32(session), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(lblOrderID.Text), 1);//, Convert.ToString(ViewState["Amount"])//, APTRANSACTIONID
                    result = feeController.InsertOnlinePayment_DCR(DM_NO, Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["SESSIONNO"]), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(lblOrderID.Text), 1, Convert.ToInt32(1), Convert.ToString(ViewState["INSTALL_NO"]));
                }
                else
                {
                    // result = feeController.InsertOnlinePayment_DCR(DM_NO, Convert.ToInt32(Session["idno"]), Convert.ToInt32(session), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(lblOrderID.Text), 1);//, Convert.ToString(ViewState["Amount"])//, APTRANSACTIONID
                    result = feeController.InsertOnlinePayment_DCR(DM_NO, Convert.ToInt32(Session["idno"]), Convert.ToInt32(session), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(lblOrderID.Text), 1, Convert.ToInt32(1), Convert.ToString(ViewState["INSTALL_NO"]));
                }

            }
            else
            {
                objCommon.DisplayUserMessage(this.updwithapp, "Demand not created!", this.Page);
                return;
            }
            if (result > 0)
            {
                DataSet ds = null;
                ds = feeController.GetOnlineTrasactionOnlineOrderID(Convert.ToInt32(Session["idno"]), Convert.ToString(lblOrderID.Text));

                if (ds.Tables[0].Rows.Count > 0)
                {

                    if (Convert.ToString(ds.Tables[0].Rows[0]["ORDER_ID"]) == lblOrderID.Text)
                    {
                        if (Convert.ToString(ViewState["INSTALLMENT_DETAILS"]) != "1")
                        {

                            hdfServiceCharge.Value = Convert.ToString(ds.Tables[0].Rows[0]["SERVICE_CHARGE"]);
                            txtOrderid.Text = lblOrderID.Text.ToString();
                            //ViewState["orderid"] = lblOrderID.Text;
                            txtAmountPaid.Text = hdfAmount.Value.ToString();
                            txtServiceCharge.Text = hdfServiceCharge.Value.ToString();
                            decimal text = Convert.ToDecimal(hdfAmount.Value) + Convert.ToDecimal(hdfServiceCharge.Value);
                            txtAmountPaid.Text = text.ToString();
                            txtTotalPayAmount.Text = hdfAmount.Value.ToString();
                            ViewState["FinalAmountPaid"] = text;
                            SendTransaction();
                        }
                        else
                        {
                            
                            decimal perCharge = (Convert.ToDecimal(hdfAmount.Value) * Convert.ToDecimal(ds.Tables[0].Rows[0]["SERVICE_CHARGE_PER"].ToString())) / 100;
                            hdfServiceCharge.Value = Convert.ToString(perCharge);
                            txtOrderid.Text = lblOrderID.Text.ToString();
                            txtAmountPaid.Text = hdfAmount.Value.ToString();
                            txtServiceCharge.Text = hdfServiceCharge.Value.ToString();
                            decimal text = Convert.ToDecimal(hdfAmount.Value) + Convert.ToDecimal(hdfServiceCharge.Value);
                            txtAmountPaid.Text = text.ToString();
                            txtTotalPayAmount.Text = hdfAmount.Value.ToString();
                            ViewState["FinalAmountPaid"] = text.ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#myModal33').modal('show')", true);
                            SendTransaction();
                        }

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
                //lblStatus.Visible = true;
                objCommon.DisplayUserMessage(updwithapp, "Failed To Done Online Payment.", this.Page);
                return;
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.SubmitData-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //private void bindpaymentdata()
    //{
    //    DataSet ds = null;
    //    ds = feeController.GetOnlineTrasactionOnlineOrderID(Convert.ToInt32(Session["idno"]), Convert.ToString(lblOrderID.Text));

    //    decimal perCharge = (Convert.ToDecimal(hdfAmount.Value) * Convert.ToDecimal(ds.Tables[0].Rows[0]["SERVICE_CHARGE_PER"].ToString())) / 100;
    //    hdfServiceCharge.Value = Convert.ToString(perCharge);
    //    txtOrderid.Text = lblOrderID.Text.ToString();
    //    txtAmountPaid.Text = hdfAmount.Value.ToString();
    //    txtServiceCharge.Text = hdfServiceCharge.Value.ToString();
    //    decimal text = Convert.ToDecimal(hdfAmount.Value) + Convert.ToDecimal(hdfServiceCharge.Value);
    //    txtAmountPaid.Text = text.ToString();
    //    txtTotalPayAmount.Text = hdfAmount.Value.ToString();
    //    ViewState["FinalAmountPaid"] = text.ToString();
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#myModal33').modal('show')", true);
    //    //SendTransaction();
    //}
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
                Session["ERPPaymentSession"] = respValues[key];
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
}