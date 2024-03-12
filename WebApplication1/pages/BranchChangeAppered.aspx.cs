//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : BRANCH CHANGE APPERED.                                       
// CREATION DATE : 10-JULY-2013 
// ADDED BY      : ASHISH DHAKATE                                                
// ADDED DATE    : 
// MODIFIED BY   : 
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

public partial class ACADEMIC_ExamRegistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();
    StudentController objSC = new StudentController();

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

                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]));
                ViewState["usertype"] = ua_type;

                if (ViewState["usertype"].ToString() == "2")
                {
                    //CHECK ACTIVITY FOR EXAM REGISTRATION
                    //CheckActivity();
                    divCourses.Visible = false;
                    pnlSearch.Visible = false;
                }
                else if (ViewState["usertype"].ToString() == "1")
                {
                    divNote.Visible = false;
                    pnlSearch.Visible = true;
                }
                else
                {
                    pnlStart.Enabled = false;
                }

                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                divNote.Visible = false;
                divCourses.Visible = true;
                ShowDetails();
            }
        }

        divMsg.InnerHtml = string.Empty;
    }

    private void CheckActivity()
    {
        string sessionno = string.Empty;
        sessionno = Session["currentsession"].ToString();
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                pnlStart.Visible = false;

            }

            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                pnlStart.Visible = false;
            }

        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            pnlStart.Visible = false;
        }
        dtr.Close();
    }



    private void FillDropdown()
    {

    }


    #region

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ExamRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ExamRegistration.aspx");
        }
    }
    private void ShowDetails()
    {
        btnReport.Visible = false;
        int idno = 0;
        int sessionno = Convert.ToInt32(Session["currentsession"]);
        StudentController objSC = new StudentController();

        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
        }
        try
        {
            DataSet dsApply = null;
            dsApply = objSC.GetStudentApplyBranch(idno);
            if (dsApply != null && dsApply.Tables[0].Rows.Count > 0)
            {

                if (idno > 0)
                {
                    DataSet dsStudent = objSC.GetStudentDetailsExam(idno);

                    if (dsStudent != null && dsStudent.Tables.Count > 0)
                    {
                        if (dsStudent.Tables[0].Rows.Count > 0)
                        {
                            lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                            lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                            lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                            lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";

                            lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["ENROLLNO"].ToString();
                            lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                            lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                            lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                            lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                            lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                            lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                            lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                            lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                            lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();
                            hdfCategory.Value = dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                            hdfDegreeno.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();

                            imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";

                            objCommon.FillDropDownList(ddlBranchPref1, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + Convert.ToInt32(hdfDegreeno.Value) + " AND BRANCHNO NOT IN('" + Convert.ToInt32(lblBranch.ToolTip) + "')", "BRANCHNO");

                        }
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage("Student Not eligible for the branch change.....", this.Page);
                pnlStart.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("ApplyBranchChange", "rptBranchChangeApply.rpt");
    }

    protected void btnProceed_Click(object sender, EventArgs e)
    {
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {

        int idno = 0;
        idno = Convert.ToInt32(Session["idno"]);

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@UserName=" + Session["username"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    protected void ddlBranchPref2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlBranchPref2.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlBranchPref3, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + Convert.ToInt32(hdfDegreeno.Value) + " AND BRANCHNO NOT IN('" + Convert.ToInt32(lblBranch.ToolTip) + "','" + Convert.ToInt32(ddlBranchPref1.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref2.SelectedValue) + "')", "BRANCHNO");
        }

    }
    protected void ddlBranchPref1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlBranchPref1.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlBranchPref2, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + Convert.ToInt32(hdfDegreeno.Value) + " AND BRANCHNO NOT IN('" + Convert.ToInt32(lblBranch.ToolTip) + "','" + Convert.ToInt32(ddlBranchPref1.SelectedValue) + "')", "BRANCHNO");
        }

    }
    protected void ddlBranchPref3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlBranchPref3.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlBranchPref4, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + Convert.ToInt32(hdfDegreeno.Value) + " AND BRANCHNO NOT IN('" + Convert.ToInt32(lblBranch.ToolTip) + "','" + Convert.ToInt32(ddlBranchPref1.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref2.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref3.SelectedValue) + "')", "BRANCHNO");
        }
    }
    protected void ddlBranchPref4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlBranchPref4.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlBranchPref5, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + Convert.ToInt32(hdfDegreeno.Value) + " AND BRANCHNO NOT IN('" + Convert.ToInt32(lblBranch.ToolTip) + "','" + Convert.ToInt32(ddlBranchPref1.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref2.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref3.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref4.SelectedValue) + "')", "BRANCHNO");
        }
    }
    protected void ddlBranchPref5_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlBranchPref5.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlBranchPref6, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + Convert.ToInt32(hdfDegreeno.Value) + " AND BRANCHNO NOT IN('" + Convert.ToInt32(lblBranch.ToolTip) + "','" + Convert.ToInt32(ddlBranchPref1.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref2.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref3.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref4.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref5.SelectedValue) + "')", "BRANCHNO");
        }
    }
    protected void ddlBranchPref6_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlBranchPref6.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlBranchPref7, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + Convert.ToInt32(hdfDegreeno.Value) + " AND BRANCHNO NOT IN('" + Convert.ToInt32(lblBranch.ToolTip) + "','" + Convert.ToInt32(ddlBranchPref1.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref2.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref3.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref4.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref5.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref6.SelectedValue) + "')", "BRANCHNO");
        }
    }
    protected void ddlBranchPref7_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlBranchPref7.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlBranchPref8, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + Convert.ToInt32(hdfDegreeno.Value) + " AND BRANCHNO NOT IN('" + Convert.ToInt32(lblBranch.ToolTip) + "','" + Convert.ToInt32(ddlBranchPref1.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref2.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref3.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref4.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref5.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref6.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref7.SelectedValue) + "')", "BRANCHNO");
        }
    }
    protected void ddlBranchPref8_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlBranchPref8.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlBranchPref9, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + Convert.ToInt32(hdfDegreeno.Value) + " AND BRANCHNO NOT IN('" + Convert.ToInt32(lblBranch.ToolTip) + "','" + Convert.ToInt32(ddlBranchPref1.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref2.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref3.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref4.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref5.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref6.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref7.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref8.SelectedValue) + "')", "BRANCHNO");
        }
    }
    protected void ddlBranchPref9_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlBranchPref9.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlBranchPref10, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + Convert.ToInt32(hdfDegreeno.Value) + " AND BRANCHNO NOT IN('" + Convert.ToInt32(lblBranch.ToolTip) + "','" + Convert.ToInt32(ddlBranchPref1.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref2.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref3.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref4.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref5.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref6.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref7.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref8.SelectedValue) + "','" + Convert.ToInt32(ddlBranchPref9.SelectedValue) + "')", "BRANCHNO");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        StudentRegistration objSRegist = new StudentRegistration();
        StudentRegist objSR = new StudentRegist();

        int chk = Convert.ToInt32(objCommon.LookUp("ACD_APPLY_BR_CHANGE", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"])));
        if (chk == 0)
        {
            if (Convert.ToInt32(ddlBranchPref1.SelectedValue) > 0)
            {
                objSR.IDNO = Convert.ToInt32(Session["idno"]);
                objSR.BRANCHNO = Convert.ToInt32(lblBranch.ToolTip);

                if (ddlBranchPref1.SelectedValue != "")
                {
                    if (Convert.ToInt32(ddlBranchPref1.SelectedValue) > 0)
                    {
                        objSR.BRANCH_REF = ddlBranchPref1.SelectedValue + ",";
                    }
                }

                if (ddlBranchPref2.SelectedValue != "")
                {
                    if (Convert.ToInt32(ddlBranchPref2.SelectedValue) > 0)
                    {
                        objSR.BRANCH_REF = objSR.BRANCH_REF + ddlBranchPref2.SelectedValue + ",";
                    }
                }

                if (ddlBranchPref3.SelectedValue != "")
                {
                    if (Convert.ToInt32(ddlBranchPref3.SelectedValue) > 0)
                    {
                        objSR.BRANCH_REF = objSR.BRANCH_REF + ddlBranchPref3.SelectedValue + ",";
                    }
                }

                if (ddlBranchPref4.SelectedValue != "")
                {
                    if (Convert.ToInt32(ddlBranchPref4.SelectedValue) > 0)
                    {
                        objSR.BRANCH_REF = objSR.BRANCH_REF + ddlBranchPref4.SelectedValue + ",";
                    }
                }
                if (ddlBranchPref5.SelectedValue != "")
                {
                    if (Convert.ToInt32(ddlBranchPref5.SelectedValue) > 0)
                    {
                        objSR.BRANCH_REF = objSR.BRANCH_REF + ddlBranchPref5.SelectedValue + ",";
                    }
                }
                if (ddlBranchPref6.SelectedValue != "")
                {
                    if (Convert.ToInt32(ddlBranchPref6.SelectedValue) > 0)
                    {
                        objSR.BRANCH_REF = objSR.BRANCH_REF + ddlBranchPref6.SelectedValue + ",";
                    }
                }

                if (ddlBranchPref7.SelectedValue != "")
                {
                    if (Convert.ToInt32(ddlBranchPref7.SelectedValue) > 0)
                    {
                        objSR.BRANCH_REF = objSR.BRANCH_REF + ddlBranchPref7.SelectedValue + ",";
                    }
                }
                if (ddlBranchPref8.SelectedValue != "")
                {
                    if (Convert.ToInt32(ddlBranchPref8.SelectedValue) > 0)
                    {
                        objSR.BRANCH_REF = objSR.BRANCH_REF + ddlBranchPref8.SelectedValue + ",";
                    }
                }
                if (ddlBranchPref9.SelectedValue != "")
                {
                    if (Convert.ToInt32(ddlBranchPref9.SelectedValue) > 0)
                    {
                        objSR.BRANCH_REF = objSR.BRANCH_REF + ddlBranchPref9.SelectedValue + ",";
                    }
                }
                if (ddlBranchPref10.SelectedValue != "")
                {
                    if (Convert.ToInt32(ddlBranchPref10.SelectedValue) > 0)
                    {
                        objSR.BRANCH_REF = objSR.BRANCH_REF + ddlBranchPref10.SelectedValue + ",";
                    }
                }
                objSR.UA_NO = Convert.ToInt32(Session["userno"]);
                objSR.IPADDRESS = ViewState["ipAddress"].ToString();
                objSR.COLLEGE_CODE = Session["colcode"].ToString();

                CustomStatus cs = (CustomStatus)objSRegist.InsertBranchChange(objSR);

                if (cs == CustomStatus.RecordSaved)
                {
                    objCommon.DisplayMessage("Student Apply Successfully!!", this.Page);
                    ShowReport("ApplyBranchChange", "rptBranchChangeApply.rpt");

                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Pref1..", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage("Already apply to the branch change", this.Page);
            btnReport.Visible = true;
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}

