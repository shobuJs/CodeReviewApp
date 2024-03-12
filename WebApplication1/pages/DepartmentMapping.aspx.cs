using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class ACADEMIC_MASTERS_DepartmentMapping : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    MappingController objmp = new MappingController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Config objConfig = new Config();
    // string DepartNos = string.Empty;
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
                this.CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                int mul_col_flag = Convert.ToInt32(objCommon.LookUp("REFF", "MUL_COL_FLAG", "CollegeName='" + Page.Title + "'"));
                if (mul_col_flag == 0)
                {
                    objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and isnull(ACTIVE,0)=1", "COLLEGE_ID");
                    ddlColg.SelectedIndex = 1;
                }
                else
                {
                    objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and isnull(ACTIVE,0)=1", "COLLEGE_ID");

                }
            }

            //objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");

            // objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNO");
            //BindListView();


            objCommon.FillDropDownList(ddlDeptMultiCheck, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNO");//DEPTCODE
            ViewState["action"] = "add";

            //BindDeparment();     // Added By Shikant Ramekar 1 may 2019
            objCommon.SetLabelData(ddlColg.SelectedValue);
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); // Add By Roshan Pannase On 21-07-2021
        }
    }
    //private void BindDeparment()
    //{
    //    try
    //    {
    //        DataSet ds = objCommon.FillDropDown("ACD_DEPARTMENT", "DEPTNO", "DEPTCODE", "DEPTNO>0", "DEPTNO");
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlDept.DataSource = ds;
    //            ddlDept.DataValueField = ds.Tables[0].Columns[0].ToString();
    //            ddlDept.DataTextField = ds.Tables[0].Columns[1].ToString();
    //            ddlDept.DataBind();
    //        }
    //        else
    //        {
    //            ddlDept.Items.Clear();
    //            ddlDept.Items.Add(new ListItem("Please Select", "0"));
    //        }
    //        ddlDept.Focus();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DepartmentMapping.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DepartmentMapping.aspx");
        }
    }
    #endregion

    //private string GetDepartment()
    //{
    //    string DepartNos = "";

    //    foreach (ListItem item in ddlDept.Items)
    //    {
    //        if (item.Selected == true)
    //        {
    //            DepartNos += item.Value + ',';
    //        }

    //    }
    //    if (!string.IsNullOrEmpty(DepartNos))
    //    {
    //        objConfig.DepartNos = DepartNos.Substring(0, DepartNos.Length - 1);
    //    }
    //    return DepartNos;
    //}

    //private string GetDeptNew()
    //{
    //    string DepartNos = "";


    //    foreach (ListItem item in ddlDeptMultiCheck.Items)
    //    {
    //        if (item.Selected == true)
    //        {
    //            DepartNos += item.Value + ',';
    //        }

    //    }
    //    if (!string.IsNullOrEmpty(DepartNos))
    //    {
    //        objConfig.DepartNos = DepartNos.Substring(0, DepartNos.Length - 1);
    //    }
    //    return DepartNos;
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string deptno = string.Empty;
            int _deptNo = 0, ck = 0;

            deptno = hdndeptno.Value;
            //if (ddlColg.SelectedIndex == 0)
            //{
            //    objCommon.DisplayMessage(this.updGradeEntry, "Please Select Faculty/School Name", this.Page);
            //    return;
            //}
            if ((deptno) == "")//GetDepartment()
            {
                objCommon.DisplayMessage(this.updGradeEntry, "Please select at least one Department", this.Page);
                return;
            }
            else
            {
                deptno = deptno.Substring(0, deptno.Length - 1);
                string[] deptValue = deptno.Split(',');
                foreach (string s in deptValue)
                {
                    _deptNo = Convert.ToInt32(s);
                    ck = objmp.AddDept(Convert.ToInt32(_deptNo), Convert.ToInt32(ddlColg.SelectedValue));
                }

                //int ck = objmp.AddDept(Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue));
                if (ck == 1)
                {

                    objCommon.DisplayMessage(this.updGradeEntry, "Record Saved Successfully", this.Page);
                    deptno = "";
                    BindListView();

                    this.bindColDept();
                    Clear();
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(this.updGradeEntry, "Few or All Record already Exist", this.Page);
                    deptno = "";
                    BindListView();
                    // BindDeparment();
                    this.bindColDept();
                    Clear();
                    return;
                }
            }
        }
        catch { }
    }

    protected void Clear()
    {
        ddlDeptMultiCheck.SelectedIndex = 0;
        ddlColg.SelectedIndex = 0;
    }

    protected void btnDel_Click(object sender, ImageClickEventArgs e)
    {
        //ACD_COLLEGE_DEGREE_BRANCH;

        ImageButton btnDel = sender as ImageButton;

        ViewState["action"] = "delete";
        int srno = int.Parse(btnDel.CommandArgument);

        // Added by swapnil thakare on dated 02/08/2021

        DataSet collegedept = objCommon.FillDropDown("ACD_COLLEGE_DEPT", "COLLEGE_ID", "DEPTNO", "COL_DEPT_NO=" + srno + "", "");
        // LookUp
        string result = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "ISNULL(CDBNO,0)AS CDBNO", "COLLEGE_ID=" + collegedept.Tables[0].Rows[0]["COLLEGE_ID"].ToString() + " and DEPTNO = " + collegedept.Tables[0].Rows[0]["DEPTNO"].ToString() + "");
        if (result != "")
        {
            // objCommon.DisplayMessage(this.updGradeEntry, "You Can't delete this entry ", this.Page);
            objCommon.DisplayMessage(this.updGradeEntry, "You can not delete this mapping ,because program already mapped ", this.Page);
        }
        else
        {
            int output = objmp.deleteDeptRecord(srno);
            if (output != -99 && output != 99)
            {
                objCommon.DisplayMessage(this.updGradeEntry, "Record Deleted Successfully", this.Page);
                ddlColg.ClearSelection();
                Clear();
            }
            else
            {
                objCommon.DisplayMessage(this.updGradeEntry, "Record Is Not Deleted ", this.Page);
                Clear();
            }
        }


        BindListView();
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_DEPT a inner join acd_dePARTMENT b on(a.dePTno=b.dePTno) inner join  ACD_COLLEGE_MASTER c on(a.college_id=c.college_id)", "a.col_dePT_no,a.college_id", "b.DEPTname,ISNULL(c.COLLEGE_NAME,'')  COLLEGE_NAME", "A.COLLEGE_ID=" + ddlColg.SelectedValue, "a.college_id");

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvlist.DataSource = ds;
                lvlist.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvlist);//Set label 
                objCommon.SetListViewHeaderLabel(Convert.ToString(Request.QueryString["pageno"]), lvlist);
            }
            else
            {
                lvlist.DataSource = null;
                lvlist.DataBind();
            }
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_dePT.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }


    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlColg.SelectedIndex > 0)
            //{
            //    objCommon.FillDropDownList(ddlDeptMultiCheck, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEPT CD ON (CD.DEPTNO=D.DEPTNO)", "D.DEPTNO", "D.DEPTNAME", "D.DEPTNO>0 AND COLLEGE_ID=" + ddlColg.SelectedValue, "D.DEPTNO");//DEPTCODE
            //}
            BindListView();       // Binding List View..
            this.bindColDept();
        }
        catch { }
    }
    public void bindColDept()
    {
        //DataSet ds = objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_DEPT a inner join ACD_DEPARTMENT b on(a.DEPTNO=b.DEPTNO) inner join  ACD_COLLEGE_MASTER c on(a.COLLEGE_ID=c.COLLEGE_ID)", "a.COL_DEPT_NO,a.COLLEGE_ID", "b.DEPTNAME,c.COLLEGE_NAME", "a.COLLEGE_ID=" + ddlColg.SelectedValue, "a.COLLEGE_ID");
        DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_DEPT a inner join acd_dePARTMENT b on(a.dePTno=b.dePTno) inner join  ACD_COLLEGE_MASTER c on(a.college_id=c.college_id)", "a.col_dePT_no,a.college_id", "b.DEPTname,ISNULL(c.COLLEGE_NAME,'')COLLEGE_NAME", "a.college_id=" + ddlColg.SelectedValue, "a.college_id");
        lvlist.DataSource = null;
        lvlist.DataBind();

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvlist.DataSource = ds;
            lvlist.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(Session["userno"]), lvlist);//Set label 
            objCommon.SetListViewHeaderLabel(Convert.ToString(Request.QueryString["pageno"]), lvlist);

        }
        else
        {
            lvlist.DataSource = null;
            lvlist.DataBind();
        }
        objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
    }

}
