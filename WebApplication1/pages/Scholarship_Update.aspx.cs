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
using System.Web.UI.WebControls;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Security;
using mastersofterp;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;



public partial class Projects_Branch_Change : System.Web.UI.Page
{
    CourseController objCourse = new CourseController();
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
                CheckPageAuthorization();
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
               // objCommon.FillDropDownList(ddlAcademicSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0", "SESSION_PNAME desc");
                objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_NAME");
               // objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME desc");
               // objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNAME");
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
                Response.Redirect("~/notauthorized.aspx?page=Scholarship_Update.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Scholarship_Update.aspx");
        }
    }

    protected void btnExel_Click(object sender, EventArgs e)
    {

        Response.ContentType = "application/vnd.ms-excel";
        string path = Server.MapPath("~/ExcelFormat/Scholarship_update_data(2).xls");
        Response.AddHeader("Content-Disposition", "attachment;filename=\"ScholarshipUpload.xls\"");
        Response.TransmitFile(path);
        Response.End();
       

    }
    protected void Exeldata()
    {
        string SP_Name1 = "PKG_ACD_GET_WALLET_DUPLICATE_RECORD";
        string SP_Parameters1 = "@P_OUT";
        string Call_Values1 = "" + Convert.ToInt32(1);

        DataSet que_out1 = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);

        if (que_out1.Tables[0] != null && que_out1.Tables[0].Rows.Count > 0)
        {
            lvDuplicaterecord.DataSource = que_out1;
            lvDuplicaterecord.DataBind();
        }
        else
        {
            lvDuplicaterecord.DataSource = null;
            lvDuplicaterecord.DataBind();
        }
       // int AdmissionBatch = Convert.ToInt32(ddlBatch.SelectedValue);
        int College_id = Convert.ToInt32(ddlFaculty.SelectedValue);
        int RECON =1;
        string TEXT_MODE="Scholarship";
        //DataSet dsfee = objCommon.FillDropDown("ACD_STUDENT S INNER  JOIN ACD_SEMESTER  SE ON(S.SEMESTERNO=SE.SEMESTERNO) INNER JOIN ACD_DEGREE D ON(D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH C ON(C.BRANCHNO=S.BRANCHNO) LEFT JOIN ACD_SCHOLARSHIP_UPDATE_AMOUNT UA ON (UA.IDNO=S.IDNO)",
        //    "S.REGNO", "S.IDNO,S.STUDNAME,SE.SEMESTERNAME,S.SEMESTERNO,S.DEGREENO,S.BRANCHNO,(D.DEGREENAME +','+ C.LONGNAME) AS PROGRAM,SCHOLAR_AMOUNT", "S.COLLEGE_ID=" + College_id + "AND S.ADMBATCH=" + AdmissionBatch + "AND S.DEGREENO=" + Convert.ToInt32(ddlProgram.SelectedValue) + "", "");
        DataSet dsfee = objCommon.FillDropDown("ACD_STUDENT_WALLET SU INNER JOIN ACD_STUDENT S ON(SU.IDNO=S.IDNO) LEFT JOIN ACD_SEMESTER SE ON(SE.SEMESTERNO=S.SEMESTERNO) LEFT JOIN ACD_DEGREE D ON(D.DEGREENO=S.DEGREENO) INNER JOIN ACD_COLLEGE_MASTER CM ON(CM.COLLEGE_ID=S.COLLEGE_ID) INNER JOIN ACD_BRANCH BR ON(BR.BRANCHNO=S.BRANCHNO)",
           "S.REGNO", "SU.IDNO,S.NAME_WITH_INITIAL,SE.SEMESTERNAME,D.DEGREENO,(D.DEGREENAME+'-'+BR.LONGNAME) AS PROGRAM ,SU.TRAN_AMT,SU.RECON,CM.COLLEGE_NAME", "SU.RECON NOT IN(" + RECON + ") AND TRAN_MODE=" + "'Scholarship'" + "", "");
        if (dsfee.Tables[0].Rows.Count > 0)
        {
            lvScholarship.DataSource = dsfee;
            lvScholarship.DataBind();
        }
        else
        {
            lvScholarship.DataSource = dsfee;
            lvScholarship.DataBind();
            objCommon.DisplayMessage(this, "Data Not Found", this.Page);
            return;
        }
         
        foreach (ListViewDataItem dataitem in lvScholarship.Items)
        {
            CheckBox CHK = dataitem.FindControl("chkapp") as CheckBox;
            Label Recon = dataitem.FindControl("lblRecon") as Label;
            string recon = (Recon.Text);
            if (recon == "True")
            {
               // CHK.Checked = true;
                Recon.Text = "Confirm";
                Recon.CssClass = "badge badge-success";
            }
            else
            {
               // CHK.Checked = false;
                Recon.Text = "Not Confirm";
                Recon.CssClass = "badge  badge-danger";
            }
        }
    }
    protected void btnUploade_Click(object sender, EventArgs e)
    {
       
        try
        {
         
            string path = System.Web.HttpContext.Current.Server.MapPath("~/ExcelFormat/");
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
                    //lvScholarship.DataSource = null;
                    //lvScholarship.DataBind();
                }
                else
                {
                    // lblTotalMsg.Text = "Only excel sheet is allowed to upload";

                    objCommon.DisplayMessage(this, "Only excel sheet is allowed to upload.", this.Page);

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
    private bool CheckFormatAndImport(string extension, string excelPath)
    {
        MappingController objmp = new MappingController();
        ImportDataMaster objIDM = new ImportDataMaster();
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
        objCommon.DisplayMessage(this, conString, this.Page);
        try
        {
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                DataTable dtExcelData = new DataTable();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString().Trim();

                dtExcelData.Columns.AddRange(new DataColumn[] {


                new DataColumn("REGNO", typeof(string)),
                new DataColumn("STUDENTNAME", typeof(string)),
                new DataColumn("SEMESTER", typeof(string)),
                new DataColumn("PROGRAM", typeof(string)),
                new DataColumn("AMOUNT", typeof(decimal)),
                new DataColumn("ACADEMIC_YEAR", typeof(decimal)),
               
                     
                });

                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                {
                    oda.Fill(dtExcelData);
                    DataSet ds1 = new DataSet();
                    ds1.Tables.Add(dtExcelData);
                   // divapti.Visible = true;
                  //  btncanceltest.Visible = true;
                    foreach (DataTable table in ds1.Tables)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            foreach (DataRow dr in table.Rows)
                            {
                                if (dr["REGNO"].ToString() == string.Empty)
                                {
                                   // objCommon.DisplayMessage("Cant Pass Null Records in Excel Sheet!!", this);
                                    dr.Delete();
                                    table.AcceptChanges();
                                    break;

                                }
                            }
                        }
                    }
                }
                excel_con.Close();
               // int admbatch = Convert.ToInt32(ddlBatch.SelectedValue);
                int College_id = Convert.ToInt32(ddlFaculty.SelectedValue);
                int degreeno = 0;
                int UserNo = Convert.ToInt32(Session["userno"].ToString());

                objIDM.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
                int ret = objmp.ScholarshipUploadDataEcel(dtExcelData, College_id, degreeno, UserNo);
                if (ret == 1)
                {
                    //lvapti.DataSource = dtExcelData;
                    //divapti.Visible = true;
                    Exeldata();
                    //btncanceltest.Visible = true;
                    //ddlintakeapti.SelectedIndex = 0;
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
            //objCommon.DisplayMessage("Record not uploaded, file is not in correct format. Please check the format of file & upload again.", this);
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Records not saved. Please check whether the file is in correct format or not. ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(this.Page, "Server UnAvailable");
            return false;
        }
    }

    protected void btnConform_Click(object sender, EventArgs e)
    {
        try
        {
            CustomStatus cs = 0;
            string RegNo = "";
            string RegNoCheck = "";
            int Idno = 0,Recon=0;
            decimal Amount = 0.0m;
            //int Adm_batch = Convert.ToInt32(ddlBatch.SelectedValue);
            int College_Id = Convert.ToInt32(ddlFaculty.SelectedValue);
            foreach (ListViewDataItem dataitem in lvScholarship.Items)
            {

                CheckBox chkBox = dataitem.FindControl("chkapp") as CheckBox;
                Label lblregno = dataitem.FindControl("lblregno") as Label;
                Label lblIdno = dataitem.FindControl("lblIdno") as Label;
               

                if (chkBox.Checked == true)
                {
                   
                    RegNo = lblregno.Text;
                    Idno = Convert.ToInt32(lblIdno.Text);
                    Recon = 1;

                    cs = (CustomStatus)objCourse.InsertScholarshipUploadAmount(Idno, RegNo, Recon);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        lvScholarship.DataSource = null;
                        lvScholarship.DataBind();
                        objCommon.DisplayMessage(this, "Scholarship Upload Confirm...", this.Page);

                     
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Failed To Save Record ", this.Page);
                    }
                }
              
            }
         
            if (RegNoCheck.ToString() == "")
            {
                objCommon.DisplayMessage(this, "Please Select At List One ", this.Page);
            }
          
           
            }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ScholarshipEligibility_Master.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    //protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    objCommon.FillDropDownList(ddlProgram, " ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_DEGREE D ON(D.DEGREENO=DB.DEGREENO)", "DISTINCT D.DEGREENO", "DEGREENAME", "COLLEGE_ID=" + Convert.ToInt32(ddlFaculty.SelectedValue), "DEGREENAME");
    //}

    protected void btnview_Click(object sender, EventArgs e)
    {
        string TEXT_MODE = "Scholarship";
       // int AdmissionBatch = Convert.ToInt32(ddlBatch.SelectedValue);
        int College_id = Convert.ToInt32(ddlFaculty.SelectedValue);
        int STATUS=1;
        //DataSet dsfee = objCommon.FillDropDown("ACD_STUDENT S INNER  JOIN ACD_SEMESTER  SE ON(S.SEMESTERNO=SE.SEMESTERNO) INNER JOIN ACD_DEGREE D ON(D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH C ON(C.BRANCHNO=S.BRANCHNO) LEFT JOIN ACD_SCHOLARSHIP_UPDATE_AMOUNT UA ON (UA.IDNO=S.IDNO)",
        //    "S.REGNO", "S.IDNO,S.STUDNAME,SE.SEMESTERNAME,S.SEMESTERNO,S.DEGREENO,S.BRANCHNO,(D.DEGREENAME +','+ C.LONGNAME) AS PROGRAM,SCHOLAR_AMOUNT", "S.COLLEGE_ID=" + College_id + "AND S.ADMBATCH=" + AdmissionBatch + "AND S.DEGREENO=" + Convert.ToInt32(ddlProgram.SelectedValue) + "", "");
        //DataSet dsfee = objCommon.FillDropDown("ACD_STUDENT_SCHOLARSHIP_DATA SU LEFT JOIN ACD_STUDENT S ON(SU.IDNO=S.IDNO) LEFT JOIN ACD_SEMESTER SE ON(SE.SEMESTERNO=SU.SEMESTERNO) LEFT JOIN ACD_DEGREE D ON(D.DEGREENO=SU.DEGREENO)",
        //   "SU.ENROLLNO", "SU.IDNO,S.STUDNAME,SE.SEMESTERNAME,SU.DEGREENO,D.DEGREENAME ,SU.SHOLARSHIP_AMOUNT", "SU.COLLEGE_ID=" + College_id + "AND SU.ADMBATCH=" + AdmissionBatch + " AND SU.SCHOLAR_STATUS NOT IN (" + STATUS + ")", "");
        DataSet dsfee = objCommon.FillDropDown("ACD_STUDENT_WALLET SU INNER JOIN ACD_STUDENT S ON(SU.IDNO=S.IDNO) LEFT JOIN ACD_SEMESTER SE ON(SE.SEMESTERNO=S.SEMESTERNO) LEFT JOIN ACD_DEGREE D ON(D.DEGREENO=S.DEGREENO) INNER JOIN ACD_COLLEGE_MASTER CM ON(CM.COLLEGE_ID=S.COLLEGE_ID) INNER JOIN ACD_BRANCH BR ON(BR.BRANCHNO=S.BRANCHNO)",
          "S.REGNO", "SU.IDNO,S.NAME_WITH_INITIAL,SE.SEMESTERNAME,D.DEGREENO,(D.DEGREENAME +'-'+BR.LONGNAME) AS PROGRAM ,SU.TRAN_AMT,SU.RECON,CM.COLLEGE_NAME", "S.COLLEGE_ID=" + College_id + "AND SU.TRAN_MODE=" + "'Scholarship'" + "", "");
        if (dsfee.Tables[0].Rows.Count > 0)
        {
            lvScholarship.DataSource = dsfee;
            lvScholarship.DataBind();
        }
        else
        {
            lvScholarship.DataSource = null;
            lvScholarship.DataBind();
            objCommon.DisplayMessage(this, "Data Not Found", this.Page);
            return;
        }
        foreach (ListViewDataItem dataitem in lvScholarship.Items)
        {
            CheckBox CHK = dataitem.FindControl("chkapp") as CheckBox;
            Label Recon = dataitem.FindControl("lblRecon") as Label;
            string  recon = (Recon.Text);
            if (recon == "True")
            {
                CHK.Checked = true;
                Recon.Text = "Confirm";
                Recon.CssClass = "badge badge-success";
            }
            else
            {
                CHK.Checked = false;
                Recon.Text = "Not Confirm";
                Recon.CssClass = "badge  badge-danger";
            }
        }
    }
}