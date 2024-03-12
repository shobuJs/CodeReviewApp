//======================================================================================
// PROJECT NAME   : UAIMS [GHREC]                                                         
// MODULE NAME    : ACADEMIC                                                             
// PAGE NAME      : Registration No. Generation
// CREATION DATE  : 06-JULY-2011                                                          
// CREATED BY     : NIRAJ D. PHALKE                                                   
// MODIFIED DATE  :                                                                      
// MODIFIED DESC  :                                                                      
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


public partial class ACADEMIC_RegistraionNoGeneration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentRegistration objRegistration = new StudentRegistration();

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

                    this.FillDropdown();
                    btnReport.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_RegistraionNoGeneration.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=RegistraionNoGeneration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RegistraionNoGeneration.aspx");
        }
    }

    private void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_NAME");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_RegistraionNoGeneration.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON A.BRANCHNO=B.BRANCHNO AND A.DEGREENO=B.DEGREENO ", "A.STUDNAME", "A.REGNO,A.ENROLLNO", "A.ADMBATCH =  " + ddlAdmBatch.SelectedValue + "and A.COLLEGE_ID=" + ddlClgname.SelectedValue + "and A.DEGREENO=" + ddlDegree.SelectedValue + "and A.BRANCHNO=" + ddlBranch.SelectedValue + "  AND A.ADMCAN = 0 AND A.CAN = 0", "A.IDNO");

            lvStudents.DataSource = dsStudent.Tables[0];
            lvStudents.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_RegistrationNoGeneration.BindListView() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    protected void btnClear0_Click(object sender, EventArgs e)
    {
        lvStudents.DataSource = null;
        lvStudents.DataBind();

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.BindListView();
        btnReport.Enabled = true;
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {

        string code = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "ISNULL(CODE,0)", "DEGREENO='" + ddlDegree.SelectedValue + "' and BRANCHNO='" + ddlBranch.SelectedValue + "' and COLLEGE_ID='" + ddlClgname.SelectedValue + "'");

        if (code != null)
        {
            objRegistration.GenereateRegistrationNo(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
            objCommon.DisplayMessage(this.UpdatePanel1, "Registration No. Generated Successfully!", this.Page);

            this.BindListView();
        }
        else
        {
            string display = "alert('Course code is not available for selected Criteria.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", display, true);

        }
    }


    protected void btnClear0_Click1(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("RegNoReport", "rptStudentRollNoGeneration.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlClgname.SelectedValue + "", "DEGREENO");

    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT (A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO>0 and B.DEGREENO=" + ddlDegree.SelectedValue + " ", "A.LONGNAME");

    }

}
