using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;

public partial class Slot_Booking_Creation : System.Web.UI.Page
{
    PhdController objphdstu = new PhdController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
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
            if (Session["userno"] == null || Session["username"] == null || Session["idno"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                this.CheckPageAuthorization();
                if (Session["usertype"].ToString() == "1" && Session["username"].ToString().ToUpper().Contains("PRINCIPAL"))
                {
                    // divShow.Visible=true;
                }
                else
                {
                    // divShow.Visible=false;
                }


                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["Edit"] = "add";
                ListViewBind();
                objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "DISTINCT  BATCHNO", "BATCHNAME", "ACTIVE=1", "BATCHNO desc");
                objCommon.FillListBox(ddlActivity, "ACD_SLOT_ACTIVITY_MASTER", "DISTINCT  ACTIVITY_NO", "ACTIVITY_NAME", "", "ACTIVITY_NO ");
                //objCommon.FillDropDownList(ddlfacultyOffer, "ACD_PHD_STUDENT_SCRUTINY_APPLICATION   SSA INNER JOIN ACD_PHD_STUDENT_SCRUTINY_INTERVIEW_SCHEDULE  SSI ON  (SSA.USERNO=SSI.USERNO) INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID=SSA.COLLEGE_ID)", "DISTINCT  SSA.COLLEGE_ID", "COLLEGE_NAME", "ISNULL(FILE_UPLOAD_STATUS,0)=1", "COLLEGE_NAME");
                //objCommon.SetLabelData("0");//for label
                //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
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
                Response.Redirect("~/notauthorized.aspx?page=Slot_Booking_Creation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Slot_Booking_Creation.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string Activity = string.Empty;
        foreach (ListItem items in ddlActivity.Items)
        {
            if (items.Selected == true)
            {
                Activity += items.Value + '$';

            }
        }
        if (Activity == string.Empty)
        {
            objCommon.DisplayMessage(this, "Please Select Activity", this.Page);
            return;
        }
        Activity = Activity.Substring(0, Activity.Length - 1);
        int Active = 0;
        if (hfdStatActi.Value == "true")
        {
            Active = 1;
        }
        DateTime date = Convert.ToDateTime(txtDate.Text);
        int Slot = 0;
        if (ViewState["Edit"].ToString() != "add")
        {
            Slot = Convert.ToInt32(ViewState["Edit"].ToString());
        }
        string SP_Name1 = "PKG_INSERT_SLOT_BOOKING_CREATION";
        string SP_Parameters1 = "@P_SLOT_NO ,@P_ADMBATCH,@P_SLOT_DATE,@P_START_SLOT,@P_END_SLOT,@P_ACTIVITY_NO,@P_CAPACITY,@P_STATUS,@P_UA_NO,@P_OUT";
        string Call_Values1 = "" + Slot + "," + Convert.ToInt32(ddlIntake.SelectedValue) + "," + Convert.ToDateTime(txtDate.Text)
            + "," + txttime.Text.ToString().Split('-')[0] + "," + txttime.Text.ToString().Split('-')[1] + "," + Activity + "," + Convert.ToInt32(txtCapacity.Text) + "," + Active + "," + Convert.ToInt32(Session["userno"]) + "," + 0 + "";

        string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//grade allotment
        if (que_out1 == "1")
        {
            ListViewBind();
            Clear();
            objCommon.DisplayMessage(this, "Record Saved Successfully !!", this.Page);
        
            // GetStudentList();
        }
        else if (que_out1 == "2")
        {
            ViewState["Edit"] = "add";
            ListViewBind();
            Clear();
            objCommon.DisplayMessage(this, "Record Update Successfully  !!", this.Page);
            return;
        }
        else if (que_out1 == "5")
        {
            objCommon.DisplayMessage(this, "Alredy Slot Alloted this Date And Time !!", this.Page);
            return;
        }
        else 
        {
            Clear();
            objCommon.DisplayMessage(this, "Server Error !!", this.Page);
            return;
        }
    }
    protected void Clear()
    {
        ddlActivity.Items.Clear();
        objCommon.FillListBox(ddlActivity, "ACD_SLOT_ACTIVITY_MASTER", "DISTINCT  ACTIVITY_NO", "ACTIVITY_NAME", "", "ACTIVITY_NO ");
        ddlIntake.SelectedValue = "0";
        txtCapacity.Text = "";
        txtDate.Text = "";
        txttime.Text = "";
    }
    protected void ListViewBind()
    {
        string SP_Name2 = "PKG_GET_SLOT_BOOKING_CREATION";
        string SP_Parameters2 = "@P_SLOT";
        string Call_Values2 = "" + 0 + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (ds.Tables[0].Rows.Count > 0 || ds.Tables[0].Rows.Count == null)
        {
            lvSlotCreate.DataSource = ds;
            lvSlotCreate.DataBind();

        }
        else
        {
            lvSlotCreate.DataSource = null;
            lvSlotCreate.DataBind();
            objCommon.DisplayMessage(this, "Record Not Found", this.Page);
            return;
        }

    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl.ToString());
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        LinkButton Edit = sender as LinkButton;
        int slot = int.Parse(Edit.CommandArgument);
        ViewState["Edit"] = slot;
        DataSet ds = objCommon.FillDropDown("ACD_SLOT_BOOKING_CREATION", "SLOT_NO,*", "CONVERT(VARCHAR, SLOT_DATE, 23) AS SLOTS_DATE", "SLOT_NO=" + slot, "");
        if (ds.Tables[0].Rows.Count > 0 || ds.Tables[0].Rows.Count == null)
        {
            ddlIntake.SelectedValue = ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
            txtDate.Text = ds.Tables[0].Rows[0]["SLOTS_DATE"].ToString();
            txtCapacity.Text = ds.Tables[0].Rows[0]["CAPACITY"].ToString();
            string[] couser = Convert.ToString(ds.Tables[0].Rows[0]["ACTIVITY_NO"]).Split(',');

            foreach (string s in couser)
            {
                foreach (ListItem item in ddlActivity.Items)
                {
                    if (s == item.Value)
                    {
                        item.Selected = true;
                        break;
                    }

                }
            }
            if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "Src", "SetStat(true);", true);
            }
            else
            {

                ScriptManager.RegisterStartupScript(this.Page, GetType(), "Src", "SetStat(false);", true);
            }
            ScriptManager.RegisterClientScriptBlock(updPresent, updPresent.GetType(), "Src", "Setdate('" + ds.Tables[0].Rows[0]["START_SLOT"].ToString() + " - " + ds.Tables[0].Rows[0]["END_SLOT"].ToString() + "');", true);
        }
    }
}