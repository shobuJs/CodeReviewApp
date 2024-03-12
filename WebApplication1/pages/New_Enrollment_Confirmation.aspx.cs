using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;

using System.Text;


public partial class Enrollment_Configuration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
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
                this.CheckPageAuthorization();
               // FillDropDown();
                ViewState["action"] = "Add";

                if (Request.QueryString["pageno"] != null)
                {

                }
               
                BindAllDropDown();
               // pnlmain.Visible = true;
                //pnlmsg.Visible = false;
                //pnlOTP.Visible = false;
            }
            ViewState["cntbranch"] = string.Empty;
            //divMsg.InnerHtml = string.Empty;
            ViewState["action"] = "add";
            Session["Employment_History"] = null;
            Session["Referees"] = null;
            ViewState["REFEREEES_SRNO"] = null;
            ViewState["EMPLOYEE_SRNO"] = null;

            ddlProgreeLevel.SelectedValue = "5";
            
            objCommon.SetLabelData("0");
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // dynamic page header addded by sarang 09/07/2021
            
        }
        //if (Request.Params["__EVENTTARGET"] != null &&
        //            Request.Params["__EVENTTARGET"].ToString() != string.Empty)
        //{
        //    if (Request.Params["__EVENTTARGET"].ToString() == "RemoveNormalDegree")
        //        this.DeleteApplyProgram(Convert.ToInt32(ViewState["STUDENT_USERNO"]), 1);
        //    else if (Request.Params["__EVENTTARGET"].ToString() == "RemoveNormalArchDegree")
        //        this.DeleteApplyProgram(Convert.ToInt32(ViewState["STUDENT_USERNO"]), 2);
        //}
       // this.BindListView();
        //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // dynamic page header addded by sarang 09/07/2021
        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
       
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Enrollment_Confirmation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Enrollment_Confirmation.aspx");
        }
    }

    protected void BindAllDropDown()
    {
        objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVE = 1", "BATCHNO DESC");
          
//        SELECT DISTINCT UA_SECTION,UA_SECTIONNAME FROM ACD_UA_SECTION S WITH(NOLOCK) 
//INNER JOIN ACD_LEAD_STUDENT_ENQUIRY_GENERATION G WITH(NOLOCK) ON S.UA_SECTION = G.UGPG
//WHERE UA_SECTION > 0 AND USERNO = @P_USERNO ORDER BY UA_SECTION

        objCommon.FillDropDownList(ddlStudyType, "ACD_UA_SECTION S WITH(NOLOCK) INNER JOIN ACD_LEAD_STUDENT_ENQUIRY_GENERATION G ON S.UA_SECTION = G.UGPG", "DISTINCT UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "UA_SECTION");
        //objCommon.FillDropDownList(ddlPermanentCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITY");
    }

    //protected void Listview()
    //{
    //    string Degreenos = ""; string BranchNos = "";
    //    foreach (ListItem item in ddlProgramIntrested.Items)
    //    {
    //        if (item.Selected)
    //        {
    //            string[] branchpref = item.Value.Split(',');
    //            string selectedValue = item.Value;
    //            Degreenos += branchpref[0] + ',';
    //            BranchNos += branchpref[1] + ',';
    //        }
    //    }

    //    ViewState["DegreeNos"] = Degreenos.Substring(0, Degreenos.Length - 1);
    //    ViewState["BranchNos"] = BranchNos.Substring(0, BranchNos.Length - 1);
    //}

    protected void OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        (lvEnrollmentConfirmation.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["DYNAMIC_DATASET"];
        lvEnrollmentConfirmation.DataSource = dt;
        lvEnrollmentConfirmation.DataBind();
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
        //this.BindListView();
    }

    private void BindListView()
    {
        ViewState["DYNAMIC_DATASET"] = null;
        if (Convert.ToInt32(ddlIntake.SelectedValue) > 0)
        {
            DataSet ds = null;

            string Degreenos = ""; string BranchNos = "";
            foreach (ListItem item in ddlProgramIntrested.Items)
            {
                if (item.Selected)
                {
                    string[] branchpref = item.Value.Split(',');
                    string selectedValue = item.Value;
                    Degreenos += branchpref[0] + ',';
                    BranchNos += branchpref[1] + ',';
                }
            }
            if (Degreenos.ToString() != "")
            {

                Degreenos = Degreenos.Substring(0, Degreenos.Length - 1);
            }
            
            // ViewState["DegreeNos"] = Degreenos.Substring(0, Degreenos.Length - 1);
            if (BranchNos.ToString() != "")
            {
                BranchNos = BranchNos.Substring(0, BranchNos.Length - 1);
            }
            

            //DocumentControllerAcad objdocContr = new DocumentControllerAcad();
           //ds = objSC.GetApplicationDetailsInterviw(Convert.ToInt32(ddlIntake.SelectedValue), Convert.ToString(ddlStudyType.SelectedValue), Convert.ToString(Degreenos), Convert.ToString(BranchNos), Convert.ToInt32(ddlProgreeLevel.SelectedValue));
            string sp_name = "PKG_ACD_GET_ADMISSION_DETAILS";
            string sp_para = "@P_INTAKE,@P_STUDYLEVELNOS,@P_PROGRAMNOS,@P_BRANCHNOS,@P_PROGRESS_LEVEL,@P_SEARCHTEXT";
            string sp_call = "" + Convert.ToInt32(ddlIntake.SelectedValue) + "," + Convert.ToString(ddlStudyType.SelectedValue) + "," + Convert.ToString(Degreenos) + "," + Convert.ToString(BranchNos) + "," + Convert.ToInt32(ddlProgreeLevel.SelectedValue) + "," + string.Empty + "";
            ds = objCommon.DynamicSPCall_Select(sp_name, sp_para, sp_call);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvEnrollmentConfirmation.DataSource = ds;
                lvEnrollmentConfirmation.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);

                lblTotalCount.Text = ds.Tables[0].Rows.Count.ToString();
                lblTotalCount.Visible = true;
                divCount.Visible = true;
                ViewState["DYNAMIC_DATASET"] = ds.Tables[0];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Label lblhead1 = lvEnrollmentConfirmation.Items[i].FindControl("lblEligible") as Label;
                    if ((ds.Tables[0].Rows[i]["CAN"].ToString() == "1" || ds.Tables[0].Rows[i]["ADMCAN"].ToString() == "1"))
                    {
                        lblhead1.Text = "PENDING";
                        lblhead1.Style.Add("color", "red");
                    }
                    else
                    {
                        lblhead1.Text = "CONFIRMED";
                        lblhead1.Style.Add("color", "green");
                    }
                    //     DataSet ds1 = null;
                    //     ds1 = objdocContr.getStudentEligiblityDetails(Convert.ToInt32(ds.Tables[0].Rows[i]["USERNO"].ToString()), Convert.ToInt32(ddlIntake.SelectedValue), Convert.ToInt32(ds.Tables[0].Rows[i]["DEGREENO"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i]["BRANCHNO"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i]["COLLEGE_ID"].ToString()));
                    //     if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                    //     {
                    //         Label lblhead1 = lvEnrollmentConfirmation.Items[i].FindControl("lblEligible") as Label;
                    //         if ((ds.Tables[0].Rows[i]["CAN"].ToString() == "1" || ds.Tables[0].Rows[i]["ADMCAN"].ToString() == "1"))
                    //         {
                    //             if (ds1.Tables[0].Rows[0]["ELIGIBILITY"].ToString().ToUpper() == "NOT ELIGIBLE")
                    //             {
                    //                 lblhead1.Text = ds1.Tables[0].Rows[0]["ELIGIBILITY"].ToString();
                    //                 lblhead1.Style.Add("color", "red");
                    //             }
                    //             else
                    //             {
                    //                 lblhead1.Text = ds1.Tables[0].Rows[0]["ELIGIBILITY"].ToString();
                    //                 lblhead1.Style.Add("color", "#007BFF");
                    //             }
                    //         }
                    //         else
                    //         {
                    //             lblhead1.Text = "CONFIRMED";
                    //             lblhead1.Style.Add("color", "green");
                    //         }
                    //     }
                    //     else
                    //     {
                    //         Label lblhead1 = lvEnrollmentConfirmation.Items[i].FindControl("lblEligible") as Label;
                    //         if ((ds.Tables[0].Rows[i]["CAN"].ToString() == "1" || ds.Tables[0].Rows[i]["ADMCAN"].ToString() == "1"))
                    //         {
                    //            lblhead1.Text = "";      
                    //         }
                    //         else
                    //         {
                    //             lblhead1.Text = "CONFIRMED";
                    //             lblhead1.Style.Add("color", "green");
                    //         }
                    //     }
                }
            }
            else
            {
                lvEnrollmentConfirmation.DataSource = null;
                lvEnrollmentConfirmation.DataBind();
                objCommon.DisplayMessage(this.Page, "Record Not Found", this.Page);
                lblTotalCount.Text = "0";
                lblTotalCount.Visible = true;
                divCount.Visible = true;
            }
            ds.Dispose();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Please Select Intake !!!", this.Page);
        }
    }

    

    protected void ddlStudyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStudyType.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlProgramIntrested, "ACD_USER_BRANCH_PREF B INNER JOIN ACD_COLLEGE_MASTER C ON B.COLLEGE_ID = C.COLLEGE_ID INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO INNER JOIN ACD_BRANCH E ON B.BRANCHNO = E.BRANCHNO INNER JOIN ACD_AREA_OF_INTEREST F ON B.AREA_INT_NO = F.AREA_INT_NO",
                //            "DISTINCT CONVERT(NVARCHAR(10),B.COLLEGE_ID) +','+ CONVERT(NVARCHAR(10),B.DEGREENO) + ',' + CONVERT(NVARCHAR(10),B.BRANCHNO) + ',' + CONVERT(NVARCHAR(10),B.AREA_INT_NO)",
                //            "C.CODE + ' - ' + D.DEGREENAME + ' - ' + LONGNAME + ' - ' + F.SHORTNAME", " B.MERITNO IS NOT NULL AND MERITNO != '' AND B.UGPG=" +Convert.ToInt32(ddlStudyType.SelectedValue) +", "");

                //objCommon.FillListBox(ddlProgramIntrested, "ACD_USER_BRANCH_PREF B INNER JOIN ACD_COLLEGE_MASTER C ON B.COLLEGE_ID = C.COLLEGE_ID INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO INNER JOIN ACD_BRANCH E ON B.BRANCHNO = E.BRANCHNO INNER JOIN ACD_AREA_OF_INTEREST F ON B.AREA_INT_NO = F.AREA_INT_NO ", "DISTINCT CONVERT(NVARCHAR(10),B.DEGREENO) + ',' + CONVERT(NVARCHAR(10),B.BRANCHNO)", "D.DEGREENAME + ' - ' + LONGNAME ", "B.MERITNO IS NOT NULL AND MERITNO != '' AND B.UGPG= " + Convert.ToInt32(ddlStudyType.SelectedValue), "");
                lvEnrollmentConfirmation.DataSource = null;
                lvEnrollmentConfirmation.DataBind();
                objCommon.FillListBox(ddlProgramIntrested, "ACD_USER_REGISTRATION UR INNER JOIN ACD_USER_BRANCH_PREF BR ON(UR.USERNO=BR.USERNO)INNER JOIN ACD_DEGREE D ON (BR.DEGREENO=D.DEGREENO)INNER JOIN ACD_BRANCH B ON(BR.BRANCHNO=B.BRANCHNO)", "DISTINCT CONVERT(NVARCHAR(10),BR.DEGREENO) + ',' + CONVERT(NVARCHAR(10),BR.BRANCHNO)", "D.DEGREENAME + ' - ' + LONGNAME ", "BR.UGPG= " + Convert.ToInt32(ddlStudyType.SelectedValue), "");
               
             
            }
            else
            {
                lvEnrollmentConfirmation.DataSource = null;
                lvEnrollmentConfirmation.DataBind();
                ddlProgramIntrested.Items.Clear();
              //  ddlPreference.Items.Clear();
            }
            if (ddlProgramIntrested.Items.Count == 0)
            {
                // objCommon.DisplayMessage(updEnquiry, "Program Interested in not found Please contact online admission department!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void lnkButtonShow_Click(Object sender, EventArgs e)
    {
        try
        {
            if (ddlStudyType.SelectedValue != null)
            {

                BindListView();
            }
        }
      
        catch (Exception ex)
        {
          
        }
    }

    protected void lnkButtonCancel_Click(Object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        
    }


    protected void txtFilter_TextChanged(object sender, EventArgs e)
    {
        TextBox searchTextBox = (TextBox)lvEnrollmentConfirmation.FindControl("txtFilter");
        string search = searchTextBox.Text;

        try
        {
            DataSet ds = null;
            string sp_name = "PKG_ACD_GET_ADMISSION_DETAILS";
            string sp_para = "@P_INTAKE,@P_STUDYLEVELNOS,@P_PROGRAMNOS,@P_BRANCHNOS,@P_PROGRESS_LEVEL,@P_SEARCHTEXT";
            string sp_call = "" + Convert.ToInt32(ddlIntake.SelectedValue) + "," + 0 + "," + string.Empty + "," + string.Empty + "," + 0 + "," + search + "";
            ds = objCommon.DynamicSPCall_Select(sp_name, sp_para, sp_call);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvEnrollmentConfirmation.DataSource = ds;
                lvEnrollmentConfirmation.DataBind();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Label lblhead1 = lvEnrollmentConfirmation.Items[i].FindControl("lblEligible") as Label;
                    if ((ds.Tables[0].Rows[i]["CAN"].ToString() == "1" || ds.Tables[0].Rows[i]["ADMCAN"].ToString() == "1"))
                    {
                        lblhead1.Text = "PENDING";
                        lblhead1.Style.Add("color", "red");
                    }
                    else
                    {
                        lblhead1.Text = "CONFIRMED";
                        lblhead1.Style.Add("color", "green");
                    }
                    
                }
            }
            else
            {
                lvEnrollmentConfirmation.DataSource = null;
                lvEnrollmentConfirmation.DataBind();
                objCommon.DisplayMessage(this.Page, "Record Not Found", this.Page);
                lblTotalCount.Text = "0";
                lblTotalCount.Visible = true;
                divCount.Visible = true;
            }
            ds.Dispose();
        }
        catch (Exception ex)
        { 
        
        }
    }
  
}