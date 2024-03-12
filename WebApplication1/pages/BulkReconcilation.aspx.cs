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
using DynamicAL_v2;
using _NVP;
using System.Collections.Specialized;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Xml;
using System.Xml.Serialization;
using RestSharp;


public partial class ACADEMIC_BulkReconcilation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MappingController objmp = new MappingController();
    UserController user = new UserController();

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
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            ViewState["action"] = "add";
            objCommon.FillDropDownList(ddlintake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlEnrollIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlintakesem, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0", "SESSIONNO DESC");
            objCommon.SetLabelData("0");//for label
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header           
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
        objCommon.DisplayMessage(this.updbulreco, conString, this.Page);
        try
        {
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelData = new DataTable();
                dtExcelData.Columns.AddRange(new DataColumn[] {
                new DataColumn("transactionDate", typeof(string)),
                new DataColumn("Payment", typeof(string)),
                new DataColumn("StudentID", typeof(string)),
                new DataColumn("BankName", typeof(string)),
                });
                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                {
                    oda.Fill(dtExcelData);
                    DataSet ds1 = new DataSet();
                    ds1.Tables.Add(dtExcelData);
                    //divexcelsheet.Visible = true;                
                    btnCancel.Visible = true;
                    foreach (DataTable table in ds1.Tables)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            foreach (DataRow dr in table.Rows)
                            {
                                if (dr["StudentID"].ToString() == string.Empty)
                                {
                                    objCommon.DisplayMessage("Cant Pass Null StudentID in Excel Sheet!!", this);
                                    dr.Delete();
                                    table.AcceptChanges();
                                    break;
                                }
                            }
                        }
                    }
                }
                excel_con.Close();
                RandomFunction();
                int ret = objmp.ImportDataForreconcilation(dtExcelData, Convert.ToInt32(ddlintake.SelectedValue), Convert.ToString(FileUpload1.PostedFile.FileName.ToString()), Convert.ToString(hdfAdmission.Value),Convert.ToInt32(Session["userno"]));
                if (ret == 1)
                {
                    binddata();                 
                    btnCancel.Visible = true;
                    objCommon.DisplayMessage("Data Upload Successfully!!", this);
                    divconfirmed.Visible = false;
                    //lblTotalMsg.Text = "Records uploaded ";
                    //lblTotalMsg.Style.Add("color", "green");
                    return true;
                }           
                else
                {
                    objCommon.DisplayMessage("Record not uploaded, file is not in correct format. Please check the format of file & upload again.", this);                   
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
    private void RandomFunction()
    {
        Random rnd = new Random();
        int ir = rnd.Next(01, 99999901);
        // lblOrderID.Text = Convert.ToString(Convert.ToInt32(Session["idno"]) + Convert.ToInt32(ViewState["SESSIONNO"]) + Convert.ToInt32(ViewState["semesterno"]) + ir);

        hdfAdmission.Value = Convert.ToString(ir);
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string path = Server.MapPath("~/ExcelData/");
            if (Convert.ToString(FileUpload1.PostedFile.FileName) != string.Empty)
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
                    objCommon.DisplayMessage(this.updbulreco, "Only excel sheet is allowed to upload.", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updbulreco, "Please select file to upload.", this.Page);
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        FileUpload1.Attributes.Clear();
        Response.Redirect(Request.Url.ToString());
        ddlintake.SelectedIndex = 0;
    }
    private void binddata()
    {
        try
        {
            DataSet ds = null;
            ds = objmp.GetReconuploadeddata(Convert.ToString(FileUpload1.PostedFile.FileName.ToString()), Convert.ToString(hdfAdmission.Value));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                uploadapp.Visible = true;
                LvTempRec.DataSource = ds.Tables[0];
                LvTempRec.DataBind();
               
                if (ds.Tables[1] != null)
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            {
                                foreach (ListViewDataItem item in LvTempRec.Items)
                                {
                       
                                    Label lblUserName = (Label)item.FindControl("lblUsername");
                                    Label lblIntake = (Label)item.FindControl("lblIntake");
                                    Label lblRemark = (Label)item.FindControl("lblRemark");
                                    CheckBox chk = (CheckBox)item.FindControl("chktransfer");
                                    if ((lblUserName.Text == ds.Tables[1].Rows[i]["USERNAME"].ToString() && lblIntake.Text == ds.Tables[1].Rows[i]["INTAKE"].ToString()))
                                    {
                                        lblRemark.Text = "Record Already Exists";
                                        chk.Enabled = false;
                                        if (ds.Tables[1].Rows[i]["USERNAME"].ToString() == null)
                                        {
                                            chk.Enabled = false;
                                            chk.Checked = false;
                                        }
                                    }
                                }
                            }
                       }
                }
            }
            else
            {
                uploadapp.Visible = false;
                LvTempRec.DataSource = null;
                LvTempRec.DataBind();
            }
        }
        catch (Exception ex)
        { 
        
        }
    }
    private void binddataforenroll()
    {
        try
        {
            DataSet ds = null;
            ds = objmp.GetReconuploadeddataforenroll(Convert.ToString(updEnrollUpload.PostedFile.FileName.ToString()), Convert.ToString(hdfAdmission.Value));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                uploadenroll.Visible = true;
                LvTempEnroll.DataSource = ds.Tables[0];
                LvTempEnroll.DataBind();
               
                if (ds.Tables[1] != null)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            foreach (ListViewDataItem item in LvTempEnroll.Items)
                            {

                                Label lblUserName = (Label)item.FindControl("lblUsername");
                                Label lblIntake = (Label)item.FindControl("lblIntake");
                                Label lblRemark = (Label)item.FindControl("lblRemark");
                                CheckBox chk = (CheckBox)item.FindControl("chktransfer");
                                if ((lblUserName.Text == ds.Tables[1].Rows[i]["REGNO"].ToString() && lblIntake.Text == ds.Tables[1].Rows[i]["INTAKENO"].ToString()))
                                {
                                    lblRemark.Text = "Record Already Exists";
                                    chk.Enabled = false;
                                    chk.Checked = false;
                                    //if (ds.Tables[1].Rows[i]["REGNO"].ToString() == null)
                                    //{
                                    //    chk.Enabled = false;
                                    //}

                                }
                            }
                        }
                    }
                }
            }
            else
            {
                uploadenroll.Visible = false;
                LvTempEnroll.DataSource = null;
                LvTempEnroll.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void rdbFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbFilter.SelectedValue == "1")
        {
            //confirmedexcellist();
            //binddata();
            btnDownload.Visible = true;
            intakeone.Visible = true;
            FileUpload.Visible = true;
            buttons.Visible = true;
            btnConfirmed.Visible = true;
            LvTempRec.Visible = true;
            divEnrollment.Visible = false;
            divSemester.Visible = false;
        }
        else if (rdbFilter.SelectedValue == "2")
        {
            btnDownload.Visible = false;
            intakeone.Visible = false;
            FileUpload.Visible = false;
            buttons.Visible = false;
            btnConfirmed.Visible = false;
            LvTempRec.Visible = false;
            divEnrollment.Visible = true;
            divSemester.Visible = false;
            divconfirmed.Visible = false;
        }
        else if (rdbFilter.SelectedValue == "3")
        {
            btnDownload.Visible = false;
            intakeone.Visible = false;
            FileUpload.Visible = false;
            buttons.Visible = false;
            btnConfirmed.Visible = false;
            LvTempRec.Visible = false;
            divEnrollment.Visible = false;
            divSemester.Visible = true;
            divconfirmed.Visible = false;
        }
        else
        {
            intakeone.Visible = false;
            //FileUpload.Visible = false;
            buttons.Visible = true;
            divEnrollment.Visible = false;
        }
    }
    protected void btnConfirmed_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdfAdmission.Value) > 0)
        {
            string username = ""; int count = 0;
            foreach (ListViewDataItem item in LvTempRec.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("chktransfer");
                if (chk.Checked ==true && chk.Enabled == true)
                {
                    count++;
                    username += chk.ToolTip + ',';
                }
            }
            if (count > 0)
            {
                username = username.TrimEnd(',');
                VerifyandRegister(username);
            }
            else
            {
                objCommon.DisplayMessage(updbulreco, "Please Select Atleast One CheckBox !!!", this);
            }
        }
        else
        {
            objCommon.DisplayMessage(updbulreco, "Excel sheet is not uploaded for given selection. First upload excel sheet then Verify and Move.", this);
        }
    }
   
    private void VerifyandRegister(string username)
    {
        try
        {
            DataSet ds = null;
            DataSet dsUserContact = null;
            string message = "";
            string subject = "";
            int retout = 0;
            retout = objmp.VerifyandMovedRecon(Convert.ToInt32(ddlintake.SelectedValue), Convert.ToString(hdfAdmission.Value), Convert.ToString(username));//, out retout
            if (retout > 0)
            {
                objCommon.DisplayMessage(updbulreco, "Records verified successfully", this.Page);
                uploadapp.Visible = false;
                LvTempRec.DataSource = null;
                LvTempRec.DataBind();
                ddlintake.SelectedIndex = 0;
                //binddata();
                foreach (ListViewDataItem dataitem in LvTempRec.Items)
                {
                    CheckBox chkBox = dataitem.FindControl("chktransfer") as CheckBox;
                    HiddenField hdfname = dataitem.FindControl("hdfname") as HiddenField;
                    HiddenField hdfusername = dataitem.FindControl("hdfusername") as HiddenField;
                    HiddenField hdfprogram = dataitem.FindControl("hdfprogram") as HiddenField;
                    HiddenField hdfbatchname = dataitem.FindControl("hdfbatchname") as HiddenField;
                    HiddenField hdffeestype = dataitem.FindControl("hdffeestype") as HiddenField;
                    HiddenField hdfamount = dataitem.FindControl("hdfamount") as HiddenField;
                    HiddenField HiddenFhdfdate = dataitem.FindControl("HiddenFhdfdate") as HiddenField;
                    HiddenField hdfuserno = dataitem.FindControl("hdfuserno") as HiddenField;
                    HiddenField hdfemailid = dataitem.FindControl("hdfemailid") as HiddenField;
                    if (chkBox.Checked == true)
                    {
                        dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(2833));
                        message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                        subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                        string ReffEmail = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]);
                        string ReffPassword = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"]);
                        string matter = "Approved";
                        SmtpMail oMail = new SmtpMail("TryIt");
                        oMail.From = ReffEmail;
                        oMail.To = Convert.ToString(hdfemailid.Value);
                        oMail.Subject = subject;
                        oMail.HtmlBody = message;
                        oMail.HtmlBody = oMail.HtmlBody.Replace("[UA_FULLNAME]", hdfname.Value);
                        oMail.HtmlBody = oMail.HtmlBody.Replace("[MATTER]", matter.ToString());
                        oMail.HtmlBody = oMail.HtmlBody.Replace("[Application ID]", hdfusername.Value);
                        oMail.HtmlBody = oMail.HtmlBody.Replace("[PROGRAM NAMES]", hdfprogram.Value);
                        oMail.HtmlBody = oMail.HtmlBody.Replace("[INTAKE]", hdfbatchname.Value);
                        oMail.HtmlBody = oMail.HtmlBody.Replace("[Payment Mode]", hdffeestype.Value);
                        oMail.HtmlBody = oMail.HtmlBody.Replace("[Payment Amount]", hdfamount.Value);
                        oMail.HtmlBody = oMail.HtmlBody.Replace("[Payment Date]", HiddenFhdfdate.Value);
                        MemoryStream oAttachment1 = ShowGeneralExportReportForMailForApplication("Reports,Academic,FeeReceiptApplication.rpt", "@P_USERNO=" + Convert.ToString(hdfuserno.Value) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "");
                        var bytesRpt = oAttachment1.ToArray();  //File.ReadAllBytes(oAttachment);
                        var fileRpt = Convert.ToBase64String(bytesRpt);
                        byte[] test = (byte[])bytesRpt;
                        oMail.AddAttachment("FeeReceipt.pdf", test);
                        SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022
                        oServer.User = ReffEmail;
                        oServer.Password = ReffPassword;
                        oServer.Port = 587;
                        oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
                        EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
                        oSmtp.SendMail(oServer, oMail);
                    }

                }
            }
            else
            {
                objCommon.DisplayMessage(updbulreco, "Records verified Failed.", this.Page);
            }

        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(updbulreco, "Error occurred.", this.Page);
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Error occurred. ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(this.Page, "Server UnAvailable");
        }
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/vnd.ms-excel";
            string path = Server.MapPath("~/ExcelFormat/Bulk_Reconciliation_Sheet.xls");
            Response.AddHeader("Content-Disposition", "attachment;filename=\"Bulk_Reconciliation_Sheet.xls\"");
            Response.TransmitFile(path);
            Response.End();
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void btnEnrollDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/vnd.ms-excel";
            string path = Server.MapPath("~/ExcelFormat/Bulk_Reconciliation_Sheet_Enrollment.xls");
            Response.AddHeader("Content-Disposition", "attachment;filename=\"Bulk_Reconciliation_Sheet_Enrollment.xls\"");
            Response.TransmitFile(path);
            Response.End();
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void btnEnrollConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            DataSet dsUserContact = null;
            string message = "";
            string subject = "";
            if (Convert.ToInt32(hdfAdmission.Value) > 0)
            {
                string username = ""; int count = 0;
                foreach (ListViewDataItem item in LvTempEnroll.Items)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chktransfer");
                    if (chk.Checked == true && chk.Enabled == true)
                    {
                        count++;
                        username += chk.ToolTip + ',';
                    }
                }
                if (count > 0)
                {
                    username = username.TrimEnd(',');
                    int retout = 0;
                    retout = objmp.VerifyandMovedReconForEnroll(Convert.ToInt32(ddlEnrollIntake.SelectedValue), Convert.ToString(hdfAdmission.Value), Convert.ToString(username));//, out retout
                    if (retout > 0)
                    {

                        objCommon.DisplayMessage(updbulreco, "Records verified successfully", this.Page);
                        uploadenroll.Visible = false;
                        LvTempEnroll.DataSource = null;
                        LvTempEnroll.DataBind();
                        ddlEnrollIntake.SelectedIndex = 0;
                        foreach (ListViewDataItem dataitem in LvTempEnroll.Items)
                        {
                            CheckBox chkBox = dataitem.FindControl("chktransfer") as CheckBox;
                            HiddenField hdfname = dataitem.FindControl("hdfname") as HiddenField;
                            HiddenField hdfusername = dataitem.FindControl("hdfusername") as HiddenField;
                            HiddenField hdfprogram = dataitem.FindControl("hdfprogram") as HiddenField;
                            HiddenField hdfbatchname = dataitem.FindControl("hdfbatchname") as HiddenField;
                            //HiddenField hdffeestype = dataitem.FindControl("hdffeestype") as HiddenField;
                            HiddenField hdfamount = dataitem.FindControl("hdfamount") as HiddenField;
                            HiddenField HiddenFhdfdate = dataitem.FindControl("HiddenFhdfdate") as HiddenField;
                            HiddenField hdfidno = dataitem.FindControl("hdfidno") as HiddenField;
                            HiddenField hdfemailid = dataitem.FindControl("hdfemailid") as HiddenField;
                            if (chkBox.Checked == true)
                            {
                                string dcr = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToString(hdfidno.Value));
                                dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(2627));
                                message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                                subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                                string ReffEmail = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]);
                                string ReffPassword = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"]);
                                string matter = "Approved";
                                SmtpMail oMail = new SmtpMail("TryIt");
                                oMail.From = ReffEmail;
                                oMail.To = Convert.ToString(hdfemailid.Value);
                                oMail.Subject = subject;
                                oMail.HtmlBody = message;
                                oMail.HtmlBody = oMail.HtmlBody.Replace("[UA_FULLNAME]", hdfname.Value);
                                oMail.HtmlBody = oMail.HtmlBody.Replace("[MATTER]", matter.ToString());
                                oMail.HtmlBody = oMail.HtmlBody.Replace("[Application ID]", hdfusername.Value);
                                oMail.HtmlBody = oMail.HtmlBody.Replace("[PROGRAM NAMES]", hdfprogram.Value);
                                oMail.HtmlBody = oMail.HtmlBody.Replace("[INTAKE]", hdfbatchname.Value);
                                //oMail.HtmlBody = oMail.HtmlBody.Replace("[Payment Mode]", hdffeestype.ToString());
                                oMail.HtmlBody = oMail.HtmlBody.Replace("[Payment Amount]", hdfamount.Value);
                                oMail.HtmlBody = oMail.HtmlBody.Replace("[Payment Date]", HiddenFhdfdate.Value);
                                MemoryStream oAttachment1 = ShowGeneralExportReportForMail("Reports,Academic,FeeReceiptEnrollnment.rpt", "@P_DCRNO=" + Convert.ToString(dcr) + ",@P_IDNO=" + Convert.ToString(hdfidno.Value) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "");
                                //MemoryStream oAttachment1 = ShowGeneralExportReportForMail("Reports,Academic,FeeReceiptEnrollnment.rpt", "");                                
                                var bytesRpt = oAttachment1.ToArray();  //File.ReadAllBytes(oAttachment);
                                var fileRpt = Convert.ToBase64String(bytesRpt);
                                byte[] test = (byte[])bytesRpt;
                                oMail.AddAttachment("FeeReceipt.pdf", test);
                                SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022
                                oServer.User = ReffEmail;
                                oServer.Password = ReffPassword;
                                oServer.Port = 587;
                                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
                                EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
                                oSmtp.SendMail(oServer, oMail);
                            }
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updbulreco, "Records verified Fail.", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updbulreco, "Please Select Atleast One CheckBox !!!", this);
                }
            }
            else
            {
                objCommon.DisplayMessage(updbulreco, "Excel sheet is not uploaded for given selection. First upload excel sheet then Verify and Move.", this);
            }
        }
        catch (Exception ex)
        {

        }
    }

 static private MemoryStream ShowGeneralExportReportForMail(string path, string paramString)
    {
        MemoryStream oStream;
        ReportDocument customReport;
        customReport = new ReportDocument();
        string reportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Academic/FeeReceiptEnrollnment.rpt");
        customReport.Load(reportPath);
        char ch = ',';
        string[] val = paramString.Split(ch);
        if (customReport.ParameterFields.Count > 0)
        {
            for (int i = 0; i < val.Length; i++)
            {

                int indexOfEql = val[i].IndexOf('=');
                int indexOfStar = val[i].IndexOf('*');
                string paramName = string.Empty;
                string value = string.Empty;
                string reportName = "MainRpt";
                paramName = val[i].Substring(0, indexOfEql);

                if (indexOfStar > 0)
                {
                    value = val[i].Substring(indexOfEql + 1, ((indexOfStar - 1) - indexOfEql));
                    reportName = val[i].Substring(indexOfStar + 1);
                }
                else
                {
                    value = val[i].Substring(indexOfEql + 1);
                }

                if (reportName == "MainRpt")
                {
                    if (value == "null")
                    {
                        customReport.SetParameterValue(paramName, null);
                    }
                    else
                        customReport.SetParameterValue(paramName, value);
                }
                else
                    customReport.SetParameterValue(paramName, value, reportName);
            }
        }

        ConfigureCrystalReports(customReport);
        for (int i = 0; i < customReport.Subreports.Count; i++)
        {
            ConfigureCrystalReports(customReport.Subreports[i]);
        }

        oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        return oStream;
    }
    protected void btnEnrollCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(Request.Url.ToString());
            ddlEnrollIntake.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnEnrollVerify_Click(object sender, EventArgs e)
    {
        try
        {
            string path = Server.MapPath("~/ExcelData/");
            if (Convert.ToString(updEnrollUpload.PostedFile.FileName) != string.Empty)
            {
                string extension = Path.GetExtension(updEnrollUpload.PostedFile.FileName);

                if (extension.Contains(".xls") || extension.Contains(".xlsx"))
                {
                    if (!(Directory.Exists(MapPath("~/PresentationLayer/ExcelData"))))
                        Directory.CreateDirectory(path);
                    string fileName = updEnrollUpload.PostedFile.FileName.Trim();
                    if (File.Exists((path + fileName).ToString()))
                        File.Delete((path + fileName).ToString());
                    updEnrollUpload.SaveAs(path + fileName);
                    CheckFormatAndImportEnrollment(extension, path + fileName);
                }
                else
                {
                    objCommon.DisplayMessage(this.updbulreco, "Only excel sheet is allowed to upload.", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updbulreco, "Please select file to upload.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }
    private bool CheckFormatAndImportEnrollment(string extension, string excelPath)
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
        objCommon.DisplayMessage(this.updbulreco, conString, this.Page);
        try
        {
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelDataEnroll = new DataTable();
                dtExcelDataEnroll.Columns.AddRange(new DataColumn[] {
                new DataColumn("transactionDate", typeof(string)),
                new DataColumn("Payment", typeof(string)),
                new DataColumn("StudentID", typeof(string)),
                new DataColumn("BankName", typeof(string)),
                });
                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                {
                    oda.Fill(dtExcelDataEnroll);
                    DataSet ds1 = new DataSet();
                    ds1.Tables.Add(dtExcelDataEnroll);
                    //divexcelsheet.Visible = true;                
                    btnCancel.Visible = true;
                    foreach (DataTable table in ds1.Tables)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            foreach (DataRow dr in table.Rows)
                            {
                                if (dr["StudentID"].ToString() == string.Empty)
                                {
                                    objCommon.DisplayMessage("Cant Pass Null StudentID in Excel Sheet!!", this);
                                    dr.Delete();
                                    table.AcceptChanges();
                                    break;
                                }
                            }
                        }
                    }
                }
                excel_con.Close();
                RandomFunction();
                int ret = objmp.ImportDataForreconcilationEnrollment(dtExcelDataEnroll, Convert.ToInt32(ddlEnrollIntake.SelectedValue), Convert.ToString(updEnrollUpload.PostedFile.FileName.ToString()), Convert.ToString(hdfAdmission.Value), Convert.ToInt32(Session["userno"]));
                if (ret == 1)
                {
                    binddataforenroll();
                    btnCancel.Visible = true;
                    objCommon.DisplayMessage("Data Upload Successfully!!", this);
                    divconfirmedenroll.Visible = false;
                    //lblTotalMsg.Text = "Records uploaded ";
                    //lblTotalMsg.Style.Add("color", "green");
                    return true;
                }
                else
                {
                    objCommon.DisplayMessage("Record not uploaded, file is not in correct format. Please check the format of file & upload again.", this);
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
    protected void ddlintake_SelectedIndexChanged(object sender, EventArgs e)
    {
         DataSet ds = null;
         ds = objmp.confirmedexcelfeeslog(Convert.ToInt32(ddlintake.SelectedValue));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                divconfirmed.Visible = true;
                lvConfirmed.DataSource = ds;
                lvConfirmed.DataBind();

            }
            else
            {
                divconfirmed.Visible = false;
                lvConfirmed.DataSource = null;
                lvConfirmed.DataBind();

            }
    }
    protected void ddlEnrollIntake_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = null;
        ds = objmp.confirmedexcelDcr(Convert.ToInt32(ddlEnrollIntake.SelectedValue));
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            divconfirmedenroll.Visible = true;
            LvConfirmeDenroll.DataSource = ds;
            LvConfirmeDenroll.DataBind();

        }
        else
        {
            divconfirmedenroll.Visible = false;
            LvConfirmeDenroll.DataSource = null;
            LvConfirmeDenroll.DataBind();

        }

    }
    protected void btncancelsem_Click(object sender, EventArgs e)
    {
        divSemester.Visible = false;
        Response.Redirect(Request.Url.ToString());

    }
    protected void btnconfirmedsem_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            DataSet dsUserContact = null;
            string message = "";
            string subject = "";
            if (Convert.ToInt32(hdfAdmission.Value) > 0)
            {
                string username = ""; int count = 0;
                foreach (ListViewDataItem item in LvTempSem.Items)
                {
                    Label lblMatchRegNo = (Label)item.FindControl("lblMatchRegNo");
                    if (lblMatchRegNo.Text != string.Empty)
                    {
                        Label lblUsername = (Label)item.FindControl("lblUsername");
                        //CheckBox chk = (CheckBox)item.FindControl("chktransfer");
                        //if (chk.Checked == true && chk.Enabled == true)
                        //{
                        count++;
                        username += lblUsername.ToolTip + ',';
                    }
                    //}
                }
                if (count > 0)
                {
                    username = username.TrimEnd(',');
                    int retout = 0;
                    retout = objmp.VerifyandMovedReconForSem(Convert.ToInt32(ddlintakesem.SelectedValue), Convert.ToString(hdfAdmission.Value), Convert.ToString(username));//, out retout
                    if (retout > 0)
                    {
                        objCommon.DisplayMessage(updbulreco, "Records verified successfully", this.Page);
                        uploadsem.Visible = false;
                        LvTempSem.DataSource = null;
                        LvTempSem.DataBind();
                        BindSemesterRegConfirmList(Convert.ToString(hdfAdmission.Value));
                        
                        foreach (ListViewDataItem dataitem in LvCONFIRMEDsEM.Items)
                        {
                            Label hdfname = dataitem.FindControl("lblname") as Label;
                            Label hdfusername = dataitem.FindControl("lblRegno") as Label;
                            Label hdfprogram = dataitem.FindControl("lblProgram") as Label;
                            HiddenField hdfbatchname = dataitem.FindControl("hdfbatchname") as HiddenField;
                            Label hdfamount = dataitem.FindControl("lblAmount") as Label;
                            Label HiddenFhdfdate = dataitem.FindControl("lblTransactionDate") as Label;
                            HiddenField hdfidno = dataitem.FindControl("hdfidno") as HiddenField;
                            HiddenField hdfemailid = dataitem.FindControl("hdfemailid") as HiddenField;
                            HiddenField hdfDcrNo = dataitem.FindControl("hdfDcrNo") as HiddenField;
                            HiddenField hdfmaindcr = dataitem.FindControl("hdfmaindcr") as HiddenField;
                            //if (chkBox.Checked == true)
                            //{

                            string SP_Name = "PKG_GET_DETAILS_FOR_API";
                            string SP_Parameters = "@P_IDNO,@P_TEMP_DCR_NO";
                            string Call_Values = "" + hdfidno.Value + "," + hdfDcrNo.Value + "";

                            DataSet ds1 = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

                            if (ds1.Tables != null && ds1.Tables[0].Rows.Count > 0)
                            {
                                var clientUrl = new RestClient("http://172.16.68.55:7048/DynamicsNAV80/OData/Company('SLIIT')/studentreceipts?$format=json");
                                clientUrl.Timeout = -1;
                                var request = new RestRequest(Method.POST);
                                request.AddHeader("Authorization", "Basic SU5UVVNFUjpmRGVWK2JOTGFlQlZuZVFZaFNiczBMZ3RpQjdHNWlDZDJxT1B1dS9KVXRzPQ==");
                                request.AddHeader("Content-Type", "application/json");
                                request.AddParameter("application/json", "{\r\n\"Journal_Template\": \"" + ds1.Tables[0].Rows[0]["JOURNAL_TEMPLATE_NAME"].ToString() + "\",\r\n \"Journal_Batch\": \"" + ds1.Tables[0].Rows[0]["JOUNRAL_BATCH"].ToString() + "\",\r\n \"Student_No_Referance\": \"" + ds1.Tables[0].Rows[0]["REGISTRATION_NO"].ToString() + "\",\r\n \"SLR_No\": \"" + ds1.Tables[0].Rows[0]["APPLICATION_NO"].ToString() + "\",\r\n \"Posting_Date\": \"" + ds1.Tables[0].Rows[0]["POSTING_DATE"].ToString() + "\",\r\n \"Document_Type\": \"" + ds1.Tables[0].Rows[0]["DOCUMENT_TYPE"].ToString() + "\",\r\n \"Description\": \"" + ds1.Tables[0].Rows[0]["NAME_WITH_INITIAL"].ToString() + "\",\r\n \"Amount\": \"" + ds1.Tables[0].Rows[0]["AMOUNT"].ToString() + "\",\r\n \"Bal_Account_No\": \"" + ds1.Tables[0].Rows[0]["BAL_ACCOUNT_TYPE"].ToString() + "\",\r\n \"Bank_Deposit_Date\": \"" + ds1.Tables[0].Rows[0]["BANK_DEPOSIT_DATE"].ToString() + "\",\r\n \"Narration\": \"" + ds1.Tables[0].Rows[0]["NARRATION"].ToString() + "\",\r\n \"Reference\": \"" + ds1.Tables[0].Rows[0]["DOCUMENT_NO"].ToString() + "\",\r\n \"Receipt_Type\": \"" + ds1.Tables[0].Rows[0]["RECEIPT_TYPE"].ToString() + "\",\r\n \"External_Document_No\": \"" + ds1.Tables[0].Rows[0]["EXTERNAL_DOCUMENT_NO"].ToString() + "\"\r\n}", RestSharp.ParameterType.RequestBody);
                                IRestResponse response = clientUrl.Execute(request);

                                string parameter = "Journal_Template:" + ds.Tables[0].Rows[0]["JOURNAL_TEMPLATE_NAME"].ToString() + "Journal_Batch:" + ds.Tables[0].Rows[0]["JOUNRAL_BATCH"].ToString() + "Student_No_Referance:" + ds.Tables[0].Rows[0]["REGISTRATION_NO"].ToString() + "SLR_No:" + ds.Tables[0].Rows[0]["APPLICATION_NO"].ToString() + "Posting_Date:" + ds.Tables[0].Rows[0]["POSTING_DATE"].ToString() + "Document_Type:" + ds.Tables[0].Rows[0]["DOCUMENT_TYPE"].ToString() + "Description:" + ds.Tables[0].Rows[0]["NAME_WITH_INITIAL"].ToString() + "Amount:" + ds.Tables[0].Rows[0]["AMOUNT"].ToString() + "Bal_Account_No:" + ds.Tables[0].Rows[0]["BAL_ACCOUNT_TYPE"].ToString() + "Bank_Deposit_Date:" + ds.Tables[0].Rows[0]["BANK_DEPOSIT_DATE"].ToString() + "Narration:" + ds.Tables[0].Rows[0]["NARRATION"].ToString() + "Reference:" + ds.Tables[0].Rows[0]["DOCUMENT_NO"].ToString() + "Receipt_Type:" + ds.Tables[0].Rows[0]["RECEIPT_TYPE"].ToString() + "External_Document_No:" + ds.Tables[0].Rows[0]["EXTERNAL_DOCUMENT_NO"].ToString();

                                string SP_Name1 = "PKG_ACD_RESPONSE_FOR_STUDENT_API";
                                string SP_Parameters1 = "@P_IDNO,@P_RESULT,@P_RESPONCE_VALUE,@P_STATUS,@P_OUTPUT";
                                string Call_Values1 = "" + Convert.ToInt32(hdfidno.Value) + "," + Convert.ToString(response.StatusDescription) + "," + Convert.ToString(parameter) + "," + "Higher Bulk Reconcile" + ",0";
                                string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);

                                //Console.WriteLine(response.Content);
                            }


                            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(1125));
                                message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                                subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                                string ReffEmail = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]);
                                string ReffPassword = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"]);
                                string matter = "Approved";
                                MailMessage msg = new MailMessage();
                                msg.To.Add(new System.Net.Mail.MailAddress(hdfemailid.Value));
                                msg.From = new System.Net.Mail.MailAddress(ReffEmail);
                                msg.Subject = subject;
                                StringBuilder sb = new StringBuilder();
                                msg.Body = message;

                                msg.Body = msg.Body.Replace("[UA_FULLNAME]", hdfname.Text);
                                msg.Body = msg.Body.Replace("[MATTER]", matter.ToString());
                                msg.Body = msg.Body.Replace("[Application ID]", hdfusername.Text);
                                msg.Body = msg.Body.Replace("[PROGRAM NAMES]", hdfprogram.Text);
                                msg.Body = msg.Body.Replace("[INTAKE]", hdfbatchname.Value);
                                msg.Body = msg.Body.Replace("[Payment Amount]", hdfamount.Text);
                                msg.Body = msg.Body.Replace("[Payment Date]", HiddenFhdfdate.Text);

                                MemoryStream oAttachment1 = ShowGeneralExportReportForMail("Reports,Academic,FeeReceiptEnrollnment.rpt", "@P_DCRNO=" + Convert.ToString(hdfmaindcr.Value) + ",@P_IDNO=" + Convert.ToString(hdfidno.Value) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "");
                                var bytesRpt = oAttachment1.ToArray();
                                var fileRpt = Convert.ToBase64String(bytesRpt);
                                byte[] test = (byte[])bytesRpt;
                                //  msg.AddAttachment("Offerletter.pdf", test);     

                                System.Net.Mail.Attachment attachment;

                                attachment = new System.Net.Mail.Attachment(new MemoryStream(test), "FeeReciept.pdf");
                                msg.Attachments.Add(attachment);


                                //sb.AppendLine(message);
                                msg.BodyEncoding = Encoding.UTF8;

                                //msg.Body = Convert.ToString(sb);
                                msg.IsBodyHtml = true;

                                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                                client.UseDefaultCredentials = false;

                                client.Credentials = new System.Net.NetworkCredential(ReffEmail, ReffPassword);

                                client.Port = 587; // You can use Port 25 if 587 is blocked (mine is)
                                client.Host = "smtp-mail.outlook.com"; // "smtp.live.com";
                                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                                //client.TargetName = "STARTTLS/smtp.office365.com";
                                client.EnableSsl = true;
                                try
                                {
                                    client.Send(msg);
                                    //lblText.Text = "Message Sent Succesfully";
                                }
                                catch (Exception ex)
                                {
                                    //lblText.Text = ex.ToString();
                                }
                            //}
                        }

                    }
                    else
                    {
                        objCommon.DisplayMessage(updbulreco, "Records verified Fail.", this.Page);
                    }
                }
                //else
                //{
                //    objCommon.DisplayMessage(updbulreco, "Please Select Atleast One CheckBox !!!", this);
                //}
            }
            else
            {
                objCommon.DisplayMessage(updbulreco, "Excel sheet is not uploaded for given selection. First upload excel sheet then Verify and Move.", this);
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnverifysem_Click(object sender, EventArgs e)
    {
        try
        {
            string path = Server.MapPath("~/ExcelData/");
            if (Convert.ToString(fileuploadsem.PostedFile.FileName) != string.Empty)
            {
                string extension = Path.GetExtension(fileuploadsem.PostedFile.FileName);

                if (extension.Contains(".xls") || extension.Contains(".xlsx"))
                {
                    if (!(Directory.Exists(MapPath("~/PresentationLayer/ExcelData"))))
                        Directory.CreateDirectory(path);
                    string fileName = fileuploadsem.PostedFile.FileName.Trim();
                    if (File.Exists((path + fileName).ToString()))
                        File.Delete((path + fileName).ToString());
                    fileuploadsem.SaveAs(path + fileName);
                    CheckFormatAndImportSemester(extension, path + fileName);
                }
                else
                {
                    objCommon.DisplayMessage(this.updbulreco, "Only excel sheet is allowed to upload.", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updbulreco, "Please select file to upload.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void binddataforsem()
    {
        try
        {
            DataSet ds = null;
            ds = objmp.GetReconuploadeddataforSem(Convert.ToString(ddlintakesem.SelectedValue), Convert.ToString(hdfAdmission.Value));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                uploadsem.Visible = true;
                LvTempSem.DataSource = ds.Tables[0];
                LvTempSem.DataBind();
            }
            else
            {
                uploadsem.Visible = false;
                LvTempSem.DataSource = null;
                LvTempSem.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    private bool CheckFormatAndImportSemester(string extension, string excelPath)
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
        objCommon.DisplayMessage(this.updbulreco, conString, this.Page);
        try
        {
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelDataEnroll = new DataTable();
                dtExcelDataEnroll.Columns.AddRange(new DataColumn[] {
                new DataColumn("transactionDate", typeof(string)),
                new DataColumn("Payment", typeof(string)),
                new DataColumn("StudentID", typeof(string)),
                new DataColumn("BankName", typeof(string)),
                new DataColumn("Semester", typeof(string)),
                });
                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                {
                    oda.Fill(dtExcelDataEnroll);
                    DataSet ds1 = new DataSet();
                    ds1.Tables.Add(dtExcelDataEnroll);                            
                    btnCancel.Visible = true;
                    foreach (DataTable table in ds1.Tables)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            foreach (DataRow dr in table.Rows)
                            {
                                if (dr["StudentID"] == DBNull.Value && dr["transactionDate"] == DBNull.Value && dr["Payment"] == DBNull.Value && dr["BankName"] == DBNull.Value && dr["Semester"] == DBNull.Value)
                                {
                                    dr.Delete();
                                    table.AcceptChanges();
                                    break;
                                }
                                else if (dr["StudentID"].ToString() == string.Empty)
                                {
                                    objCommon.DisplayMessage("Cant Pass Null StudentID in Excel Sheet!!", this);
                                    dr.Delete();
                                    table.AcceptChanges();
                                    break;
                                }
                            }
                        }
                    }
                }
                excel_con.Close();
                RandomFunction();
                int ret = objmp.ImportDataForreconcilationSemester(dtExcelDataEnroll, Convert.ToInt32(ddlintakesem.SelectedValue), Convert.ToString(fileuploadsem.PostedFile.FileName.ToString()), Convert.ToString(hdfAdmission.Value), Convert.ToInt32(Session["userno"]));
                if (ret == 1)
                {
                    binddataforsem();
                    btnCancel.Visible = true;
                    objCommon.DisplayMessage("Data Upload Successfully!!", this);
                    divconfirmedSem.Visible = false;
                    return true;
                }
                else
                {
                    objCommon.DisplayMessage("Record not uploaded, file is not in correct format. Please check the format of file & upload again.", this);
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
    protected void btndownloadsem_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/vnd.ms-excel";
            string path = Server.MapPath("~/ExcelFormat/Bulk_recon_Semester.xlsx");
            Response.AddHeader("Content-Disposition", "attachment;filename=\"Bulk_recon_Semester.xlsx\"");
            Response.TransmitFile(path);
            Response.End();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlintakesem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
             DataSet ds = null;
            ds = objmp.confirmedexcelDcrSem(Convert.ToInt32(ddlintakesem.SelectedValue),string.Empty,1);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                divconfirmedSem.Visible = true;
                LvCONFIRMEDsEM.DataSource = ds;
                LvCONFIRMEDsEM.DataBind();

            }
            else
            {
                divconfirmedSem.Visible = false;
                LvCONFIRMEDsEM.DataSource = null;
                LvCONFIRMEDsEM.DataBind();

            }
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void BindSemesterRegConfirmList(string randomno)
    {
        try
        {
            DataSet ds = null;
            ds = objmp.confirmedexcelDcrSem(Convert.ToInt32(ddlintakesem.SelectedValue),randomno,2);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                divconfirmedSem.Visible = true;
                LvCONFIRMEDsEM.DataSource = ds;
                LvCONFIRMEDsEM.DataBind();

            }
            else
            {
                divconfirmedSem.Visible = false;
                LvCONFIRMEDsEM.DataSource = null;
                LvCONFIRMEDsEM.DataBind();

            }
            ddlintakesem.SelectedIndex = 0;
        }
        catch (Exception ex)
        { 
        
        }
    }
    static private MemoryStream ShowGeneralExportReportForMailForApplication(string path, string paramString)
    {
        MemoryStream oStream;
        ReportDocument customReport;
        customReport = new ReportDocument();
        string reportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Academic/FeeReceiptApplication.rpt");
        customReport.Load(reportPath);
        char ch = ',';
        string[] val = paramString.Split(ch);
        if (customReport.ParameterFields.Count > 0)
        {
            for (int i = 0; i < val.Length; i++)
            {
                int indexOfEql = val[i].IndexOf('=');
                int indexOfStar = val[i].IndexOf('*');
                string paramName = string.Empty;
                string value = string.Empty;
                string reportName = "MainRpt";
                paramName = val[i].Substring(0, indexOfEql);

                if (indexOfStar > 0)
                {
                    value = val[i].Substring(indexOfEql + 1, ((indexOfStar - 1) - indexOfEql));
                    reportName = val[i].Substring(indexOfStar + 1);
                }
                else
                {

                    value = val[i].Substring(indexOfEql + 1);
                }

                if (reportName == "MainRpt")
                {
                    if (value == "null")
                    {
                        customReport.SetParameterValue(paramName, null);
                    }
                    else
                        customReport.SetParameterValue(paramName, value);
                }
                else
                    customReport.SetParameterValue(paramName, value, reportName);
            }
        }

        ConfigureCrystalReports(customReport);
        for (int i = 0; i < customReport.Subreports.Count; i++)
        {
            ConfigureCrystalReports(customReport.Subreports[i]);
        }

        oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        return oStream;
    }
    static private void ConfigureCrystalReports(ReportDocument customReport)
    {
        ConnectionInfo connectionInfo = Common.GetCrystalConnection();
        Common.SetDBLogonForReport(connectionInfo, customReport);
    }
            
}