<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Scholarship_And_Payment.aspx.cs" Inherits="EXAMINATION_Projects_Scholarship_And_Payment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

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

        .lbl-green {
            color: green;
        }

        .lbl-red {
            color: red;
        }

        td .fa-eye {
            font-size: 18px;
            color: #0d70fd;
        }

        .fa-plus {
            color: #17a2b8;
            border: 1px solid #17a2b8;
            border-radius: 50%;
            padding: 6px 8px;
        }

        .date-details .form-control {
            width: 70px !important;
            padding: .375rem .1rem !important;
        }
    </style>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#mytable').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 450,
                scrollX: true,
                scrollCollapse: true,
                paging : false,
                sorting : false,

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
                                            var arr = [0, 9];
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
                                            var arr = [0, 9];
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
                                            var arr = [0, 9];
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
                    sorting : false,

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
                                                var arr = [0, 9];
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
                                                var arr = [0, 9];
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
                                                var arr = [0, 9];
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

    <script>
        $(document).ready(function () {
            var table = $('#TblInstallment').DataTable({
                responsive: true,
                lengthChange: true,
                //scrollY: 450,
                //scrollX: true,
                //scrollCollapse: true,
                //padding: false,
                //paging: false,

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
                                return $('#TblInstallment').DataTable().column(idx).visible();
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
                                                return $('#TblInstallment').DataTable().column(idx).visible();
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
                                                return $('#TblInstallment').DataTable().column(idx).visible();
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
                                                return $('#TblInstallment').DataTable().column(idx).visible();
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
                var table = $('#TblInstallment').DataTable({
                    responsive: true,
                    lengthChange: true,
                    //scrollY: 450,
                    //scrollX: true,
                    //scrollCollapse: true,
                    //paging: false,
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
                                    return $('#TblInstallment').DataTable().column(idx).visible();
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
                                                    return $('#TblInstallment').DataTable().column(idx).visible();
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
                                                    return $('#TblInstallment').DataTable().column(idx).visible();
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
                                                    return $('#TblInstallment').DataTable().column(idx).visible();
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



    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><span>Scholarship And Payment</span></h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item d-none">
                                <a class="nav-link" data-toggle="tab" href="#tab_1" tabindex="0">Installment </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_2" tabindex="1">Discount </a>
                            </li>
                            <li class="nav-item" runat="server" visible="false">
                                <a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="2">Scholarship Rule</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_4" tabindex="3">Scholarship </a>
                            </li>
                        </ul>

                        <div class="tab-content">
                            <div class="tab-pane d-none" id="tab_1">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updInstall"
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
                                <asp:UpdatePanel ID="updInstall" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">

                                                <div class="form-group col-lg-2 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblRegno" runat="server" Font-Bold="true">Student Reg.No.</asp:Label>

                                                    </div>
                                                    <asp:TextBox ID="txtStudentReg" runat="server" CssClass="form-control" TabIndex="4" />
                                                </div>
                                                <div class="form-group col-lg-1 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label></label>
                                                    </div>
                                                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-outline-info" OnClick="btnSearch_Click" TabIndex="5">Search</asp:LinkButton>
                                                </div>

                                                <div id="StudentInf" class="col-lg-5 col-md-6 col-12 " runat="server" visible="false">
                                                    <ul class="list-group list-group-unbordered" id="Student">
                                                        <li class="list-group-item" visible="false"><b>Student Reg.No :</b>
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
                                                <div class="col-lg-4 col-md-6 col-12" runat="server" visible="false" id="Faculty">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Faculty/school Name :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblFacultyname" runat="server" Text="" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                                        </li>
                                                         <li class="list-group-item"><b>Program :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblinstallprogram" runat="server" Text="" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                
                                            </div>
                                        </div>


                                        <div class="col-12" runat="server" visible="false" id="Amount">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Semester</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="6" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlSemester"
                                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0"
                                                        ValidationGroup="submitapti" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Total Applicable Fees</label>
                                                    </div>
                                                    <asp:TextBox ID="txtTotalApplicableFees" runat="server" CssClass="form-control" Text="" Enabled="false" TabIndex="7" ValidationGroup="Installments" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtTotalApplicableFees"
                                                        Display="None" ErrorMessage="Please Insert Total Applicable Fees" ValidationGroup="submitapti" />
                                                </div>
                                            </div>
                                        </div>

                                        <%--<div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-2 col-md-2 col-6">
                                                    <div class="label-dynamic">
                                                        <label>Installment</label>
                                                    </div>
                                                    <asp:TextBox ID="txtfirstInstallment" runat="server" CssClass="form-control" Text="1" Enabled="false" TabIndex="8" />
                                                </div>
                                                <div class="form-group col-lg-2 col-md-2 col-6">
                                                    <div class="label-dynamic">
                                                        <label>Amount</label>
                                                    </div>
                                                    <asp:TextBox ID="txtfirstAmount" runat="server" CssClass="form-control" Text="" onblur="return IsNumeric(this);" TabIndex="9" ValidationGroup="Installments" />
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtfirstAmount"
                                                        Display="None" ErrorMessage="Please Enter First Amount" 
                                                        ValidationGroup="submitapti" />
                                                </div>
                                                <div class="form-group col-lg-2 col-md-2 col-6">
                                                    <div class="label-dynamic">
                                                        <label>Due Date</label>
                                                    </div>
                                                    <asp:TextBox ID="txtfirstDueDate" runat="server" CssClass="form-control" Text=""  type="date" TabIndex="10" ValidationGroup="Installments" />
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtfirstDueDate"
                                                        Display="None" ErrorMessage="Please Enter First Due Date" 
                                                        ValidationGroup="submitapti"/>

                                                </div>
                                                <div class="form-group col-lg-2 col-md-2 col-6">
                                                    <div class="label-dynamic">
                                                        <label>Additional Charge</label>
                                                    </div>
                                                    <asp:TextBox ID="txtfirstAdditionalCharge" runat="server" onblur="return IsNumeric(this);" CssClass="form-control" Text="" TabIndex="11" />
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtfirstAdditionalCharge"
                                                        Display="None" ErrorMessage="Please Enter First Additional Charge"
                                                       ValidationGroup="submitapti" />

                                                </div>
                                                <div class="form-group col-lg-2 col-md-2 col-6">
                                                    <div class="label-dynamic">
                                                        <label></label>
                                                    </div>

                                            
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-lg-2 col-md-2 col-6">
                                                    <div class="label-dynamic">
                                                        <label>Installment</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSecondInstallment" runat="server" CssClass="form-control" Text="2" Enabled="false" TabIndex="12" />
                                                </div>
                                                <div class="form-group col-lg-2 col-md-2 col-6">
                                                    <div class="label-dynamic">
                                                        <label>Amount</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSecondAmount" runat="server" CssClass="form-control" Text="" onblur="return IsNumeric(this);" TabIndex="13" />
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtSecondAmount"
                                                        Display="None" ErrorMessage="Please Enter Second Amount" 
                                                        ValidationGroup="submitapti" />
                                                </div>
                                                <div class="form-group col-lg-2 col-md-2 col-6">
                                                    <div class="label-dynamic">
                                                        <label>Due Date</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSecondduedate" runat="server" CssClass="form-control" Text=""  type="date" TabIndex="14" />
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtSecondduedate"
                                                        Display="None" ErrorMessage="Please Enter Second Due Date" 
                                                       ValidationGroup="submitapti" />
                                                </div>
                                                <div class="form-group col-lg-2 col-md-2 col-6">
                                                    <div class="label-dynamic">
                                                        <label>Additional Charge</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSecondAdditionalCharge" runat="server" CssClass="form-control" Text="" onblur="return IsNumeric(this);" TabIndex="15" />
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtSecondAdditionalCharge"
                                                        Display="None" ErrorMessage="Please Enter Second Additional Charge" 
                                                        ValidationGroup="submitapti" />
                                                </div>
                                                <div class="form-group col-lg-2 col-md-2 col-6">
                                                    <div class="label-dynamic">
                                                        <label></label>
                                                    </div>
                                                    
                                                </div>
                                            </div>
                                        </div>--%>
                                        <div class="col-md-12" runat="server" visible="false" id="Installement">
                                            <asp:Panel ID="pnlStudinstalment" runat="server" Visible="true">
                                                <asp:GridView ID="grdinstalment" runat="server" AutoGenerateColumns="False"
                                                    ShowFooter="True" CssClass="table table-hovered table-bordered">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr No." Visible="false">
                                                            <ItemTemplate>
                                                                <%--<%# Container.DataItemIndex + 1 %>--%>
                                                                <asp:Label runat="server" ID="LblSrno" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblPersent" Text='<%# Bind("Persentage") %>' Visible="false"></asp:Label>
                                                                RECON_STATUS
                                                                    <asp:Label runat="server" ID="lblRecon" Text='<%# Bind("RECON_STATUS") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                            </EditItemTemplate>
                                                            <ControlStyle Width="20px"></ControlStyle>
                                                            <ItemStyle Width="20px"></ItemStyle>

                                                            <FooterStyle />
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="INSTALMENT_NO" HeaderText="Installment" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle Width="30px" Font-Bold="true" />
                                                        </asp:BoundField>



                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>

                                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" Text='<%# Bind("INSTALL_AMOUNT") %>' TabIndex="7" ValidationGroup="Installments" Enabled="true" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAmount"
                                                                    Display="None" ErrorMessage="Please Enter First Amount"
                                                                    ValidationGroup="submitapti" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="fteValue" runat="server"
                                                                    FilterType="Custom" FilterMode="ValidChars" ValidChars="0.123456789" TargetControlID="txtAmount">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Amount."
                                                                    ControlToValidate="txtAmount" Display="None" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                            </EditItemTemplate>
                                                            <ControlStyle Width="80px"></ControlStyle>
                                                            <ItemStyle Width="80px"></ItemStyle>

                                                            <FooterStyle />

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Due Date">
                                                            <ItemTemplate>
                                                                <div class="input-group date-details">
                                                                    <div class="input-group-addon" id="ToDate" runat="server">
                                                                        <i class="fa fa-calendar" runat="server" id="Cal"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtDueDate" runat="server" class="form-control" TabIndex="8" Enabled="true" Style="width: 70px;"
                                                                        ondrop="return false;" placeholder="Due Date" onpaste="return false;" Text='<%# Bind("DUE_DATE") %>' onkeypress="return RestrictCommaSemicolon(event);" onkeyup="ConvertEachFirstLetterToUpper(this.id)">
                                                                    </asp:TextBox>
                                                                    <ajaxToolKit:CalendarExtender ID="meetodate" runat="server" Format="dd/MM/yyyy"
                                                                        TargetControlID="txtDueDate" PopupButtonID="Cal" Enabled="True">
                                                                    </ajaxToolKit:CalendarExtender>
                                                                    <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtDueDate" />
                                                                    <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" ControlExtender="meToDate"
                                                                        ControlToValidate="txtDueDate" Display="None" EmptyValueMessage="Please Enter Due Date"
                                                                        ErrorMessage="Please Enter Due Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                        IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                                                    <ajaxToolKit:MaskedEditValidator ID="rfvMonth" runat="server" ControlExtender="meToDate"
                                                                        ControlToValidate="txtDueDate" Display="None" EmptyValueMessage="Please Enter Due Date"
                                                                        ErrorMessage="Please Enter Due Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                        IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Daywise" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldVali4" runat="server" ErrorMessage="Please Enter DueDate."
                                                                        ControlToValidate="txtDueDate" Display="None" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                                </div>

                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                            </EditItemTemplate>
                                                            <ControlStyle Width="120px"></ControlStyle>
                                                            <ItemStyle Width="120px"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type">
                                                            <ItemTemplate>
                                                                <%-- <asp:RadioButtonList ID="rdbFilter" runat="server"  RepeatDirection="Horizontal" AutoPostBack="true"  OnSelectedIndexChanged="rdbFilter_SelectedIndexChanged" Visible="true">
                                                                <asp:ListItem Value="1">Percentage</asp:ListItem>
                                                                <asp:ListItem Value="2">Waived off</asp:ListItem>
                                                                         </asp:RadioButtonList>--%>
                                                                <asp:DropDownList ID="ddExtraDiscounty" runat="server" CssClass="form-control" Text='<%# Bind("ExtraDiscount") %>' data-select2-enable="true" TabIndex="9" AppendDataBoundItems="true" Visible="true" Enabled="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Discount</asp:ListItem>
                                                                    <asp:ListItem Value="2">Extra Charge</asp:ListItem>

                                                                </asp:DropDownList>

                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                            </EditItemTemplate>
                                                            <ControlStyle Width="120px"></ControlStyle>
                                                            <ItemStyle Width="120px"></ItemStyle>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Percentage">
                                                            <ItemTemplate>
                                                                <%-- <asp:RadioButtonList ID="rdbFilter" runat="server"  RepeatDirection="Horizontal" AutoPostBack="true"  OnSelectedIndexChanged="rdbFilter_SelectedIndexChanged" Visible="true">
                                                                <asp:ListItem Value="1">Percentage</asp:ListItem>
                                                                <asp:ListItem Value="2">Waived off</asp:ListItem>
                                                                         </asp:RadioButtonList>--%>
                                                                <asp:DropDownList ID="ddPercent" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddPercent_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true" TabIndex="9" AppendDataBoundItems="true" Visible="true" Enabled="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <%-- <asp:ListItem Value="1">5</asp:ListItem>
                                                                       <asp:ListItem Value="2">10</asp:ListItem>--%>
                                                                </asp:DropDownList>

                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                            </EditItemTemplate>
                                                            <ControlStyle Width="120px"></ControlStyle>
                                                            <ItemStyle Width="120px"></ItemStyle>

                                                        </asp:TemplateField>

                                                        <%--<asp:RadioButtonList ID="rdbFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdbFilter_SelectedIndexChanged" RepeatColumns="8" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="1"><span style="padding-left:5px">Download For Test Prep Data </span></asp:ListItem>
                                                                <asp:ListItem Value="2"><span style="padding-left:5px">Exam Marks Upload From Test Prep </span></asp:ListItem>--%>

                                                        <asp:TemplateField HeaderText="Extra/ Discount Amount">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAdditionalCharge" runat="server" Text='<%# Bind("ADDITIONAL") %>' CssClass="form-control" TabIndex="10" ValidationGroup="Installments" Enabled="true" />

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValis" runat="server" ControlToValidate="txtAdditionalCharge"
                                                                    Display="None" ErrorMessage="Please Enter Extra / Discount Amount"
                                                                    ValidationGroup="submitapti" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="fteValues" runat="server"
                                                                    FilterType="Custom" FilterMode="ValidChars" ValidChars="0.123456789" TargetControlID="txtAdditionalCharge">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldVali" runat="server" ErrorMessage="Please Enter Extra / Discount Amount."
                                                                    ControlToValidate="txtAdditionalCharge" Display="None" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>

                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                            </EditItemTemplate>
                                                            <ControlStyle Width="100px"></ControlStyle>
                                                            <ItemStyle Width="100px"></ItemStyle>
                                                            <%-- <FooterTemplate>
                                                                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                                                                        Text="Add New Installment" CssClass="btn btn-outline-info" />
                                                                </FooterTemplate>--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Payable">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtTotalPayble" runat="server" Text='<%# Bind("TOTAL_PAYABLE") %>' CssClass="form-control" TabIndex="10" ValidationGroup="Installments" Enabled="true" />

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidateTotal" runat="server" ControlToValidate="txtTotalPayble"
                                                                    Display="None" ErrorMessage="Please Enter Total Payble"
                                                                    ValidationGroup="submitapti" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="fteValueTotal" runat="server"
                                                                    FilterType="Custom" FilterMode="ValidChars" ValidChars="0.123456789" TargetControlID="txtTotalPayble">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidateTotals" runat="server" ErrorMessage="Please Enter Total Payble."
                                                                    ControlToValidate="txtTotalPayble" Display="None" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>

                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                            </EditItemTemplate>
                                                            <ControlStyle Width="100px"></ControlStyle>
                                                            <ItemStyle Width="100px"></ItemStyle>
                                                            <%-- <FooterTemplate>
                                                                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                                                                        Text="Add New Installment" CssClass="btn btn-outline-info" />
                                                                </FooterTemplate>--%>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Waived Off">
                                                            <ItemTemplate>
                                                                <asp:RadioButtonList ID="rdbWaivedOff" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" Visible="true" OnSelectedIndexChanged="rdbWaivedOff_SelectedIndexChanged" Enabled="true">

                                                                    <asp:ListItem Value="1">Waived off</asp:ListItem>
                                                                </asp:RadioButtonList>

                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                            </EditItemTemplate>
                                                            <ControlStyle Width="95px"></ControlStyle>
                                                            <ItemStyle Width="95px"></ItemStyle>
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" TabIndex="11"
                                                                    Text="Add" CssClass="btn btn-outline-info" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="bg-light-blue" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnSubmitInstallment" runat="server" CssClass="btn btn-outline-info" ValidationGroup="submitapti" OnClick="btnSubmitInstallment_Click" TabIndex="12">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelInstallment" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelInstallment_Click" TabIndex="13">Cancel</asp:LinkButton>
                                            <asp:ValidationSummary ID="vsshow3a" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submitapti" />
                                        </div>
                                        <div class="col-md-12">
                                            <div id="divapti" runat="server">
                                                <asp:Panel ID="Panel5" runat="server">
                                                    <asp:ListView ID="lvinstall" runat="server">
                                                        <LayoutTemplate>
                                                            <div>
                                                                <div class="sub-heading">
                                                                    <h5></h5>
                                                                </div>

                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="TblInstallment">
                                                                    <thead class="bg-light-blue">
                                                                        <tr id="trRow">
                                                                            <th>Edit</th>
                                                                            <th>Student ID</th>
                                                                            <th>Student Name</th>
                                                                            <th>Program</th>
                                                                            <th>Semester</th>
                                                                            <th>Request Date</th>
                                                                            <th>Details</th>
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
                                                                <td class="text-center">
                                                                    <asp:ImageButton ID="btnEditInstallMent" runat="server" ImageUrl="~/IMAGES/edit.png" CommandArgument='<%# Eval("IDNO") %>'
                                                                        AlternateText="Edit Record " OnClick="btnEditInstallMent_Click"
                                                                        ToolTip="Edit Record" />
                                                                    <asp:HiddenField ID="hdnSemester" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblenroll" Text='<%# Eval("ENROLLNMENTNO") %>'> </asp:Label></td>
                                                                <td><%# Eval("NAME_WITH_INITIAL") %></td>
                                                                <td><%# Eval("DEGREENAME") %></td>
                                                                <td><%# Eval("SEMESTERNAME") %></td>
                                                                <td><%# Eval("DATA_INSERT_DATEs") %></td>
                                                                <td class="text-center">
                                                                    <asp:LinkButton ID="lnkView" runat="server" CssClass="btn btn-outline-info"
                                                                        CommandArgument='<%# Eval("IDNO") %>' OnClick="lnkView_Click"><i class="fa fa-eye"></i> View</asp:LinkButton></td>
                                                                <%--<td class="text-center"><i class="fa fa-eye" aria-hidden="true" data-toggle="modal" data-target="#Installment_Veiw"></i></td>--%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>

                                        </div>


                                        <!-- View Modal -->
                                        <div class="modal" id="Installment_Veiw">
                                            <div class="modal-dialog modal-lg">
                                                <div class="modal-content">

                                                    <!-- Modal Header -->
                                                    <div class="modal-header">
                                                        <h4 class="modal-title">View Details</h4>
                                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                    </div>

                                                    <!-- Modal body -->
                                                    <div class="modal-body pl-0 pr-0">
                                                        <div class="col-12 mb-3">
                                                            <div class="row">
                                                                <div class="col-lg-7 col-md-6 col-12">
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
                                                                        <li class="list-group-item"><b>Program :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblProgram" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                                <div class="col-lg-5 col-md-6 col-12">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Faculty :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblfaculty" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Current Semester :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblCurrentSemester" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="col-md-12">
                                                            <div id="div4" runat="server">
                                                                <asp:Panel ID="Panel4" runat="server">
                                                                    <asp:ListView ID="lvInstallmentList" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div>
                                                                                <div class="sub-heading">
                                                                                    <h5></h5>
                                                                                </div>

                                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr id="trRow">
                                                                                            <th>Installment</th>
                                                                                            <th>Amount</th>
                                                                                            <th>DueDate</th>
                                                                                            <th>ExtraCharge</th>
                                                                                            <th>Status</th>
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

                                                                                <td><a class="sub-label">
                                                                                    <asp:Label ID="lblInstal" runat="server" Text='<%#Eval("INSTALMENT_NO") %>' Font-Bold="true"></asp:Label></a></td>
                                                                                <td><a class="sub-label">
                                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("INSTALL_AMOUNT") %>' Font-Bold="true"></asp:Label></a></td>
                                                                                <td>
                                                                                    <a class="sub-label">
                                                                                        <asp:Label ID="lblDueDate" runat="server" Text='<%#Eval("Date") %>' Font-Bold="true"></asp:Label></a>

                                                                                </td>
                                                                                <td><a class="sub-label">
                                                                                    <asp:Label ID="lblExtraCharge" runat="server" Text='<%#Eval("ADDITIONAL_CHARGE_AMOUNT") %>' Font-Bold="true"></asp:Label></a></td>
                                                                                <td class="text-center"><a class="sub-label">
                                                                                    <asp:Label ID="lblPayment" runat="server" Text='<%#Eval("INSTALL_NO")%>' class="badge"></asp:Label></a></td>

                                                                            </tr>

                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>

                                                        </div>


                                                        <%--<div class="col-12">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Installment</th>
                                                                        <th>Amount</th>
                                                                        <th>DueDate</th>
                                                                        <th>ExtraCharge</th>
                                                                        <th>Status</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td><a class="sub-label">
                                                                            <asp:Label ID="Label2" runat="server" Text="1" Font-Bold="true"></asp:Label></a></td>
                                                                        <td><a class="sub-label">
                                                                            <asp:Label ID="lblAmount" runat="server" Text="" Font-Bold="true"></asp:Label></a></td>
                                                                        <td>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblDueDate" runat="server" Text="-" Font-Bold="true"></asp:Label></a>

                                                                        </td>
                                                                        <td><a class="sub-label">
                                                                            <asp:Label ID="lblExtraCharge" runat="server" Text="0" Font-Bold="true"></asp:Label></a></td>
                                                                        <td class="text-center"><a class="sub-label">
                                                                            <asp:Label ID="lblPayment" runat="server" class="badge badge-success"></asp:Label></a></td>
                                                                      
                                                                    </tr>
                                                                    <tr>
                                                                        <td><a class="sub-label">
                                                                            <asp:Label ID="Label1" runat="server" Text="2" Font-Bold="true"></asp:Label></a></td>
                                                                        <td><a class="sub-label">
                                                                            <asp:Label ID="lblAmount2" runat="server" Text="" Font-Bold="true"></asp:Label></a></td>
                                                                        <td>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblDueDate2" runat="server" Text="-" Font-Bold="true"></asp:Label></a>

                                                                        </td>
                                                                        <td><a class="sub-label">
                                                                            <asp:Label ID="lblExtraCharge2" runat="server" Text="0" Font-Bold="true"></asp:Label></a></td>
                                                                        <td class="text-center"><a class="sub-label">
                                                                            <asp:Label ID="lblPayment2" runat="server" class="badge badge-success"></asp:Label></a></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSearch" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane active" id="tab_2">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Concession"
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
                                <asp:UpdatePanel ID="Concession" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Faculty</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        AppendDataBoundItems="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="true" TabIndex="18">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlFaculty"
                                                        Display="None" ErrorMessage="Please Select Faculty" InitialValue="0" ValidationGroup="Concession" />

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Study Level</label>
                                                    </div>
                                                    <asp:ListBox ID="lstbxStudyLevel" AppendDataBoundItems="True" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" TabIndex="19"></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="lstbxStudyLevel"
                                                        Display="None" ErrorMessage="Please Select Study Level" InitialValue="" ValidationGroup="Concession" />
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Program</label>
                                                    </div>
                                                    <asp:ListBox ID="lstbxProgram" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" SelectionMode="Multiple" TabIndex="19"></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="lstbxProgram"
                                                        Display="None" ErrorMessage="Please Select Program" InitialValue="" ValidationGroup="Concession" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Semester</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSemesterConcession" runat="server" ClientIDMode="Static" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="20">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSemesterConcession"
                                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Concession" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label></label>
                                                    </div>
                                                    <asp:RadioButtonList ID="rblSelection" runat="server" TabIndex="1" Visible="false"
                                                        RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rblSelection_SelectedIndexChanged">
                                                        <asp:ListItem Value="1">Loan Scheme &nbsp;&nbsp;&nbsp;&nbsp;
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="0">Regular  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                                        </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divConcessionOption" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label></label>
                                                    </div>
                                                    <asp:RadioButtonList ID="rdConcessionOption" runat="server" TabIndex="1"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Amount Wise &nbsp;&nbsp;&nbsp;&nbsp;
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="0">Percentage Wise  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                                        </asp:ListItem>
                                                           <%--<asp:ListItem Value="3">Applied Students &nbsp;&nbsp;&nbsp;&nbsp;
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="4">Not Applied  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                                        </asp:ListItem>--%>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnShowConcession" ValidationGroup="Concession" ToolTip="Submit" runat="server" CssClass="btn btn-outline-info" OnClick="btnShowConcession_Click" TabIndex="21">Show</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmitConcession" runat="server" CssClass="btn btn-outline-info" Visible="false" OnClick="btnSubmitConcession_Click">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelConcession" runat="server" CssClass="btn btn-outline-danger" Visible="false" OnClick="btnCancelConcession_Click">Cancel</asp:LinkButton>
                                        
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Concession"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>
                                        <div class="col-md-12">
                                            <div id="div2" runat="server">
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <asp:ListView ID="lvlConcession" runat="server">
                                                        <LayoutTemplate>
                                                            <div>
                                                                <div class="sub-heading">
                                                                    <h5></h5>
                                                                </div>

                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="mytable">
                                                                    <thead class="bg-light-blue">
                                                                        <tr id="trRow">
                                                                            <th>
                                                                                <asp:CheckBox ID="chkRows" runat="server" onclick="return totAll(this);" /></th>
                                                                            <th>Student ID</th>
                                                                            <th>Student Name</th>
                                                                            <th>Applicable Fee</th>
                                                                            <th>Discount Type
                                                                                    <br />
                                                                                <div class="d-none DiscountType">
                                                                                    <asp:DropDownList ID="dllSelectAllType" runat="server" AppendDataBoundItems="true" CssClass="form-control dllSelectAllType" data-select2-enable="true" onchange="return SelectDiscountType(this);">
                                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </th>
                                                                            <th class="Con_Disc">Discount %</th>
                                                                            <th>Discount Fee
                                                                                    <input type="text" class="form-control txtForAllStudents d-none" onkeyup="return CalculateAmount(this);" onfocus="return CalculateAmount(this);"/>
                                                                            </th>
                                                                            <th>Net Payable</th>
                                                                            <th>Status</th>
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
                                                                    <asp:CheckBox ID="chktransfer" runat="server"/></td>

                                                                <td>
                                                                    <asp:Label runat="server" ID="lblreg" Text='<%# Eval("REGNO") %>'></asp:Label></td>
                                                                <asp:Label runat="server" ID="lblIdno" Text='<%# Eval("IDNO") %>' Visible="false"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblname" Text='<%# Eval("NAME_WITH_INITIAL") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:TextBox ID="lbltotal" runat="server" CssClass="form-control DemandAmount" Text='<%# Eval("TOTAL_AMT") %>' Enabled="false" onblur="return CheckMark(this);" />

                                                                    <%--  <asp:Label runat="server" ID="lbltotal" Text='<%# Eval("TOTAL_AMT") %>'  onblur="return CheckMark(this);">

                                                                            </asp:Label>--%></td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlConcession" runat="server" CssClass="form-control ddlConcession" data-select2-enable="true" AppendDataBoundItems="true" Enabled="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lblConcessionno" runat="server" Text='<% #Eval("CONCESSION_TYPE")%>' Visible="false"></asp:Label>
                                                                </td>
                                                                <td class="Con_Disc">
                                                                    <asp:DropDownList ID="ddlDiscount" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" Enabled="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lblDiscount" runat="server" Text='<% #Eval("DISCOUNT")%>' Visible="false"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDiscountFee" runat="server" CssClass="form-control FinalAmount" Text='<%# Eval("DISCOUNT_FEES") %>' Enabled="false" /></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNetPayable" runat="server" CssClass="form-control NetPayAmount" Text='<%# Eval("NET_PAYABLE") %>' Enabled="false" /></td>
                                                                <td>
                                                                    <asp:Label ID="lbldcridno" runat="server" Text='<%# Eval("DCR_IDNO") %>' Style="width: 150px"></asp:Label></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>

                                        </div>




                                    </ContentTemplate>
                                    <Triggers>
                                        <%--  <asp:PostBackTrigger ControlID="btnShowConcession" />--%>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane fade d-none" id="tab_3" runat="server" visible="false">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="schRule"
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
                                <asp:UpdatePanel ID="schRule" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Rule Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtRuleName" runat="server" ToolTip="Please Enter Rule Name" CssClass="form-control" ValidationGroup="schRule" TabIndex="6" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtRuleName"
                                                        Display="None" ErrorMessage="Please Enter Rule Name" ValidationGroup="schRule" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Decide Mode</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDecideMode" runat="server" CssClass="form-control" AppendDataBoundItems="True" data-select2-enable="true" TabIndex="7">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlDecideMode"
                                                        Display="None" ErrorMessage="Please Enter Decide Mode " InitialValue="0" ValidationGroup="schRule" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Percentage (%)</label>
                                                    </div>
                                                    <asp:TextBox ID="txtAmountPercent" runat="server" ToolTip="Please Enter Amount Percent" onblur="return IsNumeric(this);" CssClass="form-control" ValidationGroup="schRule" TabIndex="8" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtAmountPercent"
                                                        Display="None" ErrorMessage="Please Enter Amount Percent " ValidationGroup="schRule" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Required GPA</label>
                                                    </div>
                                                    <asp:TextBox ID="txtRequiredGPA" runat="server" ToolTip="Please Enter Required GPA" CssClass="form-control" ValidationGroup="schRule" TabIndex="9" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtRequiredGPA"
                                                        Display="None" ErrorMessage="Please Enter Required GPA" ValidationGroup="schRule" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnSubmitRule" ToolTip="Submit" OnClientClick="return validate()" OnClick="btnSubmitRule_Click" runat="server" CssClass="btn btn-outline-info" ValidationGroup="schRule" TabIndex="10">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelRule" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelRule_Click" TabIndex="11">Cancel</asp:LinkButton>
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="schRule"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            <asp:HiddenField ID="hdndeptno" runat="server" />
                                        </div>
                                        <div class="col-md-12">
                                            <div id="div1" runat="server">
                                                <asp:Panel ID="Panel1" runat="server">
                                                    <asp:ListView ID="lvlRule" runat="server">
                                                        <LayoutTemplate>
                                                            <div>
                                                                <div class="sub-heading">
                                                                    <h5></h5>
                                                                </div>

                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                    <thead class="bg-light-blue">
                                                                        <tr id="trRow">
                                                                            <th>Edit</th>
                                                                            <th>Rule Name</th>
                                                                            <th>Decide Mode</th>
                                                                            <th>Percentage (%)</th>
                                                                            <th>GPA</th>

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
                                                                    <asp:ImageButton ID="btneditRule" runat="server" ImageUrl="~/IMAGES/edit.png" CommandArgument='<%# Eval("RULENO") %>'
                                                                        AlternateText="Edit Record " OnClick="btneditRule_Click"
                                                                        ToolTip="Edit Record" />
                                                                </td>
                                                                <td><%# Eval("RULENAME") %></td>
                                                                <td><%# Eval("MODE_NAME") %></td>
                                                                <td>
                                                                    <asp:Label ID="lblAmount" runat="server" Text=' <%# Eval("AMOUNT") %>'></asp:Label></td>
                                                                <td><%# Eval("REQUIREDGPA") %></td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>

                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade" id="tab_4">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updsholarship"
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
                                <asp:UpdatePanel ID="updsholarship" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Faculty</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlFacultySchlorship" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True"
                                                        OnSelectedIndexChanged="ddlFacultySchlorship_SelectedIndexChanged" AutoPostBack="true" TabIndex="5">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlFacultySchlorship"
                                                        Display="None" ErrorMessage="Please Select Faculty" InitialValue="0" ValidationGroup="sholarship" />
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlFacultySchlorship"
                                                        Display="None" ErrorMessage="Please Select Faculty" InitialValue="0" ValidationGroup="excel" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Study Level</label>
                                                    </div>
                                                    <asp:ListBox ID="lstbxStudyLevelSchlorship" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" TabIndex="6"></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="lstbxStudyLevelSchlorship"
                                                        Display="None" ErrorMessage="Please Select Study Level" InitialValue="" ValidationGroup="sholarship" />
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Program</label>
                                                    </div>
                                                    <asp:ListBox ID="lstbxProgramSchlorship" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" TabIndex="7"></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="lstbxProgramSchlorship"
                                                        Display="None" ErrorMessage="Please Select Program" InitialValue="" ValidationGroup="sholarship" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Semester</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSemesterSchlorship" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="8">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSemesterSchlorship"
                                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="sholarship" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>GPA</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlGpa" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="8">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlGpa"
                                                                Display="None" ErrorMessage="Please Select GPA" InitialValue="0" ValidationGroup="sholar" />--%>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12" id="divScholership" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label></label>
                                                    </div>
                                                    <asp:RadioButtonList ID="rdScholership" runat="server" TabIndex="1"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Amount Wise &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="0">Percentage Wise  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
                                                                                        
                                                       </asp:ListItem>
                                                           <%--    <asp:ListItem Value="3">Applied Students &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                  &nbsp;&nbsp;&nbsp;&nbsp;
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="4">Not Applied  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                              
                                                        </asp:ListItem>--%>
                                                    </asp:RadioButtonList>

                                                </div>


                                                  <div class="form-group col-lg-3 col-md-6 col-12" id="divStatus" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup> </sup>
                                                        <label>Status</label>
                                                    </div>
                                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="8">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                          <asp:ListItem Value="1">Approve</asp:ListItem>
                                                                   <asp:ListItem Value="2">Reject</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="DropDownList1"
                                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="sholarship" />
                                                </div>


                                                <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>GPA</label>
                                                            </div>
                                                            <asp:TextBox ID="txtGPA" runat="server" CssClass="form-control" ToolTip="Please Enter GPA" ValidationGroup="sholar" TabIndex="9" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSemesterSchlorship"
                                                                Display="None" ErrorMessage="Please Enter GPA" ValidationGroup="sholar" />
                                                        </div>--%>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnShowScholarship" runat="server" CssClass="btn btn-outline-info" OnClick="btnShowScholarship_Click" ValidationGroup="sholarship" ToolTip="Submit" TabIndex="10">Show</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmitScholarship" runat="server" CssClass="btn btn-outline-info" Visible="false" OnClick="btnSubmitScholarship_Click" ValidationGroup="sholar" TabIndex="11">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelScholarship" runat="server" CssClass="btn btn-outline-danger" Visible="false" OnClick="btnCancelScholarship_Click" TabIndex="12">Cancel</asp:LinkButton>
                                                 <asp:LinkButton ID="btnExcel" runat="server" CssClass="btn btn-outline-info" ValidationGroup="excel" OnClick="btnExcel_Click">Export Excel</asp:LinkButton>
                                            <asp:ValidationSummary ID="vsExcel" runat="server" ValidationGroup="excel"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            
                                             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="sholarship"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>

                                        <div class="col-md-12">
                                            <div id="div3" runat="server">
                                                <asp:Panel ID="Panel3" runat="server">
                                                    <asp:ListView ID="lvlSholarship" runat="server">
                                                        <LayoutTemplate>
                                                            <div>
                                                                <div class="sub-heading">
                                                                    <h5></h5>
                                                                </div>

                                                                <div class="row mb-1">
                                                                    <div class="col-lg-2 col-md-6 offset-lg-7">
                                                                        <%--<button type="button" class="btn btn-outline-primary float-lg-right saveAsExcel">Export Excel</button>--%>
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
                                                                <div class="table-responsive" style="height: 500px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="MainLeadTable">
                                                                        <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                            <tr id="trRow">
                                                                                <th><asp:CheckBox ID="chkRows" runat="server" onclick="return totAll(this);" /></th>
                                                                                <th>Student ID</th>
                                                                                <th>Student Name</th>
                                                                                <th class="ScholershipFee">Applicable Fee</th>
                                                                                <%--<th style="display:none">Scholarship Rule</th>--%>
                                                                                <th>Scholarship Amount</th>
                                                                                <%--  <th>Net Payable</th>--%>
                                                                                <th>Status</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="chktransfer" runat="server" CssClass="chkSholership" /></td>

                                                                <td>
                                                                    <asp:Label runat="server" ID="lblreg" Text='<%# Eval("REGNO") %>'></asp:Label></td>
                                                                <asp:Label runat="server" ID="lblIdno" Text='<%# Eval("IDNO") %>' Visible="false"></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblname" Text='<%# Eval("NAME") %>'></asp:Label></td>
                                                                <td class="ScholershipFee">
                                                                    <asp:TextBox ID="lbltotal" runat="server" CssClass="form-control" Text='<%# Eval("TOTAL_AMT") %>' Enabled="false" />


                                                                </td>
                                                                <td style="display: none">
                                                                    <asp:DropDownList ID="ddlScholarshipRule" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" onblur="return CheckDiscount(this);">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        <asp:ListItem Value="1">R101</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lblScholarshipRule" runat="server" Visible="false"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtScholarshipAmount" runat="server" CssClass="form-control txtPercentage" Text='<%# Eval("SHOLARSHIP_AMOUNT") %>' Enabled="true" /></td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Status" Text='<%# Eval("RECON") %>' Font-Size="Smaller"></asp:Label></td>

                                                                <%--<td>
                                                                            <asp:TextBox ID="txtScholarshipNetPayable" runat="server" CssClass="form-control" Text='<%# Eval("NET_PAYABLE") %>' /></td>--%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>

                                        </div>



                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger  ControlID="btnExcel"/>
                                    </Triggers>
                                </asp:UpdatePanel>

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
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvlConcession$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvlConcession$ctrl';
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
    <script>

        function CheckMark(id) {

            var ValidChars = "0123456789.-";

            var num = true;
            var mChar;
            mChar = id.value.charAt(0);
            if (ValidChars.indexOf(mChar) == -1) {
                num = false;
                id.value = '';
                alert("Error! Only Numeric Values Are Allowed")
                id.select();
                id.focus();

            }
        }
    </script>
    <script type="text/javascript">
        function IsNumeric(txt) {
            var ValidChars = "0123456789.";
            var num = true;
            var mChar;
            cnt = 0

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);

                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Please enter Numeric values only")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }
    </script>

    <script>

        function CheckMark(id) {
            var rowIndex = id.offsetParent.parentNode.rowIndex - 1;
            var cellIndex = id.offsetParent.cellIndex;

            var Apllicable = 0; var Discount = 0;

            Apllicable = document.getElementById("ctl00_ContentPlaceHolder1_lvlConcession_ctrl" + rowIndex + "_lbltotal").value;


            Discount = document.getElementById("ctl00_ContentPlaceHolder1_lvlConcession_ctrl" + rowIndex + "_ddlDiscount");
            var option = Discount.options[Discount.selectedIndex];
            var Discounts = option.text;

            if (Discounts == 'Please Select') {
                var Discounts = 0;
            }
            if (Apllicable == '') {
                var Apllicable = 0;
            }
            var total = 100
            ConvertMark = (Number(Discounts) / Number(total)) * Number(Apllicable)
            var Netpayable = (Number(Apllicable) - ConvertMark);

            document.getElementById("ctl00_ContentPlaceHolder1_lvlConcession_ctrl" + rowIndex + "_txtDiscountFee").value = ConvertMark;

            document.getElementById("ctl00_ContentPlaceHolder1_lvlConcession_ctrl" + rowIndex + "_txtNetPayable").value = Netpayable;
        }
    </script>

    <script>

        $("[id*=ddlDiscount]").bind("change", function () {
            //Find and reference the GridView.
            var List = $(this).closest("table");
            var ddlValue = $(this).find('option:selected').text();
            var td = $("td", $(this).closest("tr"));
            var Apllicable = $("[id*=lbltotal]", td).val();
            var ConvertMark = (Number(ddlValue) / Number(100)) * Number(Apllicable)
            var Netpayable = (Number(Apllicable) - ConvertMark);
            $("[id*=txtDiscountFee]", td).val(ConvertMark);
            $("[id*=txtNetPayable]", td).val(Netpayable);
        });

    </script>

    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            $("[id*=ddlDiscount]").bind("change", function () {
                //Find and reference the GridView.
                var List = $(this).closest("table");
                var ddlValue = $(this).find('option:selected').text();
                var td = $("td", $(this).closest("tr"));
                var Apllicable = $("[id*=lbltotal]", td).val();
                var ConvertMark = (Number(ddlValue) / Number(100)) * Number(Apllicable)
                var Netpayable = (Number(Apllicable) - ConvertMark);
                $("[id*=txtDiscountFee]", td).val(ConvertMark);
                $("[id*=txtNetPayable]", td).val(Netpayable);
            });
        });
    </script>

    <%--  <script>
        function CheckDiscount(id) {
            var rowIndex = id.offsetParent.parentNode.rowIndex - 1;
            var cellIndex = id.offsetParent.cellIndex;

            var Apllicable = 0; var Discount = 0;

            Apllicable = document.getElementById("ctl00_ContentPlaceHolder1_lvlSholarship_ctrl" + rowIndex + "_lbltotal").value;
            


            Discount = document.getElementById("ctl00_ContentPlaceHolder1_lvlSholarship_ctrl" + rowIndex + "_ddlScholarshipRule").value;
            //alert(Discount);
            //var option = Discount.options[Discount.SelectedValue];
            //var Discounts = option.text;
            
            if (Discount == 'Please Select') {
                var Discounts = 0;
            }
            if (Apllicable == '') {
                var Apllicable = 0;
            }
            var total = 100
            ConvertMark = (Number(Discount) / Number(total)) * Number(Apllicable)
            var Netpayable = (Number(Apllicable) - ConvertMark);

            document.getElementById("ctl00_ContentPlaceHolder1_lvlSholarship_ctrl" + rowIndex + "_txtScholarshipAmount").value = ConvertMark;

            document.getElementById("ctl00_ContentPlaceHolder1_lvlSholarship_ctrl" + rowIndex + "_txtScholarshipNetPayable").value = Netpayable;
        }

    </script>--%>

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
        var summary = "";
        $(function () {
            $('#btnSubmitRule').click(function () {
                localStorage.setItem("currentId", "#btnSubmitRule,Submit");
                debugger;
                ShowLoader('#btnSubmitRule');

                if ($('#txtRuleName').val() == "")
                    summary += '<br>Please Enter Rule Name';
                if ($('#ddlDecideMode').val() == "0")
                    summary += '<br>Please Select Decide Mode ';
                if ($('#txtAmountPercent').val() == "")
                    summary += '<br>Please Enter Amount Percent';
                if ($('#txtRequiredGPA').val() == "")
                    summary += '<br>Please Enter Required GPA';

                if (summary != "") {
                    customAlert(summary);
                    summary = "";
                    return false
                }
            });
        });
    </script>
    <script>
        $("#ctl00_ContentPlaceHolder1_rdConcessionOption").click(function () {

            var radioValue = $('#<%=rdConcessionOption.ClientID %> input[type=radio]:checked').val();

            if (radioValue == 1) {
                $(".Con_Disc").addClass('d-none');
                $(".txtForAllStudents").removeClass('d-none');
                $(".DiscountType").removeClass('d-none');
            }
            else {
                $(".Con_Disc").removeClass('d-none');
                $(".txtForAllStudents").addClass('d-none');
                $(".DiscountType").addClass('d-none');
            }
        });
    </script>

    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#ctl00_ContentPlaceHolder1_rdConcessionOption").click(function () {

                var radioValue = $('#<%=rdConcessionOption.ClientID %> input[type=radio]:checked').val();

                if (radioValue == 1) {
                    $(".Con_Disc").addClass('d-none');
                    $(".txtForAllStudents").removeClass('d-none');
                    $(".DiscountType").removeClass('d-none');
                }
                else {
                    $(".Con_Disc").removeClass('d-none');
                    $(".txtForAllStudents").addClass('d-none');
                    $(".DiscountType").addClass('d-none');
                }
            });
        });
    </script>

    <script>
        function CalculateAmount(Amount) {
            //if($('.txtForAllStudents').val() == '')
            //{
            //    $('.FinalAmount').val(0);
            //}
            //else{
            //    $('.FinalAmount').val($('.txtForAllStudents').val());
            //}
            $(".DemandAmount").each(function (index, value) {

                var List = $(this).closest("table");
                var td = $("td", $(this).closest("tr"));
                var CheckBoxValue = $("[id*=chktransfer]", td).is(":checked");

                if($('.txtForAllStudents').val() != '')
                {
                    if(CheckBoxValue == true) //document.getElementById("ctl00_ContentPlaceHolder1_lvlConcession_ctrl" + index + "_chktransfer").checked
                    {
                        //document.getElementById("ctl00_ContentPlaceHolder1_lvlConcession_ctrl" + index + "_txtDiscountFee").value = $('.txtForAllStudents').val();
                        $("[id*=txtDiscountFee]", td).val($('.txtForAllStudents').val());
                        $("[id*=txtNetPayable]", td).val(parseFloat($(this).val() - $('.txtForAllStudents').val()));
                        //document.getElementById("ctl00_ContentPlaceHolder1_lvlConcession_ctrl" + index + "_txtNetPayable").value = parseFloat($(this).val() - $('.txtForAllStudents').val());
                    }
                    //else
                    //{
                    //    document.getElementById("ctl00_ContentPlaceHolder1_lvlConcession_ctrl" + index + "_txtNetPayable").value = null;
                    //    document.getElementById("ctl00_ContentPlaceHolder1_lvlConcession_ctrl" + index + "_txtDiscountFee").value = null;
                    //}
                }
                else
                {
                    if(CheckBoxValue == true) ////document.getElementById("ctl00_ContentPlaceHolder1_lvlConcession_ctrl" + index + "_chktransfer").checked
                    {
                        $("[id*=txtDiscountFee]", td).val('');$("[id*=txtNetPayable]", td).val('');
                        //document.getElementById("ctl00_ContentPlaceHolder1_lvlConcession_ctrl" + index + "_txtDiscountFee").value = null;
                        //document.getElementById("ctl00_ContentPlaceHolder1_lvlConcession_ctrl" + index + "_txtNetPayable").value = null;
                    }
                }
            });
        }
    </script>
    <script>
        function SelectDiscountType(ddl) {

            $('.ddlConcession').val($('.dllSelectAllType option:selected').val()).change();
        }
    </script>

    <script language="javascript" type="text/javascript">
        function totAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
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
           
            var workbook = XLSX.utils.book_new();
            var allDataArray = [];
            allDataArray = makeTableArray(table5, allDataArray);
            var worksheet = XLSX.utils.aoa_to_sheet(allDataArray);
            workbook.SheetNames.push("Test");
            workbook.Sheets["Test"] = worksheet;
            XLSX.writeFile(workbook, "Scholership.xlsx");
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

     <script>
         $("#ctl00_ContentPlaceHolder1_rdScholership").click(function () {

             var radioValue = $('#<%=rdScholership.ClientID %> input[type=radio]:checked').val();

            if (radioValue == 0) {
                $(".ScholershipFee").addClass('d-none');
                $(".ScholershipFee").each(function (index, value) {

                    if(document.getElementById("ctl00_ContentPlaceHolder1_lvlSholarship_ctrl" + index + "_chktransfer").checked == true)
                    {
                        document.getElementById("ctl00_ContentPlaceHolder1_lvlSholarship_ctrl" + index + "_txtScholarshipAmount").value = null;
                    }
                });
            }
            else {
                $(".ScholershipFee").removeClass('d-none');
            }
        });
    </script>

    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#ctl00_ContentPlaceHolder1_rdScholership").click(function () {

                var radioValue = $('#<%=rdScholership.ClientID %> input[type=radio]:checked').val();

                if (radioValue == 0) {
                    $(".ScholershipFee").addClass('d-none');
                    $(".ScholershipFee").each(function (index, value) {
                      
                        if(document.getElementById("ctl00_ContentPlaceHolder1_lvlSholarship_ctrl" + index + "_chktransfer").checked == true)
                        {
                            document.getElementById("ctl00_ContentPlaceHolder1_lvlSholarship_ctrl" + index + "_txtScholarshipAmount").value = null;
                        }
                    });
                }
                else {
                    $(".ScholershipFee").removeClass('d-none');
                }
            });
        });
    </script>

     <script>
         $('.txtPercentage').keyup(function(){
             var radioValue = $('#<%=rdScholership.ClientID %> input[type=radio]:checked').val();

             if (radioValue == 0) {
                 if ($(this).val() > 100){
                     alert("Percent below or equal 100%");
                     $(this).val('');
                 }
             }
             else if (radioValue == 1) {

             }
             else
             {
                 alert("Please Select Payment Type !!!");
                 $(this).val('');
             }
         });
    </script>

    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.txtPercentage').keyup(function(){
                var radioValue = $('#<%=rdScholership.ClientID %> input[type=radio]:checked').val();

                if (radioValue == 0) {
                    if ($(this).val() > 100){
                        alert("Percent below or equal 100%");
                        $(this).val('');
                    }
                }
                else if (radioValue == 1) {

                }
                else
                {
                    alert("Please Select Payment Type !!!");
                    $(this).val('');
                }
            });
        });
    </script>

         <script>
             $('.chkSholership').click(function(){
                 var radioValue = $('#<%=rdScholership.ClientID %> input[type=radio]:checked').val();

             if (radioValue == 0) {
                 $(".chkSholership").each(function (index, value) {
                      
                     if(document.getElementById("ctl00_ContentPlaceHolder1_lvlSholarship_ctrl" + index + "_chktransfer").checked == true && document.getElementById("ctl00_ContentPlaceHolder1_lvlSholarship_ctrl" + index + "_Status").innerHTML == "Not Confirm")
                     {
                         document.getElementById("ctl00_ContentPlaceHolder1_lvlSholarship_ctrl" + index + "_txtScholarshipAmount").value = null;
                     }
                 });
             }
             
         });
    </script>

 
     <script>
         var parameter = Sys.WebForms.PageRequestManager.getInstance();
         parameter.add_endRequest(function () {
             $('.chkSholership').click(function(){
                 var radioValue = $('#<%=rdScholership.ClientID %> input[type=radio]:checked').val();

                 if (radioValue == 0) {
                     $(".chkSholership").each(function (index, value) {
                      
                         if(document.getElementById("ctl00_ContentPlaceHolder1_lvlSholarship_ctrl" + index + "_chktransfer").checked == true && document.getElementById("ctl00_ContentPlaceHolder1_lvlSholarship_ctrl" + index + "_Status").innerHTML == "Not Confirm")
                         {
                             document.getElementById("ctl00_ContentPlaceHolder1_lvlSholarship_ctrl" + index + "_txtScholarshipAmount").value = null;
                         }
                     });
                 }
             
             });
         });
    </script>

</asp:Content>

