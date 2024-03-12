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
using System.Linq;

using System.Configuration;
using System.IO;


public partial class ACADEMIC_StudentDocumentList : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    int count = 0;
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
                if (Convert.ToInt32(Session["usertype"]) == 1)
                {
                    CheckPageAuthorization();
                }
                else
                {

                }

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
                Response.Redirect("~/notauthorized.aspx?page=StudentDocumentList.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentDocumentList.aspx");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtApplicationID.Text.Trim() == string.Empty)
            {

                objCommon.DisplayMessage("Please Enter SR NO.!!", this.Page);
            }
            else
            {
                showdetails();

            }
        }
        catch (Exception ex)
        {
        }
    }
    private void showdetails()
    {
        DataSet ds = new DataSet();
        ds = objSC.GetStudentInfoDocumentListByEnrollNo(txtApplicationID.Text.Trim());
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            divStudInfo.Visible = true;
            DataRow dr = ds.Tables[0].Rows[0];
            this.PopulateStudentInfoSection(dr);       // show student information
        }
        else
        {
            objCommon.DisplayMessage("Please Enter Valid SR NO.!!", this.Page);
        }
    }
    private void PopulateStudentInfoSection(DataRow dr)
    {
        try
        {
            lblStudName.Text = dr["STUDNAME"].ToString();
            lblStudName.ToolTip = dr["ENROLLNO"].ToString();
            lblSex.Text = dr["SEX"].ToString();
            lblRegNo.Text = dr["REGNO"].ToString();
            lblRegNo.ToolTip = dr["IDNO"].ToString();
            lblDateOfAdm.Text = ((dr["ADMDATE"].ToString().Trim() != string.Empty) ? Convert.ToDateTime(dr["ADMDATE"].ToString()).ToShortDateString() : dr["ADMDATE"].ToString());
            lblPaymentType.Text = dr["PAYTYPENAME"].ToString();
            lblDegree.Text = dr["DEGREENAME"].ToString();
            lblDegree.ToolTip = dr["DEGREENO"].ToString();
            lblBranch.Text = dr["BRANCH_NAME"].ToString();
            lblYear.Text = dr["YEARNAME"].ToString();
            lblSemester.Text = dr["SEMESTERNAME"].ToString();
            lblBatch.Text = dr["BATCHNAME"].ToString();
            DataSet dslist = objCommon.FillDropDown("ACD_DOCUMENT_LIST T INNER JOIN ACD_DOC_DEGREE D ON(T.DOCUMENTNO=D.DOCUMENTTYPENO)", "DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO>0 AND D.DEGREENO=" + Convert.ToInt32(lblDegree.ToolTip), "DOCUMENTNO");
            if (dslist != null && dslist.Tables.Count > 0 && dslist.Tables[0].Rows.Count > 0)
            {
                lvDocumentList.Visible = true;
                lvDocumentList.DataSource = dslist;
                lvDocumentList.DataBind();
                btnuploadDocuments.Enabled = true;
                btnReport.Enabled = true;
            }
            else
            {
                lvDocumentList.Visible = false;
                lvDocumentList.DataSource = null;
                lvDocumentList.DataBind();
                btnuploadDocuments.Enabled = false;
                btnReport.Enabled = false;
                objCommon.DisplayMessage("Document Mapping is Not Done For " + lblDegree.Text.Trim().ToString() + "!!", this.Page);
            }
            this.bindlist();
        }
        catch (Exception ex)
        {
        }

    }
    private void bindlist()
    {
        try
        {

            foreach (ListViewDataItem lst in lvDocumentList.Items)
            {
                HiddenField iddoc = (HiddenField)lst.FindControl("docid");
                CheckBox chkOrg = (CheckBox)lst.FindControl("chkOriCopy");
                Button btndownload = (Button)lst.FindControl("btndownload");

                DataSet dsDocumentList = objCommon.FillDropDown("ACD_STUDENT_DOC_LIST", "IDNO", "DOCUMENTNO,ORICOPY,FILEDATA", "IDNO = " + lblRegNo.ToolTip.Trim() + " and DOCUMENTNO=" + iddoc.Value + "", string.Empty);
                if (dsDocumentList.Tables[0].Rows.Count > 0)
                {
                    if (dsDocumentList.Tables[0].Rows[0]["ORICOPY"].ToString() == "1")
                    {
                        chkOrg.Checked = true;
                    }
                    else
                    {
                        chkOrg.Checked = false;
                    }
                    if (dsDocumentList.Tables[0].Rows[0]["FILEDATA"] != DBNull.Value)
                    {
                        btndownload.Visible = true;
                    }
                    else
                    {
                        btndownload.Visible = false;
                    }
                }
                else
                {
                    chkOrg.Checked = false;
                }
            }
        }
        catch (Exception ex)
        { }
    }




    protected void submit_Click(object sender, EventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDocumentList.submit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    int reportcount = 0;
    protected void btnReport_Click(object sender, EventArgs e)
    {

        reportcount = 1;
        if (reportcount == 1)
        {
            if (txtApplicationID.Text.Trim() != string.Empty)
            {
                int studid = 0;
                studid = Convert.ToInt32(lblRegNo.ToolTip);

                string count = objCommon.LookUp("ACD_STUDENT_DOC_LIST", "DISTINCT ISNULL(IDNO,0)IDNO", "IDNO=" + studid);
                if (count != "")
                {
                    ShowReport("Admission_Slip_Report", "AdmissionSlipForBtechDTE.rpt", studid);
                }
                else
                {
                    objCommon.DisplayMessage("Please Check and Submit the Student Documents First!!", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Enter SR NO.!!", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage("Report called By", this.Page);
        }
    }

    private void ShowReport(string reportTitle, string rptFileName, int regno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + regno;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updreport, this.updreport.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }



    protected void btnuploadDocuments_Click(object sender, EventArgs e)
    {
        try
        {
            int chkcount = 0;
            foreach (ListViewDataItem lvitem in lvDocumentList.Items)
            {

                FileUpload fuStudPhoto = lvitem.FindControl("studentFileUpload") as FileUpload;
                int chkoricopy = 0;
                Button btndownload = lvitem.FindControl("btndownload") as Button;
                CheckBox chkDocuments = lvitem.FindControl("chkOriCopy") as CheckBox;
                HiddenField hidstudocno = lvitem.FindControl("docid") as HiddenField;
                int idno = Convert.ToInt32(lblRegNo.ToolTip);
                string ext = System.IO.Path.GetExtension(fuStudPhoto.PostedFile.FileName);
                string filename = Path.GetFileName(fuStudPhoto.PostedFile.FileName);
                byte[] image;



                if (chkDocuments.Checked)
                {
                    chkoricopy = 1;
                    chkcount++;

                }
                else
                {
                    if (ext != "")
                    {
                        goto error;
                    }
                    chkoricopy = 0;
                }
                if ((chkDocuments.Checked && ((fuStudPhoto.HasFile && btndownload.Visible == true) || (fuStudPhoto.HasFile && btndownload.Visible == false) || (!fuStudPhoto.HasFile && btndownload.Visible == false))) || chkDocuments.Checked == false)
                {
                    if (ext == string.Empty || ext.ToLower().Trim() == ".pdf" || ext.ToLower().Trim() == ".doc" || ext.ToLower().Trim() == ".docx" || ext.ToUpper().Trim() == ".JPG" || ext.ToUpper().Trim() == ".JPEG")
                    {
                        string extension = string.Empty;
                        string contentType = string.Empty;
                        HttpPostedFile file;
                        file = fuStudPhoto.PostedFile;

                        byte[] document = null;

                        if (fuStudPhoto.HasFile)
                        {
                            extension = Path.GetExtension(filename);
                            contentType = fuStudPhoto.PostedFile.ContentType;
                            file = fuStudPhoto.PostedFile;
                            document = new byte[file.ContentLength];
                            file.InputStream.Read(document, 0, file.ContentLength);
                        }

                        if (file.ContentLength <= 40960)// 31457280 before size  //For Allowing 40 Kb Size Files only 
                        {
                            CustomStatus cs = (CustomStatus)objSC.Insert_Update_StudentDocuments(Convert.ToInt32(idno), Convert.ToInt32(hidstudocno.Value), Convert.ToInt32(chkoricopy), extension, contentType, document);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                count = 1;
                            }
                            else if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                count = 2;
                            }
                        }
                        else
                        {
                            goto outer;
                        }
                    }
                    else
                    {
                        goto outer;
                    }

                }//sandy

            }
            if (chkcount == 0)
            {

                goto chkcountmsg;
            }
            if (count == 1)
            {
                objCommon.DisplayMessage("Student Documents saved Successfully", this.Page);
            }
            else if (count == 2)
            {
                objCommon.DisplayMessage("Student documents updated Successfully!!", this.Page);
            }

            else
            {
                objCommon.DisplayMessage("failed to update Student documents!!", this.Page);
            }

        chkcountmsg: objCommon.DisplayMessage("Please Check Atleast one Check Box!!", this.Page);
        outer: objCommon.DisplayMessage("Please Upload .pdf,.jpg,.doc files Below or Equal to 40 Kb only", this.Page);
        error: objCommon.DisplayMessage("cannot upload file withot checking the chekbox !!!", this.Page);
            this.bindlist();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_btnuploadDocuments_Click.butSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btndownload_Click(object sender, EventArgs e)
    {
        try
        {
            int studocno = 0;
            Button btn = sender as Button;
            string hdfidno = (btn.Parent.FindControl("docid") as HiddenField).Value;
            studocno = Convert.ToInt32(hdfidno);
            int idno = Convert.ToInt32(lblRegNo.ToolTip);
            string studentname = lblStudName.Text.Trim();
            studentname = studentname.Replace(" ", "_");

            int enrollno = Convert.ToInt32(lblStudName.ToolTip);



            DataSet ds = objCommon.FillDropDown("ACD_STUDENT_DOC_LIST ST INNER JOIN ACD_DOCUMENT_LIST DL ON(ST.DOCUMENTNO=DL.DOCUMENTNO)", "FileData", "ORICOPY,EXTENSION,CONTENTTYPE,DOCUMENTNAME", "IDNO=" + idno + "AND ST.DOCUMENTNO =" + studocno, "DL.DOCUMENTNO");
            DataTable dt = ds.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                string docname = dt.Rows[0]["DOCUMENTNAME"].ToString();
                docname = docname.Replace(" ", "_");
                string extension = dt.Rows[0]["EXTENSION"].ToString();
                string contentType = ds.Tables[0].Rows[0]["CONTENTTYPE"].ToString();
                byte[] bytes = (byte[])dt.Rows[0]["FileData"];
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AddHeader("content-disposition", "attachment;filename=" + docname + "_" + enrollno + extension);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
        }
        catch (Exception ex)
        {

        }
    }


}