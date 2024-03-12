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
public partial class ACADEMIC_AcademicRoomMapping : System.Web.UI.Page
{
    Common objCommon = new Common();
    User_AccController objUACC = new User_AccController();
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
                bindlist();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }
                objCommon.FillDropDownList(ddldept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNAME");
                DataSet ds = objCommon.FillDropDown("ACD_ACADEMIC_ROOMMASTER", "Roomno", "Roomname", "Roomno>0", "Roomno");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkroom.DataSource = ds;
                    chkroom.DataTextField = ds.Tables[0].Columns["Roomname"].ToString();
                    chkroom.DataValueField = ds.Tables[0].Columns["Roomno"].ToString();
                    chkroom.DataBind();
                }
            }
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); 
            objCommon.SetLabelData(ddldept.SelectedValue);
        }
    }
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
            Response.Redirect("~/notauthorized.aspx?page=DepartmentMapping.aspx");
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int deptno = Convert.ToInt32(ddldept.SelectedValue);
            string roomno = string.Empty;
            foreach (ListItem item in chkroom.Items)
            {
                if (item.Selected == true)
                {
                    roomno = roomno + item.Value.ToString() + ",";

                }
            }
            if (roomno != "")
            {
                if (roomno.Substring(roomno.Length - 1) == ",")
                    roomno = roomno.Substring(0, roomno.Length - 1);
            }

            int ret = objUACC.Insertacademicroommap(deptno, roomno);
            if (ret == 1)
            {
                objCommon.DisplayMessage(this.updAcademroom, "Record Inserted Successfully", this.Page);
            }
            else if (ret == 2)
            {
                objCommon.DisplayMessage(this.updAcademroom, "Record Updated Successfully", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updAcademroom, "Failed to save the Record", this.Page);
            }
            bindlist();
        }
        catch (Exception)
        {
        }
    }
    private void bindchkdash()
    {

        chkroom.ClearSelection();
        //DataSet ds = objCommon.FillDropDown("acd_department_room", "distinct deptno", "roomno", "deptno=" + ddldept.SelectedValue, "roomno");//commented as per roshan sir09072021
        DataSet ds = objCommon.FillDropDown("ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEPT CD ON (CD.DEPTNO=D.DEPTNO) INNER JOIN acd_academic_roommaster R ON(R.Collegeid=CD.COLLEGE_ID)", "distinct d.deptno", "r.roomno", "d.deptno=" + ddldept.SelectedValue, "roomno");
        if (ds.Tables[0].Rows.Count > 0)
        {
            string dashboard = ds.Tables[0].Rows[0][1].ToString();
            foreach (string value in dashboard.Split(','))
            {
                int val = Convert.ToInt32(value);
                DataSet ds1 = objCommon.FillDropDown("acd_academic_roommaster", "roomno", "roomname", "roomno=" + value, "roomname");

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    int i;
                    int j;
                    for (j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        for (i = 0; i < chkroom.Items.Count; i++)
                        {
                            if (chkroom.Items[i].Value == ds1.Tables[0].Rows[j]["roomno"].ToString())
                            {
                                chkroom.Items[i].Selected = true;

                                break;
                            }
                        }
                    }
                }
            }
        }

    }

    private void bindlist()
    {
        DataSet ds = objUACC.getlistrooms();
        lvlist.DataSource = ds;
        lvlist.DataBind();

    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        ddldept.SelectedValue = "0";
        int count = chkroom.Items.Count;
        for (int i = 0; i < count; i++)
        {
            if (chkroom.Items[i].Selected == true)
            {
                chkroom.Items[i].Selected = false;
            }
        }
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindchkdash();
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        ShowReport("Room", "rptBranchwiseDetails.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updAcademroom, this.updAcademroom.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        { }
    }

    //protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlCollege.SelectedIndex > 0)
    //    {
    //        objCommon.FillDropDownList(ddldept, "ACD_DEPARTMENT A INNER JOIN ACD_COLLEGE_DEPT B ON (A.DEPTNO=B.DEPTNO)", "A.DEPTNO", "A.DEPTNAME", "A.DEPTNO>0 AND B.COLLEGE_ID=" + ddlCollege.SelectedValue, "A.DEPTNO");
    //        chkroom.Items.Clear();
    //        DataSet ds = objCommon.FillDropDown("ACD_ACADEMIC_ROOMMASTER", "Roomno", "Roomname", "Roomno>0", "Roomno");
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            chkroom.DataSource = ds;
    //            chkroom.DataTextField = ds.Tables[0].Columns["Roomname"].ToString();
    //            chkroom.DataValueField = ds.Tables[0].Columns["Roomno"].ToString();
    //            chkroom.DataBind();
    //        }
    //    }
    //    else
    //    {
    //        ddldept.SelectedIndex = 0;
    //        objCommon.DisplayMessage("Please Select College", this.Page);
    //        ddlCollege.Focus();
    //    }
    //}
}