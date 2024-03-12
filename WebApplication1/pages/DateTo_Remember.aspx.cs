using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_DateTo_Remember : System.Web.UI.Page
{
    CourseController objCourse = new CourseController();
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
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                if (Session["usertype"].ToString() == "1" && Session["username"].ToString().ToUpper().Contains("PRINCIPAL"))
                {
                    // divShow.Visible=true;
                }
                else
                {
                    // divShow.Visible=false;
                }

                Page.Title = objCommon.LookUp("reff", "collegename", string.Empty);

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                }
                BindListView();
                ViewState["Edit"] = "NULL";
                //objCommon.SetLabelData("0");//for label
                //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string que_out1 = string.Empty;
        int HedNo = 0;
        foreach (ListViewDataItem dataitem in lvBankDetails.Items)
        {
            Label lvlHedNo = dataitem.FindControl("lvlHedNo") as Label;
            TextBox txtDateAplli = dataitem.FindControl("txtDateAplli") as TextBox;
            string SP_Name1 = "PKG_ACD_INSERT_DATE_TO_REMEMBER";
            string SP_Parameters1 = "@P_APTIDATE_NO,@P_DATE,@P_PROGRAM_APPLIED,@P_OUT ";
            string Call_Values1 = "" + Convert.ToInt32(lvlHedNo.Text) + "," + (txtDateAplli.Text) + "," + Convert.ToInt32(TxtProgmAplied.Text) + ",0";
            que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);
        }
        if (que_out1 == "2")
        {
            BindListView();
            objCommon.DisplayMessage(this.Page, "Record Updated Successfully!", this.Page);
            return;
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Server Error!", this.Page);
            return;
        }
      
    }
   
    protected void BindListView()
    {
        DataSet dss = objCommon.FillDropDown("ACD_APPTI_TEST_DATE_TO_REMEMBER", "*", "APTIDATE_NO", "APTIDATE_NO>0", "HEADING");
        int pgmap= Convert.ToInt32(objCommon.LookUp("Reff", "ISNULL(PROGRAM_APPLIED,0)PROGRAM_APPLIED", ""));
        if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
        {
            lvBankDetails.DataSource = dss;
            lvBankDetails.DataBind();      
        }
        else
        {
            lvBankDetails.DataSource = null;
            lvBankDetails.DataBind();
        }
        TxtProgmAplied.Text = Convert.ToString(pgmap);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}