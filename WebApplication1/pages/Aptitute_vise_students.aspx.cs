using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Web.Configuration;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Security;
using mastersofterp;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
public partial class ACADEMIC_Aptitute_vise_students : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController stud = new StudentController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
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
                     this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
               

                    FilldropDown();
                    HigherSemester();
                    //excellist();

                }
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
       
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACDEMIC_COLLEGE_MASTER.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Aptitute_vise_students.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Aptitute_vise_students.aspx");
        }
    }

    private void FilldropDown()
    {
        objCommon.FillDropDownList(ddlintake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVE = 1", "BATCHNO DESC");
        objCommon.FillDropDownList(ddladmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVE = 1", "BATCHNO DESC");

        objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME,(CASE WHEN COLLEGE_TYPE = 'S' THEN 'School' ELSE 'Faculty' END) AS COLLEGETYPE,SHORT_NAME,CODE,COLLEGE_ADDRESS,(CASE WHEN ACTIVE = 1 THEN 'Active' WHEN ACTIVE = 0 THEN 'Inactive' ELSE '-' END) AS ACTIVE", "COLLEGE_ID IN(SELECT VALUE FROM DBO.SPLIT('" + Session["college_nos"] + "',','))", "COLLEGE_ID");
        objCommon.FillDropDownList(ddlfacultyname, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME,(CASE WHEN COLLEGE_TYPE = 'S' THEN 'School' ELSE 'Faculty' END) AS COLLEGETYPE,SHORT_NAME,CODE,COLLEGE_ADDRESS,(CASE WHEN ACTIVE = 1 THEN 'Active' WHEN ACTIVE = 0 THEN 'Inactive' ELSE '-' END) AS ACTIVE", "COLLEGE_ID IN(SELECT VALUE FROM DBO.SPLIT('" + Session["college_nos"] + "',','))", "COLLEGE_ID");
        objCommon.FillDropDownList(ddlSemsterName, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNAME");
    }

    protected void rdbFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlintake.SelectedIndex==0)
        {
            objCommon.DisplayMessage(this.Page, "Please Select Intake", this.Page);
        }
        else
        {
            if (rdbFilter.SelectedValue == "1")
            {
                bindaptidata();
                lvtotalaplied.DataSource = null;
                lvtotalaplied.DataBind();
            }
            else if (rdbFilter.SelectedValue == "2")
            {
                bindapplieddata();                     
            
                divapti.Visible = false;
                lvapti.DataSource = null;
                lvapti.DataBind();
            }
        }
    }
    private void bindapplieddata()
    {
        DataSet ds = null;
        ds = stud.getaplieddeatils(Convert.ToInt32(ddlintake.SelectedValue));
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            divtotalapplied.Visible = true;
            lvtotalaplied.DataSource = ds;
            lvtotalaplied.DataBind();
        }

        else
        {
            lvtotalaplied.DataSource = null;
            lvtotalaplied.DataBind();
            objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
        }
           
    }
    private void bindaptidata()
    {
        DataSet ds = null;
        ds = stud.getaptideatils(Convert.ToInt32(ddlintake.SelectedValue));
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            divapti.Visible = true;
            lvapti.DataSource = ds;
            lvapti.DataBind();
        }
        else
        {
            lvapti.DataSource = null;
            lvapti.DataBind();
            objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
        }
    }
    
    protected void lkapplied_Click(object sender, EventArgs e)
    {
        divaplied.Attributes.Add("class", "active");
        divreport.Attributes.Remove("class");
        divHigher.Attributes.Remove("class");
        divreporttab.Visible = false;
        divapliedtab.Visible = true;
        divHigherSem.Visible = false;

        divreportlv.Visible = false;
        lvreport.DataSource = null;
        lvreport.DataBind();
        lvhigherSemester.DataSource = null;
        lvhigherSemester.DataBind();
        divreportlv.Visible = false;
        divListHigher.Visible = false;
        ddlprogram.SelectedIndex = 0;
        ddladmbatch.SelectedIndex = 0;
        ddlcollege.SelectedIndex = 0;
        ddlfacultyname.SelectedIndex = 0;
        ddlSemsterName.SelectedIndex = 0;
        ddlProgramName.SelectedIndex = 0;



    }
    protected void lkreport_Click(object sender, EventArgs e)
    {
        divreport.Attributes.Add("class", "active");
        divaplied.Attributes.Remove("class");
        divHigher.Attributes.Remove("class");
        divreporttab.Visible = true;
        divapliedtab.Visible = false;
        divHigherSem.Visible = false;

        lvapti.DataSource = null;
        lvapti.DataBind();
        lvhigherSemester.DataSource = null;
        lvhigherSemester.DataBind();
        divapti.Visible = false;
        divreportlv.Visible = false;
        divListHigher.Visible = false;
        ddlintake.SelectedIndex = 0;
        ddlfacultyname.SelectedIndex = 0;
        ddlSemsterName.SelectedIndex = 0;
        ddlProgramName.SelectedIndex = 0;

    }
    protected void HigherSemester()
    {
        divHigher.Attributes.Add("class", "active");
        divaplied.Attributes.Remove("class");
        divreport.Attributes.Remove("class");
        divreporttab.Visible = false;
        divapliedtab.Visible = false;
        divHigherSem.Visible = true;
        lvreport.DataSource = null;
        lvreport.DataBind();
        lvapti.DataSource = null;
        lvapti.DataBind();
        divapti.Visible = false;
        divListHigher.Visible = false;
        ddlintake.SelectedIndex = 0;
        ddlprogram.SelectedIndex = 0;
        ddladmbatch.SelectedIndex = 0;
        ddlcollege.SelectedIndex = 0;
    }
    protected void btnHithersemster_Click(object sender, EventArgs e)
    {
        HigherSemester();
        //divHigher.Attributes.Add("class", "active");
        //divaplied.Attributes.Remove("class");
        //divreport.Attributes.Remove("class");
        //divreporttab.Visible = false;
        //divapliedtab.Visible = false;
        //divHigherSem.Visible = true;
        //lvreport.DataSource = null;
        //lvreport.DataBind();
        //lvapti.DataSource = null;
        //lvapti.DataBind();
        //divapti.Visible = false;
        //divListHigher.Visible = false;
        //ddlintake.SelectedIndex = 0;
        //ddlprogram.SelectedIndex = 0;
        //ddladmbatch.SelectedIndex = 0;
        //ddlcollege.SelectedIndex = 0;
    }
    protected void btnshow_Click(object sender, EventArgs e)   
    {
        divaplied.Attributes.Remove("class");
        string[] program;
        if (ddlprogram.SelectedValue == "0")
        {
            program = "0,0,0".Split(',');
        }
        else
        {
            program = ddlprogram.SelectedValue.Split(',');
        }

                string SP_Name1 = "PKG_ACD_GET_TRANSFER_INTAKE_TOTAL_STUDNET";
                string SP_Parameters1 = "@P_INTAKE,@P_COLLGE_ID,@P_DEGREENO,@P_BRANCHNO";
                string Call_Values1 = "" + ddladmbatch.SelectedValue + "," + ddlcollege.SelectedValue + "," + Convert.ToInt32(program[0]) + "," + Convert.ToInt32(program[1]) + "";

                DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);
                if (dsStudList.Tables[0].Rows.Count > 0)
                {
                    lblIntakeOldCount.Text = dsStudList.Tables[0].Rows[0]["TotalStudent"].ToString();
                }
                      
               string SP_Name2 = "PKG_ACD_GET_TRANSFER_INTAKE_TOTAL_TRANSFER_STUDNET";
               string SP_Parameters2 = "@P_INTAKE,@P_COLLGE_ID,@P_DEGREENO,@P_BRANCHNO";
               string Call_Values2 = "" + ddladmbatch.SelectedValue + "," + ddlcollege.SelectedValue + "," + Convert.ToInt32(program[0]) + "," + Convert.ToInt32(program[1]) + "";

               DataSet dstransfer = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
               if (dstransfer.Tables[0].Rows.Count > 0)
               {
                   lblIntakeNewCount.Text = dstransfer.Tables[0].Rows[0]["TotalIntakeStudent"].ToString();
               }
            

        //int OldIntakeCount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "count(1)", "ADMBATCH=" + ddladmbatch.SelectedValue + "and isnull(can,0)=0	and isnull(admcan,0)=0"));
        //lblIntakeOldCount.Text = OldIntakeCount.ToString();

        //int NewIntakeCount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_INTAKE_TRANSFER_LOG T ON (S.USERNO= T.USER_NO AND S.ADMBATCH=T.OLD_INATKE)", "count(1)", "OLD_INATKE=" + ddladmbatch.SelectedValue + "and isnull(can,0)=0 and isnull(admcan,0)=0"));
        //lblIntakeNewCount.Text = NewIntakeCount.ToString();

      
        DataSet ds = null;

        string SP_NameReport = "PKG_ACD_GET_STUDENT_SUMMERY_REPORT";
        string SP_ParametersReport = "@P_INTAKE,@P_DEGREENO,@P_BRANCHNO,@P_AFFLIATEDNO,@P_COLLGE_ID";
        string Call_ValuesReport = "" + Convert.ToInt32(ddladmbatch.SelectedValue) + "," + Convert.ToInt32(program[0]) +  "," + Convert.ToInt32(program[1]) + "," + Convert.ToInt32(program[2]) + "," + Convert.ToInt32(ddlcollege.SelectedValue) + "";

        ds = objCommon.DynamicSPCall_Select(SP_NameReport, SP_ParametersReport, Call_ValuesReport);//stud.getsummaryreport(Convert.ToInt32(ddladmbatch.SelectedValue), Convert.ToInt32(program[0]), Convert.ToInt32(program[1]), Convert.ToInt32(ddlcollege.SelectedValue), Convert.ToInt32(program[2]));
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            divreportlv.Visible = true;
            lvreport.DataSource = ds;
            lvreport.DataBind();
            divOldIntake.Visible = true;
            divNewIntake.Visible = true;
        }
        else
        {
            lvreport.DataSource = null;
            lvreport.DataBind();
            divOldIntake.Visible = false;
            divNewIntake.Visible = false;
            objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlcollege.SelectedIndex = 0;
        ddladmbatch.SelectedIndex = 0;
        ddlprogram.SelectedIndex = 0;
        divOldIntake.Visible = false;
        divNewIntake.Visible = false;
        divreportlv.Visible = false;
    }
    protected void ddlcollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        divaplied.Attributes.Remove("class");
        if (ddlcollege.SelectedValue != "0")
        {
            objCommon.FillDropDownList(ddlprogram, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO)INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=CD.BRANCHNO)INNER JOIN ACD_AFFILIATED_UNIVERSITY A ON A.AFFILIATED_NO=CD.AFFILIATED_NO", "DISTINCT CONCAT(D.DEGREENO, ', ',B.BRANCHNO,',',A.AFFILIATED_NO)AS ID", "(D.DEGREENAME+'-'+B.LONGNAME+'-'+AFFILIATED_SHORTNAME)AS DEGBRANCH", "CD.ACTIVE=1 and CD.COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue) + "", "ID");
        }
        else
        {
            ddlprogram.SelectedValue = "0";        
        }
        lvreport.DataSource = null;
        lvreport.DataBind();
        divOldIntake.Visible = false;
        divNewIntake.Visible = false;
    }
    protected void ddlintake_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbFilter.SelectedValue == "1")
        {
            bindaptidata();
            lvtotalaplied.DataSource = null;
            lvtotalaplied.DataBind();

        }
        else if (rdbFilter.SelectedValue == "2")
        {
            bindapplieddata();

            divapti.Visible = false;
            lvapti.DataSource = null;
            lvapti.DataBind();
        }
    }
    //Higher Semester Start

    protected void ddlfacultyname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlfacultyname.SelectedValue != "0")
        {
            objCommon.FillDropDownList(ddlProgramName, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO)INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=CD.BRANCHNO)", "DISTINCT CONCAT(D.DEGREENO, ', ',B.BRANCHNO)AS ID", "(D.DEGREENAME+'-'+B.LONGNAME)AS DEGBRANCH", "CD.ACTIVE=1 and CD.COLLEGE_ID=" + Convert.ToInt32(ddlfacultyname.SelectedValue) + "", "ID");
        }
        else
        {
            ddlProgramName.SelectedValue = "0";
        }
    }

    protected void btnShowHigher_Click(object sender, EventArgs e)
    {
        int Semesterno = Convert.ToInt32(ddlSemsterName.SelectedValue);
        int Faculty = Convert.ToInt32(ddlfacultyname.SelectedValue);

        divaplied.Attributes.Remove("class");
        string[] program;
        if (ddlProgramName.SelectedValue == "0")
        {
            program = "0,0".Split(',');
        }
        else
        {
            program = ddlProgramName.SelectedValue.Split(',');
        }
        DataSet ds = null;
        ds = stud.GetHitherSemesterdata(Semesterno, Convert.ToInt32(program[0]), Convert.ToInt32(program[1]), Faculty);
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            divListHigher.Visible = true;
            lvhigherSemester.DataSource = ds;
            lvhigherSemester.DataBind();
        }
        else
        {
            lvhigherSemester.DataSource = null;
            lvhigherSemester.DataBind();
            objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
        }
        

    }
    protected void btnCancelHigher_Click(object sender, EventArgs e)
    {
        ddlfacultyname.SelectedIndex = 0;
        ddlSemsterName.SelectedIndex = 0;
        ddlProgramName.SelectedIndex = 0;
    }
   
protected void btnTransferExcel_Click(object sender, EventArgs e)
{
       try
        {
            if (ddladmbatch.SelectedIndex > 0)
            {
                string[] program;
                if (ddlprogram.SelectedValue == "0")
                {
                    program = "0,0".Split(',');
                }
                else
                {
                    program = ddlprogram.SelectedValue.Split(',');
                }
                DataSet ds = null;
                ds = stud.GetStudentTransferIntake(Convert.ToInt32(ddladmbatch.SelectedValue), Convert.ToInt32(program[0]), Convert.ToInt32(program[1]), Convert.ToInt32(ddlcollege.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0 && ds != null)
                {
                    GridView GV = new GridView();
                    string ContentType = string.Empty;
                    GV.DataSource = ds;
                    GV.DataBind();
                    string attachment = "attachment; filename=TranasferStudentList.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GV.RenderControl(htw);
                    Response.Write(sw.ToString());
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Record not found", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please Select Intake", this.Page);
                return;
            }
         }
        
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.Export() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        Response.End();
}
}