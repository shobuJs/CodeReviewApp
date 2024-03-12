//======================================================================================
// PROJECT NAME  : SVCE                                                          
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : Admission Reports                                                   
// CREATION DATE : 9/7/2019                                                      
// CREATED BY    : Dipali N                                                     
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_AdmissionReports : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
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
                if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Page Authorization
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Populate all the DropDownLists
                    FillDropDown();
                }
            }
            else
            {
                // Clear message div
                divMsg.InnerHtml = string.Empty;                
            }
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_AdmissionReports.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=AdmissionReports.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AdmissionReports.aspx");
        }
    }
    //Bind Admission Year , Degree Name,Branch Name,Semester name,Fill IDType,Admission Quota.
    public void FillDropDown()
    {
        objCommon.FillDropDownList(ddlAdmYear, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME DESC");  //Fill Admission Year
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_ID ASC");// Fill College
        objCommon.FillDropDownList(ddlIdType, "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO>0", "IDTYPENO"); //Fill IdType 
        objCommon.FillDropDownList(ddlQuota, "ACD_ADMISSION_QUOTA", "ADMQUOTANO", "QUOTANAME", "ISNULL(ADMQUOTANO,0)>0", "ADMQUOTANO"); //Fill Admission Quota 
    }
   
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlCollege.SelectedValue) > 0)
        {
            ddlDegree.Items.Clear();
            objCommon.FillDropDownList(ddlDegree,"ACD_COLLEGE_DEGREE CD INNER JOIN ACD_DEGREE  DR ON(DR.DEGREENO=CD.DEGREENO)", "DR.DEGREENO", "DEGREENAME", "CD.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "DR.DEGREENO ASC");
        }
        else
        {
            objCommon.DisplayMessage(this, "Please Select College", this);
            ddlDegree.Focus();
        }
    }

    //On select of Degree bind Branch name in drop down list
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlDegree.SelectedValue) > 0)
        {
            ddlBranch.Items.Clear();
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue.ToString() + " AND B.COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]), "A.LONGNAME");
            ddlBranch.Focus();
        }
        else
        {
            objCommon.DisplayMessage(this, "Please select degree", this);
            ddlDegree.Focus();
        }
    }

    protected void btnoperatorentry_Click(object sender, EventArgs e)
    {
        ShowAdmissionReport("Operator Entry Student Report", "rptAdmissionReport.rpt", 1);
    }
    protected void btnPrincipalApproval_Click(object sender, EventArgs e)
    {
        ShowAdmissionReport("Principal Approval Report", "rptAdmissionReport.rpt", 2);
    }
    protected void btnAdmConfirm_Click(object sender, EventArgs e)
    {
        ShowAdmissionReport("Final Admission Confirm Student Report", "rptAdmissionReport.rpt", 3);
    }

    //reset controllers.
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlCollege.SelectedIndex = 0;
        ddlAdmYear.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlIdType.SelectedIndex = 0;
        ddlQuota.SelectedIndex = 0;
    }

    private void ShowAdmissionReport(string reportTitle, string rptFileName, int reportno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            if (reportno != 0)
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmYear.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_ADMQUOTANO=" + Convert.ToInt32(ddlQuota.SelectedValue) + ",@P_IDTYPENO=" + Convert.ToInt32(ddlIdType.SelectedValue) + ",@P_FLAG=" + reportno;
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.pnlAdmReport, this.pnlAdmReport.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_AdmissionReports.ShowAdmissionReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region Report
    //this Button is used for to showing the Original Certificate Submitted report .
    protected void btnOrigCertSub_Click(object sender, EventArgs e)
    {
        try
        {
            int admBatch = Convert.ToInt32(ddlAdmYear.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlAdmYear.SelectedValue);
            int degreeNo = Convert.ToInt32(ddlDegree.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlDegree.SelectedValue);
            int branchNo = Convert.ToInt32(ddlBranch.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlBranch.SelectedValue);
            int semNo = Convert.ToInt32(ddlSem.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlSem.SelectedValue);
            int idtype = Convert.ToInt32(ddlIdType.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlIdType.SelectedValue);

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + "OriginalCertificateSubmittedByStud";
            url += "&path=~,Reports,Academic,AdmissionReports," + "Orig_Cert_Submitted_Report.rpt";
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + admBatch + ",@P_DEGREENO=" + degreeNo + ",@P_BRANCHNO=" + branchNo + ",@P_SEMESTERNO=" + semNo + ",@P_IDTYPENO=" + idtype;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this, this.updProg.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_AdmissionReports.btnOrigCertSub_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //this Button is used for to showing the First Generation Graduate report .
    protected void btnFstGenGrad_Click(object sender, EventArgs e)
    {
        try
        {
            int admBatch = Convert.ToInt32(ddlAdmYear.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlAdmYear.SelectedValue);
            int degreeNo = Convert.ToInt32(ddlDegree.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlDegree.SelectedValue);
            int branchNo = Convert.ToInt32(ddlBranch.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlBranch.SelectedValue);
            int semNo = Convert.ToInt32(ddlSem.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlSem.SelectedValue);
            int idtype = Convert.ToInt32(ddlIdType.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlIdType.SelectedValue);
            int admQouta = Convert.ToInt32(ddlQuota.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlQuota.SelectedValue);

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + "FirstGenerationGraduate";
            url += "&path=~,Reports,Academic,AdmissionReports," + "FirstGenGrad.rpt";
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + admBatch + ",@P_DEGREENO=" + degreeNo + ",@P_BRANCHNO=" + branchNo + ",@P_SEMESTERNO=" + semNo + ",@P_IDTYPENO=" + idtype + ",@P_ADMQUOTANO=" + admQouta;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this, this.updProg.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_AdmissionReports.btnFstGenGrad_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    //showing Student information report on quota wise selection and management wise selection.
    private void ShowReport(string reportTitle, string rptFileName, int reportno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            if (reportno == 1)
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmYear.SelectedValue) + ",@P_DEGREE=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCH=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTER=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_IDTYPE=" + Convert.ToInt32(ddlIdType.SelectedValue);
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.pnlAdmReport, this.pnlAdmReport.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //showing the HSC marksheet report on quotawise
    private void ShowReport_HSCMarkSheet(string reportTitle, string rptFileName, int reportno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            int admQouta = Convert.ToInt32(ddlQuota.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlQuota.SelectedValue);
            if (reportno == 1)
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmYear.SelectedValue) + ",@P_DEGREE=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCH=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTER=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_IDTYPE=" + Convert.ToInt32(ddlIdType.SelectedValue) + ",@P_ADMQUOTANO=" + admQouta;
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.pnlAdmReport, this.pnlAdmReport.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    ////showing HSC MarkSheet Report on rptHSCMarksheetDetails.rpt file .
    protected void btnHSCMarkSheet_Click(object sender, EventArgs e)
    {
        ShowReport_HSCMarkSheet("HSC MarkSheet Report", "rptHSCMarksheetDetails.rpt", 1);
    }
    //showing the report Student Information Report rptStudentInformation.rpt file .
    protected void btnStudInfo_Click(object sender, EventArgs e)
    {
        ShowReport("Student Information Report", "rptStudentInformation.rpt", 1);
    }

    protected void btnStudTcCC_Click(object sender, EventArgs e)
    {
        ShowReportTc("Student_TC_CC_DetailsReport", "rptStudentTC_CC.rpt");
    }

    private void ShowReportTc(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmYear.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.pnlAdmReport, this.pnlAdmReport.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnTransferStudent_Click(object sender, EventArgs e)
    {
        ShowReport_TransferStudent("Transfer Student Report", "Transfer_Admission.rpt");
    }

    private void ShowReport_TransferStudent(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_BATCHNO=" + Convert.ToInt32(ddlAdmYear.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue); //+ ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.pnlAdmReport, this.pnlAdmReport.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion
}