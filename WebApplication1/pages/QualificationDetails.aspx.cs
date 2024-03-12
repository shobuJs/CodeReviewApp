using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Linq;

public partial class ACADEMIC_QualificationDetails : System.Web.UI.Page
{
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

                ViewState["usertype"] = Session["usertype"];
                if (ViewState["usertype"].ToString() == "2")
                {
                    divadmissiondetailstreeview.Visible = false;
                    lvQualExm.DataSource = null;
                    lvQualExm.DataBind();
                    FillDropDown();
                    ShowStudentDetails();
                    divhome.Visible = false;
                }
                else
                {
                    divhome.Visible = true;
                    divadmissiondetailstreeview.Visible = true;
                    ddlExamNo.Enabled = true;
                    ddlpgentranceno.Enabled = true;
                    txtpgrollno.Enabled = true;
                    txtQExamRollNo.Enabled = true;

                    if (Convert.ToInt32(Session["stuinfoidno"]) != null)
                    {
                        lvQualExm.DataSource = null;
                        lvQualExm.DataBind();
                        ViewState["action"] = "edit";
                        FillDropDown();
                        ShowStudentDetails();
                    }

                }

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
                Response.Redirect("~/notauthorized.aspx?page=QualificationDetails.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=QualificationDetails.aspx");
        }
    }

    private void FillDropDown()
    {
        try
        {

            objCommon.FillDropDownList(ddldegree, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO >0 AND QEXAMSTATUS='Q' and QUALIFYNO not in (1,2)", "QUALIFYNO");
            //fill dropdown adm quota
            objCommon.FillDropDownList(ddlQuota, "ACD_QUOTA", "QUOTANO", "QUOTA", "QUOTANO>0", "QUOTANO");
            objCommon.FillDropDownList(ddlExamNo, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO>0 AND QEXAMSTATUS='Q'", "QUALIEXMNAME");
            objCommon.FillDropDownList(ddlExamNo, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO >0 AND QEXAMSTATUS='E'", "QUALIFYNO");
            objCommon.FillDropDownList(ddlpgentranceno, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO >0 AND QEXAMSTATUS='E'", "QUALIFYNO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentInfoEntry.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        Student objS = new Student();
        StudentPhoto objSPhoto = new StudentPhoto();
        StudentAddress objSAddress = new StudentAddress();
        StudentQualExm objSQualExam = new StudentQualExm();

        try
        {
            if (ViewState["usertype"].ToString() == "2" || ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7" || ViewState["usertype"].ToString() == "5")
            {

                if (ViewState["usertype"].ToString() == "2")
                {
                    objS.IdNo = Convert.ToInt32(Session["idno"]);
                }
                else
                {
                    objS.IdNo = Convert.ToInt32(Session["stuinfoidno"]);
                }
                //SSC 
                if (!txtSchoolCollegeNameSsc.Text.Trim().Equals(string.Empty)) objSQualExam.SchoolCollegeNameSsc = txtSchoolCollegeNameSsc.Text.Trim();
                if (!txtBoardSsc.Text.Trim().Equals(string.Empty)) objSQualExam.BoardSsc = txtBoardSsc.Text.Trim();
                if (!txtYearOfExamSsc.Text.Trim().Equals(string.Empty)) objSQualExam.YearOfExamSsc = txtYearOfExamSsc.Text.Trim();
                if (!txtSSCMedium.Text.Trim().Equals(string.Empty)) objSQualExam.SSC_medium = txtSSCMedium.Text.Trim();
                if (!txtMarksObtainedSsc.Text.Trim().Equals(string.Empty)) objSQualExam.MarksObtainedSsc = Convert.ToInt32(txtMarksObtainedSsc.Text.Trim());
                if (!txtOutOfMarksSsc.Text.Trim().Equals(string.Empty)) objSQualExam.OutOfMarksSsc = Convert.ToInt32(txtOutOfMarksSsc.Text.Trim());
                if (!txtPercentageSsc.Text.Trim().Equals(string.Empty)) objSQualExam.PercentageSsc = Convert.ToDecimal(txtPercentageSsc.Text.Trim());
                if (!txtExamRollNoSsc.Text.Trim().Equals(string.Empty)) objSQualExam.QEXMROLLNOSSC = txtExamRollNoSsc.Text.Trim();
                if (!txtPercentileSsc.Text.Trim().Equals(string.Empty)) objSQualExam.PercentileSsc = Convert.ToDecimal(txtPercentileSsc.Text.Trim());
                if (!txtGradeSsc.Text.Trim().Equals(string.Empty)) objSQualExam.GradeSsc = txtGradeSsc.Text.Trim();
                if (!txtAttemptSsc.Text.Trim().Equals(string.Empty)) objSQualExam.AttemptSsc = txtAttemptSsc.Text.Trim();
                if (!txtSSCSchoolColgAdd.Text.Trim().Equals(string.Empty)) objSQualExam.colg_address_SSC = txtSSCSchoolColgAdd.Text.Trim();

                //HSSC
                if (!txtSchoolCollegeNameHssc.Text.Trim().Equals(string.Empty)) objSQualExam.SCHOOL_COLLEGE_NAME = txtSchoolCollegeNameHssc.Text.Trim();
                if (!txtBoardHssc.Text.Trim().Equals(string.Empty)) objSQualExam.BOARD = txtBoardHssc.Text.Trim();
                if (!txtYearOfExamHssc.Text.Trim().Equals(string.Empty)) objSQualExam.YEAR_OF_EXAMHSSC = txtYearOfExamHssc.Text.Trim();
                if (!txtHSSCMedium.Text.Trim().Equals(string.Empty)) objSQualExam.HSSC_medium = txtHSSCMedium.Text.Trim();
                if (!txtMarksObtainedHssc.Text.Trim().Equals(string.Empty)) objSQualExam.MARKOBTAINED = Convert.ToInt32(txtMarksObtainedHssc.Text.Trim());
                if (!txtOutOfMarksHssc.Text.Trim().Equals(string.Empty)) objSQualExam.OUTOFMARK = Convert.ToInt32(txtOutOfMarksHssc.Text.Trim());
                if (!txtPercentageHssc.Text.Trim().Equals(string.Empty)) objSQualExam.PERCENTAGE = Convert.ToDecimal(txtPercentageHssc.Text.Trim());
                if (!txtExamRollNoHssc.Text.Trim().Equals(string.Empty)) objSQualExam.QEXMROLLNOHSSC = txtExamRollNoHssc.Text.Trim();
                if (!txtPercentileHssc.Text.Trim().Equals(string.Empty)) objSQualExam.PercentileHSsc = Convert.ToDecimal(txtPercentileHssc.Text.Trim());
                if (!txtGradeHssc.Text.Trim().Equals(string.Empty)) objSQualExam.GRADE = txtGradeHssc.Text.Trim();
                if (!txtAttemptHssc.Text.Trim().Equals(string.Empty)) objSQualExam.ATTEMPT = txtAttemptHssc.Text.Trim();
                if (!txtHSCColgAddress.Text.Trim().Equals(string.Empty)) objSQualExam.colg_address_HSSC = txtHSCColgAddress.Text.Trim();

                //Subject Wise Marks

                if (!txtHscChe.Text.Trim().Equals(string.Empty)) objSQualExam.HSCCHE = Convert.ToInt32(txtHscChe.Text.Trim());
                if (!txtHscCheMax.Text.Trim().Equals(string.Empty)) objSQualExam.HSCCHEMAX1 = Convert.ToInt32(txtHscCheMax.Text.Trim());
                if (!txtHscPhyMax.Text.Trim().Equals(string.Empty)) objSQualExam.HSCPHYMAX1 = Convert.ToInt32(txtHscPhyMax.Text.Trim());
                if (!txtHscEngHssc.Text.Trim().Equals(string.Empty)) objSQualExam.ENG = Convert.ToInt32(txtHscEngHssc.Text.Trim());
                if (!txtHscEngMaxHssc.Text.Trim().Equals(string.Empty)) objSQualExam.HSCENGMAX = Convert.ToInt32(txtHscEngMaxHssc.Text.Trim());
                if (!txtHscMaths.Text.Trim().Equals(string.Empty)) objSQualExam.MATHS = Convert.ToInt32(txtHscMaths.Text.Trim());
                if (!txtHscMathsMax.Text.Trim().Equals(string.Empty)) objSQualExam.MATHSMAX = Convert.ToInt32(txtHscMathsMax.Text.Trim());


                objSQualExam.QUALIFYNO = Convert.ToInt32(ddlExamNo.SelectedValue);
                if (!txtQExamRollNo.Text.Trim().Equals(string.Empty)) objS.QexmRollNo = txtQExamRollNo.Text.Trim();
                if (!txtYearOfExam.Text.Trim().Equals(string.Empty)) objS.YearOfExam = txtYearOfExam.Text.Trim();
                if (!txtPer.Text.Trim().Equals(string.Empty)) objS.Percentage = Convert.ToDecimal(txtPer.Text.Trim());
                if (!txtPercentile.Text.Trim().Equals(string.Empty)) objSQualExam.PERCENTILE = Convert.ToDecimal(txtPercentile.Text.Trim());
                if (!txtAllIndiaRank.Text.Trim().Equals(string.Empty)) objSQualExam.ALLINDIARANK = Convert.ToInt32(txtAllIndiaRank.Text.Trim());
                if (!txtScore.Text.Trim().Equals(string.Empty)) objS.Score = Convert.ToDecimal(txtScore.Text.Trim());


                objS.PGQUALIFYNO = Convert.ToInt32(ddlpgentranceno.SelectedValue);
                if (!txtpgrollno.Text.Trim().Equals(string.Empty)) objS.PGENTROLLNO = txtpgrollno.Text.Trim();
                if (!txtpgexamyear.Text.Trim().Equals(string.Empty)) objS.pgyearOfExam = txtpgexamyear.Text.Trim();
                if (!txtpgpercentage.Text.Trim().Equals(string.Empty)) objS.pgpercentage = Convert.ToDecimal(txtpgpercentage.Text.Trim());
                if (!txtpgpercentile.Text.Trim().Equals(string.Empty)) objS.pgpercentile = Convert.ToDecimal(txtpgpercentile.Text.Trim());
                if (!txtpgrank.Text.Trim().Equals(string.Empty)) objS.PGRANK = Convert.ToInt32(txtpgrank.Text.Trim());
                if (!txtpgscore.Text.Trim().Equals(string.Empty)) objS.pgscore = Convert.ToDecimal(txtpgscore.Text.Trim());

                QualifiedExam[] qualExams = null;
                this.BindLastQualifiedExamData(ref qualExams);
                objS.LastQualifiedExams = qualExams;

                CustomStatus cs = (CustomStatus)objSC.UpdateStudentQualifyingExamInformation(objS, objSQualExam, Convert.ToInt32(Session["usertype"]));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ShowStudentDetails();

                    divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('Qualification Details Updated Successfully!!'); </script>";


                    Response.Redirect("~/academic/OtherInformation.aspx", false);
                }
                else
                {
                    objCommon.DisplayMessage(upEditQualExm, "Error Occured While Updating Qualification Details!!", this.Page);
                }


            }
        }
        catch (Exception Ex)
        {
        }





        this.fillDataTable();
        Session["qualifyTbl"] = null;

    }
    protected void btnGohome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/StudentInfoEntryNew.aspx?pageno=2219");
    }
    protected void fillDataTable()
    {
        lvQualExm.DataSource = Session["qualifyTbl"]; ;
        lvQualExm.DataBind();
        ClearControls_QualDetails();

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        if (Session["qualifyTbl"] != null && ((DataTable)Session["qualifyTbl"]).Rows.Count > 0 && ((DataTable)Session["qualifyTbl"]) != null)
        {
            DataTable dt = (DataTable)Session["qualifyTbl"];
            DataRow dr = dt.NewRow();
            if (btnAdd.Text != "Update")
            {
                string expression = string.Empty;
                expression = "QUALIFYNAME='" + ddldegree.SelectedItem.Text + "'";
                DataRow[] dr1 = dt.Select(expression);
                if (dr1.Length > 0)
                {
                    lvQualExm.DataSource = dt;
                    lvQualExm.DataBind();
                    ClearControls_QualDetails();
                    objCommon.DisplayMessage(upEditQualExm, "Data for selected exam already exist!", this.Page);
                    return;
                }
            }

            if (ddldegree.SelectedIndex > 0 && (txtSchoolCollegeNameQualifying.Text != string.Empty || txtSchoolCollegeNameQualifying.Text != "") && (txtYearOfExamQualifying.Text != string.Empty || txtYearOfExamQualifying.Text != "") && (txtPercentageQualifying.Text != string.Empty || txtPercentageQualifying.Text != "") && (txtPercentageQualifying.Text != string.Empty || txtPercentageQualifying.Text != "") && (txtQualExamRollNo.Text != string.Empty || txtQualExamRollNo.Text != "") && (txtBoardQualifying.Text != string.Empty || txtBoardQualifying.Text != ""))
            {
                dr["QUALIFYNO"] = Convert.ToInt32(ddldegree.SelectedValue);
                dr["QUALIFYNAME"] = ddldegree.SelectedItem.Text;
                dr["SCHOOL_COLLEGE_NAME"] = txtSchoolCollegeNameQualifying.Text.Trim() == null ? string.Empty : txtSchoolCollegeNameQualifying.Text.Trim();
                dr["YEAR_OF_EXAMHSSC"] = txtYearOfExamQualifying.Text.Trim() == "" ? string.Empty : txtYearOfExamQualifying.Text.Trim();
                dr["MARKS_OBTAINED"] = txtMarksObtainedQualifying.Text.Trim() == "" ? 0 : Convert.ToInt32(txtMarksObtainedQualifying.Text.Trim());
                dr["OUT_OF_MRKS"] = txtOutOfMarksQualifying.Text.Trim() == "" ? 0 : Convert.ToInt32(txtOutOfMarksQualifying.Text.Trim());
                dr["PER"] = txtPercentageQualifying.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtPercentageQualifying.Text.Trim());
                dr["PERCENTILE"] = txtPercentileQualifying.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtPercentileQualifying.Text.Trim());
                dr["ATTEMPT"] = txtAttemptQualifying.Text.Trim();
                dr["BOARD"] = txtBoardQualifying.Text.Trim();
                dr["GRADE"] = txtGradeQualifying.Text.Trim();
                dr["RES_TOPIC"] = txtResearchTopic.Text.Trim();
                dr["SUPERVISOR_NAME1"] = txtSupervisorName1.Text.Trim();
                dr["SUPERVISOR_NAME2"] = txtSupervisorName2.Text.Trim();

                dr["COLLEGE_ADDRESS"] = txtQualExmAddress.Text.Trim();
                dr["QUAL_MEDIUM"] = txtQualiMedium.Text.Trim();
                dr["QEXMROLLNO"] = txtQualExamRollNo.Text.Trim();
                dt.Rows.Add(dr);
                Session["qualifyTbl"] = dt;
                lvQualExm.DataSource = dt;
                lvQualExm.DataBind();
                ClearControls_QualDetails();
                objCommon.DisplayMessage(upEditQualExm, "Data saved successfully!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(upEditQualExm, " Please enter all below details \\n \\n School/College Name \\n Board Name \\n Qualifying Exam Name \\n Exam Roll No. \\n Year of Exam \\n Percentage", this.Page);
            }
        }
        else
        {
            DataTable dt = this.GetDataTable();
            DataRow dr = dt.NewRow();


            if (ddldegree.SelectedIndex > 0 && (txtSchoolCollegeNameQualifying.Text != string.Empty || txtSchoolCollegeNameQualifying.Text != "") && (txtYearOfExamQualifying.Text != string.Empty || txtYearOfExamQualifying.Text != "") && (txtPercentageQualifying.Text != string.Empty || txtPercentageQualifying.Text != "") && (txtPercentageQualifying.Text != string.Empty || txtPercentageQualifying.Text != "") && (txtQualExamRollNo.Text != string.Empty || txtQualExamRollNo.Text != "") && (txtBoardQualifying.Text != string.Empty || txtBoardQualifying.Text != ""))
            {
                dr["QUALIFYNO"] = Convert.ToInt32(ddldegree.SelectedValue);
                dr["QUALIFYNAME"] = Convert.ToString(ddldegree.SelectedItem.Text);
                dr["SCHOOL_COLLEGE_NAME"] = txtSchoolCollegeNameQualifying.Text.Trim();
                dr["YEAR_OF_EXAMHSSC"] = txtYearOfExamQualifying.Text.Trim();
                dr["MARKS_OBTAINED"] = txtMarksObtainedQualifying.Text.Trim() == "" ? 0 : Convert.ToInt32(txtMarksObtainedQualifying.Text.Trim());
                dr["OUT_OF_MRKS"] = txtOutOfMarksQualifying.Text.Trim() == "" ? 0 : Convert.ToInt32(txtOutOfMarksQualifying.Text.Trim());
                dr["PER"] = txtPercentageQualifying.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtPercentageQualifying.Text.Trim());
                dr["PERCENTILE"] = txtPercentileQualifying.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtPercentileQualifying.Text.Trim());
                dr["ATTEMPT"] = txtAttemptQualifying.Text.Trim();
                dr["BOARD"] = txtBoardQualifying.Text.Trim();
                dr["GRADE"] = txtGradeQualifying.Text.Trim();
                dr["RES_TOPIC"] = txtResearchTopic.Text.Trim();
                dr["SUPERVISOR_NAME1"] = txtSupervisorName1.Text.Trim();
                dr["SUPERVISOR_NAME2"] = txtSupervisorName2.Text.Trim();
                dr["COLLEGE_ADDRESS"] = txtQualExmAddress.Text.Trim();
                dr["QUAL_MEDIUM"] = txtQualiMedium.Text.Trim();
                dr["QEXMROLLNO"] = txtQualExamRollNo.Text.Trim();

                dt.Rows.Add(dr);
                Session["qualifyTbl"] = dt;
                lvQualExm.DataSource = dt;
                lvQualExm.DataBind();
                ClearControls_QualDetails();
                objCommon.DisplayMessage(upEditQualExm, "Data saved successfully!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(upEditQualExm, " Please enter all below details \\n \\n School/College Name \\n Board Name \\n Qualifying Exam Name \\n Exam Roll No. \\n Year of Exam \\n Percentage", this.Page);
            }


        }

        btnAdd.Text = "Add";

    }
    private DataTable GetDataTable()
    {
        DataTable objQualify = new DataTable();
        objQualify.Columns.Add(new DataColumn("QUALIFYNO", typeof(int)));
        objQualify.Columns.Add(new DataColumn("QUALIFYNAME", typeof(string)));
        objQualify.Columns.Add(new DataColumn("SCHOOL_COLLEGE_NAME", typeof(string)));
        objQualify.Columns.Add(new DataColumn("YEAR_OF_EXAMHSSC", typeof(string)));
        objQualify.Columns.Add(new DataColumn("MARKS_OBTAINED", typeof(int)));
        objQualify.Columns.Add(new DataColumn("OUT_OF_MRKS", typeof(int)));
        objQualify.Columns.Add(new DataColumn("PER", typeof(decimal)));
        objQualify.Columns.Add(new DataColumn("PERCENTILE", typeof(decimal)));
        objQualify.Columns.Add(new DataColumn("ATTEMPT", typeof(string)));
        objQualify.Columns.Add(new DataColumn("BOARD", typeof(string)));
        objQualify.Columns.Add(new DataColumn("GRADE", typeof(string)));
        objQualify.Columns.Add(new DataColumn("RES_TOPIC", typeof(string)));
        objQualify.Columns.Add(new DataColumn("SUPERVISOR_NAME1", typeof(string)));
        objQualify.Columns.Add(new DataColumn("SUPERVISOR_NAME2", typeof(string)));
        objQualify.Columns.Add(new DataColumn("COLLEGE_ADDRESS", typeof(string)));
        objQualify.Columns.Add(new DataColumn("QUAL_MEDIUM", typeof(string)));
        objQualify.Columns.Add(new DataColumn("QEXMROLLNO", typeof(string)));
        return objQualify;
    }

    private DataTable GetEntranceDataTable()
    {
        DataTable objEntrance = new DataTable();
        objEntrance.Columns.Add(new DataColumn("QUALIFYNO", typeof(int)));
        objEntrance.Columns.Add(new DataColumn("QUALIFYEXAMNAME", typeof(string)));
        objEntrance.Columns.Add(new DataColumn("ALLINDIARANK", typeof(int)));
        objEntrance.Columns.Add(new DataColumn("YEAROFEXAM", typeof(string)));
        objEntrance.Columns.Add(new DataColumn("STATERANK", typeof(int)));
        objEntrance.Columns.Add(new DataColumn("PERCENTAGE", typeof(decimal)));
        objEntrance.Columns.Add(new DataColumn("QEXMROLLNO", typeof(string)));
        objEntrance.Columns.Add(new DataColumn("PERCENTILE", typeof(decimal)));
        objEntrance.Columns.Add(new DataColumn("QUOTA", typeof(decimal)));
        return objEntrance;
    }

    private void ClearControls_QualDetails()
    {
        ddldegree.SelectedIndex = 0;
        txtYearOfExamQualifying.Text = string.Empty;
        txtMarksObtainedQualifying.Text = string.Empty;
        txtOutOfMarksQualifying.Text = string.Empty;
        txtPercentageQualifying.Text = string.Empty;
        txtAttemptQualifying.Text = string.Empty;
        txtSupervisorName1.Text = string.Empty;
        txtSchoolCollegeNameQualifying.Text = string.Empty;
        txtBoardQualifying.Text = string.Empty;
        txtGradeQualifying.Text = string.Empty;
        txtPercentileQualifying.Text = string.Empty;
        txtResearchTopic.Text = string.Empty;
        txtSupervisorName2.Text = string.Empty;
        txtQExamRollNo.Text = string.Empty;
        txtQualExmAddress.Text = string.Empty;
        txtQualiMedium.Text = string.Empty;
        txtQualExamRollNo.Text = string.Empty;
    }

    private void ClearControls_Entrance()
    {
        ddlExamNo.SelectedIndex = 0;
        txtAllIndiaRank.Text = string.Empty;
        txtYearOfExam.Text = string.Empty;
        txtStateRank.Text = string.Empty;
        txtPer.Text = string.Empty;
        txtQExamRollNo.Text = string.Empty;
        txtPercentile.Text = string.Empty;
    }
    private void BindLastQualifiedExamData(ref QualifiedExam[] qualifiedExams)
    {
        DataTable dt;
        if (Session["qualifyTbl"] != null && ((DataTable)Session["qualifyTbl"]) != null)
        {
            int index = 0;
            dt = (DataTable)Session["qualifyTbl"];
            qualifiedExams = new QualifiedExam[dt.Rows.Count];
            foreach (DataRow dr in dt.Rows)
            {
                QualifiedExam objQualExam = new QualifiedExam();
                if (ViewState["usertype"].ToString() == "2")
                {
                    objQualExam.Idno = Convert.ToInt32(Session["idno"]);
                }
                else
                {
                    objQualExam.Idno = Convert.ToInt32(Session["stuinfoidno"]);
                }

                objQualExam.Qualifyno = Convert.ToInt32(dr["QUALIFYNO"]);
                objQualExam.School_college_name = dr["SCHOOL_COLLEGE_NAME"].ToString();
                objQualExam.Year_of_exam = dr["YEAR_OF_EXAMHSSC"].ToString();

                if (dr["MARKS_OBTAINED"].ToString() == "0" || dr["MARKS_OBTAINED"].ToString() == null || dr["MARKS_OBTAINED"].ToString() == "")
                {
                    objQualExam.MarksObtained = 0;
                }
                else
                {
                    objQualExam.MarksObtained = Convert.ToInt32(dr["MARKS_OBTAINED"]);
                }

                if (dr["OUT_OF_MRKS"].ToString() == "0" || dr["OUT_OF_MRKS"].ToString() == null || dr["OUT_OF_MRKS"].ToString() == "")
                {
                    objQualExam.Out_of_marks = 0;
                }
                else
                {
                    objQualExam.Out_of_marks = Convert.ToInt32(dr["OUT_OF_MRKS"]);
                }

                if (dr["PER"].ToString() == "0" || dr["PER"].ToString() == null || dr["PER"].ToString() == "")
                {
                    objQualExam.Per = 0;
                }
                else
                {
                    objQualExam.Per = Convert.ToDecimal(dr["PER"]);
                }

                objQualExam.Percentile = Convert.ToDecimal(dr["PERCENTILE"]);
                objQualExam.Attempt = dr["ATTEMPT"].ToString();
                objQualExam.Board = dr["BOARD"].ToString();
                objQualExam.Grade = dr["GRADE"].ToString();
                objQualExam.Res_topic = dr["RES_TOPIC"].ToString();
                objQualExam.Supervisor_name1 = dr["SUPERVISOR_NAME1"].ToString();
                objQualExam.Supervisor_name2 = dr["SUPERVISOR_NAME2"].ToString();
                objQualExam.College_code = Session["colcode"].ToString();
                objQualExam.College_address = dr["COLLEGE_ADDRESS"].ToString();
                objQualExam.Qual_medium = dr["QUAL_MEDIUM"].ToString();
                objQualExam.Qexmrollno = dr["QEXMROLLNO"].ToString();
                qualifiedExams[index] = objQualExam;
                index++;
            }
        }
    }

    protected void btnEditQualDetail_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            DataTable dt;
            if (Session["qualifyTbl"] != null && ((DataTable)Session["qualifyTbl"]) != null)
            {
                dt = ((DataTable)Session["qualifyTbl"]);
                DataRow dr = this.GetEditableDataRow(dt, btnEdit.CommandArgument);

                ddldegree.SelectedValue = dr["QUALIFYNO"].ToString();
                txtSchoolCollegeNameQualifying.Text = dr["SCHOOL_COLLEGE_NAME"].ToString();
                txtYearOfExamQualifying.Text = dr["YEAR_OF_EXAMHSSC"].ToString();
                txtMarksObtainedQualifying.Text = dr["MARKS_OBTAINED"].ToString();
                txtOutOfMarksQualifying.Text = dr["OUT_OF_MRKS"].ToString();
                txtPercentageQualifying.Text = dr["PER"].ToString();
                txtPercentileQualifying.Text = dr["PERCENTILE"].ToString();
                txtAttemptQualifying.Text = dr["ATTEMPT"].ToString();
                txtBoardQualifying.Text = dr["BOARD"].ToString();
                txtGradeQualifying.Text = dr["GRADE"].ToString();
                txtResearchTopic.Text = dr["RES_TOPIC"].ToString();
                txtSupervisorName1.Text = dr["SUPERVISOR_NAME1"].ToString();
                txtSupervisorName2.Text = dr["SUPERVISOR_NAME2"].ToString();
                txtQualExmAddress.Text = dr["COLLEGE_ADDRESS"].ToString();
                txtQualiMedium.Text = dr["QUAL_MEDIUM"].ToString();

                txtQualExamRollNo.Text = dr["QEXMROLLNO"].ToString();
                dt.Rows.Remove(dr);
                Session["qualifyTbl"] = dt;
                this.BindListView_DemandDraftDetails(dt);
                btnAdd.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.btnEditQualDetail_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnDeleteQualDetail_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            DataTable dt;
            if (Session["qualifyTbl"] != null && ((DataTable)Session["qualifyTbl"]) != null)
            {
                dt = ((DataTable)Session["qualifyTbl"]);
                dt.Rows.Remove(this.GetEditableDataRow(dt, btnDelete.CommandArgument));
                Session["qualifyTbl"] = dt;
                this.BindListView_DemandDraftDetails(dt);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.btnDeleteQualDetail_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void BindListView_DemandDraftDetails(DataTable dt)
    {
        try
        {
            lvQualExm.DataSource = dt;
            lvQualExm.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.BindListView_DemandDraftDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private DataRow GetEditableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["QUALIFYNO"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.GetEditableDataRow() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dataRow;
    }
    protected void btnEtranceCancel_Click(object sender, EventArgs e)
    {
        this.ClearControls_Entrance();
    }
    protected void btnEntranceSave_Click(object sender, EventArgs e)
    {
        if (Session["entranceTbl"] != null && ((DataTable)Session["entranceTbl"]) != null)
        {
            DataTable dt = (DataTable)Session["entranceTbl"];
            DataRow dr = dt.NewRow();
            dr["QUALIFYNO"] = Convert.ToInt32(ddlExamNo.SelectedValue);
            dr["QUALIFYEXAMNAME"] = ddlExamNo.SelectedItem.Text;
            if (!txtAllIndiaRank.Text.Trim().Equals(string.Empty)) dr["ALLINDIARANK"] = Convert.ToInt32(txtAllIndiaRank.Text.Trim());
            if (!txtYearOfExam.Text.Trim().Equals(string.Empty)) dr["YEAROFEXAM"] = txtYearOfExam.Text.Trim();
            if (!txtStateRank.Text.Trim().Equals(string.Empty)) dr["STATERANK"] = Convert.ToInt32(txtStateRank.Text.Trim());
            if (!txtPer.Text.Trim().Equals(string.Empty)) dr["PERCENTAGE"] = Convert.ToDecimal(txtPer.Text.Trim());
            dr["QEXMROLLNO"] = txtQExamRollNo.Text.Trim();
            if (!txtPercentile.Text.Trim().Equals(string.Empty)) dr["PERCENTILE"] = Convert.ToDecimal(txtPercentile.Text.Trim());
            dr["QUOTA"] = ddlQuota.Text.Trim();
            dt.Rows.Add(dr);
            Session["entranceTbl"] = dt;
            this.ClearControls_Entrance();
        }
        else
        {
            DataTable dt = this.GetEntranceDataTable();
            DataRow dr = dt.NewRow();
            dr["QUALIFYNO"] = Convert.ToInt32(ddlExamNo.SelectedValue);
            dr["QUALIFYEXAMNAME"] = ddlExamNo.SelectedItem.Text;
            if (!txtAllIndiaRank.Text.Trim().Equals(string.Empty)) dr["ALLINDIARANK"] = Convert.ToInt32(txtAllIndiaRank.Text.Trim());
            if (!txtYearOfExam.Text.Trim().Equals(string.Empty)) dr["YEAROFEXAM"] = txtYearOfExam.Text.Trim();
            if (!txtStateRank.Text.Trim().Equals(string.Empty)) dr["STATERANK"] = Convert.ToInt32(txtStateRank.Text.Trim());
            if (!txtPer.Text.Trim().Equals(string.Empty)) dr["PERCENTAGE"] = Convert.ToDecimal(txtPer.Text.Trim());
            dr["QEXMROLLNO"] = txtQExamRollNo.Text.Trim();
            if (!txtPercentile.Text.Trim().Equals(string.Empty)) dr["PERCENTILE"] = Convert.ToDecimal(txtPercentile.Text.Trim());
            dt.Rows.Add(dr);
            Session["entranceTbl"] = dt;
            this.ClearControls_Entrance();
        }
    }


    private void ShowStudentDetails()
    {
        StudentController objSC = new StudentController();
        DataTableReader dtr = null;
        int stuidno = 0;
        if (ViewState["usertype"].ToString() == "2")
        {
            dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["idno"]));
            stuidno = Convert.ToInt32(Session["idno"]);



        }
        else
        {
            dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["stuinfoidno"]));
            stuidno = Convert.ToInt32(Session["stuinfoidno"]);



        }
        if (dtr != null)
        {
            if (dtr.Read())
            {
                //SSC MARKS
                txtSchoolCollegeNameSsc.Text = dtr["SCHOOL_COLLEGE_NAMESSC"] == null ? string.Empty : dtr["SCHOOL_COLLEGE_NAMESSC"].ToString();
                txtBoardSsc.Text = dtr["BOARDSSC"] == null ? string.Empty : dtr["BOARDSSC"].ToString();
                txtYearOfExamSsc.Text = dtr["YEAR_OF_EXAMSSC"] == null ? string.Empty : dtr["YEAR_OF_EXAMSSC"].ToString();
                txtSSCMedium.Text = dtr["SSC_MEDIUM"] == null ? string.Empty : dtr["SSC_MEDIUM"].ToString();
                txtMarksObtainedSsc.Text = dtr["MARKS_OBTAINEDSSC"] == null ? string.Empty : dtr["MARKS_OBTAINEDSSC"].ToString();
                txtOutOfMarksSsc.Text = dtr["OUT_OF_MRKSSSC"] == null ? string.Empty : dtr["OUT_OF_MRKSSSC"].ToString();
                txtPercentageSsc.Text = dtr["PERSSC"] == null ? string.Empty : dtr["PERSSC"].ToString();
                txtExamRollNoSsc.Text = dtr["QEXMROLLNOSSC"] == null ? string.Empty : dtr["QEXMROLLNOSSC"].ToString();
                txtPercentileSsc.Text = dtr["PERCENTILESSC"] == null ? string.Empty : dtr["PERCENTILESSC"].ToString();
                txtGradeSsc.Text = dtr["GRADESSC"] == null ? string.Empty : dtr["GRADESSC"].ToString();
                txtAttemptSsc.Text = dtr["ATTEMPTSSC"] == null ? string.Empty : dtr["ATTEMPTSSC"].ToString();
                txtSSCSchoolColgAdd.Text = dtr["SSC_COLLEGE_ADDRESS"] == null ? string.Empty : dtr["SSC_COLLEGE_ADDRESS"].ToString();

                //HSSC Marks

                txtSchoolCollegeNameHssc.Text = dtr["SCHOOL_COLLEGE_NAMEHSSC"] == null ? string.Empty : dtr["SCHOOL_COLLEGE_NAMEHSSC"].ToString();
                txtBoardHssc.Text = dtr["BOARDHSSC"] == null ? string.Empty : dtr["BOARDHSSC"].ToString();
                txtYearOfExamHssc.Text = dtr["YEAR_OF_EXAM_GCET"] == null ? string.Empty : dtr["YEAR_OF_EXAM_GCET"].ToString();
                txtHSSCMedium.Text = dtr["HSSC_MEDIUM"] == null ? string.Empty : dtr["HSSC_MEDIUM"].ToString();
                txtMarksObtainedHssc.Text = dtr["MARKS_OBTAINEDHSSC"] == null ? "0" : dtr["MARKS_OBTAINEDHSSC"].ToString();
                txtOutOfMarksHssc.Text = dtr["OUT_OF_MRKSHSSC"] == null ? "0" : dtr["OUT_OF_MRKSHSSC"].ToString();
                txtPercentageHssc.Text = dtr["PERHSSC"] == null ? string.Empty : dtr["PERHSSC"].ToString();
                txtExamRollNoHssc.Text = dtr["QEXMROLLNO_GET"] == null ? string.Empty : dtr["QEXMROLLNO_GET"].ToString();
                txtPercentileHssc.Text = dtr["PERCENTILE"] == null ? "0" : dtr["PERCENTILE"].ToString();
                txtGradeHssc.Text = dtr["GRADEHSSC"] == null ? string.Empty : dtr["GRADEHSSC"].ToString();
                txtAttemptHssc.Text = dtr["ATTEMPTHSSC"] == null ? string.Empty : dtr["ATTEMPTHSSC"].ToString();
                txtHSCColgAddress.Text = dtr["HSSC_COLLEGE_ADDRESS"] == null ? string.Empty : dtr["HSSC_COLLEGE_ADDRESS"].ToString();

                //Subject Wise Marks

                txtHscChe.Text = dtr["HSC_CHE_GCET"] == null ? string.Empty : dtr["HSC_CHE_GCET"].ToString();
                txtHscCheMax.Text = dtr["HSC_CHE_MAX_GCET"] == null ? string.Empty : dtr["HSC_CHE_MAX_GCET"].ToString();
                txtHscPhy.Text = dtr["HSC_PHY_GCET"] == null ? string.Empty : dtr["HSC_PHY_GCET"].ToString();
                txtHscPhyMax.Text = dtr["HSC_PHY_MAX_GCET"] == null ? string.Empty : dtr["HSC_PHY_MAX_GCET"].ToString();
                txtHscEngHssc.Text = dtr["HSC_ENG_GCET"] == null ? string.Empty : dtr["HSC_ENG_GCET"].ToString();
                txtHscEngMaxHssc.Text = dtr["HSC_ENG_MAX_GCET"] == null ? string.Empty : dtr["HSC_ENG_MAX_GCET"].ToString();
                txtHscMaths.Text = dtr["HSC_MAT_GCET"] == null ? string.Empty : dtr["HSC_MAT_GCET"].ToString();
                txtHscMathsMax.Text = dtr["HSC_MAT_MAX_GCET"] == null ? string.Empty : dtr["HSC_MAT_MAX_GCET"].ToString();

                //Entrance Exam Scores
                ddlExamNo.SelectedValue = dtr["QUALIFYNO"].ToString();
                txtQExamRollNo.Text = dtr["QEXMROLLNOHSSC"] == null ? string.Empty : dtr["QEXMROLLNOHSSC"].ToString();
                txtYearOfExam.Text = dtr["YEAR_OF_EXAM"] == null ? string.Empty : dtr["YEAR_OF_EXAM"].ToString();
                txtPer.Text = dtr["PERCENTAGE"] == null ? string.Empty : dtr["PERCENTAGE"].ToString();
                txtPercentile.Text = dtr["PERCENTILE"] == null ? string.Empty : dtr["PERCENTILE"].ToString();
                txtAllIndiaRank.Text = dtr["ALL_INDIA_RANK"] == null ? string.Empty : dtr["ALL_INDIA_RANK"].ToString();
                txtScore.Text = dtr["SCORE"] == null ? string.Empty : dtr["SCORE"].ToString();
                ddlpgentranceno.SelectedValue = dtr["OTHER_QUALIFYNO"].ToString();
                txtpgrollno.Text = dtr["OTHER_QEXMROLLNO"] == null ? string.Empty : dtr["OTHER_QEXMROLLNO"].ToString();
                txtpgexamyear.Text = dtr["OTHER_YEAR_OF_EXAM"] == null ? string.Empty : dtr["OTHER_YEAR_OF_EXAM"].ToString();
                txtpgpercentage.Text = dtr["OTHER_PERCENTAGE"] == null ? string.Empty : dtr["OTHER_PERCENTAGE"].ToString();
                txtpgpercentile.Text = dtr["OTHER_PERCENTILE"] == null ? string.Empty : dtr["OTHER_PERCENTILE"].ToString();
                txtpgrank.Text = dtr["OTHER_PG_RANK"] == null ? string.Empty : dtr["OTHER_PG_RANK"].ToString();
                txtpgscore.Text = dtr["OTHER_SCORE"] == null ? string.Empty : dtr["OTHER_SCORE"].ToString();

                txtHscBioHssc.Text = dtr["HSC_BIO"] == null ? string.Empty : dtr["HSC_BIO"].ToString();
                txtHscPcmHssc.Text = dtr["HSC_PCM"] == null ? string.Empty : dtr["HSC_PCM"].ToString();
                txtHscBioMaxHssc.Text = dtr["HSC_BIO_MAX"] == null ? string.Empty : dtr["HSC_BIO_MAX"].ToString();
                txtHscPcmMaxHssc.Text = dtr["HSC_PCM_MAX"] == null ? string.Empty : dtr["HSC_PCM_MAX"].ToString();
                txtQExamRollNo.Text = dtr["QEXMROLLNO"] == null ? string.Empty : dtr["QEXMROLLNO"].ToString();

                //Added By HEmanth G For Percentage Calculation
                if (txtHscChe.Text.Trim() != "0" && txtHscCheMax.Text.Trim() != "0" && txtHscChe.Text.Trim() != string.Empty && txtHscCheMax.Text.Trim() != "")
                {
                    txtHscPhy.Text = (Double.Parse(txtHscChe.Text.Trim()) / Double.Parse(txtHscCheMax.Text.Trim()) * 100.0).ToString("#0.00");
                }
                else
                {
                    txtHscPhy.Text = string.Empty;
                }
                //Display Item in Listview Control by Calling this Method
                BindListViewQualifyExamDetails(stuidno);




            }
        }
    }

    private void BindListViewQualifyExamDetails(int idno)
    {
        try
        {
            StudentController objSC = new StudentController();
            DataSet dsCT = objSC.GetAllQualifyExamDetails(Convert.ToInt32(idno));

            if (dsCT != null && dsCT.Tables.Count > 0 && dsCT.Tables[0].Rows.Count > 0)
            {
                lvQualExm.DataSource = dsCT;
                lvQualExm.DataBind();
                Session["qualifyTbl"] = dsCT.Tables[0];

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic-BindListViewQualifyExamDetails> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lnkPersonalDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/PersonalDetails.aspx");
    }
    protected void lnkAddressDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/AddressDetails.aspx");
    }
    protected void lnkAdmissionDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/AdmissionDetails.aspx");

    }

    protected void lnkDasaStudentInfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/DASAStudentInformation.aspx");
    }
    protected void lnkQualificationDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/QualificationDetails.aspx");
    }
    protected void lnkotherinfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/OtherInformation.aspx");
    }

    protected void lnkprintapp_Click(object sender, EventArgs e)
    {
        GEC_Student objGecStud = new GEC_Student();
        if (ViewState["usertype"].ToString() == "2")
        {
            objGecStud.RegNo = Session["idno"].ToString();
            string output = objGecStud.RegNo;
            ShowReport("Admission_Form_Report_M.TECH", "Admission_Slip_Confirm_PHD_General.rpt", output);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                objGecStud.RegNo = Session["stuinfoidno"].ToString();
                string output = objGecStud.RegNo;
                ShowReport("Admission_Form_Report_M.TECH", "Admission_Slip_Confirm_PHD_General.rpt", output);
            }
            else
            {
                objCommon.DisplayMessage("Please Search Enrollment No!!", this.Page);
            }
        }
    }
    private void ShowReport(string reportTitle, string rptFileName, string regno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Admission Form Report " + Session["stuinfoenrollno"].ToString();
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(regno) + "";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");
            //ScriptManager.RegisterClientScriptBlock(this.updAddressDetails, this.updAddressDetails.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void lnkGoHome_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "1")
        {
            Session["stuinfoidno"] = null;
            Session["stuinfoenrollno"] = null;
            Session["stuinfofullname"] = null;
            Response.Redirect("~/academic/StudentInfoEntry.aspx?pageno=74");
        }
        else
        {
            Response.Redirect("~/academic/StudentInfoEntry.aspx?pageno=74");
        }
    }
}