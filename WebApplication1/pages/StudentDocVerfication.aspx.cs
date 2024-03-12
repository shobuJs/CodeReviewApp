using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;


public partial class ACADEMIC_StudentDocVerfication : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

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
        try
        {
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
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
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help

                    //Page Authorization
                    this.CheckPageAuthorization();

                    if (Request.QueryString["pageno"] != null)
                    {
                    }


                    objCommon.FillDropDownList(ddlSession, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                    ddlSession.Items.RemoveAt(0);

                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDocVerfication.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");

        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentDocVerfication.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=StudentDocVerfication.aspx");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            string userno = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + txtAppID.Text.ToString() + "'");






            //New changes on Documents verification form..Documents are verify without having allotmenmt status true for degree B-tech,B-tec+M-tech,B-tech+Mba and other degrees[19-July-2016].

            if (userno != string.Empty)
            {
                #region CODE SHOW OTHER ALL DEGREE DETAILS WITHOUT HAVING ALLOTMENT STATUS TRUE

                ViewState["newuser"] = userno;

                ds = objCommon.FillDropDown("ACD_USER_REGISTRATION UR INNER JOIN ACD_NEWUSER_REGISTRATION NR ON(NR.USERNO=UR.USERNO) LEFT OUTER JOIN ACD_STUDENT_ENTRENCE_DETAILS ED ON(UR.USERNO=ED.USERNO) LEFT OUTER JOIN ACD_QUALEXM Q ON(Q.QUALIFYNO=ED.ENTR_EXAM_NO)INNER JOIN ACD_USER_PROFILE_STATUS PS ON(PS.USERNO=UR.USERNO) INNER JOIN ACD_FEES_LOG F ON(F.USERNO=UR.USERNO)", "UR.USERNO,USERNAME,UR.FIRSTNAME,FATHERNAME,ED.REGNO,ED.VERIFY_MARKS,ED.ENTR_EXAM_NO,Q.QUALIEXMNAME", "NR.DOCUMENT_LIST,NR.DOCUMENT_STATUS", "UR.ADMBATCH=" + ddlSession.SelectedValue + " AND CONFIRM_STATUS=1 AND RECON=1  AND   UR.USERNAME='" + txtAppID.Text + "'", "UR.DEGREENO DESC");

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    fldstudent.Visible = true;

                    lblAppID.Text = ds.Tables[0].Rows[0]["USERNAME"].ToString();
                    lblName.Text = ds.Tables[0].Rows[0]["FIRSTNAME"].ToString();
                    lblFatherName.Text = ds.Tables[0].Rows[0]["FATHERNAME"].ToString();


                    if (ds.Tables[0].Rows[0]["REGNO"].ToString() == string.Empty &&
                        ds.Tables[0].Rows[0]["VERIFY_MARKS"].ToString() == string.Empty &&
                        ds.Tables[0].Rows[0]["QUALIEXMNAME"].ToString() == string.Empty)
                    {
                        TREntranceDetails.Visible = false;


                    }
                    else
                    {
                        TREntranceDetails.Visible = true;


                        lblJEERollNo.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                        lblJtotal.Text = ds.Tables[0].Rows[0]["VERIFY_MARKS"].ToString();
                        lblexamname.Text = ds.Tables[0].Rows[0]["QUALIEXMNAME"].ToString();

                    }
                    DataSet dsdocument = objCommon.FillDropDown("ACD_DOCUMENT_TYPE", "ROW_NUMBER() over(order by DOCUMENTTYPENO) AS SRNO,DOCUMENTNAME", "DOCUMENTTYPENO", "DOCUMENTTYPENO>0", string.Empty);
                    if (dsdocument.Tables[0] != null && dsdocument.Tables[0].Rows.Count > 0)
                    {
                        lvDocumentList.Visible = true;
                        lvDocumentList.DataSource = dsdocument.Tables[0];
                        lvDocumentList.DataBind();
                    }
                    else
                    {
                        lvDocumentList.Visible = false;
                        lvDocumentList.DataSource = null;
                        lvDocumentList.DataBind();
                    }

                    string Doc_list = ds.Tables[0].Rows[0]["DOCUMENT_LIST"].ToString();
                    string[] words = Doc_list.Split('$');
                    foreach (string word in words)
                    {
                        foreach (ListViewDataItem lvItem in lvDocumentList.Items)
                        {
                            CheckBox chkBox = lvItem.FindControl("chkSelect") as CheckBox;

                            if (chkBox.ToolTip.Trim() == word)
                            {
                                chkBox.Enabled = false;
                                chkBox.Checked = true;
                                chkBox.BackColor = System.Drawing.Color.SeaGreen;
                            }
                        }
                    }

                    if (ds.Tables[0].Rows[0]["DOCUMENT_STATUS"].ToString() == "1")
                    {
                        lblNote.Text = "Applicant's Document Verification Done Sucessfully";
                        lblNote.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblNote.Text = "Documents Are Not Verify Yet.";
                        lblNote.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    objCommon.DisplayMessage("Pre-requisite information not done for application ID  " + txtAppID.Text + "", this.Page);

                }
                #endregion
            }
            else
            {
                objCommon.DisplayMessage("Please Enter Correct Application ID.", Page);
                txtAppID.Text = string.Empty;
                txtAppID.Focus();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDocVerfication.btnShow_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int status;
            int COUNTONE = 0;
            int COUNTTWO = 0;
            string str = string.Empty;
            string documentnos = string.Empty;
            foreach (ListViewDataItem lvItem in lvDocumentList.Items)
            {
                CheckBox chkBox = lvItem.FindControl("chkSelect") as CheckBox;

                if (chkBox.Checked == true)
                {
                    documentnos += chkBox.ToolTip + "$";
                }

                if (chkBox.Enabled == false)
                {
                    COUNTTWO++;
                }
            }

            if (lvDocumentList.Items.Count == COUNTTWO)
            {
                objCommon.DisplayMessage("All documents are already verified for Applicant ID:  " + txtAppID.Text + "", this.Page);
            }

            else
            {
                status = objSC.InsertJoiningDetails(1, Convert.ToInt32(ViewState["newuser"]), Convert.ToInt32(ddlSession.SelectedValue), documentnos);
                if (status == 1)
                {
                    objCommon.DisplayMessage("Applicants Document Verification done successfully", this.Page);
                    btnShow_Click(sender, e);
                }
                else
                {
                    objCommon.DisplayMessage("Error in submitting the data", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDocVerfication.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnPdfreport_Click(object sender, EventArgs e)
    {
        this.ShowReport("Document_Verified_Applicant", "rptDocverifiedapplicant.rpt", 1);
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void ShowReport(string reportTitle, string rptFileName, int report_type)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (report_type == 1)
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCH=" + Convert.ToInt32(ddlSession.SelectedValue);
            else
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNO=" + Convert.ToInt32(ViewState["newuser"]) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlSession.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDocVerfication.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSingleReport_Click(object sender, EventArgs e)
    {
        this.ShowReport("Document_Verified_Report", "rptStudentdocReport.rpt", 2);
    }
}

