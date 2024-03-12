//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : REPRINT OR CANCEL RECEIPTS
// CREATION DATE : 15-JUN-2009
// CREATED BY    : AMIT YADAV
// MODIFIED DATE : 22-JUL-2009 BY AMIT YADAV
// MODIFIED DESC : ADDED RECEIPT CANCELLATION FUNCTIONALITY
//======================================================================================

using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;


public partial class Academic_ReprintReceipts : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    string strDcrNo;
    #region Page Events

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
                    //Page Authorization
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                }
                this.objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "ISNULL(ACTIVE,0)=1", "COLLEGE_ID");
                this.objCommon.FillDropDownList(ddlclgmisc, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "ISNULL(ACTIVE,0)=1", "COLLEGE_ID");
                this.objCommon.FillDropDownList(ddlCounterNo, "ACD_COUNTER_REF", "COUNTERNO", "PRINTNAME", "", "COUNTERNO");
                //this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
                ////this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO>0", "BRANCHNO");
                //this.objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");
                this.objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                this.objCommon.FillDropDownList(ddlsemmisc, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");

                this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "", "RCPTTYPENO");
                objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO > 0", "BANKNO");
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
            }
            else
            {

                // Clear message div
                divMsg.InnerHtml = string.Empty;

                /// Check if postback is caused by reprint receipt or cancel receipt buttons
                /// if yes then call corresponding methods
                if (Request.Params["__EVENTTARGET"] != null && Request.Params["__EVENTTARGET"].ToString() != string.Empty)
                {
                    if (Request.Params["__EVENTTARGET"].ToString() == "ReprintReceipt")
                        this.ReprintReceipt();
                    else if (Request.Params["__EVENTTARGET"].ToString() == "CancelReceipt")
                        this.CancelReceipt();
                    //add for the editing
                    else if (Request.Params["__EVENTTARGET"].ToString() == "EditReceipt")
                        this.EditReceipt();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ReprintReceipts.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ReprintReceipts.aspx");
        }
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string fieldName = string.Empty;
            string searchText = txtSearchText.Text.Trim();
            string errorMsg = string.Empty;

            if (rdoReceiptNo.Checked)
            {
                fieldName = "REC_NO";
                errorMsg = "having receipt no.: " + txtSearchText.Text.Trim();
            }
            else if (rdoEnrollmentNo.Checked)
            {
                fieldName = "ENROLLNMENTNO";     //"IDNO"
                errorMsg = "for student having enrollment no.: " + txtSearchText.Text.Trim();
            }
            else if (rdbStudentName.Checked)
            {
                fieldName = "NAME";     //"IDNO"
                errorMsg = "for student having Student Name.: " + txtSearchText.Text.Trim();
            }
            
            FeeCollectionController feeController = new FeeCollectionController();
            DataSet ds = feeController.FindReceipts(fieldName, searchText);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvPaidReceipts.DataSource = ds;
                lvPaidReceipts.DataBind();
                lvPaidReceipts.Visible = true;
                btnCancel.Disabled = false;
                btnReprint.Disabled = false;
                btnEdit.Disabled = false;
                ViewState["IDNO"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
            }
            else
            {


                ShowMessage("No receipt found ");
                lvPaidReceipts.DataSource = ds;
                lvPaidReceipts.DataBind();
                lvPaidReceipts.Visible = false;
                btnCancel.Disabled = true;
                btnReprint.Disabled = true;
                btnEdit.Disabled = true;
                ViewState["IDNO"] = 0;
            }
            txtRemark.Text = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ReprintReceipt()
    {
        try
        {
            DailyCollectionRegister dcr = new DailyCollectionRegister();
            if (this.GetRecieptData(ref dcr))
            {
                this.ShowReport(dcr.StudentId,dcr.DcrNo);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.btnReprint_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CancelReceipt()
    {
        try
        {
            DailyCollectionRegister dcr = new DailyCollectionRegister();
            if (this.GetRecieptData(ref dcr))
            {
                FeeCollectionController feeController = new FeeCollectionController();
                if (feeController.CancelReceipt(dcr,Convert.ToInt32(Session["userno"])))
                {
                    this.objCommon.DisplayMessage(updcancel, "Receipt has been cancelled successfully.", this.Page);
                    //this.ShowMessage("Receipt has been cancelled successfully.");
                    lvPaidReceipts.DataSource = null;
                    lvPaidReceipts.DataBind();
                    lvPaidReceipts.Visible = false;
                    btnCancel.Disabled = true;
                    btnReprint.Disabled = true;
                    btnEdit.Disabled = true;
                    txtRemark.Text = "";
                    txtSearchText.Text = "";
                  
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private bool GetRecieptData(ref DailyCollectionRegister dcr)
    {
        try
        {
            foreach (ListViewDataItem item in lvPaidReceipts.Items)
            {
                string strDcrNo = (item.FindControl("hidDcrNo") as HiddenField).Value;
                if (strDcrNo == Request.Form["Receipts"].ToString())
                {
                    dcr.DcrNo = ((strDcrNo != null && strDcrNo != string.Empty) ? int.Parse(strDcrNo) : 0);

                    string strIdNo = (item.FindControl("hidIdNo") as HiddenField).Value;
                    dcr.StudentId = ((strIdNo != null && strIdNo != string.Empty) ? int.Parse(strIdNo) : 0);

                    dcr.UserNo = int.Parse(Session["userno"].ToString());
                    dcr.Remark = "This receipt has been canceled by " + Session["userfullname"].ToString() + " on " + DateTime.Now + ". ";
                    dcr.Remark += txtRemark.Text.Trim();

                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.GetRecordIDs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return false;
    }

    private void ShowReport(int studentNo,int dcrNo)
    {
        try
        {


            //string url = "https://erptest.sliit.lk/Reports/CommonReport.aspx?";
            //// string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
            //string url = "http://localhost:59566/PresentationLayer/Reports/CommonReport.aspx?";
            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
            url += "pagetitle=" + "Fee Receipt";
            url += "&path=~,Reports,Academic,FeesReceiptApplicationForm.rpt";
            url += "&param=@P_IDNO=" + studentNo + ",@P_DCR_NO=" + dcrNo + "";
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MISCDCRNO=" + ViewState["OUTPUT"] + ",@P_COPY=1,@P_USERNAME=" + Session["userfullname"].ToString() + "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetReportParameters(int studentNo,  int dcrNo)
    {
        string param = "@P_IDNO=" + studentNo.ToString() + ",@P_DCRNO=" + dcrNo.ToString() + "";
        return param;
    }

    private string GetViewStateItem(string itemName)
    {
        if (ViewState.Count > 0 &&
            ViewState[itemName] != null &&
            ViewState[itemName].ToString() != null &&
            ViewState[itemName].ToString() != string.Empty)
            return ViewState[itemName].ToString();
        else
            return string.Empty;
    }

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

    private void EditReceipt()
    {
        panelEditing.Visible = true;
        try
        {
            DailyCollectionRegister dcr = new DailyCollectionRegister();
            if (this.GetRecieptDataEdit(ref dcr))
            {

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }




    private bool GetRecieptDataEdit(ref DailyCollectionRegister dcr)
    {
        try
        {

            foreach (ListViewDataItem item in lvPaidReceipts.Items)
            {
                //This is accept the only radiobutton checked value
                strDcrNo = Request.Form["Receipts"].ToString();
                if (strDcrNo != null)
                {
                    string DcrNo = strDcrNo;
                    DataSet dsRecord = objCommon.FillDropDown("ACD_DCR_TRAN", "DCRNO,DD_NO", "IDNO,DD_DT,DD_BANK,DD_CITY,DD_AMOUNT,BANKNO", "DCRNO = " + DcrNo, string.Empty);
                    if (dsRecord.Tables[0].Rows.Count > 0)
                    {
                        txtDDNo.Text = dsRecord.Tables[0].Rows[0]["DD_NO"].ToString();
                        txtDDAmount.Text = dsRecord.Tables[0].Rows[0]["DD_AMOUNT"].ToString();
                        txtDDDate.Text = dsRecord.Tables[0].Rows[0]["DD_DT"].ToString();
                        txtDDCity.Text = dsRecord.Tables[0].Rows[0]["DD_CITY"].ToString();
                        ddlBank.SelectedValue = dsRecord.Tables[0].Rows[0]["BANKNO"].ToString();
                        hfDcrNum.Value = dsRecord.Tables[0].Rows[0]["DCRNO"].ToString();
                        hfIdno.Value = dsRecord.Tables[0].Rows[0]["IDNO"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayMessage("This is not DD receipt record", this.Page);
                        panelEditing.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.GetRecordIDs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return false;
    }

    protected void btnDD_Info_Click(object sender, EventArgs e)
    {
        //Update the DD information
        FeeCollectionController feeController = new FeeCollectionController();

        int DCRNO = Convert.ToInt32(hfDcrNum.Value.Trim());
        string DDNO = txtDDNo.Text.Trim();
        string DDBANK = ddlBank.SelectedItem.Text.Trim();
        int BANKNO = Convert.ToInt32(ddlBank.SelectedValue.Trim());
        string DDCITY = txtDDCity.Text;
        DateTime DDDATE = Convert.ToDateTime(txtDDDate.Text.Trim());
        int IDNO = Convert.ToInt32(hfIdno.Value.Trim());
        string output = feeController.UpdateDDInfo(DCRNO, DDNO, DDDATE, DDBANK, DDCITY, BANKNO, IDNO);

        if (output != "-99")
        {
            objCommon.DisplayMessage("DD information Updated Successfully", this.Page);
        }
        DDclear();
    }
    protected void BtnCancelDD_Click(object sender, EventArgs e)
    {
        panelEditing.Visible = false;
        DDclear();
    }

    private void DDclear()
    {
        txtDDNo.Text = string.Empty;
        txtDDAmount.Text = string.Empty;
        txtDDDate.Text = string.Empty;
        ddlBank.SelectedIndex = 0;
        txtDDCity.Text = string.Empty;
    }

    protected void rdoReceiptNo_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            lvPaidReceipts.DataSource = null;
            lvPaidReceipts.DataBind();
            lvPaidReceipts.Visible = false;
            btnCancel.Disabled = true;
            btnReprint.Disabled = true;
            btnEdit.Disabled = true;
            txtRemark.Text = string.Empty;
            txtSearchText.Text = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.rdoReceiptNo_CheckedChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void rdoEnrollmentNo_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            lvPaidReceipts.DataSource = null;
            lvPaidReceipts.DataBind();
            lvPaidReceipts.Visible = false;
            btnCancel.Disabled = true;
            btnReprint.Disabled = true;
            btnEdit.Disabled = true;
            txtRemark.Text = string.Empty;
            txtSearchText.Text = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.rdoEnrollmentNo_CheckedChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    bool isChalan = false;
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            //if (rdoCancelReciept.Checked)
            //{
            //    isChalan = false;
            //    ShowReport("Cancel_Reciept_Report", "CancelReciept.rpt");
            //}
            //else
            //{
            //    isChalan = true;
            //    ShowReport("Cancel_Reciept_Report", "CancelReciept.rpt");
            //}
            DateTime fromdate = Convert.ToDateTime(txtFromDate.Text);
            DateTime todate = Convert.ToDateTime(txtToDate.Text);
            if (fromdate > todate)
            {
                divlkrecept.Attributes.Remove("class");
                objCommon.DisplayMessage(updcancel, "To date is greater than from date.", this.Page);
                txtFromDate.Text = "";
                txtToDate.Text = "";
                return;
            }
            string rec_code = "";
            if (ddlReceiptType.SelectedIndex > 0)
            {
                rec_code = ddlReceiptType.SelectedValue;
            }
            string[] program;
            if (ddlDegree.SelectedValue == "0")
            {
                program = "0,0".Split(',');
            }
            else
            {
                program = ddlDegree.SelectedValue.Split(',');
            }
            FeeCollectionController feeController = new FeeCollectionController();
            DataSet ds = feeController.GetCancellationReport(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), rec_code, Convert.ToInt32(ddlCounterNo.SelectedValue), Convert.ToInt32(program[0]), Convert.ToInt32(program[1]), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));//, out retout           
            GridView gvStudData = new GridView();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvStudData.DataSource = ds;
                gvStudData.DataBind();
                string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
                string attachment = "attachment; filename=" + "_" + "CancelationReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Response.Write(FinalHead);
                gvStudData.RenderControl(htw);
                //string a = sw.ToString().Replace("_", " ");
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this, "Record Not Found !!!",this.Page);
                return;                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_CancelReciept.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            divlkrecept.Attributes.Remove("class");
            string[] program;
            if (ddlDegree.SelectedValue == "0")
            {
                program = "0,0".Split(',');
            }
            else
            {
                program = ddlDegree.SelectedValue.Split(',');
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            //string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            string rec_code = "";
            if (ddlReceiptType.SelectedIndex > 0)
            {
                rec_code = ddlReceiptType.SelectedValue;
            }
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_REC_FROM_DT=" + txtFromDate.Text.Trim() + ",@P_REC_TO_DATE=" + txtToDate.Text.Trim() + ",@P_RECIEPTCODE=" + rec_code + ",@P_COUNTERNO=" + Convert.ToInt32(ddlCounterNo.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(program[0]) + ",@P_BRANCHNO=" + Convert.ToInt32(program[1]) + ",@P_YEARNO=" + Convert.ToInt32(ddlYear.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",username=" + Session["username"].ToString() + ",@P_IS_CHALAN=" + isChalan;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_CancelReciept.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

   
  
    //protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlDegree.SelectedIndex > 0)
    //    {
    //        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.BRANCHNO");
    //    }
    //    else
    //    {
    //        ddlBranch.SelectedIndex = 0;
    //    }
    //}
    protected void lkreceipt_Click(object sender, EventArgs e)
    {
        divlkcancel.Attributes.Remove("class");
        divlkrecept.Attributes.Add("class", "active");
        divlkmiscreprint.Attributes.Remove("class");
        divlkmisccancel.Attributes.Remove("class");

        divreceipt.Visible = true;
        divcancel.Visible = false;
        divmiscreprint.Visible=false;
        divmisccancel.Visible = false;

        txtFromDate.Text = "";
        txtToDate.Text = "";
        ddlcollege.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
    }
    protected void lnkcancel_Click(object sender, EventArgs e)
    {
        divlkrecept.Attributes.Remove("class");
        divlkcancel.Attributes.Add("class", "active");
        divlkmiscreprint.Attributes.Remove("class");
        divlkmisccancel.Attributes.Remove("class");

        divcancel.Visible = true;
        divreceipt.Visible = false;
        divmiscreprint.Visible = false;
        divmisccancel.Visible = false;
    }
    protected void ddlcollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        divlkrecept.Attributes.Remove("class");
        objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=S.BRANCHNO)INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON (AU.AFFILIATED_NO=S.AFFILIATED_NO)", "DISTINCT CONCAT(S.DEGREENO,',',S.BRANCHNO,',',S.AFFILIATED_NO)AS ID", "(D.DEGREENAME +'-'+ B.LONGNAME+'-'+AU.AFFILIATED_SHORTNAME)AS DEGBRANCH", "S.COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue) + "", "ID");

    }
    protected void btnCancelre_Click(object sender, EventArgs e)
    {
        ddlcollege.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        txtFromDate.Text="";
        txtToDate.Text = "";
    }
   
    protected void lnkmiscreprint_Click(object sender, EventArgs e)
    {
        divlkcancel.Attributes.Remove("class");
        divlkrecept.Attributes.Remove("class");
        divlkmiscreprint.Attributes.Add("class", "active");
        divlkmisccancel.Attributes.Remove("class");

        divreceipt.Visible = false;
        divcancel.Visible = false;
        divmiscreprint.Visible = true;
        divmisccancel.Visible = false;

        txtFromDate.Text = "";
        txtToDate.Text = "";
        ddlcollege.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
    }
    protected void lnkmisccancel_Click(object sender, EventArgs e)
    {
        divlkcancel.Attributes.Remove("class");
        divlkrecept.Attributes.Remove("class");
        divlkmiscreprint.Attributes.Remove("class");
        divlkmisccancel.Attributes.Add("class", "active");

        divreceipt.Visible = false;
        divcancel.Visible = false;
        divmiscreprint.Visible = false;
        divmisccancel.Visible = true;

        txtFromDate.Text = "";
        txtToDate.Text = "";
        ddlcollege.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
    }
    protected void radiomiscreprint1_CheckedChanged(object sender, EventArgs e)
    {
        LvMiscReceipt.DataSource = null;
        LvMiscReceipt.DataBind();
        TxtSearchMisc.Text = "";
    }
    protected void radiomiscreprint2_CheckedChanged(object sender, EventArgs e)
    {
        LvMiscReceipt.DataSource = null;
        LvMiscReceipt.DataBind();
        TxtSearchMisc.Text = "";
    }
    protected void btnsearchmisc_Click(object sender, EventArgs e)
    {
        try
        {
            divlkrecept.Attributes.Remove("class");
            string fieldName = string.Empty;
            string searchText = "";
            string errorMsg = string.Empty;

            string IDNO=Convert.ToString(objCommon.LookUp("ACD_STUDENT","IDNO","REGNO='" + TxtSearchMisc.Text +"' OR ENROLLNO='"+ TxtSearchMisc.Text+"'"));

            if (radiomiscreprint1.Checked)
            {
                fieldName = "COUNTR";
                errorMsg = "having receipt no.: " + TxtSearchMisc.Text.Trim();
                searchText = TxtSearchMisc.Text.Trim();
            }
            else if (radiomiscreprint2.Checked)
            {
                fieldName = "STUDSRNO";
                errorMsg = "for student having enrollment no.: " + IDNO;
                searchText = IDNO;
            }
            else if (rbdMisStudentName.Checked)
            {
                fieldName = "NAME";     //"IDNO"
                errorMsg = "for student having Student Name.: " + TxtSearchMisc.Text.Trim();
            }

            FeeCollectionController feeController = new FeeCollectionController();
            DataSet ds = feeController.FindMiscReceipts(fieldName, searchText);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                LvMiscReceipt.DataSource = ds;
                LvMiscReceipt.DataBind();
                LvMiscReceipt.Visible = true;
                btnmisccancel.Enabled = true;
                btnmiscreprint.Enabled = true; 
            }
            else
            {


                ShowMessage("No receipt found ");
                TxtSearchMisc.Text = "";
                LvMiscReceipt.DataSource = ds;
                LvMiscReceipt.DataBind();
                LvMiscReceipt.Visible = false;
                btnmisccancel.Enabled = true;
                btnmiscreprint.Enabled = true;
             
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnmiscreprint_Click(object sender, EventArgs e)
    {

        ShowReportMisc("Miscallenious Fees", "FeesReceiptApplicationFormMisc.rpt");
    }
    private void ShowReportMisc(string reportTitle, string rptFileName)
    {

        try
        {
            divlkrecept.Attributes.Remove("class");
            FeeCollectionController feeController = new FeeCollectionController();
            string strDcrNo = ""; int dcrno = 0; string strIdNo = "";
            int idno = 0; int userno = 0; string remark = "";
            foreach (ListViewDataItem item in LvMiscReceipt.Items)
            {
                strDcrNo = (item.FindControl("hidDcrNo") as HiddenField).Value;
                if (strDcrNo == Request.Form["Receipts"].ToString())
                {
                    dcrno = ((strDcrNo != null && strDcrNo != string.Empty) ? int.Parse(strDcrNo) : 0);

                    strIdNo = (item.FindControl("hidIdNo") as HiddenField).Value;
                    idno = ((strIdNo != null && strIdNo != string.Empty) ? int.Parse(strIdNo) : 0);

                    userno = int.Parse(Session["userno"].ToString());
                    remark = "This receipt has been canceled by " + Session["userfullname"].ToString() + " on " + DateTime.Now + ". ";
                    remark += TxtRemarkmisc.Text.Trim();
                    //return;

                }
                //else
                //{
                //    objCommon.DisplayMessage(updMiscreprint, "Please Select At least One receipt.", this.Page);

                //}
            }
            //string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
            //string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
            ////string url = "https://erptest.sliit.lk/Reports/CommonReport.aspx?";
            ////string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            ////url += "Reports/CommonReport.aspx?";
            //string url = "http://localhost:59566/PresentationLayer/Reports/CommonReport.aspx?";

            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];   
            url += "pagetitle=" + reportTitle;
            url += "&path=~,REPORTS,ACADEMIC," + rptFileName;
            url += "&param=@P_IDNO=" + idno + ",@P_DCR_NO=" + dcrno + "";
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MISCDCRNO=" + dcrno + ",@P_COPY=1,@P_USERNAME=" + Session["userfullname"].ToString() + "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updMiscreprint, this.updMiscreprint.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnmisccancel_Click(object sender, EventArgs e)
    {
        try
        {
            divlkrecept.Attributes.Remove("class");
            FeeCollectionController feeController = new FeeCollectionController();
            string strDcrNo = ""; int dcrno = 0; string strIdNo = "";
            int idno = 0; int userno = 0; string remark = "";

            foreach (ListViewDataItem item in LvMiscReceipt.Items)
            {
                strDcrNo = (item.FindControl("hidDcrNo") as HiddenField).Value;
                if (strDcrNo == Request.Form["Receipts"].ToString())
                {
                    dcrno = ((strDcrNo != null && strDcrNo != string.Empty) ? int.Parse(strDcrNo) : 0);

                    strIdNo = (item.FindControl("hidIdNo") as HiddenField).Value;
                    idno = ((strIdNo != null && strIdNo != string.Empty) ? int.Parse(strIdNo) : 0);

                    userno = int.Parse(Session["userno"].ToString());
                    remark = "This receipt has been canceled by " + Session["userfullname"].ToString() + " on " + DateTime.Now + ". ";
                    remark += TxtRemarkmisc.Text.Trim();
                    if (TxtRemarkmisc.Text == "")
                    {
                        divlkrecept.Attributes.Remove("class");
                        objCommon.DisplayMessage(updMiscreprint, "Please Enter Reason ", this.Page);
                        return;
                    }
                }
            }
            if (feeController.CancelMiscReceipt(dcrno, idno, Convert.ToInt32(Session["userno"]), remark))
            {
                objCommon.DisplayMessage(updMiscreprint, "Receipt has been cancelled successfully.", this.Page);
                divlkrecept.Attributes.Remove("class");
                //this.ShowMessage("Receipt has been cancelled successfully.");
                LvMiscReceipt.DataSource = null;
                LvMiscReceipt.DataBind();
                LvMiscReceipt.Visible = false;
                btnmisccancel.Enabled = false;
                btnmiscreprint.Enabled = false;

                TxtRemarkmisc.Text = "";
                TxtSearchMisc.Text = "";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ValidateDate(object sender, ServerValidateEventArgs e)
    {
        if (Regex.IsMatch(Txtfrommisc.Text, "(((0|1)[0-9]|2[0-9]|3[0-1])\\/(0[1-9]|1[0-2])\\/((19|20)\\d\\d))$"))
        {
            DateTime dt;
            e.IsValid = DateTime.TryParseExact(e.Value, "dd/MM/yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out dt);
            if (e.IsValid)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Valid Date.');", true);
            }
        }
        else
        {
            e.IsValid = false;
        }
           
    }
    protected void btnreportmisc_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt,df;
            bool validfr = DateTime.TryParseExact(Txtfrommisc.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out dt);
            bool validto = DateTime.TryParseExact(Txttomisc.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out df);
            if (validto == false || validfr==false)
            {
                objCommon.DisplayMessage(updMiscreprint, "Please Select Or Enter Date In correct format.", this.Page);
                Txttomisc.Text = "";
                Txtfrommisc.Text = "";
            }
            DateTime fromdate = Convert.ToDateTime(Txtfrommisc.Text);
            DateTime todate = Convert.ToDateTime(Txttomisc.Text);
            if (fromdate > todate)
            {
                objCommon.DisplayMessage(updMiscreprint, "To date is greater than from date.", this.Page);
                Txtfrommisc.Text="";
                Txttomisc.Text = "";
                return;
            }
             
            string[] program;
            if (ddlpgmmisc.SelectedValue == "0")
            {
                program = "0,0".Split(',');
            }
            else
            {
                program = ddlpgmmisc.SelectedValue.Split(',');
            }
            FeeCollectionController feeController = new FeeCollectionController();
            DataSet ds = feeController.GetMiscCancellationReport(Txtfrommisc.Text.Trim(), Txttomisc.Text.Trim(), "",0, Convert.ToInt32(program[0]), Convert.ToInt32(program[1]), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlsemmisc.SelectedValue));//, out retout           
            if (ds.Tables[0].Rows.Count > 0)
            {

                GridView gvStudData = new GridView();
                gvStudData.DataSource = ds;
                gvStudData.DataBind();
                string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
                string attachment = "attachment; filename=" + "_" + "MiscCancelationReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Response.Write(FinalHead);
                gvStudData.RenderControl(htw);
                //string a = sw.ToString().Replace("_", " ");
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this, "Record Not Found !!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_CancelReciept.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btncan_Click(object sender, EventArgs e)
    {
        ddlclgmisc.SelectedIndex = 0;
        ddlpgmmisc.SelectedIndex = 0;
        Txtfrommisc.Text = "";
        Txttomisc.Text = "";
        ddlsemmisc.SelectedIndex = 0;
    }
    protected void ddlclgmisc_SelectedIndexChanged(object sender, EventArgs e)
    {
        divlkrecept.Attributes.Remove("class");
        objCommon.FillDropDownList(ddlpgmmisc, "ACD_COLLEGE_DEGREE_BRANCH S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=S.BRANCHNO)INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON (AU.AFFILIATED_NO=S.AFFILIATED_NO)", "DISTINCT CONCAT(S.DEGREENO,',',S.BRANCHNO,',',S.AFFILIATED_NO)AS ID", "(D.DEGREENAME +'-'+ B.LONGNAME+'-'+AU.AFFILIATED_SHORTNAME)AS DEGBRANCH", "S.COLLEGE_ID=" + Convert.ToInt32(ddlclgmisc.SelectedValue) + "", "ID");

    }


}