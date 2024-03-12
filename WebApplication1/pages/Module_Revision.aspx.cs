//======================================================================================
// PROJECT NAME  : SLIIT                                                          
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : MODULE REVISION                                      
// CREATION DATE : 22/02/2022                                                       
// CREATED BY    :Roshan Patil                                        
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Projects_Module_Revision : System.Web.UI.Page
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
            
                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNAME");
                objCommon.FillDropDownList(ddlFromYear, "ACD_EXAM_YEAR", "EXYEAR_NO", "YEAR_NAME", "EXYEAR_NO>9", "YEAR_NAME");
                objCommon.FillDropDownList(ddlToYear, "ACD_EXAM_YEAR", "EXYEAR_NO", "YEAR_NAME", "EXYEAR_NO>9", "YEAR_NAME");
                Page.Title = objCommon.LookUp("reff", "collegename", string.Empty);
                ViewState["courseNo"] = "null";
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
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
                Response.Redirect("~/notauthorized.aspx?page=Module_Revision.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Module_Revision.aspx");
        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, System.EventArgs e)
    {
       //objCommon.FillDropDownList(ddlModule, "ACD_COURSE C INNER JOIN ACD_DEPARTMENT D ON (C.DEPTNO=D.DEPTNO)", "DISTINCT C.COURSENO", "CCODE+' - '+UPPER(COURSE_NAME) AS COURSENAME", "D.DEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue), "COURSENAME");

        DataSet dss = objCourse.CourseBindDropDownList(Convert.ToInt32(ddlDepartment.SelectedValue));
        ddlModule.Items.Clear();
        ddlModule.Items.Add("Please Select");
        ddlModule.SelectedItem.Value = "0";

        ddlModule.DataSource = dss.Tables[0];
        ddlModule.DataTextField = "COURSENAME";
        ddlModule.DataValueField = "COURSENO";
        ddlModule.DataBind();
        //ddlModule.SelectedIndex = 0;
    
        lvCourseRevision.Visible = true;
        MyDiv.Visible = false;
        btnSubmit.Visible = true;
        btnCancel.Visible = true;
        txtModuleName.Text = "";
  
        txtCredits.Text = "";
        DataSet ds = objCourse.GetModuleRevision(Convert.ToInt32(ddlDepartment.SelectedValue));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        // if (ds != null & ds.Tables.Count > 0)
        {
            lvCourseRevision.DataSource = ds;
            lvCourseRevision.DataBind();

        }
        else
        {
            lvCourseRevision.DataSource = null;
            lvCourseRevision.DataBind();
           
        }

        foreach (ListViewDataItem dataitem in lvCourseRevision.Items)
        {
            Label Status = dataitem.FindControl("status") as Label;
            string Statuss = (Status.Text);
            if (Statuss == "INACTIVE")
            {
                Status.CssClass = "badge badge-danger";
            }
            else
            {
                Status.CssClass = "badge badge-success";
            }

        }
    }
    protected void Databind()
    {
      //  DataSet ds = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (S.SUBID=C.SUBID)", "*", "COURSENO,SUBNAME", "COURSENO=" + Convert.ToInt32(ddlModule.SelectedValue), "");
       string  CourseNo=(ViewState["courseNo"].ToString());

       DataSet ds = null;
       if (CourseNo.ToString() != "null")
       {
           ds = objCourse.CourseBindTEXT_BOX(Convert.ToInt32(CourseNo));
       }
       else
       {
          ds = objCourse.CourseBindTEXT_BOX(Convert.ToInt32(ddlModule.SelectedValue));
       }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow drs = ds.Tables[0].Rows[0];

            string moduleCode = drs["CCODE"].ToString();
            lblModuleCode.Text = moduleCode;
            string Modulename = drs["COURSENAME"].ToString();
            lblModuleName.Text = Modulename;
            decimal Credite = Convert.ToDecimal(drs["CREDITS"].ToString());
            lblCredits.Text = Credite.ToString();
            string TYPEs = (drs["SUBNAME"].ToString());
            lblType.Text = TYPEs;
            string CCODE = (drs["CCODE"].ToString());
            txtModuleCode.Text = CCODE;
            int SubId = Convert.ToInt32(drs["SUB_ID"].ToString());
            lblSubID.Text = SubId.ToString();
            MyDiv.Visible = true;
            btnSubmit.Visible = true;
            btnCancel.Visible = true;
           // ViewState["MODULE"] = null;
            lvCourseRevision.Visible = true;
        }
        else
        {
        }

    }
  
    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnSubmit_Click(object sender, System.EventArgs e)
    {
        try
        {
            int deptno = Convert.ToInt32(ddlDepartment.SelectedValue);
            int courseNo = Convert.ToInt32(ddlModule.SelectedValue);
            string old_CCode = lblModuleCode.Text;
            string new_CCode = txtModuleCode.Text;
            string old_ModuleName = lblModuleName.Text;
            string new_modulename = txtModuleName.Text;
            decimal oldCredit = Convert.ToDecimal(lblCredits.Text);
            int  SubId = Convert.ToInt32(lblSubID.Text);
            int fromYear = Convert.ToInt32(ddlFromYear.SelectedValue);
            int ToYear = Convert.ToInt32(ddlToYear.SelectedValue);
            decimal newCredit = Convert.ToDecimal(txtCredits.Text);
            int UANO = Convert.ToInt32(Session["userno"].ToString());

            int Revno = 0;
            if (ViewState["REVNO"] != null)
            {
               Revno= Convert.ToInt32(ViewState["REVNO"].ToString());
            }
            CustomStatus cs = (CustomStatus)objCourse.InsertModeleRevision(deptno, courseNo, old_CCode, new_CCode, old_ModuleName, new_modulename, oldCredit, newCredit, UANO, Revno, fromYear, ToYear, SubId);

            if (cs == CustomStatus.RecordSaved)
            {
                objCommon.DisplayMessage(this, "Record Saved Successfully !!", this.Page);
               // ddlModule.SelectedValue = "0";
                
                ViewState["courseNo"] = courseNo;
                BindListView();
                ddlDepartment_SelectedIndexChanged(new object(), new EventArgs());
              
                ddlModule_SelectedIndexChanged(new object(), new EventArgs());
                this.Databind();
               
                txtCredits.Text = "";
               // txtModuleCode.Text =;
                txtModuleName.Text = "";
                ddlFromYear.SelectedValue = "0";
                ddlToYear.SelectedValue = "0";
               
            }
            else
            {
                objCommon.DisplayMessage(this, "Server Error", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Module_Revision.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindListView()
    {
        try
        {
           
            int course=0;
            course=Convert.ToInt32(ddlModule.SelectedValue);
            string Courseno= ViewState["courseNo"].ToString();
            DataSet dss = null;
            if (Courseno != "null")
            {
                dss = objCourse.GetModuleRevisionCoursewise(Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(Courseno));
            }
            else
            {
                dss = objCourse.GetModuleRevisionCoursewise(Convert.ToInt32(ddlDepartment.SelectedValue), course);
            }
           // DataSet dss = objCourse.GetModuleRevisionCoursewise(Convert.ToInt32(ddlDepartment.SelectedValue), course);
            if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            // if (ds != null & ds.Tables.Count > 0)
            {
                lvCourseRevision.DataSource = dss;
                lvCourseRevision.DataBind();
            }
            else
            {
                lvCourseRevision.DataSource = null;
                lvCourseRevision.DataBind();
            }

            foreach (ListViewDataItem dataitem in lvCourseRevision.Items)
            {
                Label Status = dataitem.FindControl("status") as Label;
                string Statuss = (Status.Text);
                if (Statuss == "INACTIVE")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Module_Revision.btnSubmit_Click->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            
            ImageButton btnEdit = sender as ImageButton;
            int REVNO = int.Parse(btnEdit.CommandArgument);
            ViewState["REVNO"] = REVNO;
            ViewState["action"] = "edit";
           // HiddenField1.Value = int.Parse(btnEdit.CommandArgument).ToString();
            DataSet ds = objCommon.FillDropDown("ACD_MODULE_REVISION", "*", "REVNO", "REVNO=" + REVNO, "");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["DEPTNO"].ToString();
                ddlDepartment_SelectedIndexChanged(new object(),new EventArgs());
                ddlModule.SelectedValue= ds.Tables[0].Rows[0]["COURSENO"].ToString();
                 lblModuleCode.Text = ds.Tables[0].Rows[0]["OLD_CCODE"].ToString();
                 lblModuleName.Text = ds.Tables[0].Rows[0]["OLD_COURSENAME"].ToString();
                 lblCredits.Text= ds.Tables[0].Rows[0]["OLD_CREDITS"].ToString();
                 txtModuleCode.Text = ds.Tables[0].Rows[0]["NEW_CCODE"].ToString();
                 txtModuleName.Text = ds.Tables[0].Rows[0]["NEW_COURSENAME"].ToString();
                 txtCredits.Text= ds.Tables[0].Rows[0]["NEW_CREDITS"].ToString();
                 ddlFromYear.SelectedValue = ds.Tables[0].Rows[0]["FROM_YEAR"].ToString();
                 ddlToYear.SelectedValue = ds.Tables[0].Rows[0]["TO_YEAR"].ToString();
                 MyDiv.Visible = true;
                 btnSubmit.Visible = true;
                 btnCancel.Visible = true;
                 lvCourseRevision.Visible = true;
                 BindListView();
               
            }
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_CollegeMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void ddlModule_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        string  CourseNo=(ViewState["courseNo"].ToString());
        BindListView();

        if (CourseNo.ToString() != "null")
        {
            ViewState["MODULE"] = ddlModule.SelectedValue;
            DataSet ds = objCourse.CourseBindTEXT_BOX(Convert.ToInt32(CourseNo));
            DataRow drs = ds.Tables[0].Rows[0];

            string moduleCode = drs["CCODE"].ToString();
            lblModuleCode.Text = moduleCode;
            string Modulename = drs["COURSENAME"].ToString();
            lblModuleName.Text = Modulename;
            decimal Credite = Convert.ToDecimal(drs["CREDITS"].ToString());
            lblCredits.Text = Credite.ToString();
            string moduleCodes = drs["CCODE"].ToString();
            txtModuleCode.Text = moduleCodes;
            int SubId = Convert.ToInt32(drs["SUB_ID"].ToString());
            lblSubID.Text = SubId.ToString();
            Databind();
            txtModuleName.Text = "";
            txtCredits.Text = "";
            ViewState["courseNo"] = "null";
        }
        else if (ddlModule.SelectedValue == "0")
        {
            lvCourseRevision.Visible = true;
            MyDiv.Visible = false;
            btnSubmit.Visible = false;
            btnCancel.Visible = false;
        }
        else
        {
            DataSet ds = objCourse.CourseBindTEXT_BOX(Convert.ToInt32(ddlModule.SelectedValue));
            DataRow drs = ds.Tables[0].Rows[0];
            string moduleCode = drs["CCODE"].ToString();
            lblModuleCode.Text = moduleCode;
            string Modulename = drs["COURSENAME"].ToString();
            lblModuleName.Text = Modulename;
            decimal Credite = Convert.ToDecimal(drs["CREDITS"].ToString());
            lblCredits.Text = Credite.ToString();
            string moduleCodes = drs["CCODE"].ToString();
            txtModuleCode.Text = moduleCodes;
            Databind();
            txtModuleName.Text = "";
            txtCredits.Text = "";
            ViewState["courseNo"] = "null";
        }
    }
}