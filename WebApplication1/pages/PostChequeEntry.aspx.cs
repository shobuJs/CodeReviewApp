//======================================================================================
// PROJECT NAME  : MNR
// MODULE NAME   : ACADEMIC
// PAGE NAME     : POST CHEQUE ENTRY
// CREATION DATE : 05/05/2021
// CREATED BY    : 
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_PostCheckEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();

    #region Page Events
    //USED FOR INITIALSING THE MASTER PAGE
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    //USED FOR BYDEFAULT LOADING THE default PAGE
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
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    YearBind();
                    MonthBind();
                }
            }
            divMsg.InnerHtml = string.Empty;
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_PostChequeEntry.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page. 
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
    //get url
    #region Search Student
    //Showing student details for searched TAN/PAN  no.
    protected void btnShowInfo_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtEnrollNo.Text.Trim() != string.Empty)
            {
                string idno = objCommon.LookUp("ACD_STUDENT", "ISNULL(IDNO,0)", "ENROLLNO='" + txtEnrollNo.Text.Trim() + "' OR REGNO='" + txtEnrollNo.Text.Trim() + "' OR TANNO='" + txtEnrollNo.Text.Trim() + "'");
                ViewState["idno"] = idno;
                if (idno == "")
                {
                    objCommon.DisplayMessage("Please enter TAN/PAN", this.Page);
                    txtEnrollNo.Focus();
                    return;
                }
                if (Convert.ToInt32(idno) > 0)
                {
                    this.DisplayInformation(Convert.ToInt32(ViewState["idno"].ToString()));
                    divStudInfo.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage("Please enter TAN/PAN", this.Page);
                    txtEnrollNo.Focus();
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage("Please enter TAN/PAN", this.Page);
                txtEnrollNo.Focus();
                return;
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_PostChequeEntry.btnShowInfo_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion

    private void YearBind()
    {
        //ddlYear.Items.Add("Please Select");
        //ddlYear.SelectedItem.Value = "0";
        for (int jLoop = 2018; jLoop <= DateTime.Now.Year + 4; jLoop++)   //need to add +4 in order to increase Year
        {
            ddlYear.Items.Add(jLoop.ToString());
        }
    }


    void MonthBind()
    {
        for (int month = 1; month <= 12; month++)
        {
            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            ddlMonth.Items.Add(new ListItem(monthName, month.ToString().PadLeft(2, '0')));
        }
    }

    private void DisplayInformation(int studentId)
    {
        try
        {
            #region Display Student Information
            /// Display student's personal and academic data in 
            /// student information section
            DataSet ds = feeController.GetStudentInfoById(studentId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                // show student information
                this.PopulateStudentInfoSection(dr);
                divStudInfo.Style["display"] = "block";
                BindListView();
            }
            else
            {
                objCommon.DisplayMessage("Student record not found.", this.Page);
                return;
            }
            #endregion
        }
        catch (Exception ex)
        {

        }
    }

    //bind student info data to labels.
    private void PopulateStudentInfoSection(DataRow dr)
    {
        try
        {
            #region Bind data to labels
            lblStudName.Text = dr["STUDNAME"].ToString();
            lblEnrollno.Text = dr["ENROLLNO"].ToString();
            lblRegNo.Text = dr["TANNO"].ToString();
            lblDateOfAdm.Text = ((dr["ADMDATE"].ToString().Trim() != string.Empty) ? Convert.ToDateTime(dr["ADMDATE"].ToString()).ToShortDateString() : dr["ADMDATE"].ToString());
            lblFather.Text = dr["FATHERNAME"].ToString();
            lblStudMobile.Text = dr["STUDENTMOBILE"].ToString();
            lblEmailid.Text = dr["EMAILID"].ToString();
            lblDegree.Text = dr["DEGREENAME"].ToString();
            lblBranch.Text = dr["BRANCH_NAME"].ToString();
            //lblYear.Text = dr["YEARNAME"].ToString();
            //lblSemester.Text = dr["SEMESTERNAME"].ToString();
            //lblSex.Text = dr["SEX"].ToString();
            //lblPaymentType.Text = dr["PAYTYPENAME"].ToString();
            lblBatch.Text = dr["BATCHNAME"].ToString();
            #endregion
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.PopulateStudentInfoSection() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //protected void txtEnrollNo_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtEnrollNo.Text != "" || txtEnrollNo.Text != string.Empty)
    //    {
    //        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO='" + txtEnrollNo.Text.Trim() + "' OR REGNO='" + txtEnrollNo.Text.Trim() + "' OR TANNO='" + txtEnrollNo.Text.Trim() + "'");
    //        if (idno.ToString() != "" && idno.ToString() != "0")
    //        {
    //            string Yearwise = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO)", "ISNULL(D.YEARWISE,0)", "IDNO=" + idno.ToString());
    //            //if (Yearwise.ToString() == "1")
    //            //{
    //            //    this.objCommon.FillDropDownList(ddlSemester, "ACD_YEAR", "SEM", "YEARNAME", "YEARNAME <> '-' AND SEM > 0", "SEM");//*******
    //            //}
    //            //else
    //            //{
    //            //    this.objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNAME <> '-' AND SEMESTERNO > 0", "SEMESTERNO");//*******
    //            //}
    //            //ddlSemester.Focus();
    //        }
    //        else
    //        {
    //            //ShowMessage("No student found for the Entered TAN/PAN");

    //            //ddlSemester.Items.Clear();
    //            txtEnrollNo.Text = string.Empty;
    //            txtEnrollNo.Focus();
    //            return;
    //        }
    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage("Please Enter TAN/PAN", this.Page);
    //        txtEnrollNo.Text = string.Empty;
    //       // ddlSemester.Items.Clear();
    //        txtEnrollNo.Focus();
    //    }
    //}   

    private void BindListView()
    {
        try
        {
            
            DataSet ds = null;
            DataSet ds1 = null;
            ds1 = objCommon.FillDropDown("ACD_CHEQUE_MASTER A LEFT JOIN ACD_POST_CHEQUE_ENTRY B ON (A.RECNO=B.CHEQUENO AND B.IDNO=" + ViewState["idno"].ToString() + ")", "A.RECNO", "ISNULL(CHEQUE,'')CHEQUE,CONVERT(VARCHAR(20),(case when CONVERT(VARCHAR(10), ISNULL(RECEIVED_DATE,''), 103) = '01/01/1900' then '' else CONVERT(VARCHAR(24), ISNULL(RECEIVED_DATE,''), 103) end),103)RECEIVED_DATE,ISNULL(AMOUNT,0)AMOUNT,ISNULL(CHEQUE_NO,'')CHEQUE_NO,ISNULL(BANK_NO,0)BANK_NO,CONVERT(VARCHAR(20),(case when CONVERT(VARCHAR(10), ISNULL(DUE_DATE,''), 103) = '01/01/1900' then '' else CONVERT(VARCHAR(24), ISNULL(DUE_DATE,''), 103) end),103)DUE_DATE", "A.RECNO>0", "A.RECNO");
            int i = 0;                   
        
            if (ds1 != null && ds1.Tables.Count > 0)
            {
                lvStudents.DataSource = ds1;
                lvStudents.DataBind();
                lvStudents.Visible = true;
                btnSubmit.Visible = true;
                btnReport.Visible = true;
                btnCancel.Visible = true;

                foreach (ListViewDataItem dataitem in lvStudents.Items)
                {
                    
                    DropDownList ddlBank = dataitem.FindControl("ddlBank") as DropDownList;
                    objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO > 0", "BANKNAME");

                    ddlBank.SelectedValue = ds1.Tables[0].Rows[i]["BANK_NO"].ToString();
                    i++;
                }
            }
            else
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lvStudents.DataSource = ds;
                        lvStudents.DataBind();
                        lvStudents.Visible = true;
                        btnSubmit.Visible = true;
                        btnReport.Visible = true;
                        btnCancel.Visible = true;
                    }
                    else
                    {
                        lvStudents.DataSource = null;
                        lvStudents.DataBind();
                        objCommon.DisplayMessage(this.Page, "No Students found for selected criteria!", this.Page);
                    }
                }
                else
                {
                    lvStudents.DataSource = null;
                    lvStudents.DataBind();
                    objCommon.DisplayMessage(this.Page, "No Students found for selected criteria!", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_SectionAllotment.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtEnrollNo.Text = string.Empty;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = false;
        divStudInfo.Visible = false;
        btnSubmit.Visible = false;
        btnCancel.Visible = false;

    }
    //protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    try
    //    {
    //        //DropDownList ddlBank = e.Item.FindControl("ddlBank") as DropDownList;
    //        TextBox txtreceive = e.Item.FindControl("txtReceivedDate") as TextBox;

    //        DataSet ds1 = objCommon.FillDropDown("ACD_CHEQUE_MASTER A LEFT JOIN ACD_POST_CHEQUE_ENTRY B ON (A.RECNO=B.CHEQUENO AND B.IDNO=" + ViewState["idno"].ToString() + ")", "A.RECNO", "ISNULL(CHEQUE,'')CHEQUE,CONVERT(VARCHAR(20),ISNULL(RECEIVED_DATE,''),103)RECEIVED_DATE,ISNULL(AMOUNT,0)AMOUNT,ISNULL(CHEQUE_NO,'')CHEQUE_NO,ISNULL(BANK_NO,0)BANK_NO,CONVERT(VARCHAR(20),ISNULL(DUE_DATE,''),103)DUE_DATE", "A.RECNO>0", "A.RECNO");
    //       // objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO > 0", "BANKNAME");

    //        int i = 0;

    //        if (ds1 != null && ds1.Tables.Count > 0)
    //        {
    //           // ddlBank.SelectedValue = ds1.Tables[0].Rows[0]["BANK_NO"].ToString();
    //           // txtreceive.Text = ds1.Tables[0].Rows[0]["RECEIVED_DATE"].ToString();
    //            lvStudents.DataSource = ds1;
    //            lvStudents.DataBind();
    //            lvStudents.Visible = true;
    //            btnSubmit.Visible = true;
    //            btnCancel.Visible = true;

    //            //foreach (ListViewDataItem dataitem in lvStudents.Items)
    //            //{
                    
    //            //    DropDownList ddlBank = dataitem.FindControl("ddlBank") as DropDownList;
    //            //    objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO > 0", "BANKNAME");

    //            //    ddlBank.SelectedValue = ds1.Tables[0].Rows[i]["BANK_NO"].ToString();
    //            //    i++;
    //            //}
    //        }
            
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "ACADEMIC_SectionAllotment.lvStudents_ItemDataBound-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StudentController objstud = new StudentController();
        int count1 = 0;
        foreach (ListViewDataItem dataitem in lvStudents.Items)
        {
            CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
            if (cbRow.Checked == true)
                count1++;
        }
        if (count1 <= 0)
        {
            objCommon.DisplayMessage(this.Page, "Please Check & Enter Atleast One Cheque Details!", this);
            return;
        }
        try
        {
            foreach (ListViewDataItem dataitem in lvStudents.Items)
            {
                CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
                if (cbRow.Checked == true)
                {
                    TextBox txtReceivedDate = (dataitem.FindControl("txtReceivedDate")) as TextBox;
                    TextBox txtAmount = (dataitem.FindControl("txtAmount")) as TextBox;
                    TextBox txtChequeNo = (dataitem.FindControl("txtChequeNo")) as TextBox;
                    DropDownList ddlBank = (dataitem.FindControl("ddlBank")) as DropDownList;
                    TextBox txtDueDate = (dataitem.FindControl("txtDueDate")) as TextBox;
                    Label lblCheque = (dataitem.FindControl("lblCheque")) as Label;

                    if (ddlBank.SelectedIndex == 0)
                    {
                        objCommon.DisplayMessage(this.Page, "Please Select Bank!!", this.Page);
                        return;
                    }
                    else
                    {
                        DateTime Receive = txtReceivedDate.Text.ToString() == string.Empty ? Convert.ToDateTime(DateTime.MinValue) : Convert.ToDateTime(txtReceivedDate.Text);
                        double Amount = Convert.ToDouble(txtAmount.Text.Trim());
                        string ChequeNo = txtChequeNo.Text.Trim();
                        int Bankno = Convert.ToInt32(ddlBank.SelectedValue);
                        DateTime DueDate = txtDueDate.Text.ToString() == string.Empty ? Convert.ToDateTime(DateTime.MinValue) : Convert.ToDateTime(txtDueDate.Text);


                        string ip = ViewState["ipAddress"].ToString();
                        int uano = Convert.ToInt32(Session["userno"]);
                        //  return;
                        CustomStatus cs = (CustomStatus)objstud.Insert_PostChequeEntry(Convert.ToInt32(ViewState["idno"].ToString()), Convert.ToInt32(lblCheque.ToolTip), Receive, Amount, ChequeNo, Bankno, DueDate, ip, uano);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage("Details Saved Successfully", this.Page);
                            ClearControls();
                        }
                        else
                        {
                            objCommon.DisplayMessage("Details Not Saved Successfully", this.Page);
                            return;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ClearControls()
    {
        txtEnrollNo.Text = string.Empty;
        divStudInfo.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = false;
        txtEnrollNo.Focus();
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("PostChequeEntry", "rptPostChequeEntry.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_IDNO=" + Convert.ToInt32(ViewState["idno"].ToString()) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnMonthReport_Click(object sender, EventArgs e)
    {
        ShowReportMonth("PostChequeEntryDueMonth", "rptPostChequeEntryDueMonth.rpt"); 
    }
    private void ShowReportMonth(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_MONTH=" + Convert.ToInt32(ddlMonth.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_YEAR=" + Convert.ToInt32(ddlYear.SelectedValue);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlMonth.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        txtEnrollNo.Text = string.Empty;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = false;
        divStudInfo.Visible = false;
        btnSubmit.Visible = false;
        btnCancel.Visible = false;
    }
}
