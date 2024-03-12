//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : BRANCH MASTER                                                        
// CREATION DATE : 14-MAY-2009                                                          
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class Academic_BranchEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MappingController objBC = new MappingController();
    Branch objBranch = new Branch();

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
                }     
                    
                int mul_col_flag = Convert.ToInt32(objCommon.LookUp("REFF", "MUL_COL_FLAG", "CollegeName='" + Page.Title + "'"));
                if (mul_col_flag == 0)
                {
                    objCommon.FillDropDownList(ddlCollegeName, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID = 1", "COLLEGE_ID");
                    ddlCollegeName.SelectedIndex = 1;
                    BindListView();
                }
                else
                {
                    objCommon.FillDropDownList(ddlCollegeName, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and isnull(ACTIVE,0)=1", "COLLEGE_ID");
                    ddlCollegeName.SelectedIndex = 0;
                }
            }
            // degreename
            PopulateDropDownList();
            objCommon.SetLabelData(ddlCollegeName.SelectedValue);
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // dynamic page header addded by sarang 09/07/2021
           // BindListView();
            ViewState["action"] = "add";
        }

        divMsg.InnerHtml = string.Empty;

    }

    private void PopulateDropDownList()
    {
        try
        {         
            objCommon.FillDropDownList(ddlDegreeName, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND C.DEGREENO>0","DEGREENO");
            objCommon.FillDropDownList(ddlBranchName, "ACD_BRANCH B LEFT JOIN ACD_AFFILIATED_UNIVERSITY U ON B.AFFILIATED_NO = U.AFFILIATED_NO", "(CONVERT(VARCHAR,BRANCHNO) + ','+ CONVERT(VARCHAR,ISNULL(B.AFFILIATED_NO,0))) AS BRANCHNO", "LONGNAME + '-' + ISNULL(AFFILIATED_SHORTNAME,'') AS LONGNAME", "BRANCHNO>0", "LONGNAME");
            //objCommon.FillDropDownList(ddlCollegeName, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            objCommon.FillDropDownList(ddlSection, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "UA_SECTION");
            objCommon.FillDropDownList(ddlClassification, "ACD_AREA_OF_INTEREST", "AREA_INT_NO", "AREA_INT_NAME", "AREA_INT_NO > 0 AND ISNULL(ACTIVE,0)=1", "AREA_INT_NO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Branch.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    //protected void btnDel_Click(object sender, ImageClickEventArgs e)   // Commented by swapnil thakare on dated 16-07-2021
    //{
    //    ImageButton btnDel = sender as ImageButton;
    //    ViewState["action"] = "delete";
    //    int srno = int.Parse(btnDel.CommandArgument);
    //    int output = objBC.deletebranchRecord(srno);
    //    BindListView();
    //    if (output != -99 && output != 99)
    //    {
    //        objCommon.DisplayMessage(updGradeEntry, " Record Deleted Successfully", this.Page);
    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage(updGradeEntry, " Record Is Not Deleted ", this.Page);
    //    }
    //}

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BranchEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BranchEntry.aspx");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            //if (Convert.ToInt32(rdoSpecilization.SelectedValue) == 1)  //added by swapnil thakare on dated 08-07-2021
            //{
            //    objBranch.IsSpecilization = 1;
            //}
            //else
            //{
            //    objBranch.IsSpecilization = 0;
            //}

            //if (chkActive.Checked == true)
            //{
            //    objBranch.Active = 1;
            //}
            //else
            //{
            //    objBranch.Active = 0;
            //}

            if(hfdStat.Value == "true")
                objBranch.Active = 1;
            else
                objBranch.Active = 0;
            
            objBranch.BranchNo = Convert.ToInt32(ddlBranchName.SelectedValue.Split(',')[0]);
            int Affiliated = Convert.ToInt32(ddlBranchName.SelectedValue.Split(',')[1]);
            //if (!txtIntake1.Text.Trim().Equals(string.Empty)) objBranch.Intake1 = Convert.ToInt32(txtIntake1.Text.Trim());
            //if (!txtIntake2.Text.Trim().Equals(string.Empty)) objBranch.Intake2 = Convert.ToInt32(txtIntake2.Text.Trim());
            //if (!txtIntake3.Text.Trim().Equals(string.Empty)) objBranch.Intake3 = Convert.ToInt32(txtIntake3.Text.Trim());
            //if (!txtIntake4.Text.Trim().Equals(string.Empty)) objBranch.Intake4 = Convert.ToInt32(txtIntake4.Text.Trim());
            //if (!txtIntake5.Text.Trim().Equals(string.Empty)) objBranch.Intake5 = Convert.ToInt32(txtIntake5.Text.Trim());
            //if (!txtIntake6.Text.Trim().Equals(string.Empty)) objBranch.Intake6 = Convert.ToInt32(txtIntake6.Text.Trim());
            objBranch.Duration = txtDuration.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToDouble(txtDuration.Text.Trim());
            objBranch.Code = txtCode.Text.Trim();
            objBranch.Ugpgot = Convert.ToInt32(ddlSection.SelectedValue);
            objBranch.DegreeNo = Convert.ToInt32(ddlDegreeName.SelectedValue);
            objBranch.DeptNo = Convert.ToInt32(ddlDept.SelectedValue);
            objBranch.CollegeCode = Session["colcode"].ToString();
            objBranch.CollegeID = Convert.ToInt32(ddlCollegeName.SelectedValue);

            objBranch.IsSpecilization = Convert.ToInt32(ddlClassification.SelectedValue);
            string DegreeName = txtModuleName.Text;

            //string Affiliated = "";
            //foreach (ListItem items in ddlAffiliated.Items)
            //{
            //    if (items.Selected == true)
            //    {
            //        Affiliated += items.Value + ',';

            //    }
            //}
            //if (Affiliated == string.Empty)
            //{
            //    objCommon.DisplayMessage(updGradeEntry, "Please Select Affiliated", this.Page);
            //    return;
            //}
            //Affiliated = Affiliated.Substring(0, Affiliated.Length - 1);

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add Branch Type
                    CustomStatus cs = (CustomStatus)objBC.AddBranchType(objBranch, Affiliated, DegreeName);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindListView();
                        Clear();
                        objCommon.DisplayMessage(updGradeEntry, "Record saved successfully", this.Page);
                        
                        //lvBranch.DataSource = null;
                        //lvBranch.DataBind();
                        //lvBranch.Visible = true;
                    }
                    else
                    {
                        objCommon.DisplayMessage(updGradeEntry, "Record Already Exists", this.Page);
                    }
                }
                else if (ViewState["action"].ToString().Equals("Edit"))
                {
                    CustomStatus cs = (CustomStatus)objBC.EditbranchRecord(Convert.ToInt32(ViewState["srno"]), Convert.ToDouble(txtDuration.Text), txtCode.Text, objBranch, Affiliated, DegreeName);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        Clear();
                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(updGradeEntry, "Record updated successfully", this.Page);
                        lvBranch.DataSource = null;
                        lvBranch.DataBind();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updGradeEntry, "Error...", this.Page);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "Branch.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server UnAvailable");
            throw;
        }
    }

    private void Clear()
    {
        ddlDegreeName.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
        ddlCollegeName.SelectedIndex = 0;
        ddlBranchName.SelectedIndex = 0;
        //txtIntake1.Text = string.Empty;
        //txtIntake2.Text = string.Empty;
        //txtIntake3.Text = string.Empty;
        //txtIntake4.Text = string.Empty;
        //txtIntake5.Text = string.Empty;
        //txtIntake6.Text = string.Empty;
        txtDuration.Text = string.Empty;
        txtCode.Text = string.Empty;
        ddlSection.SelectedIndex = 0;
        ddlDegreeName.Enabled = true;
        ddlDept.Enabled = true;
        ddlCollegeName.Enabled = true;
        ddlBranchName.Enabled = true;
        rdoSpecilization.ClearSelection();  // Added by swapnil Thakare on dated 09-07-2021
        //chkActive.Checked = false;
        ddlClassification.SelectedIndex = 0;
        txtModuleName.Text = string.Empty;
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objBC.GetAllBranchType(Convert.ToInt32(ddlCollegeName.SelectedValue), Convert.ToInt32(ddlDegreeName.SelectedValue));
          //  DataSet ds = objBC.GetAllBranchType(Convert.ToInt32(7), Convert.ToInt32(ddlDegreeName.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvBranch.Visible = true;
                lvBranch.DataSource = ds;
                lvBranch.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvBranch);//Set label 
                objCommon.SetListViewHeaderLabel(Convert.ToString(Request.QueryString["pageno"]), lvBranch);
            }
            else
            {
                lvBranch.Visible = false;
                lvBranch.DataSource = null;
                lvBranch.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Branch.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlCollegeName.SelectedValue == "0")
        {
            objCommon.DisplayMessage(updGradeEntry, "Please select College", this.Page);
            return;
        }
        //ShowReport("BranchMaster", "rptBatchMaster.rpt");
        ShowReport("BranchMaster", "rptBranch_mapping_new.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE="+ Session["colcode"].ToString() +",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegreeName.SelectedValue);// +",@P_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranchName.SelectedValue); //Commented by Irfan Shaikh on 20190413
            //url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(15) + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegreeName.SelectedValue) + ",@P_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranchName.SelectedValue) +",@P_COLLEGE_CODE =" + Convert.ToInt32(ddlCollegeName.SelectedValue);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updGradeEntry, this.updGradeEntry.GetType(), "controlJSScript", sb.ToString(), true);
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlCollegeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlCollegeName.SelectedIndex > 0)
        //{
            BindListView();
            objCommon.FillDropDownList(ddlDegreeName, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlCollegeName.SelectedValue + "", "DEGREENO");
            objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D, ACD_COLLEGE_DEPT C ", "D.DEPTNO", "D.DEPTNAME", "D.DEPTNO=C.DEPTNO AND C.DEPTNO >0 AND C.COLLEGE_ID=" + ddlCollegeName.SelectedValue + "", "DEPTNAME");    
            objCommon.SetLabelData(ddlCollegeName.SelectedValue);
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); 
        //}        
    }

    protected void ddlDegreeName_SelectedIndexChanged(object sender, EventArgs e)
    {  
        BindListView();
        //objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D, ACD_DEGREE DE,ACD_COLLEGE_DEPT C ", "D.DEPTNO", "D.DEPTNAME", "D.DEPTNO=C.DEPTNO AND DE.DEGREENO AND C.DEPTNO >0 AND C.COLLEGE_ID=" + ddlCollegeName.SelectedValue + "", "DEPTNAME"); 
        //objCommon.FillDropDownList(ddlDept,"ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", " DISTINCT D.DEPTNO", "D.DEPTNAME", "D.DEPTNO>0 AND B.DEGREENO = " + ddlDegreeName.SelectedValue, "D.DEPTNAME");
    }

    protected void btnedit_Click(object sender, ImageClickEventArgs e)
    {
       
        ImageButton btnEdit = sender as ImageButton;

        ViewState["action"] = "Edit";
        int srno = int.Parse(btnEdit.CommandArgument);
        ViewState["srno"] = srno;
        DataSet chkds = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH", "*,(CONVERT(VARCHAR,BRANCHNO) + ','+ CONVERT(VARCHAR,ISNULL(AFFILIATED_NO,0))) AS BRANCHNOS, isnull(ISSPECIALIZATION,0)as ISSPECIALIZATION1,ISNULL(ACTIVE,0) AS ACTIVE1,ISNULL(AREA_INT_NO,0) AS AREA_INT_NO1", "DEGREENO", "CDBNO=" + srno, string.Empty);
        if (chkds.Tables[0].Rows.Count > 0)
        {
            ddlCollegeName.SelectedValue = chkds.Tables[0].Rows[0]["college_id"].ToString();
            objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D, ACD_COLLEGE_DEPT C ", "D.DEPTNO", "D.DEPTNAME", "D.DEPTNO=C.DEPTNO AND C.DEPTNO >0 AND C.COLLEGE_ID=" + ddlCollegeName.SelectedValue + "", "DEPTNAME"); 
            ddlDegreeName.SelectedValue = chkds.Tables[0].Rows[0]["DEGREENO"].ToString();
            ddlDept.SelectedValue = chkds.Tables[0].Rows[0]["DEPTNO"].ToString();
            ddlBranchName.SelectedValue = chkds.Tables[0].Rows[0]["BRANCHNOS"].ToString();
            txtCode.Text = chkds.Tables[0].Rows[0]["code"].ToString().Trim();
            txtDuration.Text = chkds.Tables[0].Rows[0]["duration"].ToString();
            //txtIntake1.Text = chkds.Tables[0].Rows[0]["INTAKE1"].ToString();
            //txtIntake2.Text = chkds.Tables[0].Rows[0]["INTAKE2"].ToString();
            //txtIntake3.Text = chkds.Tables[0].Rows[0]["INTAKE3"].ToString();
            //txtIntake4.Text = chkds.Tables[0].Rows[0]["INTAKE4"].ToString();
            //txtIntake5.Text = chkds.Tables[0].Rows[0]["INTAKE5"].ToString();
            //txtIntake6.Text = chkds.Tables[0].Rows[0]["INTAKE6"].ToString();    // ADDED BY NARESH BEERLA ON 28012021 AS PER JITENDRA
            ddlSection.SelectedValue = chkds.Tables[0].Rows[0]["UGPGOT"].ToString().Trim();
            rdoSpecilization.SelectedValue = chkds.Tables[0].Rows[0]["ISSPECIALIZATION1"].ToString().Trim(); // ADDED BY SWAPNIL THAKARE ON 09-07-2021 
            if (chkds.Tables[0].Rows[0]["ACTIVE1"].ToString().Trim() == "1")
            {
                //chkActive.Checked = true;
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(true);", true);
            }
            else
            {
                //chkActive.Checked = false;
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(false);", true);
            }
            txtModuleName.Text = chkds.Tables[0].Rows[0]["DEGREE_FULL_NAME"].ToString();
            ddlClassification.SelectedValue = chkds.Tables[0].Rows[0]["AREA_INT_NO1"].ToString();  //ADDED BY SWAPNIL THAKARE ON DATED 28-07-2021
            ddlCollegeName.Enabled = false;
            ddlDegreeName.Enabled = false;            
        }
    }
}
