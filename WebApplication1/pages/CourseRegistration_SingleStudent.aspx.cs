//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : COURSE REGISTRATION FOR [CBCS] STUDENT                                    
// CREATION DATE : 15-OCT-2011
// ADDED BY      : RAJU BITODE                                                
// ADDED DATE    : 11-MAY-2019 
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================

using System;
using System.Data;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class ACADEMIC_CourseRegistration_SingleStudent : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();

    #region Page Load
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
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

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
                this.CheckPageAuthorization();

                //Check for Activity On/Off***
                if (CheckActivity() == false)
                {
                    return;
                }
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                this.PopulateDropDownList();
                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                string IPADDRESS = string.Empty;

                IPADDRESS = ip.AddressList[1].ToString();
                ViewState["ipAddress"] = IPADDRESS;
                //check activity for Subject registration.

                btnPrePrintClallan.Visible = false;
                if (Session["usertype"].ToString().Equals("2"))     //Student 
                {
                    divbody.Visible = true;
                    //btnRedirect.Visible = false;
                    trSession_name.Visible = false;
                    trRollNo.Visible = false;
                    btnShow.Visible = false;
                    btnCancel.Visible = false;
                    txtRollNo.Text = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO=" + Session["idno"].ToString());
                    ddlSession.SelectedIndex = 1;

                    ViewState["idno"] = Session["idno"].ToString();

                    string count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "ISNULL(CANCEL,0)=0 AND IDNO=" + Session["idno"].ToString() + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                    if (count != "0")
                    {
                        objCommon.DisplayMessage(this.updDiv, "Subject Registration already done..You can Generate only Registration Slip..!!", this.Page);
                        divdisplayHide();
                        btnSubmit.Enabled = false;
                        btnPrintRegSlip.Enabled = true;
                        lvDetained.Enabled = false;
                        lvCurrentSubjects.Enabled = false;
                        //lvElectiveCourse.Enabled = false;
                        ddlCoreSection.Enabled = false;
                    }
                    else
                    {
                        string chk = objCommon.LookUp("ACD_SEM_PROMOTION", "COUNT(1)", "IDNO=" + Session["idno"].ToString() + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                        if (Convert.ToInt32(chk) >= 1)// already pramoted
                        {
                            divdisplayHide();
                        }
                        btnSubmit.Enabled = true;
                        //btnDropTerm.Enabled = true;
                    }
                }
                else
                {
                    divSemPromotion.Visible = false;
                    txtRollNo.Text = string.Empty;
                    //btnRedirect.Visible = true;
                    divbody.Visible = true;
                    PopulateDropDownList();

                    if (Request.QueryString["regno"] != null)
                    {
                        txtRollNo.Text = Request.QueryString["regno"].ToString();
                        ddlSession.SelectedIndex = 1;
                    }
                }
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND FLOCK=1", "SESSIONNO DESC");
        ddlSession.Focus();
    }
    protected void ACD_SESSION_TYPE_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CourseRegistration_SingleStudent.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CourseRegistration_SingleStudent.aspx");
        }
    }

    private void ShowTRReport(string reportTitle, string rptFileName, string sessionNo, string schemeNo, string semesterNo, string idNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + sessionNo + ",@P_SCHEMENO=" + schemeNo + ",@P_SEMESTERNO=" + semesterNo + ",@P_IDNO=" + idNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private bool CheckActivity()
    {
        string sessionno = string.Empty;
        //added for get student releted data
        DataSet dsChkElg = objCommon.FillDropDown("ACD_STUDENT", "ISNULL(SEMESTERNO,0)SEMESTERNO", "ISNULL(DEGREENO,0)DEGREENO,ISNULL(BRANCHNO,0)BRANCHNO,ISNULL(COLLEGE_ID,0)COLLEGE_ID", "IDNO=" + Session["idno"].ToString(), "");

        if (dsChkElg.Tables[0].Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(updDiv, updDiv.GetType(), "Alert", "alert('No Students Found !');", true);
            return false;
        }
        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.ACTIVITY_CODE = 'EXAMREG' AND SA.STARTED = 1");
        sessionno = Session["currentsession"].ToString();
        ActivityController objActController = new ActivityController();
        //DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), dsChkElg.Tables[0].Rows[0]["DEGREENO"].ToString(), dsChkElg.Tables[0].Rows[0]["BRANCHNO"].ToString(), dsChkElg.Tables[0].Rows[0]["SEMESTERNO"].ToString());
        
        bool flag = false;
        if(dtr.HasRows)
        {
            if (dtr.Read())
            {
                if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                {
                    objCommon.DisplayMessage(this.updDiv, "This Activity has been Stopped..!!", this.Page);
                    //divCourses.Visible = false;
                    flag = false;
                }

                if (dtr["STARTED"].ToString().ToLower().Equals("true"))
                {
                    flag = true;
                }

                //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                {
                    objCommon.DisplayMessage(this.updDiv, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                    //divCourses.Visible = false;
                    flag = false;
                }
            }
            else
            {
                // objCommon.DisplayMessage(this.updDiv,"Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                objCommon.DisplayMessage(this.updDiv, "This Activity has been Stopped..!!", this.Page);
                //divCourses.Visible = false;
                flag = false;
            }
        }
        dtr.Close();
        return flag;
    }
    #endregion Page Load

    #region Show Details
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (txtRollNo.Text == string.Empty)
        {
            tblInfo.Visible = false;
            objCommon.DisplayMessage(this.updDiv, "Please Enter Registration  No...!!", this.Page);
            return;
        }
        DataSet DsInfo = objCommon.FillDropDown("ACD_STUDENT", "IDNO,SEMESTERNO", "FAC_ADVISOR", "ENROLLNO = '" + txtRollNo.Text.Trim() + "'", "");

        if (DsInfo.Tables[0].Rows.Count == 0)
        {
            tblInfo.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script", "alert('Student Not Found for This Registration No.'); location.href='CourseRegistration_SingleStudent.aspx?pageno=754';", true);
            return;
        }
        ViewState["idno"] = DsInfo.Tables[0].Rows[0]["IDNO"].ToString();
        string count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(*)", "IDNO=" + ViewState["idno"].ToString() + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND CANCEL IS NULL");

        if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
        {
            if (DsInfo.Tables[0].Rows[0]["FAC_ADVISOR"].ToString() != Session["userno"].ToString())
            {
                objCommon.DisplayMessage(this.updDiv, "Your not Authorized faculty to View Subject Registration of this Student", this.Page);
                return;
            }
        }

        if (count == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script", "alert('Yet Subject Registration havent done  by Student.'); location.href='CourseRegistration_SingleStudent.aspx?pageno=755';", true);
        }
        if (count != "0")
        {
            divdisplayHide();
            btnPrintRegSlip.Enabled = true;
        }
        else
        {
            this.ShowDetails();
            btnSubmit.Enabled = true;
            //btnDropTerm.Enabled = true;
        }
    }
    private void ShowDetails()
    {
        try
        {
            string accept = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(ACCEPTED)", "IDNO=" + ViewState["idno"].ToString() + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(CANCEL,0)=0 AND ACCEPTED=1");

            ViewState["ACCEPT"] = accept;
            lvDetained.Enabled = true;
            //lvElectiveCourse.Enabled = true;
            txtCredits.Text = "0";
            txtAllSubjects.Text = "0";


            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SECTION SE ON (SE.SECTIONNO = s.SECTIONNO)", "S.IDNO,S.ROLLNO,DG.DEGREENAME,SECTIONNAME,s.SECTIONNO", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO as ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE ISNULL(S.PHYSICALLY_HANDICAPPED,0) WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH,IDTYPE,(CASE ISNULL(S.IDTYPE,0) WHEN '3' THEN 'DIRECT SECOND YEAR ' ELSE  'REGULAR'   END) AS STUD_TYPE", "S.IDNO=" + ViewState["idno"].ToString() + "", string.Empty);

            int adm_type = 1;
            if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()) == 2) //2 direct second year
            {
                adm_type = 2;
            }

            //DataSet dsBackCourse = null;
            //dsBackCourse = objSReg.getBacklogCourses(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["idno"].ToString()), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString()), Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString()));
            DataSet dsDetained = null;
            dsDetained = objSReg.getDetainCourses(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["idno"].ToString()), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString()), Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString()));

            int student_type = 1;//1 for All Pass
            //if ((dsBackCourse.Tables[0].Rows.Count + dsDetained.Tables[0].Rows.Count) > 0)
            //{
            //    student_type = 2; //for backlog
            //}

            string cgpa = objCommon.LookUp("ACD_TRRESULT a INNER JOIN ACD_STUDENT S ON(A.IDNO=S.IDNO)", "CGPA", "S.ENROLLNO='" + txtRollNo.Text.Trim() + "'  and sessionno=(select max(sessionno) from ACD_TRRESULT b  where  S.IDNO=B.IDNO and a.SEMESTERNO=b.SEMESTERNO) and A.SEMESTERNO=(select max(SEMESTERNO) from ACD_TRRESULT Ab  where  S.IDNO=AB.IDNO) ");
            if (cgpa == string.Empty)
            {
                cgpa = "0";
            }

            #region Commented --------==========-----------
            DataSet dsCreditInfo = null;
            dsCreditInfo = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT", "FROM_CREDIT,TO_CREDIT", "MIN_SCHEME_LIMIT,MAX_SCHEME_LIMIT,MIN_REG_CREDIT_LIMIT,CREDIT_LIMIT_STATUS", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "AND STUDENT_TYPE =" + student_type + "AND DEGREENO =" + dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND ADM_TYPE=" + adm_type + "AND " + dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() + "IN (select VALUE FROM DBO.SPLIT( SEMESTER,','))", "");

            if (dsCreditInfo.Tables[0].Rows.Count > 0)
            {
                ViewState["minRegCreditLimit"] = dsCreditInfo.Tables[0].Rows[0]["MIN_REG_CREDIT_LIMIT"].ToString();

                ViewState["CREDIT_LIMIT_STATUS"] = dsCreditInfo.Tables[0].Rows[0]["CREDIT_LIMIT_STATUS"].ToString();

                lblMinCreditLimit.Text = "0"; //Convert.ToString(dsCreditInfo.Tables[0].Rows[0]["FROM_CREDIT"].ToString());

                lblMaxCreditlimit.Text = "0"; //Convert.ToString(dsCreditInfo.Tables[0].Rows[0]["TO_CREDIT"].ToString());

                #region start
                //    if (dsCreditInfo.Tables[0].Rows[0]["ADDITIONAL_COURSE"].ToString() == "1")//true
                //    {
                //        if (dsCreditInfo.Tables[0].Rows[0]["DEGREE_TYPE"].ToString() == "1")//1 for UG//2 for ug+pg
                //        {
                //            divAdditionalUg.Visible = true;
                //            divAdditionalPg.Visible = false;
                //        }
                //        else
                //        {
                //            divAdditionalUg.Visible = true;
                //            divAdditionalPg.Visible = true;
                //        }
                //    }
                //    else
                //    {
                //        divAdditionalUg.Visible = false;
                //        divAdditionalPg.Visible = false;
                //    }


                //#region start  MIN_SCHEME_LIMIT
                //if (dsCreditInfo.Tables[0].Rows[0]["MIN_SCHEME_LIMIT"].ToString() == "1")
                //{
                //    DataSet dsMinSchemeInfo = objSReg.getCreditCountOfScheme(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString()), Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString()), Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString()));
                //    decimal creditTotal = 0;

                //    if (dsMinSchemeInfo.Tables[0].Rows.Count > 0)
                //    {
                //        for (int i = 0; i < dsMinSchemeInfo.Tables[0].Rows.Count; i++)
                //        {
                //            if (dsMinSchemeInfo.Tables[0].Rows[i]["ELECT"].ToString() == "False")//Core courses
                //            {
                //                creditTotal = creditTotal + Convert.ToDecimal(dsMinSchemeInfo.Tables[0].Rows[i]["TOTAL_CRED"].ToString());
                //            }
                //        }

                //        int special_group = 0, other_group = 0;
                //        for (int i = 0; i < dsMinSchemeInfo.Tables[0].Rows.Count; i++)
                //        {
                //            if (dsMinSchemeInfo.Tables[0].Rows[i]["ELECT"].ToString() == "True")
                //            {
                //                if (dsMinSchemeInfo.Tables[0].Rows[i]["GROUPNO"].ToString() == "10")//special group from where    to pick 2 courses
                //                    special_group++;
                //                else
                //                    other_group++;
                //            }
                //        }
                //        #region if

                //        if (special_group > 0)//only group 10
                //        {
                //            int count = 0;
                //            for (int i = 0; i < dsMinSchemeInfo.Tables[0].Rows.Count; i++)
                //            {
                //                if (dsMinSchemeInfo.Tables[0].Rows[i]["ELECT"].ToString() == "True")//Elective courses
                //                {
                //                    if (dsMinSchemeInfo.Tables[0].Rows[i]["GROUPNO"].ToString() == "10")//special group from where    to pick 2 courses
                //                    {
                //                        creditTotal = creditTotal + Convert.ToDecimal(dsMinSchemeInfo.Tables[0].Rows[i]["TOTAL_CRED"].ToString());
                //                        count++;
                //                    }
                //                    if (count == 2)
                //                    {
                //                        break;//need cout of 2 Subject
                //                    }
                //                }
                //            }
                //        }
                //        else
                //        {
                //            for (int i = 0; i < dsMinSchemeInfo.Tables[0].Rows.Count; i++)
                //            {
                //                if (dsMinSchemeInfo.Tables[0].Rows[i]["ELECT"].ToString() == "True" && dsMinSchemeInfo.Tables[0].Rows[i]["SUBID"].ToString() == "1")//Elective theory  courses
                //                {

                //                    creditTotal = creditTotal + Convert.ToDecimal(dsMinSchemeInfo.Tables[0].Rows[i]["TOTAL_CRED"].ToString());

                //                    for (int j = 0; j < dsMinSchemeInfo.Tables[0].Rows.Count; j++)
                //                    {
                //                        if (dsMinSchemeInfo.Tables[0].Rows[j]["ELECT"].ToString() == "True" && dsMinSchemeInfo.Tables[0].Rows[j]["SUBID"].ToString() == "1")
                //                        {
                //                            if (dsMinSchemeInfo.Tables[0].Rows[j]["GROUPNO"].ToString() != dsMinSchemeInfo.Tables[0].Rows[i]["GROUPNO"].ToString())
                //                            {
                //                                creditTotal = creditTotal + Convert.ToDecimal(dsMinSchemeInfo.Tables[0].Rows[j]["TOTAL_CRED"].ToString());
                //                                break;
                //                            }
                //                        }
                //                    }
                //                    break;
                //                }
                //            }



                //            for (int i = 0; i < dsMinSchemeInfo.Tables[0].Rows.Count; i++)
                //            {
                //                if (dsMinSchemeInfo.Tables[0].Rows[i]["ELECT"].ToString() == "True" && dsMinSchemeInfo.Tables[0].Rows[i]["SUBID"].ToString() == "2")//Elective  practicle courses
                //                {

                //                    creditTotal = creditTotal + Convert.ToDecimal(dsMinSchemeInfo.Tables[0].Rows[i]["TOTAL_CRED"].ToString());

                //                    for (int j = 0; j < dsMinSchemeInfo.Tables[0].Rows.Count; j++)
                //                    {
                //                        if (dsMinSchemeInfo.Tables[0].Rows[j]["ELECT"].ToString() == "True" && dsMinSchemeInfo.Tables[0].Rows[j]["SUBID"].ToString() == "2")
                //                        {
                //                            if (dsMinSchemeInfo.Tables[0].Rows[j]["GROUPNO"].ToString() != dsMinSchemeInfo.Tables[0].Rows[i]["GROUPNO"].ToString())
                //                            {
                //                                creditTotal = creditTotal + Convert.ToDecimal(dsMinSchemeInfo.Tables[0].Rows[j]["TOTAL_CRED"].ToString());
                //                                break;
                //                            }
                //                        }
                //                    }
                //                    break;
                //                }
                //            }
                //        }
                //        #endregion
                //        //in above code group no 10 is elective group from which we have to take two courses where group other than 10 we have to take 1 Subject from each and 
                //        // maximum two grup will be available to each sem except group 10
                //        //if group no 10 is allocate to any sem then only group no 10 will be available no other group will come


                //        lblMinCreditLimit.Text = creditTotal.ToString();
                //    }
                //}
                //else
                //{
                //    lblMinCreditLimit.Text = Convert.ToString(dsCreditInfo.Tables[0].Rows[0]["FROM_CREDIT"].ToString());
                //}
                //#endregion

                //#region MAX_SCHEME_LIMIT
                //if (dsCreditInfo.Tables[0].Rows[0]["MAX_SCHEME_LIMIT"].ToString() == "1")
                //{
                //    DataSet dsMaxSchemeInfo = objSReg.getCreditCountOfScheme(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString()), Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString()), Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString()));

                //    // int creditTotal = 0;  //10.7.2018
                //    decimal creditTotal = 0;

                //    if (dsMaxSchemeInfo.Tables[0].Rows.Count > 0)
                //    {
                //        for (int i = 0; i < dsMaxSchemeInfo.Tables[0].Rows.Count; i++)
                //        {
                //            if (dsMaxSchemeInfo.Tables[0].Rows[i]["ELECT"].ToString() == "False")//Core courses
                //            {
                //                creditTotal = creditTotal + Convert.ToDecimal(dsMaxSchemeInfo.Tables[0].Rows[i]["TOTAL_CRED"].ToString());
                //            }
                //        }

                //        int special_group = 0, other_group = 0;
                //        for (int i = 0; i < dsMaxSchemeInfo.Tables[0].Rows.Count; i++)
                //        {
                //            if (dsMaxSchemeInfo.Tables[0].Rows[i]["ELECT"].ToString() == "True")
                //            {
                //                if (dsMaxSchemeInfo.Tables[0].Rows[i]["GROUPNO"].ToString() == "10")//special group from where    to pick 2 courses
                //                    special_group++;
                //                else
                //                    other_group++;
                //            }
                //        }


                //        if (special_group > 0)//only group 10
                //        {
                //            int count = 0;
                //            for (int i = 0; i < dsMaxSchemeInfo.Tables[0].Rows.Count; i++)
                //            {
                //                if (dsMaxSchemeInfo.Tables[0].Rows[i]["ELECT"].ToString() == "True")//Elective courses
                //                {
                //                    if (dsMaxSchemeInfo.Tables[0].Rows[i]["GROUPNO"].ToString() == "10")//special group from where    to pick 2 courses
                //                    {
                //                        creditTotal = creditTotal + Convert.ToDecimal(dsMaxSchemeInfo.Tables[0].Rows[i]["TOTAL_CRED"].ToString());
                //                        count++;
                //                    }
                //                    if (count == 2)
                //                    {
                //                        break;//need cout of 2 Subject
                //                    }
                //                }
                //            }
                //        }
                //        else
                //        {
                //            for (int i = 0; i < dsMaxSchemeInfo.Tables[0].Rows.Count; i++)
                //            {
                //                if (dsMaxSchemeInfo.Tables[0].Rows[i]["ELECT"].ToString() == "True" && dsMaxSchemeInfo.Tables[0].Rows[i]["SUBID"].ToString() == "1")//Elective theory courses
                //                {
                //                    creditTotal = creditTotal + Convert.ToDecimal(dsMaxSchemeInfo.Tables[0].Rows[i]["TOTAL_CRED"].ToString());

                //                    for (int j = 0; j < dsMaxSchemeInfo.Tables[0].Rows.Count; j++)
                //                    {
                //                        if (dsMaxSchemeInfo.Tables[0].Rows[j]["ELECT"].ToString() == "True" && dsMaxSchemeInfo.Tables[0].Rows[j]["SUBID"].ToString() == "1")
                //                        {
                //                            if (dsMaxSchemeInfo.Tables[0].Rows[j]["GROUPNO"].ToString() != dsMaxSchemeInfo.Tables[0].Rows[i]["GROUPNO"].ToString())
                //                            {
                //                                creditTotal = creditTotal + Convert.ToDecimal(dsMaxSchemeInfo.Tables[0].Rows[j]["TOTAL_CRED"].ToString());
                //                                break;
                //                            }
                //                        }
                //                    }
                //                    break;
                //                }
                //            }
                //            //ABOVE ELSE PART IS FOR SUM OF  TWO THEORY COURSES
                //            for (int i = 0; i < dsMaxSchemeInfo.Tables[0].Rows.Count; i++)
                //            {
                //                if (dsMaxSchemeInfo.Tables[0].Rows[i]["ELECT"].ToString() == "True" && dsMaxSchemeInfo.Tables[0].Rows[i]["SUBID"].ToString() == "2")//Elective practicle courses
                //                {

                //                    creditTotal = creditTotal + Convert.ToDecimal(dsMaxSchemeInfo.Tables[0].Rows[i]["TOTAL_CRED"].ToString());


                //                    for (int j = 0; j < dsMaxSchemeInfo.Tables[0].Rows.Count; j++)
                //                    {
                //                        if (dsMaxSchemeInfo.Tables[0].Rows[j]["ELECT"].ToString() == "True" && dsMaxSchemeInfo.Tables[0].Rows[j]["SUBID"].ToString() == "2")
                //                        {
                //                            if (dsMaxSchemeInfo.Tables[0].Rows[j]["GROUPNO"].ToString() != dsMaxSchemeInfo.Tables[0].Rows[i]["GROUPNO"].ToString())
                //                            {
                //                                creditTotal = creditTotal + Convert.ToDecimal(dsMaxSchemeInfo.Tables[0].Rows[j]["TOTAL_CRED"].ToString());
                //                                break;
                //                            }
                //                        }
                //                    }
                //                    break;
                //                }
                //            }
                //            //ABOVE ELSE PART IS FOR SUM OF  TWO PRACTICALE COURSES
                //        }
                //        lblMaxCreditlimit.Text = creditTotal.ToString();
                //    }
                //}

                //else
                //{
                //    lblMaxCreditlimit.Text = Convert.ToString(dsCreditInfo.Tables[0].Rows[0]["TO_CREDIT"].ToString());
                //}
                #endregion
            }
            else
            {
                objCommon.DisplayMessage(this.updDiv, "Credit limit not set for the Selected Session. Please Define Credit for Selected Session.", this.Page);
                return;
            }

            #endregion --------==========-----------

            DataSet dsStudentHistory = objCommon.FillDropDown("ACD_TRRESULT A  INNER JOIN ACD_SEMESTER S ON(A.SEMESTERNO=S.SEMESTERNO)  INNER JOIN ACD_STUDENT SA ON(SA.IDNO=A.IDNO)", " SEMESTERNAME ", "CGPA,SGPA", "a.sessionno=(select max(sessionno) from ACD_TRRESULT b  where  b.IDNO=SA.IDNO   and a.SEMESTERNO=b.SEMESTERNO)  and  SA.ENROLLNO='" + txtRollNo.Text + "'", "SEMESTERNAME");

            if (dsStudentHistory.Tables[0].Rows.Count > 0)
            {
                lvStudentHistory.DataSource = dsStudentHistory;
                lvStudentHistory.DataBind();
                lvStudentHistory.Visible = true;
            }
            else
            {
                lvStudentHistory.DataSource = null;
                lvStudentHistory.DataBind();
                lvStudentHistory.Visible = false;
            }
            StudentFeedBackController SFB = new StudentFeedBackController();
            string idno;
            if (!Session["usertype"].ToString().Equals("2") && txtRollNo.Text.Trim() == string.Empty)
            {
                objCommon.DisplayMessage(this.updDiv, "Please Enter Student Roll No.", this.Page);
                return;
            }
            if (Session["usertype"].ToString().Equals("2"))     //Student 
            {
                idno = Session["idno"].ToString();
            }
            else
            {
                idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO = '" + txtRollNo.Text.Trim() + "'");
            }

            int sessionno = 0;

            sessionno = Convert.ToInt16(ddlSession.SelectedValue);
            int semester = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + idno + ""));

            if (string.IsNullOrEmpty(idno))
            {
                objCommon.DisplayMessage(this.updDiv, "Student with Roll No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                //divCourses.Visible = false;
                return;
            }

            string branchno = objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + idno);

            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    //Show Student Details..
                    lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                    lblSection.Text = dsStudent.Tables[0].Rows[0]["SECTIONNAME"].ToString();
                    lblSection.ToolTip = dsStudent.Tables[0].Rows[0]["SECTIONNO"].ToString();
                    //lblAdmType.Text = dsStudent.Tables[0].Rows[0]["STUD_TYPE"].ToString();
                    lblFatherName.Text = dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                    lblMotherName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();

                    //lblRollNo.Text = dsStudent.Tables[0].Rows[0]["ROLLNO"].ToString();
                    lblRollNo.Text = dsStudent.Tables[0].Rows[0]["ENROLLNO"].ToString();
                    lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                    lblDegree.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString();
                    lblDegree.ToolTip = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();

                    lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                    lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();

                    // tblInfo.Visible = true;

                    //Commented as per requirement 01122016 start

                    #region BackCourse
                    //DataSet dsOfferedBackCourse = null;

                    //if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submit by fa
                    //{
                    //    dsOfferedBackCourse = objSReg.getOfferedBacklogCourses(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["idno"].ToString()), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString()), Convert.ToInt32(lblSemester.ToolTip), 1); // 1 for fa
                    //}
                    //else
                    //{
                    //    dsOfferedBackCourse = objSReg.getOfferedBacklogCourses(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["idno"].ToString()), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString()), Convert.ToInt32(lblSemester.ToolTip), 2); // 2 for student
                    //}

                    //if (dsOfferedBackCourse.Tables[0].Rows.Count > 0)
                    //{
                    //    lvBacklogCourse.DataSource = dsOfferedBackCourse.Tables[0];
                    //    lvBacklogCourse.DataBind();
                    //}
                    //else
                    //{
                    //    lvBacklogCourse.DataSource = null;
                    //    lvBacklogCourse.DataBind();
                    //}
                    ////Commented as per requirement 01122016 end
                    #endregion

                    #region Detained
                    DataSet dsOfferedDetained = null;
                    if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submit by fa
                    {
                        dsOfferedDetained = objSReg.getOfferedDetainCourses(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), Convert.ToInt32(lblSemester.ToolTip), 1); // 1 for fa
                    }
                    else
                    {
                        dsOfferedDetained = objSReg.getOfferedDetainCourses(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), Convert.ToInt32(lblSemester.ToolTip), 2); // 2 for student
                    }

                    if (dsOfferedDetained.Tables[0].Rows.Count > 0)
                    {
                        lvDetained.DataSource = dsOfferedDetained.Tables[0];
                        lvDetained.DataBind();
                    }
                    else
                    {
                        lvDetained.DataSource = null;
                        lvDetained.DataBind();
                        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", " $('#lblDMsg').text('No Data Available.');var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {$('#lblDMsg').text('No Data Available.');});", true);
                    }
                    #endregion

                    #region CurrentTerm

                    DataSet dsCurrCourses = null;
                    if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submit by fa
                    {
                        dsCurrCourses = objSReg.GetCurrentCourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 1);  // 1 for faculty
                    }
                    else
                    {
                        dsCurrCourses = objSReg.GetCurrentCourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), (Convert.ToInt32(ddlSession.SelectedValue)), Convert.ToInt32(lblScheme.ToolTip), 2);  // 2 for student
                    }

                    if (dsCurrCourses.Tables[0].Rows.Count > 0)
                    {
                        lvCurrentSubjects.DataSource = dsCurrCourses.Tables[0];
                        lvCurrentSubjects.DataBind();

                        int forEachCount = 0;
                        for (int i = 0; i < dsCurrCourses.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dsCurrCourses.Tables[0].Rows[i]["STUD_SELECTION"]) == 1)
                            {
                                foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
                                {
                                    forEachCount++;
                                    if (i == forEachCount - 1)
                                    {
                                        DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                                        for (int j = 0; j < ddl.Items.Count; j++)
                                        {
                                            Int32 v1 = Convert.ToInt32((ddl.Items[j]).Value.ToString().Split('-')[0]);
                                            Int32 v2 = Convert.ToInt32(dsCurrCourses.Tables[0].Rows[i]["SECTIONNO"]);
                                            if (v1 == v2)
                                            {
                                                ddl.SelectedIndex = j;

                                                Label Intake = (Label)dataitem.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                                                Label Filled = (Label)dataitem.FindControl("lblFilled");

                                                Intake.Text = ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                                                Filled.Text = ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString();

                                                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-default";
                                                    Filled.CssClass = "label label-default";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-danger";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-warning";
                                                }
                                                else
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-success";
                                                }
                                            }
                                        }
                                    }
                                }
                                forEachCount = 0;
                            }
                        }
                    }
                    else
                    {
                        lvCurrentSubjects.DataSource = null;
                        lvCurrentSubjects.DataBind();
                    }
                    #endregion

                    #region Elective
                    DataSet dsElective = null;
                    if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submitted by student..
                    {
                        dsElective = objSReg.GetElectiveCourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 1);//1 for faculty
                    }
                    else
                    {
                        dsElective = objSReg.GetElectiveCourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 2);// 2 for student
                    }

                    if (dsElective.Tables[0].Rows.Count > 0)
                    {
                        lvElectiveCourse.DataSource = dsElective.Tables[0];
                        lvElectiveCourse.DataBind();

                        int forEachCount = 0;
                        for (int i = 0; i < dsElective.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dsElective.Tables[0].Rows[i]["STUD_SELECTION"]) == 1)
                            {
                                foreach (ListViewDataItem dataitem in lvElectiveCourse.Items)
                                {
                                    forEachCount++;
                                    if (i == forEachCount - 1)
                                    {
                                        DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                                        for (int j = 0; j < ddl.Items.Count; j++)
                                        {
                                            Int32 v1 = Convert.ToInt32((ddl.Items[j]).Value.ToString().Split('-')[0]);
                                            Int32 v2 = Convert.ToInt32(dsElective.Tables[0].Rows[i]["SECTIONNO"]);
                                            if (v1 == v2)
                                            {
                                                ddl.SelectedIndex = j;

                                                Label Intake = (Label)dataitem.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                                                Label Filled = (Label)dataitem.FindControl("lblFilled");

                                                Intake.Text = ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                                                Filled.Text = ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString();

                                                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-default";
                                                    Filled.CssClass = "label label-default";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-danger";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-warning";
                                                }
                                                else
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-success";
                                                }
                                            }
                                        }
                                    }
                                }
                                forEachCount = 0;
                            }
                        }
                    }
                    else
                    {
                        lvElectiveCourse.DataSource = null;
                        lvElectiveCourse.DataBind();
                    }

                    #endregion

                    #region Elective Grp-2
                    DataSet dsElective2 = null;
                    if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submitted by student..
                    {
                        dsElective2 = objSReg.GetElectiveGrp2CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 1);//1 for faculty
                    }
                    else
                    {
                        dsElective2 = objSReg.GetElectiveGrp2CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 2);// 2 for student
                    }

                    if (dsElective2.Tables[0].Rows.Count > 0)
                    {
                        lvElectiveCourseGrp2.DataSource = dsElective2.Tables[0];
                        lvElectiveCourseGrp2.DataBind();

                        int forEachCount = 0;
                        for (int i = 0; i < dsElective2.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dsElective2.Tables[0].Rows[i]["STUD_SELECTION"]) == 1)
                            {
                                foreach (ListViewDataItem dataitem in lvElectiveCourseGrp2.Items)
                                {
                                    forEachCount++;
                                    if (i == forEachCount - 1)
                                    {
                                        DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                                        for (int j = 0; j < ddl.Items.Count; j++)
                                        {
                                            Int32 v1 = Convert.ToInt32((ddl.Items[j]).Value.ToString().Split('-')[0]);
                                            Int32 v2 = Convert.ToInt32(dsElective2.Tables[0].Rows[i]["SECTIONNO"]);
                                            if (v1 == v2)
                                            {
                                                ddl.SelectedIndex = j;

                                                Label Intake = (Label)dataitem.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                                                Label Filled = (Label)dataitem.FindControl("lblFilled");

                                                Intake.Text = ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                                                Filled.Text = ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString();

                                                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-default";
                                                    Filled.CssClass = "label label-default";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-danger";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-warning";
                                                }
                                                else
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-success";
                                                }
                                            }
                                        }
                                    }
                                }
                                forEachCount = 0;
                            }
                        }

                    }
                    else
                    {
                        lvElectiveCourseGrp2.DataSource = null;
                        lvElectiveCourseGrp2.DataBind();
                    }

                    #endregion  Elective Grp-2

                    #region Elective Grp-3
                    DataSet dsElective3 = null;
                    if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submitted by student..
                    {
                        dsElective3 = objSReg.GetElectiveGrp3CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 1);//1 for faculty
                    }
                    else
                    {
                        dsElective3 = objSReg.GetElectiveGrp3CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 2);// 2 for student
                    }

                    if (dsElective3.Tables[0].Rows.Count > 0)
                    {
                        lvElectiveCourseGrp3.DataSource = dsElective3.Tables[0];
                        lvElectiveCourseGrp3.DataBind();

                        int forEachCount = 0;
                        for (int i = 0; i < dsElective3.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dsElective3.Tables[0].Rows[i]["STUD_SELECTION"]) == 1)
                            {
                                foreach (ListViewDataItem dataitem in lvElectiveCourseGrp3.Items)
                                {
                                    forEachCount++;
                                    if (i == forEachCount - 1)
                                    {
                                        DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                                        for (int j = 0; j < ddl.Items.Count; j++)
                                        {
                                            Int32 v1 = Convert.ToInt32((ddl.Items[j]).Value.ToString().Split('-')[0]);
                                            Int32 v2 = Convert.ToInt32(dsElective3.Tables[0].Rows[i]["SECTIONNO"]);
                                            if (v1 == v2)
                                            {
                                                ddl.SelectedIndex = j;

                                                Label Intake = (Label)dataitem.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                                                Label Filled = (Label)dataitem.FindControl("lblFilled");

                                                Intake.Text = ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                                                Filled.Text = ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString();

                                                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-default";
                                                    Filled.CssClass = "label label-default";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-danger";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-warning";
                                                }
                                                else
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-success";
                                                }
                                            }
                                        }
                                    }
                                }
                                forEachCount = 0;
                            }
                        }

                    }
                    else
                    {
                        lvElectiveCourseGrp3.DataSource = null;
                        lvElectiveCourseGrp3.DataBind();
                    }

                    #endregion  Elective Grp-3

                    #region Elective Grp-4
                    DataSet dsElective4 = null;
                    if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submitted by student..
                    {
                        dsElective4 = objSReg.GetElectiveGrp4CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 1);//1 for faculty
                    }
                    else
                    {
                        dsElective4 = objSReg.GetElectiveGrp4CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 2);// 2 for student
                    }

                    if (dsElective4.Tables[0].Rows.Count > 0)
                    {
                        lvElectiveCourseGrp4.DataSource = dsElective4.Tables[0];
                        lvElectiveCourseGrp4.DataBind();

                        int forEachCount = 0;
                        for (int i = 0; i < dsElective4.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dsElective4.Tables[0].Rows[i]["STUD_SELECTION"]) == 1)
                            {
                                foreach (ListViewDataItem dataitem in lvElectiveCourseGrp4.Items)
                                {
                                    forEachCount++;
                                    if (i == forEachCount - 1)
                                    {
                                        DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                                        for (int j = 0; j < ddl.Items.Count; j++)
                                        {
                                            Int32 v1 = Convert.ToInt32((ddl.Items[j]).Value.ToString().Split('-')[0]);
                                            Int32 v2 = Convert.ToInt32(dsElective4.Tables[0].Rows[i]["SECTIONNO"]);
                                            if (v1 == v2)
                                            {
                                                ddl.SelectedIndex = j;

                                                Label Intake = (Label)dataitem.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                                                Label Filled = (Label)dataitem.FindControl("lblFilled");

                                                Intake.Text = ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                                                Filled.Text = ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString();

                                                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-default";
                                                    Filled.CssClass = "label label-default";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-danger";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-warning";
                                                }
                                                else
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-success";
                                                }
                                            }
                                        }
                                    }
                                }
                                forEachCount = 0;
                            }
                        }

                    }
                    else
                    {
                        lvElectiveCourseGrp4.DataSource = null;
                        lvElectiveCourseGrp4.DataBind();
                    }

                    #endregion  Elective Grp-4

                    #region Elective Grp-5
                    DataSet dsElective5 = null;
                    if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submitted by student..
                    {
                        dsElective5 = objSReg.GetElectiveGrp5CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 1);//1 for faculty
                    }
                    else
                    {
                        dsElective5 = objSReg.GetElectiveGrp5CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 2);// 2 for student
                    }

                    if (dsElective5.Tables[0].Rows.Count > 0)
                    {
                        lvElectiveCourseGrp5.DataSource = dsElective5.Tables[0];
                        lvElectiveCourseGrp5.DataBind();

                        int forEachCount = 0;
                        for (int i = 0; i < dsElective5.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dsElective5.Tables[0].Rows[i]["STUD_SELECTION"]) == 1)
                            {
                                foreach (ListViewDataItem dataitem in lvElectiveCourseGrp5.Items)
                                {
                                    forEachCount++;
                                    if (i == forEachCount - 1)
                                    {
                                        DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                                        for (int j = 0; j < ddl.Items.Count; j++)
                                        {
                                            Int32 v1 = Convert.ToInt32((ddl.Items[j]).Value.ToString().Split('-')[0]);
                                            Int32 v2 = Convert.ToInt32(dsElective5.Tables[0].Rows[i]["SECTIONNO"]);
                                            if (v1 == v2)
                                            {
                                                ddl.SelectedIndex = j;

                                                Label Intake = (Label)dataitem.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                                                Label Filled = (Label)dataitem.FindControl("lblFilled");

                                                Intake.Text = ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                                                Filled.Text = ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString();

                                                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-default";
                                                    Filled.CssClass = "label label-default";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-danger";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-warning";
                                                }
                                                else
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-success";
                                                }
                                            }
                                        }
                                    }
                                }
                                forEachCount = 0;
                            }
                        }

                    }
                    else
                    {
                        lvElectiveCourseGrp5.DataSource = null;
                        lvElectiveCourseGrp5.DataBind();
                    }

                    #endregion  Elective Grp-5

                    #region Elective Grp-6
                    DataSet dsElective6 = null;
                    if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submitted by student..
                    {
                        dsElective6 = objSReg.GetElectiveGrp6CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 1);//1 for faculty
                    }
                    else
                    {
                        dsElective6 = objSReg.GetElectiveGrp6CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 2);// 2 for student
                    }

                    if (dsElective6.Tables[0].Rows.Count > 0)
                    {
                        lvElectiveCourseGrp6.DataSource = dsElective6.Tables[0];
                        lvElectiveCourseGrp6.DataBind();

                        int forEachCount = 0;
                        for (int i = 0; i < dsElective6.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dsElective6.Tables[0].Rows[i]["STUD_SELECTION"]) == 1)
                            {
                                foreach (ListViewDataItem dataitem in lvElectiveCourseGrp6.Items)
                                {
                                    forEachCount++;
                                    if (i == forEachCount - 1)
                                    {
                                        DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                                        for (int j = 0; j < ddl.Items.Count; j++)
                                        {
                                            Int32 v1 = Convert.ToInt32((ddl.Items[j]).Value.ToString().Split('-')[0]);
                                            Int32 v2 = Convert.ToInt32(dsElective6.Tables[0].Rows[i]["SECTIONNO"]);
                                            if (v1 == v2)
                                            {
                                                ddl.SelectedIndex = j;

                                                Label Intake = (Label)dataitem.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                                                Label Filled = (Label)dataitem.FindControl("lblFilled");

                                                Intake.Text = ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                                                Filled.Text = ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString();

                                                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-default";
                                                    Filled.CssClass = "label label-default";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-danger";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-warning";
                                                }
                                                else
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-success";
                                                }
                                            }
                                        }
                                    }
                                }
                                forEachCount = 0;
                            }
                        }

                    }
                    else
                    {
                        lvElectiveCourseGrp6.DataSource = null;
                        lvElectiveCourseGrp6.DataBind();
                    }

                    #endregion  Elective Grp-6

                    #region Elective Grp-7
                    DataSet dsElective7 = null;
                    if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submitted by student..
                    {
                        dsElective7 = objSReg.GetElectiveGrp7CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 1);//1 for faculty
                    }
                    else
                    {
                        dsElective7 = objSReg.GetElectiveGrp7CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 2);// 2 for student
                    }

                    if (dsElective7.Tables[0].Rows.Count > 0)
                    {
                        lvElectiveCourseGrp7.DataSource = dsElective7.Tables[0];
                        lvElectiveCourseGrp7.DataBind();

                        int forEachCount = 0;
                        for (int i = 0; i < dsElective7.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dsElective7.Tables[0].Rows[i]["STUD_SELECTION"]) == 1)
                            {
                                foreach (ListViewDataItem dataitem in lvElectiveCourseGrp7.Items)
                                {
                                    forEachCount++;
                                    if (i == forEachCount - 1)
                                    {
                                        DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                                        for (int j = 0; j < ddl.Items.Count; j++)
                                        {
                                            Int32 v1 = Convert.ToInt32((ddl.Items[j]).Value.ToString().Split('-')[0]);
                                            Int32 v2 = Convert.ToInt32(dsElective7.Tables[0].Rows[i]["SECTIONNO"]);
                                            if (v1 == v2)
                                            {
                                                ddl.SelectedIndex = j;

                                                Label Intake = (Label)dataitem.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                                                Label Filled = (Label)dataitem.FindControl("lblFilled");

                                                Intake.Text = ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                                                Filled.Text = ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString();

                                                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-default";
                                                    Filled.CssClass = "label label-default";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-danger";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-warning";
                                                }
                                                else
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-success";
                                                }
                                            }
                                        }
                                    }
                                }
                                forEachCount = 0;
                            }
                        }

                    }
                    else
                    {
                        lvElectiveCourseGrp7.DataSource = null;
                        lvElectiveCourseGrp7.DataBind();
                    }

                    #endregion  Elective Grp-7

                    #region Elective Grp-8
                    DataSet dsElective8 = null;
                    if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submitted by student..
                    {
                        dsElective8 = objSReg.GetElectiveGrp8CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 1);//1 for faculty
                    }
                    else
                    {
                        dsElective8 = objSReg.GetElectiveGrp8CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 2);// 2 for student
                    }

                    if (dsElective8.Tables[0].Rows.Count > 0)
                    {
                        lvElectiveCourseGrp8.DataSource = dsElective8.Tables[0];
                        lvElectiveCourseGrp8.DataBind();

                        int forEachCount = 0;
                        for (int i = 0; i < dsElective8.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dsElective8.Tables[0].Rows[i]["STUD_SELECTION"]) == 1)
                            {
                                foreach (ListViewDataItem dataitem in lvElectiveCourseGrp8.Items)
                                {
                                    forEachCount++;
                                    if (i == forEachCount - 1)
                                    {
                                        DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                                        for (int j = 0; j < ddl.Items.Count; j++)
                                        {
                                            Int32 v1 = Convert.ToInt32((ddl.Items[j]).Value.ToString().Split('-')[0]);
                                            Int32 v2 = Convert.ToInt32(dsElective7.Tables[0].Rows[i]["SECTIONNO"]);
                                            if (v1 == v2)
                                            {
                                                ddl.SelectedIndex = j;

                                                Label Intake = (Label)dataitem.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                                                Label Filled = (Label)dataitem.FindControl("lblFilled");

                                                Intake.Text = ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                                                Filled.Text = ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString();

                                                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-default";
                                                    Filled.CssClass = "label label-default";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-danger";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-warning";
                                                }
                                                else
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-success";
                                                }
                                            }
                                        }
                                    }
                                }
                                forEachCount = 0;
                            }
                        }

                    }
                    else
                    {
                        lvElectiveCourseGrp8.DataSource = null;
                        lvElectiveCourseGrp8.DataBind();
                    }

                    #endregion  Elective Grp-8

                    #region Elective Grp-9
                    DataSet dsElective9 = null;
                    if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submitted by student..
                    {
                        dsElective9 = objSReg.GetElectiveGrp9CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 1);//1 for faculty
                    }
                    else
                    {
                        dsElective9 = objSReg.GetElectiveGrp9CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 2);// 2 for student
                    }

                    if (dsElective9.Tables[0].Rows.Count > 0)
                    {
                        lvElectiveCourseGrp9.DataSource = dsElective9.Tables[0];
                        lvElectiveCourseGrp9.DataBind();

                        int forEachCount = 0;
                        for (int i = 0; i < dsElective9.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dsElective9.Tables[0].Rows[i]["STUD_SELECTION"]) == 1)
                            {
                                foreach (ListViewDataItem dataitem in lvElectiveCourseGrp9.Items)
                                {
                                    forEachCount++;
                                    if (i == forEachCount - 1)
                                    {
                                        DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                                        for (int j = 0; j < ddl.Items.Count; j++)
                                        {
                                            Int32 v1 = Convert.ToInt32((ddl.Items[j]).Value.ToString().Split('-')[0]);
                                            Int32 v2 = Convert.ToInt32(dsElective7.Tables[0].Rows[i]["SECTIONNO"]);
                                            if (v1 == v2)
                                            {
                                                ddl.SelectedIndex = j;

                                                Label Intake = (Label)dataitem.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                                                Label Filled = (Label)dataitem.FindControl("lblFilled");

                                                Intake.Text = ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                                                Filled.Text = ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString();

                                                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-default";
                                                    Filled.CssClass = "label label-default";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-danger";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-warning";
                                                }
                                                else
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-success";
                                                }
                                            }
                                        }
                                    }
                                }
                                forEachCount = 0;
                            }
                        }

                    }
                    else
                    {
                        lvElectiveCourseGrp9.DataSource = null;
                        lvElectiveCourseGrp9.DataBind();
                    }

                    #endregion  Elective Grp-9

                    #region Elective Grp-10
                    DataSet dsElective10 = null;
                    if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submitted by student..
                    {
                        dsElective10 = objSReg.GetElectiveGrp10CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 1);//1 for faculty
                    }
                    else
                    {
                        dsElective10 = objSReg.GetElectiveGrp10CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 2);// 2 for student
                    }

                    if (dsElective10.Tables[0].Rows.Count > 0)
                    {
                        lvElectiveCourseGrp10.DataSource = dsElective10.Tables[0];
                        lvElectiveCourseGrp10.DataBind();

                        int forEachCount = 0;
                        for (int i = 0; i < dsElective10.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dsElective10.Tables[0].Rows[i]["STUD_SELECTION"]) == 1)
                            {
                                foreach (ListViewDataItem dataitem in lvElectiveCourseGrp10.Items)
                                {
                                    forEachCount++;
                                    if (i == forEachCount - 1)
                                    {
                                        DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                                        for (int j = 0; j < ddl.Items.Count; j++)
                                        {
                                            Int32 v1 = Convert.ToInt32((ddl.Items[j]).Value.ToString().Split('-')[0]);
                                            Int32 v2 = Convert.ToInt32(dsElective7.Tables[0].Rows[i]["SECTIONNO"]);
                                            if (v1 == v2)
                                            {
                                                ddl.SelectedIndex = j;

                                                Label Intake = (Label)dataitem.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                                                Label Filled = (Label)dataitem.FindControl("lblFilled");

                                                Intake.Text = ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                                                Filled.Text = ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString();

                                                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-default";
                                                    Filled.CssClass = "label label-default";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-danger";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-warning";
                                                }
                                                else
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-success";
                                                }
                                            }
                                        }
                                    }
                                }
                                forEachCount = 0;
                            }
                        }

                    }
                    else
                    {
                        lvElectiveCourseGrp10.DataSource = null;
                        lvElectiveCourseGrp10.DataBind();
                    }

                    #endregion  Elective Grp-10

                    #region Elective Grp-11
                    DataSet dsElective11 = null;
                    if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submitted by student..
                    {
                        dsElective11 = objSReg.GetElectiveGrp11CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 1);//1 for faculty
                    }
                    else
                    {
                        dsElective11 = objSReg.GetElectiveGrp11CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 2);// 2 for student
                    }

                    if (dsElective11.Tables[0].Rows.Count > 0)
                    {
                        lvElectiveCourseGrp11.DataSource = dsElective11.Tables[0];
                        lvElectiveCourseGrp11.DataBind();

                        int forEachCount = 0;
                        for (int i = 0; i < dsElective11.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dsElective11.Tables[0].Rows[i]["STUD_SELECTION"]) == 1)
                            {
                                foreach (ListViewDataItem dataitem in lvElectiveCourseGrp11.Items)
                                {
                                    forEachCount++;
                                    if (i == forEachCount - 1)
                                    {
                                        DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                                        for (int j = 0; j < ddl.Items.Count; j++)
                                        {
                                            Int32 v1 = Convert.ToInt32((ddl.Items[j]).Value.ToString().Split('-')[0]);
                                            Int32 v2 = Convert.ToInt32(dsElective7.Tables[0].Rows[i]["SECTIONNO"]);
                                            if (v1 == v2)
                                            {
                                                ddl.SelectedIndex = j;

                                                Label Intake = (Label)dataitem.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                                                Label Filled = (Label)dataitem.FindControl("lblFilled");

                                                Intake.Text = ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                                                Filled.Text = ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString();

                                                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-default";
                                                    Filled.CssClass = "label label-default";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-danger";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-warning";
                                                }
                                                else
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-success";
                                                }
                                            }
                                        }
                                    }
                                }
                                forEachCount = 0;
                            }
                        }

                    }
                    else
                    {
                        lvElectiveCourseGrp11.DataSource = null;
                        lvElectiveCourseGrp11.DataBind();
                    }

                    #endregion  Elective Grp-11

                    #region Elective Grp-12
                    DataSet dsElective12 = null;
                    if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submitted by student..
                    {
                        dsElective12 = objSReg.GetElectiveGrp12CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 1);//1 for faculty
                    }
                    else
                    {
                        dsElective12 = objSReg.GetElectiveGrp12CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 2);// 2 for student
                    }

                    if (dsElective12.Tables[0].Rows.Count > 0)
                    {
                        lvElectiveCourseGrp12.DataSource = dsElective12.Tables[0];
                        lvElectiveCourseGrp12.DataBind();

                        int forEachCount = 0;
                        for (int i = 0; i < dsElective12.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dsElective12.Tables[0].Rows[i]["STUD_SELECTION"]) == 1)
                            {
                                foreach (ListViewDataItem dataitem in lvElectiveCourseGrp12.Items)
                                {
                                    forEachCount++;
                                    if (i == forEachCount - 1)
                                    {
                                        DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                                        for (int j = 0; j < ddl.Items.Count; j++)
                                        {
                                            Int32 v1 = Convert.ToInt32((ddl.Items[j]).Value.ToString().Split('-')[0]);
                                            Int32 v2 = Convert.ToInt32(dsElective7.Tables[0].Rows[i]["SECTIONNO"]);
                                            if (v1 == v2)
                                            {
                                                ddl.SelectedIndex = j;

                                                Label Intake = (Label)dataitem.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                                                Label Filled = (Label)dataitem.FindControl("lblFilled");

                                                Intake.Text = ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                                                Filled.Text = ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString();

                                                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-default";
                                                    Filled.CssClass = "label label-default";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-danger";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-warning";
                                                }
                                                else
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-success";
                                                }
                                            }
                                        }
                                    }
                                }
                                forEachCount = 0;
                            }
                        }

                    }
                    else
                    {
                        lvElectiveCourseGrp12.DataSource = null;
                        lvElectiveCourseGrp12.DataBind();
                    }

                    #endregion  Elective Grp-12

                    #region Elective Grp-13
                    DataSet dsElective13 = null;
                    if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submitted by student..
                    {
                        dsElective13 = objSReg.GetElectiveGrp13CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 1);//1 for faculty
                    }
                    else
                    {
                        dsElective13 = objSReg.GetElectiveGrp13CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 2);// 2 for student
                    }

                    if (dsElective13.Tables[0].Rows.Count > 0)
                    {
                        lvElectiveCourseGrp13.DataSource = dsElective13.Tables[0];
                        lvElectiveCourseGrp13.DataBind();

                        int forEachCount = 0;
                        for (int i = 0; i < dsElective13.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dsElective13.Tables[0].Rows[i]["STUD_SELECTION"]) == 1)
                            {
                                foreach (ListViewDataItem dataitem in lvElectiveCourseGrp13.Items)
                                {
                                    forEachCount++;
                                    if (i == forEachCount - 1)
                                    {
                                        DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                                        for (int j = 0; j < ddl.Items.Count; j++)
                                        {
                                            Int32 v1 = Convert.ToInt32((ddl.Items[j]).Value.ToString().Split('-')[0]);
                                            Int32 v2 = Convert.ToInt32(dsElective7.Tables[0].Rows[i]["SECTIONNO"]);
                                            if (v1 == v2)
                                            {
                                                ddl.SelectedIndex = j;

                                                Label Intake = (Label)dataitem.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                                                Label Filled = (Label)dataitem.FindControl("lblFilled");

                                                Intake.Text = ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                                                Filled.Text = ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString();

                                                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-default";
                                                    Filled.CssClass = "label label-default";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-danger";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-warning";
                                                }
                                                else
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-success";
                                                }
                                            }
                                        }
                                    }
                                }
                                forEachCount = 0;
                            }
                        }

                    }
                    else
                    {
                        lvElectiveCourseGrp13.DataSource = null;
                        lvElectiveCourseGrp13.DataBind();
                    }

                    #endregion  Elective Grp-13

                    #region Elective Grp-14
                    DataSet dsElective14 = null;
                    if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submitted by student..
                    {
                        dsElective14 = objSReg.GetElectiveGrp14CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 1);//1 for faculty
                    }
                    else
                    {
                        dsElective14 = objSReg.GetElectiveGrp14CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 2);// 2 for student
                    }

                    if (dsElective14.Tables[0].Rows.Count > 0)
                    {
                        lvElectiveCourseGrp14.DataSource = dsElective14.Tables[0];
                        lvElectiveCourseGrp14.DataBind();

                        int forEachCount = 0;
                        for (int i = 0; i < dsElective14.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dsElective14.Tables[0].Rows[i]["STUD_SELECTION"]) == 1)
                            {
                                foreach (ListViewDataItem dataitem in lvElectiveCourseGrp14.Items)
                                {
                                    forEachCount++;
                                    if (i == forEachCount - 1)
                                    {
                                        DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                                        for (int j = 0; j < ddl.Items.Count; j++)
                                        {
                                            Int32 v1 = Convert.ToInt32((ddl.Items[j]).Value.ToString().Split('-')[0]);
                                            Int32 v2 = Convert.ToInt32(dsElective7.Tables[0].Rows[i]["SECTIONNO"]);
                                            if (v1 == v2)
                                            {
                                                ddl.SelectedIndex = j;

                                                Label Intake = (Label)dataitem.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                                                Label Filled = (Label)dataitem.FindControl("lblFilled");

                                                Intake.Text = ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                                                Filled.Text = ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString();

                                                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-default";
                                                    Filled.CssClass = "label label-default";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-danger";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-warning";
                                                }
                                                else
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-success";
                                                }
                                            }
                                        }
                                    }
                                }
                                forEachCount = 0;
                            }
                        }

                    }
                    else
                    {
                        lvElectiveCourseGrp14.DataSource = null;
                        lvElectiveCourseGrp14.DataBind();
                    }

                    #endregion  Elective Grp-14

                    #region Elective Grp-15
                    DataSet dsElective15 = null;
                    if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submitted by student..
                    {
                        dsElective15 = objSReg.GetElectiveGrp15CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 1);//1 for faculty
                    }
                    else
                    {
                        dsElective15 = objSReg.GetElectiveGrp15CourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 2);// 2 for student
                    }

                    if (dsElective15.Tables[0].Rows.Count > 0)
                    {
                        lvElectiveCourseGrp15.DataSource = dsElective15.Tables[0];
                        lvElectiveCourseGrp15.DataBind();

                        int forEachCount = 0;
                        for (int i = 0; i < dsElective15.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dsElective15.Tables[0].Rows[i]["STUD_SELECTION"]) == 1)
                            {
                                foreach (ListViewDataItem dataitem in lvElectiveCourseGrp15.Items)
                                {
                                    forEachCount++;
                                    if (i == forEachCount - 1)
                                    {
                                        DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                                        for (int j = 0; j < ddl.Items.Count; j++)
                                        {
                                            Int32 v1 = Convert.ToInt32((ddl.Items[j]).Value.ToString().Split('-')[0]);
                                            Int32 v2 = Convert.ToInt32(dsElective7.Tables[0].Rows[i]["SECTIONNO"]);
                                            if (v1 == v2)
                                            {
                                                ddl.SelectedIndex = j;

                                                Label Intake = (Label)dataitem.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                                                Label Filled = (Label)dataitem.FindControl("lblFilled");

                                                Intake.Text = ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                                                Filled.Text = ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString();

                                                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-default";
                                                    Filled.CssClass = "label label-default";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-danger";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-warning";
                                                }
                                                else
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-success";
                                                }
                                            }
                                        }
                                    }
                                }
                                forEachCount = 0;
                            }
                        }

                    }
                    else
                    {
                        lvElectiveCourseGrp15.DataSource = null;
                        lvElectiveCourseGrp15.DataBind();
                    }

                    #endregion  Elective Grp-15


                    #region AdditionalCourse

                    //objCommon.FillDropDownList(ddlCoursesUg1Section, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNAME");
                    //ddlCoursesUg1Section.SelectedValue = lblSection.ToolTip;
                    //objCommon.FillDropDownList(ddlCoursesUg2Section, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNAME");
                    //ddlCoursesUg2Section.SelectedValue = lblSection.ToolTip;


                    //DataSet dsAdditionalUg = null;
                    //if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submit by fa
                    //{
                    //    dsAdditionalUg = objSReg.GetAdditionalCoursesForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblBranch.ToolTip), 1, 1, 0);//1 for UG 1 for fa
                    //}
                    //else
                    //{
                    //    dsAdditionalUg = objSReg.GetAdditionalCoursesForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblBranch.ToolTip), 1, 2, 0);//1 for UG 2 for student and other people
                    //}

                    //ddlCoursesUg1.Items.Clear();
                    //ddlCoursesUg1.Items.Add("Please Select");
                    //ddlCoursesUg1.SelectedItem.Value = "0";

                    //if (dsAdditionalUg.Tables[0].Rows.Count > 0)
                    //{
                    //    ddlCoursesUg1.DataSource = dsAdditionalUg;
                    //    ddlCoursesUg1.DataValueField = dsAdditionalUg.Tables[0].Columns[0].ToString();
                    //    ddlCoursesUg1.DataTextField = dsAdditionalUg.Tables[0].Columns[1].ToString();

                    //    ddlCoursesUg1.DataBind();

                    //    for (int i = 0; i < dsAdditionalUg.Tables[0].Rows.Count; i++)
                    //    {
                    //        if (dsAdditionalUg.Tables[0].Rows[i]["REGISTERED"].ToString() == "1" && dsAdditionalUg.Tables[0].Rows[i]["ADD_COURSE_SRNO"].ToString() == "1")//add courseno 1 means Subject of 1st drop down
                    //        {
                    //            ddlCoursesUg1.SelectedValue = dsAdditionalUg.Tables[0].Rows[i]["COURSENO"].ToString();
                    //            ddlCoursesUg1Section.SelectedValue = dsAdditionalUg.Tables[0].Rows[i]["SECTIONNO"].ToString();
                    //        }
                    //    }
                    //}

                    //ddlCoursesUg2.Items.Clear();
                    //ddlCoursesUg2.Items.Add("Please Select");
                    //ddlCoursesUg2.SelectedItem.Value = "0";

                    //if (dsAdditionalUg.Tables[0].Rows.Count > 0)
                    //{
                    //    ddlCoursesUg2.DataSource = dsAdditionalUg;
                    //    ddlCoursesUg2.DataValueField = dsAdditionalUg.Tables[0].Columns[0].ToString();
                    //    ddlCoursesUg2.DataTextField = dsAdditionalUg.Tables[0].Columns[1].ToString();

                    //    ddlCoursesUg2.DataBind();

                    //    for (int i = 0; i < dsAdditionalUg.Tables[0].Rows.Count; i++)
                    //    {
                    //        if (dsAdditionalUg.Tables[0].Rows[i]["REGISTERED"].ToString() == "1" && dsAdditionalUg.Tables[0].Rows[i]["ADD_COURSE_SRNO"].ToString() == "2")
                    //        {
                    //            ddlCoursesUg2.SelectedValue = dsAdditionalUg.Tables[0].Rows[i]["COURSENO"].ToString();
                    //            ddlCoursesUg2Section.SelectedValue = dsAdditionalUg.Tables[0].Rows[i]["SECTIONNO"].ToString();
                    //        }
                    //    }
                    //}


                    //objCommon.FillDropDownList(ddlCoursesPg1Section, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNAME");
                    //ddlCoursesPg1Section.SelectedValue = lblSection.ToolTip;
                    //objCommon.FillDropDownList(ddlCoursesPg2Section, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNAME");
                    //ddlCoursesPg2Section.SelectedValue = lblSection.ToolTip;


                    //DataSet dsAdditionalpg = null;
                    //if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submit by fa
                    //{
                    //    dsAdditionalpg = objSReg.GetAdditionalCoursesForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblBranch.ToolTip), 2, 1, 0);//2 for pG 1 for faculty
                    //}
                    //else
                    //{
                    //    dsAdditionalpg = objSReg.GetAdditionalCoursesForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblBranch.ToolTip), 2, 2, 0);//2 for PG 2 for student and other people
                    //}

                    //ddlCoursesPg1.Items.Clear();
                    //ddlCoursesPg1.Items.Add("Please Select");
                    //ddlCoursesPg1.SelectedItem.Value = "0";

                    //if (dsAdditionalpg.Tables[0].Rows.Count > 0)
                    //{
                    //    ddlCoursesPg1.DataSource = dsAdditionalpg;
                    //    ddlCoursesPg1.DataValueField = dsAdditionalpg.Tables[0].Columns[0].ToString();
                    //    ddlCoursesPg1.DataTextField = dsAdditionalpg.Tables[0].Columns[1].ToString();

                    //    ddlCoursesPg1.DataBind();

                    //    for (int i = 0; i < dsAdditionalpg.Tables[0].Rows.Count; i++)
                    //    {
                    //        if (dsAdditionalpg.Tables[0].Rows[i]["REGISTERED"].ToString() == "1" && dsAdditionalpg.Tables[0].Rows[i]["ADD_COURSE_SRNO"].ToString() == "1")
                    //        {
                    //            ddlCoursesPg1.SelectedValue = dsAdditionalpg.Tables[0].Rows[i]["COURSENO"].ToString();
                    //            ddlCoursesPg1Section.SelectedValue = dsAdditionalpg.Tables[0].Rows[i]["SECTIONNO"].ToString();
                    //        }
                    //    }
                    //}

                    //ddlCoursesPg2.Items.Clear();
                    //ddlCoursesPg2.Items.Add("Please Select");
                    //ddlCoursesPg2.SelectedItem.Value = "0";

                    //if (dsAdditionalpg.Tables[0].Rows.Count > 0)
                    //{
                    //    ddlCoursesPg2.DataSource = dsAdditionalpg;
                    //    ddlCoursesPg2.DataValueField = dsAdditionalpg.Tables[0].Columns[0].ToString();
                    //    ddlCoursesPg2.DataTextField = dsAdditionalpg.Tables[0].Columns[1].ToString();

                    //    ddlCoursesPg2.DataBind();

                    //    for (int i = 0; i < dsAdditionalpg.Tables[0].Rows.Count; i++)
                    //    {
                    //        if (dsAdditionalpg.Tables[0].Rows[i]["REGISTERED"].ToString() == "1" && dsAdditionalpg.Tables[0].Rows[i]["ADD_COURSE_SRNO"].ToString() == "2")
                    //        {
                    //            ddlCoursesPg2.SelectedValue = dsAdditionalpg.Tables[0].Rows[i]["COURSENO"].ToString();
                    //            ddlCoursesPg2Section.SelectedValue = dsAdditionalpg.Tables[0].Rows[i]["SECTIONNO"].ToString();
                    //        }
                    //    }
                    //}

                    //#endregion

                    //#region GP_COURSE

                    //fillGpCourses();

                    //#endregion

                    //#region Open Elective

                    //fillOpenElectiveCourses();

                    #endregion

                    #region Movable
                    DataSet dsMovable = null;
                    if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submitted by student..
                    {
                        dsMovable = objSReg.GetMovableCourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 1);//1 for faculty
                    }
                    else
                    {
                        dsMovable = objSReg.GetMovableCourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 2);// 2 for student
                    }

                    if (dsMovable.Tables[0].Rows.Count > 0)
                    {
                        lvMovableSubjects.DataSource = dsMovable.Tables[0];
                        lvMovableSubjects.DataBind();

                        int forEachCount = 0;
                        for (int i = 0; i < dsMovable.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dsMovable.Tables[0].Rows[i]["STUD_SELECTION"]) == 1)
                            {
                                foreach (ListViewDataItem dataitem in lvMovableSubjects.Items)
                                {
                                    forEachCount++;
                                    if (i == forEachCount - 1)
                                    {
                                        DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                                        for (int j = 0; j < ddl.Items.Count; j++)
                                        {
                                            Int32 v1 = Convert.ToInt32((ddl.Items[j]).Value.ToString().Split('-')[0]);
                                            Int32 v2 = Convert.ToInt32(dsMovable.Tables[0].Rows[i]["SECTIONNO"]);
                                            if (v1 == v2)
                                            {
                                                ddl.SelectedIndex = j;

                                                Label Intake = (Label)dataitem.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                                                Label Filled = (Label)dataitem.FindControl("lblFilled");

                                                Intake.Text = ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                                                Filled.Text = ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString();

                                                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-default";
                                                    Filled.CssClass = "label label-default";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-danger";
                                                }
                                                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-warning";
                                                }
                                                else
                                                {
                                                    Intake.CssClass = "label label-success";
                                                    Filled.CssClass = "label label-success";
                                                }
                                            }
                                        }
                                    }
                                }
                                forEachCount = 0;
                            }
                        }
                    }
                    else
                    {
                        lvMovableSubjects.DataSource = null;
                        lvMovableSubjects.DataBind();
                    }

                    #endregion

                    ////new patch of for disabling checkbox of subject which are detain and previously registered

                    DataSet dsRegAndDetainCourses = objCommon.FillDropDown("ACD_STUDENT_RESULT_hist", "distinct COURSENO", "CCODE", "IDNO=" + Convert.ToInt32(lblName.ToolTip) + "and SESSIONNO <" + Convert.ToInt32(ddlSession.SelectedValue) + " and isnull(EXAM_REGISTERED,0)=1 and isnull(CANCEL,0)=0", "");
                    //enable audit subject which are previously register but got grademark 4(fail) to register again
                    #region CheckByRegiterdCoursese
                    if (dsRegAndDetainCourses.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dt in dsRegAndDetainCourses.Tables[0].Rows)
                        {
                            foreach (ListViewDataItem item in lvCurrentSubjects.Items)
                            {
                                CheckBox chk = item.FindControl("chkAccept") as CheckBox;
                                HiddenField hdf = item.FindControl("hdfCCode") as HiddenField;
                                if (hdf.Value == dt["COURSENO"].ToString())
                                {
                                    //  chk.Enabled = false;
                                }
                            }
                        }

                        #region Commented
                        //foreach (DataRow dt in dsRegAndDetainCourses.Tables[0].Rows)
                        //{
                        //    foreach (ListViewDataItem item in lvElectiveCourse.Items)
                        //    {
                        //        CheckBox chk = item.FindControl("chkElective") as CheckBox;
                        //        HiddenField hdf = item.FindControl("hdfCCode") as HiddenField;
                        //        if (hdf.Value == dt["COURSENO"].ToString())
                        //        {
                        //            //** chk.Enabled = false;
                        //        }
                        //    }
                        //}
                        #endregion
                    }

                    #endregion

                    ////new patch of for disabling checkbox of subject which are detain but not availab in history beacause subject were not exam reg 

                    #region CheckByRegiterdCoursese
                    if (dsOfferedDetained.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dt in dsOfferedDetained.Tables[0].Rows)
                        {
                            foreach (ListViewDataItem item in lvCurrentSubjects.Items)
                            {
                                CheckBox chk = item.FindControl("chkAccept") as CheckBox;
                                HiddenField hdf = item.FindControl("hdfCCode") as HiddenField;
                                if (hdf.Value == dt["COURSENO"].ToString())
                                {
                                    chk.Enabled = false;
                                }
                            }
                        }

                        #region Commented
                        //foreach (DataRow dt in dsOfferedDetained.Tables[0].Rows)
                        //{
                        //    foreach (ListViewDataItem item in lvElectiveCourse.Items)
                        //    {

                        //        CheckBox chk = item.FindControl("chkElective") as CheckBox;
                        //        HiddenField hdf = item.FindControl("hdfCCode") as HiddenField;
                        //        if (hdf.Value == dt["COURSENO"].ToString())
                        //        {
                        //            //*   chk.Enabled = false;
                        //        }
                        //    }
                        //}
                        #endregion
                    }

                    #endregion

                    txtAllSubjects.Text = CheckTotalSubjects().ToString();
                    txtCredits.Text = CheckTotalCredits().ToString();
                }
                else
                {
                    objCommon.DisplayMessage(this.updDiv, "Student with Registration No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion Show Details

    protected decimal CheckTotalCredits()
    {
        decimal regCreditCout = 0;
        foreach (ListViewDataItem li in lvCurrentSubjects.Items)
        {
            CheckBox chk = li.FindControl("chkAccept") as CheckBox;
            if (chk.Checked)
            {
                Label lblCredit = li.FindControl("lblCredits") as Label;
                regCreditCout = regCreditCout + Convert.ToDecimal(lblCredit.Text);
            }
        }

        foreach (ListViewDataItem li in lvDetained.Items)
        {

            CheckBox chk = li.FindControl("chkDetain") as CheckBox;
            if (chk.Checked)
            {
                Label lblCredit = li.FindControl("lblCredits") as Label;
                regCreditCout = regCreditCout + Convert.ToDecimal(lblCredit.Text);
            }
        }


        foreach (ListViewDataItem li in lvElectiveCourse.Items)
        {
            CheckBox chk = li.FindControl("chkElectiveGrp1") as CheckBox;
            if (chk.Checked)
            {
                Label lblCredit = li.FindControl("lblCredits") as Label;
                regCreditCout = regCreditCout + Convert.ToDecimal(lblCredit.Text);
            }
        }

        foreach (ListViewDataItem li in lvElectiveCourseGrp2.Items)
        {
            CheckBox chk = li.FindControl("chkElectiveGrp2") as CheckBox;
            if (chk.Checked)
            {
                Label lblCredit = li.FindControl("lblCredits") as Label;
                regCreditCout = regCreditCout + Convert.ToDecimal(lblCredit.Text);
            }
        }

        foreach (ListViewDataItem li in lvElectiveCourseGrp3.Items)
        {
            CheckBox chk = li.FindControl("chkElectiveGrp3") as CheckBox;
            if (chk.Checked)
            {
                Label lblCredit = li.FindControl("lblCredits") as Label;
                regCreditCout = regCreditCout + Convert.ToDecimal(lblCredit.Text);
            }
        }

        foreach (ListViewDataItem li in lvElectiveCourseGrp4.Items)
        {
            CheckBox chk = li.FindControl("chkElectiveGrp4") as CheckBox;
            if (chk.Checked)
            {
                Label lblCredit = li.FindControl("lblCredits") as Label;
                regCreditCout = regCreditCout + Convert.ToDecimal(lblCredit.Text);
            }
        }

        foreach (ListViewDataItem li in lvMovableSubjects.Items)
        {
            CheckBox chk = li.FindControl("chkMovable") as CheckBox;
            if (chk.Checked)
            {
                Label lblCredit = li.FindControl("lblCredits") as Label;
                regCreditCout = regCreditCout + Convert.ToDecimal(lblCredit.Text);
            }
        }

        return regCreditCout;
    }

    protected int CheckTotalSubjects()
    {
        int checkSubjectCount = 0;
        foreach (ListViewDataItem li in lvCurrentSubjects.Items)
        {
            CheckBox chk = li.FindControl("chkAccept") as CheckBox;
            if (chk.Checked)
            {
                checkSubjectCount = checkSubjectCount + 1;
            }
        }

        foreach (ListViewDataItem li in lvDetained.Items)
        {
            CheckBox chk = li.FindControl("chkDetain") as CheckBox;
            if (chk.Checked)
            {
                checkSubjectCount = checkSubjectCount + 1;
            }
        }

        foreach (ListViewDataItem li in lvElectiveCourse.Items)
        {
            CheckBox chk = li.FindControl("chkElectiveGrp1") as CheckBox;
            if (chk.Checked)
            {
                checkSubjectCount = checkSubjectCount + 1;
            }
        }

        foreach (ListViewDataItem li in lvElectiveCourseGrp2.Items)
        {
            CheckBox chk = li.FindControl("chkElectiveGrp2") as CheckBox;
            if (chk.Checked)
            {
                checkSubjectCount = checkSubjectCount + 1;
            }
        }

        foreach (ListViewDataItem li in lvElectiveCourseGrp3.Items)
        {
            CheckBox chk = li.FindControl("chkElectiveGrp3") as CheckBox;
            if (chk.Checked)
            {
                checkSubjectCount = checkSubjectCount + 1;
            }
        }

        foreach (ListViewDataItem li in lvElectiveCourseGrp4.Items)
        {
            CheckBox chk = li.FindControl("chkElectiveGrp4") as CheckBox;
            if (chk.Checked)
            {
                checkSubjectCount = checkSubjectCount + 1;
            }
        }

        foreach (ListViewDataItem li in lvMovableSubjects.Items)
        {
            CheckBox chk = li.FindControl("chkMovable") as CheckBox;
            if (chk.Checked)
            {
                checkSubjectCount = checkSubjectCount + 1;
            }
        }


        return checkSubjectCount;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StudentRegist objSR = new StudentRegist();
        objSR.IDNO = Convert.ToInt32(lblName.ToolTip);
        objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip);

        #region Commented  #endregion

        //string strno = string.Empty;
        //strno = objCommon.LookUp("ACD_BACK_DET_CRED_LIMIT", "STATUS", "IDNO =" + Convert.ToInt32(lblName.ToolTip) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt16(lblSemester.ToolTip));

        //if (!string.IsNullOrEmpty(strno))
        //{
        //    if (Convert.ToInt32(strno) != 1)
        //    {
        //        if (Convert.ToDecimal(ViewState["minRegCreditLimit"]) > CheckTotalCredits())
        //        {
        //            objCommon.DisplayMessage(this.updDiv, "Regular Courses Credit can not be less than " + ViewState["minRegCreditLimit"].ToString() + " Credits", this.Page);
        //            return;
        //        }
        //    }
        //}

        #endregion


        if (lvCurrentSubjects.Enabled == true)
        {
            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {
                if (Convert.ToInt32((dataitem.FindControl("lblIntake") as Label).Text) == Convert.ToInt32((dataitem.FindControl("lblFilled") as Label).Text))
                {
                    DropDownList ddl = dataitem.FindControl("ddlSection") as DropDownList;
                    if (ddl.Enabled == false)
                    {
                        objCommon.DisplayMessage(this.updDiv, "Sorry !!! No Seat vacant for this year.", this.Page);
                        return;
                    }
                }
            }
        }

        int status = 0;

        foreach (ListViewDataItem dataitem in lvDetained.Items)
        {
            CheckBox chk = dataitem.FindControl("chkDetain") as CheckBox;
            if (chk.Checked == true)
                status++;
        }
        // Get Checkbox Count
        foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
        {
            CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
            if (chk.Checked == true)
            {
                status++;

                DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                //if (Convert.ToInt32(ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString()))
                //{
                //    objCommon.DisplayMessage(this.updDiv, "Sorry !!! Your Selected Core Course Intake for Current Semester Core Subjects  is full ! Try another one.", this.Page);
                //    chk.Focus();
                //  //  return;
                //}
            }
        }

        #region Elective Groups
        foreach (ListViewDataItem dataitem in lvElectiveCourse.Items)//==== Elect Grp-1 =======//
        {
            CheckBox chk = dataitem.FindControl("chkElectiveGrp1") as CheckBox;
            if (chk.Checked == true)
            {
                status++;

                DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                //if (Convert.ToInt32(ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString()))
                //{
                //    objCommon.DisplayMessage(this.updDiv, "Sorry !!! Your Selected Elective Course Intake for Elective Offered Subjects [Group-1] is full ! Try another one.", this.Page);
                //    chk.Focus();
                //   // return;
                //}

            }
        }

        foreach (ListViewDataItem dataitem in lvElectiveCourseGrp2.Items)//==== Elect Grp-2 =======//
        {
            CheckBox chk = dataitem.FindControl("chkElectiveGrp2") as CheckBox;
            if (chk.Checked == true)
            {
                status++;

                DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                //if (Convert.ToInt32(ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString()))
                //{
                //    objCommon.DisplayMessage(this.updDiv, "Sorry !!! Your Selected Elective Course Intake for Elective Offered Subjects [Group-2] is full ! Try another one.", this.Page);
                //    chk.Focus();
                //  //  return;
                //}
            }
        }

        foreach (ListViewDataItem dataitem in lvElectiveCourseGrp3.Items)//==== Elect Grp-3 =======//
        {
            CheckBox chk = dataitem.FindControl("chkElectiveGrp3") as CheckBox;
            if (chk.Checked == true)
            {
                status++;

                DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                //if (Convert.ToInt32(ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString()))
                //{
                //    objCommon.DisplayMessage(this.updDiv, "Sorry !!! Your Selected Elective Course Intake for Elective Offered Subjects [Group-3] is full ! Try another one.", this.Page);
                //    chk.Focus();
                //  //  return;
                //}
            }
        }

        foreach (ListViewDataItem dataitem in lvElectiveCourseGrp4.Items)//==== Elect Grp-4 =======//
        {
            CheckBox chk = dataitem.FindControl("chkElectiveGrp4") as CheckBox;
            if (chk.Checked == true)
            {
                status++;

                DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                //if (Convert.ToInt32(ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString()))
                //{
                //    objCommon.DisplayMessage(this.updDiv, "Sorry !!! Your Selected Elective Course Intake for Elective Offered Subjects [Group-4] is full ! Try another one.", this.Page);
                //    chk.Focus();
                //   // return;
                //}
            }
        }

        foreach (ListViewDataItem dataitem in lvElectiveCourseGrp5.Items)//==== Elect Grp-4 =======//
        {
            CheckBox chk = dataitem.FindControl("chkElectiveGrp5") as CheckBox;
            if (chk.Checked == true)
            {
                status++;

                DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                //if (Convert.ToInt32(ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString()))
                //{
                //    objCommon.DisplayMessage(this.updDiv, "Sorry !!! Your Selected Elective Course Intake for Elective Offered Subjects [Group-5] is full ! Try another one.", this.Page);
                //    chk.Focus();
                //   // return;
                //}
            }
        }

        foreach (ListViewDataItem dataitem in lvElectiveCourseGrp6.Items)//==== Elect Grp-4 =======//
        {
            CheckBox chk = dataitem.FindControl("chkElectiveGrp6") as CheckBox;
            if (chk.Checked == true)
            {
                status++;

                DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                //if (Convert.ToInt32(ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString()))
                //{
                //    objCommon.DisplayMessage(this.updDiv, "Sorry !!! Your Selected Elective Course Intake for Elective Offered Subjects [Group-6] is full ! Try another one.", this.Page);
                //    chk.Focus();
                //   // return;
                //}
            }
        }

        foreach (ListViewDataItem dataitem in lvElectiveCourseGrp7.Items)//==== Elect Grp-4 =======//
        {
            CheckBox chk = dataitem.FindControl("chkElectiveGrp7") as CheckBox;
            if (chk.Checked == true)
            {
                status++;

                DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                //if (Convert.ToInt32(ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString()))
                //{
                //    objCommon.DisplayMessage(this.updDiv, "Sorry !!! Your Selected Elective Course Intake for Elective Offered Subjects [Group-7] is full ! Try another one.", this.Page);
                //    chk.Focus();
                //   // return;
                //}
            }
        }

        #endregion

        foreach (ListViewDataItem dataitem in lvMovableSubjects.Items)//==== Elect Grp-1 =======//
        {
            CheckBox chk = dataitem.FindControl("chkMovable") as CheckBox;
            if (chk.Checked == true)
            {
                status++;

                DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
                //if (Convert.ToInt32(ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString()))
                //{
                //    objCommon.DisplayMessage(this.updDiv, "Sorry !!! Your Selected Movable Subject Intake for Movable Offered Subjects is full ! Try another one.", this.Page);
                //    chk.Focus();
                //  //  return;
                //}

            }
        }

        if (status != 0)
        {
            #region Commented
            ////string studentIDs = lblName.ToolTip;
            //if (ViewState["CREDIT_LIMIT_STATUS"].ToString().Equals("1"))
            //{
            //    if (Convert.ToDecimal(txtCredits.Text) < Convert.ToDecimal(lblMinCreditLimit.Text))
            //    {
            //        objCommon.DisplayMessage(this.updDiv, "Total Credit can not be less than Minimum Credit Limit", this.Page);
            //        return;
            //    }
            //    if (Convert.ToDecimal(txtCredits.Text) > Convert.ToDecimal(lblMaxCreditlimit.Text))
            //    {
            //        objCommon.DisplayMessage(this.updDiv, "Maximum " + lblMaxCreditlimit.Text + "  credits are allowed for registration", this.Page);
            //        return;
            //    }
            //}
            #endregion

            objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
            string ipAddress = Request.ServerVariables["REMOTE_HOST"];
            Session["ipAddress"] = ipAddress;
            objSR.IPADDRESS = Session["ipAddress"].ToString();
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.REGNO = lblEnrollNo.Text.Trim();
            objSR.ROLLNO = objCommon.LookUp("ACD_STUDENT", "ROLLNO", "ENROLLNO='" + txtRollNo.Text.Trim() + "'");

            if (lvDetained.Items.Count != 0)
            {
                foreach (ListViewDataItem dataitem in lvDetained.Items)
                {
                    if ((dataitem.FindControl("chkDetain") as CheckBox).Checked == true)
                    {
                        objSR.detainNos = objSR.detainNos + (dataitem.FindControl("hdfCCode") as HiddenField).Value + ",";
                        objSR.detainSectionos = objSR.detainSectionos + (dataitem.FindControl("ddlsection") as DropDownList).SelectedValue + ",";
                    }
                }
            }

            objSR.detainNos = objSR.detainNos == null ? "0" : objSR.detainNos;
            objSR.detainSectionos = objSR.detainSectionos == null ? "0" : objSR.detainSectionos;

            if (lvCurrentSubjects.Items.Count != 0)
            {
                foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
                {
                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                    {
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("hdfCCode") as HiddenField).Value + ",";
                        objSR.SECTIONNOS = objSR.SECTIONNOS + ((dataitem.FindControl("ddlsection") as DropDownList).SelectedValue).Split('-')[0] + ",";
                        objSR._STUD_UA_NO = objSR._STUD_UA_NO + ((dataitem.FindControl("ddlsection") as DropDownList).SelectedValue).Split('-')[1] + ",";
                    }
                }
            }

            #region All Elective Groups
            if (lvElectiveCourse.Items.Count != 0)
            {
                foreach (ListViewDataItem dataitem in lvElectiveCourse.Items)
                {
                    if ((dataitem.FindControl("chkElectiveGrp1") as CheckBox).Checked == true)
                    {
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("hdfCCode") as HiddenField).Value + ",";
                        objSR.SECTIONNOS = objSR.SECTIONNOS + ((dataitem.FindControl("ddlsection") as DropDownList).SelectedValue).Split('-')[0] + ",";
                        objSR._STUD_UA_NO = objSR._STUD_UA_NO + ((dataitem.FindControl("ddlsection") as DropDownList).SelectedValue).Split('-')[1] + ",";
                    }
                }
            }

            if (lvElectiveCourseGrp2.Items.Count != 0)
            {
                foreach (ListViewDataItem dataitem in lvElectiveCourseGrp2.Items)
                {
                    if ((dataitem.FindControl("chkElectiveGrp2") as CheckBox).Checked == true)
                    {
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("hdfCCode") as HiddenField).Value + ",";
                        objSR.SECTIONNOS = objSR.SECTIONNOS + ((dataitem.FindControl("ddlsection") as DropDownList).SelectedValue).Split('-')[0] + ",";
                        objSR._STUD_UA_NO = objSR._STUD_UA_NO + ((dataitem.FindControl("ddlsection") as DropDownList).SelectedValue).Split('-')[1] + ",";
                    }
                }
            }

            if (lvElectiveCourseGrp3.Items.Count != 0)
            {
                foreach (ListViewDataItem dataitem in lvElectiveCourseGrp3.Items)
                {
                    if ((dataitem.FindControl("chkElectiveGrp3") as CheckBox).Checked == true)
                    {
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("hdfCCode") as HiddenField).Value + ",";
                        objSR.SECTIONNOS = objSR.SECTIONNOS + ((dataitem.FindControl("ddlsection") as DropDownList).SelectedValue).Split('-')[0] + ",";
                        objSR._STUD_UA_NO = objSR._STUD_UA_NO + ((dataitem.FindControl("ddlsection") as DropDownList).SelectedValue).Split('-')[1] + ",";
                    }
                }
            }

            if (lvElectiveCourseGrp4.Items.Count != 0)
            {
                foreach (ListViewDataItem dataitem in lvElectiveCourseGrp4.Items)
                {
                    if ((dataitem.FindControl("chkElectiveGrp4") as CheckBox).Checked == true)
                    {
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("hdfCCode") as HiddenField).Value + ",";
                        objSR.SECTIONNOS = objSR.SECTIONNOS + ((dataitem.FindControl("ddlsection") as DropDownList).SelectedValue).Split('-')[0] + ",";
                        objSR._STUD_UA_NO = objSR._STUD_UA_NO + ((dataitem.FindControl("ddlsection") as DropDownList).SelectedValue).Split('-')[1] + ",";
                    }
                }
            }

            if (lvElectiveCourseGrp5.Items.Count != 0)
            {
                foreach (ListViewDataItem dataitem in lvElectiveCourseGrp5.Items)
                {
                    if ((dataitem.FindControl("chkElectiveGrp5") as CheckBox).Checked == true)
                    {
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("hdfCCode") as HiddenField).Value + ",";
                        objSR.SECTIONNOS = objSR.SECTIONNOS + ((dataitem.FindControl("ddlsection") as DropDownList).SelectedValue).Split('-')[0] + ",";
                        objSR._STUD_UA_NO = objSR._STUD_UA_NO + ((dataitem.FindControl("ddlsection") as DropDownList).SelectedValue).Split('-')[1] + ",";
                    }
                }
            }

            if (lvElectiveCourseGrp6.Items.Count != 0)
            {
                foreach (ListViewDataItem dataitem in lvElectiveCourseGrp6.Items)
                {
                    if ((dataitem.FindControl("chkElectiveGrp6") as CheckBox).Checked == true)
                    {
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("hdfCCode") as HiddenField).Value + ",";
                        objSR.SECTIONNOS = objSR.SECTIONNOS + ((dataitem.FindControl("ddlsection") as DropDownList).SelectedValue).Split('-')[0] + ",";
                        objSR._STUD_UA_NO = objSR._STUD_UA_NO + ((dataitem.FindControl("ddlsection") as DropDownList).SelectedValue).Split('-')[1] + ",";
                    }
                }
            }

            if (lvElectiveCourseGrp7.Items.Count != 0)
            {
                foreach (ListViewDataItem dataitem in lvElectiveCourseGrp7.Items)
                {
                    if ((dataitem.FindControl("chkElectiveGrp7") as CheckBox).Checked == true)
                    {
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("hdfCCode") as HiddenField).Value + ",";
                        objSR.SECTIONNOS = objSR.SECTIONNOS + ((dataitem.FindControl("ddlsection") as DropDownList).SelectedValue).Split('-')[0] + ",";
                        objSR._STUD_UA_NO = objSR._STUD_UA_NO + ((dataitem.FindControl("ddlsection") as DropDownList).SelectedValue).Split('-')[1] + ",";
                    }
                }
            }


            #endregion

            if (lvMovableSubjects.Items.Count != 0)
            {
                foreach (ListViewDataItem dataitem in lvMovableSubjects.Items)
                {
                    if ((dataitem.FindControl("chkMovable") as CheckBox).Checked == true)
                    {
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("hdfCCode") as HiddenField).Value + ",";
                        objSR.SECTIONNOS = objSR.SECTIONNOS + ((dataitem.FindControl("ddlsection") as DropDownList).SelectedValue).Split('-')[0] + ",";
                        objSR._STUD_UA_NO = objSR._STUD_UA_NO + ((dataitem.FindControl("ddlsection") as DropDownList).SelectedValue).Split('-')[1] + ",";
                    }
                }
            }

            objSR.COURSENOS = objSR.COURSENOS == null ? "0" : objSR.COURSENOS;
            objSR.SECTIONNOS = objSR.SECTIONNOS == null ? "0" : objSR.SECTIONNOS;

            int ret = 0;

            if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
            {
                ret = objSReg.AcceptRegisteredSubjects(objSR);
            }
            else
            {
                ret = objSReg.AddAddlRegisteredSubjects(objSR);

                if (ret.Equals(-999))         // Added by Abhinay Lad [21-06-2019]
                {
                    objCommon.DisplayMessage(this.updDiv, "Sorry !!! Course Intake for your selected Section is full !", this.Page);
                    btnSubmit.Enabled = false;
                    //lvCurrentSubjects.Enabled = false;
                    btnPrintRegSlip.Enabled = true;
                    this.ShowDetails();
                }
                else if (ret.Equals(1))
                {
                    objCommon.DisplayMessage(this.updDiv, "Subject Registration Successful. Print the Registration Slip !!", this.Page);
                    btnSubmit.Enabled = false;
                    //btnDropTerm.Enabled = false;
                    lvCurrentSubjects.Enabled = false;
                    btnPrintRegSlip.Enabled = true;
                    if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))     //fa 
                    {
                        ShowReport("RegistrationSlip", "rptBulkPreRegslip_newall.rpt", 1);
                    }
                    else
                    {
                        ShowReport("RegistrationSlip", "rptBulkPreRegslip_newall.rpt", 2);
                    }
                    this.ShowDetails();
                }
                else
                {
                    objCommon.DisplayMessage(this.updDiv, "Error ! Please try again later.", this.Page);
                }
            }
        }

        else
        {
            objCommon.DisplayMessage(this.updDiv, "Please Select atleast One Subject in Subject list for Registration!", this.Page);
        }
    }

    protected void btnProceed_Click(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString().Equals("2"))     //Student 
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "distinct SESSIONNO", "SESSION_PNAME", "FLOCK=1", "SESSIONNO DESC");
            ddlSession.SelectedIndex = 1;
            // tblSelect.Visible = false;

            string count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "IDNO=" + Session["idno"].ToString() + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
            if (count != "0")
            {
                objCommon.DisplayMessage(this.updDiv, "Subject Registration already done..You can Generate only Registration Slip..!!", this.Page);
                this.ShowDetails();
                lvCurrentSubjects.Enabled = false;
                btnSubmit.Visible = false;
                //btnDropTerm.Enabled = false;
                btnPrintRegSlip.Enabled = true;
                lvCurrentSubjects.Enabled = false;

                //lvBacklogCourse.Enabled = false;
                //lvElectiveCourse.Enabled = false;
            }
            else
            {
                txtRollNo.Text = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO='" + Session["idno"].ToString() + "'");
                txtRollNo.Enabled = false;
                ShowDetails();
            }
        }
        else
        {
            ddlSession.SelectedIndex = 0;
            txtRollNo.Text = string.Empty;
            PopulateDropDownList();
        }
    }

    private void ShowReport(string rptName, int dcrNo, int studentNo, string copyNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=" + this.GetReportParameters(studentNo, dcrNo, copyNo);
            divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetReportParameters(int studentNo, int dcrNo, string copyNo)
    {
        string param = "@P_IDNO=" + studentNo.ToString() + ",@P_DCRNO=" + dcrNo + ",CopyNo=" + copyNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;
    }


    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))     //fa 
        {
            ShowReport("RegistrationSlip", "rptBulkPreRegslip_newall.rpt", 1);
        }
        else
        {
            ShowReport("RegistrationSlip", "rptBulkPreRegslip_newall.rpt", 2);
        }
    }

    private void ShowReport(string reportTitle, string rptFileName, int user_type)
    {
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int idno = Convert.ToInt32(lblName.ToolTip);
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@UserName=" + Session["username"].ToString() + ",@P_USER_TYPE=" + user_type;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //update panel
            string Script = string.Empty;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updDiv, updDiv.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnPrePrintClallan_Click(object sender, EventArgs e)
    {
        string studentIDs = lblName.ToolTip;
        int selectSemesterNo = Int32.Parse(lblSemester.ToolTip);

        string dcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(studentIDs) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + selectSemesterNo);

        if (dcrNo != string.Empty && studentIDs != string.Empty)
        {
            this.ShowReport("FeeCollectionReceiptForSemCourseRegister.rpt", Convert.ToInt32(dcrNo), Convert.ToInt32(studentIDs), "1");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void lnkBtnTimeTable_Click(object sender, EventArgs e)
    {
        string coursenos = string.Empty;
        string batchnos = string.Empty;
        string sectionnos = string.Empty;
        string CourCode = string.Empty;



        foreach (ListViewDataItem dataitem in lvDetained.Items)
        {
            CheckBox chk = dataitem.FindControl("chkDetain") as CheckBox;
            if (chk.Checked == true)
            {
                HiddenField hf = (HiddenField)dataitem.FindControl("hdfCCode");
                HiddenField hfb = (HiddenField)dataitem.FindControl("hdfBatchno");
                DropDownList ddl1 = (DropDownList)dataitem.FindControl("ddlsection");
                Label lblCCode = (Label)dataitem.FindControl("lblCCode");

                coursenos = coursenos + hf.Value + ",";
                batchnos = batchnos + hfb.Value + ",";
                sectionnos = sectionnos + (Convert.ToInt32(ddl1.SelectedValue)).ToString() + ",";
                // sectionnos = sectionnos + (Convert.ToInt32(ddl1.SelectedItem.Value.ToString().Split('-')[0])) + ",";
                CourCode = CourCode + lblCCode.Text + ",";
            }
        }
        foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
        {
            CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
            if (chk.Checked == true)
            {
                HiddenField hf = (HiddenField)dataitem.FindControl("hdfCCode");
                HiddenField hfb = (HiddenField)dataitem.FindControl("hdfBatchno");
                DropDownList ddl1 = (DropDownList)dataitem.FindControl("ddlsection");
                Label lblCCode = (Label)dataitem.FindControl("lblCCode");
                HiddenField hdfSectionnoCT = (HiddenField)dataitem.FindControl("hdfSectionnoCT");
                //Convert.ToInt32((ddl.Items[i]).Value.ToString().Split('-')[0]);
                coursenos = coursenos + hf.Value + ",";
                batchnos = batchnos + hfb.Value + ",";
                sectionnos = sectionnos + (Convert.ToInt32(ddl1.SelectedItem.Value.ToString().Split('-')[0])) + ",";
                //    sectionnos = sectionnos + (Convert.ToInt32(hdfSectionnoCT.Value)).ToString() + ",";
                CourCode = CourCode + lblCCode.Text + ",";
            }

        }
        int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO='" + lblRollNo.Text.ToString() + "'"));

        #region Commented
        //foreach (ListViewDataItem dataitem in lvElectiveCourse.Items)
        //{
        //    CheckBox chk = dataitem.FindControl("chkElective") as CheckBox;
        //    if (chk.Checked == true)
        //    {
        //        HiddenField hf = (HiddenField)dataitem.FindControl("hdfCCode");
        //        HiddenField hfb = (HiddenField)dataitem.FindControl("hdfBatchno");
        //        DropDownList ddl1 = (DropDownList)dataitem.FindControl("ddlsection");
        //        Label lblCCode = (Label)dataitem.FindControl("lblCCode");

        //        coursenos = coursenos + hf.Value + ",";
        //        batchnos = batchnos + hfb.Value + ",";
        //        sectionnos = sectionnos + (Convert.ToInt32(ddl1.SelectedValue)).ToString() + ",";
        //        CourCode = CourCode + lblCCode.Text + ",";
        //    }
        //}     

        #endregion
        if (coursenos == string.Empty)
        {
            objCommon.DisplayMessage(this.updDiv, "Please Select Atleast One Subject", this.Page);
            return;
        }
        else
        {
            string STUDSECTION = objCommon.LookUp("ACD_STUDENT", "ISNULL(SECTIONNO,0)", "ENROLLNO='" + lblRollNo.Text.ToString() + "'");

            DataSet dsTimeTable = objSReg.getTimeTable(Convert.ToInt32(ddlSession.SelectedValue), coursenos, sectionnos, batchnos);
            if (dsTimeTable.Tables[0].Rows.Count > 0)
            {
                lvTimeTable.DataSource = dsTimeTable;
                lvTimeTable.DataBind();
            }
            else
            {
                lvTimeTable.DataSource = null;
                lvTimeTable.DataBind();
            }

            // ===================================================
            // =========== OVERLAPPING % Calculation =============
            string OverlapList = string.Empty;

            if (CourCode.Length > 0)
            {
                CourCode = CourCode.Substring(0, CourCode.Length - 1);
            }

            string[] CourseList = CourCode.Split(',');
            int CCount = 0;
            int OCount = 0;
            foreach (string cl in CourseList)
            {
                CCount = 0;
                OCount = 0;

                if (string.IsNullOrEmpty(OverlapList))
                    OverlapList = OverlapList + cl + "- (";
                else
                    OverlapList = OverlapList + ";  " + cl + "- (";

                if (dsTimeTable.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsTimeTable.Tables[0].Rows.Count; i++)
                    {
                        for (int j = 0; j < dsTimeTable.Tables[0].Columns.Count; j++)
                        {
                            if (dsTimeTable.Tables[0].Rows[i][j].ToString().Contains(cl))
                            {
                                CCount++;
                                if (dsTimeTable.Tables[0].Rows[i][j].ToString().Contains(","))
                                {
                                    OCount++;
                                }
                            }
                        }
                    }
                }
                if (CCount > 0)
                {
                    OverlapList = OverlapList + " " + ((OCount * 100) / CCount).ToString() + " % ) ";
                }
                else
                {
                    OverlapList = OverlapList + " 0 % ) ";
                }
            }

            lblOverlap.Text = OverlapList;
            //olap.InnerHtml = OverlapList;
            //===================================================

            ModalPopupExtender1.Show();
        }

    }

    #region Commented
    //protected void ddlCoursesUg1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DataSet dsAdditionalUg = null;
    //    if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submit by fa
    //    {
    //        dsAdditionalUg = objSReg.GetAdditionalCoursesForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblBranch.ToolTip), 1, 1, Convert.ToInt32(ddlCoursesUg1.SelectedValue));//1 for UG 1 for fa
    //    }
    //    else
    //    {
    //        dsAdditionalUg = objSReg.GetAdditionalCoursesForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblBranch.ToolTip), 1, 2, Convert.ToInt32(ddlCoursesUg1.SelectedValue));//1 for UG 2 for student and other people
    //    }

    //    ddlCoursesUg2.Items.Clear();
    //    ddlCoursesUg2.Items.Add("Please Select");
    //    ddlCoursesUg2.SelectedItem.Value = "0";

    //    if (dsAdditionalUg.Tables[0].Rows.Count > 0)
    //    {
    //        ddlCoursesUg2.DataSource = dsAdditionalUg;
    //        ddlCoursesUg2.DataValueField = dsAdditionalUg.Tables[0].Columns[0].ToString();
    //        ddlCoursesUg2.DataTextField = dsAdditionalUg.Tables[0].Columns[1].ToString();

    //        ddlCoursesUg2.DataBind();

    //        for (int i = 0; i < dsAdditionalUg.Tables[0].Rows.Count; i++)
    //        {
    //            if (dsAdditionalUg.Tables[0].Rows[i]["REGISTERED"].ToString() == "1" && dsAdditionalUg.Tables[0].Rows[i]["ADD_COURSE_SRNO"].ToString() == "2")//add courseno 2 means Subject of 2 drop down
    //            {
    //                ddlCoursesUg2.SelectedValue = dsAdditionalUg.Tables[0].Rows[i]["COURSENO"].ToString();
    //            }
    //        }
    //    }
    //}
    //protected void ddlCoursesPg1_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    DataSet dsAdditionalPg = null;
    //    if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submit by fa
    //    {
    //        dsAdditionalPg = objSReg.GetAdditionalCoursesForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblBranch.ToolTip), 2, 1, Convert.ToInt32(ddlCoursesPg1.SelectedValue));//1 for PG 1 for fa
    //    }
    //    else
    //    {
    //        dsAdditionalPg = objSReg.GetAdditionalCoursesForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblBranch.ToolTip), 2, 2, Convert.ToInt32(ddlCoursesPg1.SelectedValue));//1 for UG 2 for student and other people
    //    }

    //    ddlCoursesPg2.Items.Clear();
    //    ddlCoursesPg2.Items.Add("Please Select");
    //    ddlCoursesPg2.SelectedItem.Value = "0";

    //    if (dsAdditionalPg.Tables[0].Rows.Count > 0)
    //    {
    //        ddlCoursesPg2.DataSource = dsAdditionalPg;
    //        ddlCoursesPg2.DataValueField = dsAdditionalPg.Tables[0].Columns[0].ToString();
    //        ddlCoursesPg2.DataTextField = dsAdditionalPg.Tables[0].Columns[1].ToString();

    //        ddlCoursesPg2.DataBind();

    //        for (int i = 0; i < dsAdditionalPg.Tables[0].Rows.Count; i++)
    //        {
    //            if (dsAdditionalPg.Tables[0].Rows[i]["REGISTERED"].ToString() == "1" && dsAdditionalPg.Tables[0].Rows[i]["ADD_COURSE_SRNO"].ToString() == "2") //add courseno 2 means Subject of 2 drop down
    //            {
    //                ddlCoursesPg2.SelectedValue = dsAdditionalPg.Tables[0].Rows[i]["COURSENO"].ToString();
    //            }
    //        }
    //    }
    //}
    //protected void ddlCoursesPg2_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlCoursesPg1.SelectedValue == "0")
    //    {
    //        objCommon.DisplayMessage(this.updDiv, "Please Select Additional Subject 1", this.Page);
    //        ddlCoursesPg2.SelectedValue = "0";
    //    }
    //}

    //protected void ddlCoursesUg2_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlCoursesUg1.SelectedValue == "0")
    //    {
    //        objCommon.DisplayMessage(this.updDiv, "Please Select Additional Subject 1", this.Page);
    //        ddlCoursesUg2.SelectedValue = "0";
    //    }
    //}
    #endregion
    //protected void lnkbtnHistory_Click1(object sender, EventArgs e)
    //{
    //    DataSet dsPreRequisiteCourses = objSReg.GetPrerequisiteHistory(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip));

    //    if (dsPreRequisiteCourses.Tables[0].Rows.Count > 0)
    //    {
    //        lvPreRequisiteHistory.DataSource = dsPreRequisiteCourses;
    //        lvPreRequisiteHistory.DataBind();

    //    }
    //    else
    //    {
    //        lvPreRequisiteHistory.DataSource = null;
    //        lvPreRequisiteHistory.DataBind();
    //    }

    //    mpe1.Show();
    //}
    #region Commented
    //protected void ddlgp2_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlgp2.SelectedValue != "0")
    //    {

    //        int gpfilledSeat = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(GP_NO)", "SESSIONNO=" + ddlSession.SelectedValue + "AND GP_NO=" + ddlgp2.SelectedValue));
    //        int gpintake = Convert.ToInt32(objCommon.LookUp("ACD_GP_MASTER", "INTAKE", "GPNO=" + ddlgp2.SelectedValue));


    //        int intakeAvailable = gpintake - gpfilledSeat;

    //        if (intakeAvailable <= 0)
    //        {
    //            objCommon.DisplayMessage(this.updDiv, "No Seat is available for this Subject", this.Page);

    //            /////refil and get latest seat 
    //            ddlgp2.Items.Clear();
    //            ddlgp2.Items.Add("Please Select");
    //            ddlgp2.SelectedItem.Value = "0";

    //            DataSet dsgpCourses = null;
    //            if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submit by fa
    //            {
    //                dsgpCourses = objSReg.GetGPCourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 1);  // 1 for fa
    //            }
    //            else
    //            {
    //                dsgpCourses = objSReg.GetGPCourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 2);  // 2 for student
    //            }


    //            DataView dvgpCourse2 = new DataView(dsgpCourses.Tables[0], "CCODE ='MBL103'", "course_name", DataViewRowState.CurrentRows);
    //            DataTable dt2 = dvgpCourse2.ToTable();
    //            ddlgp2.DataSource = dt2;
    //            ddlgp2.DataTextField = dt2.Columns["gpname"].ToString();
    //            ddlgp2.DataValueField = dt2.Columns["gpno"].ToString();
    //            ddlgp2.DataBind();
    //        }
    //    }
    //}
    //protected void ddlgp1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlgp1.SelectedValue != "0")
    //    {
    //        int gpfilledSeat = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(GP_NO)", "SESSIONNO=" + ddlSession.SelectedValue + "AND    GP_NO=" + ddlgp1.SelectedValue));
    //        int gpintake = Convert.ToInt32(objCommon.LookUp("ACD_GP_MASTER", "INTAKE", "GPNO=" + ddlgp1.SelectedValue));

    //        int intakeAvailable = gpintake - gpfilledSeat;

    //        if (intakeAvailable <= 0)
    //        {
    //            objCommon.DisplayMessage(this.updDiv, "No Seat is available for this Subject", this.Page);

    //            ddlgp1.Items.Clear();
    //            ddlgp1.Items.Add("Please Select");
    //            ddlgp1.SelectedItem.Value = "0";

    //            DataSet dsgpCourses = null;
    //            if ((Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5")) && ViewState["ACCEPT"].ToString() != "0")//faculty   after submit by fa
    //            {
    //                dsgpCourses = objSReg.GetGPCourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 1);  // 1 for fa
    //            }
    //            else
    //            {
    //                dsgpCourses = objSReg.GetGPCourseForReg(Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip), 2);  // 2 for student
    //            }


    //            DataView dvgpCourse1 = new DataView(dsgpCourses.Tables[0], "CCODE ='MBL102'", "course_name", DataViewRowState.CurrentRows);
    //            DataTable dt1 = dvgpCourse1.ToTable();
    //            ddlgp1.DataSource = dt1;
    //            ddlgp1.DataTextField = dt1.Columns["gpname"].ToString();
    //            ddlgp1.DataValueField = dt1.Columns["gpno"].ToString();
    //            ddlgp1.DataBind();

    //        }
    //    }
    //}
    #endregion

    protected void lnkSemPromotion_Click(object sender, EventArgs e)
    {
        StudentRegist objSR = new StudentRegist();
        int semno = 0, idtype = 0;
        objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
        objSR.IDNO = Convert.ToInt32(Session["idno"].ToString());
        string ipAddress = Request.ServerVariables["REMOTE_HOST"];
        Session["ipAddress"] = ipAddress;
        objSR.IPADDRESS = Session["ipAddress"].ToString();
        objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
        objSR.COLLEGE_CODE = Session["colcode"].ToString();
        objSR.ROLLNO = objCommon.LookUp("ACD_STUDENT", "ROLLNO", "ENROLLNO='" + txtRollNo.Text.Trim() + "'");
        int ret = 0;
        string sem = objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "ENROLLNO='" + txtRollNo.Text.Trim() + "'");
        //---- Below code is commented on 12062017 to remove sem promotion provision on Subject registration. 

        if (sem == "1")// && sem !="3")
        {
            #region Commented
            //string chk = objCommon.LookUp("ACD_SEM_PROMOTION", "COUNT(1)", "IDNO=" + Session["idno"].ToString() + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
            ////if (chk == "0")//checked for multiple clicked
            //{
            //int chkEligibility = objSReg.checkEligibilityForSemPromotion(objSR);


            // int idtype = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "1", "IDTYPE=3 AND SEMESTERNO=3 AND IDNO=" + Session["idno"].ToString()));
            #endregion
            semno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(SEMESTERNO,0)", "IDNO=" + Session["idno"].ToString()));
            if (semno >= 3)
            {
                idtype = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "(CASE WHEN ISNULL(IDTYPE,0) = 3 THEN 1 ELSE 0 END)AS IDTYPE", "SEMESTERNO=" + Convert.ToInt32(semno) + " AND IDNO=" + Session["idno"].ToString()));
                //                idtype = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "CASE WHEN ISNULL(IDTYPE,0) = 3 THEN 1 ELSE 0 END ISNULL(IDTYPE,0)", "SEMESTERNO=3 AND IDNO=" + Session["idno"].ToString()));

            }
            int idtype1 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(IDTYPE,0)", "IDNO=" + Session["idno"].ToString()));
            #region Commented
            //if (chkEligibility == 1 || idtype == 1 || idtype1 == 19)
            //{
            //    // ret = objSReg.SemesterPromotion(objSR);
            //    divdisplayHide(); //newly added 12062017
            //}
            //else
            //{
            //    objCommon.DisplayMessage(this.updDiv, "Your not eligible to promote to next Term", this.Page);
            //}


            //}

            //if (ret > 0)//if promotion is done  
            //{
            //    divdisplayHide();
            //}
            #endregion
            //if (chk != "0")//if promotion is already done
            //{
            divdisplayHide();
            //}
        }
        else
        {
            divdisplayHide();
        }
        //nothing will show if promotion is not done
        // above two if condition is added for multiple click on sem promotion button
    }

    public void divdisplayHide()
    {
        divSemPromotion.Visible = false;
        divStudHistory.Visible = true;
        tblInfo.Visible = true;
        divButton.Visible = true;
        divCurrent_back.Visible = true;
        divelective.Visible = true;
        divelective2.Visible = true;
        divelective3.Visible = true;
        divelective4.Visible = true;
        divelective5.Visible = true;
        divelective6.Visible = true;
        divelective7.Visible = true;
        divelective8.Visible = true;
        divelective9.Visible = true;
        divelective10.Visible = true;
        divelective11.Visible = true;
        divelective12.Visible = true;
        divelective13.Visible = true;
        divelective14.Visible = true;
        divelective15.Visible = true;
        //divadditional.Visible = true;
        divMovableSubjects.Visible = true;
        ShowDetails();
    }

    //protected void btnDropTerm_Click(object sender, EventArgs e)
    //{
    //    StudentRegist objStudR = new StudentRegist();

    //    objStudR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
    //    objStudR.IDNO = Convert.ToInt32(lblName.ToolTip);
    //    objStudR.SCHEMENO = Convert.ToInt32(lblBranch.ToolTip); //  sending Branchno in Scheme parameter
    //    objStudR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip);
    //    objStudR.REGNO = lblEnrollNo.Text;

    //    // CHECK IF STUDENT APPLIED FOR DROP TERM (IN CASE OF "NOT REJECTED")
    //    int ifExist = Convert.ToInt32(objCommon.LookUp("ACD_TERM_DROP", "COUNT(*)", "IDNO=" + objStudR.IDNO + " AND SEMESTERNO=" + objStudR.SEMESTERNO));//+ " AND ISNULL(HOD_APPROVE,'A') = 'A' AND ISNULL(DIR_APPROVE,'A') = 'A' ")
    //    if (ifExist > 0)
    //    {
    //        objCommon.DisplayMessage(this.updDiv, "You have Already applied for the Drop Term of Term : " + objStudR.SEMESTERNO, this.Page);
    //        // ScriptManager.RegisterStartupScript(this.updDiv, this.updDiv.GetType(), "alert", "alert('" + "You have Already applied for the Drop Term of Term " + objStudR.SEMESTERNO + ");'';", true);
    //        //return;        
    //    }
    //    else
    //    {
    //        int res = objSReg.AddDropTermRecord(objStudR, Convert.ToInt32(lblDegree.ToolTip));
    //        if (res != -99)
    //        {
    //            objCommon.DisplayMessage(this.updDiv, "You have Successfully applied for the Drop Term of Term : " + objStudR.SEMESTERNO, this.Page);
    //            // ScriptManager.RegisterStartupScript(this.updDiv, this.updDiv.GetType(), "alert", "alert('" + "You have Successfully applied for the Drop Term of 'Term " + objStudR.SEMESTERNO + "');'';", true);

    //        }
    //    }

    //}

    //protected void ddlOpenElect_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlOpenElect.SelectedIndex > 0)
    //    {

    //        #region Start
    //        decimal cred = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "ISNULL(CREDITS,0)", "COURSENO = " + ddlOpenElect.SelectedValue));

    //        int totalsub = Convert.ToInt32(txtAllSubjects.Text);
    //        decimal totSubjectCredit = Convert.ToDecimal(txtCredits.Text);

    //        totSubjectCredit = totSubjectCredit + Convert.ToDecimal(cred);
    //        totalsub = totalsub + 1;

    //        if (totSubjectCredit > Convert.ToDecimal(lblMaxCreditlimit.Text))
    //        {
    //            totSubjectCredit = totSubjectCredit - Convert.ToDecimal(cred);
    //            totalsub = totalsub - 1;
    //            ddlOpenElect.SelectedValue = "0";

    //            txtAllSubjects.Text = CheckTotalSubjects().ToString();
    //            txtCredits.Text = CheckTotalCredits().ToString();

    //            objCommon.DisplayMessage(this.updDiv, "Maximum " + lblMaxCreditlimit.Text + "  credits are allowed for registration", this.Page);
    //            return;
    //        }

    //        #endregion

    //        int opintake = 0;
    //        int opfilledSeat = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(COURSENO)", "SESSIONNO=" + ddlSession.SelectedValue + "AND COURSENO=" + ddlOpenElect.SelectedValue));

    //        string opCCode = objCommon.LookUp("ACD_OPEN_ELECT_INTAKE_MASTER", "CCODE", "COURSENO=" + ddlOpenElect.SelectedValue);
    //        if (string.IsNullOrEmpty(opCCode))
    //        {
    //            opCCode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlOpenElect.SelectedValue);
    //        }
    //        if (!string.IsNullOrEmpty(opCCode))
    //        {
    //            opintake = objCommon.LookUp("ACD_OPEN_ELECT_INTAKE_MASTER", "ISNULL(INTAKE,0)", "CCODE='" + opCCode + "' AND SESSIONNO =" + ddlSession.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_OPEN_ELECT_INTAKE_MASTER", "ISNULL(INTAKE,0)", "CCODE='" + opCCode + "' AND SESSIONNO =" + ddlSession.SelectedValue));
    //        }
    //        if (opintake == 0)
    //        {
    //            objCommon.DisplayMessage(this.updDiv, "Intake Not define for selected Subject!" + opCCode, this.Page);
    //        }
    //        int intakeAvailable = opintake - opfilledSeat;

    //        if (intakeAvailable <= 0)
    //        {
    //            objCommon.DisplayMessage(this.updDiv, "No Seat is available for this Subject", this.Page);

    //            //refil and get latest seat 
    //            fillOpenElectiveCourses();
    //        }
    //        else
    //        {
    //            //decimal cred = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "CREDITS", "COURSENO = " + ddlOpenElect.SelectedValue));
    //            //txtCredits.Text = (Convert.ToDecimal(txtCredits.Text) + cred).ToString();                
    //        }
    //    }
    //    txtAllSubjects.Text = CheckTotalSubjects().ToString();
    //    txtCredits.Text = CheckTotalCredits().ToString();
    //}

    protected void lvDetained_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlNewsec = e.Item.FindControl("ddlsection") as DropDownList;
            objCommon.FillDropDownList(ddlNewsec, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNAME");
            ddlNewsec.SelectedValue = lblSection.ToolTip;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                CheckBox chkcurrent = (CheckBox)e.Item.FindControl("chkDetain");
                if (chkcurrent.Checked)
                {
                    txtAllSubjects.Text = Convert.ToString((Convert.ToInt32(txtAllSubjects.Text) + 1));
                    txtCredits.Text = Convert.ToString(Convert.ToDecimal(txtCredits.Text) + Convert.ToDecimal(((Label)e.Item.FindControl("lblCredits")).Text));

                    ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;//as zerp is not vailable in,list 0 will not get set

                    if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
                    {
                        if (ViewState["ACCEPT"].ToString() != "0")//accepted
                        {
                            // lvDetained.Enabled = false;
                            btnSubmit.Enabled = false;
                        }
                        else
                        {
                            //lvDetained.Enabled = true;
                            btnSubmit.Enabled = true;
                        }
                    }
                    else
                    {
                        //  lvDetained.Enabled = false;
                        btnSubmit.Enabled = false;
                    }
                }

            }
            //txtAllSubjects.Text = CheckTotalSubjects().ToString();            
            //txtCredits.Text = CheckTotalCredits().ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration_SingleStudent --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lvCurrentSubjects_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
            DropDownList ddlCoreSection = cph.FindControl("ddlCoreSection") as DropDownList;
            DropDownList ddlNewsec = e.Item.FindControl("ddlsection") as DropDownList;      //Added by Abhinay Lad [21-06-2019]
            CheckBox chkAction = e.Item.FindControl("chkAccept") as CheckBox;
            chkAction.Checked = true;
            chkAction.Enabled = false;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField ccCode = (HiddenField)e.Item.FindControl("hdfCCode");

                string tableName = "ACD_COURSE_TEACHER CT INNER JOIN USER_aCC U ON (U.UA_NO=CT.UA_NO OR U.UA_NO=CT.ADTEACHER) INNER JOIN ACD_SECTION S ON S.SECTIONNO=CT.SECTIONNO";
                string paramter_1 = "DISTINCT (CAST(S.SECTIONNO AS VARCHAR)+' - '+ CAST(COALESCE(NULLIF(CT.UA_NO,''), CT.ADTEACHER) AS VARCHAR)+' - '+CAST(ISNULL(CT.INTAKE,0) AS VARCHAR)+' - '+ CAST((SELECT COUNT(*) FROM ACD_STUDENT_RESULT WHERE schemeno=CT.SCHEMENO and courseno=CT.COURSENO and sessionno=CT.SESSIONNO AND SECTIONNO=CT.SECTIONNO AND ISNULL(CANCEL,0)=0) AS VARCHAR)) as SECTIONNO";
                string parameter_2 = "'[ '+S.SECTIONNAME+' ] - '+U.UA_FULLNAME AS UA_FULLNAME,CT.SECTIONNO as SECNO";
                string whereCondition = "SESSIONNO = " + (Convert.ToInt32(ddlSession.SelectedValue)) + " AND CT.COURSENO=" + ccCode.Value + " AND ISNULL(CANCEL,0)=0 AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + " AND CT.UA_NO IS NOT NULL";
                string orderByCondition = "CT.SECTIONNO";

                // For Main Dropdown
                string paramter_1_Section = "DISTINCT CT.SECTIONNO";
                string parameter_2_Section = "'Section - '+S.SECTIONNAME";

                objCommon.FillDropDownList(ddlCoreSection, tableName, paramter_1_Section, parameter_2_Section, whereCondition, orderByCondition);
                // For Section Teacher Dropdown
                objCommon.FillDropDownList(ddlNewsec, tableName, paramter_1, parameter_2, whereCondition, orderByCondition);        //Added by Abhinat Lad [19-06-2019]
                ddlNewsec.SelectedValue = lblSection.ToolTip;

                lnkBtnTimeTable.Text = "Time Table for Section" + ddlCoreSection.SelectedItem.Text.Split('-')[1];
              //  ddlNewsec.Enabled = false;

                Label Intake = (Label)e.Item.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                Label Filled = (Label)e.Item.FindControl("lblFilled");

                Intake.Text = ddlNewsec.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                Filled.Text = ddlNewsec.SelectedValue.ToString().Split('-')[3].Trim().ToString();


                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                {
                    Intake.CssClass = "label label-default";
                    Filled.CssClass = "label label-default";

                    int CalcVal = 0;
                    for (int i = 0; i < ddlNewsec.Items.Count; i++)
                    {
                        if (Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[3].Trim().ToString()))
                        {
                            CalcVal++;
                        }
                    }

                    if (CalcVal == ddlNewsec.Items.Count)
                    {
                        //CheckBox chkSelectCourse = (CheckBox)e.Item.FindControl("chkAccept");
                        //chkSelectCourse.Enabled = false;
                       // ddlNewsec.Enabled = false;
                        lvElectiveCourse.Enabled = false;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-danger";
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-warning";
                }
                else
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-success";
                }

                #region Commented
                //CheckBox chkcurrent = (CheckBox)e.Item.FindControl("chkAccept");
                //if (chkcurrent.Checked)
                //{
                //    txtAllSubjects.Text = Convert.ToString(Convert.ToInt32(txtAllSubjects.Text) + 1);
                //    txtCredits.Text = Convert.ToString(Convert.ToDecimal(txtCredits.Text) + Convert.ToDecimal(((Label)e.Item.FindControl("lblCredits")).Text));

                //    ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;
                //    if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
                //    {
                //        if (ViewState["ACCEPT"].ToString() != "0")//accepted
                //        {
                //            lvCurrentSubjects.Enabled = false;
                //            btnSubmit.Enabled = false;
                //            lvDetained.Enabled = false;
                //        }
                //        else
                //        {
                //            lvCurrentSubjects.Enabled = true;
                //            btnSubmit.Enabled = true;
                //            lvDetained.Enabled = true;
                //        }
                //    }
                //    else
                //    {
                //        lvCurrentSubjects.Enabled = false;
                //        btnSubmit.Enabled = false;
                //        lvDetained.Enabled = false;
                //    }
                //}
                #endregion
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration_SingleStudent --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lvElectiveCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlNewsec = e.Item.FindControl("ddlsection") as DropDownList;      //Added by Abhinay Lad [21-06-2019]
          //  ddlNewsec.Enabled = false;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField ccCode = (HiddenField)e.Item.FindControl("hdfCCode");

                string tableName = "ACD_COURSE_TEACHER CT INNER JOIN USER_aCC U ON (U.UA_NO=CT.UA_NO OR U.UA_NO=CT.ADTEACHER) INNER JOIN ACD_SECTION S ON S.SECTIONNO=CT.SECTIONNO";
                string paramter_1 = "DISTINCT (CAST(S.SECTIONNO AS VARCHAR)+' - '+ CAST(COALESCE(NULLIF(CT.UA_NO,''), CT.ADTEACHER) AS VARCHAR)+' - '+CAST(ISNULL(CT.INTAKE,0) AS VARCHAR)+' - '+ CAST((SELECT COUNT(*) FROM ACD_STUDENT_RESULT WHERE schemeno=CT.SCHEMENO and courseno=CT.COURSENO and sessionno=CT.SESSIONNO AND SECTIONNO=CT.SECTIONNO  AND ISNULL(CANCEL,0)=0) AS VARCHAR)) as SECTIONNO";
                string parameter_2 = "'[ '+S.SECTIONNAME+' ] - '+U.UA_FULLNAME AS UA_FULLNAME,CT.SECTIONNO as SECNO";
                string whereCondition = "SESSIONNO = " + (Convert.ToInt32(ddlSession.SelectedValue)) + " AND CT.COURSENO=" + ccCode.Value + " AND ISNULL(CANCEL,0)=0 AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + "";
                string orderByCondition = "CT.SECTIONNO";

                objCommon.FillDropDownList(ddlNewsec, tableName, paramter_1, parameter_2, whereCondition, orderByCondition);        //Added by Abhinat Lad [19-06-2019]
                ddlNewsec.SelectedValue = lblSection.ToolTip;

                Label Intake = (Label)e.Item.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                Label Filled = (Label)e.Item.FindControl("lblFilled");

                Intake.Text = ddlNewsec.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                Filled.Text = ddlNewsec.SelectedValue.ToString().Split('-')[3].Trim().ToString();


                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                {
                    Intake.CssClass = "label label-default";
                    Filled.CssClass = "label label-default";

                    int CalcVal = 0;
                    for (int i = 0; i < ddlNewsec.Items.Count; i++)
                    {
                        if (Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[3].Trim().ToString()))
                        {
                            CalcVal++;
                        }
                    }

                    if (CalcVal == ddlNewsec.Items.Count)
                    {
                        CheckBox chkSelectCourse = (CheckBox)e.Item.FindControl("chkAccept");
                        chkSelectCourse.Enabled = false;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-danger";
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-warning";
                }
                else
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-success";
                }

                #region Hidden
                CheckBox chkcurrent = (CheckBox)e.Item.FindControl("chkElectiveGrp1");
                if (chkcurrent.Checked)
                {
                    txtAllSubjects.Text = Convert.ToString(Convert.ToInt32(txtAllSubjects.Text) + 1);

                    txtCredits.Text = Convert.ToString(Convert.ToDecimal(txtCredits.Text) + Convert.ToDecimal(((Label)e.Item.FindControl("lblCredits")).Text));

                    ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;

                    if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
                    {
                        if (ViewState["ACCEPT"].ToString() != "0")//accepted
                        {
                            lvCurrentSubjects.Enabled = false;
                            lvElectiveCourse.Enabled = false;
                            btnSubmit.Enabled = false;
                            lvDetained.Enabled = false;

                            lvElectiveCourseGrp2.Enabled = false;
                            lvElectiveCourseGrp3.Enabled = false;
                            lvElectiveCourseGrp4.Enabled = false;

                        }
                        else
                        {
                            lvCurrentSubjects.Enabled = true;
                            lvElectiveCourse.Enabled = true;
                            btnSubmit.Enabled = true;
                            lvDetained.Enabled = true;

                            lvElectiveCourseGrp2.Enabled = true;
                            lvElectiveCourseGrp3.Enabled = true;
                            lvElectiveCourseGrp4.Enabled = true;
                        }
                    }
                    else
                    {
                        ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;
                        lvCurrentSubjects.Enabled = false;
                        lvElectiveCourse.Enabled = false;
                        btnSubmit.Enabled = false;
                        lvDetained.Enabled = false;

                        lvElectiveCourseGrp2.Enabled = false;
                        lvElectiveCourseGrp3.Enabled = false;
                        lvElectiveCourseGrp4.Enabled = false;

                    }
                }
                #endregion
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration_SingleStudent --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lvElectiveCourseGrp2_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlNewsec = e.Item.FindControl("ddlsection") as DropDownList;      //Added by Abhinay Lad [21-06-2019]
          //  ddlNewsec.Enabled = false;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField ccCode = (HiddenField)e.Item.FindControl("hdfCCode");

                string tableName = "ACD_COURSE_TEACHER CT INNER JOIN USER_aCC U ON (U.UA_NO=CT.UA_NO OR U.UA_NO=CT.ADTEACHER) INNER JOIN ACD_SECTION S ON S.SECTIONNO=CT.SECTIONNO";
                string paramter_1 = "DISTINCT (CAST(S.SECTIONNO AS VARCHAR)+' - '+ CAST(COALESCE(NULLIF(CT.UA_NO,''), CT.ADTEACHER) AS VARCHAR)+' - '+CAST(ISNULL(CT.INTAKE,0) AS VARCHAR)+' - '+ CAST((SELECT COUNT(*) FROM ACD_STUDENT_RESULT WHERE schemeno=CT.SCHEMENO and courseno=CT.COURSENO and sessionno=CT.SESSIONNO AND SECTIONNO=CT.SECTIONNO  AND ISNULL(CANCEL,0)=0) AS VARCHAR)) as SECTIONNO";
                string parameter_2 = "'[ '+S.SECTIONNAME+' ] - '+U.UA_FULLNAME AS UA_FULLNAME,CT.SECTIONNO as SECNO";
                string whereCondition = "SESSIONNO = " + (Convert.ToInt32(ddlSession.SelectedValue)) + " AND CT.COURSENO=" + ccCode.Value + " AND ISNULL(CANCEL,0)=0 AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + "";
                string orderByCondition = "CT.SECTIONNO";

                objCommon.FillDropDownList(ddlNewsec, tableName, paramter_1, parameter_2, whereCondition, orderByCondition);        //Added by Abhinat Lad [19-06-2019]
                ddlNewsec.SelectedValue = lblSection.ToolTip;

                Label Intake = (Label)e.Item.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                Label Filled = (Label)e.Item.FindControl("lblFilled");

                Intake.Text = ddlNewsec.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                Filled.Text = ddlNewsec.SelectedValue.ToString().Split('-')[3].Trim().ToString();


                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                {
                    Intake.CssClass = "label label-default";
                    Filled.CssClass = "label label-default";

                    int CalcVal = 0;
                    for (int i = 0; i < ddlNewsec.Items.Count; i++)
                    {
                        if (Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[3].Trim().ToString()))
                        {
                            CalcVal++;
                        }
                    }

                    if (CalcVal == ddlNewsec.Items.Count)
                    {
                        CheckBox chkSelectCourse = (CheckBox)e.Item.FindControl("chkAccept");
                        chkSelectCourse.Enabled = false;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-danger";
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-warning";
                }
                else
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-success";
                }



                CheckBox chkcurrent = (CheckBox)e.Item.FindControl("chkElectiveGrp2");
                if (chkcurrent.Checked)
                {
                    txtAllSubjects.Text = Convert.ToString(Convert.ToInt32(txtAllSubjects.Text) + 1);

                    txtCredits.Text = Convert.ToString(Convert.ToDecimal(txtCredits.Text) + Convert.ToDecimal(((Label)e.Item.FindControl("lblCredits")).Text));

                    ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;

                    if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
                    {
                        if (ViewState["ACCEPT"].ToString() != "0")//accepted
                        {
                            lvCurrentSubjects.Enabled = false;
                            lvElectiveCourse.Enabled = false;
                            btnSubmit.Enabled = false;
                            lvDetained.Enabled = false;

                            lvElectiveCourseGrp2.Enabled = false;
                            lvElectiveCourseGrp3.Enabled = false;
                            lvElectiveCourseGrp4.Enabled = false;

                        }
                        else
                        {
                            lvCurrentSubjects.Enabled = true;
                            lvElectiveCourse.Enabled = true;
                            btnSubmit.Enabled = true;
                            lvDetained.Enabled = true;

                            lvElectiveCourseGrp2.Enabled = true;
                            lvElectiveCourseGrp3.Enabled = true;
                            lvElectiveCourseGrp4.Enabled = true;
                        }
                    }
                    else
                    {
                        ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;
                        lvCurrentSubjects.Enabled = false;
                        lvElectiveCourse.Enabled = false;
                        btnSubmit.Enabled = false;
                        lvDetained.Enabled = false;

                        lvElectiveCourseGrp2.Enabled = false;
                        lvElectiveCourseGrp3.Enabled = false;
                        lvElectiveCourseGrp4.Enabled = false;

                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration_SingleStudent --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lvElectiveCourseGrp3_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlNewsec = e.Item.FindControl("ddlsection") as DropDownList;      //Added by Abhinay Lad [21-06-2019]
          //  ddlNewsec.Enabled = false;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {

                HiddenField ccCode = (HiddenField)e.Item.FindControl("hdfCCode");

                string tableName = "ACD_COURSE_TEACHER CT INNER JOIN USER_aCC U ON (U.UA_NO=CT.UA_NO OR U.UA_NO=CT.ADTEACHER) INNER JOIN ACD_SECTION S ON S.SECTIONNO=CT.SECTIONNO";
                string paramter_1 = "DISTINCT (CAST(S.SECTIONNO AS VARCHAR)+' - '+ CAST(COALESCE(NULLIF(CT.UA_NO,''), CT.ADTEACHER) AS VARCHAR)+' - '+CAST(ISNULL(CT.INTAKE,0) AS VARCHAR)+' - '+ CAST((SELECT COUNT(*) FROM ACD_STUDENT_RESULT WHERE schemeno=CT.SCHEMENO and courseno=CT.COURSENO and sessionno=CT.SESSIONNO AND SECTIONNO=CT.SECTIONNO  AND ISNULL(CANCEL,0)=0) AS VARCHAR)) as SECTIONNO";
                string parameter_2 = "'[ '+S.SECTIONNAME+' ] - '+U.UA_FULLNAME AS UA_FULLNAME,CT.SECTIONNO as SECNO";
                string whereCondition = "SESSIONNO = " + (Convert.ToInt32(ddlSession.SelectedValue)) + " AND CT.COURSENO=" + ccCode.Value + " AND ISNULL(CANCEL,0)=0 AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + "";
                string orderByCondition = "CT.SECTIONNO";

                objCommon.FillDropDownList(ddlNewsec, tableName, paramter_1, parameter_2, whereCondition, orderByCondition);        //Added by Abhinat Lad [19-06-2019]
                ddlNewsec.SelectedValue = lblSection.ToolTip;

                Label Intake = (Label)e.Item.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                Label Filled = (Label)e.Item.FindControl("lblFilled");

                Intake.Text = ddlNewsec.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                Filled.Text = ddlNewsec.SelectedValue.ToString().Split('-')[3].Trim().ToString();


                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                {
                    Intake.CssClass = "label label-default";
                    Filled.CssClass = "label label-default";

                    int CalcVal = 0;
                    for (int i = 0; i < ddlNewsec.Items.Count; i++)
                    {
                        if (Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[3].Trim().ToString()))
                        {
                            CalcVal++;
                        }
                    }

                    if (CalcVal == ddlNewsec.Items.Count)
                    {
                        CheckBox chkSelectCourse = (CheckBox)e.Item.FindControl("chkAccept");
                        chkSelectCourse.Enabled = false;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-danger";
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-warning";
                }
                else
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-success";
                }



                CheckBox chkcurrent = (CheckBox)e.Item.FindControl("chkElectiveGrp3");
                if (chkcurrent.Checked)
                {
                    txtAllSubjects.Text = Convert.ToString(Convert.ToInt32(txtAllSubjects.Text) + 1);

                    txtCredits.Text = Convert.ToString(Convert.ToDecimal(txtCredits.Text) + Convert.ToDecimal(((Label)e.Item.FindControl("lblCredits")).Text));

                    ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;

                    if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
                    {
                        if (ViewState["ACCEPT"].ToString() != "0")//accepted
                        {
                            lvCurrentSubjects.Enabled = false;
                            lvElectiveCourse.Enabled = false;
                            btnSubmit.Enabled = false;
                            lvDetained.Enabled = false;

                            lvElectiveCourseGrp2.Enabled = false;
                            lvElectiveCourseGrp3.Enabled = false;
                            lvElectiveCourseGrp4.Enabled = false;

                        }
                        else
                        {
                            lvCurrentSubjects.Enabled = true;
                            lvElectiveCourse.Enabled = true;
                            btnSubmit.Enabled = true;
                            lvDetained.Enabled = true;

                            lvElectiveCourseGrp2.Enabled = true;
                            lvElectiveCourseGrp3.Enabled = true;
                            lvElectiveCourseGrp4.Enabled = true;
                        }
                    }
                    else
                    {
                        ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;
                        lvCurrentSubjects.Enabled = false;
                        lvElectiveCourse.Enabled = false;
                        btnSubmit.Enabled = false;
                        lvDetained.Enabled = false;

                        lvElectiveCourseGrp2.Enabled = false;
                        lvElectiveCourseGrp3.Enabled = false;
                        lvElectiveCourseGrp4.Enabled = false;

                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration_SingleStudent --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lvElectiveCourseGrp4_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlNewsec = e.Item.FindControl("ddlsection") as DropDownList;      //Added by Abhinay Lad [21-06-2019]
           // ddlNewsec.Enabled = false;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {

                HiddenField ccCode = (HiddenField)e.Item.FindControl("hdfCCode");

                string tableName = "ACD_COURSE_TEACHER CT INNER JOIN USER_aCC U ON (U.UA_NO=CT.UA_NO OR U.UA_NO=CT.ADTEACHER) INNER JOIN ACD_SECTION S ON S.SECTIONNO=CT.SECTIONNO";
                string paramter_1 = "DISTINCT (CAST(S.SECTIONNO AS VARCHAR)+' - '+ CAST(COALESCE(NULLIF(CT.UA_NO,''), CT.ADTEACHER) AS VARCHAR)+' - '+CAST(ISNULL(CT.INTAKE,0) AS VARCHAR)+' - '+ CAST((SELECT COUNT(*) FROM ACD_STUDENT_RESULT WHERE schemeno=CT.SCHEMENO and courseno=CT.COURSENO and sessionno=CT.SESSIONNO AND SECTIONNO=CT.SECTIONNO AND ISNULL(CANCEL,0)=0) AS VARCHAR)) as SECTIONNO";
                string parameter_2 = "'[ '+S.SECTIONNAME+' ] - '+U.UA_FULLNAME AS UA_FULLNAME,CT.SECTIONNO as SECNO";
                string whereCondition = "SESSIONNO = " + (Convert.ToInt32(ddlSession.SelectedValue)) + " AND CT.COURSENO=" + ccCode.Value + " AND ISNULL(CANCEL,0)=0 AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + "";
                string orderByCondition = "CT.SECTIONNO";

                objCommon.FillDropDownList(ddlNewsec, tableName, paramter_1, parameter_2, whereCondition, orderByCondition);        //Added by Abhinat Lad [19-06-2019]
                ddlNewsec.SelectedValue = lblSection.ToolTip;

                Label Intake = (Label)e.Item.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                Label Filled = (Label)e.Item.FindControl("lblFilled");

                Intake.Text = ddlNewsec.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                Filled.Text = ddlNewsec.SelectedValue.ToString().Split('-')[3].Trim().ToString();


                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                {
                    Intake.CssClass = "label label-default";
                    Filled.CssClass = "label label-default";

                    int CalcVal = 0;
                    for (int i = 0; i < ddlNewsec.Items.Count; i++)
                    {
                        if (Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[3].Trim().ToString()))
                        {
                            CalcVal++;
                        }
                    }

                    if (CalcVal == ddlNewsec.Items.Count)
                    {
                        CheckBox chkSelectCourse = (CheckBox)e.Item.FindControl("chkAccept");
                        chkSelectCourse.Enabled = false;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-danger";
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-warning";
                }
                else
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-success";
                }



                CheckBox chkcurrent = (CheckBox)e.Item.FindControl("chkElectiveGrp4");
                if (chkcurrent.Checked)
                {
                    txtAllSubjects.Text = Convert.ToString(Convert.ToInt32(txtAllSubjects.Text) + 1);

                    txtCredits.Text = Convert.ToString(Convert.ToDecimal(txtCredits.Text) + Convert.ToDecimal(((Label)e.Item.FindControl("lblCredits")).Text));

                    ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;

                    if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
                    {
                        if (ViewState["ACCEPT"].ToString() != "0")//accepted
                        {
                            lvCurrentSubjects.Enabled = false;
                            lvElectiveCourse.Enabled = false;
                            btnSubmit.Enabled = false;
                            lvDetained.Enabled = false;

                            lvElectiveCourseGrp2.Enabled = false;
                            lvElectiveCourseGrp3.Enabled = false;
                            lvElectiveCourseGrp4.Enabled = false;

                        }
                        else
                        {
                            lvCurrentSubjects.Enabled = true;
                            lvElectiveCourse.Enabled = true;
                            btnSubmit.Enabled = true;
                            lvDetained.Enabled = true;

                            lvElectiveCourseGrp2.Enabled = true;
                            lvElectiveCourseGrp3.Enabled = true;
                            lvElectiveCourseGrp4.Enabled = true;

                        }
                    }
                    else
                    {
                        ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;
                        lvCurrentSubjects.Enabled = false;
                        lvElectiveCourse.Enabled = false;
                        btnSubmit.Enabled = false;
                        lvDetained.Enabled = false;

                        lvElectiveCourseGrp2.Enabled = false;
                        lvElectiveCourseGrp3.Enabled = false;
                        lvElectiveCourseGrp4.Enabled = false;

                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration_SingleStudent --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lvElectiveCourseGrp5_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlNewsec = e.Item.FindControl("ddlsection") as DropDownList;
           // ddlNewsec.Enabled = false;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField ccCode = (HiddenField)e.Item.FindControl("hdfCCode");

                string tableName = "ACD_COURSE_TEACHER CT INNER JOIN USER_aCC U ON (U.UA_NO=CT.UA_NO OR U.UA_NO=CT.ADTEACHER) INNER JOIN ACD_SECTION S ON S.SECTIONNO=CT.SECTIONNO";
                string paramter_1 = "DISTINCT (CAST(S.SECTIONNO AS VARCHAR)+' - '+ CAST(COALESCE(NULLIF(CT.UA_NO,''), CT.ADTEACHER) AS VARCHAR)+' - '+CAST(ISNULL(CT.INTAKE,0) AS VARCHAR)+' - '+ CAST((SELECT COUNT(*) FROM ACD_STUDENT_RESULT WHERE schemeno=CT.SCHEMENO and courseno=CT.COURSENO and sessionno=CT.SESSIONNO AND SECTIONNO=CT.SECTIONNO  AND ISNULL(CANCEL,0)=0) AS VARCHAR)) as SECTIONNO";
                string parameter_2 = "'[ '+S.SECTIONNAME+' ] - '+U.UA_FULLNAME AS UA_FULLNAME,CT.SECTIONNO as SECNO";
                string whereCondition = "SESSIONNO = " + (Convert.ToInt32(ddlSession.SelectedValue)) + " AND CT.COURSENO=" + ccCode.Value + " AND ISNULL(CANCEL,0)=0 AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + "";
                string orderByCondition = "CT.SECTIONNO";

                objCommon.FillDropDownList(ddlNewsec, tableName, paramter_1, parameter_2, whereCondition, orderByCondition);
                ddlNewsec.SelectedValue = lblSection.ToolTip;

                Label Intake = (Label)e.Item.FindControl("lblIntake");
                Label Filled = (Label)e.Item.FindControl("lblFilled");

                Intake.Text = ddlNewsec.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                Filled.Text = ddlNewsec.SelectedValue.ToString().Split('-')[3].Trim().ToString();


                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))
                {
                    Intake.CssClass = "label label-default";
                    Filled.CssClass = "label label-default";

                    int CalcVal = 0;
                    for (int i = 0; i < ddlNewsec.Items.Count; i++)
                    {
                        if (Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[3].Trim().ToString()))
                        {
                            CalcVal++;
                        }
                    }

                    if (CalcVal == ddlNewsec.Items.Count)
                    {
                        CheckBox chkSelectCourse = (CheckBox)e.Item.FindControl("chkAccept");
                        chkSelectCourse.Enabled = false;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-danger";
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-warning";
                }
                else
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-success";
                }



                CheckBox chkcurrent = (CheckBox)e.Item.FindControl("chkElectiveGrp5");
                if (chkcurrent.Checked)
                {
                    txtAllSubjects.Text = Convert.ToString(Convert.ToInt32(txtAllSubjects.Text) + 1);

                    txtCredits.Text = Convert.ToString(Convert.ToDecimal(txtCredits.Text) + Convert.ToDecimal(((Label)e.Item.FindControl("lblCredits")).Text));

                    ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;

                    if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
                    {
                        if (ViewState["ACCEPT"].ToString() != "0")//accepted
                        {
                            lvCurrentSubjects.Enabled = false;
                            lvElectiveCourse.Enabled = false;
                            lvElectiveCourseGrp2.Enabled = false;
                            lvElectiveCourseGrp3.Enabled = false;
                            lvElectiveCourseGrp4.Enabled = false;
                            lvElectiveCourseGrp5.Enabled = false;
                            lvElectiveCourseGrp6.Enabled = false;
                            lvElectiveCourseGrp7.Enabled = false;
                            lvDetained.Enabled = false;
                            lvMovableSubjects.Enabled = false;
                            btnSubmit.Enabled = false;
                        }
                        else
                        {
                            lvCurrentSubjects.Enabled = true;
                            lvElectiveCourse.Enabled = true;
                            lvElectiveCourseGrp2.Enabled = true;
                            lvElectiveCourseGrp3.Enabled = true;
                            lvElectiveCourseGrp4.Enabled = true;
                            lvElectiveCourseGrp5.Enabled = true;
                            lvElectiveCourseGrp6.Enabled = true;
                            lvElectiveCourseGrp7.Enabled = true;
                            lvDetained.Enabled = true;
                            lvMovableSubjects.Enabled = true;
                            btnSubmit.Enabled = true;
                        }
                    }
                    else
                    {
                        ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;
                        lvCurrentSubjects.Enabled = false;
                        lvElectiveCourse.Enabled = false;
                        lvElectiveCourseGrp2.Enabled = false;
                        lvElectiveCourseGrp3.Enabled = false;
                        lvElectiveCourseGrp4.Enabled = false;
                        lvElectiveCourseGrp5.Enabled = false;
                        lvElectiveCourseGrp6.Enabled = false;
                        lvElectiveCourseGrp7.Enabled = false;
                        lvDetained.Enabled = false;
                        lvMovableSubjects.Enabled = false;
                        btnSubmit.Enabled = false;

                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration_SingleStudent --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lvElectiveCourseGrp6_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlNewsec = e.Item.FindControl("ddlsection") as DropDownList;
          //  ddlNewsec.Enabled = false;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField ccCode = (HiddenField)e.Item.FindControl("hdfCCode");

                string tableName = "ACD_COURSE_TEACHER CT INNER JOIN USER_aCC U ON (U.UA_NO=CT.UA_NO OR U.UA_NO=CT.ADTEACHER) INNER JOIN ACD_SECTION S ON S.SECTIONNO=CT.SECTIONNO";
                string paramter_1 = "DISTINCT (CAST(S.SECTIONNO AS VARCHAR)+' - '+ CAST(COALESCE(NULLIF(CT.UA_NO,''), CT.ADTEACHER) AS VARCHAR)+' - '+CAST(ISNULL(CT.INTAKE,0) AS VARCHAR)+' - '+ CAST((SELECT COUNT(*) FROM ACD_STUDENT_RESULT WHERE schemeno=CT.SCHEMENO and courseno=CT.COURSENO and sessionno=CT.SESSIONNO AND SECTIONNO=CT.SECTIONNO  AND ISNULL(CANCEL,0)=0) AS VARCHAR)) as SECTIONNO";
                string parameter_2 = "'[ '+S.SECTIONNAME+' ] - '+U.UA_FULLNAME AS UA_FULLNAME,CT.SECTIONNO as SECNO";
                string whereCondition = "SESSIONNO = " + (Convert.ToInt32(ddlSession.SelectedValue)) + " AND CT.COURSENO=" + ccCode.Value + " AND ISNULL(CANCEL,0)=0 AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + "";
                string orderByCondition = "CT.SECTIONNO";

                objCommon.FillDropDownList(ddlNewsec, tableName, paramter_1, parameter_2, whereCondition, orderByCondition);
                ddlNewsec.SelectedValue = lblSection.ToolTip;

                Label Intake = (Label)e.Item.FindControl("lblIntake");
                Label Filled = (Label)e.Item.FindControl("lblFilled");

                Intake.Text = ddlNewsec.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                Filled.Text = ddlNewsec.SelectedValue.ToString().Split('-')[3].Trim().ToString();


                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))
                {
                    Intake.CssClass = "label label-default";
                    Filled.CssClass = "label label-default";

                    int CalcVal = 0;
                    for (int i = 0; i < ddlNewsec.Items.Count; i++)
                    {
                        if (Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[3].Trim().ToString()))
                        {
                            CalcVal++;
                        }
                    }

                    if (CalcVal == ddlNewsec.Items.Count)
                    {
                        CheckBox chkSelectCourse = (CheckBox)e.Item.FindControl("chkAccept");
                        chkSelectCourse.Enabled = false;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-danger";
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-warning";
                }
                else
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-success";
                }



                CheckBox chkcurrent = (CheckBox)e.Item.FindControl("chkElectiveGrp6");
                if (chkcurrent.Checked)
                {
                    txtAllSubjects.Text = Convert.ToString(Convert.ToInt32(txtAllSubjects.Text) + 1);

                    txtCredits.Text = Convert.ToString(Convert.ToDecimal(txtCredits.Text) + Convert.ToDecimal(((Label)e.Item.FindControl("lblCredits")).Text));

                    ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;

                    if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
                    {
                        if (ViewState["ACCEPT"].ToString() != "0")//accepted
                        {
                            lvCurrentSubjects.Enabled = false;
                            lvElectiveCourse.Enabled = false;
                            lvElectiveCourseGrp2.Enabled = false;
                            lvElectiveCourseGrp3.Enabled = false;
                            lvElectiveCourseGrp4.Enabled = false;
                            lvElectiveCourseGrp5.Enabled = false;
                            lvElectiveCourseGrp6.Enabled = false;
                            lvElectiveCourseGrp7.Enabled = false;
                            lvDetained.Enabled = false;
                            lvMovableSubjects.Enabled = false;
                            btnSubmit.Enabled = false;
                        }
                        else
                        {
                            lvCurrentSubjects.Enabled = true;
                            lvElectiveCourse.Enabled = true;
                            lvElectiveCourseGrp2.Enabled = true;
                            lvElectiveCourseGrp3.Enabled = true;
                            lvElectiveCourseGrp4.Enabled = true;
                            lvElectiveCourseGrp5.Enabled = true;
                            lvElectiveCourseGrp6.Enabled = true;
                            lvElectiveCourseGrp7.Enabled = true;
                            lvDetained.Enabled = true;
                            lvMovableSubjects.Enabled = true;
                            btnSubmit.Enabled = true;
                        }
                    }
                    else
                    {
                        ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;
                        lvCurrentSubjects.Enabled = false;
                        lvElectiveCourse.Enabled = false;
                        lvElectiveCourseGrp2.Enabled = false;
                        lvElectiveCourseGrp3.Enabled = false;
                        lvElectiveCourseGrp4.Enabled = false;
                        lvElectiveCourseGrp5.Enabled = false;
                        lvElectiveCourseGrp6.Enabled = false;
                        lvElectiveCourseGrp7.Enabled = false;
                        lvDetained.Enabled = false;
                        lvMovableSubjects.Enabled = false;
                        btnSubmit.Enabled = false;

                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration_SingleStudent --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lvElectiveCourseGrp7_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlNewsec = e.Item.FindControl("ddlsection") as DropDownList;
           // ddlNewsec.Enabled = false;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField ccCode = (HiddenField)e.Item.FindControl("hdfCCode");

                string tableName = "ACD_COURSE_TEACHER CT INNER JOIN USER_aCC U ON (U.UA_NO=CT.UA_NO OR U.UA_NO=CT.ADTEACHER) INNER JOIN ACD_SECTION S ON S.SECTIONNO=CT.SECTIONNO";
                string paramter_1 = "DISTINCT (CAST(S.SECTIONNO AS VARCHAR)+' - '+ CAST(COALESCE(NULLIF(CT.UA_NO,''), CT.ADTEACHER) AS VARCHAR)+' - '+CAST(ISNULL(CT.INTAKE,0) AS VARCHAR)+' - '+ CAST((SELECT COUNT(*) FROM ACD_STUDENT_RESULT WHERE schemeno=CT.SCHEMENO and courseno=CT.COURSENO and sessionno=CT.SESSIONNO AND SECTIONNO=CT.SECTIONNO AND ISNULL(CANCEL,0)=0) AS VARCHAR)) as SECTIONNO";
                string parameter_2 = "'[ '+S.SECTIONNAME+' ] - '+U.UA_FULLNAME AS UA_FULLNAME,CT.SECTIONNO as SECNO";
                string whereCondition = "SESSIONNO = " + (Convert.ToInt32(ddlSession.SelectedValue)) + " AND CT.COURSENO=" + ccCode.Value + " AND ISNULL(CANCEL,0)=0 AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + "";
                string orderByCondition = "CT.SECTIONNO";

                objCommon.FillDropDownList(ddlNewsec, tableName, paramter_1, parameter_2, whereCondition, orderByCondition);
                ddlNewsec.SelectedValue = lblSection.ToolTip;

                Label Intake = (Label)e.Item.FindControl("lblIntake");
                Label Filled = (Label)e.Item.FindControl("lblFilled");

                Intake.Text = ddlNewsec.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                Filled.Text = ddlNewsec.SelectedValue.ToString().Split('-')[3].Trim().ToString();


                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))
                {
                    Intake.CssClass = "label label-default";
                    Filled.CssClass = "label label-default";

                    int CalcVal = 0;
                    for (int i = 0; i < ddlNewsec.Items.Count; i++)
                    {
                        if (Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[3].Trim().ToString()))
                        {
                            CalcVal++;
                        }
                    }

                    if (CalcVal == ddlNewsec.Items.Count)
                    {
                        CheckBox chkSelectCourse = (CheckBox)e.Item.FindControl("chkAccept");
                        chkSelectCourse.Enabled = false;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-danger";
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-warning";
                }
                else
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-success";
                }



                CheckBox chkcurrent = (CheckBox)e.Item.FindControl("chkElectiveGrp7");
                if (chkcurrent.Checked)
                {
                    txtAllSubjects.Text = Convert.ToString(Convert.ToInt32(txtAllSubjects.Text) + 1);

                    txtCredits.Text = Convert.ToString(Convert.ToDecimal(txtCredits.Text) + Convert.ToDecimal(((Label)e.Item.FindControl("lblCredits")).Text));

                    ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;

                    if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
                    {
                        if (ViewState["ACCEPT"].ToString() != "0")//accepted
                        {
                            lvCurrentSubjects.Enabled = false;
                            lvElectiveCourse.Enabled = false;
                            lvElectiveCourseGrp2.Enabled = false;
                            lvElectiveCourseGrp3.Enabled = false;
                            lvElectiveCourseGrp4.Enabled = false;
                            lvElectiveCourseGrp5.Enabled = false;
                            lvElectiveCourseGrp6.Enabled = false;
                            lvElectiveCourseGrp7.Enabled = false;
                            lvDetained.Enabled = false;
                            lvMovableSubjects.Enabled = false;
                            btnSubmit.Enabled = false;
                        }
                        else
                        {
                            lvCurrentSubjects.Enabled = true;
                            lvElectiveCourse.Enabled = true;
                            lvElectiveCourseGrp2.Enabled = true;
                            lvElectiveCourseGrp3.Enabled = true;
                            lvElectiveCourseGrp4.Enabled = true;
                            lvElectiveCourseGrp5.Enabled = true;
                            lvElectiveCourseGrp6.Enabled = true;
                            lvElectiveCourseGrp7.Enabled = true;
                            lvDetained.Enabled = true;
                            lvMovableSubjects.Enabled = true;
                            btnSubmit.Enabled = true;
                        }
                    }
                    else
                    {
                        ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;
                        lvCurrentSubjects.Enabled = false;
                        lvElectiveCourse.Enabled = false;
                        lvElectiveCourseGrp2.Enabled = false;
                        lvElectiveCourseGrp3.Enabled = false;
                        lvElectiveCourseGrp4.Enabled = false;
                        lvElectiveCourseGrp5.Enabled = false;
                        lvElectiveCourseGrp6.Enabled = false;
                        lvElectiveCourseGrp7.Enabled = false;
                        lvDetained.Enabled = false;
                        lvMovableSubjects.Enabled = false;
                        btnSubmit.Enabled = false;

                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration_SingleStudent --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lvMovableSubjects_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlNewsec = e.Item.FindControl("ddlsection") as DropDownList;      //Added by Abhinay Lad [21-06-2019]
          //  ddlNewsec.Enabled = false;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField ccCode = (HiddenField)e.Item.FindControl("hdfCCode");

                string tableName = "ACD_COURSE_TEACHER CT INNER JOIN USER_aCC U ON (U.UA_NO=CT.UA_NO OR U.UA_NO=CT.ADTEACHER) INNER JOIN ACD_SECTION S ON S.SECTIONNO=CT.SECTIONNO";
                string paramter_1 = "DISTINCT (CAST(S.SECTIONNO AS VARCHAR)+' - '+ CAST(COALESCE(NULLIF(CT.UA_NO,''), CT.ADTEACHER) AS VARCHAR)+' - '+CAST(ISNULL(CT.INTAKE,0) AS VARCHAR)+' - '+ CAST((SELECT COUNT(*) FROM ACD_STUDENT_RESULT WHERE schemeno=CT.SCHEMENO and courseno=CT.COURSENO and sessionno=CT.SESSIONNO AND SECTIONNO=CT.SECTIONNO AND ISNULL(CANCEL,0)=0) AS VARCHAR)) as SECTIONNO";
                string parameter_2 = "'[ '+S.SECTIONNAME+' ] - '+U.UA_FULLNAME AS UA_FULLNAME,CT.SECTIONNO as SECNO";
                string whereCondition = "SESSIONNO = " + (Convert.ToInt32(ddlSession.SelectedValue)) + " AND CT.COURSENO=" + ccCode.Value + " AND ISNULL(CANCEL,0)=0 AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + "";
                string orderByCondition = "CT.SECTIONNO";

                objCommon.FillDropDownList(ddlNewsec, tableName, paramter_1, parameter_2, whereCondition, orderByCondition);        //Added by Abhinat Lad [19-06-2019]
                ddlNewsec.SelectedValue = lblSection.ToolTip;

                Label Intake = (Label)e.Item.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                Label Filled = (Label)e.Item.FindControl("lblFilled");

                Intake.Text = ddlNewsec.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                Filled.Text = ddlNewsec.SelectedValue.ToString().Split('-')[3].Trim().ToString();


                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                {
                    Intake.CssClass = "label label-default";
                    Filled.CssClass = "label label-default";

                    int CalcVal = 0;
                    for (int i = 0; i < ddlNewsec.Items.Count; i++)
                    {
                        if (Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[3].Trim().ToString()))
                        {
                            CalcVal++;
                        }
                    }

                    if (CalcVal == ddlNewsec.Items.Count)
                    {
                        CheckBox chkSelectCourse = (CheckBox)e.Item.FindControl("chkAccept");
                        chkSelectCourse.Enabled = false;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-danger";
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-warning";
                }
                else
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-success";
                }

                #region Hidden
                CheckBox chkMovable = (CheckBox)e.Item.FindControl("chkMovable");
                if (chkMovable.Checked)
                {
                    txtAllSubjects.Text = Convert.ToString(Convert.ToInt32(txtAllSubjects.Text) + 1);

                    txtCredits.Text = Convert.ToString(Convert.ToDecimal(txtCredits.Text) + Convert.ToDecimal(((Label)e.Item.FindControl("lblCredits")).Text));

                    ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;

                    if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
                    {
                        if (ViewState["ACCEPT"].ToString() != "0")//accepted
                        {
                            lvCurrentSubjects.Enabled = false;
                            lvElectiveCourse.Enabled = false;
                            lvElectiveCourseGrp2.Enabled = false;
                            lvElectiveCourseGrp3.Enabled = false;
                            lvElectiveCourseGrp4.Enabled = false;
                            lvElectiveCourseGrp5.Enabled = false;
                            lvElectiveCourseGrp6.Enabled = false;
                            lvElectiveCourseGrp7.Enabled = false;
                            lvDetained.Enabled = false;
                            lvMovableSubjects.Enabled = false;
                            btnSubmit.Enabled = false;

                        }
                        else
                        {
                            lvCurrentSubjects.Enabled = true;
                            lvElectiveCourse.Enabled = true;
                            lvElectiveCourseGrp2.Enabled = true;
                            lvElectiveCourseGrp3.Enabled = true;
                            lvElectiveCourseGrp4.Enabled = true;
                            lvElectiveCourseGrp5.Enabled = true;
                            lvElectiveCourseGrp6.Enabled = true;
                            lvElectiveCourseGrp7.Enabled = true;
                            lvDetained.Enabled = true;
                            lvMovableSubjects.Enabled = true;
                            btnSubmit.Enabled = true;
                        }
                    }
                    else
                    {
                        ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;
                        lvCurrentSubjects.Enabled = false;
                        lvElectiveCourse.Enabled = false;
                        lvElectiveCourseGrp2.Enabled = false;
                        lvElectiveCourseGrp3.Enabled = false;
                        lvElectiveCourseGrp4.Enabled = false;
                        lvElectiveCourseGrp5.Enabled = false;
                        lvElectiveCourseGrp6.Enabled = false;
                        lvElectiveCourseGrp7.Enabled = false;
                        lvDetained.Enabled = false;
                        lvMovableSubjects.Enabled = false;
                        btnSubmit.Enabled = false;
                    }
                }
                #endregion
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration_SingleStudent --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region commented
    //protected void lvBacklogCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    try
    //    {
    //        DropDownList ddlNewsec = e.Item.FindControl("ddlsection") as DropDownList;
    //        objCommon.FillDropDownList(ddlNewsec, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNAME");
    //        ddlNewsec.SelectedValue = lblSection.ToolTip;
    //        if (e.Item.ItemType == ListViewItemType.DataItem)
    //        {
    //            CheckBox chkbacklog = (CheckBox)e.Item.FindControl("chkBacklog");
    //            if (chkbacklog.Checked)
    //            {
    //                //txtAllSubjects.Text = Convert.ToString((Convert.ToInt32(txtAllSubjects.Text) + 1));
    //                //txtCredits.Text = Convert.ToString(Convert.ToInt32(txtCredits.Text) + Convert.ToInt32(((Label)e.Item.FindControl("lblCredits")).Text));

    //                if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
    //                {
    //                    if (ViewState["ACCEPT"].ToString() != "0")//accepted
    //                    {
    //                        // lvBacklogCourse.Enabled = false;
    //                        btnSubmit.Enabled = false;
    //                    }
    //                    else
    //                    {
    //                        // lvBacklogCourse.Enabled = true;
    //                        btnSubmit.Enabled = true;
    //                    }
    //                }
    //                else
    //                {
    //                    // lvBacklogCourse.Enabled = false;
    //                    btnSubmit.Enabled = false;
    //                }
    //            }

    //            //else//for checking attempts more than 4
    //            //{
    //            Label attempt = (Label)e.Item.FindControl("lblAttempt");
    //            if (Convert.ToInt32(attempt.Text) >= 1)
    //            {
    //                txtAllSubjects.Text = Convert.ToString((Convert.ToInt32(txtAllSubjects.Text) + 1));
    //                txtCredits.Text = Convert.ToString(Convert.ToDecimal(txtCredits.Text) + Convert.ToDecimal(((Label)e.Item.FindControl("lblCredits")).Text));

    //                chkbacklog.Checked = true;
    //                chkbacklog.Enabled = false;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration_SingleStudent --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    #endregion commented


    protected void chkAccept_CheckedChanged(object sender, EventArgs e)
    {
        int _clear = 0;
        CheckBox chk = sender as CheckBox;

        int totalsub = Convert.ToInt32(txtAllSubjects.Text);
        decimal totSubjectCredit = Convert.ToDecimal(txtCredits.Text);

        ListViewItem item = (ListViewItem)chk.NamingContainer;
        HiddenField hf = (HiddenField)item.FindControl("hdfCCode");
        HiddenField hfStatus = (HiddenField)item.FindControl("hfStatus");

        if (chk.Checked)
        {
            #region commented
            //if (!string.IsNullOrEmpty(hf.ToString()))
            //{
            //    _clear = objCommon.LookUp("ACD_STUDENT_RESULT_HIST", "DISTINCT 1", "COURSENO=" + Convert.ToInt32(hf.Value) + " AND ISNULL(CLEAR,0)=1 AND IDNO=" + Convert.ToInt32(ViewState["idno"].ToString())) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT_HIST", "DISTINCT 1", "COURSENO=" + Convert.ToInt32(hf.Value) + " AND ISNULL(CLEAR,0)=1 AND IDNO=" + Convert.ToInt32(ViewState["idno"].ToString())));
            //    if ((_clear == 0) && Convert.ToInt32(hfStatus.Value) == 1 && !string.IsNullOrEmpty(hfStatus.Value))
            //    {
            //        _clear = 1;
            //    }
            //}
            //if (_clear == 1)
            //{
            //    chk.Checked = false;
            //    return;
            //}

            //DataSet antiRequisiteCourse = objCommon.FillDropDown("ACD_REQUISITE_COURSE", "REQUISITE_COURSENO", "SEMESTERNO", "REQUISITE_TYPE=2 AND COURSENO=" + hf.Value, "");
            //if (antiRequisiteCourse.Tables[0].Rows.Count > 0)
            //{
            //#region Check Anti Requisite

            //for (int i = 0; i < antiRequisiteCourse.Tables[0].Rows.Count; i++)
            //{
            //foreach (ListViewDataItem dataitem in lvBacklogCourse.Items)
            //{
            //    if ((dataitem.FindControl("chkBacklog") as CheckBox).Checked == true)
            //    {

            //        if (antiRequisiteCourse.Tables[0].Rows[i]["REQUISITE_COURSENO"].ToString() == ((HiddenField)dataitem.FindControl("hdfCCode")).Value)
            //        {
            //            objCommon.DisplayMessage(this.updDiv, "Can not Register this Subject because its anti requisite is already selected", this.Page);
            //            chk.Checked = false;
            //            return;
            //        }
            //    }
            //}

            //foreach (ListViewDataItem dataitem in lvDetained.Items)
            //{
            //    if ((dataitem.FindControl("chkDetain") as CheckBox).Checked == true)
            //    {
            //        if (antiRequisiteCourse.Tables[0].Rows[i]["REQUISITE_COURSENO"].ToString() == ((HiddenField)dataitem.FindControl("hdfCCode")).Value)
            //        {
            //            objCommon.DisplayMessage(this.updDiv, "Cant Register this Subject because its anti requisite is already selected", this.Page);
            //            chk.Checked = false;
            //            return;
            //        }
            //    }
            //}


            //foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            //{
            //    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
            //    {

            //        if (antiRequisiteCourse.Tables[0].Rows[i]["REQUISITE_COURSENO"].ToString() == ((HiddenField)dataitem.FindControl("hdfCCode")).Value)
            //        {
            //            objCommon.DisplayMessage(this.updDiv, "Cant Register this Subject because its anti requisite is already selected", this.Page);
            //            chk.Checked = false;
            //            return;
            //        }
            //    }                       
            //}

            //foreach (ListViewDataItem dataitem in lvElectiveCourse.Items)
            //{
            //    if ((dataitem.FindControl("chkElective") as CheckBox).Checked == true)
            //    {

            //        if (antiRequisiteCourse.Tables[0].Rows[i]["REQUISITE_COURSENO"].ToString() == ((HiddenField)dataitem.FindControl("hdfCCode")).Value)
            //        {
            //            objCommon.DisplayMessage(this.updDiv, "Cant Register this Subject because its anti requisite is already selected", this.Page);
            //            chk.Checked = false;
            //            return;
            //        }
            //    }
            //}
            // }

            //  }
            #endregion

            Label lb = (Label)item.FindControl("lblCredits");
            totSubjectCredit = totSubjectCredit + Convert.ToDecimal(lb.Text);
            totalsub = totalsub + 1;

            //DropDownList ddlSecTeacher = (DropDownList)item.FindControl("ddlsection");  //Added by Abhinay Lad [21-06-2019]
            //ddlSecTeacher.Enabled = true;
            #region Commented
            //if (totSubjectCredit > Convert.ToDecimal(lblMaxCreditlimit.Text))
            //{
            //    totSubjectCredit = totSubjectCredit - Convert.ToDecimal(lb.Text);
            //    totalsub = totalsub - 1;
            //    chk.Checked = false;

            //    objCommon.DisplayMessage(this.updDiv, "Maximum " + lblMaxCreditlimit.Text + "  credits are allowed for registration", this.Page);
            //    return;
            //}
            #endregion
        }
        else
        {
            Label lb = (Label)item.FindControl("lblCredits");
            totSubjectCredit = totSubjectCredit - Convert.ToDecimal(lb.Text);
            totalsub = totalsub - 1;
        }
        txtAllSubjects.Text = CheckTotalSubjects().ToString();
        txtCredits.Text = CheckTotalCredits().ToString();
    }

    //***************** this is for elective group -1 *************
    protected void chkElectiveGrp1_CheckedChanged(object sender, EventArgs e)
    {
        int ElectGrp1Count = 0, maxEleAllowGrp1 = 0;
        DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SECTION SE ON (SE.SECTIONNO = s.SECTIONNO)", "S.IDNO,S.ROLLNO,DG.DEGREENAME,SECTIONNAME,s.SECTIONNO", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO as ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE ISNULL(S.PHYSICALLY_HANDICAPPED,0) WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH,IDTYPE,(CASE ISNULL(S.IDTYPE,0) WHEN '3' THEN 'DIRECT SECOND YEAR ' ELSE  'REGULAR'   END) AS STUD_TYPE", "S.ENROLLNO='" + txtRollNo.Text.Trim() + "'", string.Empty);

        int adm_type = 1;
        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()) == 3) //3 direct second year
        {
            adm_type = 2;
        }
        int student_type = 1;//1 for All Pass      

        DataSet dsCreditInfo1 = null;
        dsCreditInfo1 = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT", "ElectGrp1", "", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "AND STUDENT_TYPE =" + student_type + "AND DEGREENO =" + dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND ADM_TYPE=" + adm_type + "AND " + dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() + "IN (select VALUE FROM DBO.SPLIT( SEMESTER,','))", "");

        maxEleAllowGrp1 = (dsCreditInfo1.Tables[0].Rows[0]["ElectGrp1"].ToString() == string.Empty ? 1 : Convert.ToInt32(dsCreditInfo1.Tables[0].Rows[0]["ElectGrp1"].ToString()));


        CheckBox chk = sender as CheckBox;
        int totalsub = Convert.ToInt32(txtAllSubjects.Text);
        decimal totSubjectCredit = Convert.ToDecimal(txtCredits.Text);

        ListViewItem item = (ListViewItem)chk.NamingContainer;
        HiddenField hf = (HiddenField)item.FindControl("hdfCCode");
        HiddenField hfStatus = (HiddenField)item.FindControl("hfStatus");

        foreach (ListViewDataItem dataitem in lvElectiveCourse.Items)
        {
            CheckBox chk1 = dataitem.FindControl("chkElectiveGrp1") as CheckBox;
            if (chk1.Checked == true)
                ElectGrp1Count++;
        }

        DropDownList ddlSecTeacher = (DropDownList)item.FindControl("ddlsection");  //Added by Abhinay Lad [24-06-2019]
        if (chk.Checked)
        {

            #region Start
            decimal cred = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "ISNULL(CREDITS,0)", "COURSENO = " + hf.Value));

            totSubjectCredit = totSubjectCredit + Convert.ToDecimal(cred);
            totalsub = totalsub + 1;

            if (ElectGrp1Count > maxEleAllowGrp1)
            {
                totSubjectCredit = totSubjectCredit - Convert.ToDecimal(cred);
                totalsub = totalsub - 1;
                chk.Checked = false;

                txtAllSubjects.Text = CheckTotalSubjects().ToString();
                txtCredits.Text = CheckTotalCredits().ToString();

                objCommon.DisplayMessage(this.updDiv, "Maximum " + maxEleAllowGrp1 + " Elective Subject are allowed for registration from [Group-1]", this.Page);
                return;
            }
            else
            {
                ddlSecTeacher.Enabled = true;
            }
            #endregion

            #region for Elective Intake -----
            //int ElectGrp1Intake = 0;
            //int opfilledSeat = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(COURSENO)", "SESSIONNO=" + ddlSession.SelectedValue + "AND COURSENO=" + hf.Value));

            //string opCCode = objCommon.LookUp("ACD_OPEN_ELECT_INTAKE_MASTER", "CCODE", "COURSENO=" + hf.Value);
            //if (string.IsNullOrEmpty(opCCode))
            //{
            //    opCCode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + hf.Value);
            //}
            //if (!string.IsNullOrEmpty(opCCode))
            //{
            //    ElectGrp1Intake = objCommon.LookUp("ACD_OPEN_ELECT_INTAKE_MASTER", "ISNULL(INTAKE,0)", "CCODE='" + opCCode + "' AND SESSIONNO =" + ddlSession.SelectedValue) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_OPEN_ELECT_INTAKE_MASTER", "ISNULL(INTAKE,0)", "CCODE='" + opCCode + "' AND SESSIONNO =" + ddlSession.SelectedValue));
            //}
            //if (ElectGrp1Intake == 0)
            //{
            //    objCommon.DisplayMessage(this.updDiv, "Intake Not define for selected Subject!" + opCCode, this.Page);
            //}
            //int intakeAvailable = ElectGrp1Intake - opfilledSeat;

            //if (intakeAvailable <= 0)
            //{
            //    objCommon.DisplayMessage(this.updDiv, "No Seat is available for this Subject", this.Page);

            //    //refil and get latest seat 
            //  //***  fillOpenElectiveCourses();
            //}
            //else
            //{
            //}
            #endregion for Elective Intake -----
        }
        else if (chk.Checked == false)
        {
            ddlSecTeacher.Enabled = false;
        }
        txtAllSubjects.Text = CheckTotalSubjects().ToString();
        txtCredits.Text = CheckTotalCredits().ToString();
    }

    protected void chkElectiveGrp2_CheckedChanged(object sender, EventArgs e)
    {
        int ElectGrp2Count = 0, maxEleAllowGrp2 = 0;
        DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SECTION SE ON (SE.SECTIONNO = s.SECTIONNO)", "S.IDNO,S.ROLLNO,DG.DEGREENAME,SECTIONNAME,s.SECTIONNO", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO as ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE ISNULL(S.PHYSICALLY_HANDICAPPED,0) WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH,IDTYPE,(CASE ISNULL(S.IDTYPE,0) WHEN '3' THEN 'DIRECT SECOND YEAR ' ELSE  'REGULAR'   END) AS STUD_TYPE", "S.ENROLLNO='" + txtRollNo.Text.Trim() + "'", string.Empty);

        int adm_type = 1;
        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()) == 3) //3 direct second year
        {
            adm_type = 2;
        }
        int student_type = 1;//1 for All Pass      

        DataSet dsCreditInfo2 = null;
        dsCreditInfo2 = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT", "ElectGrp2", "", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "AND STUDENT_TYPE =" + student_type + "AND DEGREENO =" + dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND ADM_TYPE=" + adm_type + "AND " + dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() + "IN (select VALUE FROM DBO.SPLIT( SEMESTER,','))", "");


        maxEleAllowGrp2 = (dsCreditInfo2.Tables[0].Rows[0]["ElectGrp2"].ToString() == string.Empty ? 1 : Convert.ToInt32(dsCreditInfo2.Tables[0].Rows[0]["ElectGrp2"].ToString()));


        CheckBox chk = sender as CheckBox;
        int totalsub = Convert.ToInt32(txtAllSubjects.Text);
        decimal totSubjectCredit = Convert.ToDecimal(txtCredits.Text);

        ListViewItem item = (ListViewItem)chk.NamingContainer;
        HiddenField hf = (HiddenField)item.FindControl("hdfCCode");
        HiddenField hfStatus = (HiddenField)item.FindControl("hfStatus");

        foreach (ListViewDataItem dataitem in lvElectiveCourseGrp2.Items)
        {
            CheckBox chk1 = dataitem.FindControl("chkElectiveGrp2") as CheckBox;
            if (chk1.Checked == true)
                ElectGrp2Count++;
        }

        DropDownList ddlSecTeacher = (DropDownList)item.FindControl("ddlsection");  //Added by Abhinay Lad [30-06-2019]
        if (chk.Checked)
        {
            decimal cred = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "ISNULL(CREDITS,0)", "COURSENO = " + hf.Value));

            totSubjectCredit = totSubjectCredit + Convert.ToDecimal(cred);
            totalsub = totalsub + 1;

            if (ElectGrp2Count > maxEleAllowGrp2)
            {
                totSubjectCredit = totSubjectCredit - Convert.ToDecimal(cred);
                totalsub = totalsub - 1;
                chk.Checked = false;

                txtAllSubjects.Text = CheckTotalSubjects().ToString();
                txtCredits.Text = CheckTotalCredits().ToString();

                objCommon.DisplayMessage(this.updDiv, "Maximum " + maxEleAllowGrp2 + " Elective Subject are allowed for registration from [Group-2]", this.Page);
                return;
            }
            else
            {
                ddlSecTeacher.Enabled = true;
            }
        }
        else if (chk.Checked == false)
        {
            ddlSecTeacher.Enabled = false;
        }
        txtAllSubjects.Text = CheckTotalSubjects().ToString();
        txtCredits.Text = CheckTotalCredits().ToString();
    }

    protected void chkElectiveGrp3_CheckedChanged(object sender, EventArgs e)
    {
        int ElectGrp3Count = 0, maxEleAllowGrp3 = 0;
        DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SECTION SE ON (SE.SECTIONNO = s.SECTIONNO)", "S.IDNO,S.ROLLNO,DG.DEGREENAME,SECTIONNAME,s.SECTIONNO", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO as ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE ISNULL(S.PHYSICALLY_HANDICAPPED,0) WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH,IDTYPE,(CASE ISNULL(S.IDTYPE,0) WHEN '3' THEN 'DIRECT SECOND YEAR ' ELSE  'REGULAR'   END) AS STUD_TYPE", "S.ENROLLNO='" + txtRollNo.Text.Trim() + "'", string.Empty);

        int adm_type = 1;
        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()) == 3) //3 direct second year
        {
            adm_type = 2;
        }
        int student_type = 1;//1 for All Pass      

        DataSet dsCreditInfo3 = null;
        dsCreditInfo3 = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT", "ElectGrp3", "", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "AND STUDENT_TYPE =" + student_type + "AND DEGREENO =" + dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND ADM_TYPE=" + adm_type + "AND " + dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() + "IN (select VALUE FROM DBO.SPLIT( SEMESTER,','))", "");

        maxEleAllowGrp3 = (dsCreditInfo3.Tables[0].Rows[0]["ElectGrp3"].ToString() == string.Empty ? 1 : Convert.ToInt32(dsCreditInfo3.Tables[0].Rows[0]["ElectGrp3"].ToString()));


        CheckBox chk = sender as CheckBox;
        int totalsub = Convert.ToInt32(txtAllSubjects.Text);
        decimal totSubjectCredit = Convert.ToDecimal(txtCredits.Text);

        ListViewItem item = (ListViewItem)chk.NamingContainer;
        HiddenField hf = (HiddenField)item.FindControl("hdfCCode");
        HiddenField hfStatus = (HiddenField)item.FindControl("hfStatus");

        foreach (ListViewDataItem dataitem in lvElectiveCourseGrp3.Items)
        {
            CheckBox chk1 = dataitem.FindControl("chkElectiveGrp3") as CheckBox;
            if (chk1.Checked == true)
                ElectGrp3Count++;
        }

        DropDownList ddlSecTeacher = (DropDownList)item.FindControl("ddlsection");  //Added by Abhinay Lad [30-06-2019]
        if (chk.Checked)
        {
            decimal cred = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "ISNULL(CREDITS,0)", "COURSENO = " + hf.Value));

            totSubjectCredit = totSubjectCredit + Convert.ToDecimal(cred);
            totalsub = totalsub + 1;

            if (ElectGrp3Count > maxEleAllowGrp3)
            {
                totSubjectCredit = totSubjectCredit - Convert.ToDecimal(cred);
                totalsub = totalsub - 1;
                chk.Checked = false;

                txtAllSubjects.Text = CheckTotalSubjects().ToString();
                txtCredits.Text = CheckTotalCredits().ToString();

                objCommon.DisplayMessage(this.updDiv, "Maximum " + maxEleAllowGrp3 + " Elective Subject are allowed for registration from [Group-3]", this.Page);
                return;
            }
            else
            {
                ddlSecTeacher.Enabled = true;
            }
        }
        else if (chk.Checked == false)
        {
            ddlSecTeacher.Enabled = false;
        }

        txtAllSubjects.Text = CheckTotalSubjects().ToString();
        txtCredits.Text = CheckTotalCredits().ToString();
    }

    protected void chkElectiveGrp4_CheckedChanged(object sender, EventArgs e)
    {
        int ElectGrp4Count = 0, maxEleAllowGrp4 = 0;
        DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SECTION SE ON (SE.SECTIONNO = s.SECTIONNO)", "S.IDNO,S.ROLLNO,DG.DEGREENAME,SECTIONNAME,s.SECTIONNO", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO as ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE ISNULL(S.PHYSICALLY_HANDICAPPED,0) WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH,IDTYPE,(CASE ISNULL(S.IDTYPE,0) WHEN '3' THEN 'DIRECT SECOND YEAR ' ELSE  'REGULAR'   END) AS STUD_TYPE", "S.ENROLLNO='" + txtRollNo.Text.Trim() + "'", string.Empty);

        int adm_type = 1;
        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()) == 3) //3 direct second year
        {
            adm_type = 2;
        }
        int student_type = 1;//1 for All Pass      

        DataSet dsCreditInfo4 = null;
        dsCreditInfo4 = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT", "ElectGrp4", "", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "AND STUDENT_TYPE =" + student_type + "AND DEGREENO =" + dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND ADM_TYPE=" + adm_type + "AND " + dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() + "IN (select VALUE FROM DBO.SPLIT( SEMESTER,','))", "");

        maxEleAllowGrp4 = (dsCreditInfo4.Tables[0].Rows[0]["ElectGrp4"].ToString() == string.Empty ? 1 : Convert.ToInt32(dsCreditInfo4.Tables[0].Rows[0]["ElectGrp4"].ToString()));


        CheckBox chk = sender as CheckBox;
        int totalsub = Convert.ToInt32(txtAllSubjects.Text);
        decimal totSubjectCredit = Convert.ToDecimal(txtCredits.Text);

        ListViewItem item = (ListViewItem)chk.NamingContainer;
        HiddenField hf = (HiddenField)item.FindControl("hdfCCode");
        HiddenField hfStatus = (HiddenField)item.FindControl("hfStatus");

        foreach (ListViewDataItem dataitem in lvElectiveCourseGrp4.Items)
        {
            CheckBox chk1 = dataitem.FindControl("chkElectiveGrp4") as CheckBox;
            if (chk1.Checked == true)
                ElectGrp4Count++;
        }

        DropDownList ddlSecTeacher = (DropDownList)item.FindControl("ddlsection");  //Added by Abhinay Lad [30-06-2019]
        if (chk.Checked)
        {
            decimal cred = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "ISNULL(CREDITS,0)", "COURSENO = " + hf.Value));

            totSubjectCredit = totSubjectCredit + Convert.ToDecimal(cred);
            totalsub = totalsub + 1;

            if (ElectGrp4Count > maxEleAllowGrp4)
            {
                totSubjectCredit = totSubjectCredit - Convert.ToDecimal(cred);
                totalsub = totalsub - 1;
                chk.Checked = false;

                txtAllSubjects.Text = CheckTotalSubjects().ToString();
                txtCredits.Text = CheckTotalCredits().ToString();

                objCommon.DisplayMessage(this.updDiv, "Maximum " + maxEleAllowGrp4 + " Elective Subject are allowed for registration from [Group-4]", this.Page);
                return;
            }
            else
            {
                ddlSecTeacher.Enabled = true;
            }
        }
        else if (chk.Checked == false)
        {
            ddlSecTeacher.Enabled = false;
        }
        txtAllSubjects.Text = CheckTotalSubjects().ToString();
        txtCredits.Text = CheckTotalCredits().ToString();
    }

    protected void chkElectiveGrp5_CheckedChanged(object sender, EventArgs e)
    {
        int ElectGrp5Count = 0, maxEleAllowGrp5 = 0;
        DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SECTION SE ON (SE.SECTIONNO = s.SECTIONNO)", "S.IDNO,S.ROLLNO,DG.DEGREENAME,SECTIONNAME,s.SECTIONNO", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO as ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE ISNULL(S.PHYSICALLY_HANDICAPPED,0) WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH,IDTYPE,(CASE ISNULL(S.IDTYPE,0) WHEN '3' THEN 'DIRECT SECOND YEAR ' ELSE  'REGULAR'   END) AS STUD_TYPE", "S.ENROLLNO='" + txtRollNo.Text.Trim() + "'", string.Empty);

        int adm_type = 1;
        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()) == 3) //3 direct second year
        {
            adm_type = 2;
        }
        int student_type = 1;//1 for All Pass      

        DataSet dsCreditInfo5 = null;
        dsCreditInfo5 = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT", "ElectGrp5", "", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "AND STUDENT_TYPE =" + student_type + "AND DEGREENO =" + dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND ADM_TYPE=" + adm_type + "AND " + dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() + "IN (select VALUE FROM DBO.SPLIT( SEMESTER,','))", "");


        maxEleAllowGrp5 = (dsCreditInfo5.Tables[0].Rows[0]["ElectGrp5"].ToString() == string.Empty ? 1 : Convert.ToInt32(dsCreditInfo5.Tables[0].Rows[0]["ElectGrp5"].ToString()));


        CheckBox chk = sender as CheckBox;
        int totalsub = Convert.ToInt32(txtAllSubjects.Text);
        decimal totSubjectCredit = Convert.ToDecimal(txtCredits.Text);

        ListViewItem item = (ListViewItem)chk.NamingContainer;
        HiddenField hf = (HiddenField)item.FindControl("hdfCCode");
        HiddenField hfStatus = (HiddenField)item.FindControl("hfStatus");

        foreach (ListViewDataItem dataitem in lvElectiveCourseGrp5.Items)
        {
            CheckBox chk1 = dataitem.FindControl("chkElectiveGrp5") as CheckBox;
            if (chk1.Checked == true)
                ElectGrp5Count++;
        }

        DropDownList ddlSecTeacher = (DropDownList)item.FindControl("ddlsection");  //Added by Abhinay Lad [30-06-2019]
        if (chk.Checked)
        {
            decimal cred = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "ISNULL(CREDITS,0)", "COURSENO = " + hf.Value));

            totSubjectCredit = totSubjectCredit + Convert.ToDecimal(cred);
            totalsub = totalsub + 1;

            if (ElectGrp5Count > maxEleAllowGrp5)
            {
                totSubjectCredit = totSubjectCredit - Convert.ToDecimal(cred);
                totalsub = totalsub - 1;
                chk.Checked = false;

                txtAllSubjects.Text = CheckTotalSubjects().ToString();
                txtCredits.Text = CheckTotalCredits().ToString();

                objCommon.DisplayMessage(this.updDiv, "Maximum " + maxEleAllowGrp5 + " Elective Subject are allowed for registration from [Group-5]", this.Page);
                return;
            }
            else
            {
                ddlSecTeacher.Enabled = true;
            }
        }
        else if (chk.Checked == false)
        {
            ddlSecTeacher.Enabled = false;
        }
        txtAllSubjects.Text = CheckTotalSubjects().ToString();
        txtCredits.Text = CheckTotalCredits().ToString();
    }

    protected void chkElectiveGrp6_CheckedChanged(object sender, EventArgs e)
    {
        int ElectGrp6Count = 0, maxEleAllowGrp6 = 0;
        DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SECTION SE ON (SE.SECTIONNO = s.SECTIONNO)", "S.IDNO,S.ROLLNO,DG.DEGREENAME,SECTIONNAME,s.SECTIONNO", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO as ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE ISNULL(S.PHYSICALLY_HANDICAPPED,0) WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH,IDTYPE,(CASE ISNULL(S.IDTYPE,0) WHEN '3' THEN 'DIRECT SECOND YEAR ' ELSE  'REGULAR'   END) AS STUD_TYPE", "S.ENROLLNO='" + txtRollNo.Text.Trim() + "'", string.Empty);

        int adm_type = 1;
        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()) == 3) //3 direct second year
        {
            adm_type = 2;
        }
        int student_type = 1;//1 for All Pass      

        DataSet dsCreditInfo6 = null;
        dsCreditInfo6 = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT", "ElectGrp6", "", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "AND STUDENT_TYPE =" + student_type + "AND DEGREENO =" + dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND ADM_TYPE=" + adm_type + "AND " + dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() + "IN (select VALUE FROM DBO.SPLIT( SEMESTER,','))", "");


        maxEleAllowGrp6 = (dsCreditInfo6.Tables[0].Rows[0]["ElectGrp6"].ToString() == string.Empty ? 1 : Convert.ToInt32(dsCreditInfo6.Tables[0].Rows[0]["ElectGrp6"].ToString()));


        CheckBox chk = sender as CheckBox;
        int totalsub = Convert.ToInt32(txtAllSubjects.Text);
        decimal totSubjectCredit = Convert.ToDecimal(txtCredits.Text);

        ListViewItem item = (ListViewItem)chk.NamingContainer;
        HiddenField hf = (HiddenField)item.FindControl("hdfCCode");
        HiddenField hfStatus = (HiddenField)item.FindControl("hfStatus");

        foreach (ListViewDataItem dataitem in lvElectiveCourseGrp6.Items)
        {
            CheckBox chk1 = dataitem.FindControl("chkElectiveGrp6") as CheckBox;
            if (chk1.Checked == true)
                ElectGrp6Count++;
        }

        DropDownList ddlSecTeacher = (DropDownList)item.FindControl("ddlsection");  //Added by Abhinay Lad [30-06-2019]
        if (chk.Checked)
        {
            decimal cred = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "ISNULL(CREDITS,0)", "COURSENO = " + hf.Value));

            totSubjectCredit = totSubjectCredit + Convert.ToDecimal(cred);
            totalsub = totalsub + 1;

            if (ElectGrp6Count > maxEleAllowGrp6)
            {
                totSubjectCredit = totSubjectCredit - Convert.ToDecimal(cred);
                totalsub = totalsub - 1;
                chk.Checked = false;

                txtAllSubjects.Text = CheckTotalSubjects().ToString();
                txtCredits.Text = CheckTotalCredits().ToString();

                objCommon.DisplayMessage(this.updDiv, "Maximum " + maxEleAllowGrp6 + " Elective Subject are allowed for registration from [Group-6]", this.Page);
                return;
            }
            else
            {
                ddlSecTeacher.Enabled = true;
            }
        }
        else if (chk.Checked == false)
        {
            ddlSecTeacher.Enabled = false;
        }
        txtAllSubjects.Text = CheckTotalSubjects().ToString();
        txtCredits.Text = CheckTotalCredits().ToString();
    }

    protected void chkElectiveGrp7_CheckedChanged(object sender, EventArgs e)
    {
        int ElectGrp7Count = 0, maxEleAllowGrp7 = 0;
        DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SECTION SE ON (SE.SECTIONNO = s.SECTIONNO)", "S.IDNO,S.ROLLNO,DG.DEGREENAME,SECTIONNAME,s.SECTIONNO", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO as ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE ISNULL(S.PHYSICALLY_HANDICAPPED,0) WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH,IDTYPE,(CASE ISNULL(S.IDTYPE,0) WHEN '3' THEN 'DIRECT SECOND YEAR ' ELSE  'REGULAR'   END) AS STUD_TYPE", "S.ENROLLNO='" + txtRollNo.Text.Trim() + "'", string.Empty);

        int adm_type = 1;
        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()) == 3) //3 direct second year
        {
            adm_type = 2;
        }
        int student_type = 1;//1 for All Pass      

        DataSet dsCreditInfo7 = null;
        dsCreditInfo7 = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT", "ElectGrp7", "", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "AND STUDENT_TYPE =" + student_type + "AND DEGREENO =" + dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND ADM_TYPE=" + adm_type + "AND " + dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() + "IN (select VALUE FROM DBO.SPLIT( SEMESTER,','))", "");


        maxEleAllowGrp7 = (dsCreditInfo7.Tables[0].Rows[0]["ElectGrp7"].ToString() == string.Empty ? 1 : Convert.ToInt32(dsCreditInfo7.Tables[0].Rows[0]["ElectGrp7"].ToString()));


        CheckBox chk = sender as CheckBox;
        int totalsub = Convert.ToInt32(txtAllSubjects.Text);
        decimal totSubjectCredit = Convert.ToDecimal(txtCredits.Text);

        ListViewItem item = (ListViewItem)chk.NamingContainer;
        HiddenField hf = (HiddenField)item.FindControl("hdfCCode");
        HiddenField hfStatus = (HiddenField)item.FindControl("hfStatus");

        foreach (ListViewDataItem dataitem in lvElectiveCourseGrp7.Items)
        {
            CheckBox chk1 = dataitem.FindControl("chkElectiveGrp7") as CheckBox;
            if (chk1.Checked == true)
                ElectGrp7Count++;
        }

        DropDownList ddlSecTeacher = (DropDownList)item.FindControl("ddlsection");  //Added by Abhinay Lad [30-06-2019]
        if (chk.Checked)
        {
            decimal cred = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "ISNULL(CREDITS,0)", "COURSENO = " + hf.Value));

            totSubjectCredit = totSubjectCredit + Convert.ToDecimal(cred);
            totalsub = totalsub + 1;

            if (ElectGrp7Count > maxEleAllowGrp7)
            {
                totSubjectCredit = totSubjectCredit - Convert.ToDecimal(cred);
                totalsub = totalsub - 1;
                chk.Checked = false;

                txtAllSubjects.Text = CheckTotalSubjects().ToString();
                txtCredits.Text = CheckTotalCredits().ToString();

                objCommon.DisplayMessage(this.updDiv, "Maximum " + maxEleAllowGrp7 + " Elective Subject are allowed for registration from [Group-7]", this.Page);
                return;
            }
            else
            {
                ddlSecTeacher.Enabled = true;
            }
        }
        else if (chk.Checked == false)
        {
            ddlSecTeacher.Enabled = false;
        }
        txtAllSubjects.Text = CheckTotalSubjects().ToString();
        txtCredits.Text = CheckTotalCredits().ToString();
    }

    protected void chkMovable_CheckedChanged(object sender, EventArgs e)
    {
        int MovableCount = 0, maxMovAllow = 0;
        DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SECTION SE ON (SE.SECTIONNO = s.SECTIONNO)", "S.IDNO,S.ROLLNO,DG.DEGREENAME,SECTIONNAME,s.SECTIONNO", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO as ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE ISNULL(S.PHYSICALLY_HANDICAPPED,0) WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH,IDTYPE,(CASE ISNULL(S.IDTYPE,0) WHEN '3' THEN 'DIRECT SECOND YEAR ' ELSE  'REGULAR'   END) AS STUD_TYPE", "S.ENROLLNO='" + txtRollNo.Text.Trim() + "'", string.Empty);

        int adm_type = 1;
        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()) == 3) //3 direct second year
        {
            adm_type = 2;
        }
        int student_type = 1;//1 for All Pass      

        DataSet dsCreditInfo1 = null;
        dsCreditInfo1 = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT", "MovableSub", "", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "AND STUDENT_TYPE =" + student_type + "AND DEGREENO =" + dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND ADM_TYPE=" + adm_type + "AND " + dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() + "IN (select VALUE FROM DBO.SPLIT( SEMESTER,','))", "");

        maxMovAllow = (dsCreditInfo1.Tables[0].Rows[0]["MovableSub"].ToString() == string.Empty ? 1 : Convert.ToInt32(dsCreditInfo1.Tables[0].Rows[0]["MovableSub"].ToString()));


        CheckBox chk = sender as CheckBox;
        int totalsub = Convert.ToInt32(txtAllSubjects.Text);
        decimal totSubjectCredit = Convert.ToDecimal(txtCredits.Text);

        ListViewItem item = (ListViewItem)chk.NamingContainer;
        HiddenField hf = (HiddenField)item.FindControl("hdfCCode");
        HiddenField hfStatus = (HiddenField)item.FindControl("hfStatus");

        foreach (ListViewDataItem dataitem in lvMovableSubjects.Items)
        {
            CheckBox chk1 = dataitem.FindControl("chkMovable") as CheckBox;
            if (chk1.Checked == true)
                MovableCount++;
        }

        DropDownList ddlSecTeacher = (DropDownList)item.FindControl("ddlsection");  //Added by Abhinay Lad [24-06-2019]
        if (chk.Checked)
        {

            #region Start
            decimal cred = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "ISNULL(CREDITS,0)", "COURSENO = " + hf.Value));

            totSubjectCredit = totSubjectCredit + Convert.ToDecimal(cred);
            totalsub = totalsub + 1;

            if (MovableCount > maxMovAllow)
            {
                totSubjectCredit = totSubjectCredit - Convert.ToDecimal(cred);
                totalsub = totalsub - 1;
                chk.Checked = false;

                txtAllSubjects.Text = CheckTotalSubjects().ToString();
                txtCredits.Text = CheckTotalCredits().ToString();

                objCommon.DisplayMessage(this.updDiv, "Maximum " + maxMovAllow + " Movable Subject are allowed for registration.", this.Page);
                return;
            }
            else
            {
                ddlSecTeacher.Enabled = true;
            }
            #endregion
        }
        else if (chk.Checked == false)
        {
            ddlSecTeacher.Enabled = false;
        }
        txtAllSubjects.Text = CheckTotalSubjects().ToString();
        txtCredits.Text = CheckTotalCredits().ToString();
    }
    //***************** this is for elective group -1 *************

    protected void ddlsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlListFind = (DropDownList)sender;

        var lvi = (ListViewItem)ddlListFind.NamingContainer;
        ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
        ListView lv = (ListView)cph.FindControl((lvi.ClientID).ToString().Split('_')[2]);

        string ddlVal = ddlListFind.SelectedItem.Text;
        foreach (ListViewDataItem dataitem in lv.Items)
        {
            DropDownList ddl = dataitem.FindControl(ddlListFind.ID) as DropDownList;
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                if ((ddl.Items[i].ToString()).Contains(ddlVal.Split('-')[0].Trim().ToString()))
                {
                    if (lv.ClientID == "ctl00_ContentPlaceHolder1_lvCurrentSubjects")
                    {
                        ddl.SelectedIndex = i;
                    }

                    Label Intake = (Label)dataitem.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                    Label Filled = (Label)dataitem.FindControl("lblFilled");

                    Intake.Text = ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                    Filled.Text = ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString();

                    if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                    {
                        Intake.CssClass = "label label-default";
                        Filled.CssClass = "label label-default";
                    }
                    else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                    {
                        Intake.CssClass = "label label-success";
                        Filled.CssClass = "label label-danger";
                    }
                    else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                    {
                        Intake.CssClass = "label label-success";
                        Filled.CssClass = "label label-warning";
                    }
                    else
                    {
                        Intake.CssClass = "label label-success";
                        Filled.CssClass = "label label-success";
                    }
                }
            }
        }
    }

    protected void ddlCoreSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
        ListView lv = (ListView)cph.FindControl("lvCurrentSubjects");

        string ddlVal = ddlCoreSection.SelectedItem.Text.Split('-')[1];
        foreach (ListViewDataItem dataitem in lv.Items)
        {
            DropDownList ddl = dataitem.FindControl("ddlsection") as DropDownList;
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                if ((ddl.Items[i].ToString()).Contains(ddlVal))
                {
                    if (lv.ClientID == "ctl00_ContentPlaceHolder1_lvCurrentSubjects")
                    {
                        ddl.SelectedIndex = ddlCoreSection.SelectedIndex;
                        lnkBtnTimeTable.Text = "Time Table for Section" + ddlVal;
                    }

                    Label Intake = (Label)dataitem.FindControl("lblIntake");      //Added by Abhinay Lad [19-06-2019]
                    Label Filled = (Label)dataitem.FindControl("lblFilled");

                    Intake.Text = ddl.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                    Filled.Text = ddl.SelectedValue.ToString().Split('-')[3].Trim().ToString();

                    if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))     //Added by Abhinay Lad [19-06-2019]
                    {
                        Intake.CssClass = "label label-default";
                        Filled.CssClass = "label label-default";
                    }
                    else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))     //Added by Abhinay Lad [19-06-2019]
                    {
                        Intake.CssClass = "label label-success";
                        Filled.CssClass = "label label-danger";
                    }
                    else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                    {
                        Intake.CssClass = "label label-success";
                        Filled.CssClass = "label label-warning";
                    }
                    else
                    {
                        Intake.CssClass = "label label-success";
                        Filled.CssClass = "label label-success";
                    }
                }
            }
        }
    }



    protected void lvElectiveCourseGrp8_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try 
        {
            DropDownList ddlNewsec = e.Item.FindControl("ddlsection") as DropDownList;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField ccCode = (HiddenField)e.Item.FindControl("hdfCCode");

                string tableName = "ACD_COURSE_TEACHER CT INNER JOIN USER_aCC U ON (U.UA_NO=CT.UA_NO OR U.UA_NO=CT.ADTEACHER) INNER JOIN ACD_SECTION S ON S.SECTIONNO=CT.SECTIONNO";
                string paramter_1 = "DISTINCT (CAST(S.SECTIONNO AS VARCHAR)+' - '+ CAST(COALESCE(NULLIF(CT.UA_NO,''), CT.ADTEACHER) AS VARCHAR)+' - '+CAST(ISNULL(CT.INTAKE,0) AS VARCHAR)+' - '+ CAST((SELECT COUNT(*) FROM ACD_STUDENT_RESULT WHERE schemeno=CT.SCHEMENO and courseno=CT.COURSENO and sessionno=CT.SESSIONNO AND SECTIONNO=CT.SECTIONNO AND ISNULL(CANCEL,0)=0) AS VARCHAR)) as SECTIONNO";
                string parameter_2 = "'[ '+S.SECTIONNAME+' ] - '+U.UA_FULLNAME AS UA_FULLNAME,CT.SECTIONNO as SECNO";
                string whereCondition = "SESSIONNO = " + (Convert.ToInt32(ddlSession.SelectedValue)) + " AND CT.COURSENO=" + ccCode.Value + " AND ISNULL(CANCEL,0)=0 AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + "";
                string orderByCondition = "CT.SECTIONNO";

                objCommon.FillDropDownList(ddlNewsec, tableName, paramter_1, parameter_2, whereCondition, orderByCondition);
                ddlNewsec.SelectedValue = lblSection.ToolTip;

                Label Intake = (Label)e.Item.FindControl("lblIntake");
                Label Filled = (Label)e.Item.FindControl("lblFilled");

                Intake.Text = ddlNewsec.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                Filled.Text = ddlNewsec.SelectedValue.ToString().Split('-')[3].Trim().ToString();


                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))
                {
                    Intake.CssClass = "label label-default";
                    Filled.CssClass = "label label-default";

                    int CalcVal = 0;
                    for (int i = 0; i < ddlNewsec.Items.Count; i++)
                    {
                        if (Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[3].Trim().ToString()))
                        {
                            CalcVal++;
                        }
                    }

                    if (CalcVal == ddlNewsec.Items.Count)
                    {
                        CheckBox chkSelectCourse = (CheckBox)e.Item.FindControl("chkAccept");
                        chkSelectCourse.Enabled = false;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-danger";
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-warning";
                }
                else
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-success";
                }

                CheckBox chkcurrent = (CheckBox)e.Item.FindControl("chkElectiveGrp8");
                if (chkcurrent.Checked)
                {
                    txtAllSubjects.Text = Convert.ToString(Convert.ToInt32(txtAllSubjects.Text) + 1);

                    txtCredits.Text = Convert.ToString(Convert.ToDecimal(txtCredits.Text) + Convert.ToDecimal(((Label)e.Item.FindControl("lblCredits")).Text));

                    ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;

                    if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
                    {
                        if (ViewState["ACCEPT"].ToString() != "0")//accepted
                        {
                            lvCurrentSubjects.Enabled = false;
                            lvElectiveCourse.Enabled = false;
                            lvElectiveCourseGrp2.Enabled = false;
                            lvElectiveCourseGrp3.Enabled = false;
                            lvElectiveCourseGrp4.Enabled = false;
                            lvElectiveCourseGrp5.Enabled = false;
                            lvElectiveCourseGrp6.Enabled = false;
                            lvElectiveCourseGrp7.Enabled = false;
                            lvElectiveCourseGrp8.Enabled = false;
                            lvElectiveCourseGrp9.Enabled = false;
                            lvElectiveCourseGrp10.Enabled = false;
                            lvElectiveCourseGrp11.Enabled = false;
                            lvElectiveCourseGrp12.Enabled = false;
                            lvElectiveCourseGrp13.Enabled = false;
                            lvElectiveCourseGrp14.Enabled = false;
                            lvElectiveCourseGrp15.Enabled = false;
                            lvDetained.Enabled = false;
                            lvMovableSubjects.Enabled = false;
                            btnSubmit.Enabled = false;
                        }
                        else
                        {
                            lvCurrentSubjects.Enabled = true;
                            lvElectiveCourse.Enabled = true;
                            lvElectiveCourseGrp2.Enabled = true;
                            lvElectiveCourseGrp3.Enabled = true;
                            lvElectiveCourseGrp4.Enabled = true;
                            lvElectiveCourseGrp5.Enabled = true;
                            lvElectiveCourseGrp6.Enabled = true;
                            lvElectiveCourseGrp7.Enabled = true;
                            lvElectiveCourseGrp8.Enabled = true;
                            lvElectiveCourseGrp9.Enabled = true;
                            lvElectiveCourseGrp10.Enabled = true;
                            lvElectiveCourseGrp11.Enabled = true;
                            lvElectiveCourseGrp12.Enabled = true;
                            lvElectiveCourseGrp13.Enabled = true;
                            lvElectiveCourseGrp14.Enabled = true;
                            lvElectiveCourseGrp15.Enabled = true;
                            lvDetained.Enabled = true;
                            lvMovableSubjects.Enabled = true;
                            btnSubmit.Enabled = true;
                        }
                    }
                    else
                    {
                        ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;
                        lvCurrentSubjects.Enabled = false;
                        lvElectiveCourse.Enabled = false;
                        lvElectiveCourseGrp2.Enabled = false;
                        lvElectiveCourseGrp3.Enabled = false;
                        lvElectiveCourseGrp4.Enabled = false;
                        lvElectiveCourseGrp5.Enabled = false;
                        lvElectiveCourseGrp6.Enabled = false;
                        lvElectiveCourseGrp7.Enabled = false;
                        lvElectiveCourseGrp8.Enabled = false;
                        lvElectiveCourseGrp9.Enabled = false;
                        lvElectiveCourseGrp10.Enabled = false;
                        lvElectiveCourseGrp11.Enabled = false;
                        lvElectiveCourseGrp12.Enabled = false;
                        lvElectiveCourseGrp13.Enabled = false;
                        lvElectiveCourseGrp14.Enabled = false;
                        lvElectiveCourseGrp15.Enabled = false;
                        lvDetained.Enabled = false;
                        lvMovableSubjects.Enabled = false;
                        btnSubmit.Enabled = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration_SingleStudent --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void chkElectiveGrp8_CheckedChanged(object sender, EventArgs e)
    {
        int ElectGrp8Count = 0, maxEleAllowGrp8 = 0;
        DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SECTION SE ON (SE.SECTIONNO = s.SECTIONNO)", "S.IDNO,S.ROLLNO,DG.DEGREENAME,SECTIONNAME,s.SECTIONNO", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO as ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE ISNULL(S.PHYSICALLY_HANDICAPPED,0) WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH,IDTYPE,(CASE ISNULL(S.IDTYPE,0) WHEN '3' THEN 'DIRECT SECOND YEAR ' ELSE  'REGULAR'   END) AS STUD_TYPE", "S.ENROLLNO='" + txtRollNo.Text.Trim() + "'", string.Empty);

        int adm_type = 1;
        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()) == 3) //3 direct second year
        {
            adm_type = 2;
        }
        int student_type = 1;//1 for All Pass      

        DataSet dsCreditInfo8 = null;
        dsCreditInfo8 = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT", "ElectGrp8", "", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "AND STUDENT_TYPE =" + student_type + "AND DEGREENO =" + dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND ADM_TYPE=" + adm_type + "AND " + dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() + "IN (select VALUE FROM DBO.SPLIT( SEMESTER,','))", "");


        maxEleAllowGrp8 = (dsCreditInfo8.Tables[0].Rows[0]["ElectGrp8"].ToString() == string.Empty ? 1 : Convert.ToInt32(dsCreditInfo8.Tables[0].Rows[0]["ElectGrp8"].ToString()));


        CheckBox chk = sender as CheckBox;
        int totalsub = Convert.ToInt32(txtAllSubjects.Text);
        decimal totSubjectCredit = Convert.ToDecimal(txtCredits.Text);

        ListViewItem item = (ListViewItem)chk.NamingContainer;
        HiddenField hf = (HiddenField)item.FindControl("hdfCCode");
        HiddenField hfStatus = (HiddenField)item.FindControl("hfStatus");

        foreach (ListViewDataItem dataitem in lvElectiveCourseGrp8.Items)
        {
            CheckBox chk1 = dataitem.FindControl("chkElectiveGrp8") as CheckBox;
            if (chk1.Checked == true)
                ElectGrp8Count++;
        }

        DropDownList ddlSecTeacher = (DropDownList)item.FindControl("ddlsection");  //Added by Abhinay Lad [30-06-2019]
        if (chk.Checked)
        {
            decimal cred = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "ISNULL(CREDITS,0)", "COURSENO = " + hf.Value));

            totSubjectCredit = totSubjectCredit + Convert.ToDecimal(cred);
            totalsub = totalsub + 1;

            if (ElectGrp8Count > maxEleAllowGrp8)
            {
                totSubjectCredit = totSubjectCredit - Convert.ToDecimal(cred);
                totalsub = totalsub - 1;
                chk.Checked = false;

                txtAllSubjects.Text = CheckTotalSubjects().ToString();
                txtCredits.Text = CheckTotalCredits().ToString();

                objCommon.DisplayMessage(this.updDiv, "Maximum " + maxEleAllowGrp8 + " Elective Subject are allowed for registration from [Group-8]", this.Page);
                return;
            }
            else
            {
                ddlSecTeacher.Enabled = true;
            }
        }
        else if (chk.Checked == false)
        {
            ddlSecTeacher.Enabled = false;
        }
        txtAllSubjects.Text = CheckTotalSubjects().ToString();
        txtCredits.Text = CheckTotalCredits().ToString();
    }

    protected void lvElectiveCourseGrp9_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlNewsec = e.Item.FindControl("ddlsection") as DropDownList;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField ccCode = (HiddenField)e.Item.FindControl("hdfCCode");

                string tableName = "ACD_COURSE_TEACHER CT INNER JOIN USER_aCC U ON (U.UA_NO=CT.UA_NO OR U.UA_NO=CT.ADTEACHER) INNER JOIN ACD_SECTION S ON S.SECTIONNO=CT.SECTIONNO";
                string paramter_1 = "DISTINCT (CAST(S.SECTIONNO AS VARCHAR)+' - '+ CAST(COALESCE(NULLIF(CT.UA_NO,''), CT.ADTEACHER) AS VARCHAR)+' - '+CAST(ISNULL(CT.INTAKE,0) AS VARCHAR)+' - '+ CAST((SELECT COUNT(*) FROM ACD_STUDENT_RESULT WHERE schemeno=CT.SCHEMENO and courseno=CT.COURSENO and sessionno=CT.SESSIONNO AND SECTIONNO=CT.SECTIONNO AND ISNULL(CANCEL,0)=0) AS VARCHAR)) as SECTIONNO";
                string parameter_2 = "'[ '+S.SECTIONNAME+' ] - '+U.UA_FULLNAME AS UA_FULLNAME,CT.SECTIONNO as SECNO";
                string whereCondition = "SESSIONNO = " + (Convert.ToInt32(ddlSession.SelectedValue)) + " AND CT.COURSENO=" + ccCode.Value + " AND ISNULL(CANCEL,0)=0 AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + "";
                string orderByCondition = "CT.SECTIONNO";

                objCommon.FillDropDownList(ddlNewsec, tableName, paramter_1, parameter_2, whereCondition, orderByCondition);
                ddlNewsec.SelectedValue = lblSection.ToolTip;

                Label Intake = (Label)e.Item.FindControl("lblIntake");
                Label Filled = (Label)e.Item.FindControl("lblFilled");

                Intake.Text = ddlNewsec.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                Filled.Text = ddlNewsec.SelectedValue.ToString().Split('-')[3].Trim().ToString();


                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))
                {
                    Intake.CssClass = "label label-default";
                    Filled.CssClass = "label label-default";

                    int CalcVal = 0;
                    for (int i = 0; i < ddlNewsec.Items.Count; i++)
                    {
                        if (Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[3].Trim().ToString()))
                        {
                            CalcVal++;
                        }
                    }

                    if (CalcVal == ddlNewsec.Items.Count)
                    {
                        CheckBox chkSelectCourse = (CheckBox)e.Item.FindControl("chkAccept");
                        chkSelectCourse.Enabled = false;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-danger";
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-warning";
                }
                else
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-success";
                }

                CheckBox chkcurrent = (CheckBox)e.Item.FindControl("chkElectiveGrp9");
                if (chkcurrent.Checked)
                {
                    txtAllSubjects.Text = Convert.ToString(Convert.ToInt32(txtAllSubjects.Text) + 1);

                    txtCredits.Text = Convert.ToString(Convert.ToDecimal(txtCredits.Text) + Convert.ToDecimal(((Label)e.Item.FindControl("lblCredits")).Text));

                    ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;

                    if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
                    {
                        if (ViewState["ACCEPT"].ToString() != "0")//accepted
                        {
                            lvCurrentSubjects.Enabled = false;
                            lvElectiveCourse.Enabled = false;
                            lvElectiveCourseGrp2.Enabled = false;
                            lvElectiveCourseGrp3.Enabled = false;
                            lvElectiveCourseGrp4.Enabled = false;
                            lvElectiveCourseGrp5.Enabled = false;
                            lvElectiveCourseGrp6.Enabled = false;
                            lvElectiveCourseGrp7.Enabled = false;
                            lvElectiveCourseGrp8.Enabled = false;
                            lvElectiveCourseGrp9.Enabled = false;
                            lvElectiveCourseGrp10.Enabled = false;
                            lvElectiveCourseGrp11.Enabled = false;
                            lvElectiveCourseGrp12.Enabled = false;
                            lvElectiveCourseGrp13.Enabled = false;
                            lvElectiveCourseGrp14.Enabled = false;
                            lvElectiveCourseGrp15.Enabled = false;
                            lvDetained.Enabled = false;
                            lvMovableSubjects.Enabled = false;
                            btnSubmit.Enabled = false;
                        }
                        else
                        {
                            lvCurrentSubjects.Enabled = true;
                            lvElectiveCourse.Enabled = true;
                            lvElectiveCourseGrp2.Enabled = true;
                            lvElectiveCourseGrp3.Enabled = true;
                            lvElectiveCourseGrp4.Enabled = true;
                            lvElectiveCourseGrp5.Enabled = true;
                            lvElectiveCourseGrp6.Enabled = true;
                            lvElectiveCourseGrp7.Enabled = true;
                            lvElectiveCourseGrp8.Enabled = true;
                            lvElectiveCourseGrp9.Enabled = true;
                            lvElectiveCourseGrp10.Enabled = true;
                            lvElectiveCourseGrp11.Enabled = true;
                            lvElectiveCourseGrp12.Enabled = true;
                            lvElectiveCourseGrp13.Enabled = true;
                            lvElectiveCourseGrp14.Enabled = true;
                            lvElectiveCourseGrp15.Enabled = true;
                            lvDetained.Enabled = true;
                            lvMovableSubjects.Enabled = true;
                            btnSubmit.Enabled = true;
                        }
                    }
                    else
                    {
                        ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;
                        lvCurrentSubjects.Enabled = false;
                        lvElectiveCourse.Enabled = false;
                        lvElectiveCourseGrp2.Enabled = false;
                        lvElectiveCourseGrp3.Enabled = false;
                        lvElectiveCourseGrp4.Enabled = false;
                        lvElectiveCourseGrp5.Enabled = false;
                        lvElectiveCourseGrp6.Enabled = false;
                        lvElectiveCourseGrp7.Enabled = false;
                        lvElectiveCourseGrp8.Enabled = false;
                        lvElectiveCourseGrp9.Enabled = false;
                        lvElectiveCourseGrp10.Enabled = false;
                        lvElectiveCourseGrp11.Enabled = false;
                        lvElectiveCourseGrp12.Enabled = false;
                        lvElectiveCourseGrp13.Enabled = false;
                        lvElectiveCourseGrp14.Enabled = false;
                        lvElectiveCourseGrp15.Enabled = false;
                        lvDetained.Enabled = false;
                        lvMovableSubjects.Enabled = false;
                        btnSubmit.Enabled = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration_SingleStudent --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void chkElectiveGrp9_CheckedChanged(object sender, EventArgs e)
    {
        int ElectGrp9Count = 0, maxEleAllowGrp9 = 0;
        DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SECTION SE ON (SE.SECTIONNO = s.SECTIONNO)", "S.IDNO,S.ROLLNO,DG.DEGREENAME,SECTIONNAME,s.SECTIONNO", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO as ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE ISNULL(S.PHYSICALLY_HANDICAPPED,0) WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH,IDTYPE,(CASE ISNULL(S.IDTYPE,0) WHEN '3' THEN 'DIRECT SECOND YEAR ' ELSE  'REGULAR'   END) AS STUD_TYPE", "S.ENROLLNO='" + txtRollNo.Text.Trim() + "'", string.Empty);

        int adm_type = 1;
        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()) == 3) //3 direct second year
        {
            adm_type = 2;
        }
        int student_type = 1;//1 for All Pass      

        DataSet dsCreditInfo9 = null;
        dsCreditInfo9 = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT", "ElectGrp9", "", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "AND STUDENT_TYPE =" + student_type + "AND DEGREENO =" + dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND ADM_TYPE=" + adm_type + "AND " + dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() + "IN (select VALUE FROM DBO.SPLIT( SEMESTER,','))", "");


        maxEleAllowGrp9 = (dsCreditInfo9.Tables[0].Rows[0]["ElectGrp9"].ToString() == string.Empty ? 1 : Convert.ToInt32(dsCreditInfo9.Tables[0].Rows[0]["ElectGrp9"].ToString()));


        CheckBox chk = sender as CheckBox;
        int totalsub = Convert.ToInt32(txtAllSubjects.Text);
        decimal totSubjectCredit = Convert.ToDecimal(txtCredits.Text);

        ListViewItem item = (ListViewItem)chk.NamingContainer;
        HiddenField hf = (HiddenField)item.FindControl("hdfCCode");
        HiddenField hfStatus = (HiddenField)item.FindControl("hfStatus");

        foreach (ListViewDataItem dataitem in lvElectiveCourseGrp9.Items)
        {
            CheckBox chk1 = dataitem.FindControl("chkElectiveGrp9") as CheckBox;
            if (chk1.Checked == true)
                ElectGrp9Count++;
        }

        DropDownList ddlSecTeacher = (DropDownList)item.FindControl("ddlsection");  //Added by Abhinay Lad [30-06-2019]
        if (chk.Checked)
        {
            decimal cred = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "ISNULL(CREDITS,0)", "COURSENO = " + hf.Value));

            totSubjectCredit = totSubjectCredit + Convert.ToDecimal(cred);
            totalsub = totalsub + 1;

            if (ElectGrp9Count > maxEleAllowGrp9)
            {
                totSubjectCredit = totSubjectCredit - Convert.ToDecimal(cred);
                totalsub = totalsub - 1;
                chk.Checked = false;

                txtAllSubjects.Text = CheckTotalSubjects().ToString();
                txtCredits.Text = CheckTotalCredits().ToString();

                objCommon.DisplayMessage(this.updDiv, "Maximum " + maxEleAllowGrp9 + " Elective Subject are allowed for registration from [Group-9]", this.Page);
                return;
            }
            else
            {
                ddlSecTeacher.Enabled = true;
            }
        }
        else if (chk.Checked == false)
        {
            ddlSecTeacher.Enabled = false;
        }
        txtAllSubjects.Text = CheckTotalSubjects().ToString();
        txtCredits.Text = CheckTotalCredits().ToString();
    }

    protected void lvElectiveCourseGrp10_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlNewsec = e.Item.FindControl("ddlsection") as DropDownList;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField ccCode = (HiddenField)e.Item.FindControl("hdfCCode");

                string tableName = "ACD_COURSE_TEACHER CT INNER JOIN USER_aCC U ON (U.UA_NO=CT.UA_NO OR U.UA_NO=CT.ADTEACHER) INNER JOIN ACD_SECTION S ON S.SECTIONNO=CT.SECTIONNO";
                string paramter_1 = "DISTINCT (CAST(S.SECTIONNO AS VARCHAR)+' - '+ CAST(COALESCE(NULLIF(CT.UA_NO,''), CT.ADTEACHER) AS VARCHAR)+' - '+CAST(ISNULL(CT.INTAKE,0) AS VARCHAR)+' - '+ CAST((SELECT COUNT(*) FROM ACD_STUDENT_RESULT WHERE schemeno=CT.SCHEMENO and courseno=CT.COURSENO and sessionno=CT.SESSIONNO AND SECTIONNO=CT.SECTIONNO AND ISNULL(CANCEL,0)=0) AS VARCHAR)) as SECTIONNO";
                string parameter_2 = "'[ '+S.SECTIONNAME+' ] - '+U.UA_FULLNAME AS UA_FULLNAME,CT.SECTIONNO as SECNO";
                string whereCondition = "SESSIONNO = " + (Convert.ToInt32(ddlSession.SelectedValue)) + " AND CT.COURSENO=" + ccCode.Value + " AND ISNULL(CANCEL,0)=0 AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + "";
                string orderByCondition = "CT.SECTIONNO";

                objCommon.FillDropDownList(ddlNewsec, tableName, paramter_1, parameter_2, whereCondition, orderByCondition);
                ddlNewsec.SelectedValue = lblSection.ToolTip;

                Label Intake = (Label)e.Item.FindControl("lblIntake");
                Label Filled = (Label)e.Item.FindControl("lblFilled");

                Intake.Text = ddlNewsec.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                Filled.Text = ddlNewsec.SelectedValue.ToString().Split('-')[3].Trim().ToString();


                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))
                {
                    Intake.CssClass = "label label-default";
                    Filled.CssClass = "label label-default";

                    int CalcVal = 0;
                    for (int i = 0; i < ddlNewsec.Items.Count; i++)
                    {
                        if (Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[3].Trim().ToString()))
                        {
                            CalcVal++;
                        }
                    }

                    if (CalcVal == ddlNewsec.Items.Count)
                    {
                        CheckBox chkSelectCourse = (CheckBox)e.Item.FindControl("chkAccept");
                        chkSelectCourse.Enabled = false;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-danger";
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-warning";
                }
                else
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-success";
                }

                CheckBox chkcurrent = (CheckBox)e.Item.FindControl("chkElectiveGrp10");
                if (chkcurrent.Checked)
                {
                    txtAllSubjects.Text = Convert.ToString(Convert.ToInt32(txtAllSubjects.Text) + 1);

                    txtCredits.Text = Convert.ToString(Convert.ToDecimal(txtCredits.Text) + Convert.ToDecimal(((Label)e.Item.FindControl("lblCredits")).Text));

                    ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;

                    if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
                    {
                        if (ViewState["ACCEPT"].ToString() != "0")//accepted
                        {
                            lvCurrentSubjects.Enabled = false;
                            lvElectiveCourse.Enabled = false;
                            lvElectiveCourseGrp2.Enabled = false;
                            lvElectiveCourseGrp3.Enabled = false;
                            lvElectiveCourseGrp4.Enabled = false;
                            lvElectiveCourseGrp5.Enabled = false;
                            lvElectiveCourseGrp6.Enabled = false;
                            lvElectiveCourseGrp7.Enabled = false;
                            lvElectiveCourseGrp8.Enabled = false;
                            lvElectiveCourseGrp9.Enabled = false;
                            lvElectiveCourseGrp10.Enabled = false;
                            lvElectiveCourseGrp11.Enabled = false;
                            lvElectiveCourseGrp12.Enabled = false;
                            lvElectiveCourseGrp13.Enabled = false;
                            lvElectiveCourseGrp14.Enabled = false;
                            lvElectiveCourseGrp15.Enabled = false;
                            lvDetained.Enabled = false;
                            lvMovableSubjects.Enabled = false;
                            btnSubmit.Enabled = false;
                        }
                        else
                        {
                            lvCurrentSubjects.Enabled = true;
                            lvElectiveCourse.Enabled = true;
                            lvElectiveCourseGrp2.Enabled = true;
                            lvElectiveCourseGrp3.Enabled = true;
                            lvElectiveCourseGrp4.Enabled = true;
                            lvElectiveCourseGrp5.Enabled = true;
                            lvElectiveCourseGrp6.Enabled = true;
                            lvElectiveCourseGrp7.Enabled = true;
                            lvElectiveCourseGrp8.Enabled = true;
                            lvElectiveCourseGrp9.Enabled = true;
                            lvElectiveCourseGrp10.Enabled = true;
                            lvElectiveCourseGrp11.Enabled = true;
                            lvElectiveCourseGrp12.Enabled = true;
                            lvElectiveCourseGrp13.Enabled = true;
                            lvElectiveCourseGrp14.Enabled = true;
                            lvElectiveCourseGrp15.Enabled = true;
                            lvDetained.Enabled = true;
                            lvMovableSubjects.Enabled = true;
                            btnSubmit.Enabled = true;
                        }
                    }
                    else
                    {
                        ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;
                        lvCurrentSubjects.Enabled = false;
                        lvElectiveCourse.Enabled = false;
                        lvElectiveCourseGrp2.Enabled = false;
                        lvElectiveCourseGrp3.Enabled = false;
                        lvElectiveCourseGrp4.Enabled = false;
                        lvElectiveCourseGrp5.Enabled = false;
                        lvElectiveCourseGrp6.Enabled = false;
                        lvElectiveCourseGrp7.Enabled = false;
                        lvElectiveCourseGrp8.Enabled = false;
                        lvElectiveCourseGrp9.Enabled = false;
                        lvElectiveCourseGrp10.Enabled = false;
                        lvElectiveCourseGrp11.Enabled = false;
                        lvElectiveCourseGrp12.Enabled = false;
                        lvElectiveCourseGrp13.Enabled = false;
                        lvElectiveCourseGrp14.Enabled = false;
                        lvElectiveCourseGrp15.Enabled = false;
                        lvDetained.Enabled = false;
                        lvMovableSubjects.Enabled = false;
                        btnSubmit.Enabled = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration_SingleStudent --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void chkElectiveGrp10_CheckedChanged(object sender, EventArgs e)
    {
        int ElectGrp10Count = 0, maxEleAllowGrp10 = 0;
        DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SECTION SE ON (SE.SECTIONNO = s.SECTIONNO)", "S.IDNO,S.ROLLNO,DG.DEGREENAME,SECTIONNAME,s.SECTIONNO", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO as ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE ISNULL(S.PHYSICALLY_HANDICAPPED,0) WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH,IDTYPE,(CASE ISNULL(S.IDTYPE,0) WHEN '3' THEN 'DIRECT SECOND YEAR ' ELSE  'REGULAR'   END) AS STUD_TYPE", "S.ENROLLNO='" + txtRollNo.Text.Trim() + "'", string.Empty);

        int adm_type = 1;
        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()) == 3) //3 direct second year
        {
            adm_type = 2;
        }
        int student_type = 1;//1 for All Pass      

        DataSet dsCreditInfo10 = null;
        dsCreditInfo10 = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT", "ElectGrp10", "", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "AND STUDENT_TYPE =" + student_type + "AND DEGREENO =" + dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND ADM_TYPE=" + adm_type + "AND " + dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() + "IN (select VALUE FROM DBO.SPLIT( SEMESTER,','))", "");


        maxEleAllowGrp10 = (dsCreditInfo10.Tables[0].Rows[0]["ElectGrp10"].ToString() == string.Empty ? 1 : Convert.ToInt32(dsCreditInfo10.Tables[0].Rows[0]["ElectGrp10"].ToString()));


        CheckBox chk = sender as CheckBox;
        int totalsub = Convert.ToInt32(txtAllSubjects.Text);
        decimal totSubjectCredit = Convert.ToDecimal(txtCredits.Text);

        ListViewItem item = (ListViewItem)chk.NamingContainer;
        HiddenField hf = (HiddenField)item.FindControl("hdfCCode");
        HiddenField hfStatus = (HiddenField)item.FindControl("hfStatus");

        foreach (ListViewDataItem dataitem in lvElectiveCourseGrp10.Items)
        {
            CheckBox chk1 = dataitem.FindControl("chkElectiveGrp10") as CheckBox;
            if (chk1.Checked == true)
                ElectGrp10Count++;
        }

        DropDownList ddlSecTeacher = (DropDownList)item.FindControl("ddlsection");  //Added by Abhinay Lad [30-06-2019]
        if (chk.Checked)
        {
            decimal cred = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "ISNULL(CREDITS,0)", "COURSENO = " + hf.Value));

            totSubjectCredit = totSubjectCredit + Convert.ToDecimal(cred);
            totalsub = totalsub + 1;

            if (ElectGrp10Count > maxEleAllowGrp10)
            {
                totSubjectCredit = totSubjectCredit - Convert.ToDecimal(cred);
                totalsub = totalsub - 1;
                chk.Checked = false;

                txtAllSubjects.Text = CheckTotalSubjects().ToString();
                txtCredits.Text = CheckTotalCredits().ToString();

                objCommon.DisplayMessage(this.updDiv, "Maximum " + maxEleAllowGrp10 + " Elective Subject are allowed for registration from [Group-10]", this.Page);
                return;
            }
            else
            {
                ddlSecTeacher.Enabled = true;
            }
        }
        else if (chk.Checked == false)
        {
            ddlSecTeacher.Enabled = false;
        }
        txtAllSubjects.Text = CheckTotalSubjects().ToString();
        txtCredits.Text = CheckTotalCredits().ToString();
    }

    protected void lvElectiveCourseGrp11_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlNewsec = e.Item.FindControl("ddlsection") as DropDownList;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField ccCode = (HiddenField)e.Item.FindControl("hdfCCode");

                string tableName = "ACD_COURSE_TEACHER CT INNER JOIN USER_aCC U ON (U.UA_NO=CT.UA_NO OR U.UA_NO=CT.ADTEACHER) INNER JOIN ACD_SECTION S ON S.SECTIONNO=CT.SECTIONNO";
                string paramter_1 = "DISTINCT (CAST(S.SECTIONNO AS VARCHAR)+' - '+ CAST(COALESCE(NULLIF(CT.UA_NO,''), CT.ADTEACHER) AS VARCHAR)+' - '+CAST(ISNULL(CT.INTAKE,0) AS VARCHAR)+' - '+ CAST((SELECT COUNT(*) FROM ACD_STUDENT_RESULT WHERE schemeno=CT.SCHEMENO and courseno=CT.COURSENO and sessionno=CT.SESSIONNO AND SECTIONNO=CT.SECTIONNO AND ISNULL(CANCEL,0)=0) AS VARCHAR)) as SECTIONNO";
                string parameter_2 = "'[ '+S.SECTIONNAME+' ] - '+U.UA_FULLNAME AS UA_FULLNAME,CT.SECTIONNO as SECNO";
                string whereCondition = "SESSIONNO = " + (Convert.ToInt32(ddlSession.SelectedValue)) + " AND CT.COURSENO=" + ccCode.Value + " AND ISNULL(CANCEL,0)=0 AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + "";
                string orderByCondition = "CT.SECTIONNO";

                objCommon.FillDropDownList(ddlNewsec, tableName, paramter_1, parameter_2, whereCondition, orderByCondition);
                ddlNewsec.SelectedValue = lblSection.ToolTip;

                Label Intake = (Label)e.Item.FindControl("lblIntake");
                Label Filled = (Label)e.Item.FindControl("lblFilled");

                Intake.Text = ddlNewsec.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                Filled.Text = ddlNewsec.SelectedValue.ToString().Split('-')[3].Trim().ToString();


                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))
                {
                    Intake.CssClass = "label label-default";
                    Filled.CssClass = "label label-default";

                    int CalcVal = 0;
                    for (int i = 0; i < ddlNewsec.Items.Count; i++)
                    {
                        if (Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[3].Trim().ToString()))
                        {
                            CalcVal++;
                        }
                    }

                    if (CalcVal == ddlNewsec.Items.Count)
                    {
                        CheckBox chkSelectCourse = (CheckBox)e.Item.FindControl("chkAccept");
                        chkSelectCourse.Enabled = false;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-danger";
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-warning";
                }
                else
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-success";
                }

                CheckBox chkcurrent = (CheckBox)e.Item.FindControl("chkElectiveGrp11");
                if (chkcurrent.Checked)
                {
                    txtAllSubjects.Text = Convert.ToString(Convert.ToInt32(txtAllSubjects.Text) + 1);

                    txtCredits.Text = Convert.ToString(Convert.ToDecimal(txtCredits.Text) + Convert.ToDecimal(((Label)e.Item.FindControl("lblCredits")).Text));

                    ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;

                    if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
                    {
                        if (ViewState["ACCEPT"].ToString() != "0")//accepted
                        {
                            lvCurrentSubjects.Enabled = false;
                            lvElectiveCourse.Enabled = false;
                            lvElectiveCourseGrp2.Enabled = false;
                            lvElectiveCourseGrp3.Enabled = false;
                            lvElectiveCourseGrp4.Enabled = false;
                            lvElectiveCourseGrp5.Enabled = false;
                            lvElectiveCourseGrp6.Enabled = false;
                            lvElectiveCourseGrp7.Enabled = false;
                            lvElectiveCourseGrp8.Enabled = false;
                            lvElectiveCourseGrp9.Enabled = false;
                            lvElectiveCourseGrp10.Enabled = false;
                            lvElectiveCourseGrp11.Enabled = false;
                            lvElectiveCourseGrp12.Enabled = false;
                            lvElectiveCourseGrp13.Enabled = false;
                            lvElectiveCourseGrp14.Enabled = false;
                            lvElectiveCourseGrp15.Enabled = false;
                            lvDetained.Enabled = false;
                            lvMovableSubjects.Enabled = false;
                            btnSubmit.Enabled = false;
                        }
                        else
                        {
                            lvCurrentSubjects.Enabled = true;
                            lvElectiveCourse.Enabled = true;
                            lvElectiveCourseGrp2.Enabled = true;
                            lvElectiveCourseGrp3.Enabled = true;
                            lvElectiveCourseGrp4.Enabled = true;
                            lvElectiveCourseGrp5.Enabled = true;
                            lvElectiveCourseGrp6.Enabled = true;
                            lvElectiveCourseGrp7.Enabled = true;
                            lvElectiveCourseGrp8.Enabled = true;
                            lvElectiveCourseGrp9.Enabled = true;
                            lvElectiveCourseGrp10.Enabled = true;
                            lvElectiveCourseGrp11.Enabled = true;
                            lvElectiveCourseGrp12.Enabled = true;
                            lvElectiveCourseGrp13.Enabled = true;
                            lvElectiveCourseGrp14.Enabled = true;
                            lvElectiveCourseGrp15.Enabled = true;
                            lvDetained.Enabled = true;
                            lvMovableSubjects.Enabled = true;
                            btnSubmit.Enabled = true;
                        }
                    }
                    else
                    {
                        ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;
                        lvCurrentSubjects.Enabled = false;
                        lvElectiveCourse.Enabled = false;
                        lvElectiveCourseGrp2.Enabled = false;
                        lvElectiveCourseGrp3.Enabled = false;
                        lvElectiveCourseGrp4.Enabled = false;
                        lvElectiveCourseGrp5.Enabled = false;
                        lvElectiveCourseGrp6.Enabled = false;
                        lvElectiveCourseGrp7.Enabled = false;
                        lvElectiveCourseGrp8.Enabled = false;
                        lvElectiveCourseGrp9.Enabled = false;
                        lvElectiveCourseGrp10.Enabled = false;
                        lvElectiveCourseGrp11.Enabled = false;
                        lvElectiveCourseGrp12.Enabled = false;
                        lvElectiveCourseGrp13.Enabled = false;
                        lvElectiveCourseGrp14.Enabled = false;
                        lvElectiveCourseGrp15.Enabled = false;
                        lvDetained.Enabled = false;
                        lvMovableSubjects.Enabled = false;
                        btnSubmit.Enabled = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration_SingleStudent --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void chkElectiveGrp11_CheckedChanged(object sender, EventArgs e)
    {
        int ElectGrp11Count = 0, maxEleAllowGrp11 = 0;
        DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SECTION SE ON (SE.SECTIONNO = s.SECTIONNO)", "S.IDNO,S.ROLLNO,DG.DEGREENAME,SECTIONNAME,s.SECTIONNO", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO as ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE ISNULL(S.PHYSICALLY_HANDICAPPED,0) WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH,IDTYPE,(CASE ISNULL(S.IDTYPE,0) WHEN '3' THEN 'DIRECT SECOND YEAR ' ELSE  'REGULAR'   END) AS STUD_TYPE", "S.ENROLLNO='" + txtRollNo.Text.Trim() + "'", string.Empty);

        int adm_type = 1;
        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()) == 3) //3 direct second year
        {
            adm_type = 2;
        }
        int student_type = 1;//1 for All Pass      

        DataSet dsCreditInfo11 = null;
        dsCreditInfo11 = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT", "ElectGrp11", "", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "AND STUDENT_TYPE =" + student_type + "AND DEGREENO =" + dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND ADM_TYPE=" + adm_type + "AND " + dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() + "IN (select VALUE FROM DBO.SPLIT( SEMESTER,','))", "");


        maxEleAllowGrp11 = (dsCreditInfo11.Tables[0].Rows[0]["ElectGrp11"].ToString() == string.Empty ? 1 : Convert.ToInt32(dsCreditInfo11.Tables[0].Rows[0]["ElectGrp11"].ToString()));


        CheckBox chk = sender as CheckBox;
        int totalsub = Convert.ToInt32(txtAllSubjects.Text);
        decimal totSubjectCredit = Convert.ToDecimal(txtCredits.Text);

        ListViewItem item = (ListViewItem)chk.NamingContainer;
        HiddenField hf = (HiddenField)item.FindControl("hdfCCode");
        HiddenField hfStatus = (HiddenField)item.FindControl("hfStatus");

        foreach (ListViewDataItem dataitem in lvElectiveCourseGrp11.Items)
        {
            CheckBox chk1 = dataitem.FindControl("chkElectiveGrp11") as CheckBox;
            if (chk1.Checked == true)
                ElectGrp11Count++;
        }

        DropDownList ddlSecTeacher = (DropDownList)item.FindControl("ddlsection");  //Added by Abhinay Lad [30-06-2019]
        if (chk.Checked)
        {
            decimal cred = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "ISNULL(CREDITS,0)", "COURSENO = " + hf.Value));

            totSubjectCredit = totSubjectCredit + Convert.ToDecimal(cred);
            totalsub = totalsub + 1;

            if (ElectGrp11Count > maxEleAllowGrp11)
            {
                totSubjectCredit = totSubjectCredit - Convert.ToDecimal(cred);
                totalsub = totalsub - 1;
                chk.Checked = false;

                txtAllSubjects.Text = CheckTotalSubjects().ToString();
                txtCredits.Text = CheckTotalCredits().ToString();

                objCommon.DisplayMessage(this.updDiv, "Maximum " + maxEleAllowGrp11 + " Elective Subject are allowed for registration from [Group-11]", this.Page);
                return;
            }
            else
            {
                ddlSecTeacher.Enabled = true;
            }
        }
        else if (chk.Checked == false)
        {
            ddlSecTeacher.Enabled = false;
        }
        txtAllSubjects.Text = CheckTotalSubjects().ToString();
        txtCredits.Text = CheckTotalCredits().ToString();
    }

    protected void lvElectiveCourseGrp12_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlNewsec = e.Item.FindControl("ddlsection") as DropDownList;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField ccCode = (HiddenField)e.Item.FindControl("hdfCCode");

                string tableName = "ACD_COURSE_TEACHER CT INNER JOIN USER_aCC U ON (U.UA_NO=CT.UA_NO OR U.UA_NO=CT.ADTEACHER) INNER JOIN ACD_SECTION S ON S.SECTIONNO=CT.SECTIONNO";
                string paramter_1 = "DISTINCT (CAST(S.SECTIONNO AS VARCHAR)+' - '+ CAST(COALESCE(NULLIF(CT.UA_NO,''), CT.ADTEACHER) AS VARCHAR)+' - '+CAST(ISNULL(CT.INTAKE,0) AS VARCHAR)+' - '+ CAST((SELECT COUNT(*) FROM ACD_STUDENT_RESULT WHERE schemeno=CT.SCHEMENO and courseno=CT.COURSENO and sessionno=CT.SESSIONNO AND SECTIONNO=CT.SECTIONNO AND ISNULL(CANCEL,0)=0) AS VARCHAR)) as SECTIONNO";
                string parameter_2 = "'[ '+S.SECTIONNAME+' ] - '+U.UA_FULLNAME AS UA_FULLNAME,CT.SECTIONNO as SECNO";
                string whereCondition = "SESSIONNO = " + (Convert.ToInt32(ddlSession.SelectedValue)) + " AND CT.COURSENO=" + ccCode.Value + " AND ISNULL(CANCEL,0)=0 AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + "";
                string orderByCondition = "CT.SECTIONNO";

                objCommon.FillDropDownList(ddlNewsec, tableName, paramter_1, parameter_2, whereCondition, orderByCondition);
                ddlNewsec.SelectedValue = lblSection.ToolTip;

                Label Intake = (Label)e.Item.FindControl("lblIntake");
                Label Filled = (Label)e.Item.FindControl("lblFilled");

                Intake.Text = ddlNewsec.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                Filled.Text = ddlNewsec.SelectedValue.ToString().Split('-')[3].Trim().ToString();


                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))
                {
                    Intake.CssClass = "label label-default";
                    Filled.CssClass = "label label-default";

                    int CalcVal = 0;
                    for (int i = 0; i < ddlNewsec.Items.Count; i++)
                    {
                        if (Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[3].Trim().ToString()))
                        {
                            CalcVal++;
                        }
                    }

                    if (CalcVal == ddlNewsec.Items.Count)
                    {
                        CheckBox chkSelectCourse = (CheckBox)e.Item.FindControl("chkAccept");
                        chkSelectCourse.Enabled = false;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-danger";
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-warning";
                }
                else
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-success";
                }

                CheckBox chkcurrent = (CheckBox)e.Item.FindControl("chkElectiveGrp12");
                if (chkcurrent.Checked)
                {
                    txtAllSubjects.Text = Convert.ToString(Convert.ToInt32(txtAllSubjects.Text) + 1);

                    txtCredits.Text = Convert.ToString(Convert.ToDecimal(txtCredits.Text) + Convert.ToDecimal(((Label)e.Item.FindControl("lblCredits")).Text));

                    ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;

                    if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
                    {
                        if (ViewState["ACCEPT"].ToString() != "0")//accepted
                        {
                            lvCurrentSubjects.Enabled = false;
                            lvElectiveCourse.Enabled = false;
                            lvElectiveCourseGrp2.Enabled = false;
                            lvElectiveCourseGrp3.Enabled = false;
                            lvElectiveCourseGrp4.Enabled = false;
                            lvElectiveCourseGrp5.Enabled = false;
                            lvElectiveCourseGrp6.Enabled = false;
                            lvElectiveCourseGrp7.Enabled = false;
                            lvElectiveCourseGrp8.Enabled = false;
                            lvElectiveCourseGrp9.Enabled = false;
                            lvElectiveCourseGrp10.Enabled = false;
                            lvElectiveCourseGrp11.Enabled = false;
                            lvElectiveCourseGrp12.Enabled = false;
                            lvElectiveCourseGrp13.Enabled = false;
                            lvElectiveCourseGrp14.Enabled = false;
                            lvElectiveCourseGrp15.Enabled = false;
                            lvDetained.Enabled = false;
                            lvMovableSubjects.Enabled = false;
                            btnSubmit.Enabled = false;
                        }
                        else
                        {
                            lvCurrentSubjects.Enabled = true;
                            lvElectiveCourse.Enabled = true;
                            lvElectiveCourseGrp2.Enabled = true;
                            lvElectiveCourseGrp3.Enabled = true;
                            lvElectiveCourseGrp4.Enabled = true;
                            lvElectiveCourseGrp5.Enabled = true;
                            lvElectiveCourseGrp6.Enabled = true;
                            lvElectiveCourseGrp7.Enabled = true;
                            lvElectiveCourseGrp8.Enabled = true;
                            lvElectiveCourseGrp9.Enabled = true;
                            lvElectiveCourseGrp10.Enabled = true;
                            lvElectiveCourseGrp11.Enabled = true;
                            lvElectiveCourseGrp12.Enabled = true;
                            lvElectiveCourseGrp13.Enabled = true;
                            lvElectiveCourseGrp14.Enabled = true;
                            lvElectiveCourseGrp15.Enabled = true;
                            lvDetained.Enabled = true;
                            lvMovableSubjects.Enabled = true;
                            btnSubmit.Enabled = true;
                        }
                    }
                    else
                    {
                        ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;
                        lvCurrentSubjects.Enabled = false;
                        lvElectiveCourse.Enabled = false;
                        lvElectiveCourseGrp2.Enabled = false;
                        lvElectiveCourseGrp3.Enabled = false;
                        lvElectiveCourseGrp4.Enabled = false;
                        lvElectiveCourseGrp5.Enabled = false;
                        lvElectiveCourseGrp6.Enabled = false;
                        lvElectiveCourseGrp7.Enabled = false;
                        lvElectiveCourseGrp8.Enabled = false;
                        lvElectiveCourseGrp9.Enabled = false;
                        lvElectiveCourseGrp10.Enabled = false;
                        lvElectiveCourseGrp11.Enabled = false;
                        lvElectiveCourseGrp12.Enabled = false;
                        lvElectiveCourseGrp13.Enabled = false;
                        lvElectiveCourseGrp14.Enabled = false;
                        lvElectiveCourseGrp15.Enabled = false;
                        lvDetained.Enabled = false;
                        lvMovableSubjects.Enabled = false;
                        btnSubmit.Enabled = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration_SingleStudent --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void chkElectiveGrp12_CheckedChanged(object sender, EventArgs e)
    {
        int ElectGrp12Count = 0, maxEleAllowGrp12 = 0;
        DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SECTION SE ON (SE.SECTIONNO = s.SECTIONNO)", "S.IDNO,S.ROLLNO,DG.DEGREENAME,SECTIONNAME,s.SECTIONNO", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO as ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE ISNULL(S.PHYSICALLY_HANDICAPPED,0) WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH,IDTYPE,(CASE ISNULL(S.IDTYPE,0) WHEN '3' THEN 'DIRECT SECOND YEAR ' ELSE  'REGULAR'   END) AS STUD_TYPE", "S.ENROLLNO='" + txtRollNo.Text.Trim() + "'", string.Empty);

        int adm_type = 1;
        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()) == 3) //3 direct second year
        {
            adm_type = 2;
        }
        int student_type = 1;//1 for All Pass      

        DataSet dsCreditInfo12 = null;
        dsCreditInfo12 = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT", "ElectGrp12", "", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "AND STUDENT_TYPE =" + student_type + "AND DEGREENO =" + dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND ADM_TYPE=" + adm_type + "AND " + dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() + "IN (select VALUE FROM DBO.SPLIT( SEMESTER,','))", "");


        maxEleAllowGrp12 = (dsCreditInfo12.Tables[0].Rows[0]["ElectGrp12"].ToString() == string.Empty ? 1 : Convert.ToInt32(dsCreditInfo12.Tables[0].Rows[0]["ElectGrp12"].ToString()));


        CheckBox chk = sender as CheckBox;
        int totalsub = Convert.ToInt32(txtAllSubjects.Text);
        decimal totSubjectCredit = Convert.ToDecimal(txtCredits.Text);

        ListViewItem item = (ListViewItem)chk.NamingContainer;
        HiddenField hf = (HiddenField)item.FindControl("hdfCCode");
        HiddenField hfStatus = (HiddenField)item.FindControl("hfStatus");

        foreach (ListViewDataItem dataitem in lvElectiveCourseGrp12.Items)
        {
            CheckBox chk1 = dataitem.FindControl("chkElectiveGrp12") as CheckBox;
            if (chk1.Checked == true)
                ElectGrp12Count++;
        }

        DropDownList ddlSecTeacher = (DropDownList)item.FindControl("ddlsection");  //Added by Abhinay Lad [30-06-2019]
        if (chk.Checked)
        {
            decimal cred = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "ISNULL(CREDITS,0)", "COURSENO = " + hf.Value));

            totSubjectCredit = totSubjectCredit + Convert.ToDecimal(cred);
            totalsub = totalsub + 1;

            if (ElectGrp12Count > maxEleAllowGrp12)
            {
                totSubjectCredit = totSubjectCredit - Convert.ToDecimal(cred);
                totalsub = totalsub - 1;
                chk.Checked = false;

                txtAllSubjects.Text = CheckTotalSubjects().ToString();
                txtCredits.Text = CheckTotalCredits().ToString();

                objCommon.DisplayMessage(this.updDiv, "Maximum " + maxEleAllowGrp12 + " Elective Subject are allowed for registration from [Group-12]", this.Page);
                return;
            }
            else
            {
                ddlSecTeacher.Enabled = true;
            }
        }
        else if (chk.Checked == false)
        {
            ddlSecTeacher.Enabled = false;
        }
        txtAllSubjects.Text = CheckTotalSubjects().ToString();
        txtCredits.Text = CheckTotalCredits().ToString();
    }

    protected void lvElectiveCourseGrp13_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlNewsec = e.Item.FindControl("ddlsection") as DropDownList;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField ccCode = (HiddenField)e.Item.FindControl("hdfCCode");

                string tableName = "ACD_COURSE_TEACHER CT INNER JOIN USER_aCC U ON (U.UA_NO=CT.UA_NO OR U.UA_NO=CT.ADTEACHER) INNER JOIN ACD_SECTION S ON S.SECTIONNO=CT.SECTIONNO";
                string paramter_1 = "DISTINCT (CAST(S.SECTIONNO AS VARCHAR)+' - '+ CAST(COALESCE(NULLIF(CT.UA_NO,''), CT.ADTEACHER) AS VARCHAR)+' - '+CAST(ISNULL(CT.INTAKE,0) AS VARCHAR)+' - '+ CAST((SELECT COUNT(*) FROM ACD_STUDENT_RESULT WHERE schemeno=CT.SCHEMENO and courseno=CT.COURSENO and sessionno=CT.SESSIONNO AND SECTIONNO=CT.SECTIONNO AND ISNULL(CANCEL,0)=0) AS VARCHAR)) as SECTIONNO";
                string parameter_2 = "'[ '+S.SECTIONNAME+' ] - '+U.UA_FULLNAME AS UA_FULLNAME,CT.SECTIONNO as SECNO";
                string whereCondition = "SESSIONNO = " + (Convert.ToInt32(ddlSession.SelectedValue)) + " AND CT.COURSENO=" + ccCode.Value + " AND ISNULL(CANCEL,0)=0 AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + "";
                string orderByCondition = "CT.SECTIONNO";

                objCommon.FillDropDownList(ddlNewsec, tableName, paramter_1, parameter_2, whereCondition, orderByCondition);
                ddlNewsec.SelectedValue = lblSection.ToolTip;

                Label Intake = (Label)e.Item.FindControl("lblIntake");
                Label Filled = (Label)e.Item.FindControl("lblFilled");

                Intake.Text = ddlNewsec.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                Filled.Text = ddlNewsec.SelectedValue.ToString().Split('-')[3].Trim().ToString();


                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))
                {
                    Intake.CssClass = "label label-default";
                    Filled.CssClass = "label label-default";

                    int CalcVal = 0;
                    for (int i = 0; i < ddlNewsec.Items.Count; i++)
                    {
                        if (Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[3].Trim().ToString()))
                        {
                            CalcVal++;
                        }
                    }

                    if (CalcVal == ddlNewsec.Items.Count)
                    {
                        CheckBox chkSelectCourse = (CheckBox)e.Item.FindControl("chkAccept");
                        chkSelectCourse.Enabled = false;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-danger";
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-warning";
                }
                else
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-success";
                }

                CheckBox chkcurrent = (CheckBox)e.Item.FindControl("chkElectiveGrp13");
                if (chkcurrent.Checked)
                {
                    txtAllSubjects.Text = Convert.ToString(Convert.ToInt32(txtAllSubjects.Text) + 1);

                    txtCredits.Text = Convert.ToString(Convert.ToDecimal(txtCredits.Text) + Convert.ToDecimal(((Label)e.Item.FindControl("lblCredits")).Text));

                    ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;

                    if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
                    {
                        if (ViewState["ACCEPT"].ToString() != "0")//accepted
                        {
                            lvCurrentSubjects.Enabled = false;
                            lvElectiveCourse.Enabled = false;
                            lvElectiveCourseGrp2.Enabled = false;
                            lvElectiveCourseGrp3.Enabled = false;
                            lvElectiveCourseGrp4.Enabled = false;
                            lvElectiveCourseGrp5.Enabled = false;
                            lvElectiveCourseGrp6.Enabled = false;
                            lvElectiveCourseGrp7.Enabled = false;
                            lvElectiveCourseGrp8.Enabled = false;
                            lvElectiveCourseGrp9.Enabled = false;
                            lvElectiveCourseGrp10.Enabled = false;
                            lvElectiveCourseGrp11.Enabled = false;
                            lvElectiveCourseGrp12.Enabled = false;
                            lvElectiveCourseGrp13.Enabled = false;
                            lvElectiveCourseGrp14.Enabled = false;
                            lvElectiveCourseGrp15.Enabled = false;
                            lvDetained.Enabled = false;
                            lvMovableSubjects.Enabled = false;
                            btnSubmit.Enabled = false;
                        }
                        else
                        {
                            lvCurrentSubjects.Enabled = true;
                            lvElectiveCourse.Enabled = true;
                            lvElectiveCourseGrp2.Enabled = true;
                            lvElectiveCourseGrp3.Enabled = true;
                            lvElectiveCourseGrp4.Enabled = true;
                            lvElectiveCourseGrp5.Enabled = true;
                            lvElectiveCourseGrp6.Enabled = true;
                            lvElectiveCourseGrp7.Enabled = true;
                            lvElectiveCourseGrp8.Enabled = true;
                            lvElectiveCourseGrp9.Enabled = true;
                            lvElectiveCourseGrp10.Enabled = true;
                            lvElectiveCourseGrp11.Enabled = true;
                            lvElectiveCourseGrp12.Enabled = true;
                            lvElectiveCourseGrp13.Enabled = true;
                            lvElectiveCourseGrp14.Enabled = true;
                            lvElectiveCourseGrp15.Enabled = true;
                            lvDetained.Enabled = true;
                            lvMovableSubjects.Enabled = true;
                            btnSubmit.Enabled = true;
                        }
                    }
                    else
                    {
                        ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;
                        lvCurrentSubjects.Enabled = false;
                        lvElectiveCourse.Enabled = false;
                        lvElectiveCourseGrp2.Enabled = false;
                        lvElectiveCourseGrp3.Enabled = false;
                        lvElectiveCourseGrp4.Enabled = false;
                        lvElectiveCourseGrp5.Enabled = false;
                        lvElectiveCourseGrp6.Enabled = false;
                        lvElectiveCourseGrp7.Enabled = false;
                        lvElectiveCourseGrp8.Enabled = false;
                        lvElectiveCourseGrp9.Enabled = false;
                        lvElectiveCourseGrp10.Enabled = false;
                        lvElectiveCourseGrp11.Enabled = false;
                        lvElectiveCourseGrp12.Enabled = false;
                        lvElectiveCourseGrp13.Enabled = false;
                        lvElectiveCourseGrp14.Enabled = false;
                        lvElectiveCourseGrp15.Enabled = false;
                        lvDetained.Enabled = false;
                        lvMovableSubjects.Enabled = false;
                        btnSubmit.Enabled = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration_SingleStudent --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void chkElectiveGrp13_CheckedChanged(object sender, EventArgs e)
    {
        int ElectGrp13Count = 0, maxEleAllowGrp13 = 0;
        DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SECTION SE ON (SE.SECTIONNO = s.SECTIONNO)", "S.IDNO,S.ROLLNO,DG.DEGREENAME,SECTIONNAME,s.SECTIONNO", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO as ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE ISNULL(S.PHYSICALLY_HANDICAPPED,0) WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH,IDTYPE,(CASE ISNULL(S.IDTYPE,0) WHEN '3' THEN 'DIRECT SECOND YEAR ' ELSE  'REGULAR'   END) AS STUD_TYPE", "S.ENROLLNO='" + txtRollNo.Text.Trim() + "'", string.Empty);

        int adm_type = 1;
        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()) == 3) //3 direct second year
        {
            adm_type = 2;
        }
        int student_type = 1;//1 for All Pass      

        DataSet dsCreditInfo13 = null;
        dsCreditInfo13 = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT", "ElectGrp13", "", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "AND STUDENT_TYPE =" + student_type + "AND DEGREENO =" + dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND ADM_TYPE=" + adm_type + "AND " + dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() + "IN (select VALUE FROM DBO.SPLIT( SEMESTER,','))", "");


        maxEleAllowGrp13 = (dsCreditInfo13.Tables[0].Rows[0]["ElectGrp13"].ToString() == string.Empty ? 1 : Convert.ToInt32(dsCreditInfo13.Tables[0].Rows[0]["ElectGrp13"].ToString()));


        CheckBox chk = sender as CheckBox;
        int totalsub = Convert.ToInt32(txtAllSubjects.Text);
        decimal totSubjectCredit = Convert.ToDecimal(txtCredits.Text);

        ListViewItem item = (ListViewItem)chk.NamingContainer;
        HiddenField hf = (HiddenField)item.FindControl("hdfCCode");
        HiddenField hfStatus = (HiddenField)item.FindControl("hfStatus");

        foreach (ListViewDataItem dataitem in lvElectiveCourseGrp13.Items)
        {
            CheckBox chk1 = dataitem.FindControl("chkElectiveGrp13") as CheckBox;
            if (chk1.Checked == true)
                ElectGrp13Count++;
        }

        DropDownList ddlSecTeacher = (DropDownList)item.FindControl("ddlsection");  //Added by Abhinay Lad [30-06-2019]
        if (chk.Checked)
        {
            decimal cred = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "ISNULL(CREDITS,0)", "COURSENO = " + hf.Value));

            totSubjectCredit = totSubjectCredit + Convert.ToDecimal(cred);
            totalsub = totalsub + 1;

            if (ElectGrp13Count > maxEleAllowGrp13)
            {
                totSubjectCredit = totSubjectCredit - Convert.ToDecimal(cred);
                totalsub = totalsub - 1;
                chk.Checked = false;

                txtAllSubjects.Text = CheckTotalSubjects().ToString();
                txtCredits.Text = CheckTotalCredits().ToString();

                objCommon.DisplayMessage(this.updDiv, "Maximum " + maxEleAllowGrp13 + " Elective Subject are allowed for registration from [Group-13]", this.Page);
                return;
            }
            else
            {
                ddlSecTeacher.Enabled = true;
            }
        }
        else if (chk.Checked == false)
        {
            ddlSecTeacher.Enabled = false;
        }
        txtAllSubjects.Text = CheckTotalSubjects().ToString();
        txtCredits.Text = CheckTotalCredits().ToString();
    }

    protected void lvElectiveCourseGrp14_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlNewsec = e.Item.FindControl("ddlsection") as DropDownList;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField ccCode = (HiddenField)e.Item.FindControl("hdfCCode");

                string tableName = "ACD_COURSE_TEACHER CT INNER JOIN USER_aCC U ON (U.UA_NO=CT.UA_NO OR U.UA_NO=CT.ADTEACHER) INNER JOIN ACD_SECTION S ON S.SECTIONNO=CT.SECTIONNO";
                string paramter_1 = "DISTINCT (CAST(S.SECTIONNO AS VARCHAR)+' - '+ CAST(COALESCE(NULLIF(CT.UA_NO,''), CT.ADTEACHER) AS VARCHAR)+' - '+CAST(ISNULL(CT.INTAKE,0) AS VARCHAR)+' - '+ CAST((SELECT COUNT(*) FROM ACD_STUDENT_RESULT WHERE schemeno=CT.SCHEMENO and courseno=CT.COURSENO and sessionno=CT.SESSIONNO AND SECTIONNO=CT.SECTIONNO AND ISNULL(CANCEL,0)=0) AS VARCHAR)) as SECTIONNO";
                string parameter_2 = "'[ '+S.SECTIONNAME+' ] - '+U.UA_FULLNAME AS UA_FULLNAME,CT.SECTIONNO as SECNO";
                string whereCondition = "SESSIONNO = " + (Convert.ToInt32(ddlSession.SelectedValue)) + " AND CT.COURSENO=" + ccCode.Value + " AND ISNULL(CANCEL,0)=0 AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + "";
                string orderByCondition = "CT.SECTIONNO";

                objCommon.FillDropDownList(ddlNewsec, tableName, paramter_1, parameter_2, whereCondition, orderByCondition);
                ddlNewsec.SelectedValue = lblSection.ToolTip;

                Label Intake = (Label)e.Item.FindControl("lblIntake");
                Label Filled = (Label)e.Item.FindControl("lblFilled");

                Intake.Text = ddlNewsec.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                Filled.Text = ddlNewsec.SelectedValue.ToString().Split('-')[3].Trim().ToString();


                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))
                {
                    Intake.CssClass = "label label-default";
                    Filled.CssClass = "label label-default";

                    int CalcVal = 0;
                    for (int i = 0; i < ddlNewsec.Items.Count; i++)
                    {
                        if (Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[3].Trim().ToString()))
                        {
                            CalcVal++;
                        }
                    }

                    if (CalcVal == ddlNewsec.Items.Count)
                    {
                        CheckBox chkSelectCourse = (CheckBox)e.Item.FindControl("chkAccept");
                        chkSelectCourse.Enabled = false;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-danger";
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-warning";
                }
                else
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-success";
                }

                CheckBox chkcurrent = (CheckBox)e.Item.FindControl("chkElectiveGrp14");
                if (chkcurrent.Checked)
                {
                    txtAllSubjects.Text = Convert.ToString(Convert.ToInt32(txtAllSubjects.Text) + 1);

                    txtCredits.Text = Convert.ToString(Convert.ToDecimal(txtCredits.Text) + Convert.ToDecimal(((Label)e.Item.FindControl("lblCredits")).Text));

                    ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;

                    if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
                    {
                        if (ViewState["ACCEPT"].ToString() != "0")//accepted
                        {
                            lvCurrentSubjects.Enabled = false;
                            lvElectiveCourse.Enabled = false;
                            lvElectiveCourseGrp2.Enabled = false;
                            lvElectiveCourseGrp3.Enabled = false;
                            lvElectiveCourseGrp4.Enabled = false;
                            lvElectiveCourseGrp5.Enabled = false;
                            lvElectiveCourseGrp6.Enabled = false;
                            lvElectiveCourseGrp7.Enabled = false;
                            lvElectiveCourseGrp8.Enabled = false;
                            lvElectiveCourseGrp9.Enabled = false;
                            lvElectiveCourseGrp10.Enabled = false;
                            lvElectiveCourseGrp11.Enabled = false;
                            lvElectiveCourseGrp12.Enabled = false;
                            lvElectiveCourseGrp13.Enabled = false;
                            lvElectiveCourseGrp14.Enabled = false;
                            lvElectiveCourseGrp15.Enabled = false;
                            lvDetained.Enabled = false;
                            lvMovableSubjects.Enabled = false;
                            btnSubmit.Enabled = false;
                        }
                        else
                        {
                            lvCurrentSubjects.Enabled = true;
                            lvElectiveCourse.Enabled = true;
                            lvElectiveCourseGrp2.Enabled = true;
                            lvElectiveCourseGrp3.Enabled = true;
                            lvElectiveCourseGrp4.Enabled = true;
                            lvElectiveCourseGrp5.Enabled = true;
                            lvElectiveCourseGrp6.Enabled = true;
                            lvElectiveCourseGrp7.Enabled = true;
                            lvElectiveCourseGrp8.Enabled = true;
                            lvElectiveCourseGrp9.Enabled = true;
                            lvElectiveCourseGrp10.Enabled = true;
                            lvElectiveCourseGrp11.Enabled = true;
                            lvElectiveCourseGrp12.Enabled = true;
                            lvElectiveCourseGrp13.Enabled = true;
                            lvElectiveCourseGrp14.Enabled = true;
                            lvElectiveCourseGrp15.Enabled = true;
                            lvDetained.Enabled = true;
                            lvMovableSubjects.Enabled = true;
                            btnSubmit.Enabled = true;
                        }
                    }
                    else
                    {
                        ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;
                        lvCurrentSubjects.Enabled = false;
                        lvElectiveCourse.Enabled = false;
                        lvElectiveCourseGrp2.Enabled = false;
                        lvElectiveCourseGrp3.Enabled = false;
                        lvElectiveCourseGrp4.Enabled = false;
                        lvElectiveCourseGrp5.Enabled = false;
                        lvElectiveCourseGrp6.Enabled = false;
                        lvElectiveCourseGrp7.Enabled = false;
                        lvElectiveCourseGrp8.Enabled = false;
                        lvElectiveCourseGrp9.Enabled = false;
                        lvElectiveCourseGrp10.Enabled = false;
                        lvElectiveCourseGrp11.Enabled = false;
                        lvElectiveCourseGrp12.Enabled = false;
                        lvElectiveCourseGrp13.Enabled = false;
                        lvElectiveCourseGrp14.Enabled = false;
                        lvElectiveCourseGrp15.Enabled = false;
                        lvDetained.Enabled = false;
                        lvMovableSubjects.Enabled = false;
                        btnSubmit.Enabled = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration_SingleStudent --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void chkElectiveGrp14_CheckedChanged(object sender, EventArgs e)
    {
        int ElectGrp14Count = 0, maxEleAllowGrp14 = 0;
        DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SECTION SE ON (SE.SECTIONNO = s.SECTIONNO)", "S.IDNO,S.ROLLNO,DG.DEGREENAME,SECTIONNAME,s.SECTIONNO", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO as ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE ISNULL(S.PHYSICALLY_HANDICAPPED,0) WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH,IDTYPE,(CASE ISNULL(S.IDTYPE,0) WHEN '3' THEN 'DIRECT SECOND YEAR ' ELSE  'REGULAR'   END) AS STUD_TYPE", "S.ENROLLNO='" + txtRollNo.Text.Trim() + "'", string.Empty);

        int adm_type = 1;
        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()) == 3) //3 direct second year
        {
            adm_type = 2;
        }
        int student_type = 1;//1 for All Pass      

        DataSet dsCreditInfo14 = null;
        dsCreditInfo14 = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT", "ElectGrp14", "", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "AND STUDENT_TYPE =" + student_type + "AND DEGREENO =" + dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND ADM_TYPE=" + adm_type + "AND " + dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() + "IN (select VALUE FROM DBO.SPLIT( SEMESTER,','))", "");


        maxEleAllowGrp14 = (dsCreditInfo14.Tables[0].Rows[0]["ElectGrp14"].ToString() == string.Empty ? 1 : Convert.ToInt32(dsCreditInfo14.Tables[0].Rows[0]["ElectGrp14"].ToString()));


        CheckBox chk = sender as CheckBox;
        int totalsub = Convert.ToInt32(txtAllSubjects.Text);
        decimal totSubjectCredit = Convert.ToDecimal(txtCredits.Text);

        ListViewItem item = (ListViewItem)chk.NamingContainer;
        HiddenField hf = (HiddenField)item.FindControl("hdfCCode");
        HiddenField hfStatus = (HiddenField)item.FindControl("hfStatus");

        foreach (ListViewDataItem dataitem in lvElectiveCourseGrp14.Items)
        {
            CheckBox chk1 = dataitem.FindControl("chkElectiveGrp14") as CheckBox;
            if (chk1.Checked == true)
                ElectGrp14Count++;
        }

        DropDownList ddlSecTeacher = (DropDownList)item.FindControl("ddlsection");  //Added by Abhinay Lad [30-06-2019]
        if (chk.Checked)
        {
            decimal cred = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "ISNULL(CREDITS,0)", "COURSENO = " + hf.Value));

            totSubjectCredit = totSubjectCredit + Convert.ToDecimal(cred);
            totalsub = totalsub + 1;

            if (ElectGrp14Count > maxEleAllowGrp14)
            {
                totSubjectCredit = totSubjectCredit - Convert.ToDecimal(cred);
                totalsub = totalsub - 1;
                chk.Checked = false;

                txtAllSubjects.Text = CheckTotalSubjects().ToString();
                txtCredits.Text = CheckTotalCredits().ToString();

                objCommon.DisplayMessage(this.updDiv, "Maximum " + maxEleAllowGrp14 + " Elective Subject are allowed for registration from [Group-14]", this.Page);
                return;
            }
            else
            {
                ddlSecTeacher.Enabled = true;
            }
        }
        else if (chk.Checked == false)
        {
            ddlSecTeacher.Enabled = false;
        }
        txtAllSubjects.Text = CheckTotalSubjects().ToString();
        txtCredits.Text = CheckTotalCredits().ToString();
    }

    protected void lvElectiveCourseGrp15_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlNewsec = e.Item.FindControl("ddlsection") as DropDownList;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField ccCode = (HiddenField)e.Item.FindControl("hdfCCode");

                string tableName = "ACD_COURSE_TEACHER CT INNER JOIN USER_aCC U ON (U.UA_NO=CT.UA_NO OR U.UA_NO=CT.ADTEACHER) INNER JOIN ACD_SECTION S ON S.SECTIONNO=CT.SECTIONNO";
                string paramter_1 = "DISTINCT (CAST(S.SECTIONNO AS VARCHAR)+' - '+ CAST(COALESCE(NULLIF(CT.UA_NO,''), CT.ADTEACHER) AS VARCHAR)+' - '+CAST(ISNULL(CT.INTAKE,0) AS VARCHAR)+' - '+ CAST((SELECT COUNT(*) FROM ACD_STUDENT_RESULT WHERE schemeno=CT.SCHEMENO and courseno=CT.COURSENO and sessionno=CT.SESSIONNO AND SECTIONNO=CT.SECTIONNO AND ISNULL(CANCEL,0)=0) AS VARCHAR)) as SECTIONNO";
                string parameter_2 = "'[ '+S.SECTIONNAME+' ] - '+U.UA_FULLNAME AS UA_FULLNAME,CT.SECTIONNO as SECNO";
                string whereCondition = "SESSIONNO = " + (Convert.ToInt32(ddlSession.SelectedValue)) + " AND CT.COURSENO=" + ccCode.Value + " AND ISNULL(CANCEL,0)=0 AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + "";
                string orderByCondition = "CT.SECTIONNO";

                objCommon.FillDropDownList(ddlNewsec, tableName, paramter_1, parameter_2, whereCondition, orderByCondition);
                ddlNewsec.SelectedValue = lblSection.ToolTip;

                Label Intake = (Label)e.Item.FindControl("lblIntake");
                Label Filled = (Label)e.Item.FindControl("lblFilled");

                Intake.Text = ddlNewsec.SelectedValue.ToString().Split('-')[2].Trim().ToString();
                Filled.Text = ddlNewsec.SelectedValue.ToString().Split('-')[3].Trim().ToString();


                if ((Convert.ToInt32(Intake.Text)) == (Convert.ToInt32(Filled.Text)))
                {
                    Intake.CssClass = "label label-default";
                    Filled.CssClass = "label label-default";

                    int CalcVal = 0;
                    for (int i = 0; i < ddlNewsec.Items.Count; i++)
                    {
                        if (Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[2].Trim().ToString()) == Convert.ToInt32(ddlNewsec.Items[i].Value.ToString().Split('-')[3].Trim().ToString()))
                        {
                            CalcVal++;
                        }
                    }

                    if (CalcVal == ddlNewsec.Items.Count)
                    {
                        CheckBox chkSelectCourse = (CheckBox)e.Item.FindControl("chkAccept");
                        chkSelectCourse.Enabled = false;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 80 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-danger";
                }
                else if ((Convert.ToInt32(Filled.Text)) >= (Convert.ToInt32(Intake.Text) * 50 / 100))
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-warning";
                }
                else
                {
                    Intake.CssClass = "label label-success";
                    Filled.CssClass = "label label-success";
                }

                CheckBox chkcurrent = (CheckBox)e.Item.FindControl("chkElectiveGrp15");
                if (chkcurrent.Checked)
                {
                    txtAllSubjects.Text = Convert.ToString(Convert.ToInt32(txtAllSubjects.Text) + 1);

                    txtCredits.Text = Convert.ToString(Convert.ToDecimal(txtCredits.Text) + Convert.ToDecimal(((Label)e.Item.FindControl("lblCredits")).Text));

                    ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;

                    if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5"))//faculty  
                    {
                        if (ViewState["ACCEPT"].ToString() != "0")//accepted
                        {
                            lvCurrentSubjects.Enabled = false;
                            lvElectiveCourse.Enabled = false;
                            lvElectiveCourseGrp2.Enabled = false;
                            lvElectiveCourseGrp3.Enabled = false;
                            lvElectiveCourseGrp4.Enabled = false;
                            lvElectiveCourseGrp5.Enabled = false;
                            lvElectiveCourseGrp6.Enabled = false;
                            lvElectiveCourseGrp7.Enabled = false;
                            lvElectiveCourseGrp8.Enabled = false;
                            lvElectiveCourseGrp9.Enabled = false;
                            lvElectiveCourseGrp10.Enabled = false;
                            lvElectiveCourseGrp11.Enabled = false;
                            lvElectiveCourseGrp12.Enabled = false;
                            lvElectiveCourseGrp13.Enabled = false;
                            lvElectiveCourseGrp14.Enabled = false;
                            lvElectiveCourseGrp15.Enabled = false;
                            lvDetained.Enabled = false;
                            lvMovableSubjects.Enabled = false;
                            btnSubmit.Enabled = false;
                        }
                        else
                        {
                            lvCurrentSubjects.Enabled = true;
                            lvElectiveCourse.Enabled = true;
                            lvElectiveCourseGrp2.Enabled = true;
                            lvElectiveCourseGrp3.Enabled = true;
                            lvElectiveCourseGrp4.Enabled = true;
                            lvElectiveCourseGrp5.Enabled = true;
                            lvElectiveCourseGrp6.Enabled = true;
                            lvElectiveCourseGrp7.Enabled = true;
                            lvElectiveCourseGrp8.Enabled = true;
                            lvElectiveCourseGrp9.Enabled = true;
                            lvElectiveCourseGrp10.Enabled = true;
                            lvElectiveCourseGrp11.Enabled = true;
                            lvElectiveCourseGrp12.Enabled = true;
                            lvElectiveCourseGrp13.Enabled = true;
                            lvElectiveCourseGrp14.Enabled = true;
                            lvElectiveCourseGrp15.Enabled = true;
                            lvDetained.Enabled = true;
                            lvMovableSubjects.Enabled = true;
                            btnSubmit.Enabled = true;
                        }
                    }
                    else
                    {
                        ddlNewsec.SelectedValue = (e.Item.FindControl("hdfSectionno") as HiddenField).Value;
                        lvCurrentSubjects.Enabled = false;
                        lvElectiveCourse.Enabled = false;
                        lvElectiveCourseGrp2.Enabled = false;
                        lvElectiveCourseGrp3.Enabled = false;
                        lvElectiveCourseGrp4.Enabled = false;
                        lvElectiveCourseGrp5.Enabled = false;
                        lvElectiveCourseGrp6.Enabled = false;
                        lvElectiveCourseGrp7.Enabled = false;
                        lvElectiveCourseGrp8.Enabled = false;
                        lvElectiveCourseGrp9.Enabled = false;
                        lvElectiveCourseGrp10.Enabled = false;
                        lvElectiveCourseGrp11.Enabled = false;
                        lvElectiveCourseGrp12.Enabled = false;
                        lvElectiveCourseGrp13.Enabled = false;
                        lvElectiveCourseGrp14.Enabled = false;
                        lvElectiveCourseGrp15.Enabled = false;
                        lvDetained.Enabled = false;
                        lvMovableSubjects.Enabled = false;
                        btnSubmit.Enabled = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration_SingleStudent --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void chkElectiveGrp15_CheckedChanged(object sender, EventArgs e)
    {
        int ElectGrp15Count = 0, maxEleAllowGrp15 = 0;
        DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SECTION SE ON (SE.SECTIONNO = s.SECTIONNO)", "S.IDNO,S.ROLLNO,DG.DEGREENAME,SECTIONNAME,s.SECTIONNO", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO as ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE ISNULL(S.PHYSICALLY_HANDICAPPED,0) WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH,IDTYPE,(CASE ISNULL(S.IDTYPE,0) WHEN '3' THEN 'DIRECT SECOND YEAR ' ELSE  'REGULAR'   END) AS STUD_TYPE", "S.ENROLLNO='" + txtRollNo.Text.Trim() + "'", string.Empty);

        int adm_type = 1;
        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()) == 3) //3 direct second year
        {
            adm_type = 2;
        }
        int student_type = 1;//1 for All Pass      

        DataSet dsCreditInfo15 = null;
        dsCreditInfo15 = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT", "ElectGrp15", "", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "AND STUDENT_TYPE =" + student_type + "AND DEGREENO =" + dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() + " AND ADM_TYPE=" + adm_type + "AND " + dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() + "IN (select VALUE FROM DBO.SPLIT( SEMESTER,','))", "");

        maxEleAllowGrp15 = (dsCreditInfo15.Tables[0].Rows[0]["ElectGrp15"].ToString() == string.Empty ? 1 : Convert.ToInt32(dsCreditInfo15.Tables[0].Rows[0]["ElectGrp15"].ToString()));

        CheckBox chk = sender as CheckBox;
        int totalsub = Convert.ToInt32(txtAllSubjects.Text);
        decimal totSubjectCredit = Convert.ToDecimal(txtCredits.Text);

        ListViewItem item = (ListViewItem)chk.NamingContainer;
        HiddenField hf = (HiddenField)item.FindControl("hdfCCode");
        HiddenField hfStatus = (HiddenField)item.FindControl("hfStatus");

        foreach (ListViewDataItem dataitem in lvElectiveCourseGrp15.Items)
        {
            CheckBox chk1 = dataitem.FindControl("chkElectiveGrp15") as CheckBox;
            if (chk1.Checked == true)
                ElectGrp15Count++;
        }

        DropDownList ddlSecTeacher = (DropDownList)item.FindControl("ddlsection");  //Added by Abhinay Lad [30-06-2019]
        if (chk.Checked)
        {
            decimal cred = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "ISNULL(CREDITS,0)", "COURSENO = " + hf.Value));

            totSubjectCredit = totSubjectCredit + Convert.ToDecimal(cred);
            totalsub = totalsub + 1;

            if (ElectGrp15Count > maxEleAllowGrp15)
            {
                totSubjectCredit = totSubjectCredit - Convert.ToDecimal(cred);
                totalsub = totalsub - 1;
                chk.Checked = false;

                txtAllSubjects.Text = CheckTotalSubjects().ToString();
                txtCredits.Text = CheckTotalCredits().ToString();

                objCommon.DisplayMessage(this.updDiv, "Maximum " + maxEleAllowGrp15 + " Elective Subject are allowed for registration from [Group-15]", this.Page);
                return;
            }
            else
            {
                ddlSecTeacher.Enabled = true;
            }
        }
        else if (chk.Checked == false)
        {
            ddlSecTeacher.Enabled = false;
        }
        txtAllSubjects.Text = CheckTotalSubjects().ToString();
        txtCredits.Text = CheckTotalCredits().ToString();
    }
   
}


