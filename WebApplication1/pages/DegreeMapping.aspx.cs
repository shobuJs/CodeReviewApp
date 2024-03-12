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

public partial class ACADEMIC_MASTERS_DegreeMapping : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    MappingController objmp = new MappingController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Config objConfig = new Config();
    //string DegreeNos = string.Empty;
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
                Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
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
                    //   objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and isnull(ACTIVE,0)=1", "COLLEGE_ID");
                    ddlColg.SelectedIndex = 1;
                }
                else
                {
                    //   objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and isnull(ACTIVE,0)=1", "COLLEGE_ID");
                    ddlColg.SelectedIndex = 0;
                }
            }

            //objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");         

            //  objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");  // Comment Shikant Ramekar
            objCommon.FillDropDownList(ddlDegreeMultiCheck, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");  // Comment Shikant Ramekar

            ViewState["action"] = "add";
            
            // BindDegree();     // Added By Shikant Ramekar 1 may 2019
            //hdndegreeno.Value = "0";
            objCommon.SetLabelData(ddlColg.SelectedValue);
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); 
        }

        BindListView();
        // hdndegreeno.Value = "0";
    }

    //private void BindDegree()
    //{
    //    try
    //    {
    //        DataSet ds = objCommon.FillDropDown("ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlDegree.DataSource = ds;
    //            ddlDegree.DataValueField = ds.Tables[0].Columns[0].ToString();
    //            ddlDegree.DataTextField = ds.Tables[0].Columns[1].ToString();
    //            ddlDegree.DataBind();
    //        }
    //        else
    //        {
    //            ddlDegree.Items.Clear();
    //            ddlDegree.Items.Add(new ListItem("Please Select", "0"));
    //        }
    //        ddlDegree.Focus();
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
                Response.Redirect("~/notauthorized.aspx?page=DegreeMapping.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DegreeMapping.aspx");
        }
    }
    #endregion
    //private string GetDegree()
    //{
    //    string DegreeNos = "";

    //    foreach (ListItem item in ddlDegree.Items)
    //    {
    //        if (item.Selected == true)
    //        {
    //            DegreeNos += item.Value + ',';
    //        }

    //    }
    //    if (!string.IsNullOrEmpty(DegreeNos))
    //    {
    //        objConfig.DegreeNoS = DegreeNos.Substring(0, DegreeNos.Length - 1);
    //    }
    //    return DegreeNos;
    //}

    private string GetDegreeNew()
    {
        string DegreeNos = "";


        foreach (ListItem item in ddlDegreeMultiCheck.Items)
        {
            if (item.Selected == true)
            {
                DegreeNos += item.Value + ',';
            }

        }
        if (!string.IsNullOrEmpty(DegreeNos))
        {
            objConfig.DegreeNoS = DegreeNos.Substring(0, DegreeNos.Length - 1);
        }
        return DegreeNos;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string degreeno = string.Empty;
            int _degreeNo = 0, ck = 0;

            degreeno = hdndegreeno.Value;
            if (ddlColg.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.updGradeEntry, "Please select Atleast one Institute !", this.Page);
                return;
            }
            //if ((degreeno)== "0")//GetDegreeNew()
            //{
            //    objCommon.DisplayMessage(this.updGradeEntry, "Please select at least one Degree !", this.Page);
            //    return;
            //}
            if ((degreeno) == "")//GetDegreeNew()
            {
                objCommon.DisplayMessage(this.updGradeEntry, "Please select at least one Degree !", this.Page);
                return;
            }
            else
            {
                degreeno = degreeno.Substring(0, degreeno.Length - 1);
                if (degreeno != "")
                {
                    string[] degValue = degreeno.Split(',');
                    foreach (string s in degValue)
                    {
                        _degreeNo = Convert.ToInt32(s);
                        ck = objmp.AddDegree(Convert.ToInt32(_degreeNo), Convert.ToInt32(ddlColg.SelectedValue));
                    }
                    if (ck == 1)
                    {
                        objCommon.DisplayMessage(this.Page, "Record Saved Successfully", this.Page);
                        degreeno = "";
                        BindListView();
                        // BindDegree();
                        this.bindColDegree();
                        Clear();
                        return;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updGradeEntry, "Few or All Record already Exist", this.Page);
                        //objCommon.DisplayMessage(this.Page, "Record Saved Sucessfully", this.Page);.
                        degreeno = "";
                        BindListView();
                        //BindDegree();
                        this.bindColDegree();
                        Clear();
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updGradeEntry, "Please select at least one Degree !", this.Page);
                    return;
                }
            }
        }
        catch { }

    }

    protected void Clear()
    {
        ddlDegreeMultiCheck.SelectedIndex = 0;
        ddlColg.SelectedIndex = 0;
    }

    protected void btnDel_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnDel = sender as ImageButton;

        ViewState["action"] = "delete";
        int srno = int.Parse(btnDel.CommandArgument);

         // Added by swapnil thakare on dated 02/08/2021

        DataSet collegedept = objCommon.FillDropDown("ACD_COLLEGE_DEGREE", "COLLEGE_ID", "DEGREENO", "COL_DEGREE_NO=" + srno + "", "");
       // LookUp
        string result = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "ISNULL(CDBNO,0)AS CDBNO", "COLLEGE_ID=" + collegedept.Tables[0].Rows[0]["COLLEGE_ID"].ToString() + " AND DEGREENO = " + collegedept.Tables[0].Rows[0]["DEGREENO"].ToString() + "");
        if (result !="")
        {
            //objCommon.DisplayMessage(this.updGradeEntry, "You Can't delete this entry", this.Page);
            objCommon.DisplayMessage(this.updGradeEntry, "You can not delete this mapping ,because program already mapped !!", this.Page);
        }
        else
        {
            int output = objmp.deleteRecord(srno);
            if (output != -99 && output != 99)
            {
                objCommon.DisplayMessage(this.updGradeEntry, "Record Deleted Successfully!!", this.Page);
                ddlColg.ClearSelection();
            }
            else
            {
                objCommon.DisplayMessage(this.updGradeEntry, "Record Is Not Deleted ", this.Page);
            }
        }
        BindListView();

        updGradeEntry.Update();
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN  ACD_COLLEGE_MASTER C ON(A.COLLEGE_ID=C.COLLEGE_ID)", "A.COL_DEGREE_NO,A.COLLEGE_ID", "B.DEGREENAME,ISNULL(c.COLLEGE_NAME,'') COLLEGE_NAME", "A.COLLEGE_ID="+ ddlColg.SelectedValue, "A.COLLEGE_ID");

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
                objUCommon.ShowError(Page, "Academic_Masters_degree.BindListView-> " + ex.Message + " " + ex.StackTrace);
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
            // if (ddlColg.SelectedIndex > 0)
            // {
            ////     objCommon.FillDropDownList(ddlDegreeMultiCheck, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE CD ON (CD.DEGREENO=D.DEGREENO)", "D.DEGREENO", "D.CODE", "D.DEGREENO>0 AND CD.COLLEGE_ID="+ddlColg.SelectedValue, "D.DEGREENO");
            // }
            BindListView();
            this.bindColDegree();
        }
        catch { }
    }

    public void bindColDegree()
    {
        //DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_DEGREE a inner join acd_degree b on(a.degreeno=b.degreeno) inner join  ACD_COLLEGE_MASTER c on(a.college_id=c.college_id)", "a.col_degree_no,a.college_id", "b.degreename,c.college_name", "a.college_id=" + ddlColg.SelectedValue, "a.college_id");
        DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_DEGREE a inner join acd_degree b on(a.degreeno=b.degreeno) inner join  ACD_COLLEGE_MASTER c on(a.college_id=c.college_id)", "a.col_degree_no,a.college_id", "b.degreename,ISNULL(COLLEGE_NAME,'') COLLEGE_NAME", "a.college_id=" + ddlColg.SelectedValue, "a.college_id");

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
