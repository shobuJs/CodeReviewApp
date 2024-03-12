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

public partial class ACADEMIC_ScholarshipApproval : System.Web.UI.Page
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
                        BindListView();
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

            Response.Redirect("~/notauthorized.aspx");
        }
    }
    protected void BindListView()
    {
        try
        {
            lvBindDetails.DataSource = null;
            lvBindDetails.DataBind();

            Common objCommon = new Common();

            string SP_Name = "PKG_ACD_BIND_SCHOLARSHIP_DROP_DOWN";
            string SP_Parameters = "@P_DYNAMIC_FILTER,@P_SESSIONNO,@P_SEMESTERNO,@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO";
            string Call_Values = "" + Convert.ToInt32(1) + ",0" + ",0" + ",0" + ",0" + ",0";

            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            ddlSession.Items.Clear();
            ddlSession.Items.Add("Please Select");
            ddlSession.SelectedItem.Value = "0";

            ddlCollege.Items.Clear();
            ddlCollege.Items.Add("Please Select");
            ddlCollege.SelectedItem.Value = "0";

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlSession.DataSource = ds.Tables[0];
                ddlSession.DataValueField = "SESSIONNO";
                ddlSession.DataTextField = "SESSION_NAME";
                ddlSession.DataBind();
            }
            if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
            {
                ddlCollege.DataSource = ds.Tables[2];
                ddlCollege.DataValueField = "COLLEGE_ID";
                ddlCollege.DataTextField = "COLLEGE_NAME";
                ddlCollege.DataBind();
            }

            ViewState["SEMESTER"] = null;
            ViewState["DEGREE"] = null;

            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                ViewState["SEMESTER"] = ds.Tables[1];
            }
            if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
            {
                ViewState["DEGREE"] = ds.Tables[3];
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvBindDetails.DataSource = null;
            lvBindDetails.DataBind();

            ddlSemester.Items.Clear();
            ddlSemester.Items.Add("Please Select");
            ddlSemester.SelectedItem.Value = "0";

            DataTable dt = ViewState["SEMESTER"] as DataTable;
            DataView dv = new DataView();

            if (dt != null)
            {
                dv = dt.DefaultView;
                dv.RowFilter = "SESSIONNO IN(" + ddlSession.SelectedValue + ")";

                ddlSemester.DataSource = dv;
                ddlSemester.DataValueField = "SEMESTERNO";
                ddlSemester.DataTextField = "SEMESTERNAME";
                ddlSemester.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvBindDetails.DataSource = null;
            lvBindDetails.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvBindDetails.DataSource = null;
            lvBindDetails.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvBindDetails.DataSource = null;
            lvBindDetails.DataBind();

            ddlProgram.Items.Clear();
            ddlProgram.Items.Add("Please Select");
            ddlProgram.SelectedItem.Value = "0";

            DataTable dt = ViewState["DEGREE"] as DataTable;
            DataView dv = new DataView();

            if (dt != null)
            {
                dv = dt.DefaultView;
                dv.RowFilter = "COLLEGE_ID IN(" + ddlCollege.SelectedValue + ")";

                ddlProgram.DataSource = dv;
                ddlProgram.DataValueField = "DEGREENO";
                ddlProgram.DataTextField = "DEGREENAME";
                ddlProgram.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            lvBindDetails.DataSource = null;
            lvBindDetails.DataBind();

            Common objCommon = new Common();

            string Call_Values = "";

            string SP_Name = "PKG_ACD_BIND_SCHOLARSHIP_DROP_DOWN";
            string SP_Parameters = "@P_DYNAMIC_FILTER,@P_SESSIONNO,@P_SEMESTERNO,@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO";
            if (ddlProgram.SelectedIndex > 0)
            {
                Call_Values = "" + Convert.ToInt32(2) + "," + ddlSession.SelectedValue + "," + ddlSemester.SelectedValue + "," + ddlCollege.SelectedValue + ","
                + ddlProgram.SelectedValue.Split('$')[0] + "," + ddlProgram.SelectedValue.Split('$')[1];
            }
            else
            {
                Call_Values = "" + Convert.ToInt32(2) + "," + ddlSession.SelectedValue + "," + ddlSemester.SelectedValue + "," + ddlCollege.SelectedValue + ","
                + 0 + "," + 0;
            }
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvBindDetails.DataSource = ds.Tables[0];
                lvBindDetails.DataBind();
            }
            else
            {
                lvBindDetails.DataSource = null;
                lvBindDetails.DataBind();

                objCommon.DisplayMessage(this, "Record Not Found !!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        { 
        
        }
    }
}