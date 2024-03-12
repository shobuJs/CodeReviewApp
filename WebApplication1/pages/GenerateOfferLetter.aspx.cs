//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : COUNTER 1
// CREATION DATE : 01-MARCH-2014
// CREATED BY    : RENUKA ADULKAR
// MODIFIED BY   : 
// MODIFIED DATE : 
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
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;


public partial class Academic_GenerateOfferLetter : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    AdmissionCancellationController admCanController = new AdmissionCancellationController();

    #region page
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
        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
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

                objCommon.FillDropDownList(ddlSession, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                ddlSession.Items.RemoveAt(0);
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");

            }
            txtDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
        }
        divMsg.InnerHtml = string.Empty;
        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];


    }

    #endregion

    #region User Defined Methods



    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=GenerateOfferLetter.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=GenerateOfferLetter.aspx");
        }
    }
    #endregion

    #region button
    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        CheckBox chkAllot = dataitem.FindControl("chkAllot") as CheckBox;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnOfferletter_Click(object sender, EventArgs e)
    {
        string studentIds = string.Empty;
        //Get Selected Students..
        foreach (ListViewDataItem lvItem in lvStudents.Items)
        {
            CheckBox chkBox = lvItem.FindControl("chkAllot") as CheckBox;
            if (chkBox.Checked == true)
            {
                studentIds += chkBox.ToolTip + "$";
            }
        }
        if (studentIds.Length <= 0)
        {
            objCommon.DisplayMessage("Please Select Atleast One Student.", this.Page);
            return;
        }
        this.ShowOfferLetterReport("Offer Letter", "OfferLetterBulk.rpt", studentIds);
    }
    private void ShowOfferLetterReport(string reportTitle, string rptFileName, string userno)
    {
        try
        {
            int Report_Type = 0;
            if (ddlListType.SelectedValue == "1")
            {
                Report_Type = 1;
            }
            else if (ddlListType.SelectedValue == "2")
            {
                Report_Type = 2;
            }
            else
            {
                Report_Type = 3;
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "filename=" + "offerletter" + ".pdf";
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNO=" + userno + ",@P_REPORT_TYPE=" + Report_Type + ",@P_PRINT_DATE=" + Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy") + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue).ToString() + ",@P_ENTERANCE=" + Convert.ToInt32(ddlEntrance.SelectedValue).ToString() + ",@P_ROUNDNO=" + Convert.ToInt32(ddlRound.SelectedValue).ToString() + ",@P_FROM_DATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("dd/MMM/yyyy") + ",@P_TO_DATE=" + Convert.ToDateTime(txtToDate.Text).ToString("dd/MMM/yyyy");

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_gradeCard .ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlListType.SelectedValue == "1")
                bindallotedlist(Convert.ToInt32(ddlSession.SelectedValue), 1, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlEntrance.SelectedValue), Convert.ToInt32(ddlRound.SelectedValue));
            else if (ddlListType.SelectedValue == "2")
                bindallotedlist(Convert.ToInt32(ddlSession.SelectedValue), 2, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlEntrance.SelectedValue), Convert.ToInt32(ddlRound.SelectedValue));
            else if (ddlListType.SelectedValue == "3")
                bindallotedlist(Convert.ToInt32(ddlSession.SelectedValue), 3, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlEntrance.SelectedValue), Convert.ToInt32(ddlRound.SelectedValue));

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_GenerateOfferLetter.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void bindallotedlist(int sessionno, int status, int degree, int Entrance, int round)
    {
        DataSet ds = objSC.GetConfirmWaitingbothStudents(sessionno, status, degree, Entrance, round);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvStudents.DataSource = ds.Tables[0];
            lvStudents.DataBind();
            lvStudents.Visible = true;
        }
        else
        {
            objCommon.DisplayMessage("Record not found", this.Page);
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            lvStudents.Visible = false;
        }
    }


    protected void ddlListType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = false;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlEntrance, "ACD_ENTRE_DEGREE ED INNER JOIN ACD_QUALEXM Q ON(ED.QUALIFYNO=Q.QUALIFYNO)", "ED.QUALIFYNO", "Q.QUALIEXMNAME", "ED.DEGREENO=" + ddlDegree.SelectedValue, "QUALIFYNO DESC");
        }
    }


    protected void btnDownload_Click(object sender, EventArgs e)
    {

    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
    }
}
    #endregion