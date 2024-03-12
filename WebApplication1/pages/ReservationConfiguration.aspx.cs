using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Data.SqlClient;

public partial class ACADEMIC_ReservationConfiguration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    IITMS.UAIMS.BusinessLayer.BusinessEntities.Config objConfig = new IITMS.UAIMS.BusinessLayer.BusinessEntities.Config();

    #region PAGE
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

                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENAME");
                this.BindListView();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)

                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                ViewState["action"] = "add";
            }
        }
        divMsg.InnerHtml = string.Empty;
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }
    #endregion

    #region PRIVATE METHODS
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ReservationConfiguration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ReservationConfiguration.aspx");
        }
    }

    private void GetBranchName()
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            ddlBranchName.Items.Clear();
            objCommon.FillDropDownList(ddlBranchName, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
        }
        else
        {
            ddlBranchName.Items.Clear();
            ddlBranchName.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    private void ClearControls()
    {
        ddlDegree.SelectedIndex = 0;
        ddlBranchName.Items.Clear();
        ddlBranchName.Items.Add(new ListItem("Please Select", "0"));
        txtIntake.Text = string.Empty;
        txtSC.Text = string.Empty;
        txtST.Text = string.Empty;
        txtGen.Text = string.Empty;
        txtOBC.Text = string.Empty;
        ViewState["action"] = "add";
    }

    private void ShowDetails(Config objConfig)
    {
        try
        {
            SqlDataReader dr = objCommon.GetSingleReservationConfig(objConfig);
            if (dr != null)
            {
                if (dr.Read())
                {
                    ddlDegree.SelectedValue = dr["DEGREENO"] == null ? "0" : dr["DEGREENO"].ToString();
                    GetBranchName();
                    ddlBranchName.SelectedValue = dr["BRANCHNO"] == null ? "0" : dr["BRANCHNO"].ToString();
                    txtSC.Text = dr["SC"] == null ? string.Empty : dr["SC"].ToString();
                    txtST.Text = dr["ST"] == null ? string.Empty : dr["ST"].ToString();
                    txtGen.Text = dr["GEN"] == null ? string.Empty : dr["GEN"].ToString();
                    txtOBC.Text = dr["OBC"] == null ? string.Empty : dr["OBC"].ToString();
                    txtIntake.Text = dr["INTAKE"] == null ? string.Empty : dr["INTAKE"].ToString();
                }
            }
            if (dr != null) dr.Close();
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SessionCreate.ShowDetails_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_RESERVATION_CONFIG ", "CNFNO", "DBO.FN_DESC('DEGREENAME',DEGREENO)DEGREE,DBO.FN_DESC('BRANCHLNAME',BRANCHNO)BRANCH,INTAKE,SC,ST,GEN,OBC", string.Empty, "CNFNO");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvConfiguration.DataSource = ds;
                lvConfiguration.DataBind();
            }
            else
            {
                lvConfiguration.DataSource = null;
                lvConfiguration.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ReservationConfiguration.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }
    #endregion

    #region EVENTS
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetBranchName();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ReservationConfiguration.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtIntake.Text != string.Empty)
            {
                objConfig.Degree_No = Convert.ToInt32(ddlDegree.SelectedValue);
                objConfig.BranchNo = Convert.ToInt32(ddlBranchName.SelectedValue);
                objConfig.Intake = Convert.ToInt32(txtIntake.Text.Trim());
                objConfig.SCQuota = Convert.ToInt32(txtSC.Text.Trim());
                objConfig.STQuota = Convert.ToInt32(txtST.Text.Trim());
                objConfig.GENQuota = Convert.ToInt32(txtGen.Text.Trim());
                objConfig.OBCQuota = Convert.ToInt32(txtOBC.Text.Trim());

                //CODE TO NOT ALLOW SUM OF QUOTAS GREATER THAN INTAKE VALUE

                #region ForIntake
                int SC, ST, GEN, OBC, SUM = 0;
                SC = Convert.ToInt32(txtSC.Text);
                ST = Convert.ToInt32(txtST.Text);
                GEN = Convert.ToInt32(txtGen.Text);
                OBC = Convert.ToInt32(txtOBC.Text);
                SUM = (SC + ST + GEN + OBC);
                #endregion

                if (SUM <= Convert.ToInt32(txtIntake.Text))
                {
                    //Check for add or edit
                    if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                    {
                        //Edit 
                        objConfig.Cnfno = Convert.ToInt32(ViewState["CNFNO"]);
                        CustomStatus cs = (CustomStatus)objCommon.UpdateResevationConfig(objConfig);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage(updLists, "Record Updated Successfully!!", this.Page);
                            ClearControls();
                            this.BindListView();
                            ViewState["action"] = "add";
                        }
                        else
                        {
                            objCommon.DisplayMessage(updLists, "Failed To Update Record.", this.Page);
                        }
                    }
                    else
                    {
                        //Add New
                        CustomStatus cs = (CustomStatus)objCommon.AddResevationConfig(objConfig);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(updLists, "Record Saved Successfully!!", this.Page);
                            ClearControls();
                            this.BindListView();
                        }
                        else
                        {
                            objCommon.DisplayMessage(updLists, "Details Are Already Exists For Selected Branch " + ddlBranchName.SelectedItem.Text + " Of Degree " + ddlDegree.SelectedItem.Text + "", this.Page);
                        }
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updLists, "Please Reserve Quota In Between The Range Of Intake!!", this.Page);
                    txtIntake.Focus();
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Enter Intake", this.Page);
                txtIntake.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ReservationConfiguration.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            objConfig.Cnfno = int.Parse(btnEdit.CommandArgument);
            ViewState["CNFNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["edit"] = "edit";
            this.ShowDetails(objConfig);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReservationConfiguration.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    #endregion

}