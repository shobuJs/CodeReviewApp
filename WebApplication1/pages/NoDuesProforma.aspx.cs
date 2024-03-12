//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : NO DUES PROFORMA                            
// CREATION DATE : 22 APRIL 2013
// ADDED BY      : YAKIN UTANE                                       
// MODIFIED DATE : 
// MODIFIED BY   : 
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
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
public partial class ACADEMIC_NoDuesProforma : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();
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
                ////Page Authorization
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]));
                ViewState["usertype"] = ua_type;

                if (ViewState["usertype"].ToString() == "2")
                {
                    //CHECK ACTIVITY FOR EXAM REGISTRATION
                    divCourses.Visible = false;
                    pnlSearch.Visible = false;
                }
                else if (ViewState["usertype"].ToString() == "1")
                {
                    pnlSearch.Visible = true;
                }
                else
                {
                    pnlStart.Enabled = false;
                }

                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }

        }
        divMsg.InnerHtml = string.Empty;
    }
    #region

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=NoDuesProforma.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=NoDuesProforma.aspx");
        }
    }
    private void ShowDetails()
    {
        divCourses.Visible = true;
        int idno = 0;
        int sessionno = Convert.ToInt32(Session["currentsession"]);
        StudentController objSC = new StudentController();
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
        }
        try
        {
            if (idno > 0)
            {
                DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT A INNER JOIN ACD_STUD_PHOTO B ON(A.IDNO = B.IDNO)", "A.IDNO", "A.STUDNAME,A.FATHERNAME,B.PHOTO,DBO.FN_DESC('SEMESTER',A.SEMESTERNO)AS SEMESTER,DBO.FN_DESC('DEGREENAME',A.DEGREENO)AS DEGREE,DBO.FN_DESC('BRANCHLNAME',A.BRANCHNO) AS BRANCH", "A.REGNO = '" + txtEnrollno.Text + "' AND REGNO IS NOT NULL", "A.IDNO");
                DataSet dsDueFee = objCommon.FillDropDown("ACD_DCR", "IDNO", "TOTAL_AMT", " IDNO = " + idno + " AND RECIEPT_CODE='DF'", string.Empty);
                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                        lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                        lblIdno.Text = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                        lblDegree.Text = dsStudent.Tables[0].Rows[0]["DEGREE"].ToString();
                        lblBranch.Text = dsStudent.Tables[0].Rows[0]["BRANCH"].ToString();
                        lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTER"].ToString();
                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";
                    }
                    if (dsDueFee.Tables[0].Rows.Count > 0)
                    {
                        lblDueFee.Text = dsDueFee.Tables[0].Rows[0]["TOTAL_AMT"].ToString();

                    }
                    else
                    {
                        lblDueFee.Text = "0.00";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_NoDuesProforma.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion
    protected void btnShow_Click1(object sender, EventArgs e)
    {
        ShowReport("NoDuesFeeProformaForStudents", "rptNoDueFeeProformaStud.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + lblIdno.Text + ",@P_SESSIONNO=" + Convert.ToInt32(Session["currentsession"]);

        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";
    }
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        ShowDetails();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}
