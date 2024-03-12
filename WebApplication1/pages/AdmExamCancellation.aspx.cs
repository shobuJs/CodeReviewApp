//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : ADMISSION CANCELLATION
// CREATION DATE : 08-11-2015
// CREATED BY    : Renuka H.
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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Academic_AdmExamCancellation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    AdmissionCancellationController admCanController = new AdmissionCancellationController();
    StudentController OBJSC = new StudentController();

    #region Page Events

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
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    //Populate all the DropDownLists

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

                }
            }
            ViewState["IPADDRESS"] = Request.ServerVariables["REMOTE_HOST"];
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_AdmissionCancellation.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT a inner join acd_scheme b on (a.schemeno=b.schemeno) inner join acd_degree c on (a.degreeno=c.degreeno) inner join acd_branch d on (a.branchno=d.branchno) INNER JOIN ACD_COLLEGE_MASTER F ON(A.COLLEGE_ID=F.COLLEGE_ID)", "A.IDNO,a.studname,a.BRANCHNO,a.SCHEMENO", "a.semesterno,a.studentmobile,b.schemename,c.degreename,d.longname,F.COLLEGE_NAME", "regno='" + txtSearchText.Text.ToString() + "'", string.Empty);
            if (ds.Tables[0].Rows.Count > 0)
            {
                divInfo.Visible = true;
                //Show Student Details..
                lblName.Text = ds.Tables[0].Rows[0]["STUDNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                lblName.ToolTip = ds.Tables[0].Rows[0]["idno"] == null ? string.Empty : ds.Tables[0].Rows[0]["IDNO"].ToString();
                lblMobile.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"] == null ? string.Empty : ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();

                lblBranch.Text = ds.Tables[0].Rows[0]["DEGREENAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["DEGREENAME"].ToString();

                lblBranch.Text += ds.Tables[0].Rows[0]["LONGNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["LONGNAME"].ToString();


                lblBranch.ToolTip = ds.Tables[0].Rows[0]["BRANCHNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["BRANCHNO"].ToString();

                lblScheme.Text = ds.Tables[0].Rows[0]["SCHEMENAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["SCHEMENAME"].ToString();


                lblScheme.ToolTip = ds.Tables[0].Rows[0]["SCHEMENO"] == null ? string.Empty : ds.Tables[0].Rows[0]["SCHEMENO"].ToString();

                lblSemester.Text = ds.Tables[0].Rows[0]["SEMESTERNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();

                lblSemester.ToolTip = ds.Tables[0].Rows[0]["SEMESTERNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();


                lblAdmBatch.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();

                lblAdmBatch.ToolTip = ds.Tables[0].Rows[0]["COLLEGE_NAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
            }
            else
            {

                objCommon.DisplayMessage("Student not found", this.Page);
                return;
            }
            DataSet dscourse = objCommon.FillDropDown("acd_student_result", "coursename,SESSIONNO", "ccode", "idno=" + Convert.ToInt32(lblName.ToolTip) + "and semesterno=" + Convert.ToInt32(ds.Tables[0].Rows[0]["SEMESTERNO"]), string.Empty);
            if (dscourse.Tables[0].Rows.Count > 0)
            {
                ViewState["SESSION"] = dscourse.Tables[0].Rows[0]["SESSIONNO"].ToString();
                trcourse.Visible = true; divRemark.Visible = true;
                lvCourseDetails.DataSource = dscourse.Tables[0];
                lvCourseDetails.DataBind();
            }
            else
            {
                trcourse.Visible = false; divRemark.Visible = false;
                lvCourseDetails.DataSource = null;
                lvCourseDetails.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_AdmissionCancellation.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    private void CancelAdmission()
    {
        try
        {
            int studId = (GetViewStateItem("StudentId") != string.Empty ? int.Parse(GetViewStateItem("StudentId")) : 0);
            string remark = "This student's admission has been cancelled by " + Session["userfullname"].ToString();
            remark += " on " + DateTime.Now + ". " + txtRemark.Text.Trim();

            if (admCanController.CancelAdmission(studId, remark))
            {
                ShowMessage("Admission cancelled successfully.");
            }
            else
                ShowMessage("Unable to cancel the student\\'s admission.");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_AdmissionCancellation.CancelAdmission() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
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

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int cs = OBJSC.InsAdmitCanLog(Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ViewState["SESSION"]), Convert.ToInt32(lblName.ToolTip), ViewState["IPADDRESS"].ToString(), Convert.ToInt32(Session["userno"]), txtRemark.Text.ToString());
    }
}