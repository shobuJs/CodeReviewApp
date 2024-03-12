using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Data.SqlClient;

using System.Data;
using System.Data.OleDb;
using System.Data.Common;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using DynamicAL_v2;

public partial class ACADEMIC_StudentIDCardReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    DynamicControllerAL AL = new DynamicControllerAL();
    StudentController studCont = new StudentController();

    #region PageLoad
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
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }

                    PopulateDropDownList();
                    ShowDetails();
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(this.btnUpload);
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(this.btnShow);
                }
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header

            }

            divMsg.InnerHtml = string.Empty;            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.Page_Load-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=StudentIDCardReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentIDCardReport.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {           
            //int mul_col_flag = Convert.ToInt32(objCommon.LookUp("REFF", "MUL_COL_FLAG", "CollegeName='" + Session["coll_name"].ToString() + "'"));
            //if (mul_col_flag == 0)
            //{
                objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER CM INNER JOIN USER_ACC UA ON (CM.COLLEGE_ID IN (SELECT CAST(VALUE AS INT) FROM DBO.SPLIT(UA.UA_COLLEGE_NOS,',')))INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON(CM.COLLEGE_ID=CDB.COLLEGE_ID)", "DISTINCT CM.COLLEGE_ID", "COLLEGE_NAME", "UA_NO =" + Session["userno"], "CM.COLLEGE_ID");
                //ddlCollege.SelectedIndex = 1;
            //}
            //else
            //{
            //   objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            //   ddlCollege.SelectedIndex = 0;
            //}
            ddlCollege.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region dropdown
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            ddlBranch.Items.Clear();
            objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + ddlDegree.SelectedValue + "AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "CD.BRANCHNO");
            ddlBranch.Focus();

            ddlSemester.Items.Clear();
            ViewState["YearWise"] = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "ISNULL(YEARWISE,0)YEARWISE", "DEGREENO=" + ddlDegree.SelectedValue));

            objCommon.FillDropDownList(ddlSemester, "ACD_YEAR A INNER JOIN ACD_STUDENT B ON (A.YEAR=B.YEAR)", "DISTINCT A.YEAR", "YEARNAME", "SEM<>0 AND ADMBATCH=" + ddlAdmbatch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue, "YEAR");

        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlAdmbatch.SelectedIndex = 0;
            ddlAdmbatch.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.SelectedIndex = 0;
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));

        }
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();

        ddlDegree.Focus(); //Added by Abhinay Lad [26-06-2019]
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlSemester.Items.Clear();
            objCommon.FillDropDownList(ddlSemester, "ACD_YEAR A INNER JOIN ACD_STUDENT B ON (A.YEAR=B.YEAR)", "DISTINCT A.YEAR", "YEARNAME", "SEM<>0 AND ADMBATCH=" + ddlAdmbatch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue, "YEAR");
            //if (ViewState["YearWise"].ToString() == "1")
            //{
            //    objCommon.FillDropDownList(ddlSemester, "ACD_YEAR A INNER JOIN ACD_STUDENT B ON (A.YEAR=B.YEAR)", "DISTINCT A.SEM", "YEARNAME", "SEM<>0 AND ADMBATCH=" + ddlAdmbatch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue, "SEM");
            //}
            //else
            //{
            //    objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER A INNER JOIN ACD_STUDENT B ON (A.SEMESTERNO=B.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO>0 AND ADMBATCH=" + ddlAdmbatch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue, "A.SEMESTERNO");
            //}
            ddlSemester.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));

            //ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();

        ddlBranch.Focus();
    }
    #endregion

    #region Click Event
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            this.BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindListView()
    {
        try
        {
            
            DataSet ds;
            DataView dv;
            ds = studCont.GetStudentListForIdentityCard(Convert.ToInt32(ddlCollege.SelectedValue),Convert.ToInt32(ddlAdmbatch.SelectedValue),Convert.ToInt32(ddlDegree.SelectedValue),Convert.ToInt32(ddlBranch.SelectedValue),Convert.ToInt32(ddlSemester.SelectedValue),Convert.ToInt32(ddlIdCardType.SelectedValue)); // Year wise Filtering in place of Semester on 25022021 
            if (ds != null && ds.Tables.Count > 0)
            {
                dv = new DataView(ds.Tables[0], "ADMBATCH=" + ddlAdmbatch.SelectedValue, "REGNO", DataViewRowState.OriginalRows);

                lvStudentRecords.DataSource = dv;
                lvStudentRecords.DataBind();
                hftot.Value = dv.Count.ToString();
            }
            else
            {
                lvStudentRecords.DataSource = null;
                lvStudentRecords.DataBind();
                objCommon.DisplayMessage(updStudent, "Record Not Found!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.BindListView --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            Session["listIdCard"] = null;
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception)
        {
            throw;
        }
    }
    
    private string GetStudentIDs()
    {
        string studentIds = string.Empty;
        try
        {
            foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
                if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                    if (studentIds.Length > 0)
                        studentIds += "$";
                    studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.GetStudentIDs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return studentIds;
    }

    private void ShowDetails()
    {
        try
        {
            SqlDataReader dr = objCommon.GetCommonDetails();

            if (dr != null)
            {
                if (dr.Read())
                {
                    imgCollegeLogo.ImageUrl = "~/showimage.aspx?id=" + dr["Errors"].ToString() + "&type=registrar";
                }
            }
            if (dr != null) dr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "reference.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnFrontBackReport_Click(object sender, EventArgs e)
    {
        #region Backup for BLOB Photo
        //string ids = GetStudentIDs();

        //string SP_Name = "PKG_STU_REPORT_IDENTITYCARD";
        //string SP_Parameters = "@P_IDNO, @P_COLLEGE_ID, @P_COLLEGE_CODE, @P_VALID_UPTO";
        //string Call_Values = "" + ids + "," + Convert.ToInt32(ddlCollege.SelectedValue) + "," + Convert.ToInt32(Session["colcode"].ToString()) + "," + Convert.ToString(txtValidUpto.Text) + "";
        //DataSet ds = AL.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

        //string reportPath = @"~\Reports\Academic\";
        //string reportName = "StudentIDCardFrontBack_MNRC.rpt";
        //Session["Blob_ReportName"] = reportPath + reportName;
        //Session["Blob_Dataset"] = ds.Tables[0];
        //Session["Blob_Column"] = "12";
        //Session["Blob_College_Id"] = 0;
        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/PresentationLayer/Reports/BlobReportViewer.aspx?forReport=" + reportName + "');", true);
        #endregion
        #region Backup Commented
        try
        {
            string ids = GetStudentIDs();
            if (!string.IsNullOrEmpty(ids))
            {
                if (Convert.ToInt32(ddlIdCardType.SelectedValue) == 1) //For Regular
                {
                    if (Convert.ToInt32(ddlCollege.SelectedValue) == 8)
                    {
                        ShowReport(ids, "Student_ID_Card_Report", "CopyofStudentIDCardFrontBackInternational.rpt");
                    }
                    else
                    {
                        ShowReport(ids, "Student_ID_Card_Report", "CopyofStudentIDCardFrontBack.rpt");
                    }


                }


                //else if (Convert.ToInt32(ddlIdCardType.SelectedValue) == 2) //For Hosteller
                //{
                //    ShowReport(ids, "Student_ID_Card_Report", "Hostel_IdCard.rpt");
                //}
                //else if (Convert.ToInt32(ddlIdCardType.SelectedValue) == 3) // For Transporter
                //{
                //    ShowReport(ids, "Student_ID_Card_Report", "StudentIDCardFrontBackTransport.rpt");
                //}
            }
            else
                objCommon.DisplayMessage(this.updStudent, "Please Select Students!", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.btnPrintReport_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
        #endregion
    }

    private void ShowReport(string param, string reportTitle, string rptFileName)
    {
        try
        {
         

            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];

            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_COLLEGE_ID="+ddlCollege.SelectedValue+",@P_Valid_Upto="+txtValidUpto.Text;
          
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
           
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updStudent, this.updStudent.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentIDCardReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    // Upload the Registrar Sign to below code..
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            ReferenceController objEc = new ReferenceController();
            Reference objRef = new Reference();

            if (fuRegistrarSign.HasFile)
            {
                objRef.CollegeLogo = objCommon.GetImageData(fuRegistrarSign);
            }
            else
            {
                System.IO.FileStream ff = new System.IO.FileStream(System.Web.HttpContext.Current.Server.MapPath("~/images/logo.gif"), System.IO.FileMode.Open);
                int ImageSize = (int)ff.Length;
                byte[] ImageContent = new byte[ff.Length];
                ff.Read(ImageContent, 0, ImageSize);
                ff.Close();
                ff.Dispose();
                objRef.CollegeLogo = ImageContent;
            }

            CustomStatus cs = (CustomStatus)objEc.UpdateRegistrarSign(objRef);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.updStudent, "Registrar Sign Updated Successfully!!", this.Page);
            }
            else
                objCommon.DisplayMessage(this.updStudent, "Error!!", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "reference.btnUpload_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region commented
    //protected void btnPrintReport_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string ids = GetStudentIDs();
    //        if (!string.IsNullOrEmpty(ids))
    //        {
    //            ShowReportDefault(ids, "Student_ID_Card_Report", "StudentIDCardFront.rpt");
    //        }
    //        else
    //            objCommon.DisplayMessage(this.updStudent, "Please Select Students!", this.Page);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_StudentIDCardReport.btnPrintReport_Click --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable");
    //    }
    //}

    //protected void btnbackReport_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string ids = GetStudentIDs();
    //        if (!string.IsNullOrEmpty(ids))
    //        {
    //            ShowReportDefault(ids, "Student_ID_Card_Report", "StudentIDCardBack.rpt");
    //        }
    //        else
    //            objCommon.DisplayMessage(this.updStudent, "Please Select Students!", this.Page);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_StudentIDCardReport.btnPrintReport_Click --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable");
    //    }
    //}

    //private void ShowReportDefault(string param, string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param;
    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";
    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");
    //        ScriptManager.RegisterClientScriptBlock(this.updStudent, this.updStudent.GetType(), "controlJSScript", sb.ToString(), true);

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "StudentIDCardReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    #endregion

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIdCardType.Focus();
    }

    protected void ddlIdCardType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
   
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNAME DESC");
        }
        else
        {
            objCommon.DisplayMessage("Please Select College",this.Page);
            ddlCollege.SelectedIndex = 0;
            ddlCollege.Focus();
        }
        
    }
    protected void ddlAdmbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdmbatch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.COLLEGE_ID="+ddlCollege.SelectedValue, "DEGREENAME");
        }
        else
        {
            objCommon.DisplayMessage("Please Select Admission Batch", this.Page);
            ddlCollege.SelectedIndex = 0;
            ddlAdmbatch.SelectedIndex = 0;
            ddlAdmbatch.Focus();
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        int idno = 0;
        string Rf_Id = "", IdnoCheck = "";

        foreach (ListViewDataItem dataitem in lvStudentRecords.Items)
        {

            CheckBox chkapp = dataitem.FindControl("chkReport") as CheckBox;
            HiddenField hidIdNo = dataitem.FindControl("hidIdNo") as HiddenField;
            TextBox txtRfId = dataitem.FindControl("txtRfId") as TextBox;

            if (chkapp.Checked == true)
            {

                IdnoCheck += hidIdNo.Value;
                idno = Convert.ToInt32(hidIdNo.Value);

                Rf_Id = txtRfId.Text;
                CustomStatus cs = (CustomStatus)studCont.InsertStudentChangeData(idno,Rf_Id);

                if (cs == CustomStatus.RecordSaved)
                {
                    objCommon.DisplayMessage(this, "Record Saved Successfully !!", this.Page);
                    BindListView();

                }
                else if (cs == CustomStatus.RecordExist)
                {
                    objCommon.DisplayMessage(this, "Record Alredy Exists", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this, "Server Error", this.Page);
                }
            }
        }
        if (IdnoCheck.ToString() == "")
        {
            objCommon.DisplayMessage(this, "Please Select At List One", this.Page);
            return;
        }
    }
}

