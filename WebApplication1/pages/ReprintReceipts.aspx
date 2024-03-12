<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ReprintReceipts.aspx.cs" Inherits="Academic_ReprintReceipts" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .dynamic-nav-tabs li.active a {
            color: #255282;
            background-color: #fff;
            border-top: 3px solid #255282 !important;
            border-color: #255282 #255282 #fff !important;
        }

        .chiller-theme .nav-tabs.dynamic-nav-tabs > li.active {
            border-radius: 8px;
            background-color: transparent !important;
        }
    </style>

    <script type="text/javascript">
        function getCurrentYear() {
            var cDate = new Date();
            return cDate.getFullYear();
        }

        function CheckDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                sender._selectedDate = new Date();
                alert("Future Date Not Accepted!");
                document.getElementById('ctl00_ContentPlaceHolder1_Txtfrommisc').value = '';
                document.getElementById('ctl00_ContentPlaceHolder1_Txttomisc').value = '';
                document.getElementById('ctl00_ContentPlaceHolder1_txtFromDate').value = '';
                document.getElementById('ctl00_ContentPlaceHolder1_txtToDate').value = '';

                //var startDate = new Date($('#Txtfrommisc').val());
                //var endDate = new Date($('#Txttomisc').val());

                //if (startDate < endDate) {
                //    alert("To date Should be Greater than From date!");
                //    endDate.value = '';
                //    endDate.value = '';
                //}
            }
        }

    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="nav-tabs-custom mt-2 col-12" id="myTabContent">
                    <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                        <li class="nav-item active" id="divlkrecept" runat="server">
                            <asp:LinkButton ID="lkreceipt" runat="server" OnClick="lkreceipt_Click" CssClass="nav-link" TabIndex="1">Reprint or Cancel Receipt</asp:LinkButton></li>
                        <li class="nav-item" id="divlkcancel" runat="server">
                            <asp:LinkButton ID="lnkcancel" runat="server" OnClick="lnkcancel_Click" CssClass="nav-link" TabIndex="2">Receipt Cancellation</asp:LinkButton></li>

                        <li class="nav-item" id="divlkmiscreprint" runat="server">
                            <asp:LinkButton ID="lnkmiscreprint" runat="server" OnClick="lnkmiscreprint_Click" CssClass="nav-link" TabIndex="3">Misc Reprint or Cancel Receipt</asp:LinkButton></li>

                        <li class="nav-item" id="divlkmisccancel" runat="server">
                            <asp:LinkButton ID="lnkmisccancel" runat="server" OnClick="lnkmisccancel_Click" CssClass="nav-link" TabIndex="4">Misc Receipt Cancellation</asp:LinkButton></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade show active" id="divreceipt" role="tabpanel" runat="server" aria-labelledby="receipt-tab">
                            <%-- <div>
                                <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlFeeTable"
                                    DynamicLayout="true" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <div id="preloader">
                                            <div class="loader-container">
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__ball"></div>
                                            </div>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>--%>

                            <asp:UpdatePanel ID="pnlFeeTable" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Search Receipt</h5>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Search by</label>
                                                    </div>
                                                    <asp:RadioButton ID="rdoReceiptNo" runat="server" Text="Receipt No."
                                                        GroupName="search" OnCheckedChanged="rdoReceiptNo_CheckedChanged" AutoPostBack="true" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoEnrollmentNo" runat="server" Text="Reg No."
                                            GroupName="search" Checked="true" OnCheckedChanged="rdoEnrollmentNo_CheckedChanged" AutoPostBack="true" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdbStudentName" runat="server" Text="Student Name"
                                            GroupName="search" Checked="true" OnCheckedChanged="rdoEnrollmentNo_CheckedChanged" AutoPostBack="true" />
                                                </div>

                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label></label>
                                                    </div>
                                                    <span class="form-inline">
                                                        <asp:TextBox ID="txtSearchText" runat="server" ToolTip="Enter text to search." CssClass="form-control" />
                                                        <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtSearchText"
                                                            Display="None" ErrorMessage="Please enter text to search." SetFocusOnError="true"
                                                            ValidationGroup="search" />

                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                            ValidationGroup="search" CssClass="btn btn-outline-primary m-top" />
                                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                            ShowSummary="false" ValidationGroup="search" />
                                                    </span>
                                                </div>

                                                <div class="form-group col-lg-5 col-md-12 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Reason of Cancelling Receipt</label>
                                                    </div>
                                                    <asp:TextBox ID="txtRemark" Rows="4" TextMode="MultiLine" CssClass="form-control" runat="server" />
                                                </div>

                                            </div>

                                            <div class="col-12">
                                                <asp:ListView ID="lvPaidReceipts" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="listViewGrid" class="vista-grid">
                                                            <div class="sub-heading">
                                                                <h5>Search Results</h5>
                                                            </div>

                                                            <table id="tblSearchResults" class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Select
                                                                        </th>
                                                                        <th>Student Name
                                                                        </th>
                                                                        <th>Receipt No
                                                                        </th>
                                                                        <th>Receipt Date
                                                                        </th>
                                                                        <th>Receipt Type
                                                                        </th>
                                                                        <th>Semester / Year
                                                                        </th>
                                                                        <th>Mode
                                                                        </th>
                                                                        <th>Amount
                                                                        </th>
                                                                        <th>Reconcile Remark
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <input
                                                                    id="rdoSelectRecord" value='<%# Eval("DCR_NO") %>' name="Receipts" type="radio" /><asp:HiddenField
                                                                        ID="hidRemark" runat="server" Value='<%# Eval("REMARK") %>' />
                                                                <asp:HiddenField ID="hidDcrNo" runat="server" Value='<%# Eval("DCR_NO") %>' />
                                                                <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />

                                                            </td>
                                                            <td>
                                                                <%# Eval("NAME") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("REC_NO") %>
                                                            </td>
                                                            <td>
                                                                <%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("RECIEPT_TITLE") %>
                                                            </td>
                                                            <%--<td>
                                                    <%# Eval("SEMESTERNAME")%>
                                                </td>--%>
                                                            <td><%#Convert.ToInt32(Eval("YEARWISE"))==1?Eval("YEARNAME"):Eval("SEMESTERNAME") %></td>
                                                            <td>
                                                                <%# Eval("PAY_MODE_CODE")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("TOTAL_AMT")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("REMARK")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>

                                        </div>
                                         <div class="col-12 btn-footer">
                                            <input id="btnReprint" onclick="ReprintReceipt();" runat="server" type="button" value="Reprint Receipt"
                                                disabled="disabled" class="btn btn-outline-info" />
                                            <input id="btnEdit" onclick="EditReceipt();" runat="server" type="button" value="Edit Receipt"
                                                disabled="disabled" class="btn btn-outline-info" visible="false" />
                                            <input id="btnCancel" onclick="CancelReceipt();" runat="server" type="button" value="Cancel Receipt"
                                                disabled="disabled" class="btn btn-outline-danger" />
                                        </div>

                                        <div class="col-12">
                                            <asp:Panel ID="panelEditing" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>DD/Check No.</label>
                                                        </div>
                                                        <asp:TextBox ID="txtDDNo" runat="server" TabIndex="7" CssClass="form-control" />
                                                        <asp:RequiredFieldValidator ID="valDDNo" ControlToValidate="txtDDNo" runat="server"
                                                            Display="None" ErrorMessage="Please enter demand draft number." ValidationGroup="dd_info" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Amount</label>
                                                        </div>
                                                        <asp:TextBox ID="txtDDAmount" onkeyup="IsNumeric(this);" runat="server" TabIndex="8"
                                                            CssClass="form-control" Enabled="False" />
                                                        <asp:RequiredFieldValidator ID="valDdAmount" ControlToValidate="txtDDAmount" runat="server"
                                                            Display="None" ErrorMessage="Please enter amount of demand draft." ValidationGroup="dd_info" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Date </label>
                                                        </div>
                                                        <div class="input-group">
                                                            <div class="input-group-addon" id="imgCalDDDate">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtDDDate" runat="server" TabIndex="9" CssClass="form-control" />
                                                            <ajaxToolKit:CalendarExtender ID="ceDDDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDDDate"
                                                                PopupButtonID="imgCalDDDate" />
                                                            <ajaxToolKit:MaskedEditExtender ID="meeDDDate" runat="server" TargetControlID="txtDDDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                                OnInvalidCssClass="errordate" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevDDDate" runat="server" ControlExtender="meeDDDate"
                                                                ControlToValidate="txtDDDate" IsValidEmpty="False" EmptyValueMessage="Demand draft date is required"
                                                                InvalidValueMessage="Demand draft date is invalid" EmptyValueBlurredText="*"
                                                                InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="dd_info" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>City </label>
                                                        </div>
                                                        <asp:TextBox ID="txtDDCity" runat="server" TabIndex="10" CssClass="form-control" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Bank </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlBank" AppendDataBoundItems="True" TabIndex="11" runat="server"
                                                            CssClass="form-control" data-select2-enable="true" AutoPostBack="True" />
                                                        <asp:RequiredFieldValidator ID="valBankName" runat="server" ControlToValidate="ddlBank"
                                                            Display="None" ErrorMessage="Please select bank name." ValidationGroup="dd_info"
                                                            InitialValue="0" SetFocusOnError="true" />

                                                        <asp:HiddenField ID="hfDcrNum" runat="server" />
                                                        <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                            ShowSummary="false" ValidationGroup="dd_info" />
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:HiddenField ID="hfIdno" runat="server" />
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnDD_Info" runat="server" OnClick="btnDD_Info_Click"
                                                            TabIndex="12" Text="Update Demand Draft" ValidationGroup="dd_info" CssClass="btn btn-outline-info" />
                                                        <asp:Button ID="BtnCancelDD" runat="server" OnClick="BtnCancelDD_Click"
                                                            Text="Cancel" CssClass="btn btn-outline-danger" />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>

                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSearch" />
                                    <asp:PostBackTrigger ControlID="btnDD_Info" />
                                    <asp:PostBackTrigger ControlID="BtnCancelDD" />
                                    <asp:PostBackTrigger ControlID="btnReprint" />
                                    <asp:PostBackTrigger ControlID="btnCancel" />
                                    <asp:PostBackTrigger ControlID="btnEdit" />
                                    <%--<asp:PostBackTrigger ControlID="btnPrintLedgerReport" />--%>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div id="divcancel" runat="server" visible="false" role="tabpanel" aria-labelledby="cancel-tab">
                            <%-- <div>
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updcancel"
                                    DynamicLayout="true" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <div id="preloader">
                                            <div class="loader-container">
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__ball"></div>
                                            </div>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>--%>
                            <asp:UpdatePanel ID="updcancel" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Report Types</h5>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-12 col-md-12 col-12">
                                                    <asp:RadioButton ID="rdoCancelReciept" Text="Cancel Receipt Report" CssClass="data_label"
                                                        Checked="true" GroupName="Reciept" TabIndex="1" runat="server" />&nbsp;&nbsp;
                                <asp:RadioButton ID="rdoChalanCancel" Text="Deleted Chalan  Report" Visible="false" CssClass="data_label"
                                    GroupName="Reciept" TabIndex="2" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 mt-2">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Criteria for Report Generation</h5>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>From Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="imgCalFromDate">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtFromDate" runat="server" TabIndex="3" CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckDate"
                                                            TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                                                            Display="None" ErrorMessage="Please enter initial date for report." SetFocusOnError="true"
                                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                                        <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFromDate" 
                                                           SetFocusOnError="true"  Display="None" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                                            ErrorMessage="Invalid date format." ValidationGroup="report" />--%>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>To Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="imgCalToDate2">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtToDate" runat="server" TabIndex="4" CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckDate"
                                                            TargetControlID="txtToDate" PopupButtonID="imgCalToDate2" Enabled="true" EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                                                            Display="None" ErrorMessage="Please enter last date for report." SetFocusOnError="true"
                                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <%--   <asp:CompareValidator ID="CompareValidator1" ValidationGroup="report" ForeColor="Red" runat="server"
                                                            ControlToValidate="txtFromDate" ControlToCompare="txtToDate" Operator="LessThan" Type="Date" Display="None"
                                                            ErrorMessage="From date must be less than To date."></asp:CompareValidator>--%>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <label>Counter No.</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCounterNo" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        TabIndex="5">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <label>Receipt Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlReceiptType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        TabIndex="6">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 mt-2">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Data Filters</h5>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Faculty/School Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlcollege" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true"
                                                        TabIndex="7" OnSelectedIndexChanged="ddlcollege_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Program</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        TabIndex="7">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <label>Year</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="8">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Branch</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="9">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>--%>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Semester</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="10">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" TabIndex="11" ValidationGroup="report" CssClass="btn btn-outline-primary" />
                                            <asp:Button ID="btnCancelre" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancelre_Click" TabIndex="12" CssClass="btn btn-outline-danger" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="report" />
                                        </div>
                                        <div id="div2" runat="server">
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnReport" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div id="divmiscreprint" runat="server" visible="false" role="tabpanel" aria-labelledby="cancel-tab">
                            <%-- <div>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updMiscreprint"
                                    DynamicLayout="true" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <div id="preloader">
                                            <div class="loader-container">
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__ball"></div>
                                            </div>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div--%>

                            <asp:UpdatePanel ID="updMiscreprint" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Search Receipt</h5>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Search by</label>
                                                    </div>
                                                    <asp:RadioButton ID="radiomiscreprint1" runat="server" Text="Receipt No."
                                                        GroupName="search" OnCheckedChanged="radiomiscreprint1_CheckedChanged" AutoPostBack="true" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="radiomiscreprint2" runat="server" Text="Reg No."
                                            GroupName="search" Checked="true" OnCheckedChanged="radiomiscreprint2_CheckedChanged" AutoPostBack="true" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rbdMisStudentName" runat="server" Text="Student Name"
                                            GroupName="search" Checked="true" OnCheckedChanged="radiomiscreprint2_CheckedChanged" AutoPostBack="true" />
                                                </div>

                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label></label>
                                                    </div>
                                                    <span class="form-inline">
                                                        <asp:TextBox ID="TxtSearchMisc" runat="server" ToolTip="Enter text to search." CssClass="form-control" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtSearchMisc"
                                                            Display="None" ErrorMessage="Please enter text to search." SetFocusOnError="true"
                                                            ValidationGroup="search" />

                                                        <asp:Button ID="btnsearchmisc" runat="server" Text="Search" OnClick="btnsearchmisc_Click"
                                                            ValidationGroup="search" CssClass="btn btn-outline-primary m-top" />
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                            ShowSummary="false" ValidationGroup="search" />
                                                    </span>
                                                </div>

                                                <div class="form-group col-lg-5 col-md-12 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Reason of Cancelling Receipt</label>
                                                    </div>
                                                    <asp:TextBox ID="TxtRemarkmisc" Rows="4" TextMode="MultiLine" CssClass="form-control" runat="server" />
                                                </div>

                                            </div>

                                            <div class="col-12">
                                                <asp:ListView ID="LvMiscReceipt" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="listViewGrid" class="vista-grid">
                                                            <div class="sub-heading">
                                                                <h5>Search Results</h5>
                                                            </div>

                                                            <table id="tblSearchResultsmisc" class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Select
                                                                        </th>
                                                                        <th>Student Name
                                                                        </th>
                                                                        <th>Receipt Date
                                                                        </th>
                                                                        <th>Receipt No.
                                                                        </th>
                                                                        <th>Semester / Year
                                                                        </th>
                                                                        <th>Receipt Type
                                                                        </th>
                                                                        <th>Amount
                                                                        </th>
                                                                        <th>Narration
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%--<asp:RadioButton ID="rdoSelectRecord" runat="server"--%>
                                                                <input
                                                                    id="rdoSelectRecord" value='<%# Eval("MISCDCRSRNO") %>' name="Receipts" type="radio" /><asp:HiddenField
                                                                        ID="hidRemark" runat="server" Value='<%# Eval("REMARK") %>' />
                                                                <asp:HiddenField ID="hidDcrNo" runat="server" Value='<%# Eval("MISCDCRSRNO") %>' />
                                                                <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />

                                                            </td>
                                                            <td>
                                                                <%# Eval("NAME") %>
                                                            </td>

                                                            <td>
                                                                <%# (Eval("MISCRECPTDATE").ToString() != string.Empty) ? ((DateTime)Eval("MISCRECPTDATE")).ToShortDateString() : Eval("MISCRECPTDATE") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("COUNTR") %>
                                                            </td>

                                                            <td><%#Convert.ToInt32(Eval("YEARWISE"))==1?Eval("YEARNAME"):Eval("SEMESTERNAME") %></td>
                                                            <td>
                                                                <%# Eval("RECIEPT_TITLE")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("CHDDAMT")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("NARRATION")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>

                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnmiscreprint" runat="server" CssClass="btn btn-outline-info" Text="Reprint Receipt" OnClick="btnmiscreprint_Click" OnClientClick=" return ReprintReceiptMisc(this);" Enabled="false" />
                                            <asp:Button ID="btnmisccancel" runat="server" CssClass="btn btn-outline-danger" Text="Cancel Receipt" OnClick="btnmisccancel_Click" OnClientClick=" return CancelReceiptMisc(this);" Enabled="false" />

                                        </div>

                                        <div class="col-12">
                                            <asp:Panel ID="panel1" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>DD/Check No.</label>
                                                        </div>
                                                        <asp:TextBox ID="TextBox3" runat="server" TabIndex="7" CssClass="form-control" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtDDNo" runat="server"
                                                            Display="None" ErrorMessage="Please enter demand draft number." ValidationGroup="dd_info" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Amount</label>
                                                        </div>
                                                        <asp:TextBox ID="TextBox4" onkeyup="IsNumeric(this);" runat="server" TabIndex="8"
                                                            CssClass="form-control" Enabled="False" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtDDAmount" runat="server"
                                                            Display="None" ErrorMessage="Please enter amount of demand draft." ValidationGroup="dd_info" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Date </label>
                                                        </div>
                                                        <div class="input-group">
                                                            <div class="input-group-addon" id="Div1">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <asp:TextBox ID="TextBox5" runat="server" TabIndex="9" CssClass="form-control" />
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDDDate"
                                                                PopupButtonID="imgCalDDDate" />
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtDDDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                                OnInvalidCssClass="errordate" />
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeDDDate"
                                                                ControlToValidate="txtDDDate" IsValidEmpty="False" EmptyValueMessage="Demand draft date is required"
                                                                InvalidValueMessage="Demand draft date is invalid" EmptyValueBlurredText="*"
                                                                InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="dd_info" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>City </label>
                                                        </div>
                                                        <asp:TextBox ID="TextBox6" runat="server" TabIndex="10" CssClass="form-control" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Bank </label>
                                                        </div>
                                                        <asp:DropDownList ID="DropDownList1" AppendDataBoundItems="True" TabIndex="11" runat="server"
                                                            CssClass="form-control" data-select2-enable="true" AutoPostBack="True" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlBank"
                                                            Display="None" ErrorMessage="Please select bank name." ValidationGroup="dd_info"
                                                            InitialValue="0" SetFocusOnError="true" />

                                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                            ShowSummary="false" ValidationGroup="dd_info" />
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="Button5" runat="server" OnClick="btnDD_Info_Click"
                                                            TabIndex="12" Text="Update Demand Draft" ValidationGroup="dd_info" CssClass="btn btn-outline-info" />
                                                        <asp:Button ID="Button6" runat="server" OnClick="BtnCancelDD_Click"
                                                            Text="Cancel" CssClass="btn btn-outline-danger" />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>

                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnsearchmisc" />
                                    <asp:PostBackTrigger ControlID="btnmiscreprint" />
                                    <asp:PostBackTrigger ControlID="btnmisccancel" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div id="divmisccancel" runat="server" visible="false" role="tabpanel" aria-labelledby="cancel-tab">
                            <%-- <div>
                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updmisccancel"
                                    DynamicLayout="true" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <div id="preloader">
                                            <div class="loader-container">
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__bar"></div>
                                                <div class="loader-container__ball"></div>
                                            </div>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>--%>
                            <asp:UpdatePanel ID="updmisccancel" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Report Types</h5>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-12 col-md-12 col-12">
                                                    <asp:RadioButton ID="RadioButton1" Text="Cancel Receipt Report" CssClass="data_label"
                                                        Checked="true" GroupName="Reciept" TabIndex="1" runat="server" />&nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton2" Text="Deleted Chalan  Report" CssClass="data_label"
                                    GroupName="Reciept" TabIndex="2" runat="server" Visible="false" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 mt-2">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Criteria for Report Generation</h5>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>From Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="Div4">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="Txtfrommisc" runat="server" TabIndex="3" CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckDate"
                                                            TargetControlID="Txtfrommisc" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Txtfrommisc"
                                                            Display="None" ErrorMessage="Please enter initial date for report." SetFocusOnError="true"
                                                            ValidationGroup="reportms"></asp:RequiredFieldValidator>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="Txtfrommisc"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>To Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="Div5">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="Txttomisc" runat="server" TabIndex="4" CssClass="form-control" onblur="return chekvalid(this);"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckDate"
                                                            TargetControlID="Txttomisc" PopupButtonID="imgCalToDate2" Enabled="true" EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Txttomisc"
                                                            Display="None" ErrorMessage="Please enter last date for report." SetFocusOnError="true"
                                                            ValidationGroup="reportms"></asp:RequiredFieldValidator>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="Txttomisc"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <%--   <asp:CompareValidator ID="CompareValidator1" ValidationGroup="report" ForeColor="Red" runat="server"
                                                            ControlToValidate="txtFromDate" ControlToCompare="txtToDate" Operator="LessThan" Type="Date" Display="None"
                                                            ErrorMessage="From date must be less than To date."></asp:CompareValidator>--%>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <label>Counter No.</label>
                                                    </div>
                                                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        TabIndex="5">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <label>Receipt Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        TabIndex="6">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 mt-2">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Data Filters</h5>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Faculty/School Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlclgmisc" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlclgmisc_SelectedIndexChanged"
                                                        TabIndex="7">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Program</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlpgmmisc" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        TabIndex="7">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>





                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Semester</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlsemmisc" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="10">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnreportmisc" runat="server" Text="Report" OnClick="btnreportmisc_Click" TabIndex="11" ValidationGroup="reportms" CssClass="btn btn-outline-primary" />
                                            <asp:Button ID="btncan" runat="server" Text="Cancel" OnClick="btncan_Click" CausesValidation="false" TabIndex="12" CssClass="btn btn-outline-danger" />
                                            <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="reportms" />
                                        </div>
                                        <div id="div6" runat="server">
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnreportmisc" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divMsg" runat="server">
        </div>
    </div>


    <script type="text/javascript" language="javascript">

        function ShowLedgerReport() {
            try {
                __doPostBack("ShowLedgerReport", "");
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function ReprintReceipt() {
            try {
                if (ValidateRecordSelection()) {
                    __doPostBack("ReprintReceipt", "");
                }
                else {
                    alert("Please select a receipt to reprint.");
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function CancelReceipt() {
            try {
                if (ValidateRecordSelection()) {
                    if (document.getElementById('<%= txtRemark.ClientID %>').value.trim() != "") {
                        if (confirm("Do you really want to cancel this receipt.") && confirm("If you cancel this receipt, it will not be considered as paid.")) {
                            __doPostBack("CancelReceipt", "");
                        }
                    }
                    else
                        alert('Please enter reason of cancelling receipt.');
                }
                else {
                    alert("Please select a receipt to cancel.");
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function EditReceipt() {
            try {
                if (ValidateRecordSelection()) {
                    __doPostBack("EditReceipt", "");
                }
                else {
                    alert("Please select a receipt to Edit.");
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function ValidateRecordSelection() {
            var check = false;
            var gridView = document.getElementById("tblSearchResults");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "radio") {
                    if (checkBoxes[i].checked) {
                        check = true;
                    }

                }
            }
            return check;
        }
        function ValidateRecordSelectionMisc() {
            // alert("gg")
            var check = false;
            var gridView = document.getElementById("tblSearchResultsmisc");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "radio") {
                    if (checkBoxes[i].checked) {
                        check = true;
                    }

                }
            }
            return check;
        }
        function CancelReceiptMisc() {
            //alert("ca")
            if (confirm("Do you really want to cancel this receipt.") && confirm("If you cancel this receipt, it will not be considered as paid.")) {
                //__doPostBack("CancelReceipt", "");
            }
            try {
                if (ValidateRecordSelectionMisc()) {

                }
                else {
                    alert("Please select a receipt to cancel.");
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }
        function ReprintReceiptMisc() {
            //alert("ca")
            try {
                if (ValidateRecordSelectionMisc()) {

                }
                else {
                    alert("Please select a receipt to Reprint.");
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }



        function ShowRemark(rdoSelect) {
            try {
                if (rdoSelect != null && rdoSelect.nextSibling != null) {
                    var hidRemark = rdoSelect.nextSibling;
                    if (hidRemark != null)
                        document.getElementById('<%= txtRemark.ClientID %>').value = hidRemark.value;
            }
        }
        catch (e) {
            alert("Error: " + e.description);
        }
    }
    function btnEdit_onclick() {

    }

    function btnCancel_onclick() {

    }

    </script>

</asp:Content>
