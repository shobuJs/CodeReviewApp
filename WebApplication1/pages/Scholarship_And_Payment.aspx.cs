using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI;

using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System;
using System.Data;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;


public partial class EXAMINATION_Projects_Scholarship_And_Payment : System.Web.UI.Page
{
    SessionController objSC = new SessionController();
   // IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession = new IITMS.UAIMS.BusinessLayer.BusinessEntities.Session();

    Common objCommon = new Common();
    FeeCollectionController feeController = new FeeCollectionController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Scholarship objScholarship = new Scholarship();
    // ScholarshipController objSchCtrl = new ScholarshipController();
    StudentController objSt = new StudentController();

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
                if (Session["usertype"].ToString() == "1" && Session["username"].ToString().ToUpper().Contains("PRINCIPAL"))
                {
                    // divShow.Visible=true;
                }
                else
                {
                    // divShow.Visible=false;
                }

              
                DropDownBindConsession();
                DropDownBindSchollarship();
                BindListViewIntalla();
                SetInitialRow();
                ListViewBindRule();
                dropwown();
                ViewState["IDNO"] = null;
                //Set the Page Title
                Page.Title = objCommon.LookUp("reff", "collegename", string.Empty);

                objCommon.FillDropDownList(ddlDecideMode, "ACD_MODE", "SRNO", "MODE_NAME", "SRNO>0", "SRNO");
               objCommon.FillDropDownList(ddlGpa, "ACD_SCHOLARSHIP_RULE", "RULENO", "REQUIREDGPA", "RULENO>0", "RULENO");

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
            //Populate the Drop Down Lists
        }
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Scholarship_And_Payment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Scholarship_And_Payment.aspx");
        }
    }

    #region Installment
   
 

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        //dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("INSTALMENT_NO", typeof(string)));
        dt.Columns.Add(new DataColumn("INSTALL_AMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("DUE_DATE", typeof(string)));
        dt.Columns.Add(new DataColumn("PERCENTS", typeof(string)));
        dt.Columns.Add(new DataColumn("Persentage", typeof(string)));
        dt.Columns.Add(new DataColumn("Waived", typeof(string)));
        dt.Columns.Add(new DataColumn("ExtraDiscount", typeof(string)));

        dt.Columns.Add(new DataColumn("ADDITIONAL", typeof(string)));
        dt.Columns.Add(new DataColumn("TOTAL_PAYABLE", typeof(string)));
        dt.Columns.Add(new DataColumn("RECON_STATUS", typeof(string)));
        dr = dt.NewRow();
        // dr["RowNumber"] = 1;
        dr["INSTALMENT_NO"] = 1;
        dr["INSTALL_AMOUNT"] = string.Empty;
        dr["DUE_DATE"] = string.Empty;
        dr["PERCENTS"] = 0;
        dr["Persentage"] = string.Empty;
        dr["Waived"] = string.Empty;
        dr["ADDITIONAL"] = string.Empty;
        dr["ExtraDiscount"] = 0;
        dr["TOTAL_PAYABLE"] = string.Empty;
        dr["RECON_STATUS"] = 0;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;
        foreach (GridViewRow item in grdinstalment.Rows)
        {
            DropDownList ddPercent = item.FindControl("ddPercent") as DropDownList;
            objCommon.FillDropDownList(ddPercent, "ACD_DISCOUNT", "DISC_NO", "DISCOUNT", "", "DISC_NO");
        }
        grdinstalment.DataSource = dt;
        grdinstalment.DataBind();
    }
   
    private void SetPreviousDataPercent()
    {
        foreach (GridViewRow item in grdinstalment.Rows)
        {
            DropDownList ddPercent = item.FindControl("ddPercent") as DropDownList;
            objCommon.FillDropDownList(ddPercent, "ACD_DISCOUNT", "DISC_NO", "DISCOUNT", "", "DISC_NO");
        }
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                   
                  //  RadioButtonList rdbWaf = (RadioButtonList)grdinstalment.Rows[rowIndex].Cells[3].FindControl("rdbFilter");
                    DropDownList box3 = (DropDownList)grdinstalment.Rows[rowIndex].Cells[3].FindControl("ddPercent");
                    DropDownList ExtraDiscount = (DropDownList)grdinstalment.Rows[rowIndex].Cells[3].FindControl("ddExtraDiscounty");
                    RadioButtonList rdbWaivedOff = (RadioButtonList)grdinstalment.Rows[rowIndex].Cells[5].FindControl("rdbWaivedOff");

                   // rdbWaf.Text = dt.Rows[i]["Waivedoff"].ToString();
                    rdbWaivedOff.Text = dt.Rows[i]["Waived"].ToString();

                    box3.Text = dt.Rows[i]["PERCENTS"].ToString();
                    ExtraDiscount.Text = dt.Rows[i]["ExtraDiscount"].ToString();
                    rowIndex++;
                }
            }
        }
    }

    private void AddNewRowToGrid()
    {
     
        int rowIndex = 0;
        int recon;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                DataTable dtNewTable = new DataTable();
                //dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("INSTALMENT_NO", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("INSTALL_AMOUNT", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("DUE_DATE", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("PERCENTS", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("Persentage", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("Waived", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("ADDITIONAL", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("ExtraDiscount", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("TOTAL_PAYABLE", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("RECON_STATUS", typeof(string)));

                
             //   RadioButtonList rdbWaivedss = (RadioButtonList)grdinstalment.Rows[rowIndex].Cells[3].FindControl("rdbFilter");
               
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    Label rdbWaived = (Label)grdinstalment.Rows[rowIndex].Cells[0].FindControl("lblPersent");
                    TextBox box1 = (TextBox)grdinstalment.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                    TextBox box2 = (TextBox)grdinstalment.Rows[rowIndex].Cells[2].FindControl("txtDueDate");
                    //DropDownList ExtraDiscount = (DropDownList)grdinstalment.Rows[rowIndex].Cells[3].FindControl("ddExtraInsertFeesConcessioy");
                    DropDownList ExtraDiscount = (DropDownList)grdinstalment.Rows[rowIndex].Cells[3].FindControl("ddExtraDiscounty");                    
                    DropDownList box4 = (DropDownList)grdinstalment.Rows[rowIndex].Cells[4].FindControl("ddPercent");
                    TextBox box3 = (TextBox)grdinstalment.Rows[rowIndex].Cells[5].FindControl("txtAdditionalCharge");
                    
                    TextBox TotalPayable = (TextBox)grdinstalment.Rows[rowIndex].Cells[6].FindControl("txtTotalPayble");
                    
                    RadioButtonList rdbWaivedOff = (RadioButtonList)grdinstalment.Rows[rowIndex].Cells[7].FindControl("rdbWaivedOff");
                    string percent=string.Empty;
                     recon = 1;
                     //if (rdbWaived.Text == "1" && box4.Text == "0")
                     //{
                     //   percent = "1";
                     //}
                     if (box1.Text.Trim() != string.Empty && box2.Text.Trim() != string.Empty && percent.ToString() != "1" && box3.Text != string.Empty)
                    {
                        drCurrentRow = dtNewTable.NewRow();

                        //drCurrentRow["RowNumber"] = i + 1;
                        drCurrentRow["INSTALMENT_NO"] = i;
                        drCurrentRow["INSTALL_AMOUNT"] = box1.Text;
                        drCurrentRow["DUE_DATE"] = box2.Text;

                        //drCurrentRow["Persentage"] = rdbWaived.Text;
                        drCurrentRow["Waived"] = rdbWaivedOff.Text;
                        drCurrentRow["ExtraDiscount"] = ExtraDiscount.Text;
                        drCurrentRow["TOTAL_PAYABLE"] = TotalPayable.Text;
                        if (ExtraDiscount.Text == "1")
                        {
                            rdbWaivedOff.Enabled = false;
                        }
                        if (rdbWaivedOff.Text == "1")
                        {
                            drCurrentRow["ADDITIONAL"] = 0;
                        }
                        else
                        {
                            drCurrentRow["ADDITIONAL"] = box3.Text;
                        }
                        if (rdbWaivedOff.Text == "1")
                        {
                            drCurrentRow["PERCENTS"] = 0;
                        }
                        else
                        {
                            drCurrentRow["PERCENTS"] = box4.Text;
                        }
                        ViewState["PERCENTS"] = box4.Text;
                       // int percentage= Convert.ToInt32(box4.Text);
                        box4.SelectedValue = box4.Text;
                        ExtraDiscount.SelectedValue = ExtraDiscount.Text;
                      //  box4.Visible = true;
                       // rdb.Visible = false;
                        rowIndex++;
                        dtNewTable.Rows.Add(drCurrentRow);
                    }
                    else
                    {
                        return;
                    }
                }

                drCurrentRow = dtNewTable.NewRow();
                //drCurrentRow["RowNumber"] = 1;
                drCurrentRow["INSTALMENT_NO"] = dtCurrentTable.Rows.Count + 1;
                drCurrentRow["INSTALL_AMOUNT"] = string.Empty;
                drCurrentRow["DUE_DATE"] = string.Empty;
                drCurrentRow["PERCENTS"] = 0;
                drCurrentRow["Persentage"] = string.Empty;
                drCurrentRow["Waived"] = string.Empty;
                drCurrentRow["ADDITIONAL"] = string.Empty;
                drCurrentRow["ExtraDiscount"] = 0;
                drCurrentRow["TOTAL_PAYABLE"] = string.Empty;
               // box4.SelectedValue = "0"; 
                dtNewTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtNewTable;
                grdinstalment.DataSource = dtNewTable;
                grdinstalment.DataBind();
              
                    SetPreviousDataPercent();
              
            }
            else
            {
                objCommon.DisplayMessage(this, "Maximum Installment Limit Reached", this.Page);
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }
    protected void btnEditInstallMent_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
        ImageButton lnkView = sender as ImageButton;
        HiddenField hdnSemester = lnkView.NamingContainer.FindControl("hdnSemester") as HiddenField;
        int semesterno =Convert.ToInt32(hdnSemester.Value);
        int IDNO = int.Parse(lnkView.CommandArgument);
        ViewState["IDNO"] = IDNO;
        hdndeptno.Value = int.Parse(lnkView.CommandArgument).ToString();
        this.BindEditData(IDNO, semesterno);
    }


    protected void BindEditData(int Idno,int semesterno)
    {
        try
        {
            
            DataSet ds = null;
            DataTable dtNewTable = new DataTable();
            ds = feeController.GetInstallmentDetails(Idno, semesterno,Convert.ToInt32(ViewState["BRANCHNO"]),Convert.ToInt32(ViewState["DEGREENO"]),Convert.ToInt32(ViewState["COLLEGE_ID"]));

           // ds = objCommon.FillDropDown("ACD_FEES_INSTALLMENT FI INNER JOIN  ACD_STUDENT ST ON (ST.IDNO=FI.IDNO) INNER JOIN ACD_COLLEGE_MASTER CM ON(CM.COLLEGE_ID=ST.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON(ST.DEGREENO=D.DEGREENO) INNER JOIN ACD_SEMESTER SE ON(ST.SEMESTERNO=SE.SEMESTERNO) INNER JOIN ACD_BRANCH B ON(ST.BRANCHNO=B.BRANCHNO) ", " DISTINCT FI.ENROLLNMENTNO",
           //"FI.IDNO,SE.SEMESTERNAME,FI.RECIPTCODE,FI.COLLEGE_CODE,FI.SESSION_NO,SE.SEMESTERNO,ST.REGNO ,ST.STUDNAME,(D.DEGREENAME+','+B.LONGNAME) AS PROGRAM,CM.COLLEGE_NAME,FI.DUE_DATE,INSTALMENT_NO,FI.INSTALL_AMOUNT,FI.ADDITIONAL_CHARGE_AMOUNT AS ADDITIONAL,PERCENTAGES as PERCENTS,PERCENTAGES as Persentage,TOTAL_PAYABLE as TOTAL_PAYABLE,ISNULL(EXTRA_DESC_TYPE,0) as ExtraDiscount,ISNULL(FI.RECON,0) AS RECON_STATUS", "FI.IDNO=" + Convert.ToInt32(Idno) + " AND FI.SEMESTERNO=" + Convert.ToInt32(semesterno), "");

            int rowIndex = 0;
            if (ds != null)
            {
                grdinstalment.DataSource = ds;
                grdinstalment.DataBind();
                int Semister = Convert.ToInt32((ds.Tables[0].Rows[0]["SEMESTERNO"].ToString().Trim()));
                DataRow drs = ds.Tables[0].Rows[0];
                string BALANCE = (objCommon.LookUp("ACD_FEES_INSTALLMENT", "distinct TOTAL_INSTALMENT", "IDNO=" + Convert.ToInt32(Idno) + " AND SEMESTERNO=" + Convert.ToInt32(semesterno) + " "));

               
                string StudentId = drs["REGNO"].ToString();
                lblStudent_id.Text = StudentId;
                txtStudentReg.Text = drs["REGNO"].ToString();
                string fullName = drs["STUDNAME"].ToString();
                lblStudentn.Text = fullName;
                string CollegeName = drs["COLLEGE_NAME"].ToString();
                lblFacultyname.Text = CollegeName;
                lblinstallprogram.Text = drs["PROGRAM"].ToString();
                Session["REGNO"] = drs["REGNO"].ToString();
                Session["IDNO"] = drs["IDNO"].ToString();
                Session["COLLEGE_CODE"] = drs["COLLEGE_CODE"].ToString();
                Session["SESSIONNO"] = drs["SESSION_NO"].ToString();
                Session["RECIEPT_CODE"] = drs["RECIPTCODE"].ToString();
                string TotalApplicableFees = BALANCE.ToString();
                txtTotalApplicableFees.Text = TotalApplicableFees.ToString();
                ddlSemester.SelectedValue = Semister.ToString();
                StudentInf.Visible = true;
                Faculty.Visible = true;
                Amount.Visible = true;
                Installement.Visible = true;

            }
            else
            {
                objCommon.DisplayMessage(this, "Record No Found", this.Page);
                return;
            }
            foreach (GridViewRow item in grdinstalment.Rows)
            {

                Label lblPersent = item.FindControl("lblPersent") as Label;
                Label lblRecon = item.FindControl("lblRecon") as Label; 
                TextBox Addtional = item.FindControl("txtAdditionalCharge") as TextBox;
                TextBox Install_Amount = item.FindControl("txtAmount") as TextBox;
                RadioButtonList rdbWaivedOff = item.FindControl("rdbWaivedOff") as RadioButtonList;
                DropDownList ddPercent = item.FindControl("ddPercent") as DropDownList;
                DropDownList ddExtraDiscounty = item.FindControl("ddExtraDiscounty") as DropDownList;
                TextBox txtTotalPayble = item.FindControl("txtTotalPayble") as TextBox;
                TextBox txtDueDate = item.FindControl("txtDueDate") as TextBox;
        
                objCommon.FillDropDownList(ddPercent, "ACD_DISCOUNT", "DISC_NO", "DISCOUNT", "", "DISC_NO");
                decimal AddtionalAmount = Convert.ToDecimal(Addtional.Text);
                decimal InstallAmount = Convert.ToDecimal(Install_Amount.Text);
                decimal Percentage = 0.0m;
                string Recon = lblRecon.Text;
                if (Recon != "False")
                {
                    ddExtraDiscounty.Enabled = false;
                    ddPercent.Enabled = false;
                    rdbWaivedOff.Enabled = false;
                    Addtional.Enabled = false;
                    Install_Amount.Enabled = false;
                    txtTotalPayble.Enabled = false;
                    txtDueDate.Enabled = false;
                }
                if (AddtionalAmount != 0.0m)
                {
                    //Percentage = Convert.ToInt32(InstallAmount) / Convert.ToInt32(AddtionalAmount)*100;
                    Percentage = (AddtionalAmount) * 100 / (InstallAmount);
                }
                if (Percentage == 0.0m)
                {
                    rdbWaivedOff.Text = "1";
                }
                else
                {
                    if (ddExtraDiscounty.Text == "1")
                    {
                        rdbWaivedOff.Enabled = false;
                    }
                    ddPercent.SelectedValue = lblPersent.Text;
                    ddExtraDiscounty.SelectedValue = ddExtraDiscounty.Text;
                   // ddPercent.SelectedItem.Text = (Percentage.ToString());
                    rdbWaivedOff.Text = null;
                }


            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Scholarship_And_Payment.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    //protected void rdbFilter_SelectedIndexChanged(object sender, System.EventArgs e)
    //{
    //   // ViewState["Radio"] = "";
    //    foreach (GridViewRow item in grdinstalment.Rows)
    //    {
    //        RadioButtonList rdbFilter = item.FindControl("rdbFilter") as RadioButtonList;
    //        DropDownList ddPercent = item.FindControl("ddPercent") as DropDownList;
    //        TextBox Addtional = item.FindControl("txtAdditionalCharge") as TextBox;
    //         string  Radio = (rdbFilter.Text);
    //       ViewState["Radio"] = "";
    //        if (Radio == "1" && ViewState["Radio"].ToString() != "2" )
    //        {
    //            //Addtional.Text = "";
    //            rdbFilter.Visible = false;
    //            ddPercent.Visible = true;
    //        }
    //        else
    //        {
    //            ViewState["Radio"] = Radio;
    //            Addtional.Text = "0";
    //        }
    //        if (ViewState["Radio"].ToString() == "1" && Radio == "1")
    //        {
    //           return;
    //        }
    //    }
    //}

    protected void rdbWaivedOff_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        foreach (GridViewRow item in grdinstalment.Rows)
        {
            RadioButtonList rdbWaivedOff = item.FindControl("rdbWaivedOff") as RadioButtonList;
            TextBox txtAmount = item.FindControl("txtAmount") as TextBox;
            TextBox Addtional = item.FindControl("txtAdditionalCharge") as TextBox;
            TextBox TotalPayable = item.FindControl("txtTotalPayble") as TextBox;
            DropDownList ddPercent = item.FindControl("ddPercent") as DropDownList;
            DropDownList ddlExtraDiscount = item.FindControl("ddExtraDiscounty") as DropDownList;
          //  objCommon.FillDropDownList(ddPercent, "ACD_DISCOUNT", "DISC_NO", "DISCOUNT", "", "DISC_NO");
            if (txtAmount.Text == "")
            {
                objCommon.DisplayMessage(this, "Please Enter Amount..", this.Page);
            }
            string Radio = (rdbWaivedOff.Text);
            if (Radio == "1")
            {
                ddlExtraDiscount.SelectedValue = "0";
                ddPercent.SelectedItem.Text = "Please Select";
                Addtional.Text = "0";
                TotalPayable.Text = txtAmount.Text;
            }

        }
    }
    protected void ddPercent_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        foreach (GridViewRow item in grdinstalment.Rows)
        {
            int total = 100;
            decimal Additional = 0;
            TextBox Amount = item.FindControl("txtAmount") as TextBox;
            TextBox AdditionalCharge = item.FindControl("txtAdditionalCharge") as TextBox;
            TextBox TotalPayble = item.FindControl("txtTotalPayble") as TextBox;
           // RadioButtonList rdbFilter = item.FindControl("rdbFilter") as RadioButtonList;
            DropDownList ddPercent = item.FindControl("ddPercent") as DropDownList;
            DropDownList ExtraDiscount = item.FindControl("ddExtraDiscounty") as DropDownList;
            RadioButtonList rdbWaivedOff = item.FindControl("rdbWaivedOff") as RadioButtonList;
            //objCommon.FillDropDownList(ddPercent, "ACD_DISCOUNT", "DISC_NO", "DISCOUNT", "", "DISC_NO");
            // int Percentage = Convert.ToInt32(ddPercent.Text);
          
               
                if (Amount.Text == "")
                {
                    ddPercent.SelectedIndex = 0;
                    objCommon.DisplayMessage(this, "Please Enter Amount", this.Page);
                }
                else
                {
                    int ExtraDiscountType=Convert.ToInt32(ExtraDiscount.SelectedValue);;
                    //ddPercent.SelectedIndex = Convert.ToInt32(ddPercent.Text);
                 
                   
                    //if (rdbWaivedOff.Text == "1")
                    //{
                    //    AdditionalCharge.Text = "0";
                    //}
                    //else
                    //{
                    //if (rdbWaivedOff.Text == "1")
                    //{
                    //    rdbWaivedOff.Text = string.Empty ;
                    //}
                    if (ExtraDiscountType == 1)
                    {
                        rdbWaivedOff.Enabled = false;
                        if (ddPercent.Text == "0" && rdbWaivedOff.Text == "0")
                        {
                            AdditionalCharge.Text = "";
                            return;
                        }
                        if (ddPercent.Text == "0" && rdbWaivedOff.Text == "1")
                        {
                            AdditionalCharge.Text = "0";
                        }
                        else
                        {
                            AdditionalCharge.Text = "0";
                            rdbWaivedOff.SelectedValue = null;
                            decimal Percentage = Convert.ToDecimal(ddPercent.SelectedItem.Text);
                            decimal TotalAmount = Convert.ToDecimal(Amount.Text);
                            Additional = (Percentage) * (TotalAmount) / total;
                            AdditionalCharge.Text = Additional.ToString();
                            decimal TotalPaybles = TotalAmount - Additional;
                            TotalPayble.Text = TotalPaybles.ToString();
                        }
                    }
                    else if (ExtraDiscountType == 2)
                    {
                        rdbWaivedOff.Enabled = true;
                        if (ddPercent.Text == "0" && rdbWaivedOff.Text == "0")
                        {
                            AdditionalCharge.Text = "";
                            return;
                        }
                        if (ddPercent.Text == "0" && rdbWaivedOff.Text == "1")
                        {
                            AdditionalCharge.Text = "0";
                        }
                        else
                        {
                            AdditionalCharge.Text = "0";
                            rdbWaivedOff.SelectedValue = null;
                            decimal Percentage = Convert.ToDecimal(ddPercent.SelectedItem.Text);
                            decimal TotalAmount = Convert.ToDecimal(Amount.Text);
                            Additional = (Percentage) * (TotalAmount) / total;
                            AdditionalCharge.Text = Additional.ToString();
                            decimal TotalPaybles = TotalAmount + Additional;
                            TotalPayble.Text = TotalPaybles.ToString();
                        }
                    }
                    else
                    {
                        rdbWaivedOff.Enabled = true;
                        ddPercent.SelectedIndex = 0;
                       // objCommon.DisplayMessage(this, "Please Select Type", this.Page);
                    }
                    }
                //}
           
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }


    protected void btnSearch_Click(object sender, System.EventArgs e)
    {
        try
        {
            int Semister = 0;
          
            if (txtStudentReg.Text == null || txtStudentReg.Text == "" || txtStudentReg.Text == "0")
            {
                objCommon.DisplayMessage(this.updInstall, "Please Enter Student Reg.No.!!", this.Page);
            }
            else
            {
                string StudentRegNo = (txtStudentReg.Text);
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");

                int CAN = 0;

                // DataSet ds = objCommon.FillDropDown("acd_demand D INNER JOIN ACD_STUDENT S ON(D.IDNO=S.IDNO) INNER JOIN ACD_COLLEGE_MASTER CM ON (D.COLLEGE_ID=CM.COLLEGE_ID)", "D.IDNO", "S.ENROLLNO,D.NAME,D.SEMESTERNO,D.F1 AS TOTALFEES,CM.COLLEGE_NAME", "D.CAN='" + CAN + "' AND S.ENROLLNO='" + StudentRegNo + "'", "");
                DataSet ds = feeController.GetStudentfeesinstallementById(StudentRegNo);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["BRANCHNO"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    ViewState["DEGREENO"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                    ViewState["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                    Amount.Visible = true;
                    Installement.Visible = true;
                    StudentInf.Visible = true;
                    Faculty.Visible = true;
                    Semister = Convert.ToInt32((ds.Tables[0].Rows[0]["SEMESTERNO"].ToString().Trim()));
                    DataRow drs = ds.Tables[0].Rows[0];
                    string StudentId = drs["REGNO"].ToString();
                    lblStudent_id.Text = StudentId;
                    string fullName = drs["STUDNAME"].ToString();
                    lblStudentn.Text = fullName;
                    string CollegeName = drs["COLLEGE_NAME"].ToString();
                    lblFacultyname.Text = CollegeName;
                    lblinstallprogram.Text = drs["PROGRAM"].ToString();
                    string TotalApplicableFees = drs["BALANCE"].ToString();
                    txtTotalApplicableFees.Text = TotalApplicableFees;
                    ddlSemester.SelectedValue = Semister.ToString();

                    Session["COLLEGE_CODE"] = drs["COLLEGE_CODE"].ToString();
                    Session["UA_NO"] = drs["UA_NO"].ToString();
                 
                    Session["RECIEPT_CODE"] = drs["RECIEPT_CODE"].ToString();
                    Session["SESSIONNO"] = drs["SESSIONNO"].ToString();
                    // Session["PAY_TYPE"] = drs["PAY_TYPE"].ToString();
                    Session["DM_NO"] = drs["DM_NO"].ToString();
                    //  Session["ORDER_ID"] = drs["ORDER_ID"].ToString();
                    Session["REGNO"] = drs["REGNO"].ToString();
                    Session["IDNO"] = drs["IDNO"].ToString();
                    ViewState["IDNO"] = drs["IDNO"].ToString();

                    DataTable table = ds.Tables[0];
                   
                }
                else
                {
                    lblStudent_id.Text = "";
                    lblStudentn.Text = "";
                    lblFacultyname.Text = "";
                    txtTotalApplicableFees.Text = "";
                    StudentInf.Visible = false;
                   
                    Faculty.Visible = false;
                    ddlSemester.SelectedValue = "0";
                    objCommon.DisplayMessage(this.updInstall, "Demand Not Create.!!", this.Page);
                    return;
                }
            }
            SetInitialRow();
            SetPreviousDataPercent();
            string idno = "";
            idno = objCommon.LookUp("ACD_FEES_INSTALLMENT", "IDNO", "IDNO=" + Convert.ToInt32(Session["IDNO"].ToString()) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "AND BRANCHNO=" + Convert.ToInt32(ViewState["BRANCHNO"]) + "AND DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"]) + "AND COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"]));
            if (idno != "")
            {
                BindEditData(Convert.ToInt32(ViewState["IDNO"].ToString()), Convert.ToInt32(ddlSemester.SelectedValue));
            }
            
            
           // objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO=" + Semister, "SEMESTERNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ScholarshipAmount_Master.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }

    }
    protected void clear()
    {
      
    }

    protected void btnSubmitInstallment_Click(object sender, System.EventArgs e)
    {
        int     Sessions = Convert.ToInt32(Session["SESSIONNO"].ToString());
        int      idno = Convert.ToInt32(Session["IDNO"].ToString());
        int     CollegeCode = Convert.ToInt32(Session["COLLEGE_CODE"].ToString());
       // int UaNo = Convert.ToInt32(Session["UA_NO"].ToString()); 
        int UaNo = Convert.ToInt32(Session["userno"].ToString());

       // string  Orderid =(Session["ORDER_ID"].ToString());
       // bool    Reco = Convert.ToBoolean(Session["RECON"].ToString());
        string Enroll = Session["REGNO"].ToString();
        
        int Recon = 0;
       // string  PayType = (Session["PAY_TYPE"].ToString());
        string  Recipt_code = (Session["RECIEPT_CODE"].ToString());

        int     Semister = Convert.ToInt32(ddlSemester.SelectedValue);
        string  TotalInstallment =txtTotalApplicableFees.Text;
      
        int STATUS = 1;
        int  InstallmentNo=0;
        string InstallmenFee=string.Empty;
        decimal   InstalCharge =0.00m;
        decimal Total_Payable = 0.00m;
        int Percent = 0,ExtraDiscount=0;
        
        CustomStatus cs =0;
        decimal totalamount = txtTotalApplicableFees.Text == string.Empty ? 0 : Convert.ToDecimal(txtTotalApplicableFees.Text);
        decimal  TotalSum = 0.00m;
        foreach (GridViewRow item in grdinstalment.Rows)
        {
            Label InstalMent = item.FindControl("LblSrno") as Label;
            TextBox InstallmenFees = item.FindControl("txtAmount") as TextBox;
            decimal Installment = Convert.ToDecimal(InstallmenFees.Text);
            int InstallmentFee = Convert.ToInt32(Installment);
           TotalSum += InstallmenFees.Text == string.Empty ? 0 : Convert.ToDecimal(InstallmenFees.Text);
        }
        if (TotalSum != totalamount || TotalSum > totalamount)
        {
            objCommon.DisplayMessage(this, "Installment Amount not matched with Total Applicable Fees.Insert Proper Calculated Amount.", this.Page);
            return;
        }
       
        foreach (GridViewRow item in grdinstalment.Rows)
        {
            DropDownList ddPercent = item.FindControl("ddPercent") as DropDownList;
            DropDownList ddExtraDiscounty = item.FindControl("ddExtraDiscounty") as DropDownList;
            RadioButtonList rdowaveoff = item.FindControl("rdbWaivedOff") as RadioButtonList;
            Label InstalMent = item.FindControl("LblSrno") as Label;
            TextBox InstallmenFees = item.FindControl("txtAmount") as TextBox;
            TextBox txtduedate = item.FindControl("txtDueDate") as TextBox;
            TextBox Additional = item.FindControl("txtAdditionalCharge") as TextBox;
            TextBox TotalPayble = item.FindControl("txtTotalPayble") as TextBox;
            InstallmentNo = Convert.ToInt32(InstalMent.Text);
            InstallmenFee = InstallmenFees.Text;
            DateTime DueDate = Convert.ToDateTime(txtduedate.Text);
            InstalCharge = Convert.ToDecimal(Additional.Text);
            Total_Payable = Convert.ToDecimal(TotalPayble.Text);
            if (rdowaveoff.SelectedValue != "1")
            {
                if (ddExtraDiscounty.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(this.updInstall, "Please Select Type.", this.Page);
                    return;
                }
                if (InstallmenFees.Text == "")
                {
                    objCommon.DisplayMessage(this.updInstall, "Please Enter Extra/ Discount Amount.", this.Page);
                    return;
                }
                if (ddPercent.SelectedItem.Text != "Please Select")
                {
                    Percent = Convert.ToInt32(ddPercent.SelectedValue);
                }
                
            }
            
                ExtraDiscount = Convert.ToInt32(ddExtraDiscounty.SelectedValue);

                cs = (CustomStatus)feeController.InsertStudentFeeInstallment(idno, CollegeCode, UaNo, Recon, Semister, DueDate, Recipt_code, TotalInstallment, InstallmenFee, InstallmentNo, Sessions, STATUS, InstalCharge, Enroll, Percent, Total_Payable, ExtraDiscount);
                Percent = 0;
            
        }
      
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this.updInstall, "Record saved successfully.", this.Page);
            BindListViewIntalla();
            ViewState["IDNO"] =null;
        }
        else if (cs.Equals(CustomStatus.RecordExist))
        {
            objCommon.DisplayMessage(this.updInstall, "Installment Alredy Exists", this.Page);
        }
        // else
        //{
        //    objCommon.DisplayMessage(this.updInstall, "Server error",this.Page);
        //}


    }
    protected void btnCancelInstallment_Click(object sender, System.EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void BindListViewIntalla()
    {
        try{
            DataSet dsfee = null;
            dsfee = objCommon.FillDropDown("ACD_FEES_INSTALLMENT FI LEFT JOIN  ACD_STUDENT S ON(FI.IDNO=S.IDNO) INNER JOIN ACD_DEGREE D ON(S.DEGREENO=D.DEGREENO) INNER JOIN ACD_SEMESTER SE ON(FI.SEMESTERNO=SE.SEMESTERNO)", " DISTINCT FI.IDNO", "FI.IDNO ,SE.SEMESTERNAME,SE.SEMESTERNO,FI.ENROLLNMENTNO ,S.NAME_WITH_INITIAL,D.DEGREENAME,CONVERT(varchar, [DATA_INSERT_DATE], 34) AS DATA_INSERT_DATEs", "", "");
            if (dsfee.Tables[0].Rows.Count > 0)
            {
               
               // DivViewData.Visible = false;
                lvinstall.DataSource = dsfee;
                lvinstall.DataBind();
            }

            else
            {
               // DivViewData.Visible = false;
            
                //objCommon.DisplayMessage(this.updInstall, "No Record Found.", this.Page);
                
                lvinstall.DataSource = null;
                lvinstall.DataBind();

            }
        }
        catch
        {
        }
    }
    

    protected void lnkView_Click(object sender, System.EventArgs e)
    {
        int semesterno = 0;
        LinkButton lnkView = sender as LinkButton;
        int ENROLLNMENT_NO = int.Parse(lnkView.CommandArgument);
        hdndeptno.Value = int.Parse(lnkView.CommandArgument).ToString();
        HiddenField hdnSemester = lnkView.NamingContainer.FindControl("hdnSemester") as HiddenField;
        semesterno = Convert.ToInt32(hdnSemester.Value);

        DataSet dsfee = null;
        dsfee = objCommon.FillDropDown("ACD_FEES_INSTALLMENT FI INNER JOIN  ACD_STUDENT ST ON (ST.IDNO=FI.IDNO) INNER JOIN ACD_COLLEGE_MASTER CM ON(CM.COLLEGE_ID=ST.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON(ST.DEGREENO=D.DEGREENO) INNER JOIN ACD_SEMESTER SE ON(ST.SEMESTERNO=SE.SEMESTERNO) INNER JOIN ACD_BRANCH B ON(ST.BRANCHNO=B.BRANCHNO) ",
            " DISTINCT FI.ENROLLNMENTNO", "FI.IDNO,SE.SEMESTERNAME,ST.REGNO ,ST.STUDNAME,(D.DEGREENAME+','+B.LONGNAME) AS PROGRAM,CM.COLLEGE_NAME", "FI.IDNO=" + ENROLLNMENT_NO +" AND FI.SEMESTERNO="+Convert.ToInt32(semesterno), "");
        if (dsfee.Tables[0].Rows.Count > 0)
        {

            lblStudentID.Text = (dsfee.Tables[0].Rows[0]["ENROLLNMENTNO"].ToString().Trim());
            lblStudentName.Text = (dsfee.Tables[0].Rows[0]["STUDNAME"].ToString().Trim());
            lblProgram.Text = (dsfee.Tables[0].Rows[0]["PROGRAM"].ToString().Trim());
            lblCurrentSemester.Text = (dsfee.Tables[0].Rows[0]["SEMESTERNAME"].ToString().Trim());
            lblfaculty.Text = (dsfee.Tables[0].Rows[0]["COLLEGE_NAME"].ToString().Trim());
            //"D.CAN='" + CAN + "' AND S.ENROLLNO='" + StudentRegNo + "'"

            DataSet dsss = objCommon.FillDropDown("ACD_FEES_INSTALLMENT FI LEFT JOIN ACD_DCR DT ON(FI.IDNO=DT.IDNO AND FI.INSTALL_NO=DT.INSTALL_NO AND ISNULL(DT.CAN,0)=0 AND ISNULL(DT.DELET,0)=0 AND FI.RECON=1)", " DISTINCT FI.IDNO",
                "FI.IDNO, INSTALMENT_NO,ADDITIONAL_CHARGE_AMOUNT,INSTALL_AMOUNT,CONVERT(varchar, [DUE_DATE], 34) AS Date,(CASE WHEN INSTALL_STATUS = 1 THEN 'Paid' ELSE 'UnPaid' END) AS INSTALL_STATUSs,DT.INSTALL_NO", "FI.IDNO=" + ENROLLNMENT_NO + " AND FI.SEMESTERNO=" + Convert.ToInt32(semesterno), "");
           // DataSet dsss = objCommon.FillDropDown("ACD_FEES_INSTALLMENT", " DISTINCT IDNO", "INSTALMENT_NO,ADDITIONAL_CHARGE_AMOUNT,INSTALL_AMOUNT,CONVERT(varchar, [DUE_DATE], 34) AS Date,(CASE WHEN INSTALL_STATUS = 1 THEN 'Paid' ELSE 'UnPaid' END) AS INSTALL_STATUSs", "IDNO=" + ENROLLNMENT_NO, "");
            if (dsss.Tables[0].Rows.Count > 0)
            {

               // DivViewData.Visible = false;
                lvInstallmentList.DataSource = dsss;
                lvInstallmentList.DataBind();
            }

            else
            {
               // DivViewData.Visible = false;
            
                objCommon.DisplayMessage(this.updInstall, "No Record Found.", this.Page);
                
                lvInstallmentList.DataSource = null;
                lvInstallmentList.DataBind();

            }
            foreach (ListViewDataItem dataitem in lvInstallmentList.Items)
            {
                Label lblPayment=dataitem.FindControl("lblPayment") as Label;


                if (lblPayment.Text != "")
                {
                    lblPayment.Text = "Paid";
                    lblPayment.CssClass = "badge badge-success";
                }
                else
                {
                    lblPayment.Text = "UnPaid";
                    lblPayment.CssClass = "badge badge-danger";
                }
            }
           

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Installment_Veiw", "$(document).ready(function () {$('#Installment_Veiw').modal();});", true);
        }

    }

    
#endregion
    //CONCESSION TAB
    #region CONCESSION TAB

    protected void DropDownBindConsession()
    {
        
        objCommon.FillDropDownList(ddlFaculty, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_ID");
        objCommon.FillListBox(lstbxStudyLevel, "ACD_UA_SECTION", "UA_SECTION", "SHORTNAME", "UA_SECTION IN (1,2,3,4,5)" , "UA_SECTION");
        objCommon.FillDropDownList(ddlSemesterConcession, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
    }


    protected void ddlFaculty_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        objCommon.FillListBox(lstbxProgram, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (D.DEGREENO=CDB.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=CDB.BRANCHNO) ", "DISTINCT CONVERT(NVARCHAR(10),D.DEGREENO) + ',' + CONVERT(NVARCHAR(10),CDB.BRANCHNO)", "(D.DEGREENAME +'-'+ B.LONGNAME) AS PROGRAM", "D.DEGREENO> 0 AND CDB.COLLEGE_ID=" + ddlFaculty.SelectedValue, "");
        //objCommon.FillListBox(lstbxProgram, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (D.DEGREENO=CDB.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=CDB.BRANCHNO) ", "DISTINCT D.DEGREENO", "(D.DEGREENAME +'-'+ B.LONGNAME) AS PROGRAM", "D.DEGREENO> 0 AND CDB.COLLEGE_ID=" + ddlFaculty.SelectedValue, "D.DEGREENO");
    }
    protected void btnShowConcession_Click(object sender, System.EventArgs e)
    {

        //if (rblSelection.SelectedValue == "")
        //{
        //    objCommon.DisplayMessage(Concession, "Please Select Loan Scheme or Regular ", this.Page);
        //    return;
        //}
        int LoanSchemeRegu = Convert.ToInt32(0);
        btnSubmitConcession.Visible = true;
        btnCancelConcession.Visible = true;
        int FACULTY = Convert.ToInt32(ddlFaculty.SelectedValue);
        string study_level = "";
        string program = ""; string Branhcno = "";
        foreach (ListItem items in lstbxStudyLevel.Items)
        {
            if (items.Selected == true)
            {
                study_level += items.Value + ',';

            }
        }
        if (study_level == string.Empty)
        {
            objCommon.DisplayMessage(Concession, "Please Select Study Level", this.Page);
            return;
        }
        study_level = study_level.Substring(0, study_level.Length - 1);

        foreach (ListItem items in lstbxProgram.Items)
        {
            if (items.Selected == true)
            {
                //program += items.Value + ',';
                program += items.Value.Split(',')[0] + ',';
                Branhcno += items.Value.Split(',')[1] + ',';
            }
        }
        if (program == string.Empty)
        {
            objCommon.DisplayMessage(Concession, "Please Select Program", this.Page);
            return;
        }
        program = program.Substring(0, program.Length - 1);
        Branhcno = Branhcno.TrimEnd(',');
        divConcessionOption.Visible = true;
        rdConcessionOption.SelectedValue = null;

        int Semester = Convert.ToInt32(ddlSemesterConcession.SelectedValue);
        DataSet dss = feeController.GetStudentFeeConcession(FACULTY, study_level, program, Semester, LoanSchemeRegu, Branhcno);
        if (dss.Tables[0].Rows.Count > 0)
        {
            lvlConcession.DataSource = dss;
            lvlConcession.DataBind();
            lvlConcession.Visible = true;

        }
        else
        {
            objCommon.DisplayMessage(Concession, "Record Not Found", this.Page);
            lvlConcession.Visible = false; divConcessionOption.Visible = false;
        }
        string SP_Name1 = "PKG_ACD_BIND_DROP_DOWN_LISTS";
        string SP_Parameters1 = "@P_OUTPUT";
        string Call_Values1 = "" + 1;

        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);

        foreach (ListViewDataItem dataitem in lvlConcession.Items)
        {
            CheckBox chktransfer = dataitem.FindControl("chktransfer") as CheckBox;
            DropDownList ddlDiscount = dataitem.FindControl("ddlDiscount") as DropDownList;
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDiscount.Items.Clear();
                ddlDiscount.Items.Add("Please Select");
                ddlDiscount.SelectedItem.Value = "0";
                ddlDiscount.DataSource = ds.Tables[0];
                ddlDiscount.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlDiscount.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlDiscount.DataBind();
                ddlDiscount.SelectedIndex = 0;

            }
            DropDownList ddlConcession = dataitem.FindControl("ddlConcession") as DropDownList;
            DropDownList dllSelectAllType = this.lvlConcession.Controls[0].FindControl("dllSelectAllType") as DropDownList;
            if (ds.Tables != null && ds.Tables[1].Rows.Count > 0)
            {
                ddlConcession.Items.Clear();
                ddlConcession.Items.Add("Please Select");
                ddlConcession.SelectedItem.Value = "0";


                ddlConcession.DataSource = ds.Tables[1];
                ddlConcession.DataValueField = ds.Tables[1].Columns[0].ToString();
                ddlConcession.DataTextField = ds.Tables[1].Columns[1].ToString();
                ddlConcession.DataBind();
                ddlConcession.SelectedIndex = 0;

                dllSelectAllType.Items.Clear();
                dllSelectAllType.Items.Add("Please Select");
                dllSelectAllType.SelectedItem.Value = "0";


                dllSelectAllType.DataSource = ds.Tables[1];
                dllSelectAllType.DataValueField = ds.Tables[1].Columns[0].ToString();
                dllSelectAllType.DataTextField = ds.Tables[1].Columns[1].ToString();
                dllSelectAllType.DataBind();
                dllSelectAllType.SelectedIndex = 0;

            }
            Label lblDiscount = dataitem.FindControl("lblDiscount") as Label;
            Label lblConcessionno = dataitem.FindControl("lblConcessionno") as Label;
            TextBox txtDiscountFee = dataitem.FindControl("txtDiscountFee") as TextBox;
            TextBox txtNetPayable = dataitem.FindControl("txtNetPayable") as TextBox;
            Label lbldcridno = dataitem.FindControl("lbldcridno") as Label;
            string DiscountFee = (txtDiscountFee.Text);
            string NetPayable = (txtNetPayable.Text);
            string dcridno = (lbldcridno.Text);

            // int Dis = Convert.ToInt32(lblDiscount.Text);
            ddlDiscount.SelectedItem.Text = lblDiscount.Text;
            ddlConcession.SelectedValue = lblConcessionno.Text;
            if (dcridno.ToString() != "0")
            {
                chktransfer.Enabled = false;
                ddlDiscount.Enabled = false;
                ddlConcession.Enabled = false;
                lbldcridno.Text = "Utilize";
                lbldcridno.CssClass = "lbl-green";
            }
            else
            {
                lbldcridno.Text = "Not Utilize";
                lbldcridno.CssClass = "lbl-red";
            }



        }

    }
    protected void btnSubmitConcession_Click(object sender, System.EventArgs e)
    {
        try
        {
            if (rdConcessionOption.SelectedValue == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Payment Type", this.Page);
                return;
            }
            string StudentReg = string.Empty;

            CustomStatus cs = 0;
            foreach (ListViewDataItem dataitem in lvlConcession.Items)
            {
                CheckBox chk = dataitem.FindControl("chktransfer") as CheckBox;
                DropDownList ddlDiscount = dataitem.FindControl("ddlDiscount") as DropDownList;
                DropDownList ddlConcession = dataitem.FindControl("ddlConcession") as DropDownList;
                Label lblreg = dataitem.FindControl("lblreg") as Label;
                Label lblname = dataitem.FindControl("lblname") as Label;
                Label lblIdno = dataitem.FindControl("lblIdno") as Label;
                TextBox lbltotal = dataitem.FindControl("lbltotal") as TextBox;
                TextBox txtDiscountFee = dataitem.FindControl("txtDiscountFee") as TextBox;
                TextBox txtNetPayable = dataitem.FindControl("txtNetPayable") as TextBox;
                Label lblDiscount = dataitem.FindControl("lblDiscount") as Label;
               
              

                if (chk.Checked == true && chk.Enabled == true)
                {
                    StudentReg += lblreg.Text + '$';

                    if (ddlConcession.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.Concession, "Please Select Discount Type.", this.Page);
                        return;
                    }
                    if (rdConcessionOption.SelectedValue != "1")
                    {
                        if (lblDiscount.Text == "")
                        {
                            if (ddlDiscount.SelectedValue == "0")
                            {
                                objCommon.DisplayMessage(this.Concession, "Please Select Discount.", this.Page);
                                return;
                            }
                        }
                    }
                  
                        if (txtDiscountFee.Text == "" && txtNetPayable.Text == "")
                        {
                            objCommon.DisplayMessage(this.Concession, "Please Enter Discount Fee And Net Payable", this.Page);
                        }
                        else
                        {
                            string Discount = Convert.ToString(ddlDiscount.SelectedItem.Text);
                            string Regno = lblreg.Text;
                            string name = lblname.Text;
                            decimal Totals = Convert.ToDecimal(lbltotal.Text);
                            int Total = Convert.ToInt32(Totals);
                            decimal DiscountFeee = Convert.ToDecimal(txtDiscountFee.Text);
                            decimal NetPayable = Convert.ToDecimal(txtNetPayable.Text);
                            int Ministry = Convert.ToInt32(0);
                            int Concession_no = Convert.ToInt32(ddlConcession.SelectedValue);
                            int UA_NO = Convert.ToInt32(Session["userno"].ToString());
                            int Idno = Convert.ToInt32(lblIdno.Text);

                            cs = (CustomStatus)feeController.InsertFeesConcessio(Regno, Convert.ToInt32(ddlFaculty.SelectedValue), name, Totals, DiscountFeee, Discount, NetPayable, Convert.ToInt32(ddlSemesterConcession.SelectedValue),Ministry,UA_NO,Concession_no, Idno,Convert.ToInt32(rdConcessionOption.SelectedValue));
                        }
                    }
                
               
            }
            if (StudentReg.ToString() == string.Empty)
            {
                objCommon.DisplayMessage(this.Concession, "Please Select At List One.", this.Page);
                return;
            }
            
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.Concession, "Record Saved Successfully.", this.Page);
                // BindListViewRuleAllocation();
               
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.Concession, "Failed To Save Record ", this.Page);
            }
        }
        catch
        {
        }

    }
    protected void btnCancelConcession_Click(object sender, System.EventArgs e)
    {
        ddlFaculty.SelectedValue = "0";
        lstbxStudyLevel.SelectedValue = null;
        lstbxProgram.SelectedValue = null;
        ddlSemesterConcession.SelectedValue = "0";
        lvlConcession.Visible = false;
        btnSubmitConcession.Visible = false;
        btnCancelConcession.Visible = false;
        divConcessionOption.Visible = false;
        rdConcessionOption.SelectedValue = null;
    }
    protected void rblSelection_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        lvlConcession.Visible = false;
        btnSubmitConcession.Visible = false;
        btnCancelConcession.Visible = false;
        divConcessionOption.Visible = false;
        rdConcessionOption.SelectedValue = null;

    }
     #endregion
    //SCHOLLARSHIP RULE TAB

    #region SCHOLLARSHIP RULE TAB

    protected void btnSubmitRule_Click(object sender, System.EventArgs e)
    {
        string RuleName = txtRuleName.Text;
        int Mode = Convert.ToInt32(ddlDecideMode.SelectedValue);
        string AmountPer = txtAmountPercent.Text;
        string RequiredGPA = txtRequiredGPA.Text;
        int RuleNo =0;
        if (ViewState["RULENO"] != null)
        {
            RuleNo = Convert.ToInt32(ViewState["RULENO"].ToString());
        }

        CustomStatus cs = 0;
        cs = (CustomStatus)feeController.InsertStudentSchollarshipRule(RuleName, Mode, AmountPer, RequiredGPA, RuleNo);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this.updInstall, "Record saved successfully.", this.Page);
            ListViewBindRule();
            Clearrule();
        }
        else
        {
            objCommon.DisplayMessage(this.updInstall, "Please Select At least On Checkbox", this.Page);
        }

    }

    protected void Clearrule()
    {
        txtRuleName.Text = "";
        ddlDecideMode.SelectedValue = "0";
        txtRequiredGPA.Text = "";
        txtAmountPercent.Text = "";
    }
    protected void ListViewBindRule()
    {
        try
        {

            DataSet dsfee = null;
            dsfee = objCommon.FillDropDown("ACD_SCHOLARSHIP_RULE SR INNER JOIN ACD_MODE M ON(M.SRNO=SR.DECIDEMODE) ", "RULENO", "MODE_NAME,RULENAME,DECIDEMODE,AMOUNT,REQUIREDGPA", "RULENO>0", "RULENAME");
            if (dsfee.Tables[0].Rows.Count > 0)
            {

                // DivViewData.Visible = false;
                lvlRule.DataSource = dsfee;
                lvlRule.DataBind();
            }

            else
            {
               // objCommon.DisplayMessage(this.updInstall, "No Record Found.", this.Page);

                lvlRule.DataSource = null;
                lvlRule.DataBind();

            }
        }
        catch
        {
        }
    }

    protected void btneditRule_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        try
        {

            ImageButton btnEditS = sender as ImageButton;
            int RULENO = int.Parse(btnEditS.CommandArgument);
            ViewState["RULENO"] = RULENO;
            hdndeptno.Value = int.Parse(btnEditS.CommandArgument).ToString();
            DataSet ds = objCommon.FillDropDown("ACD_SCHOLARSHIP_RULE ", "*", "RULENO", "RULENO=" + RULENO, string.Empty);
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtRuleName.Text = ds.Tables[0].Rows[0]["RULENAME"].ToString();

                ddlDecideMode.SelectedValue = ds.Tables[0].Rows[0]["DECIDEMODE"].ToString();
                txtAmountPercent.Text = ds.Tables[0].Rows[0]["AMOUNT"].ToString();
                txtRequiredGPA.Text = ds.Tables[0].Rows[0]["REQUIREDGPA"].ToString();
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ProgramRuleCreation.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancelRule_Click(object sender, System.EventArgs e)
    {
        Clearrule();
    }

    #endregion
   

    //SCHOLLARSHIP TAB
    #region SCHOLLARSHIP TAB
    protected void DropDownBindSchollarship()
    {
        objCommon.FillDropDownList(ddlFacultySchlorship, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_ID");
        objCommon.FillListBox(lstbxStudyLevelSchlorship, "ACD_UA_SECTION", "UA_SECTION", "SHORTNAME", "UA_SECTION IN (1,2,3,4,5)", "UA_SECTION");
        objCommon.FillDropDownList(ddlSemesterSchlorship, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
    }


    protected void ddlFacultySchlorship_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        objCommon.FillListBox(lstbxProgramSchlorship, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (D.DEGREENO=CDB.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=CDB.BRANCHNO) ", "(CONVERT(NVARCHAR(16),D.DEGREENO) + '$' + CONVERT(NVARCHAR(16),B.BRANCHNO)) AS PROGRAM", "(D.DEGREENAME +'-'+ B.LONGNAME) AS PROGRAM", "D.DEGREENO> 0 AND CDB.COLLEGE_ID=" + ddlFacultySchlorship.SelectedValue, "D.DEGREENO");
    }

    protected void ListViewBindData()
    {
        btnSubmitScholarship.Visible = true;
        btnCancelScholarship.Visible = true;
        divScholership.Visible = true;
        rdScholership.SelectedValue = null;

        int FACULTY = Convert.ToInt32(ddlFacultySchlorship.SelectedValue);
        string study_level = "";
        string Program = string.Empty;
        string degreeno = string.Empty;
        string Branchno = string.Empty;
        foreach (ListItem items in lstbxStudyLevelSchlorship.Items)
        {
            if (items.Selected == true)
            {
                study_level += items.Value + ',';

            }
        }
        if (study_level == string.Empty)
        {
            objCommon.DisplayMessage(Concession, "Please Select Study Level", this.Page);
            return;
        }
        study_level = study_level.Substring(0, study_level.Length - 1);

        foreach (ListItem items in lstbxProgramSchlorship.Items)
        {
            if (items.Selected == true)
            {
                Program = items.Value;
                string[] splitValue;
                splitValue = Program.Split('$');

                degreeno += (splitValue[0].ToString()) + ',';
                Branchno += (splitValue[1].ToString()) + ',';
            }
        }
        if (Program == string.Empty)
        {
            objCommon.DisplayMessage(this, "Please Select Faculty", this.Page);
            return;
        }
        Branchno = Branchno.Substring(0, Branchno.Length - 1);
        degreeno = degreeno.Substring(0, degreeno.Length - 1);


        int Semester = Convert.ToInt32(ddlSemesterSchlorship.SelectedValue);
        ViewState["Semester"] = Semester;
        DataSet dss = feeController.GetStudentFeeShollarship(FACULTY, study_level, degreeno, Semester);
        if (dss.Tables[0].Rows.Count > 0)
        {
            lvlSholarship.Visible = true;
            lvlSholarship.DataSource = dss;
            lvlSholarship.DataBind();
        }
        else
        {
            lvlSholarship.Visible = false;
            objCommon.DisplayMessage(Concession, "Record Not Found", this.Page);
        }
        foreach (ListViewDataItem dataitem in lvlSholarship.Items)
        {
            CheckBox chktransfer = dataitem.FindControl("chktransfer") as CheckBox;
            TextBox txtScholarshipAmount = dataitem.FindControl("txtScholarshipAmount") as TextBox;
            Label Status = dataitem.FindControl("Status") as Label;
            //int STATUS = Convert.ToInt32(Status.Text);
            string ScholarshipAmount = txtScholarshipAmount.Text;
            if (Status.Text == "True")
            {
                chktransfer.Enabled = false;
                chktransfer.ToolTip = "Confirm";
                txtScholarshipAmount.Enabled = false;
                Status.Text = "Confirm";
                Status.CssClass = "badge badge-success";
            }
            else
            {
                chktransfer.ToolTip = "Not Confirm";
                chktransfer.Enabled = true;
                Status.Text = "Not Confirm";
                Status.CssClass = "badge badge-danger";
            }
        }
    }
    protected void btnShowScholarship_Click(object sender, System.EventArgs e)
    {
        ListViewBindData();   
    }

    protected void btnSubmitScholarship_Click(object sender, System.EventArgs e)
    {
        try
        {
            if (rdScholership.SelectedValue == string.Empty)
            {
                objCommon.DisplayMessage(this.Page,"Please Select Payment Type",this.Page);
                return;
            }
            int UaNo=Convert.ToInt32(Session["userno"].ToString());
            string StudentReg = string.Empty;
            int Semester = Convert.ToInt32(ViewState["Semester"].ToString());
          
            CustomStatus cs = 0;
            foreach (ListViewDataItem dataitem in lvlSholarship.Items)
            {
                CheckBox chk = dataitem.FindControl("chktransfer") as CheckBox;
            
                Label lblreg = dataitem.FindControl("lblreg") as Label;
                Label lblIdno = dataitem.FindControl("lblIdno") as Label;
                TextBox lbltotal = dataitem.FindControl("lbltotal") as TextBox; 
                TextBox txtScholarshipAmount = dataitem.FindControl("txtScholarshipAmount") as TextBox;
                if (chk.Checked == true)
                {
                    StudentReg += lblreg.Text + '$';
                    if (txtScholarshipAmount.Text == "")
                    {
                        objCommon.DisplayMessage(this.Concession, "Please Insert Scholarship Amount .", this.Page);
                         return;
                    }
                            decimal ScholarAmount= Convert.ToDecimal(txtScholarshipAmount.Text);
                            int ScholarshipAmount = Convert.ToInt32(ScholarAmount);
                            
                            string Regno = lblreg.Text;
                            
                            int Idno = Convert.ToInt32(lblIdno.Text);
                            decimal Total = Convert.ToDecimal(lbltotal.Text);


                            cs = (CustomStatus)feeController.InsertFeesSholarship(Idno, Total, ScholarshipAmount,Semester, UaNo,Convert.ToInt32(rdScholership.SelectedValue));
                       
                    }

                
                }
            if (StudentReg.ToString() == string.Empty)
            {
                objCommon.DisplayMessage(this.Concession, "Please Select At List One.", this.Page);
                return;
            } 
            if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.Concession, "Record Saved Successfully.", this.Page);
                    ListViewBindData();

                    return;
                }
                else
                {
                    objCommon.DisplayMessage(this.Concession, "Failed To Save Record ", this.Page);
                }
            }
       
        catch
        {
        }
    }

    protected void btnCancelScholarship_Click(object sender, System.EventArgs e)
    {
        ddlFacultySchlorship.SelectedValue = "0";
        lstbxStudyLevelSchlorship.SelectedValue = null;
        lstbxProgramSchlorship.SelectedValue = null;
        ddlSemesterSchlorship.SelectedValue = "0";
        lvlSholarship.Visible = false;
        btnCancelScholarship.Visible = false;
        btnSubmitScholarship.Visible = false;
        rdScholership.SelectedValue = null;
        divScholership.Visible = false;
    }
    #endregion

    protected void dropwown()
    {
        foreach (GridViewRow item in grdinstalment.Rows)
        {
            RadioButtonList rdbFilter = item.FindControl("rdbFilter") as RadioButtonList;
            DropDownList ddPercent = item.FindControl("ddPercent") as DropDownList;
            //int Radio = Convert.ToInt32(rdbFilter.Text);
            //if (Radio == 1)
            //{
            //    rdbFilter.Visible = false;
            //    ddPercent.Visible = true;
            //}
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, System.EventArgs e)
    {
            string Demand = "";
            Demand = objCommon.LookUp("ACD_DEMAND", "TOTAL_AMT", "IDNO=" + Convert.ToInt32(Session["IDNO"].ToString())+" AND SEMESTERNO="+Convert.ToInt32(ddlSemester.SelectedValue)+"AND CAN=0 AND DELET=0");
            if (Demand == "")
            {
                objCommon.DisplayMessage(this.updInstall, "Demand Not Found.", this.Page);
                txtTotalApplicableFees.Text = "";
                grdinstalment.DataSource = null;
                grdinstalment.DataBind();
                return;
            }
            else
            {
                    txtTotalApplicableFees.Text = Demand;
                    string idno = "";
                    idno = objCommon.LookUp("ACD_FEES_INSTALLMENT", "IDNO", "IDNO=" + Convert.ToInt32(Session["IDNO"].ToString()) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue));
                    if (idno != "")
                    {
                        String IDNO = "";
                        DataSet ds1 = feeController.GetStudentfeesinstallementById(txtStudentReg.Text);
                        IDNO = Convert.ToString((ds1.Tables[0].Rows[0]["IDNO"].ToString().Trim()));
                        BindEditData(Convert.ToInt32(IDNO), Convert.ToInt32(ddlSemester.SelectedValue));
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updInstall, "Installment Not Found.", this.Page);
                        SetInitialRow();
                        SetPreviousDataPercent();
                    }
            }

    }

    protected void btnExcel_Click(object sender, System.EventArgs e)
    {
        try
        {
            string SP_Name = "PKG_GET_FEES_SHOLARSHIP_DETAILS_EXCEL";
            string SP_Parameters = "@P_COLLEGE_ID";
            string Call_Values = "" + Convert.ToInt32(ddlFacultySchlorship.SelectedValue) + "";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count <= 0)
            {
                objCommon.DisplayMessage(updsholarship, "No Records Found.", this.Page);
                return;
            }
            else
            {
                GridView gvStudData = new GridView();
                gvStudData.DataSource = ds;
                gvStudData.DataBind();
                string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
                string attachment = "attachment; filename=StudentList.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Response.Write(FinalHead);
                gvStudData.RenderControl(htw);
                //string a = sw.ToString().Replace("_", " ");
                Response.Write(sw.ToString());
                Response.End();
                //   Response.Flush();
            }

        }
        catch (Exception ex)
        {
            objCommon = new Common();
            objCommon.DisplayMessage(updsholarship, ex.Message.ToString(), this);
        }
    }
}


