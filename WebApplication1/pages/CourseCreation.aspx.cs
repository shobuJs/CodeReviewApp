using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using ClosedXML.Excel;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Text;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;


public partial class ACADEMIC_CourseCreation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objCourse = new CourseController();
    Course objc = new Course();
    CourseController objCC = new CourseController();
    SlotController objSC = new SlotController();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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

            //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            //scriptManager.RegisterPostBackControl(this.btnExportGridData);

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
                }
            }

            objCommon.SetLabelData("0");//for label
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
            ViewState["action"] = "add";
            FilldropDown();
            BindData();
        }
       
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CollegeMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CollegeMaster.aspx");
        }
    }

    private void FilldropDown()
    {
       // objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_NAME");
        objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and isnull(ACTIVE,0)=1", "COLLEGE_ID");

        objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "", "DEPTNAME");
        objCommon.FillDropDownList(ddlSubjectType, "ACD_SUBJECTTYPE", "SUBID", "SUBNAME", "", "SUBNAME");
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "", "SEMESTERNO");
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //ViewState["action"] = "add";

            if (hfdStat.Value == "true")
                objc.Active = 1;
            else
                objc.Active = 0;
            

            int ua_no = Convert.ToInt32(Session["userno"]);
            objc.Deptno = Convert.ToInt32(ddlDepartment.SelectedValue);
            objc.CCode = Convert.ToString(txtSubjectCode.Text);
            objc.CourseName = Convert.ToString(txtSubjectName.Text);
            objc.SubID = Convert.ToInt32(ddlSubjectType.SelectedValue);
            objc.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            if (!txtLecture.Text.Trim().Equals(string.Empty)) objc.Lecture = Convert.ToDecimal(txtLecture.Text);
            if (!txtTheory.Text.Trim().Equals(string.Empty)) objc.Theory = Convert.ToDecimal(txtTheory.Text.Trim());
            if (!txtPractical.Text.Trim().Equals(string.Empty)) objc.Practical = Convert.ToDecimal(txtPractical.Text.Trim());
            if (!txtCreadits.Text.Trim().Equals(string.Empty)) objc.Credits = Convert.ToDecimal(txtCreadits.Text.Trim());
            if (!txtCapacity.Text.Trim().Equals(string.Empty)) objc.Capacity = Convert.ToDecimal(txtCapacity.Text.Trim());
            decimal Lec_Unit = Convert.ToDecimal(txtLec.Text.Trim());
            decimal Lab_Unit = Convert.ToDecimal(txtLab.Text.Trim());

            if (ViewState["action"] != null)
            {
                
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //hdfAction.Value = ViewState["action"].ToString();
                    int chkexit = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "COUNT(1)", " CCODE ='" + txtSubjectCode.Text.Trim() + "'")); //"DEPTNO=" + ddlDepartment.SelectedValue + 
                    if (chkexit > 0)
                    {
                        objCommon.DisplayMessage(updCourseCreation, "Records Already Exist!", this.Page);
                        clear();
                        BindData();
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objCourse.AddCourseCreation(objc, ua_no, Lec_Unit, Lab_Unit);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            hdnEdit.Value = "0";
                            objCommon.DisplayMessage(this.updCourseCreation, "Record Saved Successfully!", this.Page);
                            //ViewState["action"] = "add";
                            clear();
                            BindData();
                        }
                        else
                        {

                            objCommon.DisplayMessage(this.updCourseCreation, "Error!!", this.Page);

                        }
                    }
                }
                else if (ViewState["action"].ToString().Equals("edit"))
                {
                    //hdfAction.Value = ViewState["action"].ToString();
                    int exist = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "count(*)", "COURSENO=" + Convert.ToInt32(ViewState["courseno"]) + " AND ISNULL(EXAM_REGISTERED,0)=1 AND ISNULL(CANCEL,0)=0"));
                    
                    if (exist > 0)
                    {
                        objCommon.DisplayMessage(this.updCourseCreation, "Module Already Offered!", this.Page);
                        clear();
                        BindData();
                        return;
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objCourse.UpdateCourseCreation(objc, ua_no, Convert.ToInt32(ViewState["courseno"]), Lec_Unit, Lab_Unit);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage(this.updCourseCreation, "Record Updated Successfully!", this.Page);
                            ViewState["action"] = "add";
                            clear();
                            BindData();
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updCourseCreation, "Error!!", this.Page);
                        }
                    }
                }

            }
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseCreation.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }


    private void BindData(int pageIndex = 0)
    {
        try
        {
            int pageSize = 1000;
            DataSet ds;
            TextBox txtSearch = (TextBox)lvCourseCreation.FindControl("txtSearch");
            string searchText = txtSearch != null ? txtSearch.Text : string.Empty;

            ds = objCourse.GetCourseCreation(Convert.ToInt32(ddlDepartment.SelectedValue), pageIndex, pageSize, searchText);

            DataTable newRecords = ds.Tables[0];
            DataTable existingRecords = ViewState["DYNAMIC_DATASET"] as DataTable;

            if (ds.Tables[0].Rows.Count > 0)
            {
                Panel1.Visible = true;
                lvCourseCreation.DataSource = ds;
                lvCourseCreation.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvCourseCreation);
                ViewState["DYNAMIC_DATASET"] = ds.Tables[0];

                UpdateLabels(pageIndex * pageSize, GetPageSize(), ds.Tables[0].Rows.Count);
            }
            else
            {
                Panel1.Visible = true;

                objCommon.DisplayMessage(this.updCourseCreation, "Records Not Found!", this.Page);
            }
        }
        catch (Exception ex)
        {
            // Log or handle the exception
        }
    }

    private int GetPageSize()
    {
        Control Tfoot1 = lvCourseCreation.FindControl("Tfoot1");
        DataPager datapager2 = Tfoot1.FindControl("datapager2") as DataPager;

        if (datapager2 != null)
        {
            return datapager2.MaximumRows;
        }

        return 10;
    }

    protected void lvCourseCreation_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        (lvCourseCreation.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);

        int pageSize = 1000;
        int newPageIndex = e.StartRowIndex / pageSize + 1;
        BindData(newPageIndex - 1); // Adjusted to zero-based index
        //DataTable paginatedData = dt.AsEnumerable()
        //                                      .Skip(pageIndex * pageSize)
        //                                      .Take(pageSize)
        //                                      .CopyToDataTable();

        //BindDataToGrid(paginatedData, lvCourseCreation);
    }

    private void UpdateLabels(int startRowIndex, int maximumRows, int totalRecords)
    {
        Control Tfoot1 = lvCourseCreation.FindControl("Tfoot1");
        DataPager datapager2 = Tfoot1.FindControl("datapager2") as DataPager;
        Label lblStart = datapager2.Controls[0].FindControl("lblStart") as Label;
        Label lblEnd = datapager2.Controls[0].FindControl("lblEnd") as Label;
        Label lblTotalRecord = datapager2.Controls[0].FindControl("lblTotalCount") as Label;

        int currentPageIndex = startRowIndex / maximumRows + 1;

        int startRecord = startRowIndex + 1; // Calculate based on the startRowIndex
        int endRecord = Math.Min(startRowIndex + maximumRows, totalRecords); // Calculate based on the remaining records


        lblStart.Text = startRecord.ToString();
        lblEnd.Text = endRecord.ToString();
        lblTotalRecord.Text = totalRecords.ToString();
    }


    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        BindData(0);
    }


    private void clear()
    {

        ddlSubjectType.SelectedIndex = 0;
        txtSubjectName.Text = string.Empty;
        txtSubjectCode.Text = string.Empty;
        txtLecture.Text = string.Empty;
        txtCapacity.Text = string.Empty;
        txtTheory.Text = string.Empty;
        txtPractical.Text = string.Empty;
        txtCreadits.Text = string.Empty;
        lvCourseCreation.DataSource = null;
        lvCourseCreation.DataBind();
        ddlSemester.SelectedValue = "0";
        txtLab.Text = string.Empty;
        txtLec.Text = string.Empty;
        ddlDepartment.SelectedIndex = 0;
        ddlFaculty.SelectedIndex = 0;
        hdnEdit.Value = "0";
        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
        Response.Redirect(Request.Url.ToString());
    }


    private void ShowDetails(int courseno)
    {

        try
        {

            DataSet ds = objCommon.FillDropDown("ACD_COURSE", "isnull(DEPTNO,0) as DEPTNO,courseno,ACTIVE,SEMESTERNO,CCODE,COURSE_NAME,SUBID,LECTURE,THEORY,PRACTICAL,CREDITS,CAPACITY,LEC_UNIT,LAB_UNIT", "", "COURSENO=" + courseno, "COURSENO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["DEPTNO"].ToString();
                txtSubjectCode.Text = ds.Tables[0].Rows[0]["CCODE"].ToString();
                txtSubjectName.Text = ds.Tables[0].Rows[0]["COURSE_NAME"].ToString();
                ddlSubjectType.SelectedValue = ds.Tables[0].Rows[0]["SUBID"].ToString();
                txtLecture.Text = ds.Tables[0].Rows[0]["LECTURE"].ToString();
                txtTheory.Text = ds.Tables[0].Rows[0]["THEORY"].ToString();
                txtPractical.Text = ds.Tables[0].Rows[0]["PRACTICAL"].ToString();
                txtCreadits.Text = ds.Tables[0].Rows[0]["CREDITS"].ToString();
                txtCapacity.Text = ds.Tables[0].Rows[0]["CAPACITY"].ToString();
                txtLec.Text = ds.Tables[0].Rows[0]["LEC_UNIT"].ToString();
                txtLab.Text = ds.Tables[0].Rows[0]["LAB_UNIT"].ToString();
                ddlSemester.SelectedValue = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                if (ds.Tables[0].Rows[0]["ACTIVE"].ToString().Trim() == "1")
                {
                    //chkActive.Checked = true;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(true);", true);
                }
                else
                {
                    //chkActive.Checked = false;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(false);", true);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseCreation.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["action"] = "edit";
            int courseno = int.Parse(btnEdit.CommandArgument);
            ViewState["courseno"] = courseno;         
            HiddenField1.Value = int.Parse(btnEdit.CommandArgument).ToString();
            hdnEdit.Value = "1";
            ShowDetails(courseno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_CollegeMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourseCreation.DataSource = null;
        lvCourseCreation.DataBind();
        if (ddlFaculty.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEPT CD ON(D.DEPTNO=CD.DEPTNO)", " DISTINCT D.DEPTNO", "D.DEPTNAME", "D.DEPTNO>0 AND CD.COLLEGE_ID = " + ddlFaculty.SelectedValue, "D.DEPTNAME");

        }
        else
        {
            //ddlDepartment.SelectedIndex = 0;
        }
    }
    //protected void btnExport_Click(object sender, EventArgs e)
    //{
    //    DataSet ds = objCC.RetrieveCourseMasterDataForExcel();

    //    ds.Tables[0].TableName = "Course Data Format";
    //    ds.Tables[1].TableName = "Subject Master";
    //    ds.Tables[2].TableName = "Semester Master";
    //    ds.Tables[3].TableName = "Scheme Master";
    //    ds.Tables[4].TableName = "BOS_Department Master";
    //    ds.Tables[5].TableName = "Elective Group";

    //    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0 && ds.Tables[2].Rows.Count > 0 && ds.Tables[3].Rows.Count > 0 && ds.Tables[4].Rows.Count > 0 && ds.Tables[5].Rows.Count > 0)
    //    {
    //        using (XLWorkbook wb = new XLWorkbook())
    //        {
    //            foreach (System.Data.DataTable dt in ds.Tables)
    //            {
    //                //Add System.Data.DataTable as Worksheet.
    //                wb.Worksheets.Add(dt);
    //            }

    //            //Export the Excel file.
    //            Response.Clear();
    //            Response.Buffer = true;
    //            Response.Charset = "";
    //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //            Response.AddHeader("content-disposition", "attachment;filename=PreFormat_For_UploadCourseData.xlsx");
    //            using (MemoryStream MyMemoryStream = new MemoryStream())
    //            {
    //                wb.SaveAs(MyMemoryStream);
    //                MyMemoryStream.WriteTo(Response.OutputStream);
    //                Response.Flush();
    //                Response.End();
    //            }
    //        }
    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage(this.updpnlImportData, "Please Define All Masters!!", this.Page);
    //    }
    //}
    protected void btnUploadexcel_Click(object sender, EventArgs e)
    {
        try
        {
            Uploaddata();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendanceReportByFaculty.btnSubjectwise_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void Uploaddata()
    {
        try
        {
            if (FUFile.HasFile)
            {
                string FileName = Path.GetFileName(FUFile.PostedFile.FileName);
                string Extension = Path.GetExtension(FUFile.PostedFile.FileName);
                if (Extension.Equals(".xls") || Extension.Equals(".xlsx"))
                {
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                    string FilePath = Server.MapPath(FolderPath + FileName);
                    FUFile.SaveAs(FilePath);
                    ExcelToDatabase(FilePath, Extension, "yes");
                }
                else
                {
                    objCommon.DisplayMessage(updpnlImportData, "Only .xls or .xlsx extention is allowed", this.Page);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updpnlImportData, "Please select the Excel File to Upload", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EnquiryGeneration.Uploaddata()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ExcelToDatabase(string FilePath, string Extension, string isHDR)
    {
        CourseController objCC = new CourseController();
        Course objC = new Course();
        try
        {
            CustomStatus cs = new CustomStatus();
            string conStr = "";

            switch (Extension)
            {
                //case ".xls": //Excel 97-03
                //    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                //    break;
                //case ".xlsx": //Excel 07
                //    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                //    break;
                case ".xls": //Excel 97-03
                    conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0'";
                    break;
                case ".xlsx": //Excel 07
                    conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0'";
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);

            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;
            //Get the name of First Sheet

            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[1]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);

            //Bind Excel to GridView
            DataSet ds = new DataSet();
            oda.Fill(ds);

            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "isnull(SUBJECT_NAME,'')<>''";
            DataTable dtNew = dv1.ToTable();

            lvStudData.DataSource = dtNew; // ds.Tables[0]; /// dSet.Tables[0].DefaultView.RowFilter = "Frequency like '%30%')"; ;
            lvStudData.DataBind();
            int i = 0;

            for (i = 0; i < dtNew.Rows.Count; i++)
            //for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            //foreach (DataRow dr in dt.Rows)
            {

                DataRow row = dtNew.Rows[i];//ds.Tables[0].Rows[i];
                object Name = row[0];
                if (Name != null && !String.IsNullOrEmpty(Name.ToString().Trim()))
                {

                    if (!(dtNew.Rows[i]["SUBJECT_NAME"]).ToString().Equals(string.Empty))
                    {
                        objC.CourseName = (dtNew.Rows[i]["SUBJECT_NAME"]).ToString();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Please enter Course Name at Row no. " + (i + 1), this.Page);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
                        return;
                    }
                    if (!(dtNew.Rows[i]["SUBJECT_CODE"]).ToString().Equals(string.Empty))
                    {
                        objC.CCode = (dtNew.Rows[i]["SUBJECT_CODE"]).ToString();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Please enter Course Code at Row no. " + (i + 1), this.Page);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
                        return;
                    }
                    if (!(dtNew.Rows[i]["LECTURE_HRS"]).ToString().Equals(string.Empty))
                    {
                        objC.Lecture = Convert.ToDecimal((dtNew.Rows[i]["LECTURE_HRS"]).ToString());
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Please enter Credits at Row no. " + (i + 1), this.Page);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
                        return;
                    }

                    if (!(dtNew.Rows[i]["LAB_HRS"]).ToString().Equals(string.Empty))
                    {
                        objC.Lab_Lecture = Convert.ToDecimal((dtNew.Rows[i]["LAB_HRS"]).ToString());
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Please enter Credits at Row no. " + (i + 1), this.Page);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
                        return;
                    }

                    if (!(dtNew.Rows[i]["UNITS"]).ToString().Equals(string.Empty))
                    {
                        objC.Credits = Convert.ToDecimal((dtNew.Rows[i]["UNITS"]).ToString());
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Please enter Credits at Row no. " + (i + 1), this.Page);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
                        return;
                    }

                    if (dtNew.Rows[i]["SUBJECTTYPE"].ToString() == string.Empty)
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Please Enter Subject Type at Row no. " + (i + 1), this.Page);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
                        return;
                    }
                    else
                    {
                        string subid = objCommon.LookUp("ACD_SUBJECTTYPE", "COUNT(1)", "SUBNAME='" + dtNew.Rows[i]["SUBJECTTYPE"].ToString() + "'");

                        if (Convert.ToInt32(subid) > 0)
                        {
                            objC.SubID = (objCommon.LookUp("ACD_SUBJECTTYPE", "SUBID", "SUBNAME ='" + dtNew.Rows[i]["SUBJECTTYPE"].ToString() + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_SUBJECTTYPE", "SUBID", "SUBNAME ='" + dtNew.Rows[i]["SUBJECTTYPE"].ToString() + "'"));
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlImportData, "Subject Type not found in ERP Master at Row no. " + (i + 1), this.Page);
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
                            return;
                        }

                    }

                    //if (dtNew.Rows[i]["SEMESTER"].ToString() == string.Empty)
                    //{
                    //    objCommon.DisplayMessage(updpnlImportData, "Please Enter Semester at Row no. " + (i + 1), this.Page);
                    //    return;
                    //}
                    //else
                    //{
                    //    string semesterno = objCommon.LookUp("acd_semester", "COUNT(1)", "SEMESTERNAME='" + dtNew.Rows[i]["SEMESTER"].ToString() + "'");

                    //    if (Convert.ToInt32(semesterno) > 0)
                    //    {
                    //        objC.SemesterNo = Convert.ToInt32((objCommon.LookUp("acd_semester", "SemesterNo", "SEMESTERNAME ='" + dtNew.Rows[i]["SEMESTER"].ToString() + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("acd_semester", "SemesterNo", "SEMESTERNAME ='" + dtNew.Rows[i]["SEMESTER"].ToString() + "'")));
                    //    }
                    //    else
                    //    {
                    //        objCommon.DisplayMessage(updpnlImportData, "Semester not found in ERP Master at Row no. " + (i + 1), this.Page);
                    //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
                    //        return;
                    //    }
                    //}
                    if (dtNew.Rows[i]["BOS_DEPT"].ToString() == string.Empty)
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Please Enter BOS_DEPT at Row no. " + (i + 1), this.Page);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
                        return;
                    }
                    else
                    {
                        string deptno = objCommon.LookUp("ACD_DEPARTMENT", "COUNT(1)", "DEPTNAME='" + dtNew.Rows[i]["BOS_DEPT"].ToString() + "'");

                        if (Convert.ToInt32(deptno) > 0)
                        {
                            objC.Deptno = Convert.ToInt32((objCommon.LookUp("ACD_DEPARTMENT", "Deptno", "DEPTNAME ='" + dtNew.Rows[i]["BOS_DEPT"].ToString() + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_DEPARTMENT", "Deptno", "DEPTNAME ='" + dtNew.Rows[i]["BOS_DEPT"].ToString() + "'")));

                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlImportData, "BOS_DEPT not found in ERP Master at Row no. " + (i + 1), this.Page);
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
                            return;
                        }

                    }

                    
                    //objC.BatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
                    cs = (CustomStatus)objCC.SaveExcelSheetCourseDataInDataBase(objC);
                    connExcel.Close();
                }

            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                // BindListView();
                objCommon.DisplayMessage(updpnlImportData, "Excel Sheet Uploaded Successfully!!", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
                return;
            }
            else
            {
                //BindListView();
                objCommon.DisplayMessage(updpnlImportData, "Excel Sheet Updated Successfully!!", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
                return;
            }          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.DisplayMessage(updpnlImportData, "Data not available in ERP Master", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);

                return;
            }
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataSet ds = objCC.RetrieveCourseMasterDataForExcel();

        ds.Tables[0].TableName = "Course Data Format";
        ds.Tables[1].TableName = "Subject Master";
        ds.Tables[2].TableName = "Semester Master";
        ds.Tables[3].TableName = "Scheme Master";
        ds.Tables[4].TableName = "BOS_Department Master";
        ds.Tables[5].TableName = "Elective Group";

        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0 && ds.Tables[2].Rows.Count > 0 && ds.Tables[3].Rows.Count > 0 && ds.Tables[4].Rows.Count > 0 && ds.Tables[5].Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    wb.Worksheets.Add(dt);
                }

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=PreFormat_For_UploadCourseData.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        else
        {
            objCommon.DisplayMessage(this.updpnlImportData, "Please Define All Masters!!", this.Page);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
        }
    }

    protected void btnCancelUpload_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    } 
    //protected void btnExportGridData_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        //dt = ds.Tables[0];
    //        dt = (DataTable)ViewState["DYNAMIC_DATASET"];
    //        if (dt.Rows.Count > 0)
    //        {
    //            string filename = "SubjectCreation.xls";
    //            System.IO.StringWriter tw = new System.IO.StringWriter();
    //            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
    //            DataGrid dgGrid = new DataGrid();
    //            dgGrid.DataSource = dt;
    //            dgGrid.DataBind();

    //            //Get the HTML for the control.
    //            dgGrid.RenderControl(hw);
    //            //Write the HTML back to the browser.
    //            //Response.ContentType = application/vnd.ms-excel;
    //            Response.ContentType = "application/vnd.ms-excel";
    //            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
    //            this.EnableViewState = false;
    //            Response.Write(tw.ToString());
    //            Response.End();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }      var columnsToRemove = new List<string> { "PAGENUMBER", "COURSENO", "DEPTNO", "SEMESTERNO", "SEMESTERNAME", "TOTAL_RECORDS" }; // Add column names to remove
    //}
    protected void btnExportGridData_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["DYNAMIC_DATASET"];

        if (dt.Rows.Count > 0)
        {
            string filename = "SubjectCreation.xls";
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

            hw.Write("<table border='1' cellpadding='5' cellspacing='0'>");

            hw.Write("<tr>");

            var columnNamesFromTable = hdnColumnNames.Value.Split(',');

            int totalColumns = columnNamesFromTable.Length;

            // Calculate the index of the last third column
            int lastThirdColumnIndex = totalColumns - (totalColumns / 2);

            // Add the "SrNo" column at the beginning
            hw.Write("<th>SrNo</th>");

            for (int i = 0; i < columnNamesFromTable.Length; i++)
            {
                string columnName = columnNamesFromTable[i];

                if (columnName.Equals("Edit", StringComparison.OrdinalIgnoreCase) || columnName.Equals("SrNo", StringComparison.OrdinalIgnoreCase) || columnName.Equals("Active", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                bool isDepartmentColumn = columnName.Equals("Department", StringComparison.OrdinalIgnoreCase);

                if (isDepartmentColumn && i < lastThirdColumnIndex)
                {
                    continue;
                }

                // Check if the column should be excluded
                if (!ShouldExcludeColumn(columnName))
                {
                    hw.Write("<th>{0}</th>", columnName);
                }
            }

            // Add the "Active" column at the end
            hw.Write("<th>Active</th>");

            hw.Write("</tr>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                hw.Write("<tr>");

                // Include SrNo data
                hw.Write("<td>{0}</td>", i + 1);

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string columnName = dt.Columns[j].ColumnName;

                    // Check if the column should be excluded
                    if (!ShouldExcludeColumn(columnName))
                    {
                        // Include data in columns based on column name
                        hw.Write("<td>{0}</td>", dt.Rows[i][columnName]);
                    }
                }
                hw.Write("</tr>");
            }

            hw.Write("</table>");

            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            this.EnableViewState = false;
            Response.Write(tw.ToString());
            Response.End();
        }
    }

    private bool ShouldExcludeColumn(string columnName)
    {
        var columnsToRemove = new List<string> { "PAGENUMBER", "COURSENO", "DEPTNO","THEORY", "CAPACITY", "SEMESTERNO", "SEMESTERNAME", "TOTAL_RECORDS" };
        return columnsToRemove.Contains(columnName);
    }


}

