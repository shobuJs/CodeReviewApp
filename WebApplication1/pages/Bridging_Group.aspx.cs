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
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessLogic;

public partial class Projects_Bridging_Group : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController stud = new StudentController();
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
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = "add";
                    objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "", "BATCHNO DESC");
                    objCommon.FillDropDownList(ddlcriteria, "ACD_GROUPING_AUTOMATION_CRITERIA", " DISTINCT CRITERIA_ID", "CRITERIA_NAME", "CRITERIA_ID>0", "CRITERIA_NAME");
                    objCommon.FillDropDownList(ddlGroupType, "ACD_ORIANTATION_GROUP_MASTER", "ORIANTATION_NO", "ORIANTATION_TYPE", "ORIANTATION_NO > 0", "");
                    BindListViewBridgingGroup();
                    objCommon.SetLabelData("0");//for label
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));


                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Bridging_Group.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Bridging_Group.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try 
        {
            int status = 0;
            if (hfbgroup.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            int Intake = Convert.ToInt32(ddlIntake.SelectedValue);
            int Faculty = Convert.ToInt32(ddlFaculty.SelectedValue);
            int Grouptype = Convert.ToInt32(ddlGroupType.SelectedValue);
            int NoofGroups = Convert.ToInt32(txtcapacity.Text);
            int exists = 0;

            if (ViewState["action"].ToString().Equals("add"))
            {
                exists = Convert.ToInt32(objCommon.LookUp("ACD_BRIDGING_GROUP", "COUNT(1)", "CRITERIA_NO=" +Convert.ToInt32(ddlcriteria.SelectedValue) +"AND ADMBATCH=" + Convert.ToInt32(ddlIntake.SelectedValue) + " AND GROUPTYPE=" + Convert.ToInt32(ddlGroupType.SelectedValue)));
                if (exists > 0)
                {
                    lvAddNewGroups.DataSource = null;
                    lvAddNewGroups.DataBind();

                    objCommon.DisplayMessage(Page, "Record Already Exists !!!", this.Page);
                    return;
                }
            }

                DataTable dt = new DataTable();
                dt = this.CreateDataTable();

                foreach (ListViewDataItem row in lvAddNewGroups.Items)
                {
                    Label lblSrno = row.FindControl("lblSrno") as Label;
                    Label lblgroupCode = row.FindControl("lblgroupCode") as Label;
                    TextBox txtGroupName = row.FindControl("txtGroupName") as TextBox;
                    TextBox txtCapacity = row.FindControl("txtCapacity") as TextBox;
                    Label lblGroupType = row.FindControl("lblGroupType") as Label;
                    Label uniqueid = row.FindControl("lblUniqueid") as Label;
                    Label lblLock = row.FindControl("lblLock") as Label;

                    DataRow dr = dt.NewRow();
                    dr["SRNO"] = lblSrno.Text;
                    dr["GROUPCODE"] = lblgroupCode.Text.ToString().Substring(0, lblgroupCode.Text.Length - 1);
                    dr["GROUPNAME"] = txtGroupName.Text;
                    dr["CAPACITY"] = txtCapacity.Text;
                    dr["GROUPTYPE"] = lblGroupType.Text;
                    dr["UNIQUE_ID"] = uniqueid.Text;
                    dr["LOCK"] = lblLock.Text;

                    dt.Rows.Add(dr);
                }

                CustomStatus cs = (CustomStatus)stud.InsertBridgingGroup(Convert.ToInt32(ddlcriteria.SelectedValue),Intake, Faculty, Grouptype,NoofGroups, status,Convert.ToInt32(Session["userno"]),dt);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        lvAddNewGroups.DataSource = null;
                        lvAddNewGroups.DataBind();

                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(Page, "Record Saved Successfully.", this.Page);
                    }
                    else
                    {
                        lvAddNewGroups.DataSource = null;
                        lvAddNewGroups.DataBind();

                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(Page, "Record Updated Successfully.", this.Page);
                    }
                   
                    clear();
                    BindListViewBridgingGroup();
                }
       }
        catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindListViewBridgingGroup()
    {
        try
        {
            DataSet ds = stud.BindLVBridgingGroup(0,0,0,0);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvBridgingGroup.DataSource = ds;
                lvBridgingGroup.DataBind();
            }
            foreach (ListViewDataItem dataitem in lvBridgingGroup.Items)
            {
                Label Status = dataitem.FindControl("lblstatus") as Label;
                string Statuss = (Status.Text.ToString());
                if (Statuss == "In-Active")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }
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
    protected void clear()
    {
        ddlcriteria.SelectedIndex = 0;
        ddlIntake.SelectedIndex = 0;
        ddlFaculty.SelectedIndex = 0;
        ddlGroupType.SelectedIndex = 0;
        txtGroupName.Text = string.Empty;
        txtcapacity.Text = string.Empty;
        txtPrefix.Text = string.Empty;
        ddlIntake.Enabled = true; ddlFaculty.Enabled = true; ddlGroupType.Enabled = true; txtPrefix.Enabled = true;
        lvAddNewGroups.DataSource = null;
        lvAddNewGroups.DataBind();
        btnLock.Visible = false;
        ViewState["NUMBER_OF_GROUP"] = null; ViewState["DYNAMIC_TABLE"] = null;
        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEdit = sender as LinkButton;
            DataSet ds = stud.BindLVBridgingGroup(Convert.ToInt32(btnEdit.CommandArgument.ToString().Split(',')[0]), Convert.ToInt32(btnEdit.CommandArgument.ToString().Split(',')[1]), Convert.ToInt32(btnEdit.CommandArgument.ToString().Split(',')[2]), Convert.ToInt32(btnEdit.CommandArgument.ToString().Split(',')[3]));
            ViewState["action"] = "edit";
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables != null)
            {
                btnLock.Visible = true;
                ddlIntake.SelectedValue = Convert.ToString(btnEdit.CommandArgument.ToString().Split(',')[0]);
                ddlIntake_SelectedIndexChanged(new object(), new EventArgs());
                ddlFaculty.SelectedValue = Convert.ToString(btnEdit.CommandArgument.ToString().Split(',')[1]);
                ddlGroupType.SelectedValue = Convert.ToString(btnEdit.CommandArgument.ToString().Split(',')[2]);
                ddlcriteria.SelectedValue = Convert.ToString(btnEdit.CommandArgument.ToString().Split(',')[3]);
                txtcapacity.Text = Convert.ToString(btnEdit.CommandArgument.ToString().Split(',')[4]);
                ViewState["NUMBER_OF_GROUP"] = Convert.ToString(btnEdit.CommandArgument.ToString().Split(',')[4]);
                txtPrefix.Text = ds.Tables[1].Rows[0]["GROUPCODE"].ToString();
                ddlIntake.Enabled = false; ddlFaculty.Enabled = false; ddlGroupType.Enabled = false; txtPrefix.Enabled = false;

                if (Convert.ToInt32(btnEdit.CommandArgument.ToString().Split(',')[5]).Equals(1))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(false);", true);
                }
                ViewState["DYNAMIC_TABLE"] = ds.Tables[1];
                txtcapacity_TextChanged(new object(), new EventArgs());
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
    protected void ddlIntake_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER ", "COLLEGE_ID", "COLLEGE_NAME", "", "COLLEGE_ID" );
        objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        lvAddNewGroups.DataSource = null;
        lvAddNewGroups.DataBind();
        txtcapacity.Text = "";
    }
    protected void txtcapacity_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lvAddNewGroups.DataSource = null;
            lvAddNewGroups.DataBind();
            DataTable dt = new DataTable();
            string Group_Type = ""; int count = 0;
            if (txtcapacity.Text.Trim() == string.Empty)
            {
                lvAddNewGroups.DataSource = null;
                lvAddNewGroups.DataBind();
                objCommon.DisplayMessage(Page, "Please Enter No. of Groups", this.Page);
                return;
            }
            if (txtPrefix.Text.Trim() == string.Empty)
            {
                lvAddNewGroups.DataSource = null;
                lvAddNewGroups.DataBind();
                objCommon.DisplayMessage(Page, "Please Enter Group preFix", this.Page);
                return;
            }
            if (ddlcriteria.SelectedIndex > 0)
            {
                if (ddlGroupType.SelectedIndex > 0)
                {
                    DataTable edit = new DataTable();
                    if (Convert.ToString(ViewState["DYNAMIC_TABLE"]) != string.Empty || Convert.ToString(ViewState["DYNAMIC_TABLE"]) != null)
                    {
                        edit = (DataTable)ViewState["DYNAMIC_TABLE"];
                        if (ViewState["action"].ToString().Equals("edit") && Convert.ToInt32(txtcapacity.Text.ToString()) < Convert.ToInt32(ViewState["NUMBER_OF_GROUP"]))
                        {
                            txtcapacity.Text = "";
                            objCommon.DisplayMessage(Page, "Number of groups can not be less than " + ViewState["NUMBER_OF_GROUP"] + " !!!", this.Page);
                            return;
                        }
                    }

                    Group_Type = txtPrefix.Text.Trim();
                    //Group_Type = objCommon.LookUp("ACD_COLLEGE_MASTER", "DISTINCT GROUP_CODE", "isnull(ACTIVE,0)=1");

                    //if (Group_Type.ToString() == string.Empty || Group_Type.ToString() == "")
                    //{
                    //    lvAddNewGroups.DataSource = null;
                    //    lvAddNewGroups.DataBind();
                    //    objCommon.DisplayMessage(Page, "Group Code Not Define !!!", this.Page);
                    //    return;
                    //}

                    dt = this.CreateDataTable();

                    for (int i = 0; i < Convert.ToInt32(txtcapacity.Text.ToString()); i++)
                    {
                        count++;
                        DataRow dr = dt.NewRow();
                        dr["SRNO"] = count;
                        if (ViewState["action"].ToString().Equals("add"))
                        {
                            dr["GROUPCODE"] = Group_Type + count;
                            dr["GROUPNAME"] = Group_Type + count;
                            dr["CAPACITY"] = string.Empty;
                            dr["UNIQUE_ID"] = 0;
                            dr["LOCK"] = 0;
                            ViewState["NUMBER_OF_GROUP"] = "0";
                        }
                        else if (ViewState["action"].ToString().Equals("edit") && Convert.ToInt32(txtcapacity.Text.ToString()) == Convert.ToInt32(ViewState["NUMBER_OF_GROUP"]))
                        {
                            dr["GROUPCODE"] = edit.Rows[i]["GROUPCODE"].ToString();
                            dr["GROUPNAME"] = edit.Rows[i]["GROUPNAME"].ToString();
                            dr["CAPACITY"] = edit.Rows[i]["CAPACITY"].ToString();
                            dr["UNIQUE_ID"] = edit.Rows[i]["BGROUPID"].ToString();
                            dr["LOCK"] = edit.Rows[i]["LOCK"].ToString();
                        }
                        else if (ViewState["action"].ToString().Equals("edit") && Convert.ToInt32(count) <= Convert.ToInt32(ViewState["NUMBER_OF_GROUP"]))
                        {
                            dr["GROUPCODE"] = edit.Rows[i]["GROUPCODE"].ToString();
                            dr["GROUPNAME"] = edit.Rows[i]["GROUPNAME"].ToString();
                            dr["CAPACITY"] = edit.Rows[i]["CAPACITY"].ToString();
                            dr["UNIQUE_ID"] = edit.Rows[i]["BGROUPID"].ToString();
                            dr["LOCK"] = edit.Rows[i]["LOCK"].ToString();
                        }
                        else
                        {
                            dr["GROUPCODE"] = Group_Type + count;
                            dr["GROUPNAME"] = Group_Type + count;
                            dr["CAPACITY"] = string.Empty;
                            dr["UNIQUE_ID"] = 0;
                            dr["LOCK"] = 0;
                            //ViewState["NUMBER_OF_GROUP"] = "0";
                        }
                        dr["GROUPTYPE"] = ddlGroupType.SelectedItem.Text;

                        dt.Rows.Add(dr);
                    }
                    lvAddNewGroups.DataSource = dt;
                    lvAddNewGroups.DataBind();
                }
                else
                {
                    lvAddNewGroups.DataSource = null;
                    lvAddNewGroups.DataBind();
                    objCommon.DisplayMessage(Page, "Please Select Group Type", this.Page);
                }
            }
            else
            {
                lvAddNewGroups.DataSource = null;
                lvAddNewGroups.DataBind();
                objCommon.DisplayMessage(Page, "Please Select Criteria Name", this.Page);
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
        dt.Columns.Add(new DataColumn("GROUPCODE", typeof(string)));
        dt.Columns.Add(new DataColumn("GROUPNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("CAPACITY", typeof(string)));
        dt.Columns.Add(new DataColumn("GROUPTYPE", typeof(string)));
        dt.Columns.Add(new DataColumn("UNIQUE_ID", typeof(int)));
        dt.Columns.Add(new DataColumn("LOCK", typeof(int)));
        return dt;
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                int status = 0;
                if (hfbgroup.Value == "true")
                {
                    status = 1;
                }
                else
                {
                    status = 0;
                }
                int Intake = Convert.ToInt32(ddlIntake.SelectedValue);
                int Faculty = Convert.ToInt32(ddlFaculty.SelectedValue);
                int Grouptype = Convert.ToInt32(ddlGroupType.SelectedValue);
                int NoofGroups = Convert.ToInt32(txtcapacity.Text);
                int exists = 0;

                if (ViewState["action"].ToString().Equals("add"))
                {
                    exists = Convert.ToInt32(objCommon.LookUp("ACD_BRIDGING_GROUP", "COUNT(1)", "ADMBATCH=" + Convert.ToInt32(ddlIntake.SelectedValue) + " AND CRITERIA_NO=" + Convert.ToInt32(ddlcriteria.SelectedValue) + " AND GROUPTYPE=" + Convert.ToInt32(ddlGroupType.SelectedValue)));
                    if (exists > 0)
                    {
                        lvAddNewGroups.DataSource = null;
                        lvAddNewGroups.DataBind();

                        objCommon.DisplayMessage(Page, "Record Already Exists !!!", this.Page);
                        return;
                    }
                }

                DataTable dt = new DataTable();
                dt = this.CreateDataTable();

                foreach (ListViewDataItem row in lvAddNewGroups.Items)
                {
                    Label lblSrno = row.FindControl("lblSrno") as Label;
                    Label lblgroupCode = row.FindControl("lblgroupCode") as Label;
                    TextBox txtGroupName = row.FindControl("txtGroupName") as TextBox;
                    TextBox txtCapacity = row.FindControl("txtCapacity") as TextBox;
                    Label lblGroupType = row.FindControl("lblGroupType") as Label;
                    Label uniqueid = row.FindControl("lblUniqueid") as Label;
                    Label lblLock = row.FindControl("lblLock") as Label;

                    DataRow dr = dt.NewRow();
                    dr["SRNO"] = lblSrno.Text;
                    dr["GROUPCODE"] = lblgroupCode.Text;
                    dr["GROUPNAME"] = txtGroupName.Text;
                    dr["CAPACITY"] = txtCapacity.Text;
                    dr["GROUPTYPE"] = lblGroupType.Text;
                    dr["UNIQUE_ID"] = uniqueid.Text;
                    dr["LOCK"] = 1;

                    dt.Rows.Add(dr);
                }

                CustomStatus cs = (CustomStatus)stud.InsertBridgingGroup(Convert.ToInt32(ddlcriteria.SelectedValue),Intake, Faculty, Grouptype, NoofGroups, status, Convert.ToInt32(Session["userno"]), dt);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        lvAddNewGroups.DataSource = null;
                        lvAddNewGroups.DataBind();

                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(Page, "Record Saved Successfully.", this.Page);
                    }
                    else
                    {
                        lvAddNewGroups.DataSource = null;
                        lvAddNewGroups.DataBind();

                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(Page, "Record Updated Successfully.", this.Page);
                    }

                    clear();
                    BindListViewBridgingGroup();
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
        catch (Exception ex)
        {

        }
    }
}