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

public partial class ACADEMIC_Bank_Details : System.Web.UI.Page
{
    CourseController objCourse = new CourseController();
    MappingController objmp = new MappingController();
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
                CheckPageAuthorization();
                
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
                this.BindAllBankForFacuty();

               // objCommon.FillDropDownList(ddlcollegename, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_NAME");
                objCommon.FillDropDownList(ddlcollegename, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and isnull(ACTIVE,0)=1", "COLLEGE_ID");

                objCommon.FillListBox(lstbank, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO>0", "BANKNAME");
                objCommon.SetLabelData(ddlcollegename.SelectedValue);

                //objCommon.SetLabelData("0");//for label
                //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
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
                Response.Redirect("~/notauthorized.aspx?page=Bank_Details.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Bank_Details.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string BankCode = txtbankCode.Text;
            string BankName = txtbankname.Text;
            string BankAddress = txtBankAddress.Text;
            int College_Code = 0;
            string Account = txtBankAccount.Text;
            int BANKNO = 0;
            int Active = 0;
            if (hfdStat.Value == "true")
            {
                Active = 1;
            }
            if (ViewState["BANKNO"] != null)
            {
                BANKNO = Convert.ToInt32(ViewState["BANKNO"].ToString());
            }
            else
            {  //SELECT TOP 1 BANKNO FROM ACD_BANK ORDER BY BANKNO DESC
                int Bankno = Convert.ToInt32(objCommon.LookUp("ACD_BANK", "ISNULL(MAX(BANKNO),0)", ""));
                //= Convert.ToInt32(objCommon.("ACD_BANK", "TOP 1 BANKNO", "", "", "BANKNO DESC"));
                BANKNO = Bankno + 1;

                int Status = Convert.ToInt32(objCommon.LookUp("ACD_BANK", "COUNT(BANKNO)", "BANKCODE='" + BankCode + "'"));
                if (Status != 0)
                {
                    objCommon.DisplayMessage(this, "Bank Code Already Exists..", this.Page);
                    return;
                }
            }
          

            CustomStatus cs = (CustomStatus)objCourse.InsertbankDetails(BankName, BankCode, BankAddress, College_Code, Account, BANKNO, Active);

            if (cs == CustomStatus.RecordSaved)
            {
                objCommon.DisplayMessage(this, "Record Saved Successfully !!", this.Page);
                BindListView();
                txtbankname.Text = "";
                txtbankCode.Text = "";
                txtBankAddress.Text = "";
                txtBankAccount.Text = "";
                ViewState["BANKNO"] = null;
                return;

            }
            else
            {
                objCommon.DisplayMessage(this, "Server Error", this.Page);
                ViewState["BANKNO"] = null;
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Bank_details.BindAllBankForFacuty-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindListView()
    {
        DataSet dss = objCommon.FillDropDown("ACD_BANK", "*", "BANKNO", "BANKNO>0", "BANKNAME");
        if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
        {
            lvBankDetails.DataSource = dss;
            lvBankDetails.DataBind();
            foreach (ListViewDataItem dataitem in lvBankDetails.Items)
            {
                Label lblStatus = dataitem.FindControl("lblStatus") as Label;
                if (lblStatus.Text == "1")
                {
                    lblStatus.Text = "Active";
                     lblStatus.CssClass = "badge badge-success";
                }
                else
                {
                    lblStatus.Text = "Deactive";
                     lblStatus.CssClass = "badge badge-danger";
                }
            }
        }
        else
        {
            lvBankDetails.DataSource = null;
            lvBankDetails.DataBind();
        }
    }

    protected void BindAllBankForFacuty()
    {
        try
        {
            DataSet dsB = null;
            dsB = objCourse.GetFacultyWiseBank();
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            ListView1.DataSource = dsB;
            ListView1.DataBind();
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Bank_details.BindAllBankForFacuty-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int BANKNO = int.Parse(btnEdit.CommandArgument);
            ViewState["BANKNO"] = BANKNO;
            ViewState["action"] = "edit";
            // HiddenField1.Value = int.Parse(btnEdit.CommandArgument).ToString();
            DataSet dss = objCommon.FillDropDown("ACD_BANK", "*", "BANKNO", "BANKNO=" + BANKNO, "BANKNAME");
            if (dss.Tables[0].Rows.Count > 0)
            {
                txtbankname.Text = dss.Tables[0].Rows[0]["BANKNAME"].ToString();
                txtbankCode.Text = dss.Tables[0].Rows[0]["BANKCODE"].ToString();
                txtBankAddress.Text = dss.Tables[0].Rows[0]["BANKADDR"].ToString();
                txtBankAccount.Text = dss.Tables[0].Rows[0]["ACCOUNT_NO"].ToString();
                if (dss.Tables[0].Rows[0]["ACTIVE"].ToString().Trim() == "1")
                {
                    //chkActive.Checked = true;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(true);", true);
                }
                else
                {
                    //chkActive.Checked = false;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(false);", true);
                }
                BindListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_CollegeMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtbankname.Text = "";
        txtbankCode.Text = "";
        txtBankAddress.Text = "";
        txtBankAccount.Text = "";
    }
    protected void ddlbank_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlcollegename_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcollegename.SelectedValue != "0")
        {
            BindListViewForBank();
        }
    }

    protected void LnkSubmit_Click(object sender, EventArgs e)
    {
        int ua_no = Convert.ToInt32(Session["userno"]);

        if (ddlcollegename.SelectedValue == "0" || ddlcollegename.SelectedIndex == null || ddlcollegename.SelectedItem.Text == "Please Select")
        {
            objCommon.DisplayMessage(UpdatePanel1, "Please Select Faculty/School Name And Bank Name", this.Page);
            return;
        }
        try
        {
            string BankName = string.Empty;
            foreach (ListItem items in lstbank.Items)
            {
                if (items.Selected == true)
                {
                    BankName += items.Value + ',';

                }
            }
            if (BankName == string.Empty)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Please Select Bank Name", this.Page);
                return;
            }

            CustomStatus cs = (CustomStatus)objCourse.InsertBankMapping(Convert.ToInt32(ddlcollegename.SelectedValue), BankName, ua_no);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Record Saved Successfully.", this.Page);
                BindListViewForBank();
                ClearData();
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Failed To Save Record ", this.Page);
                BindListViewForBank();
                ClearData();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Bank_details.LnkSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void LnkButtonCancel_Click(object sender, EventArgs e)
    {
        ddlcollegename.SelectedIndex = -1;
        lstbank.SelectedValue = null;
        BindAllBankForFacuty();
    }

    protected void ClearData()
    {
        ddlcollegename.SelectedIndex = -1;
        lstbank.SelectedValue = null;
        //BindAllBankForFacuty();
    }

    protected void BindListViewForBank()
    {
        try
        {
            DataSet ds = null;
            ds = objCourse.GetAllCollegeWiseBank(Convert.ToInt32(ddlcollegename.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                ListView1.Visible = true;
                ListView1.DataSource = ds;
                ListView1.DataBind();

            }
            else
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "No Record Found ", this.Page);
                //ListView1.DataSource = ds;
                //ListView1.DataBind = ();
                ListView1.Visible = false;
                //ddlcollegename.SelectedIndex = -1;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Bank_details.BindListViewForBank-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEditbank_Click(object sender, ImageClickEventArgs e)
    {
        //try
        //{
        //    lstbank.SelectedValue = null;
        //    ImageButton btnEditBANK = sender as ImageButton;
        //    int COLLEGE_ID = int.Parse(btnEditBANK.CommandArgument);
        //    ViewState["COLLEGE_ID"] = COLLEGE_ID;
        //    ViewState["action"] = "edit";

        //    ShowCollegewiseBank(COLLEGE_ID);
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "Bank_Details.btnEditbank_Click-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
    }

    public void ShowCollegewiseBank(int college_id)
    {
        //try
        //{
        //    //DataSet ds = objCommon.FillDropDown("ACD_BANK_MAPPING MA INNER JOIN ACD_BANK BM ON (MA.BANK_NO=BM.BANKNO)", "BANKNAME", "BANK_NO, COLLEGE_ID", "COLLEGE_ID=" + college_id, string.Empty);
        //    DataSet ds = objCommon.FillDropDown("ACD_BANK_MAPPING", "*", "BANK_NO", "COLLEGE_ID=" + college_id, string.Empty);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        //ddlcollegename.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
        //        //lstbank.SelectedValue=ds.Tables[0].Rows[0]["BANK_NO"].ToString();

        //        lstbank.SelectedValue = null;
        //        string[] BANKNO = Convert.ToString(ds.Tables[0].Rows[0]["BANK_NO"]).Split(',');


        //        foreach (string s in BANKNO)
        //        {
        //            foreach (ListItem item in lstbank.Items)
        //            {
        //                // int  bank_name = ListBox bank_name = new ListBox();
        //                if (s == item.Value)
        //                {
        //                    item.Selected = true;
        //                    //break;
        //                }
        //            }
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "Bank_Details.ShowCollegewiseBank -> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
    }

    protected void btnDel_Click(object sender, ImageClickEventArgs e)
    {
        //    //////ACD_COLLEGE_DEGREE_BRANCH;

        //    ImageButton btnDel = sender as ImageButton;

        //    ViewState["action"] = "delete";
        //    int srno = int.Parse(btnDel.CommandArgument);


        //    DataSet college_id = objCommon.FillDropDown("ACD_BANK_MAPPING", "COLLEGE_ID", "BANK_NO", "SR_NO=" + srno + "", "");

        //    string ret = objCommon.LookUp("ACD_BANK_MAPPING","BANK_NO","COLLEGE_ID="+college_id.Tables[0].Rows[0]["COLLEGE_ID"].ToString()+"SR_NO="+college_id.Tables[0].Rows[0]["SR_NO"]);
        //    //// LookUp
        //    //// string result = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "ISNULL(CDBNO,0)AS CDBNO", "COLLEGE_ID=" + collegedept.Tables[0].Rows[0]["COLLEGE_ID"].ToString() + " and DEPTNO = " + collegedept.Tables[0].Rows[0]["DEPTNO"].ToString() + "");
        //    ////if (result != "")
        //    ////{
        //    ////    // objCommon.DisplayMessage(this.updGradeEntry, "You Can't delete this entry ", this.Page);
        //    ////    objCommon.DisplayMessage(this.UpdatePanel1, "You can not delete this mapping ,because program already mapped ", this.Page);
        //    ////}
        //    ////else
        //    ////{
        //    if (ret > 0)
        //    {
        //        int output = objmp.deleteDeptRecord(srno);
        //        if (output != -99 && output != 99)
        //        {
        //            objCommon.DisplayMessage(this.UpdatePanel1, "Record Deleted Successfully", this.Page);
        //            ddlcollegename.ClearSelection();
        //            ClearData();
        //        }
        //        else
        //        {
        //            objCommon.DisplayMessage(this.UpdatePanel1, "Record Is Not Deleted ", this.Page);
        //            ClearData();
        //        }
        //    }
    }

}