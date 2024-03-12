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

public partial class ACADEMIC_StudentAllDeatils : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
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
                //Page Authorization
                CheckPageAuthorization();

                Page.Title = Session["coll_name"].ToString();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                lblUndertaking.Visible = false;
                lblDoc.Visible = true;
                docUnder.Visible = false;
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                if (Session["usertype"].ToString() != "2")     //Student 
                {
                    ViewState["MINISTYFLAG"] = null;
                    //int userno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DISTINCT USERNO", "IDNO=" + Convert.ToInt32(Session["idno"])));
                    //ViewState["USER_NO"] = userno;
                    //BindInstallment();
                    //GetPreviousReceipt();
                    objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO>0 AND RCPTTYPENO IN(1)", "RECIEPT_CODE");//RCPTTYPENO
                    ddlReceiptType.SelectedIndex = 1;
                    objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO > 0", "DOCUMENTNO");
                    //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER A INNER JOIN ACD_STUDENT B ON (A.SEMESTERNO=B.SEMESTERNO)", "A.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO>0", "A.SEMESTERNO"); // Comment by Roshan on Dated 11-02-2021 for required current to lowest semster of particular student for paying fees.
                    objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO <=(SELECT SEMESTERNO + 1 FROM ACD_STUDENT WITH (NOLOCK) WHERE IDNO= " + Convert.ToInt32(ViewState["stuinfoidno"]) + ") AND SEMESTERNO>0", "SEMESTERNO DESC");
                }
                else
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Not Found.This Page is not for Student Login!!", this.Page);
                }

                if (ViewState["action"] == null)
                    ViewState["action"] = "add";
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
                ViewState["usertype"] = Session["usertype"];
                FillDropDownList();
                //pnlId.Visible = true;
            }
           
        }
        
        divMsg.InnerHtml = string.Empty;
        if (Request.Params["__EVENTTARGET"] != null &&
            Request.Params["__EVENTTARGET"].ToString() != string.Empty)
        {
            if (Request.Params["__EVENTTARGET"].ToString() == "CreateDemand")
                this.CreateDemandForCurrentFeeCriteria();
        }
    }
    private void ShowSearchResults(string searchParams)
    {
        try
        {
            StudentSearch objSearch = new StudentSearch();

            string[] paramCollection = searchParams.Split(',');
            if (paramCollection.Length >= 2)
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

        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentInfoEntryNew.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentInfoEntryNew.aspx");
        }
    }
    protected void ClearAllControls()
    {
        Session["stuinfofullname"] = null; ViewState["stuinfoidno"] = null; ViewState["USER_NO"] = null;
        lblRegNo.Text = ""; lblStudName.Text = ""; lblSex.Text = ""; lblYear.Text = ""; lblBatch.Text = ""; lblSemester.Text = ""; lblMobileNo.Text = ""; lblEmailID.Text = ""; lblPaymentType.Text = "";
        lvRefreesPG.DataSource = null; lvRefreesPG.DataBind(); lvEmploymentHistoryPG.DataSource = null; lvEmploymentHistoryPG.DataBind(); ddlProgramName.SelectedValue = null;
        lvRefrees.DataSource = null; lvRefrees.DataBind(); lvEmploymentHistory.DataSource = null; lvEmploymentHistory.DataBind(); ddlSemester.SelectedValue = null; txtMiddleName.Text = "";
        lvDepositSlip.DataSource = null; lvDepositSlip.DataBind(); lstProgramName.DataSource = null; lstProgramName.DataBind(); txtAmount.Text = ""; lvOfferAccept.DataSource = null; lvOfferAccept.DataBind();
        txtNameInitial.Text = ""; txtFirstName.Text = ""; txtPerlastname.Text = ""; rdbQuestion.SelectedValue = null; txtDateOfBirth.Text = ""; txtFullName.Text = ""; txtMothersName.Text = ""; txtPermPIN.Text = "";
        rdbLeftRight.SelectedValue = null; txtEmail.Text = ""; txtPersonalPassprtNo.Text = ""; txtOnlineNIC.Text = ""; rdPersonalGender.SelectedValue = null; ddlOnlineMobileCode.SelectedValue = null; txtMobile.Text = "";
        txtHomeTel.Text = ""; ddlHomeMobileCode.SelectedValue = null; txtPermAddress.Text = ""; ddlPermanentState.SelectedValue = null; ddlPCon.SelectedValue = null; ddlPTahsil.SelectedValue = null; ddlSubject1.SelectedValue = null;
        ddlSubject2.SelectedValue = null; ddlSubject3.SelectedValue = null; ddlSubject4.SelectedValue = null; ddlGrade1.SelectedValue = null; ddlGrade2.SelectedValue = null; ddlGrade3.SelectedValue = null;
        ddlGrade4.SelectedValue = null; ddlALTypeUG.SelectedValue = null; ddlStreamUG.SelectedValue = null; ddlALPassesUG.SelectedValue = null; ddlOLType.SelectedValue = null; ddlPermanentCity.SelectedValue = null;
        ddlolStream.SelectedValue = null; ddlolpass.SelectedValue = null; olddlsub1.SelectedValue = null; olddlgrade1.SelectedValue = null; olddlsubj2.SelectedValue = null; olddlgrade2.SelectedValue = null;
        olddlsub3.SelectedValue = null; olddlgrade3.SelectedValue = null; olddlsub4.SelectedValue = null; olddlgrade4.SelectedValue = null; olddlsub5.SelectedValue = null; olddlgrade5.SelectedValue = null;
        olddlsub6.SelectedValue = null; olddlgrade6.SelectedValue = null; ddlConCode.SelectedValue = null; txtPMobNo.Text = ""; rdobtnpwd.SelectedValue = null; ddlDisabilityType.SelectedValue = null;
        txtHighestEducationPG.Text = ""; txtUniversityPG.Text = ""; txtQualificationAwardPG.Text = ""; txtSpecializationPG.Text = ""; txtGPAPG.Text = ""; txtProfessionalPG.Text = ""; txtProfessionalUniversityPG.Text = "";
        txtAwardDatePG.Text = ""; txtSpecilizationQualificationPG.Text = ""; rdbQuestion1PHD.SelectedValue = null; txtQuestionDetailsPHD.Text = ""; rdbQuestion2PHD.SelectedValue = null; txtQuestion1DetailsPHD.Text = "";
        txtNameofQualificationPHD.Text = ""; txtYearofAwardPHD.Text = ""; txtUniversityPHD.Text = ""; txtMainSpecialtyPHD.Text = ""; txtGPAPHD.Text = ""; txtNameQualificationPHD.Text = "";
        txtAwardingUniversityPHD.Text = ""; txtAwardDatePHD.Text = ""; txtSpecilizationQualificationPHD.Text = ""; txtDescriptionPHD.Text = ""; ddlModePHD.SelectedValue = null;
        rdPaymentOption.SelectedValue = null;txtAmount.Text = ""; hdfAmount.Value = null; txtchallanAmount.Text = ""; hdfServiceCharge.Value = null; hdfTotalAmount.Value = null; hdnDate.Value = null; 
        txtChallanId.Text = ""; txtchallanAmount.Text = ""; txtTransactionNo.Text = ""; txtPaymentdate.Text = ""; ddlbank.SelectedValue = null; txtBranchName.Text = "";
        lblpaid.Text = ""; lvStudentFees.DataSource = null; lvStudentFees.DataBind(); lblOrderID.Text = ""; hdfAmount.Value = null; hdfServiceCharge.Value = null; hdfTotalAmount.Value = null;
        lblStatus.Text = ""; lvPaidReceipts.DataSource = null; lvPaidReceipts.DataBind(); lblEnroll.Text = ""; lblm.Text = ""; lblName.Text = ""; lblmn.Text = ""; lble.Text = "";
        ddlSchClg.SelectedValue = null; lvDocument.DataSource = null; lvDocument.DataBind(); lvOfferedSubject.DataSource = null; lvOfferedSubject.DataBind(); lvcoursetwo.DataSource = null; lvcoursetwo.DataBind();
        lvcoursethree.DataSource = null; lvcoursethree.DataBind(); lblstatuspayment.Text = ""; lblIntake.Text = ""; lblEnrollmentno.Text = ""; lblNamewithInitial.Text = ""; lblFullName.Text = "";
        lblnicpass.Text = ""; lbladdress.Text = null; lblcontactno.Text = null; lblstudemail.Text = null; lblprogram.Text = ""; lblcampus.Text = ""; lblweekbatch.Text = ""; lbldateofreg.Text = "";
        txtOrderid.Text = ""; txtTotalPayAmount.Text = ""; txtServiceCharge.Text = ""; txtAmountPaid.Text = "";
    }
    private void bindlistserch()
    {

        string categoryy = "";
        //categoryy = "regno";
        StudentController objSC = new StudentController();

        if (rdselect.SelectedValue == "0")
        {
            categoryy = "name";

        }
        else if (rdselect.SelectedValue == "1")
        {
            categoryy = "EMAILID";
        }

        else if (rdselect.SelectedValue == "2")
        {
            categoryy = "regno";
        }
        //else if (rdselect.SelectedValue == "3")
        //{
        //    categoryy = "regno";
        //}
        DataSet ds = objSC.RetrieveStudentDetails(txtSearch.Text, categoryy);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = ds.Tables[0];
            lvStudent.DataBind();

            lvBankDetails.DataSource = null;
            lvBankDetails.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : 0";
        }
        ds.Dispose();
    }
    protected void lnkId_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAllControls();
            LinkButton lnk = sender as LinkButton;
            Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;
            //Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
            Session["stuinfofullname"] = lnk.Text.Trim();
            ViewState["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
            ViewState["USER_NO"] = lnk.ToolTip;
            //txtIDNo.Text = lblenrollno.Text.Trim();
            if (lblenrollno.Text == "" || lblenrollno.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "", this.Page);
                return;
            }
            else
            {
                int userno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DISTINCT USERNO", "IDNO=" + Convert.ToInt32(ViewState["stuinfoidno"])));
                ViewState["USER_NO"] = userno;
                //BindInstallment();
                getuserstatus();
                divacceptance.Attributes.Add("class", "active");
                divlkPayment.Attributes.Remove("class");
                upddetails.Visible = true;
                ShowStudentDetails();
                PopulateDropDown();
                bindlist();
                bindphoto();
                status();
                BindOfferAcceptance();
                fillDetails();
                BindDataOnline();
                BindAddressData();
                //BindListViewData();
                GetPreviousReceipt();
            }
            // LinkButton lnk = sender as LinkButton;
            // string url = string.Empty;
            // if (Request.Url.ToString().IndexOf("&id=") > 0)
            //     url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
            // else
            //     url = Request.Url.ToString();
            // Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;
            // Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
            // Session["stuinfofullname"] = lnk.Text.Trim();
            // ViewState["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
            // ViewState["USER_NO"] = lnk.ToolTip;
            // txtIDNo.Text = ViewState["stuinfoidno"].ToString();
            // //tab.Visible = true;
            // //Response.Redirect("~/academic/StudentAllDetails.aspx");
            //ShowStudentDetails();\
        }
        catch (Exception ex)
        {

        }
    }
    protected void getuserstatus()
    {
        DataSet Program = objdocContr.GetUserStatusFlag(Convert.ToInt32(ViewState["USER_NO"].ToString()), Convert.ToInt32(ViewState["stuinfoidno"]));
        if (Program.Tables[0] != null && Program.Tables[0].Rows.Count > 0)
        {
            ViewState["EducationDetails"] = Program.Tables[0].Rows[0]["EDUCATION_STATUS"].ToString();
        }
        else
        {
            ViewState["EducationDetails"] = "0";
        }
        if (Program.Tables[1] != null && Program.Tables[1].Rows.Count > 0)
        {
            ViewState["DocumentDetails"] = Program.Tables[1].Rows[0]["DOC_STATUS"].ToString();
        }
        else
        {
            ViewState["DocumentDetails"] = "0";
        }
        if (Program.Tables[2] != null && Program.Tables[2].Rows.Count > 0)
        {
            ViewState["RegistrationDetails"] = Program.Tables[2].Rows[0]["REGISTRATION_STATUS"].ToString();
        }
        else
        {
            ViewState["RegistrationDetails"] = "0";
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
            }
            else
            {
                lstProgramName.DataSource = null;
                lstProgramName.DataBind();
                ViewState["MINISTYFLAG"] = null;
            }
            //if (Program.Tables[1] != null && Program.Tables[1].Rows.Count > 0)
            //{
            //    ddlcamous.Items.Clear();
            //    ddlcamous.Items.Add("Please Select");
            //    ddlcamous.SelectedItem.Value = "0";

            //    ddlcamous.DataSource = Program.Tables[1];
            //    ddlcamous.DataValueField = "CAMPUSNO";
            //    ddlcamous.DataTextField = "CAMPUSNAME";
            //    ddlcamous.DataBind();
            //    //ddlcamous.SelectedIndex = 0;
            //}
            if (Program.Tables[1] != null && Program.Tables[1].Rows.Count > 0)
            {
                if (Convert.ToString(Program.Tables[1].Rows[0]["BRANCH_PREF"]) != "0")
                {
                    chkRowsProgram_CheckedChanged(new object(), new EventArgs());
                    ddlcamous.SelectedValue = Convert.ToString(Program.Tables[1].Rows[0]["BRANCH_PREF"]);
                    ddlcamous_SelectedIndexChanged(new object(), new EventArgs());
                }
                if (Convert.ToString(Program.Tables[1].Rows[0]["STUDENT_TABLE"]) != "0")
                {
                    chkRowsProgram_CheckedChanged(new object(), new EventArgs());
                    ddlcamous.SelectedValue = Convert.ToString(Program.Tables[1].Rows[0]["STUDENT_TABLE"]);
                    ddlcamous_SelectedIndexChanged(new object(), new EventArgs());
                    ddlweek.SelectedValue = Convert.ToString(Program.Tables[1].Rows[0]["WEEKSNOS"]);
                    //ddlcamous.Enabled = false;
                    //ddlweek.Enabled = false;
                }
                else
                {
                    ddlcamous.Enabled = true;
                    ddlweek.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void ddlcamous_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            if (ddlcamous.SelectedIndex > 0)
            {
                foreach (ListViewDataItem items in lstProgramName.Items)
                {
                    CheckBox chk = items.FindControl("chkRowsProgram") as CheckBox;
                    HiddenField hdfbranchno = items.FindControl("hdfbranchno") as HiddenField;
                    Label lbldegree = items.FindControl("lbldegree") as Label;
                    Label lblcollegename = items.FindControl("lblcollegename") as Label;
                    Label lblarea = items.FindControl("lblarea") as Label;
                    Label lblCampus = items.FindControl("lblCampus") as Label;

                    if (chk.Checked == true)
                    {
                        count++;
                        objCommon.FillDropDownList(ddlweek, "ACD_ADMISSION_CONFIG C INNER JOIN ACD_CAMPUS_WISE_INTAKE I ON C.UGPG=I.UGPG AND C.COLLEGE_ID = I.COLLEGE_ID AND C.ADMBATCH = I.ADMBATCH AND C.DEGREENO = I.DEGREENO INNER JOIN ACD_BATCH_SLIIT CA ON I.WEEKSNOS = CA.WEEKNO", "DISTINCT I.WEEKSNOS", "WEEKDAYSNAME", "C.UGPG=" + Convert.ToInt32(ViewState["UGPGOTONLINE"]) + " AND C.COLLEGE_ID=" + Convert.ToInt32(lblcollegename.ToolTip.ToString()) + " AND C.ADMBATCH=" + Convert.ToInt32(ViewState["batchno"].ToString()) + " AND C.DEGREENO=" + Convert.ToInt32(lbldegree.ToolTip.ToString()) + " AND I.CAMPUSNO=" + Convert.ToInt32(ddlcamous.SelectedValue), "");
                    }
                }
                if (count == 0)
                {
                    ddlweek.Items.Clear();
                    ddlweek.Items.Add("Please Select");
                    ddlweek.SelectedItem.Value = "0";
                    ddlweek.SelectedIndex = 0;
                }
            }
            //else
            //{
            //    ddlweek.Items.Clear();
            //    ddlweek.Items.Add("Please Select");
            //    ddlweek.SelectedItem.Value = "0";
            //    ddlweek.SelectedIndex = 0;
            //}
        }
        catch (Exception ex)
        {
        }
    }
    //private void BindOfferAcceptance()
    //{
    //    try
    //    {
    //        DataSet Program = objdocContr.getOfferAcceptanceDetails(Convert.ToInt32(ViewState["USER_NO"].ToString()), Convert.ToInt32(3), Convert.ToInt32(0));
    //        //objCommon.FillDropDown("ACD_USER_BRANCH_PREF B INNER JOIN ACD_COLLEGE_MASTER C ON B.COLLEGE_ID = C.COLLEGE_ID INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO INNER JOIN ACD_BRANCH E ON B.BRANCHNO = E.BRANCHNO INNER JOIN ACD_AREA_OF_INTEREST F ON B.AREA_INT_NO = F.AREA_INT_NO INNER JOIN ACD_CAMPUS AC ON (B.CAMPUSNO=AC.CAMPUSNO) LEFT JOIN ACD_AFFILIATED_UNIVERSITY AU ON E.AFFILIATED_NO = AU.AFFILIATED_NO INNER JOIN ACD_USER_REGISTRATION UR ON(B.USERNO=UR.USERNO) LEFT JOIN ACD_DEMAND AD ON(AD.ENROLLNMENTNO=UR.USERNAME AND AD.DEGREENO=B.DEGREENO AND AD.BRANCHNO=B.BRANCHNO AND AD.COLLEGE_ID=B.COLLEGE_ID AND ISNULL(AD.CAN,0)=0 AND ISNULL(AD.DELET,0)=0) LEFT JOIN ACD_DCR_TEMP UCD ON UR.USERNAME = UCD.ENROLLNMENTNO LEFT JOIN ACD_DCR DDA ON (DDA.IDNO = AD.IDNO AND AD.SESSIONNO = DDA.SESSIONNO AND DDA.RECON = 1 AND AD.DEGREENO = DDA.DEGREENO)", "DISTINCT CONVERT(NVARCHAR(10),B.COLLEGE_ID) +','+ CONVERT(NVARCHAR(10),B.DEGREENO) + ',' + CONVERT(NVARCHAR(10),B.BRANCHNO) + ',' + CONVERT(NVARCHAR(10),B.AREA_INT_NO)", "C.CODE + ' - ' + D.DEGREENAME + ' - ' + LONGNAME + ' - ' + F.SHORTNAME,B.USERNO,COLLEGE_NAME, (D.DEGREENAME + ' - ' + LONGNAME) AS PROGRAM_NAME,AREA_INT_NAME,AC.CAMPUSNAME,AFFILIATED_SHORTNAME,B.COLLEGE_ID,B.DEGREENO,B.BRANCHNO,B.AREA_INT_NO,AC.CAMPUSNO,ISNULL(AU.AFFILIATED_NO,0)AS AFFILIATED_NO ,ISNULL(AD.DM_NO,0)AS DM_NO,FORMAT (DEMAND_DATE, 'dd/MM/yyyy') AS DEMAND_DATE,UCD.TEMP_DCR_NO,DDA.RECON", "B.MERITNO IS NOT NULL AND MERITNO != '' AND B.USERNO=" + (ViewState["USER_NO"].ToString()), "");
    //        if (Program.Tables[0].Rows.Count > 0)
    //        {
    //            lstProgramName.DataSource = Program;
    //            lstProgramName.DataBind();
    //            ViewState["MINISTYFLAG"] = Convert.ToString(Program.Tables[0].Rows[0]["MINISTRY"]);
    //        }
    //        else
    //        {
    //            lstProgramName.DataSource = null;
    //            lstProgramName.DataBind();
    //            ViewState["MINISTYFLAG"] = null;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }


    //}


    private void ShowStudentDetails()
    {
        try
        {
            if (Session["usertype"].ToString() != "2")     //Student 
            {
                idno = Convert.ToInt32(ViewState["stuinfoidno"]);
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
                    //lblDegree.Text = dr["DEGREENAME"].ToString() == string.Empty ? string.Empty : dr["DEGREENAME"].ToString();
                    //lblBranchs.Text = dr["BRANCH_NAME"].ToString() == string.Empty ? string.Empty : dr["BRANCH_NAME"].ToString();
                    lblYear.Text = dr["YEARNAME"].ToString() == string.Empty ? string.Empty : dr["YEARNAME"].ToString();
                    lblSemester.Text = dr["SEMESTERNAME"].ToString() == string.Empty ? string.Empty : dr["SEMESTERNAME"].ToString();
                    lblBatch.Text = dr["BATCHNAME"].ToString() == string.Empty ? string.Empty : dr["BATCHNAME"].ToString();
                    lblMobileNo.Text = dr["STUDENTMOBILE"].ToString() == string.Empty ? string.Empty : dr["STUDENTMOBILE"].ToString();
                    lblEmailID.Text = dr["EMAILID"].ToString() == string.Empty ? string.Empty : dr["EMAILID"].ToString();
                    //lblBranchs.ToolTip = dr["BRANCHNO"].ToString();
                    //lblDegree.ToolTip = dr["DEGREENO"].ToString();
                    ViewState["degreeno"] = dr["DEGREENO"].ToString();
                    ViewState["BRANCHNO"] = dr["BRANCHNO"].ToString();
                    ViewState["batchno"] = Convert.ToInt32(dr["ADMBATCH"].ToString());
                    ViewState["semesterno"] = Convert.ToInt32(dr["SEMESTERNO"].ToString());
                    ViewState["paytypeno"] = Convert.ToInt32(dr["PTYPE"].ToString());
                    ViewState["RECIEPT_CODE"] = dr["RECIEPT_CODE"].ToString() == string.Empty ? string.Empty : dr["RECIEPT_CODE"].ToString();
                    ViewState["SESSIONNO"] = dr["SESSIONNO"].ToString() == string.Empty ? string.Empty : dr["SESSIONNO"].ToString();
                    //lblCollege.Text = dr["COLLEGENAME"].ToString() == string.Empty ? string.Empty : dr["COLLEGENAME"].ToString();
                    //hdnCollege.Value = dr["COLLEGE_ID"].ToString();
                    ViewState["COLLEGE_ID"] = dr["COLLEGE_ID"].ToString();
                    ViewState["RECON"] = dr["RECON"].ToString();
                    ///ViewState["HOSTEL_SESSIONNO"] = objCommon.LookUp("ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "FLOCK = 1");
                    ViewState["NICPASS"] = dr["NICPASS"].ToString();
                    ViewState["UGPGOTONLINE"] = dr["UGPGOT"].ToString();
                    ///ViewState["HOSTEL_SESSIONNO"] = objCommon.LookUp("ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "FLOCK = 1");
                    lblnicpass.Text = dr["NICPASS"].ToString() == string.Empty ? string.Empty : dr["NICPASS"].ToString();
                    lblcontactno.Text = dr["STUDENTMOBILE"].ToString() == string.Empty ? string.Empty : dr["STUDENTMOBILE"].ToString();
                    lblstudemail.Text = dr["EMAILID"].ToString() == string.Empty ? string.Empty : dr["EMAILID"].ToString();
                    lbladdress.Text = dr["PADDRESS"].ToString() == string.Empty ? string.Empty : dr["PADDRESS"].ToString();
                    lblprogram.Text = dr["PROGRAM"].ToString() == string.Empty ? string.Empty : dr["PROGRAM"].ToString();
                    lblcampus.Text = dr["CAMPUSNAME"].ToString() == string.Empty ? string.Empty : dr["CAMPUSNAME"].ToString();
                    lblweekbatch.Text = dr["WEEKDAYSNAME"].ToString() == string.Empty ? string.Empty : dr["WEEKDAYSNAME"].ToString();
                    lbldateofreg.Text = dr["REGDATE"].ToString() == string.Empty ? string.Empty : dr["REGDATE"].ToString();
                    //lblorigp.Text = dr["PADDRESS"].ToString() == string.Empty ? string.Empty : dr["PADDRESS"].ToString();
                    //lblsliitemail.Text = dr["PADDRESS"].ToString() == string.Empty ? string.Empty : dr["PADDRESS"].ToString();


                    /// Add By Roshan Pannase 17-02-2022
                    lblIntake.Text = dr["BATCHNAME"].ToString() == string.Empty ? string.Empty : dr["BATCHNAME"].ToString();
                    lblEnrollmentno.Text = dr["REGNO"].ToString() == string.Empty ? string.Empty : dr["REGNO"].ToString();
                    lblNamewithInitial.Text = dr["NAME_INITIAL"].ToString() == string.Empty ? string.Empty : dr["NAME_INITIAL"].ToString();
                    lblFullName.Text = dr["STUDNAME"].ToString() == string.Empty ? string.Empty : dr["STUDNAME"].ToString();

                    string payment = objCommon.LookUp("ACD_DCR", "(isnull(SESSIONNO,0))", "IDNO=" + Convert.ToInt32(ViewState["stuinfoidno"]) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + " AND RECIEPT_CODE=" + "'" + Convert.ToString(ddlReceiptType.SelectedValue) + "'");
                    //if (payment != "")
                    //{
                    //    btnShowDetails.Enabled = false;
                    //}
                    //else
                    //{
                    //    btnShowDetails.Enabled = true;
                    //}


                    if (dsStudent.Tables[0].Rows[0]["DM_NO"].ToString() != string.Empty)
                    {
                        // btnShowDetails.Enabled = false;
                        BindOfferAcceptance();

                        lvOfferAccept.DataSource = dsStudent;
                        lvOfferAccept.DataBind();
                        Session["Offer_Accept"] = "1";
                        lvOfferAccept.Visible = true;
                        divacceptance.Attributes.Add("class", "finished");
                    }
                    else
                    {
                        lvOfferAccept.DataSource = null;
                        lvOfferAccept.DataBind();
                        Session["Offer_Accept"] = null;
                        lvOfferAccept.Visible = false;
                        divacceptance.Attributes.Remove("class");
                    }
                    //if (status == 1)
                    //{
                    //    lblStatus.Text = "approved.";
                    //}
                    //else if (status == 2)
                    //{
                    //    if (txtRemark.Text != string.Empty || txtRemark.Text != "")
                    //    {
                    //        lblStatus.Text = "rejected";
                    //    }
                    //    else
                    //    {
                    //        lblStatus.Text = "rejected";
                    //    }
                    //}
                    //else if (status == 3)
                    //{                      
                    //        lblStatus.Text = "kept on hold";                        
                    //}


                }
                else
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Not Found.", this.Page);
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.Page, "Record Not Found.This Page is not for Student Login!!", this.Page);
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
        DataSet ds = feeController.GetPaidReceiptsInfoByStudId_FORPAYMENT(Convert.ToInt32(ViewState["stuinfoidno"]));
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

    #region After Selection

    protected void CheckPrevDemand()
    {
        btnSubmit.Visible = false; btnCancel.Visible = false; pnlStudentsFees.Visible = false; TRAmount.Visible = false; TRNote.Visible = false;
        //objCommon.DisplayUserMessage(updBulkReg, "Demand Not Created.", this.Page);
        //ScriptManager.RegisterClientScriptBlock(updBulkReg, updBulkReg.GetType(), "Message", "('" + if(confirm('No demand found for semester " + ddlSemester.SelectedItem.Text + ".\\nDo you want to create demand for this semester?'))" + "');", true);

        if (ddlReceiptType.SelectedValue != "HF")
        {

            //string payment = objCommon.LookUp("ACD_DCR", "(isnull(SESSIONNO,0))", "IDNO=" + Convert.ToInt32(ViewState["stuinfoidno"]) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + " AND RECIEPT_CODE=" + "'" + Convert.ToString(ddlReceiptType.SelectedValue) + "'");
            //if (payment != "")
            //{
            //    objCommon.DisplayUserMessage(this.Page, "Payment of this semester and session Already done..!!!", this.Page);
            //    return;
            //}
            //else
            //{
                this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'>";
                //this.divMsg.InnerHtml += " if(confirm('No demand found for semester " + lblSemester.Text + ".\\nDo you want to create demand for this semester?'))";

                this.divMsg.InnerHtml += " if(confirm('Do you want to confirm?'))";
                this.divMsg.InnerHtml += "{__doPostBack('CreateDemand', '');}</script>";
            //}

            //this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'>";
            //this.divMsg.InnerHtml += " if(confirm('No demand found for semester " + lblSemester.Text + ".\\nDo you want to create demand for this semester?'))";
            //this.divMsg.InnerHtml += "{__doPostBack('CreateDemand', '');}</script>";
        }
        else
        {
            objCommon.DisplayUserMessage(this.Page, "No Room Alloted!!!", this.Page);
        }
        //objCommon.DisplayUserMessage(updBulkReg, "Demand Not Created.", this.Page);
        //ddlReceiptType.SelectedValue = "0";
        //ViewState["semesterno"] = "0";
        //btnSubmit.Enabled = false;
        //lvStudentFees.DataSource = null;
        //lvStudentFees.DataBind();
        //lblStatus.Visible = false;

    }

    private void CreateDemand(FeeDemand feeDemand, int paymentTypeNoOld)
    {
        try
        {
            if (Convert.ToInt32(ddlcamous.SelectedValue) > 0)
            {
                ViewState["CAMPUSNO"] = Convert.ToString(ddlcamous.SelectedValue);
            }
            if (feeController.CreateOnlineAdmissionNewDemand(feeDemand, paymentTypeNoOld, Convert.ToInt32(ViewState["CAMPUSNO"].ToString()), Convert.ToInt32(ddlweek.SelectedValue)))
            {
                this.PopulateFeeItemsSection(feeDemand.SemesterNo);
                btnSubmit.Visible = true;
                objCommon.DisplayMessage(this.Page, "Offer Acceptance done successfully", this.Page);
                //  btnShowDetails.Enabled = false;
                Session["Offer_Accept"] = "1";
                lnkOnlineDetails_Click(new object(), new EventArgs());
            }
            else
            {
                objCommon.DisplayUserMessage(updBulkReg, "Standard Fees Not Created", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.btnCreateDemand_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CreateDemandForCurrentFeeCriteria()
    {
        try
        {
            FeeDemand feeDemand = new FeeDemand();
            feeDemand.StudentId = Convert.ToInt32(ViewState["stuinfoidno"]);
            feeDemand.StudentName = lblStudName.Text;
            feeDemand.EnrollmentNo = lblRegNo.Text;
            //int examType = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "FLOCK=1"));
            if (GetViewStateItem("ReceiptType") == "HF")
            {
                feeDemand.SessionNo = Convert.ToInt32(ViewState["HOSTEL_SESSIONNO"]);
            }
            else
            {
                feeDemand.SessionNo = Convert.ToInt32(Session["currentsession"].ToString());

            }
            //if (examType == 1)
            //{
            //feeDemand.SessionNo = Convert.ToInt32(Session["currentsession"]);
            //}
            //else
            //{
            //    feeDemand.SessionNo = Convert.ToInt32(Session["currentsession"]) + 1;
            //}

            // feeDemand.SessionNo = ((GetViewStateItem("SessionNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("SessionNo")) : 0);
            //string[] Split = ddlProgramName.SelectedValue.Split(',');
            //feeDemand.College_ID = Convert.ToInt32(Split[0]);
            //feeDemand.DegreeNo = Convert.ToInt32(Split[1]);
            //feeDemand.BranchNo = Convert.ToInt32(Split[2]);
            //feeDemand.Interest = Convert.ToInt32(Split[3]);

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
            feeDemand.CollegeCode = txtRemark.Text;
            int paymentTypeNoOld = 1;

            this.CreateDemand(feeDemand, paymentTypeNoOld);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.CreateDemandForCurrentFeeCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
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

    private void PopulateFeeItemsSection(int semesterNo)
    {
        try
        {
            DataSet ds = null;
            int status = 0;
            if (ddlReceiptType.SelectedIndex > 0)
            {
                // if (ddlProgramName.SelectedIndex > 0)
                if (ViewState["DEGREENO"].ToString() != null || ViewState["DEGREENO"].ToString() != "")
                {

                    session = objCommon.LookUp("ACD_DEMAND", "(isnull(SESSIONNO,0))", "IDNO=" + Convert.ToInt32(ViewState["stuinfoidno"]) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + " AND ISNULL(CAN,0) = 0 AND ISNULL(DELET,0) = 0 AND PAYTYPENO =" + Convert.ToInt32(ViewState["paytypeno"]) + " AND ADMBATCHNO=" + Convert.ToInt32(ViewState["batchno"]) + "and RECIEPT_CODE=" + "'" + Convert.ToString(ddlReceiptType.SelectedValue) + "'");
                    if (session != "")
                    {
                        ds = feeController.GetFeeItems_Data_ForOnlinePayment(Convert.ToInt32(session), Convert.ToInt32(ViewState["stuinfoidno"]), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(ddlReceiptType.SelectedValue), Convert.ToInt32(ViewState["paytypeno"]), Convert.ToInt32(ViewState["batchno"]));//, ref status);
                    }
                    else
                    {
                        CheckPrevDemand();
                    }

                    if (status != -99 && ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        btnSubmit.Visible = true;
                        btnSubmit.Enabled = true;
                        btnCancel.Visible = true; pnlStudentsFees.Visible = true;
                        //TRSPayOption.Visible = true; 
                        TRNote.Visible = true;
                        //TRSCardType.Visible = true;
                        lvStudentFees.DataSource = ds;
                        lvStudentFees.DataBind();

                    }

                    //else
                    //{
                    //    CheckPrevDemand();
                    //}
                }


                else
                {
                    btnSubmit.Visible = false; btnCancel.Visible = false; pnlStudentsFees.Visible = false; TRAmount.Visible = false;
                    //TRSPayOption.Visible = false; 
                    TRNote.Visible = false;
                    objCommon.DisplayUserMessage(this.Page, "Please Select Program Name.", this.Page);
                    ddlReceiptType.Focus();
                    lblStatus.Visible = false;
                }
            }


            else
            {
                btnSubmit.Visible = false; btnCancel.Visible = false; pnlStudentsFees.Visible = false; TRAmount.Visible = false;
                //TRSPayOption.Visible = false; 
                TRNote.Visible = false;
                objCommon.DisplayUserMessage(this.Page, "Please Select Receipt Type.", this.Page);
                ddlReceiptType.Focus();
                lblStatus.Visible = false;
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


    protected void lvStudentFees_PreRender(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            session = objCommon.LookUp("ACD_DEMAND", "(isnull(SESSIONNO,0))", "IDNO=" + Convert.ToInt32(ViewState["stuinfoidno"]) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + " AND PAYTYPENO =" + Convert.ToInt32(ViewState["paytypeno"]) + " AND ISNULL(CAN,0) = 0 AND ISNULL(DELET,0) = 0 AND ADMBATCHNO=" + Convert.ToInt32(ViewState["batchno"]) + "and RECIEPT_CODE=" + "'" + Convert.ToString(ddlReceiptType.SelectedValue) + "'");
            if (session != "")
            {
                ds = feeController.GetFeeItems_Data_ForOnlinePayment(Convert.ToInt32(session), Convert.ToInt32(ViewState["stuinfoidno"]), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(ddlReceiptType.SelectedValue), Convert.ToInt32(ViewState["paytypeno"]), Convert.ToInt32(ViewState["batchno"]));//, ref status);
            }
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
        try
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblAmount = e.Item.FindControl("lblAmount") as Label;
                TotalSum += Convert.ToDecimal(lblAmount.Text);
                ViewState["demandamounttobepaid"] = TotalSum;
            }
        }
        catch (Exception ex)
        { 
        
        }
    }

    #endregion

    #region Regarding Submition of data and Online Payment

    protected void btnSubmit_Click(object sender, EventArgs e)   ////Student Payment Click Button
    {
        try
        {


            if (txtAmount.Text == "0" || txtAmount.Text == "" || txtAmount.Text == "0.00")
            {
                objCommon.DisplayMessage(updBulkReg, "Total Amount Cannot be Less than or Equal to Zero !!", this.Page);
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

    private void SubmitData()
    {
        try
        {

            CreateCustomerRef();
            string session = string.Empty;
            session = GetSession();

            int result = 0;
            int DM_NO = 0;
            DM_NO = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "DM_NO", "IDNO=" + Convert.ToInt32(ViewState["stuinfoidno"]) + " AND ISNULL(CAN,0) = 0 AND ISNULL(DELET,0) = 0 AND SESSIONNO=" + Convert.ToInt32(session) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + "and RECIEPT_CODE=" + "'" + Convert.ToString(ddlReceiptType.SelectedValue) + "'"));
            if (DM_NO > 0)
            {
                if (ddlReceiptType.SelectedValue == "HF")
                {
                    // result = feeController.InsertOnlinePayment_DCR(DM_NO, Convert.ToInt32(ViewState["stuinfoidno"]), Convert.ToInt32(session), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(lblOrderID.Text), 1);//, Convert.ToString(ViewState["Amount"])//, APTRANSACTIONID
                    result = feeController.InsertOnlinePayment_DCR(DM_NO, Convert.ToInt32(ViewState["stuinfoidno"]), Convert.ToInt32(ViewState["SESSIONNO"]), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(lblOrderID.Text), 1, Convert.ToInt32(1), Convert.ToString(ViewState["INSTALL_NO"]));
                }
                else
                {
                    // result = feeController.InsertOnlinePayment_DCR(DM_NO, Convert.ToInt32(ViewState["stuinfoidno"]), Convert.ToInt32(session), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(lblOrderID.Text), 1);//, Convert.ToString(ViewState["Amount"])//, APTRANSACTIONID
                    result = feeController.InsertOnlinePayment_DCR(DM_NO, Convert.ToInt32(ViewState["stuinfoidno"]), Convert.ToInt32(session), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(lblOrderID.Text), 1, Convert.ToInt32(1), Convert.ToString(ViewState["INSTALL_NO"]));
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
                ds = feeController.GetOnlineTrasactionOnlineOrderID(Convert.ToInt32(ViewState["stuinfoidno"]), Convert.ToString(lblOrderID.Text));

                if (ds.Tables[0].Rows.Count > 0)
                {

                    if (Convert.ToString(ds.Tables[0].Rows[0]["ORDER_ID"]) == lblOrderID.Text)
                    {

                        SendTransaction();
                        txtOrderid.Text = lblOrderID.Text;
                        txtAmountPaid.Text = hdfAmount.Value;
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
            string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
            //url += "Reports/CommonReport.aspx?";
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
        /// This report requires nine parameters. 
        /// Main report takes three params and three subreport takes two
        /// params each. Each subreport takes a pair of DCR_NO and ID_NO as parameter.
        /// Main report takes one extra param i.e. copyNo. copyNo is used to specify whether
        /// the receipt is a original copy(value=1) OR duplicate copy(value=2)
        /// ADD THE PARAMETER COLLEGE CODE
        /// 

        //string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        //param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt";
        //param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt-01" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt-01";
        //param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt-02" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt-02";
        //return param;


        string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;


    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ////SubmitData();//Session["currentsession"]

        int DCR_NO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "MAX(DCR_NO)", "IDNO=" + Convert.ToInt32(ViewState["stuinfoidno"]) + "AND SESSIONNO=" + Convert.ToInt32(ViewState["SESSIONNO"]) + " AND  SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + "AND RECIEPT_CODE='TF'"));

        if (ViewState["COLLEGE_ID"] == "11" || ViewState["COLLEGE_ID"] == "12" || ViewState["COLLEGE_ID"] == "13")
        {
            this.ShowReport("FeeCollectionReceipt-SVIM.rpt", DCR_NO, Convert.ToInt32(ViewState["stuinfoidno"]), "1");
        }
        else if (ViewState["COLLEGE_ID"] == "10")
        {
            this.ShowReport("FeeCollectionReceipt-SVITS.rpt", DCR_NO, Convert.ToInt32(ViewState["stuinfoidno"]), "1");
        }
        else
        {
            this.ShowReport("FeeCollectionReceipt.rpt", DCR_NO, Convert.ToInt32(ViewState["stuinfoidno"]), "1");
        }
    }

    protected void btnPrintReceipt_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnPrint = sender as ImageButton;

        int DCR_NO = Convert.ToInt32(btnPrint.CommandArgument.ToString());

        string recipt_code = Convert.ToString(objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "DCR_NO = " + DCR_NO + ""));

        this.ShowReport("FeeCollectionReceipt.rpt", DCR_NO, Convert.ToInt32(ViewState["stuinfoidno"]), "1");

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
                session = objCommon.LookUp("ACD_DEMAND", "(isnull(SESSIONNO,0))", "IDNO=" + Convert.ToInt32(ViewState["stuinfoidno"]) + " AND ISNULL(CAN,0) = 0 AND ISNULL(DELET,0) = 0 AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + " AND PAYTYPENO =" + Convert.ToInt32(ViewState["paytypeno"]) + " AND ADMBATCHNO=" + Convert.ToInt32(ViewState["batchno"]) + "and RECIEPT_CODE=" + "'" + Convert.ToString(ddlReceiptType.SelectedValue) + "'");
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
        // lblOrderID.Text = Convert.ToString(Convert.ToInt32(ViewState["stuinfoidno"]) + Convert.ToInt32(ViewState["SESSIONNO"]) + Convert.ToInt32(ViewState["semesterno"]) + ir);

        lblOrderID.Text = Convert.ToString(Convert.ToInt32(ViewState["stuinfoidno"]) + Convert.ToString(ViewState["SESSIONNO"]) + Convert.ToString(ViewState["semesterno"]) + ir);
        txtOrderid.Text = lblOrderID.Text;
        Session["ERPORDERIDRESPONSE"] = lblOrderID.Text;
        ViewState["RANDOM_NUMBER"] = ir;
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

                String cs = objnuc.SUBMITOFEERDETAILS(Convert.ToString(lblStudName.Text), Convert.ToString(lblEmailID.Text), Convert.ToString(lblSex.Text), Convert.ToString(lblMobileNo.Text), Convert.ToInt32(ViewState["USER_NO"]));
                foreach (ListViewDataItem items in lstProgramName.Items)
                {
                    CheckBox chk = items.FindControl("chkRowsProgram") as CheckBox;
                    HiddenField hdfbranchno = items.FindControl("hdfbranchno") as HiddenField;
                    Label lbldegree = items.FindControl("lbldegree") as Label;
                    Label lblcollegename = items.FindControl("lblcollegename") as Label;
                    Label lblarea = items.FindControl("lblarea") as Label;
                    Label lblCampus = items.FindControl("lblCampus") as Label;

                    if (chk.Checked == true)
                    {
                        count++;
                        int status = 0;
                        if (ddlReceiptType.SelectedIndex > 0)
                        {
                            ViewState["COLLEGE_ID"] = lblcollegename.ToolTip;
                            ViewState["DEGREENO"] = lbldegree.ToolTip;
                            ViewState["BRANCHNO"] = hdfbranchno.Value;
                            ViewState["INTERESTNO"] = lblarea.ToolTip;
                            ViewState["CAMPUSNO"] = lblCampus.ToolTip;

                            CheckPrevDemand();


                            //session = objCommon.LookUp("ACD_DEMAND", "(isnull(SESSIONNO,0))", "IDNO=" + Convert.ToInt32(ViewState["stuinfoidno"]) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + " AND PAYTYPENO =" + Convert.ToInt32(ViewState["paytypeno"]) + " AND ADMBATCHNO=" + Convert.ToInt32(ViewState["batchno"]) + " AND RECIEPT_CODE=" + "'" + Convert.ToString(ddlReceiptType.SelectedValue) + "'  AND DEGREENO ='" + Convert.ToInt32(lbldegree.ToolTip) + "' AND BRANCHNO = '" + Convert.ToInt32(hdfbranchno.Value) + "'");
                            //if (session != "")
                            //{
                            //    ds = feeController.GetFeeItems_Data_ForOnlinePayment(Convert.ToInt32(session), Convert.ToInt32(ViewState["stuinfoidno"]), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(ddlReceiptType.SelectedValue), Convert.ToInt32(ViewState["paytypeno"]), Convert.ToInt32(ViewState["batchno"]));//, ref status);
                            //}
                            //if (status != -99 && ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            //{

                            //    TRNote.Visible = true;
                            //    //TRSCardType.Visible = true;
                            //    lvStudentFees.DataSource = ds;
                            //    lvStudentFees.DataBind();
                            //}
                            //else
                            //{
                            //    CheckPrevDemand();
                            //}

                        }
                        else
                        {
                            btnSubmit.Visible = false; btnCancel.Visible = false; pnlStudentsFees.Visible = false; TRAmount.Visible = false;
                            //TRSPayOption.Visible = false; 
                            TRNote.Visible = false;
                            objCommon.DisplayUserMessage(this.Page, "Please Select Receipt Type.", this.Page);
                            ddlReceiptType.Focus();
                            lblStatus.Visible = false;
                        }
                    }
                    else
                    {
                        //objCommon.DisplayUserMessage(updBulkReg, "Please Select Aleast One Program ! ", this.Page);
                    }

                }
                if (count == 0)
                {
                    objCommon.DisplayUserMessage(this.Page, "Please Select Aleast One Program ! ", this.Page);
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

        //    }
        //  }

        //    string[] Split = ddlProgramName.SelectedValue.Split(',');

        //        int status = 0;
        //        if (ddlReceiptType.SelectedIndex > 0)
        //        {
        //            session = objCommon.LookUp("ACD_DEMAND", "(isnull(SESSIONNO,0))", "IDNO=" + Convert.ToInt32(ViewState["stuinfoidno"]) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + " AND PAYTYPENO =" + Convert.ToInt32(ViewState["paytypeno"]) + " AND ADMBATCHNO=" + Convert.ToInt32(ViewState["batchno"]) + " AND RECIEPT_CODE=" + "'" + Convert.ToString(ddlReceiptType.SelectedValue) + "'  AND DEGREENO ='" + Split[1] + "' AND BRANCHNO = '" + Split[2] + "'");
        //                    if (session != "")
        //                    {
        //                        ds = feeController.GetFeeItems_Data_ForOnlinePayment(Convert.ToInt32(session), Convert.ToInt32(ViewState["stuinfoidno"]), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(ddlReceiptType.SelectedValue), Convert.ToInt32(ViewState["paytypeno"]), Convert.ToInt32(ViewState["batchno"]));//, ref status);
        //                    }
        //                if (status != -99 && ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //                {
        //                    //divlkPayment.Attributes.Add("class", "active");
        //                    //divacceptance.Attributes.Remove("class");
        //                    //acceptance.Visible = false;
        //                    //btnSubmit.Visible = true;
        //                    //btnSubmit.Enabled = true;
        //                    //btnCancel.Visible = true; pnlStudentsFees.Visible = true;
        //                    //dipayment.Visible = true;
        //                    //divpayment.Visible = true;
        //                    //TRSPayOption.Visible = true; 
        //                    TRNote.Visible = true;
        //                    //TRSCardType.Visible = true;
        //                    lvStudentFees.DataSource = ds;
        //                    lvStudentFees.DataBind();
        //                }
        //                else
        //                {
        //                    CheckPrevDemand();

        //                }

        //        }
        //        else
        //        {
        //            btnSubmit.Visible = false; btnCancel.Visible = false; pnlStudentsFees.Visible = false; TRAmount.Visible = false;
        //            //TRSPayOption.Visible = false; 
        //            TRNote.Visible = false;
        //            objCommon.DisplayUserMessage(updBulkReg, "Please Select Receipt Type.", this.Page);
        //            ddlReceiptType.Focus();
        //            lblStatus.Visible = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.ddlReceiptType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objCommon.ShowError(Page, "Server UnAvailable");
        //}
    }

    protected void SendTransaction()
    {
        System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)768 | (System.Net.SecurityProtocolType)3072;
        String result = null;
        String gatewayCode = null;
        String response = null;

        // get the request form and make sure to UrlDecode each value in case special characters used
        NameValueCollection formdata = new NameValueCollection();
        //foreach (String key in Request.Form)
        //{
        //    formdata.Add(key, HttpUtility.UrlDecode(Request.Form[key]));
        //}

        Merchant merchant = new Merchant();

        // [Snippet] howToConfigureURL - start
        StringBuilder url = new StringBuilder();
        if (!merchant.GatewayHost.StartsWith("http"))
            url.Append("https://");
        url.Append(merchant.GatewayHost);
        //url.Append("/api/nvp/version/");
        //url.Append(merchant.Version);

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
        //formdata.Add("apiUsername", "merchant.700182200072");
        //formdata.Add("apiPassword", "004dc01e2adfd7ec414ec6255a16e505");
        //formdata.Add("merchant", "700182200072");

        //formdata.Add("interaction.returnUrl", "http://localhost:55158/PresentationLayer/OnlineResponse.aspx");

        string returnurl = System.Configuration.ConfigurationManager.AppSettings["ReturnUrl"];
        formdata.Add("interaction.returnUrl", returnurl);
        // formdata.Add("interaction.returnUrl", "http://localhost:59566/PresentationLayer/OnlineResponse.aspx");

        formdata.Add("interaction.operation", "PURCHASE");
        formdata.Add("order.id", lblOrderID.Text);
        formdata.Add("order.currency", "LKR");
        formdata.Add("order.amount", hdfAmount.Value);
        formdata.Add("order.description", Convert.ToString(ViewState["stuinfoidno"]));
        //url.Append("/apiOperation/");
        //url.Append("CREATE_CHECKOUT_SESSION");
        //url.Append("/apiUsername/");
        //url.Append("merchant.700182200072");
        //url.Append("/apiPassword/");
        //url.Append("004dc01e2adfd7ec414ec6255a16e505");
        //url.Append("/merchant/");
        //url.Append("700182200072");
        //url.Append("interaction.operation");
        //url.Append("/PURCHASE/");
        //url.Append("order.id");
        //url.Append("123456789");
        //url.Append("/order.currency/");
        //url.Append("LKR");
        //url.Append("/order.amount/");
        //url.Append("1000");
        //url.Append("/order.description/");
        //url.Append("\"d2564\"");
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

            // now add the values to result fields in panels
            //lblErrorCode.Text = errorCode;
            //lblErrorMessage.Text = errorMessage;
            //pnlError.Visible = true;
        }

        // error or not display what response values can
        gatewayCode = respValues["response.gatewayCode"];
        if (gatewayCode == null)
        {
            gatewayCode = "Response not received.";
        }
        //lblGateWayCode.Text = gatewayCode;
        //lblResult.Text = result;

        // build table of NVP results and add to panel for results

        int shade = 0;
        foreach (String key in respValues)
        {

            if (key == "session.id")
            {
                Session["ERPPaymentSession"] = respValues[key];
            }

        }
    }
    protected void lkpayment_Click(object sender, EventArgs e)
    {
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
        //BindInstallment();
        if (Convert.ToString(Session["Offer_Accept"]) == "1")
        {
            divacceptance.Attributes.Add("class", "finished");
            dipayment.Visible = true;
            divpayment.Visible = true;
            divViewpayment.Visible = false;
            divShowPay.Visible = false;
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
        if (Convert.ToInt32(ViewState["EducationDetails"]) > 0)
        {
            divOnlineDetails.Attributes.Remove("class");
            divacceptance.Attributes.Add("class", "finished");
        }
        else
        {
            divOnlineDetails.Attributes.Remove("class");
        }
        if (Convert.ToInt32(ViewState["DocumentDetails"]) > 0)
        {
            divlkuploaddocument.Attributes.Remove("class");
            divlkuploaddocument.Attributes.Add("class", "finished");
        }
        else
        {
            divlkuploaddocument.Attributes.Remove("class");
        }
        if (Convert.ToInt32(ViewState["RegistrationDetails"]) > 0)
        {
            divlkModuleOffer.Attributes.Remove("class");
            divlkModuleOffer.Attributes.Add("class", "finished");
        }
        else
        {
            divlkModuleOffer.Attributes.Remove("class");
        }
        //GetPreviousReceipt();
    }


    //tab 2


    protected void ddlSchClg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO > 0", "DOCUMENTNO");
            string[] branchpref = ddlSchClg.SelectedValue.Split('.');
            UploadDocument();
            int RecCount = Convert.ToInt32(objCommon.LookUp("ACD_USER_BRANCH_PREF", "count(*)", " Degreeno=" + branchpref[0] + "AND COLLEGE_ID=" + branchpref[1] + " AND ISNULL(BRANCHNO,0) =" + branchpref[2] + " AND  ISNULL(APPLIED,0)=1 AND USERNO=" + Session["username"] + ""));
            if (RecCount > 0)
            {
                //ShowMessage("You are already applied for selected Degree !!!");
                ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('You are already applied for selected Degree !!!');", true);
                ddlSchClg.SelectedIndex = 0;
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "CourseWise_Registration.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void status()
    {
        DataSet status = objCommon.FillDropDown("ACD_STUDENT", "DISTINCT ISNULL(CONVERT(INT, CAN),0)  AS CAN", "CONVERT(INT,ADMCAN)AS ADMCAN ", "IDNO='" + ViewState["stuinfoidno"] + "'", "");

        if (status.Tables[0].Rows.Count > 0)
        {
            if (status.Tables[0].Rows[0]["CAN"].ToString() == "0" && status.Tables[0].Rows[0]["ADMCAN"].ToString() == "0")
            {
                lblstatuspayment.Text = "Approved";
                btnSummarySheet.Visible = true;
                //divlkstatus.Attributes.Add("class", "finished");
            }
            //if (status.Tables[0].Rows[0]["STATUS"].ToString() == "2")
            //{
            //    lblstatuspayment.Text = "Reject";
            //}
            if (status.Tables[0].Rows[0]["CAN"].ToString() == "1" || status.Tables[0].Rows[0]["ADMCAN"].ToString() == "1")
            {
                lblstatuspayment.Text = "On Hold";
                btnSummarySheet.Visible = false;
            }
        }
    }
    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlbank, "ACD_BANK", "DISTINCT BANKNO", "BANKNAME", "BANKNO > 0", "BANKNO");
            objCommon.FillDropDownList(ddlDisabilityType, "ACD_PHYSICAL_HANDICAPPED", "HANDICAP_NO", "HANDICAP_NAME", "HANDICAP_NO > 0", "HANDICAP_NAME");
            objCommon.FillDropDownList(ddlOnlineMobileCode, "ACD_COUNTRY", "DISTINCT COUNTRYNO", "(MOBILE_CODE + ' - ' + COUNTRYNAME)COUNTRYNAME", "COUNTRYNO > 0", "COUNTRYNO");
            objCommon.FillDropDownList(ddlHomeMobileCode, "ACD_COUNTRY", "DISTINCT COUNTRYNO", "(MOBILE_CODE + ' - ' + COUNTRYNAME)COUNTRYNAME", "COUNTRYNO > 0", "COUNTRYNO");
            objCommon.FillDropDownList(ddlConCode, "ACD_COUNTRY", "DISTINCT COUNTRYNO", "(MOBILE_CODE + ' - ' + COUNTRYNAME)COUNTRYNAME", "COUNTRYNO > 0", "COUNTRYNO");
            objCommon.FillDropDownList(ddlPCon, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO>0", "COUNTRYNAME");
            objCommon.FillDropDownList(ddlPermanentState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
            objCommon.FillDropDownList(ddlPermanentCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITY");
            objCommon.FillDropDownList(ddlPerContry, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO>0", "COUNTRYNAME");
            objCommon.FillDropDownList(ddlPerProvince, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
            DataSet dsinfo = objCommon.FillDropDown("ACD_NEWUSER_REGISTRATION A INNER JOIN ACD_USER_REGISTRATION B ON (A.USERNO=B.USERNO) ", "A.FIRSTNAME", "A.MOTHERNAME ,B.USERNAME, A.MOBILE ,A.EMAILID", "B.USERNAME = '" + Session["username"] + "'", "");
            if (dsinfo.Tables[0].Rows.Count > 0)
            {
                lblEnroll.Text = dsinfo.Tables[0].Rows[0]["USERNAME"].ToString();
                lblName.Text = dsinfo.Tables[0].Rows[0]["FIRSTNAME"].ToString();
                //lblf.Text = dsinfo.Tables[0].Rows[0]["FATHERNAME"].ToString();
                lblm.Text = dsinfo.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                lblmn.Text = dsinfo.Tables[0].Rows[0]["MOBILE"].ToString();
                lble.Text = dsinfo.Tables[0].Rows[0]["EMAILID"].ToString();
                Session["USERNAME"] = dsinfo.Tables[0].Rows[0]["USERNAME"].ToString();

            }




            int userno = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + Session["username"].ToString() + "'"));
            //int CheckMerit = Convert.ToInt32(objCommon.LookUp("ACD_ONLINE_USER_UPLOAD", "COUNT(*)", "ISNULL(MERIT_STATUS,0)=0 AND USERNAME='" + Session["username"] + "'"));
            int CheckMerit = Convert.ToInt32(objCommon.LookUp("ACD_USER_BRANCH_PREF", "ISNULL(COUNT(MERITNO),0) MERIT_COUNT", "USERNO=" + userno + " AND (MERITNO IS NOT NULL)"));
            if (CheckMerit > 0)
            {
                //objCommon.FillDropDownList(ddlSchClg, "ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID  LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO  LEFT JOIN ACD_PREFERENCE P ON P.PREFERENCE_ID = UR.BRANCH_PREF INNER JOIN ACD_ONLINE_USER_UPLOAD UP ON (UP.USERNAME=UR.USERNO  AND UP.COLLEGE_ID=UR.COLLEGE_ID AND UP.DEGREENO=UR.DEGREENO AND SGM.SUGNO=ISNULL(UP.SUBGROUPNO,0)) INNER JOIN ACD_FEES_LOG FE ON (FE.USERNO=UR.USERNO AND FE.DEGREENO=UR.DEGREENO AND FE.COLLEGE_ID=UR.COLLEGE_ID AND ISNULL(FE.BRANCHNO,0)= ISNULL(UR.BRANCHNO,0)) ", "DISTINCT convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE '' END  DEGREENAME", " ISNULL(MERIT_STATUS,0)=0 AND ISNULL(FREEZE,0)<>1 AND D.DEGREENO >0 AND UP.USERNAME =" + Session["username"] + "", "");
                //objCommon.FillDropDownList(ddlSchClg, "ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID  LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO  LEFT JOIN ACD_PREFERENCE P ON P.PREFERENCE_ID = UR.BRANCH_PREF INNER JOIN ACD_ONLINE_USER_UPLOAD UP ON (UP.USERNAME=UR.USERNO  AND UP.COLLEGE_ID=UR.COLLEGE_ID AND UP.DEGREENO=UR.DEGREENO AND SGM.SUGNO=ISNULL(UP.SUBGROUPNO,0)) INNER JOIN ACD_FEES_LOG FE ON (FE.USERNO=UR.USERNO AND FE.DEGREENO=UR.DEGREENO AND FE.COLLEGE_ID=UR.COLLEGE_ID ) ", "DISTINCT convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE '' END  DEGREENAME", " ISNULL(MERIT_STATUS,0)=0 AND ISNULL(FREEZE,0)<>1 AND D.DEGREENO >0 AND UP.USERNAME =" + Session["username"] + "", "");

                //objCommon.FillDropDownList(ddlSchClg, "ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID  LEFT JOIN ACD_BRANCH SGM ON UR.BRANCHNO = SGM.BRANCHNO  LEFT JOIN ACD_PREFERENCE P ON P.PREID = UR.BRANCH_PREF INNER JOIN ACD_ONLINE_USER_UPLOAD UP ON (UP.USERNAME=UR.USERNO  AND UP.COLLEGE_ID=UR.COLLEGE_ID AND UP.DEGREENO=UR.DEGREENO AND SGM.branchno=ISNULL(UP.SUBGROUPNO,0)) INNER JOIN ACD_FEES_LOG FE ON (FE.USERNO=UR.USERNO AND FE.DEGREENO=UR.DEGREENO AND FE.COLLEGE_ID=UR.COLLEGE_ID ) ", "DISTINCT convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE LONGNAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE '' END  DEGREENAME", " ISNULL(MERIT_STATUS,0)=0 AND ISNULL(FREEZE,0)<>1 AND D.DEGREENO >0 AND UP.USERNAME ='" + Session["username"] + "'", "");
                objCommon.FillDropDownList(ddlSchClg, "ACD_USER_BRANCH_PREF BP INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON(BP.COLLEGE_ID=DB.COLLEGE_ID AND BP.DEGREENO=DB.DEGREENO) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=BP.DEGREENO)", "DISTINCT BP.DEGREENO", "DEGREENAME", "MERITNO IS NOT NULL and USERNO=" + userno + "", "BP.DEGREENO");
            }



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "CourseWise_Registration.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {

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
            lblfile.Text = "For Image Only formats Are Allowed : png,jpg,jpeg, For Remaining PDF Only";
            DataSet ds1 = objCommon.FillDropDown("ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO LEFT JOIN ACD_PREFERENCE P ON P.PREID = UR.BRANCH_PREF ", "UR.DEGREENO,UR.COLLEGE_ID,UR.BRANCHNO,UR.BRANCH_PREF", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE '' END  DEGREENAME",
                "isnull(Applied,0)<>0 AND isnull(DocUndertaking,0) <>0 AND UR.USERNO =" + ViewState["USER_NO"] + "", "D.DEGREENAME");
            if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {
                int DEGREE = Convert.ToInt32(ds1.Tables[0].Rows[0]["DEGREENO"]);
                int clg = Convert.ToInt32(ds1.Tables[0].Rows[0]["COLLEGE_ID"]);
                int branch = Convert.ToInt32(ds1.Tables[0].Rows[0]["BRANCHNO"]);
                int pref = Convert.ToInt32(ds1.Tables[0].Rows[0]["BRANCH_PREF"]);

                //objCommon.FillDropDownList(ddlSchClg, "ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID", "convert(varchar(10),UR.COLLEGE_ID) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' DEGREENAME", " UR.DEGREENO =" + DEGREE + "AND UR.COLLEGE_ID=" + clg + "AND UR.USERNO =" + Session["username"] + "", "D.DEGREENAME");
                DataSet VALUE = objCommon.FillDropDown("ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO LEFT JOIN ACD_PREFERENCE P ON P.PREID = UR.BRANCH_PREF INNER JOIN ACD_STUDENT S  ON (S.USERNO=UR.USERNO AND S.DEGREENO=UR.DEGREENO AND S.COLLEGE_ID = UR.COLLEGE_ID)", "convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE '' END  DEGREENAME", " UR.USERNO =" + Session["username"] + " AND ISNULL(S.ADMCAN,0)=0 ", "D.DEGREENAME"); // D.DEGREENO =" + DEGREE + "  AND UR.BRANCHNO=" + branch + "AND UR.BRANCH_PREF=" + pref + "  AND UR.COLLEGE_ID=" + clg + " AND

                ///  ddlSchClg.SelectedItem.Text = VALUE.Tables[0].Rows[0]["DEGREENAME"].ToString(); // ROSHAN on 05-08-2020 for label Display Previous Course s
                if (VALUE.Tables[0].Rows.Count > 0)
                {
                    //lblAdmittedProgram.Text = VALUE.Tables[0].Rows[0]["DEGREENAME"].ToString(); //
                }
                else
                {
                    divLastAdmittedPrograme.Visible = false;
                }
            }



            //  objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO IN (8)", "DOCUMENTNO ASC");
            int USERNO1 = Convert.ToInt32(ViewState["USER_NO"].ToString());// ((UserDetails)(Session["user"])).UserNo;
            DataSet ds = new DataSet();
            //if (CheckBoxUndertaking.SelectedIndex > -1)
            //{
            //if (Convert.ToInt32(CheckBoxUndertaking.SelectedValue) == 1)
            //{
            ds = objdocContr.GetDoclistStud(USERNO1);
            //if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
            //{
            //    lvDocument.DataSource = ds;
            //    lvDocument.DataBind();
            //    lvDocument.Visible = true;
            //}
            //}
            //else if (Convert.ToInt32(CheckBoxUndertaking.SelectedValue) == 2)
            //{
            //  ds = objCommon.FillDropDown("DOCUMENTENTRY_FILE", "DOCNO,DOCNAME", "FILENAME", "DOCNO IN (8) and USERNO = " + Session["username"] + "", "");
            //}


            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvDocument.DataSource = ds;
                lvDocument.DataBind();
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
                        int value = int.Parse(lnk.CommandArgument);
                        //fileformat.Text = "Only formats are allowed : png,jpg,jpeg";
                        if (value == 0)
                        {
                            if (Convert.ToString(ds.Tables[1].Rows[0]["DOC_STATUS"]) == "1")
                            {
                                lblVerifyDocument.Text = "Approved";
                                lblVerifyDocument.Style.Add("color", "green");
                            }
                            else if (Convert.ToString(ds.Tables[1].Rows[0]["DOC_STATUS"]) == "2")
                            {
                                lblVerifyDocument.Text = "Rejected";
                                lblVerifyDocument.Style.Add("color", "red");
                            }
                            uploded.Text = "";
                            uploded.Visible = true;
                            if (ds.Tables[1].Rows[0]["PHOTO"].ToString() == string.Empty)
                            {
                                uploded.Text = "NO";
                            }
                            else
                            {
                                uploded.Text = "YES";
                                lnk.Visible = true;
                                uploadDate.Text = Convert.ToString(ds.Tables[1].Rows[0]["UPLOAD_DATE"]);
                                Session["STUDENTPHOTO"] = (byte[])ds.Tables[1].Rows[0]["PHOTO"];
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
                            int value = int.Parse(lnk.CommandArgument);
                            lnk.CommandName = ds.Tables[2].Rows[i]["DOC_FILENAME"].ToString();
                            if (value >= 1)
                            {
                                if (Convert.ToString(ds.Tables[2].Rows[i]["DOC_STATUS"]) == "1")
                                {
                                    lblVerifyDocument.Text = "Approved";
                                    lblVerifyDocument.Style.Add("color", "green");
                                }
                                else if (Convert.ToString(ds.Tables[2].Rows[i]["DOC_STATUS"]) == "2")
                                {
                                    lblVerifyDocument.Text = "Rejected";
                                    lblVerifyDocument.Style.Add("color", "red");
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

    protected void btnSaveAndContinue_Click(object sender, EventArgs e)
    {
        try
        {
            string userno = string.Empty;
            int undertakeing = 0;
            int undertakeing1 = 0;
            int DOCNO;

            //Added by swapnil thakare on dated 21-06-2021
            string path = Server.MapPath("~/ONLINEIMAGESUPLOAD\\");
            //string path = System.Configuration.ConfigurationManager.AppSettings["path"];
            string[] branchpref = ddlSchClg.SelectedValue.Split('.');

            if (!(Directory.Exists(path)))
            {
                Directory.CreateDirectory(path);
            }

            int USERNO1 = Convert.ToInt32(ViewState["USER_NO"]);
            DOCNO = Convert.ToInt32(ddlDocument.SelectedValue);
            objdocument.USERNO = USERNO1;
            objdocument.DOCNO = DOCNO;
            objdocument.DOCNAME = ddlDocument.SelectedItem.Text;
            objdocument.PATH = path;
            objdocument.FILENAME = fuDocument.FileName;
            //if (Convert.ToString(CheckBoxUndertaking.SelectedValue) == "")
            //{
            //    ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('Please Select Upload Documents');", true);
            //}
            //else
            //{
            //    undertakeing = Convert.ToInt32(CheckBoxUndertaking.SelectedValue);
            //}

            // undertakeing = Convert.ToInt32(CheckBoxUndertaking.SelectedValue);

            /// undertakeing1 = Convert.ToInt32(CheckUndertaking.SelectedValue);



            if (USERNO1 != 0)
            {

                if (fuDocument.HasFile)
                {
                    if (Convert.ToInt32(ddlDocument.SelectedValue) > 0)
                    {
                        objdocument.FILENAME = USERNO1.ToString() + "_" + ddlDocument.SelectedItem.Text + "_" + fuDocument.FileName.ToString();
                        fuDocument.SaveAs(path + USERNO1.ToString() + "_" + ddlDocument.SelectedItem.Text + "_" + fuDocument.FileName);

                        int i = objdocContr.AddDocStd(objdocument);

                        if (i == 1)
                        {
                            ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('File Uploaded SuccessFully.');", true);
                            ddlDocument.Items.Clear();
                            // Added by swapnil thakare on dated 23-06-2021
                            //objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO IN (1,4,3,9,10,2,11,12,13,14,5,6,7)", "DOCUMENTNO ASC");
                            //objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST D INNER JOIN ACD_DOC_DEGREE ACD ON (D.DOCUMENTNO=ACD.DOCUMENTTYPENO)", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "ACD.DEGREENO=" + branchpref[0] + " ", "DOCUMENTNO ASC");
                            BindDropdownDocument();
                            bindlist();
                        }
                        else if (i == 2)
                        {
                            ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('File Updated SuccessFully.');", true);
                            ddlDocument.Items.Clear();
                            //objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO IN (1,4,3,9,10,2,11,12,13,14,5,6,7)", "DOCUMENTNO ASC");
                            // objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST D INNER JOIN ACD_DOC_DEGREE ACD ON (D.DOCUMENTNO=ACD.DOCUMENTTYPENO)", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "ACD.DEGREENO=" + branchpref[0] + " ", "DOCUMENTNO ASC");
                            BindDropdownDocument();
                            bindlist();
                        }
                        else if (i == -99)
                        {
                            ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('Error.');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('Network Issue.');", true);
                        }

                        ddlDocument.SelectedIndex = 0;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('Select document type first.');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('Select File to Upload.');", true);
                    return;
                    // btnUpload.Enabled = true;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('Session is expired please try again.');", true);
                Response.Redirect("~/default");
                return;
            }


        }
        catch (DirectoryNotFoundException ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ListofDocuments.btnUpload_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Directory Not Found Exception!!");
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
            cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }

    protected void btnSub_Click(object sender, EventArgs e)
    {
        try
        {
            DocumentControllerAcad objDoc = new DocumentControllerAcad();
            DocumentAcad objDocno = new DocumentAcad();
            byte[] StudentPhoto = null;

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
                FileUpload fuDocument = item.FindControl("fuDocument") as FileUpload;
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
                                existsfile = path + Convert.ToInt32(ViewState["stuinfoidno"]) + "_" + fuDocument.FileName;
                                FileInfo file = new FileInfo(existsfile);
                                if (file.Exists)//check file exsit or not  
                                {
                                    file.Delete();
                                }
                                //fuDocument.PostedFile.SaveAs(path + Path.GetFileName(Convert.ToInt32(ViewState["stuinfoidno"]) + "_" + fuDocument.FileName));
                                StudentPhoto = objCommon.GetImageData(fuDocument);
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
                        if (Convert.ToString(Session["STUDENTPHOTO"]) != string.Empty || Convert.ToString(Session["STUDENTPHOTO"]) != null)
                        {
                            StudentPhoto = (byte[])Session["STUDENTPHOTO"];
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
                                existsfile = path + Convert.ToInt32(ViewState["stuinfoidno"]) + "_" + fuDocument.FileName;
                                FileInfo file = new FileInfo(existsfile);
                                if (file.Exists)//check file exsit or not  
                                {
                                    file.Delete();
                                }
                                int retval = Blob_Upload(blob_ConStr, blob_ContainerName, Convert.ToInt32(ViewState["stuinfoidno"]) + "_doc_" + docno.Text, fuDocument);
                                if (retval == 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                    return;
                                }
                                //fuDocument.PostedFile.SaveAs(path + Path.GetFileName(Convert.ToInt32(ViewState["stuinfoidno"]) + "_" + fuDocument.FileName));
                                filenames += Convert.ToInt32(ViewState["stuinfoidno"]) + "_doc_" + docno.Text + ext + '$';
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
            if (Count > 0)
            {
                filenames = filenames.TrimEnd('$');
                docnos = docnos.TrimEnd('$');
                docnames = docnames.TrimEnd('$');

                CustomStatus cs = CustomStatus.Others;
                cs = (CustomStatus)objDoc.AddMultipleDocStd(Convert.ToInt32(ViewState["stuinfoidno"]), Convert.ToInt32(ViewState["USER_NO"]), filenames, docnos, docnames, path, Convert.ToInt32(ViewState["sessionno"]), StudentPhoto);
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
        //int ChkCancelStatus = 0;
        //string enrollno = Convert.ToString(Session["username"]);
        //ChkCancelStatus = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COUNT(1)", "ENROLLNO='" + enrollno + "' AND ISNULL(ADMCAN,0)=0 AND ISNULL(CAN,0)=0"));
        //if (ChkCancelStatus > 0)
        //{
        //    int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "AL_NO", "AL_URL='academic/CanClgDeg.aspx'"));           
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script", "alert('You have been already applied for another courses. if you want to apply new courses then please cancel previous applied courses.after that apply new courses.  !'); location.href='./CanClgDeg.aspx?pageno=" + pageno + "';", true);

        //    //   Response.Redirect("~/CanClgDeg.aspx");

        //    // return;


        //}
        //else
        //{

        // }
        //int undertaking = 0;
        //int undertaking1 = 0;
        //if (Convert.ToString(CheckBoxUndertaking.SelectedValue) == "" ) //(Convert.ToInt32(CheckBoxUndertaking.SelectedValue) == 0 )
        //{
        //   // ShowMessage("Please Check Upload Documents and Undertaking Check Box!");
        //    ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('Please Check Upload Documents Check Box!.');", true);
        //    return;
        //}
        //else if (Convert.ToString(CheckUndertaking.SelectedValue) == "")
        //if (Convert.ToString(CheckUndertaking.SelectedValue) == "")
        //{
        //    // ShowMessage("Please Check Upload Documents and Undertaking Check Box!");
        //    ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('Please Check Undertaking Check Box!.');", true);
        //    return;
        //}

        //divlkPayment.Attributes.Remove("class");

        //int DEGREE = 0;
        //int clg = 0;
        //string branch = string.Empty;
        //string preference = string.Empty;
        //if (ddlSchClg.SelectedIndex > 0)
        //{
        //    string degreeno = ddlSchClg.SelectedValue;
        //    string college = ddlSchClg.SelectedItem.ToString();
        //    // undertaking = Convert.ToInt32(CheckBoxUndertaking.SelectedValue);
        //    undertaking1 = Convert.ToInt32(CheckUndertaking.SelectedValue);



        //    //DataSet ds=  objCommon.FillDropDown("ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID", "convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' DEGREENAME", " D.DEGREENO >0 AND UR.USERNO =" + 48 + "", "D.DEGREENAME");

        //    DataSet ds = objCommon.FillDropDown("ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO LEFT JOIN ACD_PREFERENCE P ON P.PREID = UR.BRANCH_PREF ", "UR.DEGREENO,UR.COLLEGE_ID,UR.BRANCHNO,UR.BRANCH_PREF", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE '' END  DEGREENAME",
        //       " convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) ='" + degreeno + "' AND UR.USERNO =" + Convert.ToInt32(ViewState["USER_NO"]) + "", "D.DEGREENAME");
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        DEGREE = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]);
        //        clg = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]);
        //        branch = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
        //        preference = ds.Tables[0].Rows[0]["BRANCH_PREF"].ToString();
        //    }
        //    byte[] image;
        //    if (photoupload.HasFile)
        //    {
        //        image = objCommon.GetImageData(photoupload);
        //    }
        //    else
        //    {
        //        image = null;
        //    }
        //    objdocument.USERNO = Convert.ToInt32(ViewState["USER_NO"]);
        //    objdocument.CollegeCode = Convert.ToInt32(clg);
        //    objdocument.Degree = DEGREE;
        //    objdocument.BRANCHNO = branch;
        //    objdocument.PREFERENCE = preference;
        //    int idno = Convert.ToInt32(ViewState["stuinfoidno"]);
        //    int Approve = 0;
        //    Approve = objdocContr.AddDetails(objdocument, undertaking, undertaking1, image, idno);
        //    if (Approve > 0)
        //    {
        //        //ShowMessage("");
        //        ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('Save Successfully !');", true);
        //        btnSub.Enabled = false;

        //        bindlist();
        //        bindphoto();
        //        ddlSchClg.SelectedIndex = 0;
        //    }

        //}
    }

    private void bindphoto()
    {
        DataSet dsp = objCommon.FillDropDown("ACD_STUD_PHOTO", "PHOTO", "IDNO", "IDNO='" + ViewState["stuinfoidno"] + "'", "");
        if (dsp.Tables[0].Rows.Count > 0)
        {
            byte[] imgData = null;
            if (dsp.Tables[0].Rows[0]["PHOTO"].ToString() != string.Empty)
            {
                imgData = dsp.Tables[0].Rows[0]["PHOTO"] as byte[];
                ImgPhoto.Visible = true;
                img2.Src = "data:image/png;base64," + Convert.ToBase64String(imgData);
            }
            else
            {
                imgData = null;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('Photo Not Found !');", true);
        }
    }






    protected void ddlDocument_SelectedIndexChanged(object sender, EventArgs e)
    {

        //ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "$('.fuDocumentX').trigger('click');", true);
    }

    public void BindDropdownDocument()
    {
        DataSet ds = objCommon.FillDropDown("DOCUMENTENTRY_FILE", "DOCNO", "FILENAME", "DOCNO NOT IN (8) and USERNO = " + Convert.ToInt32(ViewState["USER_NO"]) + "", "");
        string[] branchpref = ddlSchClg.SelectedValue.Split('.');
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            string docno = string.Empty;
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                docno += dt.Rows[i]["DOCNO"] + ",";
            }

            // Added by swapnil thakare on dated 23-06-2021
            string doc = docno.TrimEnd(',');
            //objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO not in (" + doc + ") AND DOCUMENTNO IN (1,4,3,9,10,2,11,12,13,14,5,6,7)", "DOCUMENTNO ASC");
            //objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST D INNER JOIN ACD_DOC_DEGREE ACD ON (D.DOCUMENTNO=ACD.DOCUMENTTYPENO)", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO not in (" + doc + ") AND ACD.DEGREENO=" + branchpref[0] + " ", "DOCUMENTNO ASC");
            objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO > 0", "DOCUMENTNO");

        }
        else
        {
            //objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO IN (1,4,3,9,10,2,11,12,13,14,5,6,7)", "DOCUMENTNO ASC");
            //objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST D INNER JOIN ACD_DOC_DEGREE ACD ON (D.DOCUMENTNO=ACD.DOCUMENTTYPENO)", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "ACD.DEGREENO=" + branchpref[0] + " ", "DOCUMENTNO ASC");
            objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO > 0", "DOCUMENTNO");
        }
    }

    public void UploadDocument()
    {
        BindDropdownDocument();
        updDocs.Visible = true;
        lblDoc.Visible = true;
        lblUndertaking.Visible = false;
        // objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO not IN (3,1) and DOCUMENTNO IN (1,3,4)", "DOCUMENTNO ASC");
        ddlDocument.SelectedIndex = 0;
        ddlDocument.Enabled = true;
        docmsg.Visible = true;
        docUnder.Visible = false;
        // undertaking.Visible = false;      // commented by swapnil thakare on dated 22-06-2021
        bindlist();
    }
    protected void CheckBoxUndertaking_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckBoxUndertaking.SelectedIndex > -1)
        {

            if (Convert.ToInt32(CheckBoxUndertaking.SelectedValue) == 1)
            {
                DataSet ds = objCommon.FillDropDown("DOCUMENTENTRY_FILE", "DOCNO", "FILENAME", "DOCNO NOT IN (8) and USERNO = " + Session["username"] + "", "");
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    string docno = string.Empty;
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        docno += dt.Rows[i]["DOCNO"] + ",";
                    }
                    string doc = docno.TrimEnd(',');
                    objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO not in (" + doc + ") AND DOCUMENTNO IN (1,4,3,9,10,2,11,12,13,14,5,6,7)", "DOCUMENTNO ASC");

                }
                else
                {
                    objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO IN (1,4,3,9,10,2,11,12,13,14,5,6,7)", "DOCUMENTNO ASC");
                }

                updDocs.Visible = true;
                lblDoc.Visible = true;
                lblUndertaking.Visible = false;
                // objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO not IN (3,1) and DOCUMENTNO IN (1,3,4)", "DOCUMENTNO ASC");
                ddlDocument.SelectedIndex = 0;
                ddlDocument.Enabled = true;
                docmsg.Visible = true;
                docUnder.Visible = false;
                // undertaking.Visible = false;
                bindlist();
            }
            else if (Convert.ToInt32(CheckBoxUndertaking.SelectedValue) == 2)
            {
                updDocs.Visible = true;
                lblDoc.Visible = false;
                lblUndertaking.Visible = true;
                objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO IN (8)", "DOCUMENTNO ASC");
                ddlDocument.SelectedIndex = 1;
                ddlDocument.Enabled = false;
                docmsg.Visible = false;
                docUnder.Visible = true;
                bindlist();
            }
        }
        else
        {
            Response.Redirect(Request.Url.ToString());
        }

        upd.Update();
    }
    protected void CheckUndertaking_SelectedIndexChanged(object sender, EventArgs e)
    {
        divlkPayment.Attributes.Remove("class");
        if (CheckUndertaking.SelectedIndex > -1)
        {
            divlkPayment.Attributes.Remove("class");
            if (Convert.ToInt32(CheckUndertaking.SelectedValue) == 2)
            {
                divlkPayment.Attributes.Remove("class");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showPopup", "$('#modalpopup11').modal('show')", true);
                divlkPayment.Attributes.Remove("class");
                undertaking.Visible = true;
                updDocs.Visible = true;
                lblDoc.Visible = false;
                lblUndertaking.Visible = true;
                //objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO IN (8)", "DOCUMENTNO ASC");
                objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO > 0", "DOCUMENTNO");
                ddlDocument.SelectedIndex = 1;
                ddlDocument.Enabled = false;
                docmsg.Visible = false;
                docUnder.Visible = true;
                //undertaking.Visible = true;
                bindlist();
            }
        }
        else
        {
            Response.Redirect(Request.Url.ToString());
        }

        upd.Update();
    }
    protected void lkuploaddocumnet_Click(object sender, EventArgs e)
    {
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
        if (Convert.ToString(Session["Offer_Accept"]) == "1")
        {
            divacceptance.Attributes.Add("class", "finished");
        }
        if (Convert.ToInt32(ViewState["EducationDetails"]) > 0)
        {
            divOnlineDetails.Attributes.Remove("class");
            divacceptance.Attributes.Add("class", "finished");
        }
        else
        {
            divOnlineDetails.Attributes.Remove("class");
        }
        if (Convert.ToInt32(ViewState["DocumentDetails"]) > 0)
        {
            divlkuploaddocument.Attributes.Remove("class");
            divlkuploaddocument.Attributes.Add("class", "active");
        }
        else
        {
            divlkuploaddocument.Attributes.Remove("class");
            divlkuploaddocument.Attributes.Add("class", "active");
        }
        if (Convert.ToInt32(ViewState["RegistrationDetails"]) > 0)
        {
            divlkModuleOffer.Attributes.Remove("class");
            divlkModuleOffer.Attributes.Add("class", "finished");
        }
        else
        {
            divlkModuleOffer.Attributes.Remove("class");
        }
        //lnkOnlineDetails.Attributes.Add("class", "finished");
        //if (Convert.ToString(Session["Offer_Accept"]) == "1")
        //{
        //    divacceptance.Attributes.Remove("class");
        //    divacceptance.Attributes.Add("class", "finished");
        //}

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
        if (Convert.ToString(Session["Offer_Accept"]) == "1")
        {
            divacceptance.Attributes.Add("class", "finished");
        }
        if (Convert.ToInt32(ViewState["EducationDetails"]) > 0)
        {
            divOnlineDetails.Attributes.Remove("class");
            divacceptance.Attributes.Add("class", "finished");
        }
        else
        {
            divOnlineDetails.Attributes.Remove("class");
        }
        if (Convert.ToInt32(ViewState["DocumentDetails"]) > 0)
        {
            divlkuploaddocument.Attributes.Remove("class");
            divlkuploaddocument.Attributes.Add("class", "finished");
        }
        else
        {
            divlkuploaddocument.Attributes.Remove("class");
        }
        if (Convert.ToInt32(ViewState["RegistrationDetails"]) > 0)
        {
            divlkModuleOffer.Attributes.Remove("class");
            divlkModuleOffer.Attributes.Add("class", "finished");
        }
        else
        {
            divlkModuleOffer.Attributes.Remove("class");
        }
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
            ddlProgramName.SelectedValue = "0";
            divacceptance.Attributes.Remove("class");
            divacceptance.Attributes.Add("class", "active");
        }
        if (Convert.ToInt32(ViewState["EducationDetails"]) > 0)
        {
            divOnlineDetails.Attributes.Remove("class");
            divacceptance.Attributes.Add("class", "finished");
        }
        else
        {
            divOnlineDetails.Attributes.Remove("class");
        }
        if (Convert.ToInt32(ViewState["DocumentDetails"]) > 0)
        {
            divlkuploaddocument.Attributes.Remove("class");
            divlkuploaddocument.Attributes.Add("class", "finished");
        }
        else
        {
            divlkuploaddocument.Attributes.Remove("class");
        }
        if (Convert.ToInt32(ViewState["RegistrationDetails"]) > 0)
        {
            divlkModuleOffer.Attributes.Remove("class");
            divlkModuleOffer.Attributes.Add("class", "finished");
        }
        else
        {
            divlkModuleOffer.Attributes.Remove("class");
        }
    }
    protected void lnkOnlineDetails_Click(object sender, EventArgs e)
    {
        try
        {
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
            if (Convert.ToString(Session["Offer_Accept"]) == "1")
            {
                divacceptance.Attributes.Add("class", "finished");
            }
            BindDataOnline();
            BindAddressData();
            CheckStudyLevel();
            if (Convert.ToInt32(ViewState["EducationDetails"]) > 0)
            {
                divOnlineDetails.Attributes.Remove("class");
                divOnlineDetails.Attributes.Add("class", "active");
            }
            else
            {
                divOnlineDetails.Attributes.Remove("class");
                divOnlineDetails.Attributes.Add("class", "active");
            }
            if (Convert.ToInt32(ViewState["DocumentDetails"]) > 0)
            {
                divlkuploaddocument.Attributes.Remove("class");
                divlkuploaddocument.Attributes.Add("class", "finished");
            }
            else
            {
                divlkuploaddocument.Attributes.Remove("class");
            }
            if (Convert.ToInt32(ViewState["RegistrationDetails"]) > 0)
            {
                divlkModuleOffer.Attributes.Remove("class");
                divlkModuleOffer.Attributes.Add("class", "finished");
            }
            else
            {
                divlkModuleOffer.Attributes.Remove("class");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlPermanentState_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlPTahsil, "ACD_DISTRICT", "DISTINCT DISTRICTNO", "DISTRICTNAME", "STATENO=" + ddlPermanentState.SelectedValue, "DISTRICTNO");
        if (Convert.ToString(rdobtnpwd) == "1")
        {
            rdobtnpwd.SelectedValue = "1";
            DIVtYPE.Style.Add("display", "block");
            txtcheckdisability.Text = "1";
        }
        else
        {
            rdobtnpwd.SelectedValue = "0";
            DIVtYPE.Style.Add("display", "none");
            txtcheckdisability.Text = "1";
        }
    }
    protected void btnPersonalSubmit_Click(object sender, EventArgs e)
    {
        objnu.SESSIONNO = 0; int LEFTRIGHHANDEDNO = 0;
        // string regId = ViewState["username"].ToString();
        //  int NATIONALITY = 0;
        objnu.UserNo = Convert.ToInt32(ViewState["USER_NO"]);
        //string passportno = string.Empty;
        try
        {
            //Added by swapnil thakare on dated 30-08-2021
            objnu.FIRSTNAME = txtFirstName.Text.ToString().ToUpper();
            objnu.LASTNAME = txtPerlastname.Text.ToString().ToUpper();
            objnu.Name = txtNameInitial.Text.Trim().ToUpper();
            objnu.NATIONALITY = Convert.ToInt32(rdbQuestion.SelectedValue);

            objnu.PASSPORT = txtPersonalPassprtNo.Text;
            objnu.NIC = txtOnlineNIC.Text;
            objnu.MobileCode = ddlOnlineMobileCode.SelectedValue;
            if (Convert.ToDateTime(txtDateOfBirth.Text).Year > 2006 || txtDateOfBirth.Text.Length < 1) // allow to 16 age also as per mail 16-09-2022 by Roshan Pannase
            {
                objCommon.DisplayMessage(updApplicantDetails, "Date of Birth is not valid", this.Page);
                if (rdobtnpwd.SelectedValue == "1" || rdobtnpwd.SelectedValue == "0")
                {
                    rdobtnpwd.SelectedValue = null;
                }
                return;
            }
            else
            {
                objnu.DOB = Convert.ToDateTime(txtDateOfBirth.Text);
            }
            objnu.MOBILENO = txtMobile.Text.ToString();
            objnu.EMAILID = txtEmail.Text.ToString();
            objnu.GENDER = rdPersonalGender.SelectedValue;
            objnu.ParentMobileNO = txtPMobNo.Text;
            objnu.FATHERNAME = txtMiddleName.Text.ToString().ToUpper();
            objnu.MOTHERNAME = txtMothersName.Text.ToString().ToUpper();
            string ParentEmail = txtPersonalEmail.Text;

            try
            {
                if (Convert.ToInt32(rdobtnpwd.SelectedValue) == 1)
                {
                    objnu.Person_disability = true;
                }
                else
                {
                    objnu.Person_disability = false;
                }
            }
            catch
            {
                objnu.Person_disability = false;
            }

            if (rdobtnpwd.SelectedIndex == 0)
            {
                if (ddlDisabilityType.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(updApplicantDetails, "Please Select Abled Type", this);
                    DIVtYPE.Visible = true;
                    return;
                }
            }

            objnu.DisabilityType = Convert.ToInt32(ddlDisabilityType.SelectedValue);
            if (Convert.ToString(rdbLeftRight.SelectedValue) == string.Empty)
            {
                LEFTRIGHHANDEDNO = 0;
            }
            else
            {
                LEFTRIGHHANDEDNO = Convert.ToInt32(rdbLeftRight.SelectedValue);
            }
            String cs = objnuc.SubmitPersonalDetailsInStudent(objnu, Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlHomeMobileCode.SelectedValue), Convert.ToString(txtHomeTel.Text), LEFTRIGHHANDEDNO, txtFullName.Text, Convert.ToInt32(ddlConCode.SelectedValue), ParentEmail);

            if (cs != string.Empty && cs != "" && cs != "-99")
            {
                if (ddlALTypeUG.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(updEdcutationalDetailsUG, "Please Select A/L Type.", this.Page);
                    return;
                }
                else if (ddlStreamUG.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(updEdcutationalDetailsUG, "Please Select A/L Stream.", this.Page);
                    return;
                }
                else if (ddlALPassesUG.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(updEdcutationalDetailsUG, "Please Select A/L No. of Passes.", this.Page);
                    return;
                }

                if (ddlALPassesUG.SelectedValue != "6")
                {

                    if (ddlStreamUG.SelectedIndex == 0)
                    {
                        objCommon.DisplayMessage(updEdcutationalDetailsUG, "Please Select A/L Stream.", this.Page);
                        return;
                    }
                    else if (ddlALPassesUG.SelectedIndex == 0)
                    {
                        objCommon.DisplayMessage(updEdcutationalDetailsUG, "Please Select A/L No. of Passes.", this.Page);
                        return;
                    }
                    else if (ddlALTypeUG.SelectedIndex == 0)
                    {
                        objCommon.DisplayMessage(updEdcutationalDetailsUG, "Please Select A/L Type.", this.Page);
                        return;
                    }
                    else if (ddlSubject1.SelectedIndex == 0)
                    {
                        objCommon.DisplayMessage(updEdcutationalDetailsUG, "Please Select Subject 1.", this.Page);
                        return;
                    }
                    else if ((ddlGrade1.SelectedIndex == 0 && ddlALPassesUG.SelectedValue != "1"))
                    {
                        objCommon.DisplayMessage(updEdcutationalDetailsUG, "Please Select Grade 1.", this.Page);
                        return;
                    }
                    else if (ddlSubject2.SelectedIndex == 0)
                    {
                        objCommon.DisplayMessage(updEdcutationalDetailsUG, "Please Select Subject 2.", this.Page);
                        return;
                    }
                    else if ((ddlGrade2.SelectedIndex == 0 && ddlALPassesUG.SelectedValue != "1"))
                    {
                        objCommon.DisplayMessage(updEdcutationalDetailsUG, "Please Select Grade 2.", this.Page);
                        return;
                    }
                    else if (ddlSubject3.SelectedIndex == 0)
                    {
                        objCommon.DisplayMessage(updEdcutationalDetailsUG, "Please Select Subject 3.", this.Page);
                        return;
                    }
                    else if ((ddlGrade3.SelectedIndex == 0 && ddlALPassesUG.SelectedValue != "1"))
                    {
                        objCommon.DisplayMessage(updEdcutationalDetailsUG, "Please Select Grade 3.", this.Page);
                        return;
                    }

                }


                objnu.UserNo = Convert.ToInt32(ViewState["USER_NO"]);
                objnu.Stream = Convert.ToInt32(ddlStreamUG.SelectedValue);
                objnu.Attempts = Convert.ToInt32(ddlALPassesUG.SelectedValue);
                objnu.ALType = Convert.ToInt32(ddlALTypeUG.SelectedValue);

                if (ddlSubject1.SelectedIndex == 0)
                {
                    objnu.Subject1 = Convert.ToString("0");
                }
                else
                {
                    objnu.Subject1 = Convert.ToString(ddlSubject1.SelectedValue);
                }
                // objnu.Grade1 = Convert.ToString(ddlGrade1.SelectedValue);
                if (ddlGrade1.SelectedIndex == 0)
                {
                    objnu.Grade1 = Convert.ToString("0");
                }
                else
                {
                    objnu.Grade1 = Convert.ToString(ddlGrade1.SelectedValue);
                }

                //objnu.Subject2 = Convert.ToString(ddlSubject2.SelectedValue);
                //objnu.Grade2 = Convert.ToString(ddlGrade2.SelectedValue);

                if (ddlSubject2.SelectedIndex == 0)
                {
                    objnu.Subject2 = Convert.ToString("0");
                }
                else
                {
                    objnu.Subject2 = Convert.ToString(ddlSubject2.SelectedValue);
                }

                if (ddlGrade2.SelectedIndex == 0)
                {
                    objnu.Grade2 = Convert.ToString("0");
                }
                else
                {
                    objnu.Grade2 = Convert.ToString(ddlGrade2.SelectedValue);
                }


                //objnu.Subject3 = Convert.ToString(ddlSubject3.SelectedValue);
                //objnu.Grade3 = Convert.ToString(ddlGrade3.SelectedValue);

                if (ddlSubject3.SelectedIndex == 0)
                {
                    objnu.Subject3 = Convert.ToString("0");
                }
                else
                {
                    objnu.Subject3 = Convert.ToString(ddlSubject3.SelectedValue);
                }

                if (ddlGrade3.SelectedIndex == 0)
                {
                    objnu.Grade3 = Convert.ToString("0");
                }
                else
                {
                    objnu.Grade3 = Convert.ToString(ddlGrade3.SelectedValue);
                }



                if (ddlSubject4.SelectedIndex == 0)
                    objnu.Subject4 = Convert.ToString("0");
                else
                    objnu.Subject4 = Convert.ToString(ddlSubject4.SelectedValue);
                if (ddlGrade4.SelectedIndex == 0)
                    objnu.Grade4 = Convert.ToString("0");
                else
                    objnu.Grade4 = Convert.ToString(ddlGrade4.SelectedValue);
                //OL    
                if (ddlolStream.SelectedIndex == 0)
                {
                    objnu.OLStream = Convert.ToInt32(0);
                }
                else
                {
                    objnu.OLStream = Convert.ToInt32(ddlolStream.SelectedValue);
                }

                objnu.OLAttempts = Convert.ToInt32(ddlolpass.SelectedValue);

                objnu.OLType = Convert.ToInt32(ddlOLType.SelectedValue);

                if (olddlsub1.SelectedIndex == 0)
                {
                    objnu.OLSubject1 = Convert.ToString("0");
                }
                else
                {
                    objnu.OLSubject1 = Convert.ToString(olddlsub1.SelectedValue);
                }

                if (olddlgrade1.SelectedIndex == 0)
                {
                    objnu.OLGrade1 = Convert.ToString("0");
                }
                else
                {
                    objnu.OLGrade1 = Convert.ToString(olddlgrade1.SelectedValue);
                }

                if (olddlsubj2.SelectedIndex == 0)
                {
                    objnu.OLSubject2 = Convert.ToString("0");
                }
                else
                {
                    objnu.OLSubject2 = Convert.ToString(olddlsubj2.SelectedValue);
                }

                if (olddlgrade2.SelectedIndex == 0)
                {
                    objnu.OLGrade2 = Convert.ToString("0");
                }
                else
                {
                    objnu.OLGrade2 = Convert.ToString(olddlgrade2.SelectedValue);
                }

                if (olddlsub3.SelectedIndex == 0)
                {
                    objnu.OLSubject3 = Convert.ToString("0");
                }
                else
                {
                    objnu.OLSubject3 = Convert.ToString(olddlsub3.SelectedValue);
                }

                if (olddlgrade3.SelectedIndex == 0)
                {
                    objnu.OLGrade3 = Convert.ToString("0");
                }
                else
                {
                    objnu.OLGrade3 = Convert.ToString(olddlgrade3.SelectedValue);
                }

                if (olddlsub4.SelectedIndex == 0)
                {
                    objnu.OLSubject4 = Convert.ToString("0");
                }
                else
                {
                    objnu.OLSubject4 = Convert.ToString(olddlsub4.SelectedValue);
                }

                if (olddlgrade4.SelectedIndex == 0)
                {
                    objnu.OLGrade4 = Convert.ToString("0");
                }
                else
                {
                    objnu.OLGrade4 = Convert.ToString(olddlgrade4.SelectedValue);
                }


                if (olddlsub5.SelectedIndex == 0)
                {
                    objnu.OLSubject5 = Convert.ToString("0");
                }
                else
                {
                    objnu.OLSubject5 = Convert.ToString(olddlsub5.SelectedValue);
                }

                if (olddlgrade5.SelectedIndex == 0)
                {
                    objnu.OLGrade5 = Convert.ToString("0");
                }
                else
                {
                    objnu.OLGrade5 = Convert.ToString(olddlgrade5.SelectedValue);
                }



                if (olddlsub6.SelectedIndex == 0)
                {
                    objnu.OLSubject6 = Convert.ToString("0");
                }
                else
                {
                    objnu.OLSubject6 = Convert.ToString(olddlsub6.SelectedValue);
                }

                if (olddlgrade6.SelectedIndex == 0)
                {
                    objnu.OLGrade6 = Convert.ToString("0");
                }
                else
                {
                    objnu.OLGrade6 = Convert.ToString(olddlgrade6.SelectedValue);
                }


                objnu.Command_type = 1;
                objnu.StlQno = 0;
                objnu.AptitudeCenter = 0;
                objnu.AptitudeMedium = 0;
                CustomStatus cs1 = CustomStatus.Others;

                objnu.Command_type = 2;
                cs1 = (CustomStatus)objnuc.SubmitUserEdcucationalDetails(objnu, Convert.ToInt32(Session["userno"]), txtALIndex.Text.ToString(), txtALyear.Text.ToString(), txtZScore.Text.ToString(), txtALSchoolDistrict.Text.ToString(), txtALSchool.Text.ToString());
                //////////////////////// ADDRESS DETAILS CODE ///////////////
                try
                {
                    // Get Address details
                    objnu.UserNo = Convert.ToInt32(ViewState["USER_NO"]);
                    objnu.PermanentAddress = txtPerAddress.Text.ToString();
                    objnu.PermanentCountry = ddlPerContry.SelectedValue.ToString();
                    objnu.PermanentProvince = ddlPerProvince.SelectedValue.ToString();
                    objnu.PermanentDistrict = ddlPerDisctrict.SelectedValue.ToString();

                    objnu.LocalAddress=txtPermAddress.Text.ToString();
                    objnu.LocalCountry=ddlPCon.SelectedValue.ToString();
                    objnu.LocalProvince=ddlPermanentState.SelectedValue.ToString();
                    objnu.LocalDistrict = ddlPTahsil.SelectedValue.ToString();
                    String Address = objnuc.SubmitAddressDetails(objnu);
                    string UgPg = Convert.ToString(objCommon.LookUp("ACD_USER_REGISTRATION", "DISTINCT UGPGOT", "USERNO=" + Convert.ToInt32(ViewState["USER_NO"])));
                    if (Address == "1")
                    {
                        objCommon.DisplayMessage(updApplicantDetails, "Personal Details & Education Details Updated Successfully", this.Page);
                        lkuploaddocumnet_Click(new object(), new EventArgs());
                    }
                    else if (Address == "2")
                    {
                        objCommon.DisplayMessage(updApplicantDetails, "Personal Details & Education Details Updated Successfully", this.Page);
                        lkuploaddocumnet_Click(new object(), new EventArgs());
                        //return;
                    }
                    else
                    {
                        objCommon.DisplayMessage("Error!!", this.Page);
                    }

                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objCommon.ShowError(Page, "domain.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
                    else
                        objCommon.ShowError(Page, "Server UnAvailable");
                }


                BindDataOnline();
                BindAddressData();
                DataSet dsStatus = objnuc.GetStudentStatusDetails(Convert.ToInt32(ViewState["USER_NO"]));

                string address = dsStatus.Tables[0].Rows[0]["ADD_DETAILS"] == DBNull.Value ? string.Empty : dsStatus.Tables[0].Rows[0]["ADD_DETAILS"].ToString();

            }
            else
            {
                objCommon.DisplayMessage("Error!!", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "domain.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindDataOnline()
    {
        try
        {
            PopulateDropDown();
            DataSet ds = objnuc.GetStudPersonalDetails(Convert.ToInt32(ViewState["USER_NO"]));
            txtDateOfBirth.Text = "";
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                txtMiddleName.Text = ds.Tables[0].Rows[0]["MIDDLENAME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["MIDDLENAME"].ToString();
                txtMothersName.Text = ds.Tables[0].Rows[0]["MOTHERNAME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["MOTHERNAME"].ToString();


                txtNameInitial.Text = ds.Tables[0].Rows[0]["NAME_INITIAL"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["NAME_INITIAL"].ToString();

                txtFirstName.Text = ds.Tables[0].Rows[0]["FIRSTNAME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["FIRSTNAME"].ToString();

                txtPerlastname.Text = ds.Tables[0].Rows[0]["LASTNAME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["LASTNAME"].ToString();
                rdbQuestion.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["CITIZEN_NO"]) == null ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["CITIZEN_NO"]);

                if (Convert.ToString(ds.Tables[0].Rows[0]["DOB"]) != string.Empty)
                {
                    txtDateOfBirth.Text = Convert.ToString(Convert.ToDateTime(ds.Tables[0].Rows[0]["DOB"].ToString()));
                }
                if (Convert.ToString(ds.Tables[0].Rows[0]["LEFT_RIGHT_HANDED"]) != "0")
                {
                    rdbLeftRight.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["LEFT_RIGHT_HANDED"]) == null ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["LEFT_RIGHT_HANDED"]);
                }
                //txtDateOfBirth.Text = ds.Tables[0].Rows[0]["DOB"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["DOB"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["EMAILID"].ToString();
                txtPersonalPassprtNo.Text = ds.Tables[0].Rows[0]["PASSPORTNO"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["PASSPORTNO"].ToString();
                txtOnlineNIC.Text = ds.Tables[0].Rows[0]["NIC"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["NIC"].ToString();
                rdPersonalGender.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["GENDER"]) == null ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["GENDER"]);
                ddlOnlineMobileCode.SelectedValue = ds.Tables[0].Rows[0]["MOBILECODE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["MOBILECODE"].ToString();
                txtMobile.Text = ds.Tables[0].Rows[0]["MOBILE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["MOBILE"].ToString();

                txtPMobNo.Text = ds.Tables[0].Rows[0]["PARENTPHONENO"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["PARENTPHONENO"].ToString();

                txtHomeTel.Text = ds.Tables[0].Rows[0]["HOME_MOBILENO"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["HOME_MOBILENO"].ToString();

                txtFullName.Text = ds.Tables[0].Rows[0]["FULLNAME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                txtPersonalEmail.Text = ds.Tables[0].Rows[0]["FATHER_EMAIL"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["FATHER_EMAIL"].ToString(); 
                ddlHomeMobileCode.SelectedValue = ds.Tables[0].Rows[0]["HOMETELCODE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["HOMETELCODE"].ToString();
                ddlConCode.SelectedValue = ds.Tables[0].Rows[0]["PARENTMOBILECODE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["PARENTMOBILECODE"].ToString();
                if (Convert.ToString(ds.Tables[0].Rows[0]["SPECIALLY_ABLED_NO"]) != string.Empty)
                {
                    if (Convert.ToString(ds.Tables[0].Rows[0]["SPECIALLY_ABLED_NO"]) == "1")
                    {
                        rdobtnpwd.SelectedValue = "1";
                        DIVtYPE.Style.Add("display", "block");
                        txtcheckdisability.Text = "1";
                    }
                    else
                    {
                        rdobtnpwd.SelectedValue = "0";
                        DIVtYPE.Style.Add("display", "none");
                        txtcheckdisability.Text = "1";
                    }
                }
                if (ds.Tables[0].Rows[0]["ABLED_TYPE_NO"].ToString() != "0")
                {
                    ddlDisabilityType.SelectedValue = Convert.ToString(Convert.ToInt32(ds.Tables[0].Rows[0]["ABLED_TYPE_NO"].ToString()) == null ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["ABLED_TYPE_NO"]));
                }
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
    private void BindAddressData()
    {
        try
        {
            DataSet ds = objnuc.GetAddressDetails(Convert.ToInt32(ViewState["USER_NO"]));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                txtPermAddress.Text = ds.Tables[0].Rows[0]["LADDRESS"].ToString();
                ddlPermanentState.SelectedValue = ds.Tables[0].Rows[0]["LPROVINCE"].ToString();
                ddlPCon.SelectedValue = ds.Tables[0].Rows[0]["LCOUNTRY"].ToString();
                objCommon.FillDropDownList(ddlPTahsil, "ACD_DISTRICT", "DISTINCT DISTRICTNO", "DISTRICTNAME", "STATENO=" + ddlPermanentState.SelectedValue, "DISTRICTNO");
                ddlPTahsil.SelectedValue = ds.Tables[0].Rows[0]["LDISTRICT"].ToString();

                txtPerAddress.Text = ds.Tables[0].Rows[0]["PADDRESS"].ToString();
                ddlPerProvince.SelectedValue = ds.Tables[0].Rows[0]["PPROVINCE"].ToString();
                ddlPerContry.SelectedValue = ds.Tables[0].Rows[0]["PCOUNTRY"].ToString();
                objCommon.FillDropDownList(ddlPerDisctrict, "ACD_DISTRICT", "DISTINCT DISTRICTNO", "DISTRICTNAME", "STATENO=" + ddlPerProvince.SelectedValue, "DISTRICTNO");
                ddlPerDisctrict.SelectedValue = ds.Tables[0].Rows[0]["PDISTRICT"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AddressDetails.BindData-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void CheckStudyLevel()
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("ACD_USER_REGISTRATION", "DISTINCT UGPGOT", "ADMBATCH", "USERNO=" + Convert.ToInt32(ViewState["USER_NO"]), "");
        if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToString(ds.Tables[0].Rows[0]["UGPGOT"]) == "1" || Convert.ToString(ds.Tables[0].Rows[0]["UGPGOT"]) == "3")
            {

                divUgEducationUG.Visible = true;
                divEducationPG.Visible = false;
                DivEducationDEtailsPDP.Visible = false;
                divEducationDetailsPHD.Visible = false;
                //FillDropDownList();
                BindListViewData();
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["UGPGOT"]) == "2")
            {
                divUgEducationUG.Visible = false;
                divEducationPG.Visible = true;
                DivEducationDEtailsPDP.Visible = false;
                divEducationDetailsPHD.Visible = false;
                BindlistView(Convert.ToInt32(ViewState["USER_NO"]));
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["UGPGOT"]) == "4")
            {
                divUgEducationUG.Visible = false;
                divEducationPG.Visible = false;
                DivEducationDEtailsPDP.Visible = true;
                divEducationDetailsPHD.Visible = false;
                //FillDropDownListPDP();
                BindListViewDataPDP();
                //return;

            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["UGPGOT"]) == "5")
            {
                divUgEducationUG.Visible = false;
                divEducationPG.Visible = false;
                DivEducationDEtailsPDP.Visible = false;
                divEducationDetailsPHD.Visible = true;
                BindUserData();
            }
        }
    }
    protected void FillDropDownList()
    {
        NewUser objNewUr = new NewUser();
        objNewUr.UserNo = 0;
        objNewUr.Command_type = 4;
        objNewUr.StlQno = Convert.ToInt32(ddlALTypeUG.SelectedValue);
        DataSet ds = objnuc.GetAppliedUserEducationDetails(objNewUr);

        ddlALTypeUG.DataSource = ds.Tables[0];
        ddlALTypeUG.DataValueField = "ALTYPENO";
        ddlALTypeUG.DataTextField = "ALTYPENAME";
        ddlALTypeUG.DataBind();

        //ddlStreamUG.DataSource = ds.Tables[1];
        //ddlStreamUG.DataValueField = "STREAMNO";
        //ddlStreamUG.DataTextField = "STREAMNAME";
        //ddlStreamUG.DataBind();
        ddlOLType.DataSource = ds.Tables[0];
        ddlOLType.DataValueField = "ALTYPENO";
        ddlOLType.DataTextField = "ALTYPENAME";
        ddlOLType.DataBind();

        ddlALPassesUG.DataSource = ds.Tables[2];
        ddlALPassesUG.DataValueField = "QUALILEVELNO";
        ddlALPassesUG.DataTextField = "QUALILEVELNAME";
        ddlALPassesUG.DataBind();

        ddlolpass.DataSource = ds.Tables[5];
        ddlolpass.DataValueField = "QUALILEVELNO_OL";
        ddlolpass.DataTextField = "QUALILEVELNAME_OL";
        ddlolpass.DataBind();
    }

    protected void BindListViewData()
    {
        NewUser objNewUr = new NewUser();
        objNewUr.UserNo = Convert.ToInt32(ViewState["USER_NO"]);
        objNewUr.Command_type = 1;
        objNewUr.StlQno = 0;
        DataSet ds = objnuc.GetAppliedUserEducationDetails(objNewUr);
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            txtALIndex.Text = ds.Tables[0].Rows[0]["ALINDEXNO"].ToString();
            txtALyear.Text = ds.Tables[0].Rows[0]["ALYEAR"].ToString();
            txtZScore.Text = ds.Tables[0].Rows[0]["ALZSCORE"].ToString();
            txtALSchoolDistrict.Text = ds.Tables[0].Rows[0]["ALDISTRICT"].ToString();
            txtALSchool.Text = ds.Tables[0].Rows[0]["ALSCHOOL"].ToString();

            ddlALPassesUG.SelectedValue = ds.Tables[0].Rows[0]["ATTEMPTNO"].ToString();
            ddlALPassesUG_SelectedIndexChanged(new object(), new EventArgs());
            ddlALTypeUG.SelectedValue = ds.Tables[0].Rows[0]["AL_TYPE"].ToString();
            if (ds.Tables[0].Rows[0]["ATTEMPTNO"].ToString() != "6")
            {
                ddlALTypeUG_SelectedIndexChanged(new object(), new EventArgs());
            }
            ddlStreamUG.SelectedValue = ds.Tables[0].Rows[0]["STREAMNO"].ToString();
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["SUBJECT2"].ToString()) > 0)
            {
                ddlSubject1.SelectedValue = ds.Tables[0].Rows[0]["SUBJECT1"].ToString();

                ddlSubject1_SelectedIndexChanged(new object(), new EventArgs());
            }
            if (ds.Tables[0].Rows[0]["ATTEMPTNO"].ToString() != "1")
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["GRADE1"].ToString()) > 0)
                {
                    ddlGrade1.SelectedValue = ds.Tables[0].Rows[0]["GRADE1"].ToString();
                }
            }
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["SUBJECT2"].ToString()) > 0)
            {
                ddlSubject2.SelectedValue = ds.Tables[0].Rows[0]["SUBJECT2"].ToString();

                ddlSubject2_SelectedIndexChanged(new object(), new EventArgs());
                if (ds.Tables[0].Rows[0]["ATTEMPTNO"].ToString() != "1")
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["GRADE2"].ToString()) > 0)
                    {
                        ddlGrade2.SelectedValue = ds.Tables[0].Rows[0]["GRADE2"].ToString();
                    }
                }
            }
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["SUBJECT3"].ToString()) > 0)
            {
                ddlSubject3.SelectedValue = ds.Tables[0].Rows[0]["SUBJECT3"].ToString();

                ddlSubject3_SelectedIndexChanged(new object(), new EventArgs());
                if (ds.Tables[0].Rows[0]["ATTEMPTNO"].ToString() != "1")
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["GRADE3"].ToString()) > 0)
                    {
                        ddlGrade3.SelectedValue = ds.Tables[0].Rows[0]["GRADE3"].ToString();
                    }
                }
            }
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["SUBJECT4"].ToString()) > 0)
            {
                ddlSubject4.SelectedValue = ds.Tables[0].Rows[0]["SUBJECT4"].ToString();
                if (ds.Tables[0].Rows[0]["ATTEMPTNO"].ToString() != "1")
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["GRADE4"].ToString()) > 0)
                    {
                        ddlGrade4.SelectedValue = ds.Tables[0].Rows[0]["GRADE4"].ToString();
                    }
                }
            }
            ddlOLType.SelectedValue = ds.Tables[0].Rows[0]["OLTYPE"].ToString();
            if (ddlOLType.SelectedIndex > 0)
            {
                ddlOLType_SelectedIndexChanged(new object(), new EventArgs());
            }
            ddlolStream.SelectedValue = ds.Tables[0].Rows[0]["OLSTREAMNO"].ToString();
            ddlolpass.SelectedValue = ds.Tables[0].Rows[0]["OLATTEMPTNO"].ToString();

            if (ds.Tables[0].Rows[0]["OLSUBJECT1"].ToString() != "0")
            {
                olddlsub1.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT1"].ToString();
                olddlgrade1.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE1"].ToString();

                olddlsub1_SelectedIndexChanged(new object(), new EventArgs());
            }
            if (ds.Tables[0].Rows[0]["OLSUBJECT2"].ToString() != "0")
            {
                olddlsubj2.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT2"].ToString();
                olddlgrade2.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE2"].ToString();

                olddlsubj2_SelectedIndexChanged(new object(), new EventArgs());
            }
            if (ds.Tables[0].Rows[0]["OLSUBJECT3"].ToString() != "0")
            {
                olddlsub3.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT3"].ToString();
                olddlgrade3.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE3"].ToString();

                olddlsub3_SelectedIndexChanged(new object(), new EventArgs());
            }
            if (ds.Tables[0].Rows[0]["OLSUBJECT4"].ToString() != "0")
            {
                olddlsub4.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT4"].ToString();
                olddlgrade4.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE4"].ToString();

                olddlsub4_SelectedIndexChanged(new object(), new EventArgs());
            }
            if (ds.Tables[0].Rows[0]["OLSUBJECT5"].ToString() != "0")
            {
                olddlsub5.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT5"].ToString();
                olddlgrade5.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE5"].ToString();
                olddlsub5_SelectedIndexChanged(new object(), new EventArgs());
            }
            if (ds.Tables[0].Rows[0]["OLSUBJECT6"].ToString() != "0")
            {
                olddlsub6.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT6"].ToString();

                olddlgrade6.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE6"].ToString();
            }

        }
    }

    private void BindlistView(int USERNO)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_USER_LAST_QUALIFICATION AL", "AL.STLQNO", "*", "USERNO=" + USERNO + "", "AL.STLQNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtHighestEducationPG.Text = ds.Tables[0].Rows[0]["HIGHESTEDUCATION"].ToString();
                txtUniversityPG.Text = ds.Tables[0].Rows[0]["UNIVERSITYINSTITUTE"].ToString();
                txtQualificationAwardPG.Text = ds.Tables[0].Rows[0]["QUALAWARDYEAR"].ToString();
                txtSpecializationPG.Text = ds.Tables[0].Rows[0]["SPECILIZOFQUAL"].ToString();
                txtGPAPG.Text = ds.Tables[0].Rows[0]["GPAOFQUAL"].ToString();
                txtProfessionalPG.Text = ds.Tables[0].Rows[0]["PROFQUAL"].ToString();
                txtProfessionalUniversityPG.Text = ds.Tables[0].Rows[0]["PROFUNIVINSTIT"].ToString();
                txtAwardDatePG.Text = ds.Tables[0].Rows[0]["QUALAWARDOFDATE"].ToString();
                txtSpecilizationQualificationPG.Text = ds.Tables[0].Rows[0]["SPECILOFQUAL"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EducationDetailsPG.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindListViewDataPDP()
    {
        NewUser objNewUr = new NewUser();
        objNewUr.UserNo = Convert.ToInt32(ViewState["USER_NO"]);
        objNewUr.Command_type = 1;
        objNewUr.StlQno = 0;
        DataSet ds = objnuc.GetAppliedUserEducationDetailsForPDP(objNewUr);
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {

            ddlALTypePDP.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["PDP_STYLLABUS_NO"].ToString());
            txtIndexNoPDP.Text = Convert.ToString(ds.Tables[0].Rows[0]["PDP_INDEX_NO"].ToString());
            ddlMediumPDP.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["PDP_MEDIUM_NO"].ToString());
            ddlAttemptPDP.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["PDP_ATTEMPT_NO"].ToString());
            ddlALPassesPDP.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["PDP_OL_RESULT"].ToString());
            txtInstitutePDP.Text = Convert.ToString(ds.Tables[0].Rows[0]["PDP_INSTITUTE"].ToString());
            txtRegistrationNoPDP.Text = Convert.ToString(ds.Tables[0].Rows[0]["PDP_REGISTRATION_NO"].ToString());
            txtNameoftheProgrammePDP.Text = Convert.ToString(ds.Tables[0].Rows[0]["PDP_PROGRAM_NAME"].ToString());
            ddlStreamPDP.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["PDP_STREAM_NO"].ToString());
            txtGradePointAveragePDP.Text = Convert.ToString(ds.Tables[0].Rows[0]["PDP_GPA"].ToString());
            ddlSubjectResultPDP.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["PDP_SUBJECT_RESULT"].ToString());
            //ddlStream.Enabled = false;
            ViewState["ForEdit"] = "edit";
        }
    }

    protected void BindUserData()
    {
        try
        {
            DataSet ds = null;
            ds = objnuc.getPHDEdcucationalDetails(Convert.ToInt32(ViewState["USER_NO"]));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvRefrees.DataSource = ds.Tables[0];
                lvRefrees.DataBind();
                Session.Add("Referees", ds.Tables[0]);
            }
            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                lvEmploymentHistory.DataSource = ds.Tables[1];
                lvEmploymentHistory.DataBind();
                Session.Add("Employment_History", ds.Tables[1]);
            }
            if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
            {
                txtNameofQualificationPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_QUALIFICATION"]);
                txtYearofAwardPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_YEAR_OF_AWARD"]);
                txtUniversityPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_UNIVERSITY"]);
                txtMainSpecialtyPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_MAIN_SPECIALITY"]);
                txtGPAPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_GPA"]);
                txtNameQualificationPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_OTHER_QUALIFICATION"]);
                txtAwardingUniversityPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_AWARDING_INSTITUTE"]);
                txtAwardDatePHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_DATE_AWARD"]);
                txtSpecilizationQualificationPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_SPECIALIZATION"]);
                txtDescriptionPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_DESCRIPTION"]);
                ddlModePHD.SelectedValue = Convert.ToString(ds.Tables[2].Rows[0]["PHD_MODE_NO"]);
                rdbQuestion1PHD.SelectedValue = Convert.ToString(ds.Tables[2].Rows[0]["PHD_FIRST_QUESTION_NO"]);
                rdbQuestion1PHD_SelectedIndexChanged(new object(), new EventArgs());
                txtQuestionDetailsPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_FIRST_QUESTION_DETAILS"]);
                rdbQuestion2PHD.SelectedValue = Convert.ToString(ds.Tables[2].Rows[0]["PHD_SECOND_QUESTION_NO"]);
                rdbQuestion2PHD_SelectedIndexChanged(new object(), new EventArgs());
                txtQuestion1DetailsPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_SECOND_QUESTION_DETAILS"]);
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlALTypeUG_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlALTypeUG.SelectedIndex > 0)
            {
                NewUser objNewUr = new NewUser();
                objNewUr.UserNo = Convert.ToInt32(ViewState["USER_NO"]);
                objNewUr.StlQno = Convert.ToInt32(ddlALTypeUG.SelectedValue);   // NOTE STLQNO USE AS A/L TYPE FOR COMMAND TYPE 3
                objNewUr.Command_type = 3;
                ViewState["DYNAMICFILLDROPDOWN"] = null;
                ddlSubject1.Items.Clear();
                ddlSubject1.Items.Insert(0, "Please Select");
                ddlGrade1.Items.Clear();
                ddlGrade1.Items.Insert(0, "Please Select");
                ddlGrade2.Items.Clear();
                ddlGrade2.Items.Insert(0, "Please Select");
                ddlGrade3.Items.Clear();
                ddlGrade3.Items.Insert(0, "Please Select");
                ddlGrade4.Items.Clear();
                ddlGrade4.Items.Insert(0, "Please Select");

                if (ddlALTypeUG.SelectedValue == "2")
                {
                    objCommon.FillDropDownList(ddlStreamUG, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO = 8", "STREAMNAME");
                    ddlStreamUG.SelectedIndex = 1;
                }
                else if (ddlALTypeUG.SelectedValue == "3")
                {
                    objCommon.FillDropDownList(ddlStreamUG, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO = 9", "STREAMNAME");
                    ddlStreamUG.SelectedIndex = 1;
                }
                else if (ddlALTypeUG.SelectedValue == "4")
                {
                    objCommon.FillDropDownList(ddlStreamUG, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO = 7", "STREAMNAME");
                    ddlStreamUG.SelectedIndex = 1;
                }
                else
                {
                    objCommon.FillDropDownList(ddlStreamUG, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO NOT IN(7,8,9)", "STREAMNAME");
                }

                DataSet ds = objnuc.GetAppliedUserEducationDetails(objNewUr);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["DYNAMICFILLDROPDOWN"] = ds.Tables[0];
                    ddlSubject1.DataSource = ds.Tables[0];
                    ddlSubject1.DataTextField = "AL_COURSES";
                    ddlSubject1.DataValueField = "ID";
                    ddlSubject1.DataBind();


                    ddlGrade1.DataSource = ds.Tables[1];
                    ddlGrade1.DataValueField = "ID";
                    ddlGrade1.DataTextField = "GRADES";
                    ddlGrade1.DataBind();

                    ddlGrade2.DataSource = ds.Tables[1];
                    ddlGrade2.DataValueField = "ID";
                    ddlGrade2.DataTextField = "GRADES";
                    ddlGrade2.DataBind();

                    ddlGrade3.DataSource = ds.Tables[1];
                    ddlGrade3.DataValueField = "ID";
                    ddlGrade3.DataTextField = "GRADES";
                    ddlGrade3.DataBind();

                    ddlGrade4.DataSource = ds.Tables[1];
                    ddlGrade4.DataValueField = "ID";
                    ddlGrade4.DataTextField = "GRADES";
                    ddlGrade4.DataBind();
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Subject 1 Details Not Found Please Contact Online Admission Department.", this.Page);
                    return;
                }
            }
            else
            {
                ddlSubject1.Items.Clear();
                ddlSubject1.Items.Insert(0, "Please Select");
                ddlSubject2.Items.Clear();
                ddlSubject2.Items.Insert(0, "Please Select");
                ddlSubject3.Items.Clear();
                ddlSubject3.Items.Insert(0, "Please Select");
                ddlSubject4.Items.Clear();
                ddlSubject4.Items.Insert(0, "Please Select");

                ddlGrade1.Items.Clear();
                ddlGrade1.Items.Insert(0, "Please Select");
                ddlGrade2.Items.Clear();
                ddlGrade2.Items.Insert(0, "Please Select");
                ddlGrade3.Items.Clear();
                ddlGrade3.Items.Insert(0, "Please Select");
                ddlGrade4.Items.Clear();
                ddlGrade4.Items.Insert(0, "Please Select");

                //objCommon.DisplayMessage(updEdcutationalDetailsUG, "Please Select A/L Type.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlSubject1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubject1.SelectedIndex > 0)
            {
                ddlSubject2.Items.Clear();
                ddlSubject2.Items.Insert(0, "Please Select");
                ddlSubject3.Items.Clear();
                ddlSubject3.Items.Insert(0, "Please Select");
                ddlSubject4.Items.Clear();
                ddlSubject4.Items.Insert(0, "Please Select");

                DataTable ds = (DataTable)ViewState["DYNAMICFILLDROPDOWN"];
                if (ds != null && ds.Rows.Count > 0)
                {
                    ddlSubject2.DataSource = ds;
                    ddlSubject2.DataTextField = "AL_COURSES";
                    ddlSubject2.DataValueField = "ID";
                    ddlSubject2.DataBind();

                    ListItem itemToRemove = ddlSubject1.Items.FindByText(ddlSubject1.SelectedItem.Text);
                    ddlSubject2.Items.Remove(itemToRemove);
                    if (ddlSubject2.Items.Count == 1)
                    {
                        objCommon.DisplayMessage(this.Page, "Subject 2 Details Not Found Please Contact Online Admission Department.", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Subject 2 Details Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                ddlSubject2.Items.Clear();
                ddlSubject2.Items.Insert(0, "Please Select");
                ddlSubject3.Items.Clear();
                ddlSubject3.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlSubject2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubject2.SelectedIndex > 0)
            {
                ddlSubject3.Items.Clear();
                ddlSubject3.Items.Insert(0, "Please Select");
                ddlSubject4.Items.Clear();
                ddlSubject4.Items.Insert(0, "Please Select");

                DataTable ds = (DataTable)ViewState["DYNAMICFILLDROPDOWN"];
                if (ds != null && ds.Rows.Count > 0)
                {
                    ddlSubject3.DataSource = ds;
                    ddlSubject3.DataTextField = "AL_COURSES";
                    ddlSubject3.DataValueField = "ID";
                    ddlSubject3.DataBind();

                    ListItem itemToRemove = ddlSubject1.Items.FindByText(ddlSubject1.SelectedItem.Text);
                    ListItem itemToRemoveSubject2 = ddlSubject2.Items.FindByText(ddlSubject2.SelectedItem.Text);
                    ddlSubject3.Items.Remove(itemToRemove);
                    ddlSubject3.Items.Remove(itemToRemoveSubject2);
                    if (ddlSubject3.Items.Count == 1)
                    {
                        objCommon.DisplayMessage(this.Page, "Subject 3 Details Not Found Please Contact Online Admission Department.", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Subject 3 Details Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                ddlSubject3.Items.Clear();
                ddlSubject3.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlSubject3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubject3.SelectedIndex > 0)
            {
                ddlSubject4.Items.Clear();
                ddlSubject4.Items.Insert(0, "Please Select");

                DataTable ds = (DataTable)ViewState["DYNAMICFILLDROPDOWN"];
                if (ds != null && ds.Rows.Count > 0)
                {
                    ddlSubject4.DataSource = ds;
                    ddlSubject4.DataTextField = "AL_COURSES";
                    ddlSubject4.DataValueField = "ID";
                    ddlSubject4.DataBind();

                    ListItem itemToRemove = ddlSubject1.Items.FindByText(ddlSubject1.SelectedItem.Text);
                    ListItem itemToRemoveSubject2 = ddlSubject2.Items.FindByText(ddlSubject2.SelectedItem.Text);
                    ListItem itemToRemoveSubject3 = ddlSubject3.Items.FindByText(ddlSubject3.SelectedItem.Text);
                    ddlSubject4.Items.Remove(itemToRemove);
                    ddlSubject4.Items.Remove(itemToRemoveSubject2);
                    ddlSubject4.Items.Remove(itemToRemoveSubject3);
                    if (ddlSubject4.Items.Count == 1)
                    {
                        objCommon.DisplayMessage(this.Page, "Subject 4 Details Not Found Please Contact Online Admission Department.", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Subject 4 Details Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                ddlSubject4.Items.Clear();
                ddlSubject4.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlOLType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlOLType.SelectedIndex > 0)
            {
                NewUser objNewUr = new NewUser();
                objNewUr.UserNo = Convert.ToInt32(ViewState["STUDENT_USERNO"]);
                objNewUr.StlQno = Convert.ToInt32(ddlOLType.SelectedValue);   // NOTE STLQNO USE AS A/L TYPE FOR COMMAND TYPE 3
                objNewUr.Command_type = 3;
                ViewState["DYNAMICFILLDROPDOWNOL"] = null;
                olddlsub1.Items.Clear();
                olddlsub1.Items.Insert(0, "Please Select");
                olddlgrade1.Items.Clear();
                olddlgrade1.Items.Insert(0, "Please Select");
                olddlgrade2.Items.Clear();
                olddlgrade2.Items.Insert(0, "Please Select");
                olddlgrade3.Items.Clear();
                olddlgrade3.Items.Insert(0, "Please Select");
                olddlgrade4.Items.Clear();
                olddlgrade4.Items.Insert(0, "Please Select");

                olddlgrade5.Items.Clear();
                olddlgrade5.Items.Insert(0, "Please Select");

                olddlgrade6.Items.Clear();
                olddlgrade6.Items.Insert(0, "Please Select");

                if (ddlOLType.SelectedValue == "2")
                {
                    objCommon.FillDropDownList(ddlolStream, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO = 8", "STREAMNAME");
                    ddlolStream.SelectedIndex = 1;
                }
                else if (ddlOLType.SelectedValue == "3")
                {
                    objCommon.FillDropDownList(ddlolStream, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO = 9", "STREAMNAME");
                    ddlolStream.SelectedIndex = 1;
                }
                else if (ddlOLType.SelectedValue == "4")
                {
                    objCommon.FillDropDownList(ddlolStream, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO = 7", "STREAMNAME");
                    ddlolStream.SelectedIndex = 1;
                }
                else
                {
                    objCommon.FillDropDownList(ddlolStream, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO NOT IN(7,8,9)", "STREAMNAME");
                }

                DataSet ds = objnuc.GetAppliedUserEducationDetails(objNewUr);
                if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    ViewState["DYNAMICFILLDROPDOWNOL"] = ds.Tables[2];
                    olddlsub1.DataSource = ds.Tables[2];
                    olddlsub1.DataTextField = "OL_COURSES";
                    olddlsub1.DataValueField = "ID";
                    olddlsub1.DataBind();
                }
                if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                {
                    olddlgrade1.DataSource = ds.Tables[3];
                    olddlgrade1.DataValueField = "ID";
                    olddlgrade1.DataTextField = "GRADES";
                    olddlgrade1.DataBind();

                    olddlgrade2.DataSource = ds.Tables[3];
                    olddlgrade2.DataValueField = "ID";
                    olddlgrade2.DataTextField = "GRADES";
                    olddlgrade2.DataBind();

                    olddlgrade3.DataSource = ds.Tables[3];
                    olddlgrade3.DataValueField = "ID";
                    olddlgrade3.DataTextField = "GRADES";
                    olddlgrade3.DataBind();

                    olddlgrade4.DataSource = ds.Tables[3];
                    olddlgrade4.DataValueField = "ID";
                    olddlgrade4.DataTextField = "GRADES";
                    olddlgrade4.DataBind();

                    olddlgrade5.DataSource = ds.Tables[3];
                    olddlgrade5.DataValueField = "ID";
                    olddlgrade5.DataTextField = "GRADES";
                    olddlgrade5.DataBind();


                    olddlgrade6.DataSource = ds.Tables[3];
                    olddlgrade6.DataValueField = "ID";
                    olddlgrade6.DataTextField = "GRADES";
                    olddlgrade6.DataBind();


                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Subject 1 Details Not Found Please Contact Online Admission Department.", this.Page);
                    return;
                }
            }
            else
            {
                olddlsub1.Items.Clear();
                olddlsub1.Items.Insert(0, "Please Select");
                olddlsubj2.Items.Clear();
                olddlsubj2.Items.Insert(0, "Please Select");
                olddlsub3.Items.Clear();
                olddlsub3.Items.Insert(0, "Please Select");
                olddlsub4.Items.Clear();
                olddlsub4.Items.Insert(0, "Please Select");

                olddlgrade1.Items.Clear();
                olddlgrade1.Items.Insert(0, "Please Select");
                olddlgrade2.Items.Clear();
                olddlgrade2.Items.Insert(0, "Please Select");
                olddlgrade3.Items.Clear();
                olddlgrade3.Items.Insert(0, "Please Select");
                olddlgrade4.Items.Clear();
                olddlgrade4.Items.Insert(0, "Please Select");

                olddlgrade5.Items.Clear();
                olddlgrade5.Items.Insert(0, "Please Select");

                olddlgrade6.Items.Clear();
                olddlgrade6.Items.Insert(0, "Please Select");

                ddlolStream.Items.Clear();
                ddlolStream.Items.Insert(0, "Please Select");

                objCommon.DisplayMessage(this.Page, "Please Select O/L Type.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void olddlsub5_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (olddlsub5.SelectedIndex > 0)
            {
                olddlsub6.Items.Clear();
                olddlsub6.Items.Insert(0, "Please Select");

                DataTable ds = (DataTable)ViewState["DYNAMICFILLDROPDOWNOL"];
                if (ds != null && ds.Rows.Count > 0)
                {
                    olddlsub6.DataSource = ds;
                    olddlsub6.DataTextField = "OL_COURSES";
                    olddlsub6.DataValueField = "ID";
                    olddlsub6.DataBind();

                    ListItem itemToRemove = olddlsub1.Items.FindByValue(olddlsub1.SelectedValue);
                    ListItem itemToRemoveSubject2 = olddlsub1.Items.FindByValue(olddlsubj2.SelectedValue);
                    ListItem itemToRemoveSubject3 = olddlsub3.Items.FindByValue(olddlsub3.SelectedValue);
                    ListItem itemToRemoveSubject4 = olddlsub4.Items.FindByValue(olddlsub4.SelectedValue);
                    ListItem itemToRemoveSubject5 = olddlsub5.Items.FindByValue(olddlsub5.SelectedValue);
                    olddlsub6.Items.Remove(itemToRemove);
                    olddlsub6.Items.Remove(itemToRemoveSubject2);
                    olddlsub6.Items.Remove(itemToRemoveSubject3);
                    olddlsub6.Items.Remove(itemToRemoveSubject4);
                    olddlsub6.Items.Remove(itemToRemoveSubject5);
                    if (olddlsub6.Items.Count == 1)
                    {
                        objCommon.DisplayMessage(this.Page, "Subject 6 Details Not Found Please Contact Online Admission Department.", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Subject 6 Details Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                olddlsub6.Items.Clear();
                olddlsub6.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void olddlsub4_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (olddlsub4.SelectedIndex > 0)
            {
                olddlsub5.Items.Clear();
                olddlsub5.Items.Insert(0, "Please Select");

                DataTable ds = (DataTable)ViewState["DYNAMICFILLDROPDOWNOL"];
                if (ds != null && ds.Rows.Count > 0)
                {
                    olddlsub5.DataSource = ds;
                    olddlsub5.DataTextField = "OL_COURSES";
                    olddlsub5.DataValueField = "ID";
                    olddlsub5.DataBind();

                    ListItem itemToRemove = olddlsub1.Items.FindByValue(olddlsub1.SelectedValue);
                    ListItem itemToRemoveSubject2 = olddlsub1.Items.FindByValue(olddlsubj2.SelectedValue);
                    ListItem itemToRemoveSubject3 = olddlsub3.Items.FindByValue(olddlsub3.SelectedValue);
                    ListItem itemToRemoveSubject4 = olddlsub3.Items.FindByValue(olddlsub4.SelectedValue);
                    olddlsub5.Items.Remove(itemToRemove);
                    olddlsub5.Items.Remove(itemToRemoveSubject2);
                    olddlsub5.Items.Remove(itemToRemoveSubject3);
                    olddlsub5.Items.Remove(itemToRemoveSubject4);
                    if (olddlsub5.Items.Count == 1)
                    {
                        objCommon.DisplayMessage(this.Page, "Subject 5 Details Not Found Please Contact Online Admission Department.", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Subject 5 Details Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                olddlsub6.Items.Clear();
                olddlsub6.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void olddlsub3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (olddlsub3.SelectedIndex > 0)
            {
                olddlsub4.Items.Clear();
                olddlsub4.Items.Insert(0, "Please Select");

                DataTable ds = (DataTable)ViewState["DYNAMICFILLDROPDOWNOL"];
                if (ds != null && ds.Rows.Count > 0)
                {
                    olddlsub4.DataSource = ds;
                    olddlsub4.DataTextField = "OL_COURSES";
                    olddlsub4.DataValueField = "ID";
                    olddlsub4.DataBind();

                    ListItem itemToRemove = olddlsub1.Items.FindByValue(olddlsub1.SelectedValue);
                    ListItem itemToRemoveSubject2 = olddlsub1.Items.FindByValue(olddlsubj2.SelectedValue);
                    ListItem itemToRemoveSubject3 = olddlsub3.Items.FindByValue(olddlsub3.SelectedValue);
                    olddlsub4.Items.Remove(itemToRemove);
                    olddlsub4.Items.Remove(itemToRemoveSubject2);
                    olddlsub4.Items.Remove(itemToRemoveSubject3);
                    if (olddlsub4.Items.Count == 1)
                    {
                        objCommon.DisplayMessage(this.Page, "Subject 4 Details Not Found Please Contact Online Admission Department.", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Subject 4 Details Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                olddlsub4.Items.Clear();
                olddlsub4.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void olddlsubj2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (olddlsubj2.SelectedIndex > 0)
            {
                olddlsub3.Items.Clear();
                olddlsub3.Items.Insert(0, "Please Select");
                olddlsub4.Items.Clear();
                olddlsub4.Items.Insert(0, "Please Select");

                DataTable ds = (DataTable)ViewState["DYNAMICFILLDROPDOWNOL"];
                if (ds != null && ds.Rows.Count > 0)
                {
                    olddlsub3.DataSource = ds;
                    olddlsub3.DataTextField = "OL_COURSES";
                    olddlsub3.DataValueField = "ID";
                    olddlsub3.DataBind();

                    ListItem itemToRemove = olddlsub1.Items.FindByValue(olddlsub1.SelectedValue);
                    ListItem itemToRemoveSubject2 = olddlsubj2.Items.FindByValue(olddlsubj2.SelectedValue);
                    olddlsub3.Items.Remove(itemToRemove);
                    olddlsub3.Items.Remove(itemToRemoveSubject2);
                    if (olddlsub3.Items.Count == 1)
                    {
                        objCommon.DisplayMessage(this.Page, "Subject 3 Details Not Found Please Contact Online Admission Department.", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Subject 3 Details Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                olddlsub3.Items.Clear();
                olddlsub3.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void olddlsub1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (olddlsub1.SelectedIndex > 0)
            {
                olddlsubj2.Items.Clear();
                olddlsubj2.Items.Insert(0, "Please Select");
                olddlsub3.Items.Clear();
                olddlsub3.Items.Insert(0, "Please Select");
                olddlsub4.Items.Clear();
                olddlsub4.Items.Insert(0, "Please Select");

                DataTable ds = (DataTable)ViewState["DYNAMICFILLDROPDOWNOL"];
                if (ds != null && ds.Rows.Count > 0)
                {
                    olddlsubj2.DataSource = ds;
                    olddlsubj2.DataTextField = "OL_COURSES";
                    olddlsubj2.DataValueField = "ID";
                    olddlsubj2.DataBind();

                    ListItem itemToRemove = olddlsub1.Items.FindByValue(olddlsub1.SelectedValue);
                    olddlsubj2.Items.Remove(itemToRemove);
                    if (olddlgrade2.Items.Count == 1)
                    {
                        objCommon.DisplayMessage(this.Page, "Subject 2 Details Not Found Please Contact Online Admission Department.", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Subject 2 Details Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                olddlgrade2.Items.Clear();
                olddlgrade2.Items.Insert(0, "Please Select");
                olddlsub3.Items.Clear();
                olddlsub3.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void rdbQuestion1PHD_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbQuestion1PHD.SelectedValue == "1")
            {
                divDetails.Visible = true;
            }
            else
            {
                divDetails.Visible = false;
                txtQuestionDetailsPHD.Text = "";
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void rdbQuestion2PHD_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbQuestion2PHD.SelectedValue == "1")
            {
                divDetails1.Visible = true;
            }
            else
            {
                divDetails1.Visible = false;
                txtQuestion1DetailsPHD.Text = "";
            }
        }
        catch (Exception ex)
        {

        }
    }
    //Added by swapnil thakare on dated 14-12-2021
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
                session = objCommon.LookUp("ACD_DEMAND", "(isnull(SESSIONNO,0))", "IDNO=" + Convert.ToInt32(ViewState["stuinfoidno"]) + " AND ISNULL(CAN,0) = 0 AND ISNULL(DELET,0) = 0 AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + " AND PAYTYPENO =" + Convert.ToInt32(ViewState["paytypeno"]) + " AND ADMBATCHNO=" + Convert.ToInt32(ViewState["batchno"]) + "and RECIEPT_CODE=" + "'" + Convert.ToString(ddlReceiptType.SelectedValue) + "'");
                if (session != "")
                {
                    ds = feeController.GetFeeItems_Data_ForOnlinePayment(Convert.ToInt32(session), Convert.ToInt32(ViewState["stuinfoidno"]), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(ddlReceiptType.SelectedValue), Convert.ToInt32(ViewState["paytypeno"]), Convert.ToInt32(ViewState["batchno"]));//, ref status);
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
                else
                {
                    CheckPrevDemand();

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
            ////GenerateChallan();


            if (txtchallanAmount.Text == "0" || txtchallanAmount.Text == "" || txtchallanAmount.Text == "0.00")
            {
                objCommon.DisplayMessage(this.Page, "Total Amount Cannot be Less than or Equal to Zero !!", this.Page);
                return;
            }
            else
            {
                if (Convert.ToString(ViewState["INSTALLMENT_DETAILS"]) != "1")
                {
                    if (Convert.ToDecimal(ViewState["demandamounttobepaid"].ToString()) < Convert.ToDecimal(txtchallanAmount.Text))
                    {
                        objCommon.DisplayMessage(updBulkReg, "Paid amount should not be greater than applicable amount of the program !!!", this.Page);
                        return;
                    }
                }
            }

            string session = string.Empty;
            session = GetSession();
            int Count = Convert.ToInt32(objCommon.LookUp("ACD_DCR_TEMP", "COUNT(*)", "IDNO=" + Convert.ToInt32(ViewState["stuinfoidno"]) + " AND PAY_SERVICE_TYPE=2"));

            if (FuChallan.HasFile)
            {
                if (ViewState["action"].ToString() != "Edit")
                {
                    ChallanCopy = objCommon.GetImageData(FuChallan);
                    CustomStatus cs = CustomStatus.Others;
                    //cs = (CustomStatus)feeController.InsertChallanCopyDetailserp(Convert.ToInt32(Session["idno"]), txtChallanId.Text, txtchallanAmount.Text, ChallanCopy, txtTransactionNo.Text, txtPaymentdate.Text, Convert.ToString(ddlbank.SelectedItem.Text), Convert.ToString(txtBranchName.Text));

                    int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, Convert.ToInt32(ViewState["stuinfoidno"]) + "_ERP_" + Count + "_Deposit_Slip", FuChallan, ChallanCopy);
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
                        int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, Convert.ToInt32(ViewState["stuinfoidno"]) + "_ERP_" + Convert.ToString(ViewState["RANDOM_NUMBER"]) + "_Deposit_Slip", FuChallan, ChallanCopy);
                        if (retval == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                            return;
                        }
                        ViewState["action"] = "Edit";
                        SubmitOfflineData(ChallanCopy);
                        rdPaymentOption.SelectedValue = null;
                        Session["EDITCHALLANCOPYDETAILS"] = null;
                    }
                }
            }
            else
            {

                objCommon.DisplayMessage(updChallan, "Please Upload Deposit Slip !!!", this.Page);
                rdPaymentOption.SelectedValue = null;
                return;
            }
        }
        catch (Exception ex)
        {

        }
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

    protected void GenerateChallan()
    {
        try
        {
            string session = objCommon.LookUp("ACD_ADMISSION_CONFIG", "MAX(ADMBATCH)", string.Empty);
            int PAYMENTTYPE = 0;
            if (rdPaymentOption.SelectedValue == "0")
            {
                objStudentFees.UserNo = Convert.ToInt32(Session["userno"]);// ((UserDetails)(ViewState["stuinfoidno"])).UserNo;
                objStudentFees.SessionNo = session;
                objStudentFees.OrderID = lblOrderID.Text;
                objStudentFees.Amount = Convert.ToDouble(txtchallanAmount.Text);
                objStudentFees.FeeType = "Offline";
                PAYMENTTYPE = 1;

                // Additional Parameters
                objStudentFees.TransDate = System.DateTime.Today;
                objStudentFees.Bankno = Convert.ToInt32(ddlbank.SelectedValue);
                objStudentFees.BranchName = txtBranchName.Text;
                objStudentFees.Ddno = 0;
                objStudentFees.Catapplied = false;
                objStudentFees.Unitno = 0;
                objStudentFees.TransID = "";
            }
            int result = 0;

            result = ObjNuc.SubmitFeesofStudent(objStudentFees, PAYMENTTYPE, 1);
            if (result > 0)
            {

                //objCommon.DisplayMessage(this.Page, "Challan Generated Successfully !!!", this.Page);

                fillDetails();


                if (rdPaymentOption.SelectedValue == "0")
                {
                    // btnPayment.Style.Add("display", "none");

                }


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
            //Session["stuinfoenrollno"] 
            //Session["stuinfofullname"] 
            //ViewState["stuinfoidno"] 
            string USERNOACC = Convert.ToString(objCommon.LookUp("USER_ACC", "DISTINCT UA_NO", "UA_IDNO='" + ViewState["stuinfoidno"] + "'"));
            ViewState["USERACC"] = USERNOACC;
            ds = feeController.getPaymentDetails(Convert.ToInt32(ViewState["USERACC"]));
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



    //Added by swapnil thakare on dated 14-12-2021

    //public void SubmitOfflineData(byte[] ChallanCopy)
    //{
    //    try
    //    {

    //        // CreateCustomerRef();
    //        string session = string.Empty;
    //        session = GetSession();

    //        int result = 0;
    //        int DM_NO = 0;


    //        DM_NO = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "DM_NO", "IDNO=" + Convert.ToInt32(ViewState["stuinfoidno"]) + " AND SESSIONNO=" + Convert.ToInt32(session) + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + "and RECIEPT_CODE=" + "'" + Convert.ToString(ddlReceiptType.SelectedValue) + "'"));

    //        if (ViewState["action"] != null)
    //        {
    //            if (ViewState["action"].ToString().Equals("add"))
    //            {

    //                if (DM_NO > 0)
    //                {
    //                    if (ddlReceiptType.SelectedValue == "HF")//txtBranchName  ddlbank
    //                    {
    //                        result = feeController.InsertOfflinePayment_DCR_TEMP(DM_NO, Convert.ToInt32(ViewState["stuinfoidno"]), Convert.ToInt32(ViewState["SESSIONNO"]), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(lblOrderID.Text), 2, Convert.ToInt32(2), ChallanCopy, Convert.ToInt32(ddlbank.SelectedValue), Convert.ToString(ddlbank.SelectedItem.Text), txtBranchName.Text, txtchallanAmount.Text);
    //                    }
    //                    else
    //                    {
    //                        result = feeController.InsertOfflinePayment_DCR_TEMP(DM_NO, Convert.ToInt32(ViewState["stuinfoidno"]), Convert.ToInt32(ViewState["SESSIONNO"]), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(lblOrderID.Text), 2, Convert.ToInt32(2), ChallanCopy, Convert.ToInt32(ddlbank.SelectedValue), Convert.ToString(ddlbank.SelectedItem.Text), txtBranchName.Text, txtchallanAmount.Text);
    //                    }
    //                }
    //                else
    //                {
    //                    objCommon.DisplayUserMessage(this.updBulkReg, "Demand not created!", this.Page);
    //                    return;
    //                }
    //                if (result > 0)
    //                {
    //                    email();
    //                    objCommon.DisplayMessage(this.Page, "Deposit Data Saved Succesfully !!!", this.Page);
    //                    ViewState["action"] = "add";
    //                    txtChallanId.Text = "";
    //                    txtchallanAmount.Text = "";
    //                    rdPaymentOption.SelectedValue = null;
    //                    ddlbank.SelectedValue = null;
    //                    txtBranchName.Text = "";
    //                    txtPaymentdate.Text = "";
    //                    fillDetails();
    //                }
    //                else
    //                {
    //                    lblStatus.Visible = true;
    //                    objCommon.DisplayUserMessage(updBulkReg, "Failed To Done Offline Payment.", this.Page);
    //                    return;
    //                }
    //            }


    //            else if (ViewState["action"].ToString().Equals("Edit")) // ViewState["ORDER_ID"]
    //            {
    //                result = feeController.UpdateOfflinePayment_DCR_TEMP(Convert.ToInt32(ViewState["srno"].ToString()), DM_NO, Convert.ToInt32(ViewState["stuinfoidno"].ToString()), Convert.ToInt32(ViewState["SESSIONNO"].ToString()), Convert.ToInt32(ViewState["semesterno"].ToString()), Convert.ToString(ViewState["ORDER_ID"].ToString()), Convert.ToInt32(2), Convert.ToInt32(2), ChallanCopy, Convert.ToInt32(ddlbank.SelectedValue), Convert.ToString(ddlbank.SelectedItem.Text), txtBranchName.Text, txtchallanAmount.Text);
    //                if (result > 0)
    //                {
    //                    email();
    //                    objCommon.DisplayMessage(this.Page, "Deposit Data Updated Succesfully!", this.Page);
    //                    ViewState["action"] = "add";
    //                    txtChallanId.Text = "";
    //                    txtchallanAmount.Text = "";
    //                    rdPaymentOption.SelectedValue = null;
    //                    ddlbank.SelectedValue = null;
    //                    txtBranchName.Text = "";
    //                    txtPaymentdate.Text = "";
    //                    fillDetails();
    //                }
    //                else
    //                {
    //                    lblStatus.Visible = true;
    //                    objCommon.DisplayUserMessage(updBulkReg, "Failed To Update Offline Payment.", this.Page);
    //                    return;
    //                }

    //            }
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.SubmitData-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}


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
            DM_NO = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "DM_NO", "IDNO=" + Convert.ToInt32(ViewState["stuinfoidno"]) + " AND SESSIONNO=" + Convert.ToInt32(session) + " AND ISNULL(CAN,0) = 0 AND ISNULL(DELET,0) = 0 AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"]) + "and RECIEPT_CODE=" + "'" + Convert.ToString(ddlReceiptType.SelectedValue) + "'"));
            int Count = Convert.ToInt32(objCommon.LookUp("ACD_DCR_TEMP", "COUNT(*)", "IDNO=" + Convert.ToInt32(ViewState["stuinfoidno"]) + " AND PAY_SERVICE_TYPE=2"));
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {

                    if (DM_NO > 0)
                    {
                        if (ddlReceiptType.SelectedValue == "HF")//txtBranchName  ddlbank
                        {
                            result = feeController.InsertOfflinePayment_DCR_TEMP(DM_NO, Convert.ToInt32(ViewState["stuinfoidno"]), Convert.ToInt32(ViewState["SESSIONNO"]), Convert.ToDateTime(txtPaymentdate.Text), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(lblOrderID.Text), 2, Convert.ToInt32(2), ChallanCopy, Convert.ToInt32(ddlbank.SelectedValue), Convert.ToString(ddlbank.SelectedItem.Text), txtBranchName.Text, txtchallanAmount.Text, Convert.ToInt32(ViewState["stuinfoidno"]) + "_ERP_" + Count + "_Deposit_Slip" + Ext, Convert.ToString(ViewState["INSTALL_NO"]));
                        }
                        else
                        {
                            result = feeController.InsertOfflinePayment_DCR_TEMP(DM_NO, Convert.ToInt32(ViewState["stuinfoidno"]), Convert.ToInt32(ViewState["SESSIONNO"]), Convert.ToDateTime(txtPaymentdate.Text), Convert.ToInt32(ViewState["semesterno"]), Convert.ToString(lblOrderID.Text), 2, Convert.ToInt32(2), ChallanCopy, Convert.ToInt32(ddlbank.SelectedValue), Convert.ToString(ddlbank.SelectedItem.Text), txtBranchName.Text, txtchallanAmount.Text, Convert.ToInt32(ViewState["stuinfoidno"]) + "_ERP_" + Count + "_Deposit_Slip" + Ext, Convert.ToString(ViewState["INSTALL_NO"]));
                        }
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(this.updBulkReg, "Demand not created!", this.Page);
                        return;
                    }
                    if (result > 0)
                    {
                        //email();
                        objCommon.DisplayMessage(this.Page, "Deposit Data Saved Succesfully !!!", this.Page);
                        ViewState["action"] = "add";
                        txtChallanId.Text = "";
                        txtchallanAmount.Text = "";
                        rdPaymentOption.SelectedValue = null;
                        ddlbank.SelectedValue = null;
                        txtBranchName.Text = "";
                        txtPaymentdate.Text = "";
                        fillDetails();
                    }
                    else
                    {
                        lblStatus.Visible = true;
                        objCommon.DisplayUserMessage(this.Page, "Failed To Done Offline Payment.", this.Page);
                        return;
                    }
                }


                else if (ViewState["action"].ToString().Equals("Edit")) // ViewState["ORDER_ID"]
                {
                    result = feeController.UpdateOfflinePayment_DCR_TEMP(Convert.ToInt32(ViewState["srno"].ToString()), DM_NO, Convert.ToInt32(ViewState["stuinfoidno"].ToString()), Convert.ToInt32(ViewState["SESSIONNO"].ToString()), Convert.ToDateTime(txtPaymentdate.Text), Convert.ToInt32(ViewState["semesterno"].ToString()), Convert.ToString(ViewState["ORDER_ID"].ToString()), Convert.ToInt32(2), Convert.ToInt32(2), ChallanCopy, Convert.ToInt32(ddlbank.SelectedValue), Convert.ToString(ddlbank.SelectedItem.Text), txtBranchName.Text, txtchallanAmount.Text, Convert.ToInt32(ViewState["stuinfoidno"]) + "_ERP_" + Convert.ToString(ViewState["RANDOM_NUMBER"]) + "_Deposit_Slip" + Ext);
                    if (result > 0)
                    {

                        objCommon.DisplayMessage(this.Page, "Deposit Data Updated Succesfully!", this.Page);
                        ViewState["action"] = "add";
                        txtChallanId.Text = "";
                        txtchallanAmount.Text = "";
                        rdPaymentOption.SelectedValue = null;
                        ddlbank.SelectedValue = null;
                        txtBranchName.Text = "";
                        txtPaymentdate.Text = "";
                        fillDetails();
                    }
                    else
                    {
                        lblStatus.Visible = true;
                        objCommon.DisplayUserMessage(this.Page, "Failed To Update Offline Payment.", this.Page);
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
            if (Convert.ToString(Session["Offer_Accept"]) == "1")
            {
                divacceptance.Attributes.Add("class", "finished");
            }

            DataSet ds = null; int count = 0;
            ds = objdocContr.getModuleOfferedCourses(Convert.ToInt32(ViewState["stuinfoidno"]));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                count++;
                Panel2.Visible = true;
                lvOfferedSubject.DataSource = ds.Tables[0];
                lvOfferedSubject.DataBind();
                if (ds.Tables[0].Rows[0]["EXAM_REGISTERED"].ToString() == "1")
                {
                    //btnSubmitOffer.Visible = false;
                    objCommon.DisplayMessage(this.Page, "Module registration already done", this.Page);
                }
                else
                {
                    btnSubmitOffer.Visible = true;
                }
            }
            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                count++;
                Panel3.Visible = true;
                lvcoursetwo.DataSource = ds.Tables[1];
                lvcoursetwo.DataBind();
                if (ds.Tables[1].Rows[0]["EXAM_REGISTERED"].ToString() == "1")
                {
                    //btnSubmitOffer.Visible = false;
                    objCommon.DisplayMessage(this.Page, "Module registration already done", this.Page);
                }
                else
                {
                    btnSubmitOffer.Visible = true;
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
                //btnSubmitOffer.Visible = false;

                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    // btnSubmitOffer.Visible = false;
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
                    }
                }

                //objCommon.DisplayMessage(this.Page, "Modules registration already done", this.Page);
                //}
                //else
                //{
                //    btnSubmitOffer.Visible = true;
                //}
            }
            if(count == 0)
            {
                lvOfferedSubject.DataSource = null;
                lvOfferedSubject.DataBind();
                objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
                btnSubmitOffer.Visible = false;
            }
            if (Convert.ToInt32(ViewState["EducationDetails"]) > 0)
            {
                divOnlineDetails.Attributes.Remove("class");
                divacceptance.Attributes.Add("class", "finished");
            }
            else
            {
                divOnlineDetails.Attributes.Remove("class");
            }
            if (Convert.ToInt32(ViewState["DocumentDetails"]) > 0)
            {
                divlkuploaddocument.Attributes.Remove("class");
                divlkuploaddocument.Attributes.Add("class", "finished");
            }
            else
            {
                divlkuploaddocument.Attributes.Remove("class");
            }
            if (Convert.ToInt32(ViewState["DocumentDetails"]) > 0)
            {
                divlkModuleOffer.Attributes.Remove("class");
                divlkModuleOffer.Attributes.Add("class", "active");
            }
            else
            {
                divlkModuleOffer.Attributes.Remove("class");
                divlkModuleOffer.Attributes.Add("class", "active");
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

                int output = objdocContr.InsertModuleRegistration(Convert.ToInt32(ViewState["stuinfoidno"]), Coursenos, Ccode, CourseNames, Credits, Subids, licUano);

                if (output != -99 && output != 99)
                {
                    objCommon.DisplayMessage(this.Page, "Record Added Successfully", this.Page);
                    btnSubmitOffer.Visible = false;
                    //  lkpayment_Click(new object(), new EventArgs());
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Error", this.Page);
                    // Clear();
                }
            }
            else
            {
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
        try
        {
            pnlStudentsFees.Visible = true;
            payoption.Visible = true;
            divViewpayment.Visible = true;
            BindInstallment();
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void rdPaymentOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdPaymentOption.SelectedValue == "0")
        {
            divUploadChallan.Visible = true;
            divUploadgENChallan.Visible = true;
            //pnlStudentsFees.Visible = false;
            //divShowPayment.Visible = false;
            btnSubmit.Visible = false;
            btnCancel.Visible = false;
            divShowPay.Visible = false;
        }
        else
        {
            //divShowPayment.Visible = true;
            divUploadChallan.Visible = false;
            btnSubmit.Visible = false;
            btnCancel.Visible = false;
            divShowPay.Visible = true;
        }
    }
    //added by  aashna
    protected void btnGenerateChallan_Click(object sender, EventArgs e)
    {
        try
        {
            int result = 0;
            int PAYMENTTYPE = 0;
            result = ObjNuc.SubmitFeesofStudent(objStudentFees, PAYMENTTYPE, 1);
            if (result > 0)
            {
                fillDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#modelBank').modal('show')", true);
            }
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
                ImageViewer.Visible = true;
                ltEmbed.Visible = false;
                ImageViewer.ImageUrl = "~/showimage.aspx?id=" + Convert.ToInt32(ViewState["stuinfoidno"]) + "&type=STUDENT";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
            }
            else
            {
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
                    ViewState["filePath_Show"] = filePath;
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);

                    byte[] bytes = System.IO.File.ReadAllBytes(filePath);

                    string Base64String = Convert.ToBase64String(bytes);
                    Session["Base64String"] = Base64String;
                    Session["Base64StringType"] = "application/pdf";
                    //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"700px\" height=\"400px\">";
                    //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    //embed += "</object>";
                    //ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
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
            string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(ViewState["stuinfoidno"]);
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
        lkModuleRegistration_Click(new object(), new EventArgs());
    }


    ////static async Task Executefinance(string message, string toEmailId, string subject, string sudname, string date, string amount, string bank, string branch, string username, string intake, string sendemail, string emailpass)
    ////{

    ////    try
    ////    {
    ////        SmtpMail oMail = new SmtpMail("TryIt");
    ////        oMail.From = sendemail;
    ////        oMail.To = toEmailId;
    ////        oMail.Subject = subject;
    ////        oMail.HtmlBody = message;
    ////        oMail.HtmlBody = oMail.HtmlBody.Replace("{Intake Name}", intake.ToString());
    ////        oMail.HtmlBody = oMail.HtmlBody.Replace("{Application ID}", username.ToString());
    ////        oMail.HtmlBody = oMail.HtmlBody.Replace("{Candidate Name}", sudname.ToString());
    ////        oMail.HtmlBody = oMail.HtmlBody.Replace("{Amount}", amount.ToString());
    ////        oMail.HtmlBody = oMail.HtmlBody.Replace("{Payment Date}", date.ToString());
    ////        oMail.HtmlBody = oMail.HtmlBody.Replace("{Bank Name}", bank.ToString());
    ////        oMail.HtmlBody = oMail.HtmlBody.Replace("{Bank Branch Name}", branch.ToString());
    ////        SmtpServer oServer = new SmtpServer("smtp.live.com");
    ////        oServer.User = sendemail;
    ////        oServer.Password = emailpass;
    ////        oServer.Port = 587;
    ////        oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
    ////        EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
    ////        oSmtp.SendMail(oServer, oMail);

    ////    }
    ////    catch (Exception ex)
    ////    {

    ////    }
    ////}



    ////static async Task Executecandidate(string message, string toEmailId, string subject, string sudname, string username, string sendemail, string emailpass)
    ////{

    ////    try
    ////    {
    ////        SmtpMail oMail = new SmtpMail("TryIt");
    ////        oMail.From = sendemail;
    ////        oMail.To = toEmailId;
    ////        oMail.Subject = subject;
    ////        oMail.HtmlBody = message;
    ////        oMail.HtmlBody = oMail.HtmlBody.Replace("[UA_FULLNAME]", sudname.ToString());
    ////        oMail.HtmlBody = oMail.HtmlBody.Replace("{Application ID}", username.ToString());
    ////        SmtpServer oServer = new SmtpServer("smtp.live.com");
    ////        oServer.User = sendemail;
    ////        oServer.Password = emailpass;
    ////        oServer.Port = 587;
    ////        oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
    ////        EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
    ////        oSmtp.SendMail(oServer, oMail);

    ////    }
    ////    catch (Exception ex)
    ////    {

    ////    }
    ////}

    //ADDED BY AASHNA 13-01-2022
    //protected void email()
    //{
    //    //STARTED BY AASHNA 12-01-2022
    //    DataSet dsUserContact = null;
    //    string message = "", subject = "", date = "", amount = "", intake = "",
    //    EMAIL = "", tranid = "", NAME = "", bankname = "", branch = "", username = "";
    //    DataSet ds = feeController.GETEMAILDETAILS(Convert.ToInt32(ViewState["USER_NO"]));
    //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    {
    //        date = ds.Tables[0].Rows[0]["REC_DT"].ToString();
    //        amount = ds.Tables[0].Rows[0]["TOTAL_AMT"].ToString();
    //        intake = ds.Tables[0].Rows[0]["BATCHNAME"].ToString();
    //        EMAIL = ds.Tables[0].Rows[0]["EMAILID"].ToString();
    //        tranid = ds.Tables[0].Rows[0]["ORDER_ID"].ToString();
    //        NAME = ds.Tables[0].Rows[0]["NAME"].ToString();
    //        bankname = ds.Tables[0].Rows[0]["BANK_NAME"].ToString();
    //        branch = ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();
    //        username = ds.Tables[0].Rows[0]["ENROLLNMENTNO"].ToString();
    //    }

    //    string FINANCEMAIL = objCommon.LookUp("REFF", "FINANCE_EMAIL", "");
    //    dsUserContact = user.GetEmailTamplateandUserDetails("", "", "", Convert.ToInt32(12));
    //    message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
    //    subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
    //    Executecandidate(message, EMAIL, subject, NAME, username, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
    //    dsUserContact = user.GetEmailTamplateandUserDetails("", "", "", Convert.ToInt32(11));
    //    message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
    //    subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
    //    Executefinance(message, FINANCEMAIL, subject, NAME, date, amount, bankname, branch, username, intake, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
    //    //ENDED BY AASHNA 12-01-2022

    //}

    //protected void btnback_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/ACADEMIC/StudentAllDetails.aspx");
    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindlistserch();
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        upddetails.Visible = false;
        txtSearch.Text = "";
        lblNoRecords.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        //txtIDNo.Text = "";
    }
    protected void btnCancelModal_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        lblNoRecords.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
    }
    protected void ddlALPassesUG_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlALPassesUG.SelectedValue == "6")
            {
                ddlSubject1.Items.Clear();
                ddlSubject1.Items.Insert(0, "Please Select");
                ddlSubject2.Items.Clear();
                ddlSubject2.Items.Insert(0, "Please Select");
                ddlSubject3.Items.Clear();
                ddlSubject3.Items.Insert(0, "Please Select");
                ddlSubject4.Items.Clear();
                ddlSubject4.Items.Insert(0, "Please Select");

                ddlGrade1.Items.Clear();
                ddlGrade1.Items.Insert(0, "Please Select");
                ddlGrade2.Items.Clear();
                ddlGrade2.Items.Insert(0, "Please Select");
                ddlGrade3.Items.Clear();
                ddlGrade3.Items.Insert(0, "Please Select");
                ddlGrade4.Items.Clear();
                ddlGrade4.Items.Insert(0, "Please Select");

                ddlSubject1.Enabled = true;
                ddlSubject2.Enabled = false;
                ddlSubject3.Enabled = false;
                ddlSubject4.Enabled = false;

                ddlGrade1.Enabled = false;
                ddlGrade2.Enabled = false;
                ddlGrade3.Enabled = false;
                ddlGrade4.Enabled = false;

            }
            else if (ddlALPassesUG.SelectedValue == "1")
            {
                ddlALTypeUG_SelectedIndexChanged(new object(), new EventArgs());

                ddlGrade1.Items.Clear();
                ddlGrade1.Items.Insert(0, "Please Select");
                ddlGrade2.Items.Clear();
                ddlGrade2.Items.Insert(0, "Please Select");
                ddlGrade3.Items.Clear();
                ddlGrade3.Items.Insert(0, "Please Select");
                ddlGrade4.Items.Clear();
                ddlGrade4.Items.Insert(0, "Please Select");

                ddlSubject1.Enabled = true;
                ddlSubject2.Enabled = true;
                ddlSubject3.Enabled = true;
                ddlSubject4.Enabled = true;

                ddlGrade1.Enabled = true;
                ddlGrade2.Enabled = true;
                ddlGrade3.Enabled = true;
                ddlGrade4.Enabled = true;
            }
            else
            {
                ddlSubject1.Enabled = true;
                ddlSubject2.Enabled = true;
                ddlSubject3.Enabled = true;
                ddlSubject4.Enabled = true;

                ddlGrade1.Enabled = true;
                ddlGrade2.Enabled = true;
                ddlGrade3.Enabled = true;
                ddlGrade4.Enabled = true;
                ViewState["BindOldSream"] = "1";
                ddlALTypeUG_SelectedIndexChanged(new object(), new EventArgs());
            }
        }
        catch (Exception ex)
        {

        }
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
    protected void btnOfferLetter_Click(object sender, EventArgs e)
    {
        try
        {
            this.ShowOfferLetterReport("Offer Letter", "OfferLetterBulk.rpt", Convert.ToString(ViewState["USER_NO"]));
        }
        catch (Exception ex)
        { 
        
        }
    }
    private void ShowOfferLetterReport(string reportTitle, string rptFileName, string userno)
    {
        try
        {
            string colgname = "SLIIT";// ViewState["College_Name"].ToString().Replace(" ", "_");

            string exporttype = "pdf";
            string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
            //    Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/commonreport.aspx?";
            url += "exporttype=" + exporttype;

            url += "&filename=" + colgname + "." + exporttype;

            url += "&path=~,Reports,Academic," + "OfferLetterBulk.rpt";

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNO=" + userno + ",@P_ADMBATCH=" + Convert.ToInt32(0) + ",@P_DEGREENO=" + Convert.ToInt32(0) + ",@P_ENTERANCE=" + Convert.ToInt32(0);
            // url += "&param=@P_USERNO=" + ((UserDetails)(Session["user"])).UserNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSummarySheet_Click(object sender, EventArgs e)
    {
        try
        {
            ShowConfirmationReport("Final_Confirmation_Report", "AdmisionConfirmSlip.rpt", Convert.ToInt32(ViewState["stuinfoidno"]));
        }
        catch (Exception ex)
        { 
        
        }
    }
    private void ShowConfirmationReport(string reportTitle, string rptFileName, int idno)
    {
        try
        {
            string colgname = "SLIIT";// ViewState["College_Name"].ToString().Replace(" ", "_");

            string exporttype = "pdf";
            string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
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

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            //url += "filename=" + "offerletter" + ".pdf";
            //url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNO=" + userno + ",@P_ADMBATCH=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlApplicantType.SelectedValue) + ",@P_ENTERANCE=" + Convert.ToInt32(ddlAdmcat.SelectedValue);

            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
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
            payoption.Visible = true;
            divViewpayment.Visible = true;
            ViewState["INSTALL_NO"] = Convert.ToString(lnk.CommandArgument);
            ViewState["action"] = "add";
        }
        catch (Exception ex)
        {

        }
    }
    protected void BindInstallment()
    {
        try
        {
            MappingController objmap = new MappingController();
            DataSet ds = null;
            ds = objmap.GetStudentInstallmentDetails(Convert.ToInt32(ViewState["stuinfoidno"]));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                rpInstallment.DataSource = ds.Tables[0];
                rpInstallment.DataBind();
                ViewState["INSTALLMENT_DETAILS"] = "1";
                showOnlinePaymentDetails.Visible = false;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    LinkButton lnkPay = rpInstallment.Items[i].FindControl("lnkPay") as LinkButton;
                    if (Convert.ToString(ds.Tables[0].Rows[i]["RECON"]) == "1")
                    {
                        lnkPay.Visible = false;
                    }
                    else
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[i]["DATE_STATUS"]) == "1")
                        {
                            lnkPay.Visible = false;
                        }
                        else
                        {
                            lnkPay.Visible = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                showOnlinePaymentDetails.Visible = true;
            }
            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                if (Convert.ToString(ds.Tables[1].Rows[0]["CLOSE_BAL"]) != string.Empty)
                {
                    if (Convert.ToString(TotalSum) == "0.00" || Convert.ToString(TotalSum).Substring(0, 1) == "-")
                    {
                        divScholarship.Visible = true;
                        btnSubmitScholorship.Enabled = false;
                        lblremark.Visible = true;
                        lblremark.Text = "You Cannot Used the Scholarship Because Your Payment Is Done.";
                        return;
                    }
                    txtScholarshipAmount.Text = Convert.ToString(ds.Tables[1].Rows[0]["CLOSE_BAL"]);
                    divScholarship.Visible = true;
                    ViewState["SCHOLARSHIP_AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[0]["CLOSE_BAL"]);
                    if (Convert.ToString(txtScholarshipAmount.Text.Substring(0, 1)) == "-")
                    {
                        txtScholarshipAmount.Text = Convert.ToString("0.00");
                        divScholarship.Visible = true;
                        ViewState["SCHOLARSHIP_AMOUNT"] = Convert.ToString("0.00");
                    }
                    if (txtScholarshipAmount.Text.ToString() == "0.00")
                    {
                        divScholarship.Visible = false;
                    }
                }
                else
                {
                    divScholarship.Visible = false;
                }
            }
            else
            {
                divScholarship.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void chkRowsProgram_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int Count = 0;
            foreach (ListViewDataItem items in lstProgramName.Items)
            {
                CheckBox chk = items.FindControl("chkRowsProgram") as CheckBox;
                HiddenField hdfbranchno = items.FindControl("hdfbranchno") as HiddenField;
                Label lbldegree = items.FindControl("lbldegree") as Label;
                Label lblcollegename = items.FindControl("lblcollegename") as Label;
                Label lblarea = items.FindControl("lblarea") as Label;
                Label lblCampus = items.FindControl("lblCampus") as Label;

                if (chk.Checked == true)
                {
                    Count++;
                    objCommon.FillDropDownList(ddlcamous, "ACD_ADMISSION_CONFIG C INNER JOIN ACD_CAMPUS_WISE_INTAKE I ON C.UGPG=I.UGPG AND C.COLLEGE_ID = I.COLLEGE_ID AND C.ADMBATCH = I.ADMBATCH AND C.DEGREENO = I.DEGREENO INNER JOIN ACD_CAMPUS CA ON CA.CAMPUSNO = I.CAMPUSNO", "DISTINCT I.CAMPUSNO", "CAMPUSNAME", "C.UGPG=" + Convert.ToInt32(ViewState["UGPGOTONLINE"]) + " AND C.COLLEGE_ID=" + Convert.ToInt32(lblcollegename.ToolTip.ToString()) + " AND C.ADMBATCH=" + Convert.ToInt32(ViewState["batchno"].ToString()) + " AND C.DEGREENO=" + Convert.ToInt32(lbldegree.ToolTip.ToString()) + " AND I.CAMPUSNO IN (SELECT VALUE FROM DBO.SPLIT(PREFERENCE,'&'))", "");
                }
            }
            if (Count == 0)
            {
                ddlcamous.Items.Clear();
                ddlcamous.Items.Add("Please Select");
                ddlcamous.SelectedItem.Value = "0";
                ddlcamous.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlPerProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlPerDisctrict, "ACD_DISTRICT", "DISTINCT DISTRICTNO", "DISTRICTNAME", "STATENO=" + ddlPerProvince.SelectedValue, "DISTRICTNO");
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSubmitScholorship_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkConfirmPayment.Checked == true)
            {
                chkConfirmPayment.Checked = false;
                if (Convert.ToDecimal(ViewState["SCHOLARSHIP_AMOUNT"]) < Convert.ToDecimal(txtScholarshipAmountAdd.Text))
                {
                    txtScholarshipAmountAdd.Text = "";
                    objCommon.DisplayMessage(this.Page, "Entered Amount can not be greater than Total Scholarship & Wallet Amount !!!", this.Page);
                    return;
                }
                else if (Convert.ToString(ViewState["SCHOLARSHIP_AMOUNT"]) == string.Empty)
                {
                    txtScholarshipAmountAdd.Text = "";
                    ViewState["SCHOLARSHIP_AMOUNT"] = "0.00";
                    objCommon.DisplayMessage(this.Page, "Total Scholarship & Wallet Amount balance is zero !!!", this.Page);
                    return;
                }
                else if (Convert.ToDecimal(ViewState["SCHOLARSHIP_AMOUNT"]) == Convert.ToDecimal(0.00))
                {
                    txtScholarshipAmountAdd.Text = "";
                    ViewState["SCHOLARSHIP_AMOUNT"] = "0.00";
                    objCommon.DisplayMessage(this.Page, "Total Scholarship & Wallet Amount balance is zero !!!", this.Page);
                    return;
                }
                else
                {
                    if (Convert.ToDecimal(txtScholarshipAmountAdd.Text) > 0)
                    {
                        CustomStatus cs = CustomStatus.Others;
                        cs = (CustomStatus)objdocContr.InsertScholorshipAmount(Convert.ToInt32(ViewState["stuinfoidno"]), Convert.ToDecimal(ViewState["SCHOLARSHIP_AMOUNT"].ToString()), Convert.ToDecimal(txtScholarshipAmountAdd.Text), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ViewState["SESSIONNO"]), Convert.ToInt32(1));
                        if (cs == CustomStatus.RecordSaved)
                        {
                            txtScholarshipAmountAdd.Text = "";
                            objCommon.DisplayMessage(this.Page, "Record Saved Successfully !!!", this.Page);
                            GetPreviousReceipt();
                            BindInstallment();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Enter Amount More Than zero !!!", this.Page);
                        txtScholarshipAmountAdd.Text = "";
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please check checkbox", this.Page);
            }
        }
        catch (Exception ex)
        {

        }
    }
}

