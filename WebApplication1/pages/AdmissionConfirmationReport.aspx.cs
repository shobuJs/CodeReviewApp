using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_AdmissionConfirmationReport : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
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
                    //fill dropdown
                    PopulateDropDown();
                //get ip address
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

            }
            divMsg.InnerHtml = string.Empty;
        }
        divMsg.InnerHtml = string.Empty;
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StandardFeeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StandardFeeDefinition.aspx");
        }
    }
    #endregion

    private void PopulateDropDown()
    {
        try
        {
            //FILL DROPDOWN 
            this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
            this.objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
            this.objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_UpdateRegistrationNo.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    private void BindListView()
    {
        DataSet ds = objSC.GetAdmittedStudents(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            pnllist.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lvStudent.Visible = true;
        }
        else
        {
            pnllist.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lvStudent.Visible = false;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    private void ShowReport(string reportTitle, string rptFileName, string userno)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "filename=" + "StudentAdmissionConfirmation" + ".pdf";
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNO=" + userno;

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_gradeCard .ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void lvStudent_ItemCommand(object sender, ListViewCommandEventArgs e)
    {

        string admcan = Convert.ToString(objCommon.LookUp("ACD_STUDENT", "admcan", "idno = " + (e.CommandArgument) + ""));
        if (e.CommandName == "Report")
        {
            if (admcan == "0")
            {
                ShowReport("Student Admission Confirmation Report", "StudentAdmissionConfirmationReport.rpt", e.CommandArgument.ToString());
            }

            else if (admcan != "0")
            {
                int chk = objSC.UpdateAdmStatus((e.CommandArgument).ToString());
                if (chk != null)
                {
                    objCommon.DisplayMessage("Admission Confirmed.", this.Page);
                    BindListView();


                }
            }
        }


    }

}