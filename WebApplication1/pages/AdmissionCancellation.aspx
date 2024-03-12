<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="AdmissionCancellation.aspx.cs" Inherits="Academic_AdmissionCancellation" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" charset="utf-8">
        //$(document).ready(function () {

        //    $(".display").dataTable({
        //        "bJQueryUI": true
        //        //"sPaginationType": "full_numbers"
        //    });

        //});

        function RunThisAfterEachAsyncPostback() {
            $(function () {
                $("#<%=txtFromDate.ClientID%>").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd/mm/yy',
                    yearRange: '1975:' + getCurrentYear()
                });
            });

            function getCurrentYear() {
                var cDate = new Date();
                return cDate.getFullYear();
            }
        }

    </script>

    <script language="javascript" type="text/javascript">
        function SelectSingleCheckbox(Chkid) {
            var chkbid = document.getElementById(Chkid);
            var chkList = document.getElementsByTagName("input");
            for (i = 0; i < chkList.length; i++) {
                if (chkList[i].type == "checkbox" && chkList[i].id != chkbid.id) {
                    chkList[i].checked = false;
                }
            }
        }
    </script>

    <%--<script type="text/javascript">
        function CheckOnlyOneCheckBox(checkbox) {
            var checkBoxList = checkbox.parentNode.parentNode.parentNode;
            var list = checkBoxList.getElementsByTagName("input");
            for (var i = 0; i < list.length; i++) {
                if (list[i] != checkbox && checkbox.checked) {
                    list[i].checked = false;
                }
            }
        }
    </script>--%>



    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-body">
                    <div class="nav-tabs-custom">

                        <div class="nav-tabs-custom" id="tabs" role="tabpanel">

                            <ul class="nav nav-tabs" role="tablist">
                                <li class="nav-item"><a class="nav-link active" href="#sub" role="tab" data-toggle="tab" aria-controls="sub">ADMISSION WITHDRAW / CANCELLATION</a></li>
                                <li class="nav-item"><a class="nav-link " href="#sub_n" data-toggle="tab" role="tab" aria-controls="sub_n">RE_ADMISSION</a></li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane active" role="tabpanel" id="sub">
                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="pnlFeeTable"
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
                                    </div>
                                    <div>
                                        <asp:UpdatePanel ID="pnlFeeTable" runat="server">
                                            <ContentTemplate>

                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Search By</label>
                                                                    </div>
                                                                    <div class="radio">
                                                                        <label>
                                                                            <asp:RadioButton ID="rdoRollNo" runat="server" Text="Roll No." GroupName="search"
                                                                                Checked="true" TabIndex="1" />
                                                                        </label>
                                                                        <label>
                                                                            <asp:RadioButton ID="rdoStudName" runat="server" Text="Name" GroupName="search"
                                                                                TabIndex="2" />
                                                                        </label>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label runat="server" ID="lblstudlable" Font-Bold="true"> Enrollment No</asp:Label>
                                                                    </div>

                                                                    <asp:TextBox ID="txtSearchText" runat="server" CssClass="form-control"
                                                                        ToolTip="Enter text to search." TabIndex="3" />
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12 mt-3">
                                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                                        ValidationGroup="search" TabIndex="4" CssClass="btn btn-success" />
                                                                    <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtSearchText"
                                                                        Display="None" ErrorMessage="Please enter text to search." SetFocusOnError="true"
                                                                        ValidationGroup="search" />
                                                                    <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                        ShowSummary="false" ValidationGroup="search" />

                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="sub-heading">
                                                            <h5>Cancellation Report</h5>
                                                        </div>
                                                        <div class="col-12">
                                                            <div class="row">


                                                                <div class="form-group col-lg-4 col-md-6 col-12" id="trInstitute" runat="server" visible="True">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>

                                                                    <asp:DropDownList ID="ddlColllege" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlColllege_SelectedIndexChanged"
                                                                        AppendDataBoundItems="True" AutoPostBack="True" data-select2-enable="true"
                                                                        TabIndex="5">
                                                                        <asp:ListItem Value="0">Please select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvInstitute" runat="server" ControlToValidate="ddlColllege"
                                                                        Display="None" ErrorMessage="Please Select Faculty" SetFocusOnError="True" ValidationGroup="Cancel"
                                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-lg-4 col-md-6 col-12" id="trDegree" runat="server" visible="True">
                                                                    <div class="label-dynamic">

                                                                        <sup></sup>
                                                                        <label>Program</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true"
                                                                        AppendDataBoundItems="True" AutoPostBack="True"
                                                                        TabIndex="5">
                                                                        <asp:ListItem Value="0">Please select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <%-- <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                                        Display="None" ErrorMessage="Please Select Program" SetFocusOnError="True" ValidationGroup="Cancel"
                                                                        InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                                </div>
                                                                <div class="form-group col-lg-2 col-md-6 col-12" id="trFrmDate" runat="server" visible="True">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>From Date</label>
                                                                    </div>
                                                                    <div class="input-group">
                                                                        <div class="input-group-addon">
                                                                            <i class="fa fa-calendar text-blue" id="imgFromDate1"></i>
                                                                        </div>
                                                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"
                                                                            ValidationGroup="Cancel" TabIndex="7" ToolTip="Please Select Date"></asp:TextBox>

                                                                        <ajaxToolKit:CalendarExtender ID="ceFromdate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                            PopupButtonID="imgFromDate1" TargetControlID="txtFromDate" />
                                                                        <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server"
                                                                            TargetControlID="txtFromDate" Mask="99/99/9999" MessageValidatorTip="true"
                                                                            MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                                            ErrorTooltipEnabled="True" />
                                                                    </div>
                                                                </div>
                                                                <div class="form-group col-lg-2 col-md-6 col-12" id="trToDate" runat="server" visible="True">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>To Date</label>
                                                                    </div>
                                                                    <div class="input-group">
                                                                        <div class="input-group-addon">
                                                                            <i class="fa fa-calendar text-blue" id="imgToDate1"></i>
                                                                        </div>
                                                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" ValidationGroup="Cancel"
                                                                            TabIndex="9" ToolTip="Please Select Date"></asp:TextBox>

                                                                        <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                            PopupButtonID="imgToDate1" TargetControlID="txtToDate">
                                                                        </ajaxToolKit:CalendarExtender>
                                                                        <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server"
                                                                            TargetControlID="txtToDate" Mask="99/99/9999" MessageValidatorTip="true"
                                                                            MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                                        <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server"
                                                                            ControlExtender="meeToDate" ControlToValidate="txtToDate" Display="None"
                                                                            EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter To Date"
                                                                            InvalidValueBlurredMessage="Invalid Date"
                                                                            InvalidValueMessage="To Date is Invalid (Enter mm-dd-yyyy Format)"
                                                                            TooltipMessage="Please Enter To Date" ValidationGroup="Cancel" />
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click"
                                                                Text="Report" ValidationGroup="Cancel" TabIndex="11" CssClass="btn btn-outline-primary" />
                                                            <asp:Button ID="btnExcel" runat="server" OnClick="btnExcel_Click"
                                                                Text="Excel Report" ValidationGroup="Cancel" TabIndex="12" CssClass="btn btn-outline-primary" />
                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                                TabIndex="12" CssClass="btn btn-outline-danger" />
                                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFromDate"
                                                                Display="None" ErrorMessage="Please enter from date" SetFocusOnError="true"
                                                                ValidationGroup="Cancel" Width="10%" ID="rfvFromDate" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server"
                                                                ControlExtender="meeFromDate" ControlToValidate="txtFromDate" Display="None"
                                                                EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter From Date"
                                                                InvalidValueBlurredMessage="Invalid Date"
                                                                InvalidValueMessage="From Date is Invalid (Enter mm-dd-yyyy Format)"
                                                                SetFocusOnError="True" TooltipMessage="Please Enter From Date"
                                                                ValidationGroup="Cancel" />
                                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtToDate"
                                                                Display="None" ErrorMessage="Please enter to date" SetFocusOnError="true"
                                                                ValidationGroup="Cancel" Width="10%" ID="rfvToDate" />
                                                            <asp:ValidationSummary ID="ValCancelSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                ShowSummary="false" ValidationGroup="Cancel" />
                                                        </div>



                                                        <div class="col-12">
                                                            <asp:ListView ID="lvSearchResults" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Search Results</h5>
                                                                    </div>
                                                                    <table id="tblSearchResults" class="table table-hover table-bordered table-striped display" style="width: 100%">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th>Select</th>
                                                                                <th>Student ID</th>
                                                                                <th>Name</th>
                                                                                <th>Faculty</th>
                                                                                <th>Program</th>
                                                                                <th>Sem.</th>
                                                                                <th>Admission Date</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <EmptyDataTemplate></EmptyDataTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <input id="rdoSelectRecord" name="Student" type="radio" value='<%# Eval("IDNO") %>' checked='<%# (Eval("CAN")).ToString() == "1" ? true : false %>' /></td>

                                                                        <td><%# Eval("ENROLLMENTNO")%></td>
                                                                        <td><%# Eval("NAME") %></td>
                                                                        <td><%# Eval("COLLEGENAME")%></td>
                                                                        <td><%# Eval("LONGNAME")%></td>
                                                                        <td><%# Eval("SEMESTERNAME")%></td>
                                                                        <td><%# (Eval("ADMDATE").ToString() != string.Empty) ? ((DateTime)Eval("ADMDATE")).ToShortDateString() : "-" %></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>

                                                        </div>

                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divRemark" runat="server" visible="false">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Reason of Cancelling Student Admission</label>
                                                                    </div>

                                                                    <asp:TextBox ID="txtRemark" Rows="4" TextMode="MultiLine" Width="450px" MaxLength="400"
                                                                        runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-12 btn-footer">

                                                            <asp:Button ID="btnCancelAdmission" runat="server" OnClientClick="CancelAdmission(); return false" Text="Cancel Admission" Visible="false" CssClass="btn btn-success" />

                                                            <asp:Button ID="btnCancelAdmissionSlip" runat="server" Text="Cancel Admission Slip" Visible="false" Enabled="false" CssClass="btn btn-outline-danger" OnClick="btnCancelAdmissionSlip_Click" />


                                                        </div>

                                                        <div id="divMsg" runat="server"></div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnCancelAdmission" />
                                                <asp:PostBackTrigger ControlID="btnCancelAdmissionSlip" />
                                                <asp:PostBackTrigger ControlID="btnSearch" />
                                                <asp:PostBackTrigger ControlID="btnReport" />
                                                <asp:PostBackTrigger ControlID="btnExcel" />
                                                <%-- <asp:PostBackTrigger ControlID="lvSearchResults" />--%>
                                                <%--<asp:PostBackTrigger ControlID="lvSearchResults" />--%>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div class="tab-pane" id="sub_n" role="tabpanel">
                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updReadmit"
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
                                    </div>
                                    <div>
                                        <asp:UpdatePanel ID="updReadmit" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Enrollment No</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtApplicationID" runat="server" MaxLength="15" TabIndex="1" CssClass="form-control" />
                                                                    <asp:RequiredFieldValidator ID="rfvApplicationID" runat="server" ErrorMessage="Please Enter Student REG NO. / ENROLLMENT NO."
                                                                        ControlToValidate="txtApplicationID" Display="None" SetFocusOnError="True" ValidationGroup="Show" />
                                                                    <span class="input-group-btn"><i class="material-icons left"></i>
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12 mt-3">
                                                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-success" TabIndex="2" ValidationGroup="Show" OnClick="btnShow_Click" />
                                                                    <asp:ValidationSummary ID="valShowSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                        ShowSummary="false" ValidationGroup="Show" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-12" id="divStudInfo" runat="server" visible="false">
                                                            <div class="row">
                                                                <div class="col-lg-4 col-md-6 col-12">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Enrollment No. :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblRegNo" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Student's Name :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblStudName" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>

                                                                        <li class="list-group-item"><b>Batch :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblBatch" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Semester :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblSemester" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                                <div class="col-lg-3 col-md-6 col-12">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Gender :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblSex" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Date of Admission  :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblDateOfAdm" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Year  :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblYear" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>

                                                                    </ul>
                                                                </div>
                                                                <div class="col-lg-5 col-md-6 col-12">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Payment Type :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblPaymentType" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>

                                                                        <li class="list-group-item"><b>Faculty  :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblCollege" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Program :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblDegree" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="form-group col-lg-5 col-md-6 col-12" id="remark" runat="server">

                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Cancel Remark</label>
                                                                    </div>

                                                                    <asp:TextBox ID="txtRemarkAdm" runat="server" TextMode="MultiLine" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="form-group col-lg-5 col-md-6 col-12" id="ReAdmremark" runat="server">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Re-Admission Remark</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtReAdmission" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvReAdmRemark" runat="server" ControlToValidate="txtReAdmission" ValidationGroup="Can" ErrorMessage="Please Enter Re-Admission Remark."></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <%--<div id="div2" runat="server"></div>--%>
                                                            <div class="col-12 btn-footer">

                                                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click"
                                                                    Text="Save" ToolTip="Click Here To Save" TabIndex="6" ValidationGroup="Can" />
                                                                <asp:Button ID="btnCan" runat="server" Text="Cancel" OnClick="btnCan_Click"
                                                                    TabIndex="12" CssClass="btn btn-outline-danger" />
                                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                    ShowSummary="false" ValidationGroup="Can" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>

                                                <%--<asp:PostBackTrigger ControlID="btnShow" />
                                    <asp:PostBackTrigger ControlID="btnSave" />
                                    <asp:PostBackTrigger ControlID="btnCan" />--%>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="TabName" Value="0" runat="server" />

    <div id="divmsgs" runat="server"></div>
    <script type="text/javascript" language="javascript">
        function ShowClearance() {
            try {
                var recValue = GetSelectedRecord();

                if (recValue != "")
                    __doPostBack("ShowClearance", recValue);
                else
                    alert("Please select a student record.");
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function GetSelectedRecord() {
            debugger;
            var recordValue = "";
            try {
                //  var tbl = document.getElementById('tblSearchResults').rows[1].cells[0].firstChild;
                var tbl = document.getElementById('tblSearchResults');
                //var tbl = document.getElementById('tblSearchResults').Value != "";
                //alert(tbl);
                debugger
                if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                    for (i = 1; i < tbl.rows.length; i++) {
                        var dataRow = tbl.rows[i];
                        //var dataCell = dataRow.firstChild;
                        var dataCell = dataRow.cells[0];
                        //  alert(dataCell);
                        //var rdo = dataCell.firstChild;
                        var rdo = dataCell.children[0];
                        //  alert(rdo);
                        if (rdo.checked) {
                            recordValue = rdo.value;
                            //    alert(recordValue);
                        }
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return recordValue;
        }

        function CancelAdmission() {
            try {

                var recValue = GetSelectedRecord();
                if (recValue != "") {
                    if (document.getElementById('<%= txtRemark.ClientID %>').value.trim() != "") {
                        if (confirm("Are you sure you want to cancel this student's admission."))
                            __doPostBack("CancelAdmission", "" + recValue + "");
                    }
                    else {
                        alert("Please enter reason for canceling the student's admission.");
                        document.getElementById('<%= txtRemark.ClientID %>').focus();
                    }
                }
                else if (recValue == "") {
                    alert("Please select a student");
                }

            }
            catch (e) {
                alert("Error: " + e.description);
            }
            // return false;
        }

        //function btnCancelAdmission_onclick() 
        //{
        //}

        ////function btnShowClearance_onclick() {
        ////}

        //function btnCancelAdmission_onclick() {
        //}

    </script>

</asp:Content>
