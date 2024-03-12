//======================================================================================
// PROJECT NAME   : NITGOA                                                 
// MODULE NAME    : ACADEMIC                                                             
// PAGE NAME      : LATE FEES REPORT
// CREATION DATE  : 22-JAN-2014                                                          
// CREATED BY     : UMESH GANORKAR                                                   
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

public partial class ACADEMIC_LateFeeReport : System.Web.UI.Page
{
    Common objCommon = new Common();
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
                //objCommon.FillDropDownList(ddlSchClg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME+'('+SHORT_NAME +'-'+ CODE +')' as COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                int mul_col_flag = Convert.ToInt32(objCommon.LookUp("REFF", "MUL_COL_FLAG", "CollegeName='" + Session["coll_name"].ToString() + "'"));
                if (mul_col_flag == 0)
                {
                    objCommon.FillDropDownList(ddlSchClg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    ddlSchClg.SelectedIndex = 1;
                }
                else
                {
                     objCommon.FillDropDownList(ddlSchClg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    ddlSchClg.SelectedIndex = 0;
                }           
                
                objCommon.FillDropDownList(ddlRecType, "ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_TITLE", "RCPTTYPENO>0", "RECIEPT_TITLE");
            }

        }

        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
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
                Response.Redirect("~/notauthorized.aspx?page=LateFeeReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=LateFeeReport.aspx");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        if (ddlDegree.SelectedIndex > 0)
        {
            this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH a INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON a.BRANCHNO=B.BRANCHNO", "B.BRANCHNO", "A.LONGNAME", "DEGREENO=" + ddlDegree.SelectedValue+"AND B.COLLEGE_ID="+ddlSchClg.SelectedValue, "A.SHORTNAME");
            ddlBranch.Focus();
        }
        else
        {
            ShowMessage("Please select degree");
            ddlDegree.Focus();
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("REPORT", "rptLateFee.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string rectype = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO =" + Convert.ToInt32(ddlRecType.SelectedValue) + "");
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_RECEIPT_CODE=" + rectype + ",@P_FEE_TITLE_NO=" + Convert.ToInt32(ddlFeesHead.SelectedValue) + ",@P_FROMDATE=" + txtFromDate.Text + ",@P_TODATE=" + txtToDate.Text + ",@P_fees_Head=" + ddlFeesHead.SelectedItem.Text;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MarksEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlRecType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string rectype = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO =" + Convert.ToInt32(ddlRecType.SelectedValue) + "");
        objCommon.FillDropDownList(ddlFeesHead, "ACD_FEE_TITLE", "FEE_TITLE_NO", "FEE_LONGNAME", "RECIEPT_CODE ='" + rectype + "' AND FEE_LONGNAME NOT IN ('')", "FEE_LONGNAME");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnExcelSheet_Click(object sender, EventArgs e)
    {
        try
        {
            string rectype = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO =" + Convert.ToInt32(ddlRecType.SelectedValue) + "");
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=xls";
            url += "&filename=" + ddlDegree.SelectedItem.Text + "_" + ddlBranch.SelectedItem.Text + "_" + ddlSemester.SelectedItem.Text + "_" + rectype + ".xls";
            url += "&path=~,Reports,Academic,rptLateFee.rpt";
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString()
                    + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)
                    + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue)
                    + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue)
                    + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)
                    + ",@P_RECEIPT_CODE=" + rectype
                    + ",@P_FEE_TITLE_NO=" + Convert.ToInt32(ddlFeesHead.SelectedValue)

                    + ",@P_FROMDATE=" + txtFromDate.Text
                    + ",@P_TODATE=" + txtToDate.Text
                    + ",@P_fees_Head=" + ddlFeesHead.SelectedItem.Text;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " window.close();";
            divMsg.InnerHtml += " </script>";


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlSchClg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            if (ddlSchClg.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND COLLEGE_ID="+ddlSchClg.SelectedValue, "SESSIONNO DESC");
                ddlSession.Focus();
            }
            else
            {
                ShowMessage("Please select college/school Name");
                ddlSchClg.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ddlSchClg_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID = " + Convert.ToInt32(ddlSchClg.SelectedValue), "A.DEGREENAME");
            ddlDegree.Focus();
        }
        else
        {
            ddlDegree.SelectedIndex = 0;
            ddlSession.Focus();
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ViewState["YearWise"] = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "ISNULL(YEARWISE,0)YEARWISE", "DEGREENO=" + ddlDegree.SelectedValue));
            if (ViewState["YearWise"].ToString() == "1")
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_YEAR", "SEM", "YEARNAME", "YEAR<>0", "YEAR");
            }
            else
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");

            }
            ddlSemester.Focus();
        }
        else
        {
            ddlSemester.SelectedIndex = 0;
            ddlBranch.Focus();
        }
    }
}
