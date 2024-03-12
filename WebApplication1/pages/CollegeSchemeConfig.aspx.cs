using System;
using System.Collections;
using System.Configuration;
using System.Data;
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
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Linq;
using System.IO;



public partial class ACADEMIC_CollegeSchemeConfig : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ConfigController objConfig = new ConfigController();
    string degreeno, branchno, schemeno, schemename, branchname, degreebranch, degreebranchscheme, degreeOfBranch, branchOfScheme = string.Empty;
    int count = 0;

    //USED FOR INITIALSING THE MASTER PAGE
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    //USED FOR BYDEFAULT LOADING THE default PAGE
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

                objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0 AND COLLEGE_ID IN (" + Session["college_nos"].ToString() + ")", "COLLEGE_ID");

            }

            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
        }

    }

    #region User Defined Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CollegeSchemeConfig.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CollegeSchemeConfig.aspx");
        }
    }

    #endregion

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        cblDegree.Items.Clear();
        if (ddlCollege.SelectedIndex > 0)
        {
            divDegree.Visible = true;
            divBranch.Visible = false;
            divScheme.Visible = false;
            fillChecboxlistDegree();
        }
        else
        {
            divBranch.Visible = false;
            divDegree.Visible = false;
            divScheme.Visible = false;
        }
    }

    public void fillChecboxlistDegree()
    {
        degreeno = string.Empty;
        ViewState["degreeno"] = "";

        DataSet dsDegree = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CDB.DEGREENO) LEFT JOIN ACD_COLLEGE_SCHEME_MAPPING CSM ON (CSM.COLLEGE_ID=CDB.COLLEGE_ID AND CSM.DEGREENO=CDB.DEGREENO)", "DISTINCT CDB.DEGREENO", "D.DEGREENAME, CSM.DEGREENO AS DEGREENO1", "ISNULL(D.DEGREENO,0)>0 AND CDB.COLLEGE_ID=" + ddlCollege.SelectedValue, "CDB.DEGREENO");
        if (dsDegree != null && dsDegree.Tables.Count > 0)
        {
            if (dsDegree.Tables[0].Rows.Count > 0)
            {
                cblDegree.DataSource = null;
                cblDegree.DataBind();
                cblDegree.DataTextField = "DEGREENAME";
                cblDegree.DataValueField = "DEGREENO";
                cblDegree.DataSource = dsDegree.Tables[0];
                cblDegree.DataBind();
                divDegree.Visible = true;

                int i = 0;
                foreach (ListItem item in cblDegree.Items)
                {
                    if (dsDegree.Tables[0].Rows[i]["DEGREENO1"].ToString() == item.Value)
                    {
                        item.Selected = true;
                        degreeno = degreeno + item.Value + ",";
                    }
                    else
                    {
                        item.Selected = false;
                    }

                    i++;
                }

                if (degreeno.Length > 0)
                {
                    if (degreeno.Substring(degreeno.Length - 1).Contains(','))
                    {
                        degreeno = degreeno.Remove(degreeno.Length - 1);
                    }
                    ViewState["degreeno"] = degreeno.ToString();
                    fillChecboxlistBranch(degreeno);
                }
                else
                {
                    divBranch.Visible = false;
                    divScheme.Visible = false;
                }

            }
            else
            {
                cblDegree.DataSource = null;
                cblDegree.DataBind();
                divDegree.Visible = false;
                divBranch.Visible = false;
                divScheme.Visible = false;
                btnSubmit.Enabled = false;
            }
        }
    }

    protected void cblDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        divScheme.Visible = false;
        cblScheme.Items.Clear();
        btnSubmit.Enabled = false;

        foreach (ListItem item in cblDegree.Items)
        {
            if (item.Selected)
            {
                degreeno = degreeno + item.Value + ",";
            }
        }

        if (degreeno != null)
        {
            if (degreeno.Length > 0)
            {
                if (degreeno.Substring(degreeno.Length - 1).Contains(','))
                {
                    degreeno = degreeno.Remove(degreeno.Length - 1);
                }
                ViewState["degreeno"] = degreeno.ToString();
                fillChecboxlistBranch(degreeno);
            }
            else
            {
                divBranch.Visible = false;
                divScheme.Visible = false;
            }
        }
        else
        {
            divBranch.Visible = false;
            divScheme.Visible = false;
        }

        if (cblScheme.Items.Count <= 0)
        {
            if (cblBranch.Items.Count > 0)
            {
                foreach (ListItem item in cblDegree.Items)
                {
                    if (item.Selected)
                    {
                        count++;
                    }
                }
            }
        }

        if (count > 0)
        {
            btnSubmit.Enabled = true;
        }
        else
        {
            btnSubmit.Enabled = false;
        }

    }

    public void fillChecboxlistBranch(string degreeno)
    {
        branchno = string.Empty;
        ViewState["branchno"] = "";
        ViewState["degreebranch"] = "";

        DataSet dsBranch = null;

        // Edited by harshal on 11-10-2021 regarding to add branch name with degree name
        dsBranch = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON (B.BRANCHNO=CDB.BRANCHNO) LEFT JOIN ACD_COLLEGE_SCHEME_MAPPING CSM ON (CSM.COLLEGE_ID=CDB.COLLEGE_ID AND CSM.DEGREENO=CDB.DEGREENO and CSM.BRANCHNO=CDB.BRANCHNO)INNER JOIN ACD_DEGREE D ON D.DEGREENO=CDB.DEGREENO", "DISTINCT (CAST(CDB.DEGREENO AS NVARCHAR(20))+'$'+CAST(CDB.BRANCHNO AS NVARCHAR(20))) AS BRANCHNO, CDB.BRANCHNO AS BRANCHNO1", "B.LONGNAME + ' - ' + D.CODE AS BRANCHNAME, CSM.BRANCHNO AS BRANCHNO2", "ISNULL(B.BRANCHNO,0)>0 AND CDB.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND CDB.DEGREENO IN(" + degreeno + ")", "BRANCHNO1");
        if (dsBranch != null && dsBranch.Tables.Count > 0)
        {
            if (dsBranch.Tables[0].Rows.Count > 0)
            {
                cblBranch.Items.Clear();
                cblBranch.DataTextField = "BRANCHNAME";
                cblBranch.DataValueField = "BRANCHNO";
                cblBranch.DataSource = dsBranch.Tables[0];
                cblBranch.DataBind();
                divBranch.Visible = true;

                int i = 0;
                foreach (ListItem item in cblBranch.Items)
                {
                    if (dsBranch.Tables[0].Rows[i]["BRANCHNO2"].ToString() == item.Value.Split('$')[1])
                    {                                         
                        item.Selected = true;
                        branchno = branchno + item.Value.Split('$')[1] + ",";
                        degreebranch = degreebranch + item.Value + ",";
                        branchname = branchname + item.Text + "$";
                    }
                    else
                    {
                        item.Selected = false;
                    }
                    i++;
                }

                if (ViewState["degreeno"].ToString().Length > 0)
                {
                    if (branchno.Length > 0)
                    {
                        if (branchno.Substring(branchno.Length - 1).Contains(','))
                        {
                            branchno = branchno.Remove(branchno.Length - 1);
                        }

                        if (degreebranch.Substring(degreebranch.Length - 1).Contains(','))
                        {
                            degreebranch = degreebranch.Remove(degreebranch.Length - 1);
                        }

                        if (branchname.Substring(branchname.Length - 1).Contains('$'))
                        {
                            branchname = branchname.Remove(branchname.Length - 1);
                        }
                        ViewState["branchno"] = branchno;
                        ViewState["degreebranch"] = degreebranch;
                        ViewState["branchname"] = branchname;  
                    }
                    else
                    {
                        divScheme.Visible = false;
                    }
                    fillChecboxlistScheme(ViewState["degreeno"].ToString(), branchno);
                }
                else
                {
                    divBranch.Visible = false;
                    divScheme.Visible = false;
                }

            }
            else
            {
                cblBranch.DataSource = null;
                cblBranch.DataBind();
                divBranch.Visible = false;
                divScheme.Visible = false;
            }
        }
    }

    protected void cblBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (degreeno == string.Empty || degreeno==null)
        {
            degreeno = ViewState["degreeno"].ToString();
        }
      
        foreach (ListItem item in cblBranch.Items)
        {
            if (item.Selected)
            {
                branchno = branchno + item.Value.Split('$')[1] + ",";
                degreebranch = degreebranch + item.Value + ",";
                branchname = branchname + item.Text + "$";
            }
        }

        if (branchno == null)
        {
            divScheme.Visible = false;
        }

        if (degreeno.Length > 0)
        {
            if (branchno != null && branchno.Length > 0)
            {
                if (branchno.Substring(branchno.Length - 1).Contains(','))
                {
                    branchno = branchno.Remove(branchno.Length - 1);
                }

                if (degreebranch == null)
                {
                    degreebranch = ViewState["degreebranch"].ToString();
                }
                if (degreebranch.Substring(degreebranch.Length - 1).Contains(','))
                {
                    degreebranch = degreebranch.Remove(degreebranch.Length - 1);
                }

                if (branchname == null)
                {
                    branchname = ViewState["branchname"].ToString();
                }
                if (branchname.Substring(branchname.Length - 1).Contains('$'))
                {
                    branchname = branchname.Remove(branchname.Length - 1);
                }
                ViewState["branchno"] = branchno;
                ViewState["degreebranch"] = degreebranch;
                ViewState["branchname"] = branchname;  
                fillChecboxlistScheme(degreeno, branchno);
            }
            else
            {
                divScheme.Visible = false;
            }
        }
        else
        {
            divBranch.Visible = false;
            divScheme.Visible = false;
        }

        int count = 0;

        foreach (ListItem item in cblScheme.Items)
        {
            if (item.Selected)
            {
                count++;
            }
        }

        if (count > 0)
        {
            btnSubmit.Enabled = true;
        }
        else
        {
            btnSubmit.Enabled = false;
        }

        DataSet dsScheme = null;
        if (branchno != null && degreeno != null)
        {
            if (branchno == string.Empty)
            {
                dsScheme = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_SCHEME S ON (S.BRANCHNO=CDB.BRANCHNO AND S.DEGREENO=CDB.DEGREENO)", "(CAST(CDB.DEGREENO AS NVARCHAR(20))+'$'+CAST(CDB.BRANCHNO AS NVARCHAR(20))+'$'+CAST(S.SCHEMENO AS NVARCHAR(20))) AS SCHEMENO, S.SCHEMENO AS SCHEMENO1", "S.SCHEMENAME", "ISNULL(S.SCHEMENO,0)>0 AND CDB.COLLEGE_ID=1 AND CDB.DEGREENO IN(" + degreeno + ")", "CDB.COLLEGE_ID, CDB.DEGREENO, CDB.BRANCHNO");
            }
            else
            {
                dsScheme = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_SCHEME S ON (S.BRANCHNO=CDB.BRANCHNO AND S.DEGREENO=CDB.DEGREENO)", "(CAST(CDB.DEGREENO AS NVARCHAR(20))+'$'+CAST(CDB.BRANCHNO AS NVARCHAR(20))+'$'+CAST(S.SCHEMENO AS NVARCHAR(20))) AS SCHEMENO, S.SCHEMENO AS SCHEMENO1", "S.SCHEMENAME", "ISNULL(S.SCHEMENO,0)>0 AND CDB.COLLEGE_ID=1 AND CDB.DEGREENO IN(" + degreeno + ") AND CDB.BRANCHNO IN(" + branchno + ")", "CDB.COLLEGE_ID, CDB.DEGREENO, CDB.BRANCHNO");
            }

            if (dsScheme.Tables.Count > 0)
            {
                if (dsScheme.Tables[0].Rows.Count <= 0)
                {
                    btnSubmit.Enabled = true;
                }
                else
                {
                    btnSubmit.Enabled = false;
                }
            }
        }

        int countSelectedBranch = 0;

        if (cblScheme.Items.Count <= 0)
        {
            if (cblBranch.Items.Count > 0)
            {
                foreach (ListItem item in cblDegree.Items)
                {
                    if (item.Selected)
                    {
                        count++;
                    }
                }

                foreach (ListItem item in cblBranch.Items)
                {
                    if (item.Selected)
                    {
                        countSelectedBranch++;
                    }
                }
            }
        }

        if (count > 0 && countSelectedBranch>0)
        {
            btnSubmit.Enabled = true;
        }
        else
        {
            btnSubmit.Enabled = false;
        }
    }

    public void fillChecboxlistScheme(string degreeno, string branchno)
    {
        Session["dsScheme"] = null;
        schemeno = string.Empty;
        ViewState["schemeno"] = "";
        ViewState["degreebranchscheme"] = "";

        DataSet dsScheme = null;

        if (branchno == string.Empty)
        {
            dsScheme = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH CDB LEFT JOIN ACD_SCHEME S ON (S.BRANCHNO=CDB.BRANCHNO AND S.DEGREENO=CDB.DEGREENO and s.COLLEGE_ID=CDB.COLLEGE_ID) LEFT JOIN ACD_COLLEGE_SCHEME_MAPPING CSM ON (CSM.SCHEMENO=S.SCHEMENO)", "DISTINCT (CAST(CDB.DEGREENO AS NVARCHAR(20))+'$'+CAST(CDB.BRANCHNO AS NVARCHAR(20))+'$'+CAST(ISNULL(S.SCHEMENO,0) AS NVARCHAR(20))) AS SCHEMENO, ISNULL(S.SCHEMENO,0) AS SCHEMENO1", "S.SCHEMENAME, ISNULL(CSM.SCHEMENO,0) AS SCHEMENO2, CDB.COLLEGE_ID, CDB.DEGREENO, CDB.BRANCHNO", "CDB.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND CDB.DEGREENO IN(" + degreeno + ")", "CDB.COLLEGE_ID, CDB.DEGREENO, CDB.BRANCHNO");  // changes by Harshal on 08-10-2021
        }
        else
        {
            dsScheme = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH CDB LEFT JOIN ACD_SCHEME S ON (S.BRANCHNO=CDB.BRANCHNO AND S.DEGREENO=CDB.DEGREENO and S.DEPTNO=CDB.DEPTNO and s.COLLEGE_ID=CDB.COLLEGE_ID) LEFT JOIN ACD_COLLEGE_SCHEME_MAPPING CSM ON (CSM.SCHEMENO=S.SCHEMENO)", "DISTINCT (CAST(CDB.DEGREENO AS NVARCHAR(20))+'$'+CAST(CDB.BRANCHNO AS NVARCHAR(20))+'$'+CAST(ISNULL(S.SCHEMENO,0) AS NVARCHAR(20))) AS SCHEMENO, ISNULL(S.SCHEMENO,0) AS SCHEMENO1", "S.SCHEMENAME, ISNULL(CSM.SCHEMENO,0) AS SCHEMENO2, CDB.COLLEGE_ID, CDB.DEGREENO, CDB.BRANCHNO", "CDB.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND CDB.DEGREENO IN(" + degreeno + ") AND CDB.BRANCHNO IN(" + branchno + ")", "CDB.COLLEGE_ID, CDB.DEGREENO, CDB.BRANCHNO"); // changes by Harshal on 08-10-2021
        }

        if (dsScheme != null && dsScheme.Tables.Count > 0)
        {
            if (dsScheme.Tables[0].Rows.Count > 0)
            {
                cblScheme.Items.Clear();
                cblScheme.DataTextField = "SCHEMENAME";
                cblScheme.DataValueField = "SCHEMENO";
                cblScheme.DataSource = dsScheme.Tables[0];
                cblScheme.DataBind();
                divScheme.Visible = true;
                Session["dsScheme"] = dsScheme.Tables[0];

                int i = 0;
                int compare = 0;
                foreach (ListItem item in cblScheme.Items)
                {
                    if (dsScheme.Tables[0].Rows[i]["SCHEMENO2"].ToString() == item.Value.Split('$')[2])
                    {
                        count++;
                        item.Selected = true;
                        schemeno = schemeno + item.Value.Split('$')[2] + ",";
                        degreebranchscheme = degreebranchscheme + item.Value + ",";
                        schemename = schemename + item.Text + "$";

                        if (dsScheme.Tables[0].Rows[i]["SCHEMENAME"].ToString() == string.Empty)
                        {
                            compare++;
                            item.Attributes.Add("style", "display:none");
                        }
                    }
                    else
                    {
                        item.Selected = false;
                    }
                    i++;
                }

                if (compare == dsScheme.Tables[0].Rows.Count)
                {
                    divScheme.Visible = false;
                }
                else
                {
                    divScheme.Visible = true;
                }

                if (ViewState["degreeno"].ToString().Length > 0)
                {
                    if (ViewState["branchno"].ToString().Length > 0)
                    {
                        if (schemeno.Length > 0)
                        {
                            if (schemeno.Substring(schemeno.Length - 1).Contains(','))
                            {
                                schemeno = schemeno.Remove(schemeno.Length - 1);
                            }
                            if (schemename.Substring(schemename.Length - 1).Contains('$'))
                            {
                                schemename = schemename.Remove(schemename.Length - 1);
                            }
                            if (degreebranchscheme.Substring(degreebranchscheme.Length - 1).Contains(','))
                            {
                                degreebranchscheme = degreebranchscheme.Remove(degreebranchscheme.Length - 1);
                            }

                            ViewState["schemeno"] = schemeno;
                            ViewState["schemename"] = schemename;
                            ViewState["degreebranchscheme"] = degreebranchscheme;
                        }

                        if (count > 0)
                        {
                            btnSubmit.Enabled = true;
                        }
                        else
                        {
                            btnSubmit.Enabled = false;
                        }

                    }
                    else
                    {
                        divScheme.Visible = false;
                    }
                }
                else
                {
                    divBranch.Visible = false;
                    divScheme.Visible = false;
                }

            }
            else
            {
                cblScheme.DataSource = null;
                cblScheme.DataBind();
                divScheme.Visible = false;
            }
        }
    }

    protected void cblScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        count = 0;

        foreach (ListItem item in cblScheme.Items)
        {
            if (item.Selected)
            {
                count++;
                schemeno = schemeno + item.Value.Split('$')[2] + ",";
                degreebranchscheme = degreebranchscheme + item.Value + ",";
                schemename = schemename + item.Text + "$";

                if (item.Text == string.Empty)
                {
                    item.Attributes.Add("style", "display:none");
                }
            }
        }

        if (schemeno.Length > 0)
        {
            if (schemeno.Substring(schemeno.Length - 1).Contains(','))
            {
                schemeno = schemeno.Remove(schemeno.Length - 1);
            }
            if (schemename.Substring(schemename.Length - 1).Contains('$'))
            {
                schemename = schemename.Remove(schemename.Length - 1);
            }
            if (degreebranchscheme.Substring(degreebranchscheme.Length - 1).Contains(','))
            {
                degreebranchscheme = degreebranchscheme.Remove(degreebranchscheme.Length - 1);
            }

            ViewState["schemeno"] = schemeno;
            ViewState["schemename"] = schemename;
            ViewState["degreebranchscheme"] = degreebranchscheme;
        }

        if (count > 0)
        {
            btnSubmit.Enabled = true;
        }
        else
        {
            btnSubmit.Enabled = false;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dsScheme = (DataTable)Session["dsScheme"];
        DataTable dt11 = (DataTable)dsScheme;

        DataTable dtResult = new DataTable();
        dtResult.Columns.AddRange(new DataColumn[6] 
        { 
            new DataColumn("COLLEGE_ID", typeof(int)),
            new DataColumn("COL_SCHEME_NAME", typeof(string)),
            new DataColumn("DEGREENO",typeof(int)),
            new DataColumn("BRANCHNO",typeof(int)), 
            new DataColumn("SCHEMENO",typeof(int)), 
            new DataColumn("CREATED_BY",typeof(int)) 
        });

        CustomStatus cs = 0;
        int degreeno = 0;
        int branchno = 0;
        int schemeno = 0;

        string schemenames = string.Empty;
        string[] schemenamevalues = schemenames.Split('$');

        string degreebranches = string.Empty;
        string degreebrancheschemes = string.Empty;

        string degreebranch = ViewState["degreebranch"].ToString();
        string degreebranchscheme = ViewState["degreebranchscheme"].ToString();

        string degree = ViewState["degreeno"].ToString();
        string[] degreevalues = degree.Split(',');

        string branch = ViewState["branchno"].ToString();
        string[] branchvalues = branch.Split(',');

        string scheme = ViewState["schemeno"].ToString();
        string[] schemevalues = scheme.Split(',');

        string branchnames = ViewState["branchname"].ToString();
        string[] branchnamevalues = branchnames.Split('$');

        if (degreebranchscheme != string.Empty)
        {
            schemenames = ViewState["schemename"].ToString();
            schemenamevalues = schemenames.Split('$');
        }

        for (int i = 0; i < degreevalues.Length; i++)
        {
            degreeno = 0;
            degreevalues[i] = degreevalues[i].Trim();
            degreeno = Convert.ToInt32(degreevalues[i].ToString());

            for (int j = 0; j < branchvalues.Length; j++)
            {
                branchno = 0;
                degreeOfBranch = string.Empty;

                branchvalues[j] = branchvalues[j].Trim();
                branchno = Convert.ToInt32(branchvalues[j].ToString());

                branchname = branchnames.ToString();
                branchnamevalues[j] = branchnamevalues[j].Trim();
                branchname = branchnamevalues[j].ToString();

                degreebranches = string.Empty;
                string[] db = degreebranch.Split(',');
                degreebranches = db[j].ToString();

                degreeOfBranch = degreebranches.Split('$')[0].Trim();

                for (int k = 0; k < schemevalues.Length; k++)
                {
                    schemeno = 0;
                    schemevalues[k] = schemevalues[k].Trim();
                    schemeno = Convert.ToInt32(schemevalues[k].ToString() == string.Empty ? "0" : schemevalues[k].ToString());

                    schemename = string.Empty;
                    schemenamevalues[k] = schemenamevalues[k].Trim();
                    schemename = schemenamevalues[k].ToString();

                    degreebrancheschemes = string.Empty;
                    string[] dbs = degreebranchscheme.Split(',');
                    degreebrancheschemes = dbs[k].ToString();

                    if (degreebrancheschemes != string.Empty)
                    {
                        branchOfScheme = degreebrancheschemes.Split('$')[1].Trim();
                    }

                    //string colscheme = (ddlCollege.SelectedItem.Text + " - " + (schemename.ToString()==string.Empty?branchname.ToString():schemename.ToString()));

                    /////////////********* ADDED BY NARESH BEERLA TO SAVE SCHEME WITH COLLEGE SHORT NAME ON 29122020 (ANKUSH SIR) *********//////////////////

                    string ShortColName = objCommon.LookUp("ACD_COLLEGE_MASTER", "SHORT_NAME", "COLLEGE_ID=" + ddlCollege.SelectedValue);

                    string colscheme = (ShortColName + " - " + (schemename.ToString() == string.Empty ? branchname.ToString() : schemename.ToString()));

                    /////////////********* ADDED BY NARESH BEERLA TO SAVE SCHEME WITH COLLEGE SHORT NAME ON 29122020 (ANKUSH SIR) *********//////////////////

                    if (Convert.ToString(degreeno) == degreeOfBranch)
                    {
                        if (Convert.ToString(branchno) == (branchOfScheme == string.Empty ? Convert.ToString(branchno) : branchOfScheme))
                        {
                            string _sqlWh = "DEGREENO=" + degreeno.ToString() + " AND BRANCHNO=" + branchno.ToString() + " AND SCHEMENO1=" + schemeno.ToString();
                            string _sqlWhere = _sqlWh.ToString();
                            string _sqlOrder = "degreeno";

                            DataRow[] filtered_rows = dt11.Select(_sqlWhere, _sqlOrder);
                            if (filtered_rows.Length > 0)
                            {
                                DataTable dr1 = dt11.Select(_sqlWhere, _sqlOrder).CopyToDataTable();

                                if (dr1.Rows.Count > 0)
                                {
                                    dtResult.Rows.Add(Convert.ToInt32(ddlCollege.SelectedValue), colscheme, degreeno, branchno, schemeno, Convert.ToInt32(Session["userno"].ToString()));
                                }
                            }
                        }
                    }
                }
            }
        }

        cs = (CustomStatus)objConfig.AddCollegeSchemeConfig(dtResult);

        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(updPnl, "Record Saved Successfully!!", this.Page);
            Clear();
        }

        objCommon.DisplayMessage(updPnl, "Record Saved Successfully!!", this.Page);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        ddlCollege.SelectedIndex = 0;
        cblDegree.Items.Clear();
        cblBranch.Items.Clear();
        cblScheme.Items.Clear();
        divDegree.Visible = false;
        divBranch.Visible = false;
        divScheme.Visible = false;
        btnSubmit.Enabled = false;
    }

}








