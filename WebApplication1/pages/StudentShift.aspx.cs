using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;
using System.IO;


public partial class ACADEMIC_StudentShift : System.Web.UI.Page
{
    Common ObjCommon = new Common();
    StudentController objSC = new StudentController();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session

                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    this.Page.Title = Session["coll_name"].ToString();
                    filldropdown();

                }

                ObjCommon.SetLabelData("1");
                ObjCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // dynamic page header

            }

        }
        catch (Exception ex)
        {
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentShift.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentShift.aspx");
        }
    }
    private void filldropdown()
    {
        ObjCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVE = 1", "BATCHNO DESC");
        ObjCommon.FillDropDownList(ddlfaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "ISNULL(ACTIVE,0)=1", "COLLEGE_NAME");
    }


    protected void ddlfaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lstbxProgram.Items.Clear();
      //      lstbxProgram.Items.Insert(0, new ListItem("Please Select", "0"));

            if (ddlfaculty.SelectedIndex > 0)
            {
                string SP_Name = "PKG_GET_STUDENT_SHIFT_PROGRAM";
                string SP_Parameters = "@P_COLLEGE_ID";
                string Call_Values = "" + Convert.ToInt32(ddlfaculty.SelectedValue) + "";
                DataSet ds = ObjCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lstbxProgram.DataValueField = ds.Tables[0].Columns["PROGRAMNO"].ToString();
                    lstbxProgram.DataTextField = ds.Tables[0].Columns["PROGRAMNAME"].ToString();
                    lstbxProgram.DataSource = ds;
                    lstbxProgram.DataBind();
                }
                else
                {
                    lstbxProgram.DataSource = null;
                    lstbxProgram.DataBind();
                }
            }
            lvStudList.DataSource = null;
            lvStudList.DataBind();
            lvStudList.Visible = false;
            btnSave.Visible = false;
            lstbxProgram.Focus();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                ObjCommon.ShowError(Page, "domain.ddlfaculty_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                ObjCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindStudent()
    {
        try
        {
            string degreeno = "",branchno = "", affiliatedno = "",program = ""; 
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
                for (int i = 0; i < pgm.Length; i += 3)
                {
                    degreeno += pgm[i] + ",";
                }
                for (int j = 1; j < pgm.Length; j += 3)
                {
                    branchno += pgm[j] + ",";
                }
                for (int k = 2; k < pgm.Length; k += 3)
                {
                    affiliatedno += pgm[k] + ",";
                }
                degreeno = degreeno.TrimEnd(',');
                branchno = branchno.TrimEnd(',');
                affiliatedno = affiliatedno.TrimEnd(',');
            }
            else
            {
                program = "0";
            }

          
            //string SP_NameStudent = "PKG_GET_STUDENT_SHIFT_STUDENT_DATA";
            //string SP_ParametersStudent = "@P_ADMBATCH,@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO,@P_AFFILIATED_NO,@P_EXCEL";
            //string Call_ValuesStudent = "" + Convert.ToInt32(ddlIntake.SelectedValue) + "," + Convert.ToInt32(ddlfaculty.SelectedValue) + "," + degreeno.ToString() + "," + branchno.ToString() + "," + affiliatedno.ToString() + ",0";
            //DataSet dsStudent = ObjCommon.DynamicSPCall_Select(SP_NameStudent, SP_ParametersStudent, Call_ValuesStudent);

            DataSet dsStudent = objSC.GetStudentShiftData(Convert.ToInt32(ddlIntake.SelectedValue), Convert.ToInt32(ddlfaculty.SelectedValue), degreeno, branchno, affiliatedno, 0);
            if (dsStudent.Tables[0].Rows.Count > 0 && dsStudent.Tables[0] != null)
            {
                lvStudList.DataSource = dsStudent.Tables[0];
                lvStudList.DataBind();
                lvStudList.Visible = true;
                btnSave.Visible = true;         
            }
            else
            {
                lvStudList.DataSource = null;
                lvStudList.DataBind();
                lvStudList.Visible = false;
                btnSave.Visible = false;        
                ObjCommon.DisplayMessage(this.updStudent, "Record not found", Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                ObjCommon.ShowError(Page, "domain.BindStudent-> " + ex.Message + " " + ex.StackTrace);
            else
                ObjCommon.ShowError(Page, "Server UnAvailable");
        }


    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            BindStudent();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                ObjCommon.ShowError(Page, "domain.btnshow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                ObjCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void ddlIntake_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlfaculty.SelectedIndex = 0;
        lstbxProgram.Items.Clear();
        lvStudList.DataSource = null;
        lvStudList.DataBind();
        lvStudList.Visible = false;
        btnSave.Visible = false;
    }
    //protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    lvStudList.Visible = false;
    //}
  
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl.ToString());
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
           int cnt = 0; string idnos = "", deegreeno = "", branchno = "", affilitedno = "";
           int college_id = 0,admbatch=0;

           foreach (ListViewDataItem itm in lvStudList.Items)
            {
                CheckBox chk = itm.FindControl("chkAccept") as CheckBox;
                if (chk.Enabled == true && chk.Checked == true)
                    cnt++;
            }
            if (cnt == 0)
            {
                ObjCommon.DisplayMessage(this, "Please Select Atleast One Student...!!!", this.Page);
            }
            else
            {
                
                foreach (ListViewDataItem lvItem in lvStudList.Items)
                {
                    Label lblStudName = lvItem.FindControl("lblStudName") as Label;
                    Label lblcollegeid = lvItem.FindControl("lblcollegeid") as Label;
                    Label lbldergreeno = lvItem.FindControl("lbldergreeno") as Label;
                    Label lblbranchno = lvItem.FindControl("lblbranchno") as Label;
                    Label lblaffilitedno = lvItem.FindControl("lblaffilitedno") as Label;
      
                    CheckBox chkAccept = lvItem.FindControl("chkAccept") as CheckBox;

                    if (chkAccept.Checked == true && chkAccept.Enabled == true)
                    {
                        idnos += lblStudName.ToolTip.ToString() + ",";                   
                        deegreeno += lbldergreeno.Text.ToString() + ",";
                        branchno += lblbranchno.Text.ToString() + ",";
                        affilitedno += lblaffilitedno.Text.ToString() + ",";
                    }                  
                }
                college_id = Convert.ToInt32(ddlfaculty.SelectedValue);
                admbatch = Convert.ToInt32(ddlIntake.SelectedValue);


                CustomStatus cs = (CustomStatus)objSC.AddStudentShiftData(idnos, admbatch, college_id, deegreeno, branchno, affilitedno, 1, Convert.ToInt32(Session["userno"]));
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    ObjCommon.DisplayMessage(this, "Studentship Expired Successfully !!", this.Page);
                       BindStudent();
                }
                else
                {
                    ObjCommon.DisplayMessage(this, "Error !!", this.Page);
                }         
            }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
          try
           {
            //string degreeno = "", branchno = "", affiliatedno = "", program = "";
            // string[] pgm = new string[] { };
            //foreach (ListItem item in lstbxProgram.Items)
            //{
            //    if (item.Selected == true)
            //    {
            //        program += item.Value + ',';
            //    }    
            //}
            //if (!string.IsNullOrEmpty(program))
            //{
            //    pgm = program.Split(',');
            //    for (int i = 0; i < pgm.Length; i += 3)
            //    {
            //        degreeno += pgm[i] + ",";

            //    }
            //    for (int j = 1; j < pgm.Length; j += 3)
            //    {
            //        branchno += pgm[j] + ",";
            //    }
            //    for (int k = 2; k < pgm.Length; k += 3)
            //    {
            //        affiliatedno += pgm[k] + ",";
            //    }
            //    degreeno = degreeno.TrimEnd(',');
            //    branchno = branchno.TrimEnd(',');
            //    affiliatedno = affiliatedno.TrimEnd(',');
            //}
            //else
            //{
            //    program = "0";
            //}

            //string SP_Excel = "PKG_GET_STUDENT_SHIFT_STUDENT_DATA";
            //string SP_ParametersExcel = "@P_ADMBATCH,@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO,@P_AFFILIATED_NO,@P_EXCEL";
            //string Call_ValuesExcel = "" + Convert.ToInt32(ddlIntake.SelectedValue) + "," + Convert.ToInt32(ddlfaculty.SelectedValue) + "," + degreeno.ToString() + "," + branchno.ToString() + "," + affiliatedno.ToString() + ",1";
            //DataSet dsExcel = ObjCommon.DynamicSPCall_Select(SP_Excel, SP_ParametersExcel, Call_ValuesExcel);

            DataSet dsExcel = objSC.GetStudentShiftData(0,Convert.ToInt32(ddlfaculty.SelectedValue),"0","0","0",1);
            if (dsExcel.Tables[0].Rows.Count <= 0)
            {
                ObjCommon.DisplayMessage(updStudent, "No Records Found.", this.Page);
                return;
            }
            else
            {
                GridView gvStudData = new GridView();
                gvStudData.DataSource = dsExcel;
                gvStudData.DataBind();
                string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
                string attachment = "attachment; filename=StudentshipExpiredList.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Response.Write(FinalHead);
                gvStudData.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }

            }
            catch (Exception ex)
            {
                ObjCommon = new Common();
                ObjCommon.DisplayMessage(updStudent, ex.Message.ToString(), this);
            }
        }
    }
