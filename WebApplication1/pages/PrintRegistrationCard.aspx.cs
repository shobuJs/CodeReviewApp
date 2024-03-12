using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.IO;
using System.Web;
using System.Data;

public partial class ACADEMIC_REPORTS_PrintRegistrationCard : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    UAIMS_Common objUCommon = new UAIMS_Common();


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
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                    //fill dropdown method
                    PopulateDropDown();
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_CertificateMaster.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=CertificateMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CertificateMaster.aspx");
        }
    }

    private void PopulateDropDown()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_ID");
        objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN  ACD_COLLEGE_DEGREE B ON A.DEGREENO=B.DEGREENO INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO = A.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "B.COLLEGE_ID=" + ddlCollege.SelectedValue, "A.degreeno");
        ddlDegree.Focus();
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.COLLEGE_ID=" + ddlCollege.SelectedValue, "A.LONGNAME");
        ddlBranch.Focus();
    }
    protected void btnShowData_Click(object sender, EventArgs e)
    {
        try
        {
            CertificateMasterController objcertMasterController = new CertificateMasterController();
            DataSet ds;

            int branchNo = 0;
            int admbatchNo = 0;
            int degreeNo = 0;
            int collegeNo = 0;

            branchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            admbatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            degreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
            ds = objcertMasterController.GetStudentListForRegistrationCard(admbatchNo, collegeNo, degreeNo, branchNo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudentRecords.DataSource = ds.Tables[0];
                lvStudentRecords.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlExam, "No student data found", this.Page);
                lvStudentRecords.DataSource = null;
                lvStudentRecords.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CertificateMaster.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private string GetStudentIDs()
    {
        string studentIds = string.Empty;
        try
        {
            foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
                if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                    if (studentIds.Length > 0)
                        studentIds += ".";
                    studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PrintRegistrationCard.GetStudentIDs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return studentIds;
    }


    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = GetStudentIDs();
            if (!string.IsNullOrEmpty(ids))
            {
                string studentIds = string.Empty;
                foreach (ListViewDataItem lvitem in lvStudentRecords.Items)
                {
                    CheckBox chkBox = lvitem.FindControl("chkReport") as CheckBox;
                    if (chkBox.Checked == true)
                    {
                        studentIds += "$";
                        studentIds += (lvitem.FindControl("hidIdNo") as HiddenField).Value.Trim();

                    }

                }
                ShowReport(ids, "Print_Registration_Card", "rptRegistrationCardReportForMultiple.rpt");

            }
            else
            {
                objCommon.DisplayMessage(updpnlExam, "Please Select Students!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PrintRegistrationCard.btnReport_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ShowReport(string param, string reportTitle, string rptFileName)
    {

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + ddlAdmBatch.SelectedValue + ",@P_COLLEGEID=" + ddlCollege.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + " ";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PrintRegistrationCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            CertificateMasterController objcerMasterController = new CertificateMasterController();
            DataSet ds;
            string idNo = objCommon.LookUp("ACD_STUDENT", "IDNO", "CAN = 0 AND ENROLLNO='" + txtSearchEnrollno.Text.Trim() + "'");

            if (idNo != "" && idNo != null)
            {
                int chkidNo = Convert.ToInt32(idNo);
                ds = objcerMasterController.GetStudentDetailsByRegistrationNo(chkidNo);//SEARCH STUDENT FOR OTHER CERTIFICATES BY REG. NO.
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvStudentRecords.DataSource = ds.Tables[0];
                    lvStudentRecords.DataBind();

                }
                else
                {
                    objCommon.DisplayMessage(this.updpnlExam, "No student data found", this);
                    lvStudentRecords.DataSource = null;
                    lvStudentRecords.DataBind();
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlExam, "No student found having Registration no.: " + txtSearchEnrollno.Text.Trim(), this);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_CertificateMaster.btnSearch_BC_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}