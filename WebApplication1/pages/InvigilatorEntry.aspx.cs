//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : Examination                                                             
// PAGE NAME     : Invigilator Entry                                                         
// CREATION DATE : 11-FEB-2012                                                     
// CREATED BY    : UMESH K. GANORKAR                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
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
using IITMS.SQLServer.SQLDAL;
public partial class ACADEMIC_InvigilatorEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    SeatingController objSc = new SeatingController();

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
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //This checks the authorization of the user.
                CheckPageAuthorization();

                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                // Set form mode equals to -1(New Mode).
                ViewState["exdtno"] = "0";

                divMsg.InnerHtml = string.Empty;
                ViewState["roomname"] = string.Empty;
                ViewState["invReq"] = null;
            }
            BindInvigilatorList();
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=InvigilatorEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=InvigilatorEntry.aspx");
        }
    }
    private void BindInvigilatorList()
    {
        try
        {
            SeatingController objSC = new SeatingController();
            DataSet ds = objSC.GetInvigilatorList();
            if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvInvigilator.DataSource = ds;
                lvInvigilator.DataBind();
            }
            else
            {
                lvInvigilator.DataSource = null;
                lvInvigilator.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_InvigilatorEntry.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string ua_no = string.Empty;
            string status = string.Empty;
            string duties = string.Empty;
            int i = 0;
            int count = 0;

            foreach (ListViewDataItem item in lvInvigilator.Items)
            {
                CheckBox chk = item.FindControl("cbRow") as CheckBox;
                TextBox txtDuties = item.FindControl("txtDuties") as TextBox;
                if (chk.Checked)
                {
                    ua_no += chk.ToolTip + ",";

                    duties += txtDuties.Text + ",";
                }
                if (chk.Checked == true)
                {
                    count++;
                }
            }
            if (count <= 0)
            {
                objCommon.DisplayMessage(this.updInv, "Please Select Invigilator And Status from List..", this.Page);
                return;
            }
            CustomStatus cs = (CustomStatus)objSc.InvigilatorEntry(ua_no, duties, Session["colcode"].ToString());
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updInv, "Invigilator Entry Done ...!!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updInv, "Error while Invigilator Entry..", this.Page);
            }
            this.BindInvigilatorList();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_EndSemesterAttendanceSheet.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
