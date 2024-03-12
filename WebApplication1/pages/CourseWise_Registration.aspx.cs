//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : STUDENT COURSE REGISTRATION REPORT                                   
// CREATION DATE : 22-AUG-2011                                                       
// CREATED BY    :                                                    
// MODIFIED DATE : 19-FEB-2024
// MODIFIED BY   : Nehal Nawkhare                                                                    
// MODIFIED DESC :                                                                      
//======================================================================================

using ClosedXML.Excel;
using IITMS;
using IITMS.UAIMS;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CourseWise_Registration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    //ConnectionString
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                Validation();

                if (Request.QueryString["pageno"] != null)
                {
                    objCommon.SetHeaderLabelData(Request.QueryString["pageno"].ToString());
                }
                PopulateDropDown();
                objCommon.SetLabelData("0");


            }
            divMsg.InnerHtml = string.Empty;
        }
        //Blank Div
        divMsg.InnerHtml = string.Empty;
    }

    #region PrivateMethods
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                //Response.Redirect("~/notauthorized.aspx?page=DecodingGeneration.aspx");
                Response.Redirect("~/notauthorized.aspx?page=" + Path.GetFileName(this.Page.AppRelativeVirtualPath));
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            //Response.Redirect("~/notauthorized.aspx?page=DecodingGeneration.aspx");
            Response.Redirect("~/notauthorized.aspx?page=" + Path.GetFileName(this.Page.AppRelativeVirtualPath));
        }
    }

    private void PopulateDropDown()
    {
        try
        {

            objCommon.FillDropDownList(ddlReport, "ACD_REPORT_TYPE", "RPT_NO", "REPORT_NAME", "ISNULL(STATUS,0)=1 AND PAGE_NAME='" + "CourseWise_Registration.aspx" + "'", "SQ_NO ASC");
            ddlReport.Focus();

            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0", "SESSIONNO DESC");
            ddlSession.Focus();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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
            if (ddlDegree.SelectedValue.ToString() == "0")
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(0) + ",@P_BRANCHNO=" + Convert.ToInt32(0) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);
            else
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[0]) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[1]) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowStudentListReport(string reportTitle, string rptFileName)
    {
        try
        {
            //Check record found or not

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListView()
    {
        try
        {

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ResetDropDown(DropDownList ddl)
    {
        ddl.Items.Clear();
        ddl.Items.Add(new ListItem("Please Select", "0"));
    }
    #endregion

    #region SelectedIndexChanged

    protected void ddlCollegeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindListView();
        try
        {
            if (ddlCollegeName.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO = A.DEGREENO) INNER JOIN ACD_BRANCH B ON CD.BRANCHNO=B.BRANCHNO", "DISTINCT (CONVERT(NVARCHAR(5),CD.DEGREENO) + ','+ CONVERT(NVARCHAR(5),CD.BRANCHNO))", "A.DEGREENAME + ','+ B.LONGNAME", "CD.COLLEGE_ID =" + ddlCollegeName.SelectedValue + "AND CD.COLLEGE_ID > 0", "");
                ddlDegree.Focus();
            }
            else
            {
                ResetDropDown(ddlDegree);
            }

            ResetDropDown(ddlSemester);
            ResetDropDown(ddlSubjectType);
            ResetDropDown(ddlCourse);
            ddlSection.Items.Clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.ddlCollegeName_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlSession.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlCollegeName, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                ddlCollegeName.Focus();

            }
            else
            {
                ResetDropDown(ddlCollegeName);

            }
            ResetDropDown(ddlDegree);
            ResetDropDown(ddlSemester);
            ResetDropDown(ddlSubjectType);
            ResetDropDown(ddlCourse);
            ddlSection.Items.Clear();

        }
        catch (Exception ex)
        {

        }
    }


    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // fill branch according degree selection
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.COLLEGE_ID = " + ddlCollegeName.SelectedValue + " AND SR.DEGREENO=" + ddlDegree.SelectedValue.Split(',')[0] + " AND SR.BRANCHNO=" + ddlDegree.SelectedValue.Split(',')[1] + "", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0

                ViewState["DEGREE_NO"] = ddlDegree.SelectedValue.Split(',')[0];
                ViewState["BRANCH_NO"] = ddlDegree.SelectedValue.Split(',')[1];

            }
            else
            {
                ResetDropDown(ddlSemester);
            }
            ResetDropDown(ddlSubjectType);
            ResetDropDown(ddlCourse);
            ddlSection.Items.Clear();

        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSemester.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSubjectType, "ACD_STUDENT_RESULT C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO >0", "C.SUBID");
                ddlSubjectType.Focus();
            }
            else
            {
                ResetDropDown(ddlSubjectType);
            }
            ResetDropDown(ddlCourse);
            ddlSection.Items.Clear();
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlSubjectType_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            if (ddlSemester.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", " SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SR.COLLEGE_ID = " + ddlCollegeName.SelectedValue + " AND SR.DEGREENO=" + ddlDegree.SelectedValue.Split(',')[0] + "AND ISNULL(SR.CANCEL,0)=0 AND ISNULL(SR.EXAM_REGISTERED,0)=1 AND SR.BRANCHNO=" + ddlDegree.SelectedValue.Split(',')[1], "COURSE_NAME");
                ddlCourse.Focus();
            }
            else
            {
                ResetDropDown(ddlCourse);
                ddlSection.Items.Clear();
            }

            //ddlSection.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlSection.Items.Clear();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.ddlSubjectType_SelectedIndexChanged1-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }


    #endregion

    #region ClickEvents & Reports

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            BindListView();
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Refresh Page url
        Response.Redirect(Request.Url.ToString());
    }

    private void ShowCourseStudentCountReport(string reportTitle, string rptFileName, int type)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (ddlDegree.SelectedValue.ToString() == "0")
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(0) + ",@P_BRANCHNO=" + Convert.ToInt32(0) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);
            else
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[0]) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[1]) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            //Course Registered Student
            if (ddlReport.SelectedValue == "15")
            {
                ShowReport("Course_Wise_Registration_Report", "rptCourseWiseStudRegistration.rpt");
            }
            else if (ddlReport.SelectedValue == "16")
            {
                ShowCourseStudentCountReport("CoursewiseStudentCount", "rptCoursewiseStudentCount.rpt", 1);
            }
            else if (ddlReport.SelectedValue == "17")
            {
                ShowReport("StudentExamRegisterdReport", "rptRegistration_Details.rpt");
            }
            else if (ddlReport.SelectedValue == "3")
            {
                ShowStudentListReport("StudentListReport", "rptRegisteredStudentList.rpt");
            }
            else if (ddlReport.SelectedValue == "19")
            {
                ShowCourseRegistration("Course_Registration_List", "CourseRegistrationList.rpt");
            }
            //else if (ddlReport.SelectedValue == "5")
            //{
            //    ShowReport_NominalList("Nominal List", "rptRegistration_Details.rpt");
            //}
            else if (ddlReport.SelectedValue == "18")
            {
                int college = Convert.ToInt32(ddlCollegeName.SelectedValue);
                Credits_MaxStudent_Data(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[1]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[0]), college);
            }
            //else if (ddlReport.SelectedValue == "5")
            //{
            //    ShowCourseStudentCountReport("Coursewise_Student_Count(University)", "rptCoursewiseStudentCountAll.rpt", 2);
            //}
            //else if (ddlReport.SelectedValue == "6")
            //{
            //    ShowCourseRegistration("Course_Registration_List_With_Core_subject", "CourseRegistrationListCoreSubject.rpt");
            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Credits_MaxStudent_Data(int sessionno, int branchno, int semsterno, int degreeno, int college_id)
    {
        try
        {
            string SP_Name2 = "";
            int subid = 0; int courseno = 0;

            SP_Name2 = "PKG_ACD_GET_MAX_CREDITS_STUDENT_LIST";

            string SP_Parameters2 = "@P_SESSIONNO,@P_SEMESTERNO,@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO,@P_SUBID,@P_COURSENO";
            string Call_Values2 = "" + sessionno + "," + semsterno + "," + college_id + "," +
               degreeno + "," + branchno + "," + subid + "," + courseno + "";

            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            DataGrid dg = new DataGrid();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string attachment = "";


                attachment = "attachment; filename=" + "Max_Credits_Student_List.xls";


                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/" + "ms-excel";
                System.IO.StringWriter sw = new System.IO.StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                dg.DataSource = ds.Tables[0];
                dg.DataBind();
                dg.HeaderStyle.Font.Bold = true;
                dg.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "No Data Found for this Selection", this.Page);
                return;
            }

        }
        catch (Exception ex)
        {

        }
    }



    private void ShowCourseRegistration(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (ddlDegree.SelectedValue.ToString() == "0")
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(0) + ",@P_BRANCHNO=" + Convert.ToInt32(0) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);
            else
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[0]) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[1]) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void GenerateReport()
    {
        try
        {
            string ReportName = "";
            DataSet ds = new DataSet();
            if (ddlDegree.SelectedValue == "0")
            {
                ViewState["DEGREE_NO"] = "0";
                ViewState["BRANCH_NO"] = "0";
            }
            string SP_Name2 = "";
            ReportName = ddlReport.SelectedItem + ".xlsx";

            SP_Name2 = ViewState["PROCEDURE_NAME"].ToString();
            string SP_Parameters2 = "@P_SESSIONNO,@P_COLLEGE_ID,@P_SEMESTERNO,@P_DEGREENO,@P_BRANCHNO,@P_SUBID,@P_COURSENO";
            string Call_Values1 = "" + Convert.ToInt32(ddlSession.SelectedValue.ToString()) + "," +
               Convert.ToInt32(ddlCollegeName.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," +
                Convert.ToInt32(ViewState["DEGREE_NO"]) + "," + Convert.ToInt32(ViewState["BRANCH_NO"]) + "," + Convert.ToInt32(ddlSubjectType.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "";
            ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values1);
            DataGrid dg = new DataGrid();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string FileNa = ReportName.Remove(ReportName.Length - 5, 5);
                ds.Tables[0].TableName = FileNa.ToString();
                string status = string.Empty;
                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    if (dt.Rows.Count == 0)
                    {
                        status += dt.TableName + ",";

                    }
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    int tableIndex = 0;
                    foreach (System.Data.DataTable dt in ds.Tables)
                    {
                        tableIndex++;
                        var worksheet = wb.Worksheets.Add(dt, dt.TableName.ToString());
                        // Set the header row

                        // Set the data rows
                        if (dt == ds.Tables[0])
                        {
                            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                            {
                                worksheet.Cell(1, i + 1).Value = ds.Tables[0].Columns[i].ColumnName;
                                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                            }
                        }
                    }
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.AppendHeader("Content-Disposition", string.Format("attachment; filename*=UTF-8''{0}", Uri.EscapeDataString(ReportName.ToString())));
                    Response.ContentType = "application/" + "ms-excel";
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
                objCommon.DisplayMessage(this.Page, "No Data Found for this Selection", this.Page);
                return;
            }
        }
        catch { }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Session !!", this.Page);
                return;
            }
            if (ddlReport.SelectedValue != "0")
            {
                GenerateReport();
            }
            else
            {
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.btnExcel_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ExportinExcel(int type)
    {
        try
        {
            DataSet ds = new DataSet();
            if (type == 11)
            {
                string sp_name = ""; string sp_para = ""; string sp_call = "";
                sp_name = "PR_ENLISTMENT_GENDER_WISE_SUMMARY";
                sp_para = "@P_SESSIONNO,@P_COLLEGEID";
                sp_call = "" + Convert.ToInt32(ddlSession.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlCollegeName.SelectedValue.ToString());
                ds = objCommon.DynamicSPCall_Select(sp_name, sp_para, sp_call);
            }
            else if (type == 12)
            {
                string sp_name12 = "PR_ACAD_ATTRITION_RATE_REPORT";
                string sp_para12 = "@P_SESSIONNO";
                string sp_call12 = "" + Convert.ToInt32(ddlSession.SelectedValue.ToString());

                ds = objCommon.DynamicSPCall_Select(sp_name12, sp_para12, sp_call12);
            }
            else
            {
                if (type != 9)
                {
                    string SP_Name2 = "";



                    if (type == 1)
                        SP_Name2 = "PKG_COURSEWISE_STUDENT_COUNT_FOR_REPORT";
                    else if (type == 2)
                        SP_Name2 = "PKG_ACAD_STUD_CNT_SCRUTINY";
                    else if (type == 3)
                        SP_Name2 = "PKG_ACAD_REGISTRATION_REPORT";
                    else if (type == 4)
                        SP_Name2 = "PKG_ACD_COURSE_REGISTRATION_LIST";

                    string SP_Parameters2 = "@P_SESSIONNO,@P_SEMESTERNO,@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO,@P_SUBID,@P_COURSENO";
                    string Call_Values2 = "" + Convert.ToInt32(ddlSession.SelectedValue.ToString()) + "," +
                        Convert.ToInt32(ddlSemester.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlCollegeName.SelectedValue.ToString()) + "," +
                        Convert.ToInt32(ddlDegree.SelectedValue.ToString() == "0" ? 0 : Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[0])) + ","
                        + Convert.ToInt32(ddlDegree.SelectedValue.ToString() == "0" ? 0 : Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[1])) + "," +
                        +Convert.ToInt32(ddlSubjectType.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlCourse.SelectedValue.ToString()) + "," +
                             "";
                    ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
                }
                else
                {
                    string sp_name = ""; string sp_para = ""; string sp_call = "";

                    if (type == 9)
                    {
                        sp_name = "PR_SUBJECT_ENLISTMENT_REPORT_EXCEL_USA";
                        sp_para = "@P_COLLEGE_ID,@P_SESSIONNO";
                        sp_call = "" + Convert.ToInt32(ddlCollegeName.SelectedValue) + "," + Convert.ToInt32(ddlSession.SelectedValue.ToString());

                        ds = objCommon.DynamicSPCall_Select(sp_name, sp_para, sp_call);
                    }


                }

            }

            DataGrid dg = new DataGrid();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string attachment = "";

                if (type == 1)
                    attachment = "attachment; filename=" + "Course_Wise_Registration_Report.xls";
                else if (type == 2)
                    attachment = "attachment; filename=" + "CoursewiseStudentCount.xls";
                else if (type == 3)
                    attachment = "attachment; filename=" + "StudentExamRegisterdReport.xls";
                else if (type == 4)
                    attachment = "attachment; filename=" + "Course_Registration_List.xls";
                else if (type == 9)
                    attachment = "attachment; filename=" + "SUBJECT_ENLISTMENT_REPORT.xls";
                else if (type == 11)
                    attachment = "attachment; filename=" + "ENLISTMENT_SUMMARY_REPORT.xls";
                else if (type == 12)
                    attachment = "attachment; filename=" + "Attrition Rate Report.xls";

                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/" + "ms-excel";
                System.IO.StringWriter sw = new System.IO.StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                dg.DataSource = ds.Tables[0];
                dg.DataBind();
                dg.HeaderStyle.Font.Bold = true;
                dg.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "No Data Found for this Selection", this.Page);
                return;
            }

        }
        catch (Exception ex)
        {

        }
    }
    private void ShowReportinFormate(string exporttype, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlDegree.SelectedItem.Text + "_" + ddlSemester.SelectedItem.Text + "_" + ddlCourse.SelectedValue + ".xls"; // "_" + ddlSection.SelectedItem.Text 
            url += "&path=~,Reports,Academic," + rptFileName;
            if (ddlReport.SelectedValue == "15")
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_SECTIONNO=" + 0 + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",Username=" + Session["username"].ToString();
            else if (ddlReport.SelectedValue == "16")
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",@P_TYPE= 1,userName=" + Session["username"].ToString();
            else if (ddlReport.SelectedValue == "17")
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",username=" + Session["username"].ToString();
            else if (ddlReport.SelectedValue == "19")
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTER_NO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SECTIONNO=" + 0 + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",username=" + Session["username"].ToString();

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCoursewiseSubjectlist_Click1(object sender, EventArgs e)
    {
        try
        {
            ShowReportCourseWiseSubjectList("Course Wise Subject List Report", "rptCourseWiseSubjectList.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendanceReportByFaculty.btnSubjectwise_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //showing the report in pdf formate as per as  selection of report name  or file name.
    private void ShowReportCourseWiseSubjectList(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            if (ddlDegree.SelectedValue.ToString() == "0")
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(0) + ",@P_BRANCHNO=" + Convert.ToInt32(0) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);
            else
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[0]) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[1]) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.AttDetailsReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //showing the Nominal list report in pdf formate as per as  selection of report name  or file name.
    private void ShowReport_NominalList(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (ddlDegree.SelectedValue.ToString() == "0")
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(0) + ",@P_BRANCHNO=" + Convert.ToInt32(0) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);
            else
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[0]) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[1]) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.AttDetailsReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    #endregion

    #region ENROLMENT LIST REPORT PDF
    protected void btnPdfReport_Click(object sender, EventArgs e)
    {
        try
        {
            // Define parameters
            string collegeId = ddlCollegeName.SelectedValue;
            string sessionno = ddlSession.SelectedValue;
            string degreeno = ddlDegree.SelectedValue.ToString().Split(',')[0];
            string branchno = ddlDegree.SelectedValue.ToString().Split(',')[1];
            string semesterno = ddlSemester.SelectedValue;

            // Construct the URL with parameters
            string targetUrl = "~/Reports/EnrollmentReport.aspx?collegeId=" + Server.UrlEncode(collegeId) + "&sessionno=" + Server.UrlEncode(sessionno)+ "&degreeno=" + Server.UrlEncode(degreeno) + "&branchno=" + Server.UrlEncode(branchno) + "&semesterno=" + Server.UrlEncode(semesterno);

            // Open the URL in a new window using JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenWindow", "window.open('" + ResolveUrl(targetUrl) + "', '_blank');", true);

          //  Response.Redirect("~/Reports/EnrollmentReport.aspx");

            //this.ShowReportPDF("pdf", "ENROLLMENT_LIST_REPORT.rpt");
        }
        catch (Exception ex)
        {

        }

    }

    protected void BindSection()
    {
        try
        {
            string SP_Name1 = "PKG_BIND_SECTION_FOR_CLASS_LIST_REPORT";
            string SP_Parameters1 = "@P_COLLEGE_ID,@P_SESSIONNO,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO,@P_SUBJECT_TYPE,@P_COURSENO";
            string Call_Values1 = "" + Convert.ToInt32(ddlCollegeName.SelectedValue) + "," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlDegree.SelectedValue.ToString().Split(',')[0]) +
                "," + Convert.ToInt32(ddlDegree.SelectedValue.ToString().Split(',')[1]) +
                "," + ddlSemester.SelectedValue + "," + ddlSubjectType.SelectedValue + "," + ddlCourse.SelectedValue;

            DataSet que_out1 = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);

            ddlSection.Items.Clear();

            if (que_out1.Tables[0] != null && que_out1.Tables[0].Rows.Count > 0)
            {
                ddlSection.DataSource = que_out1;
                ddlSection.DataValueField = "SECTIONNO";
                ddlSection.DataTextField = "SECTIONNAME";
                ddlSection.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void ShowReportPDF(string reportTitle, string rptFileName)
    {
        try
        {
            string degreeeno = "0"; string branchno = "0"; string Sectionno = "0";
            if (ddlDegree.SelectedIndex > 0)
            {
                string[] program = ddlDegree.SelectedValue.ToString().Split(',');

                degreeeno = program[0];
                branchno = program[1];
            }
            string college_id = ddlCollegeName.SelectedValue;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (rptFileName.Equals("ClassListReport.rpt"))
            {
                foreach (ListItem items in ddlSection.Items)
                {
                    if (items.Selected == true)
                    {
                        Sectionno += Convert.ToString(items.Value) + '$';
                    }
                }

                if (Sectionno == string.Empty)
                    Sectionno = "0";
                else
                    Sectionno = Sectionno.TrimEnd('$');

                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGE_ID=" + college_id + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_BRANCHNO=" + branchno + ",@P_DEGREENO=" + degreeeno + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_SECTIONNO=" + Sectionno;
            }
            else
            {
                if (ddlReport.SelectedValue == "22")
                {
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + college_id;
                }
                else if (ddlReport.SelectedValue == "23")
                {
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + college_id + ",@P_BRANCHNO=" + branchno + ",@P_DEGREENO=" + degreeeno;
                }
                else
                {
                    url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGE_ID=" + college_id + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_BRANCHNO=" + branchno + ",@P_DEGREENO=" + degreeeno;
                }
            }
            divrpt.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divrpt.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divrpt.InnerHtml += " </script>";
        }

        catch (Exception ex)
        {
            throw;
        }
    }

    private void ClearContent()
    {
        ResetDropDown(ddlReport);
        ResetDropDown(ddlSession);
        ResetDropDown(ddlCollegeName);
        ResetDropDown(ddlDegree);
        ResetDropDown(ddlSemester);
        ResetDropDown(ddlSubjectType);
        ResetDropDown(ddlCourse);
        ddlSection.Items.Clear();
    }

    #endregion

    #region MandetaryValidation
    protected void ClearViewState()
    {
        ViewState["REPORT_NAME"] = 0;
        ViewState["PROCEDURE_NAME"] = 0;
        ViewState["SESSION"] = 0;
        ViewState["COLLEGE"] = 0;
        ViewState["PROGRAM"] = 0;
        ViewState["SEMESTER"] = 0;
        ViewState["STDUYTYPE"] = 0;
        ViewState["SUBJECT"] = 0;
        ViewState["DEGREE_NO"] = "0";
        ViewState["BRANCH_NO"] = "0";

        ddlSession.SelectedValue = "0";

        ddlSection.Items.Clear();

        ddlCollegeName.Items.Clear();
        ddlCollegeName.Items.Add("Please Select");
        ddlCollegeName.SelectedItem.Value = "0";
        ddlDegree.Items.Clear();
        ddlDegree.Items.Add("Please Select");
        ddlDegree.SelectedItem.Value = "0";
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add("Please Select");
        ddlCourse.SelectedItem.Value = "0";
        ddlSubjectType.Items.Clear();
        ddlSubjectType.Items.Add("Please Select");
        ddlSubjectType.SelectedItem.Value = "0";
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add("Please Select");
        ddlSemester.SelectedItem.Value = "0";
    }

    protected void Validation()
    
    {
        supsession.Visible = false;
        rfvDept.Visible = false;
        sImp.Visible = false;
        rfvcollege.Visible = false;
        supProgram.Visible = false;
        rfvDegree.Visible = false;
        supSemester.Visible = false;
        rfvSemester.Visible = false;
        supModule.Visible = false;
        rfvSubjectType.Visible = false;
        supCourse.Visible = false;
        rfvCourse.Visible = false;
    }
    protected void MandetaryValidation()
    {
        if (ViewState["SESSION"].ToString() != "0")
        {
            supsession.Visible = true;
            rfvDept.Visible = true;

            sImp.Visible = false;
            rfvcollege.Visible = false;

            supProgram.Visible = false;
            rfvDegree.Visible = false;

            supSemester.Visible = false;
            rfvSemester.Visible = false;

            supModule.Visible = false;
            rfvSubjectType.Visible = false;

            supCourse.Visible = false;
            rfvCourse.Visible = false;
        }
        if (ViewState["COLLEGE"].ToString() != "0")
        {
            supsession.Visible = true;
            rfvDept.Visible = true;

            sImp.Visible = true;
            rfvcollege.Visible = true;

            supProgram.Visible = false;
            rfvDegree.Visible = false;

            supSemester.Visible = false;
            rfvSemester.Visible = false;

            supModule.Visible = false;
            rfvSubjectType.Visible = false;

            supCourse.Visible = false;
            rfvCourse.Visible = false;
        }
        if (ViewState["PROGRAM"].ToString() != "0")
        {
            supsession.Visible = true;
            rfvDept.Visible = true;

            sImp.Visible = true;
            rfvcollege.Visible = true;

            supProgram.Visible = true;
            rfvDegree.Visible = true;

            supSemester.Visible = false;
            rfvSemester.Visible = false;

            supModule.Visible = false;
            rfvSubjectType.Visible = false;

            supCourse.Visible = false;
            rfvCourse.Visible = false;
        }
        if (ViewState["SEMESTER"].ToString() != "0")
        {
            supsession.Visible = true;
            rfvDept.Visible = true;

            sImp.Visible = true;
            rfvcollege.Visible = true;

            supProgram.Visible = true;
            rfvDegree.Visible = true;

            supSemester.Visible = true;
            rfvSemester.Visible = true;

            supModule.Visible = false;
            rfvSubjectType.Visible = false;

            supCourse.Visible = false;
            rfvCourse.Visible = false;
        }
        if (ViewState["STDUYTYPE"].ToString() != "0")
        {
            supsession.Visible = true;
            rfvDept.Visible = true;

            sImp.Visible = true;
            rfvcollege.Visible = true;

            supProgram.Visible = true;
            rfvDegree.Visible = true;

            supSemester.Visible = true;
            rfvSemester.Visible = true;

            supModule.Visible = true;
            rfvSubjectType.Visible = true;

            supCourse.Visible = false;
            rfvCourse.Visible = false;
        }
        if (ViewState["SUBJECT"].ToString() != "0")
        {
            supsession.Visible = true;
            rfvDept.Visible = true;

            sImp.Visible = true;
            rfvcollege.Visible = true;

            supProgram.Visible = true;
            rfvDegree.Visible = true;

            supSemester.Visible = true;
            rfvSemester.Visible = true;

            supModule.Visible = true;
            rfvSubjectType.Visible = true;

            supCourse.Visible = true;
            rfvCourse.Visible = true;
        }
    }
    #endregion

    protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlReport.SelectedValue != "0" && ddlReport.SelectedValue != "21" && ddlReport.SelectedValue != "22" && ddlReport.SelectedValue != "23")
            {
                btnExcel.Visible = true;
                btnPdf.Visible = false;
                btnClassList.Visible = false;
                btnCoursewiseSubjectlist.Visible = false;

                DataSet ds = objCommon.FillDropDown("ACD_REPORT_TYPE", "REPORT_NAME,PROCEDURE_NAME,ISNULL(SESSION_MANDATORY,0) AS SESSION,ISNULL(COLLEGE_MANDATORY,0) AS COLLEGE ,ISNULL(PROGRAM_MANDATORY,0) AS PROGRAM,ISNULL(SEMESTER_MANDATORY,0) AS SEMESTER,ISNULL(STDUYTYPE_MANDATORY,0) AS STDUYTYPE,ISNULL(SUBJECT_MANDATORY,0)AS SUBJECT", "RPT_NO", "RPT_NO=" + ddlReport.SelectedValue, "");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["REPORT_NAME"] = ds.Tables[0].Rows[0]["REPORT_NAME"].ToString();
                    ViewState["PROCEDURE_NAME"] = ds.Tables[0].Rows[0]["PROCEDURE_NAME"].ToString();
                    ViewState["SESSION"] = ds.Tables[0].Rows[0]["SESSION"].ToString();
                    ViewState["COLLEGE"] = ds.Tables[0].Rows[0]["COLLEGE"].ToString();
                    ViewState["PROGRAM"] = ds.Tables[0].Rows[0]["PROGRAM"].ToString();
                    ViewState["SEMESTER"] = ds.Tables[0].Rows[0]["SEMESTER"].ToString();
                    ViewState["STDUYTYPE"] = ds.Tables[0].Rows[0]["STDUYTYPE"].ToString();
                    ViewState["SUBJECT"] = ds.Tables[0].Rows[0]["SUBJECT"].ToString();
                    Validation();
                    MandetaryValidation();
                }
            }

            else if (ddlReport.SelectedValue == "21")
            {
                btnExcel.Visible = false;
                btnPdf.Visible = false;

                supsession.Visible = true;
                sImp.Visible = true;
                supProgram.Visible = true;
                supSemester.Visible = true;
                supModule.Visible = true;
                supCourse.Visible = true;

                btnClassList.Visible = true;
                btnClassList.Focus();
            }

            else if (ddlReport.SelectedValue == "22")
            {
                //sImp.Visible = false;
                btnPdf.Visible = true;
                btnExcel.Visible = false;
                btnClassList.Visible = false;
                btnCoursewiseSubjectlist.Visible = false;

                RequiredFieldValidator5.Visible = false;
                rvfEnrolldegree.Visible = false;

                supsession.Visible = true;
                sImp.Visible = false;
                supProgram.Visible = false;
                supSemester.Visible = false;
                supModule.Visible = false;
                supCourse.Visible = false;
            }

            else if (ddlReport.SelectedValue == "23")
            {
                btnExcel.Visible = false;
                btnPdf.Visible = true;
                btnClassList.Visible = false;
                btnCoursewiseSubjectlist.Visible = false;

                RequiredFieldValidator5.Visible = true;

                supsession.Visible = true;
                sImp.Visible = true;
                supProgram.Visible = false;
                supSemester.Visible = false;
                supModule.Visible = false;
                supCourse.Visible = false;
            }
            else
            {
                Validation();
                ClearViewState();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnClassList_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlReport.SelectedIndex <= 0)
            {
                string lblrptname = lblDyReport.Text.ToString();
                objCommon.DisplayMessage(UpdatePanel1, "Please Select " + lblrptname + ".", this.Page);
                return;
            }
            if (ddlSession.SelectedIndex <= 0)
            {
                string lblsession = lblDYddlSession.Text.ToString();
                objCommon.DisplayMessage(UpdatePanel1, "Please Select " + lblsession + ".", this.Page);
                return;
            }
            if (ddlCollegeName.SelectedIndex <= 0)
            {
                string lblclgname = lblDYCollege.Text.ToString();
                objCommon.DisplayMessage(UpdatePanel1, "Please Select " + lblclgname + ".", this.Page);
                return;
            }

            if (ddlDegree.SelectedIndex <= 0)
            {
                string lbldegree = lblDyProgram.Text.ToString();
                objCommon.DisplayMessage(UpdatePanel1, "Please Select " + lbldegree + ".", this.Page);
                return;
            }
            if (ddlSemester.SelectedIndex <= 0)
            {
                string lblsem = lblDYSemester.Text.ToString();
                objCommon.DisplayMessage(UpdatePanel1, "Please Select " + lblsem + ".", this.Page);
                return;
            }
            if (ddlSubjectType.SelectedIndex <= 0)
            {
                string lblsubtype = lblDYModuleType.Text.ToString();
                objCommon.DisplayMessage(UpdatePanel1, "Please Select " + lblsubtype + ".", this.Page);
                return;
            }
            if (ddlCourse.SelectedIndex <= 0)
            {
                string lblcourse = lblICCourse.Text.ToString();
                objCommon.DisplayMessage(UpdatePanel1, "Please Select " + lblcourse + ".", this.Page);
                return;
            }

            this.ShowReportPDF("pdf", "ClassListReport.rpt");
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindSection();
        }
        catch (Exception ex)
        {

        }

    }

    protected void btnPdf_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlReport.SelectedValue == "22")
            {
                this.ShowReportPDF("Enlistment Resume Report", "rptEnlistmentResumeReport.rpt");
            }
            else if (ddlReport.SelectedValue == "23")
            {
                this.ShowReportPDF("Prospectus Report", "rptAcademicRegistryProspectusReport.rpt");
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "PDF Report Not Found", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }

}

