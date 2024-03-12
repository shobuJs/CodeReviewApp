using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using IITMS;
using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_IntakeMapping : System.Web.UI.Page
{
    CourseController objCourse = new CourseController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
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
            if (Session["userno"] == null || Session["username"] == null || Session["idno"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                this.CheckPageAuthorization();
                if (Session["usertype"].ToString() == "1" && Session["username"].ToString().ToUpper().Contains("PRINCIPAL"))
                {
                    // divShow.Visible=true;
                }
                else
                {
                    // divShow.Visible=false;
                }

                Page.Title = objCommon.LookUp("reff", "collegename", string.Empty);

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["FEES_NO"] = "edit";
                BindListViewData();
                ViewState["Mapping_Id"] = "edit";
                objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
                objCommon.FillListBox(lstbxStudyLevel, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION > 0", "UA_SECTIONNAME ");
                objCommon.FillListBox(lstbxProgramInterest, "ACD_AREA_OF_INTEREST", "AREA_INT_NO", "AREA_INT_NAME", "AREA_INT_NO > 0", "AREA_INT_NAME");
            }

        }
      
        objCommon.SetLabelData("0");
        objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
      
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=IntakeMapping.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=IntakeMapping.aspx");
        }
    }

    protected void BindListViewData()
    {
        DataSet ds = objCourse.GetIntakeMappingData();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvIntakeMapping.DataSource = ds;
            lvIntakeMapping.DataBind();
        }
        else
        {

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int AdmBatch = Convert.ToInt32(ddlAdmbatch.SelectedValue);
        string StudyLevel = string.Empty;
        string ProgramInterest = string.Empty;
        foreach (ListItem items in lstbxStudyLevel.Items)
        {
            if (items.Selected == true)
            {
                StudyLevel += items.Value + ',';

            }
        }
        if (StudyLevel == string.Empty)
        {
            objCommon.DisplayMessage(this, "Please Select Study Level", this.Page);
            return;
        }
        StudyLevel = StudyLevel.Substring(0, StudyLevel.Length - 1);
        foreach (ListItem items in lstbxProgramInterest.Items)
        {
            if (items.Selected == true)
            {
                ProgramInterest += items.Value + ',';

            }
        }
        if (ProgramInterest == string.Empty)
        {
            objCommon.DisplayMessage(this, "Please Select Study Level", this.Page);
            return;
        }
        ProgramInterest = ProgramInterest.Substring(0, ProgramInterest.Length - 1);
        int Mappint_ID = 0;
        if (ViewState["Mapping_Id"].ToString() != "edit")
        {
            Mappint_ID = Convert.ToInt32(ViewState["Mapping_Id"].ToString());
        }

        CustomStatus cs = (CustomStatus)objCourse.InsertIntakeMappingData(AdmBatch, StudyLevel, ProgramInterest, Mappint_ID);
        if (cs == CustomStatus.RecordSaved)
        {
            clear();
            BindListViewData();
            ViewState["Mapping_Id"] = "edit";
            objCommon.DisplayMessage(this, "Record Saved Successfully !!", this.Page);
            return;
        }
        else if (cs == CustomStatus.RecordUpdated)
        {
            BindListViewData();
            clear();
            ViewState["Mapping_Id"] = "edit";
            objCommon.DisplayMessage(this, "Record Update Successfully !!", this.Page);
            return;
        }
        else if (cs == CustomStatus.RecordExist)
        {
            clear();
            BindListViewData();
            ViewState["Mapping_Id"] = "edit";
            objCommon.DisplayMessage(this, "Record Already Exists", this.Page);
        }
        else
        {
            clear();
            BindListViewData();
            ViewState["Mapping_Id"] = "edit";
            objCommon.DisplayMessage(this, "Server Error", this.Page);
        }
    }
    protected void clear()
    {
        foreach (ListItem item in lstbxProgramInterest.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in lstbxStudyLevel.Items)
        {
            item.Selected = false;
        }
        //lstbxProgramInterest.SelectedValue = "0";
        //lstbxStudyLevel.SelectedValue = "0";
        ddlAdmbatch.SelectedValue = "0";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnEdits_Click(object sender, ImageClickEventArgs e)
    {
   
        ImageButton btnEdits = sender as ImageButton;
        int admbatch = int.Parse(btnEdits.CommandArgument);
        int Mapping_Id = int.Parse(btnEdits.ToolTip);
        string Area_inter = (btnEdits.CommandName);
        string UgPg = objCommon.LookUp("ACD_INTAKE_SECTION_AREA_OF_INT_MAPPING", "UGPGOT", "MAPPING_ID=" + Mapping_Id);
        ddlAdmbatch.SelectedValue = admbatch.ToString();
        ViewState["Mapping_Id"] = Mapping_Id;

        foreach (ListItem item in lstbxProgramInterest.Items)
        {
                item.Selected = false;
        }
        foreach (ListItem item in lstbxStudyLevel.Items)
        {
            item.Selected = false;
        }

        string[] ProgramInterest = Convert.ToString(Area_inter.ToString()).Split(',');

        foreach (string s in ProgramInterest)
        {
            foreach (ListItem item in lstbxProgramInterest.Items)
            {
                if (s == item.Value)
                {
                    item.Selected = true;
                    break;
                }
                
            }

        }
        string[] UGPGDATA = Convert.ToString(UgPg.ToString()).Split(',');

        foreach (string r in UGPGDATA)
        {
            foreach (ListItem item in lstbxStudyLevel.Items)
            {
                if (r == item.Value)
                {

                    item.Selected = true;
                    break;
                }
            }
        }
    
    }
}