<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Withdrawal_Aproval.aspx.cs" Inherits="Projects_Withdrawal_Aproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        input[type=checkbox], input[type=radio] {
            margin: 2px 3px 0;
        }

        .badge {
            display: inline-block;
            padding: 5px 10px 7px;
            border-radius: 15px;
            font-size: 100%;
            width: 80px;
        }

        .badge-warning {
            color: #fff !important;
        }

        td .fa-eye {
            font-size: 18px;
            color: #0d70fd;
        }
    </style>
    
<script>
    $(document).ready(function () {
        var table = $('#myTable').DataTable({
            responsive: true,
            lengthChange: true,
            scrollY: 320,
            scrollX: true,
            scrollCollapse: true,
            //paging: false, // Added by Gaurav for Hide pagination

            dom: 'lBfrtip',
            buttons: [
                {
                    extend: 'colvis',
                    text: 'Column Visibility',
                    columns: function (idx, data, node) {
                        var arr = [0,9];
                        if (arr.indexOf(idx) !== -1) {
                            return false;
                        } else {
                            return $('#myTable').DataTable().column(idx).visible();
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
                                    var arr = [0,9];
                                    if (arr.indexOf(idx) !== -1) {
                                        return false;
                                    } else {
                                        return $('#myTable').DataTable().column(idx).visible();
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
                                    var arr = [0,9];
                                    if (arr.indexOf(idx) !== -1) {
                                        return false;
                                    } else {
                                        return $('#myTable').DataTable().column(idx).visible();
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
            var table = $('#myTable').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                //paging: false, // Added by Gaurav for Hide pagination

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0,9];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#myTable').DataTable().column(idx).visible();
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
                                       var arr = [0,9];
                                       if (arr.indexOf(idx) !== -1) {
                                           return false;
                                       } else {
                                           return $('#myTable').DataTable().column(idx).visible();
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
                                       var arr = [0,9];
                                       if (arr.indexOf(idx) !== -1) {
                                           return false;
                                       } else {
                                           return $('#myTable').DataTable().column(idx).visible();
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                    </h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:RadioButtonList ID="rdbFilter" runat="server" RepeatColumns="8" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1"><span style="padding-left:5px">Pending</span></asp:ListItem>
                                    <asp:ListItem Value="2"><span style="padding-left:5px">Completed</span></asp:ListItem>
                                    <asp:ListItem Value="3"><span style="padding-left:5px">All</span></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="rdbFilter"
                                    Display="None" ErrorMessage="Please select Status" ValidationGroup="show" InitialValue="0"></asp:RequiredFieldValidator>
                                <asp:HiddenField ID="hdnDate" runat="server" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Request Type</label>
                                </div>
                                <asp:DropDownList ID="ddlWithdrawalType" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <%--  <asp:ListItem Value="1">Admission Withdrawal</asp:ListItem>
                                    <asp:ListItem Value="2">Semester Registration</asp:ListItem>
                                    <asp:ListItem Value="3">Postponement</asp:ListItem>
                                    <asp:ListItem Value="4">Pro-Rata</asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Date (From-To)</label>
                                </div>
                                <div id="picker" class="form-control">
                                    <i class="fa fa-calendar"></i>&nbsp;
                                    <span id="date"></span>
                                    <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                </div>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Faculty School Name</label>
                                </div>
                                <asp:DropDownList ID="ddlfaculty" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-outline-info" ValidationGroup="show" OnClick="btnShow_Click">Show</asp:LinkButton>
                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="show" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>


                    <div class="col-md-12" id="divwithapti" runat="server" visible="false">
                        <asp:Panel ID="Panel2" runat="server">
                            <asp:ListView ID="lvwithap" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Withdrawal / Postponement Approval</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="myTable">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Sr.No.</th>
                                                <th>Request ID</th>
                                                <th>Request Type</th>
                                                <th>Student ID</th>
                                                <th>Registration No.</th>
                                                <th>Student Name</th>
                                                <th>Request Date</th>
                                                <th>Details</th>
                                                <th>Status</th>
                                                <th>Uploaded Document</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>

                                        <td style="text-align: center"><%# Container.DataItemIndex + 1 %></td>
                                        <td>
                                            <%# Eval("SRNO")%> 
                                        </td>
                                        <td>
                                            <%# Eval("REQUEST_TYPE")%> 
                                        </td>

                                        <td>
                                            <%# Eval("REGNO")%> 
                                        </td>
                                        <td>
                                            <%# Eval("ENROLLNO")%> 
                                        </td>
                                        <td>
                                            <%# Eval("STUDFIRSTNAME")%> 
                                        </td>
                                        <td>

                                            <%# Eval("APPLIED_DATE")%> 
                                        </td>
                                        <td style="text-align: center">
                                            <%--<asp:LinkButton ID="lnkViewDoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("IDNO") %>' CommandName='<%#Eval("DOCUMENT") %>' OnClick="lnkViewDoc_Click"><i class="fa fa-eye"></i> View</asp:LinkButton>--%>
                                            <asp:LinkButton ID="lnkViewDoc" runat="server" CommandArgument='<%#Eval("IDNO") %>' CommandName='<%# Eval("SRNO") %>' ToolTip='<%# Eval("STATUS_NO")%>' OnClick="lnkViewDoc_Click"><i class="fa fa-eye" aria-hidden="true" style="color: #0d70fd; font-size: 24px;"></i></asp:LinkButton>

                                        </td>
                                        <td class="text-center"><span class="badge badge-success"><%#Eval("WITH_POST_APPROVAL") %></span></td>
                                        <td>
                                            <asp:LinkButton ID="lnkstuddoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("SRNO") %>' CommandName='<%#Eval("DOCUMENT") %>' ToolTip='<%#Eval("DOCUMENT") %>' OnClick="lnkstuddoc_Click"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                </div>


            </div>
            <asp:UpdatePanel ID="updModelPopup" runat="server">
                <ContentTemplate>
                    <div id="myModal22" class="modal fade" role="dialog" data-backdrop="static">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content" style="margin-top: -25px">
                                <div class="modal-body">
                                    <div class="modal-header">
                                        <%--<button type="button" class="close" data-dismiss="modal" style="margin-top: -18px"  >x</button>--%>
                                        <%--    <button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                                        <asp:LinkButton ID="lnkClose" runat="server" CssClass="close" Style="margin-top: -18px"  OnClick="lnkClose_Click">x</asp:LinkButton>
                                    </div>
                                    <asp:Image ID="ImageViewer" runat="server" Width="100%" Height="500px" Visible="false" />
                                    <asp:Literal ID="ltEmbed" runat="server" Visible="false" />
                                     <iframe style=" width: 100%; height: 500px;" id="irm1" src="~/PopUp.aspx" runat="server"></iframe> 
                                    <asp:LinkButton ID="lnkCloseModel" runat="server" CssClass="btn btn-default" Style="margin-top: -10px" OnClick="lnkClose_Click">Close</asp:LinkButton>

                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkClose" />
                    <asp:PostBackTrigger ControlID="lnkCloseModel" />
                </Triggers>
            </asp:UpdatePanel>
            <!-- View Modal Finance-->
            <asp:UpdatePanel ID="updmodal" runat="server">
                <ContentTemplate>
                    <div class="modal" id="Veiw_Finance">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">

                                <div class="modal-header">
                                    <h4 class="modal-title">View Details</h4>

                                    <button type="button" class="close" data-dismiss="modal" runat="server" onclick="javascript:doButtonPostBack();">&times;</button>
                                </div>

                                <div class="modal-body pl-0 pr-0">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Student ID :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblStdID" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Student Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblStdName" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                        <asp:HiddenField ID="hdfSrnoWithDrwal" runat="server" />
                                                        <asp:HiddenField ID="hdfIdnoWithDrwal" runat="server" />
                                                    </li>
                                                    <li class="list-group-item"><b>Request Date :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblRequestDate" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Request Type :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblrequestedtype" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Faculty/School Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblFaculty" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Program :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblProgram" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Semester :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSemester" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                <div class="label-dynamic">
                                                    <label>Request Description</label>
                                                </div>
                                                <asp:TextBox ID="txtRequestDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Status</label>
                                                </div>
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Approved</asp:ListItem>
                                                    <asp:ListItem Value="2">Pending</asp:ListItem>
                                                    <asp:ListItem Value="3">Reject</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlStatus"
                                                    Display="None" ErrorMessage="Please select Status" ValidationGroup="showmodal" InitialValue="0"></asp:RequiredFieldValidator>
                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Refund Status</label>
                                                </div>
                                                <asp:DropDownList ID="ddlrefundstatus" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlrefundstatus_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Withdrawal without Refund</asp:ListItem>
                                                    <asp:ListItem Value="2">Withdrawal with Refund</asp:ListItem>

                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlrefundstatus"
                                                    Display="None" ErrorMessage="Please select Refund Status" ValidationGroup="showmodal" InitialValue="0"></asp:RequiredFieldValidator>
                                                <asp:HiddenField ID="HiddenField4" runat="server" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Policy Name </label>
                                                </div>
                                                <asp:DropDownList ID="ddlPolicyName" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPolicyName_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlPolicyName"
                                                    Display="None" ErrorMessage="Please Select Policy Name" ValidationGroup="calculate" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Previous Refund </label>
                                                </div>
                                                <asp:TextBox ID="TxtPreviousrefund" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Remaining Refund </label>
                                                </div>
                                                <asp:TextBox ID="TxtRemainingRefund" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label></label>
                                                </div>
                                                <asp:LinkButton ID="btnCalculateRefund" runat="server" CssClass="btn btn-outline-info" OnClick="btnCalculateRefund_Click" ValidationGroup="calculate">Calculate Refund</asp:LinkButton>
                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="calculate" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-4 col-md-4 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Paid Amount :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblpaid" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Refundable Amount :</b>
                                                        <a class="sub-label">
                                                            <asp:TextBox ID="lblrefund" runat="server" Font-Bold="true" onblur="return UpdateTotalAndBalance(this);" onkeyup="IsNumeric(this);"></asp:TextBox>
                                                            <%--<asp:Label ID="lblrefund" runat="server" Text="" Font-Bold="true"></asp:Label>--%>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-12 pl-0">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Non-Refundable Amount :</b>
                                                        <a class="sub-label">
                                                            <asp:TextBox ID="lblnonrefund" runat="server" Font-Bold="true" onkeyup="IsNumeric(this);"></asp:TextBox>
                                                            <%--<asp:Label ID="lblnonrefund" runat="server" Text="" Font-Bold="true"></asp:Label></a>--%>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Remark</label>
                                                </div>
                                                <asp:TextBox ID="txtremark" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtremark"
                                                    Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="showmodal"></asp:RequiredFieldValidator>
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <%--<sup>* </sup>--%>
                                                    <label>Remark For Student</label>
                                                </div>
                                                <asp:TextBox ID="TxtRemarkStu" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtRemarkStu"
                                                    Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="showmodal"></asp:RequiredFieldValidator>--%>
                                                <asp:HiddenField ID="HiddenField3" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:LinkButton ID="btnSubmit1" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmit1_Click" ValidationGroup="showmodal">Submit</asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel1" runat="server" CssClass="btn btn-outline-danger">Cancel</asp:LinkButton>
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="showmodal" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit1" />
                    <asp:PostBackTrigger ControlID="btnCancel1" />
                    <asp:PostBackTrigger ControlID="btnCalculateRefund" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <!-- Start End Date Script -->
    <script>
        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    document.getElementById(textbox.id).value = '';
                }
            }
        }
    </script>
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

                // $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
            });
    </script>
    <script type="text/javascript" language="javascript">

        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvwithap$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvwithap$ctrl';
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
    <script type="text/javascript">
        function doButtonPostBack() {
            __doPostBack('Withdrawal_Aproval.aspx', '');
        }
    </script>
    <script>
        function UpdateTotalAndBalance() {
            try {
                
                var Amount = 0;
                var refundamt = 0;
                Amount = document.getElementById('ctl00_ContentPlaceHolder1_lblrefund').value;
                refundamt = document.getElementById('ctl00_ContentPlaceHolder1_TxtRemainingRefund').value;
                if(refundamt !=null && refundamt != ""){       
                    if (Number(Amount) > Number(refundamt)) {
                        alert("Amount should not be Greater Than Remaining Refundable Amount " + refundamt + "");
                        document.getElementById('ctl00_ContentPlaceHolder1_lblrefund').value= '';
                        document.getElementById('ctl00_ContentPlaceHolder1_lblnonrefund').value= '';
                        return;
                    }
                }
            }
            catch (e) {
            }
        }
    </script>
</asp:Content>

