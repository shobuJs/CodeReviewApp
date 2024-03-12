using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
//using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;
using System.Web;
using IITMS.UAIMS;
using IITMS;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using Microsoft.Win32;
using System.Net.Security;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.Web.Services;
using System.Web.Script.Services;
public partial class AdvisingDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string username = string.Empty;
    UserController user = new UserController();
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
    string blob_ContainerNamePhoto = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNamePhoto"].ToString();
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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {

                    }
                    ViewState["RemoveAdditional"] = 0;
                    ViewState["RemoveExempted"] = 0;
                    StudentDetailsBind(Convert.ToInt32(Request.QueryString["IDNO"]));
                    Session["PreSubjectListNew"] = null;
                    objCommon.SetLabelData("");
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACDEMIC_COLLEGE_MASTER.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CollegeMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CollegeMaster.aspx");
        }
    }

    protected void BindListView()
    {
       
        DataSet ds1 = null;
        string SP_Name2 = "PKG_GET_ADVISING_TRANSFEREES_STUDENT";
        string SP_Parameters2 = "@P_IDNO,@P_COMMAND_TYPE";
        string Call_Values2 = "" + ViewState["IDNO"] + "," + 2 + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (ds.Tables[0].Rows.Count > 0 || ds.Tables[0].Rows.Count == null)
        {
            if (ds.Tables[1].Rows.Count > 0 || ds.Tables[1].Rows.Count == null)
            {
                if (ViewState["RemoveExempted"].ToString() == "1")
                {
                    DataTable dt = new DataTable("retired");
                    dt.Columns.Add("PSubjectCode");
                    dt.Columns.Add("PSubjectName");
                    dt.Columns.Add("PUnits");
                    dt.Columns.Add("PGrade");
                    dt.Columns.Add("PCurriculumSubject");
                    dt.Columns.Add("PCourseNo");
                    dt.Columns.Add("PEquSubNo");

                    DataRow dr = dt.NewRow();
                    int Count = 0;
                    foreach (ListViewDataItem dataitem in lvExemptedfromStudy.Items)
                    {
                        Label PSubjectCode = dataitem.FindControl("lblpCode") as Label;
                        Label PSubjectName = dataitem.FindControl("lblpCourseName") as Label;
                        Label PUnits = dataitem.FindControl("lblpUnit") as Label;
                        Label PGrade = dataitem.FindControl("lblpGrade") as Label;
                        Label PCurriculumSubject = dataitem.FindControl("lblCourseNameNew") as Label;
                        Label PCourseNo = dataitem.FindControl("lblCourseNo") as Label;
                        Label PEquSubNo = dataitem.FindControl("lblPrev_No") as Label;

                        dt.Rows.Add(PSubjectCode.Text, PSubjectName.Text, PUnits.Text, PGrade.Text, PCurriculumSubject.Text, PCourseNo.Text, Convert.ToInt32(PEquSubNo.Text));
                        Count = Convert.ToInt32(PEquSubNo.Text);

                    }
                    ds1.Tables.Add(dt);
                    ds1.Merge(ds.Tables[1]);
                    lvExemptedfromStudy.DataSource = ds1;
                    lvExemptedfromStudy.DataBind();
                }
                else
                {
                    lvExemptedfromStudy.DataSource = ds.Tables[1];
                    lvExemptedfromStudy.DataBind();
                }

                objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvExemptedfromStudy);
            }
            else
            {

                if (ViewState["RemoveExempted"].ToString() == "1")
                {
                    DataTable dt = new DataTable("retired");
                    dt.Columns.Add("PSubjectCode");
                    dt.Columns.Add("PSubjectName");
                    dt.Columns.Add("PUnits");
                    dt.Columns.Add("PGrade");
                    dt.Columns.Add("PCurriculumSubject");
                    dt.Columns.Add("PCourseNo");
                    dt.Columns.Add("PEquSubNo");

                    DataRow dr = dt.NewRow();
                    int Count = 0;
                    foreach (ListViewDataItem dataitem in lvExemptedfromStudy.Items)
                    {
                        Label PSubjectCode = dataitem.FindControl("lblpCode") as Label;
                        Label PSubjectName = dataitem.FindControl("lblpCourseName") as Label;
                        Label PUnits = dataitem.FindControl("lblpUnit") as Label;
                        Label PGrade = dataitem.FindControl("lblpGrade") as Label;
                        Label PCurriculumSubject = dataitem.FindControl("lblCourseNameNew") as Label;
                        Label PCourseNo = dataitem.FindControl("lblCourseNo") as Label;
                        Label PEquSubNo = dataitem.FindControl("lblPrev_No") as Label;

                        dt.Rows.Add(PSubjectCode.Text, PSubjectName.Text, PUnits.Text, PGrade.Text, PCurriculumSubject.Text, PCourseNo.Text, Convert.ToInt32(PEquSubNo.Text));
                        Count = Convert.ToInt32(PEquSubNo.Text);

                    }
                    ds1.Tables.Add(dt);
                    ds1.Merge(ds.Tables[1]);
                    if (ds1.Tables[0].Rows.Count > 0 || ds.Tables[0].Rows.Count == null)
                    {
                        lvExemptedfromStudy.DataSource = ds1;
                        lvExemptedfromStudy.DataBind();
                    }
                    else
                    {
                        lvExemptedfromStudy.DataSource = null;
                        lvExemptedfromStudy.DataBind();
                    }
                }
                else
                {
                    lvExemptedfromStudy.DataSource = null;
                    lvExemptedfromStudy.DataBind();
                }

                objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvExemptedfromStudy);

            }

            if (ds.Tables[2].Rows.Count > 0 || ds.Tables[2].Rows.Count == null)
            {
                if (ViewState["RemoveAdditional"].ToString() == "1")
                {

                    DataTable dt = new DataTable("retired");
                    dt.Columns.Add("PSubjectCode");
                    dt.Columns.Add("PSubjectName");
                    dt.Columns.Add("PUnits");
                    dt.Columns.Add("PGrade");
                    dt.Columns.Add("PEquSubNo");

                    DataRow dr = dt.NewRow();
                    int Count = 0;
                    foreach (ListViewDataItem dataitem in lvAdditional.Items)
                    {

                        Label PSubjectCode = dataitem.FindControl("lblpAddiCode") as Label;
                        Label PSubjectName = dataitem.FindControl("lblpAddiCourseName") as Label;
                        Label PUnits = dataitem.FindControl("lblpAddiUnit") as Label;
                        Label PGrade = dataitem.FindControl("lblpAddiGrade") as Label;
                        Label PEquSubNo = dataitem.FindControl("lblAddNo") as Label;

                        dt.Rows.Add(PSubjectCode.Text, PSubjectName.Text, PUnits.Text, PGrade.Text, Convert.ToInt32(PEquSubNo.Text));
                        Count = Convert.ToInt32(PEquSubNo.Text);

                    }

                    ds1.Tables.Add(dt);
                    ds1.Merge(ds.Tables[2]);

                    lvAdditional.DataSource = ds;
                    lvAdditional.DataBind();

                }
                else
                {
                    lvAdditional.DataSource = ds.Tables[2];
                    lvAdditional.DataBind();
                }
                objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvAdditional);
            }
            else
            {
                if (ViewState["RemoveAdditional"].ToString() == "1")
                {

                    DataTable dt = new DataTable("retired");
                    dt.Columns.Add("PSubjectCode");
                    dt.Columns.Add("PSubjectName");
                    dt.Columns.Add("PUnits");
                    dt.Columns.Add("PGrade");
                    dt.Columns.Add("PEquSubNo");

                    DataRow dr = dt.NewRow();
                    int Count = 0;
                    foreach (ListViewDataItem dataitem in lvAdditional.Items)
                    {

                        Label PSubjectCode = dataitem.FindControl("lblpAddiCode") as Label;
                        Label PSubjectName = dataitem.FindControl("lblpAddiCourseName") as Label;
                        Label PUnits = dataitem.FindControl("lblpAddiUnit") as Label;
                        Label PGrade = dataitem.FindControl("lblpAddiGrade") as Label;
                        Label PEquSubNo = dataitem.FindControl("lblAddNo") as Label;

                        dt.Rows.Add(PSubjectCode.Text, PSubjectName.Text, PUnits.Text, PGrade.Text, Convert.ToInt32(PEquSubNo.Text));
                        Count = Convert.ToInt32(PEquSubNo.Text);

                    }

                    ds1.Tables.Add(dt);
                    ds1.Merge(ds.Tables[2]);

                    lvAdditional.DataSource = ds;
                    lvAdditional.DataBind();

                    if (ds1.Tables[0].Rows.Count > 0 || ds.Tables[0].Rows.Count == null)
                    {
                        lvAdditional.DataSource = ds1;
                        lvAdditional.DataBind();

                    }
                    else
                    {
                        lvAdditional.DataSource = null;
                        lvAdditional.DataBind();
                    }


                }
                else
                {
                    lvAdditional.DataSource = null;
                    lvAdditional.DataBind();
                }
                objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvAdditional);

            }

        }

    }
    protected void StudentDetailsBind(int Idno)
    {
        try
        {
            ViewState["COURSE_REGISTERSTATUS"] = 0;
            ViewState["IDNO"] = Idno;
            string SP_Name2 = "PKG_GET_ADVISING_TRANSFEREES_STUDENT";
            string SP_Parameters2 = "@P_IDNO,@P_COMMAND_TYPE";
            string Call_Values2 = "" + Idno + "," + 2 + "";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (ds.Tables[0].Rows.Count > 0 || ds.Tables[0].Rows.Count == null)
            {
                BindStudentName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                BindStdID.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                BindDOB.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                BindGender.Text = ds.Tables[0].Rows[0]["GENDER"].ToString();
                BindMobNo.Text = ds.Tables[0].Rows[0]["MOBILE"].ToString();

                BindEmailID.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                BindProgram.Text = ds.Tables[0].Rows[0]["PROGRAM"].ToString();
                BindSem.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                if (ds.Tables[0].Rows[0]["REMARK"].ToString() != string.Empty)
                {
                    if (ds.Tables[0].Rows[0]["EQUIV_STATUS"].ToString() == "1" && ds.Tables[0].Rows[0]["COURSE_REGISTER"].ToString() == "0")
                    {
                        chkNotExempted.Checked = true;
                        chkNotExempted_CheckedChanged(new object(), new EventArgs());
                        chkNotExempted.Enabled = true;
                    }
                    else
                    {
                        chkNotExempted.Enabled = false;
                    }
                    txtRemark.Enabled = false;
                }
                else
                {
                    chkNotExempted.Enabled = true;
                    txtRemark.Enabled = true;
                }
                if (ds.Tables[0].Rows[0]["COURSE_REGISTER"].ToString() != "0")
                {
                    ViewState["COURSE_REGISTERSTATUS"] = 1;
                    btnSubmit.Enabled = false;
                }
                if (ds.Tables[0].Rows[0]["PHOTOPATH"].ToString() != string.Empty)
                {
                    try
                    {
                        StudentPhoto(ds.Tables[0].Rows[0]["PHOTOPATH"].ToString());
                    }
                    catch
                    {
                    }
                }

                BindPreviousSchool.Text = ds.Tables[0].Rows[0]["PREVIOUS_SCHOOL"].ToString();
                objCommon.FillDropDownList(ddlCurriculumSubject, "ACD_COURSE_MAPPING CM INNER JOIN ACD_COURSE C ON (C.COURSENO=CM.COURSENO)", "DISTINCT C.COURSENO", "C.CCODE+' - '+ COURSE_NAME AS COURSENAME", "CM.SCHEMENO=" + ds.Tables[0].Rows[0]["SCHEMENO"].ToString(), "");
                BindListView();

            }
            else
            {

                objCommon.DisplayMessage(this, "Record not Found..", this.Page);
                return;

            }
        }
        catch { }
    }
    protected void ClearPSubject()
    {
        txtPreSubCode.Text = "";
        txtPreSubName.Text = "";
        txtUnits.Text = "";
        txtGrade.Text = "";
        ddlCurriculumSubject.SelectedValue = "0";
    }
    protected void ClearASubject()
    {
        txtPreSubCodeAdd.Text = "";
        txtPreSubNameAdd.Text = "";
        txtUnitsAdd.Text = "";
        txtGradeAdd.Text = "";
    }
    protected void btnAddSubject_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("retired");
            dt.Columns.Add("PSubjectCode");
            dt.Columns.Add("PSubjectName");
            dt.Columns.Add("PUnits");
            dt.Columns.Add("PGrade");
            dt.Columns.Add("PCurriculumSubject");
            dt.Columns.Add("PCourseNo");
            dt.Columns.Add("PEquSubNo");

            DataRow dr = dt.NewRow();
            int Count = 0;
            foreach (ListViewDataItem dataitem in lvExemptedfromStudy.Items)
            {
                Label PSubjectCode = dataitem.FindControl("lblpCode") as Label;
                Label PSubjectName = dataitem.FindControl("lblpCourseName") as Label;
                Label PUnits = dataitem.FindControl("lblpUnit") as Label;
                Label PGrade = dataitem.FindControl("lblpGrade") as Label;
                Label PCurriculumSubject = dataitem.FindControl("lblCourseNameNew") as Label;
                Label PCourseNo = dataitem.FindControl("lblCourseNo") as Label;
                Label PEquSubNo = dataitem.FindControl("lblPrev_No") as Label;
                dt.Rows.Add(PSubjectCode.Text, PSubjectName.Text, PUnits.Text, PGrade.Text, PCurriculumSubject.Text, PCourseNo.Text, Convert.ToInt32(PEquSubNo.Text));
                Count = Convert.ToInt32(PEquSubNo.Text);

                if (PSubjectCode.Text == txtPreSubCode.Text || PSubjectName.Text == txtPreSubName.Text)//|| PCourseNo.Text == ddlCurriculumSubject.SelectedValue
                {
                    ClearPSubject();
                    objCommon.DisplayMessage(this, "Previous Subject Code Or  Previous Subject Name Already Exists..", this.Page);
                    return;
                }

            }
            dt.Rows.Add(txtPreSubCode.Text.Trim(), txtPreSubName.Text.Trim(), txtUnits.Text.Trim(), txtGrade.Text.Trim(), ddlCurriculumSubject.SelectedItem.Text, ddlCurriculumSubject.SelectedValue, Count + 1);

            ds.Tables.Add(dt);
            Session["PreSubjectListNew"] = ds;
            lvExemptedfromStudy.DataSource = ds;
            lvExemptedfromStudy.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvExemptedfromStudy);
            ClearPSubject();
        }
        catch { }
    }

    protected void btnAdditionalSubj_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("retired");
            dt.Columns.Add("PSubjectCode");
            dt.Columns.Add("PSubjectName");
            dt.Columns.Add("PUnits");
            dt.Columns.Add("PGrade");
            //dt.Columns.Add("PCurriculumSubject");
            //dt.Columns.Add("PCourseNo");
            dt.Columns.Add("PEquSubNo");

            DataRow dr = dt.NewRow();
            int Count = 0;
            foreach (ListViewDataItem dataitem in lvAdditional.Items)
            {

                Label PSubjectCode = dataitem.FindControl("lblpAddiCode") as Label;
                Label PSubjectName = dataitem.FindControl("lblpAddiCourseName") as Label;
                Label PUnits = dataitem.FindControl("lblpAddiUnit") as Label;
                Label PGrade = dataitem.FindControl("lblpAddiGrade") as Label;
                Label PEquSubNo = dataitem.FindControl("lblAddNo") as Label;
                //Label PCurriculumSubject = dataitem.FindControl("lblCourseNameNew") as Label;
                //Label PCourseNo = dataitem.FindControl("lblCourseNo") as Label;
                dt.Rows.Add(PSubjectCode.Text, PSubjectName.Text, PUnits.Text, PGrade.Text, Convert.ToInt32(PEquSubNo.Text));
                Count = Convert.ToInt32(PEquSubNo.Text);
                if (PSubjectCode.Text == txtPreSubCodeAdd.Text || PSubjectName.Text == txtPreSubNameAdd.Text)
                {
                    ClearASubject();
                    objCommon.DisplayMessage(this, "Subject Alredy Exists..", this.Page);
                    return;
                }

            }
            dt.Rows.Add(txtPreSubCodeAdd.Text.Trim(), txtPreSubNameAdd.Text.Trim(), txtUnitsAdd.Text.Trim(), txtGradeAdd.Text.Trim(), Count + 1);

            ds.Tables.Add(dt);
            Session["PreSubjectListNew"] = ds;
            lvAdditional.DataSource = ds;
            lvAdditional.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvAdditional);
            ClearASubject();
        }
        catch { }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CourseController objCC = new CourseController();
        string PSCode = string.Empty; string PSName = string.Empty;
        string P_Units = string.Empty; string P_Grade = string.Empty;
        string CourseNo = string.Empty; string Pidno = string.Empty;
        string Prev_no = string.Empty;

        string ASCode = string.Empty; string ASName = string.Empty;
        string A_Units = string.Empty; string A_Grade = string.Empty;
        string ACuSubject = string.Empty; string Aidno = string.Empty;
        string Addit_no = string.Empty;
        int Count1 = 0;
        int Count2 = 0;
        string Remark = txtRemark.Text;
        int NotExempted = 1;
        if (chkNotExempted.Checked == false)
        {
            NotExempted = 0;
            foreach (ListViewDataItem dataitem in lvExemptedfromStudy.Items)
            {
                Count1++;
                Label PSubjectCode = dataitem.FindControl("lblpCode") as Label;
                Label PSubjectName = dataitem.FindControl("lblpCourseName") as Label;
                Label PUnits = dataitem.FindControl("lblpUnit") as Label;
                Label PGrade = dataitem.FindControl("lblpGrade") as Label;
                Label PCurriculumSubject = dataitem.FindControl("lblCourseNameNew") as Label;
                Label PCourseNo = dataitem.FindControl("lblCourseNo") as Label;
                Label PEquSubNo = dataitem.FindControl("lblPrev_No") as Label;
                PSCode += PSubjectCode.Text + "$";
                PSName += PSubjectName.Text + "$";
                P_Units += PUnits.Text + "$";
                P_Grade += PGrade.Text + "$";
                CourseNo += PCourseNo.Text + "$";
                Pidno += ViewState["IDNO"].ToString() + "$";
                Prev_no += PEquSubNo.Text + "$";
            }
            if (Count1 == 0)
            {
                objCommon.DisplayMessage(this, "Please Add At List One Credited Subject", this.Page);
                return;
            }
            PSCode = PSCode.TrimEnd('$');
            PSName = PSName.TrimEnd('$');
            P_Units = P_Units.TrimEnd('$');
            P_Grade = P_Grade.TrimEnd('$');
            CourseNo = CourseNo.TrimEnd('$');
            Pidno = Pidno.TrimEnd('$');
            Prev_no = Prev_no.TrimEnd('$');

            foreach (ListViewDataItem dataitem in lvAdditional.Items)
            {
                Count2++;
                Label ASubjectCode = dataitem.FindControl("lblpAddiCode") as Label;
                Label ASubjectName = dataitem.FindControl("lblpAddiCourseName") as Label;
                Label AUnits = dataitem.FindControl("lblpAddiUnit") as Label;
                Label AGrade = dataitem.FindControl("lblpAddiGrade") as Label;
                Label PEquSubNo = dataitem.FindControl("lblAddNo") as Label;
                ASCode += ASubjectCode.Text + "$";
                ASName += ASubjectName.Text + "$";
                A_Units += AUnits.Text + "$";
                A_Grade += AGrade.Text + "$";
                Aidno += ViewState["IDNO"].ToString() + "$";
                //  Addit_no += Count2 + "$";
                Addit_no += PEquSubNo.Text + "$";

            }
            ASCode = ASCode.TrimEnd('$');
            ASName = ASName.TrimEnd('$');
            A_Units = A_Units.TrimEnd('$');
            A_Grade = A_Grade.TrimEnd('$');
            Aidno = Aidno.TrimEnd('$');
            Addit_no = Addit_no.TrimEnd('$');
        }
        else { Pidno = ViewState["IDNO"].ToString(); }
        CustomStatus cs = (CustomStatus)objCC.InsertAdvisingDetails(Pidno, PSCode, PSName, P_Units, P_Grade, CourseNo, Prev_no, Aidno, ASCode, ASName, A_Units, A_Grade, Addit_no, Remark, Convert.ToInt32(Session["userno"]), NotExempted);
        if (cs.Equals(CustomStatus.RecordUpdated))
        {
            ViewState["RemoveAdditional"] = 0;
            ViewState["RemoveExempted"] = 0;
            StudentDetailsBind(Convert.ToInt32(ViewState["IDNO"].ToString()));
            objCommon.DisplayMessage(this, "Record saved Successfully.", this.Page);
            return;
        }
        else
        {
            objCommon.DisplayMessage(this, "Error in Saving", this.Page);
        }
    }
    protected void btncanceldata_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
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
    protected void StudentPhoto(string FileName)
    {
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

        if (FileName != null || FileName != "")
        {
            var Newblob = blobContainer.GetBlockBlobReference(FileName);
            string filePath = directoryPath + "\\" + FileName;
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
            imgPhoto.ImageUrl = string.Format("data:image/png;base64," + Base64String);
        }
    }




    protected void btnRemove_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton STUD = sender as LinkButton;
            int CourseNo = int.Parse(STUD.CommandArgument);
            int PrevNo = int.Parse(STUD.CommandName);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("retired");
            dt.Columns.Add("PSubjectCode");
            dt.Columns.Add("PSubjectName");
            dt.Columns.Add("PUnits");
            dt.Columns.Add("PGrade");
            dt.Columns.Add("PCurriculumSubject");
            dt.Columns.Add("PCourseNo");
            dt.Columns.Add("PEquSubNo");


            int Status = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_EQUIVALENCE", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString()) + "AND NEW_COURSENO=" + CourseNo + "AND ISNULL(PREV_NO,0)=" + PrevNo + "AND ISNULL(CANCEL,0)=0"));
            if (Status != 0)
            {
                if (ViewState["COURSE_REGISTERSTATUS"].ToString() == "0")
                {
                    string SP_Name1 = "PR_ACD_CANCEL_ADVISING_STUDENT_DETAILS";
                    string SP_Parameters1 = "@P_IDNO,@P_COURSENO,@P_PREV_ADDIT_NO,@P_COMMAND_TYPE,@P_OUT";
                    string Call_Values1 = "" + Convert.ToInt32(ViewState["IDNO"].ToString()) + "," + CourseNo + "," + PrevNo + "," + 1 + "," + ",0";
                    string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//insert
                    if (que_out1 == "1")
                    {
                        DataRow dr = dt.NewRow();
                        int Count = 0;
                        foreach (ListViewDataItem dataitem in lvExemptedfromStudy.Items)
                        {
                            Label PSubjectCode = dataitem.FindControl("lblpCode") as Label;
                            Label PSubjectName = dataitem.FindControl("lblpCourseName") as Label;
                            Label PUnits = dataitem.FindControl("lblpUnit") as Label;
                            Label PGrade = dataitem.FindControl("lblpGrade") as Label;
                            Label PCurriculumSubject = dataitem.FindControl("lblCourseNameNew") as Label;
                            Label PCourseNo = dataitem.FindControl("lblCourseNo") as Label;
                            Label PEquSubNo = dataitem.FindControl("lblPrev_No") as Label;
                            //if (Convert.ToInt32(PCourseNo.Text) != CourseNo && Convert.ToInt32(PEquSubNo.Text) != PrevNo)
                            if (Convert.ToInt32(PEquSubNo.Text) != PrevNo)
                            {
                                dt.Rows.Add(PSubjectCode.Text, PSubjectName.Text, PUnits.Text, PGrade.Text, PCurriculumSubject.Text, PCourseNo.Text, Convert.ToInt32(PEquSubNo.Text));
                                Count = Convert.ToInt32(PEquSubNo.Text);
                            }
                        }
                        ds.Tables.Add(dt);
                        lvExemptedfromStudy.DataSource = ds;
                        lvExemptedfromStudy.DataBind();
                        ViewState["RemoveExempted"] = 1;
                        StudentDetailsBind(Convert.ToInt32(ViewState["IDNO"].ToString()));
                        objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvExemptedfromStudy);
                        ViewState["RemoveExempted"] = 0;
                        // BindListView();
                        objCommon.DisplayMessage(this, "Exempted Curriculum Subject Remove Successfully..", this.Page);
                        return;
                    }
                }
                else
                {

                    objCommon.DisplayMessage(this, "Subject Registration already Completed Subject Should Not Be Removed..", this.Page);
                    return;
                }
            }
            else
            {


                DataRow dr = dt.NewRow();
                int Count = 0;
                foreach (ListViewDataItem dataitem in lvExemptedfromStudy.Items)
                {
                    Label PSubjectCode = dataitem.FindControl("lblpCode") as Label;
                    Label PSubjectName = dataitem.FindControl("lblpCourseName") as Label;
                    Label PUnits = dataitem.FindControl("lblpUnit") as Label;
                    Label PGrade = dataitem.FindControl("lblpGrade") as Label;
                    Label PCurriculumSubject = dataitem.FindControl("lblCourseNameNew") as Label;
                    Label PCourseNo = dataitem.FindControl("lblCourseNo") as Label;
                    Label PEquSubNo = dataitem.FindControl("lblPrev_No") as Label;
                    //if (Convert.ToInt32(PCourseNo.Text) != CourseNo && Convert.ToInt32(PEquSubNo.Text) != PrevNo)
                    if (Convert.ToInt32(PEquSubNo.Text) != PrevNo)
                    {
                        dt.Rows.Add(PSubjectCode.Text, PSubjectName.Text, PUnits.Text, PGrade.Text, PCurriculumSubject.Text, PCourseNo.Text, Convert.ToInt32(PEquSubNo.Text));
                        Count = Convert.ToInt32(PEquSubNo.Text);
                    }
                }
                //dt.Rows.Add(txtPreSubCode.Text.Trim(), txtPreSubName.Text.Trim(), txtUnits.Text.Trim(), txtGrade.Text.Trim(), ddlCurriculumSubject.SelectedItem.Text, ddlCurriculumSubject.SelectedValue, Count + 1);

                ds.Tables.Add(dt);
                Session["PreSubjectListNew"] = ds;
                lvExemptedfromStudy.DataSource = null;
                lvExemptedfromStudy.DataBind();
                lvExemptedfromStudy.DataSource = ds;
                lvExemptedfromStudy.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvExemptedfromStudy);
                objCommon.DisplayMessage(this, "Exempted Curriculum Subject Remove Successfully..", this.Page);
                return;
                //ClearPSubject();
            }
        }
        catch { }

    }
    protected void btnRemoveAdditi_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("retired");
            dt.Columns.Add("PSubjectCode");
            dt.Columns.Add("PSubjectName");
            dt.Columns.Add("PUnits");
            dt.Columns.Add("PGrade");
            dt.Columns.Add("PEquSubNo");

            LinkButton STUD = sender as LinkButton;
            string CCode = (STUD.CommandArgument);
            int AdditNo = int.Parse(STUD.CommandName);
            int Status = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_EQUIVALENCE", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString()) + "AND ADDITI_CCODE='" + CCode.ToString() + "' AND ADDIT_NO=" + AdditNo + "AND ISNULL(CANCEL,0)=0"));
            if (Status != 0)
            {
                if (ViewState["COURSE_REGISTERSTATUS"].ToString() == "0")
                {
                    string SP_Name1 = "PR_ACD_CANCEL_ADVISING_STUDENT_DETAILS";
                    string SP_Parameters1 = "@P_IDNO,@P_CCODE,@P_PREV_ADDIT_NO,@P_COMMAND_TYPE,@P_OUT";
                    string Call_Values1 = "" + Convert.ToInt32(ViewState["IDNO"].ToString()) + "," + CCode.ToString() + "," + AdditNo + "," + 2 + "," + ",0";
                    string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//insert
                    if (que_out1 == "1")
                    {
                        DataRow dr = dt.NewRow();
                        int Count = 0;
                        foreach (ListViewDataItem dataitem in lvAdditional.Items)
                        {

                            Label PSubjectCode = dataitem.FindControl("lblpAddiCode") as Label;
                            Label PSubjectName = dataitem.FindControl("lblpAddiCourseName") as Label;
                            Label PUnits = dataitem.FindControl("lblpAddiUnit") as Label;
                            Label PGrade = dataitem.FindControl("lblpAddiGrade") as Label;
                            Label PEquSubNo = dataitem.FindControl("lblAddNo") as Label;
                            if ((PSubjectCode.Text) != CCode.ToString() && Convert.ToInt32(PEquSubNo.Text) != AdditNo)
                            {
                                dt.Rows.Add(PSubjectCode.Text, PSubjectName.Text, PUnits.Text, PGrade.Text, Convert.ToInt32(PEquSubNo.Text));
                                Count = Convert.ToInt32(PEquSubNo.Text);
                            }

                        }
                        ds.Tables.Add(dt);
                        lvAdditional.DataSource = ds;
                        lvAdditional.DataBind();
                        ViewState["RemoveAdditional"] = 1;
                        StudentDetailsBind(Convert.ToInt32(ViewState["IDNO"].ToString()));
                        objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvExemptedfromStudy);
                        ViewState["RemoveAdditional"] = 0;
                        //BindListView();
                        objCommon.DisplayMessage(this, "Additional Learning Subject Remove Successfully..", this.Page);
                        return;
                    }
                }
                else
                {

                    objCommon.DisplayMessage(this, "Subject Registration already Completed Subject Should Not Be Removed..", this.Page);
                    return;
                }
            }
            else
            {

                DataRow dr = dt.NewRow();
                int Count = 0;
                foreach (ListViewDataItem dataitem in lvAdditional.Items)
                {

                    Label PSubjectCode = dataitem.FindControl("lblpAddiCode") as Label;
                    Label PSubjectName = dataitem.FindControl("lblpAddiCourseName") as Label;
                    Label PUnits = dataitem.FindControl("lblpAddiUnit") as Label;
                    Label PGrade = dataitem.FindControl("lblpAddiGrade") as Label;
                    Label PEquSubNo = dataitem.FindControl("lblAddNo") as Label;
                    if ((PSubjectCode.Text) != CCode.ToString() && Convert.ToInt32(PEquSubNo.Text) != AdditNo)
                    {
                        dt.Rows.Add(PSubjectCode.Text, PSubjectName.Text, PUnits.Text, PGrade.Text, Convert.ToInt32(PEquSubNo.Text));
                        Count = Convert.ToInt32(PEquSubNo.Text);
                    }

                }
                // dt.Rows.Add(txtPreSubCodeAdd.Text.Trim(), txtPreSubNameAdd.Text.Trim(), txtUnitsAdd.Text.Trim(), txtGradeAdd.Text.Trim(), Count + 1);

                ds.Tables.Add(dt);
                lvAdditional.DataSource = ds;
                lvAdditional.DataBind();
                objCommon.DisplayMessage(this, "Additional Learning Subject Remove Successfully..", this.Page);
                return;
                //ClearPSubject();
            }
        }
        catch { }
    }
    protected void chkNotExempted_CheckedChanged(object sender, EventArgs e)
    {
        if (chkNotExempted.Checked == true)
        {
            DivExempted.Visible = false;
            DivAdditional.Visible = false;
        }
        else
        {
            DivExempted.Visible = true;
            DivAdditional.Visible = true;
        }
    }
}