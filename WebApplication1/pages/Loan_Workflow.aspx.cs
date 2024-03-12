using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using System.Data;
using DotNetIntegrationKit;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml;
using System.Net;

public partial class Loan_Workflow : System.Web.UI.Page
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
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["action"] = "add";
                objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER", "DISTINCT COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_ID ASC");
            }
        }
        BindListView();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Loan_Workflow.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Loan_Workflow.aspx");
        }
    }

    protected void txtMaxLimit_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lvAuthor.DataSource = null;
            lvAuthor.DataBind();
            DataTable dt = new DataTable();
            int count = 0;
            if (txtMaxLimit.Text.Trim() == string.Empty)
            {
                objCommon.DisplayMessage(Page, "Please Enter Max Limit", this.Page);
                return;
            }
            if (ddlFaculty.SelectedIndex > 0)
            {
                if (ddlUgPg.SelectedIndex > 0)
                {
                    DataTable edit = new DataTable();
                    if (Convert.ToString(ViewState["DYNAMIC_TABLE"]) != string.Empty || Convert.ToString(ViewState["DYNAMIC_TABLE"]) != null)
                    {
                        edit = (DataTable)ViewState["DYNAMIC_TABLE"];
                        if (ViewState["action"].ToString().Equals("edit") && Convert.ToInt32(txtMaxLimit.Text.ToString()) < Convert.ToInt32(ViewState["NUMBER_OF_GROUP"]))
                        {
                            txtMaxLimit.Text = "";
                            objCommon.DisplayMessage(Page, "Max Limit can not be less than " + ViewState["NUMBER_OF_GROUP"] + " !!!", this.Page);
                            return;
                        }
                    }

                    dt = this.CreateDataTable();

                    DataSet dsDropDown = new DataSet();
                    dsDropDown = objCommon.FillDropDown("USER_ACC", "DISTINCT UA_NO", "UA_FULLNAME", "UA_NO > 0 AND UA_TYPE != 2", "");

                    for (int i = 0; i < Convert.ToInt32(txtMaxLimit.Text.ToString()); i++)
                    {
                        count++;
                        DataRow dr = dt.NewRow();
                        dr["SRNO"] = count;
                        if (ViewState["action"].ToString().Equals("add"))
                        {
                            dr["AMOUNT"] = string.Empty;
                            dr["AUTHOR"] = string.Empty;
                            dr["LOANNO"] = string.Empty;
                            ViewState["NUMBER_OF_GROUP"] = "0";
                        }
                        else if (ViewState["action"].ToString().Equals("edit") && Convert.ToInt32(txtMaxLimit.Text.ToString()) == Convert.ToInt32(ViewState["NUMBER_OF_GROUP"]))
                        {
                            dr["AMOUNT"] = edit.Rows[i]["REQUEST_AMOUNT"].ToString();
                            dr["AUTHOR"] = edit.Rows[i]["AUTHORNO"].ToString();
                            dr["LOANNO"] = edit.Rows[i]["LOANNO"].ToString();
                        }
                        else if (ViewState["action"].ToString().Equals("edit") && Convert.ToInt32(count) <= Convert.ToInt32(ViewState["NUMBER_OF_GROUP"]))
                        {
                            dr["AMOUNT"] = edit.Rows[i]["REQUEST_AMOUNT"].ToString();
                            dr["AUTHOR"] = edit.Rows[i]["AUTHORNO"].ToString();
                            dr["LOANNO"] = edit.Rows[i]["LOANNO"].ToString();
                        }
                        else
                        {
                            dr["AMOUNT"] = string.Empty;
                            dr["AUTHOR"] = string.Empty;
                            dr["LOANNO"] = string.Empty;
                        }

                        dt.Rows.Add(dr);
                    }
                    lvAuthor.DataSource = dt;
                    lvAuthor.DataBind();

                    for (int i = 0; i < Convert.ToInt32(txtMaxLimit.Text.ToString()); i++)
                    {
                        DropDownList ddlAuthor = lvAuthor.Items[i].FindControl("ddlAuthor") as DropDownList;

                        ddlAuthor.Items.Clear();
                        ddlAuthor.Items.Add("Please Select");
                        ddlAuthor.SelectedItem.Value = "0";

                        ddlAuthor.DataSource = dsDropDown.Tables[0];
                        ddlAuthor.DataValueField = "UA_NO";
                        ddlAuthor.DataTextField = "UA_FULLNAME";
                        ddlAuthor.DataBind();

                        if (Convert.ToString(ViewState["DYNAMIC_TABLE"]) != string.Empty || Convert.ToString(ViewState["DYNAMIC_TABLE"]) != null)
                        {
                            edit = (DataTable)ViewState["DYNAMIC_TABLE"];
                        }
                        dt = this.CreateDataTable();
                        DataRow dr = dt.NewRow();
                        if (ViewState["action"].ToString().Equals("edit") && Convert.ToInt32(txtMaxLimit.Text.ToString()) == Convert.ToInt32(ViewState["NUMBER_OF_GROUP"]))
                        {
                            ddlAuthor.SelectedValue = edit.Rows[i]["AUTHORNO"].ToString();
                        }
                        else if (ViewState["action"].ToString().Equals("edit") && Convert.ToInt32(count) <= Convert.ToInt32(ViewState["NUMBER_OF_GROUP"]))
                        {
                            ddlAuthor.SelectedValue = edit.Rows[i]["AUTHORNO"].ToString();
                        }
                    }
                }
                else
                {
                    lvAuthor.DataSource = null;
                    lvAuthor.DataBind();
                    objCommon.DisplayMessage(Page, "Please Select Study Level", this.Page);
                }
            }
            else
            {
                lvAuthor.DataSource = null;
                lvAuthor.DataBind();
                objCommon.DisplayMessage(Page, "Please Select Faculty", this.Page);
            }
        }
        catch (Exception ex)
        {

        }
    }
    private DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("AUTHOR", typeof(string)));
        dt.Columns.Add(new DataColumn("LOANNO", typeof(string)));
        return dt;
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlUgPg, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_UA_SECTION B ON A.UGPGOT = B.UA_SECTION", "DISTINCT UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0 AND COLLEGE_ID="+ ddlFaculty.SelectedValue, "UA_SECTION ASC");
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int exists = 0;

            if (ViewState["action"].ToString().Equals("add"))
            {
                exists = Convert.ToInt32(objCommon.LookUp("ACD_LOAN_WORKFLOW", "COUNT(1)", "COLLEGE_ID=" + Convert.ToInt32(ddlFaculty.SelectedValue) + " AND UGPG=" + Convert.ToInt32(ddlUgPg.SelectedValue)));
                if (exists > 0)
                {
                    lvAuthor.DataSource = null;
                    lvAuthor.DataBind();

                    objCommon.DisplayMessage(Page, "Record Already Exists !!!", this.Page);
                    return;
                }
            }

            foreach (ListViewDataItem row in lvAuthor.Items)
            {
                TextBox txtAmount = row.FindControl("txtAmount") as TextBox;
                DropDownList ddlAuthor = row.FindControl("ddlAuthor") as DropDownList;
                Label lblLoanNo = row.FindControl("lblLoanNo") as Label;
                //if (txtAmount.Text.Trim() == string.Empty || txtAmount.Text.Trim() == "")
                //{
                //    ViewState["action"] = "add";
                //    objCommon.DisplayMessage(Page, "Please Enter Request Amount For All Fields !!!", this.Page);
                //    return;
                //}
                //if (ddlAuthor.SelectedValue == "0")
                //{
                //    ViewState["action"] = "add";
                //    objCommon.DisplayMessage(Page, "Please Author For All Fields !!!", this.Page);
                //    return;
                //}
                
                    string SP_Name1 = "PKG_ACD_GET_SUBMIT_LOAN_WORKFLOW";
                    string SP_Parameters1 = "@P_COLLEGE_ID,@P_UGPG,@P_MAX_LIMIT,@P_UA_NO,@P_AMOUNT,@P_AUTHORNO,@P_LOANNO,@P_COMMAND_TYPE";
                    string Call_Values1 = "" + Convert.ToInt32(ddlFaculty.SelectedValue) + "," + ddlUgPg.SelectedValue + "," + txtMaxLimit.Text + "," + Convert.ToInt32(Session["userno"]) +
                   "," + txtAmount.Text + "," + ddlAuthor.SelectedValue + "," + lblLoanNo.Text + ",2";
                
                string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 2);

                if (que_out1 != "-99")
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        lvAuthor.DataSource = null;
                        lvAuthor.DataBind();

                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(Page, "Record Saved Successfully.", this.Page);
                    }
                    else
                    {
                        lvAuthor.DataSource = null;
                        lvAuthor.DataBind();

                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(Page, "Record Updated Successfully.", this.Page);
                    }
                }
            }
            ddlFaculty.SelectedValue = null;
            ddlUgPg.SelectedValue = null;
            txtMaxLimit.Text = "";
            BindListView();
        }
        catch (Exception ex)
        {

        }
    }

    private void BindListView()
    {
        try
        {
            string SP_Name1 = "PKG_ACD_GET_SUBMIT_LOAN_WORKFLOW";
            string SP_Parameters1 = "@P_COLLEGE_ID,@P_UGPG,@P_COMMAND_TYPE";
            string Call_Values1 = "" + Convert.ToInt32(ddlFaculty.SelectedValue) + "," + ddlUgPg.SelectedValue + ",1";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvLoanFlow.DataSource = ds;
                lvLoanFlow.DataBind();
            }
            else
            {
                lvLoanFlow.DataSource = null;
                lvLoanFlow.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEdit = sender as LinkButton;

            string SP_Name1 = "PKG_ACD_GET_SUBMIT_LOAN_WORKFLOW";
            string SP_Parameters1 = "@P_LOANNO,@P_COMMAND_TYPE";
            string Call_Values1 = "" + btnEdit.CommandArgument.ToString() + ",3";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);

            ViewState["action"] = "edit";
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables != null)
            {
                ddlFaculty.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["COLLEGE_ID"]);
                ddlFaculty_SelectedIndexChanged(new object(), new EventArgs());
                ddlUgPg.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["UGPG"]);
                txtMaxLimit.Text = Convert.ToString(ds.Tables[0].Rows[0]["MAX_LIMIT"]);
                ViewState["NUMBER_OF_GROUP"] = Convert.ToString(ds.Tables[0].Rows[0]["MAX_LIMIT"]);
                ddlFaculty.Enabled = false; ddlUgPg.Enabled = false; 

                ViewState["DYNAMIC_TABLE"] = ds.Tables[0];
                txtMaxLimit_TextChanged(new object(), new EventArgs());
            }
        }
        catch (Exception ex)
        {

        }
    }
}