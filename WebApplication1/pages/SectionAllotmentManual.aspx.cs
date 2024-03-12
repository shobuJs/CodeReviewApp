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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                PopulateDropDownList();
                btnSubmit.Enabled = false;
            }
            try
            {
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
            catch (Exception ex)
            {
               
            }
        }
        objCommon.SetLabelData("0");//for label
        objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
        
    }

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

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.BindListView();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.Clear();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            StudentController objSC = new StudentController();
            string studids = string.Empty;
            string sections = string.Empty;
            string rollnos = string.Empty;
            
            int admbatchno = Convert.ToInt32(ddlAdmBatch.SelectedValue);
        

            string[] Program = ddlProgram.SelectedValue.Split(',');
            int degreeno = Convert.ToInt32(Program[0]);             //Convert.ToInt32(ddlProgram.SelectedValue);
            int branchno = Convert.ToInt32(Program[1]);             //Convert.ToInt32(ddlBranch.SelectedValue);

            foreach (ListViewDataItem lvItem in lvStudents.Items)
            {
                
                if (Convert.ToInt32((lvItem.FindControl("ddlsec") as DropDownList).SelectedValue) > 0)
                {
                    studids += (lvItem.FindControl("cbRow") as CheckBox).ToolTip + "$";
                    sections += (lvItem.FindControl("ddlsec") as DropDownList).SelectedValue + "$";
                    rollnos += (lvItem.FindControl("txtRollNo") as TextBox).Text + "$";
                }
            }
        
            if (studids.Length <= 0 && sections.Length <= 0)
            {
                objCommon.DisplayMessage(this.updSection, "Please Select Student/Section", this.Page);
                return;
            }

            if (objSC.UpdateStudentSectionManual(studids, rollnos, sections, Convert.ToInt32(Session["userno"]), admbatchno, degreeno, branchno, (string.IsNullOrEmpty(ViewState["ipAddress"].ToString()) ? "" : ViewState["ipAddress"].ToString())) == Convert.ToInt32(CustomStatus.RecordUpdated))
            {
                this.BindListView();
                objCommon.DisplayMessage(this.updSection, "Section Alloted Successfully!!!", this.Page);
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
    private void PopulateDropDownList()
    {
        try
        {
            // objCommon.FillDropDownList(ddlSchemetype, "ACD_SCHEMETYPE", "SCHEMETYPENO", "SCHEMETYPE", "SCHEMETYPENO > 0", "SCHEMETYPENO");
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (D.DEGREENO = CD.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO >0 AND CD.UGPGOT IN (" +  Session["ua_section"] + ")","D.DEGREENO");
           // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT", "SEMESTERNO", "distinct semesterno", "SEMESTERNO > 0 AND degreeno=" + ddlDegree.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND ADMBATCH=" + ddlAdmBatch.SelectedValue + "", "SEMESTERNO");
            objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNO");
            objCommon.FillDropDownList(ddlCollege, "acd_college_master", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND COLLEGE_ID IN (" + Session["college_nos"] + ")", "COLLEGE_ID");
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
    private void BindListView()
    {
        try
        {
            string[] Program = ddlProgram.SelectedValue.Split(',');
            int degreeno = Convert.ToInt32(Program[0]);
            int branchno = Convert.ToInt32(Program[1]);            
            //Fill Student List
            hdfTot.Value = "0";
            txtTotStud.Text = "0";
            DataSet ds = null;


            ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_SECTION SC ON (S.SECTIONNO = SC.SECTIONNO)", "S.IDNO", "S.REGNO, S.STUDNAME, S.SECTIONNO,SC.SECTIONNAME,S.ROLLNO", "S.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND S.DEGREENO = " + degreeno + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " " + (rbRemaining.Checked == true ? " AND (ISNULL(S.SECTIONNO,0) = 0 or ROLLNO IS NULL or ROLLNO='')" : string.Empty) + " AND ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND isnull(ADMCAN,0)=0 AND isnull(CAN,0)=0 AND BRANCHNO=" + branchno, (rbRegNo.Checked == true ? "S.SECTIONNO,REGNO" : "S.SECTIONNO,rollno"));
            
            // else
                //ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_SECTION SC ON (S.SECTIONNO = SC.SECTIONNO) INNER JOIN ACD_SEM_PROMOTION P ON (S.IDNO = P.IDNO)", "S.IDNO", "S.REGNO,S.STUDNAME, S.SECTIONNO,SC.SECTIONNAME,S.ROLLNO", "S.BRANCHNO = " + ddlBranch.SelectedValue + " AND S.SEMESTERNO = " + ddlSemester.SelectedValue + (rbRemaining.Checked == true ? " AND (S.SECTIONNO IS NULL OR S.SECTIONNO = 0)" : string.Empty) + " AND SCHEME_TYPE=" + ddlSchemetype.SelectedValue + " AND ADMBATCH=" + ddlAdmBatch.SelectedValue, (rbRegNo.Checked == true ? "REGNO" : "rollno"));
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvStudents.DataSource = ds;
                    lvStudents.DataBind();
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
                }
            }
            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                hdfTot.Value = "0";
                objCommon.DisplayMessage(this.updSection, "No Students found for selected criteria!", this.Page);
                btnSubmit.Enabled = false;
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
    private void Clear()
    {
        // ddlSchemetype.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        ddlProgram.SelectedIndex = 0;
       // ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        txtTotStud.Text = "0";
        hdfTot.Value = "0";
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        btnSubmit.Enabled = false;
    }
    #endregion

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string[] Program = ddlProgram.SelectedValue.Split(',');
            int degreeno = Convert.ToInt32(Program[0]);
            int branchno = Convert.ToInt32(Program[1]);         
            //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            //objCommon.LookUp(ddlSemester, "ACD_STUDENT", "DISTINCT SEMESTERNO","SEMESTERNO > 0 AND degreeno=" + ddlDegree.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND ADMBATCH=" + ddlAdmBatch.SelectedValue + "", "SEMESTERNO");
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT A inner join acd_semester B on A.SEMESTERNO=b.SEMESTERNO", "DISTINCT A.SEMESTERNO", "SEMESTERNAME", "A.SEMESTERNO > 0 AND degreeno=" + degreeno + " AND BRANCHNO=" + branchno + " AND ADMBATCH=" + ddlAdmBatch.SelectedValue + "", "A.SEMESTERNO");
            lvStudents.DataSource = null;
            lvStudents.DataBind();
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

    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlsec = e.Item.FindControl("ddlsec") as DropDownList;
            DataSet ds = objCommon.FillDropDown("ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNAME");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTableReader dtr = ds.Tables[0].CreateDataReader();
                while (dtr.Read())
                {
                    ddlsec.Items.Add(new ListItem(dtr["SECTIONNAME"].ToString(), dtr["SECTIONNO"].ToString()));
                   
                }
            }

            ddlsec.SelectedValue = ddlsec.ToolTip;
            //if (ddlsec.SelectedValue == "0")
            //    ddlsec.Enabled = true;
            //else
            //    ddlsec.Enabled = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_SectionAllotment.lvStudents_ItemDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Session["usertype"].ToString() != "1")
            {
                string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());

                objCommon.FillDropDownList(ddlProgram, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO) INNER JOIN ACD_BRANCH BR ON (BR.BRANCHNO=B.BRANCHNO)", "DISTINCT CONVERT(NVARCHAR,D.DEGREENO)+','+CONVERT(NVARCHAR,B.BRANCHNO)", "(DEGREENAME+' - '+BR.LONGNAME) AS PROGRAM", "D.DEGREENO>0 AND B.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "");

            }
            else
            {
                objCommon.FillDropDownList(ddlProgram, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO) INNER JOIN ACD_BRANCH BR ON (BR.BRANCHNO=B.BRANCHNO)", "DISTINCT CONVERT(NVARCHAR,D.DEGREENO)+','+CONVERT(NVARCHAR,B.BRANCHNO)", "(DEGREENAME+' - '+BR.LONGNAME) AS PROGRAM", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlCollege.SelectedValue, "");
            }
            lvStudents.DataSource = null;
            lvStudents.DataBind();
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
}
