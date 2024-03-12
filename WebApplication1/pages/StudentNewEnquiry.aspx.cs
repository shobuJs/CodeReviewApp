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
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
using System.Text;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using System.Net.Mail;
using DynamicAL_v2;
using _NVP;
using System.Collections.Specialized;
using EASendMail;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
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
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
using System.Text;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using System.Net.Mail;
using DynamicAL_v2;
using _NVP;
using System.Collections.Specialized;
using EASendMail;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
public partial class ACADEMIC_StudentNewEnquiry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objUCS = new StudentController();
    Student_Acd objus = new Student_Acd();
    UserController objUC = new UserController();
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
                FillDropDown();
                objCommon.FillDropDownList(ddlSource, "ACD_SOURCETYPE", "SOURCETYPENO", "SOURCETYPENAME", "SOURCETYPENO>0", "SOURCETYPENAME");
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }

                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]));
                ViewState["usertype"] = ua_type;
            }          
        }       
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }
        objCommon.SetLabelData("0");//for label
        objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
    }
    
    protected void FillDropDown()
    {
        DataSet ds = objUCS.GetRegistrationBulkDetails(Convert.ToString(0), Convert.ToString(0), 0);
        ddlMobileCode.DataSource = ds.Tables[0];
        ddlMobileCode.DataValueField = "COUNTRYNO";
        ddlMobileCode.DataTextField = "COUNTRYNAME";
        ddlMobileCode.DataBind();
        ddlMobileCode.SelectedValue = "212";
        txtMobileNo.Text = "0";

        ddlHomeTelMobileCode.DataSource = ds.Tables[0];
        ddlHomeTelMobileCode.DataValueField = "COUNTRYNO";
        ddlHomeTelMobileCode.DataTextField = "COUNTRYNAME";
        ddlHomeTelMobileCode.DataBind();
        ddlHomeTelMobileCode.SelectedValue = "212";


        ddlStudyType.DataSource = ds.Tables[1];
        ddlStudyType.DataValueField = "UA_SECTION";
        ddlStudyType.DataTextField = "UA_SECTIONNAME";
        ddlStudyType.DataBind();
    }   
    protected void ddlStudyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStudyType.SelectedIndex > 0)
            {
                objCommon.FillListBox(ddlProgramIntrested, "ACD_AREA_OF_INTEREST I INNER JOIN ACD_COLLEGE_DEGREE_BRANCH D ON I.AREA_INT_NO = D.AREA_INT_NO ", "DISTINCT D.AREA_INT_NO", "AREA_INT_NAME", "D.AREA_INT_NO > 0 AND D.ACTIVE = 1 AND UGPGOT=" + Convert.ToInt32(ddlStudyType.SelectedValue), "AREA_INT_NAME");
                //objCommon.FillListBox(ddlProgramIntrested, "ACD_AREA_OF_INTEREST I INNER JOIN ACD_COLLEGE_DEGREE_BRANCH D ON I.AREA_INT_NO = D.AREA_INT_NO INNER JOIN ACD_ADMISSION_CONFIG A ON D.COLLEGE_ID = A.COLLEGE_ID AND D.DEGREENO = A.DEGREENO AND D.BRANCHNO = A.BRANCHNO AND D.UGPGOT = A.UGPG", "DISTINCT D.AREA_INT_NO", "AREA_INT_NAME", "D.AREA_INT_NO > 0 AND ADMBATCH = (SELECT MAX(ADMBATCH) FROM ACD_ADMISSION_CONFIG) AND UGPGOT=" + Convert.ToInt32(ddlStudyType.SelectedValue) + " AND ((CONVERT(VARCHAR(8),GETDATE(),112)) BETWEEN (CONVERT(VARCHAR(8),ADMSTRDATE,112)) AND (CONVERT(VARCHAR(8),ADMENDDATE,112)))", "AREA_INT_NAME");
            }
            else
            {
                ddlProgramIntrested.Items.Clear();
                ddlPreference.Items.Clear();
            }
            if (ddlProgramIntrested.Items.Count == 0)
            {
                objCommon.DisplayMessage(updenquiry, "Program Interested in not found Please contact online admission department!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlProgramIntrested_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Intrest = "0";
        foreach (ListItem items in ddlProgramIntrested.Items)
        {
            if (items.Selected == true)
            {
                Intrest += items.Value + ',';
            }
        }
        objCommon.FillListBox(ddlPreference, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_AREA_OF_INTEREST AOF ON A.AREA_INT_NO = AOF.AREA_INT_NO INNER JOIN ACD_BRANCH B ON A.BRANCHNO = B.BRANCHNO INNER JOIN ACD_DEGREE D ON A.DEGREENO = D.DEGREENO ", "DISTINCT CONVERT(NVARCHAR(10),A.COLLEGE_ID) +','+ CONVERT(NVARCHAR(10),A.DEGREENO) + ',' + CONVERT(NVARCHAR(10),A.BRANCHNO)", "D.CODE + '-' + LONGNAME", "A.ACTIVE = 1 AND UGPGOT = " + Convert.ToInt32(ddlStudyType.SelectedValue) + " AND B.BRANCHNO > 0 AND A.AREA_INT_NO IN(" + Intrest.TrimEnd(',') + ")", "");
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtDob.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtDob.Text).Year > 2004 || txtDob.Text.Length < 1)
                {
                    objCommon.DisplayMessage(updenquiry, "Date of Birth is not valid", this.Page);
                    return;
                }
            }
            if (Convert.ToString(txtNIC.Text) == string.Empty && Convert.ToString(txtPassport.Text) == string.Empty)
            {
                objCommon.DisplayMessage(updenquiry, "Passport No. OR NIC(National Identity card) is Required !", this.Page);
                return;
            }
            DataSet UserInfoDS = objUCS.GetRegistrationBulkDetails(Convert.ToString(txtEmailid.Text.Trim()), Convert.ToString(txtMobileNo.Text.Trim()), Convert.ToInt32(ddlStudyType.SelectedValue));
            if (UserInfoDS.Tables[2].Rows.Count > 0)
            {
                if (UserInfoDS.Tables[2].Rows[0]["EMAILID"].ToString() == txtEmailid.Text.Trim() && UserInfoDS.Tables[2].Rows[0]["USER_PASSWORD"].ToString() != "")
                {
                   
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#confirm').modal('show')", true);
                    return;
                }
                else if (UserInfoDS.Tables[2].Rows[0]["MOBILENO"].ToString() == txtMobileNo.Text.Trim() && UserInfoDS.Tables[2].Rows[0]["USER_PASSWORD"].ToString() != "")
                {
                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#confirm').modal('show')", true);
                    return;
                }
                if (UserInfoDS.Tables[2].Rows[0]["EMAILID"].ToString() == txtEmailid.Text.Trim() && UserInfoDS.Tables[2].Rows[0]["MOBILENO"].ToString() == txtMobileNo.Text.Trim() && UserInfoDS.Tables[2].Rows[0]["USER_PASSWORD"].ToString() == "")
                {
                    ViewState["ResendMailMSG"] = "yes";
                    ViewState["userno"] = UserInfoDS.Tables[2].Rows[0]["USERNO"].ToString();
                    Session["EnqueryUserName"] = UserInfoDS.Tables[2].Rows[0]["USERNAME"].ToString();
                    lbluserMsg.Text = "Account already exist !!!";
                    objCommon.DisplayMessage(updenquiry, "Account already exist !!!", this.Page);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#confirm').modal('show')", true);
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(updenquiry, "Account already exist !!!", this.Page);
                    lbluserMsg.Text = "Account already exist !!!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#confirm').modal('show')", true);
                    return;
                }
            }
            if (chkConfirm.Checked == true)
            {
                int IsExists = 0;
                IsExists = Convert.ToInt32(UserInfoDS.Tables[3].Rows[0]["USER_COUNT"].ToString());

                if (IsExists == 0)
                {
                    DataTable dtRecord = new DataTable();
                    DataColumn dc = new DataColumn();
                    dc = new DataColumn("FIRSTNAME");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("EMAILID");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("MOBILENO");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("MOBILECODE");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("DATEOFBIRTH");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("PASSPORTNO");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("NIC");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("PROGRAMINTRESTS");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("COLLEGE_ID");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("DEGREENO");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("BRANCHNO");
                    dtRecord.Columns.Add(dc);
                    dc = new DataColumn("GENDER");
                    dtRecord.Columns.Add(dc);
                    foreach (ListItem items in ddlProgramIntrested.Items)
                    {
                        if (items.Selected == true)
                        {
                            objus.programIntrests += items.Value + ',';
                        }
                    }
                    objus.programIntrests = objus.programIntrests.Substring(0, objus.programIntrests.Length - 1);
                    foreach (ListItem items in ddlPreference.Items)
                    {
                        if (items.Selected == true)
                        {
                            DataRow dr = dtRecord.NewRow();

                            dr["FIRSTNAME"] = txtFullName.Text.Trim().ToString().ToUpper();
                            dr["EMAILID"] = txtEmailid.Text.Trim().ToString();
                            dr["MOBILECODE"] = ddlMobileCode.Text;
                            dr["MOBILENO"] = txtMobileNo.Text;
                            dr["DATEOFBIRTH"] = Convert.ToString(txtDob.Text);
                            dr["PASSPORTNO"] = txtPassport.Text;
                            dr["NIC"] = txtNIC.Text;
                            dr["GENDER"] = rdGender.SelectedItem.Text;
                            dr["PROGRAMINTRESTS"] = objus.programIntrests;
                            string[] Split = items.Value.Split(',');
                            dr["COLLEGE_ID"] = Split[0];
                            dr["DEGREENO"] = Split[1];
                            dr["BRANCHNO"] = Split[2];
                            dtRecord.Rows.Add(dr);

                        }
                    }
                    string dob = "";
                    objus.EMAILID = txtEmailid.Text.Trim().ToString();
                    objus.MOBILENO = txtMobileNo.Text;
                    objus.FIRSTNAME = txtFullName.Text.Trim().ToString().ToUpper();
                    objus.LASTNAME = txtLastName.Text.Trim().ToString().ToUpper();
                    objus.Homemobileno = txtHomeMobileNo.Text.Trim().ToString();
                    objus.UGPG = Convert.ToInt32(ddlStudyType.SelectedValue);
                    objus.ADMTYPE = 1;
                    objus.AdmBatch = Convert.ToInt32(UserInfoDS.Tables[4].Rows[0]["ADMBATCH"].ToString());
                    dob = Convert.ToString(txtDob.Text);
                    objus.PassportNo = txtPassport.Text;
                    objus.NIC = txtNIC.Text;
                    objus.Gender = rdGender.SelectedValue;
                    objus.MobileCode = ddlMobileCode.SelectedValue;
                    objus.source = Convert.ToInt32(ddlSource.SelectedValue);
                    int ret = 0;
                    if (ViewState["ResendMailMSG"] != null)
                    {
                        objus.USERNO = Convert.ToInt32(ViewState["userno"]);
                        if (Convert.ToInt32(UserInfoDS.Tables[2].Rows[0]["UGPGOT"].ToString()) == Convert.ToInt32(ddlStudyType.SelectedValue))
                        {
                            //ret = Convert.ToInt32(ViewState["userno"]);
                            ret = objUCS.RegisterNewUser(objus, dtRecord, 2, Convert.ToString(ddlHomeTelMobileCode.SelectedValue), dob);
                        }
                        else
                        {
                            objCommon.DisplayMessage(updenquiry, "You are not change the Study Type already registered with another study type", this.Page);
                            return;
                        }

                    }
                    else if (ViewState["ResendMailMSG"] == null)
                    {
                        objus.USERNO = 0;
                        ret = objUCS.RegisterNewUser(objus, dtRecord, 1, Convert.ToString(ddlHomeTelMobileCode.SelectedValue), dob);
                        objCommon.DisplayMessage(updenquiry, "Data Submit Successfully", this.Page);
                        DataSet dsUserContact = null;
                        dsUserContact = objUC.GetEmailTamplateandUserDetails(txtEmailid.Text.ToString(), txtMobileNo.Text.ToString(), "0", Convert.ToInt32(101010));

                        string firstname = "", username = "", message = "", emailid = "";
                        if (dsUserContact != null && dsUserContact.Tables[0].Rows.Count > 0)
                        {
                            firstname = (dsUserContact.Tables[0].Rows[0]["FIRSTNAME"].ToString());
                            username = (dsUserContact.Tables[0].Rows[0]["USERNAME"].ToString());
                            emailid = (dsUserContact.Tables[0].Rows[0]["EMAILID"].ToString());
                            string Subject = dsUserContact.Tables[1].Rows[0]["SUBJECT"].ToString();
                            message = dsUserContact.Tables[1].Rows[0]["TEMPLATE"].ToString();
                            SmtpMail oMail = new SmtpMail("TryIt");

                            oMail.From = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]);

                            oMail.To = txtEmailid.Text;

                            oMail.Subject = Subject;

                            oMail.HtmlBody = message;

                            oMail.HtmlBody = oMail.HtmlBody.Replace("[USERFIRSTNAME]", firstname.ToString());
                            oMail.HtmlBody = oMail.HtmlBody.Replace("[USERNAME]", username.ToString());
                            oMail.HtmlBody = oMail.HtmlBody.Replace("[EMAILID]", txtEmailid.Text.ToString());

                            // SmtpServer oServer = new SmtpServer("smtp.live.com");
                            SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022

                            oServer.User = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL"]);
                            oServer.Password = Convert.ToString(dsUserContact.Tables[2].Rows[0]["SLIIT_EMAIL_PWD"]);

                            oServer.Port = 587;

                            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();

                            oSmtp.SendMail(oServer, oMail);
                        }
                        //PushMailServer("Registration");
                        clearcontrol();
                    }

                }
                else
                {
                    objCommon.DisplayMessage(updenquiry, "You are already registered with this details!", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updenquiry, "Please Confirm Information Before Submit", this.Page);

            }
        }
        catch (Exception ex)
        {

            throw;

        }
    }
    private void clearcontrol()
    {
        txtFullName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        txtEmailid.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        txtHomeMobileNo.Text = string.Empty;
        txtDob.Text = string.Empty;
        txtPassport.Text = string.Empty;
        txtNIC.Text = string.Empty;
        ddlStudyType.SelectedIndex = 0;
        ddlPreference.SelectedValue = null;
        ddlProgramIntrested.SelectedValue = null;
        ddlSource.SelectedIndex = 0;
        rdGender.SelectedValue = null;
        chkConfirm.Checked = false;

    }
}