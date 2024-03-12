//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADAMIC
// PAGE NAME     : BULK PHOTO UPLOADED                                                    
// CREATION DATE :                                                      
// ADDED BY      : ASHISH DHAKATE 
// ADDED DATE    : 16-DEC-2011                                                      
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.IO;
using System.Web.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Web.Services;
using System.Web.Script.Services;
using System.Text;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using DynamicAL_v2;
using _NVP;
using System.Collections.Specialized;
using EASendMail;
using System.Drawing;
//using System.Drawing.Image;
using System.Drawing.Drawing2D;



public partial class ACADEMIC_Acd_Update_Photo_Student : System.Web.UI.Page
{
    IITMS.UAIMS.Common objCommon = new IITMS.UAIMS.Common();
    IITMS.UAIMS_Common objUCommon = new IITMS.UAIMS_Common();
    StudentController objstudent = new StudentController();
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
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

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    this.FillSchemeSemester();
                butSubmit.Visible = false;
            }
            objCommon.SetLabelData("0");//for label
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header

        }
        divMsg.InnerHtml = string.Empty;

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Acd_Update_Photo_Student.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Acd_Update_Photo_Student.aspx");
        }
    }

    private void BindListViewList(int admbatch,int college_id,int degreeno, int branchno,int semesterno)
    {
        try
        {
            DataSet ds = objstudent.GetStudentsForUpdateBulkPhotoUpload(admbatch, college_id, degreeno, branchno, semesterno);

            if (ds.Tables[0].Rows.Count > 0)
            {
                butSubmit.Visible = true;
                lvUpdatePhoto.DataSource = ds;
                lvUpdatePhoto.DataBind();
            }
            else
            {
                butSubmit.Visible = false;
                lvUpdatePhoto.DataSource = null;
                lvUpdatePhoto.DataBind();
                objCommon.DisplayMessage(this.Page, "Record Not Found!", this.Page);
            }
           for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                HiddenField hididno = lvUpdatePhoto.Items[i].FindControl("hididno") as HiddenField;
                System.Web.UI.WebControls.Image ImgPhoto = lvUpdatePhoto.Items[i].FindControl("ImgPhoto") as System.Web.UI.WebControls.Image;
                ImgPhoto.ImageUrl = "~/showimage.aspx?id=" + ds.Tables[0].Rows[i]["IDNO"].ToString() + "&type=student";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Acd_Update_Photo_Student.BindListViewList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butShow_Click(object sender, EventArgs e)
    {
        string[] program;
        if (ddldegree.SelectedValue == "0")
        {
            program = "0,0".Split(',');
        }
        else
        {
            program = ddldegree.SelectedValue.Split(',');
        }
        this.BindListViewList(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlfaculty.SelectedValue), Convert.ToInt32(program[0]),Convert.ToInt32(program[1]), Convert.ToInt32(ddlsemester.SelectedValue));
        butSubmit.Visible = true;
    }

    protected void butSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            byte[] ChallanCopy = null;
            string docname = "";
            string[] program;
            if (ddldegree.SelectedValue == "0")
            {
                program = "0,0".Split(',');
            }
            else
            {
                program = ddldegree.SelectedValue.Split(',');
            }
            foreach (ListViewDataItem lvitem in lvUpdatePhoto.Items)
            {
                HiddenField hididno = lvitem.FindControl("hididno") as HiddenField;
                FileUpload fuStudPhoto = lvitem.FindControl("fuStudPhoto") as FileUpload;
                byte[] image;
                if (fuStudPhoto.HasFile)
                {                  
                    image = ResizePhoto(fuStudPhoto);
                    string contentType = contentType = fuStudPhoto.PostedFile.ContentType;
                    string ext = System.IO.Path.GetExtension(fuStudPhoto.PostedFile.FileName);
                    HttpPostedFile file = fuStudPhoto.PostedFile;
                    docname = hididno.Value + "_ERP_" + "Bulk_Photo" + ext;
                    if (ext == ".pdf" || ext == ".PDF" || ext == ".jpg" || ext == ".JPG" || ext == ".png" || ext == ".PNG" || ext == ".jpeg" || ext == ".JPEG")
                    {
                        //int retval = Blob_Upload(blob_ConStr, blob_ContainerName, hididno.Value + "_doc_" + "Bulk_Photo" + "", fuStudPhoto);
                        //if (retval == 0)
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                        //    return;
                        //}
                        ChallanCopy = objCommon.GetImageData(fuStudPhoto);
                        int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, hididno.Value + "_ERP_" + "Bulk_Photo", fuStudPhoto, image);
                        if (retval == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                            return;
                        }
                    }

                }
                else
                {
                    image = null;
                }

                CustomStatus cs = (CustomStatus)objstudent.UpdateBulkStudentPhoto(Convert.ToInt32(hididno.Value), image, docname);
            }
            this.BindListViewList(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlfaculty.SelectedValue), Convert.ToInt32(program[0]), Convert.ToInt32(program[1]), Convert.ToInt32(ddlsemester.SelectedValue));
            objCommon.DisplayMessage(this.Page,"Record Updated Successfully", this);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Acd_Update_Photo_Student.butSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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
    public byte[] ResizePhoto(FileUpload fu)
    {
        byte[] image = null;
        if (fu.PostedFile != null && fu.PostedFile.FileName != "")
        {
            string strExtension = System.IO.Path.GetExtension(fu.FileName);

            // Resize Image Before Uploading to DataBase
            System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(fu.PostedFile.InputStream as Stream);
            int imageHeight = imageToBeResized.Height;
            int imageWidth = imageToBeResized.Width;
            int maxHeight = 240;
            int maxWidth = 320;
            imageHeight = (imageHeight * maxWidth) / imageWidth;
            imageWidth = maxWidth;

            if (imageHeight > maxHeight)
            {
                imageWidth = (imageWidth * maxHeight) / imageHeight;
                imageHeight = maxHeight;
            }

            //Saving image to smaller size and converting in byte[]
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageToBeResized, imageWidth, imageHeight);
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            image = new byte[stream.Length + 1];
            stream.Read(image, 0, image.Length);
        }
        return image;
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
        catch (Exception) 
        { 
        }
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
    protected void FillSchemeSemester()
    {
        try
        {
            objCommon.FillDropDownList(ddlfaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "ISNULL(ACTIVE,0)=1", "COLLEGE_NAME");
            objCommon.FillDropDownList(ddlAdmbatch, "acd_admbatch", "batchno", "batchname", "batchno>0", "batchname desc");
            objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO ASC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Acd_Update_Photo_Student.FillSchemeSemester()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddldegree.SelectedIndex = 0;
        ddlAdmbatch.SelectedIndex = 0;
        lvUpdatePhoto.DataSource = null;
        lvUpdatePhoto.DataBind();
        pnlUpdatePhoto.Visible = false;
    }
    protected void butReport_Click1(object sender, EventArgs e)
    {
        try
        {
            ShowReport("BULK_PHOTO_UPLOAD", "StudentPhotos.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Acd_Update_Photo_Student.butReport_Click1()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string[] program;
            if (ddldegree.SelectedValue == "0")
            {
                program = "0,0".Split(',');
            }
            else
            {
                program = ddldegree.SelectedValue.Split(',');
            }
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
            //string url = "http://localhost:59566/PresentationLayer/Reports/CommonReport.aspx?";
            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + program[0] + ",@P_BRANCHNO=" + program[1] + ",@P_ADMBATCH=" + ddlAdmbatch.SelectedValue + ",@P_COLLEGE_ID=" + ddlfaculty.SelectedValue + ",@P_SEMESTERNO=" + ddlsemester.SelectedValue;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlfaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddldegree, "ACD_COLLEGE_DEGREE_BRANCH S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=S.BRANCHNO)", "DISTINCT CONCAT(S.DEGREENO,',',S.BRANCHNO)AS ID", "(D.DEGREENAME +'-'+ B.LONGNAME)AS DEGBRANCH", "S.COLLEGE_ID=" + Convert.ToInt32(ddlfaculty.SelectedValue) + "", "ID");
    }
}
