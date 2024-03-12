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
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using EASendMail;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;

public partial class Projects_My_Wallet : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController stud = new StudentController();
    FeeCollectionController feecollection = new FeeCollectionController();
    DocumentControllerAcad objdocContr = new DocumentControllerAcad();
    UserController user = new UserController();
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
    decimal TotalSum = 0;
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
                }
                if (Session["usertype"].ToString().Equals("2"))     //Student 
                {
                    this.CheckPageAuthorization();

                    BindListView();
                    wallet();
                    SCH();
                }

                else
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Not Found.This Page is use for only Student Login!!", this.Page);
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
                Response.Redirect("~/notauthorized.aspx?page=My_Wallet.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=My_Wallet.aspx");
        }
    }


    protected void lvStudentFees_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Label lblAmount = e.Item.FindControl("lblAmount") as Label;
            HiddenField hdfFieldName = e.Item.FindControl("hdfFieldName") as HiddenField;
            if (hdfFieldName.Value == "Discount")
            {
                TotalSum -= Convert.ToDecimal(lblAmount.Text);
            }
            else
            {
                TotalSum += Convert.ToDecimal(lblAmount.Text);
            }
            string FormattedPrice = TotalSum.ToString("N");
            ViewState["demandamounttobepaid"] = TotalSum;
           
        }
    }
    protected void lvStudentFees_PreRender(object sender, EventArgs e)
    {
        wallet();
    }
    private void wallet()
    {
        string semesterno = objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + Convert.ToInt32(Session["idno"]));
        string ADMBATCH = objCommon.LookUp("ACD_STUDENT", "ADMBATCH", "IDNO=" + Convert.ToInt32(Session["idno"]));
        string totalrefund = objCommon.LookUp("ACD_STUDENT_PROGRAM_CANCEL", "SUM(ISNULL(TOTAL_REFUND_AMOUNT,0))", "status_no=10 and IDNO=" + Convert.ToInt32(Session["idno"]));
        string TOTAL = objCommon.LookUp("ACD_DCR", "sum(ISNULL(AMOUNT,0))- sum(ISNULL(EXCESS_BALANCE,0))TOTAL", "can=0 and delet=0 and recon=1 and IDNO=" + Convert.ToInt32(Session["idno"]) + "AND PAY_SERVICE_TYPE in (1,2) AND SEMESTERNO=" + Convert.ToInt32(semesterno) + "AND RECIEPT_CODE='TF'");
        DataSet ds = feecollection.GetWalletData(50, Convert.ToInt32(Session["idno"]), Convert.ToInt32(semesterno), Convert.ToString("TF"), 1, Convert.ToInt32(ADMBATCH));
        if (ds.Tables[0].Rows.Count > 0)
        {
           
            lvStudentFees.DataSource = ds.Tables[0];
            lvStudentFees.DataBind();
            DataView dv = ds.Tables[0].DefaultView;
            dv.RowFilter = "SRNO in(56,57)";
            if (dv.Count > 0)
            {
                decimal LateFee = 0;
                for (int i = 0; i < dv.Count; i++)
                {
                    LateFee += Convert.ToDecimal(dv[i]["AMOUNT"]);
                    ViewState["LATE_FEE_FOR_INSTALLMENT"] = LateFee;
                }
            }
            else
            {
                ViewState["LATE_FEE_FOR_INSTALLMENT"] = null;
            }
            dv.RowFilter = "SRNO in(56)"; 
            if (dv.Count > 0)
            {
                decimal LateFee = 0;
                for (int i = 0; i < dv.Count; i++)
                {
                    LateFee = Convert.ToDecimal(dv[i]["AMOUNT"]);
                    ViewState["1ST_LATE_FEE"] = LateFee;
                }
            }
            else
            {
                ViewState["1ST_LATE_FEE"] = null;
            }
            dv.RowFilter = "SRNO in(57)"; 
            if (dv.Count > 0)
            {
                decimal LateFee = 0;
                for (int i = 0; i < dv.Count; i++)
                {
                    LateFee = Convert.ToDecimal(dv[i]["AMOUNT"]);
                    ViewState["2ND_LATE_FEE"] = LateFee;
                }
            }
            else
            {
                ViewState["2ND_LATE_FEE"] = null;
            }
        }
        else
        {
            ViewState["LATE_FEE_FOR_INSTALLMENT"] = null;
            ViewState["1ST_LATE_FEE"] = null; ViewState["2ND_LATE_FEE"] = null;
        }
        Label lbltotal = this.lvStudentFees.FindControl("lbltotal") as Label;
        ViewState["Amount"] = TotalSum.ToString();
        string FormattedPrice = TotalSum.ToString("N");
        if (TotalSum.ToString() != "0")
        {
            ViewState["totalfee"] = FormattedPrice.ToString();
        }
        else
        {
            ViewState["totalfee"] = "0.00";
        }
        if (TOTAL == "")
        {
            TOTAL = "0.00";
        }
        if (totalrefund == "")
        {
            totalrefund = "0.00";
        }
        if (Convert.ToDecimal(ViewState["totalfee"]) == Convert.ToDecimal(TOTAL))
        {
            lblpayble.Text = "0.00";
        }
        else
        {
            if (ds.Tables[1].Rows.Count > 0)
            {
                ViewState["paidfee"] = ds.Tables[1].Rows[0]["AMOUNT"].ToString();
            }
            else
            {
                ViewState["paidfee"] = "0.00";
            }

            if (ViewState["totalfee"] != "0.00")
            {
                lblpayble.Text = Convert.ToString(Convert.ToDecimal(ViewState["totalfee"]) - Convert.ToDecimal(ViewState["paidfee"]));
            }
            else
            {

                lblpayble.Text = Convert.ToString(ViewState["paidfee"]);
            }
        }
        string SP_Name2 = "PKG_ACD_SP_GET_EXCESS_AMT";
        string SP_Parameters2 = "@P_IDNO,@P_SEMESTERNO,@P_RECEIPT_CODE,@P_PAYTYPENO,@P_ADMBATCH";
        string Call_Values2 = "" +  Convert.ToInt32(Session["idno"]) + "," + Convert.ToInt32(semesterno) + "," + Convert.ToString("TF") + "," + 1 + "," + Convert.ToInt32(ADMBATCH) + "," ;
            DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (dsStudList.Tables[0].Rows.Count > 0)
            {
                ViewState["excess"] = dsStudList.Tables[0].Rows[0]["EXCESS_BALANCE"].ToString();
                if (totalrefund == "0.00")
                {
                    lblexcamt.Text = dsStudList.Tables[0].Rows[0]["EXCESS_BALANCE"].ToString();
                    lblexcessamt.Text = dsStudList.Tables[0].Rows[0]["EXCESS_BALANCE"].ToString();
                    ViewState["excess"] = dsStudList.Tables[0].Rows[0]["EXCESS_BALANCE"].ToString();
                    if (dsStudList.Tables[0].Rows[0]["EXCESS_BALANCE"].ToString() == "0.00")
                    {
                        btnexwith.Enabled = false;
                    }
                    else
                    {
                        btnexwith.Enabled = true;
                    }
                }
                else if (Convert.ToDecimal(ViewState["excess"])==0)
                {
                    lblexcamt.Text = dsStudList.Tables[0].Rows[0]["EXCESS_BALANCE"].ToString();
                    lblexcessamt.Text = dsStudList.Tables[0].Rows[0]["EXCESS_BALANCE"].ToString();
                    ViewState["excess"] = dsStudList.Tables[0].Rows[0]["EXCESS_BALANCE"].ToString();
                    if (dsStudList.Tables[0].Rows[0]["EXCESS_BALANCE"].ToString() == "0.00")
                    {
                        btnexwith.Enabled = false;
                    }
                    else
                    {
                        btnexwith.Enabled = true;
                    }
                }

                else
                {
                    lblexcamt.Text = Convert.ToString(Convert.ToDecimal(ViewState["excess"]) - Convert.ToDecimal(totalrefund));
                    lblexcessamt.Text = Convert.ToString(Convert.ToDecimal(ViewState["excess"]) - Convert.ToDecimal(totalrefund));
                    if (Convert.ToDecimal(ViewState["excess"]) == Convert.ToDecimal(totalrefund))
                    {
                        btnexwith.Enabled = false;
                    }
                    else
                    {
                        btnexwith.Enabled = true;
                    }
                }
              
            }    
    }
    private void SCH()
    {
        DataSet ds = feecollection.GetWALLETDetails(Convert.ToInt32(Session["idno"]));
        if (ds.Tables[1].Rows.Count > 0)
        {
            lblbalanceschol.Text = ds.Tables[1].Rows[0]["CLOSE_BAL"].ToString();
            if (lblbalanceschol.Text == "")
            {
                lblbalanceschol.Text = "0.00";
            }
            else
            {
                lblbalanceschol.Text = ds.Tables[1].Rows[0]["CLOSE_BAL"].ToString();
            }
            
        }
        else
        {
            lblbalanceschol.Text = "0.00";
        }
    }
    private void BindListView()
    {
        DataSet ds = feecollection.GetRefundDetails(Convert.ToInt32(Session["idno"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            //lblpayble.Text = ds.Tables[1].Rows[0]["TOTAL_AMT"].ToString();
            //lblexcamt.Text = ds.Tables[1].Rows[0]["EXCESS_AMOUNT"].ToString();
            //lblbalanceschol.Text = ds.Tables[2].Rows[0]["ScholarshipBalance"].ToString();
            ViewState["semesterno"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
            ViewState["BlobImagefinance"] = ds.Tables[0].Rows[0]["UPLOAD_TRANSACTION_DETAIL"].ToString();
            divwithapti.Visible = true;
            lvwithap.DataSource = ds;
            lvwithap.DataBind();

        }
        else
        {
            divwithapti.Visible = false;
            lvwithap.DataSource = null;
            lvwithap.DataBind();
        }
        foreach (ListViewDataItem lvitem in lvwithap.Items)
        {
            Label refundstatus = lvitem.FindControl("refundstatus") as Label;
            Label labelstatus = lvitem.FindControl("labelstatus") as Label;
            LinkButton lnkViewDoc = lvitem.FindControl("lnkViewDoc") as LinkButton;
            HiddenField hdfsts = lvitem.FindControl("hdfsts") as HiddenField;
            HiddenField lblbtnena = lvitem.FindControl("lblbtnena") as HiddenField;
            HiddenField hdfdocview = lvitem.FindControl("hdfdocview") as HiddenField;
            HiddenField hdfreqtype = lvitem.FindControl("hdfreqtype") as HiddenField;
            LinkButton lnkfinancedoc = lvitem.FindControl("lnkfinancedoc") as LinkButton;
            Label lblrefund = lvitem.FindControl("lblrefund") as Label;
            Label lblrefundwithstatus = lvitem.FindControl("lblrefundwithstatus") as Label;

            if (hdfdocview.Value == "" || hdfdocview.Value == null)
            {
                lnkfinancedoc.Visible=false;
            }
            else
            {
                 lnkfinancedoc.Visible=true;
            }
            if (hdfsts.Value == "0")

            {
                labelstatus.Visible = false;
            }
            else
            {
                labelstatus.Visible = true;
            }
            if (hdfreqtype.Value == "Excess Withdrawal")
            {
                lnkViewDoc.Visible = false;
            }
            else if (hdfreqtype.Value == "Postponement")
            {
                lnkViewDoc.Enabled = false;
                refundstatus.Visible = true;
            }
            else if (lblrefundwithstatus.Text == "1")
            {
                lnkViewDoc.Enabled = false;
            }
            else if (lblbtnena.Value == "Approved" && hdfreqtype.Value != "Postponement")
            {
                lnkViewDoc.Enabled = false;
                refundstatus.Visible = false;
            }
            //else if (lblrefundwithstatus.Text == "2")
            //{
            //    lnkViewDoc.Enabled = true;
            //    refundstatus.Visible = true;
            //}
            else
            {
                lnkViewDoc.Enabled = true;
                refundstatus.Visible = false;
            }
            //if (hdfreqtype.Value == "Postponement")
            //{
            //    lnkViewDoc.Enabled = false;
            //}
            //else
            //{
            //    lnkViewDoc.Enabled = true;
            //}
        }

    }
    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }
    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }
    protected void lnkfinancedoc_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        int value = int.Parse(lnk.CommandArgument);
        string FileName = lnk.CommandName.ToString();
        string Url = string.Empty;
        string directoryPath = string.Empty;
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
        CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
        string directoryName = "~/DownloadImg" + "/";
        directoryPath = Server.MapPath(directoryName);

        if (!Directory.Exists(directoryPath.ToString()))
        {
            Directory.CreateDirectory(directoryPath.ToString());
        }
        CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
        string img = FileName;
        var ImageName = img;
        if (img != null || img != "")
        {
            DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
            var Newblob = blobContainer.GetBlockBlobReference(ImageName);
            string filePath = directoryPath + "\\" + ImageName;
            if ((System.IO.File.Exists(filePath)))
            {
                System.IO.File.Delete(filePath);
            }
            Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal22", "$(document).ready(function () {$('#myModal22').modal();});", true);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Sorry, File not found !!!", this.Page);
        }

    }
    protected void lnkViewDoc_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        int Idno = int.Parse(lnk.CommandArgument);
        ViewState["idno"] = Idno;
        int srno = int.Parse(lnk.CommandName);
        DataSet ds = null;
        int status = Convert.ToInt32(lnk.ToolTip);
        ViewState["status"] = status;
        hdfSrnoWithDrwal.Value = Convert.ToString(srno);
        ViewState["srno"] = srno;
        hdfIdnoWithDrwal.Value = Convert.ToString(Idno);
        ds = feecollection.GetWithApplicationDetails(Idno, srno, status);
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
            refundamount.Text = ds.Tables[0].Rows[0]["REFUND_AMOUNT"].ToString();
            nonrefundamount.Text = ds.Tables[0].Rows[0]["NONREFUND_AMOUNT"].ToString();
            txtBankName.Text = ds.Tables[0].Rows[0]["BANKNAME"].ToString();
            txtAccHolderName.Text = ds.Tables[0].Rows[0]["ACOUNTHOLDERNAME"].ToString();
            txtBranch.Text = ds.Tables[0].Rows[0]["BANKBRANCH"].ToString();
            txtBranchCode.Text = ds.Tables[0].Rows[0]["BRANCHCODE"].ToString();
            txtAccNumber.Text = ds.Tables[0].Rows[0]["ACCOUNTNO"].ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "apply_refund", "$(document).ready(function () {$('#apply_refund').modal();});", true);           
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "apply_refund", "$(document).ready(function () {$('#apply_refund').modal();});", true);
            ViewState["BlobImage"] = null;           
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int ua_no = Convert.ToInt32(Session["userno"]);
        CustomStatus cs = (CustomStatus)feecollection.UpdateRefundAmount(Convert.ToInt32(ViewState["srno"]), Convert.ToInt32(ViewState["idno"]), Convert.ToString(txtBankName.Text), Convert.ToString(txtAccNumber.Text), Convert.ToString(txtBranch.Text), Convert.ToString(txtBranchCode.Text), Convert.ToString(txtAccHolderName.Text), Convert.ToInt32(ViewState["status"]));
        if (cs.Equals(CustomStatus.RecordUpdated))
        {
            objCommon.DisplayMessage(this.updmodal, "Record Saved Successfully!", this.Page);
            //foreach (ListViewDataItem lvitem in lvwithap.Items)
            //{
            //    Label labelstatus = lvitem.FindControl("labelstatus") as Label;
            //    labelstatus.Visible = true;
            //}
            BindListView();
        }
        else
        {

            objCommon.DisplayMessage(this.updmodal, "Error!!", this.Page);
        }
    }

    protected void btnexwith_Click(object sender, EventArgs e)
    {
        try
        {
            wallet();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "apply_refund", "$(document).ready(function () {$('#Withdrawl').modal();});", true);
        }

        catch (Exception ex)
        {
        }
    }
    protected void btnsubmitexce_Click(object sender, EventArgs e)
    {
        try
        {
            //string excamt = Convert.ToDecimal(ViewState["examt"]) - Convert.ToDecimal(txtamt.Text);

            DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "REGNO", "DEGREENO,BRANCHNO,COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"]), "");
            {
                ViewState["REGNO"] = ds.Tables[0].Rows[0]["REGNO"].ToString();
                ViewState["DEGREENO"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                ViewState["BRANCHNO"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                ViewState["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            }
            decimal nonrefund = Convert.ToDecimal(lblexcessamt.Text)- Convert.ToDecimal(txtamt.Text);
            int ua_no = Convert.ToInt32(Session["userno"]);
            CustomStatus cs = (CustomStatus)feecollection.InsertExcessWithdrwal(Convert.ToInt32(ViewState["srno"]), Convert.ToInt32(Session["idno"]), Txtbanknameex.Text, TxtAccNuex.Text, Txtbranchex.Text, branchcodeex.Text, TxtStudNameex.Text, Convert.ToInt32(ViewState["status"]), Convert.ToString(ViewState["REGNO"]), Convert.ToInt32(ViewState["COLLEGE_ID"]), Convert.ToInt32(ViewState["DEGREENO"]), Convert.ToInt32(ViewState["BRANCHNO"]), Convert.ToString(txtpurpose.Text), Convert.ToDecimal(txtamt.Text), Convert.ToDecimal(nonrefund));
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.updmodal, "Record Saved Successfully!", this.Page);
                BindListView();
                txtamt.Text = "";
                txtpurpose.Text = "";
            }
        }
        catch (Exception ex)
        {
        }
    }
}