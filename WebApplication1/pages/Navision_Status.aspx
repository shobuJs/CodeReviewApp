<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Navision_Status.aspx.cs" Inherits="ACADEMIC_Navision_Status" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner, #ctl00_ContentPlaceHolder1_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
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
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updCourseCreation"
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
    <asp:UpdatePanel ID="updCourseCreation" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="nav-tabs-custom mt-2 col-12" id="myTabContent">
                            <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                                <li class="nav-item active" id="divlknavision" runat="server">
                                    <asp:LinkButton ID="lknavision" runat="server" OnClick="lknavision_Click" CssClass="nav-link" TabIndex="1">Navision Status</asp:LinkButton></li>
                                <li class="nav-item" id="divlkchange" runat="server">
                                    <asp:LinkButton ID="lkchange" runat="server" OnClick="lkchange_Click" CssClass="nav-link" TabIndex="2">Resend Status</asp:LinkButton></li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane fade show active" id="divnavision" role="tabpanel" runat="server" aria-labelledby="Navision-tab">
                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updNavision"
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
                                    <asp:UpdatePanel ID="updNavision" runat="server">
                                        <ContentTemplate>
                                            <div class="box-body">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="Label23" runat="server" Font-Bold="true" Text="Date (From-To)"></asp:Label>
                                                            </div>
                                                            <div id="picker" class="form-control">
                                                                <i class="fa fa-calendar"></i>&nbsp;
                                                                <span id="date"></span>
                                                                <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                                <asp:HiddenField ID="hdnDate" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:LinkButton ID="btnShow" runat="server" ToolTip="Show" ValidationGroup="Show"
                                                        CssClass="btn btn-outline-info" OnClick="btnShow_Click" ClientIDMode="Static">Show</asp:LinkButton>
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                        OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Show"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:Panel ID="Panel1" runat="server" Visible="false">
                                                        <asp:ListView ID="LvResponceData" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Module List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" id="mytable" style="width: 100%">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th style="text-align: center">SrNo </th>
                                                                            <th style="text-align: center">Student Id </th>
                                                                            <th style="text-align: center">Name With Initial</th>
                                                                            <th style="text-align: center">Result</th>
                                                                            <th style="text-align: center">Status</th>
                                                                            <th style="text-align: center">Date</th>
                                                                            <th style="text-align: center">Responce</th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <%# Container.DataItemIndex + 1 %>                                                                             
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ENROLLNO")%> 
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("NAME_WITH_INITIAL")%> 
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("RESULT")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("STATUS")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ADDED_DATE")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("RESPONCE_VALUE")%>
                                                                    </td>

                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div id="divresend" runat="server" visible="false" role="tabpanel" aria-labelledby="Resend-tab">
                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updNavisionChange"
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
                                    <asp:UpdatePanel ID="updNavisionChange" runat="server">
                                        <ContentTemplate>
                                            <div class="box-body">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Date (From-To)"></asp:Label>
                                                            </div>
                                                            <div id="picker2" class="form-control">
                                                                <i class="fa fa-calendar"></i>&nbsp;
                                                                <span id="date2"></span>
                                                                <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                                <asp:HiddenField ID="hdnDate2" runat="server" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblSessionNameOld" runat="server" Font-Bold="true">Status</asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="8" CssClass="form-control select2 select-click"
                                                                AppendDataBoundItems="True" ToolTip="Please Select Status">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Withdrawal of Registration</asp:ListItem>
                                                                <asp:ListItem Value="2">Cancel Semester Registration</asp:ListItem>
                                                                <asp:ListItem Value="3">Postponement</asp:ListItem>
                                                                <asp:ListItem Value="4">Cancel Module Registration</asp:ListItem>
                                                                <asp:ListItem Value="5">Excess Withdrawal</asp:ListItem>
                                                                <asp:ListItem Value="6">Cancel Exam Registration</asp:ListItem>
                                                                <asp:ListItem Value="7">Applicant_Preview</asp:ListItem>
                                                                <asp:ListItem Value="8">Application</asp:ListItem>
                                                                <asp:ListItem Value="9">Enrollment</asp:ListItem>
                                                                <asp:ListItem Value="10">Higher Semester</asp:ListItem>
                                                                <asp:ListItem Value="11">PHD</asp:ListItem>
                                                                <asp:ListItem Value="12">Loan Scheme</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlStatus"
                                                                Display="None" ErrorMessage="Please Select Status" InitialValue="0"
                                                                ValidationGroup="Show" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:LinkButton ID="btnShowChange" runat="server" ToolTip="Show" ValidationGroup="Show"
                                                        CssClass="btn btn-outline-info" OnClick="btnShowChange_Click" ClientIDMode="Static">Show</asp:LinkButton>
                                                      <asp:LinkButton ID="btnSubmit" runat="server" ToolTip="Submit" Visible="false"
                                                        CssClass="btn btn-outline-info"  OnClick="btnSubmit_Click" ClientIDMode="Static">Submit</asp:LinkButton>
                                                    <asp:Button ID="btnCancelChange" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                        OnClick="btnCancelChange_Click" CssClass="btn btn-outline-danger" />
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Show"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:Panel ID="Panel2" runat="server" Visible="false">
                                                        <asp:ListView ID="ListviewChanges" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Module List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" id="mytable" style="width: 100%">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>
                                                                                <asp:CheckBox ID="cbHeadReg" runat="server" OnClick="checkAllCheckbox(this);" Text="Select" />
                                                                            </th>
                                                                            <th style="text-align: center">Student Id </th>
                                                                            <th style="text-align: center">Name With Initial</th>
                                                                            <th style="text-align: center">Result</th>
                                                                            <th style="text-align: center">Status</th>
                                                                            <th style="text-align: center">Date</th>
                                                                            <th style="text-align: center">Responce</th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkRegister" runat="server" Font-Bold="true" />
                                                                        <asp:HiddenField ID="hdfidno" runat="server" Value=' <%# Eval("IDNO")%> ' />
                                                                         <asp:HiddenField ID="hdftempdcr_no" runat="server" Value=' <%# Eval("TEMP_DCR_NO")%> ' />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ENROLLNO")%> 
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("NAME_WITH_INITIAL")%> 
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("RESULT")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("STATUS")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ADDED_DATE")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("RESPONCE_VALUE")%>
                                                                    </td>

                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                ranges: {
                },
            },
        function (start, end) {
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
            document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            //$('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
        });
    </script>
    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#picker').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    ranges: {
                    },
                },
            function (start, end) {
                $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
                document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                //$('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
            });
        });
    </script>
    <script>
        function Setdate(date) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(document).ready(function () {
                    debugger;
                    var startDate = moment(date.split('-')[0], "DD MMM, YYYY");
                    var endtDate = moment(date.split('-')[1], "DD MMM, YYYY");
                    //$('#date').html(date);
                    $('#date').html(startDate.format("DD MMM, YYYY") + ' - ' + endtDate.format("DD MMM, YYYY"));
                    document.getElementById('<%=hdnDate.ClientID%>').value = date;
                    //$('#picker').daterangepicker({ startDate: startDate, endDate: endtDate});
                    $('#picker').daterangepicker({
                        startDate: startDate.format("MM/DD/YYYY"),
                        endDate: endtDate.format("MM/DD/YYYY"),
                        ranges: {
                        },
                    },
            function (start, end) {
                debugger
                $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                });
            });
};
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#picker2').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                ranges: {
                },
            },
        function (start, end) {
            $('#date2').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
            document.getElementById('<%=hdnDate2.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            //$('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
        });
    </script>
    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#picker2').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    ranges: {
                    },
                },
            function (start, end) {
                $('#date2').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
                document.getElementById('<%=hdnDate2.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                //$('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
            });
        });
    </script>
    <script>
        function Setdate2(date) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(document).ready(function () {
                    debugger;
                    var startDate = moment(date.split('-')[0], "DD MMM, YYYY");
                    var endtDate = moment(date.split('-')[1], "DD MMM, YYYY");
                    //$('#date').html(date);
                    $('#date2').html(startDate.format("DD MMM, YYYY") + ' - ' + endtDate.format("DD MMM, YYYY"));
                    document.getElementById('<%=hdnDate2.ClientID%>').value = date;
                    //$('#picker').daterangepicker({ startDate: startDate, endDate: endtDate});
                    $('#picker2').daterangepicker({
                        startDate: startDate.format("MM/DD/YYYY"),
                        endDate: endtDate.format("MM/DD/YYYY"),
                        ranges: {
                        },
                    },
            function (start, end) {
                debugger
                $('#date2').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                document.getElementById('<%=hdnDate2.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                });
            });
};
    </script>
    <script type="text/javascript" language="javascript">

        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$ListviewChanges$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$ListviewChanges$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>
</asp:Content>

