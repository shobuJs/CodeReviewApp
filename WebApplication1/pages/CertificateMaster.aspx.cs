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
public partial class CertificateMaster : System.Web.UI.Page
{
    DocumentContro doc = new DocumentContro();
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
                ViewState["DOC_NO"] = "0";
                objCommon.FillDropDownList(ddlType, "ACD_DOCUMENTS_TYPES", "DOC_TYPENO", "DOC_TYPENAME", "", "");
                BindData();
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
            }

        }
    }
    protected void BindData()
    {
        DataSet ds = objCommon.FillDropDown("ACD_DOCUMENT_MASTER DM INNER JOIN  ACD_DOCUMENTS_TYPES DT ON (DT.DOC_TYPENO=DM.DOC_TYPE)", "DOC_NO", "DOC_NAME,DOC_TYPENAME ,DOC_TYPE, (CASE WHEN PAID_STATUS=1 THEN 'YES' ELSE 'NO'END)AS  STATUS,(CASE WHEN PAID_STATUS=1 THEN convert(nvarchar(10),AMOUNT) ELSE '-' END)AS  AMOUNTS", "", "");
        if (ds.Tables[0].Rows.Count > 0 || ds.Tables[0].Rows.Count == null)
        {
            lvDocument.DataSource = ds;
            lvDocument.DataBind();
        }
        else
        {
            lvDocument.DataSource = null;
            lvDocument.DataBind();
        }

    }
    protected void chkPaid_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPaid.Checked == true)
        {
            txtAmt.Visible = true;
            DivAmount.Visible = true;
        }
        else
        {
            txtAmt.Visible = false;
            DivAmount.Visible = false;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string Doc_Name = txtDocumentName.Text;
        int Doc_Type = Convert.ToInt32(ddlType.SelectedValue);
        int Paid_Status = 0;
        int DocNo = 0;
        decimal Amount = 0.0m;
        if (chkPaid.Checked == true)
        {
            Paid_Status=1;
            Amount=Convert.ToDecimal(txtAmt.Text);
        }
        if (ViewState["DOC_NO"] != "0")
        {
            DocNo = Convert.ToInt32(ViewState["DOC_NO"]);    
        }
        string SP_Name1 = "PKG_ACAD_INSERT_DOCUMENTNAME";
        string SP_Parameters1 = "@P_DOCUMENT_NAME,@P_DOCUMENTNO,@P_DOC_TYPE,@P_PAID_STATUS,@P_AMOUNT,@P_OUT";
        string Call_Values1 = "" + Doc_Name.ToString() + "," + DocNo + "," + Doc_Type + "," + Paid_Status + "," + Amount + "," + 0 + "";

        string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);
        if (que_out1 == "1")
        {
            BindData();
            Clear();
            objCommon.DisplayMessage(this, "Record Saved Successfully.. !!", this.Page);
            return;
        }
        else if (que_out1 == "2")
        {
            BindData();
            Clear();
            objCommon.DisplayMessage(this, "Record Update Successfully.. !!", this.Page);
            return;
        }
        else
        {
            objCommon.DisplayMessage(this, "Server Error.. !!", this.Page);
            return;
        }

    }
    protected void Clear()
    {
        txtDocumentName.Text = "";
        ddlType.SelectedValue = "0";
        chkPaid.Checked = false;
        chkPaid_CheckedChanged(new object(), new EventArgs());
        txtAmt.Text = "";
        ViewState["DOC_NO"] = 0;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
         LinkButton lnk = sender as LinkButton;
         ViewState["DOC_NO"] = (lnk.CommandArgument);
         DataSet ds = objCommon.FillDropDown("ACD_DOCUMENT_MASTER", "DOC_NO,*", "", "DOC_NO=" + Convert.ToInt32(ViewState["DOC_NO"].ToString()), "");
         txtDocumentName.Text = ds.Tables[0].Rows[0]["DOC_NAME"].ToString();
         ddlType.SelectedValue = ds.Tables[0].Rows[0]["DOC_TYPE"].ToString();
         int Status = Convert.ToInt32(ds.Tables[0].Rows[0]["PAID_STATUS"].ToString());
         if (Status == 1)
         {
             chkPaid.Checked = true;
             chkPaid_CheckedChanged(new object(), new EventArgs());
             txtAmt.Text = ds.Tables[0].Rows[0]["AMOUNT"].ToString();
         }
         else
         {
             chkPaid.Checked = false;
             chkPaid_CheckedChanged(new object(), new EventArgs());
         }
    }
}