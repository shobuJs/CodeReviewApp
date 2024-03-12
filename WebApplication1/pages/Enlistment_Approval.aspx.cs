using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

public partial class MockUps_Enlistment_Approval : System.Web.UI.Page
{
    Common objCommon = new Common();

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
            if (Session["userno"] == null || Session["username"] == null ||
                  Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null || Session["idno"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    BindDropDownList();
                    this.CheckPageAuthorization();
                }
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Semester_Registration.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Enlistment_Approval.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Enlistment_Approval.aspx");
        }
    }
    protected void BindDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlAcademicSession, "ACD_SESSION_MASTER A INNER JOIN ACD_ACADEMIC_SESSION B ON A.ACADEMIC_NO = B.ACADEMIC_NO", "DISTINCT SESSIONNO", "SESSION_NAME", "A.FLOCK = 1", "SESSIONNO");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") and isnull(ACTIVE,0)=1", "COLLEGE_ID");
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvBindEnlismentList.DataSource = null;
            lvBindEnlismentList.DataBind();

            lvEnlistmentCount.DataSource = null;
            lvEnlistmentCount.DataBind();

            ddlSemster.SelectedIndex = 0;
            ddlProgram.SelectedIndex = 0;

            objCommon.FillDropDownList(ddlSemster, "ACD_STUDENT_RESULT S INNER JOIN ACD_SEMESTER A ON S.SEMESTERNO = A.SEMESTERNO", "DISTINCT S.SEMESTERNO", "SEMESTERNAME", "S.SESSIONNO=" + ddlAcademicSession.SelectedValue + " AND COLLEGE_ID=" + ddlCollege.SelectedValue, "S.SEMESTERNO");

            
        }
        catch (Exception ex)
        { 
            
        }
    }
    
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvBindEnlismentList.DataSource = null;
            lvBindEnlismentList.DataBind();

            lvEnlistmentCount.DataSource = null;
            lvEnlistmentCount.DataBind();

            
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            string Call_Values1 = "";

            string SP_Name1 = "PKG_ACD_GET_ENLISTMENT_LIST";
            string SP_Parameters1 = "@P_SESSIONNO,@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO,@P_UA_NO";
            if (ddlProgram.SelectedIndex == 0)
            {
                Call_Values1 = "" + Convert.ToInt32(ddlAcademicSession.SelectedValue) + "," + Convert.ToInt32(ddlCollege.SelectedValue) + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(0) + "," + ddlSemster.SelectedValue + "," + Session["userno"];
            }
            else
            {
                Call_Values1 = "" + Convert.ToInt32(ddlAcademicSession.SelectedValue) + "," + Convert.ToInt32(ddlCollege.SelectedValue) + "," + Convert.ToInt32(ddlProgram.SelectedValue.Split(',')[0]) + "," + Convert.ToInt32(ddlProgram.SelectedValue.Split(',')[1]) + "," + ddlSemster.SelectedValue + "," + Session["userno"];
            }

            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);

            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                lvEnlistmentCount.DataSource = ds.Tables[0];
                lvEnlistmentCount.DataBind();
            }
            else
            {
                lvEnlistmentCount.DataSource = null;
                lvEnlistmentCount.DataBind();
            }
            if (ds.Tables != null && ds.Tables[1].Rows.Count > 0)
            {
                lvBindEnlismentList.DataSource = ds.Tables[1];
                lvBindEnlismentList.DataBind();
            }
            else
            {
                lvBindEnlismentList.DataSource = null;
                lvBindEnlismentList.DataBind();
                if(ddlCollege.SelectedIndex > 0)
                    objCommon.DisplayMessage(updEnlistment, "Record Not Found", this.Page);
            }
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            bool Flag = false; string idnos = ""; string Emailids = ""; string CourseNames = "";string StudNames = "";
            foreach (ListViewDataItem items in lvBindEnlismentList.Items)
            {
                HiddenField hdfEmailId = (HiddenField)items.FindControl("hdfEmailid");
                HiddenField hdfCourseName = (HiddenField)items.FindControl("hdfCourseName");
                Label lblStudName = (Label)items.FindControl("lblStudName");
                Label lblEnrollNo = (Label)items.FindControl("lblEnrollNo"); 

                CheckBox chk = (CheckBox)items.FindControl("chkBox");
                if (chk.Checked == true && chk.Enabled == true)
                {
                    Flag = true;
                    idnos += chk.ToolTip.ToString() + '$';
                    Emailids += hdfEmailId.Value + '$';
                    CourseNames += hdfCourseName.Value + '$';
                    StudNames += lblStudName.Text + '$';
                }
            }
            if (Flag == false)
            {
                objCommon.DisplayMessage(updEnlistment, "Please Select Atleast One CheckBox !!!", this.Page);
                return;
            }
            idnos = idnos.TrimEnd('$');
            Emailids = Emailids.TrimEnd('$');
            CourseNames = CourseNames.TrimEnd('$');
            StudNames = StudNames.TrimEnd('$');

            string Call_Values1 = "";
            string SP_Name1 = "PKG_ACD_UPDATE_ENLISTMENT_LIST";
            string SP_Parameters1 = "@P_IDNOS,@P_SESSIONNO,@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO,@P_UA_NO,@P_OUTPUT";
            if (ddlProgram.SelectedIndex == 0)
            {
                Call_Values1 = "" + idnos + "," + Convert.ToInt32(ddlAcademicSession.SelectedValue) + "," + Convert.ToInt32(ddlCollege.SelectedValue) + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(0) + "," + ddlSemster.SelectedValue + "," + Convert.ToInt32(Session["userno"]) + ",0";
            }
            else
            {
                Call_Values1 = "" + idnos + "," + Convert.ToInt32(ddlAcademicSession.SelectedValue) + "," + Convert.ToInt32(ddlCollege.SelectedValue) + "," + Convert.ToInt32(ddlProgram.SelectedValue.Split(',')[0]) + "," + Convert.ToInt32(ddlProgram.SelectedValue.Split(',')[1]) + "," + ddlSemster.SelectedValue + "," + Convert.ToInt32(Session["userno"]) + ",0";
            }

            string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);

            if (que_out1 == "1")
            {
                objCommon.DisplayMessage(updEnlistment, "Enlistment Approved Successfully !!!", this.Page);
                btnShow_Click(new object(), new EventArgs());
                EmailSmsWhatssppSend(Convert.ToInt32(Request.QueryString["pageno"].ToString()), Emailids, StudNames, CourseNames);
            }
            else
            {
                objCommon.DisplayMessage(updEnlistment, "Unable to approved enlistment !!!", this.Page);
            }

        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void ddlSemster_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvBindEnlismentList.DataSource = null;
            lvBindEnlismentList.DataBind();

            lvEnlistmentCount.DataSource = null;
            lvEnlistmentCount.DataBind();

            ddlProgram.SelectedIndex = 0;

            objCommon.FillDropDownList(ddlProgram, "ACD_STUDENT_RESULT S INNER JOIN ACD_COLLEGE_MASTER CM ON S.COLLEGE_ID = CM.COLLEGE_ID INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO) INNER JOIN ACD_BRANCH BA ON (CD.BRANCHNO = BA.BRANCHNO) INNER JOIN ACD_AFFILIATED_UNIVERSITY AU ON (CD.AFFILIATED_NO = AU.AFFILIATED_NO)", "DISTINCT (CONVERT(NVARCHAR(5),CD.DEGREENO) + ','+ CONVERT(NVARCHAR(5),CD.BRANCHNO) + ',' + CONVERT(NVARCHAR(5),CD.AFFILIATED_NO) + ',' + CONVERT(NVARCHAR(5),CD.COLLEGE_ID))", "(D.DEGREENAME + ' - ' + LONGNAME + ' - ' + AFFILIATED_SHORTNAME)", " S.COLLEGE_ID IN(" + ddlCollege.SelectedValue + ") AND S.SEMESTERNO = " + ddlSemster.SelectedValue, "");
        }
        catch (Exception ex)
        {

        }
    }

    protected void EmailSmsWhatssppSend(int Page_No, string toSendAddre, string Name, string CourseNames)
    {
        EmailSmsWhatsaap Email = new EmailSmsWhatsaap();
        DataSet ds = objCommon.FillDropDown("ACCESS_LINK", "isnull(EMAIL,0) EMAIL,isnull(SMS,0) as SMS,isnull(WHATSAAP,0) as WHATSAAP", "", "AL_NO=" + Convert.ToInt32(Request.QueryString["pageno"].ToString()), "");
        string Res = ""; string Message = "";
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["EMAIL"].ToString()) == 1)//For Email Send 
        {
            string[] EmailidArray = toSendAddre.Split('$'); string[] Names = Name.Split('$'); string[] CourseName = CourseNames.Split('$');
            for (int i = 0; i < EmailidArray.Length; i++)
            {
                int PageNo = Page_No;
                //int PageNo = 33;
                UserController objUC = new UserController();
                DataSet dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", "0", PageNo);
                if (dsUserContact.Tables[1] != null && dsUserContact.Tables[1].Rows.Count > 0)
                {
                    string Subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                    Message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                    string toSendAddress = EmailidArray[i].ToString();// 
                    MailMessage msgsPara = new MailMessage();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(Message);
                    msgsPara.BodyEncoding = Encoding.UTF8;
                    msgsPara.Body = Convert.ToString(sb);
                    msgsPara.Body = msgsPara.Body.Replace("[UA_FULLNAME]", Names[i].ToString());
                    msgsPara.Body = msgsPara.Body.Replace("[SUBJECTS]", CourseName[i].ToString());

                    Task<string> task = Email.Execute(msgsPara.Body, toSendAddress, Subject, null, string.Empty);
                    Res = task.Result;
                    // objCommon.DisplayMessage(this, Res.ToString(), this.Page);
                }
            }
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["SMS"].ToString()) == 1) //For SMS Send 
            {
                //string MobileNo = "9503244325";
                //string Msg = "Your ERP Password has been reset successfully! Your new Login Password is ";
                //string TemplateID = "1007583285079323052";
                //Res = Email.SendSMS(MobileNo.ToString(), Msg.ToString(), TemplateID.ToString());
                //objCommon.DisplayMessage(this, Res.ToString(), this.Page);
            }
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["WHATSAAP"].ToString()) == 1)//For Whatsaap Send 
            {
                //int Ress = Email.SendWhatsaapOPT("950324432", "Roshan", "20222");
            }
        }
    }
    protected void ddlAcademicSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvBindEnlismentList.DataSource = null;
            lvBindEnlismentList.DataBind();

            lvEnlistmentCount.DataSource = null;
            lvEnlistmentCount.DataBind();

            ddlCollege.SelectedIndex = 0;
            ddlSemster.SelectedIndex = 0;
            ddlProgram.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

        }
    }
}