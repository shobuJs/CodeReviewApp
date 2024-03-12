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

public partial class ACADEMIC_AcademicStaff : System.Web.UI.Page
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
            if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                this.CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }

            }
            
            ViewState["action"] = "add";
            objCommon.SetLabelData("0");//for label
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header

            excellist();
        }
      
    }
    protected void excellist()
    {
        DataSet ds = objIDC.excelsheet();
        divexcelsheet.Visible = true;
        lvexcelsheet.DataSource = ds;
        lvexcelsheet.DataBind();
        objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvexcelsheet);//Set label 
        objCommon.SetListViewHeaderLabel(Convert.ToString(Request.QueryString["pageno"]), lvexcelsheet);

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Grade.aspx");
            }
        }
        else
        {
           Response.Redirect("~/notauthorized.aspx?page=Grade.aspx");
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string path = Server.MapPath("~/ExcelData/");
            if (FileUpload1.HasFile)
            {
                string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

                if (extension.Contains(".xls") || extension.Contains(".xlsx"))
                {
                    if (!(Directory.Exists(MapPath("~/PresentationLayer/ExcelData"))))
                        Directory.CreateDirectory(path);

                    string fileName = FileUpload1.PostedFile.FileName.Trim();                   
                    if (File.Exists((path + fileName).ToString()))
                        File.Delete((path + fileName).ToString());                   
                    FileUpload1.SaveAs(path + fileName);                  
                    CheckFormatAndImport(extension, path + fileName);
                }
                else
                {
                    lblTotalMsg.Text = "Only excel sheet is allowed to upload";
                    objCommon.DisplayMessage(this.updAcademicStaff, "Only excel sheet is allowed to upload.", this.Page);
                    return;
                }
            }
            else
            {
                lblTotalMsg.Text = "Please select file to upload.";
                objCommon.DisplayMessage(this.updAcademicStaff, "Please select file to upload.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private string GeneartePassword()
    {
        string allowedChars = "";   
     
       // allowedChars += "Slit@123";
        allowedChars += "s,l,i,t";

        char[] sep = { ',' };

        string[] arr = allowedChars.Split(sep);

        string passwordString = "";

        string temp = "";

        Random rand = new Random();

        for (int i = 0; i < 6; i++)
        {
            temp = arr[rand.Next(0, arr.Length)];
            passwordString += temp;
        }
        return passwordString;
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
        objCommon.DisplayMessage(this.updAcademicStaff, conString, this.Page);
        try
        {
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();

                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelData = new DataTable();      
                 
                dtExcelData.Columns.AddRange(new DataColumn[] {
                new DataColumn("UserName", typeof(string)),
                new DataColumn("EmployeeName", typeof(string)),
                new DataColumn("EmployeeCode", typeof(string)),
                new DataColumn("Email", typeof(string)),
                new DataColumn("Department", typeof(string)),
                new DataColumn("MobileNo", typeof(string)),
                //new DataColumn("Lic", typeof(string)),
                new DataColumn("Designation", typeof(string)),   
                });

                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                {
                    oda.Fill(dtExcelData);
                    DataSet ds1 = new DataSet();
                    ds1.Tables.Add(dtExcelData);
                    divexcelsheet.Visible = true;
                    excellist();
                    //lvExcel.DataSource = dtExcelData;
                    //lvExcel.DataBind();
                  
                    btnCancel.Visible = true;
                   foreach (DataTable table in ds1.Tables)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            foreach (DataRow dr in table.Rows)
                            {
                                if (dr["EmployeeName"].ToString() == string.Empty)
                                {
                                    objCommon.DisplayMessage( "Cant Pass Null Records in Excel Sheet!!", this);
                                    dr.Delete();
                                    table.AcceptChanges();
                                    break;
                                    
                                }
                            }
                        }
                    }                
                }
                excel_con.Close();
            
                objIDM.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
                //foreach (DataRow row in dtExcelData.Rows)
                //{
                //    string email = row["Email"].ToString();
                //    string mobileno = row["MobileNo"].ToString();

                //    int EmailExist = Convert.ToInt32(objCommon.LookUp("USER_ACC", "COUNT(*)", "UA_EMAIL=" + email + ""));
                //    if (EmailExist > 0)
                //    {
                //        objCommon.DisplayMessage(updAcademicStaff, "Email Already Exist!", this.Page);
                //        lblTotalMsg.Text = "Email Already Exist!";
                //        break;
                //    }
                //}
                string pwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(GeneartePassword());

                int ret = objIDC.ImportDataForEmployee(dtExcelData, pwd, Convert.ToInt32(Session["usertype"].ToString()));                   
                if (ret == 1)
                {
                    lvexcelsheet.DataSource = dtExcelData;
                    divexcelsheet.Visible = true;
                    excellist();
                    //lvExcel.DataBind();
                    //divlvExcel.Visible = true;
                    btnCancel.Visible = true;
                    objCommon.DisplayMessage("Data Upload Successfully!!", this);
                    lblTotalMsg.Text = "Records uploaded " ;
                    lblTotalMsg.Style.Add("color", "green");
                    return true;
                }
                else if (ret == 99)
                {
                    objCommon.DisplayMessage("Email Or Mobile Number Already Exist!!", this);
                    return false;
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
            objCommon.DisplayMessage( "Record not uploaded, file is not in correct format. Please check the format of file & upload again.", this);          
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Records not saved. Please check whether the file is in correct format or not. ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(this.Page, "Server UnAvailable");
            return false;
        }
    }

    //private void BindGrid(DataTable dtExcelData)
    //{
       
    //    DataTable dt = new DataTable();
    //    DataRow dr;

    
    //    dt.Columns.Add(new System.Data.DataColumn("Column1", typeof(String)));
    //    dt.Columns.Add(new System.Data.DataColumn("Column2", typeof(String)));
    //    dt.Columns.Add(new System.Data.DataColumn("Column3", typeof(String)));
    //    dt.Columns.Add(new System.Data.DataColumn("Column4", typeof(String)));
    //    dt.Columns.Add(new System.Data.DataColumn("Column5", typeof(String)));
    //    dt.Columns.Add(new System.Data.DataColumn("Column6", typeof(String)));
    //    dt.Columns.Add(new System.Data.DataColumn("Column7", typeof(String)));s


    //    int rowCount = dtExcelData.Rows.Count;
    //    if (rowCount > 0)
    //    {
    //        for (int i = 0; i < rowCount; i++)
    //        {
    //            dr = dt.NewRow();
    //            dr[0] = dtExcelData.Rows[i]["UserName"].ToString();
    //            dr[1] = dtExcelData.Rows[i]["EmployeeName"].ToString();
    //            dr[2] = dtExcelData.Rows[i]["EmployeeCode"].ToString();
    //            dr[3] = dtExcelData.Rows[i]["Email"].ToString();
    //            dr[4] = dtExcelData.Rows[i]["Department"].ToString();
    //            dr[5] = dtExcelData.Rows[i]["MobileNo"].ToString();
    //            dr[6] = dtExcelData.Rows[i]["Lic"].ToString();
              
    //            dt.Rows.Add(dr);
    //        }
    //    }

    //    lvExcel.DataSource = dt;
    //    lvExcel.DataBind();
    //}
    protected void lbExcelFormat_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/vnd.ms-excel";
        string path = Server.MapPath("~/ExcelFormat/Preformat_Of_Employe_ entry_data_sheet.xls");      
        Response.AddHeader("Content-Disposition", "attachment;filename=\"Preformat_Of_Employe_ entry_data_sheet.xls\"");
        Response.TransmitFile(path);
        Response.End();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divexcelsheet.Visible = false;
        lvexcelsheet.DataSource = null;
        lvexcelsheet.DataBind();
        FileUpload1.Attributes.Clear();
        Response.Redirect(Request.Url.ToString());
    }
    

}