using System.Linq;
using System.Web;
using System.Xml.Linq;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Web.Configuration;


public partial class ApplicationFeesConfig : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }



            }
            objCommon.SetLabelData("0");//for label
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
            ViewState["action"] = "add";
            BindData();
            objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO");
            objCommon.FillDropDownList(ddlStudyLevel, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "UA_SECTION");
           // FilldropDown();
        }

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CollegeMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CollegeMaster.aspx");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int Active = 0;
            if (hfdStat.Value == "true")
                Active = 1;
            else
                Active = 0;
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int chkexit = Convert.ToInt32(objCommon.LookUp("ACD_APPLICATION_FEES_CONFIG", "COUNT(1)", "ADMBATCH_NO =" + ddlIntake.SelectedValue + "AND UGPG=" + ddlStudyLevel.SelectedValue));
                    if (chkexit > 0)
                    {
                        objCommon.DisplayMessage(UpdFees, "Records Already Exist!", this.Page);
                        Clear();
                        return;
                    }
                    else
                    {
                       
                        string SP_Name2 = "PKG_ACD_INSERT_APPLICATION_FEES";
                        string SP_Parameters2 = "@P_ADMBATCH_NO,@P_UGPG,@P_ADM_FEES,@P_ACTIVE,@P_UA_NO,@P_CONG_NO,@P_COMMAND_TYPE,@P_OUTPUT";
                        string Call_Values2 = "" + Convert.ToInt32(ddlIntake.SelectedValue) + "," + Convert.ToInt32(ddlStudyLevel.SelectedValue) + "," +
                            Convert.ToString(txtApplicationFees.Text) + "," + Convert.ToInt32(Active) + "," + Convert.ToInt32(Session["userno"]) + "," +
                            Convert.ToInt32(ViewState["CONFIG_NO"]) + "," + 1 + ",0";
                        string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name2, SP_Parameters2, Call_Values2, true, 1);//insert
                        if (que_out1 == "1")
                        {
                            objCommon.DisplayMessage(this.UpdFees, "Record Saved Successfully!", this.Page);
                            Clear();
                            BindData();
                        }
                        else
                        {

                            objCommon.DisplayMessage(this.UpdFees, "Error!!", this.Page);

                        }
                    }
                }
                else if (ViewState["action"].ToString().Equals("edit"))
                {
                    string SP_Name2 = "PKG_ACD_INSERT_APPLICATION_FEES";
                    string SP_Parameters2 = "@P_ADMBATCH_NO,@P_UGPG,@P_ADM_FEES,@P_ACTIVE,@P_UA_NO,@P_CONG_NO,@P_COMMAND_TYPE,@P_OUTPUT";
                    string Call_Values2 = "" + Convert.ToInt32(ddlIntake.SelectedValue) + "," + Convert.ToInt32(ddlStudyLevel.SelectedValue) + "," +
                        Convert.ToString(txtApplicationFees.Text) + "," + Convert.ToInt32(Active) + "," + Convert.ToInt32(Session["userno"]) + "," +
                        Convert.ToInt32(ViewState["CONFIG_NO"]) + "," + 1 + ",0";
                    string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name2, SP_Parameters2, Call_Values2, true, 1);//insert
                    if (que_out1 == "1")
                    {
                        objCommon.DisplayMessage(this.UpdFees, "Record Updated Successfully!", this.Page);
                        ViewState["action"] = "add";
                        Clear();
                        BindData();
                    }
                    else
                    {

                        objCommon.DisplayMessage(this.UpdFees, "Error!!", this.Page);

                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseCreation.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    private void Clear()
    {
        ddlIntake.SelectedIndex = 0;
        ddlStudyLevel.SelectedIndex = 0;
        txtApplicationFees.Text = "";
        //Panel1.Visible = false;
        //lvFees.DataSource = null;
        //lvFees.DataBind();
    }
    private void BindData()
    {
        string SP_Name2 = "PKG_ACD_INSERT_APPLICATION_FEES";
        string SP_Parameters2 = "@P_ADMBATCH_NO,@P_UGPG,@P_ADM_FEES,@P_ACTIVE,@P_UA_NO,@P_CONG_NO,@P_COMMAND_TYPE,@P_OUTPUT";
        string Call_Values2 = "" + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 2 + ",0";
        DataSet dsFacWiseCourseList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (dsFacWiseCourseList.Tables[0].Rows.Count > 0)
        {
            Panel1.Visible = true;
            lvFees.DataSource = dsFacWiseCourseList;
            lvFees.DataBind();
        }
        else
        {
            Panel1.Visible = false;
            lvFees.DataSource = null;
            lvFees.DataBind();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int CONFIG_NO = int.Parse(btnEdit.CommandArgument);
            ViewState["CONFIG_NO"] = CONFIG_NO;
            ShowDetails(CONFIG_NO);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_CollegeMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int CONFIG_NO)
    {
       
        try
        {

            DataSet ds = objCommon.FillDropDown("ACD_APPLICATION_FEES_CONFIG", "*,ISNULL(ACTIVE,0)ACTIVE1", "", "CONG_NO=" + CONFIG_NO, "CONG_NO");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlStudyLevel.SelectedValue = ds.Tables[0].Rows[0]["UGPG"].ToString();
                ddlIntake.SelectedValue = ds.Tables[0].Rows[0]["ADMBATCH_NO"].ToString();
                txtApplicationFees .Text = ds.Tables[0].Rows[0]["ADMISSION_FEES"].ToString();
                if (ds.Tables[0].Rows[0]["ACTIVE1"].ToString().Trim() == "1")
                {
                    //chkActive.Checked = true;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(true);", true);
                }
                else
                {
                    //chkActive.Checked = false;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(false);", true);
                }
                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Master_CollegeMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}