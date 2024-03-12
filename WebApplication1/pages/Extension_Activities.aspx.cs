using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Configuration;
using System.Globalization;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;

public partial class ACADEMIC_Extension_Activities : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();         
    StudentInformation objStudReg = new StudentInformation();
    StudentRegistrationController objStudRegC = new StudentRegistrationController();
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
            // imgPhoto.ImageUrl = "~/images/nophoto.jpg";
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["currentsession"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
//                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //CHECK THE Faculty LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "ua_no=" + Convert.ToInt32(Session["userno"]));
                ViewState["usertype"] = ua_type;
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                string ipaddress = ViewState["ipAddress"].ToString();

                if (ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "1")
                {
                    divExtension.Visible = true;  
                    GetExtensionDetails();
                }
                else
                {
                    divExtension.Visible = false;
                 //   objCommon.DisplayMessage(this, "Sorry Your not Eligible to View this Page", this.Page);
                    Response.Redirect("~/notauthorized.aspx?page=Extension_Activities.aspx");
                }
            }
        }
        
        divMsg.InnerHtml = string.Empty;
    }

    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page.
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Extension_Activities.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Extension_Activities.aspx");
        }
    }
    private void GetExtensionDetails()
    {
        DataSet ds = objStudRegC.GetExtensionActivityDetails();
        ViewState["Table"] = ds;
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvExtension.DataSource = ds;
            lvExtension.DataBind();
            lvExtension.Visible = true;
        }
        else
        {
            lvExtension.DataSource = null;
            lvExtension.DataBind();
            lvExtension.Visible = false;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objStudReg.Activity_Name = txtNameofActivity.Text;
            objStudReg.Award_Name = txtNameofAward.Text;
            objStudReg.Award_GovtName = txtNameofGovtAward.Text;
            if (!txtYearofAwardDate.Text.Trim().Equals(string.Empty)) objStudReg.Award_Date = Convert.ToDateTime(txtYearofAwardDate.Text);
            //objStudReg.Award_Date = Convert.ToDateTime(txtYearofAwardDate.Text);
            objStudReg.Organization_unit = txtOrganizingUnit.Text;
            objStudReg.Scheme_Name = txtSchemeName.Text;
            objStudReg.Student_Participated = Convert.ToInt32(txtStudPar.Text==""?0:Convert.ToInt32(txtStudPar.Text)); //Convert.ToInt32(txtStudPar.Text) == null ? 0 : Convert.ToInt32(txtStudPar.Text); 
            objStudReg.Ua_no = Convert.ToInt32(Session["userno"]);
            objStudReg.Ip_address = ViewState["ipAddress"].ToString();
            objStudReg.College_code = Session["colcode"].ToString();         


            //Added by Naresh For Files Upload

            objStudReg.FileName = "";
            objStudReg.FilePath = "";

            int Activity_no = ViewState["ACTIVITY_NO"] == null ? 0 : Convert.ToInt32(ViewState["ACTIVITY_NO"].ToString());

            if (fuExtensionCertificate.HasFile)
            {
                Check50KBFileSize();

                if (validationflag == true)
                {
                    //to upload multiple sports docs details
                    uploadMultipleSportsCertificate(fuExtensionCertificate, "ExtensionActivity");
                    if (status == 1)
                    {
                        CustomStatus cs = (CustomStatus)objStudRegC.AddExtensionActivityDetails(objStudReg, Activity_no);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(this.Page, "Extension Activity Details Saved Successfully !!!", this.Page);
                            ClearAll();
                            GetExtensionDetails();
                        }
                        else if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage(this.Page, "Extension Activity Details Updated Successfully !!!", this.Page);
                            ClearAll();
                            btnSubmit.Text = "Submit";
                            GetExtensionDetails();
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "Extension Activity Details Not Saved !!!", this.Page);
                        }                        
                    }
                }

            }
            else
            {
                CustomStatus cs = (CustomStatus)objStudRegC.AddExtensionActivityDetails(objStudReg, Activity_no);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.Page, "Extension Activity Details Saved Successfully !!!", this.Page);
                    ClearAll();
                    GetExtensionDetails();
                }
                else if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.Page, "Extension Activity Details Updated Successfully !!!", this.Page);
                    ClearAll();
                    btnSubmit.Text = "Submit";
                    GetExtensionDetails();
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Extension Activity Details Not Saved !!!", this.Page);
                }               
            }            
            //Ends here File Upload Naresh
                       
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Extension_Activities.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #region Upload Documents

    static bool validationflag = true;
    int status = 0;
    static int sportsno = 0;
    string Category = "";

    #region Single File Upload
    ////function to upload single docs
    //private void uploadSportsCertificate(FileUpload funame, string Category)
    //{

    //    try
    //    {
    //        if (objStudReg.IDNO != 0)
    //        {
    //            //string folderPath = WebConfigurationManager.AppSettings["SVCE_STUDENT_CERTIFICATES"].ToString() + txtCCode.Text + "\\";
    //            //string folderPath = WebConfigurationManager.AppSettings["SVCE_STUDENT_CERTIFICATES"].ToString() + objStudReg.IDNO.ToString() + "-" + txtNameOfGameOrAchievement.Text + "\\";
    //            string folderPath = WebConfigurationManager.AppSettings["SVCE_STUDENT_CERTIFICATES"].ToString() + objStudReg.IDNO.ToString() + "-" + Category + "\\";
    //            if (funame.HasFile)
    //            {
    //                string ext = System.IO.Path.GetExtension(funame.PostedFile.FileName);
    //                HttpPostedFile file = funame.PostedFile;
    //                string filename = Path.GetFileName(funame.PostedFile.FileName) + objStudReg.IDNO.ToString() + "-" + Category;
    //                if (ext == ".pdf" || ext == ".png" || ext == ".jpeg" || ext == ".jpg" || ext == ".PNG" || ext == ".JPEG" || ext == ".JPG" || ext == ".PDF") //|| ext == ".doc" || ext == ".docx" 
    //                {
    //                    //if (file.ContentLength <= 51200)// 31457280 before size  //For Allowing 50 Kb Size Files only 
    //                    //{
    //                    if (file.ContentLength <= 102400)// 31457280 before size  //For Allowing 100 Kb Size Files only 
    //                    {
    //                        string contentType = funame.PostedFile.ContentType;
    //                        if (!Directory.Exists(folderPath))
    //                        {
    //                            Directory.CreateDirectory(folderPath);
    //                        }
    //                        objStudReg.ProjectFilename = filename;
    //                        objStudReg.ProjectFilePath = folderPath + filename;
    //                        funame.PostedFile.SaveAs(folderPath + filename);
    //                        status = 1;
    //                    }
    //                    else
    //                    {
    //                        objCommon.DisplayMessage("Document size must not exceed 100 Kb !", this.Page);
    //                        return;
    //                    }
    //                }
    //                else
    //                {
    //                    objCommon.DisplayMessage("Only .jpg,.png,.jpeg,.pdf file type allowed  !", this.Page);
    //                    return;
    //                }

    //            }
    //            else
    //            {

    //                objStudReg.Filename = "";
    //                objStudReg.FilePath = "";
    //            }
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage("Please Refresh the Page again!!", this.Page);
    //            return;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // objCommon.DisplayMessage(this, "Something went wrong ! Please contact Admin", this.Page);

    //    }
    //}
    #endregion

    private void Check50KBFileSize()
    {
        try
        {
            int Activityno=Convert.ToInt32(ViewState["ACTIVITY_NO"].ToString());
            if (Activityno >= 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    //string folderPath = WebConfigurationManager.AppSettings["SVCE_STUDENT_CERTIFICATES"].ToString() + idno.ToString() + "-" + txtNameOfGameOrAchievement.Text + "\\";
                    string folderPath = WebConfigurationManager.AppSettings["SVCE_EXTENSION_CERTIFICATES"].ToString() + "\\";

                    HttpPostedFile fu = Request.Files[i];
                    if (fu.ContentLength > 0)
                    {
                        string ext = System.IO.Path.GetExtension(fu.FileName);
                        string filename = Path.GetFileName(fu.FileName);
                        if (ext == ".pdf" || ext == ".png" || ext == ".jpeg" || ext == ".jpg" || ext == ".PNG" || ext == ".JPEG" || ext == ".JPG" || ext == ".PDF") //|| ext == ".doc" || ext == ".docx" 
                        {
                            //if (fu.ContentLength <= 51200)// 31457280 before size  //For Allowing 50 Kb Size Files only 
                            //{
                            if (fu.ContentLength <= 102400)// 31457280 before size  //For Allowing 100 Kb Size Files only 
                            {
                                validationflag = true;
                            }
                            else
                            {
                                objCommon.DisplayMessage("Document size must not exceed 100 Kb !! Few File Size Large !", this.Page);
                                validationflag = false;
                                return;

                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage("Only .jpg,.png,.jpeg,.pdf file type allowed !", this.Page);
                            validationflag = false;
                            return;
                        }
                    }

                }
                objStudReg.FileName = objStudReg.FileName.TrimEnd(',');
                objStudReg.FilePath = objStudReg.FilePath.TrimEnd(',');
            }
            else
            {
                objCommon.DisplayMessage("Please Refresh the Page again!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            // objCommon.DisplayMessage(this, "Something went wrong ! Please contact Admin", this.Page);

        }
    }

    //function to upload multiple docs
    private void uploadMultipleSportsCertificate(FileUpload fun, string Category)
    {
        try
        {
                //string Regno = lblRegNo.Text;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    //string folderPath = WebConfigurationManager.AppSettings["SVCE_STUDENT_CERTIFICATES"].ToString() + idno.ToString() + "-" + txtNameOfGameOrAchievement.Text + "\\";
                    string folderPath = WebConfigurationManager.AppSettings["SVCE_EXTENSION_CERTIFICATES"].ToString() + "\\" + Convert.ToString(Category).ToString() + "\\";

                    HttpPostedFile fu = Request.Files[i];
                    if (fu.ContentLength > 0)
                    {
                        string ext = System.IO.Path.GetExtension(fu.FileName);
                        string filename = Path.GetFileName(fu.FileName);
                        if (ext == ".pdf" || ext == ".png" || ext == ".jpeg" || ext == ".jpg" || ext == ".PNG" || ext == ".JPEG" || ext == ".JPG" || ext == ".PDF") //|| ext == ".doc" || ext == ".docx" 
                        {
                            if (fu.ContentLength <= 102400)// 31457280 before size  //For Allowing 100 Kb Size Files only 
                            {
                                string contentType = fu.ContentType;
                                if (!Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }
                                objStudReg.FileName += filename + ",";
                                objStudReg.FilePath += folderPath + filename + ",";
                                fu.SaveAs(folderPath + filename);
                                status = 1;
                            }
                            else
                            {
                                objCommon.DisplayMessage("Document size must not exceed 100 Kb !", this.Page);
                                return;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage("Only .jpg,.png,.jpeg,.pdf file type allowed !", this.Page);
                            return;
                        }
                    }

                }
                objStudReg.FileName = objStudReg.FileName.TrimEnd(',');
                objStudReg.FilePath = objStudReg.FilePath.TrimEnd(',');
            
        }
        catch (Exception ex)
        {
            // objCommon.DisplayMessage(this, "Something went wrong ! Please contact Admin", this.Page);

        }
    }

    #endregion


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnSubmit.Text = "Submit";
        ClearAll();
    }
    private void ClearAll()
    {
        txtNameofActivity.Text = string.Empty;
        txtNameofAward.Text = string.Empty;
        txtNameofGovtAward.Text = string.Empty;
        txtOrganizingUnit.Text = string.Empty;
        txtStudPar.Text = string.Empty;
        txtSchemeName.Text = string.Empty;
        txtYearofAwardDate.Text = string.Empty;        
        ViewState["ACTIVITY_NO"]=0;
    }
    protected void btnExtensionDelete_Click(object sender,EventArgs e)
    {
        try
        {
            int recno = 0;
            ImageButton btn = sender as ImageButton;
            ViewState["ACTIVITY_NO"] = Convert.ToInt32(btn.CommandArgument);
            recno = Convert.ToInt32(ViewState["ACTIVITY_NO"].ToString());
            int res = objStudRegC.DeleteExtensionActivity(Convert.ToInt32(ViewState["ACTIVITY_NO"].ToString()));
            if (res == 3)
            {
                objCommon.DisplayMessage(this, "Extension Details Deleted Successfully", this.Page);
                ClearAll();
                GetExtensionDetails();
            }
            else
            {
                objCommon.DisplayMessage(this, "Extension Details Unable Delete", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Extension_Activities.btnExtensionDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEditforExtension_Click(object sender, EventArgs e)
    {
        try
        {
            int recno = 0;
            ImageButton btn = sender as ImageButton;
            ViewState["ACTIVITY_NO"] = Convert.ToInt32(btn.CommandArgument);
            recno = Convert.ToInt32(ViewState["ACTIVITY_NO"].ToString());
            DataSet ds = objStudRegC.GetExtensionActivityDetails();
            DataRow[] foundrow = ds.Tables[0].Select("ACTIVITYNO=" + ViewState["ACTIVITY_NO"].ToString());
            if (foundrow.Length > 0)
            {
                DataTable newTable = foundrow.CopyToDataTable();
                if (newTable.Rows.Count > 0)
                {
                    txtNameofActivity.Text = newTable.Rows[0]["ACTIVITY_NAME"].ToString();
                    txtNameofAward.Text = newTable.Rows[0]["AWARD_NAME"].ToString();
                    txtNameofGovtAward.Text = newTable.Rows[0]["AWARD_GOVT_NAME"].ToString();
                    txtYearofAwardDate.Text = newTable.Rows[0]["AWARD_DATE"].ToString();
                    txtOrganizingUnit.Text = newTable.Rows[0]["ORGANIZING_NAME"].ToString();
                    txtSchemeName.Text = newTable.Rows[0]["SCHEME_NAME"].ToString();
                    txtStudPar.Text = newTable.Rows[0]["STUDENT_PARTICIPATED"].ToString();
                    btnSubmit.Text = "Update";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Extension_Activities.btnEditforExtension_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        Delete(sender, "ExtensionActivity", Convert.ToInt32(btn.CommandArgument));        
        GetExtensionDetails();
    }

    private void Delete(object sender, string category, int DocNo)
    {
        int Docno = 0;
        int ret = 0;
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            Docno = Convert.ToInt32(btnDelete.CommandArgument);
            if (Docno != 0)
            {

               
                string filename = btnDelete.AlternateText;

                string path = WebConfigurationManager.AppSettings["SVCE_EXTENSION_CERTIFICATES"].ToString() + "\\" + category.ToString() + "\\";
                string DOCS_NO = string.Empty;

                if (File.Exists(path + filename))
                {
                    File.Delete(path + filename);

                    if (category == "ExtensionActivity")
                    {
                        ret = objStudRegC.DeleteExtensionActivityDocs(DocNo);
                    }                    
                    if (ret == 3)
                    {
                        objCommon.DisplayMessage(this, "File Deleted Successfully", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "File Not Found to Delete", this.Page);
                    }
                }

            }
            else
            {
                objCommon.DisplayMessage("Please Refresh the Page again!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "domain.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDownloadFile_Click(object sender, EventArgs e)
    {
        download(sender, "ExtensionActivity");
    }
    private void download(object sender, string category)
    {
        LinkButton btndownloadfile = sender as LinkButton;
        string projectname = (btndownloadfile.CommandArgument);        
        //   string category = "Project";
      
        if (projectname != ""||projectname!=string.Empty)
        {
            string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).ToolTip.ToString();
            string ContentType = string.Empty;

            //To Get the physical Path of the file(test.txt)
                        
            string filepath = WebConfigurationManager.AppSettings["SVCE_EXTENSION_CERTIFICATES"].ToString() + "\\" + category.ToString() + "\\";

            // Create New instance of FileInfo class to get the properties of the file being downloaded
            FileInfo myfile = new FileInfo(filepath + filename);
            string file = filepath + filename;

            string ext = Path.GetExtension(filename);
            // Checking if file exists
            if (myfile.Exists)
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.TransmitFile(file);
                Response.Flush();
                Response.End();
            }
        }
        else
        {
            objCommon.DisplayMessage("Please Refresh the Page again!!", this.Page);
            return;
        }
    }

    protected void lvExtension_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Label lblActivityName = (Label)e.Item.FindControl("lblActivityName");
            ListView lvActivityDetails = (ListView)e.Item.FindControl("lvActivityDetails");

            DataSet dsCERT = objStudRegC.GetExtensionActivityDocs(Convert.ToInt32(lblActivityName.ToolTip));
            if (dsCERT.Tables[0].Rows.Count > 0 && dsCERT != null)
            {
                lvActivityDetails.DataSource = dsCERT.Tables[0];
                lvActivityDetails.DataBind();
            }
            else
            {                
                lvActivityDetails.DataSource = null;
                lvActivityDetails.DataBind();
            }
        }
    }
}