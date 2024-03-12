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
using System.Text;
using System.IO;

public partial class ACADEMIC_StudentDetails : System.Web.UI.Page
{
    StudentController objSC = new StudentController();
    Common objCommon = new Common();

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
                CheckPageAuthorization();
                filldropdown();
                Page.Title = Session["coll_name"].ToString();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
            }
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentInfoEntryNew.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentInfoEntryNew.aspx");
        }
    }
    protected void filldropdown()
    {
        objCommon.FillDropDownList(ddlIntake, "ACD_ADMBATCH", "DISTINCT BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
        objCommon.FillDropDownList(ddlStudyLevel, "ACD_UA_SECTION", "DISTINCT UA_SECTION", "UA_SECTIONNAME", "UA_SECTION > 0", "");

        objCommon.FillDropDownList(ddlAdmissionIntake, "ACD_ADMBATCH", "DISTINCT BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
        objCommon.FillDropDownList(ddlAdmissionStudyLevel, "ACD_UA_SECTION", "DISTINCT UA_SECTION", "UA_SECTIONNAME", "UA_SECTION > 0", "");
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GVStudData = new GridView();
             string StartDate = "", EndDate = "";
            string[] SplitDate = hdnDate.Value.Split('-');
            StartDate = SplitDate[0];
            EndDate = SplitDate[1];
            DataSet ds = ds = objSC.GetStudentAllDetails(Convert.ToInt32(ddlIntake.SelectedValue), Convert.ToInt32(ddlStudyLevel.SelectedValue),Convert.ToInt32(rdOption.SelectedValue),Convert.ToString(StartDate),Convert.ToString(EndDate));
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVStudData.DataSource = ds;
                    GVStudData.DataBind();

                    string attachment = "attachment;filename=StudentDetails.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.Charset = "";
                    Response.ContentType = "application/ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GVStudData.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();

                }
                else
                {
                    objCommon.DisplayMessage("No Data Found", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage("No Data Found", this.Page);
            }
            //DataSet ds = null;
            //ds = objSC.GetStudentAllDetails(Convert.ToInt32(ddlIntake.SelectedValue), Convert.ToInt32(ddlStudyLevel.SelectedValue));
            //if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    lvStudentDetails.DataSource = ds.Tables[0];
            //    lvStudentDetails.DataBind();
            //}
            //else
            //{
            //    objCommon.DisplayMessage(updStudent, "Record Not Found !!!", this.Page);
            //}
            //ds.Dispose();
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void rdDataOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(rdDataOption.SelectedValue) == 1)
            {
                divEnrollment.Visible = false;
                divAdmission.Visible = true;
            }
            else
            {
                divAdmission.Visible = false;
                divEnrollment.Visible = true;
            }
        }
        catch (Exception ex)
        { 
            
        }
    }
    protected void btnAdmissionShow_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GVStudData = new GridView();
            string StartDate = "", EndDate = "";
            string[] SplitDate = hdnDate.Value.Split('-');
            StartDate = SplitDate[0];
            EndDate = SplitDate[1];

            DataSet ds = ds = objSC.GetStudentAllDetails(Convert.ToInt32(ddlAdmissionIntake.SelectedValue), Convert.ToInt32(ddlAdmissionStudyLevel.SelectedValue), Convert.ToInt32(rdAdmissionOption.SelectedValue),Convert.ToString(StartDate),Convert.ToString(EndDate));
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVStudData.DataSource = ds;
                    GVStudData.DataBind();

                    string attachment = "attachment;filename=StudentDetails.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.Charset = "";
                    Response.ContentType = "application/ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GVStudData.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();

                }
                else
                {
                    objCommon.DisplayMessage(updStudent, "No Data Found", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(updStudent,"No Data Found", this.Page);
            }
        }
        catch (Exception ex)
        { 
        
        }
    }
}