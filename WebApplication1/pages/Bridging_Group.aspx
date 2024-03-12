<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Bridging_Group.aspx.cs" Inherits="Projects_Bridging_Group" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:HiddenField ID="hfbgroup" runat="server" ClientIDMode="Static" />
    <style>
        .badge {
            display: inline-block;
            padding: 5px 10px 7px;
            border-radius: 15px;
            font-size: 100%;
            width: 90px;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updGroup"
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
    <asp:UpdatePanel ID="updGroup" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
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
                                            <label>Criteria Name</label>
                                            <%--<b> <asp:Label ID="Label1" runat="server"></asp:Label></b>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlcriteria" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1" OnSelectedIndexChanged="ddlIntake_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlcriteria"
                                            Display="None" ErrorMessage="Please Select Criteria Name." ValidationGroup="BridgGroup"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Intake</label>--%>
                                            <b>
                                                <asp:Label ID="lblDYAdmbatch" runat="server"></asp:Label></b>
                                        </div>
                                        <asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1" OnSelectedIndexChanged="ddlIntake_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlIntake"
                                            Display="None" ErrorMessage="Please Select Intake." ValidationGroup="BridgGroup"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%-- <label>Faculty</label>--%>
                                            <b>
                                                <asp:Label ID="lblFaculty" runat="server"></asp:Label></b>
                                        </div>
                                        <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlFaculty"
                                            Display="None" ErrorMessage="Please Select Faculty /School Name." ValidationGroup="BridgGroup"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Group Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlGroupType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <%--<asp:ListItem Value="1">Oriantation</asp:ListItem>--%>
                                          <%--  <asp:ListItem Value="2">Bridging</asp:ListItem>
                                            <asp:ListItem Value="3">Non-Bridging</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlGroupType"
                                            Display="None" ErrorMessage="Please Select Group Type." ValidationGroup="BridgGroup"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divGroupName" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Group Name</label>
                                        </div>
                                        <asp:TextBox ID="txtGroupName" runat="server" CssClass="form-control" MaxLength="80" TabIndex="3"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                            TargetControlID="txtGroupName" FilterType="Custom" FilterMode="InvalidChars"
                                            InvalidChars="~`!@#$%^*()_+=,/:;<>?'{}[]\|&&quot;'" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtGroupName"
                                            Display="None" ErrorMessage="Please Enter Group Name." ValidationGroup="BridgGroup"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Group PreFix</label>
                                        </div>
                                        <asp:TextBox ID="txtPrefix" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPrefix"
                                            Display="None" ErrorMessage="Please Enter Group PreFix" ValidationGroup="BridgGroup"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>No. of Groups</label>
                                        </div>
                                        <asp:TextBox ID="txtcapacity" runat="server" CssClass="form-control" MaxLength="4" AutoPostBack="true" onkeyup="validateNumeric(this);" TabIndex="4" OnTextChanged="txtcapacity_TextChanged"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftrPermPIN" runat="server" FilterType="Numbers"
                                            TargetControlID="txtcapacity" ValidChars="0123456789">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtcapacity"
                                            Display="None" ErrorMessage="Please Enter No. of Groups" ValidationGroup="BridgGroup"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Status</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="chkstatusbgroup" name="switch" class="switch" checked tabindex="5" />
                                            <label data-on="Active" data-off="Inactive" for="chkstatusbgroup"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSubmit_Click" OnClientClick="return validate();" ValidationGroup="BridgGroup" TabIndex="6">Submit</asp:LinkButton>
                                <asp:Button ID="btnLock" runat="server" CssClass="btn btn-outline-primary" Text="Lock" OnClick="btnLock_Click" Visible="false" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="BridgGroup" />
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" TabIndex="7">Cancel</asp:LinkButton>
                            </div>

                            <div class="col-md-12">
                                <asp:Panel ID="Panel2" runat="server">
                                    <asp:ListView ID="lvAddNewGroups" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Group List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap" id="table1" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center">SrNo
                                                        </th>
                                                        <th style="text-align: center;display:none">Group Code
                                                        </th>
                                                        <th style="text-align: center">Group Name
                                                        </th>
                                                        <th style="text-align: center">Capacity
                                                        </th>
                                                        <th style="text-align: center">Group Type
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblSrno" runat="server" Text='<%# Eval("SRNO") %>'></asp:Label>
                                                    <asp:Label ID="lblId" runat="server"></asp:Label>
                                                    <asp:Label ID="lblLock" runat="server" Visible="false" Text='<%# Eval("LOCK") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center;display:none">
                                                    <asp:Label ID="lblgroupCode" runat="server" Text='<%# Eval("GROUPCODE") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGroupName" runat="server" CssClass="form-control" Text='<%# Eval("GROUPNAME") %>' MaxLength="50" Enabled="false"></asp:TextBox>
                                                    <%--Enabled='<%# Eval("LOCK").ToString() == "1" ? false : true %>'--%>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:TextBox ID="txtCapacity" runat="server" CssClass="form-control" Text='<%# Eval("CAPACITY") %>' Enabled='<%# Eval("LOCK").ToString() == "1" ? false : true %>'></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftrPermPIN" runat="server"
                                                        TargetControlID="txtCapacity" ValidChars="0123456789">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblGroupType" runat="server" Text='<%# Eval("GROUPTYPE")%>'></asp:Label>
                                                    <asp:Label ID="lblUniqueid" runat="server" Text='<%# Eval("UNIQUE_ID")%>' Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div class="col-12 mt-3">
                                <%-- <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th>Faculty</th>
                                    <th>Group Type</th>
                                    <th>Group Name</th>
                                    <th>Max Size</th>
                                    <th>Status</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Faculty Name</td>
                                    <td>Bridging</td>
                                    <td>Group Name A</td>
                                    <td>200</td>
                                    <td><span class="badge badge-success">Active</span></td>
                                </tr>
                                <tr>
                                    <td>Faculty Name</td>
                                    <td>Non-Bridging</td>
                                    <td>Group Name B</td>
                                    <td>100</td>
                                    <td><span class="badge badge-danger">In Active</span></td>
                                </tr>
                            </tbody>
                        </table>--%>
                                <asp:ListView ID="lvBridgingGroup" runat="server">
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Edit</th>
                                                    <th>Criteria Name</th>
                                                    <th>Intake</th>
                                                    <%--<th>Faculty</th>--%>
                                                    <th>Group Type</th>
                                                    <th>No.of Group</th>
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
                                                <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CssClass="fa fa-pencil-square-o"
                                                    CommandArgument='<%# Eval("ADMBATCH") + "," + Eval("COLLEGE_ID") + "," + Eval("GROUPTYPE") + "," + Eval("CRITERIA_NO") + "," + Eval("NUMBER_OF_GROUPS") + "," + Eval("ACTIVE_STATUS") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" /></td>
                                             <td><%# Eval("CRITERIA_NAME")%></td>
                                             <td><%# Eval("BATCHNAME")%></td>
                                         <%--   <td><%# Eval("COLLEGE_NAME")%></td>--%>
                                            <td><%# Eval("GROUP_TYPE")%></td>
                                            <td><%# Eval("NUMBER_OF_GROUPS")%></td>
                                            <td>
                                                <asp:Label ID="lblstatus" CssClass="badge" runat="server" Text='<%# Eval("STATUS")%>'></asp:Label></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function SetStat(val) {
            $('#chkstatusbgroup').prop('checked', val);
        }

        function validate() {

            $('#hfbgroup').val($('#chkstatusbgroup').prop('checked'));

            //var txtCurrency = $("[id$=txtCurrency]").attr("id");
            //var txtCurrency = document.getElementById(txtCurrency);

            //if (txtCurrency.value.length == 0) {
            //    alert('Please Enter Currency ', 'Warning!');
            //    //   $(txtCurrency).css('border-color', 'red');
            //    $(txtCurrency).focus();
            //    return false;
            //}
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });
    </script>
    <script>
        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
    </script>
    <script>
        $(document).ready(function () {
            var table = $('#table1').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 450,
                scrollX: true,
                scrollCollapse: true,
                paging: false,
                sorting: false,

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0, 9];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#table1').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                                {
                                    extend: 'copyHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0, 9];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#table1').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("input:checkbox").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("input:checkbox").each(function () {
                                                        if ($(this).is(':checked')) {
                                                            nodereturn += "On";
                                                        } else {
                                                            nodereturn += "Off";
                                                        }
                                                    });
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
                                                    });
                                                }
                                                else if ($(node).find("select").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("select").each(function () {
                                                        var thisOption = $(this).find("option:selected").text();
                                                        if (thisOption !== "Please Select") {
                                                            nodereturn += thisOption;
                                                        }
                                                    });
                                                }
                                                else {
                                                    nodereturn = data;
                                                }
                                                return nodereturn;
                                            },
                                        },
                                    }
                                },
                                {
                                    extend: 'excelHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0, 9];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#table1').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("input:checkbox").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("input:checkbox").each(function () {
                                                        if ($(this).is(':checked')) {
                                                            nodereturn += "On";
                                                        } else {
                                                            nodereturn += "Off";
                                                        }
                                                    });
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
                                                    });
                                                }
                                                else if ($(node).find("select").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("select").each(function () {
                                                        var thisOption = $(this).find("option:selected").text();
                                                        if (thisOption !== "Please Select") {
                                                            nodereturn += thisOption;
                                                        }
                                                    });
                                                }
                                                else {
                                                    nodereturn = data;
                                                }
                                                return nodereturn;
                                            },
                                        },
                                    }
                                },
                                {
                                    extend: 'pdfHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0, 9];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#table1').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("input:checkbox").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("input:checkbox").each(function () {
                                                        if ($(this).is(':checked')) {
                                                            nodereturn += "On";
                                                        } else {
                                                            nodereturn += "Off";
                                                        }
                                                    });
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
                                                    });
                                                }
                                                else if ($(node).find("select").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("select").each(function () {
                                                        var thisOption = $(this).find("option:selected").text();
                                                        if (thisOption !== "Please Select") {
                                                            nodereturn += thisOption;
                                                        }
                                                    });
                                                }
                                                else {
                                                    nodereturn = data;
                                                }
                                                return nodereturn;
                                            },
                                        },
                                    }
                                },
                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#table1').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 450,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false,
                    sorting: false,

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0, 9];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#table1').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                                    {
                                        extend: 'copyHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0, 9];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#table1').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        nodereturn = "";
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                    }
                                                    else if ($(node).find("input:checkbox").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("input:checkbox").each(function () {
                                                            if ($(this).is(':checked')) {
                                                                nodereturn += "On";
                                                            } else {
                                                                nodereturn += "Off";
                                                            }
                                                        });
                                                    }
                                                    else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
                                                        });
                                                    }
                                                    else if ($(node).find("select").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("select").each(function () {
                                                            var thisOption = $(this).find("option:selected").text();
                                                            if (thisOption !== "Please Select") {
                                                                nodereturn += thisOption;
                                                            }
                                                        });
                                                    }
                                                    else {
                                                        nodereturn = data;
                                                    }
                                                    return nodereturn;
                                                },
                                            },
                                        }
                                    },
                                    {
                                        extend: 'excelHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0, 9];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#table1').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        nodereturn = "";
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                    }
                                                    else if ($(node).find("input:checkbox").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("input:checkbox").each(function () {
                                                            if ($(this).is(':checked')) {
                                                                nodereturn += "On";
                                                            } else {
                                                                nodereturn += "Off";
                                                            }
                                                        });
                                                    }
                                                    else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
                                                        });
                                                    }
                                                    else if ($(node).find("select").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("select").each(function () {
                                                            var thisOption = $(this).find("option:selected").text();
                                                            if (thisOption !== "Please Select") {
                                                                nodereturn += thisOption;
                                                            }
                                                        });
                                                    }
                                                    else {
                                                        nodereturn = data;
                                                    }
                                                    return nodereturn;
                                                },
                                            },
                                        }
                                    },
                                    {
                                        extend: 'pdfHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0, 9];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#table1').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        nodereturn = "";
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                    }
                                                    else if ($(node).find("input:checkbox").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("input:checkbox").each(function () {
                                                            if ($(this).is(':checked')) {
                                                                nodereturn += "On";
                                                            } else {
                                                                nodereturn += "Off";
                                                            }
                                                        });
                                                    }
                                                    else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
                                                        });
                                                    }
                                                    else if ($(node).find("select").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("select").each(function () {
                                                            var thisOption = $(this).find("option:selected").text();
                                                            if (thisOption !== "Please Select") {
                                                                nodereturn += thisOption;
                                                            }
                                                        });
                                                    }
                                                    else {
                                                        nodereturn = data;
                                                    }
                                                    return nodereturn;
                                                },
                                            },
                                        }
                                    },
                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });
    </script>
</asp:Content>

