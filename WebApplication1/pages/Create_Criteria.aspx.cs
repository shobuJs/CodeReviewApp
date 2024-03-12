using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
public partial class Projects_Create_Criteria : System.Web.UI.Page
{
    CourseController objCourse = new CourseController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    #region PageLoad
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
                //Page Authorization
                this.CheckPageAuthorization();
                if (Session["usertype"].ToString() == "1" && Session["username"].ToString().ToUpper().Contains("PRINCIPAL"))
                {
                    // divShow.Visible=true;
                }
                else
                {
                    // divShow.Visible=false;
                }

                Page.Title = objCommon.LookUp("reff", "collegename", string.Empty);

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                }
                BindDropDownAlocation();
                BindDropDownCriteria();
                BindListViewRuleAllocation();



                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
            }
            BindListView();
            CriteriaRuleListBind();
            Session["CriteriaDetails"] = null;
            ViewState["CRITERIA_CRITERIA_NO"] = null;
            Session["CriteriaDetailsAdd"] = null;
            ViewState["CRITERIA_CRITERIA_NO_ADD"] = null;

            Session["CriteriaRuleDetails"] = null;
            ViewState["CRITERIA_CRIT_RULE_NO"] = null;
            ViewState["CRIT_NO"] = "edit";
            ViewState["CRITERule_NO"] = "edit";

        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Create_Criteria.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Create_Criteria.aspx");
        }
    }

    #endregion
    // Start Create Criteria Tab
    #region Start Create Criteria Tab
    protected void BindDropDownCriteria()
    {
      objCommon.FillDropDownList(ddlExamination, "ACD_ALTYPE", "ALTYPENO", "ALTYPENAME", "ALTYPENO>0", "ALTYPENAME");
        //objCommon.FillDropDownList(ddlSubject, "AL_COURSES", "DISTINCT ID", "AL_COURSES", "ID>0", "AL_COURSES");
        // objCommon.FillDropDownList(ddlMinGradeAny, "ACD_GRADE", "GRADENO", "GRADE", "GRADENO>0", "GRADE");
      objCommon.FillDropDownList(ddlMinPass, "ACD_QUALILEVEL", "QUALILEVELNO", "QUALILEVELNAME", "QUALILEVELNO>0", "QUALILEVELNO");
      objCommon.FillDropDownList(ddlPassess, "ACD_QUALILEVEL_OL", "QUALILEVELNO_OL", "QUALILEVELNAME_OL", "QUALILEVELNO_OL > 0 and QUALILEVELNO_OL !=11", "QUALILEVELNAME_OL");
      objCommon.FillDropDownList(ddlCriteria, "ACD_CRITERIA", " DISTINCT CRIT_NO", "CRITERIA_NAME", "CRIT_NO>0", "CRITERIA_NAME");

    }
    protected void ddlExamination_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlExamType.SelectedValue == "1")
        {
            objCommon.FillDropDownList(ddlSubject, "AL_COURSES", "DISTINCT ID", "AL_COURSES", "ALTYPENO=" + Convert.ToInt32(ddlExamination.SelectedValue), "AL_COURSES");
            objCommon.FillDropDownList(ddlMinGrade, "ACD_AL_GRADES", "ID", "GRADES", "ALTYPENO=" + Convert.ToInt32(ddlExamination.SelectedValue), "GRADES");
            objCommon.FillDropDownList(ddlgradeand, "ACD_AL_GRADES", "ID", "GRADES", "ALTYPENO=" + Convert.ToInt32(ddlExamination.SelectedValue), "GRADES");
            objCommon.FillDropDownList(ddlMinGradeAny, "ACD_AL_GRADES", "ID as GRADENO", "GRADES AS GRADE", "ALTYPENO=" + Convert.ToInt32(ddlExamination.SelectedValue), "GRADES");
        }
        if (ddlExamType.SelectedValue == "2")
        {
            objCommon.FillDropDownList(ddlSubject, "OL_COURSES", "DISTINCT ID", "OL_COURSES", "OLTYPENO="+ Convert.ToInt32(ddlExamination.SelectedValue), "OL_COURSES");
            objCommon.FillDropDownList(ddlMinGrade, "ACD_OL_GRADES", "ID", "GRADES", "ALTYPENO=" + Convert.ToInt32(ddlExamination.SelectedValue), "GRADES");
            objCommon.FillDropDownList(ddlgradeand, "ACD_OL_GRADES", "ID", "GRADES", "ALTYPENO=" + Convert.ToInt32(ddlExamination.SelectedValue), "GRADES");
           objCommon.FillDropDownList(ddlMinGradeAny, "ACD_OL_GRADES", "ID as GRADENO", "GRADES AS GRADE", "ALTYPENO=" + Convert.ToInt32(ddlExamination.SelectedValue), "GRADES");
        }
    }
    private DataTable GetEducationDetails()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("CRITERIA_NO", typeof(string)));
        dt.Columns.Add(new DataColumn("SUBJECT", typeof(string)));
        dt.Columns.Add(new DataColumn("GRADE", typeof(string)));
        dt.Columns.Add(new DataColumn("SUBJECTNO", typeof(string)));
        dt.Columns.Add(new DataColumn("GRADENO", typeof(string)));
        return dt;
    }
    private DataTable AddGetEducationDetails()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("CRITERIA_NO", typeof(string)));
        dt.Columns.Add(new DataColumn("ADDGRADE", typeof(string)));
        dt.Columns.Add(new DataColumn("ADDGRADENO", typeof(string)));
        dt.Columns.Add(new DataColumn("PASSESS", typeof(string)));
        dt.Columns.Add(new DataColumn("PASSESSNO", typeof(string)));
        return dt;
    }

    protected void btnAddEducation_Click(object sender, EventArgs e)
    {
        try
        {
  
            DataTable dt;
            if (Session["CriteriaDetails"] != null && ((DataTable)Session["CriteriaDetails"]) != null)
            {
                dt = ((DataTable)Session["CriteriaDetails"]);
                DataRow dr = dt.NewRow();
                
                if (Convert.ToString(ViewState["CRITERIA_CRITERIA_NO"]) == string.Empty || Convert.ToString(ViewState["CRITERIA_CRITERIA_NO"]) == "0")
                {
                    dr["CRITERIA_NO"] = 0;
                 
                }
                else
                {
                    dr["CRITERIA_NO"] = Convert.ToString(ViewState["CRITERIA_CRITERIA_NO"]);
                }

                dr["SUBJECT"] = ddlSubject.SelectedItem;
                dr["GRADE"] = ddlMinGrade.SelectedItem;
                dr["SUBJECTNO"] = ddlSubject.SelectedValue;
                dr["GRADENO"] = ddlMinGrade.SelectedValue;
 
                dt.Rows.Add(dr);
                Session["CriteriaDetails"] = dt;
                Panel3.Visible = true;
                lvlEducationDetails.DataSource = dt;
                lvlEducationDetails.DataBind();

                ddlSubject.SelectedIndex = 0;
                ddlMinGrade.SelectedIndex = 0;
                ViewState["CRITERIA_CRITERIA_NO"] = null;

            }
            else
            {
                dt = this.GetEducationDetails();
                DataRow dr = dt.NewRow();
                dr["CRITERIA_NO"] = 0;
                dr["SUBJECT"] = ddlSubject.SelectedItem;
                dr["GRADE"] = ddlMinGrade.SelectedItem;
                dr["SUBJECTNO"] = ddlSubject.SelectedValue;
                dr["GRADENO"] = ddlMinGrade.SelectedValue;

                dt.Rows.Add(dr);
                Session.Add("CriteriaDetails", dt);
                Panel3.Visible = true;
                lvlEducationDetails.DataSource = dt;
                lvlEducationDetails.DataBind();
                ddlSubject.SelectedIndex = 0;
                ddlMinGrade.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_Create_Criteria->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lnkEditEducation_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ViewState["anygrade"]) == null)
            {
                ViewState["anygrade"] = 0;
            }
                LinkButton lnk = sender as LinkButton;
                int id = Convert.ToInt32(lnk.CommandName);
                string CRITERIA_NO = "";
                CRITERIA_NO = Convert.ToString(lnk.CommandArgument);
                if (CRITERIA_NO == string.Empty || CRITERIA_NO == "0")
                {
                    DataTable dt;
                    if (Session["CriteriaDetails"] != null && ((DataTable)Session["CriteriaDetails"]) != null)
                    {
                        if (ddlSubject.SelectedValue == "0" && ddlMinGrade.SelectedValue == "0")
                        {
                            dt = ((DataTable)Session["CriteriaDetails"]);
                            DataRow dr = dt.Rows[id - 1];
                            ddlSubject.SelectedValue = dr["SUBJECTNO"].ToString();
                            ddlMinGrade.SelectedValue = dr["GRADENO"].ToString();
                            if (ddlCombinationType.SelectedValue == "2")
                            {
                                ddlMinGradeAny.SelectedValue = Convert.ToString(ViewState["anygrade"]);
                            }
                            dt.Rows.Remove(dr);
                            Session["CriteriaDetails"] = dt;
                            lvlEducationDetails.DataSource = dt;
                            lvlEducationDetails.DataBind();
                        }
                        else
                        {
                            objCommon.DisplayMessage(this, "Unable to edit record please Update previous edited record first !!!", this);
                        }
                    }
                }
                foreach (ListViewDataItem item in lvlEducationDetails.Items)
                {
                    if (ddlSubject.SelectedValue == "0" && ddlMinGrade.SelectedValue == "0")
                    {
                        DataTable dt;
                        dt = ((DataTable)Session["CriteriaDetails"]);

                        HiddenField hdfCRITERIA_NO = (HiddenField)item.FindControl("hdfCRITERIA_NO") as HiddenField;
                     
                        string CRIT_NO = "";
                        CRIT_NO = Convert.ToString(hdfCRITERIA_NO.Value);
                        ViewState["CRITERIA_CRITERIA_NO"] = CRIT_NO;
                        if (CRIT_NO == CRITERIA_NO)
                        {
                            DataRow dr = dt.Rows[id - 1];
                            ddlSubject.SelectedValue = dr["SUBJECTNO"].ToString();
                            ddlMinGrade.SelectedValue = dr["GRADENO"].ToString();
                            if (ddlCombinationType.SelectedValue == "2")
                            {
                                ddlMinGradeAny.SelectedValue = Convert.ToString(ViewState["anygrade"]);
                            }
                            dt.Rows.Remove(dr);
                            Session["CriteriaDetails"] = dt;
                            lvlEducationDetails.DataSource = dt;
                            lvlEducationDetails.DataBind();

                            //  ScriptManager.RegisterClientScriptBlock(updEdcutationalDetails, updEdcutationalDetails.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
                            break;
                        }
                    }
                    //else
                    //{
                    //    objCommon.DisplayMessage(this, "Unable to edit record please Update previous edited record first !!!", this);
                    //}
                }

               
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_Create_Criteria->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lnkDeleteEducation_Click(object sender, EventArgs e)
    {
        try
        {
           
                LinkButton lnk = sender as LinkButton;
                int id = Convert.ToInt32(lnk.CommandName);
                string srno = Convert.ToString(lnk.CommandArgument);
                if (srno == string.Empty || srno == "0")
                {
                    DataTable dt;
                    if (Session["CriteriaDetails"] != null && ((DataTable)Session["CriteriaDetails"]) != null)
                    {
                        if (ddlSubject.SelectedValue == "0" && ddlMinGrade.SelectedValue =="0")
                        {
                            dt = ((DataTable)Session["CriteriaDetails"]);
                            dt.Rows[id - 1].Delete();
                            dt.AcceptChanges();
                            Session["CriteriaDetails"] = dt;
                            lvlEducationDetails.DataSource = dt;
                            lvlEducationDetails.DataBind();
                            objCommon.DisplayMessage(this, "Record Deleted Successfully !!!", this);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this, "Unable to edit record please Update previous edited record first !!!", this);
                        }
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this, " Permanent Saved data Not Delete..!!!", this);  
                }
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_Create_Criteria->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmitCriteria_Click(object sender, EventArgs e)
    {
        string Criteria_Name = txtCriteriaName.Text;
        int AL_OL_TYPE = Convert.ToInt32(ddlExamType.SelectedValue);
        int Examination = Convert.ToInt32(ddlExamination.SelectedValue);
        int MinPass = Convert.ToInt32(ddlMinPass.SelectedValue);
        string Combination_Type = ddlCombinationType.SelectedItem.Text;
        int AnyMinGrade = Convert.ToInt32(ddlMinGradeAny.SelectedValue);
        string Grade = string.Empty;
        string AddGrade = string.Empty;
        string Passes = string.Empty;
        string Subject = string.Empty;

        DataTable dt = new DataTable();
        dt = this.GetEducationDetails();

        DataTable addt = new DataTable();
        addt = this.AddGetEducationDetails();
        int CRIT_NO = 0;

        if (ViewState["CRIT_NO"].ToString() != "edit")
        {
            CRIT_NO = Convert.ToInt32(ViewState["CRIT_NO"].ToString());

        }
        else
        {
            int CRITNo = Convert.ToInt32(objCommon.LookUp("ACD_CRITERIA", "isnull(max(CRIT_NO),0)", ""));
            CRIT_NO = CRITNo + 1;
        }
        foreach (ListViewDataItem dataitem in lvlEducationDetails.Items)
        {

            DataRow dr = dt.NewRow();
            HiddenField hdfCriteriaNo = (HiddenField)dataitem.FindControl("hdfCRITERIA_NO") as HiddenField;
            Label SubjectNo = (Label)dataitem.FindControl("lblSubjectNo") as Label;
            Label GradeNo = (Label)dataitem.FindControl("lblGradeNo") as Label;
            Label SubjectName = (Label)dataitem.FindControl("lblSubjectName") as Label;
            Label GradeName = (Label)dataitem.FindControl("lblGradeName") as Label;
            Grade += GradeNo + ",";
            Subject += SubjectNo + ",";
            dr["CRITERIA_NO"] = hdfCriteriaNo.Value;
            dr["SUBJECT"] = SubjectName.Text.Trim();
            dr["GRADE"] = GradeName.Text.Trim();
            dr["SUBJECTNO"] = SubjectNo.Text.Trim();
            dr["GRADENO"] = GradeNo.Text.Trim();
            dt.Rows.Add(dr);

        }
        foreach (ListViewDataItem dataitem in LvAndDetails.Items)
        {

            DataRow dr = addt.NewRow();
            HiddenField hdfCriteriaNo = (HiddenField)dataitem.FindControl("hdfCRITERIA_NO") as HiddenField;
            Label AddGradeNo = (Label)dataitem.FindControl("lblGradeNo") as Label;
            Label AddGradeName = (Label)dataitem.FindControl("lblGradeName") as Label;
            Label PasseName = (Label)dataitem.FindControl("lblPasseName") as Label;
            Label PasseNo = (Label)dataitem.FindControl("lblPasseNo") as Label;

            AddGrade += AddGradeNo + ",";
            Passes += PasseNo + ",";

            dr["CRITERIA_NO"] = hdfCriteriaNo.Value;
            dr["ADDGRADE"] = AddGradeName.Text.Trim();
            dr["ADDGRADENO"] = AddGradeNo.Text.Trim();
            dr["PASSESS"] = PasseName.Text.Trim();
            dr["PASSESSNO"] = PasseNo.Text.Trim();
            addt.Rows.Add(dr);

        }
        if (Combination_Type.ToString() != "Any")
        {
            if (Grade.ToString() == "" && Subject.ToString() == "")
            {
                objCommon.DisplayMessage(this, "Please Add At List One Specific Subject And Grade ", this.Page);
                return;
            }
            else if (AddGrade.ToString() == "" && Passes.ToString() == "")
            {
                objCommon.DisplayMessage(this, "Please Add At List One Specific Grade And Passes ", this.Page);
                return;
            }
        }
        if (lvlEducationDetails.Items.Count > 1)
        {
            foreach (ListViewDataItem dataitem in lvlEducationDetails.Items)
            {
                HiddenField hfsrno2 = (HiddenField)dataitem.FindControl("hfsrno");
                foreach (ListViewDataItem dataitem2 in lvlEducationDetails.Items)
                {
                    HiddenField hfsrno = (HiddenField)dataitem2.FindControl("hfsrno");
                    Label lblSubjectNo = dataitem2.FindControl("lblSubjectNo") as Label;
                    if (lblSubjectNo.Text.ToString() == (dataitem.FindControl("lblSubjectNo") as Label).Text.ToString() &&
                        Convert.ToInt32(hfsrno.Value.ToString()) > 1 && Convert.ToInt32(hfsrno.Value.ToString()) != Convert.ToInt32(hfsrno2.Value.ToString()))
                    {
                        objCommon.DisplayMessage(this.Page, "You Cannot add Same Subject more than one time.", this.Page);
                        return;
                    }
                }
            }
        }
        if (LvAndDetails.Items.Count > 1)
        {
            foreach (ListViewDataItem dataitem in LvAndDetails.Items)
            {
                HiddenField hfsrno2 = (HiddenField)dataitem.FindControl("hfsrno");
                foreach (ListViewDataItem dataitem2 in LvAndDetails.Items)
                {
                    HiddenField hfsrno = (HiddenField)dataitem2.FindControl("hfsrno");
                    Label lblSubjectNo = dataitem2.FindControl("lblGradeNo") as Label;
                    if (lblSubjectNo.Text.ToString() == (dataitem.FindControl("lblGradeNo") as Label).Text.ToString() &&
                        Convert.ToInt32(hfsrno.Value.ToString()) > 1 && Convert.ToInt32(hfsrno.Value.ToString()) != Convert.ToInt32(hfsrno2.Value.ToString()))
                    {
                        objCommon.DisplayMessage(this.Page, "You Cannot add Same Subject more than one time.", this.Page);
                        return;
                    }
                }
            }
        }
        if (ViewState["editcri"] == null)
        {
            ViewState["editcri"] = 0;
        }
        if (Convert.ToInt32(ViewState["editcri"]) != 1)
        {
            int count = Convert.ToInt32(objCommon.LookUp("ACD_CRITERIA", "COUNT(1)", "CRITERIA_NAME='" + Criteria_Name + "'"));
            if (count > 0)
            {
                objCommon.DisplayMessage(this, "Criteria Name Already Exists.", this.Page);
                return;
            }
        }
        CustomStatus cs = (CustomStatus)objCourse.InsertCritetriaName(Criteria_Name, AL_OL_TYPE, Examination, MinPass, Combination_Type, AnyMinGrade, dt, addt, CRIT_NO);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this, "Record Saved Successfully.", this.Page);
            ViewState["CRIT_NO"] = "edit";
            ddlExamType.Enabled = true;
            ddlExamination.Enabled = true;
            objCommon.FillDropDownList(ddlCriteria, "ACD_CRITERIA", " DISTINCT CRIT_NO", "CRITERIA_NAME", "CRIT_NO>0", "CRITERIA_NAME");
            BindListView();
            // BindListViewRuleAllocation();
            if (Combination_Type.ToString() != "Any")
            {
                dt = ((DataTable)Session["CriteriaDetails"]);
                dt.Rows[0].Delete();
                dt.AcceptChanges();
                Session["CriteriaDetails"] = dt;
                Panel3.Visible = true;
                lvlEducationDetails.DataSource = dt;
                lvlEducationDetails.DataBind();
                dt = null;

                dt = ((DataTable)Session["CriteriaDetailsAdd"]);
                dt.Rows[0].Delete();
                dt.AcceptChanges();
                Session["CriteriaDetailsAdd"] = dt;
                Panel6.Visible = true;
                LvAndDetails.DataSource = dt;
                LvAndDetails.DataBind();
                dt = null;
            }

            Session["CriteriaDetails"] = null;
            Session["CriteriaDetailsAdd"] = null;
            ViewState["editcri"] = null;
            Panel3.Visible = false;
            lvlEducationDetails.DataSource = null;
            lvlEducationDetails.DataBind();
            Panel6.Visible = false;
            LvAndDetails.DataSource = null;
            LvAndDetails.DataBind();
            Clear();
            ddlCombinationType_SelectedIndexChanged(new object(), new EventArgs());

            dt = null;
        }
        else if (cs.Equals(CustomStatus.TransactionFailed))
        {
            objCommon.DisplayMessage(this, "Failed To Save Record ", this.Page);
            ddlExamType.Enabled = true;
            ddlExamination.Enabled = true;
        }
        else
        {
            objCommon.DisplayMessage(this, "Please Select At List One New Module And Mapping Status ", this.Page);

        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        int id = Convert.ToInt32(lnk.CommandName);
        string CRITERIA_NO = "";
        CRITERIA_NO = Convert.ToString(lnk.CommandArgument);
        ViewState["CRIT_NO"] = CRITERIA_NO.ToString();
        DataSet ds = objCommon.FillDropDown("ACD_CRITERIA", "CRIT_NO,CRITERIA_NO", "CRITERIA_NAME,AL_OL_TYPE,ALTYPENO,QUALILEVELNO,COMBINATION_TYPE,SUBJECT_NO,MIN_GRADE,ANY_MIN_GRADE,(CASE WHEN COMBINATION_TYPE='Specific' THEN 1 WHEN COMBINATION_TYPE='Any' THEN 2 WHEN COMBINATION_TYPE='Both' THEN 3 END) AS COMBINATION", "CRIT_NO=" + CRITERIA_NO, "");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            ddlExamType.Enabled = false;
            ddlExamination.Enabled = false;
            DataRow drs = ds.Tables[0].Rows[0];
            txtCriteriaName.Text = drs["CRITERIA_NAME"].ToString();
            ddlExamType.SelectedValue = drs["AL_OL_TYPE"].ToString();
            ddlExamination.SelectedValue = drs["ALTYPENO"].ToString();
            ddlMinPass.SelectedValue = drs["QUALILEVELNO"].ToString();
            ddlCombinationType.SelectedValue = drs["COMBINATION"].ToString();
           
            int Al_Ol_Type = Convert.ToInt32(drs["AL_OL_TYPE"].ToString());

            ddlExamination_SelectedIndexChanged(new object(), new EventArgs());
            ddlMinGradeAny.SelectedValue = drs["ANY_MIN_GRADE"].ToString();
            ViewState["anygrade"] = drs["ANY_MIN_GRADE"].ToString();
            if (Convert.ToInt32(ViewState["anygrade"]) == null)
            {
                ViewState["anygrade"] = 0;
            }
            ViewState["editcri"] = 1;
            if (Al_Ol_Type == 1)
            {
                DataSet dsS = objCommon.FillDropDown("ACD_CRITERIA CR INNER JOIN AL_COURSES C ON(CR.SUBJECT_NO=C.ID)INNER JOIN ACD_AL_GRADES AG ON(AG.ID=CR.MIN_GRADE)", "SUBJECT_NO AS SUBJECTNO, MIN_GRADE AS GRADENO,GRADES AS GRADE", "AL_COURSES AS SUBJECT,CR.CRITERIA_NO", "CRIT_NO=" + CRITERIA_NO, "");
                Panel3.Visible = true;
                lvlEducationDetails.DataSource = dsS;
                lvlEducationDetails.DataBind();
                DataTable dt = dsS.Tables[0];
                Session["CriteriaDetails"] = dt;

                DataSet dsAdd = objCommon.FillDropDown("ACD_CRITERIA_DATA CR INNER JOIN ACD_AL_GRADES AG ON(AG.ID=CR.ADD_GRADE)INNER JOIN ACD_QUALILEVEL_OL PASS ON CR.PASSESS=PASS.QUALILEVELNO_OL", "PASSESS AS PASSESSNO,QUALILEVELNAME_OL AS PASSESS", "ADD_GRADE AS ADDGRADENO,GRADES AS ADDGRADE,CRITERIA_NO", "CRIT_NO=" + CRITERIA_NO, "");
                Panel6.Visible = true;
                LvAndDetails.DataSource = dsAdd;
                LvAndDetails.DataBind();
                DataTable dtAdd = dsAdd.Tables[0];
                Session["CriteriaDetailsAdd"] = dtAdd; 
            }
            else
            {
                DataSet dsS = objCommon.FillDropDown("ACD_CRITERIA CR INNER JOIN OL_COURSES C ON(CR.SUBJECT_NO=C.ID)INNER JOIN ACD_AL_GRADES AG ON(AG.ID=CR.MIN_GRADE)", "SUBJECT_NO AS SUBJECTNO", "MIN_GRADE AS GRADENO,GRADES AS GRADE,OL_COURSES AS SUBJECT,CRITERIA_NO", "CRIT_NO=" + CRITERIA_NO, "");
                Panel3.Visible = true;
                lvlEducationDetails.DataSource = dsS;
                lvlEducationDetails.DataBind();   
                DataTable dt =(dsS).Tables[0];           
                Session["CriteriaDetails"] = dt;

                DataSet dsAdd = objCommon.FillDropDown("ACD_CRITERIA_DATA CR INNER JOIN ACD_AL_GRADES AG ON(AG.ID=CR.ADD_GRADE)INNER JOIN ACD_QUALILEVEL_OL PASS ON CR.PASSESS=PASS.QUALILEVELNO_OL", "PASSESS AS PASSESSNO,QUALILEVELNAME_OL AS PASSESS", "GRADES AS ADDGRADE,ADD_GRADE AS ADDGRADENO,CRITERIA_NO", "CRIT_NO=" + CRITERIA_NO, "");
                Panel6.Visible = true;
                LvAndDetails.DataSource = dsAdd;
                LvAndDetails.DataBind();
                DataTable dtAdd = dsAdd.Tables[0];
                Session["CriteriaDetailsAdd"] = dtAdd;
              
            }
            }
        ddlCombinationType_SelectedIndexChanged(new object(), new EventArgs());
    }
    protected void btnCancelCriteria_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());   
    }
    protected void Clear()
    {
        txtCriteriaName.Text = "";
        ddlExamType.SelectedValue = "0";
        ddlExamination.SelectedValue = "0";
        ddlCombinationType.SelectedValue = "0";
        ddlMinGrade.SelectedValue = "0";
        ddlMinGradeAny.SelectedValue = "0"; ;
        ddlMinPass.SelectedValue = "0";
        ddlPassess.SelectedValue = "0";
        ddlgradeand.SelectedValue = "0";

        lvlEducationDetails.DataSource = null;
        lvlEducationDetails.DataBind();

        LvAndDetails.DataSource = null;
        LvAndDetails.DataBind();
        
    }
    protected void BindListView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_CRITERIA", "DISTINCT CRIT_NO ", "CRITERIA_NAME,(CASE WHEN AL_OL_TYPE=1 THEN 'A/L' WHEN AL_OL_TYPE=2 THEN 'O/L' END) AS EXA_TYPE", "", "");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvCriteriaRule.DataSource = ds;
                lvCriteriaRule.DataBind();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_Create_Criteria->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlCombinationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCombinationType.SelectedValue == "1")
        {
            DivSpecificSub.Visible = true;
            DivForany.Visible = false;
        }
        else if(ddlCombinationType.SelectedValue == "2")
        {
            DivSpecificSub.Visible = false;
            DivForany.Visible = true; ;
        }
        else if (ddlCombinationType.SelectedValue == "3")
        {
            DivSpecificSub.Visible = true;
            DivForany.Visible = true;
        }
        else
        {
            DivSpecificSub.Visible = false;
            DivForany.Visible = false;
        }

    }
#endregion
    // End Create_Criteria Tab

    // Start Create_Rule Tab
    #region Start Create_Rule Tab
    private DataTable GetCriteriaRule()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("CRIT_RULE_NO", typeof(string)));
        dt.Columns.Add(new DataColumn("CRITERIANAME", typeof(string)));
        dt.Columns.Add(new DataColumn("AND_OR_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("CRITERIANO", typeof(string)));
        dt.Columns.Add(new DataColumn("AND_OR_NO", typeof(string)));
        dt.Columns.Add(new DataColumn("CRITERIA_PLUSNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("CRITERIA_PLUSNO", typeof(string)));
        return dt;
    }
    protected void btnAddcriteRule_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt;
            if (Session["CriteriaRuleDetails"] != null && ((DataTable)Session["CriteriaRuleDetails"]) != null)
            {
                if (ddlCriteriaPlus.SelectedValue != "0")
                {
                    if (ddlAndOrRule.SelectedValue == "0")
                    
                    {
                        objCommon.DisplayMessage(this, "Please Select AND/OR !!!", this);
                        return;
                    }
                }
                dt = ((DataTable)Session["CriteriaRuleDetails"]);
                DataRow dr = dt.NewRow();
                if (Convert.ToString(ViewState["CRITERIA_CRIT_RULE_NO"]) == string.Empty || Convert.ToString(ViewState["CRITERIA_CRIT_RULE_NO"]) == "0")
                {
                    dr["CRIT_RULE_NO"] = 0;

                }
                else
                {
                    dr["CRIT_RULE_NO"] = Convert.ToString(ViewState["CRITERIA_CRIT_RULE_NO"]);
                }

                dr["CRITERIANAME"] = ddlCriteria.SelectedItem;
                dr["CRITERIANO"] = ddlCriteria.SelectedValue;
                dr["AND_OR_NAME"] = ddlAndOrRule.SelectedItem;
                dr["AND_OR_NO"] = ddlAndOrRule.SelectedValue;
                dr["CRITERIA_PLUSNAME"] =ddlCriteriaPlus.SelectedItem;
                dr["CRITERIA_PLUSNO"] = ddlCriteriaPlus.SelectedValue;

                dt.Rows.Add(dr);
                Session["CriteriaRuleDetails"] = dt;
                Panel2.Visible = true;
                lvCriteriaRulename.DataSource = dt;
                lvCriteriaRulename.DataBind();

                ddlCriteria.SelectedValue = "0";
                ddlAndOrRule.SelectedValue = "0";
                ddlCriteriaPlus.SelectedValue = "0";
                ViewState["EDUCATION_CRIT_RULE_NO "] = null;
            }
            else
            {
                if (ddlCriteriaPlus.SelectedValue != "0")
                {
                    if (ddlAndOrRule.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this, "Please Select AND/OR !!!", this);
                        return;
                    }
                }
                dt = this.GetCriteriaRule();
                DataRow dr = dt.NewRow();
                dr["CRIT_RULE_NO"] = 0;

                dr["CRITERIANAME"] = ddlCriteria.SelectedItem;
                dr["CRITERIANO"] = ddlCriteria.SelectedValue;
                dr["AND_OR_NAME"] = ddlAndOrRule.SelectedItem;
                dr["AND_OR_NO"] = ddlAndOrRule.SelectedValue;
                dr["CRITERIA_PLUSNAME"] = ddlCriteriaPlus.SelectedItem;
                dr["CRITERIA_PLUSNO"] = ddlCriteriaPlus.SelectedValue;

                dt.Rows.Add(dr);
                Session.Add("CriteriaRuleDetails", dt);
                Panel2.Visible = true;
                lvCriteriaRulename.DataSource = dt;
                lvCriteriaRulename.DataBind();

                ddlCriteria.SelectedValue = "0";
                ddlAndOrRule.SelectedValue = "0";
                ddlCriteriaPlus.SelectedValue = "0";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_Create_Criteria->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lnkEditEducation_Click1(object sender, EventArgs e)
    {
        try
        {

            LinkButton lnk = sender as LinkButton;
            int id = Convert.ToInt32(lnk.CommandName);
            string CRITERIA_RULE_NO = "";
            CRITERIA_RULE_NO = Convert.ToString(lnk.CommandArgument);
            if (CRITERIA_RULE_NO == string.Empty || CRITERIA_RULE_NO == "0")
            {
                DataTable dt;
                if (Session["CriteriaRuleDetails"] != null && ((DataTable)Session["CriteriaRuleDetails"]) != null)
                {
                    if (ddlCriteria.SelectedValue == "0" && ddlAndOrRule.SelectedValue == "0" && ddlCriteriaPlus.SelectedValue == "0")
                    {
                        dt = ((DataTable)Session["CriteriaRuleDetails"]);
                        DataRow dr = dt.Rows[id - 1];
                        ddlCriteria.SelectedValue = dr["CRITERIANO"].ToString();
                        ddlCriteria_SelectedIndexChanged(new object(), new EventArgs());
                        ddlAndOrRule.SelectedValue = dr["AND_OR_NO"].ToString();
                        ddlCriteriaPlus.SelectedValue = dr["CRITERIA_PLUSNO"].ToString();
                        dt.Rows.Remove(dr);
                        Session["CriteriaRuleDetails"] = dt;
                        Panel2.Visible = true;
                        lvCriteriaRulename.DataSource = dt;
                        lvCriteriaRulename.DataBind();

                        // ScriptManager.RegisterClientScriptBlock(updEdcutationalDetails, updEdcutationalDetails.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);

                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Unable to edit record please Update previous edited record first !!!", this);
                    }
                }
            }
            else
            {
                foreach (ListViewDataItem item in lvCriteriaRulename.Items)
                {
                    if (ddlCriteria.SelectedValue == "0" && ddlAndOrRule.SelectedValue == "0" && ddlCriteriaPlus.SelectedValue == "0")
                    {
                        DataTable dt;
                        dt = ((DataTable)Session["CriteriaRuleDetails"]);

                        HiddenField hdfCRITERIA_NO = (HiddenField)item.FindControl("hdfCRIT_RULE_NO") as HiddenField;

                        string CRIT_NO = "";
                        CRIT_NO = Convert.ToString(hdfCRITERIA_NO.Value);
                        ViewState["CRITERIA_CRIT_RULE_NO"] = CRIT_NO;
                        if (CRIT_NO == CRITERIA_RULE_NO)
                        {
                            DataRow dr = dt.Rows[id - 1];
                            ddlCriteria.SelectedValue = dr["CRITERIANO"].ToString();
                            ddlCriteria_SelectedIndexChanged(new object(), new EventArgs());
                            ddlAndOrRule.SelectedValue = dr["AND_OR_NO"].ToString();

                            ddlCriteriaPlus.SelectedValue = dr["CRITERIA_PLUSNO"].ToString();
                            dt.Rows.Remove(dr);
                            Session["CriteriaRuleDetails"] = dt;
                            Panel2.Visible = true;
                            lvCriteriaRulename.DataSource = dt;
                            lvCriteriaRulename.DataBind();

                            //  ScriptManager.RegisterClientScriptBlock(updEdcutationalDetails, updEdcutationalDetails.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
                            break;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Unable to edit record please Update previous edited record first !!!", this);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_Create_Criteria->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lnkDeleteEducation_Click1(object sender, EventArgs e)
    {
        try
        {

            LinkButton lnk = sender as LinkButton;
            int id = Convert.ToInt32(lnk.CommandName);
            string RuleNo = Convert.ToString(lnk.CommandArgument);
            if (RuleNo == string.Empty || RuleNo == "0")
            {
                DataTable dt;
                if (Session["CriteriaRuleDetails"] != null && ((DataTable)Session["CriteriaRuleDetails"]) != null)
                {
                    if (ddlCriteria.SelectedValue == "0" && ddlAndOrRule.SelectedValue == "0" && ddlCriteriaPlus.SelectedValue == "0")
                    {
                        dt = ((DataTable)Session["CriteriaRuleDetails"]);
                        dt.Rows[id - 1].Delete();
                        dt.AcceptChanges();
                        Session["CriteriaRuleDetails"] = dt;
                        Panel2.Visible = true;
                        lvCriteriaRulename.DataSource = dt;
                        lvCriteriaRulename.DataBind();
                        objCommon.DisplayMessage(this, "Record Deleted Successfully !!!", this);
                        //if (lvlEducationDetails.Items.Count == 0)
                        //BindUserData();
                        //BindListView();
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Unable to edit record please Update previous edited record first !!!", this);
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(this, " Permanent Saved data Not Delete..!!!", this);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_Create_Criteria->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCriteria.SelectedValue != "0")
        {
            if (Session["CriteriaRuleDetails"] == null)
            {
                Panel2.Visible = false;
                lvCriteriaRulename.DataSource = null;
                lvCriteriaRulename.DataBind();
            }
            int Crit_RuleNo = Convert.ToInt32(ddlCriteria.SelectedValue);
            objCommon.FillDropDownList(ddlCriteriaPlus, "ACD_CRITERIA", "DISTINCT CRIT_NO", "CRITERIA_NAME", "CRIT_NO NOT IN(" + Crit_RuleNo + ")", "CRITERIA_NAME");
        }
        else
        {
         
            ddlCriteriaPlus.SelectedValue = "0";
        }
    }
    protected void btnSubmitRule_Click(object sender, EventArgs e)
    {
        try
        {
            string CritRuleName = txtRuleName.Text;
            string CriteriaNo = string.Empty;
            string AndOrRuleNo = string.Empty;
            string CriteriaPlusNo = string.Empty;
            DataTable dt = new DataTable();
            dt = this.GetCriteriaRule();

            int CRIT_RULE_NO = 0;

            if (ViewState["CRITERule_NO"].ToString() != "edit")
            {
                CRIT_RULE_NO = Convert.ToInt32(ViewState["CRITERule_NO"].ToString());
              
            }
            else
            {
                int CRITNo = Convert.ToInt32(objCommon.LookUp("ACD_CRITERIA_RULES", "isnull(max(CRITE_RULENO),0)", ""));
                CRIT_RULE_NO = CRITNo + 1;
                int count = Convert.ToInt32(objCommon.LookUp("ACD_CRITERIA_RULES", "COUNT(1)", "CRITERIA_RULE_NAME='" + txtRuleName.Text + "'"));
                if (count > 0)
                {
                    objCommon.DisplayMessage(this, "Rule Name Already Exists.", this.Page);
                    return;
                }
            }
            foreach (ListViewDataItem dataitem in lvCriteriaRulename.Items)
            {

                DataRow dr = dt.NewRow();
                HiddenField hdfCRIT_RULE_NO = (HiddenField)dataitem.FindControl("hdfCRIT_RULE_NO") as HiddenField;
                Label lblCriteriaNo = (Label)dataitem.FindControl("lblCriteriaNo") as Label;
                Label lblAndOrRuleNo = (Label)dataitem.FindControl("lblAndOrRuleNo") as Label;
                Label lblCriteriaPlusNo = (Label)dataitem.FindControl("lblCriteriaPlusNo") as Label;
                Label lblCriteriaName = (Label)dataitem.FindControl("lblCriteriaName") as Label;
                Label lblAndOrRuleName = (Label)dataitem.FindControl("lblAndOrRuleName") as Label;
                Label lblCriteriaPlusName = (Label)dataitem.FindControl("lblCriteriaPlusName") as Label;
                CriteriaNo += lblCriteriaNo.Text + ",";
                AndOrRuleNo += lblAndOrRuleNo.Text + ",";
                CriteriaPlusNo += lblCriteriaPlusNo.Text + ",";
                dr["CRIT_RULE_NO"] = hdfCRIT_RULE_NO.Value;
                dr["CRITERIANAME"] = lblCriteriaName.Text.Trim();
                dr["AND_OR_NAME"] = lblAndOrRuleName.Text.Trim();
                dr["CRITERIA_PLUSNAME"] = lblCriteriaPlusName.Text.Trim();
                dr["CRITERIANO"] = lblCriteriaNo.Text.Trim();

                dr["AND_OR_NO"] = lblAndOrRuleNo.Text.Trim();

                dr["CRITERIA_PLUSNO"] = lblCriteriaPlusNo.Text.Trim();

                dt.Rows.Add(dr);

            }

            if (CriteriaNo.ToString() == "" && AndOrRuleNo.ToString() == "" && CriteriaPlusNo.ToString() == "")
            {
                objCommon.DisplayMessage(this, "Please Add At List One Criteria ", this.Page);
                return;
            }
        
            CustomStatus cs = (CustomStatus)objCourse.InsertCriteriaRuleName(CritRuleName, dt, CRIT_RULE_NO);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this, "Record Saved Successfully.", this.Page);
                ViewState["CRITERule_NO"] = "edit";
                CriteriaRuleListBind();
                BindDropDownAlocation();
                txtRuleName.Text = "";
                txtRuleName.Enabled = true;
                Panel2.Visible = false;
                lvCriteriaRulename.DataSource = null;
                lvCriteriaRulename.DataBind();

                Session["CriteriaRuleDetails"] = null;
            }
            else if (cs.Equals(CustomStatus.TransactionFailed))
            {
                objCommon.DisplayMessage(this, "Failed To Save Record ", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this, "Please Select At List One New Module And Mapping Status ", this.Page);
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_Create_Criteria->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        
       
    }
    protected void btnCancelRule_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());   
        txtRuleName.Enabled = true;
        txtRuleName.Text = "";
        Panel2.Visible = false;
        lvCriteriaRulename.DataSource = null;
        lvCriteriaRulename.DataBind();
        ddlCriteria.SelectedValue = "0";
        ddlCriteriaPlus.SelectedValue = "0";
        ddlAndOrRule.SelectedValue = "0";
        Session["CriteriaRuleDetails"] = null;
    }

    protected void CriteriaRuleListBind()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_CRITERIA_RULES", "distinct CRITE_RULENO AS CRIT_RULE_NO ", "CRITERIA_RULE_NAME", "CRITE_RULENO>0", "CRITE_RULENO desc");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvListRuleBind.DataSource = ds;
                lvListRuleBind.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_Create_Criteria->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lnkEditEducation_Click2(object sender, EventArgs e)
    {
        try
        {
            ddlCriteria.SelectedValue = "0";
            ddlCriteriaPlus.SelectedValue = "0";
            ddlAndOrRule.SelectedValue = "0";
            LinkButton lnk = sender as LinkButton;
            int id = Convert.ToInt32(lnk.CommandName);
            string CRITERule_NO = "";
            CRITERule_NO = Convert.ToString(lnk.CommandArgument);
            ViewState["CRITERule_NO"] = CRITERule_NO.ToString();
            DataSet ds = objCourse.GetRuleCreateData(Convert.ToInt32(CRITERule_NO));
            //DataSet ds = (objCommon.FillDropDown("ACD_CRITERIA_RULES CR INNER JOIN ACD_CRITERIA  C ON(CR.CRITERIA_RULE_NO=C.CRIT_NO)INNER JOIN ACD_CRITERIA CRT ON(CR.CRITERIA_RULE_NO= CRT.CRIT_NO)"
            //    , "DISTINCT  CR.CRITE_RULENO,CRITE_NO AS CRIT_RULE_NO",
            //    " CR.CRITERIA_RULE_NAME,CR.CRITERIA_RULE_NO AS CRITERIANO,AND_OR AS AND_OR_NO ,CR.CRITERIA_NO AS CRITERIA_PLUSNO ,C.CRITERIA_NAME AS CRITERIANAME,CRT.CRITERIA_NAME AS CRITERIA_PLUSNAME, (CASE WHEN AND_OR=1 THEN 'AND' WHEN AND_OR=2 THEN 'OR' END) AS AND_OR_NAME", "CR.CRITE_RULENO=" + CRITERule_NO, ""));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow drs = ds.Tables[0].Rows[0];

                txtRuleName.Text = drs["CRITERIA_RULE_NAME"].ToString();
                txtRuleName.Enabled = false;
                Panel2.Visible = true;
                lvCriteriaRulename.DataSource = ds;
                lvCriteriaRulename.DataBind();
                DataTable dt = ds.Tables[0];
                Session["CriteriaRuleDetails"] = dt;
            }
            ViewState["editrule"] = 1;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_Create_Criteria->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
#endregion
    // End Create_Rule Tab
    //Start Rule_Allocation Tab
    #region Start Rule_Allocation Tab
    protected void BindDropDownAlocation()
    {
        objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
        objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        objCommon.FillDropDownList(ddlStudyLevel, "ACD_UA_SECTION", "UA_SECTION", "SHORTNAME", "UA_SECTION NOT IN("+"''"+")", "");
        objCommon.FillDropDownList(ddlRule, "ACD_CRITERIA_RULES", "DISTINCT CRITE_RULENO", "CRITERIA_RULE_NAME", "CRITE_RULENO>0", "CRITERIA_RULE_NAME");
    }
    protected void ddlStudyLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlProgram, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_UA_SECTION US ON(CDB.UGPGOT = US.UA_SECTION) INNER JOIN ACD_BRANCH B ON(CDB.BRANCHNO = B.BRANCHNO) inner join ACD_DEGREE D ON(CDB.DEGREENO = D.DEGREENO)", "(CONVERT(NVARCHAR(16),D.DEGREENO) + ',' + CONVERT(NVARCHAR(16),B.BRANCHNO)) AS DEGREENO", "D.DEGREENAME + ' - ' + B.LONGNAME AS PROGRAM", "B.BRANCHNO>0 AND CDB.COLLEGE_ID=" + Convert.ToInt32(ddlFaculty.SelectedValue) + "AND CDB.UGPGOT=" + Convert.ToInt32(ddlStudyLevel.SelectedValue) + "", "B.LONGNAME");
    }
    protected void btnSubmitAllocation_Click(object sender, EventArgs e)
    {
        try
        {
            int AdmBatch = Convert.ToInt32(ddlIntake.SelectedValue);
            int College_id = Convert.ToInt32(ddlFaculty.SelectedValue);
            int UgPg = Convert.ToInt32(ddlStudyLevel.SelectedValue);

            string[] splitValue;
            splitValue = ddlProgram.SelectedValue.Split(',');
            int DegreeNo = Convert.ToInt32(splitValue[0]);
            int Branchno = Convert.ToInt32(splitValue[1]);
            int CriteriaRule = Convert.ToInt32(ddlRule.SelectedValue);
            int CriteAlloNo = 0;
            if (ViewState["CRITERule_NO"].ToString() != "edit")
            {
                CriteAlloNo = Convert.ToInt32(ViewState["CRITERule_NO"].ToString());
            }

            CustomStatus cs = (CustomStatus)objCourse.InsertCriteriaRuleAllocation(AdmBatch, College_id, UgPg, DegreeNo, Branchno, CriteriaRule, CriteAlloNo);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this, "Record Saved Successfully.", this.Page);
                BindListViewRuleAllocation();
                ddlFaculty.SelectedValue = "0";
                ddlStudyLevel.SelectedValue = "0";
                ddlIntake.SelectedValue = "0";
                ddlProgram.SelectedValue = "0";
                ddlRule.SelectedValue = "0";
                objCommon.FillDropDownList(ddlRule, "ACD_CRITERIA_RULES", "DISTINCT CRITE_RULENO", "CRITERIA_RULE_NAME", "CRITE_RULENO>0", "CRITERIA_RULE_NAME");
            }
            else if (cs.Equals(CustomStatus.TransactionFailed))
            {
                objCommon.DisplayMessage(this, "Failed To Save Record ", this.Page);
            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(this, "Record Already Exists", this.Page);
                return;
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this, "Record Update Successfully.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_Create_Criteria->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindListViewRuleAllocation()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_CRITERIA_RULES_ALLOCATION CRA INNER JOIN ACD_ADMBATCH A ON(A.BATCHNO=CRA.ADMBATCH) INNER JOIN ACD_COLLEGE_MASTER  CM ON (CM.COLLEGE_ID=CRA.COLLEGE_ID) INNER JOIN ACD_UA_SECTION UA ON(UA.UA_SECTION=CRA.UG_PG)INNER JOIN ACD_CRITERIA_RULES CR ON(CR.CRITE_NO=CRA.CRITERIA_RULE_NO) INNER JOIN ACD_DEGREE D ON(D.DEGREENO=CRA.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=CRA.BRANCHNO)",
                "DISTINCT CRITERIA_ALLC_NO", "A.BATCHNAME,CM.COLLEGE_NAME,UA.SHORTNAME,CRITERIA_RULE_NAME,D.CODE + ' - ' + B.LONGNAME AS PROGRAM", "", "CRITERIA_ALLC_NO desc");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvRuleAllocation.DataSource = ds;
                lvRuleAllocation.DataBind();

            }
            else
            {
                lvRuleAllocation.DataSource = null;
                lvRuleAllocation.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_Create_Criteria->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lnkEditEducation_Click3(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            int id = Convert.ToInt32(lnk.CommandName);
            string Crite_Allo_no = "";
            Crite_Allo_no = Convert.ToString(lnk.CommandArgument);
            ViewState["CRITERule_NO"] = Crite_Allo_no.ToString();
            DataSet ds = objCommon.FillDropDown("ACD_CRITERIA_RULES_ALLOCATION", "*,(CONVERT(NVARCHAR(16),DEGREENO) + ',' + CONVERT(NVARCHAR(16),BRANCHNO)) AS PROGRAM", "", "CRITERIA_ALLC_NO=" + Convert.ToInt32(Crite_Allo_no), "");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow drs = ds.Tables[0].Rows[0];

                ddlFaculty.Enabled = false;
                ddlStudyLevel.Enabled = false;
                ddlIntake.Enabled = false;
                ddlProgram.Enabled = false;
                ddlFaculty.SelectedValue = drs["COLLEGE_ID"].ToString();
                ddlStudyLevel.SelectedValue = drs["UG_PG"].ToString();
                ddlStudyLevel_SelectedIndexChanged(new object(), new EventArgs());
                ddlIntake.SelectedValue = drs["ADMBATCH"].ToString();
                ddlProgram.SelectedValue = drs["PROGRAM"].ToString();
                ddlRule.SelectedValue = drs["CRITERIA_RULE_NO"].ToString();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_Create_Criteria->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnCancelAllocation_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());   
        ddlFaculty.SelectedValue = "0";
        ddlStudyLevel.SelectedValue = "0";
        ddlIntake.SelectedValue = "0";
        ddlProgram.SelectedValue = "0";
        ddlRule.SelectedValue = "0";
        ViewState["CRITERule_NO"] = "edit";
        ddlIntake.Enabled = true;
        ddlFaculty.Enabled = true;
        ddlStudyLevel.Enabled = true;
        ddlProgram.Enabled = true;
    }
    //End Rule_Allocation Tab
    #endregion


    protected void btnAddSecond_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dt;
            if (Session["CriteriaDetailsAdd"] != null && ((DataTable)Session["CriteriaDetailsAdd"]) != null)
            {
                dt = ((DataTable)Session["CriteriaDetailsAdd"]);
                DataRow dr = dt.NewRow();

                if (Convert.ToString(ViewState["CRITERIA_CRITERIA_NO_ADD"]) == string.Empty || Convert.ToString(ViewState["CRITERIA_CRITERIA_NO_ADD"]) == "0")
                {
                    dr["CRITERIA_NO"] = 0;

                }
                else
                {
                    dr["CRITERIA_NO"] = Convert.ToString(ViewState["CRITERIA_CRITERIA_NO_ADD"]);
                }
                dr["ADDGRADE"] = ddlgradeand.SelectedItem;
                dr["ADDGRADENO"] = ddlgradeand.SelectedValue;
                dr["PASSESS"] = ddlPassess.SelectedItem;
                dr["PASSESSNO"] = ddlPassess.SelectedValue;

                dt.Rows.Add(dr);
                Session["CriteriaDetailsAdd"] = dt;
                Panel6.Visible = true;

                LvAndDetails.DataSource = dt;
                LvAndDetails.DataBind();

                ddlPassess.SelectedValue = "0";
                ddlgradeand.SelectedValue = "0";
                ViewState["CRITERIA_CRITERIA_NO_ADD"] = null;

            }
            else
            {
                dt = this.AddGetEducationDetails();
                DataRow dr = dt.NewRow();
                dr["CRITERIA_NO"] = 0;
                dr["ADDGRADE"] = ddlgradeand.SelectedItem;
                dr["ADDGRADENO"] = ddlgradeand.SelectedValue;
                dr["PASSESS"] = ddlPassess.SelectedItem;
                dr["PASSESSNO"] = ddlPassess.SelectedValue;

                dt.Rows.Add(dr);
                Session.Add("CriteriaDetailsAdd", dt);
                Panel6.Visible = true;
                LvAndDetails.DataSource = dt;
                LvAndDetails.DataBind();
                ddlPassess.SelectedValue = "0";
                ddlgradeand.SelectedValue = "0";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_Create_Criteria->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lnkDeleteEducationAdd_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton lnk = sender as LinkButton;
            int id = Convert.ToInt32(lnk.CommandName);
            string srno = Convert.ToString(lnk.CommandArgument);
            if (srno == string.Empty || srno == "0")
            {
                DataTable dt;
                if (Session["CriteriaDetailsAdd"] != null && ((DataTable)Session["CriteriaDetailsAdd"]) != null)
                {
                    if (ddlgradeand.SelectedValue == "0" && ddlPassess.SelectedValue == "0")
                    {
                        dt = ((DataTable)Session["CriteriaDetailsAdd"]);
                        dt.Rows[id - 1].Delete();
                        dt.AcceptChanges();
                        Session["CriteriaDetailsAdd"] = dt;
                        LvAndDetails.DataSource = dt;
                        LvAndDetails.DataBind();
                        objCommon.DisplayMessage(this, "Record Deleted Successfully !!!", this);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Unable to edit record please Update previous edited record first !!!", this);
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(this, " Permanent Saved data Not Delete..!!!", this);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_Create_Criteria->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lnkEditEducationAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ViewState["anygrade"]) == null)
            {
                ViewState["anygrade"] = 0;
            }
            LinkButton lnk = sender as LinkButton;
            int id = Convert.ToInt32(lnk.CommandName);
            string CRITERIA_NO = "";
            CRITERIA_NO = Convert.ToString(lnk.CommandArgument);
            if (CRITERIA_NO == string.Empty || CRITERIA_NO == "0")
            {
                DataTable dt;
                if (Session["CriteriaDetailsAdd"] != null && ((DataTable)Session["CriteriaDetailsAdd"]) != null)
                {
                    if (ddlgradeand.SelectedValue == "0" && ddlPassess.SelectedValue == "0")
                    {
                        dt = ((DataTable)Session["CriteriaDetailsAdd"]);
                        DataRow dr = dt.Rows[id - 1];
                        ddlPassess.SelectedValue = dr["PASSESSNO"].ToString();
                        ddlgradeand.SelectedValue = dr["ADDGRADENO"].ToString();
                        if (ddlCombinationType.SelectedValue == "2")
                        {
                            ddlMinGradeAny.SelectedValue = Convert.ToString(ViewState["anygrade"]);
                        }

                        dt.Rows.Remove(dr);
                        Session["CriteriaDetailsAdd"] = dt;
                        LvAndDetails.DataSource = dt;
                        LvAndDetails.DataBind();
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Unable to edit record please Update previous edited record first !!!", this);
                    }
                }
            }
            foreach (ListViewDataItem item in LvAndDetails.Items)
            {
                if (ddlPassess.SelectedValue == "0" && ddlgradeand.SelectedValue == "0")
                {
                    DataTable dt;
                    dt = ((DataTable)Session["CriteriaDetailsAdd"]);

                    HiddenField hdfCRITERIA_NO = (HiddenField)item.FindControl("hdfCRITERIA_NO") as HiddenField;

                    string CRIT_NO = "";
                    CRIT_NO = Convert.ToString(hdfCRITERIA_NO.Value);
                    ViewState["CRITERIA_CRITERIA_NO_ADD"] = CRIT_NO;
                    if (CRIT_NO == CRITERIA_NO)
                    {
                        DataRow dr = dt.Rows[id - 1];
                       
                        ddlPassess.SelectedValue = dr["PASSESSNO"].ToString();
                        ddlExamination_SelectedIndexChanged(new object(), new EventArgs());
                        ddlgradeand.SelectedValue = dr["ADDGRADENO"].ToString();
                        if (ddlCombinationType.SelectedValue == "2")
                        {
                            ddlMinGradeAny.SelectedValue = Convert.ToString(ViewState["anygrade"]);
                        }
                        dt.Rows.Remove(dr);
                        Session["CriteriaDetailsAdd"] = dt;
                        LvAndDetails.DataSource = dt;
                        LvAndDetails.DataBind();
                        break;
                    }
                }
                //else
                //{
                //    objCommon.DisplayMessage(this, "Unable to edit record please Update previous edited record first !!!", this);
                //}
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_Create_Criteria->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
      
            ddlProgram.Items.Clear();
            ddlProgram.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlStudyLevel.SelectedIndex = 0;
      
        
    }
}