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

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
public partial class ACADEMIC_OnlinePaymentAdminLogin : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    User objU = new User();
    FeeCollectionController feeController = new FeeCollectionController();
    int idno = 0; decimal TotalSum = 0; String tspl_txn_id = string.Empty;
    string session = string.Empty;
    IITMS.UAIMS.BusinessLayer.BusinessEntities.DocumentAcad objdocument = new DocumentAcad();
    DocumentControllerAcad objdocContr = new DocumentControllerAcad();
    NewUser objnu = new NewUser();
    NewUserController objnuc = new NewUserController();
    StudentFees objStudentFees = new StudentFees();
    NewUserController ObjNuc = new NewUserController();
    private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string username = string.Empty;
    UserController user = new UserController();
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
    string blob_ContainerNamePhoto = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNamePhoto"].ToString();

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
            if (Session["userno"] == null || Session["username"] == null ||
                  Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null || Session["idno"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Page Authorization
                   // this.CheckPageAuthorization();
                    Session["Employment_History"] = null;
                    Session["Referees"] = null;
                    ViewState["REFEREEES_SRNO"] = null;
                    ViewState["EMPLOYEE_SRNO"] = null;
                 
                    Session["Employment_History_PG"] = null;
                    Session["Referees_PG"] = null;
                    ViewState["REFEREEES_SRNO_PG"] = null;
                    ViewState["EMPLOYEE_SRNO_PG"] = null;
                    // Check User Authority 
                    divlkPayment.Attributes.Remove("class");
                    divacceptance.Attributes.Add("class", "active");
                    PopulateDropDown();
                    Page.Title = Session["coll_name"].ToString();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    //this.CheckPageAuthorization();
                    if (Session["StudentPayDetail"] != null)
                    {
                        Session["StudentPayDetail"] = null;
                    }
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                }
                TRAmount.Visible = false;

            }
         
            // Clear message div
            divMsg.InnerHtml = string.Empty;

            if (Request.Params["__EVENTTARGET"] != null &&
                Request.Params["__EVENTTARGET"].ToString() != string.Empty)
            {
                if (Request.Params["__EVENTTARGET"].ToString() == "SubmitOffer")
                    this.SubmitOffer();
            }
            objCommon.SetLabelData("0");
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // dynamic page header addded by sarang 09/07/2021
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnsearch_Click(object sender, EventArgs e)
    {

        string AplicationId = txtAplicationNo.Text;
        ViewState["idno"] =  objCommon.LookUp("ACD_STUDENT", "DISTINCT IDNO", "REGNO='" + AplicationId.ToString()+"' OR ENROLLNO='"+AplicationId.ToString()+"'");

        if (ViewState["idno"].ToString() == string.Empty)
        {
         
            objCommon.DisplayUserMessage(this.updBulkReg, "Student Not Found !!", this);
            return;
        }
      
        divlkPayment.Attributes.Remove("class");
        divacceptance.Attributes.Add("class", "active");
        ViewState["userno"] = objCommon.LookUp("ACD_STUDENT", "DISTINCT USERNO", "IDNO=" + Convert.ToInt32(ViewState["idno"].ToString()));
       
        if (Convert.ToString(ViewState["action"]) == "")
        {
            ViewState["action"] = "add";
        }
        if (CheckActivity() == false)
        {
            objCommon.DisplayUserMessage(this.updBulkReg, "This Activity has been Stopped. Contact Admin.!!", this);
            return;
        }
        bindlist();
        status();

        if (Session["usertype"].ToString()!="2")     //Student 
        {
            ViewState["MINISTYFLAG"] = null;
            int userno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DISTINCT USERNO", "IDNO=" + Convert.ToInt32(ViewState["idno"])));
            ViewState["USER_NO"] = userno;

            BindOfferAcceptance();
            if (Convert.ToString(ViewState["MINISTYFLAG"]) == "1")
            {
                divlkPayment.Visible = true;
                divlkstatus.Visible = true;
                divpayment.Visible = false;
            }
            else
            {
                divlkPayment.Visible = true;
                divlkstatus.Visible = true;
            }
            fillDetails();
            ShowStudentDetails();
            GetPreviousReceipt();
            objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO>0 AND RCPTTYPENO IN(1)", "RECIEPT_CODE");//RCPTTYPENO
            ddlReceiptType.SelectedIndex = 1;

            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO <=(SELECT SEMESTERNO + 1 FROM ACD_STUDENT WITH (NOLOCK) WHERE IDNO= " + Convert.ToInt32(Session["idno"]) + ") AND SEMESTERNO>0", "SEMESTERNO DESC");
        }
        else
        {
            objCommon.DisplayUserMessage(updBulkReg, "Record Not Found.This Page is use for Online Payment for only AdminLogin Login!!", this.Page);
        }
        ViewState["LASTQUALIFICATION"] = objCommon.LookUp("ACD_USER_LAST_QUALIFICATION", "COUNT(1)", "USERNO=" + Convert.ToInt32(ViewState["USER_NO"]) + "");
        ViewState["UPLOADDOCUMENTCOUNT"] = objCommon.LookUp("ACD_UPLOAD_DOCUMENT", "COUNT(1)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "");
        ViewState["RESULT_STATUS"] = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + " AND SESSIONNO=" + Convert.ToInt32(ViewState["SESSIONNO"]));
        txtPermAddress.Attributes.Add("maxlength", txtPermAddress.MaxLength.ToString());
        DivUpdate.Visible = true;
       
    }



    private bool CheckActivity()
    {

        DataSet ds = feeController.GetSemesterRegistrationActivityStatus(Convert.ToInt32(ViewState["idno"]));
        if (ds.Tables[0].Rows.Count == 0)
        {
            // ulShow.Visible = false;
            return false;
        }
        else
        {
            ViewState["SESSIONNO"] = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
            ViewState["DOWN_PAYMENT_AMOUNT"] = ds.Tables[0].Rows[0]["DOWN_PAYMENT_AMOUNT"].ToString();
            ViewState["EARLY_BIRD_AMOUNT"] = ds.Tables[0].Rows[0]["EARLY_BIRD_AMOUNT"].ToString();
            ViewState["REGULAR_PAYMENT_AMOUNT"] = ds.Tables[0].Rows[0]["REGULAR_PAYMENT_AMOUNT"].ToString();
            return true;
        }
    }
    private void CheckPageAuthorization()
    {
        // Request.QueryString["pageno"] = "ACADEMIC/EXAMINATION/ExamRegistration_New.aspx ?pageno=1801";
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=OnlinePayment_StudentLogin.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OnlinePayment_StudentLogin.aspx");
        }
    }

    private void BindOfferAcceptance()
    {
        try
        {
            DataSet Program = objdocContr.getOfferAcceptanceDetails(Convert.ToInt32(ViewState["USER_NO"].ToString()), Convert.ToInt32(3), Convert.ToInt32(0));
            //objCommon.FillDropDown("ACD_USER_BRANCH_PREF B INNER JOIN ACD_COLLEGE_MASTER C ON B.COLLEGE_ID = C.COLLEGE_ID INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO INNER JOIN ACD_BRANCH E ON B.BRANCHNO = E.BRANCHNO INNER JOIN ACD_AREA_OF_INTEREST F ON B.AREA_INT_NO = F.AREA_INT_NO INNER JOIN ACD_CAMPUS AC ON (B.CAMPUSNO=AC.CAMPUSNO) LEFT JOIN ACD_AFFILIATED_UNIVERSITY AU ON E.AFFILIATED_NO = AU.AFFILIATED_NO INNER JOIN ACD_USER_REGISTRATION UR ON(B.USERNO=UR.USERNO) LEFT JOIN ACD_DEMAND AD ON(AD.ENROLLNMENTNO=UR.USERNAME AND AD.DEGREENO=B.DEGREENO AND AD.BRANCHNO=B.BRANCHNO AND AD.COLLEGE_ID=B.COLLEGE_ID AND ISNULL(AD.CAN,0)=0 AND ISNULL(AD.DELET,0)=0) LEFT JOIN ACD_DCR_TEMP UCD ON UR.USERNAME = UCD.ENROLLNMENTNO LEFT JOIN ACD_DCR DDA ON (DDA.IDNO = AD.IDNO AND AD.SESSIONNO = DDA.SESSIONNO AND DDA.RECON = 1 AND AD.DEGREENO = DDA.DEGREENO)", "DISTINCT CONVERT(NVARCHAR(10),B.COLLEGE_ID) +','+ CONVERT(NVARCHAR(10),B.DEGREENO) + ',' + CONVERT(NVARCHAR(10),B.BRANCHNO) + ',' + CONVERT(NVARCHAR(10),B.AREA_INT_NO)", "C.CODE + ' - ' + D.DEGREENAME + ' - ' + LONGNAME + ' - ' + F.SHORTNAME,B.USERNO,COLLEGE_NAME, (D.DEGREENAME + ' - ' + LONGNAME) AS PROGRAM_NAME,AREA_INT_NAME,AC.CAMPUSNAME,AFFILIATED_SHORTNAME,B.COLLEGE_ID,B.DEGREENO,B.BRANCHNO,B.AREA_INT_NO,AC.CAMPUSNO,ISNULL(AU.AFFILIATED_NO,0)AS AFFILIATED_NO ,ISNULL(AD.DM_NO,0)AS DM_NO,FORMAT (DEMAND_DATE, 'dd/MM/yyyy') AS DEMAND_DATE,UCD.TEMP_DCR_NO,DDA.RECON", "B.MERITNO IS NOT NULL AND MERITNO != '' AND B.USERNO=" + (ViewState["USER_NO"].ToString()), "");
            if (Program.Tables[0].Rows.Count > 0)
            {
                lstProgramName.DataSource = Program.Tables[0];
                lstProgramName.DataBind();
                ViewState["MINISTYFLAG"] = Convert.ToString(Program.Tables[0].Rows[0]["MINISTRY"]);
                objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lstProgramName);//Set label 
            }
            else
            {
                lstProgramName.DataSource = null;
                lstProgramName.DataBind();
                ViewState["MINISTYFLAG"] = null;
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void ShowStudentDetails()
    {
        try
        {
                idno = Convert.ToInt32(ViewState["idno"]);
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
                    lblStudNam.Text = fullName;
                    // lblStudLastName.Text = lasName;

                    lblSex.Text = dr["SEX"].ToString() == string.Empty ? string.Empty : dr["SEX"].ToString();
                    //lblRegNo.Text = dr["REGNO"].ToString();
                    // lblDateOfAdm.Text = ((dr["ADMDATE"].ToString().Trim() != string.Empty) ? Convert.ToDateTime(dr["ADMDATE"].ToString()).ToShortDateString() : dr["ADMDATE"].ToString());
                    lblRegN.Text = dr["REGNO"].ToString() == string.Empty ? string.Empty : dr["REGNO"].ToString();
                    lblPaymentTy.Text = dr["PAYTYPENAME"].ToString() == string.Empty ? string.Empty : dr["PAYTYPENAME"].ToString();
                    //lblDegree.Text = dr["DEGREENAME"].ToString() == string.Empty ? string.Empty : dr["DEGREENAME"].ToString();
                    //lblBranchs.Text = dr["BRANCH_NAME"].ToString() == string.Empty ? string.Empty : dr["BRANCH_NAME"].ToString();
                    lblYea.Text = dr["YEARNAME"].ToString() == string.Empty ? string.Empty : dr["YEARNAME"].ToString();
                    lblSemeste.Text = dr["SEMESTERNAME"].ToString() == string.Empty ? string.Empty : dr["SEMESTERNAME"].ToString();
                    lblBatc.Text = dr["BATCHNAME"].ToString() == string.Empty ? string.Empty : dr["BATCHNAME"].ToString();
                    lblMobileN.Text = dr["STUDENTMOBILE"].ToString() == string.Empty ? string.Empty : dr["STUDENTMOBILE"].ToString();
                    lblEmailID.Text = dr["EMAILID"].ToString() == string.Empty ? string.Empty : dr["EMAILID"].ToString();
                    //lblBranchs.ToolTip = dr["BRANCHNO"].ToString();
                    //lblDegree.ToolTip = dr["DEGREENO"].ToString();
                    ViewState["degreeno"] = dr["DEGREENO"].ToString();
                    ViewState["BRANCHNO"] = dr["BRANCHNO"].ToString();
                    ViewState["batchno"] = Convert.ToInt32(dr["ADMBATCH"].ToString());
                    ViewState["semesterno"] = Convert.ToInt32(dr["SEMESTERNO"].ToString());
                    ViewState["paytypeno"] = Convert.ToInt32(dr["PTYPE"].ToString());
                    ViewState["RECIEPT_CODE"] = "TF";
                    //ViewState["SESSIONNO"] = dr["SESSIONNO"].ToString() == string.Empty ? string.Empty : dr["SESSIONNO"].ToString();
                    //lblCollege.Text = dr["COLLEGENAME"].ToString() == string.Empty ? string.Empty : dr["COLLEGENAME"].ToString();
                    //hdnCollege.Value = dr["COLLEGE_ID"].ToString();
                    ViewState["COLLEGE_ID"] = dr["COLLEGE_ID"].ToString();
                    ViewState["RECON"] = dr["RECON"].ToString();
                    ViewState["NICPASS"] = dr["NICPASS"].ToString();
                    ViewState["UGPGOTONLINE"] = dr["UGPGOT"].ToString();
                    ///ViewState["HOSTEL_SESSIONNO"] = objCommon.LookUp("ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "FLOCK = 1");
                    lblnicpass.Text = dr["NICPASS"].ToString() == string.Empty ? string.Empty : dr["NICPASS"].ToString();
                    lblcontactn.Text = dr["STUDENTMOBILE"].ToString() == string.Empty ? string.Empty : dr["STUDENTMOBILE"].ToString();
                    lblstudemail.Text = dr["EMAILID"].ToString() == string.Empty ? string.Empty : dr["EMAILID"].ToString();
                    lbladdres.Text = dr["PADDRESS"].ToString() == string.Empty ? string.Empty : dr["PADDRESS"].ToString();
                    lblprograms.Text = dr["PROGRAM"].ToString() == string.Empty ? string.Empty : dr["PROGRAM"].ToString();
                    lblcampus.Text = dr["CAMPUSNAME"].ToString() == string.Empty ? string.Empty : dr["CAMPUSNAME"].ToString();
                    lblweekbatch.Text = dr["WEEKDAYSNAME"].ToString() == string.Empty ? string.Empty : dr["WEEKDAYSNAME"].ToString();
                    lbldateofreg.Text = dr["APPROVED_DATE"].ToString() == string.Empty ? string.Empty : dr["APPROVED_DATE"].ToString();
                    //lblorigp.Text = dr["PADDRESS"].ToString() == string.Empty ? string.Empty : dr["PADDRESS"].ToString();
                    //lblsliitemail.Text = dr["PADDRESS"].ToString() == string.Empty ? string.Empty : dr["PADDRESS"].ToString();


                    /// Add By Roshan Pannase 17-02-2022
                    lblIntake.Text = dr["BATCHNAME"].ToString() == string.Empty ? string.Empty : dr["BATCHNAME"].ToString();
                    lblEnrollmentn.Text = dr["REGNO"].ToString() == string.Empty ? string.Empty : dr["REGNO"].ToString();
                    lblNamewithInitial.Text = dr["NAME_INITIAL"].ToString() == string.Empty ? string.Empty : dr["NAME_INITIAL"].ToString();
                    lblFullName.Text = dr["STUDNAME"].ToString() == string.Empty ? string.Empty : dr["STUDNAME"].ToString();


                    string payment = objCommon.LookUp("ACD_DCR", "(isnull(SESSIONNO,0))", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + " AND RECIEPT_CODE=" + "'" + Convert.ToString("TF") + "'");
                    if (payment != "")
                    {
                        btnShowDetails.Enabled = false;
                    }
                    else
                    {
                        btnShowDetails.Enabled = true;
                    }


                    if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString()) > 0)
                    {
                        lvOfferAccept.DataSource = dsStudent;
                        lvOfferAccept.DataBind();
                        Session["Offer_Accept"] = "1";
                        lvOfferAccept.Visible = true;
                    }
                    else
                    {
                        lvOfferAccept.DataSource = null;
                        lvOfferAccept.DataBind();
                        Session["Offer_Accept"] = null;
                        lvOfferAccept.Visible = false;
                    }
                    
                }
                else
                {
                    objCommon.DisplayUserMessage(updBulkReg, "Record Not Found.", this.Page);
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
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
        {
            switch (e.Item.ItemType.ToString())
            {

            };
        }
    }

 

    #region After Selection

    protected void lvStudentFees_PreRender(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            ds = feeController.GetFeeItems_Data_ForOnlinePayment(Convert.ToInt32(ViewState["SESSIONNO"]), Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(ddlReceiptType.SelectedValue), Convert.ToInt32(ViewState["paytypeno"]), Convert.ToInt32(ViewState["batchno"]));//, ref status);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                btnSubmit.Enabled = true;
                btnCancel.Visible = true; pnlStudentsFees.Visible = true;
                //TRSPayOption.Visible = true; 
                TRNote.Visible = true;
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
        catch (Exception ex)
        { 
        
        }
    }

    protected void lvStudentFees_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Label lblAmount = e.Item.FindControl("lblAmount") as Label;
            TotalSum += Convert.ToDecimal(lblAmount.Text);
            ViewState["demandamounttobepaid"] = TotalSum;
            txtchallanAmount.Text = TotalSum.ToString();
            string FormattedPrice = TotalSum.ToString("N");
            lblFinalRegistrationFees.Text = FormattedPrice.ToString();
            lblOnlineRegFees.Text = FormattedPrice.ToString();
            txtDepositAmount.Text = TotalSum.ToString();
        }
    }

    #endregion

    #region Regarding Submition of data and Online Payment

    protected void btnSubmit_Click(object sender, EventArgs e)   ////Student Payment Click Button
    {
        try
        {
            CreateCustomerRef();
            if (txtAmount.Text == "0" || txtAmount.Text == "" || txtAmount.Text == "0.00")
            {
                objCommon.DisplayMessage(updBulkReg, "Total Amount Cannot be Less than or Equal to Zero !!", this.Page);
            }
            else
            {
                hdfAmount.Value = txtAmount.Text;
                //SubmitData();
                string session = string.Empty;
                session = GetSession();
                int result = 0; int DcrTempNo = 0;
                int DM_NO = 0;

                DcrTempNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR_TEMP", "COUNT(1)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND PAY_SERVICE_TYPE = 5 AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + "and RECIEPT_CODE=" + "'" + Convert.ToString(ViewState["RECIEPT_CODE"]) + "'"));
                if (DcrTempNo == 0)
                {
                    CreateDemand();

                    DM_NO = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "DM_NO", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND ISNULL(CAN,0)=0 AND ISNULL(DELET,0)=0 AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + "and RECIEPT_CODE=" + "'" + Convert.ToString(ViewState["RECIEPT_CODE"]) + "'"));

                    if (DM_NO > 0)
                    {
                        result = feeController.InsertOfflinePayment_DCR_TEMP(DM_NO, Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["SESSIONNO"]), Convert.ToDateTime(txtPaymentdate.Text), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(lblOrderID.Text), 2, Convert.ToInt32(5), null, Convert.ToInt32(0), Convert.ToString(""), "", hdfAmount.Value, "", Convert.ToString(ViewState["INSTALL_NO"]));
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(this.updBulkReg, "Demand not created!", this.Page);
                        return;
                    }
                    if (result > 0)
                    {
                        //Email();
                        objCommon.DisplayMessage(this.Page, "Record Saved Succesfully !!!", this.Page);
                        GetPreviousReceipt();
                        divViewpayment.Visible = false;
                        pnlStudentsFees.Visible = true;
                        divViewpayment.Visible = true;
                    }
                    else
                    {
                        lblStatus.Visible = true;
                        objCommon.DisplayUserMessage(updBulkReg, "Failed To Done Cash Payment.", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayUserMessage(updBulkReg, "Record Already Exists !!!", this.Page);
                    return;
                }
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
                objCommon.DisplayUserMessage(this.updBulkReg, "Demand not created!", this.Page);
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
                            txtOrderid.Text = lblOrderID.Text;
                            txtAmountPaid.Text = hdfAmount.Value;

                            txtServiceCharge.Text = hdfServiceCharge.Value;
                            decimal text = Convert.ToDecimal(hdfAmount.Value) + Convert.ToDecimal(hdfServiceCharge.Value);
                            txtAmountPaid.Text = text.ToString();
                            txtTotalPayAmount.Text = hdfAmount.Value;
                            ViewState["FinalAmountPaid"] = text;
                        }
                        else
                        {

                            decimal perCharge = (Convert.ToDecimal(hdfAmount.Value) * Convert.ToDecimal(ds.Tables[0].Rows[0]["SERVICE_CHARGE_PER"].ToString())) / 100;

                            hdfServiceCharge.Value = Convert.ToString(perCharge.ToString("N2"));
                            txtOrderid.Text = lblOrderID.Text;
                            txtAmountPaid.Text = hdfAmount.Value;

                            txtServiceCharge.Text = hdfServiceCharge.Value;
                            decimal text = Convert.ToDecimal(hdfAmount.Value) + Convert.ToDecimal(hdfServiceCharge.Value);
                            txtAmountPaid.Text = text.ToString();
                            txtTotalPayAmount.Text = hdfAmount.Value;
                            ViewState["FinalAmountPaid"] = text;
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
                lblStatus.Visible = true;
                objCommon.DisplayUserMessage(updBulkReg, "Failed To Done Online Payment.", this.Page);
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


    #endregion

    #region Report after Payment

    //for report
    private void ShowReport(string rptName, int dcrNo, int studentNo, string copyNo)
    {
        try
        {
            btnReport.Visible = false;
            //ddlSemester_SelectedIndexChanged(new object(), new EventArgs());
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
           // string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";

            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
            url += "pagetitle=FeeCollectionReceipt_StudentLogin";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=" + this.GetReportParameters(dcrNo, studentNo, copyNo);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + rptName + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetReportParameters(int dcrNo, int studentNo, string copyNo)
    {
        string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ////SubmitData();//Session["currentsession"]

        int DCR_NO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "MAX(DCR_NO)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND SESSIONNO=" + Convert.ToInt32(ViewState["SESSIONNO"]) + " AND  SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + "AND RECIEPT_CODE='TF'"));

        if (ViewState["COLLEGE_ID"] == "11" || ViewState["COLLEGE_ID"] == "12" || ViewState["COLLEGE_ID"] == "13")
        {
            this.ShowReport("FeeCollectionReceipt-SVIM.rpt", DCR_NO, Convert.ToInt32(Session["idno"]), "1");
        }
        else if (ViewState["COLLEGE_ID"] == "10")
        {
            this.ShowReport("FeeCollectionReceipt-SVITS.rpt", DCR_NO, Convert.ToInt32(Session["idno"]), "1");
        }
        else
        {
            this.ShowReport("FeeCollectionReceipt.rpt", DCR_NO, Convert.ToInt32(Session["idno"]), "1");
        }
    }

    protected void btnPrintReceipt_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnPrint = sender as ImageButton;

        int DCR_NO = Convert.ToInt32(btnPrint.CommandArgument.ToString());

        string recipt_code = Convert.ToString(objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "DCR_NO = " + DCR_NO + ""));

        this.ShowReport("FeeCollectionReceipt.rpt", DCR_NO, Convert.ToInt32(Session["idno"]), "1");

    }



    #endregion

    #region Other Events

    protected string GetSession()
    {
        try
        {
            if (ddlReceiptType.SelectedValue == "HF")
            {
                session = ViewState["HOSTEL_SESSIONNO"].ToString();
            }
            else
            {
                session = objCommon.LookUp("ACD_DEMAND", "(isnull(SESSIONNO,0))", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND ISNULL(CAN,0) = 0 AND ISNULL(DELET,0) = 0 AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + " AND PAYTYPENO =" + Convert.ToInt32(ViewState["paytypeno"]) + " AND ADMBATCHNO=" + Convert.ToInt32(ViewState["batchno"]) + "and RECIEPT_CODE=" + "'" + Convert.ToString(ddlReceiptType.SelectedValue) + "'");
            }

        }
        catch (Exception Ex)
        {
        }
        return session;
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



    private void Clear()
    {
        //ViewState["Amount"] = null;
        //TotalSum = 0;
        btnSubmit.Visible = false; btnCancel.Visible = false; pnlStudentsFees.Visible = false; TRAmount.Visible = false;
        //TRSPayOption.Visible = false; 
        lblStatus.Visible = false;
        ddlReceiptType.SelectedIndex = 0;
        //rdbPayOption.SelectedIndex = -1; 
        btnReport.Visible = false; TRNote.Visible = false;
        lvStudentFees.DataSource = null;
        lvStudentFees.DataBind();

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion

    protected void btnShowDetails_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            int count = 0;
            try
            {

                foreach (ListViewDataItem items in lstProgramName.Items)
                {
                    CheckBox chk = items.FindControl("chkRowsProgram") as CheckBox;
                    HiddenField hdfbranchno = items.FindControl("hdfbranchno") as HiddenField;
                    Label lbldegree = items.FindControl("lbldegree") as Label;
                    Label lblcollegename = items.FindControl("lblcollegename") as Label;
                    Label lblarea = items.FindControl("lblarea") as Label;
                    
                    if (chk.Checked == true)
                    {
                        count++;
                        if (ddlReceiptType.SelectedIndex > 0)
                        {
                            ViewState["COLLEGE_ID"] = lblcollegename.ToolTip;
                            ViewState["DEGREENO"] = lbldegree.ToolTip;
                            ViewState["BRANCHNO"] = hdfbranchno.Value;
                            ViewState["INTERESTNO"] = lblarea.ToolTip;
                            ViewState["CAMPUSNO"] = 1;

                            this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'>";
                            //this.divMsg.InnerHtml += " if(confirm('No demand found for semester " + lblSemester.Text + ".\\nDo you want to create demand for this semester?'))";

                            this.divMsg.InnerHtml += " if(confirm('Do you want to confirm?'))";
                            this.divMsg.InnerHtml += "{__doPostBack('SubmitOffer', '');}</script>";

                            
                        }
                        else
                        {
                            btnSubmit.Visible = false; btnCancel.Visible = false; pnlStudentsFees.Visible = false; TRAmount.Visible = false;
                            //TRSPayOption.Visible = false; 
                            TRNote.Visible = false;
                            objCommon.DisplayUserMessage(updBulkReg, "Please Select Receipt Type.", this.Page);
                            ddlReceiptType.Focus();
                            lblStatus.Visible = false;
                        }
                    }
                    else
                    {
                        // objCommon.DisplayUserMessage(updBulkReg, "Please Select Aleast One Program ! ", this.Page);
                    }

                }
                if (count == 0)
                {
                    objCommon.DisplayUserMessage(updBulkReg, "Please Select Aleast One Program ! ", this.Page);
                }

            }
            catch (Exception ex)
            {

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

    private void SubmitOffer()
    {
        try
        {
            FeeDemand feeDemand = new FeeDemand();
            feeDemand.StudentId = Convert.ToInt32(Session["idno"]);
            feeDemand.StudentName = lblStudNam.Text;
            feeDemand.EnrollmentNo = lblRegN.Text;

            feeDemand.SessionNo = Convert.ToInt32(ViewState["SESSIONNO"].ToString());

            feeDemand.College_ID = Convert.ToInt32(ViewState["COLLEGE_ID"].ToString());
            feeDemand.DegreeNo = Convert.ToInt32(ViewState["DEGREENO"].ToString());
            feeDemand.BranchNo = Convert.ToInt32(ViewState["BRANCHNO"].ToString());
            feeDemand.Interest = Convert.ToInt32(ViewState["INTERESTNO"].ToString());


            feeDemand.SemesterNo = ((Convert.ToInt32(ViewState["semesterno"]) > 0 && Convert.ToString(ViewState["semesterno"]) != string.Empty) ? Convert.ToInt32(ViewState["semesterno"]) : 1);
            feeDemand.AdmBatchNo = Convert.ToInt32(ViewState["batchno"]);
            feeDemand.ReceiptTypeCode = ddlReceiptType.SelectedValue;
            feeDemand.PaymentTypeNo = Convert.ToInt32(ViewState["paytypeno"]);
            feeDemand.CounterNo = 1;
            feeDemand.UserNo = ((Session["userno"].ToString() != string.Empty) ? Convert.ToInt32(Session["userno"].ToString()) : 0);
            feeDemand.CollegeCode = ((Session["colcode"].ToString() != string.Empty) ? Session["colcode"].ToString() : string.Empty);
            int paymentTypeNoOld = 1;
           
            ViewState["CAMPUSNO"] = 1;
            if (feeController.CreateOnlineAdmissionNewDemand(feeDemand, paymentTypeNoOld, Convert.ToInt32(1), Convert.ToInt32(1)))
            {
                btnSubmit.Visible = true;
                objCommon.DisplayMessage(this.Page, "Offer Acceptance done successfully", this.Page);
                Session["Offer_Accept"] = "1";
                CheckActivity();
                lnkOnlineDetails_Click(new object(), new EventArgs());
            }
            else
            {
                objCommon.DisplayUserMessage(updBulkReg, "Standard Fees Not Created", this.Page);
            }
        }
        catch (Exception ex)
        {
            CheckActivity();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.btnCreateDemand_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lkpayment_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ViewState["UPLOADDOCUMENTCOUNT"]) == 0)
        {
            objCommon.DisplayMessage(this.Page, "Please Complete Document Upload First !!!", this.Page);
            return;
        }
        if (Convert.ToString(Session["STUDENTPHOTO"]) != "")
        {
            //StudentPhoto = (byte[])Session["STUDENTPHOTO"];
            //objCommon.DisplayMessage(this.Page, "Please select at least one document for upload !!!", this.Page);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Upload student photo document is mandatory field !!!", this.Page);
            return;
        }
        //if (Convert.ToString(ViewState["RESULT_STATUS"]) == "0" || Convert.ToString(ViewState["RESULT_STATUS"]) == string.Empty)
        //{
        //    objCommon.DisplayMessage(updBulkReg, "Please Complete Step 4 First !!!", this.Page);
        //    lkModuleRegistration_Click(new object(), new EventArgs());
        //    return;
        //}
        if (Convert.ToInt32(ViewState["LASTQUALIFICATION"]) > 0)
        {
            if(Convert.ToString(ViewState["DOWN_PAYMENT_AMOUNT"]) == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Enrollment Down Payment Amount Not Define !!!", this.Page);
                return;
            }
            else if (Convert.ToString(ViewState["EARLY_BIRD_AMOUNT"]) == string.Empty || Convert.ToString(ViewState["REGULAR_PAYMENT_AMOUNT"]) == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Admission Proper Fee Not Define !!!", this.Page);
                return;
            }

            divlkPayment.Attributes.Remove("class");
            divlkPayment.Attributes.Add("class", "active");
            divlkuploaddocument.Attributes.Remove("class");
            divlkstatus.Attributes.Remove("class");
            divacceptance.Attributes.Remove("class");
            divOnlineDetails.Attributes.Remove("class");
            divlkModuleOffer.Attributes.Remove("class");
            divModuleRegistration.Visible = false;
            divuploaddoc.Visible = false;
            dipayment.Visible = false;
            divstatus.Visible = false;
            acceptance.Visible = false;
            document.Visible = false;
            divlbsta.Visible = false;
            divpayment.Visible = false;
            diApplicantDetails.Visible = false;
            if (Convert.ToString(Session["Offer_Accept"]) == "1")
            {
                //divacceptance.Attributes.Add("class", "finished");
                if (Convert.ToString(ViewState["MINISTYFLAG"]) == "1")
                {
                    dipayment.Visible = true;
                    divpayment.Visible = false;
                    divViewpayment.Visible = false;
                    divMinority.Visible = true;
                }
                else
                {
                    dipayment.Visible = true;
                    divpayment.Visible = true;
                    divViewpayment.Visible = false;
                    divMinority.Visible = false;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please Complete Offer Acceptance First !!!", this.Page);
                return;
            }

            if (Convert.ToString(ViewState["RECON"]) == "1")
            {
                showpay.Visible = true;
                //lblpaid.Text = "Payment Already Done";
                GetPreviousReceipt();
                showunpay.Visible = true;
            }
            else
            {
                showunpay.Visible = true;
            }

        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Please Enter Applicant Qualification Details !!!", this.Page);
        }
        //GetPreviousReceipt();
    }


    //tab 2

    private void status()
    {
        DataSet status = objCommon.FillDropDown("ACD_STUDENT", "DISTINCT CASE WHEN ISNULL(CAN,0)=0 THEN 0 ELSE 1 END AS CAN", "ADMCAN", "IDNO='" + Session["idno"] + "'", "");

        if (status.Tables[0].Rows.Count > 0)
        {
            if (status.Tables[0].Rows[0]["CAN"].ToString() == "0" && status.Tables[0].Rows[0]["ADMCAN"].ToString() == "0")
            {
                lblstatuspayment.Text = "Approved";
                btnSummarySheet.Visible = true;
                //divlkstatus.Attributes.Add("class", "finished");
                ShowStudentDetails();
                btnShowDetails.Visible = false;
                btnPersonalSubmit.Visible = false;
                btnSub.Visible = false;

            }
            if (status.Tables[0].Rows[0]["CAN"].ToString() == "0" && status.Tables[0].Rows[0]["ADMCAN"].ToString() == "1")
            {
                lblstatuspayment.Text = "Enrollment in Progress";
                btnSummarySheet.Visible = false;
                //divlkstatus.Attributes.Add("class", "finished");
            }
            if (status.Tables[0].Rows[0]["CAN"].ToString() == "1" && status.Tables[0].Rows[0]["ADMCAN"].ToString() == "0")
            {
                lblstatuspayment.Text = "Enrollment in Progress";
                btnSummarySheet.Visible = false;
                //divlkstatus.Attributes.Add("class", "finished");
            }
            //if (status.Tables[0].Rows[0]["STATUS"].ToString() == "2")
            //{
            //    lblstatuspayment.Text = "Reject";
            //}
            if (status.Tables[0].Rows[0]["CAN"].ToString() == "1" && status.Tables[0].Rows[0]["ADMCAN"].ToString() == "1")
            {
                lblstatuspayment.Text = "Pending";
                btnSummarySheet.Visible = false;
            }
        }
    }
    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlbank, "ACD_BANK", "DISTINCT BANKNO", "BANKNAME", "BANKNO > 0", "BANKNO");
            objCommon.FillDropDownList(ddlProgramTypes, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "", "");
            objCommon.FillDropDownList(ddlOnlineMobileCode, "ACD_COUNTRY", "DISTINCT COUNTRYNO", "(MOBILE_CODE + ' - ' + COUNTRYNAME)COUNTRYNAME", "COUNTRYNO > 0", "COUNTRYNO");
            objCommon.FillDropDownList(ddlPCon, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO>0", "COUNTRYNAME");
            objCommon.FillDropDownList(ddlHomeMobileCode, "ACD_COUNTRY", "DISTINCT COUNTRYNO", "(MOBILE_CODE + ' - ' + COUNTRYNAME)COUNTRYNAME", "COUNTRYNO > 0", "COUNTRYNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "CourseWise_Registration.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    public void bindlist()
    {
        try
        {
            int USERNO1 = Convert.ToInt32(ViewState["userno"].ToString());// ((UserDetails)(Session["user"])).UserNo;
            DataSet ds = new DataSet();
           
            ds = objdocContr.GetDoclistStud(USERNO1);



            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvDocument.DataSource = ds;
                lvDocument.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvDocument);//Set label 
                lvDocument.Visible = true;
                //divlkuploaddocument.Attributes.Add("class", "finished");
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (ListViewDataItem item in lvDocument.Items)
                    {
                        LinkButton lnk = item.FindControl("lnkViewDoc") as LinkButton;
                        Label fileformat = item.FindControl("lblImageFile") as Label;
                        Label fileformate = item.FindControl("lblFileFormat") as Label;
                        Label uploded = item.FindControl("lbluploadpic") as Label;
                        Label uploadDate = item.FindControl("lblUploadDate") as Label;
                        Label lblVerifyDocument = item.FindControl("lblVerifyDocument") as Label;
                        Label lblRemark = item.FindControl("lblRemark") as Label;
                        int value = int.Parse(lnk.CommandArgument);
                        lnk.CommandName = ds.Tables[1].Rows[0]["PHOTO"].ToString();
                        //fileformat.Text = "Only formats are allowed : png,jpg,jpeg";
                        if (value == 0)
                        {
                            if (Convert.ToString(ds.Tables[1].Rows[0]["DOC_STATUS"]) == "1")
                            {
                                lblVerifyDocument.Text = "Verified";
                                lblRemark.Text = Convert.ToString(ds.Tables[1].Rows[0]["DOC_REMARK"]);
                                lblVerifyDocument.Style.Add("color", "green");
                            }
                            else if (Convert.ToString(ds.Tables[1].Rows[0]["DOC_STATUS"]) == "2")
                            {
                                lblVerifyDocument.Text = "Not Verified";
                                lblVerifyDocument.Style.Add("color", "red");
                                lblRemark.Text = Convert.ToString(ds.Tables[1].Rows[0]["DOC_REMARK"]);
                            }
                            else if (Convert.ToString(ds.Tables[1].Rows[0]["DOC_STATUS"]) == "3")
                            {
                                lblVerifyDocument.Text = "Pending";
                                lblVerifyDocument.Style.Add("color", "red");
                                lblRemark.Text = Convert.ToString(ds.Tables[1].Rows[0]["DOC_REMARK"]);
                            }
                            else if (Convert.ToString(ds.Tables[1].Rows[0]["DOC_STATUS"]) == "4")
                            {
                                lblVerifyDocument.Text = "Incomplete";
                                lblVerifyDocument.Style.Add("color", "red");
                                lblRemark.Text = Convert.ToString(ds.Tables[1].Rows[0]["DOC_REMARK"]);
                            }
                            uploded.Text = "";
                            uploded.Visible = true;
                            if (ds.Tables[1].Rows[0]["PHOTO"].ToString() == string.Empty)
                            {
                                uploded.Text = "NO";
                                lblVerifyDocument.Text = "";
                                lblRemark.Text = "";
                            }
                            else
                            {
                                uploded.Text = "YES";
                                lnk.Visible = true;
                                uploadDate.Text = Convert.ToString(ds.Tables[1].Rows[0]["UPLOAD_DATE"]);
                                Session["STUDENTPHOTO"] = "0";
                                break;
                            }
                        }

                        else
                        {
                            uploded.Text = "NO";
                        }
                    }
                }
                if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    foreach (ListViewDataItem item in lvDocument.Items)
                    {
                        for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                        {
                            LinkButton lnk = item.FindControl("lnkViewDoc") as LinkButton;
                            Label fileformat = item.FindControl("lblFileFormat") as Label;
                            Label uploded = item.FindControl("lbluploadpdf") as Label;
                            Label uploadDate = item.FindControl("lblUploadDate") as Label;
                            Label lblVerifyDocument = item.FindControl("lblVerifyDocument") as Label;
                            Label lblRemark = item.FindControl("lblRemark") as Label;
                            int value = int.Parse(lnk.CommandArgument);
                            
                            if (value >= 1)
                            {
                                lnk.CommandName = ds.Tables[2].Rows[i]["DOC_FILENAME"].ToString();
                                if (Convert.ToString(ds.Tables[2].Rows[i]["DOC_STATUS"]) == "1" && Convert.ToString(ds.Tables[2].Rows[i]["DOCNO"]) == Convert.ToString(value))
                                {
                                    lblVerifyDocument.Text = "Verified";
                                    lblRemark.Text = Convert.ToString(ds.Tables[2].Rows[i]["DOC_REMARK"]);
                                    lblVerifyDocument.Style.Add("color", "green");
                                }
                                else if (Convert.ToString(ds.Tables[2].Rows[i]["DOC_STATUS"]) == "2" && Convert.ToString(ds.Tables[2].Rows[i]["DOCNO"]) == Convert.ToString(value))
                                {
                                    lblVerifyDocument.Text = "Not Verified";
                                    lblRemark.Text = Convert.ToString(ds.Tables[2].Rows[i]["DOC_REMARK"]);
                                    lblVerifyDocument.Style.Add("color", "red");
                                }
                                else if (Convert.ToString(ds.Tables[2].Rows[i]["DOC_STATUS"]) == "3" && Convert.ToString(ds.Tables[2].Rows[i]["DOCNO"]) == Convert.ToString(value))
                                {
                                    lblVerifyDocument.Text = "Pending";
                                    lblRemark.Text = Convert.ToString(ds.Tables[2].Rows[i]["DOC_REMARK"]);
                                    lblVerifyDocument.Style.Add("color", "red");
                                }
                                else if (Convert.ToString(ds.Tables[2].Rows[i]["DOC_STATUS"]) == "4" && Convert.ToString(ds.Tables[2].Rows[i]["DOCNO"]) == Convert.ToString(value))
                                {
                                    lblVerifyDocument.Text = "Incomplete";
                                    lblRemark.Text = Convert.ToString(ds.Tables[2].Rows[i]["DOC_REMARK"]);
                                    lblVerifyDocument.Style.Add("color", "red");
                                }
                                else
                                {
                                    lblVerifyDocument.Text = "";
                                    uploded.Text = "NO";
                                    lblRemark.Text = "";
                                }
                                if (value == Convert.ToInt32(ds.Tables[2].Rows[i]["DOCNO"]))
                                {
                                    uploded.Text = "YES";
                                    uploadDate.Text = Convert.ToString(ds.Tables[2].Rows[i]["UPLOAD_DATE"]);
                                    uploded.Visible = true;
                                    lnk.Visible = true;
                                    break;
                                }
                                else
                                {
                                    uploded.Text = "NO";
                                }

                            }
                            // fileformat.Text = "Only formats are allowed : pdf";
                        }
                    }
                }
            }
            else
            {
                lvDocument.DataSource = null;
                lvDocument.DataBind();
                lvDocument.Visible = false;
            }

            //}
            //else      Commented by swapnil thakare on dated 21-06-2021
            //{
            //    return;
            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ListofDocuments.bindlist-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
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
    public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;
        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext.ToLower();
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
            cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }

    public int Blob_UploadDepositSlip(string ConStr, string ContainerName, string DocName, FileUpload FU, byte[] ChallanCopy)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;

        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext.ToLower();
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

    private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, System.Drawing.Size size)
    {
            //Get the image current width  
            int sourceWidth = imgToResize.Width;
            //Get the image current height  
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size  
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //Calculate height with new desired size  
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //New Width  
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height  
            int destHeight = (int)(sourceHeight * nPercent);
            System.Drawing.Bitmap b = new System.Drawing.Bitmap(destWidth, destHeight);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height  
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
    }

    protected void btnSub_Click(object sender, EventArgs e)
    {
        try
        {
            DocumentControllerAcad objDoc = new DocumentControllerAcad();
            DocumentAcad objDocno = new DocumentAcad();
            byte[] StudentPhoto = null; string StudentPhotoPath = "";

            string path = "", filenames = "", docnos = "", docnames = "", existsfile = ""; int Count = 0;
            path = System.Configuration.ConfigurationManager.AppSettings["UploadDocPath"];
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (ListViewDataItem item in lvDocument.Items)
            {
                Label docno = item.FindControl("lblDocNo") as Label;
                Label DocName = item.FindControl("lblname") as Label;
                Label lblmandatory = item.FindControl("lblmandatory") as Label;
                FileUpload fuDocument = item.FindControl("fuDocument") as FileUpload;
                string docname = objCommon.LookUp("ACD_UPLOAD_DOCUMENT", "DOCNAME", "IDNO=" + Session["idno"] + "and DOCNO=" + Convert.ToInt32(docno.Text) + "");
               if (lblmandatory.Text == "1" && fuDocument.HasFile == false && docname == "")
                {
                    objCommon.DisplayMessage(this.Page, DocName.Text + " This Document Is Mandatory !!!", this.Page);
                    return;
                 }
               else
                {
                if (docno.Text == "0")
                {
                    if (fuDocument.HasFile)
                    {
                        string ext = System.IO.Path.GetExtension(fuDocument.FileName);
                        HttpPostedFile FileSize = fuDocument.PostedFile;
                        if (FileSize.ContentLength <= 1000000)
                        {
                            if (ext == ".png" || ext == ".PNG" || ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG")
                            {
                                Count++;
                                existsfile = path + Convert.ToInt32(Session["idno"]) + "_" + fuDocument.FileName;
                                FileInfo file = new FileInfo(existsfile);
                                if (file.Exists)//check file exsit or not  
                                {
                                    file.Delete();
                                }
                                //fuDocument.PostedFile.SaveAs(path + Path.GetFileName(Convert.ToInt32(Session["idno"]) + "_" + fuDocument.FileName));
                                //StudentPhoto = objCommon.GetImageData(fuDocument);
                                StudentPhotoPath = Convert.ToInt32(Session["idno"]) + "_StudentPhoto" + ext.ToLower();
                                Stream strm = fuDocument.PostedFile.InputStream;
                                System.Drawing.Image img = System.Drawing.Image.FromStream(strm);
                                System.Drawing.Bitmap b = new System.Drawing.Bitmap(img);
                                System.Drawing.Image i = resizeImage(b, new System.Drawing.Size(200, 200));
                                StudentPhoto = (byte[])(new System.Drawing.ImageConverter()).ConvertTo(i, typeof(byte[])); //objCommon.GetImageData(i);
                                int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerNamePhoto, Convert.ToInt32(Session["idno"]) + "_StudentPhoto", fuDocument, StudentPhoto);
                                //filenames += fuDocument.FileName + '$';
                                //docnos += docno.Text + '$';
                                //docnames += DocName.Text + '$'; 
                            }
                            else
                            {
                                objCommon.DisplayMessage(this.Page, "Student photo only formats are allowed : png,jpg,jpeg !!!", this.Page);
                                return;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "File Size should be less than 1 MB !!!", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        if (Convert.ToString(Session["STUDENTPHOTO"]) != "")
                        {
                            //StudentPhoto = (byte[])Session["STUDENTPHOTO"];
                            //objCommon.DisplayMessage(this.Page, "Please select at least one document for upload !!!", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "Upload student photo document is mandatory field !!!", this.Page);
                            return;
                        }
                    }

                }
                else
                {
                    if (fuDocument.HasFile)
                    {
                        HttpPostedFile FileSize = fuDocument.PostedFile;
                        string ext = System.IO.Path.GetExtension(fuDocument.FileName);
                        if (ext == ".pdf" || ext == ".PDF")
                        {
                            if (FileSize.ContentLength <= 1000000)
                            {
                                Count++;
                                existsfile = path + Convert.ToInt32(Session["idno"]) + "_" + fuDocument.FileName;
                                FileInfo file = new FileInfo(existsfile);
                                if (file.Exists)//check file exsit or not  
                                {
                                    file.Delete();
                                }
                                StudentPhoto = objCommon.GetImageData(fuDocument);
                                int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, Convert.ToInt32(Session["idno"]) + "_doc_" + docno.Text, fuDocument, StudentPhoto);
                                //if (retval == 0)
                                //{
                                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                //    return;
                                //}
                                //fuDocument.PostedFile.SaveAs(path + Path.GetFileName(Convert.ToInt32(Session["idno"]) + "_" + fuDocument.FileName));
                                filenames += Convert.ToInt32(Session["idno"]) + "_doc_" + docno.Text + ext + '$';
                                docnos += docno.Text + '$';
                                docnames += DocName.Text + '$';
                            }
                            else
                            {
                                objCommon.DisplayMessage(this.Page, "File Size should be less than 1 MB !!!", this.Page);
                                return;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "Student document only formats are allowed : pdf !!!", this.Page);
                            return;
                        }
                    }
            
                }
             }
            }
            if (Count > 0)
            {
                filenames = filenames.TrimEnd('$');
                docnos = docnos.TrimEnd('$');
                docnames = docnames.TrimEnd('$');

                CustomStatus cs = CustomStatus.Others;
                cs = (CustomStatus)objDoc.AddMultipleDocStd(Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["userno"]), filenames, docnos, docnames, StudentPhotoPath, Convert.ToInt32(ViewState["sessionno"]), StudentPhoto);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.Page, "Document Added Successfully !!!", this.Page);
                    bindlist();
                    return;
                }
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.Page, "Document Updated Successfully !!!", this.Page);
                    bindlist();
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please select at least one document for upload !!!", this.Page);
            }
        }

        catch (Exception ex)
        {

        }
       
    }
    
    protected void ddlDocument_SelectedIndexChanged(object sender, EventArgs e)
    {

        //ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "$('.fuDocumentX').trigger('click');", true);
    }


    public void UploadDocument()
    {
        updDocs.Visible = true;
        bindlist();
    }
    protected void lkuploaddocumnet_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Offer_Accept"]) != "1")
        {
            objCommon.DisplayMessage(this.Page, "Please Complete Offer Acceptance First !!!", this.Page);
            return;
        }
        if (Convert.ToInt32(ViewState["LASTQUALIFICATION"]) > 0)
        {
            if (Convert.ToString(ViewState["DOWN_PAYMENT_AMOUNT"]) == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Enrollment Down Payment Amount Not Define !!!", this.Page);
                return;
            }
            else if (Convert.ToString(ViewState["EARLY_BIRD_AMOUNT"]) == string.Empty || Convert.ToString(ViewState["REGULAR_PAYMENT_AMOUNT"]) == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Admission Proper Fee Not Define !!!", this.Page);
                return;
            }
            bindlist();
            divlkPayment.Attributes.Remove("class");
            divlkuploaddocument.Attributes.Remove("class");
            divlkPayment.Attributes.Remove("class");
            divOnlineDetails.Attributes.Remove("class");
            divlkstatus.Attributes.Remove("class");
            divacceptance.Attributes.Remove("class");
            divlkuploaddocument.Attributes.Add("class", "active");
            divlkModuleOffer.Attributes.Remove("class");
            divModuleRegistration.Visible = false;
            divlbsta.Visible = false;
            divpayment.Visible = false;
            diApplicantDetails.Visible = false;
            dipayment.Visible = false;
            divstatus.Visible = false;
            acceptance.Visible = false;
            divuploaddoc.Visible = true;
            document.Visible = true;
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Please Enter Applicant Qualification Details !!!", this.Page);
        }
        
    }
    protected void lkstatus_Click(object sender, EventArgs e)
    {
        divlkPayment.Attributes.Remove("class");
        divlkuploaddocument.Attributes.Remove("class");
        divacceptance.Attributes.Remove("class");
        status();
        divlkstatus.Attributes.Remove("class");
        divOnlineDetails.Attributes.Remove("class");
        divlkstatus.Attributes.Add("class", "active");
        divlkModuleOffer.Attributes.Remove("class");
        divModuleRegistration.Visible = false;
        divuploaddoc.Visible = false;
        dipayment.Visible = false;
        divstatus.Visible = true;
        acceptance.Visible = false;
        document.Visible = false;
        divlbsta.Visible = true;
        divpayment.Visible = false;
        diApplicantDetails.Visible = false;
        //lnkOnlineDetails.Attributes.Add("class", "finished");
        //if (Convert.ToString(Session["Offer_Accept"]) == "1")
        //{
        //    divacceptance.Attributes.Remove("class");
        //    divacceptance.Attributes.Add("class", "finished");
        //}
    }
    protected void lkacceptance_Click(object sender, EventArgs e)
    {
        divacceptance.Attributes.Remove("class");
        divlkPayment.Attributes.Remove("class");
        divlkuploaddocument.Attributes.Remove("class");
        divlkstatus.Attributes.Remove("class");
        divOnlineDetails.Attributes.Remove("class");
        divacceptance.Attributes.Add("class", "active");
        divlkModuleOffer.Attributes.Remove("class");
        divModuleRegistration.Visible = false;
        divuploaddoc.Visible = false;
        dipayment.Visible = false;
        divstatus.Visible = false;
        acceptance.Visible = true;
        document.Visible = false;
        divlbsta.Visible = false;
        divpayment.Visible = false;
        diApplicantDetails.Visible = false;
        //lnkOnlineDetails.Attributes.Add("class", "finished");
        if (Convert.ToString(Session["Offer_Accept"]) == "1")
        {
            ShowStudentDetails();
            BindOfferAcceptance();
            ddlProgramName.SelectedValue = "0";
        }
    }
    protected void lnkOnlineDetails_Click(object sender, EventArgs e)
    {
        try
        {
            BindPersonalDetails(); ListViewDataBind();
            divacceptance.Attributes.Remove("class");
            divlkPayment.Attributes.Remove("class");
            divlkuploaddocument.Attributes.Remove("class");
            divlkstatus.Attributes.Remove("class");
            divacceptance.Attributes.Remove("class");
            divOnlineDetails.Attributes.Add("class", "active");
            divlkModuleOffer.Attributes.Remove("class");
            divModuleRegistration.Visible = false;
            divuploaddoc.Visible = false;
            dipayment.Visible = false;
            divstatus.Visible = false;
            acceptance.Visible = false;
            document.Visible = false;
            divlbsta.Visible = false;
            divpayment.Visible = false;
            divOnlineDetails.Visible = true;
            diApplicantDetails.Visible = true;
        }
        catch (Exception ex)
        {

        }
    }
   
    public void getDemandData()
    {
        DataSet ds = null;
        txtchallanAmount.Text = "";
        decimal TotalChallanAmount = 0;
        try
        {
            string[] Split = ddlProgramName.SelectedValue.Split(',');

            int status = 0;
            if (ddlReceiptType.SelectedIndex > 0)
            {
                session = objCommon.LookUp("ACD_DEMAND", "(isnull(SESSIONNO,0))", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND ISNULL(CAN,0) = 0 AND ISNULL(DELET,0) = 0 AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + " AND PAYTYPENO =" + Convert.ToInt32(ViewState["paytypeno"]) + " AND ADMBATCHNO=" + Convert.ToInt32(ViewState["batchno"]) + "and RECIEPT_CODE=" + "'" + Convert.ToString(ddlReceiptType.SelectedValue) + "'");
                if (session != "")
                {
                    ds = feeController.GetFeeItems_Data_ForOnlinePayment(Convert.ToInt32(session), Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(ddlReceiptType.SelectedValue), Convert.ToInt32(ViewState["paytypeno"]), Convert.ToInt32(ViewState["batchno"]));//, ref status);
                }
                if (status != -99 && ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        // txtchallanAmount.Text =  ds.Tables[0].Rows[0]["AMOUNT"].ToString();
                        TotalChallanAmount += Convert.ToDecimal(ds.Tables[0].Rows[0]["AMOUNT"].ToString());
                    }
                    txtchallanAmount.Text = Convert.ToString(TotalChallanAmount);
                }
                
            }
        }
        catch (Exception ex)
        {
        }
    }


    protected void btnChallanSubmit_Click(object sender, EventArgs e)
    {


        byte[] ChallanCopy = null;
        try
        {
            CreateCustomerRef();
            //GenerateChallan();


            if (txtchallanAmount.Text == "0" || txtchallanAmount.Text == "" || txtchallanAmount.Text == "0.00")
            {
                objCommon.DisplayMessage(updBulkReg, "Total Amount Cannot be Less than or Equal to Zero !!", this.Page);
                return;
            }
            else
            {
                //hdfAmount.Value = txtAmount.Text;
                //SubmitData();
                //  SubmitOfflineData(); //
                //if (Convert.ToString(ViewState["INSTALLMENT_DETAILS"]) != "1")
                //{
                //    if (Convert.ToDecimal(ViewState["demandamounttobepaid"].ToString()) < Convert.ToDecimal(txtDepositAmount.Text))
                //    {
                //        objCommon.DisplayMessage(updBulkReg, "Paid amount should not be greater than applicable amount of the program !!!", this.Page);
                //        return;
                //    }
                //}
            }
            if (Convert.ToDecimal(txtchallanAmount.Text) > ((txtDepositAmount.Text == string.Empty ? Convert.ToDecimal(0) : Convert.ToDecimal(txtDepositAmount.Text))))
            {
                objCommon.DisplayMessage(updBulkReg, "All Paid amount should not be less than actual amount to be paid !!!", this.Page);
                return;
            }
            int DcrTempNo = 0;
            DcrTempNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR_TEMP", "COUNT(1)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND PAY_SERVICE_TYPE = 5 AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + "and RECIEPT_CODE=" + "'" + Convert.ToString(ViewState["RECIEPT_CODE"]) + "'"));
            if (DcrTempNo == 0)
            {
                CreateDemand();
                string session = string.Empty;
                session = GetSession();
                int Count = Convert.ToInt32(objCommon.LookUp("ACD_DCR_TEMP", "COUNT(1)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND PAY_SERVICE_TYPE IN(2,5)"));
                if (ViewState["action"].ToString() != "Edit")
                {
                    if (Count > 0)
                    {
                        if (Convert.ToString(ViewState["INSTALLMENT_DETAILS"]) == "1")
                        {
                            int InstallmentCount = Convert.ToInt32(objCommon.LookUp("ACD_DCR_TEMP", "COUNT(1)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND PAY_SERVICE_TYPE IN(2,5) AND RECIEPT_CODE = 'TF' AND SESSIONNO=" + Convert.ToInt32(session) + " AND INSTALL_NO=" + Convert.ToInt32(ViewState["INSTALL_NO"]) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"])));
                            if (InstallmentCount > 0)
                            {
                                objCommon.DisplayMessage(this.Page, "Installment deposit slip already uploaded !!!", this.Page);
                                return;
                            }
                            if (Convert.ToInt32(ViewState["INSTALLMENT_DETAILS_COUNT"].ToString()) < Count)
                            {
                                objCommon.DisplayMessage(this.Page, "All Installment deposit slip already uploaded !!!", this.Page);
                                return;
                            }
                        }
                        else
                        {
                            string SP_Name1 = "PKG_ACD_GET_PAID_AMOUNT_DETAILS";
                            string SP_Parameters1 = "@P_IDNO,@P_SEMESTERNO,@P_SESSIONNO";
                            string Call_Values1 = "" + Convert.ToInt32(Session["idno"]) + "," + Convert.ToInt32(ViewState["semesterno"].ToString()) + "," + Convert.ToInt32(session);

                            DataSet que_out1 = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);

                            if (que_out1.Tables[0] != null && que_out1.Tables[0].Rows.Count > 0)
                            {

                                if (Convert.ToInt32(que_out1.Tables[0].Rows[0]["DOC_COUNT"].ToString()) > 0)
                                {
                                    if (Convert.ToDecimal(que_out1.Tables[2].Rows[0]["TOTAL_AMT"].ToString()) <= Convert.ToDecimal(que_out1.Tables[1].Rows[0]["TOTAL_AMT"].ToString()))
                                    {
                                        objCommon.DisplayMessage(this.Page, "Deposit slip already uploaded !!!", this.Page);
                                        return;
                                    }
                                    //if (que_out1.Tables[1].Rows[0]["TOTAL_AMT"].ToString() == "0.00")
                                    //{
                                    //    objCommon.DisplayMessage(this.Page, "Deposit slip already uploaded !!!", this.Page);
                                    //    return;
                                    //}
                                    if (Convert.ToDecimal(que_out1.Tables[2].Rows[0]["TOTAL_AMT"].ToString()) <= Convert.ToDecimal(que_out1.Tables[3].Rows[0]["TOTAL_AMT"].ToString()))
                                    {
                                        objCommon.DisplayMessage(this.Page, "Deposit slip already uploaded !!!", this.Page);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                if (FuChallan.HasFile)
                {
                    string ext = System.IO.Path.GetExtension(FuChallan.FileName);
                    if (ext == ".pdf" || ext == ".PDF" || ext == ".jpg" || ext == ".JPG" || ext == ".png" || ext == ".PNG" || ext == ".jpeg" || ext == ".JPEG")
                    {
                        if (ViewState["action"].ToString() != "Edit")
                        {
                            ChallanCopy = objCommon.GetImageData(FuChallan);
                            CustomStatus cs = CustomStatus.Others;
                            //cs = (CustomStatus)feeController.InsertChallanCopyDetailserp(Convert.ToInt32(Session["idno"]), txtChallanId.Text, txtchallanAmount.Text, ChallanCopy, txtTransactionNo.Text, txtPaymentdate.Text, Convert.ToString(ddlbank.SelectedItem.Text), Convert.ToString(txtBranchName.Text));

                            int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, Convert.ToInt32(Session["idno"]) + "_ERP_" + Count + "_Deposit_Slip", FuChallan, ChallanCopy);
                            if (retval == 0)
                            {
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
                                int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, Convert.ToInt32(Session["idno"]) + "_ERP_" + Count + "_Deposit_Slip", FuChallan, ChallanCopy);
                                if (retval == 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                    return;
                                }
                                ViewState["action"] = "Edit";
                                SubmitOfflineData(ChallanCopy);
                                Session["EDITCHALLANCOPYDETAILSENROLL"] = null;
                            }
                        }
                    }
                }
                else
                {

                    objCommon.DisplayMessage(updChallan, "Please Upload Deposit Slip !!!", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(updBulkReg, "Record Already Exists !!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

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
                for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                {
                    if (ds.Tables[4].Rows[i]["RECON"].ToString() == "True")
                    {
                        divOfflinePaymentdone.Visible = true;
                        divOfflineNote.Visible = false;
                    }
                    else
                    {
                        divOfflinePaymentdone.Visible = false;
                        divOfflineNote.Visible = true;
                    }
                }
            }
            else
            {
                lvDepositSlip.DataSource = null;
                lvDepositSlip.DataBind();
                divOfflineNote.Visible = false;
                divOfflinePaymentdone.Visible = false;
            }


            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                //txtUserName.Text = Convert.ToString(ds.Tables[0].Rows[0]["USERNAME"]);
                //txtEmail.Text = Convert.ToString(ds.Tables[0].Rows[0]["EMAILID"]);
                //txtmobilecode.Text = Convert.ToString(ds.Tables[0].Rows[0]["MOBILECODE"]);
                //txtMobile.Text = Convert.ToString(ds.Tables[0].Rows[0]["MOBILENO"]);
                //txtAmount.Text = Convert.ToString(ds.Tables[0].Rows[0]["FEES"]);
                //hdfAmount.Value = Convert.ToString(ds.Tables[0].Rows[0]["FEES"]);
            }
            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                //lvPaymentDetails.DataSource = ds.Tables[1];
                //lvPaymentDetails.DataBind();
                string[] degreenames1; string[] degreenames2; int count = 0; int count1 = 0;
                degreenames1 = ds.Tables[1].Rows[0]["DEGREE_NAMES"].ToString().Split(',');
                degreenames2 = ds.Tables[1].Rows[0]["DEGREE_NAMES1"].ToString().Split(',');
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void btnedit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;

            ViewState["action"] = "Edit";
            int srno = int.Parse(btnEdit.CommandArgument);
            ViewState["srno"] = srno;

            string SP_Name1 = "PKG_ACD_GET_UPLOADED_DEPOSIT_SLIP_DATA";
            string SP_Parameters1 = "@P_IDNO,@P_TEMP_DCR_NO";
            string Call_Values1 = "" + Convert.ToInt32(Session["idno"]) + "," + Convert.ToInt32(srno);

            DataSet chkds = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);

            if (chkds.Tables[0].Rows.Count > 0)
            {
                ddlbank.SelectedValue = chkds.Tables[0].Rows[0]["BANK_NO"].ToString() == string.Empty ? "0" : chkds.Tables[0].Rows[0]["BANK_NO"].ToString();
                txtBranchName.Text = chkds.Tables[0].Rows[0]["BRANCH_NAME"].ToString().Trim();
                if (Convert.ToString(chkds.Tables[0].Rows[0]["EXTRA_CHARGE_DISCOUNT_TYPE"].ToString().Trim()) == "1")
                {
                    txtchallanAmount.Text = Convert.ToString(Convert.ToDecimal(chkds.Tables[0].Rows[0]["TOTAL_AMT"].ToString().Trim()) - Convert.ToDecimal(chkds.Tables[0].Rows[0]["EXTRA_DISC"].ToString().Trim()));
                }
                else if (Convert.ToString(chkds.Tables[0].Rows[0]["EXTRA_CHARGE_DISCOUNT_TYPE"].ToString().Trim()) == "2")
                {
                    txtchallanAmount.Text = Convert.ToString(Convert.ToDecimal(chkds.Tables[0].Rows[0]["TOTAL_AMT"].ToString().Trim()) + Convert.ToDecimal(chkds.Tables[0].Rows[0]["EXTRA_DISC"].ToString().Trim()));
                }
                else
                {
                    txtchallanAmount.Text = chkds.Tables[0].Rows[0]["TOTAL_AMT"].ToString().Trim();
                }
                txtDepositAmount.Text = Convert.ToDecimal(chkds.Tables[0].Rows[0]["AMOUNT"]).ToString("N").Trim();
                txtPaymentdate.Text = chkds.Tables[0].Rows[0]["REC_DATE"].ToString().Trim();
                ViewState["ORDER_ID"] = chkds.Tables[0].Rows[0]["ORDER_ID"].ToString().Trim();
                Session["EDITCHALLANCOPYDETAILSENROLL"] = "";//(byte[])chkds.Tables[0].Rows[0]["CHALLAN_COPY"];
            }
            //DataSet chkds = objCommon.FillDropDown("ACD_DCR_TEMP", "TEMP_DCR_NO", "Format(REC_DT,'dd/MM/yyyy') AS REC_DATE,*", "TEMP_DCR_NO=" + srno, string.Empty);
            //if (chkds.Tables[0].Rows.Count > 0)
            //{
            //    ddlbank.SelectedValue = chkds.Tables[0].Rows[0]["BANK_NO"].ToString();
            //    txtBranchName.Text = chkds.Tables[0].Rows[0]["BRANCH_NAME"].ToString().Trim();
            //    txtchallanAmount.Text = chkds.Tables[0].Rows[0]["TOTAL_AMT"].ToString().Trim();
            //    txtPaymentdate.Text = chkds.Tables[0].Rows[0]["REC_DATE"].ToString().Trim();
            //    ViewState["ORDER_ID"] = chkds.Tables[0].Rows[0]["ORDER_ID"].ToString().Trim();
            //    txtDepositAmount.Text = Convert.ToDecimal(chkds.Tables[0].Rows[0]["AMOUNT"]).ToString("N").Trim();
            //    Session["EDITCHALLANCOPYDETAILSENROLL"] = string.Empty;//(byte[])chkds.Tables[0].Rows[0]["CHALLAN_COPY"];

            //    //ddlSection.SelectedValue = chkds.Tables[0].Rows[0]["UGPGOT"].ToString().Trim();
            //    //rdoSpecilization.SelectedValue = chkds.Tables[0].Rows[0]["ISSPECIALIZATION1"].ToString().Trim(); // ADDED BY SWAPNIL THAKARE ON 09-07-2021 
            //    //if (chkds.Tables[0].Rows[0]["ACTIVE1"].ToString().Trim() == "1")
            //    //{
            //    //    //chkActive.Checked = true;
            //    //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(true);", true);
            //    //}
            //    //else
            //    //{
            //    //    //chkActive.Checked = false;
            //    //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(false);", true);
            //    //}

            //    //ddlClassification.SelectedValue = chkds.Tables[0].Rows[0]["AREA_INT_NO1"].ToString();  //ADDED BY SWAPNIL THAKARE ON DATED 28-07-2021
            //    //ddlCollegeName.Enabled = false;
            //    //ddlDegreeName.Enabled = false;
            //}
        }
        catch (Exception ex)
        { 
        
        }
    }


    protected void btnDel_Click(object sender, ImageClickEventArgs e)
    {
        //ACD_COLLEGE_DEGREE_BRANCH;

        ImageButton btnDel = sender as ImageButton;

        ViewState["action"] = "delete";
        int srno = int.Parse(btnDel.CommandArgument);

        // Added by swapnil thakare on dated 02/08/2021

        int output = feeController.DeleteDcrTempRecord(srno);
        if (output != -99 && output != 99)
        {
            objCommon.DisplayMessage(this.Page, "Record Deleted Successfully", this.Page);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Record Is Not Deleted ", this.Page);
            // Clear();
        }
        fillDetails();
    }

    public void SubmitOfflineData(byte[] ChallanCopy)
    {
        try
        {

            // CreateCustomerRef();
            string session = string.Empty;
            session = GetSession();

            int result = 0;
            int DM_NO = 0;

            string Ext = Path.GetExtension(FuChallan.FileName);
            DM_NO = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "DM_NO", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO=" + Convert.ToInt32(session) + " AND ISNULL(CAN,0) = 0 AND ISNULL(DELET,0) = 0 AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + "and RECIEPT_CODE=" + "'" + Convert.ToString(ddlReceiptType.SelectedValue) + "'"));
            int Count = Convert.ToInt32(objCommon.LookUp("ACD_DCR_TEMP", "COUNT(1)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND PAY_SERVICE_TYPE IN(2,5)"));
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {

                    if (DM_NO > 0)
                    {

                        if (ddlReceiptType.SelectedValue == "HF")//txtBranchName  ddlbank
                        {
                            result = feeController.InsertOfflinePayment_DCR_TEMP(DM_NO, Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["SESSIONNO"]), Convert.ToDateTime(txtPaymentdate.Text), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(lblOrderID.Text), 2, Convert.ToInt32(5), ChallanCopy, Convert.ToInt32(ddlbank.SelectedValue), Convert.ToString(ddlbank.SelectedItem.Text), txtBranchName.Text, txtDepositAmount.Text, Convert.ToInt32(Session["idno"]) + "_ERP_" + Count + "_Deposit_Slip" + Ext, Convert.ToString(ViewState["INSTALL_NO"]));
                        }
                        else
                        {
                            result = feeController.InsertOfflinePayment_DCR_TEMP(DM_NO, Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["SESSIONNO"]), Convert.ToDateTime(txtPaymentdate.Text), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(lblOrderID.Text), 2, Convert.ToInt32(5), ChallanCopy, Convert.ToInt32(ddlbank.SelectedValue), Convert.ToString(ddlbank.SelectedItem.Text), txtBranchName.Text, txtDepositAmount.Text, Convert.ToInt32(Session["idno"]) + "_ERP_" + Count + "_Deposit_Slip" + Ext, Convert.ToString(ViewState["INSTALL_NO"]));
                        }
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(this.updBulkReg, "Demand not created!", this.Page);
                        return;
                    }
                    if (result > 0)
                    {
                        objCommon.DisplayMessage(this.Page, "Deposit Data Saved Succesfully !!!", this.Page);
                        ViewState["action"] = "add";
                        txtChallanId.Text = "";
                        txtchallanAmount.Text = "";
                        ddlbank.SelectedValue = null;
                        txtBranchName.Text = "";
                        txtPaymentdate.Text = "";
                        fillDetails();
                        GetPreviousReceipt();
                    }
                    else
                    {
                        lblStatus.Visible = true;
                        objCommon.DisplayUserMessage(updBulkReg, "Failed To Done Offline Payment.", this.Page);
                        return;
                    }
                }


                else if (ViewState["action"].ToString().Equals("Edit")) // ViewState["ORDER_ID"]
                {
                    result = feeController.UpdateOfflinePayment_DCR_TEMP(Convert.ToInt32(ViewState["srno"].ToString()), DM_NO, Convert.ToInt32(Session["idno"].ToString()), Convert.ToInt32(ViewState["SESSIONNO"].ToString()), Convert.ToDateTime(txtPaymentdate.Text), Convert.ToInt32(ViewState["semesterno"].ToString()), Convert.ToString(ViewState["ORDER_ID"].ToString()), Convert.ToInt32(2), Convert.ToInt32(2), ChallanCopy, Convert.ToInt32(ddlbank.SelectedValue), Convert.ToString(ddlbank.SelectedItem.Text), txtBranchName.Text, txtDepositAmount.Text, Convert.ToInt32(Session["idno"]) + "_ERP_" + Count + "_Deposit_Slip" + Ext);
                    if (result > 0)
                    {

                        objCommon.DisplayMessage(this.Page, "Deposit Data Updated Succesfully!", this.Page);
                        ViewState["action"] = "add";
                        txtChallanId.Text = "";
                        txtchallanAmount.Text = "";
                        ddlbank.SelectedValue = null;
                        txtBranchName.Text = "";
                        txtPaymentdate.Text = "";
                        fillDetails();
                        GetPreviousReceipt();
                    }
                    else
                    {
                        lblStatus.Visible = true;
                        objCommon.DisplayUserMessage(updBulkReg, "Failed To Update Offline Payment.", this.Page);
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
    protected void lkModuleRegistration_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Offer_Accept"]) != "1")
            {
                objCommon.DisplayMessage(this.Page, "Please Complete Offer Acceptance First !!!", this.Page);
                return;
            }
            else if (Convert.ToInt32(ViewState["UPLOADDOCUMENTCOUNT"]) == 0)
            {
                objCommon.DisplayMessage(this.Page, "Please Complete Document Upload First !!!", this.Page);
                return;
            }
            if (Convert.ToInt32(ViewState["LASTQUALIFICATION"]) > 0)
            {
                divacceptance.Attributes.Remove("class");
                divlkPayment.Attributes.Remove("class");
                divlkuploaddocument.Attributes.Remove("class");
                divlkstatus.Attributes.Remove("class");
                divacceptance.Attributes.Remove("class");
                divOnlineDetails.Attributes.Remove("class");
                divlkModuleOffer.Attributes.Add("class", "active");
                divuploaddoc.Visible = false;
                dipayment.Visible = false;
                divstatus.Visible = false;
                acceptance.Visible = false;
                document.Visible = false;
                divlbsta.Visible = false;
                divpayment.Visible = false;
                diApplicantDetails.Visible = false;
                divlkModuleOffer.Visible = true;
                divModuleRegistration.Visible = true;
                //if (Convert.ToString(Session["Offer_Accept"]) == "1")
                //{
                //    divacceptance.Attributes.Add("class", "finished");
                //}

                DataSet ds = null; int count = 0; 
                ds = objdocContr.getModuleOfferedCourses(Convert.ToInt32(Session["idno"]));
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    count++;
                    Panel2.Visible = true;
                    lvOfferedSubject.DataSource = ds.Tables[0];
                    lvOfferedSubject.DataBind();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["EXAM_REGISTERED"].ToString() == "1")
                        {
                            btnSubmitOffer.Visible = false;
                            objCommon.DisplayMessage(this.Page, "Module registration already done", this.Page);
                            break;
                        }
                        else
                        {
                            btnSubmitOffer.Visible = true;
                        }
                    }
                }
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    count++;
                    Panel3.Visible = true;
                    lvcoursetwo.DataSource = ds.Tables[1];
                    lvcoursetwo.DataBind();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        if (ds.Tables[1].Rows[i]["EXAM_REGISTERED"].ToString() == "1")
                        {
                            btnSubmitOffer.Visible = false;
                            objCommon.DisplayMessage(this.Page, "Module registration already done", this.Page);
                            break;
                        }
                        else
                        {
                            btnSubmitOffer.Visible = true;
                        }
                    }
                }
                if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    count++;
                    Panel4.Visible = true;
                    lvcoursethree.DataSource = ds.Tables[2];
                    lvcoursethree.DataBind();
                    //if (ds.Tables[2].Rows[0]["EXAM_REGISTERED"].ToString() == "1")
                    //{

                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        //btnSubmitOffer.Visible = false;
                        CheckBox CHK = lvcoursethree.Items[i].FindControl("chkRows") as CheckBox;
                        HiddenField hdf = lvcoursethree.Items[i].FindControl("hdfCourseNo") as HiddenField;
                        if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                        {
                            for (int j = 0; j < ds.Tables[3].Rows.Count; j++)
                            {
                                if ((ds.Tables[2].Rows[i]["COURSENO"].ToString() == ds.Tables[3].Rows[j]["COURSENO"].ToString()))
                                {
                                    CHK.Checked = true;
                                    CHK.Enabled = false;
                                }
                            }
                        }

                        if ((ds.Tables[2].Rows[i]["COURSENO"].ToString() == hdf.Value.ToString() && ds.Tables[2].Rows[i]["EXAM_REGISTERED"].ToString() == "1"))
                        {
                            CHK.Checked = true;
                            //btnSubmitOffer.Visible = false;
                        }
                    }

                    //objCommon.DisplayMessage(this.Page, "Modules registration already done", this.Page);
                    //}
                    //else
                    //{
                    //    btnSubmitOffer.Visible = true;
                    //}
                }
                if (count == 0)
                {
                    lvOfferedSubject.DataSource = null;
                    lvOfferedSubject.DataBind();
                    objCommon.DisplayMessage(this.Page, "Subject Not Offered !!!", this.Page);
                    btnSubmitOffer.Visible = false;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Applicant Qualification Details !!!", this.Page);
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSubmitOffer_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(Session["usertype"]) == 2)
            {
                string Coursenos = "", Ccode = "", Subids = "", CourseNames = "", Credits = "", licUano = "";
                foreach (ListViewDataItem items in lvOfferedSubject.Items)
                {
                    CheckBox chk = items.FindControl("chkRows") as CheckBox;
                    HiddenField hdfCourseNo = items.FindControl("hdfCourseNo") as HiddenField;
                    HiddenField hdfSubid = items.FindControl("hdfSubid") as HiddenField;
                    Label lblCCode = items.FindControl("lblCCode") as Label;
                    Label lblCoursename = items.FindControl("lblCourseName") as Label;
                    Label lblCredits = items.FindControl("lblCredits") as Label;
                    Label lblUano = items.FindControl("lbluano") as Label;
                    if (chk.Checked == true)
                    {
                        Coursenos += hdfCourseNo.Value + ',';
                        Ccode += lblCCode.Text + ',';
                        CourseNames += lblCoursename.Text + ',';
                        Credits += lblCredits.Text + ',';
                        Subids += hdfSubid.Value + ',';
                        licUano += lblUano.Text + ',';
                    }
                }
                foreach (ListViewDataItem items in lvcoursetwo.Items)
                {
                    CheckBox chk = items.FindControl("chkRows") as CheckBox;
                    HiddenField hdfCourseNo = items.FindControl("hdfCourseNo") as HiddenField;
                    HiddenField hdfSubid = items.FindControl("hdfSubid") as HiddenField;
                    Label lblCCode = items.FindControl("lblCCode") as Label;
                    Label lblCoursename = items.FindControl("lblCourseName") as Label;
                    Label lblCredits = items.FindControl("lblCredits") as Label;
                    Label lblUano = items.FindControl("lbluano") as Label;
                    if (chk.Checked == true)
                    {
                        Coursenos += hdfCourseNo.Value + ',';
                        Ccode += lblCCode.Text + ',';
                        CourseNames += lblCoursename.Text + ',';
                        Credits += lblCredits.Text + ',';
                        Subids += hdfSubid.Value + ',';
                        licUano += lblUano.Text + ',';
                    }
                }
                foreach (ListViewDataItem items in lvcoursethree.Items)
                {
                    CheckBox chk = items.FindControl("chkRows") as CheckBox;
                    HiddenField hdfCourseNo = items.FindControl("hdfCourseNo") as HiddenField;
                    HiddenField hdfSubid = items.FindControl("hdfSubid") as HiddenField;
                    Label lblCCode = items.FindControl("lblCCode") as Label;
                    Label lblCoursename = items.FindControl("lblCourseName") as Label;
                    Label lblCredits = items.FindControl("lblCredits") as Label;
                    Label lblUano = items.FindControl("lbluano") as Label;
                    if (chk.Checked == true)
                    {
                        Coursenos += hdfCourseNo.Value + ',';
                        Ccode += lblCCode.Text + ',';
                        CourseNames += lblCoursename.Text + ',';
                        Credits += lblCredits.Text + ',';
                        Subids += hdfSubid.Value + ',';
                        licUano += lblUano.Text + ',';
                    }
                }
                Coursenos = Coursenos.TrimEnd(',');
                Ccode = Ccode.TrimEnd(',');
                CourseNames = CourseNames.TrimEnd(',');
                Credits = Credits.TrimEnd(',');
                Subids = Subids.TrimEnd(',');
                licUano = licUano.TrimEnd(',');

                int output = objdocContr.InsertModuleRegistration(Convert.ToInt32(Session["idno"]), Coursenos, Ccode, CourseNames, Credits, Subids, licUano);

                if (output != -99 && output != 99)
                {
                    objCommon.DisplayMessage(this.Page, "Record Added Successfully", this.Page);
                    btnSubmitOffer.Visible = false;
                    ViewState["RESULT_STATUS"] = 1;
                    string SP_Parameters = ""; string Call_Values = ""; string SP_Name = "";

                    SP_Name = "PKG_ACD_CALCULATE_DEMAND_DYNAMICALLY";
                    SP_Parameters = "@P_IDNO,@P_UA_NO,@P_SESSIONNO,@P_DM_NO";
                    Call_Values = "" + Convert.ToInt32(Session["idno"]) + "," + Convert.ToInt32(Session["userno"]) + "," + Convert.ToInt32(ViewState["SESSIONNO"]) + ",0" + "";
                    string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true, 1);

                    lkpayment_Click(new object(), new EventArgs());
                    //if (Convert.ToString(ViewState["MINISTYFLAG"]) != "1")
                    //{
                    //    lkpayment_Click(new object(), new EventArgs());
                    //}
                }
                else
                {
                    ViewState["RESULT_STATUS"] = 0;
                    objCommon.DisplayMessage(this.Page, "Error", this.Page);
                    // Clear();
                }
            }
            else
            {
                ViewState["RESULT_STATUS"] = 0;
                objCommon.DisplayMessage(this.Page, "This process only for student !", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void showOnlinePaymentDetails_Click(object sender, EventArgs e)
    {
        pnlStudentsFees.Visible = true;
        divViewpayment.Visible = true;
    }
   
    //added by  aashna
    protected void btnGenerateChallan_Click(object sender, EventArgs e)
    {
        try
        {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#modelBank').modal('show')", true);
        }
        catch (Exception ex)
        {
        }
    }

    protected void lvDocument_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label lblname, lblsrno;
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            lblname = (Label)e.Item.FindControl("lblname");
            lblsrno = (Label)e.Item.FindControl("lblSRNO");
        }
    }
    protected void imgbtnPrevDoc_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //ImageButton lnkView = (ImageButton)(sender);
            //string urlpath = System.Configuration.ConfigurationManager.AppSettings["VirtualPathOnlineAdmissionDoc"].ToString();
            //iframeView.Src = urlpath + lnkView.ToolTip;
            //mpeViewDocument.Show();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ListofDocuments.imgBtnPrev_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        //string path = ViewState["PREVIEW"].ToString();
        //iframeView.Attributes.Add("src", path);

        //mpeViewDocument.Show();
    }

    protected void CreateDemand()
    {
        try
        {
            int status = feeController.GenerateDemandForDownPayment(Convert.ToInt32(ViewState["SESSIONNO"]), Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(ddlReceiptType.SelectedValue), Convert.ToInt32(ViewState["batchno"]),Convert.ToInt32(Session["userno"]),Convert.ToDecimal(ViewState["demandamounttobepaid"]));
        }
        catch (Exception ex)
        { 
        
        }
    }

    protected void btneditProgram_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEdit = sender as ImageButton;

        ViewState["action"] = "delete";
        int srno = int.Parse(btnEdit.CommandArgument);
        ViewState["srno"] = srno;

        //this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'>";
        //this.divMsg.InnerHtml += " if(confirm('No demand found for semester " + lblSemester.Text + ".\\nDo you want to Delete demand for this semester?'))";

        LinkButton btneditProgram = sender as LinkButton;
        HiddenField hdfDmNo = btneditProgram.NamingContainer.FindControl("hdfDmNo") as HiddenField;
        int DMNO = Convert.ToInt32(hdfDmNo.Value);

        // Added by swapnil thakare on dated 02/08/2021

        int output = feeController.DeleteDemandRecord(DMNO, srno);
        if (output != -99 && output != 99)
        {
            objCommon.DisplayMessage(this.Page, "Record Deleted Successfully", this.Page);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Record Is Not Deleted ", this.Page);
            // Clear();
        }
        // fillDetails();

        //this.divMsg.InnerHtml += "{__doPostBack('CreateDemand', '');}</script>";

        //DataSet chkds = objCommon.FillDropDown("ACD_DCR_TEMP", "TEMP_DCR_NO", "Format(REC_DT,'dd/MM/yyyy') AS REC_DATE,*", "TEMP_DCR_NO=" + srno, string.Empty);
        //if (chkds.Tables[0].Rows.Count > 0)
        //{
        //    ddlbank.SelectedValue = chkds.Tables[0].Rows[0]["BANK_NO"].ToString();
        //    txtBranchName.Text = chkds.Tables[0].Rows[0]["BRANCH_NAME"].ToString().Trim();
        //    txtchallanAmount.Text = chkds.Tables[0].Rows[0]["TOTAL_AMT"].ToString().Trim();
        //    txtPaymentdate.Text = chkds.Tables[0].Rows[0]["REC_DATE"].ToString().Trim();
        //    ViewState["ORDER_ID"] = chkds.Tables[0].Rows[0]["ORDER_ID"].ToString().Trim();

        //}
    }

    protected void lnkViewDoc_Click(object sender, EventArgs e)
    {
        try
        {
            divlkPayment.Attributes.Remove("class");
            LinkButton lnk = sender as LinkButton;
            int value = int.Parse(lnk.CommandArgument);
            string FileName = lnk.CommandName.ToString();
            if (value == 0)
            {
                irm1.Visible = false;
                ImageViewer.Visible = true;
                ltEmbed.Visible = false;
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
                CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerNamePhoto);
                string img = lnk.CommandName.ToString();
                var ImageName = img;
                if (img != null || img != "")
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
                    hdfImagePath.Value = "data:image/png;base64," + Base64String;
                    imageViewerContainer.Visible = false;

                    //string embed = "<object data=\"{0}\" type=\"application/pdf\">";
                    //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    //embed += "</object>";
                    ImageViewer.ImageUrl = string.Format("data:image/png;base64," + Base64String);

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
                }
            }
            else
            {
                imageViewerContainer.Visible = false;
                irm1.Visible = true;
                ImageViewer.Visible = false;
                ltEmbed.Visible = false;
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
                string img = lnk.CommandName.ToString();
                var ImageName = img;
                if (img != null || img != "")
                {

                    DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
                    //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"700px\" height=\"400px\">";
                    //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    //embed += "</object>";
                    //ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
                }
                else
                {
                    objCommon.DisplayMessage(updBulkReg, "Sorry, File not found !!!", this.Page);
                }
                //string docPath = System.Configuration.ConfigurationManager.AppSettings["UploadDocPath"];
                //string fileName = docPath.ToString() + "\\" + FileName.ToString(); ;
                //byte[] fileContent = null;
                //if (File.Exists(fileName))
                //{
                //    System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                //    System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                //    long byteLength = new System.IO.FileInfo(fileName).Length;
                //    fileContent = br.ReadBytes((Int32)byteLength);
                //    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                //    string base64String = Convert.ToBase64String(fileContent, 0, Convert.ToInt32(byteLength));
                //    string mimetype = GetContentType(fileName);
                //    string pdfIFrameSrc = "data:" + mimetype.ToString() + ";base64," + Convert.ToString(base64String) + "";
                //    //  data:application/pdf;base64,JVBERi0xLjcgCiXi48/TIAoxIDAgb2JqIAo8PCAK
                //    //   data:application/pdf;base64,JVBERi0xLjcKJcKzx9gNCjEgMCBvYmoNPDwvTm
                //    iframe1.Attributes.Add("src", pdfIFrameSrc);
                //    iframe1.Visible = true;
                //    ImageViewer.Visible = false;

                //}
                //else
                //{
                //    objCommon.DisplayMessage(updBulkReg, "Sorry, File not available on this machine!", this.Page);
                //}
            }
        }
        catch (Exception ex)
        {

        }
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
    private string GetContentType(string fileName)
    {
        string strcontentType = "application/octetstream";
        string ext = System.IO.Path.GetExtension(fileName).ToLower();
        Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
        if (registryKey != null && registryKey.GetValue("Content Type") != null)
            strcontentType = registryKey.GetValue("Content Type").ToString();
        return strcontentType;
    }



    //add by aashna 04-01-2021
    protected void rptstatus_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("StatusSlip", "rptCourseStatusSlip.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "OnlinepaymentStudentLogin.aspx.rptstatus_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
            //string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";

            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(Session["idno"]);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "OnlinepaymentStudentLogin.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnNextDoc_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["STUDENTPHOTO"]) != "")
        {
            lkpayment_Click(new object(), new EventArgs());
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Upload student photo document is mandatory field !!!", this.Page);
            return;
        }
        
        ///lkModuleRegistration_Click(new object(), new EventArgs());
    }

    protected void lnkDocMappingDegree_Click(object sender, EventArgs e)
    {
        try
        {
            divlkPayment.Attributes.Remove("class");
            LinkButton lnk = sender as LinkButton;
            int value = int.Parse(lnk.CommandArgument);
            string FileName = lnk.CommandName.ToString();

            ImageViewer.Visible = false;
            ltEmbed.Visible = true;
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
            string img = lnk.CommandName.ToString();
            var ImageName = img;
            if (img != null || img != "")
            {

                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

                var Newblob = blobContainer.GetBlockBlobReference(ImageName);

                string filePath = directoryPath + "\\" + ImageName;


                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }

                Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);

                Response.Clear();
                Response.ClearHeaders();

                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.TransmitFile(filePath);
                Response.Flush();
                Response.End();

                //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"700px\" height=\"400px\">";
                //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                //embed += "</object>";
                //ltEmbed.Text = string.Format(embed,ImageName);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
            }
            else
            {
                objCommon.DisplayMessage(updBulkReg, "Sorry, File not found !!!", this.Page);
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnSummarySheet_Click(object sender, EventArgs e)
    {
        ShowReportAdm("Certificate Admission", "Certi_Admission.rpt");
        //DynamicPdfViewer();
        //ShowConfirmationReport("Final_Confirmation_Report", "AdmisionConfirmSlip.rpt", Convert.ToInt32(Session["idno"]));
    }
    private void ShowReportAdm(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            //url += "Reports/CommonReport.aspx?";
            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(Session["idno"]);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentReconsilReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void DynamicPdfViewer()
    {
        try
        {
            Document document = new Document(PageSize.A4, 88f, 88f, 10f, 10f);
            Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable table = null;

                document.Open();

                //Header Table
                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.8f, 0.0f });

                //Company Logo
                cell = ImageCell("~/IMAGES/logo.png", 30f, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);

                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);

                //document.Add(table);
                document.Add(table);
                //Photo
                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.8f, 0.0f });

                string SP_Name1 = "PKG_GET_CONFIRM_STUDENT_DATA";
                string SP_Parameters1 = "@P_IDNO";
                string Call_Values1 = "" + Convert.ToInt32(Session["idno"]);

                DataSet que_out1 = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);

                string directoryPath = "";
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                string directoryName = "~/DownloadImg" + "/";
                directoryPath = Server.MapPath(directoryName);

                if (!Directory.Exists(directoryPath.ToString()))
                {

                    Directory.CreateDirectory(directoryPath.ToString());
                }
                CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerNamePhoto);

                var Newblob = blobContainer.GetBlockBlobReference(que_out1.Tables[0].Rows[0]["PHOTOPATH"].ToString());
                string filePath = directoryPath + "\\" + que_out1.Tables[0].Rows[0]["PHOTOPATH"].ToString();
                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                
                byte[] Imagebytes = System.IO.File.ReadAllBytes(filePath);

                string s = Convert.ToBase64String(Imagebytes);
                byte[] imageBytes = Convert.FromBase64String(s);

                cell = ImageCellByte(imageBytes, 11f, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);

                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);

                document.Add(table);

                table = new PdfPTable(3);
                table.SetWidths(new float[] { 0.3f, 0.1f, 0.5f });
                table.TotalWidth = 410f;
                table.LockedWidth = true;
                table.SpacingBefore = 10f;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;

                table.AddCell(PhraseCell(new Phrase("Intake", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["BATCHNAME"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Student Registration No", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["ENROLLNO"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                //table.AddCell(PhraseCell(new Phrase("Name with Initials", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                //table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                //table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["NAME_INITIAL"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                //cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                //cell.Colspan = 3;
                //cell.PaddingBottom = 10f;
                //table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Name in Full", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["STUDNAME"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("NIC / Passport", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["NICPASS"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Address", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["LADDRESS"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Contact No", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["STUDENTMOBILE"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Email", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["EMAILID"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                //table.AddCell(PhraseCell(new Phrase("SLIIT Email", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                //table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                //table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["COLLEGE_EMAIL"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                //cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                //cell.Colspan = 3;
                //cell.PaddingBottom = 10f;
                //table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Programme", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["PROGRAM_NAME"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Campus", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["CAMPUSNAME"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Batch", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["WEEKDAYSNAME"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Date of Registration", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["REGDATE"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Orientation Group", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase("", FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                document.Add(table);
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=StudentSummarySheet.pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
            }
        }
        catch (Exception ex)
        {

        }
    }
    private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, Color color)
    {
        PdfContentByte contentByte = writer.DirectContent;
        contentByte.SetColorStroke(color);
        contentByte.MoveTo(x1, y1);
        contentByte.LineTo(x2, y2);
        contentByte.Stroke();
    }
    private static PdfPCell PhraseCell(Phrase phrase, int align)
    {
        PdfPCell cell = new PdfPCell(phrase);
        cell.BorderColor = Color.WHITE;
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 2f;
        cell.PaddingTop = 0f;
        return cell;
    }
    private static PdfPCell ImageCell(string path, float scale, int align)
    {
        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path));
        image.ScalePercent(scale);
        PdfPCell cell = new PdfPCell(image);
        cell.BorderColor = Color.WHITE;
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 0f;
        cell.PaddingTop = 20f;
        return cell;
    }

    private static PdfPCell ImageCellByte(byte[] imageBytes, float scale, int align)
    {
        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageBytes);
        //image.ScalePercent(15f,15f);
        //var reader = new PdfReader(imageBytes);
        image.ScaleAbsolute(95f, 95f); // Set image size.
        //image.SetAbsolutePosition(reader.GetPageSize(1).Width / 2 - 100, 50);// Set image position.
        PdfPCell cell = new PdfPCell(image);
        cell.BorderColor = Color.WHITE;
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 0f;
        cell.PaddingTop = 20f;
        //cell.Width = 20f;

        return cell;
    }
    private void ShowConfirmationReport(string reportTitle, string rptFileName, int idno)
    {
        try
        {
            string colgname = "SLIIT";// ViewState["College_Name"].ToString().Replace(" ", "_");

            string exporttype = "pdf";
            //string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];

            //    Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/commonreport.aspx?";
            url += "exporttype=" + exporttype;

            url += "&filename=" + colgname + "-" + reportTitle + "." + exporttype;

            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno;
            // url += "&param=@P_USERNO=" + ((UserDetails)(Session["user"])).UserNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_REPORTS_gradeCard .ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lnkPay_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            txtchallanAmount.Text = Convert.ToString(lnk.CommandName);
            txtAmount.Text = Convert.ToString(lnk.CommandName);
            hdfAmount.Value = Convert.ToString(lnk.CommandName);
            pnlStudentsFees.Visible = false;
            divViewpayment.Visible = true;
            ViewState["INSTALL_NO"] = Convert.ToString(lnk.CommandArgument);
            ViewState["action"] = "add";
        }
        catch (Exception ex)
        {

        }
    }
    
    protected void ddlPCon_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPCon.SelectedValue != "0")
        {
            objCommon.FillDropDownList(ddlPermanentState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0 and COUNTRYNO=" + ddlPCon.SelectedValue, "STATENAME");
        }
        else
        {
            ddlPermanentState.SelectedValue = "0";
        }
    }

    protected void ddlPermanentState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPermanentState.SelectedValue != "0")
            {
                objCommon.FillDropDownList(ddlPTahsil, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 AND ACTIVESTATUS = 1 and STATENO=" + ddlPermanentState.SelectedValue, "CITY");
                // objCommon.FillDropDownList(ddlPTahsil, "ACD_DISTRICT", "DISTINCT DISTRICTNO", "DISTRICTNAME", "STATENO=" + ddlPermanentState.SelectedValue, "DISTRICTNO");
            }
            else
            {
                ddlPTahsil.SelectedValue = "0";
            }
        }
        catch (Exception ex)
        {

        }
    }

    //////////////////////////////////////////////// Personal Details Start ////////////////////////////////////////

    private void BindPersonalDetails()
    {
        try
        {
            objCommon.FillDropDownList(ddlPCon, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO>0", "COUNTRYNAME");

            DataSet ds = objnuc.GetStudPersonalDetails(Convert.ToInt32(ViewState["USER_NO"]));
            txtDateOfBirth.Text = "";
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                txtMiddleName.Text = ds.Tables[0].Rows[0]["MIDDLENAME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["MIDDLENAME"].ToString();
                txtFirstName.Text = ds.Tables[0].Rows[0]["FIRSTNAME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["FIRSTNAME"].ToString();
                txtFatherName.Text = ds.Tables[0].Rows[0]["FATHERNAME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["FATHERNAME"].ToString();
                txtMothersName.Text = ds.Tables[0].Rows[0]["MOTHERNAME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                txtPerlastname.Text = ds.Tables[0].Rows[0]["LASTNAME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["LASTNAME"].ToString();
                rdbQuestion.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["CITIZEN_NO"]) == null ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["CITIZEN_NO"]);

                if (Convert.ToString(ds.Tables[0].Rows[0]["DOB"]) != string.Empty)
                {
                    txtDateOfBirth.Text = Convert.ToString(Convert.ToDateTime(ds.Tables[0].Rows[0]["DOB"].ToString()));
                }
                
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["EMAILID"].ToString();
                txtPersonalPassprtNo.Text = ds.Tables[0].Rows[0]["PASSPORTNO"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["PASSPORTNO"].ToString();
                txtOnlineNIC.Text = ds.Tables[0].Rows[0]["NIC"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["NIC"].ToString();
                rdPersonalGender.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["GENDER"]) == null ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["GENDER"]);
                if (ds.Tables[0].Rows[0]["MOBILECODE"].ToString() != string.Empty)
                    ddlOnlineMobileCode.SelectedValue = ds.Tables[0].Rows[0]["MOBILECODE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["MOBILECODE"].ToString();
                txtMobile.Text = ds.Tables[0].Rows[0]["MOBILE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["MOBILE"].ToString();
                TxtACRNo.Text = ds.Tables[0].Rows[0]["ACRNO"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["ACRNO"].ToString();
                txtACRDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["ACR_DATE_ISSUE"].ToString());
                txtHomeTel.Text = ds.Tables[0].Rows[0]["HOME_MOBILENO"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["HOME_MOBILENO"].ToString();
            }

            DataSet ds1 = objnuc.GetAddressDetails(Convert.ToInt32(ViewState["USER_NO"]));
            if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {
                txtPermAddress.Text = ds1.Tables[0].Rows[0]["PADDRESS"].ToString();
                ddlPCon.SelectedValue = ds1.Tables[0].Rows[0]["PCOUNTRY"].ToString();
                ddlPCon_SelectedIndexChanged(new object(), new EventArgs());
                ddlPermanentState.SelectedValue = ds1.Tables[0].Rows[0]["PPROVINCE"].ToString();
                //objCommon.FillDropDownList(ddlPTahsil, "ACD_DISTRICT", "DISTINCT DISTRICTNO", "DISTRICTNAME", "STATENO=" + ddlPermanentState.SelectedValue, "DISTRICTNO");
                ddlPermanentState_SelectedIndexChanged(new object(), new EventArgs());
                ddlPTahsil.SelectedValue = ds.Tables[0].Rows[0]["GCITY"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["GCITY"].ToString();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PersonalandBankdetails.BindData-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    /////////////////////////////////////////////////////// Personal Details End /////////////////////////////////////////////////

    // --------------------------------- UG Education Details Start -----------------------------------//

    protected void ListViewDataBind()
    {
        int UgPg = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "UGPGOT", "USERNO=" + Convert.ToInt32(ViewState["USER_NO"])));
        ddlProgramTypes.SelectedValue = UgPg.ToString();
        int UserNo = Convert.ToInt32(ViewState["USER_NO"]);
        int COUNT = Convert.ToInt32(objCommon.LookUp("ACD_USER_LAST_QUALIFICATION", "COUNT(USERNO)", "USERNO=" + UserNo));
        if (COUNT == 0)
        {
            DataSet ds = objCommon.FillDropDown("ACD_UA_SECTION", "*,'' as SCHOOL_NAME,'' as SCHOOL_ADDRESS,'' as SCHOOL_REGION,'' as YEAR_ATTENDED,'' as SCHOOL_TYPE_NO", "UA_SECTIONNAME", "UA_SECTION>" + Convert.ToInt32(ddlProgramTypes.SelectedValue), "UA_SECTION desc");
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvLevellist.DataSource = ds;
                lvLevellist.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvLevellist);//Set label 
            }
            else
            {
                lvLevellist.DataSource = null;
                lvLevellist.DataBind();
            }
        }
        else
        {
            DataSet ds = objCommon.FillDropDown("ACD_UA_SECTION S INNER JOIN ACD_USER_LAST_QUALIFICATION UL ON (S.UA_SECTION=UL.UGPGOT)", "UA_SECTION", "UA_SECTIONNAME,SCHOOL_NAME,SCHOOL_ADDRESS,SCHOOL_REGION,YEAR_ATTENDED,SCHOOL_TYPE,SCHOOL_TYPE_NO", "UA_SECTION>" + Convert.ToInt32(ddlProgramTypes.SelectedValue) + "AND USERNO=" + UserNo, "UA_SECTION desc");
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvLevellist.DataSource = ds;
                lvLevellist.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvLevellist);//Set label 
                foreach (ListViewDataItem dataitem in lvLevellist.Items)
                {
                    Label lblTypeNo = dataitem.FindControl("lblTypeNo") as Label;
                    DropDownList ddlType = dataitem.FindControl("ddlType") as DropDownList;
                    ddlType.SelectedValue = lblTypeNo.Text;
                }
            }
        }
    }

    // --------------------------------- UG Education Details End -----------------------------------//
    protected void btnPersonalSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            objnu.UserNo = Convert.ToInt32(ViewState["USER_NO"]);

            objnu.FIRSTNAME = txtFirstName.Text.Trim();
            objnu.LASTNAME = txtPerlastname.Text.Trim();
            objnu.MIDDLENAME = txtMiddleName.Text.Trim();
            objnu.GENDER = rdPersonalGender.SelectedValue;
            //  objnu.RELIGION = Convert.ToInt32(ddlReligion.SelectedValue);
            if (Convert.ToDateTime(txtDateOfBirth.Text).Year > 2020 || txtDateOfBirth.Text.Length < 1)
            {
                objCommon.DisplayMessage(this, "Date of Birth is not valid", this.Page);
                return;
            }
            else
            {
                objnu.DOB = Convert.ToDateTime(txtDateOfBirth.Text);
            }

            objnu.NATIONALITY = Convert.ToInt32(rdbQuestion.SelectedValue);
            objnu.MobileCode = ddlOnlineMobileCode.SelectedValue;   //txtmobilecode.Text;
            objnu.MOBILENO = txtMobile.Text.ToString();
            objnu.EMAILID = txtEmail.Text.ToString();
            objnu.PASSPORT = txtPersonalPassprtNo.Text;
            objnu.FATHERNAME = txtFatherName.Text.Trim();
            objnu.MOTHERNAME = txtMothersName.Text;
            int UANO = Convert.ToInt32(Session["userno"]);
            string ACRNo = TxtACRNo.Text;
            string Dateissue = txtACRDate.Text;




            //Start Education Details
            CustomStatus cs = 0;
            int StudyLevel = Convert.ToInt32(ddlProgramTypes.SelectedValue);
            int UserNo = Convert.ToInt32(ViewState["USER_NO"]);
           
                string SectionNo = string.Empty;
                string NameOfSchool = string.Empty;
                string Address = string.Empty;
                string Region = string.Empty;
                string YearAttended = string.Empty;
                string Type = string.Empty;
                string TypeNo = string.Empty;

                foreach (ListViewDataItem dataitem in lvLevellist.Items)
                {
                    Label lblSectionNo = dataitem.FindControl("lblSectionNo") as Label;
                    TextBox txtNameOfSchool = dataitem.FindControl("txtNameOfSchool") as TextBox;
                    TextBox txtAddress = dataitem.FindControl("txtAddress") as TextBox;
                    TextBox txtRegion = dataitem.FindControl("txtRegion") as TextBox;
                    TextBox txtYearAttended = dataitem.FindControl("txtYearAttended") as TextBox;
                    DropDownList ddlType = dataitem.FindControl("ddlType") as DropDownList;
                    if (txtNameOfSchool.Text == "" || txtAddress.Text == "" || txtRegion.Text == "" || txtYearAttended.Text == "" || ddlType.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this, "All Field Are Mandatory...", this.Page);
                        return;
                    }
                    SectionNo += lblSectionNo.Text + "$";
                    NameOfSchool += txtNameOfSchool.Text + "$";
                    Address += txtAddress.Text + "$";
                    Region += txtRegion.Text + "$";
                    YearAttended += txtYearAttended.Text + "$";
                    TypeNo += ddlType.SelectedValue + "$";
                    Type += ddlType.SelectedItem.Text + "$";

                }
                String cs2 = objnuc.SubmitPersonalDetails(objnu, UANO, ACRNo, Dateissue, txtPermAddress.Text, Convert.ToInt32(ddlPCon.SelectedValue), Convert.ToInt32(ddlPermanentState.SelectedValue), Convert.ToInt32(ddlPTahsil.SelectedValue));
                cs = (CustomStatus)objnuc.Insert_UpdateUserEdcucationalDetails(UserNo, NameOfSchool, Address, Region, YearAttended, Type, TypeNo, SectionNo);
                if (Convert.ToInt32(cs2) != -99)
                {
                    objCommon.DisplayMessage(this, "Applicant Detail Save Successfully", this.Page);
                    lkuploaddocumnet_Click(new object(), new EventArgs());
                }
        }
        catch (Exception ex)
        { 
        
        }
    }

    protected void btnCanceld_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}