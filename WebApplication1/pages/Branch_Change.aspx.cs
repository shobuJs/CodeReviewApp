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


public partial class Projects_Branch_Change : System.Web.UI.Page
{
    CourseController objCourse = new CourseController();
    BranchController branch = new BranchController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
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
                this.CheckPageAuthorization();

                if (Session["usertype"].ToString() == "1" && Session["username"].ToString().ToUpper().Contains("PRINCIPAL"))
                {
                    // divShow.Visible=true;
                }
                else
                {
                    // divShow.Visible=false;
                }

                Page.Title = objCommon.LookUp("reff", "collegename", string.Empty);

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                }
                string IpAddress = objCommon.LookUp("User_Acc", "IpAddress", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
                Session["IpAddress"] = IpAddress;
                objCommon.FillDropDownList(ddlAcademicSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO desc");
                objCommon.FillDropDownList(ddlbulksession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO desc");
            //    objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                //objCommon.FillDropDownList(ddlbulkfaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                objCommon.FillDropDownList(ddlbulksem, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO!=100 AND SEMESTERNO>0 ", "SEMESTERNO");
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));

            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Branch_Change.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Branch_Change.aspx");
        }
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlStudyLevel, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_UA_SECTION UA ON (UA.UA_SECTION=DB.UGPGOT)", "distinct UA_SECTION", "SHORTNAME", "COLLEGE_ID=" + Convert.ToInt32(ddlFaculty.SelectedValue), "SHORTNAME");
    }
    protected void ddlStudyLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillListBox(lstbxProgram, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (D.DEGREENO=CDB.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=CDB.BRANCHNO) ", "(CONVERT(NVARCHAR(16),D.DEGREENO) + '$' + CONVERT(NVARCHAR(16),B.BRANCHNO)) AS PROGRAM", "(D.DEGREENAME +'-'+ B.LONGNAME) AS PROGRAM", " CDB.COLLEGE_ID='" + Convert.ToInt32(ddlFaculty.SelectedValue) + "'AND UGPGOT='" + Convert.ToInt32(ddlStudyLevel.SelectedValue) + "'", "D.DEGREENO");
        objCommon.FillListBox(lstbxApplyProgram, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (D.DEGREENO=CDB.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=CDB.BRANCHNO) ", "(CONVERT(NVARCHAR(16),D.DEGREENO) + '$' + CONVERT(NVARCHAR(16),B.BRANCHNO)) AS PROGRAM", "(D.DEGREENAME +'-'+ B.LONGNAME) AS PROGRAM", " CDB.COLLEGE_ID='" + Convert.ToInt32(ddlFaculty.SelectedValue) + "'AND UGPGOT='" + Convert.ToInt32(ddlStudyLevel.SelectedValue) + "'", "D.DEGREENO");
    }
    protected void BindListViewData() 
    {
        int session = Convert.ToInt32(ddlAcademicSession.SelectedValue);
        int College_id = Convert.ToInt32(ddlFaculty.SelectedValue);
        int Semester = Convert.ToInt32(ddlSemester.SelectedValue);
        string Program = string.Empty;
        string degreeno = string.Empty;
        string Branchno = string.Empty;
        foreach (ListItem items in lstbxProgram.Items)
        {
            if (items.Selected == true)
            {
                Program = items.Value;
                string[] splitValue;
                splitValue = Program.Split('$');

                degreeno += (splitValue[0].ToString()) + ',';
                Branchno += (splitValue[1].ToString()) + ',';
            }
        }
        if (Program == string.Empty)
        {
            objCommon.DisplayMessage(this, "Please Select Faculty", this.Page);
            return;
        }
        Branchno = Branchno.Substring(0, Branchno.Length - 1);
        degreeno = degreeno.Substring(0, degreeno.Length - 1);
        int StudyLevel = Convert.ToInt32(ddlStudyLevel.SelectedValue);

        DataSet ds = objCourse.GetBranchChangeData(session, College_id, StudyLevel, Branchno, degreeno, Semester);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        // if (ds != null & ds.Tables.Count > 0)
        {
            lvchangebranch.DataSource = ds;
            lvchangebranch.DataBind();
            DivApply.Visible = true;

        }
        else
        {
            lvchangebranch.DataSource = null;
            lvchangebranch.DataBind();
            objCommon.DisplayMessage(this, "Record Not Found !!", this.Page);
            return;

        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListViewData(); 

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
        int  DegreeNo = 0, Branch = 0,idno=0,Afiliate=0;
        string Enroll = "", Regno = "",IdnoCheck="";

        int College_id = Convert.ToInt32(ddlFaculty.SelectedValue);
        int SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        string Program = string.Empty;
        string degreeno = string.Empty;
        string Branchno = string.Empty;
        foreach (ListItem items in lstbxApplyProgram.Items)
        {
            if (items.Selected == true)
            {
                Program = items.Value;
                string[] splitValue;
                splitValue = Program.Split('$');

                degreeno += (splitValue[0].ToString()) + ',';
                Branchno += (splitValue[1].ToString()) + ',';
            }
        }
        if (Program == string.Empty)
        {
            objCommon.DisplayMessage(this, "Please Select Apply Program", this.Page);
            return;
        }
        Branchno = Branchno.Substring(0, Branchno.Length - 1);
        degreeno = degreeno.Substring(0, degreeno.Length - 1);
      
        foreach (ListViewDataItem dataitem in lvchangebranch.Items)
        {
            CheckBox chkapp = dataitem.FindControl("chkapp") as CheckBox;
            Label lblregno = dataitem.FindControl("lblregno") as Label;
            Label lblEnroll = dataitem.FindControl("lblEnroll") as Label;
            Label lblIdno = dataitem.FindControl("lblIdno") as Label;
            Label lbldegreeNo = dataitem.FindControl("lbldegreeNo") as Label;
            Label lblBranchNo = dataitem.FindControl("lblBranchNo") as Label;
            Label lblAfiliated = dataitem.FindControl("lblAfiliated") as Label;          
            if (chkapp.Checked == true)
            {
               
                IdnoCheck += lblIdno.Text;
                Regno = lblregno.Text;
                Enroll = lblEnroll.Text;
                idno = Convert.ToInt32(lblIdno.Text);
                DegreeNo = Convert.ToInt32(lbldegreeNo.Text);
                Branch = Convert.ToInt32(lblBranchNo.Text);
                Afiliate=Convert.ToInt32(lblAfiliated.Text);
                CustomStatus cs = (CustomStatus)objCourse.InsertBranchChangeData(idno, Branch, Branchno, Regno, Enroll, College_id, DegreeNo, degreeno, Session["IpAddress"].ToString(), Convert.ToInt32(Session["userno"].ToString()), Afiliate, SemesterNo,Convert.ToInt32(ddlAcademicSession.SelectedValue));

                if (cs == CustomStatus.RecordSaved)
                {
                    objCommon.DisplayMessage(this, "Record Saved Successfully !!", this.Page);
                    BindListViewData(); 
                  
                }
                else if (cs == CustomStatus.RecordExist)
                {
                    objCommon.DisplayMessage(this, "Record Alredy Exists", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this, "Server Error", this.Page);
                }
                lstbxApplyProgram.SelectedValue = null;
            }
        }
        if (IdnoCheck.ToString() == "")
        {
            objCommon.DisplayMessage(this, "Please Select At List One", this.Page);
            return;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void lstbxProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Program = string.Empty;
        string degreeno = string.Empty;
        string Branchno = string.Empty;
        foreach (ListItem items in lstbxProgram.Items)
        {
            if (items.Selected == true)
            {
                Program = items.Value;
                string[] splitValue;
                splitValue = Program.Split('$');

                degreeno += (splitValue[0].ToString()) + ',';
                Branchno += (splitValue[1].ToString()) + ',';
            }
        }
        if (Program == string.Empty)
        {
            objCommon.DisplayMessage(this, "Please Select Faculty", this.Page);
            return;
        }
        Branchno = Branchno.Substring(0, Branchno.Length - 1);
        degreeno = degreeno.Substring(0, degreeno.Length - 1);
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_STUDENT ST ON (S.SEMESTERNO=ST.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "ST.DEGREENO IN(" + degreeno + ")", "SEMESTERNAME");
    }
    protected void lksingle_Click(object sender, EventArgs e)
    {
        divlksingle.Attributes.Add("class", "active");
        divlkbulk.Attributes.Remove("class");
        divsingle.Visible = true;
        divbulk.Visible = false;
    }
    protected void lkbulk_Click(object sender, EventArgs e)
    {
        divlkbulk.Attributes.Add("class", "active");
        divlksingle.Attributes.Remove("class");
        divsingle.Visible = false;
        divbulk.Visible = true;
        ConfirmData();
    }
    protected void btnEnrollDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/vnd.ms-excel";
            string path = Server.MapPath("~/ExcelFormat/Preformat_Of_Program_Apply.xls");
            Response.AddHeader("Content-Disposition", "attachment;filename=\"Preformat_Of_Program_Apply.xls\"");
            Response.TransmitFile(path);
            Response.End();
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnEnrollVerify_Click(object sender, EventArgs e)
    {
        try
        {
            divlksingle.Attributes.Remove("class");
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
                    objCommon.DisplayMessage(this.updBulkbranchchange, "Only excel sheet is allowed to upload.", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updBulkbranchchange, "Please select file to upload.", this.Page);
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
    private bool CheckFormatAndImport(string extension, string excelPath)
    {
        string[] pgm = new string[] { };
        string degreeno ="";
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
        objCommon.DisplayMessage(this.updBulkbranchchange, conString, this.Page);
        try
        {
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelData = new DataTable();
                dtExcelData.Columns.AddRange(new DataColumn[] {
                new DataColumn("StudentID", typeof(string)),
                new DataColumn("StudentName", typeof(string)),
                new DataColumn("ExistingProgram", typeof(string)),
                new DataColumn("ApplyforProgram", typeof(string)),
                });
                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                {
                    oda.Fill(dtExcelData);
                    DataSet ds1 = new DataSet();
                    ds1.Tables.Add(dtExcelData);                
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
                                pgm = dr["ApplyforProgram"].ToString().Split('-');
                                //if (pgm[2] != null)
                                //{
                                //    degreeno = pgm[1] + '-' + pgm[2];
                                //}
                                //else
                                //{
                                //    degreeno = pgm[1];
                                //}
                            }
                        }
                    }
                }
                RandomFunction();
                excel_con.Close();
                int ret = branch.ImportDataForBranch(dtExcelData, Convert.ToInt32(ddlbulksession.SelectedValue), Convert.ToInt32(ddlbulkfaculty.SelectedValue), Convert.ToInt32(ddlbulkstudy.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlbulksem.SelectedValue), Convert.ToString(Session["collge_code"]), Convert.ToString(Session["IpAddress"]), Convert.ToString(hdfAdmission.Value), Convert.ToString(FileUpload1.PostedFile.FileName.ToString()));
                if (ret == 1)
                {
                    binddata();
                    objCommon.DisplayMessage("Data Upload Successfully!!", this);
                    clear();
                    divlksingle.Attributes.Remove("class");
                    btnEnrollConfirm.Visible = true;
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
        hdfAdmission.Value = Convert.ToString(ir);
    }
    private void binddata()
    {
        DataSet ds = branch.GetBulkProgramApply(Convert.ToString(FileUpload1.PostedFile.FileName.ToString()), Convert.ToString(hdfAdmission.Value));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            uploadapp.Visible = true;
            LvTempApp.DataSource = ds;
            LvTempApp.DataBind();
            btnEnrollConfirm.Visible = true;
            DivConfirm.Visible = false;
            LvConfirm.DataSource = null;
            LvConfirm.DataBind();

        }
        else
        {
            btnEnrollConfirm.Visible = false;
            LvTempApp.DataSource = null;
            LvTempApp.DataBind();
            uploadapp.Visible = false;
            DivConfirm.Visible = false;
            LvConfirm.DataSource = null;
            LvConfirm.DataBind();
            objCommon.DisplayMessage(this, "Record Not Found !!", this.Page);
            return;

        }
    }
    private void VerifyandRegister(string username,string degreeno,string branchno)
    {
        try
        {
            int retout = 0;
            retout = branch.VerifyandMovedPgmApply(Convert.ToString(hdfAdmission.Value), Convert.ToString(username), Convert.ToString(degreeno), Convert.ToString(branchno));
            if (retout > 0)
            {
                objCommon.DisplayMessage(updBulkbranchchange, "Records verified successfully", this.Page);
                ConfirmData();
                btnEnrollConfirm.Visible = false;
                uploadapp.Visible = false;
                LvTempApp.DataSource = null;
                LvTempApp.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(updBulkbranchchange, "Records verified Failed.", this.Page);
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(updBulkbranchchange, "Error occurred.", this.Page);
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Error occurred. ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(this.Page, "Server UnAvailable");
        }
    }
    protected void btnEnrollConfirm_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdfAdmission.Value) > 0)
        {
            string username = "", degreeno="",branchno=""; int count = 0;
            foreach (ListViewDataItem item in LvTempApp.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("chktransfer");
                HiddenField hdfdegree = (HiddenField)item.FindControl("hdfnewdegree");
                HiddenField hdfbranch = (HiddenField)item.FindControl("hdfnewbranch");
                if (chk.Checked == true && chk.Enabled == true)
                {
                    count++;
                    username += chk.ToolTip + ',';
                    degreeno += hdfdegree.Value + ',';
                    branchno += hdfbranch.Value + ',';
                }
            }
            if (count > 0)
            {
                username = username.TrimEnd(',');
                degreeno = degreeno.TrimEnd(',');
                branchno = branchno.TrimEnd(',');
                VerifyandRegister(username, degreeno, branchno);
            }
            else
            {
                objCommon.DisplayMessage(updBulkbranchchange, "Please Select Atleast One CheckBox !!!", this);
            }
        }
        else
        {
            objCommon.DisplayMessage(updBulkbranchchange, "Excel sheet is not uploaded for given selection. First upload excel sheet then Verify and Move.", this);
        }
    }
    private void clear()
    {
        ddlbulksession.SelectedIndex = 0;
        ddlbulkfaculty.SelectedIndex = 0;
        ddlbulkstudy.SelectedIndex = 0;
        ddlbulksem.SelectedIndex = 0;
    }
    private void ConfirmData()
    {
        DataSet ds = null;
        ds = branch.ConfirmprogramApplyData(Convert.ToInt32(ddlbulkfaculty.SelectedValue), Convert.ToInt32(ddlbulkstudy.SelectedValue), Convert.ToInt32(ddlbulksem.SelectedValue));
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            DivConfirm.Visible = true;
            LvConfirm.DataSource = ds;
            LvConfirm.DataBind();

        }
        else
        {
            DivConfirm.Visible = false;
            LvConfirm.DataSource = null;
            LvConfirm.DataBind();

        }
    }
    
    protected void btnEnrollCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void ddlbulkfaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        divlksingle.Attributes.Remove("class");
        objCommon.FillDropDownList(ddlbulkstudy, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_UA_SECTION UA ON (UA.UA_SECTION=DB.UGPGOT)", "distinct UA_SECTION", "SHORTNAME", "COLLEGE_ID=" + Convert.ToInt32(ddlbulkfaculty.SelectedValue), "SHORTNAME");
    }
    protected void BtnProgram_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/vnd.ms-excel";
            string path = Server.MapPath("~/ExcelFormat/Program_List.xls");
            Response.AddHeader("Content-Disposition", "attachment;filename=\"Program_List.xls\"");
            Response.TransmitFile(path);
            Response.End();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlAcademicSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER CM INNER JOIN ACD_SESSION_MAPPING SM ON (CM.COLLEGE_ID=SM.COLLEGE_ID)", "CM.COLLEGE_ID", "COLLEGE_NAME COLLEGE_NAME", "CM.COLLEGE_ID > 0 and isnull(CM.ACTIVE,0)=1 AND ISNULL(STATUS,0)=1 AND isnull(SM.SESSIONNO,0)=" + ddlAcademicSession.SelectedValue, "CM.COLLEGE_ID");
    }
    protected void ddlbulksession_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.objCommon.FillDropDownList(ddlbulkfaculty, "ACD_COLLEGE_MASTER CM INNER JOIN ACD_SESSION_MAPPING SM ON (CM.COLLEGE_ID=SM.COLLEGE_ID)", "CM.COLLEGE_ID", "COLLEGE_NAME COLLEGE_NAME", "CM.COLLEGE_ID > 0 AND ISNULL(STATUS,0)=1 and isnull(CM.ACTIVE,0)=1 AND isnull(SM.SESSIONNO,0)=" + ddlbulksession.SelectedValue, "CM.COLLEGE_ID");
    }
}