//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : EXAM REGISTRATION SLIP                                     
// CREATION DATE : 25-OCT-2012
// ADDED BY      : ASHISH DHAKATE                                                
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

public partial class ACADEMIC_ExamRegistrationSlip : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
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

                //CHECK ACTIVITY FOR EXAM REGISTRATION
                //
                int idno = Convert.ToInt32(Session["idno"]);
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                DisplayInformation(idno);
            }

        }

        divMsg.InnerHtml = string.Empty;
    }

    private void CheckActivity()
    {
        string sessionno = string.Empty;
        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.ACTIVITY_CODE = 'EXAMREG'");

        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                pnlStart.Visible = false;

            }

            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                pnlStart.Visible = false;
            }

        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            pnlStart.Visible = false;
        }
        dtr.Close();
    }


    #region

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ExamRegistrationSlip.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ExamRegistrationSlip.aspx");
        }
    }


    private void DisplayInformation(int studentId)
    {
        try
        {
            /// Display Previous Receipts Information
            /// Display student paid only Exam receipt information.
            /// These are the receipts(i.e. Exam Fee) paid by the student during 
            DataSet ds = feeController.GetPaidReceiptsExamInfoByStudId(studentId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // Bind list view with exam paid receipt data 
                lvPaidReceipts.DataSource = ds;
                lvPaidReceipts.DataBind();
            }
            else
            {
                divHidPreviousReceipts.InnerHtml = "No Supplimentary Exam Receipt Found.<br/>";
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ExamregistrationSlip.DisplayInformation() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnPrintReceipt_Click(object sender, EventArgs e)
    {
        try
        {
            string idno = Session["idno"].ToString();
            ImageButton btnPrint = sender as ImageButton;
            if (btnPrint.CommandArgument != string.Empty && idno != string.Empty)
            {
                if (btnPrint.ToolTip == "SEF") //-- this condtion for Exam registration
                {
                    this.ShowReport("FeeCollectionReceiptForExam.rpt", Int32.Parse(btnPrint.CommandArgument), Int32.Parse(idno), "2");
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.btnPrintReceipt_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReport(string rptName, int dcrNo, int studentNo, string copyNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=" + this.GetReportParameters(dcrNo, studentNo, copyNo);
            divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ExamRegistrationSlip.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetReportParameters(int dcrNo, int studentNo, string copyNo)
    {
        /// Main report takes one extra param i.e. copyNo. copyNo is used to specify whether
        /// the receipt is a original copy(value=1) OR duplicate copy(value=2)
        /// ADD THE PARAMETER COLLEGE CODE
        string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;

    }


    #endregion

}

