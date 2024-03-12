using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using SendGrid;
using System.Threading.Tasks;
using EASendMail;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

public partial class Projects_Program_Transfer : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Branch objBranch = new Branch();
    StudentController objSController = new StudentController();
    Student objStudent = new Student();
    BranchController objbranch = new BranchController();
    FeeCollectionController feeController = new FeeCollectionController();
    UserController user = new UserController();
    SecurityThreadsPayloads objValidateInput = new SecurityThreadsPayloads();
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
                    this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    if (Request.QueryString["RecType"] != null && Request.QueryString["RecType"].ToString() != null)
                        ViewState["ReceiptType"] = Request.QueryString["RecType"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }

                    //divlkprogram.Attributes.Add("class", "active");
                    filldropdown();

                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                }
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACDEMIC_COLLEGE_MASTER.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=Program_Transfer.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Program_Transfer.aspx");
        }
    }


    private void filldropdown()
    {
         //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "ISNULL(FLOCK,0)=1 AND SESSIONNO=(SELECT MAX(SESSIONNO) FROM ACD_SESSION_MASTER WHERE ISNULL(FLOCK,0)=1)", "SESSIONNO desc");
         objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "ISNULL(FLOCK,0)=1 ", "SESSIONNO desc");
        //objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "ISNULL(ACTIVE,0)=1", "COLLEGE_NAME");
        // objCommon.FillDropDownList(ddlintake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "", "BATCHNO DESC");
        // objCommon.FillDropDownList(ddlAwardingInstitute, "ACD_AFFILIATED_UNIVERSITY", "AFFILIATED_NO", "AFFILIATED_LONGNAME", "AFFILIATED_NO > 0", "AFFILIATED_NO");
        // objCommon.FillDropDownList(ddlcampus, "ACD_CAMPUS", "CAMPUSNO", "CAMPUSNAME", "ISNULL(ACTIVE,0)=1", "");
        //  objCommon.FillDropDownList(ddlweek, "ACD_BATCH_SLIIT", "WEEKNO", "WEEKDAYSNAME", "", "");

    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            ddlProgram.Items.Clear();
            ddlProgram.Items.Add(new ListItem("Please Select", "0"));
            // ddlProgram.Items.Add("Please Select");

            ddlFaculty.Items.Clear();
            ddlFaculty.Items.Add(new ListItem("Please Select", "0"));

            ddlAwardingInstitute.Items.Clear();
            ddlAwardingInstitute.Items.Add(new ListItem("Please Select", "0"));
            ddlSession.SelectedIndex = 0;
            ddlProgram.SelectedIndex = 0;
            ddlFaculty.SelectedIndex = 0;
            ddlAwardingInstitute.SelectedIndex = 0;
            paneldetails.Visible = false;
            newenroll.Visible = false;
            lvprogramlist.DataSource = null;
            lvprogramlist.DataBind();
            rdbChangeRegNo.SelectedValue = "1";

            if (objValidateInput.checkForSQLInjection(txtSearchCandidateProgram.Text.ToString()))
            {
                objCommon.DisplayMessage(updEdit, "Invalid Input!", this.Page);
                paneldetails.Visible = false;
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                return;
            }

                DataSet ds = null;

                if (rdselect.SelectedValue == "0")
                {
                    LinkButton lnkRegno = sender as LinkButton;
                    string regno = lnkRegno.CommandArgument;
                    ds = objbranch.Getprogramdetails(regno, 1);
                }
                else
                {
                    string str = txtSearchCandidateProgram.Text.ToString();
                        ds = objbranch.Getprogramdetails(txtSearchCandidateProgram.Text, 1);
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    bindlist();

                    paneldetails.Visible = true;
                    DataRow dr = ds.Tables[0].Rows[0];
                    lblFaculty.Text = dr["COLLEGE_NAME"].ToString() == string.Empty ? string.Empty : dr["COLLEGE_NAME"].ToString();
                    lblstudentid.Text = dr["REGNO"].ToString() == string.Empty ? string.Empty : dr["REGNO"].ToString();
                    ViewState["oldenroll"] = dr["OLENROLL"].ToString() == string.Empty ? string.Empty : dr["OLENROLL"].ToString();
                    lblstudname.Text = dr["NAME"].ToString() == string.Empty ? string.Empty : dr["NAME"].ToString();
                    lblemailid.Text = dr["EMAILID"].ToString() == string.Empty ? string.Empty : dr["EMAILID"].ToString();
                    lblAwardingInstitute.Text = dr["AFFILIATED_SHORTNAME"].ToString() == string.Empty ? string.Empty : dr["AFFILIATED_SHORTNAME"].ToString();
                    lblProgram.Text = dr["APPLY_PROGRAM_DETAILS"].ToString() == string.Empty ? string.Empty : dr["APPLY_PROGRAM_DETAILS"].ToString();
                    ViewState["OldProgram"] = dr["APPLY_PROGRAM_DETAILS"].ToString() == string.Empty ? string.Empty : dr["APPLY_PROGRAM_DETAILS"].ToString();

                    lblstdid.Text = dr["STUDID"].ToString() == string.Empty ? string.Empty : dr["STUDID"].ToString();

                    lblCampus.Text = dr["CAMPUSNAME"].ToString() == string.Empty ? string.Empty : dr["CAMPUSNAME"].ToString();
                    ViewState["REGNO"] = dr["REGNO"].ToString() == string.Empty ? string.Empty : dr["REGNO"].ToString();
                    ViewState["BRANCHNO"] = dr["BRANCHNO"].ToString() == string.Empty ? string.Empty : dr["BRANCHNO"].ToString();
                    ViewState["DEGREENO"] = dr["DEGREENO"].ToString() == string.Empty ? string.Empty : dr["DEGREENO"].ToString();
                    ViewState["NAME"] = dr["NAME"].ToString() == string.Empty ? string.Empty : dr["NAME"].ToString();
                    ViewState["IDNO"] = dr["IDNO"].ToString() == string.Empty ? string.Empty : dr["IDNO"].ToString();
                    ViewState["COLLEGE_ID"] = dr["COLLEGE_ID"].ToString() == string.Empty ? string.Empty : dr["COLLEGE_ID"].ToString();
                    ViewState["SEMESTERNO"] = dr["SEMESTERNO"].ToString() == string.Empty ? string.Empty : dr["SEMESTERNO"].ToString();
                    ViewState["USERNO"] = dr["USERNO"].ToString() == string.Empty ? string.Empty : dr["USERNO"].ToString();
                    ViewState["ADMBATCH"] = dr["ADMBATCH"].ToString() == string.Empty ? string.Empty : dr["ADMBATCH"].ToString();
                    ViewState["PTYPE"] = dr["PTYPE"].ToString() == string.Empty ? string.Empty : dr["PTYPE"].ToString();
                    ViewState["emailid"] = dr["EMAILID"].ToString() == string.Empty ? string.Empty : dr["EMAILID"].ToString();
                    ViewState["CAMPUSNO"] = dr["CAMPUSNO"].ToString() == string.Empty ? string.Empty : dr["CAMPUSNO"].ToString();
                    // ViewState["WEEKSNOS"] = dr["WEEKSNOS"].ToString() == string.Empty ? string.Empty : dr["WEEKSNOS"].ToString();
                    //  PreviousReciept(Convert.ToInt32(ViewState["IDNO"]));
                    // this.DisplayInformation(Convert.ToInt32(ViewState["IDNO"]));
                    string str = "$(\"#myModal2\").modal(\"hide\");";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "HideModal", "" + str + "", true);
                    Panel5.Visible = false;
                }

                else
                {
                    objCommon.DisplayUserMessage(this.Page, "Student not admitted yet!.", this.Page);
                    paneldetails.Visible = false;
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();

                }
           
            
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsUserContact = null;
            string message = "";
            string subject = "";
            dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));
            FeeDemand feeDemand = new FeeDemand();

         //   int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + txtSearchCandidateProgram.Text + "'" + "OR ENROLLNO='" + txtSearchCandidateProgram.Text + "'")); 
            //lblstudentid 
            string studid = lblstudentid.Text.Trim();
            int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + studid + "'" + "OR ENROLLNO='" + studid + "'")); 

            objStudent.IdNo = Convert.ToInt32(IDNO);
            objStudent.RollNo = Convert.ToString(ViewState["REGNO"]);
            int oldbranch = Convert.ToInt32(ViewState["BRANCHNO"]); // OLD BRANCH NO
            int olddegree = Convert.ToInt32(ViewState["DEGREENO"]);
            int oldcollege_id = Convert.ToInt32(ViewState["COLLEGE_ID"]);
            int oldintake = Convert.ToInt32(ViewState["ADMBATCH"]);
            objStudent.StudName = Convert.ToString(ViewState["NAME"]);
            int newScheme = Convert.ToInt32(ddlScheme.SelectedValue);
            string[] program;
            if (ddlProgram.SelectedValue == "0")
            {
                program = "0,0".Split(',');
            }
            else
            {
                program = ddlProgram.SelectedValue.Split(',');
            }
            feeDemand.DegreeNo = Convert.ToInt32(program[0]);//new
            feeDemand.BranchNo = Convert.ToInt32(program[1]);//new  
            objStudent.CollegeCode = Session["colcode"].ToString();
            objStudent.RegNo = txtLocation.Text.Trim();
            //int newintake = Convert.ToInt32(ddlintake.SelectedValue);
            int userno = Convert.ToInt32(Session["userno"]);
            int institute = Convert.ToInt32(ddlAwardingInstitute.SelectedValue);
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            string IP = Convert.ToString(ViewState["ipAddress"]);


            feeDemand.StudentId = Convert.ToInt32(IDNO);
            feeDemand.StudentName = Convert.ToString(ViewState["NAME"]);
            feeDemand.EnrollmentNo = Convert.ToString(ViewState["newenroll"]);
            //int examType = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "FLOCK=1"));
            //if (GetViewStateItem("ReceiptType") == "HF")
            //{
            //    feeDemand.SessionNo = Convert.ToInt32(ViewState["HOSTEL_SESSIONNO"]);
            //}
            //else
            //{
            //    feeDemand.SessionNo = Convert.ToInt32(Session["currentsession"].ToString());

            // }

            feeDemand.College_ID = Convert.ToInt32(ddlFaculty.SelectedValue);
            feeDemand.Interest = 1;
            feeDemand.SemesterNo = ((Convert.ToInt32(ViewState["SEMESTERNO"]) > 0 && Convert.ToString(ViewState["SEMESTERNO"]) != string.Empty) ? Convert.ToInt32(ViewState["SEMESTERNO"]) : 1);
            // feeDemand.AdmBatchNo = Convert.ToInt32(ddlintake.SelectedValue);
            // feeDemand.ReceiptTypeCode = "TF";
            // feeDemand.PaymentTypeNo = Convert.ToInt32(ViewState["PTYPE"]);
            //feeDemand.CounterNo = 1;
            feeDemand.UserNo = ((Session["userno"].ToString() != string.Empty) ? Convert.ToInt32(Session["userno"].ToString()) : 0);
            feeDemand.CollegeCode = ((Session["colcode"].ToString() != string.Empty) ? Session["colcode"].ToString() : string.Empty);
            // int paymentTypeNoOld = 1;
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);

            //CustomStatus cs = (CustomStatus)objbranch.AddChagneBranch(objStudent, feeDemand, olddegree, oldbranch, userno, institute, IP, oldcollege_id, Convert.ToString(ViewState["emailid"]), txtEmail.Text, oldintake, newintake, Convert.ToInt32(ViewState["CAMPUSNO"]), Convert.ToInt32(ddlcampus.SelectedValue), Convert.ToInt32(ViewState["WEEKSNOS"]), Convert.ToInt32(ddlweek.SelectedValue), Convert.ToString(ViewState["oldenroll"]));

            //method to change branch
            CustomStatus cs = (CustomStatus)objbranch.AddChagneBranch(objStudent, feeDemand, olddegree, oldbranch, userno, institute, IP, oldcollege_id, Convert.ToInt32(ViewState["CAMPUSNO"]), Convert.ToInt32(1), Convert.ToString(ViewState["oldenroll"]), sessionno,newScheme,Convert.ToInt32(rdbChangeRegNo.SelectedItem.Value));

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                Session["Offer_Accept"] = "1";
                string lblprogram = lblDyProgram.Text.ToString() == string.Empty ? "Program" : lblDyProgram.Text.ToString();
                objCommon.DisplayMessage(this.Page, lblprogram.ToString() + " changed successfully.", this.Page);
                bindlist();
                //subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                //message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                paneldetails.Visible = false;
                txtSearchCandidateProgram.Text = "";

                string siteUrl = objCommon.LookUp("TBLCONFIGORGANIZATIONMASTER", "ERP_URL", "");

                EmailSmsWhatssppSend(Convert.ToInt32(Request.QueryString["pageno"].ToString()), Convert.ToString(ViewState["emailid"]), Convert.ToString(ViewState["NAME"]), Convert.ToString(ViewState["OldProgram"]), Convert.ToString(ddlProgram.SelectedItem.Text), ddlFaculty.SelectedItem.Text.ToString(), objStudent.RegNo.ToString(), siteUrl);
                clear();
                btnCancelModal_Click(sender, e);
               // Execute(message, Convert.ToString(ViewState["emailid"]), subject, Convert.ToString(ViewState["NAME"]), Convert.ToString(ViewState["newenroll"]), Convert.ToString(ViewState["newprogram"]), Convert.ToString(ViewState["newbatch"]), Convert.ToString(txtEmail.Text), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
              //  Execute(message,Convert.ToString(ViewState["emailid"]),subject,Convert.ToString(ViewState["NAME"]),Convert.ToString(ViewState["newenroll"]),Convert.ToString(ViewState["newprogram"]),
                


            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Record Already Exist", this.Page);
            }

        }
        catch (Exception ex)
        {
        }
    }

    protected void EmailSmsWhatssppSend(int Page_No, string toSendAddre, string Name, string oldProgram,string NewProgram,string NewCollege,string Regno, string URL)
    {
        EmailSmsWhatsaap Email = new EmailSmsWhatsaap(); 
        DataSet ds = objCommon.FillDropDown("ACCESS_LINK", "isnull(EMAIL,0) EMAIL,isnull(SMS,0) as SMS,isnull(WHATSAAP,0) as WHATSAAP", "", "AL_NO=" + Convert.ToInt32(Request.QueryString["pageno"].ToString()), "");
        string Res = ""; string Message = "";
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["EMAIL"].ToString()) == 1)//For Email Send 
        {
            int PageNo = Page_No;
            //int PageNo = 33;
            UserController objUC = new UserController();
            DataSet dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", "0", PageNo);
            if (dsUserContact.Tables[1] != null && dsUserContact.Tables[1].Rows.Count > 0)
            {
                string Subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                Message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                string toSendAddress = toSendAddre.ToString();// 
                MailMessage msgsPara = new MailMessage();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Message);
                msgsPara.BodyEncoding = Encoding.UTF8;
                msgsPara.Body = Convert.ToString(sb);
                msgsPara.Body = msgsPara.Body.Replace("[UA_FULLNAME]", Name.ToString());
                msgsPara.Body = msgsPara.Body.Replace("[Old_Program]", oldProgram.ToString());
                msgsPara.Body = msgsPara.Body.Replace("[New_Program]", NewProgram.ToString());
                msgsPara.Body = msgsPara.Body.Replace("[New_College_Name]", NewCollege.ToString());
                msgsPara.Body = msgsPara.Body.Replace("[New_Program_Name]", NewProgram.ToString());
                msgsPara.Body = msgsPara.Body.Replace("[Reg_no]", Regno.ToString());
                msgsPara.Body = msgsPara.Body.Replace("[URL_Name]", URL.ToString());
             
                Task<string> task = Email.Execute(msgsPara.Body, toSendAddress, Subject, null, string.Empty.ToString());
                Res = task.Result;
           
            }
        }
      
    }

    static async Task Execute(string message, string toSendAddress, string Subject, string firstname, string username, string newprogram, string sendemail, string emailpass)
    {
        try
        {
            SmtpMail oMail = new SmtpMail("TryIt");
            oMail.From = sendemail;
            oMail.To = toSendAddress;
            oMail.Subject = Subject;
            oMail.HtmlBody = message;
            oMail.HtmlBody = oMail.HtmlBody.Replace("[USERFIRSTNAME]", firstname.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[USERNAME]", username.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[NewProgram]", newprogram.ToString());
            //oMail.HtmlBody = oMail.HtmlBody.Replace("[NewIntake]", intake.ToString());
            //oMail.HtmlBody = oMail.HtmlBody.Replace("[NewEmailid]", newemailid.ToString());
            SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022
            oServer.User = sendemail;
            oServer.Password = emailpass;
            oServer.Port = 587;

            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();

            oSmtp.SendMail(oServer, oMail);


        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //private void CreateDemand(FeeDemand feeDemand, int paymentTypeNoOld)
    //{
    //    try
    //    {

    //        if (feeController.CreateNewDemand(feeDemand, paymentTypeNoOld, Convert.ToInt32(ddlFaculty.SelectedValue), Convert.ToInt32(ddlcampus.SelectedValue), Convert.ToInt32(ddlweek.SelectedValue), Convert.ToInt32(ddlintake.SelectedValue), Convert.ToInt32(Session["userno"])))
    //        {
    //            this.PopulateFeeItemsSection(feeDemand.SemesterNo);
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(this.Page, "Standard fee is not defined.", this.Page);
    //            return;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_FeeCollection.btnCreateDemand_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    ////}
    //private void PopulateFeeItemsSection(int semesterNo)
    //{
    //    try
    //    {
    //        int status = 0;
    //        int SessionNo = 0;

    //        int studId = Convert.ToInt32((ViewState["IDNO"] != string.Empty) ? ViewState["IDNO"] : "0");

    //        if (GetViewStateItem("ReceiptType") == "HF" || GetViewStateItem("ReceiptType") == "hf")
    //        {
    //            SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_HOSTEL_SESSION", "MAX(HOSTEL_SESSION_NO)", "FLOCK=1"));
    //        }
    //        else
    //        {
    //            SessionNo = Convert.ToInt32(Session["currentsession"]);
    //        }

    //        DataSet ds = null;
    //        ds = feeController.GetFeeItems_Data(SessionNo, studId, semesterNo, Convert.ToString(ViewState["ReceiptType"]), 0, 0, Convert.ToInt16(ViewState["PaymentTypeNo"]), ref status, 0, Convert.ToInt32(ViewState["ADMBATCH"]));
    //        if (status != -99 && ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //            //string CollegeId = objCommon.LookUp("ACD_DEMAND D INNER JOIN ACD_STUDENT S ON(D.IDNO=S.IDNO)", "ISNULL(COLLEGE_ID,0)COLLEGE_ID", "D.IDNO=" + Convert.ToInt32(ViewState["StudentId"]) + " AND RECIEPT_CODE='TF' AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + " AND D.SEMESTERNO=" + Convert.ToInt32(ViewState["SemesterNo"]));

    //            //if (CollegeId == "")
    //            //{
    //            //    CollegeId = "0";
    //            //}

    //            ViewState["COLLEGE_ID"] = 1;

    //            /// Bind fee items list view with the data source found.
    //            lvFeeItems.DataSource = ds;
    //            lvFeeItems.DataBind();
    //            string RecieptCode = ds.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
    //            if (RecieptCode != "" || RecieptCode != string.Empty) //if(RecieptCode == "TF" || RecieptCode == "TPF" || RecieptCode == "HF" || RecieptCode == "MF" || RecieptCode != "")
    //            {
    //                /// Show remark for current fee demand
    //                txtRemark.Text = ds.Tables[0].Rows[0]["PARTICULAR"].ToString();
    //                txtFeeBalance.Text = ds.Tables[0].Rows[0]["EXCESS_AMT"].ToString();

    //                /// Set FeeCatNo from datasource
    //                ViewState["FeeCatNo"] = ds.Tables[0].Rows[0]["FEE_CAT_NO"].ToString();

    //                /// Show total fee amount to be paid by the student in total amount textbox.
    //                /// This total fee amount can be changed by user according to the student's current 
    //                /// payment amount (i.e. student can do part payment of Fee also).
    //                //txtTotalAmount.Text = this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString();
    //                //lblamtpaid.Text = this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString();
    //                //if (RecieptCode == "CF")
    //                //{
    //                //    txtTotalCashAmt.Text = this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString();
    //                //}

    //                //if (ViewState["CSABAMT"].ToString() != "0" && RecieptCode == "TF" && lblSemester.Text == "01") //
    //                //{
    //                //    txtTotalAmount.Text = (Convert.ToInt32(txtTotalAmount.Text.ToString()) - Convert.ToInt32(ViewState["CSABAMT"].ToString())).ToString();
    //                //    lblamtpaid.Text = txtTotalAmount.Text;
    //                //}

    //                //txtTotalFeeAmount.Text = txtTotalAmount.Text;
    //                //hdnMaxamount.Value = txtTotalAmount.Text;

    //            }
    //            /// If fee items are available then Enable
    //            /// submit button and show the div FeeItems.

    //            divFeeItems.Visible = true;

    //        }
    //        else
    //        {

    //            if (GetViewStateItem("ReceiptType") == "MF")
    //            {
    //                this.CreateDemandForCurrentFeeCriteria();

    //            }
    //            else
    //            {
    //                /// As no demand record found, ask user if he want to create one.
    //                this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'>";
    //                this.divMsg.InnerHtml += " if(confirm('No demand found for semester " + ViewState["SEMESTERNO"] + ".\\nDo you want to create demand for this semester?'))";
    //                this.divMsg.InnerHtml += "{__doPostBack('CreateDemand', '');}</script>";

    //                /// If fee items are not available then disable
    //                /// submit button and hide divFeeItems.
    //                /// 
    //                divFeeItems.Visible = false;

    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_FeeCollection.PopulateFeeItemsSection() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    //private void CreateDemandForCurrentFeeCriteria()
    //{
    //    try
    //    {

    //        FeeDemand feeDemand = new FeeDemand();
    //        feeDemand.StudentId = (Convert.ToInt32(ViewState["idno"]));
    //        feeDemand.StudentName = Convert.ToString(ViewState["NAME"]);
    //        feeDemand.EnrollmentNo = Convert.ToString(ViewState["newenroll"]);
    //        int examType = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "FLOCK=1"));

    //        /* ADDED BY MOHD. FARAZ 19/05/20212 */
    //        if (GetViewStateItem("ReceiptType") == "HF" || GetViewStateItem("ReceiptType") == "hf")
    //        {
    //            feeDemand.SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_HOSTEL_SESSION", "MAX(HOSTEL_SESSION_NO)", "FLOCK=1"));
    //        }
    //        else
    //        {
    //            if (examType == 1)
    //            {
    //                //feeDemand.SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER","","");
    //                feeDemand.SessionNo = Convert.ToInt32(Session["currentsession"]);
    //            }
    //            else
    //            {
    //                //feeDemand.SessionNo = Convert.ToInt32(objCommon.LookUp("","","");
    //                feeDemand.SessionNo = Convert.ToInt32(Session["currentsession"]) + 1;
    //            }
    //        }
    //        string[] program;
    //        if (ddlProgram.SelectedValue == "0")
    //        {
    //            program = "0,0".Split(',');
    //        }
    //        else
    //        {
    //            program = ddlProgram.SelectedValue.Split(',');
    //        }
    //        int newdegree = Convert.ToInt32(program[0]);
    //        int newbranch = Convert.ToInt32(program[1]);
    //        feeDemand.BranchNo = (Convert.ToInt32(newbranch));
    //        feeDemand.DegreeNo = (Convert.ToInt32(newdegree));
    //        feeDemand.College_ID = Convert.ToInt32(ddlFaculty.SelectedValue);
    //        feeDemand.SemesterNo = (Convert.ToInt32(ViewState["SEMESTERNO"]));
    //        feeDemand.AdmBatchNo = Convert.ToInt32(ddlintake.SelectedValue);
    //        feeDemand.ReceiptTypeCode = GetViewStateItem("ReceiptType");
    //        feeDemand.PaymentTypeNo = ((ViewState["PaymentTypeNo"] != string.Empty) ? Convert.ToInt32(ViewState["PaymentTypeNo"]) : 0);
    //        feeDemand.CounterNo = ((GetViewStateItem("CounterNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("CounterNo")) : 0);
    //        feeDemand.UserNo = ((Session["userno"].ToString() != string.Empty) ? Convert.ToInt32(Session["userno"].ToString()) : 0);
    //        feeDemand.CollegeCode = ((Session["colcode"].ToString() != string.Empty) ? Session["colcode"].ToString() : string.Empty);
    //        int paymentTypeNoOld = ((ViewState["PaymentTypeNo"] != string.Empty) ? Convert.ToInt32(ViewState["PaymentTypeNo"]) : 0);

    //        //this.CreateDemand(feeDemand, paymentTypeNoOld);

    //        if (feeController.CreateNewDemand(feeDemand, paymentTypeNoOld, Convert.ToInt32(ddlFaculty.SelectedValue), Convert.ToInt32(ddlcampus.SelectedValue), Convert.ToInt32(ddlweek.SelectedValue), Convert.ToInt32(ddlintake.SelectedValue), Convert.ToInt32(Session["userno"])))
    //        {
    //            this.PopulateFeeItemsSection(feeDemand.SemesterNo);
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(this.Page, "Standard fee is not defined.", this.Page);
    //            return;
    //        }
    //    }
    //    catch (Exception ex)btnsear
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_FeeCollection.CreateDemandForCurrentFeeCriteria() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    //public void PreviousReciept(int studentId)
    //{

    //    DataSet ds = feeController.GetPaidReceiptsInfoByStudId(studentId);
    //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    {
    //        // Bind list view with paid receipt data 
    //        //divPreviousReceipts.Visible = true;
    //        lvPaidReceipts.DataSource = ds;
    //        lvPaidReceipts.DataBind();
    //    }
    //    else//MAKE CHANGE HERE
    //    {
    //        divHidPreviousReceipts.InnerHtml = "No Previous Receipt Found.<br/>";
    //    }
    //    //divPreviousReceipts.Visible = true;
    //}
    //private void DisplayInformation(int studentId)
    //{
    //    try
    //    {

    //        DataSet ds = feeController.GetStudentInfoById(studentId);
    //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //            DataRow dr = ds.Tables[0].Rows[0];
    //            this.PopulateStudentInfoSection(dr);

    //        }
    //        ds = feeController.GetPaidReceiptsInfoByStudId(studentId);
    //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {

    //            ViewState["dcr"] = ds.Tables[0].Rows[0]["DCR_NO"].ToString();
    //            ViewState["dcr"] = ds.Tables[0].Rows[0]["DCR_NO"].ToString();
    //            lvPaidReceipts.DataSource = ds;
    //            lvPaidReceipts.DataBind();
    //        }
    //        else
    //        {
    //            divHidPreviousReceipts.InnerHtml = "No Previous Receipt Found.<br/>";
    //        }
    //        //divPreviousReceipts.Visible = true;                     
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_FeeCollection.DisplayStudentInfo() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    private void PopulateStudentInfoSection(DataRow dr)
    {
        try
        {

            ViewState["StudentId"] = dr["IDNO"].ToString();
            ViewState["DegreeNo"] = dr["DEGREENO"].ToString();
            ViewState["BranchNo"] = dr["BRANCHNO"].ToString();
            ViewState["YearNo"] = dr["YEAR"].ToString();
            ViewState["SemesterNo"] = dr["SEMESTERNO"].ToString();
            ViewState["AdmBatchNo"] = dr["ADMBATCH"].ToString();
            ViewState["PaymentTypeNo"] = dr["PTYPE"].ToString();
            ViewState["CSABAMT"] = dr["CSAB_AMT"].ToString();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.PopulateStudentInfoSection() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private string GetViewStateItem(string itemName)
    {
        if (ViewState.Count > 0 &&
            ViewState[itemName] != null &&
            ViewState[itemName].ToString() != null &&
            ViewState[itemName].ToString().Trim() != string.Empty)
            return ViewState[itemName].ToString();
        else
            return string.Empty;
    }

    private void bindlist()
    {
        try
        {
            DataSet ds = null;
            ds = objbranch.GetprogramChange(txtSearchCandidateProgram.Text);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // newdemand.Visible = true;
                lnkPrintReport.Visible = true;
                // lblnewdemand.Text = ds.Tables[0].Rows[0]["TOTAL_AMT"].ToString();
                ViewState["newprogram"] = ds.Tables[0].Rows[0]["NEWPROGRAM"].ToString();
                ViewState["newbatch"] = ds.Tables[0].Rows[0]["NEWBATCH"].ToString();
                Panel1.Visible = true;
                lvprogramlist.DataSource = ds;
                lvprogramlist.DataBind();
            }
            else
            {
                Panel1.Visible = false;
                lnkPrintReport.Visible = false;
                lvprogramlist.DataSource = null;
                lvprogramlist.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void clear()
    {
        ddlProgram.Items.Clear();
        ddlProgram.Items.Add(new ListItem("Please Select", "0"));
       // ddlProgram.Items.Add("Please Select");

        ddlFaculty.Items.Clear();
        ddlFaculty.Items.Add(new ListItem("Please Select", "0"));

        ddlScheme.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));
       
        ddlAwardingInstitute.Items.Clear();
        ddlAwardingInstitute.Items.Add(new ListItem("Please Select", "0"));
        ddlSession.SelectedIndex = 0;
        ddlProgram.SelectedIndex = 0;
        ddlFaculty.SelectedIndex = 0;
        ddlAwardingInstitute.SelectedIndex = 0;
        paneldetails.Visible = false;
        newenroll.Visible = false;
        txtSearchCandidateProgram.Text = "";
        lvprogramlist.DataSource = null;
        lvprogramlist.DataBind();
        


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
       // Response.Redirect("Program_Transfer.aspx");
        rdbChangeRegNo.SelectedValue = "1";

        clear();
        btnCancelModal_Click(sender, e);
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
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] program;
        if (ddlProgram.SelectedIndex > 0)
        {
            program = ddlProgram.SelectedValue.Split(',');
            int newdegree = Convert.ToInt32(program[0]);
            int NewBranchNo = Convert.ToInt32(program[1]);
            int college_id = Convert.ToInt32(ddlFaculty.SelectedValue);
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO=" + NewBranchNo + " AND DEGREENO=" + newdegree + " AND COLLEGE_ID="+college_id, "SCHEMENAME");
            ddlScheme.Focus(); 
            DataSet ds = null;


            //newdegree, NewBranchNo, college_id,
            if (Convert.ToInt32(rdbChangeRegNo.SelectedItem.Value) == 2)
            {
                DataSet ds1 = objCommon.FillDropDown("ACD_STUDENT", "REGNO,REGNO1,ENROLLNO", "", "ISNULL(CAN,0)=0 AND IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString()), "");
                //if (Convert.ToInt32(ds1.Tables[0].Rows[0]["EMAIL"].ToString()) == 1)
                //{
                //}
                newenroll.Visible = false;
                txtLocation.Text = ds1.Tables[0].Rows[0]["ENROLLNO"].ToString();
                ViewState["newenroll"] = ds1.Tables[0].Rows[0]["ENROLLNO"].ToString();
            }
            else
            {

                ds = objbranch.getnewenoll(Convert.ToInt32(ViewState["IDNO"]), newdegree, NewBranchNo, college_id);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    newenroll.Visible = true;
                    txtLocation.Text = ds.Tables[0].Rows[0]["NEWENROLL"].ToString();
                    ViewState["newenroll"] = ds.Tables[0].Rows[0]["NEWENROLL"].ToString();
                    //ViewState["newemail"] = ds.Tables[1].Rows[0]["NEWEMAILID"].ToString();
                }
            }
            objCommon.FillDropDownList(ddlAwardingInstitute, "ACD_COLLEGE_DEGREE_BRANCH  CDB INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON (CDB.AFFILIATED_NO=AU.AFFILIATED_NO)", "DISTINCT AU.AFFILIATED_NO", "AU.AFFILIATED_LONGNAME", "AU.AFFILIATED_NO > 0 AND CDB.DEGREENO=" + newdegree + " AND CDB.BRANCHNO=" + NewBranchNo + " AND CDB.COLLEGE_ID=" + ddlFaculty.SelectedValue, "AU.AFFILIATED_NO");
                   
        }
        else
        {
            program = "0,0".Split(',');
            newenroll.Visible = false;
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlAwardingInstitute.Items.Clear();
            ddlAwardingInstitute.Items.Add(new ListItem("Please Select", "0"));
        }


    }

    protected void ddlAwardingInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlAwardingInstitute.SelectedIndex>0)
        btnTransfer.Focus();
        //objCommon.FillDropDownList(ddlProgram, "ACD_COLLEGE_DEGREE_BRANCH S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=S.BRANCHNO)", "DISTINCT CONCAT(D.DEGREENO, ', ',B.BRANCHNO)AS ID", "(D.DEGREENAME+'-'+B.LONGNAME)AS DEGBRANCH", "S.DEGREENO>0 AND B.AFFILIATED_NO=" + ddlAwardingInstitute.SelectedValue + "AND S.COLLEGE_ID=" + ddlFaculty.SelectedValue, "ID");
        //objCommon.FillDropDownList(ddlProgram, "ACD_COLLEGE_DEGREE_BRANCH S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=S.BRANCHNO)", "DISTINCT CONCAT(D.DEGREENO, ', ',B.BRANCHNO)AS ID", "(D.DEGREENAME+'-'+B.LONGNAME)AS DEGBRANCH", "ISNULL(ACTIVE,0)=1 AND  S.DEGREENO>0 AND B.AFFILIATED_NO=" + ddlAwardingInstitute.SelectedValue + "AND S.COLLEGE_ID=" + ddlFaculty.SelectedValue, "ID");
    }
    protected void lnkPrintReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowConfirmationReport("Final_Confirmation_Report", "AdmisionConfirmSlip.rpt", Convert.ToInt32(ViewState["IDNO"]));
        }
        catch (Exception ex)
        {

        }
    }
    private void ShowConfirmationReport(string reportTitle, string rptFileName, int idno)
    {
        try
        {
            //string colgname = "SLIIT";

            //string exporttype = "pdf";
            ////string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            ////url += "Reports/CommonReport.aspx?";
            //string url = "htt ps://sims.sliit.lk/Reports/CommonReport.aspx?";

            //url += "exporttype=" + exporttype;

            //url += "&filename=" + colgname + "-" + reportTitle + "." + exporttype;

            //url += "&path=~,Reports,Academic," + rptFileName;

            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno;

            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_REPORTS_gradeCard .ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            // objCommon.FillDropDownList(ddlFaculty, "ACD_SESSION_MASTER SM INNER JOIN ACD_COLLEGE_MASTER CM ON (SM.COLLEGE_ID=CM.COLLEGE_ID)", "DISTINCT CM.COLLEGE_ID", "CM.COLLEGE_NAME", "ISNULL(CM.ACTIVE,0)=1 AND SM.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "CM.COLLEGE_ID");
            //objCommon.FillDropDownList(ddlFaculty, "ACD_SESSION_MAPPING SM INNER JOIN ACD_COLLEGE_MASTER CM ON(SM.COLLEGE_ID=CM.COLLEGE_ID)", "DISTINCT CM.COLLEGE_ID", "CM.COLLEGE_NAME", "ISNULL(SM.STATUS,0)=1 AND ISNULL(CM.ACTIVE,0)=1 AND SM.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "CM.COLLEGE_ID");

            objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER CM ", "DISTINCT CM.COLLEGE_ID", "CM.COLLEGE_NAME", " ISNULL(CM.ACTIVE,0)=1", "CM.COLLEGE_ID");
            ddlFaculty.Focus();
        }
        else {
            ddlFaculty.Items.Clear();
            ddlFaculty.Items.Add(new ListItem("Please Select", "0"));
            ddlProgram.Items.Clear();
            ddlProgram.Items.Add(new ListItem("Please Select", "0"));
            ddlAwardingInstitute.Items.Clear();
            ddlAwardingInstitute.Items.Add(new ListItem("Please Select", "0"));
            newenroll.Visible = false;
            txtLocation.Text = string.Empty;
        }
        ddlFaculty.SelectedIndex = 0;
        ddlProgram.SelectedIndex = 0;
        ddlAwardingInstitute.SelectedIndex = 0;
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFaculty.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlProgram, "ACD_COLLEGE_DEGREE_BRANCH S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=S.BRANCHNO)", "DISTINCT CONCAT(D.DEGREENO, ', ',B.BRANCHNO)AS ID", "(D.DEGREENAME+'-'+B.LONGNAME)AS DEGBRANCH", "ISNULL(ACTIVE,0)=1 AND  S.DEGREENO>0 AND S.COLLEGE_ID=" + ddlFaculty.SelectedValue, "ID");
            ddlProgram.Focus();
        }
        else {
            ddlProgram.Items.Clear();
            ddlProgram.Items.Add(new ListItem("Please Select","0"));
            
            ddlAwardingInstitute.Items.Clear();
            ddlAwardingInstitute.Items.Clear();
            ddlAwardingInstitute.Items.Add(new ListItem("Please Select", "0"));
            txtLocation.Text = string.Empty;
            newenroll.Visible = false;
        }
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        ddlProgram.SelectedIndex = 0;
        ddlAwardingInstitute.SelectedIndex = 0;
    }
    protected void rdselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchCandidateProgram.Text = string.Empty;
        if (rdselect.SelectedValue == "0")
        {
            btnSearchName.Visible = true;
            btnsearch.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
        else{
            btnSearchName.Visible = false;
            btnsearch.Visible = true;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
    }
    protected void btnCancelModal_Click(object sender, EventArgs e)
    {
        txtSearchCandidateProgram.Text = "";
        lblNoRecords.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
    
    }
    protected void btnSearchName_Click(object sender, EventArgs e)
    {
       // lvStudent
        DataSet ds = null;

                ds = objbranch.Getprogramdetails(txtSearchCandidateProgram.Text,0);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Panel5.Visible = true;
                    lvStudent.DataSource = ds;
                    lvStudent.DataBind();
                }
                else
                {
                    paneldetails.Visible = false;
                    Panel5.Visible = false;
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    objCommon.DisplayUserMessage(this.Page, "Record Not Found.", this.Page);

                }
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedIndex > 0)
        {
            ddlAwardingInstitute.Focus();
        }
    }

    protected void lnkCertiAdmis_Click(object sender, EventArgs e)
    {
        ShowReportAdm("Certificate Admission", "Certi_Admission.rpt");

    }
    private void ShowReportAdm(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            //url += "Reports/CommonReport.aspx?";
            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(ViewState["IDNO"]);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentReconsilReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void rdbChangeRegNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlProgram.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlAwardingInstitute.SelectedIndex = 0;
        newenroll.Visible = false;
        //if (rdbChangeRegNo.SelectedValue == "1")
        //{
        //}
    }
}