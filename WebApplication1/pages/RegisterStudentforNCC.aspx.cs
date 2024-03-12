//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : STUDENT REGISTRATION(NCC/NSS/CLUB)
// CREATION DATE : 31/05/2020
// CREATED BY    : SNEHA G.DOBLE
// MODIFIED BY   : 
// MODIFIED DATE : 
// MODIFIED DESC : 
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_RegisterStudentforNCC : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    NCCActivityController objnccActivity = new NCCActivityController();
    NCCActivity objncc = new NCCActivity();

    #region Page Event
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
        try
        { 
            if (!Page.IsPostBack)
            {
                imgPhoto.ImageUrl = "~/images/nophoto.jpg";
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
                    ViewState["action"] = "add";
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    string Activity_type = string.Empty;
                    string Activity_typenss = string.Empty;
                    string Activity_typeclub = string.Empty;
                    DataSet dsncc = objCommon.FillDropDown("ACD_ASSIGNING_FACULTY_NSS", "ACTIVITY_TYPE_NO", "ACTIVITY_TYPE", "ACTIVITY_TYPE_NO=1 and UA_NO = " + Session["userno"].ToString(), "UA_NO");
                    DataSet dsnss = objCommon.FillDropDown("ACD_ASSIGNING_FACULTY_NSS", "ACTIVITY_TYPE_NO", "ACTIVITY_TYPE", " ACTIVITY_TYPE_NO=2 and UA_NO = " + Session["userno"].ToString(), "UA_NO");
                    DataSet dsclub = objCommon.FillDropDown("ACD_ASSIGNING_FACULTY_NSS", "ACTIVITY_TYPE_NO", "ACTIVITY_TYPE", "ACTIVITY_TYPE_NO=3 and UA_NO = " + Session["userno"].ToString(), "UA_NO");
                    if (dsncc.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsncc.Tables[0].Rows.Count; i++)
                        {                           
                            if (Activity_type == string.Empty) Activity_type = dsncc.Tables[0].Rows[i]["ACTIVITY_TYPE"].ToString();
                            else
                                Activity_type += "," + dsncc.Tables[0].Rows[i]["ACTIVITY_TYPE"].ToString();
                        }
                        //NCC Activity
                        objCommon.FillDropDownList(ddlncctype, "ACD_NCC_TYPE", "NCC_TYPE_NO", "NCC_TYPE", "NCC_TYPE_NO IN(" + Activity_type + ") AND STATUS='ACTIVE'", "NCC_TYPE");
                        //Ration Activity
                        objCommon.FillDropDownList(ddlration, "ACD_NCC_RATION", "NCC_RATION_NO", "NCC_RATION", "NCC_RATION_NO>0 AND STATUS='ACTIVE'", "NCC_RATION");
                    }
                    if (dsnss.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsnss.Tables[0].Rows.Count; i++)
                        {
                            if (Activity_typenss == string.Empty) Activity_typenss = dsnss.Tables[0].Rows[i]["ACTIVITY_TYPE"].ToString();
                            else
                                Activity_typenss += "," + dsnss.Tables[0].Rows[i]["ACTIVITY_TYPE"].ToString();
                        }
                        //NSS Activity
                        objCommon.FillDropDownList(ddlnsstype, "ACD_NSS_TYPE", "NSS_TYPE_NO", "NSS_TYPE", "NSS_TYPE_NO IN(" + Activity_typenss + ") AND STATUS='ACTIVE'", "NSS_TYPE");
                    }

                    if (dsclub.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsclub.Tables[0].Rows.Count; i++)
                        {
                            if (Activity_typeclub == string.Empty) Activity_typeclub = dsclub.Tables[0].Rows[i]["ACTIVITY_TYPE"].ToString();
                            else
                                Activity_typeclub += "," + dsclub.Tables[0].Rows[i]["ACTIVITY_TYPE"].ToString();
                        }
                        //CLUB Activity
                        objCommon.FillDropDownList(ddlclubtype, "ACD_CLUB_ACTIVITY", "CLUB_TYPE_NO", "CLUB_TYPE", "CLUB_TYPE_NO IN(" + Activity_typeclub + ") AND STATUS='ACTIVE'", "CLUB_TYPE");
                    }              
                }

                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                Session["trainTbl"] = null;
                //BindListView();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_LeaveAndHolidayEntry.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=MasterforAssignFacultyofNCC_NSS.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MasterforAssignFacultyofNCC_NSS.aspx");
        }
    }
    #endregion

    #region private methods
    private void FillDropDown()
    {
        //NCC Activity
        //objCommon.FillDropDownList(ddlncctype, "ACD_NCC_TYPE", "NCC_TYPE_NO", "NCC_TYPE", "NCC_TYPE_NO>0 AND STATUS='ACTIVE'", "NCC_TYPE");

        //objCommon.FillDropDownList(ddlration, "ACD_NCC_RATION", "NCC_RATION_NO", "NCC_RATION", "NCC_RATION_NO>0 AND STATUS='ACTIVE'", "NCC_RATION");
    }

    private void BindListView()
    {
        try
        {
            if (rdoactivity.SelectedValue == "1")
            {
                objncc.ActivityNo = 1;
                objncc.ActivityType = Convert.ToInt32(ddlncctype.SelectedValue);
            }
            if (rdoactivity.SelectedValue == "2")
            {
                objncc.ActivityNo = 2;
                objncc.ActivityType = Convert.ToInt32(ddlnsstype.SelectedValue);
            }
            if (rdoactivity.SelectedValue == "3")
            {
                objncc.ActivityNo = 3;
                objncc.ActivityType = Convert.ToInt32(ddlclubtype.SelectedValue);
            }
            DataSet ds = objnccActivity.GetStudentNametoNccActivity(objncc.ActivityNo, objncc.ActivityType);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvregdata.Visible = true;
                lvregdata.DataSource = ds;
                lvregdata.DataBind();
                lvStudInvol.Visible = false;
                txtStudInvol.Text = string.Empty;
            }
            else
            {
                lvregdata.Visible = false;
                lvregdata.DataSource = null;
                lvregdata.DataBind();
                lvStudInvol.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Activity.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region OnclickEvent

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string s = txtStudInvol.Text;
        string[] values = s.Split(',');
        string regno = string.Empty;
        string regno1 = string.Empty;
        for (int i = 0; i < values.Length; i++)
        {
            if (i == 0)
            {
                regno = "'" + values[i].Trim() + "'";
            }
            else
            {
                regno = ",'" + values[i].Trim() + "'";
            }
            regno1 = regno1 + regno;
        }

        DataSet ds = objCommon.FillDropDown("ACD_STUDENT A,ACD_SEMESTER B,ACD_BRANCH C", "IDNO,STUDNAME,A.REGNO,A.SEMESTERNO,A.BRANCHNO", "B.SEMESTERNAME,C.LONGNAME", "REGNO IN(" + regno1.ToString() + ") AND A.SEMESTERNO = B.SEMESTERNO AND A.BRANCHNO = C.BRANCHNO", "STUDNAME");
        Session["trainTbl"] = ds;
        if (ds.Tables[0].Rows.Count > 0)
        {
            btnsubmit.Enabled = true;
            divstuddata.Visible = true;
            lvStudInvol.DataSource = ds;
            lvStudInvol.DataBind();
            lvStudInvol.Visible = true;
        }
        else
        {
            divstuddata.Visible = false;
            objCommon.DisplayMessage("No Record Found..!!", this.Page);
        }
    }

    protected void rdoactivity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoactivity.SelectedValue == "1")
        {
            divncc.Visible = true;
            divration.Visible = true;
            divnss.Visible = false;
            divclub.Visible = false;
            lvregdata.Visible = false;
        }
        if (rdoactivity.SelectedValue == "2")
        {        
            divnss.Visible = true;
            divncc.Visible = false;
            divclub.Visible = false;
            divration.Visible = false;
            lvregdata.Visible = false;
        }
        if (rdoactivity.SelectedValue == "3")
        {            
            divclub.Visible = true;
            divncc.Visible = false;
            divnss.Visible = false;
            divration.Visible = false;
            lvregdata.Visible = false;
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        objncc.IP_Address = string.Empty;
        objncc.Ration_Type = 0;
        string idno = string.Empty;
        if (Session["trainTbl"] != null && ((DataSet)Session["trainTbl"]) != null)
        {
            DataSet dt;
            dt = (DataSet)Session["trainTbl"];

            foreach (DataTable table in dt.Tables)
            {
                foreach (DataRow dr in table.Rows)
                {
                    if (idno == string.Empty) idno = dr["IDNO"].ToString();
                    else
                        idno += "$" + dr["IDNO"].ToString();
                }
            }
        }
        if (rdoactivity.SelectedValue == "1")
        {
            objncc.ActivityType = Convert.ToInt32(ddlncctype.SelectedValue);
            objncc.Ration_Type = Convert.ToInt32(ddlration.SelectedValue);
        }
        if (rdoactivity.SelectedValue == "2")
        {
            objncc.ActivityType = Convert.ToInt32(ddlnsstype.SelectedValue);
        }
        if (rdoactivity.SelectedValue == "3")
        {
            objncc.ActivityType = Convert.ToInt32(ddlclubtype.SelectedValue);
        }
        idno = idno.TrimEnd('$');
        objncc.IDNOS = idno;
        objncc.ActivityNo = Convert.ToInt32(rdoactivity.SelectedValue);
        objncc.UANO = Convert.ToInt32(Session["userno"]);
        objncc.IP_Address = ViewState["ipAddress"].ToString();
        objncc.AddDate = Convert.ToDateTime(txtDate.Text);
        if (idno.Contains('$'))
        {
            if (objnccActivity.AddRegStudDetail(objncc.ActivityNo, objncc.ActivityType, objncc.Ration_Type, objncc.IDNOS, objncc.UANO, objncc.IP_Address,objncc.AddDate) == Convert.ToInt32(CustomStatus.RecordSaved))
            {
                BindListView();
                objCommon.DisplayMessage(this.updactivity, "Record Saved Successfully..!!", this.Page);
                btnsubmit.Enabled = false;
                return;
            }

        }      
        else
        {
            int count = Convert.ToInt32(objCommon.LookUp("ACD_STUD_REG_NCC", "count(*)", "ACTIVITY_NO = " + objncc.ActivityNo + " AND ACTIVITY_TYPE = " + objncc.ActivityType + " AND NCC_RATION_TYPE = " + objncc.Ration_Type + "AND IDNO = " + idno));
            if (count > 0)
            {
                int b1 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + idno));
                objCommon.DisplayMessage(this.updactivity, "Record Already Exists for this " + b1 + "", this.Page);
                ViewState["action"] = "add";
                lvStudInvol.Visible = false;
                return;
            }
        }
        objnccActivity.AddRegStudDetail(objncc.ActivityNo, objncc.ActivityType, objncc.Ration_Type, objncc.IDNOS, objncc.UANO, objncc.IP_Address,objncc.AddDate);        
        BindListView();
        objCommon.DisplayMessage(this.updactivity, "Record Saved Successfully..!!", this.Page);
        btnsubmit.Enabled = false;
        txtDate.Text = string.Empty;
    }

    protected void btnremove_Click(object sender, EventArgs e)
    {
        Button btnremove = (Button)(sender);
        int Reg_id = Convert.ToInt32(btnremove.CommandArgument);
        string Remark = string.Empty;
  
            TextBox txtDueDate = btnremove.FindControl("txtDueDate") as TextBox;
            TextBox txtRemark = btnremove.FindControl("txtremark") as TextBox;
            if (txtDueDate.Text ==string.Empty)
            {
                objCommon.DisplayMessage(this.updactivity, "Please Select Date.", this.Page);
            }
            if (txtRemark.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.updactivity, "Please Enter Remark for Remove", this.Page);
            }
            else
            {
                objncc.RemoveDate = Convert.ToDateTime(txtDueDate.Text);
                Remark = txtRemark.Text.ToString();
                if (objnccActivity.UpdateRemovedatestatus(Reg_id, objncc.RemoveDate,Remark) == Convert.ToInt32(CustomStatus.RecordUpdated))
                {
                    BindListView();
                    objCommon.DisplayMessage(this.updactivity, "Student Removed Successfully..!!", this.Page);
                    lvregdata.Visible = true;
                    lvStudInvol.Visible = false;
                    // BindListView();
                }
            }

           
       
    }

    protected void btnview_Click(object sender, EventArgs e)
    {
        Button btnview = (Button)(sender);
        int Idno = Convert.ToInt32(btnview.CommandArgument);

        DataSet ds = objnccActivity.GetStudentBasicDetails(Idno);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            lblRegNo.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
            lblEnrollNo.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
            lblbranch.Text = ds.Tables[0].Rows[0]["BRANCH"].ToString();
            lblsemester.Text = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
            lblMobNo.Text = ds.Tables[0].Rows[0]["MOB_NO"].ToString();
            lblBlood.Text = ds.Tables[0].Rows[0]["BLOOD_GROUP"].ToString();
            lblMailID.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
            lblcaddr.Text = ds.Tables[0].Rows[0]["COMM_ADDRESS"].ToString();
            imgPhoto.ImageUrl = "~/showimage.aspx?id=" + ds.Tables[0].Rows[0]["IDNO"].ToString() + "&type=STUDENT";            
        }
        else
        {
            objCommon.DisplayMessage(this.updmodal, "Data Not available for this Student", this.Page);
        }


    }

    #endregion

    #region SelectedIndexChanged

    protected void ddlncctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlncctype.SelectedIndex > 0)
        {
            BindListView();
        }
        else
        {
            ddlncctype.SelectedIndex = 0;
            ddlncctype.Focus();
        }
    }

    protected void ddlnsstype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlnsstype.SelectedIndex > 0)
        {
            BindListView();
        }
        else
        {
            ddlnsstype.SelectedIndex = 0;
            ddlnsstype.Focus();
        }
    }

    protected void ddlclubtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlclubtype.SelectedIndex > 0)
        {
            BindListView();
        }
        else
        {
            ddlclubtype.SelectedIndex = 0;
            ddlclubtype.Focus();
        }
    }

     #endregion



    
}