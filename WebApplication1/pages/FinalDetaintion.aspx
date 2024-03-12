<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="FinalDetaintion.aspx.cs" Inherits="ACADEMIC_FinalDetention" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style>
        .dynamic-nav-tabs li.active a {
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
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
        <script>
            $(document).ready(function () {
                var table = $('#table1').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false,
                    dom: 'lBfrtip',



                    //Export functionality
                    buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0, 3];
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
                                    var arr = [0, 3];
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
                                        else if ($(node).find("img").length > 0) {
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
                                    var arr = [0, 3];
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
                                        else if ($(node).find("img").length > 0) {
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
                            extend: 'pdfHtml5',
                            exportOptions: {
                                columns: function (idx, data, node) {
                                    var arr = [0, 3];
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
                                        else if ($(node).find("img").length > 0) {
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
                $('#ctl00_ContentPlaceHolder1_lvonlineadm_cbHead').on('click', function () {
                    // Get all rows with search applied
                    var rows = table.rows({ 'search': 'applied' }).nodes();
                    // Check/uncheck checkboxes for all rows in the table
                    $('input[type="checkbox"]', rows).prop('checked', this.checked);
                });







                // Handle click on checkbox to set state of "Select all" control
                $('#table1 tbody').on('change', 'input[type="checkbox"]', function () {
                    // If checkbox is not checked
                    if (!this.checked) {
                        var el = $('#ctl00_ContentPlaceHolder1_lvonlineadm_cbHead').get(0);
                        // If "Select all" control is checked and has 'indeterminate' property
                        if (el && el.checked && ('indeterminate' in el)) {
                            // Set visual state of "Select all" control
                            // as 'indeterminate'
                            el.indeterminate = true;
                        }
                    }
                });
            });
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $(document).ready(function () {
                    var table = $('#table1').DataTable({
                        responsive: true,
                        lengthChange: true,
                        scrollY: 320,
                        scrollX: true,
                        scrollCollapse: true,
                        paging: false,
                        dom: 'lBfrtip',



                        //Export functionality
                        buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0, 3];
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
                                        var arr = [0, 3];
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
                                            else if ($(node).find("img").length > 0) {
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
                                        var arr = [0, 3];
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
                                            else if ($(node).find("img").length > 0) {
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
                                extend: 'pdfHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0, 3];
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
                                            else if ($(node).find("img").length > 0) {
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
                    $('#ctl00_ContentPlaceHolder1_lvonlineadm_cbHead').on('click', function () {
                        // Get all rows with search applied
                        var rows = table.rows({ 'search': 'applied' }).nodes();
                        // Check/uncheck checkboxes for all rows in the table
                        $('input[type="checkbox"]', rows).prop('checked', this.checked);
                    });
                    // Handle click on checkbox to set state of "Select all" control
                    $('#table1 tbody').on('change', 'input[type="checkbox"]', function () {
                        // If checkbox is not checked
                        if (!this.checked) {
                            var el = $('#ctl00_ContentPlaceHolder1_lvonlineadm_cbHead').get(0);
                            // If "Select all" control is checked and has 'indeterminate' property
                            if (el && el.checked && ('indeterminate' in el)) {
                                // Set visual state of "Select all" control
                                // as 'indeterminate'
                                el.indeterminate = true;
                            }
                        }
                    });
                });
            });



    </script>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">

                    <div class="box-header with-border">
                        <%--<h3 class="box-title">FINAL DETENTION</h3>--%>
                        <h3 class="box-title">
                            <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                    </div>

                    <div class="box-body">
                        <div class="nav-tabs-custom mt-2" id="myTabContent">
                            <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                                <li class="nav-item active" runat="server" id="liTab1">
                                    <asp:LinkButton ID="lnkTab1" runat="server" OnClick="lnkTab1_Click" CssClass="nav-link" TabIndex="1">Module Suspension</asp:LinkButton>
                                    <%--<a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Internal Moderation</a>--%>
                                </li>

                                <li class="nav-item" runat="server" id="litTab3">
                                    <asp:LinkButton ID="lnkTab3" runat="server" OnClick="lnkTab3_Click" CssClass="nav-link" TabIndex="1">General Suspension</asp:LinkButton>
                                    <%--<a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">External Moderation</a>--%>
                                </li>
                                <li class="nav-item" runat="server" id="liTab2">
                                    <asp:LinkButton ID="lnkTab2" runat="server" OnClick="lnkTab2_Click" CssClass="nav-link" TabIndex="1">Remove Suspension</asp:LinkButton>
                                    <%--<a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">External Moderation</a>--%>
                                </li>

                            </ul>

                            <div class="tab-content">
                                <div class="tab-pane active" id="tab_1" runat="server" role="tabpanel">
                                    <div>

                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updeinternal"
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
                                    <asp:UpdatePanel ID="updeinternal" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true">Academic Semester</asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSession" AppendDataBoundItems="true" ValidationGroup="show" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                                    runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Report" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" TabIndex="1"
                                                                    ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname"
                                                                    Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="show">
                                                                </asp:RequiredFieldValidator>
                                                              <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlClgname"
                                                                    Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="Report">
                                                                </asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged"
                                                                    ValidationGroup="show" AutoPostBack="True" TabIndex="3" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSem"
                                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="show" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-9 col-md-6 col-12" style="padding-top: 25px;">
                                                                <asp:RadioButtonList ID="rblSelection" runat="server" AppendDataBoundItems="true" class="radiobuttonlist col-8" AutoPostBack="true"
                                                                    RepeatDirection="Horizontal" OnSelectedIndexChanged="rblSelection_SelectedIndexChanged">
                                                                    <asp:ListItem Value="1"><span style="font-size: 13px;font-weight:bold">Suspension in Perticular Module </span></asp:ListItem>
                                                                    <%--<asp:ListItem Value="2"><span style="font-size: 13px;font-weight:bold"> Suspension in all Modules</span></asp:ListItem>--%>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divCourse" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Course</label>--%>
                                                                    <asp:Label ID="lblICCourse" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"
                                                                    AutoPostBack="true" ValidationGroup="show" TabIndex="6">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" TabIndex="4" Text="Show Students"
                                                            ValidationGroup="show" CssClass="btn btn-outline-info btnX" />
                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                                            ValidationGroup="show" Visible="False" TabIndex="5" CssClass="btn btn-outline-info btnX" />
                                                        <asp:Button ID="btnReport" runat="server" Text="Final Suspension Report" ValidationGroup="Report"
                                                            TabIndex="6" CssClass="btn btn-outline-primary" OnClick="btnReport_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" TabIndex="7" CssClass="btn btn-outline-danger" />
                                                        <asp:ValidationSummary ID="vsStud" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                            ShowSummary="False" ValidationGroup="show" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                            ShowSummary="False" ValidationGroup="Report" />
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Label ID="lblMsg" runat="server" Visible="false" SkinID="lblmsg"></asp:Label>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:TextBox ID="txtAllSubjects" runat="server" Visible="false" CssClass="form-control" Enabled="false" Style="text-align: center;" Text="0" Width="30%"></asp:TextBox>
                                                        <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                                    </div>

                                                    <div class="col-12">
                                                        <asp:Panel ID="Panel3" runat="server" Visible="false">

                                                            <asp:ListView ID="lvDetend" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Suspension Student List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="table1">
                                                                        <thead class="bg-light-blue">
                                                                            <tr id="Tr1" runat="server">
                                                                                <th style="text-align: center">
                                                                                    <asp:CheckBox ID="chkbulk" runat="server" OnClick="checkAllCheckbox(this)" />
                                                                                </th>
                                                                                <th style="text-align: center; width: 15%">
                                                                                    <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true">Student ID</asp:Label>
                                                                                </th>
                                                                                <th style="text-align: center; width: 45%">Student Name </th>
                                                                                <th>Session</th>
                                                                                <th style="text-align: center; width: 45%">Suspension Reason </th>
                                                                                <th style="text-align: center; width: 15%">Final Suspension </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td style="text-align: center; color: green; width: 10%">
                                                                            <asp:CheckBox ID="chkAccept" ForeColor="Green" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                                            <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNO") %>' Visible="false" />
                                                                            <%--Enabled='<%#(Convert.ToInt32(Eval("FINAL_DETAIN"))==1 ? false : true)%>'--%>
                                                                        </td>
                                                                        <td style="text-align: center; width: 15%">
                                                                            <asp:Label ID="lblRegNo" runat="server" Text='<%# Eval("REGNO") %>' />
                                                                        </td>
                                                                        <td style="text-align: left; width: 45%">
                                                                            <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDNAME") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Txtsusnumber" runat="server" Text='<%# Eval("SUS_NUMBER") %>'></asp:TextBox>
                                                                        </td>
                                                                        <td style="text-align: left; width: 45%">
                                                                            <asp:DropDownList runat="server" ID="ddlReason" AppendDataBoundItems="true" data-select2-enable="true">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:Label ID="lblReason" runat="server" Text='<%# Eval("SUSPNO") %>' Visible="false" />
                                                                        </td>
                                                                        <td style="text-align: center; width: 15%">
                                                                            <asp:Label ID="lblFinal" runat="server" Text='<%# Eval("FINAL") %>'> </asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr>
                                                                        <td style="text-align: center; color: green; width: 10%">
                                                                            <asp:CheckBox ID="chkAccept" runat="server" ForeColor="Green" ToolTip='<%# Eval("IDNO") %>' />
                                                                            <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNO") %>' Visible="false" />
                                                                            <%--Enabled='<%#(Convert.ToInt32(Eval("FINAL_DETAIN"))==1 ? false : true)%>'--%>
                                                                        </td>
                                                                        <td style="text-align: center; width: 15%">
                                                                            <asp:Label ID="lblRegNo" runat="server" Text='<%# Eval("REGNO") %>' />
                                                                        </td>
                                                                        <td style="text-align: left; width: 45%">
                                                                            <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDNAME") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Txtsusnumber" runat="server" Text='<%# Eval("SUS_NUMBER") %>'></asp:TextBox></td>
                                                                        <td style="text-align: left; width: 45%">
                                                                            <asp:DropDownList runat="server" ID="ddlReason" AppendDataBoundItems="true" data-select2-enable="true">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:Label ID="lblReason" runat="server" Text='<%# Eval("SUSPNO") %>' Visible="false" />
                                                                        </td>
                                                                        <%-- <td>
                                                    <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("COURSE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                </td>--%>
                                                                        <%-- <td style="text-align: center; width: 15%">
                                                    <asp:Label ID="lblProv" runat="server" Text='<%# Eval("PROV") %>' ToolTip='<%# Eval("FINAL_DETAIN")%>' />
                                                </td>--%>
                                                                        <td style="text-align: center; width: 15%">
                                                                            <asp:Label ID="lblFinal" runat="server" Text='<%# Eval("FINAL") %>'> </asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
                                                        <div id="divMsg" runat="server">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnReport" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div id="tab_2" runat="server" visible="false" role="tabpanel">
                                    <div>

                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updeinternal"
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
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="row">

                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="Label1" runat="server" Font-Bold="true">Academic Semester</asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlsessionCa" AppendDataBoundItems="true" ValidationGroup="show" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                                    runat="server">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlsessionCa"
                                                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlsessionCa"
                                                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Report" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="Label2" runat="server" Font-Bold="true">Student ID</asp:Label>
                                                                </div>
                                                                <asp:TextBox runat="server" ID="txtSearchStudent" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSearchStudent"
                                                                    Display="None" ErrorMessage="Please Enter Student ID" InitialValue="" ValidationGroup="show" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label></label>
                                                                </div>
                                                                <asp:Button ID="btnSearchStudent" runat="server" OnClick="btnSearchStudent_Click" ValidationGroup="show" TabIndex="4" Text="Search"
                                                                    CssClass="btn btn-outline-info btnX" />
                                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                                    ShowSummary="False" ValidationGroup="show" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12" runat="server" visible="false" id="DivDetails">
                                                        <div class="row">
                                                            <div class="col-lg-4 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Student ID :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblStudent_id" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Student Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblStudentn" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                            <div class="col-lg-4 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Faculty :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblFacultyname" runat="server" Text="" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Program :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblPrograms" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                            <div class="col-lg-4 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Current Semester :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblCurrentSemester" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <asp:ListView ID="lvSuspensionData" runat="server" Visible="true">
                                                                <LayoutTemplate>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Select</th>
                                                                                <th>Module</th>
                                                                                <th>Semester</th>
                                                                                <th>Suspension Reason</th>
                                                                                <th>Suspension By</th>
                                                                                <th>Remark/Details</th>

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
                                                                            <asp:CheckBox ID="chkEx" runat="server" />
                                                                            <asp:HiddenField runat="server" ID="hdnIdno" Value='<%#Eval("IDNO") %>' />
                                                                        </td>
                                                                        <td><%# Eval("MODULE") %>
                                                                            <asp:Label ID="lblCourseNo" runat="server" Text='<%# Eval("COURSENO") %>' Visible="false"></asp:Label>

                                                                        </td>
                                                                        <td>
                                                                            <%#Eval("SEMESTERNAME") %>
                                                                            <asp:Label ID="lbSemesterNo" runat="server" Text='<%#Eval("SEMESTERNO") %>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblDetain" runat="server" Text='<%#Eval("DETAIND") %>' Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="Label4" Text='<%#Eval("SUSPENSION_NAME") %>'></asp:Label>

                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="DueDate" Text='<%#Eval("UA_FULLNAME") %>'></asp:Label>

                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtDetails" runat="server" Visible="true" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                                        </td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>

                                                        </asp:Panel>
                                                    </div>
                                                    <div class="col-12 btn-footer" runat="server">
                                                        <asp:Button ID="btnRemoveSus" runat="server" OnClick="btnRemoveSus_Click" TabIndex="4" Text="Remove"
                                                            ValidationGroup="show" CssClass="btn btn-outline-danger" Visible="false" />
                                                        <asp:Button ID="btnCancelSus" runat="server" Text="Cancel" OnClick="btnCancelSus_Click"
                                                            TabIndex="5" CssClass="btn btn-outline-danger" Visible="false" />
                                                    </div>

                                                </div>
                                            </div>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnRemoveSus" />
                                            <asp:PostBackTrigger ControlID="btnCancelSus" />
                                            <asp:PostBackTrigger ControlID="btnSearchStudent" />
                                        </Triggers>

                                    </asp:UpdatePanel>
                                </div>
                                <div id="tab_3" runat="server" visible="false" role="tabpanel">
                                    <div>

                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updfinal"
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
                                    <asp:UpdatePanel ID="updfinal" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12 mt-3">
                                                <div class="row">

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lbl" runat="server" Font-Bold="true">Academic Semester</asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlsessionfinal" AppendDataBoundItems="true" ValidationGroup="show" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                            runat="server">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlsessionfinal"
                                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="Label5" runat="server" Font-Bold="true">Student ID</asp:Label>
                                                        </div>
                                                        <asp:TextBox runat="server" ID="txtstud" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtstud"
                                                            Display="None" ErrorMessage="Please Enter Student ID" InitialValue="" ValidationGroup="show" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label></label>
                                                        </div>

                                                        <asp:Button ID="btnserch" runat="server" OnClick="btnserch_Click" ValidationGroup="show" TabIndex="4" Text="Search"
                                                            CssClass="btn btn-outline-info btnX" />
                                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                            ShowSummary="False" ValidationGroup="show" />
                                                    </div>
                                                </div>
                                                <div class="row" runat="server" visible="false" id="Div2">
                                                    <div class="col-lg-4 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Student ID :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblfinastud" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Student Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblfinalname" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-4 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Faculty :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblfinalfaculty" runat="server" Text="" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Program :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblfinalpgm" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-4 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Current Semester :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblcurrentsem" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <div id="details" runat="server" visible="false">
                                                    <div class="row mt-3">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Suspend Date</label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i id="txtFromDate1" runat="server" class="fa fa-calendar"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtFromDate" runat="server" TabIndex="8" ValidationGroup="teacherallot"
                                                                    CssClass="form-control" />
                                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                                                    PopupButtonID="txtFromDate1" />
                                                                <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                                    Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                                    MaskType="Date" />
                                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" EmptyValueMessage="Please Enter To Date"
                                                                    ControlExtender="meFromDate" ControlToValidate="txtFromDate" IsValidEmpty="false"
                                                                    InvalidValueMessage="Date is invalid" Display="None" ErrorMessage="Please Enter From Date"
                                                                    InvalidValueBlurredMessage="*" ValidationGroup="teacherallot" SetFocusOnError="true" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtFromDate"
                                                                    Display="None" ErrorMessage="Please Select Suspend Date" InitialValue="" ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tdToDate" runat="server" visible="true">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>To Date</label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i id="txtToDate1" runat="server" class="fa fa-calendar"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtToDate" runat="server" TabIndex="9" ValidationGroup="teacherallot"
                                                                    CssClass="form-control" />
                                                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                                    PopupButtonID="txtToDate1" />
                                                                <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                                    Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                                    MaskType="Date" />
                                                                <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" EmptyValueMessage="Please Enter To Date"
                                                                    ControlExtender="meToDate" ControlToValidate="txtToDate" IsValidEmpty="false"
                                                                    InvalidValueMessage="Date is invalid" Display="None" ErrorMessage="Please Enter To Date"
                                                                    InvalidValueBlurredMessage="*" ValidationGroup="teacherallot" SetFocusOnError="true" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtToDate"
                                                                    Display="None" ErrorMessage="Please Select To Date" InitialValue="" ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Suspention </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddltype" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                                TabIndex="5" ToolTip="Please Select Suspension">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddltype"
                                                                Display="None" ErrorMessage="Please Select Suspension" InitialValue="0" SetFocusOnError="True"
                                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Suspend Reason</label>
                                                            </div>
                                                            <asp:TextBox ID="txtEventDetail" runat="server" CssClass="form-control" Style="resize: none" TabIndex="10" TextMode="MultiLine">
                                                            </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtEventDetail" runat="server"
                                                                ControlToValidate="txtEventDetail" Display="None"
                                                                ErrorMessage="Please Enter Reason" SetFocusOnError="true"
                                                                ValidationGroup="submit" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="Div3" class="col-12 btn-footer" runat="server">

                                                    <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                        ShowSummary="False" ValidationGroup="submit" />
                                                    <asp:Button ID="btnsubmitgeneral" runat="server" TabIndex="4" Text="Submit" OnClick="btnsubmitgeneral_Click"
                                                        ValidationGroup="submit" CssClass="btn btn-outline-info" Visible="false" />
                                                    <asp:Button ID="btncancelfinal" runat="server" Text="Cancel" OnClick="btncancelfinal_Click"
                                                        TabIndex="5" CssClass="btn btn-outline-danger" Visible="false" />
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:Panel ID="Panel2" runat="server" Visible="false">
                                                        <asp:ListView ID="lvsuspention" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>General Suspenstion List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" id="mytable" style="width: 100%">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th style="text-align: center">SrNo </th>
                                                                            <th style="text-align: center">Student ID </th>
                                                                            <th style="text-align: center">Name</th>
                                                                            <th style="text-align: center">Session Name </th>
                                                                            <th style="text-align: center">Suspention From Date</th>
                                                                            <th style="text-align: center">Suspention To Date</th>
                                                                            <th style="text-align: center">Suspention Reason</th>
                                                                            <th style="text-align: center">Suspention Type</th>
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
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                              
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <%# Eval("REGNO")%> 
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <%# Eval("NAME_WITH_INITIAL")%>
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <%# Eval("SESSION_NAME")%>
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <%# Eval("SUSPENSTION_DATE")%>
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <%# Eval("TODATE")%>
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <%# Eval("REMARKS")%>
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <%# Eval("SUSPENSION_NAME")%>
                                                                    </td>

                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>


                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnsubmitgeneral" />
                                            <asp:PostBackTrigger ControlID="btncancelfinal" />
                                            <asp:PostBackTrigger ControlID="btnserch" />
                                        </Triggers>

                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" language="javascript">

        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvDetend$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvDetend$ctrl';
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
