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

public partial class ACADEMIC_Seat_Branch_Allotment : System.Web.UI.Page
{
    Common objCommon = new Common();
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

                //Fill DropDownLists
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
                this.FillDropDownList();
            }

            this.BindBranchListView();
            this.BindBranchListView_MHCET();
        }

        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }
    #region User Defined Methods
    private void FillDropDownList()
    {

        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
        objCommon.FillDropDownList(ddlRound, "ACD_ADMISSION_ROUND", "ADMROUNDNO", "ROUNDNAME", "ADMROUNDNO > 0", "ADMROUNDNO");
        objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
        objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0", "PAYTYPENO");
        objCommon.FillDropDownList(ddlAdmissionType, "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO>0", "IDTYPENO");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentDocVerfication.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentDocVerfication.aspx");
        }
    }
    private void Clear()
    {
        lblStudentname.Text = string.Empty;
        lblMinority.Text = string.Empty;
        lblCategory.Text = string.Empty;
        lblMhcetScore.Text = string.Empty;
        lblPcmSCore.Text = string.Empty;
    }
    private void ShowStudent(long idno)
    {
        try
        {
            StudentController stdController = new StudentController();
            DataSet dsStudent;
            dsStudent = stdController.Getstudent(idno, (Convert.ToInt32(ddlSession.SelectedValue)));
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    ddlRound.SelectedValue = (dsStudent.Tables[0].Rows[0]["ROUNDNO"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["ROUNDNO"].ToString());

                    if (objCommon.LookUp("ACD_BRANCH", "DEGREENO", "BRANCHNO = " + (dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString())) == string.Empty)
                        ddlDegree.SelectedIndex = 0;
                    else
                        ddlDegree.SelectedValue = objCommon.LookUp("ACD_BRANCH", "DEGREENO", "BRANCHNO = " + (dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString()));

                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
                    ddlBranch.SelectedValue = (dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString());

                    //add the fields 20/01/2012
                    ddlAdmissionBatch.SelectedValue = (dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString());
                    ddlPaymentType.SelectedValue = (dsStudent.Tables[0].Rows[0]["PTYPE"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["PTYPE"].ToString());
                    ddlAdmissionType.SelectedValue = (dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString());

                    lblStudentname.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblCategory.Text = dsStudent.Tables[0].Rows[0]["CATEGORY"].ToString();

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
                        lvAdmissionMhcet.Visible = false;
                        lvAdmission.Visible = true;

                    }
                    else if (dsStudent.Tables[0].Rows[0]["AIEEE_SCORE"].ToString() == "0")
                    {
                        lblMhcet.Text = "MHCET SCORE";
                        lblMhcetScore.Text = dsStudent.Tables[0].Rows[0]["MHCET_SCORE"].ToString();
                        lblPcm.Text = "HSC PCM";
                        lblPcmSCore.Text = dsStudent.Tables[0].Rows[0]["HSC_PCM"].ToString();
                        lvAdmission.Visible = false;
                        lvAdmissionMhcet.Visible = true;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updAllot, "This student has not been seat alloted. Please seat allot student.", this.Page);
                    Clear();
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updAllot, "Please Enter Valid Registration No.", this.Page);
                Clear();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Seat_Branch_Allotment.aspx.ShowStudent() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
        ddlBranch.Focus();
    }

    protected void btnAllot_Click(object sender, EventArgs e)
    {
        if (txtRegistrationNo.Text.Trim() == string.Empty ||
        ddlRound.SelectedIndex <= 0 || ddlBranch.SelectedIndex <= 0)
        {
            objCommon.DisplayMessage(this.updAllot, "Please Enter/Select Valid Data!", this.Page);
            return;
        }

        //Check Student Registered or not
        string registered = objCommon.LookUp("ACD_REGISTRATION", "REGISTERED", "SESSIONNO = " + ddlSession.SelectedValue + " AND IDNO = " + txtRegistrationNo.Text.Trim());
        if (!string.IsNullOrEmpty(registered))
        {
            if (registered.ToUpper() == "FALSE")
            {
                txtRegistrationNo.Text = string.Empty;
                objCommon.DisplayMessage(this.updAllot, "This student has not been registered. Please register student from Admission Registration.", this.Page);
                txtRegistrationNo.Focus();
                return;
            }
        }
        else
        {
            txtRegistrationNo.Text = string.Empty;
            objCommon.DisplayMessage(this.updAllot, "This student has not been registered. Please register student from Admission Registration.", this.Page);
            txtRegistrationNo.Focus();
            return;
        }

        //Check whether branch already alloted...
        int branchno = Convert.ToInt32(objCommon.LookUp("ACD_REGISTRATION", "BRANCHNO", "SESSIONNO=" + ddlSession.SelectedValue + " AND IDNO=" + txtRegistrationNo.Text.Trim()));

        if (branchno > 0)
        {
            txtRegistrationNo.Text = string.Empty;
            objCommon.DisplayMessage(this.updAllot, "This student has already been alloted branch.", this.Page);
            txtRegistrationNo.Focus();
            return;
        }


        StudentRegistration objSR = new StudentRegistration();
        CustomStatus cs = (CustomStatus)objSR.AllotBranch(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt64(txtRegistrationNo.Text.Trim()), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlRound.SelectedValue), Convert.ToInt32(ddlAdmissionBatch.SelectedValue), Convert.ToInt32(ddlPaymentType.SelectedValue), Convert.ToInt32(ddlAdmissionType.SelectedValue));
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            txtRegistrationNo.Text = string.Empty;
            ddlBranch.SelectedIndex = 0;
            Clear();

            objCommon.DisplayMessage(this.updAllot, "Branch Alloted Successfully!", this.Page);
            return;

            this.BindBranchListView();
            this.BindBranchListView_MHCET();

        }
        else
            objCommon.DisplayMessage(this.updAllot, "Error!", this.Page);

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void txtRegistrationNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtRegistrationNo.Text.Trim()))
            {
                objCommon.DisplayMessage(this.updAllot, "Please Enter Registration No.", this.Page);
                return;
            }

            long idno = Convert.ToInt64(txtRegistrationNo.Text.Trim());

            //Check Student Registered or not
            string registered = objCommon.LookUp("ACD_REGISTRATION", "REGISTERED", "SESSIONNO = " + ddlSession.SelectedValue + " AND IDNO = " + idno);
            if (!string.IsNullOrEmpty(registered))
            {
                if (registered.ToUpper() == "FALSE")
                {
                    txtRegistrationNo.Text = string.Empty;
                    objCommon.DisplayMessage(this.updAllot, "This student has not been registered. Please register student from Admission Registration.", this.Page);
                    txtRegistrationNo.Focus();
                    return;
                }
            }
            else
            {
                txtRegistrationNo.Text = string.Empty;
                objCommon.DisplayMessage(this.updAllot, "This student has not been registered. Please register student from Admission Registration.", this.Page);
                txtRegistrationNo.Focus();
                return;
            }

            this.ShowStudent(idno);
            this.BindBranchPreference();
            btnAllot.Enabled = true;
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.updAllot, "Please Enter Valid Registration No.!", this.Page);
            Clear();
            txtRegistrationNo.Focus();
        }
    }

    private void BindBranchListView()
    {
        DataSet ds = objCommon.FillDropDown("ACD_BRANCH B LEFT OUTER JOIN ACD_REGISTRATION R ON (R.BRANCHNO = B.BRANCHNO AND ADMSTATUS = 1 AND (ADMCAN IS NULL OR ADMCAN = 0) AND R.QUALIFYNO = 3 AND R.SESSIONNO = " + ddlSession.SelectedValue + ")", "R.BRANCHNO", "B.LONGNAME, B.SHORTNAME, B.AIEEEACTUAL_INTAKE, COUNT(R.IDNO) ADMCOUNT, (B.AIEEEACTUAL_INTAKE - COUNT(R.IDNO)) REMAININGSEAT", "B.BRANCHNO IN (1,2,3,4,5,6,7)  GROUP BY R.BRANCHNO, B.LONGNAME, B.SHORTNAME,B.AIEEEACTUAL_INTAKE UNION ALL SELECT '0' AS BRANCHNO,'0' AS LONGNAME,'Total' AS  SHORTNAME,  (SELECT SUM(ISNULL(AIEEEACTUAL_INTAKE,0)) FROM ACD_BRANCH WHERE DEGREENO = 1) AS AIEEEACTUAL_INTAKE,(SELECT COUNT(IDNO) FROM ACD_REGISTRATION WHERE SESSIONNO = " + ddlSession.SelectedValue + " AND ADMSTATUS = 1 AND (ADMCAN IS NULL OR ADMCAN = 0) AND BRANCHNO IN (1,2,3,4,5,6,7) AND QUALIFYNO = 3) AS ADMCOUNT,((SELECT SUM(ISNULL(AIEEEACTUAL_INTAKE,0)) FROM ACD_BRANCH WHERE DEGREENO = 1) - (SELECT COUNT(IDNO) FROM ACD_REGISTRATION WHERE SESSIONNO = " + ddlSession.SelectedValue + " AND ADMSTATUS = 1 AND (ADMCAN IS NULL OR ADMCAN = 0) AND BRANCHNO IN (1,2,3,4,5,6,7) AND QUALIFYNO = 3)) AS REMAININGSEAT ", "B.LONGNAME DESC");

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvAdmission.DataSource = ds.Tables[0];
                lvAdmission.DataBind();

            }
            else
            {
                lvAdmission.DataSource = null;
                lvAdmission.DataBind();
            }
        }
        else
        {
            lvAdmission.DataSource = null;
            lvAdmission.DataBind();
        }
    }
    private void BindBranchListView_MHCET()
    {
        DataSet ds = objCommon.FillDropDown("ACD_BRANCH B LEFT OUTER JOIN ACD_REGISTRATION R ON (R.BRANCHNO = B.BRANCHNO AND ADMSTATUS = 1 AND (ADMCAN IS NULL OR ADMCAN = 0) AND R.QUALIFYNO = 15 AND R.SESSIONNO = " + ddlSession.SelectedValue + ")", "R.BRANCHNO", "B.LONGNAME, B.SHORTNAME, B.MHCETACTUAL_INTAKE, COUNT(R.IDNO) ADMCOUNT, (B.MHCETACTUAL_INTAKE - COUNT(R.IDNO)) REMAININGSEAT", "B.BRANCHNO IN (1,2,3,4,5,6,7)  GROUP BY R.BRANCHNO, B.LONGNAME, B.SHORTNAME,B.MHCETACTUAL_INTAKE UNION ALL SELECT '0' AS BRANCHNO,'0' AS LONGNAME,'Total' AS  SHORTNAME,  (SELECT SUM(ISNULL(MHCETACTUAL_INTAKE,0)) FROM ACD_BRANCH WHERE DEGREENO = 1) AS MHCETACTUAL_INTAKE,(SELECT COUNT(IDNO) FROM ACD_REGISTRATION WHERE SESSIONNO = " + ddlSession.SelectedValue + " AND ADMSTATUS = 1 AND (ADMCAN IS NULL OR ADMCAN = 0) AND BRANCHNO IN (1,2,3,4,5,6,7) AND QUALIFYNO = 15) AS ADMCOUNT,((SELECT SUM(ISNULL(MHCETACTUAL_INTAKE,0)) FROM ACD_BRANCH WHERE DEGREENO = 1) - (SELECT COUNT(IDNO) FROM ACD_REGISTRATION WHERE SESSIONNO = " + ddlSession.SelectedValue + " AND ADMSTATUS = 1 AND (ADMCAN IS NULL OR ADMCAN = 0) AND BRANCHNO IN (1,2,3,4,5,6,7) AND QUALIFYNO = 15)) AS REMAININGSEAT ", "B.LONGNAME DESC");
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvAdmissionMhcet.DataSource = ds.Tables[0];
                lvAdmissionMhcet.DataBind();
            }
            else
            {
                lvAdmissionMhcet.DataSource = null;
                lvAdmissionMhcet.DataBind();
            }
        }
        else
        {
            lvAdmissionMhcet.DataSource = null;
            lvAdmissionMhcet.DataBind();
        }
    }


    private void BindBranchPreference()
    {
        if (string.IsNullOrEmpty(txtRegistrationNo.Text.Trim()))
        {
            objCommon.DisplayMessage(this.updAllot, "Please Enter Registration No.", this.Page);
            return;
        }

        long idno = Convert.ToInt64(txtRegistrationNo.Text.Trim());

        StudentRegistration objSR = new StudentRegistration();
        DataSet ds = objSR.GetBranchPreferences(Convert.ToInt32(ddlSession.SelectedValue), idno);

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvBranchPref.DataSource = ds.Tables[0];
                lvBranchPref.DataBind();
            }
            else
            {
                lvBranchPref.DataSource = null;
                lvBranchPref.DataBind();
            }
        }
        else
        {
            lvBranchPref.DataSource = null;
            lvBranchPref.DataBind();
        }
    }
}
