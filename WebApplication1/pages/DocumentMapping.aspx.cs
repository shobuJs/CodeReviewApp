//======================================================================================
// PROJECT NAME  : SVCE                                                          
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : Document Mapping                                                   
// CREATION DATE : 10/7/2019                                                       
// CREATED BY    : SatishT                                                     
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;
using System.IO;




public partial class ACADEMIC_DocumentMapping : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    MappingController objmp = new MappingController();
    UAIMS_Common objUCommon = new UAIMS_Common();

    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

    string DepartNos = string.Empty;
    //USED FOR INITIALSING THE MASTER PAGE
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    //USED FOR BYDEFAULT LOADING THE default PAGE
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
                this.CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }

            }
            objCommon.FillDropDownList(ddlcurreentintake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0 AND COLLEGE_ID IN (" + Session["college_nos"].ToString() + ")", "COLLEGE_ID");

            ViewState["action"] = "add";
        }
        BindListView();       // Binding List View..
        objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); // Add By Roshan Pannase On 21-07-2021
        objCommon.SetLabelData(ddlCollege.SelectedValue);
    }
    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page. 
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DocumentMapping.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DocumentMapping.aspx");
        }
    }
    #endregion

    //button Submit is used to Submit the Degree name with Document name.
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            byte[] ChallanCopy = null; string[] docname; string[] docnames;
            string docsno = string.Empty;
            int mandatory = 0;
            CustomStatus ck = 0;
            int documentno = 0;
            string DegreeNos = string.Empty;
            string path = "", filenames = "", docnos = string.Empty, existsfile = ""; int Count = 0;

            docsno = hdndocsno.Value;
            DegreeNos = hdndegreeno.Value;
            DegreeNos = DegreeNos.TrimEnd('$');
            
                if ((DegreeNos) == "")//GetDegreeNew()
                {
                    objCommon.DisplayMessage(this.updGradeEntry, "Please select at least one Degree !", this.Page);
                    return;
                }
                else
                {
                    foreach (ListItem items in ddlDegree.Items)
                    {
                        string[] degree; string[] degre1;
                        if (items.Selected == true)
                        {
                            degree = items.Value.ToString().Split('$');
                            degre1 = degree[0].ToString().Split(',');
                            foreach (ListViewDataItem item in lvDocument.Items)
                            {
                                Label docno = item.FindControl("lblDocNo") as Label;
                                Label DocName = item.FindControl("lblname") as Label;
                                CheckBox chkBox = item.FindControl("chkadm") as CheckBox;
                                CheckBox chkall = item.FindControl("chkall") as CheckBox;
                                string documentname = DocName.Text.ToString();
                                documentname = documentname.Replace(" ", "_");

                                FileUpload fuDocument = item.FindControl("fuDocument") as FileUpload;
                                if (chkall.Checked == true)
                                {
                                    if (chkBox.Checked == true)
                                    {
                                        mandatory = 1;
                                    }
                                    else
                                    {
                                        mandatory = 0;
                                    }
                                    if (fuDocument.HasFile)
                                    {
                                        HttpPostedFile FileSize = fuDocument.PostedFile;
                                        string ext = System.IO.Path.GetExtension(fuDocument.FileName);
                                        if (ext == ".pdf" || ext == ".PDF")
                                        {
                                            if (FileSize.ContentLength <= 1000000)
                                            {
                                                ChallanCopy = objCommon.GetImageData(fuDocument);
                                                string Filenames = docno.Text + "_" + documentname + "_" + Convert.ToString(ddlCollege.SelectedValue) + "_" + Convert.ToString(degre1[0]) + "-" + Convert.ToString(degre1[1]) + "-" + Convert.ToString(degre1[2]);
                                                int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, Filenames, fuDocument, ChallanCopy);
                                                if (retval == 0)
                                                {
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                                    return;
                                                }

                                                filenames += Filenames + ext;
                                                existsfile += Filenames + ext + ',';
                                                docnos += Convert.ToString(docno.Text) + ',';
                                            }
                                        }
                                    }
                                    documentno = Convert.ToInt32(docno.Text);

                                    if (docnos != "")
                                    {
                                        docname = docnos.TrimEnd(',').Split(',');
                                        docnames = existsfile.TrimEnd(',').Split(',');
                                        for (int i = 0; i < docname.Length; i++)
                                        {
                                            if (documentno == Convert.ToInt32(docname[i]))
                                            {
                                                filenames = docnames[i];
                                                break;
                                            }
                                            else
                                            {
                                                filenames = "";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        filenames = "";
                                    }


                                    ck = (CustomStatus)objmp.AddDocumentMapping(Convert.ToInt32(documentno), Convert.ToInt32(degre1[0]), Convert.ToInt32(degre1[1]), Convert.ToInt32(degre1[2]), Convert.ToInt32(ddlCollege.SelectedValue), filenames, Convert.ToInt32(ddlcurreentintake.SelectedValue), mandatory);
                                    //chkall.Checked = false;
                                    //chkBox.Checked = false;
                                }
                            }
                        }
                    }
               if (ck.Equals(CustomStatus.RecordSaved))
               {
                   objCommon.DisplayMessage(this.updGradeEntry, "Record Saved Successfully", this.Page);
                   docsno = "";
                   BindListView();
                   // this.bindDegreeDocs();
                   Clear();
                   return;
               }
               //else 
               //{
               //    objCommon.DisplayMessage(this.updGradeEntry, "Record Update Successfully", this.Page);
               //    docsno = "";
               //    BindListView();
               //    // this.bindDegreeDocs();
               //    Clear();
               //    return;
               //}
               else
               {
                   objCommon.DisplayMessage(this.updGradeEntry, "Please Select At least One checkbox", this.Page);
               }
           }
        }
        catch { }

        //int ck = objmp.AddDocument(Convert.ToInt32(ddlDocument.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));
        //if (ck == 1)
        //{
        //    objCommon.DisplayMessage(this.updGradeEntry, "Record Saved Sucessfully", this.Page);
        //    BindListView();
        //    return;
        //}
        //else
        //{
        //    objCommon.DisplayMessage(this.updGradeEntry, "Record already Exist", this.Page);
        //    return;
        //}
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

    //used for reset the Degree name and Document name 
    protected void Clear()
    {
        ddlCollege.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlDocument.SelectedIndex = 0;
        ddlcurreentintake.SelectedIndex = 0;
    }

    //used for to delete the Degree name and Document name from data base and not Displaying im list view.
    protected void btnDel_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnDel = sender as ImageButton;
        ViewState["action"] = "delete";
        int srno = int.Parse(btnDel.CommandArgument);
        int output = objmp.deleteDocument(srno);
        if (output != -99 && output != 99)
        {
            objCommon.DisplayMessage(this.updGradeEntry, "Record Deleted Successfully", this.Page);
            Clear();
        }
        else
        {
            objCommon.DisplayMessage(this.updGradeEntry, "Record Is Not Deleted ", this.Page);
            Clear();
        }
        // }
        BindListView();
    }

    //displaying the Degree and Document data  in list view.
    private void BindListView()
    {
        try
        {
           // DataSet ds = objCommon.FillDropDown("ACD_DOC_DEGREE D INNER JOIN ACD_DEGREE B ON (D.DEGREENO=B.DEGREENO) INNER JOIN ACD_BRANCH BA ON(D.BRANCHNO = BA.BRANCHNO) INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON (D.AFFILIATED_NO = AU.AFFILIATED_NO) INNER JOIN  ACD_DOCUMENT_LIST  T ON (T.DOCUMENTNO=D.DOCUMENTTYPENO AND ISNULL(PTYPE,0)=1) INNER JOIN ACD_COLLEGE_MASTER CM ON(D.COLLEGE_ID = CM.COLLEGE_ID) INNER JOIN ACD_ADMBATCH AA ON AA.BATCHNO = D.ADMBATCH", "D.DOC_DEGREE_NO,D.DEGREENO,D.BRANCHNO,D.AFFILIATED_NO,CASE WHEN D.MANDATORY=1 THEN 'YES' ELSE 'NO' END AS MANDATORY", "(CM.SHORT_NAME + '-'+ B.DEGREENAME + '-' + LONGNAME + '-' + AFFILIATED_SHORTNAME) DEGREENAME,T.DOCUMENTNAME,D.DOC_FILENAME,BATCHNAME", string.Empty, "D.DEGREENO,D.BRANCHNO,D.DOCUMENTTYPENO");
            DataSet ds = objCommon.FillDropDown("ACD_DOC_DEGREE D INNER JOIN ACD_DEGREE B ON (D.DEGREENO=B.DEGREENO) INNER JOIN ACD_BRANCH BA ON(D.BRANCHNO = BA.BRANCHNO) INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON (D.AFFILIATED_NO = AU.AFFILIATED_NO) INNER JOIN  ACD_DOCUMENT_LIST  T ON (T.DOCUMENTNO=D.DOCUMENTTYPENO AND ISNULL(PTYPE,0)=1) INNER JOIN ACD_COLLEGE_MASTER CM ON(D.COLLEGE_ID = CM.COLLEGE_ID) INNER JOIN ACD_ADMBATCH AA ON AA.BATCHNO = D.ADMBATCH", "D.DOC_DEGREE_NO,D.DEGREENO,D.BRANCHNO,D.AFFILIATED_NO,CASE WHEN D.MANDATORY=1 THEN 'YES' ELSE 'NO' END AS MANDATORY", "(CM.SHORT_NAME + '-'+ B.DEGREENAME + '-' + LONGNAME + '-' + AFFILIATED_SHORTNAME) DEGREENAME,T.DOCUMENTNAME,D.DOC_FILENAME,BATCHNAME", string.Empty, "D.DEGREENO,D.BRANCHNO,D.DOCUMENTTYPENO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvlist.DataSource = ds;
                lvlist.DataBind();
                lvlist.Visible = true;
                
            }
            else
            {
                lvlist.DataSource = null;
                lvlist.DataBind();
                lvlist.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_degree.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    ////refresh current page or reload current page
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO>0", "DOCUMENTNO");
    }
    //bind degree document name in list view.
    private void bindDegreeDocs()
    {
        try
        {
         //   DataSet ds = objCommon.FillDropDown("ACD_DOC_DEGREE D INNER JOIN ACD_DEGREE B ON (D.DEGREENO=B.DEGREENO) INNER JOIN ACD_BRANCH BA ON(D.BRANCHNO = BA.BRANCHNO) INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON (D.AFFILIATED_NO = AU.AFFILIATED_NO) INNER JOIN  ACD_DOCUMENT_LIST T ON (T.DOCUMENTNO=D.DOCUMENTTYPENO) INNER JOIN ACD_ADMBATCH AA ON AA.BATCHNO = D.ADMBATCH", "D.DOC_DEGREE_NO,D.DEGREENO,D.BRANCHNO,D.AFFILIATED_NO", "B.DEGREENAME,T.DOCUMENTNAME,AA.BATCHNAME,CASE WHEN D.MANDATORY=1 THEN 'YES' ELSE 'NO' END AS MANDATORY", "D.DEGREENO=" + ddlDegree.SelectedValue, "D.DEGREENO,D.BRANCHNO,D.DOCUMENTTYPENO");
            DataSet ds = objCommon.FillDropDown("ACD_DOC_DEGREE D INNER JOIN ACD_DEGREE B ON (D.DEGREENO=B.DEGREENO) INNER JOIN ACD_BRANCH BA ON(D.BRANCHNO = BA.BRANCHNO) INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON (D.AFFILIATED_NO = AU.AFFILIATED_NO) INNER JOIN  ACD_DOCUMENT_LIST T ON (T.DOCUMENTNO=D.DOCUMENTTYPENO) INNER JOIN ACD_ADMBATCH AA ON AA.BATCHNO = D.ADMBATCH", "D.DOC_DEGREE_NO,D.DEGREENO,D.BRANCHNO,D.AFFILIATED_NO", "B.DEGREENAME,T.DOCUMENTNAME,AA.BATCHNAME, D.MANDATORY", "D.DEGREENO=" + ddlDegree.SelectedValue, "D.DEGREENO,D.BRANCHNO,D.DOCUMENTTYPENO");
          
            lvlist.DataSource = null;
            lvlist.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvlist.DataSource = ds;
                lvlist.DataBind();
                lvlist.Visible = true;
            }
            else
            {
                lvlist.DataSource = null;
                lvlist.DataBind();
                lvlist.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_degree.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillListBox(ddlDegree, "ACD_COLLEGE_MASTER CM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO) INNER JOIN ACD_BRANCH BA ON (CD.BRANCHNO = BA.BRANCHNO) INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON (CD.AFFILIATED_NO = AU.AFFILIATED_NO)", "(CONVERT(NVARCHAR(5),CD.DEGREENO) + ','+ CONVERT(NVARCHAR(5),CD.BRANCHNO) + ',' + CONVERT(NVARCHAR(5),CD.AFFILIATED_NO))", "(D.DEGREENAME + ' - ' + LONGNAME + ' - ' + AFFILIATED_SHORTNAME)", "CD.COLLEGE_ID=" + ddlCollege.SelectedValue, "CD.DEGREENO");
            //objCommon.FillListBox(ddlDegree, "ACD_COLLEGE_MASTER CM INNER JOIN ACD_COLLEGE_DEGREE CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO)", "CD.DEGREENO", "D.DEGREENAME", "CM.COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENO");
            objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO>0", "DOCUMENTNO");
            DataSet ds = new DataSet();

            ds = objmp.GetDoclist(Convert.ToInt32(ddlcurreentintake.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue),0,0);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvDocument.DataSource = ds;
                lvDocument.DataBind();
                lvDocument.Visible = true;
            }
        }
        else
        {
            ddlCollege.Focus();
            ddlDegree.SelectedIndex = 0;
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
                
                ltEmbed.Visible = false;
               
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
            }
            else
            {
               
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
                    ViewState["filePath_Show"] = filePath;
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);

                    byte[] bytes = System.IO.File.ReadAllBytes(filePath);

                    string Base64String = Convert.ToBase64String(bytes);
                    Session["Base64String"] = Base64String;
                    Session["Base64StringType"] = "application/pdf";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
                    //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"700px\" height=\"400px\">";
                    //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    //embed += "</object>";
                    //ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                    /////ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "$('#myModal22').modal()", true);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
                }
                else
                {
                    objCommon.DisplayMessage("Sorry, File not found !!!", this.Page);
                }
                
            }
        }
        catch (Exception ex)
        {

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
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int doc_degreeno = int.Parse(btnEdit.CommandArgument);
            
            HiddenField1.Value = int.Parse(btnEdit.CommandArgument).ToString();
            ShowDetails(doc_degreeno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_CollegeMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int doc_degreeno)
    {
        string sDegree = string.Empty;
        try
        {
            byte[] imgData = null;

            string degree = string.Empty;
            string branch = string.Empty;
            string department = string.Empty;
            DataSet ds = objCommon.FillDropDown("ACD_DOC_DEGREE", "*", "", "DOC_DEGREE_NO=" + doc_degreeno, "DOC_DEGREE_NO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                
                objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO>0", "DOCUMENTNO");
                DataSet ds1 = new DataSet();


                ddlcurreentintake.SelectedValue = ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
                string intake = ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                objCommon.FillListBox(ddlDegree, "ACD_COLLEGE_MASTER CM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO) INNER JOIN ACD_BRANCH BA ON (CD.BRANCHNO = BA.BRANCHNO) INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON (CD.AFFILIATED_NO = AU.AFFILIATED_NO)", "(CONVERT(NVARCHAR(5),CD.DEGREENO) + ','+ CONVERT(NVARCHAR(5),CD.BRANCHNO) + ',' + CONVERT(NVARCHAR(5),CD.AFFILIATED_NO))", "(D.DEGREENAME + ' - ' + LONGNAME + ' - ' + AFFILIATED_SHORTNAME)", "CD.COLLEGE_ID=" + ddlCollege.SelectedValue, "CD.DEGREENO");
                string degreeno = ds.Tables[0].Rows[0]["DEGREENO"].ToString() + ',' + ds.Tables[0].Rows[0]["BRANCHNO"].ToString() + ',' + ds.Tables[0].Rows[0]["AFFILIATED_NO"].ToString();
                //ddlDegree.SelectedValue = degreeno;
                //string[] degNo = new string[50];
                //degNo = degreeno.Split(','); 

                ddlDegree.SelectedValue = degreeno;
                string[] values = degreeno.Split(',');

                // Store the values in different variables
                 degree = values[0];
                 branch = values[1];
                 string affiliatedNo = intake;

                ds1 = objmp.GetDoclist(Convert.ToInt32(ddlcurreentintake.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(degree), Convert.ToInt32(branch));
                if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                {
                    lvDocument.DataSource = ds1;
                    lvDocument.DataBind();
                    lvDocument.Visible = true;
                }


                //for (int i = 0; i < degNo.Length; i++)
                //{
                //    for (int j = 0; j < ddlDegree.Items.Count; j++)
                //    {
                //        if (ddlDegree.Items[j].Value.ToString() == degNo[i]) //.Split(',')[100]
                //        {
                //            ddlDegree.Items[j].Selected = true;
                //        }
                //    }

                //}
                //if (!degNo.ToString().Equals(string.Empty))
                //{

                //}

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Master_CollegeMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //protected void lvDocument_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    ListViewDataItem dataItem = (ListViewDataItem)e.Item;

    //    if (e.Item.ItemType == ListViewItemType.DataItem)
    //    {
    //        CheckBox chkSub = (CheckBox)e.Item.FindControl("chkall");
    //        CheckBox chkadm = (CheckBox)e.Item.FindControl("chkadm");

    //        string subval = chkSub.Text;
    //        string admval = chkadm.Text;

    //        //if (subval.ToString() == "1")
    //        //{
    //        //    chkSub.Checked = true;
    //        //}
    //        //else {
    //        //    chkSub.Checked = false;
    //        //}

    //        if (chkadm.ToString() == "1")
    //        {
    //            chkadm.Checked = true;
    //        }
    //        else
    //        {
    //            chkadm.Checked = false;
    //        }

    //    }
    //}
}