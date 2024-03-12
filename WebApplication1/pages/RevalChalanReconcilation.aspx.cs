//======================================================================================
// PROJECT NAME  : SVCE
// MODULE NAME   : ACADEMIC
// PAGE NAME     : RECEIPT RECONCILIATION
// CREATION DATE : 17-Jan-2020
// CREATED BY    : Neha Baranwal
// MODIFIED DATE : 
// MODIFIED DESC : 
//======================================================================================

using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Academic_RevalChalanReconcilation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    ChalanReconciliationController crController = new ChalanReconciliationController();

    #region Page Events
    //USED FOR INITIALSING THE MASTER PAGE
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    //USED FOR BYDEFAULT LOADING THE default PAGE
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    PopulateDropDownList();
                }
            }
            else
            {
                // Clear message div
                divMsg.InnerHtml = string.Empty;

                if (Request.Params["__EVENTTARGET"] != null && Request.Params["__EVENTTARGET"].ToString() != string.Empty)
                {
                    if (Request.Params["__EVENTTARGET"].ToString() == "ReconcileChalan")
                        this.ReconcileChalan();
                    else if (Request.Params["__EVENTTARGET"].ToString() == "DeleteChalan")
                        this.DeleteChalan();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ChalanReconciliation.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page. 
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RevalChallanReconcilation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RevalChallanReconcilation.aspx");
        }
    }

    ////bind ReceiptType  in drop down list. 
    protected void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO desc");

            objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_TITLE", "RCPTTYPENO > 0 AND RECIEPT_CODE IN('PRF','RF','AEF','CF')", "RCPTTYPENO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "RevalChallanReconcilation.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion
    //button Search is used for to get the student details as per as selection of radio button
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            GetStudent();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ChalanReconciliation.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //get student details as of select student reg no or enroll no
    private void GetStudent()
    {
        DataSet ds = null;
        try
        {
            string rec_code = string.Empty;
            rec_code = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO = " + ddlReceiptType.SelectedValue + "");
            ds = crController.GetChallanDetails(txtSearchText.Text, rec_code, Convert.ToInt32(ddlSession.SelectedValue));

        }
        catch
        {
            ShowMessage("No challan found.");

            lvSearchResults.Visible = false;
            //lvSearchResultSem.Visible = false;

            btnReconcile.Disabled = true;
            btnDelete.Disabled = true;
            divRemark.Visible = false;
            txtRemark.Text = string.Empty;
            ddlReceiptType.SelectedIndex = 0;
            txtSearchText.Text = string.Empty;
            ddlSession.SelectedIndex = 0;
            return;
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            divinfo.Visible = true;
            divChallanButtonDetails.Visible = true;
            lblStudName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            lblDegree.Text = ds.Tables[0].Rows[0]["DEGREE"].ToString();
            lblBranch.Text = ds.Tables[0].Rows[0]["BRANCH"].ToString();
            lblSemester.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();

            lvSearchResults.DataSource = ds;
            lvSearchResults.DataBind();


            foreach (ListViewDataItem item in lvSearchResults.Items)
            {
                HiddenField hdnrecon = (item.FindControl("hidrecon") as HiddenField);

                HtmlControl rdbchk = (HtmlControl)(item.FindControl("lblreco"));

                if (Convert.ToBoolean(hdnrecon.Value) == true)
                {
                    rdbchk.Attributes.Add("Style", "display:none;");
                }
                else
                {
                    rdbchk.Attributes.Add("Style", "display:block;");
                }
            }
            lvSearchResults.Visible = true;


            btnDelete.Disabled = false;
            btnReconcile.Disabled = false;
            divRemark.Visible = true;


        }
        else
        {
            divinfo.Visible = false;
            divChallanButtonDetails.Visible = false;
            ShowMessage("No challan found ");
            lvSearchResults.Visible = false;
             
            btnReconcile.Disabled = true;
            btnDelete.Disabled = true;
            divRemark.Visible = false;
            txtRemark.Text = string.Empty;
            txtSearchText.Text = string.Empty;
            ddlReceiptType.SelectedIndex = 0;
            ddlSession.SelectedIndex = 0;
        }
        txtRemark.Text = string.Empty;
    }

    //get reconcile chalan
    private void ReconcileChalan()
    {
        try
        {
            DailyCollectionRegister dcr = new DailyCollectionRegister();
            string rec_code = string.Empty;
            rec_code = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO = " + ddlReceiptType.SelectedValue + "");
            //  --  for demand to dcr creation -- 
            if (rec_code == "PRF")
            {
                if (this.GetRecieptData(ref dcr))
                {
                    //2 status for record updated
                    if (2 == crController.RevalReconcileChalan(dcr, 1)) // 1 for photo copy
                    {
                        this.ShowMessage("Challan Reconciled Successfully.");
                        //HideControles();
                        GetStudent();
                    }
                    else
                    {
                        this.ShowMessage("Unable to complete the operation.");
                        HideControles();
                    }
                }
            }
            else if (rec_code == "RF")
            {
                if (this.GetRecieptData(ref dcr))
                {
                    //2 status for record updated
                    if (2 == crController.RevalReconcileChalan(dcr, 2)) // 2 for reval
                    {
                        this.ShowMessage("Challan Reconciled Successfully.");
                        //HideControles();
                        GetStudent();
                    }
                    else
                    {
                        this.ShowMessage("Unable to complete the operation.");
                        HideControles();
                    }
                }
            }
            else if (rec_code == "AEF")
            {
                if (this.GetRecieptData(ref dcr))
                {
                    //2 status for record updated
                    if (2 == crController.RevalReconcileChalan(dcr, 3)) //3 for Arrear exam
                    {
                        this.ShowMessage("Challan Reconciled Successfully.");
                        //HideControles();
                        GetStudent();
                    }
                    else
                    {
                        this.ShowMessage("Unable to complete the operation.");
                        HideControles();
                    }
                }                 
            }
            else if (rec_code == "CF")
            {
                if (this.GetRecieptData(ref dcr))
                {
                    //2 status for record updated
                    if (2 == crController.RevalReconcileChalan(dcr, 4)) //4 for Condonation 
                    {
                        this.ShowMessage("Challan Reconciled Successfully.");
                        //HideControles();
                        GetStudent();
                    }
                    else
                    {
                        this.ShowMessage("Unable to complete the operation.");
                        HideControles();
                    }
                }                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ChalanReconciliation.ReconcileChalan() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //used for delete challan
    private void DeleteChalan()
    {
        try
        {
            DailyCollectionRegister dcr = new DailyCollectionRegister();

            string rec_code = string.Empty;
            rec_code = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO = " + ddlReceiptType.SelectedValue + "");
            if (rec_code == "PRF")
            {
                if (this.GetRecieptData(ref dcr))
                {
                    if (2 == crController.RevalDeleteChalan(dcr, 1)) //1 for photo copy
                    {
                        this.ShowMessage("Challan has been Canceled Successfully.");
                        HideControles();
                    }
                    else
                    {
                        this.ShowMessage("Unable to complete the operation.");
                        HideControles();
                    }
                }
            }
            else if (rec_code == "RF")
            {
                if (this.GetRecieptData(ref dcr))
                {
                    if (2 == crController.RevalDeleteChalan(dcr, 2)) //2 for reval
                    {
                        this.ShowMessage("Challan has been Canceled Successfully.");
                        HideControles();
                    }
                    else
                    {
                        this.ShowMessage("Unable to complete the operation.");
                        HideControles();
                    }
                }
            }
            else if (rec_code == "AEF")
            {
                if (this.GetRecieptData(ref dcr))
                {
                    if (2 == crController.RevalDeleteChalan(dcr, 3)) //3 for arrear exam
                    {
                        this.ShowMessage("Challan has been Canceled Successfully.");
                        HideControles();
                    }
                    else
                    {
                        this.ShowMessage("Unable to complete the operation.");
                        HideControles();
                    }
                }                
            }
            else if (rec_code == "CF")//4 for arrear exam
            {
                if (this.GetRecieptData(ref dcr))
                {
                    if (2 == crController.RevalDeleteChalan(dcr, 4)) //4 for Condonation 
                    {
                        this.ShowMessage("Challan has been Canceled Successfully.");
                        HideControles();
                    }
                    else
                    {
                        this.ShowMessage("Unable to complete the operation.");
                        HideControles();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ChalanReconciliation.DeleteChalan() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //get receipt data
    private bool GetRecieptData(ref DailyCollectionRegister dcr)
    {
        try
        {
            foreach (ListViewDataItem item in lvSearchResults.Items)
            {
                string strSessionNo = (item.FindControl("hidSessionNo") as HiddenField).Value;
                string Reciepttype = (item.FindControl("hidRcypt") as HiddenField).Value;
                //int semesterno = 0;
                // semesterno = Convert.ToInt32((item.FindControl("hidDcrSemesterNo") as HiddenField).Value);

                dcr.SessionNo = ((strSessionNo != null && strSessionNo != string.Empty) ? int.Parse(strSessionNo) : 0);
                string strIdNo = (item.FindControl("hidIdNo") as HiddenField).Value;
                dcr.StudentId = ((strIdNo != null && strIdNo != string.Empty) ? int.Parse(strIdNo) : 0);
                dcr.ReceiptDate = DateTime.Today;
                dcr.UserNo = int.Parse(Session["userno"].ToString());
                dcr.ReceiptTypeCode = Reciepttype;
                //dcr.SemesterNo = semesterno;
                if (Request.Params["__EVENTTARGET"].ToString() == "ReconcileChalan")
                    dcr.Remark = "This chalan has been reconciled by " + Session["userfullname"].ToString() + ". ";
                else if (Request.Params["__EVENTTARGET"].ToString() == "DeleteChalan")
                    dcr.Remark = "This receipt has been deleted by " + Session["userfullname"].ToString() + " on " + DateTime.Now + ". ";

                dcr.Remark += txtRemark.Text.Trim();
                return true;

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ChalanReconciliation.GetRecordIDs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return false;
    }

    private string GetViewStateItem(string itemName)
    {
        if (ViewState.Count > 0 &&
            ViewState[itemName] != null &&
            ViewState[itemName].ToString() != null &&
            ViewState[itemName].ToString() != string.Empty)
            return ViewState[itemName].ToString();
        else
            return string.Empty;
    }
    //shown alert massage
    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert(\"" + msg + "\"); </script>";
    }


    private void HideControles()
    {
        divinfo.Visible = false;
        divChallanButtonDetails.Visible = false;
        lvSearchResults.DataSource = null;
        lvSearchResults.DataBind();
        lvSearchResults.Visible = false;

        btnReconcile.Disabled = true;
        btnDelete.Disabled = true;
        txtRemark.Text = string.Empty;
        txtSearchText.Text = string.Empty;
        ddlReceiptType.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        divinfo.Visible = false;
        divRemark.Visible = false;
    }
    //refresh current page or reload current page
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}