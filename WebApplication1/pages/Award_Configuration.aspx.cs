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

public partial class Award_Configuration : System.Web.UI.Page
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
                lvAwardBind();
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
                Response.Redirect("~/notauthorized.aspx?page=Award_Configuration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Award_Configuration.aspx");
        }
    }

    protected void DropDownBindConsession()
    {
        
        objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_ID");
        //objCommon.FillDropDownList(ddldegree, "acd_degree", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
      
        objCommon.FillDropDownList(ddlAwardingInstitute, "ACD_AFFILIATED_UNIVERSITY", "AFFILIATED_NO", "AFFILIATED_LONGNAME", "AFFILIATED_NO > 0", "AFFILIATED_NO");
    }

    protected void ddlFaculty_SelectedIndexChanged1(object sender, System.EventArgs e)
    {
        hdnAwardId.Value = "0";
        objCommon.FillListBox(lstbxProgram, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (D.DEGREENO=CDB.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=CDB.BRANCHNO) ", "DISTINCT CONVERT(NVARCHAR(10),D.DEGREENO) + ',' + CONVERT(NVARCHAR(10),CDB.BRANCHNO)", "(D.DEGREENAME +'-'+ B.LONGNAME) AS PROGRAM", "D.DEGREENO> 0 AND CDB.COLLEGE_ID=" + ddlFaculty.SelectedValue, "");
    }


    protected void btnSubmit_Click(object sender, System.EventArgs e)
    {
        try
        {
            string degreeno = "";
            string branchno = "";
            string program = "";
            string[] pgm = new string[] { };
            

            foreach (ListItem item in lstbxProgram.Items)
            {
                if (item.Selected == true)
                {
                    program += item.Value + ',';
                }

            }
            if (!string.IsNullOrEmpty(program))
            {
                pgm = program.Split(',');
                for (int i = 0; i < pgm.Length; i += 2)
                {
                    degreeno += pgm[i] + ",";

                }
                for (int j = 1; j < pgm.Length; j += 2)
                {
                    branchno += pgm[j] + ",";
                }
                degreeno = degreeno.TrimEnd(',');
                branchno = branchno.TrimEnd(',');
            }

            else
            {
                program = "0";
            }
     


            //---------------------------------------------------
        //     string[] program;
        //if (lstbxProgram.SelectedValue == "0")
        //{
        //    program = "0,0".Split(',');
        //}
        //else
        //{
        //    program = lstbxProgram.SelectedValue.Split(',');
        //}
            //-----------------------------------------------------
              CustomStatus cs;

            if (btnSubmit.Text == "Submit")
            {

              //cs = (CustomStatus)objCommon.AddAward_Configuration(Convert.ToInt32(ddlFaculty.SelectedValue), Convert.ToString(txtAwardTitle.Text), Convert.ToString(program[0]), Convert.ToString(program[1]), Convert.ToInt32(ddlAwardingInstitute.SelectedValue), Convert.ToInt32(rdoPoastPonement.SelectedValue), Convert.ToInt32(rdoRegularBatch.SelectedValue), Convert.ToInt32(rdoICStatus.SelectedValue), Convert.ToInt32(rdoRepeatAttp.SelectedValue), Convert.ToInt32(rdoWPGASpe.SelectedValue), Convert.ToDecimal(txtWGPA.Text));


                cs = (CustomStatus)objCommon.AddAward_Configuration(Convert.ToInt32(ddlFaculty.SelectedValue), Convert.ToString(txtAwardTitle.Text), Convert.ToString(degreeno), Convert.ToString(branchno), Convert.ToInt32(ddlAwardingInstitute.SelectedValue), Convert.ToInt32(rdoPoastPonement.SelectedValue), Convert.ToInt32(rdoRegularBatch.SelectedValue), Convert.ToInt32(rdoICStatus.SelectedValue), Convert.ToInt32(rdoRepeatAttp.SelectedValue), Convert.ToInt32(rdoWPGASpe.SelectedValue), Convert.ToDecimal(txtWGPA.Text));

              if (cs.Equals(CustomStatus.RecordSaved))
              {
                  objCommon.DisplayMessage(this.Page, "Record Saved successfully.", this.Page);

              }
            }
            else
            {
                int AWARDS=Convert.ToInt32(ViewState["AwardNo"]);

                cs = (CustomStatus)objCommon.UpdateAward_Configuration(Convert.ToInt32(ddlFaculty.SelectedValue), Convert.ToString(txtAwardTitle.Text), Convert.ToString(program[0]), Convert.ToString(program[1]), Convert.ToInt32(ddlAwardingInstitute.SelectedValue), Convert.ToInt32(rdoPoastPonement.SelectedValue), Convert.ToInt32(rdoRegularBatch.SelectedValue), Convert.ToInt32(rdoICStatus.SelectedValue), Convert.ToInt32(rdoRepeatAttp.SelectedValue), Convert.ToInt32(rdoWPGASpe.SelectedValue), Convert.ToDecimal(txtWGPA.Text),AWARDS);

              if (cs.Equals(CustomStatus.RecordSaved))
              {
                  objCommon.DisplayMessage(this.Page, "Record Update successfully.", this.Page);

              }
            }

        }
        catch
        {
        }

    }


    



    protected void btnCancel_Click(object sender, System.EventArgs e)
    {

    }


    protected void lvAwardBind()
    {
      DataSet ds=null;

      ds = objCommon.FillDropDown("Acd_Award_Configuration AW inner join ACD_COLLEGE_MASTER CM on (AW.COLLEGEID=CM.COLLEGE_ID) inner join acd_degree D on (D.degreeno=AW.DegreeNo)inner join acd_branch B on (B.BRANCHNO= AW.BranchNo) inner join ACD_AFFILIATED_UNIVERSITY AU on (AU.AFFILIATED_NO= AW.Affilidated_No)", "AW.AwardNo,AW.AwardTitle", "CM.COLLEGE_NAME,D.CODE,B.longname,AFFILIATED_LONGNAME", "", "AwardNo DESC");

        if (ds != null && ds.Tables.Count > 0)
        {
            lvAward.DataSource = ds.Tables[0];
            lvAward.DataBind();
            
           
        }
        else
        {
            lvAward.DataSource = null;
            lvAward.DataBind();
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
       
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["action"] = "edit";
            int AwardNo = int.Parse(btnEdit.CommandArgument);
            ViewState["AwardNo"] = AwardNo;
            btnSubmit.Text = "Update";

            ShowDetails(AwardNo);
        }
        catch (Exception ex)
        {
        }
    }

    private void ShowDetails(int AwardNo)
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("Acd_Award_Configuration", "AwardNo,CollegeId,AwardTitle,DegreeNo BranchNo,CONCAT(DegreeNo,',',BranchNo)AS PROGRAM,Affilidated_No,Postponement,GraduatedStudents,ICStatus,RepeatAttempts,Highest_WGPA,Overall_WGPA", "", "AwardNo=" + AwardNo, "AwardNo");

         
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtAwardTitle.Text = ds.Tables[0].Rows[0]["AwardTitle"].ToString();
                ddlFaculty.SelectedValue = ds.Tables[0].Rows[0]["CollegeId"].ToString();
                //lstbxProgram.SelectedValue = ds.Tables[0].Rows[0]["PROGRAM"].ToString();

                ddlAwardingInstitute.SelectedValue = ds.Tables[0].Rows[0]["Affilidated_No"].ToString();
                txtWGPA.Text = ds.Tables[0].Rows[0]["Overall_WGPA"].ToString();
                rdoPoastPonement.SelectedValue = ds.Tables[0].Rows[0]["Postponement"].ToString();
                rdoRegularBatch.SelectedValue = ds.Tables[0].Rows[0]["GraduatedStudents"].ToString();
                rdoICStatus.SelectedValue = ds.Tables[0].Rows[0]["ICStatus"].ToString();
                rdoRepeatAttp.SelectedValue = ds.Tables[0].Rows[0]["RepeatAttempts"].ToString();
                rdoWPGASpe.SelectedValue = ds.Tables[0].Rows[0]["Highest_WGPA"].ToString();
               
                string program = (ds.Tables[0].Rows[0]["PROGRAM"].ToString());
                ddlFaculty_SelectedIndexChanged1(new object(), new EventArgs());

                string[] pgm = program.Split('&');
                for (int j = 0; j < pgm.Length; j++)
                {
                    for (int i = 0; i < lstbxProgram.Items.Count; i++)
                    {
                        if (pgm[j] == lstbxProgram.Items[i].Value)
                        {
                            lstbxProgram.Items[i].Selected = true;
                        }
                    }
                }
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
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        LinkButton lnk = sender as LinkButton;
        int Award_No = int.Parse(lnk.CommandArgument);
        hdnAwardId.Value = Award_No.ToString();
        PopupDetails(Award_No);

        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(System.Web.UI.Page), "Script", "ShowPUP();", true);
 
    }


    private void PopupDetails(int AwardNo)
    {
        try
        {

            //DataSet ds = objCommon.FillDropDown("Acd_Award_Configuration", "AwardNo,CollegeId,AwardTitle,DegreeNo BranchNo,CONCAT(DegreeNo,',',BranchNo)AS PROGRAM,Affilidated_No,Postponement,GraduatedStudents,ICStatus,RepeatAttempts,Highest_WGPA,Overall_WGPA", "", "AwardNo=" + AwardNo, "AwardNo");

            DataSet ds = null;

            ds = objCommon.FillDropDown("Acd_Award_Configuration AW inner join ACD_COLLEGE_MASTER CM on (AW.COLLEGEID=CM.COLLEGE_ID) inner join acd_degree D on (D.degreeno=AW.DegreeNo)inner join acd_branch B on (B.BRANCHNO= AW.BranchNo) inner join ACD_AFFILIATED_UNIVERSITY AU on (AU.AFFILIATED_NO= AW.Affilidated_No)", "AW.AwardNo,AW.AwardTitle", "CM.COLLEGE_NAME,D.CODE,B.longname,CONCAT(D.CODE,',',B.longname)AS PROGRAM,AFFILIATED_LONGNAME,AW.Postponement,AW.GraduatedStudents,AW.ICStatus,AW.RepeatAttempts,AW.Highest_WGPA", "AwardNo=" + AwardNo, "AwardNo");


            if (ds.Tables[0].Rows.Count > 0)
            {
                lblAwardTitle.Text = ds.Tables[0].Rows[0]["AwardTitle"].ToString();
                lblFaculty.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                lblProgram.Text = ds.Tables[0].Rows[0]["PROGRAM"].ToString();
                lblAwardingInstitute.Text = ds.Tables[0].Rows[0]["AFFILIATED_LONGNAME"].ToString();

                //txtWGPA.Text = ds.Tables[0].Rows[0]["Overall_WGPA"].ToString();

                RadioButtonList1.SelectedValue = ds.Tables[0].Rows[0]["Postponement"].ToString();
                RadioButtonList2.SelectedValue = ds.Tables[0].Rows[0]["GraduatedStudents"].ToString();
                RadioButtonList3.SelectedValue = ds.Tables[0].Rows[0]["ICStatus"].ToString();
                RadioButtonList4.SelectedValue = ds.Tables[0].Rows[0]["RepeatAttempts"].ToString();
                RadioButtonList5.SelectedValue = ds.Tables[0].Rows[0]["Highest_WGPA"].ToString();

                //string program = (ds.Tables[0].Rows[0]["PROGRAM"].ToString());
                //ddlFaculty_SelectedIndexChanged1(new object(), new EventArgs());

                //string[] pgm = program.Split('&');
                //for (int j = 0; j < pgm.Length; j++)
                //{
                //    for (int i = 0; i < lstbxProgram.Items.Count; i++)
                //    {
                //        if (pgm[j] == lstbxProgram.Items[i].Value)
                //        {
                //            lstbxProgram.Items[i].Selected = true;
                //        }
                //    }
                //}
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

}


