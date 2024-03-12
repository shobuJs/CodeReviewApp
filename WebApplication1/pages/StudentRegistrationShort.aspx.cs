using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ACADEMIC_StudentRegistrationShort : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    StudentRegist objstud = new StudentRegist();

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
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

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
                objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
                objCommon.FillDropDownList(ddlCore, "ACD_CORE_SUBJECT", "Core_SubID", "Core_Sub_Name", "Core_SubID>0", "Core_SubID");
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                objCommon.FillDropDownList(ddlCollegeName, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");

                Cancel();
            }
        }

        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DecodingGeneration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DecodingGeneration.aspx");
        }
    }

    private void Bindlistview()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT A INNER JOIN ACD_BRANCH B ON A.BRANCHNO = B.BRANCHNO INNER JOIN ACD_SEMESTER C ON A.SEMESTERNO = C.SEMESTERNO INNER JOIN ACD_DEGREE D ON A.DEGREENO= D.DEGREENO INNER JOIN ACD_COLLEGE_MASTER CM ON A.COLLEGE_ID = CM.COLLEGE_ID", "A.REGNO", " A.STUDNAME,A.ENROLLNO,B.LONGNAME,C.SEMESTERNAME,D.DEGREENAME,CM.COLLEGE_NAME", "A.COLLEGE_ID=" + ddlCollegeName.SelectedValue + "AND A.DEGREENO=" + ddlDegree.SelectedValue + "AND A.BRANCHNO=" + ddlBranch.SelectedValue + "AND A.SEMESTERNO=" + ddlSemester.SelectedValue, "");

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvstudDetails.DataSource = ds;
                lvstudDetails.DataBind();
            }
            else
            {
                lvstudDetails.DataSource = null;
                lvstudDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ReceiveApplicationStatus.ShowStudents()> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + txtRollno.Text + "'");
            if (idno != "" || idno != string.Empty)
            {
                objCommon.DisplayMessage(updStudent, "Entered examination roll no. already exists!!", this.Page);
                txtRollno.Text = string.Empty;
                return;
            }
            objstud.STUDNAME = txtStudname.Text.Trim();
            objstud.REGNO = txtRegno.Text.Trim();
            objstud.ROLLNO = txtRollno.Text.Trim();
            objstud.BATCHNO = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            objstud.COLLEGEID = Convert.ToInt32(ddlCollegeName.SelectedValue);
            objstud.DEGREENO = Convert.ToInt32(ddlDegree.SelectedValue);
            objstud.BRANCHNO = Convert.ToInt32(ddlBranch.SelectedValue);
            objstud.SCHEMENO = Convert.ToInt32(ddlScheme.SelectedValue);
            objstud.SEMESTERNO = Convert.ToInt32(ddlSemester.SelectedValue);

            int check = objSReg.InsertStudentDetails(objstud);
            if (check == 1)
            {
                objCommon.DisplayMessage(this.updStudent, "Student Details submitted sucessfully!!", this.Page);
                Cancel();
            }
            else
            {
                objCommon.DisplayMessage(this.updStudent, "Failed To submit Student Details !!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_batchallotment.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnStudlist_Click(object sender, EventArgs e)
    {
        Bindlistview();
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

    }

    private void Cancel()
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlCollegeName.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlCore.SelectedIndex = 0;
        txtRegno.Text = string.Empty;
        txtRollno.Text = string.Empty;
        txtStudname.Text = string.Empty;

    }

    protected void ddlCollegeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C, ACD_COLLEGE_DEGREE_BRANCH CD", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND CD.DEGREENO=D.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlCollegeName.SelectedValue + "AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "DEGREENO");
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedValue == "7" || ddlDegree.SelectedValue == "5" || ddlDegree.SelectedValue == "2")
        {
            ddlCore.Enabled = true;
            rfvCourse.Visible = true;
        }
        else
        {
            ddlCore.Enabled = false;
            rfvCourse.Visible = false;
        }

        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");

            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlDegree.SelectedIndex = 0;
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");
            ddlScheme.Focus();
        }
        else
        {
            ddlScheme.Items.Clear();
            ddlBranch.SelectedIndex = 0;
        }
    }
}