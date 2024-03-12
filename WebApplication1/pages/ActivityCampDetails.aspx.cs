//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : ACTIVITY CAMP DETAILS(NCC/NSS/CLUB)
// CREATION DATE : 02/06/2020
// CREATED BY    : SNEHA G.DOBLE
// MODIFIED BY   : 
// MODIFIED DATE : 
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

public partial class ACADEMIC_ActivityCampDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
     NCCActivityController objnccActivity = new NCCActivityController();
    NCCActivity objncc = new NCCActivity();

    static int idno = 0;
    int status = 0;
    static int Camp_no = 0;
    static bool validationflag = true;

    #region Page Event
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
                Response.Redirect("~/notauthorized.aspx?page=ActivityCampDetails.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ActivityCampDetails.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {       
        if (!Page.IsPostBack)
        {
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
             
                ViewState["studTbl"] = null;
                ViewState["action"] = "add";
                divActivityDetails.Visible = true;
                this.FillDropDown();
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
                    ClearControls();
                    divActivityDetails.Visible = false;

                }
              
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    #endregion

    #region Private Methods

    private void FillDropDown()
    {

        string Activity_type = string.Empty;
        string Activity_typenss = string.Empty;
        string Activity_typeclub = string.Empty;
        DataSet dsncc = objCommon.FillDropDown("ACD_ASSIGNING_FACULTY_NSS", "ACTIVITY_TYPE_NO", "ACTIVITY_TYPE", "ACTIVITY_TYPE_NO=1 and UA_NO = " + Session["userno"].ToString(), "UA_NO");
        DataSet dsnss = objCommon.FillDropDown("ACD_ASSIGNING_FACULTY_NSS", "ACTIVITY_TYPE_NO", "ACTIVITY_TYPE", " ACTIVITY_TYPE_NO=2 and UA_NO = " + Session["userno"].ToString(), "UA_NO");
        DataSet dsclub = objCommon.FillDropDown("ACD_ASSIGNING_FACULTY_NSS", "ACTIVITY_TYPE_NO", "ACTIVITY_TYPE", "ACTIVITY_TYPE_NO=3 and UA_NO = " + Session["userno"].ToString(), "UA_NO");
        if (dsncc.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsncc.Tables[0].Rows.Count; i++)
            {
                if (Activity_type == string.Empty) Activity_type = dsncc.Tables[0].Rows[i]["ACTIVITY_TYPE"].ToString();
                else
                    Activity_type += "," + dsncc.Tables[0].Rows[i]["ACTIVITY_TYPE"].ToString();
            }
            //NCC Activity
            objCommon.FillDropDownList(ddlNCCType, "ACD_NCC_TYPE", "NCC_TYPE_NO", "NCC_TYPE", "NCC_TYPE_NO IN(" + Activity_type + ") AND STATUS='ACTIVE'", "NCC_TYPE");
            //ddlNCCType.SelectedIndex= 1;
            BindCampActivityDetails();
            //Ration Activity
            objCommon.FillDropDownList(ddlNCCRation, "ACD_NCC_RATION", "NCC_RATION_NO", "NCC_RATION", "NCC_RATION_NO>0 AND STATUS='ACTIVE'", "NCC_RATION");
            BindStudData();
        }
        if (dsnss.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsnss.Tables[0].Rows.Count; i++)
            {
                if (Activity_typenss == string.Empty) Activity_typenss = dsnss.Tables[0].Rows[i]["ACTIVITY_TYPE"].ToString();
                else
                    Activity_typenss += "," + dsnss.Tables[0].Rows[i]["ACTIVITY_TYPE"].ToString();
            }
            //NSS Activity
            objCommon.FillDropDownList(ddlNSSType, "ACD_NSS_TYPE", "NSS_TYPE_NO", "NSS_TYPE", "NSS_TYPE_NO IN(" + Activity_typenss + ") AND STATUS='ACTIVE'", "NSS_TYPE");
            BindStudData();
        }

        if (dsclub.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsclub.Tables[0].Rows.Count; i++)
            {
                if (Activity_typeclub == string.Empty) Activity_typeclub = dsclub.Tables[0].Rows[i]["ACTIVITY_TYPE"].ToString();
                else
                    Activity_typeclub += "," + dsclub.Tables[0].Rows[i]["ACTIVITY_TYPE"].ToString();
            }
            //CLUB Activity
            objCommon.FillDropDownList(ddlCLUBType, "ACD_CLUB_ACTIVITY", "CLUB_TYPE_NO", "CLUB_TYPE", "CLUB_TYPE_NO IN(" + Activity_typeclub + ") AND STATUS='ACTIVE'", "CLUB_TYPE");
            BindStudData();
        }            
        
    }

    private void BindStudData()
    {
        try
        {
            //objncc.Ration_Type = 0;
            if (rdoactivity.SelectedValue == "1")
            {
                objncc.ActivityNo = 1;
                objncc.ActivityType = Convert.ToInt32(ddlNCCType.SelectedValue);
               // objncc.Ration_Type = Convert.ToInt32(ddlNCCRation.SelectedValue);
            }
            if (rdoactivity.SelectedValue == "2")
            {
                objncc.ActivityNo = 2;
                objncc.ActivityType = Convert.ToInt32(ddlNSSType.SelectedValue);
            }
            if (rdoactivity.SelectedValue == "3")
            {
                objncc.ActivityNo = 3;
                objncc.ActivityType = Convert.ToInt32(ddlCLUBType.SelectedValue);
            }
            DataSet ds = objnccActivity.GetStudDetailforCamp(Convert.ToInt32(Session["userno"].ToString()),objncc.ActivityNo, objncc.ActivityType);
            ViewState["studTbl"] = ds;
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvstudactivdata.Visible = true;
                lvstudactivdata.DataSource = ds;
                lvstudactivdata.DataBind();
                btnSubmit.Enabled = true;

                foreach (ListViewDataItem dataitem in lvstudactivdata.Items)
                {
                    int COUNT = 0;
                    CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;

                    HiddenField hfidno = dataitem.FindControl("hdfTempIdNo") as HiddenField;
                     COUNT = Convert.ToInt32((objCommon.LookUp("ACD_CAMP_ACTIVITY_DETAILS", "COUNT(*)", "ACTIVITY_TYPE_NO=" + objncc.ActivityNo + "and ACTIVITY_TYPE="+ objncc.ActivityType+" and IDNO=" +Convert.ToInt32( hfidno.Value)+"")));

                     if (COUNT > 0)
                     {
                         cbRow.Enabled = false;
                         //cbRow.BackColor = System.Drawing.Color.Green;
                         cbRow.Checked = true;
                         COUNT = 0;
                     }                 
                }
            }
            else
            {
                lvstudactivdata.Visible = false;
                lvstudactivdata.DataSource = null;
                lvstudactivdata.DataBind();
                btnSubmit.Enabled = false;
            }
       } 
        catch
        {
        }
    
    }

    private void BindCampActivityDetails()
    {
        try
        {
            if (rdoactivity.SelectedValue == "1")
            {
                objncc.ActivityNo = 1;
                objncc.ActivityType = Convert.ToInt32(ddlNCCType.SelectedValue);
            }
            if (rdoactivity.SelectedValue == "2")
            {
                objncc.ActivityNo = 2;
                objncc.ActivityType = Convert.ToInt32(ddlNSSType.SelectedValue);
            }
            if (rdoactivity.SelectedValue == "3")
            {
                objncc.ActivityNo = 3;
                objncc.ActivityType = Convert.ToInt32(ddlCLUBType.SelectedValue);
            }
            DataSet ds = objnccActivity.BindStudCampDetails(objncc.ActivityNo, objncc.ActivityType);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCampDetails.Visible = true;
                lvCampDetails.DataSource = ds;
                lvCampDetails.DataBind();
                lvstudactivdata.Visible = false;
            }
            else
            {
                lvCampDetails.Visible = false;
                lvCampDetails.DataSource = null;
                lvCampDetails.DataBind();
                lvstudactivdata.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Activity.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Check100KBFileSize()
    {
        try
        {
            if (rdoactivity.SelectedValue == "1")
            {
                objncc.ActivityNo = 1;
                if (ViewState["idno"].ToString() != "0")
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        string folderPath = WebConfigurationManager.AppSettings["SVCE_NCC_CERTIFICATE"].ToString();

                        HttpPostedFile fu = Request.Files[i];
                        if (fu.ContentLength > 0)
                        {
                            string ext = System.IO.Path.GetExtension(fu.FileName);
                            string filename = Path.GetFileName(fu.FileName);
                            if (ext == ".pdf" || ext == ".png" || ext == ".jpeg" || ext == ".jpg" || ext == ".PNG" || ext == ".JPEG" || ext == ".JPG" || ext == ".PDF") //|| ext == ".doc" || ext == ".docx" 
                            {
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
                }
            }

            if (rdoactivity.SelectedValue == "2")
            {
                objncc.ActivityNo = 2;
                if (ViewState["idno"].ToString() != "0")
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        string folderPath = WebConfigurationManager.AppSettings["SVCE_NSS_CERTIFICATE"].ToString();

                        HttpPostedFile fu = Request.Files[i];
                        if (fu.ContentLength > 0)
                        {
                            string ext = System.IO.Path.GetExtension(fu.FileName);
                            string filename = Path.GetFileName(fu.FileName);
                            if (ext == ".pdf" || ext == ".png" || ext == ".jpeg" || ext == ".jpg" || ext == ".PNG" || ext == ".JPEG" || ext == ".JPG" || ext == ".PDF") //|| ext == ".doc" || ext == ".docx" 
                            {
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
                }
            }

            if (rdoactivity.SelectedValue == "3")
            {
                objncc.ActivityNo = 3;
                if (ViewState["idno"].ToString() != "0")
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {

                        string folderPath = WebConfigurationManager.AppSettings["SVCE_CLUB_CERTIFICATE"].ToString();

                        HttpPostedFile fu = Request.Files[i];
                        if (fu.ContentLength > 0)
                        {
                            string ext = System.IO.Path.GetExtension(fu.FileName);
                            string filename = Path.GetFileName(fu.FileName);
                            if (ext == ".pdf" || ext == ".png" || ext == ".jpeg" || ext == ".jpg" || ext == ".PNG" || ext == ".JPEG" || ext == ".JPG" || ext == ".PDF") //|| ext == ".doc" || ext == ".docx" 
                            {
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
                }
            }

            objncc.nccFilename = objncc.nccFilename.TrimEnd(',');
            objncc.nccFilepath = objncc.nccFilepath.TrimEnd(',');

        }
        catch (Exception ex)
        {

        }
    }

    //function to upload multiple docs
    private void uploadMultipleNccCertificate()
    {
        try
        {
            if (rdoactivity.SelectedValue == "1")
            {
                objncc.ActivityNo = 1;

                if (ViewState["idno"].ToString() != "0")
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        string folderPath = WebConfigurationManager.AppSettings["SVCE_NCC_CERTIFICATE"].ToString();

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
                                    objncc.nccFilename += filename + ",";
                                    objncc.nccFilepath += folderPath + filename + ",";
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
                }
            }

            if (rdoactivity.SelectedValue == "2")
            {
                objncc.ActivityNo = 2;

                if (ViewState["idno"].ToString() != "0")
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {

                        string folderPath = WebConfigurationManager.AppSettings["SVCE_NSS_CERTIFICATE"].ToString();

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
                                    objncc.nccFilename += filename + ",";
                                    objncc.nccFilepath += folderPath + filename + ",";
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
                }
            }

            if (rdoactivity.SelectedValue == "3")
            {
                objncc.ActivityNo = 3;

                if (ViewState["idno"].ToString() != "0")
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {

                        string folderPath = WebConfigurationManager.AppSettings["SVCE_CLUB_CERTIFICATE"].ToString();

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
                                    objncc.nccFilename += filename + ",";
                                    objncc.nccFilepath += folderPath + filename + ",";
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
                }
            }

            ViewState["filename"] = objncc.nccFilename = objncc.nccFilename.TrimEnd(',');
            ViewState["filepath"] = objncc.nccFilepath = objncc.nccFilepath.TrimEnd(',');

        }
        catch (Exception ex)
        {
            // objCommon.DisplayMessage(this, "Something went wrong ! Please contact Admin", this.Page);

        }
    }
    //to clear all controls
    private void ClearControls()
    {
        ViewState["action"] = "add";
        txtCampName.Text = "";
        txtcampdetail.Text = "";
        txtFromDate.Text = "";
        txtTodate.Text = "";
        txtLocation.Text = "";
        ddlNCCRation.SelectedIndex = 0;
        ddlNCCType.SelectedIndex = 0;
        ddlNSSType.SelectedIndex = 0;
        ddlCLUBType.SelectedIndex = 0;
        lvstudactivdata.Visible = false;
    }

    #endregion
  
    #region SelectedIndexChanged

    protected void rdoactivity_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rdoactivity.SelectedValue == "1")
        {
            //ddlNCCType.SelectedIndex = 1;
            BindCampActivityDetails();
            divActivityDetails.Visible = true;
            divncc.Visible = true;
            divration.Visible = false;
            divnss.Visible = false;
            divclub.Visible = false;
            lvstudactivdata.Visible = false;
            ClearControls();
        }
        if (rdoactivity.SelectedValue == "2")
        {
            divActivityDetails.Visible = true;
            //NSS Activity
            FillDropDown();
            BindCampActivityDetails();
            divnss.Visible = true;
            divncc.Visible = false;
            divclub.Visible = false;
            divration.Visible = false;
            lvstudactivdata.Visible = false;
            ClearControls();

        }
        if (rdoactivity.SelectedValue == "3")
        {
            divActivityDetails.Visible = true;
            //CLUB Activity    
            FillDropDown();
            BindCampActivityDetails();
            divclub.Visible = true;
            divncc.Visible = false;
            divnss.Visible = false;
            divration.Visible = false;
            lvstudactivdata.Visible = false;
            ClearControls();
        }
    }

    protected void ddlNSSType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNSSType.SelectedIndex > 0)
        {
            BindStudData();
        }
        //BindCampActivityDetails();
        btnSubmit.Enabled = true;

    }

    protected void ddlCLUBType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCLUBType.SelectedIndex > 0)
        {
            BindStudData();
        }
        //BindCampActivityDetails();
        btnSubmit.Enabled = true;
    }

    protected void ddlNCCRation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNCCType.SelectedIndex > 0)
        {
            BindStudData();
        }
        //BindCampActivityDetails();        
        btnSubmit.Enabled = true;
    }

    protected void ddlNCCType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNCCType.SelectedIndex > 0)
        {
            BindStudData();
        }
        //BindCampActivityDetails();        
        btnSubmit.Enabled = true;
    }

    #endregion

    #region NoClickEvent

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            NCCActivity objncc = new NCCActivity();
            objncc.Ration_Type = 0;
            string idno = string.Empty;
            objncc.nccFilename = "";
            objncc.nccFilepath = "";

            foreach (ListViewDataItem lvItem in lvstudactivdata.Items)
            {
                CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
                HiddenField hdfTempIdNo = lvItem.FindControl("hdfTempIdNo") as HiddenField;
                if (chkBox.Checked == true && chkBox.Enabled == true)
                {
                    idno += chkBox.ToolTip + "$";
                    chkBox.Enabled = false;
                    if (rdoactivity.SelectedValue == "1")
                    {
                        objncc.ActivityNo = 1;
                        objncc.ActivityType = Convert.ToInt32(ddlNCCType.SelectedValue);
                        objncc.Ration_Type = Convert.ToInt32(ddlNCCRation.SelectedValue);
                    }

                    if (rdoactivity.SelectedValue == "2")
                    {
                        objncc.ActivityNo = 2;
                        objncc.ActivityType = Convert.ToInt32(ddlNSSType.SelectedValue);
                    }

                    if (rdoactivity.SelectedValue == "3")
                    {
                        objncc.ActivityNo = 3;
                        objncc.ActivityType = Convert.ToInt32(ddlCLUBType.SelectedValue);
                    }
                }
            }
            idno = idno.TrimEnd('$');
            objncc.IDNOS = idno;
            ViewState["idno"] = idno;

            objncc.campName = txtCampName.Text;
            objncc.Location = txtLocation.Text;
            objncc.campFromDate = Convert.ToDateTime(txtFromDate.Text);
            objncc.campToDate = Convert.ToDateTime(txtTodate.Text);

            DateTime d1 = Convert.ToDateTime(txtFromDate.Text);
            DateTime d2 = Convert.ToDateTime(txtTodate.Text);
            TimeSpan t = d2 - d1;
            double NrOfDays = t.TotalDays + 1;

            objncc.duration = Convert.ToDecimal(NrOfDays);
            objncc.UANO = Convert.ToInt32(Session["userno"].ToString());
            objncc.Camp_Detail = txtcampdetail.Text;

            if (fuNccCertificate.HasFile)
            {
                Check100KBFileSize();
                if (validationflag == true)
                {
                    uploadMultipleNccCertificate();
                }
            }


            if (ViewState["action"].ToString().Equals("add"))
            {
                if (objnccActivity.InsertStudentCampDetails(objncc.ActivityNo, objncc.ActivityType, objncc.Ration_Type, objncc.campName, objncc.Location, objncc.duration, objncc.UANO, objncc.IDNOS, objncc.campFromDate, objncc.campToDate, objncc.Camp_Detail, ViewState["filename"].ToString(), ViewState["filepath"].ToString()) == Convert.ToInt32(CustomStatus.RecordSaved))
                {
                    BindCampActivityDetails();
                    objCommon.DisplayMessage(this, "Record Saved Successfully..!!", this.Page);
                    btnSubmit.Enabled = false;
                    ClearControls();
                }
            }

            else if (ViewState["action"].ToString().Equals("edit"))
            {
                if (rdoactivity.SelectedValue == "1")
                {
                    objncc.ActivityNo = 1;
                    objncc.ActivityType = Convert.ToInt32(ddlNCCType.SelectedValue);
                    objncc.Ration_Type = Convert.ToInt32(ddlNCCRation.SelectedValue);
                }

                if (rdoactivity.SelectedValue == "2")
                {
                    objncc.ActivityNo = 2;
                    objncc.ActivityType = Convert.ToInt32(ddlNSSType.SelectedValue);
                }

                if (rdoactivity.SelectedValue == "3")
                {
                    objncc.ActivityNo = 3;
                    objncc.ActivityType = Convert.ToInt32(ddlCLUBType.SelectedValue);
                }

                objncc.Campno = Convert.ToInt32(ViewState["srno"].ToString());
                if (objnccActivity.UpdateStudentCampDetails(objncc.Campno, objncc.ActivityNo, objncc.ActivityType, objncc.Ration_Type, objncc.campName, objncc.Location, objncc.duration, objncc.campFromDate, objncc.campToDate, objncc.Camp_Detail) == Convert.ToInt32(CustomStatus.RecordUpdated))
                {
                    BindCampActivityDetails();
                    objCommon.DisplayMessage(this, "Record Updated Successfully..!!", this.Page);
                    btnSubmit.Enabled = false;
                    ClearControls();
                }
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

    protected void btnNccEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            divration.Visible = false;
            btnSubmit.Enabled = true;
            ViewState["action"] = "edit";
            ImageButton btnEdit = sender as ImageButton;
            idno = int.Parse(btnEdit.CommandArgument);
            int Activity_no = Convert.ToInt32((btnEdit.CommandName));
            ViewState["srno"] = idno;
            DataSet chkds = objCommon.FillDropDown("ACD_CAMP_ACTIVITY_DETAILS", "*", "ACTIVITY_TYPE_NO", "IDNO=" + idno + "AND ACTIVITY_TYPE_NO=" + Activity_no, string.Empty);
            if (chkds.Tables[0].Rows.Count > 0)
            {
                rdoactivity.SelectedValue = chkds.Tables[0].Rows[0]["ACTIVITY_TYPE_NO"].ToString();
                if (chkds.Tables[0].Rows[0]["ACTIVITY_TYPE_NO"].ToString() == "1")
                {
                    objCommon.FillDropDownList(ddlNCCType, "ACD_NCC_TYPE", "NCC_TYPE_NO", "NCC_TYPE", "NCC_TYPE_NO>0 AND STATUS='ACTIVE'", "NCC_TYPE");
                    ddlNCCType.SelectedValue = chkds.Tables[0].Rows[0]["ACTIVITY_TYPE"].ToString();
                    //ddlNCCRation.SelectedValue = chkds.Tables[0].Rows[0]["NCC_RATION_NO"].ToString();
                    divnss.Visible = false;
                    divncc.Visible = true;
                    //divration.Visible = true;
                    divclub.Visible = false;
                }
                else if (chkds.Tables[0].Rows[0]["ACTIVITY_TYPE_NO"].ToString() == "2")
                {
                    objCommon.FillDropDownList(ddlNSSType, "ACD_NSS_TYPE", "NSS_TYPE_NO", "NSS_TYPE", "NSS_TYPE_NO>0 AND STATUS='ACTIVE'", "NSS_TYPE");
                    ddlNSSType.SelectedValue = chkds.Tables[0].Rows[0]["ACTIVITY_TYPE"].ToString();
                    divnss.Visible = true;
                    divncc.Visible = false;
                    divclub.Visible = false;
                    //divration.Visible = false;
                }
                else if (chkds.Tables[0].Rows[0]["ACTIVITY_TYPE_NO"].ToString() == "3")
                {
                    objCommon.FillDropDownList(ddlCLUBType, "ACD_CLUB_ACTIVITY", "CLUB_TYPE_NO", "CLUB_TYPE", "CLUB_TYPE_NO>0 AND STATUS='ACTIVE'", "CLUB_TYPE");
                    ddlCLUBType.SelectedValue = chkds.Tables[0].Rows[0]["ACTIVITY_TYPE"].ToString();
                    divnss.Visible = false;
                    divncc.Visible = false;
                    divclub.Visible = true;
                    //divration.Visible = false;
                }
                txtCampName.Text = chkds.Tables[0].Rows[0]["CAMP_NAME"].ToString();
                txtLocation.Text = chkds.Tables[0].Rows[0]["CAMP_LOCATION"].ToString();
                txtFromDate.Text = chkds.Tables[0].Rows[0]["CAMP_FROM_DATE"].ToString();
                txtTodate.Text = chkds.Tables[0].Rows[0]["CAMP_TO_DATE"].ToString();
                txtcampdetail.Text = chkds.Tables[0].Rows[0]["CAMP_DETAILS"].ToString();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_studentNccDetails.btnNccEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lvCampDetails_ItemDataBound1(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
           // Button btnNccEdit = sender as Button;
            Label lblCAMP_NAME = (Label)e.Item.FindControl("lblCAMP_NAME");
            ListView lvNccDocsDetails = (ListView)e.Item.FindControl("lvNccDocsDetails");
            //int idno = Convert.ToInt32((btnNccEdit.CommandName));
            DataSet dsCERT = objnccActivity.GetCertificatesByCampNo(Convert.ToInt32(lblCAMP_NAME.ToolTip));
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
        int idno = Convert.ToInt32((btndownloadfile.CommandName));
        if (rdoactivity.SelectedValue == "1")
        {
            objncc.ActivityNo = 1;
            if (idno != 0)
            {
                string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).ToolTip.ToString();
                string ContentType = string.Empty;

                //To Get the physical Path of the file(test.txt)        
                string filepath = WebConfigurationManager.AppSettings["SVCE_NCC_CERTIFICATE"].ToString() + "\\";

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
        if (rdoactivity.SelectedValue == "2")
        {
            objncc.ActivityNo = 2;
            if (idno != 0)
            {
                string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).ToolTip.ToString();
                string ContentType = string.Empty;

                //To Get the physical Path of the file(test.txt)    
                string filepath = WebConfigurationManager.AppSettings["SVCE_NSS_CERTIFICATE"].ToString() + "\\";

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
        if (rdoactivity.SelectedValue == "3")
        {
            objncc.ActivityNo = 3;
            if (idno != 0)
            {
                string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).ToolTip.ToString();
                string ContentType = string.Empty;

                //To Get the physical Path of the file(test.txt)     
                string filepath = WebConfigurationManager.AppSettings["SVCE_CLUB_CERTIFICATE"].ToString() + "\\";

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
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    #endregion
  
}


    

