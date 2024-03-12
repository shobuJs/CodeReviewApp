using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS.UAIMS;
using IITMS;
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

using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.Web.Services;
using System.Web.Script.Services;

public partial class ACADEMIC_Advising_Details : System.Web.UI.Page
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
    protected void StudentDetailsBind(int Idno)
    {
        try
        {
            ViewState["COURSE_REGISTERSTATUS"] = 0;
            ViewState["IDNO"] = Idno;
            string SP_Name2 = "PKG_ACAD_SHIFTEES_DETAILS";
            string SP_Parameters2 = "@P_COMMAND_TYPE,@P_IDNO,@P_SESSIONNO";
            string Call_Values2 = ""+2+"," + Idno + "," + 0 + "";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (ds.Tables[0].Rows.Count > 0 || ds.Tables[0].Rows.Count == null)
            {
                BindStudentName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                BindStdID.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                BindDOB.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                BindGender.Text = ds.Tables[0].Rows[0]["GENDER"].ToString();
                BindMobNo.Text = ds.Tables[0].Rows[0]["MOBILE"].ToString();
                BindEmailID.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();

                BindProgram.Text = ds.Tables[0].Rows[0]["NEW_PROGRAM"].ToString();
                BindSem.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();

               // BindPreviousSchool.Text = ds.Tables[0].Rows[0]["PREVIOUS_SCHOOL"].ToString();
                BindOldCurriculum.Text = ds.Tables[0].Rows[0]["OLD_CURRICULUM"].ToString();
                BindNewCurriculum.Text = ds.Tables[0].Rows[0]["NEW_CURRICULUM"].ToString();

                BindOldCurriculum.ToolTip = ds.Tables[0].Rows[0]["OLD_SCHEMENO"].ToString();
                BindNewCurriculum.ToolTip = ds.Tables[0].Rows[0]["NEW_SCHEMENO"].ToString();

                //if (ds.Tables[0].Rows[0]["REMARK"].ToString() != string.Empty)
                //{
                //    if (ds.Tables[0].Rows[0]["EQUIV_STATUS"].ToString() == "1" && ds.Tables[0].Rows[0]["COURSE_REGISTER"].ToString() == "0")
                //    {
                //        //chkNotExempted.Checked = true;
                //        //chkNotExempted_CheckedChanged(new object(), new EventArgs());
                //        //chkNotExempted.Enabled = true;
                //    }
                //    else
                //    {
                //        //chkNotExempted.Enabled = false;
                //    }
                //    //txtRemark.Enabled = false;
                //}
                //else
                //{
                //    //chkNotExempted.Enabled = true;
                //    //txtRemark.Enabled = true;
                //}
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
            //    btnSubmit.Enabled = true;
                BindPreviousSchool.Text = ds.Tables[0].Rows[0]["PREVIOUS_SCHOOL"].ToString();
              //  objCommon.FillDropDownList(ddlOldCurriculumSubject, "ACD_COURSE_MAPPING CM INNER JOIN ACD_COURSE C ON (C.COURSENO=CM.COURSENO)INNER JOIN ACD_SEMESTER SEM ON (CM.SEMESTERNO=SEM.SEMESTERNO)", "DISTINCT C.COURSENO", "C.CCODE+' - '+ COURSE_NAME +' - '+ SEMESTERNAME AS COURSENAME,CM.SEMESTERNO", "CM.SCHEMENO=" + ds.Tables[0].Rows[0]["OLD_SCHEMENO"].ToString() + " AND CM.SEMESTERNO <=" + Convert.ToInt32(ds.Tables[0].Rows[0]["SEMESTERNO"].ToString()), "CM.SEMESTERNO");
      //          objCommon.FillDropDownList(ddlOldCurriculumSubject, "ACD_STUDENT_RESULT SR LEFT JOIN ACD_COURSE_MAPPING CM ON (SR.COURSENO=CM.COURSENO AND SR.SEMESTERNO=CM.SEMESTERNO) INNER JOIN ACD_COURSE C ON (C.COURSENO=CM.COURSENO) INNER JOIN ACD_SEMESTER SEM ON (CM.SEMESTERNO=SEM.SEMESTERNO)", "DISTINCT C.COURSENO", "C.CCODE+' - '+ COURSE_NAME +' - '+ SEMESTERNAME AS COURSENAME,CM.SEMESTERNO", "SR.IDNO =" + Idno + " AND ISNULL(SR.EXAM_REGISTERED,0)=1 AND ISNULL(SR.CANCEL,0)=0 AND CM.SCHEMENO=" + ds.Tables[0].Rows[0]["OLD_SCHEMENO"].ToString() + " AND CM.SEMESTERNO <=" + Convert.ToInt32(ds.Tables[0].Rows[0]["SEMESTERNO"].ToString()), "CM.SEMESTERNO");
                //objCommon.FillDropDownList(ddlOldCurriculumSubject, "ACD_STUDENT_RESULT SR LEFT JOIN ACD_COURSE_MAPPING CM ON (SR.COURSENO=CM.COURSENO AND SR.SEMESTERNO=CM.SEMESTERNO) INNER JOIN ACD_COURSE C ON (C.COURSENO=CM.COURSENO) INNER JOIN ACD_SEMESTER SEM ON (CM.SEMESTERNO=SEM.SEMESTERNO)", "DISTINCT C.COURSENO", "C.CCODE+' - '+ COURSE_NAME +' - '+ SEMESTERNAME AS COURSENAME,SR.SEMESTERNO", "SR.IDNO =" + Idno + " AND SR.SCHEMENO=" + ds.Tables[0].Rows[0]["OLD_SCHEMENO"].ToString() + " AND SR.SEMESTERNO <" + Convert.ToInt32(ds.Tables[0].Rows[0]["SEMESTERNO"].ToString()), "SR.SEMESTERNO");
                objCommon.FillDropDownList(ddlOldCurriculumSubject, "ACD_STUDENT_RESULT SR LEFT JOIN ACD_COURSE_MAPPING CM ON (SR.COURSENO=CM.COURSENO AND SR.SEMESTERNO=CM.SEMESTERNO) INNER JOIN ACD_COURSE C ON (C.COURSENO=CM.COURSENO) INNER JOIN ACD_SEMESTER SEM ON (CM.SEMESTERNO=SEM.SEMESTERNO)", "DISTINCT C.COURSENO", "C.CCODE+' - '+ COURSE_NAME +' - '+ SEMESTERNAME AS COURSENAME,SR.SEMESTERNO", "SR.IDNO =" + Idno + " AND SR.SEMESTERNO <" + Convert.ToInt32(ds.Tables[0].Rows[0]["SEMESTERNO"].ToString()), "SR.SEMESTERNO");

                objCommon.FillDropDownList(ddlNewCurriculumSubject, "ACD_COURSE_MAPPING CM INNER JOIN ACD_COURSE C ON (C.COURSENO=CM.COURSENO)INNER JOIN ACD_SEMESTER SEM ON (CM.SEMESTERNO=SEM.SEMESTERNO)", "DISTINCT C.COURSENO", "C.CCODE+' - '+ COURSE_NAME +' - '+ SEMESTERNAME AS COURSENAME,CM.SEMESTERNO", "CM.SCHEMENO=" + ds.Tables[0].Rows[0]["NEW_SCHEMENO"].ToString(), "CM.SEMESTERNO");
                //objCommon.FillDropDownList(ddlNewCurriculumSubject, "ACD_OFFERED_COURSE CM INNER JOIN ACD_COURSE C ON (C.COURSENO=CM.COURSENO)INNER JOIN ACD_SEMESTER SEM ON (CM.SEMESTERNO=SEM.SEMESTERNO)", "DISTINCT C.COURSENO", "C.CCODE+' - '+ COURSE_NAME +' - '+ SEMESTERNAME AS COURSENAME,CM.SEMESTERNO", "CM.SCHEMENO=" + ds.Tables[0].Rows[0]["NEW_SCHEMENO"].ToString(), "CM.SEMESTERNO");
                //objCommon.FillDropDownList(ddlNewCurriculumSubject, "ACD_COURSE_MAPPING CM INNER JOIN ACD_COURSE C ON (C.COURSENO=CM.COURSENO)INNER JOIN ACD_SEMESTER SEM ON (CM.SEMESTERNO=SEM.SEMESTERNO)", "DISTINCT C.COURSENO", "C.CCODE+' - '+ COURSE_NAME +' - '+ SEMESTERNAME AS COURSENAME,CM.SEMESTERNO", "CM.SCHEMENO=" + ds.Tables[0].Rows[0]["NEW_SCHEMENO"].ToString() + " AND CM.SEMESTERNO <=" + Convert.ToInt32(ds.Tables[0].Rows[0]["SEMESTERNO"].ToString()), "CM.SEMESTERNO");
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

    protected void BindListView()
    {

        DataSet ds1 = null;
        string SP_Name2 = "PKG_ACAD_SHIFTEES_DETAILS";
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
                    dt.Columns.Add("PCourseNo");
                    dt.Columns.Add("PSubjectName");
                    dt.Columns.Add("NCourseNo");
                    dt.Columns.Add("NSubjectName");
                    dt.Columns.Add("MappingID");
                    dt.Columns.Add("MappingStatus");
                    //dt.Columns.Add("PCourseNo");
                    dt.Columns.Add("PEquSubNo");

                    DataRow dr = dt.NewRow();
                    int Count = 0;
                    foreach (ListViewDataItem dataitem in lvExemptedfromStudy.Items)
                    {
                        Label lblOldCourseNo = dataitem.FindControl("lblOldCourseNo") as Label;
                        Label lblOldCurriculumSubject = dataitem.FindControl("lblOldCurriculumSubject") as Label;
                        Label lblNewCourseNo = dataitem.FindControl("lblNewCourseNo") as Label;
                        Label lblNewCurriculumSubject = dataitem.FindControl("lblNewCurriculumSubject") as Label;
                        Label lblMappingID = dataitem.FindControl("lblMappingID") as Label;
                        Label lblMappingStatus = dataitem.FindControl("lblMappingStatus") as Label;
                        Label PEquSubNo = dataitem.FindControl("lblPrev_No") as Label;

                        dt.Rows.Add(lblOldCourseNo.Text, lblOldCurriculumSubject.Text, lblNewCourseNo.Text, lblNewCurriculumSubject.Text, lblMappingID.Text, lblMappingStatus.Text, Convert.ToInt32(PEquSubNo.Text));
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
                    dt.Columns.Add("PCourseNo");
                    dt.Columns.Add("PSubjectName");
                    dt.Columns.Add("NCourseNo");
                    dt.Columns.Add("NSubjectName");
                    dt.Columns.Add("MappingID");
                    dt.Columns.Add("MappingStatus");
                    //dt.Columns.Add("PCourseNo");
                    dt.Columns.Add("PEquSubNo");

                    DataRow dr = dt.NewRow();
                    int Count = 0;
                    foreach (ListViewDataItem dataitem in lvExemptedfromStudy.Items)
                    {
                        Label lblOldCourseNo = dataitem.FindControl("lblOldCourseNo") as Label;
                        Label lblOldCurriculumSubject = dataitem.FindControl("lblOldCurriculumSubject") as Label;
                        Label lblNewCourseNo = dataitem.FindControl("lblNewCourseNo") as Label;
                        Label lblNewCurriculumSubject = dataitem.FindControl("lblNewCurriculumSubject") as Label;
                        Label lblMappingID = dataitem.FindControl("lblMappingID") as Label;
                        Label lblMappingStatus = dataitem.FindControl("lblMappingStatus") as Label;
                        Label PEquSubNo = dataitem.FindControl("lblPrev_No") as Label;
                        dt.Rows.Add(lblOldCourseNo.Text, lblOldCurriculumSubject.Text, lblNewCourseNo.Text, lblNewCurriculumSubject.Text, lblMappingID.Text, lblMappingStatus.Text, Convert.ToInt32(PEquSubNo.Text));
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

    //        if (ds.Tables[2].Rows.Count > 0 || ds.Tables[2].Rows.Count == null)
    //        {
    //            if (ViewState["RemoveAdditional"].ToString() == "1")
    //            {

    //                DataTable dt = new DataTable("retired");
    //                dt.Columns.Add("PSubjectCode");
    //                dt.Columns.Add("PSubjectName");
    //                dt.Columns.Add("PUnits");
    //                dt.Columns.Add("PGrade");
    //                dt.Columns.Add("PEquSubNo");

    //                DataRow dr = dt.NewRow();
    //                int Count = 0;
    //                foreach (ListViewDataItem dataitem in lvAdditional.Items)
    //                {

    //                    Label PSubjectCode = dataitem.FindControl("lblpAddiCode") as Label;
    //                    Label PSubjectName = dataitem.FindControl("lblpAddiCourseName") as Label;
    //                    Label PUnits = dataitem.FindControl("lblpAddiUnit") as Label;
    //                    Label PGrade = dataitem.FindControl("lblpAddiGrade") as Label;
    //                    Label PEquSubNo = dataitem.FindControl("lblAddNo") as Label;

    //                    dt.Rows.Add(PSubjectCode.Text, PSubjectName.Text, PUnits.Text, PGrade.Text, Convert.ToInt32(PEquSubNo.Text));
    //                    Count = Convert.ToInt32(PEquSubNo.Text);

    //                }

    //                ds1.Tables.Add(dt);
    //                ds1.Merge(ds.Tables[2]);

    //                //lvAdditional.DataSource = ds;
    //                //lvAdditional.DataBind();

    //            }
    //            else
    //            {
    //                //lvAdditional.DataSource = ds.Tables[2];
    //                //lvAdditional.DataBind();
    //            }
    //           // objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvAdditional);
    //        }
    //        else
    //        {
    //            if (ViewState["RemoveAdditional"].ToString() == "1")
    //            {

    //                DataTable dt = new DataTable("retired");
    //                dt.Columns.Add("PSubjectCode");
    //                dt.Columns.Add("PSubjectName");
    //                dt.Columns.Add("PUnits");
    //                dt.Columns.Add("PGrade");
    //                dt.Columns.Add("PEquSubNo");

    //                DataRow dr = dt.NewRow();
    //                int Count = 0;
    //                foreach (ListViewDataItem dataitem in lvAdditional.Items)
    //                {

    //                    Label PSubjectCode = dataitem.FindControl("lblpAddiCode") as Label;
    //                    Label PSubjectName = dataitem.FindControl("lblpAddiCourseName") as Label;
    //                    Label PUnits = dataitem.FindControl("lblpAddiUnit") as Label;
    //                    Label PGrade = dataitem.FindControl("lblpAddiGrade") as Label;
    //                    Label PEquSubNo = dataitem.FindControl("lblAddNo") as Label;

    //                    dt.Rows.Add(PSubjectCode.Text, PSubjectName.Text, PUnits.Text, PGrade.Text, Convert.ToInt32(PEquSubNo.Text));
    //                    Count = Convert.ToInt32(PEquSubNo.Text);

    //                }

    //                ds1.Tables.Add(dt);
    //                ds1.Merge(ds.Tables[2]);

    //                //lvAdditional.DataSource = ds;
    //                //lvAdditional.DataBind();

    //                if (ds1.Tables[0].Rows.Count > 0 || ds.Tables[0].Rows.Count == null)
    //                {
    //                    //lvAdditional.DataSource = ds1;
    //                    //lvAdditional.DataBind();

    //                }
    //                else
    //                {
    //                    //lvAdditional.DataSource = null;
    //                    //lvAdditional.DataBind();
    //                }


    //            }
    //            else
    //            {
    //                //lvAdditional.DataSource = null;
    //                //lvAdditional.DataBind();
    //            }
    //           // objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvAdditional);

          //  }

        }

    }


    protected void btnAddSubject_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("retired");
            dt.Columns.Add("PCourseNo");
            dt.Columns.Add("PSubjectName");
            dt.Columns.Add("NCourseNo");
            dt.Columns.Add("NSubjectName");
            dt.Columns.Add("MappingID");
            dt.Columns.Add("MappingStatus");
            //dt.Columns.Add("PCourseNo");
            dt.Columns.Add("PEquSubNo");

            DataRow dr = dt.NewRow();
            int Count = 0;
            foreach (ListViewDataItem dataitem in lvExemptedfromStudy.Items)
            {
                Label lblOldCourseNo = dataitem.FindControl("lblOldCourseNo") as Label;
                Label lblOldCurriculumSubject = dataitem.FindControl("lblOldCurriculumSubject") as Label;
                Label lblNewCourseNo = dataitem.FindControl("lblNewCourseNo") as Label;
                Label lblNewCurriculumSubject = dataitem.FindControl("lblNewCurriculumSubject") as Label;
                Label lblMappingID = dataitem.FindControl("lblMappingID") as Label;
                Label lblMappingStatus = dataitem.FindControl("lblMappingStatus") as Label;
                Label PEquSubNo = dataitem.FindControl("lblPrev_No") as Label;

                dt.Rows.Add(lblOldCourseNo.Text, lblOldCurriculumSubject.Text, lblNewCourseNo.Text, lblNewCurriculumSubject.Text,lblMappingID.Text, lblMappingStatus.Text, Convert.ToInt32(PEquSubNo.Text));
                Count = Convert.ToInt32(PEquSubNo.Text);

                if (lblOldCourseNo.Text == Convert.ToString(ddlOldCurriculumSubject.SelectedValue) || lblOldCurriculumSubject.Text == ddlOldCurriculumSubject.SelectedItem.Text)//|| PCourseNo.Text == ddlCurriculumSubject.SelectedValue
                {
                    ClearPSubject();
                    objCommon.DisplayMessage(this, "Previous Subject Code Or  Previous Subject Name Already Exists..", this.Page);
                    return;
                }

                }
            dt.Rows.Add(ddlOldCurriculumSubject.SelectedValue, ddlOldCurriculumSubject.SelectedItem.Text, ddlNewCurriculumSubject.SelectedValue, ddlNewCurriculumSubject.SelectedItem.Text,ddlMappingStatus.SelectedValue, ddlMappingStatus.SelectedItem.Text, Count + 1);

            ds.Tables.Add(dt);
            Session["PreSubjectListNew"] = ds;
            lvExemptedfromStudy.DataSource = ds;
            lvExemptedfromStudy.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvExemptedfromStudy);
            ClearPSubject();
            
        }
        catch { }
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
            dt.Columns.Add("PCourseNo");
            dt.Columns.Add("PSubjectName");
            dt.Columns.Add("NCourseNo");
            dt.Columns.Add("NSubjectName");
            dt.Columns.Add("MappingID");
            dt.Columns.Add("MappingStatus");
            //dt.Columns.Add("PCourseNo");
            dt.Columns.Add("PEquSubNo");

            int Status = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_SHIFTEE_EQUIVALENCE", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString()) + "AND OLD_COURSENO=" + CourseNo + "AND ISNULL(PREV_NO,0)=" + PrevNo + "AND ISNULL(CANCEL,0)=0"));
            if (Status != 0)
            {
                if (ViewState["COURSE_REGISTERSTATUS"].ToString() == "0")
                {
                    //string SP_Name1 = "PR_ACD_CANCEL_ADVISING_STUDENT_DETAILS";
                    string SP_Name1 = "PR_ACD_CANCEL_ADVISING_SHIFTEES_STUDENT_DETAILS";
                    string SP_Parameters1 = "@P_IDNO,@P_COURSENO,@P_PREV_ADDIT_NO,@P_COMMAND_TYPE,@P_OUT";
                    string Call_Values1 = "" + Convert.ToInt32(ViewState["IDNO"].ToString()) + "," + CourseNo + "," + PrevNo + "," + 1 + "," + ",0";
                    string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//insert
                    if (que_out1 == "1")
                    {
                        DataRow dr = dt.NewRow();
                        int Count = 0;
                        foreach (ListViewDataItem dataitem in lvExemptedfromStudy.Items)
                        {
                            Label lblOldCourseNo = dataitem.FindControl("lblOldCourseNo") as Label;
                            Label lblOldCurriculumSubject = dataitem.FindControl("lblOldCurriculumSubject") as Label;
                            Label lblNewCourseNo = dataitem.FindControl("lblNewCourseNo") as Label;
                            Label lblNewCurriculumSubject = dataitem.FindControl("lblNewCurriculumSubject") as Label;
                            Label lblMappingID = dataitem.FindControl("lblMappingID") as Label;
                            Label lblMappingStatus = dataitem.FindControl("lblMappingStatus") as Label;
                            Label PEquSubNo = dataitem.FindControl("lblPrev_No") as Label;

                            if (Convert.ToInt32(PEquSubNo.Text) != PrevNo)
                            {
                                dt.Rows.Add(lblOldCourseNo.Text, lblOldCurriculumSubject.Text, lblNewCourseNo.Text, lblNewCurriculumSubject.Text, lblMappingID.Text, lblMappingStatus.Text, Convert.ToInt32(PEquSubNo.Text));
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

                 //   objCommon.DisplayMessage(this, "Subject Registration already Completed Subject Should Not Be Removed..", this.Page);
                    objCommon.DisplayMessage(this, "Student already Enlisted!!! Please Reset enlistment for Advising.", this.Page);
                    return;
                }
            }
            else
            {
                DataRow dr = dt.NewRow();
                int Count = 0;
                foreach (ListViewDataItem dataitem in lvExemptedfromStudy.Items)
                {
                    Label lblOldCourseNo = dataitem.FindControl("lblOldCourseNo") as Label;
                    Label lblOldCurriculumSubject = dataitem.FindControl("lblOldCurriculumSubject") as Label;
                    Label lblNewCourseNo = dataitem.FindControl("lblNewCourseNo") as Label;
                    Label lblNewCurriculumSubject = dataitem.FindControl("lblNewCurriculumSubject") as Label;
                    Label lblMappingID = dataitem.FindControl("lblMappingID") as Label;
                    Label lblMappingStatus = dataitem.FindControl("lblMappingStatus") as Label;
                    Label PEquSubNo = dataitem.FindControl("lblPrev_No") as Label;

                    if (Convert.ToInt32(PEquSubNo.Text) != PrevNo)
                    {
                        dt.Rows.Add(lblOldCourseNo.Text, lblOldCurriculumSubject.Text, lblNewCourseNo.Text, lblNewCurriculumSubject.Text, lblMappingID.Text, lblMappingStatus.Text, Convert.ToInt32(PEquSubNo.Text));
                        Count = Convert.ToInt32(PEquSubNo.Text);
                    }
                }
                ds.Tables.Add(dt);
                Session["PreSubjectListNew"] = ds;
                lvExemptedfromStudy.DataSource = null;
                lvExemptedfromStudy.DataBind();
                lvExemptedfromStudy.DataSource = ds;
                lvExemptedfromStudy.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvExemptedfromStudy);
                objCommon.DisplayMessage(this, "Exempted Curriculum Subject Remove Successfully..", this.Page);
                return;
            }
        }
        catch (Exception ex)
        { 
        }

    }

    protected void ClearPSubject()
    {
        ddlOldCurriculumSubject.SelectedIndex = 0;
        ddlNewCurriculumSubject.SelectedIndex = 0;
        ddlMappingStatus.SelectedIndex = 0;
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

    protected void btncanceldata_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CourseController objCC = new CourseController();
        string PSCode = string.Empty; string PSName = string.Empty;
        string PCourseNo = string.Empty; string Pidno = string.Empty;
        string NSCode = string.Empty; string NSName = string.Empty;
        string NCourseNo = string.Empty; string Nidno = string.Empty;
        string Prev_no = string.Empty;
        string Mapping_Id = string.Empty;

        int Count1 = 0;
            foreach (ListViewDataItem dataitem in lvExemptedfromStudy.Items)
            {
                Count1++;
                Label lblOldCourseNo = dataitem.FindControl("lblOldCourseNo") as Label;
                Label lblOldCurriculumSubject = dataitem.FindControl("lblOldCurriculumSubject") as Label;
                Label lblNewCourseNo = dataitem.FindControl("lblNewCourseNo") as Label;
                Label lblNewCurriculumSubject = dataitem.FindControl("lblNewCurriculumSubject") as Label;
                Label lblMappingID = dataitem.FindControl("lblMappingID") as Label;
                Label lblMappingStatus = dataitem.FindControl("lblMappingStatus") as Label;
                Label PEquSubNo = dataitem.FindControl("lblPrev_No") as Label;


                PSCode += lblOldCurriculumSubject.Text + "$";
                PCourseNo += lblOldCourseNo.Text + "$";
                PSName += lblOldCurriculumSubject.Text + "$";
                NCourseNo += lblNewCourseNo.Text + "$";
                NSCode += lblNewCurriculumSubject.Text + "$";
                NSName += lblNewCurriculumSubject.Text + "$";
                Mapping_Id += lblMappingID.Text + "$";
                Pidno += ViewState["IDNO"].ToString() + "$";
                Prev_no += PEquSubNo.Text + "$";
            }
            if (Count1 == 0)
            {
                objCommon.DisplayMessage(this, "Please Add At List One Credited Subject", this.Page);
                return;
            }
            PSCode = PSCode.TrimEnd('$');
            PCourseNo= PCourseNo.TrimEnd('$');
            PSName = PSName.TrimEnd('$');
            NCourseNo = NCourseNo.TrimEnd('$');
            NSCode = NSCode.TrimEnd('$');
            NSName = NSName.TrimEnd('$');
            Mapping_Id = Mapping_Id.TrimEnd('$');
            Pidno = Pidno.TrimEnd('$');
            Prev_no = Prev_no.TrimEnd('$');

            //Pidno = ViewState["IDNO"].ToString();
            CustomStatus cs = (CustomStatus)objCC.InsertAdvisingShifteesDetails(Pidno, PCourseNo, PSName, NCourseNo, NSName,Convert.ToInt32(BindOldCurriculum.ToolTip),Convert.ToInt32(BindNewCurriculum.ToolTip),Convert.ToInt32(0), Prev_no, "", Convert.ToInt32(Session["userno"]), Mapping_Id);
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
}