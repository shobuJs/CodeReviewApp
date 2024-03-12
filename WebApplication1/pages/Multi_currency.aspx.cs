using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Web.Configuration;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Security;
using System.Diagnostics;
using System.Collections.Generic;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using EASendMail;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
public partial class ACADEMIC_Multi_currency : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FeeCollectionController fee = new FeeCollectionController();
    UserController user = new UserController();
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
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
                    CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                    this.objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_ID");
                    this.objCommon.FillDropDownList(ddlreceipt, "ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_TITLE", "RCPTTYPENO in(2)", "RCPTTYPENO");
                    this.objCommon.FillDropDownList(ddlcurrency, "ACD_CURRENCY", "CUR_NO", "CUR_NAME", "CUR_NO NOT IN(1,3)", "CUR_NO");
                    this.objCommon.FillDropDownList(ddlintake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
                    this.objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR > 0", "YEAR");
                    ViewState["action"] = "add";
                    binddata();
                }
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
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
                Response.Redirect("~/notauthorized.aspx?page=Multi_currency.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Multi_currency.aspx");
        }
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillListBox(lstbxProgram, "ACD_USER_BRANCH_PREF S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=S.BRANCHNO)INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON ()", "DISTINCT CONCAT(S.DEGREENO,',',S.BRANCHNO)AS ID", "(D.DEGREENAME +'-'+ B.LONGNAME)AS DEGBRANCH", "S.COLLEGE_ID=" + Convert.ToInt32(ddlFaculty.SelectedValue) + "", "ID");
        objCommon.FillListBox(lstbxProgram, "ACD_COLLEGE_DEGREE_BRANCH S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=S.BRANCHNO)INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON (AU.AFFILIATED_NO=S.AFFILIATED_NO)", "DISTINCT CONCAT(S.DEGREENO,',',S.BRANCHNO,',',S.AFFILIATED_NO)AS ID", "(D.DEGREENAME +'-'+ B.LONGNAME+'-'+AU.AFFILIATED_SHORTNAME)AS DEGBRANCH", "S.COLLEGE_ID=" + Convert.ToInt32(ddlFaculty.SelectedValue) + "", "ID");
    }
    private void binddata()
    {
        DataSet ds = fee.GetOtherCurrencyData();
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            pnlcurr.Visible = true;
            LvOtherCurrency.DataSource = ds;
            LvOtherCurrency.DataBind();
        }
        else
        {
            pnlcurr.Visible = false;
            LvOtherCurrency.DataSource = null;
            LvOtherCurrency.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string degreeno = "";
            string branchno = "";
            string affno = "";
            string program = "";
            string[] pgm = new string[] { };
            foreach (ListItem item in lstbxProgram.Items)
            {
                if (item.Selected == true)
                {
                    program += item.Value + ',';
                    //program = program.TrimEnd(',');
                }

            }
            if (!string.IsNullOrEmpty(program))
            {
                pgm = program.Split(',');
                for (int i = 0; i < pgm.Length; i += 3)
                {
                    degreeno += pgm[i] + ",";

                }
                for (int j = 1; j < pgm.Length; j += 3)
                {
                    branchno += pgm[j] + ",";
                }
                for (int k = 2; k < pgm.Length; k += 3)
                {
                    affno += pgm[k] + ",";
                }
                affno = affno.TrimEnd(',');
                degreeno = degreeno.TrimEnd(',');
                branchno = branchno.TrimEnd(',');
            }

            else
            {
                objCommon.DisplayMessage(updmulticurrency, "Please Select Program", this);
                //return;
            }

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = CustomStatus.Others;
                    cs = (CustomStatus)fee.InsertOtherCurrency(Convert.ToInt32(ddlintake.SelectedValue), Convert.ToInt32(ddlFaculty.SelectedValue), Convert.ToString(degreeno), Convert.ToString(branchno), Convert.ToInt32(ddlcurrency.SelectedValue), Convert.ToString(affno), Convert.ToDecimal(TextAmot.Text), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlYear.SelectedValue),Convert.ToInt32(ddlreceipt.SelectedValue));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.Page, "Record Saved Successfully",this);
                        binddata();
                        //ddlaffliuni.SelectedIndex = 0;
                        ddlcurrency.SelectedIndex = 0;
                        //ddlintake.SelectedIndex = 0;
                        //lstbxProgram.ClearSelection();
                        //ddlFaculty.SelectedIndex = 0;
                        ddlYear.SelectedIndex = 0;
                        TextAmot.Text = "";
                        //Clear();

                    }
                    else
                    {
                        objCommon.DisplayMessage(updmulticurrency, "Record Already Exist", this);
                        return;
                    }
                }
                else if (ViewState["action"].ToString().Equals("edit"))
                {
                    CustomStatus cs = CustomStatus.Others;
                    cs = (CustomStatus)fee.UpdateOtherCurrency(Convert.ToInt32(ddlintake.SelectedValue), Convert.ToInt32(ddlFaculty.SelectedValue), Convert.ToString(degreeno), Convert.ToString(branchno), Convert.ToInt32(ddlcurrency.SelectedValue), Convert.ToString(affno), Convert.ToDecimal(TextAmot.Text), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ViewState["srno"]), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlreceipt.SelectedValue));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.Page, "Record Updated Successfully", this);                       
                        ViewState["action"] = "add";
                        btnSubmit.Text = "Submit";
                        binddata();
                        //ddlaffliuni.SelectedIndex = 0;
                        ddlcurrency.SelectedIndex = 0;
                        //ddlintake.SelectedIndex = 0;
                        //lstbxProgram.ClearSelection();
                        //ddlFaculty.SelectedIndex = 0;
                        ddlYear.SelectedIndex = 0;
                        TextAmot.Text = "";
                        //Clear();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updmulticurrency, "Record Already Exist", this.Page);
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
        //ddlaffliuni.SelectedIndex = 0;
        ddlcurrency.SelectedIndex = 0;
        ddlintake.SelectedIndex = 0;
        lstbxProgram.ClearSelection();
        ddlFaculty.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        TextAmot.Text = "";
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["action"] = "edit";
            int srno = int.Parse(btnEdit.CommandArgument);
            ViewState["srno"] = srno;
            btnSubmit.Text = "Update";
            ShowDetails(srno);
        }
        catch (Exception ex)
        {
        }
    }
    private void ShowDetails(int srno)
    {

        try
        {

            DataSet ds = objCommon.FillDropDown("ACD_OTHER_CURRENCEY_FEES", "SRNO,COLLEGE_ID,DEGREENO BRANCHNO,CONCAT(DEGREENO,',',BRANCHNO,',',AFFILIATED_NO)AS PROGRAM,RECEIPT_NO,ADMBATCH,CUR_NO,AFFILIATED_NO,AMOUNT,YEAR", "", "SRNO=" + srno, "SRNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlFaculty.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                ddlintake.SelectedValue = ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
                ddlcurrency.SelectedValue = ds.Tables[0].Rows[0]["CUR_NO"].ToString();
                ddlreceipt.SelectedValue = ds.Tables[0].Rows[0]["RECEIPT_NO"].ToString();
                TextAmot.Text = ds.Tables[0].Rows[0]["AMOUNT"].ToString();
                ddlYear.SelectedValue = ds.Tables[0].Rows[0]["YEAR"].ToString();
                string program = (ds.Tables[0].Rows[0]["PROGRAM"].ToString());
                ddlFaculty_SelectedIndexChanged(new object(), new EventArgs());
                string[] pgm = program.Split('&');
                for (int j = 0; j < pgm.Length; j++)
                {
                    for (int i = 0; i < lstbxProgram.Items.Count; i++)
                    {
                        if (pgm[j] == lstbxProgram.Items[i].Value)
                        {
                            lstbxProgram.Items[i].Selected = true;
                        }
                    }
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