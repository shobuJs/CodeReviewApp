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


public partial class ACADEMIC_StudentNCCDetails : System.Web.UI.Page
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
                Response.Redirect("~/notauthorized.aspx?page=StudentNCCDetails.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentNCCDetails.aspx");
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
                    ////to show only sports selected in readiobuttonlist
                    //rdoType.SelectedValue = "1";
                    GetNCCHIDESHOWDetails();
                    ////  BindSportsDetails();
                }
                else
                {
                    pnlSearch.Visible = true;

                    //divStudentInfo.Visible = true;
                    //divNCCDetails.Visible = true;
                    //divNCCBtnDetails.Visible = true;
                    //lvSportsDetails.Visible = true;
                }


                ViewState["action"] = "add";
                ///Added by Neha Baranwal - 02 Oct 19
                objCommon.FillDropDownList(ddlNCCType, "ACD_NCC_TYPE", "NCC_TYPE_NO", "NCC_TYPE", "NCC_TYPE_NO>0", "NCC_TYPE_NO");
                objCommon.FillDropDownList(ddlNCCRation, "ACD_NCC_RATION", "NCC_RATION_NO", "NCC_RATION", "NCC_RATION_NO>0", "NCC_RATION_NO");
               

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
                    divNCCBtnDetails.Visible = false;
                    divNCCDetails.Visible = false;
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
        BindNccDetails();
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
                        string semester = objCommon.LookUp("ACD_SEMESTER", "SEMESTERNAME", "SEMESTERNO=" + dtr["SEMESTERNO"].ToString());
                        lblSemester.Text = semester;

                        lblMailID.Text = dtr["EMAILID"] == null ? string.Empty : dtr["EMAILID"].ToString();

                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dtr["IDNO"].ToString() + "&type=student";
                        //imgSign.ImageUrl = "~/showimage.aspx?id=" + dtr["IDNO"].ToString() + "&type=STUDENTSIGN";



                        divStudentInfo.Visible = true;
                    }
                }


                DataSet ds1 = null;
                ds1 = objSC.GetAllNCCDetails(Convert.ToInt32(idno));
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    lvNccDetails.DataSource = ds1.Tables[0];
                    lvNccDetails.DataBind();
                    //to show only sports selected in readiobuttonlist
                    //rdoType.SelectedValue = "1";
                    GetNCCHIDESHOWDetails();
                    btnReport.Enabled = true;

                    // BindLV();
                    //BindSportsDetails();
                }
                else
                {
                    lvNccDetails.DataSource = null;
                    lvNccDetails.DataBind();
                    //to show only sports selected in readiobuttonlist
                    //rdoType.SelectedValue = "1";
                    GetNCCHIDESHOWDetails();
                    btnReport.Enabled = false;
                    //BindSportsDetails();
                }

            }
            else
            {
                objCommon.DisplayMessage("No student found having Univ. Reg. No.: " + txtEnrollmentSearch.Text.Trim(), this.Page);
                txtEnrollmentSearch.Focus();
                ClearControls();
                divNCCBtnDetails.Visible = false;
                divNCCDetails.Visible = false;
                divStudentInfo.Visible = false;
                return;
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentNCCDetails.ShowDetails()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


 

    #region "Step-9: NCC Details"
    /// <summary>
    /// step--999
    /// Added by NEha Baranwal (02/10/2019)
    /// Sports And Achievement
    /// Added by Neha Baranwal
    /// </summary>

    Student objStudent = new Student();
    StudentController objSC = new StudentController();
    int status = 0;
    static int Nccno = 0;
    static bool validationflag = true;

    public void BindNccDetails()
    {
        if (idno != 0)
        {
            DataSet ds = null;
            ds = objSC.GetAllNCCDetails(Convert.ToInt32(idno));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvNccDetails.DataSource = ds.Tables[0];
                lvNccDetails.DataBind();

                //gvSportsDetails.DataSource = ds.Tables[0];
                //gvSportsDetails.DataBind();

                btnReport.Enabled = true;
                //BindLV();
            }
            else
            {
                //gvSportsDetails.DataSource = null;
                //gvSportsDetails.DataBind();
                lvNccDetails.DataSource = null;
                lvNccDetails.DataBind();
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
        txtCampName.Text = "";
       
        txtFromDate.Text = "";
        txtTodate.Text = "";
        txtLocation.Text = "";
        ddlNCCRation.SelectedIndex = 0;
        ddlNCCType.SelectedIndex = 0;
      
        lvNccCertificate.DataSource = null;
        lvNccCertificate.DataBind();
        lvNccDetails.DataSource = null;
        lvNccDetails.DataBind();
        //lblStatus.Text = string.Empty;

        DataSet ds = null;
    }

    //to show sports details on edit button
    private void ShowDetails(int Nccno)
    {
        try
        {
            SqlDataReader dr = objSC.GetNCCDetails(Nccno);
            if (dr != null)
            {
                if (dr.Read())
                {
                    ddlNCCType.SelectedValue = dr["NCC_TYPE_NO"] == DBNull.Value ? "-1" : dr["NCC_TYPE_NO"].ToString();
                    ddlNCCRation.SelectedValue = dr["NCC_RATION_NO"] == DBNull.Value ? "-1" : dr["NCC_RATION_NO"].ToString();
                    txtCampName.Text = dr["CAMP_NAME"] == DBNull.Value ? string.Empty : dr["CAMP_NAME"].ToString();
                    txtLocation.Text = dr["CAMP_LOCATION"] == DBNull.Value ? string.Empty : dr["CAMP_LOCATION"].ToString();
                    txtFromDate.Text = dr["CAMP_FROM_DATE"] == DBNull.Value ? "0" : dr["CAMP_FROM_DATE"].ToString();
                    txtTodate.Text = dr["CAMP_TO_DATE"] == DBNull.Value ? "0" : dr["CAMP_TO_DATE"].ToString();
                    //ddlLevelOfParticipation.SelectedValue = dr["PARTICIPATION_NO"] == DBNull.Value ? "-1" : dr["PARTICIPATION_NO"].ToString();
                    //ddlAchievement.SelectedValue = dr["ACHIEVEMENT_NO"] == DBNull.Value ? "-1" : dr["ACHIEVEMENT_NO"].ToString();


                    //to get uploaded Certificates
                    DataSet dsCERT = new DataSet();

                    dsCERT = objSC.GetCertificatesByNCCNo(Nccno);
                    if (dsCERT.Tables[0].Rows.Count > 0)
                    {
                        lvNccCertificate.DataSource = dsCERT;
                        lvNccCertificate.DataBind();
                    }
                    else
                    {
                        lvNccCertificate.DataSource = null;
                        lvNccCertificate.DataBind();
                    }
                }
                dr.Close();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentNccDetails.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    ////function to upload single docs
    //private void uploadSportsCertificate()
    //{

    //    try
    //    {
    //        if (idno != 0)
    //        {
    //            //string folderPath = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + txtCCode.Text + "\\";
    //            string folderPath = WebConfigurationManager.AppSettings["SVCE_NCC_CERTIFICATE"].ToString() + idno.ToString() + "-" + txtCampName.Text + "\\";
    //            if (fuSportsCertificate.HasFile)
    //            {
    //                string ext = System.IO.Path.GetExtension(fuSportsCertificate.PostedFile.FileName);
    //                HttpPostedFile file = fuSportsCertificate.PostedFile;
    //                string filename = Path.GetFileName(fuSportsCertificate.PostedFile.FileName) + idno.ToString() + "-" + txtCampName.Text;
    //                if (ext == ".pdf" || ext == ".png" || ext == ".jpeg" || ext == ".jpg" || ext == ".PNG" || ext == ".JPEG" || ext == ".JPG" || ext == ".PDF") //|| ext == ".doc" || ext == ".docx" 
    //                {
    //                    //if (file.ContentLength <= 51200)// 31457280 before size  //For Allowing 50 Kb Size Files only 
    //                    //{
    //                    if (file.ContentLength <= 102400)// 31457280 before size  //For Allowing 100 Kb Size Files only 
    //                    {
    //                        string contentType = fuSportsCertificate.PostedFile.ContentType;
    //                        if (!Directory.Exists(folderPath))
    //                        {
    //                            Directory.CreateDirectory(folderPath);
    //                        }
    //                        objStudent.sportsFilename = filename;
    //                        objStudent.sportsFilepath = folderPath + filename;
    //                        fuSportsCertificate.PostedFile.SaveAs(folderPath + filename);
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

    //                objStudent.sportsFilename = "";
    //                objStudent.sportsFilepath = "";
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



    private void Check100KBFileSize()
    {
        try
        {
            if (idno != 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    //string folderPath = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + idno.ToString() + "-" + txtNameOfGameOrAchievement.Text + "\\";
                    string folderPath = WebConfigurationManager.AppSettings["SVCE_NCC_CERTIFICATE"].ToString() + idno.ToString() + "\\";

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
                objStudent.nccFilename = objStudent.nccFilename.TrimEnd(',');
                objStudent.nccFilepath = objStudent.nccFilepath.TrimEnd(',');
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
    private void uploadMultipleNccCertificate()
    {
        try
        {
            if (idno != 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    //string folderPath = WebConfigurationManager.AppSettings["SVCE_SPORTS_CERTIFICATE"].ToString() + idno.ToString() + "-" + txtNameOfGameOrAchievement.Text + "\\";
                    string folderPath = WebConfigurationManager.AppSettings["SVCE_NCC_CERTIFICATE"].ToString() + idno.ToString() + "\\";

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
                                objStudent.nccFilename += filename + ",";
                                objStudent.nccFilepath += folderPath + filename + ",";
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
                objStudent.nccFilename = objStudent.nccFilename.TrimEnd(',');
                objStudent.nccFilepath = objStudent.nccFilepath.TrimEnd(',');
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

    //add ncc details
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["action"] != null && idno != 0)
            {
                if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtTodate.Text))
                {
                    objCommon.DisplayMessage("Please Select Dates Properly", this.Page);
                }
                else{
      
                objStudent.campName = txtCampName.Text;
                objStudent.Location = txtLocation.Text;
                objStudent.campFromDate = Convert.ToDateTime(txtFromDate.Text);
                objStudent.campToDate = Convert.ToDateTime(txtTodate.Text);
                objStudent.nccTypeNo = Convert.ToInt32(ddlNCCType.SelectedValue);
                objStudent.nccRationNo = Convert.ToInt32(ddlNCCRation.SelectedValue);
                objStudent.idno = Convert.ToInt32(idno);


                DateTime d1 = Convert.ToDateTime(txtFromDate.Text);
                DateTime d2 = Convert.ToDateTime(txtTodate.Text);
                TimeSpan t = d2 - d1;
                double NrOfDays = t.TotalDays + 1;

                objStudent.duration =Convert.ToDecimal(NrOfDays);

                objStudent.nccFilename = "";
                objStudent.nccFilepath = "";

                if (ViewState["action"].ToString().Equals("edit"))
                {
                    //check entry already done for this camp and on same date
                    int count = Convert.ToInt32(objCommon.LookUp("ACD_NCC_DETAILS", "count(*)", "NCC_TYPE_NO = " + ddlNCCType.SelectedValue + " AND NCC_RATION_NO = " + ddlNCCRation.SelectedValue + " AND CAMP_NAME = '" + txtCampName.Text + "'AND IDNO = " + idno + " AND NCC_NO <> " + Nccno));
                    if (count > 0)
                    {
                        objCommon.DisplayMessage("NCC Entry Already Done.", this.Page);
                        ViewState["action"] = "add";

                        txtCampName.Text = "";
                        txtFromDate.Text = "";
                        txtTodate.Text = "";
                        txtLocation.Text = "";
                        ddlNCCRation.SelectedIndex = 0;
                        ddlNCCType.SelectedIndex = 0;

                        lvNccCertificate.DataSource = null;
                        lvNccCertificate.DataBind();
                        return;
                    }
                    objStudent.nccNo = Nccno;



                    if (fuNccCertificate.HasFile)
                    {
                        Check100KBFileSize();

                        if (validationflag == true)
                        {
                            //to upload multiple ncc docs details
                            uploadMultipleNccCertificate();
                            //uploadSportsCertificate();
                            if (status == 1)
                            {
                                CustomStatus cs = (CustomStatus)objSC.UpdateNCCDetails(objStudent);
                                if (cs.Equals(CustomStatus.RecordUpdated))
                                {
                                    objCommon.DisplayMessage("NCC Details Modified Successfully!!", this.Page);
                                    ViewState["action"] = "add";
                                    ClearControls();
                                    BindNccDetails();
                                }
                                else
                                    objCommon.DisplayMessage("Error!!", this.Page);
                            }
                        }
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objSC.UpdateNCCDetails(objStudent);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage("NCC Details Modified Successfully!!", this.Page);
                            ViewState["action"] = "add";
                            ClearControls();
                            BindNccDetails();
                        }
                        else
                            objCommon.DisplayMessage("Error!!", this.Page);
                    }


                }
                //to add
                else
                {

                    //check entry already done for this camp and on same date
                    int count = Convert.ToInt32(objCommon.LookUp("ACD_NCC_DETAILS", "count(*)", "NCC_TYPE_NO = " + ddlNCCType.SelectedValue + " AND NCC_RATION_NO = " + ddlNCCRation.SelectedValue + " AND CAMP_NAME = '" + txtCampName.Text + "'AND IDNO = " + idno + " AND NCC_NO <> " + Nccno));
                    if (count > 0)
                    {
                        objCommon.DisplayMessage("NCC Entry Already Done.", this.Page);
                        ViewState["action"] = "add";
                        txtCampName.Text = "";
                        txtFromDate.Text = "";
                        txtTodate.Text = "";
                        txtLocation.Text = "";
                        ddlNCCRation.SelectedIndex = 0;
                        ddlNCCType.SelectedIndex = 0;
                        lvNccCertificate.DataSource = null;
                        lvNccCertificate.DataBind();
                        return;
                    }

                    if (fuNccCertificate.HasFile)
                    {
                        Check100KBFileSize();

                        if (validationflag == true)
                        {
                            //to upload multiple sports docs details
                            uploadMultipleNccCertificate();
                            if (status == 1)
                            {
                                CustomStatus cs = (CustomStatus)objSC.AddNCCDetails(objStudent);
                                if (cs.Equals(CustomStatus.RecordSaved))
                                {
                                    objCommon.DisplayMessage("NCC Details Added Successfully!!", this.Page);
                                    ViewState["action"] = "add";
                                    ClearControls();
                                    BindNccDetails();
                                }
                                else
                                    objCommon.DisplayMessage("Error!!", this.Page);
                            }
                        }

                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objSC.AddNCCDetails(objStudent);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage("NCC Details Added Successfully!!", this.Page);
                            ViewState["action"] = "add";
                            ClearControls();
                            BindNccDetails();
                        }
                        else
                            objCommon.DisplayMessage("Error!!", this.Page);
                    }

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
                objUCommon.ShowError(Page, "ACADEMIC_StudentNCCDetails.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    public void GetNCCHIDESHOWDetails()
    {
            divNCCDetails.Visible = true;
            divStudentInfo.Visible = true;
            divNCCBtnDetails.Visible = true;
            lvNccDetails.Visible = true;
    }

    //protected void rdoType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetSportsDetails();
    //}


    #endregion

    protected void btnNccEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            Nccno = int.Parse(btnEdit.CommandArgument);
            ShowDetails(Nccno);
            ViewState["action"] = "edit";
            ddlNCCType.Focus();
            //  Response.Redirect("StudentInfo.aspx#step-9");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_studentNccDetails.btnNccEdit_Click-> " + ex.Message + " " + ex.StackTrace);
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
            string filepath = WebConfigurationManager.AppSettings["SVCE_NCC_CERTIFICATE"].ToString() + idno.ToString() + "\\";


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
                objUCommon.ShowError(Page, "ACADEMIC_LeaveAndHolidayEntry.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();

        txtEnrollmentSearch.Text = "";
        divNCCDetails.Visible = false;
        divNCCBtnDetails.Visible = false;
        divStudentInfo.Visible = false;
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (idno != 0)
            {
                ShowReport("NCC Details", "rptNCCDetailsReport.rpt", idno.ToString());
            }
            else
            {
                objCommon.DisplayMessage("Please Refresh the Page again!!", this.Page);
                return;
            }
        }
        catch { }
    }


    protected void lvNccDetails_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Label lblCAMP_NAME = (Label)e.Item.FindControl("lblCAMP_NAME");
            ListView lvNccDocsDetails = (ListView)e.Item.FindControl("lvNccDocsDetails");

            DataSet dsCERT = objSC.GetCertificatesByNCCNo(Convert.ToInt32(lblCAMP_NAME.ToolTip));
            if (dsCERT.Tables[0].Rows.Count > 0 && dsCERT != null)
            {
                lvNccDocsDetails.DataSource = dsCERT.Tables[0];
                lvNccDocsDetails.DataBind();
            }
            else
            {
                // objCommon.DisplayMessage("Sports Certificate Not Found!!", this.Page);
                lvNccDocsDetails.DataSource = null;
                lvNccDocsDetails.DataBind();
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
            string filepath = WebConfigurationManager.AppSettings["SVCE_NCC_CERTIFICATE"].ToString() + idno.ToString() + "\\";

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
                string path = WebConfigurationManager.AppSettings["SVCE_NCC_CERTIFICATE"].ToString() + idno.ToString() + "\\";
                string NCC_DOCS_NO = string.Empty;

                if (File.Exists(path + filename))
                {
                    File.Delete(path + filename);

                    string Nccno = (objCommon.LookUp("ACD_NCC_DOCS_DETAILS", "NCC_NO", "NCC_DOCS_NO = " + Convert.ToInt32(btnDelete.CommandArgument)));

                    int ret = objSC.DeleteNCCDocs(Convert.ToInt32(btnDelete.CommandArgument));
                    if (ret == 3)
                    {
                        objCommon.DisplayMessage("File Deleted Successfully!", this);
                    }

                    try
                    {
                        //to get uploaded Certificates
                        DataSet dsCERT = new DataSet();
                        dsCERT = objSC.GetCertificatesByNCCNo(Convert.ToInt32(Nccno));
                        if (dsCERT.Tables[0].Rows.Count > 0)
                        {
                            lvNccCertificate.DataSource = dsCERT;
                            lvNccCertificate.DataBind();
                        }
                        else
                        {
                            lvNccCertificate.DataSource = null;
                            lvNccCertificate.DataBind();
                        }


                        BindNccDetails();
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
