using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Projects_Transfer_Module_Mapping : System.Web.UI.Page
{
    CourseController objCourse = new CourseController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    NewUserController newUser = new NewUserController();
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
            if (Session["userno"] == null || Session["username"] == null || Session["idno"] == null ||
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

                int Idno = Convert.ToInt32(Session["idno"].ToString());
                Session["IDNO"] = Idno;
                Page.Title = objCommon.LookUp("reff", "collegename", string.Empty);
               // BindListView();
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
            objCommon.SetLabelData("0");
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
            objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0 and ACTIVE=1", "COLLEGE_NAME");
            objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");

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
                Response.Redirect("~/notauthorized.aspx?page=Transfer_Module_Mapping.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page+
            Response.Redirect("~/notauthorized.aspx?page=Transfer_Module_Mapping.aspx");
        }
    }
    protected void BindListView()
    {
        try
        {
            string SP_Name2 = "PKG_ACD_GET_MODULE_TRANSFER_DATA";
            string SP_Parameters2 = "@P_COLLEGE_ID,@P_SEMESTERNO";
            string Call_Values2 = "" + Convert.ToInt32(ddlcollege.SelectedValue) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                Panel1.Visible = true;
                lvTransModule.DataSource = ds;
                lvTransModule.DataBind();
            }
            else 
            {
                objCommon.DisplayMessage(this, "Record Not Found", this.Page);
                Panel1.Visible = false;
                lvTransModule.DataSource = null;
                lvTransModule.DataBind();
                return;
            }  
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {

        LinkButton lnkView = sender as LinkButton;
        int IDNO = int.Parse(lnkView.CommandArgument);
        ViewState["IDNO"] = IDNO;
        
        int DegreeNo = Convert.ToInt32 (objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + IDNO));
        int Schemeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "isnull(SCHEMENO,0)SCHEMENO", "IDNO=" + IDNO));

        DataSet dss = objCommon.FillDropDown("ACD_STUDENT_RESULT  SR INNER JOIN ACD_COURSE  C ON(SR.COURSENO=C.COURSENO) LEFT JOIN ACD_COURSE_EQUIVALENCE TM ON(TM.IDNO=SR.IDNO AND TM.OLD_COURSENO = C.COURSENO )", "DISTINCT SR.COURSENO ", "(C.CCODE+'-'+C.COURSE_NAME) AS COURSENAME ,TM.NEW_COURSENO,TM.MAPPING_STATUS", "SR.IDNO=" + IDNO + "AND CLEAR=" + 1 + "", "");
        if (dss.Tables[0].Rows.Count > 0)
        {
            lvModule.DataSource = dss;
            lvModule.DataBind();
            foreach (ListViewDataItem dataitem in lvModule.Items)
            {
                DropDownList ddlNewModuleName = dataitem.FindControl("ddlNewModuleName") as DropDownList;
                DropDownList ddlMappingStatus = dataitem.FindControl("ddlMappingStatus") as DropDownList;
                Label lblNewCourse = dataitem.FindControl("lblNewCourse") as Label;
                Label lblModuleStat = dataitem.FindControl("lblModuleStat") as Label; 
                objCommon.FillDropDownList(ddlNewModuleName, "ACD_STUDENT S INNER JOIN  ACD_OFFERED_COURSE OC ON(S.DEGREENO=OC.DEGREENO AND S.SCHEMENO=OC.SCHEMENO) INNER JOIN ACD_COURSE C ON(C.COURSENO=OC.COURSENO)", "DISTINCT OC.COURSENO", "(OC.CCODE+'-'+ COURSE_NAME)",
                    "IDNO=" + IDNO + " AND S.DEGREENO=" + DegreeNo + "AND S.SCHEMENO=" + Schemeno + "", "");
                objCommon.FillDropDownList(ddlMappingStatus, "ACD_MAPPING_STATUS", "MAP_NO", "MAPPING_STATUS", "MAP_NO>0", "MAP_NO");
                if (lblNewCourse.Text != "")
                {
                    ddlNewModuleName.SelectedValue = (lblNewCourse.Text);
                }
                if (lblModuleStat.Text != "")
                {
                    ddlMappingStatus.SelectedValue = (lblModuleStat.Text);
                }           
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Map_Modal", "$(document).ready(function () {$('#Map_Modal').modal();});", true);
        }
        else
        {
            objCommon.DisplayMessage(this, "Record Not Found", this.Page);
            return;
        }
       
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        CustomStatus  cs=0;
        
        int Idno = Convert.ToInt32(ViewState["IDNO"].ToString());


        int SCHEME = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "isnull(SCHEMENO,0)SCHEMENO", "IDNO=" + Idno));
        DataSet ds = objCommon.FillDropDown("ACD_BRCHANGE", "IDNO", "REGNO,NEWREGNO,OLDBRANCHNO,NEWBRANCHNO,OLD_DEGREENO,NEW_DEGREENO", "IDNO=" + Idno, "");
        if (ds != null)
        {
           
            foreach (ListViewDataItem dataitem in lvModule.Items)
            {
                
                int Old_degree = Convert.ToInt32(ds.Tables[0].Rows[0]["OLD_DEGREENO"].ToString().Trim());
                int New_degree = Convert.ToInt32(ds.Tables[0].Rows[0]["NEW_DEGREENO"].ToString().Trim());
                int Old_branch = Convert.ToInt32(ds.Tables[0].Rows[0]["OLDBRANCHNO"].ToString().Trim());
                int New_branch = Convert.ToInt32(ds.Tables[0].Rows[0]["NEWBRANCHNO"].ToString().Trim());
                string newreg = Convert.ToString(ds.Tables[0].Rows[0]["NEWREGNO"].ToString().Trim());
                string oldreg = Convert.ToString(ds.Tables[0].Rows[0]["REGNO"].ToString().Trim());
                int Ua_No=Convert.ToInt32(Session["userno"].ToString());
                Label lblOldCourse = dataitem.FindControl("lblOldCourse") as Label;
                DropDownList ddlNewModuleName = dataitem.FindControl("ddlNewModuleName") as DropDownList;
                DropDownList ddlMappingStatus = dataitem.FindControl("ddlMappingStatus") as DropDownList;
                int IDNO=Convert.ToInt32(ViewState["IDNO"].ToString());
                int NewCourseNo=Convert.ToInt32(ddlNewModuleName.SelectedValue);
                int Mapping_status=Convert.ToInt32(ddlMappingStatus.SelectedValue);
                int Old_CourseNo=Convert.ToInt32(lblOldCourse.Text);
                string oldcredit = Convert.ToString(objCommon.LookUp("ACD_COURSE", "CREDITS", "COURSENO=" + Old_CourseNo));
                string NEWcredit = Convert.ToString(objCommon.LookUp("ACD_COURSE", "CREDITS", "COURSENO=" + NewCourseNo));
                string oldCCODE = Convert.ToString(objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + Old_CourseNo));
                string NEWCCODE = Convert.ToString(objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + NewCourseNo));
                int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "SESSIONNO", "IDNO=" + Idno));
                if (NewCourseNo != 0 && Mapping_status != 0)
                {
                    cs = (CustomStatus)objCourse.InsertTransferModuleMapping(sessionno, oldreg, newreg, IDNO, Old_degree, New_degree, Old_branch, New_branch,
                        Old_CourseNo, NewCourseNo, Mapping_status, Ua_No, SCHEME, SCHEME, oldCCODE, NEWCCODE, Convert.ToDecimal(oldcredit), Convert.ToDecimal(NEWcredit),Request.ServerVariables["REMOTE_ADDR"].ToString(),Convert.ToString(Session["colcode"]));

                }
            }
          
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this, "Record Saved Successfully.", this.Page);
                // BindListViewRuleAllocation();
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
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnShowList_Click(object sender, EventArgs e)
    {
        BindListView();
    }
    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        ddlsemester.SelectedIndex = 0;
        ddlcollege.SelectedIndex = 0;
        Panel1.Visible = false;
        lvTransModule.DataSource = null;
        lvTransModule.DataBind();
    }
}