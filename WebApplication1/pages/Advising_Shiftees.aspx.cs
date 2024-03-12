using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;
using System.Web;
using IITMS.UAIMS;
using IITMS;

public partial class ACADEMIC_Advising_Shiftees : System.Web.UI.Page
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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }

                    //objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "", "BATCHNO DESC");
                  //  objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO");
                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "ISNULL(IS_ACTIVE,0)=1 AND SESSIONNO=(SELECT MAX(SESSIONNO) FROM ACD_SESSION_MASTER WHERE ISNULL(IS_ACTIVE,0)=1)", "SESSIONNO desc");
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "ISNULL(FLOCK,0)=1 ", "SESSIONNO desc");

                    objCommon.SetLabelData("");
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACDEMIC_COLLEGE_MASTER.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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

    protected void btnShow_Click(object sender, EventArgs e)
    {
        string SP_Name2 = "PKG_ACAD_SHIFTEES_DETAILS";
        string SP_Parameters2 = "@P_COMMAND_TYPE,@P_IDNO,@P_SESSIONNO";
        string Call_Values2 = ""+1+","+0+"," + Convert.ToInt32(ddlSession.SelectedValue)+"";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (ds.Tables[0].Rows.Count > 0 || ds.Tables[0].Rows.Count == null)
        {
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvStudent);

        }
        else
        {
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            objCommon.DisplayMessage(this, "Record not Found..", this.Page);
            return;

        }
    }
    protected void btncanceldata_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}