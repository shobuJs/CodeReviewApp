
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ACADEMIC_BulkSemesterPromotion : System.Web.UI.Page
{
    private Common objCommon = new Common();
    private UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    Config objConfig = new Config();
    string Branchnoss = string.Empty;
    string Branchnoo = string.Empty;
    string Schemenoss = string.Empty;
    string Schemenoo = string.Empty;
    private string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    //USED FOR INITIALSING THE MASTER PAGE
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    //USED FOR BYDEFAULT LOADING THE default PAGE
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
                this.PopulateDropDownList();
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header

            }
            btnSave.Visible = false;
        }
        //Funhid();
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
    }


    //bind SEMESTER NAME, COLLEGE NAME, SESSION_PNAME  in drop down list.
    private void PopulateDropDownList()
    {
        try
        {  // Semester Method parameter updated by pratima patel on 12 dec 2021
            //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "SEMESTERNO");
            DataSet ds = new DataSet();
            ddlSemester.Items.Clear();
            ds = objCommon.FillDropDown("ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "SEMESTERNO");
            ddlSemester.Items.Add("Please Select");
            ddlSemester.SelectedItem.Value = "-1";

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSemester.DataSource = ds;
                ddlSemester.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlSemester.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlSemester.DataBind();
            }
            
            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_ID");
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0", "SESSIONNO desc");
            ddlSession.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page.
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=teacherallotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=teacherallotment.aspx");
        }
    }

    //reload current page
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    //Save button is used for to select Bulk student for promoting the next semester
    // Branch no  parameter updated by pratima patel on 12 dec 2021
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            string[] program;
            string status = "";
            if (ddlBranch.SelectedValue == "0")
            {
                program = "0,0".Split(',');
            }
            else
            {
                program = ddlBranch.SelectedValue.Split(',');
            }
            StudentController objSC = new StudentController();
            Student objS = new Student();
            objS.SectionNo = 0;
            bool flag = false;
            objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            objS.Uano = Convert.ToInt32(Session["userno"]);
            objS.CollegeCode = Session["colcode"].ToString();
            objS.Dob = DateTime.Now;
            int StudyLevel = Convert.ToInt32(ddlSubjectLevel.SelectedValue);
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            Session["Branchnos"] = program[1];
            objS.BranchNo = Convert.ToInt32(Session["Branchnos"]);
            string IdNos = "";
            string ipAddress = Request.ServerVariables["REMOTE_HOST"];
            int count = 0, idnocount = 0, alcount = 0, rulecount = 0;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                Label lblregno = item.FindControl("lblregno") as Label;
                Label lblstudname = item.FindControl("lblstudname") as Label;
                Label lblstatus = item.FindControl("lblStatus") as Label;
                CheckBox chksub = item.FindControl("chkRegister") as CheckBox;
                Label lblalverified = item.FindControl("lblalverified") as Label;
                if (chksub.Checked == true && chksub.Enabled == true)
                {
                    count++;
                    if (ddlStatus.SelectedValue == "1")
                    {

                        int alverified = Convert.ToInt32(objCommon.LookUp("ACD_UPLOAD_DOCUMENT", "COUNT(*)", "DOCNO=1 AND DOC_STATUS=1 AND IDNO=" + Convert.ToInt32(lblregno.ToolTip)));
                        flag = true;
                        if (alverified == 0)
                        {
                            alcount++;
                            objCommon.DisplayMessage(this.upddetails, "Can Not Promoted Those Student  Whose A/L Status Not Verified.", this.Page);
                        }
                        else if (lblstatus.Text == "Rule not defined")
                        {
                            rulecount++;
                            objCommon.DisplayMessage(this.upddetails, "Can Not Promoted Those Student Whose Rule Not Define.", this.Page);
                        }
                        else
                        {
                            IdNos += lblregno.ToolTip + ',';
                            idnocount++;
                            if (lblstatus.Text == "ELIGIBLE")
                            {
                                status += "1" + ',';
                            }
                            else if (lblstatus.Text == "NOT ELIGIBLE")
                            {
                                status += "0" + ',';
                            }
                        }
                    }
                    else
                    {
                        IdNos += lblregno.ToolTip + ',';
                        idnocount++;
                        if (lblstatus.Text == "ELIGIBLE")
                        {
                            status += "1" + ',';
                        }
                        else if (lblstatus.Text == "NOT ELIGIBLE")
                        {
                            status += "0" + ',';
                        }
                    }

                }
            }
            IdNos = IdNos.TrimEnd(',');
            if (count == 0)
            {
                objCommon.DisplayMessage(this.upddetails, "Please select atleast single student", this.Page);
                return;
            }
            else
            {

                CustomStatus cs = (CustomStatus)objSC.bulkStudentSemPromo(objS, IdNos, sessionno, ipAddress, Convert.ToString(status), Convert.ToInt32(ddlStatus.SelectedValue));
                objCommon.DisplayMessage(this.upddetails, "Record Saved successfully", this.Page);
                btnShow_Click(sender, e);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_teacherallotment.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //On select of Degree bind branch name in drop down list
    //protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        btnSave.Visible = false;
    //        lvStudent.DataSource = null;
    //        lvStudent.DataBind();
    //        if (ddlDegree.SelectedIndex > 0)
    //        {
    //            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " and B.College_id=" + ddlColg.SelectedValue, "A.LONGNAME");
    //        }
    //        else
    //        {
    //            ddlBranch.Items.Clear();
    //            ddlDegree.SelectedIndex = 0;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //        {
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //        }
    //    }
    //}

    //On select of Branch bind Scheme name in drop down list
    //protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        btnSave.Visible = false;
    //        lvStudent.DataSource = null;
    //        lvStudent.DataBind();
    //        if (ddlBranch.SelectedIndex > 0)
    //        {
    //            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");
    //            ddlScheme.Focus();
    //        }
    //        else
    //        {
    //            ddlScheme.Items.Clear();
    //            ddlBranch.SelectedIndex = 0;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //        {
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //        }
    //    }
    //}


    // Method parameter updated by pratima patel on 12 dec 2021
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlDegree.SelectedIndex > 0)
            //{
            //    DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH  ACD INNER JOIN ACD_BRANCH B ON(ACD.BRANCHNO=B.BRANCHNO) ", "DISTINCT ACD.BRANCHNO", "B.LONGNAME", "DEGREENO>0 AND B.BRANCHNO<>0 AND DEGREENO=" + ddlDegree.SelectedValue, "");
            //    ddlBranch1.Items.Clear();
            //    //ddlBranch1.Items.Add("Please Select");
            //   // ddlSection.SelectedValue = "0";
            //    //  ddlYear.SelectedValue = "0";

            //    // txtPrefix.Text = String.Empty;
            //    //  txtStartRange.Text = String.Empty;
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        ddlBranch1.DataSource = ds;
            //        ddlBranch1.DataValueField = ds.Tables[0].Columns[0].ToString();
            //        ddlBranch1.DataTextField = ds.Tables[0].Columns[1].ToString();
            //        ddlBranch1.DataBind();
            //    }

            //}
            //else
            //{
            //    ddlBranch1.Items.Clear();
            //    ddlDegree.SelectedIndex = 0;
            //}

            //lvStudent.DataSource = null;
            //lvStudent.DataBind();

        }
        catch
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "ACADEMIC_RollNoAllotment.lvStudents_ItemDataBound-> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private string GetScheme()    // Method created by pratima patel on 15 dec 2021
    {
        ViewState["Schemenos"] = '0';
        ViewState["SchemenosReport"] = '0';
        string SchemeNos = "";
        string SchemeNoRep = "";
        string SchemeDoll = "";
        foreach (ListItem item in ddlScheme.Items)
        {
            if (item.Selected == true)
            {
                SchemeNos += item.Value + ',';
                SchemeNoRep += item.Value + '$';
                SchemeDoll += item.Value + '$';

            }

        }
        if (!string.IsNullOrEmpty(SchemeNos))
        {
            objConfig.Details = SchemeNos.Substring(0, SchemeNos.Length - 1);
            ViewState["Schemenos"] = SchemeNos.Substring(0, SchemeNos.Length - 1);
            ViewState["SchemenoRep"] = SchemeNoRep.Substring(0, SchemeNoRep.Length - 1);
            Schemenoss = SchemeDoll.Substring(0, SchemeNos.Length - 1);
            ViewState["SchemenosReport"] = SchemeDoll.Substring(0, SchemeDoll.Length - 1);
        }
        return SchemeNos;
    }
    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnSave.Visible = false;
            Panel2.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            objCommon.FillListBox(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=S.BRANCHNO)", "DISTINCT CONCAT(S.DEGREENO,',',S.BRANCHNO)AS ID", "(D.DEGREENAME +'-'+ B.LONGNAME)AS DEGBRANCH", "S.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + "", "ID");
            // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C, ACD_COLLEGE_DEGREE_BRANCH CD", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND CD.DEGREENO=D.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlColg.SelectedValue, "DEGREENO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        lbltotal.Visible = true;
        txtTotStud.Visible = true;

        if (ddlSemester.SelectedValue == "")
        {
            objCommon.DisplayMessage(this.upddetails, "Please Select Semester", this.Page);
            return;
        }

        string[] program;
        if (ddlBranch.SelectedValue == "0")
        {
            program = "0,0".Split(',');
        }
        else
        {
            program = ddlBranch.SelectedValue.Split(',');
        }

        string degreeno = "";
        string branchno = "";
        string program1 = "";
        string[] pgm = new string[] { };
        foreach (ListItem item in ddlBranch.Items)
        {
            if (item.Selected == true)
            {
                program1 += item.Value + ',';
            }

        }
        if (!string.IsNullOrEmpty(program1))
        {
            pgm = program1.Split(',');
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

        decimal semester = Convert.ToDecimal(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION", "DEGREENO='" + program[0] + "' AND BRANCHNO IN (" + branchno + ")"));
        decimal semcheck = semester * 2;
        //if (Convert.ToInt32(ddlSemester.SelectedValue) >= semcheck)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:functionConfirm(); ", true);
        //    ddlSemester.SelectedIndex = 0;
        //    lvStudent.DataSource = null;
        //    lvStudent.DataBind();
        //}
        //else
        //{

            DataSet dsshow = objSC.GetSemesterPromotion(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue), Convert.ToString(degreeno), Convert.ToString(branchno), Convert.ToString(ViewState["Schemenos"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue));
            if (dsshow.Tables[0].Rows.Count != 0 && dsshow.Tables[0] != null)
            {
                Panel2.Visible = true;
                lvStudent.DataSource = dsshow;
                lvStudent.DataBind();
                btnSave.Visible = true;
                btnDemoted.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(this.upddetails, "Record Not Found!!!!", this.Page);
                btnSave.Visible = false;
                btnDemoted.Visible = false;
                Panel2.Visible = false;
                lvStudent.DataSource = null;
                lvStudent.DataBind();
            }
    }

    //select of Semester name Student display details  will be null.
    //protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    btnSave.Visible = false;
    //    lvStudent.DataSource = null;
    //    lvStudent.DataBind();
    //}

    protected void btnPromotedSem_Click(object sender, EventArgs e)
    {
        ShowReport_PromotedSem("BULK STUDENT SEMESTER PROMOTION", "PROMOT_STUDENT.rpt");
    }

    private void ShowReport_PromotedSem(string reportTitle, string rptFileName)
    {
        try
        {

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
            // string url = "https://erptest.sliit.lk/Reports/CommonReport.aspx?";
            //string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
            //string url = "http://localhost:59566/PresentationLayer/Reports/CommonReport.aspx?";
            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
            string degreeno = "";
            string branchno = "";
            string program1 = "";
            string[] pgm = new string[] { };
            foreach (ListItem item in ddlBranch.Items)
            {
                if (item.Selected == true)
                {
                    program1 += item.Value + ',';
                }

            }
            program1 = program1.TrimEnd(',');
            if (!string.IsNullOrEmpty(program1))
            {
                pgm = program1.Split(',');
                for (int i = 0; i < pgm.Length; i += 2)
                {
                    degreeno += pgm[i] + "$";

                }
                for (int j = 1; j < pgm.Length; j += 2)
                {
                    branchno += pgm[j] + "$";
                }
                degreeno = degreeno.TrimEnd('$');
                branchno = branchno.TrimEnd('$');
            }
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGE_ID=" + ddlColg.SelectedValue + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_SCHEMENO=" + Convert.ToString(ViewState["SchemenoRep"]) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.upddetails, this.upddetails.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BulkSemesterPromotion.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnNotPromotedSem_Click(object sender, EventArgs e)
    {
        ShowReport_NotPromotedSem("BULK STUDENT SEMESTER PROMOTION", "NOT PROMOT_STUDENT.rpt");
    }

    private string GetBranch()      // Modified By Vinay Mishra on 21/12/2023 to Bind Programme Based Curriculum
    {
        ViewState["Branchnos"] = '0';
        string BranchNos = "";
        string BranchDoll = "";
        string BranchNosS = "";
        string UniqueValues = "";

        foreach (ListItem item in ddlBranch.Items)
        {
            if (item.Selected == true)
            {
                string[] parts = item.Value.Split(',');
                string secondPart = parts[1];

                if (!UniqueValues.Contains(secondPart))
                {
                    BranchNosS += secondPart + ',';
                    UniqueValues += secondPart + ',';
                }
                BranchNos += item.Value + ',';
                BranchDoll += item.Value + '$';
            }
        }
        if (!string.IsNullOrEmpty(BranchNos) || !string.IsNullOrEmpty(BranchNosS))
        {
            objConfig.Details = BranchNos.Substring(0, BranchNos.Length - 1);
            ViewState["Branchnos"] = BranchNos.Substring(0, BranchNos.Length - 1);
            //string b = BranchNosS.Substring(0, BranchNosS.Length - 1);
            ViewState["BranchnosS"] = BranchNosS.Substring(0, BranchNosS.Length - 1);
            Branchnoss = BranchDoll.Substring(0, BranchNos.Length - 1);
            ViewState["BranchnosReport"] = BranchDoll.Substring(0, BranchNos.Length - 1);
        }
        return BranchNos;
    }

    private void ShowReport_NotPromotedSem(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
            // string url = "https://erptest.sliit.lk/Reports/CommonReport.aspx?";
            //string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
            //string url = "http://localhost:59566/PresentationLayer/Reports/CommonReport.aspx?";
            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
            string degreeno = "";
            string branchno = "";
            string program1 = "";
            string[] pgm = new string[] { };
            foreach (ListItem item in ddlBranch.Items)
            {
                if (item.Selected == true)
                {
                    program1 += item.Value + ',';
                }

            }
            program1 = program1.TrimEnd(',');
            if (!string.IsNullOrEmpty(program1))
            {
                pgm = program1.Split(',');
                for (int i = 0; i < pgm.Length; i += 2)
                {
                    degreeno += pgm[i] + "$";

                }
                for (int j = 1; j < pgm.Length; j += 2)
                {
                    branchno += pgm[j] + "$";
                }
                degreeno = degreeno.TrimEnd('$');
                branchno = branchno.TrimEnd('$');
            }
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGE_ID=" + ddlColg.SelectedValue + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_SCHEMENO=" + Convert.ToString(ViewState["SchemenoRep"]) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.upddetails, this.upddetails.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BulkSemesterPromotion.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlBranch1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetBranch();

            if (ddlBranch.SelectedItem != null)
            {
                DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH  ACD INNER JOIN ACD_SCHEME B ON(ACD.BRANCHNO=B.BRANCHNO) ", "DISTINCT B.SCHEMENO", "B.SCHEMENAME", "B.BRANCHNO>0 AND B.SCHEMENO<>0 AND B.BRANCHNO IN (" + Convert.ToString(ViewState["BranchnosS"]) + ")", "SCHEMENO");
                ddlScheme.Items.Clear();
                ddlScheme.Focus();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlScheme.DataSource = ds;
                    ddlScheme.DataValueField = ds.Tables[0].Columns[0].ToString();
                    ddlScheme.DataTextField = ds.Tables[0].Columns[1].ToString();
                    ddlScheme.DataBind();
                }

            }
            else
            {
                ddlScheme.Items.Clear();
                ddlBranch.SelectedIndex = 0;
            }
            Panel2.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();

        }
        catch
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //   objUCommon.ShowError(Page, "ACADEMIC_RollNoAllotment.lvStudents_ItemDataBound-> " + ex.Message + " " + ex.StackTrace);
            //else
            //   objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }


    //protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {          
    //        try
    //        {
    //            if (ddlScheme.SelectedIndex > 0)
    //            {
    //                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");

    //                ddlSemester.Focus();
    //            }
    //            else
    //            {
    //                ddlSemester.Items.Clear();
    //                ddlScheme.SelectedIndex = 0;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            if (Convert.ToBoolean(Session["error"]) == true)
    //                objUCommon.ShowError(Page, "Academic_batchallotment.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
    //            else
    //                objUCommon.ShowError(Page, "Server UnAvailable");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "CourseWise_Registration.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}
    protected void ddlScheme_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            GetScheme();
            //Fill Dropdown semester
            try
            {
                string degreeno = "";
                string[] pgm = new string[] { };
                string semester = "";
                string BranchNos = "";
                foreach (ListItem item in ddlBranch.Items)
                {
                    if (item.Selected == true)
                    {
                        BranchNos += item.Value + ',';

                    }
                }
                BranchNos = BranchNos.TrimEnd(',');
                if (!string.IsNullOrEmpty(BranchNos))
                {
                    pgm = BranchNos.Split(',');
                    for (int i = 0; i < pgm.Length; i += 2)
                    {
                        degreeno += pgm[i] + ",";

                    }
                    degreeno = degreeno.TrimEnd(',');
                }
                //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SC ON (SC.SEMESTERNO = S.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "SC.SEMESTERNAME", "S.COLLEGE_ID=" + ddlColg.SelectedValue + "AND S.SEMESTERNO > 0 AND ISNULL(S.CAN,0)=0 AND ISNULL(S.ADMCAN,0)=0 and s.DEGREENO IN(" + degreeno + ")", "S.SEMESTERNO");
                ddlSemester.Focus();

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Academic_batchallotment.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlSubjectLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel2.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        if (ddlSubjectLevel.SelectedValue != "-1" && ddlSubjectLevel.SelectedValue != "0")
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SC ON (SC.SEMESTERNO = S.SEMESTERNO_HONORS)", "DISTINCT S.SEMESTERNO_HONORS", "SC.SEMESTERNAME", " S.DEGREENO =" + ddlDegree.SelectedValue + "AND S.COLLEGE_ID=" + ddlColg.SelectedValue + "AND S.SEMESTERNO_HONORS > 0 AND ISNULL(S.CAN,0)=0 AND ISNULL(S.ADMCAN,0)=0", "S.SEMESTERNO_HONORS");
        }
        else
        {
            if (ViewState["Schemenos"] != null)
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO IN (" + Convert.ToString(ViewState["Schemenos"]) + ")", "SR.SEMESTERNO");
                // pnlCourse.Visible = false;
                return;
            }
            else
            {
                ddlSemester.SelectedIndex = -1;
            }
        }
        ddlSemester.SelectedIndex = -1;
    }
    protected void btnDemoted_Click(object sender, EventArgs e)
    {
        try
        {

            string[] program;
            string IdNos = "";
            if (ddlBranch.SelectedValue == "0")
            {
                program = "0,0".Split(',');
            }
            else
            {
                program = ddlBranch.SelectedValue.Split(',');
            }
            StudentController objSC = new StudentController();
            Student objS = new Student();
            objS.SectionNo = 0;
            bool flag = false;
            int count = 0;
            objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            objS.Uano = Convert.ToInt32(Session["userno"]);
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                Label lblregno = item.FindControl("lblregno") as Label;
                Label lblstudname = item.FindControl("lblstudname") as Label;
                Label lblstatus = item.FindControl("lblStatus") as Label;
                CheckBox chksub = item.FindControl("chkRegister") as CheckBox;
                CheckBox chkDem = item.FindControl("chkDem") as CheckBox;
                Label lblalverified = item.FindControl("lblalverified") as Label;
                if (chkDem.Checked == true)
                {
                    count++;
                    IdNos += lblregno.ToolTip + ',';
                }
            }

            if (count == 0)
            {
                objCommon.DisplayMessage(this.upddetails, "Please Select At Least one Student", this.Page);
            }
            IdNos = IdNos.TrimEnd(',');
            CustomStatus cs = (CustomStatus)objSC.bulkStudentSemDemotion(objS, IdNos, Convert.ToInt32(Session["userno"]));
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.upddetails, "Record updated successfully", this.Page);
                btnShow_Click(sender, e);
            }
            else
            {
                objCommon.DisplayMessage(this.upddetails, "Error", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_teacherallotment.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSave.Visible = false;
        Panel2.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
    }
}
