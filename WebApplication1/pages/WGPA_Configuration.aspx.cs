using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Projects_WGPA_Configuration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objEC = new ExamController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
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
                    {
                    }
                    //this.CheckPageAuthorization();
                    ViewState["action"] = "add";
                    FilldropDown();
                    BindWGPARule();

                }
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header

                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACDEMIC_COLLEGE_MASTER.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkStudentUpdate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkStudentUpdate.aspx");
        }
    }

    private void FilldropDown()
    {
        objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVE = 1", "BATCHNO DESC");
        objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "ISNULL(ACTIVE,0)=1", "COLLEGE_NAME");
    }

    protected void BindWGPARule()
    {
        DataSet ds = objCommon.FillDropDown("ACAD_WGPA_RULE", "WGPA_RULE_ID", "WGPA_RULE_NAME,Y1,Y2,Y3,Y4", "", "");

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvWGPARule.DataSource = ds;
            lvWGPARule.DataBind();
        }
        else
        {
            lvWGPARule.DataSource = null;
            lvWGPARule.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string RuleName = txtRuleName.Text;
            decimal y1 = Convert.ToDecimal(txtY1.Text);
            decimal y2 = Convert.ToDecimal(txtY2.Text);
            decimal y3;
            if (txtY3.Text == "")
            {
                y3 = 0;
            }
            else
            {
                y3 = Convert.ToDecimal(txtY3.Text);
            }
          //  decimal y3 = Convert.ToDecimal(txtY3.Text);
            decimal y4;
            if (txtY4.Text == "")
            {
                y4 = 0;
            }
            else
            {
                y4 = Convert.ToDecimal(txtY4.Text);
            }
        //    decimal y4 = Convert.ToDecimal(txtY4.Text);
            int RuleId = 0;
            int ua_no = Convert.ToInt32(Session["userno"]);
            if (ViewState["action"].ToString() == "add")
            {
                RuleId = 0;
            }
            else
            {
                RuleId = Convert.ToInt32(ViewState["WGPA_RULE_ID"]);
            }

            CustomStatus cs = (CustomStatus)objEC.AddWGPAConfiguration(RuleName, y1, y2, y3, y4, ua_no, RuleId);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updRuleName, "WGPA Configuration Details Added Successfully.", this.Page);
                ClearWGPARule();
                BindWGPARule();
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.updRuleName, "WGPA Configuration Details Updated Successfully ", this.Page);
                ViewState["action"] = "add";
                ClearWGPARule();
                BindWGPARule();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "WGPA_Configuration.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ClearWGPARule();
        try
        {
            ImageButton btnEdit = sender as ImageButton;

            int WGPA_RULE_ID = int.Parse(btnEdit.CommandArgument);
            ViewState["WGPA_RULE_ID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(WGPA_RULE_ID);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "WGPA_Configuration.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int WGPA_RULE_ID)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACAD_WGPA_RULE", "WGPA_RULE_ID", "WGPA_RULE_NAME,Y1,Y2,Y3,Y4", "WGPA_RULE_ID = " + WGPA_RULE_ID.ToString() + "", "");

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                txtRuleName.Text = ds.Tables[0].Rows[0]["WGPA_RULE_NAME"].ToString();
                txtY1.Text = ds.Tables[0].Rows[0]["Y1"].ToString();
                txtY2.Text = ds.Tables[0].Rows[0]["Y2"].ToString();
                txtY3.Text = ds.Tables[0].Rows[0]["Y3"].ToString();
                txtY4.Text = ds.Tables[0].Rows[0]["Y4"].ToString();
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "WGPA_Configuration.ShowDetails_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ClearWGPARule()
    {
        txtRuleName.Text = string.Empty;
        txtY1.Text = string.Empty;
        txtY2.Text = string.Empty;
        txtY3.Text = string.Empty;
        txtY4.Text = string.Empty;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearWGPARule();
        ViewState["action"] = "add";
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlFaculty.SelectedIndex > 0)
            {
                ddlIntake.Enabled = true;
                //ddlIntake.SelectedIndex = 0;
                //     objCommon.FillDropDownList(ddlIntake,"ACD_WGPA_RULE_ALLOCATION", "ADMBATCH", "ADMBATCH", "COLLEGE_ID = " + ddlFaculty.SelectedValue + "", "");
                DataSet ds1 = objCommon.FillDropDown("ACD_WGPA_RULE_ALLOCATION", "ADMBATCH", "", "COLLEGE_ID = " + ddlFaculty.SelectedValue + "", "");
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    ddlIntake.SelectedValue = ds1.Tables[0].Rows[0]["ADMBATCH"].ToString();
                    ddlIntake.Enabled = false;
                }
                int CollegeId = Convert.ToInt32(ddlFaculty.SelectedValue);
                DataSet ds = objEC.GetWPGAAllocation(CollegeId);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    lvWGPAAllocation.DataSource = ds;
                    lvWGPAAllocation.DataBind();                 
                    lvWGPAAllocation.Visible = true;
                }
                else
                {
                    lvWGPAAllocation.DataSource = null;
                    lvWGPAAllocation.DataBind();
                    objCommon.DisplayMessage(this.updWGPAAllocation, "Record Not Found!", this.Page);
                }

                DataSet dsRuleName = objCommon.FillDropDown("ACAD_WGPA_RULE", "WGPA_RULE_ID", "WGPA_RULE_NAME,Y1,Y2,Y3,Y4", "", "");
                foreach (ListViewDataItem dataitem in lvWGPAAllocation.Items)
                {

                    DropDownList ddlRuleName = dataitem.FindControl("ddlRuleName") as DropDownList;
                    BindDropDown(ddlRuleName, dsRuleName);
                    Label lblRuleName = dataitem.FindControl("lblRuleName") as Label;
                    ddlRuleName.SelectedValue = lblRuleName.Text;

                }
            }
            else
            {
                lvWGPAAllocation.DataSource = null;
                lvWGPAAllocation.DataBind();
                ddlIntake.SelectedIndex = 0;
                ddlIntake.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "WGPA_Configuration.ddlFaculty_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindDropDown(DropDownList ddlList, DataSet ds)
    {
        ddlList.Items.Clear();
        ddlList.Items.Add("Please Select");
        ddlList.SelectedItem.Value = "0";

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlList.DataSource = ds;
            ddlList.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlList.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlList.DataBind();
            ddlList.SelectedIndex = 0;
        }
        ds = null;
    }
    protected void lnkCSubmit_Click(object sender, EventArgs e)
    {
        lvWGPAAllocation.Visible = true;
        try
        {
            CustomStatus cs = 0; bool count = false;
            foreach (ListViewDataItem dataitem in lvWGPAAllocation.Items)
            {
                DropDownList ddlRuleName = dataitem.FindControl("ddlRuleName") as DropDownList;
                HiddenField hdnProgramId = dataitem.FindControl("hdnProgramId") as HiddenField;
                HiddenField hdnSrNo = dataitem.FindControl("hdnSrNo") as HiddenField;
                if (ddlRuleName.SelectedIndex > 0)
                {
                    count = true;
                    int WGPARuleId = Convert.ToInt32(ddlRuleName.SelectedValue);
                    int AdmBatch = Convert.ToInt32(ddlIntake.SelectedValue);
                    int CollegeId = Convert.ToInt32(ddlFaculty.SelectedValue);

                    string[] program;
                    if (hdnProgramId.Value == "0")
                    {
                        program = "0,0".Split(',');
                    }
                    else
                    {
                        program = hdnProgramId.Value.Split(',');
                    }


                    int DegreeNo = Convert.ToInt32(program[0]);
                    int BranchNo = Convert.ToInt32(program[1]);
                    int AffiliatedNo = Convert.ToInt32(program[2]);

                    int CollegeCode = 52;
                    int ua_no = Convert.ToInt32(Session["userno"]);
                    int SrNo;
                    int SRNO = Convert.ToInt32(hdnSrNo.Value);
                    if (SRNO > 0)
                    {
                        SrNo = Convert.ToInt32(hdnSrNo.Value);
                    }
                    else
                    {
                        SrNo = 0;
                    }
                    cs = (CustomStatus)objEC.AddWGPARuleAllocation(SrNo, AdmBatch, CollegeId, DegreeNo, BranchNo, AffiliatedNo, WGPARuleId, CollegeCode, ua_no);
                }
            }
            if (count == false)
            {
                objCommon.DisplayMessage(this.updWGPAAllocation, "Please Select Atleast Once WGPA Rule.", this.Page);
                return;
            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updWGPAAllocation, "WGPA Rule Allocation Details Added Successfully.", this.Page);
                ClearWGPAAllocation();
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.updWGPAAllocation, "WGPA Rule Allocation Details Updated Successfully ", this.Page);
                ViewState["action"] = "add";
                ClearWGPAAllocation();
            }
            lvWGPAAllocation.Visible = false;

            //lvWGPAAllocation.DataSource = null;
            //lvWGPAAllocation.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "WGPA_Configuration.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ClearWGPAAllocation()
    {
        ddlIntake.SelectedIndex = 0;
        ddlFaculty.SelectedIndex = 0;
        ddlIntake.Enabled = true;
    }

    protected void lnkCCancel_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
        ClearWGPAAllocation();
      
    }
}