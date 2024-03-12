<%@ Page Language="C#" Title="" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BulkReconcilation.aspx.cs" Inherits="ACADEMIC_BulkReconcilation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        $(document).ready(function () {
            var table = $('#table1').DataTable({
                responsive: true,
                lengthChange: true,
                //scrollY: 320,
                //scrollX: true,
                //scrollCollapse: true,
                paging: false,
                dom: 'lBfrtip',



                //Export functionality
                buttons: [
                {
                    extend: 'colvis',
                    text: 'Column Visibility',
                    columns: function (idx, data, node) {
                        var arr = [];
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
                                var arr = [];
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
                                var arr = [];
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
                                var arr = [];
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
            $('#ctl00_ContentPlaceHolder1_LvTempRec_cbHead').on('click', function () {
                // Get all rows with search applied
                var rows = table.rows({ 'search': 'applied' }).nodes();
                // Check/uncheck checkboxes for all rows in the table
                $('input[type="checkbox"]', rows).prop('checked', this.checked);
            });







            // Handle click on checkbox to set state of "Select all" control
            $('#table1 tbody').on('change', 'input[type="checkbox"]', function () {
                // If checkbox is not checked
                if (!this.checked) {
                    var el = $('#ctl00_ContentPlaceHolder1_LvTempRec_cbHead').get(0);
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
                    //scrollY: 320,
                    //scrollX: true,
                    //scrollCollapse: true,
                    paging: false,
                    dom: 'lBfrtip',



                    //Export functionality
                    buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [];
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
                                    var arr = [];
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
                                    var arr = [];
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
                                    var arr = [];
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
                $('#ctl00_ContentPlaceHolder1_LvTempRec_cbHead').on('click', function () {
                    // Get all rows with search applied
                    var rows = table.rows({ 'search': 'applied' }).nodes();
                    // Check/uncheck checkboxes for all rows in the table
                    $('input[type="checkbox"]', rows).prop('checked', this.checked);
                });







                // Handle click on checkbox to set state of "Select all" control
                $('#table1 tbody').on('change', 'input[type="checkbox"]', function () {
                    // If checkbox is not checked
                    if (!this.checked) {
                        var el = $('#ctl00_ContentPlaceHolder1_LvTempRec_cbHead').get(0);
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
        <script>
            $(document).ready(function () {
                var table = $('#tableSem').DataTable({
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
                            var arr = [];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tableSem').DataTable().column(idx).visible();
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
                                        return $('#tableSem').DataTable().column(idx).visible();
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
                                    var arr = [];
                                    if (arr.indexOf(idx) !== -1) {
                                        return false;
                                    } else {
                                        return $('#tableSem').DataTable().column(idx).visible();
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
                                    var arr = [];
                                    if (arr.indexOf(idx) !== -1) {
                                        return false;
                                    } else {
                                        return $('#tableSem').DataTable().column(idx).visible();
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
                $('#ctl00_ContentPlaceHolder1_LvTempRec_cbHead').on('click', function () {
                    // Get all rows with search applied
                    var rows = table.rows({ 'search': 'applied' }).nodes();
                    // Check/uncheck checkboxes for all rows in the table
                    $('input[type="checkbox"]', rows).prop('checked', this.checked);
                });







                // Handle click on checkbox to set state of "Select all" control
                $('#tableSem tbody').on('change', 'input[type="checkbox"]', function () {
                    // If checkbox is not checked
                    if (!this.checked) {
                        var el = $('#ctl00_ContentPlaceHolder1_LvTempRec_cbHead').get(0);
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
                    var table = $('#tableSem').DataTable({
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
                                var arr = [];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tableSem').DataTable().column(idx).visible();
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
                                            return $('#tableSem').DataTable().column(idx).visible();
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
                                        var arr = [];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tableSem').DataTable().column(idx).visible();
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
                                        var arr = [];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tableSem').DataTable().column(idx).visible();
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
                    $('#ctl00_ContentPlaceHolder1_LvTempRec_cbHead').on('click', function () {
                        // Get all rows with search applied
                        var rows = table.rows({ 'search': 'applied' }).nodes();
                        // Check/uncheck checkboxes for all rows in the table
                        $('input[type="checkbox"]', rows).prop('checked', this.checked);
                    });







                    // Handle click on checkbox to set state of "Select all" control
                    $('#tableSem tbody').on('change', 'input[type="checkbox"]', function () {
                        // If checkbox is not checked
                        if (!this.checked) {
                            var el = $('#ctl00_ContentPlaceHolder1_LvTempRec_cbHead').get(0);
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
    <style>
        .logoContainer1 img {
            width: 45px;
            height: 45px;
        }
    </style>
      <style>
        .logoContainer2 img {
            width: 45px;
            height: 45px;
        }
    </style>
    <style>
        #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner, #ctl00_ContentPlaceHolder1_Panel6 .dataTables_scrollHeadInner,#ctl00_ContentPlaceHolder1_Panel4 .dataTables_scrollHeadInner{
            width: max-content !important;
        }
    </style>


    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updbulreco"
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
    <asp:UpdatePanel ID="updbulreco" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group col-md-12">
                                            <asp:RadioButtonList ID="rdbFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdbFilter_SelectedIndexChanged" RepeatColumns="8" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1"><span style="padding-left:5px">Application</span></asp:ListItem>
                                                <asp:ListItem Value="2"><span style="padding-left:5px">Enrollment</span></asp:ListItem>
                                                <asp:ListItem Value="3"><span style="padding-left:5px">Semester Registration</span></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="intakeone" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlintake" runat="server" TabIndex="1" CssClass="form-control select2 select-click"
                                            AppendDataBoundItems="True" OnSelectedIndexChanged="ddlintake_SelectedIndexChanged" AutoPostBack="true"
                                            ToolTip="Please Select Session Long Name">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlintake"
                                            Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                            ValidationGroup="Submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="FileUpload" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYUploadExcelFile" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div id="Div1" class="logoContainer1" runat="server">
                                            <img src="../IMAGES/excel.png" alt="upload image" runat="server" id="imgUpFile" tabindex="2" />
                                        </div>
                                        <div class="fileContainer sprite pl-1">
                                            <span runat="server" id="ufFile"
                                                cssclass="form-control" tabindex="7">Upload File</span>
                                            <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Select file to upload"
                                                CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfvintake" runat="server" ControlToValidate="FileUpload1"
                                                Display="None" ErrorMessage="Please select file to upload." ValidationGroup="Submit"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Label ID="lblTotalMsg" Style="font-weight: bold; color: red;" runat="server" TabIndex="3"></asp:Label>
                                <asp:HiddenField ID="hdfAdmission" runat="server" Value="0" />
                            </div>
                            <div class="col-12 btn-footer" id="buttons" runat="server" visible="false">
                                <asp:Button ID="btnDownload" runat="server" TabIndex="3"
                                    Text="Click To Download Pre-Requisite Format" OnClick="btnDownload_Click"
                                    CssClass="btn btn-outline-info" ToolTip="Click To Download Pre-Requisite Format" Visible="false" />
                                <asp:Button ID="btnUpload" runat="server" ValidationGroup="Submit" TabIndex="4"
                                    Text="Upload and Verify" CssClass="btn btn-outline-primary" ToolTip="Click to Upload  & Verify" OnClick="btnUpload_Click" />
                                <asp:Button ID="btnConfirmed" runat="server" TabIndex="4"
                                    Text="Confirm" CssClass="btn btn-outline-primary" ToolTip="Confirm" OnClick="btnConfirmed_Click" Visible="false" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    TabIndex="5" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" />
                                <asp:ValidationSummary ID="validationsummary3" runat="server" ValidationGroup="Submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div class="col-md-12" runat="server" id="uploadapp" visible="false">
                                <asp:Panel ID="Panel2" runat="server">
                                    <asp:ListView ID="LvTempRec" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Uploded List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="table1">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" />
                                                        </th>
                                                        <th>Application ID</th>
                                                        <th>Student Name</th>
                                                        <th>Date</th>
                                                        <%--<th>semester</th>--%>
                                                        <th>Amount</th>
                                                        <th>BankName</th>
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
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chktransfer" runat="server" ToolTip='<%# Eval("TEMP_USERNAME")%>' Enabled='<%# Eval("USERNAME").ToString() == string.Empty ? false : true%>'  />
                                                    <asp:HiddenField ID="hdfname" runat="server"  Value='<%# Eval("NAME")%>' />
                                                    <asp:HiddenField ID="hdfusername" runat="server"  Value='<%# Eval("TEMP_USERNAME")%>' />
                                                    <asp:HiddenField ID="hdfprogram" runat="server"  Value='<%# Eval("PROGRAM")%>' />
                                                    <asp:HiddenField ID="hdfbatchname" runat="server"  Value='<%# Eval("BATCHNAME")%>' />
                                                    <asp:HiddenField ID="hdffeestype" runat="server"  Value='<%# Eval("FEES_TYPE")%>' />
                                                    <asp:HiddenField ID="hdfamount" runat="server"  Value='<%# Eval("AMOUNT")%>' />
                                                    <asp:HiddenField ID="HiddenFhdfdate" runat="server"  Value='<%# Eval("TRANSACTION_DATE")%>' />
                                                    <asp:HiddenField ID="hdfuserno" runat="server"  Value='<%# Eval("USERNO")%>' />
                                                    <asp:HiddenField ID="hdfemailid" runat="server"  Value='<%# Eval("EMAILID")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("USERNAME")%>' Visible='<%# Eval("USERNAME").ToString() == string.Empty ? false : true%>'></asp:Label>
                                                    <asp:Label ID="lblUserNo" runat="server" Text='<%# Eval("TEMP_USERNAME")%>' Visible='<%# Eval("USERNAME").ToString() == string.Empty ? true : false%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%# Eval("NAME")%>
                                                    <asp:Label ID="lblIntake" runat="server" Visible="false" Text='<%# Eval("INTAKE")%>'></asp:Label>
                                                </td>
                                                <%--<td>
                                                    <%# Eval("SEMESTERNAME")%>                                        
                                                </td>--%>
                                                <td>
                                                    <%# Eval("TRANSACTION_DATE")%>                                        
                                                </td>
                                                <td>
                                                    <%# Eval("AMOUNT")%> 
                                                </td>
                                                <td>
                                                    <%# Eval("BANKNAME")%> 
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRandomNo" runat="server" Text='<%# Eval("RANDOMNUMBER")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblStatus" runat="server" ForeColor='<%# Eval("USERNAME").ToString() == string.Empty ? System.Drawing.Color.Red : System.Drawing.Color.Green %>' Text='<%# Eval("USERNAME").ToString() == string.Empty ? "Not Match" : "Match" %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRemark" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div id="divconfirmed" class="col-md-12" runat="server" visible="false">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvConfirmed" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Confirmed List</h5>
                                            </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Application ID</th>
                                                        <th>Student Name</th>
                                                        <th>Faculty/School Name</th>
                                                        <th>Program</th>
                                                        <th>Date</th>
                                                        <th>Amount</th>
                                                        <th>BankName</th>

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
                                                    <%# Eval("Username")%>
                                                </td>
                                                <td>
                                                    <%# Eval("NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("COLLEGE_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PGM")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TRANSACTION_DATE")%>                                        
                                                </td>
                                                <td>
                                                    <%# Eval("AMOUNT")%> 
                                                </td>
                                                <td>
                                                    <%# Eval("BANKNAME")%> 
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <%--ENROLLMENT TAB--%>
                            <div class="col-12" id="divEnrollment" runat="server" visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="DivIntake" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Intake"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlEnrollIntake" runat="server" TabIndex="1" CssClass="form-control select2 select-click"
                                            AppendDataBoundItems="True" OnSelectedIndexChanged="ddlEnrollIntake_SelectedIndexChanged" AutoPostBack="true"
                                            ToolTip="Please Select Session Long Name">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlEnrollIntake"
                                            Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                            ValidationGroup="EnrollSubmit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="DivEnrollUpload" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Upload Excel File"></asp:Label>
                                        </div>
                                        <div id="Div4" class="logoContainer" runat="server">
                                            <img src="../IMAGES/excel.png" alt="upload image" runat="server" id="imgEnroll" tabindex="2" />
                                        </div>
                                        <div class="fileContainer sprite pl-1">
                                            <span runat="server" id="Span1"
                                                cssclass="form-control" tabindex="2">Upload File</span>
                                            <asp:FileUpload ID="updEnrollUpload" runat="server" ToolTip="Select file to upload"
                                                CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="updEnrollUpload"
                                                Display="None" ErrorMessage="Please select file to upload." ValidationGroup="EnrollSubmit"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                        <asp:HiddenField ID="hdfEnrollmentRandomNo" runat="server" Value="0" />
                                    </div>
                                </div>
                                <div class="col-12 btn-footer" id="divEnrollmentButton" runat="server">

                                    <asp:Button ID="btnEnrollDownload" runat="server" TabIndex="3"
                                        Text="Click To Download Pre-Requisite Format" OnClick="btnEnrollDownload_Click"
                                        CssClass="btn btn-outline-info" ToolTip="Click To Download Pre-Requisite Format" />

                                    <asp:Button ID="btnEnrollVerify" runat="server" ValidationGroup="EnrollSubmit" TabIndex="4" OnClick="btnEnrollVerify_Click"
                                        Text="Upload and Verify" CssClass="btn btn-outline-primary" ToolTip="Click to Upload  & Verify" />

                                    <asp:Button ID="btnEnrollConfirm" runat="server" TabIndex="5"
                                        Text="Confirm" CssClass="btn btn-outline-primary" ValidationGroup="EnrollSubmit" ToolTip="Confirm" OnClick="btnEnrollConfirm_Click" />

                                    <asp:Button ID="btnEnrollCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                        TabIndex="6" OnClick="btnEnrollCancel_Click" CssClass="btn btn-outline-danger" />

                                    <asp:ValidationSummary ID="validationsummary1" runat="server" ValidationGroup="EnrollSubmit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                                <div  class="col-md-12" runat="server" id="uploadenroll" visible="false">
                                    <asp:Panel ID="Panel3" runat="server">
                                        <asp:ListView ID="LvTempEnroll" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Uploded List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="table1">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>
                                                                <asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheck(this)" />
                                                            </th>
                                                            <th>Application ID</th>
                                                            <th>Student Name</th>
                                                            <th>Date</th>
                                                            <th>Amount</th>
                                                            <th>BankName</th>
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
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chktransfer" runat="server" ToolTip='<%# Eval("TEMP_USERNAME")%>' Enabled='<%# Eval("REGNO").ToString() == string.Empty ? false : true%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("REGNO")%>' Visible='<%# Eval("REGNO").ToString() == string.Empty ? false : true%>'></asp:Label>
                                                        <asp:Label ID="lblUserNo" runat="server" Text='<%# Eval("TEMP_USERNAME")%>' Visible='<%# Eval("REGNO").ToString() == string.Empty ? true : false%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NAME")%>
                                                        <asp:Label ID="lblIntake" runat="server" Visible="false" Text='<%# Eval("INTAKENO")%>'></asp:Label>
                                                         <asp:HiddenField ID="hdfname" runat="server"  Value='<%# Eval("NAME")%>' />
                                                        <asp:HiddenField ID="hdfusername" runat="server"  Value='<%# Eval("TEMP_USERNAME")%>' />
                                                        <asp:HiddenField ID="hdfprogram" runat="server"  Value='<%# Eval("PROGRAM")%>' />
                                                        <asp:HiddenField ID="hdfbatchname" runat="server"  Value='<%# Eval("BATCHNAME")%>' />
                                                        <%--<asp:HiddenField ID="hdffeestype" runat="server"  Value='<%# Eval("FEES_TYPE")%>' />--%>
                                                        <asp:HiddenField ID="hdfamount" runat="server"  Value='<%# Eval("AMOUNT")%>' />
                                                        <asp:HiddenField ID="HiddenFhdfdate" runat="server"  Value='<%# Eval("TRANSACTION_DATE")%>' />
                                                        <asp:HiddenField ID="hdfidno" runat="server"  Value='<%# Eval("IDNO")%>' />
                                                        <asp:HiddenField ID="hdfemailid" runat="server"  Value='<%# Eval("EMAILID")%>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("TRANSACTION_DATE")%>                                        
                                                    </td>
                                                    <td>
                                                        <%# Eval("AMOUNT")%> 
                                                    </td>
                                                    <td>
                                                        <%# Eval("BANKNAME")%> 
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRandomNo" runat="server" Text='<%# Eval("RANDOMNUMBER")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblStatus" runat="server" ForeColor='<%# Eval("REGNO").ToString() == string.Empty ? System.Drawing.Color.Red : System.Drawing.Color.Green %>' Text='<%# Eval("REGNO").ToString() == string.Empty ? "Not Match" : "Match" %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRemark" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <div id="divconfirmedenroll" class="col-md-12" runat="server" visible="false">
                                    <asp:Panel ID="Panel4" runat="server">
                                        <asp:ListView ID="LvConfirmeDenroll" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Confirmed List</h5>
                                                </div>
                                                  <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Application ID</th>
                                                            <th>Student Name</th>
                                                             <th>Faculty/School Name</th>
                                                             <th>Program</th>
                                                            <th>Date</th>
                                                            <th>Amount</th>
                                                            <th>BankName</th>

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
                                                        <%# Eval("Regno")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NAME")%>
                                                    </td>
                                                     <td>
                                                        <%# Eval("COLLEGE_NAME")%>
                                                    </td>
                                                     <td>
                                                        <%# Eval("PGM")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TRANSACTION_DATE")%>                                        
                                                    </td>
                                                    <td>
                                                        <%# Eval("AMOUNT")%> 
                                                    </td>
                                                    <td>
                                                        <%# Eval("BANKNAME")%> 
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>
                                <%--SEMESTER TAB--%>
                            <div class="col-12" id="divSemester" runat="server" visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Div3" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Session Name"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlintakesem" runat="server" TabIndex="1" CssClass="form-control select2 select-click"
                                            AppendDataBoundItems="True"  OnSelectedIndexChanged="ddlintakesem_SelectedIndexChanged" AutoPostBack="true"
                                            ToolTip="Please Select Session Name">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlintakesem"
                                            Display="None" ErrorMessage="Please Select Session Name" InitialValue="0"
                                            ValidationGroup="SemSubmit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Div5" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="Upload Excel File"></asp:Label>
                                        </div>
                                        <div id="Div6" class="logoContainer2" runat="server">
                                            <img src="../IMAGES/excel.png" alt="upload image" runat="server" id="imgSem" tabindex="2" />
                                        </div>
                                        <div class="fileContainer sprite pl-1">
                                            <span runat="server" id="Span2"
                                                cssclass="form-control" tabindex="2">Upload File</span>
                                            <asp:FileUpload ID="fileuploadsem" runat="server" ToolTip="Select file to upload"
                                                CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="updEnrollUpload"
                                                Display="None" ErrorMessage="Please select file to upload." ValidationGroup="SemSubmit"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                                    </div>
                                </div>
                                <div class="col-12 btn-footer" id="div7" runat="server">
                                    <asp:Button ID="btndownloadsem" runat="server" TabIndex="3"
                                        Text="Click To Download Pre-Requisite Format" OnClick="btndownloadsem_Click" 
                                        CssClass="btn btn-outline-info" ToolTip="Click To Download Pre-Requisite Format" />
                                    <asp:Button ID="btnverifysem" runat="server" ValidationGroup="SemSubmit" TabIndex="4" OnClick="btnverifysem_Click"
                                        Text="Upload and Verify" CssClass="btn btn-outline-primary" ToolTip="Click to Upload  & Verify" />
                                    <asp:Button ID="btnconfirmedsem" runat="server" TabIndex="5" ValidationGroup="SemSubmit"
                                        Text="Confirm" CssClass="btn btn-outline-primary" ToolTip="Confirm" OnClick="btnconfirmedsem_Click" />
                                    <asp:Button ID="btncancelsem" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                        TabIndex="6"  OnClick="btncancelsem_Click" CssClass="btn btn-outline-danger" />
                                    <asp:ValidationSummary ID="validationsummary2" runat="server" ValidationGroup="SemSubmit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                                <div id="uploadsem" class="col-md-12" runat="server" visible="false">
                                    <asp:Panel ID="Panel5" runat="server">
                                        <asp:ListView ID="LvTempSem" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Uploded List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tableSem">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                           <%-- <th>
                                                                <asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" />
                                                            </th>--%>
                                                            <th>Application ID</th>
                                                            <th>Student Name</th>
                                                            <th>Date</th>
                                                             <th>Semester</th>
                                                            <th>Amount</th>
                                                            <th>BankName</th>
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
                                                <tr>
                                                   <%-- <td>
                                                        <asp:CheckBox ID="chktransfer" runat="server" ToolTip='<%# Eval("TEMP_USERNAME")%>' Enabled='<%# Eval("REGNO").ToString() == string.Empty ? false : true%>' />
                                                    </td>--%>
                                                    <td>
                                                        <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("REGNO")%>' Visible='<%# Eval("REGNO").ToString() == string.Empty ? false : true%>' ToolTip='<%# Eval("TEMP_USERNAME")%>'></asp:Label>
                                                        <asp:Label ID="lblUserNo" runat="server" Text='<%# Eval("TEMP_USERNAME")%>' Visible='<%# Eval("REGNO").ToString() == string.Empty ? true : false%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NAME")%>
                                                        <asp:Label ID="lblIntake" runat="server" Visible="false" Text='<%# Eval("INTAKENO")%>'></asp:Label>
                                                         <asp:HiddenField ID="hdfname" runat="server"  Value='<%# Eval("NAME")%>' />
                                                        <asp:HiddenField ID="hdfusername" runat="server"  Value='<%# Eval("TEMP_USERNAME")%>' />
                                                        <asp:HiddenField ID="hdfprogram" runat="server"  Value='<%# Eval("PROGRAM")%>' />
                                                        <asp:HiddenField ID="hdfbatchname" runat="server"  Value='<%# Eval("BATCHNAME")%>' />
                                                        <%--<asp:HiddenField ID="hdffeestype" runat="server"  Value='<%# Eval("FEES_TYPE")%>' />--%>
                                                        <asp:HiddenField ID="hdfamount" runat="server"  Value='<%# Eval("AMOUNT")%>' />
                                                        <asp:HiddenField ID="HiddenFhdfdate" runat="server"  Value='<%# Eval("TRANSACTION_DATE")%>' />
                                                        <asp:HiddenField ID="hdfidno" runat="server"  Value='<%# Eval("IDNO")%>' />
                                                        <asp:HiddenField ID="hdfemailid" runat="server"  Value='<%# Eval("EMAILID")%>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("TRANSACTION_DATE")%>                                        
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTERNAME")%>                                        
                                                    </td>
                                                    <td>
                                                        <%# Eval("AMOUNT")%> 
                                                    </td>
                                                    <td>
                                                        <%# Eval("BANKNAME")%> 
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRandomNo" runat="server" Text='<%# Eval("RANDOMNUMBER")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblMatchRegNo" runat="server" Text='<%# Eval("MATCH_REGNO")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblStatus" runat="server" ForeColor='<%# Eval("MATCH_REGNO").ToString() == string.Empty ? System.Drawing.Color.Red : System.Drawing.Color.Green %>' Text='<%# Eval("MATCH_REGNO").ToString() == string.Empty ? "Not Match" : "Match" %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRemark" runat="server" Text='<%# Convert.ToString(Eval("RECON")) == "1" ? "Record Already Exists" : "" %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <div id="divconfirmedSem" class="col-md-12" runat="server" visible="false">
                                    <asp:Panel ID="Panel6" runat="server">
                                        <asp:ListView ID="LvCONFIRMEDsEM" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Confirmed List</h5>
                                                </div>
                                                     <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                             <th>SrNo</th>
                                                            <th>Application ID</th>
                                                            <th>Student Name</th>
                                                             <th>Faculty/School Name</th>
                                                             <th>Program</th>
                                                            <th>Semester</th>
                                                            <th>Date</th>
                                                            <th>Amount</th>
                                                            <th>BankName</th>
                                                            <th>Status</th>
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
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRegno" runat="server" Text='<%# Eval("REGNO")%>'></asp:Label>
                                                        <asp:HiddenField ID="hdfbatchname" runat="server"  Value='<%# Eval("BATCHNAME")%>' />
                                                        <asp:HiddenField ID="hdfamount" runat="server"  Value='<%# Eval("AMOUNT")%>' />
                                                        <asp:HiddenField ID="HiddenFhdfdate" runat="server"  Value='<%# Eval("TRANSACTION_DATE")%>' />
                                                        <asp:HiddenField ID="hdfidno" runat="server"  Value='<%# Eval("IDNO")%>' />
                                                        <asp:HiddenField ID="hdfemailid" runat="server"  Value='<%# Eval("EMAILID")%>' />
                                                        <asp:HiddenField ID="hdfDcrNo" runat="server"  Value='<%# Eval("DCR_NO")%>' />
                                                        <asp:HiddenField ID="hdfmaindcr" runat="server"  Value='<%# Eval("DCRNO")%>' />
                                                    </td>
                                                    <td>
                                                      <asp:Label ID="lblname" runat="server" Text='<%# Eval("NAME")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("COLLEGE_NAME")%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblProgram" runat="server" Text='<%# Eval("PGM")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTERNAME")%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("TRANSACTION_DATE")%>'></asp:Label>                                       
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("BANKNAME")%> 
                                                    </td>
                                                    <td>
                                                        <%# Eval("RECON_STATUS")%> 
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btndownloadsem" />
            <asp:PostBackTrigger ControlID="btnverifysem" />
            <asp:PostBackTrigger ControlID="btnDownload" />
            <asp:PostBackTrigger ControlID="btnUpload" />
            <asp:PostBackTrigger ControlID="btnEnrollDownload" />
            <asp:PostBackTrigger ControlID="btnEnrollVerify" />
        </Triggers>

    </asp:UpdatePanel>

    <script>
        $(document).ready(function () {
            $(document).on("click", ".logoContainer1", function () {
                $("#ctl00_ContentPlaceHolder1_FileUpload1").click();
            });
            $(document).on("keydown", ".logoContainer1", function () {
                if (event.keyCode === 13) {
                    // Cancel the default action, if needed
                    event.preventDefault();
                    // Trigger the button element with a click
                    $("#ctl00_ContentPlaceHolder1_FileUpload1").click();
                }
            });
        });
    </script>

    <script type="text/javascript">
        function Focus() {
            //alert("hii");
            document.getElementById("<%=imgUpFile.ClientID%>").focus();
        }
    </script>

    <script>
        $("#ctl00_ContentPlaceHolder1_FileUpload1").change(function () {
            //$('.fuCollegeLogo').on('change', function () {

            var maxFileSize = 1000000;
            var fi = document.getElementById('ctl00_ContentPlaceHolder1_FileUpload1');
            myfile = $(this).val();
            var ext = myfile.split('.').pop();
            var res = ext.toUpperCase();

            //alert(res)
            if (res != "XLSX" && res != "XLS") {
                alert("Please Select xlsx,xls File Only.");
                $('.logoContainer1 img').attr('src', "../IMAGES/excel.png");
                //$(this).val('');
            }

            for (var i = 0; i <= fi.files.length - 1; i++) {
                var fsize = fi.files.item(i).size;
                if (fsize >= maxFileSize) {
                    alert("File Size should be less than 1 MB");
                    $('.logoContainer1 img').attr('src', "../IMAGES/excel.png");
                    //$("#ctl00_ContentPlaceHolder1_FileUpload1").val("");

                }
            }

        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#ctl00_ContentPlaceHolder1_FileUpload1").change(function () {
                //$('.fuCollegeLogo').on('change', function () {

                var maxFileSize = 1000000;
                var fi = document.getElementById('ctl00_ContentPlaceHolder1_FileUpload1');
                myfile = $(this).val();
                var ext = myfile.split('.').pop();
                var res = ext.toUpperCase();

                //alert(res)
                if (res != "XLSX" && res != "XLS") {
                    alert("Please Select xlsx,xls File Only.");
                    $('.logoContainer1 img').attr('src', "../IMAGES/excel.png");
                    //$(this).val('');
                }

                for (var i = 0; i <= fi.files.length - 1; i++) {
                    var fsize = fi.files.item(i).size;
                    if (fsize >= maxFileSize) {
                        alert("File Size should be less than 1 MB");
                        $('.logoContainer1 img').attr('src', "../IMAGES/excel.png");
                        //$("#ctl00_ContentPlaceHolder1_FileUpload1").val("");

                    }
                }

            });
        });

    </script>

    <script>
        $("#ctl00_ContentPlaceHolder1_FileUpload1").change(function () {
            var fileName = $(this).val();

            newText = fileName.replace(/fakepath/g, '');
            var newtext1 = newText.replace(/C:/, '');
            //newtext2 = newtext1.replace('//', ''); 
            var result = newtext1.substring(2, newtext1.length);


            if (result.length > 0) {
                $(this).parent().children('span').html(result);
            }
            else {
                $(this).parent().children('span').html("Choose file");
            }
        });
        //file input preview
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('.logoContainer img').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        $("#ctl00_ContentPlaceHolder1_FileUpload1").change(function () {
            readURL(this);
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#ctl00_ContentPlaceHolder1_FileUpload1").change(function () {
                var fileName = $(this).val();

                newText = fileName.replace(/fakepath/g, '');
                var newtext1 = newText.replace(/C:/, '');
                //newtext2 = newtext1.replace('//', ''); 
                var result = newtext1.substring(2, newtext1.length);


                if (result.length > 0) {
                    $(this).parent().children('span').html(result);
                }
                else {
                    $(this).parent().children('span').html("Choose file");
                }
            });
            //file input preview
            function readURL(input) {
                if (input.files && input.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        //$('.logoContainer img').attr('src', e.target.result);
                    }
                    reader.readAsDataURL(input.files[0]);
                }
            }
            $("#ctl00_ContentPlaceHolder1_FileUpload1").change(function () {
                readURL(this);
            });

        });
    </script>

    <script>
          $(document).ready(function () {
              $(document).on("click", ".logoContainer1", function () {
                  $("#ctl00_ContentPlaceHolder1_fileuploadsem").click();
              });
              $(document).on("keydown", ".logoContainer2", function () {
                  if (event.keyCode === 13) {
                      // Cancel the default action, if needed
                      event.preventDefault();
                      // Trigger the button element with a click
                      $("#ctl00_ContentPlaceHolder1_fileuploadsem").click();
                  }
              });
          });
    </script>

    <script type="text/javascript">
        function Focus() {
            //alert("hii");
            document.getElementById("<%=imgSem.ClientID%>").focus();
        }
    </script>

    <script>
        $("#ctl00_ContentPlaceHolder1_fileuploadsem").change(function () {
            //$('.fuCollegeLogo').on('change', function () {

            var maxFileSize = 1000000;
            var fi = document.getElementById('ctl00_ContentPlaceHolder1_fileuploadsem');
            myfile = $(this).val();
            var ext = myfile.split('.').pop();
            var res = ext.toUpperCase();

            //alert(res)
            if (res != "XLSX" && res != "XLS") {
                alert("Please Select xlsx,xls File Only.");
                $('.logoContainer2 img').attr('src', "../IMAGES/excel.png");
                //$(this).val('');
            }

            for (var i = 0; i <= fi.files.length - 1; i++) {
                var fsize = fi.files.item(i).size;
                if (fsize >= maxFileSize) {
                    alert("File Size should be less than 1 MB");
                    $('.logoContainer2 img').attr('src', "../IMAGES/excel.png");
                    //$("#ctl00_ContentPlaceHolder1_FileUpload1").val("");

                }
            }

        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#ctl00_ContentPlaceHolder1_fileuploadsem").change(function () {
                //$('.fuCollegeLogo').on('change', function () {

                var maxFileSize = 1000000;
                var fi = document.getElementById('ctl00_ContentPlaceHolder1_fileuploadsem');
                myfile = $(this).val();
                var ext = myfile.split('.').pop();
                var res = ext.toUpperCase();

                //alert(res)
                if (res != "XLSX" && res != "XLS") {
                    alert("Please Select xlsx,xls File Only.");
                    $('.logoContainer2 img').attr('src', "../IMAGES/excel.png");
                    //$(this).val('');
                }

                for (var i = 0; i <= fi.files.length - 1; i++) {
                    var fsize = fi.files.item(i).size;
                    if (fsize >= maxFileSize) {
                        alert("File Size should be less than 1 MB");
                        $('.logoContainer2 img').attr('src', "../IMAGES/excel.png");
                        //$("#ctl00_ContentPlaceHolder1_FileUpload1").val("");

                    }
                }

            });
        });

    </script>

    <script>
        $("#ctl00_ContentPlaceHolder1_fileuploadsem").change(function () {
            var fileName = $(this).val();

            newText = fileName.replace(/fakepath/g, '');
            var newtext1 = newText.replace(/C:/, '');
            //newtext2 = newtext1.replace('//', ''); 
            var result = newtext1.substring(2, newtext1.length);


            if (result.length > 0) {
                $(this).parent().children('span').html(result);
            }
            else {
                $(this).parent().children('span').html("Choose file");
            }
        });
        //file input preview
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('.logoContainer img').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        $("#ctl00_ContentPlaceHolder1_fileuploadsem").change(function () {
            readURL(this);
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#ctl00_ContentPlaceHolder1_fileuploadsem").change(function () {
                var fileName = $(this).val();

                newText = fileName.replace(/fakepath/g, '');
                var newtext1 = newText.replace(/C:/, '');
                //newtext2 = newtext1.replace('//', ''); 
                var result = newtext1.substring(2, newtext1.length);


                if (result.length > 0) {
                    $(this).parent().children('span').html(result);
                }
                else {
                    $(this).parent().children('span').html("Choose file");
                }
            });
            //file input preview
            function readURL(input) {
                if (input.files && input.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        //$('.logoContainer img').attr('src', e.target.result);
                    }
                    reader.readAsDataURL(input.files[0]);
                }
            }
            $("#ctl00_ContentPlaceHolder1_fileuploadsem").change(function () {
                readURL(this);
            });

        });
    </script>

    <script>
        $(document).ready(function () {
            $(document).on("click", ".logoContainer", function () {
                $("#ctl00_ContentPlaceHolder1_updEnrollUpload").click();
            });
            $(document).on("keydown", ".logoContainer", function () {
                if (event.keyCode === 13) {
                    // Cancel the default action, if needed
                    event.preventDefault();
                    // Trigger the button element with a click
                    $("#ctl00_ContentPlaceHolder1_updEnrollUpload").click();
                }
            });
        });
    </script>

    <script type="text/javascript">
        function Focus() {
            //alert("hii");
            document.getElementById("<%=imgEnroll.ClientID%>").focus();
        }
    </script>

    <script>
        $("#ctl00_ContentPlaceHolder1_updEnrollUpload").change(function () {
            //$('.fuCollegeLogo').on('change', function () {

            var maxFileSize = 1000000;
            var fi = document.getElementById('ctl00_ContentPlaceHolder1_updEnrollUpload');
            myfile = $(this).val();
            var ext = myfile.split('.').pop();
            var res = ext.toUpperCase();

            //alert(res)
            if (res != "XLSX" && res != "XLS") {
                alert("Please Select xlsx,xls File Only.");
                $('.logoContainer img').attr('src', "../IMAGES/excel.png");
                //$(this).val('');
            }

            for (var i = 0; i <= fi.files.length - 1; i++) {
                var fsize = fi.files.item(i).size;
                if (fsize >= maxFileSize) {
                    alert("File Size should be less than 1 MB");
                    $('.logoContainer img').attr('src', "../IMAGES/excel.png");
                    //$("#ctl00_ContentPlaceHolder1_FileUpload1").val("");

                }
            }

        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#ctl00_ContentPlaceHolder1_updEnrollUpload").change(function () {
                //$('.fuCollegeLogo').on('change', function () {

                var maxFileSize = 1000000;
                var fi = document.getElementById('ctl00_ContentPlaceHolder1_updEnrollUpload');
                myfile = $(this).val();
                var ext = myfile.split('.').pop();
                var res = ext.toUpperCase();

                //alert(res)
                if (res != "XLSX" && res != "XLS") {
                    alert("Please Select xlsx,xls File Only.");
                    $('.logoContainer img').attr('src', "../IMAGES/excel.png");
                    //$(this).val('');
                }

                for (var i = 0; i <= fi.files.length - 1; i++) {
                    var fsize = fi.files.item(i).size;
                    if (fsize >= maxFileSize) {
                        alert("File Size should be less than 1 MB");
                        $('.logoContainer img').attr('src', "../IMAGES/excel.png");
                        //$("#ctl00_ContentPlaceHolder1_FileUpload1").val("");

                    }
                }

            });
        });

    </script>

    <script>
        $("#ctl00_ContentPlaceHolder1_updEnrollUpload").change(function () {
            var fileName = $(this).val();

            newText = fileName.replace(/fakepath/g, '');
            var newtext1 = newText.replace(/C:/, '');
            //newtext2 = newtext1.replace('//', ''); 
            var result = newtext1.substring(2, newtext1.length);


            if (result.length > 0) {
                $(this).parent().children('span').html(result);
            }
            else {
                $(this).parent().children('span').html("Choose file");
            }
        });
        //file input preview
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('.logoContainer img').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        $("#ctl00_ContentPlaceHolder1_updEnrollUpload").change(function () {
            readURL(this);
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#ctl00_ContentPlaceHolder1_updEnrollUpload").change(function () {
                var fileName = $(this).val();

                newText = fileName.replace(/fakepath/g, '');
                var newtext1 = newText.replace(/C:/, '');
                //newtext2 = newtext1.replace('//', ''); 
                var result = newtext1.substring(2, newtext1.length);


                if (result.length > 0) {
                    $(this).parent().children('span').html(result);
                }
                else {
                    $(this).parent().children('span').html("Choose file");
                }
            });
            //file input preview
            function readURL(input) {
                if (input.files && input.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        //$('.logoContainer img').attr('src', e.target.result);
                    }
                    reader.readAsDataURL(input.files[0]);
                }
            }
            $("#ctl00_ContentPlaceHolder1_updEnrollUpload").change(function () {
                readURL(this);
            });

        });
    </script>

    <script type="text/javascript" language="javascript">

        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$LvTempSem$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$LvTempSem$ctrl';
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

        function checkAllCheck(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$LvTempEnroll$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$LvTempEnroll$ctrl';
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

