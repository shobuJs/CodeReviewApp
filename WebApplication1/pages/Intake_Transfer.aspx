<%@ Page Language="C#" Title="" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master"
    CodeFile="Intake_Transfer.aspx.cs" Inherits="ACADEMIC_Intake_Transfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  tab 1 date range script start--%>

    <link href="<%=Page.ResolveClientUrl("~/plugins/Sematic/CSS/myfilterOpt.css") %>" rel="stylesheet" />
    <link href="<%=Page.ResolveClientUrl("~/plugins/Sematic/CSS/semantic.min.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/Sematic/JS/semantic.min.js")%>"></script>

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>
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
        #ctl00_ContentPlaceHolder1_Panel3 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <script>
        $(document).ready(function () {
            var table = $('#mytable1').DataTable({
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
                                return $('#mytable1').DataTable().column(idx).visible();
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
                                                return $('#mytable1').DataTable().column(idx).visible();
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
                                                return $('#mytable1').DataTable().column(idx).visible();
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
                                                return $('#mytable1').DataTable().column(idx).visible();
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
                var table = $('#mytable1').DataTable({
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
                                    return $('#mytable1').DataTable().column(idx).visible();
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
                                                    return $('#mytable1').DataTable().column(idx).visible();
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
                                                    return $('#mytable1').DataTable().column(idx).visible();
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
        #getdate, #getdateexam {
            border-top: none;
            border-left: none;
            border-right: none;
            border-bottom: 1px solid #ccc;
            height: 30px !important;
        }
        #ctl00_ContentPlaceHolder1_divapti .dataTables_scrollHeadInner,
        #ctl00_ContentPlaceHolder1_lvapti .dataTables_scrollHeadInner,
        #ctl00_ContentPlaceHolder1_divallotment .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
       <style>
        .sea-rch i {
            color: #5b5b5b;
            cursor: pointer;
        }

            .sea-rch i:hover {
                color: red;
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
     <script>
           $(document).ready(function () {
               var table = $('#my-Table').DataTable({
                   responsive: true,
                   lengthChange: true,
                   scrollY: 320,
                   scrollX: true,
                   scrollCollapse: true,
                   paging : false,
                   dom: 'lBfrtip',

                   //Export functionality
                   buttons: [
                       {
                           extend: 'colvis',
                           text: 'Column Visibility',
                           columns: function (idx, data, node) {
                               var arr = [0];
                               if (arr.indexOf(idx) !== -1) {
                                   return false;
                               } else {
                                   return $('#my-Table').DataTable().column(idx).visible();
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
                                                   return $('#my-Table').DataTable().column(idx).visible();
                                               }
                                           }
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
                                                   return $('#my-Table').DataTable().column(idx).visible();
                                               }
                                           }
                                       }
                                   },
                                   {
                                       extend: 'pdfHtml5',
                                       exportOptions: {
                                           columns: function (idx, data, node) {
                                               var arr = [0];
                                               if (arr.indexOf(idx) !== -1) {
                                                   return false;
                                               } else {
                                                   return $('#my-Table').DataTable().column(idx).visible();
                                               }
                                           }
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
                   var table = $('#my-Table').DataTable({
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
                                   var arr = [0];
                                   if (arr.indexOf(idx) !== -1) {
                                       return false;
                                   } else {
                                       return $('#my-Table').DataTable().column(idx).visible();
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
                                                       return $('#my-Table').DataTable().column(idx).visible();
                                                   }
                                               }
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
                                                       return $('#my-Table').DataTable().column(idx).visible();
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           extend: 'pdfHtml5',
                                           exportOptions: {
                                               columns: function (idx, data, node) {
                                                   var arr = [0];
                                                   if (arr.indexOf(idx) !== -1) {
                                                       return false;
                                                   } else {
                                                       return $('#my-Table').DataTable().column(idx).visible();
                                                   }
                                               }
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

        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvIntake$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvIntake$ctrl';
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
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            document.getElementById('<%=hdnDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#picker').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                    ranges: {
                        //                    'Today': [moment(), moment()],
                        //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                        //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                        //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                        //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                        //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                    },
                    //<!-- ========= Disable dates after today ========== -->
                    //maxDate: new Date(),
                    //<!-- ========= Disable dates after today END ========== -->
                },
            function (start, end) {
                debugger
                $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
                document.getElementById('<%=hdnDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            });
        });

    </script>
    <script>


        function Setdate(date) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(document).ready(function () {
                    debugger;
                    var startDate = moment(date.split('-')[0], "MMM DD, YYYY");
                    var endtDate = moment(date.split('-')[1], "MMM DD, YYYY");
                    //$('#date').html(date);
                    $('#date').html(startDate.format("MMM DD, YYYY") + ' - ' + endtDate.format("MMM DD, YYYY"));
                    document.getElementById('<%=hdnDate.ClientID%>').value = date;
                    //$('#picker').daterangepicker({ startDate: startDate, endDate: endtDate});
                    $('#picker').daterangepicker({
                        startDate: startDate.format("MM/DD/YYYY"),
                        endDate: endtDate.format("MM/DD/YYYY"),
                        ranges: {
                        },
                    },
            function (start, end) {
                debugger
                $('#date').html(start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
                document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
            });

                });
            });
};

    </script>

    <%-- tab 1 date range scrip end--%>
    <%--  tab 2 date range script start--%>
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
    <script type="text/javascript">
        $(document).ready(function () {
            $('#getdate').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },

                ranges: {

                },

            },
        function (start, end) {

            $('#Datepick').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            document.getElementById('<%=hdfdate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
            __doPostBack("dynamiccall");
        });

            $('#Datepick').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            document.getElementById('<%=hdfdate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))

        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#getdate').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },

                    ranges: {

                    },

                },
            function (start, end) {
                $('#Datepick').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                document.getElementById('<%=hdfdate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
                    __doPostBack("dynamiccall");
                });

                    $('#Datepick').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
                    document.getElementById('<%=hdfdate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))

                });
            });

    </script>
    <script>
        function Setdate(date) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(document).ready(function () {
                    debugger;
                    var startDate = moment(date.split('-')[0], "MMM DD, YYYY");
                    var endtDate = moment(date.split('-')[1], "MMM DD, YYYY");
                    //$('#date').html(date);
                    $('#Datepick').html(startDate.format("MMM DD, YYYY") + ' - ' + endtDate.format("MMM DD, YYYY"));
                    document.getElementById('<%=hdfdate.ClientID%>').value = date;
                    //$('#picker').daterangepicker({ startDate: startDate, endDate: endtDate});
                    $('#getdate').daterangepicker({
                        startDate: startDate.format("MM/DD/YYYY"),
                        endDate: endtDate.format("MM/DD/YYYY"),
                        ranges: {
                        },
                    },
                function (start, end) {
                    debugger
                    $('#Datepick').html(start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
                    document.getElementById('<%=hdfdate.ClientID%>').value = (start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
                });

                });
            });
    };

    </script>

    <script type="text/javascript" language="javascript">

        function checkbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvallotment$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvallotment$ctrl';
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

    <%--    tab 2 date range scrip end--%>

    <%--  tab 3 date range script start--%>
    <script>
        function Setdate(date) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(document).ready(function () {
                    debugger;
                    var startDate = moment(date.split('-')[0], "MMM DD, YYYY");
                    var endtDate = moment(date.split('-')[1], "MMM DD, YYYY");
                    //$('#date').html(date);
                    $('#Datepickexam').html(startDate.format("MMM DD, YYYY") + ' - ' + endtDate.format("MMM DD, YYYY"));
                    document.getElementById('<%=hdfexamdate.ClientID%>').value = date;
                    //$('#picker').daterangepicker({ startDate: startDate, endDate: endtDate});
                    $('#getdateexam').daterangepicker({
                        startDate: startDate.format("MM/DD/YYYY"),
                        endDate: endtDate.format("MM/DD/YYYY"),
                        ranges: {
                        },
                    },
             function (start, end) {
                 debugger
                 $('#Datepickexam').html(start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
                 document.getElementById('<%=hdfexamdate.ClientID%>').value = (start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
            });

                });
            });
};

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#getdateexam').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },

                ranges: {

                },

            },
        function (start, end) {
            $('#Datepickexam').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            document.getElementById('<%=hdfexamdate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
            __doPostBack("dynamiccallexam");
        });

            $('#Datepickexam').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            document.getElementById('<%=hdfexamdate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#getdateexam').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },

                    ranges: {

                    },

                },
            function (start, end) {
                debugger
                $('#Datepickexam').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                document.getElementById('<%=hdfexamdate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                __doPostBack("dynamiccallexam");
            });

                $('#Datepickexam').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
                document.getElementById('<%=hdfexamdate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            });
        });

    </script>
    <script type="text/javascript" language="javascript">

        function checkEXAM(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvexamcenter$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvexamcenter$ctrl';
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

    <%--  tab 3 date range script start--%>

    <script type="text/javascript" language="javascript">

        function checkAllhallticket(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvhallticket$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvhallticket$ctrl';
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

    <asp:UpdatePanel ID="update" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="nav-tabs-custom mt-2 col-12 pb-4" id="myTabContent">
                            <%-- <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                                <li class="nav-item" id="divlkintaketransfer" runat="server">
                                    <a ID="lkintaketransfer" runat="server" OnClick="lkintaketransfer_Click" class="nav-link active" TabIndex="1">Intake Transfer</a></li>
                                <li class="nav-item" id="divlkintakeallotment" runat="server">
                                    <a ID="lkintakeallotment" runat="server" OnClick="lkintakeallotment_Click" class="nav-link" TabIndex="2">Intake Allotment</a></li>
                                <li class="nav-item" id="divlkexamcenter" runat="server">
                                    <a ID="lkexamcenter" runat="server" OnClick="lkexamcenter_Click" class="nav-link" TabIndex="3">Exam Center Allotment</a></li>
                                <li class="nav-item" id="divlkhallticket" runat="server">
                                    <a ID="lkhallticket" runat="server" OnClick="lkhallticket_Click" class="nav-link" TabIndex="4">Apptitude Hall Ticket Report</a></li>
                                <li class="nav-item" id="divlkapptitutetest" runat="server">
                                    <a ID="lktaptitutetest" runat="server" OnClick="lktaptitutetest_Click" class="nav-link" TabIndex="5">Apptitude Test Mark Entry</a></li>
                            </ul>--%>
                            <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                                <li class="nav-item active" id="divlkintaketransfer" runat="server">
                                    <asp:LinkButton ID="lkintaketransfer" runat="server" OnClick="lkintaketransfer_Click" CssClass="nav-link" TabIndex="1">Intake Transfer</asp:LinkButton></li>
                                <li class="nav-item" id="divlkintakeallotment" runat="server">
                                    <asp:LinkButton ID="lkintakeallotment" runat="server" OnClick="lkintakeallotment_Click" CssClass="nav-link" TabIndex="2" Visible="false">Intake Allotment</asp:LinkButton></li>
                                <li class="nav-item" id="divlkexamcenter" runat="server">
                                    <asp:LinkButton ID="lkexamcenter" runat="server" OnClick="lkexamcenter_Click" CssClass="nav-link" TabIndex="3">Exam Center Allotment</asp:LinkButton></li>
                                <li class="nav-item" id="divlkhallticket" runat="server">
                                    <asp:LinkButton ID="lkhallticket" runat="server" OnClick="lkhallticket_Click" CssClass="nav-link" TabIndex="4">Aptitude Hall Ticket Report</asp:LinkButton></li>
                                <li class="nav-item" id="divlkapptitutetest" runat="server">
                                    <asp:LinkButton ID="lktaptitutetest" runat="server" OnClick="lktaptitutetest_Click" CssClass="nav-link" TabIndex="5">Aptitude Test Mark Entry</asp:LinkButton></li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane fade show active" id="divintaketransfer" role="tabpanel" runat="server" aria-labelledby="IntakeTransfer-tab">
                                    <div class="pageright-wrapper chiller-theme ">
                                        <div id="sidebar" class="right-wrapper">
                                            <a id="showright-sidebar" class="sidebtn" style="cursor: pointer">
                                                <asp:Image ID="ImLogo" runat="server" ImageUrl="../IMAGES/right-arrow.png" class="btnsidebar" />
                                            </a>
                                            <div class="right-content" style="background-color: #fff;">
                                                <div class="right-brand">
                                                    <div class="filter-heading">
                                                        <a href="#">FILTERS</a>
                                                        <i class="fa fa-search filter-toggle filter-icon" id="filter-toggle"></i>

                                                        <div class="filter-text input-group  mt-3">
                                                            <div class="input-group-prepend input-filter">
                                                                <span class="input-group-text"><i class="fa fa-search"></i></span>
                                                                <input type="text" placeholder="Search fields" class="form-control filter-FC">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div>
                                                    <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
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

                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <div class="sidebar-header scrollbar-right">
                                                            <div class="form-group">
                                                                <label>Select Topics</label>

                                                                <asp:ListBox ID="ddlMainLeadLabel" runat="server" AppendDataBoundItems="true" class="form-control"
                                                                    data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Registered Student</asp:ListItem>
                                                                    <asp:ListItem Value="2">Payment Pending</asp:ListItem>
                                                                    <asp:ListItem Value="3">Test Result</asp:ListItem>
                                                                </asp:ListBox>
                                                            </div>

                                                            <%--<div class="form-group">
                                                        <label>Select Topics</label>
                                                   
                                                        <asp:ListBox ID="lstbSecondHead" runat="server" AppendDataBoundItems="true" class="form-control" 
                                                            data-select2-enable="true">
                                                           <%-- <asp:ListItem>All</asp:ListItem>--%>
                                                            <%--</asp:ListBox>
                                                    </div>--%>
                                                            <%--<div class="form-group" id="dvThirdListbox" runat="server">
                                                        <label>Select Topics</label>
                                                        <asp:ListBox ID="lstbThirdHead" runat="server" AppendDataBoundItems="true" class="form-control" data-select2-enable="true"
                                                            SelectionMode="single">--%>
                                                            <%--  <asp:ListItem>All</asp:ListItem>--%>
                                                            <%-- </asp:ListBox>
                                                    </div>--%>

                                                            <%--<div id="divdate" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>
                                                                <asp:Label ID="lblSessionStartEndDate" runat="server" Font-Bold="true"></asp:Label></label>
                                                        </div>
                                                        <asp:TextBox ID="txtstartdate" runat="server" TabIndex="4" CssClass="datePicker PickerDate form-control" />
                                                        <asp:RequiredFieldValidator ID="valstartdate" runat="server" ControlToValidate="txtstartdate"
                                                            Display="None" ErrorMessage="Please Enter Date." SetFocusOnError="true" ValidationGroup="Report" />
                                                    </div>--%>

                                                            <div class="sidebar-footer">
                                                                <asp:Button ID="btnApplyFilter" runat="Server" Text="Apply Filter" CssClass="btn btn-outline-info" OnClick="btnApplyFilter_Click" />&nbsp;&nbsp;
                                                    <asp:Button ID="btnClearFilter" runat="server" Text="Clear Filter" CssClass="btn btn-outline-danger" OnClick="btnClearFilter_Click" />
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlMainLeadLabel" />
                                                        <%--  <asp:AsyncPostBackTrigger ControlID="lstbSecondHead" />--%>
                                                        <asp:PostBackTrigger ControlID="btnApplyFilter" />
                                                        <asp:PostBackTrigger ControlID="btnClearFilter" />
                                                    </Triggers>
                                                </asp:UpdatePanel>


                                            </div>
                                            <!-- sidebar-content  -->
                                        </div>
                                    </div>
                                    <div>
                                        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updIntaketransfer"
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
                                    <asp:UpdatePanel ID="updIntaketransfer" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-12">
                                                    <div class="sub-heading mt-3">
                                                        <h5>Intake Transfer</h5>
                                                    </div>
                                                    <div class="box-body">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblDYSession" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlcurreentintake" runat="server" TabIndex="1" CssClass="form-control select2 select-click"
                                                                        AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlcurreentintake_SelectedIndexChanged"
                                                                        ToolTip="Please Select Intake">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlcurreentintake"
                                                                        Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                                                        ValidationGroup="submit" />
                                                                    <asp:HiddenField ID="hdnDate" runat="server" />
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblDyAdmissionType" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlDiscipline" runat="server" TabIndex="2" CssClass="form-control select2 select-click"
                                                                        AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlDiscipline_SelectedIndexChanged"
                                                                        ToolTip="Please Select Decepline">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDiscipline"
                                                                        Display="None" ErrorMessage="Please Select Decepline" InitialValue="0"
                                                                        ValidationGroup="submit" />
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <asp:Label ID="lblDyProgram" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:ListBox ID="ddlProgram" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"
                                                                        SelectionMode="multiple" TabIndex="3" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:ListBox>
                                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlProgram"
                                                                        Display="None" ErrorMessage="Please Select Program" InitialValue="0" 
                                                                        ValidationGroup="submit" />--%>
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                                    <div class="label-dynamic">
                                                                        <label>From Date</label>
                                                                    </div>
                                                                    <div class="input-group">
                                                                        <div class="input-group-addon">
                                                                            <i id="calfrom" class="fa fa-calendar"></i>
                                                                        </div>
                                                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" ValidationGroup="Cancel" TabIndex="8"
                                                                            ToolTip="Please Select Date">
                                                                        </asp:TextBox>
                                                                        <ajaxToolKit:CalendarExtender ID="ceFromdate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                            PopupButtonID="calfrom" TargetControlID="txtFromDate" />
                                                                        <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server"
                                                                            TargetControlID="txtFromDate" Mask="99/99/9999" MessageValidatorTip="true"
                                                                            MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                                            ErrorTooltipEnabled="True" />
                                                                        <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meeFromDate"
                                                                            ControlToValidate="txtFromDate" Display="None" EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter From Date"
                                                                            InvalidValueBlurredMessage="Invalid Date" InvalidValueMessage="From Date is Invalid (Enter dd-mm-yyyy Format)"
                                                                            TooltipMessage="Please Enter From Date" ValidationGroup="Cancel" />
                                                                    </div>
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <asp:Label ID="lblStartEndDate" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <div id="picker" class="form-control">
                                                                        <i class="fa fa-calendar"></i>&nbsp;
                                                            <span id="date"></span>
                                                                        <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                                    <div class="label-dynamic">
                                                                        <label>To Date</label>
                                                                    </div>
                                                                    <div class="input-group">
                                                                        <div class="input-group-addon">
                                                                            <i id="calto" class="fa fa-calendar"></i>
                                                                        </div>
                                                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" ValidationGroup="Cancel" TabIndex="9"
                                                                            ToolTip="Please Select Date">
                                                                        </asp:TextBox>
                                                                        <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                            PopupButtonID="calto" TargetControlID="txtToDate">
                                                                        </ajaxToolKit:CalendarExtender>
                                                                        <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate" Mask="99/99/9999"
                                                                            MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                                        <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meeToDate" ControlToValidate="txtToDate"
                                                                            Display="None" EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter To Date"
                                                                            InvalidValueBlurredMessage="Invalid Date" InvalidValueMessage="To Date is Invalid (Enter dd-mm-yyyy Format)"
                                                                            TooltipMessage="Please Enter To Date" ValidationGroup="Cancel" />
                                                                    </div>
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblTransferIntake" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddltransferintake" runat="server" TabIndex="1" CssClass="form-control select2 select-click"
                                                                        AppendDataBoundItems="True"
                                                                        ToolTip="Please Select Intake">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:HiddenField ID="hdfcurrent" runat="server" Value='<%# Eval("ADMBATCH") %>' />
                                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddltransferintake"
                                                                        Display="None" ErrorMessage="Please Select Intake" InitialValue="0" 
                                                                        ValidationGroup="submit" />--%>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnSave" runat="server" ToolTip="Submit"
                                                                CssClass="btn btn-outline-info" OnClick="btnSave_Click" TabIndex="7" ValidationGroup="submit" Visible="false">Submit</asp:LinkButton>

                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                                TabIndex="8" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" Visible="false" />

                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                                        </div>

                                                        <div class="col-md-12" id="lvintakelist" runat="server" visible="false">
                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <asp:ListView ID="lvIntake" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Intake Transfer List</h5>
                                                                        </div>

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
                                                                <div class="table-responsive" style="max-height: 420px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="FirstTab">
                                                                        <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                            <tr>
                                                                                    <th style="text-align: center">
                                                                                        <asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" />
                                                                                    </th>
                                                                                    <th style="text-align: center">SrNo </th>
                                                                                    <th style="text-align: center">Application No.</th>
                                                                                    <th >Student Name</th>
                                                                                    <th style="text-align: center">Intake</th>

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
                                                                                <asp:CheckBox ID="chktransfer" runat="server" ToolTip='<%# Eval("USERNO")%>' /></td>
                                                                            <td style="text-align: center">
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                                <asp:HiddenField ID="hdfvalue" runat="server" />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:Label ID="username" runat="server" Text='<%# Eval("USERNAME")%>'></asp:Label>   
                                                                                <%--<asp:HiddenField ID="hdfidno" runat="server" Value='<%# Eval("IDNO")%>'   />--%>                                                  
                                                                            </td>
                                                                            <td>

                                                                                <%# Eval("NAME")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%--  <asp:Label ID="lblintake" runat="server" ToolTip='<%# Eval("BATCHNAME") %>'></asp:Label>--%>
                                                                                <%# Eval("BATCHNAME")%> 
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div id="divintakeallotment" runat="server" visible="false" role="tabpanel" aria-labelledby="IntakeAllotment-tab">
                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updIntakeAlootment"
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
                                    <asp:UpdatePanel ID="updIntakeAlootment" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-12">
                                                    <div class="sub-heading mt-3">
                                                        <h5>Intake Allotment</h5>
                                                    </div>
                                                    <div class="box-body">
                                                        <div class="col-12">
                                                            <div class="row">

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblDisc" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlugpg" runat="server" TabIndex="1" CssClass="form-control select2 select-click"
                                                                        AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlugpg_SelectedIndexChanged"
                                                                        ToolTip="Please Select Descipline">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlugpg"
                                                                        Display="None" ErrorMessage="Please Select Descipline" InitialValue="0"
                                                                        ValidationGroup="save" />
                                                                    <asp:HiddenField ID="hdfdate" runat="server" />
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <asp:Label ID="lblDate" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <div id="getdate" class="form-control">
                                                                        <i class="fa fa-calendar"></i>&nbsp;
                                                        <span id="Datepick"></span>
                                                                        <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblIntakeAllotment" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlintakeAllotment" runat="server" TabIndex="1" CssClass="form-control select2 select-click"
                                                                        AppendDataBoundItems="True"
                                                                        ToolTip="Please Select Intake">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlintakeAllotment"
                                                            Display="None" ErrorMessage="Please Select Intake" InitialValue="0" 
                                                           ValidationGroup="save" />
                                                                    --%>
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnsubmit" runat="server" ToolTip="Submit"
                                                                CssClass="btn btn-outline-info" OnClick="btnsubmit_Click" TabIndex="7" ValidationGroup="save" Visible="false">Submit</asp:LinkButton>

                                                            <asp:Button ID="btnclear" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                                TabIndex="8" CssClass="btn btn-outline-danger" OnClick="btnclear_Click" Visible="false" />

                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="save"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                                        </div>
                                                        <div class="col-md-12" id="divallotment" runat="server" visible="false">
                                                            <asp:Panel ID="Panel2" runat="server">
                                                                <asp:ListView ID="lvallotment" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Intake Allotment List</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th style="text-align: center">
                                                                                        <asp:CheckBox ID="chkallot" runat="server" OnClick="checkbox(this)" />
                                                                                    </th>
                                                                                    <th style="text-align: center">SrNo </th>
                                                                                    <th style="text-align: center">Username</th>
                                                                                    <th style="text-align: center">Student Name</th>
                                                                                    <th style="text-align: center">Old Intake</th>
                                                                                    <th style="text-align: center">New Intake</th>
                                                                                    <th style="text-align: center">Program</th>
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
                                                                                <asp:CheckBox ID="chkallotment" runat="server" ToolTip='<%# Eval("USERNO")%>' /></td>
                                                                            <td style="text-align: center">
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                                <asp:HiddenField ID="hdfvalue" runat="server" />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("USERNAME")%> 
                                                                            </td>
                                                                            <td style="text-align: center">

                                                                                <%# Eval("NAME")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:Label ID="lblintake" runat="server" Text='<%# Eval("BATCHNAME") %>' ToolTip='<%# Eval("OLDINTAKE") %>'></asp:Label>
                                                                                <%-- <%# Eval("BATCHNAME")%>--%>
                                                                                                      
                                                                            </td>
                                                                            <td style="text-align: center">

                                                                                <%# Eval("NEWBATCH")%>
                                                                                                      
                                                                            </td>
                                                                            <td style="text-align: center">

                                                                                <%# Eval("PROGRAM")%>
                                                                                <asp:HiddenField ID="hdfbranch" runat="server" Value='<%# Eval("BRANCHNO") %>' />
                                                                                <asp:HiddenField ID="hdfdegree" runat="server" Value='<%# Eval("DEGREENO") %>' />
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <%--  <Triggers>
                                     
                                     <asp:PostBackTrigger ControlID="getdate" />
                                 </Triggers>--%>
                                    </asp:UpdatePanel>
                                </div>

                                <div id="divexamcenter" runat="server" visible="false" role="tabpanel" aria-labelledby="IntakeExamCenter-tab">
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updexamallotment"
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

                                    <asp:UpdatePanel ID="updexamallotment" runat="server">
                                        <ContentTemplate>
                                            <div class="col-md-12 col-sm-12 col-12">
                                                <div class="sub-heading mt-3">
                                                    <h5>Exam Center Allotment</h5>
                                                </div>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblugpgforexam" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlugpgforexam" runat="server" TabIndex="2" CssClass="form-control select2 select-click"
                                                                    AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlugpgforexam_SelectedIndexChanged"
                                                                    ToolTip="Please Select Decepline">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlugpgforexam"
                                                                    Display="None" ErrorMessage="Please Select Decepline" InitialValue="0"
                                                                    ValidationGroup="saveexam" />
                                                                <asp:HiddenField ID="hdfexamdate" runat="server" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <asp:Label ID="lblexamdate" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <div id="getdateexam" class="form-control">
                                                                    <i class="fa fa-calendar"></i>&nbsp;
                                                            <span id="Datepickexam"></span>
                                                                    <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblCampusName" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlcenter" runat="server" TabIndex="2" CssClass="form-control select2 select-click"
                                                                    AppendDataBoundItems="True"
                                                                    ToolTip="Please Select Campus">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:LinkButton ID="btnsaveexam" runat="server" ToolTip="Submit"
                                                            CssClass="btn btn-outline-info" OnClick="btnsaveexam_Click" TabIndex="7" ValidationGroup="saveexam" Visible="false">Submit</asp:LinkButton>

                                                        <asp:Button ID="vtncancelexam" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                            TabIndex="8" CssClass="btn btn-outline-danger" OnClick="vtncancelexam_Click" Visible="false" />

                                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="saveexam"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                                    </div>

                                                    <div class="col-md-12" id="divexam" runat="server" visible="false">
                                                        <asp:Panel ID="Panel3" runat="server">
                                                            <asp:ListView ID="lvexamcenter" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Exam Center List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="mytable">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:CheckBox ID="chkEXAM" runat="server" OnClick="checkEXAM(this)" />
                                                                                </th>
                                                                                <th>SrNo </th>
                                                                                <th>Username</th>
                                                                                <th>Student Name</th>
                                                                                <th>Email</th>
                                                                                <th>Intake</th>
                                                                                <th>Exam Center</th>
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
                                                                            <asp:CheckBox ID="chkboxexamcenter" runat="server" ToolTip='<%# Eval("USERNO")%>' /></td>
                                                                        <td>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                                                        
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("USERNAME")%> 
                                                                        </td>
                                                                        <td>

                                                                            <%# Eval("NAME")%> 
                                                                        </td>
                                                                        <td>

                                                                            <%# Eval("EMAILID")%> 
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblintake" runat="server" Text='<%# Eval("BATCHNAME") %>' ToolTip='<%# Eval("ADMBATCH") %>'></asp:Label>
                                                                            <%--                  <asp:HiddenField ID="hdfbranch" runat="server" Value='<%# Eval("BRANCHNO") %>' />
                                                                                                         <asp:HiddenField ID="hdfdegree" runat="server" Value='<%# Eval("DEGREENO") %>' />--%>
                                                                            <%-- <%# Eval("BATCHNAME")%>--%>
                                                                                                      
                                                                        </td>
                                                                        <td>

                                                                            <%# Eval("APTITUDE_CENTER_NAME")%> 
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>




                                </div>

                                <div id="divhallticket" runat="server" visible="false" role="tabpanel" aria-labelledby="HallTicket-tab">
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updhallticket"
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

                                    <asp:UpdatePanel ID="updhallticket" runat="server">
                                        <ContentTemplate>
                                            <div class="col-md-12 col-sm-12 col-12">
                                                <div class="sub-heading mt-3">
                                                    <h5>Aptitute Hall Ticket</h5>
                                                </div>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblIntakeTicket" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlintaketicket" runat="server" TabIndex="2" CssClass="form-control select2 select-click"
                                                                    AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlintaketicket_SelectedIndexChanged"
                                                                    ToolTip="Please Select Intake">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlintaketicket"
                                                                    Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                                                    ValidationGroup="savehallticket" />

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblugpgforticket" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDesciplineticket" runat="server" TabIndex="2" CssClass="form-control select2 select-click"
                                                                    AppendDataBoundItems="True" OnSelectedIndexChanged="ddlDesciplineticket_SelectedIndexChanged" AutoPostBack="true"
                                                                    ToolTip="Please Select Campus">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlDesciplineticket"
                                                                    Display="None" ErrorMessage="Please Select descipline" InitialValue="0"
                                                                    ValidationGroup="savehallticket" />

                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:LinkButton ID="linksubmitHT" runat="server" ToolTip="Report"
                                                            CssClass="btn btn-outline-info" OnClick="linksubmitHT_Click" TabIndex="7" ValidationGroup="savehallticket" Visible="false">Report</asp:LinkButton>

                                                        <asp:Button ID="lnkClearHT" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                            TabIndex="8" CssClass="btn btn-outline-danger" OnClick="lnkClearHT_Click" Visible="false" />

                                                        <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="savehallticket"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        <asp:HiddenField ID="HiddenField5" runat="server" />
                                                    </div>

                                                    <div class="col-md-12" id="divlvhallticket" runat="server" visible="false">
                                                        <asp:Panel ID="Panel4" runat="server">
                                                            <asp:ListView ID="lvhallticket" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Aptitute Hall Ticket List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="mytable1">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th style="text-align: center">
                                                                                    <asp:CheckBox ID="chkhallticket" runat="server" OnClick="checkAllhallticket(this)" />
                                                                                </th>
                                                                                <th style="text-align: center">SrNo </th>
                                                                                <th style="text-align: center">Username</th>
                                                                                <th style="text-align: center">Student Name</th>
                                                                                <th style="text-align: center">Email</th>
                                                                                <th style="text-align: center">Mobile No</th>
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
                                                                            <asp:CheckBox ID="chkhallticket" runat="server" ToolTip='<%# Eval("USERNO")%>' />
                                                                            <asp:HiddenField ID="hfduserno" runat="server" Value='<%# Eval("USERNO")%>' />
                                                                        </td>

                                                                        <td style="text-align: center">
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                                                     
                                                                        </td>
                                                                        <td style="text-align: center">
                                                                            <%# Eval("USERNAME")%> 
                                                                        </td>
                                                                        <td style="text-align: center">

                                                                            <%# Eval("NAME")%> 
                                                                        </td>
                                                                        <td style="text-align: center">

                                                                            <%# Eval("EMAILID")%> 
                                                                        </td>

                                                                        <td style="text-align: center">

                                                                            <%# Eval("MOBILENO")%> 
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>

                                                        </asp:Panel>
                                                    </div>

                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="linksubmitHT" />
                                        </Triggers>
                                    </asp:UpdatePanel>




                                </div>
                                <div id="divapptitutetest" runat="server" visible="false" role="tabpanel" aria-labelledby="ApptituteTests-tab">
                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="updapptitutetest"
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
                                    <asp:UpdatePanel ID="updapptitutetest"  runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-12">
                                                    <div class="sub-heading mt-3">
                                                        <h5>Aptitude Test Mark Entry</h5>
                                                    </div>
                                                    <div class="box-body">
                                                        <div class="col-12">


                                                            <div class="col-md-12">
                                                                <div class="form-group col-md-8">
                                                                    <asp:RadioButtonList ID="rdbFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdbFilter_SelectedIndexChanged" RepeatColumns="8" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1"><span style="padding-left:5px">Download For Test Prep Data </span></asp:ListItem>
                                                                        <asp:ListItem Value="2"><span style="padding-left:5px">Exam Marks Upload From Test Prep </span></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>


                                                            <div class="row">
                                                                <div class="form-group col-md-5" runat="server" visible="false">
                                                                    <fieldset class="fieldset" style="text-align: center;">
                                                                        <legend class="legend">Download Format</legend>
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:LinkButton ID="lbExcelFormat" runat="server" OnClick="lbExcelFormat_Click1" TabIndex="1" Font-Underline="true"
                                                                                        ToolTip="Click Here For Downloading Sample Format" Style="font-weight: bold;" CssClass="stylink">
                                                                            <span style="color:green;">Pre-requisite excel format for upload</span></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </fieldset>
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="intake" visible="false" >
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblIntakeapt" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlintakeapti" runat="server" TabIndex="1" CssClass="form-control select2 select-click"
                                                                        AppendDataBoundItems="True"  OnSelectedIndexChanged="ddlintakeapti_SelectedIndexChanged" AutoPostBack="true"
                                                                        ToolTip="Please Select Intake">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlintakeapti"
                                                                        Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                                                        ValidationGroup="submitapti" />

                                                                </div>
                                                                 <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divintaketwo" visible="false" >
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblIntake" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlintakeprep" runat="server" TabIndex="1" CssClass="form-control select2 select-click"
                                                                        AppendDataBoundItems="True"  OnSelectedIndexChanged="ddlintakeprep_SelectedIndexChanged"  AutoPostBack="true"
                                                                        ToolTip="Please Select Intake">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlintakeprep"
                                                                        Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                                                        ValidationGroup="submitapti" />
                                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlintakeprep"
                                                                        Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                                                        ValidationGroup="confirmed" />

                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divfaculty"   visible="false" >
                                                                    <div class="label-dynamic">
                                                                        <%--<sup>* </sup>--%>
                                                                        <asp:Label ID="lblfaculty" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlfaculty" runat="server" TabIndex="2" CssClass="form-control select2 select-click"
                                                                        AppendDataBoundItems="True"  OnSelectedIndexChanged="ddlfaculty_SelectedIndexChanged" AutoPostBack="true"
                                                                        ToolTip="Please Select Faculty/School Name">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlfaculty"
                                                                        Display="None" ErrorMessage="Please Select Faculty/School Name" InitialValue="0"
                                                                        ValidationGroup="submitapti" />--%>

                                                                </div>
                                                                 <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divdesfirst"  visible="false"  >
                                                                    <div class="label-dynamic">
                                                                        <%--<sup>* </sup>--%>
                                                                        <asp:Label ID="lbldesc" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                       <asp:ListBox ID="ddldesrdiofir" runat="server" CssClass="form-control multi-select-demo"
                                                                      SelectionMode="Multiple" AppendDataBoundItems="true"></asp:ListBox>
                                                                   <%-- <asp:DropDownList ID="ddldesrdiofir" runat="server" TabIndex="2" CssClass="form-control select2 select-click"
                                                                        AppendDataBoundItems="True"  OnSelectedIndexChanged="ddldesrdiofir_SelectedIndexChanged" AutoPostBack="true"
                                                                        ToolTip="Please Select Descipline">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>--%>
                                                                  <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddldesrdiofir"
                                                                        Display="None" ErrorMessage="Please Select Discipline" InitialValue="0"
                                                                        ValidationGroup="submitapti" />--%>

                                                                </div>
                                                                 <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divMultiselectdate"  visible="false">
                                                                     <div class="label-dynamic">
                                                                            <sup> </sup>
                                                                            From/To Date Filter
                                                                      </div>
                                                                        <div id="pickerNew" class="form-control" tabindex="3">
                                                                            <i class="fa fa-calendar"></i>&nbsp;
                                                                            <span id="dateNew"></span>
                                                                            <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                                        </div>
                                                                     <asp:HiddenField ID="hdfGetFromDate" runat="server" />
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server"  id="divdesp" visible="false"  >
                                                                    <div class="label-dynamic">
                                                                      <%--  <sup>* </sup>--%>
                                                                        <asp:Label ID="lbldes" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                      <asp:ListBox ID="ddldes" runat="server" CssClass="form-control multi-select-demo"
                                                                      SelectionMode="Multiple" AppendDataBoundItems="true" AutoPostBack="true">
                                                                          <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                      </asp:ListBox>
                                                                   <%-- <asp:DropDownList ID="ddldes" runat="server" TabIndex="2" CssClass="form-control select2 select-click"
                                                                        AppendDataBoundItems="True" OnSelectedIndexChanged="ddldes_SelectedIndexChanged" AutoPostBack="true"
                                                                        ToolTip="Please Select Descipline">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>--%>
                                                                 <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddldes"
                                                                        Display="None" ErrorMessage="Please Select Discipline" InitialValue="0"
                                                                        ValidationGroup="submitapti" />--%>

                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divprogram"   visible="false" >
                                                                    <div class="label-dynamic">
                                                                        <%--<sup>* </sup>--%>
                                                                        <asp:Label ID="lblprogram" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlprgm" runat="server" TabIndex="2" CssClass="form-control select2 select-click"
                                                                        AppendDataBoundItems="True"  OnSelectedIndexChanged="ddlprgm_SelectedIndexChanged" AutoPostBack="true"
                                                                        ToolTip="Please Select Program">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlprgm"
                                                                        Display="None" ErrorMessage="Please Select Program" InitialValue="0"
                                                                        ValidationGroup="submitapti" />--%>

                                                                </div>


                                                                 <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divdegree"  visible="false">
                                                                    <div class="label-dynamic">
                                                                     <%--   <sup>* </sup>--%>
                                                                        <asp:Label ID="lblDYDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="2" CssClass="form-control select2 select-click"
                                                                        AppendDataBoundItems="True"  OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true"
                                                                        ToolTip="Please Select Degree">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator81" runat="server" ControlToValidate="ddlDegree"
                                                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0"
                                                                        ValidationGroup="submitapti" />--%>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="FileUpload" visible="false" >
                                                                    <sup>*</sup>
                                                                    <asp:Label ID="lblDYUploadExcelFile" runat="server" Font-Bold="true"></asp:Label>
                                                                    <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Please Select file to Import" TabIndex="2" />
                                                                    <asp:RequiredFieldValidator ID="rfvintake" runat="server" ControlToValidate="FileUpload1"
                                                                        Display="None" ErrorMessage="Please select file to upload." ValidationGroup="submitapti"
                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>   
                                                                   <%-- <asp:RegularExpressionValidator ID="revIntake" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx)$"
                                                                        ControlToValidate="FileUpload1" runat="server" ValidationGroup="submitapti" ErrorMessage="Please select a valid excel file."
                                                                        Display="None" SetFocusOnError="true" />--%>
                                                                </div>

                                                                <div class="form-group col-md-12">
                                                                    <asp:Label ID="lblTotalMsg" Style="font-weight: bold; color: red;" runat="server" TabIndex="3"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="box-footer">
                                                            <p class="text-center">

                                                                  <asp:Button ID="btnexcelformat" runat="server"  TabIndex="3"
                                                                    Text="PreFormat Data Excel Sheet" OnClick="btnexcelformat_Click"
                                                                    CssClass="btn btn-outline-info" ToolTip="Click Download PreFormat Data"   Visible="false"/>

                                                                <asp:Button ID="btnDownload" runat="server"  TabIndex="3"
                                                                    Text="TestPrep Data Excel Sheet" OnClick="btnDownload_Click" 
                                                                    CssClass="btn btn-outline-info" ToolTip="Click Download TestPrep Data"  ValidationGroup="submitapti"  Visible="false"/>

                                                                  <asp:Button ID="btnMarksPreData" runat="server"  TabIndex="4" Visible="false"
                                                                    Text="Download Excel" OnClick="btnMarksPreData_Click"  CssClass="btn btn-outline-info" ToolTip="Click Download Excel"/>

                                                                <asp:Button ID="btnUpload" runat="server" ValidationGroup="submitapti" TabIndex="3" Visible="false" 
                                                                    Text="Upload & Verify Data" OnClick="btnUpload_Click" CssClass="btn btn-outline-info" ToolTip="Click to Upload & Verify Data" />
                                                                <%-- <asp:Button ID="btnverify" runat="server" ValidationGroup="submitapti" TabIndex="3" Visible="false" 
                                                                    Text="Verify Data" OnClick="btnverify_Click"  CssClass="btn btn-outline-info" ToolTip="Click to Verify Data" />--%>
                                                                 <asp:Button ID="btnconfirmed" runat="server" ValidationGroup="confirmed" TabIndex="3" Visible="false" 
                                                                    Text="Confirmed" OnClick="btnconfirmed_Click"  CssClass="btn btn-outline-info" ToolTip="Click to Confirmed Data" />

                                                                <asp:Button ID="btncanceltest" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                                    TabIndex="4" OnClick="btncanceltest_Click" CssClass="btn btn-outline-danger" Visible="false" />
                                                                 <asp:ValidationSummary ID="validationsummary6" runat="server" ValidationGroup="confirmed" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                                  <p>
                                                                      &nbsp;<asp:ValidationSummary ID="validationsummary5" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submitapti" />
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                      <p>
                                                                      </p>
                                                                  </p>
                                                            </p>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <div id="divapti" runat="server" visible="false">
                                                                <asp:Panel ID="Panel5" runat="server">
                                                                    <asp:ListView ID="lvapti" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div>
                                                                                <div class="sub-heading">
                                                                                    <h5>Verify Excel Records</h5>
                                                                                </div>

                                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr id="trRow">
                                                                                            <th class="text-center">Sr.No. </th>
                                                                                            <th class="text-center">Exam Name </th>
                                                                                            <th class="text-center">SubjectName</th>
                                                                                            <th class="text-center">RollNo</th>
                                                                                            <th class="text-center">PRNNo </th>                                                                                  
                                                                                            <th class="text-center">CandidateName</th>
                                                                                            <th class="text-center">MobileNo</th>
                                                                                            <th class="text-center">MaxMarks</th>
                                                                                            <th class="text-center">MarksObtained</th>
                                                                                            <th class="text-center">ExamSubmitDate</th>
                                                                                            <th class="text-center">ExamStatus</th>
                                                                                            <th class="text-center">General</th>
                                                                                            <th class="text-center">English</th>
                                                                                        
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
                                                                                <td class="text-center"><%#Container.DataItemIndex+1 %></td>
                                                                            
                                                                                <td class="text-center"><%# Eval("EXAMNAME") %> </td>
                                                                                 <td class="text-center"><%# Eval("SUBJECTNAME") %> </td>
                                                                                <td class="text-center"><%# Eval("ROLLNO") %> </td>                                                                           
                                                                                <td class="text-center"><%# Eval("PRNNO") %></td>
                                                                                <td class="text-center"><%# Eval("NAME") %></td>
                                                                                <td class="text-center"><%# Eval("MOBILENO") %></td>
                                                                                <td class="text-center"><%# Eval("MAXMARKS") %></td>
                                                                                  <td class="text-center"><%# Eval("MARKSOBTAINED") %></td>
                                                                                 <td class="text-center"><%# Eval("EXAMSUBMITDATE") %></td>
                                                                                  <td class="text-center"><%# Eval("EXAMSTATUS") %></td>
                                                                                  <td class="text-center"><%# Eval("GENERAL") %></td>
                                                                                  <td class="text-center"><%# Eval("ENGLISH_MARKS") %></td>

                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>

                                                        </div>
                                                        <div class="col-md-12">
                                                            <div id="divuploadexcel" runat="server" visible="false">
                                                                <asp:Panel ID="Panel7" runat="server">
                                                                    <asp:ListView ID="lvuploexcel" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div>
                                                                                <div class="sub-heading">
                                                                                    <h5>Confirmed Excel Records</h5>
                                                                                </div>

                                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr id="trRow">
                                                                                            <th class="text-center">Sr.No. </th>
                                                                                            <th class="text-center">Exam Name </th>
                                                                                            <th class="text-center">SubjectName</th>
                                                                                            <th class="text-center">RollNo</th>
                                                                                            <th class="text-center">PRNNo </th>                                                                                  
                                                                                            <th class="text-center">CandidateName</th>
                                                                                            <th class="text-center">MobileNo</th>
                                                                                            <th class="text-center">MaxMarks</th>
                                                                                            <th class="text-center">MarksObtained</th>
                                                                                            <th class="text-center">ExamSubmitDate</th>
                                                                                            <th class="text-center">ExamStatus</th>
                                                                                            <th class="text-center">General</th>
                                                                                            <th class="text-center">English</th>
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
                                                                                <td class="text-center"><%#Container.DataItemIndex+1 %></td>
                                                                            
                                                                                <td class="text-center"><%# Eval("EXAMNAME") %> </td>
                                                                                 <td class="text-center"><%# Eval("SUBJECTNAME") %> </td>
                                                                                <td class="text-center"><%# Eval("ROLLNO") %> </td>                                                                           
                                                                                <td class="text-center"><%# Eval("PRNNO") %></td>
                                                                                <td class="text-center"><%# Eval("NAME") %></td>
                                                                                <td class="text-center"><%# Eval("MOBILENO") %></td>
                                                                                <td class="text-center"><%# Eval("MAXMARKS") %></td>
                                                                                  <td class="text-center"><%# Eval("MARKSOBTAINED") %></td>
                                                                                 <td class="text-center"><%# Eval("EXAMSUBMITDATE") %></td>
                                                                                  <td class="text-center"><%# Eval("EXAMSTATUS") %></td>
                                                                                  <td class="text-center"><%# Eval("GENERAL") %></td>
                                                                                  <td class="text-center"><%# Eval("ENGLISH_MARKS") %></td>

                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>

                                                        </div>
                                                        <div class="col-md-12">
                                                            <div id="divtestprep" runat="server" visible="false">
                                                                <asp:Panel ID="Panel6" runat="server">
                                                                    <asp:ListView ID="lvtestprep" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div>
                                                                                <div class="sub-heading">
                                                                                    <h5>Testprep Data List</h5>
                                                                                </div>

                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="my-Table">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr id="trRow">
                                                                                            <th class="text-center">Sr.No. </th>
                                                                                            <th class="text-center">FirstName </th>
                                                                                            <th class="text-center">MiddleName</th>
                                                                                            <th class="text-center">LastName </th>                                                                                  
                                                                                            <th class="text-center">RollNo</th>
                                                                                            <th class="text-center">MobileNo</th>
                                                                                            <th class="text-center">EmailID</th>
                                                                                            <th class="text-center">Gender</th>
                                                                                            <th class="text-center">PRNNo </th>
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
                                                                                <td class="text-center"><%#Container.DataItemIndex+1 %></td>
                                                                            
                                                                                <td class="text-center"><%# Eval("FirstName") %> </td>
                                                                                 <td class="text-center"><%# Eval("MiddleName") %> </td>
                                                                                <td class="text-center"><%# Eval("LastName") %> </td>                                                                           
                                                                                <td class="text-center"><%# Eval("RollNo") %></td>
                                                                                <td class="text-center"><%# Eval("MobileNo") %></td>
                                                                                <td class="text-center"><%# Eval("EmailID") %></td>
                                                                                <td class="text-center"><%# Eval("Gender") %></td>
                                                                                 <td class="text-center"><%# Eval("PRNNo") %></td>

                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lbExcelFormat" />
                                            <asp:PostBackTrigger ControlID="btnUpload" />
                                            <asp:PostBackTrigger ControlID="btnDownload" />
                                            <asp:PostBackTrigger ControlID="btnMarksPreData" />
                                            <asp:PostBackTrigger ControlID="btnexcelformat" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
      <script type="text/javascript">
          $(document).ready(function () {
              $('.multi-select-demo').multiselect({
                  includeSelectAllOption: true,
                  maxHeight: 200,
                  enableFiltering: true,
                  filterPlaceholder: 'Search'
              });
          });
          var parameter = Sys.WebForms.PageRequestManager.getInstance();
          parameter.add_endRequest(function () {
              $('.multi-select-demo').multiselect({
                  includeSelectAllOption: true,
                  maxHeight: 200,
                  enableFiltering: true,
                  filterPlaceholder: 'Search'
              });
          });

    </script>
    <script type="text/javascript">
        function topFunction() {
            document.body.scrollTop = 0;
            document.documentElement.scrollTop = 0;
        }
    </script>
    <script>
        $(document).ready(function () {
            (function ($) {
                "use strict";
                $('.label.ui.dropdown')
             .dropdown();
                $('.no.label.ui.dropdown')
                  .dropdown({
                      useLabels: false
                  });
                $('.ui.button').on('click', function () {
                    $('.ui.dropdown')
                      .dropdown('restore defaults')
                })
            })(jQuery);

            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () { 
                "use strict";
                $('.label.ui.dropdown')
             .dropdown();
                $('.no.label.ui.dropdown')
                  .dropdown({
                      useLabels: false
                  });
                $('.ui.button').on('click', function () {
                    $('.ui.dropdown')
                      .dropdown('restore defaults')
                })
            })(jQuery);
        });
    </script>
    <script>
        //$("#close-sidebar").click(function() {
        //  $(".page-wrapper").toggleClass("toggled");
        //});
        $("#showright-sidebar").click(function () {
            // alert('hi');
            $(".pageright-wrapper").toggleClass("toggleed");
            // $(".btnsidebar").toggleClass('rotated')

        });
        var prm2 = Sys.WebForms.PageRequestManager.getInstance();
        prm2.add_endRequest(function () {
            $("#showright-sidebar").click(function () {
                // alert('hi');
                $(".pageright-wrapper").toggleClass("toggleed");
                //  $(".btnsidebar").toggleClass('rotated')
            });
        });
        //   $(".filter-text").hide();
        $("#filter-toggle").click(function () {
            $(".filter-text").toggle();
            $(".input-filter").addClass('inputfilter')

        });
        var prm3 = Sys.WebForms.PageRequestManager.getInstance();
        prm3.add_endRequest(function () {
            $("#filter-toggle").click(function () {
                $(".filter-text").toggle();
                $(".input-filter").addClass('inputfilter')

            });
        });

    </script>
    <script>
        function CmbChange(obj) {
            var cmbValue = document.getElementById("slMainLeadLabel").value;
            __doPostBack('slMainLeadLabel', cmbValue);
        }
    </script>
    <script>
        function CmbChange(obj) {
            var cmbValue = document.getElementById("slMainLeadLabel").value;
            __doPostBack('slMainLeadLabel', cmbValue);
        }

    </script>
    <script>
        $(document).ready(function () {
            var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlMainLeadLabel option[selected="selected"]').length;
            if (check_panel_state > 0) {
                $(".pageright-wrapper").addClass("toggleed");
            }
            else {
                $(".pageright-wrapper").removeClass("toggleed");
            }
        });

        varprm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (sender, e) {
            var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlMainLeadLabel option[selected="selected"]').length;
            if (check_panel_state > 0) {
                $(".pageright-wrapper").addClass("toggleed");
            }
            else {
                $(".pageright-wrapper").removeClass("toggleed");
            }
        });
    </script>
        <script type="text/javascript">
            $(document).ready(function () {
                $('#pickerNew').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    ranges: {
                    },
                },
            function (start, end) {
                $('#dateNew').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                document.getElementById('<%=hdfGetFromDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
        });

                //$('#dateNew').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            //document.getElementById('<%=hdfGetFromDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#pickerNew').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    ranges: {
                    },
                },
            function (start, end) {
                debugger
                $('#dateNew').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                document.getElementById('<%=hdfGetFromDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                //$('#dateNew').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
                //document.getElementById('<%=hdfGetFromDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            });
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
             var table5 = document.querySelector('#FirstTab');

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

