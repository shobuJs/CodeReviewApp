//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : STUDENT INFORMATION                                                     
// CREATION DATE : 27-SEPT-2010                                                          
// CREATED BY    : NIRAJ D. PHALKE                               
// MODIFIED DATE : 30-11-2013                
// ADDED BY      : ASHISH MOTGHARE                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Linq;

public partial class Academic_StudentInfoEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"..\includes\prototype.js"));
        Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"..\includes\scriptaculous.js"));
        Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"..\includes\modalbox.js"));

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
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";
                ViewState["usertype"] = Session["usertype"];

                if (ViewState["usertype"].ToString() == "2")
                {

                    divadmissiondetails.Visible = false;
                    divtabs.Visible = true;
                    pnlId.Visible = false;
                    Server.Transfer("~/academic/PersonalDetails.aspx", false);

                }
                else
                {
                    divadmissiondetails.Visible = true;
                    if (Session["stuinfoidno"] == null || Session["stuinfoenrollno"] == null || Session["stuinfofullname"] == null)
                    {
                        divstudentidno.Visible = false;
                        txtIDNo.Text = string.Empty;
                        txtStudFullname.Text = string.Empty;
                        txtRegNo.Text = string.Empty;

                    }
                    else
                    {
                        divstudentidno.Visible = true;
                        txtIDNo.Text = Session["stuinfoidno"].ToString();
                        txtStudFullname.Text = Session["stuinfofullname"].ToString();
                        txtRegNo.Text = Session["stuinfoenrollno"].ToString();

                    }

                    pnlId.Visible = true;
                }

            }

        }
        else
        {
            if (Page.Request.Params["__EVENTTARGET"] != null)
            {
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                {
                    string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                    bindlist(arg[0], arg[1]);
                }
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                {
                    txtSearch.Text = string.Empty;
                    lvStudent.DataSource = null;

                    lvStudent.DataBind();
                    lblNoRecords.Text = string.Empty;
                }
            }
        }
    }
    private void ShowSearchResults(string searchParams)
    {
        try
        {
            StudentSearch objSearch = new StudentSearch();

            string[] paramCollection = searchParams.Split(',');
            if (paramCollection.Length >= 2)
            {
                for (int i = 0; i < paramCollection.Length; i++)
                {
                    string paramName = paramCollection[i].Substring(0, paramCollection[i].IndexOf('='));
                    string paramValue = paramCollection[i].Substring(paramCollection[i].IndexOf('=') + 1);

                    switch (paramName)
                    {
                        case "Name":
                            objSearch.StudentName = paramValue;
                            break;
                        case "EnrollNo":
                            objSearch.EnrollmentNo = paramValue;
                            break;
                        case "IdNo":
                            objSearch.IdNo = paramValue;
                            break;

                        default:
                            break;
                    }
                }
            }
            DataSet ds = feeController.GetStudents(objSearch);
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
        }
        catch (Exception ex)
        {

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


    protected void ProfileComplition()
    {
        try
        {

        }
        catch (Exception Ex)
        {

        }
    }

    private void bindlist(string category, string searchtext)
    {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetails(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
            lblNoRecords.Text = "Total Records : 0";
    }
    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        else
            url = Request.Url.ToString();

        Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

        Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
        Session["stuinfofullname"] = lnk.Text.Trim();



        Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
        txtIDNo.Text = Session["stuinfoidno"].ToString();
        Response.Redirect("~/academic/PersonalDetails.aspx");

    }

    protected void lnkPersonalDetail_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "2")
        {
            Server.Transfer("~/academic/PersonalDetails.aspx", false);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                Server.Transfer("~/academic/PersonalDetails.aspx", false);
            }
            else
            {
                objCommon.DisplayMessage("Please Search Enrollment No!!", this.Page);

            }
        }
    }
    protected void lnkAddressDetail_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "2")
        {
            Server.Transfer("~/academic/AddressDetails.aspx", false);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                Server.Transfer("~/academic/AddressDetails.aspx", false);
            }
            else
            {
                objCommon.DisplayMessage("Please Search Enrollment No!!", this.Page);
            }
        }

    }
    protected void lnkAdmissionDetail_Click(object sender, EventArgs e)
    {

        if (ViewState["usertype"].ToString() == "2")
        {
            Server.Transfer("~/academic/AdmissionDetails.aspx", false);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                Server.Transfer("~/academic/AdmissionDetails.aspx", false);
            }
            else
            {
                objCommon.DisplayMessage("Please Search Enrollment No!!", this.Page);
            }
        }



    }

    protected void lnkDasaStudentInfo_Click(object sender, EventArgs e)
    {

        if (ViewState["usertype"].ToString() == "2")
        {
            Server.Transfer("~/academic/DASAStudentInformation.aspx", false);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                Server.Transfer("~/academic/DASAStudentInformation.aspx", false);
            }
            else
            {
                objCommon.DisplayMessage("Please Search Enrollment No!!", this.Page);
            }
        }


    }
    protected void lnkQualificationDetail_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "2")
        {
            Server.Transfer("~/academic/QualificationDetails.aspx", false);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                Server.Transfer("~/academic/QualificationDetails.aspx", false);
            }
            else
            {
                objCommon.DisplayMessage("Please Search Enrollment No!!", this.Page);
            }
        }

    }
    protected void lnkotherinfo_Click(object sender, EventArgs e)
    {

        if (ViewState["usertype"].ToString() == "2")
        {
            Server.Transfer("~/academic/OtherInformation.aspx", false);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                Server.Transfer("~/academic/OtherInformation.aspx", false);
            }
            else
            {
                objCommon.DisplayMessage("Please Search Enrollment No!!", this.Page);
            }
        }


    }
    protected void lnkprintapp_Click(object sender, EventArgs e)
    {
        GEC_Student objGecStud = new GEC_Student();
        if (ViewState["usertype"].ToString() == "2")
        {
            objGecStud.RegNo = Session["idno"].ToString();
            string output = objGecStud.RegNo;
            ShowReport("Admission_Form_Report_M.TECH", "Admission_Slip_Confirm_PHD_General.rpt", output);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                objGecStud.RegNo = Session["stuinfoidno"].ToString();
                string output = objGecStud.RegNo;
                ShowReport("Admission_Form_Report_M.TECH", "Admission_Slip_Confirm_PHD_General.rpt", output);
            }
            else
            {
                objCommon.DisplayMessage("Please Search Enrollment No!!", this.Page);
            }
        }
    }

    private void ShowReport(string reportTitle, string rptFileName, string regno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Admission Form Report " + txtRegNo.Text;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt16(txtIDNo.Text.Trim().ToString()) + "";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
