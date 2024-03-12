using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Text;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using EASendMail;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;
using System.IO;
using Microsoft.WindowsAzure.Storage;



public partial class ACADEMIC_PaymentReconcilation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MappingController objmp = new MappingController();
    UserController user = new UserController();

    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
    string blob_ContainerNameAdd = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameAdd"].ToString();
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
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {

                //Page Authorization
                CheckPageAuthorization();
                objCommon.FillDropDownList(ddlfacultytwo, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_ID");
                objCommon.FillDropDownList(ddlstudytwo, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "");
                objCommon.FillDropDownList(ddlfaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_ID");
                objCommon.FillDropDownList(ddlintake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                objCommon.FillDropDownList(ddlstudylevel, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "");
                objCommon.FillDropDownList(ddlintaketwo, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");

                objCommon.FillDropDownList(ddlintakeregistration, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                objCommon.FillDropDownList(ddlintakeregtwo, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                objCommon.FillDropDownList(ddlstudylevelsem, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "");
                objCommon.FillDropDownList(ddlintakethree, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");
                //objCommon.FillDropDownList(ddlintakethree, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                objCommon.FillDropDownList(ddlBank, "ACD_BANK", "DISTINCT BANKNO", "BANKNAME", "BANKNO>0", "BANKNO");
                objCommon.FillDropDownList(ddlBankEnrollnment, "ACD_BANK", "DISTINCT BANKNO", "BANKNAME", "BANKNO>0", "BANKNO");
                objCommon.FillDropDownList(ddlBankHigherSem, "ACD_BANK", "DISTINCT BANKNO", "BANKNAME", "BANKNO>0", "BANKNO");
                objCommon.FillDropDownList(ddlbankroyal, "ACD_BANK", "DISTINCT BANKNO", "BANKNAME", "BANKNO>0", "BANKNO");
                objCommon.FillDropDownList(ddlintaketworoyalty, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                objCommon.FillDropDownList(ddlintakeroyalty, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                objCommon.FillDropDownList(ddlstyduroyalty, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION=5", "");
                objCommon.FillDropDownList(ddlLoanSchemeStudyLevel, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "");
                objCommon.FillDropDownList(ddlLoanSchemeIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                objCommon.FillListBox(ddlProgram, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_DEGREE D ON A.DEGREENO = D.DEGREENO INNER JOIN ACD_BRANCH B ON A.BRANCHNO = B.BRANCHNO", "DISTINCT (CONVERT(VARCHAR,A.DEGREENO) + ',' + CONVERT(VARCHAR,A.BRANCHNO))", "(D.DEGREENAME + ' - ' + B.LONGNAME)", "", "");
                Page.Title = Session["coll_name"].ToString();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                // ImageViewer.ImageUrl = "~/showimage.aspx?id=" + 36 + "&type=ONLINEADMISSIONCOPY";
                //image.ImageUrl = "~/showimage.aspx?id=" + Session["userno"] + "&type=ONLINEADMISSIONCOPY";
                hdfImagePath.Value = null;
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
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
                Response.Redirect("~/notauthorized.aspx?page=CourseWise_Registration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CourseWise_Registration.aspx");
        }
    }
    private void BindApplicationCount()
    {
        DataSet ds = objmp.GetApplicationCount();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            Panel2.Visible = true;
            LvApplicationCount.DataSource = ds;
            LvApplicationCount.DataBind();

        }
        else
        {
            //objCommon.DisplayMessage(this.updonlineadm, "Record Not Found", this.Page);
            Panel2.Visible = false;
            LvApplicationCount.DataSource = null;
            LvApplicationCount.DataBind();

        }
    }
    private void BindEnrollmentCount()
    {
        DataSet ds = objmp.GetEnrollmentCount();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            Panel3.Visible = true;
            LvEnrollmentCount.DataSource = ds;
            LvEnrollmentCount.DataBind();

        }
        else
        {
            //objCommon.DisplayMessage(this.updonlineadm, "Record Not Found", this.Page);
            Panel3.Visible = false;
            LvEnrollmentCount.DataSource = null;
            LvEnrollmentCount.DataBind();

        }
    }

    //added by ekansh
    private void BindAddmissonCount()
    {
        DataSet ds = objmp.GetAddmissionCount();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            Panel3.Visible = true;
            LvEnrollmentCount.DataSource = ds;
            LvEnrollmentCount.DataBind();

        }
        else
        {
            //objCommon.DisplayMessage(this.updonlineadm, "Record Not Found", this.Page);
            Panel3.Visible = false;
            LvEnrollmentCount.DataSource = null;
            LvEnrollmentCount.DataBind();

        }
    }
    private void BindRoyaltCount()
    {
        DataSet ds = objmp.GetRoyaltCount();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            Panelroyacout.Visible = true;
            Lvroyacount.DataSource = ds;
            Lvroyacount.DataBind();

        }
        else
        {
            //objCommon.DisplayMessage(this.updonlineadm, "Record Not Found", this.Page);
            Panelroyacout.Visible = false;
            Lvroyacount.DataSource = null;
            Lvroyacount.DataBind();

        }
    }
    private void BindSemesterCount()
    {
        DataSet ds = objmp.GetSemesterCount();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            Panel4.Visible = true;
            LvSemesterCount.DataSource = ds;
            LvSemesterCount.DataBind();

        }
        else
        {
            //objCommon.DisplayMessage(this.updonlineadm, "Record Not Found", this.Page);
            Panel4.Visible = false;
            LvSemesterCount.DataSource = null;
            LvSemesterCount.DataBind();

        }
    }
    protected void lkonlineadm_Click(object sender, EventArgs e)
    {
        divlkonlineadm.Attributes.Add("class", "active");
        divlkerpadm.Attributes.Remove("class");
        divlksemester.Attributes.Remove("class");
        divlkroyality.Attributes.Remove("class");
        DivsLoanScheme.Attributes.Remove("class");

        intakeoneroya.Visible = false;
        statusroya.Visible = false;
        bankroya.Visible = false;
        intaketworoya.Visible = false;
        divroyastudy.Visible = false;
        pnlreg.Visible = false;
        pnlregOffline.Visible = false;
        divLoanScheme.Visible = false;
        divroyalty.Visible = false;
        lvreg.DataSource = null;
        lvreg.DataBind();
        divonlineadm.Visible = true;
        diverpadm.Visible = false;
        divsemster.Visible = false;
        Panel3.Visible = false;
        LvEnrollmentCount.DataSource = null;
        LvEnrollmentCount.DataBind();
        Panel4.Visible = false;
        LvSemesterCount.DataSource = null;
        LvSemesterCount.DataBind();

    }
    protected void lkerpadm_Click(object sender, EventArgs e)
    {
        divlkerpadm.Attributes.Add("class", "active");
        divlkonlineadm.Attributes.Remove("class");
        divlksemester.Attributes.Remove("class");
        divlkroyality.Attributes.Remove("class");
        DivsLoanScheme.Attributes.Remove("class");

        lvonlineadm.DataSource = null;
        lvonlineadm.DataBind();


        intakeoneroya.Visible = false;
        statusroya.Visible = false;
        bankroya.Visible = false;
        intaketworoya.Visible = false;
        divroyastudy.Visible = false;
        ddlintake.SelectedIndex = 0;
        ddluserselection.SelectedIndex = 0;
        ddlBank.SelectedIndex = 0;
        pnlCourse.Visible = false;
        divroyalty.Visible = false;
        divonlineadm.Visible = false;
        divLoanScheme.Visible = false;
        diverpadm.Visible = true;
        divsemster.Visible = false;
        Panel2.Visible = false;
        LvApplicationCount.DataSource = null;
        LvApplicationCount.DataBind();
        Panel4.Visible = false;
        LvSemesterCount.DataSource = null;
        LvSemesterCount.DataBind();
        Selection.Visible = true;
        registration.Visible = true;
        semselection.Visible = true;
        electroyalty.Visible = true;

    }
    protected void rblSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblSelection.SelectedValue == "1")
        {
            userselection.Visible = true;
            divBank.Visible = true;
            offline.Visible = true;
            online.Visible = false;
            btnShow.Visible = true;
            btnCancel.Visible = true;
            btnSubmit.Visible = false;
            divstudy.Visible = true;
            btnshoworder.Visible = false;
            divLoanScheme.Visible = false;

            //divclg.Visible = true;
            BindApplicationCount();
            lvonlineadm.DataSource = null;
            lvonlineadm.DataBind();
            Panel3.Visible = false;
            LvEnrollmentCount.DataSource = null;
            LvEnrollmentCount.DataBind();
            Panel4.Visible = false;
            LvSemesterCount.DataSource = null;
            LvSemesterCount.DataBind();

        }
        else if (rblSelection.SelectedValue == "2")
        {
            divstudy.Visible = true;
            divBank.Visible = false;
            online.Visible = true;
            offline.Visible = false;
            userselection.Visible = false;
            btnshoworder.Visible = true;
            btnShow.Visible = false;
            btnCancel.Visible = true;
            //divclg.Visible = true;
            btnSubmit.Visible = false;
            divLoanScheme.Visible = false;
            lvonlineadm.DataSource = null;
            lvonlineadm.DataBind();
            Panel2.Visible = false;
            LvApplicationCount.DataSource = null;
            LvApplicationCount.DataBind();
            Panel3.Visible = false;
            LvEnrollmentCount.DataSource = null;
            LvEnrollmentCount.DataBind();
            Panel4.Visible = false;
            LvSemesterCount.DataSource = null;
            LvSemesterCount.DataBind();
        }
    }
    // Change in abhijeet Naik Sir 12-12-2023//
    private void bindorderidlist()
    {
        DataSet ds = objmp.GETORDERID(Convert.ToInt32(ddlintaketwo.SelectedValue), Convert.ToInt32(ddlstudylevel.SelectedValue), Convert.ToInt32(ddlfaculty.SelectedValue));
        btnSubmit.Visible = false;
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            pnlCourse.Visible = true;
            lvonlineadm.DataSource = ds;
            lvonlineadm.DataBind();
            btnSubmit.Visible = true;

        }
        else
        {
            objCommon.DisplayMessage(this.updonlineadm, "Record Not Found", this.Page);
            pnlCourse.Visible = false;
            lvonlineadm.DataSource = null;
            lvonlineadm.DataBind();
            btnSubmit.Visible = false;

        }
        foreach (ListViewDataItem dataitem in lvonlineadm.Items)
        {
            DropDownList dstatus = dataitem.FindControl("ddlstatus") as DropDownList;
            Label lbstatus = dataitem.FindControl("lblstatus") as Label;
            TextBox txtamt = dataitem.FindControl("txtamount") as TextBox;
            LinkButton lnk = dataitem.FindControl("lnkView") as LinkButton;
            dstatus.SelectedValue = lbstatus.Text;
            lnk.Visible = false;

            if (dstatus.SelectedValue == "1")
            {
                txtamt.Enabled = false;
            }

            else if (dstatus.SelectedValue == "2")
            {
                txtamt.Enabled = true;
            }
        }
    }
    private void BINDLISRVIEW()
    {
        DataSet ds = objmp.GETONLINEADM(Convert.ToInt32(ddlintake.SelectedValue), Convert.ToInt32(ddluserselection.SelectedValue), Convert.ToInt32(ddlstudylevel.SelectedValue), Convert.ToInt32(ddlBank.SelectedValue));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            pnlCourse.Visible = true;
            lvonlineadm.DataSource = ds;
            lvonlineadm.DataBind();
            hdnCapacity.Value = ds.Tables[1].Rows[0]["CAPACITY"].ToString();
            if (ddluserselection.SelectedValue == "2")
            {
                btnSubmit.Visible = true;

            }
            else
            {
                if ((Convert.ToInt32(Session["userno"].ToString()) == 32 || Convert.ToInt32(Session["userno"].ToString()) == 255 || Convert.ToInt32(Session["userno"].ToString()) == 467 || Convert.ToInt32(Session["userno"].ToString()) == 466 || Convert.ToInt32(Session["userno"].ToString()) == 468) && ddluserselection.SelectedValue == "3")
                {
                    btnSubmit.Visible = true;
                }
                else
                {
                    btnSubmit.Visible = false;
                }

            }

            DataSet ds_dropdown = objCommon.FillDropDown("ACD_BANK", "DISTINCT BANKNO", "BANK_SHORTNAME", "BANKNO>0", "BANKNO");
            foreach (ListViewDataItem dataitem in lvonlineadm.Items)
            {
                DropDownList dstatus = dataitem.FindControl("ddlstatus") as DropDownList;
                DropDownList ddlbank = dataitem.FindControl("ddlbank") as DropDownList;
                Label lblBankNo = dataitem.FindControl("lblbank") as Label;
                ddlbank.Items.Clear();
                ddlbank.Items.Add("Please Select");
                ddlbank.SelectedItem.Value = "0";

                ddlbank.DataSource = ds_dropdown.Tables[0];
                ddlbank.DataValueField = "BANKNO";
                ddlbank.DataTextField = "BANK_SHORTNAME";
                ddlbank.DataBind();
                if (lblBankNo.Text == "0" || lblBankNo.Text == "")
                {
                    ddlbank.SelectedValue = "0";
                }
                else
                {
                    ddlbank.SelectedValue = lblBankNo.Text.ToString();
                }
                //ddlbank.SelectedValue = lblBankNo.Text.ToString();
                Label lbstatus = dataitem.FindControl("lblstatus") as Label;
                TextBox txtamt = dataitem.FindControl("txtamount") as TextBox;
                LinkButton lnk = dataitem.FindControl("lnkView") as LinkButton;
                dstatus.SelectedValue = lbstatus.Text;
                lnk.Visible = true;
                if (dstatus.SelectedValue == "1")
                {
                    txtamt.Enabled = false;
                }

                else if (dstatus.SelectedValue == "2")
                {
                    txtamt.Enabled = true;
                }
            }
        }
        else
        {
            objCommon.DisplayMessage(this.updonlineadm, "Record Not Found", this.Page);
            pnlCourse.Visible = false;
            lvonlineadm.DataSource = null;
            lvonlineadm.DataBind();
            btnSubmit.Visible = false;

        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BINDLISRVIEW();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlfaculty.SelectedIndex = 0;
        ddlstudylevel.SelectedIndex = 0;
        ddlintake.SelectedIndex = 0;
        ddluserselection.SelectedIndex = 0;
        ddlintaketwo.SelectedIndex = 0;
        btnSubmit.Visible = false;
        lvonlineadm.DataSource = null;
        lvonlineadm.DataBind();
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
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            string img = lnk.CommandArgument.ToString();
            //string img = lnk.CommandName.ToString();
            string extension = Path.GetExtension(img.ToString());
            string FileName = lnk.CommandName.ToString();
            if (extension == ".pdf")
            {
                imageViewerContainer.Visible = false;
                irm1.Visible = true;
                ImageViewer.Visible = false;
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
                CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerNameAdd);

                var ImageName = img;
                if (img != null || img != "")
                {

                    DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerNameAdd, img);

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
                    ScriptManager.RegisterClientScriptBlock(this.updModelPopup, this.GetType(), "Popup", "$('#myModal22').modal('show')", true);
                    //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal(show);});", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> Swal.fire({html: 'Sorry, File not found !!!', icon: 'warning' });</script>", false);

                }
            }
            else
            {
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
                CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerNameAdd);

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
                    ImageViewer.ImageUrl = string.Format("data:image/png;base64," + Base64String);
                    ScriptManager.RegisterClientScriptBlock(this.updModelPopup, this.GetType(), "Popup", "$('#myModal22').modal('show')", true);
                    // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
                }

            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void EmailSmsWhatssppSend(int Page_No, string toSendAddre, string Name, string Matter, string UserName, string Degree, string Intake, string Paymode, string Amount, string date, string UserNo, string College_Code, string Dcr, string IDNO)
    {

        EmailSmsWhatsaap Email = new EmailSmsWhatsaap();
        DataSet ds = objCommon.FillDropDown("ACCESS_LINK", "isnull(EMAIL,0) EMAIL,isnull(SMS,0) as SMS,isnull(WHATSAAP,0) as WHATSAAP", "", "AL_NO=" + Convert.ToInt32(Request.QueryString["pageno"].ToString()), "");
        string Res = ""; string Message = "";
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["EMAIL"].ToString()) == 1)//For Email Send 
        {
            int PageNo = Page_No;
            //int PageNo = 33;
            UserController objUC = new UserController();
            DataSet dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", "0", PageNo);
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
                msgsPara.Body = msgsPara.Body.Replace("[UA_FULLNAME]", Name.ToString());
                msgsPara.Body = msgsPara.Body.Replace("[PARAM_USERNO]", UserNo.ToString()); 

                MemoryStream Attachment = null; string AttachmentName = "FeeReceipt.pdf";
                if (IDNO.ToString() == string.Empty)
                {
                    AttachmentName = string.Empty;
                    //Attachment = ShowGeneralExportReportForMailForApplication("Reports,Academic,FeeReceiptApplication.rpt", "@P_USERNO=" + Convert.ToString(UserNo) + ",@P_COLLEGE_CODE=" + College_Code.ToString() + "");
                   // Attachment = ShowGeneralExportReportForMailForApplication("Reports,Academic,FeeCollectionHallticket.rpt", "@P_USERNO=" + Convert.ToString(UserName) + ",@P_COLLEGE_CODE=" + College_Code.ToString() + "");
                }
                else
                {
                    //Attachment = ShowGeneralExportReportForMail("Reports,Academic,FeeCollectionReceiptDownPay.rpt", "@P_DCRNO=" + Convert.ToString(Dcr) + ",@P_USERNO=" + UserNo.ToString() + ",@P_COLLEGE_CODE=" + College_Code.ToString() + "");
                    Attachment = ShowGeneralExportReportForMail("Reports,Academic,FeeReceiptEnrollnment.rpt", "@P_DCRNO=" + Convert.ToString(Dcr) + ",@P_IDNO=" + IDNO.ToString() + ",@P_COLLEGE_CODE=" + College_Code.ToString() + "");
                }
                Task<string> task = Email.Execute(msgsPara.Body, toSendAddress, Subject, Attachment, AttachmentName.ToString());
                Res = task.Result;
               
            }
        }
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["SMS"].ToString()) == 1) 
        {
            
        }
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["WHATSAAP"].ToString()) == 1)
        {
            
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string subject = "";
            int ua_no = Convert.ToInt32(Session["userno"]);
            DataSet dsUserContact = null;
            string message = "";
            CustomStatus cs = 0;
            int Count = 0;
            if (hdnCapacity.Value == "0")
            {
                objCommon.DisplayMessage(this.updonlineadm, "Exam Capacity Full or Not Define.", this.Page);
                return;
            }

            foreach (ListViewDataItem dataitem in lvonlineadm.Items)
            {
                int userno = 0;
                string matter = "";
                string orderid = string.Empty;
                string amount = string.Empty;
                string date = string.Empty;
                string STATUS = string.Empty;
                string REMARK = string.Empty;
                string EMAIL = "";
                string USERNO = "";
                string NAME = "";
                string paymode = "";
                string intake = "";
                string degree = "";

                CheckBox chkBox = dataitem.FindControl("chkadm") as CheckBox;
                TextBox torderid = dataitem.FindControl("txtorder") as TextBox;
                TextBox tamount = dataitem.FindControl("txtamount") as TextBox;
                TextBox tdate = dataitem.FindControl("txtdate") as TextBox;
                DropDownList dstatus = dataitem.FindControl("ddlstatus") as DropDownList;
                DropDownList ddlBank = dataitem.FindControl("ddlbank") as DropDownList;
                TextBox treamrk = dataitem.FindControl("txtremark") as TextBox;
                Label lbemail = dataitem.FindControl("lblemail") as Label;
                Label lbregname = dataitem.FindControl("lblname") as Label;
                HiddenField hdfuser = dataitem.FindControl("hdnDocno") as HiddenField;
                Label lbusername = dataitem.FindControl("lblusername") as Label;
                HiddenField hdpaymod = dataitem.FindControl("hdfpaymod") as HiddenField;
                HiddenField hdintake = dataitem.FindControl("hdfintake") as HiddenField;
                HiddenField hddegree = dataitem.FindControl("hdfdegree") as HiddenField;
                //int intake=();
                if (chkBox.Checked == true)
                {
                    Count++;
                    userno = Convert.ToInt32(chkBox.ToolTip);
                    //orderid = torderid.Text;
                    amount = tamount.Text;
                    date = tdate.Text;
                    STATUS = dstatus.SelectedValue;
                    REMARK = treamrk.Text;
                    EMAIL = lbemail.Text;
                    NAME = lbregname.Text;
                    USERNO = lbusername.Text;
                    paymode = hdpaymod.Value;
                    intake = hdintake.Value;
                    degree = hddegree.Value;
                    if (dstatus.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.updonlineadm, "Please Select Status", this.Page);
                    }
                    else
                    {
                        
                        cs = (CustomStatus)objmp.insertonlineadmrecon(userno, amount, date, STATUS, REMARK, Convert.ToString(ddlBank.SelectedValue), Convert.ToInt32(Session["userno"]));
                        if (dstatus.SelectedValue == "1")
                        {
                            matter = "Approved.";

                        }
                        else if (dstatus.SelectedValue == "2")
                        {
                            matter = "Pending";
                        }
                        else if (dstatus.SelectedValue == "3")
                        {
                            matter = "Rejected";
                        }

                        if (dstatus.SelectedValue == "1")
                        {
                            EmailSmsWhatssppSend(Convert.ToInt32(1002), EMAIL, NAME.ToString(), matter.ToString(), lbusername.Text.ToString(), degree.ToString(), intake.ToString(), paymode.ToString(), amount.ToString(), date.ToString(), Convert.ToString(hdfuser.Value), Session["colcode"].ToString(), "", "");
                            chkBox.Checked = false;


                        }

                    }
                }
            }
            if (Count == 0)
            {
                objCommon.DisplayMessage(this.updonlineadm, "Please Select At least One checkbox", this.Page);
                return;
            }
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.updonlineadm, "Record saved successfully.", this.Page);
                BINDLISRVIEW();
            }
            else
            {
                objCommon.DisplayMessage(this.updonlineadm, "Server Error", this.Page);
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

    static async Task Execute(string message, string date, string amount, string paymode, string intake, string degree, string toEmailId, string subject, string sudname, string matter, string usernoo, string sendemail, string emailpass)
    {

        try
        {
            SmtpMail oMail = new SmtpMail("TryIt");

            oMail.From = sendemail;
            oMail.To = toEmailId;
            oMail.Subject = subject;
            oMail.HtmlBody = message;
            oMail.HtmlBody = oMail.HtmlBody.Replace("[UA_FULLNAME]", sudname.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[MATTER]", matter.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[Application ID]", usernoo.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[PROGRAM NAMES]", degree.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[INTAKE]", intake.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[Payment Mode]", paymode.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[Payment Amount]", amount.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[Payment Date]", date.ToString());
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

        }
    }
    protected void btnshoworder_Click(object sender, EventArgs e)
    {
        bindorderidlist();
    }
    /// <summary>
    /// tab 2 start
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rdioregistration_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdioregistration.SelectedValue == "1")
        {
            divlkonlineadm.Attributes.Remove("class");
            div1intake2.Visible = true;
            divBankEnrollment.Visible = true;
            div1paystatus2.Visible = true;
            div2intake2.Visible = false;
            btnshowregone.Visible = true;
            btnShowAddmissionProper.Visible = false;
            btncancelreg.Visible = true;
            btnsubreg.Visible = false;
            btnSubAddProper.Visible = false;
            divstudyleveltwo.Visible = true;
            divLoanScheme.Visible = false;
            btnshowregtwo.Visible = false;
            BindEnrollmentCount();
            // BindAddmissonCount();
            //divfacultytwo.Visible = true;
            lvreg.DataSource = null;
            lvreg.DataBind();
            Panel2.Visible = false;
            LvApplicationCount.DataSource = null;
            LvApplicationCount.DataBind();
            Panel4.Visible = false;
            LvSemesterCount.DataSource = null;
            LvSemesterCount.DataBind();
            pnlreg.Visible = false;
            pnlregOffline.Visible = false;

        }
        else if (rdioregistration.SelectedValue == "2")
        {
            divlkonlineadm.Attributes.Remove("class");
            div2intake2.Visible = true;
            divBankEnrollment.Visible = false;
            div1intake2.Visible = false;
            div1paystatus2.Visible = false;
            btnshowregtwo.Visible = true;
            divstudyleveltwo.Visible = true;
            btnshowregone.Visible = false;
            btnShowAddmissionProper.Visible = false;
            btncancelreg.Visible = true;
            btnsubreg.Visible = false;
            btnSubAddProper.Visible = false;
            btnSubAddProper.Visible = false;
            divstudyleveltwo.Visible = true;
            divLoanScheme.Visible = false;
            lvreg.DataSource = null;
            lvreg.DataBind();
            Panel2.Visible = false;
            LvApplicationCount.DataSource = null;
            LvApplicationCount.DataBind();
            Panel3.Visible = false;
            LvEnrollmentCount.DataSource = null;
            LvEnrollmentCount.DataBind();
            Panel4.Visible = false;
            LvSemesterCount.DataSource = null;
            LvSemesterCount.DataBind();
            pnlreg.Visible = false;
            pnlregOffline.Visible = false;
        }

    }

    private void BINDREGONELIST()
    {
        DataSet ds = objmp.GETREGLISTONE(Convert.ToInt32(ddlintakeregistration.SelectedValue), Convert.ToInt32(ddlpaystatustwo.SelectedValue), Convert.ToInt32(ddlstudytwo.SelectedValue), Convert.ToInt32(ddlBankEnrollnment.SelectedValue), 0, 1000, string.Empty);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            pnlreg.Visible = true;
            pnlregOffline.Visible = false;
            lvreg.DataSource = ds;
            lvreg.DataBind();
            ViewState["ds"] = ds.Tables[0];
         
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            ViewState["dcr"] = ds.Tables[0].Rows[0]["DCR_NO"].ToString();
            if (ddlpaystatustwo.SelectedValue == "2")
            {
                btnsubreg.Visible = true;
            }
            else
            {
                if ((Convert.ToInt32(Session["userno"].ToString()) == 32 || Convert.ToInt32(Session["userno"].ToString()) == 255 || Convert.ToInt32(Session["userno"].ToString()) == 467 || Convert.ToInt32(Session["userno"].ToString()) == 466 || Convert.ToInt32(Session["userno"].ToString()) == 468) && ddlpaystatustwo.SelectedValue == "3")
                {
                    btnsubreg.Visible = true;
                }
                else
                {
                    btnsubreg.Visible = false;
                }
            }

        }
        else
        {
            objCommon.DisplayMessage(this.upderpadm, "Record Not Found", this.Page);
            pnlreg.Visible = false;
            pnlregOffline.Visible = false;
            lvreg.DataSource = null;
            lvreg.DataBind();
            lvregOffline.DataSource = null;
            lvregOffline.DataBind();

            btnsubreg.Visible = false;

        }

        DataSet FillDropDown = null;
        FillDropDown = objCommon.FillDropDown("ACD_BANK", "DISTINCT BANKNO", "BANK_SHORTNAME", "BANKNO>0", "BANKNO");

        foreach (ListViewDataItem dataitem in lvreg.Items)
        {
            DropDownList dstatus = dataitem.FindControl("ddlregstatus") as DropDownList;
            DropDownList ddlbank = dataitem.FindControl("ddlbank") as DropDownList;
            Label lblBankNo = dataitem.FindControl("lblbank") as Label;

            ddlbank.Items.Clear();
            ddlbank.Items.Add("Please Select");
            ddlbank.SelectedItem.Value = "0";

            ddlbank.DataSource = FillDropDown;
            ddlbank.DataTextField = "BANK_SHORTNAME";
            ddlbank.DataValueField = "BANKNO";
            ddlbank.DataBind();

            if (lblBankNo.Text == "0" || lblBankNo.Text == "")
            {
                ddlbank.SelectedValue = "0";
            }
            else
            {
                ddlbank.SelectedValue = lblBankNo.Text.ToString();
            }
            Label lbstatus = dataitem.FindControl("lblregstatus") as Label;
            TextBox txtamt = dataitem.FindControl("txtregamount") as TextBox;
            LinkButton lnk = dataitem.FindControl("lnkreg") as LinkButton;
            dstatus.SelectedValue = lbstatus.Text;
            lnk.Visible = true;

            if (dstatus.SelectedValue == "1")
            {
                txtamt.Enabled = false;
            }

            else if (dstatus.SelectedValue == "2")
            {
                txtamt.Enabled = true;
            }
        }
    }
    protected void btnshowregone_Click(object sender, EventArgs e)
    {
        divlkonlineadm.Attributes.Remove("class");
        BINDREGONELIST();

    }
    protected void btnshowregtwo_Click(object sender, EventArgs e)
    {
        divlkonlineadm.Attributes.Remove("class");
        bindregtwolist();
        btnsubreg.Visible = false;
        btnSubAddProper.Visible = false;
    }
    protected void btnsubreg_Click(object sender, EventArgs e)
    {
        try
        {
            divlkonlineadm.Attributes.Remove("class");
            string subject = "";
            string matter = "";
            int ua_no = Convert.ToInt32(Session["userno"]);
            CustomStatus cs = 0;
            int Count = 0;
            foreach (ListViewDataItem dataitem in lvreg.Items)
            {
                int userno = 0;
                string orderid = string.Empty;
                string amount = string.Empty;
                string date = string.Empty;
                string STATUS = string.Empty;
                string REMARK = string.Empty;
                string EMAIL = "";
                string USERNO = "";
                string NAME = "";
                string paymode = "";
                string intake = "";
                string degree = "";
                CheckBox chkBox = dataitem.FindControl("chreg") as CheckBox;
                TextBox torderid = dataitem.FindControl("txtorder") as TextBox;
                TextBox tamount = dataitem.FindControl("txtregamount") as TextBox;
                TextBox tdate = dataitem.FindControl("txtregdate") as TextBox;
                DropDownList dstatus = dataitem.FindControl("ddlregstatus") as DropDownList;
                DropDownList ddlBank = dataitem.FindControl("ddlbank") as DropDownList;
                TextBox treamrk = dataitem.FindControl("txtregremark") as TextBox;
                Label lbemail = dataitem.FindControl("lblregemail") as Label;
                Label lbregname = dataitem.FindControl("lblregname") as Label;
                Label lblTempDcrNo = dataitem.FindControl("lblDcrTempNo") as Label;
                HiddenField hdfuser = dataitem.FindControl("hdnDocno") as HiddenField;
                Label lbusername = dataitem.FindControl("lblregusername") as Label;
                HiddenField hdpaymod = dataitem.FindControl("hdfpaymod") as HiddenField;
                HiddenField hdintake = dataitem.FindControl("hdfintake") as HiddenField;
                HiddenField hddegree = dataitem.FindControl("hdfdegree") as HiddenField;
                HiddenField hdfdcr = dataitem.FindControl("hdfdcr") as HiddenField;
                if (chkBox.Checked == true)
                {
                    Count++;
                    userno = Convert.ToInt32(chkBox.ToolTip);
                    amount = tamount.Text;
                    date = tdate.Text;
                    STATUS = dstatus.SelectedValue;
                    REMARK = treamrk.Text;
                    EMAIL = lbemail.Text;
                    NAME = lbregname.Text;
                    USERNO = lbusername.Text;
                    paymode = hdpaymod.Value;
                    intake = hdintake.Value;
                    degree = hddegree.Value;
                    if (dstatus.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.updonlineadm, "Please Select Status", this.Page);
                    }
                    else
                    {
                        cs = (CustomStatus)objmp.insertREG(userno, amount, date, STATUS, REMARK, Convert.ToInt32(lblTempDcrNo.Text), Convert.ToString(ddlBank.SelectedValue), Convert.ToInt32(Session["userno"]));
                        if (dstatus.SelectedValue == "1")
                        {
                            matter = "Approved.";
                        }
                        else if (dstatus.SelectedValue == "2")
                        {
                            matter = "Pending";
                        }
                        else if (dstatus.SelectedValue == "3")
                        {
                            matter = "Rejected";
                        }

                        DataSet dsUserContact = null;
                        string message = "";
                     
                        string dcr = objCommon.LookUp("ACD_DCR", "DCR_NO", "TEMP_DCR_NUMBER=" + Convert.ToString(lblTempDcrNo.Text));

                        if (dstatus.SelectedValue == "1")
                        {
                            EmailSmsWhatssppSend(2627, EMAIL, NAME.ToString(), matter.ToString(), lbusername.Text.ToString(), degree.ToString(), intake.ToString(), paymode.ToString(), amount.ToString(), date.ToString(), Convert.ToString(hdfuser.Value), Session["colcode"].ToString(), Convert.ToString(dcr), Convert.ToString(chkBox.ToolTip));
                          

                            chkBox.Checked = false;


                        }
                    }
                }

            }
            if (Count == 0)
            {
                objCommon.DisplayMessage(this.upderpadm, "Please Select At least One checkbox", this.Page);
                return;
            }
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.upderpadm, "Record saved successfully.", this.Page);
                BINDREGONELIST();
            }
            else
            {
                objCommon.DisplayMessage(this.upderpadm, "Server Error", this.Page);
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
    private void bindregtwolist()
    {
        DataSet ds = objmp.getreglisttwo(Convert.ToInt32(ddlintakeregtwo.SelectedValue), Convert.ToInt32(ddlstudytwo.SelectedValue), Convert.ToInt32(ddlfacultytwo.SelectedValue));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            pnlreg.Visible = true;
            pnlregOffline.Visible = false;
            //lvreg.DataSource = ds;
            //lvreg.DataBind();

            lvregOffline.DataSource = ds;
            lvregOffline.DataBind();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            btnsubreg.Visible = false;
            btnSubAddProper.Visible = false;
        }
        else
        {
            objCommon.DisplayMessage(this.upderpadm, "Record Not Found", this.Page);
            pnlreg.Visible = false;
            pnlregOffline.Visible = false;
            lvreg.DataSource = null;
            lvreg.DataBind();
            lvregOffline.DataSource = null;
            lvregOffline.DataBind();
            btnsubreg.Visible = false;
            btnSubAddProper.Visible = false;
        }
        foreach (ListViewDataItem dataitem in lvreg.Items)
        {
            DropDownList dstatus = dataitem.FindControl("ddlregstatus") as DropDownList;
            Label lblstatus = dataitem.FindControl("lblregstatus") as Label;
            LinkButton lnk = dataitem.FindControl("lnkreg") as LinkButton;
            TextBox txtamt = dataitem.FindControl("txtregamount") as TextBox;
            dstatus.SelectedValue = lblstatus.Text;
            dstatus.SelectedValue = lblstatus.Text;
            lnk.Visible = false;

            if (dstatus.SelectedValue == "1")
            {
                txtamt.Enabled = false;
            }

            else if (dstatus.SelectedValue == "2")
            {
                txtamt.Enabled = true;
            }
        }
    }
    protected void btncancelreg_Click(object sender, EventArgs e)
    {
        divlkonlineadm.Attributes.Remove("class");
        ddlintakeregistration.SelectedIndex = 0;
        ddlpaystatustwo.SelectedIndex = 0;
        ddlstudytwo.SelectedIndex = 0;
        Panel3.Visible = false;
        ddlintakeregtwo.SelectedIndex = 0;
        btnsubreg.Visible = false;
        btnSubAddProper.Visible = false;
        lvreg.DataSource = null;
        rdioregistrationOnline.ClearSelection();
        rdioregistration.ClearSelection();
        lvreg.DataBind();
        lvregOffline.DataSource = null;
        lvregOffline.DataBind();
    }
    protected void lnkreg_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    imageViewerContainer.Visible = false;
        //    irm1.Visible = true;
        //    divlkonlineadm.Attributes.Remove("class");
        //    //foreach (ListViewDataItem dataitem in lvreg.Items)
        //    //{
        //    LinkButton lnkreg = (LinkButton)(sender);
        //    //CheckBox chk = dataitem.FindControl("chreg") as CheckBox;
        //    //HiddenField hdf = dataitem.FindControl("hdnDocno") as HiddenField;
        //    //int userno = Convert.ToInt32(chk.ToolTip);
        //    //if (Convert.ToInt32(lnkreg.ToolTip) == userno)
        //    //{
        //    string Url = string.Empty;
        //    string directoryPath = string.Empty;
        //    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
        //    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
        //    string directoryName = "~/DownloadImg" + "/";
        //    directoryPath = Server.MapPath(directoryName);

        //    if (!Directory.Exists(directoryPath.ToString()))
        //    {

        //        Directory.CreateDirectory(directoryPath.ToString());
        //    }
        //    CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerNameAdd);
        //    string img = lnkreg.CommandArgument.ToString();
        //    var ImageName = img;
        //    if (img.ToString() != string.Empty || img.ToString() != "")
        //    {
        //        string extension = Path.GetExtension(img.ToString());
        //        DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerNameAdd, img);
        //        if (extension == ".pdf")
        //        {
        //            imageViewerContainer.Visible = false;
        //            var Newblob = blobContainer.GetBlockBlobReference(ImageName);
        //            string filePath = directoryPath + "\\" + ImageName;
        //            if ((System.IO.File.Exists(filePath)))
        //            {
        //                System.IO.File.Delete(filePath);
        //            }
        //            ViewState["filePath_Show"] = filePath;
        //            Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);

        //            byte[] bytes = System.IO.File.ReadAllBytes(filePath);

        //            string Base64String = Convert.ToBase64String(bytes);
        //            Session["Base64String"] = Base64String;
        //            Session["Base64StringType"] = "application/pdf";
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
                  
        //        }
        //        else
        //        {
        //            imageViewerContainer.Visible = true;
        //            var Newblob = blobContainer.GetBlockBlobReference(ImageName);
        //            string filePath = directoryPath + "\\" + ImageName;
        //            if ((System.IO.File.Exists(filePath)))
        //            {
        //                System.IO.File.Delete(filePath);
        //            }
        //            ViewState["filePath_Show"] = filePath;
        //            Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);

        //            byte[] bytes = System.IO.File.ReadAllBytes(filePath);

        //            string Base64String = Convert.ToBase64String(bytes);
        //            Session["Base64String"] = Base64String;
        //            Session["Base64StringType"] = "image/png";
        //            imageViewerContainer.Visible = true;
        //            irm1.Visible = false;
        //            hdfImagePath.Value = "data:image/png;base64," + Base64String;
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
        //            hdfImagePath.Value = null;             

        //        }
        //    }
        //    else
        //    {
        //        objCommon.DisplayMessage(this.Page, "Sorry, File not found !!!", this.Page);
        //    }
          
        //}
        //catch (Exception ex)
        //{

        //}




        try
        {
            LinkButton lnkreg = (LinkButton)(sender);
            string img = lnkreg.CommandArgument.ToString();
            //string img = lnk.CommandName.ToString();
            string extension = Path.GetExtension(img.ToString());
            string FileName = lnkreg.CommandName.ToString();
            if (extension == ".pdf")
            {
                imageViewerContainer.Visible = false;
                irm1.Visible = true;
                ImageViewer.Visible = false;
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
                CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerNameAdd);

                var ImageName = img;
                if (img != null || img != "")
                {

                    DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerNameAdd, img);

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
                    ScriptManager.RegisterClientScriptBlock(this.updModelPopup, this.GetType(), "Popup", "$('#myModal22').modal('show')", true);
                    //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal(show);});", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> Swal.fire({html: 'Sorry, File not found !!!', icon: 'warning' });</script>", false);

                }
            }
            else
            {
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
                CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerNameAdd);

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
                    ImageViewer.ImageUrl = string.Format("data:image/png;base64," + Base64String);
                    ScriptManager.RegisterClientScriptBlock(this.updModelPopup, this.GetType(), "Popup", "$('#myModal22').modal('show')", true);
                    // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
                }

            }
        }
        catch (Exception ex)
        {

        }

    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListViewDataItem dataitem in lvonlineadm.Items)
        {
            DropDownList dstatus = dataitem.FindControl("ddlstatus") as DropDownList;
            Label lbstatus = dataitem.FindControl("lblstatus") as Label;
            TextBox txtamt = dataitem.FindControl("txtamount") as TextBox;


            if (dstatus.SelectedValue == "1")
            {
                txtamt.Enabled = false;
            }

            else if (dstatus.SelectedValue == "2")
            {
                txtamt.Enabled = true;
            }
        }
    }
    protected void ddlregstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListViewDataItem dataitem in lvreg.Items)
        {
            DropDownList dstatus = dataitem.FindControl("ddlregstatus") as DropDownList;
            Label lblstatus = dataitem.FindControl("lblregstatus") as Label;
            TextBox txtamt = dataitem.FindControl("txtregamount") as TextBox;

            if (dstatus.SelectedValue == "1")
            {
                txtamt.Enabled = false;
            }

            else if (dstatus.SelectedValue == "2")
            {
                txtamt.Enabled = true;
            }
        }
    }
    protected void lksemester_Click(object sender, EventArgs e)
    {
        divlkerpadm.Attributes.Remove("class");
        divlkonlineadm.Attributes.Remove("class");
        divlksemester.Attributes.Add("class", "active");
        divlkroyality.Attributes.Remove("class");
        DivsLoanScheme.Attributes.Remove("class");

        divsemster.Visible = true;
        divroyalty.Visible = false;
        divonlineadm.Visible = false;
        diverpadm.Visible = false;

        lvonlineadm.DataSource = null;
        lvonlineadm.DataBind();

        ddlintake.SelectedIndex = 0;
        ddluserselection.SelectedIndex = 0;
        pnlCourse.Visible = false;

        intakeoneroya.Visible = false;
        statusroya.Visible = false;
        bankroya.Visible = false;
        intaketworoya.Visible = false;
        divroyastudy.Visible = false;
        divLoanScheme.Visible = false;
        pnlreg.Visible = false;
        pnlregOffline.Visible = false;

        lvreg.DataSource = null;
        lvreg.DataBind();
        lvregOffline.DataSource = null;
        lvregOffline.DataBind();
        Panel2.Visible = false;
        LvApplicationCount.DataSource = null;
        LvApplicationCount.DataBind();
        Panel3.Visible = false;
        LvEnrollmentCount.DataSource = null;
        LvEnrollmentCount.DataBind();
        Selection.Visible = true;
        registration.Visible = true;
        semselection.Visible = true;
        electroyalty.Visible = true;
    }
    private void Clearsem()
    {
        ddlstudylevelsem.SelectedIndex = 0;
        ddlstatus.SelectedIndex = 0;
        ddlsemester.SelectedIndex = 0;
        ddlintakethree.SelectedIndex = 0;
        //ddlProgram.SelectedIndex = 0;
        Panel1.Visible = false;
        lvsemester.DataSource = null;
        lvsemester.DataBind();
        btnsubsem.Visible = false;
        ddlProgram.ClearSelection();
        divlkonlineadm.Attributes.Remove("class");
    }
    protected void btncancelsem_Click(object sender, EventArgs e)
    {
        Clearsem();
    }
    private void bindsemdataoffline()
    {
        string Program = string.Empty; string Branchno = string.Empty;
        divlkonlineadm.Attributes.Remove("class");
        foreach (ListItem items in ddlProgram.Items)
        {
            if (items.Selected == true)
            {
                Program += Convert.ToString(items.Value.Split(',')[0]) + ',';
                Branchno += Convert.ToString(items.Value.Split(',')[1]) + ','; ;
            }
        }
        ViewState["DYNAMIC_DATASET"] = null;
        DataSet ds = objmp.GETSEMESTERDATAOFF(Convert.ToInt32(ddlintakethree.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlstudylevelsem.SelectedValue), Convert.ToInt32(ddlstatus.SelectedValue), Convert.ToInt32(ddlBankHigherSem.SelectedValue), Program, Branchno);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            divlkonlineadm.Attributes.Remove("class");
            Panel1.Visible = true;
            lvsemester.DataSource = ds;
            lvsemester.DataBind();
            btnsubsem.Visible = true;
            ViewState["DYNAMIC_DATASET"] = ds.Tables[0];

            //Added By Abhijit Naik 11-12-2023
            //ViewState["ds1"] = ds.Tables[0];
            //Control div3 = lvsemester.FindControl("div3");
            //DataPager datapagerSem = div3.FindControl("datapagerSem") as DataPager;
            //Label lblTotalRecord = datapagerSem.Controls[0].FindControl("lbltotalcountSem") as Label;
            //lblTotalRecord.Text = ds.Tables[0].Rows.Count.ToString();

        }
        else
        {
            objCommon.DisplayMessage(this.updsemster, "Record Not Found", this.Page);
            Panel1.Visible = false;
            lvsemester.DataSource = null;
            lvsemester.DataBind();
            btnsubsem.Visible = false;
            Clearsem();

        }
        DataSet ds_dropdown = objCommon.FillDropDown("ACD_BANK", "DISTINCT BANKNO", "BANK_SHORTNAME", "BANKNO>0", "BANKNO");
        foreach (ListViewDataItem dataitem in lvsemester.Items)
        {
            DropDownList dstatus = dataitem.FindControl("ddlstatussem") as DropDownList;
            DropDownList ddlbank = dataitem.FindControl("ddlbank") as DropDownList;
            Label lblBankNo = dataitem.FindControl("lblsembank") as Label;
            ddlbank.Items.Clear();
            ddlbank.Items.Add("Please Select");
            ddlbank.SelectedItem.Value = "0";

            ddlbank.DataSource = ds_dropdown.Tables[0];
            ddlbank.DataValueField = "BANKNO";
            ddlbank.DataTextField = "BANK_SHORTNAME";
            ddlbank.DataBind();
            if (lblBankNo.Text == "0" || lblBankNo.Text == "")
            {
                ddlbank.SelectedValue = "0";
            }
            else
            {
                ddlbank.SelectedValue = lblBankNo.Text.ToString();
            }

            //ddlbank.SelectedValue = lblBankNo.Text.ToString();
            Label lbstatus = dataitem.FindControl("lblsemstatus") as Label;
            TextBox txtamt = dataitem.FindControl("txtsemamount") as TextBox;
            LinkButton lnk = dataitem.FindControl("lnksem") as LinkButton;
            dstatus.SelectedValue = lbstatus.Text;
            lnk.Visible = true;

            if (dstatus.SelectedValue == "1")
            {
                divlkonlineadm.Attributes.Remove("class");
                txtamt.Enabled = false;
            }

            else if (dstatus.SelectedValue == "2")
            {
                divlkonlineadm.Attributes.Remove("class");
                txtamt.Enabled = true;
            }

        }
    }
    private void bindsemdata()
    {
        divlkonlineadm.Attributes.Remove("class");
        DataSet ds = objmp.GETSEMESTERDATA(Convert.ToInt32(ddlintakethree.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlstudylevelsem.SelectedValue));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            Panel1.Visible = true;
            lvsemester.DataSource = ds;
            lvsemester.DataBind();
            btnsubsem.Visible = true;

        }
        else
        {
            objCommon.DisplayMessage(this.updsemster, "Record Not Found", this.Page);
            Panel1.Visible = false;
            lvsemester.DataSource = null;
            lvsemester.DataBind();
            btnsubsem.Visible = false;
            Clearsem();

        }
        foreach (ListViewDataItem dataitem in lvsemester.Items)
        {
            DropDownList dstatus = dataitem.FindControl("ddlstatussem") as DropDownList;
            Label lbstatus = dataitem.FindControl("lblsemstatus") as Label;
            TextBox txtamt = dataitem.FindControl("txtsemamount") as TextBox;
            LinkButton lnk = dataitem.FindControl("lnksem") as LinkButton;
            dstatus.SelectedValue = lbstatus.Text;
            lnk.Visible = false;

            if (dstatus.SelectedValue == "1")
            {
                txtamt.Enabled = false;
            }

            else if (dstatus.SelectedValue == "2")
            {
                txtamt.Enabled = true;
            }
        }
    }
    protected void btnsubsem_Click(object sender, EventArgs e)
    {

        try
        {
            divlkonlineadm.Attributes.Remove("class");
            string subject = "";
            string matter = "";
            int ua_no = Convert.ToInt32(Session["userno"]);
            CustomStatus cs = 0;
            foreach (ListViewDataItem dataitem in lvsemester.Items)
            {
                int userno = 0;
                string orderid = string.Empty;
                string amount = string.Empty;
                string date = string.Empty;
                string STATUS = string.Empty;
                string REMARK = string.Empty;
                string EMAIL = "";
                string USERNO = "";
                string NAME = "";
                string paymode = "";
                string intake = "";
                string degree = "";
                CheckBox chkBox = dataitem.FindControl("chreg") as CheckBox;
                TextBox torderid = dataitem.FindControl("txtorder") as TextBox;
                TextBox tamount = dataitem.FindControl("txtsemamount") as TextBox;
                TextBox tdate = dataitem.FindControl("txtsemdate") as TextBox;
                DropDownList dstatus = dataitem.FindControl("ddlstatussem") as DropDownList;
                DropDownList ddlBank = dataitem.FindControl("ddlbank") as DropDownList;
                TextBox treamrk = dataitem.FindControl("txtsemremark") as TextBox;
                Label lbemail = dataitem.FindControl("lblsememail") as Label;
                Label lbregname = dataitem.FindControl("lblsemname") as Label;
                Label lblTempDcrNo = dataitem.FindControl("lblDcrTempNo") as Label;
                HiddenField hdfuser = dataitem.FindControl("hdnDocno") as HiddenField;
                Label lbusername = dataitem.FindControl("lblsemusername") as Label;
                HiddenField hdpaymod = dataitem.FindControl("hdfpaymod") as HiddenField;
                HiddenField hdintake = dataitem.FindControl("hdfintake") as HiddenField;
                HiddenField hddegree = dataitem.FindControl("hdfdegree") as HiddenField;
                if (chkBox.Checked == true)
                {
                    userno = Convert.ToInt32(chkBox.ToolTip);
                    amount = tamount.Text;
                    date = tdate.Text;
                    STATUS = dstatus.SelectedValue;
                    REMARK = treamrk.Text;
                    EMAIL = lbemail.Text;
                    NAME = lbregname.Text;
                    USERNO = lbusername.Text;
                    paymode = hdpaymod.Value;
                    intake = hdintake.Value;
                    degree = hddegree.Value;
                    if (dstatus.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.updonlineadm, "Please Select Status", this.Page);
                    }
                    else
                    {
                        cs = (CustomStatus)objmp.insertREG(userno, amount, date, STATUS, REMARK, Convert.ToInt32(lblTempDcrNo.Text), Convert.ToString(ddlBank.SelectedValue), Convert.ToInt32(Session["userno"]));
                        if (dstatus.SelectedValue == "1")
                        {
                            matter = "Approved.";
                        }
                        else if (dstatus.SelectedValue == "2")
                        {
                            matter = "Pending";
                        }
                        else if (dstatus.SelectedValue == "3")
                        {
                            matter = "Rejected";
                        }

                        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                        string SP_Parameters = ""; string Call_Values = ""; string SP_Name = "";

                        DataSet dsUserContact = null;
                        string message = "";
                        //dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(1125));
                        //message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                        //subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                        string dcr = objCommon.LookUp("ACD_DCR", "DCR_NO", "TEMP_DCR_NUMBER=" + Convert.ToString(lblTempDcrNo.Text));
                        //string ReffEmail = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]);
                        //string ReffPassword = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"]);
                        //if (dstatus.SelectedValue == "1")
                        //{
                        //    EmailSmsWhatssppSend(1125, EMAIL, NAME.ToString(), matter.ToString(), lbusername.ToString(), degree.ToString(), intake.ToString(), paymode.ToString(), amount.ToString(), date.ToString(), Convert.ToString(hdfuser.Value), Session["colcode"].ToString(), Convert.ToString(dcr), Convert.ToString(chkBox.ToolTip));
                        //    //Task<int> task = ExecutesErp(message, EMAIL, subject, NAME.ToString(), matter.ToString(), lbusername.ToString(), degree.ToString(), intake.ToString(), paymode.ToString(), amount.ToString(), date.ToString(), Convert.ToString(hdfuser.Value), Session["colcode"].ToString(), Convert.ToString(dcr), Convert.ToString(chkBox.ToolTip));
                        //    //int status = task.Result;
                        //    //if (status == 0)
                        //    //{
                        //    //    objCommon.DisplayMessage(this, "Sorry , failed to send email !!!", this.Page);
                        //    //}

                        //    chkBox.Checked = false;


                        //}

                    }

                }
            }

            if (cs.Equals(CustomStatus.RecordUpdated))
            {

                objCommon.DisplayMessage(this.upderpadm, "Record saved successfully.", this.Page);
                bindsemdataoffline();
                divlkonlineadm.Attributes.Remove("class");
            }
            else
            {
                objCommon.DisplayMessage(this.upderpadm, "Please Select At least One checkbox", this.Page);
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
    protected void btnshowsem_Click(object sender, EventArgs e)
    {
        bindsemdata();
        btnsubsem.Visible = false;
    }

    protected void lnksem_Click(object sender, EventArgs e)
    {
        try
        {
            imageViewerContainer.Visible = false;
            irm1.Visible = true;
            divlkonlineadm.Attributes.Remove("class");
            //foreach (ListViewDataItem dataitem in lvsemester.Items)
            //{
            LinkButton lnksem = (LinkButton)(sender);
            //CheckBox chk = dataitem.FindControl("chreg") as CheckBox;
            //HiddenField hdf = dataitem.FindControl("hdnDocno") as HiddenField;
            //int userno = Convert.ToInt32(chk.ToolTip);
            //if (Convert.ToInt32(lnksem.ToolTip) == userno)
            //{
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
            string img = lnksem.CommandArgument.ToString();
            var ImageName = img;
            if (img.ToString() != string.Empty || img.ToString() != "")
            {
                string extension = Path.GetExtension(img.ToString());
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
                if (extension == ".pdf")
                {
                    //Change by Ashish
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
                    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect script", "window.location = '" + Page.ResolveUrl("~/PopUp.aspx") + "'", true);
                    //ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
                    //break;
                    ///////////End By Ashish/////////////////////////////


                    //ImageViewer.Visible = false;
                    //ltEmbed.Visible = true;
                    //imageViewerContainer.Visible = false;
                    //var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                    //string filePath = directoryPath + "\\" + ImageName;
                    //if ((System.IO.File.Exists(filePath)))
                    //{
                    //    System.IO.File.Delete(filePath);
                    //}
                    //ViewState["filePath_Show"] = filePath;
                    //Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    //hdfImagePath.Value = null;
                    //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"765px\" height=\"400px\">";
                    //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    //embed += "</object>";
                    //ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                    ////ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
                    //break;
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
                    //hdfImagePath.Value = "data:image/png;base64," + Base64String;
                    //imageViewerContainer.Visible = true;
                    //irm1.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
                    //hdfImagePath.Value = null;
                    //ImageViewer.Visible = false;
                    //ltEmbed.Visible = false;
                    //imageViewerContainer.Visible = true;
                    //var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                    //string filePath = directoryPath + "\\" + ImageName;
                    //if ((System.IO.File.Exists(filePath)))
                    //{
                    //    System.IO.File.Delete(filePath);
                    //}
                    //ViewState["filePath_Show"] = filePath;
                    //Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    //hdfImagePath.Value = ResolveUrl("~/DownloadImg/" + ImageName);
                    ////string embed = "<object data=\"{0}\" type=\"image/jpeg\" width=\"765px\" height=\"400px\">";
                    ////embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    ////embed += " or download <a target = \"_blank\" href = \"https://www.xnview.com/en/\">Adobe PDF Reader</a> to view the file.";
                    ////embed += "</object>";
                    ////ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
                    // break;

                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Sorry, File not found !!!", this.Page);
            }
            //ImageViewer.ImageUrl = "~/showimage.aspx?id=" + userno + "&type=ONLINEADMISSIONCOPY";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);

            // }
            //else
            //{
            //    ImageViewer.ImageUrl = "~/showimage.aspx?id=" + userno + "&type=ONLINEADMISSIONCOPY";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
            //}
            // }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Sorry, File not found !!!", this.Page);
        }
    }
    protected void ddlstatussem_SelectedIndexChanged(object sender, EventArgs e)
    {
        divlkonlineadm.Attributes.Remove("class");
        foreach (ListViewDataItem dataitem in lvsemester.Items)
        {
            DropDownList dstatus = dataitem.FindControl("ddlstatussem") as DropDownList;
            Label lbstatus = dataitem.FindControl("lblsemstatus") as Label;
            TextBox txtamt = dataitem.FindControl("txtsemamount") as TextBox;


            if (dstatus.SelectedValue == "1")
            {
                txtamt.Enabled = false;
            }

            else if (dstatus.SelectedValue == "2")
            {
                txtamt.Enabled = true;
            }
        }
    }

    protected void rdosemselection_SelectedIndexChanged(object sender, EventArgs e)
    {
        divlkonlineadm.Attributes.Remove("class");
        if (rdosemselection.SelectedValue == "1")
        {
            divstatusem.Visible = true;
            divHigherBank.Visible = true;
            divProgram.Visible = true;
            studysem.Visible = true;
            intakesem.Visible = true;
            sem.Visible = true;
            btnshowsem.Visible = false;
            btnsubsem.Visible = false;
            btnshowsemoff.Visible = true;
            divLoanScheme.Visible = false;
            btncancelsem.Visible = true;
            lvsemester.DataSource = null;
            lvsemester.DataBind();
            ddlintakethree.SelectedIndex = 0;
            ddlstudylevelsem.SelectedIndex = 0;
            ddlsemester.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 0;
            BindSemesterCount();
            Panel2.Visible = false;
            LvApplicationCount.DataSource = null;
            LvApplicationCount.DataBind();
            Panel3.Visible = false;
            LvEnrollmentCount.DataSource = null;
            LvEnrollmentCount.DataBind();
        }
        else if (rdosemselection.SelectedValue == "2")
        {
            sem.Visible = true;
            studysem.Visible = true;
            intakesem.Visible = true;
            btnshowsem.Visible = true;
            divHigherBank.Visible = false;
            divProgram.Visible = false;
            btnshowsemoff.Visible = false;
            btnsubsem.Visible = false;
            btncancelsem.Visible = true;
            divstatusem.Visible = false;
            divLoanScheme.Visible = false;
            lvsemester.DataSource = null;
            lvsemester.DataBind();
            ddlintakethree.SelectedIndex = 0;
            ddlstudylevelsem.SelectedIndex = 0;
            ddlsemester.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 0;
            Panel2.Visible = false;
            LvApplicationCount.DataSource = null;
            LvApplicationCount.DataBind();
            Panel3.Visible = false;
            LvEnrollmentCount.DataSource = null;
            LvEnrollmentCount.DataBind();
            Panel4.Visible = false;
            LvSemesterCount.DataSource = null;
            LvSemesterCount.DataBind();
        }
    }
    protected void btnshowsemoff_Click(object sender, EventArgs e)
    {
        divlkonlineadm.Attributes.Remove("class");
        bindsemdataoffline();
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {

    }

    static private void ConfigureCrystalReports(ReportDocument customReport)
    {
        ConnectionInfo connectionInfo = Common.GetCrystalConnection();
        Common.SetDBLogonForReport(connectionInfo, customReport);
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

    // added by ekansh
    static private MemoryStream ShowGeneralOnlineExportReportForMail(string path, string paramString)
    {
        MemoryStream oStream;
        ReportDocument customReport;
        customReport = new ReportDocument();
        string reportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Academic/FeeCollectionReceiptDownPay.rpt");
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

    static private MemoryStream ShowGeneralExportReportForMailForApplication(string path, string paramString)
    {
        MemoryStream oStream;
        ReportDocument customReport;
        customReport = new ReportDocument();
        string reportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Academic/FeeCollectionHallticket.rpt");
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

    protected void lkroyality_Click(object sender, EventArgs e)
    {
        divlkerpadm.Attributes.Remove("class");
        divlkonlineadm.Attributes.Remove("class");
        divlksemester.Attributes.Remove("class");
        divlkroyality.Attributes.Add("class", "active");
        DivsLoanScheme.Attributes.Remove("class");
        lvonlineadm.DataSource = null;
        lvonlineadm.DataBind();
        ddlintake.SelectedIndex = 0;
        ddluserselection.SelectedIndex = 0;
        ddlBank.SelectedIndex = 0;
        pnlCourse.Visible = false;
        diverpadm.Visible = false;
        divonlineadm.Visible = false;
        diverpadm.Visible = false;
        divsemster.Visible = false;
        divroyalty.Visible = true;
        divLoanScheme.Visible = false;
        Panel2.Visible = false;
        LvApplicationCount.DataSource = null;
        LvApplicationCount.DataBind();
        Panel4.Visible = false;
        LvSemesterCount.DataSource = null;
        LvSemesterCount.DataBind();
        Selection.Visible = true;
        registration.Visible = true;
        semselection.Visible = true;
        electroyalty.Visible = true;

    }

    protected void rdoselectroyalty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoselectroyalty.SelectedValue == "1")
        {
            divlkonlineadm.Attributes.Remove("class");
            btnshowroyalon.Visible = false;
            btnshowroyaloff.Visible = true;
            btnroyacancel.Visible = true;
            bankroya.Visible = true;
            intaketworoya.Visible = false;
            statusroya.Visible = false;
            divroyastudy.Visible = true;
            intakeoneroya.Visible = false;
            statusroya.Visible = true;
            ddlfacultyroyal.Visible = true;
            div1intake2.Visible = false;
            divLoanScheme.Visible = false;
            divBankEnrollment.Visible = false;
            div1paystatus2.Visible = false;
            div2intake2.Visible = false;
            btnshowregone.Visible = false;
            btnShowAddmissionProper.Visible = false;
            btncancelreg.Visible = false;
            btnsubreg.Visible = false;
            btnSubAddProper.Visible = false;
            divstudyleveltwo.Visible = false;
            btnshowregtwo.Visible = false;
            BindRoyaltCount();
            //divfacultytwo.Visible = true;
            lvreg.DataSource = null;
            lvreg.DataBind();
            lvregOffline.DataSource = null;
            lvregOffline.DataBind();
            Panel2.Visible = false;
            LvApplicationCount.DataSource = null;
            LvApplicationCount.DataBind();
            Panel4.Visible = false;
            LvSemesterCount.DataSource = null;
            LvSemesterCount.DataBind();

        }
        else if (rdoselectroyalty.SelectedValue == "2")
        {
            divlkonlineadm.Attributes.Remove("class");
            btnshowroyaloff.Visible = false;
            btnshowroyalon.Visible = true;
            divroyastudy.Visible = true;
            bankroya.Visible = false;
            statusroya.Visible = false;
            intakeoneroya.Visible = false;
            statusroya.Visible = false;
            ddlfacultyroyal.Visible = false;
            intaketworoya.Visible = true;
            div2intake2.Visible = false;
            divBankEnrollment.Visible = false;
            div1intake2.Visible = false;
            div1paystatus2.Visible = false;
            btnshowregtwo.Visible = true;
            divstudyleveltwo.Visible = false;
            btnshowregone.Visible = false;
            btnShowAddmissionProper.Visible = false;
            btncancelreg.Visible = false;
            btnsubreg.Visible = false;
            btnSubAddProper.Visible = false;
            btnroyacancel.Visible = true;
            divstudyleveltwo.Visible = false;
            divLoanScheme.Visible = false;
            lvreg.DataSource = null;
            lvreg.DataBind();
            lvregOffline.DataSource = null;
            lvregOffline.DataBind();
            Panel2.Visible = false;
            LvApplicationCount.DataSource = null;
            LvApplicationCount.DataBind();
            Panel3.Visible = false;
            LvEnrollmentCount.DataSource = null;
            LvEnrollmentCount.DataBind();
            Panel4.Visible = false;
            LvSemesterCount.DataSource = null;
            LvSemesterCount.DataBind();
            Panelroyacout.Visible = false;
            Lvroyacount.DataSource = null;
            Lvroyacount.DataBind();
        }
    }
    private void BindRoyaOn()
    {
        DataSet ds = objmp.GetOnlineRoyal(Convert.ToInt32(ddlintaketworoyalty.SelectedValue), Convert.ToInt32(ddlstyduroyalty.SelectedValue), Convert.ToInt32(ddlfacultyroyal.SelectedValue));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            PanelRoya.Visible = true;
            LvRoya.DataSource = ds;
            LvRoya.DataBind();
            btnsubroya.Visible = true;
        }
        else
        {
            objCommon.DisplayMessage(this.upderpadm, "Record Not Found", this.Page);
            PanelRoya.Visible = false;
            LvRoya.DataSource = null;
            LvRoya.DataBind();
            btnsubroya.Visible = false;
        }
        foreach (ListViewDataItem dataitem in LvRoya.Items)
        {
            DropDownList dstatus = dataitem.FindControl("ddloyastatus") as DropDownList;
            Label lblstatus = dataitem.FindControl("lbloyastatus") as Label;
            LinkButton lnk = dataitem.FindControl("lnkroya") as LinkButton;
            TextBox txtamt = dataitem.FindControl("txtoyaamount") as TextBox;
            dstatus.SelectedValue = lblstatus.Text;
            lnk.Visible = false;
            if (dstatus.SelectedValue == "1")
            {
                txtamt.Enabled = false;
            }
            else if (dstatus.SelectedValue == "2")
            {
                txtamt.Enabled = true;
            }
        }
    }
    private void BindERoyaOffline()
    {
        divlkonlineadm.Attributes.Remove("class");
        DataSet ds = objmp.GetRoyaltyOff(Convert.ToInt32(ddlintakeroyalty.SelectedValue), Convert.ToInt32(ddlpaymentstsroya.SelectedValue), Convert.ToInt32(ddlstyduroyalty.SelectedValue), Convert.ToInt32(ddlbankroyal.SelectedValue));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            divlkonlineadm.Attributes.Remove("class");
            PanelRoya.Visible = true;
            LvRoya.DataSource = ds;
            LvRoya.DataBind();
            if (ddlpaymentstsroya.SelectedValue == "2")
            {
                btnsubroya.Visible = true;
            }
            else
            {
                if ((Convert.ToInt32(Session["userno"].ToString()) == 32 || Convert.ToInt32(Session["userno"].ToString()) == 255 || Convert.ToInt32(Session["userno"].ToString()) == 467 || Convert.ToInt32(Session["userno"].ToString()) == 466 || Convert.ToInt32(Session["userno"].ToString()) == 468) && ddlpaymentstsroya.SelectedValue == "3")
                {
                    btnsubroya.Visible = true;
                }
                else
                {
                    btnsubroya.Visible = false;
                }
            }
        }
        else
        {
            PanelRoya.Visible = false;
            LvRoya.DataSource = null;
            LvRoya.DataBind();
            btnsubroya.Visible = false;
            Clearsem();
            objCommon.DisplayMessage(this.updsemster, "Record Not Found", this.Page);
            return;

        }
        foreach (ListViewDataItem dataitem in LvRoya.Items)
        {
            DropDownList dstatus = dataitem.FindControl("ddloyastatus") as DropDownList;
            DropDownList ddlbank = dataitem.FindControl("ddlbank") as DropDownList;
            Label lblBankNo = dataitem.FindControl("lblbank") as Label;
            objCommon.FillDropDownList(ddlbank, "ACD_BANK", "DISTINCT BANKNO", "BANK_SHORTNAME", "BANKNO>0", "BANKNO");
            if (lblBankNo.Text == "0" || lblBankNo.Text == "")
            {
                ddlbank.SelectedValue = "0";
            }
            else
            {
                ddlbank.SelectedValue = lblBankNo.Text.ToString();
            }
            TextBox txtamt = dataitem.FindControl("txtoyaamount") as TextBox;
            LinkButton lnk = dataitem.FindControl("lnkroya") as LinkButton;
            Label lblstas = dataitem.FindControl("lbloyastatus") as Label;
            dstatus.SelectedValue = lblstas.Text;
            lnk.Visible = true;

            if (dstatus.SelectedValue == "1")
            {
                divlkonlineadm.Attributes.Remove("class");
                txtamt.Enabled = false;
            }

            else if (dstatus.SelectedValue == "2")
            {
                divlkonlineadm.Attributes.Remove("class");
                txtamt.Enabled = true;
            }

        }
    }
    protected void btnshowroyaloff_Click(object sender, EventArgs e)
    {
        BindERoyaOffline();
    }
    protected void btnshowroyalon_Click(object sender, EventArgs e)
    {
        BindRoyaOn();
    }
    protected void btnroyacancel_Click(object sender, EventArgs e)
    {
        divlkonlineadm.Attributes.Remove("class");
        ddlstyduroyalty.SelectedIndex = 0;
        ddlintakeroyalty.SelectedIndex = 0;
        ddlintaketworoyalty.SelectedIndex = 0;
        ddlpaymentstsroya.SelectedIndex = 0;
        ddlbankroyal.SelectedIndex = 0;
        PanelRoya.Visible = false;
        btnsubroya.Visible = false;
        LvRoya.DataSource = null;
        LvRoya.DataBind();
    }
    protected void ddloyastatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        divlkonlineadm.Attributes.Remove("class");
        foreach (ListViewDataItem dataitem in LvRoya.Items)
        {
            DropDownList dstatus = dataitem.FindControl("ddloyastatus") as DropDownList;
            Label lblstatus = dataitem.FindControl("lbloyastatus") as Label;
            TextBox txtamt = dataitem.FindControl("txtoyaamount") as TextBox;

            if (dstatus.SelectedValue == "1")
            {
                txtamt.Enabled = false;
            }

            else if (dstatus.SelectedValue == "2")
            {
                txtamt.Enabled = true;
            }
        }
    }
    protected void lnkroya_Click(object sender, EventArgs e)
    {
        try
        {
            imageViewerContainer.Visible = false;
            irm1.Visible = true;
            divlkonlineadm.Attributes.Remove("class");
            //foreach (ListViewDataItem dataitem in LvRoya.Items)
            //{
            LinkButton lnkreg = (LinkButton)(sender);
            //CheckBox chk = dataitem.FindControl("chroya") as CheckBox;
            //HiddenField hdf = dataitem.FindControl("hdnDocno") as HiddenField;
            //int userno = Convert.ToInt32(chk.ToolTip);
            //if (Convert.ToInt32(lnkreg.ToolTip) == userno)
            //{
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
            string img = lnkreg.CommandArgument.ToString();
            var ImageName = img;
            if (img.ToString() != string.Empty || img.ToString() != "")
            {
                string extension = Path.GetExtension(img.ToString());
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
                if (extension == ".pdf")
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
                    Session["Base64StringType"] = "application/pdf";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
                    //ImageViewer.Visible = false;
                    //ltEmbed.Visible = true;
                    //imageViewerContainer.Visible = false;

                    //var Newblob = blobContainer.GetBlockBlobReference(ImageName);

                    //string filePath = directoryPath + "\\" + ImageName;


                    //if ((System.IO.File.Exists(filePath)))
                    //{
                    //    System.IO.File.Delete(filePath);
                    //}
                    //ViewState["filePath_Show"] = filePath;
                    //Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    //hdfImagePath.Value = null;

                    //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"765px\" height=\"400px\">";
                    //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    //embed += "</object>";
                    //ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
                    //break;
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
                    imageViewerContainer.Visible = true;
                    irm1.Visible = false;
                    hdfImagePath.Value = "data:image/png;base64," + Base64String;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
                    //hdfImagePath.Value = null;
                    //ImageViewer.Visible = false;
                    //ltEmbed.Visible = false;
                    //imageViewerContainer.Visible = true;
                    //var Newblob = blobContainer.GetBlockBlobReference(ImageName);

                    //string filePath = directoryPath + "\\" + ImageName;


                    //if ((System.IO.File.Exists(filePath)))
                    //{
                    //    System.IO.File.Delete(filePath);
                    //}
                    //ViewState["filePath_Show"] = filePath;
                    //Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    //hdfImagePath.Value = ResolveUrl("~/DownloadImg/" + ImageName);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
                    //break;

                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Sorry, File not found !!!", this.Page);
            }
            // }

            // }
        }
        catch (Exception ex)
        {

        }

    }
    protected void btnsubroya_Click(object sender, EventArgs e)
    {
        try
        {
            divlkonlineadm.Attributes.Remove("class");
            string subject = "";
            string matter = "";
            int ua_no = Convert.ToInt32(Session["userno"]);
            CustomStatus cs = 0;
            foreach (ListViewDataItem dataitem in LvRoya.Items)
            {
                int userno = 0;
                string orderid = string.Empty;
                string amount = string.Empty;
                string date = string.Empty;
                string STATUS = string.Empty;
                string REMARK = string.Empty;
                string EMAIL = "";
                string USERNO = "";
                string NAME = "";
                string paymode = "";
                string intake = "";
                string degree = "";
                CheckBox chkBox = dataitem.FindControl("chroya") as CheckBox;
                TextBox tamount = dataitem.FindControl("txtoyaamount") as TextBox;
                TextBox tdate = dataitem.FindControl("txtoyadate") as TextBox;
                DropDownList dstatus = dataitem.FindControl("ddloyastatus") as DropDownList;
                DropDownList ddlBank = dataitem.FindControl("ddlbank") as DropDownList;
                TextBox treamrk = dataitem.FindControl("txtoyaremark") as TextBox;
                Label lbemail = dataitem.FindControl("lblemail") as Label;
                Label lbregname = dataitem.FindControl("lbloyaname") as Label;
                Label lblTempDcrNo = dataitem.FindControl("lblDcrTempNo") as Label;
                HiddenField hdfuser = dataitem.FindControl("hdnDocno") as HiddenField;
                Label lbusername = dataitem.FindControl("lbloyausername") as Label;
                HiddenField hdpaymod = dataitem.FindControl("hdfpaymod") as HiddenField;
                HiddenField hdintake = dataitem.FindControl("hdfintake") as HiddenField;
                HiddenField hddegree = dataitem.FindControl("hdfdegree") as HiddenField;
                HiddenField hdfdcr = dataitem.FindControl("hdfdcr") as HiddenField;
                if (chkBox.Checked == true)
                {
                    userno = Convert.ToInt32(chkBox.ToolTip);
                    amount = tamount.Text;
                    date = tdate.Text;
                    STATUS = dstatus.SelectedValue;
                    REMARK = treamrk.Text;
                    EMAIL = lbemail.Text;
                    NAME = lbregname.Text;
                    USERNO = lbusername.Text;
                    paymode = hdpaymod.Value;
                    intake = hdintake.Value;
                    degree = hddegree.Value;
                    if (dstatus.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.updonlineadm, "Please Select Status", this.Page);
                    }
                    else
                    {
                        cs = (CustomStatus)objmp.InsertRoyal(userno, amount, date, STATUS, REMARK, Convert.ToInt32(lblTempDcrNo.Text), Convert.ToString(ddlBank.SelectedValue), Convert.ToInt32(Session["userno"]));

                        if (dstatus.SelectedValue == "1")
                        {
                            matter = "Approved.";
                        }
                        else if (dstatus.SelectedValue == "2")
                        {
                            matter = "Pending";
                        }
                        else if (dstatus.SelectedValue == "3")
                        {
                            matter = "Rejected";
                        }

                        DataSet dsUserContact = null;
                        string message = "";
                        //dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));
                        //message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                        //subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                        ////Execute(message, date, amount, paymode, intake,degree, EMAIL, subject, NAME, matter, USERNO, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
                        //chkBox.Checked = false;
                        string dcr = objCommon.LookUp("ACD_DCR", "DCR_NO", "TEMP_DCR_NUMBER=" + Convert.ToString(lblTempDcrNo.Text));
                        dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(2627));
                        message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                        subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                        string ReffEmail = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]);
                        string ReffPassword = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"]);
                        if (dstatus.SelectedValue == "1")
                        {


                            EmailSmsWhatssppSend(2627, EMAIL, NAME.ToString(), matter.ToString(), lbusername.Text.ToString(), degree.ToString(), intake.ToString(), paymode.ToString(), amount.ToString(), date.ToString(), Convert.ToString(hdfuser.Value), Session["colcode"].ToString(), Convert.ToString(dcr), Convert.ToString(chkBox.ToolTip));


                            chkBox.Checked = false;


                        }


                    }

                }
            }

            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.upderpadm, "Record saved successfully.", this.Page);
                BindERoyaOffline();
            }
            else
            {
                objCommon.DisplayMessage(this.upderpadm, "Please Select At least One checkbox", this.Page);
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
    //Loan Scheme Tab Start
    protected void lkLoanScheme_Click(object sender, EventArgs e)
    {
        divlkonlineadm.Attributes.Remove("class");
        divlkerpadm.Attributes.Remove("class");
        divlksemester.Attributes.Remove("class");
        divlkroyality.Attributes.Remove("class");
        DivsLoanScheme.Attributes.Add("class", "active");
        divLoanScheme.Visible = true;
        Selection.Visible = false;
        registration.Visible = false;
        semselection.Visible = false;
        electroyalty.Visible = false;
        divroyalty.Visible = false;
        divsemster.Visible = false;
        diverpadm.Visible = false;
        divonlineadm.Visible = false;

    }
    private void BindLoanSchemeData()
    {
        DataSet ds = objmp.GetOnlineLoanSchemedata(Convert.ToInt32(ddlLoanSchemeIntake.SelectedValue), Convert.ToInt32(ddlLoanSchemeStudyLevel.SelectedValue));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            PanelLoan.Visible = true;
            LvLoanScheme.DataSource = ds;
            LvLoanScheme.DataBind();
            btnSubmitLoanScheme.Visible = true;
        }
        else
        {
            objCommon.DisplayMessage(this.upderpadm, "Record Not Found", this.Page);
            PanelLoan.Visible = false;
            LvLoanScheme.DataSource = null;
            LvLoanScheme.DataBind();
            btnSubmitLoanScheme.Visible = false;
        }
        foreach (ListViewDataItem dataitem in LvLoanScheme.Items)
        {
            DropDownList dstatus = dataitem.FindControl("ddlLoanstatus") as DropDownList;
            Label lblstatus = dataitem.FindControl("lblLoanstatus") as Label;
            LinkButton lnk = dataitem.FindControl("lnkroya") as LinkButton;
            TextBox txtamt = dataitem.FindControl("txtTotalDemand") as TextBox;
            dstatus.SelectedValue = lblstatus.Text;
            lnk.Visible = false;
            if (dstatus.SelectedValue == "1")
            {
                txtamt.Enabled = false;
            }
            else if (dstatus.SelectedValue == "2")
            {
                txtamt.Enabled = true;
            }
        }
    }

    protected void btnShowLoanScheme_Click(object sender, EventArgs e)
    {
        BindLoanSchemeData();
    }
    protected void btnSubmitLoanScheme_Click(object sender, EventArgs e)
    {
        try
        {
            divlkonlineadm.Attributes.Remove("class");
            string subject = "";
            string matter = "";
            int ua_no = Convert.ToInt32(Session["userno"]);
            CustomStatus cs = 0;
            foreach (ListViewDataItem dataitem in LvLoanScheme.Items)
            {
                int userno = 0;
                string orderid = string.Empty;
                string amount = string.Empty;
                string Consession_amt = string.Empty;
                string date = string.Empty;
                string STATUS = string.Empty;
                string REMARK = string.Empty;
                string EMAIL = "";
                string USERNO = "";
                string NAME = "";
                string paymode = "";
                string intake = "";
                string degree = "";
                CheckBox chkBox = dataitem.FindControl("chrLoan") as CheckBox;
                TextBox torderid = dataitem.FindControl("txtorder") as TextBox;
                TextBox tamount = dataitem.FindControl("txtTotal_payble") as TextBox;
                TextBox txtComnsession = dataitem.FindControl("txtComnsession") as TextBox;
                TextBox tdate = dataitem.FindControl("txtLoandate") as TextBox;
                DropDownList dstatus = dataitem.FindControl("ddlLoanstatus") as DropDownList;
                DropDownList ddlBank = dataitem.FindControl("ddlbank") as DropDownList;
                TextBox treamrk = dataitem.FindControl("txtLoanremark") as TextBox;
                Label lbemail = dataitem.FindControl("lblLoanemail") as Label;
                Label lbregname = dataitem.FindControl("lblLoanusername") as Label;
                Label lblTempDcrNo = dataitem.FindControl("lblDcrTempNo") as Label;
                HiddenField hdfuser = dataitem.FindControl("hdnDocno") as HiddenField;
                Label lbusername = dataitem.FindControl("lblLoanusername") as Label;
                HiddenField hdpaymod = dataitem.FindControl("hdfpaymod") as HiddenField;
                HiddenField hdintake = dataitem.FindControl("hdfintake") as HiddenField;
                HiddenField hddegree = dataitem.FindControl("hdfdegree") as HiddenField;
                HiddenField hdfdcr = dataitem.FindControl("hdfdcr") as HiddenField;
                if (chkBox.Checked == true)
                {
                    userno = Convert.ToInt32(chkBox.ToolTip);
                    amount = tamount.Text;
                    date = tdate.Text;
                    STATUS = dstatus.SelectedValue;
                    REMARK = treamrk.Text;
                    EMAIL = lbemail.Text;
                    NAME = lbregname.Text;
                    USERNO = lbusername.Text;
                    paymode = hdpaymod.Value;
                    intake = hdintake.Value;
                    degree = hddegree.Value;
                    Consession_amt = txtComnsession.Text;
                    if (dstatus.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.updonlineadm, "Please Select Status", this.Page);
                    }
                    else
                    {
                        cs = (CustomStatus)objmp.insertREGLoanScheme(userno, amount, date, STATUS, REMARK, Convert.ToInt32(lblTempDcrNo.Text), Convert.ToString(ddlBank.SelectedValue), Convert.ToInt32(Session["userno"]), Consession_amt);
                        if (dstatus.SelectedValue == "1")
                        {
                            matter = "Approved.";
                        }
                        else if (dstatus.SelectedValue == "2")
                        {
                            matter = "Pending";
                        }
                        else if (dstatus.SelectedValue == "3")
                        {
                            matter = "Rejected";
                        }

                        DataSet dsUserContact = null;
                        string message = "";
                        //dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));
                        //message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                        //subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                        ////Execute(message, date, amount, paymode, intake,degree, EMAIL, subject, NAME, matter, USERNO, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
                        //chkBox.Checked = false;
                        string dcr = objCommon.LookUp("ACD_DCR", "DCR_NO", "TEMP_DCR_NUMBER=" + Convert.ToString(lblTempDcrNo.Text));
                        dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(2627));
                        message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                        subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                        string ReffEmail = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]);
                        string ReffPassword = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"]);
                        if (dstatus.SelectedValue == "1")
                        {

                            EmailSmsWhatssppSend(2627, EMAIL, NAME.ToString(), matter.ToString(), lbusername.Text.ToString(), degree.ToString(), intake.ToString(), paymode.ToString(), amount.ToString(), date.ToString(), Convert.ToString(hdfuser.Value), Session["colcode"].ToString(), Convert.ToString(dcr), Convert.ToString(chkBox.ToolTip));


                            chkBox.Checked = false;

                        }


                    }
                }

            }

            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.upderpadm, "Record saved successfully.", this.Page);
                BINDREGONELIST();
            }
            else
            {
                objCommon.DisplayMessage(this.upderpadm, "Please Select At least One checkbox", this.Page);
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
    protected void btnCancelLoanScheme_Click(object sender, EventArgs e)
    {
        LvLoanScheme.DataSource = null;
        LvLoanScheme.DataBind();
        ddlLoanSchemeStudyLevel.SelectedValue = "0";
        ddlLoanSchemeIntake.SelectedValue = "0";


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

    static async Task<int> Executes(string message, string toSendAddress, string Subject, string NAME, string matter, string lbusername, string degree, string intake, string paymode, string amount, string date, string UserNo, string College_Code)
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

            msgs.Body = msgs.Body.Replace("[UA_FULLNAME]", NAME.ToString());
            msgs.Body = msgs.Body.Replace("[MATTER]", matter.ToString());
            msgs.Body = msgs.Body.Replace("[Application ID]", lbusername.ToString());
            msgs.Body = msgs.Body.Replace("[PROGRAM NAMES]", degree.ToString());
            msgs.Body = msgs.Body.Replace("[INTAKE]", intake.ToString());
            msgs.Body = msgs.Body.Replace("[Payment Mode]", paymode.ToString());
            msgs.Body = msgs.Body.Replace("[Payment Amount]", amount.ToString());
            msgs.Body = msgs.Body.Replace("[Payment Date]", date.ToString());


            MemoryStream oAttachment1 = ShowGeneralExportReportForMailForApplication("Reports,Academic,FeeCollectionHallticket.rpt", "@P_USERNO=" + Convert.ToString(lbusername) + ",@P_COLLEGE_CODE=" + College_Code.ToString() + "");
            //MemoryStream oAttachment = ShowGeneralExportReportForMail("Reports,Academic,FeeCollectionReceipt.rpt", "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DCRNO=" + hfddcr.Value + ",@P_IDNO=" + Convert.ToInt32(chk.ToolTip) + "");
            var bytesRpt = oAttachment1.ToArray();
            var fileRpt = Convert.ToBase64String(bytesRpt);
            byte[] test = (byte[])bytesRpt;
            //  msg.AddAttachment("Offerletter.pdf", test);     

            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment(new MemoryStream(test), "FeeReceipt.pdf");
            msgs.Attachments.Add(attachment);
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
            ret = 0;
        }
        return ret;
    }
    static async Task<int> ExecutesErp(string message, string toSendAddress, string Subject, string NAME, string matter, string lbusername, string degree, string intake, string paymode, string amount, string date, string UserNo, string College_Code, string Dcr, string IDNO)
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

            msgs.Body = msgs.Body.Replace("[UA_FULLNAME]", NAME.ToString());
            msgs.Body = msgs.Body.Replace("[MATTER]", matter.ToString());
            msgs.Body = msgs.Body.Replace("[Application ID]", lbusername.ToString());
            msgs.Body = msgs.Body.Replace("[PROGRAM NAMES]", degree.ToString());
            msgs.Body = msgs.Body.Replace("[INTAKE]", intake.ToString());
            msgs.Body = msgs.Body.Replace("[Payment Mode]", paymode.ToString());
            msgs.Body = msgs.Body.Replace("[Payment Amount]", amount.ToString());
            msgs.Body = msgs.Body.Replace("[Payment Date]", date.ToString());

            MemoryStream oAttachment1 = ShowGeneralExportReportForMail("Reports,Academic,FeeReceiptEnrollnment.rpt", "@P_DCRNO=" + Convert.ToString(Dcr) + ",@P_IDNO=" + IDNO.ToString() + ",@P_COLLEGE_CODE=" + College_Code.ToString() + "");
            //MemoryStream oAttachment1 = ShowGeneralExportReportForMailForApplication("Reports,Academic,FeeReceiptApplication.rpt", "@P_USERNO=" + Convert.ToString(UserNo) + ",@P_COLLEGE_CODE=" + College_Code.ToString() + "");
            //MemoryStream oAttachment = ShowGeneralExportReportForMail("Reports,Academic,FeeCollectionReceipt.rpt", "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DCRNO=" + hfddcr.Value + ",@P_IDNO=" + Convert.ToInt32(chk.ToolTip) + "");
            var bytesRpt = oAttachment1.ToArray();
            var fileRpt = Convert.ToBase64String(bytesRpt);
            byte[] test = (byte[])bytesRpt;
            //  msg.AddAttachment("Offerletter.pdf", test);     

            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment(new MemoryStream(test), "FeeReceipt.pdf");
            msgs.Attachments.Add(attachment);
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
            ret = 0;
        }
        return ret;
    }

    protected void lnkNext_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["ds"];
        int pageIndex = 0;
        pageIndex = Convert.ToInt32(dt.Compute("max([PAGENUMBER])", string.Empty));
        if (pageIndex == Convert.ToInt32(dt.Rows[0]["TOTAL_RECORDS"].ToString()))
        {

            lvreg.DataSource = dt;
            lvreg.DataBind();
            //lvregOffline.DataSource = dt;
            //lvregOffline.DataBind();
            ViewState["ds"] = dt;
            objCommon.DisplayMessage(this.Page, "Records not available.", this.Page);
        }
        else
        {
            pageIndex = pageIndex > 0 ? pageIndex - 1 : 0;
            TextBox txtSearch = (TextBox)lvreg.FindControl("txtSearch");
            // DataSet ds = objSC.GetSlotDetails(college_ids, degrees, pageIndex, 1000, txtSearch.Text);
            // DataSet ds = objmp.GETREGLISTONE(Convert.ToInt32(ddlintakeregistration.SelectedValue), Convert.ToInt32(ddlpaystatustwo.SelectedValue), pageIndex, 1000, string.Empty);
            DataSet ds = objmp.GETREGLISTONE(Convert.ToInt32(ddlintakeregistration.SelectedValue), Convert.ToInt32(ddlpaystatustwo.SelectedValue), Convert.ToInt32(ddlstudytwo.SelectedValue), Convert.ToInt32(ddlBankEnrollnment.SelectedValue), pageIndex, 1000, txtSearch.Text);

            if (ds.Tables[0].Rows.Count > 0)
            {

                lvreg.DataSource = ds;
                lvreg.DataBind();
                ViewState["ds"] = ds.Tables[0];
                objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvreg);//Set label 

                Control div2 = lvreg.FindControl("div2");
                DataPager datapager2 = div2.FindControl("datapager2") as DataPager;
                Label lblTotalRecord = datapager2.Controls[0].FindControl("lbltotalcount") as Label;
                lblTotalRecord.Text = ds.Tables[0].Rows.Count.ToString();
            }
        }
    }

    // By Abhijit Niak
    protected void lnkPrevious_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["ds"];
        int pageIndex = 0;
        pageIndex = Convert.ToInt32(dt.Compute("min([PAGENUMBER])", string.Empty));
        if (pageIndex == 1)
        {

            lvreg.DataSource = dt;
            lvreg.DataBind();
            ViewState["ds"] = dt;
            objCommon.DisplayMessage(this.Page, "Records not available.", this.Page);
        }
        else
        {
            pageIndex = pageIndex > 0 ? pageIndex - 1 : 0;
            TextBox txtSearch = (TextBox)lvreg.FindControl("txtSearch");
            // DataSet ds = objSC.GetSlotDetails(college_ids, degrees, pageIndex, 1000, txtSearch.Text);
            DataSet ds = objmp.GETREGLISTONE(Convert.ToInt32(ddlintakeregistration.SelectedValue), Convert.ToInt32(ddlpaystatustwo.SelectedValue), Convert.ToInt32(ddlstudytwo.SelectedValue), Convert.ToInt32(ddlBankEnrollnment.SelectedValue), pageIndex, 1000, txtSearch.Text);

            if (ds.Tables[0].Rows.Count > 0)
            {

                lvreg.DataSource = ds;
                lvreg.DataBind();
                ViewState["ds"] = ds.Tables[0];
                objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvreg);//Set label 

                Control div2 = lvreg.FindControl("div2");
                DataPager datapager2 = div2.FindControl("datapager2") as DataPager;
                Label lblTotalRecord = datapager2.Controls[0].FindControl("lbltotalcount") as Label;
                lblTotalRecord.Text = ds.Tables[0].Rows.Count.ToString();
            }
        }
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["ds"];
        TextBox txtSearch = (TextBox)lvreg.FindControl("txtSearch");
        DataSet ds = objmp.GETREGLISTONE(Convert.ToInt32(ddlintakeregistration.SelectedValue), Convert.ToInt32(ddlpaystatustwo.SelectedValue), Convert.ToInt32(ddlstudytwo.SelectedValue), Convert.ToInt32(ddlBankEnrollnment.SelectedValue), 0, 1000, txtSearch.Text);
        if (ds.Tables[0].Rows.Count > 0)
        {
            //pnlTimeTable.Visible = true;
            lvreg.DataSource = ds;
            lvreg.DataBind();
            ViewState["ds"] = ds.Tables[0];
        }

    }

    //For lvSemster

    //protected void lnkPreviousSem_Click(object sender, EventArgs e)
    //{
    //    string Program = string.Empty; string Branchno = string.Empty;
    //    divlkonlineadm.Attributes.Remove("class");
    //    foreach (ListItem items in ddlProgram.Items)
    //    {
    //        if (items.Selected == true)
    //        {
    //            Program += Convert.ToString(items.Value.Split(',')[0]) + ',';
    //            Branchno += Convert.ToString(items.Value.Split(',')[1]) + ','; ;
    //        }
    //    }

    //    DataTable dt = new DataTable();
    //    dt = (DataTable)ViewState["ds1"];
    //    int pageIndex = 0;
    //    pageIndex = Convert.ToInt32(dt.Compute("min([PAGENUMBER])", string.Empty));
    //    if (pageIndex == 1)
    //    {

    //        lvsemester.DataSource = dt;
    //        lvsemester.DataBind();
    //        ViewState["ds1"] = dt;
    //        objCommon.DisplayMessage(this.Page, "Records not available.", this.Page);
    //    }
    //    else
    //    {
    //        pageIndex = pageIndex > 0 ? pageIndex - 1 : 0;
    //        TextBox txtSearch = (TextBox)lvsemester.FindControl("txtSearchSem");
    //        // DataSet ds = objSC.GetSlotDetails(college_ids, degrees, pageIndex, 1000, txtSearch.Text);
    //        DataSet ds = objmp.GETSEMESTERDATAOFF(Convert.ToInt32(ddlintakethree.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlstudylevelsem.SelectedValue), Convert.ToInt32(ddlstatus.SelectedValue), Convert.ToInt32(ddlBankHigherSem.SelectedValue), Program, Branchno);

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {

    //            lvsemester.DataSource = ds;
    //            lvsemester.DataBind();
    //            ViewState["ds1"] = ds.Tables[0];
    //            objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvsemester);//Set label 

    //            Control div2 = lvreg.FindControl("div3");
    //            DataPager datapager2 = div2.FindControl("datapagerSem") as DataPager;
    //            Label lblTotalRecord = datapager2.Controls[0].FindControl("lbltotalcountSem") as Label;
    //            lblTotalRecord.Text = ds.Tables[0].Rows.Count.ToString();
    //        }
    //    }
    //}
    //protected void lnkNextSem_Click(object sender, EventArgs e)
    //{
    //    string Program = string.Empty; string Branchno = string.Empty;
    //    divlkonlineadm.Attributes.Remove("class");
    //    foreach (ListItem items in ddlProgram.Items)
    //    {
    //        if (items.Selected == true)
    //        {
    //            Program += Convert.ToString(items.Value.Split(',')[0]) + ',';
    //            Branchno += Convert.ToString(items.Value.Split(',')[1]) + ','; ;
    //        }
    //    }

    //    DataTable dt = new DataTable();
    //    dt = (DataTable)ViewState["ds1"];
    //    int pageIndex = 0;
    //    pageIndex = Convert.ToInt32(dt.Compute("max([PAGENUMBER])", string.Empty));
    //    if (pageIndex == Convert.ToInt32(dt.Rows[0]["TOTAL_RECORDS"].ToString()))
    //    {

    //        lvsemester.DataSource = dt;
    //        lvsemester.DataBind();
    //        ViewState["ds1"] = dt;
    //        objCommon.DisplayMessage(this.Page, "Records not available.", this.Page);
    //    }
    //    else
    //    {
    //        pageIndex = pageIndex > 0 ? pageIndex - 1 : 0;
    //        TextBox txtSearch = (TextBox)lvsemester.FindControl("txtSearchSem");
    //        // DataSet ds = objSC.GetSlotDetails(college_ids, degrees, pageIndex, 1000, txtSearch.Text);
    //        // DataSet ds = objmp.GETREGLISTONE(Convert.ToInt32(ddlintakeregistration.SelectedValue), Convert.ToInt32(ddlpaystatustwo.SelectedValue), pageIndex, 1000, string.Empty);


    //        DataSet ds = objmp.GETSEMESTERDATAOFF(Convert.ToInt32(ddlintakethree.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlstudylevelsem.SelectedValue), Convert.ToInt32(ddlstatus.SelectedValue), Convert.ToInt32(ddlBankHigherSem.SelectedValue), Program, Branchno, pageIndex, 1000, txtSearch.Text);

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {

    //            lvsemester.DataSource = ds;
    //            lvsemester.DataBind();
    //            ViewState["ds1"] = ds.Tables[0];
    //            objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvsemester);//Set label 

    //            Control div2 = lvreg.FindControl("div3");
    //            DataPager datapager2 = div2.FindControl("datapagerSem") as DataPager;
    //            Label lblTotalRecord = datapager2.Controls[0].FindControl("lbltotalcountSem") as Label;
    //            lblTotalRecord.Text = ds.Tables[0].Rows.Count.ToString();
    //        }
    //    }

    //}
    //protected void txtSearchSem_TextChanged(object sender, EventArgs e)
    //{
    //    string Program = string.Empty; string Branchno = string.Empty;
    //    divlkonlineadm.Attributes.Remove("class");
    //    foreach (ListItem items in ddlProgram.Items)
    //    {
    //        if (items.Selected == true)
    //        {
    //            Program += Convert.ToString(items.Value.Split(',')[0]) + ',';
    //            Branchno += Convert.ToString(items.Value.Split(',')[1]) + ','; ;
    //        }
    //    }

    //    DataTable dt = new DataTable();
    //    dt = (DataTable)ViewState["ds1"];
    //    TextBox txtSearch = (TextBox)lvreg.FindControl("txtSearchSem");

    //    DataSet ds = objmp.GETSEMESTERDATAOFF(Convert.ToInt32(ddlintakethree.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlstudylevelsem.SelectedValue), Convert.ToInt32(ddlstatus.SelectedValue), Convert.ToInt32(ddlBankHigherSem.SelectedValue), Program, Branchno, 0, 1000, txtSearch.Text);

    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        //pnlTimeTable.Visible = true;
    //        lvsemester.DataSource = ds;
    //        lvsemester.DataBind();
    //        ViewState["ds1"] = ds.Tables[0];
    //    }

    //}

    protected void lvsemester_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        (lvsemester.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["DYNAMIC_DATASET"];
        lvsemester.DataSource = dt;
        lvsemester.DataBind();

    }
    protected void lk_Addmission_Proper_Click(object sender, EventArgs e)
    {
        divl_lk_Addmission_Proper.Attributes.Add("class", "active");
        divlkonlineadm.Attributes.Remove("class");
        divlksemester.Attributes.Remove("class");
        divlkroyality.Attributes.Remove("class");
        DivsLoanScheme.Attributes.Remove("class");

        lvonlineadm.DataSource = null;
        lvonlineadm.DataBind();


        intakeoneroya.Visible = false;
        statusroya.Visible = false;
        bankroya.Visible = false;
        intaketworoya.Visible = false;
        divroyastudy.Visible = false;
        ddlintake.SelectedIndex = 0;
        ddluserselection.SelectedIndex = 0;
        ddlBank.SelectedIndex = 0;
        pnlCourse.Visible = false;
        divroyalty.Visible = false;
        divonlineadm.Visible = false;
        divLoanScheme.Visible = false;
        diverpadm.Visible = true;
        divsemster.Visible = false;
        Panel2.Visible = false;
        LvApplicationCount.DataSource = null;
        LvApplicationCount.DataBind();
        Panel4.Visible = false;
        LvSemesterCount.DataSource = null;
        LvSemesterCount.DataBind();
        Selection.Visible = true;
        registration.Visible = true;
        semselection.Visible = true;
        electroyalty.Visible = true;
    }
    protected void btnShowAddmissionProper_Click(object sender, EventArgs e)
    {
        divlkonlineadm.Attributes.Remove("class");
        BINDADDMISSIONPROPER();
    }

    // added by ekansh moundekar
    private void BINDADDMISSIONPROPER()
    {
        DataSet ds = objmp.GETADDMISSIONPROPERLIST(Convert.ToInt32(ddlintakeregistration.SelectedValue), Convert.ToInt32(ddlpaystatustwo.SelectedValue), Convert.ToInt32(ddlstudytwo.SelectedValue), Convert.ToInt32(ddlBankEnrollnment.SelectedValue), 0, 1000, string.Empty);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            pnlreg.Visible = false;
            pnlregOffline.Visible = true;
            lvregOffline.DataSource = ds;
            lvregOffline.DataBind();


            ViewState["ds"] = ds.Tables[0];
            //Control div2 = lvreg.FindControl("div2");
            //DataPager datapager2 = div2.FindControl("datapager2") as DataPager;
            //Label lblTotalRecord = datapager2.Controls[0].FindControl("lbltotalcount") as Label;
            //lblTotalRecord.Text = ds.Tables[0].Rows.Count.ToString();


            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            ViewState["dcr"] = ds.Tables[0].Rows[0]["DCR_NO"].ToString();
            if (ddlpaystatustwo.SelectedValue == "2")
            {
                btnSubAddProper.Visible = true;
            }
            else
            {
                if ((Convert.ToInt32(Session["userno"].ToString()) == 32 || Convert.ToInt32(Session["userno"].ToString()) == 255 || Convert.ToInt32(Session["userno"].ToString()) == 467 || Convert.ToInt32(Session["userno"].ToString()) == 466 || Convert.ToInt32(Session["userno"].ToString()) == 468) && ddlpaystatustwo.SelectedValue == "3")
                {
                    btnSubAddProper.Visible = true;
                }
                else
                {
                    btnSubAddProper.Visible = false;
                }
            }

        }
        else
        {
            objCommon.DisplayMessage(this.upderpadm, "Record Not Found", this.Page);
            pnlreg.Visible = false;
            pnlregOffline.Visible = false;
            lvreg.DataSource = null;
            lvreg.DataBind();
            lvregOffline.DataSource = null;
            lvregOffline.DataBind();
            btnSubAddProper.Visible = false;

        }

        DataSet FillDropDown = null;
        FillDropDown = objCommon.FillDropDown("ACD_BANK", "DISTINCT BANKNO", "BANK_SHORTNAME", "BANKNO>0", "BANKNO");

        foreach (ListViewDataItem dataitem in lvregOffline.Items)
        {
            DropDownList dstatus = dataitem.FindControl("ddlregstatus") as DropDownList;
            DropDownList ddlbank = dataitem.FindControl("ddlbank") as DropDownList;
            Label lblBankNo = dataitem.FindControl("lblbank") as Label;

            ddlbank.Items.Clear();
            ddlbank.Items.Add("Please Select");
            ddlbank.SelectedItem.Value = "0";

            ddlbank.DataSource = FillDropDown;
            ddlbank.DataTextField = "BANK_SHORTNAME";
            ddlbank.DataValueField = "BANKNO";
            ddlbank.DataBind();

            if (lblBankNo.Text == "0" || lblBankNo.Text == "")
            {
                ddlbank.SelectedValue = "0";
            }
            else
            {
                ddlbank.SelectedValue = lblBankNo.Text.ToString();
            }
            //ddlbank.SelectedValue = lblBankNo.Text.ToString();
            Label lbstatus = dataitem.FindControl("lblregstatus") as Label;
            TextBox txtamt = dataitem.FindControl("txtregamount") as TextBox;
            //LinkButton lnk = dataitem.FindControl("lnkreg") as LinkButton;
            LinkButton lnk = dataitem.FindControl("lnkregOnline") as LinkButton;
            dstatus.SelectedValue = lbstatus.Text;
            lnk.Visible = true;

            if (dstatus.SelectedValue == "1")
            {
                txtamt.Enabled = false;
            }

            else if (dstatus.SelectedValue == "2")
            {
                txtamt.Enabled = true;
            }
        }
    }
    protected void btnSubAddProper_Click(object sender, EventArgs e)
    {
        try
        {
            divlkonlineadm.Attributes.Remove("class");

            string subject = "";
            string matter = "";
            int ua_no = Convert.ToInt32(Session["userno"]);
            CustomStatus cs = 0;
            int Count = 0;
            foreach (ListViewDataItem dataitem in lvregOffline.Items)
            {
                int userno = 0;
                string orderid = string.Empty;
                string amount = string.Empty;
                string date = string.Empty;
                string STATUS = string.Empty;
                string REMARK = string.Empty;
                string EMAIL = "";
                string USERNO = "";
                string NAME = "";
                string paymode = "";
                string intake = "";
                string degree = "";
                CheckBox chkBox = dataitem.FindControl("chreg") as CheckBox;
                TextBox torderid = dataitem.FindControl("txtorder") as TextBox;
                TextBox tamount = dataitem.FindControl("txtregamount") as TextBox;
                TextBox tdate = dataitem.FindControl("txtregdate") as TextBox;
                DropDownList dstatus = dataitem.FindControl("ddlregstatus") as DropDownList;
                DropDownList ddlBank = dataitem.FindControl("ddlbank") as DropDownList;
                TextBox treamrk = dataitem.FindControl("txtregremark") as TextBox;
                Label lbemail = dataitem.FindControl("lblregemail") as Label;
                Label lbregname = dataitem.FindControl("lblregname") as Label;
                Label lblTempDcrNo = dataitem.FindControl("lblDcrTempNo") as Label;
                HiddenField hdfuser = dataitem.FindControl("hdnDocno") as HiddenField;
                Label lbusername = dataitem.FindControl("lblregusername") as Label;
                HiddenField hdpaymod = dataitem.FindControl("hdfpaymod") as HiddenField;
                HiddenField hdintake = dataitem.FindControl("hdfintake") as HiddenField;
                HiddenField hddegree = dataitem.FindControl("hdfdegree") as HiddenField;
                HiddenField hdfdcr = dataitem.FindControl("hdfdcr") as HiddenField;


                if (chkBox.Checked == true)
                {
                    Count++;
                    userno = Convert.ToInt32(chkBox.ToolTip);
                    amount = tamount.Text;
                    date = tdate.Text;
                    STATUS = dstatus.SelectedValue;
                    REMARK = treamrk.Text;
                    EMAIL = lbemail.Text;
                    NAME = lbregname.Text;
                    USERNO = lbusername.Text;
                    paymode = hdpaymod.Value;
                    intake = hdintake.Value;
                    degree = hddegree.Value;
                    if (dstatus.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.updonlineadm, "Please Select Status", this.Page);
                    }
                    else
                    {
                        cs = (CustomStatus)objmp.Insert_AddmissionProper(userno, amount, date, STATUS, REMARK, Convert.ToInt32(lblTempDcrNo.Text), Convert.ToString(ddlBank.SelectedValue), Convert.ToInt32(Session["userno"]));
                        if (dstatus.SelectedValue == "1")
                        {
                            matter = "Approved.";
                        }
                        else if (dstatus.SelectedValue == "2")
                        {
                            matter = "Pending";
                        }
                        else if (dstatus.SelectedValue == "3")
                        {
                            matter = "Rejected";
                        }

                        DataSet dsUserContact = null;
                        string message = "";
                    
                        string dcr = objCommon.LookUp("ACD_DCR_ONLINE", "DCR_NO", "TEMP_DCR_NUMBER=" + Convert.ToString(lblTempDcrNo.Text));

                        if (dstatus.SelectedValue == "1")
                        {
                            EmailSmsWhatssppSendForAddmissionProper(1004, EMAIL, NAME.ToString(), matter.ToString(), lbusername.Text.ToString(), degree.ToString(), intake.ToString(), hdpaymod.ToString(), amount.ToString(), date.ToString(), Convert.ToString(hdfuser.Value), Session["colcode"].ToString(), Convert.ToString(dcr), Convert.ToString(chkBox.ToolTip), "Down Payment");
                            
                            chkBox.Checked = false;

                        }
                    }
                }

            }
            if (Count == 0)
            {
                objCommon.DisplayMessage(this.upderpadm, "Please Select At least One checkbox", this.Page);
                return;
            }
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.upderpadm, "Record saved successfully.", this.Page);
                BINDADDMISSIONPROPER();
            }
            else
            {
                objCommon.DisplayMessage(this.upderpadm, "Server Error", this.Page);
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
    protected void lvreg_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        (lvreg.FindControl("DataPagerReg") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["DYNAMIC_LVREG"];

        lvreg.DataSource = dt;
        lvreg.DataBind();
    }

    protected void lvregOffline_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        (lvregOffline.FindControl("DataPagerReg") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["DYNAMIC_LVREG"];

        lvregOffline.DataSource = dt;
        lvregOffline.DataBind();
    }

    // added by ekansh moundekar
    protected void EmailSmsWhatssppSendForAddmissionProper(int Page_No, string toSendAddre, string Name, string Matter, string UserName, string Degree, string Intake, string Paymode, string Amount, string date, string UserNo, string College_Code, string Dcr, string IDNO, string payment_type)
    {
        EmailSmsWhatsaap Email = new EmailSmsWhatsaap();
        DataSet ds = objCommon.FillDropDown("ACCESS_LINK", "isnull(EMAIL,0) EMAIL,isnull(SMS,0) as SMS,isnull(WHATSAAP,0) as WHATSAAP", "", "AL_NO=" + Convert.ToInt32(Request.QueryString["pageno"].ToString()), "");
        string Res = ""; string Message = "";
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["EMAIL"].ToString()) == 1)//For Email Send 
        {
            int PageNo = Page_No;
            //int PageNo = 33;
            UserController objUC = new UserController();
            DataSet dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", "0", PageNo);
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
                msgsPara.Body = msgsPara.Body.Replace("[UA_FULLNAME]", Name.ToString());
                msgsPara.Body = msgsPara.Body.Replace("[UA_STDID]", UserName.ToString());              
                MemoryStream Attachment = null; string AttachmentName = "FeeReceipt.pdf";
                if (IDNO.ToString() == string.Empty)
                {
                    AttachmentName = string.Empty;
                  //  Attachment = ShowGeneralExportReportForMailForApplication("Reports,Academic,FeeCollectionHallticket.rpt", "@P_USERNO=" + Convert.ToString(UserNo) + ",@P_COLLEGE_CODE=" + College_Code.ToString() + "");
                }
                else
                {
                    Attachment = ShowGeneralOnlineExportReportForMail("Reports,Academic,FeeCollectionReceiptDownPay.rpt", "@P_DCRNO=" + Convert.ToString(Dcr) + ",@P_USERNO=" + UserNo.ToString() + ",@P_COLLEGE_CODE=" + College_Code.ToString() + "");
                }
                Task<string> task = Email.Execute(msgsPara.Body, toSendAddress, Subject, Attachment, AttachmentName.ToString());
                Res = task.Result;
            }
        }      
    }
    protected void rdioregistrationOnline_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdioregistrationOnline.SelectedValue == "1")
        {
            divlkonlineadm.Attributes.Remove("class");
            div1intake2.Visible = true;
            divBankEnrollment.Visible = true;
            div1paystatus2.Visible = true;
            div2intake2.Visible = false;
            // btnshowregone.Visible = true;
            btnshowregone.Visible = false;
            btnShowAddmissionProper.Visible = true;
            btncancelreg.Visible = true;
            btnsubreg.Visible = false;
            btnSubAddProper.Visible = false;
            divstudyleveltwo.Visible = true;
            divLoanScheme.Visible = false;
            btnshowregtwo.Visible = false;
            // BindEnrollmentCount();
            BindAddmissonCount();
            //divfacultytwo.Visible = true;
            lvreg.DataSource = null;
            lvreg.DataBind();
            Panel2.Visible = false;
            LvApplicationCount.DataSource = null;
            LvApplicationCount.DataBind();
            Panel4.Visible = false;
            LvSemesterCount.DataSource = null;
            LvSemesterCount.DataBind();
            pnlreg.Visible = false;
            pnlregOffline.Visible = false;

        }
        else if (rdioregistrationOnline.SelectedValue == "2")
        {
            divlkonlineadm.Attributes.Remove("class");
            div2intake2.Visible = true;
            divBankEnrollment.Visible = false;
            div1intake2.Visible = false;
            pnlreg.Visible = false;
            pnlregOffline.Visible = false;
            div1paystatus2.Visible = false;
            btnshowregtwo.Visible = true;
            divstudyleveltwo.Visible = true;
            btnshowregone.Visible = false;
            btnShowAddmissionProper.Visible = false;
            btncancelreg.Visible = true;
            btnsubreg.Visible = false;
            btnSubAddProper.Visible = false;
            btnSubAddProper.Visible = false;
            divstudyleveltwo.Visible = true;
            divLoanScheme.Visible = false;
            lvreg.DataSource = null;
            lvreg.DataBind();
            Panel2.Visible = false;
            LvApplicationCount.DataSource = null;
            LvApplicationCount.DataBind();
            Panel3.Visible = false;
            LvEnrollmentCount.DataSource = null;
            LvEnrollmentCount.DataBind();
            Panel4.Visible = false;
            LvSemesterCount.DataSource = null;
            LvSemesterCount.DataBind();
            divstudyleveltwo.Visible = true;
        }

    }

    protected void ddlIDChange_SelectedIndexChanged(object sender, EventArgs e)
    {
        int IDtype = Convert.ToInt32(ddlIDChange.SelectedValue);

        if (IDtype == 2)
        {
            rdioregistration.Visible = true;
            rdioregistrationOnline.Visible = false;
            divlkonlineadm.Attributes.Remove("class");
            ddlintakeregistration.SelectedIndex = 0;
            ddlpaystatustwo.SelectedIndex = 0;
            ddlstudytwo.SelectedIndex = 0;
            ddlintakeregtwo.SelectedIndex = 0;
            btnsubreg.Visible = false;
            btnSubAddProper.Visible = false;
            lvreg.DataSource = null;
            rdioregistrationOnline.ClearSelection();
            rdioregistration.ClearSelection();
            lvreg.DataBind();
            LvApplicationCount.DataSource = null;
            LvApplicationCount.DataBind();
            LvEnrollmentCount.DataSource = null;
            LvEnrollmentCount.DataBind();
            Panel3.Visible = false;
            lvregOffline.DataSource = null;
            lvregOffline.DataBind();
            div1intake2.Visible = false;
            divBankEnrollment.Visible = true;
            div1paystatus2.Visible = false;
            div2intake2.Visible = false;
            divstudyleveltwo.Visible = false;
            divBankEnrollment.Visible = false;
            pnlreg.Visible = false;
        }
        else
        {
            rdioregistrationOnline.Visible = true;
            rdioregistration.Visible = false;
            divlkonlineadm.Attributes.Remove("class");
            ddlintakeregistration.SelectedIndex = 0;
            ddlpaystatustwo.SelectedIndex = 0;
            ddlstudytwo.SelectedIndex = 0;
            ddlintakeregtwo.SelectedIndex = 0;
            btnsubreg.Visible = false;
            btnSubAddProper.Visible = false;
            lvreg.DataSource = null;
            rdioregistrationOnline.ClearSelection();
            rdioregistration.ClearSelection();
            lvreg.DataBind();
            LvApplicationCount.DataSource = null;
            LvApplicationCount.DataBind();
            LvEnrollmentCount.DataSource = null;
            LvEnrollmentCount.DataBind();
            Panel3.Visible = false;
            lvregOffline.DataSource = null;
            lvregOffline.DataBind();
            div1intake2.Visible = false;
            divBankEnrollment.Visible = true;
            div1paystatus2.Visible = false;
            div2intake2.Visible = false;
            divstudyleveltwo.Visible = false;
            divBankEnrollment.Visible = false;
            pnlreg.Visible = false;
        }
    }
    protected void lnkregOnline_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkreg = (LinkButton)(sender);
            string img = lnkreg.CommandArgument.ToString();
            string extension = Path.GetExtension(img.ToString());
            string FileName = lnkreg.CommandName.ToString();
            if (extension == ".pdf")
            {
                imageViewerContainer.Visible = false;
                irm1.Visible = true;
                ImageViewer.Visible = false;
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
                CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerNameAdd);

                var ImageName = img;
                if (img != null || img != "")
                {

                    DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerNameAdd, img);

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
                    ScriptManager.RegisterClientScriptBlock(this.updModelPopup, this.GetType(), "Popup", "$('#myModal22').modal('show')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> Swal.fire({html: 'Sorry, File not found !!!', icon: 'warning' });</script>", false);
                }
            }
            else
            {
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
                CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerNameAdd);

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
                    ImageViewer.ImageUrl = string.Format("data:image/png;base64," + Base64String);
                    ScriptManager.RegisterClientScriptBlock(this.updModelPopup, this.GetType(), "Popup", "$('#myModal22').modal('show')", true);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
}