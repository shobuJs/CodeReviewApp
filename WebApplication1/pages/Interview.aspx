<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Interview.aspx.cs" Inherits="Interview" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <style>
        .daterangepicker .drp-calendar .table-condensed {
            display: none;
        }

        .daterangepicker.show-ranges .drp-calendar .table-condensed,
        .daterangepicker.single .drp-calendar .table-condensed {
            display: block;
        }
    </style>
      <style>
        #ctl00_ContentPlaceHolder1_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
       <style>
        #ctl00_ContentPlaceHolder1_Panel3 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
     <script>
         $(document).ready(function () {
             var table = $('#mytable_int').DataTable({
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
                                 return $('#mytable_int').DataTable().column(idx).visible();
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
                return $('#mytable_int').DataTable().column(idx).visible();
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
            var arr = [0];
            if (arr.indexOf(idx) !== -1) {
                return false;
            } else {
                return $('#mytable_int').DataTable().column(idx).visible();
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
                 var table = $('#mytable_int').DataTable({
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
                                     return $('#mytable_int').DataTable().column(idx).visible();
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
                    return $('#mytable_int').DataTable().column(idx).visible();
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
                var arr = [0];
                if (arr.indexOf(idx) !== -1) {
                    return false;
                } else {
                    return $('#mytable_int').DataTable().column(idx).visible();
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

     <style>
        .dynamic-nav-tabs li.active a{
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
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <%--<div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>--%>
                <div class="nav-tabs-custom mt-2 col-12" id="myTabContent">
                    <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                        <li class="nav-item active" id="divlkschedule" runat="server">
                            <asp:LinkButton ID="lkschedule" runat="server" OnClick="lkschedule_Click" CssClass="nav-link" TabIndex="1">Schedule Interview</asp:LinkButton></li>
                        <li class="nav-item" id="divlkinterview" runat="server">
                            <asp:LinkButton ID="lnkinterview" runat="server" OnClick="lnkinterview_Click" CssClass="nav-link" TabIndex="2">Interview Result</asp:LinkButton></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade show active" id="divSchedule" role="tabpanel" runat="server" aria-labelledby="ALCourses-tab">
                            <div>
                                <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updInterViewSchedule"
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
                            <asp:UpdatePanel ID="updInterViewSchedule" runat="server">
                                <ContentTemplate>
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Intake</label>
                                                </div>
                                                <asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlIntake_SelectedIndexChanged">
                                                    <asp:ListItem>Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlIntake"
                                                    Display="None" ErrorMessage="Please Select Intake" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlIntake"
                                                    Display="None" ErrorMessage="Please Select Intake" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Study Level</label>
                                                </div>
                                                <asp:ListBox ID="ddlStudyLevel" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlStudyLevel_SelectedIndexChanged"></asp:ListBox>
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Program</label>
                                                </div>
                                                <asp:ListBox ID="ddlProgram" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true"></asp:ListBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Interview Date</label>
                                                </div>
                                                <asp:TextBox ID="txtInterViewDate" runat="server" CssClass="form-control interviewDate"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtInterViewDate"
                                                    Display="None" ErrorMessage="Please Enter Interview Date" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12 frm-to-time">
                                                <div class="label-dynamic">
                                                    <label>Time (From-To)</label>
                                                </div>
                                                <asp:TextBox ID="txttime" runat="server" CssClass="form-control" Width="100%" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txttime"
                                                    Display="None" ErrorMessage="Please Enter Time (From-To)" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Venue</label>
                                                </div>
                                                <asp:TextBox ID="txtVenue" runat="server" CssClass="form-control" MaxLength="200" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtVenue"
                                                    Display="None" ErrorMessage="Please Enter Venue" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" CssClass="btn btn-outline-info" Text="Show" OnClick="btnShow_Click" ValidationGroup="Show" />
                                        <asp:Button ID="btnSheduleInterview" runat="server" CssClass="btn btn-outline-info" Text="Shedule Interview" OnClick="btnSheduleInterview_Click" ValidationGroup="Submit" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn btn-outline-danger" Text="Cancel" OnClick="btnClear_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ValidationGroup="Show" ShowSummary="False" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ValidationGroup="Submit" ShowSummary="False" />
                                    </div>
                                    <div class="col-12 mt-3">
                                        <asp:Panel ID="Panel2" runat="server" Visible="false">
                                            <asp:ListView ID="lvInterviewSchedule" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>
                                                                    <asp:CheckBox ID="cbHead" runat="server" ToolTip="Select/Select all" OnClick="checkAllCheckbox(this)" /></th>
                                                                <th>Application ID</th>
                                                                <th>Applicant Name</th>
                                                                <th>Email ID</th>
                                                                <th>Program</th>
                                                                <th>Interview Date</th>
                                                                <th>Interview Time</th>
                                                                <th>Interview Venue</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="item">
                                                        <td>
                                                            <asp:CheckBox ID="chkAllot" runat="server" />
                                                            <asp:Label ID="lblUserNo" runat="server" Visible="false" Text='<%# Eval("USERNO") %>'></asp:Label>
                                                            <asp:Label ID="lblDegreeNo" runat="server" Visible="false" Text='<%# Eval("DEGREENO") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblFullName" runat="server" Text='<%# Eval("FULLNAME") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("EMAILID") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblProgram" runat="server" Text='<%# Eval("DEGREENAME") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("INTERVIEW_DATE") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblTime" runat="server" Text='<%# Eval("INTERVIEW_TIME") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblVenue" runat="server" Text='<%# Eval("INTERVIEW_VENUE") %>'></asp:Label>
                                                        </td>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </ContentTemplate>
                                <%-- <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnClear" />
                                    </Triggers>--%>
                            </asp:UpdatePanel>
                        </div>
                        <div id="divInterview" runat="server" visible="false" role="tabpanel" aria-labelledby="Grade-tab">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updInterviewResult"
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
                            <asp:UpdatePanel ID="updInterviewResult" runat="server">
                                <ContentTemplate>
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Intake</label>
                                                </div>
                                                <asp:DropDownList ID="ddlResultIntake" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlResultIntake_SelectedIndexChanged">
                                                    <asp:ListItem>Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlResultIntake"
                                                    Display="None" ErrorMessage="Please Select Intake" ValidationGroup="ShowResult" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Study Level</label>
                                                </div>
                                                <asp:ListBox ID="ddlStudyLevelResult" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlStudyLevelResult_SelectedIndexChanged"></asp:ListBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Program</label>
                                                </div>
                                                <%--<asp:ListBox ID="ddlProgramResult" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true"></asp:ListBox>--%>

                                                <asp:ListBox ID="ddlProgramResult" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlProgramResult_SelectedIndexChanged"></asp:ListBox>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Interview Date</label>
                                                </div>
                                                <asp:DropDownList ID="ddlInterViewDate" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                    <asp:ListItem>Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlInterViewDate"
                                                    Display="None" ErrorMessage="Please Select Interview Date" ValidationGroup="ShowResult" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShowResult" runat="server" CssClass="btn btn-outline-info" Text="Show" OnClick="btnShowResult_Click" ValidationGroup="ShowResult" />
                                        <asp:Button ID="btnSubmitResult" runat="server" CssClass="btn btn-outline-info" Text="Submit" OnClick="btnSubmitResult_Click" Visible="false" />
                                        <asp:Button ID="btnClearResult" runat="server" CssClass="btn btn-outline-danger" Text="Cancel" OnClick="btnClearResult_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ValidationGroup="ShowResult" ShowSummary="False" />
                                    </div>
                                    <div class="col-12 mt-3">
                                        <asp:Panel ID="Panel3" runat="server" Visible="false">
                                            <asp:ListView ID="lvInterviewResult" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                                <h5>Interview Result List</h5>
                                                            </div>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="mytable_int">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>
                                                                    <asp:CheckBox ID="cbHeadAll" runat="server" ToolTip="Select/Select all" OnClick="checkAllCheckboxsem(this)" /></th>
                                                                <th>Application ID</th>
                                                                <th>Applicant Name</th>
                                                                <th>Email ID</th>
                                                                <th>Program</th>
                                                                <th>Status</th>
                                                                <th>Remark</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="item">
                                                        <td>
                                                            <asp:CheckBox ID="chkResult" runat="server" />
                                                            <asp:Label ID="lblUserNoResult" runat="server" Visible="false" Text='<%# Eval("USERNO") %>'></asp:Label>
                                                            <asp:Label ID="lblDegreeNoResult" runat="server" Visible="false" Text='<%# Eval("DEGREENO") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblUserNameResult" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblFullNameResult" runat="server" Text='<%# Eval("FULLNAME") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblEmailResult" runat="server" Text='<%# Eval("EMAILID") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblProgramResult" runat="server" Text='<%# Eval("DEGREENAME") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Rejected</asp:ListItem>
                                                                <asp:ListItem Value="2">Selected</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblStats" runat="server" Visible="false" Text='<%# Eval("INTERVIEW_RESULT_STATUS") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" MaxLength="100" Text='<%# Eval("INTERVIEW_RESULT_REMARK") %>'></asp:TextBox>
                                                        </td>

                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </div>
            <script type="text/javascript">

                function checkAllCheckboxsem(headchk) {
                    var frm = document.forms[0]
                    for (i = 0; i < document.forms[0].elements.length; i++) {
                        var e = frm.elements[i];
                        var s = e.name.split("ctl00$ContentPlaceHolder1$lvInterviewResult$ctrl");
                        var b = 'ctl00$ContentPlaceHolder1$lvInterviewResult$ctrl';
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

                function checkAllCheckbox(headchk) {
                    var frm = document.forms[0]
                    for (i = 0; i < document.forms[0].elements.length; i++) {
                        var e = frm.elements[i];
                        var s = e.name.split("ctl00$ContentPlaceHolder1$lvInterviewSchedule$ctrl");
                        var b = 'ctl00$ContentPlaceHolder1$lvInterviewSchedule$ctrl';
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
            <!-- MultiSelect Script -->
            <script type="text/javascript">
                $(document).ready(function () {
                    $('.multi-select-demo').multiselect({
                        includeSelectAllOption: true,
                        maxHeight: 200
                    });
                });
                var parameter = Sys.WebForms.PageRequestManager.getInstance();
                parameter.add_endRequest(function () {
                    $('.multi-select-demo').multiselect({
                        includeSelectAllOption: true,
                        maxHeight: 200
                    });
                });

            </script>
            <script>
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
                    $('#ctl00_ContentPlaceHolder1_txttime').val('');
                });
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function () {
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
                        $('#ctl00_ContentPlaceHolder1_txttime').val('');
                    });
                });
            </script>
            <script>
                $(function () {
                    $('#ctl00_ContentPlaceHolder1_txtInterViewDate').daterangepicker({
                        singleDatePicker: true,
                        locale: {
                            format: 'DD-MM-YYYY'
                        },
                        //<!-- ========= Disable dates before today ========== -->
                        minDate: new Date(),
                        //<!-- ========= Disable dates before today END ========== -->
                    });
                    $('#ctl00_ContentPlaceHolder1_txtInterViewDate').val('');
                });

                var parameter = Sys.WebForms.PageRequestManager.getInstance();
                parameter.add_endRequest(function () {
                    $(function () {
                        $('#ctl00_ContentPlaceHolder1_txtInterViewDate').daterangepicker({
                            singleDatePicker: true,
                            locale: {
                                format: 'DD-MM-YYYY'
                            },
                            //<!-- ========= Disable dates before today ========== -->
                            minDate: new Date(),
                            //<!-- ========= Disable dates before today END ========== -->
                        });
                        $('#ctl00_ContentPlaceHolder1_txtInterViewDate').val('');
                    });
                });

            </script>
</asp:Content>

