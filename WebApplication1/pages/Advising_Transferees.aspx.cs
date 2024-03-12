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
public partial class Advising_Transferees : System.Web.UI.Page
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

                    objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "", "BATCHNO DESC");
                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_NAME");

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
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlProgram, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_DEGREE D  ON (CD.DEGREENO=D.DEGREENO)INNER JOIN  ACD_BRANCH B ON (B.BRANCHNO=CD.BRANCHNO) ", "DISTINCT CONVERT(NVARCHAR,D.DEGREENO)+','+CONVERT(NVARCHAR,CD.BRANCHNO) AS PROGRAMNO", "D.DEGREENAME+' - '+B.LONGNAME AS PROGRAM", "COLLEGE_ID=" + ddlCollege.SelectedValue, "");
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        string[] Split = ddlProgram.SelectedValue.Split(',');
        int Degreeno = Convert.ToInt32(Split[0]);
        int Branhcno = Convert.ToInt32(Split[1]);
        string SP_Name2 = "PKG_GET_ADVISING_TRANSFEREES_STUDENT";
        string SP_Parameters2 = "@P_ADMBATCH,@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO,@P_COMMAND_TYPE";
        string Call_Values2 = "" + Convert.ToInt32(ddlIntake.SelectedValue) + "," + Convert.ToInt32(ddlCollege.SelectedValue) + "," + Degreeno + "," + Branhcno + "," + 1 + "";
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