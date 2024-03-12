//=================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : Academic                                                                
// PAGE NAME     : StudentFeedBackAns.aspx                                               
// CREATION DATE : 25-05-2012                                                   
// CREATED BY    : Pawan Mourya                               
// MODIFIED BY   : Neha Baranwal
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
    string sessionno = string.Empty;

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
                    this.CheckPageAuthorization();
                    //Check for Activity On/Off
                    GetStudentDeatlsForEligibilty();
                    //if (CheckActivity() == false)
                    //    return;

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                  //  objCommon.SetLabelData("0");//for label
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
 
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                    try
                    {
                        if (Session["usertype"].ToString() == "2")
                        {
                            //fill student details
                            pnlStudInfo.Visible = true;
                            FillLabel();
                            //Fill DropDownList
                            objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME", "EXAMNO", "LEFT(EXAMNAME,5) EXAMNAME", "PATTERNNO=(SELECT PATTERNNO FROM ACD_SCHEME WHERE SCHEMENO= " + lblScheme.ToolTip + ") AND FLDNAME='S1'", "EXAMNO");

                            //attendance is not available in old session thats why comment
                            //to check Attendance condition for NON CBCS
                            //DataSet dsCheckOverallAtt = objSFBC.GetOverallAttPercent(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(lblScheme.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblName.ToolTip));
                            //if (dsCheckOverallAtt.Tables[0].Rows.Count > 0)
                            //{
                            //    //to check att PErcent course wise for CBCS only
                            //    if (dsCheckOverallAtt.Tables[0].Rows[0]["SCHEMETYPE"].ToString() == "Non CBCS")
                            //    {
                            //        //to check Attendance % for CBCS 
                            //        if (Convert.ToDouble(dsCheckOverallAtt.Tables[0].Rows[0]["OVERALL_PERCENT"].ToString()) >= 0.00)//80
                            //        {
                            //            //fill student details
                            //            pnlStudInfo.Visible = true;
                            //            FillLabel();
                            //            //Fill DropDownList
                            //            objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME", "EXAMNO", "LEFT(EXAMNAME,5) EXAMNAME", "PATTERNNO=(SELECT PATTERNNO FROM ACD_SCHEME WHERE SCHEMENO= " + lblScheme.ToolTip + ") AND FLDNAME='S1'", "EXAMNO");
                            //            pnlSubject.Visible = true;
                            //            divnotemsg.Visible = true;
                            //        }
                            //        else
                            //        {
                            //            objCommon.DisplayMessage(this, "Not Eligible For any Feedback because Attendance is only " + dsCheckOverallAtt.Tables[0].Rows[0]["OVERALL_PERCENT"].ToString() + "%. Attendance Should be greater than or equals to 80.00%", this.Page);
                            //            pnlFeedback.Visible = false;
                            //            lblMsg.Text = "";
                            //            lblMsg.Visible = false;
                            //            pnlSubject.Visible = false;
                            //            divnotemsg.Visible = false;
                            //            return;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        //fill student details
                            //        pnlStudInfo.Visible = true;
                            //        FillLabel();
                            //        //Fill DropDownList
                            //        objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME", "EXAMNO", "LEFT(EXAMNAME,5) EXAMNAME", "PATTERNNO=(SELECT PATTERNNO FROM ACD_SCHEME WHERE SCHEMENO= " + lblScheme.ToolTip + ") AND FLDNAME='S1'", "EXAMNO");
                            //        pnlSubject.Visible = true;
                            //        divnotemsg.Visible = true;
                            //    }
                            //}
                            //else
                            //{
                            //    objCommon.DisplayMessage(this, "Attendance Not Done For NON CBCS Student!!!", this.Page);
                            //    pnlFeedback.Visible = false;
                            //    lblMsg.Text = "";
                            //    lblMsg.Visible = false;
                            //    pnlSubject.Visible = false;
                            //    divnotemsg.Visible = false;
                            //    return;
                            //}
                        
                        }
                        else
                        {
                            //pnlSearch.Visible = true;
                            pnlStudInfo.Visible = false;
                        }

                    }
                    catch { }

                  

                }
            }
               divMsg.InnerHtml = string.Empty;
            if (Session["userno"] == null || Session["username"] == null ||
                   Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
        }
        catch { }
    }
    //to get student details for checking activity
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
                //CheckActivity();
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
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedbackAns.GetStudentDeatlsForEligibilty() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //check activity start or not
    private bool CheckActivity()
    {
        try
        {
            sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
            Session["sessionno"] = sessionno;

            ActivityController objActController = new ActivityController();
            if (sessionno != "")
            {
                DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));
                if (dtr.Read())
                {
                    if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                    {
                        objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                        //pnlSearch.Visible = false;
                        pnlStudInfo.Visible = false;

                    }
                    if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                    {
                        objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                        //pnlSearch.Visible = false;
                        pnlStudInfo.Visible = false;

                    }
                }
                else
                {
                    objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                    //pnlSearch.Visible = false;
                    pnlStudInfo.Visible = false;

                }

                dtr.Close();
                return true;
            }
            else
            {
                objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
               // pnlSearch.Visible = false;
                pnlStudInfo.Visible = false;
                return false;
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.ShowError(Page, "ACADEMIC_StudentFeedbackAns.CheckActivity() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            else
            {
                objCommon.ShowError(Page, "Server Unavailable.");
                return false;
            }
        }


    }

 
    //to get current session
    public int GetSession()
    {
        int sessionno = 0;
        string act_code = string.Empty;

        int idno = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_TYPE = 2 AND UA_NO=" + Session["userno"]));
        int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + idno));
        string session = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "SA.STARTED = 1 AND (AM.ACTIVITY_CODE='Feedback' OR AM.ACTIVITY_CODE='FBM') AND " + branchno + " IN (SELECT VALUE FROM DBO.Split(BRANCH,','))");

        if (session != string.Empty)
        {
            sessionno = Convert.ToInt32(session);
        }
        return sessionno;
    }


    //to fill student details in label control
    private void FillLabel()
    {
        Course objCourse = new Course();
        CourseController objCC = new CourseController();
        SqlDataReader dr = null;
        if (Session["usertype"].ToString() == "2")
        {
            dr = objCC.GetSchemeSemesterByUser(Convert.ToInt32(Session["idno"]));
        }
        else
        {
            dr = objCC.GetSchemeSemesterByUser(Convert.ToInt32(ViewState["Id"]));
        }
        if (dr != null)
        {
            if (dr.Read())
            {
                int sessionno = 0;
                lblName.Text = dr["STUDNAME"] == null ? string.Empty : dr["STUDNAME"].ToString();
                lblName.ToolTip = dr["IDNO"] == null ? string.Empty : dr["IDNO"].ToString();
               lblSession.ToolTip = dr["SESSIONNO"].ToString();
               lblSession.Text = dr["SESSION_NAME"].ToString(); 
                //lblSession.Text = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", " SESSIONNO = " + Convert.ToInt32(Session["sessionno"]));
                lblScheme.Text = dr["SCHEMENAME"] == null ? string.Empty : dr["SCHEMENAME"].ToString();
                lblScheme.ToolTip = dr["SCHEMENO"] == null ? string.Empty : dr["SCHEMENO"].ToString();
                lblcollege.Text = dr["college_name"] == null ? string.Empty : dr["college_name"].ToString();
                lblcollege.ToolTip = dr["COLLEGE_ID"] == null ? string.Empty : dr["COLLEGE_ID"].ToString();
     
                if (dr["YEARWISE"].ToString() == "1")
                {
                    lblSemester.Text = dr["YEARNAME"] == null ? string.Empty : dr["YEARNAME"].ToString();
                    lblSemester.ToolTip = dr["SEM"] == null ? string.Empty : dr["SEM"].ToString();
                }
                else
                {
                    lblSemester.Text = dr["SEMESTERNAME"] == null ? string.Empty : dr["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dr["SEMESTERNO"] == null ? string.Empty : dr["SEMESTERNO"].ToString();
                }
                //imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dr["IDNO"].ToString() + "&type=student";
            }
        }
        if (dr != null) dr.Close();

        if (lblScheme.ToolTip != "")
        {
            int idno = 0;
            idno = Session["usertype"].ToString() == "2" ? Convert.ToInt32(Session["idno"]) : Convert.ToInt32(ViewState["Id"]);
           // DataSet ds = objSFBC.GetCourseSelected(Convert.ToInt32(lblSession.ToolTip), idno, Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblScheme.ToolTip));
            DataSet ds = objSFBC.GetCourseSelectedForQuestion(Convert.ToInt32(lblSession.ToolTip), idno, Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblScheme.ToolTip));
            lvSelected.DataSource = ds;
            lvSelected.DataBind();
            this.CheckSubjectAssign();
        }
    }



    //function to show course name wise teacher
    public string GetCourseName(object coursename, object TeachName, object Teachertype)
    {
        return coursename.ToString() + " [<span style='color:Green;font-weight: bold;'>" + TeachName.ToString() + "</span>]" + " [<span style='color:darkcyan;font-weight: bold;'>" + Teachertype.ToString() + "</span>]";
    }


    //chechk which subject assign to which teacher with feedback status
    private void CheckSubjectAssign()
    {
        SqlDataReader dr = null;
        foreach (ListViewDataItem item in lvSelected.Items)
        {
            Label lblComplete = item.FindControl("lblComplete") as Label;
            lblComplete.ForeColor = System.Drawing.Color.Red;
            lblComplete.Text = "Incomplete";
        }

        if (Session["usertype"].ToString() == "2")
            dr = objSFBC.GetCourseSelected(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["idno"]));
        else
            dr = objSFBC.GetCourseSelected(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(ViewState["Id"]));
        if (dr != null)
        {
            while (dr.Read())
            {
                foreach (ListViewDataItem item in lvSelected.Items)
                {
                    LinkButton lnkCourse = item.FindControl("lnkbtnCourse") as LinkButton;
                    Label lblComplete = item.FindControl("lblComplete") as Label;

                    if (Convert.ToInt32(lnkCourse.CommandArgument) == Convert.ToInt32(dr["COURSENO"].ToString()) && lnkCourse.ToolTip == dr["UA_NO"].ToString())
                    {
                        lblComplete.ForeColor = System.Drawing.Color.Green;
                        lblComplete.Text = "Complete";
                        break;
                    }
                }
            }
        }
        if (dr != null) dr.Close();
    }


   
    private void FillCourseQuestion(int SubID)
    {
        objSEB.CTID = 1;
        objSEB.SubId = SubID;
        objSEB.SemesterNo =Convert.ToInt32(lblSemester.ToolTip);
        objSEB.Collegeid = Convert.ToInt32(lblcollege.ToolTip);
        try
        {
            //DataSet ds = objSFBC.GetFeedBackQuestionForMaster(objSEB);
            //// DataSet ds = objSFBC.GetFeedBackQuestionForMaster(objSEB, SubID);
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    lblcrse.Visible = true;
            //    lvCourse.DataSource = ds;
            //    lvCourse.DataBind();

            //    foreach (ListViewDataItem dataitem in lvCourse.Items)
            //    {
            //        RadioButtonList rblCourse = dataitem.FindControl("rblCourse") as RadioButtonList;
            //        HiddenField hdnCourse = dataitem.FindControl("hdnCourse") as HiddenField;

            //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //        {
            //            if (Convert.ToInt32(hdnCourse.Value) == Convert.ToInt32(ds.Tables[0].Rows[i]["QUESTIONID"]))
            //            {
            //                string ansOptions = ds.Tables[0].Rows[i]["ANS_OPTIONS"].ToString();
            //                string ansValue = ds.Tables[0].Rows[i]["ANS_VALUE"].ToString();

            //                if (ansOptions.Contains(","))
            //                {
            //                    string[] opt;
            //                    string[] val;

            //                    opt = ansOptions.Split(new[] { "," }, StringSplitOptions.None);
            //                    val = ansValue.Split(new[] { "," }, StringSplitOptions.None);

            //                    int itemindex = 0;
            //                    for (int j = 0; j < opt.Length; j++)
            //                    {
            //                        for (int k = 0; k < val.Length; k++)
            //                        {
            //                            if (j == k)
            //                            {
            //                                RadioButtonList lst;
            //                                lst = new RadioButtonList();

            //                                rblCourse.Items.Add(opt[j]);
            //                                rblCourse.SelectedIndex = itemindex;
            //                                rblCourse.SelectedItem.Value = val[j];

            //                                itemindex++;
            //                                break;
            //                            }
            //                        }
            //                    }
            //                }
            //                rblCourse.SelectedIndex = -1;
            //                break;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    lvCourse.Items.Clear();
            //    lblcrse.Visible = false;
            //    lvCourse.DataSource = null;
            //    lvCourse.DataBind();
            //}

            DataSet ds = objCommon.FillDropDown("ACD_FEEDBACK_MASTER M INNER JOIN ACD_FEEDBACK_QUESTION Q ON FEEDBACK_NO = CTID", "DISTINCT FEEDBACK_NO", "FEEDBACK_NAME", "FEEDBACK_TYPE != 1 AND QUESTIONID IS NOT NULL AND ISNULL([SEMESTERNO], 0) = " + Convert.ToInt32(lblSemester.ToolTip.Trim()) + " AND ISNULL([ACTIVE_STATUS], 0) = 1 AND [COLLEGE_ID] = " + Convert.ToInt32(lblcollege.ToolTip.Trim()) + "", "FEEDBACK_NO");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblcrse.Visible = true;
                lvCourse.DataSource = ds;
                lvCourse.DataBind();

                foreach (ListViewDataItem dataitem in lvCourse.Items)
                {
                    RadioButtonList rblCourse = dataitem.FindControl("rblCourse") as RadioButtonList;
                    Label lblCTType = dataitem.FindControl("lblCTType") as Label;
                    Label lblcom = dataitem.FindControl("lblcom") as Label;

                    lblcom.Text = "Please write any additional comments about the " + lblCTType.Text;

                }

                //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //    {
                //        if (Convert.ToInt32(hdnCourse.Value) == Convert.ToInt32(ds.Tables[0].Rows[i]["QUESTIONID"]))
                //        {
                //            string ansOptions = ds.Tables[0].Rows[i]["ANS_OPTIONS"].ToString();
                //            string ansValue = ds.Tables[0].Rows[i]["ANS_VALUE"].ToString();

                //            if (ansOptions.Contains(","))
                //            {
                //                string[] opt;
                //                string[] val;

                //                opt = ansOptions.Split(new[] { "," }, StringSplitOptions.None);
                //                val = ansValue.Split(new[] { "," }, StringSplitOptions.None);

                //                int itemindex = 0;
                //                for (int j = 0; j < opt.Length; j++)
                //                {
                //                    for (int k = 0; k < val.Length; k++)
                //                    {
                //                        if (j == k)
                //                        {
                //                            RadioButtonList lst;
                //                            lst = new RadioButtonList();

                //                            rblCourse.Items.Add(opt[j]);
                //                            rblCourse.SelectedIndex = itemindex;
                //                            rblCourse.SelectedItem.Value = val[j];

                //                            itemindex++;
                //                            break;
                //                        }
                //                    }
                //                }
                //            }
                //            rblCourse.SelectedIndex = -1;
                //            break;
                //        }
                //    }
                //}
            }
            else
            {
                lvCourse.Items.Clear();
                lblcrse.Visible = false;
                lvCourse.DataSource = null;
                lvCourse.DataBind();
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.fillCourseQuestion()-> " + ex.Message + "" + ex.StackTrace);
            }
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    //to check page is authorized or not
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


    // function to save the feedback details
    private int FillAnswers()
    {
        objSEB.SessionNo = Convert.ToInt32(lblSession.ToolTip);
        objSEB.Ipaddress = Request.ServerVariables["REMOTE_HOST"];
        objSEB.Date = DateTime.Now;
        objSEB.CollegeCode = Session["colcode"].ToString();
        objSEB.Idno = Convert.ToInt32(lblName.ToolTip);
        objSEB.CourseNo = Convert.ToInt32(ViewState["COURSENO"].ToString());
        objSEB.UA_NO = Convert.ToInt32(ViewState["TeachNo"].ToString());
        objSEB.FB_Status = true;
        objSEB.OverallImpression = "0";
        objSEB.Suggestion_A = lblWhatOtherChanges.Text;
        objSEB.Suggestion_B = txtWhatOtherChanges.Text;
        objSEB.Suggestion_C = lblAnyComments.Text;
        objSEB.Suggestion_D = txtAnyComments.Text;

        //objSEB.CTID = 1;

        objSEB.ExamNo = Convert.ToInt32(ddlExam.SelectedValue);

        if (!txtRemark.Text.Equals(string.Empty)) objSEB.Remark = txtRemark.Text.ToString();
        int ret = objSFBC.AddStudentFeedBackAns(objSEB);
        return ret;
    }



    // to save the feedback details
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["usertype"].ToString() == "2")
            {
                foreach (ListViewDataItem dataitem in lvCourse.Items)
                {
                    ListView lQuestion = dataitem.FindControl("lvQuestion") as ListView;
                    TextBox TxtComments = dataitem.FindControl("TxtComments") as TextBox;
                    Label lblCTType = dataitem.FindControl("lblCTType") as Label;
                    foreach (ListViewDataItem dataitem1 in lQuestion.Items)
                    {
                        RadioButtonList rblCourse = dataitem1.FindControl("rblAnswer") as RadioButtonList;
                        Label lblCourse = dataitem1.FindControl("lblQuestion") as Label;
                       
                        HiddenField hdnCTType = dataitem1.FindControl("hdnCTType") as HiddenField;
                        if (rblCourse.SelectedValue == "")
                        {
                            objCommon.DisplayMessage("You must have answer all the questions", this.Page);
                            return;
                        }
                        else
                        {
                            objSEB.AnswerIds += rblCourse.SelectedValue + ",";
                            objSEB.QuestionIds += lblCourse.Text + ",";
                           
                        }
                        
                    }
                    objSEB.CidComments += TxtComments.Text + ",";
                    objSEB.FeedbackNo += lblCTType.ToolTip + ",";
                }
               
                objSEB.AnswerIds = objSEB.AnswerIds.TrimEnd(',');
                objSEB.QuestionIds = objSEB.QuestionIds.TrimEnd(',');
                objSEB.CidComments = objSEB.CidComments.TrimEnd(',');
                objSEB.FeedbackNo = objSEB.FeedbackNo.TrimEnd(',');
              
                //this.FillAnswers();

                int retFlag = this.FillAnswers();

                if (retFlag == 1)
                {
                    objCommon.DisplayMessage("Your FeedBack Saved Successfully !", this.Page);
                    txtWhatOtherChanges.Text = "";
                    txtAnyComments.Text = "";
                }
                else
                {
                    //objCommon.DisplayMessage("Your FeedBack Saved Successfully !", this.Page);
                    objCommon.DisplayMessage("Something Went Wrong !", this.Page);
                }

                this.CheckSubjectAssign();

                this.ClearControls();
            }
            else
            {
                objCommon.DisplayMessage("Only Students fills this form!!", this.Page);
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

    //to clear all controls
    private void ClearControls()
    {
        lblMsg.Text = string.Empty;
        txtRemark.Text = string.Empty;
        ddlExam.SelectedIndex = 0;

        pnlFeedback.Visible = false;
    }

    //to load questions for selected course
    protected void lnkbtnCourse_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            lblcrse.Text = lnk.Text;
            //lblteacher.Text = lnk.Text;
            ViewState["TeachNo"] = lnk.ToolTip;
            ViewState["COURSENO"] = lnk.CommandArgument;

            ViewState["SubId"] = 0;

            if (ViewState["COURSENO"] != "")
            {
                string subid = objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + ViewState["COURSENO"]);
                if (subid != "")
                {
                    ViewState["SubId"] = subid;
                }
                else
                {
                    ViewState["SubId"] = 0;
                }
            }
            string count = "-1";

            if (lnk.ToolTip == string.Empty)
            {
                objCommon.DisplayMessage(this, "No faculty is assigned to the selected Course!!!", this.Page);
                return;
            }
            else
            {
                if (lnk.ToolTip != "")
                {
                    DataSet dsCheckStudAtt = objSFBC.GetCourseWiseAttPercent(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(lblScheme.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lnk.CommandArgument), Convert.ToInt32(lblName.ToolTip));
                   if (dsCheckStudAtt.Tables[0].Rows.Count > 0)
                   {
                       //to check att PErcent course wise for CBCS only
                       if (dsCheckStudAtt.Tables[0].Rows[0]["SCHEMETYPE"].ToString() == "CBCS")
                       {
                           //to check Attendance % for CBCS 
                           if (Convert.ToDouble(dsCheckStudAtt.Tables[0].Rows[0]["ATT_PER"].ToString()) >= 0.00)//75
                           {
                               //to check entry already done or not
                               count = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND COURSENO=" + Convert.ToInt32(lnk.CommandArgument) + "AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + "AND UA_NO=" + Convert.ToInt32(lnk.ToolTip) + "AND CTID=1");
                           }
                           else
                           {
                               objCommon.DisplayMessage(this, "Not Eligible For this Subject Feedback because Attendance is only " + dsCheckStudAtt.Tables[0].Rows[0]["ATT_PER"].ToString() + "%. Attendance Should be greater than or equals to 75.00%", this.Page);
                               pnlFeedback.Visible = false;
                               lblMsg.Text = "";
                               lblMsg.Visible = false;
                               return;
                           }
                       }
                       else
                       {
                           //to check entry already done or not
                           count = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND COURSENO=" + Convert.ToInt32(lnk.CommandArgument) + "AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + "AND UA_NO=" + Convert.ToInt32(lnk.ToolTip) + "AND CTID=1");
                       }
                   }
                   else
                   {
                       objCommon.DisplayMessage(this, "Attendance Not Done For This Selected Course!!!", this.Page);
                       pnlFeedback.Visible = false;
                       lblMsg.Text = "";
                       lblMsg.Visible = false;
                       return;
                   }

                }
               
            }

            if (Convert.ToInt32(count) > 0)//entry already done
            {
                string date = "";
                date = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "distinct Convert(varchar(10),DATE,103)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND isnull(COURSENO,0)=" + Convert.ToInt32(lnk.CommandArgument) + " AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + "AND isnull(UA_NO,0)=" + Convert.ToInt32(lnk.ToolTip) + " AND CTID=1");

                lblMsg.Text = "FeedBack is already completed for " + lnk.Text + " on DATE " + date;
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Visible = true;
                pnlFeedback.Visible = false;
            }
            else if (Convert.ToInt32(count) == 0)//new entry
            {
                lblMsg.Text = "";
                lblMsg.Visible = false;
                FillCourseQuestion(Convert.ToInt16(ViewState["SubId"]));
                //FillTeacherQuestion();
                pnlFeedback.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.lnkbtnCourse_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    //to clear the page
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    //protected void btnReport_Click(object sender, EventArgs e)
    //{
    //    string count = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND COURSENO=" + Convert.ToInt32(ViewState["COURSENO"].ToString()));
    //    if (Convert.ToInt32(count) != 0)
    //        ShowReport("Student_FeedBack", "StudentFeedBackAns.rpt");
    //    else
    //        objCommon.DisplayMessage("Record Not Found", this.Page);
    //}

    //private void ShowReport(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(lblName.ToolTip) + ",@P_SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + ",@P_SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + ",@P_SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["COURSENO"].ToString());
    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    string idno = objCommon.LookUp("ACD_STUDENT", "DISTINCT IDNO", "REGNO='" + txtSearch.Text.Trim() + "'");
    //    if (idno != "")
    //    {
    //        ViewState["Id"] = Convert.ToInt32(idno);
    //        FillLabel();
    //        pnlStudInfo.Visible = true;
    //        btnClear.Visible = true;
    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage("Record Not Found", this.Page);
    //    }
    //}
    //private void FillTeacherQuestion()
    //{
    //    objSEB.CTID = 2;
    //    try
    //    {
    //        DataSet ds = objSFBC.GetFeedBackQuestionForMaster(objSEB);
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            lblteacher.Visible = true;
    //            lvTeacher.DataSource = ds;
    //            lvTeacher.DataBind();
    //            foreach (ListViewDataItem dataitem in lvTeacher.Items)
    //            {
    //                RadioButtonList rblTeacher = dataitem.FindControl("rblTeacher") as RadioButtonList;
    //                HiddenField hdnTeacher = dataitem.FindControl("hdnTeacher") as HiddenField;

    //                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //                {
    //                    if (Convert.ToInt32(rblTeacher.ToolTip) == Convert.ToInt32(ds.Tables[0].Rows[i]["QUESTIONID"]))
    //                    {
    //                        string ansOptions = ds.Tables[0].Rows[i]["ANS_OPTIONS"].ToString();
    //                        string ansValue = ds.Tables[0].Rows[i]["ANS_VALUE"].ToString();

    //                        if (ansOptions.Contains(","))
    //                        {
    //                            string[] opt;
    //                            string[] val;

    //                            opt = ansOptions.Split(new[] { "," }, StringSplitOptions.None);
    //                            val = ansValue.Split(new[] { "," }, StringSplitOptions.None);

    //                            int itemindex = 0;
    //                            for (int j = 0; j < opt.Length; j++)
    //                            {
    //                                for (int k = 0; k < val.Length; k++)
    //                                {
    //                                    if (j == k)
    //                                    {
    //                                        RadioButtonList lst;
    //                                        lst = new RadioButtonList();

    //                                        rblTeacher.Items.Add(opt[j]);
    //                                        rblTeacher.SelectedIndex = itemindex;
    //                                        rblTeacher.SelectedItem.Value = val[j];

    //                                        itemindex++;
    //                                        break;
    //                                    }
    //                                }
    //                            }
    //                        }
    //                        rblTeacher.SelectedIndex = -1;
    //                        break;
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            lvTeacher.Items.Clear();
    //            lblteacher.Visible = false;
    //            lvTeacher.DataSource = null;
    //            lvTeacher.DataBind();
    //        }
    //    }


    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.FillTeacherQuestion()-> " + ex.Message + "" + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}


    //to load all question for particular selected course
    //protected void btnClear_Click(object sender, EventArgs e)
    //{
    //    //Response.Redirect(Request.Url.ToString());
    //}
    protected void lvQuestion_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        RadioButtonList rAnswer = (RadioButtonList)e.Item.FindControl("rblAnswer");
        Label lQuestion = (Label)e.Item.FindControl("lblQuestion");

        //DataSet ds = objCommon.FillDropDown("(SELECT Id, Value Value1 FROM DBO.SPLIT((SELECT [ANS_OPTIONS] FROM ACD_FEEDBACK_QUESTION WHERE [QUESTIONID] = " + Convert.ToInt32(lQuestion.Text.Trim()) + "), ','))A INNER JOIN  (SELECT Id, Value Value2 FROM DBO.SPLIT((SELECT [ANS_VALUE] FROM ACD_FEEDBACK_QUESTION WHERE [QUESTIONID] = " + Convert.ToInt32(lQuestion.Text.Trim()) + "), ','))B ON A.Id = B.Id", "Value2", "Value1", "", "");
        DataSet ds = objCommon.FillDropDown("ACD_FEEDBACK_QUESTION Q inner join ACD_FEEDBACK_ANSWER_LIST QL ON  (QL.ANS_NO IN (SELECT * FROM [DBO].[FN_SPLIT](ISNULL(Q.ANS_VALUE,0),',')))", "ANS_NO AS Value2", "ANSWER AS Value1", "QUESTIONID=" + Convert.ToInt32(lQuestion.Text.Trim()), "");

        rAnswer.Items.Clear();

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            rAnswer.Items.Add(new ListItem(ds.Tables[0].Rows[i]["Value1"].ToString(), ds.Tables[0].Rows[i]["Value2"].ToString()));

        }
        //rAnswer.SelectedIndex = 0;
    }

    protected void lvCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListView lQuestion = (ListView)e.Item.FindControl("lvQuestion");
        HiddenField hCTType = (HiddenField)e.Item.FindControl("hdnCTType");
        DataSet ds = objCommon.FillDropDown("ACD_FEEDBACK_QUESTION Q", "ROW_NUMBER() OVER (ORDER BY [QUESTIONID])SLNO", "[QUESTIONID],[QUESTIONNAME],(SELECT FEEDBACK_NAME FROM ACD_FEEDBACK_MASTER WITH(NOLOCK) WHERE FEEDBACK_NO = Q.CTID)FEEDBACK_NAME", "ISNULL([SEMESTERNO], 0) = " + Convert.ToInt32(lblSemester.ToolTip.Trim()) + " AND ISNULL([ACTIVE_STATUS], 0) = 1 AND [COLLEGE_ID] = " + Convert.ToInt32(lblcollege.ToolTip.Trim()) + " AND CTID = " + Convert.ToInt32(hCTType.Value.Trim()) + "", "");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lQuestion.Visible = true;
            lQuestion.DataSource = ds;
            lQuestion.DataBind();         
        }
        else
        {
            lQuestion.Items.Clear();
            lQuestion.Visible = false;
            lQuestion.DataSource = null;
            lQuestion.DataBind();
        }
    }
}
