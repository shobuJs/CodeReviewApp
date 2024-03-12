//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Fees Installment Details
// CREATION DATE : 02/03/2020
// CREATED BY    : Sneha Doble
// MODIFIED BY   : 
// MODIFIED DATE : 
// MODIFIED DESC : 
//======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Linq;
using System.Configuration;
using System.IO;



public partial class ACADEMIC_StudentDocumentList : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    int count = 0;

    #region Pageload

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
                if (Convert.ToInt32(Session["usertype"]) == 1)
                {
                    CheckPageAuthorization();
                }
                else
                {
                }
                SetInitialRow();
                ViewState["uano"] = Session["userno"];
            }
            //PopulateDropDownList();
            //int mul_col_flag = Convert.ToInt32(objCommon.LookUp("REFF", "MUL_COL_FLAG", "CollegeName='" + Session["coll_name"].ToString() + "'"));
            // if (mul_col_flag == 0)
            //{
            //    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            //    ddlCollege.SelectedIndex = 1;
            //}
            //else
            //{
            //objCommon.FillDropDownList(ddlSemester, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");                
            //ddlCollege.SelectedIndex = 0;
            // }
            //objCommon.FillDropDownList(ddlSemester, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO > 0 AND RECIEPT_CODE='TF'", "RECIEPT_TITLE");
             //AND RECIEPT_CODE='TF'
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentDocumentList.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=StudentDocumentList.aspx");
        }
    }
    #endregion

    #region private Methods

    //private void PopulateDropDownList()
    //{
    //    //objCommon.FillDropDownList(ddlSemester, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSION_PNAME DESC");
    //   // objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_TITLE", "RCPTTYPENO IN (1,2,3)", "RCPTTYPENO");
    //}

    private void showdetails()
    {
        DataSet ds = new DataSet();
        //Show Total Demand
        string RecieptCode = Convert.ToString(ddlReceiptType.SelectedValue);

        string demand = "";
        int idnodcr = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO = '" + txtAdmissionNo.Text + "' OR REGNO='" + txtAdmissionNo.Text + "'"));
        demand = (objCommon.LookUp("ACD_DEMAND", "ISNULL(TOTAL_AMT,0)TOTAL_AMT", "SEMESTERNO = " + ddlSemester.SelectedValue + " and IDNO=" + idnodcr + "and RECIEPT_CODE='" + RecieptCode + "'"));

        if (demand != "")
        {
            //string locks = "";
            //locks = (objCommon.LookUp("ACD_STUDENT_FEES_INSTALLMENT", "Lock", "SESSION_NO = " + ddlSemester.SelectedValue + " and IDNO=" + idnodcr + "and RECIPTNO = " + ddlReceiptType.SelectedValue +""));
            //if (locks != "")
            //{

            //DataSet ds2 = objCommon.FillDropDown("ACD_FEES_INSTALLMENT", "DUE_DATE", "TOTAL_AMOUNT", " IDNO=" + idnodcr + "AND SEMESTERNO=" + ddlSemester.SelectedValue + "AND RECIPTCODE=" + ddlReceiptType.SelectedValue + "AND DUE_DATE IS NOT NULL", "");
            //    if (ds2.Tables[0].Rows.Count > 0)
            //    {

            int count = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "count(*)", "IDNO= " + idnodcr + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND RECIPTCODE = '" + Convert.ToString(ddlReceiptType.SelectedValue) + "'"));
            if (count > 0)
            {
                objCommon.DisplayMessage(upBulkInstalment, "Installment Already Created for Selected Criteria.", this.Page);
                ds = objSC.GetStudentInfoInstalmentListByEnrollNo(Convert.ToInt32(ddlSemester.SelectedValue), txtAdmissionNo.Text.Trim(), ViewState["receipt_code"].ToString());
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    divStudInfo.Visible = true;
                    pnlStudinstalment.Visible = false;
                    btnRemove.Visible = true;
                    DataRow dr = ds.Tables[0].Rows[0];
                    this.PopulateStudentInfoSection(dr);

                }
            }
            else
            {
                ds = objSC.GetStudentInfoInstalmentListByEnrollNo(Convert.ToInt32(ddlSemester.SelectedValue), txtAdmissionNo.Text.Trim(), ViewState["receipt_code"].ToString());
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    divStudInfo.Visible = true;
                    pnlStudinstalment.Visible = false;
                    DataRow dr = ds.Tables[0].Rows[0];
                    this.PopulateStudentInfoSection(dr);

                }
            }
           
        }
        else
        {
            objCommon.DisplayMessage(upBulkInstalment, "Demand not found for this Receipt Type.", this.Page);
            ClearControl();
        }
    }

    private void PopulateStudentInfoSection(DataRow dr)
    {
        try
        {
            lblStudName.Text = dr["STUDNAME"].ToString();
            lblStudName.ToolTip = dr["ENROLLNO"].ToString();
            lblidno.ToolTip = dr["IDNO"].ToString();
            lblDegree.Text = dr["DEGREENAME"].ToString();
            lblDegree.ToolTip = dr["DEGREENO"].ToString();
            lblBranch.Text = dr["BRANCH_NAME"].ToString();
            // lblSemester.Text = dr["YEARWISE"].ToString() == "1" ? dr["YEARNAME"].ToString() : dr["SEMESTERNAME"].ToString();
            lblSemester.Text = dr["SEMESTERNAME"].ToString();
            lblPaymentType.Text = dr["PAYTYPENAME"].ToString();
            //imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dr["IDNO"].ToString() + "&type=student";
            lblDemand.Text = dr["TOTAL_AMT"].ToString();
            lblinstalment.Text = dr["TOTAL_INSTALMENT"].ToString();
            hdfDmno.Value = dr["DM_NO"].ToString();
            txtRemark.Text = dr["REMARK_BY_AUTHORITY"].ToString();
            //DataSet dslist = objCommon.FillDropDown("ACD_STUDENT_FEES_INSTALLMENT", "NO_OF_INSTALL", "SESSION_NO", "IDNO=" + Convert.ToInt32(dr["IDNO"].ToString()), "");
            //if (dslist != null && dslist.Tables.Count > 0 && dslist.Tables[0].Rows.Count > 0)
            //{
            

            DataSet dslist = objCommon.FillDropDown("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALL_AMOUNT,INSTALMENT_NO,RECON", "IDNO=" + Convert.ToInt32(dr["IDNO"].ToString()) + " AND SEMESTERNO=" + Convert.ToInt32(dr["SEMESTERNO"].ToString()) + " AND RECIPTCODE='" + Convert.ToString(ddlReceiptType.SelectedValue) + "'", "");
            if (dslist != null && dslist.Tables.Count > 0 && dslist.Tables[0].Rows.Count > 0)
            {
                DataTable dt = dslist.Tables[0];
                
                pnlStudinstalment.Visible = true;
                grdinstalment.Visible = true;
                btnRemove.Visible = true;
                SetInitialRow();
                //SetBindPreviousData();
                ViewState["CurrentTable"] = dt;
                grdinstalment.DataSource = dslist;
                grdinstalment.DataBind();
                
                
            }
            else
            {
                int dcr_recon = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(*)", "IDNO =" + Convert.ToInt32(dr["IDNO"].ToString()) + " AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND RECIEPT_CODE='" + Convert.ToString(ddlReceiptType.SelectedValue) + "' AND ISNULL(LOCKED,0) = 1"));
                if (dcr_recon > 0)
                {
                    DataSet ds = objCommon.FillDropDown("ACD_DCR", "REC_DT AS DUE_DATE", "TOTAL_AMT AS INSTALL_AMOUNT,ROW_NUMBER() OVER(ORDER BY IDNO) AS INSTALMENT_NO,RECON", "IDNO=" + Convert.ToInt32(dr["IDNO"].ToString()) + " AND SEMESTERNO=" + Convert.ToInt32(dr["SEMESTERNO"].ToString()) + " AND RECIEPT_CODE='" + Convert.ToString(ddlReceiptType.SelectedValue) + "' AND ISNULL(LOCKED,0) = 1", "");

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];

                        pnlStudinstalment.Visible = true;
                        grdinstalment.Visible = true;
                        btnRemove.Visible = true;
                        SetInitialRow();
                        //SetBindPreviousData();
                        ViewState["CurrentTable"] = dt;
                        grdinstalment.DataSource = ds;
                        grdinstalment.DataBind();
                    }
                    //btnsubmit.Enabled = false;
                    //pnlStudinstalment.Visible = true;
                    return;
                }
           
                pnlStudinstalment.Visible = true;
                grdinstalment.Visible = true;
                btnRemove.Visible = false;
                //grdinstalment.DataSource = dslist;
                SetInitialRow();
                SetPreviousData();
                grdinstalment.DataBind();
                btnsubmit.Enabled = true;
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void ClearControl()
    {
        pnlStudinstalment.Visible = false;
        txtAdmissionNo.Text = string.Empty;
        ddlReceiptType.SelectedIndex = 0;
        //ddlCollege.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        divStudInfo.Visible = false;
        grdinstalment.Visible = false;
    }
    #endregion

    #region Button Click Functionality

    protected void btnShow_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        try
        {
            if (ddlSemester.SelectedIndex == 0 || ddlReceiptType.SelectedIndex == 0 || txtAdmissionNo.Text.Trim() == string.Empty)
            {
                objCommon.DisplayMessage(upBulkInstalment, "Please Enter Registration No", this.Page);
            }
            else
            {
                int idnodcr = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO = '" + txtAdmissionNo.Text + "' OR REGNO='" + txtAdmissionNo.Text + "'"));

                string recieptcode = Convert.ToString(ddlReceiptType.SelectedValue);
                //string recieptcode = Convert.ToString(objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO = " + ddlReceiptType.SelectedValue));
                ViewState["receipt_code"] = Convert.ToString(ddlReceiptType.SelectedValue);
                int dcr_recon = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(*)", "IDNO =" + idnodcr + " AND SEMESTERNO=" + ddlSemester.SelectedValue + "AND RECIEPT_CODE='" + recieptcode + "'"));
                if (dcr_recon > 0)
                {
                    //int RECON = Convert.ToInt32(objCommon.LookUp("ACD_STUD_INSTALMENT_AMOUNT", "count(*)", "IDNO = '" + idnodcr + "' AND SESSION_NO=" + ddlSemester.SelectedValue + "' AND RECIEPT_NO=" + ddlReceiptType.SelectedValue + "'"));


                    objCommon.DisplayMessage(upBulkInstalment, "Installment Amount Already Paid For this Selection Criteria.", this.Page);
                    ds = objSC.GetStudentInfoInstalmentListByEnrollNo(Convert.ToInt32(ddlSemester.SelectedValue), txtAdmissionNo.Text.Trim(), recieptcode);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        divStudInfo.Visible = true;
                        pnlStudinstalment.Visible = true;
                        //btnsubmit.Enabled = false;
                        btnRemove.Visible = true;
                        DataRow dr = ds.Tables[0].Rows[0];
                        this.PopulateStudentInfoSection(dr);

                    }
                }
                else
                {
                    //int OnlineCount = Convert.ToInt32(objCommon.LookUp("ACD_STUD_INSTALMENT_AMOUNT", " count(*)", "SESSION_NO = " + ddlSemester.SelectedValue + " and IDNO=" + idnodcr + " and RECIEPT_NO=" + ddlReceiptType.SelectedValue + "AND DUE_DATE IS NULL"));

                    //if (OnlineCount > 0)
                    //{
                    //int ret = objSC.GetUnpaidFeeDetails(idnodcr, Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlReceiptType.SelectedValue),Convert.ToInt32( ViewState["uano"].ToString()));
                    showdetails();
                    //}
                    //else
                    //{
                    //    showdetails();
                    //}
                }
            }
        }

        catch (Exception ex)
        {
        }
    }

    protected void submit_Click(object sender, EventArgs e)
    {
        try
        {
            int idnodcr = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO = '" + txtAdmissionNo.Text + "' OR REGNO='" + txtAdmissionNo.Text + "'"));

             int count = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "count(*)", "IDNO= " + idnodcr + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND RECIPTCODE = '" + Convert.ToString(ddlReceiptType.SelectedValue) + "'"));
             if (count > 0)
             {
                 CustomStatus cs = (CustomStatus)objSC.DeleteStudentInstalment(idnodcr, Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToString(ddlReceiptType.SelectedValue));

             }

            decimal TotalSum = 0.00M;
            foreach (GridViewRow item in grdinstalment.Rows)
            {
                TextBox txtamount = item.FindControl("txtAmount") as TextBox;
                TotalSum += txtamount.Text == string.Empty ? 0 : Convert.ToDecimal(txtamount.Text);
            }

            if (TotalSum.ToString() == lblDemand.Text.ToString())
            {
                foreach (GridViewRow item in grdinstalment.Rows)
                {
                    SubmitDatarow(item);
                }
                txtAdmissionNo.Text = string.Empty;
                ddlReceiptType.SelectedIndex = 0;
                ddlSemester.SelectedIndex = 0;
            }
            else
            {
                objCommon.DisplayMessage(upBulkInstalment, "Installment Amount not matched with Total Demand Amount.Insert Proper Calculated Amount.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDocumentList.submit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void SubmitDatarow(GridViewRow dRow)
    {
        try
        {
            int idnodcr = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO = '" + txtAdmissionNo.Text + "' OR REGNO='" + txtAdmissionNo.Text + "'"));
            //int session = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "SESSIONNO = " + ddlSemester.SelectedValue));
            string recieptcode = Convert.ToString(ddlReceiptType.SelectedValue);
            int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);

            string remark = txtRemark.Text == string.Empty ? null : txtRemark.Text;
            int installment = Convert.ToInt32(dRow.Cells[1].Text);//Get Bounded value from GridView.

            int totalcount = Convert.ToInt32(grdinstalment.Rows.Count);
            int dmno = Convert.ToInt32(hdfDmno.Value);

            int usertype = Convert.ToInt32(Session["usertype"]);
            int userno = Convert.ToInt32(Session["userno"]);
            int CollegeCode = Convert.ToInt32(Session["colcode"]);

            TextBox txtduedate = dRow.FindControl("txtDueDate") as TextBox;
            TextBox txtamount = dRow.FindControl("txtAmount") as TextBox;
            Label lblStatus = dRow.FindControl("lblStatus") as Label;

            DateTime date = Convert.ToDateTime(txtduedate.Text);
            decimal amount = txtamount.Text == string.Empty ? 0 : Convert.ToDecimal(txtamount.Text);
            decimal totalamount = lblDemand.Text == string.Empty ? 0 : Convert.ToDecimal(lblDemand.Text);
            string status = Convert.ToString(lblStatus.Text);

            int recon = 0;
            if (status == "Paid")
            {
                recon = 1;
            }
            else
            {
                recon = 0;
            }
            //int count = Convert.ToInt32(objCommon.LookUp("ACD_STUD_INSTALMENT_AMOUNT", " count(*)", "SESSION_NO = '" + ddlSemester.SelectedValue + "' and IDNO='" + idnodcr + "'and RECIEPT_NO='" + ddlReceiptType.SelectedValue + "'"));
            if (txtAdmissionNo.Text != string.Empty && ddlSemester.SelectedItem != null && ddlReceiptType.SelectedItem != null)
            {
                CustomStatus cs = (CustomStatus)objSC.InsertStudentInstalment(idnodcr, Semesterno, date, amount, totalamount, installment, remark, totalcount, usertype, recieptcode, dmno,recon, userno, CollegeCode, Convert.ToString(Session["ipAddress"]));
                objCommon.DisplayMessage(upBulkInstalment, "Student Installment Data Saved Successfully", this.Page);
                pnlStudinstalment.Visible = false;
                //btnsubmit.Enabled = false;
                divStudInfo.Visible = false;
               
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    #endregion

    #region Gridview
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        //dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("INSTALMENT_NO", typeof(string)));
        dt.Columns.Add(new DataColumn("DUE_DATE", typeof(string)));
        dt.Columns.Add(new DataColumn("INSTALL_AMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("RECON", typeof(string)));
        dr = dt.NewRow();
       // dr["RowNumber"] = 1;
        dr["INSTALMENT_NO"] = 1;
        dr["DUE_DATE"] = string.Empty;
        dr["INSTALL_AMOUNT"] = string.Empty;
        dr["RECON"] = 0;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;

        grdinstalment.DataSource = dt;
        grdinstalment.DataBind();
    }
    //private void SetInitialRow()
    //{
    //    DataTable dt = new DataTable();
    //    DataRow dr = null;
    //    dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
    //    dt.Columns.Add(new DataColumn("INSTALMENT_NO", typeof(string)));
    //    dt.Columns.Add(new DataColumn("Column1", typeof(string)));
    //    dt.Columns.Add(new DataColumn("Column2", typeof(string)));
    //    dr = dt.NewRow();
    //    dr["RowNumber"] = 1;
    //    dr["INSTALMENT_NO"] = 1;
    //    dr["Column1"] = string.Empty;
    //    dr["Column2"] = string.Empty;
    //    dt.Rows.Add(dr);

    //    //Store the DataTable in ViewState
    //    ViewState["CurrentTable"] = dt;

    //    grdinstalment.DataSource = dt;
    //    grdinstalment.DataBind();
    //}

    private void SetInitialRow1()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        //dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("INSTALMENT_NO", typeof(string)));
        dt.Columns.Add(new DataColumn("DUE_DATE", typeof(string)));
        dt.Columns.Add(new DataColumn("INSTALL_AMOUNT", typeof(string)));
        dr = dt.NewRow();
        //dr["RowNumber"] = 1;
        dr["INSTALMENT_NO"] = 1;
        dr["DUE_DATE"] = string.Empty;
        dr["INSTALL_AMOUNT"] = string.Empty;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        //ViewState["CurrentTable"] = dt;

        grdinstalment.DataSource = dt;
        grdinstalment.DataBind();
        
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    TextBox box1 = (TextBox)grdinstalment.Rows[rowIndex].Cells[1].FindControl("txtDueDate");
                    TextBox box2 = (TextBox)grdinstalment.Rows[rowIndex].Cells[2].FindControl("txtAmount");

                    box1.Text = dt.Rows[i]["DUE_DATE"].ToString();
                    box2.Text = dt.Rows[i]["INSTALL_AMOUNT"].ToString();

                    rowIndex++;
                }
            }
        }
    }

    private void SetBindPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    TextBox box1 = (TextBox)grdinstalment.Rows[rowIndex].Cells[1].FindControl("txtDueDate");
                    TextBox box2 = (TextBox)grdinstalment.Rows[rowIndex].Cells[2].FindControl("txtAmount");

                    box1.Text = dt.Rows[i]["DUE_DATE"].ToString();
                    box2.Text = dt.Rows[i]["INSTALL_AMOUNT"].ToString();

                    AddNewRowToGrid1();
                    rowIndex++;
                }
            }
        }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }

    protected void lnkRemove_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lb = (ImageButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (rowID > 0)
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 1)
                {
                    if (gvRow.RowIndex <= dt.Rows.Count)
                    {
                        //Remove the Selected Row data
                        dt.Rows.Remove(dt.Rows[rowID]);
                    }
                }
                //Store the current data in ViewState for future reference
                ViewState["CurrentTable"] = dt;
                //Re bind the GridView for the updated data
                grdinstalment.DataSource = dt;
                grdinstalment.DataBind();

                //Set Previous Data on Postbacks
                SetPreviousData();
            }
        }
    }

    private void AddNewRowToGrid()
    {
        int rowIndex = 0;
        int recon;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                DataTable dtNewTable = new DataTable();
                //dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("INSTALMENT_NO", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("DUE_DATE", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("INSTALL_AMOUNT", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("RECON", typeof(string)));

               


                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox box1 = (TextBox)grdinstalment.Rows[rowIndex].Cells[1].FindControl("txtDueDate");
                    TextBox box2 = (TextBox)grdinstalment.Rows[rowIndex].Cells[2].FindControl("txtAmount");
                    Label lbl1 = (Label)grdinstalment.Rows[rowIndex].Cells[3].FindControl("lblStatus");
                    if (lbl1.Text == "Paid")
                    {
                        recon = 1;
                    }
                    else
                    {
                        recon = 0;
                    }

                    if (box1.Text.Trim() != string.Empty && box2.Text.Trim() != string.Empty)
                    {
                        drCurrentRow = dtNewTable.NewRow();

                        //drCurrentRow["RowNumber"] = i + 1;
                        drCurrentRow["INSTALMENT_NO"] = i;
                        drCurrentRow["DUE_DATE"] = box1.Text;
                        drCurrentRow["INSTALL_AMOUNT"] = box2.Text;
                        drCurrentRow["RECON"] = recon;

                        rowIndex++;
                        dtNewTable.Rows.Add(drCurrentRow);
                    }
                    else
                    {
                        return;
                    }
                }

                drCurrentRow = dtNewTable.NewRow();
                //drCurrentRow["RowNumber"] = 1;
                drCurrentRow["INSTALMENT_NO"] = dtCurrentTable.Rows.Count + 1;
                drCurrentRow["DUE_DATE"] = string.Empty;
                drCurrentRow["INSTALL_AMOUNT"] = string.Empty;
                drCurrentRow["RECON"] = 0;

                dtNewTable.Rows.Add(drCurrentRow);

                
                

                ViewState["CurrentTable"] = dtNewTable;
                grdinstalment.DataSource = dtNewTable;
                grdinstalment.DataBind();

               // SetPreviousData();
            }
            else
            {
                objCommon.DisplayMessage(upBulkInstalment, "Maximum Installment Limit Reached", this.Page);
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }


    //private void AddNewRowToGrid()
    //{
    //    int rowIndex = 0;
    //    if (ViewState["CurrentTable"] != null)
    //    {
    //        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
    //        DataRow drCurrentRow = null;
    //        if (dtCurrentTable.Rows.Count > 0)
    //        {
    //            DataTable dtNewTable = new DataTable();
    //            dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
    //            dtNewTable.Columns.Add(new DataColumn("InstalmentNo", typeof(string)));
    //            dtNewTable.Columns.Add(new DataColumn("Column1", typeof(string)));
    //            dtNewTable.Columns.Add(new DataColumn("Column2", typeof(string)));
    //            drCurrentRow = dtNewTable.NewRow();

    //            drCurrentRow["RowNumber"] = 1;
    //            drCurrentRow["InstalmentNo"] = 1;
    //            drCurrentRow["Column1"] = string.Empty;
    //            drCurrentRow["Column2"] = string.Empty;

    //            dtNewTable.Rows.Add(drCurrentRow);

    //            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
    //            {
    //                //extract the TextBox values
    //                TextBox box1 = (TextBox)grdinstalment.Rows[rowIndex].Cells[1].FindControl("txtDueDate");
    //                TextBox box2 = (TextBox)grdinstalment.Rows[rowIndex].Cells[2].FindControl("txtAmount");

    //                if (box1.Text.Trim() != string.Empty && box2.Text.Trim() != string.Empty)
    //                {
    //                    drCurrentRow = dtNewTable.NewRow();

    //                    drCurrentRow["RowNumber"] = i + 1;
    //                    drCurrentRow["InstalmentNo"] = i + 1;
    //                    drCurrentRow["Column1"] = box1.Text;
    //                    drCurrentRow["Column2"] = box2.Text;

    //                    rowIndex++;
    //                    dtNewTable.Rows.Add(drCurrentRow);
    //                }
    //                else
    //                {
    //                    return;
    //                }
    //            }
    //            ViewState["CurrentTable"] = dtNewTable;
    //            grdinstalment.DataSource = dtNewTable;
    //            grdinstalment.DataBind();

    //            SetPreviousData();
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(upBulkInstalment, "Maximum Installment Limit Reached", this.Page);
    //        }
    //    }
    //    else
    //    {
    //        Response.Write("ViewState is null");
    //    }
    //}


    private void AddNewRowToGrid1()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                DataTable dtNewTable = new DataTable();
                dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("InstalmentNo", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("DUE_DATE", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("INSTALL_AMOUNT", typeof(string)));
                drCurrentRow = dtNewTable.NewRow();

                drCurrentRow["RowNumber"] = 1;
                drCurrentRow["InstalmentNo"] = 1;
                drCurrentRow["DUE_DATE"] = string.Empty;
                drCurrentRow["INSTALL_AMOUNT"] = string.Empty;

                dtNewTable.Rows.Add(drCurrentRow);

                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox box1 = (TextBox)grdinstalment.Rows[rowIndex].Cells[1].FindControl("txtDueDate");
                    TextBox box2 = (TextBox)grdinstalment.Rows[rowIndex].Cells[2].FindControl("txtAmount");

                    if (box1.Text.Trim() != string.Empty && box2.Text.Trim() != string.Empty)
                    {
                        drCurrentRow = dtNewTable.NewRow();

                        drCurrentRow["RowNumber"] = i + 1;
                        drCurrentRow["InstalmentNo"] = i + 1;
                        drCurrentRow["DUE_DATE"] = box1.Text;
                        drCurrentRow["INSTALL_AMOUNT"] = box2.Text;

                        rowIndex++;
                        dtNewTable.Rows.Add(drCurrentRow);
                    }
                    else
                    {
                        return;
                    }
                }
                ViewState["CurrentTable"] = dtNewTable;
                grdinstalment.DataSource = dtNewTable;
                grdinstalment.DataBind();

                SetBindPreviousData();
            }
            else
            {
                objCommon.DisplayMessage(upBulkInstalment, "Maximum Installment Limit Reached", this.Page);
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }
    #endregion



    //protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlSemester.SelectedIndex > 0)
    //    {
    //        objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_TITLE", "RCPTTYPENO IN (1,2,3)", "RECIEPT_TITLE");
    //        ddlReceiptType.Focus();
    //    }
    //    else
    //    {
    //        ddlSemester.Focus();
    //        ddlReceiptType.SelectedIndex = 0;
    //    }
    //}

    protected void ddlReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReceiptType.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_DEMAND D INNER JOIN ACD_SEMESTER S ON(D.SEMESTERNO = S.SEMESTERNO)", "DISTINCT D.SEMESTERNO", "S.SEMESTERNAME", "D.RECIEPT_CODE ='" + ddlReceiptType.SelectedValue + "' AND D.ENROLLNMENTNO = '" + txtAdmissionNo.Text + "'", "D.SEMESTERNO");
            ddlReceiptType.Focus();
        }
        else
        {
            ddlSemester.Focus();
            ddlReceiptType.SelectedIndex = 0;
        }
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        divStudInfo.Visible = false;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.ToString());
        txtAdmissionNo.Text = string.Empty;
        ddlSemester.SelectedIndex = 0;
        ddlReceiptType.SelectedIndex = 0;
        divStudInfo.Visible = false;
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        int idnodcr = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO = '" + txtAdmissionNo.Text + "' OR REGNO='" + txtAdmissionNo.Text + "'"));

        int count = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "count(*)", "IDNO= " + idnodcr + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND RECIPTCODE = '" + Convert.ToString(ddlReceiptType.SelectedValue) + "'"));
        if (count > 0)
        {
            CustomStatus cs = (CustomStatus)objSC.DeleteStudentInstalment(idnodcr, Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToString(ddlReceiptType.SelectedValue));
            objCommon.DisplayMessage(upBulkInstalment, "Student Installment Data Remove Successfully", this.Page);
            pnlStudinstalment.Visible = false;
            btnsubmit.Enabled = false;
            divStudInfo.Visible = false;
            txtAdmissionNo.Text = string.Empty;
            ddlReceiptType.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
        }
    }
}

