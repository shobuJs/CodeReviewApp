using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_FeesRefundReport : System.Web.UI.Page
{
    private Common objCommon = new Common();
    private UAIMS_Common objUaimsCommon = new UAIMS_Common();
    private FeeCollectionController objFeeCollectionController = new FeeCollectionController();
    string StudentEnroll = string.Empty;
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
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }

                    // Fill Dropdown lists
                    this.objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_NAME");
                    objCommon.SetLabelData("0");//for label
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header


                    //if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != string.Empty)
                    //{
                    //    int studentId = int.Parse(Request.QueryString["id"].ToString()); this.DisplayInformation(studentId);
                    //}

                    if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != string.Empty)
                    {
                        //int studentId = int.Parse(Request.QueryString["id"].ToString());//commeneted by pankaj nakhle 09 freb 2021
                        int studentIds = int.Parse(Request.QueryString["id"].ToString());
                        string studentId = (Request.QueryString["id"].ToString());
                        int COUNT = 0;
                        //txtSearch.Text = "SLR03184";
                        string USERFORNOTADMITED = objCommon.LookUp("ACD_STUDENT", "USERNO", "IDNO='" + Request.QueryString["id"].ToString() + "'");
                        ViewState["USERNO"] = USERFORNOTADMITED;
                        COUNT = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COUNT(*)", "IDNO='" + Request.QueryString["id"].ToString() + "'"));
                        if (COUNT > 0)
                        {
                            divstudinfo.Visible = true;
                            Panel2.Visible = true;
                            this.DisplayInformation(studentId);
                            this.PopulateStudentInfoSection_NotAdm(studentIds);
                        }
                        else
                        {
                            //this.PopulateStudentInfoSection_NotAdm(studentId);
                            this.PopulateStudentInfoSection_NotAdm(studentIds);
                        }

                    }

                    //int COUNT = 0;
                    //int CHK_FEESLOG = 0;
                    //int studentId = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DISTINCT ENROLLNO", "ENROLLNO=" + txtEnrollNo.Text + ""));//COMMENETED BY PANKAJ NAKHALE 17 DEC 2020
                    //CHK_FEESLOG = Convert.ToInt32(objCommon.LookUp("ACD_FEES_LOG", "COUNT(*)", "USERNO=" + txtEnrollNo.Text + ""));
                    //COUNT = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COUNT(*)", "ENROLLNO=" + txtEnrollNo.Text + ""));
                    //if (COUNT > 0)
                    //{
                    //    studentId = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DISTINCT ENROLLNO", "ENROLLNO=" + txtEnrollNo.Text + ""));//COMMENETED BY PANKAJ NAKHALE 17 DEC 
                    //    this.clear();
                    //    this.DisplayInformation(studentId);
                    //    divstudinfo.Visible = true;
                    //}
                    //else if (CHK_FEESLOG > 0)
                    //{
                    //    studentId = Convert.ToInt32(objCommon.LookUp("ACD_FEES_LOG", "DISTINCT USERNO", "USERNO=" + txtEnrollNo.Text + ""));//COMMENETED BY PANKAJ NAKHALE 17 DEC 
                    //    this.clear();
                    //    this.DisplayInformation(studentId);
                    //    lvFeesDetails.Visible = false;
                    //    this.PopulateStudentInfoSection_NotAdm();
                    //    divstudinfo.Visible = true;
                    //}
                }
                txtEnrollNo.Focus();
            }
            else
            {
                // Clear message div
                divMsg.InnerHtml = string.Empty;

                /// Check if postback is caused by btnSearch then call search method.
                if (Request.Params["__EVENTTARGET"] != null && Request.Params["__EVENTTARGET"].ToString() != string.Empty)
                {
                    if (Request.Params["__EVENTTARGET"].ToString() == "btnSearch")
                        this.ShowSearchResults(Request.Params["__EVENTARGUMENT"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeesRefundReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowSearchResults(string searchParams)
    {
        try
        {
            int College_id = 0;
            StudentSearch objSearch = new StudentSearch();
            string[] paramCollection = searchParams.Split(',');
            if (paramCollection.Length > 1)
            {
                for (int i = 0; i < paramCollection.Length; i++)
                {
                    string paramName = paramCollection[i].Substring(0, paramCollection[i].IndexOf('='));
                    string paramValue = paramCollection[i].Substring(paramCollection[i].IndexOf('=') + 1);

                    switch (paramName)
                    {

                        case "TANNO":
                            objSearch.Tanno = paramValue;
                            break;
                        case "enrollno":
                            objSearch.Regno = paramValue;
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
                        case "Name":
                            objSearch.StudentName = paramValue;
                            break;
                        case "REGNO":
                            objSearch.EnrollmentNo = paramValue;
                            break;
                        case "ENROLLNO":
                            objSearch.Srno = paramValue;
                            break;
                        case "emailid":
                            objSearch.IdNo = paramValue;
                            break;
                        case "NICNO":
                            objSearch.Tanno = (paramValue);
                            break;
                        case "College_id":
                            College_id = int.Parse(paramValue);
                            break;
                        default:
                            break;
                    }
                }
            }
            FeeCollectionController feeController = new FeeCollectionController();
            DataSet ds = feeController.GetStudentsLEDGER(objSearch, College_id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#myModal4').modal()", false);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#myModal4').modal()", true);
            }
            else
            {
                lblNoRecords.Text = "Total Records : 0";
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#myModal4').modal()", false);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#myModal4').modal()", true);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeesRefundReport.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //private void ShowSearchResults(string searchParams)
    //{
    //    try
    //    {
    //        int College_id = 0;
    //        StudentSearch objSearch = new StudentSearch();
    //        string[] paramCollection = searchParams.Split(',');
    //        if (paramCollection.Length > 2)
    //        {
    //            for (int i = 0; i < paramCollection.Length; i++)
    //            {
    //                string paramName = paramCollection[i].Substring(0, paramCollection[i].IndexOf('='));
    //                string paramValue = paramCollection[i].Substring(paramCollection[i].IndexOf('=') + 1);

    //                switch (paramName)
    //                {
    //                    case "Name":
    //                        objSearch.StudentName = paramValue;
    //                        break;

    //                    case "SRNO":
    //                        objSearch.Srno = paramValue;
    //                        break;

    //                    case "DegreeNo":
    //                        objSearch.DegreeNo = int.Parse(paramValue);
    //                        break;

    //                    case "BranchNo":
    //                        objSearch.BranchNo = int.Parse(paramValue);
    //                        break;

    //                    case "YearNo":
    //                        objSearch.YearNo = int.Parse(paramValue);
    //                        break;

    //                    case "SemNo":
    //                        objSearch.SemesterNo = int.Parse(paramValue);
    //                        break;

    //                    case "College_id":
    //                        College_id = int.Parse(paramValue);
    //                        break;
    //                    default:
    //                        break;
    //                }
    //            }
    //        }
    //        FeeCollectionController feeController = new FeeCollectionController();      
    //        DataSet ds = feeController.GetStudentsLEDGER(objSearch, College_id);
    //        lvStudent.DataSource = ds;
    //        lvStudent.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Academic_FeesRefundReport.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FeesDetails.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeesDetails.aspx");
        }
    }

    //private void DisplayInformation(int studentId)
    private void DisplayInformation(string studentId)
    {
        try
        {
            DataSet ds = objFeeCollectionController.GET_FEESDETAILS_IDNOWISE(studentId, Convert.ToInt32(Session["usertype"]));
            DataSet ds1 = objFeeCollectionController.GET_FEESDETAILS_IDNOWISE_misc(studentId, Convert.ToInt32(Session["usertype"]));
            decimal sum = 0;
            decimal Totalsum = 0;
            decimal Balancesum = 0;
            decimal Demandsum = 0;
            ViewState["IDNO"] = 0;
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                Panel1.Visible = true;
                lvFeesDetails.DataSource = ds;
                lvFeesDetails.DataBind();
                ViewState["IDNO"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
                // div5.Visible = true;
                foreach (ListViewItem item in lvFeesDetails.Items)
                {
                    Label lblpaid = item.FindControl("lblTotPaid") as Label;
                    Label lblRptCode = item.FindControl("lblRecieptCode") as Label;
                    Label lblBalAmt = item.FindControl("lblBalAmt") as Label;
                    Label lblDemand = item.FindControl("lblDemand") as Label;
                    Label lblpaystatus = item.FindControl("lblpaystatus") as Label;
                    ImageButton ImgPrintReceipt = item.FindControl("ImgPrintReceipt") as ImageButton;
                    ImageButton btnDownloadFile = item.FindControl("btnDownloadFile") as ImageButton;
                    Button ReportButton = item.FindControl("ReportButton") as Button;

                    if (lblRptCode.Text == "Refund fee")
                    {
                        //sum -= Convert.ToDecimal(lblpaid.Text);
                        if (lblpaid.Text != "" && lblRptCode.Text == "Semester Fees" || lblpaid.Text != string.Empty && lblRptCode.Text == "Semester Fees")
                        {
                            Totalsum -= Convert.ToDecimal(lblpaid.Text);

                        }
                        if (lblDemand.Text == "" || lblDemand.Text == string.Empty)
                        {

                        }
                        else
                        {
                            if (lblDemand.Text != "" && lblRptCode.Text == "Semester Fees" || lblDemand.Text != string.Empty && lblRptCode.Text == "Semester Fees")
                            {
                                //sum -= Convert.ToDecimal(lblpaid.Text);
                                Demandsum -= Convert.ToDecimal(lblDemand.Text);
                            }
                        }


                    }
                    else
                    {
                        //sum += Convert.ToDecimal(lblpaid.Text);
                        if (lblpaid.Text != "" && lblRptCode.Text == "Semester Fees" || lblpaid.Text != string.Empty && lblRptCode.Text == "Semester Fees")
                        {
                            Totalsum += Convert.ToDecimal(lblpaid.Text);

                        }

                        if (lblDemand.Text == "" || lblDemand.Text == string.Empty)
                        {

                        }
                        else
                        {
                            if (lblDemand.Text != "" && lblRptCode.Text == "Semester Fees" || lblDemand.Text != string.Empty && lblRptCode.Text == "Semester Fees")
                            {
                                Demandsum += Convert.ToDecimal(lblDemand.Text);
                            }
                        }
                    }


                    if (lblRptCode.Text == "Refund fee")
                    {
                        sum -= Convert.ToDecimal(lblpaid.Text);
                    }
                    else
                    {
                        sum += Convert.ToDecimal(lblpaid.Text);
                    }

                    if (lblpaystatus.Text == "Completed")
                    {
                        ImgPrintReceipt.Visible = true;
                        btnDownloadFile.Visible = true;
                    }
                    else
                    {
                        ImgPrintReceipt.Visible = false;
                        btnDownloadFile.Visible = false;
                    }
                }
                lbltotalDemand.Text = (Demandsum.ToString());
                lblTotalPaid.Text = (Totalsum.ToString());
                decimal TotalBalanceAmount = 0.0m;
                TotalBalanceAmount = Convert.ToDecimal(lbltotalDemand.Text) - Convert.ToDecimal(lblTotalPaid.Text);
                lblTotalBalance.Text = TotalBalanceAmount.ToString();
                //  decimal fees_log_fee = 0;
                //  fees_log_fee = Convert.ToDecimal(objCommon.LookUp("ACD_FEES_LOG", "sum(AMOUNT)", "USERNO=" + studentId + ""));

                //lblPaidAmt.Text = (fees_log_fee + sum).ToString();
                lblPaidAmt.Text = Convert.ToString(sum);//.ToString();
                if (Convert.ToDouble(lblPaidAmt.Text) > 0)
                {
                  //  divtotamt.Visible = true;

                    divtotamt.Visible = false;  // Changes in condition as per USA-830
                }
                DataRow dr = ds.Tables[0].Rows[0];
                lvFeesDetails.Visible = true;//pankaj17dec2020

            }
            else
            {
                Panel1.Visible = false;
                lvFeesDetails.DataSource = null;
                lvFeesDetails.DataBind();
            }
            if (ds1.Tables[0].Rows.Count > 0 && ds1 != null)
            {
                lvMisc.DataSource = ds1;
                lvMisc.DataBind();
            }
            else
            {
                lvMisc.DataSource = null;
                lvMisc.DataBind();
            }
           
            this.PopulateStudentInfoSection();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeesRefundReport.DisplayInformation() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void clear()
    {
        lblPaidAmt.Text = lblStudName.Text = lblRegNo.Text = lblBatch.Text = lblDateOfAdm.Text = lblDegree.Text = string.Empty;
        lblMobileNo.Text = lblPaymentType.Text = lblSemester.Text = lblSex.Text = lblStudName.Text = lblYear.Text = string.Empty;
        btnReport.Visible = false;
    }

    private void PopulateStudentInfoSection()
    {
        try
        {
            decimal sum = 0;
            DataSet ds = null;
            if (Convert.ToString(Session["stuinfoidnoledger"]) != string.Empty)
            {
                //ds = objFeeCollectionController.GET_STUDENT_ENROLLNO(Convert.ToInt32(Session["stuinfoidnoledger"].ToString().Trim()), 1);//09feb2021
                ds = objFeeCollectionController.GET_FEESDETAILS_IDNOWISE(Convert.ToString(Session["stuinfoidnoledger"].ToString().Trim()), 1);
            }
            else
            {
                //ds = objFeeCollectionController.GET_STUDENT_ENROLLNO(Convert.ToInt32(txtEnrollNo.Text.Trim()), 1);//09feb2021
                ds = objFeeCollectionController.GET_STUDENT_ENROLLNO(Convert.ToString(txtEnrollNo.Text.Trim()), 1);

            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                // Bind data with labels
                ViewState["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                lblCollege.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                Session["idno"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
                ViewState["idno"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
                lblStudName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                lblSex.Text = ds.Tables[0].Rows[0]["GENDER"].ToString();
                lblenrolled.Text = ds.Tables[0].Rows[0]["ENROLLED"].ToString();
                lblRegNo.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                lblenroll.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
                lblDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString() + '-' + ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                //lblBranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                lblYear.Text = ds.Tables[0].Rows[0]["YEAR"].ToString();
                lblSemester.Text = ds.Tables[0].Rows[0]["CURR_SEMESTERNO"].ToString();
                lblBatch.Text = ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
                lblMobileNo.Text = ds.Tables[0].Rows[0]["MOBILENO"].ToString();
                divstudinfo.Visible = true;
                btnReport.Visible = false;
            }
            DataSet ds1 = null;
            if (Convert.ToString(Session["stuinfoidnoledger"]) != string.Empty)
            {
                ////ds1 = objFeeCollectionController.GET_STUDENT_ENROLLNO(Convert.ToInt32(Session["stuinfoidnoledger"].ToString().Trim()), 2);//09feb2021
                ds1 = objFeeCollectionController.GET_FEESDETAILS_IDNOWISE(Convert.ToString(Session["stuinfoidnoledger"].ToString().Trim()), 2);
                Session["REPORT"] = Convert.ToString(Session["stuinfoidnoledger"]);
                Session["stuinfoidnoledger"] = null;
            }
            else
            {
                //ds1 = objFeeCollectionController.GET_STUDENT_ENROLLNO(Convert.ToInt32(txtEnrollNo.Text.Trim()), 2);//09feb2021
                ds1 = objFeeCollectionController.GET_STUDENT_ENROLLNO(Convert.ToString(ViewState["USERNO"]), 2);
            }
            if (ds1.Tables[0].Rows.Count > 0)
            {
                Panel2.Visible = true;
                lvApplicationFee.DataSource = ds1;
                lvApplicationFee.DataBind();

                foreach (ListViewItem item in lvApplicationFee.Items)
                {
                    Label lblAmt = item.FindControl("lblAmt") as Label;
                    // Label lblRptCode = item.FindControl("lblRecieptCode") as Label;                   
                    sum += Convert.ToDecimal(lblAmt.Text);
                }
                if (lblPaidAmt.Text != "")
                {

                    if (Convert.ToDouble(lblPaidAmt.Text) > 0)
                    {
                        lblPaidAmt.Text = Convert.ToString(sum + Convert.ToDecimal(lblPaidAmt.Text));

                    }
                    else
                    {
                        lblPaidAmt.Text = Convert.ToString(sum);
                    }
                }
                else
                {
                    lblPaidAmt.Text = Convert.ToString(sum);
                }
            }
            else
            {
                Panel2.Visible = false;
                divtotamt.Visible = false;
                lvApplicationFee.DataSource = null;
                lvApplicationFee.DataBind();
                Session["stuinfoidnoledger"] = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Refund.PopulateStudentInfoSection() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void PopulateStudentInfoSection_NotAdm(int studentId)
    {
        try
        {
            decimal sum = 0;
            DataSet ds = null;
            if (Convert.ToString(Session["stuinfoidnoledger"]) != string.Empty)
            {
                ds = objFeeCollectionController.GET_STUDENT_ENROLLNO_Not_Admitted(Convert.ToInt32(ViewState["USERNO"].ToString().Trim()), 1);
            }
            else
            {
                ds = objFeeCollectionController.GET_STUDENT_ENROLLNO_Not_Admitted(Convert.ToInt32(ViewState["USERNO"]), 1);

            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                // Bind data with labels
                ViewState["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                lblCollege.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                Session["idno"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
                ViewState["idno"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
                lblStudName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                lblSex.Text = ds.Tables[0].Rows[0]["GENDER"].ToString();
                lblenrolled.Text = ds.Tables[0].Rows[0]["ENROLLED"].ToString();
                lblRegNo.Text = ds.Tables[0].Rows[0]["regno"].ToString();
                lblenroll.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
                lblDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString() + '-' + ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                //lblBranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                lblYear.Text = ds.Tables[0].Rows[0]["YEAR"].ToString();
                lblSemester.Text = ds.Tables[0].Rows[0]["CURR_SEMESTERNO"].ToString();
                lblBatch.Text = ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
                //lblFatherName.Text = ds.Tables[0].Rows[0]["FATHERNAME"].ToString();
                lblMobileNo.Text = ds.Tables[0].Rows[0]["MOBILENO"].ToString();
                divstudinfo.Visible = true;
                btnReport.Visible = false;
            }
            DataSet ds1 = null;
            if (Convert.ToString(Session["stuinfoidnoledger"]) != string.Empty)
            {
                ds1 = objFeeCollectionController.GET_STUDENT_ENROLLNO_Not_Admitted(Convert.ToInt32(ViewState["USERNO"].ToString().Trim()), 2);
                Session["REPORT"] = Convert.ToString(Session["stuinfoidnoledger"]);
                // Session["stuinfoidnoledger"] = null;
            }
            else
            {
                ds1 = objFeeCollectionController.GET_STUDENT_ENROLLNO_Not_Admitted(Convert.ToInt32(ViewState["USERNO"]), 2);
            }
            if (ds1.Tables[0].Rows.Count > 0)
            {
                Panel2.Visible = true;
                lvApplicationFee.DataSource = ds1;
                lvApplicationFee.DataBind();

                //decimal sum = 0;
                //if (Convert.ToString(Session["stuinfoidnoledger"]) != string.Empty)
                //{
                //    sum = Convert.ToDecimal(objCommon.LookUp("ACD_FEES_LOG", "sum(AMOUNT)", "USERNO=" + Convert.ToInt32(Session["stuinfoidnoledger"].ToString().Trim()) + ""));
                //    Session["stuinfoidnoledger"] = null;
                //}
                //else
                //{
                //    sum = Convert.ToDecimal(objCommon.LookUp("ACD_FEES_LOG", "sum(AMOUNT)", "USERNO=" + txtEnrollNo.Text + ""));
                //}
                //  lblPaidAmt.Text = ds.Tables[0].Rows[0]["AMT"].ToString();  //sum.ToString();
                //foreach (ListViewItem item in lvApplicationFee.Items)
                //{
                //    Label lblAmt = item.FindControl("lblAmt") as Label;
                //    // Label lblRptCode = item.FindControl("lblRecieptCode") as Label;                   
                //    sum += Convert.ToDecimal(lblAmt.Text);
                //}
                //lblPaidAmt.Text = Convert.ToString(sum);
                //if (Convert.ToDouble(lblPaidAmt.Text) > 0)
                //{
                //    lblPaidAmt.Text = Convert.ToString(sum + Convert.ToDecimal(lblPaidAmt.Text));
                //    divtotamt.Visible = true;
                //}
                //else
                //{
                //    lblPaidAmt.Text = Convert.ToString(sum);
                //}
            }
            else
            {
                divtotamt.Visible = false;
                Panel2.Visible = false;
                lvApplicationFee.DataSource = null;
                lvApplicationFee.DataBind();
                Session["stuinfoidnoledger"] = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Refund.PopulateStudentInfoSection() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
          
                if (hdnClientId.Value != "0")
                {
                    
                    txtEnrollNo.Text = objCommon.LookUp("ACD_STUDENT", "(CASE WHEN ENROLLNO IS NULL THEN REGNO ELSE  ENROLLNO END) STUDENT_ID", "IDNO=" + hdnClientId.Value);
                    Session["REPORT"] = null;
                    int studentId = 0;
                    hdnClientId.Value = "0";
                    string student_enrollno = "";
                    int COUNT = 0;
                    int CHK_FEESLOG = 0;
                    int chkDcr_COUNT = 0;
                    int fEESUSERNO = 0;
                    int COUNTUSERNAME = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "COUNT(*)", "USERNAME='" + txtEnrollNo.Text + "'"));
                    if (COUNTUSERNAME > 0)
                    {
                        fEESUSERNO = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + txtEnrollNo.Text + "'"));
                    }
                    string USERNO = objCommon.LookUp("ACD_STUDENT", "USERNO", "REGNO='" + txtEnrollNo.Text + "'" + "OR ENROLLNO='" + txtEnrollNo.Text + "'");
                    if (fEESUSERNO > 0)
                    {
                        CHK_FEESLOG = Convert.ToInt32(objCommon.LookUp("ACD_FEES_LOG", "COUNT(*)", "USERNO=" + fEESUSERNO + ""));
                        ViewState["USERNO"] = fEESUSERNO;
                    }
                    COUNT = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COUNT(*)", "REGNO='" + txtEnrollNo.Text + "'" + "OR ENROLLNO='" + txtEnrollNo.Text + "'"));

                    student_enrollno = objCommon.LookUp("ACD_STUDENT", "DISTINCT IDNO", "REGNO='" + txtEnrollNo.Text + "'" + "OR ENROLLNO='" + txtEnrollNo.Text + "'");//COMMENETED BY PANKAJ NAKHALE 17 DEC 
                    chkDcr_COUNT = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(*)", "IDNO='" + student_enrollno + "'"));
                    if (COUNT > 0)
                    {
                        ViewState["USERNO"] = USERNO;
                        //studentId = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DISTINCT ENROLLNO", "ENROLLNO='" + txtEnrollNo.Text + "'"));//COMMENETED BY PANKAJ NAKHALE 09feb2021
                        student_enrollno = objCommon.LookUp("ACD_STUDENT", "DISTINCT IDNO", "REGNO='" + txtEnrollNo.Text + "'" + "OR ENROLLNO='" + txtEnrollNo.Text + "'");//COMMENETED BY PANKAJ NAKHALE 17 DEC 
                        this.clear();

                        //this.DisplayInformation(studentId);//COMMENETED BY PANKAJ NAKHALE 17 DEC 
                        this.DisplayInformation(student_enrollno);
                        this.PopulateStudentInfoSection_NotAdm(studentId);
                        divstudinfo.Visible = true;
                    }
                    else if (CHK_FEESLOG > 0)
                    {
                        studentId = Convert.ToInt32(objCommon.LookUp("ACD_FEES_LOG", "DISTINCT USERNO", "USERNO=" + fEESUSERNO + ""));//COMMENETED BY PANKAJ NAKHALE 17 DEC 
                        this.clear();
                        // this.DisplayInformation(studentId);
                        lvFeesDetails.Visible = false;
                        this.PopulateStudentInfoSection_NotAdm(studentId);
                        divstudinfo.Visible = true;
                        btnReport.Visible = false;
                    }
                    else
                    {
                        objCommon.DisplayMessage(updEdit, "No student found having Admission No.: ", this.Page);
                        divstudinfo.Visible = false;
                    }
                    //int studentId = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DISTINCT ENROLLNO", "ENROLLNO=" + txtEnrollNo.Text + ""));
                    //if (studentId > 0)
                    //{
                    //    this.clear();
                    //    this.DisplayInformation(studentId);
                    //    divstudinfo.Visible = true;
                    //}
                    //else
                    //    objCommon.DisplayMessage(updEdit, "No student found having Admission No.: ", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(updEdit, "Please Search Student !!!", this.Page);
                    //objCommon.DisplayMessage(updEdit, "No student found having Admission No.: ", this.Page);
                    //ShowMessage("No student found having Admission No.: " + txtEnrollNo.Text.Trim());
                    //   clear();
                    divstudinfo.Visible = false;
                }
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeesRefundReport.btnShow_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private string GetReportParameters(int dcrNo, int studentNo, string copyNo)
    {
        string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;
    }


    private void ShowReport_All(string rptName, int Idno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            //string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
            url += "pagetitle=Fees_Details";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=@P_IDNO=" + Convert.ToInt32(ViewState["idno"]) + "*MainRpt,@P_USERTYPE=" + Convert.ToInt32(Session["usertype"]) + "*MainRpt,@UserName=" + Session["username"].ToString() + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + rptName + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeesRefundReport.ShowReport_All() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        hdnClientId.Value = "0";
        // Reload/refresh complete page.
        if (Request.Url.ToString().IndexOf("&id=") > 0)
        {
            Response.Redirect(Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("&id=")));
        }
        else
        {
            Response.Redirect(Request.Url.ToString());
            Session["stuinfoidnoledger"] = null;
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            this.ShowReport_All("rpt_GetFeesDetails_IDNOWise.rpt", Convert.ToInt32(ViewState["idno"]));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeesRefundReport.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            Session["REPORT"] = null;
            string url = string.Empty;
            if (Request.Url.ToString().IndexOf("&id=") > 0)
                url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
            else
                url = Request.Url.ToString();

            //Session["stuinfoidnoledger"] = Convert.ToInt32(lnk.CommandArgument);
            Response.Redirect(url + "&id=" + lnk.CommandArgument);
            //Session["USERNO"] = Convert.ToInt32(lnk.CommandName);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Refund.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ImgPrintReceipt_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnPrint = sender as ImageButton;
        int DCR_NO = Convert.ToInt32(btnPrint.CommandArgument.ToString());
        int idno = Convert.ToInt32(btnPrint.CommandName.ToString());

        //this.ShowReport("FeeCollectionReceipt.rpt", DCR_NO, Convert.ToInt32(idno), "1");
        ShowReport("Fees", "FeesReceiptApplicationForm.rpt", idno, DCR_NO);
    }
    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
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

    protected void lvpetycash_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Label lblid = (Label)e.Item.FindControl("lblenroll");
            ListView lvpetycashdetails = (ListView)e.Item.FindControl("lvpetycashdetails");

            DataSet ds1 = objFeeCollectionController.GET_FEES_PETTYCASHCOLLECTION_IDNOWISE(Convert.ToInt32(lblid.ToolTip.ToString()), Convert.ToInt32(Session["usertype"]));
            if (ds1.Tables.Count > 0 && ds1 != null)
            {
                lvpetycashdetails.DataSource = ds1;
                lvpetycashdetails.DataBind();
            }
        }
    }


    protected void ImgPrintReceipt1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton img = (ImageButton)sender;
        string dcrno = img.CommandArgument.ToString();
        ShowReport1("Miscellaneous Reprint Report", "rptMiscReport.rpt", dcrno);
    }

    private void ShowReport1(string reportTitle, string rptFileName, string dcrno)
    {
        try
        {
           // string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";

            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
            url += "pagetitle=" + reportTitle;
            url += "&path=~,REPORTS,ACADEMIC," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MISCDCRNO=" + dcrno + ",@P_COPY=4,@P_USERNAME=" + Session["userfullname"].ToString() + "";

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updpnlMain, this.updpnlMain.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID=" + ddlCollege.SelectedValue, "");
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH B INNER JOIN ACD_BRANCH D ON B.BRANCHNO = D.BRANCHNO", "DISTINCT D.BRANCHNO", "D.SHORTNAME", "D.SHORTNAME <> ''" + " AND DEGREENO=" + ddlDegree.SelectedValue, "");
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.objCommon.FillDropDownList(ddlYear, "ACD_STUDENT S INNER JOIN ACD_YEAR Y ON S.YEAR = Y.YEAR", "DISTINCT S.YEAR", "YEARNAME", "COLLEGE_ID=" + ddlCollege.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue, "");
        this.objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SA ON S.SEMESTERNO = SA.SEMESTERNO", "DISTINCT S.SEMESTERNO", "SA.SEMESTERNAME", " DEGREENO=" + ddlDegree.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue, "");
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
    }
    protected void ImgPrintReceiptApp_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnPrint = sender as ImageButton;
        int userno = Convert.ToInt32(btnPrint.CommandArgument.ToString());
        //this.ShowReportApp("FeeReceiptApplication.rpt", Convert.ToString(userno), "");
        //this.ShowReport("", "FeesReceiptApplicationFormApplication.rpt", Convert.ToString(userno), 0);
        ShowReport("Application Fees", "FeesReceiptApplicationFormApplication.rpt", userno, 0);
    }
    private void ShowReportApp(string rptName, string userno, string college_code)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
            //string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
            //string url = "https://erptest.sliit.lk/Reports/CommonReport.aspx?";

            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];        
            url += "pagetitle=Fees_Details";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=@P_USERNO=" + Convert.ToString(userno) + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + rptName + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowConfirmationReport(string reportTitle, string rptFileName, int idno)
    {
        try
        {
            string colgname = "USA";

            string exporttype = "pdf";
           // string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];


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
    //added by aashna 19-10-2022 download file from block storage
    //protected void BtnDownload_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        string filename = "";
    //        ImageButton lnkbtn = sender as ImageButton;
    //        filename = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
    //        if (filename == "")
    //        {
    //            objCommon.DisplayMessage(updEdit, "Deposite Slip Not Found.: ", this.Page);
    //            return;
    //        }
    //        else
    //        {
    //            string accountname = System.Configuration.ConfigurationManager.AppSettings["Blob_AccountName"].ToString();
    //            string accesskey = System.Configuration.ConfigurationManager.AppSettings["Blob_AccessKey"].ToString();
    //            string containerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
    //            StorageCredentials creden = new StorageCredentials(accountname, accesskey);
    //            CloudStorageAccount acc = new CloudStorageAccount(creden, useHttps: true);
    //            CloudBlobClient client = acc.CreateCloudBlobClient();
    //            CloudBlobContainer container = client.GetContainerReference(containerName);
    //            CloudBlob blob = container.GetBlobReference(filename);
    //            MemoryStream ms = new MemoryStream();
    //            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
    //            blob.DownloadToStream(ms);
    //            Response.ContentType = blob.Properties.ContentType;
    //            Response.AddHeader("Content-Disposition", "Attachment; filename=" + filename.ToString());
    //            Response.AddHeader("Content-Length", blob.Properties.Length.ToString());
    //            Response.BinaryWrite(ms.ToArray());
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}
    protected void BtnDownload_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string filename = "";
            filename = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            foreach (ListViewDataItem dataitem in lvApplicationFee.Items)
            {
                ImageButton lnkreg = (ImageButton)(sender);
                int userno = Convert.ToInt32(lnkreg.CommandArgument);
                if (Convert.ToInt32(lnkreg.CommandArgument) == userno)
                {
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
                    string img = filename;
                    var ImageName = img;
                    if (img.ToString() != string.Empty || img.ToString() != "")
                    {
                        string extension = Path.GetExtension(img.ToString());
                        DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
                        if (extension == ".pdf")
                        {
                            ImageViewer.Visible = false;
                            ltEmbed.Visible = true;
                            imageViewerContainer.Visible = false;
                            var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                            string filePath = directoryPath + "\\" + ImageName;
                            if ((System.IO.File.Exists(filePath)))
                            {
                                System.IO.File.Delete(filePath);
                            }
                            Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                            hdfImagePath.Value = null;
                            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"765px\" height=\"400px\">";
                            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                            embed += "</object>";
                            ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
                            break;
                        }
                        else
                        {
                            hdfImagePath.Value = null;
                            ImageViewer.Visible = true;
                            ltEmbed.Visible = false;
                            imageViewerContainer.Visible = true;
                            var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                            string filePath = directoryPath + "\\" + ImageName;
                            if ((System.IO.File.Exists(filePath)))
                            {
                                System.IO.File.Delete(filePath);
                            }
                            Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                            ImageViewer.ImageUrl = ResolveUrl("~/DownloadImg/" + ImageName);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
                            break;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Sorry, File not found !!!", this.Page);
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnDownloadFile_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string filename = "";
            filename = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            foreach (ListViewDataItem dataitem in lvFeesDetails.Items)
            {
                ImageButton lnkreg = (ImageButton)(sender);
                int userno = Convert.ToInt32(lnkreg.CommandArgument);
                if (Convert.ToInt32(lnkreg.CommandArgument) == userno)
                {
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
                    string img = filename;
                    var ImageName = img;
                    if (img.ToString() != string.Empty || img.ToString() != "")
                    {
                        string extension = Path.GetExtension(img.ToString());
                        DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
                        if (extension == ".pdf")
                        {
                            ImageViewer.Visible = false;
                            ltEmbed.Visible = true;
                            imageViewerContainer.Visible = false;
                            var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                            string filePath = directoryPath + "\\" + ImageName;
                            if ((System.IO.File.Exists(filePath)))
                            {
                                System.IO.File.Delete(filePath);
                            }
                            Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                            hdfImagePath.Value = null;
                            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"765px\" height=\"400px\">";
                            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                            embed += "</object>";
                            ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
                            break;
                        }
                        else
                        {
                            hdfImagePath.Value = null;
                            ImageViewer.Visible = true;
                            ltEmbed.Visible = false;
                            imageViewerContainer.Visible = true;
                            var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                            string filePath = directoryPath + "\\" + ImageName;
                            if ((System.IO.File.Exists(filePath)))
                            {
                                System.IO.File.Delete(filePath);
                            }
                            Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                            ImageViewer.ImageUrl = ResolveUrl("~/DownloadImg/" + ImageName);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
                            break;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Sorry, File not found !!!", this.Page);
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
   
    protected void ImgPrintReceiptisc_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnPrint = sender as ImageButton;
        int DCR_NO = Convert.ToInt32(btnPrint.CommandArgument.ToString());
        int idno = Convert.ToInt32(btnPrint.CommandName.ToString());
        //ShowReport("Miscallenious Fees", "rptMiscReport.rpt", DCR_NO);
        ShowReport("Miscallenious Fees", "FeesReceiptApplicationFormMisc.rpt", idno,DCR_NO);

    }
    private void ShowReport(string reportTitle, string rptFileName, int idno,int DCR_NO)
    {

        try
        {
            //string url = "https://erptest.sliit.lk/Reports/CommonReport.aspx?";
            //// string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
           // string url = "http://localhost:59566/PresentationLayer/Reports/CommonReport.aspx?";
            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_IDNO=" + idno + ",@P_DCR_NO=" + DCR_NO + "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ReportButton_Click(object sender, EventArgs e)
    {
        Button btnPrint = sender as Button;
        int Semesterno = Convert.ToInt32(btnPrint.CommandArgument.ToString().Split('-')[0]);
        int batch = Convert.ToInt32(btnPrint.CommandArgument.ToString().Split('-')[1]);
        int paytype = Convert.ToInt32(btnPrint.CommandArgument.ToString().Split('-')[2]);
        string receipt_code = Convert.ToString(btnPrint.CommandArgument.ToString().Split('-')[3]);
        int Sessionno = Convert.ToInt32(btnPrint.ToolTip.ToString());
        int idno = Convert.ToInt32(btnPrint.CommandName.ToString());

        string enlistCount = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", @"IDNO=" + idno + " AND SESSIONNO=" + Sessionno + " AND SEMESTERNO=" + Semesterno + " AND ISNULL(CANCEL,0)=0 AND ISNULL(ACCEPTED,0)=1 AND ISNULL(REGISTERED,0)=1 AND ISNULL(EXAM_REGISTERED,0)=1");
        if (Convert.ToInt32(enlistCount) > 0)
        {
            this.ShowReportStatementAccount("Statement Of Account", "StatementOfAccount.rpt", Sessionno, idno, Semesterno, batch, paytype, receipt_code);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Student is not yet enlisted! Please retry after enlistment of student.", this.Page);
            return;
        }
    }
    private void ShowReportStatementAccount(string reportTitle, string rptFileName, int sessionno, int idno, int semesterno, int admbatch, int paytype, string receipt_type)
    {
        try
        {
           // string url = "http://localhost:59566/PresentationLayer/Reports/commonreport.aspx?";
            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@P_SEMESTERNO=" + semesterno + ",@P_RECEIPT_CODE=" + receipt_type + ",@P_PAYTYPENO=" + paytype + ",@P_ADMBATCH=" + admbatch+ ",@P_UANO="+ Convert.ToInt32(Session["userno"].ToString()); ;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnStudentLedgerReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["IDNO"].ToString() == "0")
            {
                objCommon.DisplayMessage(this.Page, "Payment transactions / Demand not found for student !!!", this.Page);
            }
            else
            {
                this.ShowStudentLedger("StudentLedgerReport.rpt", Convert.ToInt32(ViewState["IDNO"].ToString()));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeesRefundReport.btnStudentLedgerReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowStudentLedger(string rptName, int Idno)
    {
        try
        {
      
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Student Ledger Report";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_IDNO=" + Idno;
            //divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            //divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updFeesDetails, this.updFeesDetails.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeesRefundReport.ShowStudentLedger() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


}
