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
using IITMS.SQLServer.SQLDAL;
using System.Web;
using System.Web.Security;
using System.Data;

public partial class ACADEMIC_StudentCompleteStatusList : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //USED FOR INITIALSING THE MASTER PAGE
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

                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }
                objCommon.FillDropDownList(ddlsession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSION_NAME desc");
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "SEMESTERNAME");
                objCommon.FillDropDownList(ddlfaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");

                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
            }
        }
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
    }

    //to check page is authorized or not
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentCompleteStatusList.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page+
            Response.Redirect("~/notauthorized.aspx?page=StudentCompleteStatusList.aspx");
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        string[] Program;
        if (ddlpgm.SelectedValue == "0")
        {
            Program = "0,0".Split(',');
        }
        else
        {
            Program = ddlpgm.SelectedValue.Split(',');
        }
        string SP_Name1 = "PKG_GET_STUDENT_COMPLETE_STATUS";
        string SP_Parameters1 = "@P_SESSION_NO,@P_SEMESTER_NO,@P_COLLEGE_ID,@P_DEGREENO,@P_BRANCHNO";
        string Call_Values1 = "" + ddlsession.SelectedValue + "," + ddlSemester.SelectedValue + "," + ddlfaculty.SelectedValue + "," + Program[0] + "," + Program[1] + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudents.DataSource = ds;
            lvStudents.DataBind();
            int  Counts = lvStudents.Items.Count;
            lblCount.Text = Counts.ToString();
            divCount.Visible = true;
        }
        else
        {
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            objCommon.DisplayMessage(this, "Record Not Found", this.Page);
            return;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlfaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlpgm, "ACD_COLLEGE_DEGREE_BRANCH S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=S.BRANCHNO)", "DISTINCT CONCAT(D.DEGREENO, ', ',B.BRANCHNO)AS ID", "(D.DEGREENAME+'-'+B.LONGNAME)AS DEGBRANCH", "S.DEGREENO>0 AND S.COLLEGE_ID=" + ddlfaculty.SelectedValue, "ID");

    }
}