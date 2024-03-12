//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : STUDENT ID CARD                                     
// CREATION DATE : 
// ADDED BY      : ASHISH DHAKATE                                                  
// ADDED DATE    : 16-DEC-2011
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================
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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Academic_StudentIDCard : System.Web.UI.Page
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

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    PopulateDropDownList();
                }
            }

            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCard.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
       
    }

    protected void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN BATCH
            objCommon.FillDropDownList(ddlDegree,"ACD_DEGREE","DEGREENO","DEGREENAME","DEGREENO>0","DEGREENO");
            // FILL DROPDOWN SEMESTER
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            //FILL DROPDOWN BRANCH
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCard.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController studCont = new StudentController();
            DataTable dt;
            if (Session["listIdCard"] != null && (DataTable)Session["listIdCard"] != null)
            {
                dt = (DataTable)Session["listIdCard"];
                string searchText = txtSearchText.Text.Trim();
                string searchBy = ("enrollmentno");
                DataTableReader dtr = studCont.RetrieveStudentDetails(searchText, searchBy).Tables[0].CreateDataReader();
                if (dtr != null && dtr.Read())
                {
                    DataRow dr = dt.NewRow();
                    dr["NAME"] = dtr["NAME"];
                    dr["ENROLLMENTNO"] = dtr["ENROLLMENTNO"];
                    dr["CODE"] = dtr["CODE"];
                    dr["SHORTNAME"] = dtr["SHORTNAME"];
                    dr["YEARNAME"] = dtr["YEARNAME"];
                    dr["SEMESTERNAME"] = dtr["SEMESTERNAME"];
                    dr["BATCHNAME"] = dtr["BATCHNAME"];
                    dt.Rows.Add(dr);
                    Session["listIdCard"] = dt;
                    lvStudentRecords.DataSource = dt;
                    lvStudentRecords.DataBind();
                    lblEnrollmentNo.Text = string.Empty;
                }
                else
                {
                    lblEnrollmentNo.Text = "Enrollment No. does not match.";
                }
                dtr.Close();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCard.btnAdd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController studCont = new StudentController();
            DataSet ds;
            int degreeno = 0;
            int branchno = 0; 
            int semesterno = 0;
            if(Session["listIdCard"]==null || ((DataTable)Session["listIdCard"]==null))
            {           
                degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
                branchno = Convert.ToInt32(ddlBranch.SelectedValue);
                semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                ds = studCont.GetStudentListForIdentityCard(degreeno, branchno, semesterno);
                if (ds != null && ds.Tables.Count > 0)
                {
                    lvStudentRecords.DataSource = ds.Tables[0];
                    lvStudentRecords.DataBind();
                }
                Session["listIdCard"] = ds.Tables[0];
            }
            else
            {
                degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
                branchno = Convert.ToInt32(ddlBranch.SelectedValue);
                semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                ds = studCont.GetStudentListForIdentityCard(degreeno, branchno, semesterno);
                if (ds != null && ds.Tables.Count > 0)
                {
                    lvStudentRecords.DataSource = ds.Tables[0];
                    lvStudentRecords.DataBind();
                }
                Session["listIdCard"] = ds.Tables[0];
            }           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCard.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnPrintReport_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = GetStudentIDs();
            if (!string.IsNullOrEmpty(ids))
                ShowReport(ids, "Student_ID_Card_Report", "StudentIDCardFront.rpt");
            else
                objCommon.DisplayMessage("Please Select Students!", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCard.btnPrintReport_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentIDCard.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentIDCard.aspx");
        }
    }
   
    protected void BindListView()
    {
        try
        {
            StudentController studCont = new StudentController();
            int degreeno = 0;
            int branchno = 0;
            int semesterno = 0;
            degreeno = ddlDegree.SelectedIndex;
            branchno = ddlBranch.SelectedIndex;
            semesterno = ddlSemester.SelectedIndex;
            DataSet ds = studCont.GetStudentListForIdentityCard(degreeno, branchno, semesterno);
            if (ds != null && ds.Tables.Count > 0)
            {
                lvStudentRecords.DataSource = ds.Tables[0];
                lvStudentRecords.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCard.BindListView --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            txtSearchText.Text = string.Empty;
            Session["listIdCard"] = null;
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    private void ShowReport(string param, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",academicsession=" + (DateTime.Today.Year).ToString() + "-" + (DateTime.Today.Year + 1).ToString() + ",@P_IDNO=" + param;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentReport1.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetStudentIDs()
    {
        string studentIds = string.Empty;
        try
        {
            foreach (ListViewDataItem  item in lvStudentRecords.Items)
            {
                if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                    if (studentIds.Length > 0)
                        studentIds += ".";
                    studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCard.GetStudentIDs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return studentIds;
    }

    protected void btnBackSide_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = GetStudentIDs();
            if (!string.IsNullOrEmpty(ids))
                ShowReport(ids, "Student_ID_Card_Report", "StudentIDCardBack.rpt");
            else
                objCommon.DisplayMessage("Please Select Students!", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCard.btnBackSide_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");            
    }
}
