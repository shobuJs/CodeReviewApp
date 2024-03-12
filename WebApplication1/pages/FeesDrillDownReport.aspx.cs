using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_FeesDrillDownReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    StudentController studcon = new StudentController();
    UAIMS_Common objUCommon = new UAIMS_Common();

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

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ODD_EVEN=1", "SESSIONNO DESC");              
            }
        }

        if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        divMsg.InnerHtml = string.Empty;

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FeesDrillDownReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeesDrillDownReport.aspx");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
        }
        else
        {
            ddlAdmBatch.Items.Clear();
            ddlAdmBatch.Items.Insert(0, new ListItem("Please Select"));
            ddlAdmBatch.SelectedIndex = 0;
        }

        ddlDegree.Items.Clear();
        ddlDegree.Items.Insert(0, new ListItem("Please Select"));
        ddlDegree.SelectedIndex = 0;

        divlv.Visible = false;
        lvFeeDetails.DataSource = null;
        lvFeeDetails.DataBind();
    }

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdmBatch.SelectedIndex > 0)
        {
            ddlDegree.Items.Clear();
            ddlDegree.Items.Insert(0, new ListItem("Please Select"));
            ddlDegree.SelectedIndex = 0; 

            ddlDegree.Items.Insert(1, new ListItem("UG"));
            ddlDegree.Items.Insert(2, new ListItem("PG"));
            ddlDegree.SelectedIndex = 0;

            objCommon.FillDropDownList(ddlRecpType, "ACD_RECIEPT_TYPE", "TOP(3) RCPTTYPENO", "RECIEPT_CODE", "RCPTTYPENO > 0", "RCPTTYPENO");
        }
        else
        {
            ddlDegree.Items.Clear();
            ddlDegree.Items.Insert(0, new ListItem("Please Select"));
            ddlDegree.SelectedIndex = 0;

            ddlRecpType.Items.Clear();
            ddlRecpType.Items.Insert(0, new ListItem("Please Select"));
            ddlRecpType.SelectedIndex = 0;  
        }

        divlv.Visible = false;
        lvFeeDetails.Visible = false;
        lvFeeDetails.DataSource = null;
        lvFeeDetails.DataBind();
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {      
        ddlRecpType.SelectedIndex = 0;
      
        divlv.Visible = false;
        lvFeeDetails.Visible = false;
        lvFeeDetails.DataSource = null;
        lvFeeDetails.DataBind();
    }

    void Cancel()
    {
        ddlSession.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        ddlAdmBatch.Items.Clear();
        ddlAdmBatch.Items.Insert(0, new ListItem("Please Select"));

        ddlDegree.Items.Clear();
        ddlDegree.Items.Insert(0, new ListItem("Please Select"));
        ddlDegree.SelectedIndex = 0;

        ddlRecpType.Items.Clear();
        ddlRecpType.Items.Insert(0, new ListItem("Please Select"));
        ddlRecpType.SelectedIndex = 0;

        divlv.Visible = false;
        lvFeeDetails.DataSource = null;
        lvFeeDetails.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Cancel();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        DataSet dsShow = new DataSet();
        dsShow = studcon.GetFeeDetailCount(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedIndex), ddlRecpType.SelectedItem.Text);

        if (dsShow.Tables[0].Rows.Count > 0)
        {
            divlv.Visible = true;
            lvFeeDetails.Visible = true;
            lvFeeDetails.DataSource = dsShow;
            lvFeeDetails.DataBind();
        }
        else
        {
            divlv.Visible = false;
            lvFeeDetails.Visible = false;
        }
    }

    protected void btnAppStudents_Click(object sender, EventArgs e)
    {
        LinkButton btnAppStudents = (LinkButton)(sender);
        int branchno = Convert.ToInt32(btnAppStudents.CommandArgument);
        int colno = Convert.ToInt32(btnAppStudents.CommandName);
        int degreeno = Convert.ToInt32(btnAppStudents.ToolTip);

        if (btnAppStudents.Text != "0")
        {
            ShowReport("AppliedStudents", "rptFeesDrillDownStudentList.rpt", branchno, colno, degreeno);
        }
    }

    protected void lblJoinedStud_Click(object sender, EventArgs e)
    {
        LinkButton btnJoinedStud = (LinkButton)(sender);

        int branchno = Convert.ToInt32(btnJoinedStud.CommandArgument);
        int colno = Convert.ToInt32(btnJoinedStud.CommandName);
        int degreeno = Convert.ToInt32(btnJoinedStud.ToolTip);

        if (btnJoinedStud.Text != "0")
        {
            ShowReport("AppliedStudents", "rptFeesDrillDownStudentList.rpt", branchno, colno, degreeno);
        }
    }

    protected void lblStudPaidFees_Click(object sender, EventArgs e)
    {
        LinkButton btnStudPaidFees = (LinkButton)(sender);
        int branchno = Convert.ToInt32(btnStudPaidFees.CommandArgument);
        int colno = Convert.ToInt32(btnStudPaidFees.CommandName);
        int degreeno = Convert.ToInt32(btnStudPaidFees.ToolTip);

        if (btnStudPaidFees.Text != "0")
        {
            ShowReport("AppliedStudents", "rptFeesDrillDownStudentList.rpt", branchno, colno, degreeno);
        }
    }

    protected void btnStudNotPaidFees_Click(object sender, EventArgs e)
    {
        LinkButton btnStudNotPaidFees = (LinkButton)(sender);
        int branchno = Convert.ToInt32(btnStudNotPaidFees.CommandArgument);
        int colno = Convert.ToInt32(btnStudNotPaidFees.CommandName);
        int degreeno = Convert.ToInt32(btnStudNotPaidFees.ToolTip);

        if (btnStudNotPaidFees.Text != "0")
        {
            ShowReport("AppliedStudents", "rptFeesDrillDownStudentList.rpt", branchno, colno, degreeno);
        }
    }

    protected void btnStudLeft_Click(object sender, EventArgs e)
    {
        LinkButton btnStudLeft = (LinkButton)(sender);
        int branchno = Convert.ToInt32(btnStudLeft.CommandArgument);
        int colno = Convert.ToInt32(btnStudLeft.CommandName);
        int degreeno = Convert.ToInt32(btnStudLeft.ToolTip);

        if (btnStudLeft.Text != "0")
        {
            ShowReport("AppliedStudents", "rptFeesDrillDownStudentListStudLeft.rpt", branchno, colno, degreeno);
        }
    }

    protected void btnFeePaidStudLeft_Click(object sender, EventArgs e)
    {
        LinkButton btnFeePaidStudLeft = (LinkButton)(sender);
        int branchno = Convert.ToInt32(btnFeePaidStudLeft.CommandArgument);
        int colno = Convert.ToInt32(btnFeePaidStudLeft.CommandName);
        int degreeno = Convert.ToInt32(btnFeePaidStudLeft.ToolTip);

        if (btnFeePaidStudLeft.Text != "0")
        {
            ShowReport("AppliedStudents", "rptFeesDrillDownStudentList.rpt", branchno, colno, degreeno);
        }
    }

    protected void btnCurStudStrength_Click(object sender, EventArgs e)
    {
        LinkButton btnCurStudStrength = (LinkButton)(sender);
        int branchno = Convert.ToInt32(btnCurStudStrength.CommandArgument);
        int colno = Convert.ToInt32(btnCurStudStrength.CommandName);
        int degreeno = Convert.ToInt32(btnCurStudStrength.ToolTip);

        if (btnCurStudStrength.Text != "0")
        {
            ShowReport("AppliedStudents", "rptFeesDrillDownStudentList.rpt", branchno, colno, degreeno);
        }
    }

    private void ShowReport(string reportTitle, string rptFileName, int branchno, int colno, int degreeno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(degreeno) + ",@P_BRANCHNO=" + Convert.ToInt32(branchno) + ",@P_COLNO=" + Convert.ToInt32(colno) + ",@P_RECIEPTCODE=" + ddlRecpType.SelectedItem.Text;          

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updTeacher, this.updTeacher.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlRecpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        divlv.Visible = false;
        lvFeeDetails.Visible = false;
        lvFeeDetails.DataSource = null;
        lvFeeDetails.DataBind();
    }

}