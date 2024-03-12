using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using IITMS.UAIMS;
using System.Web.Services;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using BusinessLogicLayer.BusinessEntities;
using DynamicAL_v2;
using System.IO;
using System.Web.Configuration;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using BusinessLogicLayer.BusinessLogic.Academic;

public partial class Projects_Module_Outline : System.Web.UI.Page
{
    Common objCommon = new Common();

    ModuleOutline_Controller ObjMOC = new ModuleOutline_Controller();
    acd_module_outline ObjMO = new acd_module_outline();

    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

    int userno = 0; int deptno = 0; int MODULEOUTLINEID = 0;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            hdnUserNo.Value = Session["userno"].ToString();
            int IDNO = Convert.ToInt32(Session["idno"]);
            if (!Page.IsPostBack)
            {
                //int deptno = Convert.ToInt32(Request.QueryString["deptno"]);
                FilldropDown();
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
                    BindListViewModuleOutline();
                }

                ViewState["action"] = "add";
                ViewState["MODULEOUTLINEID"] = "0";
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_ModuleOutline.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Module_Outline.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Module_Outline.aspx");
        }
    }

    private void FilldropDown()
    {
        //objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT STD INNER JOIN ACD_COURSE SP ON STD.DEPTNO = SP.DEPTNO INNER JOIN ACD_OFFERED_COURSE C ON (C.COURSENO=SP.COURSENO)", "DISTINCT STD.DEPTNO", "STD.DEPTNAME", "", "");
        objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "", "");
        //objCommon.FillDropDownList(ddlModule, "ACD_COURSE STD INNER JOIN ACD_OFFERED_COURSE SP ON STD.COURSENO = SP.COURSENO ", "DISTINCT STD.COURSENO", "STD.COURSE_NAME", "", "");
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
     {
        int DEPTNO = Convert.ToInt32(ddlDepartment.SelectedValue);

        objCommon.FillDropDownList(ddlModule, "ACD_COURSE", "COURSENO", "(CCODE+'-'+course_name) as course_name", "courseno in (select courseno from ACD_COURSE where DEPTNO = DEPTNO) AND DEPTNO =" + DEPTNO + "", "");
         //objCommon.FillDropDownList(ddlModule, "ACD_COURSE", "COURSENO", "(CCODE+'-'+course_name) as course_name", "courseno in (select courseno from ACD_OFFERED_COURSE where modulelic = modulelic) AND DEPTNO =" + DEPTNO + "", "");
        //objCommon.FillDropDownList(ddlModule, "ACD_COURSE STD INNER JOIN ACD_OFFERED_COURSE SP ON STD.COURSENO = SP.COURSENO ", "DISTINCT STD.COURSENO", "STD.COURSE_NAME", "", "");        
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

     protected void btnSubmit_Click(object sender, EventArgs e)
     {
        try
        {
            ObjMO.userid = Convert.ToInt32(Session["userno"].ToString());
            ObjMO.outline = txtModuleOutline.Text.Replace(",", "^").Replace("MyLRMar", "").Replace("badge", "");
            //ObjMO.with_effect_from = DateTime.Now;
            ObjMO.with_effect_from = Convert.ToDateTime(dtpEffectFromDate.Text);
            ObjMO.deptno = Convert.ToInt32(ddlDepartment.SelectedValue);
            ObjMO.courseno = Convert.ToInt32(ddlModule.SelectedValue);
            //ObjMO.CCode = string.Empty;


            byte[] imgData;

            if (fuUploadOutlineDocument.HasFile)
            {
                imgData = objCommon.GetImageData(fuUploadOutlineDocument);
                ObjMO.document_path = imgData.ToString();
           
            string filename_DOCUMENT_PATH = Path.GetFileName(fuUploadOutlineDocument.PostedFile.FileName);
           
            
            byte[] ChallanCopy = null;

            ChallanCopy = objCommon.GetImageData(fuUploadOutlineDocument);
            string Ext = Path.GetExtension(fuUploadOutlineDocument.FileName);
            int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, Convert.ToInt32(Session["userno"]) + "_ERP_" + "_0_" + "UploadOutlineDocument_File", fuUploadOutlineDocument, ChallanCopy);
            if (retval == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                return;
            }

            ObjMO.document_path = Convert.ToInt32(Session["userno"]) + "_ERP_" + "_0_" + "UploadOutlineDocument_File" + Ext;
            }
            else
            {
                ObjMO.document_path = "";
            }
        //else
        //{
        //      ObjMO.document_path=null;
        //}
            //uploadApprovalDocument();
            //uploadApprovalFile();0

            if (ViewState["action"].ToString() == "add")
            {
                MODULEOUTLINEID = 0;

            }
            else
            {
                MODULEOUTLINEID = 1;
                ObjMO.moduleoutlineid = Convert.ToInt32(ViewState["MODULEOUTLINEID"].ToString());


            }
            //int ret = ObjMOC.AddPDPModuleOutline(ObjMO, Convert.ToInt32(ViewState["MODULEOUTLINEID"].ToString()));
            int ret = ObjMOC.AddPDPModuleOutline(ObjMO, MODULEOUTLINEID);
            if (ret == 1 && MODULEOUTLINEID == 0)
            {
                objCommon.DisplayMessage(this, "Submit Data successfully !", this.Page);
                BindListViewModuleOutline();
                ClearControls();
            }

            else if (ret == 2 && MODULEOUTLINEID > 0)
            {
                objCommon.DisplayMessage(this, "Updated Data Successfully !", this.Page);
                BindListViewModuleOutline();
                ClearControls();
            }
            else
            {

                objCommon.DisplayMessage(this, "Failed To submit Details !!", this.Page);
            }

        }
       
        
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Module_Outline.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }
   

    protected void BindListViewModuleOutline()
    {
      
        DataSet ds = objCommon.FillDropDown("ACD_MODULE_OUTLINE A INNER JOIN ACD_DEPARTMENT B  ON A.DEPTNO = B.DEPTNO INNER JOIN ACD_COURSE C ON A.COURSENO=C.COURSENO  INNER JOIN USER_ACC D ON (A.CREATED_BY = D.UA_NO ) ", "MODULEOUTLINEID", "B.DEPTNAME,OUTLINE,WITH_EFFECT_FROM,DOCUMENT_PATH,D.UA_FULLNAME AS CREATED_BY,(C.CCODE+'-'+ course_name) as COURSE_NAME", "", "");

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvModuleOutline.DataSource = ds;
            lvModuleOutline.DataBind();
        }
        else
        {
            lvModuleOutline.DataSource = null;
            lvModuleOutline.DataBind();
        }

    }

    protected void ClearControls()
    {
        ddlModule.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        txtModuleOutline.Text = string.Empty;
        //lblUploadOutlineDocument.Text = string.Empty;
        ViewState["action"] = "add";

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }





    protected void btnEdit_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int MODULEOUTLINEID = int.Parse(btnEdit.CommandArgument);
            ViewState["MODULEOUTLINEID"] = int.Parse(btnEdit.CommandArgument);
            //Label1.Text = string.Empty;
            //HiddenField1.Value = int.Parse(btnEdit.CommandArgument).ToString();
            ShowDetails(MODULEOUTLINEID);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Module_Outline.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowDetails(int MODULEOUTLINEID)
    {
        try
        {
            //int rowIndex = 1;

            DataSet ds = ObjMOC.GetModuleOutline(MODULEOUTLINEID);

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {

                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["DEPTNO"].ToString();
                ddlDepartment_SelectedIndexChanged(new object(), new EventArgs());
                ddlModule.SelectedValue = ds.Tables[0].Rows[0]["COURSENO"].ToString();
                txtModuleOutline.Text = ds.Tables[0].Rows[0]["OUTLINE"].ToString();
                dtpEffectFromDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WITH_EFFECT_FROM"].ToString()).ToString("dd-MM-yyyy");
                //lblUploadOutlineDocument.Text = ds.Tables[0].Rows[0]["DOCUMENT_PATH"].ToString();
              
            }
        }
        
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Module_Outline.ShowDetails_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }

    }
    protected void btnDownloadDocumentPath_Click1(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnDocumentPath = sender as LinkButton;
            string FileName = btnDocumentPath.CommandArgument.ToString();
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
            
            string img = btnDocumentPath.CommandArgument.ToString();
            string extension = Path.GetExtension(img.ToString());
            var ImageName = img;
            if (img != null || img != "")
            {
                if (extension == ".pdf")
                {
                    DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

                    var Newblob = blobContainer.GetBlockBlobReference(ImageName);

                    string filePath = directoryPath + "\\" + ImageName;


                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);

                    Response.Clear();
                    Response.ClearHeaders();

                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                    Response.TransmitFile(filePath);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

                    var Newblob = blobContainer.GetBlockBlobReference(ImageName);

                    string filePath = directoryPath + "\\" + ImageName;


                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);

                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ContentType = "image/jpeg";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                    Response.TransmitFile(filePath);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Module_Outline.btnDownloadDocumentPath_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
