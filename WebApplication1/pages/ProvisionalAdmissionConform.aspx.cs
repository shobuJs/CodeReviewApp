using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_CheckStudentInfo : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    StudentRegistration objSReg = new StudentRegistration();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }
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

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();



                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                tblStudent.Visible = false;
            }
        }
        divMsg.InnerHtml = string.Empty;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtRollNo.Text.Trim() == string.Empty)
        {
            objCommon.DisplayMessage("Please Enter Student Roll No.", this.Page);
            return;
        }

        string count = objCommon.LookUp("ACD_STUDENT", "COUNT(*)", "ROLLNO='" + txtRollNo.Text.Trim() + "'");


        if (Convert.ToInt32(count) > 0)
        {
            int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ROLLNO='" + txtRollNo.Text.Trim() + "'"));

            string demandno = objCommon.LookUp("ACD_DEMAND", "COUNT(*)", "IDNO=" + idno + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]));

            if (Convert.ToInt32(demandno) == 0)
            {
                this.ShowDetails();
            }
            else
            {
                this.ShowDetailsAfterConfirm();
            }
        }
        else
        {
            objCommon.DisplayMessage(UpdatePanel1, "This is wrong Roll No.", this.Page);
            tblStudent.Visible = false;
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();

        string studentIDs = objCommon.LookUp("ACD_STUDENT", "IDNO", "ROLLNO='" + txtRollNo.Text.Trim() + "'");

        if (rblStatus.SelectedIndex == 0)
        {

            if (rblPaymentStatus.SelectedIndex == 0)
            {
                string dcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(studentIDs) + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]));
                if (dcrNo != string.Empty && studentIDs != string.Empty)
                {

                    feeController.ReconcileDataForPro(Convert.ToInt32(studentIDs), Convert.ToInt32(Session["currentsession"]));
                    objCommon.DisplayMessage(UpdatePanel1, "This student Admission Successfully", this.Page);

                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Please paid the Fees", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Please Choose the Payment Status YES for Reconcilation", this.Page);
            }
        }

        else
        {
            CustomStatus cs = (CustomStatus)objSC.DeleteCourseRegistered(Convert.ToInt32(studentIDs), Convert.ToInt32(Session["currentsession"]), Convert.ToInt32(lblSemester.ToolTip));

            if (cs.Equals(CustomStatus.RecordDeleted))
                objCommon.DisplayMessage("Provisional Admission Deleted Successfully", this.Page);
            else
                objCommon.DisplayMessage("Error in deleting record...", this.Page);
        }
    }





    private void ClearControls_DemandDraftDetails()
    {

    }

    private void ShowDetails()
    {
        StudentController objSC = new StudentController();
        FeeCollectionController feeController = new FeeCollectionController();

        try
        {
            int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ROLLNO='" + txtRollNo.Text.Trim() + "'"));
            DataTableReader dtr = objSC.GetProStudentDetails(idno);

            if (dtr != null)
            {
                tblStudent.Visible = true;
                if (dtr.Read())
                {
                    lblTempNo.Text = dtr["IDNO"].ToString();
                    string branchname = objCommon.LookUp("ACD_BRANCH", "LONGNAME", "BRANCHNO=" + dtr["branchno"].ToString());
                    lblBranch.Text = branchname;
                    lblBranch.ToolTip = dtr["branchno"].ToString();
                    lblName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                    lblMName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString();
                    lblEnrollNo.Text = dtr["ENROLLNO"] == null ? string.Empty : dtr["ENROLLNO"].ToString();
                    lblDOB.Text = dtr["DOB"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["DOB"]).ToString("dd/MM/yyyy");
                    string semester = objCommon.LookUp("ACD_SEMESTER", "SEMESTERNAME", "SEMESTERNO=" + dtr["SEMESTERNO"].ToString());
                    lblSemester.Text = semester;
                    lblSemester.ToolTip = dtr["SEMESTERNO"].ToString();
                    string category = objCommon.LookUp("ACD_CATEGORY", "CATEGORY", "CATEGORYNO=" + dtr["ADMCATEGORYNO"].ToString());
                    lblCategory.Text = category;
                    string religion = objCommon.LookUp("ACD_RELIGION", "RELIGION", "RELIGIONNO=" + dtr["RELIGIONNO"].ToString());
                    lblReligion.Text = religion;
                    string nation = objCommon.LookUp("ACD_NATIONALITY", "NATIONALITY", "NATIONALITYNO=" + dtr["NATIONALITYNO"].ToString());
                    lblNationality.Text = nation;
                    lblLAdd.Text = dtr["LADDRESS"] == null ? string.Empty : dtr["LADDRESS"].ToString();
                    string city = objCommon.LookUp("ACD_CITY", "CITY", "CITYNO=" + dtr["PCITY"].ToString());
                    lblCity.Text = city;
                    lblLLNo.Text = dtr["LTELEPHONE"] == null ? string.Empty : dtr["LTELEPHONE"].ToString();
                    lblMobNo.Text = dtr["LMOBILE"] == null ? string.Empty : dtr["LMOBILE"].ToString();
                    lblPAdd.Text = dtr["PADDRESS"] == null ? string.Empty : dtr["PADDRESS"].ToString();
                    city = objCommon.LookUp("ACD_CITY", "CITY", "CITYNO=" + dtr["PCITY"].ToString());
                    lblPCity.Text = city;
                    string degree = objCommon.LookUp("ACD_DEGREE", "DEGREENAME", "DEGREENO=" + dtr["DEGREENO"].ToString());
                    lblDegree.Text = degree;
                    string paytype = objCommon.LookUp("ACD_PAYMENTTYPE", "PAYTYPENAME", "PAYTYPENO=" + dtr["PTYPE"].ToString());
                    lblPaymenttype.Text = paytype;
                    lblPaymenttype.ToolTip = dtr["PTYPE"].ToString();

                    int degreeno = Convert.ToInt32(dtr["DEGREENO"]);

                    trPayStatus.Visible = false;
                    trPrint.Visible = true;
                    trSubmit.Visible = false;
                    trStatus.Visible = true;
                    trMsg.Visible = false;
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "This is not a Provisional Admission Student.", this.Page);
                    tblStudent.Visible = false;
                }

            }


        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ProvisionalAdmissionConfirmation.btnSearch_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowDetailsAfterConfirm()
    {
        StudentController objSC = new StudentController();
        FeeCollectionController feeController = new FeeCollectionController();

        try
        {
            int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ROLLNO='" + txtRollNo.Text.Trim() + "'"));
            DataTableReader dtr = objSC.GetProStudentDetails(idno);

            if (dtr != null)
            {
                tblStudent.Visible = true;
                if (dtr.Read())
                {
                    lblTempNo.Text = dtr["IDNO"].ToString();
                    string branchname = objCommon.LookUp("ACD_BRANCH", "LONGNAME", "BRANCHNO=" + dtr["branchno"].ToString());
                    lblBranch.Text = branchname;
                    lblBranch.ToolTip = dtr["branchno"].ToString();
                    lblName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                    lblMName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString();
                    lblEnrollNo.Text = dtr["ENROLLNO"] == null ? string.Empty : dtr["ENROLLNO"].ToString();
                    lblDOB.Text = dtr["DOB"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["DOB"]).ToString("dd/MM/yyyy");
                    string semester = objCommon.LookUp("ACD_SEMESTER", "SEMESTERNAME", "SEMESTERNO=" + dtr["SEMESTERNO"].ToString());
                    lblSemester.Text = semester;
                    lblSemester.ToolTip = dtr["SEMESTERNO"].ToString();
                    string category = objCommon.LookUp("ACD_CATEGORY", "CATEGORY", "CATEGORYNO=" + dtr["ADMCATEGORYNO"].ToString());
                    lblCategory.Text = category;
                    string religion = objCommon.LookUp("ACD_RELIGION", "RELIGION", "RELIGIONNO=" + dtr["RELIGIONNO"].ToString());
                    lblReligion.Text = religion;
                    string nation = objCommon.LookUp("ACD_NATIONALITY", "NATIONALITY", "NATIONALITYNO=" + dtr["NATIONALITYNO"].ToString());
                    lblNationality.Text = nation;
                    lblLAdd.Text = dtr["LADDRESS"] == null ? string.Empty : dtr["LADDRESS"].ToString();
                    string city = objCommon.LookUp("ACD_CITY", "CITY", "CITYNO=" + dtr["PCITY"].ToString());
                    lblCity.Text = city;
                    lblLLNo.Text = dtr["LTELEPHONE"] == null ? string.Empty : dtr["LTELEPHONE"].ToString();
                    lblMobNo.Text = dtr["LMOBILE"] == null ? string.Empty : dtr["LMOBILE"].ToString();
                    lblPAdd.Text = dtr["PADDRESS"] == null ? string.Empty : dtr["PADDRESS"].ToString();
                    city = objCommon.LookUp("ACD_CITY", "CITY", "CITYNO=" + dtr["PCITY"].ToString());
                    lblPCity.Text = city;
                    string degree = objCommon.LookUp("ACD_DEGREE", "DEGREENAME", "DEGREENO=" + dtr["DEGREENO"].ToString());
                    lblDegree.Text = degree;
                    string paytype = objCommon.LookUp("ACD_PAYMENTTYPE", "PAYTYPENAME", "PAYTYPENO=" + dtr["PTYPE"].ToString());
                    lblPaymenttype.Text = paytype;
                    lblPaymenttype.ToolTip = dtr["PTYPE"].ToString();

                    int degreeno = Convert.ToInt32(dtr["DEGREENO"]);
                    trPayStatus.Visible = true;
                    trPrint.Visible = false;
                    trSubmit.Visible = true;
                    trStatus.Visible = false;
                    trMsg.Visible = true;

                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "This is not a Provisional Admission Student.", this.Page);
                    tblStudent.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ProvisionalAdmissionConfirmation.btnSearch_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void rblStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblStatus.SelectedIndex == 1)
        {
            trSubmit.Visible = true;
            trPrint.Visible = false;
        }
        else
        {
            trSubmit.Visible = false;
            trPrint.Visible = true;
        }
    }
    protected void btnPrintChallan_Click(object sender, EventArgs e)
    {
        try
        {

            //Create Demand and Print the Challan..
            DemandModificationController dmController = new DemandModificationController();
            FeeDemand demandCriteria = this.GetDemandCriteria();
            int selectSemesterNo = Int32.Parse(lblSemester.ToolTip);
            string studentIDs = lblTempNo.Text.Trim();

            bool overwriteDemand = true;



            string demandno = objCommon.LookUp("ACD_DEMAND", "COUNT(*)", "IDNO=" + studentIDs.ToString() + " AND SEMESTERNO=" + selectSemesterNo);
            if (Convert.ToInt32(demandno) <= 0)
            {
                string response = dmController.CreateDemandForStudents(studentIDs, demandCriteria, selectSemesterNo, overwriteDemand);


            }

            //Create DCR and print Challan
            string receiptno = this.GetNewReceiptNo();
            FeeDemand dcr = this.GetDcrCriteria();
            string dcritem = dmController.CreateDcrForStudents(studentIDs, dcr, selectSemesterNo, overwriteDemand, receiptno);


            //Print Challan..


            string dcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(studentIDs) + " AND SEMESTERNO=" + selectSemesterNo);

            if (dcrNo != string.Empty && studentIDs != string.Empty)
            {
                this.ShowReport("FeeCollectionReceiptForCourseRegister1.rpt", Convert.ToInt32(dcrNo), Convert.ToInt32(studentIDs), "1");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Academic_ProvisionalAdmissionConfirmation.btnPrintChallan_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private FeeDemand GetDemandCriteria()
    {
        FeeDemand demandCriteria = new FeeDemand();
        try
        {
            demandCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
            demandCriteria.ReceiptTypeCode = "TF";
            demandCriteria.BranchNo = int.Parse(lblBranch.ToolTip);
            demandCriteria.SemesterNo = int.Parse(lblSemester.ToolTip);
            demandCriteria.PaymentTypeNo = int.Parse(lblPaymenttype.ToolTip);

            demandCriteria.UserNo = int.Parse(Session["userno"].ToString());
            demandCriteria.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Academic_ProvisionalAdmissionConfirmation.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return demandCriteria;
    }


    //get the new receipt No.
    private string GetNewReceiptNo()
    {
        string receiptNo = string.Empty;

        try
        {
            string demandno = objCommon.LookUp("ACD_DEMAND", "MAX(DM_NO)", "");
            DataSet ds = feeController.GetNewReceiptData("B", Int32.Parse(Session["userno"].ToString()), "TF");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString());
                receiptNo = dr["PRINTNAME"].ToString() + "/" + "B" + "/" + DateTime.Today.Year.ToString().Substring(2, 2) + "/" + dr["FIELD"].ToString() + demandno;
                // save counter no in hidden field to be used while saving the record
                ViewState["CounterNo"] = dr["COUNTERNO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Academic_ProvisionalAdmissionConfirmation.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return receiptNo;
    }



    private FeeDemand GetDcrCriteria()
    {
        FeeDemand dcrCriteria = new FeeDemand();
        try
        {
            dcrCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
            dcrCriteria.ReceiptTypeCode = "TF";
            dcrCriteria.BranchNo = int.Parse(lblBranch.ToolTip);
            dcrCriteria.SemesterNo = int.Parse(lblSemester.ToolTip);
            dcrCriteria.PaymentTypeNo = int.Parse(lblPaymenttype.ToolTip);
            dcrCriteria.UserNo = int.Parse(Session["userno"].ToString());
            dcrCriteria.ExcessAmount = 0;
            dcrCriteria.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Academic_ProvisionalAdmissionConfirmation.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dcrCriteria;
    }

    private void ShowReport(string rptName, int dcrNo, int studentNo, string copyNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=" + this.GetReportParameters(studentNo, dcrNo, copyNo);
            divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ProvisionalAdmissionConfirmation.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetReportParameters(int studentNo, int dcrNo, string copyNo)
    {
        /// This report requires nine parameters. 
        /// Main report takes three params and three subreport takes two
        /// params each. Each subreport takes a pair of DCR_NO and ID_NO as parameter.
        /// Main report takes one extra param i.e. copyNo. copyNo is used to specify whether
        /// the receipt is a original copy(value=1) OR duplicate copy(value=2)
        /// ADD THE PARAMETER COLLEGE CODE

        string param = "@P_IDNO=" + studentNo.ToString() + ",@P_DCRNO=" + dcrNo + ",CopyNo=" + copyNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;
    }

}
