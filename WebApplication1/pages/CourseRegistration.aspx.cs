//======================================================================================
// PROJECT NAME  : RFCAMPUS-SVCE
// MODULE NAME   : ACADEMIC
// PAGE NAME     : COURSE REGISTRATION                                      
// CREATION DATE : 2/6/2019
// ADDED BY      : 
// ADDED DATE    : 
// MODIFIED BY   : RAJU BITODE
// MODIFIED DESC : USED FOR [NON-CBCS] STUDENT COURSE REG.                                                   
//======================================================================================

using System;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_CourseRegistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    StudentRegist objSR = new StudentRegist();
    //FeeCollectionController feeController = new FeeCollectionController();

    #region Page Load
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
                ////Page Authorization
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                this.PopulateDropDownList();

                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                string IPADDRESS = string.Empty;

                IPADDRESS = ip.AddressList[0].ToString();
                //ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                ViewState["ipAddress"] = IPADDRESS;
               // objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
                //Check for Activity On/Off for course registration.
                //if (CheckActivity())
                //{
                    ViewState["action"] = "add";
                    ViewState["idno"] = "0";

                    if (Session["usertype"].ToString().Equals("2"))     //Student 
                    {
                        divOptions.Visible = false;
                        ViewState["idno"] = Session["idno"].ToString();
                    }
                    else
                    {
                        divOptions.Visible = false;
                        //LoadFacultyPanel();
                        AdminPanelShow();
                    }
                //}
                //else
                //{
                //    divCourses.Visible = false;
                //    divNote.Visible = false;
                //    divOptions.Visible = false;

                //}
            }
        }

        lblmsg.Text = string.Empty;
        divMsg.InnerHtml = string.Empty;
    }
    //geting user ip address
    private string GetUserIPAddress()
    {
        string User_IPAddress = string.Empty;
        string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
        {
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            //or
            //Client_IPAddress = Request.UserHostAddress;
            //or
            //User_IPAddress = Request.ServerVariables["REMOTE_HOST"];
        }
        else////with Proxy detection
        {
            string[] splitter = { "," };
            string[] IP_Array = User_IPAddressRange.Split(splitter, System.StringSplitOptions.None);

            int LatestItem = IP_Array.Length - 1;
            User_IPAddress = IP_Array[LatestItem - 1];
            //User_IPAddress = IP_Array[0];
        }
        return User_IPAddress;
    }

    private string getIP_Address()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        string IP = String.Empty;
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                IP = ip.ToString();
            }
        }
        if (IP == string.Empty)
        {
            IP = "0:0:0";
        }
        return IP;
    }

    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page. 
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page and maintain user activity
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CourseRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CourseRegistration.aspx");
        }
    }

    private bool CheckNotOldStudents()
    {
        bool flag = true;

        return flag;
    }
    //chcek Activity is ON 
    private bool CheckActivity()
    {
        bool ret = true;
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                ret = false;
            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                ret = false;
            }
        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            ret = false;
        }
        dtr.Close();
        return ret;
    }
    #endregion

    #region From Student Login
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["usertype"].ToString().Equals("2"))     //Student 
            {
                if (CheckNotOldStudents())
                {
                    LoadStudentPanel();
                }
            }
            else
            {
                LoadFacultyPanel();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.btnProceed_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
 {
        try
        {
            ViewState["ipAddress"] = GetUserIPAddress();

            ///Check Student is Already Registered from Student Login Only 

            if (Session["usertype"].ToString().Equals("2"))     //Student 
            {
                ///Check current semester registered or not  
                if (Convert.ToInt32(ViewState["countAlreadyReg"]) > 0)    //if (count != "0")
                {
                    objCommon.DisplayMessage("Student Registration already done.", this.Page);
                    return;
                }
            }


            if (ViewState["action"].ToString() == "add")
            {
                objSR.EXAM_REGISTERED = 1;

                foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
                {
                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                }

                //added By satish for Elective-1 Course
                foreach (ListViewDataItem dataitem in lvElectiveCourse.Items)
                {
                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                }

                //added By satish for Elective-2 Course
                foreach (ListViewDataItem dataitem in lvElectiveCourse2.Items)
                {
                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                }

                //added By satish for Elective-3 Course
                foreach (ListViewDataItem dataitem in lvElectiveCourse3.Items)
                {
                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                }

                //added By satish for Elective-4 Course
                foreach (ListViewDataItem dataitem in lvElectiveCourse4.Items)
                {
                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                }
                if (objSR.COURSENOS != null)
                    objSR.COURSENOS = objSR.COURSENOS.TrimEnd('$');
            }
            else
            {
                objSR.EXAM_REGISTERED = 1;
                foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
                {
                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                }
                //added By satish for Elective-I Course
                foreach (ListViewDataItem dataitem in lvElectiveCourse.Items)
                {
                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                }

                //added By satish for Elective-2 Course
                foreach (ListViewDataItem dataitem in lvElectiveCourse2.Items)
                {
                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                }

                //added By satish for Elective-3 Course
                foreach (ListViewDataItem dataitem in lvElectiveCourse3.Items)
                {
                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                }

                //added By satish for Elective-4 Course
                foreach (ListViewDataItem dataitem in lvElectiveCourse4.Items)
                {
                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                }
                if (objSR.COURSENOS != null)
                    objSR.COURSENOS = objSR.COURSENOS.TrimEnd('$');
            }

            int subRegCount = 0;
            foreach (ListViewDataItem item in lvCurrentSubjects.Items)
            {
                CheckBox chk = item.FindControl("chkAccept") as CheckBox;
                if (chk.Checked)
                {
                    subRegCount++;
                }
            }

            if (lvCurrentSubjects.Items.Count > subRegCount)
            {
                objCommon.DisplayMessage("Please Select All Core Subjects for Subject Registration!", this.Page);
                return;
            }

            if (objSR.COURSENOS.Length > 0)
            {
                //Add registered 
                objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                objSR.IDNO = Convert.ToInt32(lblName.ToolTip);
                objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip);
                objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);

                //objSR.IPADDRESS = Session["ipAddress"].ToString();
                objSR.IPADDRESS = ViewState["ipAddress"].ToString();
                objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                objSR.COLLEGE_CODE = Session["colcode"].ToString();
                objSR.REGNO = lblEnrollNo.Text.Trim();
                objSR.ROLLNO = txtRollNo.Text.Trim();
                string moduletype = Convert.ToString(lblselect.Text);
                int amount = Convert.ToInt32(hdnSelectedCourseFeeBkg.Value);
                int totalfee = Convert.ToInt32(hdnTotalFee.Value);
                
                //insert registration  
                int ret = objSReg.AddRegisteredSubjects_Stud(objSR, moduletype, amount, totalfee);
                if (ret == 1)
                {
                    Session["RegStatus"] = "1"; // maintained subject registration done flag.
                    if (Session["usertype"].ToString().Equals("2"))     //Student 
                    {
                        objCommon.DisplayMessage("Subject(s) Registered Successfully.", this.Page);

                        BindStudentDetails();
                        BindAvailableCourseList();
                        BindElectiveCourseList();
                        BindElectiveCourseList2();
                        BindElectiveCourseList3();
                        BindElectiveCourseList4();
                        btnPrintRegSlip.Visible = true;
                        btnPrintRegSlip.Enabled = true;
                        btnSubmit.Visible = false;
                    }
                    else
                    {
                        objCommon.DisplayMessage("Subject(s) Registered Successfully.", this.Page);
                        BindStudentDetails();
                        BindAvailableCourseList();
                        BindElectiveCourseList();
                        BindElectiveCourseList2();
                        BindElectiveCourseList3();
                        BindElectiveCourseList4();
                        btnPrintRegSlip.Visible = true;
                        btnPrintRegSlip.Enabled = true;
                        btnSubmit.Visible = false;
                    }
                }
                else
                {
                    Session["RegStatus"] = "0";
                    objCommon.DisplayMessage("Error! in saving record.", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select atleast One Subject in Subject list for Subject Registration..!!", this.Page);
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
    #endregion

    #region Private Methods
    //bind SESSION_NAME in drop down list
    private void PopulateDropDownList()
    {


      ///  objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO = " + Session["currentsession"].ToString(), "SESSIONNO");
      objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'  and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO = " + Session["currentsession"].ToString(), "SESSIONNO");
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'  and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");

  //      objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO = " + Session["currentsession"].ToString(), "SESSIONNO");
      objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'  and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
        ddlSession.SelectedIndex = 1;
        mrqSession.InnerHtml = "Registration Started for Session : " + (Convert.ToInt32(ddlSession.SelectedValue) > 0 ? ddlSession.SelectedItem.Text : "---");

        ddlSession.Focus();
    }

    private void LoadStudentPanel()
    {
        tblSession.Visible = false;
        divNote.Visible = false;
        ///Changes Done by Mr.Manish Walde : PREV_STATUS = 0 and 
        string Regcount = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND IDNO=" + ViewState["idno"].ToString() + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));

        string count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ISNULL(REGISTERED,0) = 1 AND IDNO=" + ViewState["idno"].ToString() + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        Session["RegStatus"] = count;
        if (count != "0")
        {
            objCommon.DisplayMessage("Registration already done .", this.Page);
            this.ShowDetails();
            BindStudentDetails();//added by Satish
            lvCurrentSubjects.Visible = true;
            lvElectiveCourse.Visible = true;
            lvElectiveCourse2.Visible = true;
            lvElectiveCourse3.Visible = true;
            lvElectiveCourse4.Visible = true;

            btnSubmit.Visible = false;
            btnPrintRegSlip.Visible = true;
            btnPrintRegSlip.Enabled = true;
        }
        if (Regcount != "0")
        {
            btnSubmit.Enabled = false;
        }

        else
        {
            this.ShowDetails();
            BindStudentDetails();
            txtRcptNo.Enabled = true;
            txtRcptDt.Enabled = true;
            txtRcptAmt.Enabled = true;
        }
    }
    //bind SESSION_PNAME in drop down list and Bind  student list
    private void LoadFacultyPanel()
    {
        if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("5") || Session["usertype"].ToString().Equals("1"))
        {
            objCommon.FillDropDownList(ddlSessionReg, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");

            if (ddlSessionReg.Items.Count > 1)
            {
                ddlSessionReg.SelectedIndex = 1;
            }
            BindStudentList();
        }
        divNote.Visible = false;
        divCourses.Visible = false;
        pnlDept.Visible = true;
        ddlSession.SelectedIndex = 0;
        txtRollNo.Text = string.Empty;
        PopulateDropDownList();


    }
    #endregion
    //used for enable true and false controles
    private void AdminPanelShow()
    {
        divNote.Visible = false;
        divCourses.Visible = true;
        lvCurrentSubjects.Visible = false;


        lvElectiveCourse.Visible = false;

        pnlDept.Visible = false;
        ddlSession.Enabled = false;
        txtRollNo.Text = string.Empty;
        tblInfo.Visible = false;
        tblSession.Visible = true;
        btnShow.Visible = true;
        btnCancel.Visible = true;
        txtRollNo.Enabled = true;
        //btnBackHOD.Visible = false;
        btnEditReg.Visible = false;
        lblmsg.Text = string.Empty;
    }

    #region From Faculty Advisor Login Single Student Search

    protected void rblOptions_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["idno"] = "0";

        if (rblOptions.SelectedValue == "S")///For Single
        {
            divNote.Visible = false;
            divCourses.Visible = true;
            lvCurrentSubjects.Visible = false;

            lvElectiveCourse.Visible = false;

            pnlDept.Visible = false;
            ddlSession.Enabled = false;
            txtRollNo.Text = string.Empty;
            tblInfo.Visible = false;
            tblSession.Visible = true;
            btnShow.Visible = true;
            btnCancel.Visible = true;
            txtRollNo.Enabled = true;
            // btnBackHOD.Visible = false;
            btnEditReg.Visible = false;
            lblmsg.Text = string.Empty;

        }
        else///For Multiple
        {
            divNote.Visible = false;
            divCourses.Visible = false;
            pnlDept.Visible = true;
            txtRollNo.Text = string.Empty;
            btnEditReg.Visible = false;
            lblmsg.Text = string.Empty;
        }
    }
    //used for showing Register student details
    protected void btnShow_Click(object sender, EventArgs e)
    {
        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + txtRollNo.Text.Trim() + "'AND ISNULL(CAN,0)=0 AND DEGREENO NOT IN (2,3) ");
        ViewState["idno"] = idno;

        if (idno == "")
        {
            objCommon.DisplayMessage("Student Not Found for Entered Registration No.[" + txtRollNo.Text.Trim() + "]", this.Page);
            lvCurrentSubjects.DataSource = null;
            lvCurrentSubjects.DataBind();

            lvElectiveCourse.DataSource = null;
            lvElectiveCourse.DataBind();

            lvElectiveCourse2.DataSource = null;
            lvElectiveCourse2.DataBind();

            lvElectiveCourse3.DataSource = null;
            lvElectiveCourse3.DataBind();

            lvElectiveCourse4.DataSource = null;
            lvElectiveCourse4.DataBind();

        }
        else
        {
            ViewState["idno"] = idno;
            if (string.IsNullOrEmpty(ViewState["idno"].ToString()) || ViewState["idno"].ToString() == "0")
            {
                objCommon.DisplayMessage("Student with Registration No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                return;
            }
            if (CheckNotOldStudents())
            {

                this.ShowDetails();
                //Check current semester applied or not
                string applyCount = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0  AND REGISTERED = 1 AND IDNO=" + ViewState["idno"] + "AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                if (applyCount == "0")
                {
                    Session["RegStatus"] = "0";
                    objCommon.DisplayMessage("Student with Registration No.[" + txtRollNo.Text.Trim() + "] has not registered for selected session. \\n you can Proceed registeration.", this.Page);
                    //return;
                    BindStudentDetails();
                    //btnSubmit.Visible = true; // by satish-19072018
                    btnPrintRegSlip.Visible = false;
                    btnPrintRegSlip.Enabled = false;

                    btnSubmit.Enabled = true;
                    // btnBackHOD.Visible = false;
                }
                else
                {
                    Session["RegStatus"] = "1";
                    objCommon.DisplayMessage("Student with Registration No.[" + txtRollNo.Text.Trim() + "] has already registered for selected session.", this.Page);

                    ViewState["action"] = "edit";
                    BindStudentDetails();

                    // btnBackHOD.Visible = false;
                    txtRollNo.Enabled = false;
                    rblOptions.Enabled = false;

                    btnSubmit.Visible = false;
                    btnPrintRegSlip.Visible = true;
                    btnPrintRegSlip.Enabled = true;
                }
            }
        }
    }
    //used for editing student details 
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            txtRollNo.Text = btnEdit.CommandArgument;
            txtRollNo.Enabled = false;

            ViewState["idno"] = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + btnEdit.CommandArgument + "'");

            if (string.IsNullOrEmpty(ViewState["idno"].ToString()) || ViewState["idno"].ToString() == "0")
            {
                objCommon.DisplayMessage("Student with Registration No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                return;
            }

            if (CheckNotOldStudents())
            {
                this.ShowDetails();
                //Check current semester applied or not
                string applyCount = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0  AND REGISTERED=1 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                if (applyCount == "0")
                {
                    objCommon.DisplayMessage("Student with Registration No.[" + txtRollNo.Text.Trim() + "] has not registered for selected session. \\nBut you can directly register him.", this.Page);
                    //return;
                }
                BindStudentDetails();
                ViewState["action"] = "edit";
                btnShow.Visible = false;
                btnCancel.Visible = false;
                pnlDept.Visible = false;
                tblSession.Visible = false;
                rblOptions.Enabled = false;

                btnSubmit.Enabled = true;
                btnPrintRegSlip.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_courseRegistration.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //used for refreshing the controles or reset or canceling all the controlles
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["idno"] = "0";
        divNote.Visible = false;
        divCourses.Visible = true;
        pnlDept.Visible = false;
        ddlSession.Enabled = false;
        txtRollNo.Text = string.Empty;
        txtRollNo.Enabled = true;
        rblOptions.Enabled = true;

        lvCurrentSubjects.DataSource = null;
        lvCurrentSubjects.DataBind();


        lvElectiveCourse.DataSource = null;
        lvElectiveCourse.DataBind();

        lvElectiveCourse2.DataSource = null;
        lvElectiveCourse2.DataBind();

        lvElectiveCourse3.DataSource = null;
        lvElectiveCourse3.DataBind();

        lvElectiveCourse4.DataSource = null;
        lvElectiveCourse4.DataBind();

        tblInfo.Visible = false;
        btnEditReg.Visible = false;
        btnSubmit.Enabled = false;
        btnPrintRegSlip.Visible = false;
        // btnBackHOD.Visible = false;
        btnSubmit.Visible = false;
    }

    #endregion

    #region Private Methods
    //used for bind all the data from dataset on labels and show
    private void ShowDetails()
    {
        try
        {
            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO)  LEFT OUTER JOIN ACD_COURSE_REG_FEES RF ON(S.IDNO=RF.IDNO  AND RF.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ") INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) ", "S.IDNO,DG.DEGREENAME", "RF.RECEIPT_NO,RF.RECEIPT_DT,RF.RECEIPT_AMT,RF.BckLog_RECEIPT_NO,RF.BckLog_RECEIPT_DT,RF.BckLog_RECEIPT_AMT ,B.LONGNAME,S.ENROLLNO,S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.SEMESTERNO,ISNULL(S.SCHEMENO,0)SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.NAME_WITH_INITIAL,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE S.PHYSICALLY_HANDICAPPED WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH", "ISNULL(S.CAN,0)=0 AND S.IDNO = " + ViewState["idno"].ToString(), string.Empty);
           // DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO)  LEFT OUTER JOIN ACD_COURSE_REG_FEES RF ON(S.IDNO=RF.IDNO  AND RF.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ") INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO)INNER JOIN ACD_DEFINE_TOTAL_CREDIT C ON (S.IDNO=C.IDNO) INNER JOIN ACD_MODULE_REGISTRATION_TYPE RT ON (C.MODULE_REG_TYPE =RT.MODULE_REGISTRATION_NO)", "S.IDNO,DG.DEGREENAME", "RF.RECEIPT_NO,RF.RECEIPT_DT,RF.RECEIPT_AMT,RF.BckLog_RECEIPT_NO,RF.BckLog_RECEIPT_DT,RF.BckLog_RECEIPT_AMT ,(C.FROM_CREDIT+C.TO_CREDIT)AS TOTAL_CREDIT ,C.MIN_REG_CREDIT_LIMIT,RT.MODULE_REGISTRATION_NO,S.ENROLLNO,S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.SEMESTERNO,ISNULL(S.SCHEMENO,0)SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE S.PHYSICALLY_HANDICAPPED WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH", "ISNULL(S.CAN,0)=0 AND S.IDNO = " + ViewState["idno"].ToString(), string.Empty);
            // DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO)   INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO)", "S.IDNO,DG.DEGREENAME", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO,S.SEMESTERNO,ISNULL(S.SCHEMENO,0)SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE S.PHYSICALLY_HANDICAPPED WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH", "S.IDNO = " + ViewState["idno"].ToString(), string.Empty);
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    lblName.Text = dsStudent.Tables[0].Rows[0]["NAME_WITH_INITIAL"].ToString();
                    lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                    lblFatherName.Text = "<b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + "</b>" + " / " + "<b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + "</b>";
                    lblMotherName.Text = "<b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + "</b>";
                    lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["ENROLLNO"].ToString();
                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                   // lblbTotalCredit.Text = dsStudent.Tables[0].Rows[0]["TOTAL_CREDIT"].ToString();
                    //lblbTotalCredit.Text = dsStudent.Tables[0].Rows[0]["MIN_REG_CREDIT_LIMIT"].ToString();
                    //lblofferedcredit.Text = dsStudent.Tables[0].Rows[0]["MIN_REG_CREDIT_LIMIT"].ToString();
                   
                    ///lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();
                    tblInfo.Visible = true;
                    divCourses.Visible = true;
                    txtRcptNo.Text = dsStudent.Tables[0].Rows[0]["RECEIPT_NO"].ToString();

                    //ADDED BY AASHNA 25-11-2021
                    if (dsStudent.Tables[0].Rows[0]["MODULE_REGISTRATION_NO"].ToString() == "1")
                    {
                        lblselect.Text = "Rupees Per Subject";
                    }
                    if (dsStudent.Tables[0].Rows[0]["MODULE_REGISTRATION_NO"].ToString() == "2")
                    {
                        lblselect.Text = "Rupees Per Credit";
                    }
                    if (dsStudent.Tables[0].Rows[0]["MODULE_REGISTRATION_NO"].ToString() == "3")
                    {
                        lblselect.Text = "Rupees Per Module";
                    }
                    //hdnSelectedCourseFeeBkg.Value = dsStudent.Tables[0].Rows[0]["MIN_REG_CREDIT_LIMIT"].ToString();
                    //lblCourseFee.Text = dsStudent.Tables[0].Rows[0]["MIN_REG_CREDIT_LIMIT"].ToString();
                   // hdnTotalFee.Value = dsStudent.Tables[0].Rows[0]["MIN_REG_CREDIT_LIMIT"].ToString();
                    //lblTotalFee.Text = dsStudent.Tables[0].Rows[0]["MIN_REG_CREDIT_LIMIT"].ToString();

                    if (lblselect.Text == "Rupees Per Module")
                    {
                        lblCourseFee.Text=dsStudent.Tables[0].Rows[0]["MIN_REG_CREDIT_LIMIT"].ToString();
                        lblTotalFee.Text = dsStudent.Tables[0].Rows[0]["MIN_REG_CREDIT_LIMIT"].ToString();
                    }
                    //* AASHNA
                    if (dsStudent.Tables[0].Rows[0]["RECEIPT_DT"] != DBNull.Value)
                    {
                        txtRcptDt.Text = Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["RECEIPT_DT"].ToString()).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        txtRcptDt.Text = string.Empty;
                    }
                    if (dsStudent.Tables[0].Rows[0]["RECEIPT_AMT"] != DBNull.Value)
                    {
                        txtRcptAmt.Text = dsStudent.Tables[0].Rows[0]["RECEIPT_AMT"].ToString();
                    }
                    else
                    {
                        txtRcptAmt.Text = string.Empty;
                    }
                    txtBckLogRcptNo.Text = dsStudent.Tables[0].Rows[0]["BckLog_RECEIPT_NO"].ToString();
                    if (dsStudent.Tables[0].Rows[0]["BckLog_RECEIPT_DT"] != DBNull.Value)
                    {
                        txtBckLogRcptDt.Text = Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["BckLog_RECEIPT_DT"].ToString()).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        txtBckLogRcptDt.Text = string.Empty;
                    }
                    if (dsStudent.Tables[0].Rows[0]["BckLog_RECEIPT_AMT"] != DBNull.Value)
                    {
                        txtBckLogRcptAmt.Text = dsStudent.Tables[0].Rows[0]["BckLog_RECEIPT_AMT"].ToString();
                    }
                    else
                    {
                        txtBckLogRcptAmt.Text = string.Empty;
                    }
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
    //used this methods for registering student details
    private void BindStudentDetails()
    {
        try
        {
            lvCurrentSubjects.Visible = false;
            lvElectiveCourse.Visible = false;
            lvElectiveCourse2.Visible = false;
            lvElectiveCourse3.Visible = false;
            lvElectiveCourse4.Visible = false;

            //Check Whether the student is not eligible for registration or not
            string CountEligibility = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ACCEPTED = 1 AND  IDNO=" + ViewState["idno"] + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SESSIONNO <" + Convert.ToInt32(ddlSession.SelectedValue));
            ViewState["CountEligibility"] = CountEligibility;
            //Check current semester registered or not  //PREV_STATUS = 0 and 
            string countAlreadyReg = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND IDNO=" + ViewState["idno"] + "AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
            ViewState["countAlreadyReg"] = countAlreadyReg;
            //added by satish to re-strict student without promotion in next sem-19072018
            int semOddEven = Convert.ToInt32(objCommon.LookUp("ACD_SEMESTER", "ODD_EVEN", "SEMESTERNO=" + Convert.ToUInt32(lblSemester.ToolTip)));
            int sessionOddEven = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "ODD_EVEN", "SESSIONNO=" + ddlSession.SelectedValue));

            if (Convert.ToInt32(CountEligibility) > 0 && semOddEven != sessionOddEven)
            {
                if (Session["usertype"].ToString().Equals("2"))     //Student 
                {
                    objCommon.DisplayMessage("You are not eligible for current (" + lblSemester.Text + ") semester registration. Because registration for semester " + lblSemester.Text + " already exist in previous session or not eligible for Registration.\\nYou can register for backlog course if any. \\nIn case of any query please contact MIS/ Dean Academics.", this.Page);
                    btnSubmit.Visible = false; //added by satish-19072018
                    return;
                }
                else
                {
                    ////Check current semester registered or not  //PREV_STATUS = 0 and 

                    if (countAlreadyReg != "0")
                    {
                        if (Session["usertype"].ToString().Equals("2"))     //Student 
                        {
                            objCommon.DisplayMessage("Student Registration already done.", this.Page);
                        }
                        btnSubmit.Visible = true;
                        btnEditReg.Visible = false;
                        //btnPrintRegSlip.Enabled = true;
                        btnPrintRegSlip.Visible = false;
                    }
                    btnSubmit.Visible = false; //added by satish-19072018
                    lblmsg.Text = "Student with Registration No." + lblEnrollNo.Text.Trim() + " is not eligible for current (" + lblSemester.Text + ") semester registration. Because registration for semester " + lblSemester.Text + " already exist in previous session or you are not eligible for registration.<br />You can register for backlog courses if any.<br />In case of any query Please contact MIS/Dean Academics.";
                }
            }
            else
            {
                BindAvailableCourseList();
                BindElectiveCourseList();
                BindElectiveCourseList2();
                BindElectiveCourseList3();
                BindElectiveCourseList4();
            }

            if (Session["usertype"].ToString().Equals("2"))     //Student 
            {
                HideRegisterChk(lvCurrentSubjects);
            }
            else
            {
                BindStudAppliedCourseList();
            }

            if (countAlreadyReg != "0")
            {
                if (Session["usertype"].ToString().Equals("2"))     //Student 
                {
                    objCommon.DisplayMessage("Student Registration already done.", this.Page);
                }
                btnSubmit.Visible = false;
                btnPrintRegSlip.Enabled = true;
                btnEditReg.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.BindStudentDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //bind available course list in lvCurrentSubjects list view.
    private void BindAvailableCourseList()
    {
        try
        {
            //DataView dvStudent = new DataView(dsStudent.Tables[0]);
            //dvStudent.RowFilter = "SHIFT = " + ddlShift.SelectedValue;
            DataSet dsCurrCourses = null;
            //Show Current Semester Courses ..
            int Cure=1;
            dsCurrCourses = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO) LEFT JOIN ACD_STUDENT_RESULT R ON OC.COURSENO=R.COURSENO AND OC.SEMESTERNO=R.SEMESTERNO AND ISNULL(CANCEL,0)=0 AND R.IDNO=" + ViewState["idno"], "DISTINCT C.COURSENO", "C.CCODE,C.COURSE_NAME,C.SUBID,C.ELECT, C.CREDITS,S.SUBNAME,ISNULL(REGISTERED,0)REGISTERED,ISNULL(ACCEPTED,0)ACCEPTED,ISNULL(EXAM_REGISTERED,0)EXAM_REGISTERED, DBO.FN_DESC('SEMESTER',OC.SEMESTERNO)SEMESTER ", "OC.SCHEMENO = " + lblScheme.ToolTip + " AND OC.SEMESTERNO = " + lblSemester.ToolTip + "  AND OC.ELECT="+Cure+"AND ISNULL(C.ELECT,0)=0", "C.CCODE");
            //dsCurrCourses = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO) LEFT JOIN ACD_STUDENT_RESULT R ON OC.COURSENO=R.COURSENO AND OC.SEMESTERNO=R.SEMESTERNO AND ISNULL(CANCEL,0)=0 AND R.IDNO=" + ViewState["idno"], "DISTINCT C.COURSENO", "C.CCODE,C.COURSE_NAME,C.SUBID,C.ELECT, C.CREDITS,S.SUBNAME,ISNULL(REGISTERED,0)REGISTERED,ISNULL(ACCEPTED,0)ACCEPTED,ISNULL(EXAM_REGISTERED,0)EXAM_REGISTERED, DBO.FN_DESC('SEMESTER',OC.SEMESTERNO)SEMESTER ", "C.SCHEMENO = " + lblScheme.ToolTip + " AND OC.SEMESTERNO = " + lblSemester.ToolTip, "C.CCODE");
            if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                lvCurrentSubjects.DataSource = dsCurrCourses.Tables[0];
                lvCurrentSubjects.DataBind();
                lvCurrentSubjects.Visible = true;
                //make check box checked
                ListOperations(lvCurrentSubjects, dsCurrCourses.Tables[0]);
            }
            else
            {
                btnSubmit.Visible = false;
                lvCurrentSubjects.DataSource = null;
                lvCurrentSubjects.DataBind();
                lvCurrentSubjects.Visible = false;
                objCommon.DisplayMessage("No Mandatory Subject found in Allotted Scheme and Semester.\\nIn case of any query contact Dean Academics", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.BindAvailableCourseList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //added by satish - 06/10/2017
    //bind elective course list in lvElectiveCourse list view.
    private void BindElectiveCourseList()
    {
        try
        {
            DataSet dsCurrCourses = null;
            //Show Current Semester Courses ..
            int Elect = 2;
            dsCurrCourses = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO) LEFT JOIN ACD_STUDENT_RESULT R ON OC.COURSENO=R.COURSENO AND OC.SEMESTERNO=R.SEMESTERNO AND ISNULL(CANCEL,0)=0 AND R.IDNO=" + ViewState["idno"], "DISTINCT C.COURSENO", "C.CCODE,C.COURSE_NAME,C.SUBID,C.ELECT,C.CREDITS,S.SUBNAME, ISNULL(R.ACCEPTED,0) as ACCEPTED, ISNULL(R.EXAM_REGISTERED,0) as EXAM_REGISTERED, DBO.FN_DESC('SEMESTER',OC.SEMESTERNO)SEMESTER,C.GROUPNO ELECTGROUP,ISNULL(R.REGISTERED,0) REGISTERED ", "OC.SCHEMENO = " + lblScheme.ToolTip + " AND OC.SEMESTERNO = " + lblSemester.ToolTip + "AND OC.ELECT=2", "C.COURSENO");
            if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                lvElectiveCourse.DataSource = dsCurrCourses.Tables[0];
                lvElectiveCourse.DataBind();
                lvElectiveCourse.Visible = true;
                HFElectiveCount1.Value = "0";
            }
            else
            {
                //btnSubmit.Visible = false;
                lvElectiveCourse.DataSource = null;
                lvElectiveCourse.DataBind();
                lvElectiveCourse.Visible = false;
                HFElectiveCount1.Value = "1";
                //objCommon.DisplayMessage("No Elective-1 Course found in Allotted Scheme and Semester.\\nIn case of any query contact Dean Academics", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.BindElectiveCourseList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //added by satish - 20/12/2017
    //bind elective course list in lvElectiveCourse2 list view.
    private void BindElectiveCourseList2()
    {
        try
        {

            DataSet dsCurrCourses = null;
            //Show Current Semester Courses ..
            dsCurrCourses = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO) LEFT JOIN ACD_STUDENT_RESULT R ON OC.COURSENO=R.COURSENO AND OC.SEMESTERNO=R.SEMESTERNO AND ISNULL(CANCEL,0)=0 AND R.IDNO=" + ViewState["idno"], "DISTINCT C.COURSENO", "C.CCODE,C.COURSE_NAME,C.SUBID,C.ELECT,C.CREDITS,S.SUBNAME, ISNULL(R.ACCEPTED,0) as ACCEPTED, ISNULL(R.EXAM_REGISTERED,0) as EXAM_REGISTERED, DBO.FN_DESC('SEMESTER',OC.SEMESTERNO)SEMESTER,C.GROUPNO ELECTGROUP,ISNULL(R.REGISTERED,0) REGISTERED ", "OC.SCHEMENO = " + lblScheme.ToolTip + " AND OC.SEMESTERNO = " + lblSemester.ToolTip + "AND C.GROUPNO=2 AND C.ELECT=2", "C.COURSENO");
            if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                lvElectiveCourse2.DataSource = dsCurrCourses.Tables[0];
                lvElectiveCourse2.DataBind();
                lvElectiveCourse2.Visible = true;
                HFElectiveCount2.Value = "0";
            }
            else
            {
                //btnSubmit.Visible = false;
                lvElectiveCourse2.DataSource = null;
                lvElectiveCourse2.DataBind();
                lvElectiveCourse2.Visible = false;
                HFElectiveCount2.Value = "1";
                //objCommon.DisplayMessage("No Elective-2 Course found in Allotted Scheme and Semester.\\nIn case of any query contact Dean Academics", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.BindElectiveCourseList2() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //added by satish - 25122017
    //bind elective course list in lvElectiveCourse3 list view.
    private void BindElectiveCourseList3()
    {
        try
        {

            DataSet dsCurrCourses = null;
            //Show Current Semester Courses ..
            dsCurrCourses = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO) LEFT JOIN ACD_STUDENT_RESULT R ON OC.COURSENO=R.COURSENO AND OC.SEMESTERNO=R.SEMESTERNO AND ISNULL(CANCEL,0)=0 AND R.IDNO=" + ViewState["idno"], "DISTINCT C.COURSENO", "C.CCODE,C.COURSE_NAME,C.SUBID,C.ELECT,C.CREDITS,S.SUBNAME, ISNULL(R.ACCEPTED,0) as ACCEPTED, ISNULL(R.EXAM_REGISTERED,0) as EXAM_REGISTERED, DBO.FN_DESC('SEMESTER',OC.SEMESTERNO)SEMESTER,C.GROUPNO ELECTGROUP,ISNULL(R.REGISTERED,0) REGISTERED ", "OC.SCHEMENO = " + lblScheme.ToolTip + " AND OC.SEMESTERNO = " + lblSemester.ToolTip + "AND C.GROUPNO=3 AND C.ELECT=2", "C.COURSENO");
            if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                lvElectiveCourse3.DataSource = dsCurrCourses.Tables[0];
                lvElectiveCourse3.DataBind();
                lvElectiveCourse3.Visible = true;
                HFElectiveCount3.Value = "0";
            }
            else
            {
                // btnSubmit.Visible = false;
                lvElectiveCourse3.DataSource = null;
                lvElectiveCourse3.DataBind();
                lvElectiveCourse3.Visible = false;
                HFElectiveCount3.Value = "1";
                //objCommon.DisplayMessage("No Elective-3 Course found in Allotted Scheme and Semester.\\nIn case of any query contact Dean Academics", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.BindElectiveCourseList3() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //added by satish - 25122017
    //bind elective course list in lvElectiveCourse4 list view.
    private void BindElectiveCourseList4()
    {
        try
        {

            DataSet dsCurrCourses = null;
            //Show Current Semester Courses ..
            dsCurrCourses = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO) LEFT JOIN ACD_STUDENT_RESULT R ON OC.COURSENO=R.COURSENO AND OC.SEMESTERNO=R.SEMESTERNO AND ISNULL(CANCEL,0)=0 AND R.IDNO=" + ViewState["idno"], "DISTINCT C.COURSENO", "C.CCODE,C.COURSE_NAME,C.SUBID,C.ELECT,C.CREDITS,S.SUBNAME, ISNULL(R.ACCEPTED,0) as ACCEPTED, ISNULL(R.EXAM_REGISTERED,0) as EXAM_REGISTERED, DBO.FN_DESC('SEMESTER',OC.SEMESTERNO)SEMESTER,C.GROUPNO ELECTGROUP,ISNULL(R.REGISTERED,0) REGISTERED ", "OC.SCHEMENO = " + lblScheme.ToolTip + " AND OC.SEMESTERNO = " + lblSemester.ToolTip + "AND C.GROUPNO=7 AND C.ELECT=2", "C.COURSENO");
            if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                lvElectiveCourse4.DataSource = dsCurrCourses.Tables[0];
                lvElectiveCourse4.DataBind();
                lvElectiveCourse4.Visible = true;
                HFElectiveCount4.Value = "1";
            }
            else
            {
                //btnSubmit.Visible = false;
                lvElectiveCourse4.DataSource = null;
                lvElectiveCourse4.DataBind();
                lvElectiveCourse4.Visible = false;
                HFElectiveCount4.Value = "1";
                //objCommon.DisplayMessage("No Elective-3 Course found in Allotted Scheme and Semester.\\nIn case of any query contact Dean Academics", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.BindElectiveCourseList3() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void HideRegisterChk(ListView list)
    {
        foreach (ListViewDataItem item in list.Items)
        {
            CheckBox cbHeadReg = list.FindControl("cbHeadReg") as CheckBox;
            CheckBox chkRegister = item.FindControl("chkRegister") as CheckBox;

            //cbHeadReg.Visible = false;
            //chkRegister.Visible = false;
        }
    }
    #endregion
    ////showing the StudentsRegList report in rptCourseRegStudList.rpt file.
    protected void btnPrintStudList_Click(object sender, EventArgs e)
    {
        ShowReportStudentList("StudentsRegList", "rptCourseRegStudList.rpt");
    }
    //showing the report in pdf formate as per as  selection of report name  or file name.
    private void ShowReportStudentList(string reportTitle, string rptFileName)
    {
        try
        {
           // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
           // url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_TYPE=" + Session["usertype"].ToString() + ",@P_UA_NO=" + Session["userno"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    ////showing the Registration Slip report in rptPreRegslip_student.rpt file.
    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        ShowReport("RegistrationSlip", "rptPreRegslip_student.rpt");
    }
    //showing the report in pdf formate as per as  selection of report name  or file name.
    private void ShowReport(string reportTitle, string rptFileName)
    {
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int idno = Convert.ToInt32(lblName.ToolTip);
        string CODE = Session["colcode"].ToString();
        string CODENO =Session["userno"].ToString();
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";

            string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno; 
            //+ ",@UserName=" + Session["username"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //showing the report in pdf formate as per as  selection of report name  or file name.
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

    #region Faculty Advisor Accepting Student Registration
    //used for Registration
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            StudentRegistration objSRegist = new StudentRegistration();

            string idnos = string.Empty;
            foreach (ListViewDataItem dataItem in lvStudent.Items)
            {
                idnos += (dataItem.FindControl("lblIDNO") as Label).ToolTip + "$";
            }

            CustomStatus cs = (CustomStatus)objSRegist.UdpateRegistrationByHOD(Convert.ToInt32(ddlSessionReg.SelectedValue), idnos, Convert.ToInt32(Session["userno"].ToString()), ViewState["ipAddress"].ToString());
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage("Registration for Department till date Successfull!", this.Page);
                this.BindStudentList();
            }
            else
                objCommon.DisplayMessage("Registration Failed!", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_courseRegistration.btnRegister_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //bind student registration list in lvStudent list view.
    private void BindStudentList()
    {
        try
        {
            if (ddlSessionReg.SelectedValue == "0")
            {
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                btnRegister.Enabled = false;
                btnPrintStudList.Visible = false;
                return;
            }

            DataSet dsStudent = objSReg.GetCourseRegStudentList_Stud(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["dec"]), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSessionReg.SelectedValue));
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    lvStudent.DataSource = dsStudent.Tables[0];
                    lvStudent.DataBind();
                    btnRegister.Enabled = true;
                    btnPrintStudList.Visible = true;
                }
                else
                {
                    // objCommon.DisplayMessage("Students Not Registered for selected Session!", this.Page);
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    btnRegister.Enabled = false;
                    btnPrintStudList.Visible = false;
                }
            }
            else
            {
                // objCommon.DisplayMessage("Students Not Registered for selected Session!", this.Page);
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                btnRegister.Enabled = false;
                btnPrintStudList.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_courseRegistration.BindStudentList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //bind student applied course list in lvCurrentSubjects list view.
    private void BindStudAppliedCourseList()
    {
        try
        {
            StudentRegistration objSRegist = new StudentRegistration();
            DataSet dsOfferedCourses = null;

            dsOfferedCourses = objCommon.FillDropDown("ACD_STUDENT_RESULT SR", "DISTINCT SR.COURSENO", "SR.CCODE, SR.SEMESTERNO, SR.EXAM_REGISTERED", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.IDNO = " + ViewState["idno"], "SR.CCODE");

            if (dsOfferedCourses != null)
            {
                if (dsOfferedCourses.Tables.Count > 0)  /// && dsOfferedCourses.Tables[0].Rows.Count > 0
                {
                    ListOperations(lvCurrentSubjects, dsOfferedCourses.Tables[0]);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_courseRegistration.BindStudAppliedCourseList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //method to check /uncheck check box on load
    private void ListOperations(ListView list, DataTable dt)
    {
        foreach (ListViewDataItem item in list.Items)
        {
            CheckBox cbHead = list.FindControl("cbHead") as CheckBox;
            CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
            string lblCCode = (item.FindControl("lblCCode") as Label).ToolTip;

            if (!(Session["RegStatus"].ToString() == "0"))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (lblCCode == dt.Rows[i]["courseno"].ToString())
                    {
                        if (dt.Rows[i]["ACCEPTED"].ToString() == "1")
                        {
                            chkAccept.Checked = true;
                            chkAccept.Enabled = false;
                            cbHead.Enabled = false;
                        }
                        else
                        {
                            chkAccept.Checked = true;
                            cbHead.Checked = true;
                        }
                        //if (dt.Rows[i]["EXAM_REGISTERED"].ToString() == "1")
                        //{
                        //    chkAccept.Enabled = false;
                        //    cbHead.Enabled = false;
                        //}
                    }
                }

                cbHead.Enabled = false;
                chkAccept.Enabled = false;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (lblCCode == dt.Rows[i]["courseno"].ToString())
                    {
                        chkAccept.Checked = true;
                        cbHead.Checked = true;
                        chkAccept.Enabled = false;
                        cbHead.Enabled = false;
                    }
                }
            }
        }
    }

    //used for check register 
    protected void btnEditReg_Click(object sender, EventArgs e)
    {
        //added by satish for elective - 1 courses
        foreach (ListViewDataItem item in lvElectiveCourse.Items)
        {
            CheckBox chkRegister = item.FindControl("chkAccept") as CheckBox;
            chkRegister.Enabled = true;
        }

        btnSubmit.Visible = true;
        btnEditReg.Visible = false;
        btnPrintRegSlip.Visible = false;
    }
    //check if label value true then set label style color green else false set label color red
    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label lbl = e.Item.FindControl("lblStatus") as Label;
        if (lbl.Text.ToLower() == "registered")
        {
            lbl.Style.Add("color", "Green");
        }
        else
        {
            lbl.Style.Add("color", "Red");
        }
    }

    #endregion

    #region Not In Use Payment Related
    //showing the report in pdf formate as per as  selection of report name  or file name.
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
    //used for getting the parameter
    private string GetReportParameters(int studentNo, int dcrNo, string copyNo)
    {
        string param = "@P_IDNO=" + studentNo.ToString() + ",@P_DCRNO=" + dcrNo + ",CopyNo=" + copyNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;
    }


    #endregion

    protected void lvElectiveCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (Convert.ToInt32(ViewState["countAlreadyReg"]) > 0)    //if (count != "0")
        {
            CheckBox chk = e.Item.FindControl("chkAccept") as CheckBox;
            chk.Enabled = false;
        }
    }

    protected void lvElectiveCourse2_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (Convert.ToInt32(ViewState["countAlreadyReg"]) > 0)    //if (count != "0")
        {
            CheckBox chk = e.Item.FindControl("chkAccept") as CheckBox;
            chk.Enabled = false;
        }
    }

    protected void lvElectiveCourse3_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (Convert.ToInt32(ViewState["countAlreadyReg"]) > 0)    //if (count != "0")
        {
            CheckBox chk = e.Item.FindControl("chkAccept") as CheckBox;
            chk.Enabled = false;
        }
    }

    protected void lvElectiveCourse4_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (Convert.ToInt32(ViewState["countAlreadyReg"]) > 0)    //if (count != "0")
        {
            CheckBox chk = e.Item.FindControl("chkAccept") as CheckBox;
            chk.Enabled = false;
        }
    }

}


