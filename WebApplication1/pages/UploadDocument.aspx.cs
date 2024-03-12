//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : STUDENT FEES RELATED REPORT
// CREATION DATE : 02-JULY-2013
// CREATED BY    : ASHISH DHAKATE
// MODIFIED DATE :
// MODIFIED BY   :
// MODIFIED DESC :
//======================================================================================

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Web.Configuration;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Security;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Web.Configuration;

public partial class ACADEMIC_UploadDocument : System.Web.UI.Page
{
    private Common objCommon = new Common();
    private UAIMS_Common objUCommon = new UAIMS_Common();
    private FeeCollectionController feeCntrl = new FeeCollectionController();

    // Common objCommon = new Common();
    //  NewUser objnu = new NewUser();
    // User objU = new User();
    IITMS.UAIMS.BusinessLayer.BusinessEntities.DocumentAcad objdocument = new DocumentAcad();
    DocumentControllerAcad objdocContr = new DocumentControllerAcad();
    // NewUserController objnuc = new NewUserController();

    //ConnectionString
    private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string username = string.Empty;

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
                //  CheckPageAuthorization();
                ViewState["userno"] = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + Session["username"].ToString() + "'");
                PopulateDropDown();
                bindlist();
               bindphoto();
               

                // Bindbrach();
                //Set the Page Title
                //updDocs.Visible = false;
                Page.Title = Session["coll_name"].ToString();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                lblUndertaking.Visible = false;
                lblDoc.Visible = true;
                docUnder.Visible = false;
               
                // UploadDocument();
            }

            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
            objCommon.SetLabelData("0");//for label
            
        }

    }



    protected void ddlSchClg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO > 0", "DOCUMENTNO");
            string[] branchpref = ddlSchClg.SelectedValue.Split('.');
            UploadDocument();
            int RecCount = Convert.ToInt32(objCommon.LookUp("ACD_USER_BRANCH_PREF", "count(*)", " Degreeno=" + branchpref[0] + "AND COLLEGE_ID=" + branchpref[1] + " AND ISNULL(BRANCHNO,0) =" + branchpref[2] + " AND  ISNULL(APPLIED,0)=1 AND USERNO=" + Session["username"] + ""));
            if (RecCount > 0)
            {
                //ShowMessage("You are already applied for selected Degree !!!");
                ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('You are already applied for selected Degree !!!');", true);
                ddlSchClg.SelectedIndex = 0;
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            DataSet dsinfo = objCommon.FillDropDown("ACD_NEWUSER_REGISTRATION A INNER JOIN ACD_USER_REGISTRATION B ON (A.USERNO=B.USERNO) ", "A.FIRSTNAME", "A.MOTHERNAME ,B.USERNAME, A.MOBILE ,A.EMAILID", "B.USERNAME = '" + Session["username"] + "'", "");
            if (dsinfo.Tables[0].Rows.Count > 0)
            {
                lblEnroll.Text = dsinfo.Tables[0].Rows[0]["USERNAME"].ToString();
                lblName.Text = dsinfo.Tables[0].Rows[0]["FIRSTNAME"].ToString();
                //lblf.Text = dsinfo.Tables[0].Rows[0]["FATHERNAME"].ToString();
                lblm.Text = dsinfo.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                lblmn.Text = dsinfo.Tables[0].Rows[0]["MOBILE"].ToString();
                lble.Text = dsinfo.Tables[0].Rows[0]["EMAILID"].ToString();
                Session["USERNAME"] = dsinfo.Tables[0].Rows[0]["USERNAME"].ToString();

            }

            //  string deg = GetDegree();
            //objCommon.FillDropDownList(ddlSchClg, "ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID", "DISTINCT CM.COLLEGE_ID", "CM.COLLEGE_NAME+'('+SHORT_NAME +'-'+ CODE +')' as COLLEGE_NAME", "CM.COLLEGE_ID > 0 AND UR.USERNO=" + 48 + "", "CM.COLLEGE_ID");
            //ddlSchClg.SelectedIndex = 0;
            int userno=Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + Session["username"].ToString()+"'"));
            //int CheckMerit = Convert.ToInt32(objCommon.LookUp("ACD_ONLINE_USER_UPLOAD", "COUNT(*)", "ISNULL(MERIT_STATUS,0)=0 AND USERNAME='" + Session["username"] + "'"));
            int CheckMerit = Convert.ToInt32(objCommon.LookUp("ACD_USER_BRANCH_PREF", "ISNULL(COUNT(MERITNO),0) MERIT_COUNT", "USERNO=" + userno + " AND (MERITNO IS NOT NULL)"));
            if (CheckMerit > 0)
            {
                //objCommon.FillDropDownList(ddlSchClg, "ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID  LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO  LEFT JOIN ACD_PREFERENCE P ON P.PREFERENCE_ID = UR.BRANCH_PREF INNER JOIN ACD_ONLINE_USER_UPLOAD UP ON (UP.USERNAME=UR.USERNO  AND UP.COLLEGE_ID=UR.COLLEGE_ID AND UP.DEGREENO=UR.DEGREENO AND SGM.SUGNO=ISNULL(UP.SUBGROUPNO,0)) INNER JOIN ACD_FEES_LOG FE ON (FE.USERNO=UR.USERNO AND FE.DEGREENO=UR.DEGREENO AND FE.COLLEGE_ID=UR.COLLEGE_ID AND ISNULL(FE.BRANCHNO,0)= ISNULL(UR.BRANCHNO,0)) ", "DISTINCT convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE '' END  DEGREENAME", " ISNULL(MERIT_STATUS,0)=0 AND ISNULL(FREEZE,0)<>1 AND D.DEGREENO >0 AND UP.USERNAME =" + Session["username"] + "", "");
                //objCommon.FillDropDownList(ddlSchClg, "ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID  LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO  LEFT JOIN ACD_PREFERENCE P ON P.PREFERENCE_ID = UR.BRANCH_PREF INNER JOIN ACD_ONLINE_USER_UPLOAD UP ON (UP.USERNAME=UR.USERNO  AND UP.COLLEGE_ID=UR.COLLEGE_ID AND UP.DEGREENO=UR.DEGREENO AND SGM.SUGNO=ISNULL(UP.SUBGROUPNO,0)) INNER JOIN ACD_FEES_LOG FE ON (FE.USERNO=UR.USERNO AND FE.DEGREENO=UR.DEGREENO AND FE.COLLEGE_ID=UR.COLLEGE_ID ) ", "DISTINCT convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE '' END  DEGREENAME", " ISNULL(MERIT_STATUS,0)=0 AND ISNULL(FREEZE,0)<>1 AND D.DEGREENO >0 AND UP.USERNAME =" + Session["username"] + "", "");

                //objCommon.FillDropDownList(ddlSchClg, "ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID  LEFT JOIN ACD_BRANCH SGM ON UR.BRANCHNO = SGM.BRANCHNO  LEFT JOIN ACD_PREFERENCE P ON P.PREID = UR.BRANCH_PREF INNER JOIN ACD_ONLINE_USER_UPLOAD UP ON (UP.USERNAME=UR.USERNO  AND UP.COLLEGE_ID=UR.COLLEGE_ID AND UP.DEGREENO=UR.DEGREENO AND SGM.branchno=ISNULL(UP.SUBGROUPNO,0)) INNER JOIN ACD_FEES_LOG FE ON (FE.USERNO=UR.USERNO AND FE.DEGREENO=UR.DEGREENO AND FE.COLLEGE_ID=UR.COLLEGE_ID ) ", "DISTINCT convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE LONGNAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE '' END  DEGREENAME", " ISNULL(MERIT_STATUS,0)=0 AND ISNULL(FREEZE,0)<>1 AND D.DEGREENO >0 AND UP.USERNAME ='" + Session["username"] + "'", "");
                objCommon.FillDropDownList(ddlSchClg,"ACD_USER_BRANCH_PREF BP INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON(BP.COLLEGE_ID=DB.COLLEGE_ID AND BP.DEGREENO=DB.DEGREENO) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=BP.DEGREENO)","DISTINCT BP.DEGREENO","DEGREENAME","MERITNO IS NOT NULL and USERNO="+userno+"","BP.DEGREENO");
            }
            //else
            //{
            //    //objCommon.FillDropDownList(ddlSchClg, "ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID  LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO  LEFT JOIN ACD_PREFERENCE P ON P.PREFERENCE_ID = UR.BRANCH_PREF INNER JOIN ACD_FEES_LOG FE ON (FE.USERNO=UR.USERNO AND FE.DEGREENO=UR.DEGREENO AND FE.COLLEGE_ID=UR.COLLEGE_ID AND ISNULL(FE.BRANCHNO,0)= ISNULL(UR.BRANCHNO,0))", "convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE '' END  DEGREENAME", " D.DEGREENO in (1,4,12) AND UR.USERNO =" + Session["username"] + " AND ISNULL(FREEZE,0)<>1 AND UR.COLLEGE_ID NOT IN (SELECT COLLEGE_ID FROM ACD_STUDENT WHERE USERNO=" + Session["username"] + " AND ISNULL(ADMCAN,0)=0)  AND ISNULL(UR.DEGREENO,0) NOT IN (SELECT ISNULL(DEGREENO,0) FROM ACD_STUDENT WHERE USERNO=" + Session["username"] + " AND ISNULL(ADMCAN,0)=0)  AND ISNULL(UR.BRANCHNO,0) NOT IN (SELECT ISNULL(BRANCHNO,0) FROM ACD_STUDENT WHERE USERNO=" + Session["username"] + " AND ISNULL(ADMCAN,0)=0)", ""); ///AND ISNULL(APPLIED,0)=0
            //    objCommon.FillDropDownList(ddlSchClg, "ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID  LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO  LEFT JOIN ACD_PREFERENCE P ON P.PREID = UR.BRANCH_PREF INNER JOIN ACD_FEES_LOG FE ON (FE.USERNO=UR.USERNO AND FE.DEGREENO=UR.DEGREENO AND FE.COLLEGE_ID=UR.COLLEGE_ID)", "convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE '' END  DEGREENAME", " D.DEGREENO in (1,4,12) AND UR.USERNO ='" + Session["userno"] + "' AND ISNULL(FREEZE,0)<>1 AND UR.COLLEGE_ID NOT IN (SELECT COLLEGE_ID FROM ACD_STUDENT WHERE USERNO='" + Session["userno"] + "' AND ISNULL(ADMCAN,0)=0)  AND ISNULL(UR.DEGREENO,0) NOT IN (SELECT ISNULL(DEGREENO,0) FROM ACD_STUDENT WHERE USERNO='" + Session["userno"] + "' AND ISNULL(ADMCAN,0)=0)  AND ISNULL(UR.BRANCHNO,0) NOT IN (SELECT ISNULL(BRANCHNO,0) FROM ACD_STUDENT WHERE USERNO='" + Session["userno"] + "' AND ISNULL(ADMCAN,0)=0)", ""); ///AND ISNULL(APPLIED,0)=0
            //}

            //Comment by Roshan on dated 05/08/2020 not required
            //int RecCount = Convert.ToInt32(objCommon.LookUp("ACD_USER_BRANCH_PREF", "count(*)", " ISNULL(APPLIED,0)<>1 AND USERNO=" + Session["username"] + ""));
            //if (RecCount == 0)
            //{
            //    int DegreeNo = Convert.ToInt32(objCommon.LookUp("ACD_USER_BRANCH_PREF", "DEGREENO", "USERNO=" + Session["username"] + ""));
            //    ddlSchClg.SelectedValue = Convert.ToString(DegreeNo);
            //    ddlSchClg.Enabled = false;
            //}
            //else
            //{
            //    //ddlSchClg.SelectedIndex = 0;
            //   ddlSchClg.Enabled = true;
            //}




        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {

    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    public void bindlist()
    {
        try
        {

            DataSet ds1 = objCommon.FillDropDown("ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO LEFT JOIN ACD_PREFERENCE P ON P.PREID = UR.BRANCH_PREF ", "UR.DEGREENO,UR.COLLEGE_ID,UR.BRANCHNO,UR.BRANCH_PREF", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE '' END  DEGREENAME",
                "isnull(Applied,0)<>0 AND isnull(DocUndertaking,0) <>0 AND UR.USERNO =" + Session["userno"] + "", "D.DEGREENAME");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                int DEGREE = Convert.ToInt32(ds1.Tables[0].Rows[0]["DEGREENO"]);
                int clg = Convert.ToInt32(ds1.Tables[0].Rows[0]["COLLEGE_ID"]);
                int branch = Convert.ToInt32(ds1.Tables[0].Rows[0]["BRANCHNO"]);
                int pref = Convert.ToInt32(ds1.Tables[0].Rows[0]["BRANCH_PREF"]);

                //objCommon.FillDropDownList(ddlSchClg, "ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID", "convert(varchar(10),UR.COLLEGE_ID) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' DEGREENAME", " UR.DEGREENO =" + DEGREE + "AND UR.COLLEGE_ID=" + clg + "AND UR.USERNO =" + Session["username"] + "", "D.DEGREENAME");
                DataSet VALUE = objCommon.FillDropDown("ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO LEFT JOIN ACD_PREFERENCE P ON P.PREID = UR.BRANCH_PREF INNER JOIN ACD_STUDENT S  ON (S.USERNO=UR.USERNO AND S.DEGREENO=UR.DEGREENO AND S.COLLEGE_ID = UR.COLLEGE_ID)", "convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE '' END  DEGREENAME", " UR.USERNO =" + Session["username"] + " AND ISNULL(S.ADMCAN,0)=0 ", "D.DEGREENAME"); // D.DEGREENO =" + DEGREE + "  AND UR.BRANCHNO=" + branch + "AND UR.BRANCH_PREF=" + pref + "  AND UR.COLLEGE_ID=" + clg + " AND

                ///  ddlSchClg.SelectedItem.Text = VALUE.Tables[0].Rows[0]["DEGREENAME"].ToString(); // ROSHAN on 05-08-2020 for label Display Previous Course s
                if (VALUE.Tables[0].Rows.Count > 0)
                {
                    //lblAdmittedProgram.Text = VALUE.Tables[0].Rows[0]["DEGREENAME"].ToString(); //
                }
                else
                {
                    divLastAdmittedPrograme.Visible = false;
                }
            }



            //  objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO IN (8)", "DOCUMENTNO ASC");
            int USERNO1 = Convert.ToInt32(ViewState["userno"].ToString());// ((UserDetails)(Session["user"])).UserNo;
            DataSet ds = new DataSet();
            //if (CheckBoxUndertaking.SelectedIndex > -1)
            //{
            //if (Convert.ToInt32(CheckBoxUndertaking.SelectedValue) == 1)
            //{
            ds = objdocContr.GetDoclistStud(USERNO1);
            //}
            //else if (Convert.ToInt32(CheckBoxUndertaking.SelectedValue) == 2)
            //{
            //  ds = objCommon.FillDropDown("DOCUMENTENTRY_FILE", "DOCNO,DOCNAME", "FILENAME", "DOCNO IN (8) and USERNO = " + Session["username"] + "", "");
            //}


            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvDocument.DataSource = ds;
                lvDocument.DataBind();
                lvDocument.Visible = true;
            }
            else
            {
                lvDocument.DataSource = null;
                lvDocument.DataBind();
                lvDocument.Visible = false;
            }
            //}
            //else      Commented by swapnil thakare on dated 21-06-2021
            //{
            //    return;
            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ListofDocuments.bindlist-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnSaveAndContinue_Click(object sender, EventArgs e)
    {
        try
        {
            string userno = string.Empty;
            int undertakeing = 0;
            int undertakeing1 = 0;
            int DOCNO;

            //Added by swapnil thakare on dated 21-06-2021
            string path = Server.MapPath("~/ONLINEIMAGESUPLOAD\\");
            //string path = System.Configuration.ConfigurationManager.AppSettings["path"];
            string[] branchpref = ddlSchClg.SelectedValue.Split('.');

            if (!(Directory.Exists(path)))
            {
                Directory.CreateDirectory(path);
            }

            int USERNO1 = Convert.ToInt32(ViewState["userno"]);
            DOCNO = Convert.ToInt32(ddlDocument.SelectedValue);
            objdocument.USERNO = USERNO1;
            objdocument.DOCNO = DOCNO;
            objdocument.DOCNAME = ddlDocument.SelectedItem.Text;
            objdocument.PATH = path;
            objdocument.FILENAME = fuDocument.FileName;
            //if (Convert.ToString(CheckBoxUndertaking.SelectedValue) == "")
            //{
            //    ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('Please Select Upload Documents');", true);
            //}
            //else
            //{
            //    undertakeing = Convert.ToInt32(CheckBoxUndertaking.SelectedValue);
            //}

            // undertakeing = Convert.ToInt32(CheckBoxUndertaking.SelectedValue);

            /// undertakeing1 = Convert.ToInt32(CheckUndertaking.SelectedValue);



            if (USERNO1 != 0)
            {

                if (fuDocument.HasFile)
                {
                    if (Convert.ToInt32(ddlDocument.SelectedValue) > 0)
                    {
                        objdocument.FILENAME = USERNO1.ToString() + "_" + ddlDocument.SelectedItem.Text + "_" + fuDocument.FileName.ToString();
                        fuDocument.SaveAs(path + USERNO1.ToString() + "_" + ddlDocument.SelectedItem.Text + "_" + fuDocument.FileName);

                        int i = objdocContr.AddDocStd(objdocument);

                        if (i == 1)
                        {
                            ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('File Uploaded SuccessFully.');", true);
                            ddlDocument.Items.Clear();
                            // Added by swapnil thakare on dated 23-06-2021
                            //objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO IN (1,4,3,9,10,2,11,12,13,14,5,6,7)", "DOCUMENTNO ASC");
                            //objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST D INNER JOIN ACD_DOC_DEGREE ACD ON (D.DOCUMENTNO=ACD.DOCUMENTTYPENO)", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "ACD.DEGREENO=" + branchpref[0] + " ", "DOCUMENTNO ASC");
                            BindDropdownDocument();
                            bindlist();
                        }
                        else if (i == 2)
                        {
                            ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('File Updated SuccessFully.');", true);
                            ddlDocument.Items.Clear();
                            //objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO IN (1,4,3,9,10,2,11,12,13,14,5,6,7)", "DOCUMENTNO ASC");
                            // objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST D INNER JOIN ACD_DOC_DEGREE ACD ON (D.DOCUMENTNO=ACD.DOCUMENTTYPENO)", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "ACD.DEGREENO=" + branchpref[0] + " ", "DOCUMENTNO ASC");
                            BindDropdownDocument();
                            bindlist();
                        }
                        else if (i == -99)
                        {
                            ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('Error.');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('Network Issue.');", true);
                        }

                        ddlDocument.SelectedIndex = 0;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('Select document type first.');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('Select File to Upload.');", true);
                    return;
                    // btnUpload.Enabled = true;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('Session is expired please try again.');", true);
                Response.Redirect("~/default");
                return;
            }


        }
        catch (DirectoryNotFoundException ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ListofDocuments.btnUpload_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Directory Not Found Exception!!");
        }
    }

    protected void btnSub_Click(object sender, EventArgs e)
    {
        //int ChkCancelStatus = 0;
        //string enrollno = Convert.ToString(Session["username"]);
        //ChkCancelStatus = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COUNT(1)", "ENROLLNO='" + enrollno + "' AND ISNULL(ADMCAN,0)=0 AND ISNULL(CAN,0)=0"));
        //if (ChkCancelStatus > 0)
        //{
        //    int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "AL_NO", "AL_URL='academic/CanClgDeg.aspx'"));           
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script", "alert('You have been already applied for another courses. if you want to apply new courses then please cancel previous applied courses.after that apply new courses.  !'); location.href='./CanClgDeg.aspx?pageno=" + pageno + "';", true);

        //    //   Response.Redirect("~/CanClgDeg.aspx");

        //    // return;


        //}
        //else
        //{

        // }
        int undertaking = 0;
        int undertaking1 = 0;
        //if (Convert.ToString(CheckBoxUndertaking.SelectedValue) == "" ) //(Convert.ToInt32(CheckBoxUndertaking.SelectedValue) == 0 )
        //{
        //   // ShowMessage("Please Check Upload Documents and Undertaking Check Box!");
        //    ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('Please Check Upload Documents Check Box!.');", true);
        //    return;
        //}
        //else if (Convert.ToString(CheckUndertaking.SelectedValue) == "")
        if (Convert.ToString(CheckUndertaking.SelectedValue) == "")
        {
            // ShowMessage("Please Check Upload Documents and Undertaking Check Box!");
            ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('Please Check Undertaking Check Box!.');", true);
            return;
        }

       

        int DEGREE = 0;
        int clg = 0;
        string branch = string.Empty;
        string preference = string.Empty;
        if (ddlSchClg.SelectedIndex > 0)
        {
            string degreeno = ddlSchClg.SelectedValue;
            string college = ddlSchClg.SelectedItem.ToString();
            // undertaking = Convert.ToInt32(CheckBoxUndertaking.SelectedValue);
            undertaking1 = Convert.ToInt32(CheckUndertaking.SelectedValue);



            //DataSet ds=  objCommon.FillDropDown("ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID", "convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID) DEGREENO ", "D.DEGREENAME + ' (' + CM.CODE + ')' DEGREENAME", " D.DEGREENO >0 AND UR.USERNO =" + 48 + "", "D.DEGREENAME");

            DataSet ds = objCommon.FillDropDown("ACD_USER_BRANCH_PREF UR INNER JOIN  ACD_DEGREE D ON UR.DEGREENO=D.DEGREENO INNER JOIN  ACD_COLLEGE_MASTER CM ON UR.COLLEGE_ID=CM.COLLEGE_ID LEFT JOIN ACD_SUB_GROUP_MATER SGM ON UR.BRANCHNO = SGM.SUGNO LEFT JOIN ACD_PREFERENCE P ON P.PREID = UR.BRANCH_PREF ", "UR.DEGREENO,UR.COLLEGE_ID,UR.BRANCHNO,UR.BRANCH_PREF", "D.DEGREENAME + ' (' + CM.CODE + ')' + ' ' + CASE WHEN ISNULL(UR.BRANCHNO,0)=0 THEN ' ' ELSE SUB_GROUP_NAME END + ' '  + CASE WHEN ISNULL(UR.BRANCH_PREF,0)=0 THEN ' ' ELSE '' END  DEGREENAME",
               " convert(varchar(10),D.DEGREENO)+ '.' + convert(varchar(10),UR.COLLEGE_ID)+ '.' + convert(varchar(10),UR.BRANCHNO)+ '.' + convert(varchar(10),UR.BRANCH_PREF) ='" + degreeno + "' AND UR.USERNO =" +Convert.ToInt32(ViewState["userno"]) + "", "D.DEGREENAME");
            if (ds.Tables[0].Rows.Count > 0)
            {
                DEGREE = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]);
                clg = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]);
                branch = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                preference = ds.Tables[0].Rows[0]["BRANCH_PREF"].ToString();
            }
            byte[] image;
            if (photoupload.HasFile)
            {
                image = objCommon.GetImageData(photoupload);
            }
            else
            {
                image = null;
            }
            objdocument.USERNO = Convert.ToInt32(ViewState["userno"]);
            objdocument.CollegeCode = Convert.ToInt32(clg);
            objdocument.Degree = DEGREE;
            objdocument.BRANCHNO = branch;
            objdocument.PREFERENCE = preference;
            int idno=Convert.ToInt32(Session["idno"]);
            int Approve = 0;
            Approve = objdocContr.AddDetails(objdocument, undertaking, undertaking1, image, idno);
            if (Approve > 0)
            {
                //ShowMessage("");
                ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('Save Successfully !');", true);
                btnSub.Enabled = false;

                bindlist();
                bindphoto();
                ddlSchClg.SelectedIndex = 0;
            }

        }
    }

      private void bindphoto ()
      {
          DataSet dsp = objCommon.FillDropDown("ACD_STUD_PHOTO", "PHOTO", "IDNO", "IDNO='" + Session["idno"] + "'", "");
          if (dsp.Tables[0].Rows.Count > 0)
          {
              byte[] imgData = null;
              if (dsp.Tables[0].Rows[0]["PHOTO"].ToString() != string.Empty)
              {
                  imgData = dsp.Tables[0].Rows[0]["PHOTO"] as byte[];
                  ImgPhoto.Visible = true;
                  img2.Src = "data:image/png;base64," + Convert.ToBase64String(imgData);
              }
              else
              {
                  imgData = null;
              }
          }
          else
          {
              ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "alert('Photo Not Found !');", true);
          }
      }

    public void TransferToEmail(string toEmailId, string Sub, string sudname, string degreename, string matter, string userno)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("REFF", "MASTERSOFT_GRID_MAILID", "MASTERSOFT_GRID_USERNAME,MASTERSOFT_GRID_PASSWORD", "MASTERSOFT_GRID_MAILID <> '' and MASTERSOFT_GRID_PASSWORD<> '' and MASTERSOFT_GRID_USERNAME <> ''", string.Empty);

            var fromAddress = ds.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString();
            var toAddress = toEmailId;

            string fromPassword = ds.Tables[0].Rows[0]["MASTERSOFT_GRID_PASSWORD"].ToString();
            string userId = ds.Tables[0].Rows[0]["MASTERSOFT_GRID_USERNAME"].ToString();


            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            msg.From = new MailAddress(fromAddress, Sub);
            msg.To.Add(new MailAddress(toAddress));

            msg.Subject = "Admission Status";

            using (StreamReader reader = new StreamReader(Server.MapPath("~/email_template_Applied.html")))
            {
                msg.Body = reader.ReadToEnd();
            }


            msg.Body = msg.Body.Replace("{Name}", sudname.ToString());
            msg.Body = msg.Body.Replace("{confirmed}", matter.ToString());
            msg.Body = msg.Body.Replace("{ApplicationNo}", Session["USERNAME"].ToString());
            msg.Body = msg.Body.Replace("{Degree}", degreename.ToString());
            //msg.Body = msg.Body.Replace("{FeesPaid}", matter.ToString());



            msg.IsBodyHtml = true;
            //smtp.enableSsl = "true";            
            smtp.Host = "smtp.sendgrid.net";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(userId, fromPassword);

            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            smtp.Send(msg);

            ////smtp.Host = "smtp.gmail.com";
            ////smtp.Port = 587;
            ////smtp.UseDefaultCredentials = true;
            ////smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
            ////smtp.EnableSsl = true;
            ////smtp.Send(msg);

            /* Sending Sms - Without resp.*/
            //objUC.NewRegisSMS(username, password, MOBILENO.Trim());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PresentationLayer_NewRegistration.TransferToEmail-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    public void SendSMS(string Mobile, string text)
    {
        string status = "";
        try
        {
            string Message = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?"));
                request.ContentType = "text/xml; charset=utf-8";
                request.Method = "POST";

                string postDate = "ID=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                postDate += "&";
                postDate += "Pwd=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                postDate += "&";
                postDate += "PhNo=91" + Mobile;
                postDate += "&";
                postDate += "Text=" + text;

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
                request.ContentType = "application/x-www-form-urlencoded";

                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse _webresponse = request.GetResponse();
                dataStream = _webresponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                status = reader.ReadToEnd();
            }
            else
            {
                status = "0";
            }
        }
        catch
        {

        }
    }

    //protected void rbtnDoc_SelectedIndexChanged(object sender, EventArgs e)
    //{       

    //    if (Convert.ToInt32(rbtnDoc.SelectedValue) == 0)       
    //    {
    //        DataSet ds = objCommon.FillDropDown("DOCUMENTENTRY_FILE", "DOCNO", "FILENAME", "DOCNO NOT IN (8) and USERNO = " + Session["username"] + "", "");
    //        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            string docno = string.Empty;
    //            DataTable dt = new DataTable();
    //            dt = ds.Tables[0];
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                docno += dt.Rows[i]["DOCNO"] + ",";
    //            }
    //            string doc = docno.TrimEnd(',');
    //            objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO not in (" + doc + ") and DOCUMENTNO IN (1,4,3)", "DOCUMENTNO ASC");

    //        }
    //        else
    //        {
    //            objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO IN (1,3,4)", "DOCUMENTNO ASC");
    //        }

    //        updDocs.Visible = true;
    //        lblDoc.Visible = true;
    //        lblUndertaking.Visible = false;
    //       // objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO not IN (3,1) and DOCUMENTNO IN (1,3,4)", "DOCUMENTNO ASC");
    //        ddlDocument.SelectedIndex = 0;
    //        ddlDocument.Enabled = true;
    //        docmsg.Visible = true;
    //        docUnder.Visible = false;
    //        bindlist(); 
    //    }
    //    else if (Convert.ToInt32(rbtnDoc.SelectedValue) == 1)    
    //    {
    //        updDocs.Visible = true;
    //        lblDoc.Visible = false;
    //        lblUndertaking.Visible = true;
    //        objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO IN (8)", "DOCUMENTNO ASC");
    //        ddlDocument.SelectedIndex = 1;
    //        ddlDocument.Enabled = false;
    //        docmsg.Visible = false;
    //        docUnder.Visible = true;
    //        bindlist();
    //    }

    //    upd.Update();
    //}
    protected void ddlDocument_SelectedIndexChanged(object sender, EventArgs e)
    {

        //ScriptManager.RegisterStartupScript(upd, upd.GetType(), "Script", "$('.fuDocumentX').trigger('click');", true);
    }

    public void BindDropdownDocument()
    {
        DataSet ds = objCommon.FillDropDown("DOCUMENTENTRY_FILE", "DOCNO", "FILENAME", "DOCNO NOT IN (8) and USERNO = " +Convert.ToInt32(ViewState["userno"]) + "", "");
        string[] branchpref = ddlSchClg.SelectedValue.Split('.');
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            string docno = string.Empty;
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                docno += dt.Rows[i]["DOCNO"] + ",";
            }

            // Added by swapnil thakare on dated 23-06-2021
            string doc = docno.TrimEnd(',');
            //objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO not in (" + doc + ") AND DOCUMENTNO IN (1,4,3,9,10,2,11,12,13,14,5,6,7)", "DOCUMENTNO ASC");
            //objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST D INNER JOIN ACD_DOC_DEGREE ACD ON (D.DOCUMENTNO=ACD.DOCUMENTTYPENO)", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO not in (" + doc + ") AND ACD.DEGREENO=" + branchpref[0] + " ", "DOCUMENTNO ASC");
            objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO > 0", "DOCUMENTNO");

        }
        else
        {
            //objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO IN (1,4,3,9,10,2,11,12,13,14,5,6,7)", "DOCUMENTNO ASC");
            //objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST D INNER JOIN ACD_DOC_DEGREE ACD ON (D.DOCUMENTNO=ACD.DOCUMENTTYPENO)", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "ACD.DEGREENO=" + branchpref[0] + " ", "DOCUMENTNO ASC");
            objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO > 0", "DOCUMENTNO");
        }
    }

    public void UploadDocument()
    {
        BindDropdownDocument();
        updDocs.Visible = true;
        lblDoc.Visible = true;
        lblUndertaking.Visible = false;
        // objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO not IN (3,1) and DOCUMENTNO IN (1,3,4)", "DOCUMENTNO ASC");
        ddlDocument.SelectedIndex = 0;
        ddlDocument.Enabled = true;
        docmsg.Visible = true;
        docUnder.Visible = false;
        // undertaking.Visible = false;      // commented by swapnil thakare on dated 22-06-2021
        bindlist();
    }
    protected void CheckBoxUndertaking_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckBoxUndertaking.SelectedIndex > -1)
        {

            if (Convert.ToInt32(CheckBoxUndertaking.SelectedValue) == 1)
            {
                DataSet ds = objCommon.FillDropDown("DOCUMENTENTRY_FILE", "DOCNO", "FILENAME", "DOCNO NOT IN (8) and USERNO = " + Session["username"] + "", "");
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    string docno = string.Empty;
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        docno += dt.Rows[i]["DOCNO"] + ",";
                    }
                    string doc = docno.TrimEnd(',');
                    objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO not in (" + doc + ") AND DOCUMENTNO IN (1,4,3,9,10,2,11,12,13,14,5,6,7)", "DOCUMENTNO ASC");

                }
                else
                {
                    objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO IN (1,4,3,9,10,2,11,12,13,14,5,6,7)", "DOCUMENTNO ASC");
                }

                updDocs.Visible = true;
                lblDoc.Visible = true;
                lblUndertaking.Visible = false;
                // objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO not IN (3,1) and DOCUMENTNO IN (1,3,4)", "DOCUMENTNO ASC");
                ddlDocument.SelectedIndex = 0;
                ddlDocument.Enabled = true;
                docmsg.Visible = true;
                docUnder.Visible = false;
                // undertaking.Visible = false;
                bindlist();
            }
            else if (Convert.ToInt32(CheckBoxUndertaking.SelectedValue) == 2)
            {
                updDocs.Visible = true;
                lblDoc.Visible = false;
                lblUndertaking.Visible = true;
                objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO IN (8)", "DOCUMENTNO ASC");
                ddlDocument.SelectedIndex = 1;
                ddlDocument.Enabled = false;
                docmsg.Visible = false;
                docUnder.Visible = true;
                bindlist();
            }
        }
        else
        {
            Response.Redirect(Request.Url.ToString());
        }

        upd.Update();
    }


    protected void CheckUndertaking_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (CheckUndertaking.SelectedIndex > -1)
        {

            if (Convert.ToInt32(CheckUndertaking.SelectedValue) == 2)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "showPopup", "$('#myModal33').modal('show')", true);
                undertaking.Visible = true;


                updDocs.Visible = true;
                lblDoc.Visible = false;
                lblUndertaking.Visible = true;
                //objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DISTINCT DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO IN (8)", "DOCUMENTNO ASC");
                objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO > 0", "DOCUMENTNO");
                ddlDocument.SelectedIndex = 1;
                ddlDocument.Enabled = false;
                docmsg.Visible = false;
                docUnder.Visible = true;
                //undertaking.Visible = true;
                bindlist();
            }
        }
        else
        {
            Response.Redirect(Request.Url.ToString());
        }

        upd.Update();
    }


    //protected void lnkCancelCourse_Click(object sender, EventArgs e)
    //{
    //    int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "AL_NO", "AL_URL='academic/CanClgDeg.aspx'"));
    //    //Response.Redirect("./CanClgDeg.aspx?pageno=2631");
    //    Response.Redirect("./CanClgDeg.aspx?pageno=" + pageno + "");
    //    //  Response.Redirect("~/home.aspx");
    //}
    //protected void btnphoto_Click(object sender, EventArgs e)
    //{

    //}
}
