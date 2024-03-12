using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI.HtmlControls;

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

public partial class ACADEMIC_TransfereeConfirmation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {

            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                this.CheckPageAuthorization();
                // FillDropDown();
                ViewState["action"] = "Add";

                if (Request.QueryString["pageno"] != null)
                {

                }

                BindAllDropDown();
                // pnlmain.Visible = true;
                //pnlmsg.Visible = false;
                //pnlOTP.Visible = false;
            }
            ViewState["cntbranch"] = string.Empty;
            //divMsg.InnerHtml = string.Empty;
            ViewState["action"] = "add";
            Session["Employment_History"] = null;
            Session["Referees"] = null;
            ViewState["REFEREEES_SRNO"] = null;
            ViewState["EMPLOYEE_SRNO"] = null;

            ddlProgreeLevel.SelectedValue = "5";

            objCommon.SetLabelData("0");
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // dynamic page header addded by sarang 09/07/2021

        }
        

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Enrollment_Confirmation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Enrollment_Confirmation.aspx");
        }
    }
    protected void BindAllDropDown()
    {
        objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVE = 1", "BATCHNO DESC");
        objCommon.FillDropDownList(ddlStudyType, "ACD_UA_SECTION S WITH(NOLOCK) INNER JOIN ACD_LEAD_STUDENT_ENQUIRY_GENERATION G ON S.UA_SECTION = G.UGPG", "DISTINCT UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "UA_SECTION");
    }
    private void BindListView()
    {
        DataSet ds = null;

        string Degreenos = ""; string BranchNos = "";
        foreach (ListItem item in ddlProgramIntrested.Items)
        {
            if (item.Selected)
            {
                string[] branchpref = item.Value.Split(',');
                string selectedValue = item.Value;
                Degreenos += branchpref[0] + ',';
                BranchNos += branchpref[1] + ',';
            }
        }
        if (Degreenos.ToString() != "")
        {

            Degreenos = Degreenos.Substring(0, Degreenos.Length - 1);
        }
        // ViewState["DegreeNos"] = Degreenos.Substring(0, Degreenos.Length - 1);
        if (BranchNos.ToString() != "")
        {
            BranchNos = BranchNos.Substring(0, BranchNos.Length - 1);
        }


        DocumentControllerAcad objdocContr = new DocumentControllerAcad();
        ds = objSC.GetApplicationDetailsInterviw_Transfree(Convert.ToInt32(ddlIntake.SelectedValue), Convert.ToString(ddlStudyType.SelectedValue), Convert.ToString(0), Convert.ToString(0), Convert.ToInt32(0));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvEnrollmentConfirmation.DataSource = ds;
            lvEnrollmentConfirmation.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            lblTotalCount.Text = ds.Tables[0].Rows.Count.ToString();
            lblTotalCount.Visible = true;
            divCount.Visible = true;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Label lblhead1 = lvEnrollmentConfirmation.Items[i].FindControl("lblEligible") as Label;
                if ((ds.Tables[0].Rows[i]["CAN"].ToString() == "1" || ds.Tables[0].Rows[i]["ADMCAN"].ToString() == "1"))
                {
                    lblhead1.Text = "";
                }
                else
                {
                    lblhead1.Text = "CONFIRMED";
                    lblhead1.Style.Add("color", "green");
                }
            }
        }
        else
        {
            lvEnrollmentConfirmation.DataSource = null;
            lvEnrollmentConfirmation.DataBind();
            objCommon.DisplayMessage(this.Page, "Record Not Found", this.Page);
            lblTotalCount.Text = "0";
            lblTotalCount.Visible = true;
            divCount.Visible = true;
        }
        ds.Dispose();
    }

    protected void lnkButtonShow_Click(Object sender, EventArgs e)
    {
        try
        {
            if (ddlStudyType.SelectedValue != null)
            {

                BindListView();
            }
        }

        catch (Exception ex)
        {

        }
    }

    protected void lnkButtonCancel_Click(Object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.ToString());
        ddlIntake.SelectedIndex = 0;
        ddlStudyType.SelectedIndex = 0;
    }

}