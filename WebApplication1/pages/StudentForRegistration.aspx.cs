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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using System.Text;
public partial class ACADEMIC_StudentForRegistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    int idno;
    StudentController stdController = new StudentController();
    CustomStatus cs = new CustomStatus();
    DataSet dsStudent = new DataSet();

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
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                // Load Page Help
                if (Request.QueryString["pageno"] != null)
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                //fill sessions
                string sessionno = objCommon.LookUp("REFF", "CUR_ADM_SESSIONNO", string.Empty);
                if (string.IsNullOrEmpty(sessionno))
                {
                    objCommon.DisplayMessage("Please Set the Admission Session from Reference Page!!", this.Page);
                    return;
                }
                else
                {
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO=" + sessionno, "SESSIONNO DESC");
                    ddlSession.Items.RemoveAt(0);
                }
            }

        }

        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentForRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentForRegistration.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (rbtYes.Checked == true)
        {
            UpdateStudentStatus(Convert.ToInt32(txtRegistrationNo.Text));
            tdStudInfo.Visible = false;
        }
        else if (rbtNo.Checked == true)
        {
            lblMinority.Text = string.Empty;
            lblStudentname.Text = string.Empty;
            tdStudInfo.Visible = false;
            objCommon.DisplayMessage(updMeritList, "Please Check Yes To Register", this);
        }
        Clear();
    }

    private void UpdateStudentStatus(int studentID)
    {
        try
        {

            cs = (CustomStatus)stdController.UpDateStudentStatus(studentID, (Convert.ToInt32(ddlSession.SelectedValue)));
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                this.ShowReport("PROCESS_FORM", "rptProcessForm.rpt");
            }
            else
            {
                objCommon.DisplayMessage(updMeritList, "Error in Registration", this);
            }
        }

        catch (Exception ex)
        {

        }

    }


    private void Clear()
    {
        txtRegistrationNo.Text = string.Empty;
        rbtNo.Checked = true;
    }
    protected void txtRegistrationNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            idno = Convert.ToInt32(txtRegistrationNo.Text);
            long lookidno = Convert.ToInt32((objCommon.LookUp("ACD_REGISTRATION", "count(IDNO)", "IDNO = " + Convert.ToInt32(txtRegistrationNo.Text) + " and SESSIONNO=" + ddlSession.SelectedValue)));
            if (idno == 1)
            {
                tdStudInfo.Visible = true;
                ShowStudent(idno);
            }
            else
            {
                objCommon.DisplayMessage("This Registration Number Does Not Exist", this);
            }
            btnSubmit.Enabled = true;
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage("This Registration Number Does Not Exist", this);
            txtRegistrationNo.Text = string.Empty;
        }
    }

    private void ShowStudent(long idno)
    {
        try
        {
            dsStudent = stdController.Getstudent(idno, (Convert.ToInt32(ddlSession.SelectedValue)));
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    lblStudentname.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblCategory.Text = dsStudent.Tables[0].Rows[0]["CATEGORY"].ToString();
                    if (dsStudent.Tables[0].Rows[0]["REGISTERED"].ToString().ToUpper() == "TRUE")
                    {
                        rbtYes.Checked = true;
                        rbtNo.Checked = false;
                    }
                    else
                    {
                        rbtNo.Checked = true;
                        rbtYes.Checked = false;
                    }


                    if (dsStudent.Tables[0].Rows[0]["MINORITY"].ToString() == "1")
                    {
                        lblMinority.Text = "Yes";
                    }
                    else if (dsStudent.Tables[0].Rows[0]["MINORITY"].ToString() == "0")
                    {
                        lblMinority.Text = "No";
                    }
                    if (dsStudent.Tables[0].Rows[0]["MHCET_SCORE"].ToString() == "0")
                    {
                        lblMhcet.Text = "AIEEE SCORE";
                        lblMhcetScore.Text = dsStudent.Tables[0].Rows[0]["AIEEE_SCORE"].ToString();
                        lblPcm.Text = "HSC PCM";
                        lblPcmSCore.Text = dsStudent.Tables[0].Rows[0]["HSC_PCM"].ToString();

                    }
                    else if (dsStudent.Tables[0].Rows[0]["AIEEE_SCORE"].ToString() == "0")
                    {
                        lblMhcet.Text = "MHCET SCORE";
                        lblMhcetScore.Text = dsStudent.Tables[0].Rows[0]["MHCET_SCORE"].ToString();
                        lblPcm.Text = "HSC PCM";
                        lblPcmSCore.Text = dsStudent.Tables[0].Rows[0]["HSC_PCM"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_sStudentForRegistration.aspx.ShowStudent() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_IDNO=" + txtRegistrationNo.Text.Trim() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            string Script = string.Empty;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updMeritList, updMeritList.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MeritListGeneration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
