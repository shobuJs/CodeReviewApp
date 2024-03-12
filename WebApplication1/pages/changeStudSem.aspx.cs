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
using IITMS.SQLServer.SQLDAL;

public partial class Academic_changeStudSem : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //ConnectionString
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=changestudsem.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=changestudsem.aspx");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            StudentRegistration objSRegist = new StudentRegistration();

            StudentController objSC = new StudentController();
            int idno = objSC.GetStudentIDByRegNo(txtEnrollmentNo.Text.Trim());

            DataTableReader dtr = objSRegist.GetStudentDetails(idno);

            if (dtr.Read())
            {
                lblName.Text = dtr["STUDNAME"].ToString();
                lblScheme.Text = dtr["SCHEMENAME"].ToString();
                lblScheme.ToolTip = dtr["SCHEMENO"].ToString();
                lblSemester.Text = dtr["SEMESTERNAME"].ToString();
                lblSemester.ToolTip = dtr["SEMESTERNO"].ToString();
                lblBranch.Text = dtr["LONGNAME"].ToString();
                lblBranch.ToolTip = dtr["BRANCHNO"].ToString();
                ddlSemester.Enabled = true;
                ddlYear.Enabled = true;
            }
            else
                lblMsg.Text = "Student Not Found!!!";

            dtr.Close();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_changeStudSem.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

            lblMsg.Text = "Student Not Found!!!";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        txtEnrollmentNo.Text = string.Empty;
        lblName.Text = string.Empty;
        lblName.ToolTip = string.Empty;
        lblScheme.Text = string.Empty;
        lblScheme.ToolTip = string.Empty;
        lblBranch.Text = string.Empty;
        lblBranch.ToolTip = string.Empty;
        lblSemester.Text = string.Empty;
        lblSemester.ToolTip = string.Empty;
        ddlSemester.SelectedIndex = 0;
        ddlSemester.Enabled = false;
        ddlYear.Enabled = false;
    }

    protected void btnChangeSem_Click(object sender, EventArgs e)
    {
        if (txtEnrollmentNo.Text.Trim().Equals(string.Empty))
        {
            lblMsg.Text = "Please Enter Enrollment No";
            return;
        }

        try
        {
            StudentController objSC = new StudentController();
            Student objStudent = new Student();
            objStudent.IdNo = objSC.GetStudentIDByRegNo(txtEnrollmentNo.Text.Trim());
            objStudent.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            objStudent.Year = Convert.ToInt32(ddlYear.SelectedValue);

            if (objSC.ChangeStudentSemester(objStudent) == Convert.ToInt32(CustomStatus.RecordUpdated))
                lblMsg.Text = "Student Semester Changed to " + ddlSemester.SelectedValue;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_changeStudSem.btnChangeSem_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

            lblMsg.Text = "Student Not Found!!!";
        }
    }
}
