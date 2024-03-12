using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Configuration;
using System.Globalization;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;


public partial class InternshipApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentInformation objStudReg = new StudentInformation();
    StudentRegistrationController objStudRegC = new StudentRegistrationController();

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

                //CHECK THE LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]) + " and ua_no=" + Convert.ToInt32(Session["userno"]));
                ViewState["usertype"] = ua_type;
                PopularDropdown();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }
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
                Response.Redirect("~/notauthorized.aspx?page=InternshipApprovel.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=InternshipApprovel.aspx");
        }
    }

    #endregion

    #region Private Methods

    private void PopularDropdown()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");       
    }

    private void ClearControls()
    {
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        lvlInternship.Visible = false;
        btnApprove.Visible = false;
    }

    #endregion

    #region SelectedIndexChanged
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlDegree.SelectedIndex = 0;
            ddlDegree.Focus();
        }

    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            int yearwise = objCommon.LookUp("ACD_DEGREE", "ISNULL(YEARWISE,0)", "DEGREENO=" +ddlDegree.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "ISNULL(YEARWISE,0)", "DEGREENO=" +ddlDegree.SelectedValue));

            if (yearwise == 1)
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");
            }
            else
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT A INNER JOIN ACD_SEMESTER SE ON(A.SEMESTERNO=SE.SEMESTERNO)", "DISTINCT SE.SEMESTERNO", "SE.SEMESTERNAME", "SE.SEMESTERNO>0 AND A.BRANCHNO= " + ddlBranch.SelectedValue, "SE.SEMESTERNO");
            }
            ddlSemester.Focus();
        }
        else
        {
            ddlSemester.Items.Clear();
            ddlBranch.SelectedIndex = 0;
            ddlBranch.Focus();
        }
    }

    #endregion

    #region ButtonClick
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindInternshipDetails();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        bool selection = false;
        int Result = 0;

        foreach (ListViewDataItem lvItem in lvlInternship.Items)
        {
            CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
            int Internno = Convert.ToInt32(((lvItem.FindControl("lblitno") as Label).ToolTip)); ;
            if (chkBox.Checked == true && chkBox.Enabled == true)
            {
                objStudReg.Approved_By = Convert.ToInt32(ViewState["usertype"].ToString());
                objStudReg.Ua_no = Convert.ToInt32(Session["userno"]);
                Result = objStudRegC.ApprovedStudentInternshipDetails(Internno, objStudReg.Approved_By, objStudReg.Ua_no);

                if (Result > 0)
                {
                    objCommon.DisplayMessage(this.updinternship, "Student Approved Successfully.", this.Page);
                    chkBox.Checked = true;
                    chkBox.Enabled = false;
                }
            }
        }
        if (!selection)
        {
            objStudReg.IndustryName = "";
            objStudReg.Degreeno = 0;
            objStudReg.Branchno = 0;
            objCommon.DisplayMessage(this.updinternship, "Please Select atleast one Student from list.", this.Page);
            return;
        }
    }

    private void BindInternshipDetails()
    {
        try
        {
            objStudReg.Degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            objStudReg.Branchno = Convert.ToInt32(ddlBranch.SelectedValue);
            objStudReg.Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);

            DataSet ds = objStudRegC.GetStudentInternshipApprovedDetails(objStudReg.Degreeno, objStudReg.Branchno, objStudReg.Semesterno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvlInternship.Visible = true;
                lvlInternship.DataSource = ds;
                lvlInternship.DataBind();
                btnApprove.Visible = true;

                foreach (ListViewDataItem dataitem in lvlInternship.Items)
                {
                    int Count = 0;
                    CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
                    HiddenField hfidno = dataitem.FindControl("hdfTempIdNo") as HiddenField;
                    Count = Convert.ToInt32((objCommon.LookUp("ACD_STUD_INTERNSHIP_DETAILS", "COUNT(*)", "IDNO=" + hfidno.Value + " AND APPROVED_BY > 0")));
                    if (Count > 0)
                    {
                        cbRow.Enabled = false;
                        cbRow.Checked = true;
                        Count = 0;
                    }
                }
            }
            else
            {
                lvlInternship.Visible = false;
                lvlInternship.DataSource = null;
                lvlInternship.DataBind();
                btnApprove.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Activity.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDegree.SelectedIndex = 0;
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH ACD INNER JOIN ACD_DEGREE D ON (D.DEGREENO=ACD.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENO");
            ddlBranch.SelectedIndex = 0;
        }
    }
}