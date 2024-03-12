using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;

public partial class ACADEMIC_Scholarship_Application : System.Web.UI.Page
{
    Common objCommon = new Common();
    StudentController objSC = new StudentController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["userno"] == null || Session["username"] == null ||
                  Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null || Session["idno"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Change by Sarang mutkure sir Date 15-11-2023 // 
                    CheckPageAuthorization();
                    if (Convert.ToInt32(Session["usertype"]) == 2)
                    {
                        Response.Redirect("~/notauthorized.aspx");
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlAcademicSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
                    }
                }

                objCommon.SetLabelData("0");
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // dynamic page header addded by sarang 09/07/2021
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {

            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx");
            }
            Common objCommon = new Common();

        }
        else
        {

            Response.Redirect("~/notauthorized.aspx?page=ALCourseList.aspx");
        }
    }

    private void SetInitialRow()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("id", typeof(string)));
            dt.Columns.Add(new DataColumn("ScholarshipType", typeof(string)));
            dt.Columns.Add(new DataColumn("CalculationType", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("CalculationOn", typeof(string)));
            dt.Columns.Add(new DataColumn("FinalAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("RemainingLabAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("RemainingLectureAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("RemainingMiscAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("SCHOL_STATUS", typeof(string)));

            dr = dt.NewRow();
            dr["id"] = 0;
            dr["ScholarshipType"] = string.Empty;
            dr["CalculationType"] = string.Empty;
            dr["Amount"] = string.Empty;
            dr["CalculationOn"] = string.Empty;
            dr["FinalAmount"] = string.Empty;
            dr["RemainingLabAmount"] = string.Empty;
            dr["RemainingLectureAmount"] = string.Empty;
            dr["RemainingMiscAmount"] = string.Empty;
            dr["SCHOL_STATUS"] = 0;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;

            lvScholarshipAmount.DataSource = dt;
            lvScholarshipAmount.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lvStudent.DataSource = null;
            lvStudent.DataBind();

            lvScholarshipAmount.DataSource = null;
            lvScholarshipAmount.DataBind();

            lvBindDetails.DataSource = null;
            lvBindDetails.DataBind();

            ViewState["CurrentTable"] = null;

            if (txtStdID.Text.Trim() == string.Empty)
            {
                divShowDetails.Visible = false;
                objCommon.DisplayMessage(this, "Please Enter Student ID", this.Page);
                return;
            }
            else
            {
                DataSet ds = null;
                if (txtStdID.Text.Trim() != string.Empty)
                {
                    ds = objSC.GetStudentScholarshipDetails(txtStdID.Text.Trim(), Convert.ToInt32(2), 0, Convert.ToInt32(ddlAcademicSession.SelectedValue), 1);

                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count == 0)
                    {
                        divShowDetails.Visible = false;

                        lblName.Text = "";
                        lblbranch.Text = "";
                        lblProgram.Text = "";
                        lblsem.Text = "";

                        lblTotalFees.Text = "";
                        lblLectureFees.Text = "";
                        lblLabFees.Text = "";
                        lblMisFees.Text = "";
                        lblPaidFees.Text = "";

                        ViewState["IDNO"] = null;
                        ViewState["CONCESSION_TYPE"] = null;
                        ViewState["TYPENAME"] = null;
                        ViewState["CALCULATION_NAME"] = null;

                        objCommon.DisplayMessage(this, "Please Enter Valid Student ID", this.Page);
                        return;
                    }
                    if (ds.Tables[6] != null)
                    {
                        if (Convert.ToInt32(ds.Tables[6].Rows[0]["REGISTERED"].ToString()) == 0)
                        {
                            divShowDetails.Visible = false;

                            lblName.Text = "";
                            lblbranch.Text = "";
                            lblProgram.Text = "";
                            lblsem.Text = "";

                            lblTotalFees.Text = "";
                            lblLectureFees.Text = "";
                            lblLabFees.Text = "";
                            lblMisFees.Text = "";
                            lblPaidFees.Text = "";

                            ViewState["IDNO"] = null;
                            ViewState["CONCESSION_TYPE"] = null;
                            ViewState["TYPENAME"] = null;
                            ViewState["CALCULATION_NAME"] = null;

                            objCommon.DisplayMessage(this, "Student Enlistment Not Found !!!", this.Page);
                            return;
                        }
                    }
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        divShowDetails.Visible = true;

                        ViewState["IDNO"] = ds.Tables[1].Rows[0]["IDNO"].ToString();

                        lblName.Text = ds.Tables[1].Rows[0]["STUDNAME"].ToString();
                        lblbranch.Text = ds.Tables[1].Rows[0]["BATCHNAME"].ToString();
                        lblProgram.Text = ds.Tables[1].Rows[0]["DEGREENAME"].ToString();
                        lblsem.Text = ds.Tables[1].Rows[0]["SEMESTERNAME"].ToString();

                        lblTotalFees.Text = ds.Tables[1].Rows[0]["TOTAL_AMT"].ToString();
                        lblLectureFees.Text = ds.Tables[1].Rows[0]["F1"].ToString();
                        lblLabFees.Text = ds.Tables[1].Rows[0]["F2"].ToString();
                        lblMisFees.Text = ds.Tables[1].Rows[0]["MISC_FEE"].ToString();
                        lblPaidFees.Text = ds.Tables[1].Rows[0]["PAID_AMOUNT"].ToString();

                        hfdLectureFees.Value = ds.Tables[1].Rows[0]["F1"].ToString();
                        hdfLabFees.Value = ds.Tables[1].Rows[0]["F2"].ToString();
                        hdfMisFees.Value = ds.Tables[1].Rows[0]["MISC_FEE"].ToString();

                        SetInitialRow();

                        if (ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
                        {
                            lvBindDetails.DataSource = ds.Tables[5];
                            lvBindDetails.DataBind();

                            if (ds.Tables[5].Rows[0]["SCHOL_STATUS"].ToString() == "1")
                            {
                                btnAddRecord.Visible = false;
                                btnSave.Visible = false;
                            }
                            else
                            {
                                btnAddRecord.Visible = true;
                                btnSave.Visible = true;
                            }

                            lvScholarshipAmount.DataSource = ds.Tables[5];
                            lvScholarshipAmount.DataBind();

                            ViewState["CurrentTable"] = ds.Tables[5];
                        }

                        BindScholarshipAmountList(ds);

                        if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                        {
                            ViewState["CONCESSION_TYPE"] = ds.Tables[2];
                        }
                        if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                        {
                            ViewState["TYPENAME"] = ds.Tables[3];
                        }
                        if (ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
                        {
                            ViewState["CALCULATION_NAME"] = ds.Tables[4];
                        }
                    }
                    else
                    {
                        divShowDetails.Visible = false;

                        lblName.Text = "";
                        lblbranch.Text = "";
                        lblProgram.Text = "";
                        lblsem.Text = "";

                        lblTotalFees.Text = "";
                        lblLectureFees.Text = "";
                        lblLabFees.Text = "";
                        lblMisFees.Text = "";
                        lblPaidFees.Text = "";

                        ViewState["CONCESSION_TYPE"] = null;
                        ViewState["TYPENAME"] = null;
                        ViewState["CALCULATION_NAME"] = null;
                        ViewState["IDNO"] = null;

                        objCommon.DisplayMessage(this, "Student Payment Details Not Found !!!", this.Page);
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void lnkUsername_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlAcademicSession.SelectedIndex > 0)
            {
                txtStdID.Text = "";

                lvBindDetails.DataSource = null;
                lvBindDetails.DataBind();

                lvStudent.DataSource = null;
                lvStudent.DataBind();

                lvScholarshipAmount.DataSource = null;
                lvScholarshipAmount.DataBind();

                ViewState["CurrentTable"] = null;

                txtSearchCandidateProgram.Text = "";
                rdselect.SelectedValue = null;
                LinkButton lnkUsername = sender as LinkButton;
                ViewState["IDNO"] = lnkUsername.CommandArgument;

                DataSet ds = null;

                ds = objSC.GetStudentScholarshipDetails("0", Convert.ToInt32(0), int.Parse(lnkUsername.CommandArgument.ToString()), Convert.ToInt32(ddlAcademicSession.SelectedValue), 3);

                if (ds.Tables[6] != null)
                {
                    if (Convert.ToInt32(ds.Tables[6].Rows[0]["REGISTERED"].ToString()) == 0)
                    {
                        divShowDetails.Visible = false;

                        lblName.Text = "";
                        lblbranch.Text = "";
                        lblProgram.Text = "";
                        lblsem.Text = "";

                        lblTotalFees.Text = "";
                        lblLectureFees.Text = "";
                        lblLabFees.Text = "";
                        lblMisFees.Text = "";
                        lblPaidFees.Text = "";

                        ViewState["IDNO"] = null;
                        ViewState["CONCESSION_TYPE"] = null;
                        ViewState["TYPENAME"] = null;
                        ViewState["CALCULATION_NAME"] = null;

                        objCommon.DisplayMessage(this, "Student Enlistment Not Found !!!", this.Page);
                        return;
                    }
                }

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    divShowDetails.Visible = true;

                    lblName.Text = ds.Tables[1].Rows[0]["STUDNAME"].ToString();
                    lblbranch.Text = ds.Tables[1].Rows[0]["BATCHNAME"].ToString();
                    lblProgram.Text = ds.Tables[1].Rows[0]["DEGREENAME"].ToString();
                    lblsem.Text = ds.Tables[1].Rows[0]["SEMESTERNAME"].ToString();

                    lblTotalFees.Text = ds.Tables[1].Rows[0]["TOTAL_AMT"].ToString();
                    lblLectureFees.Text = ds.Tables[1].Rows[0]["F1"].ToString();
                    lblLabFees.Text = ds.Tables[1].Rows[0]["F2"].ToString();
                    lblMisFees.Text = ds.Tables[1].Rows[0]["MISC_FEE"].ToString();
                    lblPaidFees.Text = ds.Tables[1].Rows[0]["PAID_AMOUNT"].ToString();

                    hfdLectureFees.Value = ds.Tables[1].Rows[0]["F1"].ToString();
                    hdfLabFees.Value = ds.Tables[1].Rows[0]["F2"].ToString();
                    hdfMisFees.Value = ds.Tables[1].Rows[0]["MISC_FEE"].ToString();

                    SetInitialRow();

                    if (ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
                    {
                        lvBindDetails.DataSource = ds.Tables[5];
                        lvBindDetails.DataBind();

                        if (ds.Tables[5].Rows[0]["SCHOL_STATUS"].ToString() == "1")
                        {
                            btnAddRecord.Visible = false;
                            btnSave.Visible = false;
                        }
                        else
                        {
                            btnAddRecord.Visible = true;
                            btnSave.Visible = true;
                        }

                        lvScholarshipAmount.DataSource = ds.Tables[5];
                        lvScholarshipAmount.DataBind();

                        ViewState["CurrentTable"] = ds.Tables[5];
                    }

                    BindScholarshipAmountList(ds);

                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        ViewState["CONCESSION_TYPE"] = ds.Tables[2];
                    }
                    if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                    {
                        ViewState["TYPENAME"] = ds.Tables[3];
                    }
                    if (ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
                    {
                        ViewState["CALCULATION_NAME"] = ds.Tables[4];
                    }
                }
                else
                {
                    divShowDetails.Visible = false;

                    lblName.Text = "";
                    lblbranch.Text = "";
                    lblProgram.Text = "";
                    lblsem.Text = "";

                    lblTotalFees.Text = "";
                    lblLectureFees.Text = "";
                    lblLabFees.Text = "";
                    lblMisFees.Text = "";
                    lblPaidFees.Text = "";

                    ViewState["CONCESSION_TYPE"] = null;
                    ViewState["TYPENAME"] = null;
                    ViewState["CALCULATION_NAME"] = null;
                    ViewState["IDNO"] = null;

                    objCommon.DisplayMessage(this, "Student Payment Details Not Found !!!", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "Please Select Academic Session !!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

        }

    }
    protected void btnsearchs_Click(object sender, EventArgs e)
    {
        try
        {
            lvStudent.DataSource = null;
            lvStudent.DataBind();

            lvScholarshipAmount.DataSource = null;
            lvScholarshipAmount.DataBind();

            lvBindDetails.DataSource = null;
            lvBindDetails.DataBind();

            txtStdID.Text = "";
            DataSet ds = null;
            ds = objSC.GetStudentScholarshipDetails(txtSearchCandidateProgram.Text, Convert.ToInt32(rdselect.SelectedValue), 0,Convert.ToInt32(ddlAcademicSession.SelectedValue), 2);

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                divShowDetails.Visible = false;
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
            }
            else
            {
                divShowDetails.Visible = false;
                objCommon.DisplayUserMessage(this.Page, "Record Not Found.", this.Page);
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                ClearModel();
            }
        }
        catch (Exception ex)
        {
            ClearModel();
        }
    }

    protected void btnCancelModal_Click(object sender, EventArgs e)
    {
        ClearModel();
    }
    protected void ClearModel()
    {
        try
        {
            txtSearchCandidateProgram.Text = "";
            rdselect.SelectedValue = null;
            divShowDetails.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    protected void BindScholarshipAmountList(DataSet ds)
    {
        try
        {
            int i = 0;
            foreach (ListViewDataItem dataitem in lvScholarshipAmount.Items)
            {
                DropDownList ddlScholarshipType = dataitem.FindControl("ddlScholarshipType") as DropDownList;
                DropDownList ddlCalculationType = dataitem.FindControl("ddlCalculationType") as DropDownList;
                DropDownList ddlCalculation = dataitem.FindControl("ddlCalculation") as DropDownList;
                TextBox txtValue = dataitem.FindControl("txtValue") as TextBox;
                TextBox txtScholarshipAmt = dataitem.FindControl("txtScholarshipAmt") as TextBox;

                HiddenField hdfRemainingLabAmount = dataitem.FindControl("hdfRemainingLabAmount") as HiddenField;
                HiddenField hdfRemainingLectureAmount = dataitem.FindControl("hdfRemainingLectureAmount") as HiddenField;
                HiddenField hdfRemainingMiscAmount = dataitem.FindControl("hdfRemainingMiscAmount") as HiddenField;
                HiddenField hdfSrno = dataitem.FindControl("hdfSrno") as HiddenField;
                HiddenField hdfScholStatus = dataitem.FindControl("hdfScholStatus") as HiddenField;

                ddlScholarshipType.Items.Clear();
                ddlScholarshipType.Items.Add("Please Select");
                ddlScholarshipType.SelectedItem.Value = "0";

                ddlCalculationType.Items.Clear();
                ddlCalculationType.Items.Add("Please Select");
                ddlCalculationType.SelectedItem.Value = "0";

                ddlCalculation.Items.Clear();
                ddlCalculation.Items.Add("Please Select");
                ddlCalculation.SelectedItem.Value = "0";

                if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    ddlScholarshipType.DataSource = ds.Tables[2];
                    ddlScholarshipType.DataValueField = "CONCESSION_TYPENO";
                    ddlScholarshipType.DataTextField = "CONCESSION_TYPE";
                    ddlScholarshipType.DataBind();
                }
                if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                {
                    ddlCalculationType.DataSource = ds.Tables[3];
                    ddlCalculationType.DataValueField = "TYPENO";
                    ddlCalculationType.DataTextField = "TYPENAME";
                    ddlCalculationType.DataBind();
                }
                if (ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
                {
                    ddlCalculation.DataSource = ds.Tables[4];
                    ddlCalculation.DataValueField = "SRNO";
                    ddlCalculation.DataTextField = "CALCULATION_NAME";
                    ddlCalculation.DataBind();
                }
                if (ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
                {
                    ddlScholarshipType.SelectedValue = ds.Tables[5].Rows[i]["CONCESSION_NO"].ToString();
                    ddlCalculationType.SelectedValue = ds.Tables[5].Rows[i]["TYPENO"].ToString();
                    ddlCalculation.SelectedValue = ds.Tables[5].Rows[i]["CALCULATION_NO"].ToString();
                    txtValue.Text = ds.Tables[5].Rows[i]["AMOUNT_PER"].ToString();
                    txtScholarshipAmt.Text = ds.Tables[5].Rows[i]["SCHOLARSHIP_AMOUNT"].ToString();

                    hdfRemainingLabAmount.Value = ds.Tables[5].Rows[i]["REMAINING_LAB_FEE"].ToString();
                    hdfRemainingLectureAmount.Value = ds.Tables[5].Rows[i]["REMAINING_LECTURE_FEE"].ToString();
                    hdfRemainingMiscAmount.Value = ds.Tables[5].Rows[i]["REMAINING_MISC_FEE"].ToString();
                    hdfSrno.Value = ds.Tables[5].Rows[i]["id"].ToString();
                }
                else
                {
                    ddlScholarshipType.SelectedValue = "0";
                    ddlCalculationType.SelectedValue = "0";
                    ddlCalculation.SelectedValue = "0";
                    txtValue.Text = "";
                    txtScholarshipAmt.Text = "";

                    hdfRemainingLabAmount.Value = "0";
                    hdfRemainingLectureAmount.Value = "0";
                    hdfRemainingMiscAmount.Value = "0";
                    hdfSrno.Value = "0";
                }

                i++;

                if (hdfScholStatus.Value.ToString() == "1")
                {
                    ddlScholarshipType.Enabled = false;
                    ddlCalculationType.Enabled = false;
                    ddlCalculation.Enabled = false;
                    txtValue.Enabled = false;
                    txtScholarshipAmt.Enabled = false;
                }
                else
                {
                    ddlScholarshipType.Enabled = true;
                    ddlCalculationType.Enabled = true;
                    ddlCalculation.Enabled = true;
                    txtValue.Enabled = true;
                    //txtScholarshipAmt.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void ddlCalculationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = sender as DropDownList;

            TextBox txtValue = lvScholarshipAmount.Items[ddl.TabIndex - 1].FindControl("txtValue") as TextBox;
            TextBox txtScholarshipAmt = lvScholarshipAmount.Items[ddl.TabIndex - 1].FindControl("txtScholarshipAmt") as TextBox;
            HiddenField hdfRemainingLabAmount = lvScholarshipAmount.Items[ddl.TabIndex - 1].FindControl("hdfRemainingLabAmount") as HiddenField;
            HiddenField hdfRemainingLectureAmount = lvScholarshipAmount.Items[ddl.TabIndex - 1].FindControl("hdfRemainingLectureAmount") as HiddenField;
            HiddenField hdfRemainingMiscAmount = lvScholarshipAmount.Items[ddl.TabIndex - 1].FindControl("hdfRemainingMiscAmount") as HiddenField;

            txtValue.Text = "";
            txtScholarshipAmt.Text = "";
            hdfRemainingLabAmount.Value = "0";
            hdfRemainingLectureAmount.Value = "0";
            hdfRemainingMiscAmount.Value = "0";
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlScholarshipType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = sender as DropDownList;

            TextBox txtValue = lvScholarshipAmount.Items[ddl.TabIndex - 1].FindControl("txtValue") as TextBox;
            TextBox txtScholarshipAmt = lvScholarshipAmount.Items[ddl.TabIndex - 1].FindControl("txtScholarshipAmt") as TextBox;
            HiddenField hdfRemainingLabAmount = lvScholarshipAmount.Items[ddl.TabIndex - 1].FindControl("hdfRemainingLabAmount") as HiddenField;
            HiddenField hdfRemainingLectureAmount = lvScholarshipAmount.Items[ddl.TabIndex - 1].FindControl("hdfRemainingLectureAmount") as HiddenField;
            HiddenField hdfRemainingMiscAmount = lvScholarshipAmount.Items[ddl.TabIndex - 1].FindControl("hdfRemainingMiscAmount") as HiddenField;
            DropDownList ddlCalculation = lvScholarshipAmount.Items[ddl.TabIndex - 1].FindControl("ddlCalculation") as DropDownList;

            txtValue.Text = "";
            txtScholarshipAmt.Text = "";
            hdfRemainingLabAmount.Value = "0";
            hdfRemainingLectureAmount.Value = "0";
            hdfRemainingMiscAmount.Value = "0";

            if (ddl.SelectedIndex > 0)
            {
                foreach (ListViewDataItem dataitem in lvScholarshipAmount.Items)
                {
                    DropDownList ddlScholarshipType = dataitem.FindControl("ddlScholarshipType") as DropDownList;
                    DropDownList ddlCalculation1 = dataitem.FindControl("ddlCalculation") as DropDownList;

                    if (ddl.TabIndex.ToString() != ddlCalculation1.TabIndex.ToString() && ddlCalculation.SelectedValue.ToString() == ddlCalculation1.SelectedValue.ToString() && ddl.SelectedValue.ToString() == ddlScholarshipType.SelectedValue.ToString())
                    {
                        ddl.SelectedValue = "0";
                        objCommon.DisplayMessage(updScholarship, "Same Scholarship/Discount Type Not Allowed For Same Calculation On !!!", this.Page);
                        return;
                    }
                }

            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlCalculation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = sender as DropDownList;

            TextBox txtValue = lvScholarshipAmount.Items[ddl.TabIndex - 1].FindControl("txtValue") as TextBox;
            TextBox txtScholarshipAmt = lvScholarshipAmount.Items[ddl.TabIndex - 1].FindControl("txtScholarshipAmt") as TextBox;
            HiddenField hdfRemainingLabAmount = lvScholarshipAmount.Items[ddl.TabIndex - 1].FindControl("hdfRemainingLabAmount") as HiddenField;
            HiddenField hdfRemainingLectureAmount = lvScholarshipAmount.Items[ddl.TabIndex - 1].FindControl("hdfRemainingLectureAmount") as HiddenField;
            HiddenField hdfRemainingMiscAmount = lvScholarshipAmount.Items[ddl.TabIndex - 1].FindControl("hdfRemainingMiscAmount") as HiddenField;
            DropDownList ddlScholarshipType = lvScholarshipAmount.Items[ddl.TabIndex - 1].FindControl("ddlScholarshipType") as DropDownList;

            txtValue.Text = "";
            txtScholarshipAmt.Text = "";
            hdfRemainingLabAmount.Value = "0";
            hdfRemainingLectureAmount.Value = "0";
            hdfRemainingMiscAmount.Value = "0";

            if (ddl.SelectedIndex > 0)
            {
                foreach (ListViewDataItem dataitem in lvScholarshipAmount.Items)
                {
                    DropDownList ddlScholarshipType1 = dataitem.FindControl("ddlScholarshipType") as DropDownList;
                    DropDownList ddlCalculation1 = dataitem.FindControl("ddlCalculation") as DropDownList;

                    if (ddl.TabIndex.ToString() != ddlScholarshipType1.TabIndex.ToString() && ddlScholarshipType.SelectedValue.ToString() == ddlScholarshipType1.SelectedValue.ToString() && ddl.SelectedValue.ToString() == ddlCalculation1.SelectedValue.ToString())
                    {
                        ddl.SelectedValue = "0";
                        objCommon.DisplayMessage(updScholarship, "Same Scholarship/Discount Type Not Allowed For Same Calculation On !!!", this.Page);
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void txtValue_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txt = sender as TextBox;

            DropDownList ddlCalculationType = lvScholarshipAmount.Items[txt.TabIndex - 1].FindControl("ddlCalculationType") as DropDownList;
            DropDownList ddlScholarshipType = lvScholarshipAmount.Items[txt.TabIndex - 1].FindControl("ddlScholarshipType") as DropDownList;
            TextBox txtScholarshipAmt = lvScholarshipAmount.Items[txt.TabIndex - 1].FindControl("txtScholarshipAmt") as TextBox;
            HiddenField hdfRemainingLabAmount = lvScholarshipAmount.Items[txt.TabIndex - 1].FindControl("hdfRemainingLabAmount") as HiddenField;
            HiddenField hdfRemainingLectureAmount = lvScholarshipAmount.Items[txt.TabIndex - 1].FindControl("hdfRemainingLectureAmount") as HiddenField;
            HiddenField hdfRemainingMiscAmount = lvScholarshipAmount.Items[txt.TabIndex - 1].FindControl("hdfRemainingMiscAmount") as HiddenField;
            DropDownList ddlCalculation = lvScholarshipAmount.Items[txt.TabIndex - 1].FindControl("ddlCalculation") as DropDownList;

            decimal Amount = 0; decimal RemainingAmount = 0; decimal RemainingLecAmount = 0; decimal RemainingMiscAmount = 0;

            txtScholarshipAmt.Text = "";
            if (ddlCalculationType.SelectedIndex > 0)
            {
                if (ddlScholarshipType.SelectedIndex > 0)
                {
                    if (ddlCalculationType.SelectedIndex == 1)
                    {
                        if (Convert.ToDecimal(txt.Text.Trim()) > 100)
                        {
                            txt.Text = "";
                            objCommon.DisplayMessage(updScholarship, "Percentage can not more than 100% !!!", this.Page);
                            return;
                        }
                        if (Convert.ToDecimal(txt.Text.Trim()) == 0)
                        {
                            txt.Text = "";
                            objCommon.DisplayMessage(updScholarship, "Percentage should be greater than 0% !!!", this.Page);
                            return;
                        }
                    }
                    else if (ddlCalculationType.SelectedIndex == 2)
                    {
                        if (Convert.ToDecimal(txt.Text.Trim()) == 0)
                        {
                            txt.Text = "";
                            objCommon.DisplayMessage(updScholarship, "Amount should be greater than zero !!!", this.Page);
                            return;
                        }
                    }
                    if (ddlCalculation.SelectedIndex > 0)
                    {
                        bool labLoop = false; bool LecLoop = false; bool MiscLoop = false;

                        foreach (ListViewDataItem dataitem in lvScholarshipAmount.Items)
                        {
                            TextBox txtValue = dataitem.FindControl("txtValue") as TextBox;
                            DropDownList ddlCalculationType1 = dataitem.FindControl("ddlCalculationType") as DropDownList;
                            TextBox txtScholarshipAmt1 = dataitem.FindControl("txtScholarshipAmt") as TextBox;
                            HiddenField hdfRemainingLabAmount1 = dataitem.FindControl("hdfRemainingLabAmount") as HiddenField;
                            HiddenField hdfRemainingLectureAmount1 = dataitem.FindControl("hdfRemainingLectureAmount") as HiddenField;
                            HiddenField hdfRemainingMiscAmount1 = dataitem.FindControl("hdfRemainingMiscAmount") as HiddenField;
                            DropDownList ddlCalculation1 = dataitem.FindControl("ddlCalculation") as DropDownList;

                            if (ddlCalculation1.SelectedIndex == 1)
                            {
                                //if (txtValue.TabIndex.ToString() != txt.TabIndex.ToString())
                                //{
                                if (ddlCalculationType1.SelectedIndex == 1)
                                {
                                    if (Convert.ToDecimal(hdfLabFees.Value) == 0)
                                    {
                                        objCommon.DisplayMessage(updScholarship, "Scholarship is not applicable as lab fee is not available !!!", this.Page);
                                        return;
                                    }
                                    else if (labLoop == false)
                                    {
                                        labLoop = true;

                                        Amount = (Convert.ToDecimal(hdfLabFees.Value) * Convert.ToDecimal(txtValue.Text.Trim())) / 100;

                                        txtScholarshipAmt1.Text = Amount.ToString("0.00");
                                        RemainingAmount = Convert.ToDecimal(hdfLabFees.Value) - Amount;
                                        hdfRemainingLabAmount1.Value = RemainingAmount.ToString();
                                    }
                                    else
                                    {
                                        if (Convert.ToDecimal(RemainingAmount) == 0)
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "No Lab Fees Remaining !!!", this.Page);
                                            return;
                                        }
                                        Amount = (Convert.ToDecimal(RemainingAmount) * Convert.ToDecimal(txtValue.Text.Trim())) / 100;

                                        if (Convert.ToDecimal(Amount) > Convert.ToDecimal(RemainingAmount))
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "Amount can not be greater than remaining amount " + RemainingAmount + " !!!", this.Page);
                                            return;
                                        }
                                        else if (Amount.ToString() == "0" || Amount.ToString() == "0.00")
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "No Lab Fees Remaining !!!", this.Page);
                                            return;
                                        }
                                        txtScholarshipAmt1.Text = Amount.ToString("0.00");
                                        RemainingAmount = Convert.ToDecimal(RemainingAmount) - Amount;
                                        hdfRemainingLabAmount1.Value = RemainingAmount.ToString();
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(hdfLabFees.Value) == 0)
                                    {
                                        objCommon.DisplayMessage(updScholarship, "Scholarship is not applicable as lab fee is not available !!!", this.Page);
                                        return;
                                    }
                                    else if (labLoop == false)
                                    {
                                        labLoop = true;

                                        Amount = (Convert.ToDecimal(hdfLabFees.Value) - Convert.ToDecimal(txtValue.Text.Trim()));

                                        if (Convert.ToDecimal(txtValue.Text.Trim()) > Convert.ToDecimal(hdfLabFees.Value))
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "Amount can not be greater than remaining amount " + Convert.ToString(hdfLabFees.Value) + " !!!", this.Page);
                                            return;
                                        }
                                        txtScholarshipAmt1.Text = Convert.ToDecimal(txtValue.Text).ToString("0.00");
                                        RemainingAmount = Amount;
                                        hdfRemainingLabAmount1.Value = RemainingAmount.ToString();
                                    }
                                    else
                                    {
                                        if (Convert.ToDecimal(RemainingAmount) == 0)
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "No Lab Fees Remaining !!!", this.Page);
                                            return;
                                        }
                                        Amount = (Convert.ToDecimal(RemainingAmount) - Convert.ToDecimal(txtValue.Text.Trim()));

                                        if (Convert.ToDecimal(txtValue.Text.Trim()) > Convert.ToDecimal(RemainingAmount))
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "Amount can not be greater than remaining amount " + RemainingAmount + " !!!", this.Page);
                                            return;
                                        }
                                        else if (Amount.ToString().Substring(0, 1) == "-")
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "No Lab Fees Remaining !!!", this.Page);
                                            return;
                                        }

                                        txtScholarshipAmt1.Text = Convert.ToDecimal(txtValue.Text).ToString("0.00");
                                        RemainingAmount = Amount;
                                        hdfRemainingLabAmount1.Value = RemainingAmount.ToString();
                                    }
                                }
                            }
                            if (ddlCalculation1.SelectedIndex == 2)
                            {
                                //if (txtValue.TabIndex.ToString() != txt.TabIndex.ToString())
                                //{
                                if (ddlCalculationType1.SelectedIndex == 1)
                                {
                                    if (Convert.ToDecimal(hfdLectureFees.Value) == 0)
                                    {
                                        objCommon.DisplayMessage(updScholarship, "Scholarship is not applicable as lecture fee is not available !!!", this.Page);
                                        return;
                                    }
                                    else if (LecLoop == false)
                                    {
                                        LecLoop = true;

                                        Amount = (Convert.ToDecimal(hfdLectureFees.Value) * Convert.ToDecimal(txtValue.Text.Trim())) / 100;

                                        txtScholarshipAmt1.Text = Amount.ToString("0.00");
                                        RemainingLecAmount = Convert.ToDecimal(hfdLectureFees.Value) - Amount;
                                        hdfRemainingLectureAmount1.Value = RemainingLecAmount.ToString();
                                    }
                                    else
                                    {
                                        if (Convert.ToDecimal(RemainingLecAmount) == 0)
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "No Lecture Fees Remaining !!!", this.Page);
                                            return;
                                        }

                                        Amount = (Convert.ToDecimal(RemainingLecAmount) * Convert.ToDecimal(txtValue.Text.Trim())) / 100;

                                        if (Convert.ToDecimal(Amount) > Convert.ToDecimal(RemainingLecAmount))
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "Amount can not be greater than remaining amount " + RemainingLecAmount + " !!!", this.Page);
                                            return;
                                        }
                                        else if (Amount.ToString() == "0" || Amount.ToString() == "0.00")
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "No Lecture Fees Remaining !!!", this.Page);
                                            return;
                                        }
                                        txtScholarshipAmt1.Text = Amount.ToString("0.00");
                                        RemainingLecAmount = Convert.ToDecimal(RemainingLecAmount) - Amount;
                                        hdfRemainingLectureAmount1.Value = RemainingLecAmount.ToString();
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(hfdLectureFees.Value) == 0)
                                    {
                                        objCommon.DisplayMessage(updScholarship, "Scholarship is not applicable as lecture fee is not available !!!", this.Page);
                                        return;
                                    }
                                    else if (LecLoop == false)
                                    {
                                        LecLoop = true;

                                        Amount = (Convert.ToDecimal(hfdLectureFees.Value) - Convert.ToDecimal(txtValue.Text.Trim()));

                                        if (Convert.ToDecimal(txtValue.Text.Trim()) > Convert.ToDecimal(hfdLectureFees.Value))
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "Amount can not be greater than remaining amount " + Convert.ToString(hfdLectureFees.Value) + " !!!", this.Page);
                                            return;
                                        }
                                        txtScholarshipAmt1.Text = Convert.ToDecimal(txtValue.Text).ToString("0.00");
                                        RemainingLecAmount = Amount;
                                        hdfRemainingLectureAmount1.Value = RemainingLecAmount.ToString();
                                    }
                                    else
                                    {
                                        if (Convert.ToDecimal(RemainingLecAmount) == 0)
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "No Lecture Fees Remaining !!!", this.Page);
                                            return;
                                        }

                                        Amount = (Convert.ToDecimal(RemainingLecAmount) - Convert.ToDecimal(txtValue.Text.Trim()));

                                        if (Convert.ToDecimal(txtValue.Text.Trim()) > Convert.ToDecimal(RemainingLecAmount))
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "Amount can not be greater than remaining amount " + RemainingLecAmount + " !!!", this.Page);
                                            return;
                                        }
                                        else if (Amount.ToString().Substring(0, 1) == "-")
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "No Lecture Fees Remaining !!!", this.Page);
                                            return;
                                        }

                                        txtScholarshipAmt1.Text = Convert.ToDecimal(txtValue.Text).ToString("0.00");
                                        RemainingLecAmount = Amount;
                                        hdfRemainingLectureAmount1.Value = RemainingLecAmount.ToString();
                                    }
                                }
                            }
                            if (ddlCalculation1.SelectedIndex == 3)
                            {
                                //if (txtValue.TabIndex.ToString() != txt.TabIndex.ToString())
                                //{
                                if (ddlCalculationType1.SelectedIndex == 1)
                                {
                                    if (Convert.ToDecimal(hdfMisFees.Value) == 0)
                                    {
                                        objCommon.DisplayMessage(updScholarship, "Scholarship is not applicable as misc fee is not available !!!", this.Page);
                                        return;
                                    }
                                    else if (MiscLoop == false)
                                    {
                                        MiscLoop = true;

                                        Amount = (Convert.ToDecimal(hdfMisFees.Value) * Convert.ToDecimal(txtValue.Text.Trim())) / 100;

                                        txtScholarshipAmt1.Text = Amount.ToString("0.00");
                                        RemainingMiscAmount = Convert.ToDecimal(hdfMisFees.Value) - Amount;
                                        hdfRemainingMiscAmount1.Value = RemainingMiscAmount.ToString();
                                    }
                                    else
                                    {
                                        if (Convert.ToDecimal(RemainingMiscAmount) == 0)
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "No Misc Fees Remaining !!!", this.Page);
                                            return;
                                        }

                                        Amount = (Convert.ToDecimal(RemainingMiscAmount) * Convert.ToDecimal(txtValue.Text.Trim())) / 100;

                                        if (Convert.ToDecimal(Amount) > Convert.ToDecimal(RemainingMiscAmount))
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "Amount can not be greater than remaining amount " + RemainingMiscAmount + " !!!", this.Page);
                                            return;
                                        }
                                        else if (Amount.ToString() == "0" || Amount.ToString() == "0.00")
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "No Misc Fees Remaining !!!", this.Page);
                                            return;
                                        }
                                        txtScholarshipAmt1.Text = Amount.ToString("0.00");
                                        RemainingMiscAmount = Convert.ToDecimal(RemainingMiscAmount) - Amount;
                                        hdfRemainingMiscAmount1.Value = RemainingMiscAmount.ToString();
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(hdfMisFees.Value) == 0)
                                    {
                                        objCommon.DisplayMessage(updScholarship, "Scholarship is not applicable as misc fee is not available !!!", this.Page);
                                        return;
                                    }
                                    else if (MiscLoop == false)
                                    {
                                        MiscLoop = true;

                                        Amount = (Convert.ToDecimal(hdfMisFees.Value) - Convert.ToDecimal(txtValue.Text.Trim()));

                                        if (Convert.ToDecimal(txtValue.Text.Trim()) > Convert.ToDecimal(hdfMisFees.Value))
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "Amount can not be greater than remaining amount " + Convert.ToString(hdfMisFees.Value) + " !!!", this.Page);
                                            return;
                                        }
                                        txtScholarshipAmt1.Text = Convert.ToDecimal(txtValue.Text).ToString("0.00");
                                        RemainingMiscAmount = Amount;
                                        hdfRemainingMiscAmount1.Value = RemainingMiscAmount.ToString();
                                    }
                                    else
                                    {
                                        if (Convert.ToDecimal(RemainingMiscAmount) == 0)
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "No Misc Fees Remaining !!!", this.Page);
                                            return;
                                        }

                                        Amount = (Convert.ToDecimal(RemainingMiscAmount) - Convert.ToDecimal(txtValue.Text.Trim()));

                                        if (Convert.ToDecimal(txtValue.Text.Trim()) > Convert.ToDecimal(RemainingMiscAmount))
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "Amount can not be greater than remaining amount " + RemainingMiscAmount + " !!!", this.Page);
                                            return;
                                        }
                                        else if (Amount.ToString().Substring(0, 1) == "-")
                                        {
                                            txtValue.Text = ""; txtScholarshipAmt1.Text = "";
                                            objCommon.DisplayMessage(updScholarship, "No Misc Fees Remaining !!!", this.Page);
                                            return;
                                        }

                                        txtScholarshipAmt1.Text = Convert.ToDecimal(txtValue.Text).ToString("0.00");
                                        RemainingMiscAmount = Amount;
                                        hdfRemainingMiscAmount1.Value = RemainingMiscAmount.ToString();
                                    }
                                }
                            }
                                //}
                                //else
                                //{
                                //    if (ddlCalculationType1.SelectedIndex == 1)
                                //    {
                                //        if (hdfRemainingLabAmount1.Value.ToString() == "0" || hdfRemainingLabAmount1.Value.ToString() == "0.00")
                                //        {
                                //            Amount = (Convert.ToDecimal(hdfLabFees.Value) * Convert.ToDecimal(txtValue.Text.Trim())) / 100;

                                //            txtScholarshipAmt1.Text = Amount.ToString("0.00");
                                //            RemainingAmount = Convert.ToDecimal(hdfLabFees.Value) - Amount;
                                //            hdfRemainingLabAmount1.Value = RemainingAmount.ToString();
                                //        }
                                //        else
                                //        {
                                //            Amount = (Convert.ToDecimal(hdfRemainingLabAmount1.Value) * Convert.ToDecimal(txtValue.Text.Trim())) / 100;

                                //            txtScholarshipAmt1.Text = Amount.ToString("0.00");
                                //            RemainingAmount = Convert.ToDecimal(hdfRemainingLabAmount1.Value) - Amount;
                                //            hdfRemainingLabAmount1.Value = RemainingAmount.ToString();
                                //        }
                                //    }
                                //}
                            
                    //        if (ddlCalculation.SelectedIndex == 1)
                    //        {
                    //            if (txtValue.TabIndex.ToString() != txt.TabIndex.ToString())
                    //            {
                    //                if (ddlCalculationType.SelectedIndex == 1)
                    //                {
                    //                    if (hdfRemainingLabAmount1.Value.ToString() == "0" || hdfRemainingLabAmount1.Value.ToString() == "0.00")
                    //                    {
                    //                        Amount = (Convert.ToDecimal(hdfLabFees.Value) * Convert.ToDecimal(txt.Text.Trim())) / 100;

                    //                        txtScholarshipAmt.Text = Amount.ToString("0.00");
                    //                        RemainingAmount = Convert.ToDecimal(hdfLabFees.Value) - Amount;
                    //                        hdfRemainingLabAmount.Value = RemainingAmount.ToString();
                    //                    }
                    //                    else
                    //                    {
                    //                        Amount = (Convert.ToDecimal(hdfRemainingLabAmount1.Value) * Convert.ToDecimal(txt.Text.Trim())) / 100;

                    //                        txtScholarshipAmt.Text = Amount.ToString("0.00");
                    //                        RemainingAmount = Convert.ToDecimal(hdfRemainingLabAmount1.Value) - Amount;
                    //                        hdfRemainingLabAmount.Value = RemainingAmount.ToString();
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    if (hdfRemainingLabAmount1.Value.ToString() == "0" || hdfRemainingLabAmount1.Value.ToString() == "0.00")
                    //                    {
                    //                        Amount = (Convert.ToDecimal(hdfLabFees.Value) - Convert.ToDecimal(txt.Text.Trim()));

                    //                        txtScholarshipAmt.Text = txt.Text.Trim();
                    //                        RemainingAmount = Amount;
                    //                        hdfRemainingLabAmount.Value = RemainingAmount.ToString();
                    //                    }
                    //                    else
                    //                    {
                    //                        if (Convert.ToDecimal(txt.Text.Trim()) > Convert.ToDecimal(hdfRemainingLabAmount1.Value))
                    //                        {
                    //                            txt.Text = ""; txtScholarshipAmt.Text = "";
                    //                            objCommon.DisplayMessage(updScholarship, "Enter value can not greater than remaining amount !!!", this.Page);
                    //                            return;
                    //                        }
                    //                        else
                    //                        {
                    //                            Amount = (Convert.ToDecimal(hdfRemainingLabAmount1.Value) - Convert.ToDecimal(txt.Text.Trim()));

                    //                            txtScholarshipAmt.Text = txt.Text.Trim();
                    //                            RemainingAmount = Amount;
                    //                            hdfRemainingLabAmount.Value = RemainingAmount.ToString();
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                //if (Convert.ToString(hdfRemainingLabAmount1.Value) == "0" || Convert.ToString(hdfRemainingLabAmount1.Value) == "0.00" || Convert.ToString(hdfRemainingLabAmount1.Value) == string.Empty)
                    //                //{
                    //                if (lvScholarshipAmount.Items.Count == 1)
                    //                {
                    //                    if (ddlCalculationType.SelectedIndex == 1)
                    //                    {
                    //                        Amount = (Convert.ToDecimal(hdfLabFees.Value) * Convert.ToDecimal(txt.Text.Trim())) / 100;

                    //                        txtScholarshipAmt.Text = Amount.ToString("0.00");
                    //                        RemainingAmount = Convert.ToDecimal(hdfLabFees.Value) - Amount;
                    //                        hdfRemainingLabAmount.Value = RemainingAmount.ToString();
                    //                    }
                    //                    else
                    //                    {
                    //                        if (Convert.ToDecimal(txt.Text.Trim()) > Convert.ToDecimal(hdfLabFees.Value))
                    //                        {
                    //                            txt.Text = ""; txtScholarshipAmt.Text = "";
                    //                            objCommon.DisplayMessage(updScholarship, "Enter value can not greater than remaining amount !!!", this.Page);
                    //                            return;
                    //                        }
                    //                        else
                    //                        {
                    //                            Amount = (Convert.ToDecimal(hdfLabFees.Value) - Convert.ToDecimal(txt.Text.Trim()));

                    //                            txtScholarshipAmt.Text = txt.Text.Trim();
                    //                            RemainingAmount = Amount;
                    //                            hdfRemainingLabAmount.Value = RemainingAmount.ToString();
                    //                        }
                    //                    }
                    //                }
                    //                else
                    //                {

                    //                    if (hdfRemainingLabAmount.Value.ToString() == "0" || hdfRemainingLabAmount.Value.ToString() == "0.00")
                    //                    {
                    //                        if (Convert.ToInt32(txt.TabIndex) > 1)
                    //                        {
                    //                            if (ddlCalculationType.SelectedIndex == 1)
                    //                            {
                    //                                Amount = (Convert.ToDecimal(hdfLabFees.Value) * Convert.ToDecimal(txt.Text.Trim())) / 100;

                    //                                txtScholarshipAmt.Text = Amount.ToString("0.00");
                    //                                RemainingAmount = Convert.ToDecimal(hdfLabFees.Value) - Amount;
                    //                                hdfRemainingLabAmount.Value = RemainingAmount.ToString();
                    //                            }
                    //                            else
                    //                            {
                    //                                if (Convert.ToDecimal(txt.Text.Trim()) > Convert.ToDecimal(hdfLabFees.Value))
                    //                                {
                    //                                    txt.Text = ""; txtScholarshipAmt.Text = "";
                    //                                    objCommon.DisplayMessage(updScholarship, "Enter value can not greater than remaining amount !!!", this.Page);
                    //                                    return;
                    //                                }
                    //                                else
                    //                                {
                    //                                    Amount = (Convert.ToDecimal(hdfLabFees.Value) - Convert.ToDecimal(txt.Text.Trim()));

                    //                                    txtScholarshipAmt.Text = txt.Text.Trim();
                    //                                    RemainingAmount = Amount;
                    //                                    hdfRemainingLabAmount.Value = RemainingAmount.ToString();
                    //                                }
                    //                            }
                    //                        }
                    //                        else
                    //                        {
                    //                            txt.Text = ""; txtScholarshipAmt.Text = "";
                    //                            objCommon.DisplayMessage(updScholarship, "No Lab Fees Remaining !!!", this.Page);
                    //                            return;
                    //                        }
                    //                    }
                    //                }
                    //                //}
                    //            }
                    //        }
                    //        else if (ddlCalculation.SelectedIndex == 2)
                    //        {
                    //            if (txtValue.TabIndex.ToString() != txt.TabIndex.ToString())
                    //            {
                    //                if (ddlCalculationType.SelectedIndex == 1)
                    //                {
                    //                    if (hdfRemainingLectureAmount1.Value.ToString() == "0" || hdfRemainingLectureAmount1.Value.ToString() == "0.00")
                    //                    {
                    //                        Amount = (Convert.ToDecimal(hfdLectureFees.Value) * Convert.ToDecimal(txt.Text.Trim())) / 100;

                    //                        txtScholarshipAmt.Text = Amount.ToString("0.00");
                    //                        RemainingAmount = Convert.ToDecimal(hfdLectureFees.Value) - Amount;
                    //                        hdfRemainingLectureAmount.Value = RemainingAmount.ToString();
                    //                    }
                    //                    else
                    //                    {
                    //                        Amount = (Convert.ToDecimal(hdfRemainingLectureAmount1.Value) * Convert.ToDecimal(txt.Text.Trim())) / 100;

                    //                        txtScholarshipAmt.Text = Amount.ToString("0.00");
                    //                        RemainingAmount = Convert.ToDecimal(hdfRemainingLectureAmount1.Value) - Amount;
                    //                        hdfRemainingLectureAmount.Value = RemainingAmount.ToString();
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    if (hdfRemainingLectureAmount1.Value.ToString() == "0" || hdfRemainingLectureAmount1.Value.ToString() == "0.00")
                    //                    {
                    //                        Amount = (Convert.ToDecimal(hfdLectureFees.Value) - Convert.ToDecimal(txt.Text.Trim()));

                    //                        txtScholarshipAmt.Text = txt.Text.Trim();
                    //                        RemainingAmount = Amount;
                    //                        hdfRemainingLectureAmount.Value = RemainingAmount.ToString();
                    //                    }
                    //                    else
                    //                    {
                    //                        if (txtValue.TabIndex.ToString() == txt.TabIndex.ToString())
                    //                        {
                    //                            if (Convert.ToDecimal(txt.Text.Trim()) > Convert.ToDecimal(hdfRemainingLectureAmount1.Value))
                    //                            {
                    //                                txt.Text = ""; txtScholarshipAmt.Text = "";
                    //                                objCommon.DisplayMessage(updScholarship, "Enter value can not greater than remaining amount !!!", this.Page);
                    //                                return;
                    //                            }
                    //                        }
                    //                        else
                    //                        {
                    //                            Amount = (Convert.ToDecimal(hdfRemainingLectureAmount1.Value) - Convert.ToDecimal(txt.Text.Trim()));

                    //                            txtScholarshipAmt.Text = txt.Text.Trim();
                    //                            RemainingAmount = Amount;
                    //                            hdfRemainingLectureAmount.Value = RemainingAmount.ToString();
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (lvScholarshipAmount.Items.Count == 1)
                    //                {
                    //                    if (ddlCalculationType.SelectedIndex == 1)
                    //                    {
                    //                        Amount = (Convert.ToDecimal(hfdLectureFees.Value) * Convert.ToDecimal(txt.Text.Trim())) / 100;

                    //                        txtScholarshipAmt.Text = Amount.ToString("0.00");
                    //                        RemainingAmount = Convert.ToDecimal(hfdLectureFees.Value) - Amount;
                    //                        hdfRemainingLectureAmount.Value = RemainingAmount.ToString();
                    //                    }
                    //                    else
                    //                    {
                    //                        if (Convert.ToDecimal(txt.Text.Trim()) > Convert.ToDecimal(hfdLectureFees.Value))
                    //                        {
                    //                            txt.Text = ""; txtScholarshipAmt.Text = "";
                    //                            objCommon.DisplayMessage(updScholarship, "Enter value can not greater than remaining amount !!!", this.Page);
                    //                            return;
                    //                        }
                    //                        else
                    //                        {
                    //                            Amount = (Convert.ToDecimal(hfdLectureFees.Value) - Convert.ToDecimal(txt.Text.Trim()));

                    //                            txtScholarshipAmt.Text = txt.Text.Trim();
                    //                            RemainingAmount = Amount;
                    //                            hdfRemainingLectureAmount.Value = RemainingAmount.ToString();
                    //                        }
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    if (hdfRemainingLectureAmount.Value.ToString() == "0" || hdfRemainingLectureAmount.Value.ToString() == "0.00")
                    //                    {
                    //                        if (Convert.ToInt32(txt.TabIndex) > 1)
                    //                        {
                    //                            if (ddlCalculationType.SelectedIndex == 1)
                    //                            {
                    //                                Amount = (Convert.ToDecimal(hfdLectureFees.Value) * Convert.ToDecimal(txt.Text.Trim())) / 100;

                    //                                txtScholarshipAmt.Text = Amount.ToString("0.00");
                    //                                RemainingAmount = Convert.ToDecimal(hfdLectureFees.Value) - Amount;
                    //                                hdfRemainingLectureAmount.Value = RemainingAmount.ToString();
                    //                            }
                    //                            else
                    //                            {
                    //                                if (Convert.ToDecimal(txt.Text.Trim()) > Convert.ToDecimal(hfdLectureFees.Value))
                    //                                {
                    //                                    txt.Text = ""; txtScholarshipAmt.Text = "";
                    //                                    objCommon.DisplayMessage(updScholarship, "Enter value can not greater than remaining amount !!!", this.Page);
                    //                                    return;
                    //                                }
                    //                                else
                    //                                {

                    //                                    Amount = (Convert.ToDecimal(hfdLectureFees.Value) - Convert.ToDecimal(txt.Text.Trim()));

                    //                                    txtScholarshipAmt.Text = txt.Text.Trim();
                    //                                    RemainingAmount = Amount;
                    //                                    hdfRemainingLectureAmount.Value = RemainingAmount.ToString();
                    //                                }
                    //                            }
                    //                        }
                    //                        else
                    //                        {
                    //                            txt.Text = ""; txtScholarshipAmt.Text = "";
                    //                            objCommon.DisplayMessage(updScholarship, "No Lecture Fees Remaining !!!", this.Page);
                    //                            return;
                    //                        }
                    //                    }
                    //                }

                    //            }
                    //        }
                    //        else if (ddlCalculation.SelectedIndex == 3)
                    //        {
                    //            if (txtValue.TabIndex.ToString() != txt.TabIndex.ToString())
                    //            {
                    //                if (ddlCalculationType.SelectedIndex == 1)
                    //                {
                    //                    if (hdfRemainingMiscAmount1.Value.ToString() == "0" || hdfRemainingMiscAmount1.Value.ToString() == "0.00")
                    //                    {
                    //                        Amount = (Convert.ToDecimal(hdfMisFees.Value) * Convert.ToDecimal(txt.Text.Trim())) / 100;

                    //                        txtScholarshipAmt.Text = Amount.ToString("0.00");
                    //                        RemainingAmount = Convert.ToDecimal(hdfMisFees.Value) - Amount;
                    //                        hdfRemainingMiscAmount.Value = RemainingAmount.ToString();
                    //                    }
                    //                    else
                    //                    {
                    //                        Amount = (Convert.ToDecimal(hdfRemainingMiscAmount1.Value) * Convert.ToDecimal(txt.Text.Trim())) / 100;

                    //                        txtScholarshipAmt.Text = Amount.ToString("0.00");
                    //                        RemainingAmount = Convert.ToDecimal(hdfRemainingMiscAmount1.Value) - Amount;
                    //                        hdfRemainingMiscAmount.Value = RemainingAmount.ToString();
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    if (hdfRemainingMiscAmount1.Value.ToString() == "0" || hdfRemainingMiscAmount1.Value.ToString() == "0.00")
                    //                    {
                    //                        Amount = (Convert.ToDecimal(hdfMisFees.Value) - Convert.ToDecimal(txt.Text.Trim()));

                    //                        txtScholarshipAmt.Text = txt.Text.Trim();
                    //                        RemainingAmount = Amount;
                    //                        hdfRemainingMiscAmount.Value = RemainingAmount.ToString();
                    //                    }
                    //                    else
                    //                    {
                    //                        if (Convert.ToDecimal(txt.Text.Trim()) > Convert.ToDecimal(hdfRemainingMiscAmount1.Value))
                    //                        {
                    //                            txt.Text = ""; txtScholarshipAmt.Text = "";
                    //                            objCommon.DisplayMessage(updScholarship, "Enter value can not greater than remaining amount !!!", this.Page);
                    //                            return;
                    //                        }
                    //                        else
                    //                        {
                    //                            Amount = (Convert.ToDecimal(hdfRemainingMiscAmount1.Value) - Convert.ToDecimal(txt.Text.Trim()));

                    //                            txtScholarshipAmt.Text = txt.Text.Trim();
                    //                            RemainingAmount = Amount;
                    //                            hdfRemainingMiscAmount.Value = RemainingAmount.ToString();
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                //if (Convert.ToString(hdfRemainingMiscAmount1.Value) == "0" || Convert.ToString(hdfRemainingMiscAmount1.Value) == "0.00" || Convert.ToString(hdfRemainingMiscAmount1.Value) == string.Empty)
                    //                //{
                    //                if (lvScholarshipAmount.Items.Count == 1)
                    //                {
                    //                    if (ddlCalculationType.SelectedIndex == 1)
                    //                    {
                    //                        Amount = (Convert.ToDecimal(hdfMisFees.Value) * Convert.ToDecimal(txt.Text.Trim())) / 100;

                    //                        txtScholarshipAmt.Text = Amount.ToString("0.00");
                    //                        RemainingAmount = Convert.ToDecimal(hdfMisFees.Value) - Amount;
                    //                        hdfRemainingMiscAmount.Value = RemainingAmount.ToString();
                    //                    }
                    //                    else
                    //                    {
                    //                        if (Convert.ToDecimal(txt.Text.Trim()) > Convert.ToDecimal(hdfMisFees.Value))
                    //                        {
                    //                            txt.Text = ""; txtScholarshipAmt.Text = "";
                    //                            objCommon.DisplayMessage(updScholarship, "Enter value can not greater than remaining amount !!!", this.Page);
                    //                            return;
                    //                        }
                    //                        else
                    //                        {
                    //                            Amount = (Convert.ToDecimal(hdfMisFees.Value) - Convert.ToDecimal(txt.Text.Trim()));

                    //                            txtScholarshipAmt.Text = txt.Text.Trim();
                    //                            RemainingAmount = Amount;
                    //                            hdfRemainingMiscAmount.Value = RemainingAmount.ToString();
                    //                        }
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    if (hdfRemainingMiscAmount.Value.ToString() == "0" || hdfRemainingMiscAmount.Value.ToString() == "0.00")
                    //                    {
                    //                        if (Convert.ToInt32(txt.TabIndex) > 1)
                    //                        {
                    //                            if (ddlCalculationType.SelectedIndex == 1)
                    //                            {
                    //                                Amount = (Convert.ToDecimal(hdfMisFees.Value) * Convert.ToDecimal(txt.Text.Trim())) / 100;

                    //                                txtScholarshipAmt.Text = Amount.ToString("0.00");
                    //                                RemainingAmount = Convert.ToDecimal(hdfMisFees.Value) - Amount;
                    //                                hdfRemainingMiscAmount.Value = RemainingAmount.ToString();
                    //                            }
                    //                            else
                    //                            {
                    //                                if (Convert.ToDecimal(txt.Text.Trim()) > Convert.ToDecimal(hdfMisFees.Value))
                    //                                {
                    //                                    txt.Text = ""; txtScholarshipAmt.Text = "";
                    //                                    objCommon.DisplayMessage(updScholarship, "Enter value can not greater than remaining amount !!!", this.Page);
                    //                                    return;
                    //                                }
                    //                                else
                    //                                {
                    //                                    Amount = (Convert.ToDecimal(hdfMisFees.Value) - Convert.ToDecimal(txt.Text.Trim()));

                    //                                    txtScholarshipAmt.Text = txt.Text.Trim();
                    //                                    RemainingAmount = Amount;
                    //                                    hdfRemainingMiscAmount.Value = RemainingAmount.ToString();
                    //                                }
                    //                            }
                    //                        }
                    //                        else
                    //                        {
                    //                            txt.Text = ""; txtScholarshipAmt.Text = "";
                    //                            objCommon.DisplayMessage(updScholarship, "No Mis. Fees Remaining !!!", this.Page);
                    //                            return;
                    //                        }
                    //                    }
                    //                }
                    //                //}
                    //            }
                    //        }
                        }
                    }
                    else
                    {
                        txt.Text = "";
                        objCommon.DisplayMessage(updScholarship, "Please Select Calculation On !!!", this.Page);
                        return;

                    }
                }
                else
                {
                    txt.Text = "";
                    txtScholarshipAmt.Text = "";
                    objCommon.DisplayMessage(updScholarship, "Please Select Scholarship/Discount Type !!!", this.Page);
                    return;
                }
            }
            else
            {
                txt.Text = "";
                txtScholarshipAmt.Text = "";
                objCommon.DisplayMessage(updScholarship, "Please Select Calculation Type !!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnAddRecord_Click(object sender, EventArgs e)
    {
        try
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                DataRow dr = null;
                if (dt.Rows.Count > 0 && dt.Rows.Count < 10)
                {
                    DataTable dtNewTable = new DataTable();
                    dtNewTable.Columns.Add(new DataColumn("id", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("ScholarshipType", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("CalculationType", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("Amount", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("CalculationOn", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("FinalAmount", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("RemainingLabAmount", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("RemainingLectureAmount", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("RemainingMiscAmount", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("SCHOL_STATUS", typeof(string)));  

                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        DropDownList ddlScholarshipType = lvScholarshipAmount.Items[rowIndex].FindControl("ddlScholarshipType") as DropDownList;
                        DropDownList ddlCalculationType = lvScholarshipAmount.Items[rowIndex].FindControl("ddlCalculationType") as DropDownList;
                        DropDownList ddlCalculation = lvScholarshipAmount.Items[rowIndex].FindControl("ddlCalculation") as DropDownList;
                        TextBox txtValue = lvScholarshipAmount.Items[rowIndex].FindControl("txtValue") as TextBox;
                        TextBox txtScholarshipAmt = lvScholarshipAmount.Items[rowIndex].FindControl("txtScholarshipAmt") as TextBox;
                        HiddenField hdfRemainingLabAmount = lvScholarshipAmount.Items[rowIndex].FindControl("hdfRemainingLabAmount") as HiddenField;
                        HiddenField hdfRemainingLectureAmount = lvScholarshipAmount.Items[rowIndex].FindControl("hdfRemainingLectureAmount") as HiddenField;
                        HiddenField hdfRemainingMiscAmount = lvScholarshipAmount.Items[rowIndex].FindControl("hdfRemainingMiscAmount") as HiddenField;
                        HiddenField hdfSrno = lvScholarshipAmount.Items[rowIndex].FindControl("hdfSrno") as HiddenField;

                        if (ddlScholarshipType.SelectedIndex == 0)
                        {
                            objCommon.DisplayMessage(updScholarship, "Please Select Scholarship/Discount Type", this.Page);
                            return;
                        }
                        else if (ddlCalculationType.SelectedIndex == 0)
                        {
                            objCommon.DisplayMessage(updScholarship, "Please Select Calculation Type", this.Page);
                            return;
                        }
                        else if (ddlCalculation.SelectedIndex == 0)
                        {
                            objCommon.DisplayMessage(updScholarship, "Please Select Calculation On", this.Page);
                            return;
                        }
                        else if (txtValue.Text.Trim() == "")
                        {
                            objCommon.DisplayMessage(updScholarship, "Please Enter Value", this.Page);
                            return;
                        }
                        else if (txtScholarshipAmt.Text.Trim() == "")
                        {
                            objCommon.DisplayMessage(updScholarship, "Please Enter Scholarship/Discount Amount", this.Page);
                            return;
                        }
                        else
                        {
                            dr = dtNewTable.NewRow();
                            dr["id"] = hdfSrno.Value;
                            dr["ScholarshipType"] = ddlScholarshipType.SelectedValue;
                            dr["CalculationType"] = ddlCalculationType.SelectedValue;
                            dr["Amount"] = txtValue.Text.Trim();
                            dr["CalculationOn"] = ddlCalculation.SelectedValue;
                            dr["FinalAmount"] = txtScholarshipAmt.Text.Trim();
                            dr["RemainingLabAmount"] = hdfRemainingLabAmount.Value;
                            dr["RemainingLectureAmount"] = hdfRemainingLectureAmount.Value;
                            dr["RemainingMiscAmount"] = hdfRemainingMiscAmount.Value;
                            dr["SCHOL_STATUS"] = "0";

                            rowIndex++;
                            dtNewTable.Rows.Add(dr);
                        }
                    }

                    dr = dtNewTable.NewRow();
                    dr["id"] = 0;
                    dr["ScholarshipType"] = string.Empty;
                    dr["CalculationType"] = string.Empty;
                    dr["Amount"] = string.Empty;
                    dr["CalculationOn"] = string.Empty;
                    dr["FinalAmount"] = string.Empty;
                    dr["RemainingLabAmount"] = string.Empty;
                    dr["RemainingLectureAmount"] = string.Empty;
                    dr["RemainingMiscAmount"] = string.Empty;
                    dr["SCHOL_STATUS"] = "0";

                    dtNewTable.Rows.Add(dr);

                    ViewState["CurrentTable"] = dtNewTable;
                    lvScholarshipAmount.DataSource = dtNewTable;
                    lvScholarshipAmount.DataBind();
                }
                else
                {
                    objCommon.DisplayMessage(updScholarship, "Maximum Options Limit Reached", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(updScholarship, "Error!!!", this.Page);
            }
            SetPreviousData();
        }
        catch (Exception ex)
        {

        }
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList ddlScholarshipType = lvScholarshipAmount.Items[rowIndex].FindControl("ddlScholarshipType") as DropDownList;
                    DropDownList ddlCalculationType = lvScholarshipAmount.Items[rowIndex].FindControl("ddlCalculationType") as DropDownList;
                    DropDownList ddlCalculation = lvScholarshipAmount.Items[rowIndex].FindControl("ddlCalculation") as DropDownList;
                    TextBox txtValue = lvScholarshipAmount.Items[rowIndex].FindControl("txtValue") as TextBox;
                    TextBox txtScholarshipAmt = lvScholarshipAmount.Items[rowIndex].FindControl("txtScholarshipAmt") as TextBox;
                    HiddenField hdfRemainingLabAmount = lvScholarshipAmount.Items[rowIndex].FindControl("hdfRemainingLabAmount") as HiddenField;
                    HiddenField hdfRemainingLectureAmount = lvScholarshipAmount.Items[rowIndex].FindControl("hdfRemainingLectureAmount") as HiddenField;
                    HiddenField hdfRemainingMiscAmount = lvScholarshipAmount.Items[rowIndex].FindControl("hdfRemainingMiscAmount") as HiddenField;
                    HiddenField hdfSrno = lvScholarshipAmount.Items[rowIndex].FindControl("hdfSrno") as HiddenField;

                    ddlScholarshipType.Items.Clear();
                    ddlScholarshipType.Items.Add("Please Select");
                    ddlScholarshipType.SelectedItem.Value = "0";

                    ddlCalculationType.Items.Clear();
                    ddlCalculationType.Items.Add("Please Select");
                    ddlCalculationType.SelectedItem.Value = "0";

                    ddlCalculation.Items.Clear();
                    ddlCalculation.Items.Add("Please Select");
                    ddlCalculation.SelectedItem.Value = "0";

                    DataTable dt1 = new DataTable();
                    dt1 = (DataTable)ViewState["CONCESSION_TYPE"];
                    DataTable dt2 = new DataTable();
                    dt2 = (DataTable)ViewState["TYPENAME"];
                    DataTable dt3 = new DataTable();
                    dt3 = (DataTable)ViewState["CALCULATION_NAME"];

                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        ddlScholarshipType.DataSource = dt1;
                        ddlScholarshipType.DataValueField = "CONCESSION_TYPENO";
                        ddlScholarshipType.DataTextField = "CONCESSION_TYPE";
                        ddlScholarshipType.DataBind();
                    }
                    if (dt2 != null && dt2.Rows.Count > 0)
                    {
                        ddlCalculationType.DataSource = dt2;
                        ddlCalculationType.DataValueField = "TYPENO";
                        ddlCalculationType.DataTextField = "TYPENAME";
                        ddlCalculationType.DataBind();
                    }
                    if (dt3 != null && dt3.Rows.Count > 0)
                    {
                        ddlCalculation.DataSource = dt3;
                        ddlCalculation.DataValueField = "SRNO";
                        ddlCalculation.DataTextField = "CALCULATION_NAME";
                        ddlCalculation.DataBind();
                    }

                    hdfSrno.Value = dt.Rows[i]["id"].ToString();
                    ddlScholarshipType.SelectedValue = dt.Rows[i]["ScholarshipType"].ToString();
                    ddlCalculationType.SelectedValue = dt.Rows[i]["CalculationType"].ToString();
                    txtValue.Text = dt.Rows[i]["Amount"].ToString();
                    ddlCalculation.SelectedValue = dt.Rows[i]["CalculationOn"].ToString();
                    txtScholarshipAmt.Text = dt.Rows[i]["FinalAmount"].ToString();
                    hdfRemainingLabAmount.Value = dt.Rows[i]["RemainingLabAmount"].ToString();
                    hdfRemainingLectureAmount.Value = dt.Rows[i]["RemainingLectureAmount"].ToString();
                    hdfRemainingMiscAmount.Value = dt.Rows[i]["RemainingMiscAmount"].ToString();

                    rowIndex++;
                }
            }
        }
        else
        {
            SetInitialRow();
        }
    }

    protected void btncanceldata_Click(object sender, EventArgs e)
    {
        try
        {
            txtStdID.Text = "";
            ddlAcademicSession.SelectedValue = "0";
            lvStudent.DataSource = null;
            lvStudent.DataBind();

            lvScholarshipAmount.DataSource = null;
            lvScholarshipAmount.DataBind();

            lvBindDetails.DataSource = null;
            lvBindDetails.DataBind();

            txtSearchCandidateProgram.Text = "";
            rdselect.SelectedValue = null;

            ViewState["IDNO"] = null;

            divShowDetails.Visible = false;

            lblName.Text = "";
            lblbranch.Text = "";
            lblProgram.Text = "";
            lblsem.Text = "";

            lblTotalFees.Text = "";
            lblLectureFees.Text = "";
            lblLabFees.Text = "";
            lblMisFees.Text = "";
            lblPaidFees.Text = "";

            ViewState["CONCESSION_TYPE"] = null;
            ViewState["TYPENAME"] = null;
            ViewState["CALCULATION_NAME"] = null;
        }
        catch (Exception ex)
        { 
        
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtNewTable = new DataTable();
            dtNewTable.Columns.Add(new DataColumn("srno", typeof(string)));
            dtNewTable.Columns.Add(new DataColumn("id", typeof(string)));
            dtNewTable.Columns.Add(new DataColumn("ScholarshipType", typeof(string)));
            dtNewTable.Columns.Add(new DataColumn("CalculationType", typeof(string)));
            dtNewTable.Columns.Add(new DataColumn("CalculationOn", typeof(string)));
            dtNewTable.Columns.Add(new DataColumn("Amount", typeof(string)));
            dtNewTable.Columns.Add(new DataColumn("FinalAmount", typeof(string)));
            dtNewTable.Columns.Add(new DataColumn("RemainingLabAmount", typeof(string)));
            dtNewTable.Columns.Add(new DataColumn("RemainingLectureAmount", typeof(string)));
            dtNewTable.Columns.Add(new DataColumn("RemainingMiscAmount", typeof(string)));

            DataRow dr = null;
            int i = 0;

            foreach (ListViewDataItem dataitem in lvScholarshipAmount.Items)
            {
                DropDownList ddlCalculationType = dataitem.FindControl("ddlCalculationType") as DropDownList;
                DropDownList ddlScholarshipType = dataitem.FindControl("ddlScholarshipType") as DropDownList;
                DropDownList ddlCalculation = dataitem.FindControl("ddlCalculation") as DropDownList;
                TextBox txtValue = dataitem.FindControl("txtValue") as TextBox;
                TextBox txtScholarshipAmt = dataitem.FindControl("txtScholarshipAmt") as TextBox;
                HiddenField hdfSrno = dataitem.FindControl("hdfSrno") as HiddenField;
                HiddenField hdfRemainingLabAmount = dataitem.FindControl("hdfRemainingLabAmount") as HiddenField;
                HiddenField hdfRemainingLectureAmount = dataitem.FindControl("hdfRemainingLectureAmount") as HiddenField;
                HiddenField hdfRemainingMiscAmount = dataitem.FindControl("hdfRemainingMiscAmount") as HiddenField;

                if (ddlScholarshipType.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(updScholarship, "Please Select Scholarship/Discount Type", this.Page);
                    return;
                }
                else if (ddlCalculationType.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(updScholarship, "Please Select Calculation Type", this.Page);
                    return;
                }
                else if (ddlCalculation.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(updScholarship, "Please Select Calculation On", this.Page);
                    return;
                }
                else if (txtValue.Text.Trim() == "")
                {
                    objCommon.DisplayMessage(updScholarship, "Please Enter Value", this.Page);
                    return;
                }
                else if (txtScholarshipAmt.Text.Trim() == "")
                {
                    objCommon.DisplayMessage(updScholarship, "Please Enter Scholarship/Discount Amount", this.Page);
                    return;
                }
                else
                {
                    dr = dtNewTable.NewRow();
                    dr["srno"] = i.ToString();
                    dr["id"] = hdfSrno.Value;
                    dr["ScholarshipType"] = ddlScholarshipType.SelectedValue;
                    dr["CalculationType"] = ddlCalculationType.SelectedValue;
                    dr["CalculationOn"] = ddlCalculation.SelectedValue;
                    dr["Amount"] = txtValue.Text.Trim();
                    dr["FinalAmount"] = txtScholarshipAmt.Text.Trim();
                    dr["RemainingLabAmount"] = hdfRemainingLabAmount.Value;
                    dr["RemainingLectureAmount"] = hdfRemainingLectureAmount.Value;
                    dr["RemainingMiscAmount"] = hdfRemainingMiscAmount.Value;

                    dtNewTable.Rows.Add(dr);
                    i++;
                }
            }

            DataSet dsXml = new DataSet();
            dsXml.Tables.Add(dtNewTable);

            DataSet ds = objSC.InsertUpdteStudentScholarshipDetails(dsXml, Convert.ToInt32(ddlAcademicSession.SelectedValue), Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(Session["userno"]));
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["RECORD_STATUS"].ToString() == "1")
                {
                    lvBindDetails.DataSource = ds.Tables[0];
                    lvBindDetails.DataBind();

                    objCommon.DisplayMessage(this, "Record Saved Successfully !!!", this.Page);
                    return;
                }
                else
                {
                    lvBindDetails.DataSource = null;
                    lvBindDetails.DataBind();

                    objCommon.DisplayMessage(this, "Error occured !!!", this.Page);
                    return;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
}