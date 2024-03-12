//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Modify Student Type Details(Change Hostel and Transport)
// CREATION DATE : 20-JUL-2019
// CREATED BY    : Neha Baranwal
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public partial class Academic_ModifyStudType : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController ObjNuc = new FeeCollectionController();
    //StudentFees objStudentFees = new StudentFees();


    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
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
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    Page.Title = Session["coll_name"].ToString();
                    ViewState["ipAddress"] = GetUserIPAddress();

                    //this.objCommon.FillDropDownList(ddlpaytype, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "", "");
                    //this.objCommon.FillDropDownList(ddlroute, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "BELONGS_TO = 'T'", "RECIEPT_CODE");
                    //this.objCommon.FillDropDownList(ddlhostel, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "BELONGS_TO = 'H'", "RECIEPT_CODE");
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_CreateDemand.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    // IPADRESS  DETAILS ..
    private string GetUserIPAddress()
    {
        string User_IPAddress = string.Empty;
        string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
        {
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];

        }
        else////with Proxy detection
        {
            string[] splitter = { "," };
            string[] IP_Array = User_IPAddressRange.Split(splitter, System.StringSplitOptions.None);

            int LatestItem = IP_Array.Length - 1;

        }
        return User_IPAddress;
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ModifyStudType.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ModifyStudType.aspx");
        }
    }

    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            int idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "TANNO='" + txtEnrollmentSearch.Text + "' OR ENROLLNO='" + txtEnrollmentSearch.Text + "'") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "TANNO='" + txtEnrollmentSearch.Text + "' OR ENROLLNO='" + txtEnrollmentSearch.Text + "'"));

            DataSet dsGetDetails = new DataSet();
            dsGetDetails = objCommon.FillDropDown("ACD_DCR", "*", "", "IDNO = " + idno + "AND RECON=1 AND ISNULL(CAN,0)=0", "");
            if (dsGetDetails.Tables[0].Rows.Count > 0)
            {
                //select * from ACD_DCR where idno=5056 AND RECON=1 AND ISNULL(CAN,0)=0
                objCommon.DisplayMessage(this, "Fee Collection already Done!!!", this.Page);
                btnSubmit.Visible = false;
                StudDetails.Visible = false;
                txtEnrollmentSearch.Focus();
            }
            else
            {
                GetStudentDetails(idno);
            }
        }
        catch { }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string StudType = "";
            if (rdbHostel.Checked == true)
            {
                StudType = "Hostel";
            }
            else if (rdbTransport.Checked == true)
            {
                StudType = "Transport";
            }
            else
            {
                StudType = "Non Transport";
            }
            //to update details
            // ObjNuc.UpdateDemandStudType(Convert.ToInt32(txtEnrollmentSearch.Text.ToString()), StudType);

            CustomStatus cs = (CustomStatus)ObjNuc.AddandUpdateStudTypeDetails(Convert.ToInt32(lblapp.Text), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), txtRemarks.Text, StudType, DateTime.Today.Date);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                clear();
                objCommon.DisplayMessage(this, "Details Saved Sucessfully", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this, "Something went Wrong !!! Please Try after some Time.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ModifyStudType.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    public void GetStudentDetails(int idno)
    {
        try
        {
            DataSet ds = ObjNuc.GetStudentDetailForStudTypeDetails(Convert.ToInt32(idno));

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblname.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblapp.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
                    //lblenrollno.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString(); 
                    lbldegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                    lbldegree.ToolTip = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                    lblbranch.Text = ds.Tables[0].Rows[0]["BRANCHNAME"].ToString();
                    lblbranch.ToolTip = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    lblsem.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblsem.ToolTip = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();

                    string hostel = (ds.Tables[0].Rows[0]["HOSTELER"].ToString());
                    int Transport = Convert.ToInt32(ds.Tables[0].Rows[0]["TRANSPORT"].ToString());
                    if (hostel == "True")
                    {
                        rdbHostel.Checked = true;
                    }
                    else if (Transport == 1)
                    {
                        rdbTransport.Checked = true;
                    }
                    else
                    {
                        rdbHostel.Checked = false;
                        rdbTransport.Checked = false;
                        rdbNonTransport.Checked = true;
                    }

                    btnSubmit.Visible = true;
                    StudDetails.Visible = true;
                    //TempDetails.Visible = true;
                    #region "Commented"
                    //txtAdmdate.Text = ds.Tables[0].Rows[0]["ADMDATE"].ToString();
                    //lblmobile.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                    // lblEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                    // lblSession.Text = ds.Tables[0].Rows[0]["SESSIONNAME"].ToString();
                    // lblSession.ToolTip = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
                    // lblcategory.Text = ds.Tables[0].Rows[0]["CATEGORY"].ToString();
                    // hdfadmbatch.Value = ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
                    //lbladmbatch.Text = ds.Tables[0].Rows[0]["ADMBATCHNAME"].ToString();
                    // lbllastname.Text = ds.Tables[0].Rows[0]["STUDLASTNAME"].ToString();
                    // lbladmquota.Text = ds.Tables[0].Rows[0]["QUOTANAME"].ToString();

                    //string ptype = ds.Tables[0].Rows[0]["PTYPE"].ToString();
                    //if (Convert.ToInt32(ptype) > 0) { ddlpaytype.SelectedValue = ptype; }
                    // if (Convert.ToBoolean(hostel) == false) { rdbhostelyes.Checked = false; rdbhostelNo.Checked = true; } else { rdbhostelyes.Checked = true; rdbhostelNo.Checked = false; }
                    //// if (Transport == 0) { rdbtransportyes.Checked = false; rdbtransportNo.Checked = true; } else { rdbtransportyes.Checked = true; rdbtransportNo.Checked = false; }


                    // //ShowDemand();             

                    // string Status = ds.Tables[0].Rows[0]["APP_STATUS"].ToString();

                    // ViewState["PRO_STATUS"] = ds.Tables[0].Rows[0]["PROCESS_STATUS"].ToString();
                    //  //------------For Photo  --------------// 
                    // imgpreview.ImageUrl = "~/showimage.aspx?id=" + lblapp.Text + "&type=student";

                    // if (ViewState["PRO_STATUS"].ToString() == "1")
                    // {
                    //     if (Convert.ToInt32(Status) == 1)
                    //     {
                    //        // objCommon.DisplayMessage(this.pnlFeeTable, "Student Approve sucessfully", this);
                    //         objCommon.DisplayMessage(this.pnlFeeTable, "Student Approve Sucessfully. \\n Admission No. is : " + lblenrollno.Text + "\\n.", this);

                    //         //btnapproval.Enabled = false;
                    //        // ddlpaytype.Enabled = false;
                    //         // ddlroute.Enabled = false;
                    //         //ddlhostel.Enabled = false;
                    //         rdbhostelNo.Enabled = false;
                    //         rdbhostelyes.Enabled = false;

                    //        // btnProcess.Enabled = false;

                    //         if (Transport == 0) 
                    //         {
                    //             //btntransportChallan.Visible = false;
                    //             //btnChallan.Visible = true;
                    //         }
                    //         else
                    //         {
                    //             //btntransportChallan.Visible = true;
                    //             //btnChallan.Visible = true;
                    //         }

                    //         foreach (ListViewDataItem item in lvdemand.Items)
                    //         {
                    //             ImageButton btndel = item.FindControl("btnEdit") as ImageButton;
                    //             btndel.Enabled = false;
                    //         }


                    //     }
                    //     else
                    //     {

                    //        // ddlpaytype.Enabled = true;
                    //        // btnProcess.Enabled = true;
                    //         foreach (ListViewDataItem item in lvdemand.Items)
                    //         {
                    //             ImageButton btndel = item.FindControl("btnEdit") as ImageButton;
                    //             btndel.Enabled = true;
                    //         }
                    //         objCommon.DisplayMessage(this.pnlFeeTable, "Student Process Already Done!!", this);
                    //         //btnapproval.Enabled = true;
                    //     }
                    // }
                    // else
                    // {
                    //     lblMsg.Visible = false;
                    //     //btnapproval.Enabled = false;
                    // }

                    #endregion
                }
                else
                {
                    objCommon.DisplayMessage(this.pnlFeeTable, "Student Registration is Not Done.", this);
                    clear();
                    txtEnrollmentSearch.Text = string.Empty;
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.pnlFeeTable, "Please Select Right Temp No.", this);
                clear();
                return;
            }
        }
        catch { objCommon.DisplayMessage(this.pnlFeeTable, "Please Select Right Temp No.", this); }
    }
    public void clear()
    {
        lblname.Text = string.Empty;
        // lblapp.Text = string.Empty;
        lbldegree.Text = string.Empty;
        lblbranch.Text = string.Empty;
        //lblmobile.Text = string.Empty;
        //lblEmail.Text = string.Empty;
        //lblSession.Text = string.Empty;
        //lblcategory.Text = string.Empty;
        //lbladmbatch.Text = string.Empty;
        lblsem.Text = string.Empty;
        // txtAdmdate.Text = string.Empty;
        rdbHostel.Checked = false;
        rdbNonTransport.Checked = false;
        rdbTransport.Checked = false;
        txtRemarks.Text = "";
        btnSubmit.Visible = false;
        // TempDetails.Visible = false;
        StudDetails.Visible = false;
        txtEnrollmentSearch.Text = "";
        //lbllastname.Text = string.Empty;
        //lblenrollno.Text = string.Empty;
        //lvdemand.DataSource = null;
        //lvdemand.DataBind();
        //imgpreview.Dispose();
        //imgpreview.ImageUrl = null;
    }

    //#region Create Demand

    //private FeeDemand GetDemandCriteria()
    //{
    //    FeeDemand demandCriteria = new FeeDemand();
    //    try
    //    {
    //        demandCriteria.SessionNo = Convert.ToInt16(lblSession.ToolTip.ToString());
    //        //demandCriteria.ReceiptTypeCode = (chktution.);
    //        demandCriteria.BranchNo = Convert.ToInt32(lblbranch.ToolTip.ToString());
    //        demandCriteria.SemesterNo = Convert.ToInt32(lblsem.ToolTip.ToString());
    //        //demandCriteria.PaymentTypeNo = Convert.ToInt32(ddlpaytype.SelectedValue);
    //        demandCriteria.UserNo = int.Parse(Session["userno"].ToString());
    //        demandCriteria.CollegeCode = Session["colcode"].ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Academic_CreateDemand.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //    return demandCriteria;
    //}

    //private void ShowMessage(string msg)
    //{
    //    this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    //}
    //#endregion
    //protected void ddlpaytype_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int count = 0;
    //    if (rdbtutionyes.Checked == true || rdbtransportyes.Checked == true) // rdbhostelyes.Checked == true ||
    //    {
    //        //int ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "COUNT(1)idno", "IDNO=" + Convert.ToInt32(txtEnrollmentSearch.Text.ToString()) + " AND SESSIONNO =" + Convert.ToInt32(lblSession.ToolTip) + " AND SEMESTERNO =" + Convert.ToInt32(lblsem.ToolTip.ToString()) + " AND RECIEPT_CODE = 'TF'"));
    //        //if (ifPaidAlready > 0)
    //        //{
    //        //    objCommon.DisplayMessage(this.pnlFeeTable, "Student Already process", this);

    //        //   // return;
    //        //}

    //        if (rdbtransportyes.Checked == true)
    //        {
    //            //if (ddlroute.SelectedValue == "0")
    //            //{
    //            //    objCommon.DisplayMessage(this.pnlFeeTable, "First Select Route Type", this); ddlpaytype.SelectedIndex = 0; return;
    //            //} else {
    //            int Routestandard = Convert.ToInt32(objCommon.LookUp("ACD_STANDARD_FEES", "COUNT(*)", "RECIEPT_CODE ='TPF' AND DEGREENO= " + Convert.ToInt32(lbldegree.ToolTip.ToString()) + " AND BATCHNO= " + Convert.ToInt32(hdfadmbatch.Value.ToString()) + " AND PAYTYPENO=" + Convert.ToInt32(ddlpaytype.SelectedValue.ToString())));
    //            if (Routestandard <= 0)
    //            {
    //                objCommon.DisplayMessage(this.pnlFeeTable, " Transport Standard fees is not defined for fees criteria applicable to these students", this);
    //                btnProcess.Enabled = false; btnapproval.Enabled = false; count = count + 1;
    //                ddlpaytype.SelectedIndex = 0;
    //                return;
    //            }
    //            //}
    //        }
    //        //if (rdbhostelyes.Checked == true)
    //        //{
    //        //    //if (ddlhostel.SelectedValue == "0")
    //        //    //{
    //        //    //    objCommon.DisplayMessage(this.pnlFeeTable, "First Select Hostel Type", this); ddlpaytype.SelectedIndex = 0; return; } else {
    //        //    int hostelstandard = Convert.ToInt32(objCommon.LookUp("ACD_STANDARD_FEES", "COUNT(*)", "RECIEPT_CODE ='HF' AND DEGREENO= " + Convert.ToInt32(lbldegree.ToolTip.ToString()) + " AND BATCHNO= " + Convert.ToInt32(hdfadmbatch.Value.ToString()) + " AND PAYTYPENO=" + Convert.ToInt32(ddlpaytype.SelectedValue.ToString())));
    //        //    if (hostelstandard <= 0)
    //        //    {
    //        //        objCommon.DisplayMessage(this.pnlFeeTable, "Hostel Standard fees is not defined for fees criteria applicable to these students", this);
    //        //        btnProcess.Enabled = false; btnapproval.Enabled = false; count = count + 1;
    //        //        ddlpaytype.SelectedIndex = 0;
    //        //        return;
    //        //    }
    //        //    //}
    //        //}
    //        if (rdbtutionyes.Checked == true)
    //        {
    //            int tutstandard = Convert.ToInt32(objCommon.LookUp("ACD_STANDARD_FEES", "COUNT(*)", "RECIEPT_CODE ='TF' AND DEGREENO= " + Convert.ToInt32(lbldegree.ToolTip.ToString()) + " AND BATCHNO= " + Convert.ToInt32(hdfadmbatch.Value.ToString()) + " AND PAYTYPENO=" + Convert.ToInt32(ddlpaytype.SelectedValue.ToString())));
    //            if (tutstandard <= 0)
    //            {
    //                objCommon.DisplayMessage(this.pnlFeeTable, " Tuition Standard fees is not defined for fees criteria applicable to these students.", this);
    //                btnProcess.Enabled = false; btnapproval.Enabled = false; count = count + 1;
    //                ddlpaytype.SelectedIndex = 0;
    //                return;
    //            }
    //        }

    //        if (count > 0)
    //        {
    //            btnProcess.Enabled = false;
    //            btnapproval.Enabled = false;
    //        }
    //        else
    //        {
    //            btnapproval.Enabled = false;
    //            btnProcess.Enabled = true;
    //        }
    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage(this.pnlFeeTable, "First Select Student Fees Type", this);
    //        return;
    //    }
    //}
    //public void SendEmail(string mailId, string name)
    //{
    //    try
    //    {
    //        string EMAILID = mailId.Trim();
    //        var fromAddress = objCommon.LookUp("REFF", "LTRIM(RTRIM(EMAILSVCID))", "");

    //        // any address where the email will be sending
    //        var toAddress = EMAILID.Trim();

    //        //Password of your gmail address
    //        var fromPassword = objCommon.LookUp("REFF", "LTRIM(RTRIM(EMAILSVCPWD))", "");

    //        // Passing the values and make a email formate to display
    //        MailMessage msg = new MailMessage();
    //        SmtpClient smtp = new SmtpClient();

    //        msg.From = new MailAddress(fromAddress, "SVCE");
    //        msg.To.Add(new MailAddress(toAddress));
    //        msg.Subject = "Admission Approved Successfully";

    //        using (StreamReader reader = new StreamReader(Server.MapPath("~/approval_template.html")))
    //        {
    //            msg.Body = reader.ReadToEnd();
    //        }


    //        msg.Body = msg.Body.Replace("{Name}", name);
    //        msg.Body = msg.Body.Replace("{AdmissionNumber}", lblenrollno.Text);

    //        msg.IsBodyHtml = true;
    //        //smtp.enableSsl = "true";
    //        smtp.Host = "smtp.gmail.com";
    //        smtp.Port = 587;
    //        smtp.UseDefaultCredentials = false;
    //        smtp.EnableSsl = true;
    //        smtp.Credentials = new System.Net.NetworkCredential(fromAddress.Trim(), fromPassword.Trim());
    //        ServicePointManager.ServerCertificateValidationCallback =
    //            delegate(object s, X509Certificate certificate,
    //            X509Chain chain, SslPolicyErrors sslPolicyErrors)
    //            { return true; };
    //        try
    //        {
    //            smtp.Send(msg);
    //        }
    //        catch (Exception ex)
    //        {
    //            if (Convert.ToBoolean(Session["error"]) == true)
    //                objUaimsCommon.ShowError(Page, "ACADEMIC_NewStudentApproval_SemdEmail-> " + ex.Message + " " + ex.StackTrace);
    //            else
    //                objUaimsCommon.ShowError(Page, "Server UnAvailable");
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "ACADEMIC_NewStudentApproval_SemdEmail-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}




    //public void ShowDemand()
    //{
    //    DataSet ds1 = ObjNuc.GetStudentDetailfordemanddetails(Convert.ToInt32(txtEnrollmentSearch.Text.ToString()));
    //    lvdemand.DataSource = ds1;
    //    lvdemand.DataBind();
    //}

    //protected void btnEdit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ImageButton btnEdit = sender as ImageButton;
    //        DemandModificationController dmController1 = new DemandModificationController();
    //        int demandno = int.Parse(btnEdit.CommandArgument);
    //        int response = dmController1.deleteDemandForNewStudents(Convert.ToInt32(txtEnrollmentSearch.Text), demandno);
    //        if (response > 0)
    //        {
    //            objCommon.DisplayMessage(this.pnlFeeTable, "Student Demand Cancel Sucessfully!", this);
    //            GetStudentDetails();
    //        }

    //        //  this.ShowDetails(SESSIONNO);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Academic_SessionCreate.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable");
    //    }
    //}


    //protected void btnProcess_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (rdbtransportyes.Checked == true || rdbtutionyes.Checked == true) //rdbhostelyes.Checked == true ||
    //        {
    //            DemandModificationController dmController = new DemandModificationController();
    //            FeeDemand demandCriteria = this.GetDemandCriteria();
    //            int selectSemesterNo = Int32.Parse(lblsem.ToolTip.ToString());
    //            int hostel = 0; int transport = 0;
    //            if (rdbhostelyes.Checked == true)
    //            { hostel = 1; }
    //            else
    //            { hostel = 0; }
    //            if (rdbtransportyes.Checked == true)
    //            { transport = 1; }
    //            else
    //            { transport = 0; }
    //            string response = string.Empty; int count = 0;
    //            if (rdbtutionyes.Checked == true)
    //            {
    //                response = dmController.CreateDemandForNewStudents(Convert.ToInt32(txtEnrollmentSearch.Text.Trim().ToString()), demandCriteria, Convert.ToInt32(lblsem.ToolTip.ToString()), "TF", ViewState["ipAddress"].ToString(), hostel, transport);//chkOverwrite.Checked
    //                if (response != "-99" && response != "2") { count = count + 1; }
    //            }
    //            //if (rdbhostelyes.Checked == true)
    //            //{
    //            //    response = dmController.CreateDemandForNewStudents(Convert.ToInt32(txtEnrollmentSearch.Text.Trim().ToString()), demandCriteria, Convert.ToInt32(lblsem.ToolTip.ToString()), "HF", ViewState["ipAddress"].ToString(), hostel, transport);//chkOverwrite.Checked
    //            //    if (response != "-99" && response != "2") { count = count + 1; }
    //            //}
    //            if (rdbtransportyes.Checked == true)
    //            {
    //                response = dmController.CreateDemandForNewStudents(Convert.ToInt32(txtEnrollmentSearch.Text.Trim().ToString()), demandCriteria, Convert.ToInt32(lblsem.ToolTip.ToString()), "TPF", ViewState["ipAddress"].ToString(), hostel, transport);//chkOverwrite.Checked
    //                if (response != "-99" && response != "2") { count = count + 1; }
    //            }
    //            if (response != "-99")
    //            {
    //                if (response == "2")
    //                    objCommon.DisplayMessage(this.pnlFeeTable,  "Unable to Processs following students.\\Temp No.: " + txtEnrollmentSearch.Text + "\\nStandard fees is not defined for fees criteria applicable to these students.",this);
    //                else
    //                {
    //                    objCommon.DisplayMessage(this.pnlFeeTable, "Student Fees Process Sucessfully", this);
    //                    GetStudentDetails();
    //                }
    //            }
    //            else
    //                ShowMessage("Something Get Wrong");
    //            if (count > 0)
    //            {
    //                objCommon.DisplayMessage(this.pnlFeeTable, "Student Fees Process Sucessfully", this);
    //                GetStudentDetails();
    //            }
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(this.pnlFeeTable, "Please Select Altleast One Fees Type!!", this);
    //            return;
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Academic_CreateDemand.btnCreateDemand_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    //protected void rdbhostelyes_CheckedChanged(object sender, EventArgs e)
    //{
    //   // if (rdbhostelyes.Checked == true) { ddlhostel.Enabled = true; ddlhostel.SelectedIndex = 0; } else { ddlhostel.Enabled = false; ddlhostel.SelectedIndex = 0; }
    //}
    //protected void rdbtransportyes_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rdbtransportyes.Checked == true) { ddlroute.Enabled = true; ddlroute.SelectedIndex = 0; } else { ddlroute.Enabled = false; ddlroute.SelectedIndex = 0; }
    //}

    //  -- TUITION FEES CHALAN
    //protected void btnChallan_Click(object sender, EventArgs e)
    //{
    //    int COUNT = 0;

    //    COUNT = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(*) ", "IDNO=" + Convert.ToInt32(lblapp.Text.ToString()) + " AND SESSIONNO =" + lblSession.ToolTip + " AND SEMESTERNO =" + Convert.ToInt32(lblsem.ToolTip) + " AND RECIEPT_CODE ='TF' AND ISNULL(RECON,0)=1 AND ISNULL(CAN,0) = 0"));
    //    if (COUNT == 0)
    //    {
    //        this.ShowReport("Payment_Details", "RptBankChallanSummary.rpt", "TF");
    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage(this.pnlFeeTable, "Tuition Fees Already Done For this Session..!", this.Page);
    //        return;
    //    }


    //}

    ////  -- TRANSPORT CHALAN RECEIPT 
    //protected void btntransportChallan_Click(object sender, EventArgs e)
    //{
    //    int COUNT = 0;

    //    COUNT = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(*) ", "IDNO=" + Convert.ToInt32(lblapp.Text.ToString()) + " AND SESSIONNO =" + lblSession.ToolTip + " AND SEMESTERNO =" + Convert.ToInt32(lblsem.ToolTip) + " AND RECIEPT_CODE ='TPF' AND ISNULL(RECON,0)=1 AND ISNULL(CAN,0) = 0"));
    //    if (COUNT == 0)
    //    {
    //        this.ShowReport("Payment_Details", "RptBankChallanSummary.rpt", "TPF");
    //    }           
    //     else
    //        {
    //            objCommon.DisplayMessage(this.pnlFeeTable, "Transport Fees Already Done For this Session..!", this.Page);
    //            return;
    //        }
    //}

    //private void ShowReport(string reportTitle, string rptFileName, string Reciepttype)
    //{
    //    try
    //    {
    //        string OrderId = string.Empty;

    //      //  OrderId = objCommon.LookUp("ACD_DCR", "distinct DCR_NO ", "IDNO=" + Convert.ToInt32(lblapp.Text.ToString()) + " AND SESSIONNO =" + lblSession.ToolTip + " AND SEMESTERNO =" + Convert.ToInt32(lblsem.ToolTip) + " AND RECIEPT_CODE ='" + Reciepttype + "' AND ISNULL(CAN,0) = 0");

    //        OrderId = objCommon.LookUp("ACD_DEMAND", "distinct DM_NO ", "IDNO=" + Convert.ToInt32(lblapp.Text.ToString()) + " AND SESSIONNO =" + lblSession.ToolTip + " AND SEMESTERNO =" + Convert.ToInt32(lblsem.ToolTip) + " AND RECIEPT_CODE ='" + Reciepttype + "' AND ISNULL(CAN,0) = 0");


    //        if (!string.IsNullOrEmpty(OrderId))
    //        {
    //            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //            url += "Reports/CommonReport.aspx?";
    //            url += "pagetitle=" + reportTitle;
    //            url += "&path=~,Reports,Academic," + rptFileName;
    //            url += "&param=@P_IDNO=" + Convert.ToInt32(lblapp.Text.ToString()) + ",@P_DCRNO=" + OrderId + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
    //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //            sb.Append(@"window.open('" + url + "','','" + features + "');");

    //            ScriptManager.RegisterClientScriptBlock(this.pnlFeeTable, this.pnlFeeTable.GetType(), "controlJSScript", sb.ToString(), true);

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Atom_Response.ShowReport_NEW() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    //private void ShowReportHostel(string reportTitle, string rptFileName, string Reciepttype)
    //{
    //    try
    //    {
    //        string OrderId = string.Empty;
    //        int COUNT = 0;

    //        COUNT = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(*) ", "IDNO=" + Convert.ToInt32(lblapp.Text.ToString()) + " AND SESSIONNO =" + lblSession.ToolTip + " AND SEMESTERNO =" + Convert.ToInt32(lblsem.ToolTip) + " AND RECIEPT_CODE ='HF' AND RECON=1 AND ISNULL(CAN,0) = 0"));
    //        if (COUNT == 0)
    //        {
    //            OrderId = objCommon.LookUp("ACD_DEMAND", "distinct DM_NO ", "IDNO=" + Convert.ToInt32(lblapp.Text.ToString()) + " AND SESSIONNO =" + lblSession.ToolTip + " AND SEMESTERNO =" + Convert.ToInt32(lblsem.ToolTip) + " AND RECIEPT_CODE ='HF' AND ISNULL(CAN,0) = 0");


    //            if (!string.IsNullOrEmpty(OrderId))
    //            {
    //                RoomAllotmentController RAC = new RoomAllotmentController();

    //                RAC.InsertPendingInDCR(Convert.ToInt32(lblapp.Text.ToString()), Convert.ToInt32(lblsem.ToolTip.ToString()), Reciepttype, 0.00, Convert.ToInt32(lblSession.ToolTip));


    //                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //                url += "Reports/CommonReport.aspx?";
    //                url += "pagetitle=" + reportTitle;
    //                url += "&path=~,Reports,Academic," + rptFileName;
    //                url += "&param=@P_IDNO=" + Convert.ToInt32(lblapp.Text.ToString()) + ",@P_DCRNO=" + OrderId + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
    //                System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //                sb.Append(@"window.open('" + url + "','','" + features + "');");

    //                ScriptManager.RegisterClientScriptBlock(this.pnlFeeTable, this.pnlFeeTable.GetType(), "controlJSScript", sb.ToString(), true);

    //            }
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(this.pnlFeeTable, "Hostel Fees Already Done For this Session..!", this.Page);
    //            return;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Atom_Response.ShowReport_NEW() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    //protected void btnHostelChallan_Click(object sender, EventArgs e)
    //{
    //    this.ShowReportHostel("Payment_Details", "RptBankChallanSummary.rpt", "HF");
    //}

}

