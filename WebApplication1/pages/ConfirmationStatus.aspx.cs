
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;

public partial class ACADEMIC_ConfirmationStatus : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FeeCollectionController objFeeControl = new FeeCollectionController();
    GridView GV = new GridView();
    string ExcelStatus = "";


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
                    //  this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }

                    ViewState["slotno"] = "0";

                    ViewState["action"] = "add";
                    PopolateDropDownList();
                    //BindListView();
                }
                //BindListView();
            }
            // Added by Abhinay Lad on 07-11-2020 for Excel File Export Enable/Disable Purpose.
            ExcelStatus = objCommon.LookUp("reff", "table_btn_setting", "").Split(',')[1];
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_TimeTableSlot.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region User-Defined Methods
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TimeTableSlot.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TimeTableSlot.aspx");
        }
    }

    private void Clear()
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        //  ViewState["slotno"] = "0";
    }

    private void PopolateDropDownList()
    {
        objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME DESC");
        objCommon.FillDropDownList(ddlStatus, "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO>0", "IDTYPENO"); //Fill IdType
    }
    #endregion
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ////  objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "", "A.DEGREENO");
        if (ddlAdmBatch.SelectedIndex > 0)
        {
            DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "", "A.DEGREENO");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlDegree.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
            }
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Please select Admission Batch", this.Page);
            ddlAdmBatch.Focus();
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
     //   ViewState["degreeno"] = GetDegree();

        string deg = GetDegree();
        deg = deg.Replace('$', ',');

        //ViewState["DegreeNo"] = deg;
        //string[] DegreeNo = deg.Split(',');


        DataSet ds1 = objCommon.FillDropDown("ACD_BRANCH a INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON a.BRANCHNO=B.BRANCHNO INNER JOIN ACD_DEGREE DE ON (DE.DEGREENO = B.DEGREENO)", "B.BRANCHNO", "DE.DEGREENAME+'-'+A.LONGNAME AS LONGNAME", "B.DEGREENO in(" + deg + ")", "B.BRANCHNO");
        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
        {
            ddlBranch.Items.Add(new ListItem(Convert.ToString(ds1.Tables[0].Rows[i][1]), Convert.ToString(ds1.Tables[0].Rows[i][0])));
        }     

    }

    private string GetDegree()
    {
        string degreeNo = "";
        string degreeno = string.Empty;
        //  degreeNo = hdndegreeno.Value;
        int x = 0;
        pnlFeeTable.Update();
        foreach (ListItem item in ddlDegree.Items)
        {
            if (item.Selected == true)
            {
                degreeNo += item.Value + '$';
                x = 1;
            }
        }
        if (x == 0)
        {
            degreeNo = "0";
        }

        if (degreeNo != "0")
        {
            degreeno = degreeNo.Substring(0, degreeNo.Length - 1);
        }
        else
        {
            degreeno = degreeNo;
        }

        //  degreeno = degreeNo.Substring(0, degreeNo.Length - 1);
        if (degreeno != "")
        {
            string[] degValue = degreeno.Split('$');

        }
        // degreeno = degreeno.Substring(0, degreeno.Length - 1);
        //}
        return degreeno;
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        string branchno = GetBranch();
    }

    private string GetBranch()
    {
        string branchNo = "";
        string branchno = string.Empty;
        int X = 0;
        //  degreeNo = hdndegreeno.Value;
        pnlFeeTable.Update();
        foreach (ListItem item in ddlBranch.Items)
        {
            if (item.Selected == true)
            {
                branchNo += item.Value + '$';
                X = 1;
            }
        }

        if (X == 0)
        {
            branchNo = "0";
        }

        if (branchNo != "0")
        {
            branchno = branchNo.Substring(0, branchNo.Length - 1);
        }
        else
        {
            branchno = branchNo;
        }
        if (branchno != "")
        {
            string[] bValue = branchno.Split('$');

        }
        // degreeno = degreeno.Substring(0, degreeno.Length - 1);
        //}
        return branchno;

    }
        
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
        lvHday.Visible = true;
    }
      
  
    private void BindListView()
    {
        try
        {
            DataSet ds = null;

            string degreeno = GetDegree();
            string branchno = GetBranch();
            int status=Convert.ToInt32(ddlStatus.SelectedValue);
            ds = objFeeControl.GetStudentConfirmDetails(Convert.ToInt32(ddlAdmBatch.SelectedValue), degreeno, branchno ,status);

            lvHday.DataSource = ds;
            lvHday.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_HolidayMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (ExcelStatus == "1")
        {
            DataSet ds = null;
            string degreeno = GetDegree();
            string branchno = GetBranch();
            int status = Convert.ToInt32(ddlStatus.SelectedValue);

            ds = objFeeControl.GetStudentConfirmDetails(Convert.ToInt32(ddlAdmBatch.SelectedValue), degreeno, branchno, status);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds;
                GV.DataBind();
                this.CallExcel();
            }
            else
            {
                objCommon.DisplayMessage("Record Not Found!!", this.Page);
                return;
            }
        }
    }

    private void CallExcel()
    {
        string attachment = "attachment; filename=AdmissionStatusReport_" + ddlAdmBatch.SelectedItem.Text + ".xls";

        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.MS-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GV.HeaderRow.Style.Add("background-color", "#e3ac9a");

        GV.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }        
}
