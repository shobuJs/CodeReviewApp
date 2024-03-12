<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Add_Campaign.aspx.cs" Inherits="Add_Campaign" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>

        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updCampaign"
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

     <%--===== Data Table Script added by gaurav =====--%>
        <script>
            $(document).ready(function () {
                var table = $('#mytable_detail').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

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
                                    return $('#mytable_detail').DataTable().column(idx).visible();
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
                                                return $('#mytable_detail').DataTable().column(idx).visible();
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
                                                else if ($(node).find("a").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("a").each(function () {
                                                        nodereturn += $(this).text();
                                                    });
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).text();
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
                                                else if ($(node).find("img").length > 0) {
                                                    nodereturn = "";
                                                }
                                                else if ($(node).find("input:hidden").length > 0) {
                                                    nodereturn = "";
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
                                                return $('#mytable_detail').DataTable().column(idx).visible();
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
                                                else if ($(node).find("a").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("a").each(function () {
                                                        nodereturn += $(this).text();
                                                    });
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).text();
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
                                                else if ($(node).find("img").length > 0) {
                                                    nodereturn = "";
                                                }
                                                else if ($(node).find("input:hidden").length > 0) {
                                                    nodereturn = "";
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
                    var table = $('#mytable_detail').DataTable({
                        responsive: true,
                        lengthChange: true,
                        scrollY: 320,
                        scrollX: true,
                        scrollCollapse: true,
                        paging: false, // Added by Gaurav for Hide pagination

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
                                        return $('#mytable_detail').DataTable().column(idx).visible();
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
                                                    return $('#mytable_detail').DataTable().column(idx).visible();
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
                                                    else if ($(node).find("a").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("a").each(function () {
                                                            nodereturn += $(this).text();
                                                        });
                                                    }
                                                    else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).text();
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
                                                    else if ($(node).find("img").length > 0) {
                                                        nodereturn = "";
                                                    }
                                                    else if ($(node).find("input:hidden").length > 0) {
                                                        nodereturn = "";
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
                                                    return $('#mytable_detail').DataTable().column(idx).visible();
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
                                                    else if ($(node).find("a").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("a").each(function () {
                                                            nodereturn += $(this).text();
                                                        });
                                                    }
                                                    else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).text();
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
                                                    else if ($(node).find("img").length > 0) {
                                                        nodereturn = "";
                                                    }
                                                    else if ($(node).find("input:hidden").length > 0) {
                                                        nodereturn = "";
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

    <asp:UpdatePanel ID="updCampaign" runat="server">
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
                                            <label>Intake</label>
                                        </div>
                                        <asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlIntake"
                                                        Display="None" ErrorMessage="Please Select Intake" ValidationGroup="btnCampaign" InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Study Level</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStudyLevel" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlStudyLevel_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStudyLevel"
                                                        Display="None" ErrorMessage="Please Select Study Level" ValidationGroup="btnCampaign" InitialValue="0" />
                                    </div>
                                </div>
                            </div>
                         

                            <div class="col-md-12">
                                <asp:Panel ID="Panel" runat="server">
                                    <asp:ListView runat="server" ID="lvDetails">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="mytable_detail">
                            <thead class="bg-light-blue">
                                <tr>
                                                        <th>Faculty</th>
                                                        <th>Program</th>
                                                        <th>Total Sign-Up</th>
                                                        <th>Total Application Filled</th>
                                                        <th>Total Paid</th>
                                                        <th>Total Admitted</th>

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
                                                    <asp:Label runat="server" ID="Label1" Text='<%#Eval("SHORT_NAME") %>'></asp:Label>
                                            
                                                    <asp:Label runat="server" ID="lblCollege_id" Text='<%#Eval("COLLEGE_ID") %>' Visible="false"></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                     <asp:Label runat="server" ID="Label2" Text='<%#Eval("PROGRAM") %>'></asp:Label>
                                                    <asp:Label runat="server" ID="lblDegreeno" Text='<%#Eval("DEGREENO") %>' Visible="false"></asp:Label>
                                                    <asp:Label runat="server" ID="lblBranchno" Text='<%#Eval("BRANCHNO") %>' Visible="false"></asp:Label>
                                                    <asp:Label runat="server" ID="lblAfilated" Text='<%#Eval("AFFILIATED_NO") %>' Visible="false"></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTotalSIgnUp" Text='<%#Eval("TOTAL_SIGN_UP") %>' runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTotalAppFilled" Text='<%#Eval("TOTAL_APPL_FILLED") %>' runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTotalPaid" Text='<%#Eval("TOTAL_PAID") %>' runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAd"  Text='<%#Eval("TOTAL_ADMITTED") %>' runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>


                            <%--    <div class="col-12 mt-3">
                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th>Faculty</th>
                                    <th>Program</th>
                                    <th>Total Sign-Up</th>
                                    <th>Total Application Filled</th>
                                    <th>Total Paid</th>
                                    <th>Total Admitted</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>FOC</td>
                                    <td>BSC (IT)</td>
                                    <td>
                                        <asp:TextBox ID="txtTotalSIgnUp" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalAppFilled" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalPaid" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAd" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>--%>

                            <div class="col-12 btn-footer mt-4">
                                <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-outline-info" TabIndex="3" OnClick="btnSave_Click" ValidationGroup="btnCampaign">Save</asp:LinkButton>
                             
                                 <asp:LinkButton ID="btnReport" runat="server" CssClass="btn btn-outline-primary" TabIndex="4" OnClick="btnReport_Click">Report</asp:LinkButton>
                                   <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" TabIndex="5" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                  <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="btnCampaign"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

