using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Projects_Student_Progression : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentProgression objSP = new StudentProgression();
    StudentProgressionController objSPC = new StudentProgressionController();

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

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                    //this.CheckPageAuthorization();
                    ViewState["action"] = "add";
                    ViewState["UANO"] = Session["userno"].ToString();
                    FilldropDown();
                    BindStudentProgression();
                    BindRule();
                    binddata();
                    //       BindRuleAllocation();
                    //    bindFromSemester();
                }


                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                ViewState["strid"] = 0;

                ViewState["Row"] = null;
                //       lvSemester.Visible = false;
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header

                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Student_Progression.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void FilldropDown()
    {
        objCommon.FillDropDownList(ddlclgbrid, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");       
        objCommon.FillDropDownList(ddltosembrid, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO=100", "SEMESTERNO");
        ddltosembrid.SelectedValue = "100";
        objCommon.FillDropDownList(ddlfromsembrid, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
       
        objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "ISNULL(ACTIVE,0)=1", "COLLEGE_NAME");
        objCommon.FillDropDownList(ddlFromSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "SEMESTERNO");
        objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVE = 1", "BATCHNO DESC");
        objCommon.FillDropDownList(lstbxRule, "ACD_STUDENT_PROGRESSION_RULE", "STUDENT_PROGRESSION_RULEID", "RULENAME", "STUDENT_PROGRESSION_RULEID>0", "");

        objCommon.FillDropDownList(ddlintakeAlot, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVE = 1", "BATCHNO DESC");
        objCommon.FillDropDownList(ddlfac, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        objCommon.FillDropDownList(ddlsemester, "ACD_STUDENT_PROGRESSION_BRIDGING_RULE SR INNER JOIN ACD_SEMESTER S ON S.SEMESTERNO=SR.SEMESTERNO_FROM", "DISTINCT SEMESTERNO_FROM", "SEMESTERNAME", "", "SEMESTERNO_FROM");

        objCommon.FillDropDownList(ddlsessionresult, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlfacresult, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        //objCommon.FillDropDownList(ddlsemresult, "ACD_STUDENT_PROGRESSION_BRIDGING_RULE SR INNER JOIN ACD_SEMESTER S ON S.SEMESTERNO=SR.SEMESTERNO_FROM", "DISTINCT SEMESTERNO_FROM", "SEMESTERNAME", "", "SEMESTERNO_FROM");

        objCommon.FillDropDownList(ddlsemresult, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO=100", "SEMESTERNO");        
    }
    private void BindStudyLevel()
    {
        objCommon.FillDropDownList(ddlStudyLevel, "ACD_COLLEGE_DEGREE_BRANCH a inner join ACD_UA_SECTION b on (a.UGPGOT=UA_SECTION)", "DISTINCT(UA_SECTION)", "UA_SECTIONNAME,UA_SECTION", "COLLEGE_ID = " + ddlFaculty.SelectedValue + "", "");

    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindStudyLevel();

    }
    protected void BindStudentProgression()
    {
        string query1 = string.Empty;
        string query2 = string.Empty;
        string query3 = string.Empty;
        string query4 = "";

        string SP_Name2 = "PKG_ACD_GET_ALLOCATION_DETAILS";
        string SP_Parameters2 = "@P_OUTPUT";
        string Call_Values2 = "" + ",0";
        DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (dsStudList != null && dsStudList.Tables[0].Rows.Count > 0)
        {
            lvStudentProgression.DataSource = dsStudList;
            lvStudentProgression.DataBind();
        }
        else
        {
            lvStudentProgression.DataSource = null;
            lvStudentProgression.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int Active = 0;
            DataTable dt = (DataTable)ViewState["Row"];
            if (dt == null)
            {
                objCommon.DisplayMessage(this.updStudentProgression, "Please add atleast one record of semester", this.Page);
            }
            else
            {
                if (rdioselect.SelectedValue == "")
                {
                    objCommon.DisplayMessage(updStudentProgression, "Please Select Option", this.Page);
                    return;
                }
                dt.TableName = "StudentProgression";
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                objSP.rulename = txtRuleName.Text;
                objSP.college_id = Convert.ToInt32(ddlFaculty.SelectedValue);
                objSP.ua_section = Convert.ToInt32(ddlStudyLevel.SelectedValue);
                string[] program;
                if (ddlProgram.SelectedValue == "0")
                {
                    program = "0,0".Split(',');
                }
                else
                {
                    program = ddlProgram.SelectedValue.Split(',');
                }
                string year = "";
                foreach (ListItem listItem in chkyear.Items)
                {
                    if (listItem.Selected==true)
                    {
                        year += listItem.Value + ',';     
                    }
                }
                if (!string.IsNullOrEmpty(year))
                {
                    year = year.TrimEnd(',');
                }
                if (year == string.Empty)
                {
                    objCommon.DisplayMessage(updStudentProgression, "Please Select Year", this.Page);
                    return;
                }
                objSP.degreeno = Convert.ToInt32(program[0]);
                objSP.branchno = Convert.ToInt32(program[1]);
                objSP.affiliated_no = Convert.ToInt32(program[2]);
                objSP.userid = Convert.ToInt32(Session["userno"].ToString());
                int STUDENT_PROGRESSION_RULEID;
                if (ViewState["action"].ToString() == "add")
                {
                    STUDENT_PROGRESSION_RULEID = 0;
                    objSP.student_progression_ruleid = STUDENT_PROGRESSION_RULEID;
                }
                else
                {
                    STUDENT_PROGRESSION_RULEID = Convert.ToInt32(ViewState["STUDENT_PROGRESSION_RULEID"]);
                    objSP.student_progression_ruleid = Convert.ToInt32(ViewState["STUDENT_PROGRESSION_RULEID"]);
                }
                if (hfdStat.Value == "true")
                    Active = 1;
                else
                    Active = 0;
                CustomStatus cs = (CustomStatus)objSPC.AddStudentProgression(objSP, STUDENT_PROGRESSION_RULEID, ds.GetXml(), year, Convert.ToInt32(rdioselect.SelectedValue), Active);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.updStudentProgression, "Student Progression Saved Successfully!", this.Page);
                    BindStudentProgression();
                    clear();
                }
                else if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.updStudentProgression, "Student Progression Updated Successfully ", this.Page);
                    ViewState["action"] = "add";
                    clear();
                    BindStudentProgression();
                }
                else if (cs.Equals(CustomStatus.DuplicateRecord))
                {
                    objCommon.DisplayMessage(this.updStudentProgression, "Rule Name Already Exist", this.Page);
                    ViewState["action"] = "add";
                }
                else if (cs.Equals(CustomStatus.TransactionFailed))
                {
                    objCommon.DisplayMessage(this.updStudentProgression, "Error", this.Page);
                    ViewState["action"] = "add";
                }
                ViewState["Row"] = null;
                pnllst.Visible = false;
                txtFailModule.Text="";
                TxtCredit.Text="";
                Txtcgpa.Text = "";
                lvSemester.DataSource = null;
                lvSemester.DataBind();
                //lvSemester.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Student_Progression.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void clear()
    {
        txtRuleName.Text = string.Empty;
        txtFailModule.Text = string.Empty;
        ddlFaculty.SelectedIndex = 0;
        ddlStudyLevel.SelectedIndex = 0;
        ddlProgram.Items.Clear();
        ddlProgram.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlFromSemester.SelectedIndex = 0;
        ddlToSemester.SelectedIndex = 0;
        pnllst.Visible = false;
        lvSemester.DataSource = null;
        lvSemester.DataBind();
        rdioselect.ClearSelection();
        failmodule.Visible=false;
        Credit.Visible=false;
        cgpa.Visible = false;
        divyear.Visible = false;
    }
    private void Clearlv()
    {
        lvSemester.DataSource = null;
        lvSemester.DataBind();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        clear();
        Clearlv();
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int STUDENT_PROGRESSION_RULEID = int.Parse(btnEdit.CommandArgument);
            ViewState["STUDENT_PROGRESSION_RULEID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(STUDENT_PROGRESSION_RULEID);
            string query = string.Empty;
            string query1 = string.Empty;
            query = "ACD_STUDENT_PROGRESSION_RULE_SEMESTER SP";
            query1 = "year,SP.SEMESTERNO_FROM as FromSemesterNo,DBO.FN_DESC('SEMESTER',SEMESTERNO_FROM) as FromSemester,SP.SEMESTERNO_TO as ToSemesterNo,OPTION_WISE,DBO.FN_DESC('SEMESTER',SEMESTERNO_TO) as ToSemester,SP.MAX_FAIL_MODULE as FailModule";
            DataSet ds = objCommon.FillDropDown(query.ToString(), "STUDENT_PROGRESSION_RULE_SEMID", query1.ToString(), "STUDENT_PROGRESSION_RULEID=" + STUDENT_PROGRESSION_RULEID.ToString() + "", "");
            ViewState["Row"] = ds.Tables[0];
            pnllst.Visible = true;
            lvSemester.DataSource = ViewState["Row"];
            lvSemester.DataBind();
            lvSemester.Visible = true;
            rdioselect.SelectedValue = ds.Tables[0].Rows[0]["OPTION_WISE"].ToString();
            ddlFromSemester.SelectedValue = ds.Tables[0].Rows[0]["FromSemesterNo"].ToString();
            ddlFromSemester_SelectedIndexChanged(new object(), new EventArgs());
            ddlToSemester.SelectedValue = ds.Tables[0].Rows[0]["ToSemesterNo"].ToString();
            divyear.Visible = true;
            BINDYEAR();
            if (rdioselect.SelectedValue == "1")
            {
                failmodule.Visible = true;
                txtFailModule.Text = ds.Tables[0].Rows[0]["FailModule"].ToString();
            }
            else if (rdioselect.SelectedValue == "2")
            {
                Credit.Visible = true;
                TxtCredit.Text = ds.Tables[0].Rows[0]["FailModule"].ToString();
            }
            else if (rdioselect.SelectedValue == "3")
            {
                cgpa.Visible = true;
                Txtcgpa.Text = ds.Tables[0].Rows[0]["FailModule"].ToString();
            }
            
            string year = (ds.Tables[0].Rows[0]["year"].ToString());
            string[] pgm = year.Split(',');
            for (int j = 0; j < pgm.Length; j++)
            {
                for (int i = 0; i < chkyear.Items.Count; i++)
                {
                    if (pgm[j] == chkyear.Items[i].Value)
                    {
                        chkyear.Items[i].Selected = true;
                    }
                }
            }
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Student_Progression.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int STUDENT_PROGRESSION_RULEID)
    {
        try
        {
            string query1 = string.Empty;
            string query2 = string.Empty;
            string query3 = string.Empty;
            query1 = "SPR.STUDENT_PROGRESSION_RULEID,SPR.RULENAME,SPR.COLLEGE_ID,SPR.UA_SECTION";
            query3 = "SPR.ACTIVE,(CONVERT(NVARCHAR(5),D.DEGREENO) + ','+ CONVERT(NVARCHAR(5),B.BRANCHNO) + ',' + CONVERT(NVARCHAR(5),AF.AFFILIATED_NO))as PROGRAMID";
            query2 = "ACD_STUDENT_PROGRESSION_RULE SPR inner join ACD_COLLEGE_MASTER CM on (CM.COLLEGE_ID = SPR.COLLEGE_ID)";
            query2 = query2 + "inner join ACD_UA_SECTION US on (US.UA_SECTION = SPR.UA_SECTION) inner join ACD_DEGREE D on (D.DEGREENO = SPR.DEGREENO)";
            query2 = query2 + "inner join ACD_BRANCH B on (B.BRANCHNO = SPR.BRANCHNO) inner join ACD_AFFILIATED_UNIVERSITY AF on (AF.AFFILIATED_NO = SPR.AFFILIATED_NO)";

            DataSet ds = objCommon.FillDropDown(query2.ToString(), query3.ToString(), query1.ToString(), "STUDENT_PROGRESSION_RULEID=" + STUDENT_PROGRESSION_RULEID.ToString() + "", "");
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ACTIVE"].ToString().Trim() == "1")
                {
                    //chkActive.Checked = true;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(true);", true);
                }
                else
                {
                    //chkActive.Checked = false;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(false);", true);
                }
                txtRuleName.Text = ds.Tables[0].Rows[0]["RULENAME"].ToString();
                ddlFaculty.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                BindStudyLevel();
                ddlStudyLevel.SelectedValue = ds.Tables[0].Rows[0]["UA_SECTION"].ToString();
                BindProgram();
                ddlProgram.SelectedValue = ds.Tables[0].Rows[0]["PROGRAMID"].ToString();
                lvSemester.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Student_Progression.ShowDetails_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
        ViewState["action"] = "add";
    }

    protected void ddlStudyLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProgram();
    }

    private void BindProgram()
    {
        string query1 = string.Empty;
        string query2 = string.Empty;
        string query3 = string.Empty;

        query1 = "(D.DEGREENAME + ' - ' + BA.LONGNAME + ' - ' + AU.AFFILIATED_SHORTNAME)";
        query2 = "(CONVERT(NVARCHAR(5),CD.DEGREENO) + ','+ CONVERT(NVARCHAR(5),CD.BRANCHNO) + ',' + CONVERT(NVARCHAR(5),CD.AFFILIATED_NO))";
        query3 = "ACD_COLLEGE_MASTER CM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID)";
        query3 = query3 + "INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO) INNER JOIN ACD_BRANCH BA ON (CD.BRANCHNO = BA.BRANCHNO)";
        query3 = query3 + "INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON (CD.AFFILIATED_NO = AU.AFFILIATED_NO)";

       //objCommon.FillDropDownList(ddlProgram, query3.ToString(), query2.ToString(), query1.ToString(), "CD.BRANCHNO=" + ddlStudyLevel.SelectedValue + "", "");
        objCommon.FillDropDownList(ddlProgram, query3.ToString(), query2.ToString(), query1.ToString(), "CD.COLLEGE_ID=" + ddlFaculty.SelectedValue + "", "");
    }
    protected void ddlFromSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlToSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO <>" + ddlFromSemester.SelectedValue + "", "");
    }

    private void BindDropDown(DropDownList ddlList, DataSet ds)
    {
        ddlList.Items.Clear();
        ddlList.Items.Add("Please Select");
        ddlList.SelectedItem.Value = "0";

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlList.DataSource = ds;
            ddlList.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlList.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlList.DataBind();
            ddlList.SelectedIndex = 0;
        }
        ds = null;
    }


    private string validate()
    {
        string _validate = string.Empty;
        if (ddlFromSemester.SelectedValue == "0")
        {
            _validate = "Please Select From Semester";
        }
        else if (ddlToSemester.SelectedValue == "0")
        {
            _validate = "Please Select To Semester";
        }
        if (rdioselect.SelectedValue == "1")
        {
            if (txtFailModule.Text == string.Empty)
            {
                _validate = "Please Enter Max. No. of Fail Modules";
            }
        }
        else if (rdioselect.SelectedValue == "2")
        {
            if (TxtCredit.Text == string.Empty)
            {
                _validate = "Please Enter Credit";
            }
        }
        else if (rdioselect.SelectedValue == "3")
        {
            if (Txtcgpa.Text == string.Empty)
            {
                _validate = "Please Enter cgpa";
            }
        }
       

        return _validate;
    }
    protected void Add_Click(object sender, EventArgs e)
    {
        if (rdioselect.SelectedValue == "")
        {
            objCommon.DisplayMessage(this.updStudentProgression, "Please Select Option!", this.Page);
            return;
        }
        string _validateData = validate();
        if (_validateData != string.Empty)
        {
            objCommon.DisplayMessage(updStudentProgression, _validateData, this.Page);
            return;
        }
        // Add_Datatable_Refund();
        ShowLV();
        lvSemester.Visible = true;
        ddlFromSemester.Enabled = true;
        ddlToSemester.Enabled = true;
        ddlFromSemester.SelectedIndex = 0;
        ddlToSemester.SelectedIndex = 0;
        txtFailModule.Text = string.Empty;
        //ViewState["action"] = "add";
        ViewState["actionsem"] = "add";
    }

    protected DataTable ShowLV()
    {
        DataTable dt = new DataTable();
        dt.TableName = "StudentProgression";
        if (ViewState["Row"] != null)
        {
            dt = (DataTable)ViewState["Row"];
            DataRow dr = null;
            if (dt.Rows.Count > 0)
            {
                if (ViewState["actionsem"].ToString() == "edit1")
                {
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        if (dr1["STUDENT_PROGRESSION_RULE_SEMID"].ToString() == hdnRuleSemId.Value.ToString())
                        {
                            dr1["FromSemesterNo"] = ddlFromSemester.SelectedValue;
                            dr1["ToSemesterNo"] = ddlToSemester.SelectedValue;
                            dr1["FromSemester"] = ddlFromSemester.SelectedItem;
                            dr1["ToSemester"] = ddlToSemester.SelectedItem;
                            if (rdioselect.SelectedValue == "1")
                            {
                                dr1["FailModule"] = txtFailModule.Text;
                            }
                            else if (rdioselect.SelectedValue == "2")
                            {
                                dr1["FailModule"] = TxtCredit.Text;
                            }
                            else if (rdioselect.SelectedValue == "3")
                            {
                                dr1["FailModule"] = Txtcgpa.Text;
                            }
                            dt.AcceptChanges();
                            ViewState["Row"] = dt;
                            lvSemester.DataSource = ViewState["Row"];
                            lvSemester.DataBind();
                            lvSemester.Visible = true;
                        }
                    }
                }
                else
                {
                    string fromSemester = ddlFromSemester.SelectedItem.Text;
                    string toSemester = ddlToSemester.SelectedItem.Text;
                    //    int failModule = Convert.ToInt32(txtFailModule.Text);
                    //       DataRow[] rows = dt.Select("FromSemester ='" + fromSemester + "'and ToSemester ='" + toSemester + "'and FailModule='" + failModule + "'");
                    DataRow[] rows = dt.Select("FromSemester ='" + fromSemester + "'and ToSemester ='" + toSemester + "'");
                    if (rows.Length > 0)
                    {
                        objCommon.DisplayMessage(this.updStudentProgression, "From Semester and To Semester are already exist!", this.Page);
                    }
                    else
                    {
                        dr = dt.NewRow();
                        dr["STUDENT_PROGRESSION_RULE_SEMID"] = Convert.ToInt32(ViewState["strid"]) + 1;
                        dr["FromSemesterNo"] = ddlFromSemester.SelectedValue;
                        dr["ToSemesterNo"] = ddlToSemester.SelectedValue;
                        dr["FromSemester"] = ddlFromSemester.SelectedItem;
                        dr["ToSemester"] = ddlToSemester.SelectedItem;
                        if (rdioselect.SelectedValue == "1")
                        {
                            dr["FailModule"] = txtFailModule.Text;
                        }
                        else if (rdioselect.SelectedValue == "2")
                        {
                            dr["FailModule"] = TxtCredit.Text;
                        }
                        else if (rdioselect.SelectedValue == "3")
                        {
                            dr["FailModule"] = Txtcgpa.Text;
                        }
                        ViewState["strid"] = Convert.ToInt32(ViewState["strid"]) + 1;
                        dt.Rows.Add(dr);
                        ViewState["Row"] = dt;
                        lvSemester.DataSource = ViewState["Row"];
                        lvSemester.DataBind();
                        lvSemester.Visible = true;
                    }

                }
            }
        }
        else
        {
            if (dt.Columns.Count <= 0)
            {
                dt.Columns.Add("STUDENT_PROGRESSION_RULE_SEMID", typeof(int));
                dt.Columns.Add("FromSemesterNo", typeof(int));
                dt.Columns.Add("ToSemesterNo", typeof(int));
                dt.Columns.Add("FromSemester", typeof(string));
                dt.Columns.Add("ToSemester", typeof(string));
                dt.Columns.Add("FailModule", typeof(int));
                dt.Columns.Add("OptionWise", typeof(int));
            }
            string fromSemester = ddlFromSemester.SelectedItem.Text;
            string toSemester = ddlToSemester.SelectedItem.Text;
            //    int failModule = Convert.ToInt32(txtFailModule.Text);
            //       DataRow[] rows = dt.Select("FromSemester ='" + fromSemester + "'and ToSemester ='" + toSemester + "'and FailModule='" + failModule + "'");
            DataRow[] rows = dt.Select("FromSemester ='" + fromSemester + "'and ToSemester ='" + toSemester + "'");
            if (rows.Length > 0)
            {
                objCommon.DisplayMessage(this.updStudentProgression, "From Semester and To Semester are already exist!", this.Page);
            }
            else
            {
                DataRow dr1 = dt.NewRow();
                dr1 = dt.NewRow();
                dr1["STUDENT_PROGRESSION_RULE_SEMID"] = Convert.ToInt32(ViewState["strid"]) + 1;
                dr1["FromSemesterNo"] = ddlFromSemester.SelectedValue;
                dr1["ToSemesterNo"] = ddlToSemester.SelectedValue;
                dr1["FromSemester"] = ddlFromSemester.SelectedItem;
                dr1["ToSemester"] = ddlToSemester.SelectedItem;
                if (rdioselect.SelectedValue == "1")
                {
                    dr1["FailModule"] = txtFailModule.Text;
                }
                else if (rdioselect.SelectedValue == "2")
                {
                    dr1["FailModule"] = TxtCredit.Text;
                }
                else if (rdioselect.SelectedValue == "3")
                {
                    dr1["FailModule"] = Txtcgpa.Text;
                }
                dt.Rows.Add(dr1);
                ViewState["Row"] = dt;
                ViewState["strid"] = Convert.ToInt32(ViewState["strid"]) + 1;
                lvSemester.DataSource = ViewState["Row"];
                lvSemester.DataBind();
                lvSemester.Visible = true;
            }
        }
        return dt;
    }

    private void ShowDetailsSemester(int STUDENT_PROGRESSION_RULE_SEMID)
    {
        try
        {
            string query1 = string.Empty;
            string query2 = string.Empty;

            query1 = "ACD_STUDENT_PROGRESSION_RULE_SEMESTER SP";
            query2 = "SP.SEMESTERNO_FROM as FromSemesterNo,DBO.FN_DESC('SEMESTER',SEMESTERNO_FROM) as FromSemesterNo,SP.SEMESTERNO_TO as ToSemesterNo,DBO.FN_DESC('SEMESTER',SEMESTERNO_TO) as ToSemester,SP.MAX_FAIL_MODULE as FailModule";
            //    query1 = query1 + "S.SEMESTERNAME as ToSemester,SP.MAX_FAIL_MODULE as FailModule";
            DataSet ds = objCommon.FillDropDown(query1.ToString(), "STUDENT_PROGRESSION_RULE_SEMID", query2.ToString(), "STUDENT_PROGRESSION_RULE_SEMID=" + STUDENT_PROGRESSION_RULE_SEMID.ToString() + "", "");
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                FilldropDown();
                ddlFromSemester.SelectedValue = ds.Tables[0].Rows[0]["FromSemesterNo"].ToString();
                objCommon.FillDropDownList(ddlToSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "SEMESTERNO");
                ddlToSemester.SelectedValue = ds.Tables[0].Rows[0]["ToSemesterNo"].ToString();
                BindProgram();
                txtFailModule.Text = ds.Tables[0].Rows[0]["FailModule"].ToString();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Student_Progression.ShowDetails_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }
    protected void btnEdit1_Click(object sender, ImageClickEventArgs e)
    {
        ddlFromSemester.Enabled = false;
        ddlToSemester.Enabled = false;
        try
        {
            ImageButton btnEdit1 = sender as ImageButton;
            ListViewItem item = btnEdit1.NamingContainer as ListViewItem;
            HiddenField hdFromSemester = btnEdit1.FindControl("hdnFromSemester") as HiddenField;
            HiddenField hdToSemester = btnEdit1.FindControl("hdnToSemester") as HiddenField;
            HiddenField hdFailModule = btnEdit1.FindControl("hdnFailModule") as HiddenField;
            //hfidno.Value = btnEdit1.ToolTip;
            ddlFromSemester.SelectedValue = hdFromSemester.Value;
            objCommon.FillDropDownList(ddlToSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "");
            ddlToSemester.SelectedValue = hdToSemester.Value;
            txtFailModule.Text = hdFailModule.Value;
            hdnRuleSemId.Value = btnEdit1.CommandArgument;
            ViewState["actionsem"] = "edit1";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Student_Progression.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmitRule_Click(object sender, EventArgs e)
    {
        try
        {
            string _validate = validateRule();
            if (_validate != string.Empty)
            {
                objCommon.DisplayMessage(updRuleAllocation, _validate, this.Page);
                return;
            }

            int BatchNo = Convert.ToInt32(ddlIntake.SelectedValue);
            string RuleName = "";
            //foreach (ListItem item in lstbxRule.Items)
            //{
            //    if (item.Selected == true)
            //    {
            //        RuleName += item.Value + ',';
            //    }
            //}
            //if (!string.IsNullOrEmpty(RuleName))
            //{
            //    RuleName = RuleName.Substring(0, RuleName.Length - 1);
            //}
            //else
            //{
            //    RuleName = "0";

            //}

            string RuleId = Convert.ToString(lstbxRule.SelectedValue);
            
            int UserId = Convert.ToInt32(Session["userno"].ToString());
            CustomStatus cs = (CustomStatus)objSPC.AddRuleAllocation(BatchNo, RuleId, UserId);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updRuleAllocation, "Rule Allocation Saved Successfully!", this.Page);
                ClearRule();
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.updRuleAllocation, "Rule Allocation Updated Successfully ", this.Page);
                ClearRule();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Student_Progression.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    private void BINDRULE()
    {
    }
    protected void btnCancelRule_Click(object sender, EventArgs e)
    {
        ClearRule();
        ViewState["action"] = "add";
    }

    private void ClearRule()
    {
        ddlIntake.SelectedIndex = 0;
        lstbxRule.ClearSelection();
    }
    private string validateRule()
    {
        string _validate = string.Empty;
        if (ddlIntake.SelectedValue == "0")
        {
            _validate = "Please Select Intake";
        }
        else if (lstbxRule.SelectedValue == "0")
        {
            _validate = "Please Select Atleast One Rule";
        }

        return _validate;
    }

    //protected void ddlIntake_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    //DataSet ds = objCommon.FillDropDown("ACD_STUDENT_PROGRESSION_RULE_INTAKE_MAPPING", "STUDENT_PROGRESSION_RULEID", "", "BATCHNO=" + ddlIntake.SelectedValue + "", "");
    //    //if (ds != null && ds.Tables[0].Rows.Count > 0)
    //    //{
    //    //    //lstbxRule.ClearSelection();
    //    //    char delimiterChars = ',';
    //    //    string RuleName = ds.Tables[0].Rows[0]["STUDENT_PROGRESSION_RULEID"].ToString();
    //    //    string[] rule = RuleName.Split(delimiterChars);
    //    //    for (int j = 0; j < rule.Length; j++)
    //    //    {
    //    //        for (int i = 0; i < lstbxRule.Items.Count; i++)
    //    //        {
    //    //            if (rule[j].Trim() == lstbxRule.Items[i].Value.Trim())
    //    //            {
    //    //                lstbxRule.Items[i].Selected = true;
    //    //            }
    //    //        }
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    objCommon.FillDropDownList(lstbxRule, "ACD_STUDENT_PROGRESSION_RULE", "STUDENT_PROGRESSION_RULEID", "RULENAME", "", "");
    //    //}
    //}
    protected void BindRule()
    {
        string query1 = string.Empty;
        string query2 = string.Empty;
        string query3 = string.Empty;

        query1 = "RM.BATCHNO,RM.STUDENT_PROGRESSION_RULEID,INTAKE_RULE_MAPPINGID";
        query3 = "BATCHNAME,RULENAME";

        query2 = "ACD_STUDENT_PROGRESSION_RULE_INTAKE_MAPPING RM INNER JOIN ACD_ADMBATCH B ON (B.BATCHNO=RM.BATCHNO)";
        query2 = query2 + "INNER JOIN ACD_STUDENT_PROGRESSION_RULE PR ON(PR.STUDENT_PROGRESSION_RULEID=RM.STUDENT_PROGRESSION_RULEID)";

        DataSet ds = objCommon.FillDropDown(query2.ToString(), query3.ToString(), query1.ToString(), "", "");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvRuleAllocation.DataSource = ds;
            lvRuleAllocation.DataBind();
        }
        else
        {
            lvRuleAllocation.DataSource = null;
            lvRuleAllocation.DataBind();
        }
    }
    private void ShowDetailsRule(int STUDENT_PROGRESSION_RULEID)
    {
        try
        {
            string query1 = string.Empty;
            string query2 = string.Empty;
            string query3 = string.Empty;
            query1 = "INTAKE_RULE_MAPPINGID";
            query3 = "BATCHNO,STUDENT_PROGRESSION_RULEID";

            query2 = "ACD_STUDENT_PROGRESSION_RULE_INTAKE_MAPPING";


            DataSet ds = objCommon.FillDropDown(query2.ToString(), query3.ToString(), query1.ToString(), "INTAKE_RULE_MAPPINGID=" + STUDENT_PROGRESSION_RULEID.ToString() + "", "");
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {

                ddlIntake.SelectedValue = ds.Tables[0].Rows[0]["BATCHNO"].ToString();
                lstbxRule.SelectedValue = ds.Tables[0].Rows[0]["STUDENT_PROGRESSION_RULEID"].ToString();               
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Student_Progression.ShowDetails_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }
    protected void btnEdit_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int STUDENT_PROGRESSION_RULEID = int.Parse(btnEdit.CommandArgument);
            ViewState["STUDENT_PROGRESSION_RULEID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetailsRule(STUDENT_PROGRESSION_RULEID);

            string query = string.Empty;
            string query1 = string.Empty;
            query = "ACD_STUDENT_PROGRESSION_RULE_INTAKE_MAPPING SP";
            query1 = "BATCHNO,STUDENT_PROGRESSION_RULEID,INTAKE_RULE_MAPPINGID";

            DataSet ds = objCommon.FillDropDown(query.ToString(), "ACD_STUDENT_PROGRESSION_RULE_INTAKE_MAPPING", query1.ToString(), "INTAKE_RULE_MAPPINGID=" + STUDENT_PROGRESSION_RULEID.ToString() + "", "");
            ViewState["Row"] = ds.Tables[0];
            
            lvSemester.DataSource = ViewState["Row"];
            lvSemester.DataBind();
            lvSemester.Visible = true;
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Student_Progression.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmitbrid_Click(object sender, EventArgs e)
    {
        try
        {
   
          objSP.rulename = txtRuleName.Text;
          objSP.college_id = Convert.ToInt32(ddlclgbrid.SelectedValue);
          objSP.ua_section = Convert.ToInt32(ddlstudylevelbrid.SelectedValue);
          string[] program;
          if (ddlpgmbrid.SelectedValue == "0")
          {
              program = "0,0,0".Split(',');
          }
          else
          {
              program = ddlpgmbrid.SelectedValue.Split(',');
          }
          objSP.degreeno = Convert.ToInt32(program[0]);
          objSP.branchno = Convert.ToInt32(program[1]);
          objSP.affiliated_no = Convert.ToInt32(program[2]);        
          objSP.userid = Convert.ToInt32(Session["userno"].ToString());
          string[] bridprogram;
          if (ddlbridgingpgm.SelectedValue == "0")
          {
              bridprogram = "0,0,0".Split(',');
          }
          else
          {
              bridprogram = ddlbridgingpgm.SelectedValue.Split(',');
          }
          int STUDENT_PROGRESSION_RULEID;
          if (ViewState["action"].ToString() == "add")
          {
              STUDENT_PROGRESSION_RULEID = 0;
              objSP.student_progression_ruleid = STUDENT_PROGRESSION_RULEID;
          }
          else
          {
              STUDENT_PROGRESSION_RULEID = Convert.ToInt32(ViewState["ID"]);
              objSP.student_progression_ruleid = Convert.ToInt32(ViewState["ID"]);
          }
          CustomStatus cs = (CustomStatus)objSPC.AddStudentBridgingProgression(objSP, Convert.ToInt32(TxtModules.Text), Convert.ToInt32(ddlfromsembrid.SelectedValue), Convert.ToInt32(ddltosembrid.SelectedValue), Convert.ToInt32(bridprogram[0]), Convert.ToInt32(bridprogram[1]), Convert.ToInt32(bridprogram[2]), 2);
          if (cs.Equals(CustomStatus.RecordSaved))
          {
              objCommon.DisplayMessage(this.updbridging, "Student Progression Saved Successfully!", this.Page);
              binddata();
              ClearBridge();
          }
          else if (cs.Equals(CustomStatus.RecordUpdated))
          {
              objCommon.DisplayMessage(this.updbridging, "Student Progression Updated Successfully ", this.Page);
              ViewState["action"] = "add";
              binddata();
              ClearBridge();
          }
          else if (cs.Equals(CustomStatus.DuplicateRecord))
          {
              objCommon.DisplayMessage(this.updbridging, "Record Already Exist", this.Page);
              ClearBridge();
              ViewState["action"] = "add";
              return;
          }         
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Student_Progression.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void binddata()
    {
        string SP_Name2 = "PKG_ACD_GET_BRIDGE_PROGESITION";
        string SP_Parameters2 = ""+ "@P_SEMESTER";
        string Call_Values2 = "" + 0;
        DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (dsStudList.Tables[0].Rows.Count > 0)
        {
            Panel2.Visible = true;
            Lvbridlist.DataSource = dsStudList;
            Lvbridlist.DataBind();
        }
        else
        {
            Panel2.Visible = false;
            Lvbridlist.DataSource = dsStudList;
            Lvbridlist.DataBind();
        }
    }
    private void ClearBridge()
    {
        ddlclgbrid.SelectedIndex = 0;
        ddlstudylevelbrid.SelectedIndex = 0;
        ddlpgmbrid.SelectedIndex = 0;
        ddlfromsembrid.SelectedIndex = 0;
        ddltosembrid.SelectedIndex = 0;
        ddlbridgingpgm.SelectedIndex = 0;
        TxtModules.Text = "";
    }
    protected void btnclearbrid_Click(object sender, EventArgs e)
    {
        ClearBridge();
    }
    protected void lkprogression_Click(object sender, EventArgs e)
    {
        divlkprogression.Attributes.Add("class", "active");
        divlkallocation.Attributes.Remove("class");
        divlkbrid.Attributes.Remove("class");
        divlkallot.Attributes.Remove("class");
        divlkbridresult.Attributes.Remove("class");
        divprogress.Visible = true;
        divallocation.Visible = false;
        divEmoji.Visible = false;
        divAlot.Visible = false;
        divbridresult.Visible = false;

    }
    protected void lkallocation_Click(object sender, EventArgs e)
    {
        divlkallocation.Attributes.Add("class", "active");
        divlkprogression.Attributes.Remove("class");
        divlkbrid.Attributes.Remove("class");
        divlkallot.Attributes.Remove("class");
        divlkallot.Attributes.Remove("class");
        divlkbridresult.Attributes.Remove("class");
        divallocation.Visible = true;
        divprogress.Visible = false;
        divEmoji.Visible = false;
        divAlot.Visible = false;
        divbridresult.Visible = false;

        
    }
    protected void lkbrid_Click(object sender, EventArgs e)
    {
        divlkbrid.Attributes.Add("class", "active");
        divlkallocation.Attributes.Remove("class");
        divlkprogression.Attributes.Remove("class");
        divlkallot.Attributes.Remove("class");
        divlkbridresult.Attributes.Remove("class");
        divprogress.Visible = false;
        divallocation.Visible = false;
        divEmoji.Visible = true;
        divAlot.Visible = false;
        divbridresult.Visible = false;

    }
    protected void ddlclgbrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlstudylevelbrid, "ACD_COLLEGE_DEGREE_BRANCH a inner join ACD_UA_SECTION b on (a.UGPGOT=UA_SECTION)", "DISTINCT(UA_SECTION)", "UA_SECTIONNAME,UA_SECTION", "COLLEGE_ID = " + ddlclgbrid.SelectedValue + "", "");

    }
    protected void ddlstudylevelbrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        string query1 = string.Empty;
        string query2 = string.Empty;
        string query3 = string.Empty;

        query1 = "(D.DEGREENAME + ' - ' + BA.LONGNAME + ' - ' + AU.AFFILIATED_SHORTNAME)";
        query2 = "(CONVERT(NVARCHAR(5),CD.DEGREENO) + ','+ CONVERT(NVARCHAR(5),CD.BRANCHNO) + ',' + CONVERT(NVARCHAR(5),CD.AFFILIATED_NO))";
        query3 = "ACD_COLLEGE_MASTER CM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID)";
        query3 = query3 + "INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO) INNER JOIN ACD_BRANCH BA ON (CD.BRANCHNO = BA.BRANCHNO)";
        query3 = query3 + "INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON (CD.AFFILIATED_NO = AU.AFFILIATED_NO)";

        objCommon.FillDropDownList(ddlpgmbrid, query3.ToString(), query2.ToString(), query1.ToString(), "CD.COLLEGE_ID=" + ddlclgbrid.SelectedValue + "", "");
      
        string query11 = string.Empty;
        string query21 = string.Empty;
        string query31 = string.Empty;

        query11 = "(D.DEGREENAME + ' - ' + BA.LONGNAME + ' - ' + AU.AFFILIATED_SHORTNAME)";
        query21 = "(CONVERT(NVARCHAR(5),CD.DEGREENO) + ','+ CONVERT(NVARCHAR(5),CD.BRANCHNO) + ',' + CONVERT(NVARCHAR(5),CD.AFFILIATED_NO))";
        query31 = "ACD_COLLEGE_MASTER CM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID)";
        query31 = query31 + "INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO) INNER JOIN ACD_BRANCH BA ON (CD.BRANCHNO = BA.BRANCHNO)";
        query31 = query31 + "INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON (CD.AFFILIATED_NO = AU.AFFILIATED_NO) INNER JOIN ACD_SCHEME SM ON SM.BRANCHNO=CD.BRANCHNO AND SM.DEGREENO=CD.DEGREENO  AND SM.COLLEGE_ID=CD.COLLEGE_ID";

        objCommon.FillDropDownList(ddlbridgingpgm, query31.ToString(), query21.ToString(), query11.ToString(), "BRIDGING=1" + "", "");
       
    }
    protected void btnEditBrid_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["action"] = "edit";
            int id = int.Parse(btnEdit.CommandArgument);
            ViewState["id"] = id;
            btnSubmit.Text = "Update";
            ShowDetailsBrid(id);
        }
        catch (Exception ex)
        {
        }
    }
    private void ShowDetailsBrid(int id)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT_PROGRESSION_BRIDGING_RULE", "RULEID,(CONVERT(NVARCHAR(5),DEGREENO) + ','+ CONVERT(NVARCHAR(5),BRANCHNO) + ',' + CONVERT(NVARCHAR(5),AFFILIATED_NO))PGM,(CONVERT(NVARCHAR(5),BRIDGING_DEGREENO) + ','+ CONVERT(NVARCHAR(5),BRIDGING_BRANCHNO) + ',' + CONVERT(NVARCHAR(5),BRIDGING_AFFILIATED_NO))BRIDGPGM,RULEID,MAX_FAIL_MODULE,SEMESTERNO_FROM", "SEMESTERNO_TO,BRIDGING_DEGREENO,BRIDGING_BRANCHNO,BRIDGING_AFFILIATED_NO,UA_SECTION,COLLEGE_ID", "RULEID=" + id, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlclgbrid.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                ddlclgbrid_SelectedIndexChanged(new object(), new EventArgs());
                ddlstudylevelbrid.SelectedValue = ds.Tables[0].Rows[0]["UA_SECTION"].ToString();
                ddlstudylevelbrid_SelectedIndexChanged(new object(), new EventArgs());  
                ddlfromsembrid.SelectedValue = ds.Tables[0].Rows[0]["SEMESTERNO_FROM"].ToString();
                ddltosembrid.SelectedValue = ds.Tables[0].Rows[0]["SEMESTERNO_TO"].ToString();
                TxtModules.Text = ds.Tables[0].Rows[0]["MAX_FAIL_MODULE"].ToString();
                ViewState["ID"] = ds.Tables[0].Rows[0]["RULEID"].ToString();
                ddlpgmbrid.SelectedValue = ds.Tables[0].Rows[0]["PGM"].ToString();
                ddlbridgingpgm.SelectedValue = ds.Tables[0].Rows[0]["BRIDGPGM"].ToString();
                //string program = (ds.Tables[0].Rows[0]["PGM"].ToString());
                //string[] pgm = program.Split('-');
                //for (int j = 0; j < pgm.Length; j++)
                //{
                //    for (int i = 0; i < ddlpgmbrid.Items.Count; i++)
                //    {
                //        if (pgm[j] == ddlpgmbrid.Items[i].Value)
                //        {
                //            ddlpgmbrid.Items[i].Selected = true;
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
    protected void rdioselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdioselect.SelectedValue == "1")
        {
            failmodule.Visible=true;
            Credit.Visible=false;
            cgpa.Visible = false;
        }
        else if (rdioselect.SelectedValue == "2")
        {
            failmodule.Visible = false;
            Credit.Visible = true;
            cgpa.Visible = false;
        }
        else if (rdioselect.SelectedValue == "3")
        {
            failmodule.Visible = false;
            Credit.Visible = false;
            cgpa.Visible = true;
        }
    }
    private void BINDYEAR()
    {
        string SP_Name1 = "PKG_ACD_GET_YEAR";
        string SP_Parameters1 = "@P_SEMSTART,@P_SEMEND";
        string Call_Values1 = "" + Convert.ToInt32(ddlFromSemester.SelectedValue) + "," + Convert.ToInt32(ddlToSemester.SelectedValue) + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);
        if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
        {
            divyear.Visible = true;
            chkyear.Items.Clear();
            chkyear.DataSource = ds.Tables[0];
            chkyear.DataValueField = ds.Tables[0].Columns[0].ToString();
            chkyear.DataTextField = ds.Tables[0].Columns[1].ToString();
            chkyear.DataBind();

        }
    }
    protected void ddlToSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        BINDYEAR();
    }
    protected void ddlpgmbrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem itemToRemove = ddlpgmbrid.Items.FindByValue(ddlpgmbrid.SelectedValue);
        ddlbridgingpgm.Items.Remove(itemToRemove);
    }
    protected void lkalot_Click(object sender, EventArgs e)
    {
        divlkbrid.Attributes.Remove("class");
        divlkallocation.Attributes.Remove("class");
        divlkprogression.Attributes.Remove("class");
        divlkallot.Attributes.Add("class", "active");
        divlkbridresult.Attributes.Remove("class");

        divprogress.Visible = false;
        divallocation.Visible = false;
        divEmoji.Visible = false;
        divAlot.Visible = true;
        divbridresult.Visible = false;
    }
    protected void lkbridresult_Click(object sender, EventArgs e)
    {
        divlkbrid.Attributes.Remove("class");
        divlkallocation.Attributes.Remove("class");
        divlkprogression.Attributes.Remove("class");
        divlkallot.Attributes.Remove("class");
        divlkbridresult.Attributes.Add("class", "active");

        divprogress.Visible = false;
        divallocation.Visible = false;
        divEmoji.Visible = false;
        divAlot.Visible = false;
        divbridresult.Visible = true ;
    }
    private void BindList()
    {
        string[] program;
        if (ddlpgm.SelectedValue == "0")
        {
            program = "0,0,0".Split(',');
        }
        else
        {
            program = ddlpgm.SelectedValue.Split(',');
        }

        string[] Bridprogram;
        if (ddlChangeProgram.SelectedValue == "0")
        {
            Bridprogram = "0,0,0".Split(',');
        }
        else
        {
            Bridprogram = ddlChangeProgram.SelectedValue.Split(',');
        }
        string SP_Name1 = "PKG_ACD_GET_BRIDGING_SEM_ALLOT";
        string SP_Parameters1 = "@P_ADMBATCH,@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO,@P_AFFILIATED_NO,@P_BIDGDEGREENO,@P_BIDGBRANCHNO,@P_BIDGAFFILIATED_NO,@P_SEMESTERNO";
        string Call_Values1 = "" + Convert.ToInt32(ddlintakeAlot.SelectedValue) + "," + Convert.ToInt32(ddlfac.SelectedValue) + "," + Convert.ToInt32(program[0]) + "," + Convert.ToInt32(program[1]) + "," + Convert.ToInt32(program[2]) + "," +
           Convert.ToInt32(Bridprogram[0]) + "," + Convert.ToInt32(Bridprogram[1]) + "," + Convert.ToInt32(Bridprogram[2]) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);
        if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
        {
            btnSubmitAlot.Visible = true;
            Panel3.Visible = true;
            LvAllot.DataSource = ds;
            LvAllot.DataBind();
        }
        else
        {
            btnSubmitAlot.Visible = false;
            objCommon.DisplayMessage(this.updalot, "No Recod Found!", this.Page);
            Panel3.Visible = false;
            LvAllot.DataSource = null;
            LvAllot.DataBind();
        }
        foreach (ListViewDataItem lvitem in LvAllot.Items)
        {
            Label lblStatus = lvitem.FindControl("lblStatus") as Label;
            CheckBox chktransfer = lvitem.FindControl("chktransfer") as CheckBox;
            if (lblStatus.Text != "ELIGIBLE")
            {
                chktransfer.Enabled = false;
            }
            else
            {
                chktransfer.Enabled = true;
            }
        }

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindList();
    }
    protected void btnCancelAlot_Click(object sender, EventArgs e)
    {
        ddlfac.SelectedIndex = 0;
        ddlintakeAlot.SelectedIndex = 0;
        ddlpgm.SelectedIndex = 0;
        ddlsemester.SelectedIndex = 0;
        btnSubmitAlot.Visible = false;
        LvAllot.DataSource = null;
        LvAllot.DataBind();
    }
    protected void ddlfac_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        string query1 = string.Empty;
        string query2 = string.Empty;
        string query3 = string.Empty;

        query1 = "(D.DEGREENAME + ' - ' + BA.LONGNAME + ' - ' + AU.AFFILIATED_SHORTNAME)";
        query2 = "DISTINCT(CONVERT(NVARCHAR(5),CD.DEGREENO) + ','+ CONVERT(NVARCHAR(5),CD.BRANCHNO) + ',' + CONVERT(NVARCHAR(5),CD.AFFILIATED_NO))";
        query3 = "ACD_COLLEGE_MASTER CM INNER JOIN ACD_STUDENT_PROGRESSION_BRIDGING_RULE CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID)";
        query3 = query3 + "INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO) INNER JOIN ACD_BRANCH BA ON (CD.BRANCHNO = BA.BRANCHNO)";
        query3 = query3 + "INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON (CD.AFFILIATED_NO = AU.AFFILIATED_NO)";

        objCommon.FillDropDownList(ddlpgm, query3.ToString(), query2.ToString(), query1.ToString(), "CD.COLLEGE_ID=" + ddlfac.SelectedValue + "", "");      
        Panel3.Visible = false;
        LvAllot.DataSource = null;
        LvAllot.DataBind();
    }
    protected void btnSubmitAlot_Click(object sender, EventArgs e)
    {
        try
        {
            int ua_no = Convert.ToInt32(Session["userno"]);
            CustomStatus cs = 0;
            int count = 0; string idno = "", regno = "", name = "";

            foreach (ListViewDataItem dataitem in LvAllot.Items)
            {
                              
                CheckBox chkBox = dataitem.FindControl("chktransfer") as CheckBox;
                Label lblenroll = dataitem.FindControl("lblenroll") as Label;
                Label lblname = dataitem.FindControl("lblname") as Label;
                HiddenField hdfSemester = dataitem.FindControl("hdfSemester") as HiddenField;
                if (chkBox.Checked == true && chkBox.Enabled==true)
                {
                    count++;
                    idno += Convert.ToString(chkBox.ToolTip)+',';
                    regno += Convert.ToString(lblenroll.Text) + ',';
                    name += Convert.ToString(lblname.Text) + ',';
                    
                }
            }
            idno = idno.TrimEnd(',');
            name = name.TrimEnd(',');
            regno = regno.TrimEnd(',');
            if (count == 0)
            {
                objCommon.DisplayMessage(this.updalot, "Please Select At least One checkbox", this.Page);
            }
            

           string[] program;
            if (ddlChangeProgram.SelectedValue == "0")
            {
                program = "0,0".Split(',');
            }
            else
            {
                program = ddlChangeProgram.SelectedValue.Split(',');
            }
            cs = (CustomStatus)objSPC.AddStudentBridgingAllot(idno, ua_no, ViewState["ipAddress"].ToString(), name, regno, Convert.ToInt32(ddlsemester.SelectedValue));
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updalot, "Record saved successfully.", this.Page);
                btnSubmit.Visible = false;
                BindList();
            }
            else
            {
                objCommon.DisplayMessage(this.updalot, "Error", this.Page);
            }

        noresult:
            { }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Offered_Course.btnAd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnShowResult_Click(object sender, EventArgs e)
    {
        BINDresult();
    }
    private void CLEARrESULT()
    {
        ddlsessionresult.SelectedIndex = 0;
        ddlfacresult.SelectedIndex = 0;
        ddlpgmresult.SelectedIndex = 0;
        ddlsemresult.SelectedIndex = 0;
        btnSubmitResult.Visible = false;
        Panel4.Visible = false;
        LvbridResult.DataSource = null;
        LvbridResult.DataBind();
    }
    protected void btnCancelresult_Click(object sender, EventArgs e)
    {
        ddlsessionresult.SelectedIndex = 0;
        ddlfacresult.SelectedIndex=0;
        ddlpgmresult.SelectedIndex=0;
        ddlsemresult.SelectedIndex = 0;
        btnSubmitResult.Visible = false;
        Panel4.Visible = false;
        LvbridResult.DataSource = null;
        LvbridResult.DataBind();
    }
    private void BINDresult()
    {
        string[] program;
        if (ddlpgmresult.SelectedValue == "0")
        {
            program = "0,0".Split(',');
        }
        else
        {
            program = ddlpgmresult.SelectedValue.Split(',');
        }
        string SP_Name1 = "PKG_ACD_GET_BRIDGING_RESULT";
        string SP_Parameters1 = "@P_SESSIONNO,@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO";
        string Call_Values1 = "" + Convert.ToInt32(ddlsessionresult.SelectedValue) + "," + Convert.ToInt32(ddlfacresult.SelectedValue) + "," + Convert.ToInt32(program[0]) + "," + Convert.ToInt32(program[1]) + "," + Convert.ToInt32(ddlsemresult.SelectedValue) + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);
        if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
        {
            //ddlfacresult_SelectedIndexChanged(new object(), new EventArgs());  
            btnSubmitResult.Visible = true;
            Panel4.Visible = true;
            LvbridResult.DataSource = ds;
            LvbridResult.DataBind();
        }
        else
        {
            objCommon.DisplayMessage(this.updresult, "No Record Found", this.Page);
            btnSubmitResult.Visible = false;
            Panel4.Visible = false;
            LvbridResult.DataSource = null;
            LvbridResult.DataBind();
        }
        //foreach (ListViewDataItem items in LvbridResult.Items)
        //{
        //    CheckBox chk = items.FindControl("chkResultProcess") as CheckBox;
        //    HiddenField hdfstatus = items.FindControl("hdfstatus") as HiddenField;
        //    if (hdfstatus.Value == "PASS")
        //    {
        //        chk.Checked = true;
        //    }
        //    else
        //    {
        //        chk.Checked = false;
        //    }
        //}
       
    }
    protected void ddlfacresult_SelectedIndexChanged(object sender, EventArgs e)
    {
        string query1 = string.Empty;
        string query2 = string.Empty;
        string query3 = string.Empty;
        query1 = "(D.DEGREENAME + ' - ' + BA.LONGNAME + ' - ' + AU.AFFILIATED_SHORTNAME)";
        query2 = "DISTINCT (CONVERT(NVARCHAR(5),CD.DEGREENO) + ','+ CONVERT(NVARCHAR(5),CD.BRANCHNO) + ',' + CONVERT(NVARCHAR(5),CD.AFFILIATED_NO))";
        query3 = "ACD_COLLEGE_MASTER CM INNER JOIN ACD_STUDENT_PROGRESSION_BRIDGING_RULE CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID)";
        query3 = query3 + "INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO) INNER JOIN ACD_BRANCH BA ON (CD.BRANCHNO = BA.BRANCHNO)";
        query3 = query3 + "INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON (CD.AFFILIATED_NO = AU.AFFILIATED_NO)";
        objCommon.FillDropDownList(ddlpgmresult, query3.ToString(), query2.ToString(), query1.ToString(), "CD.COLLEGE_ID=" + ddlfacresult.SelectedValue + "", "");


      
       
    }
    protected void btnSubmitResult_Click(object sender, EventArgs e)
    {

        string[] program;
        if (ddltranpgmresults.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this, "Please Select Transfer Program!", this.Page);
            return;
        }
        else
        {
            program = ddltranpgmresults.SelectedValue.Split(',');
        }
        string[] oldprogram;
        if (ddlpgmresult.SelectedValue == "0")
        {
            oldprogram = "0,0".Split(',');
        }
        else
        {
            oldprogram = ddlpgmresult.SelectedValue.Split(',');
        }


        ResultProcessing result = new ResultProcessing();
        string IDNO = string.Empty, name = "", regno = "";
        int Session = Convert.ToInt32(ddlsessionresult.SelectedValue);
        int CollegeId = Convert.ToInt32(ddlfacresult.SelectedValue);
        int Semester = Convert.ToInt32(ddlsemresult.SelectedValue);
        foreach (ListViewDataItem dataitem in LvbridResult.Items)
        {
            CheckBox chkResultProcess = dataitem.FindControl("chkResultProcess") as CheckBox;
            HiddenField hdfidno = dataitem.FindControl("hdfidno") as HiddenField;
            HiddenField hdfname = dataitem.FindControl("hdfname") as HiddenField;
            Label lblDegreeNo = dataitem.FindControl("lblDegreeNo") as Label;
            Label lblregno = dataitem.FindControl("lblregno") as Label;
            Label lblBranchNo = dataitem.FindControl("lblBranchNo") as Label;
            Label lblSchemeNo = dataitem.FindControl("lblSchemeNo") as Label;
            Label lblbAffiNo = dataitem.FindControl("lblbAffiNo") as Label;
            if (chkResultProcess.Checked == true && chkResultProcess.Enabled == true)
            {
                IDNO += hdfidno.Value + ',';
                name += hdfname.Value + ',';
                regno += lblregno.Text + ',';
            }
        }
        IDNO = IDNO.ToString().Trim(',');
        name = name.ToString().Trim(',');
        regno = regno.ToString().Trim(',');
        if (IDNO == "")
        {
            objCommon.DisplayMessage(this, "Please Select At List One Checkbox!", this.Page);
            return;
        }

        CustomStatus cs = (CustomStatus)objSPC.AddStudentBridgingPgmTransfer(IDNO, Convert.ToInt32(ViewState["UANO"]), Convert.ToString(ViewState["ipAddress"]), name, regno, Convert.ToInt32(oldprogram[0]), Convert.ToInt32(oldprogram[1]), Convert.ToInt32(program[0]), Convert.ToInt32(program[1]));
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this, "Record Save Successfully!", this.Page);
            CLEARrESULT();
        }
        else if (cs.Equals(CustomStatus.TransactionFailed))
        {
            objCommon.DisplayMessage(this, "Transaction Failed", this.Page);
        }
    }

    protected void ddlpgm_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] program;
        if (ddlpgm.SelectedValue == "0")
        {
            program = "0,0,0".Split(',');
        }
        else
        {
            program = ddlpgm.SelectedValue.Split(',');
        }
        string query11 = string.Empty;
        string query21 = string.Empty;
        string query31 = string.Empty;



        query11 = "(D.DEGREENAME + ' - ' + BA.LONGNAME + ' - ' + AU.AFFILIATED_SHORTNAME)";
        query21 = "DISTINCT(CONVERT(NVARCHAR(5),CD.BRIDGING_DEGREENO) + ','+ CONVERT(NVARCHAR(5),CD.BRIDGING_BRANCHNO) + ',' + CONVERT(NVARCHAR(5),CD.BRIDGING_AFFILIATED_NO))";
        query31 = "ACD_COLLEGE_MASTER CM INNER JOIN ACD_STUDENT_PROGRESSION_BRIDGING_RULE CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID)";
        query31 = query31 + "INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.BRIDGING_DEGREENO) INNER JOIN ACD_BRANCH BA ON (CD.BRIDGING_BRANCHNO = BA.BRANCHNO)";
        query31 = query31 + "INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON (CD.BRIDGING_AFFILIATED_NO = AU.AFFILIATED_NO)";

        objCommon.FillDropDownList(ddlChangeProgram, query31.ToString(), query21.ToString(), query11.ToString(), "CD.COLLEGE_ID=" + ddlfac.SelectedValue + "AND CD.DEGREENO=" + program[0] + "AND CD.BRANCHNO=" + program[1] + "AND CD.AFFILIATED_NO=" + program[2], "");
    }
    protected void ddlpgmresult_SelectedIndexChanged(object sender, EventArgs e)
    {
        string query11 = string.Empty;
        string query21 = string.Empty;
        string query31 = string.Empty;

        string[] program;
        if (ddlpgmresult.SelectedValue == "0")
        {
            program = "0,0,0".Split(',');
        }
        else
        {
            program = ddlpgmresult.SelectedValue.Split(',');
        }

        query11 = "(D.DEGREENAME + ' - ' + BA.LONGNAME + ' - ' + AU.AFFILIATED_SHORTNAME)";
        query21 = "DISTINCT(CONVERT(NVARCHAR(5),CD.BRIDGING_DEGREENO) + ','+ CONVERT(NVARCHAR(5),CD.BRIDGING_BRANCHNO) + ',' + CONVERT(NVARCHAR(5),CD.BRIDGING_AFFILIATED_NO))";
        query31 = "ACD_COLLEGE_MASTER CM INNER JOIN ACD_STUDENT_PROGRESSION_BRIDGING_RULE CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID)";
        query31 = query31 + "INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.BRIDGING_DEGREENO) INNER JOIN ACD_BRANCH BA ON (CD.BRIDGING_BRANCHNO = BA.BRANCHNO)";
        query31 = query31 + "INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON (CD.BRIDGING_AFFILIATED_NO = AU.AFFILIATED_NO)";

        objCommon.FillDropDownList(ddltranpgmresults, query31.ToString(), query21.ToString(), query11.ToString(), "CD.COLLEGE_ID=" + ddlfacresult.SelectedValue + "AND CD.DEGREENO=" + program[0] + "AND CD.BRANCHNO=" + program[1] + "AND CD.AFFILIATED_NO=" + program[2], "");

        
    }
}

