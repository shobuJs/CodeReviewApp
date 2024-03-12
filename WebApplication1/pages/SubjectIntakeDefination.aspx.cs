using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
//using IITMS.NITPRM;
//using IITMS.NITPRM.BusinessLayer.BusinessEntities;
//using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS;
using System.Data;



public partial class ACADEMIC_SubjectIntakeDefination : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objstudent = new StudentController();
    DataTable ds = new DataTable();

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


                    this.PopulateDropDownList();

            }
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=teacherallotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=teacherallotment.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CDB.DEGREENO)", "DISTINCT (D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CDB.COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENO");
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {

        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON B.BRANCHNO = CDB.BRANCHNO", "B.BRANCHNO", "B.LONGNAME", "CDB.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");

        DataSet ds = objCommon.FillDropDown("ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
        string BranchNos = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            BranchNos += ds.Tables[0].Rows[i]["BranchNo"].ToString() + ",";
        }
        DataSet dsReff = objCommon.FillDropDown("REFF", "*", "", string.Empty, string.Empty);
        //on faculty login to get only those dept which is related to logged in faculty
        objCommon.FilterDataByBranch(ddlBranch, dsReff.Tables[0].Rows[0]["FILTER_USER_TYPE"].ToString(), BranchNos, Convert.ToInt32(dsReff.Tables[0].Rows[0]["BRANCH_FILTER"].ToString()), Convert.ToInt32(Session["usertype"]));
        ddlBranch.Focus();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "distinct SCHEMENO", "SCHEMENAME", "BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND DEGREENO =" + Convert.ToInt32(ddlDegree.SelectedValue), "SCHEMENO");
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int yearwise = objCommon.LookUp("ACD_DEGREE", "ISNULL(YEARWISE,0)", "DEGREENO=" + ddlDegree.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "ISNULL(YEARWISE,0)", "DEGREENO=" + ddlDegree.SelectedValue));

            ddlSemester.Items.Clear();
            string odd_even = objCommon.LookUp("acd_session_master", "odd_Even", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue));
            string exam_type = objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
            string semCount = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "CAST(DURATION AS INT)*2 AS DURATION", "DEGREENO=" + ddlDegree.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + "");
            if (exam_type == "1" && odd_even != "3")
            {
                if (yearwise == 1)
                {
                    objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_YEAR Y ON(S.YEAR=Y.YEAR)", "DISTINCT Y.YEAR", "Y.YEARNAME ", "Y.YEAR>0", "Y.YEAR");              
                }
                else
                {
                    objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.ODD_EVEN=" + odd_even + "AND SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNO");
                }
            }
            else
            {
                if (yearwise == 1)
                {
                    objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_YEAR Y ON(S.YEAR=Y.YEAR)", "DISTINCT Y.YEAR", "Y.YEARNAME ", "Y.YEAR>0", "Y.YEAR");
                }
                else
                {
                    objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNO");
                }
            }
            ddlSemester.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_batchallotment.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string semesterno = ddlSemester.SelectedValue == "1" ? "1,2" : ddlSemester.SelectedValue == "2" ? "3,4" : ddlSemester.SelectedValue == "3" ? "5,6" : ddlSemester.SelectedValue == "4" ? "7,8" : ddlSemester.SelectedValue == "5" ? "9,10" : ddlSemester.SelectedValue == "6" ? "11,12" : ddlSemester.SelectedValue;
            objCommon.FillDropDownList(ddlSubject, "ACD_OFFERED_COURSE AOC LEFT JOIN ACD_COURSE AC ON (AOC.COURSENO = AC.COURSENO)", "AOC.COURSENO", "AC.CCODE+ ' - ' +AC.COURSE_NAME AS COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND AOC.DEGREENO =" + ddlDegree.SelectedValue + " AND AOC.SCHEMENO =" + ddlScheme.SelectedValue + " AND AOC.SEMESTERNO IN(" + semesterno + ")", "AOC.COURSENO");
            ddlSubject.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_batchallotment.ddlSemester_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        binddata();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }


    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    protected void btnLock_Click(object sender, EventArgs e)
    {
        foreach (ListViewDataItem lvitem in ListView1.Items)
        {
            SQLHelper objsql = new IITMS.SQLServer.SQLDAL.SQLHelper(_nitprm_constr);
            //to get already exist intake
            DataSet ds = objsql.ExecuteDataSet("select INTAKE,CT_NO from ACD_COURSE_TEACHER where SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "and SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + "and SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "and COURSENO=" + Convert.ToInt32(ddlSubject.SelectedValue));

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //TextBox intake = lvitem.FindControl("txtLimit") as TextBox;
                //HiddenField hidctno = lvitem.FindControl("htnCTNO") as HiddenField;
                //if (intake.Text != "")
                //{
                CustomStatus cs = (CustomStatus)objstudent.updateFacultyLOCKIntake(Convert.ToInt32(ds.Tables[0].Rows[i]["CT_NO"].ToString()), 1);
                if (Convert.ToInt32(cs) == 2)
                {
                    binddata();
                    lblmessageShow.ForeColor = System.Drawing.Color.Green;
                    lblmessageShow.Text = "Record saved successfully !";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);

                    btnSubmit.Visible = false;
                    btnLock.Visible = false;
                }
                else
                {
                    lblmessageShow.ForeColor = System.Drawing.Color.Red;
                    lblmessageShow.Text = "Unable to save intake !";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                }
                //}
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        foreach (ListViewDataItem lvitem in ListView1.Items)
        {
            TextBox intake = lvitem.FindControl("txtLimit") as TextBox;
            HiddenField hidctno = lvitem.FindControl("htnCTNO") as HiddenField;
            if (intake.Text != "")
            {
                CustomStatus cs = (CustomStatus)objstudent.insertFacuiltyIntake(Convert.ToInt32(intake.Text), Convert.ToInt32(hidctno.Value));
                if (Convert.ToInt32(cs) == 2)
                {
                    binddata();
                    lblmessageShow.ForeColor = System.Drawing.Color.Green;
                    lblmessageShow.Text = "Record saved successfully !";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);

                    btnLock.Visible = true;
                }
                else
                {
                    lblmessageShow.ForeColor = System.Drawing.Color.Red;
                    lblmessageShow.Text = "Unable to save intake !";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                }
            }
        }
    }

    protected void binddata()
    {
        DataSet dslist = objstudent.getFacuilty(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSubject.SelectedValue));

        try
        {
            if (dslist != null && dslist.Tables.Count > 0 && dslist.Tables[0].Rows.Count > 0)
            {
                ListView1.DataSource = dslist;
                ListView1.DataBind();

                for (int i = 0; i < dslist.Tables[0].Rows.Count; i++)
                {
                    if (dslist.Tables[0].Rows[i]["LOCK_INTAKE"].ToString() == "0")
                    {
                        btnLock.Visible = false;
                        btnSubmit.Visible = true;
                    }
                }
            }
            else
            {
                ListView1.Visible = false;
                ListView1.DataSource = null;
                ListView1.DataBind();
                btnLock.Visible = false;
                btnSubmit.Visible = false;
                lblmessageShow.ForeColor = System.Drawing.Color.Red;
                lblmessageShow.Text = "No Record Found!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_batchallotment.ddlSemester_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSession.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND COLLEGE_ID=" + ddlCollege.SelectedValue, "SESSIONNO DESC");
        }
    }

}

