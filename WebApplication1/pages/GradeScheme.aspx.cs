using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;

public partial class ACADEMIC_GradeScheme : System.Web.UI.Page
{
    Common objCommon = new Common();
    Grade objG = new Grade();
    GradeController objGC = new GradeController();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
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

                    ViewState["usertype"] = Session["usertype"];
             
                    BindListview();
                    BindAllotment();
                    FillDropDown();
                    ViewState["action"] = "add";

                }
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count <= 0)
                    {
                        SetInitialRow();
                    }
                }
                else
                {
                    SetInitialRow();
                }
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PDPGrading.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=GradeScheme.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=GradeScheme.aspx");
        }
    }

    #region "Grade Scheme"
    private void SetInitialRow()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dt.Columns.Add(new DataColumn("Column4", typeof(bool)));
            dt.Columns.Add(new DataColumn("Column5", typeof(string)));
            dt.Columns.Add(new DataColumn("ID", typeof(string)));
            dt.Columns.Add(new DataColumn("Column6", typeof(string)));
            dr = dt.NewRow();
            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;
            dr["Column3"] = string.Empty;
            dr["Column4"] = false;
            dr["Column5"] = string.Empty;
            dr["Column6"] = string.Empty;
            //dr["Column3"] = string.Empty;
            dr["ID"] = 0;
            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;

            lvGradeScheme.DataSource = dt;
            lvGradeScheme.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PDPGrading.SetInitialRow() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void SetPreviousData()
    {
        try
        {
            int rowIndex = 0;
            bool chkGradeFail = false;

            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            dtCurrentTable.Rows.Clear();
            dtCurrentTable.AcceptChanges();
            foreach (var item in lvGradeScheme.Items)
            {

                HiddenField hidden1 = (HiddenField)lvGradeScheme.Items[rowIndex].FindControl("hfdvalue");
                TextBox box1 = (TextBox)lvGradeScheme.Items[rowIndex].FindControl("txtGradeName");
                TextBox box2 = (TextBox)lvGradeScheme.Items[rowIndex].FindControl("txtMinMarks");
                TextBox box3 = (TextBox)lvGradeScheme.Items[rowIndex].FindControl("txtMaxMarks");
                CheckBox box4 = (CheckBox)lvGradeScheme.Items[rowIndex].FindControl("chkFailGrade");
                Label Gradeno = (Label)lvGradeScheme.Items[rowIndex].FindControl("lblGradeno");
                TextBox box5 = (TextBox)lvGradeScheme.Items[rowIndex].FindControl("txtGradePoint");
                if (box4.Checked == true) { chkGradeFail = true; }
                else { chkGradeFail = false; }

                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["Column1"] = box1.Text.Trim();
                drCurrentRow["Column2"] = box2.Text.Trim();
                drCurrentRow["Column3"] = box3.Text.Trim();
                drCurrentRow["Column4"] = chkGradeFail;
                drCurrentRow["Column5"] = Gradeno.Text.Trim();
                drCurrentRow["Column6"] = box5.Text.Trim();
                drCurrentRow["ID"] = hidden1.Value;
                dtCurrentTable.Rows.Add(drCurrentRow);
                rowIndex += 1;

                //Do stuff
            }
            ViewState["CurrentTable"] = dtCurrentTable;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PDPGrading.SetPreviousData() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void BindListview()
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("ACD_GRADE", "DISTINCT GRADING_SCHEME_NO", "GRADING_SCHEME_NAME,STATUS", "", "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvData.DataSource = ds;
                lvData.DataBind();
            }
            else
            {
                lvData.DataSource = null;
                lvData.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "GradeScheme.BindListview() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private string validateGradeSchemeData()
    {
        string _validate = string.Empty;
        try
        {
            int rowIndex = 0;
            foreach (var item in lvGradeScheme.Items)
            {

                TextBox box1 = (TextBox)lvGradeScheme.Items[rowIndex].FindControl("txtGradeName");
                TextBox box2 = (TextBox)lvGradeScheme.Items[rowIndex].FindControl("txtMinMarks");
                TextBox box3 = (TextBox)lvGradeScheme.Items[rowIndex].FindControl("txtMaxMarks");
                TextBox box4 = (TextBox)lvGradeScheme.Items[rowIndex].FindControl("txtGradePoint");

                if (box1.Text.Trim() == string.Empty || box1.Text.Trim() == "0")
                {
                    _validate = "Please Enter Grade Name";
                    return _validate;
                }
                else if (box2.Text.Trim() == string.Empty || box2.Text.Trim() == "0")
                {
                    _validate = "Please Enter Min Marks";
                    return _validate;
                }
                else if (box3.Text.Trim() == string.Empty || box3.Text.Trim() == "0")
                {
                    _validate = "Please Enter Max Marks";
                    return _validate;

                }
                else if (box4.Text.Trim() == string.Empty || box4.Text.Trim() == "0")
                {
                    _validate = "Please Enter Grade Point";
                    return _validate;
                }
                rowIndex += 1;

                //Do stuff
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                _validate = ex.Message;
                objCommon.ShowError(Page, "PDPGrading.validateGradeSchemeData() --> " + ex.Message + " " + ex.StackTrace);
            }
            else
            {
                objCommon.ShowError(Page, "Server Unavailable.");
            }
        }
        return _validate;
    }
    protected void btnAddGradeSlab_Click(object sender, EventArgs e)
    {
        try
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                string _validateGradeSchemeData = validateGradeSchemeData();
                if (_validateGradeSchemeData != string.Empty)
                {
                    objCommon.DisplayMessage(updGradeScheme, _validateGradeSchemeData, this.Page);
                    return;
                }
                SetPreviousData();
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    rowIndex = dtCurrentTable.Rows.Count + 1;
                    drCurrentRow = dtCurrentTable.NewRow();

                    drCurrentRow["Column1"] = "0";
                    drCurrentRow["Column2"] = "0";
                    drCurrentRow["Column3"] = "0";
                    drCurrentRow["Column4"] = false;
                    drCurrentRow["Column5"] = "";
                    drCurrentRow["Column6"] = "0";
                    drCurrentRow["ID"] = rowIndex;
                    dtCurrentTable.Rows.Add(drCurrentRow);

                    ViewState["CurrentTable"] = dtCurrentTable;
                    lvGradeScheme.DataSource = dtCurrentTable;
                    lvGradeScheme.DataBind();
                }
                else
                {
                    objCommon.DisplayMessage(updGradeScheme, "Maximum Options Limit Reached", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(updGradeScheme, "Error!!!", this.Page);
            }
            //SetPreviousData();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "GradeScheme.btnAddGradeSlab_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private DataTable CreateDatatable_GradeScheme()
    {
        DataTable dt = new DataTable();
        try
        {
            dt.TableName = "GradeSchemeDatatable";
            dt.Columns.Add("GradeNo");
            dt.Columns.Add("Grade");
            dt.Columns.Add("SchemeId");
            dt.Columns.Add("SchemeName");
            dt.Columns.Add("MinMarks");
            dt.Columns.Add("MaxMarks");
            dt.Columns.Add("CreatedBy");
            dt.Columns.Add("GradeType");
            dt.Columns.Add("DescGrade");
            dt.Columns.Add("GradePoint");
            dt.Columns.Add("CollegeCode");
            dt.Columns.Add("Status");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "GradeScheme.CreateDatatable_GradeScheme() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dt;
    }
    private DataTable Add_Datatable_GradeScheme()
    {
        string chkGradeFail = string.Empty;
        int chkGradeType;
        int GradeMaxno = 0;
        //int gradeNo = Convert.ToInt32(objCommon.LookUp("ACD_GRADE", "MAX(GRADENO)+1", ""));  ISNULL(MAX(GRADENO),0)+1
        int gradeNo = Convert.ToInt32(objCommon.LookUp("ACD_GRADE", "ISNULL(MAX(GRADENO),0)+1", ""));
        int schemeId = Convert.ToInt32(objCommon.LookUp("ACD_GRADE", "ISNULL(MAX(GRADING_SCHEME_NO),0)+1", ""));

        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {
            int SchemeId = Convert.ToInt32(ViewState["SchemId"].ToString());
            // int GradeNo = Convert.ToInt32(ViewState["GradeNo"].ToString());
            int GradeNo = Convert.ToInt32(objCommon.LookUp("ACD_GRADE", "top(1)GRADENO", "GRADING_SCHEME_NO=" + SchemeId));

            DataTable dt = CreateDatatable_GradeScheme();
            try
            {
                int rowIndex = 0;
                foreach (var item in lvGradeScheme.Items)
                {
                    DataRow dRow = dt.NewRow();
                    TextBox box1 = (TextBox)lvGradeScheme.Items[rowIndex].FindControl("txtGradeName");
                    TextBox box2 = (TextBox)lvGradeScheme.Items[rowIndex].FindControl("txtMinMarks");
                    TextBox box3 = (TextBox)lvGradeScheme.Items[rowIndex].FindControl("txtMaxMarks");
                    CheckBox box4 = (CheckBox)lvGradeScheme.Items[rowIndex].FindControl("chkFailGrade");
                    TextBox box5 = (TextBox)lvGradeScheme.Items[rowIndex].FindControl("txtGradePoint");
                    Label Gradeno = (Label)lvGradeScheme.Items[rowIndex].FindControl("lblGradeno");

                    if (dt.Rows.Count > 0)
                    {
                        if (Gradeno.Text != "")
                        {
                            dRow["GradeNo"] = Gradeno.Text;
                            GradeMaxno = Convert.ToInt32(objCommon.LookUp("ACD_GRADE", "max(GRADENO)", ""));

                        }
                        else
                        {
                            if (Gradeno.Text == "" && GradeMaxno == 0)
                            {

                                GradeMaxno = Convert.ToInt32(ViewState["GARDENO"].ToString()) + 1;
                                dRow["GradeNo"] = GradeMaxno.ToString();
                            }
                            else
                            {
                                GradeMaxno = GradeMaxno + 1;
                                ViewState["GARDENOs"] = GradeMaxno + 1;
                                dRow["GradeNo"] = GradeMaxno.ToString();
                            }

                        }
                        dRow["SchemeId"] = SchemeId.ToString();
                        dRow["SchemeName"] = txtGradeSchemeName.Text.Trim();
                        if (hfdStat.Value == "true")
                        { dRow["Status"] = 1; }
                        else { dRow["Status"] = 0; }
                        dRow["CreatedBy"] = Convert.ToInt32(Session["userno"].ToString());
                        dRow["Grade"] = box1.Text.Trim();
                        dRow["MinMarks"] = Convert.ToDecimal(box2.Text.Trim());
                        dRow["MaxMarks"] = Convert.ToDecimal(box3.Text.Trim());
                        dRow["GradePoint"] = Convert.ToDecimal(box5.Text.Trim());
                        dRow["CollegeCode"] = Convert.ToInt32(Session["colcode"].ToString());

                        if (box4.Checked == true)
                        { chkGradeFail = "FAIL"; }
                        else { chkGradeFail = "PASS"; }
                        dRow["DescGrade"] = chkGradeFail;

                        if (box4.Checked == true)
                        { chkGradeType = 0; }
                        else { chkGradeType = 1; }
                        dRow["GradeType"] = chkGradeType;

                        rowIndex += 1;
                        dt.Rows.Add(dRow);
                    }
                    else
                    {
                        ViewState["GARDENO"] = Gradeno.Text;
                        dRow["GradeNo"] = Gradeno.Text;
                        dRow["SchemeId"] = SchemeId.ToString();
                        dRow["SchemeName"] = txtGradeSchemeName.Text.Trim();
                        if (hfdStat.Value == "true")
                        { dRow["Status"] = 1; }
                        else { dRow["Status"] = 0; }
                        dRow["CreatedBy"] = Convert.ToInt32(Session["userno"].ToString());
                        dRow["Grade"] = box1.Text.Trim();
                        dRow["MinMarks"] = Convert.ToDecimal(box2.Text.Trim());
                        dRow["MaxMarks"] = Convert.ToDecimal(box3.Text.Trim());
                        dRow["GradePoint"] = Convert.ToDecimal(box5.Text.Trim());
                        dRow["CollegeCode"] = Convert.ToInt32(Session["colcode"].ToString());
                        if (box4.Checked == true)
                        { chkGradeFail = "FAIL"; }
                        else { chkGradeFail = "PASS"; }
                        dRow["DescGrade"] = chkGradeFail;

                        if (box4.Checked == true)
                        { chkGradeType = 0; }
                        else { chkGradeType = 1; }
                        dRow["GradeType"] = chkGradeType;

                        rowIndex += 1;
                        dt.Rows.Add(dRow);
                    }
                    //Do stuff
                }
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "GradeScheme.Add_Datatable_GradeScheme() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server Unavailable.");
            }
            return dt;
        }
        else
        {
            DataTable dt = CreateDatatable_GradeScheme();
            try
            {
                int rowIndex = 0;
                foreach (var item in lvGradeScheme.Items)
                {
                    DataRow dRow = dt.NewRow();
                    TextBox box1 = (TextBox)lvGradeScheme.Items[rowIndex].FindControl("txtGradeName");
                    TextBox box2 = (TextBox)lvGradeScheme.Items[rowIndex].FindControl("txtMinMarks");
                    TextBox box3 = (TextBox)lvGradeScheme.Items[rowIndex].FindControl("txtMaxMarks");
                    CheckBox box4 = (CheckBox)lvGradeScheme.Items[rowIndex].FindControl("chkFailGrade");
                    TextBox box5 = (TextBox)lvGradeScheme.Items[rowIndex].FindControl("txtGradePoint");

                    if (dt.Rows.Count > 0)
                    {
                        int Gradeno = Convert.ToInt32(dt.Compute("MAX(GradeNo)", string.Empty));
                        Gradeno = Gradeno + 1;
                        dRow["GradeNo"] = Gradeno.ToString();
                        dRow["SchemeId"] = schemeId.ToString();
                        dRow["SchemeName"] = txtGradeSchemeName.Text.Trim();
                        if (hfdStat.Value == "true")
                        { dRow["Status"] = 1; }
                        else { dRow["Status"] = 0; }
                        dRow["CreatedBy"] = Convert.ToInt32(Session["userno"].ToString());
                        dRow["Grade"] = box1.Text.Trim();
                        dRow["MinMarks"] = Convert.ToDecimal(box2.Text.Trim());
                        dRow["MaxMarks"] = Convert.ToDecimal(box3.Text.Trim());
                        dRow["GradePoint"] = Convert.ToDecimal(box5.Text.Trim());
                        dRow["CollegeCode"] = Convert.ToInt32(Session["colcode"].ToString());
                        if (box4.Checked == true)
                        { chkGradeFail = "FAIL"; }
                        else { chkGradeFail = "PASS"; }
                        dRow["DescGrade"] = chkGradeFail;

                        if (box4.Checked == true)
                        { chkGradeType = 0; }
                        else { chkGradeType = 1; }
                        dRow["GradeType"] = chkGradeType;

                        rowIndex += 1;
                        dt.Rows.Add(dRow);
                    }
                    else
                    {
                        dRow["GradeNo"] = gradeNo.ToString();
                        dRow["SchemeId"] = schemeId.ToString();
                        dRow["SchemeName"] = txtGradeSchemeName.Text.Trim();
                        if (hfdStat.Value == "true")
                        { dRow["Status"] = 1; }
                        else { dRow["Status"] = 0; }
                        dRow["CreatedBy"] = Convert.ToInt32(Session["userno"].ToString());
                        dRow["Grade"] = box1.Text.Trim();
                        dRow["MinMarks"] = Convert.ToDecimal(box2.Text.Trim());
                        dRow["MaxMarks"] = Convert.ToDecimal(box3.Text.Trim());
                        dRow["GradePoint"] = Convert.ToDecimal(box5.Text.Trim());
                        dRow["CollegeCode"] = Convert.ToInt32(Session["colcode"].ToString());

                        if (box4.Checked == true)
                        { chkGradeFail = "FAIL"; }
                        else { chkGradeFail = "PASS"; }
                        dRow["DescGrade"] = chkGradeFail;

                        if (box4.Checked == true)
                        { chkGradeType = 0; }
                        else { chkGradeType = 1; }
                        dRow["GradeType"] = chkGradeType;

                        rowIndex += 1;
                        dt.Rows.Add(dRow);
                    }
                    //Do stuff
                }
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "GradeScheme.Add_Datatable_GradeScheme() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server Unavailable.");
            }
            return dt;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = Add_Datatable_GradeScheme();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            if (dt.Rows.Count > 0)
            {
                Session["Data"] = dt;
                if (ViewState["action"].ToString() == "add")
                {

                    CustomStatus cs = (CustomStatus)objGC.InsertGradeScheme(ds.GetXml());
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(updGradeScheme, "Record Saved Successfully!", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.TransactionFailed))
                    {
                        objCommon.DisplayUserMessage(updGradeScheme, "Failed to Save Records", this.Page);
                        return;
                    }
                }
                else
                {
                    // CustomStatus cs = (CustomStatus)objGC.UpdateGradeScheme(ds.GetXml());
                    CustomStatus cs = (CustomStatus)objGC.UpdateGradeScheme(dt);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(updGradeScheme, "Record Updated Successfully!", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.TransactionFailed))
                    {
                        objCommon.DisplayUserMessage(updGradeScheme, "Failed to Update Records", this.Page);
                        return;
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(updGradeScheme, "Please Enter Grade Scheme Detail", this.Page);
                return;
            }
            //Session["PDPSHOWMESAGE"] = "1";
            BindListview();
            ClearControls();
            SetInitialRow();
            //Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "GradeScheme.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ClearControls()
    {
        txtGradeSchemeName.Text = string.Empty;
        ViewState["action"] = "add";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        ClearControls();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int SchemId = int.Parse(btnEdit.CommandArgument);
            //  int GradeNo = int.Parse(btnEdit.ToolTip);
            ViewState["SchemId"] = int.Parse(btnEdit.CommandArgument);
            //ViewState["GradeNo"] = int.Parse(btnEdit.ToolTip);
            ShowDetails(SchemId);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PDPGrading.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowDetails(int SchemId)
    {
        try
        {
            int rowIndex = 1;
            DataSet ds = objCommon.FillDropDown("ACD_GRADE", "GRADENO", "GRADE,GRADING_SCHEME_NAME,MAXMARK,MINMARK,DESC_GRADE,GRADE_TYPE,GRADEPOINT,STATUS", "GRADING_SCHEME_NO=" + SchemId.ToString() + "", "");
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                txtGradeSchemeName.Text = ds.Tables[0].Rows[0]["GRADING_SCHEME_NAME"].ToString();
                int STATUS = Convert.ToInt32(ds.Tables[0].Rows[0]["STATUS"].ToString());
                if (STATUS == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(false);", true);
                }
            }

            DataTable dt = (DataTable)ViewState["CurrentTable"];
            dt.Rows.Clear();
            dt.AcceptChanges();
            DataRow drCurrentRow;
            foreach (DataRow dRow in ds.Tables[0].Rows)
            {
                drCurrentRow = dt.NewRow();
                drCurrentRow["Column1"] = dRow["GRADE"].ToString();
                drCurrentRow["Column2"] = dRow["MINMARK"].ToString();
                drCurrentRow["Column3"] = dRow["MAXMARK"].ToString();
                if (dRow["DESC_GRADE"].ToString() == "FAIL")
                {
                    drCurrentRow["Column4"] = true;
                }
                else
                {
                    drCurrentRow["Column4"] = false;
                }
                drCurrentRow["Column5"] = dRow["GRADENO"].ToString();

                drCurrentRow["Column6"] = dRow["GRADEPOINT"].ToString();
                drCurrentRow["ID"] = rowIndex;// dRow["REFUNDPOLICY_ID"].ToString();
                dt.Rows.Add(drCurrentRow);
                rowIndex += 1;
            }

            ViewState["CurrentTable"] = dt;
            lvGradeScheme.DataSource = dt;
            lvGradeScheme.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "GradeScheme.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnRemove = sender as ImageButton;
            int Id = Convert.ToInt32(btnRemove.CommandArgument);
            int RowNumber = Convert.ToInt32(btnRemove.CommandName);
            string GradeNo = (btnRemove.ToolTip);
            int GradeNos = 0;
            if (GradeNo.ToString() != string.Empty)
            {
                GradeNos = Convert.ToInt32(GradeNo.ToString());
            }
          
            DataTable dt = ViewState["CurrentTable"] as DataTable;
            if (Id != 0)
            {
                CustomStatus cs = (CustomStatus)objGC.DeleteGradeScheme(GradeNos);
               if (cs.Equals(CustomStatus.RecordDeleted))
               {
                   dt.Rows[RowNumber - 1].Delete();
                   dt.AcceptChanges();
                   ViewState["CurrentTable"] = dt;
                   lvGradeScheme.DataSource = dt;
                   lvGradeScheme.DataBind();
                   string ID="";
                    foreach (var item in lvGradeScheme.Items)
                    {  
                        HiddenField hfdvalue=item.FindControl("hfdvalue") as HiddenField;
                         ID=hfdvalue.Value;
                    }
                  
                 //  CreateDatatable_GradeScheme();
                   if (ID.ToString()=="")
                   {
                       SetInitialRow();
                   }
                   SetPreviousData();
               }
               else
               {
                   objCommon.DisplayMessage(updGradeScheme, "Grade Details Are Not Deleted", this.Page);
                   return;
               }
               BindListview();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "GradeScheme.btnRemove_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region "Grade Scheme Allotment"
    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "DISTINCT COLLEGE_ID", "COLLEGE_NAME", "ISNULL(ACTIVE,0)=1", "COLLEGE_NAME");
        objCommon.FillDropDownList(ddlGradeScheme, "ACD_GRADE", "DISTINCT GRADING_SCHEME_NO", "GRADING_SCHEME_NAME", "GRADING_SCHEME_NO > 0 and STATUS=1 ", "");
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlStudyLevel.Items.Clear();
            ddlStudyLevel.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlDegree.Items.Clear();
            ddlDegree.Items.Insert(0, new ListItem("Please Select", "0"));
            lstbxScheme.Items.Clear();
         //   ddlGradeScheme.SelectedIndex = 0;
            if (ddlCollege.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlStudyLevel, "ACD_UA_SECTION US inner join ACD_COLLEGE_DEGREE_BRANCH DB  ON (US.UA_SECTION = DB.UGPGOT)", "DISTINCT UA_SECTION", "UA_SECTIONNAME", "COLLEGE_ID=" + ddlCollege.SelectedValue + "", "UA_SECTION");
            }
            ddlStudyLevel.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "domain.ddlCollege_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlStudyLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDegree.Items.Clear();
            ddlDegree.Items.Insert(0, new ListItem("Please Select", "0"));
            lstbxScheme.Items.Clear();
      //      ddlGradeScheme.SelectedIndex = 0;
            if (ddlStudyLevel.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH C ON D.DEGREENO=C.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "ISNULL(C.ACTIVE,0)=1 AND COLLEGE_ID=" + ddlCollege.SelectedValue + "AND UGPGOT=" + ddlStudyLevel.SelectedValue, "D.DEGREENAME ASC");
            }
            ddlDegree.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "domain.ddlStudyLevel_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lstbxScheme.Items.Clear();
       //     ddlGradeScheme.SelectedIndex = 0;
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillListBox(lstbxScheme, "ACD_SCHEME S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO) ", "S.SCHEMENO", "S.SCHEMENAME", "S.SCHEMENO>0 AND S.DEGREENO='" + ddlDegree.SelectedValue + "'AND S.COLLEGE_ID=" + ddlCollege.SelectedValue + "", "S.SCHEMENAME ASC");
            }
            lstbxScheme.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "domain.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objG.CollegeId = Convert.ToInt32(ddlCollege.SelectedValue);
            objG.BranchNo = Convert.ToInt32(ddlStudyLevel.SelectedValue);
            objG.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            objG.SchemeId = Convert.ToInt32(ddlGradeScheme.SelectedValue);
            string Scheme = "";
            foreach (ListItem item in lstbxScheme.Items)
            {
                if (item.Selected == true)
                {
                    Scheme += item.Value + ',';
                }
            }
            if (!string.IsNullOrEmpty(Scheme))
            {
                Scheme = Scheme.Substring(0, Scheme.Length - 1);
            }
            else
            {
                Scheme = "0";

            }
      
            objG.User = Convert.ToInt32(Session["userno"].ToString());
            //        objG.SchemeNo = Convert.ToInt32(Scheme);
            string SchemeNo = Scheme;
            CustomStatus cs = (CustomStatus)objGC.Insert_Update_Grading_Scheme_Allotment(objG, SchemeNo);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updAllotment, "Record Saved Successfully!", this.Page);
                BindAllotment();
                clear();
            }
            else if (cs.Equals(CustomStatus.TransactionFailed))
            {
                objCommon.DisplayMessage(updAllotment, "Record Not Saved Successfully!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "domain.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindAllotment()
    {
        try
        {
            DataSet ds = objGC.GetAllotment();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvAllotment.DataSource = ds;
                lvAllotment.DataBind();
            }
            else
            {
                lvAllotment.DataSource = null;
                lvAllotment.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "GradeScheme.BindAllotment() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            foreach (ListViewDataItem lv in lvAllotment.Items)
            {
                ImageButton btnEdit = sender as ImageButton;
                int Srno = int.Parse(btnEdit.CommandArgument);

                string SchemeNo = objCommon.LookUp("ACD_GRADING_SCHEME", "SCHEMENO", "SRNO=" + Srno);
                if (SchemeNo != null && SchemeNo != string.Empty && SchemeNo != "")
                {
                    CustomStatus cs = (CustomStatus)objGC.DeleteAllotment(Srno, Convert.ToInt32(SchemeNo));
                    if (cs.Equals(CustomStatus.RecordDeleted))
                    {
                        objCommon.DisplayMessage(updAllotment, "Record Deleted Successfully!", this.Page);
                        BindAllotment();
                        clear();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updAllotment, "Record Not Deleted !", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updAllotment, "Record Not Deleted !", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "GradeScheme.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void btnCancelAllotment_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void clear()
    {
        ddlCollege.SelectedIndex = 0;
        ddlStudyLevel.Items.Clear();
        ddlStudyLevel.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlDegree.Items.Clear();
        ddlDegree.Items.Insert(0, new ListItem("Please Select", "0"));
        lstbxScheme.Items.Clear();
        ddlGradeScheme.SelectedIndex = 0;

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("GradeScheme", "rptGradeScheme.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
      
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;  
            url += "&param=@P_SCHEME_ID=" + 0 + ",@P_COLLEGE_CODE=52";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updGradeScheme, this.updGradeScheme.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "GradeScheme.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion
   
}