//======================================================================================
// PROJECT NAME  : SVCE
// MODULE NAME   : ACADEMIC
// PAGE NAME     : ADMISSION CANCELLATION
// CREATION DATE : 20/06/19
// CREATED BY    : Pritesh S
// MODIFIED BY   : 
// MODIFIED DATE : 
// MODIFIED DESC : 
//======================================================================================

using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Globalization;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Net;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Net.Security;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;


public partial class Academic_AdmissionCancellation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    AdmissionCancellationController admCanController = new AdmissionCancellationController();
    StudentController objSC = new StudentController();
    UserAcc objUA = new UserAcc();
    UserController user = new UserController();
    string idno = string.Empty;
    #region Page Events
    //USED FOR INITIALSING THE MASTER PAGE
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
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
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Page Authorization
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                    //Populate all the DropDownLists
                    FillDropDown();
                    objCommon.SetLabelData("0");//for label
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
                }
            }
            else
            {
                // Clear message div
                divMsg.InnerHtml = string.Empty;

                /// Check if postback is caused by reprint receipt or cancel receipt buttons
                /// if yes then call corresponding methods
                if (Request.Params["__EVENTTARGET"] != null && Request.Params["__EVENTTARGET"].ToString() != string.Empty)
                {
                    if (Request.Params["__EVENTTARGET"].ToString() == "CancelAdmission")
                        idno = Request.Params["__EVENTARGUMENT"].ToString();
                    this.CancelAdmission(Convert.ToInt32(idno));
                }
            }
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_AdmissionCancellation.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page. 
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
    #endregion
    #region Admission Cancle Tab
    public void FillDropDown()
    {
            objCommon.FillDropDownList(ddlColllege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'') COLLEGE_NAME", "COLLEGE_ID in(" + Session["college_nos"] + ")", string.Empty);
    
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        try
        {
         //   divmsgs.InnerText = string.Empty;
            string searchBy = string.Empty;
            string searchText = txtSearchText.Text.Trim();
            string errorMsg = string.Empty;
            txtRemark.Text = string.Empty;
            //if (rdoStudName.Checked)
            //{
            //    searchBy = "name";
            //    errorMsg = "having name: " + txtSearchText.Text.Trim();
            //}
            //else 
           //if (rdoRollNo.Checked)
           // {
                searchBy = "REGNO";
                errorMsg = "having Roll no.: " + searchText;
            //}
            ShowStudents(searchBy, searchText, errorMsg);
            ////lvClearanceDetails.Visible = false;
            ////btnCancelAdmission.Visible = false;
            ////btnCancelAdmissionSlip.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_AdmissionCancellation.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowStudents(string searchBy, string searchText, string errorMsg)
    {
        DataSet ds = admCanController.SearchStudents(searchText, searchBy);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvSearchResults.DataSource = ds;
            lvSearchResults.DataBind();
            lvSearchResults.Visible = true;
            // ViewState["StudentId"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
            btnCancelAdmission.Visible = true;
            btnCancelAdmissionSlip.Visible = true;
            divRemark.Visible = true;
            //txtRemark.Text = "";
            //txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
            ////btnShowClearance.Visible = true;
            ViewState["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();

            ViewState["SEMESTERNAME"] = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();

            ViewState["BRANCHNO"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();

            ViewState["REGNO"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
            ViewState["NAME"] = ds.Tables[0].Rows[0]["NAME"].ToString();
            ViewState["DEGREE"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();

            if (searchBy == "REGNO")
            {
                ViewState["StudentId"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
            }
            else
            {
                txtRemark.Text = string.Empty;
            }
        }
        else
        {
            //ShowMessage("No student found " + errorMsg);
            lvSearchResults.Visible = false;
            btnCancelAdmission.Visible = false;
            btnCancelAdmissionSlip.Visible = false;
            divRemark.Visible = false;
            ////btnShowClearance.Visible = false;
        }
    }
    private void CancelAdmission(int idno)
    {
        try
        {
            //int studId = (GetViewStateItem("StudentId") != string.Empty ? int.Parse(GetViewStateItem("StudentId")) : 0);//get another person idno for update
            int studId = idno; //(GetViewStateItem("StudentId") != string.Empty ? int.Parse(GetViewStateItem("StudentId")) : 0);
            string remark = "This student's admission has been cancelled by " + Session["userfullname"].ToString();
            remark += " on " + DateTime.Now + ". " + txtRemark.Text.Trim();
            int chk_already_can = 0;
            chk_already_can = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COUNT(*)", "ISNULL(ADMCAN,0)=1 AND ISNULL(ADMCAN,0)=1 and  IDNO=" + studId + ""));
            if (chk_already_can > 0)
            {
                objCommon.DisplayMessage(this, "Already Admission cancelled for this student.", this.Page);
                return;
            }
            else
            {
                if (admCanController.CancelAdmission(studId, remark, Session["ipAddress"].ToString(), Convert.ToInt32(Session["userno"])))
                {
                    if (Convert.ToInt32(ViewState["MailAlert"]) == 1)
                    {
                        SendEmail("Cancelled", "Admission Cancelled");
                    }
                    objCommon.DisplayMessage(this, "Admission cancelled successfully.", this.Page);
                   // ShowMessage("Admission cancelled successfully.");
                    // objCommon.DisplayMessage(this.updReadmit, "Admission cancelled successfully.", this.Page);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script", "alert('Admission cancelled successfully.'); location.href='AdmissionCancellation.aspx';", true);
                    btnCancelAdmissionSlip.Enabled = true;
                    if (rdoStudName.Checked)
                    {
                        txtRemark.Text = string.Empty;
                    }

                    // Response.Redirect(Request.Url.ToString());
                    lvSearchResults.Visible = false;
                    btnCancelAdmission.Visible = false;
                    btnCancelAdmissionSlip.Visible = false;
                    divRemark.Visible = false;
                    txtSearchText.Text = string.Empty;
                    // divmsgs.InnerText = string.Empty;
                }
                else
                    objCommon.DisplayMessage(this, "Unable to cancel the student\\'s admission.", this.Page);
                //    ShowMessage("Unable to cancel the student\\'s admission.");


            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_AdmissionCancellation.CancelAdmission() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    public void SendEmail(string Title, string SUBJECT)
    {

        //ViewState["SEMESTERNO"]

        string subject = "";
        string matter = "";
        int Branchno = Convert.ToInt32(ViewState["BRANCHNO"].ToString());
        int degreeno = Convert.ToInt32(ViewState["DEGREE"].ToString());
        //string SessionName = (ViewState["Session_Name"].ToString());
        int ua_no = Convert.ToInt32(Session["userno"]);
        DataSet dsUserContact = null;
        DateTime Date = DateTime.Now;
        string message = "";
        string Cc = "";
        DateTime Dates = DateTime.Now;
        DataSet ds = objCommon.FillDropDown("USER_ACC", "DISTINCT UA_FULLNAME", "UA_EMAIL", "UA_NO=" + Convert.ToInt32(Session["userno"]) + "", "");
        DataSet dsreff = objCommon.FillDropDown("REFF", "TRIGERED_EMAIL", "MailAlert", "", "");
        string TOemailid = Convert.ToString(ds.Tables[0].Rows[0]["UA_EMAIL"]);
        string username = Convert.ToString(ds.Tables[0].Rows[0]["UA_FULLNAME"]);

        string admtype = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT UGPGOT", "DEGREENO =" + Convert.ToInt32(degreeno) + "");

        dsUserContact = user.GetEmailTamplateandUserDetails(Convert.ToString(ViewState["COLLEGE_ID"]), Convert.ToString(Branchno), username.ToString(), Convert.ToInt32(Request.QueryString["pageno"]));
        message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
        subject = SUBJECT;
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp-mail.outlook.com");
        //DataSet dsconfig = null;
        //dsconfig = objCommon.FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD,FASCILITY", string.Empty, string.Empty);
        string emailfrom = Convert.ToString(dsUserContact.Tables[2].Rows[0]["EMAILSVCID"]);
        string emailpass = Convert.ToString(dsUserContact.Tables[2].Rows[0]["EMAILSVCPWD"]);
        int fascility = Convert.ToInt32(dsUserContact.Tables[2].Rows[0]["FASCILITY"].ToString());
        if (fascility == 1 || fascility == 3)
        {
            if (emailfrom != "" && emailpass != "")
            {
                mail.From = new MailAddress(emailfrom);
                string MailFrom = emailfrom;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(emailfrom, emailpass);
                SmtpServer.EnableSsl = true;
                string aa = string.Empty;
                mail.Subject = subject;
                mail.To.Clear();
                mail.To.Add(TOemailid);
                // mail.CC.Add("coe@bitmesra.ac.in");
                if (Convert.ToString(dsUserContact.Tables[0].Rows[0]["CCMAILID"]) != "")
                {
                    mail.CC.Add(Convert.ToString(dsUserContact.Tables[0].Rows[0]["CCMAILID"]));
                }
                mail.IsBodyHtml = true;
                mail.Body = message;

                mail.Body = mail.Body.Replace("[USERNAME]", username);
                mail.Body = mail.Body.Replace("[TITLE]", Title);
                mail.Body = mail.Body.Replace("[REGNO]", ViewState["REGNO"].ToString());
                mail.Body = mail.Body.Replace("[STUDNAME]", ViewState["NAME"].ToString());
                //mail.Body = mail.Body.Replace("[SESSION]", SessionName.ToString());
                mail.Body = mail.Body.Replace("[SEMESTER]", ViewState["SEMESTERNAME"].ToString());
                mail.Body = mail.Body.Replace("[DATE]", Convert.ToString(Dates));

                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                SmtpServer.Send(mail);
                if (DeliveryNotificationOptions.OnSuccess == DeliveryNotificationOptions.OnSuccess)
                {
                    //return ret = 1;
                    //Storing the details of sent email
                }
            }
            // return ret = 0;
        }
    }
    protected void btnCancelAdmissionSlip_Click(object sender, EventArgs e)
    {
        ShowReport_Adm_Cancel("StudentAdmissionCancel", "StudentAdmissionCancellationSlip.rpt");
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
    private void ShowReport_Adm_Cancel(string reportTitle, string rptFileName)
    {
        try
        {
            int studId = (GetViewStateItem("StudentId") != string.Empty ? int.Parse(GetViewStateItem("StudentId")) : 0);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_IDNO=" + studId + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlColllege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlColllege.SelectedIndex != 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_DEGREE D  ON (D.DEGREENO=DB.DEGREENO) INNER JOIN ACD_BRANCH B  ON (B.BRANCHNO=DB.BRANCHNO)", "CONVERT(NVARCHAR,D.DEGREENO) +','+ CONVERT(VARCHAR,B.BRANCHNO) AS PROGRAMNO", "(D.CODE +' - '+LONGNAME) AS PROGRAM", "COLLEGE_ID=" + Convert.ToInt32(ddlColllege.SelectedValue), "");
        }
        else
        {
            ddlDegree.SelectedValue = "0";
        }
    }
    private void clearControl()
    {
        ddlDegree.SelectedIndex = 0;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            //check the from date always be less than to date
            string[] fromDate = txtFromDate.Text.Split('/');
            string[] toDate = txtToDate.Text.Split('/');
            DateTime fromdate = Convert.ToDateTime(Convert.ToInt32(fromDate[0]) + "/" + Convert.ToInt32(fromDate[1]) + "/" + Convert.ToInt32(fromDate[2]));
            DateTime todate = Convert.ToDateTime(Convert.ToInt32(toDate[0]) + "/" + Convert.ToInt32(toDate[1]) + "/" + Convert.ToInt32(toDate[2]));
            if (fromdate > todate)
            {
                objCommon.DisplayMessage("From Date always be less than To date. Please Enter proper Date range.", this.Page);
                clearControl();
            }
            else
            {
                ShowReport("BranchCancelAdmission", "rptBranchwiseAdmissionCancel.rpt");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Admission_Cancellation.Report --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int degreeno = 0,branchno=0;
            string[] couser ={};
            if (ddlDegree.SelectedValue !="0")
            {
                couser = (ddlDegree.SelectedValue).Split(',');
                degreeno=Convert.ToInt32(couser[0].ToString());
                branchno = Convert.ToInt32(couser[1].ToString());
            }
            
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            ////url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_START_DATE=" + txtFromDate.Text.Trim() + ",@P_END_DATE=" + txtToDate.Text.Trim() + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + 0;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_START_DATE=" + txtFromDate.Text.Trim() + ",@P_END_DATE=" + txtToDate.Text.Trim() + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@UserName=" + Session["username"].ToString() + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlColllege.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {

        try
        {
            int degreeno = 0, branchno = 0;
            string[] couser = { };
            if (ddlDegree.SelectedValue != "0")
            {
                couser = (ddlDegree.SelectedValue).Split(',');
                degreeno = Convert.ToInt32(couser[0].ToString());
                branchno = Convert.ToInt32(couser[1].ToString());
            }
            DataSet ds = objSC.GetAdmissionCancelStudentData(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), degreeno, branchno, Convert.ToInt32(ddlColllege.SelectedValue));

            if (ds.Tables[0].Rows.Count <= 0)
            {
                objCommon.DisplayMessage(updReadmit, "No Records Found.", this.Page);
                return;
            }

            GridView gvStudData = new GridView();
            gvStudData.DataSource = ds;
            gvStudData.DataBind();
            string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
            string attachment = "attachment; filename = Application/Admission Details Degree wise.xls";
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
        catch (Exception ex)
        {
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion
    #region Readmission Tab

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtApplicationID.Text.Trim() == string.Empty)
            {

                objCommon.DisplayMessage(this.updReadmit, "Please Enter Enrollment No. or RegNo.!!", this.Page);
            }
            else
            {
                showdetails();

            }
        }
        catch (Exception ex)
        {
        }
    }
    private void showdetails()
    {
        txtReAdmission.Text = string.Empty;
        // Response.Redirect(Request.Url.ToString());
        DataSet ds = new DataSet();
        ds = objSC.GetAdmissionCancelStudentInfo(txtApplicationID.Text.Trim());
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            divStudInfo.Visible = true;
            DataRow dr = ds.Tables[0].Rows[0];
            this.PopulateStudentInfoSection(dr);       // show student information
        }
        else
        {
            objCommon.DisplayMessage(this.updReadmit, "Please Enter Admission Canceled Student only!!", this.Page);
            txtApplicationID.Text = string.Empty;
        }
    }
    private void PopulateStudentInfoSection(DataRow dr)
    {
        try
        {
            lblStudName.Text = dr["STUDNAME"].ToString();
            lblStudName.ToolTip = dr["ENROLLNO"].ToString();
            lblSex.Text = dr["SEX"].ToString();
            lblRegNo.Text = dr["REGNO"].ToString();
            lblRegNo.ToolTip = dr["IDNO"].ToString();
            lblDateOfAdm.Text = ((dr["ADMDATE"].ToString().Trim() != string.Empty) ? Convert.ToDateTime(dr["ADMDATE"].ToString()).ToShortDateString() : dr["ADMDATE"].ToString());
            lblPaymentType.Text = dr["PAYTYPENAME"].ToString();
            lblDegree.Text = dr["PROGRAM"].ToString();
            lblDegree.ToolTip = dr["DEGREENO"].ToString();
            //lblBranch.Text = dr["BRANCH_NAME"].ToString();

            ViewState["DEGREE"] = dr["DEGREENO"].ToString();
            ViewState["BRANCHNO"] = dr["BRANCHNO"].ToString();

            ViewState["COLLEGE_ID"] = dr["COLLEGE_ID"].ToString();
            ViewState["SEMESTERNO"] = dr["SEMESTERNO"].ToString();

            ViewState["SEMESTERNAME"] = dr["SEMESTERNAME"].ToString();
            ViewState["REGNO"] = dr["REGNO"].ToString();
            ViewState["NAME"] = dr["STUDNAME"].ToString();


            lblYear.Text = dr["YEARNAME"].ToString();
            lblSemester.Text = dr["SEMESTERNAME"].ToString();
            lblBatch.Text = dr["BATCHNAME"].ToString();
            txtRemarkAdm.Text = dr["REMARK"].ToString();
            lblCollege.Text = dr["COLLEGE_NAME"].ToString();
            divStudInfo.Visible = true;
            remark.Visible = true;
            ReAdmremark.Visible = true;
            txtReAdmission.Enabled = true;
            btnSave.Enabled = true;
        }
        catch (Exception ex)
        {
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objUA.UA_IDNO = Convert.ToInt32(lblRegNo.ToolTip);
            objUA.IP_ADDRESS = Convert.ToString(Session["ipAddress"].ToString());
            string remark = txtReAdmission.Text.Trim();
            objUA.USER_ID = Convert.ToString(Session["userno"]);
            string regno = lblRegNo.Text;
            if (admCanController.RE_Admission_Cancel_Student(objUA, remark, regno))
            {
                //ShowMessage(this.updReadmit,"RE_Admission done successfully.".this.Page);

                if (Convert.ToInt32(ViewState["MailAlert"]) == 1)
                {
                    SendEmail("Re_Admission", "Re_Admission");
                }

                objCommon.DisplayMessage(this.updReadmit, "Re_Admission done successfully.", this.Page);
                showdetails();
                btnSave.Enabled = false;
                txtReAdmission.Enabled = false;
                divStudInfo.Visible = false;
                ////lvSearchResults.Visible = false;
                ////btnCancelAdmission.Visible = false;
                ////btnCancelAdmissionSlip.Visible = false;
                ////divRemark.Visible = false;
            }
            else
                //ShowMessage(this.updReadmit,"Unable to cancel the student\\'s admission.",this.Page);
                objCommon.DisplayMessage(this.updReadmit, "Unable to Re_Admitted the student\\'s admission.", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_AdmissionCancellation.CancelAdmission() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCan_Click(object sender, EventArgs e)
    {
        divStudInfo.Visible = false;
        remark.Visible = false;
        ReAdmremark.Visible = false;
        txtReAdmission.Text = string.Empty;
        txtApplicationID.Text = string.Empty;
    }
    #endregion
}