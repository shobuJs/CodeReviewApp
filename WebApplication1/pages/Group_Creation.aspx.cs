using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessLogic;

public partial class Projects_Group_Creation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController stud = new StudentController();
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
                    //Page Authorization
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = "add";
                    
                    objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                    objCommon.FillDropDownList(ddlGroupName, "ACD_BRIDGING_GROUP ", "DISTINCT GROUPTYPE", " GROUPNAME", "", " GROUPNAME");
                    pnlGroupName.Visible = false;
                    objCommon.SetLabelData("0");//for label
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Group_Creation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Group_Creation.aspx");
        }
    }

    protected void ddlIntake_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
    }
    
    protected void btnShow_Click(object sender, EventArgs e)
    {
        
        //string id = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON (A.BRANCHNO=B.BRANCHNO) INNER JOIN ACD_DEGREE C ON (C.DEGREENO=A.DEGREENO) INNER JOIN ACD_AFFILIATED_UNIVERSITY D on (C.AFFILIATED_NO=A.AFFILIATED_NO)", "B.BRANCHNO,A.DEGREENO,A.AFFILIATED_NO", "B.BRANCHNO=" + (lstbxProgram.SelectedValue) + "and A.DEGREENO=" + (lstbxProgram.SelectedValue) + "and A.AFFILIATED_NO="+ (lstbxProgram.SelectedValue));
        string Program = lstbxProgram.SelectedValue;
        string[] subs = Program.Split(',');
       ViewState["Branchno"] = Convert.ToInt32(subs[0]);
       ViewState["Degreeno"] = Convert.ToInt32(subs[1]);
       ViewState["AffUniversity"] = Convert.ToInt32(subs[2]);
       pnlGroupName.Visible = true;
        BindListViewGroupcreation();
        //int admbatch = Convert.ToInt32(objCommon.LookUp("ACD_BRIDGING_GROUP", "ADMBATCH", ""));
        //int College = Convert.ToInt32(objCommon.LookUp("ACD_BRIDGING_GROUP", "COLLEGE_ID", ""));

        //if (admbatch == Convert.ToInt32(ddlIntake.SelectedValue) && College == Convert.ToInt32(ddlFaculty.SelectedValue))
        //{
            //objCommon.FillDropDownList(ddlGroupName, "ACD_BRIDGING_GROUP  ", "DISTINCT BGROUPID", " GROUPNAME", "", " BGROUPID");
             objCommon.FillDropDownList(ddlGroupName, "ACD_BRIDGING_GROUP ", "DISTINCT BGROUPID", " GROUPNAME", "ADMBATCH=" + Convert.ToInt32(ddlIntake.SelectedValue) + " AND COLLEGE_ID="+Convert.ToInt32(ddlFaculty.SelectedValue), " BGROUPID");
        //}
        //else
        //{
        //    objCommon.DisplayMessage(this.Page, "Group is Not Created for this Intake and Faculty Please Create Group First", Page);
        //}
    }

    private void BindListViewGroupcreation()
    {
        try
        {
            DataSet ds;
            //string Program = "";

            int Faculty = Convert.ToInt32(ddlFaculty.SelectedValue);
            int StudyLevel = Convert.ToInt32(ddlStudyLevel.SelectedValue);

            //foreach (ListItem items in lstbxProgram.Items)
            //{
            //    if (items.Selected == true)
            //    {                    
            //        Program += items.Value + ',';
            //    }
            //}
            //Program = Program.Remove(Program.Length - 1);


            ds = stud.GetStudentListonGroupCreationpage(Convert.ToInt32(ddlIntake.SelectedValue), Faculty, StudyLevel, Convert.ToInt32(ViewState["Branchno"]), Convert.ToInt32(ViewState["Degreeno"]), Convert.ToInt32(ViewState["AffUniversity"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvGroupCreation.DataSource = ds.Tables[0];
                lvGroupCreation.DataBind();
                lvGroupCreation.Visible = true;
                
            }
            else
            {
                objCommon.DisplayMessage(Page, "No student data found", this.Page);
                lvGroupCreation.DataSource = null;
                lvGroupCreation.DataBind();
                pnlGroupName.Visible = false;
            }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test();", true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Bulkstudentupdate.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lvGroupCreation_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
            DropDownList ddlGrupName = (DropDownList)e.Item.FindControl("ddlGrupName");
            objCommon.FillDropDownList(ddlGrupName, "ACD_BRIDGING_GROUP ", "DISTINCT BGROUPID", " GROUPNAME", "ADMBATCH=" + Convert.ToInt32(ddlIntake.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(ddlFaculty.SelectedValue), " BGROUPID");
            //objCommon.FillDropDownList(ddlGrupName, "ACD_BRIDGING_GROUP  ", "DISTINCT BGROUPID", " GROUPNAME", "", " BGROUPID");
            HiddenField hdnGroupName = (HiddenField)e.Item.FindControl("hdnGroupName");
        
            ddlGrupName.SelectedValue = hdnGroupName.Value;
       
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       

        int count1 = 0;
        foreach (ListViewDataItem dataitem in lvGroupCreation.Items)
        {
            CheckBox cbRow = dataitem.FindControl("chkReport") as CheckBox;
            
            if (cbRow.Checked == true)
                count1++;
        }
        if (count1 <= 0)
        {
            //ClearControls();
            objCommon.DisplayMessage(this.Page, "Please Select Atleast One Student for Update.", this);
            return;
        }
        try
        {
            foreach (ListViewDataItem lvitem in lvGroupCreation.Items)
            {
                CheckBox cbRow = lvitem.FindControl("chkReport") as CheckBox;
                ViewState["GROUPID"] = cbRow.ToolTip.ToString();
                string groupid=cbRow.ToolTip.ToString();
               // Label lbl = lvitem.FindControl("lblIDNO") as Label;
                HiddenField hdnIdno = lvitem.FindControl("hdnIDNO") as HiddenField;
               // ViewState["IDNO"] = lbl.Text.ToString(); 
                ViewState["IDNO"] = hdnIdno.Value.ToString();
                int Admbatch = Convert.ToInt32(ddlIntake.SelectedValue);
                if (cbRow.Checked == true)
                {
                   // int count = 0;
                    int a = 0;
                    //TextBox txt = lvGroupCreation.FindControl("txtEditFieldDT") as TextBox;

                    DropDownList ddl = lvitem.FindControl("ddlGrupName") as DropDownList;

                    if (Convert.ToInt32(ddl.SelectedValue) != 0)
                    {

                        a = Convert.ToInt32(ddl.SelectedValue);
                    }

                    if (Convert.ToInt32(ddlGroupName.SelectedValue) != 0)
                    {
                        a = Convert.ToInt32(ddlGroupName.SelectedValue);
                    }
                    if (ViewState["action"] != null)
                    {
                        if (ViewState["action"].ToString().Equals("add"))
                        {
                            CustomStatus cs = (CustomStatus)stud.InsertGroupNameonGroupCreationPage(Convert.ToInt32(ViewState["IDNO"]), (a), Admbatch);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                               // count = 1;
                                objCommon.DisplayMessage(Page, "Record Saved Successfully.", this.Page);
                                //pnlGroupName.Visible = false;
                                BindListViewGroupcreation();
                            }
                        }
                        
                    }
                }
            }  
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Bulkstudentupdate.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlStudyLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillListBox(lstbxProgram, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON (A.BRANCHNO=B.BRANCHNO) INNER JOIN ACD_DEGREE C ON (C.DEGREENO=A.DEGREENO) INNER JOIN ACD_AFFILIATED_UNIVERSITY D on (D.AFFILIATED_NO=A.AFFILIATED_NO)", "CONCAT(B.BRANCHNO,',',A.DEGREENO,',',A.AFFILIATED_NO)", "CONCAT(LONGNAME,'- ',DEGREENAME,'-',AFFILIATED_SHORTNAME) as PROGRAME", "A.UGPGOT=" + ddlStudyLevel.SelectedValue, "(A.DEGREENO)");

        objCommon.FillListBox(lstbxProgram, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON (A.BRANCHNO=B.BRANCHNO) INNER JOIN ACD_DEGREE C ON (C.DEGREENO=A.DEGREENO) INNER JOIN ACD_AFFILIATED_UNIVERSITY D on (D.AFFILIATED_NO=A.AFFILIATED_NO)", "CONCAT(B.BRANCHNO,',',A.DEGREENO,',',A.AFFILIATED_NO)", "CONCAT(LONGNAME,'- ',DEGREENAME,'-',AFFILIATED_SHORTNAME) as PROGRAME", "A.UGPGOT=" + Convert.ToInt32(ddlStudyLevel.SelectedValue) + " AND A.COLLEGE_ID=" + Convert.ToInt32(ddlFaculty.SelectedValue), "(A.DEGREENO)");
        
       
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlStudyLevel, "ACD_UA_SECTION A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B on (A.UA_SECTION=B.UGPGOT)", "DISTINCT (UA_SECTION)", "A.UA_SECTIONNAME", "B.COLLEGE_ID=" + ddlFaculty.SelectedValue, "(UA_SECTION)");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        pnlGroupName.Visible = false;
    }
    protected void Clear()
    {
        ddlIntake.SelectedIndex = 0;
        ddlFaculty.SelectedIndex = 0;
        ddlStudyLevel.SelectedIndex = 0;
        lstbxProgram.ClearSelection();
        lvGroupCreation.Visible = false;
        pnlGroupName.Visible = false;
    }
}