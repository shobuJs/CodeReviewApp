using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Projects_Intake_Lead_Mapping : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    IntakeLeadMapping objILM = new IntakeLeadMapping();
    IntakeLeadMappingController objILMC = new IntakeLeadMappingController();
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
    int mappingid;
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

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                    //this.CheckPageAuthorization();
                    ViewState["action"] = "add";
                    FilldropDown();
                    BindListview(mappingid);


                }
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header

                Page.Form.Attributes.Add("enctype", "multipart/form-data");
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

    private void FilldropDown()
    {
        objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVE = 1", "BATCHNO DESC");
        objCommon.FillListBox(lstbxStudyLevel, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "UA_SECTION");
        objCommon.FillListBox(lstbxDiscipline, "ACD_AREA_OF_INTEREST", "AREA_INT_NO", "AREA_INT_NAME", "", "AREA_INT_NO");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string _validateListBox = validateListBox();
            if (_validateListBox != string.Empty)
            {
                objCommon.DisplayMessage(updIntake, _validateListBox, this.Page);
                return;
            }
            objILM.intakeno = Convert.ToInt32(ddlIntake.SelectedValue);
            string StudyLevel = "";
            foreach (ListItem item in lstbxStudyLevel.Items)
            {
                if (item.Selected == true)
                {
                    StudyLevel += item.Value + ',';
                }
            }
            if (!string.IsNullOrEmpty(StudyLevel))
            {
                StudyLevel = StudyLevel.Substring(0, StudyLevel.Length - 1);
            }
            else
            {
                StudyLevel = "0";

            }
            objILM.ua_section = StudyLevel;
            string Discipline = "";
            foreach (ListItem item in lstbxDiscipline.Items)
            {
                if (item.Selected == true)
                {
                    Discipline += item.Value + ',';
                }
            }
            if (!string.IsNullOrEmpty(Discipline))
            {
                Discipline = Discipline.Substring(0, Discipline.Length - 1);
            }
            else
            {
                Discipline = "0";

            }
            int MappingId;
            objILM.area_int_no = Discipline;

      
            if (ViewState["action"].ToString() == "add")
            {
                MappingId = 0;
                objILM.mappingid = 0;
            }
            else
            {
                MappingId = Convert.ToInt32(ViewState["MAPPING_ID"]);
                objILM.mappingid = Convert.ToInt32(ViewState["MAPPING_ID"]);
            }
       
            CustomStatus cs = (CustomStatus)objILMC.AddIntakeLeadMapping(objILM, MappingId);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updIntake, "Intake Details Added Successfully.", this.Page);
                clear();
                BindListview(mappingid);
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.updIntake, "Intake Details Updated Successfully ", this.Page);
                ViewState["action"] = "add";
                clear();
                BindListview(mappingid);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Intake_Lead_Mapping.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private string validateListBox()
    {
        string _validate = string.Empty;
        if (lstbxStudyLevel.SelectedValue == string.Empty)
        {
            _validate = "Please Select Atleast One Study Level";
        }
        else if (lstbxDiscipline.SelectedValue == string.Empty)
        {
            _validate = "Please Select Atleast One Discipline";
        }
       
        return _validate;
    }

    private void clear()
    {      
        ddlIntake.SelectedIndex = 0;
        ddlIntake.Enabled = true;
        lstbxStudyLevel.ClearSelection();
        lstbxDiscipline.ClearSelection();
    }
    protected void BindListview(int MappingId)
    {
        DataSet ds = objILMC.GetIntakeLeadMapping(MappingId);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvIntakeDetail.DataSource = ds;
            lvIntakeDetail.DataBind();
        }
        else
        {
            lvIntakeDetail.DataSource = null;
            lvIntakeDetail.DataBind();
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        clear();
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int MAPPING_ID = int.Parse(btnEdit.CommandArgument);
            ViewState["MAPPING_ID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(MAPPING_ID);
            ViewState["action"] = "edit";
        }
        
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Intake_Lead_Mapping.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int MAPPING_ID)
    {      
        DataSet ds = objCommon.FillDropDown("ACD_INTAKE_SECTION_AREA_OF_INT_MAPPING", "MAPPING_ID", "ADMBATCH,UGPGOT,AREA_OF_INT", "MAPPING_ID = " + MAPPING_ID + "", "");

        ddlIntake.SelectedValue = ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
      
        ddlIntake.Enabled = false;
        char delimiterChars = ',';

        string studylevel = ds.Tables[0].Rows[0]["UGPGOT"].ToString();
        string[] stu = studylevel.Split(delimiterChars);
        for (int j = 0; j < stu.Length; j++)
        {
            for (int i = 0; i < lstbxStudyLevel.Items.Count; i++)
            {
                if (stu[j].Trim() == lstbxStudyLevel.Items[i].Value.Trim())
                {
                    lstbxStudyLevel.Items[i].Selected = true;
                }
            }
        }
    
        string discipline = ds.Tables[0].Rows[0]["AREA_OF_INT"].ToString();
        string[] dis = discipline.Split(delimiterChars);
        for (int j = 0; j < dis.Length; j++)
        {
            for (int i = 0; i < lstbxDiscipline.Items.Count; i++)
            {              
                if (dis[j].Trim() == lstbxDiscipline.Items[i].Value.Trim())
                {
                    lstbxDiscipline.Items[i].Selected = true;
                }
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
        ViewState["action"] = "add";
    }
}