<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CourseOffered.aspx.cs" Inherits="ACADEMIC_CourseOffered" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <%--     <link href="../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../plugins/multiselect/bootstrap-multiselect.js"></script>--%>

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>


    <%--    <script type="text/javascript">
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
    <script type="text/javascript">

        $(document).ready(function () {

            //--*========Added by akhilesh on [2019-05-11]==========*--          
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(InitAutoCompl)

            //--*========Added by akhilesh on [2019-05-11]==========*--          
            // if you use jQuery, you can load them when dom is read.          
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            // Place here the first init of the autocomplete
            InitAutoCompl();

            function InitializeRequest(sender, args) {
            }

            function EndRequest(sender, args) {
                // after update occur on UpdatePanel re-init the Autocomplete
                InitAutoCompl();
            }
        });

    </script>

    <script>
        function getVal() {
            var array = []
            var sectionNo;
            var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

            for (var i = 0; i < checkboxes.length; i++) {
                //array.push(checkboxes[i].value)    
                if (sectionNo == undefined) {
                    sectionNo = checkboxes[i].value + ',';
                }
                else {
                    sectionNo += checkboxes[i].value + ',';
                }
            }
            //alert(sectionNo);

            //document.getElementById(inpHide).value = "degreeNo";
        }
    </script>
    <script type="text/javascript">
        document.onreadystatechange = function () {
            var state = document.readyState
            if (state == 'interactive') {
                document.getElementById('contents').style.visibility = "hidden";
            } else if (state == 'complete') {
                setTimeout(function () {
                    document.getElementById('interactive');
                    document.getElementById('load').style.visibility = "hidden";
                    document.getElementById('contents').style.visibility = "visible";
                }, 1000);
            }
        }
    </script>--%>



    <%--  <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>--%>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>

    <style>
        .multiselect-selected-text {
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
        }


        .test .form-control {
            padding: 0.15rem 0.15rem;
        }

        #ctl00_ContentPlaceHolder1_Panel3 .dataTables_scrollHeadInner {
            width: max-content !important;
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

    <script>
        $(document).ready(function () {
            var table = $('#mytables').DataTable({
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
                                return $('#mytables').DataTable().column(idx).visible();
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
                                            return $('#mytables').DataTable().column(idx).visible();
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
                                            return $('#mytables').DataTable().column(idx).visible();
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
                                            return $('#mytables').DataTable().column(idx).visible();
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
                var table = $('#mytables').DataTable({
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
                                    return $('#mytables').DataTable().column(idx).visible();
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
                                                return $('#mytables').DataTable().column(idx).visible();
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
                                                return $('#mytables').DataTable().column(idx).visible();
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
                                                return $('#mytables').DataTable().column(idx).visible();
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
    <%--    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updCourseOffered"
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

    <asp:UpdatePanel ID="updCourseOffered" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>

                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item test">
                                <a class="nav-link active" data-toggle="tab" onclick="document.getElemtbyId('sel-tab').value=1;" href="#tab_1" type="button">
                                    <asp:Label ID="lblRAModule" runat="server"></asp:Label>
                                    Offer</a>
                            </li>
                            <li class="nav-item test2">
                                <a class="nav-link" data-toggle="tab" onclick="document.getElemtbyId('sel-tab').value=2;" href="#tab_2" type="button">Copy
                                    <asp:Label ID="lblRASLModule" runat="server"></asp:Label>
                                    Offer</a>
                            </li>

                            <li class="nav-item test2" runat="server" visible="false">
                                <a class="nav-link" data-toggle="tab" onclick="document.getElemtbyId('sel-tab').value=3;" href="#tab_3" type="button">Mismatch Module
                                    Offer</a>
                            </li>

                        </ul>

                        <div class="tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updCourseOffered"
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
                                <asp:UpdatePanel ID="updCourseOffered" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblSessionName" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" CssClass="form-control select2 select-click"
                                                            AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                                            ToolTip="Please Select Session Long Name">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSession"
                                                            Display="None" ErrorMessage="Please Select Session Name" InitialValue="0"
                                                            ValidationGroup="submit" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSession"
                                                            Display="None" ErrorMessage="Please Select Session Name" InitialValue="0"
                                                            ValidationGroup="ExcelExport" />

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCollege" runat="server" TabIndex="2" CssClass="form-control select2 select-click"
                                                            AppendDataBoundItems="True"
                                                            ToolTip="Please Select College" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                                            Display="None" ErrorMessage="Please Select College" InitialValue="0"
                                                            ValidationGroup="submit" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCollege"
                                                            Display="None" ErrorMessage="Please Select College" InitialValue="0"
                                                            ValidationGroup="ExcelExport" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblDYDegree" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="3" CssClass="form-control select2 select-click"
                                                            AppendDataBoundItems="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true"
                                                            ToolTip="Please Select Degree">
                                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDegree"
                                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0"
                                                            ValidationGroup="submit" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblDYScheme" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlScheme" runat="server" TabIndex="4" CssClass="form-control select2 select-click"
                                                            AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged1"
                                                            ToolTip="Please Select Regulation">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlScheme"
                                                            Display="None" ErrorMessage="Please Select Curriculum" InitialValue="0"
                                                            ValidationGroup="submit" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true"
                                                            CssClass="form-control select2 select-click">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlSemester"
                                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0"
                                                            ValidationGroup="submit" />

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="lable-dynamic">
                                                            
                                                            <asp:Label ID="lblOtherDegree" runat="server" Font-Bold="true">Other Degree</asp:Label>
                                                        </div>
                                                        <asp:ListBox ID="ddlOtherDegree" runat="server" AppendDataBoundItems="true"
                                                            CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <%--   <sup>* </sup>--%>
                                                            <asp:Label ID="lblDYInterDept" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <%-- <asp:DropDownList ID="ddlDept" runat="server" TabIndex="5" CssClass="form-control select2 select-click"
                                                            AppendDataBoundItems="True" AutoPostBack="True"
                                                            ToolTip="Please Select Department" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                                        </asp:DropDownList>--%>
                                                        <asp:ListBox ID="lstbxDept" runat="server" AppendDataBoundItems="true"
                                                            CssClass="form-control multi-select-demo" SelectionMode="multiple" OnSelectedIndexChanged="lstbxDept_SelectedIndexChanged"></asp:ListBox>

                                                        <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDept"
                                                            Display="None" ErrorMessage="Please Select Department" InitialValue="0" />--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DivUpdateRemo">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="Label9" runat="server" Font-Bold="true">Update/Remove For</asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlUpdate_remove" runat="server" TabIndex="4" CssClass="form-control select2 select-click"
                                                            AppendDataBoundItems="True"
                                                            ToolTip="Please Select Regulation">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Selected Scheme</asp:ListItem>
                                                            <asp:ListItem Value="2">All Scheme</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlUpdate_remove"
                                                            Display="None" ErrorMessage="Please Select Update/Remove For" InitialValue="0"
                                                            ValidationGroup="submit" />--%>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">

                                                <asp:LinkButton ID="btnShow" runat="server" ToolTip="Show"
                                                    CssClass="btn btn-outline-info" OnClick="btnShow_Click"
                                                    TabIndex="6" ValidationGroup="submit">Show</asp:LinkButton>



                                                <asp:LinkButton ID="btnSave" runat="server" ToolTip="Submit"
                                                    CssClass="btn btn-outline-info" OnClick="btnSave_Click" TabIndex="6" ValidationGroup="submit" Visible="false" OnClientClick="return getVal();">Submit</asp:LinkButton>

                                                <asp:LinkButton ID="btnUpdate" runat="server" ToolTip="Submit"
                                                    CssClass="btn btn-outline-info" OnClick="btnUpdate_Click" TabIndex="6" ValidationGroup="submit" Visible="false" OnClientClick="return getVal();">Update</asp:LinkButton>

                                                <asp:LinkButton ID="btnreport" runat="server" ToolTip="Report"
                                                    CssClass="btn btn-outline-info" OnClick="btnreport_Click" TabIndex="7" ValidationGroup="submit" Visible="false">Report</asp:LinkButton>

                                                <asp:Button ID="btnExcelExport" runat="server" CssClass="btn btn-outline-primary" Text="Offered Report" OnClick="btnExcelExport_Click" ValidationGroup="ExcelExport" />

                                                <asp:Button ID="lnkOfferedCourselist" runat="server" CssClass="btn btn-outline-info" Text="View Offered" OnClick="lnkOfferedCourselist_Click" ValidationGroup="submit" />

                                                <asp:Button ID="btnDeleteModule" runat="server" CssClass="btn btn-outline-info" Text="Remove Offered" ValidationGroup="submit" OnClick="btnDeleteModule_Click" Visible="false" />

                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                    TabIndex="7" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" />

                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="ExcelExport"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                <asp:HiddenField ID="hdfsem" runat="server" />
                                            </div>
                                            <div class="col-md-12">
                                                <asp:Panel ID="pnlCourse" runat="server" ScrollBars="Auto">
                                                    <asp:ListView ID="lvCourse" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Subject Offer List </h5>
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
                                                                    <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px; z-index: 1;">
                                                                        <tr>
                                                                            <%-- <td>Sr No.</td>--%>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblDYClgCode"></asp:Label>
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblDYSubjectCode"></asp:Label></th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lbltype"></asp:Label></th>
                                                                            <th style="text-align: center">
                                                                                <asp:Label runat="server" ID="lblDYCredits"></asp:Label></th>
                                                                            <%-- <th style="text-align: center">Capacity</th>--%>
                                                                            <th style="text-align: center">
                                                                                <asp:Label runat="server" ID="lblCoreElect"></asp:Label></th>
                                                                            <th style="text-align: center;" class="d-none">
                                                                                <asp:Label runat="server" ID="lblSubjectGroup"></asp:Label></th>
                                                                            <th style="text-align: center">
                                                                                <asp:Label runat="server" ID="lblOffer"></asp:Label></th>
                                                                            <th style="text-align: center" runat="server" visible="false">Special</th>
                                                                            <th style="text-align: center">
                                                                                <asp:Label runat="server" ID="lblSemester"></asp:Label></th>
                                                                            <th style="text-align: center">
                                                                                <asp:Label runat="server" ID="lblcurri">Curriculum</asp:Label></th>
                                                                            <th style="text-align: center" runat="server" visible="false">CA% </th>
                                                                            <th style="text-align: center" runat="server" visible="false">Final%</th>
                                                                            <th style="text-align: center; display: none">total</th>
                                                                            <th style="text-align: center" runat="server" visible="false">Min CA%</th>
                                                                            <th style="text-align: center" runat="server" visible="false">Min Final%</th>
                                                                            <th style="text-align: center" runat="server" visible="false">Overall %</th>
                                                                            <th style="text-align: center" runat="server" visible="false">Module LIC</th>
                                                                            <%--  <th>Total</th>--%>
                                                                            <%-- <th>Seq No.</th>--%>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody class="test">
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>

                                                        <ItemTemplate>
                                                            <tr>
                                                                <%-- <th><%# Container.DataItemIndex + 1 %></th>--%>
                                                                <%--    <td><%# Eval("COURSE_NAME")%> - <%# Eval("CCODE")%> </td>--%>

                                                                <%--   <td><%# Eval("CCODE")%> - <%# Eval("COURSE_NAME")%> </td>--%>

                                                                <td>
                                                                    <asp:Label runat="server" ID="lblCCode" Text='<%# Eval("CCODE")%>'></asp:Label></td>
                                                                <td><%# Eval("COURSE_NAME")%> </td>

                                                                <td><%# Eval("SUBNAME")%> </td>
                                                                <td style="text-align: center">
                                                                    <asp:TextBox ID="txtcredits" runat="server" CssClass="form-control" Text='<% #Eval("CREDITS")%>' Enabled="false"></asp:TextBox></td>
                                                                <td style="text-align: center; display: none">
                                                                    <asp:TextBox ID="txtcapacity" runat="server" CssClass="form-control" Text='<% #Eval("CAPACITY")%>'></asp:TextBox></td>
                                                                <%--  <td><%# Eval("CREDITS") %></td>
                                       <td><%# Eval("CAPACITY") %></td>--%>
                                                                <td style="text-align: center">
                                                                    <asp:DropDownList ID="ddlcore" runat="server" CssClass="form-control test" AppendDataBoundItems="True" Enabled="false"
                                                                        OnSelectedIndexChanged="ddlcore_SelectedIndexChanged" ToolTip='<%# Container.DataItemIndex + 1 %>'>
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lblcore" runat="server" Text='<% #Eval("ELECTIVEGP")%>' Visible="false"></asp:Label>
                                                                    <asp:HiddenField ID="hdfcourse" runat="server" Value='<% #Eval("COURSENO")%>' />

                                                                </td>
                                                                <td style="text-align: center;" class="d-none">
                                                                    <asp:DropDownList ID="ddlcoregroup" runat="server" CssClass="form-control"
                                                                        AppendDataBoundItems="True" Enabled="false">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:HiddenField ID="hdfValue1" runat="server" Value='<%# Container.DataItemIndex + 1 %>' />
                                                                    <asp:Label ID="lblelective" runat="server" Text='<% #Eval("GROUPNO")%>' Visible="false"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:CheckBox ID="chkoffered" runat="server" ToolTip='<%# Eval("COURSENO") %>' Enabled='<%# (Eval("EXAM_REGISTERED").ToString() == "1" && Eval("REGULAR").ToString() == "1") ? false : true %>' /></td>
                                                                <td style="text-align: center" runat="server" visible="false">
                                                                    <asp:CheckBox ID="chkSpecial" runat="server" ToolTip='<%# Eval("BACKLOG") %>' Enabled='<%# (Eval("EXAM_REGISTERED").ToString() == "1" && Eval("BACKLOG").ToString() == "1") ? false : true %>' /></td>
                                                                <td style="text-align: center">
                                                                    <%-- <asp:DropDownList ID="ddlsem" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>--%>
                                                                    <asp:ListBox ID="ddlsem" runat="server" CssClass="form-control multi-select-demo"
                                                                        SelectionMode="Multiple" AppendDataBoundItems="true" onblur="getVal();"></asp:ListBox>
                                                                    <asp:Label ID="LblSemNo" runat="server" Text='<% #Eval("SEMESTERNO")%>' ToolTip='<% #Eval("OFFERED")%>' Visible="false"></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:ListBox ID="lstbScheme" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true"></asp:ListBox>
                                                                </td>

                                                                <td style="text-align: center" runat="server" visible="false">
                                                                    <asp:TextBox ID="txtIntern" runat="server" CssClass="form-control" Text='<% #Eval("INTERNAL_WEIGHTAGE")%>' Enabled='<%# (Eval("INTERNAL_WEIGHTAGE").ToString() == "") ? true : false  %>' onblur="return CheckMark(this);" onkeypress="return onlyNumberKey(event)"></asp:TextBox></td>
                                                                <td style="text-align: center" runat="server" visible="false">
                                                                    <asp:TextBox ID="txtExtern" runat="server" CssClass="form-control" Text='<% #Eval("EXTERNAL_WEIGHTAGE")%>' Enabled='<%# (Eval("EXTERNAL_WEIGHTAGE").ToString() == "") ? true : false  %>' onblur="return CheckMark(this);" onkeypress="return onlyNumberKey(event)"></asp:TextBox></td>

                                                                <td style="text-align: center; display: none">
                                                                    <asp:TextBox ID="txtToatl" runat="server" CssClass="form-control" Text='<% #Eval("CA_PERCENTAGE")%>' Enabled='<%# (Eval("CA_PERCENTAGE").ToString() == "") ? true : false  %>' onkeypress="return onlyNumberKey(event)"></asp:TextBox>
                                                                </td>
                                                                <td style="text-align: center" runat="server" visible="false">
                                                                    <asp:TextBox ID="txtca" runat="server" CssClass="form-control" Text='<% #Eval("CA_PERCENTAGE")%>' Enabled='<%# (Eval("CA_PERCENTAGE").ToString() == "") ? true : false  %>' onkeypress="return onlyNumberKey(event)"></asp:TextBox>
                                                                </td>
                                                                <td style="text-align: center" runat="server" visible="false">
                                                                    <asp:TextBox ID="txtfinal" runat="server" CssClass="form-control" Text='<% #Eval("FINAL_PERCENTAGE")%>' Enabled='<%# (Eval("FINAL_PERCENTAGE").ToString() == "") ? true : false  %>' onkeypress="return onlyNumberKey(event)"></asp:TextBox></td>

                                                                <td style="text-align: center" runat="server" visible="false">
                                                                    <asp:TextBox ID="txtoverall" runat="server" CssClass="form-control" Text='<% #Eval("OVERALL_PERCENTAGE")%>' Enabled='<%# (Eval("OVERALL_PERCENTAGE").ToString() == "") ? true : false  %>' onkeypress="return onlyNumberKey(event)"></asp:TextBox>
                                                                </td>
                                                                <td style="text-align: center" runat="server" visible="false">
                                                                    <asp:DropDownList ID="ddlmodulelic" runat="server" CssClass="form-control select2 select-click" AppendDataBoundItems="True">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lbllic" runat="server" Text='<% #Eval("MODULELIC")%>' ToolTip='<%#Eval("REGULAR") %>' Visible="false"></asp:Label></td>
                                                                </td>
                                                            </tr>

                                                        </ItemTemplate>

                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>

                                        </div>

                                        <div class="col-md-12">
                                            <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
                                                <asp:ListView ID="lvViewOfferModule" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Subject Offer List </h5>
                                                        </div>
                                                        
                                                        <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="MainLeadTable">
                                                                <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px; z-index: 1;">
                                                                    <tr>
                                                                        <th>Select</th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblDYClgCode"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblDYSubjectCode"></asp:Label></th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lbltype"></asp:Label></th>
                                                                        <th style="text-align: center">
                                                                            <asp:Label runat="server" ID="lblDYCredits"></asp:Label></th>
                                                                        <%-- <th style="text-align: center">Capacity</th>--%>
                                                                        <th style="text-align: center">
                                                                            <asp:Label runat="server" ID="lblCoreElect"></asp:Label></th>
                                                                        <th style="text-align: center" class="d-none">
                                                                            <asp:Label runat="server" ID="lblSubjectGroup"></asp:Label></th>
                                                                        <th style="text-align: center">
                                                                            <asp:Label runat="server" ID="lblOffer"></asp:Label></th>
                                                                        <th style="text-align: center" runat="server" visible="false">Special</th>
                                                                        <th style="text-align: center">
                                                                            <asp:Label runat="server" ID="lblSemester"></asp:Label></th>
                                                                       <%-- <th style="text-align: center">
                                                                            <asp:Label runat="server" ID="lblcurri">Curriculum</asp:Label></th>--%>
                                                                        <th style="text-align: center" runat="server" visible="false">CA% </th>
                                                                        <th style="text-align: center" runat="server" visible="false">Final%</th>
                                                                        <th style="text-align: center; display: none">total</th>
                                                                        <th style="text-align: center" runat="server" visible="false">Min CA%</th>
                                                                        <th style="text-align: center" runat="server" visible="false">Min Final%</th>
                                                                        <th style="text-align: center" runat="server" visible="false">Overall %</th>
                                                                        <th style="text-align: center" runat="server" visible="false">Module LIC</th>
                                                                        <%--  <th>Total</th>--%>
                                                                        <%-- <th>Seq No.</th>--%>
                                                                    </tr>
                                                                </thead>
                                                                <tbody class="test">
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>

                                                    <ItemTemplate>
                                                        <tr>
                                                            <%-- <th><%# Container.DataItemIndex + 1 %></th>--%>
                                                            <%--    <td><%# Eval("COURSE_NAME")%> - <%# Eval("CCODE")%> </td>--%>

                                                            <%--   <td><%# Eval("CCODE")%> - <%# Eval("COURSE_NAME")%> </td>--%>
                                                            <td style="text-align: center">
                                                                <asp:CheckBox ID="chkUpdate_Remove" runat="server" ToolTip='<%# Eval("COURSENO") %>' Visible='<%# (Eval("REMOVE_UPDATE").ToString() == "1" && Eval("REGULAR").ToString() == "1" || Eval("BACKLOG").ToString() == "1") ? false : true %>' /></td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblCCodeU" Text='<%# Eval("CCODE")%>'></asp:Label></td>
                                                            <td><%# Eval("COURSE_NAME")%> </td>

                                                            <td><%# Eval("SUBNAME")%> </td>
                                                            <td style="text-align: center">
                                                                <asp:TextBox ID="txtcreditsU" runat="server" CssClass="form-control" Text='<% #Eval("CREDITS")%>' Enabled="false"></asp:TextBox></td>
                                                            <td style="text-align: center; display: none">
                                                                <asp:TextBox ID="txtcapacityU" runat="server" CssClass="form-control" Text='<% #Eval("CAPACITY")%>'></asp:TextBox></td>
                                                            <%--  <td><%# Eval("CREDITS") %></td>
                                       <td><%# Eval("CAPACITY") %></td>--%>
                                                            <td style="text-align: center">
                                                                <asp:DropDownList ID="ddlcoreU" runat="server" CssClass="form-control test" AppendDataBoundItems="True" Enabled="false"
                                                                    OnSelectedIndexChanged="ddlcore_SelectedIndexChanged" ToolTip='<%# Container.DataItemIndex + 1 %>'>
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblcoreU" runat="server" Text='<% #Eval("ELECTIVEGP")%>' Visible="false"></asp:Label>
                                                                <asp:HiddenField ID="hdfcourseU" runat="server" Value='<% #Eval("COURSENO")%>' />

                                                            </td>
                                                            <td style="text-align: center" class="d-none">
                                                                <asp:DropDownList ID="ddlcoregroupU" runat="server" CssClass="form-control"
                                                                    AppendDataBoundItems="True" Enabled="false">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="hdfValue1U" runat="server" Value='<%# Container.DataItemIndex + 1 %>' />
                                                                <asp:Label ID="lblelectiveU" runat="server" Text='<% #Eval("GROUPNO")%>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td style="text-align: center">
                                                                <asp:CheckBox ID="chkofferedU" runat="server" ToolTip='<%# Eval("COURSENO") %>' Enabled='<%# (Eval("EXAM_REGISTERED").ToString() == "1" && Eval("REGULAR").ToString() == "1") ? false : true %>' /></td>
                                                            <td style="text-align: center" runat="server" visible="false">
                                                                <asp:CheckBox ID="chkSpecialU" runat="server" ToolTip='<%# Eval("BACKLOG") %>' Enabled='<%# (Eval("EXAM_REGISTERED").ToString() == "1" && Eval("BACKLOG").ToString() == "1") ? false : true %>' /></td>
                                                            <td style="text-align: center">
                                                                <%-- <asp:DropDownList ID="ddlsem" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>--%>
                                                                <asp:ListBox ID="ddlsemU" runat="server" CssClass="form-control multi-select-demo" Enabled="false"
                                                                    SelectionMode="Multiple" AppendDataBoundItems="true" onblur="getVal();"></asp:ListBox>
                                                                <asp:Label ID="LblSemNoU" runat="server" Text='<% #Eval("SEMESTERNO")%>' ToolTip='<% #Eval("OFFERED")%>' Visible="false"></asp:Label>

                                                            </td>

<%--                                                            <td>
                                                                <asp:ListBox ID="lstbSchemeU" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true"></asp:ListBox>
                                                            </td>--%>
                                                            <td style="text-align: center" runat="server" visible="false">
                                                                <asp:TextBox ID="txtInternU" runat="server" CssClass="form-control" Text='<% #Eval("INTERNAL_WEIGHTAGE")%>' onblur="return CheckMarkS(this);" onkeypress="return onlyNumberKeyS(event)"></asp:TextBox></td>
                                                            <td style="text-align: center" runat="server" visible="false">
                                                                <asp:TextBox ID="txtExternU" runat="server" CssClass="form-control" Text='<% #Eval("EXTERNAL_WEIGHTAGE")%>' onblur="return CheckMarkS(this);" onkeypress="return onlyNumberKeyS(event)"></asp:TextBox></td>

                                                            <td style="text-align: center; display: none">
                                                                <asp:TextBox ID="txtToatlU" runat="server" CssClass="form-control" Text='<% #Eval("CA_PERCENTAGE")%>' onkeypress="return onlyNumberKeyS(event)"></asp:TextBox>
                                                            </td>
                                                            <td style="text-align: center" runat="server" visible="false">
                                                                <asp:TextBox ID="txtcaU" runat="server" CssClass="form-control" Text='<% #Eval("CA_PERCENTAGE")%>' onkeypress="return onlyNumberKeyS(event)"></asp:TextBox>
                                                            </td>
                                                            <td style="text-align: center" runat="server" visible="false">
                                                                <asp:TextBox ID="txtfinalU" runat="server" CssClass="form-control" Text='<% #Eval("FINAL_PERCENTAGE")%>' onkeypress="return onlyNumberKeyS(event)"></asp:TextBox></td>

                                                            <td style="text-align: center" runat="server" visible="false">
                                                                <asp:TextBox ID="txtoverallU" runat="server" CssClass="form-control" Text='<% #Eval("OVERALL_PERCENTAGE")%>' onkeypress="return onlyNumberKeyS(event)"></asp:TextBox>
                                                            </td>
                                                            <td style="text-align: center" runat="server" visible="false">
                                                                <asp:DropDownList ID="ddlmodulelicU" runat="server" CssClass="form-control select2 select-click" AppendDataBoundItems="True">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lbllicU" runat="server" Text='<% #Eval("MODULELIC")%>' ToolTip='<%#Eval("REGULAR") %>' Visible="false"></asp:Label></td>
                                                            </td>
                                                        </tr>

                                                    </ItemTemplate>

                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>



                                    </ContentTemplate>

                                    <Triggers>
                                        <%--<asp:PostBackTrigger ControlID="btnShow" />--%>
                                        <asp:PostBackTrigger ControlID="btnreport" />
                                        <asp:PostBackTrigger ControlID="btnExcelExport" />
                                    </Triggers>

                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane" id="tab_2" onclick="document.getElemtbyId('sel-tab').value=2;">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updCopyCourseOffered"
                                        DynamicLayout="true" DisplayAfter="0">
                                        <ProgressTemplate>
                                            <div id="preloader">
                                                <div class="loader-container">
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                </div>

                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <asp:UpdatePanel ID="updCopyCourseOffered" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblSessionNameOld" runat="server" Font-Bold="true">Previous Session</asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSessionOld" runat="server" TabIndex="8" CssClass="form-control select2 select-click"
                                                            AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSessionOld_SelectedIndexChanged"
                                                            ToolTip="Please Select Previous Session" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSessionOld"
                                                            Display="None" ErrorMessage="Please Select Previous Session" InitialValue="0"
                                                            ValidationGroup="CopySession" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblDYCollegeNew" runat="server" Font-Bold="true">Faculty /School Name</asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCollegeNew" runat="server" TabIndex="9" CssClass="form-control select2 select-click"
                                                            AppendDataBoundItems="True" AutoPostBack="true"
                                                            ToolTip="Please Select Faculty/School Name" OnSelectedIndexChanged="ddlCollegeNew_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvFaculty" runat="server" ControlToValidate="ddlCollegeNew"
                                                            Display="None" ErrorMessage="Please Select Faculty/School Name" InitialValue="0"
                                                            ValidationGroup="CopySession" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblDYDegreeNew" runat="server" Font-Bold="true">Degree</asp:Label>
                                                        </div>
                                                        <asp:ListBox ID="ddlDegreeNew" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"
                                                            SelectionMode="multiple" OnSelectedIndexChanged="ddlDegreeNew_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                                        <%-- <asp:DropDownList ID="ddlDegreeNew" runat="server" TabIndex="10" CssClass="form-control select2 select-click"
                                                            AppendDataBoundItems="True"
                                                            ToolTip="Please Select Program" OnSelectedIndexChanged="ddlDegreeNew_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                                        </asp:DropDownList>--%>
                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlDegreeNew"
                                                            Display="None" ErrorMessage="Please Select Program" InitialValue="0"
                                                            ValidationGroup="CopySession" />--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divCurriculum" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblDYSchemeNew" runat="server" Font-Bold="true">Curriculum</asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSchemeNew" runat="server" TabIndex="11" CssClass="form-control select2 select-click"
                                                            AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSchemeNew_SelectedIndexChanged"
                                                            ToolTip="Please Select Curriculum" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvCurriculum" runat="server" ControlToValidate="ddlSchemeNew"
                                                            Display="None" ErrorMessage="Please Select Curriculum" InitialValue="0"
                                                            ValidationGroup="CopySession" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblSessionNameNew" runat="server" Font-Bold="true">New Session </asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSessionNew" runat="server" TabIndex="12" CssClass="form-control select2 select-click"
                                                            AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSessionNew_SelectedIndexChanged" AutoPostBack="true"
                                                            ToolTip="Please Select New Session">
                                                            <%-- AutoPostBack="true"  --%>
                                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvNewSessionName" runat="server" ControlToValidate="ddlSessionNew"
                                                            Display="None" ErrorMessage="Please Select New Session" InitialValue="0" ValidationGroup="CopySession" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <%--   <sup>* </sup>--%>
                                                            <asp:Label ID="lblSemester" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:ListBox ID="lstbxSemester" runat="server" AppendDataBoundItems="true"
                                                            CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>


                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <%--   <sup>* </sup>--%>
                                                            <asp:Label ID="lblDYBranch" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:ListBox ID="lstbxBranch" runat="server" AppendDataBoundItems="true"
                                                            CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>
                                                        <%-- <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="9" CssClass="form-control select2 select-click"
                                                            AppendDataBoundItems="True" 
                                                            ToolTip="Please Select 	Specialization">ddlSessionNew_SelectedIndexChanged
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        --%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divProgram" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="Label1" runat="server" Font-Bold="true">New Program</asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlNewProgram" runat="server" TabIndex="10" CssClass="form-control select2 select-click"
                                                            AppendDataBoundItems="True" OnSelectedIndexChanged="ddlNewProgram_SelectedIndexChanged"
                                                            ToolTip="Please Select New Program" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvNewDegreee" runat="server" ControlToValidate="ddlNewProgram"
                                                            Display="None" ErrorMessage="Please Select New Program" InitialValue="0"
                                                            ValidationGroup="CopySession" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divNewCurriculum" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblNewCurriculum" runat="server" Font-Bold="true">New Curriculum</asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlNewCurriculum" runat="server" TabIndex="11" CssClass="form-control select2 select-click"
                                                            AppendDataBoundItems="True"
                                                            ToolTip="Please Select New Regulation">
                                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvNewCurriculum" runat="server" ControlToValidate="ddlNewCurriculum"
                                                            Display="None" ErrorMessage="Please Select New Curriculum" InitialValue="0"
                                                            ValidationGroup="CopySession" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnCopyShow" runat="server" OnClick="btnCopyShow_Click" CssClass="btn btn-outline-info" Text="Show" ValidationGroup="CopySession" />
                                                <asp:LinkButton ID="btnCopySession" runat="server" ToolTip="Copy" OnClick="btnCopySession_Click" Visible="false"
                                                    CssClass="btn btn-outline-info" TabIndex="14" ValidationGroup="CopySession" OnClientClick="if ( ! UserConfirmation()) return false;">Copy</asp:LinkButton>

                                                <asp:Button ID="btnCancelNew" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                    TabIndex="15" CssClass="btn btn-outline-danger" OnClick="btnCancelNew_Click" />

                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="CopySession"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </div>
                                            <asp:Label ID="lblCountHeading" runat="server" Font-Bold="true" CssClass="d-none">Total Number of Records to Copy :   
                                                <asp:Label ID="lblCount" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                            </asp:Label>

                                            <div class="col-md-12">
                                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                                    <asp:ListView ID="lvCopyOffered" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Subject Offer List</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="mytable">
                                                                <thead>
                                                                    <tr>
                                                                        <th>
                                                                            <asp:CheckBox ID="chkhead" runat="server" onclick="return totAll(this);" /></th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblProgramShort"></asp:Label></th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblDYScheme"></asp:Label></th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblDYClgCode"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblDYSubjectCode"></asp:Label></th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lbltype"></asp:Label></th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblDYCredits"></asp:Label></th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblSemester"></asp:Label></th>
                                                                        <th runat="server" visible="false">CA%</th>
                                                                        <th runat="server" visible="false">Final%</th>
                                                                        <th runat="server" visible="false">Min CA%</th>
                                                                        <th runat="server" visible="false">Min Final%</th>
                                                                        <th runat="server" visible="false">Overall %</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody class="test">
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                            </div>
                                                        </LayoutTemplate>

                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="chkRow" runat="server" Checked='<%# Eval("NEWSESSIONNO").ToString() == string.Empty ? false : true %>' /></td>
                                                                <td>
                                                                    <asp:Label ID="lblProgramName" runat="server" Text='<%#Eval("CODE") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblCurriculum" runat="server" Text='<%#Eval("SCHEMENAME") %>' ToolTip='<%#Eval("SCHEMENO") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblCcode" runat="server" Text='<%#Eval("CCODE") %>' ToolTip='<%#Eval("COURSENO") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblModuleName" runat="server" Text='<%#Eval("COURSE_NAME") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblType" runat="server" Text='<%#Eval("ELECTIVENAME") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblCredits" runat="server" Text='<%#Eval("CREDITS") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblSem" runat="server" Text='<%#Eval("SEMESTERNAME") %>' ToolTip='<%#Eval("SEMESTERNO") %>'></asp:Label></td>
                                                                <td runat="server" visible="false">
                                                                    <asp:Label ID="lblWeighatge" runat="server" Text='<%#Eval("INTERNAL_WEIGHTAGE") %>'></asp:Label></td>
                                                                <td runat="server" visible="false">
                                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("EXTERNAL_WEIGHTAGE") %>'></asp:Label></td>
                                                                <td runat="server" visible="false">
                                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("CA_PERCENTAGE") %>'></asp:Label></td>
                                                                <td runat="server" visible="false">
                                                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("FINAL_PERCENTAGE") %>'></asp:Label></td>
                                                                <td runat="server" visible="false">
                                                                    <asp:Label ID="Label5" runat="server" Text='<%#Eval("OVERALL_PERCENTAGE") %>'></asp:Label></td>
                                                            </tr>

                                                        </ItemTemplate>

                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>

                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane" id="tab_3" onclick="document.getElemtbyId('sel-tab').value=3;">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updCopyCourseOffered"
                                        DynamicLayout="true" DisplayAfter="0">
                                        <ProgressTemplate>
                                            <div id="preloader">
                                                <div class="loader-container">
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                </div>

                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="Label6" runat="server" Font-Bold="true">Session Name</asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlMismatchSession" runat="server" TabIndex="1" CssClass="form-control select2 select-click"
                                                            AppendDataBoundItems="True" AutoPostBack="true" ToolTip="Please Select Session Long Name">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="Label7" runat="server" Font-Bold="true">Faculty /School Name</asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlMisMatchCollege" runat="server" TabIndex="2" CssClass="form-control select2 select-click"
                                                            AppendDataBoundItems="True" OnSelectedIndexChanged="ddlMisMatchCollege_SelectedIndexChanged"
                                                            ToolTip="Please Select Faculty/School Name" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="Label8" runat="server" Font-Bold="true">Degree</asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlMisMatchDegree" runat="server" TabIndex="3" CssClass="form-control select2 select-click"
                                                            AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlMisMatchDegree_SelectedIndexChanged"
                                                            ToolTip="Please Select Degree">
                                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <asp:Panel ID="Panel3" runat="server">
                                                    <asp:ListView ID="lvMisMatchMoudle" runat="server" Visible="true">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="mytables">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <%--     <th>Edit</th>--%>
                                                                        <th>Code</th>
                                                                        <th>Module Name</th>
                                                                        <th>Scheme</th>
                                                                        <th>Semester</th>
                                                                        <th>Core/Elective</th>
                                                                        <th>Module LIC</th>
                                                                        <th>Credits</th>
                                                                        <th>CA% </th>
                                                                        <th>Final%</th>
                                                                        <th>Min CA%</th>
                                                                        <th>Min Final%</th>
                                                                        <th>Overall %</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%#Eval("CCODE") %></td>
                                                                <td><%#Eval("COURSENAME") %></td>
                                                                <td><%#Eval("SCHEMENAME") %></td>
                                                                <td><%#Eval("SEMESTERNAME") %></td>
                                                                <td><%#Eval("ELECT_CORE") %></td>
                                                                <td><%#Eval("UA_FULLNAME") %></td>
                                                                <td><%#Eval("CREDITS") %></td>
                                                                <td><%#Eval("INTERNAL_WEIGHTAGE") %></td>
                                                                <td><%#Eval("EXTERNAL_WEIGHTAGE") %></td>
                                                                <td><%#Eval("CA_PERCENTAGE") %></td>
                                                                <td><%#Eval("FINAL_PERCENTAGE") %></td>
                                                                <td><%#Eval("OVERALL_PERCENTAGE") %></td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>

                                                </asp:Panel>

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

  <%--  <script>
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
                        else {
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

    </script>--%>

    <script>
        function getVal() {
            // alert("hii")
            var array = []
            var semno;
            var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

            for (var i = 0; i < checkboxes.length; i++) {

                //array.push(checkboxes[i].value)    
                if (semno === "undefined") {
                    semno = checkboxes[i].value + ',';
                }
                else {
                    semno += checkboxes[i].value + ',';

                }
            }
            alert(degreeNo);

            $('#<%= hdfsem.ClientID %>').val(semno);
        }
    </script>

    <%--   <script>
        function getValCheck() {
            $(<%=ddlsem.ClientID%>).multiselect({ selectAll: true });
        }
    </script>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>
    <script>
        $("[id*=ddlcore]").bind("change", function () {
            //Find and reference the GridView.
            var List = $(this).closest("table");
            var ddlValue = $(this).val();
            //If the CheckBox is Checked then enable the TextBoxes in thr Row.
            if (ddlValue == "2") {
                var td = $("td", $(this).closest("tr"));
                $("[id*=ddlcoregroup]", td).removeAttr("disabled");

            } else {
                var td = $("td", $(this).closest("tr"));
                $("[id*=ddlcoregroup]", td).attr("disabled", "disabled");
                //td.css({ "background-color": "#D8EBF2" });
                //$("input[type=dropdown]", td).removeAttr("disabled");
                //$("select", td).removeAttr("disabled");

            }
        });
    </script>
    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            $("[id*=ddlcore]").bind("change", function () {
                //Find and reference the GridView.
                var List = $(this).closest("table");
                var ddlValue = $(this).val();
                //If the CheckBox is Checked then enable the TextBoxes in thr Row.
                if (ddlValue == "2") {
                    var td = $("td", $(this).closest("tr"));
                    $("[id*=ddlcoregroup]", td).removeAttr("disabled");

                } else {
                    var td = $("td", $(this).closest("tr"));
                    $("[id*=ddlcoregroup]", td).attr("disabled", "disabled");
                    //td.css({ "background-color": "#D8EBF2" });
                    //$("input[type=dropdown]", td).removeAttr("disabled");
                    //$("select", td).removeAttr("disabled");

                }
            });
        });
    </script>
    <script>
        function UserConfirmation() {
            return confirm("Are you sure you want to copy?");
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

            var rowIndex = id.offsetParent.parentNode.rowIndex - 1;
            var cellIndex = id.offsetParent.cellIndex;
            //alert(rowIndex)
            var internal1 = 0; var External = 0; var txtToatl;

            internal1 = document.getElementById("ctl00_ContentPlaceHolder1_lvCourse_ctrl" + rowIndex + "_txtIntern").value;
            //alert(internal1)  ctl00_ContentPlaceHolder1_lvCourse_ctrl0_txtIntern
            if (internal1 > 100) {
                num = false;
                id.value = '';
                alert("CA(%) Is Not Greater Than 100")
                id.select();
                id.focus();
                internal1 = '';
            }
            External = document.getElementById("ctl00_ContentPlaceHolder1_lvCourse_ctrl" + rowIndex + "_txtExtern").value;
            if (External > 100) {
                num = false;
                id.value = '';
                alert("Final(%) Is Not Greater Than 100")
                id.select();
                id.focus();
                External = '';

            }
            if (internal1 == '') {
                var internal1 = 0;
            }

            TotaltMark = (Number(internal1) + Number(External))
            txtToatl = document.getElementById("ctl00_ContentPlaceHolder1_lvCourse_ctrl" + rowIndex + "_txtToatl").value = TotaltMark;

            //Total=internal1+External
            if (txtToatl > 100) {
                num = false;
                id.value = '';
                alert("CA(%) And Final(%) Total Is Not Greater Than 100")
                id.select();
                id.focus();
                External = '';
                txtToatl = '';
            }
            if (External == '') {
                var External = 0;
            }
        }

    </script>
    <script>

        function CheckMarkS(id) {

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

            var rowIndex = id.offsetParent.parentNode.rowIndex - 1;
            var cellIndex = id.offsetParent.cellIndex;
            //alert(rowIndex)
            var internal1 = 0; var External = 0; var txtToatl;

            internal1 = document.getElementById("ctl00_ContentPlaceHolder1_lvViewOfferModule_ctrl" + rowIndex + "_txtInternU").value;
            //alert(internal1)  ctl00_ContentPlaceHolder1_lvCourse_ctrl0_txtIntern
            if (internal1 > 100) {
                num = false;
                id.value = '';
                alert("CA(%) Is Not Greater Than 100")
                id.select();
                id.focus();
                internal1 = '';
            }
            External = document.getElementById("ctl00_ContentPlaceHolder1_lvViewOfferModule_ctrl" + rowIndex + "_txtExternU").value;
            if (External > 100) {
                num = false;
                id.value = '';
                alert("Final(%) Is Not Greater Than 100")
                id.select();
                id.focus();
                External = '';

            }
            if (internal1 == '') {
                var internal1 = 0;
            }

            TotaltMark = (Number(internal1) + Number(External))
            txtToatl = document.getElementById("ctl00_ContentPlaceHolder1_lvViewOfferModule_ctrl" + rowIndex + "_txtToatlU").value = TotaltMark;

            //Total=internal1+External
            if (txtToatl > 100) {
                num = false;
                id.value = '';
                alert("CA(%) And Final(%) Total Is Not Greater Than 100")
                id.select();
                id.focus();
                External = '';
                txtToatl = '';
            }
            if (External == '') {
                var External = 0;
            }
        }

    </script>
    <script>
        function onlyNumberKey(evt) {

            // Only ASCII character in that range allowed
            var ASCIICode = (evt.which) ? evt.which : evt.keyCode
            if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
                return false;
            return true;
        }
    </script>

    <script>
        function onlyNumberKeyS(evt) {

            // Only ASCII character in that range allowed
            var ASCIICode = (evt.which) ? evt.which : evt.keyCode
            if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
                return false;
            return true;
        }
    </script>
</asp:Content>
