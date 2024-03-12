<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ALCourseList.aspx.cs" Inherits="ACADEMIC_ALCourseList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <%--===== Data Table Script added by gaurav =====--%>
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
                            var arr = [0, 3];
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
                                            var arr = [0, 3];
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
                                            var arr = [0, 3];
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
                                            var arr = [0, 3];
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
                                var arr = [0,3];
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
                                                var arr = [0,3];
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
                                                var arr = [0,3];
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
                                                var arr = [0,3];
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

    <%--===== Data Table Script added by gaurav =====--%>
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
                            var arr = [0,4];
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
                                            var arr = [0,4];
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
                                            var arr = [0,4];
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
                                            var arr = [0,4];
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
                                var arr = [0, 4];
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
                                                var arr = [0, 4];
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
                                                var arr = [0, 4];
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
                                                var arr = [0, 4];
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
                <div class="nav-tabs-custom mt-2 col-12" id="myTabContent">
                   <%-- <ul class="nav nav-tabs" role="tablist" id="myTab">
                        <li class="nav-item">
                            <a class="nav-link active" id="ALCourses-tab" data-toggle="tab" href="#ALCourses" tabindex="1">ALCourses</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="Grade-tab" data-toggle="tab" href="#Grade" tabindex="2">Grades</a>
                        </li>
                    </ul>--%>
                    <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                       <li class="nav-item active" id="divlkAnnouncement" runat="server"><asp:LinkButton ID="lkAnnouncement" runat="server" OnClick="lkAnnouncement_Click" CssClass="nav-link" TabIndex="1">ALCourses</asp:LinkButton></li>
                       <li class="nav-item" id="divlkFeed" runat="server"><asp:LinkButton ID="lkFeedback" runat="server" OnClick="lkFeedback_Click" CssClass="nav-link" TabIndex="2">Grades</asp:LinkButton></li>
                    </ul>
                    <div class="tab-content">
                        <%-- <div class="tab-pane fade show active" id="ALCourses" role="tabpanel" aria-labelledby="ALCourses-tab">--%>
                       <div class="tab-pane fade show active" id="divAnnounce" role="tabpanel" runat="server" aria-labelledby="ALCourses-tab">
                        <%--<div class="box box-info" id="divAnnounce" runat="server">--%>
                            <div>
                                <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updALCourse"
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
   
                            <div class="box-body">
                                <asp:UpdatePanel ID="updALCourse" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-12 pl-0 pr-0">
                                                <div class="box-header with-border">
                                                    <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>                              
                                                </div>
                                                 <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12 offset-lg-4 offset-md-3">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblDYAlTypeCourse" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlAlCourse" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlAlCourse_SelectedIndexChanged">
                                                                    <asp:ListItem>Please Select </asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                
                                                    <div class="col-12 btn-footer" id="divSubmit" runat="server" visible="false">
                                                        <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" TabIndex="2" OnClientClick="return validate()" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" TabIndex="3" OnClick="btnCancel_Click">Clear</asp:LinkButton>
                                                    </div>

                                                    <div class="col-12 pl-0 pr-0" >
                                                        <asp:Panel ID="pnllst" runat="server">
                                                            <asp:ListView ID="lvALCourse" runat="server">                                        
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>A/L Course Details</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap" id="mytable1" style="width:100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th style="text-align:center">SrNo
                                                                                </th>   
                                                                                <th style="text-align:center">Course Name
                                                                                </th>           
                                                                                <th style="text-align:center">Short Name
                                                                                </th>
                                                                                <th style="text-align:center">Action
                                                                                </th> 
                                                                                <th></th>                
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td style="text-align:center">
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                            <asp:HiddenField  ID="hfdvalue" runat="server" Value="0"/>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtCourseName" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                                <asp:TextBox ID="txtShortName" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                                                        </td>
                                                                        <td style="text-align:center">
                                                                                <asp:CheckBox ID="chlactive" runat="server" />
                                                                        </td>
                                                                        <td style="text-align:center">
                                                                                <asp:Button ID="btnRemove" runat="server" CssClass="btn btn-outline-danger" Text="Remove" OnClick="btnRemove_Click" CommandArgument='<%#Eval("ID")%>' CommandName='<%# Container.DataItemIndex + 1 %>'/>
                                                                        </td>
                                                                    </tr>
                                           
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                            <div class="row mt-4" id="DivAdd" runat="server" visible="false">
                                                                <div class="form-group col-12 text-center">
                                                                    <asp:Button ID="btnAddCourse" runat="server" CssClass="btn btn-outline-info" Text="Add New Courses" OnClick="btnAddCourse_Click"/>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                       <div id="divEmoji" runat="server" visible="false" role="tabpanel" aria-labelledby="Grade-tab" >
                         <%-- <div class="tab-pane fade" id="Grade" role="tabpanel" aria-labelledby="Grade-tab">--%>
                      
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updGrades"
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
                            <div class="box-body">
                                <asp:UpdatePanel ID="updGrades" runat="server">
                                    <ContentTemplate>
                                       
                                        <div class="row">
                                            <div class="col-12">
                                               <div class="box-header with-border">
                                                        <h3 class="box-title">Grade Details</h3>                             
                                                    </div>
                                                       <div class="box-body">
                                                          <div class="col-12">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12 offset-lg-4 offset-md-3">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="lblDYGrade" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlGrade" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged" >
                                                                            <asp:ListItem>Please Select </asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                     <div class="col-12 btn-footer" id="divgradesbuttons" runat="server" visible="false">
                                                            <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="btn btn-outline-info" TabIndex="2"   OnClick="lnkSubmit_Click" >Submit</asp:LinkButton>
                                                            <asp:LinkButton ID="lnkClear" runat="server" CssClass="btn btn-outline-danger" TabIndex="3" OnClick="lnkClear_Click" >Clear</asp:LinkButton>
                                                            <asp:HiddenField ID="TabName" runat="server" Value="image" />
                                                        </div>
                                                     <div class="col-md-12">
                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <asp:ListView ID="lvGrades" runat="server" >                                        
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Grades</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap" id="mytable" style="width:100%">
                                                                            <thead class="bg-light-blue" >
                                                                                <tr>
                                                                                    <th style="text-align:center">SrNo
                                                                                    </th>   
                                                                                    <th style="text-align:center">Grades
                                                                                    </th>           
                                                                                    <th style="text-align:center">
                                                                                        Active
                                                                                    </th>
                                                                                    <th></th>    
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td style="text-align:center">
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                                <asp:HiddenField  ID="hfdValue" runat="server" Value="0"/>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtGrades" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                                                                            </td>
                                                                            <td style="text-align:center">
                                                                                <asp:CheckBox ID="chkActiveGrade" runat="server" />
                                                                            </td>
                                                                            <td style="text-align:center">
                                                                                <asp:Button ID="btnRemoveGrades" runat="server" CssClass="btn btn-outline-danger" Text="Remove"   OnClick="btnRemoveGrades_Click" CommandArgument='<%#Eval("ID")%>' CommandName='<%# Container.DataItemIndex + 1 %>'/>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>

                                                                <div class="row mt-4" id="DivAddgrades" runat="server" visible="false">
                                                                    <div class="form-group col-12 text-center">
                                                                        <asp:Button ID="btnaddGrades" runat="server" CssClass="btn btn-outline-info" Text="Add New Grades" OnClick="btnaddGrades_Click"  />
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                          </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
         function validate() {

            var rfvALCourse = (document.getElementById("<%=lblDYAlTypeCourse.ClientID%>").innerHTML);
            var alertmsg = '';

            if (document.getElementById("<%=ddlAlCourse.ClientID%>").value == "0") {
                if (alertmsg == '') {
                    document.getElementById("<%=ddlAlCourse.ClientID%>").focus();
                }
                alertmsg = 'Please Select ' + rfvAdmbatch + ' \n';

            }
            if (alertmsg != '') {
                alert(alertmsg);
                HideLoader('Submit');
                return false;
            }

            return true;
        }
    </script>
    <script type="text/javascript">

        function Grades() {

            var rfvGrades = (document.getElementById("<%=lblDYGrade.ClientID%>").innerHTML);
             var alertgrades = '';

             if (document.getElementById("<%=lvGrades.ClientID%>").value == "0") {
                if (alertmsg == '') {
                    document.getElementById("<%=lvGrades.ClientID%>").focus();
                }
                 alertgrades = 'Please Select ' + rfvAdmbatch + ' \n';

            }
            if (alertgrades != '') {
                alert(alertgrades);
                HideLoader('Submit');
                return false;
            }

            return true;
        }
    </script>
    <%--<script src="/Scripts/bootstrap-tabs.js" type="text/javascript"></script>--%>
    <%--<script>
    $(function () {
        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
           
            var activeTab = $(e.target).text();
            var previousTab = $(e.relatedTarget).text();
            $(".tab-pane fade show active").html(activeTab);
            $(".tab-pane fade").html(previousTab);
        });
    });
   </script>--%>
    <%--<script>
        $('#myTab a').click(function (e) {
            e.preventDefault()
            $(this).tab('show')
        })
        </script>--%>
</asp:Content>

