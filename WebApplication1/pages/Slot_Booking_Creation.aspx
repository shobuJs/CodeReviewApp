<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Slot_Booking_Creation.aspx.cs" Inherits="Slot_Booking_Creation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
        .daterangepicker .drp-calendar .table-condensed {
            display: none;
        }

        .daterangepicker.show-ranges .drp-calendar .table-condensed,
        .daterangepicker.single .drp-calendar .table-condensed {
            display: block;
        }
    </style>
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
    <div>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updPresent"
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

    <asp:UpdatePanel ID="updPresent" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%-- <h3 class="box-title">--%>
                               <%-- <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>--%>
                            <h3 class="box-title"><span>Slot Booking Creation</span></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Intake</label>
                                        </div>
                                        <asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlIntake"
                                            Display="None" ErrorMessage="Please Select Intake" InitialValue="0" ValidationGroup="Submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Date</label>
                                        </div>
                                        <asp:TextBox ID="txtDate" runat="server" type="date" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                            Display="None" ErrorMessage="Please Enter Date" InitialValue="" ValidationGroup="Submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                         <label>Start & End Time </label>   
                                        </div>
                                        <asp:TextBox ID="txttime" runat="server" TabIndex="6" CssClass="form-control" Width="100%"
                                            ToolTip="Please Enter Exam Time" />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txttime"
                                            Display="None" ErrorMessage="Please Enter Start & End Time" InitialValue="" ValidationGroup="Submit" />
                                    </div>
                                    
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Activity</label>
                                        </div>
                                        <asp:ListBox ID="ddlActivity" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" SelectionMode="multiple">
                                           
                                        </asp:ListBox>
                                       
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlActivity"
                                            Display="None" ErrorMessage="Please Select Activity" InitialValue="" ValidationGroup="Submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Capacity</label>
                                        </div>
                                        <asp:TextBox ID="txtCapacity" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCapacity"
                                            Display="None" ErrorMessage="Please Enter Capacity" InitialValue="" ValidationGroup="Submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Status</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="switch" name="switch" class="switch" checked tabindex="8" />
                                            <label data-on="Active" data-off="Inactive" for="switch"></label>
                                            <asp:HiddenField ID="hfdStatActi" runat="server" ClientIDMode="Static"/>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click" ValidationGroup="Submit">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                  <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div class="box-body">
                                <div class="col-12">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <asp:ListView ID="lvSlotCreate" runat="server" Visible="true">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit</th>
                                                            <th>Intake</th>
                                                            <th>Date</th>
                                                            <th>Start Time</th>
                                                            <th>End Time</th>
                                                            <th>Activity</th>
                                                            <th>Capacity </th>
                                                            <th>Status</th>
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
                                                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="fa fa-pencil-square-o" OnClick="btnEdit_Click" CommandArgument='<%#Eval("SLOT_NO") %>'></asp:LinkButton></td>

                                                    <td><%#Eval("BATCHNAME") %></td>
                                                    <td><%#Eval("SLOTS_DATE") %></td>
                                                    <td><%#Eval("START_SLOT") %></td>
                                                    <td><%#Eval("END_SLOT") %></td>
                                                    <td><%#Eval("ACTIVITY_NAME") %></td>
                                                    <td><%#Eval("CAPACITY") %></td>
                                                    <td><%#Eval("STATUS") %></td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>

                                    </asp:Panel>

                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
   <script type="text/javascript">
        function SetStat(val) {
            $('#switch').prop('checked', val);
        }
        var summary = "";
        $(document).ready(function () {
            $('#ctl00_ContentPlaceHolder1_btnSubmit').click(function () {
                //localStorage.setItem("currentId", "#btnSubmit,Submit");
                //debugger;
                //ShowLoader('#btnSubmit');
                //if (summary != "") {
                //    customAlert(summary);
                //    summary = "";
                //    //return false
                //}
                $('#hfdStatActi').val($('#switch').prop('checked'));
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#ctl00_ContentPlaceHolder1_btnSubmit').click(function () {
                    //localStorage.setItem("currentId", "#btnSubmit,Submit");
                    //ShowLoader('#btnSubmit');
                    //if (summary != "") {
                    //    customAlert(summary);
                    //    summary = "";
                    //    //return false
                    //}
                    $('#hfdStatActi').val($('#switch').prop('checked'));
                });
            });
        });
    </script>


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
        <script type="text/javascript">
            $(document).ready(function () {
                $('#ctl00_ContentPlaceHolder1_txttime').daterangepicker({
                    DatePicker: false,
                    timePicker: true,
                    locale: {
                        format: 'hh:mm A'
                    },
                },
                function (start, end, label) {
                    $('#ctl00_ContentPlaceHolder1_txttime').val(start.format('hh:mm A') + ' - ' + end.format('hh:mm A'));

                });
                $("#ctl00_ContentPlaceHolder1_txttime").on('apply.daterangepicker', function (ev, Picker) {
                    $('#ctl00_ContentPlaceHolder1_txttime').val(Picker.startDate.format('hh:mm A') + ' - ' + Picker.endDate.format('hh:mm A'));
                });
            });
            $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                    $('#ctl00_ContentPlaceHolder1_txttime').daterangepicker({
                        DatePicker: false,
                        timePicker: true,
                        locale: {
                            format: 'hh:mm A'
                        },
                    },
                    function (start, end, label) {
                        $('#ctl00_ContentPlaceHolder1_txttime').val(start.format('hh:mm A') + ' - ' + end.format('hh:mm A'));

                    });
                    $("#ctl00_ContentPlaceHolder1_txttime").on('apply.daterangepicker', function (ev, Picker) {
                        $('#ctl00_ContentPlaceHolder1_txttime').val(Picker.startDate.format('hh:mm A') + ' - ' + Picker.endDate.format('hh:mm A'));
                    });
                });
            });
    </script>

        <script>


            function Setdate(date) {

                var t = date.split('-')[0];
                var t2 = date.split('-')[1];
                t = moment(date.split('-')[0], 'hh:mm A');
                t2 = moment(date.split('-')[1], 'hh:mm A');
                $('#ctl00_ContentPlaceHolder1_txttime').html(t.format('hh:mm A') + ' - ' + t2.format('hh:mm A'));
               
                $('#ctl00_ContentPlaceHolder1_txttime').daterangepicker({
                    DatePicker: false,
                    timePicker: true,
                    locale: {
                        format: 'hh:mm A'                        
                    },
                },
                function (start, end, label) {
                    $('#ctl00_ContentPlaceHolder1_txttime').val(start.format('hh:mm A') + ' - ' + end.format('hh:mm A'));

                });
                $('#ctl00_ContentPlaceHolder1_txttime').val(t.format('hh:mm A') + ' - ' + t2.format('hh:mm A'));
                $("#ctl00_ContentPlaceHolder1_txttime").on('apply.daterangepicker', function (ev, Picker) {
                    alert('hiii')
                $('#ctl00_ContentPlaceHolder1_txttime').val(Picker.t.format('hh:mm A') + ' - ' + Picker.t.format('hh:mm A'));
            });
};

    </script>
</asp:Content>

