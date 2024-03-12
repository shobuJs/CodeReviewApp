using System;
using System.Collections;
using System.Configuration;
using System.Data;
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

public partial class ACADEMIC_ALCourseList : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeesHeadController objFHC = new FeesHeadController();
    FeesHead fee = new FeesHead();

    protected void Page_PreInit(object sender, EventArgs e)
    {

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
                objCommon.SetLabelData("0");
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
            }
            else
            {
                CheckPageAuthorization();
                objCommon.FillDropDownList(ddlAlCourse, "ACD_ALTYPE", "ALTYPENO", "ALTYPENAME", "ALTYPENO > 0 ", "ALTYPENO desc");
                objCommon.FillDropDownList(ddlGrade, "ACD_ALTYPE", "ALTYPENO", "ALTYPENAME", "ALTYPENO > 0 ", "ALTYPENO desc");
            }
        }
        objCommon.SetLabelData("0");
        objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // dynamic page header addded by sarang 09/07/2021
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {

            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ALCourseList.aspx");
            }
            Common objCommon = new Common();

        }
        else
        {

            Response.Redirect("~/notauthorized.aspx?page=ALCourseList.aspx");
        }
    }
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("ID", typeof(string)));
        dr = dt.NewRow();
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["ID"] = 0;
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;

        lvALCourse.DataSource = dt;
        lvALCourse.DataBind();
    }
    protected void ddlAlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAlCourse.SelectedIndex > 0)
            {
                SetInitialRow();
                BindData();
                DivAdd.Visible = true;
                divSubmit.Visible = true;
            }
            else
            {
                Clear();
                divSubmit.Visible = false;
            }
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void btnAddCourse_Click(object sender, EventArgs e)
    {
        try
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0 && dtCurrentTable.Rows.Count < 40)
                {
                    DataTable dtNewTable = new DataTable();
                    dtNewTable.Columns.Add(new DataColumn("Column1", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("Column2", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("Column3", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("ID", typeof(string)));
                    drCurrentRow = dtNewTable.NewRow();
                    drCurrentRow["Column1"] = string.Empty;
                    drCurrentRow["Column2"] = string.Empty;
                    drCurrentRow["Column3"] = string.Empty;
                    drCurrentRow["ID"] = 0;
                    dtNewTable.Rows.Add(drCurrentRow);
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        HiddenField hdfid = (HiddenField)lvALCourse.Items[rowIndex].FindControl("hfdvalue");
                        TextBox box1 = (TextBox)lvALCourse.Items[rowIndex].FindControl("txtCourseName");
                        TextBox box2 = (TextBox)lvALCourse.Items[rowIndex].FindControl("txtShortName");
                        CheckBox box3 = (CheckBox)lvALCourse.Items[rowIndex].FindControl("chlactive");

                        if (box1.Text.Trim() == string.Empty)
                        {
                            objCommon.DisplayMessage(updALCourse, "Please Enter Course Name", this.Page);
                            return;
                        }
                        else if (box2.Text.Trim() == string.Empty)
                        {
                            objCommon.DisplayMessage(updALCourse, "Please Enter Course Short Name", this.Page);
                            return;
                        }
                        else
                        {
                            drCurrentRow = dtNewTable.NewRow();
                            drCurrentRow["Column1"] = box1.Text;
                            drCurrentRow["Column2"] = box2.Text;
                            if (box3.Checked == true)
                                drCurrentRow["Column3"] = 1;
                            else
                                drCurrentRow["Column3"] = 0;
                            drCurrentRow["ID"] = hdfid.Value;
                            rowIndex++;
                            dtNewTable.Rows.Add(drCurrentRow);
                        }
                    }

                    ViewState["CurrentTable"] = dtNewTable;
                    lvALCourse.DataSource = dtNewTable;
                    lvALCourse.DataBind();
                }
                else
                {
                    objCommon.DisplayMessage(updALCourse, "Maximum Options Limit Reached", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(updALCourse, "Error!!!", this.Page);
            }
            SetPreviousData();
        }
        catch (Exception ex)
        { 
            
        }
    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField hdfid = (HiddenField)lvALCourse.Items[rowIndex].FindControl("hfdvalue");
                    TextBox box1 = (TextBox)lvALCourse.Items[rowIndex].FindControl("txtCourseName");
                    TextBox box2 = (TextBox)lvALCourse.Items[rowIndex].FindControl("txtShortName");
                    CheckBox box3 = (CheckBox)lvALCourse.Items[rowIndex].FindControl("chlactive");

                    hdfid.Value = dt.Rows[i]["ID"].ToString();
                    box1.Text = dt.Rows[i]["Column1"].ToString();
                    box2.Text = dt.Rows[i]["Column2"].ToString();
                    if (dt.Rows[i]["Column3"].ToString() == "1")
                        box3.Checked = true;
                    else
                        box3.Checked = false;

                    rowIndex++;
                }
            }
        }
        else
        {
            SetInitialRow();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string CourseName = string.Empty; string ShortName = string.Empty; string savechkK = string.Empty; string hdnvalue = string.Empty;
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        HiddenField hdfid = (HiddenField)lvALCourse.Items[rowIndex].FindControl("hfdvalue");
                        Label Srno = (Label)lvALCourse.Items[rowIndex].FindControl("lblSrNo");
                        TextBox txCourseName = (TextBox)lvALCourse.Items[rowIndex].FindControl("txtCourseName");
                        TextBox txShortName = (TextBox)lvALCourse.Items[rowIndex].FindControl("txtShortName");
                        CheckBox savechk = (CheckBox)lvALCourse.Items[rowIndex].FindControl("chlactive");

                        if (txCourseName.Text.Trim() != string.Empty && txShortName.Text.Trim() != string.Empty)
                        {
                            CourseName += txCourseName.Text.Trim() + ",";
                            ShortName += txShortName.Text.Trim() + ",";
                            hdnvalue += hdfid.Value.Trim() + ",";

                            rowIndex++;

                            if (savechk.Checked)
                            {
                                savechkK += "1" + ",";
                            }
                            else
                            {
                                savechkK += "0" + ",";
                            }

                        }

                        else
                        {
                            objCommon.DisplayUserMessage(updALCourse, "Please filled All Course Details!", this.Page);
                            return;
                        }
                    }
                }
            }
            CourseName = CourseName.TrimEnd(',');
            ShortName = ShortName.TrimEnd(',');
            savechkK = savechkK.TrimEnd(',');
            hdnvalue += hdnvalue.TrimEnd(',');
            CustomStatus cs = (CustomStatus)objFHC.InsertCourse(Convert.ToInt32(ddlAlCourse.SelectedValue), CourseName, ShortName, savechkK, hdnvalue, 0, 1, Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["userno"]));

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updALCourse, "Record Saved Successfully!", this.Page);
                return;

            }

            else if (cs.Equals(CustomStatus.TransactionFailed))
            {
                objCommon.DisplayUserMessage(updALCourse, "Failed to Save Records", this.Page);
                return;
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void Clear()
    {
        Response.Redirect(Request.Url.ToString());
    }
    private void BindData()
    {
        try
        {

            DataSet ds = objFHC.GetCourse(Convert.ToInt32(ddlAlCourse.SelectedValue));
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlAlCourse.SelectedValue = ds.Tables[0].Rows[0]["ALTYPENO"] == null ? "0" : ds.Tables[0].Rows[0]["ALTYPENO"].ToString();
                    DataTable dtCurrentTable = new DataTable();

                    DataRow drCurrentRow = null;
                    dtCurrentTable.Columns.Add(new DataColumn("Column1", typeof(string)));
                    dtCurrentTable.Columns.Add(new DataColumn("Column2", typeof(string)));
                    dtCurrentTable.Columns.Add(new DataColumn("Column3", typeof(string)));
                    dtCurrentTable.Columns.Add(new DataColumn("ID", typeof(string)));
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Column1"] = ds.Tables[0].Rows[i]["AL_COURSES"];
                        drCurrentRow["Column2"] = ds.Tables[0].Rows[i]["AL_SHORTNAME"];
                        drCurrentRow["Column3"] = ds.Tables[0].Rows[i]["ACTIVE"];
                        drCurrentRow["ID"] = ds.Tables[0].Rows[i]["ID"];

                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }

                    ViewState["CurrentTable"] = dtCurrentTable;
                    lvALCourse.DataSource = dtCurrentTable;
                    lvALCourse.DataBind();
                    divSubmit.Visible = true;
                }

            }
            if (ds != null) ds.Dispose();
            showdetails();

        }
        catch (Exception ex)
        {
          
        }
    }
    private void showdetails()
    {
        try
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HiddenField hdfid = (HiddenField)lvALCourse.Items[rowIndex].FindControl("hfdvalue");
                        Label Srno = (Label)lvALCourse.Items[rowIndex].FindControl("lblSrNo");
                        TextBox box1 = (TextBox)lvALCourse.Items[rowIndex].FindControl("txtCourseName");
                        TextBox box2 = (TextBox)lvALCourse.Items[rowIndex].FindControl("txtShortName");
                        CheckBox box3 = (CheckBox)lvALCourse.Items[rowIndex].FindControl("chlactive");
                        hdfid.Value = dt.Rows[i]["ID"].ToString();
                        box1.Text = dt.Rows[i]["Column1"].ToString();
                        box2.Text = dt.Rows[i]["Column2"].ToString();
                        if (dt.Rows[i]["Column3"].ToString() == "1")
                            box3.Checked = true;
                        else
                            box3.Checked = false;

                        rowIndex++;
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnRemove = sender as Button;
            int Id = Convert.ToInt32(btnRemove.CommandArgument);
            int RowNumber = Convert.ToInt32(btnRemove.CommandName);
            DataTable dt = ViewState["CurrentTable"] as DataTable;
            if (Id == 0)
            {
                dt.Rows[RowNumber-1].Delete();
                dt.AcceptChanges();
                ViewState["CurrentTable"] = dt;
                lvALCourse.DataSource = dt;
                lvALCourse.DataBind();
                SetPreviousData();
            }
            else
            {
                CustomStatus cs = (CustomStatus)objFHC.InsertCourse(Convert.ToInt32(ddlAlCourse.SelectedValue), "0", "0", "0", "0", Id, 2, Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["userno"]));
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayUserMessage(updALCourse, "Record Deteted Successfully", this.Page);
                    dt.Rows[RowNumber-1].Delete();
                    dt.AcceptChanges();
                    ViewState["CurrentTable"] = dt;
                    lvALCourse.DataSource = dt;
                    lvALCourse.DataBind();
                    SetPreviousData();
                }
            }
        }
        catch (Exception ex)
        { 
        
        }
    }
    private void SetInitialRowGrades()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("ID", typeof(string)));
        dr = dt.NewRow();
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["ID"] = 0;
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;

        lvGrades.DataSource = dt;
        lvGrades.DataBind();
    }
    private void showdetailsGrades()
    {
        try
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HiddenField hdfid = (HiddenField)lvGrades.Items[rowIndex].FindControl("hfdValue");
                        Label Srno = (Label)lvGrades.Items[rowIndex].FindControl("lblSrNo");
                        TextBox box1 = (TextBox)lvGrades.Items[rowIndex].FindControl("txtGrades");
                        CheckBox box2 = (CheckBox)lvGrades.Items[rowIndex].FindControl("chkActiveGrade");
                        hdfid.Value = dt.Rows[i]["ID"].ToString();
                        box1.Text = dt.Rows[i]["Column1"].ToString();
                        if (dt.Rows[i]["Column2"].ToString() == "1")
                            box2.Checked = true;
                        else
                            box2.Checked = false;

                        rowIndex++;
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void BindDataGrades()
    {
        try
        {

            DataSet ds = objFHC.GetGrades(Convert.ToInt32(ddlGrade.SelectedValue));
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlGrade.SelectedValue = ds.Tables[0].Rows[0]["ALTYPENO"] == null ? "0" : ds.Tables[0].Rows[0]["ALTYPENO"].ToString();
                    DataTable dtCurrentTable = new DataTable();

                    DataRow drCurrentRow = null;
                    dtCurrentTable.Columns.Add(new DataColumn("Column1", typeof(string)));
                    dtCurrentTable.Columns.Add(new DataColumn("Column2", typeof(string)));
                    dtCurrentTable.Columns.Add(new DataColumn("ID", typeof(string)));
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Column1"] = ds.Tables[0].Rows[i]["GRADES"];
                        drCurrentRow["Column2"] = ds.Tables[0].Rows[i]["ACTIVE"];
                        drCurrentRow["ID"] = ds.Tables[0].Rows[i]["ID"];
                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }

                    ViewState["CurrentTable"] = dtCurrentTable;
                    lvGrades.DataSource = dtCurrentTable;
                    lvGrades.DataBind();
                    divgradesbuttons.Visible = true;
                }

            }
            if (ds != null) ds.Dispose();
            showdetailsGrades();

        }
        catch (Exception ex)
        {

        }
    }
    private void SetPreviousDataGrades()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField hdfid = (HiddenField)lvGrades.Items[rowIndex].FindControl("hfdValue");
                    TextBox box1 = (TextBox)lvGrades.Items[rowIndex].FindControl("txtGrades");
                    CheckBox box2 = (CheckBox)lvGrades.Items[rowIndex].FindControl("chkActiveGrade");

                    hdfid.Value = dt.Rows[i]["ID"].ToString();
                    box1.Text = dt.Rows[i]["Column1"].ToString();
                     if (dt.Rows[i]["Column2"].ToString() == "1")
                        box2.Checked = true;
                    else
                        box2.Checked = false;

                    rowIndex++;
                }
            }
        }
        else
        {
            showdetailsGrades();
        }
    }
    protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        try
        {
            if (ddlGrade.SelectedIndex > 0)
            {
                SetInitialRowGrades();
                BindDataGrades();
                DivAddgrades.Visible = true;
                divgradesbuttons.Visible = true;
            }
            else
            {
                Clear();
                divgradesbuttons.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnaddGrades_Click(object sender, EventArgs e)
    {
      
        try
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0 && dtCurrentTable.Rows.Count < 40)
                {
                    DataTable dtNewTable = new DataTable();
                    dtNewTable.Columns.Add(new DataColumn("Column1", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("Column2", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("ID", typeof(string)));
                    drCurrentRow = dtNewTable.NewRow();
                    drCurrentRow["Column1"] = string.Empty;
                    drCurrentRow["Column2"] = string.Empty;
                     drCurrentRow["ID"] = 0;
                    dtNewTable.Rows.Add(drCurrentRow);
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        HiddenField hdfid = (HiddenField)lvGrades.Items[rowIndex].FindControl("hfdValue");
                        TextBox box1 = (TextBox)lvGrades.Items[rowIndex].FindControl("txtGrades");
                        CheckBox box2 = (CheckBox)lvGrades.Items[rowIndex].FindControl("chkActiveGrade");

                        if (box1.Text.Trim() == string.Empty)
                        {
                            objCommon.DisplayMessage(updGrades, "Please Enter Grades", this.Page);
                            return;
                        }
                        
                        else
                        {
                            drCurrentRow = dtNewTable.NewRow();
                            drCurrentRow["Column1"] = box1.Text;
                           if (box2.Checked == true)
                                drCurrentRow["Column2"] = 1;
                            else
                                drCurrentRow["Column2"] = 0;
                            drCurrentRow["ID"] = hdfid.Value;
                            rowIndex++;
                            dtNewTable.Rows.Add(drCurrentRow);
                        }
                    }

                    ViewState["CurrentTable"] = dtNewTable;
                    lvGrades.DataSource = dtNewTable;
                    lvGrades.DataBind();
                }
                else
                {
                    objCommon.DisplayMessage(updGrades, "Maximum Options Limit Reached", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(updGrades, "Error!!!", this.Page);
            }
            SetPreviousDataGrades();
        }
        catch (Exception ex)
        { 
            
        }
    }
   
    protected void btnRemoveGrades_Click(object sender, EventArgs e)
    {
        try
        {
   
            Button btnRemove = sender as Button;
            int Id = Convert.ToInt32(btnRemove.CommandArgument);
            int RowNumber = Convert.ToInt32(btnRemove.CommandName);
            DataTable dt = ViewState["CurrentTable"] as DataTable;
            if (Id == 0)
            {
                dt.Rows[RowNumber - 1].Delete();
                dt.AcceptChanges();
                ViewState["CurrentTable"] = dt;
                lvGrades.DataSource = dt;
                lvGrades.DataBind();
                SetPreviousDataGrades();
            }
            else
            {
                CustomStatus cs = (CustomStatus)objFHC.InsertGrades(Convert.ToInt32(ddlGrade.SelectedValue), "0", "0", "0", Id, 2, Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["userno"]));
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayUserMessage(updGrades, "Record Deteted Successfully", this.Page);
                    dt.Rows[RowNumber - 1].Delete();
                    dt.AcceptChanges();
                    ViewState["CurrentTable"] = dt;
                    lvGrades.DataSource = dt;
                    lvGrades.DataBind();
                    SetPreviousDataGrades();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string GradesName = string.Empty;  string Gradechk = string.Empty; string hiddenvalue = string.Empty;
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        HiddenField hdfid = (HiddenField)lvGrades.Items[rowIndex].FindControl("hfdValue");
                        Label Srno = (Label)lvGrades.Items[rowIndex].FindControl("lblSrNo");
                        TextBox txGradeName = (TextBox)lvGrades.Items[rowIndex].FindControl("txtGrades");
                        CheckBox GradeActive = (CheckBox)lvGrades.Items[rowIndex].FindControl("chkActiveGrade");
                        HiddenField hdfifuano = (HiddenField)lvGrades.Items[rowIndex].FindControl("hfvforuano");
                        if (txGradeName.Text.Trim() != string.Empty)
                        {
                            GradesName += txGradeName.Text.Trim() + ",";
                            hiddenvalue += hdfid.Value.Trim() + ",";

                            rowIndex++;

                            if (GradeActive.Checked)
                            {
                                Gradechk += "1" + ",";
                            }
                            else
                            {
                                Gradechk += "0" + ",";
                            }

                        }

                        else
                        {
                            objCommon.DisplayUserMessage(updGrades, "Please filled All Grades Details!", this.Page);
                            return;
                        }
                    }
                }
            }
            GradesName = GradesName.TrimEnd(',');
            Gradechk = Gradechk.TrimEnd(',');
            hiddenvalue += hiddenvalue.TrimEnd(',');
            CustomStatus cs = (CustomStatus)objFHC.InsertGrades(Convert.ToInt32(ddlGrade.SelectedValue), GradesName, Gradechk, hiddenvalue, 0, 1, Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["userno"]));

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updGrades, "Record Saved Successfully!", this.Page);
                return;

            }

            else if (cs.Equals(CustomStatus.TransactionFailed))
            {
                objCommon.DisplayUserMessage(updGrades, "Failed to Save Records", this.Page);
                return;
            }

        }
        catch (Exception ex)
        {

        }
    }
    private void  ClearGrades()

    {
        lvGrades.DataSource = null;
        lvGrades.DataBind();
        DivAddgrades.Visible = false;
        divgradesbuttons.Visible = false;
        ddlGrade.SelectedIndex = 0;

    }
    protected void lnkClear_Click(object sender, EventArgs e)
    {
        ClearGrades();
    }
    protected void lkAnnouncement_Click(object sender, EventArgs e)
    {
        ddlGrade.SelectedIndex = 0;
       
        divEmoji.Visible = false;
        divAnnounce.Visible = true;
        divlkFeed.Attributes.Remove("class");
        divlkAnnouncement.Attributes.Add("class", "active");
    }
    protected void lkFeedback_Click(object sender, EventArgs e)
    {
        ddlAlCourse.SelectedIndex = 0;
       
        divAnnounce.Visible = false;
        divEmoji.Visible = true;
        divlkAnnouncement.Attributes.Remove("class");
        divlkFeed.Attributes.Add("class", "active");
        
    }
}