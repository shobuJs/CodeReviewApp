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
using System.Data;

public partial class ACADEMIC_Section_Update : System.Web.UI.Page
{
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
                // objCommon.FillDropDownList(ddlAcdSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "", "SESSIONNO desc");
                //  BindData();
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
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
                Response.Redirect("~/notauthorized.aspx?page=Section_Update.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Section_Update.aspx");
        }
    }

    protected void BindData()
    {
        try
        {
            string StudeId = txtStudId.Text;
            string SP_Name2 = "PKG_GET_SECTION_UPDATE_INFO";
            string SP_Parameters2 = "@P_ENROLLNO,@P_IDNO";
            string Call_Values2 = "" + StudeId.ToString() + "," + 0 + "";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (ds.Tables[1].Rows.Count > 0 || ds.Tables[1].Rows.Count == null)
            {
                ddlSection.Items.Clear();
                ddlSection.Items.Insert(0, new ListItem("Please Select", "0"));
                //ddlSection.Items.Clear();
                ddlSection.DataSource = ds.Tables[1];
                ddlSection.DataValueField = ds.Tables[1].Columns["BGROUPID"].ToString();
                ddlSection.DataTextField = ds.Tables[1].Columns["GROUP_NAME"].ToString();
                ddlSection.DataBind();
            }
            if (ds.Tables[0].Rows.Count > 0 || ds.Tables[0].Rows.Count == null)
            {
                DivSerch.Visible = false;
                DivData.Visible = true;
                DivButton.Visible = true;
                lblStudeId.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                lblFaculty.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                lvlStudeName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                lblProgram.Text = ds.Tables[0].Rows[0]["PROGRAM"].ToString();
                lblSemester.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                ViewState["IDNO"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
                ddlSection.SelectedValue = ds.Tables[0].Rows[0]["SECTION"].ToString();
            }
            else
            {
                objCommon.DisplayMessage(this, "Record Not Found.. !!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

        }

    }
    protected void btnSerchStud_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSection.SelectedValue == "0" || ddlSection.SelectedValue == "")
            {
                objCommon.DisplayMessage(this, "Please Select Section", this.Page);
                return;
            }
            int section = Convert.ToInt32(ddlSection.SelectedValue);
            string SP_Name1 = "PKG_GET_SECTION_UPDATE_INFO";
            string SP_Parameters1 = "@P_IDNO,@P_SECTIONNO,@P_UA_NO,@P_OUT";
            string Call_Values1 = "" + Convert.ToInt32(ViewState["IDNO"]) + "," + section + "," + Convert.ToInt32(Session["userno"]) + "," + 0 + "";

            string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);
            if (que_out1 == "1")
            {
                BindData();
                objCommon.DisplayMessage(this, "Record Saved Successfully.. !!", this.Page);
            }
            else if (que_out1 == "5")
            {
                BindData();
                objCommon.DisplayMessage(this, "Section Group Capacity is Full,Please Allot Another Section Group.. !!", this.Page);
                return;
            }
            else
            {
                objCommon.DisplayMessage(this, "Server Error.. !!", this.Page);
                return;
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
}