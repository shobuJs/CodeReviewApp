//=================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : Academic
// PAGE NAME     : StudentFeedBackAns.aspx
// CREATION DATE : 13-02-2016
// CREATED BY    : Mr.Manish Walde
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
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
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
public partial class ACADEMIC_StudentFeedBackAns : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentFeedBackController objSFBC = new StudentFeedBackController();
    StudentFeedBack objSEB = new StudentFeedBack();
    string Semesterno = string.Empty;
    string Degreeno = string.Empty;
    string branchno = string.Empty;
    #region Page Load
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
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help   
                if (Request.QueryString["pageno"] != null)

                    if (Session["usertype"].ToString() == "2")
                    {
                        GetStudentDeatlsForEligibilty();
                        FillLabel();
                    }
                    else
                    {
                        pnlStudInfo.Visible = false;
                    }
            }
        }
        divMsg.InnerHtml = string.Empty;
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentFeedBackAns.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentFeedBackAns.aspx");
        }
    }
    #endregion

    #region Private Methods

    private bool CheckActivity()
    {
        bool ret = true;
        string sessionno = string.Empty;
        sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT ISNULL(SESSIONNO,0)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), Convert.ToString(Degreeno), Convert.ToString(branchno), Convert.ToString(Semesterno));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage(this.updDetails, "This Activity has been Stopped. Contact Admin.!!", this.Page);
                ret = false;

            }

            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage(this.updDetails, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                ret = false;
            }

        }
        else
        {
            objCommon.DisplayMessage(this.updDetails, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            ret = false;
        }
        dtr.Close();
        return ret;
    }
    protected void GetStudentDeatlsForEligibilty()
    {
        try
        {

            DataSet ds;
            ds = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO,BRANCHNO,SEMESTERNO", "STUDNAME", "IDNO=" + Convert.ToInt32(Session["idno"]), "IDNO");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Degreeno = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                branchno = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                Semesterno = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                CheckActivity();

            }
            else
            {
                objCommon.DisplayMessage("This Activity has not been Started for" + Semesterno + "rd sem.Please Contact Admin.!!", this.Page);
                pnlStudInfo.Visible = false;
                return;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntry.GetStudentDeatlsForEligibilty --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillLabel()
    {
        try
        {
            //Check for Activity On/Off
            if (CheckActivity())
            {
                Course objCourse = new Course();
                CourseController objCC = new CourseController();
                SqlDataReader dr = null;

                if (Session["usertype"].ToString() == "2")
                {
                    dr = objCC.GetShemeSemesterByUser(Convert.ToInt32(Session["idno"]));
                }
                else
                {
                    dr = objCC.GetShemeSemesterByUser(Convert.ToInt32(ViewState["Id"]));
                }
                int sessionno = Convert.ToInt32(Session["currentsession"]);
                StudentController objSC = new StudentController();

                // Commented below code because we added drop dwon list for session

                string Session_Name = string.Empty;
                Session_Name = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");

                lblSession.Text = Convert.ToString(Session_Name);

                string Session_No = string.Empty;
                Session_No = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT ISNULL(SESSIONNO,0)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
                Session["SESSIONNO"] = Convert.ToInt32(Session_No);
                lblSession.ToolTip = Session_No;
                if (dr != null)
                {
                    if (dr.Read())
                    {
                        lblName.Text = dr["STUDNAME"] == null ? string.Empty : dr["STUDNAME"].ToString();
                        lblName.ToolTip = dr["IDNO"] == null ? string.Empty : dr["IDNO"].ToString();

                        lblScheme.Text = dr["SCHEMENAME"] == null ? string.Empty : dr["SCHEMENAME"].ToString();
                        lblScheme.ToolTip = dr["SCHEMENO"] == null ? string.Empty : dr["SCHEMENO"].ToString();
                        lblSemester.Text = dr["SEMESTERNAME"] == null ? string.Empty : dr["SEMESTERNAME"].ToString();
                        lblSemester.ToolTip = dr["SEMESTERNO"] == null ? string.Empty : dr["SEMESTERNO"].ToString();
                        hdnFinalSem.Value = dr["FINAL_SEMESTER"] == null ? string.Empty : dr["FINAL_SEMESTER"].ToString();
                        lblCollegeName.Text = dr["COLLEGE_NAME"] == null ? string.Empty : dr["COLLEGE_NAME"].ToString();

                        string photo = objCommon.LookUp("ACD_STUD_PHOTO", "PHOTO", "IDNO = " + dr["IDNO"].ToString());
                        if (photo != string.Empty)
                        {
                            imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dr["IDNO"].ToString() + "&type=student";
                        }
                        else
                            imgPhoto.ImageUrl = "~/images/nophoto.jpg";


                        ValidateStudent();
                    }
                }

                if (dr != null) dr.Close();

                pnlStudInfo.Visible = true;
            }
            else
            {
                pnlStudInfo.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.FillLabel-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ValidateStudent()
    {
        try
        {
            string count = objCommon.LookUp("ACD_ONLINE_FEEDBACK OFD INNER JOIN ACD_FEEDBACK_QUESTION FQUE ON (OFD.QUESTIONID=FQUE.QUESTIONID)", "COUNT(1)", "FQUE.CTID = 3 AND SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + "  AND IDNO=" + Convert.ToInt32(lblName.ToolTip));

            if (Convert.ToInt32(count) != 0)
            {
                lblMsg.Text = string.Empty;
                lblMsg.Text = "You have already submitted feedback for this session.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Visible = true;
                pnlFeedback.Visible = false;
            }
            else
            {
                lblMsg.Text = string.Empty;
                lblMsg.Visible = false;

                if (hdnFinalSem.Value == "Y")
                {
                    FillUniversityQuestion();

                    if (lvUniversity.Items.Count > 0)
                    {
                        pnlFeedback.Visible = true;
                    }
                    else
                    {
                        pnlFeedback.Visible = false;
                        lblMsg.Text = "No questions found for feed back .";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        lblMsg.Visible = true;
                    }
                }
                else
                {
                    lblMsg.Text = string.Empty;
                    lblMsg.Text = "You are not eligible to fill university feedback. Only final semester students can fill it.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Visible = true;
                    pnlFeedback.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.ddlCourse_SelectedIndexChanged --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillUniversityQuestion()
    {
        objSEB.CTID = 3;
        try
        {
            DataSet ds = objSFBC.GetFeedBackQuestionForMaster(objSEB);

            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lvUniversity.DataSource = ds;
                    lvUniversity.DataBind();

                    foreach (ListViewDataItem dataitem in lvUniversity.Items)
                    {
                        RadioButtonList rblUniversity = dataitem.FindControl("rblUniversity") as RadioButtonList;
                        HiddenField hdnUniversity = dataitem.FindControl("hdnUniversity") as HiddenField;

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(hdnUniversity.Value) == Convert.ToInt32(ds.Tables[0].Rows[i]["QUESTIONID"]))
                            {
                                string ansOptions = ds.Tables[0].Rows[i]["ANS_OPTIONS"].ToString();
                                string ansValue = ds.Tables[0].Rows[i]["ANS_VALUE"].ToString();

                                if (ansOptions.Contains(","))
                                {
                                    string[] opt;
                                    string[] val;

                                    opt = ansOptions.Split(new[] { "," }, StringSplitOptions.None);
                                    val = ansValue.Split(new[] { "," }, StringSplitOptions.None);

                                    int itemindex = 0;
                                    for (int j = 0; j < opt.Length; j++)
                                    {
                                        for (int k = 0; k < val.Length; k++)
                                        {
                                            if (j == k)
                                            {
                                                RadioButtonList lst;
                                                lst = new RadioButtonList();

                                                rblUniversity.Items.Add(opt[j]);
                                                rblUniversity.SelectedIndex = itemindex;
                                                rblUniversity.SelectedItem.Value = val[j];

                                                itemindex++;
                                                break;
                                            }
                                        }
                                    }
                                }
                                rblUniversity.SelectedIndex = -1;
                                break;
                            }
                        }
                    }
                    lvUniversity.Visible = true;
                }
                else
                {
                    lvUniversity.Items.Clear();
                    lvUniversity.DataSource = null;
                    lvUniversity.DataBind();
                }
                ds.Dispose();
            }
            else
            {
                lvUniversity.Items.Clear();
                lvUniversity.DataSource = null;
                lvUniversity.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.FillUniversityQuestion-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ClearControls()
    {
        lvUniversity.DataSource = null;
        lvUniversity.DataBind();

        lblMsg.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtSuggestionA.Text = string.Empty;
        txtSuggestionB.Text = string.Empty;
        txtSuggestionC.Text = string.Empty;
        txtSuggestionD.Text = string.Empty;
        pnlFeedback.Visible = false;
    }

    private int FillAnswers()
    {
        objSEB.SessionNo = Convert.ToInt32(lblSession.ToolTip);
        objSEB.Ipaddress = Request.ServerVariables["REMOTE_HOST"];
        objSEB.Date = DateTime.Now;
        objSEB.CollegeCode = Session["colcode"].ToString();
        objSEB.Idno = Convert.ToInt32(lblName.ToolTip);
        objSEB.FB_Status = true;
        if (!txtRemark.Text.Equals(string.Empty)) objSEB.Remark = txtRemark.Text.ToString();
        if (!txtSuggestionA.Text.Equals(string.Empty)) objSEB.Suggestion_A = txtSuggestionA.Text.ToString();
        if (!txtSuggestionB.Text.Equals(string.Empty)) objSEB.Suggestion_B = txtSuggestionB.Text.ToString();
        if (!txtSuggestionC.Text.Equals(string.Empty)) objSEB.Suggestion_C = txtSuggestionC.Text.ToString();
        if (!txtSuggestionD.Text.Equals(string.Empty)) objSEB.Suggestion_D = txtSuggestionD.Text.ToString();

        int ret = objSFBC.AddStudentFeedBackAns(objSEB);

        return ret;
    }
    #endregion

    #region Form Events
    protected void lvSelected_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label lbl = e.Item.FindControl("lblComplete") as Label;
        if (lbl.Text.ToLower() == "completed")
        {
            lbl.Style.Add("color", "Green");
        }
        else
        {
            lbl.Style.Add("color", "Red");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["usertype"].ToString() == "2")
            {
                foreach (ListViewDataItem dataitem in lvUniversity.Items)
                {
                    RadioButtonList rblUniversity = dataitem.FindControl("rblUniversity") as RadioButtonList;
                    Label lblUniversity = dataitem.FindControl("lblUniversity") as Label;
                    if (rblUniversity.SelectedValue == "")
                    {
                        objCommon.DisplayUserMessage(updDetails, "Question No." + lblUniversity.Text + " must be select", this.Page);
                        return;
                    }
                    else
                    {
                        objSEB.AnswerIds += rblUniversity.SelectedValue + ",";
                        objSEB.QuestionIds += lblUniversity.ToolTip + ",";
                    }
                }

                objSEB.AnswerIds = objSEB.AnswerIds.TrimEnd(',');
                objSEB.QuestionIds = objSEB.QuestionIds.TrimEnd(',');

                int retFlag = this.FillAnswers();

                if (retFlag == 1)
                {
                    objCommon.DisplayUserMessage(updDetails, "Your FeedBack Saved Successfully.", this.Page);
                    this.ClearControls();
                }
                else
                    objCommon.DisplayUserMessage(updDetails, "Error..!! System Unable to Save FeedBack.", this.Page);

            }
            else
            {
                objCommon.DisplayUserMessage(updDetails, "Only students can fills this form.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearControls();
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        string count = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "COUNT(1)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND IDNO=" + Convert.ToInt32(lblName.ToolTip));
        if (Convert.ToInt32(count) != 0)
            ShowReport("Student_FeedBack", "StudentFeedBackAns.rpt");
        else
            objCommon.DisplayUserMessage(updDetails, "Record Not Found", this.Page);
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(lblName.ToolTip) + ",@P_SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + ",@P_SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + ",@P_SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + ",@P_COURSENO=0";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.ClearControls();
    }
    #endregion

}
