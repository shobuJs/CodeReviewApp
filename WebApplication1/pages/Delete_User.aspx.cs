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
using System;
using System.Windows.Forms;

public partial class ACADEMIC_Delete_User : System.Web.UI.Page
{
    //CourseController objCourse = new CourseController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    NewUserController newUser = new NewUserController();
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
            if (Session["userno"] == null || Session["username"] == null || Session["idno"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                this.CheckPageAuthorization();
                if (Session["usertype"].ToString() == "1" && Session["username"].ToString().ToUpper().Contains("PRINCIPAL"))
                {
                    // divShow.Visible=true;
                }
                else
                {
                    // divShow.Visible=false;
                }

                int Idno = Convert.ToInt32(Session["idno"].ToString());
                Session["IDNO"] = Idno;
                Page.Title = objCommon.LookUp("reff", "collegename", string.Empty);

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
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
                Response.Redirect("~/notauthorized.aspx?page=Delete_User.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Delete_User.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        string EnrollNo = txtEnrollmentSearch.Text;
        //DataSet ds = objCommon.FillDropDown("ACD_USER_REGISTRATION UR LEFT JOIN ACD_UA_SECTION US ON (UR.UGPGOT=US.UA_SECTION) LEFT JOIN ACD_ADMBATCH A ON (A.BATCHNO=UR.ADMBATCH) LEFT JOIN ACD_DEGREE D ON(UR.DEGREENO=D.DEGREENO)",
        //    "UR.USERNO", "(FIRSTNAME+' '+LASTNAME) AS NAME,US.SHORTNAME,BATCHNAME,EMAILID,DEGREENAME", "UR.USERNAME=" + EnrollNo, "");
        DataSet ds = newUser.Get_UserData(EnrollNo);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            
               DataRow drs = ds.Tables[0].Rows[0];
               ViewState["USERNO"] = drs["USERNO"].ToString();
               lblRegNo.Text = drs["USERNAME"].ToString();
               lblName.Text = drs["NAME"].ToString();
               lblbatch.Text= drs["BATCHNAME"].ToString();
               lbldegree.Text= drs["PROGRAM"].ToString();
               lblStudyLevel.Text= drs["SHORTNAME"].ToString();
               lblEmail .Text= drs["EMAILID"].ToString();
               divStudInfo.Visible = true;
        }
        else
        {
            objCommon.DisplayMessage(this, "Record Not Found", this.Page);
            return;
        }

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string EnrollNo = txtEnrollmentSearch.Text;
        int UserNo = Convert.ToInt32(ViewState["USERNO"].ToString());

        //string message = "Sure";
        //string title = "Close Window";
        //MessageBoxButtons buttons = MessageBoxButtons.YesNo;
        //DialogResult result = MessageBox.Show(message, title, buttons);
        //if (result == DialogResult.Yes)
        //{

        //    return;

            CustomStatus cs = (CustomStatus)newUser.DeleteUserRecord(EnrollNo, UserNo);

            if (cs == CustomStatus.RecordSaved)
            {
                divStudInfo.Visible = false;
                txtEnrollmentSearch.Text = "";
                Session["USERNO"] = null;
                objCommon.DisplayMessage(this, "Record Deleted Successfully !!", this.Page);
                return;

            }
            else
            {

                objCommon.DisplayMessage(this, "Record Not Delete", this.Page);
            }
       // }
        //else
        //{
        //    return;

        //}
    }


 
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}