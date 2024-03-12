<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="DCR_ReportUI.aspx.cs" Inherits="Academic_DCR_ReportUI" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--   <link href="../plugins/multi-select/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../plugins/multi-select/bootstrap-multiselect.js"></script>--%>

    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <%--<script type="text/javascript">
function RunThisAfterEachAsyncPostback()
{

		$(function() {
				 $("#<%=txtToDate.ClientID%>").datepicker({
					changeMonth: true,
					changeYear: true,
					dateFormat: 'dd/mm/yy',
					yearRange: '1975:' + getCurrentYear()
				});
			});
			
			$(function() {
				 $("#<%=txtFromDate.ClientID%>").datepicker({
					changeMonth: true,
					changeYear: true,
					dateFormat: 'dd/mm/yy',
					yearRange: '1975:' + getCurrentYear()
				});
			});
			
			function getCurrentYear()
       {
var cDate = new Date(); 
return  cDate.getFullYear();  
       }
  }     
</script>--%>
    <%--<script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlFeeTable"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true">DCR summary  Report</asp:Label>
                            </h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Report Type</label>
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <asp:DropDownList ID="ddlreporttype" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true"
                                                TabIndex="1" OnSelectedIndexChanged="ddlreporttype_SelectedIndexChanged">
                                                <%-- <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Date-wise Fee Collections Report</asp:ListItem>
                                            <asp:ListItem Value="2">Bank-wise Demand Drafts Report</asp:ListItem>
                                            <asp:ListItem Value="3">Fee Item-wise Collections Report</asp:ListItem>
                                            <asp:ListItem Value="4">Outstanding Report</asp:ListItem>
                                            <asp:ListItem Value="5">Outstanding Report Format I</asp:ListItem>
                                            <asp:ListItem Value="6">Short DCR Report</asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RFVreporttype" runat="server" ControlToValidate="ddlreporttype"
                                                Display="None" ErrorMessage="Please Select The Report Type" InitialValue="0" ValidationGroup="report" />

                                            
                                            <%--<asp:RadioButton ID="rdoDatewiseFeeCollectionReport" Text="Date-wise Fee Collections Report" AutoPostBack="true"
                                            GroupName="ReportType" OnCheckedChanged="rdoDatewiseFeeCollectionReport_CheckedChanged" Checked="true" TabIndex="1" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoBankwiseDDReport" Text="Bank-wise Demand Drafts Report" style="display:none;"  AutoPostBack="true"
                                            GroupName="ReportType" TabIndex="1" runat="server" OnCheckedChanged="rdoBankwiseDDReport_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoFeeItemwiseCollectionReport" Text="Fee Item-wise Collections Report" style="display:none;" AutoPostBack="true"
                                            GroupName="ReportType" TabIndex="1" runat="server" OnCheckedChanged="rdoFeeItemwiseCollectionReport_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoOutstandingReport" Text="Outstanding Report" AutoPostBack="true"
                                            GroupName="ReportType" TabIndex="1" runat="server" OnCheckedChanged="rdoOutstandingReport_CheckedChanged1" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                            <br />
                                            <%--<asp:RadioButton ID="rdoOutstandingrptFormat1" Text="Outstanding Report Format I" AutoPostBack="true"
                                            GroupName="ReportType" TabIndex="1" runat="server" OnCheckedChanged="rdoOutstandingrptFormat1_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        
                                           <asp:RadioButton ID="rdoShortDcrReport" Text="Short DCR Report" style="display:none;" AutoPostBack="true"
                                               GroupName="ReportType" TabIndex="1" runat="server" OnCheckedChanged="rdoShortDcrReport_CheckedChanged" Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            --%>
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <span class="form-group col-12" id="divdetailsumary" runat="server">
                                                <asp:RadioButton ID="rdoDetailedReport" Text="Detailed Report" CssClass="data_label"
                                                    Checked="true" GroupName="RptSubType" TabIndex="2" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoSummeryReport" Text="Summary Report" CssClass="data_label"
                                            GroupName="RptSubType" TabIndex="2" runat="server" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <%--<asp:Label ID="note" runat="server" ForeColor="Red"> (For Outstanding report generation :  Select 'To Date' and Receipt Type)</asp:Label>--%>

                                <asp:Label ID="Label3" runat="server" Font-Bold="true" />
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-12 d-none">
                                        <asp:RadioButton ID="rdoTransferReport" Text="PNB Collection" Visible="false"
                                            GroupName="ReportType" TabIndex="1" runat="server" AutoPostBack="true"
                                            OnCheckedChanged="rdoTransferReport_CheckedChanged" />

                                        <asp:RadioButton ID="rdofeecolother" Text="Other Bank Collection" Visible="false"
                                            GroupName="ReportType" TabIndex="1" runat="server" AutoPostBack="true"
                                            OnCheckedChanged="rdofeecolother_CheckedChanged" />

                                        <asp:RadioButton ID="rdodcb" Text="DCB Report" Visible="false"
                                            GroupName="ReportType" TabIndex="1" runat="server" AutoPostBack="true"
                                            OnCheckedChanged="rdodcb_CheckedChanged" />

                                        <asp:RadioButton ID="rdomiscregfee" Text="Misc and Daily FeeCollection" Visible="false"
                                            GroupName="ReportType" TabIndex="1" runat="server" AutoPostBack="true"
                                            OnCheckedChanged="rdomiscregfee_CheckedChanged" />

                                        <asp:RadioButton ID="rdopnbwithcheque" Text="PNB Collection With Cheque" Visible="false"
                                            GroupName="ReportType" TabIndex="1" runat="server" AutoPostBack="true"
                                            OnCheckedChanged="rdopnbwithcheque_CheckedChanged" />

                                        <asp:RadioButton ID="rdbotherbankwithcheque" Text="Other Bank Collection With Cheque"
                                            GroupName="ReportType" TabIndex="1" runat="server" AutoPostBack="true" Visible="false"
                                            OnCheckedChanged="rdbotherbankwithcheque_CheckedChanged" />
                                    </div>

                                    <div class="form-group col-12" id="outfieldset" visible="false" runat="server">
                                        <asp:CheckBox ID="ChkShowAllStudent" Text="Show All Students" GroupName="ReportType"
                                            Checked="true" runat="server" Visible="false" AutoPostBack="true"
                                            OnCheckedChanged="ChkShowAllStudent_CheckedChanged" />
                                        <asp:CheckBox ID="ChkShowStudentsWithBalance" Text="Show Students having Balance."
                                            GroupName="ReportType" runat="server" Visible="false" AutoPostBack="true"
                                            OnCheckedChanged="ChkShowStudentsWithBalance_CheckedChanged" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Criteria for Report Generation</h5>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="fromDSpan" runat="server" visible="true">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgCalFromDate">
                                                <%-- <asp:Image ID="imgCalFromDate" runat="server" src="../images/calendar.png" Style="cursor: pointer" /> --%>
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="3" CssClass="form-control" />

                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                        </div>
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeFromDate" ControlToValidate="txtFromDate"
                                            IsValidEmpty="False" EmptyValueMessage="From date is required" InvalidValueMessage="From date is invalid" ForeColor="Red"
                                            InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="report" Enabled="true" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgCalToDate">
                                                <%-- <asp:Image ID="imgCalToDate" runat="server" src="../images/calendar.png" Style="cursor: pointer" />--%>
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" TabIndex="4" CssClass="form-control" />

                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />

                                        </div>
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meeToDate" ControlToValidate="txtToDate"
                                            IsValidEmpty="False" EmptyValueMessage="To date is required" InvalidValueMessage="To date is invalid" ForeColor="Red"
                                            InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="report" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Counter No.</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCounterNo" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="5" />
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Payment Mode</label>
                                        </div>
                                        <asp:DropDownList ID="ddlPaymentMode" runat="server" AppendDataBoundItems="true"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="6" />
                                        <%--<asp:CompareValidator ID="valPayMode" runat="server" ControlToValidate="ddlPaymentMode"
                                            Display="None" ErrorMessage="Please select payment mode." Operator="NotEqual" SetFocusOnError="true"
                                            Type="String" ValidationGroup="report" ValueToCompare="Please Select" />--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divrectypesingle" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Receipt Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlReceiptType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                            TabIndex="7">
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlReceiptType"
                                            Display="None" ErrorMessage="Please Select The Receipt Type" InitialValue="0" ValidationGroup="report" />--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divrectype" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Receipt Type</label>
                                        </div>
                                        <div class="well" style="max-height: 150px; overflow: auto;">
                                            <ul id="check-list-box" class="list-group checked-list-box">
                                                <li class="list-group-item">
                                                    <asp:CheckBoxList ID="chkrectpe" runat="server" AppendDataBoundItems="true" TabIndex="7" BorderStyle="None">
                                                    </asp:CheckBoxList></li>
                                            </ul>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Payment Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                            TabIndex="8" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpay" visible="false">
                                        <div class="label-dynamic">
                                            <label>Pay Type</label>
                                        </div>
                                        <asp:DropDownList runat="server" ID="ddlPaytype" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divOnline" visible="false">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>

                                        <asp:CheckBox ID="chkonline" runat="server" Text="Online" Style="border-radius: inherit" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="idDataFilter" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Data Filters</h5>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divcol" runat="server" >
                                            <div class="label-dynamic">
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCol" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                TabIndex="9" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlCol_SelectedIndexChanged" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Degree</label>
                                                <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                TabIndex="9" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divYr" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Year</label>
                                                <asp:Label ID="lblDYddlYear" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                TabIndex="10" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divYrLst" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Year</label>
                                                <asp:Label ID="lblDYtxtYear" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:ListBox ID="ddlYearList" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Branch</label>
                                                <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                TabIndex="11">
                                                <asp:ListItem Value="0">Please select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Semester</label>
                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                TabIndex="12" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlOSReportFormat1" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Outstanding Report Format I</h5>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div3" runat="server">
                                            <div class="label-dynamic">
                                                <label>School/College</label>
                                                <asp:Label ID="lblDYddlSchool_Tab" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:ListBox ID="lbOSCollege" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AutoPostBack="true"
                                                AppendDataBoundItems="true" OnSelectedIndexChanged="lbOSCollege_SelectedIndexChanged"></asp:ListBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div4" runat="server">
                                            <div class="label-dynamic">
                                                <label>Degree</label>
                                                <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:ListBox ID="lbOSDegree" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo"
                                                AppendDataBoundItems="true" OnSelectedIndexChanged="lbOSDegree_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div5" runat="server">
                                            <div class="label-dynamic">
                                                <label>Branch</label>
                                                <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:ListBox ID="lbOSBranch" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"
                                                OnSelectedIndexChanged="lbOSBranch_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div6" runat="server">
                                            <div class="label-dynamic">
                                                <label>Semester</label>
                                                <asp:Label ID="lblDYddlSemester_Tab2" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:ListBox ID="lbOSSemester" runat="server" SelectionMode="Multiple" AutoPostBack="true" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div2" runat="server">
                                            <div class="label-dynamic">
                                                <label>Year</label>
                                            </div>
                                            <asp:ListBox ID="lbOSYear" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                        </div>

                                    </div>
                                </asp:Panel>
                            </div>


                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click"
                                    TabIndex="13" ValidationGroup="report" CssClass="btn btn-outline-info" />
                                <asp:Button ID="btnExcel" runat="server" Text="OutStanding Report(Excel)" Style="display: none;" OnClick="btnExcel_Click"
                                    CssClass="btn btn-outline-info" Enabled="true" />
                                <asp:Button ID="btnExcelFormat2" runat="server" Text="O/S Report(Excel) Format - I" OnClick="btnExcelFormat2_Click" Enabled="false"
                                    CssClass="btn btn-outline-info" Visible="true" />

                             
                                <asp:Button ID="btnExcelFormat3" runat="server" Text="O/S Report(Excel) Format - II" OnClick="btnExcelFormat3_Click" Visible="false"
                                    CssClass="btn btn-info" />

                                <asp:Button ID="btnExcelFormat4" runat="server" Text="O/S Report(Excel) Format - III" OnClick="btnExcelFormat4_Click" CssClass="btn btn-info" Visible="false" />



                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    OnClick="btnCancel_Click" TabIndex="14" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="report" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="ColSumreport" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:PostBackTrigger ControlID="btnExcelFormat2" />
            <asp:PostBackTrigger ControlID="btnExcelFormat3" />
            <asp:PostBackTrigger ControlID="btnExcelFormat4" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>
    <script>
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200
                });
            });
        });
    </script>
</asp:Content>
