using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;

using System.Text;

using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

using System.Net;
using System.Collections.Generic;
using System.Net.Mail;
using DynamicAL_v2;
using _NVP;
using System.Collections.Specialized;

using EASendMail;

using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;

public partial class ACADEMIC_LeadAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    FaqController objFAQ = new FaqController();     
    FetchDataController objfech = new FetchDataController();
    StudentController objUCS = new StudentController();
    Student_Acd objus = new Student_Acd();
    NewUser objnu = new NewUser();
    NewUserController objnuc = new NewUserController();
    User objU = new User();
    UserController objUC = new UserController();
    OnlineAdmBranchController objBC = new OnlineAdmBranchController();
    DynamicControllerAL AL = new DynamicControllerAL();
    OnlineAdmStudentFees objStudentFees = new OnlineAdmStudentFees();
    OnlineAdmStudentController ObjNucFees = new OnlineAdmStudentController();
    int ResendOTP = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                this.CheckPageAuthorization();
                FillDropDown();
                ViewState["action"] = "Add";

                if (Request.QueryString["pageno"] != null)
                {

                }
                this.FillDropdownList();       
            }
            ViewState["cntbranch"] = string.Empty;

            ViewState["action"] = "add";

            objCommon.SetLabelData("0");
        }
        //BindListView();
        //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // dynamic page header addded by sarang 09/07/2021
        
        txtEmailMessage.Attributes.Add("maxlenght", txtEmailMessage.MaxLength.ToString());
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=LeadAllotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=LeadAllotment.aspx");
        }
    }

    private void BindListView()
    {
        ViewState["DYNAMIC_DATASET"] = null;
        DataSet ds = null;
        string[] date;
        if (Convert.ToString(hdnDate.Value) == string.Empty || Convert.ToString(hdnDate.Value) == "0")
        {
            date = ",".Split(',');

        }
        else
        {
            date = hdnDate.Value.ToString().Split('-');
            date[0] = Convert.ToDateTime(date[0]).ToString("dd/MM/yyyy");
            date[1] = Convert.ToDateTime(date[1]).ToString("dd/MM/yyyy");
        }
        if (Convert.ToString(Session["usertype"]) == "1")
        {
            ds = objSC.getstudentid(Convert.ToString(ddlProgressLavel.SelectedItem.Text), "0", Convert.ToInt32(0), 5, Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlugpg.SelectedValue), Convert.ToInt32(ddlDiscipline.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlAptitudeTestCentre.SelectedValue), Convert.ToInt32(ddlAptitudeTestMedium.SelectedValue), date[0], date[1]);
            btnemail.Visible = true;
            divstudent.Visible = true;
            btnSubmit1.Visible = true;
            btnClear1.Visible  =  true;
        }
        else
        {
            ds = objSC.getstudentid(Convert.ToString(ddlProgressLavel.SelectedItem.Text), "0", Convert.ToInt32(Session["userno"]), 5, Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlugpg.SelectedValue), Convert.ToInt32(ddlDiscipline.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlAptitudeTestCentre.SelectedValue), Convert.ToInt32(ddlAptitudeTestMedium.SelectedValue), date[0], date[1]);
            divstudent.Visible = true;
            btnSubmit1.Visible = true;
            btnClear1.Visible = true;
            btnemail.Visible = true;
        }
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            divAllDemands.Visible = true;
            lvLeadDetails.DataSource = ds;
            lvLeadDetails.DataBind();
           // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            DivMAinPanel.Visible = true;
            lblTotalCount.Text = ds.Tables[0].Rows.Count.ToString();
            lblTotalCount.Visible = true;
            ViewState["DYNAMIC_DATASET"] = ds.Tables[0];
        }
        else
        {
            lvLeadDetails.DataSource = null;
            lvLeadDetails.DataBind();
            objCommon.DisplayMessage(this.Page,"No Record Found !!!",this.Page);
            btnemail.Visible = false;
            divstudent.Visible = false;
            btnSubmit1.Visible = false;
            btnClear1.Visible = false;
            lblTotalCount.Text = "0";
        }
        if (hdnDate.Value != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
        }
    }
    protected void btnClear1_Click(object sender, EventArgs e)
    {
        Clear();
    }
    private void FillDropdownList()
    {
        if (Convert.ToString(Session["usertype"]) == "1")
        {
            objCommon.FillDropDownList(ddlUser, "USER_ACC", "DISTINCT UA_NO", "UA_FULLNAME", "isnull(UA_STATUS,0)=0 AND UA_NO>0 AND UA_TYPE <> 2", "UA_NO ASC");
        }
        else
        {
            objCommon.FillDropDownList(ddlUser, "ACD_LEAD_GROUP LG INNER JOIN USER_ACC UA ON UA.UA_NO IN (SELECT VALUE FROM DBO.SPLIT(SUBUSER_UA_NO,','))", "DISTINCT UA.UA_NO", "UA_FULLNAME", "isnull(UA.UA_STATUS,0)=0 AND UA_NO > 0 AND UA_TYPE <> 2 AND MAINUSER_UA_NO=" + Convert.ToInt32(Session["userno"]), "UA_NO ASC");
        }
        objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
        objCommon.FillDropDownList(ddlSource, "ACD_SOURCETYPE", "SOURCETYPENO", "SOURCETYPENAME", "SOURCETYPENO>0", "SOURCETYPENAME");
        objCommon.FillDropDownList(ddlugpg, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "UA_SECTION");
        objCommon.FillDropDownList(ddlRemarkAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");

        objCommon.FillDropDownList(ddlFormCategory, "ACD_QUERY_CATEGORY", "DISTINCT CATEGORYNO", "QUERY_CATEGORY_NAME", "CATEGORYNO>0", "CATEGORYNO");
       // objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMBATCH AB INNER JOIN ACD_MONTH A ON(AB.STARTMONTHNO=A.MONTHNO)INNER JOIN ACD_MONTH AA ON(AB.ENDMONTHNO=AA.MONTHNO)", "BATCHNO", "(BATCHNAME + '    ' + (A.MONTH) + ' - ' + (AA.MONTH)) AS BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
        objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");

        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A ", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 ", "A.DEGREENO");

        objCommon.FillDropDownList(ddlCampus, "ACD_CAMPUS", "DISTINCT CAMPUSNO", "CAMPUSNAME", "CAMPUSNO > 0 ", "CAMPUSNO");
        objCommon.FillDropDownList(ddlDiscipline, "ACD_AREA_OF_INTEREST", "DISTINCT AREA_INT_NO", "AREA_INT_NAME", "AREA_INT_NO > 0 ", "AREA_INT_NO");
        objCommon.FillDropDownList(ddlAptitudeTestCentre, "ACD_CAMPUS", "DISTINCT CAMPUSNO", "CAMPUSNAME", "CAMPUSNO > 0 ", "CAMPUSNO");
        objCommon.FillDropDownList(ddlAptitudeTestMedium, "ACD_MEDIUM", "DISTINCT SRNO", "MEDIUM_NAME", "SRNO > 0 ", "SRNO");

        if (Session["ADD_ALERT"] == "1")
        {
            objCommon.DisplayMessage(UpdatePanel1, "Record Added successfully!", this.Page);
            Session["ADD_ALERT"] = null;
        }
        if (Session["ADD_ALERT"] == "2")
        {
            objCommon.DisplayMessage(UpdatePanel1, "Record Added & Updated successfully!", this.Page);
            Session["ADD_ALERT"] = null;
        }
        else if (Session["ADD_ALERT"] == "3")
        {
            objCommon.DisplayMessage(UpdatePanel1, "Error!", this.Page);
            Session["ADD_ALERT"] = null;
        }
    }

 
   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
       
    }
    
    public void BindRadioListStatus()
    {
          string constr = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
          using (SqlConnection con = new SqlConnection(constr))
          {
              string query = "SELECT LEAD_STAGE_NAME, LEADNO FROM ACD_LEAD_STAGE";
              using (SqlCommand cmd = new SqlCommand(query))
              {
                  cmd.CommandType = CommandType.Text;
                  cmd.Connection = con;
                  con.Open();
                  //rblLeadStatus.DataSource = cmd.ExecuteReader();
                  //rblLeadStatus.DataTextField = "LEAD_STAGE_NAME";
                  //rblLeadStatus.DataValueField = "LEADNO";
                  //rblLeadStatus.DataBind();
                  con.Close();
              }
          }
    }

    protected void rblLeadStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = null;
       
    }

    protected void rblSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();

    }
   
    protected void Clear()
    {
        Response.Redirect(Request.Url.ToString());
    }

   
    //added by swapnil thakare on dated 06-09-2021
    protected void lnkApplicationNo_Click(Object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkApplicationNo = sender as LinkButton;
            string UserName = (lnkApplicationNo.ToolTip).ToString();
            int UserNo = int.Parse(lnkApplicationNo.CommandArgument);
            ViewState["STUDENT_USERNO"] = Convert.ToString(lnkApplicationNo.CommandArgument);
            string LeadApplNo = (lnkApplicationNo.Text).ToString();
            ViewState["STUDENT_USERNAME"] = LeadApplNo;
            //if (UserName == "" || UserName == string.Empty)
            if (LeadApplNo == "" || LeadApplNo == string.Empty)
            {
                    objCommon.DisplayMessage(this.UpdatePanel1, "Student not alloted", this.Page);
                    return;
            }
            else
            {
                //string url = string.Empty;
                //string host = Request.Url.Host;
                //string scheme = Request.Url.Scheme;
                //int portno = Request.Url.Port;

                //if (host == "localhost")
                //{
                //    url = scheme + "://" + host + ":" + portno + "/PresentationLayer/ACADEMIC/leadallotmentnew.aspx";
                //    Session["urlLeadUSerno"] = UserNo;
                //}
                //else
                //{
                //    url = scheme + "://" + host + "/ACADEMIC/leadallotmentnew.aspx";
                //    Session["urlLeadUSerno"] = UserNo;
                //}


                //string URL = "window.open('" + url + "', '_blank');";
                //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", URL, true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#myApplicationModal').modal()", true);
                BindListViewApplication();
                BindData();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#myApplicationModal').modal()", true);
                DataSet dsProgress = null;
                dsProgress = objUCS.GetStudentTrackRecord(Convert.ToInt32(ViewState["STUDENT_USERNO"]));
                if (dsProgress.Tables[0] != null && dsProgress.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(dsProgress.Tables[0].Rows[0]["APPLICATION_STATUS"]) == "1")
                    {
                        iOnlineAdmSub.Attributes.Add("class", "fa fa-check");
                    }
                    else
                    {
                        iOnlineAdmSub.Attributes.Add("class", "fa fa-times");
                    }
                    if (Convert.ToString(dsProgress.Tables[0].Rows[0]["PAY_SETAILS"]) == "1")
                    {
                        iStudDocUpload.Attributes.Add("class", "fa fa-check");
                    }
                    else
                    {
                        iStudDocUpload.Attributes.Add("class", "fa fa-times");
                    }
                    if (Convert.ToInt32(dsProgress.Tables[0].Rows[0]["MERITNO"]) > 0)
                    {
                        iAdmSecAprvl.Attributes.Add("class", "fa fa-check");
                    }
                    else
                    {
                        iAdmSecAprvl.Attributes.Add("class", "fa fa-times");
                    }
                    if (Convert.ToInt32(dsProgress.Tables[0].Rows[0]["IDNO"]) > 0)
                    {
                        iFinSecAprvl.Attributes.Add("class", "fa fa-check");
                    }
                    else
                    {
                        iFinSecAprvl.Attributes.Add("class", "fa fa-times");
                    }
                }
                else
                {
                    iOnlineAdmSub.Attributes.Add("class", "fa fa-times");
                    iStudDocUpload.Attributes.Add("class", "fa fa-times");
                    iAdmSecAprvl.Attributes.Add("class", "fa fa-times");
                    iFinSecAprvl.Attributes.Add("class", "fa fa-times");

                    //iFeePayByStud.Attributes.Add("class", "fa fa-times");
                }
                dsProgress.Dispose();
                return;
                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_CollegeMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnSubmit1_Click(object sender, EventArgs e)
    {
        try
        {
            string ApplicationNo = ""; string ipAddress = "";
            string EnqueryNos = ""; string User = ""; int count = 0;
            foreach (ListViewDataItem item in lvLeadDetails.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("chkCheck");
                if (chk.Checked == true && chk.Enabled == true)
                {
                   // Label lblApplicationNo = (Label)item.FindControl("lblApplicationNo");
                    LinkButton lnkApplicationNo = (LinkButton)item.FindControl("lnkApplicationNo");
                    HiddenField hdnEnqueryNo = (HiddenField)item.FindControl("hdnEnqueryNo");
                    ApplicationNo += lnkApplicationNo.Text + ',';
                    EnqueryNos += hdnEnqueryNo.Value + ',';
                    count++;
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please select at least one student in student list.", this.Page);
                return;
            }
            //foreach (ListItem items in ddlUser.Items)
            //{
            //    if (items.Selected == true)
            //    {
            //        User += items.Value + ',';
            //    }
            //}
            User = ddlUser.SelectedValue;
            ApplicationNo = ApplicationNo.ToString().TrimEnd(',');
            EnqueryNos = EnqueryNos.ToString().TrimEnd(',');
            ipAddress = GetUserIPAddress();
            int Status = objSC.Insert_StudentRecord(User, ApplicationNo, EnqueryNos, ipAddress, Convert.ToInt32(Session["userno"]));
            if (Status == 1)
            {
                Session["ADD_ALERT"] = "1";
                Clear();
            }
            else if (Status == 2)
            {
                Session["ADD_ALERT"] = "2";
                Clear();
            }
            else
            {
                Session["ADD_ALERT"] = "3";
                Clear();
            }
        }
        catch (Exception ex)
        {

        }
    }
    private string GetUserIPAddress()
    {
        string User_IPAddress = string.Empty;
        string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
        {
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];

        }
        return User_IPAddress;
    }

    protected void lkAnnouncement_Click(object sender, EventArgs e)
    {
      
        ddlAdmissionBatch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlFormCategory.SelectedIndex = 0;
 
        divReports.Visible = false;
        divAnnounce.Visible = true;
        divQuery.Visible = false;
        divEnquiry.Visible = false;
        divRemark.Visible = false;

        divlkReports.Attributes.Remove("class");
        divlkAnnouncement.Attributes.Add("class", "active");
        divlkQuery.Attributes.Remove("class");
        divlkEnquiry.Attributes.Remove("class");
        DivlkRemark.Attributes.Remove("class");

        lvShowReport.DataSource = null;
        lvShowReport.DataBind();
        lvShowReport.Visible = false;


        lvStudentQuery.DataSource = null;
        lvStudentQuery.DataBind();
        lvStudentQuery.Visible = false;




    }
  
    protected void lkReports_Click(object sender, EventArgs e)
    {
        ddlAdmissionBatch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlFormCategory.SelectedIndex = 0;

        divAnnounce.Visible = false;
        divQuery.Visible = false;
        divReports.Visible = true;
        divEnquiry.Visible = false;
        divRemark.Visible = false;

        divlkEnquiry.Attributes.Remove("class");
        divlkAnnouncement.Attributes.Remove("class");
        divlkQuery.Attributes.Remove("class");
        divlkReports.Attributes.Add("class", "active");
        DivlkRemark.Attributes.Remove("class");

        divdate.Visible = false;
 
        divstudent.Visible = false;
        divAllDemands.Visible = false;
        lvLeadDetails.DataSource = null;
        lvLeadDetails.DataBind();
        DivMAinPanel.Visible = false;

        lvStudentQuery.DataSource = null;
        lvStudentQuery.DataBind();
        lvStudentQuery.Visible = false;

        lvOnlineAdmissionDetails.DataSource = null;
        lvOnlineAdmissionDetails.DataBind();
    }

    public void controValid()
    {
        if (ddlAdmissionBatch.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Intake", this.Page);
            return;
        }

        //if (ddlDegree.SelectedValue == "0")
        //{
        //    objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Degree", this.Page);
        //    return;
        //}
    }

    public void ClearReprtControls()
    {
        ddlAdmissionBatch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        lvShowReport.DataSource = null;
        lvShowReport.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearReprtControls();
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        controValid();
        DataSet ds = null;
        ds = objSC.getShowReport(Convert.ToInt32(ddlAdmissionBatch.SelectedValue),Convert.ToInt32(ddlDegree.SelectedValue),1);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvShowReport.DataSource = ds;
            lvShowReport.DataBind();
            lvShowReport.Visible = true;
        }
        else
        {
            objCommon.DisplayMessage(UpdatePanel1, "Record Not Found!", this.Page);
            lvShowReport.DataSource = null;
            lvShowReport.DataBind();
            lvShowReport.Visible = false;
            return;
        }
    }

    protected void btnAdmReport_Click(object sender, EventArgs e)
    {
        try
        {
            lvShowReport.DataSource = null;
            lvShowReport.DataBind();

            if (ddlAdmissionBatch.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Intake", this.Page);
                return;
            }

            //if (ddlDegree.SelectedValue == "0")
            //{
            //    objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Degree", this.Page);
            //    return;
            //}

            int admBatch = Convert.ToInt32(ddlAdmissionBatch.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
            int degreeno = Convert.ToInt32(ddlDegree.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlDegree.SelectedValue);
            DataSet dsStudExcel = objCommon.DynamicSPCall_Select("PKG_ACD_GET_ALL_STUDENT_DATA_IN_EXCEL", "@P_ADMBATCH,@P_DEGREENO,@P_COLLEGE_ID", "" + admBatch + "," + degreeno + "," + 0 + "");
            if (dsStudExcel.Tables[0].Rows.Count <= 0)
            {
                objCommon.DisplayMessage(this.Page, "Record Not Found!", this.Page);
                return;
            }
            else
            {
                GridView gvStudData = new GridView();
                gvStudData.DataSource = dsStudExcel;
                gvStudData.DataBind();
                string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
                string attachment = "attachment; filename=" + degreeno + "StudentDetailsReport.xls";
                // string attachment = "attachment; filename=Applied_Students_Status_Report.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Response.Write(FinalHead);
                gvStudData.RenderControl(htw);
                //string a = sw.ToString().Replace("_", " ");
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            objCommon = new Common();
            objCommon.DisplayMessage(UpdatePanel1, ex.Message.ToString(), this);
        }
    }

    protected void btnStudentDataReport_Click(object sender, EventArgs e)
    {
        try
        {
            lvShowReport.DataSource = null;
            lvShowReport.DataBind();

            if (ddlAdmissionBatch.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Intake", this.Page);
                return;
            }

            //if (ddlDegree.SelectedValue == "0")
            //{
            //    objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Degree", this.Page);
            //    return;
            //}

            int admBatch = Convert.ToInt32(ddlAdmissionBatch.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
            int degreeno = Convert.ToInt32(ddlDegree.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlDegree.SelectedValue);
            //int collegeid = Convert.ToInt32(ddlCollege.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlCollege.SelectedValue);
            //string Colname = objCommon.LookUp("ACD_COLLEGE_MASTER", "SHORT_NAME", "COLLEGE_ID=" + collegeid);
            DataSet dsStudExcel = objCommon.DynamicSPCall_Select("PKG_ACD_GET_ALL_STUDENT_DATA_IN_EXCEL", "@P_ADMBATCH,@P_DEGREENO,@P_COLLEGE_ID", "" + admBatch + "," + degreeno + "," + 0 + "");
            if (dsStudExcel.Tables[0].Rows.Count <= 0)
            {
                objCommon.DisplayMessage(this.Page, "Record Not Found!", this.Page);
                return;
            }
            else
            {
                GridView gvStudData = new GridView();
                gvStudData.DataSource = dsStudExcel;
                gvStudData.DataBind();
                string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
                string attachment = "attachment; filename=" + degreeno + "StudentDetailsReport.xls";
                // string attachment = "attachment; filename=Applied_Students_Status_Report.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Response.Write(FinalHead);
                gvStudData.RenderControl(htw);
                //string a = sw.ToString().Replace("_", " ");
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            objCommon = new Common();
            objCommon.DisplayMessage(UpdatePanel1, ex.Message.ToString(), this);
        }
 }

    protected void btnFillUpStatusReport_Click(object sender, EventArgs e)
    {
        try
        {
            lvShowReport.DataSource = null;
            lvShowReport.DataBind();

            controValid();
            DataSet ds = objSC.getShowReport(Convert.ToInt32(ddlAdmissionBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue),2);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Record Not Found!", this.Page);
                return;
            }
            else
            {
                GridView gvStudData = new GridView();
                gvStudData.DataSource = ds;
                gvStudData.DataBind();
                string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
                string attachment = "attachment; filename=Applied_Students_Status_Report" + (DateTime.Now).ToString("dd/MM/yyyy") + ".xls";
               // string attachment = "attachment; filename=Applied_Students_Status_Report.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Response.Write(FinalHead);
                gvStudData.RenderControl(htw);
                //string a = sw.ToString().Replace("_", " ");
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            objCommon = new Common();
            objCommon.DisplayMessage(UpdatePanel1, ex.Message.ToString(), this);
        }
    }

    protected void ddlMainLeadLabel_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ClearListview();
        lstbSecondHead.Items.Clear();
        lstbThirdHead.Items.Clear();

        divdate.Visible = false;
        if (ddlMainLeadLabel.SelectedIndex == 0 || ddlMainLeadLabel.SelectedValue =="")
        {
            objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Main Heading.", this.Page);
            return;
        }

       
        string mainIds = string.Empty;
        foreach (ListItem items in ddlMainLeadLabel.Items)
        {
            if (items.Selected == true)
            {
                mainIds += items.Value + ',';

            }
        }
        mainIds = mainIds.Substring(0, mainIds.Length - 1);
        DataSet ds = null;
        ds = objSC.getMainHeadData(mainIds);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            //ddlMainLeadLabel.SelectedValue = null;
            lstbThirdHead.SelectedValue = null;
            //objCommon.FillListBox(lstbSecondHead, "ACD_LEAD_STAGE", "DISTINCT LEADNO", "LEAD_STAGE_NAME", "LEADNO > 0", "LEADNO");
            lstbSecondHead.DataSource = ds;
            //lstbSecondHead.DataTextField = ds.Tables[0].Rows[0]["APPLICATION_NAME"].ToString();
            //lstbSecondHead.DataValueField = ds.Tables[0].Rows[0]["ID"].ToString();
            lstbSecondHead.DataTextField ="APPLICATION_NAME";
            lstbSecondHead.DataValueField ="ID";
            lstbSecondHead.DataBind();  

        }
        else
        {
            objCommon.DisplayMessage(UpdatePanel1, "Record Not Found!", this.Page);
            lvShowReport.DataSource = null;
            lvShowReport.DataBind();
           // ddlMainLeadLabel.SelectedValue = null;
            lstbThirdHead.SelectedValue = null;
            lvShowReport.Visible = false;
            return;
        }
    }

    protected void lstbSecondHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ClearListview();
        if (lstbSecondHead.SelectedValue == "")
        {
            objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Sub Heading.", this.Page);
            return;
        }


        string mainIds = string.Empty;
        foreach (ListItem items in lstbSecondHead.Items)
        {
            if (items.Selected == true)
            {
                mainIds += items.Text + ',';

            }
        }
        mainIds = mainIds.Substring(0, mainIds.Length - 1);
        DataSet ds = null;

        if (Convert.ToString(lstbSecondHead.SelectedItem) == "Date")
            {
                lstbThirdHead.Items.Clear();
                divdate.Visible = true;
                dvThirdListbox.Visible = false;
            }
        else
            {
                         ds = objSC.getSecondHeadData(mainIds);
                         if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                         {
                       
                             divdate.Visible = false;
                             lstbThirdHead.Items.Clear();
                             lstbThirdHead.DataSource = ds;
                             lstbThirdHead.DataTextField = "APPLICATION_NAME";
                             lstbThirdHead.DataValueField = "ID";
                             lstbThirdHead.DataBind();
                             dvThirdListbox.Visible = true;
                         }
                         else
                         {
                             divdate.Visible = false;
                           //  objCommon.DisplayMessage(UpdatePanel1, "Record Not Found For This Selection!", this.Page);
                             //lstbThirdHead.DataSource = null;
                             //lstbThirdHead.DataBind();
                             dvThirdListbox.Visible = false;
                             lstbThirdHead.Items.Clear();
                             lvShowReport.Visible = false;
                             return;
                         }
            }

        
    }

    public void ClearListview()
    {
        lvLeadDetails.DataSource = null;
        lvLeadDetails.DataBind();
        DivMAinPanel.Visible = false;
        divstudent.Visible = false;
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        ddlMainLeadLabel.SelectedValue = "0";


        lstbSecondHead.Items.Clear();
        lstbThirdHead.Items.Clear();
        divdate.Visible = false;
        ClearListview();

        return;

    }

    protected void btnApplyFilter_Click(object sender, EventArgs e)
    {
        try
        {

            string[] date;
            if (Convert.ToString(hdnDate.Value) == string.Empty || Convert.ToString(hdnDate.Value) == "0")
            {
                date = ",".Split(',');
            }
            else
            {
                date = hdnDate.Value.ToString().Split('-');
                date[0] = Convert.ToDateTime(date[0]).ToString("dd/MM/yyyy");
                date[1] = Convert.ToDateTime(date[1]).ToString("dd/MM/yyyy");
            }
            if (ddlMainLeadLabel.SelectedIndex == 0 || ddlMainLeadLabel.SelectedValue == "")
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Main Heading.", this.Page);
                return;
            }

            if (lstbSecondHead.SelectedValue == "")
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Sub Heading.", this.Page);
                return;
            }

            int adm_type = 0;
            for (int i = 0; i < ddlMainLeadLabel.Items.Count; i++)
            {
                foreach (string category in ddlMainLeadLabel.ToString().Split(','))
                {
                    if (ddlMainLeadLabel.Items[i].Value == "2")
                    {
                        //getApplicationStatus();
                        //return;
                    }
                   
                }
            }

            if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                adm_type = 0;
            }
            else
            {
                adm_type = Convert.ToInt32(Session["userno"]);
            }

          
            DataSet ds = null;
            if (Convert.ToString(lstbSecondHead.SelectedItem) == "Country")
            {
                if (lstbThirdHead.SelectedValue == "")
                {
                    objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Sub Heading of Country.", this.Page);
                    return;
                }

                ds = objSC.getstudentid(Convert.ToString(lstbThirdHead.SelectedValue), "0", adm_type, 1, Convert.ToInt32(ddlAdmbatch.SelectedValue), 0, Convert.ToInt32(ddlDiscipline.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlAptitudeTestCentre.SelectedValue), Convert.ToInt32(ddlAptitudeTestMedium.SelectedValue), date[0], date[1]);
            }
            else if (Convert.ToString(lstbSecondHead.SelectedItem) == "Date")
            {
                string[] SplitDate = txtstartdate.Text.Split('-');
                ds = objSC.getstudentid(Convert.ToString(SplitDate[0].TrimEnd(' ')), Convert.ToString(SplitDate[1].TrimStart(' ')), adm_type, 2, Convert.ToInt32(ddlAdmbatch.SelectedValue), 0, Convert.ToInt32(ddlDiscipline.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlAptitudeTestCentre.SelectedValue), Convert.ToInt32(ddlAptitudeTestMedium.SelectedValue), date[0], date[1]);
            }
            else if (Convert.ToString(lstbSecondHead.SelectedItem) == "Application Status")
            {
                if (lstbThirdHead.SelectedValue == "")
                {
                    objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Sub Heading of Status.", this.Page);
                    return;
                }

                ds = objSC.getstudentid(Convert.ToString(lstbThirdHead.SelectedValue), "0", adm_type, 3, Convert.ToInt32(ddlAdmbatch.SelectedValue), 0, Convert.ToInt32(ddlDiscipline.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlAptitudeTestCentre.SelectedValue), Convert.ToInt32(ddlAptitudeTestMedium.SelectedValue), date[0], date[1]);
            }
            else if (Convert.ToString(lstbSecondHead.SelectedItem) == "Todays")
            {
                ds = objSC.getstudentid(Convert.ToString((DateTime.UtcNow.ToString("MM/dd/yyyy")).TrimEnd(' ')), "0", adm_type, 4, Convert.ToInt32(ddlAdmbatch.SelectedValue), 0, Convert.ToInt32(ddlDiscipline.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlAptitudeTestCentre.SelectedValue), Convert.ToInt32(ddlAptitudeTestMedium.SelectedValue), date[0], date[1]);
            }
            else if (Convert.ToString(lstbSecondHead.SelectedItem) == "Payment Completed")
            {
                ds = objSC.getstudentid(Convert.ToString("0"), "0", adm_type, 6, Convert.ToInt32(ddlAdmbatch.SelectedValue), 0, Convert.ToInt32(ddlDiscipline.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlAptitudeTestCentre.SelectedValue), Convert.ToInt32(ddlAptitudeTestMedium.SelectedValue), date[0], date[1]);
            }
            else if (Convert.ToString(lstbSecondHead.SelectedItem) == "Payment Incompleted")
            {
                ds = objSC.getstudentid(Convert.ToString("0"), "0", adm_type, 7, Convert.ToInt32(ddlAdmbatch.SelectedValue), 0, Convert.ToInt32(ddlDiscipline.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlAptitudeTestCentre.SelectedValue), Convert.ToInt32(ddlAptitudeTestMedium.SelectedValue), date[0], date[1]);
            }


            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvLeadDetails.DataSource = ds;
                lvLeadDetails.DataBind();
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
                divAllDemands.Visible = true;
                //txtRemark.Visible = false;s
                btnSubmit1.Visible = true;
                btnClear1.Visible = true;
                showbar.Visible = true;
                if (Convert.ToString(Session["usertype"]) == "1") { divstudent.Visible = true; } else { divstudent.Visible = false; }
                lblTotalCount.Visible = true;
                lblTotalCount.Text = ds.Tables[0].Rows.Count.ToString();
                DivMAinPanel.Visible = true;
                // remark.Visible = true;
                lblDynamicPageTitle.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Record Not Found!", this.Page);
                lvLeadDetails.DataSource = null;
                lvLeadDetails.DataBind();
                DivMAinPanel.Visible = false;
                divstudent.Visible = false;
                lblTotalCount.Text = "0";
                //this.BindListView();
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_adminmaster.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        
    }

    public void getApplicationStatus()
    {

        DataSet ds = null;
        //rdoAll.ClearSelection();
        // rdoAll.Checked = false;
        ds = objSC.getLeadStatusData(Convert.ToInt32(lstbSecondHead.SelectedValue));//PKG_GET_LEAD_STATUS_DATA
        
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvLeadDetails.DataSource = ds;
            lvLeadDetails.DataBind();
           //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
        }
        else
        {
            lvLeadDetails.DataSource = null;
            lvLeadDetails.DataBind();
        }
    }


    /// <summary>
    /// query panel code
    /// </summary>
    /// started BY Aashna 28-10-2021 
    protected void ddlFormCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListViewQuery();
    }
    private void BindListViewQuery()
    {
        try
        {
            int categoryno = Convert.ToInt32(ddlFormCategory.SelectedValue);
            DataSet ds = objFAQ.GetAllStudent_Feadback(categoryno);
            if (ds != null)
            {
               
                lvStudentQuery.DataSource = ds;
                lvStudentQuery.DataBind();
                lvStudentQuery.Visible = true;
            }
            else
            {
               
                lvStudentQuery.DataSource = null;
                lvStudentQuery.DataBind();
                lvStudentQuery.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_ProgramType_Master.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }   
    protected void lvStudentQuery_PagePropertiesChanged(object sender, EventArgs e)
    {
        DataPager dp = (DataPager)lvStudentQuery.FindControl("DataPager1");
        BindListViewQuery();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int status = 0;
        int Catno = 0;
        int userno = 0;
        int Queryno = 0;
        int USERNO = 0;
        int IDNO = 0;
        string UserID = string.Empty;  

        string FeedbackQuery = string.Empty;
        string feedback_reply = string.Empty;
        string replied_user = objCommon.LookUp("USER_ACC", "UA_NO", "UA_NO=" + Convert.ToInt32(Session["userno"]));
        try
        {

            Catno = Convert.ToInt32(ViewState["CategoryNo"]);
            userno = Convert.ToInt32(ViewState["userno"]);
            feedback_reply = txtFeedback.Text;
            Queryno = Convert.ToInt32(ViewState["QueryNo"]);
           // int Queryno = Convert.ToInt32(objCommon.LookUp("ACD_USER_QUERY_DETAILS", "QUERYNO", "USERNO=" + userno + "AND ACTIVE_STATUS=1 AND QUERY_CATEGORY=" + Catno));
           // int categoryno = Convert.ToInt32(objCommon.LookUp("ACD_USER_QUERY_DETAILS", "QUERY_CATEGORY", "USERNO=" + userno + " AND QUERYNO=" + Queryno));

            int categoryno = Convert.ToInt32(objCommon.LookUp("ACD_USER_QUERY_DETAILS", "QUERY_CATEGORY", "USERNO=" + userno + "OR IDNO=" + userno + " AND QUERYNO=" + Queryno));

            DataSet ds = objCommon.FillDropDown("ACD_USER_QUERY_DETAILS", "USERNO", "IDNO", "USERNO=" + userno + "OR IDNO=" + userno + "AND QUERYNO=" + Queryno + "", "");

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                USERNO = Convert.ToInt32(ds.Tables[0].Rows[0]["USERNO"].ToString() == string.Empty ? "0" : ds.Tables[0].Rows[0]["USERNO"].ToString());
                IDNO = Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString() == string.Empty ? "0" : ds.Tables[0].Rows[0]["IDNO"].ToString());

                if (USERNO == 0)
                {
                    USERNO = 0;
                    IDNO = Convert.ToInt16(ds.Tables[0].Rows[0]["IDNO"]);
                    UserID = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + IDNO);
                    ViewState["userno"] = IDNO;
                }
                else
                {
                    USERNO = Convert.ToInt32(ds.Tables[0].Rows[0]["USERNO"].ToString());
                    IDNO = 0;
                    UserID = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + USERNO);
                    ViewState["userno"] = USERNO;
                }
            }

            string Ip_Address = Request.ServerVariables["REMOTE_HOST"];
            //string UserID = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + userno);
            status = Convert.ToInt32(ddlStatus.SelectedValue);
            //int result = objFAQ.Add_Feedback_Reply(userno, UserID, feedback_reply, Ip_Address, categoryno, status, Queryno, replied_user);
            int result = objFAQ.Add_Feedback_Reply(USERNO, UserID, feedback_reply, Ip_Address, categoryno, status, Queryno, replied_user, IDNO);

            if (result != -99)
            {
                lblStatus1.Visible = true;
                lblStatus1.ForeColor = System.Drawing.Color.Green;
                lblStatus1.Text = "Query Submitted Successfully!";
                txtFeedback.Text = "";
                BindConversation();

            }
            //BindListView();

            if (ddlStatus.SelectedValue == "2")
            {

                ddlStatus.Visible = false;
                txtFeedback.Visible = false;
                btnSubmit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Online_QueryManagement.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    private void BindConversation()
    {
        int userno = 0;
        userno = Convert.ToInt32(ViewState["userno"]);
        int query = 0;
      //  DataSet user_ds = objCommon.FillDropDown("ACD_USER_QUERY_DETAILS Q LEFT JOIN ACD_USER_FEEDBACK F ON(F.QUERYNO=Q.QUERYNO) ", "DISTINCT Q.QUERYNO,(CASE WHEN QUERY_DETAILS IS NULL THEN '' ELSE CONCAT( CONCAT('<em class=re-ply>Applicant:</em> ',QUERY_DETAILS), '<em class=appli-cant><small><br/><sd>'+ cast(FEEDBACK_DATE as varchar(50)) + '</sd></small></em>') END) AS FEEDBACK_DETAILS", "(CASE WHEN FEEDBACK_REPLY IS NULL THEN '' ELSE CONCAT(CONCAT('<em class=appli-cant>SLIIT Reply('+ REPLIED_USER +'):</em> ',FEEDBACK_REPLY), '<em class=re-ply><small><br/><sd>'+ cast(FEEDBACK_DATE as varchar(50)) + '</sd></small></em>') END) AS FEEDBACK_REPLY", "Q.USERNO=" + userno + "", "Q.QUERYNO ASC");
        //DataSet user_ds = objCommon.FillDropDown("ACD_USER_QUERY_DETAILS Q LEFT JOIN ACD_USER_FEEDBACK F ON(F.QUERYNO=Q.QUERYNO) ", "DISTINCT Q.QUERYNO,(CASE WHEN QUERY_DETAILS IS NULL THEN '' ELSE CONCAT( CONCAT('<em class=re-ply>Applicant:</em> ',QUERY_DETAILS), '<em class=appli-cant><small><br/><sd>'+ cast(QUERY_DATE as varchar(50)) , '</sd></small></em>' + cast(FEEDBACK_DATE as varchar(50))) END) AS FEEDBACK_DETAILS", "(CASE WHEN FEEDBACK_REPLY IS NULL THEN '' ELSE CONCAT(CONCAT('<em class=appli-cant>SLIIT Reply('+ REPLIED_USER +'):</em> ',FEEDBACK_REPLY), '<em class=re-ply><small><br/><sd>'+ cast(FEEDBACK_DATE as varchar(50)) + '</sd></small></em>') END) AS FEEDBACK_REPLY", "Q.USERNO=" + userno + "", "Q.QUERYNO ASC");
        // DataSet user_ds = objCommon.FillDropDown("ACD_USER_QUERY_DETAILS", "(CASE WHEN USER_QUERY_DETAILS IS NULL THEN '' ELSE CONCAT( CONCAT('<em>Applicant:</em> ',USER_QUERY_DETAILS), '<em><small><br/><sd>'+ cast(USER_QUERY_DATE as varchar(50)) + '</sd></small></em>') END) AS FEEDBACK_DETAILS", "(CASE WHEN ADMIN_FEEDBACK_REPLY IS NULL THEN '' ELSE CONCAT(CONCAT('<em>AUAT Reply('+ REPLIED_USER +'):</em> ',ADMIN_FEEDBACK_REPLY), '<em><small><br/><sd>'+ cast(USER_QUERY_DATE as varchar(50)) + '</sd></small></em>') END) AS ADMIN_FEEDBACK_REPLY", "USERNO=" + userno + " AND QUERYNO=" + Convert.ToInt32(ViewState["QueryNo"]) + "", "QUERYNO");
        //DataSet user_ds = objCommon.FillDropDown("ACD_USER_QUERY_DETAILS Q LEFT JOIN ACD_USER_FEEDBACK F ON(F.QUERYNO=Q.QUERYNO) ", "DISTINCT Q.QUERYNO,(CASE WHEN QUERY_DETAILS IS NULL THEN '' ELSE CONCAT( CONCAT('<em class=re-ply>Applicant:</em> ',QUERY_DETAILS), '<em class=appli-cant><small><br/><sd>'+ cast(QUERY_DATE as varchar(50)) , '</sd></small></em>' + cast(FEEDBACK_DATE as varchar(50))) END) AS FEEDBACK_DETAILS", "(CASE WHEN FEEDBACK_REPLY IS NULL THEN '' ELSE CONCAT(CONCAT('<em class=appli-cant>SLIIT Reply('+ REPLIED_USER +'):</em> ',FEEDBACK_REPLY), '<em class=re-ply><small><br/><sd>'+ cast(FEEDBACK_DATE as varchar(50)) + '</sd></small></em>') END) AS FEEDBACK_REPLY", "Q.USERNO=" + userno + "OR Q.IDNO=" + userno + "", "Q.QUERYNO ASC");
        string SP_Name2 = "PKG_ACD_GET_CONVERSATION";
        string SP_Parameters2 = "@P_USERNO";
        string Call_Values2 = "" + Convert.ToInt32(userno) + "";
        DataSet user_ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);    
        foreach (ListViewDataItem dataitem in lvFeedbackReply.Items)
        {
            Label LBLQUERY = dataitem.FindControl("lblUserReply") as Label;
            HiddenField hdfquery = dataitem.FindControl("hdfquery") as HiddenField;
            query = Convert.ToInt32(hdfquery.Value);
        }
        lvFeedbackReply.DataSource = user_ds;
        lvFeedbackReply.DataBind();
    }

    protected void btnPriview_Click(object sender, EventArgs e)
    {

        ViewState["CategoryNo"] = null;
        ViewState["userno"] = null;
        ImageButton btnImgB = sender as ImageButton;
        Button btnPreview = sender as Button;

        int userno = Int32.Parse(btnPreview.CommandName);
        int Catno = Int32.Parse(btnPreview.ToolTip);
       // int Queryno = Convert.ToInt32(objCommon.LookUp("ACD_USER_QUERY_DETAILS", "QUERYNO", "USERNO=" + userno + "AND QUERY_CATEGORY=" + Catno));
        // int Queryno = Int32.Parse(btnPreview.ToolTip);
      //  int categoryno = Convert.ToInt32(objCommon.LookUp("ACD_USER_QUERY_DETAILS", "QUERY_CATEGORY", "USERNO=" + userno + " AND QUERYNO=" + Queryno));




        int Queryno = Convert.ToInt32(objCommon.LookUp("ACD_USER_QUERY_DETAILS", "QUERYNO", "USERNO=" + userno + "OR IDNO=" + userno + "AND QUERY_CATEGORY=" + Catno));

        int categoryno = Convert.ToInt32(objCommon.LookUp("ACD_USER_QUERY_DETAILS", "QUERY_CATEGORY", "USERNO=" + userno + "OR IDNO=" + userno + " AND QUERYNO=" + Queryno));

        //     ViewState["idno"] = Convert.ToInt32(idno);
        


        ViewState["QueryNo"] = Convert.ToInt32(Queryno);
        ViewState["CategoryNo"] = Convert.ToInt32(categoryno);
        ViewState["userno"] = Convert.ToInt32(userno);
      //  int Active_Status = Convert.ToInt32(objCommon.LookUp("ACD_USER_QUERY_DETAILS", "ACTIVE_STATUS", "USERNO=" + userno + " AND QUERYNO=" + Queryno + " AND QUERY_CATEGORY=" + categoryno));
        int Active_Status = Convert.ToInt32(objCommon.LookUp("ACD_USER_QUERY_DETAILS", "ACTIVE_STATUS", "USERNO=" + userno + "OR IDNO=" + userno + " AND QUERYNO=" + Queryno + " AND QUERY_CATEGORY=" + categoryno));
        
        if (Active_Status == 2)
        {
            ddlStatus.Visible = false;
            txtFeedback.Visible = false;
            btnSubmit.Visible = false;
        }
        else
        {
            ddlStatus.Visible = true;
            txtFeedback.Visible = true;
            btnSubmit.Visible = true;
        }
        ddlStatus.SelectedIndex = 0;
        BindConversation();


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showPopup();", true);


    }
    protected void lvStudentQuery_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if ((e.Item.ItemType == ListViewItemType.DataItem))
        {
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;
            DataRow dr = ((DataRowView)dataItem.DataItem).Row;

            if (dr["FEEDBACK_STATUS"].Equals("CLOSED"))
            {

                ((Label)e.Item.FindControl("lblStatus") as Label).ForeColor = System.Drawing.Color.Red;
                ((Label)e.Item.FindControl("lblStatus") as Label).Font.Bold = true;

            }
            else if (dr["FEEDBACK_STATUS"].Equals("OPEN"))
            {
                ((Label)e.Item.FindControl("lblStatus") as Label).ForeColor = System.Drawing.Color.Green;
                ((Label)e.Item.FindControl("lblStatus") as Label).Font.Bold = true;
            }
        }
    }
    protected void lkQuery_Click(object sender, EventArgs e)
    {

        ddlAdmissionBatch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
       
        divReports.Visible = false;
        divAnnounce.Visible = false;
        divQuery.Visible = true;
        divEnquiry.Visible = false;
        divRemark.Visible = false;

        divlkReports.Attributes.Remove("class");
        divlkAnnouncement.Attributes.Remove("class");
        divlkQuery.Attributes.Add("class", "active");
        divlkEnquiry.Attributes.Remove("class");
        DivlkRemark.Attributes.Remove("class");

        lvShowReport.DataSource = null;
        lvShowReport.DataBind();
        lvShowReport.Visible = false;
    }
   
    ///ENDED BY Aashna 28-10-2021 

    /// enquiry tab code started by aashna 30-10-2021
    protected void lkenquiry_Click(object sender, EventArgs e)
    {
        ddlAdmissionBatch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlFormCategory.SelectedIndex = 0;

        divReports.Visible = false;
        divAnnounce.Visible = false;
        divQuery.Visible = false;
        divEnquiry.Visible = true;
        divRemark.Visible = false;
        
        divlkReports.Attributes.Remove("class");
        divlkAnnouncement.Attributes.Remove("class");
        divlkQuery.Attributes.Remove("class");
        divlkEnquiry.Attributes.Add("class", "active");
        DivlkRemark.Attributes.Remove("class");

        lvShowReport.DataSource = null;
        lvShowReport.DataBind();
        lvShowReport.Visible = false;

        lvStudentQuery.DataSource = null;
        lvStudentQuery.DataBind();
        lvStudentQuery.Visible = false;

        
    }
    protected void ddlStudyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStudyType.SelectedIndex > 0)
            {
                objCommon.FillListBox(ddlProgramIntrested, "ACD_AREA_OF_INTEREST I INNER JOIN ACD_COLLEGE_DEGREE_BRANCH D ON I.AREA_INT_NO = D.AREA_INT_NO ", "DISTINCT D.AREA_INT_NO", "AREA_INT_NAME", "D.AREA_INT_NO > 0 AND D.ACTIVE = 1 AND UGPGOT=" + Convert.ToInt32(ddlStudyType.SelectedValue), "AREA_INT_NAME");
                //objCommon.FillListBox(ddlProgramIntrested, "ACD_AREA_OF_INTEREST I INNER JOIN ACD_COLLEGE_DEGREE_BRANCH D ON I.AREA_INT_NO = D.AREA_INT_NO INNER JOIN ACD_ADMISSION_CONFIG A ON D.COLLEGE_ID = A.COLLEGE_ID AND D.DEGREENO = A.DEGREENO AND D.BRANCHNO = A.BRANCHNO AND D.UGPGOT = A.UGPG", "DISTINCT D.AREA_INT_NO", "AREA_INT_NAME", "D.AREA_INT_NO > 0 AND ADMBATCH = (SELECT MAX(ADMBATCH) FROM ACD_ADMISSION_CONFIG) AND UGPGOT=" + Convert.ToInt32(ddlStudyType.SelectedValue) + " AND ((CONVERT(VARCHAR(8),GETDATE(),112)) BETWEEN (CONVERT(VARCHAR(8),ADMSTRDATE,112)) AND (CONVERT(VARCHAR(8),ADMENDDATE,112)))", "AREA_INT_NAME");
            }
            else
            {
                ddlProgramIntrested.Items.Clear();
                ddlPreference.Items.Clear();
            }
            if (ddlProgramIntrested.Items.Count == 0)
            {
                objCommon.DisplayMessage(updEnquiry, "Program Interested in not found Please contact online admission department!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlProgramIntrested_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Intrest = "0";
        foreach (ListItem items in ddlProgramIntrested.Items)
        {
            if (items.Selected == true)
            {
                Intrest += items.Value + ',';
            }
        }
        objCommon.FillListBox(ddlPreference, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_AREA_OF_INTEREST AOF ON A.AREA_INT_NO = AOF.AREA_INT_NO INNER JOIN ACD_BRANCH B ON A.BRANCHNO = B.BRANCHNO INNER JOIN ACD_DEGREE D ON A.DEGREENO = D.DEGREENO ", "DISTINCT CONVERT(NVARCHAR(10),A.COLLEGE_ID) +','+ CONVERT(NVARCHAR(10),A.DEGREENO) + ',' + CONVERT(NVARCHAR(10),A.BRANCHNO)", "D.CODE + '-' + LONGNAME", "A.ACTIVE = 1 AND UGPGOT = " + Convert.ToInt32(ddlStudyType.SelectedValue) + " AND B.BRANCHNO > 0 AND A.AREA_INT_NO IN(" + Intrest.TrimEnd(',') + ")", "");

        //objCommon.FillListBox(ddlPreference, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_AREA_OF_INTEREST AOF ON A.AREA_INT_NO = AOF.AREA_INT_NO INNER JOIN ACD_BRANCH B ON A.BRANCHNO = B.BRANCHNO INNER JOIN ACD_DEGREE D ON A.DEGREENO = D.DEGREENO INNER JOIN ACD_ADMISSION_CONFIG AA ON AA.COLLEGE_ID = A.COLLEGE_ID AND AA.DEGREENO = A.DEGREENO AND AA.BRANCHNO = A.BRANCHNO AND AA.UGPG = A.UGPGOT", "DISTINCT CONVERT(NVARCHAR(10),A.COLLEGE_ID) +','+ CONVERT(NVARCHAR(10),A.DEGREENO) + ',' + CONVERT(NVARCHAR(10),A.BRANCHNO)", "D.CODE + '-' + LONGNAME", "((CONVERT(VARCHAR(8),GETDATE(),112)) BETWEEN (CONVERT(VARCHAR(8),ADMSTRDATE,112)) AND (CONVERT(VARCHAR(8),ADMENDDATE,112))) AND AA.ADMBATCH = (SELECT MAX(ADMBATCH) FROM ACD_ADMISSION_CONFIG) AND UGPG = " + Convert.ToInt32(ddlStudyType.SelectedValue) + " AND B.BRANCHNO > 0 AND A.AREA_INT_NO IN(" + Intrest.TrimEnd(',') + ")", "");
    
    }
    protected void FillDropDown()
    {
        DataSet ds = objUCS.GetRegistrationBulkDetails(Convert.ToString(0), Convert.ToString(0), 0);
        ddlMobileCode.DataSource = ds.Tables[0];
        ddlMobileCode.DataValueField = "COUNTRYNO";
        ddlMobileCode.DataTextField = "COUNTRYNAME";
        ddlMobileCode.DataBind();
        ddlMobileCode.SelectedValue = "212";
        txtMobileNo.Text = "0";

        ddlHomeTelMobileCode.DataSource = ds.Tables[0];
        ddlHomeTelMobileCode.DataValueField = "COUNTRYNO";
        ddlHomeTelMobileCode.DataTextField = "COUNTRYNAME";
        ddlHomeTelMobileCode.DataBind();
        ddlHomeTelMobileCode.SelectedValue = "212";
     

        ddlStudyType.DataSource = ds.Tables[1];
        ddlStudyType.DataValueField = "UA_SECTION";
        ddlStudyType.DataTextField = "UA_SECTIONNAME";
        ddlStudyType.DataBind();
    }   
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {
          
             if (txtDob.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtDob.Text).Year > 2009 || txtDob.Text.Length < 1)
                {
                    objCommon.DisplayMessage(updEnquiry, "Date of Birth is not valid", this.Page);
                    return;
                }
            }
            if (Convert.ToString(txtNIC.Text) == string.Empty && Convert.ToString(txtPassport.Text) == string.Empty)
            {
                objCommon.DisplayMessage(updEnquiry, "Passport No. OR NIC(National Identity card) is Required !", this.Page);
                return;
            }
            DataSet UserInfoDS = objUCS.GetRegistrationBulkDetails(Convert.ToString(txtEmailid.Text.Trim()), Convert.ToString(txtMobileNo.Text.Trim()), Convert.ToInt32(ddlStudyType.SelectedValue));
            if (UserInfoDS.Tables[2].Rows.Count > 0)
            {
                if (UserInfoDS.Tables[2].Rows[0]["EMAILID"].ToString() == txtEmailid.Text.Trim() && UserInfoDS.Tables[2].Rows[0]["USER_PASSWORD"].ToString() != "")
                {
                    //objCommon.DisplayMessage(updEnquiry, "This Email is Already Registered for " + ddlStudyType.SelectedItem.Text + " Study Level", this.Page);
                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#confirm').modal('show')", true);
                    return;
                }
                else if (UserInfoDS.Tables[2].Rows[0]["MOBILENO"].ToString() == txtMobileNo.Text.Trim() && UserInfoDS.Tables[2].Rows[0]["USER_PASSWORD"].ToString() != "")
                {
                    //objCommon.DisplayMessage(updEnquiry, "This Mobile No. is Already Registered for " + ddlStudyType.SelectedItem.Text + " Study Level", this.Page);
                   
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#confirm').modal('show')", true);
                    return;
                }
                if (UserInfoDS.Tables[2].Rows[0]["EMAILID"].ToString() == txtEmailid.Text.Trim() && UserInfoDS.Tables[2].Rows[0]["MOBILENO"].ToString() == txtMobileNo.Text.Trim() && UserInfoDS.Tables[2].Rows[0]["USER_PASSWORD"].ToString() == "")
                {
                    ViewState["ResendMailMSG"] = "yes";
                    ViewState["userno"] = UserInfoDS.Tables[2].Rows[0]["USERNO"].ToString();
                    Session["EnqueryUserName"] = UserInfoDS.Tables[2].Rows[0]["USERNAME"].ToString();
                    lbluserMsg.Text = "Account already exist !!!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#confirm').modal('show')", true);
                    return;
                }
                else
                {
                    lbluserMsg.Text = "Account already exist !!!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#confirm').modal('show')", true);
                    return;
                }
            }
            if (chkConfirm.Checked == true)
            {
                int IsExists = 0;
                IsExists = Convert.ToInt32(UserInfoDS.Tables[3].Rows[0]["USER_COUNT"].ToString());

                if (IsExists == 0)
                {
                    DataTable dtRecord = new DataTable();
                    DataColumn dc = new DataColumn();
                    dc = new DataColumn("FIRSTNAME");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("EMAILID");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("MOBILENO");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("MOBILECODE");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("DATEOFBIRTH");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("PASSPORTNO");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("NIC");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("PROGRAMINTRESTS");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("COLLEGE_ID");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("DEGREENO");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("BRANCHNO");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("GENDER");
                    dtRecord.Columns.Add(dc);      
                    foreach (ListItem items in ddlProgramIntrested.Items)
                    {
                        if (items.Selected == true)
                        {
                            objus.programIntrests += items.Value + ',';
                        }
                    }
                    objus.programIntrests = objus.programIntrests.Substring(0, objus.programIntrests.Length - 1);
                    foreach (ListItem items in ddlPreference.Items)
                    {
                        if (items.Selected == true)
                        {
                            DataRow dr = dtRecord.NewRow();

                            dr["FIRSTNAME"] = txtFullName.Text.Trim().ToString().ToUpper();
                            dr["EMAILID"] = txtEmailid.Text.Trim().ToString();
                            dr["MOBILECODE"] = ddlMobileCode.Text;
                            dr["MOBILENO"] = txtMobileNo.Text;
                            dr["DATEOFBIRTH"] = Convert.ToString(txtDob.Text);
                            dr["PASSPORTNO"] = txtPassport.Text;
                            dr["NIC"] = txtNIC.Text;
                            dr["GENDER"] = rdGender.SelectedItem.Text;
                            dr["PROGRAMINTRESTS"] = objus.programIntrests;
                            string[] Split = items.Value.Split(',');
                            dr["COLLEGE_ID"] = Split[0];
                            dr["DEGREENO"] = Split[1];
                            dr["BRANCHNO"] = Split[2];
                            dtRecord.Rows.Add(dr);
                            
                        }
                    }
                    string dob = "";
                    objus.EMAILID = txtEmailid.Text.Trim().ToString();
                    objus.MOBILENO = txtMobileNo.Text;
                    objus.FIRSTNAME = txtFullName.Text.Trim().ToString().ToUpper();
                    objus.LASTNAME = txtLastName.Text.Trim().ToString().ToUpper();
                    objus.Homemobileno = txtHomeMobileNo.Text.Trim().ToString();
                    objus.UGPG = Convert.ToInt32(ddlStudyType.SelectedValue);
                    objus.ADMTYPE = 1;
                    objus.AdmBatch = Convert.ToInt32(UserInfoDS.Tables[4].Rows[0]["ADMBATCH"].ToString());
                    dob = Convert.ToString(txtDob.Text);
                    objus.PassportNo = txtPassport.Text;
                    objus.NIC = txtNIC.Text;
                    objus.Gender = rdGender.SelectedValue;
                    objus.MobileCode = ddlMobileCode.SelectedValue;
                    objus.source = Convert.ToInt32(ddlSource.SelectedValue);
                    int ret = 0;
                    if (ViewState["ResendMailMSG"] != null)
                    {
                        objus.USERNO = Convert.ToInt32(ViewState["userno"]);
                        if (Convert.ToInt32(UserInfoDS.Tables[2].Rows[0]["UGPGOT"].ToString()) == Convert.ToInt32(ddlStudyType.SelectedValue))
                        {
                            //ret = Convert.ToInt32(ViewState["userno"]);
                            ret = objUCS.RegisterNewUser(objus, dtRecord, 2, Convert.ToString(ddlHomeTelMobileCode.SelectedValue),dob);
                        }
                        else
                        {
                            objCommon.DisplayMessage(updEnquiry, "You are not change the Study Type already registered with another study type", this.Page);
                            return;
                        }

                    }
                    else if (ViewState["ResendMailMSG"] == null)
                    {
                        objus.USERNO = 0;
                        ret = objUCS.RegisterNewUser(objus, dtRecord, 1, Convert.ToString(ddlHomeTelMobileCode.SelectedValue),dob);
                        objCommon.DisplayMessage(updEnquiry, "Data Submit Successfully", this.Page);
                        DataSet dsUserContact = null;
                        dsUserContact = objUC.GetEmailTamplateandUserDetails(txtEmailid.Text.ToString(), txtMobileNo.Text.ToString(), "0", Convert.ToInt32(101010));

                        string firstname = "",username="", message = "" , emailid="";
                        if (dsUserContact != null && dsUserContact.Tables[0].Rows.Count > 0)
                        {
                            firstname = (dsUserContact.Tables[0].Rows[0]["FIRSTNAME"].ToString());
                            username = (dsUserContact.Tables[0].Rows[0]["USERNAME"].ToString());
                            emailid = (dsUserContact.Tables[0].Rows[0]["EMAILID"].ToString());
                            string Subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                            message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                            SmtpMail oMail = new SmtpMail("TryIt");

                            oMail.From = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]);

                            oMail.To = txtEmailid.Text;

                            oMail.Subject = Subject;

                            oMail.HtmlBody = message;

                            oMail.HtmlBody = oMail.HtmlBody.Replace("[USERFIRSTNAME]", firstname.ToString());
                            oMail.HtmlBody = oMail.HtmlBody.Replace("[USERNAME]", username.ToString());
                            oMail.HtmlBody = oMail.HtmlBody.Replace("[EMAILID]", txtEmailid.Text.ToString());

                           // SmtpServer oServer = new SmtpServer("smtp.live.com");
                            SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022

                            oServer.User = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]);
                            oServer.Password = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"]);

                            oServer.Port = 587;

                            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();

                            oSmtp.SendMail(oServer, oMail);
                        }
                        //PushMailServer("Registration");
                        clearcontrol();
                    }

                   
                    //if (ret != -99)
                    //{
                    //    ViewState["userno"] = ret;
                    //    this.TransferToEmail();
                    //    // this.Email();

                    //    string userMsg = "Dear " + txtFullName.Text.Trim().ToString().ToUpper() + ", Thanks for seeking admission in SLIIT. Your Application ID is '" + Session["UserName"].ToString() + "' Please use OTP '" + Convert.ToString(ViewState["otp"]) + "'for registration process.";
                    //    string msg1 = ""; string msg2 = "";
                    //    if (userMsg.Length > 160)
                    //    {
                    //        string[] arr = userMsg.Split('|');
                    //        msg1 = arr[0];
                    //        this.SendSMS(txtMobileNo.Text, msg1);
                    //        msg2 = arr[1];
                    //        this.SendSMS(txtMobileNo.Text, msg2);
                    //    }
                    //    else
                    //    {
                    //        this.SendSMS(txtMobileNo.Text, userMsg);
                    //    }
                    //    pnlmsg.Visible = true;
                    //    pnlmain.Visible = false;
                    //    btnbackerr.Visible = false;
                    //    lblError.Text = "Your Registration completed successfully, Your Application ID is '" + Session["UserName"].ToString() + "'.<br/>Please check your Email/ SMS for Application ID and OTP for further communication/ Sign In.";
                    //    lblError.ForeColor = System.Drawing.Color.Green;
                    //    lbtnregsign.Visible = true;
                    //    trnote.Visible = true;
                    //    txt_username.Text = Session["UserName"].ToString();
                    //}
                    //else
                    //{
                    //    pnlmsg.Visible = true;
                    //    btnbackerr.Visible = true;
                    //    lblError.Text = "Error...User Not Created.";
                    //}
                }
                else
                {
                    objCommon.DisplayMessage(updEnquiry, "You are already registered with this details!", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updEnquiry, "Please Confirm Information Before Submit", this.Page);

            }
        }
        catch (Exception ex)
        {

            throw;
         
        }
    }
    private string GeneartePassword()
    {
        string allowedChars = "";
        //   allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
        //    allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
        allowedChars += "1,2,3,4,5,6,7,8,9,0"; //,!,@,#,$,%,&,?
        //--------------------------------------
        char[] sep = { ',' };

        string[] arr = allowedChars.Split(sep);

        string passwordString = "";

        string temp = "";

        Random rand = new Random();

        for (int i = 0; i < 6; i++)
        {
            temp = arr[rand.Next(0, arr.Length)];
            passwordString += temp;
        }
        return passwordString;
    }
    private void PushMailServer(string title)
    {

        try
        {
            DataSet d = new DataSet();
            string pwd = GeneartePassword();
            if (ResendOTP == 0)
            {
                d = objCommon.FillDropDown("ACD_USER_REGISTRATION WITH(NOLOCK)", "DISTINCT USERNAME,USER_PASSWORD,FIRSTNAME", "USERNO,MOBILENO", "USERNO >=1 AND USER_PASSWORD IS NULL AND MOBILENO =" + txtMobileNo.Text.Trim() + " AND EMAILID='" + Convert.ToString(txtEmailid.Text) + "'", string.Empty);
            }
            else
            {
                d = objCommon.FillDropDown("ACD_USER_REGISTRATION WITH(NOLOCK)", "DISTINCT USERNAME,USER_PASSWORD,FIRSTNAME", "USERNO,MOBILENO", "USERNO >=1 AND USER_PASSWORD IS NULL AND MOBILENO =" + txtMobileNo.Text.Trim() + " AND EMAILID='" + Convert.ToString(txtEmailid.Text) + "'", string.Empty);

            }
            //string password = (d.Tables[0].Rows[0]["USER_PASSWORD"].ToString());
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "EMAILSVCID,EMAILSVCPWD", "", "");
            string password = pwd;
            ViewState["Enqueryotp"] = password;
            string username = (d.Tables[0].Rows[0]["USERNAME"].ToString());
            Session["EnqueryUserName"] = username.ToString();
            string firstname = (d.Tables[0].Rows[0]["FIRSTNAME"].ToString());
            string MOBILENO = (d.Tables[0].Rows[0]["MOBILENO"].ToString());
            var toAddress = txtEmailid.Text.Trim().ToString();

            //   USED TO SEND MAIL USING PMS
            //email = CCemailid.ToString();
            //email += "$" + TOemailid.ToString();

            string SP = "PKG_SCHEDULE_MAIL";
            string PR = "@P_PAGENO, @P_MAIL_OR_SMS, @P_SP_VAL";
            //   FOR SINGLE MAIL
            //   string VL = "" + Convert.ToInt32(Request.QueryString["pageno"]) + ",1," + TOemailid + "";  //(Pageno,Mailorsms,parameters)(used ^ for multiple parameters)

            //string VL = "" + Convert.ToInt32(Request.QueryString["pageno"]) + ",1,'" + toAddress + "^" + username + "^" + Convert.ToString(ViewState["otp"]) + "^" + Convert.ToString(firstname) + "^"+ "User Registration" +"'";  //FOR MULTI MAIL

           // string VL = "" + Convert.ToInt32(Request.QueryString["pageno"]) + ",1,'" + toAddress + "^" + username + "^" + Convert.ToString(ViewState["Enqueryotp"]) + "^" + Convert.ToString(firstname) + "^" + "User Registration" + "^" + " " + "'";  //FOR MULTI MAIL
            string VL = "" + Convert.ToInt32(Request.QueryString["pageno"]) + ",1,'" + "User Registration" + "^" + toAddress + "^" + username + "^" + Convert.ToString(ViewState["otp"]) + "^" + Convert.ToString(firstname) + "^" + "  " + "'";  //FOR MULTI MAIL

            string retVal = AL.DynamicSPCall_IUD(SP, PR, VL, true, 2);

            //if (retVal == "1")
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "alert('Sending mail started successfully.');", true);
            //else if (retVal == "-99")
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "alert('Sending Failed.');", true);





            if (ResendOTP == 0)
            {
            }
        }
        catch (Exception ex)
        {
        }

    }
    private void clearcontrol()
    {
        txtFullName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        txtEmailid.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        txtHomeMobileNo.Text = string.Empty;
        txtDob.Text = string.Empty;
        txtPassport.Text = string.Empty;
        txtNIC.Text = string.Empty;
        ddlStudyType.SelectedIndex = 0;
        ddlPreference.SelectedValue = null;
        ddlProgramIntrested.SelectedValue = null;
        ddlSource.SelectedIndex = 0;
        rdGender.SelectedValue = null;
        chkConfirm.Checked = false;

    }
    //protected void lbtnregsign_Click(object sender, EventArgs e)
    //{

    //}
    //protected void btnbackerr_Click(object sender, EventArgs e)
    //{

    //}
    //protected void btnOTPSub_Click(object sender, EventArgs e)
    //{

    //}
    //protected void btncanceldomain_Click(object sender, EventArgs e)
    //{

    //}
    //protected void btnbackdomain_Click(object sender, EventArgs e)
    //{

    //}
    //public void SendSMS(string Mobile, string text)
    //{
    //    string status = "";
    //    try
    //    {
    //        string Message = string.Empty;
    //        DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?"));
    //            request.ContentType = "text/xml; charset=utf-8";
    //            request.Method = "POST";

    //            string postDate = "ID=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
    //            postDate += "&";
    //            postDate += "Pwd=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
    //            postDate += "&";
    //            postDate += "PhNo=91" + Mobile;
    //            postDate += "&";
    //            postDate += "Text=" + text;

    //            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
    //            request.ContentType = "application/x-www-form-urlencoded";

    //            request.ContentLength = byteArray.Length;
    //            Stream dataStream = request.GetRequestStream();
    //            dataStream.Write(byteArray, 0, byteArray.Length);
    //            dataStream.Close();
    //            WebResponse _webresponse = request.GetResponse();
    //            dataStream = _webresponse.GetResponseStream();
    //            StreamReader reader = new StreamReader(dataStream);
    //            status = reader.ReadToEnd();
    //        }
    //        else
    //        {
    //            status = "0";
    //        }
    //    }
    //    catch
    //    {

    //    }
    //}

    //static async Task Execute(string message, string toSendAddress, string Subject, string firstname, string username, string nMailbody)
    //{
    //    try
    //    {
    //        Common objCommon = new Common();


    //        DataSet dsconfig = null;
    //        dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "COMPANY_EMAILSVCID,SENDGRID_USERNAME,SENDGRID_PWD,API_KEY_SENDGRID", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

    //        //MasterSoft
    //        //var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["FCGRIDEMAILID"].ToString(), "HSNCU ADMISSION");
    //        var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "SLIIT ADMISSION");
    //        var toAddress = new MailAddress(toSendAddress, "");
    //        //var apiKey = dsconfig.Tables[0].Rows[0]["CLIENT_API_KEY"].ToString();  //"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";
    //        var apiKey = dsconfig.Tables[0].Rows[0]["API_KEY_SENDGRID"].ToString();
    //        var client = new SendGridClient(apiKey.ToString());
    //        //var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["FCGRIDEMAILID"].ToString(), "HSNCU ADMISSION");
    //        var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "SLIIT ADMISSION");

    //        //Client 
    //        //var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["FCGRIDEMAILID"].ToString(), "HSNCU ADMISSION");
    //        //var toAddress = new MailAddress(toSendAddress, "");
    //        //var apiKey = dsconfig.Tables[0].Rows[0]["CLIENT_API_KEY"].ToString();  //"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";
    //        //var client = new SendGridClient(apiKey.ToString());
    //        //var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["FCGRIDEMAILID"].ToString(), "HSNCU ADMISSION");

    //        var subject = Subject;// "Your OTP for Certificate Registration.";
    //        var to = new EmailAddress(toSendAddress, "");
    //        var plainTextContent = "";
    //        var htmlContent = nMailbody;

    //        //string AttcPath = System.Web.HttpContext.Current.Server.MapPath("~/TempDocument/" + filename + "");
    //        //var bytes = File.ReadAllBytes(AttcPath);
    //        //var file = Convert.ToBase64String(bytes);
    //        MailMessage msg = new MailMessage();
    //        SmtpClient smtp = new SmtpClient();
    //        var msgs = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
    //        //msgs.AddAttachment("" + filename + "", file);
    //        var response = await client.SendEmailAsync(msgs);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    //private string GeneartePassword()
    //{
    //    string allowedChars = "";
    //    //   allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
    //    //    allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
    //    allowedChars += "1,2,3,4,5,6,7,8,9,0"; //,!,@,#,$,%,&,?
    //    //--------------------------------------
    //    char[] sep = { ',' };

    //    string[] arr = allowedChars.Split(sep);

    //    string passwordString = "";

    //    string temp = "";

    //    Random rand = new Random();

    //    for (int i = 0; i < 6; i++)
    //    {
    //        temp = arr[rand.Next(0, arr.Length)];
    //        passwordString += temp;
    //    }
    //    return passwordString;
    //}
    //public void TransferToEmail()
    //{
    //    try
    //    {
    //        DataSet d = new DataSet();
    //        string pwd = GeneartePassword();
    //        if (ResendOTP == 0)
    //        {
    //            d = objCommon.FillDropDown("ACD_USER_REGISTRATION WITH(NOLOCK)", "DISTINCT USERNAME,USER_PASSWORD,FIRSTNAME", "USERNO,MOBILENO", "USERNO >=1 AND USER_PASSWORD IS NULL AND MOBILENO =" + txtMobileNo.Text.Trim() + " AND USERNO=" + Convert.ToInt32(ViewState["userno"]), string.Empty);
    //        }
    //        else
    //        {

    //            d = objCommon.FillDropDown("ACD_USER_REGISTRATION WITH(NOLOCK)", "DISTINCT USERNAME,USER_PASSWORD,FIRSTNAME", "USERNO,MOBILENO", "USERNO >=1 AND USER_PASSWORD IS NULL AND MOBILENO =" + txtMobileNo.Text.Trim() + " AND USERNO=" + Convert.ToInt32(ViewState["userno"]), string.Empty);

    //        }

    //        DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "EMAILSVCID,EMAILSVCPWD", "", "");
    //        string password = pwd;
    //        ViewState["otp"] = password;
    //        string username = (d.Tables[0].Rows[0]["USERNAME"].ToString());

    //        Session["UserName"] = username.ToString();
    //        string firstname = (d.Tables[0].Rows[0]["FIRSTNAME"].ToString());


    //        string MOBILENO = (d.Tables[0].Rows[0]["MOBILENO"].ToString());


    //        var toAddress = txtEmailid.Text.Trim().ToString();

    //        if (ResendOTP == 0)
    //        {
    //            string Subject = "NEW CREDENTIAL FOR PRE-ADMISSION PROCESS OF THE SLIIT COLLEGE";

    //            const string EmailTemplate = "<!DOCTYPE html><html><head><meta charset='utf-8'><meta name='viewport' content='width=device-width, initial-scale=1'><link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css' /><link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css' />" +
    //                "<style>.email-template .email-header{background-image:linear-gradient(-60deg,rgba(22,160,133,.6) 0,rgba(244,208,63,.8) 100%);text-align:center}.email-body{padding:30px 15px 0 15px;border:1px solid #ccc}.email-body p{font-size:16px;font-weight:500}ul.simply-list{list-style-type:none;padding:0;margin:0}ul.simply-list .list-group-item{padding:2px 15px;font-weight:600;font-size:16px;line-height:1.4;border:none;background-color:transparent}ul.simply-list.no-border .list-group-item{border:none}.strip{padding:20px 15px;background-color:#eee;margin:15px 0}.strip ul.simply-list li:first-child{padding-left:0}.d-flex{display:flex;-webkit-display:flex;-moz-display:flex;-o-display:flex;flex-direction:row}.d-flex.align-items-center{align-items:center}.note{color:red;font-weight:500;font-size:12px;margin-bottom:10px}.email-template .email-header img{width:85px;display:block;margin:auto}.email-template .email-header span{font-size:22px;font-weight:500;padding:10px 0}.email-info{margin-top:10px;padding:10px 0}.info-list span{font-size:14px;display:flex;align-items:center;font-weight:600;color:#666}.info-list span a{color:#666}.info-list span i{font-size:18px;margin-right:5px;color:#888}@media only screen and (max-width:767px){.email-template .email-header{padding:10px 10px;text-align:center}.email-template .email-header img{width:70px;display:block;margin:auto;margin-bottom:5px}.email-template .email-header span{font-size:22px;font-weight:500}.strip ul.simply-list li{padding-left:0}.info-list li{line-height:1.9}}</style>" +
    //               "<style>.form-group{margin-bottom:15px}.btn-success{color:#fff;background-color:#5cb85c;border-color:#4cae4c}.btn-success.focus,.btn-success:focus{color:#fff;background-color:#449d44;border-color:#255625}.btn-success:hover{color:#fff;background-color:#449d44;border-color:#398439}.btn-success.active,.btn-success:active,.open>.dropdown-toggle.btn-success{color:#fff;background-color:#449d44;background-image:none;border-color:#398439}.btn-success.active.focus,.btn-success.active:focus,.btn-success.active:hover,.btn-success:active.focus,.btn-success:active:focus,.btn-success:active:hover,.open>.dropdown-toggle.btn-success.focus,.open>.dropdown-toggle.btn-success:focus,.open>.dropdown-toggle.btn-success:hover{color:#fff;background-color:#398439;border-color:#255625}.btn-success.disabled.focus,.btn-success.disabled:focus,.btn-success.disabled:hover,.btn-success[disabled].focus,.btn-success[disabled]:focus,.btn-success[disabled]:hover,fieldset[disabled] .btn-success.focus,fieldset[disabled] .btn-success:focus,fieldset[disabled] .btn-success:hover{background-color:#5cb85c;border-color:#4cae4c}.text-success{color:#3c763d}a.text-success:focus,a.text-success:hover{color:#2b542c}.btn{margin:.2em .1em;font-weight:500;font-size:1em;-webkit-font-smoothing:antialiased;-moz-osx-font-smoothing:grayscale;border:none;border-bottom:.15em solid #000;border-radius:3px;padding:.65em 1.3em}.btn-primary{border-color:#2a6496;background-image:linear-gradient(#428bca,#357ebd)}.btn-primary:hover{background:linear-gradient(#357ebd,#3071a9)}</style>" +

    //                                     "<div class='container-fluid' style='padding-top:15px'>" +
    //                                     "<div class='row'>" +
    //                                     "<div class='col-sm-8 col-md-6 col-md-offset-3 col-sm-offset-2'>" +
    //                                     "<div class='email-template'>" +
    //                                     "<div class='email-header'>" +

    //                                     "<span>SLIIT</span>" +
    //                                     "</div>" +
    //                                     "<div class='email-body' style='clear-both'>#content</div>" +


    //            "</div></div></div></div>" +
    //            "</body></html>";


    //            string message = "<h3>Dear <span></span>" + firstname + ", </h3>"
    //           + "<h4 class='text-success'>Registration Successfully Completed.</h4>"
    //           + "<div class='strip'><p>Your login credentials for Online Admission are :</p>"
    //           + "<ul class='list-group list-inline simply-list no-border'>"
    //           + "<li class='list-group-item'>Application ID : <span class='text-success'>"
    //           + username + "</span></li>"
    //           + "<li class='list-group-item'>One Time Password (OTP) : <span class='text-success'>"
    //           + password + "</span></li></div>" +
    //           "<p class='note' style='margin-bottom:20px'>Note: This is system generated email. Please do not reply to this email.</p>"
    //           + "<div style='clear:both;display:block; margin-bottom:15px'><a class='btn btn-success' href='http://SLIIT.edu.in/' class='btn btn-success'>Click Here to Login</a><div>";


    //            string Mailbody = message.ToString();
    //            string nMailbody = EmailTemplate.Replace("#content", Mailbody);
    //            Execute(message, toAddress, Subject, firstname, username, nMailbody).Wait();

    //        }
    //        else
    //        {
    //            string Subject = "NEW CREDENTIAL FOR PRE-ADMISSION PROCESS OF THE SLIIT COLLEGE";

    //            const string EmailTemplate = "<!DOCTYPE html><html><head><meta charset='utf-8'><meta name='viewport' content='width=device-width, initial-scale=1'><link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css' /><link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css' />" +
    //                "<style>.email-template .email-header{background-image:linear-gradient(-60deg,rgba(22,160,133,.6) 0,rgba(244,208,63,.8) 100%);text-align:center}.email-body{padding:30px 15px 0 15px;border:1px solid #ccc}.email-body p{font-size:16px;font-weight:500}ul.simply-list{list-style-type:none;padding:0;margin:0}ul.simply-list .list-group-item{padding:2px 15px;font-weight:600;font-size:16px;line-height:1.4;border:none;background-color:transparent}ul.simply-list.no-border .list-group-item{border:none}.strip{padding:20px 15px;background-color:#eee;margin:15px 0}.strip ul.simply-list li:first-child{padding-left:0}.d-flex{display:flex;-webkit-display:flex;-moz-display:flex;-o-display:flex;flex-direction:row}.d-flex.align-items-center{align-items:center}.note{color:red;font-weight:500;font-size:12px;margin-bottom:10px}.email-template .email-header img{width:85px;display:block;margin:auto}.email-template .email-header span{font-size:22px;font-weight:500;padding:10px 0}.email-info{margin-top:10px;padding:10px 0}.info-list span{font-size:14px;display:flex;align-items:center;font-weight:600;color:#666}.info-list span a{color:#666}.info-list span i{font-size:18px;margin-right:5px;color:#888}@media only screen and (max-width:767px){.email-template .email-header{padding:10px 10px;text-align:center}.email-template .email-header img{width:70px;display:block;margin:auto;margin-bottom:5px}.email-template .email-header span{font-size:22px;font-weight:500}.strip ul.simply-list li{padding-left:0}.info-list li{line-height:1.9}}</style>" +
    //                "<style>.form-group{margin-bottom:15px}.btn-success{color:#fff;background-color:#5cb85c;border-color:#4cae4c}.btn-success.focus,.btn-success:focus{color:#fff;background-color:#449d44;border-color:#255625}.btn-success:hover{color:#fff;background-color:#449d44;border-color:#398439}.btn-success.active,.btn-success:active,.open>.dropdown-toggle.btn-success{color:#fff;background-color:#449d44;background-image:none;border-color:#398439}.btn-success.active.focus,.btn-success.active:focus,.btn-success.active:hover,.btn-success:active.focus,.btn-success:active:focus,.btn-success:active:hover,.open>.dropdown-toggle.btn-success.focus,.open>.dropdown-toggle.btn-success:focus,.open>.dropdown-toggle.btn-success:hover{color:#fff;background-color:#398439;border-color:#255625}.btn-success.disabled.focus,.btn-success.disabled:focus,.btn-success.disabled:hover,.btn-success[disabled].focus,.btn-success[disabled]:focus,.btn-success[disabled]:hover,fieldset[disabled] .btn-success.focus,fieldset[disabled] .btn-success:focus,fieldset[disabled] .btn-success:hover{background-color:#5cb85c;border-color:#4cae4c}.text-success{color:#3c763d}a.text-success:focus,a.text-success:hover{color:#2b542c}.btn{margin:.2em .1em;font-weight:500;font-size:1em;-webkit-font-smoothing:antialiased;-moz-osx-font-smoothing:grayscale;border:none;border-bottom:.15em solid #000;border-radius:3px;padding:.65em 1.3em}.btn-primary{border-color:#2a6496;background-image:linear-gradient(#428bca,#357ebd)}.btn-primary:hover{background:linear-gradient(#357ebd,#3071a9)}</style>" +



    //                                     "<div class='container-fluid' style='padding-top:15px'>" +
    //                                     "<div class='row'>" +
    //                                     "<div class='col-sm-8 col-md-6 col-md-offset-3 col-sm-offset-2'>" +
    //                                     "<div class='email-template'>" +
    //                                     "<div class='email-header'>" +

    //                                     "<span>SLIIT</span>" +
    //                                     "</div>" +
    //                                     "<div class='email-body' style='clear:both'>#content</div>" +

    //            "</div></div></div></div>" +
    //            "</body></html>";
    //            string message = "<h3>Dear <span></span>" + firstname + ", </h3>"
    //          + "<h4 class='text-success'>Registration Successfully Completed.</h4>"
    //          + "<div class='strip'><p>Your login credentials for Online Admission are :</p>"
    //          + "<ul class='list-group list-inline simply-list no-border'>"
    //          + "<li class='list-group-item'>Application ID : <span class='text-success'>"
    //          + username + "</span></li>"
    //          + "<li class='list-group-item'>One Time Password (OTP) : <span class='text-success'>"
    //          + password + "</span></li></div>" +
    //          "<p class='note' style='margin-bottom:20px'>Note: This is system generated email. Please do not reply to this email.</p>"
    //          + "<div style='clear:both;display:block; margin-bottom:15px'><a class='btn btn-success' href='http://SLIIT.edu.in/' class='btn btn-success'>Click Here to Login</a><div>";


    //            string Mailbody = message.ToString();
    //            string nMailbody = EmailTemplate.Replace("#content", Mailbody);
    //            Execute(message, toAddress, Subject, firstname, username, nMailbody).Wait();

    //        }         
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;

    //    }
    //}
    /// enquiry tab code ended by aashna 30-10-2021
    //protected void ddlAdmbatch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //this.BindListView();
    //    divstudent.Visible = true;
    //}

    ///////////////////////////////////////////////////// Application Details ////////////////////////////////////////////////////////
        private void BindData()
    {
        try
        {
            if (ViewState["STUDENT_USERNAME"] != null)
            {
                DataSet ds = objSC.getAppicantData(Convert.ToString(ViewState["STUDENT_USERNO"]));
                // ds = objSC.getstudentid(Convert.ToString();
                // getAppicantData
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblName.Text = ds.Tables[0].Rows[0]["FIRSTNAME"].ToString();
                    lblEmailId.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                    lblPhone.Text = ds.Tables[0].Rows[0]["MOBILENO"].ToString();
                    lblNICApp.Text = ds.Tables[0].Rows[0]["NIC"].ToString();
                    lblPassportApp.Text = ds.Tables[0].Rows[0]["PASSPORTNO"].ToString();
                    lblApplAddress.Text = ds.Tables[0].Rows[0]["PADDRESS"].ToString();
                    //ddlDept.DataSource = ds;
                    //ddlDept.DataValueField = ds.Tables[0].Columns[0].ToString();
                    //ddlDept.DataTextField = ds.Tables[0].Columns[1].ToString();
                    //ddlDept.DataBind();
                    lvDetails.DataSource = ds;
                    lvDetails.DataBind();

                    objCommon.FillDropDownList(ddlLeadStatus, "ACD_LEAD_STAGE", "LEADNO", "LEAD_STAGE_NAME", "LEADNO > 0", "LEADNO");//DEPTCODE
                }
                else
                {
                    lblName.Text = "";
                    lblEmailId.Text = "";
                    lblPhone.Text = "";
                    lblNICApp.Text = "";
                    lblPassportApp.Text = "";
                    lvDetails.DataSource = null;
                    lvDetails.DataBind();
                    //ddlDept.Items.Clear();
                    //ddlDept.Items.Add(new ListItem("Please Select", "0"));
                }
                ds.Dispose();
                //ddlDept.Focus();
            }
            else
            {
                //objCommon.DisplayMessage(this.UpdatePanel1, "Appicant data not found", this.Page);
            }
           
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

        public void ClearControls()
        {
            txt_Remark.Text = string.Empty;
            ddlLeadStatus.SelectedIndex = 0;
            txtEndDate.Text = string.Empty;
        }
    //public void ClearControls()
    //{
    //    txt_Remark.Text = string.Empty;
    //    ddlLeadStatus.SelectedIndex = 0;
    //}

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    protected void btn_SubmitModal_Click(object sender, EventArgs e)
    {
            MappingController objmp = new MappingController();
        //ViewState["action"] = "Edit";
        if(ddlLeadStatus.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.UpdatePanel1, "Please select Lead Status", this.Page);
            return;
        }
        if(txt_Remark.Text == "")
        {
            objCommon.DisplayMessage(this.UpdatePanel1, "Please Enter Remarks", this.Page);
            return;
        }

        int enqueryNo =0,action=0;
        string Action =Convert.ToString(ViewState["action"]);
        if (Action == "Edit")
        {
            enqueryNo = Convert.ToInt32(hdnEnqueryno.Value);
            action = 2;
        }
        else
        {
            enqueryNo = 0;
            action = 1;
        }
        
       // String uano = (objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME="+(ViewState["STUDENT_USERNO"])+""));
        //int ck1 = objmp.AddLeadStatus(Convert.ToInt32(ddlLeadStatus.SelectedValue),(ViewState["STUDENT_USERNO"].ToString()), Convert.ToInt32((Session["userno"]).ToString()),txt_Remark.Text,enqueryNo, action);
        int ck1 = objmp.AddLeadStatus(Convert.ToInt32(ddlLeadStatus.SelectedValue), (ViewState["STUDENT_USERNO"].ToString()), Convert.ToInt32((Session["userno"]).ToString()), txt_Remark.Text, enqueryNo, action, (Convert.ToString(txtEndDate.Text)));
        if (ck1 == 1)
        {
            objCommon.DisplayMessage(this.UpdatePanel1, "Record Saved Successfully", this.Page);
            
             //BindListView();
            BindListViewApplication();
             ClearControls();
             ViewState["action"] = "Add";
            //this.bindColDept();Session["userno"]
            //Clear();
           // return;
             
        }
        if (ck1 == 2)
        {
            objCommon.DisplayMessage(this.UpdatePanel1, "Record Updated Successfully", this.Page);
            //BindListView();
            BindListViewApplication();
            ClearControls();
            ViewState["action"] = "Add";
            //this.bindColDept();Session["userno"]
            //Clear();
            // return;
        }
        else
        {
            objCommon.DisplayMessage(this.UpdatePanel1, "Few or All Record already Exist", this.Page);
            //BindListView();
            ClearControls();
            ViewState["action"] = "Add";
            //this.bindColDept();
            //Clear();
          //  return;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        ViewState["action"] = "Edit";
        int enqueryno = int.Parse(btnEdit.CommandArgument);
        hdnEnqueryno.Value = Convert.ToString(enqueryno);
        DataSet leadstagedata = objCommon.FillDropDown("ACD_LEAD_STUDENT_ENQUIRY_FOLLOWUP EF INNER JOIN ACD_LEAD_STAGE LS ON (EF.ENQUIRYSTATUS=LS.LEADNO)", "ENQUIRYNO", "ENQUIRYNO,convert(varchar, NEXTFOLLOUP_DATE, 3)AS NEXTDATE,LEAD_UA_NO,ENQUIRYSTATUS,LEAD_STAGE_NAME,ENQUIRYSTATUS_DATE,REMARKS", "ENQUIRYNO=" + enqueryno + "", "");
        if (leadstagedata.Tables[0].Rows.Count > 0)
        {
            lblLeadStatusName.Text = leadstagedata.Tables[0].Rows[0]["LEAD_STAGE_NAME"].ToString();
            ddlLeadStatus.SelectedValue = leadstagedata.Tables[0].Rows[0]["ENQUIRYSTATUS"].ToString();
            txt_Remark.Text = leadstagedata.Tables[0].Rows[0]["REMARKS"].ToString();
            txtEndDate.Text = leadstagedata.Tables[0].Rows[0]["NEXTDATE"].ToString();
        }
        else
        {

        }
    }
  //protected void btnEdit_Click(object sender, EventArgs e)
  //  {
  //      ImageButton btnEdit = sender as ImageButton;
  //      ViewState["action"] = "Edit";
  //      int enqueryno = int.Parse(btnEdit.CommandArgument);
  //      hdnEnqueryno.Value =Convert.ToString(enqueryno);
  //      DataSet leadstagedata = objCommon.FillDropDown("ACD_LEAD_STUDENT_ENQUIRY_FOLLOWUP EF INNER JOIN ACD_LEAD_STAGE LS ON (EF.ENQUIRYSTATUS=LS.LEADNO)","ENQUIRYNO", "ENQUIRYNO,LEAD_UA_NO,ENQUIRYSTATUS,LEAD_STAGE_NAME,ENQUIRYSTATUS_DATE,REMARKS", "ENQUIRYNO=" + enqueryno + "", "");
  //      if (leadstagedata.Tables[0].Rows.Count > 0)
  //      {
  //          lblLeadStatusName.Text = leadstagedata.Tables[0].Rows[0]["LEAD_STAGE_NAME"].ToString();
  //          ddlLeadStatus.SelectedValue = leadstagedata.Tables[0].Rows[0]["ENQUIRYSTATUS"].ToString();
  //          txt_Remark.Text = leadstagedata.Tables[0].Rows[0]["REMARKS"].ToString();
  //      }
  //      else
  //      {
          
  //      }
  //  }
   
    private void BindListViewApplication()
    {
        try
        {
                MappingController objmp = new MappingController();
           // String uano = (objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME=" + Convert.ToString(ViewState["STUDENT_USERNO"]) + ""));
            DataSet ds = objmp.GetLeadFolloup(Convert.ToInt32(ViewState["STUDENT_USERNO"]));
           
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables != null)
            {
                lblLeadStatusName.Text = ds.Tables[0].Rows[0]["MAX_LEAD_STAGE_NAME"].ToString();
                lblApplicationStageState.Text = ds.Tables[0].Rows[0]["APP_STATUS"].ToString();
                lblApplAddress.Text = ds.Tables[0].Rows[0]["PADDRESS"].ToString();
                lvlist.DataSource = ds;
                lvlist.DataBind();
                lvLeadList.DataSource = ds;
                lvLeadList.DataBind();
               
            }
            else
            {
                lblLeadStatusName.Text = "";
                lblApplicationStageState.Text = "";
                lblApplAddress.Text = "";
                lvlist.DataSource = null;
                lvlist.DataBind();
                lvLeadList.DataSource = null;
                lvLeadList.DataBind();
               
            }
            ds.Dispose();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_dePT.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlColg.SelectedIndex > 0)
            //{
            //    objCommon.FillDropDownList(ddlDeptMultiCheck, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEPT CD ON (CD.DEPTNO=D.DEPTNO)", "D.DEPTNO",                           "D.DEPTNAME", "D.DEPTNO>0 AND COLLEGE_ID=" + ddlColg.SelectedValue, "D.DEPTNO");//DEPTCODE
            //}
            this.bindColDept();
        }
        catch { }
    }
    public void bindColDept()
    {
        //DataSet ds = objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_DEPT a inner join ACD_DEPARTMENT b on(a.DEPTNO=b.DEPTNO) inner join  ACD_COLLEGE_MASTER c on(a.COLLEGE_ID=c.COLLEGE_ID)", "a.COL_DEPT_NO,a.COLLEGE_ID", "b.DEPTNAME,c.COLLEGE_NAME", "a.COLLEGE_ID=" + ddlColg.SelectedValue, "a.COLLEGE_ID");
        DataSet ds = null; // objCommon.FillDropDown("ACD_COLLEGE_DEPT a inner join acd_dePARTMENT b on(a.dePTno=b.dePTno) inner join  ACD_COLLEGE_MASTER c on(a.college_id=c.college_id)", "a.col_dePT_no,a.college_id", "b.DEPTname,ISNULL(COLLEGE_NAME,'')COLLEGE_NAME", "a.college_id=" + ddlColg.SelectedValue, "a.college_id");
        //lvlist.DataSource = null;
        //lvlist.DataBind();

        if (ds.Tables[0].Rows.Count > 0)
        {
            //lvlist.DataSource = ds;
            //lvlist.DataBind();
        }
        else
        {
            //lvlist.DataSource = null;
            //lvlist.DataBind();
        }
    }



    protected void lkAnnouncementApplication_Click(object sender, EventArgs e)
    {

        //ddlAdmissionBatch.SelectedIndex = 0;
        //ddlDegree.SelectedIndex = 0;
        divEmoji.Visible = false;
       // divReports.Visible = false;
        divAnnounce.Visible = true;
        DivLeadStatus.Visible = true;
        divlkFeed.Attributes.Remove("class");

       // divlkReports.Attributes.Remove("class");
        divlkApplication.Attributes.Add("class", "active");
        BindListViewApplication();
        BindData();
    }
    protected void lkFeedback_Click(object sender, EventArgs e)
    {
        //  ddlAdmissionBatch.SelectedIndex = 0;
        // ddlDegree.SelectedIndex = 0;
        //  BindRadioListStatus();
        divAnnounce.Visible = false;
        DivLeadStatus.Visible = false;
        //  divReports.Visible = false;DivLeadStatus
        divEmoji.Visible = true;
        divlkApplication.Attributes.Remove("class");
        //   divlkReports.Attributes.Remove("class");
        divlkFeed.Attributes.Add("class", "active");
        lvlist.DataSource = null;
        lvlist.DataBind();

    }

    protected void lnkRemark_Click(object sender, EventArgs e)
    {
        ddlAdmissionBatch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlFormCategory.SelectedIndex = 0;
        ddlRemarkAdmBatch.SelectedIndex = 0;

        divAnnounce.Visible = false;
        divQuery.Visible = false;
        divReports.Visible = false;
        divEnquiry.Visible = false;
        divRemark.Visible = true;

        divlkEnquiry.Attributes.Remove("class");
        divlkAnnouncement.Attributes.Remove("class");
        divlkQuery.Attributes.Remove("class");
        divlkReports.Attributes.Remove("class");
        DivlkRemark.Attributes.Add("class", "active");


        divdate.Visible = false;

        divstudent.Visible = false;
        divAllDemands.Visible = false;
        lvLeadDetails.DataSource = null;
        lvLeadDetails.DataBind();
        DivMAinPanel.Visible = false;

        lvStudentQuery.DataSource = null;
        lvStudentQuery.DataBind();
        lvStudentQuery.Visible = false;


        lvRemarkList.DataSource = null;
        lvRemarkList.DataBind();

    }
    protected void ddlRemarkAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRemarkAdmBatch.SelectedIndex > 0)
        {
            MappingController objmp = new MappingController();
            DataSet ds = objmp.GetLeadFolloupBulkRemark(Convert.ToInt32(ddlRemarkAdmBatch.SelectedValue));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvRemarkList.DataSource = ds;
                lvRemarkList.DataBind();
            }
            else
            {
                lvRemarkList.DataSource = null;
                lvRemarkList.DataBind();
            }
        }
        else
        {
            lvRemarkList.DataSource = null;
            lvRemarkList.DataBind();
        }
    }

    protected void ddlAdmissionBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = null;
        ViewState["DYNAMIC_DATASET"] = null;
        OnlineAdmFeeCollectionController objFees = new OnlineAdmFeeCollectionController();
        ds = objFees.GetUserAllData(Convert.ToInt32(ddlAdmissionBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            lvOnlineAdmissionDetails.DataSource = ds;
            lvOnlineAdmissionDetails.DataBind();
            ViewState["DYNAMIC_DATASET"] = ds.Tables[0];
        }
        else
        {
            lvOnlineAdmissionDetails.DataSource = null;
            lvOnlineAdmissionDetails.DataBind();
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = null;
        ViewState["DYNAMIC_DATASET"] = null;
        OnlineAdmFeeCollectionController objFees = new OnlineAdmFeeCollectionController();
        ds = objFees.GetUserAllData(Convert.ToInt32(ddlAdmissionBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            lvOnlineAdmissionDetails.DataSource = ds;
            lvOnlineAdmissionDetails.DataBind();
            ViewState["DYNAMIC_DATASET"] = ds.Tables[0];
        }
        else
        {
            lvOnlineAdmissionDetails.DataSource = null;
            lvOnlineAdmissionDetails.DataBind();
        }
    }
    protected void btnSendBulkEmail_Click(object sender, EventArgs e)
    {
        try
        {
            string studentIds = string.Empty; string filename = ""; int count = 0;

            string folderPath = Server.MapPath("~/EmailUploadFile/");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            Session["EmailFileAttachemnt"] = fuAttachFile.FileName;
            if (Convert.ToString(Session["EmailFileAttachemnt"]) != string.Empty || Convert.ToString(Session["EmailFileAttachemnt"]) != "")
            {
                fuAttachFile.PostedFile.SaveAs(folderPath + Path.GetFileName(Convert.ToString(Session["EmailFileAttachemnt"])));
            }

            DataSet dsUserContact = null;
            if (rbtodayselect.SelectedValue == "1")
                dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));
            else
                dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(2830));

            if (dsUserContact.Tables[1] != null && dsUserContact.Tables[1].Rows.Count > 0)
            {
                foreach (ListViewDataItem item in lvLeadDetails.Items)
                {
                    CheckBox chk = item.FindControl("chkCheck") as CheckBox;
                    LinkButton hdfAppli = item.FindControl("lnkApplicationNo") as LinkButton;
                    Label hdfEmailid = item.FindControl("lblEmailId") as Label;
                    Label hdfirstname = item.FindControl("lblFirstname") as Label;

                    if (chk.Checked == true)
                    {
                        count++;
                        string message = "";
                        if (rbtodayselect.SelectedValue == "1")
                        {
                            message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                            //  const string EmailTemplate = "<!DOCTYPE html><html><head><meta charset='utf-8'><meta name='viewport' content='width=device-width, initial-scale=1'><link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css' /><link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css' />" +
                            //     "<style>.email-template .email-header{background-color:#346CB0;text-align:center}.email-body{padding:30px 15px 0 15px;border:1px solid #ccc}.email-body p{font-size:16px;font-weight:500}ul.simply-list{list-style-type:none;padding:0;margin:0}ul.simply-list .list-group-item{padding:2px 15px;font-weight:600;font-size:16px;line-height:1.4;border:none;background-color:transparent}ul.simply-list.no-border .list-group-item{border:none}.strip{padding:20px 15px;background-color:#eee;margin:15px 0}.strip ul.simply-list li:first-child{padding-left:0}.d-flex{display:flex;-webkit-display:flex;-moz-display:flex;-o-display:flex;flex-direction:row}.d-flex.align-items-center{align-items:center}.note{color:red;font-weight:500;font-size:12px;margin-bottom:10px}.email-template .email-header img{width:85px;display:block;margin:auto}.email-template .email-header span{font-size:22px;font-weight:500;padding:10px 0}.email-info{margin-top:10px;padding:10px 0}.info-list span{font-size:14px;display:flex;align-items:center;font-weight:600;color:#666}.info-list span a{color:#666}.info-list span i{font-size:18px;margin-right:5px;color:#888}@media only screen and (max-width:767px){.email-template .email-header{padding:10px 10px;text-align:center}.email-template .email-header img{width:70px;display:block;margin:auto;margin-bottom:5px}.email-template .email-header span{font-size:22px;font-weight:500}.strip ul.simply-list li{padding-left:0}.info-list li{line-height:1.9}}</style>" +
                            //    "<style>.form-group{margin-bottom:15px}.btn-success{color:#fff;background-color:#5cb85c;border-color:#4cae4c}.btn-success.focus,.btn-success:focus{color:#fff;background-color:#449d44;border-color:#255625}.btn-success:hover{color:#fff;background-color:#449d44;border-color:#398439}.btn-success.active,.btn-success:active,.open>.dropdown-toggle.btn-success{color:#fff;background-color:#449d44;background-image:none;border-color:#398439}.btn-success.active.focus,.btn-success.active:focus,.btn-success.active:hover,.btn-success:active.focus,.btn-success:active:focus,.btn-success:active:hover,.open>.dropdown-toggle.btn-success.focus,.open>.dropdown-toggle.btn-success:focus,.open>.dropdown-toggle.btn-success:hover{color:#fff;background-color:#398439;border-color:#255625}.btn-success.disabled.focus,.btn-success.disabled:focus,.btn-success.disabled:hover,.btn-success[disabled].focus,.btn-success[disabled]:focus,.btn-success[disabled]:hover,fieldset[disabled] .btn-success.focus,fieldset[disabled] .btn-success:focus,fieldset[disabled] .btn-success:hover{background-color:#5cb85c;border-color:#4cae4c}.text-success{color:#3c763d}a.text-success:focus,a.text-success:hover{color:#2b542c}.btn{margin:.2em .1em;font-weight:500;font-size:1em;-webkit-font-smoothing:antialiased;-moz-osx-font-smoothing:grayscale;border:none;border-bottom:.15em solid #000;border-radius:3px;padding:.65em 1.3em}.btn-primary{border-color:#2a6496;background-image:linear-gradient(#428bca,#357ebd)}.btn-primary:hover{background:linear-gradient(#357ebd,#3071a9)}</style>" +

                            //                          "<div class='container-fluid' style='padding-top:15px'>" +
                            //                          "<div class='row'>" +
                            //                          "<div class='col-sm-8 col-md-6 col-md-offset-3 col-sm-offset-2'>" +
                            //                          "<div class='email-template'>" +
                            //                          "<div class='email-header'>" +
                            //                         //"<img alt='logo' src='http://erptest.sliit.lk/IMAGES/SLIIT_logo.png' />" +
                            //                          "<span style='color:#fff;font-weight:bold'>Sri Lanka Institute of Information Technology</span>" +
                            //                          "</div>" +
                            //                          "<div class='email-body' style='clear-both'>#content</div>" +


                            // "</div></div></div></div>" +
                            // "</body></html>";

                            // string message = "<h3>Dear <span></span>" + hdfirstname.Text + ", </h3>"
                            //+ "<div class='strip'><p>" + txtEmailMessage.Text + "</p>"
                            //+ "</div>" +
                            //"<p class='note' style='margin-bottom:20px'>Note: This is system generated email. Please do not reply to this email.</p>";


                            // string Mailbody = message.ToString();
                            // string nMailbody = EmailTemplate.Replace("#content", Mailbody);

                            filename = Convert.ToString(Session["EmailFileAttachemnt"]);
                            Execute(message, hdfEmailid.Text, txtEmailSubject.Text, txtEmailMessage.Text, hdfirstname.Text, hdfAppli.Text, filename, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"]));
                        }
                        else
                        {
                            if (rbtodaytemplate.SelectedValue == "1")
                            {
                                message = dsUserContact.Tables[1].Rows[1]["TEMPLATE"].ToString();
                            }
                            else if (rbtodaytemplate.SelectedValue == "2")
                            {
                                message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                            }

                            Execute(message, hdfEmailid.Text, txtEmailSubject.Text, txtEmailMessage.Text, hdfirstname.Text, hdfAppli.Text, filename, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"]));
                        }
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(updEmailSend, "Email Tamplate Not Found!!!", this.Page);
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(updEmailSend, "Please select atleast one check box for email send !!!", this.Page);
                txtEmailMessage.Text = "";
                txtEmailSubject.Text = "";
                Session["EmailFileAttachemnt"] = null;
                return;
            }
            else
            {
                string path = Server.MapPath("~/EmailUploadFile\\" + fuAttachFile.FileName);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not  
                {
                    file.Delete();
                }  
                objCommon.DisplayMessage(updEmailSend, "Email send Successfully !!!", this.Page);
                txtEmailMessage.Text = "";
                txtEmailSubject.Text = "";
                Session["EmailFileAttachemnt"] = null;
            }
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void Execute(string message, string toSendAddress, string Subject,string ManualMesage, string firstname, string username,string filename,string ReffEmail,string ReffPassword)
    {
        try
        {
            SmtpMail oMail = new SmtpMail("TryIt");

            oMail.From = ReffEmail;

            oMail.To = toSendAddress;

            oMail.Subject = Subject;

            oMail.HtmlBody = message;
            if (rbtodayselect.SelectedValue == "1")
            {
                oMail.HtmlBody = oMail.HtmlBody.Replace("[USERFIRSTNAME]", firstname.ToString());
                oMail.HtmlBody = oMail.HtmlBody.Replace("[MESSAGE]", ManualMesage.ToString());
            }
            else
            {
                oMail.HtmlBody = oMail.HtmlBody.Replace("[UA_FULLNAME]", firstname.ToString());
            }
            if (filename != string.Empty)
            {
                oMail.AddAttachment(System.Web.HttpContext.Current.Server.MapPath("~/EmailUploadFile/" + filename + ""));
            }
           // SmtpServer oServer = new SmtpServer("smtp.live.com");
            SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022

            oServer.User = ReffEmail;
            oServer.Password = ReffPassword;

            oServer.Port = 587;

            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();



            oSmtp.SendMail(oServer, oMail);
            //Common objCommon = new Common();


            //DataSet dsconfig = null;
            //dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "COMPANY_EMAILSVCID,SENDGRID_USERNAME,SENDGRID_PWD,API_KEY_SENDGRID,SLIIT_EMAIL,SLIIT_EMAIL_PWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            ////MasterSoft
            ////var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["FCGRIDEMAILID"].ToString(), "HSNCU ADMISSION");
            //var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["SLIIT_EMAIL"].ToString(), "SLIIT");
            //var toAddress = new MailAddress(toSendAddress, "");
            ////var apiKey = dsconfig.Tables[0].Rows[0]["CLIENT_API_KEY"].ToString();  //"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";
            //var apiKey = dsconfig.Tables[0].Rows[0]["API_KEY_SENDGRID"].ToString();
            //var client = new SendGridClient(apiKey.ToString());
            ////var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["FCGRIDEMAILID"].ToString(), "HSNCU ADMISSION");
            //var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["SLIIT_EMAIL"].ToString(), "SLIIT");

            ////Client 
            ////var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["FCGRIDEMAILID"].ToString(), "HSNCU ADMISSION");
            ////var toAddress = new MailAddress(toSendAddress, "");
            ////var apiKey = dsconfig.Tables[0].Rows[0]["CLIENT_API_KEY"].ToString();  //"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";
            ////var client = new SendGridClient(apiKey.ToString());
            ////var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["FCGRIDEMAILID"].ToString(), "HSNCU ADMISSION");

            //var subject = Subject;// "Your OTP for Certificate Registration.";
            //var to = new EmailAddress(toSendAddress, "");
            //var plainTextContent = "";
            //var htmlContent = nMailbody;
            //var file = "";
            //if (filename != string.Empty)
            //{
            //    string AttcPath = System.Web.HttpContext.Current.Server.MapPath("~/EmailUploadFile/" + filename + "");
            //    var bytes = File.ReadAllBytes(AttcPath);
            //    file = Convert.ToBase64String(bytes);
            //}
            //MailMessage msg = new MailMessage();
            //SmtpClient smtp = new SmtpClient();
            //var msgs = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //if (filename != string.Empty)
            //{
            //    msgs.AddAttachment("" + filename + "", file);
            //}
            //var response = await client.SendEmailAsync(msgs);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnApplicationForm_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("UserInfoFC.rpt");
        }
        catch (Exception ex)
        {

        }
    }
    private void ShowReport(string rptFileName)
    {
        try
        {
            string colgname = "SLIIT";// ViewState["College_Name"].ToString().Replace(" ", "_");

            string exporttype = "pdf";
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("")));
            url += "Reports/commonreport.aspx?";
            url += "exporttype=" + exporttype;

            url += "&filename=" + colgname + "_" + ViewState["STUDENT_USERNO"].ToString() + "." + exporttype;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_USERNO=" + Convert.ToInt32(ViewState["STUDENT_USERNO"]) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            // url += "&param=@P_USERNO=" + ((UserDetails)(Session["user"])).UserNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Default2.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
  
    protected void rbtodayselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtodayselect.SelectedValue == "1")
        {
            todaymau.Visible = true;
            todayauto.Visible = false;
            btnSendBulkEmail.Visible = true;
        }

        else if (rbtodayselect.SelectedValue == "2")
        {
            todaymau.Visible = false;
            todayauto.Visible = true;
            btnSendBulkEmail.Visible = true;
            txtEmailSubject.Text = "";
            txtEmailMessage.Text = "";
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        this.BindListView();
    }
    protected void OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        (lvLeadDetails.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["DYNAMIC_DATASET"];
        lvLeadDetails.DataSource = dt;
        lvLeadDetails.DataBind();
       //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
        //this.BindListView();
    }
    protected void btnGenerateExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string[] date;
            if (Convert.ToString(hdnDate.Value) == string.Empty || Convert.ToString(hdnDate.Value) == "0")
            {
                date = ",".Split(',');
            }
            else
            {
                date = hdnDate.Value.ToString().Split('-');
                date[0] = Convert.ToDateTime(date[0]).ToString("dd/MM/yyyy");
                date[1] = Convert.ToDateTime(date[1]).ToString("dd/MM/yyyy");
            }
            if (txtVerifyPassword.Text.Trim() == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Password !!!", this.Page);
                return;
            }
            if (txtVerifyPassword.Text.Trim() == Convert.ToString(Session["ExcelDetails"]))
            {

                DataSet ds = null;
                ds = objSC.getstudentid(Convert.ToString(ddlProgressLavel.SelectedItem.Text), "0", Convert.ToInt32(0), 12, Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlugpg.SelectedValue), Convert.ToInt32(ddlDiscipline.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlAptitudeTestCentre.SelectedValue), Convert.ToInt32(ddlAptitudeTestMedium.SelectedValue), date[0], date[1]);
                if (ds.Tables == null || ds.Tables[0].Rows.Count <= 0)
                {
                    objCommon.DisplayMessage(this.Page, "Record Not Found!", this.Page);
                    return;
                }
                else
                {
                    GridView gvStudData = new GridView();
                    gvStudData.DataSource = ds;
                    gvStudData.DataBind();
                    string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
                    string attachment = "attachment; filename=LeadAllotment.xls";
                    // string attachment = "attachment; filename=Applied_Students_Status_Report.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    Response.Write(FinalHead);
                    gvStudData.RenderControl(htw);
                    //string a = sw.ToString().Replace("_", " ");
                    Response.Write(sw.ToString());
                    Response.End();
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Password is not matched, Please enter valid password !!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        { 
        
        }
    }

    // Add BY AAshana on 28-09-2022
    protected void btncmpareexcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmbatch.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.Page, "Please Select Intake !!!", this.Page);
                return;
            }
            if (TxtpassCamparision.Text.Trim() == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Password !!!", this.Page);
                return;
            }
            if (TxtpassCamparision.Text.Trim() == Convert.ToString(Session["ExcelDetails"]))
            {

                string SP_Name2 = "PKG_ACD_GET_LEAD_CAMPARISION_DATA";
                string SP_Parameters2 = "@P_INTAKE,@P_DATE,@P_CAMPUSNO";
                string Call_Values2 = "" + Convert.ToInt32(ddlAdmbatch.SelectedValue.ToString()) + "," +
                "" + "," + Convert.ToInt32(ddlCampus.SelectedValue);
                DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    GridView gvStudData = new GridView();
                    gvStudData.DataSource = ds;
                    gvStudData.DataBind();
                    string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
                    string attachment = "attachment; filename=Lead Camparison Data.xls";
                    // string attachment = "attachment; filename=Applied_Students_Status_Report.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    Response.Write(FinalHead);
                    gvStudData.RenderControl(htw);
                    //string a = sw.ToString().Replace("_", " ");
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "No Record Found !!!", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Password is not matched, Please enter valid password !!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }
   
    protected void lvOnlineAdmissionDetails_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        try
        {
            (lvLeadDetails.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["DYNAMIC_DATASET"];
            lvOnlineAdmissionDetails.DataSource = dt;
            lvOnlineAdmissionDetails.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    protected void FilterData2_TextChanged(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.TextBox searchTextBox = (System.Web.UI.WebControls.TextBox)lvOnlineAdmissionDetails.FindControl("FilterData2");
        string searchText = searchTextBox.Text.Trim();

        try
        {
            System.Data.DataTable dt = ViewState["DYNAMIC_DATASET"] as System.Data.DataTable;
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                if (searchText != string.Empty)
                {
                    string searchedData = "BATCHNAME LIKE '%" + searchText + "%' OR USERNAME LIKE '%" + searchText + "%' OR FIRSTNAME LIKE '%" + searchText + "%' OR LASTNAME LIKE '%" + searchText + "%' OR EMAILID LIKE '%" + searchText + "%' OR APPLY_PROGRAM_DETAILS LIKE '%" + searchText + "%' OR MOBILENO LIKE '%" + searchText + "%'  OR MOBILECODE LIKE '%" + searchText + "%'  OR RECEIPTNO LIKE '%" + searchText + "%' OR SR_YEAR LIKE '%" + searchText + "%' OR UG_YEAR LIKE '%" + searchText + "%' OR UG_TYPE LIKE '%" + searchText + "%'";
                    dv.RowFilter = searchedData;
                    if (dv != null && dv.ToTable().Rows.Count > 0)
                    {
                        lvOnlineAdmissionDetails.DataSource = dv;
                        lvOnlineAdmissionDetails.DataBind();
                    }
                   
                }
                else
                {
                    lvOnlineAdmissionDetails.DataSource = dt;
                    lvOnlineAdmissionDetails.DataBind();
                }
            }
            
        }
        catch (Exception ex)
        {
        }

    }
}