
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

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net;
public partial class ACADEMIC_ChangeCoreSubject : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();


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
                ////Page Authorization
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                this.PopulateDropDownList();
                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                string IPADDRESS = string.Empty;

                IPADDRESS = ip.AddressList[0].ToString();
                ViewState["ipAddress"] = IPADDRESS;

                ViewState["action"] = "add";
                ViewState["idno"] = "0";
                if (Session["usertype"].ToString().Equals("1"))     //Only Admin 
                {
                    divOptions.Visible = false;
                    divCourses.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage("You are Not Authorized to View this Page. Contact Admin.", this.Page);
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
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
    protected void btnShow_Click(object sender, EventArgs e)
    {
        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");

        if (idno == "")
        {
            objCommon.DisplayMessage("Student Not Found for Entered Registration No.[" + txtRollNo.Text.Trim() + "]", this.Page);
        }
        else
        {
            ViewState["idno"] = idno;

            if (string.IsNullOrEmpty(ViewState["idno"].ToString()) || ViewState["idno"].ToString() == "0")
            {
                objCommon.DisplayMessage("Student with Registration No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                return;
            }
        }

        string schemeno = string.Empty;
        schemeno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "' AND DEGREENO IN(7,5) AND BRANCHNO !=35");
        if (string.IsNullOrEmpty(schemeno))
        {
            objCommon.DisplayMessage("This Core Subject Change Provision Is Only For BA and B.Sc", this.Page);
            return;
        }

        this.ShowDetails();

    }

    private void ShowDetails()
    {
        try
        {
            DataSet dsStudent = objSReg.GetStudInfoForChangeCoreSubject(Convert.ToInt32(ViewState["idno"].ToString()));

            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                    lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                    lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";
                    lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                    lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                    lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();
                    lblCollege.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                    lblRegNo.Text = dsStudent.Tables[0].Rows[0]["ENROLLNO"].ToString();
                    lblCoreSub.Text = dsStudent.Tables[0].Rows[0]["Core_Sub_Name"].ToString();

                    tblInfo.Visible = true;
                    divCourses.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_ChangeCoreSubject.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlCoreSubjet, "ACD_CORE_SUBJECT", "Core_SubID", "Core_Sub_Name", "Core_SubID > 0 ", "Core_SubID");
        ddlCoreSubjet.Focus();
    }
    protected void btnChange_Click(object sender, EventArgs e)
    {

        try
        {
            int idno = Convert.ToInt32(ViewState["idno"]);
            int core_SubId = Convert.ToInt32(ddlCoreSubjet.SelectedValue);
            int Uano = Convert.ToInt32(Session["userno"]);

            int ret = objSReg.GetChangeCoreSubject(idno, core_SubId, Uano);
            if (ret == 1)
            {
                objCommon.DisplayMessage("Subject Changed Successfully.", this.Page);

            }
            else
                objCommon.DisplayMessage("Sorry transaction Failed! Error in saving record.", this.Page);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_ChangeCoreSubject.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

        this.ShowDetails();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["idno"] = "0";
        divCourses.Visible = true;
        ddlCoreSubjet.Enabled = true;
        txtRollNo.Text = string.Empty;
        txtRollNo.Enabled = true;
        tblInfo.Visible = false;
    }
}