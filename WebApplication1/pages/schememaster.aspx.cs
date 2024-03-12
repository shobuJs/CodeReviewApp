
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

public partial class Academic_schememaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MappingController objmp = new MappingController();
    Config objConfig = new Config();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    #region Page Load
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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }
            }
            //Populate the Drop Down Lists
            PopulateDropDown();            
            ViewState["schemeno"] = null;
            ViewState["action"] = null;
           // BindListView();  
           // ddlSchemeType.Enabled=false;
            BindListViewData();
        }
        GenerateDyanamicJavaScript();
        objCommon.SetLabelData("0");
        objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // dynamic page header addded by sarang 09/07/2021

    }
    #endregion
    #region GenerateDyanamicJavaScript
    private void GenerateDyanamicJavaScript()
    {
        DataSet ds = objCommon.FillDropDown("REFF", "Table_Btn_Setting", "", "", "");
        string[] btnArr = Convert.ToString(ds.Tables[0].Rows[0]["Table_Btn_Setting"]).Split(',');

        string ScriptToLoad = @"
                                var table = $('#example').DataTable({
                                lengthChange: false,
                                buttons: [
                                    {
                                        extend: 'copyHtml5',
                                        text: '<i class=""fa fa-files-o""></i>',
                                        titleAttr: 'Copy',
                                        exportOptions: {
                                            columns: ""thead th:not(th:first)""
                                        }
                                    },
                                    {
                                        extend: 'excelHtml5',
                                        text: '<i class=""fa fa-file-excel-o""></i>',
                                        titleAttr: 'Excel',
                                        exportOptions: {
                                            columns: ""thead th:not(th:first)""
                                        }
                                    },
                                    {
                                        extend: 'pdfHtml5',
                                        text: '<i class=""fa fa-file-pdf-o""></i>',
                                        titleAttr: 'Pdf',
                                        exportOptions: {
                                            columns: ""thead th:not(th:first)""
                                        }
                                    },
                                    {
                                        extend: 'print',
                                        text: '<i class=""fa fa-print""></i>',
                                        titleAttr: 'Print',
                                        exportOptions: {
                                            columns: ""thead th:not(th:first)""
                                        }
                                    },
                                    {
                                        extend: 'colvis',
                                        text: '<i class=""fa fa-eye""></i>',
                                        titleAttr: 'Column Visibility'
                                    }
                                ]
                            });

                            table.buttons().container().appendTo('#example_wrapper .col-sm-6:eq(0)');

                            var CopyVal =" + btnArr[0] + ";" + "var ExcelVal = " + btnArr[1] + ";" + "var PdfVal = " + btnArr[2] + ";" + "var ColvisVal = " + btnArr[3] + ";" +

                            @"var table = $('#example').DataTable();

                            if (CopyVal) {
                                table.button(0).enable();
                            }
                            else {
                                table.button(0).disable();
                            }

                            if (ExcelVal) {
                                table.button(1).enable();
                            }
                            else {
                                table.button(1).disable();
                            }

                            if (PdfVal) {
                                table.button(2).enable();
                            }
                            else {
                                table.button(2).disable();
                            }

                            if (ColvisVal) {
                                table.button(3).enable();
                            }
                            else {
                                table.button(3).disable();
                            }


                            var prm = Sys.WebForms.PageRequestManager.getInstance();
                            prm.add_endRequest(function () {
                                var table = $('#example').DataTable({
                                    lengthChange: false,
                                    //buttons: ['copy', 'excel', 'pdf', 'colvis']
                                    buttons: [
                                        {
                                            extend: 'copyHtml5',
                                            text: '<i class=""fa fa-files-o""></i>',
                                            titleAttr: 'Copy',
                                            exportOptions: {
                                                columns: ""thead th:not(th:first)""
                                            }
                                        },
                                        {
                                            extend: 'excelHtml5',
                                            text: '<i class=""fa fa-file-excel-o""></i>',
                                            titleAttr: 'Excel',
                                            exportOptions: {
                                                columns: ""thead th:not(th:first)""
                                            }
                                        },
                                        {
                                            extend: 'pdfHtml5',
                                            text: '<i class=""fa fa-file-pdf-o""></i>',
                                            titleAttr: 'Pdf',
                                            exportOptions: {
                                                columns: ""thead th:not(th:first)""
                                            }
                                        },
                                        {
                                            extend: 'print',
                                            text: '<i class=""fa fa-print""></i>',
                                            titleAttr: 'Print',
                                            exportOptions: {
                                                columns: ""thead th:not(th:first)""
                                            }
                                        },
                                        {
                                            extend: 'colvis',
                                            text: '<i class=""fa fa-eye""></i>',
                                            titleAttr: 'Column Visibility'
                                        }
                                    ]
                                });

                                table.buttons().container()
                                .appendTo('#example_wrapper .col-sm-6:eq(0)');

                                var CopyVal =" + btnArr[0] + ";" + "var ExcelVal = " + btnArr[1] + ";" + "var PdfVal = " + btnArr[2] + ";" + "var ColvisVal = " + btnArr[3] + ";" +

                                @"var table = $('#example').DataTable();

                                if (CopyVal) {
                                    table.button(0).enable();
                                }
                                else {
                                    table.button(0).disable();
                                }

                                if (ExcelVal) {
                                    table.button(1).enable();
                                }
                                else {
                                    table.button(1).disable();
                                }

                                if (PdfVal) {
                                    table.button(2).enable();
                                }
                                else {
                                    table.button(2).disable();
                                }

                                if (ColvisVal) {
                                    table.button(3).enable();
                                }
                                else {
                                    table.button(3).disable();
                                }
                            });
                        ";

        ScriptManager.RegisterStartupScript(this, GetType(), "script", ScriptToLoad, true);
    }
    #endregion
    #region Click Events
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            SchemeController objSC = new SchemeController();
            Scheme objScheme = new Scheme();
           // objScheme.SchemeName = ddlDegreeNo.SelectedItem.Text + "-" + ddlBranch.SelectedItem.Text + "-" + ddlYear.SelectedItem.Text ;
            objScheme.SchemeName = txtCurricu.Text;
            char[] ch = { ',' };
            string[] br = ddlBranch.SelectedValue.Split(ch);

            objScheme.BranchNo = Convert.ToInt32(br[0]);
            objScheme.DegreeNo = Convert.ToInt32(ddlDegreeNo.SelectedValue);
            objScheme.Dept_No = Convert.ToInt32(ddlDept.SelectedValue);
 
            objScheme.Year = Convert.ToInt32(ddlYear.SelectedValue);
           // objScheme.BatchNo = Convert.ToInt32(ddlBatchNo.SelectedValue);
            objScheme.NewScheme = 1;
            objScheme.CollegeCode = Session["colcode"].ToString();

            objScheme.SchemeTypeNo = Convert.ToInt32(ddlSchemeType.SelectedValue);
            objScheme.PatternNo = Convert.ToInt32(ddlPatternName.SelectedValue);
            objScheme.gradeMarks = ddlgrademarks.SelectedValue;
            objScheme.College_id = Convert.ToInt32(ddlCollege.SelectedValue);
            objScheme.GradingSchemeNo = Convert.ToInt32(ddlGradeScheme.SelectedValue);
            int Intake=Convert.ToInt32(ddlIntake.SelectedValue);

            int IsLatest = 0;

            if (ChkIsLatest.Checked == true)
            {
                IsLatest = 1;
            }
            else
            {
                IsLatest = 0;
            }
            
            string schNo = "-100";
          //  schNo = objCommon.LookUp("ACD_SCHEME", "SCHEMENO", "BRANCHNO=" + Convert.ToInt32(br[0]) + "AND DEGREENO=" + Convert.ToInt32(ddlDegreeNo.SelectedValue) + "AND ADMBATCH=" + Convert.ToInt32(ddlBatchNo.SelectedValue));
            schNo = objCommon.LookUp("ACD_SCHEME", "SCHEMENO", "BRANCHNO=" + Convert.ToInt32(br[0]) + "AND DEGREENO=" + Convert.ToInt32(ddlDegreeNo.SelectedValue) +" AND COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + "AND YEAR=" + Convert.ToInt32(ddlYear.SelectedValue));

            if (ViewState["action"] != null)
            {
               
                if (ViewState["action"].Equals("edit"))
                {
                    //Update Scheme

                    //objScheme.SchemeNo = Convert.ToInt32(schNo.ToString());

                    objScheme.SchemeNo = Convert.ToInt32(ViewState["SCHEME_NO"].ToString());
                   // objScheme.SchemeNo = Convert.ToInt32(ViewState["SCHEME"].ToString());

                    CustomStatus cs = (CustomStatus)objSC.UpdateScheme(objScheme, Convert.ToInt32(ddlyearName.SelectedValue), Convert.ToInt32(rdPaymentOption.SelectedValue), Intake,IsLatest);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.updScheme, "Curriculum Updated Successfully.", this.Page);
                        //if (ViewState["SCHEME"] != null)
                        //{
                         BindListViewData();
                        //}
                        //else
                        //{
                           // BindListView();
                        //}
                        Clear();
                        
                    }
                    ViewState["action"] = null;
                }
            }
            else
            {
                if (schNo == "")
                {
                    //Add Scheme
                    int schemeno = objSC.AddScheme(objScheme, Convert.ToInt32(ddlyearName.SelectedValue), Convert.ToInt32(rdPaymentOption.SelectedValue), Intake,IsLatest);
                    if (schemeno != -99)
                    {
                        objCommon.DisplayMessage(this.updScheme, "Curriculum Created Successfully.", this.Page);
                        ViewState["schemeno"] = schemeno.ToString();
                        //BindListView();
                        BindListViewData();
                        Clear();
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updScheme, "Transaction Failed!!", this.Page);
                    }
                }
                else
                    objCommon.DisplayMessage(this.updScheme, "Already Exists!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_schememaster.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        Response.Redirect(Request.Url.ToString());
        btnReport.Enabled = false;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("SchemeMaster", "rptSchemeMaster.rpt", 1, 0);
    }

    protected void btnCheckListReport_Click(object sender, EventArgs e)
    {
        if (ViewState["schemeno"] != null)
            ShowReport("Check_List", "rptSubjectCourseListSchemewise.rpt", 2, Convert.ToInt16(ViewState["schemeno"].ToString()));
        else
        {
            string schemeno = "0";
            if (ddlBranch.SelectedValue != "0" & ddlSchemeType.SelectedValue != "0")
            {
                schemeno = objCommon.LookUp("ACD_SCHEME", "SCHEMENO", ddlBranch.SelectedValue != "0" ? "BRANCHNO =" + ddlBranch.SelectedValue + " AND SCHEMETYPE =" + ddlSchemeType.SelectedValue : "SCHEMENO =0");
                if (schemeno == "")
                {
                    objCommon.DisplayMessage(this.updScheme, "Data Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updScheme, "Please Select Branch & Curriculum type", this.Page);
                return;
            }

            ShowReport("Check_List", "rptSubjectCourseListSchemewise.rpt", 2, Convert.ToInt16(schemeno));
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ddlCollege.SelectedIndex = 0;
            ddlDept.SelectedIndex = 0;
            ddlDegreeNo.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            ddlYear.SelectedIndex = 0;
            ddlSchemeType.SelectedIndex = 0;
            ddlGradeScheme.SelectedIndex = 0;
            ddlyearName.SelectedIndex = 0;
            ddlgrademarks.SelectedIndex = 0;
            ImageButton btnEdit = sender as ImageButton;
            int schemeno = int.Parse(btnEdit.CommandArgument);
            ViewState["SCHEME_NO"] = schemeno;
            if (ddlCollege.SelectedValue != "0")
            {
                ShowDetail(schemeno);
            }
            if (ddlDept.SelectedValue == "0" )
            {
               

                ShowDetailS(schemeno);
                BindListViewData();
            }
            ViewState["action"] = "edit";
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_schememaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
       
    }
    #endregion

    #region Other Events

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        lvScheme.DataSource = null;
        lvScheme.DataBind();
        BindListViewData();

        lblStatus.Text = string.Empty;
        //if (ddlDept.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", "CB.BRANCHNO> 0 AND  CB.DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + "AND CB.DEGREENO=" + Convert.ToInt32(ddlDegreeNo.SelectedValue), "LONGNAME");

        //}
        //else
        //    ddlBranch.SelectedIndex = 0;
       if (ddlDept.SelectedIndex > 0)
       {
           objCommon.FillDropDownList(ddlDegreeNo, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (D.DEGREENO=CDB.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "D.DEGREENO> 0 AND ISNULL(CDB.ACTIVE,0)=1 AND CDB.COLLEGE_ID=" + ddlCollege.SelectedValue + "AND CDB.DEPTNO=" + ddlDept.SelectedValue, "D.DEGREENO");
            ddlDegreeNo.Focus();

        }
        else
        {
            objCommon.DisplayMessage("Please Select College", this.Page);
            ddlDegreeNo.SelectedIndex = 0;
            ddlCollege.Focus();

        }
     
        ddlSchemeType.SelectedIndex = 0;
      //  ddlBatchNo.SelectedIndex = 0;
        ViewState["action"] = null;
        ViewState["schemeno"] = null;
        ddlYear.SelectedValue = "0";
        ddlSchemeType_SelectedIndexChanged(new object(), new EventArgs());
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedValue != "0")
        {
            btnReport.Enabled = true;
            btnCheckListReport.Enabled = true;
            lblStatus.Text = string.Empty;
            ddlYear.SelectedValue = "0";
            ddlSchemeType.SelectedValue = "0";
            ddlSchemeType_SelectedIndexChanged(new object(), new EventArgs());
            BindListViewData();
            //BindListView();
        }
        else
        {
            btnReport.Enabled = true;
            btnCheckListReport.Enabled = true;
            lblStatus.Text = string.Empty;
            ddlYear.SelectedValue = "0";
            ddlSchemeType.SelectedValue = "0";
            ddlSchemeType_SelectedIndexChanged(new object(), new EventArgs());
            Panel1.Visible = false;
            lvScheme.DataSource = null;
            lvScheme.DataBind();
        }
    }

    protected void ddlDegreeNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlDegreeNo.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", " DISTINCT D.DEPTNO", "D.DEPTNAME", "D.DEPTNO>0 AND B.DEGREENO = " + ddlDegreeNo.SelectedValue, "D.DEPTNAME");

        //}
        //else
        //{
        //    ddlDept.SelectedIndex = 0;
        //}
       if (ddlDegreeNo.SelectedIndex > 0)
        {
           // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON (B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", "CB.BRANCHNO> 0 AND  CB.DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + "AND CB.DEGREENO=" + Convert.ToInt32(ddlDegreeNo.SelectedValue), "LONGNAME");
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON (B.BRANCHNO=CB.BRANCHNO) INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON(CB.AFFILIATED_NO=AU.AFFILIATED_NO) ", "DISTINCT B.BRANCHNO", "(B.LONGNAME+' - '+AU.AFFILIATED_LONGNAME) AS BRANCHNAME", "CB.BRANCHNO> 0 AND ISNULL(CB.ACTIVE,0)=1 AND  CB.DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + "AND CB.DEGREENO=" + Convert.ToInt32(ddlDegreeNo.SelectedValue), "");
        }
        else
        {

            ddlBranch.SelectedIndex = 0;
        }
        ddlBranch.SelectedIndex = 0;
        ddlSchemeType.SelectedIndex = 0;
        ddlPatternName.SelectedIndex = 0;
       // ddlBatchNo.SelectedIndex = 0;
        Panel1.Visible = false;
        lvScheme.DataSource = null;
        lvScheme.DataBind();
        BindListViewData();
        btnReport.Enabled = true;
        btnCheckListReport.Enabled = true;
        lblStatus.Text = string.Empty;
        ViewState["action"] = null;
        ViewState["schemeno"] = null;
        ddlYear.SelectedValue = "0";
        ddlSchemeType_SelectedIndexChanged(new object(), new EventArgs());
    }

    #endregion

    #region User Defined Methods

    private void PopulateDropDown()
    {
        try
        {
            //FILL DROPDOWN 
            
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND COLLEGE_ID IN ("+Session["college_nos"].ToString()+")", "COLLEGE_ID");
            objCommon.FillDropDownList(ddlGradeScheme, "ACD_GRADE", "DISTINCT GRADING_SCHEME_NO", "GRADING_SCHEME_NAME", "GRADING_SCHEME_NO > 0 and STATUS=1 ", "");
            objCommon.FillDropDownList(ddlyearName, "ACD_YEAR", "DISTINCT YEAR", "YEARNAME", "YEAR > 0", "");
            objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "ACTIVE=1", "");
          ///  objCommon.FillDropDownList(ddlBatchNo, "ACD_ADMBATCH", "BATCHNO", "(M.MONTH+' - '+E.MONTH+' - '+YEAR_NAME) ADMBATCH", "BATCHNO>0", "BATCHNO DESC");


            //ddlBatchNo.SelectedIndex = 0;
            //DataSet ds = objmp.GetBatchMonthYear();
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlBatchNo.DataSource = ds;
            //    ddlBatchNo.DataValueField = ds.Tables[0].Columns[0].ToString();
            //    ddlBatchNo.DataTextField = ds.Tables[0].Columns[1].ToString();
            //    ddlBatchNo.DataBind();
            //    ddlBatchNo.SelectedIndex = 0;
            //}
            //else
            //{
            //    ddlBatchNo.Items.Clear();
            //    ddlBatchNo.Items.Add(new ListItem("Please Select", "0"));
            //}
            //ddlBatchNo.SelectedIndex = 0;

           // objCommon.FillDropDownList(ddlBatchNo, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVE = 1", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlYear, "ACD_EXAM_YEAR", "EXYEAR_NO", "YEAR_NAME", "EXYEAR_NO > 0 ", "YEAR_NAME DESC");

            // objCommon.FillDropDownList(ddlDegreeNo, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO> 0", "DEGREENO");
            objCommon.FillDropDownList(ddlSchemeType, "ACD_SCHEMETYPE", "SCHEMETYPENO", "SCHEMETYPE", "SCHEMETYPENO> 0 ", "SCHEMETYPENO");
            objCommon.FillDropDownList(ddlPatternName, "ACD_EXAM_PATTERN", "PATTERNNO", "PATTERN_NAME", "PATTERNNO> 0", "PATTERNNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_schememaster.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListView()
    {
        try
        {
            SchemeController objSC = new SchemeController();
            DataSet ds = objSC.GetScheme(Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlDegreeNo.SelectedValue));
          //  DataSet ds = objSC.GetScheme(Convert.ToInt32(1), Convert.ToInt32(1));

            if (ds.Tables[0].Rows.Count > 0)
            {
              //  lvScheme.Visible = true;
                Panel1.Visible = true;
                lvScheme.DataSource = ds;
                lvScheme.DataBind();

            }
            else
            {
                Panel1.Visible = false;
                lvScheme.DataSource = null;
                lvScheme.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_schememaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetailS(int schemeno)
    {
        SchemeController objSC = new SchemeController();
        SqlDataReader dr = objSC.GetSingleScheme(schemeno);

        //Show scheme create Detail
        if (dr != null)
        {
            if (dr.Read())
            {
              ViewState["SCHEME"] = schemeno.ToString();
              ViewState["schemeno"] = schemeno.ToString();
              ddlCollege.SelectedValue = dr["COLLEGE_ID"] == DBNull.Value ? "0" : dr["COLLEGE_ID"].ToString();
              //ddlBranch.SelectedValue
              ddlCollege_SelectedIndexChanged(new object(), new EventArgs());
              ddlDept.SelectedValue = dr["DEPTNO"] == DBNull.Value ? "0" : dr["DEPTNO"].ToString();
              ddlDept_SelectedIndexChanged(new object(), new EventArgs());
              ddlDegreeNo.SelectedValue = dr["DEGREENO"] == DBNull.Value ? "0" : dr["DEGREENO"].ToString();
              ddlDegreeNo_SelectedIndexChanged(new object(), new EventArgs());
             // ddlDept.SelectedValue = dr["DEPTNO"] == DBNull.Value ? "0" : dr["DEPTNO"].ToString();
              ddlBranch_SelectedIndexChanged(new object(), new EventArgs());
              //ddlDept_SelectedIndexChanged (new object(),new EventArgs());             
              ddlBranch.SelectedValue = dr["BRANCHNO"] == DBNull.Value ? "0" : dr["BRANCHNO"].ToString();
              ddlYear.SelectedValue = dr["YEAR"] == DBNull.Value ? "0" : dr["YEAR"].ToString();
              ddlgrademarks.SelectedValue = dr["GRADEMARKS"].ToString().Replace(" ", "");
              ddlPatternName.SelectedValue =  dr["PATTERNNO"] == DBNull.Value ? "0" : dr["PATTERNNO"].ToString();
              ddlSchemeType.SelectedValue = dr["SCHEMETYPE"] == DBNull.Value ? "0" : dr["SCHEMETYPE"].ToString();
              ddlGradeScheme.SelectedValue = dr["GRADING_SCHEME_NO"] == DBNull.Value ? "0" : dr["GRADING_SCHEME_NO"].ToString();            
              rdPaymentOption.SelectedValue = dr["BRIDGING"] == DBNull.Value ? "0" : dr["BRIDGING"].ToString();
              rdPaymentOption_SelectedIndexChanged(new object(), new EventArgs());
              ddlyearName.SelectedValue = dr["YEARNO"] == DBNull.Value ? "0" : dr["YEARNO"].ToString();
              if (ddlYear.SelectedValue != "0")
               {
               divCurricu.Visible=true;
               txtCurricu.Text = dr["SCHEMENAME"] == DBNull.Value ? "0" : dr["SCHEMENAME"].ToString();
              }
              //int isLatest = Convert.ToInt32(Convert.ToBoolean(dr["IS_LATEST"]) == true ? "1" : "0");
              bool latest = Convert.ToBoolean(dr["IS_LATEST"] == DBNull.Value ? false : dr["IS_LATEST"]);

              if (latest == true)
              {
                  ChkIsLatest.Checked = true;
              }
              else
              {
                  ChkIsLatest.Checked = false;
              }
            }
        }
        if (dr != null) dr.Close();
    }
    private void ShowDetail(int schemeno)
    {
        SchemeController objSC = new SchemeController();
        SqlDataReader dr = objSC.GetSingleScheme(schemeno);

        //Show scheme create Detail
        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["schemeno"] = schemeno.ToString();
                ddlCollege.SelectedValue = dr["COLLEGE_ID"] == DBNull.Value ? "0" : dr["COLLEGE_ID"].ToString();
                ddlBranch.SelectedValue = dr["BRANCHNO"] == DBNull.Value ? "0" : dr["BRANCHNO"].ToString();
                ddlDept.SelectedValue = dr["DEPTNO"] == DBNull.Value ? "0" : dr["DEPTNO"].ToString();
                ddlYear.SelectedValue = dr["YEAR"] == DBNull.Value ? "0" : dr["YEAR"].ToString();
                ddlDegreeNo.SelectedValue = dr["DEGREENO"] == DBNull.Value ? "0" : dr["DEGREENO"].ToString();
               // ddlBatchNo.SelectedValue = dr["ADMBATCH"] == DBNull.Value ? "0" : dr["ADMBATCH"].ToString();
                ddlgrademarks.SelectedValue = dr["GRADEMARKS"].ToString().Replace(" ", "");
                ddlSchemeType.SelectedValue = dr["SCHEMETYPE"] == DBNull.Value ? "0" : dr["SCHEMETYPE"].ToString();
                ddlPatternName.SelectedValue = dr["PATTERNNO"] == DBNull.Value ? "0" : dr["PATTERNNO"].ToString();
                ddlGradeScheme.SelectedValue = dr["GRADING_SCHEME_NO"] == DBNull.Value ? "0" : dr["GRADING_SCHEME_NO"].ToString();
                if (ddlYear.SelectedValue != "0")
                {
                    divCurricu.Visible = true;
                    txtCurricu.Text = dr["SCHEMENAME"] == DBNull.Value ? "0" : dr["SCHEMENAME"].ToString();
                }
                bool latest = Convert.ToBoolean(dr["IS_LATEST"] == DBNull.Value ? false : dr["IS_LATEST"]);

                if (latest == true)
                {
                    ChkIsLatest.Checked = true;
                }
                else
                {
                    ChkIsLatest.Checked = false;
                }
            }
        }
        if (dr != null) dr.Close();
    }

    private void ShowReport(string reportTitle, string rptFileName, int type, int schemeno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (type == 1)
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString() + ",@P_DEPT_NO=" + ddlDept.SelectedValue + ",@P_DEGREE_NO=" + ddlDegreeNo.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue;
            else if (type == 2)
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + schemeno.ToString() + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updScheme, this.updScheme.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_RollListForScrutiny.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void Clear()
    {
        txtCurricu.Text = "";
        ddlyearName.SelectedIndex = 0;
        rdPaymentOption.ClearSelection();
        ddlCollege.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
      //  ddlBatchNo.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSchemeType.SelectedIndex = 0;
        ddlPatternName.SelectedIndex = 0;
        ddlgrademarks.SelectedIndex = 0;
        ddlDegreeNo.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        ddlGradeScheme.SelectedIndex = 0;
        divCurricu.Visible = false;
        ChkIsLatest.Checked = false;
       // Response.Redirect(Request.Url.ToString());

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=schememaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=schememaster.aspx");
        }
    }
    #endregion

    // on Scheme type selection bind pattern name


    //clear schemetype and pattername Dropdown 


    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
          if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEPT CD ON(D.DEPTNO=CD.DEPTNO)", " DISTINCT D.DEPTNO", "D.DEPTNAME", "D.DEPTNO>0 AND ISNULL(CD.ACTIVESTATUS,0)=1 AND CD.COLLEGE_ID = " + ddlCollege.SelectedValue, "D.DEPTNAME");

        }
        else
        {
            ddlDept.SelectedIndex = 0;
        }


        //if (ddlCollege.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlDegreeNo, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (D.DEGREENO=CDB.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "D.DEGREENO> 0 AND CDB.COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENO");
        //    ddlDegreeNo.Focus();
            
        //}
        //else
        //{
        //    objCommon.DisplayMessage("Please Select College", this.Page);            
        //    ddlDegreeNo.SelectedIndex = 0;
        //    ddlCollege.Focus();
            
        //}
       
        ddlBranch.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
        ddlSchemeType.SelectedIndex = 0;
        ddlPatternName.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        //ddlBatchNo.SelectedIndex = 0;
        Panel1.Visible = false;
        lvScheme.DataSource = null;
        lvScheme.DataBind();
        BindListViewData();
        btnReport.Enabled = true;
        btnCheckListReport.Enabled = true;
        lblStatus.Text = string.Empty;
        ViewState["action"] = null;
        ViewState["schemeno"] = null;
        ddlYear.SelectedValue = "0";
        ddlSchemeType_SelectedIndexChanged(new object(), new EventArgs());
    }

    protected void BindListViewData()
    {
        SchemeController objSC = new SchemeController();
        // DataSet ds = objSC.GetScheme(Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlDegreeNo.SelectedValue));
        //DataSet ds = objCommon.FillDropDown("ACD_SCHEME S	left join acd_year ye on ye.YEAR=s.YEARNO INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO)INNER JOIN ACD_DEGREE D ON (S.DEGREENO = D.DEGREENO)  LEFT JOIN ACD_ADMBATCH AB ON ( S.ADMBATCH = AB.BATCHNO) INNER  JOIN ACD_GRADE GD ON (S.GRADING_SCHEME_NO=GD.GRADING_SCHEME_NO)", "DISTINCT  GRADING_SCHEME_NAME", " case when isnull(IS_LATEST,0)=1 then 'Yes' when isnull(IS_LATEST,0)=0 then 'No' end as IS_LATEST,YEARNAME,S.SCHEMENO,SCHEMENAME,DEGREENAME + '-'+ LONGNAME AS PROGRAM,S.ADMBATCH ", "S.SCHEMENO>0", "S.ADMBATCH DESC, S.SCHEMENO DESC");


        string SP_Name2 = "PKG_ACD_GET_SCHEEME_DETAILS";
        string SP_Parameters2 = "@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO,@P_DEPTNO,@P_YEAR";
        string Call_Values2 = "" + Convert.ToInt32(ddlCollege.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlDegreeNo.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlBranch.SelectedValue.ToString()) + "," + Convert.ToString(ddlDept.SelectedValue.ToString()) + "," + Convert.ToString(ddlYear.SelectedValue.ToString());
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (ds.Tables[0].Rows.Count > 0)
        {
            //  lvScheme.Visible = true;
            Panel1.Visible = true;
            lvScheme.DataSource = ds;
            lvScheme.DataBind();

        }
        else
        {
            Panel1.Visible = false;
            lvScheme.DataSource = null;
            lvScheme.DataBind();
        }
    }
    //protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlYear.SelectedValue != "0")
    //    {
    //        btnReport.Enabled = true;
    //        btnCheckListReport.Enabled = true;
    //        lblStatus.Text = string.Empty;
    //        BindListView();
    //        if(ddlDegreeNo.SelectedValue!="0" && ddlBranch.SelectedValue!="0")
    //        {
    //              divCurricu.Visible = true;
    //              txtCurricu.Text = ddlDegreeNo.SelectedItem.Text + "-" + ddlBranch.SelectedItem.Text + "-" + ddlYear.SelectedItem.Text + "(" + ddlSchemeType.SelectedItem.Text+")";
    //        }
          
           
    //    }
    //    else
    //    {
    //        btnReport.Enabled = true;
    //        btnCheckListReport.Enabled = true;
    //        lblStatus.Text = string.Empty;
    //        lvScheme.DataSource = null;
    //        lvScheme.DataBind();
    //        divCurricu.Visible = false;
    //        txtCurricu.Text = "";
    //    }
    //}
    protected void rdPaymentOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdPaymentOption.SelectedValue == "1")
        {
            yearname.Visible = true;
        }
        else if (rdPaymentOption.SelectedValue == "2")
        {
            yearname.Visible = false;
        }
    }
    protected void ddlSchemeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSchemeType.SelectedValue != "0")
        {
            btnReport.Enabled = true;
            btnCheckListReport.Enabled = true;
            lblStatus.Text = string.Empty;
           // BindListView();
            string COLLEGE_CODE = objCommon.LookUp("ACD_COLLEGE_MASTER", "SHORT_NAME", "COLLEGE_ID=" + ddlCollege.SelectedValue);
            //string DEGREECODE = objCommon.LookUp("ACD_DEGREE", "CODE", "DEGREENO=" + ddlDegreeNo.SelectedValue);
            string AFFCODE = objCommon.LookUp("ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON (B.BRANCHNO=CB.BRANCHNO) INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON(CB.AFFILIATED_NO=AU.AFFILIATED_NO) ", "AFFILIATED_SHORTNAME", "CB.BRANCHNO> 0 AND  CB.DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + "AND CB.DEGREENO=" + Convert.ToInt32(ddlDegreeNo.SelectedValue) + "AND CB.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue));
            string BRANCHNAME = objCommon.LookUp("ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON (B.BRANCHNO=CB.BRANCHNO) INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON(CB.AFFILIATED_NO=AU.AFFILIATED_NO) ", "B.LONGNAME", "CB.BRANCHNO> 0 AND  CB.DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + "AND CB.DEGREENO=" + Convert.ToInt32(ddlDegreeNo.SelectedValue) + "AND CB.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue));
            if (ddlDegreeNo.SelectedValue != "0" && ddlBranch.SelectedValue != "0" && ddlYear.SelectedValue!="0")
            {
                divCurricu.Visible = true;
                //txtCurricu.Text = ddlDegreeNo.SelectedItem.Text + "-" + ddlBranch.SelectedItem.Text + "-" + ddlYear.SelectedItem.Text + "(" + ddlSchemeType.SelectedItem.Text + ")";
                txtCurricu.Text = COLLEGE_CODE + "-" + ddlDegreeNo.SelectedItem.Text + "-" + BRANCHNAME + "-" + AFFCODE + "-" + ddlYear.SelectedItem.Text;
            
            }


        }
        else
        {
            btnReport.Enabled = true;
            btnCheckListReport.Enabled = true;
            lblStatus.Text = string.Empty;
          //  lvScheme.DataSource = null;
           // lvScheme.DataBind();
            divCurricu.Visible = false;
            txtCurricu.Text = "";
        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSchemeType.SelectedIndex = 0;
         // BindListView();
        BindListViewData();
    }
}
    #endregion