<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Change_Section.aspx.cs" Inherits="MockUps_Change_Section" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        #ctl00_ContentPlaceHolder1_pnlBlock .dataTables_scrollHeadInner, #ctl00_ContentPlaceHolder1_Panel2 .dataTables_scrollHeadInner {
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
                            var arr =  [0];
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
                                            var arr =   [0];
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
                                            var arr =   [0];
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
                                var arr =   [0];
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
                                                var arr =   [0];
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
                                                var arr =   [0];
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
                                                var arr =   [0];
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
        });
    </script>    
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdChangeSe"
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
    <asp:UpdatePanel runat="server" ID="UpdChangeSe">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div id="Tabs" role="tabpanel">
                            <div class="box-body">
                                <div class="nav-tabs-custom">
                                    <ul class="nav nav-tabs" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active" data-toggle="tab" href="#tab_1">Block Section</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_2">Subject Wise</a>
                                        </li>
                                    </ul>
                                    <div class="tab-content" id="my-tab-content">
                                        <div class="tab-pane active" id="tab_1">
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                        <div class="form-group">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblRASession" runat="server" Font-Bold="true"></asp:Label>
                                                                <%--                      <label><span>Academic Session</span></label>--%>
                                                            </div>
                                                            <asp:DropDownList ID="ddlAcademicSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                ControlToValidate="ddlAcademicSession" Display="None" SetFocusOnError="true"
                                                                ErrorMessage="Please Select Academic Session" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                        <div class="form-group">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                                                <%-- <label><span>College</span></label>--%>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                ControlToValidate="ddlCollege" Display="None" SetFocusOnError="true"
                                                                ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                        <div class="form-group">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblDyCenter" runat="server" Font-Bold="true"></asp:Label>
                                                                <%--   <label><span>Campus</span></label>--%>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCampus" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                ControlToValidate="ddlCampus" Display="None" SetFocusOnError="true"
                                                                ErrorMessage="Please Select Campus" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                        <div class="form-group">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblDYScheme" runat="server" Font-Bold="true"></asp:Label>
                                                                <%--  <label><span>Curriculum</span></label>--%>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCurriculum" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCurriculum_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                                ControlToValidate="ddlCurriculum" Display="None" SetFocusOnError="true"
                                                                ErrorMessage="Please Select Curriculum" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                        <div class="form-group">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                <%--   <label><span>Semester</span></label>--%>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSemester" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                                ControlToValidate="ddlSemester" Display="None" SetFocusOnError="true"
                                                                ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                        <div class="form-group">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <asp:Label ID="lblBlockSection" runat="server" Font-Bold="true"></asp:Label>
                                                                <%-- <label><span>Block Section</span></label>--%>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSection" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- Buttons -->
                                                <div class="text-center mt-2 mb-3">
                                                    <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-outline-primary" ValidationGroup="Show" OnClick="btnShow_Click" TabIndex="1">Show</asp:LinkButton>
                                                    <asp:LinkButton ID="btnSubmit" Visible="false" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSubmit_Click" TabIndex="1">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" TabIndex="1">Cancel</asp:LinkButton>
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Show"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <asp:Panel ID="pnlBlock" runat="server" Visible="false">
                                                    <asp:ListView ID="lvBlockSection" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Block Section List</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" id="mytable" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th style="text-align: center">
                                                                            <asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" />
                                                                        </th>
                                                                        <th>Student ID</th>
                                                                        <th>Student Name</th>
                                                                        <th>College</th>
                                                                        <th>Program</th>
                                                                        <th>Block Section</th>
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
                                                                    <asp:CheckBox ID="chkSection" runat="server" ToolTip='<%#Eval("IDNO")%>' />
                                                                    <asp:HiddenField ID="hdfsemesterno" runat="server" Value='<%#Eval("SEMESTERNO")%>' />
                                                                    <asp:HiddenField ID="hdfsessionno" runat="server" Value='<%#Eval("SESSIONNO")%>' />
                                                                    <asp:HiddenField ID="hdfsectionno" runat="server" Value='<%#Eval("SECTIONNO")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblStudentId" runat="server" Text='<%#Eval("ENROLLNO")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblStudentName" runat="server" Text='<%#Eval("STUDNAME")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCollege" runat="server" Text='<%#Eval("COLLEGE_NAME")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblProgram" runat="server" Text='<%#Eval("PROGRAM")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblBlockSection" runat="server" Text='<%#Eval("SECTIONNAME")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>

                                        <div class="tab-pane fade" id="tab_2">
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                        <div class="form-group">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblPAAcadSession" runat="server" Font-Bold="true"></asp:Label>
                                                                <%--      <label><span>Academic Session</span></label>--%>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSessionSub" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                                ControlToValidate="ddlSessionSub" Display="None" SetFocusOnError="true"
                                                                ErrorMessage="Please Select Academic Session" InitialValue="0" ValidationGroup="ShowSub"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                        <div class="form-group">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblFacultyName" runat="server" Font-Bold="true"></asp:Label>
                                                                <%--  <label><span>College</span></label>--%>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCollegeSub" OnSelectedIndexChanged="ddlCollegeSub_SelectedIndexChanged" AutoPostBack="true" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                                ControlToValidate="ddlCollegeSub" Display="None" SetFocusOnError="true"
                                                                ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="ShowSub"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                        <div class="form-group">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblCampus" runat="server" Font-Bold="true"></asp:Label>
                                                                <%-- <label><span>Campus</span></label>--%>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCampusSub" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                                ControlToValidate="ddlCampusSub" Display="None" SetFocusOnError="true"
                                                                ErrorMessage="Please Select Campus" InitialValue="0" ValidationGroup="ShowSub"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                        <div class="form-group">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblDYScheme1" runat="server" Font-Bold="true"></asp:Label>
                                                                <%--  <label><span>Curriculum</span></label>--%>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCurriculumSub" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCurriculumSub_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                                ControlToValidate="ddlCurriculumSub" Display="None" SetFocusOnError="true"
                                                                ErrorMessage="Please Select Curriculum" InitialValue="0" ValidationGroup="ShowSub"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>                                                   
                                                    <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                        <div class="form-group">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblParentSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                <%-- <label><span>Semester</span></label>--%>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSemSub" OnSelectedIndexChanged="ddlSemSub_SelectedIndexChanged" AutoPostBack="true" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                                ControlToValidate="ddlSemSub" Display="None" SetFocusOnError="true"
                                                                ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="ShowSub"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                        <div class="form-group">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblDYSubjectCode" runat="server" Font-Bold="true"></asp:Label>
                                                                <%--  <label><span>Subject</span></label>--%>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSubject" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="0" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                                                ControlToValidate="ddlSubject" Display="None" SetFocusOnError="true"
                                                                ErrorMessage="Please Select Subject" InitialValue="0" ValidationGroup="ShowSub"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                        <div class="form-group">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <asp:Label ID="lblBlockSection1" runat="server" Font-Bold="true"></asp:Label>
                                                                <%-- <label><span>Block Section</span></label>--%>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSectionSub" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- Buttons -->
                                                <div class="text-center mt-2 mb-3">
                                                    <asp:LinkButton ID="btnShowSub" runat="server" CssClass="btn btn-outline-primary" OnClick="btnShowSub_Click" ValidationGroup="ShowSub" TabIndex="1">Show</asp:LinkButton>
                                                    <asp:LinkButton ID="btnSubmitSub" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSubmitSub_Click" Visible="false" TabIndex="1">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnClearSub" runat="server" CssClass="btn btn-outline-danger" OnClick="btnClearSub_Click" TabIndex="1">Cancel</asp:LinkButton>
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ShowSub"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>
                                            </div>


                                            <div class="col-12">
                                                <asp:Panel ID="Panel2" runat="server" Visible="false">
                                                    <asp:ListView ID="LvSubSection" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Subject Wise Section List</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" id="mytable1" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th style="text-align: center">
                                                                            <asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckboxSub(this)" />
                                                                        </th>
                                                                        <th>Student ID</th>
                                                                        <th>Student Name</th>
                                                                        <th>College</th>
                                                                        <th>Program</th>
                                                                        <th>Block Section</th>
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
                                                                    <asp:CheckBox ID="chkSection" runat="server" ToolTip='<%#Eval("IDNO")%>' />
                                                                    <asp:HiddenField ID="hdfsemesterno" runat="server" Value='<%#Eval("SEMESTERNO")%>' />
                                                                    <asp:HiddenField ID="hdfsessionno" runat="server" Value='<%#Eval("SESSIONNO")%>' />
                                                                    <asp:HiddenField ID="hdfsectionno" runat="server" Value='<%#Eval("SECTIONNO")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblStudentId" runat="server" Text='<%#Eval("ENROLLNO")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblStudentName" runat="server" Text='<%#Eval("STUDNAME")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCollege" runat="server" Text='<%#Eval("COLLEGE_NAME")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblProgram" runat="server" Text='<%#Eval("PROGRAM")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblBlockSection" runat="server" Text='<%#Eval("SECTIONNAME")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        function checkAllCheckboxSub(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$LvSubSection$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$LvSubSection$ctrl';
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
    <script type="text/javascript" language="javascript">
        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvBlockSection$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvBlockSection$ctrl';
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
        function TabShow() {
            var tabName = "tab_2";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        }
    </script>
</asp:Content>

