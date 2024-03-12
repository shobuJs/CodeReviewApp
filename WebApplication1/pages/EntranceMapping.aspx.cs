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

public partial class ACADEMIC_EntranceMapping : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    MappingController objmp = new MappingController();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
                }

            }
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
            //objCommon.FillDropDownList(ddlEntrance, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO>0 AND QEXAMSTATUS = 'E'", "QUALIFYNO");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0 AND COLLEGE_ID IN (" + Session["college_nos"].ToString() + ")", "COLLEGE_ID");
            ViewState["action"] = "add";
        }
        BindListView();
    }

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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string entranceexamno = string.Empty;
            int _EntranceNo = 0, ck = 0;

            entranceexamno = hdnentranceexamNo.Value;
            if (ddlDegree.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.updGradeEntry, "Please select Atleast one Degree !", this.Page);
                return;
            }
            if ((entranceexamno) == "")//GetDocs()
            {
                objCommon.DisplayMessage(this.updGradeEntry, "Please select Atleast one Entrance Exam !", this.Page);
                return;
            }
            else
            {
                entranceexamno = entranceexamno.Substring(0, entranceexamno.Length - 1);
                string[] examValue = entranceexamno.Split(',');
                foreach (string s in examValue)
                {
                    _EntranceNo = Convert.ToInt32(s);
                    ck = objmp.AddEntrance(Convert.ToInt32(_EntranceNo), Convert.ToInt32(ddlDegree.SelectedValue),Convert.ToInt32(ddlCollege.SelectedValue));
                }


                if (ck == 1)
                {
                    objCommon.DisplayMessage(this.updGradeEntry, "Record Saved Successfully", this.Page);
                    entranceexamno = "";
                    BindListView();
                    //this.bindEntranceExams();
                    Clear();
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(this.updGradeEntry, "Few or All Record already Exist", this.Page);
                    entranceexamno = "";
                    BindListView();
                    // this.bindEntranceExams();
                    Clear();
                    return;
                }
            }

        }
        catch { }

        //int ck = objmp.AddEntrance(Convert.ToInt32(ddlEntrance.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));
        //if (ck == 1)
        //{
        //    objCommon.DisplayMessage(this.updGradeEntry, "Save Record Sucessfully", this.Page);
        //    BindListView();
        //    return;
        //}
        //else
        //{
        //    objCommon.DisplayMessage(this.updGradeEntry, "Record already Exist", this.Page);
        //    return;
        //}
    }

    protected void Clear()
    {
        ddlCollege.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlEntrance.SelectedIndex = 0;
    }

    protected void btnDel_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnDel = sender as ImageButton;
        ViewState["action"] = "delete";
        int srno = int.Parse(btnDel.CommandArgument);
        int output = objmp.deleteEntrance(srno);
        if (output != -99 && output != 99)
        {
            objCommon.DisplayMessage(updGradeEntry, "Information Deleted Successfully!!", this.Page);
            Clear();
        }
        else
        {
            objCommon.DisplayMessage(updGradeEntry, "Information Is Not Deleted ", this.Page);
            Clear();
        }
        // }
        BindListView();
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_ENTRE_DEGREE E INNER JOIN ACD_DEGREE B ON (E.DEGREENO=B.DEGREENO) INNER JOIN  ACD_QUALEXM Q ON (Q.QUALIFYNO=E.QUALIFYNO)", "E.ENTR_DEGREE_NO,E.DEGREENO", "B.DEGREENAME,Q.QUALIEXMNAME", "Q.QEXAMSTATUS = 'E'", "E.DEGREENO");

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvlist.DataSource = ds;
                lvlist.DataBind();
            }
            else
            {
                lvlist.DataSource = null;
                lvlist.DataBind();
            }

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

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlEntrance, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO>0 AND QEXAMSTATUS = 'E'", "QUALIFYNO");
    }
    private void bindEntranceExams()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_ENTRE_DEGREE E INNER JOIN ACD_DEGREE B ON (E.DEGREENO=B.DEGREENO) INNER JOIN  ACD_QUALEXM Q ON (Q.QUALIFYNO=E.QUALIFYNO)", "E.ENTR_DEGREE_NO,E.DEGREENO", "B.DEGREENAME,Q.QUALIEXMNAME", "Q.QEXAMSTATUS = 'E' and E.DEGREENO=" + ddlDegree.SelectedValue, "E.DEGREENO");
            lvlist.DataSource = null;
            lvlist.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvlist.DataSource = ds;
                lvlist.DataBind();
            }
            else
            {
                lvlist.DataSource = null;
                lvlist.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_degree.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");            
            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_MASTER CM INNER JOIN ACD_COLLEGE_DEGREE CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO)", "CD.DEGREENO", "D.DEGREENAME", "CM.COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENO");
        }
        else
        {
            ddlDegree.SelectedIndex = 0;
            ddlCollege.Focus();
        }
    }
}