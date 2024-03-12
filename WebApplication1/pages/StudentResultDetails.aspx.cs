using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;



public partial class ACADEMIC_StudentResultDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSRegist = new StudentRegistration();
   
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
            if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();

                GetStudentInfo(Convert.ToInt32(Session["idno"].ToString()));
                BindlvResult(Convert.ToInt32(Session["idno"].ToString()));
                txtRemark.Attributes["maxlength"] = "600";
            }          
        }      
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentResultDetails.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=StudentResultDetails.aspx");
        }
    }

    private void GetStudentInfo(int idno)
    {
        if (idno != 0)
        {
            DataTableReader dtr = objSRegist.GetStudentDetails(Convert.ToInt32(idno));

            if (dtr.Read())
            {
                lblStudName.Text = dtr["STUDNAME"].ToString();
                lblRegNo.Text = dtr["REGNO"].ToString().Equals(DBNull.Value) ? "" : dtr["REGNO"].ToString();
                lblEnrollno.Text = dtr["ENROLLNO"].ToString().Equals(DBNull.Value) ? "" : dtr["ENROLLNO"].ToString();
                lblDegree.Text = dtr["DEGREENAME"].ToString().Equals(DBNull.Value) ? "" : dtr["DEGREENAME"].ToString();
                lblBranch.Text = dtr["BRANCH"].ToString().Equals(DBNull.Value) ? "" : dtr["BRANCH"].ToString();
                lblEmail.Text = dtr["EMAILID"].ToString().Equals(DBNull.Value) ? "" : dtr["EMAILID"].ToString();
                lblMobile.Text = dtr["STUDENTMOBILE"].ToString().Equals(DBNull.Value) ? "" : dtr["STUDENTMOBILE"].ToString();
                lblAdmBatch.Text = dtr["BATCHNAME"].ToString().Equals(DBNull.Value) ? "" : dtr["BATCHNAME"].ToString();
            }
        }
    }

    private void BindlvResult(int idno)
    {
        try
        {
            //DataSet ds1 = objSRegist.GetResultDetailsCGPA(Convert.ToInt32(idno));
            //if (ds1.Tables[0].Rows.Count > 0)
            //{
            //    lvResultCGPA.DataSource = ds1;
            //    lvResultCGPA.DataBind();
            //    lvResultCGPA.Visible = true;
            //}
            //else
            //{
            //    lvResultCGPA.Visible = false;
            //    lvResultCGPA.DataSource = null;
            //    lvResultCGPA.DataBind();
            //}

            DataSet ds = objSRegist.GetResultDetails(Convert.ToInt32(idno));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvResult.DataSource = ds;
                lvResult.DataBind();
                lvResult.Visible = true;
                lblcgpa.Text = ds.Tables[0].Rows[0]["CGPA"].ToString();
                divDetails.Visible = true;
            }
            else
            {
                lvResult.Visible = false;
                lvResult.DataSource = null;
                lvResult.DataBind();
                divDetails.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_publication_Details .BindListViewMeetingDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int applied = 0;

        if (rbYes.Checked == true)
        {
            if (txtRemark.Text.Trim() != string.Empty || txtRemark.Text.Trim() != "")
            {
                applied = 1;
            }
            else
            {
                objCommon.DisplayMessage(updpnlExam, "Please enter the detailed remark!", this.Page);
                txtRemark.Text = string.Empty;
                return;
            }
        }      

        CustomStatus cs = (CustomStatus)objSRegist.AddResultRemark(Convert.ToInt32(Session["idno"].ToString()), txtRemark.Text, Session["ipAddress"].ToString(), applied);

        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(updpnlExam, "Record Saved Successfully!", this.Page);
            txtRemark.Text = string.Empty;
            rbYes.Checked = false;
            rbNo.Checked = true;
            divRemark.Visible = false;
        }          
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtRemark.Text = string.Empty;
        rbYes.Checked = false;
        rbNo.Checked = true;
        divRemark.Visible = false;
    }

    protected void rbYes_CheckedChanged(object sender, EventArgs e)
    {
        if (rbYes.Checked == true)
        {
            divRemark.Visible = true;
        }
        else
        {
            divRemark.Visible = false;
        }
    }

    protected void rbNo_CheckedChanged(object sender, EventArgs e)
    {
        if (rbNo.Checked == true)
        {
            divRemark.Visible = false;
        }
        else
        {
            divRemark.Visible = true;
        }
    }

}
