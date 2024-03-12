using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;

public partial class Academic_Awards : System.Web.UI.Page
{

    SessionController objSC = new SessionController();
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();
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

                DropDownBindConsession();
                
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
                Response.Redirect("~/notauthorized.aspx?page=Academic_Awards.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Academic_Awards.aspx");
        }
    }

    protected void DropDownBindConsession()
    {
        objCommon.FillDropDownList(ddlAwardTitle, "Acd_Award_Configuration", "AwardNo", "AwardTitle", "", "");
        objCommon.FillDropDownList(ddlSession, "acd_session_master", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO");
    }
    protected void ddlAwardTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        string degreeno = (objCommon.LookUp("Acd_Award_Configuration", "degreeno","AwardNo=" + (ddlAwardTitle.SelectedValue)));
        string BRANCHNO = (objCommon.LookUp("Acd_Award_Configuration", "BRANCHNO","AwardNo=" + (ddlAwardTitle.SelectedValue)));  

        string WGPA = (objCommon.LookUp("Acd_Award_Configuration", "Overall_WGPA", "AwardNo=" + (ddlAwardTitle.SelectedValue)));



       string Semester = (objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH A inner join Acd_Award_Configuration B on  A.DegreeNo= B.Degreeno", "DISTINCT convert(int,(DURATION * 2)) as semester", "A.DEGREENO=" + Convert.ToInt32(degreeno) + " AND A.BRANCHNO =" + Convert.ToInt32( BRANCHNO)));

        int SEMESTERNO = Convert.ToInt32(Semester);

  objCommon.FillDropDownList(ddlSemester, "acd_semester", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO=" + Convert.ToInt32(Semester), "");

    }

    protected void BindListViewAcdAward()
    {
        try
        {
            //DataSet ds = null;
            DataSet ds = null;
            string degreeno = (objCommon.LookUp("Acd_Award_Configuration", "degreeno", "AwardNo=" + (ddlAwardTitle.SelectedValue)));
            int SESSION= Convert.ToInt32(ddlSession.SelectedValue);

            string DEGREE1 = (objCommon.LookUp("Acd_Award_Configuration", "degreeno", "AwardNo=" + (ddlAwardTitle.SelectedValue)));
            int Degreeno = Convert.ToInt32(DEGREE1);

            string SEMESTER = (objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH A inner join Acd_Award_Configuration B on  A.DegreeNo= B.Degreeno", "convert(int,(DURATION * 2)) as semester", "A.DEGREENO=" + Convert.ToInt32(degreeno)));

            int SEMESTERNO = Convert.ToInt32(SEMESTER);

            string WGPA = (objCommon.LookUp("Acd_Award_Configuration", "Overall_WGPA", "AwardNo=" + (ddlAwardTitle.SelectedValue)));

            int UaNo = Convert.ToInt32(Session["userno"].ToString());

            //ds = objCommon.FillDropDown("acd_student S inner join acd_student_result_hist A on (S.idno=A.idno) inner join acd_TrResult R on (A.idno=R.Idno AND A.SESSIONNO=R.SESSIONNO) inner join User_acc U on (U.UA_NO =) inner join acd_degree D on (D.degreeno=S.DegreeNo)inner join acd_branch B on (B.BRANCHNO= S.BranchNo)", "DISTINCT U.UA_FULLNAME as Faculty", "(D.DEGREENAME +'-'+ B.LONGNAME) AS PROGRAM, S.RegNo, (S.STUDNAME) as Student_Name,S.SEMESTERNO,R.WGPA", "A.sessionno=" + Convert.ToInt32(SESSION) + " and A.semesterno=" + Convert.ToInt32(SEMESTER) + " and degreeno=" + Convert.ToInt32(Degreeno) + " and AND R.WGPA IS NULL", "");

            ds = objCommon.ShowAcademic_Awards(SESSION, Degreeno, SEMESTERNO, WGPA, UaNo);


            if (ds.Tables[0].Rows.Count > 0)
            {

                // DivViewData.Visible = false;
                lvAcdAward.DataSource = ds;
                lvAcdAward.DataBind();
            }

            else
            {

                lvAcdAward.DataSource = null;
                lvAcdAward.DataBind();

            }
        }
        catch
        {
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListViewAcdAward();
    }
   
    //protected void btnView_Click(object sender, EventArgs e)
    //{
    //    //LinkButton lnk = sender as LinkButton;

    //    int Award_No = Convert.ToInt32(ddlAwardTitle.SelectedValue);

    //    PopupDetails(Award_No);

    //    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(System.Web.UI.Page), "Script", "ShowPUP();", true);
    //}





    private void PopupDetails(int AwardNo)
    {
        try
        {
            DataSet ds = null;

            ds = objCommon.FillDropDown("Acd_Award_Configuration AW inner join ACD_COLLEGE_MASTER CM on (AW.COLLEGEID=CM.COLLEGE_ID) inner join acd_degree D on (D.degreeno=AW.DegreeNo)inner join acd_branch B on (B.BRANCHNO= AW.BranchNo) inner join ACD_AFFILIATED_UNIVERSITY AU on (AU.AFFILIATED_NO= AW.Affilidated_No)", "AW.AwardNo,AW.AwardTitle", "CM.COLLEGE_NAME,D.CODE,B.longname,CONCAT(D.CODE,',',B.longname)AS PROGRAM,AFFILIATED_LONGNAME,AW.Postponement,AW.GraduatedStudents,AW.ICStatus,AW.RepeatAttempts,AW.Highest_WGPA", "AwardNo=" + AwardNo, "AwardNo");


            if (ds.Tables[0].Rows.Count > 0)
            {

                RadioButtonList1.SelectedValue = ds.Tables[0].Rows[0]["Postponement"].ToString();
                RadioButtonList2.SelectedValue = ds.Tables[0].Rows[0]["GraduatedStudents"].ToString();
                RadioButtonList3.SelectedValue = ds.Tables[0].Rows[0]["ICStatus"].ToString();
                RadioButtonList4.SelectedValue = ds.Tables[0].Rows[0]["RepeatAttempts"].ToString();
                RadioButtonList5.SelectedValue = ds.Tables[0].Rows[0]["Highest_WGPA"].ToString();


            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseCreation.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
       
        LinkButton lnk = sender as LinkButton;

        int Award_No = Convert.ToInt32(ddlAwardTitle.SelectedValue);
       hdnAwardId.Value = Award_No.ToString();

        PopupDetails(Award_No);

        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(System.Web.UI.Page), "Script", "ShowPUP();", true);


    }
}