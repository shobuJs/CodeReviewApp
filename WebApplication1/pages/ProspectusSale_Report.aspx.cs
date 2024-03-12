using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_ProspectusSale_Report : System.Web.UI.Page
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

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                FillDropDown();
                selectRadio();
            }
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
                Response.Redirect("~/notauthorized.aspx?page=ReptSemester_Promotion.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ReptSemester_Promotion.aspx");
        }
    }

    public void FillDropDown()
    {
        //fill degree
        objCommon.FillDropDownList(ddlDegree, "acd_degree", "degreeno", "degreename", "degreeno>0", "degreename");
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdolistReport.SelectedValue == "3")
            {
                ShowReport("SemPromotion", "rptProspectusBranch.rpt");
            }
            else
            {
                //check the from date always be less than to date
                string[] fromDate = txtRecDate.Text.Split('/');
                string[] toDate = txtToDate.Text.Split('/');
                DateTime fromdate = Convert.ToDateTime(Convert.ToInt32(fromDate[0]) + "/" + Convert.ToInt32(fromDate[1]) + "/" + Convert.ToInt32(fromDate[2]));
                DateTime todate = Convert.ToDateTime(Convert.ToInt32(toDate[0]) + "/" + Convert.ToInt32(toDate[1]) + "/" + Convert.ToInt32(toDate[2]));
                if (fromdate > todate)
                {
                    objCommon.DisplayMessage(updPnl, "From Date always be less than To date. Please Enter proper Date range.", this.Page);
                    txtRecDate.Text = string.Empty;
                    txtToDate.Text = string.Empty;
                }
                else
                {
                    if (rdolistReport.SelectedValue == "1")
                    {
                        ShowReport("SemPromotionRollwise", "rptProspectusUserwise.rpt");
                    }
                    else if (rdolistReport.SelectedValue == "2")
                    {
                        int count = Convert.ToInt32(objCommon.LookUp("ACD_PROSPECTUS", "count(*)", "CONVERT(NVARCHAR,SALE_DATE,103) BETWEEN '" + txtRecDate.Text.Trim() + "' AND '" + txtToDate.Text.Trim() + "'"));
                        if (count > 0)
                        {
                            ShowReport("ProspectusDatewise", "rptProspectusDatewise.rpt");
                        }
                        else
                        {
                            objCommon.DisplayMessage(updPnl, "No Record Found for date: " + txtRecDate.Text, this.Page);
                            return;
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ProspectusSale_btnReport --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            url += "Reports/commonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (rdolistReport.SelectedValue == "1")
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_START_DATE=" + txtRecDate.Text + ",@P_END_DATE=" + txtToDate.Text + ",@P_USERNAME=" + ddlUser.SelectedItem.Text.Trim() + ",@P_UA_NO=" + ddlUser.SelectedValue;
            }
            else if (rdolistReport.SelectedValue == "2")
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_START_DATE=" + txtRecDate.Text + ",@P_END_DATE=" + txtToDate.Text;
            }
            else if (rdolistReport.SelectedValue == "3")
            {
                url += "&param=@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            }
            else if (rdolistReport.SelectedValue == "4")
            {
                url += "&param=@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue;
            }
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updPnl, this.updPnl.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO > 0 AND DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");

    }
    public void GetBranch()
    {
        DataSet ds = new DataSet();
        ds = objCommon.FillDropDown("acd_branch", "BRANCHNO", "LONGNAME", "DEGREENO=" + ddlDegree.SelectedValue.ToString().Trim(), "LONGNAME");
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add("Please Select");
        ddlBranch.SelectedItem.Value = "0";
        ddlBranch.DataTextField = "LONGNAME";
        ddlBranch.DataValueField = "BRANCHNO";
        ddlBranch.DataSource = ds;
        ddlBranch.DataBind();
        ddlBranch.SelectedIndex = 0;
    }
    protected void rdolistReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdolistReport.SelectedValue == "1")
        {
            selectRadio();
        }
        if (rdolistReport.SelectedValue == "2")
        {
            trDegree.Visible = false;
            trBranch.Visible = false;
            trdate.Visible = true;
            trUser.Visible = false;
            txtRecDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
        }
        else if (rdolistReport.SelectedValue == "3")
        {
            trDegree.Visible = true;
            trBranch.Visible = true;
            trdate.Visible = false;
            trUser.Visible = false;
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
        }
        else if (rdolistReport.SelectedValue == "4")
        {
            trDegree.Visible = false;
            trBranch.Visible = false;
            trdate.Visible = false;
            trUser.Visible = false;
        }
    }

    public void selectRadio()
    {
        trDegree.Visible = false;
        trBranch.Visible = false;
        trdate.Visible = true;
        trUser.Visible = true;
        txtRecDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        objCommon.FillDropDownList(ddlUser, "ACD_PROSPECTUS P INNER JOIN USER_ACC UA ON (P.UA_NO=UA.UA_NO)", " DISTINCT UA.UA_NO", "UA.UA_FULLNAME", "P.UA_NO > 0", "UA.UA_NO");
    }


}
