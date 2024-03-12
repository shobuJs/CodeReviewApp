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
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Collections.Generic;

public partial class ACADEMIC_Refund_Policy : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeesHeadController objFHC = new FeesHeadController();
    FeesHead fee = new FeesHead();
    RefundPolicy objRefundPolicy = new RefundPolicy();
    RefundpolicyController objcon = new RefundpolicyController();
    int status = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                this.CheckPageAuthorization();

                ViewState["usertype"] = Session["usertype"];
                BindListview();
                ViewState["action"] = "add";
                ViewState["RefundPolicyId"] = "0";
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
            }
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count <= 0)
                {
                    SetInitialRow();
                }
            }
            else
            {
                SetInitialRow();
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
                Response.Redirect("~/notauthorized.aspx?page=Refund_Policy.aspx.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Refund_Policy.aspx.aspx");
        }
    }

    protected void BindListview()
    {
        DataSet ds = objcon.GetPOLICYDATA(0);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvData.DataSource = ds;
            lvData.DataBind();
        }
        else
        {
            lvData.DataSource = null;
            lvData.DataBind();
        }

    }


    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));  
        dt.Columns.Add(new DataColumn("ID", typeof(string)));
        dr = dt.NewRow();
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["ID"] = 0;
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;
        lvRefundPolicy.DataSource = dt;
        lvRefundPolicy.DataBind();
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        DataRow drCurrentRow = null;
        dtCurrentTable.Rows.Clear();
        dtCurrentTable.AcceptChanges();
        foreach (var item in lvRefundPolicy.Items)
        {

            HiddenField hidden1 = (HiddenField)lvRefundPolicy.Items[rowIndex].FindControl("hfdvalue");
            TextBox box1 = (TextBox)lvRefundPolicy.Items[rowIndex].FindControl("txtNoOfDays");
            TextBox box2 = (TextBox)lvRefundPolicy.Items[rowIndex].FindControl("txtPercentage");

            drCurrentRow = dtCurrentTable.NewRow();

            drCurrentRow["Column1"] = box1.Text.Trim();
            drCurrentRow["Column2"] = box2.Text.Trim();
            drCurrentRow["ID"] = hidden1.Value;
            dtCurrentTable.Rows.Add(drCurrentRow);
            rowIndex += 1;

            //Do stuff
        }
        ViewState["CurrentTable"] = dtCurrentTable;

    }
   
    private string validateRefundData()
    {
        string _validate = string.Empty;

        int rowIndex = 0;
        foreach (var item in lvRefundPolicy.Items)
        {

            TextBox box1 = (TextBox)lvRefundPolicy.Items[rowIndex].FindControl("txtNoOfDays");
            TextBox box2 = (TextBox)lvRefundPolicy.Items[rowIndex].FindControl("txtPercentage");
            if (box1.Text.Trim() == string.Empty || box1.Text.Trim() == "")
            {
                _validate = "Please Enter No of Days";
                return _validate;
            }
            else if (box2.Text.Trim() == string.Empty || box2.Text.Trim() == "")
            {
                _validate = "Please Enter Percentage";
                return _validate;
            }
            rowIndex += 1;

            //Do stuff
        }
        return _validate;
    }

    protected void btnAddPolicySlab_Click(object sender, EventArgs e)
    {
        try
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                string _validateRefundData = validateRefundData();
                if (_validateRefundData != string.Empty)
                {
                    objCommon.DisplayMessage(updRefundPolicy, _validateRefundData, this.Page);
                    return;
                }
                SetPreviousData();
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    rowIndex = dtCurrentTable.Rows.Count + 1;
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["Column1"] = "0";
                    drCurrentRow["Column2"] = "0";
                    drCurrentRow["ID"] = rowIndex;
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    lvRefundPolicy.DataSource = dtCurrentTable;
                    lvRefundPolicy.DataBind();
                }
                else
                {
                    objCommon.DisplayMessage(updRefundPolicy, "Maximum Options Limit Reached", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(updRefundPolicy, "Error!!!", this.Page);
            }
            //SetPreviousData();
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int RefundPolicyId = int.Parse(btnEdit.CommandArgument);
            ViewState["RefundPolicyId"] = int.Parse(btnEdit.CommandArgument);
            //Label1.Text = string.Empty;
            //HiddenField1.Value = int.Parse(btnEdit.CommandArgument).ToString();
            ShowDetails(RefundPolicyId);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PDP_PDPRefund.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }
    private void ShowDetails(int RefundPolicyId)
    {

        int rowIndex = 1;
        DataSet ds = objcon.GetPOLICYDATA(RefundPolicyId);
        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
        {
            txtPolicyName.Text = ds.Tables[1].Rows[0]["REFUNDPOLICY_NAME"].ToString();
            ddlPaymentType.SelectedValue = ds.Tables[1].Rows[0]["PAYMENTTYPE1"].ToString();
            dtpEffectFromDate.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["WITHEFFECTFROM"].ToString()).ToString("dd-MM-yyyy");
            if (ds.Tables[1].Rows[0]["STATUS"].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(false);", true);
            }
        }
        DataTable dt = (DataTable)ViewState["CurrentTable"];
        dt.Rows.Clear();
        dt.AcceptChanges();
        DataRow drCurrentRow;
        foreach (DataRow dRow in ds.Tables[0].Rows)
        {
            drCurrentRow = dt.NewRow();
            drCurrentRow["Column1"] = dRow["NOOFDAYS"].ToString();
            drCurrentRow["Column2"] = dRow["PERCENTAGE"].ToString();
            drCurrentRow["ID"] = rowIndex;// dRow["REFUNDPOLICY_ID"].ToString();
            dt.Rows.Add(drCurrentRow);
            rowIndex += 1;


        }

        ViewState["CurrentTable"] = dt;
        lvRefundPolicy.DataSource = dt;
        lvRefundPolicy.DataBind();

    }

    //private void ShowDetails(int RefundPolicyId)
    //{

    //    int rowIndex = 1;
    //    DataSet ds = objCommon.FillDropDown("ACD_REFUND_POLICY", "REFUND_ID,REFUND_NAME,PAYMENTTYPE,WITHEFFECTFROM,POLICY_CATEGORY", "NOOFDAYS,PERCENTAGE,STATUS", "REFUND_ID=" + ViewState["RefundPolicyId"], "");
    //    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
    //    {
    //        txtPolicyName.Text = ds.Tables[0].Rows[0]["REFUND_NAME"].ToString();
    //        ddlPaymentType.SelectedValue = ds.Tables[0].Rows[0]["PAYMENTTYPE"].ToString();
    //        dtpEffectFromDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WITHEFFECTFROM"].ToString()).ToString("dd-MM-yyyy");
    //        if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "1")
    //        {
    //            ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(true);", true);
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(false);", true);
    //        }
    //    }
    //    DataTable dt = (DataTable)ViewState["CurrentTable"];
    //    dt.Rows.Clear();
    //    dt.AcceptChanges();
    //    DataRow drCurrentRow;
    //    foreach (DataRow dRow in ds.Tables[0].Rows)
    //    {
    //        drCurrentRow = dt.NewRow();
    //        drCurrentRow["Column1"] = dRow["NOOFDAYS"].ToString();
    //        drCurrentRow["Column2"] = dRow["PERCENTAGE"].ToString();
    //        drCurrentRow["ID"] = rowIndex;// dRow["REFUNDPOLICY_ID"].ToString();
    //        dt.Rows.Add(drCurrentRow);
    //        rowIndex += 1;


    //    }

    //    ViewState["CurrentTable"] = dt;
    //    lvRefundPolicy.DataSource = dt;
    //    lvRefundPolicy.DataBind();

    //}
    private DataTable CreateDatatable_Refund()
    {
        DataTable dt = new DataTable();
        dt.TableName = "RefundDatatable";
        dt.Columns.Add("RefundPolicyId");
        dt.Columns.Add("RefundPolicyName");
        dt.Columns.Add("RefundPolicyType");
        dt.Columns.Add("WithEffectFrom");
        dt.Columns.Add("PolicyCategory");
        dt.Columns.Add("Status");
        dt.Columns.Add("CreatedBy");
        dt.Columns.Add("ModifiedBy");
        dt.Columns.Add("NoOfDays");
        dt.Columns.Add("RefundPercentage");
        return dt;
    }
    private DataTable Add_Datatable_Refund()
    {
        int RefundPolicyId = Convert.ToInt32(ViewState["RefundPolicyId"]);

        DataTable dt = CreateDatatable_Refund();

        int rowIndex = 0;
        foreach (var item in lvRefundPolicy.Items)
        {
            DataRow dRow = dt.NewRow();
            TextBox box1 = (TextBox)lvRefundPolicy.Items[rowIndex].FindControl("txtNoOfDays");
            TextBox box2 = (TextBox)lvRefundPolicy.Items[rowIndex].FindControl("txtPercentage");

            dRow["RefundPolicyId"] = RefundPolicyId.ToString();
            dRow["RefundPolicyName"] = txtPolicyName.Text.Trim();
            dRow["RefundPolicyType"] = ddlPaymentType.SelectedValue;
            dRow["WithEffectFrom"] = Convert.ToDateTime("02/10/2023").ToString("dd-MMM-yyyy");
            dRow["PolicyCategory"] = "GENERAL";

            if (hfdStat.Value == "true")
            { dRow["Status"] = 1; }
            else { dRow["Status"] = 0; }

            dRow["CreatedBy"] = Convert.ToInt32(Session["userno"].ToString());
            dRow["ModifiedBy"] = 0;
            dRow["NoOfDays"] = Convert.ToInt32(box1.Text.Trim());
            dRow["RefundPercentage"] = Convert.ToDecimal(box2.Text.Trim());
            rowIndex += 1;
            dt.Rows.Add(dRow);

            //Do stuff
        }

        return dt;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string _validateRefundData = validateRefundData();
            if (_validateRefundData != string.Empty)
            {
                objCommon.DisplayMessage(updRefundPolicy, _validateRefundData, this.Page);
                return;
            }
            DataTable dt = Add_Datatable_Refund();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            if (lvRefundPolicy.Items.Count > 1)
            {
                foreach (ListViewDataItem dataitem in lvRefundPolicy.Items)
                {
                    HiddenField hfsrno2 = (HiddenField)dataitem.FindControl("hfsrno");
                    foreach (ListViewDataItem dataitem2 in lvRefundPolicy.Items)
                    {
                        HiddenField hfsrno = (HiddenField)dataitem2.FindControl("hfsrno");
                        TextBox txtNoOfDays = dataitem2.FindControl("txtNoOfDays") as TextBox;
                        TextBox txtPercentage = dataitem2.FindControl("txtPercentage") as TextBox;                       
                        if (txtNoOfDays.Text.ToString() == (dataitem.FindControl("txtNoOfDays") as TextBox).Text.ToString() &&
                            Convert.ToInt32(hfsrno.Value.ToString()) > 1 && Convert.ToInt32(hfsrno.Value.ToString()) != Convert.ToInt32(hfsrno2.Value.ToString()) &&
                            txtPercentage.Text.ToString().Trim().ToUpper() == (dataitem.FindControl("txtPercentage") as TextBox).Text.ToString().Trim().ToUpper())
                        {
                            objCommon.DisplayMessage(updRefundPolicy, "You Cannot add Same Refund Details.", this.Page);
                            return;
                        }
                    }
                }
            }
            if (ViewState["action"].ToString() == "add")
            {
                string count_Exists = Convert.ToString(objCommon.LookUp("ACD_REFUND_POLICY", "COUNT(1)", "REFUNDPOLICY_NAME='" + Convert.ToString(txtPolicyName.Text) + "'" + "AND PAYMENTTYPE ='" + Convert.ToString(ddlPaymentType.SelectedValue)  + "'"));
                if (count_Exists == "1")
                {
                    objCommon.DisplayMessage(updRefundPolicy, "Record Already Exists!!", this.Page);
                    return;
                }
                CustomStatus cs = (CustomStatus)objcon.ADDREFUNDDATA(ds.GetXml());
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(updRefundPolicy, "Record Saved Successfully!", this.Page);
                    BindListview();
                }
                else if (cs.Equals(CustomStatus.TransactionFailed))
                {
                    objCommon.DisplayUserMessage(updRefundPolicy, "Failed to Save Records", this.Page);
                    return;
                }
            }
            else
            {
                CustomStatus cs = (CustomStatus)objcon.UPDATEREFUNDDATA(ds.GetXml());
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(updRefundPolicy, "Record Updated Successfully!", this.Page);
                    BindListview();
                }
                else if (cs.Equals(CustomStatus.TransactionFailed))
                {
                    objCommon.DisplayUserMessage(updRefundPolicy, "Failed to Update Records", this.Page);
                    return;
                }
            }
            //Session["PDPSHOWMESAGE"] = "1";
            //BindListview();
            ClearControls();
            SetInitialRow();
            //Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PDP_RefundPolicy.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    private void ClearControls()
    {
        txtPolicyName.Text = string.Empty;
        ddlPaymentType.SelectedIndex = 0;
        dtpEffectFromDate.Text = string.Empty;
        ViewState["action"] = "add";
    }
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        int RefundPolicyId = Convert.ToInt32(ViewState["RefundPolicyId"]);
    //        string noofdays = string.Empty, percentage = string.Empty, policyname = "", with = "", category = "", paytype = "";
    //        string NODAYS = "", PER = "";
    //        int ua_no = 0;
    //        DataTable dt = CreateDatatable_Refund();

    //        int rowIndex = 0;
    //        foreach (var item in lvRefundPolicy.Items)
    //        {
    //            DataRow dRow = dt.NewRow();
    //            TextBox box1 = (TextBox)lvRefundPolicy.Items[rowIndex].FindControl("txtNoOfDays");
    //            TextBox box2 = (TextBox)lvRefundPolicy.Items[rowIndex].FindControl("txtPercentage");
    //            noofdays += box1.Text.Trim() + ",";
    //            percentage += box2.Text.Trim() + ",";

    //            rowIndex++;
    //        }
    //        NODAYS = noofdays.TrimEnd(',');
    //        PER = percentage.TrimEnd(',');
    //            if (hfdStat.Value == "true")
    //            {
    //                status = 1;
    //            }
    //            else
    //            {
    //                status = 0;
    //            }
    //            category = "";
    //            policyname = txtPolicyName.Text.Trim();
    //            paytype = Convert.ToString(ddlPaymentType.SelectedValue);
    //            with = Convert.ToDateTime(dtpEffectFromDate.Text).ToString("dd-MMM-yyyy");
    //            ua_no = Convert.ToInt32(Session["userno"].ToString());
                
    //            //rowIndex += 1;
    //            //dt.Rows.Add(dRow);

    //            //Do stuff
    //        //}
    //        //DataTable dt = Add_Datatable_Refund();
    //        DataSet ds = new DataSet();
    //        ds.Tables.Add(dt);

    //        if (ViewState["action"].ToString() == "add")
    //        {
    //            CustomStatus cs = (CustomStatus)objcon.ADDREFUNDDATA(Convert.ToString(policyname), Convert.ToString(paytype), Convert.ToString(with), Convert.ToString(category), Convert.ToInt32(status), Convert.ToString(NODAYS), Convert.ToString(PER), ua_no);
    //            if (cs.Equals(CustomStatus.RecordSaved))
    //            {
    //                objCommon.DisplayMessage(updRefundPolicy, "Record Saved Successfully!", this.Page);
    //            }
    //            else if (cs.Equals(CustomStatus.TransactionFailed))
    //            {
    //                objCommon.DisplayUserMessage(updRefundPolicy, "Failed to Save Records", this.Page);
    //                return;
    //            }
    //        }
    //        //else
    //        //{
    //        //    CustomStatus cs = (CustomStatus)objcon.ADDREFUNDDATA();
    //        //    if (cs.Equals(CustomStatus.RecordSaved))
    //        //    {
    //        //        objCommon.DisplayMessage(updRefundPolicy, "Record Updated Successfully!", this.Page);
    //        //    }
    //        //    else if (cs.Equals(CustomStatus.TransactionFailed))
    //        //    {
    //        //        objCommon.DisplayUserMessage(updRefundPolicy, "Failed to Update Records", this.Page);
    //        //        return;
    //        //    }
    //        //}
    //        //Session["PDPSHOWMESAGE"] = "1";
    //        //BindListview();
    //        ClearControls();
    //        SetInitialRow();
    //        //Response.Redirect(Request.Url.ToString());
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "RefundPolicy.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }

    //}


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        SetInitialRow();
        ViewState["RefundPolicyId"] = null;
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnRemove = sender as ImageButton;
            int Id = Convert.ToInt32(btnRemove.CommandArgument);
            int RowNumber = Convert.ToInt32(btnRemove.CommandName);
            DataTable dt = ViewState["CurrentTable"] as DataTable;
            if (Id != 0)
            {
                dt.Rows[RowNumber - 1].Delete();
                dt.AcceptChanges();
                ViewState["CurrentTable"] = dt;
                lvRefundPolicy.DataSource = dt;
                lvRefundPolicy.DataBind();
                SetPreviousData();
            }
        }
        catch (Exception ex)
        {

        }
    }
}





