//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Comprehensive Student Report
// CREATION DATE : 
// CREATED BY    : AMIT YADAV
// MODIFIED BY   : ASHISH DHAKATE
// MODIFIED DATE : 14/02/2012
// MODIFIED DESC : 
//======================================================================================
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
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.IO;

using System.Collections.Generic;
using System.Linq;
using System.Web;


public partial class ACADEMIC_StudentSportsDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    static int idno = 0;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //HtmlAnchor HA = new HtmlAnchor();
        //HA.ServerClick += new EventHandler(sportsdetails);
        //ceStartDate.EndDate = DateTime.Now;   //to dissable future  Date
        if (!Page.IsPostBack)
        {
            imgPhoto.ImageUrl = "~/images/nophoto.jpg";

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["currentsession"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]) + " and ua_no=" + Convert.ToInt32(Session["userno"]));
                ViewState["usertype"] = ua_type;

                if (ViewState["usertype"].ToString() == "2")
                {
                    pnlSearch.Visible = false;
                    ShowDetails();
                    //to show only sports selected in readiobuttonlist
                    rdoType.SelectedValue = "1";
                    GetSportsDetails();
                    //  BindSportsDetails();
                }
                else
                {
                    pnlSearch.Visible = true;
                }


                ViewState["action"] = "add";
                ///Added by Neha Baranwal - 02 Oct 19
                objCommon.FillDropDownList(ddlGameName, "ACD_GAME_MASTER", "GAME_NO", "GAME_NAME", "GAME_NO>0", "GAME_NO");
                objCommon.FillDropDownList(ddlLevelOfParticipation, "ACD_PARTICIPATION_LEVEL_MASTER", "PARTICIPATION_NO", "PARTICIPATION_LEVEL", "PARTICIPATION_NO>0", "PARTICIPATION_NO");
                objCommon.FillDropDownList(ddlAchievement, "ACD_ACHIEVEMENT_MASTER", "ACHIEVEMENT_NO", "ACHIEVEMENT_NAME", "ACHIEVEMENT_NO>0", "ACHIEVEMENT_NO");

            }
        }
        else
        {
            if (Page.Request.Params["__EVENTTARGET"] != null)
            {
                //imgPhoto.ImageUrl = "~/images/nophoto.jpg";
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                {
                    string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                }

                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                {
                    txtEnrollmentSearch.Text = string.Empty;
                    txtEnrollmentSearch.Focus();
                    ClearControls();
                    divSportsBtnDetails.Visible = false;
                    divSportsDetails.Visible = false;
                    divStudentInfo.Visible = false;
                    //return;
                }
                //imgPhoto.ImageUrl = "~/images/nophoto.jpg";
            }
        }
        divMsg.InnerHtml = string.Empty;
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowDetails();
        ClearControls();
        BindSportsDetails();
    }


    private void ShowDetails()
    {
        //Clear();

        StudentController objSC = new StudentController();

        FeeCollectionController feeController = new FeeCollectionController();
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());
        }

        try
        {
            if (idno > 0)
            {

                DataTableReader dtr = objSC.GetStudentDetails(idno);
                if (dtr != null && (dtr.HasRows))
                {
                    while (dtr.Read())
                    {
                        lblRegNo.Text = dtr["REGNO"].ToString();
                        ViewState["admbatch"] = dtr["ADMBATCH"];
                        string degreeno = dtr["DEGREENO"] == null ? "0" : dtr["DEGREENO"].ToString();
                        string branchno = dtr["BRANCHNO"] == null ? "0" : dtr["BRANCHNO"].ToString();

                        DataSet ds = objCommon.FillDropDown("ACD_BRANCH A ,ACD_DEGREE B", "LONGNAME", "DEGREENAME", "(A.BRANCHNO=" + branchno + " OR " + branchno + "=0) AND (B.DEGREENO=" + degreeno + " OR " + degreeno + "=0)", "");
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            lblBranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                            lblDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                        }
                        else
                        {
                            lblBranch.Text = string.Empty;
                            lblDegree.Text = string.Empty;
                        }
                        lblName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                        lblMobNo.Text = dtr["STUDENTMOBILE"] == null ? string.Empty : dtr["STUDENTMOBILE"].ToString();
                        lblEnrollNo.Text = dtr["ENROLLNO"] == null ? string.Empty : dtr["ENROLLNO"].ToString();
                        string semester = string.Empty;

                        if (dtr["YEARWISE"].ToString() == "1")
                        {
                            semester = objCommon.LookUp("ACD_YEAR", "YEARNAME", "YEAR=" + dtr["YEAR"].ToString());
                        }
                        else
                        {
                            semester = objCommon.LookUp("ACD_SEMESTER", "SEMESTERNAME", "SEMESTERNO=" + dtr["SEMESTERNO"].ToString());
                        }

                        lblSemester.Text = semester;

                        lblMailID.Text = dtr["EMAILID"] == null ? string.Empty : dtr["EMAILID"].ToString();

                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dtr["IDNO"].ToString() + "&type=student";
                        //imgSign.ImageUrl = "~/showimage.aspx?id=" + dtr["IDNO"].ToString() + "&type=STUDENTSIGN";
                    }
                }


                DataSet ds1 = null;
                ds1 = objSC.GetAllSportsDetails(Convert.ToInt32(idno));
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    lvSportsDetails.DataSource = ds1.Tables[0];
                    lvSportsDetails.DataBind();
                    //to show only sports selected in readiobuttonlist
                    rdoType.SelectedValue = "1";
                    GetSportsDetails();
                    btnReport.Enabled = true;

                    // BindLV();
                    //BindSportsDetails();
                }
                else
                {
                    lvSportsDetails.DataSource = null;
                    lvSportsDetails.DataBind();
                    //to show only sports selected in readiobuttonlist
                    rdoType.SelectedValue = "1";
                    GetSportsDetails();
                    btnReport.Enabled = false;
                    //BindSportsDetails();
                }

            }
            else
            {
                objCommon.DisplayMessage("No student found having Univ. Reg. No.: " + txtEnrollmentSearch.Text.Trim(), this.Page);
                txtEnrollmentSearch.Focus();
                ClearControls();
                divSportsBtnDetails.Visible = false;
                divSportsDetails.Visible = false;
                divStudentInfo.Visible = false;
                return;
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentSportsDetails.btnSearch_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    //public void BindLV()
    //{
    //    foreach (ListViewDataItem item in lvSportsDetails.Items)
    //    {
    //        LinkButton btnDownloadFile = item.FindControl("btnDownloadFile") as LinkButton;
    //        Label lblFileName = item.FindControl("lblFileName") as Label;
    //        DataSet ds = objSC.GetAllSportsDetails(Convert.ToInt32(idno));

    //        //if (lblFileName.Text == "")
    //        //{
    //        //    btnDownloadFile.Text = "-";
    //        //    btnDownloadFile.Enabled = false;
    //        //}
    //        //else
    //        //{
    //        //    btnDownloadFile.Text = "Download";
    //        //    btnDownloadFile.Enabled = true;
    //        //}

    //    }
    //}

    #region "Step-9: Sports Details"
    /// <summary>
    /// step--999
    /// Added by NEha Baranwal (02/10/2019)
    /// Sports And Achievement
    /// Added by Neha Baranwal
    /// </summary>

    Student objStudent = new Student();
    StudentController objSC = new StudentController();
    int status = 0;
    static int sportsno = 0;
    static bool validationflag = true;

    public void BindSportsDetails()
    {
        if (idno != 0)
        {
            DataSet ds = null;
            ds = objSC.GetAllSportsDetails(Convert.ToInt32(idno));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSportsDetails.DataSource = ds.Tables[0];
                lvSportsDetails.DataBind();

                //gvSportsDetails.DataSource = ds.Tables[0];
                //gvSportsDetails.DataBind();

                btnReport.Enabled = true;
                //BindLV();
            }
            else
            {
                //gvSportsDetails.DataSource = null;
                //gvSportsDetails.DataBind();
                lvSportsDetails.DataSource = null;
                lvSportsDetails.DataBind();
                btnReport.Enabled = false;
            }
        }
        else
        {
            objCommon.DisplayMessage("Please Refresh the Page again!!", this.Page);
            return;
        }
    }






    //to clear all controls
    private void ClearControls()
    {
        ViewState["action"] = "add";
        txtNameOfGameOrAchievement.Text = "";
        txtDate.Text = "";
        txtVenue.Text = "";
        ddlLevelOfParticipation.SelectedIndex = 0;
        ddlAchievement.SelectedIndex = 0;
        ddlGameName.SelectedIndex = 0;
        lvSportsCertificate.DataSource = null;
        lvSportsCertificate.DataBind();
        lvSportsDetails.DataSource = null;
        lvSportsDetails.DataBind();
        //lblStatus.Text = string.Empty;

        DataSet ds = null;
    }

    //to show sports details on edit button
    private void ShowDetails(int sportsno)
    {
        try
        {
            SqlDataReader dr = objSC.GetSportsDetails(sportsno);
            if (dr != null)
            {
                if (dr.Read())
                {
                    ddlGameName.SelectedValue = dr["GAME_NO"] == DBNull.Value ? "-1" : dr["GAME_NO"].ToString();
                    txtNameOfGameOrAchievement.Text = dr["SPORTS_NAME"] == DBNull.Value ? string.Empty : dr["SPORTS_NAME"].ToString();
                    txtDate.Text = dr["SPORTS_DATE"] == DBNull.Value ? "0" : dr["SPORTS_DATE"].ToString();
                    txtVenue.Text = dr["SPORTS_VENUE"] == DBNull.Value ? "0" : dr["SPORTS_VENUE"].ToString();
                    ddlLevelOfParticipation.SelectedValue = dr["PARTICIPATION_NO"] == DBNull.Value ? "-1" : dr["PARTICIPATION_NO"].ToString();
                    ddlAchievement.SelectedValue = dr["ACHIEVEMENT_NO"] == DBNull.Value ? "-1" : dr["ACHIEVEMENT_NO"].ToString();


                    //to get uploaded Certificates
                    DataSet dsCERT = new DataSet();

                    dsCERT = objSC.GetCertificatesBySportsNo(sportsno);
                    if (dsCERT.Tables[0].Rows.Count > 0)
                    {
                        lvSportsCertificate.DataSource = dsCERT;
                        lvSportsCertificate.DataBind();
                    }
                    else
                    {
                        lvSportsCertificate.DataSource = null;
                        lvSportsCertificate.DataBind();
                    }
                }
                dr.Close();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentSportsDetails.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    //function to upload single docs
    private void uploadSportsCertificate()
    {

        try
        {
            if (idno != 0)
            {
                //string folderPath = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + txtCCode.Text + "\\";
                string folderPath = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + idno.ToString() + "-" + txtNameOfGameOrAchievement.Text + "\\";
                if (fuSportsCertificate.HasFile)
                {
                    string ext = System.IO.Path.GetExtension(fuSportsCertificate.PostedFile.FileName);
                    HttpPostedFile file = fuSportsCertificate.PostedFile;
                    string filename = Path.GetFileName(fuSportsCertificate.PostedFile.FileName) + idno.ToString() + "-" + txtNameOfGameOrAchievement.Text;
                    if (ext == ".pdf" || ext == ".png" || ext == ".jpeg" || ext == ".jpg" || ext == ".PNG" || ext == ".JPEG" || ext == ".JPG" || ext == ".PDF") //|| ext == ".doc" || ext == ".docx" 
                    {
                        //if (file.ContentLength <= 51200)// 31457280 before size  //For Allowing 50 Kb Size Files only 
                        //{
                        if (file.ContentLength <= 102400)// 31457280 before size  //For Allowing 100 Kb Size Files only 
                        {
                            string contentType = fuSportsCertificate.PostedFile.ContentType;
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }
                            objStudent.sportsFilename = filename;
                            objStudent.sportsFilepath = folderPath + filename;
                            fuSportsCertificate.PostedFile.SaveAs(folderPath + filename);
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
                        objCommon.DisplayMessage("Only .jpg,.png,.jpeg,.pdf file type allowed  !", this.Page);
                        return;
                    }

                }
                else
                {

                    objStudent.sportsFilename = "";
                    objStudent.sportsFilepath = "";
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
            // objCommon.DisplayMessage(this, "Something went wrong ! Please contact Admin", this.Page);

        }
    }



    private void Check50KBFileSize()
    {
        try
        {
            if (idno != 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    //string folderPath = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + idno.ToString() + "-" + txtNameOfGameOrAchievement.Text + "\\";
                    string folderPath = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + idno.ToString() + "\\";

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
                objStudent.sportsFilename = objStudent.sportsFilename.TrimEnd(',');
                objStudent.sportsFilepath = objStudent.sportsFilepath.TrimEnd(',');
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
    private void uploadMultipleSportsCertificate()
    {
        try
        {
            if (idno != 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    //string folderPath = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + idno.ToString() + "-" + txtNameOfGameOrAchievement.Text + "\\";
                    string folderPath = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + idno.ToString() + "\\";

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
                                objStudent.sportsFilename += filename + ",";
                                objStudent.sportsFilepath += folderPath + filename + ",";
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
                objStudent.sportsFilename = objStudent.sportsFilename.TrimEnd(',');
                objStudent.sportsFilepath = objStudent.sportsFilepath.TrimEnd(',');
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

    //add sports details
    protected void btnSaveNext10_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["action"] != null && idno != 0)
            {
                objStudent.sportsType = rdoType.SelectedValue;
                objStudent.sportsName = txtNameOfGameOrAchievement.Text;
                objStudent.sportsVenue = txtVenue.Text;
                objStudent.sportsDate = Convert.ToDateTime(txtDate.Text);
                objStudent.participationNo = Convert.ToInt32(ddlLevelOfParticipation.SelectedValue);
                objStudent.achievementNo = Convert.ToInt32(ddlAchievement.SelectedValue);
                objStudent.idno = Convert.ToInt32(idno);
                objStudent.gameNo = Convert.ToInt32(ddlGameName.SelectedValue);
                objStudent.sportsFilename = "";
                objStudent.sportsFilepath = "";

                if (ViewState["action"].ToString().Equals("edit"))
                {
                    //check entry already done for this game and on same date
                    int count = Convert.ToInt32(objCommon.LookUp("ACD_SPORTS_DETAILS", "count(*)", "GAME_NO = " + ddlGameName.SelectedValue + " AND SPORTS_NAME = '" + txtNameOfGameOrAchievement.Text + "'AND IDNO = " + idno + " AND SPORTS_NO <> " + sportsno + " AND SPORTS_DATE = CONVERT(DATE,'" + Convert.ToDateTime(txtDate.Text.Trim()).ToString("dd/MM/yyyy") + "',103)"));
                    if (count > 0)
                    {
                        objCommon.DisplayMessage("Sports Entry Already Done For this Date", this.Page);
                        ViewState["action"] = "add";
                        txtNameOfGameOrAchievement.Text = "";
                        txtDate.Text = "";
                        txtVenue.Text = "";
                        ddlLevelOfParticipation.SelectedIndex = 0;
                        ddlAchievement.SelectedIndex = 0;
                        lvSportsCertificate.DataSource = null;
                        lvSportsCertificate.DataBind();
                        return;
                    }
                    objStudent.sportsNo = sportsno;



                    if (fuSportsCertificate.HasFile)
                    {
                        Check50KBFileSize();

                        if (validationflag == true)
                        {
                            //to upload multiple sports docs details
                            uploadMultipleSportsCertificate();
                            //uploadSportsCertificate();
                            if (status == 1)
                            {
                                CustomStatus cs = (CustomStatus)objSC.UpdateSportsDetails(objStudent);
                                if (cs.Equals(CustomStatus.RecordUpdated))
                                {
                                    objCommon.DisplayMessage("Sports Details Modified Successfully!!", this.Page);
                                    ViewState["action"] = "add";
                                    ClearControls();
                                    BindSportsDetails();
                                }
                                else
                                    objCommon.DisplayMessage("Error!!", this.Page);
                            }
                        }
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objSC.UpdateSportsDetails(objStudent);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage("Sports Details Modified Successfully!!", this.Page);
                            ViewState["action"] = "add";
                            ClearControls();
                            BindSportsDetails();
                        }
                        else
                            objCommon.DisplayMessage("Error!!", this.Page);
                    }


                }
                //to add
                else
                {

                    //check entry already done for this game and on same date
                    int count = Convert.ToInt32(objCommon.LookUp("ACD_SPORTS_DETAILS", "count(*)", "GAME_NO = " + ddlGameName.SelectedValue + " AND SPORTS_NAME = '" + txtNameOfGameOrAchievement.Text + "'AND IDNO = " + idno + "AND SPORTS_DATE = CONVERT(DATE,'" + Convert.ToDateTime(txtDate.Text.Trim()).ToString("dd/MM/yyyy") + "',103)"));
                    if (count > 0)
                    {
                        objCommon.DisplayMessage("Sports Entry Already Done For this Date", this.Page);
                        ViewState["action"] = "add";
                        txtNameOfGameOrAchievement.Text = "";
                        txtDate.Text = "";
                        txtVenue.Text = "";
                        ddlLevelOfParticipation.SelectedIndex = 0;
                        ddlAchievement.SelectedIndex = 0;

                        lvSportsCertificate.DataSource = null;
                        lvSportsCertificate.DataBind();
                        return;
                    }

                    if (fuSportsCertificate.HasFile)
                    {
                        Check50KBFileSize();

                        if (validationflag == true)
                        {
                            //to upload multiple sports docs details
                            uploadMultipleSportsCertificate();
                            if (status == 1)
                            {
                                CustomStatus cs = (CustomStatus)objSC.AddSportsDetails(objStudent);
                                if (cs.Equals(CustomStatus.RecordSaved))
                                {
                                    objCommon.DisplayMessage("Sports Details Added Successfully!!", this.Page);
                                    ViewState["action"] = "add";
                                    ClearControls();
                                    BindSportsDetails();
                                }
                                else
                                    objCommon.DisplayMessage("Error!!", this.Page);
                            }
                        }

                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objSC.AddSportsDetails(objStudent);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage("Sports Details Added Successfully!!", this.Page);
                            ViewState["action"] = "add";
                            ClearControls();
                            BindSportsDetails();
                        }
                        else
                            objCommon.DisplayMessage("Error!!", this.Page);
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
                objUCommon.ShowError(Page, "ACADEMIC_StudentSportsDetails.btnSaveNext10_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    public void GetSportsDetails()
    {
        if (rdoType.SelectedValue == "1")
        {
            divSportsDetails.Visible = true;
            divStudentInfo.Visible = true;
            lblNameOf.Text = "Name Of The Tournament :";
            lblDate.Text = "Match Date :";
            divSportsBtnDetails.Visible = true;
            lvSportsDetails.Visible = true;
        }
        else
        {

            divSportsDetails.Visible = false;
            divStudentInfo.Visible = false;
            lblNameOf.Text = "Name Of The Tournament :";
            lblDate.Text = "Match Date :";
            divSportsBtnDetails.Visible = false;
            lvSportsDetails.Visible = false;
            //divSportsDetails.Visible = true;
            //lblNameOf.Text = "Name Of The Achievement :";
            //lblDate.Text = "Achievement Date :";
            //divSportsBtnDetails.Visible = true;
        }
    }

    protected void rdoType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSportsDetails();
    }


    #endregion

    protected void btnSportsEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            sportsno = int.Parse(btnEdit.CommandArgument);
            ShowDetails(sportsno);
            ViewState["action"] = "edit";
            txtNameOfGameOrAchievement.Focus();
            //  Response.Redirect("StudentInfo.aspx#step-9");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_studentSportsDetails.btnSportsEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //51200 for 50 kb
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        if (idno != 0)
        {
            string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).CommandArgument.ToString();
            string ContentType = string.Empty;

            //To Get the physical Path of the file(test.txt)

            // string filepath = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + idno.ToString() + "-" + txtNameOfGameOrAchievement.Text + "\\";
            string filepath = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + idno.ToString() + "\\";


            // string filepath = WebConfigurationManager.AppSettings["SVCE_SUBJECT_DOC"].ToString()+ "\\";

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

    private void ShowReport(string reportTitle, string rptFileName, string idNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_IDNO=" + idNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_LeaveAndHolidayEntry.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();

        txtEnrollmentSearch.Text = "";
        divSportsDetails.Visible = false;
        divSportsBtnDetails.Visible = false;
        divStudentInfo.Visible = false;
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (idno != 0)
            {
                ShowReport("Sports Details", "rptSportsDetailsReport.rpt", idno.ToString());
            }
            else
            {
                objCommon.DisplayMessage("Please Refresh the Page again!!", this.Page);
                return;
            }
        }
        catch { }
    }


    protected void lvSportsDetails_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Label lblSPORTS_NAME = (Label)e.Item.FindControl("lblSPORTS_NAME");
            ListView lvSportsDocsDetails = (ListView)e.Item.FindControl("lvSportsDocsDetails");

            DataSet dsCERT = objSC.GetCertificatesBySportsNo(Convert.ToInt32(lblSPORTS_NAME.ToolTip));
            if (dsCERT.Tables[0].Rows.Count > 0 && dsCERT != null)
            {
                lvSportsDocsDetails.DataSource = dsCERT.Tables[0];
                lvSportsDocsDetails.DataBind();
            }
            else
            {
                // objCommon.DisplayMessage("Sports Certificate Not Found!!", this.Page);
                lvSportsDocsDetails.DataSource = null;
                lvSportsDocsDetails.DataBind();
            }
        }
    }


    protected void btnDownloadFile_Click(object sender, EventArgs e)
    {
        LinkButton btndownloadfile = sender as LinkButton;
        string sportsname = (btndownloadfile.CommandArgument);
        if (idno != 0)
        {
            string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).ToolTip.ToString();
            string ContentType = string.Empty;

            //To Get the physical Path of the file(test.txt)

            //string filepath = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + idno.ToString() + "-" + sportsname + "\\";
            string filepath = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + idno.ToString() + "\\";

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


    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (idno != 0)
            {

                ImageButton btnDelete = sender as ImageButton;
                string filename = btnDelete.AlternateText;
                //string path = MapPath("~/CourseMaterial/");
                string path = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + idno.ToString() + "\\";
                string SPORTS_DOCS_NO = string.Empty;

                if (File.Exists(path + filename))
                {
                    File.Delete(path + filename);

                    string sportsno = (objCommon.LookUp("ACD_SPORTS_DOCS_DETAILS", "SPORTS_NO", "SPORTS_DOCS_NO = " + Convert.ToInt32(btnDelete.CommandArgument)));

                    int ret = objSC.DeleteSportsDocs(Convert.ToInt32(btnDelete.CommandArgument));
                    if (ret == 3)
                    {
                        objCommon.DisplayMessage("File Deleted Successfully!", this);
                    }

                    try
                    {
                        //to get uploaded Certificates
                        DataSet dsCERT = new DataSet();
                        dsCERT = objSC.GetCertificatesBySportsNo(Convert.ToInt32(sportsno));
                        if (dsCERT.Tables[0].Rows.Count > 0)
                        {
                            lvSportsCertificate.DataSource = dsCERT;
                            lvSportsCertificate.DataBind();
                        }
                        else
                        {
                            lvSportsCertificate.DataSource = null;
                            lvSportsCertificate.DataBind();
                        }


                        BindSportsDetails();
                    }
                    catch { }

                    //ShowDetails(sportsno); 
                }
                else
                {
                    objCommon.DisplayMessage("File Not Found!", this);
                    return;
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
}
