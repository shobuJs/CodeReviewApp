<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LeadGroupCreation.aspx.cs" Inherits="ACADEMIC_LeadGroupCreation" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- jQuery library -->
    <%-- <link href="../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet"/>--%>
    <%--<link href="<%#Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css") %>" rel="stylesheet" />--%>
    <%--<link href="../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet"/>--%>
    <link href="../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>


    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
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

    <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />


    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGradeEntry"
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

    <%--<script type="text/javascript">
        document.onreadystatechange = function () {
            var state = document.readyState
            if (state == 'interactive') {
                document.getElementById('contents').style.visibility = "hidden";
            } else if (state == 'complete') {
                setTimeout(function () {
                    document.getElementById('interactive');
                    document.getElementById('load').style.visibility = "hidden";
                    document.getElementById('contents').style.visibility = "visible";
                }, 1000);
            }
        }
    </script>--%>

    <div id="contents">
        <%--This is testing--%>
    </div>
    <asp:UpdatePanel ID="updGradeEntry" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblIntake" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmbatch" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True"
                                            ToolTip="Please Select Batch" TabIndex="2" ClientIDMode="Static">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYstudylevel" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlstudylevel" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True"
                                            ToolTip="Please Select Study Level" TabIndex="2" ClientIDMode="Static">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYmainuser" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlmainuser" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True"
                                            ToolTip="Please Select Main User" TabIndex="3" ClientIDMode="Static" OnSelectedIndexChanged="ddlEndMonth_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYsubuser" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:ListBox ID="ddlsubuser" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"
                                            SelectionMode="multiple" TabIndex="8"></asp:ListBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYStatus" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="switch" name="switch" class="switch" checked />
                                            <label data-on="Active" data-off="Inactive" for="switch"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnSave" runat="server" ToolTip="Submit" OnClientClick="return validate()"
                                    CssClass="btn btn-outline-info" OnClick="btnSave_Click" TabIndex="8" ClientIDMode="Static">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" TabIndex="3" OnClick="btnCancel_Click">Clear</asp:LinkButton>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlSession" runat="server">
                                    <asp:ListView ID="lvlist" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Lead Group List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divdepartmentlist">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Action </th>
                                                            <th>INTAKE</th>
                                                            <th>STUDY LEVEL</th>
                                                            <th>MAIN COUNSELOR L1</th>
                                                            <th>COUNSELORS</th>
                                                            <th>STATUS </th>
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
                                                <td style="text-align: center;">
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("SR_NO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" OnClientClick="return InitAutoCompl();" />
                                                </td>
                                                <td><%# Eval("BATCHNAME") %></td>
                                                <td><%# Eval("ua_sectionname") %></td>
                                                <td><%# Eval("UA_FULLNAME") %></td>
                                                <td><%# Eval("SUBUSER_UA_NO") %> </td>
                                                <td><%# Eval("Status") %></td>
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
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="lvlist" />--%>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="ddlsubuser" />

        </Triggers>
    </asp:UpdatePanel>

    <script>
        function SetStat(val) {
            $('#switch').prop('checked', val);
        }

        var summary = "";
        $(function () {
            //debugger;
            $('#btnSave').click(function () {
                localStorage.setItem("currentId", "#btnSave,Submit");
                debugger;
                ShowLoader('#btnSave');

                if ($('#txtAdmbatch').val() == "")
                    summary += '<br>Please Enter Intake.';
                if ($('#ddlMonth').val() == "0")
                    summary += '<br>Please Select Start Month.';
                if ($('#ddlEndMonth').val() == "0")
                    summary += '<br>Please Select End Month.';
                if ($('#ddlYear').val() == "0")
                    summary += '<br>Please Select Year.';


                if (summary != "") {
                    customAlert(summary);
                    summary = "";
                    return false
                }
                $('#hfdStat').val($('#switch').prop('checked'));
                return GetSelectedTextValue();

            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSave').click(function () {
                    localStorage.setItem("currentId", "#btnSave,Submit");
                    ShowLoader('#btnSave');

                    if ($('#txtAdmbatch').val() == "")
                        summary += '<br>Please Enter Intake.';
                    if ($('#ddlMonth').val() == "0")
                        summary += '<br>Please Select Start Month.';
                    if ($('#ddlEndMonth').val() == "0")
                        summary += '<br>Please Select End Month.';
                    if ($('#ddlYear').val() == "0")
                        summary += '<br>Please Select Year.';

                    if (summary != "") {
                        customAlert(summary);
                        summary = "";
                        return false
                    }
                    $('#hfdStat').val($('#switch').prop('checked'));
                    return GetSelectedTextValue();
                });
            });
        });
    </script>

    <script type="text/javascript">

        function validate() {

        }
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

</asp:Content>

