//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : FINAL DETENTION
// CREATION DATE : 21/01/2021                                              
// CREATED BY    : SAFAL GUPTA
//MODIFIED BY    :                                          
// MODIFIED DATE :                                                                      
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
using System.IO;

public partial class ACADEMIC_FinalDetention : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegist objSR = new StudentRegist();
    StudentRegistration objSReg = new StudentRegistration();
    string finaldet = string.Empty;
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

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                string college = Session["college_nos"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));


                    //Student
                    if (Session["usertype"].ToString().Equals("2"))
                    {
                        btnShow.Visible = false;
                    }
                    else if (Session["usertype"].ToString().Equals("1") || Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("4"))
                    {
                        btnShow.Visible = true;
                    }
               // objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "ACTIVE=1", "COLLEGE_NAME");
             
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER SM INNER JOIN ACD_STUDENT_RESULT SR ON(SM.SESSIONNO=SR.SESSIONNO)", "DISTINCT SM.SESSIONNO", "SM.SESSION_NAME", "SM.SESSIONNO > 0", "SM.SESSIONNO DESC");
                objCommon.FillDropDownList(ddltype, "ACD_SUSPENSION", "SUSPNO", "SUSPENSION_NAME", "", "SUSPENSION_NAME");
                objCommon.FillDropDownList(ddlsessionfinal, "ACD_SESSION_MASTER SM INNER JOIN ACD_STUDENT_RESULT SR ON(SM.SESSIONNO=SR.SESSIONNO)", "DISTINCT SM.SESSIONNO", "SM.SESSION_NAME", "SM.SESSIONNO > 0", "SM.SESSIONNO DESC");
                //ddlSession.Focus();
            }

            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
        //    hdfTotNoCourses.Value = System.Configuration.ConfigurationManager.AppSettings["totExamCourses"].ToString();
        }
        objCommon.SetLabelData("0");
        objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        else
        {
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
                Response.Redirect("~/notauthorized.aspx?page=FinalDetaintion.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FinalDetaintion.aspx");
        }
    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
        Panel3.Visible = false;
        lvDetend.DataSource = null;
        lvDetend.DataBind();
        lvDetend.Visible = false;
        divCourse.Visible = false;
        ddlCourse.SelectedValue = "0";
        rblSelection.ClearSelection();
        // lblshow.Visible = false;
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        ViewState["schemeno"] = null;
        ViewState["college_id"] = null;
        if (ddlClgname.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_DETENTION_INFO D ON(S.SESSIONNO=D.SESSIONNO AND S.ORGANIZATIONID=D.ORGANIZATIONID)", "DISTINCT S.SESSIONNO", "S.SESSION_PNAME", "S.SESSIONNO > 0 AND ISNULL(PROV_DETAIN,0)=1 AND ISNULL(CANCEL_DETAIN,0)=0 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND S.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "S.SESSIONNO DESC"); Roshan  
                ddlSession.Focus();
            }
        }
        else
        {
            objCommon.DisplayMessage("Please Select Faculty & Curriculum", this.Page);
            ddlClgname.Focus();
        }
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        if (ddlClgname.SelectedValue != "0") 
        {
            objCommon.FillDropDownList(ddlSem, "ACD_SESSION_MASTER SM INNER JOIN ACD_STUDENT_RESULT SR ON(SM.SESSIONNO=SR.SESSIONNO)INNER JOIN ACD_SEMESTER S ON(S.SEMESTERNO=SR.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SR.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"].ToString()) + "AND SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"].ToString()), "S.SEMESTERNAME");
            ddlSem.Focus();
        }
        
    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSem.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlCourse, "ACD_SESSION_MASTER SM INNER JOIN ACD_STUDENT_RESULT SR ON(SM.SESSIONNO=SR.SESSIONNO) INNER JOIN ACD_SEMESTER S ON(S.SEMESTERNO=SR.SEMESTERNO) INNER JOIN ACD_COURSE C ON (C.COURSENO=SR.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE +'-'+C.COURSE_NAME)COURSENAME", "SR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + "AND SR.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"].ToString()) + "AND SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"].ToString()), "C.COURSENO");
            ddlSem.Focus();
        }
        else
        {
        }
        btnSubmit.Visible = false;
        //btnCancel.Visible = false;
        Panel3.Visible = false;
        lvDetend.DataSource = null;
        lvDetend.DataBind();
        //lblshow.Visible = false;
        lvDetend.Visible = false;
    }
    private void Clearall()
    {
        ddlClgname.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        liTab1.Attributes.Add("class", "active");
        ShowDetendInfo();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string final_detend = string.Empty;
        int count = 0;
        string number = "";

        foreach (ListViewDataItem dataitem in lvDetend.Items)
        {
            CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
            if (chk.Checked == true && chk.Enabled == true)
            {
                count++;
            }
        }

        if (count == 0)
        {
            objCommon.DisplayMessage(UpdatePanel1, "Please Check at least one Student for Final Detention", this.Page);
            return;
        }

        try
        {
            objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objSR.DEGREENO = Convert.ToInt32(ViewState["degreeno"]);
            objSR.SCHEMENO = Convert.ToInt32(ViewState["schemeno"]);
            objSR.SEMESTERNO = Convert.ToInt32(ddlSem.SelectedValue);
            objSR.COURSENO = Convert.ToInt32(ddlCourse.SelectedValue);
            objSR.IPADDRESS = Session["ipAddress"].ToString();
            objSR.UA_NO = Convert.ToInt32(Session["userno"]);
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            foreach (ListViewDataItem item in lvDetend.Items)
            {
                CheckBox chk = item.FindControl("chkAccept") as CheckBox;
                DropDownList ddlReason = item.FindControl("ddlReason") as DropDownList;
                TextBox txtnum = item.FindControl("Txtsusnumber") as TextBox;
                //Label lblcourse = item.FindControl("lblCourse") as Label;

                if (chk.Checked && chk.Enabled)
                {
                    objSR.FINAL_DETEND += "1,";
                    objSR.IDNOS += chk.ToolTip + ",";
                    objSR.SUSPENSION_REASON += Convert.ToInt32(ddlReason.SelectedValue) + ",";
                    number += Convert.ToString(txtnum.Text) + ",";
                }
                else
                    objSR.FINAL_DETEND += "0,";
            }

            if (rblSelection.SelectedValue == "2")
            {
                if (objSReg.UpdateFinalDetend(objSR, number) == Convert.ToInt32(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Final Detention Entry Done Successfully. ", this);
                    ShowDetendInfo();
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Server Error", this.Page);
                }
            }
            else
            {
                if (objSReg.UpdateCourseFinalDetend(objSR) == Convert.ToInt32(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Final Detention Entry Done Successfully. ", this);
                    ShowDetendInfo();
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Server Error", this.Page);
                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    //protected void lvDetend_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    CheckBox chkRow = e.Item.FindControl("chkAccept") as CheckBox;
    //    Label prov = e.Item.FindControl("lblProv") as Label;
    //    Label provstatus = e.Item.FindControl("lblProv") as Label;

    //    Label final = e.Item.FindControl("lblFinal") as Label;
    //    if (prov.ToolTip == "1")
    //    {

    //        chkRow.Enabled = false;
    //        chkRow.BackColor = System.Drawing.Color.Green;
    //        chkRow.Checked = false;
    //    }
    //    else
    //    {
    //    }

    //    if (final.ToolTip == "1")
    //    {
    //        final.Text = "YES";
    //        final.Style.Add("color", "Green");
    //    }
    //    else
    //    {

    //        final.Text = "NO";
    //        final.Style.Add("color", "Red");
    //    }
    //    if (provstatus.Text == "YES")
    //    {
    //        provstatus.Text = "YES";
    //        provstatus.Style.Add("color", "Green");
    //    }
    //    else
    //    {
    //        provstatus.Text = "NO";
    //        provstatus.Style.Add("color", "Red");
    //    }

    //}

    private void ShowDetendInfo()
    {
        try
        {
            DataSet ds = null;
            if (rblSelection.SelectedValue != "1" && rblSelection.SelectedValue != "2")
            {
                objCommon.DisplayMessage(UpdatePanel1, "Please Select  Detention Type.", this);
                return;
            }
            if (rblSelection.SelectedValue == "2")
            {
                ds = objSReg.GetProvDetendInfo(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), 2, Convert.ToInt32(ddlClgname.SelectedValue));
            }
            else
            {
                if (ddlCourse.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Please Select Module.", this);
                    return;
                }
                ds = objSReg.GetProvDetendInfo(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), 1, Convert.ToInt32(ddlClgname.SelectedValue));
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    Panel3.Visible = true;
                    lvDetend.DataSource = ds;
                    lvDetend.DataBind();
                    foreach (ListViewDataItem dataitem in lvDetend.Items)
                    {
                        DropDownList ddlReason = dataitem.FindControl("ddlReason") as DropDownList;

                        objCommon.FillDropDownList(ddlReason, "ACD_SUSPENSION", "SUSPNO", "SUSPENSION_NAME", "", "SUSPENSION_NAME");
                    }
                  //  objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvDetend);//Set label - 
                    lvDetend.Visible = true;
                    int i, count = 0;
                    for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["FINAL"].ToString() == "YES")
                        {
                            count++;

                        }
                    }
                    txtAllSubjects.Text = count.ToString();
                    //lblshow.Visible = true;
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                }
                else
                {
                    Panel3.Visible = false;
                    lvDetend.DataSource = null;
                    lvDetend.DataBind();
                    lvDetend.Visible = false;
                    // lblshow.Visible = false;
                    objCommon.DisplayMessage(UpdatePanel1, "Record Not Found.", this);
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Record Not Found.", this);
                Panel3.Visible = false;
                lvDetend.DataSource = null;
                lvDetend.DataBind();
                lvDetend.Visible = false;
            }
            foreach (ListViewDataItem dataitem in lvDetend.Items)
            {
                CheckBox chkAccept = dataitem.FindControl("chkAccept") as CheckBox;
                Label lblFinal = dataitem.FindControl("lblFinal") as Label;
                DropDownList ddlStreams = dataitem.FindControl("ddlReason") as DropDownList;
                Label lblReason = dataitem.FindControl("lblReason") as Label;
                //int Reason = Convert.ToInt32(lblReason.Text);
                if (lblFinal.Text == "YES")
                {
                    ddlStreams.SelectedValue = lblReason.Text;
                    chkAccept.Checked = true;
                }
            

            }
        }
         
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedValue != "0")
        {
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING CSM INNER JOIN ACD_STUDENT_RESULT SR ON(CSM.SCHEMENO=SR.SCHEMENO)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "COL_SCHEME_NAME");
        }
        else
        {
            ddlSession.SelectedValue = "0";
        }
        rblSelection.ClearSelection();
        divCourse.Visible = false;
        ddlCourse.SelectedValue = "0";
        btnSubmit.Visible = false;
        lvDetend.DataSource = null;
        lvDetend.DataBind();
        //lblshow.Visible = false;
        lvDetend.Visible = false;
    }

    protected void rblSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
          lvDetend.DataSource = null;
        lvDetend.DataSource = null;
        lvDetend.DataSource = null;
        lvDetend.DataBind();
        // lblshow.Visible = false;
        lvDetend.Visible = false;
        ddlCourse.SelectedIndex = 0;

        if (rblSelection.SelectedValue == "1")
        {
            divCourse.Visible = true;
            ddlCourse.Focus();
        }
        else
        {
            divCourse.Visible = false;
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
     
        DataSet ds = objSReg.Get_Final_Detain_StudentList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue));

        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView GV = new GridView();
            string ContentType = string.Empty;

            GV.DataSource = ds;
            GV.DataBind();
            string attachment = "attachment; filename=" + "Final Suspension Report" + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GV.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(UpdatePanel1, "Record Not Found.", this);
        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
        Panel3.Visible = false;
        lvDetend.DataSource = null;
        lvDetend.DataBind();
        lvDetend.Visible = false;
    }
    protected void lnkTab1_Click(object sender, EventArgs e)
    {
        try
        {
            liTab1.Attributes.Add("class", "active");
            liTab2.Attributes.Remove("class");
            litTab3.Attributes.Remove("class");
            lvSuspensionData.DataSource = null;
            lvSuspensionData.DataBind();

            tab_1.Visible = true;
            tab_2.Visible = false;
            tab_3.Visible = false;

            txtSearchStudent.Text = "";
            ddlsessionCa.SelectedValue = "0";
            DivDetails.Visible = false;
            lblStudent_id.Text = "";
            lblStudentn.Text = "";
            lblFacultyname.Text = "";
            lblPrograms.Text = "";
            lblCurrentSemester.Text = "";
            
          

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkTab2_Click(object sender, EventArgs e)
    {
        try
        {
            liTab2.Attributes.Add("class", "active");
            liTab1.Attributes.Remove("class");
            litTab3.Attributes.Remove("class");
            Panel3.Visible = false;
            lvDetend.DataSource = null;
            lvDetend.DataBind();
            lvSuspensionData.DataSource = null;
            lvSuspensionData.DataBind();
            tab_2.Visible = true;
            tab_1.Visible = false;
            tab_3.Visible = false;
            cancel();
            ddlSession.SelectedIndex = 0;
            ddlClgname.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            objCommon.FillDropDownList(ddlsessionCa, "ACD_SESSION_MASTER SM INNER JOIN ACD_STUDENT_RESULT SR ON(SM.SESSIONNO=SR.SESSIONNO)", "DISTINCT SM.SESSIONNO", "SM.SESSION_NAME", "SM.SESSIONNO > 0", "SM.SESSIONNO DESC");
           
        }
        catch (Exception ex)
        {

        }
    }
    protected void SearchData()
    {
        int sessionno = Convert.ToInt32(ddlsessionCa.SelectedValue);
        string StudentID = txtSearchStudent.Text;
        DataSet dss = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=S.BRANCHNO) INNER JOIN ACD_SEMESTER SE ON (SE.SEMESTERNO=S.SEMESTERNO) INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID=S.COLLEGE_ID)", "REGNO", "NAME_WITH_INITIAL,(DEGREENAME+' - '+ B.SHORTNAME) AS PROGRAM ,COLLEGE_NAME,SEMESTERNAME", "REGNO='" + StudentID + "'", "");
        if (dss.Tables[0].Rows.Count > 0)
        {
            DivDetails.Visible = true;
            lblStudent_id.Text = dss.Tables[0].Rows[0]["REGNO"].ToString().Trim();
            lblStudentn.Text = dss.Tables[0].Rows[0]["NAME_WITH_INITIAL"].ToString().Trim();
            lblFacultyname.Text = dss.Tables[0].Rows[0]["COLLEGE_NAME"].ToString().Trim();
            lblPrograms.Text = dss.Tables[0].Rows[0]["PROGRAM"].ToString().Trim();
            lblCurrentSemester.Text = dss.Tables[0].Rows[0]["SEMESTERNAME"].ToString().Trim();
        }
        btnCancelSus.Visible = true;
        DataSet ds = objSReg.GetSuspentionDataList(sessionno, StudentID);
        if (ds.Tables[0].Rows.Count > 0)
        {
            btnRemoveSus.Visible = true;
            lvSuspensionData.DataSource = ds;
            lvSuspensionData.DataBind();
        }
        else
        {
            objCommon.DisplayMessage(this, "Modules Not Found !!", this.Page);
        }
    }
    protected void btnSearchStudent_Click(object sender, EventArgs e)
    {
        SearchData();
        liTab2.Attributes.Add("class", "active");
        liTab1.Attributes.Remove("class");
        litTab3.Attributes.Remove("class");
    }
 
    protected void btnRemoveSus_Click(object sender, EventArgs e)
    {
        int Sessionno = Convert.ToInt32(ddlsessionCa.SelectedValue);
        int UaNo = Convert.ToInt32(Session["userno"]);
        CustomStatus cs = 0;
          string idnos=string.Empty;
        foreach (ListViewDataItem dataitem in lvSuspensionData.Items)
        {
            CheckBox Chk = dataitem.FindControl("chkEx") as CheckBox;
            HiddenField hdnIdno = dataitem.FindControl("hdnIdno") as HiddenField;
            Label hdnCourseNo = dataitem.FindControl("lblCourseNo") as Label;
            Label hdnSemesterno = dataitem.FindControl("lbSemesterNo") as Label;
            TextBox txtRemark = dataitem.FindControl("txtDetails") as TextBox; 

            if (Chk.Checked == true)
            {
                idnos +=Convert.ToString(hdnIdno.Value)+',';
                int idno = Convert.ToInt32(hdnIdno.Value);
                int CourseNo = Convert.ToInt32(hdnCourseNo.Text);
                int Semesterno = Convert.ToInt32(hdnSemesterno.Text);
                string Remark = txtRemark.Text;
                 cs = (CustomStatus)objSReg.RemoveSuspensionStudent(Sessionno, idno, CourseNo, Semesterno, Remark, UaNo);
            }

        }
        liTab2.Attributes.Add("class", "active");
        liTab1.Attributes.Remove("class");
        litTab3.Attributes.Remove("class");
        if (idnos == "")
        {
            objCommon.DisplayMessage(this, "Please Check At list One Course !!", this.Page);
            return;
        }
        if (cs == CustomStatus.RecordUpdated)
        {

            SearchData(); 
            objCommon.DisplayMessage(this, "Record Saved Successfully !!", this.Page);

        }
        else if (cs == CustomStatus.FileExists)
        {
            SearchData(); 
            objCommon.DisplayMessage(this, "File Exits", this.Page);
        }
      
    }
    protected void btnCancelSus_Click(object sender, EventArgs e)
    {
        lvSuspensionData.DataSource = null;
        lvSuspensionData.DataBind();
        txtSearchStudent.Text = "";
        ddlsessionCa.SelectedValue = "0";
        DivDetails.Visible = false;
        lblStudent_id.Text = "";
        lblStudentn.Text ="";
        lblFacultyname.Text = "";
        lblPrograms.Text = "";
        lblCurrentSemester.Text = "";
        btnRemoveSus.Visible = false;
        btnCancelSus.Visible = false;
        liTab2.Attributes.Add("class", "active");
        liTab1.Attributes.Remove("class");
        litTab3.Attributes.Remove("class");
    }
    protected void lnkTab3_Click(object sender, EventArgs e)
    {

        try
        {
            litTab3.Attributes.Add("class", "active");
            liTab1.Attributes.Remove("class");
            liTab2.Attributes.Remove("class");
            Panel3.Visible = false;
            lvDetend.DataSource = null;
            lvDetend.DataBind();
            lvSuspensionData.DataSource = null;
            lvSuspensionData.DataBind();
            tab_2.Visible = false;
            tab_1.Visible = false;
            tab_3.Visible = true;
            lvSuspensionData.DataSource = null;
            lvSuspensionData.DataBind();
            txtSearchStudent.Text = "";
            ddlsessionCa.SelectedValue = "0";
            DivDetails.Visible = false;
            lblStudent_id.Text = "";
            lblStudentn.Text = "";
            lblFacultyname.Text = "";
            lblPrograms.Text = "";
            lblCurrentSemester.Text = "";
            objCommon.FillDropDownList(ddlsessionCa, "ACD_SESSION_MASTER SM INNER JOIN ACD_STUDENT_RESULT SR ON(SM.SESSIONNO=SR.SESSIONNO)", "DISTINCT SM.SESSIONNO", "SM.SESSION_NAME", "SM.SESSIONNO > 0", "SM.SESSIONNO DESC");

        }
        catch (Exception ex)
        {

        }
    }
    private void bindsuslist()
    {
        int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + txtstud.Text+"'"));
        DataSet dss = objCommon.FillDropDown("ACD_DETENTION_INFO D INNER JOIN ACD_STUDENT S ON D.IDNO=S.IDNO  INNER JOIN ACD_SUSPENSION SUS ON SUS.SUSPNO=D.SUSPNO INNER JOIN ACD_SESSION_MASTER SM ON SM.SESSIONNO=D.SESSIONNO", "DISTINCT REGNO,SUSPENSION_NAME,SESSION_NAME", "NAME_WITH_INITIAL,D.SESSIONNO,S.SCHEMENO,S.SEMESTERNO,S.IDNO,REMARKS,SUSPENSTION_DATE,TODATE", "S.IDNO=" + idno, "");
        if (dss.Tables[0].Rows.Count > 0)
        {
            Panel2.Visible = true;
            lvsuspention.DataSource = dss;
            lvsuspention.DataBind();
        }
        else
        {
            Panel2.Visible = false;
            lvsuspention.DataSource = null;
            lvsuspention.DataBind();
        }
    }
    protected void SearchDataFinal()
    {
        liTab1.Attributes.Remove("class");
        int sessionno = Convert.ToInt32(ddlsessionfinal.SelectedValue);
        string StudentID = txtstud.Text;
        DataSet dss = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=S.BRANCHNO) INNER JOIN ACD_SEMESTER SE ON (SE.SEMESTERNO=S.SEMESTERNO) INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID=S.COLLEGE_ID)INNER JOIN ACD_STUDENT_RESULT SR ON SR.IDNO=S.IDNO AND SR.DEGREENO=S.DEGREENO AND S.BRANCHNO=SR.BRANCHNO AND SR.SEMESTERNO=S.SEMESTERNO", "DISTINCT SR.SESSIONNO ,S.REGNO,S.SEMESTERNO,S.DEGREENO,S.SCHEMENO,S.IDNO", "NAME_WITH_INITIAL,(DEGREENAME+' - '+ B.SHORTNAME) AS PROGRAM ,COLLEGE_NAME,SEMESTERNAME", "SR.SESSIONNO=" + ddlsessionfinal.SelectedValue+ "AND S.REGNO='" + StudentID + "'", "");
        if (dss.Tables[0].Rows.Count > 0)
        {
            details.Visible = true;
            Div2.Visible = true;
            lblfinastud.Text = dss.Tables[0].Rows[0]["REGNO"].ToString().Trim();
            lblfinalname.Text = dss.Tables[0].Rows[0]["NAME_WITH_INITIAL"].ToString().Trim();
            lblfinalfaculty.Text = dss.Tables[0].Rows[0]["COLLEGE_NAME"].ToString().Trim();
            lblfinalpgm.Text = dss.Tables[0].Rows[0]["PROGRAM"].ToString().Trim();
            lblcurrentsem.Text = dss.Tables[0].Rows[0]["SEMESTERNAME"].ToString().Trim();
            ViewState["SEMESTERNO"] = dss.Tables[0].Rows[0]["SEMESTERNO"].ToString().Trim();
            ViewState["DEGREENO"] = dss.Tables[0].Rows[0]["DEGREENO"].ToString().Trim();
            ViewState["SCHEMENO"] = dss.Tables[0].Rows[0]["SCHEMENO"].ToString().Trim();
            ViewState["IDNO"] = dss.Tables[0].Rows[0]["IDNO"].ToString().Trim();
            bindsuslist();
            btncancelfinal.Visible = true;
            btnsubmitgeneral.Visible = true;
        }
        else
        {
            objCommon.DisplayMessage(this, "Record Not Found !!", this.Page);
        }
        
      
    }
    protected void btnserch_Click(object sender, EventArgs e)
    {
        liTab1.Attributes.Remove("class");
        SearchDataFinal();
    }
    private void cancel()
    {
        liTab1.Attributes.Remove("class");
        txtFromDate.Text = "";
        txtToDate.Text = "";
        ddltype.SelectedIndex = 0;
        txtEventDetail.Text = "";
        ddlsessionfinal.SelectedIndex = 0;
        txtstud.Text = "";
        details.Visible = false;
        Div2.Visible = false;
        Panel2.Visible = false;
        btnsubmitgeneral.Visible=false;
        lvsuspention.DataSource = null;
        lvsuspention.DataBind();
    }

    protected void btncancelfinal_Click(object sender, EventArgs e)
    {
        liTab1.Attributes.Remove("class");
        cancel();
    }

    protected void btnsubmitgeneral_Click(object sender, EventArgs e)
    {
        liTab1.Attributes.Remove("class");
        CustomStatus cs = 0;
        objSR.SESSIONNO = Convert.ToInt32(ddlsessionfinal.SelectedValue);
        objSR.DEGREENO = Convert.ToInt32(ViewState["DEGREENO"]);
        objSR.SCHEMENO = Convert.ToInt32(ViewState["SCHEMENO"]);
        objSR.SEMESTERNO = Convert.ToInt32(ViewState["SEMESTERNO"]);
        objSR.COURSENO = Convert.ToInt32(ddlCourse.SelectedValue);
        objSR.IPADDRESS = Session["ipAddress"].ToString();
        objSR.UA_NO = Convert.ToInt32(Session["userno"]);
        objSR.COLLEGE_CODE = Session["colcode"].ToString();
        objSR.IDNOS = Convert.ToString(ViewState["IDNO"]);
        objSR.SUSPENSION_REASON = Convert.ToString(txtEventDetail.Text);
        cs = (CustomStatus)objSReg.InsertGeneralSuspension(objSR, txtToDate.Text, txtFromDate.Text,Convert.ToInt32(ddltype.SelectedValue));

        if (cs == CustomStatus.RecordSaved)
        {            
            objCommon.DisplayMessage(this, "Record Saved Successfully !!", this.Page);
            cancel();
        }
        else if (cs == CustomStatus.RecordExist)
        {           
            objCommon.DisplayMessage(this, "Record Already Exits", this.Page);
            cancel();
        }
    }
}
