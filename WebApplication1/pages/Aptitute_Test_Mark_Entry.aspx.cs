using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Web.Configuration;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Security;
using mastersofterp;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

public partial class ACADEMIC_Aptitute_Test_Mark_Entry : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MappingController objmp = new MappingController();
    StudentController objStud = new StudentController();
    Student_Acd obj = new Student_Acd();
    User_AccController objUC = new User_AccController();
    UserAcc objUA = new UserAcc();
    ImportDataMaster objIDM = new ImportDataMaster();
    ImportDataController objIDC = new ImportDataController();

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
                
                 
                    FilldropDownTest();
                    FilldropDown();


                }
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
                lvapti.Visible = false;

            }
            if (Request.Params["__EVENTTARGET"] != null &&
              Request.Params["__EVENTTARGET"].ToString() != string.Empty)
            {

                //ScriptManager.RegisterClientScriptBlock(updIntakeAlootment, updIntakeAlootment.GetType(), "Src", "Setdate('" + hdfdate.Value + "');", true);
            }
            if (Request.Params["__EVENTTARGET"] != null &&
              Request.Params["__EVENTTARGET"].ToString() != string.Empty)
            {


            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACDEMIC_COLLEGE_MASTER.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=Aptitute_Test_Mark_Entry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Aptitute_Test_Mark_Entry.aspx");
        }
    }

    //Aptitude Test Mark Entry Start

    private void FilldropDownTest()
    {
        objCommon.FillDropDownList(ddlintakeapti, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVE = 1", "BATCHNO DESC");
        objCommon.FillDropDownList(ddlintakeprep, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVE = 1", "BATCHNO DESC");
        objCommon.FillDropDownList(ddlfaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_ID");
        objCommon.FillListBox(ddldesrdiofir, "ACD_AREA_OF_INTEREST", "AREA_INT_NO", "AREA_INT_NAME", "", "AREA_INT_NO");
        objCommon.FillListBox(ddldes, "ACD_AREA_OF_INTEREST", "AREA_INT_NO", "AREA_INT_NAME", "", "AREA_INT_NO");
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH  B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT A.DEGREENO", "A.DEGREENAME", "A.DEGREENO > 0 and isnull(ACTIVE,0)=1", "A.DEGREENAME");
    }

   

    protected void rdbFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbFilter.SelectedValue == "1")
        {
            btnDownload.Visible = true;
            // btnCancel.Visible = true;
            lvapti.Visible = false;
            btnUpload.Visible = false;
            FileUpload.Visible = false;
            btnMarksPreData.Visible = false;
            //divdegree.Visible = false;
            //divprogram.Visible = true;
            //divfaculty.Visible = true;
            divdesfirst.Visible = true;
            divdesp.Visible = false;
            intake.Visible = true;
            divintaketwo.Visible = false;
            divMultiselectdate.Visible = true;
            divapti.Visible = false;
            lvapti.DataSource = null;
            lvapti.DataBind();
            ViewData.DataSource = null;
            ViewData.DataBind();

            divuploadexcel.Visible = false;
            lvuploexcel.DataSource = null;
            lvuploexcel.DataBind();

            btncanceltest.Visible = true;
            btnDownload.Visible = true;

            btnMarksPreData.Visible = false;
            btnUpload.Visible = false;
            //btnverify.Visible = false;
            btnconfirmed.Visible = false;
            btnexcelformat.Visible = false;
            btnView.Visible = false;
            ddlintakeprep.SelectedIndex = 0;

        }
        else if (rdbFilter.SelectedValue == "2")
        {
            lvapti.Visible = true;
            btnUpload.Visible = true;
            btnDownload.Visible = false;
            FileUpload.Visible = true;
            btnDownload.Visible = false;
            ddlintakeapti.SelectedIndex = 0;

           
            divMultiselectdate.Visible = false;
            ddlDegree.SelectedIndex = 0;
            btnMarksPreData.Visible = true;
          
            divdesfirst.Visible = false;
            divdesp.Visible = true;

            divtestprep.Visible = false;
            lvtestprep.DataSource = null;
            lvtestprep.DataBind();

            intake.Visible = false;
            divintaketwo.Visible = true;
            btncanceltest.Visible = true;
            btnDownload.Visible = false;
            ViewData.DataSource = null;
            ViewData.DataBind();
            btnMarksPreData.Visible = true;
            btnUpload.Visible = true;
            //btnverify.Visible = true;
            btnconfirmed.Visible = true;
            btnexcelformat.Visible = true;
            btnView.Visible = true;
        }

    }

    protected void btncanceltest_Click(object sender, EventArgs e)
    {
        // divlkintaketransfer.Attributes.Remove("class");
        ddlintakeapti.SelectedIndex = 0;
        ddldesrdiofir.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddldes.SelectedValue = null;
        ddlfaculty.SelectedIndex = 0;
        ddlprgm.SelectedIndex = 0;
        ddlintakeprep.SelectedIndex = 0;
        ddldesrdiofir.SelectedValue = null;

        divapti.Visible = false;
        lvapti.DataSource = null;
        lvapti.DataBind();
        ViewData.DataSource = null;
        ViewData.DataBind();

        lvuploexcel.DataSource = null;
        lvuploexcel.DataBind();

        lvapti.DataSource = null;
        lvapti.DataBind();

        lvtestprep.DataSource = null;
        lvtestprep.DataBind();
        FileUpload1.Attributes.Clear();

    }
    protected void lbExcelFormat_Click1(object sender, EventArgs e)
    {
        Response.ContentType = "application/vnd.ms-excel";
        string path = Server.MapPath("~/ExcelFormat/apptitude_exam_test_sheet.xls");
        Response.AddHeader("Content-Disposition", "attachment;filename=\"apptitude_exam_test_sheet.xls\"");
        Response.TransmitFile(path);
        Response.End();
    }
    protected void btnexcelformat_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/vnd.ms-excel";
        string path = Server.MapPath("~/ExcelFormat/Preformat_Of_Exam_Result.xls");
        Response.AddHeader("Content-Disposition", "attachment;filename=\"Preformat_Of_Exam_Result.xls\"");
        Response.TransmitFile(path);
        Response.End();
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        GridView GvStudent = new GridView();
       
        string area = ""; string[] getedates;

        foreach (ListItem item in ddldesrdiofir.Items)
        {
            if (item.Selected == true)
            {
                area += item.Value + ',';
            }
        }
        if (!string.IsNullOrEmpty(area))
        {
            area = area.Substring(0, area.Length - 1);
        }
        else
        {
            area = "0";
        }
        DataSet dsfee = null;
        if (hdfGetFromDate.Value.ToString() == string.Empty)
        {
            getedates = "0,0".Split(',');
            dsfee = objIDC.getTestPrepDataExcel(Convert.ToInt32(ddlintakeapti.SelectedValue), area, Convert.ToString(getedates[0]), Convert.ToString(getedates[1]));
        }
        else
        {
            getedates = hdfGetFromDate.Value.ToString().Split('-');
            dsfee = objIDC.getTestPrepDataExcel(Convert.ToInt32(ddlintakeapti.SelectedValue), area, Convert.ToString(Convert.ToDateTime(getedates[0]).ToString("dd/MM/yyyy")), Convert.ToString(Convert.ToDateTime(getedates[1]).ToString("dd/MM/yyyy")));
        }

        if (dsfee.Tables[0].Rows.Count > 0)
        {
            GvStudent.DataSource = dsfee;
            GvStudent.DataBind();
            string attachment = "attachment; filename=TestprepDataExcel.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GvStudent.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
        else
        {
            objCommon.DisplayMessage(updapptitutetest, "Data Not Found", this.Page);
        }
    }


    private bool CheckFormatAndImport(string extension, string excelPath)
    {
        string conString = string.Empty;
        switch (extension)
        {
            case ".xls":
                conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx":
                conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                break;
        }

        conString = string.Format(conString, excelPath);
        objCommon.DisplayMessage(this.updapptitutetest, conString, this.Page);
        try
        {
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                DataTable dtExcelData = new DataTable();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString().Trim();

                dtExcelData.Columns.AddRange(new DataColumn[] {


                new DataColumn("ExamName", typeof(string)),
                new DataColumn("SubjectName", typeof(string)),
                new DataColumn("RollNo", typeof(string)),
                new DataColumn("PRNNo", typeof(string)),
                new DataColumn("CandidateName", typeof(string)),
                new DataColumn("MobileNo", typeof(string)),
                new DataColumn("MaxMarks", typeof(decimal)),
                new DataColumn("MarksObtained", typeof(decimal)),
                new DataColumn("ExamSubmitDate", typeof(DateTime)),
                new DataColumn("ExamStatus", typeof(string)),
                new DataColumn("General", typeof(decimal)),
                new DataColumn("English", typeof(decimal)),
                     
                });

                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                {
                    oda.Fill(dtExcelData);
                    DataSet ds1 = new DataSet();
                    ds1.Tables.Add(dtExcelData);
                    divapti.Visible = true;
                    btncanceltest.Visible = true;
                    foreach (DataTable table in ds1.Tables)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            foreach (DataRow dr in table.Rows)
                            {
                                if (dr["CandidateName"].ToString() == string.Empty)
                                {
                                    objCommon.DisplayMessage("Cant Pass Null Records in Excel Sheet!!", this);
                                    dr.Delete();
                                    table.AcceptChanges();
                                    break;

                                }
                            }
                        }
                    }
                }
                excel_con.Close();
                string area = "";

                foreach (ListItem item in ddldes.Items)
                {
                    if (item.Selected == true)
                    {
                        area += item.Value + ',';
                    }
                }
                if (!string.IsNullOrEmpty(area))
                {
                    area = area.Substring(0, area.Length - 1);
                }
                else
                {
                    //area = "0";
                  
                    objCommon.DisplayMessage("Please Select Descipline.!!", this);
                    //objCommon.DisplayMessage(this.updapptitutetest, "Please Select Descipline.", this.Page);
                    return true;
                }


                objIDM.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
                int ret = objmp.ImportDataForApti(dtExcelData, Convert.ToInt32(ddlintakeprep.SelectedValue), Convert.ToInt32(Session["userno"]), area);
                if (ret == 1)
                {
                    lvapti.DataSource = dtExcelData;
                    divapti.Visible = true;
                    excellist();
                    btncanceltest.Visible = true;
                    ddlintakeapti.SelectedIndex = 0;
                    objCommon.DisplayMessage("Data Upload Successfully!!", this);
                    //lblTotalMsg.Text = "Records uploaded ";
                    //ddlintakeprep.SelectedIndex = 0;
                    //ddldes.SelectedIndex = 0;
                    lblTotalMsg.Style.Add("color", "green");
                    return true;
                }

                else
                {
                    objCommon.DisplayMessage("Record not uploaded, file is not in correct format. Please check the format of file & upload again.", this);
                    lblTotalMsg.Text = "Error.!! Record not saved.";
                    return false;
                }

            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage("Record not uploaded, file is not in correct format. Please check the format of file & upload again.", this);
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Records not saved. Please check whether the file is in correct format or not. ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(this.Page, "Server UnAvailable");
            return false;
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string area = "";

            foreach (ListItem item in ddldes.Items)
            {
                if (item.Selected == true)
                {
                    area += item.Value + ',';
                }
            }
            if (!string.IsNullOrEmpty(area))
            {
                area = area.Substring(0, area.Length - 1);
            }
            else
            {
                //area = "0";

                objCommon.DisplayMessage("Please Select Descipline.!!", this);
                //objCommon.DisplayMessage(this.updapptitutetest, "Please Select Descipline.", this.Page);
                return ;
            }
        


            string path = System.Web.HttpContext.Current.Server.MapPath("~/ExcelData/");
            if (FileUpload1.PostedFile.FileName.ToString() == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Attach File. !!!", this.Page);
                lblTotalMsg.Text = "Please select file to upload.";
                return;
            }
            else
            {
                string extension = Path.GetExtension(FileUpload1.PostedFile.FileName.Trim());

                if (extension.Contains(".xls") || extension.Contains(".xlsx"))
                {
                    if (!(Directory.Exists(MapPath("~/PresentationLayer/ExcelData"))))
                        Directory.CreateDirectory(path);

                    string fileName = FileUpload1.PostedFile.FileName.Trim();
                    if (File.Exists((path + fileName).ToString().Trim()))
                        File.Delete((path + fileName).ToString().Trim());
                    FileUpload1.SaveAs(path + fileName);
                    CheckFormatAndImport(extension, path.Trim() + fileName);

                    //Added My Mahesh M on dated 09-01-2022
                    divuploadexcel.Visible = false;
                    lvuploexcel.DataSource = null;
                    lvuploexcel.DataBind();
                }
                else
                {
                    // lblTotalMsg.Text = "Only excel sheet is allowed to upload";

                    objCommon.DisplayMessage(this.updapptitutetest, "Only excel sheet is allowed to upload.", this.Page);

                    return;
                }
            }
            //else
            //{
            //    lblTotalMsg.Text = "Please select file to upload.";
            //    objCommon.DisplayMessage(this.updapptitutetest, "Please select file to upload.", this.Page);
            //    return;
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnMarksPreData_Click(object sender, EventArgs e)
    {

        if (ddlintakeprep.SelectedIndex <= 0)
        {
            objCommon.DisplayMessage(this.updapptitutetest, "Please Select Intake!", this.Page);
            return;
        }

        GridView GvStudent = new GridView();
        string area = "";

        foreach (ListItem item in ddldes.Items)
        {
            if (item.Selected == true)
            {
                area += item.Value + ',';
            }
        }
        if (!string.IsNullOrEmpty(area))
        {
            area = area.Substring(0, area.Length - 1);
        }
        else
        {
            area = "0";
        }
        DataSet dsfee = objIDC.getApptiDataExcel(Convert.ToInt32(ddlintakeprep.SelectedValue), area);
        if (dsfee.Tables[0].Rows.Count > 0)
        {
            GvStudent.DataSource = dsfee;
            GvStudent.DataBind();
            string attachment = "attachment; filename=TestprepDataExcel.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GvStudent.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
        else
        {
            objCommon.DisplayMessage(updapptitutetest, "Data Not Found", this.Page);
        }
    }
    private void bindtestprep()
    {
     
        string area = "";

        foreach (ListItem item in ddldesrdiofir.Items)
        {
            if (item.Selected == true)
            {
                area += item.Value + ',';
            }
        }
        if (!string.IsNullOrEmpty(area))
        {
            area = area.Substring(0, area.Length - 1);
        }
        else
        {
            area = "0";
        }
        DataSet ds = objmp.gettwestprepdata(Convert.ToInt32(ddlintakeapti.SelectedValue), area, "0", "0");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvtestprep.DataSource = ds;
            lvtestprep.DataBind();
            divtestprep.Visible = true;

        }
        else
        {
            lvtestprep.DataSource = null;
            lvtestprep.DataBind();
            divtestprep.Visible = false;

        }
    }
    protected void confirmedexcellist()
    {
        //  divlkintaketransfer.Attributes.Remove("class");
        string area = "";

        foreach (ListItem item in ddldes.Items)
        {
            if (item.Selected == true)
            {
                area += item.Value + ',';
            }
        }
        if (!string.IsNullOrEmpty(area))
        {
            area = area.Substring(0, area.Length - 1);
        }
        else
        {
            area = "0";
        }
        DataSet ds = objmp.confirmedexcelsheet(Convert.ToInt32(ddlintakeprep.SelectedValue), area);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DivViewData.Visible = false;
            divapti.Visible = false;
            lvapti.DataSource = null;
            lvapti.DataBind();
            ViewData.DataSource = null;
            ViewData.DataBind();
            divuploadexcel.Visible = true;
            lvuploexcel.DataSource = ds;
            lvuploexcel.DataBind();
        }
        else
        {
            DivViewData.Visible = false;
            divapti.Visible = false;
            lvapti.DataSource = null;
            lvapti.DataBind();
            divuploadexcel.Visible = false;
            lvuploexcel.DataSource = null;
            lvuploexcel.DataBind();

        }

    }
    protected void excellist()
    {
        // divlkintaketransfer.Attributes.Remove("class");
        string area = "";

        foreach (ListItem item in ddldes.Items)
        {
            if (item.Selected == true)
            {
                area += item.Value + ',';
            }
        }
        if (!string.IsNullOrEmpty(area))
        {
            area = area.Substring(0, area.Length - 1);
        }
        else
        {
            area = "0";
        }
        DataSet ds = objmp.excelsheet(Convert.ToInt32(ddlintakeprep.SelectedValue), area);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            divapti.Visible = true;
            lvapti.DataSource = ds;
            lvapti.DataBind();
        }
        else
        {
            divapti.Visible = false;
            lvapti.DataSource = null;
            lvapti.DataBind();
        }

    }
    protected void ddlfaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlfaculty.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlprgm, "ACD_USER_BRANCH_PREF S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=S.BRANCHNO)", "DISTINCT CONCAT(D.DEGREENO, ', ',B.BRANCHNO)AS ID", "(D.DEGREENAME+'-'+B.LONGNAME)AS DEGBRANCH", "S.DEGREENO>0 AND S.COLLEGE_ID=" + (ddlfaculty.SelectedValue), "ID");
        }
        // bindtestprep();
    }
    protected void ddlintakeprep_SelectedIndexChanged(object sender, EventArgs e)
    {
        DivViewData.Visible = false;
        divlvaptimark.Visible = false;
    }
    protected void ddlintakeapti_SelectedIndexChanged(object sender, EventArgs e)
    {
        DivViewData.Visible = false;
        bindtestprep();

    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        excellist();
    }
    protected void ddlprgm_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindtestprep();

    }
    protected void ddldesrdiofir_SelectedIndexChanged(object sender, EventArgs e)
    {
        DivViewData.Visible = false;
        divlvaptimark.Visible = false;
    }
   

   
    private void VerifyandRegister()
    {
        try
        {
            DataSet ds = null;
            int retout = 0;
            retout = objmp.VerifyandMovedImportedDataExt(Convert.ToInt32(ddlintakeprep.SelectedValue), Convert.ToInt32(Session["uano"]));//, out retout
            if (retout > 0)
            {
                objCommon.DisplayMessage(updapptitutetest, "Records verified successfully", this.Page);
                confirmedexcellist();
                //lblTotalMsg.Text = "Records Verified ";
                //lblTotalMsg.Style.Add("color", "green");

            }
            else
            {
                objCommon.DisplayMessage(updapptitutetest, "Records verified Fail.", this.Page);
            }

        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(updapptitutetest, "Error occurred.", this.Page);
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Error occurred. ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(this.Page, "Server UnAvailable");
        }
    }
    protected void btnconfirmed_Click(object sender, EventArgs e)
    {
        int cntExcelUpload = 0;
        cntExcelUpload = Convert.ToInt16(objCommon.LookUp("TEMP_APPTITUDE_TEST_MARK_ENTRY", "COUNT(ROLLNO)", "ISNULL(CANCEL,0)=0 AND INTAKENO=" + ddlintakeprep.SelectedValue));

        if (cntExcelUpload > 0)
        {
            VerifyandRegister();
        }
        else
        {
            objCommon.DisplayMessage(updapptitutetest, "Excel sheet is not uploaded for given selection. First upload excel sheet then Verify and Move.", this);
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        if (ddlintakeprep.SelectedItem != null)
        {

            BindListViewDataAndBiew();
        }
        else
        {

            objCommon.DisplayMessage(this.updapptitutetest, "Please Select Intake.", this.Page);
        }
    }
    protected void BindListViewDataAndBiew()
    {

        string areas = "";

        foreach (ListItem item in ddldes.Items)
        {
            if (item.Selected == true)
            {
                areas += item.Value + ',';
            }
        }
        if (!string.IsNullOrEmpty(areas))
        {
            areas = areas.Substring(0, areas.Length - 1);
        }
        else
        {
            areas = "0";
        }
        DataSet ds = objmp.confirmedexcelsheet(Convert.ToInt32(ddlintakeprep.SelectedValue), areas);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            divlvaptimark.Visible = false;
            divuploadexcel.Visible = false;
            divapti.Visible = false;
            DivViewData.Visible = true;
            ViewData.DataSource = ds;
            ViewData.DataBind();
           
        }
        else
        {
            objCommon.DisplayMessage(this.updapptitutetest, "No Record Found.", this.Page);
            divlvaptimark.Visible = false;
            divuploadexcel.Visible = false;
            divapti.Visible = false;
            ViewData.DataSource = null;
            ViewData.DataBind();
          
        }

    }

 //Aptitude Test Mark Entry End

//Aptitude_Manual_Mark_Enrty Start

    private void FilldropDown()
    {
        objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVE = 1", "BATCHNO DESC");
        objCommon.FillDropDownList(ddlStudyLevel, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0 and UA_SECTION!=4 ", "UA_SECTION");
        objCommon.FillDropDownList(ddlCampus, "ACD_CAMPUS", "CAMPUSNO", "CAMPUSNAME", "CAMPUSNO>0", "CAMPUSNAME");
        objCommon.FillListBox(lstbxDiscipline, "ACD_AREA_OF_INTEREST", "AREA_INT_NO", "AREA_INT_NAME", "", "AREA_INT_NO");

    }
    private void binddata()
    {
        try
        {
            string area = "";
            string Areas = string.Empty;

            foreach (ListItem item in lstbxDiscipline.Items)
            {
                if (item.Selected == true)
                {
                    area += item.Value + ',';
                }
            }
            if (!string.IsNullOrEmpty(area))
            {
                area = area.Substring(0, area.Length - 1);
            }
            else
            {
                area = "0";
            }
            DataSet dsfee = null;

            // ADDED BY NARESH BEERLA ON 15022022
            //   bool StudType = rbtnPending.Checked = true ? false : true;
            //int subType = Convert.ToInt32(StudType);
            //if (rbtnCompleted.Checked)
            //    subType = 1;
            // ADDED BY NARESH BEERLA ON 15022022


            // ADDED BY NARESH BEERLA ON 15022022
            ///bool StudType = rbtnPending.Checked = true ? false : true;
            int subType = -1;// Convert.ToInt32(StudType);
            //if (rbtnCompleted.Checked)
            subType = Convert.ToInt32(ddlMarkStatus.SelectedValue);
            // ADDED BY NARESH BEERLA ON 15022022
              dsfee = objIDC.GETAPTIMANUALMARK(Convert.ToInt32(ddlIntake.SelectedValue), area, Convert.ToInt32(ddlStudyLevel.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), subType);      // ADDED subType BY NARESH BEERLA ON 15022022
            if (dsfee.Tables[0].Rows.Count > 0)
            {
               
               // DivViewData.Visible = false;
                divlvaptimark.Visible = true;
                btnSubmit.Visible = true;
                lvMark.DataSource = dsfee;
                lvMark.DataBind();
                lblTotalCount.Text = dsfee.Tables[0].Rows.Count.ToString();
                lblTotalCount.Visible = true;
                int AllCount = 0;
                int PendingCount = 0;
                int CompleteCount = 0;
                foreach (ListViewDataItem dataitem in lvMark.Items)
                {
                    AllCount++;
                    TextBox txtTotal = dataitem.FindControl("txtTotal") as TextBox;
                    DivCountAll.Visible = true;
                    DivCountPending.Visible = true;
                    DivCountComplete.Visible = true;
                    if (txtTotal.Text == "" || txtTotal.Text==null)
                    {
                         PendingCount++;
                    }
                    else 
                    {
                        CompleteCount++;
                    }
                   
                }
                lblCountAll.Text = AllCount.ToString();
                lblCountPending.Text = PendingCount.ToString();
                lblCountCompleted.Text = CompleteCount.ToString();
            }               
            else
            {
               // DivViewData.Visible = false;
                divlvaptimark.Visible = false;
                objCommon.DisplayMessage(this.updaptimark, "No Record Found.", this.Page);
                btnSubmit.Visible = false;
                lvMark.DataSource = null;
                lvMark.DataBind();
                lblTotalCount.Text = "0";
                lblTotalCount.Visible = false;

                DivCountAll.Visible = false;
                DivCountPending.Visible = false;
                DivCountComplete.Visible = false;
            }           
        }
        catch
        {
        }

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        binddata();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string oldEng = "", oldGen = "", oldTotal_marks = "";
            string username = "", name = "", intake = "", program = "", UANO = "", Upd_uano = "", date="",Campus="",EmailId="",NIC="";
            string eng_marks = "", general_marks = "", total = "",areaofinterest = "";
          
            string area = "";

            foreach (ListItem item in lstbxDiscipline.Items)
            {
                if (item.Selected == true)
                {
                    area += item.Value + ',';
                }
            }
            if (!string.IsNullOrEmpty(area))
            {
                area = area.Substring(0, area.Length - 1);
            }
            else
            {
                area = "0";
            }

            CustomStatus cs = 0;
            foreach (ListViewDataItem dataitem in lvMark.Items)
            {
               
                CheckBox chkBox = dataitem.FindControl("chktransfer") as CheckBox;
                HiddenField UserNo = (dataitem.FindControl("hdfUser")) as HiddenField;
                TextBox txteng = dataitem.FindControl("txtEnglish") as TextBox;
                TextBox txtgen = dataitem.FindControl("txtGeneral") as TextBox;
                TextBox txttotal = dataitem.FindControl("txtTotal") as TextBox;
                Label lblusername = dataitem.FindControl("lblusername") as Label;
                Label lblsnrno = dataitem.FindControl("snrno") as Label;
                Label lblname = dataitem.FindControl("lblname") as Label;
                Label lblpgm = dataitem.FindControl("lblpgm") as Label;
                Label lblintake = dataitem.FindControl("lblintake") as Label;
                Label lblEmail = dataitem.FindControl("lblEmail") as Label;
                Label lblNic = dataitem.FindControl("lblNic") as Label;

                HiddenField hdfAreaInterest = dataitem.FindControl("hdfPgm") as HiddenField; // ADDED BY NARESH BEERLA ON 15022022
               
                if (chkBox.Checked == true)
                {

                   
                  string  usernameS = lblusername.Text;
                  
                    if (txteng.Text == "" && txtgen.Text == "" && txttotal.Text == "")
                    {
                        objCommon.DisplayMessage(this.updaptimark, "Please enter details", this.Page);
                        goto noresult;
                    }

                    DataSet ds = objCommon.FillDropDown("ACD_APPTITUDE_TEST_MARK_ENTRY", "USERNO", "ISNULL(ENGLISH_MARKS,0)ENGLISH_MARKS,ISNULL(GENERAL,0)GENERAL,ISNULL(TOTAL_MARKS,0)TOTAL_MARKS,USERNO,UANO",
                        "USERNO='" + usernameS + "' AND ISNULL(CANCEL,0)='" + 0 + "'", string.Empty);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                       

                        oldEng += Convert.ToInt32((ds.Tables[0].Rows[0]["ENGLISH_MARKS"].ToString().Trim())) + "$";
                        oldGen += Convert.ToDecimal(ds.Tables[0].Rows[0]["GENERAL"].ToString().Trim()) + "$";
                        oldTotal_marks += Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_MARKS"].ToString().Trim()) + "$";
                    
                    }
                   
                    UANO +=  (Session["uano"]) + "$";
                    username += Convert.ToString(lblusername.Text) + "$";
                    name += Convert.ToString(lblname.Text) + "$";
                    intake += Convert.ToString(ddlIntake.SelectedValue) + "$";
                    //EmailId += Convert.ToString(lblEmail.Text) + "$";
                    //NIC += Convert.ToString(lblNic.Text) + "$";
                    program += Convert.ToString(lblpgm.Text) + "$";
                    Campus += Convert.ToInt32(ddlCampus.SelectedValue) + "$";
                    areaofinterest += hdfAreaInterest.Value.ToString() + '$';
                    eng_marks += Convert.ToDecimal(txteng.Text) + "$";
                    general_marks += Convert.ToDecimal(txtgen.Text) + "$";
                    total += Convert.ToDecimal(txttotal.Text) + "$";
                    date += DateTime.Now.ToString("dd/MM/yyyy") + "$";
                    Upd_uano += (Session["uano"]) + "$";
               
                 // cs = (CustomStatus)objIDC.INSERTAPTIMANULMARKS(username, Convert.ToInt32(ddlIntake.SelectedValue), name, area, total, general_marks, eng_marks, Convert.ToInt32(Session["uano"]), oldEnglish, oldGeneral, oldTotal);
                }

            }
            username = username.TrimEnd('$');
            name = name.TrimEnd('$');
            intake = intake.TrimEnd('$');
            UANO = UANO.TrimEnd('$');
            //EmailId = EmailId.TrimEnd('$');
            //NIC = NIC.TrimEnd('$');
            program = program.TrimEnd('$');
            eng_marks = eng_marks.TrimEnd('$');
            general_marks = general_marks.TrimEnd('$');
            total = total.TrimEnd('$');
            areaofinterest = areaofinterest.TrimEnd('$');
            oldEng = oldEng.TrimEnd('$');
            oldGen = oldGen.TrimEnd('$');
            oldTotal_marks = oldTotal_marks.TrimEnd('$');
            Upd_uano = Upd_uano.TrimEnd('$');
            date = date.TrimEnd('$');
            Campus = Campus.TrimEnd('$');

           // cs = (CustomStatus)objIDC.INSERTAPTIMANULMARKS(username, intake, name, area, total, general_marks, eng_marks,UANO, oldEng, oldGen, oldTotal_marks);

            //cs = (CustomStatus)objIDC.INSERTAPTIMANULMARKS(username, intake, name, area, total, general_marks, eng_marks, UANO, oldEng, oldGen, oldTotal_marks, Upd_uano, date, Campus, EmailId,NIC);
            cs = (CustomStatus)objIDC.INSERTAPTIMANULMARKS(username, intake, name, areaofinterest, total, general_marks, eng_marks, UANO, oldEng, oldGen, oldTotal_marks, Upd_uano, date, Campus);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                binddata();

                objCommon.DisplayMessage(this.updaptimark, "Record saved successfully.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updaptimark, "Please Select At least On Checkbox", this.Page);
            }

        noresult:
            { }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Offered_Course.btnAd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlIntake.SelectedIndex = 0;
        ddlMarkStatus.SelectedValue = null;
        lstbxDiscipline.SelectedValue = null;
        ddlStudyLevel.SelectedIndex = 0;
        ddlCampus.SelectedIndex = 0;
        divlvaptimark.Visible = false;
        btnSubmit.Visible = false;
        lvMark.DataSource = null;
        lvMark.DataBind();
        ViewData.DataSource = null;
        ViewData.DataBind();
    }

    
} 
    