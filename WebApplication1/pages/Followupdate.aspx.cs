using System.Linq;
using System.Web;
using System.Xml.Linq;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Web.Configuration;

using System.Text;

using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

using System.Net;
using System.Collections.Generic;
using System.Net.Mail;

using EASendMail;

public partial class ACADEMIC_Followupdate : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController stud = new StudentController();
    UserController user = new UserController();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
              
                    gettofdaylistview();
                    BindData();
                    //getupcominglistview();
                }
             
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
                Response.Redirect("~/notauthorized.aspx?page=Followupdate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Followupdate.aspx");
        }
    }
    private void BindData()
    {
        try
        {
            
         objCommon.FillDropDownList(ddlLeadStatus, "ACD_LEAD_STAGE", "LEADNO", "LEAD_STAGE_NAME", "LEADNO > 0", "LEADNO");//DEPTCODE
               

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    private void gettofdaylistview()
    {
         try
        {
              DataSet ds = null;
              ds = stud.GETTODAYSDATE(Convert.ToInt32(Session["userno"]));
            //DataSet ds = objCommon.FillDropDown("ACD_LEAD_STUDENT_ENQUIRY_FOLLOWUP F INNER JOIN ACD_USER_REGISTRATION R  ON (R.USERNO=F.USER_NO)", " DISTINCT SUBSTRING(CAST (F.NEXTFOLLOUP_DATE AS VARCHAR(50)),1,11)as date,R.EMAILID,R.USERNO,R.USERNAME,R.MOBILENO,R.FIRSTNAME +  ' ' + R.LASTNAME  AS NAME", "", "CAST(NEXTFOLLOUP_DATE AS DATE) = CAST(GETDATE() AS DATE)AND COMPLETED_DATE=0", "");
            if (ds.Tables[0].Rows.Count > 0)                                                                                                                                                                                                                                                                                                                         
            {
                btnsubmit.Visible = true;
                btnTodayEmail.Visible = true;
              //  btnCancel.Visible = true;
                pnllst.Visible = true;
                lvtodaysdate.DataSource = ds;
                lvtodaysdate.DataBind();

            }
            else
            {
                //objCommon.DisplayMessage(this.updfollowup, "No Record found.", this.Page);
                btnsubmit.Visible = false;
                btnTodayEmail.Visible = false;
               // btnCancel.Visible = false;
                pnllst.Visible = true;
                lvtodaysdate.DataSource = ds;              
                lvtodaysdate.DataBind();
               
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseCreation.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
    private void getupcominglistview()
    {
        try
        {
            DataSet ds = null;
            ds = stud.GETUPCOMINGDATE(Convert.ToInt32(Session["userno"]));
            //DataSet ds = objCommon.FillDropDown("ACD_LEAD_STUDENT_ENQUIRY_FOLLOWUP F INNER JOIN ACD_USER_REGISTRATION R  ON (R.USERNO=F.USER_NO)", "DISTINCT SUBSTRING(CAST (F.NEXTFOLLOUP_DATE AS VARCHAR(50)),1,11)as date,R.EMAILID,R.USERNAME,R.USERNO,R.MOBILENO,(R.FIRSTNAME  +  ''  +  R.LASTNAME )AS NAME", "", "CAST(NEXTFOLLOUP_DATE AS DATE) > CAST(GETDATE() AS DATE)AND COMPLETED_DATE=0", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                updupcoming.Visible = true;
                btnsave.Visible = true;
                btnemailoupc.Visible = true;
               // btncancelup.Visible = true;
                Panel1.Visible = true;
                lvupcomings.DataSource = ds;
                lvupcomings.DataBind();

            }

            else
            {
                //objCommon.DisplayMessage(this.updupcoming, "No Record found.", this.Page);
                updupcoming.Visible = true;
                Panel1.Visible = true;
                btnsave.Visible = false;
                btnemailoupc.Visible = false;
                //btncancelup.Visible = false;
                lvupcomings.DataSource = ds;
                lvupcomings.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseCreation.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void getOverduelistview()
    {
        try
        {
            DataSet ds = null;
            ds = stud.GETOVERDUEDATE(Convert.ToInt32(Session["userno"]));
            //DataSet ds = objCommon.FillDropDown("ACD_LEAD_STUDENT_ENQUIRY_FOLLOWUP F INNER JOIN ACD_USER_REGISTRATION R ON (R.USERNO=F.USER_NO)", "DISTINCT SUBSTRING(CAST (F.NEXTFOLLOUP_DATE AS VARCHAR(50)),1,11)as date,R.EMAILID,R.USERNAME,R.MOBILENO,R.USERNO,R.FIRSTNAME  + '  ' + R.LASTNAME  AS NAME", "", "CAST(NEXTFOLLOUP_DATE AS DATE) < CAST(GETDATE() AS DATE)AND COMPLETED_DATE=0 ", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Panel2.Visible = true;
                btnodsave.Visible = true;
                btnemailove.Visible = true;
                //btnodcancel.Visible = true;
                lvoverdue.DataSource = ds;
                lvoverdue.DataBind();

            }

            else
            {
               // objCommon.DisplayMessage(this.updoverdue, "No Record found.", this.Page);
                Panel2.Visible = true;
                btnodsave.Visible = false;
                btnemailove.Visible = false;
               // btnodcancel.Visible = false;
                lvoverdue.DataSource = null;
                lvoverdue.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseCreation.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void getCompletedlistview()
    {
        try
        {
            //DataSet ds = objCommon.FillDropDown("ACD_MARKEDAS_COMPLETED_FOLLOWUPDATE", "DISTINCT SUBSTRING(CAST (FOLLOWUP_DATE AS VARCHAR(50)),1,11)as date,EMAILID,USERNAME,MOBILENO,NAME", "", "", "");
            //DataSet ds = objCommon.FillDropDown("ACD_LEAD_STUDENT_ENQUIRY_FOLLOWUP F INNER JOIN ACD_USER_REGISTRATION R  ON (R.USERNO=F.USER_NO)", " DISTINCT SUBSTRING(CAST (F.NEXTFOLLOUP_DATE AS VARCHAR(50)),1,11)as date,R.EMAILID,R.USERNAME,R.USERNO,R.MOBILENO,R.FIRSTNAME  + '  ' +  R.LASTNAME  AS NAME", "", " COMPLETED_DATE=1 AND USER_NO LIKE '%' + CAST(@P_ADMIN_TYPE AS NVARCHAR(16)) + '%'", "");
            DataSet ds = null;
            ds = stud.GETCOMPLETEDATE(Convert.ToInt32(Session["userno"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
               
                Panel3.Visible = true;
                btnemailcompl.Visible = true;
                lvcomplete.DataSource = ds;
                lvcomplete.DataBind();

            }

            else
            {
                //objCommon.DisplayMessage(this.updcomplete, "No Record found.", this.Page);
                Panel3.Visible = true;
                btnemailcompl.Visible = false;
                lvcomplete.DataSource = ds;
                lvcomplete.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseCreation.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void getAlllistview()
    {
        try
        {
            DataSet ds = null;
            ds = stud.GETALLDATE(Convert.ToInt32(Session["userno"]));
            //DataSet ds = objCommon.FillDropDown("ACD_LEAD_STUDENT_ENQUIRY_FOLLOWUP F INNER JOIN ACD_USER_REGISTRATION R  ON (R.USERNO=F.USER_NO)", "DISTINCT SUBSTRING(CAST (F.NEXTFOLLOUP_DATE AS VARCHAR(50)),1,11)as date,R.EMAILID,R.USERNAME,R.USERNO,R.MOBILENO,R.FIRSTNAME + '  ' + R.LASTNAME  AS NAME", "", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Panel4.Visible = true;
                updall.Visible = true;
                btnBulkEmail.Visible = true;
                lvall.DataSource = ds;
                lvall.DataBind();
            }

            else
            {
                //objCommon.DisplayMessage(this.updall, "No Record found.", this.Page);
                Panel4.Visible = true;
                updall.Visible = true;
                btnBulkEmail.Visible = false;
                lvall.DataSource = ds;
                lvall.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseCreation.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lkfollowdate_Click(object sender, EventArgs e)
    {
        divlkfollowdate.Attributes.Add("class","active");
        divlkUpcoming.Attributes.Remove("class");
        divlkOverdue.Attributes.Remove("class");
        divlkCompleted.Attributes.Remove("class");
        divlkAll.Attributes.Remove("class");
        gettofdaylistview();
        todaydate.Visible = true;
        updupcoming.Visible = false;
        alldate.Visible = false;
        completedate.Visible = false;
        overduedate.Visible = false;
        upcomindate.Visible = false;


        lvoverdue.DataSource = null;
        lvoverdue.DataBind();

        lvcomplete.DataSource = null;
        lvcomplete.DataBind();

        lvall.DataSource = null;
        lvall.DataBind();

        lvupcomings.DataSource = null;
        lvupcomings.DataBind();

    }
    protected void lkUpcoming_Click(object sender, EventArgs e)
    {
        divlkUpcoming.Attributes.Add("class","active");
        divlkfollowdate.Attributes.Remove("class");
        divlkOverdue.Attributes.Remove("class");
        divlkCompleted.Attributes.Remove("class");
        divlkAll.Attributes.Remove("class");
        getupcominglistview();
        todaydate.Visible = false;
        upcomindate.Visible = true;
        alldate.Visible = false;
        completedate.Visible = false;
        overduedate.Visible = false;

        lvtodaysdate.DataSource = null;
        lvtodaysdate.DataBind();

        lvoverdue.DataSource = null;
        lvoverdue.DataBind();

        lvcomplete.DataSource = null;
        lvcomplete.DataBind();

        lvall.DataSource = null;
        lvall.DataBind();


      
        
    }
    protected void lkOverdue_Click(object sender, EventArgs e)
    {
        divlkUpcoming.Attributes.Remove("class");
        divlkfollowdate.Attributes.Remove("class");
        divlkOverdue.Attributes.Add("class", "active");
        divlkCompleted.Attributes.Remove("class");
        divlkAll.Attributes.Remove("class");

        getOverduelistview();

        todaydate.Visible = false;
        updupcoming.Visible = false;
        alldate.Visible = false;
        completedate.Visible = false;
        overduedate.Visible = true;
        upcomindate.Visible = false;

        lvtodaysdate.DataSource = null;
        lvtodaysdate.DataBind();


        lvcomplete.DataSource = null;
        lvcomplete.DataBind();

        lvall.DataSource = null;
        lvall.DataBind();
    }
    protected void lkCompleted_Click(object sender, EventArgs e)
    {
        divlkUpcoming.Attributes.Remove("class");
        divlkfollowdate.Attributes.Remove("class");
        divlkOverdue.Attributes.Remove("class");
        divlkCompleted.Attributes.Add("class", "active");
        divlkAll.Attributes.Remove("class");

        getCompletedlistview();

        todaydate.Visible = false;
        updupcoming.Visible = false;
        alldate.Visible = false;
        completedate.Visible = true;
        overduedate.Visible = false;
        upcomindate.Visible = false;

        lvtodaysdate.DataSource = null;
        lvtodaysdate.DataBind();

        lvoverdue.DataSource = null;
        lvoverdue.DataBind();

        lvupcomings.DataSource = null;
        lvupcomings.DataBind();

        lvall.DataSource = null;
        lvall.DataBind();
    }
    protected void lkAll_Click(object sender, EventArgs e)
    {
        divlkUpcoming.Attributes.Remove("class");
        divlkfollowdate.Attributes.Remove("class");
        divlkOverdue.Attributes.Remove("class");
        divlkCompleted.Attributes.Remove("class");
        divlkAll.Attributes.Add("class", "active");

        getAlllistview();

        todaydate.Visible = false;
        updupcoming.Visible = false;
        alldate.Visible = true;
        completedate.Visible = false;
        overduedate.Visible = false;
        upcomindate.Visible = false;

        lvtodaysdate.DataSource = null;
        lvtodaysdate.DataBind();

        lvoverdue.DataSource = null;
        lvoverdue.DataBind();

        lvupcomings.DataSource = null;
        lvupcomings.DataBind();

        lvcomplete.DataSource = null;
        lvcomplete.DataBind();

    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lvtodaysdate.DataSource = null;
        lvtodaysdate.DataBind();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string name = string.Empty;
        string username = string.Empty;
        string email = string.Empty;
        string mobile = string.Empty;
        string chkcomp = string.Empty;
        DateTime followupdtae;
        CustomStatus cs = 0;
        foreach (ListViewDataItem dataitem in lvtodaysdate.Items)
        {
            CheckBox chkBox = dataitem.FindControl("chk") as CheckBox;
            LinkButton lblusername = dataitem.FindControl("lblusername") as LinkButton;
            Label lblname = dataitem.FindControl("lblname") as Label;
            Label lblemail = dataitem.FindControl("lblemail") as Label;
            Label lblmobile = dataitem.FindControl("lblmobile") as Label;
            Label lbldate = dataitem.FindControl("lbldate") as Label;
            Label lblregdate = dataitem.FindControl("lblregdate") as Label;

            if (chkBox.Checked == true)
            {
                if (chkBox.Checked == true)
                {
                    chkcomp = "1";
                }
                else
                {
                    chkcomp = "0";
                }

                username = lblusername.ToolTip;
                name = lblname.ToolTip;
                email = lblemail.ToolTip;
                mobile = lblmobile.ToolTip;
                followupdtae = Convert.ToDateTime(lblregdate.ToolTip);
                int USERNO = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + username + "'"));
                cs = (CustomStatus)stud.InsertFollowDate(USERNO, Convert.ToInt32(Session["uano"]), Convert.ToString(username), Convert.ToString(name), Convert.ToString(email), Convert.ToString(mobile), Convert.ToDateTime(followupdtae), Convert.ToInt32(chkcomp));
            }
        }
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this.updfollowup, "Mark As Completed successfully.", this.Page);
            gettofdaylistview();
        }

    }
  
    protected void btnsave_Click(object sender, EventArgs e)
    {
        string name = string.Empty;
        string username = string.Empty;
        string email = string.Empty;
        string mobile = string.Empty;
        string chkcomp = string.Empty;
        DateTime followupdtae;
        CustomStatus cs = 0;
        foreach (ListViewDataItem dataitem in lvupcomings.Items)
        {
            CheckBox chkBox = dataitem.FindControl("upchk") as CheckBox;
            LinkButton lblusername = dataitem.FindControl("lbluuser") as LinkButton;
            Label lblname = dataitem.FindControl("lblupname") as Label;
            Label lblemail = dataitem.FindControl("lblupemail") as Label;
            Label lblmobile = dataitem.FindControl("lblupmobile") as Label;
            Label lbldate = dataitem.FindControl("lblupdate") as Label;
            Label lblregupdate = dataitem.FindControl("lblregupdate") as Label;

            if (chkBox.Checked == true)
            {
                if (chkBox.Checked == true)
                {
                    chkcomp = "1";
                }
                else
                {
                    chkcomp = "0";
                }             
                username = lblusername.ToolTip;
                name = lblname.ToolTip;
                email = lblemail.ToolTip;
                mobile = lblmobile.ToolTip;
                followupdtae = Convert.ToDateTime(lblregupdate.ToolTip);
                int USERNO = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + username + "'"));
                cs = (CustomStatus)stud.InsertFollowDate(USERNO, Convert.ToInt32(Session["uano"]), Convert.ToString(username), Convert.ToString(name), Convert.ToString(email), Convert.ToString(mobile), Convert.ToDateTime(followupdtae),Convert.ToInt32(chkcomp));
            }
        }
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this.updupcoming, "Mark As Completed successfully.", this.Page);
            getupcominglistview();
        }
    }
    protected void btncancelup_Click(object sender, EventArgs e)
    {
        lvupcomings.DataSource = null;
        lvupcomings.DataBind();
    }
   
    protected void btnodsave_Click(object sender, EventArgs e)
    {
        string name = string.Empty;
        string username = string.Empty;
        string email = string.Empty;
        string mobile = string.Empty;
        string chkcomp = string.Empty;
        DateTime followupdtae;
        CustomStatus cs = 0;
        foreach (ListViewDataItem dataitem in lvoverdue.Items)
        {
            CheckBox chkBox = dataitem.FindControl("odchk") as CheckBox;
            LinkButton lblusername = dataitem.FindControl("lbloduname") as LinkButton;
            Label lblname = dataitem.FindControl("lblodname") as Label;
            Label lblemail = dataitem.FindControl("lblodemail") as Label;
            Label lblmobile = dataitem.FindControl("lblodmobile") as Label;
            Label lbldate = dataitem.FindControl("lbloddate") as Label;
            Label lblregdate = dataitem.FindControl("lblregoverdate") as Label;

            if (chkBox.Checked == true)
            {
                if (chkBox.Checked == true)
                {
                    chkcomp = "1";
                }
                else
                {
                    chkcomp = "0";
                }             
                username = lblusername.ToolTip;
                name = lblname.ToolTip;
                email = lblemail.ToolTip;
                mobile = lblmobile.ToolTip;
                followupdtae = Convert.ToDateTime(lblregdate.ToolTip);
                int USERNO = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + username + "'"));
                cs = (CustomStatus)stud.InsertFollowDate(USERNO, Convert.ToInt32(Session["uano"]), Convert.ToString(username), Convert.ToString(name), Convert.ToString(email), Convert.ToString(mobile), Convert.ToDateTime(followupdtae),Convert.ToInt32(chkcomp));
            }
        }
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this.updoverdue, "Mark As Completed successfully.", this.Page);
            getOverduelistview();
        }

    }
    protected void btnodcancel_Click(object sender, EventArgs e)
    {
        lvoverdue.DataSource = null;
        lvoverdue.DataBind();
    }

    protected void lblusername_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton lnkApplicationNo = sender as LinkButton;
            string UserName = (lnkApplicationNo.ToolTip).ToString();
            int UserNo = int.Parse(lnkApplicationNo.CommandArgument);
            ViewState["STUDENT_USERNO"] = Convert.ToString(lnkApplicationNo.CommandArgument);
            string LeadApplNo = (lnkApplicationNo.Text).ToString();
            ViewState["STUDENT_USERNAME"] = LeadApplNo;
            if (UserName == "" || UserName == string.Empty)
            {
                objCommon.DisplayMessage(this.updfollowup, "student not Alloted", this.Page);
                return;
            }
            else
            {
                BindListViewApplication();
                BindData();
                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_CollegeMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    protected void btn_SubmitModal_Click(object sender, EventArgs e)
    {
        try
        {
            MappingController objmp = new MappingController();
            if (ddlLeadStatus.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.Page, "Please select Lead Status", this.Page);
                return;
            }
            if (txt_Remark.Text == "")
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Remarks", this.Page);
                return;
            }

            int enqueryNo = 0, action = 0;
            string Action = Convert.ToString(ViewState["action"]);
            if (Action == "Edit")
            {
                enqueryNo = Convert.ToInt32(hdnEnqueryno.Value);
                action = 2;
            }
            else
            {
                enqueryNo = 0;
                action = 1;
            }

          
            int ck1 = objmp.AddLeadStatus(Convert.ToInt32(ddlLeadStatus.SelectedValue), (ViewState["STUDENT_USERNO"].ToString()), Convert.ToInt32((Session["userno"]).ToString()), txt_Remark.Text, enqueryNo, action, (Convert.ToString(txtEndDate.Text)));
            if (ck1 == 1)
            {
                objCommon.DisplayMessage(this.updLeadStatus, "Record Saved Successfully", this.Page);

                //BindListView();
                BindListViewApplication();
                ClearControls();
                ViewState["action"] = "Add";
                

            }
            if (ck1 == 2)
            {
                objCommon.DisplayMessage(this.updLeadStatus, "Record Updated Successfully", this.Page);
                //BindListView();
                BindListViewApplication();
                ClearControls();
                ViewState["action"] = "Add";
               
            }
            else
            {
                objCommon.DisplayMessage(this.updfollowup, "Few or All Record already Exist", this.Page);
               // BindListView();
                ClearControls();
                ViewState["action"] = "Add";
               
            }
        }
        catch
        {
        }

    }
    private void BindListViewApplication()
    {
        try
        {
            MappingController objmp = new MappingController();
            // String uano = (objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME=" + Convert.ToString(ViewState["STUDENT_USERNO"]) + ""));
            DataSet ds = objmp.GetLeadFolloup(Convert.ToInt32(ViewState["STUDENT_USERNO"]));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables != null)
            {

                lvLeadList.DataSource = ds;
                lvLeadList.DataBind();

            }
            else
            {

                lvLeadList.DataSource = null;
                lvLeadList.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_dePT.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    public void ClearControls()
    {
        txt_Remark.Text = string.Empty;
        ddlLeadStatus.SelectedIndex = 0;
        txtEndDate.Text = string.Empty;
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        txtEndDate.Text = string.Empty;
        txt_Remark.Text = string.Empty;
        ddlLeadStatus.SelectedIndex = 0;
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        ViewState["action"] = "Edit";
        int enqueryno = int.Parse(btnEdit.CommandArgument);
        hdnEnqueryno.Value = Convert.ToString(enqueryno);
        DataSet leadstagedata = objCommon.FillDropDown("ACD_LEAD_STUDENT_ENQUIRY_FOLLOWUP EF INNER JOIN ACD_LEAD_STAGE LS ON (EF.ENQUIRYSTATUS=LS.LEADNO)", "ENQUIRYNO", "ENQUIRYNO,convert(varchar, NEXTFOLLOUP_DATE, 3)AS NEXTDATE,LEAD_UA_NO,ENQUIRYSTATUS,LEAD_STAGE_NAME,ENQUIRYSTATUS_DATE,REMARKS", "ENQUIRYNO=" + enqueryno + "", "");
        if (leadstagedata.Tables[0].Rows.Count > 0)
        {
            //lblLeadStatusName.Text = leadstagedata.Tables[0].Rows[0]["LEAD_STAGE_NAME"].ToString();
            ddlLeadStatus.SelectedValue = leadstagedata.Tables[0].Rows[0]["ENQUIRYSTATUS"].ToString();
            txt_Remark.Text = leadstagedata.Tables[0].Rows[0]["REMARKS"].ToString();
            txtEndDate.Text = leadstagedata.Tables[0].Rows[0]["NEXTDATE"].ToString();
        }
        else
        {

        }
    }  
    protected void lbluuser_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton lnkApplicationNo = sender as LinkButton;
            string UserName = (lnkApplicationNo.ToolTip).ToString();
            int UserNo = int.Parse(lnkApplicationNo.CommandArgument);
            ViewState["STUDENT_USERNO"] = Convert.ToString(lnkApplicationNo.CommandArgument);
            string LeadApplNo = (lnkApplicationNo.Text).ToString();
            ViewState["STUDENT_USERNAME"] = LeadApplNo;
            if (UserName == "" || UserName == string.Empty)
            {
                objCommon.DisplayMessage(this.updfollowup, "student not Alloted", this.Page);
                return;
            }
            else
            {
                BindListViewApplication();
                BindData();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_CollegeMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void alluser_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton lnkApplicationNo = sender as LinkButton;
            string UserName = (lnkApplicationNo.ToolTip).ToString();
            int UserNo = int.Parse(lnkApplicationNo.CommandArgument);
            ViewState["STUDENT_USERNO"] = Convert.ToString(lnkApplicationNo.CommandArgument);
            string LeadApplNo = (lnkApplicationNo.Text).ToString();
            ViewState["STUDENT_USERNAME"] = LeadApplNo;
            if (UserName == "" || UserName == string.Empty)
            {
                objCommon.DisplayMessage(this.updfollowup, "student not Alloted", this.Page);
                return;
            }
            else
            {
                BindListViewApplication();
                BindData();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_CollegeMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lbloduname_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton lnkApplicationNo = sender as LinkButton;
            string UserName = (lnkApplicationNo.ToolTip).ToString();
            int UserNo = int.Parse(lnkApplicationNo.CommandArgument);
            ViewState["STUDENT_USERNO"] = Convert.ToString(lnkApplicationNo.CommandArgument);
            string LeadApplNo = (lnkApplicationNo.Text).ToString();
            ViewState["STUDENT_USERNAME"] = LeadApplNo;
            if (UserName == "" || UserName == string.Empty)
            {
                objCommon.DisplayMessage(this.updfollowup, "student not Alloted", this.Page);
                return;
            }
            else
            {
                BindListViewApplication();
                BindData();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_CollegeMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSendBulkEmail_Click(object sender, EventArgs e)
    {
        try
        {
            divlkfollowdate.Attributes.Remove("class");
            string studentIds = string.Empty; string filename = ""; int count = 0;

            string folderPath = Server.MapPath("~/EmailUploadFile/");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            Session["EmailFileAttachemnt"] = fuAttachFile.FileName;
            if (Session["EmailFileAttachemnt"] != string.Empty || Session["EmailFileAttachemnt"] != "")
            {
                fuAttachFile.SaveAs(folderPath + Path.GetFileName(fuAttachFile.FileName));
            }

            DataSet dsUserContact = null;
            string message = "";
            dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));
            //if (ddlmainall.SelectedValue == "")
            //{
            //    objCommon.DisplayMessage(updEmailSend, "Please Add filter For sending Email. !!!", this.Page);
            //}
            //else
            //{
                //if (ddlmainall.SelectedValue == dsUserContact.Tables[1].Rows[0]["FILTER_TYPE_NO"].ToString())
                //{
                //    message = dsUserContact.Tables[1].Rows[1]["TEMPLATE"].ToString();
                //}

                //else
                //{
                //    message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                //}
           
            //}
            foreach (ListViewDataItem item in lvall.Items)
            {
                CheckBox chk = item.FindControl("chkemail") as CheckBox;
                LinkButton hdfAppli = item.FindControl("alluser") as LinkButton;
                Label hdfEmailid = item.FindControl("lblallemail") as Label;
                Label hdfirstname = item.FindControl("lblallname") as Label;
                //if (chk.Checked == false)
                //{

                //    objCommon.DisplayMessage(updEmailSend, "Please Select At least One User For Sending Email !!!", this.Page);
                //}
                if (chk.Checked == true)
                {
                    count++;
                   
                                  
                   // msg.Body = "<table width='500px' cellspacing='0' style='background-color: #F2F2F2'><tr><td>Dear " + firstname.ToString() + " " + lastname.ToString() + ',' + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Greetings from the LNMIIT …!!!</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>We've created a new LNMIIT user account for you. Please use the following application ID and password to sign in & complete the application.The application ID will be treated as your unique registration ID for all further proceedings.</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Your account details are :</td></tr></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Application ID : " + username.ToString() + "</td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Password : " + password.ToString() + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thanking you,</td></tr><tr><td></td></tr>Sincerely,<tr><td></td></tr><tr><td></td></tr><tr>Convener<td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>UG Admissions – 2014</td></tr><tr><td></td></tr><tr><td>This is an automated e-mail from an unattended mailbox. Please do not reply to this email.For any further communication please write to : <a href='ugadmissions@lnmiit.ac.in'>ugadmissions@lnmiit.ac.in</a></td></tr></table>";
                    //msg.Body = "<table width='500px' cellspacing='0' style='background-color: #F2F2F2'><tr><td>Dear " + firstname.ToString() + " " + lastname.ToString() + ',' + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Greetings from the LNMIIT. </td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thanks for registering with LNMIIT. </td></tr><tr><td></td></tr><tr><td></td></tr><tr><td >Use </td></tr></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Application ID : " + username.ToString() + "</td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Password : " + password.ToString() + "</td></tr><tr><td>for further processing.</td></tr><tr><td></td></tr><tr><td>Thanking you,</td></tr><tr><td></td></tr>Sincerely,<tr><td></td></tr><tr><td></td></tr><tr>Convener<td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>UG Admissions – 2015</td></tr><tr><td></td></tr><tr><td>This is an automated e-mail. Please do not reply to this email. For any further communication please write to : <a href='ugadmissions@lnmiit.ac.in'>ugadmissions@lnmiit.ac.in</a></td></tr></table>";
                    const string EmailTemplate = "<!DOCTYPE html><html><head><meta charset='utf-8'><meta name='viewport' content='width=device-width, initial-scale=1'><link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css' /><link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css' />" +
                       "<style>.email-template .email-header{background-color:#346CB0;text-align:center}.email-body{padding:30px 15px 0 15px;border:1px solid #ccc}.email-body p{font-size:16px;font-weight:500}ul.simply-list{list-style-type:none;padding:0;margin:0}ul.simply-list .list-group-item{padding:2px 15px;font-weight:600;font-size:16px;line-height:1.4;border:none;background-color:transparent}ul.simply-list.no-border .list-group-item{border:none}.strip{padding:20px 15px;background-color:#eee;margin:15px 0}.strip ul.simply-list li:first-child{padding-left:0}.d-flex{display:flex;-webkit-display:flex;-moz-display:flex;-o-display:flex;flex-direction:row}.d-flex.align-items-center{align-items:center}.note{color:red;font-weight:500;font-size:12px;margin-bottom:10px}.email-template .email-header img{width:85px;display:block;margin:auto}.email-template .email-header span{font-size:22px;font-weight:500;padding:10px 0}.email-info{margin-top:10px;padding:10px 0}.info-list span{font-size:14px;display:flex;align-items:center;font-weight:600;color:#666}.info-list span a{color:#666}.info-list span i{font-size:18px;margin-right:5px;color:#888}@media only screen and (max-width:767px){.email-template .email-header{padding:10px 10px;text-align:center}.email-template .email-header img{width:70px;display:block;margin:auto;margin-bottom:5px}.email-template .email-header span{font-size:22px;font-weight:500}.strip ul.simply-list li{padding-left:0}.info-list li{line-height:1.9}}</style>" +
                      "<style>.form-group{margin-bottom:15px}.btn-success{color:#fff;background-color:#5cb85c;border-color:#4cae4c}.btn-success.focus,.btn-success:focus{color:#fff;background-color:#449d44;border-color:#255625}.btn-success:hover{color:#fff;background-color:#449d44;border-color:#398439}.btn-success.active,.btn-success:active,.open>.dropdown-toggle.btn-success{color:#fff;background-color:#449d44;background-image:none;border-color:#398439}.btn-success.active.focus,.btn-success.active:focus,.btn-success.active:hover,.btn-success:active.focus,.btn-success:active:focus,.btn-success:active:hover,.open>.dropdown-toggle.btn-success.focus,.open>.dropdown-toggle.btn-success:focus,.open>.dropdown-toggle.btn-success:hover{color:#fff;background-color:#398439;border-color:#255625}.btn-success.disabled.focus,.btn-success.disabled:focus,.btn-success.disabled:hover,.btn-success[disabled].focus,.btn-success[disabled]:focus,.btn-success[disabled]:hover,fieldset[disabled] .btn-success.focus,fieldset[disabled] .btn-success:focus,fieldset[disabled] .btn-success:hover{background-color:#5cb85c;border-color:#4cae4c}.text-success{color:#3c763d}a.text-success:focus,a.text-success:hover{color:#2b542c}.btn{margin:.2em .1em;font-weight:500;font-size:1em;-webkit-font-smoothing:antialiased;-moz-osx-font-smoothing:grayscale;border:none;border-bottom:.15em solid #000;border-radius:3px;padding:.65em 1.3em}.btn-primary{border-color:#2a6496;background-image:linear-gradient(#428bca,#357ebd)}.btn-primary:hover{background:linear-gradient(#357ebd,#3071a9)}</style>" +

                                            "<div class='container-fluid' style='padding-top:15px'>" +
                                            "<div class='row'>" +
                                            "<div class='col-sm-8 col-md-6 col-md-offset-3 col-sm-offset-2'>" +
                                            "<div class='email-template'>" +
                                            "<div class='email-header'>" +
                        //"<img alt='logo' src='http://erptest.sliit.lk/IMAGES/SLIIT_logo.png' />" +
                                            "<span style='color:#fff;font-weight:bold'>Sri Lanka Institute of Information Technology</span>" +
                                            "</div>" +
                                            "<div class='email-body' style='clear-both'>#content</div>" +


                   "</div></div></div></div>" +
                   "</body></html>";
                    string message1 = "<h3>Dear <span></span>" + hdfirstname.Text + ", </h3>"
                    + "<div class='strip'><p>" + txtEmailMessage.Text + "</p>"
                    + "</div>" +
                    "<p class='note' style='margin-bottom:20px'>Note: This is system generated email. Please do not reply to this email.</p>";
                    string Mailbody = message1.ToString();
                    string nMailbody = EmailTemplate.Replace("#content", Mailbody);

                    if (rbselect.SelectedValue == "1")
                    {
                        message = nMailbody;
                    }

                    else
                    {

                        if (rbtamplate.SelectedValue == "1")
                        {
                            message = dsUserContact.Tables[1].Rows[1]["TEMPLATE"].ToString();
                        }
                        else if (rbtamplate.SelectedValue == "2")
                        {
                            message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                        }
                    }    

                    //message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                    filename = Convert.ToString(Session["EmailFileAttachemnt"]);

                    Execute(message, hdfEmailid.Text, txtEmailSubject.Text, hdfirstname.Text, hdfAppli.Text, filename, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
                    chk.Checked = false;
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(updEmailSend, "Please select atleast one check box for email send !!!", this.Page);
                txtEmailMessage.Text = "";
                txtEmailSubject.Text = "";
                Session["EmailFileAttachemnt"] = null;
                return;
            }
            else
            {
                objCommon.DisplayMessage(updEmailSend, "Email send Successfully !!!", this.Page);
                txtEmailMessage.Text = "";
                txtEmailSubject.Text = "";
                Session["EmailFileAttachemnt"] = null;
            }
        }
        catch (Exception ex)
        {

        }
    }
    static async Task Execute(string message, string toSendAddress, string Subject, string firstname, string username, string filename,string sendemail,string emailpass)
    {
        try
        {
            SmtpMail oMail = new SmtpMail("TryIt");

            oMail.From = sendemail;

            oMail.To = toSendAddress;

            oMail.Subject = Subject;

            oMail.HtmlBody = message;

            oMail.HtmlBody = oMail.HtmlBody.Replace("[UA_FULLNAME]", firstname.ToString());
            if (filename != string.Empty)
            {
                oMail.HtmlBody = System.Web.HttpContext.Current.Server.MapPath("~/EmailUploadFile/" + filename + "");
               
            }
            SmtpServer oServer = new SmtpServer("smtp.live.com");
            oServer.User = sendemail;
            oServer.Password = emailpass;

            oServer.Port = 587;

            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();

            oSmtp.SendMail(oServer, oMail);

            //Common objCommon = new Common();

            //DataSet dsconfig = null;
            //dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "SLIIT_EMAIL,COMPANY_EMAILSVCID,SENDGRID_USERNAME,SENDGRID_PWD,API_KEY_SENDGRID", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

        
            //var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["SLIIT_EMAIL"].ToString(), "SLIIT");
            //var toAddress = new MailAddress(toSendAddress, "");
            //var apiKey = dsconfig.Tables[0].Rows[0]["API_KEY_SENDGRID"].ToString();
            //var client = new SendGridClient(apiKey.ToString());
            //var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["SLIIT_EMAIL"].ToString(), "SLIIT");

            //var subject = Subject;
            //var to = new EmailAddress(toSendAddress, "");
            //var plainTextContent = "";
            //var htmlContent = nMailbody;
            //var file = "";
            //if (filename != string.Empty)
            //{
            //    string AttcPath = System.Web.HttpContext.Current.Server.MapPath("~/EmailUploadFile/" + filename + "");
            //    var bytes = File.ReadAllBytes(AttcPath);
            //    file = Convert.ToBase64String(bytes);
            //}
            //MailMessage msg = new MailMessage();
            //SmtpClient smtp = new SmtpClient();
            //var msgs = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //if (filename != string.Empty)
            //{
            //    msgs.AddAttachment("" + filename + "", file);
            //}
            //var response = await client.SendEmailAsync(msgs);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnApplyFilter_Click(object sender, EventArgs e)
    {
        try
        {
            divlkUpcoming.Attributes.Remove("class");
            MappingController objmp = new MappingController();
            if (ddlMainLeadLabel.SelectedIndex == 0)
            {

                if (ddlMainLeadLabel.SelectedIndex == 0 || ddlMainLeadLabel.SelectedValue == "")
                {
                    objCommon.DisplayMessage(this.UpdatePanel2, "Please Select Main Heading.", this.Page);
                    return;
                }
                if (Convert.ToString(ddlMainLeadLabel.SelectedValue) == "1")
                {
                    DataSet ds = objmp.GETFILTER(Convert.ToInt32(Session["userno"]), 1);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        UpdatePanel2.Visible = true;
                        btnsubmit.Visible = true;
                        btnTodayEmail.Visible = true;
                        // btncancelup.Visible = true;
                        pnllst.Visible = true;
                        lvtodaysdate.DataSource = ds;
                        lvtodaysdate.DataBind();

                    }
                    else
                    {
                        UpdatePanel2.Visible = true;
                        btnsubmit.Visible = false;
                        btnTodayEmail.Visible = false;
                        // btncancelup.Visible = true;
                        pnllst.Visible = true;
                        lvtodaysdate.DataSource = ds;
                        lvtodaysdate.DataBind();
                    }
                }

                else if(Convert.ToString(ddlMainLeadLabel.SelectedValue) == "2")
                {
                    DataSet ds = objmp.GETFILTER(Convert.ToInt32(Session["userno"]), 2);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        UpdatePanel2.Visible = true;
                        btnsubmit.Visible = true;
                        btnTodayEmail.Visible = true;
                        // btncancelup.Visible = true;
                        pnllst.Visible = true;
                        lvtodaysdate.DataSource = ds;
                        lvtodaysdate.DataBind();

                    }
                    else
                    {
                        UpdatePanel2.Visible = true;
                        btnsubmit.Visible = false;
                        btnTodayEmail.Visible = false;
                        // btncancelup.Visible = true;
                        pnllst.Visible = true;
                        lvtodaysdate.DataSource = ds;
                        lvtodaysdate.DataBind();
                    }
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_adminmaster.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }         
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {

        ddlMainLeadLabel.SelectedValue = "0";

    }

    protected void btnfilterupc_Click(object sender, EventArgs e)
    {
        try
        {
            divlkfollowdate.Attributes.Remove("class");
            MappingController objmp = new MappingController();
           
                if (ddlmainleadupc.SelectedIndex == 0 || ddlmainleadupc.SelectedValue == "")
                {
                    objCommon.DisplayMessage(this.UpdatePanel4, "Please Select Main Heading.", this.Page);
                    return;
                }
                if (Convert.ToString(ddlmainleadupc.SelectedValue) == "1")
                {
                    DataSet ds = objmp.GETFILTERUPC(Convert.ToInt32(Session["userno"]), 1);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        UpdatePanel4.Visible = true;
                        btnsave.Visible = true;
                        btnemailoupc.Visible = true;
                        // btncancelup.Visible = true;
                        Panel1.Visible = true;
                        lvupcomings.DataSource = ds;
                        lvupcomings.DataBind();

                    }
                    else
                    {
                        UpdatePanel4.Visible = true;
                        btnsave.Visible = false;
                        btnemailoupc.Visible = false;
                        // btncancelup.Visible = true;
                        Panel1.Visible = true;
                        lvupcomings.DataSource = ds;
                        lvupcomings.DataBind();
                    }
                }
                else if (Convert.ToString(ddlmainleadupc.SelectedValue) == "2")
                {
                    DataSet ds = objmp.GETFILTERUPC(Convert.ToInt32(Session["userno"]), 2);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        UpdatePanel4.Visible = true;
                        btnsave.Visible = true;
                        btnemailoupc.Visible = true;
                        // btncancelup.Visible = true;
                        Panel1.Visible = true;
                        lvupcomings.DataSource = ds;
                        lvupcomings.DataBind();

                    }
                    else
                    {
                        UpdatePanel4.Visible = true;
                        btnsave.Visible = false;
                        btnemailoupc.Visible = false;
                        // btncancelup.Visible = true;
                        Panel1.Visible = true;
                        lvupcomings.DataSource = ds;
                        lvupcomings.DataBind();
                    }
                }
            }
        
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_adminmaster.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnclearupc_Click(object sender, EventArgs e)
    {
        divlkfollowdate.Attributes.Remove("class");
        ddlmainleadupc.SelectedValue = "0";
    }
    protected void btnfilover_Click(object sender, EventArgs e)
    {
        try
        {
            divlkfollowdate.Attributes.Remove("class");
            MappingController objmp = new MappingController();

            if (ddlmainover.SelectedIndex == 0 || ddlmainover.SelectedValue == "")
            {
                objCommon.DisplayMessage(this.UpdatePanel6, "Please Select Main Heading.", this.Page);
                return;
            }
            if (Convert.ToString(ddlmainover.SelectedValue) == "1")
            {
                DataSet ds = objmp.GETFILTEROVED(Convert.ToInt32(Session["userno"]), 1);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    UpdatePanel6.Visible = true;
                    btnsave.Visible = true;
                    btnemailove.Visible = true;
                    // btncancelup.Visible = true;
                    Panel2.Visible = true;
                    lvoverdue.DataSource = ds;
                    lvoverdue.DataBind();

                }
                else
                {
                    UpdatePanel6.Visible = true;
                    btnsave.Visible = false;
                    btnemailove.Visible = false;
                    // btncancelup.Visible = true;
                    Panel2.Visible = true;
                    lvoverdue.DataSource = ds;
                    lvoverdue.DataBind();
                }
            }
            else if (Convert.ToString(ddlmainover.SelectedValue) == "2")
            {
                DataSet ds = objmp.GETFILTEROVED(Convert.ToInt32(Session["userno"]), 2);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    UpdatePanel6.Visible = true;
                    btnsave.Visible = true;
                    btnemailove.Visible = true;
                    // btncancelup.Visible = true;
                    Panel2.Visible = true;
                    lvoverdue.DataSource = ds;
                    lvoverdue.DataBind();

                }
                else
                {
                    UpdatePanel6.Visible = true;
                    btnsave.Visible = false;
                    btnemailove.Visible = false;
                    // btncancelup.Visible = true;
                    Panel2.Visible = true;
                    lvoverdue.DataSource = ds;
                    lvoverdue.DataBind();
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_adminmaster.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void btncanover_Click(object sender, EventArgs e)
    {
        divlkfollowdate.Attributes.Remove("class");
        ddlmainover.SelectedValue = "0";
    }
    protected void btnsubcomp_Click(object sender, EventArgs e)
    {
        try
        {
            divlkfollowdate.Attributes.Remove("class");
            MappingController objmp = new MappingController();

            if (ddlmaincompl.SelectedIndex == 0 || ddlmaincompl.SelectedValue == "")
            {
                objCommon.DisplayMessage(this.UpdatePanel33, "Please Select Main Heading.", this.Page);
                return;
            }
            if (Convert.ToString(ddlmaincompl.SelectedValue) == "1")
            {
                DataSet ds = objmp.GETFILTERCOMP(Convert.ToInt32(Session["userno"]), 1);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    UpdatePanel33.Visible = true;
                    btnemailcompl.Visible = true;
                   // btnsave.Visible = true;
                    // btncancelup.Visible = true;
                    Panel3.Visible = true;
                    lvcomplete.DataSource = ds;
                    lvcomplete.DataBind();

                }
                else
                {
                    UpdatePanel33.Visible = true;
                    btnemailcompl.Visible = false;
                   // btnsave.Visible = false;
                    // btncancelup.Visible = true;
                    Panel3.Visible = true;
                    lvcomplete.DataSource = ds;
                    lvcomplete.DataBind();
                }
            }
            else if (Convert.ToString(ddlmaincompl.SelectedValue) == "2")
            {
                DataSet ds = objmp.GETFILTERCOMP(Convert.ToInt32(Session["userno"]), 2);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    UpdatePanel33.Visible = true;
                    btnemailcompl.Visible = true;
                    // btnsave.Visible = true;
                    // btncancelup.Visible = true;
                    Panel3.Visible = true;
                    lvcomplete.DataSource = ds;
                    lvcomplete.DataBind();

                }
                else
                {
                    UpdatePanel33.Visible = true;
                    btnemailcompl.Visible = false;
                    // btnsave.Visible = false;
                    // btncancelup.Visible = true;
                    Panel3.Visible = true;
                    lvcomplete.DataSource = ds;
                    lvcomplete.DataBind();
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_adminmaster.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnclearcomp_Click(object sender, EventArgs e)
    {
        divlkfollowdate.Attributes.Remove("class");
        ddlmaincompl.SelectedValue = "0";
    }
    protected void btnsuball_Click(object sender, EventArgs e)
    {
        try
        {
            divlkfollowdate.Attributes.Remove("class");
            MappingController objmp = new MappingController();

            if (ddlmainall.SelectedIndex == 0 || ddlmainall.SelectedValue == "")
            {
                objCommon.DisplayMessage(this.UpdatePanel44, "Please Select Main Heading.", this.Page);
                return;
            }
            if (Convert.ToString(ddlmainall.SelectedValue) == "1")
            {
                DataSet ds = objmp.GETFILTERALL(Convert.ToInt32(Session["userno"]), 1);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    UpdatePanel44.Visible = true;
                    btnBulkEmail.Visible = true;
                    // btnsave.Visible = true;
                    // btncancelup.Visible = true;
                    Panel4.Visible = true;
                    lvall.DataSource = ds;
                    lvall.DataBind();

                }
                else
                {
                    UpdatePanel44.Visible = true;
                    btnBulkEmail.Visible = false;
                    // btnsave.Visible = false;
                    // btncancelup.Visible = true;
                    Panel4.Visible = true;
                    lvall.DataSource = ds;
                    lvall.DataBind();
                }
            }
            else if (Convert.ToString(ddlmainall.SelectedValue) == "2")
            {
                DataSet ds = objmp.GETFILTERALL(Convert.ToInt32(Session["userno"]), 2);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    UpdatePanel44.Visible = true;
                    btnBulkEmail.Visible = true;
                    // btnsave.Visible = true;
                    // btncancelup.Visible = true;
                    Panel4.Visible = true;
                    lvall.DataSource = ds;
                    lvall.DataBind();

                }
                else
                {
                    UpdatePanel44.Visible = true;
                    btnBulkEmail.Visible = false;
                    // btnsave.Visible = false;
                    // btncancelup.Visible = true;
                    Panel4.Visible = true;
                    lvall.DataSource = ds;
                    lvall.DataBind();
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_adminmaster.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void clearall_Click(object sender, EventArgs e)
    {
        divlkfollowdate.Attributes.Remove("class");
        ddlmainall.SelectedValue = "0";

    }
   
   
    protected void btnemailtoday_Click(object sender, EventArgs e)
    {
        try
        {
            
            string studentIds = string.Empty; string filename = ""; int count = 0;

            string folderPath = Server.MapPath("~/EmailUploadFile/");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            Session["EmailFileAttachemnt"] = fuAttachFile.FileName;
            if (Session["EmailFileAttachemnt"] != string.Empty || Session["EmailFileAttachemnt"] != "")
            {
                fuAttachFile.SaveAs(folderPath + Path.GetFileName(fuAttachFile.FileName));
            }

            DataSet dsUserContact = null;
            dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));
            foreach (ListViewDataItem item in lvtodaysdate.Items)
            {
                CheckBox chk = item.FindControl("chktodayemail") as CheckBox;
                LinkButton hdfAppli = item.FindControl("lblusername") as LinkButton;
                Label hdfEmailid = item.FindControl("lblemail") as Label;
                Label hdfirstname = item.FindControl("lblname") as Label;
                //if (chk.Checked == false)
                //{

                //    objCommon.DisplayMessage(updEmailSend, "Please Select At least One User For Sending Email !!!", this.Page);
                //}
                if (chk.Checked == true)
                {
                    count++;
                    //FOR MANISH : ERR: AT HTML TAGS :
                    // msg.Body = "<table width='500px' cellspacing='0' style='background-color: #F2F2F2'><tr><td>Dear " + firstname.ToString() + " " + lastname.ToString() + ',' + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Greetings from the LNMIIT …!!!</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>We've created a new LNMIIT user account for you. Please use the following application ID and password to sign in & complete the application.The application ID will be treated as your unique registration ID for all further proceedings.</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Your account details are :</td></tr></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Application ID : " + username.ToString() + "</td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Password : " + password.ToString() + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thanking you,</td></tr><tr><td></td></tr>Sincerely,<tr><td></td></tr><tr><td></td></tr><tr>Convener<td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>UG Admissions – 2014</td></tr><tr><td></td></tr><tr><td>This is an automated e-mail from an unattended mailbox. Please do not reply to this email.For any further communication please write to : <a href='ugadmissions@lnmiit.ac.in'>ugadmissions@lnmiit.ac.in</a></td></tr></table>";
                    // msg.Body = "<table width='500px' cellspacing='0' style='background-color: #F2F2F2'><tr><td>Dear " + firstname.ToString() + " " + lastname.ToString() + ',' + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Greetings from the LNMIIT. </td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thanks for registering with LNMIIT. </td></tr><tr><td></td></tr><tr><td></td></tr><tr><td >Use </td></tr></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Application ID : " + username.ToString() + "</td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Password : " + password.ToString() + "</td></tr><tr><td>for further processing.</td></tr><tr><td></td></tr><tr><td>Thanking you,</td></tr><tr><td></td></tr>Sincerely,<tr><td></td></tr><tr><td></td></tr><tr>Convener<td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>UG Admissions – 2015</td></tr><tr><td></td></tr><tr><td>This is an automated e-mail. Please do not reply to this email. For any further communication please write to : <a href='ugadmissions@lnmiit.ac.in'>ugadmissions@lnmiit.ac.in</a></td></tr></table>";
                    const string EmailTemplate = "<!DOCTYPE html><html><head><meta charset='utf-8'><meta name='viewport' content='width=device-width, initial-scale=1'><link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css' /><link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css' />" +
                       "<style>.email-template .email-header{background-color:#346CB0;text-align:center}.email-body{padding:30px 15px 0 15px;border:1px solid #ccc}.email-body p{font-size:16px;font-weight:500}ul.simply-list{list-style-type:none;padding:0;margin:0}ul.simply-list .list-group-item{padding:2px 15px;font-weight:600;font-size:16px;line-height:1.4;border:none;background-color:transparent}ul.simply-list.no-border .list-group-item{border:none}.strip{padding:20px 15px;background-color:#eee;margin:15px 0}.strip ul.simply-list li:first-child{padding-left:0}.d-flex{display:flex;-webkit-display:flex;-moz-display:flex;-o-display:flex;flex-direction:row}.d-flex.align-items-center{align-items:center}.note{color:red;font-weight:500;font-size:12px;margin-bottom:10px}.email-template .email-header img{width:85px;display:block;margin:auto}.email-template .email-header span{font-size:22px;font-weight:500;padding:10px 0}.email-info{margin-top:10px;padding:10px 0}.info-list span{font-size:14px;display:flex;align-items:center;font-weight:600;color:#666}.info-list span a{color:#666}.info-list span i{font-size:18px;margin-right:5px;color:#888}@media only screen and (max-width:767px){.email-template .email-header{padding:10px 10px;text-align:center}.email-template .email-header img{width:70px;display:block;margin:auto;margin-bottom:5px}.email-template .email-header span{font-size:22px;font-weight:500}.strip ul.simply-list li{padding-left:0}.info-list li{line-height:1.9}}</style>" +
                      "<style>.form-group{margin-bottom:15px}.btn-success{color:#fff;background-color:#5cb85c;border-color:#4cae4c}.btn-success.focus,.btn-success:focus{color:#fff;background-color:#449d44;border-color:#255625}.btn-success:hover{color:#fff;background-color:#449d44;border-color:#398439}.btn-success.active,.btn-success:active,.open>.dropdown-toggle.btn-success{color:#fff;background-color:#449d44;background-image:none;border-color:#398439}.btn-success.active.focus,.btn-success.active:focus,.btn-success.active:hover,.btn-success:active.focus,.btn-success:active:focus,.btn-success:active:hover,.open>.dropdown-toggle.btn-success.focus,.open>.dropdown-toggle.btn-success:focus,.open>.dropdown-toggle.btn-success:hover{color:#fff;background-color:#398439;border-color:#255625}.btn-success.disabled.focus,.btn-success.disabled:focus,.btn-success.disabled:hover,.btn-success[disabled].focus,.btn-success[disabled]:focus,.btn-success[disabled]:hover,fieldset[disabled] .btn-success.focus,fieldset[disabled] .btn-success:focus,fieldset[disabled] .btn-success:hover{background-color:#5cb85c;border-color:#4cae4c}.text-success{color:#3c763d}a.text-success:focus,a.text-success:hover{color:#2b542c}.btn{margin:.2em .1em;font-weight:500;font-size:1em;-webkit-font-smoothing:antialiased;-moz-osx-font-smoothing:grayscale;border:none;border-bottom:.15em solid #000;border-radius:3px;padding:.65em 1.3em}.btn-primary{border-color:#2a6496;background-image:linear-gradient(#428bca,#357ebd)}.btn-primary:hover{background:linear-gradient(#357ebd,#3071a9)}</style>" +

                                            "<div class='container-fluid' style='padding-top:15px'>" +
                                            "<div class='row'>" +
                                            "<div class='col-sm-8 col-md-6 col-md-offset-3 col-sm-offset-2'>" +
                                            "<div class='email-template'>" +
                                            "<div class='email-header'>" +
                        //"<img alt='logo' src='http://erptest.sliit.lk/IMAGES/SLIIT_logo.png' />" +
                                            "<span style='color:#fff;font-weight:bold'>Sri Lanka Institute of Information Technology</span>" +
                                            "</div>" +
                                            "<div class='email-body' style='clear-both'>#content</div>" +


                   "</div></div></div></div>" +
                   "</body></html>";
                    string message1 = "<h3>Dear <span></span>" + hdfirstname.Text + ", </h3>"
                    + "<div class='strip'><p>" + txtEmailMessage.Text + "</p>"
                    + "</div>" +
                    "<p class='note' style='margin-bottom:20px'>Note: This is system generated email. Please do not reply to this email.</p>";
                    string Mailbody = message1.ToString();
                    string nMailbody = EmailTemplate.Replace("#content", Mailbody);

                    string message = "";
                    if (rbtodayselect.SelectedValue == "1")
                    {
                        message = nMailbody;
                    }

                    else
                    {

                        if (rbtodaytemplate.SelectedValue == "1")
                        {
                            message = dsUserContact.Tables[1].Rows[1]["TEMPLATE"].ToString();
                        }
                        else if (rbtodaytemplate.SelectedValue == "2")
                        {
                            message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                        }
                    }    
                    //message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                    filename = Convert.ToString(Session["EmailFileAttachemnt"]);
                    Execute(message, hdfEmailid.Text, txtEmailSubject.Text, hdfirstname.Text, hdfAppli.Text, filename, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
                    chk.Checked = false;
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(updEmailSend, "Please select atleast one check box for email send !!!", this.Page);
                txtEmailMessage.Text = "";
                txtEmailSubject.Text = "";
                Session["EmailFileAttachemnt"] = null;
                return;
            }
            else
            {
                objCommon.DisplayMessage(updEmailSend, "Email send Successfully !!!", this.Page);
                txtEmailMessage.Text = "";
                txtEmailSubject.Text = "";
                Session["EmailFileAttachemnt"] = null;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnsendemailupc_Click(object sender, EventArgs e)
    {
        try
        {
            divlkfollowdate.Attributes.Remove("class");
            string studentIds = string.Empty; string filename = ""; int count = 0;

            string folderPath = Server.MapPath("~/EmailUploadFile/");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            Session["EmailFileAttachemnt"] = fuAttachFile.FileName;
            if (Session["EmailFileAttachemnt"] != string.Empty || Session["EmailFileAttachemnt"] != "")
            {
                fuAttachFile.SaveAs(folderPath + Path.GetFileName(fuAttachFile.FileName));
            }

            DataSet dsUserContact = null;
            dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));
            foreach (ListViewDataItem item in lvupcomings.Items)
            {
                CheckBox chk = item.FindControl("chkemailupc") as CheckBox;
                LinkButton hdfAppli = item.FindControl("lbluuser") as LinkButton;
                Label hdfEmailid = item.FindControl("lblupemail") as Label;
                Label hdfirstname = item.FindControl("lblupname") as Label;
                //if (chk.Checked == false)
                //{

                //    objCommon.DisplayMessage(updEmailSend, "Please Select At least One User For Sending Email !!!", this.Page);
                //}
                if (chk.Checked == true)
                {
                    count++;
                    //FOR MANISH : ERR: AT HTML TAGS :
                    // msg.Body = "<table width='500px' cellspacing='0' style='background-color: #F2F2F2'><tr><td>Dear " + firstname.ToString() + " " + lastname.ToString() + ',' + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Greetings from the LNMIIT …!!!</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>We've created a new LNMIIT user account for you. Please use the following application ID and password to sign in & complete the application.The application ID will be treated as your unique registration ID for all further proceedings.</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Your account details are :</td></tr></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Application ID : " + username.ToString() + "</td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Password : " + password.ToString() + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thanking you,</td></tr><tr><td></td></tr>Sincerely,<tr><td></td></tr><tr><td></td></tr><tr>Convener<td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>UG Admissions – 2014</td></tr><tr><td></td></tr><tr><td>This is an automated e-mail from an unattended mailbox. Please do not reply to this email.For any further communication please write to : <a href='ugadmissions@lnmiit.ac.in'>ugadmissions@lnmiit.ac.in</a></td></tr></table>";
                    // msg.Body = "<table width='500px' cellspacing='0' style='background-color: #F2F2F2'><tr><td>Dear " + firstname.ToString() + " " + lastname.ToString() + ',' + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Greetings from the LNMIIT. </td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thanks for registering with LNMIIT. </td></tr><tr><td></td></tr><tr><td></td></tr><tr><td >Use </td></tr></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Application ID : " + username.ToString() + "</td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Password : " + password.ToString() + "</td></tr><tr><td>for further processing.</td></tr><tr><td></td></tr><tr><td>Thanking you,</td></tr><tr><td></td></tr>Sincerely,<tr><td></td></tr><tr><td></td></tr><tr>Convener<td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>UG Admissions – 2015</td></tr><tr><td></td></tr><tr><td>This is an automated e-mail. Please do not reply to this email. For any further communication please write to : <a href='ugadmissions@lnmiit.ac.in'>ugadmissions@lnmiit.ac.in</a></td></tr></table>";
                    const string EmailTemplate = "<!DOCTYPE html><html><head><meta charset='utf-8'><meta name='viewport' content='width=device-width, initial-scale=1'><link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css' /><link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css' />" +
                       "<style>.email-template .email-header{background-color:#346CB0;text-align:center}.email-body{padding:30px 15px 0 15px;border:1px solid #ccc}.email-body p{font-size:16px;font-weight:500}ul.simply-list{list-style-type:none;padding:0;margin:0}ul.simply-list .list-group-item{padding:2px 15px;font-weight:600;font-size:16px;line-height:1.4;border:none;background-color:transparent}ul.simply-list.no-border .list-group-item{border:none}.strip{padding:20px 15px;background-color:#eee;margin:15px 0}.strip ul.simply-list li:first-child{padding-left:0}.d-flex{display:flex;-webkit-display:flex;-moz-display:flex;-o-display:flex;flex-direction:row}.d-flex.align-items-center{align-items:center}.note{color:red;font-weight:500;font-size:12px;margin-bottom:10px}.email-template .email-header img{width:85px;display:block;margin:auto}.email-template .email-header span{font-size:22px;font-weight:500;padding:10px 0}.email-info{margin-top:10px;padding:10px 0}.info-list span{font-size:14px;display:flex;align-items:center;font-weight:600;color:#666}.info-list span a{color:#666}.info-list span i{font-size:18px;margin-right:5px;color:#888}@media only screen and (max-width:767px){.email-template .email-header{padding:10px 10px;text-align:center}.email-template .email-header img{width:70px;display:block;margin:auto;margin-bottom:5px}.email-template .email-header span{font-size:22px;font-weight:500}.strip ul.simply-list li{padding-left:0}.info-list li{line-height:1.9}}</style>" +
                      "<style>.form-group{margin-bottom:15px}.btn-success{color:#fff;background-color:#5cb85c;border-color:#4cae4c}.btn-success.focus,.btn-success:focus{color:#fff;background-color:#449d44;border-color:#255625}.btn-success:hover{color:#fff;background-color:#449d44;border-color:#398439}.btn-success.active,.btn-success:active,.open>.dropdown-toggle.btn-success{color:#fff;background-color:#449d44;background-image:none;border-color:#398439}.btn-success.active.focus,.btn-success.active:focus,.btn-success.active:hover,.btn-success:active.focus,.btn-success:active:focus,.btn-success:active:hover,.open>.dropdown-toggle.btn-success.focus,.open>.dropdown-toggle.btn-success:focus,.open>.dropdown-toggle.btn-success:hover{color:#fff;background-color:#398439;border-color:#255625}.btn-success.disabled.focus,.btn-success.disabled:focus,.btn-success.disabled:hover,.btn-success[disabled].focus,.btn-success[disabled]:focus,.btn-success[disabled]:hover,fieldset[disabled] .btn-success.focus,fieldset[disabled] .btn-success:focus,fieldset[disabled] .btn-success:hover{background-color:#5cb85c;border-color:#4cae4c}.text-success{color:#3c763d}a.text-success:focus,a.text-success:hover{color:#2b542c}.btn{margin:.2em .1em;font-weight:500;font-size:1em;-webkit-font-smoothing:antialiased;-moz-osx-font-smoothing:grayscale;border:none;border-bottom:.15em solid #000;border-radius:3px;padding:.65em 1.3em}.btn-primary{border-color:#2a6496;background-image:linear-gradient(#428bca,#357ebd)}.btn-primary:hover{background:linear-gradient(#357ebd,#3071a9)}</style>" +

                                            "<div class='container-fluid' style='padding-top:15px'>" +
                                            "<div class='row'>" +
                                            "<div class='col-sm-8 col-md-6 col-md-offset-3 col-sm-offset-2'>" +
                                            "<div class='email-template'>" +
                                            "<div class='email-header'>" +
                        //"<img alt='logo' src='http://erptest.sliit.lk/IMAGES/SLIIT_logo.png' />" +
                                            "<span style='color:#fff;font-weight:bold'>Sri Lanka Institute of Information Technology</span>" +
                                            "</div>" +
                                            "<div class='email-body' style='clear-both'>#content</div>" +


                   "</div></div></div></div>" +
                   "</body></html>";
                    string message1 = "<h3>Dear <span></span>" + hdfirstname.Text + ", </h3>"
                    + "<div class='strip'><p>" + txtEmailMessage.Text + "</p>"
                    + "</div>" +
                    "<p class='note' style='margin-bottom:20px'>Note: This is system generated email. Please do not reply to this email.</p>";
                    string Mailbody = message1.ToString();
                    string nMailbody = EmailTemplate.Replace("#content", Mailbody);

                    string message = "";
                    if (rbupcselect.SelectedValue == "1")
                    {
                        message = nMailbody;
                    }

                    else
                    {
                        if (rbupctemplate.SelectedValue == "1")
                        {
                            message = dsUserContact.Tables[1].Rows[1]["TEMPLATE"].ToString();
                        }
                        else if (rbupctemplate.SelectedValue == "2")
                        {
                            message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                        }
                    }    
                    //message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                    filename = Convert.ToString(Session["EmailFileAttachemnt"]);
                    Execute(message, hdfEmailid.Text, txtEmailSubject.Text, hdfirstname.Text, hdfAppli.Text, filename, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(updEmailSend, "Please select atleast one check box for email send !!!", this.Page);
                txtEmailMessage.Text = "";
                txtEmailSubject.Text = "";
                Session["EmailFileAttachemnt"] = null;
                return;
            }
            else
            {
                objCommon.DisplayMessage(updEmailSend, "Email send Successfully !!!", this.Page);
                txtEmailMessage.Text = "";
                txtEmailSubject.Text = "";
                Session["EmailFileAttachemnt"] = null;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnsendemailove_Click(object sender, EventArgs e)
    {
        try
        {
            divlkfollowdate.Attributes.Remove("class");
            string studentIds = string.Empty; string filename = ""; int count = 0;

            string folderPath = Server.MapPath("~/EmailUploadFile/");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            Session["EmailFileAttachemnt"] = fuAttachFile.FileName;
            if (Session["EmailFileAttachemnt"] != string.Empty || Session["EmailFileAttachemnt"] != "")
            {
                fuAttachFile.SaveAs(folderPath + Path.GetFileName(fuAttachFile.FileName));
            }

            DataSet dsUserContact = null;
            dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));
            foreach (ListViewDataItem item in lvoverdue.Items)
            {
                CheckBox chk = item.FindControl("chkemailove") as CheckBox;
                LinkButton hdfAppli = item.FindControl("lbloduname") as LinkButton;
                Label hdfEmailid = item.FindControl("lblodemail") as Label;
                Label hdfirstname = item.FindControl("lblodname") as Label;
                //if (chk.Checked == false)
                //{

                //    objCommon.DisplayMessage(updEmailSend, "Please Select At least One User For Sending Email !!!", this.Page);
                //}
                if (chk.Checked == true)
                {
                    count++;
                    //FOR MANISH : ERR: AT HTML TAGS :
                    // msg.Body = "<table width='500px' cellspacing='0' style='background-color: #F2F2F2'><tr><td>Dear " + firstname.ToString() + " " + lastname.ToString() + ',' + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Greetings from the LNMIIT …!!!</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>We've created a new LNMIIT user account for you. Please use the following application ID and password to sign in & complete the application.The application ID will be treated as your unique registration ID for all further proceedings.</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Your account details are :</td></tr></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Application ID : " + username.ToString() + "</td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Password : " + password.ToString() + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thanking you,</td></tr><tr><td></td></tr>Sincerely,<tr><td></td></tr><tr><td></td></tr><tr>Convener<td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>UG Admissions – 2014</td></tr><tr><td></td></tr><tr><td>This is an automated e-mail from an unattended mailbox. Please do not reply to this email.For any further communication please write to : <a href='ugadmissions@lnmiit.ac.in'>ugadmissions@lnmiit.ac.in</a></td></tr></table>";
                    // msg.Body = "<table width='500px' cellspacing='0' style='background-color: #F2F2F2'><tr><td>Dear " + firstname.ToString() + " " + lastname.ToString() + ',' + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Greetings from the LNMIIT. </td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thanks for registering with LNMIIT. </td></tr><tr><td></td></tr><tr><td></td></tr><tr><td >Use </td></tr></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Application ID : " + username.ToString() + "</td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Password : " + password.ToString() + "</td></tr><tr><td>for further processing.</td></tr><tr><td></td></tr><tr><td>Thanking you,</td></tr><tr><td></td></tr>Sincerely,<tr><td></td></tr><tr><td></td></tr><tr>Convener<td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>UG Admissions – 2015</td></tr><tr><td></td></tr><tr><td>This is an automated e-mail. Please do not reply to this email. For any further communication please write to : <a href='ugadmissions@lnmiit.ac.in'>ugadmissions@lnmiit.ac.in</a></td></tr></table>";
                    const string EmailTemplate = "<!DOCTYPE html><html><head><meta charset='utf-8'><meta name='viewport' content='width=device-width, initial-scale=1'><link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css' /><link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css' />" +
                       "<style>.email-template .email-header{background-color:#346CB0;text-align:center}.email-body{padding:30px 15px 0 15px;border:1px solid #ccc}.email-body p{font-size:16px;font-weight:500}ul.simply-list{list-style-type:none;padding:0;margin:0}ul.simply-list .list-group-item{padding:2px 15px;font-weight:600;font-size:16px;line-height:1.4;border:none;background-color:transparent}ul.simply-list.no-border .list-group-item{border:none}.strip{padding:20px 15px;background-color:#eee;margin:15px 0}.strip ul.simply-list li:first-child{padding-left:0}.d-flex{display:flex;-webkit-display:flex;-moz-display:flex;-o-display:flex;flex-direction:row}.d-flex.align-items-center{align-items:center}.note{color:red;font-weight:500;font-size:12px;margin-bottom:10px}.email-template .email-header img{width:85px;display:block;margin:auto}.email-template .email-header span{font-size:22px;font-weight:500;padding:10px 0}.email-info{margin-top:10px;padding:10px 0}.info-list span{font-size:14px;display:flex;align-items:center;font-weight:600;color:#666}.info-list span a{color:#666}.info-list span i{font-size:18px;margin-right:5px;color:#888}@media only screen and (max-width:767px){.email-template .email-header{padding:10px 10px;text-align:center}.email-template .email-header img{width:70px;display:block;margin:auto;margin-bottom:5px}.email-template .email-header span{font-size:22px;font-weight:500}.strip ul.simply-list li{padding-left:0}.info-list li{line-height:1.9}}</style>" +
                      "<style>.form-group{margin-bottom:15px}.btn-success{color:#fff;background-color:#5cb85c;border-color:#4cae4c}.btn-success.focus,.btn-success:focus{color:#fff;background-color:#449d44;border-color:#255625}.btn-success:hover{color:#fff;background-color:#449d44;border-color:#398439}.btn-success.active,.btn-success:active,.open>.dropdown-toggle.btn-success{color:#fff;background-color:#449d44;background-image:none;border-color:#398439}.btn-success.active.focus,.btn-success.active:focus,.btn-success.active:hover,.btn-success:active.focus,.btn-success:active:focus,.btn-success:active:hover,.open>.dropdown-toggle.btn-success.focus,.open>.dropdown-toggle.btn-success:focus,.open>.dropdown-toggle.btn-success:hover{color:#fff;background-color:#398439;border-color:#255625}.btn-success.disabled.focus,.btn-success.disabled:focus,.btn-success.disabled:hover,.btn-success[disabled].focus,.btn-success[disabled]:focus,.btn-success[disabled]:hover,fieldset[disabled] .btn-success.focus,fieldset[disabled] .btn-success:focus,fieldset[disabled] .btn-success:hover{background-color:#5cb85c;border-color:#4cae4c}.text-success{color:#3c763d}a.text-success:focus,a.text-success:hover{color:#2b542c}.btn{margin:.2em .1em;font-weight:500;font-size:1em;-webkit-font-smoothing:antialiased;-moz-osx-font-smoothing:grayscale;border:none;border-bottom:.15em solid #000;border-radius:3px;padding:.65em 1.3em}.btn-primary{border-color:#2a6496;background-image:linear-gradient(#428bca,#357ebd)}.btn-primary:hover{background:linear-gradient(#357ebd,#3071a9)}</style>" +

                                            "<div class='container-fluid' style='padding-top:15px'>" +
                                            "<div class='row'>" +
                                            "<div class='col-sm-8 col-md-6 col-md-offset-3 col-sm-offset-2'>" +
                                            "<div class='email-template'>" +
                                            "<div class='email-header'>" +
                        //"<img alt='logo' src='http://erptest.sliit.lk/IMAGES/SLIIT_logo.png' />" +
                                            "<span style='color:#fff;font-weight:bold'>Sri Lanka Institute of Information Technology</span>" +
                                            "</div>" +
                                            "<div class='email-body' style='clear-both'>#content</div>" +


                   "</div></div></div></div>" +
                   "</body></html>";
                    string message1 = "<h3>Dear <span></span>" + hdfirstname.Text + ", </h3>"
                    + "<div class='strip'><p>" + txtEmailMessage.Text + "</p>"
                    + "</div>" +
                    "<p class='note' style='margin-bottom:20px'>Note: This is system generated email. Please do not reply to this email.</p>";
                    string Mailbody = message1.ToString();
                    string nMailbody = EmailTemplate.Replace("#content", Mailbody);

                    string message = "";
                    if (rboverselect.SelectedValue == "1")
                    {
                        message = nMailbody;
                    }

                    else
                    {

                        if (rbovertemplate.SelectedValue == "1")
                        {
                            message = dsUserContact.Tables[1].Rows[1]["TEMPLATE"].ToString();
                        }
                        else if (rbovertemplate.SelectedValue == "2")
                        {
                            message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                        }
                    }
                    //message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                    filename = Convert.ToString(Session["EmailFileAttachemnt"]);
                    Execute(message, hdfEmailid.Text, txtEmailSubject.Text, hdfirstname.Text, hdfAppli.Text, filename, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(updEmailSend, "Please select atleast one check box for email send !!!", this.Page);
                txtEmailMessage.Text = "";
                txtEmailSubject.Text = "";
                Session["EmailFileAttachemnt"] = null;
                return;
            }
            else
            {
                objCommon.DisplayMessage(updEmailSend, "Email send Successfully !!!", this.Page);
                txtEmailMessage.Text = "";
                txtEmailSubject.Text = "";
                Session["EmailFileAttachemnt"] = null;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnsendemailcomp_Click(object sender, EventArgs e)
    {
        try
        {
            divlkfollowdate.Attributes.Remove("class");
            string studentIds = string.Empty; string filename = ""; int count = 0;

            string folderPath = Server.MapPath("~/EmailUploadFile/");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            Session["EmailFileAttachemnt"] = fuAttachFile.FileName;
            if (Session["EmailFileAttachemnt"] != string.Empty || Session["EmailFileAttachemnt"] != "")
            {
                fuAttachFile.SaveAs(folderPath + Path.GetFileName(fuAttachFile.FileName));
            }

            DataSet dsUserContact = null;
            dsUserContact = user.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));
            foreach (ListViewDataItem item in lvcomplete.Items)
            {
                CheckBox chk = item.FindControl("chkemailcompl") as CheckBox;
                LinkButton hdfAppli = item.FindControl("lblcomplusername") as LinkButton;
                Label hdfEmailid = item.FindControl("lblcompemail") as Label;
                Label hdfirstname = item.FindControl("lblcompname") as Label;
                //if (chk.Checked == false)
                //{

                //    objCommon.DisplayMessage(updEmailSend, "Please Select At least One User For Sending Email !!!", this.Page);
                //}
                if (chk.Checked == true)
                {
                    count++;
                    //FOR MANISH : ERR: AT HTML TAGS :
                    // msg.Body = "<table width='500px' cellspacing='0' style='background-color: #F2F2F2'><tr><td>Dear " + firstname.ToString() + " " + lastname.ToString() + ',' + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Greetings from the LNMIIT …!!!</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>We've created a new LNMIIT user account for you. Please use the following application ID and password to sign in & complete the application.The application ID will be treated as your unique registration ID for all further proceedings.</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Your account details are :</td></tr></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Application ID : " + username.ToString() + "</td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Password : " + password.ToString() + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thanking you,</td></tr><tr><td></td></tr>Sincerely,<tr><td></td></tr><tr><td></td></tr><tr>Convener<td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>UG Admissions – 2014</td></tr><tr><td></td></tr><tr><td>This is an automated e-mail from an unattended mailbox. Please do not reply to this email.For any further communication please write to : <a href='ugadmissions@lnmiit.ac.in'>ugadmissions@lnmiit.ac.in</a></td></tr></table>";
                    // msg.Body = "<table width='500px' cellspacing='0' style='background-color: #F2F2F2'><tr><td>Dear " + firstname.ToString() + " " + lastname.ToString() + ',' + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Greetings from the LNMIIT. </td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thanks for registering with LNMIIT. </td></tr><tr><td></td></tr><tr><td></td></tr><tr><td >Use </td></tr></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Application ID : " + username.ToString() + "</td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Password : " + password.ToString() + "</td></tr><tr><td>for further processing.</td></tr><tr><td></td></tr><tr><td>Thanking you,</td></tr><tr><td></td></tr>Sincerely,<tr><td></td></tr><tr><td></td></tr><tr>Convener<td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>UG Admissions – 2015</td></tr><tr><td></td></tr><tr><td>This is an automated e-mail. Please do not reply to this email. For any further communication please write to : <a href='ugadmissions@lnmiit.ac.in'>ugadmissions@lnmiit.ac.in</a></td></tr></table>";
                    const string EmailTemplate = "<!DOCTYPE html><html><head><meta charset='utf-8'><meta name='viewport' content='width=device-width, initial-scale=1'><link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css' /><link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css' />" +
                       "<style>.email-template .email-header{background-color:#346CB0;text-align:center}.email-body{padding:30px 15px 0 15px;border:1px solid #ccc}.email-body p{font-size:16px;font-weight:500}ul.simply-list{list-style-type:none;padding:0;margin:0}ul.simply-list .list-group-item{padding:2px 15px;font-weight:600;font-size:16px;line-height:1.4;border:none;background-color:transparent}ul.simply-list.no-border .list-group-item{border:none}.strip{padding:20px 15px;background-color:#eee;margin:15px 0}.strip ul.simply-list li:first-child{padding-left:0}.d-flex{display:flex;-webkit-display:flex;-moz-display:flex;-o-display:flex;flex-direction:row}.d-flex.align-items-center{align-items:center}.note{color:red;font-weight:500;font-size:12px;margin-bottom:10px}.email-template .email-header img{width:85px;display:block;margin:auto}.email-template .email-header span{font-size:22px;font-weight:500;padding:10px 0}.email-info{margin-top:10px;padding:10px 0}.info-list span{font-size:14px;display:flex;align-items:center;font-weight:600;color:#666}.info-list span a{color:#666}.info-list span i{font-size:18px;margin-right:5px;color:#888}@media only screen and (max-width:767px){.email-template .email-header{padding:10px 10px;text-align:center}.email-template .email-header img{width:70px;display:block;margin:auto;margin-bottom:5px}.email-template .email-header span{font-size:22px;font-weight:500}.strip ul.simply-list li{padding-left:0}.info-list li{line-height:1.9}}</style>" +
                      "<style>.form-group{margin-bottom:15px}.btn-success{color:#fff;background-color:#5cb85c;border-color:#4cae4c}.btn-success.focus,.btn-success:focus{color:#fff;background-color:#449d44;border-color:#255625}.btn-success:hover{color:#fff;background-color:#449d44;border-color:#398439}.btn-success.active,.btn-success:active,.open>.dropdown-toggle.btn-success{color:#fff;background-color:#449d44;background-image:none;border-color:#398439}.btn-success.active.focus,.btn-success.active:focus,.btn-success.active:hover,.btn-success:active.focus,.btn-success:active:focus,.btn-success:active:hover,.open>.dropdown-toggle.btn-success.focus,.open>.dropdown-toggle.btn-success:focus,.open>.dropdown-toggle.btn-success:hover{color:#fff;background-color:#398439;border-color:#255625}.btn-success.disabled.focus,.btn-success.disabled:focus,.btn-success.disabled:hover,.btn-success[disabled].focus,.btn-success[disabled]:focus,.btn-success[disabled]:hover,fieldset[disabled] .btn-success.focus,fieldset[disabled] .btn-success:focus,fieldset[disabled] .btn-success:hover{background-color:#5cb85c;border-color:#4cae4c}.text-success{color:#3c763d}a.text-success:focus,a.text-success:hover{color:#2b542c}.btn{margin:.2em .1em;font-weight:500;font-size:1em;-webkit-font-smoothing:antialiased;-moz-osx-font-smoothing:grayscale;border:none;border-bottom:.15em solid #000;border-radius:3px;padding:.65em 1.3em}.btn-primary{border-color:#2a6496;background-image:linear-gradient(#428bca,#357ebd)}.btn-primary:hover{background:linear-gradient(#357ebd,#3071a9)}</style>" +

                                            "<div class='container-fluid' style='padding-top:15px'>" +
                                            "<div class='row'>" +
                                            "<div class='col-sm-8 col-md-6 col-md-offset-3 col-sm-offset-2'>" +
                                            "<div class='email-template'>" +
                                            "<div class='email-header'>" +
                        //"<img alt='logo' src='http://erptest.sliit.lk/IMAGES/SLIIT_logo.png' />" +
                                            "<span style='color:#fff;font-weight:bold'>Sri Lanka Institute of Information Technology</span>" +
                                            "</div>" +
                                            "<div class='email-body' style='clear-both'>#content</div>" +


                   "</div></div></div></div>" +
                   "</body></html>";
                    string message1 = "<h3>Dear <span></span>" + hdfirstname.Text + ", </h3>"
                    + "<div class='strip'><p>" + txtEmailMessage.Text + "</p>"
                    + "</div>" +
                    "<p class='note' style='margin-bottom:20px'>Note: This is system generated email. Please do not reply to this email.</p>";
                    string Mailbody = message1.ToString();
                    string nMailbody = EmailTemplate.Replace("#content", Mailbody);

                    string message = "";
                    if (rbcompselect.SelectedValue == "1")
                    {
                        message = nMailbody;
                    }
                    else
                    {
                        if (rbcomtemplate.SelectedValue == "1")
                        {
                            message = dsUserContact.Tables[1].Rows[1]["TEMPLATE"].ToString();
                        }
                        else if (rbcomtemplate.SelectedValue == "2")
                        {
                            message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                        }
                    }
                    //message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                    filename = Convert.ToString(Session["EmailFileAttachemnt"]);
                    Execute(message, hdfEmailid.Text, txtEmailSubject.Text, hdfirstname.Text, hdfAppli.Text, filename, Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]), Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"])).Wait();
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(updEmailSend, "Please select atleast one check box for email send !!!", this.Page);
                txtEmailMessage.Text = "";
                txtEmailSubject.Text = "";
                Session["EmailFileAttachemnt"] = null;
                return;
            }
            else
            {
                objCommon.DisplayMessage(updEmailSend, "Email send Successfully !!!", this.Page);
                txtEmailMessage.Text = "";
                txtEmailSubject.Text = "";
                Session["EmailFileAttachemnt"] = null;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void rbselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbselect.SelectedValue == "1")
        {
            auto.Visible = true;
            manu.Visible = false;
        }

        else if (rbselect.SelectedValue == "2")
        {
            auto.Visible = false;
            manu.Visible = true;
        }
    }
    protected void rbtodayselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtodayselect.SelectedValue == "1")
        {
            todaymau.Visible = true;
            todayauto.Visible = false;
        }

        else if (rbtodayselect.SelectedValue == "2")
        {
            todaymau.Visible = false;
            todayauto.Visible = true;
        }
    }
    protected void rbupcselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbupcselect.SelectedValue == "1")
        {
            manuupc.Visible = true;
            autoupc.Visible = false;
        }

        else if (rbupcselect.SelectedValue == "2")
        {
            manuupc.Visible = false;
            autoupc.Visible = true;
        }
    }
    protected void rboverselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rboverselect.SelectedValue == "1")
        {
            manuover.Visible = true;
            autoover.Visible = false;
        }

        else if (rboverselect.SelectedValue == "2")
        {
            manuover.Visible = false;
            autoover.Visible = true;
        }
    }
    protected void rbcompselect_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rbcompselect.SelectedValue == "1")
        {
            manucomp.Visible = true;
            autocomp.Visible = false;
        }

        else if (rbcompselect.SelectedValue == "2")
        {
            manucomp.Visible = false;
            autocomp.Visible = true;
        }

    }
}