using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using System.Data;
using DotNetIntegrationKit;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml;
using System.Net;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using _NVP;
using System.Collections.Specialized;
using EASendMail;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.Script.Serialization;
using RestSharp;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class ACADEMIC_TransfreePreview : System.Web.UI.Page
{
    Common objCommon = new Common();
    UserController user = new UserController();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    int idno = 0; decimal TotalSum = 0; String tspl_txn_id = string.Empty;
    string session = string.Empty;
    IITMS.UAIMS.BusinessLayer.BusinessEntities.DocumentAcad objdocument = new DocumentAcad();
    DocumentControllerAcad objdocContr = new DocumentControllerAcad();
    NewUser objnu = new NewUser();
    NewUserController objnuc = new NewUserController();
    StudentFees objStudentFees = new StudentFees();
    NewUserController ObjNuc = new NewUserController();
    private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string username = string.Empty;
    //PDFDynamicViewer pdf = new PDFDynamicViewer();


    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
    string blob_ContainerNamePhoto = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNamePhoto"].ToString();
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
        try
        {
            if (Session["userno"] == null || Session["username"] == null ||
                  Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null || Session["idno"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    if (Convert.ToString(ViewState["action"]) == "")
                    {
                        ViewState["action"] = "add";
                    }

                    // Check User Authority 

                    Page.Title = Session["coll_name"].ToString();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    objCommon.FillDropDownList(ddlevalutionResult, "ACD_EVALUATION_RESULT", "EVAL_NO", "EVALUATION_NAME", "", "EVAL_NO");

                    if (Request.QueryString["userno"].ToString() != null || Request.QueryString["userno"].ToString() != "")
                    {
                        //int idno = Convert.ToInt32((objCommon.LookUp("ACD_STUDENT", "IDNO", "USERNO=" + (Request.QueryString["userno"]) + "")));
                        //ViewState["ID_NO"] = idno;
                    }


                    this.CheckPageAuthorization();
                    ListViewDataBind();
                    ListViewApplyPgmDataBind();
                    bindmodules();
                    binddocumentlist();
                    ShowStudentDetails();
                    ApplicantPersonalDetails();
                    BindAddressData();
                    bindOfferAcceptance();

                    CheckStudyLevel();
                    CheckUserEligiblity();
                    bindSection();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                }

            }
            string FinalStatus = "";
            FinalStatus = objCommon.LookUp("ACD_STUDENT", "ISNULL(ENROLLNO,0)", "ISNULL(CAN,0) = 0 AND ISNULL(ADMCAN,0) = 0 AND IDNO=" + Convert.ToInt32(ViewState["ID_NO"]));
            if (FinalStatus.ToString() == "0" || FinalStatus.ToString() == string.Empty)
            {
                lnkSendEmail.Enabled = true;
                lnkGeneratereport.Visible = false;
                lnkPrintReport.Visible = false;
                btnFrontBackReport.Visible = false;
            }
            else
            {
                lnkSendEmail.Enabled = true;
                lnkGeneratereport.Visible = true;
                lnkPrintReport.Visible = true;
                //btnFrontBackReport.Visible = true;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void CheckUserEligiblity()
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("ACD_STUDENT", "DISTINCT USERNO", "DEGREENO,BRANCHNO,COLLEGE_ID,ADMBATCH,IDNO", "IDNO =" + Convert.ToInt32(ViewState["ID_NO"]), "");
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                DataSet ds1 = null;
                ds1 = objdocContr.getStudentEligiblityDetails(Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[0]["ADMBATCH"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString()));
                if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                {
                    lblstatus.Text = ds1.Tables[0].Rows[0]["ELIGIBILITY"].ToString();
                }
                else
                {
                    lblstatus.Text = "";
                }
            }
            else
            {
                lblstatus.Text = "";
            }

            int countpay = Convert.ToInt32(objCommon.LookUp("acd_dcr d inner join acd_student s  on s.idno=d.idno and s.semesterno=d.semesterno", "count(1)", "s.idno=" + Convert.ToInt32(ViewState["ID_NO"]) + "and recon=1 and ENROLLNO is null and s.semesterno=" + Convert.ToInt32(ViewState["SEMESTERNO"])));
            if (countpay > 0)
            {
                btnRefundInitiated.Visible = true;
            }
            else
            {
                btnRefundInitiated.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void bindSection()
    {
        try
        {

            string SP_Name1 = "PKG_ACD_GET_USER_GROUP_DETAILS";
            string SP_Parameters1 = "@P_IDNO,@P_ADMBATCH,@P_TYPE";
            string Call_Values1 = "" + Convert.ToInt32(ViewState["ID_NO"]) + "," + Convert.ToInt32(ViewState["batchno"]) + ",0";
            DataSet ds1 = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);
            if (ds1.Tables != null && ds1.Tables[0].Rows.Count > 0)
            {
                lblSectionName.Text = ds1.Tables[0].Rows[0]["GROUPNAME"].ToString();
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void CheckPageAuthorization()
    {
        // Request.QueryString["pageno"] = "ACADEMIC/EXAMINATION/ExamRegistration_New.aspx ?pageno=1801";
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=OnlinePayment_StudentLogin.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OnlinePayment_StudentLogin.aspx");
        }
    }

    private void bindmodules()
    {
        try
        {
            DataSet ds = null;


            ds = objdocContr.getModuleOfferedCourses(Convert.ToInt32(ViewState["ID_NO"].ToString()));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                Panel2.Visible = true;
                lvOfferedSubject.DataSource = ds.Tables[0];
                lvOfferedSubject.DataBind();
                //if (ds.Tables[0].Rows[0]["EXAM_REGISTERED"].ToString() == "1")
                //{
                //    btnSubmitOffer.Visible = false;
                //    objCommon.DisplayMessage(this.Page, "Module registration already done", this.Page);
                //}
                //else
                {

                    btnSubmitOffer.Visible = true;
                }
            }
            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                Panel3.Visible = true;
                lvcoursetwo.DataSource = ds.Tables[1];
                lvcoursetwo.DataBind();
                //if (ds.Tables[1].Rows[0]["EXAM_REGISTERED"].ToString() == "1")
                //{
                //    btnSubmitOffer.Visible = false;

                //}
                //else
                {
                    btnSubmitOffer.Visible = true;

                }
            }
            if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
            {
                Panel4.Visible = true;
                lvcoursethree.DataSource = ds.Tables[2];
                lvcoursethree.DataBind();
                //if (ds.Tables[2].Rows[0]["EXAM_REGISTERED"].ToString() == "1")
                //{
                //    btnSubmitOffer.Visible = false;
                //    objCommon.DisplayMessage(this.Page, "Module registration already done", this.Page);
                //}
                //else
                {
                    btnSubmitOffer.Visible = true;

                }
            }
            else
            {

                lvOfferedSubject.DataSource = ds.Tables[0];
                lvOfferedSubject.DataBind();
                //lvOfferedSubject.DataSource = null;
                //lvOfferedSubject.DataBind();
                //objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);//add by aashna 04-01-2022
                //btnSubmitOffer.Visible = false;
            }

        }

        catch (Exception ex)
        {
        }
    }

    protected void btnSubmitOffer_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(Session["usertype"]) == 2)
            {
                string Coursenos = "", Ccode = "", Subids = "", CourseNames = "", Credits = "", licUano = "";
                foreach (ListViewDataItem items in lvOfferedSubject.Items)
                {
                    CheckBox chk = items.FindControl("chkRows") as CheckBox;
                    HiddenField hdfCourseNo = items.FindControl("hdfCourseNo") as HiddenField;
                    HiddenField hdfSubid = items.FindControl("hdfSubid") as HiddenField;
                    Label lblCCode = items.FindControl("lblCCode") as Label;
                    Label lblCoursename = items.FindControl("lblCourseName") as Label;
                    Label lblCredits = items.FindControl("lblCredits") as Label;
                    Label lblUano = items.FindControl("lbluano") as Label;
                    if (chk.Checked == true)
                    {
                        Coursenos += hdfCourseNo.Value + ',';
                        Ccode += lblCCode.Text + ',';
                        CourseNames += lblCoursename.Text + ',';
                        Credits += lblCredits.Text + ',';
                        Subids += hdfSubid.Value + ',';
                        licUano += lblUano.Text + ',';
                    }
                }
                foreach (ListViewDataItem items in lvcoursetwo.Items)
                {
                    CheckBox chk = items.FindControl("chkRows") as CheckBox;
                    HiddenField hdfCourseNo = items.FindControl("hdfCourseNo") as HiddenField;
                    HiddenField hdfSubid = items.FindControl("hdfSubid") as HiddenField;
                    Label lblCCode = items.FindControl("lblCCode") as Label;
                    Label lblCoursename = items.FindControl("lblCourseName") as Label;
                    Label lblCredits = items.FindControl("lblCredits") as Label;
                    Label lblUano = items.FindControl("lbluano") as Label;
                    if (chk.Checked == true)
                    {
                        Coursenos += hdfCourseNo.Value + ',';
                        Ccode += lblCCode.Text + ',';
                        CourseNames += lblCoursename.Text + ',';
                        Credits += lblCredits.Text + ',';
                        Subids += hdfSubid.Value + ',';
                        licUano += lblUano.Text + ',';
                    }
                }
                foreach (ListViewDataItem items in lvcoursethree.Items)
                {
                    CheckBox chk = items.FindControl("chkRows") as CheckBox;
                    HiddenField hdfCourseNo = items.FindControl("hdfCourseNo") as HiddenField;
                    HiddenField hdfSubid = items.FindControl("hdfSubid") as HiddenField;
                    Label lblCCode = items.FindControl("lblCCode") as Label;
                    Label lblCoursename = items.FindControl("lblCourseName") as Label;
                    Label lblCredits = items.FindControl("lblCredits") as Label;
                    Label lblUano = items.FindControl("lbluano") as Label;
                    if (chk.Checked == true)
                    {
                        Coursenos += hdfCourseNo.Value + ',';
                        Ccode += lblCCode.Text + ',';
                        CourseNames += lblCoursename.Text + ',';
                        Credits += lblCredits.Text + ',';
                        Subids += hdfSubid.Value + ',';
                        licUano += lblUano.Text + ',';
                    }
                }
                Coursenos = Coursenos.TrimEnd(',');
                Ccode = Ccode.TrimEnd(',');
                CourseNames = CourseNames.TrimEnd(',');
                Credits = Credits.TrimEnd(',');
                Subids = Subids.TrimEnd(',');
                licUano = licUano.TrimEnd(',');

                int output = objdocContr.InsertModuleRegistration(Convert.ToInt32(ViewState["ID_NO"]), Coursenos, Ccode, CourseNames, Credits, Subids, licUano);

                if (output != -99 && output != 99)
                {
                    objCommon.DisplayMessage(this.Page, "Record Added Successfully", this.Page);

                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Error", this.Page);
                    // Clear();
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "This process only for student !", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }



    public void binddocumentlist()
    {
        try
        {

            //int USERNO1 = Convert.ToInt32(ViewState["userno"].ToString());
            DataSet ds = new DataSet();

            ds = objdocContr.GetDoclistByAdmin_ForTransferee(Convert.ToInt32(Request.QueryString["userno"]));

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvDocument.DataSource = ds.Tables[0];
                lvDocument.DataBind();
                lvDocument.Visible = true;
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    LvAdditionalDoc.DataSource = ds.Tables[1];
                    LvAdditionalDoc.DataBind();
                }
                else
                {
                    LvAdditionalDoc.DataSource = null;
                    LvAdditionalDoc.DataBind();
                }
                foreach (ListViewDataItem row in lvDocument.Items)
                {
                    Label lblStatus = row.FindControl("lblStatus") as Label;
                    DropDownList ddlStatus = row.FindControl("ddlstatus") as DropDownList;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (lblStatus.Text.ToString() == ds.Tables[0].Rows[i]["DOC_STATUS"].ToString())
                        {
                            ddlStatus.SelectedValue = ds.Tables[0].Rows[i]["DOC_STATUS"].ToString();
                        }
                    }
                }
                //    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                //    {
                //        foreach (ListViewDataItem item in lvDocument.Items)
                //        {
                //            LinkButton lnk = item.FindControl("lnkViewDoc") as LinkButton;
                //            Label fileformat = item.FindControl("lblImageFile") as Label;
                //            Label fileformate = item.FindControl("lblFileFormat") as Label;
                //            Label uploded = item.FindControl("lbluploadpic") as Label;
                //            int value = int.Parse(lnk.CommandArgument);                       
                //            if (value == 0)
                //            {
                //                lnk.Visible = true;
                //                uploded.Visible = true;
                //                uploded.Text = "YES";
                //                Session["STUDENTPHOTO"] = (byte[])ds.Tables[1].Rows[0]["PHOTO"];
                //                break;
                //            }

                //            else
                //            {
                //                uploded.Text = "NO";
                //            }
                //        }
                //    }
                //    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                //    {
                //        foreach (ListViewDataItem item in lvDocument.Items)
                //        {
                //            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                //            {
                //                LinkButton lnk = item.FindControl("lnkViewDoc") as LinkButton;
                //                Label fileformat = item.FindControl("lblFileFormat") as Label;
                //                Label uploded = item.FindControl("lbluploadpdf") as Label;
                //                int value = int.Parse(lnk.CommandArgument);
                //                lnk.CommandName = ds.Tables[2].Rows[i]["DOC_FILENAME"].ToString();
                //                if (value == Convert.ToInt32(ds.Tables[2].Rows[i]["DOCNO"]))
                //                {
                //                    uploded.Text = "YES";
                //                    uploded.Visible = true;
                //                    lnk.Visible = true;
                //                    break;
                //                }
                //                else
                //                {
                //                    uploded.Text = "NO";
                //                }

                //            }
                //        }
                //    }
                //}
                //else
                //{
                //    lvDocument.DataSource = null;
                //    lvDocument.DataBind();
                //    lvDocument.Visible = false;
                //}
            }
            else
            {
                btnSubmit.Visible = false;
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
    private string GetContentType(string fileName)
    {
        string strcontentType = "application/octetstream";
        string ext = System.IO.Path.GetExtension(fileName).ToLower();
        Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
        if (registryKey != null && registryKey.GetValue("Content Type") != null)
            strcontentType = registryKey.GetValue("Content Type").ToString();
        return strcontentType;
    }

    protected void lnkViewDoc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            int value = int.Parse(lnk.CommandArgument);
            // int value = 24435;
            string FileName = lnk.CommandName.ToString();
            if (value == 0)
            {
                //hdfImagePath.Value = null;
                //imageViewerContainer.Visible = false;
                //ImageViewer.Visible = true;
                //ltEmbed.Text = "";
                //ltEmbed.Visible = false;
                //ImageViewer.ImageUrl = "~/showimage.aspx?id=" + Convert.ToInt32(ViewState["ID_NO"]) + "&type=STUDENT";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
                irm1.Visible = false;
                ImageViewer.Visible = true;
                ltEmbed.Visible = false;
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
                CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerNamePhoto);
                string img = lnk.CommandName.ToString();
                var ImageName = img;
                if (img != null || img != "")
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
                    hdfImagePath.Value = "data:image/png;base64," + Base64String;
                    imageViewerContainer.Visible = false;

                    //string embed = "<object data=\"{0}\" type=\"application/pdf\">";
                    //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    //embed += "</object>";
                    ImageViewer.ImageUrl = string.Format("data:image/png;base64," + Base64String);

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
                }
            }
            else
            {
                //string docPath = System.Configuration.ConfigurationManager.AppSettings["UploadDocPath"];
                //string fileName = docPath.ToString() + "\\" + FileName.ToString(); ;
                //byte[] fileContent = null;
                //if (File.Exists(fileName))
                //{
                //    System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                //    System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                //    long byteLength = new System.IO.FileInfo(fileName).Length;
                //    fileContent = br.ReadBytes((Int32)byteLength);
                //    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                //    string base64String = Convert.ToBase64String(fileContent, 0, Convert.ToInt32(byteLength));
                //    string mimetype = GetContentType(fileName);
                //    string pdfIFrameSrc = "data:" + mimetype.ToString() + ";base64," + Convert.ToString(base64String) + "";
                //    //  data:application/pdf;base64,JVBERi0xLjcgCiXi48/TIAoxIDAgb2JqIAo8PCAK
                //    //   data:application/pdf;base64,JVBERi0xLjcKJcKzx9gNCjEgMCBvYmoNPDwvTm
                //    iframe1.Attributes.Add("src", pdfIFrameSrc);
                //    iframe1.Visible = true;
                //    ImageViewer.Visible = false;

                irm1.Visible = true;

                ltEmbed.Visible = true;
                ImageViewer.Visible = false;
  

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
                    ViewState["filePath_Show"] = filePath;
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                    string Base64String = Convert.ToBase64String(bytes);
                    Session["Base64String"] = Base64String;
                    Session["Base64StringType"] = "application/pdf";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
                    //hdfImagePath.Value = null;
                    //imageViewerContainer.Visible = false;
                    //ltEmbed.Visible = true;
                    //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"750px\" height=\"400px\">";
                    //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    //embed += "</object>";
                    //ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Sorry, File not found !!!", this.Page);
                }

                //}
                //else
                //{
                //    objCommon.DisplayMessage(this.Page, "Sorry, File not available on this machine!", this.Page);

                //}
            }
        }
        catch (Exception ex)
        {
        }
    }
    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DocumentControllerAcad objDoc = new DocumentControllerAcad();
            DocumentAcad objDocno = new DocumentAcad();

            string docnos = "", docnames = "", status = "", remark = ""; int Count = 0, reject = 0;

            DataSet dsUserContact = null;
            UserController objUC = new UserController();
            dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", Convert.ToString(ViewState["ID_NO"]), Convert.ToInt32(Request.QueryString["pageno"]));
            int UploadDocCount = 0;
            foreach (ListViewDataItem item in lvDocument.Items)
            {
                Label docno = item.FindControl("lblDocNo") as Label;
                Label DocName = item.FindControl("lblname") as Label;
                TextBox txtRemark = item.FindControl("txtremark") as TextBox;
                DropDownList ddlStatus = item.FindControl("ddlstatus") as DropDownList;
                HiddenField hdfMandatory = item.FindControl("hdfMandatory") as HiddenField;
                LinkButton lnkViewDoc = item.FindControl("lnkViewDoc") as LinkButton;
                //if (ddlStatus.SelectedIndex == 0)
                //{
                //    objCommon.DisplayMessage(this.Page, "Please Select Status for " + DocName.Text + " !!!", this.Page);
                //    return;
                //}

                if (lnkViewDoc.Visible == true)
                {
                    UploadDocCount++;
                }

               

               

                if (hdfMandatory.Value == "YES")
                {
                    //if (lnkViewDoc.Visible == true)
                    //{
                        if (ddlStatus.SelectedIndex == 0)
                        {
                            objCommon.DisplayMessage(this.Page, "Please Select Status for " + DocName.Text + " !!!", this.Page);
                            return;
                        }
                    //}
                }
                else
                {
                    Count++;
                    status += ddlStatus.SelectedValue + '$';
                }
                if (ddlStatus.SelectedValue == "2")
                {
                    reject = 1;
                }
                if (hdfMandatory.Value == "YES")
                {
                    if (lnkViewDoc.Visible == true)
                    {
                        if (Convert.ToString(txtRemark.Text) == string.Empty)
                        {
                            objCommon.DisplayMessage(this.Page, "Please Enter Remark for " + DocName.Text + " !!!", this.Page);
                            return;
                        }
                    }
                }
                else
                {
                    remark += txtRemark.Text + '$';
                }
                if (docno.Text == "0")
                {
                    docnos += docno.Text + '$';
                    docnames += DocName.Text + '$';
                }
                else
                {
                    docnos += docno.Text + '$';
                    docnames += DocName.Text + '$';
                }
            }
            if (UploadDocCount == 0)
            {
                objCommon.DisplayMessage(this.Page, "Document Not Uploaded !!!", this.Page);
                return;
            }

            if (Count > 0)
            {
                docnos = docnos.TrimEnd('$');
                docnames = docnames.TrimEnd('$');
                status = status.TrimEnd('$');
                remark = remark.TrimEnd('$');

                string Subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                string message = "";
                message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();

                if (reject == 1)
                {
                    //Execute(message, lblEmailID.Text, Subject, lblStudName.Text, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
                    EmailSmsWhatssppSend(Convert.ToInt32(Request.QueryString["pageno"]), lblEmailID.Text, lblStudName.Text, Convert.ToString(ViewState["ID_NO"]));
                }
                CustomStatus cs = CustomStatus.Others;
                cs = (CustomStatus)objDoc.UpdateDocumentStatus_Transferee(Convert.ToInt32(Request.QueryString["userno"]), docnos, docnames, status, remark, Convert.ToInt32(Session["userno"]));
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.Page, "Document Verified Successfully !!!", this.Page);
                    return;
                }
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.Page, "Document Verified Successfully !!!", this.Page);
                    return;
                }
            }
            //else
            //{
            //    objCommon.DisplayMessage(this.Page, "Please select at least one document for upload !!!", this.Page);
            //}
        }

        catch (Exception ex)
        {

        }
    }

    //ADDED BY ROSHAN PATIL 12/06/2023
    protected void EmailSmsWhatssppSend(int Page_No, string toSendAddre, string Name, string IDNO)
    {
        EmailSmsWhatsaap Email = new EmailSmsWhatsaap();
        DataSet ds = objCommon.FillDropDown("ACCESS_LINK", "isnull(EMAIL,0) EMAIL,isnull(SMS,0) as SMS,isnull(WHATSAAP,0) as WHATSAAP", "", "AL_NO=" + Page_No, "");
        string Res = ""; string Message = "";
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["EMAIL"].ToString()) == 1)//For Email Send 
        {
            UserController objUC = new UserController();
            DataSet dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", IDNO, Page_No);
            if (dsUserContact.Tables[1] != null && dsUserContact.Tables[1].Rows.Count > 0)
            {

                string Subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                Message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                string toSendAddress = toSendAddre.ToString();// 
                MailMessage msgsPara = new MailMessage();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Message);
                msgsPara.BodyEncoding = Encoding.UTF8;
                msgsPara.Body = Convert.ToString(sb);
                msgsPara.Body = msgsPara.Body.Replace("{User}", Name.ToString());
                MemoryStream Attachment = null; string AttachmentName = "";
                Task<string> task = Email.Execute(msgsPara.Body, toSendAddress, Subject, Attachment, AttachmentName.ToString());
                Res = task.Result;

            }
        }
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["SMS"].ToString()) == 1) //For SMS Send 
        {

        }
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["WHATSAAP"].ToString()) == 1)//For Whatsaap Send 
        {

        }
    }

    static async Task Execute(string message, string toSendAddress, string Subject, string firstname, string ReffEmail, string ReffPassword)
    {
        int ret = 0;

        try
        {
            //Common objCommon = new Common();

            //// SmtpMail oMail = new SmtpMail("TryIt");
            //// oMail.From = ReffEmail;
            //// oMail.To = toSendAddress;
            //// oMail.Subject = Subject;
            //// oMail.HtmlBody = message;
            //// oMail.HtmlBody = oMail.HtmlBody.Replace("{User}", firstname.ToString());
            ////// SmtpServer oServer = new SmtpServer("smtp.live.com");
            //// SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022
            //// oServer.User = ReffEmail;
            //// oServer.Password = ReffPassword;
            //// oServer.Port = 587;
            //// oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
            //// EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
            //// oSmtp.SendMail(oServer, oMail);

            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_USERNAME", "CollegeName,COMPANY_EMAILSVCID ,SENDGRID_PWD ,API_KEY_SENDGRID", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            var fromAddress = new System.Net.Mail.MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["CollegeName"].ToString());
            var toAddress = new System.Net.Mail.MailAddress(toSendAddress, "");
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var apiKey = dsconfig.Tables[0].Rows[0]["API_KEY_SENDGRID"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["CollegeName"].ToString());
            var subject = Subject;
            var to = new EmailAddress(toSendAddress, "");

            MailMessage msgs = new MailMessage();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(message);
            msgs.BodyEncoding = Encoding.UTF8;
            msgs.Body = Convert.ToString(sb);

            msgs.Body = msgs.Body.Replace("{User}", firstname.ToString());

            msgs.BodyEncoding = Encoding.UTF8;
            msgs.IsBodyHtml = true;
            var plainTextContent = "";
            var htmlContent = msgs.Body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            string res = Convert.ToString(response.StatusCode);
            if (res == "Accepted")
            {
                ret = 1;
            }
            else
            {
                ret = 0;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowStudentDetails()
    {
        try
        {
            idno = Convert.ToInt32(Request.QueryString["userno"]);
            DataSet dsStudent = feeController.GetStudentInfoById_ForOnlinePayment_Transferee(idno);
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                DataRow dr = dsStudent.Tables[0].Rows[0];
                lblStudName.Text = dr["FIRSTNAME"].ToString() == string.Empty ? string.Empty : dr["FIRSTNAME"].ToString(); ;
                ViewState["fullName"] = lblStudName.Text;
                lblSex.Text = dr["SEX"].ToString() == string.Empty ? string.Empty : dr["SEX"].ToString();
                lblRegNo.Text = dr["USERNAME"].ToString() == string.Empty ? string.Empty : dr["USERNAME"].ToString();
                lblPaymentType.Text = dr["PAYTYPENAME"].ToString() == string.Empty ? string.Empty : dr["PAYTYPENAME"].ToString();
                lblYear.Text = dr["YEARNAME"].ToString() == string.Empty ? string.Empty : dr["YEARNAME"].ToString();
                lblSemester.Text = dr["SEMESTERNAME"].ToString() == string.Empty ? string.Empty : dr["SEMESTERNAME"].ToString();

                lblSemesterP.Text = dr["SEMESTERNAME"].ToString() == string.Empty ? string.Empty : dr["SEMESTERNAME"].ToString();
                Txtstudylevel.Text = dr["UA_SECTIONNAME"].ToString() == string.Empty ? string.Empty : dr["UA_SECTIONNAME"].ToString();
                txtpreviousschool.Text = dr["PREVIOUS_SCHOOL"].ToString() == string.Empty ? string.Empty : dr["PREVIOUS_SCHOOL"].ToString();
                txtxpreviouspgm.Text = dr["PREVIOUS_PROGRAM"].ToString() == string.Empty ? string.Empty : dr["PREVIOUS_PROGRAM"].ToString();
                lblapplysem.Text = dr["APPLYSEM"].ToString() == string.Empty ? string.Empty : dr["APPLYSEM"].ToString();

                lblBatch.Text = dr["BATCHNAME"].ToString() == string.Empty ? string.Empty : dr["BATCHNAME"].ToString();
                lblMobileNo.Text = dr["STUDENTMOBILE"].ToString() == string.Empty ? string.Empty : dr["STUDENTMOBILE"].ToString();
                lblEmailID.Text = dr["EMAILID"].ToString() == string.Empty ? string.Empty : dr["EMAILID"].ToString();
                ViewState["EMAILID"] = dr["EMAILID"].ToString();
                ViewState["degreeno"] = dr["DEGREENO"].ToString();
                ViewState["BRANCHNO"] = dr["BRANCHNO"].ToString();               
                ViewState["EVAL_NO"] = dr["EVAL_NO"].ToString() == string.Empty ? string.Empty : dr["EVAL_NO"].ToString();

                if (ViewState["EVAL_NO"].ToString() == "1")
                {
                    ddlevalutionResult.SelectedValue = dr["EVAL_NO"].ToString() == string.Empty ? string.Empty : dr["EVAL_NO"].ToString();
                    rdiotest.SelectedValue= dr["TEST_RECOMMENDED_STATUS"].ToString() == string.Empty ? string.Empty : dr["TEST_RECOMMENDED_STATUS"].ToString();
                    EvalRemark.Text = dr["REVAL_REMARK"].ToString() == string.Empty ? string.Empty : dr["REVAL_REMARK"].ToString();
                    evalstatus.Visible = true;
                    divremarks.Visible = true;
                }
                else
                {
                    ddlevalutionResult.SelectedValue = dr["EVAL_NO"].ToString() == string.Empty ? string.Empty : dr["EVAL_NO"].ToString();
                    evalstatus.Visible = false;
                    divremarks.Visible = false;
                }
                if(ViewState["EVAL_NO"].ToString() != "")
                {
                    lblStatusREval.Visible = true;
                    btnSubmitEval.Enabled = false;
                }
                else
                {
                    lblStatusREval.Visible = false;
                    btnSubmitEval.Enabled = true;
                }
                ViewState["batchno"] = Convert.ToInt32(dr["ADMBATCH"].ToString());
                ViewState["semesterno"] = Convert.ToInt32(dr["SEMESTERNO"].ToString());
                ViewState["paytypeno"] = Convert.ToInt32(dr["PTYPE"].ToString());
                ViewState["RECIEPT_CODE"] = dr["RECIEPT_CODE"].ToString() == string.Empty ? string.Empty : dr["RECIEPT_CODE"].ToString();
                ViewState["SESSIONNO"] = dr["SESSIONNO"].ToString() == string.Empty ? string.Empty : dr["SESSIONNO"].ToString();
                ViewState["COLLEGE_ID"] = dr["COLLEGE_ID"].ToString();
                ViewState["RECON"] = dr["RECON"].ToString();
                ViewState["SEMESTERNO"] = dr["SEMESTERNO"].ToString();
                ViewState["REGNO"] = dr["REGNO"].ToString();
                ViewState["NAME_WITH_INITIAL"] = dr["NAME_WITH_INITIAL"].ToString();
               

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
    protected void ListViewDataBind()
    {
        int UserNo = Convert.ToInt32(Request.QueryString["userno"]);
        int ugogot = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "UGPGOT", "USERNO=" + UserNo));
        int COUNT = Convert.ToInt32(objCommon.LookUp("ACD_USER_LAST_QUALIFICATION", "COUNT(USERNO)", "USERNO=" + UserNo));
        if (COUNT == 0)
        {
            DataSet ds = objCommon.FillDropDown("ACD_UA_SECTION", "*,'' as SCHOOL_NAME,'' as SCHOOL_ADDRESS,'' as SCHOOL_REGION,'' as YEAR_ATTENDED,'' as SCHOOL_TYPE_NO", "UA_SECTIONNAME", "UA_SECTION>" + Convert.ToInt32(ugogot), "UA_SECTION desc");
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                diveducationlast.Visible = true;
                diveducation.Visible = true;
                lvLevellist.DataSource = ds;
                lvLevellist.DataBind();
            }
            else
            {
                diveducationlast.Visible = false;
                diveducation.Visible = false;
                lvLevellist.DataSource = null;
                lvLevellist.DataBind();
            }
        }
        else
        {
            DataSet ds = objCommon.FillDropDown("ACD_UA_SECTION S INNER JOIN ACD_USER_LAST_QUALIFICATION UL ON (S.UA_SECTION=UL.UGPGOT)", "UA_SECTION", "UA_SECTIONNAME,SCHOOL_NAME,SCHOOL_ADDRESS,SCHOOL_REGION,YEAR_ATTENDED,SCHOOL_TYPE,SCHOOL_TYPE_NO", "UA_SECTION>" + Convert.ToInt32(ugogot) + "AND USERNO=" + UserNo, "UA_SECTION desc");
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                diveducationlast.Visible = true;
                diveducation.Visible = true;
                lvLevellist.DataSource = ds;
                lvLevellist.DataBind();
                foreach (ListViewDataItem dataitem in lvLevellist.Items)
                {
                    Label lblTypeNo = dataitem.FindControl("lblTypeNo") as Label;
                    DropDownList ddlType = dataitem.FindControl("ddlType") as DropDownList;
                    ddlType.SelectedValue = lblTypeNo.Text;
                }
            }
        }
    }

    protected void ListViewApplyPgmDataBind()
    {
        BranchController objBC = new BranchController();
        int UserNo = Convert.ToInt32(Request.QueryString["userno"]);
        int ugogot = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "UGPGOT", "USERNO=" + UserNo));
        DataSet ds = objBC.Getdetailsofbranch(Convert.ToInt32(ugogot), (Convert.ToInt32(Request.QueryString["userno"])), Convert.ToInt32(0), Convert.ToInt32(0), Convert.ToInt32(0));
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {

            ddlPrefrence1.DataSource = ds.Tables[0];
            ddlPrefrence1.DataValueField = ds.Tables[0].Columns["PREFERENCESNO"].ToString();
            ddlPrefrence1.DataTextField = ds.Tables[0].Columns["PREFERENCES"].ToString();
            ddlPrefrence1.DataBind();


            ddlPrefrence2.DataSource = ds.Tables[0];
            ddlPrefrence2.DataValueField = ds.Tables[0].Columns["PREFERENCESNO"].ToString();
            ddlPrefrence2.DataTextField = ds.Tables[0].Columns["PREFERENCES"].ToString();
            ddlPrefrence2.DataBind();

            ddlPrefrence3.DataSource = ds.Tables[0];
            ddlPrefrence3.DataValueField = ds.Tables[0].Columns["PREFERENCESNO"].ToString();
            ddlPrefrence3.DataTextField = ds.Tables[0].Columns["PREFERENCES"].ToString();
            ddlPrefrence3.DataBind();

            string STATUS = ds.Tables[3].Rows[0]["STATUS"].ToString();

            if (STATUS != "0")
            {
                divPreference1.Style.Add("pointer-events", "none");
                divPreference2.Style.Add("pointer-events", "none");
                divPreference3.Style.Add("pointer-events", "none");
            }
            else
            {
                divPreference1.Style.Add("pointer-events", "block");
                divPreference2.Style.Add("pointer-events", "block");
                divPreference3.Style.Add("pointer-events", "block");
            }

        }
        string PREF1 = (objCommon.LookUp("ACD_USER_BRANCH_PREF", "(CONVERT(NVARCHAR(50),COLLEGE_ID)+ ',' + CONVERT(NVARCHAR(50),AREA_INT_NO) + ','+CONVERT(NVARCHAR(50),DEGREENO) + ',' + CONVERT(NVARCHAR(50),BRANCHNO)+ ',' + CONVERT(NVARCHAR(50),CAMPUSNO)+ ',' + CONVERT(NVARCHAR(50),ADMBATCH))PREFERENCESNO", "USERNO=" + Convert.ToInt32(Request.QueryString["userno"]) + "AND BRANCH_PREF=1"));
        string PREF2 = (objCommon.LookUp("ACD_USER_BRANCH_PREF", "(CONVERT(NVARCHAR(50),COLLEGE_ID)+ ',' + CONVERT(NVARCHAR(50),AREA_INT_NO) + ','+CONVERT(NVARCHAR(50),DEGREENO) + ',' + CONVERT(NVARCHAR(50),BRANCHNO)+ ',' + CONVERT(NVARCHAR(50),CAMPUSNO)+ ',' + CONVERT(NVARCHAR(50),ADMBATCH))PREFERENCESNO", "USERNO=" + Convert.ToInt32(Request.QueryString["userno"]) + "AND BRANCH_PREF=2"));
        string PREF3 = (objCommon.LookUp("ACD_USER_BRANCH_PREF", "(CONVERT(NVARCHAR(50),COLLEGE_ID)+ ',' + CONVERT(NVARCHAR(50),AREA_INT_NO) + ','+CONVERT(NVARCHAR(50),DEGREENO) + ',' + CONVERT(NVARCHAR(50),BRANCHNO)+ ',' + CONVERT(NVARCHAR(50),CAMPUSNO)+ ',' + CONVERT(NVARCHAR(50),ADMBATCH))PREFERENCESNO", "USERNO=" + Convert.ToInt32(Request.QueryString["userno"]) + "AND BRANCH_PREF=3"));
        if (PREF1.ToString() != "" || PREF1.ToString() != null)
        {
            ddlPrefrence1.Text = PREF1.ToString();
        }
        if (PREF2.ToString() != "" || PREF2.ToString() != null)
        {
            ddlPrefrence2.Text = PREF2.ToString();
        }
        if (PREF3.ToString() != "" || PREF3.ToString() != null)
        {
            ddlPrefrence3.Text = PREF3.ToString();
        }
        
    }
    private void ApplicantPersonalDetails()
    {
        try
        {
            idno = Convert.ToInt32(Request.QueryString["userno"]);
            DataSet dsStudent = objnuc.GetStudPersonalDetails(Convert.ToInt32(Request.QueryString["userno"].ToString()));
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                DataRow dr = dsStudent.Tables[0].Rows[0];
                lblFirstNameP.Text = dr["FIRSTNAME"].ToString() == string.Empty ? string.Empty : dr["FIRSTNAME"].ToString();
                lblmiddele.Text = dr["MIDDLENAME"].ToString() == string.Empty ? string.Empty : dr["MIDDLENAME"].ToString();
                lblLastNameP.Text = dr["LASTNAME"].ToString() == string.Empty ? string.Empty : dr["LASTNAME"].ToString();
                lblInitialName.Text = dr["NAME_INITIAL"].ToString() == string.Empty ? string.Empty : dr["NAME_INITIAL"].ToString();
                lblEmailP.Text = dr["EMAILID"].ToString() == string.Empty ? string.Empty : dr["EMAILID"].ToString();
                lblMobileNoP.Text = dr["MOBILE"].ToString() == string.Empty ? string.Empty : dr["MOBILE"].ToString();
                lblNIC.Text = dr["NIC"].ToString() == string.Empty ? string.Empty : dr["NIC"].ToString();
                lblPassPortNo.Text = dr["PASSPORTNO"].ToString() == string.Empty ? string.Empty : dr["PASSPORTNO"].ToString();
                lblDOB.Text = dr["DOB"].ToString() == string.Empty ? string.Empty : dr["DOB"].ToString();
                lblGender.Text = dr["SEX"].ToString() == string.Empty ? string.Empty : dr["SEX"].ToString();
                lblCitizenType.Text = dr["CITIZEN"].ToString() == string.Empty ? string.Empty : dr["CITIZEN"].ToString();
                lblLRHanded.Text = dr["LRHANDED"].ToString() == string.Empty ? string.Empty : dr["LRHANDED"].ToString();

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
    private void BindAddressData()
    {
        try
        {
            DataSet ds = objnuc.GetAddressDetails(Convert.ToInt32(Request.QueryString["userno"].ToString()));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lblAddress.Text = ds.Tables[0].Rows[0]["PADDRESS"].ToString();
                lblProvince.Text = ds.Tables[0].Rows[0]["STATENAME"].ToString();
                lblCountry.Text = ds.Tables[0].Rows[0]["COUNTRYNAME"].ToString();
                lblDistrict.Text = ds.Tables[0].Rows[0]["DISTRICTNAME"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AddressDetails.BindData-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void bindOfferAcceptance()
    {
        try
        {
            DataSet ds = null;
            ds = objdocContr.getOfferAcceptanceDetails(Convert.ToInt32(Request.QueryString["userno"]), Convert.ToInt32(1), Convert.ToInt32(0));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lstProgramName.DataSource = ds.Tables[0];
                lstProgramName.DataBind();
                pnlProgramName.Visible = true;
            }
            else
            {
                lstProgramName.DataSource = null;
                lstProgramName.DataBind();
            }
            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                lvStudentFees.DataSource = ds.Tables[1];
                lvStudentFees.DataBind();
            }
            else
            {
                lvStudentFees.DataSource = null;
                lvStudentFees.DataBind();
            }

            if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
            {
                ddlcamous.Items.Clear();
                ddlcamous.Items.Insert(0, "Please Select");
                ddlcamous.DataSource = ds.Tables[2];
                ddlcamous.DataValueField = "CAMPUSNO";
                ddlcamous.DataTextField = "CAMPUSNAME";
                ddlcamous.DataBind();
            }
            if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
            {
                if (Convert.ToString(ds.Tables[3].Rows[0]["BRANCH_PREF"]) != "0")
                {
                    ddlcamous.SelectedValue = Convert.ToString(ds.Tables[3].Rows[0]["BRANCH_PREF"]);
                    ddlcamous_SelectedIndexChanged(new object(), new EventArgs());
                }
                if (Convert.ToString(ds.Tables[3].Rows[0]["STUDENT_TABLE"]) != "0")
                {
                    ddlcamous.SelectedValue = Convert.ToString(ds.Tables[3].Rows[0]["STUDENT_TABLE"]);
                    ddlcamous_SelectedIndexChanged(new object(), new EventArgs());
                    ddlweek.SelectedValue = Convert.ToString(ds.Tables[3].Rows[0]["WEEKSNOS"]);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void FillDropDownListEducation()
    {
        NewUser objNewUr = new NewUser();
        objNewUr.UserNo = Convert.ToInt32(Request.QueryString["userno"]);
        objNewUr.Command_type = 4;
        objNewUr.StlQno = Convert.ToInt32(ddlALTypeUG.SelectedValue);
        DataSet ds = objnuc.GetAppliedUserEducationDetails(objNewUr);


        ddlALTypeUG.DataSource = ds.Tables[0];
        ddlALTypeUG.DataValueField = "ALTYPENO";
        ddlALTypeUG.DataTextField = "ALTYPENAME";
        ddlALTypeUG.DataBind();

        ddlStreamUG.DataSource = ds.Tables[1];
        ddlStreamUG.DataValueField = "STREAMNO";
        ddlStreamUG.DataTextField = "STREAMNAME";
        ddlStreamUG.DataBind();

        ddlALPassesUG.DataSource = ds.Tables[2];
        ddlALPassesUG.DataValueField = "QUALILEVELNO";
        ddlALPassesUG.DataTextField = "QUALILEVELNAME";
        ddlALPassesUG.DataBind();

        ddlOLType.DataSource = ds.Tables[0];
        ddlOLType.DataValueField = "ALTYPENO";
        ddlOLType.DataTextField = "ALTYPENAME";
        ddlOLType.DataBind();

        ddlolStream.DataSource = ds.Tables[1];
        ddlolStream.DataValueField = "STREAMNO";
        ddlolStream.DataTextField = "STREAMNAME";
        ddlolStream.DataBind();

        ddlolpass.DataSource = ds.Tables[5];
        ddlolpass.DataValueField = "QUALILEVELNO_OL";
        ddlolpass.DataTextField = "QUALILEVELNAME_OL";
        ddlolpass.DataBind();
    }

    protected void BindListViewDataEducation()
    {

        NewUser objNewUr = new NewUser();
        objNewUr.UserNo = Convert.ToInt32(Request.QueryString["userno"]);
        objNewUr.Command_type = 1;
        objNewUr.StlQno = 0;
        DataSet ds = objnuc.GetAppliedUserEducationDetails(objNewUr);
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            txtALIndex.Text = ds.Tables[0].Rows[0]["ALINDEXNO"].ToString();
            txtALyear.Text = ds.Tables[0].Rows[0]["ALYEAR"].ToString();
            txtZScore.Text = ds.Tables[0].Rows[0]["ALZSCORE"].ToString();
            txtALSchoolDistrict.Text = ds.Tables[0].Rows[0]["ALDISTRICT"].ToString();
            txtALSchool.Text = ds.Tables[0].Rows[0]["ALSCHOOL"].ToString();

            ddlALPassesUG.SelectedValue = ds.Tables[0].Rows[0]["ATTEMPTNO"].ToString();
            ddlALPassesUG_SelectedIndexChanged(new object(), new EventArgs());
            ddlALTypeUG.SelectedValue = ds.Tables[0].Rows[0]["AL_TYPE"].ToString();
            if (ds.Tables[0].Rows[0]["ATTEMPTNO"].ToString() != "6")
            {
                ddlALTypeUG_SelectedIndexChanged(new object(), new EventArgs());
            }
            ddlStreamUG.SelectedValue = ds.Tables[0].Rows[0]["STREAMNO"].ToString();
            ddlSubject1.SelectedValue = ds.Tables[0].Rows[0]["SUBJECT1"].ToString();

            ddlSubject1_SelectedIndexChanged(new object(), new EventArgs());
            if (ds.Tables[0].Rows[0]["ATTEMPTNO"].ToString() != "1")
            {
                ddlGrade1.SelectedValue = ds.Tables[0].Rows[0]["GRADE1"].ToString();
            }
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["SUBJECT2"].ToString()) > 0)
            {
                ddlSubject2.SelectedValue = ds.Tables[0].Rows[0]["SUBJECT2"].ToString();

                ddlSubject2_SelectedIndexChanged(new object(), new EventArgs());
                if (ds.Tables[0].Rows[0]["ATTEMPTNO"].ToString() != "1")
                {
                    ddlGrade2.SelectedValue = ds.Tables[0].Rows[0]["GRADE2"].ToString();
                }
            }
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["SUBJECT3"].ToString()) > 0)
            {
                ddlSubject3.SelectedValue = ds.Tables[0].Rows[0]["SUBJECT3"].ToString();

                ddlSubject3_SelectedIndexChanged(new object(), new EventArgs());
                if (ds.Tables[0].Rows[0]["ATTEMPTNO"].ToString() != "1")
                {
                    ddlGrade3.SelectedValue = ds.Tables[0].Rows[0]["GRADE3"].ToString();
                }
            }
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["SUBJECT4"].ToString()) > 0)
            {
                ddlSubject4.SelectedValue = ds.Tables[0].Rows[0]["SUBJECT4"].ToString();
                if (ds.Tables[0].Rows[0]["ATTEMPTNO"].ToString() != "1")
                {
                    ddlGrade4.SelectedValue = ds.Tables[0].Rows[0]["GRADE4"].ToString();
                }
            }
            ddlOLType.SelectedValue = ds.Tables[0].Rows[0]["OLTYPE"].ToString();
            if (ddlOLType.SelectedIndex > 0)
            {
                ddlOLType_SelectedIndexChanged(new object(), new EventArgs());
            }
            ddlolStream.SelectedValue = ds.Tables[0].Rows[0]["OLSTREAMNO"].ToString();
            ddlolpass.SelectedValue = ds.Tables[0].Rows[0]["OLATTEMPTNO"].ToString();

            if (ds.Tables[0].Rows[0]["OLSUBJECT1"].ToString() != "0")
            {
                olddlsub1.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT1"].ToString();
                olddlgrade1.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE1"].ToString();

                olddlsub1_SelectedIndexChanged(new object(), new EventArgs());
            }
            if (ds.Tables[0].Rows[0]["OLSUBJECT2"].ToString() != "0")
            {
                olddlsubj2.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT2"].ToString();
                olddlgrade2.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE2"].ToString();

                olddlsubj2_SelectedIndexChanged(new object(), new EventArgs());
            }
            if (ds.Tables[0].Rows[0]["OLSUBJECT3"].ToString() != "0")
            {
                olddlsub3.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT3"].ToString();
                olddlgrade3.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE3"].ToString();

                olddlsub3_SelectedIndexChanged(new object(), new EventArgs());
            }
            if (ds.Tables[0].Rows[0]["OLSUBJECT4"].ToString() != "0")
            {
                olddlsub4.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT4"].ToString();
                olddlgrade4.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE4"].ToString();

                olddlsub4_SelectedIndexChanged(new object(), new EventArgs());
            }
            if (ds.Tables[0].Rows[0]["OLSUBJECT5"].ToString() != "0")
            {
                olddlsub5.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT5"].ToString();
                olddlgrade5.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE5"].ToString();
                olddlsub5_SelectedIndexChanged(new object(), new EventArgs());
            }
            if (ds.Tables[0].Rows[0]["OLSUBJECT6"].ToString() != "0")
            {
                olddlsub6.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT6"].ToString();

                olddlgrade6.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE6"].ToString();

                olddlsub6_SelectedIndexChanged(new object(), new EventArgs());
            }
            if (ds.Tables[0].Rows[0]["OLSUBJECT7"].ToString() != "0")
            {
                olddlsub7.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT7"].ToString();

                olddlgrade7.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE7"].ToString();

                olddlsub7_SelectedIndexChanged(new object(), new EventArgs());
            }
            if (ds.Tables[0].Rows[0]["OLSUBJECT8"].ToString() != "0")
            {
                olddlsub8.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT8"].ToString();

                olddlgrade8.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE8"].ToString();

                olddlsub8_SelectedIndexChanged(new object(), new EventArgs());
            }
            if (ds.Tables[0].Rows[0]["OLSUBJECT9"].ToString() != "0")
            {
                olddlsub9.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT9"].ToString();

                olddlgrade9.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE9"].ToString();
            }

        }
    }
    protected void ddlALTypeUG_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlALTypeUG.SelectedIndex > 0)
            {
                NewUser objNewUr = new NewUser();
                objNewUr.UserNo = Convert.ToInt32(Request.QueryString["userno"]);
                objNewUr.StlQno = Convert.ToInt32(ddlALTypeUG.SelectedValue);   // NOTE STLQNO USE AS A/L TYPE FOR COMMAND TYPE 3
                objNewUr.Command_type = 3;
                ViewState["DYNAMICFILLDROPDOWN"] = null;
                ddlSubject1.Items.Clear();
                ddlSubject1.Items.Insert(0, "Please Select");
                ddlGrade1.Items.Clear();
                ddlGrade1.Items.Insert(0, "Please Select");
                ddlGrade2.Items.Clear();
                ddlGrade2.Items.Insert(0, "Please Select");
                ddlGrade3.Items.Clear();
                ddlGrade3.Items.Insert(0, "Please Select");
                ddlGrade4.Items.Clear();
                ddlGrade4.Items.Insert(0, "Please Select");

                if (ddlALTypeUG.SelectedValue == "2")
                {
                    objCommon.FillDropDownList(ddlStreamUG, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO = 8", "STREAMNAME");
                    ddlStreamUG.SelectedIndex = 1;
                }
                else if (ddlALTypeUG.SelectedValue == "3")
                {
                    objCommon.FillDropDownList(ddlStreamUG, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO = 9", "STREAMNAME");
                    ddlStreamUG.SelectedIndex = 1;
                }
                else if (ddlALTypeUG.SelectedValue == "4")
                {
                    objCommon.FillDropDownList(ddlStreamUG, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO = 7", "STREAMNAME");
                    ddlStreamUG.SelectedIndex = 1;
                }
                else
                {
                    objCommon.FillDropDownList(ddlStreamUG, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO NOT IN(7,8,9)", "STREAMNAME");
                }

                DataSet ds = objnuc.GetAppliedUserEducationDetails(objNewUr);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["DYNAMICFILLDROPDOWN"] = ds.Tables[0];
                    ddlSubject1.DataSource = ds.Tables[0];
                    ddlSubject1.DataTextField = "AL_COURSES";
                    ddlSubject1.DataValueField = "ID";
                    ddlSubject1.DataBind();


                    ddlGrade1.DataSource = ds.Tables[1];
                    ddlGrade1.DataValueField = "ID";
                    ddlGrade1.DataTextField = "GRADES";
                    ddlGrade1.DataBind();

                    ddlGrade2.DataSource = ds.Tables[1];
                    ddlGrade2.DataValueField = "ID";
                    ddlGrade2.DataTextField = "GRADES";
                    ddlGrade2.DataBind();

                    ddlGrade3.DataSource = ds.Tables[1];
                    ddlGrade3.DataValueField = "ID";
                    ddlGrade3.DataTextField = "GRADES";
                    ddlGrade3.DataBind();

                    ddlGrade4.DataSource = ds.Tables[1];
                    ddlGrade4.DataValueField = "ID";
                    ddlGrade4.DataTextField = "GRADES";
                    ddlGrade4.DataBind();
                }
                else
                {
                    //objCommon.DisplayMessage(updEdcutationalDetailsUG, "Subject 1 Details Not Found Please Contact Online Admission Department.", this.Page);
                    return;
                }
            }
            else
            {
                ddlSubject1.Items.Clear();
                ddlSubject1.Items.Insert(0, "Please Select");
                ddlSubject2.Items.Clear();
                ddlSubject2.Items.Insert(0, "Please Select");
                ddlSubject3.Items.Clear();
                ddlSubject3.Items.Insert(0, "Please Select");
                ddlSubject4.Items.Clear();
                ddlSubject4.Items.Insert(0, "Please Select");

                ddlGrade1.Items.Clear();
                ddlGrade1.Items.Insert(0, "Please Select");
                ddlGrade2.Items.Clear();
                ddlGrade2.Items.Insert(0, "Please Select");
                ddlGrade3.Items.Clear();
                ddlGrade3.Items.Insert(0, "Please Select");
                ddlGrade4.Items.Clear();
                ddlGrade4.Items.Insert(0, "Please Select");

                // objCommon.DisplayMessage(updEdcutationalDetailsUG, "Please Select A/L Type.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlSubject1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubject1.SelectedIndex > 0)
            {
                ddlSubject2.Items.Clear();
                ddlSubject2.Items.Insert(0, "Please Select");
                ddlSubject3.Items.Clear();
                ddlSubject3.Items.Insert(0, "Please Select");
                ddlSubject4.Items.Clear();
                ddlSubject4.Items.Insert(0, "Please Select");

                DataTable ds = (DataTable)ViewState["DYNAMICFILLDROPDOWN"];
                if (ds != null && ds.Rows.Count > 0)
                {
                    ddlSubject2.DataSource = ds;
                    ddlSubject2.DataTextField = "AL_COURSES";
                    ddlSubject2.DataValueField = "ID";
                    ddlSubject2.DataBind();

                    System.Web.UI.WebControls.ListItem itemToRemove = ddlSubject1.Items.FindByText(ddlSubject1.SelectedItem.Text);
                    ddlSubject2.Items.Remove(itemToRemove);
                    if (ddlSubject2.Items.Count == 1)
                    {
                        //objCommon.DisplayMessage(updEdcutationalDetailsUG, "Subject 2 Details Not Found Please Contact Online Admission Department.", this.Page);
                        return;
                    }
                }
                else
                {
                    //objCommon.DisplayMessage(updEdcutationalDetailsUG, "Subject 2 Details Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                ddlSubject2.Items.Clear();
                ddlSubject2.Items.Insert(0, "Please Select");
                ddlSubject3.Items.Clear();
                ddlSubject3.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlSubject2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubject2.SelectedIndex > 0)
            {
                ddlSubject3.Items.Clear();
                ddlSubject3.Items.Insert(0, "Please Select");
                ddlSubject4.Items.Clear();
                ddlSubject4.Items.Insert(0, "Please Select");

                DataTable ds = (DataTable)ViewState["DYNAMICFILLDROPDOWN"];
                if (ds != null && ds.Rows.Count > 0)
                {
                    ddlSubject3.DataSource = ds;
                    ddlSubject3.DataTextField = "AL_COURSES";
                    ddlSubject3.DataValueField = "ID";
                    ddlSubject3.DataBind();

                    System.Web.UI.WebControls.ListItem itemToRemove = ddlSubject1.Items.FindByText(ddlSubject1.SelectedItem.Text);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject2 = ddlSubject2.Items.FindByText(ddlSubject2.SelectedItem.Text);
                    ddlSubject3.Items.Remove(itemToRemove);
                    ddlSubject3.Items.Remove(itemToRemoveSubject2);
                    if (ddlSubject3.Items.Count == 1)
                    {
                        //  objCommon.DisplayMessage(updEdcutationalDetailsUG, "Subject 3 Details Not Found Please Contact Online Admission Department.", this.Page);
                        return;
                    }
                }
                else
                {
                    // objCommon.DisplayMessage(updEdcutationalDetailsUG, "Subject 3 Details Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                ddlSubject3.Items.Clear();
                ddlSubject3.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlSubject3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubject3.SelectedIndex > 0)
            {
                ddlSubject4.Items.Clear();
                ddlSubject4.Items.Insert(0, "Please Select");

                DataTable ds = (DataTable)ViewState["DYNAMICFILLDROPDOWN"];
                if (ds != null && ds.Rows.Count > 0)
                {
                    ddlSubject4.DataSource = ds;
                    ddlSubject4.DataTextField = "AL_COURSES";
                    ddlSubject4.DataValueField = "ID";
                    ddlSubject4.DataBind();

                    System.Web.UI.WebControls.ListItem itemToRemove = ddlSubject1.Items.FindByText(ddlSubject1.SelectedItem.Text);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject2 = ddlSubject2.Items.FindByText(ddlSubject2.SelectedItem.Text);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject3 = ddlSubject3.Items.FindByText(ddlSubject3.SelectedItem.Text);
                    ddlSubject4.Items.Remove(itemToRemove);
                    ddlSubject4.Items.Remove(itemToRemoveSubject2);
                    ddlSubject4.Items.Remove(itemToRemoveSubject3);
                    if (ddlSubject4.Items.Count == 1)
                    {
                        //  objCommon.DisplayMessage(updEdcutationalDetailsUG, "Subject 4 Details Not Found Please Contact Online Admission Department.", this.Page);
                        return;
                    }
                }
                else
                {
                    // objCommon.DisplayMessage(updEdcutationalDetailsUG, "Subject 4 Details Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                ddlSubject4.Items.Clear();
                ddlSubject4.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlOLType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlOLType.SelectedIndex > 0)
            {
                NewUser objNewUr = new NewUser();
                objNewUr.UserNo = Convert.ToInt32(Request.QueryString["userno"]);
                objNewUr.StlQno = Convert.ToInt32(ddlOLType.SelectedValue);   // NOTE STLQNO USE AS A/L TYPE FOR COMMAND TYPE 3
                objNewUr.Command_type = 3;
                ViewState["DYNAMICFILLDROPDOWNOL"] = null;
                olddlsub1.Items.Clear();
                olddlsub1.Items.Insert(0, "Please Select");
                olddlgrade1.Items.Clear();
                olddlgrade1.Items.Insert(0, "Please Select");
                olddlgrade2.Items.Clear();
                olddlgrade2.Items.Insert(0, "Please Select");
                olddlgrade3.Items.Clear();
                olddlgrade3.Items.Insert(0, "Please Select");
                olddlgrade4.Items.Clear();
                olddlgrade4.Items.Insert(0, "Please Select");

                olddlgrade5.Items.Clear();
                olddlgrade5.Items.Insert(0, "Please Select");

                olddlgrade6.Items.Clear();
                olddlgrade6.Items.Insert(0, "Please Select");

                if (ddlOLType.SelectedValue == "2")
                {
                    objCommon.FillDropDownList(ddlolStream, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO = 8", "STREAMNAME");
                    ddlolStream.SelectedIndex = 1;
                }
                else if (ddlOLType.SelectedValue == "3")
                {
                    objCommon.FillDropDownList(ddlolStream, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO = 9", "STREAMNAME");
                    ddlolStream.SelectedIndex = 1;
                }
                else if (ddlOLType.SelectedValue == "4")
                {
                    objCommon.FillDropDownList(ddlolStream, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO = 7", "STREAMNAME");
                    ddlolStream.SelectedIndex = 1;
                }
                else
                {
                    objCommon.FillDropDownList(ddlolStream, "ACD_STREAM", "DISTINCT STREAMNO", "STREAMNAME", "ACTIVE = 1 AND STREAMNO NOT IN(7,8,9)", "STREAMNAME");
                }

                DataSet ds = objnuc.GetAppliedUserEducationDetails(objNewUr);
                if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    ViewState["DYNAMICFILLDROPDOWNOL"] = ds.Tables[2];
                    olddlsub1.DataSource = ds.Tables[2];
                    olddlsub1.DataTextField = "OL_COURSES";
                    olddlsub1.DataValueField = "ID";
                    olddlsub1.DataBind();
                }
                if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                {
                    olddlgrade1.DataSource = ds.Tables[3];
                    olddlgrade1.DataValueField = "ID";
                    olddlgrade1.DataTextField = "GRADES";
                    olddlgrade1.DataBind();

                    olddlgrade2.DataSource = ds.Tables[3];
                    olddlgrade2.DataValueField = "ID";
                    olddlgrade2.DataTextField = "GRADES";
                    olddlgrade2.DataBind();

                    olddlgrade3.DataSource = ds.Tables[3];
                    olddlgrade3.DataValueField = "ID";
                    olddlgrade3.DataTextField = "GRADES";
                    olddlgrade3.DataBind();

                    olddlgrade4.DataSource = ds.Tables[3];
                    olddlgrade4.DataValueField = "ID";
                    olddlgrade4.DataTextField = "GRADES";
                    olddlgrade4.DataBind();

                    olddlgrade5.DataSource = ds.Tables[3];
                    olddlgrade5.DataValueField = "ID";
                    olddlgrade5.DataTextField = "GRADES";
                    olddlgrade5.DataBind();


                    olddlgrade6.DataSource = ds.Tables[3];
                    olddlgrade6.DataValueField = "ID";
                    olddlgrade6.DataTextField = "GRADES";
                    olddlgrade6.DataBind();


                }
                else
                {
                    //  objCommon.DisplayMessage(updEdcutationalDetailsUG, "Subject 1 Details Not Found Please Contact Online Admission Department.", this.Page);
                    return;
                }
            }
            else
            {
                olddlsub1.Items.Clear();
                olddlsub1.Items.Insert(0, "Please Select");
                olddlsubj2.Items.Clear();
                olddlsubj2.Items.Insert(0, "Please Select");
                olddlsub3.Items.Clear();
                olddlsub3.Items.Insert(0, "Please Select");
                olddlsub4.Items.Clear();
                olddlsub4.Items.Insert(0, "Please Select");

                olddlgrade1.Items.Clear();
                olddlgrade1.Items.Insert(0, "Please Select");
                olddlgrade2.Items.Clear();
                olddlgrade2.Items.Insert(0, "Please Select");
                olddlgrade3.Items.Clear();
                olddlgrade3.Items.Insert(0, "Please Select");
                olddlgrade4.Items.Clear();
                olddlgrade4.Items.Insert(0, "Please Select");

                olddlgrade5.Items.Clear();
                olddlgrade5.Items.Insert(0, "Please Select");

                olddlgrade6.Items.Clear();
                olddlgrade6.Items.Insert(0, "Please Select");

                ddlolStream.Items.Clear();
                ddlolStream.Items.Insert(0, "Please Select");

                //  objCommon.DisplayMessage(updEdcutationalDetailsUG, "Please Select O/L Type.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void olddlsub5_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (olddlsub5.SelectedIndex > 0)
            {
                olddlsub6.Items.Clear();
                olddlsub6.Items.Insert(0, "Please Select");

                DataTable ds = (DataTable)ViewState["DYNAMICFILLDROPDOWNOL"];
                if (ds != null && ds.Rows.Count > 0)
                {
                    olddlsub6.DataSource = ds;
                    olddlsub6.DataTextField = "OL_COURSES";
                    olddlsub6.DataValueField = "ID";
                    olddlsub6.DataBind();

                    System.Web.UI.WebControls.ListItem itemToRemove = olddlsub1.Items.FindByValue(olddlsub1.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject2 = olddlsub1.Items.FindByValue(olddlsubj2.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject3 = olddlsub3.Items.FindByValue(olddlsub3.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject4 = olddlsub4.Items.FindByValue(olddlsub4.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject5 = olddlsub5.Items.FindByValue(olddlsub5.SelectedValue);
                    olddlsub6.Items.Remove(itemToRemove);
                    olddlsub6.Items.Remove(itemToRemoveSubject2);
                    olddlsub6.Items.Remove(itemToRemoveSubject3);
                    olddlsub6.Items.Remove(itemToRemoveSubject4);
                    olddlsub6.Items.Remove(itemToRemoveSubject5);
                    if (olddlsub6.Items.Count == 1)
                    {
                        // objCommon.DisplayMessage(updEdcutationalDetailsUG, "Subject 6 Details Not Found Please Contact Online Admission Department.", this.Page);
                        return;
                    }
                }
                else
                {
                    // objCommon.DisplayMessage(updEdcutationalDetailsUG, "Subject 6 Details Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                olddlsub6.Items.Clear();
                olddlsub6.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void olddlsub4_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (olddlsub4.SelectedIndex > 0)
            {
                olddlsub5.Items.Clear();
                olddlsub5.Items.Insert(0, "Please Select");

                DataTable ds = (DataTable)ViewState["DYNAMICFILLDROPDOWNOL"];
                if (ds != null && ds.Rows.Count > 0)
                {
                    olddlsub5.DataSource = ds;
                    olddlsub5.DataTextField = "OL_COURSES";
                    olddlsub5.DataValueField = "ID";
                    olddlsub5.DataBind();

                    System.Web.UI.WebControls.ListItem itemToRemove = olddlsub1.Items.FindByValue(olddlsub1.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject2 = olddlsub1.Items.FindByValue(olddlsubj2.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject3 = olddlsub3.Items.FindByValue(olddlsub3.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject4 = olddlsub3.Items.FindByValue(olddlsub4.SelectedValue);
                    olddlsub5.Items.Remove(itemToRemove);
                    olddlsub5.Items.Remove(itemToRemoveSubject2);
                    olddlsub5.Items.Remove(itemToRemoveSubject3);
                    olddlsub5.Items.Remove(itemToRemoveSubject4);
                    if (olddlsub5.Items.Count == 1)
                    {
                        //  objCommon.DisplayMessage(updEdcutationalDetailsUG, "Subject 5 Details Not Found Please Contact Online Admission Department.", this.Page);
                        return;
                    }
                }
                else
                {
                    //  objCommon.DisplayMessage(updEdcutationalDetailsUG, "Subject 5 Details Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                olddlsub6.Items.Clear();
                olddlsub6.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void olddlsub3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (olddlsub3.SelectedIndex > 0)
            {
                olddlsub4.Items.Clear();
                olddlsub4.Items.Insert(0, "Please Select");

                DataTable ds = (DataTable)ViewState["DYNAMICFILLDROPDOWNOL"];
                if (ds != null && ds.Rows.Count > 0)
                {
                    olddlsub4.DataSource = ds;
                    olddlsub4.DataTextField = "OL_COURSES";
                    olddlsub4.DataValueField = "ID";
                    olddlsub4.DataBind();

                    System.Web.UI.WebControls.ListItem itemToRemove = olddlsub1.Items.FindByValue(olddlsub1.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject2 = olddlsub1.Items.FindByValue(olddlsubj2.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject3 = olddlsub3.Items.FindByValue(olddlsub3.SelectedValue);
                    olddlsub4.Items.Remove(itemToRemove);
                    olddlsub4.Items.Remove(itemToRemoveSubject2);
                    olddlsub4.Items.Remove(itemToRemoveSubject3);
                    if (olddlsub4.Items.Count == 1)
                    {
                        //  objCommon.DisplayMessage(updEdcutationalDetailsUG, "Subject 4 Details Not Found Please Contact Online Admission Department.", this.Page);
                        return;
                    }
                }
                else
                {
                    //  objCommon.DisplayMessage(updEdcutationalDetailsUG, "Subject 4 Details Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                olddlsub4.Items.Clear();
                olddlsub4.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void olddlsubj2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (olddlsubj2.SelectedIndex > 0)
            {
                olddlsub3.Items.Clear();
                olddlsub3.Items.Insert(0, "Please Select");
                olddlsub4.Items.Clear();
                olddlsub4.Items.Insert(0, "Please Select");

                DataTable ds = (DataTable)ViewState["DYNAMICFILLDROPDOWNOL"];
                if (ds != null && ds.Rows.Count > 0)
                {
                    olddlsub3.DataSource = ds;
                    olddlsub3.DataTextField = "OL_COURSES";
                    olddlsub3.DataValueField = "ID";
                    olddlsub3.DataBind();

                    System.Web.UI.WebControls.ListItem itemToRemove = olddlsub1.Items.FindByValue(olddlsub1.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject2 = olddlsubj2.Items.FindByValue(olddlsubj2.SelectedValue);
                    olddlsub3.Items.Remove(itemToRemove);
                    olddlsub3.Items.Remove(itemToRemoveSubject2);
                    if (olddlsub3.Items.Count == 1)
                    {
                        //  objCommon.DisplayMessage(updEdcutationalDetailsUG, "Subject 3 Details Not Found Please Contact Online Admission Department.", this.Page);
                        return;
                    }
                }
                else
                {
                    // objCommon.DisplayMessage(updEdcutationalDetailsUG, "Subject 3 Details Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                olddlsub3.Items.Clear();
                olddlsub3.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void olddlsub1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (olddlsub1.SelectedIndex > 0)
            {
                olddlsubj2.Items.Clear();
                olddlsubj2.Items.Insert(0, "Please Select");
                olddlsub3.Items.Clear();
                olddlsub3.Items.Insert(0, "Please Select");
                olddlsub4.Items.Clear();
                olddlsub4.Items.Insert(0, "Please Select");

                DataTable ds = (DataTable)ViewState["DYNAMICFILLDROPDOWNOL"];
                if (ds != null && ds.Rows.Count > 0)
                {
                    olddlsubj2.DataSource = ds;
                    olddlsubj2.DataTextField = "OL_COURSES";
                    olddlsubj2.DataValueField = "ID";
                    olddlsubj2.DataBind();

                    System.Web.UI.WebControls.ListItem itemToRemove = olddlsub1.Items.FindByValue(olddlsub1.SelectedValue);
                    olddlsubj2.Items.Remove(itemToRemove);
                    if (olddlgrade2.Items.Count == 1)
                    {
                        // objCommon.DisplayMessage(updEdcutationalDetailsUG, "Subject 2 Details Not Found Please Contact Online Admission Department.", this.Page);
                        return;
                    }
                }
                else
                {
                    //  objCommon.DisplayMessage(updEdcutationalDetailsUG, "Subject 2 Details Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                olddlgrade2.Items.Clear();
                olddlgrade2.Items.Insert(0, "Please Select");
                olddlsub3.Items.Clear();
                olddlsub3.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlALPassesUG_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlALPassesUG.SelectedValue == "6")
            {
                ddlSubject1.Items.Clear();
                ddlSubject1.Items.Insert(0, "Please Select");
                ddlSubject2.Items.Clear();
                ddlSubject2.Items.Insert(0, "Please Select");
                ddlSubject3.Items.Clear();
                ddlSubject3.Items.Insert(0, "Please Select");
                ddlSubject4.Items.Clear();
                ddlSubject4.Items.Insert(0, "Please Select");

                ddlGrade1.Items.Clear();
                ddlGrade1.Items.Insert(0, "Please Select");
                ddlGrade2.Items.Clear();
                ddlGrade2.Items.Insert(0, "Please Select");
                ddlGrade3.Items.Clear();
                ddlGrade3.Items.Insert(0, "Please Select");
                ddlGrade4.Items.Clear();
                ddlGrade4.Items.Insert(0, "Please Select");

                ddlSubject1.Enabled = false;
                ddlSubject2.Enabled = false;
                ddlSubject3.Enabled = false;
                ddlSubject4.Enabled = false;

                ddlGrade1.Enabled = false;
                ddlGrade2.Enabled = false;
                ddlGrade3.Enabled = false;
                ddlGrade4.Enabled = false;

            }
            else if (ddlALPassesUG.SelectedValue == "1")
            {
                ddlALTypeUG_SelectedIndexChanged(new object(), new EventArgs());

                ddlGrade1.Items.Clear();
                ddlGrade1.Items.Insert(0, "Please Select");
                ddlGrade2.Items.Clear();
                ddlGrade2.Items.Insert(0, "Please Select");
                ddlGrade3.Items.Clear();
                ddlGrade3.Items.Insert(0, "Please Select");
                ddlGrade4.Items.Clear();
                ddlGrade4.Items.Insert(0, "Please Select");

                ddlSubject1.Enabled = false;
                ddlSubject2.Enabled = false;
                ddlSubject3.Enabled = false;
                ddlSubject4.Enabled = false;

                ddlGrade1.Enabled = false;
                ddlGrade2.Enabled = false;
                ddlGrade3.Enabled = false;
                ddlGrade4.Enabled = false;
            }
            else
            {
                ddlSubject1.Enabled = false;
                ddlSubject2.Enabled = false;
                ddlSubject3.Enabled = false;
                ddlSubject4.Enabled = false;

                ddlGrade1.Enabled = false;
                ddlGrade2.Enabled = false;
                ddlGrade3.Enabled = false;
                ddlGrade4.Enabled = false;
                ddlALTypeUG_SelectedIndexChanged(new object(), new EventArgs());
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void BindlistViewPG(int USERNO)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_USER_LAST_QUALIFICATION AL", "AL.STLQNO", "*", "USERNO=" + USERNO + "", "AL.STLQNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtHighestEducationPG.Text = ds.Tables[0].Rows[0]["HIGHESTEDUCATION"].ToString();
                txtUniversityPG.Text = ds.Tables[0].Rows[0]["UNIVERSITYINSTITUTE"].ToString();
                txtQualificationAwardPG.Text = ds.Tables[0].Rows[0]["QUALAWARDYEAR"].ToString();
                txtSpecializationPG.Text = ds.Tables[0].Rows[0]["SPECILIZOFQUAL"].ToString();
                txtGPAPG.Text = ds.Tables[0].Rows[0]["GPAOFQUAL"].ToString();
                txtProfessionalPG.Text = ds.Tables[0].Rows[0]["PROFQUAL"].ToString();
                txtProfessionalUniversityPG.Text = ds.Tables[0].Rows[0]["PROFUNIVINSTIT"].ToString();
                txtAwardDatePG.Text = ds.Tables[0].Rows[0]["QUALAWARDOFDATE"].ToString();
                txtSpecilizationQualificationPG.Text = ds.Tables[0].Rows[0]["SPECILOFQUAL"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EducationDetailsPG.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindUserDataPG()
    {
        try
        {
            DataSet ds = null;
            ds = objnuc.getPHDEdcucationalDetails(Convert.ToInt32(Request.QueryString["userno"]));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvRefreesPG.DataSource = ds.Tables[0];
                lvRefreesPG.DataBind();
            }
            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                lvEmploymentHistoryPG.DataSource = ds.Tables[1];
                lvEmploymentHistoryPG.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void CheckStudyLevel()
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("ACD_USER_REGISTRATION", "DISTINCT UGPGOT", "ADMBATCH", "USERNO=" + Convert.ToInt32(Request.QueryString["userno"]), "");
        if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToString(ds.Tables[0].Rows[0]["UGPGOT"]) == "1" || Convert.ToString(ds.Tables[0].Rows[0]["UGPGOT"]) == "3" || Convert.ToString(ds.Tables[0].Rows[0]["UGPGOT"]) == "6")
            {

                divUgEducationUG.Visible = true; divEducationPG.Visible = false; divEducationDetailsPHD.Visible = false;
                FillDropDownListEducation();
                BindListViewDataEducation();
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["UGPGOT"]) == "2" || Convert.ToString(ds.Tables[0].Rows[0]["UGPGOT"]) == "7")
            {
                divUgEducationUG.Visible = false; divEducationPG.Visible = false; divEducationDetailsPHD.Visible = false;
                BindlistViewPG(Convert.ToInt32(Request.QueryString["userno"]));
                BindUserDataPG();
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["UGPGOT"]) == "4")
            {
                divEducationPG.Visible = false;
                divEducationDetailsPHD.Visible = false;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["UGPGOT"]) == "5")
            {
                divEducationPG.Visible = false;
                divEducationDetailsPHD.Visible = true;
                BindUserDataPHD();
            }
        }
    }

    protected void BindUserDataPHD()
    {
        try
        {
            DataSet ds = null;
            ds = objnuc.getPHDEdcucationalDetails(Convert.ToInt32(Request.QueryString["userno"]));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvRefrees.DataSource = ds.Tables[0];
                lvRefrees.DataBind();
            }
            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                lvEmploymentHistory.DataSource = ds.Tables[1];
                lvEmploymentHistory.DataBind();
            }
            if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
            {
                txtNameofQualificationPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_QUALIFICATION"]);
                txtYearofAwardPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_YEAR_OF_AWARD"]);
                txtUniversityPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_UNIVERSITY"]);
                txtMainSpecialtyPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_MAIN_SPECIALITY"]);
                txtGPAPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_GPA"]);
                txtNameQualificationPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_OTHER_QUALIFICATION"]);
                txtAwardingUniversityPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_AWARDING_INSTITUTE"]);
                txtAwardDatePHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_DATE_AWARD"]);
                txtSpecilizationQualificationPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_SPECIALIZATION"]);
                txtDescriptionPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_DESCRIPTION"]);
                ddlModePHD.SelectedValue = Convert.ToString(ds.Tables[2].Rows[0]["PHD_MODE_NO"]);
                rdbQuestion1PHD.SelectedValue = Convert.ToString(ds.Tables[2].Rows[0]["PHD_FIRST_QUESTION_NO"]);
                rdbQuestion1PHD_SelectedIndexChanged(new object(), new EventArgs());
                txtQuestionDetailsPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_FIRST_QUESTION_DETAILS"]);
                rdbQuestion2PHD.SelectedValue = Convert.ToString(ds.Tables[2].Rows[0]["PHD_SECOND_QUESTION_NO"]);
                rdbQuestion2PHD_SelectedIndexChanged(new object(), new EventArgs());
                txtQuestion1DetailsPHD.Text = Convert.ToString(ds.Tables[2].Rows[0]["PHD_SECOND_QUESTION_DETAILS"]);
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void rdbQuestion1PHD_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbQuestion1PHD.SelectedValue == "1")
            {
                divDetails.Visible = true;
            }
            else
            {
                divDetails.Visible = false;
                txtQuestionDetailsPHD.Text = "";
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void rdbQuestion2PHD_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbQuestion2PHD.SelectedValue == "1")
            {
                divDetails1.Visible = true;
            }
            else
            {
                divDetails1.Visible = false;
                txtQuestion1DetailsPHD.Text = "";
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkViewSlip_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            int value = int.Parse(lnk.CommandArgument);
            ImageViewer.Visible = false;
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
                string extension = Path.GetExtension(img.ToString());
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

                var Newblob = blobContainer.GetBlockBlobReference(ImageName);

                string filePath = directoryPath + "\\" + ImageName;


                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                ViewState["filePath_Show"] = filePath;
                Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);

                if (extension == ".pdf")
                {
                    byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                    string Base64String = Convert.ToBase64String(bytes);
                    Session["Base64String"] = Base64String;
                    Session["Base64StringType"] = "application/pdf";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);

                    //imageViewerContainer.Visible = false;
                    //ltEmbed.Visible = true;
                    //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"750px\" height=\"400px\">";
                    //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    //embed += "</object>";
                    //ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
                }
                else
                {
                    byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                    string Base64String = Convert.ToBase64String(bytes);
                    Session["Base64String"] = Base64String;
                    Session["Base64StringType"] = "image/png";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
                    //hdfImagePath.Value = null;
                    //imageViewerContainer.Visible = true;
                    //ltEmbed.Visible = false;
                    //hdfImagePath.Value = ResolveUrl("~/DownloadImg/" + ImageName);
                    //ImageViewer.ImageUrl = ResolveUrl("~/DownloadImg/" + ImageName);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Sorry, File not found !!!", this.Page);
            }
            //ltEmbed.Text = "";
            //ImageViewer.Visible = true;
            //ImageViewer.ImageUrl = "~/showimage.aspx?id=" + Convert.ToInt32(value) + "&type=DCRTEMPPHOTO";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
        }
        catch (Exception ex)
        {
        }

    }
    protected void ddlcamous_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlcamous.SelectedIndex > 0)
            {
                DataSet ds = null;
                ds = objdocContr.getOfferAcceptanceDetails(Convert.ToInt32(Request.QueryString["userno"]), Convert.ToInt32(2), Convert.ToInt32(ddlcamous.SelectedValue));
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlweek.Items.Clear();
                    ddlweek.Items.Insert(0, "Please Select");
                    ddlweek.DataSource = ds.Tables[0];
                    ddlweek.DataValueField = "WEEKSNOS";
                    ddlweek.DataTextField = "WEEKDAYSNAME";
                    ddlweek.DataBind();
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Week Day/Week End Not Found For Selected Campus !!!", this.Page);
                }
            }
            else
            {
                ddlweek.Items.Clear();
                ddlweek.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void lnkFinalSubmit_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            string parameter = "";
            //if (ddlcamous.SelectedIndex > 0)
            //{
            CustomStatus cs = CustomStatus.Others;

            int Pay = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1)", "PAY_SERVICE_TYPE=5 AND RECON=1 AND IDNO=" + Convert.ToInt32(ViewState["ID_NO"]) + "AND SEMESTERNO=" + Convert.ToInt32(ViewState["SEMESTERNO"])));
            if (Pay > 0)
            {
                cs = (CustomStatus)objdocContr.UpdateFinalUserConformation(Convert.ToInt32(Request.QueryString["userno"]), 0, 0, Convert.ToInt32(Session["userno"]));
                if (cs == CustomStatus.RecordSaved)
                {
                    objCommon.DisplayMessage(this.Page, "Record Saved Successfully !!!", this.Page);


                    //ShowConfirmationReport("Final_Confirmation_Report", "AdmisionConfirmSlip.rpt", Convert.ToInt32(ViewState["ID_NO"]));
                    string FinalStatus = "";
                    FinalStatus = objCommon.LookUp("ACD_STUDENT", "ISNULL(ENROLLNO,0)", "ISNULL(CAN,0) = 0 AND ISNULL(ADMCAN,0) = 0 AND IDNO=" + Convert.ToInt32(ViewState["ID_NO"]));
                    if (FinalStatus.ToString() == "0" || FinalStatus.ToString() == string.Empty)
                    {
                        lnkSendEmail.Enabled = true;
                        lnkGeneratereport.Visible = false;
                        lnkPrintReport.Visible = false;
                        btnFrontBackReport.Visible = false;
                    }
                    else
                    {
                        lnkSendEmail.Enabled = true;
                        lnkGeneratereport.Visible = true;
                        lnkPrintReport.Visible = true;
                        //btnFrontBackReport.Visible = true;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Error !!!", this.Page);
                    return;
                }

                DataSet dsUserContact = null;
                UserController objUC = new UserController();
                dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", Convert.ToString(Request.QueryString["userno"]), Convert.ToInt32(Request.QueryString["pageno"]));

                if (dsUserContact.Tables[1] != null && dsUserContact.Tables[1].Rows.Count > 0)
                {
                    if (dsUserContact.Tables[3] != null && dsUserContact.Tables[3].Rows.Count > 0)
                    {
                        string Subject = dsUserContact.Tables[1].Rows[1]["SUBJECT"].ToString();
                        string message = "";
                        message = dsUserContact.Tables[1].Rows[1]["TEMPLATE"].ToString();
                        string name = dsUserContact.Tables[3].Rows[0]["STUDNAME"].ToString();
                        string degreename = dsUserContact.Tables[3].Rows[0]["DEGREENAME"].ToString();
                        string campusname = dsUserContact.Tables[3].Rows[0]["CAMPUSNAME"].ToString();
                        string weekdayname = dsUserContact.Tables[3].Rows[0]["WEEKDAYSNAME"].ToString();
                        string date = dsUserContact.Tables[3].Rows[0]["COMMENCE_DATE"].ToString();

                        EmailSmsWhatssppSend(Convert.ToInt32(Request.QueryString["pageno"]), Convert.ToString(Request.QueryString["userno"]), message, lblEmailID.Text, Subject, name, degreename, campusname, weekdayname, date, Convert.ToInt32(ViewState["ID_NO"]), Session["colcode"].ToString());
                        //int status = task.Result;
                        //SmtpMail oMail = new SmtpMail("TryIt");

                        //oMail.From = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]);

                        //oMail.To = lblEmailID.Text;

                        //oMail.Subject = Subject;

                        //oMail.HtmlBody = message;

                        //oMail.HtmlBody = oMail.HtmlBody.Replace("[USERFIRSTNAME]", dsUserContact.Tables[3].Rows[0]["STUDNAME"].ToString());
                        //oMail.HtmlBody = oMail.HtmlBody.Replace("[PROGRAM]", dsUserContact.Tables[3].Rows[0]["DEGREENAME"].ToString());
                        //oMail.HtmlBody = oMail.HtmlBody.Replace("[CAMPUS]", dsUserContact.Tables[3].Rows[0]["CAMPUSNAME"].ToString());
                        //oMail.HtmlBody = oMail.HtmlBody.Replace("[WEEKDAY]", dsUserContact.Tables[3].Rows[0]["WEEKDAYSNAME"].ToString());
                        //oMail.HtmlBody = oMail.HtmlBody.Replace("[Date]", dsUserContact.Tables[3].Rows[0]["COMMENCE_DATE"].ToString());

                        ////SmtpServer oServer = new SmtpServer("smtp.live.com");
                        //SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022

                        //oServer.User = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]);
                        //oServer.Password = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"]);

                        //oServer.Port = 587;

                        //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                        //EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();

                        //oSmtp.SendMail(oServer, oMail);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Unable to send email !!!", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Unable to send email !!!", this.Page);
                }

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //String username = "INTUSER";
                //String password = "fDeV+bNLaeBVneQYhSbs0LgtiB7G5iCd2qOPuu/JUts=";
                //String encoded = username + ":" + password;


                //var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://172.16.68.55:7048/DynamicsNAV80/OData/Company('SLIIT')/studentdetails?$format=json");
                //httpWebRequest.Headers.Add("Authorization", "Basic " + encoded);
                //httpWebRequest.PreAuthenticate = true;
                //httpWebRequest.ContentType = "application/json";
                //httpWebRequest.Method = "POST";

                //string SP_Parameters = ""; string Call_Values = ""; string SP_Name = "";

                //SP_Name = "PKG_GET_DETAILS_FOR_API_STUDENT_DETAILS";
                //SP_Parameters = "@P_IDNO";
                //Call_Values = "" + Convert.ToInt32(ViewState["ID_NO"]) + "";

                //DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

                //if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                //{
                //    var client = new RestClient("http://172.16.68.55:7048/DynamicsNAV80/OData/Company('SLIIT')/studentdetails?$format=json");
                //    client.Timeout = -1;
                //    var request = new RestRequest(Method.POST);
                //    request.AddHeader("Authorization", "Basic SU5UVVNFUjpmRGVWK2JOTGFlQlZuZVFZaFNiczBMZ3RpQjdHNWlDZDJxT1B1dS9KVXRzPQ==");
                //    request.AddHeader("Content-Type", "application/json");
                //    request.AddParameter("application/json", "{\r\n\"No\": \"" + ds.Tables[0].Rows[0]["REGISTRATION_NO"].ToString() + "\",\r\n  \"SLR_No\": \"" + ds.Tables[0].Rows[0]["APPLICATION_NO"].ToString() + "\",\r\n  \"Search_Name\": \"" + ds.Tables[0].Rows[0]["NAME_WITH_INITIAL"].ToString() + "\",\r\n  \"Name\": \"" + ds.Tables[0].Rows[0]["STUDNAME"].ToString() + "\",\r\n  \"Address\": \"" + ds.Tables[0].Rows[0]["PADDRESS"].ToString() + "\",\r\n  \"Address_2\": \"" + ds.Tables[0].Rows[0]["ADDRESS_2"].ToString() + "\",\r\n  \"Phone_No\": \"" + ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() + "\",\r\n  \"Gen_Bus_Posting_Group\": \"" + ds.Tables[0].Rows[0]["GEN_BUS_POSTING_GROUP"].ToString() + "\",\r\n  \"Email\": \"" + ds.Tables[0].Rows[0]["EMAILID"].ToString() + "\",\r\n  \"VAT_Bus_Posting_Group\": \"" + ds.Tables[0].Rows[0]["VAT_BUS_POSTING_GROUP"].ToString() + "\",\r\n  \"Customer_Type\": \"" + ds.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString() + "\",\r\n  \"Faculty\": \"" + ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString() + "\",\r\n  \"Specialization\": \"" + ds.Tables[0].Rows[0]["LONGNAME"].ToString() + "\",\r\n  \"University\": \"" + ds.Tables[0].Rows[0]["CAMPUSNAME"].ToString() + "\",\r\n  \"NIC\": \"" + ds.Tables[0].Rows[0]["NICNO"].ToString() + "\",\r\n  \"Gender\": \"M\",\r\n  \"Mobile_No\": \"" + ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() + "\",\r\n  \"Permanent_Address\": \"" + ds.Tables[0].Rows[0]["PADDRESS"].ToString() + "\",\r\n  \"Programme\": \"" + ds.Tables[0].Rows[0]["DEGREENAME"].ToString() + "\"\r\n}", ParameterType.RequestBody);
                //    IRestResponse response = client.Execute(request);
                //    Console.WriteLine(response.Content);
                //    parameter = "Registration No:" + ds.Tables[0].Rows[0]["REGISTRATION_NO"].ToString() + "SLR_No:" + ds.Tables[0].Rows[0]["APPLICATION_NO"].ToString() + "Search_Name:" + ds.Tables[0].Rows[0]["NAME_WITH_INITIAL"].ToString() + "Name:" + ds.Tables[0].Rows[0]["STUDNAME"].ToString() + "Address:" + ds.Tables[0].Rows[0]["PADDRESS"].ToString() + "Address_2:" + ds.Tables[0].Rows[0]["ADDRESS_2"].ToString() + "Phone_No:" + ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() + "Gen_Bus_Posting_Group:" + ds.Tables[0].Rows[0]["GEN_BUS_POSTING_GROUP"].ToString() + "Email:" + ds.Tables[0].Rows[0]["EMAILID"].ToString() + "VAT_Bus_Posting_Group:" + ds.Tables[0].Rows[0]["VAT_BUS_POSTING_GROUP"].ToString() + "Customer_Type:" + ds.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString() + "Faculty:" + ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString() + "Specialization:" + ds.Tables[0].Rows[0]["LONGNAME"].ToString() + "University:" + ds.Tables[0].Rows[0]["CAMPUSNAME"].ToString() + "NIC:" + ds.Tables[0].Rows[0]["NICNO"].ToString() + "Mobile_No:" + ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() + "Permanent_Address:" + ds.Tables[0].Rows[0]["PADDRESS"].ToString() + "Programme:" + ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                //    string SP_Name1 = "PKG_ACD_RESPONSE_FOR_STUDENT_API";
                //    string SP_Parameters1 = "@P_IDNO,@P_RESULT,@P_STATUS,@P_RESPONCE_VALUE,@P_OUTPUT";
                //    string Call_Values1 = "" + Convert.ToInt32(ViewState["ID_NO"]) + "," + Convert.ToString(response.StatusDescription) + "," + "Applicant_Preview" + "," + Convert.ToString(parameter) + ",0";
                //    string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);
                //}
                DynamicPdfViewer();
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Payment Not Received !!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void EmailSmsWhatssppSend(int Page_No, string userno, string Message, string toEmailId, string sub, string name, string degreename, string campusname, string weekdayname, string date, int idno, string College_Code)
    {
        EmailSmsWhatsaap Email = new EmailSmsWhatsaap();
        DataSet ds = objCommon.FillDropDown("ACCESS_LINK", "isnull(EMAIL,0) EMAIL,isnull(SMS,0) as SMS,isnull(WHATSAAP,0) as WHATSAAP", "", "AL_NO=" + Convert.ToInt32(Request.QueryString["pageno"].ToString()), "");
        string Res = "";
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["EMAIL"].ToString()) == 1)//For Email Send 
        {
            int PageNo = Page_No;
            //int PageNo = 33;
            UserController objUC = new UserController();
            DataSet dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", userno.ToString(), PageNo);
            if (dsUserContact.Tables[1] != null && dsUserContact.Tables[1].Rows.Count > 0)
            {
                string Subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                Message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                string toSendAddress = toEmailId.ToString();// 
                MailMessage msgsPara = new MailMessage();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Message);
                msgsPara.BodyEncoding = Encoding.UTF8;
                msgsPara.Body = Convert.ToString(sb);

                msgsPara.Body = msgsPara.Body.Replace("[USERFIRSTNAME]", name.ToString());
                msgsPara.Body = msgsPara.Body.Replace("[PROGRAM]", degreename.ToString());
                msgsPara.Body = msgsPara.Body.Replace("[CAMPUS]", campusname.ToString());
                msgsPara.Body = msgsPara.Body.Replace("[WEEKDAY]", weekdayname.ToString());
                msgsPara.Body = msgsPara.Body.Replace("[Date]", date.ToString());

                MemoryStream Attachment = null; string AttachmentName = "Certificate_Admission.pdf";

                MemoryStream oAttachment1 = ShowGeneralExportReportForMailForApplication("Reports,Academic,Certi_Admission.rpt", "@P_IDNO=" + Convert.ToInt32(idno) + ",@P_COLLEGE_CODE=" + College_Code.ToString() + "");

                Task<string> task = Email.Execute(msgsPara.Body, toSendAddress, Subject, Attachment, AttachmentName.ToString());
                Res = task.Result;
                // objCommon.DisplayMessage(this, Res.ToString(), this.Page);
            }
        }
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["SMS"].ToString()) == 1) //For SMS Send 
        {
            //string MobileNo = "9503244325";
            //string Msg = "Your ERP Password has been reset successfully! Your new Login Password is ";
            //string TemplateID = "1007583285079323052";
            //Res = Email.SendSMS(MobileNo.ToString(), Msg.ToString(), TemplateID.ToString());
            //objCommon.DisplayMessage(this, Res.ToString(), this.Page);
        }
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["WHATSAAP"].ToString()) == 1)//For Whatsaap Send 
        {
            //int Ress = Email.SendWhatsaapOPT("950324432", "Roshan", "20222");
        }
    }
    static async Task<int> Execute(string Message, string toEmailId, string sub, string name, string degreename, string campusname, string weekdayname, string date, int idno, string College_Code)
    {
        int ret = 0;

        try
        {

            Common objCommon = new Common();
            DataSet dsconfig = null;

            // dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_USERNAME", "CollegeName,COMPANY_EMAILSVCID ,SENDGRID_PWD ,API_KEY_SENDGRID", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            var fromAddress = new System.Net.Mail.MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["CollegeName"].ToString());
            var toAddress = new System.Net.Mail.MailAddress(toEmailId, "");
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var apiKey = dsconfig.Tables[0].Rows[0]["API_KEY_SENDGRID"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["CollegeName"].ToString());
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            //var htmlContent = Message;
            MailMessage msgs = new MailMessage();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Message);
            msgs.BodyEncoding = Encoding.UTF8;
            msgs.Body = Convert.ToString(sb);

            msgs.Body = msgs.Body.Replace("[USERFIRSTNAME]", name.ToString());
            msgs.Body = msgs.Body.Replace("[PROGRAM]", degreename.ToString());
            msgs.Body = msgs.Body.Replace("[CAMPUS]", campusname.ToString());
            msgs.Body = msgs.Body.Replace("[WEEKDAY]", weekdayname.ToString());
            msgs.Body = msgs.Body.Replace("[Date]", date.ToString());

            //htmlContent = htmlContent.Replace("[USERFIRSTNAME]", name);
            //htmlContent = htmlContent.Replace("[PROGRAM]", degreename);
            //htmlContent = htmlContent.Replace("[CAMPUS]", campusname);
            //htmlContent = htmlContent.Replace("[WEEKDAY]", weekdayname);
            //htmlContent = htmlContent.Replace("[Date]", date);

            MemoryStream oAttachment1 = ShowGeneralExportReportForMailForApplication("Reports,Academic,Certi_Admission.rpt", "@P_IDNO=" + Convert.ToInt32(idno) + ",@P_COLLEGE_CODE=" + College_Code.ToString() + "");
            //MemoryStream oAttachment = ShowGeneralExportReportForMail("Reports,Academic,FeeCollectionReceipt.rpt", "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DCRNO=" + hfddcr.Value + ",@P_IDNO=" + Convert.ToInt32(chk.ToolTip) + "");
            var bytesRpt = oAttachment1.ToArray();
            var fileRpt = Convert.ToBase64String(bytesRpt);
            byte[] test = (byte[])bytesRpt;
            //  msg.AddAttachment("Offerletter.pdf", test);     

            //System.Net.Mail.Attachment attachment;
            //attachment = new System.Net.Mail.Attachment(new MemoryStream(test), "Certificate_Admission.pdf");
            //msgs.Attachments.Add(attachment);

            msgs.BodyEncoding = Encoding.UTF8;
            msgs.IsBodyHtml = true;
            var htmlContent = msgs.Body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            msg.AddAttachment("Certificate_Admission.pdf", fileRpt);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            string res = Convert.ToString(response.StatusCode);
            if (res == "Accepted")
            {
                ret = 1;
            }
            else
            {
                ret = 0;

            }
        }
        catch (Exception ex)
        {
            ret = 0;
        }
        return ret;
    }

    static private MemoryStream ShowGeneralExportReportForMailForApplication(string path, string paramString)
    {
        MemoryStream oStream;
        ReportDocument customReport;
        customReport = new ReportDocument();
        string reportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Academic/Certi_Admission.rpt");
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
    private void ShowConfirmationReport(string reportTitle, string rptFileName, int idno)
    {
        try
        {
            string colgname = "SLIIT";// ViewState["College_Name"].ToString().Replace(" ", "_");

            string exporttype = "pdf";
            string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
            //    Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/commonreport.aspx?";
            url += "exporttype=" + exporttype;

            url += "&filename=" + colgname + "-" + reportTitle + "." + exporttype;

            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno;
            // url += "&param=@P_USERNO=" + ((UserDetails)(Session["user"])).UserNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            //url += "filename=" + "offerletter" + ".pdf";
            //url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNO=" + userno + ",@P_ADMBATCH=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlApplicantType.SelectedValue) + ",@P_ENTERANCE=" + Convert.ToInt32(ddlAdmcat.SelectedValue);

            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_REPORTS_gradeCard .ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void lnkGeneratereport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("StatusSlip", "rptCourseStatusSlip.rpt");
        }
        catch (Exception ex)
        {

        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
            string exporttype = "pdf";
            string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;

            url += "&filename=" + reportTitle + "." + rptFileName;

            url += "&path=~,Reports,Academic," + "OfferLetterBulk.rpt";
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(ViewState["ID_NO"].ToString());
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "OnlinepaymentStudentLogin.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkbtn = sender as LinkButton;
            string filename = lnkbtn.CommandName.ToString();
            int value = int.Parse(lnkbtn.CommandArgument);
            //if (value == 0)
            //{
            //    ImageViewer.ImageUrl = "~/showimage.aspx?id=" + Convert.ToInt32(ViewState["ID_NO"]) + "&type=STUDENT";
            //}
            //else
            //{
            string accountname = System.Configuration.ConfigurationManager.AppSettings["Blob_AccountName"].ToString();
            string accesskey = System.Configuration.ConfigurationManager.AppSettings["Blob_AccessKey"].ToString();
            string containerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

            StorageCredentials creden = new StorageCredentials(accountname, accesskey);
            CloudStorageAccount acc = new CloudStorageAccount(creden, useHttps: true);
            CloudBlobClient client = acc.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference(containerName);
            CloudBlob blob = container.GetBlobReference(filename);
            MemoryStream ms = new MemoryStream();
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            blob.DownloadToStream(ms);
            Response.ContentType = blob.Properties.ContentType;
            Response.AddHeader("Content-Disposition", "Attachment; filename=" + filename.ToString());
            Response.AddHeader("Content-Length", blob.Properties.Length.ToString());
            Response.BinaryWrite(ms.ToArray());
            //}
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnkPrintReport_Click(object sender, EventArgs e)
    {
        try
        {
            //using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            //{

            //    PdfPTable table = null;
            //    Document document = new Document(PageSize.A4, 88f, 88f, 10f, 10f);
            //    string SP_Name1 = "PKG_GET_CONFIRM_STUDENT_DATA";
            //    string SP_Parameters1 = "@P_IDNO";
            //    string Call_Values1 = "" + Convert.ToInt32(ViewState["ID_NO"]);

            //    DataSet que_out1 = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);
            //    string path = HttpContext.Current.Server.MapPath("~/IMAGES/SLIIT_logo.png");
            //    table = (PdfPTable)pdf.DynamicViewer(path, que_out1);

            //    document.Add(table);
            //    document.Close();
            //    byte[] bytes = memoryStream.ToArray();
            //    memoryStream.Close();

            //    Response.Clear();
            //    Response.ContentType = "application/pdf";
            //    Response.AddHeader("Content-Disposition", "attachment; filename=StudentSummarySheet.pdf");
            //    Response.ContentType = "application/pdf";
            //    Response.Buffer = true;
            //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //    Response.BinaryWrite(bytes);
            //    Response.End();
            //    Response.Close();
            //}
            DynamicPdfViewer();
            //ShowConfirmationReport("Final_Confirmation_Report", "AdmisionConfirmSlip.rpt", Convert.ToInt32(ViewState["ID_NO"]));
        }
        catch (Exception ex)
        {

        }
    }
    protected void DynamicPdfViewer()
    {
        try
        {
            Document document = new Document(PageSize.A4, 88f, 88f, 10f, 10f);
            Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable table = null;

                document.Open();

                //Header Table
                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.8f, 0.0f });

                //Company Logo
                cell = ImageCell("~/IMAGES/SLIIT_logo.png", 30f, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);

                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);

                //document.Add(table);
                document.Add(table);
                //Photo
                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.8f, 0.0f });

                string SP_Name1 = "PKG_GET_CONFIRM_STUDENT_DATA";
                string SP_Parameters1 = "@P_IDNO";
                string Call_Values1 = "" + Convert.ToInt32(ViewState["ID_NO"]);

                DataSet que_out1 = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);

                string s = Convert.ToBase64String((byte[])que_out1.Tables[0].Rows[0]["PHOTO"]);
                byte[] imageBytes = Convert.FromBase64String(s);

                cell = ImageCellByte(imageBytes, 11f, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);

                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);

                document.Add(table);

                table = new PdfPTable(3);
                table.SetWidths(new float[] { 0.3f, 0.1f, 0.5f });
                table.TotalWidth = 410f;
                table.LockedWidth = true;
                table.SpacingBefore = 10f;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;

                table.AddCell(PhraseCell(new Phrase("Intake", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["BATCHNAME"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Student Registration No", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["ENROLLNO"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Name with Initials", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["NAME_INITIAL"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Name in Full", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["STUDNAME"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("NIC / Passport", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["NICPASS"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Address", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["LADDRESS"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Contact No", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["STUDENTMOBILE"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Email", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["EMAILID"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("SLIIT Email", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["COLLEGE_EMAIL"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Programme", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["PROGRAM_NAME"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Campus", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["CAMPUSNAME"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Batch", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["WEEKDAYSNAME"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Date of Registration", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(que_out1.Tables[0].Rows[0]["REGDATE"]), FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Orientation Group", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(":", FontFactory.GetFont("Vardana", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell(new Phrase("", FontFactory.GetFont("Vardana", 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 3;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                document.Add(table);
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=StudentSummarySheet.pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
            }
        }
        catch (Exception ex)
        {

        }
    }
    private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, Color color)
    {
        PdfContentByte contentByte = writer.DirectContent;
        contentByte.SetColorStroke(color);
        contentByte.MoveTo(x1, y1);
        contentByte.LineTo(x2, y2);
        contentByte.Stroke();
    }
    private static PdfPCell PhraseCell(Phrase phrase, int align)
    {
        PdfPCell cell = new PdfPCell(phrase);
        cell.BorderColor = Color.WHITE;
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 2f;
        cell.PaddingTop = 0f;
        return cell;
    }
    private static PdfPCell ImageCell(string path, float scale, int align)
    {
        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path));
        image.ScalePercent(scale);
        PdfPCell cell = new PdfPCell(image);
        cell.BorderColor = Color.WHITE;
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 0f;
        cell.PaddingTop = 20f;
        return cell;
    }

    private static PdfPCell ImageCellByte(byte[] imageBytes, float scale, int align)
    {
        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageBytes);
        //image.ScalePercent(15f,15f);
        //var reader = new PdfReader(imageBytes);
        image.ScaleAbsolute(95f, 95f); // Set image size.
        //image.SetAbsolutePosition(reader.GetPageSize(1).Width / 2 - 100, 50);// Set image position.
        PdfPCell cell = new PdfPCell(image);
        cell.BorderColor = Color.WHITE;
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 0f;
        cell.PaddingTop = 20f;
        //cell.Width = 20f;

        return cell;
    }


    protected void lnkViewAdditionalDoc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            int value = int.Parse(lnk.CommandArgument);
            ImageViewer.Visible = false;
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
                ViewState["filePath_Show"] = filePath;
                Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                string Base64String = Convert.ToBase64String(bytes);
                Session["Base64String"] = Base64String;
                Session["Base64StringType"] = "application/pdf";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
                //ltEmbed.Visible = true;
                //hdfImagePath.Value = null;
                //imageViewerContainer.Visible = false;
                //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"750px\" height=\"400px\">";
                //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                //embed += "</object>";
                //ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
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
    protected void btnAdditionalDoc_Click(object sender, EventArgs e)
    {
        try
        {

            DocumentControllerAcad objDoc = new DocumentControllerAcad();
            DocumentAcad objDocno = new DocumentAcad();
            byte[] DocumentFile = null;

            string path = "", filenames = "", docnos = "", docnames = "", existsfile = "", remark = ""; int Count = 0;
            path = System.Configuration.ConfigurationManager.AppSettings["UploadDocPath"];

            foreach (ListViewDataItem item in LvAdditionalDoc.Items)
            {
                Label docno = item.FindControl("lblDocNo") as Label;
                Label DocName = item.FindControl("lblname") as Label;
                FileUpload fuDocument = item.FindControl("fuDocument") as FileUpload;
                TextBox txtRemark = item.FindControl("txtremark") as TextBox;

                if (fuDocument.HasFile)
                {
                    HttpPostedFile FileSize = fuDocument.PostedFile;
                    string ext = System.IO.Path.GetExtension(fuDocument.FileName);
                    if (ext == ".pdf" || ext == ".PDF")
                    {
                        if (FileSize.ContentLength <= 1000000)
                        {
                            Count++;
                            existsfile = path + Convert.ToInt32(Session["idno"]) + "_" + fuDocument.FileName;
                            FileInfo file = new FileInfo(existsfile);
                            if (file.Exists)//check file exsit or not  
                            {
                                file.Delete();
                            }
                            DocumentFile = objCommon.GetImageData(fuDocument);
                            int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, Convert.ToInt32(ViewState["ID_NO"].ToString()) + "_doc_" + docno.Text, fuDocument, DocumentFile);
                            if (retval == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                return;
                            }
                            //fuDocument.PostedFile.SaveAs(path + Path.GetFileName(Convert.ToInt32(Session["idno"]) + "_" + fuDocument.FileName));
                            filenames += Convert.ToInt32(ViewState["ID_NO"].ToString()) + "_doc_" + docno.Text + ext + '$';
                            docnos += docno.Text + '$';
                            docnames += DocName.Text + '$';
                            remark += txtRemark.Text + '$';
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
            if (Count > 0)
            {
                filenames = filenames.TrimEnd('$');
                docnos = docnos.TrimEnd('$');
                docnames = docnames.TrimEnd('$');
                remark = remark.TrimEnd('$');

                CustomStatus cs = CustomStatus.Others;
                cs = (CustomStatus)objDoc.AddAdditionalMultipleDocStd(Convert.ToInt32(ViewState["ID_NO"].ToString()), Convert.ToInt32(Request.QueryString["userno"]), filenames, docnos, docnames, path, remark);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.Page, "Document Added Successfully !!!", this.Page);
                    binddocumentlist();
                    return;
                }
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.Page, "Document Updated Successfully !!!", this.Page);
                    binddocumentlist();
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please select at least one document for upload !!!", this.Page);
            }
        }
        catch (Exception ex)
        {

        }
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

    protected void olddlsub6_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (olddlsub6.SelectedIndex > 0)
            {
                olddlsub7.Items.Clear();
                olddlsub7.Items.Insert(0, "Please Select");

                DataTable ds = (DataTable)ViewState["DYNAMICFILLDROPDOWNOL"];
                if (ds != null && ds.Rows.Count > 0)
                {
                    olddlsub7.DataSource = ds;
                    olddlsub7.DataTextField = "OL_COURSES";
                    olddlsub7.DataValueField = "ID";
                    olddlsub7.DataBind();

                    System.Web.UI.WebControls.ListItem itemToRemove = olddlsub1.Items.FindByValue(olddlsub1.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject2 = olddlsub1.Items.FindByValue(olddlsubj2.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject3 = olddlsub3.Items.FindByValue(olddlsub3.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject4 = olddlsub4.Items.FindByValue(olddlsub4.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject5 = olddlsub5.Items.FindByValue(olddlsub5.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject6 = olddlsub6.Items.FindByValue(olddlsub6.SelectedValue);
                    olddlsub7.Items.Remove(itemToRemove);
                    olddlsub7.Items.Remove(itemToRemoveSubject2);
                    olddlsub7.Items.Remove(itemToRemoveSubject3);
                    olddlsub7.Items.Remove(itemToRemoveSubject4);
                    olddlsub7.Items.Remove(itemToRemoveSubject5);
                    olddlsub7.Items.Remove(itemToRemoveSubject6);
                    if (olddlsub7.Items.Count == 1)
                    {
                        objCommon.DisplayMessage(this.Page, "Subject 7 Details Not Found Please Contact Online Admission Department.", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Subject 7 Details Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                olddlsub7.Items.Clear();
                olddlsub7.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void olddlsub7_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (olddlsub7.SelectedIndex > 0)
            {
                olddlsub8.Items.Clear();
                olddlsub8.Items.Insert(0, "Please Select");

                DataTable ds = (DataTable)ViewState["DYNAMICFILLDROPDOWNOL"];
                if (ds != null && ds.Rows.Count > 0)
                {
                    olddlsub8.DataSource = ds;
                    olddlsub8.DataTextField = "OL_COURSES";
                    olddlsub8.DataValueField = "ID";
                    olddlsub8.DataBind();

                    System.Web.UI.WebControls.ListItem itemToRemove = olddlsub1.Items.FindByValue(olddlsub1.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject2 = olddlsub1.Items.FindByValue(olddlsubj2.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject3 = olddlsub3.Items.FindByValue(olddlsub3.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject4 = olddlsub4.Items.FindByValue(olddlsub4.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject5 = olddlsub5.Items.FindByValue(olddlsub5.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject6 = olddlsub6.Items.FindByValue(olddlsub6.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject7 = olddlsub6.Items.FindByValue(olddlsub7.SelectedValue);
                    olddlsub8.Items.Remove(itemToRemove);
                    olddlsub8.Items.Remove(itemToRemoveSubject2);
                    olddlsub8.Items.Remove(itemToRemoveSubject3);
                    olddlsub8.Items.Remove(itemToRemoveSubject4);
                    olddlsub8.Items.Remove(itemToRemoveSubject5);
                    olddlsub8.Items.Remove(itemToRemoveSubject6);
                    olddlsub8.Items.Remove(itemToRemoveSubject7);
                    if (olddlsub8.Items.Count == 1)
                    {
                        objCommon.DisplayMessage(this.Page, "Subject 8 Details Not Found Please Contact Online Admission Department.", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Subject 8 Details Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                olddlsub8.Items.Clear();
                olddlsub8.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void olddlsub8_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (olddlsub8.SelectedIndex > 0)
            {
                olddlsub9.Items.Clear();
                olddlsub9.Items.Insert(0, "Please Select");

                DataTable ds = (DataTable)ViewState["DYNAMICFILLDROPDOWNOL"];
                if (ds != null && ds.Rows.Count > 0)
                {
                    olddlsub9.DataSource = ds;
                    olddlsub9.DataTextField = "OL_COURSES";
                    olddlsub9.DataValueField = "ID";
                    olddlsub9.DataBind();

                    System.Web.UI.WebControls.ListItem itemToRemove = olddlsub1.Items.FindByValue(olddlsub1.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject2 = olddlsub1.Items.FindByValue(olddlsubj2.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject3 = olddlsub3.Items.FindByValue(olddlsub3.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject4 = olddlsub4.Items.FindByValue(olddlsub4.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject5 = olddlsub5.Items.FindByValue(olddlsub5.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject6 = olddlsub6.Items.FindByValue(olddlsub6.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject7 = olddlsub6.Items.FindByValue(olddlsub7.SelectedValue);
                    System.Web.UI.WebControls.ListItem itemToRemoveSubject8 = olddlsub6.Items.FindByValue(olddlsub8.SelectedValue);
                    olddlsub9.Items.Remove(itemToRemove);
                    olddlsub9.Items.Remove(itemToRemoveSubject2);
                    olddlsub9.Items.Remove(itemToRemoveSubject3);
                    olddlsub9.Items.Remove(itemToRemoveSubject4);
                    olddlsub9.Items.Remove(itemToRemoveSubject5);
                    olddlsub9.Items.Remove(itemToRemoveSubject6);
                    olddlsub9.Items.Remove(itemToRemoveSubject7);
                    olddlsub9.Items.Remove(itemToRemoveSubject8);
                    if (olddlsub9.Items.Count == 1)
                    {
                        objCommon.DisplayMessage(this.Page, "Subject 9 Details Not Found Please Contact Online Admission Department.", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Subject 9 Details Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                olddlsub9.Items.Clear();
                olddlsub9.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnFrontBackReport_Click(object sender, EventArgs e)
    {
        try
        {

            ShowReport(Convert.ToString(ViewState["ID_NO"]), "Student_ID_Card_Report", "CopyofStudentIDCardFrontBack.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentIDCardReport.btnPrintReport_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }
    private void ShowReport(string param, string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));         
            //url += "Reports/CommonReport.aspx?";
            string regno = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO=" + param);
            string exporttype = "pdf";
            string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";

            url += "exporttype=" + exporttype;
            url += "&filename=" + regno + "." + exporttype;
            //url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_COLLEGE_ID=" + ViewState["COLLEGE_ID"] + ",@P_Valid_Upto=" + DateTime.Now.ToString("dd/MM/yyyy");

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "StudentIDCardReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
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
    protected void btnRefundInitiated_Click(object sender, EventArgs e)
    {
        idno = idno = Convert.ToInt32(Convert.ToInt32(ViewState["ID_NO"]));
        int faculty = Convert.ToInt32(ViewState["COLLEGE_ID"]);
        int program = Convert.ToInt32(ViewState["BRANCHNO"]);
        int degreeno = Convert.ToInt32(ViewState["degreeno"]);
        int admbatch = Convert.ToInt32(ViewState["batchno"]);
        int semesterno = Convert.ToInt32(ViewState["SEMESTERNO"]);
        string Status = "Withdrawal of Registration";
        int StatusNo = 1;
        string REGNO = Convert.ToString(ViewState["REGNO"]);
        var todaydate = DateTime.Now;
        CustomStatus cs = CustomStatus.Others;
        cs = (CustomStatus)feeController.InsertStudentWithdrawApplication(idno, faculty, program, REGNO, degreeno, "", "", Status, StatusNo, todaydate);
        DataSet dsUserContact = null;
        string message = "";
        string subject = "";
        string filename = "";
        dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(2995));
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
            message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
            ExecuteRefund(message, Convert.ToString(ViewState["EMAILID"]), subject, Convert.ToString(ViewState["NAME_WITH_INITIAL"]), Convert.ToString(ViewState["REGNO"]), filename, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
            objCommon.DisplayMessage(this.Page, "Record Added Successfully !!!", this.Page);
            return;
        }
        if (cs.Equals(CustomStatus.TransactionFailed))
        {
            objCommon.DisplayMessage(this.Page, "Transaction Failed !!!", this.Page);
            return;
        }
    }
    static async Task ExecuteRefund(string message, string toSendAddress, string Subject, string firstname, string username, string filename, string sendemail, string emailpass)
    {
        try
        {

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            MailMessage msg = new MailMessage();
            msg.To.Add(new System.Net.Mail.MailAddress(toSendAddress));
            msg.From = new System.Net.Mail.MailAddress(sendemail);
            msg.Subject = Subject;
            StringBuilder sb = new StringBuilder();
            msg.Body = message;
            msg.Body = msg.Body.Replace("[USERFIRSTNAME]", firstname.ToString());
            msg.Body = msg.Body.Replace("[USERNAME]", username.ToString());

            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(sendemail, emailpass);

            client.Port = 587; // You can use Port 25 if 587 is blocked (mine is!)
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


        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void lnkCertiAdmis_Click(object sender, EventArgs e)
    {
        ShowReportAdm("Certificate Admission", "Certi_Admission.rpt");
    }
    private void ShowReportAdm(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            //url += "Reports/CommonReport.aspx?";
            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(ViewState["ID_NO"]);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentReconsilReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlevalutionResult_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlevalutionResult.SelectedValue != "4")
        {
            evalstatus.Visible = true;
            divremarks.Visible = true;
        }
        else
        {
            evalstatus.Visible = false;
            divremarks.Visible = false;
        }
    }
    protected void btnSubmitEval_Click(object sender, EventArgs e)
    {

        if (ddlevalutionResult.SelectedValue == "1")
        {
            if (EvalRemark.Text=="")
            {
                objCommon.DisplayMessage(this.updEvalution, "Please Enter Remarks !!", this.Page);
                return;
            }
        }
         DataSet dsUserContact = null;
         UserController objUC = new UserController();
         string message = "", Subject="";
        string SP_Name1 = "PKG_ACD_INSERT_EVALUTION_RESULT";
        string SP_Parameters1 = "@P_USERNO,@P_EVAL_NO,@P_TEST_RECOMMENDED_STATUS,@P_REVAL_REMARK,@P_OUTPUT";
        string Call_Values1 = "" + Request.QueryString["userno"].ToString() + "," + Convert.ToInt32(ddlevalutionResult.SelectedValue.ToString()) + "," + Convert.ToInt32(rdiotest.SelectedValue) + "," +
            Convert.ToString(EvalRemark.Text)  + ",0";
        int PageNo = 0;
        string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//insert
        if (que_out1 == "1")
        {
            objCommon.DisplayMessage(this.updEvalution, "Evaluation Result Save Successfully !!", this.Page);
            ShowStudentDetails();
            if (rdiotest.SelectedValue == "1")
            {
                
                PageNo = 287990;
                //dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", Convert.ToString(ViewState["ID_NO"]), Convert.ToInt32(287990));
                //Subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                //message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
            }
            else
            {
                PageNo = 287991;
                //dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", Convert.ToString(ViewState["ID_NO"]), Convert.ToInt32(287991));
                //Subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                //message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
            }
         //   ExecuteEval(message, Convert.ToString(ViewState["EMAILID"]), Subject, Convert.ToString(ViewState["fullName"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"]), Convert.ToString(ddlevalutionResult.SelectedItem), Convert.ToString(rdiotest.SelectedItem)).Wait();
            EmailSmsWhatssppSendEval(PageNo, Convert.ToString(ViewState["EMAILID"]), Convert.ToString(ViewState["fullName"]), Convert.ToString(ViewState["ID_NO"]), Convert.ToString(ddlevalutionResult.SelectedItem), Convert.ToString(rdiotest.SelectedItem));
        }

    }
    static async Task ExecuteEval(string message, string toSendAddress, string Subject, string firstname, string ReffEmail, string ReffPassword, string EvalStatus, string yessno)
    {
        int ret = 0;

        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_USERNAME", "CollegeName,COMPANY_EMAILSVCID ,SENDGRID_PWD ,API_KEY_SENDGRID", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            var fromAddress = new System.Net.Mail.MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["CollegeName"].ToString());
            var toAddress = new System.Net.Mail.MailAddress(toSendAddress, "");
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var apiKey = dsconfig.Tables[0].Rows[0]["API_KEY_SENDGRID"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["CollegeName"].ToString());
            var subject = Subject;
            var to = new EmailAddress(toSendAddress, "");

            MailMessage msgs = new MailMessage();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(message);
            msgs.BodyEncoding = Encoding.UTF8;
            msgs.Body = Convert.ToString(sb);

            msgs.Body = msgs.Body.Replace("{User}", firstname.ToString());
            msgs.Body = msgs.Body.Replace("{status}", EvalStatus.ToString());
            msgs.Body = msgs.Body.Replace("[YESNO]", yessno.ToString());
            msgs.BodyEncoding = Encoding.UTF8;
            msgs.IsBodyHtml = true;
            var plainTextContent = "";
            var htmlContent = msgs.Body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            string res = Convert.ToString(response.StatusCode);
            if (res == "Accepted")
            {
                ret = 1;
            }
            else
            {
                ret = 0;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    //ADDED BY ROSHAN PATIL 12/06/2023
    protected void EmailSmsWhatssppSendEval(int Page_No, string toSendAddre, string Name, string IDNO, string EvalStatus, string yessno)
    {
        EmailSmsWhatsaap Email = new EmailSmsWhatsaap();
        DataSet ds = objCommon.FillDropDown("ACCESS_LINK", "isnull(EMAIL,0) EMAIL,isnull(SMS,0) as SMS,isnull(WHATSAAP,0) as WHATSAAP", "", "AL_NO=" + Convert.ToInt32(Request.QueryString["pageno"].ToString()), "");
        string Res = ""; string Message = "";
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["EMAIL"].ToString()) == 1)//For Email Send 
        {
            UserController objUC = new UserController();
            DataSet dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", IDNO, Page_No);
            if (dsUserContact.Tables[1] != null && dsUserContact.Tables[1].Rows.Count > 0)
            {

                string Subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                Message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                string toSendAddress = toSendAddre.ToString();// 
                MailMessage msgsPara = new MailMessage();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Message);
                msgsPara.BodyEncoding = Encoding.UTF8;
                msgsPara.Body = Convert.ToString(sb);
                msgsPara.Body = msgsPara.Body.Replace("{User}", Name.ToString());
                msgsPara.Body = msgsPara.Body.Replace("{status}", EvalStatus.ToString());
                msgsPara.Body = msgsPara.Body.Replace("[YESNO]", yessno.ToString());
                MemoryStream Attachment = null; string AttachmentName = "";
                Task<string> task = Email.Execute(msgsPara.Body, toSendAddress, Subject, Attachment, AttachmentName.ToString());
                Res = task.Result;

            }
        }
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["SMS"].ToString()) == 1) //For SMS Send 
        {

        }
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["WHATSAAP"].ToString()) == 1)//For Whatsaap Send 
        {

        }
    }
}
    