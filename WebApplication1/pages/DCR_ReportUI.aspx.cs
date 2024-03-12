//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : DAILY FEE COLLECTION REPORT
// CREATION DATE : 21-08-2021
// CREATED BY    : Aditya Talewar
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
public partial class Academic_DCR_ReportUI : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

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
                    // Check User Authority 
                    //this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    idDataFilter.Visible = true;

                    string College_code = objCommon.LookUp("REFF", "College_code", "OrganizationId = '" + Session["OrgId"].ToString() + "'");

                    // Added by Aditya Talewar 06-09-2023//

                    //ViewState["college_id"] = College_code;

                    ViewState["college_id"] = 52;
                    //End Aditya Talewar 06-09-2023 //
                    // Load drop down lists
                    // this.objCommon.FillDropDownList(ddlCounterNo, "ACD_COUNTER_REF", "COUNTERNO", "PRINTNAME", "", "COUNTERNO");
                    this.objCommon.FillDropDownList(ddlCounterNo, "acd_counter_ref a inner join user_acc u on (a.ua_no=u.ua_no)", "a.COUNTERNO", "a.PRINTNAME+'-'+(u.ua_name collate database_default)+'-'+a.RECEIPT_PERMISSION", "", "COUNTERNO");
                    this.objCommon.FillDropDownList(ddlPaymentMode, "ACD_PAYMENT_MODE_MAS", "PAY_MODE_CODE", "LINK_CAPTION", "LINK_CAPTION IS NOT NULL  AND LINK_CAPTION <> ''", "PAY_MODE_NO");
                    this.objCommon.FillListBox(lbOSCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_ID");
                    this.objCommon.FillDropDownList(ddlreporttype, "ACD_REPORT_TYPE", "RPT_NO", "REPORT_NAME", "ISNULL(RPT_NO,0)>0 AND ISNULL(PAGE_NO,0)=3", "RPT_NO");
                    //this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "CODE", "CODE <> ''", "CODE");
                    //this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENAME");
                    //this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "SHORTNAME", "SHORTNAME <> ''", "SHORTNAME");
                    objCommon.FillDropDownList(ddlCol, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_ID");
                    if (Convert.ToInt32(Session["OrgId"]).ToString() == "2") // crescent
                    {
                        divYrLst.Visible = true;
                        divYr.Visible = false;
                        divcol.Visible = true;
                        divpay.Visible = true;
                        btnExcelFormat3.Visible = true;
                        objCommon.FillDropDownList(ddlCol, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_ID");
                        this.objCommon.FillDropDownList(ddlPaytype, "ACD_PAYTYPE", "PTYPE_CODE", "PTYPENAME", "ACTIVESTATUS=1", "PAYNO");
                        this.objCommon.FillListBox(ddlYearList, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "");
                    }
                    else
                    {
                        divYrLst.Visible = false;
                        divYr.Visible = true;
                        //        divcol.Visible = false;
                        divpay.Visible = false;
                        btnExcelFormat2.Visible = true;
                        this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENAME");
                        this.objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "");
                    }

                    if (Session["OrgId"].ToString() == "3" || Session["OrgId"].ToString() == "4" || Session["OrgId"].ToString() == "5")
                    {
                        btnExcelFormat4.Visible = true;
                    }

                    if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")// For RCPIT and RCPIPER
                    {
                        ddlreporttype.SelectedValue = "6";
                        divOnline.Visible = true;
                    }

                    if (Session["OrgId"].ToString() == "6")// For RCPIPER only
                    {
                        this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO NOT IN (3,4,5,6,7,9,10,11,12,13,14,15,16,17,18)", "RCPTTYPENO");
                    }
                    else if (Session["OrgId"].ToString() == "1") // For RCPIT only
                    {
                        this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO NOT IN (2,5,6,9,10,11)", "RCPTTYPENO");
                    }
                    else
                    {
                        this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO NOT IN (9,10)", "RCPTTYPENO");
                    }

                    this.objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "SEMESTERNO");
                    this.objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "", "PAYTYPENO");

                    DataSet ds = objCommon.FillDropDown("ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_TITLE", "RCPTTYPENO>0", "RCPTTYPENO");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        chkrectpe.DataSource = ds;
                        chkrectpe.DataTextField = ds.Tables[0].Columns["RECIEPT_TITLE"].ToString();
                        chkrectpe.DataValueField = ds.Tables[0].Columns["RCPTTYPENO"].ToString();
                        chkrectpe.DataBind();
                    }

                    btnExcelFormat2.Visible = true; ///  Added by Aditya Talewar Date 06-09-2023
                }
            }
            //txtFromDate.Focus();
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
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

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            DailyFeeCollectionRpt dcrReport = GetReportCriteria();
            string chkrectype = string.Empty;
            string chkreccheck = string.Empty;
            int chkreg = 0;
            int chkmisc = 0;
            foreach (ListItem item in chkrectpe.Items)
            {
                if (item.Selected == true)
                {
                    chkrectype = chkrectype + item.Value.ToString() + "$";

                }
            }
            if (chkrectype != "")
            {
                if (chkrectype.Substring(chkrectype.Length - 1) == "$")
                    chkrectype = chkrectype.Substring(0, chkrectype.Length - 1);
            }
            foreach (ListItem item in chkrectpe.Items)
            {
                if (item.Selected == true)
                {
                    chkreccheck = chkreccheck + item.Value.ToString() + ",";

                }
            }
            if (chkreccheck != "")
            {
                if (chkreccheck.Substring(chkreccheck.Length - 1) == ",")
                    chkreccheck = chkreccheck.Substring(0, chkreccheck.Length - 1);
            }

            if (chkreccheck != "")
            {
                chkmisc = Convert.ToInt32(objCommon.LookUp("acd_reciept_type", "count(1)", "BELONGS_TO='M' AND RCPTTYPENO IN (" + chkreccheck + ")"));
                chkreg = Convert.ToInt32(objCommon.LookUp("acd_reciept_type", "count(1)", "BELONGS_TO='A' AND RCPTTYPENO IN (" + chkreccheck + ")"));
                if (chkmisc <= 0)
                {
                    ShowMessage("Please select atleast one receipt type from other fees.");
                    return;
                }
                if (chkreg <= 0)
                {
                    ShowMessage("Please select atleast one receipt type from Tuition fees.");
                    return;
                }
            }
            if (ddlreporttype.SelectedValue == "8" || ddlreporttype.SelectedValue == "9" || ddlreporttype.SelectedValue == "10" || ddlreporttype.SelectedValue == "13")
            {
                DateTime repdatefrom = Convert.ToDateTime(txtFromDate.Text.Trim());
                DateTime repdateto = Convert.ToDateTime(txtToDate.Text.Trim());
                if (repdatefrom > repdateto)
                {
                    ShowMessage("selected date is greater than current date..");
                    return;
                }
            }
            if (ddlreporttype.SelectedValue == "11")
            {
                DateTime repdatefrom = Convert.ToDateTime(txtFromDate.Text.Trim());
                DateTime repdateto = Convert.ToDateTime(txtToDate.Text.Trim());
                if (repdatefrom > repdateto)
                {
                    ShowMessage("selected date is greater than current date..");
                    return;
                }
            }

            if (ddlreporttype.SelectedValue == "14")
            {

                DateTime repdatefrom = Convert.ToDateTime(txtFromDate.Text.Trim());
                DateTime repdateto = Convert.ToDateTime(txtToDate.Text.Trim());
                if (repdatefrom > repdateto)
                {
                    ShowMessage("selected date is greater than current date..");
                    return;
                }
                if (ddlCol.SelectedValue == "0")
                {
                    ShowMessage("Please Select College.");
                    return;
                }
            }
            else
            {
                if (ddlReceiptType.SelectedValue == "0")
                {
                    ShowMessage("Please Select Receipt Type.");
                    return;
                }

            }


            ViewState["chkrectype"] = chkrectype;
            string reportTitle = string.Empty;
            string rptFileName = string.Empty;

            this.SetReportFileAndTitle(ref reportTitle, ref rptFileName);


            if ((rdofeecolother.Checked || ddlreporttype.SelectedValue == "9" || ddlreporttype.SelectedValue == "8" || ddlreporttype.SelectedValue == "10" || ddlreporttype.SelectedValue == "11" || rdoTransferReport.Checked || rdomiscregfee.Checked || rdopnbwithcheque.Checked || rdbotherbankwithcheque.Checked) && (ddlReceiptType.SelectedIndex != 0) || ddlreporttype.SelectedValue == "13")
            {
                this.ShowReport(dcrReport, reportTitle, rptFileName);
            }
            else if (ddlreporttype.SelectedValue == "14")
            {
                this.ShowReport(dcrReport, reportTitle, rptFileName);
            }
            else if ((rdomiscregfee.Checked) && (chkrectype != ""))
            {
                this.ShowReport(dcrReport, reportTitle, rptFileName);
            }
            else if (rdodcb.Checked)
            {
                this.ShowReport(dcrReport, reportTitle, rptFileName);
            }
            else
            {
                //ShowMessage("Receipt type is required for this report. \\nPlease select a receipt type.");
                ddlReceiptType.Focus();
                return;
            }

            //if (ddlreporttype.SelectedValue=="5" == false)
            //{
            //    DataSet ds = this.GetReportingData(dcrReport);
            //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //        this.ShowReport(dcrReport, reportTitle, rptFileName);
            //    else
            //        this.ShowMessage("No information found based on given criteria.");
            //}
            //else
            //{
            //    this.ShowReport(dcrReport, reportTitle, rptFileName);
            //}
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void SetReportFileAndTitle(ref string reportTitle, ref string rptFileName)
    {
        //tb2.Visible = true;
        //rfv2.Enabled = true; // Enables the second requiredfieldvalidator
        if (ddlreporttype.SelectedValue == "8")
        {
            if (Convert.ToInt32(Session["OrgId"]) == 2)
            {
                reportTitle = "Fee_Collection_Report";  // Changes Aditya Talewar Date 17-08-2023//
                rptFileName = "FeeCollectionReport";    // Changes Aditya Talewar Date 17-08-2023//
            }
            else
            {
                if (rdoDetailedReport.Checked)
                {
                    reportTitle = "Fee_Collection_Report";
                    rptFileName = "FeeCollectionReport_Rcpit";

                }
                else
                {
                    reportTitle = "Fee_Collection_Report";
                    rptFileName = "FeeCollectionReport";
                }
            }
        }
        else if (ddlreporttype.SelectedValue == "9")
        {
            reportTitle = "Demand_Draft_Report";
            rptFileName = "DDReport";
        }
        else if (rdoTransferReport.Checked)
        {
            reportTitle = "Demand_Draft_Report";
            rptFileName = "DDReport_punjab";
        }
        else if (rdofeecolother.Checked)
        {
            reportTitle = "Demand_Draft_Report";
            rptFileName = "DDReport_Other";
        }
        else if (ddlreporttype.SelectedValue == "8")
        {
            reportTitle = "Fee_Item_Report";
            rptFileName = "FeeItemReport";
        }
        else if (ddlreporttype.SelectedValue == "11")
        {
            reportTitle = "Outstanding";
            rptFileName = "OutstandingFeesReport";
        }
        else if (rdodcb.Checked)
        {
            reportTitle = "Demand_Draft_Report";
            rptFileName = "DCB_Other";
        }
        else if (rdomiscregfee.Checked)
        {
            reportTitle = "Demand_Draft_Report";
            rptFileName = "FeeCollectionReportMisc";
        }
        else if (rdopnbwithcheque.Checked)
        {
            reportTitle = "PNB_Cheque_Report";
            rptFileName = "DDReport_punjab_Cheque";
        }
        else if (rdbotherbankwithcheque.Checked)
        {
            reportTitle = "Other_Cheque_Report";
            rptFileName = "DDReport_Other_Cheque";
        }
        else if (ddlreporttype.SelectedValue == "13")
        {
            reportTitle = "Short_DCR_Report";
            rptFileName = "ShortDCR_Report";
        }
        else if (ddlreporttype.SelectedValue == "14")
        {
            reportTitle = "Collection_Summary_Report";
            rptFileName = "rptFeeCollectionSummary.rpt";
        }
        if (ddlreporttype.SelectedValue != "14")
        {
            if (rdoDetailedReport.Checked)
            {
                rptFileName += "_Detailed.rpt";
            }
            else
            {
                rptFileName += "_Summary.rpt";
            }
        }
    }

    private DailyFeeCollectionRpt GetReportCriteria()
    {
        DailyFeeCollectionRpt dcrReport = new DailyFeeCollectionRpt();
        try
        {
            string fromDt = string.Empty;
            if (ddlreporttype.SelectedValue == "11")
            {
                dcrReport.FDate = Convert.ToString(txtFromDate.Text);
            }
            else
            {
                dcrReport.FromDate = (txtFromDate.Text.Trim() != string.Empty) ? Convert.ToDateTime(txtFromDate.Text) : DateTime.MinValue;
            }
            dcrReport.ToDate = (txtToDate.Text.Trim() != string.Empty) ? Convert.ToDateTime(txtToDate.Text) : DateTime.MinValue;
            dcrReport.CounterNo = (ddlCounterNo.SelectedIndex > 0) ? Int32.Parse(ddlCounterNo.SelectedValue) : 0;

            if (chkonline.Checked == true)
            {
                dcrReport.PaymentMode = "O";
            }
            else
            {
                dcrReport.PaymentMode = (ddlPaymentMode.SelectedIndex > 0) ? ddlPaymentMode.SelectedValue : string.Empty;
            }


            dcrReport.DegreeNo = (ddlDegree.SelectedIndex > 0) ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
            dcrReport.BranchNo = (ddlBranch.SelectedIndex > 0) ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;

            if (Convert.ToInt32(Session["OrgId"]).ToString() == "2") // crescent
            {
                string years = string.Empty;
                foreach (ListItem items in ddlYearList.Items)
                {
                    if (items.Selected == true)
                    {
                        years += items.Value + ',';
                    }
                }
                if (years.Length > 1)
                {
                    years = years.Remove(years.Length - 1);
                }
                dcrReport.YearNos = years;
            }
            else
            {
                dcrReport.YearNo = (ddlYear.SelectedIndex > 0) ? Convert.ToInt32(ddlYear.SelectedValue) : 0;
            }



            dcrReport.SemesterNo = (ddlSemester.SelectedIndex > 0) ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;
            dcrReport.PayTypeNo = (ddlPaymentType.SelectedIndex > 0) ? Convert.ToInt32(ddlPaymentType.SelectedValue) : 0;
            dcrReport.ReceiptTypes = (ddlReceiptType.SelectedIndex > 0) ? ddlReceiptType.SelectedValue : string.Empty;

            if (ddlreporttype.SelectedValue == "12" == true)
            {
                if (ChkShowAllStudent.Checked == true)
                {
                    dcrReport.ShowBalance = "0";
                }
                else
                {
                    dcrReport.ShowBalance = "1";
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return dcrReport;
    }

    private void ShowReport(DailyFeeCollectionRpt dcrRpt, string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=" + this.GetReportParameters(dcrRpt);
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'> try{";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description); } </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.pnlFeeTable, this.pnlFeeTable.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void Export(string type)
    {

        string filename = string.Empty;
        string ContentType = string.Empty;

        if (type == "Excel")
        {
            filename = "SelectedFieldReport.xls";       //USED TO GENERATE IN EXCEL FORMAT
            ContentType = "ms-excel";
        }
        else if (type == "Word")
        {
            filename = "SelectedFieldReport.doc";       //USED TO GENERATE IN WORD FORMAT
            ContentType = "vnd.word";
        }

        string attachment = "attachment; filename=" + filename;
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + ContentType;
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Response.Write(sw.ToString());
        Response.End();
    }

    private string GetReportParameters(DailyFeeCollectionRpt dcrRpt)
    {
        StringBuilder param = new StringBuilder();
        try
        {
            if (ddlreporttype.SelectedValue == "11" == true)
            {
                string showbalance;
                if (ChkShowAllStudent.Checked == true)
                {
                    showbalance = "0";
                }
                else
                {
                    showbalance = "1";
                }
                param.Append("UserName=" + Session["userfullname"].ToString());
                param.Append(",@P_COUNTERNO=" + dcrRpt.CounterNo.ToString() + ",@p_PAYMODE=" + dcrRpt.PaymentMode);
                param.Append(",@P_RECIEPT_CODE=" + dcrRpt.ReceiptTypes + "");
                param.Append(",@P_BRANCHNO=" + dcrRpt.BranchNo.ToString() + ",@P_DEGREENO=" + dcrRpt.DegreeNo.ToString());
                param.Append(",@P_SEMESTERNO=" + dcrRpt.SemesterNo.ToString() + ",@P_REC_FROM_DT=" + dcrRpt.FDate.ToString());
                param.Append(",@P_PAYTYPENO=" + dcrRpt.PayTypeNo.ToString() + "");
                param.Append(",@P_SHOWBALANCE=" + showbalance + "");
                param.Append(",@P_REC_TO_DATE=" + dcrRpt.ToDate.ToShortDateString() + "");
                param.Append(",Degree=" + ((ddlDegree.SelectedIndex > 0) ? ddlDegree.SelectedItem.Text : "0"));
                param.Append(",Branch=" + ((ddlBranch.SelectedIndex > 0) ? ddlBranch.SelectedItem.Text : "0"));
                param.Append(",Semester=" + ((ddlSemester.SelectedIndex > 0) ? ddlSemester.SelectedItem.Text : "0"));
                param.Append(",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]));



            }
            else
            {
                if (rdoTransferReport.Checked == true)
                {
                    param.Append("UserName=" + Session["userfullname"].ToString());
                    param.Append(",@P_COUNTERNO=" + dcrRpt.CounterNo.ToString() + ",@P_PAYMENT_MODE=" + dcrRpt.PaymentMode);
                    param.Append(",@P_RECIEPTCODE=" + dcrRpt.ReceiptTypes + ",@P_DEGREENO=" + dcrRpt.DegreeNo.ToString());
                    param.Append(",@P_BRANCHNO=" + dcrRpt.BranchNo.ToString());
                    param.Append(",@P_PAYTYPENO=" + dcrRpt.PayTypeNo.ToString() + "");
                    param.Append(",@P_VERSION=" + 2 + "");
                    param.Append(",@P_REC_TO_DATE=" + dcrRpt.ToDate.ToShortDateString());// + ",@P_RECIEPT_CODE=" + dcrRpt.ReceiptTypes);
                    param.Append(",Degree=" + ((ddlDegree.SelectedIndex > 0) ? ddlDegree.SelectedItem.Text : "0"));
                    param.Append(",Branch=" + ((ddlBranch.SelectedIndex > 0) ? ddlBranch.SelectedItem.Text : "0"));
                    param.Append(",Year=" + ((ddlYear.SelectedIndex > 0) ? ddlYear.SelectedItem.Text : "0"));
                    param.Append(",Semester=" + ((ddlSemester.SelectedIndex > 0) ? ddlSemester.SelectedItem.Text : "0"));
                    param.Append(",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]));
                }
                else if (rdofeecolother.Checked == true)
                {
                    param.Append("UserName=" + Session["userfullname"].ToString());
                    param.Append(",@P_COUNTERNO=" + dcrRpt.CounterNo.ToString() + ",@P_PAYMENT_MODE=" + dcrRpt.PaymentMode);
                    param.Append(",@P_RECIEPTCODE=" + dcrRpt.ReceiptTypes + ",@P_DEGREENO=" + dcrRpt.DegreeNo.ToString());
                    param.Append(",@P_BRANCHNO=" + dcrRpt.BranchNo.ToString());// + ",@P_YEARNO=" + dcrRpt.YearNo.ToString());
                    param.Append(",@P_SEMESTERNO=" + dcrRpt.SemesterNo.ToString() + ",@P_REC_FROM_DT=" + dcrRpt.FromDate.ToShortDateString());
                    param.Append(",@P_PAYTYPENO=" + dcrRpt.PayTypeNo.ToString() + "");
                    param.Append(",@P_VERSION=" + 2 + "");
                    param.Append(",@P_REC_TO_DATE=" + dcrRpt.ToDate.ToShortDateString());// + ",@P_RECIEPT_CODE=" + dcrRpt.ReceiptTypes);
                    param.Append(",Degree=" + ((ddlDegree.SelectedIndex > 0) ? ddlDegree.SelectedItem.Text : "0"));
                    param.Append(",Branch=" + ((ddlBranch.SelectedIndex > 0) ? ddlBranch.SelectedItem.Text : "0"));
                    param.Append(",Year=" + ((ddlYear.SelectedIndex > 0) ? ddlYear.SelectedItem.Text : "0"));
                    param.Append(",Semester=" + ((ddlSemester.SelectedIndex > 0) ? ddlSemester.SelectedItem.Text : "0"));
                    param.Append(",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]));
                }
                else if (rdodcb.Checked == true)
                {
                    param.Append("UserName=" + Session["userfullname"].ToString());
                    //param.Append(",@P_COUNTERNO=" + dcrRpt.CounterNo.ToString() + ",@P_PAYMENT_MODE=" + dcrRpt.PaymentMode);
                    param.Append(",@P_RECIEPT_CODE=" + dcrRpt.ReceiptTypes + ",@P_DEGREENO=" + dcrRpt.DegreeNo.ToString());
                    param.Append(",@P_BRANCHNO=" + dcrRpt.BranchNo.ToString());// + ",@P_YEAR=" + dcrRpt.YearNo.ToString());
                    param.Append(",@P_SEMESTERNO=" + dcrRpt.SemesterNo.ToString() + ",@P_REC_FROM_DT=" + dcrRpt.FromDate.ToShortDateString());
                    param.Append(",@P_PAYTYPENO=" + dcrRpt.PayTypeNo.ToString() + "");
                    //param.Append(",@P_VERSION=" + 2 + "");
                    param.Append(",@P_REC_TO_DATE=" + dcrRpt.ToDate.ToShortDateString());
                    param.Append(",Degree=" + ((ddlDegree.SelectedIndex > 0) ? ddlDegree.SelectedItem.Text : "0"));
                    param.Append(",Branch=" + ((ddlBranch.SelectedIndex > 0) ? ddlBranch.SelectedItem.Text : "0"));
                    param.Append(",Year=" + ((ddlYear.SelectedIndex > 0) ? ddlYear.SelectedItem.Text : "0"));
                    param.Append(",Semester=" + ((ddlSemester.SelectedIndex > 0) ? ddlSemester.SelectedItem.Text : "0"));
                    param.Append(",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]));
                }
                else if (rdomiscregfee.Checked == true)
                {

                    param.Append("UserName=" + Session["userfullname"].ToString());
                    param.Append(",@P_COUNTERNO=" + dcrRpt.CounterNo.ToString() + ",@P_PAYMENT_MODE=" + dcrRpt.PaymentMode);
                    param.Append(",@P_RECIEPTCODE=" + ViewState["chkrectype"].ToString() + ",@P_DEGREENO=" + dcrRpt.DegreeNo.ToString());
                    param.Append(",@P_BRANCHNO=" + dcrRpt.BranchNo.ToString());// + ",@P_YEARNO=" + dcrRpt.YearNo.ToString());
                    param.Append(",@P_SEMESTERNO=" + dcrRpt.SemesterNo.ToString() + ",@P_REC_FROM_DT=" + dcrRpt.FromDate.ToShortDateString());
                    param.Append(",@P_PAYTYPENO=" + dcrRpt.PayTypeNo.ToString() + "");

                    param.Append(",@P_REC_TO_DATE=" + dcrRpt.ToDate.ToShortDateString());
                    param.Append(",Degree=" + ((ddlDegree.SelectedIndex > 0) ? ddlDegree.SelectedItem.Text : "0"));
                    param.Append(",Branch=" + ((ddlBranch.SelectedIndex > 0) ? ddlBranch.SelectedItem.Text : "0"));
                    param.Append(",Year=" + ((ddlYear.SelectedIndex > 0) ? ddlYear.SelectedItem.Text : "0"));
                    param.Append(",Semester=" + ((ddlSemester.SelectedIndex > 0) ? ddlSemester.SelectedItem.Text : "0"));
                    param.Append(",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]));

                }
                else if (ddlreporttype.SelectedValue == "9" == true)
                {
                    param.Append("UserName=" + Session["userfullname"].ToString());
                    param.Append(",@P_COUNTERNO=" + dcrRpt.CounterNo.ToString() + ",@P_PAYMENT_MODE=" + dcrRpt.PaymentMode);
                    param.Append(",@P_RECIEPTCODE=" + dcrRpt.ReceiptTypes + ",@P_DEGREENO=" + dcrRpt.DegreeNo.ToString());
                    param.Append(",@P_BRANCHNO=" + dcrRpt.BranchNo.ToString());// + ",@P_YEARNO=" + dcrRpt.YearNo.ToString());
                    param.Append(",@P_SEMESTERNO=" + dcrRpt.SemesterNo.ToString() + ",@P_REC_FROM_DT=" + dcrRpt.FromDate.ToShortDateString());
                    param.Append(",@P_PAYTYPENO=" + dcrRpt.PayTypeNo.ToString() + "");
                    param.Append(",@P_VERSION=" + 1 + "");
                    param.Append(",@P_REC_TO_DATE=" + dcrRpt.ToDate.ToShortDateString());// + ",@P_RECIEPT_CODE=" + dcrRpt.ReceiptTypes);
                    param.Append(",Degree=" + ((ddlDegree.SelectedIndex > 0) ? ddlDegree.SelectedItem.Text : "0"));
                    param.Append(",Branch=" + ((ddlBranch.SelectedIndex > 0) ? ddlBranch.SelectedItem.Text : "0"));
                    param.Append(",Year=" + ((ddlYear.SelectedIndex > 0) ? ddlYear.SelectedItem.Text : "0"));
                    param.Append(",Semester=" + ((ddlSemester.SelectedIndex > 0) ? ddlSemester.SelectedItem.Text : "0"));
                    param.Append(",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]));
                }
                else if (ddlreporttype.SelectedValue == "14" == true)
                {
                    param.Append("@P_COLLEGE_ID=" + ddlCol.SelectedValue);
                    param.Append(",@P_DEGREENO=" + ddlDegree.SelectedValue);
                    param.Append(",@P_BRANCHNO=" + ddlBranch.SelectedValue);
                    param.Append(",@P_SEMESTERNO=" + ddlSemester.SelectedValue);
                    param.Append(",@P_REC_FROM_DT=" + dcrRpt.FromDate.ToShortDateString());
                    param.Append(",@P_REC_TO_DATE=" + dcrRpt.ToDate.ToShortDateString());// + ",@P_RECIEPT_CODE=" + dcrRpt.ReceiptTypes);
                    param.Append(",@P_COLLEGE_CODE=" + Session["colcode"].ToString());
                }
                if (rdopnbwithcheque.Checked == true || rdbotherbankwithcheque.Checked == true)
                {
                    param.Append("UserName=" + Session["userfullname"].ToString());
                    param.Append(",@P_COUNTERNO=" + dcrRpt.CounterNo.ToString() + ",@P_PAYMENT_MODE=" + dcrRpt.PaymentMode);
                    param.Append(",@P_RECIEPTCODE=" + dcrRpt.ReceiptTypes + ",@P_DEGREENO=" + dcrRpt.DegreeNo.ToString());
                    param.Append(",@P_BRANCHNO=" + dcrRpt.BranchNo.ToString());// + ",@P_YEARNO=" + dcrRpt.YearNo.ToString());
                    param.Append(",@P_SEMESTERNO=" + dcrRpt.SemesterNo.ToString() + ",@P_REC_FROM_DT=" + dcrRpt.FromDate.ToShortDateString());
                    param.Append(",@P_PAYTYPENO=" + dcrRpt.PayTypeNo.ToString() + "");
                    param.Append(",@P_VERSION=" + 2 + "");
                    param.Append(",@P_REC_TO_DATE=" + dcrRpt.ToDate.ToShortDateString());// + ",@P_RECIEPT_CODE=" + dcrRpt.ReceiptTypes);
                    param.Append(",Degree=" + ((ddlDegree.SelectedIndex > 0) ? ddlDegree.SelectedItem.Text : "0"));
                    param.Append(",Branch=" + ((ddlBranch.SelectedIndex > 0) ? ddlBranch.SelectedItem.Text : "0"));
                    param.Append(",Year=" + ((ddlYear.SelectedIndex > 0) ? ddlYear.SelectedItem.Text : "0"));
                    param.Append(",Semester=" + ((ddlSemester.SelectedIndex > 0) ? ddlSemester.SelectedItem.Text : "0"));
                    param.Append(",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]));
                }
                else
                {
                    if (ddlreporttype.SelectedValue != "14")
                    {
                        param.Append("UserName=" + Session["userfullname"].ToString());
                        param.Append(",@P_COUNTERNO=" + dcrRpt.CounterNo.ToString() + ",@P_PAYMENT_MODE=" + dcrRpt.PaymentMode);
                        param.Append(",@P_RECIEPTCODE=" + dcrRpt.ReceiptTypes + ",@P_DEGREENO=" + dcrRpt.DegreeNo.ToString());
                        param.Append(",@P_BRANCHNO=" + dcrRpt.BranchNo.ToString());// + ",@P_YEARNO=" + dcrRpt.YearNo.ToString());
                        param.Append(",@P_SEMESTERNO=" + dcrRpt.SemesterNo.ToString() + ",@P_REC_FROM_DT=" + dcrRpt.FromDate.ToShortDateString());
                        param.Append(",@P_PAYTYPENO=" + dcrRpt.PayTypeNo.ToString() + "");

                        param.Append(",@P_REC_TO_DATE=" + dcrRpt.ToDate.ToShortDateString());// + ",@P_RECIEPT_CODE=" + dcrRpt.ReceiptTypes);
                        param.Append(",Degree=" + ((ddlDegree.SelectedIndex > 0) ? ddlDegree.SelectedItem.Text : "0"));
                        param.Append(",Branch=" + ((ddlBranch.SelectedIndex > 0) ? ddlBranch.SelectedItem.Text : "0"));
                        param.Append(",Year=" + ((ddlYear.SelectedIndex > 0) ? ddlYear.SelectedItem.Text : "0"));
                        param.Append(",Semester=" + ((ddlSemester.SelectedIndex > 0) ? ddlSemester.SelectedItem.Text : "0"));
                        param.Append(",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]));
                    }
                }
                if (ddlreporttype.SelectedValue != "14")
                {
                    if (Convert.ToInt32(Session["OrgId"]).ToString() == "2") // crescent
                    {
                        param.Append(",@P_PayType=" + ((ddlPaytype.SelectedIndex > 0) ? ddlPaytype.SelectedValue : ""));
                        param.Append(",@P_YEARNO=" + dcrRpt.YearNos.ToString());
                    }
                    else
                    {
                        param.Append(",@P_YEARNO=" + dcrRpt.YearNo.ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return param.ToString();
    }

    private DataSet GetReportingData(DailyFeeCollectionRpt reportCriteria)
    {
        DataSet ds = null;
        try
        {
            DailyFeeCollectionController dcrController = new DailyFeeCollectionController();

            if (ddlreporttype.SelectedValue == "8")
            {
                ds = dcrController.GetFeeCollectionReportData(reportCriteria);
            }
            else if (ddlreporttype.SelectedValue == "12")
            {
                ds = dcrController.GetOutstandingFees(reportCriteria);
            }
            else if (ddlreporttype.SelectedValue == "9")
            {
                if (rdoDetailedReport.Checked)
                {
                    ds = dcrController.GetDemandDraftsReportData(reportCriteria, 1);
                }
                else
                {
                    ds = dcrController.GetDemandDraftsReportDataSummary(reportCriteria, 1);
                }


            }
            else if (rdoTransferReport.Checked)
            {
                if (rdoDetailedReport.Checked)
                {
                    ds = dcrController.GetDemandDraftsReportData(reportCriteria, 2);
                }
                else
                {
                    ds = dcrController.GetDemandDraftsReportDataSummary(reportCriteria, 2);
                }
            }
            else if (ddlreporttype.SelectedValue == "10")
            {
                if (rdoDetailedReport.Checked)
                {
                    ds = dcrController.GetFeeItemsDetailed_ReportData(reportCriteria);
                }
                else
                {
                    ds = dcrController.GetFeeItemsSummary_ReportData(reportCriteria);
                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }
        return ds;
    }

    private DataSet GetReportingDataExcel(DailyFeeCollectionRpt reportCriteria)
    {
        DataSet ds = null;
        try
        {
            DailyFeeCollectionController dcrController = new DailyFeeCollectionController();

            if (ddlreporttype.SelectedValue == "8")
            {
                ds = dcrController.GetFeeCollectionReportDataExcel(reportCriteria);
            }
            else if (ddlreporttype.SelectedValue == "11")
            {
                ds = dcrController.GetOutstandingFeesExcel(reportCriteria);
            }
            else if (ddlreporttype.SelectedValue == "9")
            {
                if (rdoDetailedReport.Checked)
                {
                    ds = dcrController.GetDemandDraftsReportData(reportCriteria, 1);
                }
                else
                {
                    ds = dcrController.GetDemandDraftsReportDataSummary(reportCriteria, 1);
                }


            }
            else if (rdoTransferReport.Checked)
            {
                if (rdoDetailedReport.Checked)
                {
                    ds = dcrController.GetDemandDraftsReportData(reportCriteria, 2);
                }
                else
                {
                    ds = dcrController.GetDemandDraftsReportDataSummary(reportCriteria, 2);
                }
            }
            else if (ddlreporttype.SelectedValue == "10")
            {
                if (rdoDetailedReport.Checked)
                {
                    ds = dcrController.GetFeeItemsDetailed_ReportData(reportCriteria);
                }
                else
                {
                    ds = dcrController.GetFeeItemsSummary_ReportData(reportCriteria);
                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }
        return ds;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // Reload/refresh complete page. 
        Response.Redirect(Request.Url.ToString());
    }

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH a INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON a.BRANCHNO=B.BRANCHNO", "B.BRANCHNO", "A.LONGNAME", "B.DEGREENO=" + ddlDegree.SelectedValue, "A.SHORTNAME");
    }

    protected void ddlReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string rec_code = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE='" + ddlReceiptType.SelectedItem.Text + "'");
        //this.objCommon.FillDropDownList(ddlCounterNo, "ACD_COUNTER_REF", "COUNTERNO", "PRINTNAME", "RECEIPT_PERMISSION = '" + rec_code + "'", "COUNTERNO");
        this.objCommon.FillDropDownList(ddlCounterNo, "acd_counter_ref a inner join user_acc u on (a.ua_no=u.ua_no)", "a.COUNTERNO", "a.PRINTNAME+'-'+(u.ua_name collate database_default)+'-'+a.RECEIPT_PERMISSION", "RECEIPT_PERMISSION = '" + rec_code + "'", "COUNTERNO");
    }

    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlreporttype.SelectedValue != "9" && ddlReceiptType.SelectedIndex == 0)
            {
                ShowMessage("Receipt type is required for this report. \\nPlease select a receipt type.");
                ddlReceiptType.Focus();
                return;
            }
            string reportTitle = string.Empty;
            string rptFileName = string.Empty;

            this.SetReportFileAndTitle(ref reportTitle, ref rptFileName);
            GridView GVDayWiseAtt = new GridView();
            DailyFeeCollectionRpt dcrReport = GetReportCriteria();
            //if (ddlreporttype.SelectedValue=="5" == false)
            //{
            DataSet ds = this.GetReportingDataExcel(dcrReport);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string degree = objCommon.LookUp("ACD_DEGREE", "DEGREENAME", "DEGREENO=" + ddlDegree.SelectedValue);
                //this.ShowReportExcel("xls",dcrReport, reportTitle, rptFileName);
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=" + degree + ".xls";
                //string attachment = "attachment; filename=" + degree.Replace(" ", "_") + "_" + branch.Replace(" ", "_") + "_" + ccode + "_" + txtAttDate.Text.Trim() + "_" + txtRecDate.Text.Trim() + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                //Response.ContentType = "application/pdf";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                this.ShowMessage("No information found based on given criteria.");
            }
            //}
            //else
            //{
            //    DataSet ds = this.GetOutstandingFees(dcrReport);

            //    //this.ShowReportExcel("xls",dcrReport, reportTitle, rptFileName);
            //}
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowReportExcel(string exporttype, DailyFeeCollectionRpt dcrRpt, string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            url += "exporttype=" + exporttype;
            url += "&filename=" + reportTitle.Replace(" ", "-").ToString() + "." + exporttype;

            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=" + this.GetReportParameters(dcrRpt);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'> try{";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description); } </script>";

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //Show the Excel Sheet
    //private void ShowCombinedShiftExcelSheet()
    //{
    //    try
    //    {
    //        GridView GVDayWiseAtt = new GridView();
    //        string degree = objCommon.LookUp("ACD_DEGREE", "DEGREENAME", "DEGREENO=" + ddlDegree.SelectedValue);

    //        //string ContentType = string.Empty;
    //        //char ch = '/';
    //        //string[] Fromdate = txtAttDate.Text.Split(ch);
    //        //string[] Todate = txtRecDate.Text.Split(ch);
    //        //string fdate = Fromdate[1] + "/" + Fromdate[0] + "/" + Fromdate[2];
    //        //string tdate = Todate[1] + "/" + Todate[0] + "/" + Todate[2];
    //        StudentController objSC = new StudentController();
    //        DataSet ds = objSC.GetDataInExcelSheetForTSIPSI(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(StudentCategory.SelectedValue));
    //        //DataSet ds = objAC.GetDateWiseData(Convert.ToInt32(ddlSession.SelectedValue), SchemeNo, Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), txtFromDate.Text, txtTodate.Text);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            GVDayWiseAtt.DataSource = ds;
    //            GVDayWiseAtt.DataBind();

    //            string attachment = "attachment; filename=" + degree + ".xls";
    //            //string attachment = "attachment; filename=" + degree.Replace(" ", "_") + "_" + branch.Replace(" ", "_") + "_" + ccode + "_" + txtAttDate.Text.Trim() + "_" + txtRecDate.Text.Trim() + ".xls";
    //            Response.ClearContent();
    //            Response.AddHeader("content-disposition", attachment);
    //            Response.ContentType = "application/vnd.MS-excel";
    //            //Response.ContentType = "application/pdf";
    //            StringWriter sw = new StringWriter();
    //            HtmlTextWriter htw = new HtmlTextWriter(sw);
    //            GVDayWiseAtt.RenderControl(htw);
    //            Response.Write(sw.ToString());
    //            Response.End();

    //            //PDF
    //            //Response.ContentType = "application/pdf";
    //            //Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
    //            //Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
    //            //StringWriter sw = new StringWriter();

    //            //HtmlTextWriter hw = new HtmlTextWriter(sw);
    //            //GVDayWiseAtt.AllowPaging = false;
    //            //GVDayWiseAtt.DataBind();
    //            //GVDayWiseAtt.RenderControl(hw);

    //            //StringReader sr = new StringReader(sw.ToString());
    //            //Document pdfDoc = new Document(PageSize.LETTER_LANDSCAPE, 10f, 10f, 10f, 0f);

    //            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
    //            //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
    //            //pdfDoc.Open();
    //            //htmlparser.Parse(sr);
    //            //pdfDoc.Close();
    //            //Response.Write(pdfDoc);
    //            //Response.End();    
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage("No Data Found for current selection.", this.Page);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //     throw;
    //    }
    //}

    protected void rdoOutstandingReport_CheckedChanged(object sender, EventArgs e)
    {
        if (ddlreporttype.SelectedValue == "11")
        {
            outfieldset.Visible = true;
            ChkShowAllStudent.Visible = true;
            ChkShowStudentsWithBalance.Visible = true;
            divrectypesingle.Visible = true;
            divrectype.Visible = false;
            ddlreporttype.SelectedValue = "11";
            btnExcel.Enabled = true;
            btnExcelFormat2.Enabled = true;
        }
        else
        {
            outfieldset.Visible = false;
            ChkShowAllStudent.Visible = false;
            ChkShowStudentsWithBalance.Visible = false;
            ddlreporttype.SelectedValue = "0";
            btnExcel.Enabled = false;
        }
    }

    protected void ChkShowAllStudent_CheckedChanged(object sender, EventArgs e)
    {
        divrectypesingle.Visible = true;
        divrectype.Visible = false;
        ChkShowStudentsWithBalance.Checked = false;
    }

    protected void ChkShowStudentsWithBalance_CheckedChanged(object sender, EventArgs e)
    {
        divrectypesingle.Visible = true;
        divrectype.Visible = false;
        ChkShowAllStudent.Checked = false;
    }

    protected void ddlreporttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlreporttype.SelectedValue == "8")
        {
            divrectypesingle.Visible = true;
            divrectype.Visible = false;
            divdetailsumary.Visible = true;
            rdoSummeryReport.Enabled = true;
            fromDSpan.Visible = true;
            MaskedEditValidator1.Enabled = true;
            idDataFilter.Visible = true;
            pnlOSReportFormat1.Visible = false;
            btnExcelFormat2.Enabled = true;
            btnExcel.Enabled = false;
        }
        else if (ddlreporttype.SelectedValue == "9")
        {
            divrectypesingle.Visible = true;
            divrectype.Visible = false;
            divdetailsumary.Visible = true;
            rdoSummeryReport.Enabled = true;
            fromDSpan.Visible = true;
            MaskedEditValidator1.Enabled = true;
            idDataFilter.Visible = true;
            pnlOSReportFormat1.Visible = false;
            btnExcelFormat2.Enabled = true;
            btnExcel.Enabled = false;
        }
        else if (ddlreporttype.SelectedValue == "10")
        {
            divrectypesingle.Visible = true;
            divrectype.Visible = false;
            divdetailsumary.Visible = true;
            rdoSummeryReport.Enabled = true;
            fromDSpan.Visible = true;
            MaskedEditValidator1.Enabled = true;
            idDataFilter.Visible = true;
            pnlOSReportFormat1.Visible = false;
            btnExcelFormat2.Enabled = true;
            btnExcel.Enabled = false;
        }
        else if (ddlreporttype.SelectedValue == "12")
        {
            divdetailsumary.Visible = true;
            rdoSummeryReport.Enabled = true;
            divrectypesingle.Visible = true;
            divrectype.Visible = false;
            fromDSpan.Visible = true;
            txtFromDate.Text = string.Empty;
            MaskedEditValidator1.Enabled = false;
            btnExcel.Enabled = true;
            idDataFilter.Visible = false;
            pnlOSReportFormat1.Visible = true;
            btnExcelFormat2.Enabled = true;
            if (ddlreporttype.SelectedValue == "13" == false)
            {
                btnExcel.Enabled = true;
            }
        }

    }


    protected void rdoTransferReport_CheckedChanged(object sender, EventArgs e)
    {
        divrectypesingle.Visible = true;
        divrectype.Visible = false;
        divdetailsumary.Visible = false;
        rdoDetailedReport.Checked = true;
        outfieldset.Visible = false;
        ChkShowAllStudent.Visible = false;
        ChkShowStudentsWithBalance.Visible = false;
        ddlreporttype.SelectedValue = "0";
        idDataFilter.Visible = true;
        pnlOSReportFormat1.Visible = false;
        btnExcelFormat2.Enabled = true;
        btnExcel.Enabled = false;
    }

    protected void rdofeecolother_CheckedChanged(object sender, EventArgs e)
    {
        divrectypesingle.Visible = true;
        divrectype.Visible = false;
        divdetailsumary.Visible = false;
        outfieldset.Visible = false;
        rdoDetailedReport.Checked = true;
        ChkShowAllStudent.Visible = false;
        ChkShowStudentsWithBalance.Visible = false;
        ddlreporttype.SelectedValue = "0";
        idDataFilter.Visible = true;
        pnlOSReportFormat1.Visible = false;
        btnExcelFormat2.Enabled = true;
        btnExcel.Enabled = false;
    }
    protected void rdoOutstandingReport_CheckedChanged1(object sender, EventArgs e)
    {

    }

    protected void rdodcb_CheckedChanged(object sender, EventArgs e)
    {
        divdetailsumary.Visible = false;
        divrectypesingle.Visible = true;
        divrectype.Visible = false;
        outfieldset.Visible = false;
        rdoDetailedReport.Checked = true;
        ChkShowAllStudent.Visible = false;
        ChkShowStudentsWithBalance.Visible = false;
        ddlreporttype.SelectedValue = "0";
        idDataFilter.Visible = true;
        pnlOSReportFormat1.Visible = false;
        btnExcelFormat2.Enabled = true;
        btnExcel.Enabled = true;
    }
    protected void rdomiscregfee_CheckedChanged(object sender, EventArgs e)
    {
        divrectype.Visible = true;
        divrectypesingle.Visible = false;
        divdetailsumary.Visible = false;
        outfieldset.Visible = false;
        rdoDetailedReport.Checked = true;
        ChkShowAllStudent.Visible = false;
        ChkShowStudentsWithBalance.Visible = false;
        ddlreporttype.SelectedValue = "0";
        idDataFilter.Visible = true;
        pnlOSReportFormat1.Visible = false;
        btnExcelFormat2.Enabled = true;
        btnExcel.Enabled = true;
    }
    protected void rdopnbwithcheque_CheckedChanged(object sender, EventArgs e)
    {
        divrectypesingle.Visible = true;
        divrectype.Visible = false;
        divdetailsumary.Visible = false;
        outfieldset.Visible = false;
        rdoDetailedReport.Checked = true;
        ChkShowAllStudent.Visible = false;
        ChkShowStudentsWithBalance.Visible = false;
        ddlreporttype.SelectedValue = "0";
        idDataFilter.Visible = true;
        pnlOSReportFormat1.Visible = false;
        btnExcelFormat2.Enabled = true;
        btnExcel.Enabled = true;
    }
    protected void rdbotherbankwithcheque_CheckedChanged(object sender, EventArgs e)
    {
        divrectypesingle.Visible = true;
        divrectype.Visible = false;
        divdetailsumary.Visible = false;
        outfieldset.Visible = false;
        rdoDetailedReport.Checked = true;
        ChkShowAllStudent.Visible = false;
        ChkShowStudentsWithBalance.Visible = false;
        ddlreporttype.SelectedValue = "0";
        idDataFilter.Visible = true;
        pnlOSReportFormat1.Visible = false;
        btnExcelFormat2.Enabled = true;
        btnExcel.Enabled = true;
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (txtToDate.Text == string.Empty)
        {
            ShowMessage("Please Enter To Date");
            txtToDate.Focus();
            return;
        }
        if (ddlReceiptType.SelectedIndex == 0)
        {
            ShowMessage("Receipt type is required for this report. \\nPlease select a receipt type.");
            ddlReceiptType.Focus();
            return;
        }

        DailyFeeCollectionRpt outReport = new DailyFeeCollectionRpt();
        DailyFeeCollectionController objFeeControll = new DailyFeeCollectionController();
        fromDSpan.Visible = false;
        txtFromDate.Text = string.Empty;
        MaskedEditValidator1.Enabled = false;
        DataSet ds = null;

        outReport.ReceiptTypes = ddlReceiptType.SelectedValue;
        outReport.BranchNo = (ddlBranch.SelectedIndex > 0) ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
        outReport.DegreeNo = (ddlDegree.SelectedIndex > 0) ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
        outReport.SemesterNo = (ddlSemester.SelectedIndex > 0) ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;

        if (ChkShowAllStudent.Checked == true)
        {
            outReport.ShowBalance = "0";
        }
        else
        {
            outReport.ShowBalance = "1";
        }
        outReport.FDate = txtFromDate.Text == string.Empty ? string.Empty : txtFromDate.Text.ToString();
        outReport.ToDate = txtToDate.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(txtToDate.Text);
        outReport.CounterNo = (ddlCounterNo.SelectedIndex > 0) ? Int32.Parse(ddlCounterNo.SelectedValue) : 0;
        outReport.PayTypeNo = (ddlPaymentType.SelectedIndex > 0) ? Convert.ToInt32(ddlPaymentType.SelectedValue) : 0;
        outReport.PaymentMode = (ddlPaymentMode.SelectedIndex > 0) ? ddlPaymentMode.SelectedValue : string.Empty;
        DataGrid Gr = new DataGrid();
        ds = objFeeControll.GetOutstandingReportExcel(outReport);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Gr.DataSource = ds;
                Gr.DataBind();
                string Attachment = "Attachment; FileName=OutstandingReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Gr.HeaderStyle.Font.Bold = true;
                Gr.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {

            }
        }
        else
        {

        }
    }
    protected void ddlCol_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCol.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "A.COLLEGE_ID=" + Convert.ToInt32(ddlCol.SelectedValue), "a.DEGREENO");
        }
    }


    protected void btnExcelFormat2_Click(object sender, EventArgs e)
    {
        string colleges = string.Empty;
        string degrees = string.Empty;
        string branches = string.Empty;
        string semesters = string.Empty;
        string years = string.Empty;

        if (txtToDate.Text == string.Empty)
        {
            ShowMessage("Please Enter To Date");
            txtToDate.Focus();
            return;
        }
        if (ddlReceiptType.SelectedIndex == 0)
        {
            ShowMessage("Receipt type is required for this report. \\nPlease select a receipt type.");
            ddlReceiptType.Focus();
            return;
        }

        DailyFeeCollectionRpt outReport = new DailyFeeCollectionRpt();
        DailyFeeCollectionController objFeeControll = new DailyFeeCollectionController();


        fromDSpan.Visible = false;
        txtFromDate.Text = string.Empty;
        MaskedEditValidator1.Enabled = false;
        DataSet ds = null;
        outReport.ReceiptTypes = ddlReceiptType.SelectedValue;
        outReport.BranchNo = (ddlBranch.SelectedIndex > 0) ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
        outReport.DegreeNo = (ddlDegree.SelectedIndex > 0) ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
        outReport.SemesterNo = (ddlSemester.SelectedIndex > 0) ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;

        if (ChkShowAllStudent.Checked == true)
        {
            outReport.ShowBalance = "0";
        }
        else
        {
            outReport.ShowBalance = "1";
        }
        outReport.FDate = txtFromDate.Text == string.Empty ? string.Empty : txtFromDate.Text.ToString();
        outReport.ToDate = txtToDate.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(txtToDate.Text);
        outReport.CounterNo = (ddlCounterNo.SelectedIndex > 0) ? Int32.Parse(ddlCounterNo.SelectedValue) : 0;
        outReport.PayTypeNo = (ddlPaymentType.SelectedIndex > 0) ? Convert.ToInt32(ddlPaymentType.SelectedValue) : 0;
        outReport.PaymentMode = (ddlPaymentMode.SelectedIndex > 0) ? ddlPaymentMode.SelectedValue : string.Empty;

        foreach (ListItem items in lbOSCollege.Items) //collegenos
        {
            if (items.Selected == true)
            {
                colleges += items.Value + ',';
            }
        }
        if (colleges.Length > 1)
        {
            colleges = colleges.Remove(colleges.Length - 1);
        }
        outReport.CollegeNos = colleges; //end

        foreach (ListItem items in lbOSDegree.Items) //degreenos
        {
            if (items.Selected == true)
            {
                degrees += items.Value + ',';
            }
        }
        if (degrees.Length > 1)
        {
            degrees = degrees.Remove(degrees.Length - 1);
        }
        outReport.DegreeNos = degrees; //end


        foreach (ListItem items in lbOSBranch.Items) //branchnos
        {
            if (items.Selected == true)
            {
                branches += items.Value + ',';
            }
        }
        if (branches.Length > 1)
        {
            branches = branches.Remove(branches.Length - 1);
        }
        outReport.BranchNos = branches; //end

        foreach (ListItem items in lbOSSemester.Items) //semesternos
        {
            if (items.Selected == true)
            {
                semesters += items.Value + ',';
            }
        }
        if (semesters.Length > 1)
        {
            semesters = semesters.Remove(semesters.Length - 1);
        }
        outReport.SemesterNos = semesters; //end


        foreach (ListItem items in lbOSYear.Items) //yearnos
        {
            if (items.Selected == true)
            {
                years += items.Value + ',';
            }
        }
        if (years.Length > 1)
        {
            years = years.Remove(years.Length - 1);
        }
        outReport.YearNos = years;


        DataGrid Gr = new DataGrid();

        ds = objFeeControll.GetOutstandingReportExcelFormat2(outReport);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Gr.DataSource = ds;
                Gr.DataBind();
                string Attachment = "Attachment; FileName=OutstandingReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Gr.HeaderStyle.Font.Bold = true;
                Gr.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayUserMessage(pnlFeeTable, "No Data Found for current selection.", this.Page);
            }
        }
        else
        {

        }
    }


    protected void rdoOutstandingrptFormat1_CheckedChanged(object sender, EventArgs e)
    {
        idDataFilter.Visible = false;
        rdoSummeryReport.Enabled = true;
        pnlOSReportFormat1.Visible = true;
        btnExcelFormat2.Enabled = true;
        btnExcel.Enabled = true;
    }
    protected void lbOSCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            PopulateDegreeList();
        }

        catch (Exception ex)
        {
            throw;
        }
    }

    protected void PopulateDegreeList()
    {
        lbOSDegree.Items.Clear();
        lbOSBranch.Items.Clear();
        lbOSSemester.Items.Clear();
        lbOSYear.Items.Clear();
        try
        {
            int count = 0;

            string values = string.Empty;
            foreach (ListItem Item in lbOSCollege.Items)
            {
                if (Item.Selected)
                {
                    values += Item.Value + ",";
                    count++;
                }
            }

            if (count > 0)
            {
                string ColgNos = values.TrimEnd(',');
                DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "A.COLLEGE_ID IN (" + ColgNos + ")", "a.DEGREENO");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lbOSDegree.DataTextField = "DEGREENAME";
                        lbOSDegree.DataValueField = "DEGREENO";
                        lbOSDegree.ToolTip = "DEGREENO";
                        lbOSDegree.DataSource = ds.Tables[0];
                        lbOSDegree.DataBind();
                    }

                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void lbOSDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            PopulateBranchList();
        }

        catch (Exception ex)
        {
            throw;
        }
    }

    private void PopulateBranchList()
    {
        lbOSBranch.Items.Clear();
        lbOSSemester.Items.Clear();
        lbOSYear.Items.Clear();
        try
        {
            int count = 0;

            string values = string.Empty;
            foreach (ListItem Item in lbOSDegree.Items)
            {
                if (Item.Selected)
                {
                    values += Item.Value + ",";
                    count++;
                }
            }

            if (count > 0)
            {
                string degNos = values.TrimEnd(',');
                DataSet ds = objCommon.FillDropDown("ACD_BRANCH a INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON a.BRANCHNO=B.BRANCHNO", "DISTINCT B.BRANCHNO", "A.LONGNAME", "B.DEGREENO IN(" + degNos + ")", "B.BRANCHNO");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lbOSBranch.DataTextField = "LONGNAME";
                        lbOSBranch.DataValueField = "BRANCHNO";
                        lbOSBranch.ToolTip = "BRANCHNO";
                        lbOSBranch.DataSource = ds.Tables[0];
                        lbOSBranch.DataBind();
                    }

                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void lbOSBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbOSSemester.Items.Clear();
        lbOSYear.Items.Clear();
        int count = 0;

        string values = string.Empty;
        foreach (ListItem Item in lbOSBranch.Items)
        {
            if (Item.Selected)
            {
                values += Item.Value + ",";
                count++;
            }
        }
        if (count > 0)
        {
            this.objCommon.FillListBox(lbOSSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            this.objCommon.FillListBox(lbOSYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "");
        }
    }

    protected void btnExcelFormat3_Click(object sender, EventArgs e)
    {
        if (txtToDate.Text == string.Empty)
        {
            objCommon.DisplayMessage(this.pnlFeeTable, "Please Enter To Date.", this.Page);
            txtToDate.Focus();
            return;
        }
        if (ddlReceiptType.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this.pnlFeeTable, "Please select a Receipt type.", this.Page);
            ddlReceiptType.Focus();
            return;
        }

        DailyFeeCollectionRpt outReport = new DailyFeeCollectionRpt();
        DailyFeeCollectionController objFeeControll = new DailyFeeCollectionController();
        fromDSpan.Visible = false;
        txtFromDate.Text = string.Empty;
        MaskedEditValidator1.Enabled = false;
        DataSet ds = null;

        outReport.ReceiptTypes = ddlReceiptType.SelectedValue;
        outReport.BranchNo = (ddlBranch.SelectedIndex > 0) ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
        outReport.DegreeNo = (ddlDegree.SelectedIndex > 0) ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
        outReport.SemesterNo = (ddlSemester.SelectedIndex > 0) ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;

        if (ChkShowAllStudent.Checked == true)
        {
            outReport.ShowBalance = "0";
        }
        else
        {
            outReport.ShowBalance = "1";
        }
        outReport.FDate = txtFromDate.Text == string.Empty ? string.Empty : txtFromDate.Text.ToString();
        outReport.ToDate = txtToDate.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(txtToDate.Text);
        outReport.CounterNo = (ddlCounterNo.SelectedIndex > 0) ? Int32.Parse(ddlCounterNo.SelectedValue) : 0;
        outReport.PayTypeNo = (ddlPaymentType.SelectedIndex > 0) ? Convert.ToInt32(ddlPaymentType.SelectedValue) : 0;
        outReport.PaymentMode = (ddlPaymentMode.SelectedIndex > 0) ? ddlPaymentMode.SelectedValue : string.Empty;
        DataGrid Gr = new DataGrid();
        ds = objFeeControll.GetOutstandingReportExcelFormatII(outReport);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Gr.DataSource = ds;
                Gr.DataBind();
                string Attachment = "Attachment; FileName=OutstandingReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Gr.HeaderStyle.Font.Bold = true;
                Gr.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {

            }
        }
        else
        {

        }
    }
    protected void rdoShortDcrReport_CheckedChanged(object sender, EventArgs e)
    {
        divrectypesingle.Visible = true;
        divrectype.Visible = false;
        divdetailsumary.Visible = true;
        rdoSummeryReport.Enabled = false;
        fromDSpan.Visible = true;
        MaskedEditValidator1.Enabled = true;
        idDataFilter.Visible = true;
        pnlOSReportFormat1.Visible = false;
        btnExcelFormat2.Enabled = true;
        btnExcel.Enabled = true;

    }


    protected void btnExcelFormat4_Click(object sender, EventArgs e)
    {
        if (txtToDate.Text == string.Empty)
        {
            objCommon.DisplayMessage(this.pnlFeeTable, "Please Enter To Date.", this.Page);
            txtToDate.Focus();
            return;
        }

        if (ddlReceiptType.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this.pnlFeeTable, "Please select a Receipt type.", this.Page);
            ddlReceiptType.Focus();
            return;
        }

        DailyFeeCollectionRpt outReport = new DailyFeeCollectionRpt();
        DailyFeeCollectionController objFeeControll = new DailyFeeCollectionController();

        //fromDSpan.Visible = false;
        //txtFromDate.Text = string.Empty;
        //MaskedEditValidator1.Enabled = false;

        DataSet ds = null;

        outReport.ReceiptTypes = ddlReceiptType.SelectedValue;
        outReport.BranchNo = (ddlBranch.SelectedIndex > 0) ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
        outReport.DegreeNo = (ddlDegree.SelectedIndex > 0) ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
        outReport.SemesterNo = (ddlSemester.SelectedIndex > 0) ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;


        //if (ChkShowAllStudent.Checked == true)
        //{
        //    outReport.ShowBalance = "0";
        //}
        //else
        //{
        //    outReport.ShowBalance = "1";
        //}


        outReport.FromDate = txtFromDate.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(txtFromDate.Text);
        outReport.ToDate = txtToDate.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(txtToDate.Text);

        //outReport.CounterNo = (ddlCounterNo.SelectedIndex > 0) ? Int32.Parse(ddlCounterNo.SelectedValue) : 0;
        //outReport.PayTypeNo = (ddlPaymentType.SelectedIndex > 0) ? Convert.ToInt32(ddlPaymentType.SelectedValue) : 0;
        //outReport.PaymentMode = (ddlPaymentMode.SelectedIndex > 0) ? ddlPaymentMode.SelectedValue : string.Empty;

        DataGrid Gr = new DataGrid();

        ds = objFeeControll.GetNewOutstandingReportExcelFormatIII(outReport);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Gr.DataSource = ds;
                Gr.DataBind();
                string Attachment = "Attachment; FileName=OutstandingReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Gr.HeaderStyle.Font.Bold = true;
                Gr.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {

            }
        }
        else
        {
            objCommon.DisplayMessage(this.pnlFeeTable, "No Data Found", this.Page);
        }
    }

    protected void btnCollectionSummaryReport_Click(object sender, EventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
        }
    }
}