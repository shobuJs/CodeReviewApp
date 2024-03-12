//======================================================================================
// PROJECT NAME  : SLIIT                                                  
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : ADMBATCH MONTH YEAR MAPPING                                                   
// CREATION DATE : 14 JULY 2021                                               
// CREATED BY    : PANKAJ NAKHALE                                     
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class ACADEMIC_Intake : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    MappingController objmp = new MappingController();
    UAIMS_Common objUCommon = new UAIMS_Common();


    string DepartNos = string.Empty;
    //USED FOR INITIALSING THE MASTER PAGE
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    //USED FOR BYDEFAULT LOADING THE default PAGE
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
                }

            }
            BindDropDownList();
            BindListView();
            ViewState["action"] = "add";
        }

        objCommon.SetLabelData(Convert.ToString(1));
        objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // dynamic page header addded by sarang 09/07/2021
    }
    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page. 
    private void BindDropDownList()
    {
       // objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO");
        //objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");
        objCommon.FillDropDownList(ddlYear, "ACD_EXAM_YEAR", "EXYEAR_NO", "YEAR_NAME", "EXYEAR_NO>0", "EXYEAR_NO desc");
        objCommon.FillDropDownList(ddlMonth, "ACD_MONTH", "MONTHNO", "MONTH", "MONTHNO>0", "MONTHNO");
        objCommon.FillDropDownList(ddlEndMonth, "ACD_MONTH", "MONTHNO", "MONTH", "MONTHNO>0", "MONTHNO");
        objCommon.FillListBox(ddlstudylevel, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "UA_SECTION");
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DegreeMapping.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DegreeMapping.aspx");
        }
    }
    #endregion

    //protected void btnSave_Click(object sender, EventArgs e)
    //{

    //}
    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(Request.Url.ToString());
    //}
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            int ck = 0;
            int AMYNO = 0;
            Access_Link objAL = new Access_Link();

            // added by swapnil thakare on dated 04-08-2021 
            if (hfdStat.Value == "true")
            {
                objAL.chklinkstatus = 1;
            }
            else
            {
                objAL.chklinkstatus = 0;
            }
            string faculty = "";
            foreach (ListItem item in ddlstudylevel.Items)
            {
                if (item.Selected == true)
                {
                    faculty += item.Value + ',';
                }
            }
            if (!string.IsNullOrEmpty(faculty))
            {
                faculty = faculty.Substring(0, faculty.Length - 1);
            }
            else
            {
                faculty = "0";
            }
           
            if (btnSave.Text == "Update")
            {
                AMYNO = Convert.ToInt32(ViewState["BATCHNO"]);
                ck = objmp.AddAdmBatchYearMonth(Convert.ToString(txtAdmbatch.Text), Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue), objAL, AMYNO, Convert.ToInt32(ddlEndMonth.SelectedValue), Convert.ToString(hdnDate.Value), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ViewState["id"]), faculty);
                // int BATCHNO, int YEAR, int MONTH, int ACTIVE


                //int ck = objmp.AddDept(Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue));
                if (ck == 1)
                {
                    objCommon.DisplayMessage(this.updGradeEntry, "Record Update Successfully", this.Page);
                    BindListView();
                    clear();
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(this.updGradeEntry, "Few or All Record already Exist", this.Page);
                    return;
                }
            }
            else
            {
                AMYNO = 0;
                ck = objmp.AddAdmBatchYearMonth(Convert.ToString(txtAdmbatch.Text), Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue), objAL, AMYNO, Convert.ToInt32(ddlEndMonth.SelectedValue), Convert.ToString(hdnDate.Value), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ViewState["id"]), faculty);
                // int BATCHNO, int YEAR, int MONTH, int ACTIVE


                //int ck = objmp.AddDept(Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue));
                if (ck == 1)
                {
                    objCommon.DisplayMessage(this.updGradeEntry, "Record Saved Successfully", this.Page);
                    BindListView();
                    clear();
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(this.updGradeEntry, "Record already Exist", this.Page);
                    return;
                }
            }

        }

        catch { }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void clear()
    {
        txtAdmbatch.Text = "";
        ddlYear.SelectedIndex = 0;
       // ddlYear.SelectedValue = "0";
        ddlMonth.SelectedIndex = 0;
        ddlstudylevel.ClearSelection();
        //chknlstatus.Checked = false;
        ddlEndMonth.SelectedIndex = 0;
        btnSave.Text = "Submit";
    }

    private void BindListView()
    {
        try
        {
            //DataSet ds = objCommon.FillDropDown("ACD_ADMBATCH_MONTH_YEAR_MAPPING AMY  INNER JOIN ACD_ADMBATCH B ON B.BATCHNO=AMY.BATCHNO  INNER JOIN ACD_YEAR Y ON Y.YEAR=AMY.YEAR  INNER JOIN ACD_MONTH M ON M.MONTHNO=AMY.MONTHNO", "AMYNO,AMY.BATCHNO,AMY.YEAR,AMY.MONTHNO", "BATCHNAME,YEARNAME,MONTH ,CASE WHEN ACTIVE=1 THEN 'ACTIVE' ELSE 'DEACTIVE'  END ACTIVE", string.Empty, "M.MONTHNO");
            DataSet ds = objmp.GetAdmbatchYearMonthDetails(Convert.ToString(0), Convert.ToInt32(0), Convert.ToInt32(1));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvlist.DataSource = ds;
                lvlist.DataBind();
            }
            else
            {
                lvlist.DataSource = null;
                lvlist.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_dePT.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int Batchno = int.Parse(btnEdit.CommandArgument);
            int id = int.Parse(btnEdit.CommandName);
            ViewState["id"] = id;
            //Label1.Text = string.Empty;
            //HiddenField1.Value = int.Parse(btnEdit.CommandArgument).ToString();
            ViewState["BATCHNO"] = Batchno;
            ShowDetails(Batchno);
            btnSave.Text = "Update";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_CollegeMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int Batchno)
    {
        string sDegree = string.Empty;
        try
        {
            BindDropDownList();
            int active = 0;
            DataSet ds = objmp.GetAdmbatchYearMonthDetails(Convert.ToString(txtAdmbatch.Text), Convert.ToInt32(Batchno), Convert.ToInt32(2));//objCommon.FillDropDown("ACD_ADMBATCH_MONTH_YEAR_MAPPING", "AMYNO", "BATCHNO,YEAR,MONTHNO,ACTIVE,ISNULL(ENDMONTHNO,0)ENDMONTHNO,START_END_DATE", "AMYNO=" + AMYNO, "AMYNO");
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtAdmbatch.Text = ds.Tables[0].Rows[0]["BATCHNAME"].ToString();
               // ddlYear.SelectedItem.Text = ds.Tables[0].Rows[0]["YEAR"].ToString();
                int year = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_YEAR", "EXYEAR_NO", "YEAR_NAME=" + Convert.ToInt32(ds.Tables[0].Rows[0]["YEAR"].ToString())));
                ddlYear.SelectedValue = year.ToString();
                ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["STARTMONTHNO"].ToString();
                ddlEndMonth.SelectedValue = ds.Tables[0].Rows[0]["ENDMONTHNO"].ToString();

                hdnDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["START_END_DATE"].ToString().Split('-')[0]).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(ds.Tables[0].Rows[0]["START_END_DATE"].ToString().Split('-')[1]).ToString("MMM dd, yyyy");
                char delimiterChars = ',';
                string faculty = "";
                faculty = ds.Tables[0].Rows[0]["UA_SECTION"].ToString();
                string[] fac = faculty.Split(delimiterChars);
                for (int j = 0; j < fac.Length; j++)
                {
                    for (int i = 0; i < ddlstudylevel.Items.Count; i++)
                    {
                        if (fac[j] == ddlstudylevel.Items[i].Value)
                        {
                            ddlstudylevel.Items[i].Selected = true;
                        }
                    }
                }
                if (ds.Tables[0].Rows[0]["START_END_DATE"].ToString() == string.Empty)
                    hdnDate.Value = "";
                else
                hdnDate.Value = ds.Tables[0].Rows[0]["START_END_DATE"].ToString();
                //Session["EDITDATE"] = ds.Tables[0].Rows[0]["START_END_DATE"].ToString();

                if (ds.Tables[0].Rows[0]["ACTIVE"].ToString().Trim() == "1")  // added by swapnil thakare on dated 04-08-2021 
                {
                    //chkActive.Checked = true;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(true);", true);
                }
                else
                {
                    //chkActive.Checked = false;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(false);", true);
                }

                ScriptManager.RegisterClientScriptBlock(updGradeEntry, updGradeEntry.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
                //active = Convert.ToInt32(ds.Tables[0].Rows[0]["ACTIVE"].ToString());
                //if (active == 1)
                //{
                //   // chknlstatus.Checked = true;
                //}
                //else
                //{
                //   // chknlstatus.Checked = false;
                //}
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Master_CollegeMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //protected void ddlAdmbatch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //DataSet ds = objCommon.FillDropDown("ACD_ADMBATCH_MONTH_YEAR_MAPPING AMY  INNER JOIN ACD_ADMBATCH B ON B.BATCHNO=AMY.BATCHNO  INNER JOIN ACD_YEAR Y ON Y.YEAR=AMY.YEAR  INNER JOIN ACD_MONTH M ON M.MONTHNO=AMY.MONTHNO", "AMYNO,AMY.BATCHNO,AMY.YEAR,AMY.MONTHNO", "BATCHNAME,YEARNAME,MONTH ,CASE WHEN ACTIVE=1 THEN 'ACTIVE' ELSE 'DEACTIVE'  END ACTIVE", string.Empty, "M.MONTHNO");
    //        DataSet ds = objmp.GetAdmbatchYearMonthDetails(Convert.ToInt32(ddlAdmbatch.SelectedValue),Convert.ToInt32(0),Convert.ToInt32(1));

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            lvlist.DataSource = ds;
    //            lvlist.DataBind();
    //        }
    //        else
    //        {
    //            lvlist.DataSource = null;
    //            lvlist.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_Masters_dePT.BindListView-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}
}