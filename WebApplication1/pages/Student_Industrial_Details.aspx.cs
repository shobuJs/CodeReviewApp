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
using DynamicAL;
using System.IO;
using System.Data.SqlClient;



public partial class ACADEMIC_Student_Industrial_Details : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    //StudentRegistrationModel objStudReg = new StudentRegistrationModel();  
    StudentInformation objStudReg = new StudentInformation();
    StudentRegistrationController objStudRegC = new StudentRegistrationController();
    DynamicControllerAL AL = new DynamicControllerAL();

    #region Page Event

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    //  this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    PopulateDropDownList();
                    string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]) + " AND UA_NO=" + Convert.ToInt32(Session["userno"]));
                    ViewState["usertype"] = ua_type;
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    string ipaddress = ViewState["ipAddress"].ToString();
                    ViewState["action"] = "add";
                    GetIndustrialDetails();
                    GetIndustrialLinkDetails();
                }
            }

            //Blank Div
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentAdmission_Register.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentAdmission_Register.aspx");
        }
    }

    #endregion

    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO>0", "");
        objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO");
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
    }

    private void GetIndustrialDetails()
    {
        DataSet dsvisit = objStudRegC.GetIndustrialVisitDetails(Convert.ToInt32(ddlBranch.SelectedValue.Split('-')[0]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue));
        if (dsvisit.Tables[0].Rows.Count > 0)
        {
            lvIndustrialVisit.DataSource = dsvisit;
            lvIndustrialVisit.DataBind();
            lvIndustrialVisit.Visible = true;
        }
        else
        {
            lvIndustrialVisit.DataSource = null;
            lvIndustrialVisit.DataBind();
            lvIndustrialVisit.Visible = false;
        }
    }

    private void GetStudentList()
    {
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT ASR ON (ASR.IDNO=S.IDNO)", "DISTINCT S.IDNO", "S.REGNO,CONCAT(STUDNAME,' ',STUDLASTNAME)STUDENTNAME", "S.BRANCHNO=" + ddlBranch.SelectedValue.Split('-')[0] + "AND ASR.SEMESTERNO=" + ddlSemester.SelectedValue + "AND ASR.SECTIONNO=" + ddlSection.SelectedValue + "AND ADMBATCH=" + ddlAdmBatch.SelectedValue + "AND PREV_STATUS=0 AND ISNULL(CAN,0)=0", "REGNO");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudentList.DataSource = ds;
            lvStudentList.DataBind();
            lvStudentList.Visible = true;
        }
        else
        {
            lvStudentList.DataSource = null;
            lvStudentList.DataBind();
            lvStudentList.Visible = false;
            objCommon.DisplayMessage(this.Page, "No Students Found For this Selection", this.Page);
        }
    }

    private void GetIndustrialLinkDetails()
    {
        //INNER JOIN ACD_STUD_INDUSTRIAL_LINK_DOCS ILDS ON(ILD.INDLINKNO=ILDS.INDLINKNO)
        //DataSet dsLink = objCommon.FillDropDown("ACD_STUD_INDUSTRIAL_LINK_DETAILS ILD ", "DISTINCT ILD.INDLINKNO", "ILD.COMPANY_NAME,COMPANY_ADDRESS,CONVERT(VARCHAR(20),MOU_FROM,(103))MOU_FROM,	CONVERT (VARCHAR(20),MOU_TO,(103))MOU_TO,(CASE WHEN MOU_TYPE = 1 THEN 'Visit' ELSE 'Project' END)MOUTYPE,MOU_TYPE,ACTIVITIES,REMARKS,(CASE WHEN LIVE_STATUS = 1 THEN 'YES' ELSE 'NO' END)LIVESTATUS,LIVE_STATUS,ILD.STUD_PART,TEA_PART,LINK", "", "ILD.INDLINKNO"); Commented on 24/08/2020 
        DataSet dsLink = objCommon.FillDropDown("ACD_STUD_INDUSTRIAL_LINK_DETAILS ILD ", "DISTINCT ILD.INDLINKNO", "ILD.COMPANY_NAME,COMPANY_ADDRESS,CONVERT(VARCHAR(20),MOU_FROM,(103))MOU_FROM,	CONVERT (VARCHAR(20),MOU_TO,(103))MOU_TO,(CASE WHEN MOU_TYPE = 1 THEN 'Industrial' ELSE 'Institute' END)MOUTYPE,MOU_TYPE,ACTIVITIES,REMARKS,(CASE WHEN LIVE_STATUS = 1 THEN 'YES' ELSE 'NO' END)LIVESTATUS,LIVE_STATUS,ILD.STUD_PART,TEA_PART,LINK", "", "ILD.INDLINKNO");
        ViewState["Table"] = dsLink;
        if (dsLink.Tables[0].Rows.Count > 0)
        {
            LvIndustrialLink.DataSource = dsLink;
            LvIndustrialLink.DataBind();
            LvIndustrialLink.Visible = true;
        }
        else
        {
            LvIndustrialLink.DataSource = null;
            LvIndustrialLink.DataBind();
            LvIndustrialLink.Visible = false;
        }
    }

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetStudentList();
        lvStudentList.Visible = true;
        lvIndustrialVisit.Visible = false;
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            string str = ddlBranch.SelectedValue;
            string degreeno = str.Substring(str.IndexOf("-")+1);
            int yearwise = objCommon.LookUp("ACD_DEGREE", "ISNULL(YEARWISE,0)", "DEGREENO=" + degreeno.ToString()) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "ISNULL(YEARWISE,0)", "DEGREENO=" + degreeno.ToString()));

            if (yearwise == 1)
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");
            }
            else
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            }
        }

        ddlAdmBatch.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        lvStudentList.Visible = false;
        ddlSemester.Focus();
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        lvStudentList.Visible = false;
        ddlSection.Focus();
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlAdmBatch.SelectedIndex = 0;
        lvStudentList.Visible = false;
        ddlAdmBatch.Focus();
    }

    protected void btnSubmitvisit_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (ListViewDataItem dataitem in lvStudentList.Items)
        {
            CheckBox cbRow = dataitem.FindControl("chkReport") as CheckBox;
            if (cbRow.Checked == true)
                count++;
        }
        if (count <= 0 && btnSubmitvisit.Text == "Submit")
        {
            objCommon.DisplayMessage(this.Page, "Please select atleast one student !", this);
            return;
        }
        AddtoTable();
        DataTable dt = new DataTable();
        dt = (DataTable)Session["Industrial_visit"];

        try
        {
            objStudReg.Student_data = dt;
            objStudReg.Industry_name = txtNameIndustry.Text.Replace(',', '^');
            objStudReg.Address_industry = txtAdresIndustry.Text.Replace(',', '^');
            objStudReg.Visit_purpose = txtVisitDetails.Text.Replace(',', '^');
            objStudReg.Visit_date = Convert.ToDateTime(txtDateofVisit.Text);
            objStudReg.no_Students = txtnoStudents.Text;
            objStudReg.no_Faculities = txtnoFaculties.Text;
            objStudReg.branchno = Convert.ToInt32(ddlBranch.SelectedValue.Split('-')[0]);
            objStudReg.semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            objStudReg.sectiono = Convert.ToInt32(ddlSection.SelectedValue);
            objStudReg.admyear = Convert.ToInt32(ddlAdmBatch.SelectedItem.Text);
            objStudReg.Ua_no = Convert.ToInt32(Session["userno"]);
            objStudReg.Ip_address = ViewState["ipAddress"].ToString();
            objStudReg.College_code = Session["colcode"].ToString();
            int degreeno = Convert.ToInt32(ddlBranch.SelectedValue.Split('-')[1]);

            int VisitNo = 0;
            if (ViewState["VISITNO"] != null)
            {
                VisitNo = Convert.ToInt32(ViewState["VISITNO"].ToString());
            }

            objStudReg.Indus_file_name = "";
            objStudReg.Indus_file_path = "";

            if (FuIndustrial.HasFile)
            {
                Check50KBFileSize(FuIndustrial);

                if (validationflag == true)
                {
                    //to upload multiple sports docs details
                    uploadMultipleSportsCertificate(FuIndustrial, "IndustrialVisit");
                    if (status == 1)
                    {
                        string SP_Name = "PKG_ACD_STUDENT_INDUSTRIAL_VISIT_DETAILS";
                        string SP_Parameters = "@P_INDUSTRIAL_TBL ,@P_INDUSTRY_NAME ,@P_ADDRESS_INDUSTRY ,@P_VISIT_DETAILS ,@P_VISIT_DATE ,@P_STUDENTSNO_VISIT ,@P_FACULTIESNO_VISIT ,@P_BRANCHNO ,@P_SEMESTERNO ,@P_SECTIONNO ,@P_ADM_YEAR ,@P_UA_NO ,@P_IP_ADDRESS ,@P_COLLEGE_CODE ,@P_INDUSTRIAL_VISIT_FILE_NAME ,@P_INDUSTRIAL_VISIT_FILE_PATH ,@P_DEGREENO, @P_ISFILE, @P_VISITNO";

                        string Call_Values = "0, " + objStudReg.Industry_name + ", " + objStudReg.Address_industry + ", " + objStudReg.Visit_purpose + ", " + Convert.ToString(txtDateofVisit.Text) + ", " + objStudReg.no_Students + ", " + objStudReg.no_Faculities + ", " + objStudReg.branchno + ", " + objStudReg.semesterno + ", " + objStudReg.sectiono + ", " + objStudReg.admyear + ", " + objStudReg.Ua_no + ", " + objStudReg.Ip_address + ", " + objStudReg.College_code + ", " + objStudReg.Indus_file_name + ", " + objStudReg.Indus_file_path + "," + degreeno + ",1," + VisitNo + "";

                        string que_out = "";

                        if (btnSubmitvisit.Text == "Update")
                        {
                            DataTable dtX = new DataTable();
                            dtX.Columns.Add("SRNO");
                            dtX.Columns.Add("IDNO");
                            dtX.Columns.Add("STUDNAME");

                            que_out = AL.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, dtX, true, 2);
                        }
                        else
                        {
                            que_out = AL.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, dt, true, 2);
                        }


                        if (que_out == "1")
                        {
                            objCommon.DisplayMessage(this.Page, "Industrial Visit Details Saved Successfully !!!", this.Page);
                        }
                        else if (que_out == "2")
                        {
                            objCommon.DisplayMessage(this.Page, "Industrial Visit Details Updated Successfully !!!", this.Page);
                        }

                        IndustrialCancel();
                        DataSet dsvisit = objStudRegC.GetIndustrialVisitDetails(Convert.ToInt32(ddlBranch.SelectedValue.Split('-')[0]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue));
                        if (dsvisit.Tables[0].Rows.Count > 0)
                        {
                            lvIndustrialVisit.DataSource = dsvisit;
                            lvIndustrialVisit.DataBind();
                            lvIndustrialVisit.Visible = true;
                        }
                        else
                        {
                            lvIndustrialVisit.DataSource = null;
                            lvIndustrialVisit.DataBind();
                            lvIndustrialVisit.Visible = false;
                        }
                    }
                }

            }
            else
            {
                string SP_Name = "PKG_ACD_STUDENT_INDUSTRIAL_VISIT_DETAILS";
                string SP_Parameters = "@P_INDUSTRIAL_TBL ,@P_INDUSTRY_NAME ,@P_ADDRESS_INDUSTRY ,@P_VISIT_DETAILS ,@P_VISIT_DATE ,@P_STUDENTSNO_VISIT ,@P_FACULTIESNO_VISIT ,@P_BRANCHNO ,@P_SEMESTERNO ,@P_SECTIONNO ,@P_ADM_YEAR ,@P_UA_NO ,@P_IP_ADDRESS ,@P_COLLEGE_CODE ,@P_INDUSTRIAL_VISIT_FILE_NAME ,@P_INDUSTRIAL_VISIT_FILE_PATH ,@P_DEGREENO, @P_ISFILE, @P_VISITNO";

                string Call_Values = "0, " + objStudReg.Industry_name + ", " + objStudReg.Address_industry + ", " + objStudReg.Visit_purpose + ", " + Convert.ToString(txtDateofVisit.Text) + ", " + objStudReg.no_Students + ", " + objStudReg.no_Faculities + ", " + objStudReg.branchno + ", " + objStudReg.semesterno + ", " + objStudReg.sectiono + ", " + objStudReg.admyear + ", " + objStudReg.Ua_no + ", " + objStudReg.Ip_address + ", " + objStudReg.College_code + ", " + objStudReg.Indus_file_name + ", " + objStudReg.Indus_file_path + "," + degreeno + ",0," + VisitNo + "";

                string que_out = "";

                if (btnSubmitvisit.Text == "Update")
                {
                    DataTable dtX = new DataTable();
                    dtX.Columns.Add("SRNO");
                    dtX.Columns.Add("IDNO");
                    dtX.Columns.Add("STUDNAME");

                    que_out = AL.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, dtX, true, 2);
                }
                else
                {
                    que_out = AL.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, dt, true, 2);
                }


                if (que_out == "1")
                {
                    objCommon.DisplayMessage(this.Page, "Industrial Visit Details Saved Successfully !!!", this.Page);
                }
                else if (que_out == "2")
                {
                    objCommon.DisplayMessage(this.Page, "Industrial Visit Details Updated Successfully !!!", this.Page);
                }

                IndustrialCancel();
                DataSet dsvisit = objStudRegC.GetIndustrialVisitDetails(Convert.ToInt32(ddlBranch.SelectedValue.Split('-')[0]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue));
                if (dsvisit.Tables[0].Rows.Count > 0)
                {
                    lvIndustrialVisit.DataSource = dsvisit;
                    lvIndustrialVisit.DataBind();
                    lvIndustrialVisit.Visible = true;
                }
                else
                {
                    lvIndustrialVisit.DataSource = null;
                    lvIndustrialVisit.DataBind();
                    lvIndustrialVisit.Visible = false;
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Student_details.btnProjectAdd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void IndustrialCancel()
    {
        txtNameIndustry.Text = string.Empty;
        txtAdresIndustry.Text = string.Empty;
        txtVisitDetails.Text = string.Empty;
        txtDateofVisit.Text = string.Empty;
        txtnoStudents.Text = string.Empty;
        txtnoFaculties.Text = string.Empty;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        lvStudentList.Visible = false;
        lvIndustrialVisit.Visible = false;
        btnSubmitvisit.Text = "Submit";
        ViewState["VISITNO"] = 0;
    }



    #region Upload Docs
    static bool validationflag = true;
    int status = 0;
    private void Check50KBFileSize(FileUpload filecheck)
    {
        try
        {
            if (filecheck.HasFile)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    //string folderPath = WebConfigurationManager.AppSettings["SVCE_STUDENT_CERTIFICATES"].ToString() + idno.ToString() + "-" + txtNameOfGameOrAchievement.Text + "\\";
                    //string folderPath = WebConfigurationManager.AppSettings["SVCE_INDUSTRIAL_CERTIFICATES"].ToString() + objStudReg.IDNO.ToString() + "\\";
                    string folderPath = WebConfigurationManager.AppSettings["SVCE_INDUSTRIAL_CERTIFICATES"].ToString() + objStudReg.IDNO.ToString() + "\\";

                    HttpPostedFile fu = Request.Files[i];
                    if (fu.ContentLength > 0)
                    {
                        string ext = System.IO.Path.GetExtension(fu.FileName);
                        string filename = Path.GetFileName(fu.FileName);
                        if (ext == ".pdf" || ext == ".png" || ext == ".jpeg" || ext == ".jpg" || ext == ".PNG" || ext == ".JPEG" || ext == ".JPG" || ext == ".PDF") //|| ext == ".doc" || ext == ".docx" 
                        {
                            //if (fu.ContentLength <= 51200)// 31457280 before size  //For Allowing 50 Kb Size Files only 
                            //{
                            //if (fu.ContentLength <= 102400)// 31457280 before size  //For Allowing 100 Kb Size Files only 
                            //{
                            if (fu.ContentLength <= 1000000)// 102400 before size  //For Allowing 1 Mb Size Files only 
                            {
                                validationflag = true;
                            }
                            else
                            {
                                objCommon.DisplayMessage("Document size must not exceed 1 MB !! Few File Size Large !", this.Page);
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
                objStudReg.Indus_file_name = objStudReg.Indus_file_name.TrimEnd(',');
                objStudReg.Indus_file_path = objStudReg.Indus_file_path.TrimEnd(',');
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

    private void uploadMultipleSportsCertificate(FileUpload fun, string Category)
    {
        try
        {
            if (fun.HasFile)
            {
                //  string Regno = lblRegNo.Text;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    //string folderPath = WebConfigurationManager.AppSettings["SVCE_STUDENT_CERTIFICATES"].ToString() + idno.ToString() + "-" + txtNameOfGameOrAchievement.Text + "\\";
                    //string folderPath = WebConfigurationManager.AppSettings["SVCE_INDUSTRIAL_CERTIFICATES"].ToString() + Regno.ToString() + "\\" + Convert.ToString(Category).ToString() + "\\";

                    string folderPath = WebConfigurationManager.AppSettings["SVCE_INDUSTRIAL_CERTIFICATES"].ToString() + Convert.ToString(Category).ToString() + "\\";

                    HttpPostedFile fu = Request.Files[i];
                    if (fu.ContentLength > 0)
                    {
                        string ext = System.IO.Path.GetExtension(fu.FileName);
                        string filename = Path.GetFileName(fu.FileName);
                        if (ext == ".pdf" || ext == ".png" || ext == ".jpeg" || ext == ".jpg" || ext == ".PNG" || ext == ".JPEG" || ext == ".JPG" || ext == ".PDF") //|| ext == ".doc" || ext == ".docx" 
                        {
                            //if (fu.ContentLength <= 102400)// 31457280 before size  //For Allowing 100 Kb Size Files only 
                            //{
                            if (fu.ContentLength <= 1000000)// 102400 before size  //For Allowing 1 MB Size Files only 
                            {
                                string contentType = fu.ContentType;
                                if (!Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }
                                objStudReg.Indus_file_name += filename + ",";
                                objStudReg.Indus_file_path += folderPath + filename + ",";
                                fu.SaveAs(folderPath + filename);
                                status = 1;
                            }
                            else
                            {
                                objCommon.DisplayMessage("Document size must not exceed 1 MB !", this.Page);
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
                objStudReg.Indus_file_name = objStudReg.Indus_file_name.TrimEnd(',');
                objStudReg.Indus_file_path = objStudReg.Indus_file_path.TrimEnd(',');
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


    #endregion

    #region  DataTable
    private void AddtoTable()
    {
        DataTable dt = new DataTable();
        Cleartable();
        int IDNO;
        try
        {
            foreach (ListViewItem gdb in lvStudentList.Items)
            {
                CheckBox chkselect = gdb.FindControl("chkReport") as CheckBox;
                Label lblname = gdb.FindControl("lblstudname") as Label;
                if (chkselect.Checked == true)
                {
                    IDNO = 0;
                    IDNO = Convert.ToInt32(chkselect.ToolTip);

                    if (ViewState["actionCo"] == null)
                    {
                        if (Session["Industrial_visit"] != null && (DataTable)Session["Industrial_visit"] != null)
                        {
                            int maxVal = 0;
                            DataRow dr = dt.NewRow();
                            if (dr != null)
                            {
                                maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["SRNO"]));
                            }
                            dr["SRNO"] = maxVal + 1;
                            dr["IDNO"] = IDNO;
                            dr["STUDNAME"] = lblname.Text;

                            dt.Rows.Add(dr);
                            Session["Industrial"] = dt;
                            ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                        }
                        else
                        {
                            dt = this.CreateTable();
                            DataRow dr = dt.NewRow();
                            dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                            dr["IDNO"] = IDNO;
                            dr["STUDNAME"] = lblname.Text;

                            ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                            dt.Rows.Add(dr);

                            Session["Industrial_visit"] = dt;
                        }
                    }
                    else
                    {
                        if (Session["Industrial_visit"] != null && ((DataTable)Session["Industrial_visit"]) != null)
                        {
                            dt = (DataTable)Session["Industrial_visit"];
                            DataRow dr = dt.NewRow();
                            dr["IDNO"] = IDNO;
                            dr["STUDNAME"] = lblname.Text;

                            dt.Rows.Add(dr);

                            Session["Industrial_visit"] = dt;

                            ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                        }
                    }
                }
            }
        }



        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, " --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    public DataTable CreateTable()
    {
        DataTable dtIndustrial_visit = new DataTable();
        dtIndustrial_visit.Columns.Add("SRNO", typeof(int));
        dtIndustrial_visit.Columns.Add("IDNO", typeof(int));
        dtIndustrial_visit.Columns.Add("STUDNAME", typeof(string));
        return dtIndustrial_visit;
    }

    private void Cleartable()
    {
        Session["Industrial_visit"] = null;
        ViewState["actionCo"] = null;
        ViewState["SRNO"] = null;
    }
    #endregion

    protected void btnSubmit_Link_Click(object sender, EventArgs e)
    {
        Industrial_link_Insert();
    }

    private void Industrial_link_Insert()
    {
        try
        {
            string Company_name = txtCompany.Text.Replace(',', '^');
            string Company_address = txtAddresCompany.Text.Replace(',', '^');
            string Mou_from = txtMouFrom.Text;
            string Mou_to = txtMouTo.Text;
            int Mou_type = Convert.ToInt32(ddlMoutype.SelectedValue);
            string Activities = txtActivities.Text.Replace(',', '^');
            string Remarks = txtRemarks.Text.Replace(',', '^');
            int Live_status = Convert.ToInt32(ddlLive.SelectedValue);
            int Ua_no = Convert.ToInt32(Session["userno"]);
            string Ip_address = ViewState["ipAddress"].ToString();
            int College_code = Convert.ToInt32(Session["colcode"].ToString());
            int Indlinkno = 0;
            string Stud_Par = txt_StuentParticipated.Text.Replace(',', '^');
            string Teacher_par = txt_TeacherParticipated.Text.Replace(',', '^');
            string Link = txt_Link.Text.Replace(',', '^');

            if (ViewState["action"].Equals("edit"))
            {
                Indlinkno = Convert.ToInt32(Session["indlinkno"].ToString());
            }

            objStudReg.Indus_file_name = "";
            objStudReg.Indus_file_path = "";

            if (FuIndustrialLink.HasFile)
            {
                Check50KBFileSize(FuIndustrialLink);

                if (validationflag == true)
                {
                    //to upload multiple sports docs details
                    uploadMultipleSportsCertificate(FuIndustrialLink, "IndustrialLink");
                    if (status == 1)
                    {
                        string SP_Name = "PKG_ACD_STUDENT_INDUSTRIAL_LINK_DETAILS";
                        string SP_Parameters = " @P_COMPANY_NAME ,@P_COMPANY_ADDRESS ,@P_MOU_FROM ,@P_MOU_TO ,@P_MOU_TYPE ,@P_ACTIVITIES ,@P_REMARKS ,@P_LIVE_STATUS ,@P_UA_NO ,@P_IP_ADDRESS ,@P_COLLEGE_CODE ,@P_INDUSTRIAL_LINK_FILE_NAME ,@P_INDUSTRIAL_LINK_FILE_PATH ,@P_INDLINKNO ,@P_STUD_PART ,@P_TEA_PART ,@P_LINK ,@P_OUT";

                        string Call_Values = "" + Company_name + ", " + Company_address + ", " + Convert.ToString(txtMouFrom.Text) + ", " + Convert.ToString(txtMouTo.Text) + ", " + Convert.ToInt32(Mou_type) + ", " + Activities + ", " + Remarks + ", " + Convert.ToInt32(Live_status) + ", " + Convert.ToInt32(Ua_no) + ", " + Ip_address + ", " + Convert.ToInt32(College_code) + ", " + objStudReg.Indus_file_name + ", " + objStudReg.Indus_file_path + ", " + Convert.ToInt32(Indlinkno) + "," + Convert.ToString(txt_StuentParticipated.Text) + "," + Convert.ToString(txt_TeacherParticipated.Text) + "," + Convert.ToString(txt_Link.Text) + "," + Convert.ToInt32(Indlinkno) + "";

                        string que_out = AL.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true, 2);

                        if (que_out == "1")
                        {
                            objCommon.DisplayMessage(this.Page, "MoU Details Saved Successfully !!!", this.Page);
                        }
                        else if (que_out == "2")
                        {
                            objCommon.DisplayMessage(this.Page, "MoU Details Updated Successfully !!!", this.Page);
                        }

                        IndustrialLinkCancel();
                        LvIndustrialLink.Visible = true;
                        GetIndustrialLinkDetails();
                    }
                }
            }
            else
            {
                string SP_Name = "PKG_ACD_STUDENT_INDUSTRIAL_LINK_DETAILS";
                string SP_Parameters = " @P_COMPANY_NAME ,@P_COMPANY_ADDRESS ,@P_MOU_FROM ,@P_MOU_TO ,@P_MOU_TYPE ,@P_ACTIVITIES ,@P_REMARKS ,@P_LIVE_STATUS ,@P_UA_NO ,@P_IP_ADDRESS ,@P_COLLEGE_CODE ,@P_INDUSTRIAL_LINK_FILE_NAME ,@P_INDUSTRIAL_LINK_FILE_PATH ,@P_INDLINKNO ,@P_STUD_PART ,@P_TEA_PART ,@P_LINK ,@P_OUT";

                string Call_Values = "" + Company_name + ", " + Company_address + ", " + Convert.ToString(txtMouFrom.Text) + ", " + Convert.ToString(txtMouTo.Text) + ", " + Convert.ToInt32(Mou_type) + ", " + Activities + ", " + Remarks + ", " + Convert.ToInt32(Live_status) + ", " + Convert.ToInt32(Ua_no) + ", " + Ip_address + ", " + Convert.ToInt32(College_code) + ", " + objStudReg.Indus_file_name + ", " + objStudReg.Indus_file_path + ", " + Convert.ToInt32(Indlinkno) + "," + Convert.ToString(txt_StuentParticipated.Text) + "," + Convert.ToString(txt_TeacherParticipated.Text) + "," + Convert.ToString(txt_Link.Text) + "," + Convert.ToInt32(Indlinkno) + "";

                string que_out = AL.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true, 2);

                if (que_out == "1")
                {
                    objCommon.DisplayMessage(this.Page, "MoU Details Saved Successfully !!!", this.Page);
                }
                else if (que_out == "2")
                {
                    objCommon.DisplayMessage(this.Page, "MoU Details Updated Successfully !!!", this.Page);
                }

                IndustrialLinkCancel();
                LvIndustrialLink.Visible = true;
                GetIndustrialLinkDetails();
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void IndustrialLinkCancel()
    {
        txtCompany.Text = string.Empty;
        txtAddresCompany.Text = string.Empty;
        txtMouFrom.Text = string.Empty;
        txtMouTo.Text = string.Empty;
        ddlMoutype.SelectedIndex = 0;
        txtActivities.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        ddlLive.SelectedIndex = 0;
        txt_StuentParticipated.Text = string.Empty;
        txt_TeacherParticipated.Text = string.Empty;
        txt_Link.Text = string.Empty;
        LvIndustrialLink.Visible = false;


    }

    protected void btnCancel_Link_Click(object sender, EventArgs e)
    {
        IndustrialLinkCancel();
    }
    protected void btnCancelvisit_Click(object sender, EventArgs e)
    {
        // Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
        IndustrialCancel();
    }

    protected void lvIndustrialVisit_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Label lblIndustryname = (Label)e.Item.FindControl("lblIndustryname");
            ListView lvIndustrialdocs = (ListView)e.Item.FindControl("lvIndustrialdocs");
            LinkButton btn = e.Item.FindControl("btnDownloadFile") as LinkButton;

            DataSet dsVisitCERT = objStudRegC.GetStudentIndustrialVisitDocs(Convert.ToInt32(lblIndustryname.ToolTip));
            if (dsVisitCERT.Tables[0].Rows.Count > 0 && dsVisitCERT != null)
            {
                lvIndustrialdocs.DataSource = dsVisitCERT.Tables[0];
                lvIndustrialdocs.DataBind();
            }
            else
            {
                lvIndustrialdocs.DataSource = null;
                lvIndustrialdocs.DataBind();
            }
        }
    }

    private void download(object sender, string category)
    {
        LinkButton btndownloadfile = sender as LinkButton;
        string Industryname = (btndownloadfile.CommandArgument);
        //int idno = Convert.ToInt32(lblRegNo.ToolTip);
        //int Visitno = Convert.ToInt32(lblIndustryname.ToolTip);
        //   string category = "Project";
        //if (idno != 0)
        //{
        string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).ToolTip.ToString();
        string ContentType = string.Empty;

        //To Get the physical Path of the file(test.txt)

        // string filepath = WebConfigurationManager.AppSettings["SVCE_STUDENT_CERTIFICATES"].ToString() + lblRegNo.Text.ToString() + "\\" + category.ToString() + "\\";
        string filepath = WebConfigurationManager.AppSettings["SVCE_INDUSTRIAL_CERTIFICATES"].ToString() + Convert.ToString(category).ToString() + "\\";

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
        //}
        else
        {
            objCommon.DisplayMessage("Please Refresh the Page again!!", this.Page);
            return;
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        Delete(sender, "IndustrialVisit", Convert.ToInt32(btn.CommandArgument));
        DataSet dsvisit = objStudRegC.GetIndustrialVisitDetails(Convert.ToInt32(ddlBranch.SelectedValue.Split('-')[0]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue));
        if (dsvisit.Tables[0].Rows.Count > 0)
        {
            lvIndustrialVisit.DataSource = dsvisit;
            lvIndustrialVisit.DataBind();
            lvIndustrialVisit.Visible = true;
        }
        else
        {
            lvIndustrialVisit.DataSource = null;
            lvIndustrialVisit.DataBind();
            lvIndustrialVisit.Visible = false;
        }

    }
    protected void btnDownloadFile_Click(object sender, EventArgs e)
    {
        download(sender, "IndustrialVisit");
    }

    private void Delete(object sender, string category, int DocNo)
    {
        //  int idno = Convert.ToInt32(lblRegNo.ToolTip);
        int ret = 0;
        try
        {
            //if (idno != 0)
            //{

            ImageButton btnDelete = sender as ImageButton;
            string filename = btnDelete.AlternateText;

            string path = WebConfigurationManager.AppSettings["SVCE_INDUSTRIAL_CERTIFICATES"].ToString() + Convert.ToString(category).ToString() + "\\";
            string DOCS_NO = string.Empty;

            if (File.Exists(path + filename))
            {
                File.Delete(path + filename);

                if (category == "IndustrialVisit")
                {
                    ret = objStudRegC.DeleteIndustrialVisitDocs(DocNo);
                }
                else if (category == "IndustrialLink")
                {
                    ret = objStudRegC.DeleteIndustrialLinkDocs(DocNo);
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

        //}
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

    protected void LvIndustrialLink_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Label lblCompany = (Label)e.Item.FindControl("lblCompany");
            ListView LvIndustrialCert = (ListView)e.Item.FindControl("LvIndustrialCert");

            //  DataSet dsLinkCERT = GetIndustrialLinkDetails();
            //DataSet dsLinkCERT = objCommon.FillDropDown("ACD_STUD_INDUSTRIAL_LINK_DETAILS ILD INNER JOIN ACD_STUD_INDUSTRIAL_LINK_DOCS ILDS ON(ILD.INDLINKNO=ILDS.INDLINKNO)", "DISTINCT ILD.INDLINKNO", "ILDS.*,ILD.COMPANY_NAME,COMPANY_ADDRESS,CONVERT(VARCHAR(20),MOU_FROM,(103))MOU_FROM,	CONVERT (VARCHAR(20),MOU_TO,(103))MOU_TO,(CASE WHEN MOU_TYPE = 1 THEN 'Visit' ELSE 'Project' END)MOU_TYPE,ACTIVITIES,REMARKS,(CASE WHEN LIVE_STATUS = 1 THEN 'YES' ELSE 'NO' END)LIVE_STATUS", "ILD.INDLINKNO="+lblCompany.ToolTip, "ILD.INDLINKNO");

            DataSet dsLinkCERT = objCommon.FillDropDown("ACD_STUD_INDUSTRIAL_LINK_DETAILS ILD INNER JOIN ACD_STUD_INDUSTRIAL_LINK_DOCS ILDS ON(ILD.INDLINKNO=ILDS.INDLINKNO)", "DISTINCT ILD.INDLINKNO", "ILDS.*,ILD.COMPANY_NAME,COMPANY_ADDRESS,CONVERT(VARCHAR(20),MOU_FROM,(103))MOU_FROM,	CONVERT (VARCHAR(20),MOU_TO,(103))MOU_TO,(CASE WHEN MOU_TYPE = 1 THEN 'Industrial' ELSE 'Institute' END)MOUTYPE,MOU_TYPE,ACTIVITIES,REMARKS,(CASE WHEN LIVE_STATUS = 1 THEN 'YES' ELSE 'NO' END)LIVESTATUS,LIVE_STATUS", "ILD.INDLINKNO=" + lblCompany.ToolTip, "ILD.INDLINKNO");

            if (dsLinkCERT.Tables[0].Rows.Count > 0 && dsLinkCERT != null)
            {
                LvIndustrialCert.DataSource = dsLinkCERT.Tables[0];
                LvIndustrialCert.DataBind();
            }
            else
            {
                // objCommon.DisplayMessage("Sports Certificate Not Found!!", this.Page);
                LvIndustrialCert.DataSource = null;
                LvIndustrialCert.DataBind();
            }
        }
    }

    protected void btnDownloadIndustrialLink_Click(object sender, EventArgs e)
    {
        download(sender, "IndustrialLink");
    }
    protected void btnIndustrialLnkDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        Delete(sender, "IndustrialLink", Convert.ToInt32(btn.CommandArgument));
        DataSet dsLink = objCommon.FillDropDown("ACD_STUD_INDUSTRIAL_LINK_DETAILS ILD INNER JOIN ACD_STUD_INDUSTRIAL_LINK_DOCS ILDS ON(ILD.INDLINKNO=ILDS.INDLINKNO)", "DISTINCT ILD.INDLINKNO", "ILD.COMPANY_NAME,COMPANY_ADDRESS,CONVERT(VARCHAR(20),MOU_FROM,(103))MOU_FROM,	CONVERT (VARCHAR(20),MOU_TO,(103))MOU_TO,(CASE WHEN MOU_TYPE = 1 THEN 'Industrial' ELSE 'Institute' END)MOUTYPE,MOU_TYPE,ACTIVITIES,REMARKS,(CASE WHEN LIVE_STATUS = 1 THEN 'YES' ELSE 'NO' END)LIVESTATUS,LIVE_STATUS", "", "ILD.INDLINKNO");
        if (dsLink.Tables[0].Rows.Count > 0)
        {
            LvIndustrialLink.DataSource = dsLink;
            LvIndustrialLink.DataBind();
            LvIndustrialLink.Visible = true;
        }
        else
        {
            LvIndustrialLink.DataSource = null;
            LvIndustrialLink.DataBind();
            LvIndustrialLink.Visible = false;
        }
    }

    protected void btnLinkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            DataSet dt = (DataSet)ViewState["Table"];
            DataRow[] foundrow = dt.Tables[0].Select("INDLINKNO=" + btnEdit.CommandArgument);
            if (foundrow.Length > 0)
            {
                DataTable newTable = foundrow.CopyToDataTable();
                if (newTable.Rows.Count > 0)
                {
                    txtCompany.Text = newTable.Rows[0]["COMPANY_NAME"].ToString();
                    txtAddresCompany.Text = newTable.Rows[0]["COMPANY_ADDRESS"].ToString();
                    txtMouFrom.Text = newTable.Rows[0]["MOU_FROM"].ToString();
                    txtMouTo.Text = newTable.Rows[0]["MOU_TO"].ToString();
                    ddlMoutype.SelectedValue = newTable.Rows[0]["MOU_TYPE"].ToString();
                    txtActivities.Text = newTable.Rows[0]["ACTIVITIES"].ToString();
                    txtRemarks.Text = newTable.Rows[0]["REMARKS"].ToString();
                    ddlLive.SelectedValue = newTable.Rows[0]["LIVE_STATUS"].ToString();
                    txt_StuentParticipated.Text = newTable.Rows[0]["STUD_PART"].ToString();
                    txt_TeacherParticipated.Text = newTable.Rows[0]["TEA_PART"].ToString();
                    txt_Link.Text = newTable.Rows[0]["LINK"].ToString();

                    ViewState["action"] = "edit";
                    Session["indlinkno"] = btnEdit.CommandArgument;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_studentIndustrialDetails.btnLinkEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEditforVisit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        ViewState["VISITNO"] = btn.CommandArgument.Split('$')[0];
        txtNameIndustry.Text = btn.CommandArgument.Split('$')[1];
        txtAdresIndustry.Text = btn.CommandArgument.Split('$')[2];
        txtVisitDetails.Text = btn.CommandArgument.Split('$')[3];
        txtDateofVisit.Text = btn.CommandArgument.Split('$')[4];
        txtnoStudents.Text = btn.CommandArgument.Split('$')[5];
        txtnoFaculties.Text = btn.CommandArgument.Split('$')[6];

        // For Retriving Branch in the Dropdown when Click on Edit Added by Naresh Beerla on 24-08-2020
        if (btn.CommandArgument.Split('$')[7] != "" && btn.CommandArgument.Split('$')[11] != "" && btn.CommandArgument.Split('$')[7] != "0")
        {
            DataSet dsBranchno = objCommon.FillDropDown("ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH ACD ON (B.BRANCHNO=ACD.BRANCHNO) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=ACD.DEGREENO)", "CONCAT(B.BRANCHNO,'-',D.DEGREENO) AS BRANCHNO", "CONCAT (D.DEGREENAME,'-',B.LONGNAME)BRANCH", "B.BRANCHNO=" + btn.CommandArgument.Split('$')[7] + "and D.DEGREENO=" + btn.CommandArgument.Split('$')[11], "D.DEGREENO,B.BRANCHNO");
            if (dsBranchno.Tables[0].Rows.Count > 0)
            {
                ddlBranch.SelectedValue = dsBranchno.Tables[0].Rows[0]["BRANCHNO"] == "0" ? "0" : dsBranchno.Tables[0].Rows[0]["BRANCHNO"].ToString();
            }
            else
            {
                ddlBranch.SelectedValue = "0";
            }
        }
        else
        {
            ddlBranch.SelectedValue = "0";
        }
        ddlSemester.SelectedValue = btn.CommandArgument.Split('$')[8];
        ddlSection.SelectedValue = btn.CommandArgument.Split('$')[9];
        ddlAdmBatch.SelectedValue = ddlAdmBatch.Items.FindByText(btn.CommandArgument.Split('$')[10]).Value;

        btnSubmitvisit.Text = "Update";
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH ACD ON (B.BRANCHNO=ACD.BRANCHNO) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=ACD.DEGREENO)", "CONCAT(B.BRANCHNO,'-',D.DEGREENO) AS BRANCHNO", "CONCAT (D.DEGREENAME,'-',B.LONGNAME)BRANCH", "COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENO,B.BRANCHNO");
            ddlBranch.SelectedIndex = 0;
        }
    }

}