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

using EASendMail;

public partial class ACADEMIC_SignUpExcel : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ImportDataMaster objIDM = new ImportDataMaster();
    ImportDataController objIDC = new ImportDataController();

    UserController objUC = new UserController();
   
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
            objCommon.FillDropDownList(ddlcurreentintake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO desc");
            //excellist();
        }

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
    
    protected void excellist()
    {
        DataSet ds = objIDC.excelsheetforsignup(Convert.ToInt32(ddlcurreentintake.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            divexcelsheet.Visible = true;
            btnemail.Visible = true;
            lvexcelsheet.DataSource = ds;
            lvexcelsheet.DataBind();
        }
        else
        {
            divexcelsheet.Visible = true;
            btnemail.Visible = false;
            lvexcelsheet.DataSource = ds;
            lvexcelsheet.DataBind();
        }

    }
    protected void lbExcelFormat_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/vnd.ms-excel";
        string path = Server.MapPath("~/ExcelFormat/Preformat_Of_SignUp_ entry_data_sheet.xls");
        Response.AddHeader("Content-Disposition", "attachment;filename=\"Preformat_Of_SignUp_ entry_data_sheet.xls\"");
        Response.TransmitFile(path);
        Response.End();
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
                    objCommon.DisplayMessage(this.updSignupexcel, "Only excel sheet is allowed to upload.", this.Page);
                    return;
                }
            }
            else
            {
                lblTotalMsg.Text = "Please select file to upload.";
                objCommon.DisplayMessage(this.updSignupexcel, "Please select file to upload.", this.Page);
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

         allowedChars += "Slit@123";
        //allowedChars += "s,l,i,t";

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
        objCommon.DisplayMessage(this.updSignupexcel, conString, this.Page);
        try
        {
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();

                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelData = new DataTable();

                dtExcelData.Columns.AddRange(new DataColumn[] {
                new DataColumn("FIRSTNAME", typeof(string)),
                new DataColumn("LASTNAME", typeof(string)),
                new DataColumn("EMAILID", typeof(string)),
                new DataColumn("MOBILENO", typeof(string)), 
                new DataColumn("DATEOFBIRTH", typeof(DateTime)),
                new DataColumn("GENDER", typeof(string)),
                new DataColumn("STUDYLEVEL", typeof(string)),           
                new DataColumn("PROGRAMINTRESTS", typeof(string)),
                new DataColumn("NIC", typeof(string)),
                new DataColumn("ALINDEXNUMBER", typeof(string)),
                
                //new DataColumn("COLLEGE_ID", typeof(string)),
                //new DataColumn("DEGREENO", typeof(string)),
                //new DataColumn("BRANCHNO", typeof(string)), 
              
                });


                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                {
                    oda.Fill(dtExcelData);
                    DataSet ds1 = new DataSet();
                    ds1.Tables.Add(dtExcelData);
                    divexcelsheet.Visible = true;
                    excellist();
                    btnCancel.Visible = true;

                    foreach (DataTable table in ds1.Tables)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            foreach (DataRow dr in table.Rows)
                            {
                                
                                //dtExcelData = dr["PROGRAMINTRESTS"].ToString();
                                //string[] Split = table.Rows.Count.Split(',');
                                //dr["COLLEGE_ID"] = Split[0];
                                //dr["DEGREENO"] = Split[1];
                                //dr["BRANCHNO"] = Split[2];
                                //dtExcelData.Rows.Add(dr);

                                if (dr["FIRSTNAME"].ToString() == string.Empty)
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

                objIDM.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
                //string pwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(GeneartePassword());
               
                string pwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt("Sliit@123");
                int ret = objIDC.ImportDataSignupExcel(dtExcelData, 1, pwd,Convert.ToInt32(ddlcurreentintake.SelectedValue));
                if (ret == 1)
                {
                   // this.TransferToEmail();
                    lvexcelsheet.DataSource = dtExcelData;
                    excellist();
                    divexcelsheet.Visible = true;
                    btnCancel.Visible = true;
                    objCommon.DisplayMessage("Data Upload Successfully!!", this);
                    lblTotalMsg.Text = "Records Uploaded ";
                    lblTotalMsg.Style.Add("color", "green");
                    return true;
                }
                else if (ret == 2)
                {
                    objCommon.DisplayMessage("Email Id Already Exist!!", this);
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
            objCommon.DisplayMessage("Record not uploaded, file is not in correct format. Please check the format of file & upload again.", this);
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Records not saved. Please check whether the file is in correct format or not. ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(this.Page, "Server UnAvailable");
            return false;
        }
    }
   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divexcelsheet.Visible = false;
        lvexcelsheet.DataSource = null;
        lvexcelsheet.DataBind();
        FileUpload1.Attributes.Clear();
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlcurreentintake_SelectedIndexChanged(object sender, EventArgs e)
    {
        excellist();
    }
    protected void btnemail_Click(object sender, EventArgs e)
    {
        try
        {

            string studentIds = string.Empty; string filename = ""; int count = 0;
            string password = "";
            password = "Sliit@123";

            string folderPath = Server.MapPath("~/EmailUploadFile/");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            Session["EmailFileAttachemnt"] = fuAttachFile.FileName;
            if (Session["EmailFileAttachemnt"] != string.Empty || Session["EmailFileAttachemnt"] != "")
            {
                fuAttachFile.SaveAs(folderPath + Path.GetFileName(fuAttachFile.FileName));
            }

            DataSet dsUserContact = null;
            string message = "";
            string subject = "";
          
            dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", "ERP",2872);

            foreach (ListViewDataItem item in lvexcelsheet.Items)
            {
                CheckBox chk = item.FindControl("chkemail") as CheckBox;
                Label hdfAppli = item.FindControl("lblusername") as Label;
                Label hdfEmailid = item.FindControl("lblemail") as Label;
                Label hdfirstname = item.FindControl("lblname") as Label;
               
                if (chk.Checked == true)
                {
                    count++;                
                    message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                    subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                    filename = Convert.ToString(Session["EmailFileAttachemnt"]);

                    Execute(message, hdfEmailid.Text, subject, hdfirstname.Text, hdfAppli.Text, filename, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"]), password).Wait();
                    chk.Checked = false;
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(updSignupexcel, "Please select atleast one check box for email send !!!", this.Page);
                txtEmailMessage.Text = "";
                txtEmailSubject.Text = "";
                //foreach (ListViewDataItem item in lvexcelsheet.Items)
                //{
                //    Label lblmeg = item.FindControl("lblmeg") as Label;
                //    lblmeg.Text = "No";
                //}
                Session["EmailFileAttachemnt"] = null;
                return;
            }
            else
            {
                objCommon.DisplayMessage(updSignupexcel, "Email send Successfully !!!", this.Page);
                //foreach (ListViewDataItem item in lvexcelsheet.Items)
                //{
                //    Label lblmeg = item.FindControl("lblmeg") as Label;
                //    lblmeg.Text = "yess";
                //}
                txtEmailMessage.Text = "";
                txtEmailSubject.Text = "";
                Session["EmailFileAttachemnt"] = null;
            }
        }
        catch (Exception ex)
        {

        }
    }
    static async Task Execute(string message, string toSendAddress, string Subject, string firstname, string username, string filename, string sendemail, string emailpass, string password)
    {
        try
        {
            SmtpMail oMail = new SmtpMail("TryIt");

            oMail.From = sendemail;

            oMail.To = toSendAddress;

            oMail.Subject = Subject;

            oMail.HtmlBody = message;

            oMail.HtmlBody = oMail.HtmlBody.Replace("[USERFIRSTNAME]", firstname.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[USERNAME]", username.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[EMAILID]", toSendAddress.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[PASSWORD]", password.ToString());
            if (filename != string.Empty)
            {
                oMail.HtmlBody = System.Web.HttpContext.Current.Server.MapPath("~/EmailUploadFile/" + filename + "");

            }
           // SmtpServer oServer = new SmtpServer("smtp-mail.outlook.com");
            SmtpServer oServer = new SmtpServer("smtp.office365.com");
            oServer.User = sendemail;
            oServer.Password = emailpass;

            oServer.Port = 587;

            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();

            oSmtp.SendMail(oServer, oMail);


        }
        catch (Exception ex)
        {
            throw;
        }
    }
}