//======================================================================================
// PROJECT NAME  : SLIIT                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : ProgramRuleCreation.aspx.cs                                      
// CREATION DATE : 29/12/2021                                                      
// CREATED BY    : Roshan Patil    
// PAGE DESC     :                                  
// MODIFIED DATE : 04/01/2022                                                                     
// MODIFIED DESC :                                                                     
//====================================================================================== 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class ProgramRuleCreation : System.Web.UI.Page
{
    Common objCommon = new Common();
    CourseController objCourse = new CourseController();
    Course objc = new Course();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Branch objBranch = new Branch();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    #region Page Load
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
                this.CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                HiddenField2.Value = "0";
                hdndeptno.Value = "0";

            }
            BindListViewRuleAllocation();
            BindListViewRuleCreation();
            ListViewBindBridging();
            PopulateDropDownRuleCreation();
            PopulateDropDownRuleAllocation();
            BindListViewBridging();
            DropDownBindBridgingAllocation();
            BindListViewBridgingAllocation();
            ViewState["action"] = "add";

            objCommon.SetLabelData("0");
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
        }
    }
    #endregion
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Exam_Mapping.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Exam_Mapping.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void Clear()
    {
        ddlALType.SelectedIndex = 0;
    }
    protected void PopulateDropDownRuleCreation()
    {
        objCommon.FillListBox(ddlALType, "ACD_ALTYPE", "ALTYPENO", "ALTYPENAME", "ALTYPENO>0", "ALTYPENAME");
        objCommon.FillDropDownList(ddlStreamAL, "ACD_STREAM", "STREAMNO", "STREAMNAME", "STREAMNO>0", "STREAMNAME");
        objCommon.FillListBox(ddlSubjectAL, " AL_COURSES ", "DISTINCT AL_SHORTNAME", "AL_COURSES", "ID>0", "AL_SHORTNAME");

        objCommon.FillDropDownList(ddlMinGradeAL, " ACD_AL_GRADES ", "DISTINCT GRADES", "GRADES AS GRADE", "ID>0", "GRADES");

        objCommon.FillDropDownList(ddlMaxGradeAL, " ACD_AL_GRADES ", "DISTINCT GRADES", "GRADES AS GRADE ", "ID>0", "GRADES");
        objCommon.FillDropDownList(ddlMinGradeOL, " ACD_AL_GRADES ", "DISTINCT GRADES", "GRADES AS GRADE ", "ID>0", "GRADES");
        objCommon.FillDropDownList(ddlMaxGradeOL, " ACD_AL_GRADES ", "DISTINCT GRADES", "GRADES AS GRADE ", "ID>0", "GRADES");
        objCommon.FillListBox(ddlSubjectOL, " OL_COURSES ", "DISTINCT OL_SHORTNAME", "OL_COURSES ", "ID>0", "OL_SHORTNAME");
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        int chkAptitutes = 0;
        int chkInterviews = 0;
        string AlType = string.Empty;
        string ALSubject = string.Empty;
        string OlSubject = string.Empty;

        string RollName = string.Empty;
        if (txtRuleName.Text == "")
        {
            objCommon.DisplayMessage(updRule, "Please Enter Rule Name", this.Page);
        }
        else
        {
            RollName = txtRuleName.Text;
        }

        int Active = 0;
        if (hfdStat.Value == "true")
        {
            Active = 1;
        }

        if (HiddenField1.Value == "true")
        {
            chkAptitutes = 1;
        }
        if (HiddenField3.Value == "true")
        {
            chkInterviews = 1;
        }
        int StreamOL = Convert.ToInt32(ddlStreamOL.SelectedIndex);
        foreach (ListItem items in ddlALType.Items)
        {
            if (items.Selected == true)
            {
                AlType += items.Value + ',';

            }
        }
        if (AlType == string.Empty)
        {
            objCommon.DisplayMessage(updRule, "Please Select Al Type", this.Page);
            return;
        }
        AlType = AlType.Substring(0, AlType.Length - 1);

        int StreamAL = Convert.ToInt32(ddlStreamAL.SelectedValue);
        int MinCourseAL = Convert.ToInt32(ddlMinCourseAl.SelectedValue);
        string MinGradesAL = ddlMinGradeAL.SelectedItem.Text;
        string ALSubj = ddlSubjectAL.SelectedValue;

        // int MinGradesAL = Convert.ToInt32(ddlMinGradeAL.SelectedValue);
        int MaxCourseAL = Convert.ToInt32(ddlMaxCourseAL.SelectedValue);
        //int MaxGradesAL = Convert.ToInt32(ddlMaxGradeAL.SelectedValue);
        string MaxGradesAL = ddlMaxGradeAL.SelectedValue;

        foreach (ListItem items in ddlSubjectAL.Items)
        {
            if (items.Selected == true)
            {
                ALSubject += items.Value + ',';

            }
        }
        if (ALSubject == string.Empty)
        {
            objCommon.DisplayMessage(updRule, "Please Select Subject", this.Page);
            return;
        }
        ALSubject = ALSubject.Substring(0, ALSubject.Length - 1);


        int MinCourseOL = Convert.ToInt32(ddlMinCourseOL.SelectedValue);
        string MinGradesOL = ddlMinGradeOL.SelectedValue;
        int MaxCourseOL = Convert.ToInt32(ddlMaxCourseOl.SelectedValue);
        string MaxGradesOL = ddlMaxGradeOL.SelectedValue;

        foreach (ListItem items in ddlSubjectOL.Items)
        {
            if (items.Selected == true)
            {
                OlSubject += items.Value + ',';

            }
        }

        if (OlSubject.ToString() == "")
        {
            OlSubject = OlSubject.Substring(0, OlSubject.Length - 0);
        }
        else
        {
            OlSubject = OlSubject.Substring(0, OlSubject.Length - 1);
        }


        if (hdndeptno.Value != null)
        {
            if (hdndeptno.Value.ToString().Equals("0"))
            {

                CustomStatus cs = (CustomStatus)objCourse.AddRuleName(RollName, chkAptitutes, chkInterviews, AlType, StreamAL, MinCourseAL, MinGradesAL, MaxCourseAL, MaxGradesAL, ALSubject, StreamOL, MinCourseOL, MinGradesOL, MaxCourseOL, MaxGradesOL, OlSubject, Active);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.updRule, "Record Saved Successfully.", this.Page);
                    BindListViewRuleCreation();
                    DropDown();
                    ClearAll();
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(this.updRule, "Failed To Save Record ", this.Page);
                    BindListViewRuleCreation();
                }
            }
            else
            {
                if (hdndeptno.Value != null)
                {

                    int Courseruleno = 0;
                    Courseruleno = Convert.ToInt32(hdndeptno.Value);
                    CustomStatus css = 0;
                    css = (CustomStatus)objCourse.UpdateRuleCreation(Courseruleno, RollName, chkAptitutes, chkInterviews, AlType, StreamAL, MinCourseAL, MinGradesAL, MaxCourseAL, MaxGradesAL, ALSubject, StreamOL, MinCourseOL, MinGradesOL, MaxCourseOL, MaxGradesOL, OlSubject, Active);
                    if (css.Equals(CustomStatus.RecordUpdated))
                    {
                        hdndeptno.Value = "0";

                        objCommon.DisplayMessage(this.updRule, "Record Update Successfully.", this.Page);
                        BindListViewRuleCreation();
                        DropDown();
                        ClearAll();
                        return;

                    }
                    else
                    {

                        hdndeptno.Value = "0";
                        objCommon.DisplayMessage(this.updRule, "Failed To Save Record ", this.Page);
                        BindListViewRuleCreation();
                        Clear();

                    }

                }
            }
        }

    }

    protected void ClearAll()
    {
        txtRuleName.Text = "";
        ddlALType.SelectedValue = null;
        ddlStreamAL.SelectedValue = null;
        ddlMinCourseAl.SelectedValue = null;
        ddlMinGradeAL.SelectedValue = null;
        ddlMaxGradeAL.SelectedValue = null;
        ddlMaxCourseAL.SelectedValue = null;

        ddlMaxCourseOl.SelectedValue = null;
        ddlMaxGradeOL.SelectedValue = null;
        ddlMinCourseOL.SelectedValue = null;
        ddlMinGradeOL.SelectedValue = null;

        ddlSubjectAL.SelectedValue=null;
        ddlSubjectOL.SelectedValue=null;


    }
    protected void btneditRuleCreation_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlALType.SelectedValue = null;
            ImageButton btnEditS = sender as ImageButton;
            int COURSERULENO = int.Parse(btnEditS.CommandArgument);
            //   Label1.Text = string.Empty;
            // ViewState["ALLOCATIONNO"] = ALLOCATIONNO;
            hdndeptno.Value = int.Parse(btnEditS.CommandArgument).ToString();
            ShowRuleDetails(COURSERULENO);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ProgramRuleCreation.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ShowRuleDetails(int COURSERULENO)
    {
        try
        {
            
            DataSet ds = objCommon.FillDropDown("ACD_COURSE_RULE", "*","COURSERULENO", "COURSERULENO=" + COURSERULENO, string.Empty);
            if (ds.Tables[0].Rows.Count > 0)
            {
               
                txtRuleName.Text= ds.Tables[0].Rows[0]["RULENAME"].ToString();

                 string[] couser = Convert.ToString(ds.Tables[0].Rows[0]["ALTYPENAME"]).Split(',');

                foreach (string s in couser)
                {
                    foreach (ListItem item in ddlALType.Items)
                    {
                        if (s == item.Value)
                        {
                            item.Selected = true;
                            break;
                        }
                        
                    }
                }
                string  aptitude = ds.Tables[0].Rows[0]["APTITUDE_TEST"].ToString().Trim();
                string interview = ds.Tables[0].Rows[0]["INTERVIEW_TEST"].ToString().Trim();
                string status = ds.Tables[0].Rows[0]["STATUS"].ToString().Trim();

                if (status == "1")
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page,GetType(), "Src", "SetStat(true);", true);
                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page,GetType(), "Src", "SetStat(false);", true);
                }

                if (aptitude == "1")
                {

                    ScriptManager.RegisterClientScriptBlock(updRule, updRule.GetType(), "Src", "SetStat1(true);", true);
                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(updRule, updRule.GetType(), "Src", "SetStat1(false);", true);
                }

                if (interview == "1")
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, HiddenField3.GetType(), "Src", "SetStat2(true);", true);
                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, HiddenField3.GetType(), "Src", "SetStat2(false);", true);
                }

              
               
                 ddlStreamAL.SelectedValue = ds.Tables[0].Rows[0]["ALSTREAMNAME"].ToString();
                 ddlMinCourseAl.SelectedValue = ds.Tables[0].Rows[0]["ALMIN_COURSE"].ToString();
                //ddlDiscipline_SelectedIndexChanged(new object(), new EventArgs());
                ddlMinGradeAL.SelectedValue = ds.Tables[0].Rows[0]["ALMIN_GRADES"].ToString();
                ddlMaxCourseAL.SelectedValue = ds.Tables[0].Rows[0]["ALMAX_COURSE"].ToString();
                ddlMaxGradeAL.SelectedValue = ds.Tables[0].Rows[0]["ALMAX_GRADES"].ToString();

                ddlSubjectAL.SelectedValue = null;
                ddlSubjectOL.SelectedValue = null;
                string[] subjectal = Convert.ToString(ds.Tables[0].Rows[0]["AL_SUBJECT"]).Split(',');

                foreach (string s in subjectal)
                {
                    foreach (ListItem item in ddlSubjectAL.Items)
                    {
                        if (s == item.Value)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
               
               // ddlStreamOL.SelectedValue = ds.Tables[0].Rows[0]["OLSTREAMNAME"].ToString();
                ddlMinCourseOL.SelectedValue = ds.Tables[0].Rows[0]["OLMIN_COURSE"].ToString();
                //ddlDiscipline_SelectedIndexChanged(new object(), new EventArgs());
                ddlMinGradeOL.SelectedValue = ds.Tables[0].Rows[0]["OLMIN_GRADES"].ToString();
                ddlMaxCourseOl.SelectedValue = ds.Tables[0].Rows[0]["OLMAX_COURSE"].ToString();
                ddlMaxGradeOL.SelectedValue = ds.Tables[0].Rows[0]["OLMAX_GRADES"].ToString();


              
                string[] subjectOL = Convert.ToString(ds.Tables[0].Rows[0]["OL_SUBJECT"]).Split(',');

                foreach (string s in subjectOL)
                {
                    foreach (ListItem item in ddlSubjectOL.Items)
                    {
                        if (s == item.Value)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
              
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ProgramRuleCreation.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }


    protected void BindListViewRuleCreation()
    {
        try
        {

            DataSet ds = objCourse.GetAllRuleCreation();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvProgram.DataSource = ds;
                lvProgram.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ProgramRuleCreation.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //Rule Allocation

    protected void PopulateDropDownRuleAllocation()
    {
        objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVE=" + 1, "BATCHNO DESC");
        objCommon.FillDropDownList(ddlDiscipline, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "UA_SECTIONNAME");
        //   objCommon.FillDropDownList(ddlProgram, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMENO>0", "SCHEMENAME");
        
        objCommon.FillListBox(ddlRulename, "ACD_COURSE_RULE CR inner join ACD_ALTYPE A ON(A.ALTYPENO IN(SELECT CAST(VALUE AS CHAR) FROM DBO.SPLIT(CR.ALTYPENAME, ',')))", "COURSERULENO", "A.ALTYPENAME+ ' - ' + CR.RULENAME AS RULENAME", "COURSERULENO>0", "COURSERULENO");
  

  //  objCommon.FillListBox(ddlRulename, "ACD_COURSE_RULE", "COURSERULENO", "RULENAME", "COURSERULENO>0", "COURSERULENO");
    }
    protected void ddlDiscipline_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlProgram, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_UA_SECTION US ON(CDB.UGPGOT = US.UA_SECTION) INNER JOIN ACD_BRANCH B ON(CDB.BRANCHNO = B.BRANCHNO) inner join ACD_DEGREE D ON(CDB.DEGREENO = D.DEGREENO)", "(CONVERT(NVARCHAR(16),D.DEGREENO) + ',' + CONVERT(NVARCHAR(16),B.BRANCHNO)) AS DEGREENO", "D.DEGREENAME + ' - ' + B.LONGNAME AS PROGRAM", "B.BRANCHNO>0 AND CDB.UGPGOT=" + ddlDiscipline.SelectedValue, "B.LONGNAME");
    }
    private void BindListViewRuleAllocation()

    {
        try
        {
            //DataSet ds = objCommon.FillDropDown("ACD_COURSE_RULE_ALLOCATION CRA INNER JOIN ACD_ADMBATCH AD ON(AD.BATCHNO=CRA.INTAKENO) INNER JOIN ACD_UA_SECTION UA ON(UA.UA_SECTION = CRA.UA_SECTION) INNER JOIN ACD_SCHEME S ON(S.SCHEMENO = CRA.SCHEMENO)INNER JOIN ACD_COURSE_RULE CR ON(CR.COURSERULENO = CRA.COURSERULE)",
            //     "ALLOCATIONNO", " AD.BATCHNAME,UA.UA_SECTIONNAME,S.SCHEMENAME,CR.RULENAME", string.Empty, "ALLOCATIONNO");
            DataSet ds = objCourse.GetAllRuleAllocation();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCollegeDetails.DataSource = ds;
                lvCollegeDetails.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ProgramRuleCreation.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSaveClick_Click(object sender, EventArgs e)
    {
        string RuleName = string.Empty;
        foreach (ListItem items in ddlRulename.Items)
        {
            if (items.Selected == true)
            {
                RuleName += items.Value + ',';

            }
        }
        //if (RuleName == string.Empty)
        //{
        //    objCommon.DisplayMessage(updRule, "Please Select Mode", this.Page);
        //    return;
        //}
        RuleName = RuleName.Substring(0, RuleName.Length - 1);
        string[] splitValue;
        splitValue = ddlProgram.SelectedValue.Split(',');
        if (HiddenField2.Value != null)
        {
            if (HiddenField2.Value.ToString().Equals("0"))
            {
                //string[] splitValue;
                //splitValue = ddlProgram.SelectedValue.Split(',');
                CustomStatus cs = (CustomStatus)objCourse.AddRuleAllocation(Convert.ToInt32(ddlIntake.SelectedValue), Convert.ToInt32(ddlDiscipline.SelectedValue), Convert.ToInt32(splitValue[0]), Convert.ToInt32(splitValue[1]), RuleName);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.updPanel2, "Record Saved Successfully.", this.Page);
                    BindListViewRuleAllocation();
                    clearAllo();
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(this.updPanel2, "Failed To Save Record ", this.Page);
                }
            }
            else
            {

                if (HiddenField2.Value != null)
                {

                    int AllocationNo = 0;
                    AllocationNo = Convert.ToInt32(HiddenField2.Value);
                    CustomStatus css = 0;
                    css = (CustomStatus)objCourse.UpdateRuleAllocation(AllocationNo, Convert.ToInt32(ddlIntake.SelectedValue), Convert.ToInt32(ddlDiscipline.SelectedValue), Convert.ToInt32(splitValue[0]), Convert.ToInt32(splitValue[1]), RuleName);
                    if (css.Equals(CustomStatus.RecordUpdated))
                    {
                        HiddenField2.Value = "0";

                        objCommon.DisplayMessage(this.updPanel2, "Record Updated Successfully!", this.Page);
                        BindListViewRuleAllocation();
                        clearAllo();
                        Clear();

                    }
                    else
                    {
                       
                        HiddenField2.Value = "0";
                        BindListViewRuleAllocation();
                        Clear();

                    }

                }
            }
        }
    }
    protected void btnedit_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ALLOCATIONNO = int.Parse(btnEdit.CommandArgument);
            //   Label1.Text = string.Empty;
           // ViewState["ALLOCATIONNO"] = ALLOCATIONNO;
            HiddenField2.Value = int.Parse(btnEdit.CommandArgument).ToString();
            ShowDetails(ALLOCATIONNO);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ProgramRuleCreation.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void ShowDetails(int ALLOCATIONNO)
    {
       // string AllocationNo = string.Empty;
        try
        {
            string Intake = string.Empty;
            string Descipline = string.Empty;
            string Program = string.Empty;
            string Rules = string.Empty;
                                                           
            DataSet ds = objCommon.FillDropDown(" ACD_COURSE_RULE_ALLOCATION", "*,(CONVERT(VARCHAR,DEGREENO) + ',' + CONVERT(VARCHAR,BRANCHNO)) AS BRANCHNAME", "ALLOCATIONNO", "ALLOCATIONNO=" + ALLOCATIONNO, string.Empty);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlIntake.SelectedValue = ds.Tables[0].Rows[0]["INTAKENO"].ToString();
                ddlDiscipline.SelectedValue = ds.Tables[0].Rows[0]["UA_SECTION"].ToString();
                ddlDiscipline_SelectedIndexChanged(new object(), new EventArgs());
               // ddlProgram.SelectedValue = ds.Tables[0].Rows[0]["DEGREENOS"].ToString() ;


                ddlRulename.SelectedValue = null;
                string[] couser = Convert.ToString(ds.Tables[0].Rows[0]["COURSERULENO"]).Split(',');

                foreach (string s in couser)
                {
                    foreach (ListItem item in ddlRulename.Items)
                    {
                        if (s == item.Value)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
                ddlProgram.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNAME"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ProgramRuleCreation.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void clearAllo()
    {
        ddlIntake.SelectedIndex = 0;
        ddlDiscipline.SelectedIndex = 0;
        ddlProgram.SelectedIndex = 0;
        ddlRulename.SelectedValue = null;
    }

    protected void btnCancelAllocation_Click(object sender, EventArgs e)
    {
        clearAllo();

    }

    //Bridging Eligibility

    protected void ListViewBindBridging()
    {
        objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_NAME");
        objCommon.FillDropDownList(ddlIntakeBA, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVE=" + 1, "BATCHNO DESC");

    }
    protected void DropDown()
    {
        objCommon.FillListBox(ddlRulename, "ACD_COURSE_RULE CR inner join ACD_ALTYPE A ON(A.ALTYPENO IN(SELECT CAST(VALUE AS CHAR) FROM DBO.SPLIT(CR.ALTYPENAME, ',')))", "COURSERULENO", "A.ALTYPENAME+ ' - ' + CR.RULENAME AS RULENAME", "COURSERULENO>0", "COURSERULENO");
       // objCommon.FillListBox(ddlRulename, "ACD_COURSE_RULE", "COURSERULENO", "RULENAME", "COURSERULENO>0", "COURSERULENO");
       
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
       //objCommon.FillDropDownList(ddlProgramBrid, "ACD_COURSE_MAPPING CM INNER JOIN ACD_DEGREE D ON(CM.DEGREENO = D.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON(CM.DEGREENO = CDB.DEGREENO)INNER JOIN ACD_BRANCH B ON(CDB.BRANCHNO = B.BRANCHNO)", "DISTINCT  B.BRANCHNO", "D.DEGREENAME + ' - ' + B.LONGNAME AS PROGRAM", "CM.COLLEGE_ID=" + ddlFaculty.SelectedValue, "PROGRAM");
        //  objCommon.FillDropDownList(ddlProgramBrid, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_UA_SECTION US ON(CDB.UGPGOT = US.UA_SECTION) INNER JOIN ACD_BRANCH B ON(CDB.BRANCHNO = B.BRANCHNO) inner join ACD_DEGREE D ON(CDB.DEGREENO = D.DEGREENO)",
        //"DISTINCT  B.BRANCHNO", "D.DEGREENAME + ' - ' + B.LONGNAME AS PROGRAM", "CDB.COLLEGE_ID=" + ddlFaculty.SelectedValue, "PROGRAM");

       objCommon.FillDropDownList(ddlProgramBrid, "ACD_COLLEGE_DEGREE_BRANCH  CDM INNER JOIN  ACD_DEGREE D ON(CDM.DEGREENO = D.DEGREENO) INNER JOIN ACD_BRANCH B ON(CDM.BRANCHNO = B.BRANCHNO)", "DISTINCT (CONVERT(VARCHAR,B.BRANCHNO) + ',' + CONVERT(VARCHAR,D.DEGREENO) ) AS PROGRAMS", " D.DEGREENAME + ' - ' + B.LONGNAME AS PROGRAM", "CDM.COLLEGE_ID=" + ddlFaculty.SelectedValue, "PROGRAM");
        // objCommon.FillListBox(ddlProgramBrid, "ACD_COLLEGE_DEGREE_BRANCH  CDM INNER JOIN ACD_DEGREE D ON(CDM.DEGREENO = D.DEGREENO) INNER JOIN ACD_BRANCH B ON(CDM.BRANCHNO = B.BRANCHNO)", "DISTINCT  B.BRANCHNO", " D.DEGREENAME + ' - ' + B.LONGNAME AS PROGRAM", "CDM.COLLEGE_ID=" + ddlFaculty.SelectedValue, "PROGRAM");
        lvlBridging.DataSource = null;
        lvlBridging.DataBind();
    }



    private void SetInitialRowGrades()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dt.Columns.Add(new DataColumn("Column5", typeof(string)));
        dt.Columns.Add(new DataColumn("Column6", typeof(string)));
        dt.Columns.Add(new DataColumn("Column7", typeof(string)));

      
        dr = dt.NewRow();
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dr["Column5"] = string.Empty;
        dr["Column6"] = string.Empty;
        dr["Column7"] = string.Empty;
     
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;

        lvlBridging.DataSource = dt;
        lvlBridging.DataBind();
    }

    private void showdetailsbridge()
    {
        try
        {
            foreach (ListViewDataItem dataitem in lvlBridging.Items)
            {
                ListBox ddlStreams = dataitem.FindControl("ddlStreams") as ListBox;
          
                objCommon.FillListBox(ddlStreams, "ACD_STREAM", "STREAMNO", "STREAMNAME", "STREAMNO > 0", "STREAMNAME");
            }

            string SEMNO = string.Empty;
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CheckBox chkbox = (CheckBox)lvlBridging.Items[rowIndex].FindControl("chkCheck");
                        HiddenField hdfid = (HiddenField)lvlBridging.Items[rowIndex].FindControl("hdfvalue");
                        Label Srno = (Label)lvlBridging.Items[rowIndex].FindControl("lblSrNo");
                        Label COURSENO = (Label)lvlBridging.Items[rowIndex].FindControl("lblCourseNo");
                       // Label CCODE = (Label)lvlBridging.Items[rowIndex].FindControl("lblModuleCode");
                        Label COURSE_NAME = (Label)lvlBridging.Items[rowIndex].FindControl("lblModuleName");
                        Label PROGRAM = (Label)lvlBridging.Items[rowIndex].FindControl("lblProgram");
                        ListBox ddlStreams = (ListBox)lvlBridging.Items[rowIndex].FindControl("ddlStreams");
                        Label CCODE = (Label)lvlBridging.Items[rowIndex].FindControl("lblModuleCode");
                        Label CModule = (Label)lvlBridging.Items[rowIndex].FindControl("lblModule");

                   
                        COURSENO.Text = dt.Rows[i]["Column2"].ToString();
                      //  CCODE.Text = dt.Rows[i]["Column3"].ToString();
                        COURSE_NAME.Text = dt.Rows[i]["Column3"].ToString();
                       PROGRAM.Text = dt.Rows[i]["Column4"].ToString();
                       CCODE.Text = dt.Rows[i]["Column6"].ToString();
                       CModule.Text = dt.Rows[i]["Column7"].ToString();
                  
                        string[] couser = dt.Rows[i]["Column5"].ToString().Split(',');
                     

                        foreach (string s in couser)
                        {
                            foreach (ListItem item in ddlStreams.Items)
                            {
                                if (s == item.Value)
                                {
                                    item.Selected = true;
                                    break;
                                }
                            }
                        }


                        if (dt.Rows[i]["Column1"].ToString() == "1")
                            chkbox.Checked = true;
                        else
                            chkbox.Checked = false;

                        rowIndex++;
                    }
                }
            }
        }
        catch 
        {

        }
    }

    private void BindListViewBridging()
    {
        try
        {

            string[] splitValue;
            splitValue = ddlProgramBrid.SelectedValue.Split(',');

            DataSet ds = objCourse.GetModuleName(Convert.ToInt32(splitValue[0]), Convert.ToInt32(splitValue[1]));
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    DataTable dtCurrentTable = new DataTable();

                    DataRow drCurrentRow = null;
                    dtCurrentTable.Columns.Add(new DataColumn("Column1", typeof(string)));
                    dtCurrentTable.Columns.Add(new DataColumn("Column2", typeof(string)));
                    dtCurrentTable.Columns.Add(new DataColumn("Column3", typeof(string)));
                    dtCurrentTable.Columns.Add(new DataColumn("Column4", typeof(string)));
                    dtCurrentTable.Columns.Add(new DataColumn("Column5", typeof(string)));
                    dtCurrentTable.Columns.Add(new DataColumn("Column6", typeof(string)));
                    dtCurrentTable.Columns.Add(new DataColumn("Column7", typeof(string)));



                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Column1"] = ds.Tables[0].Rows[i]["CHECKED"];
                        drCurrentRow["Column2"] = ds.Tables[0].Rows[i]["COURSENO"];
                      //  drCurrentRow["Column3"] = ds.Tables[0].Rows[i]["CCODE"];
                        drCurrentRow["Column3"] = ds.Tables[0].Rows[i]["MODULE_NAME"];
                        drCurrentRow["Column4"] = ds.Tables[0].Rows[i]["PROGRAM"];
                        drCurrentRow["Column5"] = ds.Tables[0].Rows[i]["STREAMNO"];
                        drCurrentRow["Column6"] = ds.Tables[0].Rows[i]["CCODE"];
                        drCurrentRow["Column7"] = ds.Tables[0].Rows[i]["COURSE_NAME"];
                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }

                    ViewState["CurrentTable"] = dtCurrentTable;
                    lvlBridging.DataSource = dtCurrentTable;
                    lvlBridging.DataBind();
                    foreach (ListViewDataItem dataitem in lvlBridging.Items)
                    {
                        ListBox ddlStreams = dataitem.FindControl("ddlStreams") as ListBox;
                        objCommon.FillListBox(ddlStreams, "ACD_STREAM", "STREAMNO", "STREAMNAME", "STREAMNO > 0", "STREAMNAME");
                    }
                }
                else
                {
                    lvlBridging.DataSource = null;
                    lvlBridging.DataBind();
                    objCommon.DisplayMessage(updBridgingAllocation, "Record Not Found", this.Page);
                }

            }
            if (ds != null) ds.Dispose();
            showdetailsbridge();
        }
        catch
        {

        }
    }

    private void SetPreviousDataGrades()
    {
        foreach (ListViewDataItem dataitem in lvlBridging.Items)
        {
            ListBox ddlStreams = dataitem.FindControl("ddlStreams") as ListBox;
        
            objCommon.FillListBox(ddlStreams, "ACD_STREAM", "STREAMNO", "STREAMNAME", "STREAMNO > 0", "STREAMNAME");
        }
        int rowIndex = 0;
        string SEMNO = string.Empty;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CheckBox chkbox = (CheckBox)lvlBridging.Items[rowIndex].FindControl("chkCheck");
                    HiddenField hdfid = (HiddenField)lvlBridging.Items[rowIndex].FindControl("hdfvalue");
                    Label Srno = (Label)lvlBridging.Items[rowIndex].FindControl("lblSrNo");
                    Label COURSENO = (Label)lvlBridging.Items[rowIndex].FindControl("lblCourseNo");
                 //   Label CCODE = (Label)lvlBridging.Items[rowIndex].FindControl("lblModuleCode");
                    Label COURSE_NAME = (Label)lvlBridging.Items[rowIndex].FindControl("lblModuleName");
                   Label PROGRAM = (Label)lvlBridging.Items[rowIndex].FindControl("lblProgram");
                    ListBox ddlStreams = (ListBox)lvlBridging.Items[rowIndex].FindControl("ddlStreams");
                    Label CCODE = (Label)lvlBridging.Items[rowIndex].FindControl("lblModuleCode");
                    Label CModule = (Label)lvlBridging.Items[rowIndex].FindControl("lblModule");

                    COURSENO.Text = dt.Rows[i]["Column2"].ToString();
                   // CCODE.Text = dt.Rows[i]["Column3"].ToString();
                    COURSE_NAME.Text = dt.Rows[i]["Column3"].ToString();
                   PROGRAM.Text = dt.Rows[i]["Column4"].ToString();
                   CCODE.Text = dt.Rows[i]["Column6"].ToString();
                   CModule.Text = dt.Rows[i]["Column7"].ToString();
                    string[] couser = dt.Rows[i]["Column5"].ToString().Split(',');

                 

                    foreach (string s in couser)
                    {
                        foreach (ListItem item in ddlStreams.Items)
                        {
                            if (s == item.Value)
                            {
                                item.Selected = true;
                                break;
                            }
                        }
                    }
                    
                    if (dt.Rows[i]["Column1"].ToString() == "1")
                        chkbox.Checked = true;
                    else
                        chkbox.Checked = false;

                    rowIndex++;
                }
            }
        }
        else
        {
            showdetailsbridge();
        }
    }
    
    //private void BindListViewBridging()
    //{
    //    try
    //    {
    //        string[] splitValue;
    //        splitValue = ddlProgramBrid.SelectedValue.Split(',');

    //        DataSet ds = objCourse.GetModuleName(Convert.ToInt32(splitValue[0]), Convert.ToInt32(splitValue[1]));
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {

    //            lvlBridging.DataSource = ds;
    //            lvlBridging.DataBind();

    //            foreach (ListViewDataItem dataitem in lvlBridging.Items)
    //            {
    //                ListBox ddlStreams = dataitem.FindControl("ddlStreams") as ListBox;
    //                // ListBox ddlStreams = (ListBox)lvlBridging.Items[k].FindControl("ddlStreams");
    //                objCommon.FillListBox(ddlStreams, "ACD_STREAM", "STREAMNO", "STREAMNAME", "STREAMNO > 0", "STREAMNAME");
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ProgramRuleCreation.BindListView-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}
    protected void btnCancelBridging_Click(object sender, EventArgs e)
    {
        txtBridgingRule.Text = "";
        ddlFaculty.SelectedIndex = 0;
        ddlProgramBrid.SelectedValue= "0";
        lvlBridging.DataSource = null;
        lvlBridging.DataBind();
        // Response.Redirect(Request.Url.ToString());
    }
  
    protected void ddlPrograms_SelectedIndexChanged1(object sender, EventArgs e)
    {
      
        BindListViewBridging();
    }
    protected void btnSaveBridging_Click(object sender, EventArgs e)
    {
        string Rulename = txtBridgingRule.Text.Trim();
        int college = Convert.ToInt32(ddlFaculty.SelectedValue);



       // string branch = string.Empty;
        //foreach (ListItem items in ddlProgramBrid.Items)
        //{
        //    if (items.Selected == true)
        //    {
        //        branch += items.Value + ',';

        //    }
        //}
        //if (branch == string.Empty)
        //{
        //    objCommon.DisplayMessage(updBridging, "Please Select Branch", this.Page);
        //    return;
        //}
        //branch = branch.Substring(0, branch.Length - 1);
        string[] splitValue;
        splitValue = ddlProgramBrid.SelectedValue.Split(',');

        // string Stream = string.Empty;
        CustomStatus cs = 0;
        foreach (ListViewDataItem dataitem in lvlBridging.Items)
        {
            
            string Stream = string.Empty;
            HiddenField hdfValue = (HiddenField)dataitem.FindControl("hdfvalue") as HiddenField;
            Label lblCourseNo = dataitem.FindControl("lblCourseNo") as Label;
            Label lblModuleCode = dataitem.FindControl("lblModuleCode") as Label;
            Label lblModuleName = dataitem.FindControl("lblModule") as Label;
            ListBox ddlStreams = dataitem.FindControl("ddlStreams") as ListBox;
            CheckBox chk = dataitem.FindControl("chkCheck") as CheckBox;
            int chks = 0;
            if (chk.Checked == true)
            {
                chks = 1;
                foreach (ListItem items in ddlStreams.Items)
                {
                    if (items.Selected == true)
                    {
                        Stream += items.Value + ',';
                       
                    }
                }
                if (Stream == string.Empty)
                {
                    objCommon.DisplayMessage(updBridging, "Please Select Stream", this.Page);
                    return;
                }
                Stream = Stream.Substring(0, Stream.Length - 1);
                int CourseNo = Convert.ToInt32(lblCourseNo.Text);
                string ModuleCode = lblModuleCode.Text;
                string ModuleName = lblModuleName.Text;
                // int Stream = Convert.ToInt32(ddlStreams.SelectedValue);

                cs = (CustomStatus)objCourse.InsertBridging(Rulename, college, (splitValue[0]), Convert.ToInt32(splitValue[1]) ,ModuleCode, ModuleName, Stream,CourseNo,chks);
            }
           
       
        }
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this.updBridging, "Record Saved Successfully.", this.Page);
            // BindListViewRuleAllocation();
            DropDownBindBridgingAllocation();
            return;
        }
        else
        {
            objCommon.DisplayMessage(this.updBridging, "Failed To Save Record ", this.Page);
        }
    }

  


    // BRIDGING ALLOCATION //

    protected void DropDownBindBridgingAllocation()
    {

        objCommon.FillDropDownList(ddlIntakeBA, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVE=" + 1, "BATCHNO DESC");
       // objCommon.FillListBox(ddlRuleNameBA, "ACD_BRIDGING_ELIGIBILITY", " BRIDGINGNO ", " RULENAME ", "BRIDGINGNO>0 ", "RULENAME");

        objCommon.FillListBox(ddlRuleNameBA, "ACD_BRIDGING_ELIGIBILITY", "DISTINCT UNIQUE_ID", " RULENAME ", "BRIDGINGNO>0 ", "RULENAME");
        
    }
    protected void btnSaveBridgingAllo_Click(object sender, EventArgs e)
    {
        CustomStatus cs = 0;
        int IntakeBridAll = 0;
        if (ddlIntakeBA.SelectedIndex == 0)
        {
            //objCommon.DisplayMessage(updBridgingAllocation, "Please Select Intake", this.Page);
           // return;
        }
        else
        {
            IntakeBridAll = Convert.ToInt32(ddlIntakeBA.SelectedValue);
        }
        string RuleNameBridAllo = string.Empty;
        foreach (ListItem items in ddlRuleNameBA.Items)
        {
            if (items.Selected == true)
            {
                RuleNameBridAllo += items.Value + ',';

            }
        }
        if (RuleNameBridAllo == string.Empty)
        {
            objCommon.DisplayMessage(updBridgingAllocation, "Please Select Rule Name", this.Page);
            return;
        }
        RuleNameBridAllo = RuleNameBridAllo.Substring(0, RuleNameBridAllo.Length - 1);

        cs = (CustomStatus)objCourse.InsertBridgingAllocation(IntakeBridAll, RuleNameBridAllo);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this.updBridgingAllocation, "Record Saved Successfully.", this.Page);
            BindListViewBridgingAllocation();
            return;
        }
        else
        {
            objCommon.DisplayMessage(this.updBridgingAllocation, "Failed To Save Record ", this.Page);
        }
    }
    private void BindListViewBridgingAllocation()

    {
        try
        {

            DataSet ds = objCourse.GetAllBridgingAllocationRule();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ListView1.DataSource = ds;
                ListView1.DataBind();

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


    protected void Button1_Click(object sender, EventArgs e)
    {
        ddlIntakeBA.SelectedIndex = 0;
        ddlRuleNameBA.SelectedValue = null;
    }

   
}


