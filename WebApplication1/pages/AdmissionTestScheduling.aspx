<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AdmissionTestScheduling.aspx.cs" Inherits="AdmissionTestScheduling" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- jQuery library -->
    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>

    <style>
        .daterangepicker .drp-calendar .table-condensed {
            display: none;
        }
    </style>
    <style>
        /*--======= toggle switch css added by gaurav 29072021 =======--*/
        .switch input[type=checkbox] {
            height: 0;
            width: 0;
            visibility: hidden;
        }

        .switch label {
            cursor: pointer;
            width: 82px;
            height: 34px;
            background: #dc3545;
            display: block;
            border-radius: 4px;
            position: relative;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }

            .switch label:hover {
                background-color: #c82333;
            }

            .switch label:before {
                content: attr(data-off);
                position: absolute;
                right: 0;
                font-size: 16px;
                padding: 4px 8px;
                font-weight: 400;
                color: #fff;
                transition: 0.35s;
                -webkit-transition: 0.35s;
                -moz-user-select: none;
                -webkit-user-select: none;
            }

        .switch input:checked + label:before {
            content: attr(data-on);
            position: absolute;
            left: 0;
            font-size: 16px;
            padding: 4px 15px;
            font-weight: 400;
            color: #fff;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }

        .switch label:after {
            content: '';
            position: absolute;
            top: 1.5px;
            left: 1.7px;
            width: 10.2px;
            height: 31.5px;
            background: #fff;
            border-radius: 2.5px;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }

        .switch input:checked + label {
            background: #28a745;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }

            .switch input:checked + label:hover {
                background: #218838;
            }

            .switch input:checked + label:after {
                transform: translateX(68px);
                transition: 0.35s;
                -webkit-transition: 0.35s;
                -moz-user-select: none;
                -webkit-user-select: none;
            }
    </style>

    <div>

        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updTestSchedule"
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
    <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
    <asp:UpdatePanel ID="updTestSchedule" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%-- <h3 class="box-title"><b>Admission Test Scheduling</b></h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%-- <label>Intake</label>--%>
                                            <asp:Label ID="lblDYSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="1" AppendDataBoundItems="true" ToolTip="Please Select Intake">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlIntake"
                                            Display="None" ErrorMessage="Please Select Intake" ValidationGroup="Show" InitialValue="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYPrgmtype" runat="server" Font-Bold="true"></asp:Label>
                                            <%--   <label>Study Level</label>--%>
                                        </div>
                                     <%--   <asp:DropDownList ID="ddlStudyLevel" runat="server" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="1" AppendDataBoundItems="true" ToolTip="Please Select Study Level">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>--%>
                                          <asp:ListBox ID="ddlStudyLevel" runat="server" AppendDataBoundItems="true"
                                                            CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlStudyLevel"
                                            Display="None" ErrorMessage="Please Select Study Level" ValidationGroup="Show"  />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblRAExamDate" runat="server" Font-Bold="true"></asp:Label>
                                            <%--        <label>Exam Date</label>--%>
                                        </div>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" type="date" TabIndex="1" ToolTip="Please Select Time Slot"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDate"
                                            Display="None" ErrorMessage="Please Enter Exam Date" ValidationGroup="Show" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblTimeSlot" runat="server" Font-Bold="true"></asp:Label>
                                            <%--     <label>Time Slot</label>--%>
                                        </div>
                                        <asp:TextBox ID="txtTimeSlot" onfocus="TimeSlot(this)" runat="server" Text='<%# Eval("TIMETOFROM")%>' CssClass="form-control" TabIndex="1" ToolTip="Please Select Time Slot" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTimeSlot"
                                            Display="None" ErrorMessage="Please Enter Time Slot" ValidationGroup="Show" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblVenue" runat="server" Font-Bold="true"></asp:Label>
                                            <%--  <label>Venue </label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlVenue" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true" ToolTip="Please Select Venue">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlVenue"
                                            Display="None" ErrorMessage="Please Select Venue" ValidationGroup="Show" InitialValue="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDyCapacity" runat="server" Font-Bold="true" ToolTip="Please Enter Capacity"></asp:Label>
                                            <%-- <label>Capacity </label>--%>
                                        </div>
                                        <asp:TextBox ID="txtCapacity" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCapacity"
                                            Display="None" ErrorMessage="Please Enter Capacity" ValidationGroup="Show" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="switch" name="switch" class="switch" checked />
                                            <label data-on="Active" data-off="Inactive" for="switch"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-outline-info" TabIndex="1" OnClick="btnSave_Click" ValidationGroup="Show" ClientIDMode="Static">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" TabIndex="1" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Show"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <%--<div class="col-12 mt-4">
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Intake</th>
                                            <th>Study Level</th>
                                            <th>Program</th>
                                            <th>Exam Date</th>
                                            <th>Time Slot</th>
                                            <th>Venue</th>
                                            <th>Capacity</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Jan-June 2022</td>
                                            <td>Graduation</td>
                                            <td>IT, CS</td>
                                            <td>16-02-2023</td>
                                            <td>01:00pm - 04:00pm</td>
                                            <td>University Of San Agustin</td>
                                            <td>100</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>--%>

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <asp:ListView ID="Lvschedule" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                               
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit </th>
                                                            <th>Intake</th>
                                                            <th>Study Level</th>                                                           
                                                            <th>Exam Date</th>
                                                            <th>Time Slot</th>
                                                            <th>Venue</th>
                                                            <th>Capacity</th>
                                                            <th>Status</th>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("SCHEDULING_NO") %>' CommandName='<%# Eval("EXAMDATE_NO") %>' ImageUrl="~/images/edit.gif"  OnClick="btnEdit_Click" ToolTip='<%# Eval("SCHEDULING_NO") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("BATCHNAME")%>
                                                </td>

                                                <td>
                                                    <%# Eval("UA_SECTIONNAME")%>
                                                </td>

                                                <td>
                                                    <%# Eval("EXAMDATE1")%>
                                                </td>

                                                <td>
                                                    <%# Eval("TIMESLOT")%>
                                                </td>

                                                <td>
                                                    <%# Eval("VENUE_NAME")%>
                                                </td>
                                                  <td>
                                                    <%# Eval("CAPACITY")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ACTIVESTATUS")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>

    <script>
        function TimeSlot(id) {
            $(id).daterangepicker({
                DatePicker: false,
                timePicker: true,
                locale: {
                    format: 'hh:mm A'
                },
            },
            function (start, end, label) {
                $(id).val(start.format('hh:mm A') + ' - ' + end.format('hh:mm A'));

            });
            $(id).on('apply.daterangepicker', function (ev, Picker) {
                $(id).val(Picker.startDate.format('hh:mm A') + ' - ' + Picker.endDate.format('hh:mm A'));
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(document).ready(function () {
                    $(id).daterangepicker({
                        DatePicker: false,
                        timePicker: true,
                        locale: {
                            format: 'hh:mm A'
                        },
                    },
                    function (start, end, label) {
                        $('#ctl00_ContentPlaceHolder1_txtTimeSlot').val(start.format('hh:mm A') + ' - ' + end.format('hh:mm A'));

                    });
                    $(id).on('apply.daterangepicker', function (ev, Picker) {
                        $(id).val(Picker.startDate.format('hh:mm A') + ' - ' + Picker.endDate.format('hh:mm A'));
                    });
                });
            });
        }
    </script>
    <script>
        function SetStat(val) {
            $('#switch').prop('checked', val);
        }

        var summary = "";
        $(function () {

            $('#btnSave').click(function () {
                localStorage.setItem("currentId", "#btnSave,Submit");
                debugger;
               // ShowLoader('#btnSave');


                if (summary != "") {
                    customAlert(summary);
                    summary = "";
                    return false
                }
                $('#hfdStat').val($('#switch').prop('checked'));
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSave').click(function () {
                    localStorage.setItem("currentId", "#btnSave,Submit");
                    //ShowLoader('#btnSave');

                    if (summary != "") {
                        customAlert(summary);
                        summary = "";
                        return false
                    }
                    $('#hfdStat').val($('#switch').prop('checked'));
                });
            });
        });
    </script>
</asp:Content>

