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

public partial class Slot_Approval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
                this.CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "DISTINCT BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVE=1", "BATCHNO DESC");
            }
        }
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Slot_Approval.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Slot_Approval.aspx");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            string StartDate = "",EndDate="";
            if (hdfDate.Value.ToString() != string.Empty && hdfDate.Value.ToString() != "" && hdfDate.Value.ToString() != "0")
            {
                StartDate = Convert.ToString(Convert.ToDateTime(hdfDate.Value.ToString().Split('-')[0]).ToString("dd/MM/yyyy"));
                EndDate = Convert.ToString(Convert.ToDateTime(hdfDate.Value.ToString().Split('-')[1]).ToString("dd/MM/yyyy"));
            }
            else
            {
                StartDate = "0"; EndDate = "0";
            }
            string SP_Name1 = "PKG_ACD_GET_STUDENT_FOR_SLOT_APPROVAL";
            string SP_Parameters1 = "@P_INTAKENO,@P_START_DATE,@P_ENDT_DATE,@P_STATUS,@P_COMMAND_TYPE";
            string Call_Values1 = "" + Convert.ToInt32(ddlIntake.SelectedValue) + "," + StartDate + "," + EndDate + "," + Convert.ToInt32(ddlStatus.SelectedValue) + ",1";

            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);

            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                lvListApv.DataSource = ds.Tables[0];
                lvListApv.DataBind();

                
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ListBox lstContent = lvListApv.Items[i].FindControl("lstActivity") as ListBox;
                        Label lblIdno = lvListApv.Items[i].FindControl("lblRegno") as Label;

                        string SP_Name = "PKG_ACD_GET_STUDENT_FOR_SLOT_APPROVAL";
                        string SP_Parameters = "@P_INTAKENO,@P_IDNO,@P_COMMAND_TYPE";
                        string Call_Values = "" + Convert.ToInt32(ddlIntake.SelectedValue) + "," + lblIdno.ToolTip + ",3";

                        DataSet ds1 = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
                        if (ds1.Tables != null && ds1.Tables[0].Rows.Count > 0)
                        {

                        lstContent.Items.Clear();
                        lstContent.Items.Add("Please Select");
                        //lstContent.SelectedItem.Value = "0";

                        lstContent.DataSource = ds1.Tables[0];
                        lstContent.DataValueField = "ACTIVITY_NO";
                        lstContent.DataTextField = "ACTIVITY_NAME";
                        lstContent.DataBind();

                        string[] ActivityNo;

                        ActivityNo = ds.Tables[0].Rows[i]["ACTIVITYNO"].ToString().Split(',');

                        for (int j = 0; j < ActivityNo.Length; j++)
                        {
                            foreach (ListItem lst in lstContent.Items)
                            {
                                if (lst.Value == ActivityNo[j])
                                {
                                    lst.Selected = true;
                                }
                            }
                        }

                    }
                }
            }
            else
            {
                lvListApv.DataSource = null;
                lvListApv.DataBind();
                objCommon.DisplayMessage(this.Page, "Record Not Found !!!", this.Page);
            }
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            bool flag = false;

            foreach (ListViewDataItem item in lvListApv.Items)
            {
                CheckBox chk = item.FindControl("chkRow") as CheckBox;
                if (chk.Checked == true && chk.Enabled == true)
                {
                    flag = true;
                    Label lblIdno = item.FindControl("lblRegno") as Label;
                    Label lblSlotNo = item.FindControl("lblSlotNo") as Label;

                    string SP_Name1 = "PKG_ACD_GET_STUDENT_FOR_SLOT_APPROVAL";
                    string SP_Parameters1 = "@P_INTAKENO,@P_IDNO,@P_SLOT_BOOK_NO,@P_UA_NO,@P_COMMAND_TYPE";
                    string Call_Values1 = "" + Convert.ToInt32(ddlIntake.SelectedValue) + "," + lblIdno.ToolTip + "," + lblSlotNo.Text + "," + Convert.ToInt32(Session["userno"]) + ",2";
                    string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 2);

                    if (que_out1 != "-99")
                    {
                        objCommon.DisplayMessage(updApprove, "Record Approved Successfully !!!", this.Page);
                    }
                }
            }
            if (flag == false)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Atleast One CheckBox !!!", this.Page);
            }
            btnShow_Click(new object(), new EventArgs());
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}