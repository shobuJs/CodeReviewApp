using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
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

public partial class ACADEMIC_Generate_RollNo : System.Web.UI.Page
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

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //Fill Dropdown degree
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (D.DEGREENO = CD.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO >0 AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "D.DEGREENO");
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
                Response.Redirect("~/notauthorized.aspx?page=Semester_promotion.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Semester_promotion.aspx");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            ddlBranch.Items.Clear();
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            rdoSex.SelectedIndex = 1;
            txtFrmRollno.Text = string.Empty;
            txtToRollno.Text = string.Empty;
            txtFrmRollno.Enabled = true;

            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + ddlDegree.SelectedValue, "LONGNAME");
            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlDegree.Focus();
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            rdoSex.SelectedIndex = 1;
            txtFrmRollno.Text = string.Empty;
            txtToRollno.Text = string.Empty;
            txtFrmRollno.Enabled = true;
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            rdoSex.SelectedIndex = 1;
            txtFrmRollno.Text = string.Empty;
            txtToRollno.Text = string.Empty;
            txtFrmRollno.Enabled = true;

            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S", "S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0", "s.SEMESTERNO");
        }
        else
        {
            ddlBranch.Focus();
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            rdoSex.SelectedIndex = 1;
            txtFrmRollno.Text = string.Empty;
            txtToRollno.Text = string.Empty;
            txtFrmRollno.Enabled = true;
        }
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            ddlSection.Items.Clear();
            rdoSex.SelectedIndex = 1;
            txtFrmRollno.Text = string.Empty;
            txtToRollno.Text = string.Empty;
            txtFrmRollno.Enabled = true;

            objCommon.FillDropDownList(ddlSection, "ACD_SECTION S INNER JOIN ACD_STUDENT_RESULT SR ON (S.SECTIONNO = SR.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "S.SECTIONNO > 0", "SECTIONNO");
            ddlSection.Focus();
        }
        else
        {
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Focus();
            rdoSex.SelectedIndex = 1;
            txtFrmRollno.Text = string.Empty;
            txtToRollno.Text = string.Empty;
            txtFrmRollno.Enabled = true;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objSC = new StudentController();
            Student objS = new Student();

            objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            objS.SectionNo = Convert.ToInt32(ddlSection.SelectedValue);
            objS.Sex = Convert.ToChar(rdoSex.SelectedValue);
            objS.Uano = Convert.ToInt32(Session["userno"]);
            objS.CollegeCode = Session["colcode"].ToString();
            int rollFrom = Convert.ToInt32(txtFrmRollno.Text);
            int rollTo = Convert.ToInt32(txtToRollno.Text);

            CustomStatus cs = (CustomStatus)objSC.GenerateClassRollNumber(objS, rollFrom, rollTo);

            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(updPnl, "Roll Number is Generated Successfully.", this.Page);
                BindListView();
                clear();

            }
            else
            {
                objCommon.DisplayMessage(updPnl, "Error..", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.btnGenerate_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    void clear()
    {
        ddlDegree.SelectedIndex = 0;
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        rdoSex.SelectedIndex = 1;
        txtFrmRollno.Text = string.Empty;
        txtToRollno.Text = string.Empty;
        txtFrmRollno.Enabled = true;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("", "");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SECTIONNO=" + ddlSection.SelectedValue;
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
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    protected void rdoSex_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoSex.SelectedValue == "M")
        {
            int fcount = Convert.ToInt32(objCommon.LookUp("ACD_ROLLNO", "COUNT(*)", "DEGREENO=" + ddlDegree.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND MF='F'"));
            if (fcount > 0)
            {
                string rollFrom = objCommon.LookUp("ACD_ROLLNO", "MAX(ISNULL((ROLLNOTO),0))+1", "DEGREENO=" + ddlDegree.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND MF='F'");
                txtFrmRollno.Enabled = false;
                txtFrmRollno.Text = rollFrom;
            }
            else
            {
                objCommon.DisplayMessage(updPnl, "Please First Generate Class Rollnumbers For Females.", this.Page);
                rdoSex.SelectedValue = "F";
                return;
            }
        }
        else
        {
            string rollFrom = objCommon.LookUp("ACD_ROLLNO", "MAX(ISNULL((ROLLNOTO),0))", "DEGREENO=" + ddlDegree.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND MF='F'");
            txtFrmRollno.Enabled = false;
            txtFrmRollno.Text = rollFrom;
            objCommon.DisplayMessage(updPnl, "Roll Numbers are already Generated for Females.", this.Page);
            rdoSex.SelectedValue = "M";
        }

    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSection.SelectedIndex > 0)
        {
            rdoSex.SelectedIndex = 1;
            txtFrmRollno.Text = string.Empty;
            txtToRollno.Text = string.Empty;
            txtFrmRollno.Enabled = true;

            int fcount = Convert.ToInt32(objCommon.LookUp("ACD_ROLLNO", "COUNT(*)", "DEGREENO=" + ddlDegree.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND MF='F'"));
            if (fcount > 0)
            {
                string rollFrom = objCommon.LookUp("ACD_ROLLNO", "MAX(ISNULL((ROLLNOTO),0))+1", "DEGREENO=" + ddlDegree.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND MF='F'");
                txtFrmRollno.Enabled = false;
                txtFrmRollno.Text = rollFrom;
                objCommon.DisplayMessage(updPnl, "Roll Numbers are already Generated for Females.", this.Page);
                rdoSex.SelectedValue = "M";
            }
        }
        else
        {
            rdoSex.SelectedIndex = 1;
            txtFrmRollno.Text = string.Empty;
            txtToRollno.Text = string.Empty;
            txtFrmRollno.Enabled = true;
            ddlSection.Focus();
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
    }

    protected void BindListView()
    {
        try
        {
            DataSet ds = null;
            StudentController objSC = new StudentController();
            Student objS = new Student();

            objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            objS.SectionNo = Convert.ToInt32(ddlSection.SelectedValue);
            objS.Sex = Convert.ToChar(rdoSex.SelectedValue);

            ds = objSC.GetStudentListForRollNo(objS);

            if (ds.Tables[0].Rows.Count > 0)
            {
                int totStud = ds.Tables[0].Rows.Count;
                pnlStud.Visible = true;
                if (rdoSex.SelectedValue == "M")
                    lblCount.Text = "Total Male Students :" + totStud.ToString();
                else
                    lblCount.Text = "Total Female Students :" + totStud.ToString();
                lvStudents.DataSource = ds;
                lvStudents.DataBind();

            }
            else
            {
                pnlStud.Visible = false;
                lblCount.Text = string.Empty;
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                objCommon.DisplayMessage(pnlStud, "No Student Found For Current Selection.", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.btnShow_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
