using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using ClosedXML.Excel;
using System.IO;

public partial class EnrollmentReports : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
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
                    this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {

                    }
                    objCommon.FillDropDownList(ddlReportType, "ACD_REPORT_TYPE", "RPT_NO", "REPORT_NAME", "", "");
                    objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "ACTIVE=1", "");
                    //objCommon.FillListBox(lstSchoolCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "ACTIVE=1", "");

                    objCommon.FillListBox(lstSchoolCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and isnull(ACTIVE,0)=1", "COLLEGE_ID");

                }
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EnrollmentReports.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=EnrollmentReports.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=EnrollmentReports.aspx");
        }
    }
    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlReportType.SelectedValue == "1")
            {
                Admission_Applicant_Report();
            }
            else if (ddlReportType.SelectedValue == "2")
            {
                Admitted_Applicant_Report();
            }
            else if (ddlReportType.SelectedValue == "3")
            {
                Applicant_Paid_Report(1);
            }
            else if (ddlReportType.SelectedValue == "4")
            {
                TransfreeApplicant_Paid_Report();
            }
            else if (ddlReportType.SelectedValue == "5")
            {
                Registered_Applicant_Report();
            }
            else if (ddlReportType.SelectedValue == "6")
            {
                Shiftee_Applicant_Report();
            }
            else if (ddlReportType.SelectedValue == "7")
            {
                Applicant_Nationality_Report();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EnrollmentReports.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void Admission_Applicant_Report()
    {
        string StartDate = "2015-02-01";
        string EndDate = "2035-02-01";
        string college_id = string.Empty;
        foreach (ListItem items in lstSchoolCollege.Items)
        {
            if (items.Selected == true)
            {
                college_id += items.Value + '$';

            }
        }
        if (college_id == string.Empty)
        {
            college_id = "0$";
        }
        college_id = college_id.Substring(0, college_id.Length - 1);
        int Intake = Convert.ToInt32(ddlIntake.SelectedValue);
        if (che.Checked == true)
        {
            StartDate = txtStartDate.Text;
            EndDate = txtEndDate.Text;
        }
        string SP_Name2 = "PKG_NUMBER_OF_ADMISSION_APPLICANTS_REPORT";
        string SP_Parameters2 = "@P_INTAKE,@P_COLLEGE_ID,@P_STARTDATE,@P_ENDTDATE";
        string Call_Values2 = "" + Convert.ToInt32(Intake) + "," + college_id.ToString() + "," + StartDate.ToString() + "," + EndDate.ToString() + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);

        DataGrid dg = new DataGrid();
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string attachment = "attachment; filename=" + "AdmissionApplicatList.xls";
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
    protected void Admitted_Applicant_Report()
    {
        string StartDate = "2015-02-01";
        string EndDate = "2035-02-01";
        string college_id = string.Empty;
        foreach (ListItem items in lstSchoolCollege.Items)
        {
            if (items.Selected == true)
            {
                college_id += items.Value + '$';

            }
        }
        if (college_id == string.Empty)
        {
            college_id = "0$";
        }
        college_id = college_id.Substring(0, college_id.Length - 1);
        int Intake = Convert.ToInt32(ddlIntake.SelectedValue);
        if (che.Checked == true)
        {
            StartDate = txtStartDate.Text;
            EndDate = txtEndDate.Text;
        }
        string SP_Name2 = "PKG_GET_ADMITTED_APPLICANTS_USA";
        string SP_Parameters2 = "@P_INTAKE,@P_COLLEGE_ID,@P_STARTDATE,@P_ENDTDATE";
        string Call_Values2 = "" + Convert.ToInt32(Intake) + "," + college_id.ToString() + "," + StartDate.ToString() + "," + EndDate.ToString() + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);

        DataGrid dg = new DataGrid();
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string attachment = "attachment; filename=" + "AdmittedApplicatList.xls";
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
    protected void TransfreeApplicant_Paid_Report()
    {
        string StartDate = "2015-02-01";
        string EndDate = "2035-02-01";
        string college_id = string.Empty;
        foreach (ListItem items in lstSchoolCollege.Items)
        {
            if (items.Selected == true)
            {
                college_id += items.Value + '$';

            }
        }
        if (college_id == string.Empty)
        {
            college_id = "0$";
        }
        college_id = college_id.Substring(0, college_id.Length - 1);
        int Intake = Convert.ToInt32(ddlIntake.SelectedValue);
        if (che.Checked == true)
        {
            StartDate = txtStartDate.Text;
            EndDate = txtEndDate.Text;
        }
        string SP_Name2 = "PKG_GET_PAID_APPLICANTS_DETAIL";
        string SP_Parameters2 = "@P_INTAKE,@P_COLLEGE_ID,@P_STARTDATE,@P_ENDTDATE,@P_COMMAND_TYPE";
        string Call_Values2 = "" + Convert.ToInt32(Intake) + "," + college_id.ToString() + "," + StartDate.ToString() + "," + EndDate.ToString() + "," + 2 + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);

        DataGrid dg = new DataGrid();
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].TableName = "Transferee Student List";
            ds.Tables[1].TableName = "Transferee Summary  Report";
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
                Response.AddHeader("content-disposition", "attachment;filename=TransfereeApplicantsDetails.xlsx");
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


    protected void Registered_Applicant_Report()
    {
        string StartDate = "2015-02-01";
        string EndDate = "2035-02-01";
        string college_id = string.Empty;
        foreach (ListItem items in lstSchoolCollege.Items)
        {
            if (items.Selected == true)
            {
                college_id += items.Value + '$';

            }
        }
        if (college_id == string.Empty)
        {
            college_id = "0$";
        }
        college_id = college_id.Substring(0, college_id.Length - 1);
        int Intake = Convert.ToInt32(ddlIntake.SelectedValue);
        if (che.Checked == true)
        {
            StartDate = txtStartDate.Text;
            EndDate = txtEndDate.Text;
        }
        string SP_Name2 = "PKG_GET_REGISTERED_APPLICANT_DETAILS";
        string SP_Parameters2 = "@P_INTAKE,@P_COLLEGE_ID,@P_STARTDATE,@P_ENDTDATE,@P_COMMAND_TYPE";
        string Call_Values2 = "" + Convert.ToInt32(Intake) + "," + college_id.ToString() + "," + StartDate.ToString() + "," + EndDate.ToString() + "," + 2 + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);

        DataGrid dg = new DataGrid();
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].TableName = "Registered Student List";
            //ds.Tables[1].TableName = "Transferee Summary  Report";
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
                Response.AddHeader("content-disposition", "attachment;filename=RegisteredApplicantsDetails.xlsx");
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

    protected void Applicant_Paid_Report(int CommandType)
    {
        string FileName="ApplicatPaidList.xls";
        if (CommandType == 2)
        {
            FileName = "TransfereeApplicantsDetails.xls";
        }
        string StartDate = "2015-02-01";
        string EndDate = "2035-02-01";
        string college_id = string.Empty;
        foreach (ListItem items in lstSchoolCollege.Items)
        {
            if (items.Selected == true)
            {
                college_id += items.Value + '$';

            }
        }
        if (college_id == string.Empty)
        {
            college_id = "0$";
        }
        college_id = college_id.Substring(0, college_id.Length - 1);
        int Intake = Convert.ToInt32(ddlIntake.SelectedValue);
        if (che.Checked == true)
        {
            StartDate = txtStartDate.Text;
            EndDate = txtEndDate.Text;
        }
        string SP_Name2 = "PKG_GET_PAID_APPLICANTS_DETAIL";
        string SP_Parameters2 = "@P_INTAKE,@P_COLLEGE_ID,@P_STARTDATE,@P_ENDTDATE,@P_COMMAND_TYPE";
        string Call_Values2 = "" + Convert.ToInt32(Intake) + "," + college_id.ToString() + "," + StartDate.ToString() + "," + EndDate.ToString() + "," + CommandType + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);

        DataGrid dg = new DataGrid();
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string attachment = "attachment; filename=" + FileName.ToString();
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            dg.DataSource = ds.Tables[0];
            dg.DataBind();
            dg.HeaderStyle.Font.Bold = true;
            dg.HeaderStyle.Font.Name = "Calibri";
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

    protected void Shiftee_Applicant_Report()
    {
        string StartDate = "2015-02-01";
        string EndDate = "2035-02-01";
        string college_id = string.Empty;
        foreach (ListItem items in lstSchoolCollege.Items)
        {
            if (items.Selected == true)
            {
                college_id += items.Value + '$';

            }
        }
        if (college_id == string.Empty)
        {
            college_id = "0$";
        }
        college_id = college_id.Substring(0, college_id.Length - 1);
        int Intake = Convert.ToInt32(ddlIntake.SelectedValue);
        if (che.Checked == true)
        {
            StartDate = txtStartDate.Text;
            EndDate = txtEndDate.Text;
        }
        string SP_Name2 = "PKG_GET_SHIFTEES_APPLICANT_DETAILS";
        string SP_Parameters2 = "@P_INTAKE,@P_COLLEGE_ID,@P_STARTDATE,@P_ENDTDATE,@P_COMMAND_TYPE";
        string Call_Values2 = "" + Convert.ToInt32(Intake) + "," + college_id.ToString() + "," + StartDate.ToString() + "," + EndDate.ToString() + "," + 2 + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);

        DataGrid dg = new DataGrid();
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].TableName = "Shiftees Student List";
            //ds.Tables[1].TableName = "Transferee Summary  Report";
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
                Response.AddHeader("content-disposition", "attachment;filename=ShifteeApplicantsDetails.xlsx");
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

    protected void Applicant_Nationality_Report()
    {
        string StartDate = "2015-02-01";
        string EndDate = "2035-02-01";
        string college_id = string.Empty;
        foreach (ListItem items in lstSchoolCollege.Items)
        {
            if (items.Selected == true)
            {
                college_id += items.Value + '$';

            }
        }
        if (college_id == string.Empty)
        {
            college_id = "0$";
        }
        college_id = college_id.Substring(0, college_id.Length - 1);
        int Intake = Convert.ToInt32(ddlIntake.SelectedValue);
        if (che.Checked == true)
        {
            StartDate = txtStartDate.Text;
            EndDate = txtEndDate.Text;
        }
        string SP_Name2 = "PKG_GET_APPLICANT_NATIONALITY_DETAILS";
        string SP_Parameters2 = "@P_INTAKE,@P_COLLEGE_ID,@P_STARTDATE,@P_ENDTDATE,@P_COMMAND_TYPE";
        string Call_Values2 = "" + Convert.ToInt32(Intake) + "," + college_id.ToString() + "," + StartDate.ToString() + "," + EndDate.ToString() + "," + 2 + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);

        DataGrid dg = new DataGrid();
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].TableName = "Student Nationality List";
            //ds.Tables[1].TableName = "Transferee Summary  Report";
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
                Response.AddHeader("content-disposition", "attachment;filename=ApplicantsNationalityDetails.xlsx");
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void che_CheckedChanged(object sender, EventArgs e)
    {
        if (che.Checked == true)
        {
            StartDate.Visible = true;
            EndDate.Visible = true;
            txtStartDate.Text = "";
            txtEndDate.Text = "";
        }
        else
        {
            StartDate.Visible = false;
            EndDate.Visible = false;
            txtStartDate.Text = "";
            txtEndDate.Text = "";
        }
    }
}