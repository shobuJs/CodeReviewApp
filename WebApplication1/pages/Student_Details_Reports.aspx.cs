//======================================================================================
// PROJECT NAME  : SVCE                                                          
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : Student Details Reports                                                   
// CREATION DATE : 30/09/2020                                                     
// CREATED BY    : NARESH BEERLA                                                     
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_Student_Details_Reports : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController studcon = new StudentController();
    StudentRegistrationController studreg = new StudentRegistrationController();

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
                if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Page Authorization
               //     this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Populate all the DropDownLists
                    FillDropDown();
                }
            }
            else
            {
                // Clear message div
                divMsg.InnerHtml = string.Empty;
            }
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Student_Details_Reports.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=Academic_Student_Details_Reports.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Academic_Student_Details_Reports.aspx");
        }
    }

    //Bind Admission Year , Degree Name,Branch Name,Semester name,Fill IDType,Admission Quota.
    public void FillDropDown()
    {
        objCommon.FillDropDownList(ddlAdmYear, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME DESC");  //Fill Admission Year      
        objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNAME"); //Fill Semester        
    }

    protected void ddlAdmYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdmYear.SelectedIndex > 0)
        {
            DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "", "A.DEGREENO");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlDegree.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
            }
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Please select Admission Batch", this.Page);
            ddlAdmYear.Focus();
        }
    }

    ////On select of Degree bind Branch name in drop down list
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        string deg = GetDegree();
        deg = deg.Replace('$', ',');

        ViewState["DegreeNo"] = deg;
        string[] DegreeNo = deg.Split(',');
        ddlBranch.Items.Clear();
        DataSet ds = objCommon.FillDropDown("ACD_BRANCH a INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON a.BRANCHNO=B.BRANCHNO INNER JOIN ACD_DEGREE DE ON (DE.DEGREENO = B.DEGREENO)", "B.BRANCHNO", "DE.DEGREENAME+'-'+A.LONGNAME AS LONGNAME", "B.DEGREENO in(" + deg + ")", "B.BRANCHNO");
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ddlBranch.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
        }
        DataSet ds1 = objCommon.FillDropDown("ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO) INNER JOIN ACD_DEGREE DE ON (DE.DEGREENO = B.DEGREENO)", "DISTINCT(A.BRANCHNO)", "DE.DEGREENAME+'-'+A.LONGNAME AS LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO in(" + deg + ") AND B.DEPTNO =" + Session["userdeptno"].ToString(), "LONGNAME");
        //  string BranchNos = "";
        if (ds1.Tables[0].Rows.Count > 0)
        {
            ddlBranch.Items.Clear();
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                ddlBranch.Items.Add(new ListItem(Convert.ToString(ds1.Tables[0].Rows[i][1]), Convert.ToString(ds1.Tables[0].Rows[i][0])));
            }
        }
        ddlBranch.Focus();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        string branchno = GetBranch();
    }

    private string GetDegree()
    {
        string degreeNo = "";
        string degreeno = string.Empty;

        foreach (ListItem item in ddlDegree.Items)
        {
            if (item.Selected == true)
            {
                degreeNo += item.Value + '$';
            }
        }

        if (degreeNo != "")
        {
            degreeno = degreeNo.Substring(0, degreeNo.Length - 1);
            string[] degValue = degreeno.Split('$');

        }
        // degreeno = degreeno.Substring(0, degreeno.Length - 1);
        //}

        if (degreeno == "")
        {
            degreeno = "0";
        }

        return degreeno;

    }

    private string GetBranch()
    {
        string branchNo = "";
        string branchno = string.Empty;
        int X = 0;

        foreach (ListItem item in ddlBranch.Items)
        {
            if (item.Selected == true)
            {
                branchNo += item.Value + '$';
                X = 1;
            }
        }

        if (X == 0)
        {
            branchNo = "0";
        }

        if (branchNo != "0")
        {
            branchno = branchNo.Substring(0, branchNo.Length - 1);
        }
        else
        {
            branchno = branchNo;
        }
        if (branchno != "")
        {
            string[] bValue = branchno.Split('$');

        }
        // degreeno = degreeno.Substring(0, degreeno.Length - 1);
        //}
        return branchno;

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlAdmYear.SelectedIndex = 0;
        //ddlDegree.SelectedIndex = 0;
        ddlDegree.ClearSelection();
        ddlBranch.SelectedIndex = 0;
        ddlBranch.ClearSelection();
        ddlSem.SelectedIndex = 0;       
        //Response.Redirect(Request.Url.ToString());
    }
    protected void btnProject_Click(object sender, EventArgs e)
    {
        int admBatch = Convert.ToInt32(ddlAdmYear.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlAdmYear.SelectedValue);
        string degreeno = GetDegree();
        string branchno = GetBranch();
        int semester = Convert.ToInt32(ddlSem.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlSem.SelectedValue);
        DataSet dsproject = objCommon.DynamicSPCall_Select("PKG_ACD_STUDENT_GET_PROJECT_DETAILS_FOR_EXCEL", "@P_ADMBATCH,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO", "" + admBatch + "," + degreeno + "," + branchno + "," + semester + "");
        DataGrid dgp = new DataGrid();
        if (dsproject.Tables.Count > 0 && dsproject.Tables[0].Rows.Count > 0)
        {
            string attachment = "attachment; filename=" + "ProjectExcelReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            dgp.DataSource = dsproject.Tables[0];
            dgp.DataBind();
            dgp.HeaderStyle.Font.Bold = true;
            dgp.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            btnCancel_Click(sender, e);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No Data Found for this Selection", this.Page);
            btnCancel_Click(sender, e);
            return;
        }        
    }                                                                                   	
    protected void btnInternshipExcel_Click(object sender, EventArgs e) 
    {                                                                                   
        int admBatch = Convert.ToInt32(ddlAdmYear.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlAdmYear.SelectedValue);
        string degreeno = GetDegree();
        string branchno = GetBranch();
        int semester = Convert.ToInt32(ddlSem.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlSem.SelectedValue);

        DataSet ds = studreg.GetStudentInternshipDetails_Excel(Convert.ToInt32(ddlAdmYear.SelectedItem.Value), degreeno, branchno, semester);
        DataGrid dg = new DataGrid();
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string attachment = "attachment; filename=" + "InternshipExcelReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            dg.DataSource = ds.Tables[0];
            dg.DataBind();
            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            btnCancel_Click(sender, e);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No Data Found for this Selection", this.Page);
            btnCancel_Click(sender, e);
            return;
        }

    }
    protected void btnAwards_Click(object sender, EventArgs e)
    {
        CallExcel_Awards();
        btnCancel_Click(sender, e);
    }
    private void CallExcel_Awards()
    {
        int admBatch = Convert.ToInt32(ddlAdmYear.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlAdmYear.SelectedValue);
        string degreeno = GetDegree();
        string branchno = GetBranch();
        int semester = Convert.ToInt32(ddlSem.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlSem.SelectedValue);
        DataSet dsawards = objCommon.DynamicSPCall_Select("PKG_ACD_STUDENT_GET_ACHIEVEMENTS_DETAILS_FOR_EXCEL", "@P_ADMBATCH,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO", "" + admBatch + "," + degreeno + "," + branchno + "," + semester + "");
        DataGrid dgp = new DataGrid();
        if (dsawards.Tables.Count > 0 && dsawards.Tables[0].Rows.Count > 0)
        {
            string attachment = "attachment; filename=" + "Awards&AchievementsExcelReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            dgp.DataSource = dsawards.Tables[0];
            dgp.DataBind();
            dgp.HeaderStyle.Font.Bold = true;
            dgp.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            //btnCancel_Click(sender, e);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No Data Found for this Selection", this.Page);
          //  btnCancel_Click(sender, e);
            return;
        }        
    }
    protected void btnPublications_Click(object sender, EventArgs e)
    {
        CallExcel_Publication();
        btnCancel_Click(sender, e);
    }
    private void CallExcel_Publication()
    {
        int admBatch = Convert.ToInt32(ddlAdmYear.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlAdmYear.SelectedValue);
        string degreeno = GetDegree();
        string branchno = GetBranch();
        int semester = Convert.ToInt32(ddlSem.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlSem.SelectedValue);
        DataSet dspub = objCommon.DynamicSPCall_Select("PKG_ACD_STUDENT_GET_PUBLICATIONS_DETAILS_FOR_EXCEL", "@P_ADMBATCH,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO", "" + admBatch + "," + degreeno + "," + branchno + "," + semester + "");
        DataGrid dgp = new DataGrid();
        if (dspub.Tables.Count > 0 && dspub.Tables[0].Rows.Count > 0)
        {
            string attachment = "attachment; filename=" + "PublicationsExcelReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            dgp.DataSource = dspub.Tables[0];
            dgp.DataBind();
            dgp.HeaderStyle.Font.Bold = true;
            dgp.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            //btnCancel_Click(sender, e);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No Data Found for this Selection", this.Page);
            //  btnCancel_Click(sender, e);
            return;
        }
    }
    protected void btnScholarship_Click(object sender, EventArgs e)
    {
        CallExcel_Scholarship();
        btnCancel_Click(sender, e);
    }
    private void CallExcel_Scholarship()
    {
        int admBatch = Convert.ToInt32(ddlAdmYear.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlAdmYear.SelectedValue);
        string degreeno = GetDegree();
        string branchno = GetBranch();
        int semester = Convert.ToInt32(ddlSem.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlSem.SelectedValue);
        DataSet dsScholarship = objCommon.DynamicSPCall_Select("PKG_ACD_STUDENT_GET_SCHOLARSHIP_DETAILS_FOR_EXCEL", "@P_ADMBATCH,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO", "" + admBatch + "," + degreeno + "," + branchno + "," + semester + "");
        DataGrid dgp = new DataGrid();
        if (dsScholarship.Tables.Count > 0 && dsScholarship.Tables[0].Rows.Count > 0)
        {
            string attachment = "attachment; filename=" + "ScholarshipExcelReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            dgp.DataSource = dsScholarship.Tables[0];
            dgp.DataBind();
            dgp.HeaderStyle.Font.Bold = true;
            dgp.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            //btnCancel_Click(sender, e);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No Data Found for this Selection", this.Page);
            //  btnCancel_Click(sender, e);
            return;
        }
    }
    protected void btnIndusVisit_Click(object sender, EventArgs e)
    {
        int admBatch = Convert.ToInt32(ddlAdmYear.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlAdmYear.SelectedValue);
        string degreeno = GetDegree();
        string branchno = GetBranch();
        int semester = Convert.ToInt32(ddlSem.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlSem.SelectedValue);
        DataSet dsIndusVisit = objCommon.DynamicSPCall_Select("PKG_ACD_STUDENT_GET_INDUSTRIAL_VISIT_DETAILS_FOR_EXCEL", "@P_ADMBATCH,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO", "" + admBatch + "," + degreeno + "," + branchno + "," + semester + "");
        DataGrid dgiv = new DataGrid();
        if (dsIndusVisit.Tables.Count > 0 && dsIndusVisit.Tables[0].Rows.Count > 0)
        {
            string attachment = "attachment; filename=" + "IndustrailVisitExcelReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            dgiv.DataSource = dsIndusVisit.Tables[0];
            dgiv.DataBind();
            dgiv.HeaderStyle.Font.Bold = true;
            dgiv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            btnCancel_Click(sender, e);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No Data Found for this Selection", this.Page);
            btnCancel_Click(sender, e);
            return;
        }
        btnCancel_Click(sender, e);
    }
    protected void btnMouDetails_Click(object sender, EventArgs e)
    {
        DataSet dsMou = studreg.GetStudentMouDetails_Excel();
        DataGrid dMou = new DataGrid();
        if (dsMou.Tables.Count > 0 && dsMou.Tables[0].Rows.Count > 0)
        {
            string attachment = "attachment; filename=" + "MouDetailsExcelReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            dMou.DataSource = dsMou.Tables[0];
            dMou.DataBind();
            dMou.HeaderStyle.Font.Bold = true;
            dMou.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            btnCancel_Click(sender, e);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No Data Found for this Selection", this.Page);
            btnCancel_Click(sender, e);
            return;
        }        
    }
    protected void btnExtension_Click(object sender, EventArgs e)
    {
        DataSet dsExtsn = studreg.GetStudentExtensionDetails_Excel();
        DataGrid dExtsn = new DataGrid();
        if (dsExtsn.Tables.Count > 0 && dsExtsn.Tables[0].Rows.Count > 0)
        {
            string attachment = "attachment; filename=" + "ExtensionDetailsExcelReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            dExtsn.DataSource = dsExtsn.Tables[0];
            dExtsn.DataBind();
            dExtsn.HeaderStyle.Font.Bold = true;
            dExtsn.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            btnCancel_Click(sender, e);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No Data Found for this Selection", this.Page);
            btnCancel_Click(sender, e);
            return;
        }        
    }
}