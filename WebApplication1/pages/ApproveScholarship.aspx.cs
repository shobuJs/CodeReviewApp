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

public partial class ACADEMIC_ApproveScholarship : System.Web.UI.Page
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
                    CheckPageAuthorization();
                    if (Convert.ToInt32(Session["usertype"]) == 2)
                    {
                        Response.Redirect("~/notauthorized.aspx");
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlAcademicSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
                        BindScholarshipDetails();
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

    protected void BindScholarshipDetails()
    {
        try
        {
            lvScholarshipAmount.DataSource = null;
            lvScholarshipAmount.DataBind();

            lvBindDetails.DataSource = null;
            lvBindDetails.DataBind();

            ViewState["CurrentTable"] = null;

            ddlAcademicSession.SelectedValue = Request.QueryString["sessionno"].ToString().Trim();
            txtStdID.Text = Request.QueryString["regno"].ToString().Trim();

            if (txtStdID.Text.Trim() == string.Empty)
            {
                divShowDetails.Visible = false;
                objCommon.DisplayMessage(this, "Student ID Not Found !!!", this.Page);
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

                        ddlApproveType.SelectedValue = "0";

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

                        objCommon.DisplayMessage(this, "Please Enter Valid Student ID", this.Page);
                        return;
                    }
                    if (ds.Tables[6] != null)
                    {
                        if (Convert.ToInt32(ds.Tables[6].Rows[0]["REGISTERED"].ToString()) == 0)
                        {
                            divShowDetails.Visible = false;

                            ddlApproveType.SelectedValue = "0";

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

                            objCommon.DisplayMessage(this, "Student Enlistment Not Found !!!", this.Page);
                            return;
                        }
                    }
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
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
                            ddlApproveType.SelectedValue = ds.Tables[5].Rows[0]["SCHOL_STATUS"].ToString();
                            if (ds.Tables[5].Rows[0]["SCHOL_STATUS"].ToString() == "1")
                            {
                                //ddlApproveType.Enabled = false;
                                //btnSave.Visible = false;
                            }
                            else
                            {
                                btnSave.Visible = true;
                                ddlApproveType.Enabled = true;
                            }
                            lvBindDetails.DataSource = ds.Tables[5];
                            lvBindDetails.DataBind();

                            lvScholarshipAmount.DataSource = ds.Tables[5];
                            lvScholarshipAmount.DataBind();

                            ViewState["CurrentTable"] = ds.Tables[5];
                        }
                        else
                        {
                            ddlApproveType.SelectedValue = "0";
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

                        ddlApproveType.SelectedValue = "0";

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
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objSC.UpdateStudentScholarshipApprovalDetails(Convert.ToInt32(ddlApproveType.SelectedValue), Convert.ToInt32(Request.QueryString["sessionno"].ToString().Trim()), Convert.ToInt32(Request.QueryString["idno"].ToString().Trim()), Convert.ToInt32(Session["userno"]));
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["RECORD_STATUS"].ToString() == "1")
                {
                    lvBindDetails.DataSource = ds.Tables[0];
                    lvBindDetails.DataBind();

                    ddlApproveType.SelectedValue = ds.Tables[0].Rows[0]["SCHOL_STATUS"].ToString();
                    if (ds.Tables[0].Rows[0]["SCHOL_STATUS"].ToString() == "1")
                    {
                        ddlApproveType.Enabled = false;
                        btnSave.Visible = false;
                    }
                    else
                    {
                        btnSave.Visible = true;
                        ddlApproveType.Enabled = true;
                    }

                    objCommon.DisplayMessage(this, "Scholarship Approval Done Successfully !!!", this.Page);
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