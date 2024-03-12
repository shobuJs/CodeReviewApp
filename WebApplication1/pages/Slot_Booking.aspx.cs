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


public partial class Slot_Booking : System.Web.UI.Page
{
    private Common objCommon = new Common();
    private UAIMS_Common objUaimsCommon = new UAIMS_Common();
    private FeeCollectionController feeController = new FeeCollectionController();
    StudentController Stud = new StudentController();
    private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
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

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                    ViewState["action"] = "add";
                    //objCommon.SetLabelData("0");//for label
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
                    if (Session["usertype"].ToString() == "2")
                    {
                        ShowStudentDetails();
                        binddata();

                    }
                    else
                    {
                        objCommon.DisplayMessage(updSlot, "This Page Is For Student Login", this.Page);
                        return;
                    }
                    // Fill Dropdown lists
                    objCommon.FillListBox(ddlContent, "ACD_SLOT_BOOKING_CREATION C INNER JOIN ACD_SLOT_ACTIVITY_MASTER S ON (S.ACTIVITY_NO IN (SELECT CAST(VALUE AS INT) FROM DBO.SPLIT(C.ACTIVITY_NO,',')))", "CONCAT(C.SLOT_NO ,',' ,S.ACTIVITY_NO )AS ACTIVITY_NO", "CONVERT(NVARCHAR(11),SLOT_DATE,103)+ ','+ START_SLOT +'-'+END_SLOT+','+ ACTIVITY_NAME  AS SLOTNAME", "ISNULL(STATUS,0)=1", "C.SLOT_NO");
                 

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeesRefundReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowStudentDetails()
    {
        try
        {
            int idno = Convert.ToInt32(Session["idno"]);
            DataSet dsStudent = feeController.GetStudentInfoById_ForOnlinePayment(idno);
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                DataRow dr = dsStudent.Tables[0].Rows[0];
                lblStudentName1.Text = dr["STUDNAME"].ToString() == string.Empty ? string.Empty : dr["STUDNAME"].ToString();
                lblStdID.Text = dr["REGNO"].ToString() == string.Empty ? string.Empty : dr["REGNO"].ToString();
                lblSem.Text = dr["SEMESTERNAME"].ToString() == string.Empty ? string.Empty : dr["SEMESTERNAME"].ToString();
                lblMobileNo.Text = dr["STUDENTMOBILE"].ToString() == string.Empty ? string.Empty : dr["STUDENTMOBILE"].ToString();
                lblEmailID.Text = dr["EMAILID"].ToString() == string.Empty ? string.Empty : dr["EMAILID"].ToString();
                lblProgram1.Text = dr["PROGRAM"].ToString() == string.Empty ? string.Empty : dr["PROGRAM"].ToString();
                lblGender.Text = dr["SEX1"].ToString() == string.Empty ? string.Empty : dr["SEX1"].ToString();
                lblDOB1.Text = dr["DOB"].ToString() == string.Empty ? string.Empty : dr["DOB"].ToString();

                if (dr["PHOTO"].ToString() != "")
                {
                    imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dr["IDNO"].ToString() + "&type=student";
                }

            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.ShowStudentDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string activity = "";
        string SLOTNO = "";
        string START = "";
        string END = "";
        string activityno = "";
        string slotname = "";
        string selct ="";
        string[] act = new string[] { };
        string[] SLOTO = new string[] { };
        foreach (ListItem items in ddlContent.Items)
        {
            if (items.Selected == true)
            {
                activityno += items.Value + ',';
                selct += Convert.ToString(items) + ',';
            }
        }
        if (!string.IsNullOrEmpty(activityno))
        {
            selct = selct.TrimEnd(',');
            act = selct.Split(',');
            for (int i = 1; i < act.Length; i += 3)
            {
                slotname += act[i] + ",";
            }
            slotname = slotname.TrimEnd(',').Replace("'", "");
            SLOTO = slotname.Split('-');
            act = activityno.Split(',');
            for (int I = 0; I < act.Length; I += 2)
            {
                SLOTNO += act[I] + ",";
            }
            for (int J = 1; J < act.Length; J += 2)
            {
                activity += act[J] + ",";
            }
            activity = activity.TrimEnd(',');
            slotname = slotname.TrimEnd(',');
            SLOTNO = SLOTNO.TrimEnd(',');
        }
        if (activityno == string.Empty)
        {
            objCommon.DisplayMessage(updSlot, "Please Select Activity", this.Page);
            return;
        }
        CustomStatus cs = 0;
        if (ViewState["action"] != null)
        {
            if (ViewState["action"].ToString().Equals("add"))
            {
                //string SP_Name1 = "PKG_ACD_INSERT_SLOT_BOOKING";
                //string SP_Parameters1 = "@P_IDNO,@P_ACTIVITYNO,@P_SLOTNAME,@P_SLOTNO,@P_OUTPUT";
                //string Call_Values1 = "" + Convert.ToInt32(Session["idno"]) + "," + activity + "," + slotname + "," +  SLOTNO +  ",0";
                //string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//insert
                //if (que_out1 == "1")
                //{
                //    objCommon.DisplayMessage(updSlot, "Record Saved Successfully", this.Page);
                //    ddlContent.ClearSelection();
                //    binddata();
                //    return;
                //}
                 
               
                cs = (CustomStatus)Stud.Insert_Slot_Booking(Convert.ToInt32(Session["idno"]), activity, slotname, START, END, SLOTNO);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(updSlot, "Record Saved Successfully", this.Page);
                    ddlContent.ClearSelection();
                    binddata();
                    return;
                }
            }
            else if (ViewState["action"].ToString().Equals("edit"))
            {
                string SP_Name1 = "PKG_ACD_UPDATE_SLOT_BOOKING";
                string SP_Parameters1 = "@P_IDNO,@P_ACTIVITYNO,@P_SLOTNAME,@P_SLOT_BOOK_NO,@P_OUTPUT";
                string Call_Values1 = "" + Convert.ToInt32(Session["idno"]) + "," + activity + "," + slotname + "," +
                    Convert.ToInt32(ViewState["slotno"]) + ",0";
                string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//insert
                if (que_out1 == "1")
                {
                    objCommon.DisplayMessage(updSlot, "Record Saved Successfully", this.Page);
                    ddlContent.ClearSelection();
                    ViewState["action"] = "add";
                    btnSubmit.Text = "Submit";
                    binddata();
                    return;
                }
                else if (que_out1 == "2")
                {
                    objCommon.DisplayMessage(updSlot, "Record Already Exists", this.Page);
                    ddlContent.ClearSelection();
                    ViewState["action"] = "add";
                    btnSubmit.Text = "Submit";
                    binddata();
                    return;
                }
                //cs = (CustomStatus)Stud.Update_Slot_Booking(Convert.ToInt32(Session["idno"]), activity, slotname, Convert.ToInt32(ViewState["slotno"]));
                //if (cs.Equals(CustomStatus.RecordSaved))
                //{
                //    objCommon.DisplayMessage(updSlot, "Record Saved Successfully", this.Page);
                //    ddlContent.ClearSelection();
                //    ViewState["action"] = "add";
                //    btnSubmit.Text = "Submit";
                //    binddata();
                //    return;
                //}
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlContent.ClearSelection();
        btnSubmit.Text = "Submit";
    }
    protected void LnkREsch_Click(object sender, EventArgs e)
    {
        try
        {
            ddlContent.ClearSelection();
            LinkButton btnEdit = sender as LinkButton;
            ViewState["action"] = "edit";
            string start = ""; string end = "";
            int slotno = int.Parse(btnEdit.CommandArgument);
            string slotname = Convert.ToString(btnEdit.CommandName);
            string[] pgm = new string[] { };
            pgm = slotname.Split('-');
            start = pgm[0];
            end = pgm[1];
            ViewState["slotno"] = slotno;
            btnSubmit.Text = "Update";
            ShowDetails(slotno, start, end);
        }
        catch (Exception ex)
        {
        }
    }
    public int Insert_Slot_Booking(int idno, string activityno, string slotname, string START, string END, string SLOTNO)
    {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[7];
            objParams[0] = new SqlParameter("@P_IDNO", idno);
            objParams[1] = new SqlParameter("@P_ACTIVITYNO", activityno);
            objParams[2] = new SqlParameter("@P_SLOTNAME", slotname);
            objParams[3] = new SqlParameter("@P_START", START);
            objParams[4] = new SqlParameter("@P_END", END);
            objParams[5] = new SqlParameter("@P_SLOTNO", SLOTNO);
            objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
            objParams[6].Direction = ParameterDirection.Output;
            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_SLOT_BOOKING", objParams, true);

            if (Convert.ToInt32(ret) == 1)
                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            else
                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
        }
        catch (Exception ex)
        {
            retStatus = Convert.ToInt32(CustomStatus.Error);
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.Insert_Update_StudentDocuments-> " + ex.ToString());
        }
        return retStatus;
    }
    private void ShowDetails(int slotno, string start, string end)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_SLOT_BOOKING B INNER JOIN ACD_SLOT_BOOKING_CREATION C  ON (B.ACTIVITYNO IN (SELECT CAST(VALUE AS INT) FROM DBO.SPLIT(C.ACTIVITY_NO,',')))  ", "CONCAT(C.SLOT_NO ,',' ,B.ACTIVITYNO )AS ACTIVITY_NO", "", "SLOT_BOOK_NO=" + slotno + "AND STATUS=1 AND c.START_SLOT='" +start+ "'" + " AND c.END_SLOT='" +end+"'", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string activity = (ds.Tables[0].Rows[0]["ACTIVITY_NO"].ToString());
                string[] pgm = activity.Split('&');
                for (int j = 0; j < pgm.Length; j++)
                {
                    for (int i = 0; i < ddlContent.Items.Count; i++)
                    {
                        if (pgm[j] == ddlContent.Items[i].Value)
                        {
                            ddlContent.Items[i].Selected = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_CourseCreation.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void binddata()
    {

        string SP_Name2 = "PKG_ACD_GET_SLOT_BOOKING";
        string SP_Parameters2 = "@P_OUTPUT";
        string Call_Values2 = "" + Convert.ToInt32(Session["idno"])+ "" ;
        DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (dsStudList.Tables[0].Rows.Count > 0)
        {
            Panel2.Visible = true;
            LvSlot.DataSource = dsStudList;
            LvSlot.DataBind();
        }
        else
        {
            Panel2.Visible = false;
            LvSlot.DataSource = null;
            LvSlot.DataBind();
        }
        //}
        //DataSet ds = Stud.GetSlot_Booking();
        //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //{
        //    Panel2.Visible = true;
        //    LvSlot.DataSource = ds;
        //    LvSlot.DataBind();
        //}
        //else
        //{
        //    Panel2.Visible = false;
        //    LvSlot.DataSource = null;
        //    LvSlot.DataBind();
        //}
        //foreach (ListViewDataItem lvitem in LvSlot.Items)
        //{
        //    HiddenField hdfgetdate = lvitem.FindControl("hdfgetdate") as HiddenField;
        //    HiddenField hdfslotdate = lvitem.FindControl("hdfslotdate") as HiddenField;
        //    LinkButton LnkREsch = lvitem.FindControl("LnkREsch") as LinkButton;

        //    if (Convert.ToDateTime(hdfgetdate.Value) > Convert.ToDateTime(hdfslotdate.Value))
        //    {
        //        LnkREsch.Enabled = false;
        //    }
        //}
    }
}