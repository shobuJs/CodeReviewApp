//======================================================================================
// PROJECT NAME  : RFC SVCE                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Section Allotment                                    
// CREATION DATE : 30/05/19
// CREATED BY    : Ankush T                                              
// MODIFIED DATE : 
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//====================================================================================== 

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Data.Common;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_SectionAllotment : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    //USED FOR INITIALSING THE MASTER PAGE
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    //USED FOR BYDEFAULT LOADING THE default PAGE
    protected void Page_Load(object sender, EventArgs e)
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

                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                PopulateDropDownList();
                ViewState["degreeno"] = 0;
                ViewState["branchno"] = 0;
                ViewState["deptno"] = 0;
                ViewState["schemeno"] = 0;
                ViewState["YearWise"] = 0;
                btnSubmit.Enabled = false;
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
            }
        }
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
    }
    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page.
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SectionAllotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SectionAllotment.aspx");
        }
    }
    #endregion
    //display student details as per as selection of criteria
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        if (ddlStudenType.SelectedValue != "3")
        {
            if (txtSectionName.Text.Trim() == string.Empty)
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                objCommon.DisplayMessage(this.updSection, "Please Enter Short Name !", this.Page);
                return;
            }
            else if (txtSectionNo.Text.Trim() == string.Empty)
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                objCommon.DisplayMessage(this.updSection, "Please Enter Group No !", this.Page);
                return;
            }
            else
            {
                this.BindListView();
                this.GetSectionName();
            }
        }
        else
        {
            this.BindListView();
            this.GetSectionName();
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        this.Clear();
        ViewState["YearWise"] = 0;                
    }
    //used for select the Section Name or Class Roll NO for allotment and submit this selection data and display this data from database
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            this.GetSectionName();
           
            string studids = string.Empty;
            string sections = string.Empty;
            string rollnos = string.Empty;
            string studidno = string.Empty;
            string sectionname = string.Empty;
         
            foreach (ListViewDataItem lvItem in lvStudents.Items)
            {
                CheckBox chkStu =lvItem.FindControl("chkStu") as CheckBox;
                Label lblIdno = lvItem.FindControl("lblIdno") as Label;
                TextBox RollNo = lvItem.FindControl("txtRollNo") as TextBox;
                if (chkStu.Checked == true && chkStu.Enabled == true)
                {
                    studidno += lblIdno.Text + "$";

                        //studids += (lvItem.FindControl("cbRow") as CheckBox).ToolTip + "$";
                        studids += lblIdno.Text + "$";
                        sections += 1 + "$";
                        rollnos += 1 + "$";// (lvItem.FindControl("txtRollNo") as TextBox).Text + "$";
                        sectionname += Label3.Text + "$";
                }
            }
            if (studidno.Length <= 0 && sections.Length <= 0)
            {
                objCommon.DisplayMessage(this.updSection, "Please check at list one student", this.Page);
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                ddlSection.SelectedValue = "0";
                this.BindListView(); 
               // return;
            }
            if (studids.Length <= 0 && sections.Length <= 0)
            {
                objCommon.DisplayMessage(this.updSection, "Please Select Student/Section", this.Page);
                return;
            }
   
            if (objSC.UpdateStudentSection(studids, rollnos, sections,Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(Session["userno"]),sectionname, Convert.ToString(ViewState["ipAddress"])) == Convert.ToInt32(CustomStatus.RecordUpdated))
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                this.BindListView();
                ddlSection.SelectedValue = "0";
                objCommon.DisplayMessage(this.updSection, "Record Save Successfully!!!", this.Page);
                //objCommon.DisplayMessage(this.updSection, "Section Alloted Successfully!!!", this.Page);
            }
            else
                objCommon.DisplayMessage(this.updSection, "Server Error...", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_SectionAllotment.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #region Private Methods
    ////bind BATCH NAME ,DEGREE NAME,  SEMESTER NAME, SECTION NAME  in drop down list. 
    private void PopulateDropDownList()
    {
        try
        {

            objCommon.FillDropDownList(ddlsession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "", "SESSION_NAME DESC");
     
            //objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNAME DESC");
            //objCommon.FillDropDownList(ddlClgReg, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COSCHNO>0 AND COLLEGE_ID IN (" + Session["college_nos"].ToString() + ")", "COSCHNO");
           // objCommon.FillDropDownList(ddlClgReg, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "CONCAT(DEGREENO ,',', BRANCHNO,',', DEPTNO,',',SCHEMENO) ID,COL_SCHEME_NAME", "COSCHNO>0", "COSCHNO");

            objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNO");
            objCommon.FillDropDownList(ddlCampus, "ACD_CAMPUS", "CAMPUSNO", "CAMPUSNAME", "CAMPUSNO > 0", "CAMPUSNO");
            objCommon.FillDropDownList(ddlweekday, "ACD_BATCH_SLIIT", "WEEKNO", "WEEKDAYSNAME", "WEEKNO > 0", "WEEKNO");
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_SectionAllotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    public void GetSectionName()
    { 
        
        //DataSet ds = objSC.GetSecionName();

          DataSet ds = objSC.GetSecionName(Convert.ToInt32(ddlSemester.SelectedValue),Convert.ToInt32(ddlweekday.SelectedValue) ,Convert.ToString(txtSectionName.Text),Convert.ToString(txtSectionNo.Text));

             //string dsfee = objIDC.getApptiDataExcel(Convert.ToInt32(ddlintakeprep.SelectedValue), area);

          Label3.Text = ds.Tables[0].Rows[0][0].ToString();
    
    }

    //bind student deatils in List view according to selection of criteria
    private void BindListView()
    {
        try
        {
            string DegreeNo = string.Empty;
            string BranchNo = string.Empty;
            string Program = string.Empty;
            foreach (ListItem items in lstProgram.Items)
            {
                if (items.Selected == true)
                {
                    Program = items.Value;
                    string[] splitValue;
                    splitValue = Program.Split('$');

                    DegreeNo += (splitValue[0].ToString()) + '$';
                    BranchNo += (splitValue[1].ToString()) + '$';
                }
            }

            DegreeNo = DegreeNo.TrimEnd('$');
            BranchNo = BranchNo.TrimEnd('$');
            if (DegreeNo.ToString() == "" && BranchNo.ToString() == "")
            {
                objCommon.DisplayMessage(this.updSection, "Please select Program !", this.Page);
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                return;
            }
            if (ddlSemester.SelectedValue == "0" || ddlSemester.SelectedValue=="")
            {
                objCommon.DisplayMessage(this.updSection, "Please select Semester !", this.Page);
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                return;
            }
            hdfTot.Value = "0";
            txtTotStud.Text = "0";
            int total = 0;
            string TotalCount = "";

            string SP_Name1 = "PKG_ACD_GET_STUDENT_FOR_SECTION_ALLOTMENT";
            string SP_Parameters1 = "@P_SESSIONNO,@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO,@P_CAMPUSNO,@P_WEEKNO,@P_FILTER_TYPE";
            string Call_Values1 = "" + Convert.ToInt32(ddlsession.SelectedValue) + "," + Convert.ToInt32(ddlfaculty.SelectedValue) + "," + Convert.ToString(DegreeNo) + "," +
                Convert.ToString(BranchNo) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(ddlCampus.SelectedValue) + "," + Convert.ToInt32(ddlweekday.SelectedValue) + "," + Convert.ToInt32(ddlStudenType.SelectedValue);

            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);

            if (ds != null && ds.Tables.Count > 0)
            {
                TotalCount = ds.Tables[0].Rows.Count.ToString();
                int count=0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvStudents.DataSource = ds;
                    lvStudents.DataBind();

                    foreach (ListViewDataItem dataitem in lvStudents.Items)
                    {
                        CheckBox chkAll = dataitem.FindControl("chkAll") as CheckBox;
                        CheckBox chkStu = dataitem.FindControl("chkStu") as CheckBox;
                        HiddenField hdfSection = dataitem.FindControl("hdfSection") as HiddenField;
                        Label lblSection = dataitem.FindControl("lblSection") as Label;

                        total++;

                        if (lblSection.Text.Trim().ToString() != string.Empty)
                        {
                            count++;
                            chkStu.Checked = true;
                            chkStu.Enabled = false;
                        }
                    }
                   
                    DivTotal.Visible=true;
                    txtTotStud.Text = count.ToString();
                    lblTotal.Text = total.ToString();
                    hdfTot.Value = ds.Tables[0].Rows.Count.ToString();
                    btnSubmit.Enabled = true;
                }
                else
                {
                    lvStudents.DataSource = null;
                    lvStudents.DataBind();
                    hdfTot.Value = "0";
                    objCommon.DisplayMessage(this.updSection, "No Students found for selected criteria!", this.Page);
                    btnSubmit.Enabled = false;
                    DivTotal.Visible = false;
                }
            }
            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                hdfTot.Value = "0";
                objCommon.DisplayMessage(this.updSection, "No Students found for selected criteria!", this.Page);
                btnSubmit.Enabled = false;
                DivTotal.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_SectionAllotment.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //cleare all the selection
    private void Clear()
    {


        ddlfaculty.Items.Clear();
        lstProgram.Items.Clear();
        ddlSemester.Items.Clear();
        txtTotStud.Text = "0";
        hdfTot.Value = "0";
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        btnSubmit.Enabled = false;
        lstProgram.Items.Clear();
        DivTotal.Visible = false;
    }
    #endregion

    
    //protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    try
    //    {
    //        DropDownList ddlsec = e.Item.FindControl("ddlsec") as DropDownList;
    //        DataSet ds = objCommon.FillDropDown("ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNAME");
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            DataTableReader dtr = ds.Tables[0].CreateDataReader();
    //            while (dtr.Read())
    //            {
    //                ddlsec.Items.Add(new ListItem(dtr["SECTIONNAME"].ToString(), dtr["SECTIONNO"].ToString()));
    //            }
    //        }
    //        ddlsec.SelectedValue = ddlsec.ToolTip;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_SectionAllotment.lvStudents_ItemDataBound-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    //protected void ddlClgReg_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlClgReg.SelectedIndex > 0)
    //    {
    //        ddlAdmBatch.SelectedIndex = 0;
    //        ddlSemester.SelectedIndex = 0;
    //      //  DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "DEGREENO , BRANCHNO, DEPTNO,SCHEMENO,COL_SCHEME_NAME", "COSCHNO>0", "COSCHNO");
    //        DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgReg.SelectedValue));
    //        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
    //        {
    //            ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
    //            ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
    //            ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
    //            ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();                
    //        }

    //        ViewState["YearWise"] = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "ISNULL(YEARWISE,0)YEARWISE", "DEGREENO=" + ViewState["degreeno"].ToString()));
    //        if (ViewState["YearWise"].ToString() == "1")
    //        {
    //            objCommon.FillDropDownList(ddlSemester, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR<>0", "YEAR");               
    //        }
    //        else
    //        {
    //            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO<>0", "SEMESTERNO");
    //        }                   
    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage("Please Select College & Regulation", this.Page);
    //        ddlSemester.SelectedIndex = 0;
    //        ddlSection.SelectedIndex = 0;           
    //    }

    //}
    protected void bntExcelReport_Click(object sender, EventArgs e)
    {
        string DegreeNo = string.Empty;
        string BranchNo = string.Empty;
        string Program = string.Empty;
        foreach (ListItem items in lstProgram.Items)
        {
            if (items.Selected == true)
            {
                Program = items.Value;
                string[] splitValue;
                splitValue = Program.Split('$');

                DegreeNo += (splitValue[0].ToString()) + '$';
                BranchNo += (splitValue[1].ToString()) + '$';
            }
        }

        DegreeNo = DegreeNo.TrimEnd('$');
        BranchNo = BranchNo.TrimEnd('$');
        if (DegreeNo.ToString() == "" && BranchNo.ToString() == "")
        {
            objCommon.DisplayMessage(this.updSection, "Please select Program !", this.Page);
            return;
        }
        if (ddlSemester.SelectedValue == "0" || ddlSemester.SelectedValue == "")
        {
            objCommon.DisplayMessage(this.updSection, "Please select Semester !", this.Page);
            return;
        }
        string SP_Name1 = "PKG_GET_ALLOTED_STUDENT_EXCEL_LIST";
        string SP_Parameters1 = "@P_DEGREENO,@P_BRANCHNO,@P_COLLEGE_ID,@P_SESSIONNO,@P_SEMESTERNO,@P_STATUS";
        string Call_Values1 = "" + DegreeNo.ToString() + "," + BranchNo.ToString() + "," + Convert.ToInt32(ddlfaculty.SelectedValue) + "," + Convert.ToInt32(ddlsession.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + ","+ddlStudenType.SelectedValue+"";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView GV = new GridView();
            string ContentType = string.Empty;
            string attachment = string.Empty;
            GV.DataSource = ds;
            GV.DataBind();
            Response.Clear();
            Response.Buffer = true;

            attachment = "attachment; filename=GroupAlloteList.xls";

            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GV.RenderControl(htw);
            Response.Write(sw.ToString());
            // Response.Flush();
            Response.End(); 
        }
        else
        {
            objCommon.DisplayMessage(this, "Record Not Found", this.Page);
            return;
        }

    }
    protected void ddlfaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlfaculty.SelectedValue != "0")
        {

            objCommon.FillListBox(lstProgram, "ACD_STUDENT_RESULT DB INNER JOIN  ACD_BRANCH B ON(DB.BRANCHNO=B.BRANCHNO)INNER JOIN ACD_DEGREE D ON(D.DEGREENO=DB.DEGREENO)", " DISTINCT (CONVERT(NVARCHAR(16),D.DEGREENO) + '$' + CONVERT(NVARCHAR(16),B.BRANCHNO)) AS DEGREENO", "D.DEGREENAME + ' - ' + B.LONGNAME AS PROGRAM", "DB.COLLEGE_ID=" + Convert.ToInt32(ddlfaculty.SelectedValue) + "AND SESSIONNO=" + Convert.ToInt32(ddlsession.SelectedValue) + "", "");
            ddlSemester.Items.Clear();
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            txtTotStud.Text = "0";
            DivTotal.Visible = false;
      
        }
        else
        {
            ddlSemester.Items.Clear();
            lstProgram.Items.Clear();
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            txtTotStud.Text = "0";
            DivTotal.Visible = false;
         
        }
    }
    protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsession.SelectedValue != "0")
        {
            objCommon.FillDropDownList(ddlfaculty, "ACD_COLLEGE_MASTER CM INNER JOIN ACD_STUDENT_RESULT SR ON (CM.COLLEGE_ID=SR.COLLEGE_ID)", "DISTINCT CM.COLLEGE_ID", "COLLEGE_NAME", "SESSIONNO=" + ddlsession.SelectedValue, "COLLEGE_NAME");
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add("Please Select");
            lstProgram.Items.Clear();
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            txtTotStud.Text = "0";
            DivTotal.Visible = false;
        }
        else
        {

            ddlfaculty.Items.Clear();
            lstProgram.Items.Clear();
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add("Please Select");
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            txtTotStud.Text = "0";
            DivTotal.Visible = false;

        }
    }
    protected void lstProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        string DegreeNo = string.Empty;
        string BranchNo = string.Empty;
        string Program = string.Empty;
        if (lstProgram.SelectedValue == "0")
        {
            return;
        }
        foreach (ListItem items in lstProgram.Items)
        {
            if (items.Selected == true)
            {
                Program = items.Value;
                string[] splitValue;
                splitValue = Program.Split('$');

                DegreeNo += (splitValue[0].ToString()) + ',';
                BranchNo += (splitValue[1].ToString()) + ',';
            }
        }

        DegreeNo = DegreeNo.TrimEnd(',');
        BranchNo = BranchNo.TrimEnd(',');
        if (DegreeNo.ToString() != "" || DegreeNo.ToString() != string.Empty && BranchNo.ToString() != "" || BranchNo.ToString() != string.Empty)
        {
           
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT SR on(s.SEMESTERNO=sr.SEMESTERNO)", "DISTINCT s.SEMESTERNO", "SEMESTERNAME", " sr.SESSIONNO=" + ddlsession.SelectedValue + "AND SR.COLLEGE_ID=" + ddlfaculty.SelectedValue + "AND DEGREENO IN(" + DegreeNo.ToString() + ")AND BRANCHNO IN(" + BranchNo .ToString()+ ")", "s.SEMESTERNO");
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            txtTotStud.Text = "0";
            DivTotal.Visible = false;
        }
        else
        {
            ddlSemester.Items.Clear();
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            txtTotStud.Text = "0";
            DivTotal.Visible = false;
        }
    }
}
