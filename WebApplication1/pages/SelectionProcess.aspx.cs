using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using System.IO;
using System.IO;
using System.Web.Mail;

using System.Threading;

public partial class ACADEMIC_SelectionProcess : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //fill dropdown
                PopulateDropDown();
                //get ip address
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                btnSubmit.Enabled = false;
            }
            divMsg.InnerHtml = string.Empty;
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
                Response.Redirect("~/notauthorized.aspx?page=SelectionProcess.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SelectionProcess.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            //FILL DROPDOWN 
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_UpdateRegistrationNo.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        pnlStudent.Visible = false;
        btnSubmit.Enabled = false;
        if (ddlDegree.SelectedIndex > 0)
        {
            ddlBranchName.Items.Clear();
            objCommon.FillDropDownList(ddlBranchName, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
            ddlBranchName.Focus();
            ddlSession.Enabled = false;
        }
        else
        {
            ddlBranchName.Items.Clear();
            ddlBranchName.Items.Add(new ListItem("Please Select", "0"));
            ddlBranchName.Items.Clear();
        }
    }

    protected void btnShow_Click(object sender, System.EventArgs e)
    {
        try
        {
            DataSet dsStudent = objCommon.FillDropDown("ACD_USER_REGISTRATION UR INNER JOIN ACD_NEWUSER_REGISTRATION NR ON (UR.USERNO = NR.USERNO)", "UR.USERNO", "UR.USERNAME,(UR.FIRSTNAME+' '+UR.LASTNAME)STUDNAME,DBO.FN_DESC('DEGREENAME',UR.DEGREENO)DEGREENAME,DBO.FN_DESC('BRANCHLNAME',UR.BRANCHNO)BRANCHNAME,NR.SELECTEDMERITNO,NR.WAITNINGNO,NR.MOBILE,NR.EMAILID", " NR.SESSIONNO=" + ddlSession.SelectedValue + " AND UR.DEGREENO=" + ddlDegree.SelectedValue + "AND UR.BRANCHNO=" + ddlBranchName.SelectedValue, "UR.USERNO");
            if (dsStudent.Tables[0].Rows.Count > 0 && dsStudent != null && dsStudent.Tables.Count > 0)
            {
                pnlStudent.Visible = true;
                lvStudents.DataSource = dsStudent.Tables[0];
                lvStudents.DataBind();
                btnSubmit.Enabled = true;
            }
            else
            {
                objCommon.DisplayMessage("Student not Available for this Selection", this.Page);
                pnlStudent.Visible = false;
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                btnSubmit.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_SelectionProcess.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ClearControls()
    {
        ddlBranchName.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        pnlStudent.Visible = false;
        btnSubmit.Enabled = false;
        ddlSession.Enabled = true;
    }

    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        ClearControls();
    }

    protected void btnSubmit_Click(object sender, System.EventArgs e)
     {
        try
        {
            StudentController objSC = new StudentController();
            string Userno = string.Empty;
            string ddlValue = string.Empty;
            string txtselect = string.Empty;
            string txtwait = string.Empty;

            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);

            foreach (ListViewDataItem lvItem in lvStudents.Items)
            {
                DropDownList ddlStype = ((lvItem.FindControl("ddlStudType") as DropDownList));
                TextBox txtselected = ((lvItem.FindControl("txtSelected") as TextBox));
                TextBox txtWaiting = ((lvItem.FindControl("txtWaiting") as TextBox));
                HiddenField hdfUno = ((lvItem.FindControl("hdfUserno") as HiddenField));

                if (ddlStype.SelectedValue != "0")
                {
                    Userno += hdfUno.Value + ",";
                    ddlValue += ddlStype.SelectedValue + ",";
                    txtselect += txtselected.Text + ",";
                    txtwait += txtWaiting.Text + ",";
                }
             
            }

            if (Userno == string.Empty)
            {
                objCommon.DisplayMessage(updSelection,"Please select atleast Single Student.", this.Page);
                return;
            }

            if (objSC.UpdateStudentSelection(sessionno, Userno, ddlValue, txtselect, txtwait) == Convert.ToInt32(CustomStatus.RecordUpdated))
            {
                ClearControls();
                objCommon.DisplayMessage(updSelection,"Students selected Successfully!!!", this.Page);
            }
            else
                objCommon.DisplayMessage("Server Error...", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_SelectionProcess.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DropDownList ddlStype = (DropDownList)e.Item.FindControl("ddlStudType");
        HiddenField hdfUno = (HiddenField)e.Item.FindControl("hdfUserno");

        int userno = Convert.ToInt32(hdfUno.Value);
        string selected = objCommon.LookUp("ACD_NEWUSER_REGISTRATION", "ISNULL(SELECTED,0)SELECTED", "SESSIONNO=" + ddlSession.SelectedValue + " AND USERNO=" + userno);
        if (selected == "1")
        {
            ddlStype.SelectedValue = "1";
        }
        else if (selected == "2")
        {
            ddlStype.SelectedValue = "2";
        }
        else if (selected == "3")
        {
            ddlStype.SelectedValue = "3";
        }
        else
        {
            ddlStype.SelectedValue = "0";
        }

        TextBox txtselect = (TextBox)e.Item.FindControl("txtSelected");
        TextBox txtWaiting = (TextBox)e.Item.FindControl("txtWaiting");
        if (ddlStype.SelectedValue == "1")
        {
            txtselect.Enabled = true;
            txtWaiting.Enabled = false;
        }
        else if (ddlStype.SelectedValue == "2")
        {
            txtWaiting.Enabled = true;
            txtselect.Enabled = false;
        }
        else if (ddlStype.SelectedValue == "3")
        {
            txtWaiting.Enabled = true;
            txtselect.Enabled = true;
        }
        else
        {
            txtWaiting.Enabled = true;
            txtselect.Enabled = true;
        }
        Label lblUname = (Label)e.Item.FindControl("lblUserName");
        Label lblSname = (Label)e.Item.FindControl("lblStudName");
        if (ddlStype.SelectedValue == "1")
        {
            lblUname.Style.Add("Color", "Green");
            lblSname.Style.Add("Color", "Green");
        }
        else if (ddlStype.SelectedValue == "2")
        {
            lblUname.Style.Add("Color", "Yellow");
            lblSname.Style.Add("Color", "Yellow");
        }

    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranchName.SelectedValue) + ",@P_SELECTED=" + Convert.ToInt32(ddlStudType.SelectedValue) + "";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReport_Click(object sender, System.EventArgs e)
    {
        {
            ShowReport("Student Selected Report", "StudentSelectedReport.rpt");
        }
    }

    protected void ddlStudType_SelectedIndexChanged(object sender, System.EventArgs e)
    {

        foreach (ListViewDataItem lvItem in lvStudents.Items)
        {
            DropDownList ddlStype = ((lvItem.FindControl("ddlStudType") as DropDownList));
            TextBox txtselected = ((lvItem.FindControl("txtSelected") as TextBox));
            TextBox txtWaiting = ((lvItem.FindControl("txtWaiting") as TextBox));
            if (ddlStype.SelectedValue == "1")
            {
                txtselected.Enabled = true;
                txtWaiting.Enabled = false;
            }
            else if (ddlStype.SelectedValue == "2")
            {
                txtWaiting.Enabled = true;
                txtselected.Enabled = false;
            }
            else if (ddlStype.SelectedValue == "3")
            {
                txtWaiting.Enabled = true;
                txtselected.Enabled = true;
            }
            else
            {
                txtWaiting.Enabled = true;
                txtselected.Enabled = true;
            }
        }
    }

    protected void btnselectedletter_Click(object sender, System.EventArgs e)
    {
        ShowOfferReport("Student Selected Report", "OfferLetterForSelectedStudents.rpt");
        
    }


    private void ShowOfferReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranchName.SelectedValue) + ",@P_SELECTED="+ Convert.ToInt32(ddlStudType.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnWaitngletter_Click(object sender, System.EventArgs e)
    {
        ShowLetterWaitingReport("Student Selected Report", "OfferLetterForWaitingStudents.rpt");
    }

    private void ShowLetterWaitingReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranchName.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnSendMail_Click(object sender, System.EventArgs e)
    {
       

    }
}