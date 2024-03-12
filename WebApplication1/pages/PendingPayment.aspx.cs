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
using System.Collections.Specialized;
using _NVP;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;

public partial class ACADEMIC_PendingPayment : System.Web.UI.Page
{
    private Common objCommon = new Common();
    private FeeCollectionController objFeeCollectionController = new FeeCollectionController();

    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

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
                    // Check User Authority
                    //this.CheckPageAuthorization();
                    ViewState["action"] = "add";
                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }

                    if (Session["usertype"].ToString() == "2")
                    {
                        DivSearch.Visible = false;
                        BindListView();
                    }
                    else
                    {
                        DivSearch.Visible = true;
                    }
                    //objCommon.SetLabelData("0");//for label
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FeesDetails.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeesDetails.aspx");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            string idno = "";

           // idno = objCommon.LookUp("ACD_STUDENT", "DISTINCT IDNO", "(REGNO='" + txtEnrollNo.Text.Trim() + "' OR ENROLLNO='" + txtEnrollNo.Text.Trim() + "')");

            idno = hdnClientId.Value;
            ViewState["IDNO"] = idno;
            if (idno == string.Empty)
            {
                divstudinfo.Visible = false;
                lvFeesDetails.DataSource = null;
                lvFeesDetails.DataBind();
                objCommon.DisplayMessage(updFeesDetails, "Record Not Found !!!", this.Page);
            }
            else
            {
                BindListView();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void BindListView()
    {
        try
        {
            if (Session["usertype"].ToString() == "2")
            {
                string SP_Name = "PKG_ACD_GET_SEMESTER_WISE_REMAINING_PAYMENT_DETAILS";
                string SP_Parameters = "@P_IDNO,@P_COMMAND_TYPE";
                string Call_Values = "" + Convert.ToInt32(Session["idno"]) + "," + 1 + "";

                DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

                if (ds.Tables != null && ds.Tables[2].Rows.Count > 0)
                {
                    lvPaidReceipts.DataSource = ds.Tables[2];
                    lvPaidReceipts.DataBind();
                }
                else
                {
                    lvPaidReceipts.DataSource = null;
                    lvPaidReceipts.DataBind();
                }
                if (ds.Tables != null && ds.Tables[1].Rows.Count > 0)
                {
                    lblCollege.Text = ds.Tables[1].Rows[0]["COLLEGE_NAME"].ToString();
                    lblStudName.Text = ds.Tables[1].Rows[0]["NAME_WITH_INITIAL"].ToString();
                    lblSex.Text = ds.Tables[1].Rows[0]["SEX"].ToString();
                    lblRegNo.Text = ds.Tables[1].Rows[0]["REGNO"].ToString();
                    lblenroll.Text = ds.Tables[1].Rows[0]["ENROLLNO"].ToString();
                    lblDegree.Text = ds.Tables[1].Rows[0]["DEGREENAME"].ToString() + '-' + ds.Tables[1].Rows[0]["LONGNAME"].ToString();
                    lblYear.Text = ds.Tables[1].Rows[0]["YEARNAME"].ToString();
                    lblSemester.Text = ds.Tables[1].Rows[0]["CURRENT_SEMESTER"].ToString();
                    lblMobileNo.Text = ds.Tables[1].Rows[0]["MOBILENO"].ToString();
                }
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    divstudinfo.Visible = true;
                    lvFeesDetails.DataSource = ds.Tables[0];
                    lvFeesDetails.DataBind();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        int Count = 0;

                        //foreach (ListViewDataItem row in lvFeesDetails.Items)
                        //{
                            HiddenField hdfSemesterNo = (HiddenField)lvFeesDetails.Items[i].FindControl("hdfSemesterNo");
                            LinkButton lnkPromissorynote = (LinkButton)lvFeesDetails.Items[i].FindControl("lnkPromissorynote");

                            if (hdfSemesterNo.Value.ToString() == ds.Tables[0].Rows[i]["SEMESTERNO"].ToString() && ds.Tables[0].Rows[i]["BALANCE_AMOUNT"].ToString() != string.Empty)
                            {
                                
                                DataView dv = new DataView();
                                dv = ds.Tables[0].DefaultView;
                                dv.RowFilter = "SEMESTERNO = " + hdfSemesterNo.Value;

                                if (dv.Count > 0)
                                {
                                    if (ds.Tables[0].Rows[i]["PROMISSORY_STATUS_NO"].ToString() == "1" || ds.Tables[0].Rows[i]["PROMISSORY_STATUS_NO"].ToString() == "2")
                                    {
                                        lnkPromissorynote.Visible = false;
                                        //break;
                                    }
                                    else if (ds.Tables[0].Rows[i]["RECON"].ToString() == "1")
                                    {
                                        lnkPromissorynote.Visible = false;
                                    }
                                    else if (ds.Tables[0].Rows[i]["DATE_STATUS"].ToString() == "0")
                                    {
                                        lnkPromissorynote.Visible = false;
                                    }
                                    //else if (Count == 2)
                                    //{
                                    //    lnkPromissorynote.Visible = true;
                                    //    //break;
                                    //}
                                    else
                                    {
                                        lnkPromissorynote.Visible = true;
                                    }
                                    Count++;
                                }
                                else
                                {
                                    lnkPromissorynote.Visible = false;
                                }
                            }
                            else
                            {
                                lnkPromissorynote.Visible = false;
                            }
                        //}
                        //Count = 0;
                    }
                }
                else
                {
                    divstudinfo.Visible = true;
                    lvFeesDetails.DataSource = null;
                    lvFeesDetails.DataBind();
                    objCommon.DisplayMessage(updFeesDetails, "Pending Amount Record Not Found !!!", this.Page);
                }
            }
            else
            {
                string SP_Name = "PKG_ACD_GET_SEMESTER_WISE_REMAINING_PAYMENT_DETAILS";
                string SP_Parameters = "@P_IDNO,@P_COMMAND_TYPE,@P_OUTPUT";
                string Call_Values = "" + Convert.ToInt32(ViewState["IDNO"]) + "," + 1 + ",0";

                DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

                if (ds.Tables != null && ds.Tables[2].Rows.Count > 0)
                {
                    lvPaidReceipts.DataSource = ds.Tables[2];
                    lvPaidReceipts.DataBind();
                }
                else
                {
                    lvPaidReceipts.DataSource = null;
                    lvPaidReceipts.DataBind();
                }
                if (ds.Tables != null && ds.Tables[1].Rows.Count > 0)
                {
                    divstudinfo.Visible = true;
                    lblCollege.Text = ds.Tables[1].Rows[0]["COLLEGE_NAME"].ToString();
                    lblStudName.Text = ds.Tables[1].Rows[0]["NAME_WITH_INITIAL"].ToString();
                    lblSex.Text = ds.Tables[1].Rows[0]["SEX"].ToString();
                    lblRegNo.Text = ds.Tables[1].Rows[0]["REGNO"].ToString();
                    lblenroll.Text = ds.Tables[1].Rows[0]["ENROLLNO"].ToString();
                    lblDegree.Text = ds.Tables[1].Rows[0]["DEGREENAME"].ToString() + '-' + ds.Tables[1].Rows[0]["LONGNAME"].ToString();
                    lblYear.Text = ds.Tables[1].Rows[0]["YEARNAME"].ToString();
                    lblSemester.Text = ds.Tables[1].Rows[0]["CURRENT_SEMESTER"].ToString();
                    lblMobileNo.Text = ds.Tables[1].Rows[0]["MOBILENO"].ToString();
                }
                else
                {
                    divstudinfo.Visible = false;
                }
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    divstudinfo.Visible = true;
                    lvFeesDetails.DataSource = ds.Tables[0];
                    lvFeesDetails.DataBind();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        int Count = 0;

                        //foreach (ListViewDataItem row in lvFeesDetails.Items)
                        //{
                            HiddenField hdfSemesterNo = (HiddenField)lvFeesDetails.Items[i].FindControl("hdfSemesterNo");
                            LinkButton lnkPromissorynote = (LinkButton)lvFeesDetails.Items[i].FindControl("lnkPromissorynote");

                            if (hdfSemesterNo.Value.ToString() == ds.Tables[0].Rows[i]["SEMESTERNO"].ToString() && ds.Tables[0].Rows[i]["BALANCE_AMOUNT"].ToString() != string.Empty)
                            {
                                Count++;
                                DataView dv = new DataView();
                                dv = ds.Tables[0].DefaultView;
                                dv.RowFilter = "SEMESTERNO = " + hdfSemesterNo.Value;

                                if (dv.Count > 0)
                                {
                                    if (ds.Tables[0].Rows[i]["PROMISSORY_STATUS_NO"].ToString() == "1" || ds.Tables[0].Rows[i]["PROMISSORY_STATUS_NO"].ToString() == "2")
                                    {
                                        lnkPromissorynote.Visible = false;
                                        //break;
                                    }
                                    else if (ds.Tables[0].Rows[i]["RECON"].ToString() == "1")
                                    {
                                        lnkPromissorynote.Visible = false;
                                    }
                                    else if (ds.Tables[0].Rows[i]["DATE_STATUS"].ToString() == "0")
                                    {
                                        lnkPromissorynote.Visible = false;
                                    }
                                    //else if (Count == 2)
                                    //{
                                    //    lnkPromissorynote.Visible = true;
                                    //    break;
                                    //}
                                    else
                                    {
                                        lnkPromissorynote.Visible = true;
                                    }
                                }
                                else
                                {
                                    lnkPromissorynote.Visible = false;
                                }
                            }
                            else
                            {
                                lnkPromissorynote.Visible = false;
                            }
                        //}
                        //Count = 0;
                    }
                }
                else
                {
                    divstudinfo.Visible = false;
                    lvFeesDetails.DataSource = null;
                    lvFeesDetails.DataBind();
                    objCommon.DisplayMessage(updFeesDetails, "Pending Amount Record Not Found !!!", this.Page);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnPayOffline_Click(object sender, EventArgs e)
    {
        try
        {
            BindBank();
            LinkButton btnOffline = sender as LinkButton;
            txtchallanAmount.Text = Convert.ToDecimal(btnOffline.CommandName.Split('$')[0]).ToString("N");

            ViewState["OFFLINE_SEMESTERNO"] = btnOffline.CommandName.Split('$')[1];
            ViewState["OFFLINE_SESSIONNO"] = btnOffline.CommandName.Split('$')[2];
            ViewState["OFFLINE_INSTALLMENT_AMOUNT"] = btnOffline.CommandArgument.Split('$')[0];
            GetPreviousReceipt();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#myModalChallan').modal('show')", true);
        }
        catch (Exception ex)
        {

        }
    }

    protected void BindBank()
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("ACD_BANK", "DISTINCT BANKNO", "BANKNAME", "ISNULL(ACTIVE,0)=1 AND BANKNO > 0", "BANKNO");
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlbank.Items.Clear();
                ddlbank.Items.Add("Please Select");
                ddlbank.SelectedItem.Value = "0";
                ddlbank.DataSource = ds;
                ddlbank.DataValueField = "BANKNO";
                ddlbank.DataTextField = "BANKNAME";
                ddlbank.DataBind();
            }
        }
        catch (Exception ex)
        { 
        
        }
    }
    private void CreateCustomerRef(int Idno, int Sessionno, int Semesterno)
    {
        try
        {
            Random rnd = new Random();
            int ir = rnd.Next(01, 10000);

            hdfOrderID.Value = Convert.ToString(Convert.ToInt32(Idno) + Convert.ToString(Sessionno) + Convert.ToString(Semesterno) + ir);
            txtOrderid.Text = hdfOrderID.Value;
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnPayOnline_Click(object sender, EventArgs e)
    {
        try
        {
            string Call_Values1 = "";
            FeeCollectionController feeController = new FeeCollectionController();
            LinkButton btnonline = sender as LinkButton;

            if (Session["usertype"].ToString() == "2")
            {
                CreateCustomerRef(Convert.ToInt32(Session["idno"]), Convert.ToInt32(btnonline.CommandName.Split('$')[2]), Convert.ToInt32(btnonline.CommandName.Split('$')[1]));
            }
            else
            {
                CreateCustomerRef(Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(btnonline.CommandName.Split('$')[1]), Convert.ToInt32(btnonline.CommandName.Split('$')[2]));
            }

                string SP_Name = "PKG_ACD_GET_DOWN_PAYEMNT_UPLOAD_DETAILS";
                string SP_Parameters = "@P_IDNO,@P_SEMESTERNO,@P_RECIEPT_CODE,@P_INSTALL_NO";
                DataSet ds = new DataSet();

                if (Session["usertype"].ToString() == "2")
                {
                    string Call_Values = "" + Convert.ToInt32(Session["idno"]) + "," + Convert.ToInt32(btnonline.CommandName.Split('$')[1]) + "," + "TF" + "," + btnonline.CommandArgument.Split('$')[0];
                    ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
                }
                else
                {
                    string Call_Values = "" + Convert.ToInt32(ViewState["IDNO"]) + "," + Convert.ToInt32(btnonline.CommandName.Split('$')[1]) + "," + "TF" + "," + btnonline.CommandArgument.Split('$')[0];
                    ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
                }

                if (Convert.ToInt32(ds.Tables[2].Rows[0]["CHECKPAY"]) > 0)
                {
                    objCommon.DisplayMessage(this.Page, "Deposit Slip Already Uploaded Unable To Pay In Cash !!!", this.Page);
                    return;
                }
                if (Convert.ToInt32(ds.Tables[3].Rows[0]["DCRTEMPNO"]) > 0)
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Already Exists !!!", this.Page);
                    return;
                }

            string SP_Name1 = "PKG_ACD_GET_SEMESTER_WISE_REMAINING_PAYMENT_DETAILS";
            string SP_Parameters1 = "@P_IDNO,@P_COMMAND_TYPE,@P_SESSIONNO,@P_SEMESTERNO,@P_AMOUNT,@P_ORDER_ID,@P_UA_NO,@P_INSTALL_NO,@P_DEPOSIT_DATE,@P_OUTPUT";
            if (Session["usertype"].ToString() == "2")
            {
                Call_Values1 = "" + Convert.ToInt32(Session["idno"]) + ",2," + Convert.ToInt32(btnonline.CommandName.Split('$')[2]) + "," +
                Convert.ToInt32(btnonline.CommandName.Split('$')[1]) + "," + Convert.ToDecimal(btnonline.CommandName.Split('$')[0]) + "," + Convert.ToString(hdfOrderID.Value) + "," + Session["userno"].ToString() + "," + btnonline.CommandArgument.Split('$')[0] + "," + System.DateTime.Now.ToString() + ",0";
            }
            else
            {
                Call_Values1 = "" + Convert.ToInt32(ViewState["IDNO"]) + ",2," + Convert.ToInt32(btnonline.CommandName.Split('$')[2]) + "," +
                Convert.ToInt32(btnonline.CommandName.Split('$')[1]) + "," + Convert.ToDecimal(btnonline.CommandName.Split('$')[0]) + "," + Convert.ToString(hdfOrderID.Value) + "," + Session["userno"].ToString() + "," + btnonline.CommandArgument.Split('$')[0] + "," + System.DateTime.Now.ToString() + ",0";
            }
            string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);

            if (que_out1 == "1")
            {
                objCommon.DisplayMessage(this.Page, "Record Saved Succesfully !!!", this.Page);
            }
            else
            {
                objCommon.DisplayUserMessage(updFeesDetails, "Failed To Done Online Payment.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }
    public void DeleteIFExits(string FileName)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }
    public int Blob_UploadDepositSlip(string ConStr, string ContainerName, string DocName, FileUpload FU, byte[] ChallanCopy)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;

        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.Properties.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            if (!cblob.Exists())
            {
                using (Stream stream = new MemoryStream(ChallanCopy))
                {
                    cblob.UploadFromStream(stream);
                }
            }
            //cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }

    protected void btnChallanSubmit_Click(object sender, EventArgs e)
    {
        byte[] ChallanCopy = null;
        try
        {
            if (Session["usertype"].ToString() == "2")
            {
                CreateCustomerRef(Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["OFFLINE_SESSIONNO"]), Convert.ToInt32(ViewState["OFFLINE_SEMESTERNO"]));
            }
            else
            {
                CreateCustomerRef(Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(ViewState["OFFLINE_SESSIONNO"]), Convert.ToInt32(ViewState["OFFLINE_SEMESTERNO"]));
            }

            if (txtchallanAmount.Text == "0" || txtchallanAmount.Text == "" || txtchallanAmount.Text == "0.00" || txtchallanAmount.Text.Substring(0, 1) == "-")
            {
                objCommon.DisplayMessage(updChallan, "Total Amount Cannot be Less than or Equal to Zero !!", this.Page);
                return;
            }

            if (ViewState["action"].ToString() != "Edit")
            {
                if (Convert.ToDecimal(txtchallanAmount.Text) > ((txtDepositAmount.Text == string.Empty ? Convert.ToDecimal(0) : Convert.ToDecimal(txtDepositAmount.Text))))
                {
                    objCommon.DisplayMessage(updChallan, "All Paid amount should not be less than actual amount to be paid !!!", this.Page);
                    return;
                }
            }
            int Count = 0;
            if (Session["usertype"].ToString() == "2")
            {
                Count = Convert.ToInt32(objCommon.LookUp("ACD_DCR_TEMP", "COUNT(1)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND PAY_SERVICE_TYPE=2"));
            }
            else
            {
                Count = Convert.ToInt32(objCommon.LookUp("ACD_DCR_TEMP", "COUNT(1)", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + " AND PAY_SERVICE_TYPE=2"));
            }

            if (ViewState["action"].ToString() != "Edit")
            {
                if (Count > 0)
                {
                    string Call_Values1 = "";
                    string SP_Name1 = "PKG_ACD_GET_PAID_AMOUNT_DETAILS";
                    string SP_Parameters1 = "@P_IDNO,@P_SEMESTERNO,@P_SESSIONNO";
                    if (Session["usertype"].ToString() == "2")
                    {
                        Call_Values1 = "" + Convert.ToInt32(Session["idno"]) + "," + Convert.ToInt32(ViewState["OFFLINE_SEMESTERNO"].ToString()) + "," + Convert.ToInt32(ViewState["OFFLINE_SESSIONNO"]);
                    }
                    else
                    {
                        Call_Values1 = "" + Convert.ToInt32(ViewState["IDNO"]) + "," + Convert.ToInt32(ViewState["OFFLINE_SEMESTERNO"].ToString()) + "," + Convert.ToInt32(ViewState["OFFLINE_SESSIONNO"]);
                    }
                    DataSet que_out1 = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);

                    if (que_out1.Tables[0] != null && que_out1.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToInt32(que_out1.Tables[0].Rows[0]["DOC_COUNT"].ToString()) > 0)
                        {
                            if (Convert.ToDecimal(que_out1.Tables[2].Rows[0]["TOTAL_AMT"].ToString()) <= Convert.ToDecimal(que_out1.Tables[1].Rows[0]["TOTAL_AMT"].ToString()))
                            {
                                objCommon.DisplayMessage(this.Page, "Deposit slip already uploaded !!!", this.Page);
                                return;
                            }
                            if (Convert.ToDecimal(que_out1.Tables[2].Rows[0]["TOTAL_AMT"].ToString()) <= Convert.ToDecimal(que_out1.Tables[3].Rows[0]["TOTAL_AMT"].ToString()))
                            {
                                objCommon.DisplayMessage(this.Page, "Deposit slip already uploaded !!!", this.Page);
                                return;
                            }
                        }
                    }
                }
            }
            if (FuChallan.HasFile)
            {
                string ext = System.IO.Path.GetExtension(FuChallan.FileName);
                if (ext == ".pdf" || ext == ".PDF" || ext == ".jpg" || ext == ".JPG" || ext == ".png" || ext == ".PNG" || ext == ".jpeg" || ext == ".JPEG")
                {
                    if (ViewState["action"].ToString() != "Edit")
                    {
                        ChallanCopy = objCommon.GetImageData(FuChallan);

                        int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, Convert.ToInt32(Session["idno"]) + "_ERP_" + Count + "_Higher_Sem_Slip", FuChallan, ChallanCopy);
                        if (retval == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                            return;
                        }
                        SubmitOfflineData(ChallanCopy);

                    }
                    else
                    {
                        if (ViewState["action"].ToString().Equals("Edit"))
                        {
                            ChallanCopy = objCommon.GetImageData(FuChallan);
                            int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, Convert.ToInt32(Session["idno"]) + "_ERP_" + Count + "_Higher_Sem_Slip", FuChallan, ChallanCopy);
                            if (retval == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                return;
                            }
                            ViewState["action"] = "Edit";
                            SubmitOfflineData(ChallanCopy);
                        }
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(updChallan, "Please Upload Deposit Slip !!!", this.Page);
                return;
            } 
        }
        catch (Exception ex)
        {

        }
    }

    private void SubmitOfflineData(byte[] ChallanCopy)
    {
        try
        {
            string Ext = Path.GetExtension(FuChallan.FileName);
            int Count = 0;
            if (Session["usertype"].ToString() == "2")
            {
                Count = Convert.ToInt32(objCommon.LookUp("ACD_DCR_TEMP", "COUNT(1)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND PAY_SERVICE_TYPE=2"));
            }
            else
            {
                Count = Convert.ToInt32(objCommon.LookUp("ACD_DCR_TEMP", "COUNT(1)", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + " AND PAY_SERVICE_TYPE=2"));
            }

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    string Call_Values1 = "";
                    string SP_Name1 = "PKG_ACD_GET_SEMESTER_WISE_REMAINING_PAYMENT_DETAILS";
                    string SP_Parameters1 = "@P_IDNO,@P_COMMAND_TYPE,@P_SESSIONNO,@P_SEMESTERNO,@P_AMOUNT,@P_ORDER_ID,@P_UA_NO,@P_FILENAME,@P_TEMP_DCR_NO,@P_BANKNO,@P_BANK_NAME,@P_BRANCH_NAME,@P_DEPOSIT_DATE,@P_INSTALL_NO,@P_OUTPUT";
                    if (Session["usertype"].ToString() == "2")
                    {
                        Call_Values1 = "" + Convert.ToInt32(Session["idno"]) + ",2," + Convert.ToInt32(ViewState["OFFLINE_SESSIONNO"]) + "," +
                        Convert.ToInt32(ViewState["OFFLINE_SEMESTERNO"]) + "," + Convert.ToDecimal(txtDepositAmount.Text) + "," + Convert.ToString(hdfOrderID.Value) + "," + Session["userno"].ToString() + "," + (Convert.ToInt32(Session["idno"]) + "_ERP_" + Count + "_Higher_Sem_Slip" + Ext) + ",0," + Convert.ToInt32(ddlbank.SelectedValue) + "," + Convert.ToString(ddlbank.SelectedItem.Text) + "," + txtBranchName.Text + "," + txtPaymentdate.Text + "," + ViewState["OFFLINE_INSTALLMENT_AMOUNT"] + ",0";
                    }
                    else
                    {
                        Call_Values1 = "" + Convert.ToInt32(ViewState["IDNO"]) + ",2," + Convert.ToInt32(ViewState["OFFLINE_SESSIONNO"]) + "," +
                        Convert.ToInt32(ViewState["OFFLINE_SEMESTERNO"]) + "," + Convert.ToDecimal(txtDepositAmount.Text) + "," + Convert.ToString(hdfOrderID.Value) + "," + Session["userno"].ToString() + "," + (Convert.ToInt32(ViewState["IDNO"]) + "_ERP_" + Count + "_Higher_Sem_Slip" + Ext) + ",0," + Convert.ToInt32(ddlbank.SelectedValue) + "," + Convert.ToString(ddlbank.SelectedItem.Text) + "," + txtBranchName.Text + "," + txtPaymentdate.Text + "," + ViewState["OFFLINE_INSTALLMENT_AMOUNT"] + ",0";
                    }
                    string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);

                    if (que_out1 == "1")
                    {
                        objCommon.DisplayMessage(this.Page, "Deposit Data Saved Succesfully !!!", this.Page);
                        ViewState["action"] = "add";
                        txtchallanAmount.Text = "";
                        ddlbank.SelectedValue = null;
                        txtBranchName.Text = "";
                        txtPaymentdate.Text = "";
                        GetPreviousReceipt();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(updChallan, "Failed To Done Offline Payment.", this.Page);
                        return;
                    }
                }
                else if (ViewState["action"].ToString().Equals("Edit"))
                {
                    string Call_Values1 = "";
                    string SP_Name1 = "PKG_ACD_GET_SEMESTER_WISE_REMAINING_PAYMENT_DETAILS";
                    string SP_Parameters1 = "@P_IDNO,@P_COMMAND_TYPE,@P_SESSIONNO,@P_SEMESTERNO,@P_AMOUNT,@P_ORDER_ID,@P_UA_NO,@P_FILENAME,@P_TEMP_DCR_NO,@P_BANKNO,@P_BANK_NAME,@P_BRANCH_NAME,@P_DEPOSIT_DATE,@P_INSTALL_NO,@P_OUTPUT";
                    if (Session["usertype"].ToString() == "2")
                    {
                        Call_Values1 = "" + Convert.ToInt32(Session["idno"]) + ",4," + Convert.ToInt32(ViewState["OFFLINE_SESSIONNO"]) + "," +
                        Convert.ToInt32(ViewState["OFFLINE_SEMESTERNO"]) + "," + Convert.ToDecimal(txtDepositAmount.Text) + "," + Convert.ToString(hdfOrderID.Value) + "," + Session["userno"].ToString() + "," + (Convert.ToInt32(Session["idno"]) + "_ERP_" + Count + "_Higher_Sem_Slip" + Ext) + "," + ViewState["TEMP_DCR_NO"] + "," + Convert.ToInt32(ddlbank.SelectedValue) + "," + Convert.ToString(ddlbank.SelectedItem.Text) + "," + txtBranchName.Text + "," + txtPaymentdate.Text + "," + ViewState["OFFLINE_INSTALLMENT_AMOUNT"] + ",0";
                    }
                    else
                    {
                        Call_Values1 = "" + Convert.ToInt32(ViewState["IDNO"]) + ",4," + Convert.ToInt32(ViewState["OFFLINE_SESSIONNO"]) + "," +
                        Convert.ToInt32(ViewState["OFFLINE_SEMESTERNO"]) + "," + Convert.ToDecimal(txtDepositAmount.Text) + "," + Convert.ToString(hdfOrderID.Value) + "," + Session["userno"].ToString() + "," + (Convert.ToInt32(ViewState["IDNO"]) + "_ERP_" + Count + "_Higher_Sem_Slip" + Ext) + "," + ViewState["TEMP_DCR_NO"] + "," + Convert.ToInt32(ddlbank.SelectedValue) + "," + Convert.ToString(ddlbank.SelectedItem.Text) + "," + txtBranchName.Text + "," + txtPaymentdate.Text + "," + ViewState["OFFLINE_INSTALLMENT_AMOUNT"] + ",0";
                    }
                    string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);

                    if (que_out1 == "1")
                    {
                        objCommon.DisplayMessage(this.Page, "Deposit Data Updated Succesfully!", this.Page);
                        ViewState["action"] = "add";
                        txtchallanAmount.Text = "";
                        ddlbank.SelectedValue = null;
                        txtBranchName.Text = "";
                        txtPaymentdate.Text = "";
                        //GetPreviousReceipt();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(updChallan, "Failed To Update Offline Payment.", this.Page);
                        return;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Semester_Registration.SubmitData-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void GetPreviousReceipt()
    {
        try
        {
            string Call_Values = "";
            string SP_Name = "PKG_ACD_GET_SEMESTER_WISE_REMAINING_PAYMENT_DETAILS";
            string SP_Parameters = "@P_IDNO,@P_COMMAND_TYPE,@P_SESSIONNO,@P_SEMESTERNO,@P_OUTPUT";
            if (Session["usertype"].ToString() == "2")
            {
                Call_Values = "" + Convert.ToInt32(Session["idno"]) + ",5," + Convert.ToInt32(ViewState["OFFLINE_SESSIONNO"]) + "," + Convert.ToInt32(ViewState["OFFLINE_SEMESTERNO"]) + ",0";
            }
            else
            {
                Call_Values = "" + Convert.ToInt32(ViewState["IDNO"]) + ",5," + Convert.ToInt32(ViewState["OFFLINE_SESSIONNO"]) + "," + Convert.ToInt32(ViewState["OFFLINE_SEMESTERNO"]) + ",0";
            }
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                lvDepositSlip.DataSource = ds;
                lvDepositSlip.DataBind();
            }
            else
            {
                lvDepositSlip.DataSource = null;
                lvDepositSlip.DataBind();
            }
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;

            ViewState["action"] = "Edit";
            ViewState["TEMP_DCR_NO"] = btnEdit.CommandArgument;

            string Call_Values = "";
            string SP_Name = "PKG_ACD_GET_SEMESTER_WISE_REMAINING_PAYMENT_DETAILS";
            string SP_Parameters = "@P_IDNO,@P_COMMAND_TYPE,@P_TEMP_DCR_NO,@P_OUTPUT";
            if (Session["usertype"].ToString() == "2")
            {
                Call_Values = "" + Convert.ToInt32(Session["idno"]) + ",5," + Convert.ToInt32(btnEdit.CommandArgument) + ",0";
            }
            else
            {
                Call_Values = "" + Convert.ToInt32(ViewState["IDNO"]) + ",5," + Convert.ToInt32(btnEdit.CommandArgument) + ",0";
            }

            DataSet chkds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (chkds.Tables != null && chkds.Tables[0].Rows.Count > 0)
            {
                ddlbank.SelectedValue = chkds.Tables[0].Rows[0]["BANK_NO"].ToString() == string.Empty ? "0" : chkds.Tables[0].Rows[0]["BANK_NO"].ToString();
                txtBranchName.Text = chkds.Tables[0].Rows[0]["BRANCH_NAME"].ToString().Trim();
                txtchallanAmount.Text = Convert.ToDecimal(chkds.Tables[0].Rows[0]["AMOUNT"]).ToString("N").Trim();
                txtDepositAmount.Text = chkds.Tables[0].Rows[0]["AMOUNT"].ToString().Trim();
                txtPaymentdate.Text = chkds.Tables[0].Rows[0]["CHALAN_DATE"].ToString().Trim();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#myModalChallan').modal('show')", true);
            }
            else
            {
                ddlbank.SelectedValue = "0";
                txtBranchName.Text = "";
                txtchallanAmount.Text = "";
                txtDepositAmount.Text = "";
                txtPaymentdate.Text = "";

                objCommon.DisplayMessage(updChallan, "Something Went Wrong !!!", this.Page);
            }
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void lnkPromissorynote_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;

            ViewState["OFFLINE_SEMESTERNO"] = lnk.CommandName.Split('$')[1];
            ViewState["OFFLINE_SESSIONNO"] = lnk.CommandName.Split('$')[2];
            ViewState["OFFLINE_INSTALLMENT_AMOUNT"] = lnk.CommandArgument.Split('$')[0];
            ViewState["PROMISSORY_AMOUNT"] = lnk.CommandName.Split('$')[0].ToString();
            txtPromissoryAmount.Text = lnk.CommandName.Split('$')[0].ToString();
            txtPromissoryReason.InnerText = lnk.CommandArgument.Split('$')[2].ToString();
            txtPromissoryDate.Text = lnk.CommandArgument.Split('$')[3].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#DivPromissory').modal('show')", true);
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void btnSubmitPromissory_Click(object sender, EventArgs e)
    {
        try
        {
            string Call_Values1 = "";
            string SP_Name1 = "PKG_ACD_GET_SEMESTER_WISE_REMAINING_PAYMENT_DETAILS";
            string SP_Parameters1 = "@P_IDNO,@P_COMMAND_TYPE,@P_SESSIONNO,@P_SEMESTERNO,@P_AMOUNT,@P_UA_NO,@P_DEPOSIT_DATE,@P_INSTALL_NO,@P_PROMISSORY_REASON,@P_OUTPUT";
            if (Session["usertype"].ToString() == "2")
            {
                Call_Values1 = "" + Convert.ToInt32(Session["idno"]) + ",6," + Convert.ToInt32(ViewState["OFFLINE_SESSIONNO"]) + "," +
                Convert.ToInt32(ViewState["OFFLINE_SEMESTERNO"]) + "," + Convert.ToDecimal(ViewState["PROMISSORY_AMOUNT"]) + "," + Session["userno"].ToString() + "," + txtPromissoryDate.Text + "," + ViewState["OFFLINE_INSTALLMENT_AMOUNT"] + "," + txtPromissoryReason.InnerText.Replace(",","") + ",0";
            }
            else
            {
                Call_Values1 = "" + Convert.ToInt32(ViewState["IDNO"]) + ",6," + Convert.ToInt32(ViewState["OFFLINE_SESSIONNO"]) + "," +
                Convert.ToInt32(ViewState["OFFLINE_SEMESTERNO"]) + "," + Convert.ToDecimal(ViewState["PROMISSORY_AMOUNT"]) + "," + Session["userno"].ToString() + "," + txtPromissoryDate.Text + "," + ViewState["OFFLINE_INSTALLMENT_AMOUNT"] + "," + txtPromissoryReason.InnerText.Replace(",","") + ",0";
            }
            string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);

            if (que_out1 == "1")
            {
                objCommon.DisplayMessage(this.Page, "Promissory Note Added Succesfully!", this.Page);
                txtPromissoryAmount.Text = "";
                txtPromissoryReason.InnerText = "";
                BindListView();
            }
            else if (que_out1 == "2")
            {
                objCommon.DisplayMessage(this.Page, "Promissory Note Updated Succesfully!", this.Page);
                txtPromissoryAmount.Text = "";
                txtPromissoryReason.InnerText = "";
                BindListView();
            }
            else
            {
                objCommon.DisplayUserMessage(updChallan, "Something Wents Wrong", this.Page);
                return;
            }
        }
        catch (Exception ex)
        { 
        
        }
    }

    protected void btnPrintReceipt_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnPrint = sender as ImageButton;

        int DCR_NO = Convert.ToInt32(btnPrint.CommandArgument.ToString());

        string recipt_code = Convert.ToString(objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "DCR_NO = " + DCR_NO + ""));
        if (Session["usertype"].ToString() == "2")
        {
            this.ShowReport("FeeCollectionReceipt.rpt", DCR_NO, Convert.ToInt32(Session["idno"]), "1");
        }
        else
        {
            this.ShowReport("FeeCollectionReceipt.rpt", DCR_NO, Convert.ToInt32(ViewState["IDNO"]), "1");
        }
    }

    private void ShowReport(string rptName, int dcrNo, int studentNo, string copyNo)
    {
        try
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];
            //url += "Reports/CommonReport.aspx?";
            url += "pagetitle=FeeCollectionReceipt_StudentLogin";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=" + this.GetReportParameters(dcrNo, studentNo, copyNo);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
           
        }
    }
    private string GetReportParameters(int dcrNo, int studentNo, string copyNo)
    {
        string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;
    }
}