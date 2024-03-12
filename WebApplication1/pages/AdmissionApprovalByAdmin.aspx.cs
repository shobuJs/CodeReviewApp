using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Web.Configuration;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Security;
using System.Diagnostics;

using System.Collections.Generic;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using EASendMail;




public partial class ACADEMIC_AdmissionApprovalByAdmin : System.Web.UI.Page
{
    private Common objCommon = new Common();
    private UAIMS_Common objUCommon = new UAIMS_Common();
    private StudentRegistrationModel objSR = new StudentRegistrationModel();
    private FeeDemand feeDemand = new FeeDemand();
    private StudentRegistrationController objStudRegC = new StudentRegistrationController();
    private FeeCollectionController feeController = new FeeCollectionController();
    UserController user = new UserController();

    string degreeno = string.Empty;
    string collegeid = string.Empty;
    string branchno = string.Empty;
    string preference = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //  CheckPageAuthorization();  
                Page.Title = Session["coll_name"].ToString();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                string path = Request.Url.AbsoluteUri;
                string appno = string.Empty;

                if (Convert.ToString(Session["urlAppno"]) != string.Empty)
                {
                    appno = Convert.ToString(Session["urlAppno"]); // Request.QueryString["AppNo"].ToString();
                    SearchStudent(appno);
                    Session["urlAppno"] = null;
                }
            }
        }
        Session["urlAppno"] = null;
        
    }

    private void BindDocuments()
    {
        try
        {
            DataSet dsDocuments = objCommon.GetDocumentDetailsOfStudent(ViewState["userno"].ToString());

            string filepath = System.Configuration.ConfigurationManager.AppSettings["path"];

            if (dsDocuments.Tables[0].Rows.Count > 0)
            {
                lvDocument.DataSource = dsDocuments;
                lvDocument.DataBind();
                divDoc.Visible = true;
                lvDocument.Visible = true;
                int i = 0;

                foreach (ListViewItem lvitem in lvDocument.Items)
                {
                    CheckBox chkVerified = lvitem.FindControl("chkVerified") as CheckBox;
                    HiddenField hdnDocno = lvitem.FindControl("hdnDocno") as HiddenField;

                    string verified = objCommon.LookUp("DOCUMENTENTRY_FILE", "ISNULL(IsDocumentVerified,0) AS A", "USERNO=" + Convert.ToInt32(Session["User"].ToString()) + " AND DOCNO=" + Convert.ToInt32(hdnDocno.Value)) == string.Empty ? "0" : objCommon.LookUp("DOCUMENTENTRY_FILE", "ISNULL(IsDocumentVerified,0) AS A", "USERNO=" + Convert.ToInt32(Session["User"].ToString()) + " AND DOCNO=" + Convert.ToInt32(hdnDocno.Value));
                    //string verified = objCommon.LookUp("DOCUMENTENTRY_FILE", "DOCNO", "USERNO=" + Convert.ToInt32(Session["User"].ToString()) + " AND DOCNO='" + hdnDocno.Value + "'") == string.Empty ? "0" : objCommon.LookUp("DOCUMENTENTRY_FILE", "DOCNO", "USERNO=" +Convert.ToInt32(ViewState["userno"]) + " AND DOCNO='" + hdnDocno.Value + "'");

                    if (verified == "1")
                    {
                        i++;
                        chkVerified.Checked = true;
                    }
                }

                foreach (ListViewItem lvitem in lvDocument.Items)
                {
                    CheckBox chkVerified = lvitem.FindControl("chkVerified") as CheckBox;

                    if (i >= 1)
                    {
                        ////if (Session["ADMISSION_STATUS"].ToString() == "1")
                        ////{
                        ////    chkVerified.Enabled = false;
                        ////}
                        ////  divBoard.Visible = false;
                        ////  divApprove.Visible = false;
                        ////  divPayment.Visible = false;
                        divRemark.Visible = false;
                        ////  divSuccess.Visible = false;

                        divBoard.Visible = true;
                        divApprove.Visible = true;
                        divPayment.Visible = true;
                        divSuccess.Visible = true;
                    }
                    else
                    {
                        divBoard.Visible = true;
                        divApprove.Visible = true;
                        divPayment.Visible = true;
                        divSuccess.Visible = true;
                    }
                }

            }
            else
            {
                divDoc.Visible = false;
                lvDocument.Visible = false;
                divBoard.Visible = true; /// For the  UG
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_publication_Details .BindDocuments-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    void GetCandidateDetails(string AppNo)
    {
        DataSet ds = new DataSet();
        int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + txtApplNo.Text.ToString() + "'"));
        ds = objCommon.GetStudentDetails(txtApplNo.Text, IDNO);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataSet lastuginfo = new DataSet();

            //lastuginfo = objCommon.GetQualifyExamUGStudentDetails(ds.Tables[0].Rows[0]["USERNO"].ToString());
            lastuginfo = objCommon.FillDropDown("ACD_STUDENT_LAST_QUALIFICATION", "UNO", "QUALIFYEXAMNAME,BOARD_UNIV,PERCENTAGE,YEAR_OF_PASSING,SUBJECTS ", "UNO=" + ds.Tables[0].Rows[0]["USERNO"].ToString() + "AND QUALIFYNO not in (1,2)", ""); //
            if (lastuginfo.Tables[0].Rows.Count > 0)
            {
                lblQlyExam.Text = lastuginfo.Tables[0].Rows[0]["QUALIFYEXAMNAME"].ToString();
                lblBoardUni.Text = lastuginfo.Tables[0].Rows[0]["BOARD_UNIV"].ToString();
                lblPercentage.Text = lastuginfo.Tables[0].Rows[0]["PERCENTAGE"].ToString();
                lblYearPass.Text = lastuginfo.Tables[0].Rows[0]["YEAR_OF_PASSING"].ToString();
                lblSubject.Text = lastuginfo.Tables[0].Rows[0]["SUBJECTS"].ToString();
                lblSchool.Text = lastuginfo.Tables[0].Rows[0]["SCHOOL_NAME"].ToString();
                lblMonthPass.Text = lastuginfo.Tables[0].Rows[0]["MONTH_OF_PASSING"].ToString();
                dvUG.Visible = true;
            }
            else
            {
            }

            txtApplNo.Enabled = false;
            divimg.Visible = true;

            if (ds.Tables[0].Rows[0]["PHOTO"].ToString() != string.Empty)
            {
                imgPhoto.ImageUrl = "../showimage.aspx?id=" + IDNO + "&type=STUDENT";
            }
            else
            {
                imgPhoto.ImageUrl = "~/images/nophoto.jpg";
            }

            lblStudName.Text = ds.Tables[0].Rows[0]["FIRSTNAME"].ToString();
            lblDOB.Text = ds.Tables[0].Rows[0]["DOB1"].ToString();
            lblCollege.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
            lblEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
            lblApplNo.Text = ds.Tables[0].Rows[0]["USERNAME"].ToString();
            //lblCategory.Text = ds.Tables[0].Rows[0]["CATEGORYNAME"].ToString();
            lblDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
            lblMobile.Text = ds.Tables[0].Rows[0]["MOBILE"].ToString();
            Session["Degreeno"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            Session["CollegeId"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            Session["User"] = ds.Tables[0].Rows[0]["USERNO"].ToString();
            Session["Branchno"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
            Session["UsernameTEMP"] = ds.Tables[0].Rows[0]["USERNAME"].ToString();
            Session["Mobile"] = ds.Tables[0].Rows[0]["MOBILE"].ToString();
            Session["Email"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();
            Session["StudName"] = ds.Tables[0].Rows[0]["FIRSTNAME"].ToString() + " " + ds.Tables[0].Rows[0]["LASTNAME"].ToString();
            Session["ADMBATCH"] = ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
            Session["ADMISSION_STATUS"] = ds.Tables[0].Rows[0]["ADMISSION_STATUS"].ToString();
            Session["ADMISSION_REMARK"] = ds.Tables[0].Rows[0]["ADMISSION_REMARK"].ToString();
            Session["BOARD_STUDIED"] = ds.Tables[0].Rows[0]["BOARD_STUDIED"].ToString();
            Session["ACCOUNT_STATUS"] = ds.Tables[0].Rows[0]["ACCOUNT_STATUS"].ToString();
            Session["ACCOUNT_REMARK"] = ds.Tables[0].Rows[0]["ACCOUNT_REMARK"].ToString();
            Session["PTYPE"] = ds.Tables[0].Rows[0]["PTYPE"].ToString();
            //lblAdmCategory.Text = ds.Tables[0].Rows[0]["ADMCATEGORY"].ToString();
            lblIN_HOUSE.Text = ds.Tables[0].Rows[0]["IN_HOUSE"].ToString();
            //lbl12Per.Text = ds.Tables[0].Rows[0]["PERCENTAGE"].ToString();
            lblUndertaking.Text = ds.Tables[0].Rows[0]["UNDERTAKING"].ToString();
            lblPrograme.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
            BindDocuments();
            divSuccess.Visible = true;

            if (Session["ADMISSION_STATUS"].ToString() == "1")
            {
                //rbApprove.Checked = true;
            }
            else if (Session["ADMISSION_STATUS"].ToString() == "2")
            {
                rbReject.Checked = true;
                txtRemark.Text = Session["ADMISSION_REMARK"].ToString();
                txtRemark.Visible = true;
                divRemark.Visible = true;
            }
            else if (Session["ADMISSION_STATUS"].ToString() == "3")
            {
                rbHold.Checked = true;
                txtRemark.Text = Session["ADMISSION_REMARK"].ToString();
                txtRemark.Visible = true;
                divRemark.Visible = true;
            }
            objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "DISTINCT PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0", "PAYTYPENO");
      
           
        }
        else
        {
            objCommon.DisplayMessage(updAdmissionDetails, "No record found", this.Page);
            Clear();
            divRemark.Visible = false;
            rbApprove.Checked = false;
            rbReject.Checked = false;
            rbHold.Checked = false;
            divPayment.Visible = false;
            divdata.Visible = false;
            divDoc.Visible = false;
            divApprove.Visible = false;
            divBoard.Visible = false;
            rbMaha.Checked = false;
            rbOther.Checked = false;
        }
    }

    public void GetApplicationCollegeDegreeBranchDetails(string AppNo)
    {
        DataSet dsAppDetails = objCommon.FillDropDown("ACD_USER_BRANCH_PREF A INNER JOIN ACD_USER_REGISTRATION B ON A.USERNO = B.USERNO LEFT JOIN ACD_DEGREE DEG ON DEG.DEGREENO = A.DEGREENO ", "A.BRPREF", "A.USERNO,A.BRANCH_PREF,A.DEGREENO,A.BRANCHNO,A.PROGRAN_TYPE,A.MERIT_LIST_NO,A.MERIT_GEN_DATE,A.COLLEGE_ID,A.GD_PI_CAMPUS,DEG.DEGREENAME,A.CAMPUS_CHOICE_PREF,A.DEPARTMENTNO,A.ONLINETESTCENTERCHOICE,ADMISSION_STATUS,ACCOUNT_STATUS,B.DATA_TRANSFERED", "A.USERNO = " +Convert.ToInt32(ViewState["userno"]) + " AND APPLIED = 1", "");

        DataSet CheckStatus = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON S.DEGREENO = D.DEGREENO", "IDNO", "COLLEGE_ID,BRANCHNO,S.DEGREENO,D.DEGREENAME", "USERNO=" + Convert.ToInt32(ViewState["userno"]), "");

        if (dsAppDetails.Tables[0].Rows.Count > 0)
        {
            string collegeid = string.Empty;
            for (int i = 0; i < dsAppDetails.Tables[0].Rows.Count; i++)
            {
                if (collegeid == string.Empty)
                {
                    collegeid = dsAppDetails.Tables[0].Rows[i]["COLLEGE_ID"].ToString();
                }
                else
                {
                    if (collegeid != dsAppDetails.Tables[0].Rows[i]["COLLEGE_ID"].ToString())
                    {
                        collegeid += "," + dsAppDetails.Tables[0].Rows[i]["COLLEGE_ID"].ToString();
                    }
                }
            }

            try
            {
                ddlCollege.Items.Clear();
                //  DataSet dsCollege = objCommon.FillDropDown("ACD_COLLEGE_MASTER", "DISTINCT COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN (" + collegeid + ")", "");
                //int CheckMerit = Convert.ToInt32(objCommon.LookUp("ACD_ONLINE_USER_UPLOAD", "COUNT(*)", "ISNULL(MERIT_STATUS,0)=0 AND USERNAME=" + Session["UsernameTEMP"] + ""));
                int CheckMerit = Convert.ToInt32(objCommon.LookUp("ACD_USER_BRANCH_PREF", "ISNULL(COUNT(MERITNO),0) MERIT_COUNT", "USERNO=" +Convert.ToInt32(ViewState["userno"]) + " AND (MERITNO IS NOT NULL)"));
                //Temp Commented 27-10-2020
                //if (CheckMerit > 0)
                //{
                //    //objCommon.FillDropDownList(ddlCollege, "ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID  LEFT JOIN ACD_BRANCH SGM ON UR.BRANCHNO = SGM.BRANCHNO   INNER JOIN ACD_ONLINE_USER_UPLOAD UP ON (UP.USERNAME=UR.USERNO  AND UP.COLLEGE_ID=UR.COLLEGE_ID AND UP.DEGREENO=UR.DEGREENO AND SGM.BRANCHNO=ISNULL(UP.SUBGROUPNO,0)) INNER JOIN ACD_FEES_LOG FE ON (FE.USERNO=UR.USERNO AND FE.DEGREENO=UR.DEGREENO AND FE.COLLEGE_ID=UR.COLLEGE_ID ) ", "DISTINCT convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE LONGNAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE PREFERENCE_NAME END  DEGREENAME", " ISNULL(MERIT_STATUS,0)=0 AND ISNULL(FREEZE,0)<>1 AND D.DEGREENO >0 AND ISNULL(APPLIED,0)=1 AND UP.USERNAME ='" + Session["UsernameTEMP"] + "'", "");
                //}
                //else
                //{
                //    //objCommon.FillDropDownList(ddlCollege, "ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID  LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO  LEFT JOIN ACD_PREFERENCE P ON P.PREFERENCE_ID = UR.BRANCH_PREF", "convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE PREFERENCE_NAME END  DEGREENAME", " D.DEGREENO in (1,4,12) AND UR.USERNO =" + Convert.ToInt32(ViewState["userno"]) + " AND ISNULL(APPLIED,0)=1", "D.DEGREENAME");
                //}

                // local offline admited
                /// objCommon.FillDropDownList(ddlCollege, "ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID  LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO  LEFT JOIN ACD_PREFERENCE P ON P.PREFERENCE_ID = UR.BRANCH_PREF", "convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE PREFERENCE_NAME END  DEGREENAME", " UR.USERNO =" + txtApplNo.Text.Trim() + " AND ISNULL(APPLIED,0)=1", "D.DEGREENAME");

                string degreename = string.Empty;
                for (int i = 0; i < CheckStatus.Tables[0].Rows.Count; i++)
                {
                    string CHECK = CheckStatus.Tables[0].Rows[i]["IDNO"].ToString();
                    if (CHECK.ToString() != string.Empty)
                    {
                        string degree = Convert.ToString(CheckStatus.Tables[0].Rows[i]["DEGREENO"]);
                        string college = Convert.ToString(CheckStatus.Tables[0].Rows[i]["COLLEGE_ID"]);
                        string branch = Convert.ToString(CheckStatus.Tables[0].Rows[i]["BRANCHNO"]);
                        //string pref = Convert.ToString(dsAppDetails.Tables[0].Rows[i]["BRANCH_PREF"]);
                        string combination = degree + "." + college + "." + branch;

                        lblAppliedDegrees.Text = " <label style='color:red;margin-left:5px'>" + CheckStatus.Tables[0].Rows[i]["DEGREENAME"].ToString() + " , </label>" + degreename;
                        degreename = lblAppliedDegrees.Text;
                        //ListItem itemToRemove = ddlCollege.Items.FindByValue(combination);
                        //ddlCollege.Items.Remove(itemToRemove);
                    }
                }
                if (lblAppliedDegrees.Text != string.Empty)
                {
                    lblAppliedDegrees.Text = lblAppliedDegrees.Text.Substring(0, lblAppliedDegrees.Text.Length - 10);
                }
            }
            catch
            {
                //ddlCollege.Items.Clear();

                //int CheckMerit = Convert.ToInt32(objCommon.LookUp("ACD_ONLINE_USER_UPLOAD", "COUNT(*)", "ISNULL(MERIT_STATUS,0)=0 AND USERNAME=" + Session["UsernameTEMP"] + ""));
                //int CheckMerit = Convert.ToInt32(objCommon.LookUp("ACD_USER_BRANCH_PREF", "ISNULL(COUNT(MERITNO),0) MERIT_COUNT", "USERNO=" + Convert.ToInt32(ViewState["userno"]) + " AND (MERITNO IS NOT NULL)"));
                //if (CheckMerit > 0)
                //{
                //    //objCommon.FillDropDownList(ddlCollege, "ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID  LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO  LEFT JOIN ACD_PREFERENCE P ON P.PREFERENCE_ID = UR.BRANCH_PREF INNER JOIN ACD_ONLINE_USER_UPLOAD UP ON (UP.USERNAME=UR.USERNO AND UP.COLLEGE_ID=UR.COLLEGE_ID AND UP.DEGREENO=UR.DEGREENO)", "DISTINCT convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE PREFERENCE_NAME END  DEGREENAME", " ISNULL(MERIT_STATUS,0)=0 AND D.DEGREENO >0 AND UP.USERNAME =" + Session["username"] + "", "");
                //    // objCommon.FillDropDownList(ddlCollege, "ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID  LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO  LEFT JOIN ACD_PREFERENCE P ON P.PREFERENCE_ID = UR.BRANCH_PREF INNER JOIN ACD_ONLINE_USER_UPLOAD UP ON (UP.USERNAME=UR.USERNO  AND UP.COLLEGE_ID=UR.COLLEGE_ID AND UP.DEGREENO=UR.DEGREENO) INNER JOIN ACD_FEES_LOG FE ON (FE.USERNO=UR.USERNO AND FE.DEGREENO=UR.DEGREENO AND FE.COLLEGE_ID=UR.COLLEGE_ID AND ISNULL(FE.BRANCHNO,0)= ISNULL(UR.BRANCHNO,0)) ", "DISTINCT convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE PREFERENCE_NAME END  DEGREENAME", " ISNULL(MERIT_STATUS,0)=0 AND ISNULL(FREEZE,0)<>1 AND D.DEGREENO >0 AND UP.USERNAME =" + Session["username"] + "", "");
                //    objCommon.FillDropDownList(ddlCollege, "ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID  LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO  LEFT JOIN ACD_PREFERENCE P ON P.PREFERENCE_ID = UR.BRANCH_PREF INNER JOIN ACD_ONLINE_USER_UPLOAD UP ON (UP.USERNAME=UR.USERNO  AND UP.COLLEGE_ID=UR.COLLEGE_ID AND UP.DEGREENO=UR.DEGREENO AND SGM.SUGNO=ISNULL(UP.SUBGROUPNO,0)) INNER JOIN ACD_FEES_LOG FE ON (FE.USERNO=UR.USERNO AND FE.DEGREENO=UR.DEGREENO AND FE.COLLEGE_ID=UR.COLLEGE_ID AND ISNULL(FE.BRANCHNO,0)= ISNULL(UR.BRANCHNO,0)) ", "DISTINCT convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE PREFERENCE_NAME END  DEGREENAME", " ISNULL(MERIT_STATUS,0)=0 AND ISNULL(FREEZE,0)<>1 AND D.DEGREENO >0 AND ISNULL(APPLIED,0)=1 AND UP.USERNAME ='" + Session["UsernameTEMP"] + "'", "");
                //}
                //else
                //{
                //    /// DataSet dsCollege = objCommon.FillDropDown("ACD_COLLEGE_MASTER", "DISTINCT COLLEGE_ID", "COLLEGE_NAME", "", "");
                //    objCommon.FillDropDownList(ddlCollege, "ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID  LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO  LEFT JOIN ACD_PREFERENCE P ON P.PREFERENCE_ID = UR.BRANCH_PREF", "convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE PREFERENCE_NAME END  DEGREENAME", " D.DEGREENO in (1,4,12) AND UR.USERNO =" + Convert.ToInt32(ViewState["userno"]) + " AND ISNULL(APPLIED,0)=1", "D.DEGREENAME");
                //    //ddlCollege.DataSource = dsCollege;

                //    //ddlCollege.DataSource = dsCollege;
                //    //ddlCollege.DataTextField = "COLLEGE_NAME";
                //    //ddlCollege.DataValueField = "COLLEGE_ID";
                //    //ddlCollege.DataBind();
                //    //ddlCollege.Items.Insert(0, new ListItem("Please Select", "0"));
                //}
                string degreename = string.Empty;
                for (int i = 0; i < CheckStatus.Tables[0].Rows.Count; i++)
                {
                    string CHECK = CheckStatus.Tables[0].Rows[i]["IDNO"].ToString();
                    if (CHECK.ToString() == "1")
                    {
                        string degree = Convert.ToString(CheckStatus.Tables[0].Rows[i]["DEGREENO"]);
                        string college = Convert.ToString(CheckStatus.Tables[0].Rows[i]["COLLEGE_ID"]);
                        string branch = Convert.ToString(CheckStatus.Tables[0].Rows[i]["BRANCHNO"]);
                        //string pref = Convert.ToString(dsAppDetails.Tables[0].Rows[i]["BRANCH_PREF"]);
                        string combination = degree + "." + college + "." + branch;

                        lblAppliedDegrees.Text = " <label style='color:red;margin-left:5px'>" + dsAppDetails.Tables[0].Rows[i]["DEGREENAME"].ToString() + " , </label>" + degreename;
                        degreename = lblAppliedDegrees.Text;
                        //ListItem itemToRemove = ddlCollege.Items.FindByValue(combination);
                        //ddlCollege.Items.Remove(itemToRemove);
                    }
                }
                if (lblAppliedDegrees.Text != string.Empty)
                {
                    lblAppliedDegrees.Text = lblAppliedDegrees.Text.Substring(0, lblAppliedDegrees.Text.Length - 10);
                }
            }
            DataSet dsDegree = objCommon.FillDropDown("ACD_DEGREE", "DISTINCT DEGREENO", "DEGREENAME", "DEGREENO =" + dsAppDetails.Tables[0].Rows[0]["DEGREENO"].ToString(), "");



            for (int i = 0; i < dsAppDetails.Tables[0].Rows.Count; i++)
            {
                if (degreeno == string.Empty)
                {
                    degreeno = dsAppDetails.Tables[0].Rows[i]["DEGREENO"].ToString();
                    lblDegreeSelected.Text = dsAppDetails.Tables[0].Rows[i]["DEGREENAME"].ToString();
                }
                else
                {
                    if (degreeno != dsAppDetails.Tables[0].Rows[i]["DEGREENO"].ToString())
                    {
                        degreeno += "," + dsAppDetails.Tables[0].Rows[i]["DEGREENO"].ToString();
                        lblDegreeSelected.Text += ", " + dsAppDetails.Tables[0].Rows[i]["DEGREENAME"].ToString();
                    }
                }
            }

            string branchno = string.Empty;

            for (int i = 0; i < dsAppDetails.Tables[0].Rows.Count; i++)
            {
                if (branchno == string.Empty)
                {
                    branchno = dsAppDetails.Tables[0].Rows[i]["BRANCHNO"].ToString();
                }
                else
                {
                    if (branchno != dsAppDetails.Tables[0].Rows[i]["BRANCHNO"].ToString())
                    {
                        branchno += "," + dsAppDetails.Tables[0].Rows[i]["BRANCHNO"].ToString();
                    }
                }
            }

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        // objCommon.DisplayMessage(updAdmissionDetails, hdnfldVariable.Value, this.Page);
        dvUG.Visible = false;
        ViewState["userno"] = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" +txtApplNo.Text.ToString() + "'");
        //int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + txtApplNo.Text.ToString() + "'"));
        DataSet dsStudInfo = objCommon.FillDropDown("ACD_USER_PROFILE_STATUS A INNER JOIN ACD_USER_BRANCH_PREF B ON A.USERNO = B.USERNO INNER JOIN ACD_COLLEGE_MASTER C ON (B.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT B.COLLEGE_ID", "C.CODE", "A.USERNO = " + Convert.ToInt32(ViewState["userno"]) + " AND ISNULL(Applied,0)=1", string.Empty);
        string[] ua_college_id = Session["college_nos"].ToString().Split(',');
        int ApprovalAutho = 0;
         if (dsStudInfo.Tables[0].Rows.Count > 0)
            {
                    for (int i = 0; i < ua_college_id.Length; i++)
                    {
                       
                            if (ua_college_id[i].ToString() == dsStudInfo.Tables[0].Rows[0]["COLLEGE_ID"].ToString())
                            {
                                //objCommon.DisplayMessage(updAdmissionDetails, "Application Not Confirmed!", this.Page);
                                ApprovalAutho = 0; //match authorization
                                break;
                            }
                            else
                            {
                                ApprovalAutho = 1;//not match authorizationddlCollege
                            }
                        
                       
                }
           
            }
         else
         {
             objCommon.DisplayMessage(updAdmissionDetails, "No Record Found ", this.Page);
         }
        if (ApprovalAutho == 1)
        {
            objCommon.DisplayMessage(updAdmissionDetails, "Sorry Authorization is not allow for " + dsStudInfo.Tables[0].Rows[0]["CODE"], this.Page);
            return;
        }

        Clear();
        DivApproveDegree.Visible = true;
        if (txtApplNo.Text != string.Empty || txtApplNo.Text != "")
        {
            string UserNo = txtApplNo.Text.Trim();

            string RecExists = objCommon.LookUp("ACD_USER_PROFILE_STATUS A INNER JOIN ACD_USER_BRANCH_PREF B ON A.USERNO = B.USERNO", "COUNT(*)", "A.USERNO = " + Convert.ToInt32(ViewState["userno"]) + " AND ISNULL(Applied,0)=1");

            if (!string.IsNullOrEmpty(RecExists))
            {
                if (Convert.ToInt32(RecExists) > 0)
                {
                    divPayment.Visible = true;
                    divdata.Visible = true;
                    divApprove.Visible = true;
                    GetCandidateDetails(txtApplNo.Text);
                    GetApplicationCollegeDegreeBranchDetails(txtApplNo.Text);
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                    divSuccess.Visible = true;
                    DataSet ds = new DataSet();
                    int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + txtApplNo.Text.ToString() + "'"));
                    ds = objCommon.GetStudentDetails(txtApplNo.Text, IDNO);
                    if (Session["ADMISSION_STATUS"].ToString() != "1")
                    {
                        if (Session["ACCOUNT_STATUS"].ToString() != "1" || Session["ACCOUNT_STATUS"].ToString() != "2" || Session["ACCOUNT_STATUS"].ToString() != "3")
                        {
                            divApprove.Visible = true;
                            rbApprove.Visible = true;
                            rbHold.Visible = true;
                            rbReject.Visible = true;
                            rbApprove.Enabled = true;
                            rbHold.Enabled = true;
                            rbReject.Enabled = true;
                            btnSubmit.Enabled = true;
                        }
                        else
                        {
                            objCommon.DisplayMessage(updAdmissionDetails, "Application for admission already approved by admission section!", this.Page);
                        }
                    }
                }
                else if (Convert.ToInt32(RecExists) == 0)
                {
                    string checkConfirmStatus = objCommon.LookUp("ACD_USER_PROFILE_STATUS ", "COUNT(*)", "USERNO = " + ViewState["userno"] );
                    if (!string.IsNullOrEmpty(checkConfirmStatus))
                    {
                        if (Convert.ToInt32(checkConfirmStatus) > 0)
                        {
                            objCommon.DisplayMessage(updAdmissionDetails, "Application Not Confirmed!", this.Page);
                            btnCancel.Visible = false;
                            btnSubmit.Visible = false;
                            Clear();
                            divimg.Visible = false;
                        }
                        else
                        {
                            objCommon.DisplayMessage(updAdmissionDetails, "Application No. not found!", this.Page);
                            btnCancel.Visible = false;
                            btnSubmit.Visible = false;
                            Clear();
                            divimg.Visible = false;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updAdmissionDetails, "Application No. not found!", this.Page);
                        btnCancel.Visible = false;
                        btnSubmit.Visible = false;
                        Clear();
                        divimg.Visible = false;
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(updAdmissionDetails, "Application No. not found!", this.Page);
                btnCancel.Visible = false;
                btnSubmit.Visible = false;
                Clear();
                divimg.Visible = false;
            }
            
            string AdmExist = objCommon.LookUp("ACD_STUDENT", "COUNT(*)", "USERNO = " +Convert.ToInt32(ViewState["userno"]) + "");
            if (!string.IsNullOrEmpty(AdmExist))
            {
                if (Convert.ToInt32(AdmExist) > 0)
                {
                    if (Session["ADMISSION_STATUS"].ToString() != "1")
                    {
                        objCommon.DisplayMessage(updAdmissionDetails, "Applicant data already transfered in ERP, but admission status can be updated!", this.Page);
                        btnSubmit.Enabled = true;
                        btnCancel.Enabled = true;
                        btnSubmit.Visible = true;
                        btnCancel.Visible = true;
                        divSuccess.Visible = true;
                    }
                    else
                    {
                        objCommon.DisplayMessage(updAdmissionDetails, "Applicant data already transfered in ERP!", this.Page);
                        txtApplNo.Enabled = true;
                        //btnSubmit.Enabled = false;
                        // btnCancel.Enabled = true;
                        //btnSubmit.Visible = false;
                        //btnCancel.Visible = false;
                        divDoc.Visible = true;
                    }

                    //  return;
                }
            }

        }
        else
        {
            objCommon.DisplayMessage(updAdmissionDetails, "Please Enter Application No", this.Page);
            btnCancel.Visible = false;
            btnSubmit.Visible = false;
            Clear();
            divimg.Visible = false;
        }
        GetPayment();
    }

    protected void ddlPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPaymentType.SelectedIndex > 0)
        {
            string amount = objCommon.LookUp("ACD_STANDARD_FEES", "SUM(SEMESTER1)", "DEGREENO=" + Convert.ToInt32(Session["Degreeno"].ToString()) + " AND COLLEGE_ID=" + Convert.ToInt32(Session["CollegeId"].ToString()) + " AND (BRANCHNO=" + Convert.ToInt32(Session["Branchno"].ToString()) + " OR BRANCHNO = 0)  AND BATCHNO=" + Convert.ToInt32(Session["ADMBATCH"].ToString()) + " AND PAYTYPENO=" + Convert.ToInt32(ddlPaymentType.SelectedValue)) == string.Empty ? "0" : objCommon.LookUp("acd_standard_fees", "SUM(SEMESTER1)", "DEGREENO=" + Convert.ToInt32(Session["Degreeno"].ToString()) + " AND COLLEGE_ID=" + Convert.ToInt32(Session["CollegeId"].ToString()) + " AND PAYTYPENO=" + Convert.ToInt32(ddlPaymentType.SelectedValue) + " AND (BRANCHNO=" + Convert.ToInt32(Session["Branchno"].ToString()) + " OR BRANCHNO = 0) AND BATCHNO=" + Convert.ToInt32(Session["ADMBATCH"].ToString()) + " AND RECIEPT_CODE='TF'");

            if (amount == "0" || amount == "")
            {
                //lblAmt.Visible = false;
                objCommon.DisplayMessage(updAdmissionDetails, "Fees not defined for the selected payment type! Please define the fees!", this.Page);
                btnSubmit.Enabled = false;
                rbApprove.Enabled = false;
                rbReject.Enabled = false;
                rbHold.Checked = false;
                return;
            }
            else
            {
                if (rbMaha.Checked == true)
                {
                    int amt = 0;

                    string verifyfees = objCommon.LookUp("ACD_STANDARD_FEES", "SUM(SEMESTER1)", "DEGREENO=" + Convert.ToInt32(Session["Degreeno"].ToString()) + " AND COLLEGE_ID=" + Convert.ToInt32(Session["CollegeId"].ToString()) + " AND PAYTYPENO=" + Convert.ToInt32(ddlPaymentType.SelectedValue) + " AND BATCHNO=" + Convert.ToInt32(Session["ADMBATCH"].ToString()) + " AND (BRANCHNO=" + Convert.ToInt32(Session["Branchno"].ToString()) + " OR BRANCHNO = 0) AND FEE_HEAD='F25'") == string.Empty ? "0" : objCommon.LookUp("acd_standard_fees", "SUM(SEMESTER1)", "DEGREENO=" + Convert.ToInt32(Session["Degreeno"].ToString()) + " AND COLLEGE_ID=" + Convert.ToInt32(Session["CollegeId"].ToString()) + " AND PAYTYPENO=" + Convert.ToInt32(ddlPaymentType.SelectedValue) + " AND BATCHNO=" + Convert.ToInt32(Session["ADMBATCH"].ToString()) + " AND (BRANCHNO=" + Convert.ToInt32(Session["Branchno"].ToString()) + " OR BRANCHNO = 0) AND FEE_HEAD='F25'");

                    amount = amount.Substring(0, amount.IndexOf("."));
                    verifyfees = verifyfees.Substring(0, verifyfees.IndexOf("."));

                    amt = Convert.ToInt32(amount) - Convert.ToInt32(verifyfees);
                    if (amt == 0)
                    {
                        amount = "Not Applicable";
                    }
                    amount = Convert.ToString(amt) + ".00";
                }

                lblAmt.Text = "Paid Fees: Rs. " + amount;
                lblAmt.Visible = true;
                rbApprove.Enabled = true;
                rbReject.Enabled = true;
                rbHold.Enabled = true;
            }
        }
        else
        {
            lblAmt.Visible = false;
            rbApprove.Enabled = false;
            rbReject.Enabled = false;
            rbHold.Enabled = false;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int count = 0;
        UserAcc objUA = new UserAcc();

        DataSet ds3 = objCommon.FillDropDown("ACD_USER_REGISTRATION", "FIRSTNAME", "USERNAME", "USERNO=" +Convert.ToInt32(ViewState["userno"]), "");
        foreach (ListViewItem lvitem in lvDocument.Items)
        {
            CheckBox chkVerified = lvitem.FindControl("chkVerified") as CheckBox;

            if (chkVerified.Checked == true)
            {
                count++;
            }
        }

        if (count == 0)
        {
            if (rbApprove.Checked == true && rbReject.Checked == false && rbHold.Checked == false)//added pankaj19082021
            {
                objCommon.DisplayMessage(updAdmissionDetails, "Please select the verified documents!", this.Page);
                return;
            }
        }
        else if (rbApprove.Checked == false && rbReject.Checked == false && rbHold.Checked == false)
        {
            objCommon.DisplayMessage(updAdmissionDetails, "Please select the Admission Approval Status!", this.Page);
            rbApprove.Enabled = true;
            rbReject.Enabled = true;
            rbHold.Enabled = true;
            return;
        }
        else if (rbReject.Checked == true || rbHold.Checked == true)
        {
            if (txtRemark.Text == string.Empty)
            {
                objCommon.DisplayMessage(updAdmissionDetails, "Please enter the remark!", this.Page);
                return;
            }
        }
        else if (Session["BOARD_STUDIED"].ToString() == "" || Session["BOARD_STUDIED"].ToString() == string.Empty)
        {
            if (rbMaha.Checked == false && rbOther.Checked == false)
            {
                objCommon.DisplayMessage(updAdmissionDetails, "Please select the Board Studied!", this.Page);
                ddlPaymentType.Items.Clear();
                ddlPaymentType.Items.Insert(0, new ListItem("Please Select", "0"));
                rbApprove.Enabled = false;
                rbReject.Enabled = false;
                rbHold.Enabled = false;
                divRemark.Visible = false;
                rbApprove.Checked = false;
                rbReject.Checked = false;
                rbHold.Checked = false;
                return;
            }
        }

        try
        {
            //Student Admission Details

            #region Commented Code
        

            #endregion

            string Student_Name = objCommon.LookUp("ACD_USER_REGISTRATION", "FIRSTNAME", "USERNO=" +Convert.ToInt32(ViewState["userno"]));

            string newdegree = ddlCollege.SelectedValue;

            //DataSet ds = objCommon.FillDropDown("ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO LEFT JOIN ACD_PREFERENCE P ON P.PREFERENCE_ID = UR.BRANCH_PREF ", "UR.DEGREENO,UR.COLLEGE_ID,UR.BRANCHNO,UR.BRANCH_PREF", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE PREFERENCE_NAME END  DEGREENAME", " convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) ='" + newdegree + "' AND UR.USERNO =" + txtApplNo.Text + "", "D.DEGREENAME");
            //DataSet ds = objCommon.FillDropDown("ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID", "UR.DEGREENO,UR.COLLEGE_ID,UR.BRANCHNO,UR.BRANCH_PREF", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE BRANCHNO END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE DEGREENAME END  DEGREENAME", " convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) ='" + newdegree + "' AND UR.USERNO =" + Convert.ToInt32(ViewState["userno"]) + "", "D.DEGREENAME");
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO)", "S.DEGREENO,D.DEGREENAME", "S.BRANCHNO,S.COLLEGE_ID", " S.USERNO=" + Convert.ToInt32(ViewState["userno"]), "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                degreeno = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                collegeid = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                branchno = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                //preference = ds.Tables[0].Rows[0]["BRANCH_PREF"].ToString();
            }


            int userno = Convert.ToInt32(ViewState["userno"].ToString());
            //string collegeid = Session["CollegeId"].ToString();
            //string degreeno = Session["Degreeno"].ToString();
            //string branchno = Session["Branchno"].ToString();
            string paytype = Convert.ToString(ddlPaymentType.SelectedValue);

            int status = 0;
            int boardstudied = 0;

            if (rbApprove.Checked == true)
            {
                status = 1;
                txtRemark.Text = string.Empty;
            }
            else if (rbReject.Checked == true)
            {
                status = 2;
            }
            else if (rbHold.Checked == true)
            {
                status = 3;
            }

            if (rbMaha.Checked == true)
            {
                boardstudied = 1;
            }
            else if (rbOther.Checked == true)
            {
                boardstudied = 2;
            }

            string ipaddress = Session["ipAddress"].ToString();

            string sss = Session["userno"].ToString();

            CustomStatus csdoc = 0;
            CustomStatus cs = (CustomStatus)objStudRegC.AddStudentDetails(userno, collegeid, degreeno, branchno, txtRemark.Text, status, boardstudied, Convert.ToInt32(Session["userno"].ToString()), ipaddress, DateTime.Now);
            //if (rbApprove.Checked)
            //{
            //    CreateDemand();
            //}
            if (Convert.ToInt32(cs) == 1 || Convert.ToInt32(cs) == 2)
            {
                foreach (ListViewItem lvitem in lvDocument.Items)
                {
                    CheckBox chkVerified = lvitem.FindControl("chkVerified") as CheckBox;
                    HiddenField hdnDoc = lvitem.FindControl("hdnDoc") as HiddenField;
                    HiddenField hdnDocno = lvitem.FindControl("hdnDocno") as HiddenField;

                    if (chkVerified.Checked == true)
                    {
                        csdoc = (CustomStatus)objStudRegC.UpdateDocumentVerification(userno, Convert.ToInt32(hdnDocno.Value));
                    }
                }
                if ((Convert.ToInt32(cs) == 1 && Convert.ToInt32(csdoc) == 1) || (Convert.ToInt32(cs) == 1 && Convert.ToInt32(csdoc) == 1 || Convert.ToInt32(csdoc) == 0))//added pankaj19082021
                {
                    objCommon.DisplayMessage(updAdmissionDetails, "Record Submitted Successfully!", this.Page);

                    string studmobile = Session["Mobile"].ToString();
                    string studemail = Session["Email"].ToString();
                    string matter = string.Empty;

                    if (status == 1)
                    {
                        matter = "Approved.";
                    }
                    else if (status == 2)
                    {
                        if (txtRemark.Text != string.Empty || txtRemark.Text != "")
                        {
                            matter = "Rejected due to " + txtRemark.Text;
                        }
                        else
                        {
                            matter = "Rejected";
                        }
                    }
                    else if (status == 3)
                    {
                        if (txtRemark.Text != string.Empty || txtRemark.Text != "")
                        {
                            matter = "Kept on hold with following Remark : " + txtRemark.Text;
                        }
                        else
                        {
                            matter = "Kept on hold";
                        }
                    }

                    if (studmobile != string.Empty || studmobile != "")
                    {
                        //Close on 22-10-2020 temp  
                       SendSMS(studmobile, "Dear " + Student_Name + ", Your application for admission in " + ds.Tables[0].Rows[0]["DEGREENAME"].ToString() + ", had been " + matter);
                    }

                    if (studemail != string.Empty || studemail != "")
                    {
                        ViewState["StudentName"] = Student_Name;
                        ViewState["DegreeName"] = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                        ViewState["Message"] = matter;

                    
                     //TransferToEmail(studemail, "SLIIT", ds3.Tables[0].Rows[0]["FIRSTNAME"].ToString(), ds.Tables[0].Rows[0]["DEGREENAME"].ToString(), matter, ds3.Tables[0].Rows[0]["USERNAME"].ToString());

                    }
                    string Max_idno = objCommon.LookUp("ACD_STUDENT", "MAX(IDNO)", "REGNO='"+ txtApplNo.Text +"'");
                    DataSet dsStud = objCommon.FillDropDown("ACD_STUDENT S LEFT JOIN ACD_DEGREE D ON S.DEGREENO = D.DEGREENO", "DISTINCT IDNO, REGNO, STUDENTMOBILE, BRANCHNO, SEX, CATEGORYNO, RELIGIONNO, YEAR", "SEMESTERNO, S.DEGREENO, SECTIONNO, SCHEMENO,DEGREENAME, EMAILID, PTYPE, ADMBATCH, COLLEGE_ID", "IDNO='" + Max_idno.ToString() + "'", "");         
                    string DegreeName = string.Empty;
                    if (dsStud.Tables[0].Rows.Count > 0)
                    {
                        
                        DegreeName = dsStud.Tables[0].Rows[0]["DEGREENAME"].ToString();
                    }
                    else
                    {
                        DegreeName = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                    }
                    //end added by pankaj 14082021 
                    objUA.EMAIL = objCommon.LookUp("ACD_USER_REGISTRATION", "DISTINCT EMAILID", "USERNAME='" + Session["UsernameTEMP"] + "'") == string.Empty ? "" : objCommon.LookUp("ACD_USER_REGISTRATION", "DISTINCT EMAILID", "USERNAME='" + Session["UsernameTEMP"] + "'");

                    objUA.MOBILE = Session["Mobile"].ToString();
                    string username = objCommon.LookUp("ACD_USER_REGISTRATION", "DISTINCT USERNAME", "USERNAME='" + Session["UsernameTEMP"] + "'");
                    //string useremail = objUA.EMAIL;
                    string useremail = studemail;
                    string sudname = Student_Name;

                    string degreename = DegreeName.ToString();
                    string usernoo = Session["User"].ToString();

                    string subject = "";
                    //const string EmailTemplate = "<!DOCTYPE html><html><head><meta charset='utf-8'><meta name='viewport' content='width=device-width, initial-scale=1'><link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css' /><link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css' />" +
                    //    "<style>.email-template .email-header{background-color:#346CB0;text-align:center}.email-body{padding:30px 15px 0 15px;border:1px solid #ccc}.email-body p{font-size:16px;font-weight:500}ul.simply-list{list-style-type:none;padding:0;margin:0}ul.simply-list .list-group-item{padding:2px 15px;font-weight:600;font-size:16px;line-height:1.4;border:none;background-color:transparent}ul.simply-list.no-border .list-group-item{border:none}.strip{padding:20px 15px;background-color:#eee;margin:15px 0}.strip ul.simply-list li:first-child{padding-left:0}.d-flex{display:flex;-webkit-display:flex;-moz-display:flex;-o-display:flex;flex-direction:row}.d-flex.align-items-center{align-items:center}.note{color:red;font-weight:500;font-size:12px;margin-bottom:10px}.email-template .email-header img{width:85px;display:block;margin:auto}.email-template .email-header span{font-size:22px;font-weight:500;padding:10px 0}.email-info{margin-top:10px;padding:10px 0}.info-list span{font-size:14px;display:flex;align-items:center;font-weight:600;color:#666}.info-list span a{color:#666}.info-list span i{font-size:18px;margin-right:5px;color:#888}@media only screen and (max-width:767px){.email-template .email-header{padding:10px 10px;text-align:center}.email-template .email-header img{width:70px;display:block;margin:auto;margin-bottom:5px}.email-template .email-header span{font-size:22px;font-weight:500}.strip ul.simply-list li{padding-left:0}.info-list li{line-height:1.9}}</style>" +
                    //   "<style>.form-group{margin-bottom:15px}.btn-success{color:#fff;background-color:#5cb85c;border-color:#4cae4c}.btn-success.focus,.btn-success:focus{color:#fff;background-color:#449d44;border-color:#255625}.btn-success:hover{color:#fff;background-color:#449d44;border-color:#398439}.btn-success.active,.btn-success:active,.open>.dropdown-toggle.btn-success{color:#fff;background-color:#449d44;background-image:none;border-color:#398439}.btn-success.active.focus,.btn-success.active:focus,.btn-success.active:hover,.btn-success:active.focus,.btn-success:active:focus,.btn-success:active:hover,.open>.dropdown-toggle.btn-success.focus,.open>.dropdown-toggle.btn-success:focus,.open>.dropdown-toggle.btn-success:hover{color:#fff;background-color:#398439;border-color:#255625}.btn-success.disabled.focus,.btn-success.disabled:focus,.btn-success.disabled:hover,.btn-success[disabled].focus,.btn-success[disabled]:focus,.btn-success[disabled]:hover,fieldset[disabled] .btn-success.focus,fieldset[disabled] .btn-success:focus,fieldset[disabled] .btn-success:hover{background-color:#5cb85c;border-color:#4cae4c}.text-success{color:#3c763d}a.text-success:focus,a.text-success:hover{color:#2b542c}.btn{margin:.2em .1em;font-weight:500;font-size:1em;-webkit-font-smoothing:antialiased;-moz-osx-font-smoothing:grayscale;border:none;border-bottom:.15em solid #000;border-radius:3px;padding:.65em 1.3em}.btn-primary{border-color:#2a6496;background-image:linear-gradient(#428bca,#357ebd)}.btn-primary:hover{background:linear-gradient(#357ebd,#3071a9)}</style>" +

                    //                         "<div class='container-fluid' style='padding-top:15px'>" +
                    //                         "<div class='row'>" +
                    //                         "<div class='col-sm-8 col-md-6 col-md-offset-3 col-sm-offset-2'>" +
                    //                         "<div class='email-template'>" +
                    //                         "<div class='email-header'>" +
                    //    //"<img alt='logo' src='http://erptest.sliit.lk/IMAGES/SLIIT_logo.png' />" +
                    //                         "<span style='color:#fff;font-weight:bold'>Sri Lanka Institute of Information Technology</span>" +
                    //                         "</div>" +
                    //                         "<div class='email-body' style='clear-both'>#content</div>" +


                    //"</div></div></div></div>" +
                    //"</body></html>";
                    //string message = "<h3>Dear <span></span>" + sudname + ", </h3>"
                    // + "<div class='strip'><p>Your Admission for SLIIT is " +  matter +", !! Your details are as below :"
                    // + "</br> <b>Application Number is :</b>" +  username + ""
                    // + "</br> <b>Name  :</b>"  +  sudname + ""
                    // + "</br> <b>Degree :</b>"  +  degreename + "</p></div>"
                    // +"<p class='note' style='margin-bottom:20px'>Note: This is system generated email. Please do not reply to this email.</p>";
                    //string Mailbody = message.ToString();
                    //string nMailbody = EmailTemplate.Replace("#content", Mailbody);
                    DataSet dsUserContact = null;
                    string message = "";
                    dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));
                    message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                    subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                    Execute(message, useremail, sudname, matter, subject, degreename, username, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
                    Clear();
                    txtApplNo.Enabled = true;
                    btnSubmit.Visible = false;
                    btnCancel.Visible = false;
                    divRemark.Visible = false;
                    divSuccess.Visible = false;
                    divPayment.Visible = false;
                    divDoc.Visible = false;
                    divdata.Visible = false;
                    divBoard.Visible = false;
                    divApprove.Visible = false;
                    txtApplNo.Text = string.Empty;
                    divimg.Visible = false;
                }
            }
        }
        catch
        {

        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DISTINCT DEGREENO", "DEGREENAME", "DEGREENO IN(" + Session["DEGREENO"].ToString() + ")", "DEGREENO");
        }
        else
        {
            ddlDegree.Items.Clear();
            ddlDegree.Items.Insert(0, new ListItem("Please Select", "0"));
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void rbApprove_CheckedChanged(object sender, EventArgs e)
    {
        if (rbApprove.Checked == true)
        {
            divRemark.Visible = false;
            txtRemark.Visible = false;
        }
        else
        {
            divRemark.Visible = true;
            txtRemark.Visible = true;
        }
        btnSubmit.Enabled = true;
    }

    protected void rbReject_CheckedChanged(object sender, EventArgs e)
    {
        if (rbReject.Checked == true)
        {
            divRemark.Visible = true;
            txtRemark.Visible = true;
        }
        else
        {
            divRemark.Visible = false;
            txtRemark.Visible = false;
        }
        btnSubmit.Enabled = true;
    }

    protected void rbHold_CheckedChanged(object sender, EventArgs e)
    {
        if (rbHold.Checked == true)
        {
            divRemark.Visible = true;
            txtRemark.Visible = true;
        }
        else
        {
            divRemark.Visible = false;
            txtRemark.Visible = false;
        }
        btnSubmit.Enabled = true;
    }

    private void Clear()
    {
        divRemark.Visible = false;
        rbApprove.Checked = false;
        rbReject.Checked = false;
        rbHold.Checked = false;
        rbMaha.Checked = false;
        rbOther.Checked = false;
        divPayment.Visible = false;
        divdata.Visible = false;
        divDoc.Visible = false;
        divApprove.Visible = false;
        btnSubmit.Enabled = false;
        ddlDegree.Items.Clear();
        ddlBranch.Items.Clear();
        ddlCollege.Items.Clear();
        ddlPaymentType.Items.Clear();
        lblAmt.Visible = false;
        ddlBranch.Items.Clear();
        ddlBranch.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlDegree.Items.Clear();
        ddlDegree.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlPaymentType.Items.Clear();
        ddlPaymentType.Items.Insert(0, new ListItem("Please Select", "0"));
        divBoard.Visible = false;
        rbApprove.Enabled = false;
        rbReject.Enabled = false;
        rbHold.Enabled = false;
        txtRemark.Text = string.Empty;
        DivApproveDegree.Visible = false;
        Session["Degreeno"] = null;
        Session["CollegeId"] = null;
        Session["User"] = null;
        Session["Branchno"] = null;
        Session["UsernameTEMP"] = null;
        Session["Mobile"] = null;
        Session["Email"] = null;
        Session["StudName"] = null;
        Session["ADMBATCH"] = null;
        Session["ADMISSION_STATUS"]= null;
        Session["ADMISSION_REMARK"]= null;
        Session["BOARD_STUDIED"] = null;
        Session["ACCOUNT_STATUS"] = null;
        Session["ACCOUNT_REMARK"] = null;
        Session["PTYPE"] = null;
        Session["urlAppno"] = null;
        foreach (ListViewItem lvitem in lvDocument.Items)
        {
            CheckBox chkVerified = lvitem.FindControl("chkVerified") as CheckBox;
            chkVerified.Checked = false;
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                ddlBranch.Items.Clear();
                DataSet dsBranch = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON A.BRANCHNO = B.BRANCHNO", "DISTINCT B.BRANCHNO", "B.LONGNAME", "A.DEGREENO=" + ddlDegree.SelectedValue, "");
                ddlBranch.DataSource = dsBranch;
                ddlBranch.DataTextField = "LONGNAME";
                ddlBranch.DataValueField = "BRANCHNO";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            else
            {
                ddlBranch.Items.Clear();
                ddlBranch.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }
        catch
        {

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        txtApplNo.Text = string.Empty;
        txtApplNo.Enabled = true;
        btnCancel.Visible = false;
        btnSubmit.Visible = false;
        divSuccess.Visible = false;
        divimg.Visible = false;
    }

    protected void rbMaha_CheckedChanged(object sender, EventArgs e)
    {
        ddlPaymentType.SelectedIndex = 0;
        lblAmt.Visible = false;
        rbApprove.Enabled = false;
        rbReject.Enabled = false;
        rbHold.Enabled = false;
        btnSubmit.Enabled = false;
        rbApprove.Checked = false;
        rbReject.Checked = false;
        rbHold.Checked = false;
        divRemark.Visible = false;
        txtRemark.Text = string.Empty;
        if (rbMaha.Checked == true || rbOther.Checked == true)
        {
            //objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "DISTINCT PAYTYPENO", "PAYTYPENAME", "", "PAYTYPENO");
        }
    }

    protected void rbOther_CheckedChanged(object sender, EventArgs e)
    {
        ddlPaymentType.SelectedIndex = 0;
        lblAmt.Visible = false;
        rbApprove.Enabled = false;
        rbReject.Enabled = false;
        rbHold.Enabled = false;
        rbApprove.Checked = false;
        rbReject.Checked = false;
        rbHold.Checked = false;
        divRemark.Visible = false;
        txtRemark.Text = string.Empty;
        btnSubmit.Enabled = false;
        if (rbMaha.Checked == true || rbOther.Checked == true)
        {
            //objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "DISTINCT PAYTYPENO", "PAYTYPENAME", "", "PAYTYPENO");
        }
    }

    public void SendSMS(string Mobile, string text)
    {
        string status = "";
        try
        {
            string Message = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?"));
                request.ContentType = "text/xml; charset=utf-8";
                request.Method = "POST";

                string postDate = "ID=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                postDate += "&";
                postDate += "Pwd=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                postDate += "&";
                postDate += "PhNo=91" + Mobile;
                postDate += "&";
                postDate += "Text=" + text;

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
                request.ContentType = "application/x-www-form-urlencoded";

                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse _webresponse = request.GetResponse();
                dataStream = _webresponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                status = reader.ReadToEnd();
            }
            else
            {
                status = "0";
            }
        }
        catch
        {

        }
    }

    //public void sendmail(string toEmailId, string Sub, string body)
    //{
    //    try
    //    {
    //        DataSet dsconfig = null;
    //        dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
    //        var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString(), "");
    //        var toAddress = new MailAddress(toEmailId, "");
    //        // string fromPassword = clsTripleLvlEncyrpt.ThreeLevelDecrypt(Session["EMAILSVCPWD"].ToString());
    //        string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
    //        var smtp = new SmtpClient
    //        {
    //            Host = "smtp.gmail.com",
    //            Port = 587,
    //            EnableSsl = true,
    //            DeliveryMethod = SmtpDeliveryMethod.Network,
    //            UseDefaultCredentials = false,
    //            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
    //        };
    //        using (var message = new MailMessage(fromAddress, toAddress)
    //        {
    //            Subject = Sub,
    //            Body = body,
    //            BodyEncoding = System.Text.Encoding.UTF8,
    //            SubjectEncoding = System.Text.Encoding.Default,
    //            IsBodyHtml = true
    //        })
    //        {
    //            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
    //            smtp.Send(message);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message);
    //    }
    //}

    protected void lnkView_Click(object sender, EventArgs e)
    {
        
        LinkButton lnkView = (LinkButton)(sender);
        string docName =lnkView.CommandArgument;
        string docPath = Server.MapPath("~/ONLINEIMAGESUPLOAD\\");
        //string docPath = System.Configuration.ConfigurationManager.AppSettings["path"];
        string fileName = docPath.ToString() + "\\" + docName.ToString(); ;
        byte[] fileContent = null;
        if (File.Exists(fileName))
        {
            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            long byteLength = new System.IO.FileInfo(fileName).Length;
            fileContent = br.ReadBytes((Int32)byteLength);
            Byte[] bytes = br.ReadBytes((Int32)fs.Length);
            string base64String = Convert.ToBase64String(fileContent, 0, Convert.ToInt32(byteLength));
            string mimetype = GetContentType(fileName);
            string pdfIFrameSrc = "data:" + mimetype.ToString() + ";base64," + Convert.ToString(base64String) + "";
            //  data:application/pdf;base64,JVBERi0xLjcgCiXi48/TIAoxIDAgb2JqIAo8PCAK
            //   data:application/pdf;base64,JVBERi0xLjcKJcKzx9gNCjEgMCBvYmoNPDwvTm
            iframe1.Attributes.Add("src", pdfIFrameSrc);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
        }
        else
        {
            objCommon.DisplayMessage(updAdmissionDetails, "Sorry, File not available on this machine!", this.Page);
        }

        //if (hdnfldVariable.Value != string.Empty || hdnfldVariable.Value != "" || hdnfldVariable.Value != null)
        //{
        //    string docPath = System.Configuration.ConfigurationManager.AppSettings["path"];
        //    docPath = docPath.Replace("\\", "/");
        //    Process.Start(hdnfldVariable.Value, string.Format("\"{0}\"", @"file:///" + docPath.ToString() + docName.ToString()));
        //}

        //iframe1.Attributes.Add("src", lnkView.CommandArgument);
        //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);

        //string fileName = "E:\\DOCSTORAGE\\HSNCU_Document\\1234.pdf";
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReportUserInfo("Admission Detail Report", "UserInfo.rpt");
    }

    private void ShowReportUserInfo(string reportTitle, string rptFileName)
    {
        try
        {
            int collegeid = Session["CollegeId"].ToString() == string.Empty ? 0 : Convert.ToInt32(Session["CollegeId"].ToString());
            int degreeno = Session["Degreeno"].ToString() == string.Empty ? 0 : Convert.ToInt32(Session["Degreeno"].ToString());
            int branchno = Session["Branchno"].ToString() == string.Empty ? 0 : Convert.ToInt32(Session["Branchno"].ToString());
            int userno = Session["User"].ToString() == string.Empty ? 0 : Convert.ToInt32(Session["User"].ToString());

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("ac")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_USERNO=" + userno + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_COLLEGE_ID=" + collegeid + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno;
            //url += "&param=@P_USERNO=" + userno + ",@P_COLLEGE_ID=" + collegeid + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno;

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            objCommon = new Common();
            objCommon.DisplayMessage(updAdmissionDetails, ex.Message.ToString(), this);
        }
    }

    public void TransferToEmail(string toEmailId, string Sub, string sudname, string degreename, string matter, string userno)
    {
        try
        {
            //DataSet ds = objCommon.FillDropDown("REFF", "MASTERSOFT_GRID_MAILID", "MASTERSOFT_GRID_USERNAME,MASTERSOFT_GRID_PASSWORD", "MASTERSOFT_GRID_MAILID <> '' and MASTERSOFT_GRID_PASSWORD<> '' and MASTERSOFT_GRID_USERNAME <> ''", string.Empty);
            //var fromAddress = ds.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString();
            //var toAddress = toEmailId;
            //string fromPassword = ds.Tables[0].Rows[0]["MASTERSOFT_GRID_PASSWORD"].ToString();
            //string userId = ds.Tables[0].Rows[0]["MASTERSOFT_GRID_USERNAME"].ToString();

            //DataSet ds = objCommon.FillDropDown("REFF", "MASTERSOFT_GRID_MAILID", "MASTERSOFT_GRID_USERNAME,MASTERSOFT_GRID_PASSWORD,COMPANY_EMAILSVCID,SENDGRID_USERNAME,SENDGRID_PWD", "MASTERSOFT_GRID_MAILID <> '' and MASTERSOFT_GRID_PASSWORD<> '' and MASTERSOFT_GRID_USERNAME <> ''", string.Empty);
            //var fromAddress = ds.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString();//added by pankaj nakhale 23102020 bcz of credential changed.
            //var toAddress = toEmailId;
            //string fromPassword = ds.Tables[0].Rows[0]["SENDGRID_PWD"].ToString();//added by pankaj nakhale 23102020 bcz of credential changed.
            //string userId = ds.Tables[0].Rows[0]["SENDGRID_USERNAME"].ToString();//added by pankaj nakhale 23102020 bcz of credential changed.


            //MailMessage msg = new MailMessage();
            //SmtpClient smtp = new SmtpClient();

            //msg.From = new MailAddress(fromAddress, Sub);
            //msg.To.Add(new MailAddress(toAddress));

            //msg.Subject = "Admission Status for applied degree";

            //using (StreamReader reader = new StreamReader(Server.MapPath("~/email_template_conformed_Adm.html")))
            //{
            //    msg.Body = reader.ReadToEnd();
            //}



            //msg.Body = msg.Body.Replace("{Name}", sudname.ToString());
            //msg.Body = msg.Body.Replace("{confirmed}", matter.ToString());
            //msg.Body = msg.Body.Replace("{ApplicationNo}", userno.ToString());
            //msg.Body = msg.Body.Replace("{Degree}", degreename.ToString());
            ////msg.Body = msg.Body.Replace("{FeesPaid}", matter.ToString());




            //msg.IsBodyHtml = true;
            ////smtp.enableSsl = "true";            
            //smtp.Host = "smtp.sendgrid.net";
            //smtp.Port = 587;
            //smtp.UseDefaultCredentials = false;
            //smtp.EnableSsl = true;
            //smtp.Credentials = new NetworkCredential(userId, fromPassword);

            //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            //smtp.Send(msg);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PresentationLayer_NewRegistration.TransferToEmail-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //updemo.Update();
        lvDocument.DataSource = null;
        lvDocument.DataBind();
        try
        {
            ImageButton btnDelete = sender as ImageButton;
  
            string filename = string.Empty;

            string path = Server.MapPath("~/ONLINEIMAGESUPLOAD\\");           
            //int userno= Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION","USERNO","USERNAME=" + txtApplNo.Text));
            //int uno = Convert.ToInt32(txtApplNo.Text.Trim());
            int dno = Convert.ToInt32(btnDelete.CommandArgument);
            filename = btnDelete.AlternateText.ToString();
            objCommon.DeleteDocumentOtherInfo(dno, ViewState["userno"]);
            objCommon.DeleteDocumentLog(ViewState["userno"], Session["userno"], filename);
            objCommon.DisplayMessage(updAdmissionDetails, "File Deleted Successfully!", this.Page);
            BindDocuments();
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "domain.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //    public void TransferToEmail(string toEmailId, string Sub)
    //    {
    //        try
    //        {
    //            DataSet ds = objCommon.FillDropDown("REFF", "MASTERSOFT_GRID_MAILID", "MASTERSOFT_GRID_USERNAME,MASTERSOFT_GRID_PASSWORD", "MASTERSOFT_GRID_MAILID <> '' and MASTERSOFT_GRID_PASSWORD<> '' and MASTERSOFT_GRID_USERNAME <> ''", string.Empty);

    //            var fromAddress = ds.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString();
    //            var toAddress = toEmailId;

    //            string fromPassword = ds.Tables[0].Rows[0]["MASTERSOFT_GRID_PASSWORD"].ToString();
    //            string userId = ds.Tables[0].Rows[0]["MASTERSOFT_GRID_USERNAME"].ToString();


    //            MailMessage msg = new MailMessage();
    //            SmtpClient smtp = new SmtpClient();

    //            msg.From = new MailAddress(fromAddress, Sub);
    //            msg.To.Add(new MailAddress(toAddress));

    //                msg.Subject = "Admission Status for applied degree";

    //               // string EmailTemplate = body;
    //                StringBuilder mailBody = new StringBuilder();
    //                //mailBody.AppendFormat("<h1>Greetings !!</h1>");

    //                mailBody.AppendFormat("Dear " + ViewState["StudentName"].ToString() + "Your application for admission in " + ViewState["DegreeName"].ToString() + ", had been " + ViewState["Message"].ToString() + ". Thanks and Regards, <b> Admission Team </b><br/><b> HSNC Univeristy, Mumbai</b>");

    //                msg.IsBodyHtml = true;

    //            //Host = "smtp.gmail.com",
    //            smtp.Host = "smtp.sendgrid.net";
    //            smtp.Port = 587;
    //            smtp.EnableSsl = true;
    //            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
    //            smtp.UseDefaultCredentials = false;
    //            smtp.Credentials = new NetworkCredential(userId, fromPassword);

    //            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
    //            smtp.Send(msg);

    //            ////smtp.Host = "smtp.gmail.com";
    //            ////smtp.Port = 587;
    //            ////smtp.UseDefaultCredentials = true;
    //            ////smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
    //            ////smtp.EnableSsl = true;
    //            ////smtp.Send(msg);

    //            /* Sending Sms - Without resp.*/
    //            //objUC.NewRegisSMS(username, password, MOBILENO.Trim());
    //        }
    //        catch (Exception ex)
    //        {
    //            if (Convert.ToBoolean(Session["error"]) == true)
    //                objCommon.ShowError(Page, "PresentationLayer_NewRegistration.TransferToEmail-> " + ex.Message + " " + ex.StackTrace);
    //            else
    //                objCommon.ShowError(Page, "Server UnAvailable");
    //        }
    //    }

    private void SearchStudent(string AppNo)
    {
        txtApplNo.Text = AppNo.ToString();
        DataSet dsStudInfo = objCommon.FillDropDown("ACD_USER_PROFILE_STATUS A INNER JOIN ACD_USER_BRANCH_PREF B ON A.USERNO = B.USERNO INNER JOIN ACD_COLLEGE_MASTER C ON (B.COLLEGE_ID=C.COLLEGE_ID)", "B.COLLEGE_ID", "C.CODE", "A.USERNO = " + txtApplNo.Text + " AND ISNULL(Applied,0)=1", "");
        string[] ua_college_id = Session["college_nos"].ToString().Split(',');
        int ApprovalAutho = 0;
        for (int i = 0; i < ua_college_id.Length; i++)
        {
            if (ua_college_id[i].ToString() == dsStudInfo.Tables[0].Rows[0]["COLLEGE_ID"].ToString())
            {
                ApprovalAutho = 0; //match authorization
                break;
            }
            else
            {
                ApprovalAutho = 1;//not match authorizationddlCollege
            }
        }

        if (ApprovalAutho == 1)
        {
            objCommon.DisplayMessage(updAdmissionDetails, "Sorry Authorization is not allow for " + dsStudInfo.Tables[0].Rows[0]["CODE"], this.Page);
            return;
        }

        Clear();
        DivApproveDegree.Visible = true;
        if (txtApplNo.Text != string.Empty || txtApplNo.Text != "")
        {
            string UserNo = txtApplNo.Text.Trim();

            string RecExists = objCommon.LookUp("ACD_USER_PROFILE_STATUS A INNER JOIN ACD_USER_BRANCH_PREF B ON A.USERNO = B.USERNO", "COUNT(*)", "A.USERNO = " + txtApplNo.Text + " AND ISNULL(Applied,0)=1");

            if (!string.IsNullOrEmpty(RecExists))
            {
                if (Convert.ToInt32(RecExists) > 0)
                {
                    divPayment.Visible = true;
                    divdata.Visible = true;
                    divApprove.Visible = true;
                    GetCandidateDetails(txtApplNo.Text);
                    GetApplicationCollegeDegreeBranchDetails(txtApplNo.Text);
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                    divSuccess.Visible = true;

                    if (Session["ADMISSION_STATUS"].ToString() != "1")
                    {
                        if (Session["ACCOUNT_STATUS"].ToString() != "1" || Session["ACCOUNT_STATUS"].ToString() != "2" || Session["ACCOUNT_STATUS"].ToString() != "3")
                        {
                            divApprove.Visible = true;
                            rbApprove.Visible = true;
                            rbHold.Visible = true;
                            rbReject.Visible = true;
                            rbApprove.Enabled = true;
                            rbHold.Enabled = true;
                            rbReject.Enabled = true;
                            btnSubmit.Enabled = true;
                        }
                        else
                        {
                            objCommon.DisplayMessage(updAdmissionDetails, "Application for admission already approved by admission section!", this.Page);
                        }
                    }
                }
                else if (Convert.ToInt32(RecExists) == 0)
                {
                    string checkConfirmStatus = objCommon.LookUp("ACD_USER_PROFILE_STATUS ", "COUNT(*)", "USERNO = " + txtApplNo.Text + "");
                    if (!string.IsNullOrEmpty(checkConfirmStatus))
                    {
                        if (Convert.ToInt32(checkConfirmStatus) > 0)
                        {
                            objCommon.DisplayMessage(updAdmissionDetails, "Application Not Confirmed!", this.Page);
                            btnCancel.Visible = false;
                            btnSubmit.Visible = false;
                            Clear();
                            divimg.Visible = false;
                        }
                        else
                        {
                            objCommon.DisplayMessage(updAdmissionDetails, "Application No. not found!", this.Page);
                            btnCancel.Visible = false;
                            btnSubmit.Visible = false;
                            Clear();
                            divimg.Visible = false;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updAdmissionDetails, "Application No. not found!", this.Page);
                        btnCancel.Visible = false;
                        btnSubmit.Visible = false;
                        Clear();
                        divimg.Visible = false;
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(updAdmissionDetails, "Application No. not found!", this.Page);
                btnCancel.Visible = false;
                btnSubmit.Visible = false;
                Clear();
                divimg.Visible = false;
            }

            string AdmExist = objCommon.LookUp("ACD_STUDENT", "COUNT(*)", "USERNO = " + txtApplNo.Text + "");
            if (!string.IsNullOrEmpty(AdmExist))
            {
                if (Convert.ToInt32(AdmExist) > 0)
                {
                    if (Session["ADMISSION_STATUS"].ToString() != "1")
                    {
                        objCommon.DisplayMessage(updAdmissionDetails, "Applicant data already transfered in ERP, but admission status can be updated!", this.Page);
                        btnSubmit.Enabled = true;
                        btnCancel.Enabled = true;
                        btnSubmit.Visible = true;
                        btnCancel.Visible = true;
                        divSuccess.Visible = true;
                    }
                    else
                    {
                        objCommon.DisplayMessage(updAdmissionDetails, "Applicant data already transfered in ERP!", this.Page);
                        txtApplNo.Enabled = true;
                        divDoc.Visible = true;
                    }
                }
            }

        }
        else
        {
            objCommon.DisplayMessage(updAdmissionDetails, "Please Enter Application No", this.Page);
            btnCancel.Visible = false;
            btnSubmit.Visible = false;
            Clear();
            divimg.Visible = false;
        }

    }

    private string GetContentType(string fileName)
    {
        string strcontentType = "application/octetstream";
        string ext = System.IO.Path.GetExtension(fileName).ToLower();
        Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
        if (registryKey != null && registryKey.GetValue("Content Type") != null)
            strcontentType = registryKey.GetValue("Content Type").ToString();
        return strcontentType;
    }

    static async Task Execute(string message, string toEmailId, string sudname, string matter, string subject, string degreename, string username, string sendemail, string emailpass)
    {
      
        try
        {
            SmtpMail oMail = new SmtpMail("TryIt");

            oMail.From = sendemail;

            oMail.To = toEmailId;

            oMail.Subject = subject;

            oMail.HtmlBody = message;
            oMail.HtmlBody = oMail.HtmlBody.Replace("[UA_FULLNAME]", sudname.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[MATTER]", matter.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[Application ID]", username.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[PROGRAM NAMES]", degreename.ToString());
            //oMail.HtmlBody = oMail.HtmlBody.Replace("[INTAKE]", usernoo.ToString());
          
            
            SmtpServer oServer = new SmtpServer("smtp.live.com");
            oServer.User = sendemail;
            oServer.Password = emailpass;

            oServer.Port = 587;

            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();

            oSmtp.SendMail(oServer, oMail);

      
        }
        catch (Exception ex)
        {

        }
    }
    protected void GetPayment()
    {
        int payment = 1; 
        string amount = objCommon.LookUp("ACD_STANDARD_FEES", "SUM(SEMESTER1)", "DEGREENO=" + Convert.ToInt32(Session["Degreeno"].ToString()) + " AND COLLEGE_ID=" + Convert.ToInt32(Session["CollegeId"].ToString()) + " AND (BRANCHNO=" + Convert.ToInt32(Session["Branchno"].ToString()) + " OR BRANCHNO = 0)  AND BATCHNO=" + Convert.ToInt32(Session["ADMBATCH"].ToString()) + " AND PAYTYPENO=" + Convert.ToInt32(payment)) == string.Empty ? "0" : objCommon.LookUp("acd_standard_fees", "SUM(SEMESTER1)", "DEGREENO=" + Convert.ToInt32(Session["Degreeno"].ToString()) + " AND COLLEGE_ID=" + Convert.ToInt32(Session["CollegeId"].ToString()) + " AND PAYTYPENO=" + Convert.ToInt32(payment) + " AND (BRANCHNO=" + Convert.ToInt32(Session["Branchno"].ToString()) + " OR BRANCHNO = 0) AND BATCHNO=" + Convert.ToInt32(Session["ADMBATCH"].ToString()) + " AND RECIEPT_CODE='TF'");

        if (amount == "0" || amount == "")
        {
           
            objCommon.DisplayMessage(updAdmissionDetails, "Fees not defined for the selected payment type! Please define the fees!", this.Page);
            btnSubmit.Enabled = false;         
            return;
        }
        else
        {
            if (rbMaha.Checked == true)
            {
                int amt = 0;

                string verifyfees = objCommon.LookUp("ACD_STANDARD_FEES", "SUM(SEMESTER1)", "DEGREENO=" + Convert.ToInt32(Session["Degreeno"].ToString()) + " AND COLLEGE_ID=" + Convert.ToInt32(Session["CollegeId"].ToString()) + " AND PAYTYPENO=" + Convert.ToInt32(ddlPaymentType.SelectedValue) + " AND BATCHNO=" + Convert.ToInt32(Session["ADMBATCH"].ToString()) + " AND (BRANCHNO=" + Convert.ToInt32(Session["Branchno"].ToString()) + " OR BRANCHNO = 0) AND FEE_HEAD='F25'") == string.Empty ? "0" : objCommon.LookUp("acd_standard_fees", "SUM(SEMESTER1)", "DEGREENO=" + Convert.ToInt32(Session["Degreeno"].ToString()) + " AND COLLEGE_ID=" + Convert.ToInt32(Session["CollegeId"].ToString()) + " AND PAYTYPENO=" + Convert.ToInt32(ddlPaymentType.SelectedValue) + " AND BATCHNO=" + Convert.ToInt32(Session["ADMBATCH"].ToString()) + " AND (BRANCHNO=" + Convert.ToInt32(Session["Branchno"].ToString()) + " OR BRANCHNO = 0) AND FEE_HEAD='F25'");

                amount = amount.Substring(0, amount.IndexOf("."));
                verifyfees = verifyfees.Substring(0, verifyfees.IndexOf("."));

                amt = Convert.ToInt32(amount) - Convert.ToInt32(verifyfees);
                if (amt == 0)
                {
                    amount = "Not Applicable";
                }
                amount = Convert.ToString(amt) + ".00";
            }

            lblAmt.Text = "Paid Fees: Rs. " + amount;
            lblAmt.Visible = true;
        }
    }
    #region commented
    //private void CreateDemand()
    //{
    //    try
    //    {
    //        FeeDemand feeDemand = new FeeDemand();
    //        FeeCollectionController feeController = new FeeCollectionController();
    //        int boardstudied = 0;
    //        DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "USERNO","DEGREENO,COLLEGE_ID,BRANCHNO,ADMBATCH,*", "USERNO=" + Convert.ToInt32(ViewState["userno"]),"");
    //        feeDemand.StudentId = Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString());
    //        feeDemand.StudentName = Session["StudName"].ToString();
    //        feeDemand.EnrollmentNo = txtApplNo.Text.Trim();
    //        feeDemand.SessionNo = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "FLOCK=1") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "FLOCK=1"));

    //        feeDemand.BranchNo = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"].ToString());
    //        feeDemand.DegreeNo = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"].ToString());
    //        feeDemand.SemesterNo = Convert.ToInt32(ds.Tables[0].Rows[0]["SEMESTERNO"].ToString());
    //        feeDemand.AdmBatchNo = Convert.ToInt32(ds.Tables[0].Rows[0]["ADMBATCH"].ToString());
    //        feeDemand.ReceiptTypeCode = "TF";
    //        feeDemand.PaymentTypeNo = Convert.ToInt32(ddlPaymentType.SelectedValue);
    //        feeDemand.CounterNo = objCommon.LookUp("ACD_COUNTER_REF", "TOP 1 COUNTERNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_COUNTER_REF", "TOP 1 COUNTERNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())));
    //        feeDemand.UserNo = ((Session["userno"].ToString() != string.Empty) ? Convert.ToInt32(Session["userno"].ToString()) : 0);
    //        feeDemand.CollegeCode = ((Session["colcode"].ToString() != string.Empty) ? Session["colcode"].ToString() : string.Empty);
    //        int paymentTypeNoOld = objCommon.LookUp("ACD_DEMAND", "DISTINCT PAYTYPENO", "IDNO=" + Convert.ToInt32(Session["Idno"].ToString()) + " AND SEMESTERNO=" + 1 + " AND PAYTYPENO =" + Convert.ToInt32(ddlPaymentType.SelectedValue) + " AND ADMBATCHNO=" + Convert.ToInt32(Session["ADMBATCH"].ToString()) + "and RECIEPT_CODE='TF'") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "DISTINCT PAYTYPENO", "IDNO=" + Convert.ToInt32(Session["Idno"].ToString()) + " AND SEMESTERNO=" + 1 + " AND PAYTYPENO =" + Convert.ToInt32(ddlPaymentType.SelectedValue) + " AND ADMBATCHNO=" + Convert.ToInt32(Session["ADMBATCH"].ToString()) + "and RECIEPT_CODE='TF'"));

    //        if (rbMaha.Checked == true)
    //        {
    //            boardstudied = 1;
    //        }
    //        else
    //        {
    //            boardstudied = 2;
    //        }

    //        Boolean csdemand = feeController.CreateNewDemandAdmission(feeDemand, paymentTypeNoOld, boardstudied);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_FeeCollection.CreateDemandForCurrentFeeCriteria() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    #endregion
}