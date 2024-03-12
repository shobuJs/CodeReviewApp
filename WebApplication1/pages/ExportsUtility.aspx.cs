/* ======================================================================================
    PROJECT NAME  : RFCAMPUS SVCE                                                               
    MODULE NAME   :                                                              
    PAGE NAME     : ExportsUtility.aspx                                       
    CREATION DATE : 09-NOV-2019                                                        
    CREATED BY    : TOHSIF KHAN                                            
    MODIFIED DATE :                                                                      
    MODIFIED DESC :                                                                      
======================================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.IO;

public partial class ACADEMIC_ExportsUtility : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string deptno = string.Empty;
    string degreeno = string.Empty;
    string admbatchno = string.Empty;
    string semno = string.Empty;
    string branchno = string.Empty;
    string schemeno = string.Empty;
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
               // CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }

                PopulateDeptDDL();
                PopulateDegreeDDL();
                PopulateAdmBatchDDL();
                PopulateSemesterDDL();
                PopulateSectionDDL();
                ddlBranchStud.Items.Clear();
                ddlBranchStud.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlSchemeStud.Items.Clear();
                ddlSchemeStud.Items.Insert(0, new ListItem("Please Select", "0"));
                PopulateDeptDDLForEmp();

                PopulateSessionRegDDL();
                PopulateDegreeRegDDL();
                ddlBranchReg.Items.Clear();
                ddlBranchReg.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlSchemeReg.Items.Clear();
                ddlSchemeReg.Items.Insert(0, new ListItem("Please Select", "0"));
                PopulateSectionRegDDL();
                PopulateSemesterRegDDL();
            }
            Session["reportdata"] = null;
        }
       divMsg.InnerHtml = string.Empty;
    }
    private void PopulateSectionDDL()
    {
        try
        {
            //Fill SECTION

            objCommon.FillDropDownList(ddlSectionStud, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO >0", "SECTIONNAME");
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility.PopulateSectionDDL-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void PopulateDeptDDL()
    {
        try
        {           
            //Fill Department
            if (Session["usertype"].ToString() != "1")
                objCommon.FillDropDownList(ddlDeptCourse, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO >0 AND DEPTNO =" + Session["userdeptno"].ToString(), "DEPTNAME");
            else
                objCommon.FillDropDownList(ddlDeptCourse, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO >0", "DEPTNAME");

           // ddlDeptCourse.SelectedValue = deptno;
           //// ddlDeptCourse.Items.Insert(0, new ListItem("Please Select", "0"));
           // ddlDeptCourse.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility.PopulateDeptDDL-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ExportsUtility.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ExportsUtility.aspx");
        }
    }    
    protected void ddlDeptCourse_SelectedIndexChanged(object sender, EventArgs e)
    { 
        string id = ddlDeptCourse.SelectedItem.Value;
    }
    protected void btnExportCourse_Click(object sender, EventArgs e)
    {
        bool CompleteRequest = false;
        try
        {
            string DepartmentId = ddlDeptCourse.SelectedItem.Value;
            SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@DepartmentId", Convert.ToInt32(DepartmentId.ToString()));
            DataTable CourseTable = objSQLHelper.ExecuteDataSetSP("spExportCourseDataToExcel", objParams).Tables[0];
            string FileName = ddlDeptCourse.SelectedItem.Text.Replace(' ', '_');
            GridView GVStatus = new GridView();
            string ContentType = string.Empty;
            if (CourseTable.Rows.Count > 0)
            {
                GVStatus.DataSource = CourseTable;
                GVStatus.DataBind();
                string attachment = "attachment;filename="+ FileName + ".xlsx";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVStatus.RenderControl(htw);
                Response.Write(sw.ToString());
                CompleteRequest = true;
            }
            else
            {
                GVStatus.DataSource = null;
                GVStatus.DataBind();
                objCommon.DisplayMessage("No record found...", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility_btnExportCourse_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        if (CompleteRequest)
            Response.End();
    }
    protected void btnExportDegree_Click(object sender, EventArgs e)
    {
        bool CompleteRequest = false;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
            SqlParameter[] objParams = new SqlParameter[0];
            DataTable DegreeTable = objSQLHelper.ExecuteDataSetSP("spExportDegreeDataToExcel", objParams).Tables[0];
            GridView GVStatus = new GridView();
            string ContentType = string.Empty;
            if (DegreeTable.Rows.Count > 0)
            {
                GVStatus.DataSource = DegreeTable;
                GVStatus.DataBind();
                string attachment = "attachment;filename=Degree.xlsx";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVStatus.RenderControl(htw);
                Response.Write(sw.ToString());
                CompleteRequest = true;
            }
            else
            {
                GVStatus.DataSource = null;
                GVStatus.DataBind();
                objCommon.DisplayMessage("No record found...", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility_btnExportDegree_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        if (CompleteRequest)
            Response.End();
    }
    protected void btnExportBranch_Click(object sender, EventArgs e)
    {
        bool CompleteRequest = false;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
            SqlParameter[] objParams = new SqlParameter[0];
            DataTable BranchTable = objSQLHelper.ExecuteDataSetSP("spExportBranchDataToExcel", objParams).Tables[0];
            GridView GVStatus = new GridView();
            string ContentType = string.Empty;
            if (BranchTable.Rows.Count > 0)
            {
                GVStatus.DataSource = BranchTable;
                GVStatus.DataBind();
                string attachment = "attachment;filename=Branch.xlsx";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVStatus.RenderControl(htw);
                Response.Write(sw.ToString());
                CompleteRequest = true;
            }
            else
            {
                GVStatus.DataSource = null;
                GVStatus.DataBind();
                objCommon.DisplayMessage("No record found...", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility_btnExportBranch_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        if (CompleteRequest)
            Response.End();
    }
    protected void btnExportDepartment_Click(object sender, EventArgs e)
    {
        bool CompleteRequest = false;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
            SqlParameter[] objParams = new SqlParameter[0];
            DataTable DepartmentTable = objSQLHelper.ExecuteDataSetSP("spExportDepartmentDataToExcel", objParams).Tables[0];
            GridView GVStatus = new GridView();
            string ContentType = string.Empty;
            if (DepartmentTable.Rows.Count > 0)
            {
                GVStatus.DataSource = DepartmentTable;
                GVStatus.DataBind();
                string attachment = "attachment;filename=Departments.xlsx";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVStatus.RenderControl(htw);
                Response.Write(sw.ToString());
                CompleteRequest = true;
            }
            else
            {
                GVStatus.DataSource = null;
                GVStatus.DataBind();
                objCommon.DisplayMessage("No record found...", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility_btnExportDepartment_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        if (CompleteRequest)
            Response.End();
    }
    protected void btnExportStudents_Click(object sender, EventArgs e)
    {
        bool CompleteRequest = false;
        try
        {
            int DegreeId = int.Parse(ddlDegreeStud.SelectedItem.Value);
            int BranchId = int.Parse(ddlBranchStud.SelectedItem.Value);
            int AdmissionBatchId = int.Parse(ddlAdmBatchStud.SelectedItem.Value);
            int schemeId = int.Parse(ddlSchemeStud.SelectedItem.Value);
            int SemesterId = int.Parse(ddlSemesterStud.SelectedItem.Value);
            int SectionId = int.Parse(ddlSectionStud.SelectedItem.Value);

            string BranchName = ddlBranchStud.SelectedItem.Text.Replace(' ','_');
            string SchemeName = ddlSchemeStud.SelectedItem.Text.Replace(' ', '_');
            string SemesterName = ddlSemesterStud.SelectedItem.Text.Replace(' ', '_');
            string SectionName = ddlSectionStud.SelectedItem.Text.Replace(' ', '_');
            string FileName = string.Empty;
            if (Convert.ToInt32(SectionId) != 0)
                FileName = BranchName.Replace(' ', '_') +"-" + SchemeName.Replace(' ', '_') + "-" + SemesterName.Replace(' ', '_') + "-" + SectionName.Replace(' ', '_');
            else
                FileName = BranchName.Replace(' ', '_') + "-" + SchemeName.Replace(' ', '_') + "-" + SemesterName.Replace(' ', '_');

            SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
            SqlParameter[] objParams = new SqlParameter[6];
            objParams[0] = new SqlParameter("@P_DEGREENO", DegreeId);
            objParams[1] = new SqlParameter("@P_BRANCHNO", BranchId);
            objParams[2] = new SqlParameter("@P_ADMISSIONBATCH", AdmissionBatchId);
            objParams[3] = new SqlParameter("@P_SCHEMENO", schemeId);
            objParams[4] = new SqlParameter("@P_SEMESTERNO", SemesterId);
            objParams[5] = new SqlParameter("@P_SECTIONNO", SectionId);
            DataTable StudentsTable = objSQLHelper.ExecuteDataSetSP("SPTBL_ACD_STUDENT_EXPORT", objParams).Tables[0];
            GridView GVStatus = new GridView();
            string ContentType = string.Empty;
            if (StudentsTable.Rows.Count > 0)
            {
                GVStatus.DataSource = StudentsTable;
                GVStatus.DataBind();
                string attachment = "attachment;filename=" + FileName + ".xlsx";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVStatus.RenderControl(htw);
                Response.Write(sw.ToString());
                CompleteRequest = true;
            }
            else
            {
                GVStatus.DataSource = null;
                GVStatus.DataBind();
                objCommon.DisplayMessage("No record found...", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility_btnExportStudents_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        if (CompleteRequest)
            Response.End();
    }
    private void PopulateDegreeDDL()
    {
        try
        {
            //Fill Degree
                objCommon.FillDropDownList(ddlDegreeStud, "[dbo].[ACD_DEGREE]", "DEGREENO", "DEGREENAME", "DEGREENO >0", "DEGREENAME");           
            ddlDegreeStud.SelectedValue = degreeno;
            //// ddlDeptCourse.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlDegreeStud.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility.PopulateDegreeDDL-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void PopulateAdmBatchDDL()
    {
        try
        {
            //Fill Degree
            objCommon.FillDropDownList(ddlAdmBatchStud, "[ACD_ADMBATCH]", "BATCHNO", "BATCHNAME", "BATCHNO >0", "BATCHNAME");

            //ddlAdmBatchStud.SelectedValue = admbatchno;
            //// ddlDeptCourse.Items.Insert(0, new ListItem("Please Select", "0"));
            //ddlAdmBatchStud.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility.PopulateAdmBatchDDL-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void PopulateSemesterDDL()
    {
        try
        {
            //Fill Degree
            objCommon.FillDropDownList(ddlSemesterStud, "[dbo].[ACD_SEMESTER]", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO >0", "SEMESTERNAME");

            //ddlSemesterStud.SelectedValue = semno;
            //// ddlDeptCourse.Items.Insert(0, new ListItem("Please Select", "0"));
            //ddlSemesterStud.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility.PopulateSemesterDDL-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void PopulateBranchDDL()
    {
        try
        {
            int DegreeNo = int.Parse(ddlDegreeStud.SelectedItem.Value);
            //Fill Degree
            objCommon.FillDropDownList(ddlBranchStud, "[dbo].[ACD_BRANCH] B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH BD ON(B.BRANCHNO = BD.BRANCHNO)", "B.BRANCHNO", "B.LONGNAME", "B.BRANCHNO >0 AND BD.DEGREENO="+ DegreeNo, "B.LONGNAME");

            //ddlBranchStud.SelectedValue = branchno;
            //// ddlDeptCourse.Items.Insert(0, new ListItem("Please Select", "0"));
            //ddlBranchStud.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility.PopulateBranchDDL-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void PopulateSchemeDDL()
    {
        try
        {
            int DegreeNo = int.Parse(ddlDegreeStud.SelectedItem.Value);
            int BranchNo = int.Parse(ddlBranchStud.SelectedItem.Value);
            //Fill Degree
            objCommon.FillDropDownList(ddlSchemeStud, "[dbo].[ACD_SCHEME]", "SCHEMENO", "SCHEMENAME", "SCHEMENO >0 AND DEGREENO=" + DegreeNo + "AND BRANCHNO=" + BranchNo, "SCHEMENAME");

            //ddlSchemeStud.SelectedValue = schemeno;
            //// ddlDeptCourse.Items.Insert(0, new ListItem("Please Select", "0"));
            //ddlSchemeStud.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility.PopulateSchemeDDL-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnExportEmployees_Click(object sender, EventArgs e)
    {
        bool CompleteRequest = false;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_DEPTNO", int.Parse(ddlDepartmentEmp.SelectedItem.Value));
            DataTable EmployeesTable = objSQLHelper.ExecuteDataSetSP("SP_EMPLOYEE_DATA_EXPORT", objParams).Tables[0];
            string FileName = ddlDepartmentEmp.SelectedItem.Text.Replace(' ','_');
            GridView GVStatus = new GridView();
            string ContentType = string.Empty;
            if (EmployeesTable.Rows.Count > 0)
            {
                GVStatus.DataSource = EmployeesTable;
                GVStatus.DataBind();
                string attachment = "attachment;filename=" + FileName + ".xlsx";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVStatus.RenderControl(htw);
                Response.Write(sw.ToString());
                CompleteRequest = true;
            }
            else
            {
                GVStatus.DataSource = null;
                GVStatus.DataBind();
                objCommon.DisplayMessage("No record found...", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility_btnExportEmployees_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        if (CompleteRequest)
            Response.End();
    }
    private void PopulateDeptDDLForEmp()
    {
        try
        {
            //Fill Department
            if (Session["usertype"].ToString() != "1")
                objCommon.FillDropDownList(ddlDepartmentEmp, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO >0 AND DEPTNO =" + Session["userdeptno"].ToString(), "DEPTNAME");
            else
                objCommon.FillDropDownList(ddlDepartmentEmp, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO >0", "DEPTNAME");

            // ddlDeptCourse.SelectedValue = deptno;
            //// ddlDeptCourse.Items.Insert(0, new ListItem("Please Select", "0"));
            // ddlDeptCourse.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility.PopulateDeptDDLForEmp-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDegreeStud_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(ddlDegreeStud.SelectedItem.Value) != 0)
        {
            ddlBranchStud.Items.Clear();
            PopulateBranchDDL();
        }
        else
        {
            ddlBranchStud.Items.Clear();
            ddlBranchStud.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlSchemeStud.Items.Clear();
            ddlSchemeStud.Items.Insert(0, new ListItem("Please Select", "0"));
        }
    }
    protected void ddlBranchStud_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(ddlBranchStud.SelectedItem.Value) != 0)
        {
            ddlSchemeStud.Items.Clear();
            PopulateSchemeDDL();
        }else
        {
            ddlSchemeStud.Items.Clear();
            ddlSchemeStud.Items.Insert(0, new ListItem("Please Select", "0"));
        }
    }
    protected void btnCancelStud_Click(object sender, EventArgs e)
    {
        ddlDegreeStud.SelectedIndex = 0;
        ddlAdmBatchStud.SelectedIndex = 0;
        ddlSemesterStud.SelectedIndex = 0;
        ddlSectionStud.SelectedIndex = 0;
        ddlBranchStud.Items.Clear();
        ddlBranchStud.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlSchemeStud.Items.Clear();
        ddlSchemeStud.Items.Insert(0, new ListItem("Please Select", "0"));
    }
    protected void btnCancelEmployees_Click(object sender, EventArgs e)
    {
        ddlDepartmentEmp.SelectedIndex = 0;
    }
    protected void btnCancelCourse_Click(object sender, EventArgs e)
    {
        ddlDegreeStud.SelectedIndex = 0;
    }

    #region Course Registration
    private void PopulateSessionRegDDL()
    {
        try
        {
            // Added on 25-02-2020
            objCommon.FillDropDownList(ddlSessionReg, "[ACD_SESSION_MASTER]", "SESSIONNO", "SESSION_NAME", "SESSIONNO >0 AND EXAMTYPE =1 AND ODD_EVEN IN (1,2)", "SESSIONNO DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility.PopulateSessionRegDDL-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void PopulateDegreeRegDDL()
    {
        try
        {
            // Added on 25-02-2020
            objCommon.FillDropDownList(ddlDegreeReg, "[dbo].[ACD_DEGREE]", "DEGREENO", "DEGREENAME", "DEGREENO >0", "DEGREENAME");
            ddlDegreeReg.SelectedValue = degreeno;
            ddlDegreeReg.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility.PopulateDegreeRegDDL-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void PopulateBranchRegDDL()
    {
        try
        {
            // Added on 25-02-2020
            int DegreeNo = int.Parse(ddlDegreeReg.SelectedItem.Value);
            //objCommon.FillDropDownList(ddlBranchReg, "[dbo].[ACD_BRANCH]", "BRANCHNO", "LONGNAME", "DEGREENO=" + DegreeNo, "LONGNAME");
            objCommon.FillDropDownList(ddlBranchReg, "[dbo].[ACD_BRANCH] B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH BD ON(B.BRANCHNO = BD.BRANCHNO)", "B.BRANCHNO", "B.LONGNAME", "B.BRANCHNO >0 AND BD.DEGREENO=" + DegreeNo, "B.LONGNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility.PopulateBranchRegDDL-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void PopulateSchemeRegDDL()
    {
        try
        {
            int DegreeNo = int.Parse(ddlDegreeReg.SelectedItem.Value);
            int BranchNo = int.Parse(ddlBranchReg.SelectedItem.Value);
            //Fill Degree
            objCommon.FillDropDownList(ddlSchemeReg, "[dbo].[ACD_SCHEME]", "SCHEMENO", "SCHEMENAME", "SCHEMENO >0 AND DEGREENO=" + DegreeNo + "AND BRANCHNO=" + BranchNo, "SCHEMENAME");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility.PopulateSchemeRegDDL-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDegreeReg_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(ddlDegreeReg.SelectedItem.Value) != 0)
        {
            ddlBranchReg.Items.Clear();
            PopulateBranchRegDDL();
        }
        else
        {
            ddlBranchReg.Items.Clear();
            ddlBranchReg.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlSchemeReg.Items.Clear();
            ddlSchemeReg.Items.Insert(0, new ListItem("Please Select", "0"));
        }
    }
    protected void ddlBranchReg_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(ddlBranchReg.SelectedItem.Value) != 0)
        {
            ddlSchemeReg.Items.Clear();
            PopulateSchemeRegDDL();
        }
        else
        {
            ddlSchemeReg.Items.Clear();
            ddlSchemeReg.Items.Insert(0, new ListItem("Please Select", "0"));
        }
    }
    private void PopulateSectionRegDDL()
    {
        try
        {
            objCommon.FillDropDownList(ddlSectionReg, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO >0", "SECTIONNAME");
           

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility.PopulateSectionDDL-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void PopulateSemesterRegDDL()
    {
        try
        {
            objCommon.FillDropDownList(ddlSemesterReg, "[dbo].[ACD_SEMESTER]", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO >0", "SEMESTERNAME");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility.PopulateSemesterRegDDL-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnExportRegistration_Click(object sender, EventArgs e)
    {
        bool CompleteRequest = false;
        try
        {
            int SessionId = int.Parse(ddlSessionReg.SelectedItem.Value);
            int schemeId = int.Parse(ddlSchemeReg.SelectedItem.Value);
            int SemesterId = int.Parse(ddlSemesterReg.SelectedItem.Value);
            int SectionId = int.Parse(ddlSectionReg.SelectedItem.Value);

            string BranchName = ddlBranchReg.SelectedItem.Text.Replace(' ', '_');
            string SchemeName = ddlSchemeReg.SelectedItem.Text.Replace(' ', '_');
            string SemesterName = ddlSemesterReg.SelectedItem.Text.Replace(' ', '_');
            string SectionName = ddlSectionReg.SelectedItem.Text.Replace(' ', '_');
            string FileName = string.Empty;
            if (Convert.ToInt32(SectionId) != 0)
                FileName = BranchName.Replace(' ', '_') + "-" + SchemeName.Replace(' ', '_') + "-" + SemesterName.Replace(' ', '_') + "-" + SectionName.Replace(' ', '_');
            else
                FileName = BranchName.Replace(' ', '_') + "-" + SchemeName.Replace(' ', '_') + "-" + SemesterName.Replace(' ', '_');

            SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
            SqlParameter[] objParams = new SqlParameter[4];
            objParams[0] = new SqlParameter("@SESSIONNO", SessionId);
            objParams[1] = new SqlParameter("@SCHEMENO", schemeId);
            objParams[2] = new SqlParameter("@SEMESTERNO", SemesterId);
            objParams[3] = new SqlParameter("@SECTIONNO", SectionId);
            DataTable CourseRegistrationTable = objSQLHelper.ExecuteDataSetSP("[spExportCourseRegistrationData]", objParams).Tables[0];
            GridView GVStatus = new GridView();
            string ContentType = string.Empty;
            if (CourseRegistrationTable != null)
            {
                if (CourseRegistrationTable.Rows.Count > 0)
                {
                    GVStatus.DataSource = CourseRegistrationTable;
                    GVStatus.DataBind();
                    string attachment = "attachment;filename=CourseReg_"+ FileName + ".xlsx";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GVStatus.RenderControl(htw);
                    Response.Write(sw.ToString());
                    CompleteRequest = true;
                }
                else
                {
                    GVStatus.DataSource = null;
                    GVStatus.DataBind();
                    objCommon.DisplayMessage("No record found...", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExportsUtility_btnExportRegistration_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        if (CompleteRequest)
            Response.End();

    }
    protected void btnClearReg_Click(object sender, EventArgs e)
    {
        ddlDegreeReg.SelectedIndex = 0;
        ddlSessionReg.SelectedIndex = 0;
        ddlSemesterReg.SelectedIndex = 0;
        ddlSectionReg.SelectedIndex = 0;
        ddlBranchReg.Items.Clear();
        ddlBranchReg.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlSchemeReg.Items.Clear();
        ddlSchemeReg.Items.Insert(0, new ListItem("Please Select", "0"));
    }
    #endregion Course Registration


}