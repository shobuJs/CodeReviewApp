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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using System.Text;

public partial class ACADEMIC_StudentTempRegistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objReg = new StudentRegistration();

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
                    CheckPageAuthorization();
                    Page.Title = Session["coll_name"].ToString();
                    trMHCET.Visible = false;
                    trMHCET1.Visible = false;
                    trAIEEE.Visible = false;
                    trAIEEE1.Visible = false;
                    rfvAIEEEScore.Visible = false;
                    rfvAIEEERank.Visible = false;
                    rfvMHCETScore.Visible = false;
                    rfvMHCETMaths.Visible = false;
                    rfvMHCETPhysics.Visible = false;

                    string sessionno = objCommon.LookUp("REFF", "CUR_ADM_SESSIONNO", string.Empty);
                    if (string.IsNullOrEmpty(sessionno))
                    {
                        objCommon.DisplayMessage("Please Set the Admission Session from Reference Page!!", this.Page);
                        return;
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO=" + sessionno, "SESSIONNO DESC");
                        ddlSession.Items.RemoveAt(0);
                    }

                    //Hide Registration No. for Admission Operator..
                    if (Session["username"].ToString() == "admentry")
                        trRegNo.Visible = false;
                    else
                        trRegNo.Visible = true;

                    //Fill First Branch Drop DownList
                    objCommon.FillDropDownList(ddlBranch1, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = 3 AND (BRANCHNO > 0 AND BRANCHNO NOT IN (99,19))", "BRANCHNO");
                    txtFirstName.Focus();
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentTempRegistration.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentTempRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentTempRegistration.aspx");
        }
    }
    protected void rbAIEEE_CheckedChanged(object sender, EventArgs e)
    {
        trAIEEE.Visible = rbAIEEE.Checked;
        trAIEEE1.Visible = rbAIEEE.Checked;
        trMHCET.Visible = !rbAIEEE.Checked;
        trMHCET1.Visible = !rbAIEEE.Checked;
        //validations
        rfvAIEEEScore.Visible = rbAIEEE.Checked;
        rfvAIEEERollNo.Visible = rbAIEEE.Checked;
        rfvAIEEERank.Visible = rbAIEEE.Checked;
        rfvMHCETScore.Visible = !rbAIEEE.Checked;
        rfvMHCETMaths.Visible = !rbAIEEE.Checked;
        rfvMHCETPhysics.Visible = !rbAIEEE.Checked;
        txtAIEEEScore.Focus();
    }

    protected void rbMHCET_CheckedChanged(object sender, EventArgs e)
    {
        trMHCET.Visible = rbMHCET.Checked;
        trMHCET1.Visible = rbMHCET.Checked;
        trAIEEE.Visible = !rbMHCET.Checked;
        trAIEEE1.Visible = !rbMHCET.Checked;
        //validations
        rfvMHCETScore.Visible = rbMHCET.Checked;
        rfvMHCETMaths.Visible = rbMHCET.Checked;
        rfvMHCETPhysics.Visible = rbMHCET.Checked;
        rfvAIEEEScore.Visible = !rbMHCET.Checked;
        rfvAIEEERank.Visible = !rbMHCET.Checked;
        rfvAIEEERollNo.Visible = !rbMHCET.Checked;
        txtMHCETScore.Focus();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (rbMinorityYes.Checked == false && rbMinorityNo.Checked == false)
            {
                objCommon.DisplayMessage("Please Select Linguistic Minority (Yes or No)", this.Page);
                return;
            }

            if (rbAIEEE.Checked == false && rbMHCET.Checked == false)
            {
                objCommon.DisplayMessage("Please Select AIEEE OR MHCET Exam Type", this.Page);
                return;
            }

            if (rbAIEEE.Checked == true && (txtAIEEERank.Text.Trim() == string.Empty || txtAIEEEScore.Text.Trim() == string.Empty))
            {
                objCommon.DisplayMessage("Please Enter all details of AIEEE Exam", this.Page);
                return;
            }

            if (rbMHCET.Checked == true && (txtMHCETMaths.Text.Trim() == string.Empty || txtMHCETPhysics.Text.Trim() == string.Empty || txtMHCETScore.Text.Trim() == string.Empty))
            {
                objCommon.DisplayMessage("Please Enter all details of MHCET Exam", this.Page);
                return;
            }

            //get common fields
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            long idno = (txtRegNo.ToolTip.Trim() == string.Empty ? 0 : Convert.ToInt64(txtRegNo.ToolTip.Trim()));
            string gender = (rbMale.Checked == true ? "M" : "F");
            DateTime dob = Convert.ToDateTime(txtDateOfBirth.Text.Trim());
            DateTime REG_ENTRY_DATE = DateTime.Now;
            int mtongueno = Convert.ToInt32(objCommon.GetIDNo(txtMTongue));
            int cityno = Convert.ToInt32(objCommon.GetIDNo(txtCity));
            int stateno = Convert.ToInt32(objCommon.GetIDNo(txtState));
            int categoryno = Convert.ToInt32(objCommon.GetIDNo(txtCategory));
            int casteno = Convert.ToInt32(objCommon.GetIDNo(txtCaste));
            int nationalityno = Convert.ToInt32(objCommon.GetIDNo(txtNationality));
            int minority = (rbMinorityYes.Checked == true ? 1 : 0);
            int sscmaths = (txtSSCMaths.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtSSCMaths.Text.Trim()));
            decimal ssctotal = (txtSSCTotal.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtSSCTotal.Text.Trim()));
            decimal sscoutofmarks = (txtSSCOutOfMarks.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtSSCOutOfMarks.Text.Trim()));
            decimal sscper = (ssctotal / sscoutofmarks) * 100;
            sscper = Math.Round(sscper, 2);

            int hscphy = (txtHSCPhysics.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtHSCPhysics.Text.Trim()));
            int hscchem = (txtHSCChemistry.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtHSCChemistry.Text.Trim()));
            int hscmaths = (txtHSCMaths.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtHSCMaths.Text.Trim()));
            int hscpcm = hscphy + hscchem + hscmaths;
            decimal hsctotal = (txtHSCTotal.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtHSCTotal.Text.Trim()));
            decimal hscoutofmarks = (txtHSCOutofMarks.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtHSCOutofMarks.Text.Trim()));
            decimal hscper = (hsctotal / hscoutofmarks) * 100;
            hscper = Math.Round(hscper, 2);

            //3 = AIEEE;  15 = MHCET
            int qualifyno = (rbAIEEE.Checked == true ? 3 : 15);
            int sscmaths_max = 0;
            int hscmaths_max = 0;
            int hscchem_max = 0;
            int hscphy_max = 0;
            int hscpcm_max = 0;

            //AIEEE
            int aieee_score = (txtAIEEEScore.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtAIEEEScore.Text.Trim()));
            int aieee_rank = (txtAIEEERank.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtAIEEERank.Text.Trim()));
            int aieee_rollno = (txtAIEEERollNo.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtAIEEERollNo.Text.Trim()));

            //MHCET
            int mhcet_score = (txtMHCETScore.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtMHCETScore.Text.Trim()));
            int mhcet_phy = (txtMHCETPhysics.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtMHCETPhysics.Text.Trim()));
            int mhcet_maths = (txtMHCETMaths.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtMHCETMaths.Text.Trim()));

            if (mtongueno == 0 && txtMTongue.Text.Trim() != string.Empty)
                mtongueno = objCommon.AddMasterTableData("ACD_MTONGUE", "MTONGUENO", "MTONGUE", txtMTongue.Text.Trim(), 0);
            if (mtongueno == -99) mtongueno = 0;

            if (nationalityno == 0 && txtNationality.Text.Trim() != string.Empty)
                nationalityno = objCommon.AddMasterTableData("ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", txtNationality.Text.Trim(), 0);
            if (nationalityno == -99) nationalityno = 0;

            if (cityno == 0 && txtCity.Text.Trim() != string.Empty)
                cityno = objCommon.AddMasterTableData("ACD_CITY", "CITYNO", "CITY", txtCity.Text.Trim(), 0);
            if (cityno == -99) cityno = 0;

            if (stateno == 0 && txtState.Text.Trim() != string.Empty)
                stateno = objCommon.AddMasterTableData("ACD_STATE", "STATENO", "STATENAME", txtState.Text.Trim(), 0);
            if (stateno == -99) stateno = 0;

            if (categoryno == 0 && txtCategory.Text.Trim() != string.Empty)
                categoryno = objCommon.AddMasterTableData("ACD_CATEGORY", "CATEGORYNO", "CATEGORY", txtCategory.Text.Trim(), 0);
            if (categoryno == -99) categoryno = 0;

            if (casteno == 0 && txtCaste.Text.Trim() != string.Empty)
                casteno = objCommon.AddMasterTableData("ACD_CASTE", "CASTENO", "CASTE", txtCaste.Text.Trim(), 0);
            if (casteno == -99) casteno = 0;

            string branch_pref = string.Empty;
            branch_pref = ddlBranch1.SelectedValue;
            branch_pref += "," + ddlBranch2.SelectedValue;
            branch_pref += "," + ddlBranch3.SelectedValue;
            branch_pref += "," + ddlBranch4.SelectedValue;
            branch_pref += "," + ddlBranch5.SelectedValue;
            branch_pref += "," + ddlBranch6.SelectedValue;
            branch_pref += "," + ddlBranch7.SelectedValue;

            string output = objReg.PreAdmissionRegistration(sessionno, idno, txtFirstName.Text.Trim().ToUpper(), txtFatherName.Text.Trim().ToUpper(), txtMothersName.Text.Trim().ToUpper(),
                txtLastName.Text.Trim().ToUpper(), gender, dob, mtongueno, cityno, txtStdPhone.Text.Trim(), txtPhone.Text.Trim(), txtMobile.Text.Trim(), txtEmail.Text.Trim(),
                stateno, casteno, categoryno, nationalityno, minority, qualifyno, sscmaths, sscmaths_max, sscper, Convert.ToInt32(ssctotal), Convert.ToInt32(sscoutofmarks),
                mhcet_score, mhcet_maths, mhcet_phy,
                hscmaths, hscmaths_max, hscchem, hscchem_max, hscphy, hscphy_max, hscpcm, hscpcm_max, hscper, Convert.ToInt32(hsctotal), Convert.ToInt32(hscoutofmarks),
                aieee_score, aieee_rank, aieee_rollno, branch_pref, REG_ENTRY_DATE);

            if (output != "-99")
            {
                this.ClearControls();
                objCommon.DisplayMessage("Student Registered Successfully and You are Registration number is =" + output, this.Page);
                Label1.Text = "You are Registration Number is =" + output;
            }
            else
                objCommon.DisplayMessage("Error!!", this.Page);
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(ex.ToString(), this.Page);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    #region User Defined Methods
    private void ClearControls()
    {
        txtRegNo.Text = string.Empty;
        txtFirstName.Text = string.Empty;
        txtFatherName.Text = string.Empty;
        txtMothersName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        txtDateOfBirth.Text = string.Empty;
        txtMTongue.Text = string.Empty;
        txtNationality.Text = string.Empty;
        txtStdPhone.Text = string.Empty;
        txtCity.Text = string.Empty;
        txtPhone.Text = string.Empty;
        txtMobile.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtState.Text = string.Empty;
        txtCategory.Text = string.Empty;
        txtCaste.Text = string.Empty;
        rbMinorityYes.Checked = true;
        rbMinorityNo.Checked = true;

        //Hide AIEEE & MHCET DETAILS
        trMHCET.Visible = false;
        trMHCET1.Visible = false;
        trAIEEE.Visible = false;
        trAIEEE1.Visible = false;
        rfvAIEEEScore.Visible = false;
        rfvAIEEERank.Visible = false;
        rfvMHCETScore.Visible = false;
        rfvMHCETMaths.Visible = false;
        rfvMHCETPhysics.Visible = false;

        txtAIEEERank.Text = string.Empty;
        txtAIEEEScore.Text = string.Empty;
        txtAIEEERollNo.Text = string.Empty;
        txtMHCETMaths.Text = string.Empty;
        txtMHCETPhysics.Text = string.Empty;
        txtMHCETScore.Text = string.Empty;

        //HSC & SSC
        txtHSCChemistry.Text = string.Empty;
        txtHSCMaths.Text = string.Empty;
        txtHSCPCM.Text = string.Empty;
        txtHSCPhysics.Text = string.Empty;
        txtHSCPer.Text = string.Empty;
        txtHSCTotal.Text = string.Empty;
        txtHSCOutofMarks.Text = string.Empty;
        txtSSCMaths.Text = string.Empty;
        txtSSCPer.Text = string.Empty;
        txtSSCTotal.Text = string.Empty;
        txtSSCOutOfMarks.Text = string.Empty;

        rbMale.Checked = false;
        rbFemale.CausesValidation = false;


        rbMHCET.Checked = false;
        rbAIEEE.Checked = false;

        trMHCET.Visible = false;
        trMHCET1.Visible = false;
        trAIEEE.Visible = false;
        trAIEEE1.Visible = false;

        ddlBranch1.SelectedIndex = 0;
    }
    #endregion

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (txtRegNo.Text.Trim() == string.Empty)
        {
            objCommon.DisplayMessage("Enter Registration No. to Modify!", this.Page);
            return;
        }

        DataSet dsStudent = objCommon.FillDropDown("ACD_REGISTRATION", "SESSIONNO,IDNO", "STUDNAME,FATHERNAME,MOTHERNAME,LASTNAME,GENDER,DOB,MTONGUENO,PCITY,PTELEPHONE,PTELEPHONESTD,PMOBILE,EMAILID,STATENO,CASTE,CATEGORYNO,NATIONALITYNO,MINORITY,QUALIFYNO,SSC_MATHS,SSC_MATHS_MAX,SSC_MATHS_PER,SSC_TOTAL,SSC_OUTOF,MHCET_SCORE,MHCET_MATHS_SCORE,MHCET_PHYSICS_SCORE,HSC_MAT,HSC_MAT_MAX,HSC_CHE,HSC_CHE_MAX,HSC_PHY,HSC_PHY_MAX,HSC_PCM,HSC_PCM_MAX,PER,HSC_TOTAL,HSC_OUTOF,AIEEE_SCORE,AIEEE_RANK,AIEEE_ROLLNO,BRANCH_PREF", "IDNO = " + txtRegNo.Text.Trim(), string.Empty);
        if (dsStudent != null && dsStudent.Tables.Count > 0)
        {
            if (dsStudent.Tables[0].Rows.Count > 0)
            {
                ddlSession.SelectedValue = dsStudent.Tables[0].Rows[0]["SESSIONNO"].ToString();
                txtRegNo.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                txtFirstName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                txtFatherName.Text = dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                txtMothersName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                txtLastName.Text = dsStudent.Tables[0].Rows[0]["LASTNAME"].ToString();

                if (dsStudent.Tables[0].Rows[0]["GENDER"].ToString().Trim().Equals("M"))
                {
                    rbMale.Checked = true;
                    rbFemale.Checked = false;
                }
                else
                {
                    rbFemale.Checked = true;
                    rbMale.Checked = false;
                }

                txtDateOfBirth.Text = (dsStudent.Tables[0].Rows[0]["DOB"].ToString() == string.Empty ? string.Empty : Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy"));

                txtMTongue.Text = (dsStudent.Tables[0].Rows[0]["MTONGUENO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["MTONGUENO"].ToString()), "ACD_MTONGUE", "MTONGUENO", "MTONGUE"));
                txtCity.Text = (dsStudent.Tables[0].Rows[0]["PCITY"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PCITY"].ToString()), "ACD_CITY", "CITYNO", "CITY"));

                txtStdPhone.Text = dsStudent.Tables[0].Rows[0]["PTELEPHONESTD"].ToString();
                txtPhone.Text = dsStudent.Tables[0].Rows[0]["PTELEPHONE"].ToString();
                txtMobile.Text = dsStudent.Tables[0].Rows[0]["PMOBILE"].ToString();
                txtEmail.Text = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString();
                txtState.Text = (dsStudent.Tables[0].Rows[0]["STATENO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["STATENO"].ToString()), "ACD_STATE", "STATENO", "STATENAME"));
                txtCaste.Text = (dsStudent.Tables[0].Rows[0]["CASTE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["CASTE"].ToString()), "ACD_CASTE", "CASTENO", "CASTE"));
                txtCategory.Text = (dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString()), "ACD_CATEGORY", "CATEGORYNO", "CATEGORY"));
                txtNationality.Text = (dsStudent.Tables[0].Rows[0]["NATIONALITYNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["NATIONALITYNO"].ToString()), "ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY"));

                if (dsStudent.Tables[0].Rows[0]["MINORITY"].ToString() == "1")
                {
                    rbMinorityYes.Checked = true;
                    rbMinorityNo.Checked = false;
                }
                else
                {
                    rbMinorityYes.Checked = false;
                    rbMinorityNo.Checked = true;
                }

                //3 = AIEEE  15 = MHCET
                if (dsStudent.Tables[0].Rows[0]["QUALIFYNO"].ToString() == "3")
                {
                    rbAIEEE.Checked = true;
                    rbAIEEE_CheckedChanged(sender, e);
                }
                else
                {
                    rbMHCET.Checked = true;
                    rbMHCET_CheckedChanged(sender, e);
                }

                txtSSCMaths.Text = dsStudent.Tables[0].Rows[0]["SSC_MATHS"].ToString();
                txtSSCPer.Text = dsStudent.Tables[0].Rows[0]["SSC_MATHS_PER"].ToString();
                txtSSCTotal.Text = dsStudent.Tables[0].Rows[0]["SSC_TOTAL"].ToString();
                txtSSCOutOfMarks.Text = dsStudent.Tables[0].Rows[0]["SSC_OUTOF"].ToString();

                txtMHCETScore.Text = dsStudent.Tables[0].Rows[0]["MHCET_SCORE"].ToString();
                txtMHCETMaths.Text = dsStudent.Tables[0].Rows[0]["MHCET_MATHS_SCORE"].ToString();
                txtMHCETPhysics.Text = dsStudent.Tables[0].Rows[0]["MHCET_PHYSICS_SCORE"].ToString();

                txtHSCMaths.Text = dsStudent.Tables[0].Rows[0]["HSC_MAT"].ToString();
                txtHSCChemistry.Text = dsStudent.Tables[0].Rows[0]["HSC_CHE"].ToString();
                txtHSCPhysics.Text = dsStudent.Tables[0].Rows[0]["HSC_PHY"].ToString();
                txtHSCPCM.Text = dsStudent.Tables[0].Rows[0]["HSC_PCM"].ToString();
                txtHSCPer.Text = dsStudent.Tables[0].Rows[0]["PER"].ToString();
                txtHSCTotal.Text = dsStudent.Tables[0].Rows[0]["HSC_TOTAL"].ToString();
                txtHSCOutofMarks.Text = dsStudent.Tables[0].Rows[0]["HSC_OUTOF"].ToString();

                txtAIEEEScore.Text = dsStudent.Tables[0].Rows[0]["AIEEE_SCORE"].ToString();
                txtAIEEERank.Text = dsStudent.Tables[0].Rows[0]["AIEEE_RANK"].ToString();
                txtAIEEERollNo.Text = dsStudent.Tables[0].Rows[0]["AIEEE_ROLLNO"].ToString();

                //Branch Preferences..
                char sp = ',';
                string[] branches = dsStudent.Tables[0].Rows[0]["BRANCH_PREF"].ToString().Split(sp);
                if (branches.Length > 1)
                {
                    objCommon.FillDropDownList(ddlBranch1, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = 3 AND (BRANCHNO > 0 AND BRANCHNO NOT IN (99,19))", "BRANCHNO");
                    if (branches[0] != string.Empty || Convert.ToInt32(branches[0]) != 0)
                        ddlBranch1.SelectedValue = branches[0];
                    objCommon.FillDropDownList(ddlBranch2, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = 3 AND (BRANCHNO > 0 AND BRANCHNO NOT IN (99,19," + ddlBranch1.SelectedValue + "))", "BRANCHNO");
                    if (branches[1] != string.Empty || Convert.ToInt32(branches[1]) != 0)
                        ddlBranch2.SelectedValue = branches[1];
                    objCommon.FillDropDownList(ddlBranch3, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = 3 AND (BRANCHNO > 0 AND BRANCHNO NOT IN (99,19," + ddlBranch1.SelectedValue + "," + ddlBranch2.SelectedValue + "))", "BRANCHNO");
                    if (branches[2] != string.Empty || Convert.ToInt32(branches[2]) != 0)
                        ddlBranch3.SelectedValue = branches[2];
                    objCommon.FillDropDownList(ddlBranch4, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = 3 AND (BRANCHNO > 0 AND BRANCHNO NOT IN (99,19," + ddlBranch1.SelectedValue + "," + ddlBranch2.SelectedValue + "," + ddlBranch3.SelectedValue + "))", "BRANCHNO");
                    if (branches[3] != string.Empty || Convert.ToInt32(branches[3]) != 0)
                        ddlBranch4.SelectedValue = branches[3];
                    objCommon.FillDropDownList(ddlBranch5, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = 3 AND (BRANCHNO > 0 AND BRANCHNO NOT IN (99,19," + ddlBranch1.SelectedValue + "," + ddlBranch2.SelectedValue + "," + ddlBranch3.SelectedValue + "," + ddlBranch4.SelectedValue + "))", "BRANCHNO");
                    if (branches[4] != string.Empty || Convert.ToInt32(branches[4]) != 0)
                        ddlBranch5.SelectedValue = branches[4];
                    objCommon.FillDropDownList(ddlBranch6, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = 3 AND (BRANCHNO > 0 AND BRANCHNO NOT IN (99,19," + ddlBranch1.SelectedValue + "," + ddlBranch2.SelectedValue + "," + ddlBranch3.SelectedValue + "," + ddlBranch4.SelectedValue + "," + ddlBranch5.SelectedValue + "))", "BRANCHNO");
                    if (branches[5] != string.Empty || Convert.ToInt32(branches[5]) != 0)
                        ddlBranch6.SelectedValue = branches[5];
                    objCommon.FillDropDownList(ddlBranch7, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = 3 AND (BRANCHNO > 0 AND BRANCHNO NOT IN (99,19," + ddlBranch1.SelectedValue + "," + ddlBranch2.SelectedValue + "," + ddlBranch3.SelectedValue + "," + ddlBranch4.SelectedValue + "," + ddlBranch5.SelectedValue + "," + ddlBranch6.SelectedValue + "))", "BRANCHNO");
                    if (branches[6] != string.Empty || Convert.ToInt32(branches[6]) != 0)
                        ddlBranch7.SelectedValue = branches[6];
                }
                else
                {
                    ddlBranch1.SelectedIndex = 0;
                    ddlBranch2.SelectedIndex = 0;
                    ddlBranch3.SelectedIndex = 0;
                    ddlBranch4.SelectedIndex = 0;
                    ddlBranch5.SelectedIndex = 0;
                    ddlBranch6.SelectedIndex = 0;
                    ddlBranch7.SelectedIndex = 0;
                }

                txtFirstName.Focus();
            }
            else
            {
                objCommon.DisplayMessage("Please Enter Valid Registration No.!", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage("Please Enter Valid Registration No.!", this.Page);
        }
    }

    protected void txtMTongue_TextChanged(object sender, EventArgs e)
    {
        if (txtMTongue.Text.Contains("HINDI"))
        {
            rbMinorityYes.Checked = true;
            rbMinorityNo.Checked = false;
        }
        else
        {
            rbMinorityYes.Checked = false;
            rbMinorityNo.Checked = true;
        }

        txtCity.Focus();
    }

    protected void ddlBranch1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Fill Branch Drop DownList
        objCommon.FillDropDownList(ddlBranch2, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = 3 AND (BRANCHNO > 0 AND BRANCHNO NOT IN (99,19," + ddlBranch1.SelectedValue + "))", "BRANCHNO");
        ddlBranch2.Focus();
    }
    protected void ddlBranch2_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Fill Branch Drop DownList
        objCommon.FillDropDownList(ddlBranch3, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = 3 AND (BRANCHNO > 0 AND BRANCHNO NOT IN (99,19," + ddlBranch1.SelectedValue + "," + ddlBranch2.SelectedValue + "))", "BRANCHNO");
        ddlBranch3.Focus();
    }
    protected void ddlBranch3_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Fill Branch Drop DownList
        objCommon.FillDropDownList(ddlBranch4, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = 3 AND (BRANCHNO > 0 AND BRANCHNO NOT IN (99,19," + ddlBranch1.SelectedValue + "," + ddlBranch2.SelectedValue + "," + ddlBranch3.SelectedValue + "))", "BRANCHNO");
        ddlBranch4.Focus();
    }
    protected void ddlBranch4_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Fill Branch Drop DownList
        objCommon.FillDropDownList(ddlBranch5, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = 3 AND (BRANCHNO > 0 AND BRANCHNO NOT IN (99,19," + ddlBranch1.SelectedValue + "," + ddlBranch2.SelectedValue + "," + ddlBranch3.SelectedValue + "," + ddlBranch4.SelectedValue + "))", "BRANCHNO");
        ddlBranch5.Focus();
    }
    protected void ddlBranch5_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Fill Branch Drop DownList
        objCommon.FillDropDownList(ddlBranch6, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = 3 AND (BRANCHNO > 0 AND BRANCHNO NOT IN (99,19," + ddlBranch1.SelectedValue + "," + ddlBranch2.SelectedValue + "," + ddlBranch3.SelectedValue + "," + ddlBranch4.SelectedValue + "," + ddlBranch5.SelectedValue + "))", "BRANCHNO");
        ddlBranch6.Focus();
    }
    protected void ddlBranch6_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Fill Branch Drop DownList
        objCommon.FillDropDownList(ddlBranch7, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = 3 AND (BRANCHNO > 0 AND BRANCHNO NOT IN (99,19," + ddlBranch1.SelectedValue + "," + ddlBranch2.SelectedValue + "," + ddlBranch3.SelectedValue + "," + ddlBranch4.SelectedValue + "," + ddlBranch5.SelectedValue + "," + ddlBranch6.SelectedValue + "))", "BRANCHNO");
        ddlBranch7.Focus();
    }
}
