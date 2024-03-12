using System;
using System.Collections;
using System.Configuration;
using System.IO;
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
using System.Net;
using System.Net.Mail;
using mastersofterp;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Security;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;



public partial class ACADEMIC_StudentRegistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();
    StudentRegistration objRegistration = new StudentRegistration();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            if (!Page.IsPostBack)
            {
                //Page Authorization
                 CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();             
                txtFirstName.Focus();
                PopulateDropDownList();
                ViewState["Otp"] = null;
                ViewState["Action"] = "";

                if (Session["usertype"].ToString() == "1")
                {
                    divSearch.Visible = true;
                    divRemark.Visible = true;
                  //  DisableControls();         // disbled on 27_01_2022
                    divSearch.Visible = false;   // Add on 27_01_2022
                }
                else
                {
                    divSearch.Visible = false;
                    divRemark.Visible = false;
                    txtDateOfAdmission.Text = DateTime.Today.ToString("dd/MM/yyyy");
                  
                }
                
            }

            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            divMsg.InnerHtml = string.Empty;
            objCommon.SetLabelData("0");//for label
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentRegistration.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentRegistration.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "TOP(1) BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0 AND ODD_EVEN=1", "SEMESTERNO");         
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0 AND ODD_EVEN=1", "SEMESTERNO");
            ddlSemester.SelectedValue = "1";
            objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0 AND PAYTYPENO NOT IN(5)", "PAYTYPENO");
            objCommon.FillDropDownList(ddlAdmType, "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO > 0 AND IDTYPENO NOT IN(3)", "IDTYPENO");
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentRegistration.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string regNo = string.Empty;
        string IUEmail = string.Empty;
        StudentController objSC = new StudentController();
        Student objS = new Student();
        StudentAddress objSAddress = new StudentAddress();
        StudentQualExm objSQualExam = new StudentQualExm();
        GEC_Student objStud = new GEC_Student();
        StudentPhoto objSPhoto = new StudentPhoto();
        UserAcc objUa = new UserAcc();

        try
        {
           
            //*****************Added on 12/07/2017****************************
            if (ddlBranch.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.updStudent, "Please Select Branch!", this.Page);
                return;
            }
            //*********************************************

            if (!txtNameInitial.Text.Trim().Equals(string.Empty)) objS.StudName = txtNameInitial.Text.Trim();


            string firstname = txtFirstName.Text;
            string lastname = txtLastName.Text;
            if (!txtStudMobile.Text.Trim().Equals(string.Empty)) objS.StudentMobile = txtStudMobile.Text.Trim();
            if (!txtFatherMobile.Text.Trim().Equals(string.Empty)) objS.FatherMobile = txtFatherMobile.Text.Trim();
        

            if (rdoMale.Checked)
            {
                objS.Sex = 'M';
            }
            else if (rdoFemale.Checked)
            {
                objS.Sex = 'F';
            }
           
            else
            {
                objCommon.DisplayMessage(this.updStudent, "Please Select Gender!", this.Page);
            }

            int sri = 0;
           if(rdbQuestion.SelectedValue=="1")
           {
               sri = 1;
           }
           else
           {
               sri = 0;
           }
        

            if (!txtDateOfAdmission.Text.Trim().Equals(string.Empty)) objS.AdmDate = Convert.ToDateTime(txtDateOfAdmission.Text.Trim());

            objS.College_ID = Convert.ToInt32(ddlSchool.SelectedValue);
            objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objS.AdmBatch = Convert.ToInt32(ddlBatch.SelectedValue);
            objS.PType = Convert.ToInt32(ddlPaymentType.SelectedValue);

            objS.ExamPtype = Convert.ToInt32(ddlPaymentType.SelectedValue);
            objS.Year = Convert.ToInt32(ddlYear.SelectedValue);
            objS.SemesterNo = (objS.Year * 2) - 1; // calculating semester based on selected year

            objS.CollegeCode = Session["colcode"].ToString();
            objS.Uano = Convert.ToInt32(Session["userno"].ToString());
            objS.IPADDRESS = ViewState["ipAddress"].ToString();
            objS.IdType = Convert.ToInt32(ddlAdmType.SelectedValue);

            if (objSAddress.PSTATE == -99) objSAddress.PSTATE = 0;

            if (objS.StateNo == -99)
                objS.StateNo = 0;

            objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);

            objS.AdmroundNo = 0;

            //ADD THE CONTAIN TO RELATED LOCAL ADDRESS
            if (!txtStudEmail.Text.Trim().Equals(string.Empty)) objS.EmailID = txtStudEmail.Text.Trim();

            if (objSAddress.LCITY == -99) objSAddress.LCITY = 0;
            if (objSAddress.LSTATE == -99) objSAddress.LSTATE = 0;

         
            if (!txtDateOfBirth.Text.Trim().Equals(string.Empty)) objS.DOR = Convert.ToDateTime(txtDateOfBirth.Text.Trim());

            
            //Generate OTP as a Password//
            ViewState["Otp"] = GeneartePassword();
            string password = string.Empty;

            if (ViewState["Action"].ToString() == "Search")
            {
                string output = objSC.AddNewStudent(objS, firstname, lastname, Convert.ToInt32(txtHometel.Text), Convert.ToInt32(txtNIC.Text), Convert.ToDateTime(txtDateOfBirth.Text), Convert.ToString(txtMothersName.Text), Convert.ToString(txtfathername.Text),(txtFatherMobile.Text), sri, Convert.ToInt32(txtPassport.Text), "", txtRemark.Text, Convert.ToInt32(ViewState["IDNO"].ToString()), 3);

                if (output != "-99")
                {
                    objCommon.DisplayMessage(this.updStudent, "Student Record Updated Successfully!", this.Page);
                    Clear();
                }
            }
            else
            {
                int StandardFees = objCommon.LookUp("ACD_STANDARD_FEES", "COUNT(*)", "RECIEPT_CODE='TF' AND BATCHNO=" + ddlBatch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND PAYTYPENO=" + ddlPaymentType.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STANDARD_FEES", "COUNT(*)", "RECIEPT_CODE='TF' AND BATCHNO=" + ddlBatch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND PAYTYPENO=" + ddlPaymentType.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue));

                 if (StandardFees > 0)
                {
                    //string output = objSC.AddNewStudent(objS,"", txtRemark.Text, 0, 1);
                    string output = objSC.AddNewStudent(objS, firstname, lastname, Convert.ToInt32(txtHometel.Text), Convert.ToInt32(txtNIC.Text), Convert.ToDateTime(txtDateOfBirth.Text), Convert.ToString(txtMothersName.Text), Convert.ToString(txtfathername.Text), (txtFatherMobile.Text),sri, Convert.ToInt32(txtPassport.Text), "", txtRemark.Text, 0, 1);

                    if (output != "-99")
                    {
                        ViewState["maxidno"] = output.ToString();
                        string enrollno = objCommon.LookUp("ACD_STUDENT", "DISTINCT ENROLLNO", "IDNO=" + output) == string.Empty ? "" : objCommon.LookUp("ACD_STUDENT", "DISTINCT ENROLLNO", "IDNO=" + output);
                        password = clsTripleLvlEncyrpt.ThreeLevelEncrypt(enrollno.ToString());
                       // output = objSC.AddNewStudent(objS, password, txtRemark.Text, 0, 2);
                        output = objSC.AddNewStudent(objS, firstname, lastname, Convert.ToInt32(txtHometel.Text), Convert.ToInt32(txtNIC.Text), Convert.ToDateTime(txtDateOfBirth.Text), Convert.ToString(txtMothersName.Text), Convert.ToString(txtfathername.Text), (txtFatherMobile.Text),sri, Convert.ToInt32(txtPassport.Text), password, txtRemark.Text, 0, 2);
                        if (output != "-99")
                        {
                            string srnno = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO=" + ViewState["maxidno"].ToString());
                            objCommon.DisplayMessage(this.updStudent, "Student Record Saved Successfully! Enroll. No. is " + srnno + "", this.Page);
                            Clear();
                            //TO SEND MAIL TO THE STUDENT//
                            string subject = "SLIT Login Credentials";
                            string message = "Thanks for showing interest in SLIT. Your Account has been created successfully!<br/>Login with Username : <b>" + srnno + "</b> and Password : <b>" + srnno.ToString() + "</b><br/> Follow this link for further process <b>http://SLIIT.edu.in/</b>";

                           // int status = TransferToEmail(message, objS.EmailID, subject);
                            Execute(message, objS.EmailID, subject).Wait();
              
                            if (txtStudMobile.Text != "")
                            {
                                objCommon.SendSMS(txtStudMobile.Text, "Thanks for showing interest in SLIT. Your SLIT Account has been created successfully! Login with Username : " + srnno + "  Password : " + "" + srnno.ToString() + " Follow this link for further process http://SLIIT.edu.in/" + "");
                            }

                            output = objSC.AddNewStudentDemand(Convert.ToInt32(ViewState["maxidno"].ToString()), "TF", Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlPaymentType.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), Session["colcode"].ToString());


                            Clear();
                        }
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updStudent, "Standard Fees Not Defined for Selected Criteria!", this.Page);
                }
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentInfoEntry.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void Clear()
    {
        //txtStudentfullName.Text = string.Empty;
        txtFirstName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        txtNameInitial.Text = string.Empty;
        txtNIC.Text = string.Empty;
        txtPassport.Text = string.Empty;
        txtDateOfBirth.Text = string.Empty;
        txtHometel.Text = string.Empty;
        txtfathername.Text = string.Empty;
        txtMothersName.Text = string.Empty;
        ddlSchool.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlAdmType.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        rdoMale.Checked = true;
        rdoFemale.Checked = false;     
        txtStudEmail.Text = string.Empty;     
        txtDateOfAdmission.Text = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
        ddlBatch.SelectedIndex = 0;
        ddlPaymentType.SelectedIndex = 0;
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0", "SEMESTERNO");
        ddlSemester.SelectedValue = "1";
        ddlYearSem.Items.Clear();
        ddlYearSem.Items.Insert(0, "Please Select");
        ddlYearSem.SelectedIndex = 0;
        txtStudMobile.Text = string.Empty;       
        txtTanno.Text = string.Empty;
        txtRemark.Text = string.Empty;
        if (Session["usertype"].ToString() == "1")
        {
            txtTanno.Focus();
        }
        else
        {
            
            txtFirstName.Focus();
        }
        if (ViewState["Action"].ToString() == "Search" || Session["usertype"].ToString() == "1")
        {
            DisableControls();
        }
        else
        {
            EnableControls();
        }
        divFees.Visible = false;
        rfvRemark.Enabled = false;
      
        txtFatherMobile.Text = string.Empty;
       
    }

    private string GeneartePassword()
    {
        string allowedChars = "";
        allowedChars = "0,1,2,3,4,5,6,7,8,9";
        //--------------------------------------
        char[] sep = { ',' };

        string[] arr = allowedChars.Split(sep);

        string passwordString = "";

        string temp = "";

        Random rand = new Random();

        for (int i = 0; i < 6; i++)
        {
            temp = arr[rand.Next(0, arr.Length)];
            passwordString += temp;
        }
        return passwordString;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
      
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSchool.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "B.LONGNAME");
            ddlBranch.Focus();

            if (ddlSchool.SelectedValue == "8")
            {
                if (ddlDegree.SelectedValue == "6" || ddlDegree.SelectedValue == "7")
                {
                    objCommon.FillDropDownList(ddlYearSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND ODD_EVEN=1", "SEMESTERNO");
                }
                else
                {
                    objCommon.FillDropDownList(ddlYearSem, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");
                }
            }
        }
        else
        {
            objCommon.DisplayMessage(updStudent, "Please Select School/College Name!", this.Page);
            return;
        }
    }

    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSchool.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlSchool.SelectedValue, "D.DEGREENO");
            ddlDegree.Focus();

            if (ddlSchool.SelectedValue == "1" || ddlSchool.SelectedValue == "2" || ddlSchool.SelectedValue == "3" || ddlSchool.SelectedValue == "4" || ddlSchool.SelectedValue == "5" || ddlSchool.SelectedValue == "8")
            {
                objCommon.FillDropDownList(ddlYearSem, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");
            }
            else
            {
                objCommon.FillDropDownList(ddlYearSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND ODD_EVEN=1", "SEMESTERNO");
            }
        }
        else
        {
            ddlSchool.SelectedIndex = 0;
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtTanno.Text.Trim() == string.Empty)
        {
            //objCommon.DisplayMessage("Enter Temporary Enrollment Number to Modify!", this.Page);
            objCommon.DisplayMessage(updStudent,"Enter Temporary Admission Number to Modify!", this.Page);
            return;
        }

        DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT A", "A.IDNO", "A.STUDNAME, A.COLLEGE_ID, A.DEGREENO, A.BRANCHNO, A.PRINCIPAL_REMARK,A.IDTYPE, A.SEMESTERNO, A.FATHERMOBILE, A.FATHER_EMAIL, A.YEAR, A.SEX, A.STUD_PAN_NO, A.FATHER_PAN_NO, A.ADMDATE, A.DOR,A.ADMCATEGORYNO,A.CLAIMID,A.ADMBATCH, A.PTYPE,A.EMAILID, A.STUDENTMOBILE,A.STUDFIRSTNAME,A.STUDLASTNAME,A.STUDNAME_HINDI,A.MOTHERMOBILE,A.PASSPORTNO,A.MOTHERNAME,A.FATHERNAME,A.FATHERMOBILE,A.DOB,A.TELPHONENO,A.NICNO,A.SRILANKAN,ISNULL(A.TRANSPORT,0) AS TRANSPORT,ISNULL(A.HOSTELER,0) AS HOSTELER,ISNULL(A.SCHOLARSHIP,0) AS SCHOLARSHIP", "A.TANNO = '" + txtTanno.Text.Trim() + "'", string.Empty);
        if (dsStudent != null && dsStudent.Tables.Count > 0)
        {
            if (dsStudent.Tables[0].Rows.Count > 0)
            {
                PopulateDropDownList();
                objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "TOP(1) BATCHNO", "BATCHNAME", "BATCHNO>0 AND BATCHNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString()), "BATCHNO DESC");

                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString(), "D.DEGREENO");

                if (dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString() == "1" || dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString() == "2" || dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString() == "3" || dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString() == "4" || dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString() == "5" || dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString() == "8")
                {
                    objCommon.FillDropDownList(ddlYearSem, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");
                }
                else
                {
                    objCommon.FillDropDownList(ddlYearSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND ODD_EVEN=1", "SEMESTERNO");
                }

                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString()) + " AND CD.BRANCHNO > 0 AND CD.COLLEGE_ID=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString()), "B.LONGNAME");
                ddlBranch.Focus();

                if (dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString() == "8")
                {
                    if (dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() == "6" || dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() == "7")
                    {
                        objCommon.FillDropDownList(ddlYearSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND ODD_EVEN=1", "SEMESTERNO");
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlYearSem, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");
                    }
                }

                EnableControls();
                ViewState["Action"] = "Search";
                rfvRemark.Enabled = true;
                ViewState["IDNO"] = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();             
                txtFirstName.Text = dsStudent.Tables[0].Rows[0]["STUDFIRSTNAME"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["STUDFIRSTNAME"].ToString();
                txtLastName.Text = dsStudent.Tables[0].Rows[0]["STUDLASTNAME"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["STUDLASTNAME"].ToString();
                txtNameInitial.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                txtFatherMobile.Text = dsStudent.Tables[0].Rows[0]["FATHERMOBILE"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["FATHERMOBILE"].ToString();
                txtHometel.Text = dsStudent.Tables[0].Rows[0]["TELPHONENO"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["TELPHONENO"].ToString();
                txtNIC.Text = dsStudent.Tables[0].Rows[0]["NICNO"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["NICNO"].ToString();
                txtPassport.Text = dsStudent.Tables[0].Rows[0]["PASSPORTNO"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["PASSPORTNO"].ToString();
                txtDateOfBirth.Text = dsStudent.Tables[0].Rows[0]["DOB"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["DOB"].ToString();
                txtfathername.Text = dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                txtMothersName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                ddlSchool.SelectedValue = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                ddlDegree.SelectedValue = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                ddlBranch.SelectedValue = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                ddlAdmType.SelectedValue = dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString();
                ddlYearSem.SelectedValue = dsStudent.Tables[0].Rows[0]["YEAR"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["YEAR"].ToString();
           

                if (dsStudent.Tables[0].Rows[0]["SEX"].ToString().Trim().Equals("M"))
                {
                    rdoMale.Checked = true;
                    rdoFemale.Checked = false;
                  
                }
                else if (dsStudent.Tables[0].Rows[0]["SEX"].ToString().Trim().Equals("F"))
                {
                    rdoFemale.Checked = true;
                    rdoMale.Checked = false;
                   
                }
                else if (dsStudent.Tables[0].Rows[0]["SEX"].ToString().Trim().Equals("T"))
                {
                    rdoFemale.Checked = false;
                    rdoMale.Checked = false;
                   
                }
                else
                {
                    rdoFemale.Checked = false;
                    rdoMale.Checked = false;
                    
                }

                if (dsStudent.Tables[0].Rows[0]["SRILANKAN"].ToString().Trim().Equals("1"))
                {
                    rdbQuestion.SelectedValue = "1";
                   
                }
                else if (dsStudent.Tables[0].Rows[0]["SRILANKAN"].ToString().Trim().Equals("F"))
                {

                    rdbQuestion.SelectedValue = "2";
                   
                }

              

                
                ddlYear.SelectedValue = dsStudent.Tables[0].Rows[0]["YEAR"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["YEAR"].ToString();
                ddlAdmType.SelectedValue = dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString();          
                txtStudEmail.Text = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["EMAILID"].ToString();
                txtStudMobile.Text = dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                txtFatherMobile.Text = dsStudent.Tables[0].Rows[0]["FATHERMOBILE"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["FATHERMOBILE"].ToString();
                txtFirstName.Text = dsStudent.Tables[0].Rows[0]["STUDFIRSTNAME"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["STUDFIRSTNAME"].ToString();
                txtLastName.Text = dsStudent.Tables[0].Rows[0]["STUDLASTNAME"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["STUDLASTNAME"].ToString();
                txtNameInitial.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                txtHometel.Text = dsStudent.Tables[0].Rows[0]["TELPHONENO"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["TELPHONENO"].ToString();
                txtNIC.Text = dsStudent.Tables[0].Rows[0]["NICNO"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["NICNO"].ToString();
                txtPassport.Text = dsStudent.Tables[0].Rows[0]["PASSPORTNO"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["PASSPORTNO"].ToString();
                txtDateOfBirth.Text = dsStudent.Tables[0].Rows[0]["DOB"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["DOB"].ToString();
                txtfathername.Text = dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                txtMothersName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                ddlSchool.SelectedValue = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();              
                txtDateOfAdmission.Text = (dsStudent.Tables[0].Rows[0]["ADMDATE"].ToString() == string.Empty ? string.Empty : Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["ADMDATE"].ToString()).ToString("dd/MM/yyyy"));             
                ddlBatch.SelectedValue = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                ddlPaymentType.SelectedValue = dsStudent.Tables[0].Rows[0]["PTYPE"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["PTYPE"].ToString();
                ViewState["PTYPE"] = dsStudent.Tables[0].Rows[0]["PTYPE"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["PTYPE"].ToString();
                txtRemark.Text = dsStudent.Tables[0].Rows[0]["PRINCIPAL_REMARK"].ToString() == string.Empty ? "" : dsStudent.Tables[0].Rows[0]["PRINCIPAL_REMARK"].ToString();
                divFees.Visible = true;
                lblFees.Text = objCommon.LookUp("ACD_STANDARD_FEES", "SUM(SEMESTER" + ddlSemester.SelectedValue + ")", "RECIEPT_CODE='TF' AND BATCHNO=" + ddlBatch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND PAYTYPENO=" + ddlPaymentType.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue) == string.Empty ? "0" : objCommon.LookUp("ACD_STANDARD_FEES", "SUM(SEMESTER" + ddlSemester.SelectedValue + ")", "RECIEPT_CODE='TF' AND BATCHNO=" + ddlBatch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND PAYTYPENO=" + ddlPaymentType.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue);         

                btnSave.Enabled = true;
            }
            else
            {
                objCommon.DisplayMessage(updStudent,"Please Enter Valid Temporary Enrollment Number!", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage(updStudent,"Please Enter Valid Temporary Enrollment Number!", this.Page);
        }
    }

    protected void ddlYearSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYearSem.SelectedIndex > 0)
        {
            ddlYear.SelectedIndex = ddlSemester.SelectedIndex = ddlYearSem.SelectedIndex;
        }
        else
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0 AND ODD_EVEN=1", "SEMESTERNO");
        }
    }

    public int TransferToEmail(string message, string useremail, string subject)
    {
        int ret = 0;
        try
        {
            DataSet ds = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "CollegeName, SENDGRID_USERNAME,SENDGRID_PWD", "", string.Empty);
            var fromAddress = ds.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString();
            var toAddress = useremail;
            string fromPassword = ds.Tables[0].Rows[0]["SENDGRID_PWD"].ToString();
            string userId = ds.Tables[0].Rows[0]["SENDGRID_USERNAME"].ToString();

            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            msg.From = new MailAddress(fromAddress, ds.Tables[0].Rows[0]["CollegeName"].ToString());
            msg.To.Add(new MailAddress(toAddress));

            msg.Subject = subject;

            msg.IsBodyHtml = true;
            msg.Body = message;
            smtp.Host = "smtp.sendgrid.net";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(userId, fromPassword);

            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            smtp.Send(msg);
            return ret = 1;
        }
        catch (Exception ex)
        {
            return ret = 0;
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PresentationLayer_NewRegistration.TransferToEmail-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void DisableControls()
    {
       // txtStudentfullName.Enabled = false;
        txtFirstName.Enabled = false;
        txtLastName.Enabled = false;
        txtNameInitial.Enabled = false;
        txtNIC.Enabled = false;
        txtPassport.Enabled = false;
        txtDateOfBirth.Enabled = false;
        txtHometel.Enabled = false;
        txtMothersName.Enabled = false;
        txtfathername.Enabled = false;

        ddlSchool.Enabled = false;
        ddlDegree.Enabled = false;
        ddlBranch.Enabled = false;
        ddlAdmType.Enabled = false;
        ddlYearSem.Enabled = false;
        rdoMale.Enabled = false;
        rdbQuestion.Enabled = false;
        rdoFemale.Enabled = false;     
        txtStudEmail.Enabled = false;
        txtStudMobile.Enabled = false;
        txtDateOfAdmission.Enabled = false;     
        ddlBatch.Enabled = false;
        ddlPaymentType.Enabled = false;
        txtRemark.Enabled = false;
        btnSave.Enabled = false;       
        txtFatherMobile.Enabled = false;
     
    }

    private void EnableControls()
    {
       
        txtFirstName.Enabled = true;
        txtLastName.Enabled = true;
        txtNameInitial.Enabled = true;
        txtMothersName.Enabled = true;
        txtfathername.Enabled = true;
        txtHometel.Enabled = true;
        txtNIC.Enabled = true;
        txtPassport.Enabled = true;
        txtDateOfBirth.Enabled = true;
        rdbQuestion.Enabled = true;
        ddlSchool.Enabled = true;
        ddlDegree.Enabled = true;
        ddlBranch.Enabled = true;
        ddlAdmType.Enabled = true;
        ddlYearSem.Enabled = true;
        rdoMale.Enabled = true;
        rdoFemale.Enabled = true;    
        txtStudEmail.Enabled = true;
        txtStudMobile.Enabled = true;
        txtDateOfAdmission.Enabled = true;     
        ddlBatch.Enabled = true;
        ddlPaymentType.Enabled = true;
        txtRemark.Enabled = true;
        btnSave.Enabled = true;       
        txtFatherMobile.Enabled = true;
       
    }

    protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["PTYPE"] != null)
        {
            if (ViewState["Action"].ToString() == "Search")
            {
                if (ViewState["PTYPE"].ToString() != "0")
                {
                    if (ddlSemester.SelectedIndex > 0)
                    {
                        lblFees.Text = objCommon.LookUp("ACD_STANDARD_FEES", "SUM(SEMESTER" + ddlSemester.SelectedValue + ")", "RECIEPT_CODE='TF' AND BATCHNO=" + ddlBatch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND PAYTYPENO=" + ddlPaymentType.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue) == string.Empty ? "0" : objCommon.LookUp("ACD_STANDARD_FEES", "SUM(SEMESTER" + ddlSemester.SelectedValue + ")", "RECIEPT_CODE='TF' AND BATCHNO=" + ddlBatch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND PAYTYPENO=" + ddlPaymentType.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue);
                    }
               }
            }
            else if (ViewState["PTYPE"].ToString() != "0")
            {
                lblFees.Text = objCommon.LookUp("ACD_STANDARD_FEES", "SUM(SEMESTER" + ViewState["SEMESTERNO"].ToString() + ")", "RECIEPT_CODE='TF' AND BATCHNO=" + ddlBatch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND PAYTYPENO=" + ViewState["PTYPE"].ToString() + " AND BRANCHNO=" + ddlBranch.SelectedValue) == string.Empty ? "0" : objCommon.LookUp("ACD_STANDARD_FEES", "SUM(SEMESTER" + ViewState["SEMESTERNO"].ToString() + ")", "RECIEPT_CODE='TF' AND BATCHNO=" + ddlBatch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND PAYTYPENO=" + ViewState["PTYPE"].ToString() + " AND BRANCHNO=" + ddlBranch.SelectedValue);
            }
        }
    }

   

    static async Task Execute(string Message, string toEmailId, string Subject)
    {
        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString(), "SLIT");
            var toAddress = new MailAddress(toEmailId, "");
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString(), "SLIT");
            var subject = Subject.ToString();
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);
        }
        catch (Exception ex)
        {

        }
    }

}