<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AdminMultipleLogin.aspx.cs" Inherits="ACADEMIC_AdminMultipleLogin"
    ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="UpdateImport" runat="server" AssociatedUpdatePanelID="updpnlUser"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>


    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tbluser1').DataTable({
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
                            var arr = [2];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tbluser1').DataTable().column(idx).visible();
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
                                        var arr = [2];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tbluser1').DataTable().column(idx).visible();
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
                                        var arr = [2];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tbluser1').DataTable().column(idx).visible();
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
                var table = $('#tbluser1').DataTable({
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
                                var arr = [2];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tbluser1').DataTable().column(idx).visible();
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
                                            var arr = [2];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tbluser1').DataTable().column(idx).visible();
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
                                            var arr = [2];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tbluser1').DataTable().column(idx).visible();
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

    <asp:UpdatePanel ID="updpnlUser" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title"><%--ONLINE PAYMENT--%>
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>User Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlUserType" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged" TabIndex="1">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="01">Application</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-3 col-md-6 col-12" id="trDept" visible="false" runat="server">
                                    <div class="label-dynamic">
                                        <label>Department</label>
                                    </div>
                                    <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                        <%--<asp:ListItem Value="0">Please Select</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-lg-6 col-12" id="pnlStudent" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblSearch" runat="server" Font-Bold="true" Text="User Name"></asp:Label>
                                                <%-- <label>User Name</label>--%>
                                            </div>
                                            <asp:TextBox ID="txtSearch" runat="server" MaxLength="50" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtSearch" Enabled="false"
                                                Display="None" ErrorMessage="Please Enter Registration No." ValidationGroup="studSearch"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-12 mt-3">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" ValidationGroup="studSearch" CssClass="btn btn-outline-info" TabIndex="1" />
                                            <asp:ValidationSummary ID="vsSum1" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="studSearch" />
                                        </div> 
                                    </div>
                                </div>
                                <div class="col-md-12 col-12">
                                    <div class="btn-foter">
                                        <div id="divMsg" runat="server"></div>
                                        <asp:Label ID="lblEmpty" Font-Bold="true" Text="No Record!" Visible="false" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 mt-3">
                            <asp:ListView ID="lvlinks" runat="server" OnItemDataBound="lvlinks_ItemDataBound">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Users Account Info List</h5>
                                    </div>
                                    <%-- <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tbluser1">--%>
                                    <table class="table table-striped table-bordered" style="width: 100%" id="Table1">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Username
                                                </th>
                                                <th>Name
                                                </th>
                                                <th>Purpose For Login
                                                </th>
                                                <th>Login As
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
                                        <td>
                                            <%# Eval("UA_NAME")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblUser" runat="server" Text='<%# Eval("UA_FULLNAME")%>' ToolTip='<%# Eval("UA_NO")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <div class="text-center d-flex align-items-center">
                                                <asp:DropDownList ID="ddlPurpose" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlPurpose_SelectedIndexChanged">
                                                    <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtReson" runat="server" Visible="false"
                                                    placeholder="Maximum 200 Character Allowed." MaxLength="200" CssClass="form-control"></asp:TextBox>
                                                <span id="charCount"></span>
                                                <asp:Button ID="btnAddReason" runat="server" Text="Add New Purpose" CommandArgument='<%# Eval("UA_NAME") %>' CssClass="btn btn-outline-info"
                                                    AlternateText="Purpose for Login " ToolTip='<%# Eval("UA_NO")%>' OnClick="btnAddReason_Click" />
                                            </div>
                                        </td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="btnLogin" runat="server" CommandArgument='<%# Eval("UA_NAME") %>' ToolTip='<%# Eval("UA_NO")%>'
                                                OnClick="btnLogin_Click" TabIndex="1">
                                                <i class="fa fa-user" aria-hidden="true"></i></asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="ddlDept" />
        </Triggers>--%>

        <%-- <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlPurpose" EventName="SelectedIndexChanged" />
        </Triggers>--%>
    </asp:UpdatePanel>

    <!-- The Modal -->

    <%--<asp:Button ID="btnForPopUpModel" Style="display: none" runat="server" Text="For PopUp Model Box" />--%>
    <%--<ajaxToolKit:ModalPopupExtender ID="upd_ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
        PopupControlID="pnlVerifyOTP" TargetControlID="btnForPopUpModel"
        Enabled="True">
    </ajaxToolKit:ModalPopupExtender>--%>

    <!-- The Modal -->
    <div class="modal fade" id="pnlVerifyOTP">
        <div class="modal-dialog">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Verify OTP</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <div class="col-12" id="dvOTP" runat="server">
                        <div class="row">
                            <div class="form-group col-lg-6 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>OTP</label>
                                </div>
                                <asp:TextBox ID="txtVerifyOTP" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label ID="lblTimer" ForeColor="Green" runat="server"></asp:Label>
                            </div>
                            <%-- <div class="col-lg-6 col-md-6 col-12 mt-3">
                                <asp:Button ID="btnVerifyOTP" runat="server" Text="Verify OTP" OnClick="btnVerifyOTP_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnResndOTP" runat="server" Visible="false" Text="Resend OTP" OnClick="btnResndOTP_Click" CssClass="btn btn-primary" />
                            </div>--%>
                            <div class="col-12 text-center">
                                <asp:Button ID="btnVerifyOTP" runat="server" Text="Verify OTP" OnClick="btnVerifyOTP_Click" CssClass="btn btn-outline-info" />
                                <asp:Button ID="btnResndOTP" runat="server" Text="Resend OTP" OnClick="btnResndOTP_Click" CssClass="btn btn-outline-info" />
                                <asp:Button ID="btnback" runat="server" Text="Close" OnClick="btnback_Click" CssClass="btn btn-outline-danger" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        function myfunction() {
            // $('.modal').Open();
            $("#pnlVerifyOTP").modal('show');
        }
    </script>

    <script>
        k = 60;
        function onTimer() {
            document.getElementById('<%=lblTimer.ClientID%>').innerHTML = 'Time Left : ' + k + ' Sec';
            k--;

            document.getElementById('<%=btnResndOTP.ClientID%>').style.display = "none";
            document.getElementById('<%=btnVerifyOTP.ClientID%>').enabled = true;
            if (k < 0) {
                document.getElementById('<%=lblTimer.ClientID%>').innerHTML = "";
                document.getElementById('<%=btnVerifyOTP.ClientID%>').style.display = "none";
                alert('OTP Timeout!');
                document.getElementById('<%=btnResndOTP.ClientID%>').style.display = "block";
            }
            else {
                setTimeout(onTimer, 1000);
            }
        }
    </script>

    <%--  <script>
       function ValidateChar(txt) {
           var maxLength = 10;
           var textID = txt.id;
          // parseInt(textID.value)
           var textLength = textID.value.length;
           var remainingChars = maxLength - textLength;
           $('#charCount').text(remainingChars + ' characters remaining');
           if (textLength > maxLength) {
               $(this).val($(this).val().substring(0, maxLength));
           }
       }
</script>--%>
   
</asp:Content>