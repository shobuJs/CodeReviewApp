using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Activities.Expressions;
using System.Data;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_CourseOffered : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objCourse = new CourseController();
    Course objc = new Course();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }


                    FilldropDown();
                    string host = Dns.GetHostName();
                    IPHostEntry ip = Dns.GetHostEntry(host);
                    string IPADDRESS = string.Empty;
                    IPADDRESS = ip.AddressList[0].ToString();
                    ViewState["ipAddress"] = IPADDRESS;


                }
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header

                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                if (lblDYDegree.Text != string.Empty)
                {
                    lblOtherDegree.Text = "Other " + lblDYDegree.Text;
                }
                //lblOtherDegree
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACDEMIC_COLLEGE_MASTER.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CourseOffered.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CourseOffered.aspx");
        }
    }
    private void FilldropDown()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "", "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlMismatchSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "", "SESSIONNO DESC");
        //objCommon.FillListBox(ddlSemester, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "", "SEMESTERNO ASC");      
        objCommon.FillDropDownList(ddlMisMatchCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and isnull(ACTIVE,0)=1", "COLLEGE_ID");
        objCommon.FillDropDownList(ddlSessionOld, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "", "SESSIONNO DESC");

        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH C ON D.DEGREENO=C.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "ISNULL(C.ACTIVE,0)=1 AND COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENAME ASC");
        //to bind other degree dd 
        objCommon.FillListBox(ddlOtherDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH C ON D.DEGREENO=C.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "ISNULL(C.ACTIVE,0)=1", "D.DEGREENAME ASC");
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            CourseController objCC = new CourseController();
            lvViewOfferModule.DataSource = null;
            lvViewOfferModule.DataBind();
            lvViewOfferModule.Visible = false;
            int SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
            int collegeid = Convert.ToInt32(ddlCollege.SelectedValue);
            int ua_no = Convert.ToInt32(Session["userno"]);
            CustomStatus cs = 0;
            string offcourse = string.Empty;
            string sem = string.Empty;
            string elect = string.Empty;
            string intern = string.Empty;
            string externn = string.Empty;
            string capcity = string.Empty;
            string group = string.Empty;
            string TOTAL = string.Empty;
            string capacity = string.Empty;
            string ca = string.Empty;
            string final = string.Empty;
            string overall = string.Empty;
            string modulelic = string.Empty;
            string CCode = string.Empty;
            //decimal credits = 0.0m;
            string credits = string.Empty;
            string Special = string.Empty;
            int SapecialC = 0;
            string Regular = string.Empty;
            string schemeNo = string.Empty;
            int RegularNo = 0;
            int offeredChkm = 0;
            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {
                CheckBox chkBox = dataitem.FindControl("chkoffered") as CheckBox;
                CheckBox chkSpecial = dataitem.FindControl("chkSpecial") as CheckBox;
                if (chkBox.Checked == true || chkSpecial.Checked == true)
                {
                    offeredChkm += 1;
                    //if (chkBox.Checked == false)
                    //    continue;

                    //string offcourse = string.Empty;
                    //string sem = string.Empty;
                    //string elect = string.Empty;
                    //string intern = string.Empty;
                    //string externn = string.Empty;
                    //string capcity = string.Empty;
                    //string group = string.Empty;
                    //string TOTAL = string.Empty;
                    //string capacity = string.Empty;
                    //string ca = string.Empty;
                    //string final = string.Empty;
                    //string overall = string.Empty;
                    //string modulelic = string.Empty;
                    //decimal credits = 0.0m;


                    ListBox ddlsem = dataitem.FindControl("ddlsem") as ListBox;
                    ListBox ddlscheme = dataitem.FindControl("lstbScheme") as ListBox;
                    DropDownList ddlgroup = dataitem.FindControl("ddlcoregroup") as DropDownList;
                    DropDownList ddlelect = dataitem.FindControl("ddlcore") as DropDownList;
                    TextBox txtinternn = dataitem.FindControl("txtIntern") as TextBox;
                    TextBox txtextern = dataitem.FindControl("txtExtern") as TextBox;
                    TextBox txtcapacity = dataitem.FindControl("txtcapacity") as TextBox;
                    DropDownList ddlmodulelic = dataitem.FindControl("ddlmodulelic") as DropDownList;
                    TextBox txtca = dataitem.FindControl("txtca") as TextBox;
                    TextBox txtfinal = dataitem.FindControl("txtfinal") as TextBox;
                    TextBox txtoverall = dataitem.FindControl("txtoverall") as TextBox;
                    TextBox txtcredits = dataitem.FindControl("txtcredits") as TextBox;
                    Label lblCCode = dataitem.FindControl("lblCCode") as Label;
                    //if (chkBox.Checked == true)
                    //{
                    //       if (txtinternn.Text == "" && txtinternn.Text == "")
                    //if (txtinternn.Text == "" || txtextern.Text == "")
                    //{
                    //    objCommon.DisplayMessage(this.updCourseOffered, "Please enter details", this.Page);
                    //    goto noresult;
                    //}
                    string semno = "";

                    foreach (ListItem item in ddlsem.Items)
                    {
                        if (item.Selected == true)
                        {
                            semno += item.Value + ',';
                        }

                    }

                    if (!string.IsNullOrEmpty(semno))
                    {
                        semno = semno.Substring(0, semno.Length - 1);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updCourseOffered, "Please Select at least one semester!", this.Page);
                        break;
                        //semno = "0";
                    }
                    if (chkSpecial.Checked == true)
                    {
                        SapecialC = 1;
                    }
                    else
                    {
                        SapecialC = 0;
                    }
                    if (chkBox.Checked == true)
                    {
                        RegularNo = 1;
                    }
                    else
                    {
                        RegularNo = 0;
                    }
                    offcourse += chkBox.ToolTip + '$';
                    sem += semno + '$';

                    if (ddlelect.SelectedIndex > 0)
                    {
                        elect += ddlelect.SelectedValue + '$';
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updCourseOffered, "Please Select Core/Elective", this.Page);
                        goto noresult;
                    }
                    Regular += RegularNo.ToString() + '$';
                    Special += SapecialC.ToString() + '$';
                    group += ddlgroup.SelectedValue + '$';
                    intern += txtinternn.Text + '$';
                    externn += txtextern.Text + '$';
                    //externn += txtextern.Text + '$';
                    //externn += txtextern.Text + '$';
                    capacity += txtcapacity.Text + '$';
                    ca += txtca.Text + '$';
                    final += txtfinal.Text + '$';
                    overall += txtoverall.Text + '$';
                    modulelic += Convert.ToString(ddlmodulelic.SelectedValue) + '$';
                    credits += Convert.ToString(txtcredits.Text) + '$';
                    TOTAL += 0 + '$';
                    CCode += lblCCode.Text + '$';

                    string strSchemeno = "";
                    foreach (ListItem item in ddlscheme.Items)
                    {
                        if (item.Selected == true)
                        {
                            strSchemeno += item.Value + ',';
                        }

                    }
                    if (!string.IsNullOrEmpty(strSchemeno))
                    {
                        strSchemeno = strSchemeno.Substring(0, strSchemeno.Length - 1);
                    }
                    else
                    {
                        strSchemeno = "0";
                    }
                    schemeNo += strSchemeno + '$';
                }
                // cs = (CustomStatus)objCC.InsertOfferedCourse(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), capacity, credits, sem, elect, group, Convert.ToInt32(ddlCollege.SelectedValue), offcourse, intern, externn, TOTAL, ua_no, ViewState["ipAddress"].ToString(), ca, final, overall, modulelic);
                // }

            }
            if (offeredChkm == 0)
            {
                objCommon.DisplayMessage(this.updCourseOffered,"Please offer at least one subject.", this.Page);
                return;
            }
            Regular = Regular.TrimEnd('$');
            Special = Special.TrimEnd('$');
            offcourse = offcourse.TrimEnd('$');
            sem = sem.TrimEnd('$');
            elect = elect.TrimEnd('$');
            group = group.TrimEnd('$');
            intern = intern.TrimEnd('$');
            externn = externn.TrimEnd('$');
            ca = ca.TrimEnd('$');
            capacity = capacity.TrimEnd('$');
            final = final.TrimEnd('$');
            overall = overall.TrimEnd('$');
            modulelic = modulelic.TrimEnd('$');
            credits = credits.TrimEnd('$');
            TOTAL = TOTAL.TrimEnd('$');
            CCode = CCode.TrimEnd('$');
            schemeNo = schemeNo.TrimEnd('$');
            if (string.IsNullOrEmpty(sem.ToString()))
            {
                objCommon.DisplayMessage(this.updCourseOffered, "Please select at least one semester to offer.", this.Page);
                lvCourse.Focus();
                return;
            }
            cs = (CustomStatus)objCC.InsertOfferedCourse(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), capacity, credits, sem, elect, group, Convert.ToInt32(ddlCollege.SelectedValue), offcourse, intern, externn, TOTAL, ua_no, ViewState["ipAddress"].ToString(), ca, final, overall, modulelic, CCode, Special, Regular, schemeNo);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                BindListview();

                objCommon.DisplayMessage(this.updCourseOffered, "Subject offered saved successfully.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updCourseOffered, "Error in Saving", this.Page);
            }

        noresult:
            { }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Offered_Course.btnAd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    private void clear()
    {
        ddlSession.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvCourse.Visible = false;
        Response.Redirect(Request.Url.ToString());
    }

    private void BindListview()
    {
        DataSet dsfaculty = null;
        DataSet dsCE = null;
        DataSet dsSem = null;
        DataSet dsUser = null;
        DataSet dsElectG = null;
        DataSet dsScheme = null;
        try
        {
            string SEMNO = string.Empty;
            string crossScheme = string.Empty;
            int schemeno = int.Parse(ddlScheme.SelectedValue);
            int sessionno = int.Parse(ddlSession.SelectedValue);
            int college_id = int.Parse(ddlCollege.SelectedValue);
            int degree = int.Parse(ddlDegree.SelectedValue);
            //int dept = int.Parse(ddlDept.SelectedValue);
            int dept = 0;
            //dsfaculty = objCourse.GetCourseOfferedCreation(schemeno, sessionno, degree, college_id, dept);

            string interdepts = "0";

            for (int i = 0; i < lstbxDept.Items.Count; i++)
            {
                if (lstbxDept.Items[i].Selected == true)
                {
                    interdepts += lstbxDept.Items[i].Value + "$";
                }
            }

            string Semesternos = "";

            //for (int i = 0; i < ddlSemester.Items.Count; i++)
            //{
            //    if (ddlSemester.Items[i].Selected == true)
            //    {
            Semesternos += ddlSemester.SelectedValue + "$";
            //    }
            //}
            //if (coexaminers == "0")
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Select Inter Department!", this.Page);
            //    return;
            //}

            //if (Semesternos == string.Empty)
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Select Semester", this.Page);
            //    return;
            //}
            string otherDegreeNos = "0";
            for (int i = 0; i < ddlOtherDegree.Items.Count; i++)
            {
                if (ddlOtherDegree.Items[i].Selected == true)
                {
                    otherDegreeNos += ddlOtherDegree.Items[i].Value + ",";
                }
            }
            if (otherDegreeNos.Length > 1)
            {
                otherDegreeNos = otherDegreeNos.Substring(0, otherDegreeNos.Length - 1);
            }

            interdepts = interdepts.TrimEnd('$');
            Semesternos = Semesternos.TrimEnd('$');
            string SP_Name2 = "PKG_GET_COURSE_OFFERED_COURSE";
            string SP_Parameters2 = "@P_SESSIONNO,@P_SCHEMENO,@P_DEGREENO,@P_COLLEGE_ID,@P_DEPT,@P_SEMESTERNO";
            string Call_Values2 = "" + Convert.ToInt32(ddlSession.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlScheme.SelectedValue.ToString()) + ","
                + Convert.ToInt32(ddlDegree.SelectedValue.ToString()) + ","
                + Convert.ToInt32(ddlCollege.SelectedValue.ToString()) + ","
                + Convert.ToString(interdepts.ToString()) + "," + Convert.ToString(Semesternos) + "";
            dsfaculty = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);

            if (dsfaculty != null && dsfaculty.Tables.Count > 0 && dsfaculty.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = dsfaculty;
                lvCourse.DataBind();
                lvCourse.Visible = true;
                btnSave.Visible = true;
                btnCancel.Visible = true;
                btnreport.Visible = true;
                btnDeleteModule.Visible = false;
                btnUpdate.Visible = false;
                DivUpdateRemo.Visible = false;
                objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvCourse);//Set label 

            }
            else
            {
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                lvCourse.Visible = false;
                lvCourse.Visible = false;
                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnreport.Visible = false;
                btnUpdate.Visible = false;
                DivUpdateRemo.Visible = false;
                objCommon.DisplayMessage(this.updCourseOffered, "Record Not Found!", this.Page);
            }
            int Scheme_type = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "SCHEMETYPE", "SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue)));
            dsCE = objCommon.FillDropDown("ACD_CORE_ELECTIVE", "GROUPNO", "ELECTIVENAME", "GROUPNO>0", "GROUPNO");
            dsSem = objCommon.FillDropDown("ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND SCHEMETYPENO=" + Scheme_type, "SEMESTERNO");
            dsUser = objCommon.FillDropDown("USER_ACC", "UA_NO", "UA_FULLNAME", "ISNULL(UA_STATUS,0)=0 AND ISNULL(LIC,0)=1", "UA_FULLNAME");
            dsElectG = objCommon.FillDropDown("ACD_ELECTGROUP", "GROUPNO", "GROUPNAME", "GROUPNO > 0", "GROUPNO");

            dsScheme = objCommon.FillDropDown("ACD_SCHEME S INNER JOIN ACD_DEGREE D ON S.DEGREENO=D.DEGREENO", "DISTINCT S.SCHEMENO", "S.SCHEMENAME", "S.SCHEMENO>0 AND (S.DEGREENO IN (" + otherDegreeNos + ") OR S.DEGREENO=0) AND S.SCHEMENO<>" + ddlScheme.SelectedValue + "", "S.SCHEMENAME ASC");

            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {
                DropDownList ddlcore = dataitem.FindControl("ddlcore") as DropDownList;
                ListBox ddlsem = dataitem.FindControl("ddlsem") as ListBox;
                DropDownList ddlcoregroup = dataitem.FindControl("ddlcoregroup") as DropDownList;
                DropDownList ddlmodulelic = dataitem.FindControl("ddlmodulelic") as DropDownList;
                CheckBox ChkOffer = dataitem.FindControl("chkoffered") as CheckBox;
                CheckBox chkSpecial = dataitem.FindControl("chkSpecial") as CheckBox;
                TextBox txtintern = dataitem.FindControl("txtIntern") as TextBox;
                TextBox txtextern = dataitem.FindControl("txtExtern") as TextBox;
                TextBox txtca = dataitem.FindControl("txtca") as TextBox;
                TextBox txtfinal = dataitem.FindControl("txtfinal") as TextBox;
                TextBox txtoverall = dataitem.FindControl("txtoverall") as TextBox;
                Label Regular = dataitem.FindControl("lbllic") as Label;
                ListBox ddlscheme = dataitem.FindControl("lstbScheme") as ListBox;
                if (txtca.Text == "" || txtca.Text == "0")
                {
                    txtca.Text = "0";
                }

                if (txtfinal.Text == "" || txtfinal.Text == "0")
                {
                    txtfinal.Text = "0";

                }
                if (txtoverall.Text == "")
                {
                    txtoverall.Text = "45";
                }
                chkSpecial.Checked = chkSpecial.ToolTip.Equals("1") ? true : false;


                Label lblSem = dataitem.FindControl("LblSemNo") as Label;
                Label LBLCORE = dataitem.FindControl("lblcore") as Label;
                Label LBLELECTIVE = dataitem.FindControl("lblelective") as Label;
                Label lbllic = dataitem.FindControl("lbllic") as Label;
                BindDropDown(ddlcore, dsCE);
                BindListBox(ddlsem, dsSem);
                BindListBox(ddlscheme, dsScheme);
                BindDropDown(ddlmodulelic, dsUser);

                BindDropDown(ddlcoregroup, dsElectG);

                //objCommon.FillDropDownList(ddlcore, "ACD_CORE_ELECTIVE", "GROUPNO", "ELECTIVENAME", "GROUPNO>0", "GROUPNO");
                //objCommon.FillListBox(ddlsem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                //objCommon.FillDropDownList(ddlmodulelic, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE=3", "UA_FULLNAME");
                //// Commented by Nikhil L. on 23/11/2021 as per requirement by Roshaan P. sir.               
                //objCommon.FillDropDownList(ddlcoregroup, "ACD_ELECTGROUP", "GROUPNO", "GROUPNAME", "GROUPNO > 0", "GROUPNO"); // Added by Nikhil L. on 23/11/2021 to get data from different table.                
                ddlcore.SelectedValue = LBLCORE.Text;
                ddlcoregroup.SelectedValue = LBLELECTIVE.Text;
                (ddlmodulelic.SelectedValue) = lbllic.Text;
                if (lbllic.Text != "")
                {
                    ddlmodulelic.Enabled = false;
                }
                if (LBLCORE.Text == "2")
                {
                    ddlcoregroup.Enabled = true;
                }
                else
                {
                    ddlcoregroup.Enabled = false;
                }
                ChkOffer.Checked = Regular.ToolTip.Equals("1") ? true : false;
                ddlsem.SelectedValue = lblSem.Text;
            }
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
            string courseNo = "";
            foreach (ListViewDataItem lvitem in lvCourse.Items)
            {
                CheckBox chkBox = lvitem.FindControl("chkoffered") as CheckBox;
                ListBox ddlsem = lvitem.FindControl("ddlsem") as ListBox;
                ListBox ddlscheme = lvitem.FindControl("lstbScheme") as ListBox;
                if (dsfaculty.Tables[0].Rows.Count > 0 && dsfaculty.Tables[0] != null)
                {
                    for (int k = 0; k < dsfaculty.Tables[0].Rows.Count; k++)
                    {
                        courseNo = dsfaculty.Tables[0].Rows[k]["COURSENO"].ToString();
                        SEMNO = dsfaculty.Tables[0].Rows[k]["SEMESTERNO"].ToString();
                        crossScheme = dsfaculty.Tables[0].Rows[k]["CROSS_SCHEMENO"].ToString();
                        string[] SEM = SEMNO.Split(',');
                        string[] cScheme = crossScheme.Split(',');
                        foreach (ListItem item in ddlsem.Items)
                        {
                            if (Convert.ToString(chkBox.ToolTip) == courseNo.ToString())
                            {
                                for (int j = 0; j < SEM.Length; j++)
                                {
                                    if (SEM[j].ToString() == item.Value)
                                    {
                                        item.Selected = true;
                                    }
                                }
                            }
                        }
                        foreach (ListItem item in ddlscheme.Items)
                        {
                            if (Convert.ToString(chkBox.ToolTip) == courseNo.ToString())
                            {
                                for (int cs = 0; cs < cScheme.Length; cs++)
                                {
                                    if (cScheme[cs].ToString() == item.Value)
                                    { item.Selected = true; }
                                }
                            }
                        }
                    }
                }
            }

            #region Dispose Dataset Object

            dsfaculty = null;
            dsCE = null;
            dsSem = null;
            dsUser = null;
            dsElectG = null;
            dsScheme = null;

            #endregion Dispose Dataset Object
        }

        catch (Exception ex)
        {
            #region Dispose Dataset Object

            dsfaculty = null;
            dsCE = null;
            dsSem = null;
            dsUser = null;
            dsElectG = null;
            dsScheme = null;
            #endregion Dispose Dataset Object

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Offered_Course.BindListView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {

            string otherDegree = "0";

            for (int i = 0; i < ddlOtherDegree.Items.Count; i++)
            {
                if (ddlOtherDegree.Items[i].Selected == true)
                {
                    otherDegree += ddlOtherDegree.Items[i].Value + "$";
                }
            }

            //if (otherDegree == "0")
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Select Other Degree!", this.Page);
            //    lvViewOfferModule.DataSource = null;
            //    lvViewOfferModule.DataBind();
            //    lvCourse.DataSource = null;
            //    lvCourse.DataBind();
            //    return;
            //}

            //coexaminers = coexaminers.TrimEnd('$');
            lvViewOfferModule.DataSource = null;
            lvViewOfferModule.DataBind();
            lvViewOfferModule.Visible = false;
            BindListview();



        }
        catch { }
    }


    protected void ddlcore_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        string Value = ddl.ToolTip.ToString();
        foreach (ListViewDataItem dataitem in lvCourse.Items)
        {
            DropDownList ddlcore = dataitem.FindControl("ddlcore") as DropDownList;

            DropDownList ddlcoregroup = dataitem.FindControl("ddlcoregroup") as DropDownList;
            HiddenField hdfValue = (HiddenField)dataitem.FindControl("hdfValue") as HiddenField;
            HiddenField hdfValue1 = (HiddenField)dataitem.FindControl("hdfValue1") as HiddenField;

            if (Value == hdfValue1.Value && ddlcore.SelectedItem.Text == "ELECTIVE")
            {
                ddlcoregroup.Enabled = true;
            }
            else
            {
                ddlcoregroup.Enabled = false;
            }
        }
    }


    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            lvCourse.Visible = false;
            lvViewOfferModule.DataSource = null;
            lvViewOfferModule.DataBind();
            lvViewOfferModule.Visible = false;
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER CM INNER JOIN ACD_SESSION_MAPPING SM ON (CM.COLLEGE_ID=SM.COLLEGE_ID)", "DISTINCT CM.COLLEGE_ID", "COLLEGE_NAME COLLEGE_NAME", "CM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND CM.COLLEGE_ID > 0 AND isnull(SM.STATUS,0)=1 and isnull(CM.ACTIVE,0)=1 AND isnull(SM.SESSIONNO,0)=" + ddlSession.SelectedValue, "CM.COLLEGE_ID");

            ddlCollege.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            lstbxDept.Items.Clear();
            btnSave.Visible = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            btnreport.Visible = false;
            btnUpdate.Visible = false;
            DivUpdateRemo.Visible = false;
        }
        catch { }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH C ON D.DEGREENO=C.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "ISNULL(C.ACTIVE,0)=1 AND COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENAME ASC");
        }

        ddlDegree.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        lstbxDept.Items.Clear();

        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvCourse.Visible = false;
        lvViewOfferModule.DataSource = null;
        lvViewOfferModule.DataBind();
        lvViewOfferModule.Visible = false;

        btnSave.Visible = false;
        btnSave.Visible = false;
        btnSave.Visible = false;
        btnCancel.Visible = false;
        btnreport.Visible = false;
        btnUpdate.Visible = false;
        DivUpdateRemo.Visible = false;
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO) ", "S.SCHEMENO", "S.SCHEMENAME", "S.SCHEMENO>0 AND S.DEGREENO='" + ddlDegree.SelectedValue + "'", "S.SCHEMENAME ASC");
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO) ", "S.SCHEMENO", "S.SCHEMENAME", "S.SCHEMENO>0 AND S.DEGREENO='" + ddlDegree.SelectedValue + "'AND S.COLLEGE_ID='" + ddlCollege.SelectedValue + "'", "S.SCHEMENAME ASC");
        }

        ddlScheme.SelectedIndex = 0;
        lstbxDept.Items.Clear();

        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvCourse.Visible = false;
        lvViewOfferModule.DataSource = null;
        lvViewOfferModule.DataBind();
        lvViewOfferModule.Visible = false;

        btnSave.Visible = false;
        btnSave.Visible = false;
        btnSave.Visible = false;
        btnCancel.Visible = false;
        btnreport.Visible = false;
        btnUpdate.Visible = false;
        DivUpdateRemo.Visible = false;
    }
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    protected void ddlScheme_SelectedIndexChanged1(object sender, EventArgs e)
    {
        //BindListview();
        try
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            lvCourse.Visible = false;
            lvViewOfferModule.DataSource = null;
            lvViewOfferModule.DataBind();
            lvViewOfferModule.Visible = false;

            int Scheme_type = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "SCHEMETYPE", "SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue)));
            //  objCommon.FillListBox(ddlSemester, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SCHEMETYPENO=" + Scheme_type, "SEMESTERNO ASC");

            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO ASC");

            if (ddlScheme.SelectedIndex > 0)
            {
                IITMS.SQLServer.SQLDAL.SQLHelper objsql = new IITMS.SQLServer.SQLDAL.SQLHelper(_nitprm_constr);
                DataSet ds = objsql.ExecuteDataSet("SELECT DEPTNO,DEPTNAME FROM ACD_DEPARTMENT WHERE DEPTNO>0 ORDER BY DEPTNAME ASC");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lstbxDept.Items.Clear();
                    lstbxDept.DataValueField = "DEPTNO";
                    lstbxDept.DataTextField = "DEPTNAME";
                    lstbxDept.DataSource = ds.Tables[0];
                    lstbxDept.DataBind();
                }
                else
                {
                    lstbxDept.DataSource = null;
                    lstbxDept.DataBind();
                }

                //BindListview();
            }

            //lstbxDept.Items.Clear();

            //lvCourse.DataSource = null;
            //lvCourse.DataBind();

            //btnSave.Visible = false;
            btnSave.Visible = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            btnreport.Visible = false;
            btnUpdate.Visible = false;
            DivUpdateRemo.Visible = false;
        }
        catch { }
    }

    //protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH C ON D.DEGREENO=C.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "ISNULL(C.ACTIVE,0)=1 AND COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENAME");
    //}

    protected void lstbxDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvCourse.Visible = false;

        btnSave.Visible = false;
    }


    //protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindListview();
    //}

    private void BindDropDown(DropDownList ddlList, DataSet ds)
    {
        ddlList.Items.Clear();
        ddlList.Items.Add("Please Select");
        ddlList.SelectedItem.Value = "0";

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlList.DataSource = ds;
            ddlList.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlList.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlList.DataBind();
            ddlList.SelectedIndex = 0;
        }
        ds = null;
    }

    private void BindListBox(ListBox lstbox, DataSet ds)
    {
        lstbox.Items.Clear();

        if (ds.Tables[0].Rows.Count > 0)
        {
            lstbox.DataSource = ds;
            lstbox.DataValueField = ds.Tables[0].Columns[0].ToString();
            lstbox.DataTextField = ds.Tables[0].Columns[1].ToString();
            lstbox.DataBind();
        }

        ds = null;
    }




    protected void ddlSessionOld_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            ddlCollegeNew.SelectedIndex = 0;
            ddlDegreeNew.Items.Clear();
            ddlSessionNew.Items.Clear();
            ddlSessionNew.Items.Insert(0, new ListItem("Please Select", "0"));
            lstbxSemester.Items.Clear();
            lstbxBranch.Items.Clear();
            lvCopyOffered.DataSource = null;
            lvCopyOffered.DataBind();


            //objCommon.FillDropDownList(ddlCollegeNew, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "ISNULL(ACTIVE,0)=1", "COLLEGE_NAME");
            objCommon.FillDropDownList(ddlCollegeNew, "ACD_COLLEGE_MASTER CM INNER JOIN ACD_SESSION_MAPPING SM ON (CM.COLLEGE_ID=SM.COLLEGE_ID)", "DISTINCT CM.COLLEGE_ID", "COLLEGE_NAME COLLEGE_NAME", "CM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND CM.COLLEGE_ID > 0  and isnull(SM.STATUS,0)=1 and isnull(CM.ACTIVE,0)=1 AND isnull(SM.SESSIONNO,0)=" + ddlSessionOld.SelectedValue, "CM.COLLEGE_ID");
            //objCommon.FillDropDownList(ddlCollegeNew, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and isnull(ACTIVE,0)=1", "COLLEGE_ID");

            btnCopySession.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseOffered.ddlSessionOld_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlCollegeNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSessionNew.Items.Clear();
            ddlSessionNew.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlDegreeNew.Items.Clear();
            lstbxSemester.Items.Clear();
            lstbxBranch.Items.Clear();
            lvCopyOffered.DataSource = null;
            lvCopyOffered.DataBind();

            objCommon.FillListBox(ddlDegreeNew, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH C ON D.DEGREENO=C.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "ISNULL(C.ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ddlCollegeNew.SelectedValue), "D.DEGREENAME");

            btnCopySession.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseOffered.btnCopyShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDegreeNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSessionNew.Items.Clear();
            ddlSessionNew.Items.Insert(0, new ListItem("Please Select", "0"));
            lstbxSemester.Items.Clear();
            lstbxBranch.Items.Clear();
            lvCopyOffered.DataSource = null;
            lvCopyOffered.DataBind();

            string Programno = "";
            foreach (ListItem items in ddlDegreeNew.Items)
            {
                if (items.Selected == true)
                {
                    Programno += items.Value + ',';

                }
            }
            if (Programno == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Degree", this.Page);
                return;
            }
            Programno = Programno.TrimEnd(',');

            objCommon.FillListBox(lstbxBranch, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_BRANCH B ON B.BRANCHNO=DB.BRANCHNO", "DISTINCT B.BRANCHNO", "B.LONGNAME", "ISNULL(DB.ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ddlCollegeNew.SelectedValue) + "AND DEGREENO IN (" + Programno.ToString() + ")", "B.LONGNAME");
            objCommon.FillDropDownList(ddlSessionNew, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO >'" + Convert.ToInt32(ddlSessionOld.SelectedValue) + "'", "SESSIONNO DESC");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseOffered.ddlSessionOld_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlSessionNew_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            lstbxSemester.Items.Clear();
            lvCopyOffered.DataSource = null;
            lvCopyOffered.DataBind();
            objCommon.FillListBox(lstbxSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SESSION SM ON(SR.SESSIONNO=SM.SESSIONID) INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO=S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "SEMESTERNAME", "SR.SESSIONNO=" + Convert.ToInt32(ddlSessionOld.SelectedValue) + "", "SR.SEMESTERNO");
            Count();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseOffered.ddlSessionNew_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCopySession_Click(object sender, EventArgs e)
    {
        string Coursenos = ""; string semesternos = ""; string Schemenos = ""; string Programno = "";
        int count = 0;

        try
        {
            foreach (ListItem items in ddlDegreeNew.Items)
            {
                if (items.Selected == true)
                {
                    Programno += items.Value + ',';

                }
            }
            if (Programno == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Degree", this.Page);
                return;
            }
            foreach (ListViewDataItem item in lvCopyOffered.Items)
            {
                CheckBox chk = item.FindControl("chkRow") as CheckBox;
                Label lblCourseno = item.FindControl("lblCcode") as Label;
                Label lblSem = item.FindControl("lblSem") as Label;
                Label lblCurriculum = item.FindControl("lblCurriculum") as Label;

                if (chk.Checked == true)
                {
                    count++;
                    Coursenos += lblCourseno.ToolTip.ToString() + ",";
                    semesternos += lblSem.ToolTip.ToString() + ",";
                    Schemenos += lblCurriculum.ToolTip.ToString() + ",";
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(this.updCopyCourseOffered, "Please Select Atleast One Checkbox !!!", this.Page);
                return;
            }
            else
            {
                Coursenos = Coursenos.TrimEnd(',');
                semesternos = semesternos.TrimEnd(',');
                Schemenos = Schemenos.TrimEnd(',');
                Programno = Programno.TrimEnd(',');
            }
            CustomStatus cs = (CustomStatus)objCourse.CopyOfferedCourses(Convert.ToInt32(ddlSessionOld.SelectedValue), Convert.ToInt32(ddlCollegeNew.SelectedValue), Programno, Convert.ToInt32(ddlSessionNew.SelectedValue), Coursenos, semesternos, Convert.ToInt32(Session["userno"]), Schemenos);

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                //   Clear();
                objCommon.DisplayMessage(this.updCopyCourseOffered, "Subject offered coppied successfully.", this.Page);

            }
        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "Academic_Offered_Course.btnCopySession_Click -> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        ddlSessionOld.SelectedIndex = 0;
        ddlSessionNew.SelectedIndex = 0;
        ddlCollegeNew.Items.Clear();
        ddlCollegeNew.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlDegreeNew.Items.Clear();
        ddlSchemeNew.Items.Clear();
        ddlSchemeNew.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlNewProgram.Items.Clear();
        ddlNewProgram.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlNewCurriculum.Items.Clear();
        ddlNewCurriculum.Items.Insert(0, new ListItem("Please Select", "0"));
        lblCount.Text = "0";
        ddlSessionNew.Items.Clear();
        ddlSessionNew.Items.Insert(0, new ListItem("Please Select", "0"));
        lvCopyOffered.DataSource = null;
        lvCopyOffered.DataBind();
        lstbxBranch.Items.Clear();
        lstbxSemester.Items.Clear();

    }
    protected void btnCancelNew_Click(object sender, EventArgs e)
    {
        Clear();
    }

    //protected void ddlDegreeNew_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    objCommon.FillDropDownList(ddlSchemeNew, "ACD_SCHEME S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO) ", "S.SCHEMENO", "S.SCHEMENAME", "S.SCHEMENO>0 AND S.DEGREENO='" + ddlDegreeNew.SelectedValue + "'", "S.SCHEMENAME");
    //    Count();
    //    objCommon.FillDropDownList(ddlNewProgram, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH C ON D.DEGREENO=C.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "ISNULL(C.ACTIVE,0)=1 AND COLLEGE_ID=" + ddlCollegeNew.SelectedValue + "and  C.DEGREENO <>" + ddlDegreeNew.SelectedValue + "", "D.DEGREENAME");
    //    lvCopyOffered.DataSource = null;
    //    lvCopyOffered.DataBind();
    //    btnCopySession.Visible = false;
    //}

    private void Count()
    {
        string Programno = "";
        foreach (ListItem items in ddlDegreeNew.Items)
        {
            if (items.Selected == true)
            {
                Programno += items.Value + ',';

            }
        }
        if (Programno == string.Empty)
        {
            objCommon.DisplayMessage(this.Page, "Please Select Degree", this.Page);
            return;
        }
        Programno = Programno.TrimEnd(',');

        string query = string.Empty;
        query = "SESSIONNO = " + ddlSessionOld.SelectedValue + "";
        query = query + "AND DEGREENO IN (" + Programno + ")";
        //   query = query + "AND DEGREENO = (case when " + Programno + " <> 0 then " + Programno + " else DEGREENO end) ";
        //   query = query + "AND SCHEMENO = (case when " + ddlSchemeNew.SelectedValue + " <> 0 then " + ddlSchemeNew.SelectedValue + " else SCHEMENO end) ";

        DataSet ds = objCommon.FillDropDown("ACD_OFFERED_COURSE", "count(*) Cnt", "", query.ToString(), "");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lblCount.Text = ds.Tables[0].Rows[0]["Cnt"].ToString();
        }

    }


    protected void ddlSchemeNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        // objCommon.FillDropDownList(ddlNewCurriculum, "ACD_SCHEME S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO) ", "S.SCHEMENO", "S.SCHEMENAME", "S.SCHEMENO>0 AND S.DEGREENO=" + ddlDegreeNew.SelectedValue + "AND SCHEMENO <>" + ddlSchemeNew.SelectedValue + "", "S.SCHEMENAME");
        //   Count();
    }
    protected void ddlNewProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlNewCurriculum, "ACD_SCHEME S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO) ", "S.SCHEMENO", "S.SCHEMENAME", "S.SCHEMENO>0 AND S.DEGREENO='" + ddlNewProgram.SelectedValue + "'", "S.SCHEMENAME");
        //  Count();
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        this.ShowOfferList("OfferCourseList Report", "rptCourseListOffered.rpt");
        lvViewOfferModule.DataSource = null;
        lvViewOfferModule.DataBind();
        lvViewOfferModule.Visible = false;
        BindListview();
    }
    private void ShowOfferList(string reportTitle, string rptFileName)
    {
        try
        {
            //string[] program;
            //if (ddlDegree.SelectedValue == "0")
            //{
            //    program = "0,0".Split(',');
            //}
            //else
            //{
            //    program = ddlDegree.SelectedValue.Split(',');
            //}
            //  string url = "https://sims.sliit.lk/Reports/CommonReport.aspx?";
            string url = System.Configuration.ConfigurationManager.AppSettings["ReportUrl"];

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_IDNO=" + chk.ToolTip + ",@P_SEMESTERNO=" + ddlSemesterResult.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + 0;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updCourseOffered, this.updCourseOffered.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_TranscriptReport.btnTranscript_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCopyShow_Click(object sender, EventArgs e)
    {
        try
        {
            string Programno = "";

            foreach (ListItem items in ddlDegreeNew.Items)
            {
                if (items.Selected == true)
                {
                    Programno += items.Value + '$';

                }
            }
            if (Programno == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Degree", this.Page);
                return;
            }
            Programno = Programno.TrimEnd('$');

            string semesterno = "";

            foreach (ListItem items in lstbxSemester.Items)
            {
                if (items.Selected == true)
                {
                    semesterno += items.Value + '$';

                }
            }
            if (semesterno == string.Empty)
            {
                semesterno = "0";
            }
            semesterno = semesterno.TrimEnd('$');

            string branchno = "";
            foreach (ListItem items in lstbxBranch.Items)
            {
                if (items.Selected == true)
                {
                    branchno += items.Value + '$';

                }
            }
            if (branchno == string.Empty)
            {
                branchno = "0";
            }
            branchno = branchno.TrimEnd('$');

            DataSet ds = null;
            string SP_Name2 = "PKG_GET_OFFERED_COURSE_FOR_COPY";
            string SP_Parameters2 = "@P_SESSIONNO,@P_COLLEGE_ID,@P_DEGREENO,@P_NEWSESSIONNO,@P_SEMESTERNO,@P_BRANCHNO";
            string Call_Values2 = "" + Convert.ToInt32(ddlSessionOld.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlCollegeNew.SelectedValue.ToString()) + ","
                + Programno + "," + Convert.ToInt32(ddlSessionNew.SelectedValue.ToString()) + "," + semesterno.ToString() + "," + branchno;



            ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvCopyOffered.DataSource = ds;
                lvCopyOffered.DataBind();
                btnCopySession.Visible = true;
                objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvCopyOffered);//Set label 
            }
            else
            {
                lvCopyOffered.DataSource = null;
                lvCopyOffered.DataBind();
                btnCopySession.Visible = false;
                objCommon.DisplayMessage(this.Page, "Record Not Found !!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseOffered.btnCopyShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnExcelExport_Click(object sender, EventArgs e)
    {
        try
        {
            string Semesterno = ""; string Deptno = "";

            //foreach (ListItem items in ddlSemester.Items)
            //{
            //    if (items.Selected == true)
            //    {
            Semesterno += ddlSemester.SelectedValue + '$';

            //    }
            //}
            if (Semesterno == string.Empty)
            {
                Semesterno = "0";
            }
            Semesterno = Semesterno.TrimEnd('$');

            foreach (ListItem items in lstbxDept.Items)
            {
                if (items.Selected == true)
                {
                    Deptno += items.Value + '$';

                }
            }
            if (Deptno == string.Empty)
            {
                Deptno = "0";
            }
            Deptno = Deptno.TrimEnd('$');

            string SP_Name2 = "PKG_ACD_EXPORT_MODULE_OFFER_DATA";
            //string SP_Parameters2 = "@P_SESSIONNO,@P_COURSENO,@P_UA_NO";
            string SP_Parameters2 = "@P_SESSIONNO,@P_COLLEGE_ID,@P_DEGREENO,@P_CURRICULUMNO,@P_SEMESTERNO,@P_INTER_DEPARTMENTNO";

            string Call_Values2 = "" + Convert.ToInt32(ddlSession.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlCollege.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlDegree.SelectedValue.ToString()) + ","
            + Convert.ToInt32(ddlScheme.SelectedValue.ToString()) + "," + Semesterno.ToString() + "," + Deptno.ToString() + "";

            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            DataGrid dg = new DataGrid();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string attachment = "attachment; filename=" + "ModuleOfferList.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/" + "ms-excel";
                System.IO.StringWriter sw = new System.IO.StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                dg.DataSource = ds.Tables[0];
                dg.DataBind();
                dg.HeaderStyle.Font.Bold = true;
                dg.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Record Not Found !!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnkOfferedCourselist_Click(object sender, EventArgs e)
    {
        try
        {
            ddlUpdate_remove.SelectedValue = "0";
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            lvCourse.Visible = false;
            DataSet dsfaculty = null;
            DataSet dsCE = null;
            DataSet dsSem = null;
            DataSet dsUser = null;
            DataSet dsElectG = null;
            string SEMNO = string.Empty;
            string Semesterno = ""; string Deptno = "";

            //foreach (ListItem items in ddlSemester.Items)
            //{
            //    if (items.Selected == true)
            //    {
            Semesterno += ddlSemester.SelectedValue + '$';

            //    }
            //}
            if (Semesterno != string.Empty)
            {
                Semesterno = Semesterno.TrimEnd('$');
            }


            foreach (ListItem items in lstbxDept.Items)
            {
                if (items.Selected == true)
                {
                    Deptno += items.Value + '$';

                }
            }
            if (Deptno == string.Empty)
            {
                Deptno = Deptno.TrimEnd('$');
            }


            string SP_Name2 = "PKG_GET_COURSE_OFFERED_COURSE_LIST";

            string SP_Parameters2 = "@P_SESSIONNO,@P_SCHEMENO,@P_DEGREENO,@P_COLLEGE_ID,@P_DEPT,@P_SEMESTERNO";
            string Call_Values2 = "" + Convert.ToInt32(ddlSession.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlScheme.SelectedValue.ToString()) + ","
                + Convert.ToInt32(ddlDegree.SelectedValue.ToString()) + ","
                + Convert.ToInt32(ddlCollege.SelectedValue.ToString()) + ","
                + Convert.ToString(Deptno.ToString()) + "," + Convert.ToString(Semesterno) + "";

            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvViewOfferModule.DataSource = ds;
                lvViewOfferModule.DataBind();
                lvViewOfferModule.Visible = true;
                btnSave.Visible = true;
                btnCancel.Visible = true;
                btnreport.Visible = true;
                btnDeleteModule.Visible = true;
                btnUpdate.Visible = false;
                btnSave.Visible = false;
                DivUpdateRemo.Visible = true;
                objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvViewOfferModule);//Set label 
            }
            else
            {
                lvViewOfferModule.DataSource = null;
                lvViewOfferModule.DataBind();
                lvViewOfferModule.Visible = false;
                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnreport.Visible = false;
                btnDeleteModule.Visible = false;
                btnUpdate.Visible = false;
                btnSave.Visible = false;
                DivUpdateRemo.Visible = false;
                // objCommon.DisplayMessage(this.updCourseOffered, "Record Not Found!", this.Page);
            }

            dsCE = objCommon.FillDropDown("ACD_CORE_ELECTIVE", "GROUPNO", "ELECTIVENAME", "GROUPNO>0", "GROUPNO");
            dsSem = objCommon.FillDropDown("ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            dsUser = objCommon.FillDropDown("USER_ACC", "UA_NO", "UA_FULLNAME", "ISNULL(UA_STATUS,0)=0 AND ISNULL(LIC,0)=1", "UA_FULLNAME");
            dsElectG = objCommon.FillDropDown("ACD_ELECTGROUP", "GROUPNO", "GROUPNAME", "GROUPNO > 0", "GROUPNO");
            foreach (ListViewDataItem dataitem in lvViewOfferModule.Items)
            {
                DropDownList ddlcore = dataitem.FindControl("ddlcoreU") as DropDownList;
                ListBox ddlsem = dataitem.FindControl("ddlsemU") as ListBox;
                DropDownList ddlcoregroup = dataitem.FindControl("ddlcoregroupU") as DropDownList;
                DropDownList ddlmodulelic = dataitem.FindControl("ddlmodulelicU") as DropDownList;
                CheckBox ChkOffer = dataitem.FindControl("chkofferedU") as CheckBox;
                CheckBox chkSpecial = dataitem.FindControl("chkSpecialU") as CheckBox;
                TextBox txtintern = dataitem.FindControl("txtInternU") as TextBox;
                TextBox txtextern = dataitem.FindControl("txtExternU") as TextBox;
                TextBox txtca = dataitem.FindControl("txtcaU") as TextBox;
                TextBox txtfinal = dataitem.FindControl("txtfinalU") as TextBox;
                TextBox txtoverall = dataitem.FindControl("txtoverallU") as TextBox;
                Label Regular = dataitem.FindControl("lbllicU") as Label;
                if (txtca.Text == "" || txtca.Text == "0")
                {
                    txtca.Text = "0";
                }

                if (txtfinal.Text == "" || txtfinal.Text == "0")
                {
                    txtfinal.Text = "0";

                }
                if (txtoverall.Text == "")
                {
                    txtoverall.Text = "45";
                }
                chkSpecial.Checked = chkSpecial.ToolTip.Equals("1") ? true : false;


                Label lblSem = dataitem.FindControl("LblSemNoU") as Label;
                Label LBLCORE = dataitem.FindControl("lblcoreU") as Label;
                Label LBLELECTIVE = dataitem.FindControl("lblelectiveU") as Label;
                Label lbllic = dataitem.FindControl("lbllicU") as Label;
                BindDropDown(ddlcore, dsCE);
                BindListBox(ddlsem, dsSem);
                BindDropDown(ddlmodulelic, dsUser);

                BindDropDown(ddlcoregroup, dsElectG);

                //objCommon.FillDropDownList(ddlcore, "ACD_CORE_ELECTIVE", "GROUPNO", "ELECTIVENAME", "GROUPNO>0", "GROUPNO");
                //objCommon.FillListBox(ddlsem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                //objCommon.FillDropDownList(ddlmodulelic, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE=3", "UA_FULLNAME");
                //// Commented by Nikhil L. on 23/11/2021 as per requirement by Roshaan P. sir.               
                //objCommon.FillDropDownList(ddlcoregroup, "ACD_ELECTGROUP", "GROUPNO", "GROUPNAME", "GROUPNO > 0", "GROUPNO"); // Added by Nikhil L. on 23/11/2021 to get data from different table.                
                ddlcore.SelectedValue = LBLCORE.Text;
                ddlcoregroup.SelectedValue = LBLELECTIVE.Text;
                (ddlmodulelic.SelectedValue) = lbllic.Text;
                ChkOffer.Checked = Regular.ToolTip.Equals("1") ? true : false;
                ddlsem.SelectedValue = lblSem.Text;
            }
            string courseNo = "";
            foreach (ListViewDataItem lvitem in lvViewOfferModule.Items)
            {
                CheckBox chkBox = lvitem.FindControl("chkofferedU") as CheckBox;
                ListBox ddlsem = lvitem.FindControl("ddlsemU") as ListBox;
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                    {
                        courseNo = ds.Tables[0].Rows[k]["COURSENO"].ToString();
                        SEMNO = ds.Tables[0].Rows[k]["SEMESTERNO"].ToString();
                        string[] SEM = SEMNO.Split(',');
                        foreach (ListItem item in ddlsem.Items)
                        {
                            if (Convert.ToString(chkBox.ToolTip) == courseNo.ToString())
                            {
                                for (int j = 0; j < SEM.Length; j++)
                                {
                                    if (SEM[j].ToString() == item.Value)
                                    {
                                        item.Selected = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnDeleteModule_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlUpdate_remove.SelectedValue == "0")
            {
                lnkOfferedCourselist_Click(new object(), new EventArgs());
                objCommon.DisplayMessage(this, "Please Select Update/Remove For", this.Page);
                return;
            }
            CourseController objCC = new CourseController();

            int SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
            int collegeid = Convert.ToInt32(ddlCollege.SelectedValue);
            int ua_no = Convert.ToInt32(Session["userno"]);
            string offcourse = string.Empty;
            string Special = string.Empty;
            int SapecialC = 0;
            string Regular = string.Empty;
            int RegularNo = 0;
            int count = 0;
            int CountModule = 0;
            foreach (ListViewDataItem dataitem in lvViewOfferModule.Items)
            {
                CheckBox chkBox = dataitem.FindControl("chkofferedU") as CheckBox;
                CheckBox chkUpdate_Remove = dataitem.FindControl("chkUpdate_Remove") as CheckBox;
                string Courseno = chkUpdate_Remove.ToolTip;
                CheckBox chkSpecial = dataitem.FindControl("chkSpecialU") as CheckBox;
                Label lblCCodeU = dataitem.FindControl("lblCCodeU") as Label;
                if (chkUpdate_Remove.Checked == true)
                {
                    CountModule++;
                    if (chkBox.Checked == false || chkSpecial.Checked == false)
                    {
                        SapecialC = 0;
                        RegularNo = 0;

                        count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(COURSENO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND COURSENO=" + Convert.ToInt32(Courseno.ToString()) + "AND SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue)));
                        if (count != 0)
                        {
                            objCommon.DisplayMessage(this, lblCCodeU.Text + "This subject is already registered and should not be removed.", this.Page);
                            return;
                        }
                        //if (chkSpecial.Checked == true)
                        //{
                        //    SapecialC = 1;
                        //}
                        //else
                        //{
                        //    SapecialC = 0;
                        //}
                        //if (chkBox.Checked == true)
                        //{
                        //    RegularNo = 1;
                        //}
                        //else
                        //{
                        //    RegularNo = 0;
                        //}
                        offcourse += chkBox.ToolTip + '$';
                        Regular += RegularNo.ToString() + '$';
                        Special += SapecialC.ToString() + '$';
                    }
                }
            }
            if (CountModule == 0)
            {
                lnkOfferedCourselist_Click(new object(), new EventArgs());
                objCommon.DisplayMessage(this, "Please select at least one.", this.Page);
                return;
            }
            Regular = Regular.TrimEnd('$');
            Special = Special.TrimEnd('$');
            offcourse = offcourse.TrimEnd('$');

            string SP_Name1 = "PKG_COURSE_REMOVE_OFFERED_COURSE";
            string SP_Parameters1 = "@P_SESSIONNO,@P_SCHEMENO,@P_COLLEGE_ID,@P_UANO,@P_IPADDDRESS,@P_COURSENOS,@P_SPECIAL,@P_REGULAR,@P_UPDATE_REMOVE,@P_OUTPUT";
            string Call_Values1 = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlScheme.SelectedValue) + "," +
            Convert.ToInt32(ddlCollege.SelectedValue) + "," + Convert.ToInt32(Session["userno"]) + "," + ViewState["ipAddress"].ToString() + "," + offcourse + "," + Special + "," + Regular + "," + Convert.ToInt32(ddlUpdate_remove.SelectedValue) + ",0";

            string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);

            if (que_out1 == "1")
            {
                //BindListview();
                lnkOfferedCourselist_Click(new object(), new EventArgs());
                objCommon.DisplayMessage(this.updCourseOffered, "Subject offered removed successfully.", this.Page);
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.updCourseOffered, "Error in Saving", this.Page);
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlUpdate_remove.SelectedValue == "0")
            {
                lnkOfferedCourselist_Click(new object(), new EventArgs());
                objCommon.DisplayMessage(this, "Please Select Update/Remove For", this.Page);
                return;
            }
            int SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
            int collegeid = Convert.ToInt32(ddlCollege.SelectedValue);
            int ua_no = Convert.ToInt32(Session["userno"]);
            CustomStatus cs = 0;
            string offcourse = string.Empty;
            string sem = string.Empty;
            string elect = string.Empty;
            string intern = string.Empty;
            string externn = string.Empty;
            string capcity = string.Empty;
            string group = string.Empty;
            string TOTAL = string.Empty;
            string capacity = string.Empty;
            string ca = string.Empty;
            string final = string.Empty;
            string overall = string.Empty;
            string modulelic = string.Empty;
            string CCode = string.Empty;
            //decimal credits = 0.0m;
            string credits = string.Empty;
            string Special = string.Empty;
            int SapecialC = 0;
            string Regular = string.Empty;
            int RegularNo = 0;
            int Count = 0;
            foreach (ListViewDataItem dataitem in lvViewOfferModule.Items)
            {
                int counts = 0;
                CheckBox chkUpdate_Remove = dataitem.FindControl("chkUpdate_Remove") as CheckBox;
                CheckBox chkoffered = dataitem.FindControl("chkofferedU") as CheckBox;
                if (chkUpdate_Remove.Checked == true)
                {
                    Count++;
                    TextBox txtinternn = dataitem.FindControl("txtInternU") as TextBox;
                    TextBox txtextern = dataitem.FindControl("txtExternU") as TextBox;
                    DropDownList ddlmodulelic = dataitem.FindControl("ddlmodulelicU") as DropDownList;
                    TextBox txtca = dataitem.FindControl("txtcaU") as TextBox;
                    TextBox txtfinal = dataitem.FindControl("txtfinalU") as TextBox;
                    TextBox txtoverall = dataitem.FindControl("txtoverallU") as TextBox;
                    TextBox txtcredits = dataitem.FindControl("txtcreditsU") as TextBox;
                    Label lblCCode = dataitem.FindControl("lblCCodeU") as Label;

                    if (txtinternn.Text == "" || txtextern.Text == "")
                    {
                        objCommon.DisplayMessage(this.updCourseOffered, "Please enter details", this.Page);
                        return;
                    }
                    string Courseno = chkUpdate_Remove.ToolTip;
                    //counts = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(COURSENO)", "SESSIONNO=" + ddlSession.SelectedValue + "AND COURSENO=" + Convert.ToInt32(Courseno.ToString())));
                    //if (counts != 0)
                    //{
                    //    objCommon.DisplayMessage(this, lblCCode.Text + "This Course Code Is Alredy Register,Should  Not be  Removed this Course Code", this.Page);
                    //    return;
                    //}

                    offcourse += chkoffered.ToolTip + '$';

                    intern += txtinternn.Text + '$';
                    externn += txtextern.Text + '$';

                    ca += txtca.Text + '$';
                    final += txtfinal.Text + '$';
                    overall += txtoverall.Text + '$';
                    modulelic += Convert.ToString(ddlmodulelic.SelectedValue) + '$';
                    credits += Convert.ToString(txtcredits.Text) + '$';
                    TOTAL += Convert.ToString(Convert.ToInt32(txtinternn.Text) + Convert.ToInt32(txtextern.Text)) + '$';
                    CCode += lblCCode.Text + '$';
                }

            }

            if (Count == 0)
            {
                lnkOfferedCourselist_Click(new object(), new EventArgs());
                objCommon.DisplayMessage(this.updCourseOffered, "Please Select At List One..", this.Page);
                return;
            }
            offcourse = offcourse.TrimEnd('$');
            intern = intern.TrimEnd('$');
            externn = externn.TrimEnd('$');
            ca = ca.TrimEnd('$');
            final = final.TrimEnd('$');
            overall = overall.TrimEnd('$');
            modulelic = modulelic.TrimEnd('$');
            credits = credits.TrimEnd('$');
            TOTAL = TOTAL.TrimEnd('$');
            CCode = CCode.TrimEnd('$');

            string SP_Name1 = "PKG_COURSE_UPDATE_OFFERED_COURSE";
            string SP_Parameters1 = "@P_SESSIONNO,@P_SCHEMENO,@P_UANO,@P_IPADDDRESS,@P_COURSENOS,@P_CREDITS,@P_CA,@P_FINAL,@P_TOTAL,@P_MINCA,@P_MINFINAL,@P_OVERALL,@P_MODULE_LIC,@P_UPDATE_REMOVE,@P_OUTPUT";
            string Call_Values1 = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlScheme.SelectedValue) + "," +
            Convert.ToInt32(Session["userno"]) + "," + ViewState["ipAddress"].ToString() + "," + offcourse + "," + credits + "," + intern + "," + externn +
            "," + TOTAL + "," + ca + "," + final + "," + overall + "," + modulelic + "," + Convert.ToInt32(ddlUpdate_remove.SelectedValue) + ",0";

            string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);

            if (que_out1 == "1")
            {
                //BindListview();
                lnkOfferedCourselist_Click(new object(), new EventArgs());
                objCommon.DisplayMessage(this.updCourseOffered, "Subject offered Updated successfully.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updCourseOffered, "Error in Saving", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Offered_Course.btnAd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlMisMatchCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMisMatchCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlMisMatchDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH C ON D.DEGREENO=C.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "ISNULL(C.ACTIVE,0)=1 AND COLLEGE_ID=" + ddlMisMatchCollege.SelectedValue, "D.DEGREENAME ASC");
        }
        else
        {
            ddlMisMatchDegree.Items.Clear();
            ddlMisMatchDegree.Items.Add(new ListItem("Please Select", "0"));
        }
        lvMisMatchMoudle.DataSource = null;
        lvMisMatchMoudle.DataBind();
    }
    protected void ddlMisMatchDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMisMatchDegree.SelectedIndex > 0)
        {
            string SP_Name2 = "PKG_COURSE_MISMATCH_OFFERED_COURSE";

            string SP_Parameters2 = "@P_SESSIONNO,@P_DEGREENO";
            string Call_Values2 = "" + Convert.ToInt32(ddlMismatchSession.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlMisMatchDegree.SelectedValue.ToString()) + "";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvMisMatchMoudle.DataSource = ds;
                lvMisMatchMoudle.DataBind();
            }
            else
            {
                lvMisMatchMoudle.DataSource = null;
                lvMisMatchMoudle.DataBind();
            }

        }
        else
        {
            lvMisMatchMoudle.DataSource = null;
            lvMisMatchMoudle.DataBind();
        }
    }
}
