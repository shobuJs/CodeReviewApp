//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : COURSE REGISTRATION BY ADMIN                                    
// CREATION DATE : 24-JUNE-2015
// ADDED BY      : MR. MANISH WALDE
// ADDED DATE    : 
// MODIFIED BY   : PRITY KHANDAIT
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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net;

public partial class ACADEMIC_Lockunlockstudentwise : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    CourseController objCourse = new CourseController();
    MarksEntryController objMarksEntry = new MarksEntryController();
    #region Page Load
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
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

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
                ////Page Authorization
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                this.PopulateDropDownList();
                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                string IPADDRESS = string.Empty;

                IPADDRESS = ip.AddressList[0].ToString();
                ViewState["ipAddress"] = IPADDRESS;

            }
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
                Response.Redirect("~/notauthorized.aspx?page=CourseRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CourseRegistration.aspx");
        }
    }


    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }

    protected void rblOptions_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void showdata()
    {
        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");

        if (idno == "")
        {
            objCommon.DisplayMessage(updReg, "Student Not Found for Entered Univ. Reg. No.[" + txtRollNo.Text.Trim() + "]", this.Page);
        }
        else
        {
            ViewState["idno"] = idno;

            if (string.IsNullOrEmpty(ViewState["idno"].ToString()) || ViewState["idno"].ToString() == "0")
            {
                objCommon.DisplayMessage(updReg, "Student with Univ. Reg. No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                return;
            }

            string applyCount = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ACCEPTED = 1 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
            if (applyCount == "0")
            {
                objCommon.DisplayMessage(updReg, "Student with Univ. Reg. No.[" + txtRollNo.Text.Trim() + "] has not registered for selected session. \\nBut you can directly register him.", this.Page);
                lvlockunlock.DataSource = null;
                lvlockunlock.DataBind();
                tblInfo.Visible = false;
            }
            else
            {
                DataSet dsCourse = null;

                dsCourse = objCourse.GetCoursesForLockUnlockStudentwise(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlExamType.SelectedValue), Convert.ToInt32(idno));

                if (dsCourse.Tables.Count > 0 && dsCourse.Tables[0].Rows.Count > 0)
                {
                    tblInfo.Visible = true;
                    lvlockunlock.DataSource = dsCourse;
                    lvlockunlock.DataBind();
                }
                else
                {
                    lvlockunlock.DataSource = null;
                    lvlockunlock.DataBind();
                    tblInfo.Visible = false;
                }




                // }
            }
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            showdata();
        }
        catch (Exception ex)
        { }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["idno"] = "0";
        ddlSession.Enabled = true;
        txtRollNo.Text = string.Empty;
        txtRollNo.Enabled = true;
        ddlSession.SelectedIndex = -1;
        ddlExamType.SelectedIndex = -1;
        lvlockunlock.DataSource = null;
        lvlockunlock.DataBind();
        tblInfo.Visible = false;

    }

    #region Private Methods

    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 ", "SESSIONNO DESC");

        ddlSession.Focus();
    }





    #endregion

    private void ShowReport(string reportTitle, string rptFileName)
    {
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        try
        {

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }




    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnlockunlock_Click(object sender, EventArgs e)
    {
        try
        {
            if (lvlockunlock.Items.Count > 0)
            {
                foreach (ListViewDataItem dataRow in lvlockunlock.Items)
                {
                    UpdateLockStatus(dataRow); //UPDATE ONE BY ONE LOCK STATUS OF RECORD FOR COURSE
                }
                showdata();
            }
            else
            {
                objCommon.DisplayMessage(this.updReg, "No Record found !..", this);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.btnSave_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void UpdateLockStatus(ListViewDataItem dataRow)
    {
        //SAVE THE LOCK STATUS FOR EACH ROW
        try
        {

            int SessionNo = Convert.ToInt32(ddlSession.SelectedValue);

            int examtype = Convert.ToInt32(ddlExamType.SelectedValue);
            string courseno = ((Label)dataRow.FindControl("lblCCode")).ToolTip;

            string schemeno = ((Label)dataRow.FindControl("lblSemester")).ToolTip;
            string semesterno = ((Label)dataRow.FindControl("lblCourseName")).ToolTip;
            int lockunlock = 0;
            bool lockunlock_status = ((CheckBox)dataRow.FindControl("chklock")).Checked; //LOCK UNLOCK STATUS
            if (lockunlock_status == true)
            {
                lockunlock = 1;
            }
            else
                lockunlock = 0;
            string ipAddress = Request.ServerVariables["REMOTE_HOST"];
            if (objMarksEntry.LockUnLockMarkEntryByAdminstudentwise(Convert.ToInt32(SessionNo), Convert.ToInt32(semesterno), Convert.ToInt32(schemeno), Convert.ToInt32(examtype), Convert.ToInt32(courseno), Convert.ToInt32(lockunlock), ipAddress, Convert.ToInt32(Session["userno"]), Convert.ToInt32(ViewState["idno"])) == 1)
            {
                objCommon.DisplayMessage(this.updReg, "Lock/Unlock Done Successfully.", this);
            }
            else
            {
                objCommon.DisplayMessage(this.updReg, "Lock/Unlock Failed..Please Check.", this);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.UpdateLockStatus --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void txtRollNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");

            if (idno == "")
            {
                objCommon.DisplayMessage(updReg, "Student Not Found for Entered Univ. Reg. No.[" + txtRollNo.Text.Trim() + "]", this.Page);
                lvlockunlock.DataSource = null;
                lvlockunlock.DataBind();
                tblInfo.Visible = false;
            }
            if (txtRollNo.Text == "")
            {
                objCommon.DisplayMessage(updReg, "Please Enter the Univ. Reg. No.", this.Page);
                ddlSession.SelectedIndex = -1;
                ddlSession.Focus();
            }

            else
            {
                objCommon.FillDropDownList(ddlExamType, " ACD_SCHEME S INNER JOIN ACD_EXAM_NAME ED ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=(select MAX(schemeno) from acd_student_result WHERE REGNO='" + txtRollNo.Text.Trim() + "'" + ")", "EXAMNO");
            }
        }
        catch (Exception ex)
        {
        }
    }
}


