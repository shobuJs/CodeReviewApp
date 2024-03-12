<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Promissory_Note_Approval.aspx.cs" Inherits="MockUps_Promissory_Note_Approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPromissoryDetails"
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
    <asp:UpdatePanel ID="updPromissoryDetails" runat="server">
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
                                    <div class="col-xl-12 col-lg-12 col-sm-12 col-12">
                                        <div class="form-group">
                                            <asp:RadioButtonList ID="rdobtn" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdobtn_SelectedIndexChanged">
                                                <asp:ListItem Value="0">&nbsp; Pending &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="1">&nbsp; Approved &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2">&nbsp; Rejected &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="3">&nbsp; All</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 text-center">
                                <asp:Button ID="btn_excel_download" runat="server" Text="Promissory Details" class="btn btn-outline-primary" OnClick="btn_excel_download_Click"/>
                            </div>

                            <div class="col-12 mt-3">
                                <div class="table-responsive">
                                    <asp:ListView ID="lvPromissoryNote" runat="server">
                                        <LayoutTemplate>

                                            <table id="mytable" class="table table-hover table-bordered" width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>Student ID</th>
                                                        <th>Student Name</th>
                                                        <th>Semester</th>
                                                        <th>Fees Details</th>
                                                        <th>Amount</th>
                                                        <th>Due Date</th>
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
                                                    <asp:LinkButton ID="lnkEnroll" runat="server" CommandName='<%# Eval("SRNO") %>' Text='<%# Eval("ENROLLNO") %>' CommandArgument='<%# Eval("ENROLLNO") + "$" + Eval("STUDNAME") + "$" + Eval("COLLEGE_NAME") + "$" + Eval("STUDENTMOBILE") + "$" + Eval("EMAILID") + "$" + Eval("PROGRAM") + "$" + Eval("INSTALLMENT_AMOUNT") + "$" + Eval("PROMISSORY_REASON") + "$" + Eval("PROMISSORYSTAUS") + "$" + Eval("PROMISSORY_DATE") + "$" + Eval("PROMISSORY_REMARK") %>' OnClick="lnkEnroll_Click"></asp:LinkButton>
                                                </td>
                                                <td>
                                                    <%# Eval("STUDNAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTERNAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("INSTALL_NAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("INSTALLMENT_AMOUNT") %>
                                                </td>
                                                <td>
                                                    <%# Eval("DUE_DATE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("PROMISSORY_STATUS") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_excel_download" /> 
        </Triggers>
    </asp:UpdatePanel>
    <!-- The Modal -->
    <div class="modal fade" id="StudentModal">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Student Details</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>

                        <!-- Modal body -->
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Student ID :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblStudentID" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Student Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblStudentName" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>College :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblCollege" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>

                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Mobile :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblMobile" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Email :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblEmail" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Program :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblProgram" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>
                            </div>

                            <div class="row mt-4">
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Due Amount :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblDueAmt" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </a>
                                        </li>
                                    </ul>
                                </div>

                                <div class="col-xl-5 col-lg-5 offset-lg-1 col-sm-6 col-12">
                                    <div class="form-inline">
                                        <div class="label-dynamic d-flex">
                                            <sup style="top:0">* </sup>
                                            <label>Promise to be Pay Date</label>
                                        </div>
                                        <asp:TextBox type="date" id="txtPromissoryDate" runat="server" class="form-control" tabindex="0"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row mt-4">
                                <div class="col-lg-12 col-md-12 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label><span>Reason</span></label>
                                        </div>
                                        <textarea class="form-control" runat="server" rows="2" id="txtReason" tabindex="0" disabled></textarea>
                                    </div>
                                </div>

                                <div class="col-xl-6 col-lg-6 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>Status</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="0" AppendDataBoundItems="true">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Pending</asp:ListItem>
                                            <asp:ListItem Value="1">Approved</asp:ListItem>
                                            <asp:ListItem Value="2">Rejected</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Select Status"
                                                    ControlToValidate="ddlStatus" InitialValue="-1" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-xl-6 col-lg-6 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>Remark</span></label>
                                        </div>
                                        <textarea class="form-control" runat="server" maxlength="100" rows="2" id="Remark" tabindex="0"></textarea>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Remark"
                                                    ControlToValidate="Remark" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Modal footer -->
                        <div class="modal-footer">
                            <!-- Buttons -->
                            <div class="text-center mt-2 mb-3">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="Submit"/>
                                <input type="button" class="btn btn-outline-danger" tabindex="0" value="Cancel" id="btnCancel" data-dismiss="modal" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
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
                            var arr = [];
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
                                            var arr = [];
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
                                            var arr = [];
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
                                            var arr = [];
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
                                var arr = [];
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
                                                var arr = [];
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
                                                var arr = [];
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
                                                var arr = [];
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

