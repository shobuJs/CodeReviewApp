//======================================================================================
// PROJECT NAME  : RFC SVCE                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Scheme Allotment                                    
// CREATION DATE : 20/05/19
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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_SchemeAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
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
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
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
                else
                {
                }
                int mul_col_flag = Convert.ToInt32(objCommon.LookUp("REFF", "MUL_COL_FLAG", "CollegeName='" + Page.Title + "'"));
                if (mul_col_flag == 0)
                {
                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    ddlCollege.SelectedIndex = 1;
                    clg.Visible = false;
                }
                else
                {
                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    ddlCollege.SelectedIndex = 0;
                }
                PopulateDropDownList();
                ViewState["degreeno"] = 0;
                ViewState["branchno"] = 0;
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header

            }
        }
    }
    //bind COLLEGE NAME, DEGREE NAME,BATCHNAME , SCHEME TYPE  in drop down list. 
    private void PopulateDropDownList()
    {
        try
        {
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            //objCommon.FillDropDownList(ddlDegree, "ACD_SCHEME S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=S.BRANCHNO)", "CONCAT(D.DEGREENO, ', ',B.BRANCHNO)AS ID", "(D.DEGREENAME+'-'+B.LONGNAME)AS DEGBRANCH", "S.DEGREENO>0", "S.DEGREENO");

            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON (A.BRANCHNO=B.BRANCHNO) INNER JOIN ACD_DEGREE C ON (C.DEGREENO=A.DEGREENO) INNER JOIN ACD_AFFILIATED_UNIVERSITY D on (D.AFFILIATED_NO=A.AFFILIATED_NO) INNER JOIN ACD_STUDENT_RESULT SR ON(SR.DEGREENO=A.DEGREENO AND SR.BRANCHNO=A.BRANCHNO)", "DISTINCT (CONVERT(NVARCHAR(16),A.DEGREENO ))+'$'+(CONVERT(NVARCHAR(16),B.BRANCHNO))+'$'+(CONVERT(NVARCHAR(16), A.AFFILIATED_NO))", "(DEGREENAME +'-'+LONGNAME+'-'+AFFILIATED_SHORTNAME) as PROGRAME", " A.DEGREENO>0 AND ISNULL(A.ACTIVE,0)=1 ", "");



            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNAME DESC");
            objCommon.FillDropDownList(ddlSType, "ACD_SCHEMETYPE", "SCHEMETYPENO", "SCHEMETYPE", "SCHEMETYPENO > 0", "SCHEMETYPE");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_schemeAllotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    ////On select of Batch Year bind Branch name in drop down list
    protected void ddlBatchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_BATCHNO", Convert.ToInt32(ddlBatchYear.SelectedValue));

            SqlDataReader dr = objSQLHelper.ExecuteReaderSP("PKG_DROPDOWN_SP_RET_BRANCH_BYBATCH", objParams);

            //ddlBranch.Items.Clear();
            //ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            //while (dr.Read())
               // ddlBranch.Items.Add(new ListItem(dr["LongName"].ToString(), dr["BranchNo"].ToString()));
                //int branchno = Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[1]);
            //dr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_schemeAllotment.ddlBatchYear_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //On select of Branch bind Scheme name in drop down list
    //protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //Populate Scheme
    //    try
    //    {
    //        objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO=" + ddlDegree.SelectedValue + " and BRANCHNO=" + ddlBranch.SelectedValue + "", "SCHEMENO");
    //        lvStudents.DataSource = null;
    //        lvStudents.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_schemeAllotment.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}
    //display student details for scheme allotment 

    protected void btnShowStudent_Click(object sender, EventArgs e)
    {
        BindListView();
    }
    // bind selected criteria student in lsit view.
    private void BindListView()
    {
        ViewState["degreeno"] = Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[0]);      
        ViewState["branchno"] = Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[1]);
    
        hdfTot.Value = "0";
        txtTotStud.Text = "0";
        try
        {
            StudentController objSC = new StudentController();
            DataSet ds = objSC.GetStudentsBySchemeAllot(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ViewState["branchno"].ToString()), Convert.ToInt32(ViewState["degreeno"].ToString()), Convert.ToInt32(ddlSType.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue));

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                lblSch.Visible = true;
                ddlScheme.Enabled = true;
                lvStudents.Visible = true;
                lblStatus.Text = string.Empty;
            }
            else
            {
                objCommon.DisplayMessage(updScheme, "No Students for selected criteria!!", this.Page);
                lblStatus.Text = "No Students for selected criteria";
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                lblSch.Visible = false;
                ddlScheme.Enabled = false;
                lvStudents.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_schemeAllotment.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //refresh the drop down and label and list view
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlCollege.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBatchYear.SelectedIndex = 0;
        //ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        txtTotStud.Text = "0";
        hdfTot.Value = "0";
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = false;
        lblSch.Visible = false;
        ddlScheme.SelectedIndex = 0;
        ddlScheme.Enabled = false;
        lblStatus.Text = string.Empty;
        lblStatus2.Text = string.Empty;
    }

    //used for assigne the scheme for selected student
    protected void btnAssignSch_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        Student_Acd objStudent = new Student_Acd();
        objStudent.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
        objStudent.Sem = ddlSemester.SelectedValue;

        foreach (RepeaterItem lvItem in lvStudents.Items)
        {
            CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
            if (chkBox.Checked == true)
                objStudent.StudId += chkBox.ToolTip + ",";
        }

        if (objSC.UpdateSchemes(objStudent) != -99)
        {
            objCommon.DisplayMessage(updScheme, "Curriculum Alloted Successfully!!", this.Page);
            //lblStatus2.Text = "Regulation Alloted Successfully";
            BindListView();
        }
        else
            objCommon.DisplayMessage(updScheme, "Error in Alloting Curriculum!", this.Page);
          //  lblStatus2.Text = "Error in Alloting Regulation";
    }
    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page. 
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=schemeallotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=schemeallotment.aspx");
        }
    }

    //On select of Degree bind Branch name ,SEMESTER NAME in drop down list
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlDegree.SelectedValue != null && ddlCollege.SelectedValue != "0")
        if (ddlDegree.SelectedValue != null && ddlDegree.SelectedValue != "0")
        {
            int degreeno = Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[0]);

            int branchno = Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[1]);

            int Affilation = Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[2]);

            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue.ToString() + " AND B.COLLEGE_ID=" + ddlCollege.SelectedValue, "A.LONGNAME");

            //DataSet ds = objCommon.FillDropDown("ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
            //string BranchNos = "";
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    BranchNos += ds.Tables[0].Rows[i]["BranchNo"].ToString() + ",";
            //}
            //DataSet dsReff = objCommon.FillDropDown("REFF", "*", "", string.Empty, string.Empty);
            ////on faculty login to get only those dept which is related to logged in faculty
            //objCommon.FilterDataByBranch(ddlBranch, dsReff.Tables[0].Rows[0]["FILTER_USER_TYPE"].ToString(), BranchNos, Convert.ToInt32(dsReff.Tables[0].Rows[0]["BRANCH_FILTER"].ToString()), Convert.ToInt32(Session["usertype"]));
            //ddlBranch.Focus();

            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO=" + degreeno + " and BRANCHNO=" + branchno + "", "SCHEMENO");
         
            //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            ddlAdmBatch.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            ViewState["YearWise"] = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "ISNULL(YEARWISE,0)YEARWISE", "DEGREENO=" + degreeno));
            if (ViewState["YearWise"].ToString() == "1")
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_YEAR", "SEM", "YEARNAME", "YEAR<>0", "YEAR");
            }
            else
            {
                //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO<>0", "SEMESTERNO");
                ddlSemester.Items.Clear();
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO<>0", "SEMESTERNO");
                ddlSemester.Focus();
            }
            ddlAdmBatch.Focus();
        }
        else
        {
            objCommon.DisplayMessage(updScheme, "Please Select Degree", this.Page);
            ddlDegree.SelectedIndex = 0;
            ddlDegree.Focus();
        }
    }

    protected void lvStudents_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        hdfTot.Value = (Convert.ToInt16(hdfTot.Value) + 1).ToString();

        //Label elective = (e.Item.FindControl("lblCourseName")) as Label;
        //if (elective.ToolTip == "False")
        //{
        //    ((e.Item.FindControl("cbRow")) as CheckBox).Checked = true;
        //}
        //else
        //{
        //    ((e.Item.FindControl("cbRow")) as CheckBox).Checked = false;
        //}
    }
    //On select of College  bind Degree name in drop down list
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_BRANCH BR ON(B.BRANCHNO=A.BRANCHNO) INNER JOIN  ACD_COLLEGE_DEGREE B ON A.DEGREENO=B.DEGREENO INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO = A.DEGREENO)", "DISTINCT(A.DEGREENO)", "(A.DEGREENAME+'-'+BR.LONGNAME)AS DEGREENAME", "B.COLLEGE_ID=" + ddlCollege.SelectedValue, "A.degreeno");
        //objCommon.FillDropDownList(ddlDegree, "ACD_SCHEME S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=S.BRANCHNO)", "CONCAT(D.DEGREENO, ', ',B.BRANCHNO)AS ID", "(D.DEGREENAME+'-'+B.LONGNAME)AS DEGBRANCH", "S.DEGREENO>0 AND B.COLLEGE_ID=" + ddlCollege.SelectedValue, "S.DEGREENO");       
        if (ddlCollege.SelectedIndex > 0)
        {
           // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO = A.DEGREENO) INNER JOIN ACD_BRANCH BR ON (BR.BRANCHNO=cd.BRANCHNO) INNER JOIN  ACD_COLLEGE_DEGREE B ON A.DEGREENO=B.DEGREENO", "DISTINCT CONCAT(A.DEGREENO, ', ',BR.BRANCHNO)AS ID", "(A.DEGREENAME+'-'+BR.LONGNAME)AS DEGREENAME", "B.COLLEGE_ID=" + ddlCollege.SelectedValue, "DEGREENAME");

            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON (A.BRANCHNO=B.BRANCHNO) INNER JOIN ACD_DEGREE C ON (C.DEGREENO=A.DEGREENO) INNER JOIN ACD_AFFILIATED_UNIVERSITY D on (D.AFFILIATED_NO=A.AFFILIATED_NO) INNER JOIN ACD_STUDENT SR ON(SR.DEGREENO=A.DEGREENO AND SR.BRANCHNO=A.BRANCHNO)", "DISTINCT (CONVERT(NVARCHAR(16),A.DEGREENO ))+','+(CONVERT(NVARCHAR(16),B.BRANCHNO))+','+(CONVERT(NVARCHAR(16), A.AFFILIATED_NO))", "(DEGREENAME +'-'+LONGNAME+'-'+AFFILIATED_SHORTNAME) as PROGRAME", "A.COLLEGE_ID="+ ddlCollege.SelectedValue +"AND A.DEGREENO>0 AND ISNULL(A.ACTIVE,0)=1", "");



            ddlDegree.Focus();
        }
        else
        {
            ddlDegree.SelectedIndex = 0;
            ddlAdmBatch.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            objCommon.DisplayMessage(updScheme, "Please Select College", this.Page);
            ddlCollege.Focus();
        }
    }   
}
