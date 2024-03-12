//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : BRANCH CHANGE                                     
// CREATION DATE : 
// ADDED BY      : ASHISH DHAKATE                                                  
// ADDED DATE    : 16-DEC-2011
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================

using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net;

public partial class ACADEMIC_BranchChange : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    int degreeno = 0; int IDNO = 0;
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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                PopulateDropDown();
                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                string IPADDRESS = string.Empty;
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                btnSubmit.Enabled = false;
                if (rblBranchChange.SelectedValue == "2")
                {
                    btnPrint.Visible = true;
                    btnReport.Visible = false;
                    trRemark.Visible = true;
                }
                else
                {
                    btnPrint.Visible = false;
                    trRemark.Visible = false;
                }
            }
        }

        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BranchChange.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BranchChange.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            //session
            if (rblBranchChange.SelectedValue == "1")
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO>0 AND DEGREENO IN (1,3)", "LONGNAME");
            }
            //admround
            objCommon.FillDropDownList(ddlAdmRound, "ACD_ADMISSION_ROUND", "ADMROUNDNO", "ROUNDNAME", "ADMROUNDNO>0", "ROUNDNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_BranchChange.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        int brchange = 0;
        int checksem = 0, chkcgpa = 0, maxsem = 0, chkpass = 0, count = 0;
        string chkph = null;
        string getidno;
        divMsg.InnerHtml = string.Empty;

        if (rblBranchChange.SelectedValue == "1")
        {

            if (txtEnrollNo.Text.Trim() == string.Empty)
            {
                lblMsg.Text = "Please Enter Proper Enrollment No.";
                txtRegno.Focus();
                return;
            }
        }
        else
        {
            if (txtStudent.Text.Trim() == "" || txtStudent.Text.Trim() == string.Empty || txtStudent.Text == null)
            {
                lblMsg.Text = "Please Enter Proper Registration No.";
                txtStudent.Focus();
                return;
            }
        }

        lblMsg.Text = string.Empty;
        //get idno
        if (rblBranchChange.SelectedValue == "1")
        {
            getidno = objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO = '" + txtEnrollNo.Text.Trim() + "' AND CAN=0 AND ADMCAN=0");
        }
        else
        {
            getidno = objCommon.LookUp("ACD_STUDENT", "IDNO", "rollno = '" + txtStudent.Text.Trim() + "' AND CAN=0 AND ADMCAN=0");
        }

        if (getidno == "" || getidno == string.Empty || getidno == null)
        {
            objCommon.DisplayMessage("Roll Number is not Vaild", this.Page);
            ClearControls();
        }

        else
        {
            StudentRegistration objSRegist = new StudentRegistration();
            StudentController objSC = new StudentController();
            if (rblBranchChange.SelectedValue == "1")
            {
                int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO='" + txtEnrollNo.Text.Trim() + "'"));
                //int idno = objSC.GetStudentIDByRegNo(tempno);
                Session["idno"] = idno;
                if (idno == 0)
                {
                    lblName.Text = string.Empty;
                    lblMsg.Text = "Student Not Found!!";
                    return;
                }

                DataTableReader dtr = objSRegist.GetStudentDetails(idno);

                if (dtr.Read())
                {
                    lblName.Text = dtr["STUDNAME"].ToString();
                    lblRegNo.Text = dtr["ROLLNO"].ToString();
                    lblRegNo.ToolTip = dtr["ROLLNO"].ToString();
                    lblBranch.Text = dtr["LONGNAME"].ToString();
                    lblBranch.ToolTip = dtr["BRANCHNO"].ToString();
                    lblAdmbatch.Text = dtr["ADMBATCH"].ToString();
                    ViewState["lblAdmbatch"] = lblAdmbatch.Text;
                    lblYear.Text = dtr["YEARNAME"].ToString();
                    ViewState["lblYear"] = lblYear.Text;
                    btnSubmit.Enabled = true;
                    btnReport.Visible = true;
                }
                else
                {
                    btnSubmit.Enabled = false;
                }
                dtr.Close();

            }
            else
            {

                brchange = Convert.ToInt32(objCommon.LookUp("ACD_BRCHANGE", "COUNT(IDNO)", "IDNO = " + Convert.ToInt32(getidno) + " AND LOCK = 1"));
                if (brchange == 0)
                {
                    //Check Registration Number is not Null.
                    count = Convert.ToInt32(objCommon.LookUp("ACD_TRRESULT", "COUNT(IDNO)", "IDNO = " + Convert.ToInt32(getidno) + " AND REGNO IS NOT NULL"));
                    if (count > 0)
                    {
                        //Check Maximum Semester of Student it should be 2.
                        maxsem = Convert.ToInt32(objCommon.LookUp("ACD_TRRESULT", "MAX(SEMESTERNO)", "IDNO = " + Convert.ToInt32(getidno) + " AND REGNO IS NOT NULL"));
                        if (maxsem == 2)
                        {
                            //Check Student has given both Semester are not?
                            checksem = Convert.ToInt32(objCommon.LookUp("ACD_TRRESULT", "COUNT(IDNO)", "IDNO = " + Convert.ToInt32(getidno) + " AND SEMESTERNO IN (1,2)"));

                            if (checksem == 2)
                            {
                                //Check Student has cleared both Semester are not?
                                chkpass = Convert.ToInt32(objCommon.LookUp("ACD_TRRESULT", "COUNT(IDNO)", "IDNO = " + Convert.ToInt32(getidno) + " AND SEMESTERNO IN (1,2) AND RESULT = 'P'"));

                                if (chkpass == 2)
                                {
                                    //Check CGPA of Student Should be Greater than 7.5.
                                    chkcgpa = Convert.ToInt32(objCommon.LookUp("ACD_TRRESULT", "COUNT(IDNO)", "IDNO = " + Convert.ToInt32(getidno) + " AND SEMESTERNO IN (2) AND CGPA >= 7.5 AND RESULT = 'P'"));

                                    if (chkcgpa == 2)
                                    {
                                        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO>0 AND DEGREENO = (SELECT DEGREENO FROM ACD_STUDENT WHERE REGNO ='" + txtStudent.Text + "' AND REGNO IS NOT NULL)", "BRANCHNO");
                                        try
                                        {
                                            int idno = objSC.GetStudentIDByRegNo(txtStudent.Text.Trim());
                                            Session["idno"] = idno;
                                            if (idno == 0)
                                            {
                                                lblName.Text = string.Empty;
                                                lblMsg.Text = "Student Not Found!!";
                                                return;
                                                ClearControls();
                                                btnSubmit.Enabled = false;
                                            }
                                            chkph = objCommon.LookUp("ACD_STUD_PHOTO", "PHOTO", "IDNO=" + idno);

                                            if (chkph != "")
                                            {
                                                imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + idno.ToString() + "&type=student";
                                            }
                                            DataTableReader dtr = objSRegist.GetStudentDetails(idno);
                                            if (dtr.Read())
                                            {
                                                lblName.Text = dtr["STUDNAME"].ToString();
                                                lblRegNo.Text = dtr["REGNO"].ToString();
                                                lblRegNo.ToolTip = dtr["ROLLNO"].ToString();
                                                lblBranch.Text = dtr["LONGNAME"].ToString();
                                                lblBranch.ToolTip = dtr["BRANCHNO"].ToString();
                                                lblAdmbatch.Text = dtr["ADMBATCH"].ToString();
                                                ViewState["lblAdmbatch"] = lblAdmbatch.Text;
                                                lblYear.Text = dtr["YEARNAME"].ToString();
                                                ViewState["lblYear"] = lblYear.Text;
                                                btnSubmit.Enabled = true;
                                                txtRemark.Text = string.Empty;
                                                txtRegno.Text = string.Empty;
                                            }
                                            else
                                            {
                                                btnSubmit.Enabled = false;
                                            }
                                            dtr.Close();
                                        }
                                        catch (Exception ex)
                                        {
                                            if (Convert.ToBoolean(Session["error"]) == true)
                                                objUaimsCommon.ShowError(Page, "ACADEMIC_BranchChange.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
                                            else
                                                objUaimsCommon.ShowError(Page, "Server UnAvailable");
                                        }
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage("CGPA IS LESS THAN 7.5!!", this.Page);
                                        ClearControls();
                                    }
                                }
                                else
                                {
                                    objCommon.DisplayMessage("NOT CLEARED IN BOTH SEMESTER!!", this.Page);
                                    ClearControls();
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage("YOUR ATTEMPT IS MORE THAN 2 !!", this.Page);
                                ClearControls();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage("YOU ARE NOT IN SECOND SEMESTER !!", this.Page);
                            ClearControls();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage("NOT ELIGIBLE FOR BRANCH CHANGE", this.Page);
                        ClearControls();
                    }
                }

                else
                {
                    objCommon.DisplayMessage("ALREADY BRANCH CHANGE", this.Page);
                    ClearControls();
                }
            }
        }
    }

    private void ShowStudentDetails(int idno)
    {
        try
        {
            StudentRegistration objSRegist = new StudentRegistration();
            DataTableReader dtr = objSRegist.GetStudentDetails(idno);

            if (dtr.Read())
            {
                lblName.Text = dtr["STUDNAME"].ToString();
                lblRegNo.Text = dtr["REGNO"].ToString();
                lblBranch.Text = dtr["LONGNAME"].ToString();
                lblBranch.ToolTip = dtr["BRANCHNO"].ToString();
                imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + idno.ToString() + "&type=student";
            }
            dtr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_BranchChange.ShowStudentDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtRegno.Text != "")
            {
                IDNO = Convert.ToInt32(objCommon.LookUp("ACD_Student", "IDNO", "REGNO='" + txtRegno.Text.Trim() + "'"));
                ShowReport("ACADEMIC_BranchChange", "rptBranchchange.rpt", IDNO);
            }
            else
            {
                if (txtStudent.Text != "")
                {
                    IDNO = Convert.ToInt32(objCommon.LookUp("ACD_Student", "IDNO", "REGNO='" + txtStudent.Text.Trim() + "'"));
                    ShowReport("ACADEMIC_BranchChange", "rptBranchchange.rpt", IDNO);
                }
                else
                {
                    objCommon.DisplayMessage("Please Enter Registration Number!!", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_BranchChange.btnPrint_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (rblBranchChange.SelectedValue == "1")
        {
            StudentController objSController = new StudentController();
            BranchController objBranch = new BranchController();
            Student objStudent = new Student();
            int IDNO = 0;

            IDNO = Convert.ToInt32(objCommon.LookUp("ACD_Student", "IDNO", "ENROLLNO='" + txtEnrollNo.Text.Trim() + "'"));

            StudentRegistration objSRegist = new StudentRegistration();
            DataTableReader dtr = objSRegist.GetStudentDetails(IDNO);

            if (dtr.Read())
            {
                lblName.Text = dtr["STUDNAME"].ToString();
                lblRegNo.Text = dtr["ENROLLNO"].ToString();
                lblBranch.Text = dtr["LONGNAME"].ToString();
                lblBranch.ToolTip = dtr["BRANCHNO"].ToString();
            }
            dtr.Close();

            objStudent.IdNo = Convert.ToInt32(IDNO);
            objStudent.BranchNo = Convert.ToInt32(lblBranch.ToolTip); // old branch no
            objStudent.StudName = lblName.Text;
            objStudent.NewBranchNo = Convert.ToInt32(ddlBranch.SelectedValue); // new branch no
            objStudent.Remark = txtRemark.Text.Trim();
            objStudent.RegNo = txtEnrollNo.Text.Trim();
            objStudent.RollNo = txtEnrollNo.Text.Trim();
            objStudent.CollegeCode = Session["colcode"].ToString();

            CustomStatus cs = (CustomStatus)objBranch.AddChagneBranchData(objStudent);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                btnPrint.Enabled = true;
                lblMsg.Text = "Branch Change Process Completed Successfully";
                btnReport.Visible = true;

            }
            else
            {
                lblMsg.Text = "Error...";
                btnPrint.Enabled = false;
            }
        }
        else
        {
            if (txtStudent.Text != "")
            {
                if (txtRegno.Text != "")
                {
                    StudentController objSController = new StudentController();
                    BranchController objBranch = new BranchController();
                    Student objStudent = new Student();

                    if (txtStudent.Text.Trim().Length > 4)
                    {
                        IDNO = Convert.ToInt32(objCommon.LookUp("ACD_Student", "IDNO", "REGNO='" + txtRegno.Text + "'"));
                    }
                    else if (txtStudent.Text.Trim().Length <= 4)
                    {
                        IDNO = Convert.ToInt32(objCommon.LookUp("ACD_Student", "IDNO", "IDNO='" + txtStudent.Text.Trim() + "'"));
                    }
                    StudentRegistration objSRegist = new StudentRegistration();
                    DataTableReader dtr = objSRegist.GetStudentDetails(IDNO);

                    if (dtr.Read())
                    {
                        lblName.Text = dtr["STUDNAME"].ToString();
                        lblRegNo.Text = dtr["REGNO"].ToString();
                        lblBranch.Text = dtr["LONGNAME"].ToString();
                        lblBranch.ToolTip = dtr["BRANCHNO"].ToString();
                    }
                    dtr.Close();

                    objStudent.IdNo = Convert.ToInt32(IDNO);
                    objStudent.RollNo = txtStudent.Text;
                    objStudent.BranchNo = Convert.ToInt32(lblBranch.ToolTip); // old branch no
                    objStudent.StudName = lblName.Text;
                    objStudent.NewBranchNo = Convert.ToInt32(ddlBranch.SelectedValue); // new branch no
                    objStudent.Remark = txtRemark.Text.Trim();

                    degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(IDNO)));

                    objStudent.RegNo = txtRegno.Text;

                    objStudent.CollegeCode = Session["colcode"].ToString();

                    CustomStatus cs = (CustomStatus)objBranch.AddChagneBranchData(objStudent);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        btnPrint.Enabled = true;
                        lblMsg.Text = "Branch Change Process Completed Successfully";

                    }
                    else
                    {
                        lblMsg.Text = "Error...";
                    }
                }
                else
                {
                    objCommon.DisplayMessage("Please Enter New Registration No!!", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Enter Registration No.!!", this.Page);
            }
        }
    }
    private void ShowReport(string reportTitle, string rptFileName, int idno)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_BranchChange.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    public void ClearControls()
    {
        lblName.Text = string.Empty;
        lblRegNo.Text = string.Empty;
        lblBranch.Text = string.Empty;
        txtStudent.Text = string.Empty;
        lblMsg.Text = string.Empty;
        ddlBranch.SelectedIndex = 0;
        txtRegno.Text = string.Empty;
        txtRemark.Text = string.Empty;
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblBranchChange.SelectedValue == "2")
        {
            if (txtStudent.Text != "")
            {

                string studidno = objCommon.LookUp("ACD_STUDENT", "IDNO", "rollno = '" + txtStudent.Text.Trim() + "' AND CAN=0 AND ADMCAN=0");
                ViewState["studidno"] = studidno;
                degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(ViewState["studidno"])));
                StudentRegistration objRegistration = new StudentRegistration();
                objRegistration.GenereateRegistrationNoBranch(degreeno, Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(lblAdmbatch.Text), Convert.ToInt32(studidno));
                DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT", "STUDNAME", "REGNO,ENROLLNO", "DEGREENO = " + degreeno + " AND BRANCHNO =  " + Convert.ToInt32(lblBranch.ToolTip) + "  AND ADMBATCH =  " + lblAdmbatch.Text + "  AND ADMCAN = 0 AND IDNO=" + Convert.ToInt32(ViewState["studidno"]), "STUDNAME,IDNO");
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    txtRegno.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                }
                else
                {
                    objCommon.DisplayMessage("Registration No. Not Found !!", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Enter Registration No.!!", this.Page);
            }
        }

    }

    protected void rblBranchChange_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblBranchChange.SelectedValue == "1")
        {
            trEnrollno.Visible = true;
            trRegNo.Visible = false;
            trNewRegNo.Visible = false;
            btnPrint.Visible = false;
            trRemark.Visible = false;
        }
        else
        {
            trEnrollno.Visible = false;
            trRegNo.Visible = true;
            trNewRegNo.Visible = true;
            btnReport.Visible = false;
            btnPrint.Visible = true;
            trRemark.Visible = true;
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReportAdmSlip("ACADEMIC_BranchChange", "AdmissionSlipForBtech.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_BranchChange.btnPrint_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReportAdmSlip(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_IDNO=" + Session["idno"] + ",@P_ADMBATCH=" + ViewState["lblAdmbatch"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_BranchChange.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
