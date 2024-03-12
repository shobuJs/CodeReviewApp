using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
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

public partial class ACADEMIC_Semester_Promotion : System.Web.UI.Page
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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //Populate all the DropDownLists
                FillDropDown();

                ViewState["PROMOTIONNO"] = 0;
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
                Response.Redirect("~/notauthorized.aspx?page=Semester_promotion.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Semester_promotion.aspx");
        }
    }

    public void FillDropDown()
    {
        //fill session
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");

        //fill semester
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNAME");

        //fill section
        objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO>0", "SECTIONNO");

        objCommon.FillDropDownList(ddlSType, "ACD_SCHEMETYPE", "SCHEMETYPENO", "SCHEMETYPE", "SCHEMETYPENO > 0", "SCHEMETYPE");
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        string idno = string.Empty;
        StudentController objSC = new StudentController();

        if (ddlSType.SelectedValue == "1")
        {
            idno = objCommon.LookUp("acd_student", "idno", "REGNO='" + txtEnrollmentNo.Text.Trim() + "'");
        }
        else
        {
            idno = objCommon.LookUp("acd_student", "idno", "REGNO='" + txtEnrollmentNo.Text.Trim() + "'");
        }

        string branch = string.Empty;

        if (idno != "" && idno != null)
        {
            DataSet dtr = objSC.GetStudentDetailsRtm(Convert.ToInt32(idno));
            if (dtr != null)
            {
                infoPnl.Visible = true;

                lblName.ToolTip = dtr.Tables[0].Rows[0]["IDNO"].ToString();
                lblName.Text = dtr.Tables[0].Rows[0]["STUDNAME"] == null ? string.Empty : dtr.Tables[0].Rows[0]["STUDNAME"].ToString();


                branch = Convert.ToString(objCommon.LookUp("acd_branch", "LONGNAME", "BRANCHNO=" + dtr.Tables[0].Rows[0]["BRANCHNO"].ToString()));
                lblBranch.Text = branch.ToString();
                lblBranch.ToolTip = dtr.Tables[0].Rows[0]["BRANCHNO"].ToString();
                ddlSemester.Text = dtr.Tables[0].Rows[0]["semesterno"] == null ? "0" : dtr.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                ddlSection.Text = dtr.Tables[0].Rows[0]["SECTIONNO"] == null ? "0" : dtr.Tables[0].Rows[0]["SECTIONNO"].ToString();
                txtRollNo.Text = dtr.Tables[0].Rows[0]["ROLLNO"] == null ? "0" : dtr.Tables[0].Rows[0]["ROLLNO"].ToString();
                txtRecNo.Text = dtr.Tables[0].Rows[0]["RECEIPTNO"] == null ? "0" : dtr.Tables[0].Rows[0]["RECEIPTNO"].ToString();
                txtRecDate.Text = dtr.Tables[0].Rows[0]["RECEIPT_DATE"] == null ? "0" : dtr.Tables[0].Rows[0]["RECEIPT_DATE"].ToString();
                txtRecAmt.Text = dtr.Tables[0].Rows[0]["RECEIPT_AMOUNT"] == null ? "0" : dtr.Tables[0].Rows[0]["RECEIPT_AMOUNT"].ToString();

                if (dtr.Tables[0].Rows[0]["PROMOTIONNO"].ToString() != "0")
                {
                    pnlStud.Visible = true;
                    lvStudents.DataSource = dtr;
                    lvStudents.DataBind();
                    infoPnl.Visible = false;
                }
                else
                {
                    pnlStud.Visible = false;
                    infoPnl.Visible = true;
                    lvStudents.DataSource = null;
                    lvStudents.DataBind();
                }
            }
            else
            {
                infoPnl.Visible = false;
                objCommon.DisplayMessage("NO DATA FOUND...", this.Page);
            }
        }
        else
        {
            infoPnl.Visible = false;
            objCommon.DisplayMessage("No Student Record found...", this.Page);
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            infoPnl.Visible = true;
            ImageButton editButton = sender as ImageButton;
            int PromotionNo = Int32.Parse(editButton.CommandArgument);
            ViewState["PROMOTIONNO"] = PromotionNo.ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Sem_promotion.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private string GetViewStateItem(string itemName)
    {
        if (ViewState.Count > 0 &&
            ViewState[itemName] != null &&
            ViewState[itemName].ToString() != null &&
            ViewState[itemName].ToString().Trim() != string.Empty)
            return ViewState[itemName].ToString();
        else
            return string.Empty;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        StudentController objSC = new StudentController();
        Student objS = new Student();

        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        objS.IdNo = Convert.ToInt32(lblName.ToolTip);
        objS.BranchNo = Convert.ToInt32(lblBranch.ToolTip);
        objS.SectionNo = Convert.ToInt32(ddlSection.SelectedValue);
        objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        objS.Uano = Convert.ToInt32(Session["userno"]);
        objS.CollegeCode = Session["colcode"].ToString();
        objS.Dob = DateTime.Now;
        if (!txtRollNo.Text.Trim().Equals(string.Empty)) objS.RollNo = txtRollNo.Text.Trim();
        string ipAddress = Request.ServerVariables["REMOTE_HOST"];
        string enroll = txtEnrollmentNo.Text.Trim();
        string recno = txtRecNo.Text.Trim();
        string recdate = txtRecDate.Text;
        string recamt = txtRecAmt.Text;

        int promotion = (GetViewStateItem("PROMOTIONNO") != string.Empty ? int.Parse(GetViewStateItem("PROMOTIONNO")) : 0);

        CustomStatus cs = (CustomStatus)objSC.PromotStudentSemRtm(objS, sessionno, enroll, ipAddress, recno, recdate, recamt, promotion);

        if (cs.Equals(CustomStatus.RecordUpdated))
        {
            objCommon.DisplayMessage("Record is Updated", this.Page);
            clear();
        }
        else
        {
            objCommon.DisplayMessage("Error", this.Page);
        }

    }

    public void clear()
    {
        txtEnrollmentNo.Text = string.Empty;
        lblName.Text = string.Empty;
        lblBranch.Text = string.Empty;
        ddlSection.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        txtRecNo.Text = string.Empty;
        txtRecDate.Text = string.Empty;
        txtRecAmt.Text = string.Empty;
        infoPnl.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        pnlStud.Visible = false;
        ViewState["PROMOTIONNO"] = 0;
    }
}
