using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS;
using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Block_Student_Registration : System.Web.UI.Page
{
    StudentController objCourse = new StudentController();
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

                Page.Title = objCommon.LookUp("reff", "collegename", string.Empty);

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["FEES_NO"] = "edit";
                string College_id = objCommon.LookUp("USER_ACC", "UA_COLLEGE_NOS", "UA_NO=" + Session["userno"].ToString());
                objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID in(" + College_id +")", "COLLEGE_NAME");
                //objCommon.FillDropDownList(ddlfaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_NAME");
            }

        }
        //objCommon.SetLabelData("0");
        //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Block_Student_Registration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Block_Student_Registration.aspx");
        }
    }


    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFaculty.SelectedValue != "0")
        {
            objCommon.FillDropDownList(ddlProgram, "ACD_STUDENT_RESULT SR INNER JOIN ACD_DEGREE D ON (D.DEGREENO=SR.DEGREENO)INNER JOIN ACD_BRANCH B ON (B.BRANCHNO=SR.BRANCHNO)", "DISTINCT (CONVERT(NVARCHAR(16),D.DEGREENO) + ',' + CONVERT(NVARCHAR(16),B.BRANCHNO))", "(DEGREENAME+' - '+LONGNAME)", "COLLEGE_ID=" + ddlFaculty.SelectedValue, "");
            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Please Select");
            divSession.Visible = false;
            btnSubmit.Visible = false;
            lvblockStudent.DataSource = null;
            lvblockStudent.DataBind();
        }
        else
        {
            ddlProgram.Items.Clear();
            ddlProgram.Items.Insert(0, "Please Select");
            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Please Select");
            divSession.Visible = false;
            btnSubmit.Visible = false;
            lvblockStudent.DataSource = null;
            lvblockStudent.DataBind();
        }
    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProgram.SelectedValue != "0")
        {
            string[] splitValue;
            splitValue = ddlProgram.SelectedValue.Split(',');
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (S.SEMESTERNO=SR.SEMESTERNO)", "DISTINCT sr.SEMESTERNO", "SEMESTERNAME", "COLLEGE_ID=" + ddlFaculty.SelectedValue + "AND DEGREENO=" + splitValue[0].ToString() + "AND BRANCHNO=" + splitValue[1].ToString(), "SEMESTERNAME");
            divSession.Visible = false;
            btnSubmit.Visible = false;
            lvblockStudent.DataSource = null;
            lvblockStudent.DataBind();
        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Please Select");
            divSession.Visible = false;
            btnSubmit.Visible = false;
            lvblockStudent.DataSource = null;
            lvblockStudent.DataBind();
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        string[] splitValue;
        splitValue = ddlProgram.SelectedValue.Split(',');
        string SP_Name2 = "PKG_GET_BLOCK_STUDENT_REGISTRATION";
        string SP_Parameters2 = "@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO,@P_CRITERIA";
        string Call_Values2 = "" + ddlFaculty.SelectedValue + "," + splitValue[0].ToString() + "," + splitValue[1].ToString() + ","+ddlSemester.SelectedValue+","+ddlCriteria.SelectedValue+"";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvblockStudent.DataSource = ds;
            lvblockStudent.DataBind();
            objCommon.FillDropDownList(ddlsession, "acd_session_master", "sessionno", "session_name", "", "session_name desc");
            divSession.Visible = true;
            btnSubmit.Visible = true;

        }
        else
        {
            lvblockStudent.DataSource = null;
            lvblockStudent.DataBind();
            divSession.Visible = false;
            btnSubmit.Visible = false;
            objCommon.DisplayMessage(this, "Record Not Found ", this.Page);
            return;
            
        }

     
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvblockStudent.DataSource = null;
        lvblockStudent.DataBind();
        divSession.Visible = false;
        btnSubmit.Visible = false;

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int Sessiono = Convert.ToInt32(ddlsession.SelectedValue);
        string[] splitValue;
        splitValue = ddlProgram.SelectedValue.Split(',');
        int College=Convert.ToInt32(ddlFaculty.SelectedValue);
        int Semester = Convert.ToInt32(ddlSemester.SelectedValue);
        int Count = 0;
        string Idno = string.Empty;
        string Regno = string.Empty;
        CustomStatus cs = 0;
        foreach (ListViewDataItem dataitem in lvblockStudent.Items)
        {
            CheckBox chkstud = dataitem.FindControl("chkstud") as CheckBox;
            if (chkstud.Checked == true)
            {
                Count++;
                Label lblRegno = dataitem.FindControl("lblRegno") as Label;
                Label lblIdno = dataitem.FindControl("lblIdno") as Label;
                Idno += lblIdno.Text + "$";
                Regno += lblRegno.Text + "$";
            }
        }
        if (Count == 0)
        {
            objCommon.DisplayMessage(this, "Please Select At List One Student", this.Page);
            return;
        }
        string SP_Name1 = "PKG_INSERT_BLOCK_STUDENT_REGISTRATION";
        string SP_Parameters1 = "@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO,@P_SESSIONNO,@P_REGNO,@P_IDNO,@P_UA_NO,@P_OUT";
        string Call_Values1 = "" + College + "," + Convert.ToInt32(splitValue[0].ToString()) + "," + Convert.ToInt32(splitValue[1].ToString())
            + "," + Semester + "," + Sessiono + "," +  Regno.ToString() + "," + Idno.ToString() + "," + Convert.ToInt32(Session["userno"]) + "," + 0 + "";

        string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//grade allotment
        if (que_out1 == "1")
        {
            objCommon.DisplayMessage(this, "Student Allocation done Successfully !!", this.Page);
            //       ddlRoom.SelectedValue = "0";
            // GetStudentList();

        }
        else
        {
            objCommon.DisplayMessage(this, "Server Error !!", this.Page);
            return;
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl.ToString());
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        Button ViewDta = sender as Button;
        int idno = int.Parse(ViewDta.CommandArgument);
        // int FEES_NO = int.Parse(ViewDta.ToolTip);
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH B ON (B.BRANCHNO=S.BRANCHNO) INNER JOIN ACD_SEMESTER SE ON (SE.SEMESTERNO=S.SEMESTERNO) ", "REGNO", "NAME_WITH_INITIAL,(CODE+'-'+LONGNAME) AS  PROGRAM,SEMESTERNAME", "IDNO=" + idno, "");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            txtStudentId.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
            txtStudentName.Text = ds.Tables[0].Rows[0]["NAME_WITH_INITIAL"].ToString();
            txtProgram.Text = ds.Tables[0].Rows[0]["PROGRAM"].ToString();
            txtSemester.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
        }
        if (ddlCriteria.SelectedValue == "1")
        {
            DataSet dss = objCommon.FillDropDown("ACD_STUDENT_RESULT_HIST S  INNER JOIN ACD_SEMESTER SE ON (SE.SEMESTERNO=S.SEMESTERNO) INNER JOIN ACD_COURSE C ON (C.COURSENO=S.COURSENO)", "distinct C.COURSENO", "C.CCODE,COURSE_NAME,SEMESTERNAME", "IDNO=" + idno + "and isnull(clear,0)=0 AND s.SEMESTERNO=" + ddlSemester.SelectedValue, "c.CCODE");
            if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            {
                lvPendingModule.DataSource = dss;
                lvPendingModule.DataBind();
            }
            divDemand.Visible = false;
            divTotalfees.Visible = false;
            divPaidfees.Visible = false;
        }
        else if (ddlCriteria.SelectedValue == "2")
        {
            DataSet dss = objCommon.FillDropDown("ACD_DCR ", "IDNO", "isnull(TOTAL_AMT,0) as TOTAL_AMT,isnull(DD_AMT,0) as DD_AMT", "IDNO=" + idno, "");
            divDemand.Visible = true;
            divTotalfees.Visible = true;
            divPaidfees.Visible = true;
            txtDemand.Text = dss.Tables[0].Rows[0]["TOTAL_AMT"].ToString();
            txtTotalPaidFee.Text = dss.Tables[0].Rows[0]["DD_AMT"].ToString();
            decimal Total = Convert.ToDecimal(txtDemand.Text) - Convert.ToDecimal(txtTotalPaidFee.Text);
            txtPaidFee.Text = Total.ToString();
            lvPendingModule.DataSource = null;
            lvPendingModule.DataBind();

        }
        else
        {
            divDemand.Visible = false;
            divTotalfees.Visible = false;
            divPaidfees.Visible = false;
            lvPendingModule.DataSource = null;
            lvPendingModule.DataBind();
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#myModal').modal('show')", true);
    }
}