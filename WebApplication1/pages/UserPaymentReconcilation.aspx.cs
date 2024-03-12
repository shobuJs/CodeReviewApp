//======================================================================================
// PROJECT NAME   : MZURFCAMPUS                                                        
// MODULE NAME    : ACADEMIC                                                             
// PAGE NAME      : User Payment Reconcilation
// CREATION DATE  : 10-MAY-2016                                                          
// CREATED BY     : MANISH CHAWADE                                                   
// MODIFIED DATE  :                                                                      
// MODIFIED DESC  :                                                                      
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
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
using System.IO;


public partial class ACADEMIC_UserPaymentReconcilation : System.Web.UI.Page
{
    Common objCommon = new Common();

    FeeCollectionController objFees = new FeeCollectionController();


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

                Page.Title = Session["coll_name"].ToString();


                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }
                this.PopulateDropDown();
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
                Response.Redirect("~/notauthorized.aspx?page=UserPaymentReconcilation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=UserPaymentReconcilation.aspx");
        }
    }

    private void PopulateDropDown()
    {
        objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
    }

    private void ShowStudents()
    {
        try
        {
            DataSet ds = objFees.ShowStudentsForReconcile(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlPaymentType.SelectedValue), txtAppliid.Text.Trim());
            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                if (ddlPaymentType.SelectedValue == "1")
                {
                    pnlStudentlistCash.Visible = true;
                    lvStudentCash.DataSource = ds;
                    lvStudentCash.DataBind();
                    lvStudentCash.Visible = true;

                    foreach (ListViewDataItem lvItem in lvStudentCash.Items)
                    {
                        CheckBox chkb = lvItem.FindControl("chkRecon") as CheckBox;
                        TextBox chktxt = lvItem.FindControl("txtremark") as TextBox;

                        if (chkb.ToolTip == "1")
                        {
                            chkb.Enabled = false;
                            chkb.Checked = true;
                            chktxt.Enabled = false;
                            chkb.ForeColor = System.Drawing.Color.Green;
                            chkb.Text = "RECONCILED";
                        }
                    }
                }
                else
                {
                    pnlStudentList.Visible = true;
                    lvStudent.DataSource = ds;
                    lvStudent.DataBind();
                    lvStudent.Visible = true;

                    foreach (ListViewDataItem lvItem in lvStudent.Items)
                    {
                        CheckBox chkb = lvItem.FindControl("chkRecon") as CheckBox;
                        TextBox chktxt = lvItem.FindControl("txtremark") as TextBox;

                        if (chkb.ToolTip == "1")
                        {
                            chkb.Enabled = false;
                            chkb.Checked = true;
                            chktxt.Enabled = false;
                            chkb.ForeColor = System.Drawing.Color.Green;
                            chkb.Text = "RECONCILED";
                        }
                    }

                }
                btnRecon.Visible = true;
                btnReport.Visible = true;
                btnexport.Visible = true;
                ddlAdmbatch.Enabled = false;
                ddlPaymentType.Enabled = false;
            }
            else
            {
                objCommon.DisplayMessage(this.updReconcile, "Students not found.", this.Page);
                pnlStudentList.Visible = false;
                pnlStudentlistCash.Visible = false;
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                lvStudentCash.DataSource = null;
                lvStudentCash.DataBind();
                lvStudent.Visible = false;
                lvStudentCash.Visible = false;
                btnRecon.Visible = false;
                btnReport.Visible = false;
                btnexport.Visible = false;
                ddlAdmbatch.Enabled = true;
                ddlPaymentType.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentReconsilReport.btnShow_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnApplystudList_Click(object sender, EventArgs e)
    {
        if (ddlPaymentType.SelectedValue != "")
        {
            this.ShowStudents();
        }
        else
        {
            objCommon.DisplayMessage(this.updReconcile, "Please Select Payment Type.", this.Page);
            return;
        }
    }

    protected void btnRecon_Click(object sender, EventArgs e)
    {
        try
        {
            string userIds = string.Empty;
            string Remark = string.Empty;
            int count = 0;

            if (ddlPaymentType.SelectedValue == "1")
            {
                foreach (ListViewDataItem lvItem in lvStudentCash.Items)
                {
                    CheckBox chkb = lvItem.FindControl("chkRecon") as CheckBox;
                    TextBox txtRemark = lvItem.FindControl("txtremark") as TextBox;
                    HiddenField hdfuserid = lvItem.FindControl("hdfUserno") as HiddenField;

                    if (chkb.Checked == true && chkb.Enabled == true)
                    {
                        userIds += hdfuserid.Value.ToString() + "$";
                        Remark += txtRemark.Text.Trim() + "$";
                    }
                    else if (chkb.Checked == true && chkb.Enabled == false)
                        count++;

                    if (count == lvStudentCash.Items.Count)
                    {
                        objCommon.DisplayMessage(this.updReconcile, "Students already reconciled in the List.", this.Page);
                        return;
                    }
                }

            }
            else
            {


                foreach (ListViewDataItem lvItem in lvStudent.Items)
                {
                    CheckBox chkb = lvItem.FindControl("chkRecon") as CheckBox;
                    TextBox txtRemark = lvItem.FindControl("txtremark") as TextBox;
                    HiddenField hdfuserid = lvItem.FindControl("hdfUserno") as HiddenField;

                    if (chkb.Checked == true && chkb.Enabled == true)
                    {
                        userIds += hdfuserid.Value.ToString() + "$";
                        Remark += txtRemark.Text.Trim() + "$";
                    }
                    else if (chkb.Checked == true && chkb.Enabled == false)
                        count++;

                    if (count == lvStudent.Items.Count)
                    {
                        objCommon.DisplayMessage(this.updReconcile, "Students already reconciled in the List.", this.Page);
                        return;
                    }
                }
            }

            if (userIds != "")
            {
                if (userIds.Substring(userIds.Length - 1) == ",")
                    userIds = userIds.Substring(0, userIds.Length - 1);
            }
            else
            {
                objCommon.DisplayMessage(this.updReconcile, "Please select at least one student.", this.Page);
                return;
            }

            int cs = objFees.ReconcileStudentFees(Convert.ToInt32(ddlAdmbatch.SelectedValue), userIds.ToString().TrimEnd('$'), Remark.ToString());
            if (cs == 2)
            {
                objCommon.DisplayMessage(this.updReconcile, "Student fees reconciled Sucessfully.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updReconcile, "Error occured in reconciliation.", this.Page);
                return;
            }
            this.ShowStudents();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentReconsilReport.btnRecon_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlPaymentType.SelectedValue == "0")
            ShowReport("Reconcile Student List", "rptStudentfeesReconcile.rpt", 0);
        else
            ShowReport("Reconcile Student List", "rptStudentfeesReconcilebyCash.rpt", 1);
    }

    private void Export()
    {
        string attachment = "attachment; filename=" + "ReconciledStudentList.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        DataSet ds = objFees.ShowStudentsByExcel(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlPaymentType.SelectedValue), txtAppliid.Text.Trim());
        DataGrid dg = new DataGrid();
        if (ds.Tables.Count > 0)
        {
            dg.DataSource = ds.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    private void ShowReport(string reportTitle, string rptFileName, int Paymenttype)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmbatch.SelectedValue) + ",@P_PAYMENT_TYPE=" + Paymenttype + ",@P_APPLIID=" + txtAppliid.Text.Trim();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updReconcile, this.updReconcile.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "CourseWise_Registration.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnexport_Click(object sender, EventArgs e)
    {
        this.Export();
    }
}