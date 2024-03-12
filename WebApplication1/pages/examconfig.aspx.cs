//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : EXAM CONFIG                    
// CREATION DATE : 23-MAY-2009                                                          
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

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
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class Academic_examconfig : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                PopulateDropDownList();
                BindExamList();
            }
        }

    }

    private void BindExamList()
    {
        try
        {
            ExamController objEC = new ExamController();
            DataSet dsExam = objEC.GetAllConfigExam(Convert.ToInt32(ddlSession.SelectedValue));
            lvExam.DataSource = dsExam;
            lvExam.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ExamConfig.BindExamList -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", string.Empty);
            ddlSession.SelectedIndex = 1;
            objCommon.FillDropDownList(ddlExamName, "ACD_EXAM_NAME", "EXAMNO", "EXAMNAME", "EXAMNAME <> ''", "EXAMNO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ExamConfig.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int econid = int.Parse(btnEdit.CommandArgument);

            ShowDetail(econid);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ExamConfig.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ShowDetail(int econid)
    {
        ExamController objEC = new ExamController();
        SqlDataReader dr = objEC.GetSingleConfigExam(econid);

        //Show Exam Details
        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["econid"] = econid;
                ddlExamName.SelectedValue = dr["EXAMNO"].ToString();
                txtStartDate.Text = dr["FROMDATE"] == null ? "" : Convert.ToDateTime(dr["FROMDATE"].ToString()).ToString("dd/MM/yyyy");
                txtEndDate.Text = dr["TODATE"] == null ? "" : Convert.ToDateTime(dr["TODATE"].ToString()).ToString("dd/MM/yyyy");
            }
        }
        if (dr != null) dr.Close();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ExamController objEC = new ExamController();
            Exam objExam = new Exam();
            objExam.ConfigExamName = ddlExamName.SelectedItem.Text;
            objExam.FromDate = Convert.ToDateTime(txtStartDate.Text);
            objExam.ToDate = Convert.ToDateTime(txtEndDate.Text);
            objExam.SessionNo = Convert.ToInt32(Session["currentsession"].ToString());

            objExam.CollegeCode = Session["colcode"].ToString();

            if (Convert.ToDateTime(txtEndDate.Text) > Convert.ToDateTime(txtStartDate.Text))
            {
                if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                {
                    if (ViewState["econid"] != null)
                    {
                        objExam.EconId = Convert.ToInt32(ViewState["econid"]);
                        CustomStatus cs = (CustomStatus)objEC.UpdateConfig(objExam);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            lblSubmitStatus.Text = "Record Is Updated";
                            ClearControls();
                        }
                        else
                            lblSubmitStatus.Text = "Error...";
                    }
                }
                else
                {

                    CustomStatus cs = (CustomStatus)objEC.AddConfig(objExam);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        lblSubmitStatus.Text = "New Configuration For Exam Is Added";
                        ClearControls();
                    }
                    else
                        lblSubmitStatus.Text = "Error...";
                }
            }

            else
            {
                lblSubmitStatus.Text = "Start Date Is Greater Than End Date,Please Check It";
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ExamConfig.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    private void ClearControls()
    {
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        ddlExamName.SelectedIndex = 0;
        lblSubmitStatus.Text = string.Empty;
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindExamList();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=examconfig.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=examconfig.aspx");
        }
    }

}
