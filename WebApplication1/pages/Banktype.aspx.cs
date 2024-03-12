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

public partial class ACADEMIC_Banktype : System.Web.UI.Page
{
    FeeCollectionController feecolle = new FeeCollectionController();
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
                //Page Authorization
                this.CheckPageAuthorization();

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
                binddata();
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
                ViewState["action"] = "add";
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
                Response.Redirect("~/notauthorized.aspx?page=Banktype.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Banktype.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //int Bankno = Convert.ToInt32(objCommon.LookUp("ACD_MISBANK", "ISNULL(MAX(BANK_NO),0)BANK_NO", ""));
        //int BANKNO = Bankno + 1;
        CustomStatus cs = (CustomStatus)feecolle.InsertBanktype(Convert.ToInt32(ddlbanktype.SelectedValue), Convert.ToInt32(ViewState["BANK_NO"]), Convert.ToString(txtbankCode.Text), Convert.ToString(ddlbanktype.SelectedItem), Convert.ToString(txtBankNAme.Text));
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this.updsetting, "Record Saved Successfully!", this.Page);
            binddata();
            clear();
        }
        else if (cs.Equals(CustomStatus.RecordUpdated))
        {
            objCommon.DisplayMessage(this.updsetting, "Record Updated Successfully!", this.Page);
            binddata();
            clear();
        }

    }
    private void binddata()
    {
        DataSet ds = objCommon.FillDropDown("ACD_MISBANK", "BANK_NO", "BANKTYPE_NO,BANK_CODE,BANK_NAME,BANKTYPE_NAME", "", "");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvBankDetails.DataSource = ds;
            lvBankDetails.DataBind();
        }
        else
        {
            lvBankDetails.DataSource = null;
            lvBankDetails.DataBind();

        }
    }
    private void clear()
    {
        ddlbanktype.SelectedIndex = 0;
        txtBankNAme.Text = "";
        txtbankCode.Text = "";
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
            int BANKNO = int.Parse(btnEdit.CommandArgument);
            ViewState["BANK_NO"] = BANKNO;
            ViewState["action"] = "edit";
            DataSet dss = objCommon.FillDropDown("ACD_MISBANK", "*", "BANK_NO", "BANK_NO=" + BANKNO, "BANK_NAME");
            if (dss.Tables[0].Rows.Count > 0)
            {

                ddlbanktype.SelectedValue = dss.Tables[0].Rows[0]["BANKTYPE_NO"].ToString();
                txtbankCode.Text = dss.Tables[0].Rows[0]["BANK_CODE"].ToString();
                txtBankNAme.Text = dss.Tables[0].Rows[0]["BANK_NAME"].ToString();         
                //BindListView();
               
            }
        }
        catch(Exception ex)
        {
        }
    }
}