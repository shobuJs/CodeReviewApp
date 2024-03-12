using System.Linq;
using System.Web;
using System.Xml.Linq;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Web.Configuration;

public partial class ACADEMIC_CollegeWise_RollList : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objCourse = new CourseController();
    Course objc = new Course();
 
   // string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
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
               
                   
                    FilldropDown();
                    
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
                Response.Redirect("~/notauthorized.aspx?page=CollegeMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CollegeMaster.aspx");
        }
    }

    private void FilldropDown()
    {
        objCommon.FillDropDownList(ddlyear, "ACD_EXAM_YEAR", "EXYEAR_NO", "YEAR_NAME", "", "EXYEAR_NO");
       
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
          
            int year = Convert.ToInt32(ddlyear.SelectedValue);
            int ua_no = Convert.ToInt32(Session["userno"]);
            string startrange = string.Empty;
            string endrange = string.Empty;
            int college_id = 0;

            CustomStatus cs = 0;
            foreach (ListViewDataItem dataitem in lvCollgeRoll.Items)
            {

                TextBox txtStartrange = dataitem.FindControl("txtstartrange") as TextBox;
                TextBox txtendrange = dataitem.FindControl("txtendrange") as TextBox;
                TextBox txtcollege = dataitem.FindControl("txtcollege") as TextBox;
                Label lblcollege = dataitem.FindControl("lblclg") as Label;
                CheckBox chk = dataitem.FindControl("cbRow") as CheckBox;
                if (chk.Checked == true)
                {
                    if (txtStartrange.Text == "" && txtendrange.Text == "")
                    {
                        objCommon.DisplayMessage(this.updCollegeroll, "Please Enter Details", this.Page);
                        goto noresult;
                    }
                    college_id = Convert.ToInt32(lblcollege.ToolTip);
                    startrange = txtStartrange.Text;
                    endrange = txtendrange.Text;

                    cs = (CustomStatus)objCourse.InsertCollegeroll(Convert.ToInt32(ddlyear.SelectedValue), college_id, startrange, endrange);
                }

            }
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                 //BindData();

                objCommon.DisplayMessage(this.updCollegeroll, "Data updated successfully.", this.Page);
                clear();
            }
            else if (cs.Equals(CustomStatus.RecordSaved))
            {
                //BindData();
                objCommon.DisplayMessage(this.updCollegeroll, "Data saved successfully.", this.Page);
                clear();
            }

            else
            {
                objCommon.DisplayMessage(this.updCollegeroll, "Error in Saving", this.Page);
            }

        noresult:
            { }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Offered_Course.btnAd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {                                                                                      
        clear();
    }
    private void clear()
    {
        ddlyear.SelectedIndex = 0;
        lvCollgeRoll.DataSource = null;
        lvCollgeRoll.DataBind();
    }

    private void BindData()
    {
        try
        {
            DataSet ds = objCourse.GetCollegeRollList(Convert.ToInt32(ddlyear.SelectedValue));
            lvCollgeRoll.DataSource = ds;
            lvCollgeRoll.DataBind();            
        }

        catch (Exception ex)
        {

        }
    }
    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}