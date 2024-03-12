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
using DotNetIntegrationKit;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml;
using System.Net;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using _NVP;
using System.Collections.Specialized;
using EASendMail;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;

using Microsoft.WindowsAzure.Storage;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.Script.Serialization;
using RestSharp;

public partial class ACADEMIC_Navision_Status : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
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
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }
            }
            objCommon.SetLabelData("0");//for label
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header          
        }

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CollegeMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CollegeMaster.aspx");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            string[] date;
            if (Convert.ToString(hdnDate.Value) == string.Empty || Convert.ToString(hdnDate.Value) == "0")
            {
                date = ",".Split(',');
                objCommon.DisplayMessage(this.Page, "Please Select Date Range !!!", this.Page);
                return;
            }
            else
            {
                date = hdnDate.Value.ToString().Split('-');
                date[0] = Convert.ToDateTime(date[0]).ToString("dd/MM/yyyy");
                date[1] = Convert.ToDateTime(date[1]).ToString("dd/MM/yyyy");
            }
            string SP_Parameters = ""; string Call_Values = ""; string SP_Name = "";
            SP_Name = "PKG_ACD_GET_NAVISION_DETAILS";
            SP_Parameters = "@P_FROM_DATE,@P_TO_DATE";
            Call_Values = "" + date[0] + "," +  date[1] + "";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                Panel1.Visible = true;
                LvResponceData.DataSource = ds;
                LvResponceData.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "No Record Found !!!", this.Page);
                Panel1.Visible = false;
                LvResponceData.DataSource = ds;
                LvResponceData.DataBind();
            }
            if (hdnDate.Value != "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        Response.Redirect(Request.Url.ToString());
    }
    protected void btnShowChange_Click(object sender, EventArgs e)
    {
        try
        {
            divlknavision.Attributes.Remove("class");
            string[] date;
            if (Convert.ToString(hdnDate2.Value) == string.Empty || Convert.ToString(hdnDate2.Value) == "0")
            {
                date = ",".Split(',');
                objCommon.DisplayMessage(this.Page, "Please Select Date Range !!!", this.Page);
                return;
            }
            else
            {
                date = hdnDate2.Value.ToString().Split('-');
                date[0] = Convert.ToDateTime(date[0]).ToString("dd/MM/yyyy");
                date[1] = Convert.ToDateTime(date[1]).ToString("dd/MM/yyyy");
            }
            string SP_Parameters = ""; string Call_Values = ""; string SP_Name = "";
            SP_Name = "PKG_ACD_GET_NAVISION_DETAILS_CHANGES";
            SP_Parameters = "@P_FROM_DATE,@P_TO_DATE,@P_STATUS";
            Call_Values = "" + date[0] + "," + date[1] + "," + ddlStatus.SelectedItem + "";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                Panel2.Visible = true;
                ListviewChanges.DataSource = ds;
                ListviewChanges.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "No Record Found !!!", this.Page);
                btnSubmit.Visible = false;
                Panel2.Visible = false;
                ListviewChanges.DataSource = ds;
                ListviewChanges.DataBind();
            }
            if (hdnDate2.Value != "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Src", "Setdate2('" + hdnDate2.Value + "');", true);
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnCancelChange_Click(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
        divlknavision.Attributes.Remove("class");
        hdnDate2.Value = null;
        ddlStatus.SelectedIndex = 0;
        Panel2.Visible = false;
        ListviewChanges.DataSource = null;
        ListviewChanges.DataBind();
    }
    protected void lknavision_Click(object sender, EventArgs e)
    {
        divlkchange.Attributes.Remove("class");
        divlknavision.Attributes.Add("class", "active");
        divresend.Visible = false;
        divnavision.Visible = true; 
        hdnDate2.Value = "";
        Panel2.Visible = false;
        ListviewChanges.DataSource = null;
        ListviewChanges.DataBind();
    }
    protected void lkchange_Click(object sender, EventArgs e)
    {
        divlknavision.Attributes.Remove("class");
        divlkchange.Attributes.Add("class", "active");
        divnavision.Visible = false;
        divresend.Visible = true;
        Panel1.Visible = false;
        LvResponceData.DataSource = null;
        LvResponceData.DataBind();
        hdnDate.Value = "";
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        divlknavision.Attributes.Remove("class");
        int count = 0;
        string SP_Parameters = ""; string Call_Values = ""; string SP_Name = "";
        string parameter = "";
        foreach (ListViewDataItem dataitem in ListviewChanges.Items)
        {
            CheckBox chkBox = dataitem.FindControl("chkRegister") as CheckBox;
            HiddenField hdfidno = dataitem.FindControl("hdfidno") as HiddenField;
            HiddenField hdftempdcr_no = dataitem.FindControl("hdftempdcr_no") as HiddenField;
            if (chkBox.Checked == true)
            {
                count++;
                SP_Name = "PKG_GET_DETAILS_FOR_API_STUDENT_DETAILS";
                SP_Parameters = "@P_IDNO";
                Call_Values = "" + Convert.ToInt32(hdfidno.Value) + "";
                DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
                if (Convert.ToString(ddlStatus.SelectedItem) == "Applicant_Preview")
                {
                    if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                    {
                        var client = new RestClient("http://172.16.68.55:7048/DynamicsNAV80/OData/Company('SLIIT')/studentdetails?$format=json");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Authorization", "Basic SU5UVVNFUjpmRGVWK2JOTGFlQlZuZVFZaFNiczBMZ3RpQjdHNWlDZDJxT1B1dS9KVXRzPQ==");
                        request.AddHeader("Content-Type", "application/json");
                        request.AddParameter("application/json", "{\r\n\"No\": \"" + ds.Tables[0].Rows[0]["REGISTRATION_NO"].ToString() + "\",\r\n  \"SLR_No\": \"" + ds.Tables[0].Rows[0]["APPLICATION_NO"].ToString() + "\",\r\n  \"Search_Name\": \"" + ds.Tables[0].Rows[0]["NAME_WITH_INITIAL"].ToString() + "\",\r\n  \"Name\": \"" + ds.Tables[0].Rows[0]["STUDNAME"].ToString() + "\",\r\n  \"Address\": \"" + ds.Tables[0].Rows[0]["PADDRESS"].ToString() + "\",\r\n  \"Address_2\": \"" + ds.Tables[0].Rows[0]["ADDRESS_2"].ToString() + "\",\r\n  \"Phone_No\": \"" + ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() + "\",\r\n  \"Gen_Bus_Posting_Group\": \"" + ds.Tables[0].Rows[0]["GEN_BUS_POSTING_GROUP"].ToString() + "\",\r\n  \"Email\": \"" + ds.Tables[0].Rows[0]["EMAILID"].ToString() + "\",\r\n  \"VAT_Bus_Posting_Group\": \"" + ds.Tables[0].Rows[0]["VAT_BUS_POSTING_GROUP"].ToString() + "\",\r\n  \"Customer_Type\": \"" + ds.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString() + "\",\r\n  \"Faculty\": \"" + ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString() + "\",\r\n  \"Specialization\": \"" + ds.Tables[0].Rows[0]["LONGNAME"].ToString() + "\",\r\n  \"University\": \"" + ds.Tables[0].Rows[0]["CAMPUSNAME"].ToString() + "\",\r\n  \"NIC\": \"" + ds.Tables[0].Rows[0]["NICNO"].ToString() + "\",\r\n  \"Gender\": \"M\",\r\n  \"Mobile_No\": \"" + ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() + "\",\r\n  \"Permanent_Address\": \"" + ds.Tables[0].Rows[0]["PADDRESS"].ToString() + "\",\r\n  \"Programme\": \"" + ds.Tables[0].Rows[0]["DEGREENAME"].ToString() + "\"\r\n}", ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);
                        Console.WriteLine(response.Content);
                        parameter = "Registration No:" + ds.Tables[0].Rows[0]["REGISTRATION_NO"].ToString() + "SLR_No:" + ds.Tables[0].Rows[0]["APPLICATION_NO"].ToString() + "Search_Name:" + ds.Tables[0].Rows[0]["NAME_WITH_INITIAL"].ToString() + "Name:" + ds.Tables[0].Rows[0]["STUDNAME"].ToString() + "Address:" + ds.Tables[0].Rows[0]["PADDRESS"].ToString() + "Address_2:" + ds.Tables[0].Rows[0]["ADDRESS_2"].ToString() + "Phone_No:" + ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() + "Gen_Bus_Posting_Group:" + ds.Tables[0].Rows[0]["GEN_BUS_POSTING_GROUP"].ToString() + "Email:" + ds.Tables[0].Rows[0]["EMAILID"].ToString() + "VAT_Bus_Posting_Group:" + ds.Tables[0].Rows[0]["VAT_BUS_POSTING_GROUP"].ToString() + "Customer_Type:" + ds.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString() + "Faculty:" + ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString() + "Specialization:" + ds.Tables[0].Rows[0]["LONGNAME"].ToString() + "University:" + ds.Tables[0].Rows[0]["CAMPUSNAME"].ToString() + "NIC:" + ds.Tables[0].Rows[0]["NICNO"].ToString() + "Mobile_No:" + ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() + "Permanent_Address:" + ds.Tables[0].Rows[0]["PADDRESS"].ToString() + "Programme:" + ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                        string SP_Name1 = "PKG_ACD_RESPONSE_FOR_STUDENT_API";
                        string SP_Parameters1 = "@P_IDNO,@P_RESULT,@P_STATUS,@P_RESPONCE_VALUE,@P_OUTPUT";
                        string Call_Values1 = "" + Convert.ToInt32(hdfidno.Value) + "," + Convert.ToString(response.StatusDescription) + "," + ddlStatus.SelectedItem + "," + Convert.ToString(parameter) + ",0";
                        string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);
                    }
                }
                else if (Convert.ToString(ddlStatus.SelectedItem) != "Applicant_Preview")
                {
                    SP_Name = "PKG_GET_DETAILS_FOR_API";
                    SP_Parameters = "@P_IDNO,@P_TEMP_DCR_NO";
                    Call_Values = "" + Convert.ToInt32(hdfidno.Value) + "," + Convert.ToInt32(hdftempdcr_no.Value) + "";
                    DataSet ds1 = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
                    if (ds1.Tables != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                        var clientUrl = new RestClient("http://172.16.68.55:7048/DynamicsNAV80/OData/Company('SLIIT')/studentreceipts?$format=json");
                        clientUrl.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Authorization", "Basic SU5UVVNFUjpmRGVWK2JOTGFlQlZuZVFZaFNiczBMZ3RpQjdHNWlDZDJxT1B1dS9KVXRzPQ==");
                        request.AddHeader("Content-Type", "application/json");
                        request.AddParameter("application/json", "{\r\n\"Journal_Template\": \"" + ds.Tables[0].Rows[0]["JOURNAL_TEMPLATE_NAME"].ToString() + "\",\r\n \"Journal_Batch\": \"" + ds.Tables[0].Rows[0]["JOUNRAL_BATCH"].ToString() + "\",\r\n \"Student_No_Referance\": \"" + ds.Tables[0].Rows[0]["REGISTRATION_NO"].ToString() + "\",\r\n \"SLR_No\": \"" + ds.Tables[0].Rows[0]["APPLICATION_NO"].ToString() + "\",\r\n \"Posting_Date\": \"" + ds.Tables[0].Rows[0]["POSTING_DATE"].ToString() + "\",\r\n \"Document_Type\": \"" + ds.Tables[0].Rows[0]["DOCUMENT_TYPE"].ToString() + "\",\r\n \"Description\": \"" + ds.Tables[0].Rows[0]["NAME_WITH_INITIAL"].ToString() + "\",\r\n \"Amount\": \"" + ds.Tables[0].Rows[0]["AMOUNT"].ToString() + "\",\r\n \"Bal_Account_No\": \"" + ds.Tables[0].Rows[0]["BAL_ACCOUNT_TYPE"].ToString() + "\",\r\n \"Bank_Deposit_Date\": \"" + ds.Tables[0].Rows[0]["BANK_DEPOSIT_DATE"].ToString() + "\",\r\n \"Narration\": \"" + ds.Tables[0].Rows[0]["NARRATION"].ToString() + "\",\r\n \"Reference\": \"" + ds.Tables[0].Rows[0]["DOCUMENT_NO"].ToString() + "\",\r\n \"Receipt_Type\": \"" + ds.Tables[0].Rows[0]["RECEIPT_TYPE"].ToString() + "\",\r\n \"External_Document_No\": \"" + ds.Tables[0].Rows[0]["EXTERNAL_DOCUMENT_NO"].ToString() + "\"\r\n}", RestSharp.ParameterType.RequestBody);
                        IRestResponse response = clientUrl.Execute(request);
                        parameter = "Journal_Template:" + ds.Tables[0].Rows[0]["JOURNAL_TEMPLATE_NAME"].ToString() + "Journal_Batch:" + ds.Tables[0].Rows[0]["JOUNRAL_BATCH"].ToString() + "Student_No_Referance:" + ds.Tables[0].Rows[0]["REGISTRATION_NO"].ToString() + "SLR_No:" + ds.Tables[0].Rows[0]["APPLICATION_NO"].ToString() + "Posting_Date:" + ds.Tables[0].Rows[0]["POSTING_DATE"].ToString() + "Document_Type:" + ds.Tables[0].Rows[0]["DOCUMENT_TYPE"].ToString() + "Description:" + ds.Tables[0].Rows[0]["NAME_WITH_INITIAL"].ToString() + "Amount:" + ds.Tables[0].Rows[0]["AMOUNT"].ToString() + "Bal_Account_No:" + ds.Tables[0].Rows[0]["BAL_ACCOUNT_TYPE"].ToString() + "Bank_Deposit_Date:" + ds.Tables[0].Rows[0]["BANK_DEPOSIT_DATE"].ToString() + "Narration:" + ds.Tables[0].Rows[0]["NARRATION"].ToString() + "Reference:" + ds.Tables[0].Rows[0]["DOCUMENT_NO"].ToString() + "Receipt_Type:" + ds.Tables[0].Rows[0]["RECEIPT_TYPE"].ToString() + "External_Document_No:" + ds.Tables[0].Rows[0]["EXTERNAL_DOCUMENT_NO"].ToString();
                        string SP_Name1 = "PKG_ACD_RESPONSE_FOR_STUDENT_API";
                        string SP_Parameters1 = "@P_IDNO,@P_RESULT,@P_STATUS,@P_RESPONCE_VALUE,@P_OUTPUT";
                        string Call_Values1 = "" + Convert.ToInt32(Convert.ToInt32(hdfidno.Value)) + "," + Convert.ToString(response.StatusDescription) + "," + ddlStatus.SelectedItem + "," + Convert.ToString(parameter) + ",0";
                        string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);
                    }
                }
            }
        }
        if (count == 0)
        {
            objCommon.DisplayMessage(this.Page, "Please Select At least One Checkbox !!!", this.Page);
            return;
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Record Save Successfully.!!!", this.Page);
        }
    }
}