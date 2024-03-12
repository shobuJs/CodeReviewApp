//CREATED BY - IFTAKHAR KHAN
//DATED - 27-MARCH-2014
//MODIFIED ON - 1-APRIL-2014
//APPROVED BY - PIYUSH R
//PURPOSE - THIS PAGE IS USED TO ENTRY MISCELLANEOUS FEES DETAIL

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_MASTERS_Srcategory : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController stud = new StudentController();
    FeeCollectionController feeController = new FeeCollectionController();
    #region Page Events
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
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();
                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {

                    }

                    this.objCommon.FillDropDownList(ddldegree, "acd_degree", "degreeno", "degreename", "degreeno>0", "degreeno");
                    this.objCommon.FillDropDownList(ddlsrcategory, "acd_srcategory", "srcategoryno", "srcategory", "srcategoryno>0", "srcategoryno");
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "DefineCounter.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DefineCounter.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DefineCounter.aspx");
        }
    }
    #endregion

    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {

            DataSet ds = stud.Get_Intake_Admission_Data(Convert.ToInt32(ddldegree.SelectedValue), Convert.ToInt32(ddlsrcategory.SelectedValue), ddlcollegecode.SelectedItem.Text.ToString());
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    trListview.Visible = true;
                    lvcategorylist.DataSource = ds;
                    lvcategorylist.DataBind();
                    btnsubmit.Enabled = true;

                }
            }
            else
            {
                objCommon.DisplayMessage(updpnl, "No data found!!", this.Page);
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        try
        {
            btnsubmit.Enabled = false;
            ddldegree.SelectedValue = "0";
            ddlsrcategory.SelectedValue = "0";
            ddlcollegecode.SelectedValue = "0";
            lvcategorylist.DataSource = null;
            lvcategorylist.DataBind();
            trListview.Visible = false;
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (ListViewDataItem lvitem in lvcategorylist.Items)
            {
                HiddenField hdncat = lvitem.FindControl("hdncat") as HiddenField;
                TextBox txtintake = lvitem.FindControl("txtintake") as TextBox;
                HiddenField hdnbranch = lvitem.FindControl("hdnbranch") as HiddenField;

                CustomStatus cs = (CustomStatus)stud.Insert_update_intake_admission(Convert.ToInt32(hdncat.Value), Convert.ToInt32(ddldegree.SelectedValue), Convert.ToInt32(hdnbranch.Value), Convert.ToInt32(ddlsrcategory.SelectedValue), ddlcollegecode.SelectedItem.Text.ToString(), txtintake.Text.ToString());
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(updpnl, "Record saved successfully!!", this.Page);
                }
                else if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(updpnl, "Record updated successfully!!", this.Page);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddldegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        setdata();
    }
    protected void ddlsrcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        setdata();
    }
    protected void ddlcollegecode_SelectedIndexChanged(object sender, EventArgs e)
    {
        setdata();
    }

    private void setdata()
    {
        lvcategorylist.DataSource = null;
        lvcategorylist.DataBind();
        btnsubmit.Enabled = false;
        trListview.Visible = false;
    }
}
