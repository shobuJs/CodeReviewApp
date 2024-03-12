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
using IITMS.SQLServer.SQLDAL;
using ClosedXML.Excel;
using System.Data.OleDb;

public partial class ACADEMIC_Screening_Configuration : System.Web.UI.Page
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
            FilldropDown();
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

    private void FilldropDown()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'') COLLEGE_NAME", "COLLEGE_ID > 0 and isnull(ACTIVE,0)=1", "COLLEGE_ID");
        objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "ISNULL(ACTIVE,0)=1", "BATCHNO DESC");
    }

    private void BindData()
    {
        try
        {
            string SP_Name3 = "PKG_ACD_GET_INSERT_SCREENING_CONFIGURATION";
            string SP_Parameters3 = "@P_COLLEGE_ID,@P_COMMAND_TYPE,@P_OUTPUT";
            string Call_Values3 = "" + Convert.ToInt32(ddlCollege.SelectedValue.ToString()) + "," + 1 + "," + 0 + "";
            DataSet Ds = objCommon.DynamicSPCall_Select(SP_Name3, SP_Parameters3, Call_Values3);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
                Panel1.Visible = true;
                lstscreen.Visible = true;
                btnSubmit.Visible = true;
                LvScreenList.DataSource = Ds;
                LvScreenList.DataBind();
            }
            else
            {
                Panel1.Visible = false;
                lstscreen.Visible = false;
                btnSubmit.Visible = false;
                LvScreenList.DataSource = Ds;
                LvScreenList.DataBind();
                objCommon.DisplayMessage(this.updScreening, "No Record Found!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Screening_Configuration.BindData() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string Screen = "";
            foreach (ListItem item in lstScreeningRequired.Items)
            {
                if (item.Selected == true)
                {
                    Screen += item.Value + '$';
                }
            }
            if (!string.IsNullOrEmpty(Screen))
            {
                Screen = Screen.Substring(0, Screen.Length - 1);
            }
            else
            {
                Screen = "0";
            }
            string Degreeno = "", Branchno = "";
            int Count = 0;
            foreach (ListViewDataItem dataitem in LvScreenList.Items)
            {
                CheckBox chkBox = dataitem.FindControl("chktransfer") as CheckBox;
                HiddenField hdfDegreeno = dataitem.FindControl("hdfDegreeno") as HiddenField;
                HiddenField hdfBranchno = dataitem.FindControl("hdfBranchno") as HiddenField;
                if (chkBox.Checked == true)
                {
                    Count++;
                    Degreeno += hdfDegreeno.Value + "$";
                    Branchno += hdfBranchno.Value + "$";
                }
            }
            Degreeno = Degreeno.TrimEnd('$');
            Branchno = Branchno.TrimEnd('$');
            if (Count == 0)
            {
                objCommon.DisplayMessage(this.updScreening, "Please Select at least one checkbox!", this.Page);
                return;
            }
            string SP_Name1 = "PKG_ACD_GET_INSERT_SCREENING_CONFIGURATION";
            string SP_Parameters1 = "@P_INTAKE,@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO,@P_SCREENO,@P_CREATED_BY,@P_COMMAND_TYPE,@P_OUTPUT";
            string Call_Values1 = "" + Convert.ToInt32(ddlIntake.SelectedValue) + "," + Convert.ToInt32(ddlCollege.SelectedValue.ToString()) + "," +
                Degreeno + "," + Branchno + "," + Screen + "," + Convert.ToInt32(Session["userno"].ToString()) + "," + 2 + ",0";

            string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//insert
            if (que_out1 == "1")
            {
                objCommon.DisplayMessage(this.updScreening, "Record Saved Successfully!", this.Page);
                lstScreeningRequired.ClearSelection();
                BindData();
                return;
            }
            else if (que_out1 == "3")
            {
                objCommon.DisplayMessage(this.updScreening, "Record Already Exists!", this.Page);
                lstScreeningRequired.ClearSelection();
                BindData();
                return;
            }
            else if (que_out1 == "7")
            {
                objCommon.DisplayMessage(this.updScreening, "Please Select Both University and Program Screening!", this.Page);
                lstScreeningRequired.ClearSelection();
                BindData();
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Screening_Configuration.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlCollege.SelectedIndex = 0;
        ddlIntake.SelectedIndex = 0;
        lstScreeningRequired.ClearSelection();
        Panel1.Visible = false;
        LvScreenList.DataSource = null;
        LvScreenList.DataBind();
        btnSubmit.Visible = false;
        lstscreen.Visible = false;
    }
}