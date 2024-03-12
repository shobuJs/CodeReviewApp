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
using System.Text;

public partial class ACADEMIC_admissionprocess : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    HtmlTable tbl = null;

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
                CheckPageAuthorization();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                else
                    lblHelp.Text = "No Help Added";

                txtFromDate.Text = Common.reportStartDate.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
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
                Response.Redirect("~/notauthorized.aspx?page=createhelp.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createhelp.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (rbAllYear.Checked == true)
            FillAllYearTable();
        else
            FillFirstYearTable();

        lblStatus.Text = string.Empty;

        //start the timer
        tmList.Enabled = true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session["temptable"] = null;
        lblStatus.Text = string.Empty;
        //stop the timer if error occurs
        tmList.Enabled = false;
        txtFromDate.Text = Common.reportStartDate.ToString("dd/MM/yyyy");
        txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
    }

    private void FillAllYearTable()
    {
        string ugpgot = string.Empty;

        if (rbUG.Checked == true)
            ugpgot = "UG";
        else if (rbPG.Checked == true)
            ugpgot = "PG";
        else if (rbOT.Checked == true)
            ugpgot = "OT";

        tbl = new HtmlTable();
        tbl.CellSpacing = 1;
        tbl.CellPadding = 1;
        tbl.Border = 1;
        tbl.Align = "left";
        tbl.Attributes.Add("class", "vista-grid .datatable");

        try
        {
            //fill cells
            DataTable dtHead = objCommon.FillDropDown("acd_category", "categoryno", "category", string.Empty, "categoryno").Tables[0];
            HtmlTableRow headRow = new HtmlTableRow();
            headRow.Attributes.Add("class", "gv_header");
            //fill cells (category)
            foreach (DataRow row in dtHead.Rows)
            {
                HtmlTableCell cell = new HtmlTableCell();
                cell.Width = "10%";
                cell.InnerText = row["category"].ToString();
                headRow.Cells.Add(cell);
            }
            HtmlTableCell cellVTot = new HtmlTableCell();
            cellVTot.Width = "10%";

            cellVTot.InnerText = "Total";
            headRow.Cells.Add(cellVTot);

            tbl.Rows.Add(headRow);

            //fill rows (branch)
            DataTable dtBranch = objCommon.FillDropDown("acd_branch", "branchno", "shortname", string.Empty, "branchno").Tables[0];

            foreach (DataRow row in dtBranch.Rows)
            {
                HtmlTableRow dataRow = new HtmlTableRow();
                dataRow.Align = "left";
                dataRow.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFFAA'");
                dataRow.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");

                HtmlTableCell cell0 = new HtmlTableCell();

                cell0.InnerText = row["shortname"].ToString();
                cell0.Attributes.Add("class", "gv_colfixed");

                dataRow.Cells.Add(cell0);

                for (int i = 0; i < headRow.Cells.Count - 1; i++)
                {
                    HtmlTableCell cell1 = new HtmlTableCell();
                    cell1.InnerText = "0";

                    cell1.Attributes.Add("class", "tbl_col");
                    dataRow.Cells.Add(cell1);
                }
                tbl.Rows.Add(dataRow);
            }

            StudentController objSC = new StudentController();
            SqlDataReader dr = objSC.GetAllStudentAdmitted(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), ugpgot);
            dr.Close();

            //total row
            HtmlTableRow rowTot = new HtmlTableRow();
            rowTot.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFFAA'");
            rowTot.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");

            HtmlTableCell cellVtot = new HtmlTableCell();
            cellVtot.InnerText = "Total";
            cellVtot.Attributes.Add("class", "gv_colfixed");

            rowTot.Cells.Add(cellVtot);

            for (int i = 0; i < headRow.Cells.Count - 1; i++)
            {
                HtmlTableCell cell1 = new HtmlTableCell();
                cell1.InnerText = "0";

                cell1.Attributes.Add("class", "tbl_col");
                rowTot.Cells.Add(cell1);
            }
            tbl.Rows.Add(rowTot);

            //Horizontal Total
            HorizTotal();  //check

            VertTotal();   //check

            //add table to placeholder
            phList.Controls.Add(tbl);

            //for downloading
            Session["temptable"] = tbl;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_admissionprocess.FillTable-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillFirstYearTable()
    {
        StudentController objSC = new StudentController();
        string ugpgot = string.Empty;

        if (rbUG.Checked == true)
            ugpgot = "UG";
        else if (rbPG.Checked == true)
            ugpgot = "PG";
        else if (rbOT.Checked == true)
            ugpgot = "OT";

        tbl = new HtmlTable();
        tbl.CellPadding = 0;
        tbl.CellSpacing = 0;
        tbl.Attributes.Add("class", "vista-grid .datatable");
        tbl.CellSpacing = 1;
        tbl.CellPadding = 1;
        tbl.Border = 1;
        tbl.Align = "left";
        try
        {
            //fill cells
            DataTable dtHead = objCommon.FillDropDown("acd_category", "categoryno", "category", string.Empty, "categoryno").Tables[0];
            HtmlTableRow headRow = new HtmlTableRow();
            headRow.Attributes.Add("class", "gv_header");

            //fill cells (category)
            foreach (DataRow row in dtHead.Rows)
            {
                HtmlTableCell cell = new HtmlTableCell();
                cell.Width = "5%";
                cell.InnerText = row["category"].ToString();
                headRow.Cells.Add(cell);
            }
            HtmlTableCell cellVTot = new HtmlTableCell();
            cellVTot.Width = "5%";
            cellVTot.InnerText = "Total";
            headRow.Cells.Add(cellVTot);

            tbl.Rows.Add(headRow);

            //fill rows (branch)
            DataTable dtBranch = objCommon.FillDropDown("acd_branch", "branchno", "shortname", string.Empty, "branchno").Tables[0];

            foreach (DataRow row in dtBranch.Rows)
            {
                HtmlTableRow dataRow = new HtmlTableRow();
                dataRow.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFFAA'");
                dataRow.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");

                HtmlTableCell cell0 = new HtmlTableCell();
                cell0.InnerText = row["shortname"].ToString();
                cell0.Attributes.Add("class", "gv_colfixed");

                dataRow.Cells.Add(cell0);

                foreach (DataRow catrow in dtHead.Rows)
                {
                    HtmlTableCell cell1 = new HtmlTableCell();
                    cell1.InnerText = "0";

                    cell1.Attributes.Add("class", "tbl_col");

                    SqlDataReader dr = objSC.GetFirstYrStudentAdmitted(Convert.ToInt32(catrow["categoryno"]), Convert.ToInt32(row["branchno"]), row["shortname"].ToString(), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), ugpgot);
                    if (dr.Read())
                        cell1.InnerText = dr["total"].ToString();

                    dr.Close();

                    dataRow.Cells.Add(cell1);
                }
                tbl.Rows.Add(dataRow);
            }


            //total row
            HtmlTableRow rowTot = new HtmlTableRow();
            rowTot.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFFAA'");
            rowTot.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");

            HtmlTableCell cellVtot = new HtmlTableCell();
            cellVtot.InnerText = "Total";
            cellVtot.Attributes.Add("class", "gv_colfixed");

            rowTot.Cells.Add(cellVtot);

            for (int i = 0; i < headRow.Cells.Count - 1; i++)
            {
                HtmlTableCell cell1 = new HtmlTableCell();
                cell1.InnerText = "0";

                cell1.Attributes.Add("class", "tbl_col");
                rowTot.Cells.Add(cell1);
            }
            tbl.Rows.Add(rowTot);

            //Horizontal Total
            HorizTotal();  //check


            //add table to placeholder
            phList.Controls.Add(tbl);

            //for downloading
            Session["temptable"] = tbl;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_admissionprocess.FillTable-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private int GetRowNo(string item)
    {
        for (int i = 1; i < tbl.Rows.Count - 1; i++)
        {
            if (tbl.Rows[i].Cells[0].InnerText.ToLower().Equals(item.ToLower()))
                return i;
        }
        return 0;
    }

    private int GetColNo(string item)
    {
        for (int i = 0; i < tbl.Rows[0].Cells.Count - 1; i++)
        {
            if (tbl.Rows[0].Cells[i].InnerText.ToLower().Equals(item.ToLower()))
                return i;
        }
        return 0;
    }

    private void HorizTotal()
    {
        int tot = 0;
        for (int i = 1; i < tbl.Rows.Count - 1; i++)
        {
            for (int j = 1; j < tbl.Rows[i].Cells.Count - 2; j++)
            {
                tot += int.Parse(tbl.Rows[i].Cells[j].InnerText);
            }
            tbl.Rows[i].Cells[tbl.Rows[i].Cells.Count - 1].InnerText = tot.ToString();
            tot = 0;
        }
    }

    private void VertTotal()
    {
        int tot = 0;
        int gtot = 0;

        for (int j = 1; j < tbl.Rows[0].Cells.Count; j++)
        {
            for (int i = 1; i < tbl.Rows.Count; i++)
            {
                tot += int.Parse(tbl.Rows[i].Cells[j].InnerText);
            }
            tbl.Rows[tbl.Rows.Count - 1].Cells[j].InnerText = tot.ToString();
            tot = 0;
        }
    }
    public string HtmlTable2ExcelString(HtmlTable dt)
    {
        StringBuilder sbTop = new StringBuilder();
        sbTop.Append("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" ");
        sbTop.Append("xmlns=\"http://www.w3.org/TR/REC-html40\"><head><meta http-equiv=Content-Type content=\"text/html; charset=windows-1252\">");
        sbTop.Append("<meta name=ProgId content=Excel.Sheet><meta name=Generator content=\"Microsoft Excel 9\"><!--[if gte mso 9]>");
        sbTop.Append("<xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>SHEET1</x:Name><x:WorksheetOptions>");
        sbTop.Append("<x:Selected/><x:ProtectContents>False</x:ProtectContents><x:ProtectObjects>False</x:ProtectObjects>");
        sbTop.Append("<x:ProtectScenarios>False</x:ProtectScenarios></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets>");
        sbTop.Append("<x:ProtectStructure>False</x:ProtectStructure><x:ProtectWindows>False</x:ProtectWindows></x:ExcelWorkbook></xml>");
        sbTop.Append("<![endif]--></head><body><table>");
        string bottom = "</table></body></html>";

        StringBuilder sb = new StringBuilder();

        //All Items
        for (int x = 0; x < dt.Rows.Count; x++)
        {
            sb.Append("<tr>");
            for (int i = 0; i < dt.Rows[x].Cells.Count; i++)
            {
                sb.Append("<td>" + dt.Rows[x].Cells[i].InnerText + "</td>");
            }
            sb.Append("</tr>");
        }

        string SSxml = sbTop.ToString() + sb.ToString() + bottom;

        return SSxml;
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        if (Session["temptable"] != null)
        {
            HtmlTable ht = Session["temptable"] as HtmlTable;
            string strBody = HtmlTable2ExcelString(ht);

            Response.AppendHeader("Content-Type", "application/vnd.ms-excel");
            Response.AppendHeader("Content-disposition", "attachment; filename=admissionlist.xls");
            Response.Clear();
            Response.Write(strBody);
            Response.End();
        }
        else
            lblStatus.Text = "There is no data available.";
    }

    protected void tmList_Tick(object sender, EventArgs e)
    {
        try
        {
            if (rbAllYear.Checked == true)
                FillAllYearTable();
            else
                FillFirstYearTable();

            lblStatus.Text = string.Empty;

            System.Threading.Thread.Sleep(2000);
        }
        catch (Exception ex)
        {
            //stop the timer if error occurs
            tmList.Enabled = false;
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_admissionprocess.FillTable-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
