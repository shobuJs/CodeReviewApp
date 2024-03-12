//======================================================================================
// PROJECT NAME   : UAIMS [GHREC]                                                         
// MODULE NAME    : ACADEMIC                                                             
// PAGE NAME      : Registration No. Generation
// CREATION DATE  : 06-JULY-2011                                                          
// CREATED BY     : NIRAJ D. PHALKE                                                   
// MODIFIED DATE  :                                                                      
// MODIFIED DESC  :                                                                      
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class ACADEMIC_Backdateattlock : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentRegistration objRegistration = new StudentRegistration();
    StudentAttendanceController objstudatt = new StudentAttendanceController();
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

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                    this.FillDropdown();
                    showdata();
                    ViewState["action"] = "0";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_RegistraionNoGeneration.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RegistraionNoGeneration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RegistraionNoGeneration.aspx");
        }
    }

    private void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "", "DEGREENO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_RegistraionNoGeneration.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDegree.SelectedIndex = -1;
            txtdays.Text = string.Empty;
            chkAlertEmail.Checked = false;
            btnShow.Text = "Submit";

        }
        catch (Exception ex)
        {

        }
    }
    private void showdata()
    {
        try
        {
            DataSet ds = objstudatt.GetAttendencedatabacknodays();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvlockatt.DataSource = ds;
                lvlockatt.DataBind();
            }
            else
            {
                lvlockatt.DataSource = null;
                lvlockatt.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ViewState["action"].Equals("edit"))
            {
                foreach (ListViewDataItem item in lvlockatt.Items)
                {
                    Label lbldegree = item.FindControl("lbldegree") as Label;
                    Label lblnoday = item.FindControl("lblnoday") as Label;
                    if ((Convert.ToInt32(lbldegree.ToolTip) == Convert.ToInt32(ddlDegree.SelectedValue)))
                    {
                        goto error;
                    }
                }
            }
            int i = objstudatt.Attendancebacklockins(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(txtdays.Text), Convert.ToInt32(chkAlertEmail.Checked));
            if (i == 1)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Record Saved Successfully!", this.Page);
            }
            else if (i == 0)
            {

                objCommon.DisplayMessage(this.UpdatePanel1, "Record is updated Successfully!", this.Page);


            }
            else
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Record not Saved...Please check!", this.Page);
            }
        error:
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Record for selected degree is already Exists!", this.Page);
                ViewState["action"] = "0";
                btnShow.Text = "Submit";
            }
            showdata();
            ddlDegree.SelectedIndex = -1;
            txtdays.Text = string.Empty;
            chkAlertEmail.Checked = false;
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int degreeno = int.Parse(btnEdit.CommandArgument);
            int days = int.Parse(btnEdit.CommandName);
            ViewState["degreeno"] = int.Parse(btnEdit.CommandArgument);
            ViewState["days"] = int.Parse(btnEdit.CommandName);
            ddlDegree.SelectedValue = ViewState["degreeno"].ToString();
            txtdays.Text = ViewState["days"].ToString();
            ViewState["action"] = "edit";
            btnShow.Text = "Update";

            CheckBox chk = btnEdit.FindControl("chklvAlertEmail") as CheckBox;
            if (chk.Checked == true)
                chkAlertEmail.Checked = true;
            else
                chkAlertEmail.Checked = false;
        }
        catch (Exception ex)
        {
        }
    }
}
