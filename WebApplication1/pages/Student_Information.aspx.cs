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
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;

using Microsoft.WindowsAzure.Storage;

public partial class ACADEMIC_Student_Information : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    StudentController objSC = new StudentController();
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

    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();


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
                DivPayment.Visible = false;
                DivMod.Visible = false;
                updfinalblock.Visible = false;
                DivMain.Visible = false;
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
                    if (Session["usertype"].ToString() == "2")
                    {
                        Search.Visible = false;
                        int idno = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO=" + (Session["userno"].ToString()) + ""));
                        ViewState["ID_NO"] = idno;
                        Session["urlAppno"] = idno;
                        //panelsearch.Visible = false;
                        UG.Visible = false;
                        DivShow.Visible = false;
                        DivMod.Visible = false;
                        DivSubmit.Visible = false;
                        updfinalblock.Visible = false;
                        DivMain.Visible = true;
                        DivResult.Visible = true;
                        DivPayment.Visible = true;

                    }

                  //query mgt page open student information 
                    else if (Request.QueryString["userno"].ToString() != null || Request.QueryString["userno"].ToString() != "")
                    {
                        int idno = Convert.ToInt32((objCommon.LookUp("ACD_STUDENT", "IDNO", "USERNO=" + (Request.QueryString["userno"]) + "")));
                        ViewState["ID_NO"] = idno;
                        Session["urlAppno"] = idno;
                        //panelsearch.Visible = false;
                        UG.Visible = false;
                        DivShow.Visible = false;
                        DivMod.Visible = false;
                        DivSubmit.Visible = false;
                        updfinalblock.Visible = false;
                        DivMain.Visible = true;
                        DivResult.Visible = true;
                        DivPayment.Visible = true;
                        Search.Visible = false;
                    }


                    {
                        //panelsearch.Visible = true;
                    }

                    Page.Title = Session["coll_name"].ToString();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                    //if (Session["urlAppno"].ToString() != null || Session["urlAppno"].ToString() != "")
                    //{
                    //    int idno = Convert.ToInt32((objCommon.LookUp("ACD_STUDENT", "IDNO", "USERNO=" + (Session["urlAppno"]) + "")));
                    //    ViewState["ID_NO"] = idno;
                    //}
                    //if (Session["usertype"].ToString() == "2")
                    //{
                    //    panelsearch.Visible = false;
                    //}
                    //else
                    //{
                    //    panelsearch.Visible = true; 
                    //}


                    //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
                    this.CheckPageAuthorization();
                    bindmodules();
                    binddocumentlist();
                    //paymentdetails();
                    //ShowStudentDetails(); //
                    ApplicantPersonalDetails();
                    BindAddressData();
                    bindOfferAcceptance();
                    Result();
                    ExternalResult();
                    SemesterRegistrationStatus();
                    FeesDetails();
                    BindListView();


                    CheckStudyLevel();
                    CheckUserEligiblity();
                    //FillDropDownListEducation(); 
                    //BindListViewDataEducation();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                }
                //objCommon.SetLabelData("0");
                //objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpConteQueryStringxt.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//For label
                //objCommon.SetHeaderLabelData(Convert.ToString(Request.["pageno"]));//for header

                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header

            }

            string FinalStatus = "";
            FinalStatus = objCommon.LookUp("ACD_STUDENT", "ISNULL(ENROLLNO,0)", "ISNULL(CAN,0) = 0 AND ISNULL(ADMCAN,0) = 0 AND IDNO=" + Convert.ToInt32(ViewState["ID_NO"]));
            if (FinalStatus.ToString() == "0" || FinalStatus.ToString() == string.Empty)
            {
                lnkSendEmail.Enabled = true;
                lnkGeneratereport.Visible = false;
                lnkPrintReport.Visible = false;
            }
            else
            {
                lnkSendEmail.Enabled = false;
                lnkGeneratereport.Visible = true;
                lnkPrintReport.Visible = true;
            }

            if (Request.Params["__EVENTTARGET"] != null &&
                 Request.Params["__EVENTTARGET"].ToString() != string.Empty)
            {

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

    protected void BindListView()
    {
        try
        {
            StudentController studCont = new StudentController();
            DataSet ds;

            if (Session["idno"] == null)
            {
                Response.Redirect("~/default.aspx");
                return;
            }

            int Degreeno = objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"])) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"])));
            int Branchno = objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + Convert.ToInt32(Session["idno"])) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + Convert.ToInt32(Session["idno"])));
            int Semesterno = objCommon.LookUp("ACD_STUDENT_RESULT", "MAX(DISTINCT SEMESTERNO)", "IDNO=" + Convert.ToInt32(Session["idno"])) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "MAX(DISTINCT SEMESTERNO)", "IDNO=" + Convert.ToInt32(Session["idno"])));
            int SESSIONNO = objCommon.LookUp("ACD_STUDENT_RESULT", "MAX(DISTINCT SESSIONNO)", "IDNO=" + Convert.ToInt32(Session["idno"])) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "MAX(DISTINCT SESSIONNO)", "IDNO=" + Convert.ToInt32(Session["idno"])));

            ds = studCont.GetEXAMINATION(Convert.ToInt32(ViewState["ID_NO"]));
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvStudentRecords.DataSource = ds;
                    lvStudentRecords.DataBind();
                }
                else
                {
                    //objCommon.DisplayUserMessage(updexamDetails, "Exam Time Table Not Created Yet. Please Create Exam Time Table!", this.Page);
                }
            }
            else
            {
                //objCommon.DisplayUserMessage(updexamDetails, "Record Not Found!!", this.Page);
                lvStudentRecords.DataSource = null;
                lvStudentRecords.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void CheckUserEligiblity()
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("ACD_STUDENT", "DISTINCT USERNO", "DEGREENO,BRANCHNO,COLLEGE_ID,ADMBATCH", "IDNO =" + Convert.ToInt32(ViewState["ID_NO"]), "");
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                DataSet ds1 = null;
                ds1 = objdocContr.getStudentEligiblityDetails(Convert.ToInt32(ds.Tables[0].Rows[0]["USERNO"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[0]["ADMBATCH"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString()));
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

            DataSet ds = new DataSet();
            // objNewUr.UserNo = (Convert.ToInt32(Session["urlAppno"]));
            idno = Convert.ToInt32(Session["urlAppno"]);
            int USERNO = Convert.ToInt32((objCommon.LookUp("ACD_STUDENT", "  USERNO", "IDNO=" + (Session["urlAppno"]) + "")));

            //ds = objdocContr.GetDoclistByAdmin(Convert.ToInt32(Session["urlAppno"]));
            ds = objdocContr.GetDoclistByAdmin(USERNO);


            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {

                lvDocument.DataSource = ds;
                lvDocument.DataBind();
                lvDocument.Visible = true;

                //foreach (ListViewDataItem row in lvDocument.Items)
                //{
                //    Label lblStatus = row.FindControl("lblStatus") as Label;
                //    DropDownList ddlStatus = row.FindControl("ddlstatus") as DropDownList;
                //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //    {
                //        if (lblStatus.Text.ToString() == ds.Tables[0].Rows[i]["DOC_STATUS"].ToString())
                //        {
                //            ddlStatus.SelectedValue = ds.Tables[0].Rows[i]["DOC_STATUS"].ToString();
                //        }
                //    }
                //}


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
                ImageViewer.Visible = true;
                ltEmbed.Text = "";
                ImageViewer.ImageUrl = "~/showimage.aspx?id=" + Convert.ToInt32(ViewState["ID_NO"]) + "&type=STUDENT";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
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

                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);


                    string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"750px\" height=\"400px\">";
                    embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    embed += "</object>";
                    ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
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

            foreach (ListViewDataItem item in lvDocument.Items)
            {
                Label docno = item.FindControl("lblDocNo") as Label;
                Label DocName = item.FindControl("lblname") as Label;
                TextBox txtRemark = item.FindControl("txtremark") as TextBox;
                DropDownList ddlStatus = item.FindControl("ddlstatus") as DropDownList;
                if (ddlStatus.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(this.Page, "Please Select Status for" + DocName.Text + " !!!", this.Page);
                    return;
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
                if (Convert.ToString(txtRemark.Text) == string.Empty)
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Remark for " + DocName.Text + " !!!", this.Page);
                    return;
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
                    Execute(message, lblEmailID.Text, Subject, lblStudName.Text, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
                }
                CustomStatus cs = CustomStatus.Others;
                cs = (CustomStatus)objDoc.UpdateDocumentStatus(Convert.ToInt32(Session["urlAppno"]), docnos, docnames, status, remark, Convert.ToInt32(Session["userno"]));
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
    static async Task Execute(string message, string toSendAddress, string Subject, string firstname, string ReffEmail, string ReffPassword)
    {
        try
        {
            //Common objCommon = new Common();

            SmtpMail oMail = new SmtpMail("TryIt");

            oMail.From = ReffEmail;

            oMail.To = toSendAddress;

            oMail.Subject = Subject;

            oMail.HtmlBody = message;

            oMail.HtmlBody = oMail.HtmlBody.Replace("{User}", firstname.ToString());

            // SmtpServer oServer = new SmtpServer("smtp.live.com");
            SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022


            oServer.User = ReffEmail;
            oServer.Password = ReffPassword;

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
    private void ShowStudentDetails()
    {
        try
        {
            idno = Convert.ToInt32(Session["urlAppno"]);
            DataSet dsStudent = feeController.GetStudentInfoById_ForOnlinePayment(Convert.ToInt32(ViewState["ID_NO"].ToString()));
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                DataRow dr = dsStudent.Tables[0].Rows[0];
                string fullName = dr["STUDNAME"].ToString();
                string[] names = fullName.Split(' ');
                string name = names.First();
                string lasName = names.Last();
                lblStudName.Text = fullName;
                lblSex.Text = dr["SEX"].ToString() == string.Empty ? string.Empty : dr["SEX"].ToString();
                lblRegNo1.Text = dr["REGNO"].ToString() == string.Empty ? string.Empty : dr["REGNO"].ToString();
                lblPaymentType.Text = dr["PAYTYPENAME"].ToString() == string.Empty ? string.Empty : dr["PAYTYPENAME"].ToString();
                lblYear.Text = dr["YEARNAME"].ToString() == string.Empty ? string.Empty : dr["YEARNAME"].ToString();
                lblSemester.Text = dr["SEMESTERNAME"].ToString() == string.Empty ? string.Empty : dr["SEMESTERNAME"].ToString();

                lblSemesterP.Text = dr["SEMESTERNAME"].ToString() == string.Empty ? string.Empty : dr["SEMESTERNAME"].ToString();

                lblBatch.Text = dr["BATCHNAME"].ToString() == string.Empty ? string.Empty : dr["BATCHNAME"].ToString();
                lblMobileNo.Text = dr["STUDENTMOBILE"].ToString() == string.Empty ? string.Empty : dr["STUDENTMOBILE"].ToString();
                lblEmailID.Text = dr["EMAILID"].ToString() == string.Empty ? string.Empty : dr["EMAILID"].ToString();
                ViewState["degreeno"] = dr["DEGREENO"].ToString();
                ViewState["BRANCHNO"] = dr["BRANCHNO"].ToString();
                ViewState["batchno"] = Convert.ToInt32(dr["ADMBATCH"].ToString());
                //ViewState["semesterno"] = Convert.ToInt32(dr["SEMESTERNO"].ToString());
                ViewState["paytypeno"] = Convert.ToInt32(dr["PTYPE"].ToString());
                ViewState["RECIEPT_CODE"] = dr["RECIEPT_CODE"].ToString() == string.Empty ? string.Empty : dr["RECIEPT_CODE"].ToString();
                ViewState["SESSIONNO"] = dr["SESSIONNO"].ToString() == string.Empty ? string.Empty : dr["SESSIONNO"].ToString();
                ViewState["COLLEGE_ID"] = dr["COLLEGE_ID"].ToString();
                ViewState["RECON"] = dr["RECON"].ToString();



                //Offer Acceptance Bind listview 11-01-2022
                DataSet Acceptance = feeController.GetStudentInfoById_ForOnlinePayment(Convert.ToInt32(ViewState["ID_NO"].ToString()));
                if (Acceptance != null && Acceptance.Tables.Count > 0)
                {

                }


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

    private void ApplicantPersonalDetails()
    {
        //clearAll();
        try
        {

            //idno = Convert.ToInt32(Session["urlAppno"]);
            DataSet dsStudent1 = objnuc.GetStudApplicationDetails(Convert.ToInt32(ViewState["ID_NO"]));
            if (dsStudent1 != null && dsStudent1.Tables.Count > 0)
            {
                DataRow dr = dsStudent1.Tables[0].Rows[0];
                //string fullName = dr["STUDNAME"].ToString();
                //string[] names = fullName.Split(' ');
                //string name = names.First();
                //string lasName = names.Last();
                //lblStudName.Text = fullName;

                lblFirstNameP.Text = dr["FIRSTNAME"].ToString() == string.Empty ? string.Empty : dr["FIRSTNAME"].ToString();
                lblLastNameP.Text = dr["LASTNAME"].ToString() == string.Empty ? string.Empty : dr["LASTNAME"].ToString();
                lblInitialName.Text = dr["FULLNAME"].ToString() == string.Empty ? string.Empty : dr["FULLNAME"].ToString();
                lblEmailP.Text = dr["EMAIL"].ToString() == string.Empty ? string.Empty : dr["EMAIL"].ToString();
                lblMobileNoP.Text = dr["STUDENTMOBILE"].ToString() == string.Empty ? string.Empty : dr["STUDENTMOBILE"].ToString();
                lblNICNO.Text = dr["NIC"].ToString() == string.Empty ? string.Empty : dr["NIC"].ToString();
                lblPassPortNum.Text = dr["PASSPORTNO"].ToString() == string.Empty ? string.Empty : dr["PASSPORTNO"].ToString();
                lblSemesterP.Text = dr["SEMESTER_NAME"].ToString() == string.Empty ? string.Empty : dr["SEMESTER_NAME"].ToString();
                lblDOBName.Text = dr["DOB"].ToString() == string.Empty ? string.Empty : dr["DOB"].ToString();
                lblGender.Text = dr["SEX"].ToString() == string.Empty ? string.Empty : dr["SEX"].ToString();
                lblCitizenType.Text = dr["CITIZEN"].ToString() == string.Empty ? string.Empty : dr["CITIZEN"].ToString();
                lblLRHanded.Text = dr["LRHANDED"].ToString() == string.Empty ? string.Empty : dr["LRHANDED"].ToString();
                lblregnoIII.Text = dr["STUDENTID"].ToString() == string.Empty ? string.Empty : dr["STUDENTID"].ToString();
                lblEnrollNOIII.Text = dr["ENROLLNO"].ToString() == string.Empty ? string.Empty : dr["ENROLLNO"].ToString();
                lblIntakeName.Text = dr["BATCHNAME"].ToString() == string.Empty ? string.Empty : dr["BATCHNAME"].ToString();
                lblProgramName.Text = dr["PROGRAM"].ToString() == string.Empty ? string.Empty : dr["PROGRAM"].ToString();
                lblFacultyName.Text = dr["COLLEGE_NAME"].ToString() == string.Empty ? string.Empty : dr["COLLEGE_NAME"].ToString();
                //lblDegree.Text = dr["DEGREENAME"].ToString() == string.Empty ? string.Empty : dr["DEGREENAME"].ToString();
                lblCampus.Text = dr["CAMPUSNAME"].ToString() == string.Empty ? string.Empty : dr["CAMPUSNAME"].ToString();
                lblWeekdayWeekend.Text = dr["WEEKDAYSNAME"].ToString() == string.Empty ? string.Empty : dr["WEEKDAYSNAME"].ToString();
                lblAwardingName.Text = dr["AFFILIATED_LONGNAME"].ToString() == string.Empty ? string.Empty : dr["AFFILIATED_LONGNAME"].ToString();
                lblEnrollmentDate.Text = dr["ENROLLMENT_DATE"].ToString() == string.Empty ? string.Empty : dr["ENROLLMENT_DATE"].ToString();
                lblStdname.Text = dr["STUDNAME"].ToString() == string.Empty ? string.Empty : dr["STUDNAME"].ToString();
                //lblEmailP.Text = dr["STUDNAME"].ToString() == string.Empty ? string.Empty : dr["STUDNAME"].ToString();
                lblSliitEmail.Text = dr["COLLEGE_EMAIL"].ToString() == string.Empty ? string.Empty : dr["COLLEGE_EMAIL"].ToString();
                if (dr["PHOTO"].ToString() != "")
                {
                    imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dr["IDNO"].ToString() + "&type=student";
                }


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


            DataSet ds = objnuc.GetAddressDetailsErp(Convert.ToInt32(ViewState["ID_NO"]));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lblAddressName.Text = ds.Tables[0].Rows[0]["PADDRESS"].ToString();
                lblProvince.Text = ds.Tables[0].Rows[0]["STATENAME"].ToString();
                lblCountry.Text = ds.Tables[0].Rows[0]["COUNTRYNAME"].ToString();
                //objCommon.FillDropDownList(ddlPTahsil, "ACD_DISTRICT", "DISTINCT DISTRICTNO", "DISTRICTNAME", "STATENO=" + ddlPermanentState.SelectedValue, "DISTRICTNO");
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

    //protected void FillDropDownList()
    //{
    //    NewUser objNewUr = new NewUser();
    //    objNewUr.UserNo = 0;
    //    objNewUr.Command_type = 4;
    //    objNewUr.StlQno = Convert.ToInt32(ddlALTypeUG.SelectedValue);
    //    DataSet ds = objnuc.GetAppliedUserEducationDetails(objNewUr);

    //    ddlALTypeUG.DataSource = ds.Tables[0];
    //    ddlALTypeUG.DataValueField = "ALTYPENO";
    //    ddlALTypeUG.DataTextField = "ALTYPENAME";
    //    ddlALTypeUG.DataBind();

    //    ddlStreamUG.DataSource = ds.Tables[1];
    //    ddlStreamUG.DataValueField = "STREAMNO";
    //    ddlStreamUG.DataTextField = "STREAMNAME";
    //    ddlStreamUG.DataBind();

    //    ddlALPassesUG.DataSource = ds.Tables[2];
    //    ddlALPassesUG.DataValueField = "QUALILEVELNO";
    //    ddlALPassesUG.DataTextField = "QUALILEVELNAME";
    //    ddlALPassesUG.DataBind();

    //    ddlOLType.DataSource = ds.Tables[0];
    //    ddlOLType.DataValueField = "ALTYPENO";
    //    ddlOLType.DataTextField = "ALTYPENAME";
    //    ddlOLType.DataBind();

    //    ddlolStream.DataSource = ds.Tables[1];
    //    ddlolStream.DataValueField = "STREAMNO";
    //    ddlolStream.DataTextField = "STREAMNAME";
    //    ddlolStream.DataBind();

    //    ddlTestUG.DataSource = ds.Tables[3];
    //    ddlTestUG.DataValueField = "CAMPUSNO";
    //    ddlTestUG.DataTextField = "CAMPUSNAME";
    //    ddlTestUG.DataBind();

    //    ddlUGApptitudeMode.DataSource = ds.Tables[4];
    //    ddlUGApptitudeMode.DataValueField = "SRNO";
    //    ddlUGApptitudeMode.DataTextField = "MEDIUM_NAME";
    //    ddlUGApptitudeMode.DataBind();
    //}
    protected void bindOfferAcceptance()
    {
        try
        {
            DataSet ds = null;
            ds = objdocContr.getOfferAcceptanceDetails(Convert.ToInt32(Session["urlAppno"]), Convert.ToInt32(1), Convert.ToInt32(0));
            //if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    lstProgramName.DataSource = ds.Tables[0];
            //    lstProgramName.DataBind();
            //    pnlProgramName.Visible = true;
            //}
            //else
            //{
            //    lstProgramName.DataSource = null;
            //    lstProgramName.DataBind();
            //}



            //if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    lvStudentFees.DataSource = ds.Tables[0];
            //    lvStudentFees.DataBind();
            //}
            //else
            //{
            //    lvStudentFees.DataSource = null;
            //    lvStudentFees.DataBind();
            //}



            //if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
            //{
            //    ddlcamous.Items.Clear();
            //    ddlcamous.Items.Insert(0, "Please Select");
            //    ddlcamous.DataSource = ds.Tables[2];
            //    ddlcamous.DataValueField = "CAMPUSNO";
            //    ddlcamous.DataTextField = "CAMPUSNAME";
            //    ddlcamous.DataBind();
            //}
        }
        catch (Exception ex)
        {

        }
    }
    protected void FillDropDownListEducation()
    {
        NewUser objNewUr = new NewUser();
        objNewUr.UserNo = Convert.ToInt32(Session["urlAppno"]);
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
        objNewUr.UserNo = Convert.ToInt32(Session["urlAppno"]);
        objNewUr.Command_type = 1;
        objNewUr.StlQno = 0;
        DataSet ds = objnuc.GetAppliedUserEducationDetails(objNewUr);
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["ForEdit"] = "edit";
            //lvExam.DataSource = ds;
            //lvExam.DataBind();
            ddlALTypeUG.SelectedValue = ds.Tables[0].Rows[0]["AL_TYPE"].ToString();
            ddlALTypeUG_SelectedIndexChanged(new object(), new EventArgs());

            ddlStreamUG.SelectedValue = ds.Tables[0].Rows[0]["STREAMNO"].ToString();
            ddlALPassesUG.SelectedValue = ds.Tables[0].Rows[0]["ATTEMPTNO"].ToString();
            ddlALPassesUG_SelectedIndexChanged(new object(), new EventArgs());

            ddlSubject1.SelectedValue = ds.Tables[0].Rows[0]["SUBJECT1"].ToString();

            ddlSubject1_SelectedIndexChanged(new object(), new EventArgs());
            ddlSubject2.SelectedValue = ds.Tables[0].Rows[0]["SUBJECT2"].ToString();

            ddlSubject2_SelectedIndexChanged(new object(), new EventArgs());
            ddlSubject3.SelectedValue = ds.Tables[0].Rows[0]["SUBJECT3"].ToString();

            ddlSubject3_SelectedIndexChanged(new object(), new EventArgs());
            ddlSubject4.SelectedValue = ds.Tables[0].Rows[0]["SUBJECT4"].ToString();


            ddlGrade1.SelectedValue = ds.Tables[0].Rows[0]["GRADE1"].ToString();
            ddlGrade2.SelectedValue = ds.Tables[0].Rows[0]["GRADE2"].ToString();
            ddlGrade3.SelectedValue = ds.Tables[0].Rows[0]["GRADE3"].ToString();
            ddlGrade4.SelectedValue = ds.Tables[0].Rows[0]["GRADE4"].ToString();
            //OL TYPE
            ddlOLType.SelectedValue = ds.Tables[0].Rows[0]["OLTYPE"].ToString();
            if (ddlOLType.SelectedIndex > 0)
            {
                ddlOLType_SelectedIndexChanged(new object(), new EventArgs());
            }
            ddlolStream.SelectedValue = ds.Tables[0].Rows[0]["OLSTREAMNO"].ToString();
            ddlolpass.SelectedValue = ds.Tables[0].Rows[0]["OLATTEMPTNO"].ToString();

            olddlsub1.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT1"].ToString();

            olddlsub1_SelectedIndexChanged(new object(), new EventArgs());
            olddlsubj2.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT2"].ToString();

            olddlsubj2_SelectedIndexChanged(new object(), new EventArgs());
            olddlsub3.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT3"].ToString();

            olddlsub3_SelectedIndexChanged(new object(), new EventArgs());
            olddlsub4.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT4"].ToString();


            olddlsub4_SelectedIndexChanged(new object(), new EventArgs());
            olddlsub5.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT5"].ToString();

            olddlsub5_SelectedIndexChanged(new object(), new EventArgs());
            olddlsub6.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT6"].ToString();

            olddlgrade1.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE1"].ToString();
            olddlgrade2.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE2"].ToString();
            olddlgrade3.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE3"].ToString();
            olddlgrade4.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE4"].ToString();
            olddlgrade5.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE5"].ToString();
            olddlgrade6.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE6"].ToString();

            //ddlStream.Enabled = false;

            //ddlApptituteCenter.SelectedValue = ds.Tables[0].Rows[0]["APTITUDE_CENTER_NO"].ToString();
            //ddlApptituteMedium.SelectedValue = ds.Tables[0].Rows[0]["APTITUDE_MEDIUM_NO"].ToString();
        }
        else
        {
            //lvExam.DataSource = null;
            //lvExam.DataBind();
        }



        //NewUser objNewUr = new NewUser();
        //objNewUr.UserNo = Convert.ToInt32(Session["urlAppno"]);
        //objNewUr.Command_type = 1;
        //objNewUr.StlQno = 0;
        //DataSet ds = objnuc.GetAppliedUserEducationDetails(objNewUr);
        //if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //{
        //    ddlALPassesUG.SelectedValue = ds.Tables[0].Rows[0]["ATTEMPTNO"].ToString();
        //    ddlALTypeUG.SelectedValue = ds.Tables[0].Rows[0]["AL_TYPE"].ToString();
        //    ddlALTypeUG_SelectedIndexChanged(new object(), new EventArgs());
        //    ddlStreamUG.SelectedValue = ds.Tables[0].Rows[0]["STREAMNO"].ToString();
        //    ddlSubject1.SelectedValue = ds.Tables[0].Rows[0]["SUBJECT1"].ToString();

        //    ddlSubject1_SelectedIndexChanged(new object(), new EventArgs());
        //    ddlSubject2.SelectedValue = ds.Tables[0].Rows[0]["SUBJECT2"].ToString();

        //    ddlSubject2_SelectedIndexChanged(new object(), new EventArgs());
        //    ddlSubject3.SelectedValue = ds.Tables[0].Rows[0]["SUBJECT3"].ToString();

        //    ddlSubject3_SelectedIndexChanged(new object(), new EventArgs());
        //    ddlSubject4.SelectedValue = ds.Tables[0].Rows[0]["SUBJECT4"].ToString();

        //    ddlGrade1.SelectedValue = ds.Tables[0].Rows[0]["GRADE1"].ToString();
        //    ddlGrade2.SelectedValue = ds.Tables[0].Rows[0]["GRADE2"].ToString();
        //    ddlGrade3.SelectedValue = ds.Tables[0].Rows[0]["GRADE3"].ToString();
        //    ddlGrade4.SelectedValue = ds.Tables[0].Rows[0]["GRADE4"].ToString();

        //    ddlOLType.SelectedValue = ds.Tables[0].Rows[0]["OLTYPE"].ToString();
        //    if (ddlOLType.SelectedIndex > 0)
        //    {
        //        ddlOLType_SelectedIndexChanged(new object(), new EventArgs());
        //    }
        //    ddlolStream.SelectedValue = ds.Tables[0].Rows[0]["OLSTREAMNO"].ToString();
        //    ddlolpass.SelectedValue = ds.Tables[0].Rows[0]["OLATTEMPTNO"].ToString();

        //    olddlsub1.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT1"].ToString();

        //    olddlsub1_SelectedIndexChanged(new object(), new EventArgs());
        //    olddlsubj2.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT2"].ToString();

        //    olddlsubj2_SelectedIndexChanged(new object(), new EventArgs());
        //    olddlsub3.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT3"].ToString();

        //    olddlsub3_SelectedIndexChanged(new object(), new EventArgs());
        //    olddlsub4.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT4"].ToString();


        //    olddlsub4_SelectedIndexChanged(new object(), new EventArgs());
        //    olddlsub5.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT5"].ToString();

        //    olddlsub5_SelectedIndexChanged(new object(), new EventArgs());
        //    olddlsub6.SelectedValue = ds.Tables[0].Rows[0]["OLSUBJECT6"].ToString();

        //    olddlgrade1.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE1"].ToString();
        //    olddlgrade2.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE2"].ToString();
        //    olddlgrade3.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE3"].ToString();
        //    olddlgrade4.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE4"].ToString();
        //    olddlgrade5.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE5"].ToString();
        //    olddlgrade6.SelectedValue = ds.Tables[0].Rows[0]["OLGRADE6"].ToString();

        //}
    }
    protected void ddlALTypeUG_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlALTypeUG.SelectedIndex > 0)
            {
                NewUser objNewUr = new NewUser();
                objNewUr.UserNo = Convert.ToInt32(Session["urlAppno"]);
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

                    ListItem itemToRemove = ddlSubject1.Items.FindByText(ddlSubject1.SelectedItem.Text);
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

                    ListItem itemToRemove = ddlSubject1.Items.FindByText(ddlSubject1.SelectedItem.Text);
                    ListItem itemToRemoveSubject2 = ddlSubject2.Items.FindByText(ddlSubject2.SelectedItem.Text);
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

                    ListItem itemToRemove = ddlSubject1.Items.FindByText(ddlSubject1.SelectedItem.Text);
                    ListItem itemToRemoveSubject2 = ddlSubject2.Items.FindByText(ddlSubject2.SelectedItem.Text);
                    ListItem itemToRemoveSubject3 = ddlSubject3.Items.FindByText(ddlSubject3.SelectedItem.Text);
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
                objNewUr.UserNo = Convert.ToInt32(Session["urlAppno"]);
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

                    ListItem itemToRemove = olddlsub1.Items.FindByValue(olddlsub1.SelectedValue);
                    ListItem itemToRemoveSubject2 = olddlsub1.Items.FindByValue(olddlsubj2.SelectedValue);
                    ListItem itemToRemoveSubject3 = olddlsub3.Items.FindByValue(olddlsub3.SelectedValue);
                    ListItem itemToRemoveSubject4 = olddlsub4.Items.FindByValue(olddlsub4.SelectedValue);
                    ListItem itemToRemoveSubject5 = olddlsub5.Items.FindByValue(olddlsub5.SelectedValue);
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

                    ListItem itemToRemove = olddlsub1.Items.FindByValue(olddlsub1.SelectedValue);
                    ListItem itemToRemoveSubject2 = olddlsub1.Items.FindByValue(olddlsubj2.SelectedValue);
                    ListItem itemToRemoveSubject3 = olddlsub3.Items.FindByValue(olddlsub3.SelectedValue);
                    ListItem itemToRemoveSubject4 = olddlsub3.Items.FindByValue(olddlsub4.SelectedValue);
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

                    ListItem itemToRemove = olddlsub1.Items.FindByValue(olddlsub1.SelectedValue);
                    ListItem itemToRemoveSubject2 = olddlsub1.Items.FindByValue(olddlsubj2.SelectedValue);
                    ListItem itemToRemoveSubject3 = olddlsub3.Items.FindByValue(olddlsub3.SelectedValue);
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

                    ListItem itemToRemove = olddlsub1.Items.FindByValue(olddlsub1.SelectedValue);
                    ListItem itemToRemoveSubject2 = olddlsubj2.Items.FindByValue(olddlsubj2.SelectedValue);
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

                    ListItem itemToRemove = olddlsub1.Items.FindByValue(olddlsub1.SelectedValue);
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
            if (ddlALPassesUG.SelectedValue == "1")
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


                //ddlSubject1.Enabled = true;
                //ddlSubject2.Enabled = false;
                //ddlSubject3.Enabled = false;
                //ddlSubject4.Enabled = false;

                //ddlGrade1.Enabled = false;
                //ddlGrade2.Enabled = false;
                //ddlGrade3.Enabled = false;
                //ddlGrade4.Enabled = false;

            }

            else
            {
                //ddlALTypeUG_SelectedIndexChanged(new object(), new EventArgs());
                //ddlSubject2.Enabled = true;
                //ddlSubject3.Enabled = true;
                //ddlSubject4.Enabled = true;

                //ddlGrade1.Enabled = true;
                //ddlGrade2.Enabled = true;
                //ddlGrade3.Enabled = true;
                //ddlGrade4.Enabled = true;
                //if (ddlSubject1.SelectedIndex == 0)
                // {
                //     objCommon.DisplayMessage(updEdcutationalDetails, "Please Select Subject 1.", this.Page);
                //     return;
                // }
                // else if (ddlGrade1.SelectedIndex == 0)
                // {
                //     objCommon.DisplayMessage(updEdcutationalDetails, "Please Select Grade 1.", this.Page);
                //     return;
                // }
                // else if (ddlSubject2.SelectedIndex == 0)
                // {
                //     objCommon.DisplayMessage(updEdcutationalDetails, "Please Select Subject 2.", this.Page);
                //     return;
                // }
                // else if (ddlGrade2.SelectedIndex == 0)
                // {
                //     objCommon.DisplayMessage(updEdcutationalDetails, "Please Select Grade 2.", this.Page);
                //     return;
                // }
                // else if (ddlSubject3.SelectedIndex == 0)
                // {
                //     objCommon.DisplayMessage(updEdcutationalDetails, "Please Select Subject 3.", this.Page);
                //     return;
                // }
                // else if (ddlGrade3.SelectedIndex == 0)
                // {
                //     objCommon.DisplayMessage(updEdcutationalDetails, "Please Select Grade 3.", this.Page);
                //     return;
                // }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void CheckStudyLevel()
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("ACD_USER_REGISTRATION", "DISTINCT UGPGOT", "ADMBATCH", "USERNO=" + Convert.ToInt32(Session["urlAppno"]), "");
        if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToString(ds.Tables[0].Rows[0]["UGPGOT"]) == "1" || Convert.ToString(ds.Tables[0].Rows[0]["UGPGOT"]) == "3")
            {

                divUgEducationUG.Visible = true;
                //divEducationPG.Visible = false;
                //DivEducationDEtailsPDP.Visible = false;
                //divEducationDetailsPHD.Visible = false;
                //FillDropDownList();
                //BindListViewData();

                FillDropDownListEducation();
                BindListViewDataEducation();
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["UGPGOT"]) == "2")
            {
                divUgEducationUG.Visible = false;
                //divEducationPG.Visible = true;
                //DivEducationDEtailsPDP.Visible = false;
                //divEducationDetailsPHD.Visible = false;
                //BindlistView(Convert.ToInt32(ViewState["userno"]));
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["UGPGOT"]) == "4")
            {
                //divUgEducationUG.Visible = false;
                //divEducationPG.Visible = false;
                //DivEducationDEtailsPDP.Visible = true;
                //divEducationDetailsPHD.Visible = false;
                //FillDropDownListPDP();
                //BindListViewDataPDP();
                //return;

            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["UGPGOT"]) == "5")
            {
                //divUgEducationUG.Visible = false;
                //divEducationPG.Visible = false;
                //DivEducationDEtailsPDP.Visible = false;
                //divEducationDetailsPHD.Visible = true;
                // BindUserData();
            }
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

                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

                var Newblob = blobContainer.GetBlockBlobReference(ImageName);

                string filePath = directoryPath + "\\" + ImageName;


                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }

                Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);


                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"750px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
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
                ds = objdocContr.getOfferAcceptanceDetails(Convert.ToInt32(Session["urlAppno"]), Convert.ToInt32(2), Convert.ToInt32(ddlcamous.SelectedValue));
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
            if (ddlcamous.SelectedIndex > 0)
            {
                CustomStatus cs = CustomStatus.Others;
                cs = (CustomStatus)objdocContr.UpdateFinalUserConformation(Convert.ToInt32(Session["urlAppno"]), Convert.ToInt32(ddlcamous.SelectedValue), Convert.ToInt32(ddlweek.SelectedValue), Convert.ToInt32(Session["userno"]));
                if (cs == CustomStatus.RecordSaved)
                {
                    objCommon.DisplayMessage(this.Page, "Record Saved Successfully !!!", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Error !!!", this.Page);
                    return;
                }

                DataSet dsUserContact = null;
                UserController objUC = new UserController();
                dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", Convert.ToString(Session["urlAppno"]), Convert.ToInt32(Request.QueryString["pageno"]));

                if (dsUserContact.Tables[1] != null && dsUserContact.Tables[1].Rows.Count > 0)
                {
                    if (dsUserContact.Tables[3] != null && dsUserContact.Tables[3].Rows.Count > 0)
                    {
                        string Subject = dsUserContact.Tables[1].Rows[1]["SUBJECT"].ToString();
                        string message = "";
                        message = dsUserContact.Tables[1].Rows[1]["TEMPLATE"].ToString();

                        SmtpMail oMail = new SmtpMail("TryIt");

                        oMail.From = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]);

                        oMail.To = lblEmailID.Text;

                        oMail.Subject = Subject;

                        oMail.HtmlBody = message;

                        oMail.HtmlBody = oMail.HtmlBody.Replace("[USERFIRSTNAME]", dsUserContact.Tables[3].Rows[0]["STUDNAME"].ToString());
                        oMail.HtmlBody = oMail.HtmlBody.Replace("[PROGRAM]", dsUserContact.Tables[3].Rows[0]["DEGREENAME"].ToString());
                        oMail.HtmlBody = oMail.HtmlBody.Replace("[CAMPUS]", dsUserContact.Tables[3].Rows[0]["CAMPUSNAME"].ToString());
                        oMail.HtmlBody = oMail.HtmlBody.Replace("[WEEKDAY]", dsUserContact.Tables[3].Rows[0]["WEEKDAYSNAME"].ToString());
                        oMail.HtmlBody = oMail.HtmlBody.Replace("[Date]", dsUserContact.Tables[3].Rows[0]["COMMENCE_DATE"].ToString());

                        //SmtpServer oServer = new SmtpServer("smtp.live.com");
                        SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022

                        oServer.User = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]);
                        oServer.Password = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"]);

                        oServer.Port = 587;

                        oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                        EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();

                        oSmtp.SendMail(oServer, oMail);
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
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please Select Campus !!!", this.Page);
            }

            ShowConfirmationReport("Final_Confirmation_Report", "AdmisionConfirmSlip.rpt", Convert.ToInt32(ViewState["ID_NO"]));
            string FinalStatus = "";
            FinalStatus = objCommon.LookUp("ACD_STUDENT", "ISNULL(ENROLLNO,0)", "ISNULL(CAN,0) = 0 AND ISNULL(ADMCAN,0) = 0 AND IDNO=" + Convert.ToInt32(ViewState["ID_NO"]));
            if (FinalStatus.ToString() == "0" || FinalStatus.ToString() == string.Empty)
            {
                lnkSendEmail.Enabled = true;
                lnkGeneratereport.Visible = false;
                lnkPrintReport.Visible = false;
            }
            else
            {
                lnkSendEmail.Enabled = false;
                lnkGeneratereport.Visible = true;
                lnkPrintReport.Visible = true;
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void ShowConfirmationReport(string reportTitle, string rptFileName, int idno)
    {
        try
        {
            string colgname = "Report";// ViewState["College_Name"].ToString().Replace(" ", "_");

            string exporttype = "pdf";
            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];

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
            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
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

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkPrintReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowConfirmationReport("Final_Confirmation_Report", "AdmisionConfirmSlip.rpt", Convert.ToInt32(ViewState["ID_NO"]));
        }
        catch (Exception ex)
        {

        }
    }

    public void Result()
    {
        try
        {

            dsResult = objSC.RetrieveStudentCurrentResult(Convert.ToInt32(ViewState["ID_NO"].ToString()));
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                lvResult.DataSource = dsResult;
                lvResult.DataBind();
                lvResult.Visible = true;
            }
            else
            {
                lvResult.DataSource = null;
                lvResult.DataBind();
                // lvResult.Visible = false;
            }

        }
        catch (Exception ex)
        {
        }
    }


    public void SemesterRegistrationStatus()
    {
        try
        {
            dsregistration = objSC.RetrieveStudentCurrentRegDetailSem(Convert.ToInt32(ViewState["ID_NO"].ToString()));
            if (dsregistration.Tables[0].Rows.Count > 0)
            {

                lvReg.DataSource = dsregistration;
                lvReg.DataBind();
                lvReg.Visible = true;
            }
            else
            {

                lvReg.DataSource = null;
                lvReg.DataBind();
                //lvReg.Visible = false;
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

    public void FeesDetails()
    {
        try
        {
            dsFees = objSC.RetrieveStudentFeesDetailsNEW(Convert.ToInt32(ViewState["ID_NO"].ToString()));
            if (dsFees.Tables[0].Rows.Count > 0)
            {
                lvStudentFees.DataSource = dsFees;
                lvStudentFees.DataBind();
            }
            else
            {
                lvStudentFees.DataSource = null;
                lvStudentFees.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void clearAll()
    {
        lblFirstNameP.Text = string.Empty;
        lblLastNameP.Text = string.Empty;
        lblInitialName.Text = string.Empty;
        lblMobileNoP.Text = string.Empty;
        lblNICNO.Text = string.Empty;
        lblPassPortNum.Text = string.Empty;
        lblSemesterP.Text = string.Empty;
        lblDOBName.Text = string.Empty;
        lblEmailP.Text = string.Empty;
        lblGender.Text = string.Empty;
        lblCitizenType.Text = string.Empty;
        lblLRHanded.Text = string.Empty;
        imgPhoto.ImageUrl = null;
        lblAddressName.Text = string.Empty;
        lblProvince.Text = string.Empty;
        lblCountry.Text = string.Empty;
        lblDistrict.Text = string.Empty;

        lvDocument.DataSource = null;
        lvDocument.DataBind();

    }




    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

            DataSet ds = null;
            ds = objSC.GetOnlineSearchDetailsForStudent(txtSearch.Text, Convert.ToInt32(rdSearch.SelectedValue));

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            //if (txtSearch.Text != string.Empty)
            {

                int userno = 0;
                int idno = 0;

                lvStudent.DataSource = ds;
                lvStudent.DataBind();

                //idno = Convert.ToInt32((objCommon.LookUp("ACD_STUDENT A", "DISTINCT A.IDNO ", "A.REGNO='" + txtSearch.Text.Trim() + "'")));

                UG.Visible = false;
                DivShow.Visible = false;
                DivMod.Visible = false;
                DivSubmit.Visible = false;
                updfinalblock.Visible = false;
                DivMain.Visible = true;
                DivResult.Visible = true;
                DivPayment.Visible = true;

                lvStudent.Visible = true;

                ViewState["ID_NO"] = idno;
                Session["urlAppno"] = idno;
                ViewState["REGNO"] = userno;

                //ApplicantPersonalDetails();
                //BindAddressData();
                //binddocumentlist();
                //Result();
                //SemesterRegistrationStatus();
                //FeesDetails();


                //DataSet dsStudent = feeController.GetStudentInfoById_ForOnlinePayment(Convert.ToInt32(Session["idno"]));

                //if (dsStudent != null && dsStudent.Tables.Count > 0)
                //{
                //    DataRow dr = dsStudent.Tables[0].Rows[0];
                //    string fullName = dr["STUDNAME"].ToString();
                //    string[] names = fullName.Split(' ');
                //    string name = names.First();
                //    string lasName = names.Last();
                //    lblStudName.Text = fullName;
                //    lblSex.Text = dr["SEX"].ToString() == string.Empty ? string.Empty : dr["SEX"].ToString();
                //    lblRegNo1.Text = dr["REGNO"].ToString() == string.Empty ? string.Empty : dr["REGNO"].ToString();
                //    lblPaymentType.Text = dr["PAYTYPENAME"].ToString() == string.Empty ? string.Empty : dr["PAYTYPENAME"].ToString();
                //    lblYear.Text = dr["YEARNAME"].ToString() == string.Empty ? string.Empty : dr["YEARNAME"].ToString();
                //    lblSemester.Text = dr["SEMESTER_NAME"].ToString() == string.Empty ? string.Empty : dr["SEMESTER_NAME"].ToString();
                //    lblSemesterP.Text = dr["SEMESTER_NAME"].ToString() == string.Empty ? string.Empty : dr["SEMESTER_NAME"].ToString();
                //    lblBatch.Text = dr["BATCHNAME"].ToString() == string.Empty ? string.Empty : dr["BATCHNAME"].ToString();
                //    lblMobileNo.Text = dr["STUDENTMOBILE"].ToString() == string.Empty ? string.Empty : dr["STUDENTMOBILE"].ToString();
                //    lblEmailID.Text = dr["EMAILID"].ToString() == string.Empty ? string.Empty : dr["EMAILID"].ToString();


                //    ViewState["degreeno"] = dr["DEGREENO"].ToString();
                //    ViewState["BRANCHNO"] = dr["BRANCHNO"].ToString();
                //    ViewState["batchno"] = Convert.ToInt32(dr["ADMBATCH"].ToString());
                //    ViewState["semesterno"] = Convert.ToInt32(dr["SEMESTERNO"].ToString());
                //    ViewState["paytypeno"] = Convert.ToInt32(dr["PTYPE"].ToString());
                //    ViewState["RECIEPT_CODE"] = dr["RECIEPT_CODE"].ToString() == string.Empty ? string.Empty : dr["RECIEPT_CODE"].ToString();
                //    ViewState["SESSIONNO"] = dr["SESSIONNO"].ToString() == string.Empty ? string.Empty : dr["SESSIONNO"].ToString();
                //    ViewState["COLLEGE_ID"] = dr["COLLEGE_ID"].ToString();
                //    ViewState["RECON"] = dr["RECON"].ToString();

                //    DataSet Acceptance = feeController.GetStudentInfoById_ForOnlinePayment(Convert.ToInt32(ViewState["ID_NO"].ToString()));
                //}

                //else
                //{
                //    objCommon.DisplayUserMessage(this.Page, "Record Not Found.", this.Page);
                //    DivMain.Visible = false;
                //   // objCommon.DisplayUserMessage(this.Page, "Record Not Found.", this.Page);

                //}
                //ds.Dispose();
            }
            else
            {
                objCommon.DisplayUserMessage(this.Page, "Record Not Found.", this.Page);
                DivMain.Visible = false;
                lvStudent.Visible = false;
                // objCommon.DisplayUserMessage(this.Page, "Record Not Found.", this.Page);

            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearch.Text = "";
            rdSearch.SelectedValue = null;
            lvStudent.Visible = false;
        }
        catch (Exception ex)
        {

        }
    }



    protected void lnkUsername_Click(object sender, EventArgs e)
    {
        try
        {
            clearAll();


            lvStudent.DataSource = null;
            lvStudent.DataBind();
            txtSearch.Text = "";
            rdSearch.SelectedValue = null;
            LinkButton lnkUsername = sender as LinkButton;

            int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "enrollno = '" + lnkUsername.Text + "' or REGNO='" + lnkUsername.Text + "'"));

            UG.Visible = false;
            DivShow.Visible = false;
            DivMod.Visible = false;
            DivSubmit.Visible = false;
            updfinalblock.Visible = false;
            DivMain.Visible = true;
            DivResult.Visible = true;
            DivPayment.Visible = true;

            lvStudent.Visible = true;

            ViewState["ID_NO"] = IDNO;
            Session["urlAppno"] = IDNO;
            bindmodules();
            binddocumentlist();
            //paymentdetails();
            //ShowStudentDetails(); //
            ApplicantPersonalDetails();
            BindAddressData();
            bindOfferAcceptance();
            Result();
            ExternalResult();
            SemesterRegistrationStatus();
            FeesDetails();
            BindListView();


            CheckStudyLevel();
            CheckUserEligiblity();


        }
        catch (Exception ex)
        {
        }

    }

    public DataSet dsResult { get; set; }
    protected void lvDocument_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public DataSet dsregistration { get; set; }
    protected void lvResult_ItemDataBound(object sender, ListViewItemEventArgs e)   //FOR RESULT DETAILS 2 LIST VIEW
    {
        try
        {

            HiddenField SemesterNo = e.Item.FindControl("hdfSemester") as HiddenField;
            HiddenField SessionNo = e.Item.FindControl("hdfSession") as HiddenField;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {

                ListView lvResult = (ListView)e.Item.FindControl("lvDocs1");
                dsregistration = objSC.RetrieveStudentCurrentResultFORGRADE(Convert.ToInt32(ViewState["ID_NO"].ToString()), Convert.ToInt32(SemesterNo.Value), Convert.ToInt32(SessionNo.Value));
                //dsregistration = objSC.RetrieveStudentCurrentRegDetails(Convert.ToInt32(ViewState["ID_NO"].ToString()));
                if (dsregistration.Tables[0].Rows.Count > 0 && dsregistration != null)
                {
                    lvResult.DataSource = dsregistration.Tables[0];
                    lvResult.DataBind();
                }
                else
                {
                    lvResult.DataSource = null;
                    lvResult.DataBind();
                }
            }
        }
        catch
        {
        }

    }
    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)   //FOR SEMESTER REGISTRATION STATUS 2 LIST VIEW
    {
        try
        {
            ;
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblSemester = (Label)e.Item.FindControl("lblsemno");
                ListView lvReg = (ListView)e.Item.FindControl("lvDocs");
                //dsregistration = objSC.RetrieveStudentCurrentRegDetails(Convert.ToInt32(ViewState["ID_NO"].ToString()));
                string SP_Name2 = "PKG_ACAD_GET_REGISTRATION_DETAILS_BY_ID";
                string SP_Parameters2 = "@P_IDNO,@P_SEMESTERNO";
                string Call_Values2 = "" + Convert.ToInt32(ViewState["ID_NO"].ToString()) + "," + Convert.ToInt32(lblSemester.Text) + "";
                DataSet dsregistration = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
                if (dsregistration.Tables[0].Rows.Count > 0 && dsregistration != null)
                {
                    lvReg.DataSource = dsregistration.Tables[0];
                    lvReg.DataBind();
                    //lvReg.Visible = false;
                }
                else
                {
                    lvReg.DataSource = null;
                    lvReg.DataBind();
                }
            }
        }
        catch { }

    }

    public DataSet dsFees { get; set; }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        //int idno=0;
        int dcrno = 0;
        dcrno = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "ISNULL(DCR_NO,0)", "IDNO=" + Convert.ToInt32(ViewState["ID_NO"])));

        string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
        url += "pagetitle=" + "StudentReport";
        url += "&path=~,Reports,Academic," + "FeeCollectionReceipt.rpt";

        url += "&param=@P_IDNO=" + Convert.ToInt32(ViewState["ID_NO"]) + ",@P_DCRNO=" + Convert.ToInt32(dcrno);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "controlJSScript", sb.ToString(), true);

    }
    public DataSet ds { get; set; }
    protected void lvStudentRecords_DataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            StudentController studCont = new StudentController();
            ;
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {

                Label lblSemester = (Label)e.Item.FindControl("lblSemester");
                Label lblsession = (Label)e.Item.FindControl("lblsessionno");
                Label lbldegree = (Label)e.Item.FindControl("lbldegree");
                Label lblbranch = (Label)e.Item.FindControl("lblbranch");

                ListView lvReg = (ListView)e.Item.FindControl("lvStudentTimeTable");
                ds = studCont.GetStudentExamAdmitCard(Convert.ToInt32(lblsession.Text), Convert.ToInt32(lbldegree.Text), Convert.ToInt32(lblbranch.Text), Convert.ToInt32(lblSemester.Text), Convert.ToInt32(ViewState["ID_NO"]));
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lvReg.DataSource = ds;
                        lvReg.DataBind();
                    }
                    else
                    {
                        //objCommon.DisplayUserMessage(updexamDetails, "Exam Time Table Not Created Yet. Please Create Exam Time Table!", this.Page);
                    }
                }
                else
                {
                    //objCommon.DisplayUserMessage(updexamDetails, "Record Not Found!!", this.Page);
                    lvReg.DataSource = null;
                    lvReg.DataBind();
                }
            }
        }
        catch { }
    }
    public DataSet dsresult { get; set; }
    protected void lvDocs1_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField SemesterNo = e.Item.FindControl("hdfSemester") as HiddenField;
                HiddenField SessionNo = e.Item.FindControl("hdfSession") as HiddenField;
                HiddenField courseno = e.Item.FindControl("hdfcourseno") as HiddenField;
                ListView lvResult1 = (ListView)e.Item.FindControl("lvdetailsresult");
                //dsresult = objSC.RetrieveStudentCurrentResultFORGRADE(Convert.ToInt32(ViewState["ID_NO"].ToString()), Convert.ToInt32(6), Convert.ToInt32(52));
                dsresult = objSC.Couresewiseresult(Convert.ToInt32(ViewState["ID_NO"]), Convert.ToInt32(SemesterNo.Value), Convert.ToInt32(SessionNo.Value), Convert.ToInt32(courseno.Value));
                if (dsresult.Tables[0].Rows.Count > 0 && dsresult != null)
                {
                    lvResult1.DataSource = dsresult;
                    lvResult1.DataBind();
                }
                else
                {
                    lvResult1.DataSource = null;
                    lvResult1.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void ExternalResult()
    {
        DataSet ds = null;
        ds = objSC.RetrieveStudentCurrentResultFORGRADEEX(Convert.ToInt32(ViewState["ID_NO"].ToString()), Convert.ToInt32(ViewState["SEMESTERNO"]), Convert.ToInt32(ViewState["SESSIONNO"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            Lvexternalresult.DataSource = ds;
            Lvexternalresult.DataBind();
        }
        else
        {
            Lvexternalresult.DataSource = null;
            Lvexternalresult.DataBind();
        }
    }

    protected void Lvexternalresult_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField SemesterNo = e.Item.FindControl("hdfSemester") as HiddenField;
                HiddenField SessionNo = e.Item.FindControl("hdfSession") as HiddenField;
                HiddenField courseno = e.Item.FindControl("hdfcourseno") as HiddenField;
                ListView lvResult1 = (ListView)e.Item.FindControl("Lvexternal");
                //dsresult = objSC.RetrieveStudentCurrentResultFORGRADE(Convert.ToInt32(ViewState["ID_NO"].ToString()), Convert.ToInt32(6), Convert.ToInt32(52));
                dsresult = objSC.CouresewiseresultExternal(Convert.ToInt32(ViewState["ID_NO"]), Convert.ToInt32(SemesterNo.Value), Convert.ToInt32(SessionNo.Value), Convert.ToInt32(courseno.Value));
                if (dsresult.Tables[0].Rows.Count > 0 && dsresult != null)
                {
                    lvResult1.DataSource = dsresult;
                    lvResult1.DataBind();
                }
                else
                {
                    lvResult1.DataSource = null;
                    lvResult1.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void Lvexternal_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField SemesterNo = e.Item.FindControl("hdfSemester") as HiddenField;
                HiddenField SessionNo = e.Item.FindControl("hdfSession") as HiddenField;
                HiddenField courseno = e.Item.FindControl("hdfcourseno") as HiddenField;
                ListView lvResult1 = (ListView)e.Item.FindControl("lvdetailsresult");
                //dsresult = objSC.RetrieveStudentCurrentResultFORGRADE(Convert.ToInt32(ViewState["ID_NO"].ToString()), Convert.ToInt32(6), Convert.ToInt32(52));
                dsresult = objSC.Couresewiseresult(Convert.ToInt32(ViewState["ID_NO"]), Convert.ToInt32(SemesterNo.Value), Convert.ToInt32(SessionNo.Value), Convert.ToInt32(courseno.Value));
                if (dsresult.Tables[0].Rows.Count > 0 && dsresult != null)
                {
                    lvResult1.DataSource = dsresult;
                    lvResult1.DataBind();
                }
                else
                {
                    lvResult1.DataSource = null;
                    lvResult1.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
}
    


   
    