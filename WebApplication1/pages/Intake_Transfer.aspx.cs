using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Web.Configuration;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Security;
using mastersofterp;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

public partial class ACADEMIC_Intake_Transfer : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MappingController objmp = new MappingController(); 
    StudentController objStud = new StudentController();
    Student_Acd obj = new Student_Acd();
    User_AccController objUC = new User_AccController();
    UserAcc objUA = new UserAcc();
    ImportDataMaster objIDM = new ImportDataMaster();
    ImportDataController objIDC = new ImportDataController();

   

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
                    this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                  

                     FilldropDown();
                     //excellist();

                }
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
                //excellist();
                divlkintaketransfer.Attributes.Add("class", "active");
                lvapti.Visible = false;
               
            }
            if (Request.Params["__EVENTTARGET"] != null &&
              Request.Params["__EVENTTARGET"].ToString() != string.Empty)
            {
                if (Request.Params["__EVENTTARGET"].ToString() == "dynamiccall")
                   
                    BindList();
                divlkintaketransfer.Attributes.Remove("class");
                //ScriptManager.RegisterClientScriptBlock(updIntakeAlootment, updIntakeAlootment.GetType(), "Src", "Setdate('" + hdfdate.Value + "');", true);
            }
            if (Request.Params["__EVENTTARGET"] != null &&
              Request.Params["__EVENTTARGET"].ToString() != string.Empty)
            {
                if (Request.Params["__EVENTTARGET"].ToString() == "dynamiccallexam")

                    binddata();

                //divlkintaketransfer.Attributes.Remove("class");
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
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
                Response.Redirect("~/notauthorized.aspx?page=Intake_Transfer.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Intake_Transfer.aspx");
        }
    }
//tab-1 start
    private void FilldropDown()
    {
        objCommon.FillDropDownList(ddltransferintake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVE = 1", "BATCHNO DESC");
        objCommon.FillDropDownList(ddlintakeprep, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVE = 1", "BATCHNO DESC");
        objCommon.FillDropDownList(ddlcurreentintake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVE = 1", "BATCHNO DESC");
        objCommon.FillDropDownList(ddlDiscipline, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "", "UA_SECTION");

        objCommon.FillListBox(ddldes, "ACD_AREA_OF_INTEREST", "AREA_INT_NO", "AREA_INT_NAME", "", "AREA_INT_NO");

        objCommon.FillDropDownList(ddlugpg, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "", "UA_SECTION");
        objCommon.FillDropDownList(ddlintakeAllotment, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVE = 1", "BATCHNO DESC");

        objCommon.FillDropDownList(ddlugpgforexam, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "", "UA_SECTION");
        objCommon.FillDropDownList(ddlcenter, "ACD_APTITUDE_CENTER", "APTITUDE_CENTER_NO", "APTITUDE_CENTER_NAME", "", "APTITUDE_CENTER_NO");

        objCommon.FillDropDownList(ddlDesciplineticket, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "", "UA_SECTION");
        objCommon.FillDropDownList(ddlintaketicket, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVE = 1", "BATCHNO DESC");

        objCommon.FillDropDownList(ddlintakeapti, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVE = 1", "BATCHNO DESC");
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH  B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT A.DEGREENO", "A.DEGREENAME", "A.DEGREENO > 0 and isnull(ACTIVE,0)=1", "A.DEGREENAME");

        objCommon.FillListBox(ddldesrdiofir, "ACD_AREA_OF_INTEREST", "AREA_INT_NO", "AREA_INT_NAME", "", "AREA_INT_NO");

        objCommon.FillDropDownList(ddlfaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_ID");
       
    }

    protected void rdbFilter_SelectedIndexChanged(object sender, EventArgs e)
    {     
        if (rdbFilter.SelectedValue == "1")
        {
            btnDownload.Visible = true;
            btnCancel.Visible = true;
            lvapti.Visible = false;
            btnUpload.Visible = false;
            FileUpload.Visible = false;
            btnMarksPreData.Visible = false;
            //divdegree.Visible = false;
            //divprogram.Visible = true;
            //divfaculty.Visible = true;
            divdesfirst.Visible = true;
            divdesp.Visible = false;
            intake.Visible = true;
            divintaketwo.Visible = false;
            divMultiselectdate.Visible = true;
            divapti.Visible = false;
            lvapti.DataSource = null;
            lvapti.DataBind();

            divuploadexcel.Visible = false;
            lvuploexcel.DataSource = null;
            lvuploexcel.DataBind();

            btncanceltest.Visible = true;
            btnDownload.Visible = true;

            btnMarksPreData.Visible = false;
            btnUpload.Visible = false;
            //btnverify.Visible = false;
            btnconfirmed.Visible = false;
            btnexcelformat.Visible = false;

            //ddldes.SelectedIndex=0;
            //ddldes.Items.Clear();
            ddlintakeprep.SelectedIndex = 0;




        }
        else if (rdbFilter.SelectedValue == "2")
        {
            lvapti.Visible = true;
            btnUpload.Visible = true;
            btnDownload.Visible = false;
            FileUpload.Visible = true;
            btnDownload.Visible = false;
            ddlintakeapti.SelectedIndex = 0;

            //ddldesrdiofir.SelectedIndex = 0;
            //ddldesrdiofir.Items.Clear(); 
            divMultiselectdate.Visible = false;
            ddlDegree.SelectedIndex = 0;
            btnMarksPreData.Visible = true;
           // divdegree.Visible = true;
            //divfaculty.Visible = false;
            //divprogram.Visible = false;
            divdesfirst.Visible = false;
            divdesp.Visible = true;

            divtestprep.Visible = false;
            lvtestprep.DataSource = null;
            lvtestprep.DataBind();

            intake.Visible = false;
            divintaketwo.Visible = true;
            btncanceltest.Visible = true;
            btnDownload.Visible = false;

            btnMarksPreData.Visible = true;
            btnUpload.Visible = true;
            //btnverify.Visible = true;
            btnconfirmed.Visible = true;
            btnexcelformat.Visible = true;
        }

    }

    private void BindListview()
    {
        try
        {
            divlkintaketransfer.Attributes.Add("class", "active");
            string StartEndDate = hdnDate.Value; string program = "";
            string[] dates = new string[] { };
            if ((StartEndDate) == "")
            {
                objCommon.DisplayMessage(this.updexamallotment, "Please select Start Date End Date !", this.Page);
                return;
            }
            else
            {
                StartEndDate = StartEndDate.Substring(0, StartEndDate.Length - 0);
                dates = StartEndDate.Split('-');
            }
            string StartDate = dates[0];
            string EndDate = dates[1];
            DateTime dtStartDate = DateTime.Parse(StartDate);
            string SDate = dtStartDate.ToString("yyyy/MM/dd");
            DateTime dtEndDate = DateTime.Parse(EndDate);
            string EDate = dtEndDate.ToString("yyyy/MM/dd");
            foreach (ListItem items in ddlProgram.Items)
            {
                if (items.Selected == true)
                {
                    program += items.Value.Split(',')[1] + ',';

                }
            }
            if (program.ToString() == string.Empty)
            {
                program = "0";
            }
            else
            {
                program = program.Substring(0, program.Length - 1);
            }
            DataSet ds = objmp.GetIntake_transfer(Convert.ToInt32(ddlcurreentintake.SelectedValue), Convert.ToInt32(ddlDiscipline.SelectedValue),program);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvIntake.DataSource = ds;
                lvIntake.DataBind();
                lvintakelist.Visible = true;
                btnSave.Visible = true;
                btnCancel.Visible = true;
            }
            else
            {
                //objCommon.DisplayMessage(updIntaketransfer, "Data Not Found", this.Page);
                //objCommon.DisplayMessage("Data Not Found", this.Page);
                lvIntake.DataSource = null;
                lvIntake.DataBind();
                lvintakelist.Visible = false;
                btnSave.Visible = false;
                btnCancel.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlcurreentintake_SelectedIndexChanged(object sender, EventArgs e)
    {
        divlkintaketransfer.Attributes.Add("class", "active");
        BindListview();
    }
    protected void ddlDiscipline_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillListBox(ddlProgram, "ACD_COLLEGE_MASTER CM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO) INNER JOIN ACD_BRANCH BA ON (CD.BRANCHNO = BA.BRANCHNO) INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON (CD.AFFILIATED_NO = AU.AFFILIATED_NO)", "(CONVERT(NVARCHAR(5),CD.DEGREENO) + ','+ CONVERT(NVARCHAR(5),CD.BRANCHNO) + ',' + CONVERT(NVARCHAR(5),CD.AFFILIATED_NO))", "(D.DEGREENAME + ' - ' + LONGNAME + ' - ' + AFFILIATED_SHORTNAME)", "CD.UGPGOT=" + ddlDiscipline.SelectedValue, "CD.DEGREENO");

        divlkintaketransfer.Attributes.Add("class", "active");
        BindListview();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        GridView GvStudent = new GridView();
        //string[] program;
        //if (ddlprgm.SelectedValue == "0")
        //{
        //    program = "0,0".Split(',');
        //}
        //else
        //{
        //    program = ddlprgm.SelectedValue.Split(',');
        //}
        string area = ""; string[] getedates; 

        foreach (ListItem item in ddldesrdiofir.Items)
        {
            if (item.Selected == true)
            {
                area += item.Value + ',';
            }
        }
        if (!string.IsNullOrEmpty(area))
        {
            area = area.Substring(0, area.Length - 1);
        }
        else
        {
            area = "0";
        }
        DataSet dsfee = null;
        if (hdfGetFromDate.Value.ToString() == string.Empty)
        {
            getedates = "0,0".Split(',');
            dsfee = objIDC.getTestPrepDataExcel(Convert.ToInt32(ddlintakeapti.SelectedValue), area, Convert.ToString(getedates[0]), Convert.ToString(getedates[1]));
        }
        else
        {
            getedates = hdfGetFromDate.Value.ToString().Split('-');
            dsfee = objIDC.getTestPrepDataExcel(Convert.ToInt32(ddlintakeapti.SelectedValue), area, Convert.ToString(Convert.ToDateTime(getedates[0]).ToString("dd/MM/yyyy")), Convert.ToString(Convert.ToDateTime(getedates[1]).ToString("dd/MM/yyyy")));
        }
        
        if (dsfee.Tables[0].Rows.Count > 0)
        {
            GvStudent.DataSource = dsfee;
            GvStudent.DataBind();
            string attachment = "attachment; filename=TestprepDataExcel.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GvStudent.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
        else
        {
            objCommon.DisplayMessage(updapptitutetest, "Data Not Found", this.Page);
        }
    }


    protected void btnMarksPreData_Click(object sender, EventArgs e)
    {

        if (ddlintakeprep.SelectedIndex <= 0)
        {
            objCommon.DisplayMessage(this.updIntaketransfer, "Please Select Intake!", this.Page);
            return;
        }
        //if (ddldes.SelectedIndex <= 0)
        //{
        //    objCommon.DisplayMessage(this.updIntaketransfer, "Please Select Discipline!", this.Page);
        //    return;
        //}
        //if (ddlDegree.SelectedIndex <= 0)
        //{
        //    objCommon.DisplayMessage(this.updIntaketransfer, "Please Select Degree!", this.Page);
        //    return;
        //}


        GridView GvStudent = new GridView();
        string area = "";

        foreach (ListItem item in ddldes.Items)
        {
            if (item.Selected == true)
            {
                area += item.Value + ',';
            }
        }
        if (!string.IsNullOrEmpty(area))
        {
            area = area.Substring(0, area.Length - 1);
        }
        else
        {
            area = "0";
        }
        DataSet dsfee = objIDC.getApptiDataExcel(Convert.ToInt32(ddlintakeprep.SelectedValue), area);
        if (dsfee.Tables[0].Rows.Count > 0)
        {
            GvStudent.DataSource = dsfee;
            GvStudent.DataBind();
            string attachment = "attachment; filename=TestprepDataExcel.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GvStudent.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
        else
        {
            objCommon.DisplayMessage(updapptitutetest, "Data Not Found", this.Page);
        }
    }

    private void clear()
    {
        ddlDiscipline.SelectedIndex = 0;
        ddlcurreentintake.SelectedIndex = 0;
        ddltransferintake.SelectedIndex = 0;
        ddlProgram.SelectedValue = null;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;      
        lvIntake.DataSource = null;
        lvIntake.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            
            if (ddltransferintake.SelectedIndex <= 0)
            {
                objCommon.DisplayMessage(this.updIntaketransfer, "Please Select Intake!", this.Page);
                return;
            }
         
            int  uano=Convert.ToInt32(Session["userno"]) ;
            int oldintake = Convert.ToInt32(ddlcurreentintake.SelectedValue);
            int newintake = Convert.ToInt32(ddltransferintake.SelectedValue);         
            string userno = "";
            CustomStatus cs = 0;
    
            foreach (ListViewDataItem lvItem in lvIntake.Items)
            {
                CheckBox chkBox = lvItem.FindControl("chktransfer") as CheckBox;
                Label lblusername = lvItem.FindControl("username") as Label;
                //HiddenField HDFIDNO = lvItem.FindControl("hdfidno") as HiddenField;

                if (chkBox.Checked == true)
                {
                    userno += chkBox.ToolTip + ",";
                     cs = (CustomStatus)objmp.AddIntakeTransfer(uano, oldintake, newintake, userno);
                }
                  
             
            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {

                ddlcurreentintake.SelectedIndex = 0;
                ddlDiscipline.SelectedIndex = 0;
                ddlProgram.ClearSelection();
                ddltransferintake.SelectedIndex = 0;

                BindListview();
                objCommon.DisplayMessage(this.updIntaketransfer, "Intake Transfer Successfully!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please Select At least One checkbox", this.Page);
            }
            //if (objmp.AddIntakeTransfer(uano, oldintake, newintake, userno) == Convert.ToInt32(CustomStatus.RecordSaved))
            //{
            //    ddlcurreentintake.SelectedIndex = 0;
            //    ddlDiscipline.SelectedIndex = 0;
            //    ddlProgram.ClearSelection();
            //    ddltransferintake.SelectedIndex = 0;

            //    BindListview();
            //    objCommon.DisplayMessage(this.updIntaketransfer, "Intake Transfer Successfully!", this.Page);
            //}
            //else
            //{
            //    objCommon.DisplayMessage(this.updIntaketransfer, "Error in transfer Intake", this.Page);
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_assignFacultyAdvisor.btnAssignFA0_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lkintaketransfer_Click(object sender, EventArgs e)
    {
        divlkintakeallotment.Attributes.Remove("class");
        divlkexamcenter.Attributes.Remove("class");
        divlkintaketransfer.Attributes.Add("class", "active");
        divlkhallticket.Attributes.Remove("class");
        divlkapptitutetest.Attributes.Remove("class");

        divintakeallotment.Visible = false;
        divintaketransfer.Visible = true;
        divhallticket.Visible = false;
        divexamcenter.Visible = false;
        divapptitutetest.Visible = false;

        lvallotment.DataSource = null;
        lvallotment.DataBind();
        lvexamcenter.DataSource = null;
        lvexamcenter.DataBind();

        lvhallticket.DataSource = null;
        lvhallticket.DataBind();

        divallotment.Visible = false;
        divexam.Visible = false;
        divlvhallticket.Visible = false;

        ddlugpg.SelectedIndex = 0;
        ddlintakeAllotment.SelectedIndex = 0;


        ddlugpgforexam.SelectedIndex = 0;

        ddlintaketicket.SelectedIndex = 0;
        ddlDesciplineticket.SelectedIndex = 0;
    }

    //tab-1 end

    //tab2-start
    protected void lkintakeallotment_Click(object sender, EventArgs e)
    {
        divlkintakeallotment.Attributes.Add("class", "active");
        divlkintaketransfer.Attributes.Remove("class");
        divlkexamcenter.Attributes.Remove("class");
        divlkapptitutetest.Attributes.Remove("class");

        divintakeallotment.Visible = true;
        divintaketransfer.Visible = false;
        divexamcenter.Visible = false;
        divhallticket.Visible = false;
        divapptitutetest.Visible = false;

        lvIntake.DataSource = null;
        lvIntake.DataBind();
        lvexamcenter.DataSource = null;
        lvexamcenter.DataBind();
        lvhallticket.DataSource = null;
        lvhallticket.DataBind();

        lvintakelist.Visible = false;
        divallotment.Visible = false;
        divexam.Visible = false;
        divlvhallticket.Visible = false;

        ddltransferintake.SelectedIndex = 0;
        ddlProgram.SelectedIndex = 0;
        ddlDiscipline.SelectedIndex = 0;
        ddlcurreentintake.SelectedIndex = 0;

        ddlugpgforexam.SelectedIndex = 0;

        ddlintaketicket.SelectedIndex = 0;
        ddlDesciplineticket.SelectedIndex = 0;
    }

   protected void ddlugpg_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindList();
    }
   private void BindList()
   {
       try
       {
           string StartEndDate = hdfdate.Value;
           string StartDate = null;
           string EndDate = null;
           string[] dates = new string[] { };
           if ((StartEndDate) == "")
           {

               //objCommon.DisplayMessage(this.updexamallotment, "Please select Start Date End Date !", this.Page);
               //return;
           }
           else
           {

               StartEndDate = StartEndDate.Substring(0, StartEndDate.Length - 0);
               dates = StartEndDate.Split('-');
                StartDate = dates[0];
                EndDate = dates[1];
           }
           //DataSet ds = null; string[] date;

           //if (Convert.ToString(hdnDate.Value) == string.Empty || Convert.ToString(hdnDate.Value) == "0")
           //{
           //    date = "0,0".Split(',');
           //}
           //else
           //{
           //    date = hdnDate.Value.ToString().Split('-');
           //    date[0] = Convert.ToDateTime(date[0]).ToString("dd/MM/yyyy");
           //    date[1] = Convert.ToDateTime(date[1]).ToString("dd/MM/yyyy");
           //}
         
           DataSet ds = objmp.GetIntake_ALLOTMENT(Convert.ToInt32(ddlugpg.SelectedValue), Convert.ToString(StartDate), Convert.ToString(EndDate));
           if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
           {
              
              //ScriptManager.RegisterClientScriptBlock(updEdcutationalDetails, updEdcutationalDetails.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
               lvallotment.DataSource = ds;
               lvallotment.DataBind();
               divallotment.Visible = true;
               btnsubmit.Visible = true;
               btnclear.Visible = true;

           }

           else
           {
               lvallotment.DataSource = null;
               lvallotment.DataBind();
               divallotment.Visible = false;
               btnsubmit.Visible = false;
               btnclear.Visible = false;
           }
       }
       catch (Exception ex)
       {

       }
       
   }

   protected void btnsubmit_Click(object sender, EventArgs e)
   {
       try
        {

            if (ddlintakeAllotment.SelectedIndex <= 0)
            {
                objCommon.DisplayMessage(this.updIntaketransfer, "Please Select Intake!", this.Page);
                return;
            }
            int uano = Convert.ToInt32(Session["userno"]);
            string oldintake = "";
            int newintake =Convert.ToInt32(ddlintakeAllotment.SelectedValue);         
            string userno = "";
            string degree = "";
            string branch = "";

            foreach (ListViewDataItem lvItem in lvallotment.Items)
            {
                CheckBox chkallotment = lvItem.FindControl("chkallotment") as CheckBox;
                HiddenField hddegree = lvItem.FindControl("hdfdegree") as HiddenField;
                HiddenField hdbranch = lvItem.FindControl("hdfbranch") as HiddenField;
                Label lbladm = lvItem.FindControl("lblintake") as Label;

                //if (chkallotment.Checked == false)
                //{
                //    objCommon.DisplayMessage(this.updIntaketransfer, "Please enter details", this.Page);

                    if (chkallotment.Checked == true)
                    {
                        userno += chkallotment.ToolTip + ",";
                        oldintake = lbladm.ToolTip;
                        degree = (hddegree.Value);
                        branch = (hdbranch.Value);
                    }
                //}
            }

            if (objmp.AddIntakeAllotment(newintake, uano, oldintake,degree, branch, userno) == Convert.ToInt32(CustomStatus.RecordSaved))
            {
                ddlintakeAllotment.SelectedIndex = 0;
                BindList();
                objCommon.DisplayMessage(this.updIntaketransfer, "Intake Allotment Successfully!", this.Page);
            }
            else
                objCommon.DisplayMessage(this.updIntaketransfer, "Error in Intake Allotment", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_assignFacultyAdvisor.btnAssignFA0_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

   }
   protected void btnclear_Click(object sender, EventArgs e)
   {

       clearall();
   }

   private void clearall()
   {
       ddlugpg.SelectedIndex = 0;
       lvallotment.DataSource = null;
       lvallotment.DataBind();
   }

    //tab-2 end

    //tab-3 start
   protected void lkexamcenter_Click(object sender, EventArgs e)
   {
       divlkexamcenter.Attributes.Add("class", "active");
       divlkintaketransfer.Attributes.Remove("class");
       divlkintakeallotment.Attributes.Remove("class");
       divlkhallticket.Attributes.Remove("class");
       divlkapptitutetest.Attributes.Remove("class");
       
       divexamcenter.Visible = true;
       divintakeallotment.Visible = false;
       divintaketransfer.Visible = false;
       divhallticket.Visible = false;
       divapptitutetest.Visible = false;

       lvintakelist.Visible = false;
       divallotment.Visible = false;
       divlvhallticket.Visible = false;

       lvIntake.DataSource = null;
       lvIntake.DataBind();
       lvallotment.DataSource = null;
       lvallotment.DataBind();
       lvhallticket.DataSource = null;
       lvhallticket.DataBind();

       ddltransferintake.SelectedIndex = 0;
       ddlProgram.SelectedIndex = 0;
       ddlDiscipline.SelectedIndex = 0;
       ddlcurreentintake.SelectedIndex = 0;

       ddlugpg.SelectedIndex = 0;
       ddlintakeAllotment.SelectedIndex = 0;

       ddlintaketicket.SelectedIndex = 0;
       ddlDesciplineticket.SelectedIndex = 0;

   }

   protected void btnsaveexam_Click(object sender, EventArgs e)
   {
       try
       {
           if (ddlcenter.SelectedIndex <= 0)
           {
               objCommon.DisplayMessage(this.updexamallotment, "Please Select Campus!", this.Page);
               return;
           }
           int uano = Convert.ToInt32(Session["userno"]);
           string userno = "";
           int center = Convert.ToInt32(ddlcenter.SelectedValue);
           CustomStatus cs = 0;
          
           foreach (ListViewDataItem lvItem in lvexamcenter.Items)
           {
               CheckBox chkexam = lvItem.FindControl("chkboxexamcenter") as CheckBox;

               if (chkexam.Checked == true)
               {
                   userno += chkexam.ToolTip + ",";
                   cs = (CustomStatus)objmp.AddexamCenter(center, userno);
               }
           }
           if (cs.Equals(CustomStatus.RecordSaved))
           {

               objCommon.DisplayMessage(this.Page, "Record saved successfully.", this.Page);
           }
           else
           {
               objCommon.DisplayMessage(this.Page, "Please Select At least One checkbox", this.Page);
           }
           //if (objmp.AddexamCenter(center, userno) == Convert.ToInt32(CustomStatus.RecordSaved))
           //{
           //    ddlcenter.SelectedIndex = 0;
           //    binddata();
           //    objCommon.DisplayMessage(this.updexamallotment, "Campus Change Successfully!", this.Page);
           //}
           //else
           //    objCommon.DisplayMessage(this.updexamallotment, "Error in Campus Change", this.Page);
       }
       catch (Exception ex)
       {
           if (Convert.ToBoolean(Session["error"]) == true)
               objUCommon.ShowError(Page, "Academic_assignFacultyAdvisor.btnAssignFA0_Click-> " + ex.Message + " " + ex.StackTrace);
           else
               objUCommon.ShowError(Page, "Server UnAvailable");
       }

   }
   protected void vtncancelexam_Click(object sender, EventArgs e)
   {
       Clearexam();
   }

   private void Clearexam()
   {
       ddlugpgforexam.SelectedIndex = 0;
       lvexamcenter.DataSource = null;
       lvexamcenter.DataBind();
   }
   protected void ddlugpgforexam_SelectedIndexChanged(object sender, EventArgs e)
   {
       binddata();
   }

   private void binddata()
   {
       try
       {

           string StartEndDate = hdfexamdate.Value;
           string[] dates = new string[] { };
           if ((StartEndDate) == "")
           {
               objCommon.DisplayMessage(this.updexamallotment, "Please select Start Date End Date !", this.Page);
               return;
           }
           else
           {
               StartEndDate = StartEndDate.Substring(0, StartEndDate.Length - 0);         
               dates = StartEndDate.Split('-');
           }
           string StartDate = dates[0];
            string EndDate = dates[1];         
           DateTime dtStartDate = DateTime.Parse(StartDate);
           string SDate = dtStartDate.ToString("yyyy/MM/dd");
           DateTime dtEndDate = DateTime.Parse(EndDate);
           string EDate = dtEndDate.ToString("yyyy/MM/dd");      
          //DateTime EXAMDATE= Convert.ToDateTime(hdfexamdate.Value);
           DataSet ds = objmp.GetIntake_ExamCenter(Convert.ToInt32(ddlugpgforexam.SelectedValue), Convert.ToDateTime(SDate), Convert.ToDateTime(EDate));
           if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
           {
               lvexamcenter.DataSource = ds;
               lvexamcenter.DataBind();
               divexam.Visible = true;
               btnsaveexam.Visible = true;
               vtncancelexam.Visible = true;
           }

           else
           {
               lvexamcenter.DataSource = null;
               lvexamcenter.DataBind();
               divexam.Visible = false;
               btnsaveexam.Visible = false;
               vtncancelexam.Visible = false;
           }
       }
       catch (Exception ex)
       {

       }
      
   }

    //TAB-3 END

    //tab-4 start
   protected void lkhallticket_Click(object sender, EventArgs e)
   {
       divlkhallticket.Attributes.Add("class", "active");
       divlkexamcenter.Attributes.Remove("class");
       divlkintaketransfer.Attributes.Remove("class");
       divlkintakeallotment.Attributes.Remove("class");
       divlkapptitutetest.Attributes.Remove("class");

       divintakeallotment.Visible = false;
       divintaketransfer.Visible = false;
       divexamcenter.Visible = false;
       divhallticket.Visible = true;
       divapptitutetest.Visible = false;

       lvIntake.DataSource = null;
       lvIntake.DataBind();
       lvallotment.DataSource = null;
       lvallotment.DataBind();
       lvexamcenter.DataSource = null;
       lvexamcenter.DataBind();

       ddltransferintake.SelectedIndex = 0;
       ddlProgram.SelectedIndex = 0;
       ddlDiscipline.SelectedIndex = 0;
       ddlcurreentintake.SelectedIndex = 0;

       ddlugpgforexam.SelectedIndex = 0;

       ddlugpg.SelectedIndex = 0;
       ddlintakeAllotment.SelectedIndex = 0;

   }
   protected void ddlintaketicket_SelectedIndexChanged(object sender, EventArgs e)
   {
       bindhallticket();
   }

   protected void ddlDesciplineticket_SelectedIndexChanged(object sender, EventArgs e)
   {
       bindhallticket();
   }
   private void bindhallticket()
   {
       try
       {

           DataSet ds = objmp.GetExam_hallticket(Convert.ToInt32(ddlintaketicket.SelectedValue), Convert.ToInt32(ddlDesciplineticket.SelectedValue));
           if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
           {
               lvhallticket.DataSource = ds;
               lvhallticket.DataBind();
               divlvhallticket.Visible = true;
               linksubmitHT.Visible = true;
               lnkClearHT.Visible = true;
           }

           else
           {
               //objCommon.DisplayMessage(this.updhallticket, "Record Not Found!", this.Page);
               lvhallticket.DataSource = null;
               lvhallticket.DataBind();
               divlvhallticket.Visible = false;
               linksubmitHT.Visible = false;
               lnkClearHT.Visible = false;
           }
       }
       catch (Exception ex)
       {

       }

   }
   protected void lnkClearHT_Click(object sender, EventArgs e)
   {
       clearticket();
   }

   private void clearticket()
   {
       ddlDesciplineticket.SelectedIndex = 0;
       ddlintaketicket.SelectedIndex = 0;

       lvhallticket.DataSource = null;
       lvhallticket.DataBind();
   }
   protected void linksubmitHT_Click(object sender, EventArgs e)
   {
       try
       {
           string ids = GETUSER();
           if (!string.IsNullOrEmpty(ids))
           {
               ShowReport("ApptituteHallTicket", "AptitudeTestTicket.rpt");
           }
       }
       catch (Exception ex)
       {
           if (Convert.ToBoolean(Session["error"]) == true)
               objUCommon.ShowError(Page, "Intake_Transfer.linksubmitHT_Click-> " + ex.Message + " " + ex.StackTrace);
           else
               objUCommon.ShowError(Page, "Server UnAvailable");
       }
   }
   private string GETUSER()
   {
       string userno = string.Empty;
       try
       {
           foreach (ListViewDataItem item in lvhallticket.Items)
           {
               if ((item.FindControl("chkhallticket") as CheckBox).Checked)
               {
                   if (userno.Length > 0)
                       userno += ".";
                   userno += (item.FindControl("hfduserno") as HiddenField).Value.Trim();
                   if ((item.FindControl("chkhallticket") as CheckBox).Checked == true)
                   {

                       string USER = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNO=" + Convert.ToInt32((((item.FindControl("chkhallticket")) as CheckBox).ToolTip) + ""));
             
                   }
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
       return userno;
   }  
   private void ShowReport(string reportTitle, string rptFileName)
   {
       try
       {
           string ID = GETUSER();
           string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
           url += "Reports/CommonReport.aspx?";
           url += "pagetitle=" + reportTitle;
           url += "&path=~,Reports,Academic," + rptFileName;
           url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNO=" + ID;
           divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
           divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
           divMsg.InnerHtml += " </script>";
       }
       catch (Exception ex)
       {
           if (Convert.ToBoolean(Session["error"]) == true)
               objUCommon.ShowError(Page, "Intake_Transfer.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
           else
               objUCommon.ShowError(Page, "Server Unavailable.");
       }
   }
    //tab-4 end

    //tab-5 start
   protected void lktaptitutetest_Click(object sender, EventArgs e)
   {
       divlkhallticket.Attributes.Remove("class");
       divlkexamcenter.Attributes.Remove("class");
       divlkintaketransfer.Attributes.Remove("class");
       divlkintakeallotment.Attributes.Remove("class");
       divlkapptitutetest.Attributes.Add("class", "active");

       lvallotment.DataSource = null;
       lvallotment.DataBind();
       lvexamcenter.DataSource = null;
       lvexamcenter.DataBind();
       lvhallticket.DataSource = null;
       lvhallticket.DataBind();
       lvIntake.DataSource = null;
       lvIntake.DataBind();
       divintakeallotment.Visible = false;
       divintaketransfer.Visible = false;
       divexamcenter.Visible = false;
       divhallticket.Visible = false;
       divapptitutetest.Visible = true;
       ddltransferintake.SelectedIndex = 0;
       ddlProgram.SelectedIndex = 0;
       ddlDiscipline.SelectedIndex = 0;
       ddlcurreentintake.SelectedIndex = 0;
       ddlugpgforexam.SelectedIndex = 0;
       ddlugpg.SelectedIndex = 0;
       ddlintakeAllotment.SelectedIndex = 0;
   }
   protected void lbExcelFormat_Click1(object sender, EventArgs e)
   {
       Response.ContentType = "application/vnd.ms-excel";
       string path = Server.MapPath("~/ExcelFormat/apptitude_exam_test_sheet.xls");
       Response.AddHeader("Content-Disposition", "attachment;filename=\"apptitude_exam_test_sheet.xls\"");
       Response.TransmitFile(path);
       Response.End();
   }
   protected void btnUpload_Click(object sender, EventArgs e)
   {
       try

       {
          //if (ddlintakeapti.SelectedIndex <= 0)
           //{
           //    objCommon.DisplayMessage(this.updIntaketransfer, "Please Select Intake!", this.Page);
           //    return;
           //}
           //if (ddlDegree.SelectedIndex <= 0)
           //{
           //    objCommon.DisplayMessage(this.updIntaketransfer, "Please Select Degree!", this.Page);
           //    return;
           //}

           divlkhallticket.Attributes.Remove("class");
           string path = System.Web.HttpContext.Current.Server.MapPath("~/ExcelData/");
            if (FileUpload1.PostedFile.FileName.ToString() == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Attach File. !!!", this.Page);
                lblTotalMsg.Text = "Please select file to upload.";
                return;
            }
            else
            {
               string extension = Path.GetExtension(FileUpload1.PostedFile.FileName.Trim());

               if (extension.Contains(".xls") || extension.Contains(".xlsx"))
               {
                   if (!(Directory.Exists(MapPath("~/PresentationLayer/ExcelData"))))
                       Directory.CreateDirectory(path);

                   string fileName = FileUpload1.PostedFile.FileName.Trim();
                   if (File.Exists((path + fileName).ToString().Trim()))
                       File.Delete((path + fileName).ToString().Trim());
                   FileUpload1.SaveAs(path + fileName);
                   CheckFormatAndImport(extension, path.Trim() + fileName);

                   //Added My Mahesh M on dated 09-01-2022
                   divuploadexcel.Visible = false;
                   lvuploexcel.DataSource = null;
                   lvuploexcel.DataBind();
               }
               else
               {
                  // lblTotalMsg.Text = "Only excel sheet is allowed to upload";
                  
                   objCommon.DisplayMessage(this.updapptitutetest, "Only excel sheet is allowed to upload.", this.Page);

                   return;
               }
           }
           //else
           //{
           //    lblTotalMsg.Text = "Please select file to upload.";
           //    objCommon.DisplayMessage(this.updapptitutetest, "Please select file to upload.", this.Page);
           //    return;
           //}
       }
       catch (Exception ex)
       {
           if (Convert.ToBoolean(Session["error"]) == true)
               objUCommon.ShowError(this.Page, " ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
           else
               objUCommon.ShowError(Page, "Server UnAvailable");
       }
   }

   private bool CheckFormatAndImport(string extension, string excelPath)
   {
       string conString = string.Empty;
       switch (extension)
       {
           case ".xls":
               conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
               break;
           case ".xlsx":
               conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
               break;
       }

       conString = string.Format(conString, excelPath);
       objCommon.DisplayMessage(this.updapptitutetest, conString, this.Page);
       try
       {
           using (OleDbConnection excel_con = new OleDbConnection(conString))
           {
               excel_con.Open();
               DataTable dtExcelData = new DataTable();
               string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString().Trim();

               dtExcelData.Columns.AddRange(new DataColumn[] {


                new DataColumn("ExamName", typeof(string)),
                new DataColumn("SubjectName", typeof(string)),
                new DataColumn("RollNo", typeof(string)),
                new DataColumn("PRNNo", typeof(string)),
                new DataColumn("CandidateName", typeof(string)),
                new DataColumn("MobileNo", typeof(string)),
                new DataColumn("MaxMarks", typeof(decimal)),
                new DataColumn("MarksObtained", typeof(decimal)),
                new DataColumn("ExamSubmitDate", typeof(DateTime)),
                new DataColumn("ExamStatus", typeof(string)),
                new DataColumn("General", typeof(decimal)),
                new DataColumn("English", typeof(decimal)),
                     
                });

               using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
               {
                   oda.Fill(dtExcelData);
                   DataSet ds1 = new DataSet();
                   ds1.Tables.Add(dtExcelData);
                   divapti.Visible = true;
                   btncanceltest.Visible = true;
                   foreach (DataTable table in ds1.Tables)
                   {
                       for (int i = 0; i < table.Rows.Count; i++)
                       {
                           foreach (DataRow dr in table.Rows)
                           {
                               if (dr["CandidateName"].ToString() == string.Empty)
                               {
                                   objCommon.DisplayMessage("Cant Pass Null Records in Excel Sheet!!", this);
                                   dr.Delete();
                                   table.AcceptChanges();
                                   break;

                               }
                           }
                       }
                   }
               }
               excel_con.Close();
               string area = "";

               foreach (ListItem item in ddldes.Items)
               {
                   if (item.Selected == true)
                   {
                       area += item.Value + ',';
                   }
               }
               if (!string.IsNullOrEmpty(area))
               {
                   area = area.Substring(0, area.Length - 1);
               }
               else
               {
                   area = "0";
               }
             

               objIDM.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
               int ret = objmp.ImportDataForApti(dtExcelData, Convert.ToInt32(ddlintakeprep.SelectedValue), Convert.ToInt32(Session["userno"]), area);
               if (ret == 1)
               {
                   lvapti.DataSource = dtExcelData;
                   divapti.Visible = true;
                   excellist();                 
                   btncanceltest.Visible = true;
                   ddlintakeapti.SelectedIndex = 0;
                   objCommon.DisplayMessage("Data Upload Successfully!!", this);
                   //lblTotalMsg.Text = "Records uploaded ";
                   //ddlintakeprep.SelectedIndex = 0;
                   //ddldes.SelectedIndex = 0;
                    lblTotalMsg.Style.Add("color", "green");
                   return true;
               }
              
               else
               {
                   objCommon.DisplayMessage("Record not uploaded, file is not in correct format. Please check the format of file & upload again.", this);
                   lblTotalMsg.Text = "Error.!! Record not saved.";
                   return false;
               }

           }
       }
       catch (Exception ex)
       {
           objCommon.DisplayMessage("Record not uploaded, file is not in correct format. Please check the format of file & upload again.", this);
           if (Convert.ToBoolean(Session["error"]) == true)
               objUCommon.ShowError(Page, "Records not saved. Please check whether the file is in correct format or not. ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
           else
               objUCommon.ShowError(this.Page, "Server UnAvailable");
           return false;
       }
   }
   
   protected void btncanceltest_Click(object sender, EventArgs e)
   {
       divlkintaketransfer.Attributes.Remove("class");
       ddlintakeapti.SelectedIndex = 0;
       ddldesrdiofir.SelectedIndex = 0;
       ddlDegree.SelectedIndex = 0;
       ddldes.SelectedIndex = 0;
       ddlfaculty.SelectedIndex = 0;
       ddlprgm.SelectedIndex = 0;
       ddlintakeprep.SelectedIndex = 0;

       divapti.Visible = false;
       lvapti.DataSource = null;
       lvapti.DataBind();


       lvuploexcel.DataSource = null;
       lvuploexcel.DataBind();

       lvapti.DataSource = null;
       lvapti.DataBind();

       lvtestprep.DataSource = null;
       lvtestprep.DataBind();
       FileUpload1.Attributes.Clear();
      
   }

   protected void excellist()
   {
       divlkintaketransfer.Attributes.Remove("class");
       string area = "";

       foreach (ListItem item in ddldes.Items)
       {
           if (item.Selected == true)
           {
               area += item.Value + ',';
           }
       }
       if (!string.IsNullOrEmpty(area))
       {
           area = area.Substring(0, area.Length - 1);
       }
       else
       {
           area = "0";
       }
       DataSet ds = objmp.excelsheet(Convert.ToInt32(ddlintakeprep.SelectedValue), area);
       if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
       {
           divapti.Visible = true;
           lvapti.DataSource = ds;
           lvapti.DataBind();
       }
       else
       {
           divapti.Visible = false;
           lvapti.DataSource = null;
           lvapti.DataBind();
       }

   }
    //tab-5 end

   protected void btnApplyFilter_Click(object sender, EventArgs e)
   {
       try
       {
           if (ddlMainLeadLabel.SelectedIndex == 0 || ddlMainLeadLabel.SelectedValue == "")
           {
               objCommon.DisplayMessage(this.UpdatePanel2, "Please Select Main Heading.", this.Page);
               return;
           }
           if (Convert.ToString(ddlMainLeadLabel.SelectedValue) == "1")
           {
               DataSet ds = objmp.GetIntake_transfer_FITERATION(1, Convert.ToInt32(ddlcurreentintake.SelectedValue), 1);
               if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
               {
                   lvIntake.DataSource = ds;
                   lvIntake.DataBind();
                   ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
                   lvintakelist.Visible = true;
                   btnSave.Visible = true;
                   btnCancel.Visible = true;
               }
               else
               {
                   lvIntake.DataSource = null;
                   lvIntake.DataBind();
                   lvintakelist.Visible = false;
                   btnSave.Visible = false;
                   btnCancel.Visible = false;
               }
           }
           if (Convert.ToString(ddlMainLeadLabel.SelectedValue) == "2")
           {
               DataSet ds = objmp.GetIntake_transfer_FITERATION(0, Convert.ToInt32(ddlcurreentintake.SelectedValue), 2);
               if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
               {
                   lvIntake.DataSource = ds;
                   lvIntake.DataBind();
                   ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
                   lvintakelist.Visible = true;
                   btnSave.Visible = true;
                   btnCancel.Visible = true;
               }
               else
               {
                   lvIntake.DataSource = null;
                   lvIntake.DataBind();
                   lvintakelist.Visible = false;
                   btnSave.Visible = false;
                   btnCancel.Visible = false;
               }
           }
           if (Convert.ToString(ddlMainLeadLabel.SelectedValue) == "3")
           {
               DataSet ds = objmp.GetIntake_transfer_FITERATION(0, Convert.ToInt32(ddlcurreentintake.SelectedValue), 3);
               if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
               {
                   lvIntake.DataSource = ds;
                   lvIntake.DataBind();
                   ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
                   lvintakelist.Visible = true;
                   btnSave.Visible = true;
                   btnCancel.Visible = true;
               }
               else
               {
                   lvIntake.DataSource = null;
                   lvIntake.DataBind();
                   lvintakelist.Visible = false;
                   btnSave.Visible = false;
                   btnCancel.Visible = false;
               }
           }
       }

       catch (Exception ex)
       {
           if (Convert.ToBoolean(Session["error"]) == true)
               objCommon.ShowError(Page, "Academic_adminmaster.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
           else
               objCommon.ShowError(Page, "Server Unavailable.");
       }
        
   }
   protected void btnClearFilter_Click(object sender, EventArgs e)
   {
       ddlMainLeadLabel.SelectedValue = "0";
   }

   private void bindtestprep()
   {
   //    string[] program;
   //    if (ddlprgm.SelectedValue == "0")
   //    {
   //        program = "0,0".Split(',');
   //    }
   //    else
   //    {
   //        program = ddlprgm.SelectedValue.Split(',');
   //    }
       string area = "";

       foreach (ListItem item in ddldesrdiofir.Items)
       {
           if (item.Selected == true)
           {
               area += item.Value + ',';
           }
       }
       if (!string.IsNullOrEmpty(area))
       {
           area = area.Substring(0, area.Length - 1);
       }
       else
       {
           area = "0";
       }
       DataSet ds = objmp.gettwestprepdata(Convert.ToInt32(ddlintakeapti.SelectedValue), area,"0","0");
       if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
       {
           lvtestprep.DataSource = ds;
           lvtestprep.DataBind();
           divtestprep.Visible = true;

       }
       else
       {
           lvtestprep.DataSource = null;
           lvtestprep.DataBind();
           divtestprep.Visible = false;

       }
   }


   protected void ddlprgm_SelectedIndexChanged(object sender, EventArgs e)
   {
       bindtestprep();

   }
   protected void ddlintakeapti_SelectedIndexChanged(object sender, EventArgs e)
   {
      
       bindtestprep();
       
   }
   protected void ddldes_SelectedIndexChanged(object sender, EventArgs e)
   {
       //excellist();
   }
   protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
   {
       //excellist();
   }
   protected void ddlfaculty_SelectedIndexChanged(object sender, EventArgs e)
   {
       if (ddlfaculty.SelectedIndex > 0)
       {
           objCommon.FillDropDownList(ddlprgm, "ACD_USER_BRANCH_PREF S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=S.BRANCHNO)", "DISTINCT CONCAT(D.DEGREENO, ', ',B.BRANCHNO)AS ID", "(D.DEGREENAME+'-'+B.LONGNAME)AS DEGBRANCH", "S.DEGREENO>0 AND S.COLLEGE_ID=" + (ddlfaculty.SelectedValue), "ID");
       }
       bindtestprep();
   }
   protected void ddldesrdiofir_SelectedIndexChanged(object sender, EventArgs e)
   {
       bindtestprep();
   }
   protected void ddlintakeprep_SelectedIndexChanged(object sender, EventArgs e)
   {
       //excellist();
   }
 
   //protected void btnverify_Click(object sender, EventArgs e)
   //{
   //    //Button btnVerifyAndRegisterV = (Button)(sender);
   //    //int TotStudent = Convert.ToInt32(btnVerifyAndRegisterV.CommandArgument);
   //    //string CCODE = btnVerifyAndRegisterV.CommandName;

   //    //ImportDataMaster objIDM = new ImportDataMaster();
   //    //ImportDataController objIDC = new ImportDataController();

   //    //objIDM.SESSIONNO = Convert.ToInt16(ddlSessionV.SelectedValue);
   //    //objIDM.COLLEGEID = Convert.ToInt16(ddlCollegeV.SelectedValue);
   //    //objIDM.DEGREENO = Convert.ToInt16(ddlDegreeV.SelectedValue);
   //    //objIDM.SEMESTERNO = Convert.ToInt16(ddlSemesterV.SelectedValue);
   //    //int EXAM_TYPE = Convert.ToInt32(ddlExamTypeV.SelectedValue);
   //    //DataSet ds = objIDC.ShowExamSheetUploadedDataExt(objIDM, EXAM_TYPE, CCODE);//, out retout
   //    //if (ds.Tables[0].Rows.Count > 0)
   //    //{
   //    //    lvuploexcel.DataSource = ds;
   //    //    lvuploexcel.DataBind();
   //    //    divuploadexcel.Visible = true;
   //    //}
   //    //else
   //    //{
   //    //    divuploadexcel.Visible = false;
   //    //    lvuploexcel.DataSource = null;
   //    //    lvuploexcel.DataBind();
   //    //    objCommon.DisplayMessage(updapptitutetest, "Exam Sheet Excel Not Uploaded,So Please Uploading Excel Sheet First After That Verify !", this.Page);

   //    //}
   //}
   protected void btnconfirmed_Click(object sender, EventArgs e)
   {
       int cntExcelUpload = 0;
       cntExcelUpload = Convert.ToInt16(objCommon.LookUp("TEMP_APPTITUDE_TEST_MARK_ENTRY", "COUNT(ROLLNO)", "ISNULL(CANCEL,0)=0 AND INTAKENO=" + ddlintakeprep.SelectedValue));

       if (cntExcelUpload > 0)
       {
           VerifyandRegister();
       }
       else
       {
           objCommon.DisplayMessage(updapptitutetest, "Excel sheet is not uploaded for given selection. First upload excel sheet then Verify and Move.", this);
       }
   }
   protected void confirmedexcellist()
   {
       divlkintaketransfer.Attributes.Remove("class");
       string area = "";

       foreach (ListItem item in ddldes.Items)
       {
           if (item.Selected == true)
           {
               area += item.Value + ',';
           }
       }
       if (!string.IsNullOrEmpty(area))
       {
           area = area.Substring(0, area.Length - 1);
       }
       else
       {
           area = "0";
       }
       DataSet ds = objmp.confirmedexcelsheet(Convert.ToInt32(ddlintakeprep.SelectedValue), area);
       if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
       {
           divapti.Visible = false;
           lvapti.DataSource = null;
           lvapti.DataBind();
           divuploadexcel.Visible = true;
           lvuploexcel.DataSource = ds;
           lvuploexcel.DataBind();
       }
       else
       {
           divapti.Visible = false;
           lvapti.DataSource = null;
           lvapti.DataBind();
           divuploadexcel.Visible = false;
           lvuploexcel.DataSource = null;
           lvuploexcel.DataBind();
       }

   }
   private void VerifyandRegister()
   {
       try
       {
           DataSet ds = null;
           int retout = 0;
           retout = objmp.VerifyandMovedImportedDataExt(Convert.ToInt32(ddlintakeprep.SelectedValue), Convert.ToInt32(Session["uano"]));//, out retout
           if (retout > 0)
           {
               objCommon.DisplayMessage(updapptitutetest, "Records verified successfully", this.Page);
               confirmedexcellist();
               lblTotalMsg.Text = "Records Verified ";      
               lblTotalMsg.Style.Add("color", "green");

           }
           else
           {
               objCommon.DisplayMessage(updapptitutetest, "Records verified Fail.", this.Page);
           }

       }
       catch (Exception ex)
       {
           objCommon.DisplayMessage(updapptitutetest, "Error occurred.", this.Page);
           if (Convert.ToBoolean(Session["error"]) == true)
               objUCommon.ShowError(Page, "Error occurred. ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
           else
               objUCommon.ShowError(this.Page, "Server UnAvailable");
       }
   }
   protected void btnexcelformat_Click(object sender, EventArgs e)
   {
       Response.ContentType = "application/vnd.ms-excel";
       string path = Server.MapPath("~/ExcelFormat/Preformat_Of_Exam_Result.xls");
       Response.AddHeader("Content-Disposition", "attachment;filename=\"Preformat_Of_Exam_Result.xls\"");
       Response.TransmitFile(path);
       Response.End();
   }
   //protected void ddldesrdiofir_SelectedIndexChanged1(object sender, EventArgs e)
   //{
   //    bindtestprep();
   //}
   protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
   {
       try
       {
           BindListview();
       }
       catch (Exception ex)
       { 
        
       }
   }
}