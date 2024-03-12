<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Enlistment_Approval.aspx.cs" Inherits="MockUps_Enlistment_Approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updpro" runat="server" AssociatedUpdatePanelID="updEnlistment"
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>
                <asp:UpdatePanel ID="updEnlistment" runat="server">
                    <ContentTemplate>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Academic Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAcademicSession" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademicSession_SelectedIndexChanged" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                                            ControlToValidate="ddlAcademicSession" Display="None"
                                                                            ErrorMessage="Please Select Academic Session" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                            ControlToValidate="ddlAcademicSession" Display="None"
                                                                            ErrorMessage="Please Select Academic Session" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-5 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                            ControlToValidate="ddlCollege" Display="None"
                                                                            ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="Approve"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                            ControlToValidate="ddlCollege" Display="None"
                                                                            ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="Show"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemster" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSemster_SelectedIndexChanged" TabIndex="1" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                                            ControlToValidate="ddlSemster" Display="None"
                                                                            ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="Approve"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                                            ControlToValidate="ddlSemster" Display="None"
                                                                            ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Program</label>
                                        </div>
                                        <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-outline-info" TabIndex="1" OnClick="btnShow_Click" ValidationGroup="Show">Show</asp:LinkButton>
                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" TabIndex="1" OnClick="btnSubmit_Click" ValidationGroup="Approve">Bulk Approval</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" TabIndex="1" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="valpaymentsummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                    ShowSummary="false" ValidationGroup="Show" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                    ShowSummary="false" ValidationGroup="Approve" />
                            </div>

                            <div class="col-12 mt-3">
                                <asp:ListView ID="lvEnlistmentCount" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                        </div>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>SrNo</th>
                                                    <th>College</th>
                                                    <th>Enlistment Pending Count</th>
                                                    <th>Enlistment Approved Count</th>
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
                                                <%# Container.DisplayIndex + 1 %>
                                            </td>
                                            <td><%# Eval("COLLEGE_NAME") %></td>
                                            <td>
                                                <%# Eval("PENDING_COUNT") %>
                                            </td>
                                            <td>
                                                <%# Eval("APPROVED_COUNT") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>

                                </asp:ListView>
                            </div>

                            <div class="col-12 mt-5">
                                <asp:ListView ID="lvBindEnlismentList" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                        </div>

                                        <table class="table table-striped table-bordered nowrap" id="mytable" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="chkAll" runat="server" onclick="return SelectAll(this)"/></th>
                                                    <th>Student ID</th>
                                                    <th>Name </th>
                                                    <th>Program</th>
                                                    <th>Enlistment Status</th>
                                                    <th>Enlistment Date</th>
                                                    <th>Approval Date</th>
                                                    <th>Approved By</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server"/>
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>

                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:HiddenField ID="hdfEmailid" runat="server" Value='<%# Eval("EMAILID") %>' />
                                                <asp:HiddenField ID="hdfCourseName" runat="server" Value='<%# Eval("COURSENAME") %>' />
                                                <asp:CheckBox ID="chkBox" runat="server" ToolTip='<%# Eval("IDNO") %>' Checked='<%# Eval("ACCEPTED").ToString() == "1" ? true :false %>' Enabled='<%# Eval("ACCEPTED").ToString() == "1" ? false :true %>'/>
                                            </td>
                                            <td><a target="_blank" href='<%# Eval("SITE_URL_ENLISTMENT") %>'><%# Eval("ENROLLNO") %></a></td>
                                            <td><asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDNAME") %>'></asp:Label></td>
                                            <td><%# Eval("PROGRAMNAME") %></td>
                                            <td><%# Eval("REGISTER_STATUS") %> </td>
                                            <td><%# Eval("CREGDATE") %></td>
                                            <td><%# Eval("EXREGDATE") %></td>
                                            <td><%# Eval("UA_FULLNAME") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </ContentTemplate>

                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script>
    $(document).ready(function () {
        var table = $('#mytable').DataTable({
            responsive: true,
            lengthChange: true,
            scrollY: 450,
            scrollX: true,
            scrollCollapse: true,
            paging: false,
            dom: 'lBfrtip',
            buttons: [
                {
                    extend: 'colvis',
                    text: 'Column Visibility',
                    columns: function (idx, data, node) {
                        var arr = [0];
                        if (arr.indexOf(idx) !== -1) {
                            return false;
                        } else {
                            return $('#mytable').DataTable().column(idx).visible();
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
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#mytable').DataTable().column(idx).visible();
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
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    var thisOption = $(this).text();
                                                    nodereturn += thisOption;
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
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#mytable').DataTable().column(idx).visible();
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
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    var thisOption = $(this).text();
                                                        nodereturn += thisOption;
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
                var table = $('#mytable').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 450,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false,
                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#mytable').DataTable().column(idx).visible();
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
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#mytable').DataTable().column(idx).visible();
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
                                                    else if ($(node).find("a").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("a").each(function () {
                                                            var thisOption = $(this).text();
                                                            nodereturn += thisOption;
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
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#mytable').DataTable().column(idx).visible();
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
                                                    else if ($(node).find("a").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("a").each(function () {
                                                            var thisOption = $(this).text();
                                                            nodereturn += thisOption;
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
       <script type="text/javascript" language="javascript">

           function SelectAll(headchk) {
               var frm = document.forms[0]
               for (i = 0; i < document.forms[0].elements.length; i++) {
                   var e = frm.elements[i];
                   var s = e.name.split("ctl00$ContentPlaceHolder1$lvBindEnlismentList$ctrl");
                   var b = 'ctl00$ContentPlaceHolder1$lvBindEnlismentList$ctrl';
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

