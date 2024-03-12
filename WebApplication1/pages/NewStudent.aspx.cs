using EASendMail;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using mastersofterp;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using DotNetIntegrationKit;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using System.Xml;
using System.Net;
using _NVP;
using System.Collections.Specialized;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;

using Microsoft.WindowsAzure.Storage;
public partial class ACADEMIC_AddStudent : System.Web.UI.Page
{
    Common objCommon = new Common();
    FeeCollectionController feeController = new FeeCollectionController();
    StudentController objSC = new StudentController();
    Student objS = new Student();
    NewUserController objNC = new NewUserController();
    DocumentControllerAcad objdocContr = new DocumentControllerAcad();
    FeeCollectionController fee = new FeeCollectionController();
    private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string username = string.Empty;
    UserController user = new UserController();
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
    string orderid = string.Empty;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
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
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                this.CheckPageAuthorization();
                ViewState["userno"] = 0;//Session["userno"].ToString();// objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + Session["username"].ToString() + "'");
                //Page Authorization
              

                FillDropDown();
                bindlist();
                Session["Row"]=null;
                //      BankDetails();
                //       BindAmount();
                //ShowStudentDetails();
                //binddoclist();
            }
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
        }


    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=NewStudent.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=NewStudent.aspx");
        }
    }
    //private void CheckPageAuthorization()
    //{
    //    if (Request.QueryString["pageno"] != null)
    //    {
    //        //Check for Authorization of Page
    //        if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
    //        {
    //            Response.Redirect("~/notauthorized.aspx?page=AddStudent.aspx");
    //        }
    //    }
    //    else
    //    {
    //        //Even if PageNo is Null then, don't show the page
    //        Response.Redirect("~/notauthorized.aspx?page=AddStudent.aspx");
    //    }
    //}

    protected void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlMobileCode, "ACD_COUNTRY", "DISTINCT COUNTRYNO", "(MOBILE_CODE + ' - ' + COUNTRYNAME)COUNTRYNAME", "COUNTRYNO>0", "COUNTRYNO");
            objCommon.FillDropDownList(ddlHomeMobileCode, "ACD_COUNTRY", "DISTINCT COUNTRYNO", "(MOBILE_CODE + ' - ' + COUNTRYNAME)COUNTRYNAME", "COUNTRYNO>0", "COUNTRYNO");
            objCommon.FillDropDownList(ddlConCode, "ACD_COUNTRY", "DISTINCT COUNTRYNO", "(MOBILE_CODE + ' - ' + COUNTRYNAME)COUNTRYNAME", "COUNTRYNO>0", "COUNTRYNO");
            ddlHomeMobileCode.SelectedValue = "212";
            ddlMobileCode.SelectedValue = "212";
            ddlConCode.SelectedValue = "212";
            objCommon.FillDropDownList(ddlCountry, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO>0", "COUNTRYNAME");
            objCommon.FillDropDownList(ddlProvince, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");

            objCommon.FillDropDownList(ddlPerContry, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO>0", "COUNTRYNAME");
            objCommon.FillDropDownList(ddlPerProvince, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");

            objCommon.FillDropDownList(ddlALExamType, "ACD_ALTYPE", "ALTYPENO", "ALTYPENAME", "", "");
            objCommon.FillDropDownList(ddlALPasses, "ACD_QUALILEVEL", "QUALILEVELNO", "QUALILEVELNAME", "", "QUALILEVELNAME");
            objCommon.FillDropDownList(ddlALStream, "ACD_STREAM", "STREAMNO", "STREAMNAME", "", "STREAMNAME");

            objCommon.FillDropDownList(ddlOLExamType, "ACD_ALTYPE", "ALTYPENO", "ALTYPENAME", "", "");
            objCommon.FillDropDownList(ddlOLPasses, "ACD_QUALILEVEL_OL", "QUALILEVELNO_OL", "QUALILEVELNAME_OL", "QUALILEVELNO_OL > 0", "QUALILEVELNAME_OL");
            objCommon.FillDropDownList(ddlOLStream, "ACD_STREAM", "STREAMNO", "STREAMNAME", "", "STREAMNAME");

            objCommon.FillDropDownList(ddlWeekdayWeekend, "ACD_BATCH_SLIIT", "WEEKNO", "WEEKDAYSNAME", "", "WEEKDAYSNAME");
            //     objCommon.FillDropDownList(ddlFacultySchoolName, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");

            objCommon.FillDropDownList(ddlCampus, "ACD_CAMPUS", "CAMPUSNO", "CAMPUSNAME", "", "CAMPUSNO");


            objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVE = 1", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO=1", "");
            objCommon.FillDropDownList(ddlbankdetails, "ACD_BANK", "DISTINCT BANKNO", "BANKNAME", "BANKNO > 0", "BANKNO");
            objCommon.FillDropDownList(ddlStudyLevel, "ACD_UA_SECTION", "DISTINCT (UA_SECTION)", "UA_SECTIONNAME", "", "(UA_SECTION)");
            objCommon.FillDropDownList(ddlsource, "ACD_SOURCETYPE", "DISTINCT (SOURCETYPENO)", "SOURCETYPENAME", "", "(SOURCETYPENO)");

        }
        catch (Exception ex)
        {

        }
    }
    public void binddoclist()
    {
        try
        {
            string[] program;
            if (ddlProgram.SelectedValue == "0")
            {
                program = "0,0".Split(',');
            }
            else
            {
                program = ddlProgram.SelectedValue.Split(',');
            }

            int USERNO1 = Convert.ToInt32(ViewState["USER_NO"].ToString());// ((UserDetails)(Session["user"])).UserNo;
            DataSet ds = new DataSet();
            ds = objSC.GetDocumentNewStudentDetails(Convert.ToInt32(ViewState["stuinfoidno"]), Convert.ToInt32(ddlIntake.SelectedValue), Convert.ToInt32(program[0]), Convert.ToInt32(program[1]), Convert.ToInt32(ddlFacultySchoolName.SelectedValue));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvDocument.DataSource = ds;
                lvDocument.DataBind();
                lvDocument.Visible = true;
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (ListViewDataItem item in lvDocument.Items)
                    {
                        LinkButton lnk = item.FindControl("lnkViewDoc") as LinkButton;
                        Label fileformat = item.FindControl("lblImageFile") as Label;
                        Label fileformate = item.FindControl("lblFileFormat") as Label;
                        Label uploded = item.FindControl("lbluploadpic") as Label;
                        Label uploadDate = item.FindControl("lblUploadDate") as Label;
                        Label lblVerifyDocument = item.FindControl("lblVerifyDocument") as Label;
                        int value = int.Parse(lnk.CommandArgument);
                        if (value == 0)
                        {
                            if (Convert.ToString(ds.Tables[1].Rows[0]["DOC_STATUS"]) == "1")
                            {
                                lblVerifyDocument.Text = "Approved";
                                lblVerifyDocument.Style.Add("color", "green");
                            }
                            else if (Convert.ToString(ds.Tables[1].Rows[0]["DOC_STATUS"]) == "2")
                            {
                                lblVerifyDocument.Text = "Rejected";
                                lblVerifyDocument.Style.Add("color", "red");
                            }
                            uploded.Text = "";
                            uploded.Visible = true;
                            if (ds.Tables[1].Rows[0]["PHOTO"].ToString() == string.Empty)
                            {
                                uploded.Text = "NO";
                            }
                            else
                            {
                                uploded.Text = "YES";
                                lnk.Visible = true;
                                //uploadDate.Text = Convert.ToString(ds.Tables[1].Rows[0]["UPLOAD_DATE"]);
                                Session["STUDENTPHOTO"] = (byte[])ds.Tables[1].Rows[0]["PHOTO"];
                                break;
                            }
                        }

                        else
                        {
                            uploded.Text = "NO";
                        }
                    }
                }
                if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    foreach (ListViewDataItem item in lvDocument.Items)
                    {
                        for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                        {
                            LinkButton lnk = item.FindControl("lnkViewDoc") as LinkButton;
                            Label fileformat = item.FindControl("lblFileFormat") as Label;
                            Label uploded = item.FindControl("lbluploadpdf") as Label;
                            Label uploadDate = item.FindControl("lblUploadDate") as Label;
                            Label lblVerifyDocument = item.FindControl("lblVerifyDocument") as Label;
                            int value = int.Parse(lnk.CommandArgument);
                            lnk.CommandName = ds.Tables[2].Rows[i]["DOC_FILENAME"].ToString();
                            if (value >= 1)
                            {
                                if (Convert.ToString(ds.Tables[2].Rows[i]["DOC_STATUS"]) == "1")
                                {
                                    lblVerifyDocument.Text = "Approved";
                                    lblVerifyDocument.Style.Add("color", "green");
                                }
                                else if (Convert.ToString(ds.Tables[2].Rows[i]["DOC_STATUS"]) == "2")
                                {
                                    lblVerifyDocument.Text = "Rejected";
                                    lblVerifyDocument.Style.Add("color", "red");
                                }
                                if (value == Convert.ToInt32(ds.Tables[2].Rows[i]["DOCNO"]))
                                {
                                    uploded.Text = "YES";
                                    //uploadDate.Text = Convert.ToString(ds.Tables[2].Rows[i]["UPLOAD_DATE"]);
                                    uploded.Visible = true;
                                    lnk.Visible = true;
                                    break;
                                }
                                else
                                {
                                    uploded.Text = "NO";
                                }

                            }
                            // fileformat.Text = "Only formats are allowed : pdf";
                        }
                    }
                }
            }
            else
            {
                lvDocument.DataSource = null;
                lvDocument.DataBind();
                lvDocument.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ListofDocuments.bindlist-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void lnkViewDoc_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton lnk = sender as LinkButton;
            int value = int.Parse(lnk.CommandArgument);
            string FileName = lnk.CommandName.ToString();
            if (value == 0)
            {
                ImageViewer.Visible = true;
                ltEmbed.Visible = false;
                ImageViewer.ImageUrl = "~/showimage.aspx?id=" + Convert.ToInt32(ViewState["stuinfoidno"]) + "&type=STUDENT";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
            }
            else
            {
                ImageViewer.Visible = false;
                ltEmbed.Visible = true;
                string Url = string.Empty;
                string directoryPath = string.Empty;
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                string directoryName = "~/DownloadImg" + "/";
                directoryPath = Server.MapPath(directoryName);

                if (!Directory.Exists(directoryPath.ToString()))
                {

                    Directory.CreateDirectory(directoryPath.ToString());
                }
                CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
                string img = lnk.CommandName.ToString();
                var ImageName = img;
                if (img != null || img != "")
                {

                    DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
                    var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                    string filePath = directoryPath + "\\" + ImageName;
                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"700px\" height=\"400px\">";
                    embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    embed += "</object>";
                    ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
                }
                else
                {
                    objCommon.DisplayMessage(updDocument, "Sorry, File not found !!!", this.Page);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    //protected void BankDetails()
    //{
    //    DataSet ds = objCommon.FillDropDown("ACD_BANK", "BANKNO", "BANKCODE,BANKNAME,BANKADDR,ACCOUNT_NO", "BANKNO>0", "BANKNAME");
    //    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
    //    {
    //        lvBankDetails.DataSource = ds.Tables[0];
    //        lvBankDetails.DataBind();
    //    }
    //    else
    //    {
    //        lvBankDetails.DataSource = null;
    //        lvBankDetails.DataBind();
    //    }
    //}
    //protected void BindAmount()
    //{
    //    DataSet ds = objCommon.FillDropDown("ACD_DEMAND", "DM_NO", "IDNO,TOTAL_AMT", "", "");
    //    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
    //    {
    //        txtAmount.Text = ds.Tables[0].Rows[0]["TOTAL_AMT"].ToString();
    //    }
    //}
    protected void ddlFacultySchoolName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlStudyLevel, "ACD_UA_SECTION A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B on (A.UA_SECTION=B.UGPGOT)", "DISTINCT (A.UA_SECTION)", "A.UA_SECTIONNAME", "B.COLLEGE_ID=" + ddlFacultySchoolName.SelectedValue, "(A.UA_SECTION)");
        if (ddlFacultySchoolName.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlProgram, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON (A.BRANCHNO=B.BRANCHNO) INNER JOIN ACD_DEGREE C ON (C.DEGREENO=A.DEGREENO) INNER JOIN ACD_AFFILIATED_UNIVERSITY D on (D.AFFILIATED_NO=A.AFFILIATED_NO)", "(CONVERT(NVARCHAR(16),A.DEGREENO))+','+(CONVERT(NVARCHAR(16),B.BRANCHNO))+','+(CONVERT(NVARCHAR(16), A.AFFILIATED_NO))", "(DEGREENAME+'-'+LONGNAME+'-'+AFFILIATED_SHORTNAME) as PROGRAME", "A.UGPGOT=" + Convert.ToInt32(ddlStudyLevel.SelectedValue) + "AND A.COLLEGE_ID=" + ddlFacultySchoolName.SelectedValue, "(A.DEGREENO)");
        }
        else
        {
            ddlProgram.SelectedIndex = 0;
        }

    }
    protected void ddlIntake_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFacultySchoolName.SelectedIndex = 0;
        ddlProgram.SelectedIndex = 0;
        ddlStudyLevel.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        paymentdetails.Visible = false;
    }
    protected void ddlStudyLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlProgram.SelectedIndex = 0;
        if (ddlStudyLevel.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlFacultySchoolName, " ACD_COLLEGE_MASTER CM INNER JOIN USER_ACC UA ON (CM.COLLEGE_ID IN (SELECT CAST(VALUE AS INT) FROM DBO.SPLIT(UA.UA_COLLEGE_NOS,',')))INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON(CM.COLLEGE_ID=CDB.COLLEGE_ID)", "DISTINCT CM.COLLEGE_ID", "COLLEGE_NAME", "UA_NO =" + Session["userno"] + "AND UGPGOT=" + ddlStudyLevel.SelectedValue, "CM.COLLEGE_ID");
        }
        else
        {
            ddlFacultySchoolName.SelectedIndex = 0;
            ddlProgram.SelectedIndex = 0;
        }
        //objCommon.FillDropDownList(ddlProgram, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON (A.BRANCHNO=B.BRANCHNO) INNER JOIN ACD_DEGREE C ON (C.DEGREENO=A.DEGREENO) INNER JOIN ACD_AFFILIATED_UNIVERSITY D on (D.AFFILIATED_NO=A.AFFILIATED_NO)", "(CONVERT(NVARCHAR(16),A.DEGREENO))+','+(CONVERT(NVARCHAR(16),B.BRANCHNO))+','+(CONVERT(NVARCHAR(16), A.AFFILIATED_NO))", "(DEGREENAME+'-'+LONGNAME+'-'+AFFILIATED_SHORTNAME) as PROGRAME", "A.COLLEGE_ID=" + ddlFacultySchoolName.SelectedValue + "AND A.UGPGOT=" + Convert.ToInt32(ddlStudyLevel.SelectedItem.Value), "(A.DEGREENO)");
    }
    protected void btnSubmitStudentDetails_Click(object sender, EventArgs e)
    {
        try
        {
            // Student Personal Details

            if ((txtNIC.Text == "" && txtPassportNo.Text == ""))
            {
                objCommon.DisplayMessage(this.Page, "Please Enter NIC or Passport No.", this.Page);
                return;
            }
            string fullname = txtfullname.Text.Trim();
            objS.studfirstname = txtFirstName.Text.Trim();
            objS.studlastname = txtLastName.Text.Trim();
            objS.name_with_initial = txtNameInitial.Text.Trim();
            objS.emailid = txtEmail.Text;
            objS.studmobile = txtMobile.Text;
            objS.telphoneno = txtHomeTel.Text;
            objS.nicno = txtNIC.Text;
            objS.passportno = txtPassportNo.Text;
            if (Convert.ToString(txtDateOfBirth.Text) != string.Empty)
            {
                //if (Convert.ToDateTime(txtDateOfBirth.Text).Year > 2004 || txtDateOfBirth.Text.Length < 1)
                if (txtDateOfBirth.Text.Length < 1)
                {
                    objCommon.DisplayMessage(this.Page, "Date of Birth is not valid", this.Page);
                    return;
                }
                else
                {
                    objS.stud_dob = Convert.ToDateTime(txtDateOfBirth.Text).ToString("yyyy-MM-dd");
                }
            }
            objS.stud_sex = rdoGender.SelectedValue;
            if (rdoGender.SelectedValue == "0")
            {
                objS.stud_sex = "M";
            }
            else
            {
                objS.stud_sex = "F";
            }
            if (divdemand.Visible == true)
            {
                //if (ddlbankdetails.SelectedValue == "0")
                //{
                //    objCommon.DisplayMessage(this.Page, "Please Select Bank !!!", this.Page);
                //    return;
                //}
                //else if (TxtReceipt.Text == "")
                //{
                //    objCommon.DisplayMessage(this.Page, "Please Enter Receipt Number !!!", this.Page);
                //    return;
                //}
                //else if (Txtamt.Text == "")
                //{
                //    objCommon.DisplayMessage(this.Page, "Please Enter Amount To be Paid !!!", this.Page);
                //    return;
                //}


            }
            if (alolselection.SelectedValue == "1")
            {
                if (ddlALPasses.SelectedValue != "1")
                {
                    if (ddlALSubject1.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Select ALSubject1 !!!", this.Page);
                        return;
                    }
                    else if (ddlALGrade1.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Select ALGrade1 !!!", this.Page);
                        return;
                    }
                    else if (ddlALSubject2.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Select ALSubject2 !!!", this.Page);
                        return;
                    }
                    if (ddlALGrade2.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Select ALGrade2 !!!", this.Page);
                        return;
                    }
                    else if (ddlALSubject3.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Select ALSubject3 !!!", this.Page);
                        return;
                    }
                    else if (ddlALGrade3.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Select ALGrade3 !!!", this.Page);
                        return;
                    }
                    else if (ddlALSubject4.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Select ALSubject4 !!!", this.Page);
                        return;
                    }
                    else if (ddlALGrade4.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Select ALGrade4 !!!", this.Page);
                        return;
                    }
                    else if (txtALIndex.Text == "")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Enter A/L IndexNo !!!", this.Page);
                        return;
                    }
                    else if (txtalschool.Text == "")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Enter School Name !!!", this.Page);
                        return;
                    }
                }
            }
           
            objS.stud_citizenship = rdoCitizenType.SelectedValue;
            objS.identi_mark = rdoLeftRight.SelectedValue;

            //    Student Address            
            objS.paddress = txtPerAddress.Text.ToString();
            objS.pcountry = ddlPerContry.SelectedValue.ToString();
            objS.pprovince = ddlPerProvince.SelectedValue.ToString();
            objS.pdistrict = ddlPerDisctrict.SelectedValue.ToString();

            string Laddress = txtPermAddress.Text.ToString();
            string Lcountry = ddlCountry.SelectedValue.ToString();
            string Lprovince = ddlProvince.SelectedValue.ToString();
            string Ldistrict = ddlDistrict.SelectedValue.ToString();
            
            //    Student Education Details
            objS.altype = Convert.ToInt32(ddlALExamType.SelectedValue);
            objS.alstreamno = Convert.ToInt32(ddlALStream.SelectedValue);
            objS.alattemptno = Convert.ToInt32(ddlALPasses.SelectedValue);
            objS.alsubject1 = ddlALSubject1.SelectedValue;
            objS.algrade1 = ddlALGrade1.SelectedValue;
            objS.alsubject2 = ddlALSubject2.SelectedValue;
            objS.algrade2 = ddlALGrade2.SelectedValue;
            objS.alsubject3 = ddlALSubject3.SelectedValue;
            objS.algrade3 = ddlALGrade3.SelectedValue;
            objS.alsubject4 = ddlALSubject4.SelectedValue;
            objS.algrade4 = ddlALGrade4.SelectedValue;
            objS.oltype = Convert.ToInt32(ddlOLExamType.SelectedValue);
            objS.olstreamno = Convert.ToInt32(ddlOLStream.SelectedValue);
            objS.olattemptno = Convert.ToInt32(ddlOLPasses.SelectedValue);
            objS.olsubject1 = ddlOLSubject1.SelectedValue;
            objS.olgrade1 = ddlOLGrade1.SelectedValue;
            objS.olsubject2 = ddlOLSubject2.SelectedValue;
            objS.olgrade2 = ddlOLGrade2.SelectedValue;
            objS.olsubject3 = ddlOLSubject3.SelectedValue;
            objS.olgrade3 = ddlOLGrade3.SelectedValue;
            objS.olsubject4 = ddlOLSubject4.SelectedValue;
            objS.olgrade4 = ddlOLGrade4.SelectedValue;
            objS.olsubject5 = ddlOLSubject5.SelectedValue;
            objS.olgrade5 = ddlOLGrade5.SelectedValue;
            objS.olsubject6 = ddlOLSubject6.SelectedValue;
            objS.olgrade6 = ddlOLGrade6.SelectedValue;

            //   Student Faculty
            objS.stud_college_id = Convert.ToInt32(ddlFacultySchoolName.SelectedValue);

            string[] program;
            if (ddlProgram.SelectedValue == "0")
            {
                program = "0,0".Split(',');
            }
            else
            {
                program = ddlProgram.SelectedValue.Split(',');
            }
            objS.degreeno = Convert.ToInt32(program[0]);
            objS.branchno = Convert.ToInt32(program[1]);
            objS.campusno = Convert.ToInt32(ddlCampus.SelectedValue);
            objS.weeksnos = Convert.ToInt32(ddlWeekdayWeekend.SelectedValue);
            objS.semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            objS.admbatch = Convert.ToInt32(ddlIntake.SelectedValue);
            objS.ua_no = Convert.ToInt32(Session["userno"].ToString());
            objS.stud_remark = txtRemark.Text;
            string Password = "";
            Password = clsTripleLvlEncyrpt.ThreeLevelEncrypt("Sliit@123");

            objS.password = Password.ToString();
            objS.mobilecode = ddlMobileCode.SelectedValue;
            objS.hometelephonecode = ddlHomeMobileCode.SelectedValue;

            foreach (ListViewDataItem item in lvDocument.Items)
            {
                string DocNo = string.Empty;
                Label docno = item.FindControl("lblDocNo") as Label;
                Label DocName = item.FindControl("lblname") as Label;
                Label lbluploadpic = item.FindControl("lbluploadpic") as Label;
                FileUpload fuDocument = item.FindControl("fuDocument") as FileUpload;
                if (docno.Text == "0")
                {
                    if (lbluploadpic.Text == "")
                    {
                        if (fuDocument.HasFile)
                        {
                            DocNo += docno.Text;
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "Upload student photo document is mandatory field !!!", this.Page);
                            return;
                        }
                    }
                }
            }
             byte[] ChallanCopy = null;
             string filename = "";
             string docname = "";
             string bank = "", receiptno = "", amount = "";
             int COUNTRECEIPT = 0;
             foreach (ListViewDataItem dataitem in lvSemester.Items)
             {
                              
                 int count = Convert.ToInt32(objCommon.LookUp("ACD_DCR_TEMP", "MAX(TEMP_DCR_NO)", ""));
                 HttpPostedFile file = fileupload1.PostedFile;
                 COUNTRECEIPT++;
                 HiddenField hdfbankno = dataitem.FindControl("hdfbankno") as HiddenField;
                 HiddenField hdnReceiptNo = dataitem.FindControl("hdnReceiptNo") as HiddenField;
                 HiddenField hdnAmount = dataitem.FindControl("hdnAmount") as HiddenField;
                 HiddenField hdfReceipt = dataitem.FindControl("hdfReceipt") as HiddenField;
              
                     bank += hdfbankno.Value + ',';
                     receiptno += hdnReceiptNo.Value + ',';
                     amount += hdnAmount.Value + ',';
                     docname += hdfReceipt.Value + ',';
                     string contentType = contentType = fileupload1.PostedFile.ContentType;
                     string ext = System.IO.Path.GetExtension(hdfReceipt.Value);    
                    
                     DataTable table = (DataTable)Session["Row"];
                     for (int i = 0; i < table.Rows.Count; i++)
                     {
                         Random aa1 = new Random();
                         int aa = aa1.Next(01, 10000);
                         ChallanCopy = (byte[])table.Rows[i]["Byte"];
                         docname = aa + "_doc_" + "DirectRegSlip" + ext;
                         if (ext == ".pdf" || ext == ".PDF" || ext == ".jpg" || ext == ".JPG" || ext == ".png" || ext == ".PNG" || ext == ".jpeg" || ext == ".JPEG")
                         {
                             int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, docname + "", fileupload1, ChallanCopy);
                             if (retval == 0)
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                             }
                             filename += aa + "_doc_" + "DirectRegSlip" + ext + ',';
                         }
                     }
             }
             //if (ViewState["edit"] == null)
             //{
            if(TxtReceipt.Text!="")
            {
                 if (COUNTRECEIPT == 0)
                 {
                     objCommon.DisplayMessage(this.Page, "Please add at least one Record. !!", this.Page);
                     return;
                 }
             }
             bank = bank.TrimEnd(',');
             receiptno = receiptno.TrimEnd(',');
             amount = amount.TrimEnd(',');
             docname = docname.TrimEnd(',');
             filename = filename.TrimEnd(',');
             //if (fileupload1.HasFile)
             //{
             //    string contentType = contentType = fileupload1.PostedFile.ContentType;
             //    string ext = System.IO.Path.GetExtension(fileupload1.PostedFile.FileName);
             //    HttpPostedFile file = fileupload1.PostedFile;
             //    filename = fileupload1.PostedFile.FileName;                 
             //    int  count = Convert.ToInt32(objCommon.LookUp("ACD_DCR_TEMP", "MAX(TEMP_DCR_NO)", ""));
             //    docname = count+1+"_doc_" + "DirectRegSlip" + ext;
             //    int retval = Blob_Upload(blob_ConStr, blob_ContainerName, count+1 +"_doc_" + "DirectRegSlip" + "", fileupload1);
             //    if (retval == 0)
             //    {
             //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
             //        return;
             //    }
             //}
             //else
             //{
             //    docname = "";
             //}
            Random rnd = new Random();
            int ir = rnd.Next(01, 10000);
            orderid = Convert.ToString(Convert.ToInt32(ddlFacultySchoolName.SelectedValue) + 50 + 50 + Convert.ToString(ddlSemester.SelectedValue) + ir);
            Session["ERPORDERIDRESPONSE"] = orderid;
            string EnrollmentNo = txtEnrollmentNo.Text;

            string FatherName = txtMiddleName.Text;
            string FMobileNo = txtPMobNo.Text;
            string PEmail = txtPersonalEmail.Text;
            int FMobileCode = Convert.ToInt32(ddlConCode.SelectedValue);
            

            int idno = 0;
            idno = objSC.SubmitNewStudentAllDetails(objS, Convert.ToString(ddlALGrade1.SelectedValue), EnrollmentNo, amount, receiptno, ChallanCopy, filename, Convert.ToString(ddlbankdetails.SelectedItem), bank, Convert.ToString(Session["ERPORDERIDRESPONSE"]), Convert.ToString(txtALIndex.Text), Convert.ToString(Txtolindex.Text), Convert.ToDecimal(Txtaptimarks.Text), Convert.ToString(txtalschool.Text), Convert.ToString(txtolschool.Text), Convert.ToInt32(ddlsource.SelectedValue), Convert.ToInt32(ViewState["stuinfoidno"]), fullname, FatherName, FMobileNo, PEmail, FMobileCode, Laddress, Lcountry, Lprovince, Ldistrict);
            if (idno > 1 && idno != 2627)
            {
                int IDNO = idno;
                DataSet ds = objCommon.FillDropDown("User_Acc", "UA_NO", "UA_NAME", "UA_IDNO= " + IDNO + "", "");
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    string UserName = ds.Tables[0].Rows[0]["UA_NAME"].ToString();
                    DataSet dsUserContact = null;
                    UserController objUC = new UserController();
                    dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(1788));
                    if (dsUserContact.Tables[1] != null && dsUserContact.Tables[1].Rows.Count > 0)
                    {
                        string fullName = txtFirstName.Text + " " + txtLastName.Text;
                        SmtpMail oMail = new SmtpMail("TryIt");
                        oMail.From = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]);
                        oMail.To = txtEmail.Text;
                        oMail.Subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                        string message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                        oMail.HtmlBody = message;
                        oMail.HtmlBody = oMail.HtmlBody.Replace("[UA_FULLNAME]", fullName.ToString());
                        oMail.HtmlBody = oMail.HtmlBody.Replace("[ERP_USERNAME]", UserName.ToString());
                        oMail.HtmlBody = oMail.HtmlBody.Replace("[PASSWORD]", "Sliit@123");
                        SmtpServer oServer = new SmtpServer("smtp.office365.com");
                        oServer.User = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]);
                        oServer.Password = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"]);
                        oServer.Port = 587;
                        oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
                        EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
                        oSmtp.SendMail(oServer, oMail);
                    }
                }
                //Added by Roshan Patil 19-05-2022
                // int IDNONew = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "EMAILID="+txtEmail.Text+"AND STUDENTMOBILE="+ txtMobile.Text +""));
                ViewState["userno"] = 0;//Convert.ToInt32(objCommon.LookUp("User_Acc", "UA_NO", "UA_IDNO= " + IDNO));
                DocumentControllerAcad objDoc = new DocumentControllerAcad();
                DocumentAcad objDocno = new DocumentAcad();
                byte[] StudentPhoto = null;

                string path = "", filenames = "", docnos = "", docnames = "", existsfile = ""; int Count = 0;
                path = System.Configuration.ConfigurationManager.AppSettings["UploadDocPath"];
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                foreach (ListViewDataItem item in lvDocument.Items)
                {
                    Label docno = item.FindControl("lblDocNo") as Label;
                    Label DocName = item.FindControl("lblname") as Label;
                    Label lbluploadpic = item.FindControl("lbluploadpic") as Label;
                    FileUpload fuDocument = item.FindControl("fuDocument") as FileUpload;
                    if (docno.Text == "0")
                    {
                        if (fuDocument.HasFile)
                        {
                            string ext = System.IO.Path.GetExtension(fuDocument.FileName);
                            HttpPostedFile FileSize = fuDocument.PostedFile;
                            if (FileSize.ContentLength <= 1000000)
                            {
                                if (ext == ".png" || ext == ".PNG" || ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG")
                                {
                                    Count++;
                                    existsfile = path + Convert.ToInt32(IDNO) + "_" + "Student_DirectEntry" + "_" + fuDocument.FileName;
                                    FileInfo file = new FileInfo(existsfile);
                                    if (file.Exists)//check file exsit or not  
                                    {
                                        file.Delete();
                                    }
                                    //fuDocument.PostedFile.SaveAs(path + Path.GetFileName(Convert.ToInt32(Session["idno"]) + "_" + fuDocument.FileName));
                                    StudentPhoto = objCommon.GetImageData(fuDocument);
                                    //filenames += fuDocument.FileName + '$';
                                    //docnos += docno.Text + '$';
                                    //docnames += DocName.Text + '$'; 
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.Page, "Student photo only formats are allowed : png,jpg,jpeg !!!", this.Page);
                                    return;
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage(this.Page, "File Size should be less than 1 MB !!!", this.Page);
                                return;
                            }
                        }
                        else
                        {
                            if (Convert.ToString(Session["STUDENTPHOTO"]) != string.Empty || Convert.ToString(Session["STUDENTPHOTO"]) != null || lbluploadpic.Text == "")
                            {
                                StudentPhoto = (byte[])Session["STUDENTPHOTO"];
                                //objCommon.DisplayMessage(this.Page, "Please select at least one document for upload !!!", this.Page);
                            }

                            else
                            {
                                objCommon.DisplayMessage(this.Page, "Upload student photo document is mandatory field !!!", this.Page);
                                return;
                            }
                        }

                    }
                    else
                    {
                        if (docno.Text != "")
                        {
                            Count++;
                            if (fuDocument.HasFile)
                            {
                                HttpPostedFile FileSize = fuDocument.PostedFile;
                                string ext = System.IO.Path.GetExtension(fuDocument.FileName);
                                if (ext == ".pdf" || ext == ".PDF")
                                {
                                    if (FileSize.ContentLength <= 1000000)
                                    {
                                        string FileName = fuDocument.FileName.ToString();
                                        Count++;
                                        existsfile = path + Convert.ToInt32(IDNO) + "_" + "_Student_DirectEntry" + "_" + fuDocument.FileName;
                                        FileInfo file = new FileInfo(existsfile);
                                        if (file.Exists)//check file exsit or not  
                                        {
                                            file.Delete();
                                        }
                                        int retval = Blob_Upload(blob_ConStr, blob_ContainerName, Convert.ToInt32(IDNO) + "_Student_DirectEntry" + "_doc_" + docno.Text + "_" + DocName.Text, fuDocument);
                                        if (retval == 0)
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                            return;
                                        }
                                        //fuDocument.PostedFile.SaveAs(path + Path.GetFileName(Convert.ToInt32(Session["idno"]) + "_" + fuDocument.FileName));
                                        filenames += Convert.ToInt32(IDNO) + "_Student_DirectEntry" + "_doc_" + docno.Text + "_" + DocName.Text + ext + '$';
                                        docnos += docno.Text + '$';
                                        docnames += DocName.Text + '$';
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(this.Page, "File Size should be less than 1 MB !!!", this.Page);
                                        return;
                                    }
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.Page, "Student document only formats are allowed : pdf !!!", this.Page);
                                    return;
                                }
                            }
                        }
                    }
                }
                if (Count > 0)
                {
                    filenames = filenames.TrimEnd('$');
                    docnos = docnos.TrimEnd('$');
                    docnames = docnames.TrimEnd('$');
                    string REGNO = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO=" + IDNO);

                    CustomStatus cs = CustomStatus.Others;
                    cs = (CustomStatus)objDoc.AddMultipleDocStd(Convert.ToInt32(IDNO), Convert.ToInt32(ViewState["userno"]), filenames, docnos, docnames, path, Convert.ToInt32(ViewState["sessionno"]), StudentPhoto);

                    if (idno != 2627 && idno > 1)
                    {
                        objCommon.DisplayMessage(this.Page, "Registration is successfully completed,Your Student ID is " + REGNO, this.Page);
                        this.ShowReport("StudentRegistrationrpt", "StudentRegistrationrpt.rpt", idno);
                        ClearAllFields();
                        bindlist();
                        return;
                    }
                    //if (idno == 2627)
                    //{

                    //    objCommon.DisplayMessage(this.Page, "Record Updated Sucessfully", this.Page);

                    //    ClearAllFields();
                    //    return;
                    //}
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please select at least one document for upload !!!", this.Page);
                }

            }
            else if (idno == 2627)
            {
                objCommon.DisplayMessage(this.Page, "Mobile Number , Email or Nic Already Registrated.", this.Page);
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Error.", this.Page);
                return;
            }

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "domain.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport(string reportTitle, string rptName, int studentNo)
    {
        try
        {
            //string colgname = "SLIIT";

            //string exporttype = "pdf";
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
            ////string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";          
            //url += "exporttype=" + exporttype;

            //url += "&filename=" + colgname + "." + exporttype;

           // string url = "https://erptest.sliit.lk/Reports/CommonReport.aspx?";
            //string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNO=" + Convert.ToInt32(studentNo);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Semester_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindlistserch();
    }
    private void bindlistserch()
    {

        string categoryy = "";
        //categoryy = "regno";
        StudentController objSC = new StudentController();

        if (rdselect.SelectedValue == "0")
        {
            categoryy = "name";

        }
        else if (rdselect.SelectedValue == "1")
        {
            categoryy = "EMAILID";
        }

        else if (rdselect.SelectedValue == "2")
        {
            categoryy = "regno";
        }

        DataSet ds = objSC.NewStudentDetails(txtSearch.Text, categoryy);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = ds.Tables[0];
            lvStudent.DataBind();

            lvBankDetails.DataSource = null;
            lvBankDetails.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : 0";
        }
        ds.Dispose();
    }
    protected void btnCancelModal_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        lblNoRecords.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
    }
    protected void lnkId_Click(object sender, EventArgs e)
    {
        //ClearAllControls();

        LinkButton lnk = sender as LinkButton;
        Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;
        Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
        Session["stuinfofullname"] = lnk.Text.Trim();
        ViewState["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
        ViewState["USER_NO"] = lnk.ToolTip;
        //txtIDNo.Text = lblenrollno.Text.Trim();
        if (lblenrollno.Text == "" || lblenrollno.Text == string.Empty)
        {
            objCommon.DisplayMessage(this.Page, "", this.Page);
            return;
        }
        else
        {

            int userno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DISTINCT IDNO", "IDNO=" + Convert.ToInt32(ViewState["stuinfoidno"])));
            ViewState["USER_NO"] = userno;
            FillDropDown();
            ShowStudentDetails();
            binddoclist();
            BindBankdetails();
            pnllst.Visible = false;
            lvSemester.Visible = false;
            lvSemester.DataSource = null;
            lvSemester.DataBind();
            Session["Row"] = null;
            ViewState["edit"] = 1;
        }
    }
    private void BindBankdetails()
    {
        try
        {
            string SP_Name2 = "PKG_ACD_GET_BANK_DETAILS";
            string SP_Parameters2 = "@P_IDNO";
            string Call_Values2 = "" + Convert.ToInt32(ViewState["stuinfoidno"]) + "";
            DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (dsStudList.Tables[0].Rows.Count > 0)
            {

                slipview.Visible = true;
                Lvbank.DataSource = dsStudList;
                Lvbank.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void ShowStudentDetails()
    {
        try
        {
            ViewState["update"] = "update";
            int idno = 0;
            if (Session["usertype"].ToString() != "2")     //Student 
            {
                FillDropDown();
                idno = Convert.ToInt32(ViewState["stuinfoidno"]);
                DataSet dsStudent = objSC.GetNewStudentDetails(idno);
                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    btnreport.Visible = true;
                    DataRow dr = dsStudent.Tables[0].Rows[0];

                    string fullName = dr["FIRSTNAME"].ToString();
                    string lastname = dr["LASTNAME"].ToString();
                    string slip = dr["SLIP"].ToString();
                    ViewState["SLIP"] = dr["SLIP"].ToString();
                    txtFirstName.Text = fullName;
                    txtLastName.Text = lastname;
                    string fullname = objCommon.LookUp("ACD_STUDENT", "STUDNAME", "IDNO=" + idno);
                    txtfullname.Text = fullname;
                    txtNameInitial.Text = dr["NAME_WITH_INITIAL"].ToString() == string.Empty ? string.Empty : dr["NAME_WITH_INITIAL"].ToString();
                    txtEmail.Text = dr["EMAILID"].ToString() == string.Empty ? string.Empty : dr["EMAILID"].ToString();
                    txtALIndex.Text = dr["ALINDEXNO"].ToString() == string.Empty ? string.Empty : dr["ALINDEXNO"].ToString();
                    Txtolindex.Text = dr["OLINDEXNO"].ToString() == string.Empty ? string.Empty : dr["OLINDEXNO"].ToString();

                    ddlMobileCode.SelectedValue = dr["MOBILECODE"].ToString() == string.Empty ? string.Empty : dr["MOBILECODE"].ToString();
                    txtMobile.Text = dr["STUDENTMOBILE"].ToString() == string.Empty ? string.Empty : dr["STUDENTMOBILE"].ToString();
                    ddlHomeMobileCode.SelectedValue = dr["HOMETELEPHONECODE"].ToString() == string.Empty ? string.Empty : dr["HOMETELEPHONECODE"].ToString();
                    txtNIC.Text = dr["NICNO"].ToString() == string.Empty ? string.Empty : dr["NICNO"].ToString();
                    txtPassportNo.Text = dr["PASSPORTNO"].ToString() == string.Empty ? string.Empty : dr["PASSPORTNO"].ToString();
                    txtDateOfBirth.Text = dr["DOB"].ToString() == string.Empty ? string.Empty : dr["DOB"].ToString();
                    txtHomeTel.Text = dr["TELPHONENO"].ToString() == string.Empty ? string.Empty : dr["TELPHONENO"].ToString();
                    txtPassportNo.Text = dr["PASSPORTNO"].ToString() == string.Empty ? string.Empty : dr["PASSPORTNO"].ToString();
                    rdoCitizenType.SelectedValue = dr["CITIZEN_NO"].ToString() == string.Empty ? string.Empty : dr["CITIZEN_NO"].ToString();
                    rdoLeftRight.SelectedValue = dr["IDENTI_MARK"].ToString() == string.Empty ? string.Empty : dr["IDENTI_MARK"].ToString();
                    rdoGender.SelectedValue = dr["SEX"].ToString() == string.Empty ? string.Empty : dr["SEX"].ToString();

                    txtPerAddress.Text = dr["PADDRESS"].ToString() == string.Empty ? string.Empty : dr["PADDRESS"].ToString();
                    ddlPerContry.SelectedValue = dr["LCOUNTRY"].ToString() == string.Empty ? string.Empty : dr["LCOUNTRY"].ToString();
                    ddlPerProvince.SelectedValue = dr["LSTATE"].ToString() == string.Empty ? string.Empty : dr["LSTATE"].ToString();
                    ddlPerProvince_SelectedIndexChanged(new object(), new EventArgs());
                    ddlPerDisctrict.SelectedValue = dr["LDISTRICT"].ToString() == string.Empty ? string.Empty : dr["LDISTRICT"].ToString();

                    txtPermAddress.Text = dr["LADDRESS"].ToString() == string.Empty ? string.Empty : dr["LADDRESS"].ToString();
                    ddlCountry.SelectedValue = dr["PCOUNTRY"].ToString() == string.Empty ? string.Empty : dr["PCOUNTRY"].ToString();
                    ddlProvince.SelectedValue = dr["PSTATE"].ToString() == string.Empty ? string.Empty : dr["PSTATE"].ToString();
                    ddlProvince_SelectedIndexChanged(new object(), new EventArgs());
                    ddlDistrict.SelectedValue = dr["PDISTRICT"].ToString() == string.Empty ? string.Empty : dr["PDISTRICT"].ToString();

                    txtMiddleName.Text = dr["FATHERNAME"].ToString() == string.Empty ? string.Empty : dr["FATHERNAME"].ToString();
                    txtPersonalEmail.Text = dr["FATHER_EMAIL"].ToString() == string.Empty ? string.Empty : dr["FATHER_EMAIL"].ToString();
                    ddlConCode.SelectedValue = dr["FMOBILECODE"].ToString() == string.Empty ? string.Empty : dr["FMOBILECODE"].ToString();
                    //ddlConCode.SelectedValue = dr["FATHERMOBILE"].ToString() == string.Empty ? string.Empty : dr["FATHERMOBILE"].ToString();


                    ddlIntake.SelectedValue = dr["ADMBATCH"].ToString() == string.Empty ? string.Empty : dr["ADMBATCH"].ToString();
                    ddlStudyLevel.SelectedValue = dr["UGPGOT"].ToString() == string.Empty ? string.Empty : dr["UGPGOT"].ToString();
                    ddlStudyLevel_SelectedIndexChanged(new object(), new EventArgs());
                    ddlFacultySchoolName.SelectedValue = dr["COLLEGE_ID"].ToString() == string.Empty ? string.Empty : dr["COLLEGE_ID"].ToString();
                    ddlFacultySchoolName_SelectedIndexChanged(new object(), new EventArgs());
                    ddlProgram.SelectedValue = dr["PROGRAM"].ToString() == string.Empty ? string.Empty : dr["PROGRAM"].ToString();

                    ddlCampus.SelectedValue = dr["CAMPUSNO"].ToString() == string.Empty ? string.Empty : dr["CAMPUSNO"].ToString();
                    ddlWeekdayWeekend.SelectedValue = dr["WEEKSNOS"].ToString() == string.Empty ? string.Empty : dr["WEEKSNOS"].ToString();
                    ddlSemester.SelectedValue = dr["SEMESTERNO"].ToString() == string.Empty ? string.Empty : dr["SEMESTERNO"].ToString();
                    txtRemark.Text = dr["remark"].ToString() == string.Empty ? string.Empty : dr["remark"].ToString();
                    Txtaptimarks.Text = dr["APTIMARKS"].ToString() == string.Empty ? string.Empty : dr["APTIMARKS"].ToString();

                    //if (ddlFacultySchoolName.SelectedValue == "7" || ddlFacultySchoolName.SelectedValue == "8")
                    //{
                    paymentdetails.Visible = true;
                    fileupload.Visible = false;
                    divdemand.Visible = true;                 
                    bank.Visible = true;
                    fileupload.Visible = true;
                    receipt.Visible = true;
                    amt.Visible = true;
                    btnadd.Visible = true;
                    slipview.Visible = true;
                    //if (slip != "")
                    //{
                    //    slipview.Visible = true;
                    //}

                    //else
                    //{
                    //    slipview.Visible = false;
                    //}
                    //}
                    //else
                    //{
                    //    paymentdetails.Visible = true;
                    //    receipt.Visible = false;
                    //    bank.Visible = false;
                    //    amt.Visible = false;

                    //}
                    //paymentdetails.Visible = true;
                    //divdemand.Visible = true;

                    TxtDemand.Text = dr["TOTAL_AMT"].ToString() == string.Empty ? string.Empty : dr["TOTAL_AMT"].ToString();
                    //ddlbankdetails.SelectedValue = dr["BANK_NO"].ToString() == string.Empty ? string.Empty : dr["BANK_NO"].ToString();
                    //TxtReceipt.Text = dr["REC_NO"].ToString() == string.Empty ? string.Empty : dr["REC_NO"].ToString();
                    //Txtamt.Text = dr["DCRAMOUNT"].ToString() == string.Empty ? string.Empty : dr["DCRAMOUNT"].ToString();

                    //alol
                    txtalschool.Text = dr["ALSCHOOL_NAME"].ToString() == string.Empty ? string.Empty : dr["ALSCHOOL_NAME"].ToString();
                    txtolschool.Text = dr["OLSCHOOL_NAME"].ToString() == string.Empty ? string.Empty : dr["OLSCHOOL_NAME"].ToString();
                    ddlALExamType.SelectedValue = dr["AL_TYPE"].ToString() == string.Empty ? string.Empty : dr["AL_TYPE"].ToString();
                    ddlALExamType_SelectedIndexChanged(new object(), new EventArgs());

                    ddlALStream.SelectedValue = dr["STREAMNO"].ToString() == string.Empty ? string.Empty : dr["STREAMNO"].ToString();
                    ddlALPasses.SelectedValue = dr["ATTEMPTNO"].ToString() == string.Empty ? string.Empty : dr["ATTEMPTNO"].ToString();
                    ddlALSubject1.SelectedValue = dr["SUBJECT1"].ToString() == string.Empty ? string.Empty : dr["SUBJECT1"].ToString();
                    ddlALSubject1_SelectedIndexChanged(new object(), new EventArgs());
                    ddlALGrade1.SelectedValue = dr["GRADE1"].ToString() == string.Empty ? string.Empty : dr["GRADE1"].ToString();
                    ddlALSubject2.SelectedValue = dr["SUBJECT2"].ToString() == string.Empty ? string.Empty : dr["SUBJECT2"].ToString();
                    ddlALSubject2_SelectedIndexChanged(new object(), new EventArgs());
                    ddlALGrade2.SelectedValue = dr["GRADE2"].ToString() == string.Empty ? string.Empty : dr["GRADE2"].ToString();
                    ddlALSubject3.SelectedValue = dr["SUBJECT3"].ToString() == string.Empty ? string.Empty : dr["SUBJECT3"].ToString();
                    ddlALSubject3_SelectedIndexChanged(new object(), new EventArgs());
                    ddlALGrade3.SelectedValue = dr["GRADE3"].ToString() == string.Empty ? string.Empty : dr["GRADE3"].ToString();
                    ddlALSubject4.SelectedValue = dr["SUBJECT4"].ToString() == string.Empty ? string.Empty : dr["SUBJECT4"].ToString();

                    ddlALGrade4.SelectedValue = dr["GRADE4"].ToString() == string.Empty ? string.Empty : dr["GRADE4"].ToString();
                    ddlOLExamType.SelectedValue = dr["OLTYPE"].ToString() == string.Empty ? string.Empty : dr["OLTYPE"].ToString();
                    ddlOLExamType_SelectedIndexChanged(new object(), new EventArgs());
                    ddlOLStream.SelectedValue = dr["OLSTREAMNO"].ToString() == string.Empty ? string.Empty : dr["OLSTREAMNO"].ToString();
                    ddlOLPasses.SelectedValue = dr["OLATTEMPTNO"].ToString() == string.Empty ? string.Empty : dr["OLATTEMPTNO"].ToString();
                    ddlOLSubject1.SelectedValue = dr["OLSUBJECT1"].ToString() == string.Empty ? string.Empty : dr["OLSUBJECT1"].ToString();
                    ddlOLSubject1_SelectedIndexChanged(new object(), new EventArgs());
                    ddlOLGrade1.SelectedValue = dr["OLGRADE1"].ToString() == string.Empty ? string.Empty : dr["OLGRADE1"].ToString();
                    ddlOLSubject2.SelectedValue = dr["OLSUBJECT2"].ToString() == string.Empty ? string.Empty : dr["OLSUBJECT2"].ToString();
                    ddlOLSubject2_SelectedIndexChanged(new object(), new EventArgs());
                    ddlOLGrade2.SelectedValue = dr["OLGRADE2"].ToString() == string.Empty ? string.Empty : dr["OLGRADE2"].ToString();
                    ddlOLSubject3.SelectedValue = dr["OLSUBJECT3"].ToString() == string.Empty ? string.Empty : dr["OLSUBJECT3"].ToString();
                    ddlOLSubject3_SelectedIndexChanged(new object(), new EventArgs());
                    ddlOLGrade3.SelectedValue = dr["OLGRADE3"].ToString() == string.Empty ? string.Empty : dr["OLGRADE3"].ToString();
                    ddlOLSubject4.SelectedValue = dr["OLSUBJECT4"].ToString() == string.Empty ? string.Empty : dr["OLSUBJECT4"].ToString();
                    ddlOLSubject4_SelectedIndexChanged(new object(), new EventArgs());

                    ddlOLGrade4.SelectedValue = dr["OLGRADE4"].ToString() == string.Empty ? string.Empty : dr["OLGRADE4"].ToString();

                    ddlOLSubject5.SelectedValue = dr["OLSUBJECT5"].ToString() == string.Empty ? string.Empty : dr["OLSUBJECT5"].ToString();
                    ddlOLSubject5_SelectedIndexChanged(new object(), new EventArgs());
                    ddlOLGrade5.SelectedValue = dr["OLGRADE5"].ToString() == string.Empty ? string.Empty : dr["OLGRADE5"].ToString();

                    ddlOLSubject6.SelectedValue = dr["OLSUBJECT6"].ToString() == string.Empty ? string.Empty : dr["OLSUBJECT6"].ToString();
                    ddlOLGrade6.SelectedValue = dr["OLGRADE6"].ToString() == string.Empty ? string.Empty : dr["OLGRADE6"].ToString();
                    ddlsource.SelectedValue = dr["SOURCETYPENO"].ToString() == string.Empty ? string.Empty : dr["SOURCETYPENO"].ToString();

                    if (ddlALExamType.SelectedValue == "0")
                    {
                        alolselection.SelectedValue = "2";
                        aldetails.Visible = false;
                    }
                    else
                    {
                        alolselection.ClearSelection();
                        aldetails.Visible = true;
                    }

                }
                else
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Not Found.", this.Page);
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.Page, "Record Not Found.This Page is not for Student Login!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.ShowStudentDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlDistrict, "ACD_DISTRICT", "DISTINCT DISTRICTNO", "DISTRICTNAME", "STATENO=" + ddlProvince.SelectedValue, "DISTRICTNO");
        }
        catch (Exception ex)
        {

        }
    }
    protected void ClearAllFields()
    {  //Personal Details
        txtfullname.Text = "";
        rdselect.ClearSelection();
        alolselection.ClearSelection();
        ddlHomeMobileCode.SelectedValue = "212";
        ddlMobileCode.SelectedValue = "212";
        slipview.Visible = false;
        txtALIndex.Text = "";
        Txtolindex.Text = "";
        Txtaptimarks.Text = "";
        txtFirstName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        txtNameInitial.Text = string.Empty;
        txtEmail.Text = string.Empty;
        ddlMobileCode.SelectedIndex = 0;
        txtMobile.Text = string.Empty;
        ddlHomeMobileCode.SelectedIndex = 0;
        txtHomeTel.Text = string.Empty;
        txtNIC.Text = string.Empty;
        txtPassportNo.Text = string.Empty;
        txtDateOfBirth.Text = string.Empty;
        rdoGender.ClearSelection();
        rdoCitizenType.ClearSelection();
        rdoLeftRight.ClearSelection();

        //Address
        txtPermAddress.Text = string.Empty;
        ddlCountry.SelectedIndex = 0;
        ddlProvince.SelectedIndex = 0;
        ddlDistrict.Items.Clear();
        ddlDistrict.Items.Insert(0, new ListItem("Please Select", "0"));

        //Education Details
        ddlALExamType.SelectedIndex = 0;
        ddlALStream.Items.Clear();
        ddlALStream.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlALPasses.SelectedIndex = 0;
        ddlOLExamType.SelectedIndex = 0;
        ddlOLStream.Items.Clear();
        ddlOLStream.Items.Insert(0, new ListItem("Please Select", "0"));

        ddlOLPasses.SelectedIndex = 0;

        ddlALSubject1.Items.Clear();
        ddlALSubject1.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlALGrade1.Items.Clear();
        ddlALGrade1.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlALSubject2.Items.Clear();
        ddlALSubject2.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlALGrade2.Items.Clear();
        ddlALGrade2.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlALSubject3.Items.Clear();
        ddlALSubject3.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlALGrade3.Items.Clear();
        ddlALGrade3.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlALSubject4.Items.Clear();
        ddlALSubject4.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlALGrade4.Items.Clear();
        ddlALGrade4.Items.Insert(0, new ListItem("Please Select", "0"));

        ddlOLSubject1.Items.Clear();
        ddlOLSubject1.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlOLGrade1.Items.Clear();
        ddlOLGrade1.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlOLSubject2.Items.Clear();
        ddlOLSubject2.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlOLGrade2.Items.Clear();
        ddlOLGrade2.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlOLSubject3.Items.Clear();
        ddlOLSubject3.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlOLGrade3.Items.Clear();
        ddlOLGrade3.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlOLSubject4.Items.Clear();
        ddlOLSubject4.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlOLGrade4.Items.Clear();
        ddlOLGrade4.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlOLSubject5.Items.Clear();
        ddlOLSubject5.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlOLGrade5.Items.Clear();
        ddlOLGrade5.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlOLSubject6.Items.Clear();
        ddlOLSubject6.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlOLGrade6.Items.Clear();
        ddlOLGrade6.Items.Insert(0, new ListItem("Please Select", "0"));

        //Faculty Details
        ddlFacultySchoolName.SelectedIndex = 0;
        ddlStudyLevel.Items.Clear();
        ddlStudyLevel.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlProgram.Items.Clear();
        ddlProgram.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlCampus.SelectedIndex = 0;
        ddlWeekdayWeekend.SelectedIndex = 0;
        ddlIntake.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        txtRemark.Text = string.Empty;
        txtEnrollmentNo.Text = string.Empty;
        paymentdetails.Visible = false;
        DataSet ds = null;
        ddlsource.SelectedIndex = 0;
        txtSearch.Text = "";
        lvStudent.DataBind();
        lvStudent.DataSource = null;
        lblNoRecords.Text = "";
        ds = objdocContr.GetDoclistStud(0);

    }
    protected void ddlALExamType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddlALExamType.SelectedValue) > 0)
            {
                if (ddlALExamType.SelectedValue == "2")
                {
                    objCommon.FillDropDownList(ddlALStream, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO = 8", "STREAMNAME");
                    ddlALStream.SelectedIndex = 1;
                }
                else if (ddlALExamType.SelectedValue == "3")
                {
                    objCommon.FillDropDownList(ddlALStream, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO = 9", "STREAMNAME");
                    ddlALStream.SelectedIndex = 1;
                }
                else if (ddlALExamType.SelectedValue == "4")
                {
                    objCommon.FillDropDownList(ddlALStream, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO = 7", "STREAMNAME");
                    ddlALStream.SelectedIndex = 1;
                }
                else
                {
                    objCommon.FillDropDownList(ddlALStream, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO NOT IN(7,8,9)", "STREAMNAME");
                }

                objCommon.FillDropDownList(ddlALSubject1, "AL_COURSES", "ID", "AL_COURSES", "ACTIVE = 1 AND ALTYPENO =" + ddlALExamType.SelectedValue + "", "AL_COURSES");
                objCommon.FillDropDownList(ddlALGrade1, "ACD_AL_GRADES", "DISTINCT ID", "GRADES", "ACTIVE = 1 AND ALTYPENO = " + ddlALExamType.SelectedValue + "", "ID");
                objCommon.FillDropDownList(ddlALGrade2, "ACD_AL_GRADES", "DISTINCT ID", "GRADES", "ACTIVE = 1 AND ALTYPENO = " + ddlALExamType.SelectedValue + "", "ID");
                objCommon.FillDropDownList(ddlALGrade3, "ACD_AL_GRADES", "DISTINCT ID", "GRADES", "ACTIVE = 1 AND ALTYPENO = " + ddlALExamType.SelectedValue + "", "ID");
                objCommon.FillDropDownList(ddlALGrade4, "ACD_AL_GRADES", "DISTINCT ID", "GRADES", "ACTIVE = 1 AND ALTYPENO = " + ddlALExamType.SelectedValue + "", "ID");
            }
            else
            {
                ddlALStream.SelectedIndex = 0;

                ddlALSubject1.SelectedIndex = 0;

                ddlALSubject2.SelectedIndex = 0;

                ddlALSubject3.SelectedIndex = 0;

                ddlALSubject4.SelectedIndex = 0;
                ddlALGrade1.SelectedIndex = 0;

                ddlALGrade2.SelectedIndex = 0;

                ddlALGrade3.SelectedIndex = 0;

                ddlALGrade4.SelectedIndex = 0;

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "domain.ddlALExamType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlALSubject1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlALSubject1.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlALSubject2, "AL_COURSES", "DISTINCT ID", " AL_COURSES", "ACTIVE = 1 AND ID <>" + ddlALSubject1.SelectedValue + "AND ALTYPENO=" + ddlALExamType.SelectedValue, "AL_COURSES");
        }
        else
        {
            ddlALSubject2.SelectedIndex = 0;
            //ddlALSubject2.Items.Insert(0, "Please Select");
        }
    }
    protected void ddlALSubject2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlALSubject2.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlALSubject3, "AL_COURSES", "DISTINCT ID", " AL_COURSES", "ACTIVE = 1 AND ID NOT IN (" + ddlALSubject1.SelectedValue + "," + ddlALSubject2.SelectedValue + ") AND ALTYPENO=" + ddlALExamType.SelectedValue, "AL_COURSES");
        }
        else
        {
            ddlALSubject3.SelectedIndex = 0;
            // ddlALSubject3.Items.Insert(0, "Please Select");
        }
    }
    protected void ddlALSubject3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlALSubject3.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlALSubject4, "AL_COURSES", "DISTINCT ID", " AL_COURSES", "ACTIVE = 1 AND ID NOT IN (" + ddlALSubject1.SelectedValue + "," + ddlALSubject2.SelectedValue + "," + ddlALSubject3.SelectedValue + ") AND ALTYPENO=" + ddlALExamType.SelectedValue, "AL_COURSES");
        }
        else
        {
            ddlALSubject4.SelectedIndex = 0;
            //ddlALSubject4.Items.Insert(0, "Please Select");
        }
    }
    protected void ddlOLExamType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddlOLExamType.SelectedValue) > 0)
            {
                if (ddlOLExamType.SelectedValue == "2")
                {
                    objCommon.FillDropDownList(ddlOLStream, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO = 8", "STREAMNAME");
                    ddlOLStream.SelectedIndex = 1;
                }
                else if (ddlOLExamType.SelectedValue == "3")
                {
                    objCommon.FillDropDownList(ddlOLStream, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO = 9", "STREAMNAME");
                    ddlOLStream.SelectedIndex = 1;
                }
                else if (ddlOLExamType.SelectedValue == "4")
                {
                    objCommon.FillDropDownList(ddlOLStream, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO = 7", "STREAMNAME");
                    ddlOLStream.SelectedIndex = 1;
                }
                else
                {
                    objCommon.FillDropDownList(ddlOLStream, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO NOT IN(7,8,9)", "STREAMNAME");
                }

                objCommon.FillDropDownList(ddlOLSubject1, "OL_COURSES", "ID", "OL_COURSES", "ACTIVE = 1 AND OLTYPENO=" + ddlOLExamType.SelectedValue + "", "OL_COURSES");
                objCommon.FillDropDownList(ddlOLGrade1, "ACD_OL_GRADES", "ID", "GRADES", "ACTIVE = 1 AND ALTYPENO=" + ddlOLExamType.SelectedValue + "", "ID");
                objCommon.FillDropDownList(ddlOLGrade2, "ACD_OL_GRADES", "ID", "GRADES", "ACTIVE = 1 AND ALTYPENO=" + ddlOLExamType.SelectedValue + "", "ID");
                objCommon.FillDropDownList(ddlOLGrade3, "ACD_OL_GRADES", "ID", "GRADES", "ACTIVE = 1 AND ALTYPENO=" + ddlOLExamType.SelectedValue + "", "ID");
                objCommon.FillDropDownList(ddlOLGrade4, "ACD_OL_GRADES", "ID", "GRADES", "ACTIVE = 1 AND ALTYPENO=" + ddlOLExamType.SelectedValue + "", "ID");
                objCommon.FillDropDownList(ddlOLGrade5, "ACD_OL_GRADES", "ID", "GRADES", "ACTIVE = 1 AND ALTYPENO=" + ddlOLExamType.SelectedValue + "", "ID");
                objCommon.FillDropDownList(ddlOLGrade6, "ACD_OL_GRADES", "ID", "GRADES", "ACTIVE = 1 AND ALTYPENO=" + ddlOLExamType.SelectedValue + "", "ID");
            }
            else
            {
                ddlOLStream.SelectedIndex = 0;
                //ddlOLStream.Items.Insert(0, "Please Select");
                ddlOLSubject1.SelectedIndex = 0;

                ddlOLSubject2.SelectedIndex = 0;

                ddlOLSubject3.SelectedIndex = 0;

                ddlOLSubject4.SelectedIndex = 0;

                ddlOLSubject5.SelectedIndex = 0;

                ddlOLSubject6.SelectedIndex = 0;

                ddlOLGrade1.SelectedIndex = 0;

                ddlOLGrade2.SelectedIndex = 0;

                ddlOLGrade3.SelectedIndex = 0;

                ddlOLGrade4.SelectedIndex = 0;

                ddlOLGrade5.SelectedIndex = 0;

                ddlOLGrade6.SelectedIndex = 0;

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "domain.ddlOLExamType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void ddlOLSubject1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlOLSubject1.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlOLSubject2, "OL_COURSES", "DISTINCT ID", " OL_COURSES", "ACTIVE = 1 AND ID <>" + ddlOLSubject1.SelectedValue + "AND OLTYPENO=" + ddlOLExamType.SelectedValue, "OL_COURSES");
        }
        else
        {
            ddlOLSubject2.SelectedIndex = 0;

        }
    }
    protected void ddlOLSubject2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlOLSubject2.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlOLSubject3, "OL_COURSES", "DISTINCT ID", " OL_COURSES", "ACTIVE = 1 AND ID NOT IN (" + ddlOLSubject1.SelectedValue + "," + ddlOLSubject2.SelectedValue + ")AND OLTYPENO=" + ddlOLExamType.SelectedValue, "OL_COURSES");
        }
        else
        {
            ddlOLSubject3.SelectedIndex = 0;

        }
    }
    protected void ddlOLSubject3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlOLSubject3.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlOLSubject4, "OL_COURSES", "DISTINCT ID", " OL_COURSES", "ACTIVE = 1 AND ID NOT IN(" + ddlOLSubject1.SelectedValue + "," + ddlOLSubject2.SelectedValue + "," + ddlOLSubject3.SelectedValue + ")AND OLTYPENO=" + ddlOLExamType.SelectedValue, "OL_COURSES");
        }
        else
        {
            ddlOLSubject4.SelectedIndex = 0;

        }
    }
    protected void ddlOLSubject4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlOLSubject4.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlOLSubject5, "OL_COURSES", "DISTINCT ID", " OL_COURSES", "ACTIVE = 1 AND ID NOT IN(" + ddlOLSubject1.SelectedValue + "," + ddlOLSubject2.SelectedValue + "," + ddlOLSubject3.SelectedValue + "," + ddlOLSubject4.SelectedValue + ")AND OLTYPENO=" + ddlOLExamType.SelectedValue, "OL_COURSES");
        }
        else
        {
            ddlOLSubject5.SelectedIndex = 0;

        }
    }
    protected void ddlOLSubject5_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlOLSubject5.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlOLSubject6, "OL_COURSES", "DISTINCT ID", " OL_COURSES", "ACTIVE = 1 AND ID  NOT IN(" + ddlOLSubject1.SelectedValue + "," + ddlOLSubject2.SelectedValue + "," + ddlOLSubject3.SelectedValue + "," + ddlOLSubject4.SelectedValue + "," + ddlOLSubject5.SelectedValue + ") AND OLTYPENO=" + ddlOLExamType.SelectedValue, "OL_COURSES");
        }
        else
        {
            ddlOLSubject6.SelectedIndex = 0;

        }
    }
    //Document Details

    public void bindlist()
    {
        try
        {
            //lblfile.Text = "For Image Only formats Are Allowed : png,jpg,jpeg, For Remaining PDF Only";
            DataSet ds1 = objCommon.FillDropDown("ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO LEFT JOIN ACD_PREFERENCE P ON P.PREID = UR.BRANCH_PREF ", "UR.DEGREENO,UR.COLLEGE_ID,UR.BRANCHNO,UR.BRANCH_PREF", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE '' END  DEGREENAME",
                "isnull(Applied,0)<>0 AND isnull(DocUndertaking,0) <>0 AND UR.USERNO =" + Session["userno"] + "", "D.DEGREENAME");
            if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {
                int DEGREE = Convert.ToInt32(ds1.Tables[0].Rows[0]["DEGREENO"]);
                int clg = Convert.ToInt32(ds1.Tables[0].Rows[0]["COLLEGE_ID"]);
                int branch = Convert.ToInt32(ds1.Tables[0].Rows[0]["BRANCHNO"]);
                int pref = Convert.ToInt32(ds1.Tables[0].Rows[0]["BRANCH_PREF"]);

                //objCommon.FillDropDownList(ddlSchClg, "ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID", "convert(varchar(10),UR.COLLEGE_ID) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' DEGREENAME", " UR.DEGREENO =" + DEGREE + "AND UR.COLLEGE_ID=" + clg + "AND UR.USERNO =" + Session["username"] + "", "D.DEGREENAME");
                DataSet VALUE = objCommon.FillDropDown("ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO LEFT JOIN ACD_PREFERENCE P ON P.PREID = UR.BRANCH_PREF INNER JOIN ACD_STUDENT S  ON (S.USERNO=UR.USERNO AND S.DEGREENO=UR.DEGREENO AND S.COLLEGE_ID = UR.COLLEGE_ID)", "convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE '' END  DEGREENAME", " UR.USERNO =" + Session["username"] + " AND ISNULL(S.ADMCAN,0)=0 ", "D.DEGREENAME"); // D.DEGREENO =" + DEGREE + "  AND UR.BRANCHNO=" + branch + "AND UR.BRANCH_PREF=" + pref + "  AND UR.COLLEGE_ID=" + clg + " AND

                ///  ddlSchClg.SelectedItem.Text = VALUE.Tables[0].Rows[0]["DEGREENAME"].ToString(); // ROSHAN on 05-08-2020 for label Display Previous Course s
                if (VALUE.Tables[0].Rows.Count > 0)
                {
                    //lblAdmittedProgram.Text = VALUE.Tables[0].Rows[0]["DEGREENAME"].ToString(); //
                }
                else
                {
                    //divLastAdmittedPrograme.Visible = false;
                }
            }



            //  objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO IN (8)", "DOCUMENTNO ASC");
            //int USERNO1 = Convert.ToInt32(ViewState["userno"].ToString());// ((UserDetails)(Session["user"])).UserNo;
            int USERNO1 = Convert.ToInt32(ViewState["stuinfoidno"]);
            DataSet ds = new DataSet();
            //if (CheckBoxUndertaking.SelectedIndex > -1)
            //{
            //if (Convert.ToInt32(CheckBoxUndertaking.SelectedValue) == 1)
            //{
            if (ddlFacultySchoolName.SelectedValue == "" && ddlIntake.SelectedValue == "" && ddlProgram.SelectedValue == "")
            {
                ds = objdocContr.GetDoclistStud(USERNO1);
            }
            else
            {
                string[] program;
                if (ddlProgram.SelectedValue == "0")
                {
                    program = "0,0".Split(',');
                }
                else
                {
                    program = ddlProgram.SelectedValue.Split(',');
                }

                ds = objdocContr.GetDocStudList(USERNO1, Convert.ToInt32(ddlIntake.SelectedValue), Convert.ToInt32(program[1]), Convert.ToInt32(ddlFacultySchoolName.SelectedValue), Convert.ToInt32(program[0])); // PKG_ACAD_DOCUMENTENTRY_SEL_STUDNEW
            }

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvDocument.DataSource = ds;
                lvDocument.DataBind();
                lvDocument.Visible = true;
                //divlkuploaddocument.Attributes.Add("class", "finished");
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (ListViewDataItem item in lvDocument.Items)
                    {
                        LinkButton lnk = item.FindControl("lnkViewDoc") as LinkButton;
                        Label fileformat = item.FindControl("lblImageFile") as Label;
                        Label fileformate = item.FindControl("lblFileFormat") as Label;
                        Label uploded = item.FindControl("lbluploadpic") as Label;
                        Label uploadDate = item.FindControl("lblUploadDate") as Label;
                        Label lblVerifyDocument = item.FindControl("lblVerifyDocument") as Label;
                        Label lblRemark = item.FindControl("lblRemark") as Label;
                        int value = int.Parse(lnk.CommandArgument);

                        //fileformat.Text = "Only formats are allowed : png,jpg,jpeg";
                        if (value == 0)
                        {
                            if (Convert.ToString(ds.Tables[1].Rows[0]["DOC_STATUS"]) == "1")
                            {
                                lblVerifyDocument.Text = "Verified";
                                lblRemark.Text = Convert.ToString(ds.Tables[1].Rows[0]["DOC_REMARK"]);
                                lblVerifyDocument.Style.Add("color", "green");
                            }
                            else if (Convert.ToString(ds.Tables[1].Rows[0]["DOC_STATUS"]) == "2")
                            {
                                lblVerifyDocument.Text = "Not Verified";
                                lblVerifyDocument.Style.Add("color", "red");
                                lblRemark.Text = Convert.ToString(ds.Tables[1].Rows[0]["DOC_REMARK"]);
                            }
                            else if (Convert.ToString(ds.Tables[1].Rows[0]["DOC_STATUS"]) == "3")
                            {
                                lblVerifyDocument.Text = "Pending";
                                lblVerifyDocument.Style.Add("color", "red");
                                lblRemark.Text = Convert.ToString(ds.Tables[1].Rows[0]["DOC_REMARK"]);
                            }
                            else if (Convert.ToString(ds.Tables[1].Rows[0]["DOC_STATUS"]) == "4")
                            {
                                lblVerifyDocument.Text = "Incomplete";
                                lblVerifyDocument.Style.Add("color", "red");
                                lblRemark.Text = Convert.ToString(ds.Tables[1].Rows[0]["DOC_REMARK"]);
                            }
                            uploded.Text = "";
                            uploded.Visible = true;
                            if (ds.Tables[1].Rows[0]["PHOTO"].ToString() == string.Empty)
                            {
                                uploded.Text = "NO";
                                lblVerifyDocument.Text = "";
                                lblRemark.Text = "";
                            }
                            else
                            {
                                uploded.Text = "YES";
                                lnk.Visible = true;
                                uploadDate.Text = Convert.ToString(ds.Tables[1].Rows[0]["UPLOAD_DATE"]);
                                Session["STUDENTPHOTO"] = (byte[])ds.Tables[1].Rows[0]["PHOTO"];
                                break;
                            }
                        }

                        else
                        {
                            uploded.Text = "NO";
                        }
                    }
                }
                if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    foreach (ListViewDataItem item in lvDocument.Items)
                    {
                        for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                        {
                            LinkButton lnk = item.FindControl("lnkViewDoc") as LinkButton;
                            Label fileformat = item.FindControl("lblFileFormat") as Label;
                            Label uploded = item.FindControl("lbluploadpdf") as Label;
                            Label uploadDate = item.FindControl("lblUploadDate") as Label;
                            Label lblVerifyDocument = item.FindControl("lblVerifyDocument") as Label;
                            Label lblRemark = item.FindControl("lblRemark") as Label;
                            int value = int.Parse(lnk.CommandArgument);
                            lnk.CommandName = ds.Tables[2].Rows[i]["DOC_FILENAME"].ToString();
                            if (value >= 1)
                            {
                                if (Convert.ToString(ds.Tables[2].Rows[i]["DOC_STATUS"]) == "1" && Convert.ToString(ds.Tables[2].Rows[i]["DOCNO"]) == Convert.ToString(value))
                                {
                                    lblVerifyDocument.Text = "Verified";
                                    lblRemark.Text = Convert.ToString(ds.Tables[2].Rows[i]["DOC_REMARK"]);
                                    lblVerifyDocument.Style.Add("color", "green");
                                }
                                else if (Convert.ToString(ds.Tables[2].Rows[i]["DOC_STATUS"]) == "2" && Convert.ToString(ds.Tables[2].Rows[i]["DOCNO"]) == Convert.ToString(value))
                                {
                                    lblVerifyDocument.Text = "Not Verified";
                                    lblRemark.Text = Convert.ToString(ds.Tables[2].Rows[i]["DOC_REMARK"]);
                                    lblVerifyDocument.Style.Add("color", "red");
                                }
                                else if (Convert.ToString(ds.Tables[2].Rows[i]["DOC_STATUS"]) == "3" && Convert.ToString(ds.Tables[2].Rows[i]["DOCNO"]) == Convert.ToString(value))
                                {
                                    lblVerifyDocument.Text = "Pending";
                                    lblRemark.Text = Convert.ToString(ds.Tables[2].Rows[i]["DOC_REMARK"]);
                                    lblVerifyDocument.Style.Add("color", "red");
                                }
                                else if (Convert.ToString(ds.Tables[2].Rows[i]["DOC_STATUS"]) == "4" && Convert.ToString(ds.Tables[2].Rows[i]["DOCNO"]) == Convert.ToString(value))
                                {
                                    lblVerifyDocument.Text = "Incomplete";
                                    lblRemark.Text = Convert.ToString(ds.Tables[2].Rows[i]["DOC_REMARK"]);
                                    lblVerifyDocument.Style.Add("color", "red");
                                }
                                else
                                {
                                    lblVerifyDocument.Text = "";
                                    uploded.Text = "NO";
                                    lblRemark.Text = "";
                                }
                                if (value == Convert.ToInt32(ds.Tables[2].Rows[i]["DOCNO"]))
                                {
                                    uploded.Text = "YES";
                                    uploadDate.Text = Convert.ToString(ds.Tables[2].Rows[i]["UPLOAD_DATE"]);
                                    uploded.Visible = true;
                                    lnk.Visible = true;
                                    break;
                                }
                                else
                                {
                                    uploded.Text = "NO";
                                }

                            }
                            // fileformat.Text = "Only formats are allowed : pdf";
                        }
                    }
                }
            }
            else
            {
                lvDocument.DataSource = null;
                lvDocument.DataBind();
                lvDocument.Visible = false;
            }

            //}
            //else      Commented by swapnil thakare on dated 21-06-2021
            //{
            //    return;
            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ListofDocuments.bindlist-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void lvDocument_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label lblname, lblsrno;
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            lblname = (Label)e.Item.FindControl("lblname");
            lblsrno = (Label)e.Item.FindControl("lblSRNO");
        }
    }

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }
    public void DeleteIFExits(string FileName)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }
    public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;
        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }

    public int Blob_UploadDepositSlip(string ConStr, string ContainerName, string DocName, FileUpload FU, byte[] ChallanCopy)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;

        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.Properties.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            if (!cblob.Exists())
            {
                using (Stream stream = new MemoryStream(ChallanCopy))
                {
                    cblob.UploadFromStream(stream);
                }
            }
            //cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }
    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }
    private string GetContentType(string fileName)
    {
        string strcontentType = "application/octetstream";
        string ext = System.IO.Path.GetExtension(fileName).ToLower();
        Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
        if (registryKey != null && registryKey.GetValue("Content Type") != null)
            strcontentType = registryKey.GetValue("Content Type").ToString();
        return strcontentType;
    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSemester.SelectedIndex = 0;
        paymentdetails.Visible = false;
        bindlist();
        //GetDemand();
    }
    protected void alolselection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (alolselection.SelectedValue == "1")
        {
            aldetails.Visible = true;
            oldetails.Visible = false;
        }
        else if (alolselection.SelectedValue == "2")
        {
            aldetails.Visible = false;
            oldetails.Visible = true;
        }
        else if (alolselection.SelectedValue == "3")
        {
            aldetails.Visible = true;
            oldetails.Visible = true;
        }
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDemand();
    }
    private void GetDemand()
    {
        try
        {
            string[] program;
            if (ddlProgram.SelectedValue == "0")
            {
                program = "0,0".Split(',');
            }
            else
            {
                program = ddlProgram.SelectedValue.Split(',');
            }


            int DegreeNo = Convert.ToInt32(program[0]);
            int BranchNo = Convert.ToInt32(program[1]);
            DataSet ds = fee.GetCurrentDemand(Convert.ToInt32(ddlFacultySchoolName.SelectedValue), Convert.ToInt32(BranchNo), Convert.ToInt32(DegreeNo), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToString("TF"), Convert.ToInt32(ddlIntake.SelectedValue));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //btnSubmitStudentDetails.Enabled = true;
                divdemand.Visible = true;
                paymentdetails.Visible = true;
              
                TxtDemand.Text = ds.Tables[0].Rows[0]["DEMANDAMT"].ToString();
                if (TxtDemand.Text != "0.00")
                {
                    //btnSubmitStudentDetails.Enabled = true;
                    paymentdetails.Visible = true;
                    divdemand.Visible = true;
                    bank.Visible = true;
                    receipt.Visible = true;
                    amt.Visible = true;
                    btnadd.Visible = true;
                    fileupload.Visible = true;
                   
                }
                else
                {
                    //btnSubmitStudentDetails.Enabled = false;
                    divdemand.Visible = false;
                    paymentdetails.Visible = false;
                    divdemand.Visible = false;
                    bank.Visible = false;
                    receipt.Visible = false;
                    amt.Visible = false;
                    fileupload.Visible = false;
                }

                //if (ddlFacultySchoolName.SelectedValue == "7" || ddlFacultySchoolName.SelectedValue == "8")
                //{
                //bank.Visible = true;
                //receipt.Visible = true;
                //amt.Visible = true;
                //fileupload.Visible = true;
                //}
                //else
                //{
                //    bank.Visible = false;
                //    receipt.Visible = false;
                //    amt.Visible = false;
                //    fileupload.Visible = false;
                //}
            }
            else
            {
                //divdemand.Visible = true;
                //paymentdetails.Visible = true;
                objCommon.DisplayMessage(this.Page, "Standard Fees Not Define.", this.Page);
                paymentdetails.Visible = false;
                //btnSubmitStudentDetails.Enabled = false;
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lnkViewslip_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            int value = int.Parse(lnk.CommandArgument);
            string FileName = lnk.CommandName.ToString();
            string Url = string.Empty;
            string directoryPath = string.Empty;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/DownloadImg" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = FileName;
            var ImageName = img;
            if (img.ToString() != string.Empty || img.ToString() != "")
            {
                string extension = Path.GetExtension(img.ToString());
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
                if (extension == ".pdf")
                {
                    ImageViewer1.Visible = false;
                    ltEmbed.Visible = true;
                    imageViewerContainer.Visible = false;
                    var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                    string filePath = directoryPath + "\\" + ImageName;
                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    //Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    //hdfImagePath.Value = null;
                    //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"765px\" height=\"400px\">";
                    //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    //embed += "</object>";
                    //ltEmbed1.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                    //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "myModal33", "$(document).ready(function () {$('#myModal33').modal();});", true);
                    ViewState["filePath_Show"] = filePath;
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                    string Base64String = Convert.ToBase64String(bytes);
                    Session["Base64String"] = Base64String;
                    Session["Base64StringType"] = "application/pdf";
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "myModal33", "$(document).ready(function () {$('#myModal33').modal();});", true);
                }
                else
                {

                    var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                    string filePath = directoryPath + "\\" + ImageName;
                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    ViewState["filePath_Show"] = filePath;
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);

                    byte[] bytes = System.IO.File.ReadAllBytes(filePath);

                    string Base64String = Convert.ToBase64String(bytes);
                    Session["Base64String"] = Base64String;
                    Session["Base64StringType"] = "image/png";
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "myModal33", "$(document).ready(function () {$('#myModal33').modal();});", true);
                    //hdfImagePath.Value = null;
                    //ImageViewer1.Visible = false;
                    //ltEmbed1.Visible = false;
                    //imageViewerContainer.Visible = true;
                    //var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                    //string filePath = directoryPath + "\\" + ImageName;
                    //if ((System.IO.File.Exists(filePath)))
                    //{
                    //    System.IO.File.Delete(filePath);
                    //}

                    //Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    //hdfImagePath.Value = ResolveUrl("~/DownloadImg/" + ImageName);
                    //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "myModal33", "$(document).ready(function () {$('#myModal33').modal();});", true);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Sorry, File not found !!!", this.Page);
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        //int idnore = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO=" + REGNO));
        this.ShowReport("StudentRegistrationrpt", "StudentRegistrationrpt.rpt", Convert.ToInt32(ViewState["stuinfoidno"]));
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        ClearAllFields();
    }
    protected DataTable ShowLV()
    {
        byte[] ChallanCopy = null;
        DataTable dt = new DataTable();
        dt.TableName = "NewStudent";

        if (Session["Row"] != null)
        {
            dt = (DataTable)Session["Row"];
            DataRow dr = null;
            if (dt.Rows.Count > 0)
            {
                dr = dt.NewRow();
                dr["Bank"] = ddlbankdetails.SelectedValue;
                dr["ddlbank"] = ddlbankdetails.SelectedItem;
                dr["ReceiptNo"] = TxtReceipt.Text;
                dr["Amount"] = Txtamt.Text;
                dr["Receipt"] = fileupload1.PostedFile.FileName;
                ChallanCopy = objCommon.GetImageData(fileupload1);
                dr["Byte"] = ChallanCopy;              
                dt.Rows.Add(dr);
                Session["Row"] = dt;
                pnllst.Visible = true;
                lvSemester.DataSource = Session["Row"];
                lvSemester.DataBind();
                lvSemester.Visible = true;
            }
        }
        else
        {
            if (dt.Columns.Count <= 0)
            {
                dt.Columns.Add("Bank", typeof(int));
                dt.Columns.Add("ddlbank", typeof(string));
                dt.Columns.Add("ReceiptNo", typeof(string));
                dt.Columns.Add("Amount", typeof(string));
                dt.Columns.Add("Receipt", typeof(string));
                dt.Columns.Add("Byte", typeof(byte[]));
            }
            DataRow dr1 = dt.NewRow();
            dr1 = dt.NewRow();

            dr1["Bank"] = ddlbankdetails.SelectedValue;
            dr1["ddlbank"] = ddlbankdetails.SelectedItem;
            dr1["ReceiptNo"] = TxtReceipt.Text;
            dr1["Amount"] = Txtamt.Text;
            dr1["Receipt"] = fileupload1.PostedFile.FileName;
            ChallanCopy = objCommon.GetImageData(fileupload1);
            dr1["Byte"] = ChallanCopy;      
            dt.Rows.Add(dr1);
            Session["Row"] = dt;
            pnllst.Visible = true;
            lvSemester.DataSource = Session["Row"];
            lvSemester.DataBind();
            lvSemester.Visible = true;
        }
        return dt;
    }
    protected void Add_Click(object sender, EventArgs e)
    {
        if (ddlbankdetails.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.Page, "Please Select Bank.", this.Page);
            return;
        }
        else if (TxtReceipt.Text == "")
        {
            objCommon.DisplayMessage(this.Page, "Please Enter Receipt Number.", this.Page);
            return;
        }
        else if (Txtamt.Text=="")
        {
            objCommon.DisplayMessage(this.Page, "Please Enter Amount.", this.Page);
            return;
        }
        ShowLV();
        lvSemester.Visible = true;
        ViewState["actionsem"] = "add";
        ddlbankdetails.SelectedIndex = 0;
        TxtReceipt.Text = "";
        Txtamt.Text = "";
    }
    protected void lnkClose_Click(object sender, EventArgs e)
    {
        try
        {
            if ((System.IO.File.Exists(Convert.ToString(ViewState["filePath_Show"]))))
            {
                System.IO.File.Delete(Convert.ToString(ViewState["filePath_Show"]));
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlPerProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlPerDisctrict, "ACD_DISTRICT", "DISTINCT DISTRICTNO", "DISTRICTNAME", "STATENO=" + ddlPerProvince.SelectedValue, "DISTRICTNO");
        }
        catch (Exception ex)
        {

        }
    }
}
