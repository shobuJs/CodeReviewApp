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


public partial class ACADEMIC_CourseWorkDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistrationModel objStudReg = new StudentRegistrationModel();
    PhdController objPhd = new PhdController();

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
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }     

            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void ShowStudentDetails()
    {
        int ua_no = objCommon.LookUp("USER_ACC", "UA_NO", "UA_IDNO=" + Convert.ToInt32(Session["idno"].ToString())) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_NO", "UA_IDNO=" + Convert.ToInt32(Session["idno"].ToString())));

        DataTableReader dtr = null;
        dtr = objPhd.GetStudentDetailsPhd(ua_no);

        if (dtr != null)
        {
            if (dtr.Read())
            {
                try
                {
                    //---------------------------COURSE DETAILS----------------------------------//
                    txtccode1.Text = dtr["CCODE1"].ToString();
                    txtcourse1.Text = dtr["COURSENAME1"].ToString();
                    txtcollege1.Text = dtr["COLLEGENAME1"].ToString();
                    txtAppearYr1.Text = dtr["APPEARANCEYEAR1"].ToString();
                    txtResult1.Text = dtr["RESULT1"].ToString();

                    txtccode2.Text = dtr["CCODE2"].ToString();
                    txtcourse2.Text = dtr["COURSENAME2"].ToString();
                    txtcollege2.Text = dtr["COLLEGENAME2"].ToString();
                    txtAppearYr2.Text = dtr["APPEARANCEYEAR2"].ToString();
                    txtResult2.Text = dtr["RESULT2"].ToString();

                    txtccode3.Text = dtr["CCODE3"].ToString();
                    txtcourse3.Text = dtr["COURSENAME3"].ToString();
                    txtcollege3.Text = dtr["COLLEGENAME3"].ToString();
                    txtAppearYr3.Text = dtr["APPEARANCEYEAR3"].ToString();
                    txtResult3.Text = dtr["RESULT3"].ToString();

                    txtccode4.Text = dtr["CCODE4"].ToString();
                    txtcourse4.Text = dtr["COURSENAME4"].ToString();
                    txtcollege4.Text = dtr["COLLEGENAME4"].ToString();
                    txtAppearYr4.Text = dtr["APPEARANCEYEAR4"].ToString();
                    txtResult4.Text = dtr["RESULT4"].ToString();

                    txtccode5.Text = dtr["CCODE5"].ToString();
                    txtcourse5.Text = dtr["COURSENAME5"].ToString();
                    txtcollege5.Text = dtr["COLLEGENAME5"].ToString();
                    txtAppearYr5.Text = dtr["APPEARANCEYEAR5"].ToString();
                    txtResult5.Text = dtr["RESULT5"].ToString();

                    txtccode6.Text = dtr["CCODE6"].ToString();
                    txtcourse6.Text = dtr["COURSENAME6"].ToString();
                    txtcollege6.Text = dtr["COLLEGENAME6"].ToString();
                    txtAppearYr6.Text = dtr["APPEARANCEYEAR6"].ToString();
                    txtResult6.Text = dtr["RESULT6"].ToString();

                    txtccode7.Text = dtr["CCODE7"].ToString();
                    txtcourse7.Text = dtr["COURSENAME7"].ToString();
                    txtcollege7.Text = dtr["COLLEGENAME7"].ToString();
                    txtAppearYr7.Text = dtr["APPEARANCEYEAR7"].ToString();
                    txtResult7.Text = dtr["RESULT7"].ToString();

                    txtccode8.Text = dtr["CCODE8"].ToString();
                    txtcourse8.Text = dtr["COURSENAME8"].ToString();
                    txtcollege8.Text = dtr["COLLEGENAME8"].ToString();
                    txtAppearYr8.Text = dtr["APPEARANCEYEAR8"].ToString();
                    txtResult8.Text = dtr["RESULT8"].ToString();
                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUCommon.ShowError(Page, "PresentationLayer_StudentRegistrationPhd.ShowStudentDetails-> " + ex.Message + " " + ex.StackTrace);
                    else
                        objUCommon.ShowError(Page, "Server UnAvailable");
                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objStudReg.IDNO = Convert.ToInt32(Session["userno"]);
            if (!txtccode1.Text.Trim().Equals(string.Empty)) objStudReg.Ccode1 = txtccode1.Text.Trim();
            if (!txtcourse1.Text.Trim().Equals(string.Empty)) objStudReg.CourseName1 = txtcourse1.Text.Trim();
            if (!txtcollege1.Text.Trim().Equals(string.Empty)) objStudReg.CollegeName1 = txtcollege1.Text.Trim();
            if (!txtAppearYr1.Text.Trim().Equals(string.Empty)) objStudReg.AppearanceYear1 = txtAppearYr1.Text.Trim();
            if (!txtResult1.Text.Trim().Equals(string.Empty)) objStudReg.Result1 = txtResult1.Text.Trim();

            if (!txtccode2.Text.Trim().Equals(string.Empty)) objStudReg.Ccode2 = txtccode2.Text.Trim();
            if (!txtcourse2.Text.Trim().Equals(string.Empty)) objStudReg.CourseName2 = txtcourse2.Text.Trim();
            if (!txtcollege2.Text.Trim().Equals(string.Empty)) objStudReg.CollegeName2 = txtcollege2.Text.Trim();
            if (!txtAppearYr2.Text.Trim().Equals(string.Empty)) objStudReg.AppearanceYear2 = txtAppearYr2.Text.Trim();
            if (!txtResult2.Text.Trim().Equals(string.Empty)) objStudReg.Result2 = txtResult2.Text.Trim();

            if (!txtccode3.Text.Trim().Equals(string.Empty)) objStudReg.Ccode3 = txtccode3.Text.Trim();
            if (!txtcourse3.Text.Trim().Equals(string.Empty)) objStudReg.CourseName3 = txtcourse3.Text.Trim();
            if (!txtcollege3.Text.Trim().Equals(string.Empty)) objStudReg.CollegeName3 = txtcollege3.Text.Trim();
            if (!txtAppearYr3.Text.Trim().Equals(string.Empty)) objStudReg.AppearanceYear3 = txtAppearYr3.Text.Trim();
            if (!txtResult3.Text.Trim().Equals(string.Empty)) objStudReg.Result3 = txtResult3.Text.Trim();

            if (!txtccode4.Text.Trim().Equals(string.Empty)) objStudReg.Ccode4 = txtccode4.Text.Trim();
            if (!txtcourse4.Text.Trim().Equals(string.Empty)) objStudReg.CourseName4 = txtcourse4.Text.Trim();
            if (!txtcollege4.Text.Trim().Equals(string.Empty)) objStudReg.CollegeName4 = txtcollege4.Text.Trim();
            if (!txtAppearYr4.Text.Trim().Equals(string.Empty)) objStudReg.AppearanceYear4 = txtAppearYr4.Text.Trim();
            if (!txtResult4.Text.Trim().Equals(string.Empty)) objStudReg.Result4 = txtResult4.Text.Trim();

            if (!txtccode5.Text.Trim().Equals(string.Empty)) objStudReg.Ccode5 = txtccode5.Text.Trim();
            if (!txtcourse5.Text.Trim().Equals(string.Empty)) objStudReg.CourseName5 = txtcourse5.Text.Trim();
            if (!txtcollege5.Text.Trim().Equals(string.Empty)) objStudReg.CollegeName5 = txtcollege5.Text.Trim();
            if (!txtAppearYr5.Text.Trim().Equals(string.Empty)) objStudReg.AppearanceYear5 = txtAppearYr5.Text.Trim();
            if (!txtResult5.Text.Trim().Equals(string.Empty)) objStudReg.Result5 = txtResult5.Text.Trim();

            if (!txtccode6.Text.Trim().Equals(string.Empty)) objStudReg.Ccode6 = txtccode6.Text.Trim();
            if (!txtcourse6.Text.Trim().Equals(string.Empty)) objStudReg.CourseName6 = txtcourse6.Text.Trim();
            if (!txtcollege6.Text.Trim().Equals(string.Empty)) objStudReg.CollegeName6 = txtcollege6.Text.Trim();
            if (!txtAppearYr6.Text.Trim().Equals(string.Empty)) objStudReg.AppearanceYear6 = txtAppearYr6.Text.Trim();
            if (!txtResult6.Text.Trim().Equals(string.Empty)) objStudReg.Result6 = txtResult6.Text.Trim();

            if (!txtccode7.Text.Trim().Equals(string.Empty)) objStudReg.Ccode7 = txtccode7.Text.Trim();
            if (!txtcourse7.Text.Trim().Equals(string.Empty)) objStudReg.CourseName7 = txtcourse7.Text.Trim();
            if (!txtcollege7.Text.Trim().Equals(string.Empty)) objStudReg.CollegeName7 = txtcollege7.Text.Trim();
            if (!txtAppearYr7.Text.Trim().Equals(string.Empty)) objStudReg.AppearanceYear7 = txtAppearYr7.Text.Trim();
            if (!txtResult7.Text.Trim().Equals(string.Empty)) objStudReg.Result7 = txtResult7.Text.Trim();

            if (!txtccode8.Text.Trim().Equals(string.Empty)) objStudReg.Ccode8 = txtccode8.Text.Trim();
            if (!txtcourse8.Text.Trim().Equals(string.Empty)) objStudReg.CourseName8 = txtcourse8.Text.Trim();
            if (!txtcollege8.Text.Trim().Equals(string.Empty)) objStudReg.CollegeName8 = txtcollege8.Text.Trim();
            if (!txtAppearYr8.Text.Trim().Equals(string.Empty)) objStudReg.AppearanceYear8 = txtAppearYr8.Text.Trim();
            if (!txtResult8.Text.Trim().Equals(string.Empty)) objStudReg.Result8 = txtResult8.Text.Trim();

            CustomStatus cs = (CustomStatus)objPhd.AddUpdateCourseDetail(objStudReg);
            if (Convert.ToInt32(cs) == 1 || Convert.ToInt32(cs) == 2)
            {
                objCommon.DisplayMessage(this, "Record Updated Successfully!", this);
            }
            else
            {
                objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this, "Something went wrong ..Please try again !!", this);
            return;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        divMsg.InnerHtml = string.Empty;
        divlv.Visible = false;
        divdata.Visible = false;
        btnSubmit.Visible = false;
        btnCancel.Visible = false;

        if (txtStudent.Text.Trim() == "" || txtStudent.Text.Trim() == string.Empty || txtStudent.Text == null)
        {
            objCommon.DisplayMessage(UpdatePanel9,"Please Enter Proper Registration No.", this.Page);
            txtStudent.Focus();
            return;
        }

        try
        {
            string searchText = txtStudent.Text.Trim();
            string errorMsg = string.Empty;

            //student details shown
            ShowStudents(searchText, errorMsg);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AdmissionCancellation.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //show student details
    private void ShowStudents(string searchText, string errorMsg)
    {
        if (txtStudent.Text.Trim() == "" || txtStudent.Text.Trim() == string.Empty || txtStudent.Text == null)
        {
            objCommon.DisplayMessage(UpdatePanel9, "Please Enter Registration No./ Admission No.", this.Page);
            txtStudent.Focus();
            return;
        }
        try
        {
            StudentRegistration objSRegist = new StudentRegistration();
            StudentController objSC = new StudentController();
            string idno = "0";

            idno = objCommon.LookUp("ACD_PHD_COURSE_DETAILS CD INNER JOIN ACD_STUDENT S ON (S.IDNO=CD.IDNO)", "DISTINCT CD.IDNO", "(S.REGNO='" + txtStudent.Text.Trim() + "' OR S.ENROLLNO='" + txtStudent.Text.Trim() + "') AND ISNULL(S.ADMCAN,0)=" + 0) == string.Empty ? "0" : objCommon.LookUp("ACD_PHD_COURSE_DETAILS CD INNER JOIN ACD_STUDENT S ON (S.IDNO=CD.IDNO)", "DISTINCT CD.IDNO", "(S.REGNO='" + txtStudent.Text.Trim() + "' OR S.ENROLLNO='" + txtStudent.Text.Trim() + "') AND ISNULL(S.ADMCAN,0)=" + 0);

            if (idno == string.Empty || idno=="0")
            {
                btnSubmit.Visible = false;
                btnCancel.Visible = false;
                divlv.Visible = false;
                divdata.Visible = false;
                objCommon.DisplayMessage(UpdatePanel9, "No Student Found for Course Work Details Having Registration No./ Admission No. as " + txtStudent.Text + " !", this.Page);
            }
            else
            {
                if (idno != string.Empty)
                {
                    ViewState["idno"] = idno.ToString();
                    DataTableReader dtr = objSRegist.GetStudentDetails(Convert.ToInt32(idno));

                    if (dtr.Read())
                    {
                        divlv.Visible = true;
                        btnSubmit.Visible = true;
                        btnCancel.Visible = true;
                        divdata.Visible = true;
                        Session["idno"] = dtr["idno"].ToString();
                        lblName.Text = dtr["STUDNAME"].ToString();
                        lblRegNo.Text = dtr["REGNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["REGNO"].ToString();
                        lblEnrollno.Text = dtr["ENROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ENROLLNO"].ToString();
                        lblScheme.Text = dtr["SCHEMENAME"].ToString();

                        ShowStudentDetails();
                    }
                    else
                    {
                        btnSubmit.Visible = false;
                        btnCancel.Visible = false;
                        divlv.Visible = false;
                        divdata.Visible = false;
                    }
                    dtr.Close();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BranchChange.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divlv.Visible = false;
        divdata.Visible = false;
        txtStudent.Text = string.Empty;
        btnCancel.Visible = false;
        btnSubmit.Visible = false;
    }

}
