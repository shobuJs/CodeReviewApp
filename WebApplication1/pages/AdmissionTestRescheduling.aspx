<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AdmissionTestRescheduling.aspx.cs" Inherits="AdmissionTestRescheduling" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

        <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>
    <style>
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

        #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
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
    <style>
        #ctl00_ContentPlaceHolder1_LvReschedule_DataPager1 a:first-child,
        #ctl00_ContentPlaceHolder1_LvReschedule_DataPager1 a:last-child {
            padding: 5px 10px;
            border-radius: 0%;
            background: white;
            margin: 0 0px;
            box-shadow: none;
        }


        #ctl00_ContentPlaceHolder1_LvReschedule_DataPager1 a {
            padding: 5px 10px;
            border-radius: 50%;
            background: white;
            margin: 0 0px;
            box-shadow: 0 1px 3px rgb(0 0 0 / 12%), 0 1px 3px rgb(0 0 0 / 24%);
        }

        #ctl00_ContentPlaceHolder1_LvReschedule_DataPager1 span {
            padding: 5px 10px;
            border-radius: 50%;
            background: #4183c4;
            color: #fff;
            margin: 0 0px;
            box-shadow: 0 1px 3px rgb(0 0 0 / 12%), 0 1px 3px rgb(0 0 0 / 24%);
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updTestReSchedule"
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
    <asp:UpdatePanel ID="updTestReSchedule" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--  <h3 class="box-title"><span>Admission Test Rescheduling</span></h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--      <label>Intake</label>--%>
                                            <asp:Label ID="lblDYSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlintake" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1" OnSelectedIndexChanged="ddlintake_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlintake"
                                            Display="None" ErrorMessage="Please Select Intake" ValidationGroup="Show" InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--  <label>Study Level</label>--%>
                                            <asp:Label ID="lblDYPrgmtype" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlStudyLevel" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlStudyLevel_SelectedIndexChanged" AutoPostBack="true"
                                            AppendDataBoundItems="true" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStudyLevel"
                                            Display="None" ErrorMessage="Please Select Study Level" ValidationGroup="Show" InitialValue="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--  <label>Study Level</label>--%>
                                            <asp:Label ID="Label1" runat="server" Font-Bold="true">Month</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlMonths" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlMonths"
                                            Display="None" ErrorMessage="Please Select Month" ValidationGroup="Show" InitialValue="0" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-12" runat="server" visible="false" id="schedule">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <%--   <label>Exam Date</label>--%>
                                            <asp:Label ID="lblRAExamDate" runat="server" Font-Bold="true"></asp:Label>
                                        </div>

                                        <asp:DropDownList ID="ddlExamDate" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlExamDate_SelectedIndexChanged" AutoPostBack="true"
                                            TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlExamDate"
                                            Display="None" ErrorMessage="Please Select Exam Date" ValidationGroup="Submit" InitialValue="0" />
                                        <asp:HiddenField runat="server" ID="hdnCapacity" Value="0" />
                                        <asp:HiddenField runat="server" ID="hdnTotal" Value="0" />
                                        <input type="hidden" id="hdnStatus" value="Yes" />

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Time Slot</label>--%>
                                            <asp:Label ID="lblTimeSlot" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlTimeSlot" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlTimeSlot_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTimeSlot"
                                            Display="None" ErrorMessage="Please Select Time Slot" ValidationGroup="Submit" InitialValue="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--     <label>Venue</label>--%>
                                            <asp:Label ID="lblVenue" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlVenue" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="3">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlVenue"
                                            Display="None" ErrorMessage="Please Select Venue" ValidationGroup="Submit" InitialValue="0" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer mt-3">
                                <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="Show" CssClass="btn btn-outline-info" OnClick="btnShow_Click" />
                                <asp:Button ID="btnReport" runat="server" Text="List Of Applicant" CssClass="btn btn-outline-info" ValidationGroup="Show" OnClick="btnReport_Click" />
                                <asp:Button ID="btnHallReport" runat="server" Text="Hall Ticket" CssClass="btn btn-outline-info" Visible="false" OnClick="btnHallReport_Click" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click" Visible="false" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Show"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div id="divMsg" runat="server">
                            </div>
                            <%--<div class="col-12 mt-3">
                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                            <thead>
                                <tr class="bg-light-blue">
                                    <th>
                                        <input id="Checkbox2" type="checkbox" />
                                    </th>
                                    <th>Application No. </th>
                                    <th>Student Name </th>
                                    <th>Email Id </th>
                                    <th>Exam Date </th>
                                    <th>Time Slot</th>
                                    <th>Venue </th>
                                    <th>Payment Status </th>
                                    <th>Reshedule Name  </th>
                                    <th>Reshedule Date </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <input id="Checkbox1" type="checkbox" /></td>
                                    <td><a href="#">VSA00001</a> </td>
                                    <td>Priti Bondre</td>
                                    <td>pbondre@gmail.com</td>
                                    <td>12/02/2023</td>
                                    <td>08:00am - 09:00am</td>
                                    <td>Nagpur</td>
                                    <td><span class="badge badge-pill badge-success">Paid</span></td>
                                    <td>Priti Bondre</td>
                                    <td>19/02/2023</td>
                                </tr>
                                <tr>
                                    <td>
                                        <input id="Checkbox3" type="checkbox" /></td>
                                    <td><a href="#">VSA00003 </a></td>
                                    <td>Prathmesh Pande</td>
                                    <td>prathmesh@gmail.com</td>
                                    <td>19/02/2023</td>
                                    <td>10:00am - 11:00am</td>
                                    <td>Nagpur</td>
                                    <td><span class="badge badge-pill badge-danger">Pending</span></td>
                                    <td>Prathmesh Pande</td>
                                    <td>20/02/2023</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>--%>
                            <%--                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server" Visible="false">--%>
                            <asp:ListView ID="LvReschedule" runat="server" OnPagePropertiesChanging="OnPagePropertiesChanging">
                                <LayoutTemplate>
                                    <div id="demo-grid">
                                        <div class="row mb-1">
                                            <div class="col-lg-2 col-md-6 offset-lg-7">
                                                <button type="button" class="btn btn-outline-primary float-lg-right saveAsExcel">Export Excel</button>
                                            </div>

                                            <div class="col-lg-3 col-md-6">
                                                <div class="input-group sea-rch">
                                                    <input type="text" id="FilterData" class="form-control" placeholder="Search" />
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-search"></i>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="MainLeadTable">
                                                <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="cbHeadReg" runat="server" OnClick="checkAllCheckbox(this)" />
                                                        </th>
                                                        <th>Application No. </th>
                                                        <th>Student Name </th>
                                                        <th>Email Id </th>
                                                        <th>Exam Date </th>
                                                        <th>Time Slot</th>
                                                        <th>Venue </th>
                                                        <th>Payment Status </th>
                                                        <th>Reschedule Request</th>
                                                        <th>Reschedule By  </th>
                                                        <th>Date </th>
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
                                            <asp:CheckBox ID="chkRegister" onclick="SelectSeperate(this)" CssClass="CheckAllSeats" runat="server" ToolTip='<%# Eval("USERNO")%>' />
                                        </td>
                                        <td>
                                            <%# Eval("USERNAME")%>
                                        </td>

                                        <td>
                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("FULLNAME")%>'></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lblEmailid" runat="server" Text='<%# Eval("EMAILID")%>'></asp:Label>
                                        </td>

                                        <td>
                                            <%# Eval("EXAM_DATE")%>
                                        </td>

                                        <td>
                                            <%# Eval("TIME_SLOT")%>
                                        </td>
                                        <td>
                                            <%# Eval("VENUE_NAME")%>
                                        </td>
                                        <td class="text-center"><span class="badge badge-success">
                                            <%# Eval("PAIDSTATUS")%>
                                        </td>
                                        <td class="text-center">
                                            <%# Eval("REQ_RESCHEDULE_STATUS")%>
                                        </td>
                                        <td>
                                            <%# Eval("UA_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("CREATED_DATE")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <%--  </asp:Panel>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnHallReport" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="modal fade text-left" id="modal-custom-confirmation" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="modal-help-title">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <%-- <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>--%>
                    <strong class="modal-title" id="modal-help-title">Confirm</strong>
                </div>
                <!-- /.modal-header -->
                <div class="modal-body">
                    <p id="modal-help-text"></p>
                </div>
                <!-- /.modal-body -->
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-success">Yes</button>
<button type="button" class="btn btn-default" data-dismiss="modal">No</button>--%>

                    <button type="button" class="btn btn-success ok">Yes</button>
                    <button type="button" class="btn btn-default cancel">No</button>
                </div>
                <!-- /.modal-footer -->
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <script type="text/javascript">
        function Confirm(value, commandtype, id) {
            debugger;
            //var confirm_value = document.createElement("INPUT");
            //confirm_value.type = "hidden";
            //confirm_value.name = "confirm_value";
            var hdnField = $("#ctl00_ContentPlaceHolder1_ddlExamDate").val();
            var Capacity = $("#ctl00_ContentPlaceHolder1_hdnCapacity").val();
            var Total = $("#ctl00_ContentPlaceHolder1_hdnTotal").val();
            //var Total = $("#ctl00_ContentPlaceHolder1_hdnTotal").val();
            //var Total = 69;
            Total = Number(Total) + Number(value);
            if (hdnField != 0) {
                if (Number(Capacity) <= Number(Total)) {             
                    custom_confirm("Test Center Capacity is Exceeded! Do you still want to reschedule?", commandtype, id)
                }
                else {
                    $("#hdnStatus").val('CapacityNotFull').change();
                }
            }
            else {
                $("#hdnStatus").val('CapacityNotFull').change();
            }

        }

        function custom_confirm(message, commandtype, id) {
            debugger;
            //var status = '';
            //  put message into the body of modal
            $('#modal-custom-confirmation').on('show.bs.modal', function (event) {
                //  store current modal reference
                var modal = $(this);
                //  update modal body help text
                modal.find('.modal-body #modal-help-text').text(message);
            });

            //  show modal ringer custom confirmation
            $('#modal-custom-confirmation').modal('show');

            $('#modal-custom-confirmation button.ok').off().on('click', function () {
                // close window
                $('#modal-custom-confirmation').modal('hide');

                // and callback
                //callback(true);
            });

            $('#modal-custom-confirmation button.cancel').off().on('click', function () {
                // close window
                $('#modal-custom-confirmation').modal('hide');
                // callback
                //callback(false);
                if (commandtype == 1) {
                    $(".CheckAllSeats").each(function (index, value) {
                        var List = $(this).closest("table");
                        var td = $("td", $(this).closest("tr"));
                        var CheckBoxValue = $("[id*=cbHeadReg]").is(":checked");
                        document.getElementById("ctl00_ContentPlaceHolder1_LvReschedule_ctrl" + index + "_chkRegister").checked = false;
                        id.checked = false;
                    });

                }
                else {
                    if("ctl00_ContentPlaceHolder1_LvReschedule_cbHeadReg"==id.id)
                    {
                        var frm = document.forms[0]
                        for (i = 0; i < document.forms[0].elements.length; i++) {
                            var e = frm.elements[i];
                            var s = e.name.split("ctl00$ContentPlaceHolder1$LvReschedule$ctrl");
                            var b = 'ctl00$ContentPlaceHolder1$LvReschedule$ctrl';
                            var g = b + s[1];
                            if (e.name == g) {                              
                                e.checked = false;                                
                            }
                        }               
                    }
                    $('input[type="checkbox"]:checked').each(function () {
                        id.checked = false;
                    });

                }
            });
        }
    </script>


    <script type="text/javascript" language="javascript">
      

           
        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            var Count=0;
            var Capacity = $("#ctl00_ContentPlaceHolder1_hdnCapacity").val();
            var Total = $("#ctl00_ContentPlaceHolder1_hdnTotal").val();
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$LvReschedule$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$LvReschedule$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                    {
                        Count++;
                    }
                }
            }  

            Total = Number(Total) + Number(Count);   
            if (headchk.checked == true){            
                if (Number(Capacity) <= Number(Total)) {       
                    custom_confirm("Test Center Capacity is Exceeded! Do you still want to reschedule?", 2, headchk)                             
                } else{   $("#hdnStatus").val('CapacityNotFull').change();
                }
            }
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$LvReschedule$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$LvReschedule$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true){
                        e.checked = true;                        
                    }
                    else
                    {
                        e.checked = false;
                    }
                }
            }
        }
      
    </script>
    <script>

        function SelectSeperate(chkAccept) {
            var Counts = 0;
            var Total = 50;
            $('input[type="checkbox"]:checked').each(function () {
                Counts = Counts + 1;
                if (Counts > Total) {
                    chkAccept.checked = false;
                    var TotalSeat = Total - Counts;
                }
                Confirm(Counts, 2, chkAccept);
            });
        }
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            function SelectSeperate(chkAccept) {
                var Counts = 0;
                var Total = 50;
                $('input[type="checkbox"]:checked').each(function () {
                    Counts = Counts + 1;
                    if (Counts > Total) {
                        chkAccept.checked = false;
                        var TotalSeat = Total - Counts;
                    }
                    Confirm(Counts, 2, chkAccept);
                });
            }
        });
    </script>

    <script>
        function toggleSearch(searchBar, table) {
            var tableBody = table.querySelector('tbody');
            var allRows = tableBody.querySelectorAll('tr');
            var val = searchBar.value.toLowerCase();
            allRows.forEach((row, index) => {
                var insideSearch = row.innerHTML.trim().toLowerCase();
            //console.log('data',insideSearch.includes(val),'searchhere',insideSearch);
            if (insideSearch.includes(val)) {
                row.style.display = 'table-row';
            }
            else {
                row.style.display = 'none';
            }

        });
        }

        function test5() {
            var searchBar5 = document.querySelector('#FilterData');
            var table5 = document.querySelector('#MainLeadTable');

            //console.log(allRows);
            searchBar5.addEventListener('focusout', () => {
                toggleSearch(searchBar5, table5);
        });

        $(".saveAsExcel").click(function () {
            //let UserCall = prompt("Please Enter Password:");
            //var ExcelDetails = '<%=Session["ExcelDetails"] %>';
            //if (UserCall == null || UserCall == "") {
            //    return false;
            //} else {
            //    if(UserCall == ExcelDetails)
            //    {

            //    }
            //    else {
            //        alert('Password is not matched !!!')
            //        return false;
            //    }
            //}
            //if (confirm('Do You Want To Apply for New Program?') == true) {
            //    return false;
            //}
            var workbook = XLSX.utils.book_new();
            var allDataArray = [];
            allDataArray = makeTableArray(table5, allDataArray);
            var worksheet = XLSX.utils.aoa_to_sheet(allDataArray);
            workbook.SheetNames.push("Test");
            workbook.Sheets["Test"] = worksheet;
            XLSX.writeFile(workbook, "LeadData.xlsx");
        });
        }

        function makeTableArray(table, array) {
            var allTableRows = table.querySelectorAll('tr');
            allTableRows.forEach(row => {
                var rowArray = [];
            if (row.querySelector('td')) {
                var allTds = row.querySelectorAll('td');
                allTds.forEach(td => {
                    if (td.querySelector('span')) {
                        rowArray.push(td.querySelector('span').textContent);
            }
            else if (td.querySelector('input')) {
                rowArray.push(td.querySelector('input').value);
            }
            else if (td.querySelector('select')) {
                rowArray.push(td.querySelector('select').value);
            }
            else if (td.innerText) {
                rowArray.push(td.innerText);
            }
            else{
                rowArray.push('');
            }
        });
        }
        if (row.querySelector('th')) {
            var allThs = row.querySelectorAll('th');
            allThs.forEach(th => {
                if (th.textContent) {
                    rowArray.push(th.textContent);
        }
        else {
            rowArray.push('');
        }
        });
        }
        // console.log(allTds);

        array.push(rowArray);
        });
        return array;
        }

    </script>
</asp:Content>

