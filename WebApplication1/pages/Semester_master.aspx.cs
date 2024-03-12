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
public partial class ACADEMIC_Semester_master : System.Web.UI.Page
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
            objCommon.FillDropDownList(ddlcurrtype, "ACD_SCHEMETYPE", "SCHEMETYPENO", "SCHEMETYPE", "SCHEMETYPENO>0", "SCHEMETYPENO");
            Bindlist();

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
            if (ViewState["action"] != null)
            {
                int active = 0;
                if (hfdStat.Value == "true")
                    active = 1;
                else
                    active = 0;
                if (ViewState["action"].ToString().Equals("add"))
                {
                    string SP_Name1 = "PKG_ACD_INSERT_UPDATE_SEMESTER_MASTER";
                    string SP_Parameters1 = "@P_SEM_NAME,@P_SEM_FULLNAME,@P_CURR_TYPE,@P_ACTIVE,@P_COMMAND_TYPE,@P_SEMESTERNO,@P_OUTPUT";
                    string Call_Values1 = "" + Convert.ToString(txtsemname.Text) + "," + Convert.ToString(txtsemfullname.Text) + "," + 
                        Convert.ToInt32(ddlcurrtype.SelectedValue) + "," + active + "," + 1 + "," + 0 + ",0";

                    string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//insert
                    if (que_out1 == "1")
                    {
                        objCommon.DisplayMessage(this, "Record Saved Successfully !!", this.Page);
                        Bindlist();
                        clear();
                    }
                    else if (que_out1 == "3")
                    {
                        objCommon.DisplayMessage(this, "Record Already Exists !!", this.Page);
                        Bindlist();
                        clear();
                    }
                }
                else if (ViewState["action"].ToString().Equals("edit"))
                {
                    string SP_Name1 = "PKG_ACD_INSERT_UPDATE_SEMESTER_MASTER";
                    string SP_Parameters1 = "@P_SEM_NAME,@P_SEM_FULLNAME,@P_CURR_TYPE,@P_ACTIVE,@P_COMMAND_TYPE,@P_SEMESTERNO,@P_OUTPUT";
                    string Call_Values1 = "" + Convert.ToString(txtsemname.Text) + "," + Convert.ToString(txtsemfullname.Text) + "," +
                        Convert.ToInt32(ddlcurrtype.SelectedValue) + "," + active + "," + 2 + "," + Convert.ToInt32(ViewState["SEMESTERNO"]) + ",0";

                    string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//insert
                    if (que_out1 == "2")
                    {
                        objCommon.DisplayMessage(this, "Record Updated Successfully !!", this.Page);
                        ViewState["action"] = "add";
                        Bindlist();
                        clear();
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void Bindlist()
    {
        string SP_Name1 = "PKG_ACD_INSERT_UPDATE_SEMESTER_MASTER";
        string SP_Parameters1 = "@P_SEM_NAME,@P_SEM_FULLNAME,@P_CURR_TYPE,@P_ACTIVE,@P_COMMAND_TYPE,@P_SEMESTERNO,@P_OUTPUT";
        string Call_Values1 = "" + Convert.ToString(txtsemname.Text) + "," + Convert.ToString(txtsemfullname.Text) + "," +
            Convert.ToInt32(ddlcurrtype.SelectedValue) + "," + 0 + "," + 3 + "," + 0 + ",0";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvSem.DataSource = ds;
            lvSem.DataBind();
        }
        else
        {
            lvSem.DataSource = null;
            lvSem.DataBind();
        }
    }
    private void clear()
    {
        ddlcurrtype.SelectedIndex = 0;
        txtsemfullname.Text = "";
        txtsemname.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["action"] = "edit";
            int SEMESTERNO = int.Parse(btnEdit.CommandArgument);
            ViewState["SEMESTERNO"] = SEMESTERNO;
            ShowDetails(SEMESTERNO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_CollegeMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int SEMESTERNO)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_SEMESTER", "SEMESTERNO,SEMESTERNAME,SEMFULLNAME,ODD_EVEN,COLLEGE_CODE,yearno,SCHEMETYPENO,isnull(STATUS,0)ACTIVE", "", "SEMESTERNO=" + SEMESTERNO, "SEMESTERNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcurrtype.SelectedValue = ds.Tables[0].Rows[0]["SCHEMETYPENO"].ToString();
                txtsemfullname.Text = ds.Tables[0].Rows[0]["SEMFULLNAME"].ToString();
                txtsemname.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();             
                if (ds.Tables[0].Rows[0]["ACTIVE"].ToString().Trim() == "1")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(true);", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(false);", true);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseCreation.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}