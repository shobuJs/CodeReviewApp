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
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
public partial class Student_Achievement : System.Web.UI.Page
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

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                }
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
                ViewState["Achivement_no"] = "NULL";
                ViewState["SPORTNO"] = "NULL";
            }
            objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_NAME");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNAME");
            BindAchivementData();
            BindSportdata();


        }
    }

    private void CheckPageAuthorization()
    {
        // Request.QueryString["pageno"] = "ACADEMIC/EXAMINATION/ExamRegistration_New.aspx ?pageno=1801";
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Student_Achievement.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Student_Achievement.aspx");
        }
    }
    
    protected void lnkTab1_Click(object sender, EventArgs e)
    {
        liTab1.Attributes.Add("class", "active");
        liTab2.Attributes.Remove("class");
        liTab3.Attributes.Remove("class");
        tab_1.Visible = true;
        tab_2.Visible = false;
        tab_3.Visible = false;
    }
    protected void lnkTab2_Click(object sender, EventArgs e)
    {
        liTab1.Attributes.Remove("class");
        liTab2.Attributes.Add("class", "active");
        liTab3.Attributes.Remove("class");
        tab_1.Visible = false;
        tab_2.Visible = true;
        tab_3.Visible = false;
    }
    protected void lnkTab3_Click(object sender, EventArgs e)
    {
        liTab1.Attributes.Remove("class");
        liTab2.Attributes.Remove("class");
        liTab3.Attributes.Add("class", "active");
        tab_1.Visible = false;
        tab_2.Visible = false;
        tab_3.Visible = true;
    }
    //Achivement Tab Start
    protected void BindAchivementData()
    {
        DataSet ds = objCommon.FillDropDown("ACD_ACHIEVEMENT_MASTER", "*", "", "", "ACHIEVEMENT_NAME");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvAchivement.DataSource = ds;
            lvAchivement.DataBind();
        }
        else
        {
            lvAchivement.DataSource = null;
            lvAchivement.DataBind();
        }
    }
    protected void btnAchivement_Click(object sender, EventArgs e)
    {
        string Achivement = txtAchivement.Text;
        int AchivementNo = 0;
        if (ViewState["Achivement_no"].ToString() != "NULL")
        {
            AchivementNo = Convert.ToInt32(ViewState["Achivement_no"].ToString());
        }
        CustomStatus cs=0;
        cs = (CustomStatus)objCourse.Insert_achivement_data(Achivement, AchivementNo);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            txtAchivement.Text = "";
            BindAchivementData();
            ViewState["Achivement_no"] = "NULL";
            objCommon.DisplayMessage(this, "Record Saved Successfully.", this.Page);
            return;
        }
        else if (cs.Equals(CustomStatus.TransactionFailed))
        {
            txtAchivement.Text = "";
            BindAchivementData();
            ViewState["Achivement_no"] = "NULL";
            objCommon.DisplayMessage(this, "Failed To Save Record ", this.Page);
        }
        else if (cs.Equals(CustomStatus.RecordUpdated))
        {
            txtAchivement.Text = "";
            BindAchivementData();
            ViewState["Achivement_no"] = "NULL";
            objCommon.DisplayMessage(this, "Record Update Successfully", this.Page);
        }
    }
    protected void btnEdits_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdits = sender as ImageButton;
        string AchivementName =(btnEdits.CommandArgument);
        int Achivement_no = Convert.ToInt32(btnEdits.ToolTip);
        ViewState["Achivement_no"] = Achivement_no;
        txtAchivement.Text = AchivementName.ToString();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        txtAchivement.Text = "";
    }
    //Achivement Tab End
    //Sport Tab Start
    protected void BindSportdata()
    {
        DataSet ds = objCommon.FillDropDown("ACD_SPORTS_DETAILS", "*", "", "", "SPORTS_NAME");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvSportType.DataSource = ds;
            lvSportType.DataBind();
        }
        else
        {
            lvSportType.DataSource = null;
            lvSportType.DataBind();
        }
    }
    protected void btnSoprtData_Click(object sender, EventArgs e)
    {
        string SportName = txtSportName.Text;
        int SportType = Convert.ToInt32(ddlSportType.SelectedValue);
        int SportNo = 0;
        if (ViewState["SPORTNO"].ToString() != "NULL")
        {
            SportNo = Convert.ToInt32(ViewState["SPORTNO"].ToString());
        }
        CustomStatus cs = 0;
        cs = (CustomStatus)objCourse.Insert_Sports_data(SportName, SportNo, SportType);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
           // ddlSportType.SelectedValue="0";
            txtSportName.Text = "";
            BindSportdata();
            ViewState["SPORTNO"] = "NULL";
            objCommon.DisplayMessage(this, "Record Saved Successfully.", this.Page);
            return;
        }
        else if (cs.Equals(CustomStatus.TransactionFailed))
        {
             //ddlSportType.SelectedValue="0";
            txtSportName.Text = "";
           BindSportdata();
           ViewState["SPORTNO"] = "NULL";
            objCommon.DisplayMessage(this, "Failed To Save Record ", this.Page);
        }
        else if (cs.Equals(CustomStatus.RecordUpdated))
        {
             ddlSportType.SelectedValue="0";
            txtSportName.Text = "";
            BindSportdata();
            ViewState["SPORTNO"] = "NULL";
            objCommon.DisplayMessage(this, "Record Update Successfully", this.Page);
        }
    }
    protected void btnEditSport_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdits = sender as ImageButton;
        string SportName = (btnEdits.CommandArgument);
        int Sport_no = Convert.ToInt32(btnEdits.ToolTip);
        string SportType =(btnEdits.CommandName);
        ViewState["SPORTNO"] = Sport_no;
        txtSportName.Text = SportName.ToString();
        //ddlSportType.SelectedValue = SportType.ToString();
    }
    protected void btncancel1_Click(object sender, EventArgs e)
    {
        txtSportName.Text = "";
        ddlSportType.SelectedValue = "0";
    }
    //Sport Tab End
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFaculty.SelectedValue != "0")
        {
            Divbutton.Visible = false;
            lvStudentAchievement.DataSource = null;
            lvStudentAchievement.DataBind();
         objCommon.FillDropDownList(ddlDegree, "ACD_STUDENT S INNER JOIN ACD_DEGREE D ON(S.DEGREENO=D.DEGREENO)", "DISTINCT D.DEGREENO", "DEGREENAME", "COLLEGE_ID=" + Convert.ToInt32(ddlFaculty.SelectedValue), "DEGREENAME");
        }
        else

        {
            Divbutton.Visible = false;
            lvStudentAchievement.DataSource = null;
            lvStudentAchievement.DataBind();
            ddlDegree.SelectedValue = "0";
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedValue != "0")
        {
            Divbutton.Visible = false;
            lvStudentAchievement.DataSource = null;
            lvStudentAchievement.DataBind();
            objCommon.FillDropDownList(ddlProgram, "ACD_STUDENT S INNER JOIN ACD_BRANCH D ON(S.BRANCHNO=D.BRANCHNO)", "DISTINCT D.BRANCHNO", "LONGNAME", "COLLEGE_ID=" + Convert.ToInt32(ddlFaculty.SelectedValue) + "AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
        }
        else
        {
            Divbutton.Visible = false;
            lvStudentAchievement.DataSource = null;
            lvStudentAchievement.DataBind();
            ddlProgram.SelectedValue = "0";
        }
    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListViewData();
    }
    protected void BindListViewData()
    {

        DataSet ds = objCommon.FillDropDown("ACD_STUDENT S LEFT JOIN ACD_STUDENT_ACHIEVEMENT SA ON (S.IDNO=SA.IDNO) LEFT JOIN ACD_ACHIEVEMENT_MASTER AM ON (AM.ACHIEVEMENT_NO=SA.ACHIEVEMENT_NO) LEFT JOIN ACD_SPORTS_DETAILS SD ON(SD.SPORTS_NO=SA.SPORTS_NO) LEFT JOIN ACD_SEMESTER SE ON(SE.SEMESTERNO=SA.SEMESTERNO) ", "REGNO", "S.BRANCHNO,S.IDNO,NAME_WITH_INITIAL,(CONVERT(VARCHAR,SA.SPORTS_NO) + ',' + CONVERT(VARCHAR,SA.SPORTS_TYPE)) AS SPORTS,SA.ACHIEVEMENT_NO,SA.SEMESTERNO,SE.SEMESTERNAME", "S.COLLEGE_ID=" + Convert.ToInt32(ddlFaculty.SelectedValue) + "AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND S.BRANCHNO=" + Convert.ToInt32(ddlProgram.SelectedValue), "NAME_WITH_INITIAL");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            lvStudentAchievement.DataSource = ds;
            lvStudentAchievement.DataBind();
            foreach (ListViewDataItem dataitem in lvStudentAchievement.Items)
            {
                DropDownList ddlSport = dataitem.FindControl("ddlSport") as DropDownList;
                DropDownList ddlAchievement = dataitem.FindControl("ddlAchievement") as DropDownList;
                Label lblSportno=dataitem.FindControl("lblSportno")as Label;
                Label lblAchivement=dataitem.FindControl("lblAchivement")as Label;
                objCommon.FillDropDownList(ddlSport, "ACD_SPORTS_DETAILS", "(CONVERT(VARCHAR,SPORTS_NO) + ',' + CONVERT(VARCHAR,isnull(SPORTS_TYPE,0))) AS SPORTS", "SPORTS_NAME", "SPORTS_NO>0", "SPORTS_NAME");
                objCommon.FillDropDownList(ddlAchievement, "ACD_ACHIEVEMENT_MASTER", "ACHIEVEMENT_NO", "ACHIEVEMENT_NAME", "ACHIEVEMENT_NO>0", "ACHIEVEMENT_NAME");
                
                if(lblSportno.Text!="" || lblSportno.Text!="0")
                {
                      ddlSport.SelectedValue=lblSportno.Text;
                }
                if (lblAchivement.Text != "" || lblAchivement.Text != "0")
                {
                    ddlAchievement.SelectedValue = lblAchivement.Text;
                }
                
        
            }
            Divbutton.Visible = true;
        }
        else
        {

            lvStudentAchievement.DataSource = null;
            lvStudentAchievement.DataBind();
        }
       
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int College_id = Convert.ToInt32(ddlFaculty.SelectedValue);
        int Degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
        int branchno = Convert.ToInt32(ddlProgram.SelectedValue);
        int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
        int UA_NO = Convert.ToInt32(Session["userno"].ToString());
        string Sportno=string.Empty;
        string SportType = string.Empty;
        string AchivementNo = string.Empty;
        string Idno = string.Empty;
        CustomStatus cs = 0;
        foreach (ListViewDataItem dataitem in lvStudentAchievement.Items)
        {
            CheckBox chkRow = dataitem.FindControl("chkRow") as CheckBox;
            Label lblIdno = dataitem.FindControl("lblIdno") as Label;
            DropDownList ddlSport = dataitem.FindControl("ddlSport") as DropDownList;
            DropDownList ddlAchievement = dataitem.FindControl("ddlAchievement") as DropDownList;
            if (chkRow.Checked == true)
            {
                if (ddlSport.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(this, "Please Select Sport..", this.Page);
                    return;
                }
                if (ddlAchievement.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(this, "Please Select Achievement..", this.Page);
                    return;
                }
                string[] splitValue;
                splitValue = ddlSport.SelectedValue.Split(',');
                Sportno += Convert.ToInt32(splitValue[0]) + ",";
                SportType += Convert.ToInt32(splitValue[1]) + ",";
                AchivementNo += (ddlAchievement.SelectedValue) + ",";
                Idno += lblIdno.Text + ",";
            }
        }
        if (Idno.ToString() == "")
        {
            objCommon.DisplayMessage(this, "Please Check atlist one Student...", this.Page);
            return;
        }
        //Sportno = Sportno.Trim(',');
        //SportType = SportType.Trim(',');
        //AchivementNo = AchivementNo.Trim(',');
        //Idno = Idno.Trim(',');

        cs = (CustomStatus)objCourse.InsertStudentAchievementData(College_id, Degreeno, branchno, Semesterno, Idno, Sportno, SportType, AchivementNo, UA_NO);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            BindListViewData();
            objCommon.DisplayMessage(this, "Record Saved Successfully.", this.Page);
            return;
        }
        else if (cs.Equals(CustomStatus.TransactionFailed))
        {
           BindListViewData();
            objCommon.DisplayMessage(this, "Failed To Save Record ", this.Page);
        }
        else if (cs.Equals(CustomStatus.RecordExist))
        {
            BindListViewData();
            objCommon.DisplayMessage(this, "Failed To Save Record ", this.Page);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString()); 
    }

}