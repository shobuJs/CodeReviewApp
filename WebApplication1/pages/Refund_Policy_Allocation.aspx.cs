using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
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
using System.Diagnostics;

using System.Collections.Generic;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using EASendMail;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;

public partial class Projects_Refund_Policy_Allocation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController stud = new StudentController();
    DocumentControllerAcad objdocContr = new DocumentControllerAcad();
    UserController user = new UserController();
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
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
                    //Page Authorization
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                  
                 
                    this.objCommon.FillListBox(lstbxSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
                    this.objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_ID");
                    this.objCommon.FillDropDownList(ddlPolicyName, "ACD_REFUND_POLICY", "REFUNDPOLICY_ID", "REFUNDPOLICY_NAME", "REFUNDPOLICY_ID > 0 AND ISNULL(STATUS,0)=1", "REFUNDPOLICY_ID");
                    this.objCommon.FillDropDownList(ddlStudyLevel, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION > 0", "UA_SECTION");
                    this.objCommon.FillDropDownList(ddlAwardingInstitute, "ACD_AFFILIATED_UNIVERSITY", "AFFILIATED_NO", "AFFILIATED_LONGNAME", "AFFILIATED_NO > 0", "AFFILIATED_NO");
                    objCommon.FillDropDownList(ddlWithdrawalType, "ACD_REFUND_REQUEST_TYPE", "SRNO", "REQUEST_TYPE", "SRNO NOT IN (10)", "");
                    
                    binddata();
                    ViewState["action"] = "add";
                }
                objCommon.SetLabelData("0");//for label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACDEMIC_COLLEGE_MASTER.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ReprintReceipts.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ReprintReceipts.aspx");
        }
    }

    private void binddata()
    {
        DataSet ds = stud.GetRefundAllocationData();
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            pnlRefund.Visible = true;
            LvRefundAllocation.DataSource = ds;
            LvRefundAllocation.DataBind();
        }
        else
        {
            pnlRefund.Visible = false;
            LvRefundAllocation.DataSource = null;
            LvRefundAllocation.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string degreeno = "";
        string branchno = "";
        string program = "";
        string[] pgm = new string[] { };
        string semester = "";
        
        foreach (ListItem item in lstbxProgram.Items)
        {
            if (item.Selected == true)
            {
                program += item.Value + ',';
            }
            //else
            //{
            //    objUCommon.ShowError(this.Page,"Please Select Program");
            //    return;
            //}
        }
        if (!string.IsNullOrEmpty(program))
        {
            pgm = program.Split(',');
            for (int i = 0; i < pgm.Length; i += 2)
            {
                degreeno += pgm[i] + ",";
                
            }
            for (int j = 1; j < pgm.Length; j += 2)
            {
                branchno += pgm[j] + ",";
            }
            degreeno = degreeno.TrimEnd(',');
            branchno = branchno.TrimEnd(',');
        }
      
        else
        {
            program = "0";
        }
        foreach (ListItem item in lstbxSemester.Items)
        {
            if (item.Selected == true)
            {
                semester += item.Value + ',';
            }
            //else
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Select Semester",this.Page);
            //    return;
            //}
        }
        if (!string.IsNullOrEmpty(semester))
        {
            semester = semester.Substring(0, semester.Length - 1);
        }
        else
        {
            semester = "0";
        }
        if (ViewState["action"] != null)
        {
            if (ViewState["action"].ToString().Equals("add"))
            {
                //int chkexit = Convert.ToInt32(objCommon.LookUp("ACD_REFUND_POLICY_ALLOCATION", "COUNT(*)", "COLLEGE_ID=" + ddlFaculty.SelectedValue + " AND UGPG=" + Convert.ToInt32(ddlStudyLevel.SelectedValue) + "AND POLICY_ID=" + ddlPolicyName.SelectedValue + "AND AFFILIATED_NO=" + Convert.ToInt32(ddlAwardingInstitute.SelectedValue) + "AND SEMESTERNO=" + lstbxSemester.SelectedValue + "AND degreeno="+degreeno+ "AND BRANCHNO="+branchno));
                //if (chkexit > 0)
                //{
                //    objCommon.DisplayMessage(this.updCourseCreation, "Records Already Exist!", this.Page);
                //    clear();
                //    return;
                //}
                //else
                //{
                    CustomStatus cs = CustomStatus.Others;
                    cs = (CustomStatus)stud.InsertRefundAllocationData(Convert.ToInt32(ddlFaculty.SelectedValue), Convert.ToString(degreeno), Convert.ToString(branchno), semester, Convert.ToInt32(ddlStudyLevel.SelectedValue), Convert.ToInt32(ddlAwardingInstitute.SelectedValue), Convert.ToInt32(ddlPolicyName.SelectedValue), Convert.ToInt32(Session["userno"]),Convert.ToInt32(ddlWithdrawalType.SelectedValue));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.updCourseCreation, "Record Saved Successfully", this.Page);
                        binddata();
                        clear();

                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updCourseCreation, "Error", this.Page);
                    } 
                //}
            }
            else if (ViewState["action"].ToString().Equals("edit"))
            {
                CustomStatus cs = CustomStatus.Others;
                cs = (CustomStatus)stud.UpdateRefundAllocationData(Convert.ToInt32(ddlFaculty.SelectedValue), Convert.ToString(degreeno), Convert.ToString(branchno), semester, Convert.ToInt32(ddlStudyLevel.SelectedValue), Convert.ToInt32(ddlAwardingInstitute.SelectedValue), Convert.ToInt32(ddlPolicyName.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ViewState["srno"]), Convert.ToInt32(ddlWithdrawalType.SelectedValue));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.updCourseCreation, "Record Updated Successfully", this.Page);
                    ViewState["action"] = "add";
                    btnSubmit.Text = "Submit";
                    binddata();
                    clear();

                }
                else
                {
                    objCommon.DisplayMessage(Page, "Error", this.Page);
                }
            }
        }
    }
    private void clear()
    {
        ddlFaculty.SelectedIndex = 0;
        ddlStudyLevel.SelectedIndex = 0;
        ddlAwardingInstitute.SelectedIndex = 0;
        lstbxProgram.ClearSelection();
        lstbxSemester.ClearSelection();
        ddlPolicyName.SelectedIndex = 0;
        btnSubmit.Text = "Submit";
       
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillListBox(lstbxProgram, "ACD_USER_BRANCH_PREF S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=S.BRANCHNO)", "DISTINCT CONCAT(S.DEGREENO,',',S.BRANCHNO)AS ID", "(D.DEGREENAME +'-'+ B.LONGNAME)AS DEGBRANCH", "S.COLLEGE_ID=" + Convert.ToInt32(ddlFaculty.SelectedValue) + "", "ID");
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["action"] = "edit";
            int srno = int.Parse(btnEdit.CommandArgument);
            ViewState["srno"] = srno;
            btnSubmit.Text = "Update";
            ShowDetails(srno);
        }
        catch (Exception ex)
        {
        }
    }
    private void ShowDetails(int courseno)
    {

        try
        {

            DataSet ds = objCommon.FillDropDown("ACD_REFUND_POLICY_ALLOCATION", "REQUEST_TYPE,SRNO,COLLEGE_ID,DEGREENO BRANCHNO,CONCAT(DEGREENO,',',BRANCHNO)AS PROGRAM,SEMESTERNO,UGPG,AFFILIATED_NO,POLICY_ID,CREATED_UA_NO,CREATED_DATE", "", "SRNO=" + courseno, "SRNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlFaculty.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                ddlWithdrawalType.SelectedValue = ds.Tables[0].Rows[0]["REQUEST_TYPE"].ToString();
                lstbxSemester.SelectedValue = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                ddlPolicyName.SelectedValue = ds.Tables[0].Rows[0]["POLICY_ID"].ToString();
                ddlAwardingInstitute.SelectedValue = ds.Tables[0].Rows[0]["AFFILIATED_NO"].ToString();
                ddlStudyLevel.SelectedValue = ds.Tables[0].Rows[0]["UGPG"].ToString();
                string program = (ds.Tables[0].Rows[0]["PROGRAM"].ToString());
                ddlFaculty_SelectedIndexChanged(new object(), new EventArgs());  
                string[] pgm = program.Split('&');
                for (int j = 0; j < pgm.Length; j++)
                {
                    for (int i = 0; i < lstbxProgram.Items.Count; i++)
                    {
                        if (pgm[j] == lstbxProgram.Items[i].Value)
                        {
                            lstbxProgram.Items[i].Selected = true;
                        }
                    }
                }     
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseCreation.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
}