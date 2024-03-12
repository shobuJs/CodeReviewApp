//======================================================================================
// PROJECT NAME  : SLIIT                                                  
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : LEAD GROUP CREATION                                                
// CREATION DATE : 10 NOVEMBER 2021                                               
// CREATED BY    : PRATIMA PATEL                                    
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

public partial class ACADEMIC_LeadGroupCreation : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    MappingController objmp = new MappingController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Config objConfig = new Config();
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
               
                if (Request.QueryString["pageno"] != null)
                {
                }

            }
            BindDropDownList();
            BindListView();
            ViewState["action"] = "add";
          }
        objCommon.SetLabelData(Convert.ToString(1));
        objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
      }
    private void BindDropDownList()
    {
       
       objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
       objCommon.FillListBox(ddlsubuser, "USER_ACC", "UA_NO", "UA_NAME", "ISNULL(UA_STATUS,0)=0 AND UA_NO>0 AND  UA_TYPE<>2", "UA_NO");   
       objCommon.FillDropDownList(ddlstudylevel, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "UA_SECTION");
       objCommon.FillDropDownList(ddlmainuser, "USER_ACC", "UA_NO", "UA_NAME", "ISNULL(UA_STATUS,0)=0 AND UA_NO>0 AND  UA_TYPE<>2", "UA_NO");  
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=LeadGroupCreation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=LeadGroupCreation.aspx");
        }
    }
    #endregion
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int ck = 0;
            int AMYNO = 0;
            Access_Link objAL = new Access_Link();

            if (hfdStat.Value == "true")
            {
                objAL.chklinkstatus = 1;
            }
            else
            {
                objAL.chklinkstatus = 0;
            }

            if (btnSave.Text == "Update")
            {
                AMYNO = Convert.ToInt32(ViewState["SR_NO"]);

                foreach (ListItem items in ddlsubuser.Items)
                {
                    if (items.Selected == true)
                    {
                        
                        objConfig.suser += items.Value + ',';
                    }

                }
                ck = objmp.editUserDetails(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlstudylevel.SelectedValue), Convert.ToInt32(ddlmainuser.SelectedValue), objConfig, AMYNO, Convert.ToInt32(Session["userno"]), objAL);
                BindListView();
                clear();
            }                                                                                               
           else
           {
             
                foreach (ListItem items in ddlsubuser.Items)
                {
                    if (items.Selected == true)
                    {
                        
                        objConfig.suser += items.Value + ',';
                    } 

                }
            objConfig.suser = objConfig.suser.Substring(0, objConfig.suser.Length - 1);
            ck = objmp.AddUserDetails(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlstudylevel.SelectedValue), Convert.ToInt32(ddlmainuser.SelectedValue), objConfig, Convert.ToInt32(Session["userno"]), objAL);
            BindListView();
            clear();
         }

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_CollegeMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void clear()
    {
       
        ddlAdmbatch.SelectedIndex = 0;
        ddlstudylevel.SelectedIndex = 0;
       
        btnSave.Text = "Submit";
        ddlmainuser.SelectedIndex = 0;
        ddlsubuser.SelectedValue = null;

    }
    private void BindListView()
    {
        try
        {
            
            DataSet ds = objmp.GetUserDetails(Convert.ToInt32(0), Convert.ToInt32(1));
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
            int srno = int.Parse(btnEdit.CommandArgument);
            
            ViewState["SR_NO"] = srno;
            ShowDetails(srno);
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
    private void ShowDetails(int srno)
    {
      
        try
        {
            BindDropDownList();
            int active = 0;
            DataSet ds = objmp.GetUserDetails(Convert.ToInt32(srno), Convert.ToInt32(2));
           if (ds.Tables[0].Rows.Count > 0)
            {
                ddlAdmbatch.SelectedValue = ds.Tables[0].Rows[0]["INTAKE_NO"].ToString();
                ddlstudylevel.SelectedValue = ds.Tables[0].Rows[0]["STUDY_LEVEL"].ToString();
                ddlmainuser.SelectedValue = ds.Tables[0].Rows[0]["MAINUSER_UA_NO"].ToString();
                string[] couser = Convert.ToString(ds.Tables[0].Rows[0]["SUBUSER_UA_NO"]).Split(',');
                
               foreach(string s in couser)
                {
                    foreach (ListItem item in ddlsubuser.Items)
                    {
                       if (s == item.Value)
                        {
                            item.Selected = true;
                            break;
                        } 
                    }
                }

                if (ds.Tables[0].Rows[0]["Status"].ToString().Trim() == "1")  
                {
                    
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(true);", true);
                }
                else
                {
                    
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(false);", true);
                }
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
    protected void ddlEndMonth_SelectedIndexChanged(object sender, EventArgs e)  
    {
        objCommon.FillListBox(ddlsubuser, "user_acc", "UA_NO", "UA_NAME", "ISNULL(UA_STATUS,0)=0 AND UA_NO>0 AND UA_TYPE<>2 AND UA_NO <>" + ddlmainuser.SelectedValue, "UA_NO");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
}

           